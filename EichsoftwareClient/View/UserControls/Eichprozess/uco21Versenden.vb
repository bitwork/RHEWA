Imports System.Net.FtpClient
Imports System.Net
Imports System.IO
Imports EichsoftwareClient.EichsoftwareWebservice
Imports EichsoftwareClient

Public Class Uco21Versenden
    Inherits ucoContent
    Implements IRhewaEditingDialog

#Region "Member Variables"
    Private WithEvents objFTP As New clsFTP
    Private FTPUploadPath As String = "" 'Wert der gesetzt wird, falls ein Dokument zum FTP hochgeladen wird. Dieser wert entspricht dann dem reelen Dateipfad auf dem FTP Server

#End Region

#Region "Konstruktor"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub

    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
    End Sub
#End Region

#Region "Events"

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Versenden

        ParentFormular.RadButtonNavigateForwards.Enabled = False

        'daten füllen
        LoadFromDatabase()

        'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
        If DialogModus = enuDialogModus.lesend Then
            DisableControls(Me)
        End If
    End Sub


    Private Sub RadButtonUploadPath_Click(sender As Object, e As EventArgs) Handles RadButtonUploadPath.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            RadTextBoxControlUploadPath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub RadButtonAnRhewaSenden_Click(sender As Object, e As EventArgs) Handles RadButtonAnRhewaSenden.Click
        If ValidateControls() = False Then 'alle vorherigen controls validieren
            ParentFormular.NavigiereZuUco(GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten)
            Exit Sub

        End If

        If Not RadTextBoxControlUploadPath.Text.Trim.Equals("") Then
            If IO.File.Exists(RadTextBoxControlUploadPath.Text) Then
                initFTPUpload()
            Else
                SendObject()
            End If
        Else
            SendObject()
        End If

    End Sub


    Private Sub BackgroundWorkerUploadFTP_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerUploadFTP.ProgressChanged
        Try
            RadProgressBar.Value1 = e.UserState
            RadProgressBar.Text = CInt(CInt(e.UserState) / 1024) & " KB/ " & CInt(CInt(RadProgressBar.Maximum) / 1024) & " KB"
            Me.Refresh()
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorkerUploadFTP_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUploadFTP.RunWorkerCompleted
        Try
            RadProgressBar.Visible = False
            RadProgressBar.Value1 = 0
            Me.Enabled = True
            Me.ParentFormular.RadButtonNavigateBackwards.Enabled = True
            Me.ParentFormular.RadButtonNavigateForwards.Enabled = True
            RadProgressBar.Text = ""
            SendObject()
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub objFTP_ReportFTPProgress(Progress As Integer) Handles objFTP.ReportFTPProgress
        Try
            BackgroundWorkerUploadFTP.ReportProgress(0, Progress)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub BackgroundWorkerUploadFTP_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUploadFTP.DoWork
        InitUploadFileToFTP()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Methode welchen den gesammten Eichprozess an RHEWA versendet
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendObject()
        'testen der Verbindung

        If Not clsWebserviceFunctions.TesteVerbindung() Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung)
            Exit Sub
        End If

        'Bei Standardwaagen soll eine Bestätigung des Benutzers eingefordert werden
        If objEichprozess.AusStandardwaageErzeugt Then
            If InputBox(String.Format("Bitte bestätigen Sie mit ihrer HE-Kennung ({0}) die ordnungsgemäße Überprüfung.", AktuellerBenutzer.Instance.Lizenz.HEKennung), "Bestätigung").ToLower <> AktuellerBenutzer.Instance.Lizenz.HEKennung.ToLower Then
                MessageBox.Show("Die HE-Kennung stimmt nicht überrein. Das Versenden wird abgebrochen")
                Return
            End If
        End If

        Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
        Dim alterVersendetStatus = objEichprozess.FK_Bearbeitungsstatus
        VersendetStatusSetzen(objServerEichprozess)

        Using dbcontext As New Entities
            objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            objEichprozess.AusStandardwaageErzeugt = False 'Egal ob der Prozess versendet wird oder nicht, das Flag bei einer kopierten Leistung kann entfernt werden, da es sich jetzt um eine gültige Waage handelt
            objServerEichprozess = clsClientServerConversionFunctions.CopyServerObjectProperties(objServerEichprozess, objEichprozess, clsClientServerConversionFunctions.enuModus.ClientSendetAnRhewa)

            'verbindung öffnen
            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                If Not clsWebserviceFunctions.TesteVerbindung() Then
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung)
                    Exit Sub
                End If

                Dim objLiz = (From db In dbcontext.Lizensierung Where db.Lizenzschluessel = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And db.HEKennung = AktuellerBenutzer.Instance.Lizenz.HEKennung).FirstOrDefault

                'upload des Eichprotokolls
                LadeDatensatzHoch(Webcontext, objLiz, objServerEichprozess)

                'Eichmarken abziehen, wenn neu
                If alterVersendetStatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet Then ErgaenzeEichmarken(Webcontext, objLiz)
            End Using
        End Using
        ParentFormular.Close()
    End Sub

    Private Sub ErgaenzeEichmarken(Webcontext As EichsoftwareWebserviceClient, objLiz As Lizensierung)
        Try
            'nur versenden wenn der Eichprozess noch nicht versendet wurde.  Nach Absprache vom 07.06 soll nur einmalig abgezogen werden. Alle anderen korrekturen würden per Absprache geklärt.  Veraltet:Sonst würden zu oft Marken abgezogen. Ausserdem sonderfall: eichmarkenverwaltung wurde geändert (dirty)
            Webcontext.AddEichmarkenverwaltung(objLiz.HEKennung, objLiz.Lizenzschluessel, objLiz.FK_BenutzerID,
                                          objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl, objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl,
                              objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl, objEichprozess.Eichprotokoll.Sicherung_HinweismarkeAnzahl,
                             My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
        Catch e As Exception
            MessageBox.Show(e.StackTrace, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LadeDatensatzHoch(webcontext As EichsoftwareWebserviceClient, objLiz As Lizensierung, objServerEichprozess As ServerEichprozess)
        Dim bolSuccess As Boolean = False
        Try
            'prüfen ob neue WZ hinzuzufügen ist
            If Not objEichprozess Is Nothing Then
                If Not objEichprozess.Lookup_Waegezelle Is Nothing Then
                    If objEichprozess.Lookup_Waegezelle.Neu Then
                        'umwandeln von Client WZ in Server WZ
                        Dim objServerWZ As New EichsoftwareWebservice.ServerLookup_Waegezelle
                        clsClientServerConversionFunctions.CopyServerObjectPropertieWZs(objServerWZ, objEichprozess.Lookup_Waegezelle)
                        Dim neueWZID = webcontext.AddWaegezelle(objLiz.HEKennung, objLiz.Lizenzschluessel, objServerWZ, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                        If neueWZID = "" Then
                            '//zurücksetzen des Status
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalie)
                            EichprozessStatusZurueckSetzen()
                            Return
                        End If
                        ' überschreiben der WZ mit der Serverseitigen. für den Fall, das die WZ die neu war, bereits von einem anderem Benutzer angelegt wurde aber noch nicht freigegeben
                        objServerEichprozess._FK_Waegezelle = neueWZID
                        For Each item In objServerEichprozess._ServerMogelstatistik
                            item._FK_Waegezelle = neueWZID
                        Next
                    End If
                End If
            End If

            bolSuccess = webcontext.AddEichprozess(objLiz.HEKennung, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, Version)
            If bolSuccess = False Then
                '//zurücksetzen des Status
                MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalie)
                EichprozessStatusZurueckSetzen()
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '//zurücksetzen des Status
            EichprozessStatusZurueckSetzen()
            Return
        End Try
    End Sub



    Private Sub VersendetStatusSetzen(objServerEichprozess As ServerEichprozess)
        'auf versendet Status setzen
        If objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet Or objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Fehlerhaft Then 'wenn neu oder fehlerhaft auf versendet zurücksetzen
            'neuen Context aufbauen
            Using Context As New Entities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen
                        UpdateObjekt()
                        objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Wartet_auf_Bearbeitung

                        Try
                            Context.SaveChanges()
                        Catch ex As Entity.Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                            Exit Sub
                        End Try
                    End If
                End If
            End Using
        End If
    End Sub



    Private Sub EichprozessStatusZurueckSetzen()
        Using Context As New Entities
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess
                    'neuen Status zuweisen

                    ' Status zurück setzen
                    objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden

                    'SaveWithoutValidationNeeded(Me)
                    Try
                        Context.SaveChanges()
                    Catch ex2 As Entity.Validation.DbEntityValidationException
                        Exit Sub
                    End Try
                End If
            End If
        End Using
    End Sub


    Private Sub initFTPUpload()
        'initiere FTP Upload in Background thread
        If Not RadTextBoxControlUploadPath.Text.Trim.Equals("") Then
            If IO.File.Exists(RadTextBoxControlUploadPath.Text) Then
                If Not BackgroundWorkerUploadFTP.IsBusy Then
                    Dim file As New FileInfo(RadTextBoxControlUploadPath.Text)

                    'dateigröße ermitteln
                    Using stream = file.OpenRead
                        RadProgressBar.Maximum = stream.Length
                    End Using

                    Me.ParentFormular.RadButtonNavigateBackwards.Enabled = False
                    Me.ParentFormular.RadButtonNavigateForwards.Enabled = False
                    Me.Enabled = False
                    RadProgressBar.Value1 = 0
                    RadProgressBar.Visible = True
                    BackgroundWorkerUploadFTP.RunWorkerAsync()
                End If
            End If
        End If
    End Sub

    Public Sub InitUploadFileToFTP()
        Using dbcontext As New Entities
            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                'daten von WebDB holen
                Dim objLiz = (From db In dbcontext.Lizensierung Where db.Lizenzschluessel = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And db.HEKennung = AktuellerBenutzer.Instance.Lizenz.HEKennung).FirstOrDefault
                Dim objFTPDaten = Webcontext.GetFTPCredentials(objLiz.HEKennung, objLiz.Lizenzschluessel, objEichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                'aufbereiten der für FTP benötigten Verbindungsdaten
                If Not objFTPDaten Is Nothing Then

                    'get decrypted Password and configuration from Database

                    Dim password As String = objFTPDaten.FTPEncryptedPassword
                    ' original plaintext
                    Dim passPhrase As String = "Pas5pr@se"
                    ' can be any string
                    Dim saltValue As String = objFTPDaten.FTPSaltKey
                    ' can be any string
                    Dim hashAlgorithm As String = "SHA1"
                    ' can be "MD5"
                    Dim passwordIterations As Integer = 2
                    ' can be any number
                    Dim initVector As String = "@1B2c3D4e5F6g7H8"
                    ' must be 16 bytes
                    Dim keySize As Integer = 256
                    ' can be 192 or 128
                    Dim UploadfilePath = RadTextBoxControlUploadPath.Text

                    password = RijndaelSimple.Decrypt(password, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
                        keySize)

                    'datei upload. FTPUploadPath bekommt den reelen Pfad auf dem FTP Server
                    FTPUploadPath = objFTP.UploadFiletoFTP(objFTPDaten.FTPServername, objFTPDaten.FTPUserName, password, UploadfilePath)
                End If
            End Using
        End Using
    End Sub
#End Region



#Region "Interface Methods"
    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Versenden)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Versenden
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)

            End Try
        End If
    End Sub

    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New Entities
                context.Configuration.LazyLoadingEnabled = True
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End Using
        End If

        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(Me)

        End If
    End Sub

    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
        'wenn noch nicht gesendet wurde
        If objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet Then
            RadButtonAnRhewaSenden.Enabled = True
        ElseIf objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Fehlerhaft Then 'wenn fehlerhaft
            RadButtonAnRhewaSenden.Enabled = True
        Else
            RadButtonAnRhewaSenden.Enabled = False
        End If

        RadTextBoxControlUploadPath.Text = objEichprozess.UploadFilePath
    End Sub

    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        If FTPUploadPath.Equals("") Then
            objEichprozess.UploadFilePath = RadTextBoxControlUploadPath.Text

        Else
            objEichprozess.UploadFilePath = FTPUploadPath

        End If
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

    End Sub



    Protected Friend Overrides Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt
        'neuen Context aufbauen
        Using Context As New Entities
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess
                    'neuen Status zuweisen

                    'Speichern in Datenbank
                    UpdateObjekt()
                    Try
                        Context.SaveChanges()
                    Catch ex As Entity.Validation.DbEntityValidationException
                        For Each e In ex.EntityValidationErrors
                            MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                        Next
                    End Try
                End If
            End If
        End Using
    End Sub


    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaving = False
        Dim validationErrorName As String = ""
        ParentFormular.CurrentEichprozess = objEichprozess
        Me.ParentFormular.ValidiereRueckwaerts(validationErrorName, Me)

        'alle Controls validieren bevor versendet wird
        If validationErrorName.Equals("") = False Then
            Me.AbortSaving = True 'todo entfernen

        End If
        Return Not Me.AbortSaving
    End Function


    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(Me)
        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub


    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
        CloneAndSendServerObjekt()
    End Sub

    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Uco21Versenden))
        Lokalisierung(Me, resources)
    End Sub



#End Region



End Class