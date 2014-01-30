
Public Class ucoBeschaffenheitspruefung
    Inherits ucoContent


#Region "Member Variables"
    Private _ObjBeschaffenheitspruefung As Beschaffenheitspruefung
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken

#End Region


#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
    End Sub
#End Region

#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Beschaffenheitspruefung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Beschaffenheitspruefung
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
        'daten füllen
        LoadFromDatabase()
    End Sub

   

#End Region
    Private Sub LoadFromDatabase()
        'TH: Laden aus Datenbank
        Using Context As New EichsoftwareClientdatabaseEntities1
            _suspendEvents = True

            'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
            If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
                objEichprozess = (From Eichprozess In Context.Eichprozess.Include("Beschaffenheitspruefung").Include("Kompatiblitaetsnachweis") Select Eichprozess Where Eichprozess.ID = objEichprozess.ID).FirstOrDefault
                _ObjBeschaffenheitspruefung = objEichprozess.Beschaffenheitspruefung

                If _ObjBeschaffenheitspruefung Is Nothing Then
                    Context.Beschaffenheitspruefung.Create()
                    _ObjBeschaffenheitspruefung = Context.Beschaffenheitspruefung.Create()
                    objEichprozess.Beschaffenheitspruefung = _ObjBeschaffenheitspruefung

                End If
            Else
                _ObjBeschaffenheitspruefung = objEichprozess.Beschaffenheitspruefung
            End If
        End Using



        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.RadScrollablePanel1.PanelContainer.Controls
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
        _suspendEvents = False
    End Sub


    ''' <summary>
    ''' Lädt die Werte aus dem Beschaffenheitspruefungsobjekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        If Not _ObjBeschaffenheitspruefung Is Nothing Then
            RadCheckBoxAWG1.Checked = _ObjBeschaffenheitspruefung.AWG_Auslieferungszustand
            RadCheckBoxAWG2.Checked = _ObjBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
            RadCheckBoxAWG3.Checked = _ObjBeschaffenheitspruefung.AWG_KabelUnbeschaedigt

            RadCheckBoxWZ1.Checked = _ObjBeschaffenheitspruefung.WZ_ZulassungOIMLR60
            RadCheckBoxWZ2.Checked = _ObjBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
            RadCheckBoxWZ3.Checked = _ObjBeschaffenheitspruefung.WZ_KabelUnbeschaedigt
            RadCheckBoxWZ4.Checked = _ObjBeschaffenheitspruefung.WZ_AnschraubplattenEben
            RadCheckBoxWZ5.Checked = _ObjBeschaffenheitspruefung.WZ_VergussUnbeschaedigt

            RadCheckBoxVerbindungelemente1.Checked = _ObjBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
            RadCheckBoxVerbindungelemente2.Checked = _ObjBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
            RadCheckBoxVerbindungelemente3.Checked = _ObjBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
            RadCheckBoxVerbindungelemente4.Checked = _ObjBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt

            RadCheckBoxBruecke1.Checked = _ObjBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
            RadCheckBoxBruecke2.Checked = _ObjBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei
            RadCheckBoxBruecke3.Checked = _ObjBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
        End If

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        _ObjBeschaffenheitspruefung.AWG_Auslieferungszustand = RadCheckBoxAWG1.Checked
        _ObjBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = RadCheckBoxAWG2.Checked
        _ObjBeschaffenheitspruefung.AWG_KabelUnbeschaedigt = RadCheckBoxAWG3.Checked

        _ObjBeschaffenheitspruefung.WZ_ZulassungOIMLR60 = RadCheckBoxWZ1.Checked
        _ObjBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = RadCheckBoxWZ2.Checked
        _ObjBeschaffenheitspruefung.WZ_KabelUnbeschaedigt = RadCheckBoxWZ3.Checked
        _ObjBeschaffenheitspruefung.WZ_AnschraubplattenEben = RadCheckBoxWZ4.Checked
        _ObjBeschaffenheitspruefung.WZ_VergussUnbeschaedigt = RadCheckBoxWZ5.Checked

        _ObjBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = RadCheckBoxVerbindungelemente1.Checked
        _ObjBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = RadCheckBoxVerbindungelemente2.Checked
        _ObjBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = RadCheckBoxVerbindungelemente3.Checked
        _ObjBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = RadCheckBoxVerbindungelemente4.Checked

        _ObjBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = RadCheckBoxBruecke1.Checked
        _ObjBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei = RadCheckBoxBruecke2.Checked
        _ObjBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = RadCheckBoxBruecke3.Checked

        objEichprozess.FK_Beschaffenheitspruefung = _ObjBeschaffenheitspruefung.ID
    End Sub

#Region "Overrides"

    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If


            If ValidateControls() Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If _ObjBeschaffenheitspruefung.ID = 0 Then
                        _ObjBeschaffenheitspruefung = Context.Beschaffenheitspruefung.Create
                        Context.Beschaffenheitspruefung.Add(_ObjBeschaffenheitspruefung)
                        Context.SaveChanges()
                    End If

                    If _ObjBeschaffenheitspruefung.ID <> 0 Then
                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dbObjBeschaffenheitspruefung As Beschaffenheitspruefung = Context.Beschaffenheitspruefung.FirstOrDefault(Function(value) value.ID = _ObjBeschaffenheitspruefung.ID)
                        If Not dbObjBeschaffenheitspruefung Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            _ObjBeschaffenheitspruefung = dbObjBeschaffenheitspruefung
                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()

                            Dim dbobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
                            If Not dbobjEichprozess Is Nothing Then
                                'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                                objEichprozess = dbobjEichprozess
                                objEichprozess.FK_Beschaffenheitspruefung = _ObjBeschaffenheitspruefung.ID

                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                                End If

                                'Speichern in Datenbank
                                Context.SaveChanges()
                            End If


                        End If

                    End If
                End Using
                ParentFormular.CurrentEichprozess = objEichprozess
            End If
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Beschaffenheitspruefung)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Beschaffenheitspruefung
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso. Ergänzung: war ausdokumentiert, weil damit die Werte der NSW und WZ übeschrieben werden wenn man auf zurück klickt. Wenn es allerdings ausdokumenterit ist, funktioniert das anlegen einer neuen WZ nicht
        End If
    End Sub


    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        Me.AbortSaveing = False
        'prüfen ob alle Felder ausgefüllt sind
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadCheckBox Then
                        If Control.checked = False Then
                            AbortSaveing = true
                        End If
                    End If
                Next
            End If
        Next
        If AbortSaveing = True Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True

        'prüfen ob eine neue WZ angelegt wurde (über button und neuem Dialog vermutlich)
    End Function

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoBeschaffenheitspruefung))

        Me.RadGroupBoxWaegebruecke.Text = resources.GetString("RadGroupBoxWaegebruecke.Text")
        Me.RadLabel15.Text = resources.GetString("RadLabel15.Text")
        Me.RadCheckBoxBruecke1.Text = resources.GetString("RadCheckBoxBruecke1.Text")
        Me.RadCheckBoxBruecke3.Text = resources.GetString("RadCheckBoxBruecke3.Text")
        Me.RadCheckBoxBruecke2.Text = resources.GetString("RadCheckBoxBruecke2.Text")
        Me.RadLabel11.Text = resources.GetString("RadLabel11.Text")
        Me.RadLabel13.Text = resources.GetString("RadLabel13.Text")
        Me.RadGroupBoxWaegezellen.Text = resources.GetString("RadGroupBoxWaegezellen.Text")
        Me.RadLabel7.Text = resources.GetString("RadLabel7.Text")
        Me.RadLabel5.Text = resources.GetString("RadLabel5.Text")
        Me.RadLabel3.Text = resources.GetString("RadLabel3.Text")
        Me.RadLabel6.Text = resources.GetString("RadLabel6.Text")
        Me.RadLabel8.Text = resources.GetString("RadLabel8.Text")
        Me.RadCheckBoxWZ1.Text = resources.GetString("RadCheckBoxWZ1.Text")
        Me.RadCheckBoxWZ2.Text = resources.GetString("RadCheckBoxWZ2.Text")
        Me.RadCheckBoxWZ4.Text = resources.GetString("RadCheckBoxWZ4.Text")
        Me.RadCheckBoxWZ3.Text = resources.GetString("RadCheckBoxWZ3.Text")
        Me.RadCheckBoxWZ5.Text = resources.GetString("RadCheckBoxWZ5.Text")
        Me.RadGroupBoxVerbindungselemente.Text = resources.GetString("RadGroupBoxVerbindungselemente.Text")
        Me.RadLabel16.Text = resources.GetString("RadLabel16.Text")
        Me.RadLabel14.Text = resources.GetString("RadLabel14.Text")
        Me.RadLabel12.Text = resources.GetString("RadLabel12.Text")
        Me.RadLabel10.Text = resources.GetString("RadLabel10.Text")
        Me.RadCheckBoxVerbindungelemente1.Text = resources.GetString("RadCheckBoxVerbindungelemente1.Text")
        Me.RadCheckBoxVerbindungelemente3.Text = resources.GetString("RadCheckBoxVerbindungelemente3.Text")
        Me.RadCheckBoxVerbindungelemente2.Text = resources.GetString("RadCheckBoxVerbindungelemente2.Text")
        Me.RadCheckBoxVerbindungelemente4.Text = resources.GetString("RadCheckBoxVerbindungelemente4.Text")
        Me.RadGroupBoxAuswerteGeraete.Text = resources.GetString("RadGroupBoxAuswerteGeraete.Text")
        Me.RadLabel1.Text = resources.GetString("RadLabel1.Text")
        Me.RadLabel4.Text = resources.GetString("RadLabel4.Text")
        Me.RadLabel2.Text = resources.GetString("RadLabel2.Text")
        Me.RadCheckBoxAWG1.Text = resources.GetString("RadCheckBoxAWG1.Text")
        Me.RadCheckBoxAWG3.Text = resources.GetString("RadCheckBoxAWG3.Text")
        Me.RadCheckBoxAWG2.Text = resources.GetString("RadCheckBoxAWG2.Text")


        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Beschaffenheitspruefung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Beschaffenheitspruefung
            Catch ex As Exception
            End Try
        End If


    End Sub
 

#End Region

    ''' <summary>
    ''' da die checkboxen keinen Text haben, sieht man den Fokus nicht. Workaround der border aktiviert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBox_GotFocus(sender As Object, e As EventArgs) Handles RadCheckBoxWZ5.GotFocus, RadCheckBoxWZ4.GotFocus, RadCheckBoxWZ3.GotFocus, RadCheckBoxWZ2.GotFocus, RadCheckBoxWZ1.GotFocus, RadCheckBoxVerbindungelemente4.GotFocus, RadCheckBoxVerbindungelemente3.GotFocus, RadCheckBoxVerbindungelemente2.GotFocus, RadCheckBoxVerbindungelemente1.GotFocus, RadCheckBoxBruecke3.GotFocus, RadCheckBoxBruecke2.GotFocus, RadCheckBoxBruecke1.GotFocus, RadCheckBoxAWG3.GotFocus, RadCheckBoxAWG2.GotFocus, RadCheckBoxAWG1.GotFocus
        If _suspendEvents Then Exit Sub
        Dim c As Telerik.WinControls.UI.RadCheckBox

        If (TypeOf sender Is Telerik.WinControls.UI.RadCheckBox) Then
            c = CType(sender, Telerik.WinControls.UI.RadCheckBox)

            c.ButtonElement.CheckMarkPrimitive.Border.BottomColor = Color.FromArgb(255, 0, 102, 51)
            c.ButtonElement.CheckMarkPrimitive.Border.LeftColor = Color.FromArgb(255, 0, 102, 51)
            c.ButtonElement.CheckMarkPrimitive.Border.RightColor = Color.FromArgb(255, 0, 102, 51)
            c.ButtonElement.CheckMarkPrimitive.Border.TopColor = Color.FromArgb(255, 0, 102, 51)


        End If
    End Sub
    ''' <summary>
    ''' da die checkboxen keinen Text haben, sieht man den Fokus nicht. Workaround der border deaktivert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBox_LostFocus(sender As Object, e As EventArgs) Handles RadCheckBoxWZ5.LostFocus, RadCheckBoxWZ4.LostFocus, RadCheckBoxWZ3.LostFocus, RadCheckBoxWZ2.LostFocus, RadCheckBoxWZ1.LostFocus, RadCheckBoxVerbindungelemente4.LostFocus, RadCheckBoxVerbindungelemente3.LostFocus, RadCheckBoxVerbindungelemente2.LostFocus, RadCheckBoxVerbindungelemente1.LostFocus, RadCheckBoxBruecke3.LostFocus, RadCheckBoxBruecke2.LostFocus, RadCheckBoxBruecke1.LostFocus, RadCheckBoxAWG3.LostFocus, RadCheckBoxAWG2.LostFocus, RadCheckBoxAWG1.LostFocus
        If _suspendEvents Then Exit Sub
        Dim c As Telerik.WinControls.UI.RadCheckBox

        If (TypeOf sender Is Telerik.WinControls.UI.RadCheckBox) Then
            c = CType(sender, Telerik.WinControls.UI.RadCheckBox)
            c.ButtonElement.CheckMarkPrimitive.Border.BottomColor = Color.FromArgb(255, 173, 176, 173)
            c.ButtonElement.CheckMarkPrimitive.Border.LeftColor = Color.FromArgb(255, 173, 176, 173)
            c.ButtonElement.CheckMarkPrimitive.Border.RightColor = Color.FromArgb(255, 173, 176, 173)
            c.ButtonElement.CheckMarkPrimitive.Border.TopColor = Color.FromArgb(255,  173, 176, 173)
        End If
    End Sub

    'Entsperrroutine
    Protected Friend Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.RadScrollablePanel1.PanelContainer.Controls
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
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                    Try
                        'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                        Webcontext.AddEichprozess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, objServerEichprozess)

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
End Class
