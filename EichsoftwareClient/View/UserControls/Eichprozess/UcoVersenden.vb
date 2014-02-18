Imports System.Net.FtpClient
Imports System.Net
Imports System.IO

Public Class UcoVersenden
    Inherits ucoContent
    Private _bolEichprozessIsDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann 
    Private FTPUploadPath As String = "" 'Wert der gesetzt wird, falls ein Dokument zum FTP hochgeladen wird. Dieser wert entspricht dann dem reelen Dateipfad auf dem FTP Server
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


    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Versenden)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Versenden
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Versenden

        ParentFormular.RadButtonNavigateForwards.Enabled = False


        'daten füllen
        LoadFromDatabase()


        'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
        If DialogModus = enuDialogModus.lesend Then
            For Each Control In Me.Controls
                Try
                    Control.readonly = True
                Catch ex As Exception
                    Try
                        Control.isreadonly = True
                    Catch ex2 As Exception
                        Try
                            Control.enabled = False
                        Catch ex3 As Exception
                        End Try
                    End Try
                End Try
            Next
        End If
    End Sub

    Private Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                context.Configuration.LazyLoadingEnabled = True
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.ID = objEichprozess.ID).FirstOrDefault
            End Using
        End If

        FillControls()


        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.Controls
                Try
                    Control.readonly = True
                Catch ex As Exception
                    Try
                        Control.isreadonly = True
                    Catch ex2 As Exception
                        Try
                            Control.enabled = False
                        Catch ex3 As Exception
                        End Try
                    End Try
                End Try
            Next
        End If
    End Sub

    Private Sub FillControls()
        'wenn noch nicht gesendet wurde
        If objEichprozess.FK_Bearbeitungsstatus = 4 Then
            RadButtonAnRhewaSenden.Enabled = True
        ElseIf objEichprozess.FK_Bearbeitungsstatus = 2 Then 'wenn fehlerhaft
            RadButtonAnRhewaSenden.Enabled = True
        Else
            RadButtonAnRhewaSenden.Enabled = False
        End If

        RadTextBoxControlUploadPath.Text = objEichprozess.UploadFilePath
    End Sub

    Private Sub UpdateObject()
        objEichprozess.UploadFilePath = FTPuploadpath
    End Sub

    ''' <summary>
    ''' Methode welchen den gesammten Eichprozess an RHEWA versendet
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendObject()
        Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess

        'auf versendet Status setzen
        If objEichprozess.FK_Bearbeitungsstatus = 4 Or objEichprozess.FK_Bearbeitungsstatus = 2 Then 'wenn neu oder fehlerhaft auf versendet zurücksetzen
            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen
                        UpdateObject()
                        objEichprozess.FK_Bearbeitungsstatus = 1
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit RHEWA sich den DS von Anfang angucken kann

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

        Using dbcontext As New EichsoftwareClientdatabaseEntities1
            objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.ID = objEichprozess.ID).FirstOrDefault

            objServerEichprozess = clsServerHelper.CopyObjectProperties(objServerEichprozess, objEichprozess)

            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Exit Sub
                End Try


                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                Try

                    Webcontext.AddEichprozess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                Catch ex As Exception
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Status zurück setzen
                    objEichprozess.FK_Bearbeitungsstatus = 4
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    SaveWithoutValidationNeeded(Me)
                End Try
                Try
                    'nur versenden wenn der Eichprozess noch nicht versendet wurde. Sonst würden zu oft Marken abgezogen

                    If objEichprozess.FK_Bearbeitungsstatus = 1 Then
                        Webcontext.AddEichmarkenverwaltung(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, objEichprozess.Eichprotokoll.FK_Identifikationsdaten_SuperOfficeBenutzer, _
                                                       objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl, objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl, _
                                                       objEichprozess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl, objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl, _
                                                       objEichprozess.Eichprotokoll.Sicherung_GruenesMAnzahl, objEichprozess.Eichprotokoll.Sicherung_CEAnzahl, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                    End If

                    ParentFormular.Close()
                Catch e As Exception
                    MessageBox.Show(e.StackTrace, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen



                        'Speichern in Datenbank
                        UpdateObject()
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

            ParentFormular.CurrentEichprozess = objEichprozess
        End If


    End Sub

    'Speicherroutine
    Protected Friend Overrides Sub SaveWithoutValidationNeeded(usercontrol As System.Windows.Forms.UserControl)
        MyBase.SaveWithoutValidationNeeded(usercontrol)
        If Me.Equals(usercontrol) Then
            If DialogModus = enuDialogModus.lesend Then
                UpdateObject()
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen



                        'Speichern in Datenbank
                        UpdateObject()
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

            ParentFormular.CurrentEichprozess = objEichprozess
        End If


    End Sub

    ''' <summary>
    ''' aktualisieren der Oberfläche wenn nötig
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Friend Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Versenden)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Versenden
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

    Private Sub RadButtonUploadPath_Click(sender As Object, e As EventArgs) Handles RadButtonUploadPath.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            RadTextBoxControlUploadPath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub RadButtonAnRhewaSenden_Click(sender As Object, e As EventArgs) Handles RadButtonAnRhewaSenden.Click
        'hinzufügen zur Eichmarkenverwaltung

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

    'Entsperrroutine
    Protected Friend Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.Controls
            Try
                Control.readonly = Not Control.readonly
            Catch ex As Exception
                Try
                    Control.isreadonly = Not Control.isReadonly
                Catch ex2 As Exception
                    Try
                        Control.enabled = Not Control.enabled
                    Catch ex3 As Exception
                    End Try
                End Try
            End Try
        Next

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Friend Overrides Sub VersendenNeeded(TargetUserControl As UserControl)
        MyBase.VersendenNeeded(TargetUserControl)

        If Me.Equals(TargetUserControl) Then

            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Eichbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()

                'erzeuegn eines Server Objektes auf basis des aktuellen DS
                objServerEichprozess = clsServerHelper.CopyObjectProperties(objServerEichprozess, objEichprozess)
                Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                    Try
                        Webcontext.Open()
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                    Try
                        'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                        Webcontext.AddEichprozess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                        'schließen des dialoges
                        ParentFormular.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ' Status zurück setzen
                        Exit Sub
                    End Try
                End Using
            End Using
        End If
    End Sub

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UcoVersenden))



        Me.RadButtonUploadPath.Text = resources.GetString("RadButton1.Text")




        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Versenden)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Versenden
            Catch ex As Exception
            End Try
        End If


    End Sub


#Region "FTP Upload"
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

    Private Sub BackgroundWorkerUploadFTP_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerUploadFTP.DoWork
        UploadFileToFTP(e)
    End Sub

    Private Sub BackgroundWorkerUploadFTP_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerUploadFTP.ProgressChanged
        Try
            RadProgressBar.Value1 = e.UserState
            RadProgressBar.Text = CInt(CInt(e.UserState) / 1024) & " KB/ " & CInt(CInt(RadProgressBar.Maximum) / 1024) & " KB"
            Me.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorkerUploadFTP_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerUploadFTP.RunWorkerCompleted
        Try
            RadProgressBar.Visible = False
            RadProgressBar.Value1 = 0
            Me.Enabled = True
            Me.ParentFormular.RadButtonNavigateBackwards.Enabled = True
            Me.ParentFormular.RadButtonNavigateForwards.Enabled = True

            SendObject()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UploadFileToFTP(ByVal e As System.ComponentModel.DoWorkEventArgs)
        Using dbcontext As New EichsoftwareClientdatabaseEntities1
            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                'daten von WebDB holen
                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                Dim objFTPDaten = Webcontext.GetFTPCredentials(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, objEichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


                'öffnen der FTP connection
                If Not objFTPDaten Is Nothing Then
                    Using conn As New FtpClient()


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


                        password = RijndaelSimple.Decrypt(password, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, _
                            keySize)


                        'FTP Upload
                        conn.Host = objFTPDaten.FTPServername
                        conn.Credentials = New NetworkCredential(objFTPDaten.FTPUserName, password)
                        conn.Connect()

                        If conn.IsConnected Then
                            Dim file As New FileInfo(RadTextBoxControlUploadPath.Text)

                            Dim extension As String = file.Extension
                            Dim Filename As String = file.Name.Replace(extension, "")
                            Dim FileNameOriginal As String = Filename

                            'check if file exists
                            Dim counter As Integer = 0
                            If conn.FileExists(Filename & extension) Then
                                'versuche es erneut
                                Do
                                    counter += 1
                                    Filename = FileNameOriginal & "(" & counter & ")"
                                Loop While conn.FileExists(Filename & extension)
                            End If

                            Using ostream As Stream = conn.OpenWrite(Filename & extension)
                                ' istream.Position is incremented accordingly to the writes you perform

                                FTPUploadPath = Filename & extension

                                Try
                                    Dim sumbytes As Integer
                                    Const buffer As Integer = 2048
                                    Dim contentRead As Byte() = New Byte(buffer - 1) {}
                                    Dim bytesRead As Integer

                                    Using fs As FileStream = file.OpenRead()
                                        Do
                                            bytesRead = fs.Read(contentRead, 0, buffer)
                                            sumbytes += bytesRead

                                            If bytesRead > 0 Then
                                                ostream.Write(contentRead, 0, bytesRead)
                                                BackgroundWorkerUploadFTP.ReportProgress(0, sumbytes)
                                            End If
                                        Loop While (bytesRead > 0)
                                        fs.Close()
                                    End Using


                                Finally

                                    ostream.Close()
                                End Try
                            End Using
                        End If


                    End Using
                End If
            End Using
        End Using

        'objEichprozess.UploadFilePath = FTPResult
    End Sub

#End Region

End Class
