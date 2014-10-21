Imports System.Globalization
Imports EichsoftwareClient.My.Resources
Imports System.IO

''' <summary>
''' Haupt Container für die gesamte Anwendung. In das Mainpanel werden die jeweils benötigten UserControls geladen. Die Ucos kommunizieren über Properties immer mit dem MainContainer
''' </summary>
''' <remarks></remarks>
Public Class FrmMainContainer

#Region "Membervariables"
    ' auflistung aller aktuellen UCOs, damit diese nicht immer neu erzeugt werden müssen
    Private _ListofUcos As New List(Of ucoContent)

    ''' <summary>
    ''' Das aktuelle UserControl welches im Inhaltsfenster angezeigt wird
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private WithEvents _CurrentUco As ucoContent
    ''' <summary>
    ''' uco zur Navigation in der Breadcrump Leiste oben rechts
    ''' </summary>
    ''' <remarks></remarks>
    Friend WithEvents BreadCrumb As ucoAmpel

    ''' <summary>
    ''' uco zum benutzer wechseln welches oben rechts auf dem maindialog eingeblendet wird
    ''' </summary>
    ''' <remarks></remarks>
    Friend objUCOBenutzerwechsel As ucoBenutzerwechsel


#End Region

#Region "Enumeratoren"
    Enum enuDialogModus
        normal = 0
        lesend = 1
        korrigierend = 2
    End Enum
#End Region

#Region "Constructor"
    Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'zuweisen des custom Telerik Themes
        Telerik.WinControls.ThemeResolutionService.LoadPackageResource("EichsoftwareClient.RHEWAGREEN.tssp") 'Pfad zur Themedatei
        Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "RHEWAGREEN" 'standard Themename
    End Sub

    Sub New(ByVal pEichprozess As Eichprozess, Optional ByVal penumDialogModus As enuDialogModus = enuDialogModus.normal)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        'zuweisen des Eichprozesses
        Me.CurrentEichprozess = pEichprozess
        Me.DialogModus = penumDialogModus
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' auflistung aller aktuellen UCOs, damit diese nicht immer neu erzeugt werden müssen
    ''' </summary>
    ''' <value>The P listof ucos.</value>
    Public ReadOnly Property AllUcos() As List(Of ucoContent)
        Get
            Return _ListofUcos
        End Get
    End Property

    ''' <summary>
    ''' Mit dieser Property kann der LEsemodus des UCOs eingestellt werden. Normal meint einen Client der eine Eichung anlegend. Im Lesenden Modus darf keine änderung vorgenommen / gespeichert werden. Im Korrigierenden Modus ändern RHEWA die Eichung eines externen bevollmächtigten
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Property DialogModus As enuDialogModus = enuDialogModus.normal
    ''' <summary>
    ''' Überschrift des aktuellen Programmschrittes
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor></commentauthor>
    Protected Friend Property GETSETHeaderText As String
        Get
            Return lblHeaderText.Text
        End Get
        Set(value As String)
            lblHeaderText.Text = value
        End Set
    End Property

    'der aktuelle Eichprozess
    Public Property CurrentEichprozess As Eichprozess

    ''' <summary>
    ''' aktuell angezeigtes UCO im Main Container
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentUCO As UserControl
        Get
            Return _CurrentUco
        End Get
    End Property

#End Region

#Region "Eigene Events"
    'Event welches angibt das in Datenbank gespeichert werden muss
    Public Event SaveNeeded(ByVal TargetUserControl As UserControl)
    Public Event SaveWithoutValidationNeeded(ByVal TargetUserControl As UserControl)
    Public Event LokalisierungNeeded(ByVal TargetUserControl As UserControl)
    Public Event UpdateNeeded(ByVal TargetUserControl As UserControl)
    Public Event EntsperrungNeeded()
    Public Event VersendenNeeded(ByVal TargetUserControl As UserControl)
#End Region

#Region "Methods Functions"
    ''' <summary>
    ''' Setzt den Text der Hilfeleiste als HTML Formatierten Text
    ''' </summary>
    ''' <param name="Helptext">Der Text für die Hilfe. Unterstützt HTML-Formatierungen</param>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor></commentauthor>
    Protected Friend Sub SETContextHelpText(ByVal Helptext As String)
        'Formatierten Text dem Steuerelement zuweisen
        RadLabelContextHelp.Text = Helptext

        'größe des Labels auf die Breite des Panels legen. Der manuelle Weg wurde gewählt, so das nur ein Horizontaler SCrollbalken entsteht und kein Vertikaler
        RadLabelContextHelp.MaximumSize = New Size(RadScrollablePanelContextHelp.Size.Width - 30, 0)
    End Sub


    ''' <summary>
    ''' tauscht das aktuell zur Ansicht gebrachte UCO mit dem aus dem Parameter aus. Die anderen werden nicht gelöscht und befinden sich im hintergrund
    ''' </summary>
    ''' <param name="uco"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub ChangeActiveContentUserControl(ByVal uco As UserControl)
        If _ListofUcos.Contains(uco) Then
            _CurrentUco = uco

            If Not Me.SplitPanelContent1.Controls.Contains(_CurrentUco) Then
                Me.SplitPanelContent1.Controls.Add(_CurrentUco)
            End If

            _CurrentUco.BringToFront()
            _CurrentUco.Dock = DockStyle.Fill
        End If


        If Not CurrentEichprozess Is Nothing Then
            Select Case _CurrentUco.EichprozessStatusReihenfolge
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                    RadButtonNavigateBackwards.Enabled = False
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    RadButtonNavigateBackwards.Enabled = True
                    RadButtonNavigateForwards.Enabled = False
                Case Else
                    RadButtonNavigateBackwards.Enabled = True
            End Select
        Else
            RadButtonNavigateBackwards.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Navigiert solange zurück bis UCO gefunden wurde
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub NavigiereZuUco(ByVal Status As GlobaleEnumeratoren.enuEichprozessStatus) Handles BreadCrumb.Navigieren
        Try
            'im debugger zur einfachheit, kann per click auf jeden Status gesprungen werden
            If Debugger.IsAttached Then
                SpringeZuUCO(Status)
            Else
                'prüfen ob zu hoher Status gewählt wurde
                If Status > CurrentEichprozess.FK_Vorgangsstatus Then
                    Exit Sub
                Else

                    'schnelles blättern im lese modus
                    If Not Status = _CurrentUco.EichprozessStatusReihenfolge Then
                        SpringeZuUCO(Status)
                    End If
                End If
            End If
        Catch e As Exception
        End Try
    End Sub

#Region "Localization"

    ''' <summary>
    ''' ändert die Kultur des Anwendungsthreads. Alle neuen Dialoge werden dann entsprechend in der neuen Sprache geladen
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <remarks></remarks>
    Friend Sub changeCulture(ByVal Code As String)
        Dim culture As CultureInfo = CultureInfo.GetCultureInfo(Code)

        Threading.Thread.CurrentThread.CurrentUICulture = culture
        AktuellerBenutzer.Instance.AktuelleSprache = Code
        AktuellerBenutzer.SaveSettings()

        'speichern der aktuellen Eingaben ins Objekt
        RaiseEvent LokalisierungNeeded(_CurrentUco)


        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMainContainer))
        'übersetzung der Formular elemente von frmMainContainer

        Me.RadButtonNavigateBackwards.Text = resources.GetString("RadButtonNavigateBackwards.Text")
        Me.RadButtonNavigateForwards.Text = resources.GetString("RadButtonNavigateForwards.Text")

    End Sub

    ''' <summary>
    ''' zuweiseung der Sprache für den Hauptthread bei Klick auf die Flaggen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonChangeLanguage_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToGerman.Click, RadButtonChangeLanguageToEnglish.Click, RadButtonChangeLanguageToPolish.Click
        If sender.Equals(RadButtonChangeLanguageToEnglish) Then
            changeCulture("en")
        ElseIf sender.Equals(RadButtonChangeLanguageToGerman) Then
            changeCulture("de")
        ElseIf sender.Equals(RadButtonChangeLanguageToPolish) Then
            changeCulture("pl")
        End If
    End Sub

#End Region
#End Region

#Region "Formular Events"

    ''' <summary>
    ''' abfangen von Sondertasten. F1 für EFG Dialog, sofern anzeigbar, shift + enter für schnelles navigieren, strg + enter für schnelles zurück navigieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMainContainer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'F1 für EFG Dialog
        If e.KeyData.ToString.Equals("F1") Then
            If Not _CurrentUco Is Nothing Then
                If Not _CurrentUco.objEichprozess Is Nothing Then
                    Dim f As New frmEichfehlergrenzen(_CurrentUco.objEichprozess)
                    f.Show()
                End If
            End If
        End If


        'Shift Enter für schnelleres vorblättern
        If e.KeyCode = Keys.Return Then
            If e.Modifiers.HasFlag(Keys.Shift) Then
                BlaettereVorwaerts()
            End If
            'STRG Enter für schnelleres zurückblättern
            If e.Modifiers.HasFlag(Keys.Control) Then
                BlaettereRueckwaerts()
            End If

        End If
    End Sub


    ''' <summary>
    '''   Laderoutine
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMainContainer_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'frmMain Container nutzt entweder die logiken zum Blättern eines eichprozesses (sofern me.currenteichprozess) nicht nothing ist oder aber zeigt die Auswahlliste an, in der die eigenen Eichprozesse aufgelistet werden.


        'prüfen ob ein Vorgang vorliegt oder nicht
        If Me.CurrentEichprozess Is Nothing Then 'wenn kein vorgang vorliegt Auswahlliste anzeiegn

            'auswahl des Benutzers, führt auch zur Lizenzeingabe, falls noch kein Benutzer angelegt wurde
            Dim frmBenutzerauswahl As New FrmBenutzerauswahl
            If frmBenutzerauswahl.ShowDialog = Windows.Forms.DialogResult.OK Then
                LadeAuswahlListe()

                'aktuelle Sprache der Anwendung auf vorher gewählte Sprache setzen
                RuntimeLocalizer.ChangeCulture(Me, AktuellerBenutzer.Instance.AktuelleSprache)

                'Lokalisierung anstossen
                TriggerLokalisierung()
            Else
                Application.Exit()
                Me.Close()
            End If
        Else
            'laden des benötigten UCOs anhand status von me.currentEichprozess
            LadeEichprozessVorgangsUco()
        End If

        'Lokalisierung anstossen
        TriggerLokalisierung()
    End Sub

    ''' <summary>
    ''' button klick der flaggen simulieren, anhand von aktueller Sprache des Benutzerobjektes
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TriggerLokalisierung()
        'Lokalisierung aktualisieren
        Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
            Case Is = "en"
                RadButtonChangeLanguage_Click(RadButtonChangeLanguageToEnglish, Nothing)
            Case Is = "de"
                RadButtonChangeLanguage_Click(RadButtonChangeLanguageToGerman, Nothing)
            Case Is = "pl"
                RadButtonChangeLanguage_Click(RadButtonChangeLanguageToPolish, Nothing)
            Case Else
                RadButtonChangeLanguage_Click(RadButtonChangeLanguageToEnglish, Nothing)
        End Select
    End Sub

    ''' <summary>
    ''' lädt die Auswahliste, da noch kein Eichprozess gewählt wurde. Hier können Eichungen auch gelöscht werden etc.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub LadeAuswahlListe()
        'laden des benötigten UCOs
        Dim uco As New ucoEichprozessauswahlliste(Me)
        _ListofUcos.Add(uco)
        ChangeActiveContentUserControl(uco)

        RadButtonNavigateBackwards.Visible = False
        RadButtonNavigateForwards.Visible = False


        'laden des Grid Layouts aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettings.ToString.Equals("") Then
                Using stream As New MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettings))
                    uco.RadGridViewAuswahlliste.LoadLayout(stream)
                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try

        'laden des RHEWA Grids aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettingsRhewa.ToString.Equals("") Then
                Using stream As New MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettingsRhewa))
                    uco.RadGridViewRHEWAAlle.LoadLayout(stream)
                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' lädt das benötigte Uco zum aktuellen Eichprozess Objektes (me.currenteichprozess)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub LadeEichprozessVorgangsUco()
        If Not Me.CurrentEichprozess Is Nothing Then
            'Breadcumb (Ampelfunktion intiieren)
            Try
                BreadCrumb = New ucoAmpel(Me)
                BreadCrumb.TabStop = False

                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls.Add(BreadCrumb)
                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls(0).Dock = DockStyle.Fill
                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls(0).Visible = True
            Catch ex As Exception
                Debug.WriteLine(ex.ToString)

            End Try

            Dim uco As Object = Nothing
            'aktuelen Status zur Ampel zuweisen

            If CurrentEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Fehlerhaft AndAlso Me.DialogModus <> enuDialogModus.lesend Then
                BreadCrumb.AktuellerGewaehlterVorgang = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                '      CurrentEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
            Else
                BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus

            End If
            If Me.DialogModus = enuDialogModus.lesend Then 'falls RHEWA seitig ein DS angeguckt wird, ist dieser bereits fertig, soll aber dennoch von anfang an angeguckt werden
                uco = New uco_2StammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                'auf erste seite Blättern
                BreadCrumb.FindeElementUndSelektiere(GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe)
            Else
                Select Case CurrentEichprozess.FK_Vorgangsstatus
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                        uco = New uco_2StammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                        uco = New uco_3Kompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                        uco = New uco_4KompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung '"  Beschaffenheitsprüfung"
                        uco = New uco_5Beschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                        uco = New uco_6EichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                        uco = New uco_7EichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                        uco = New uco_8PruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        uco = New uco_9PruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        uco = New uco10PruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                        uco = New uco11PruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                        uco = New uco12PruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                        uco = New uco13PruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                        uco = New uco17PruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                        uco = New uco15PruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                        uco = New uco16Taraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        uco = New uco18Fallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                        uco = New uco19EichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                        uco = New uco20Reports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                        uco = New Uco21Versenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Else
                        Me.Close()
                        Exit Sub
                End Select
            End If


            'prüfen ob der Button zum entsperren eingeblendet werden soll
            If DialogModus = enuDialogModus.lesend Then
                'einblenden des entsperren Buttons wenn RHEWA Mitarbeiter

                If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz = True Then
                    RadButtonEntsperren.Visible = True
                End If

            End If

            'zur auflistung der UCOs hinzufügen
            _ListofUcos.Add(uco)
            RadButtonNavigateBackwards.Enabled = True
            RadButtonNavigateForwards.Enabled = True
            RadButtonNavigateBackwards.Visible = True
            RadButtonNavigateForwards.Visible = True

            'anzeigen des neuen UCOs
            ChangeActiveContentUserControl(uco)
        End If
    End Sub

    ''' <summary>
    ''' funktion welches für die das AmpelUco genutzt wird, um von einem Eichprozess Status auf den anderen zu springen
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub SpringeZuUCO(ByVal Status As GlobaleEnumeratoren.enuEichprozessStatus)
        If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
            'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
            RaiseEvent SaveWithoutValidationNeeded(_CurrentUco)
        End If

        If Not _CurrentUco Is Nothing Then

            'beim springen werden weder previous noch nextuco gesetzt. D.h. es werden ucos mehrmals geladen, falls benötigt. Sonst funktioneirt das mit dem Vor und zurück blättern aber nicht
            'der grund dafür ist der folgende: ich fange in Stammdateneingabe an, springe auf Versenden. Klicke nun auf zurück und uco Versenden hat als previous UCO UcoStammdaten. das ist was der Benutzer erwartet
            Dim uco As Object = Nothing
            '    BreadCrumb.AktuellerGewaehlterVorgang = SpringeZuVorgangsstatus
            Select Case Status
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                    uco = New uco_2StammdatenEingabe(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                    uco = New uco_3Kompatiblititaetsnachweis(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                    uco = New uco_4KompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                    uco = New uco_5Beschaffenheitspruefung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                    uco = New uco_6EichprotokollVerfahrenswahl(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                    uco = New uco_7EichprotokollDaten(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                    uco = New uco_8PruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                    uco = New uco_9PruefungLinearitaet(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                    uco = New uco10PruefungStaffelverfahren(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                    uco = New uco11PruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                    uco = New uco12PruefungUeberlastanzeige(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                    uco = New uco13PruefungRollendeLasten(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                    uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                    uco = New uco17PruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    uco = New uco15PruefungStabilitaet(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    uco = New uco16Taraeinrichtung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                    uco = New uco18Fallbeschleunigung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                    uco = New uco19EichtechnischeSicherung(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                    uco = New uco20Reports(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    uco = New Uco21Versenden(Me, CurrentEichprozess, Nothing, Nothing, DialogModus)
                Case Else 'sonderfall der nicht eintreten sollte
                    Me.Close()
                    Exit Sub
            End Select

            If Not _ListofUcos.Contains(uco) Then
                _ListofUcos.Add(uco)

            End If
            '_CurrentUco.NextUco = uco
            'neues UCO in vordergrund bringen
            ChangeActiveContentUserControl(uco)

        End If
    End Sub




    ''' <summary>
    ''' Navigiere  vorwärts inklusive speichern und laden des neuen UCOs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadButtonNavigateForwards_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNavigateForwards.Click
        BlaettereVorwaerts()
    End Sub

    ''' <summary>
    ''' Navigiere rückwärts inklusive speichern und laden des UCOs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadButtonNavigateBackwards_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNavigateBackwards.Click
        BlaettereRueckwaerts()
    End Sub

    ''' <summary>
    ''' Zeige nächsten Dialog ein Eichprozess Reihenfolge an
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub BlaettereVorwaerts()
        'abbruch
        If _CurrentUco Is Nothing Then Exit Sub
        If _CurrentUco.objEichprozess Is Nothing Then Exit Sub
        If Me.RadButtonNavigateForwards.Enabled = False Then Exit Sub

        'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
        Dim bolUCODirty As Boolean
        bolUCODirty = _CurrentUco.AktuellerStatusDirty 'im Save Needed wird der Dirty Flag bereits wieder zurückgesetzt. Deswegen wird er hier zwischengespeichert
        RaiseEvent SaveNeeded(_CurrentUco)





        'abbruch des Vorgangs,wenn die validierung einen fehler erzeugt hat
        If _CurrentUco.AbortSaveing = True Then Exit Sub
        If _CurrentUco.NextUco Is Nothing Then

            Dim uco As Object = Nothing

            'sonderfälle abprüfen in denen der Vorgang nicht wie gewohnt weiter geht
            'TODO Abändern der Routine. Vorwärts Blättern setzt die Reihenfolge eines hoch je nach Status, Rückwärts blättern prüft im Status welcher Eichprozesswert vorliegt und lädt dann ein anderes UCO. Sollte angepasst werden
            If _CurrentUco.EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                Try
                    Select Case _CurrentUco.objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                        Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                            'überspringe Prüfung mit Normallast
                            _CurrentUco.EichprozessStatusReihenfolge += 1
                            If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
                                BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                            End If
                    End Select
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                End Try
            ElseIf _CurrentUco.EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet Then
                Try
                    'überspringe staffelverfahren
                    _CurrentUco.EichprozessStatusReihenfolge += 1
                    If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
                        BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                    End If
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                End Try
            ElseIf _CurrentUco.EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige Then
                Try
                    Select Case _CurrentUco.objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                        Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                            'überspringe fahrzeugwaagen
                            _CurrentUco.EichprozessStatusReihenfolge += 1
                            If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
                                BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                            End If
                    End Select
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                End Try
            ElseIf _CurrentUco.EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                Try
                    If _CurrentUco.objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                        'überspringe Stablität der GLeichgewichtslage
                        _CurrentUco.EichprozessStatusReihenfolge += 1
                        If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
                            BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                        End If
                    End If

                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                End Try
            ElseIf _CurrentUco.EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung Then
                Try
                    Select Case _CurrentUco.objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                        Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                            'überspringe Achlastwägungen
                            _CurrentUco.EichprozessStatusReihenfolge += 1
                            If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
                                BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                            End If
                    End Select
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)

                End Try
            End If

            If bolUCODirty Then
                BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
            Else
                'damit die Auswahl weiter geht
                If BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge Then
                    BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge + 1
                Else
                    BreadCrumb.FindeElementUndSelektiere(_CurrentUco.EichprozessStatusReihenfolge + 1)
                End If

            End If

            Select Case _CurrentUco.EichprozessStatusReihenfolge + 1
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                    uco = New uco_2StammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                    uco = New uco_3Kompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                    uco = New uco_4KompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                    uco = New uco_5Beschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                    uco = New uco_6EichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                    uco = New uco_7EichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                    uco = New uco_8PruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                    uco = New uco_9PruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                    uco = New uco10PruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                    uco = New uco11PruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                    uco = New uco12PruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                    uco = New uco13PruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                    uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                    uco = New uco17PruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    uco = New uco15PruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    uco = New uco16Taraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                    uco = New uco18Fallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                    uco = New uco19EichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                    uco = New uco20Reports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    uco = New Uco21Versenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Else 'sonderfall der nicht eintreten sollte
                    Me.Close()
                    Exit Sub
            End Select

            _ListofUcos.Add(uco)
            _CurrentUco.NextUco = uco
            'neues UCO in vordergrund bringen
            ChangeActiveContentUserControl(uco)
        Else

            If bolUCODirty Then
                BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.NextUco.EichprozessStatusReihenfolge
            Else
                'damit die Auswahl weiter geht
                If BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge Then
                    BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.NextUco.EichprozessStatusReihenfolge
                Else
                    BreadCrumb.FindeElementUndSelektiere(_CurrentUco.NextUco.EichprozessStatusReihenfolge)
                End If
            End If


            '.CurrentEichprozess.FK_Vorgangsstatus
            ChangeActiveContentUserControl(_CurrentUco.NextUco)
            'ermöglichen das das UCO noch einmal die Daten aktualisiert (z.b. wenn in den Stammdaten die Art der waage geändert wurde, muss der Kompatiblitätsnachweis darauf reagieren können
            RaiseEvent UpdateNeeded(_CurrentUco)
        End If



        'sonderfall für Standardwaagen. In diesem Fall werden bestimmte Dialoge beim vorwärts blättern übersprungen
        If CurrentEichprozess.AusStandardwaageErzeugt = True Then
            Select Case _CurrentUco.EichprozessStatusReihenfolge
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                    Me.RadButtonNavigateForwards.PerformClick()
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                    Me.RadButtonNavigateForwards.PerformClick()
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                    Me.RadButtonNavigateForwards.PerformClick()
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                    Me.RadButtonNavigateForwards.PerformClick()
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                    Me.RadButtonNavigateForwards.PerformClick()
            End Select
        End If
    End Sub


    ''' <summary>
    ''' Zeige vorherigen Dialog ein Eichprozess Reihenfolge an
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub BlaettereRueckwaerts()
        If _CurrentUco Is Nothing Then Exit Sub
        If _CurrentUco.objEichprozess Is Nothing Then Exit Sub
        If Me.RadButtonNavigateBackwards.Enabled = False Then Exit Sub

        Dim uco As ucoContent = Nothing


        'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
        RaiseEvent SaveWithoutValidationNeeded(_CurrentUco)


        'prüfen ob das vorherige UCO bereits geladen wurde, ansonsten erzeugen
        If _CurrentUco.PreviousUco Is Nothing Then

            'das vorherige UCO msus in abhängigkeit das aktuellen UCOs und nicht des Eichprozesses geladen werden, da dieser ja in einem viel weiteren Status sein kann
            'rückwärts navigieren
            BreadCrumb.FindeElementUndSelektiere(_CurrentUco.EichprozessStatusReihenfolge - 1)
            Select Case _CurrentUco.EichprozessStatusReihenfolge
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                    'hier gibt es kein vorheriges UCO
                    RadButtonNavigateBackwards.Enabled = False 'solte bereits deaktivert sein, so das es nie zu diesem code kommt
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                    uco = New uco_2StammdatenEingabe(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    _CurrentUco.PreviousUco = uco
                    RadButtonNavigateBackwards.Enabled = False
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                    uco = New uco_3Kompatiblititaetsnachweis(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung '"  Beschaffenheitsprüfung"
                    uco = New uco_4KompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren '"  Beschaffenheitsprüfung"
                    uco = New uco_5Beschaffenheitspruefung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten '"  Beschaffenheitsprüfung"
                    uco = New uco_6EichprotokollVerfahrenswahl(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                    uco = New uco_7EichprotokollDaten(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                    uco = New uco_8PruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                    uco = New uco_8PruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                    'je nach verfahren
                    'normallast oder ersatzlast
                    If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien Then
                        uco = New uco_9PruefungLinearitaet(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Else
                        uco = New uco10PruefungStaffelverfahren(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    End If
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                    uco = New uco11PruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                    uco = New uco11PruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)

                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                    'je nach verfahren rollende Lasten oder Überlastanzeige
                    If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen Then
                        uco = New uco13PruefungRollendeLasten(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Else
                        uco = New uco12PruefungUeberlastanzeige(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    End If
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                    uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    'je nach verfahren Eignung für Achlastwägungen oder Prüfung des Ansprechvermögens
                    If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen Then
                        uco = New uco17PruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Else
                        uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    End If

                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    'je nachdem ob ein Drucker gewählt wurde oder nicht
                    If CurrentEichprozess.Eichprotokoll.Verwendungszweck_Drucker = True Then
                        uco = New uco15PruefungStabilitaet(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Else
                        uco = New uco14PruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    End If

                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                    uco = New uco16Taraeinrichtung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                    uco = New uco18Fallbeschleunigung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                    uco = New uco19EichtechnischeSicherung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    uco = New uco20Reports(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)



                Case Else

                    Me.Close()
                    Exit Sub
            End Select

            uco.NextUco = _CurrentUco
            _CurrentUco.PreviousUco = uco
            _ListofUcos.Add(uco)
        Else
            BreadCrumb.FindeElementUndSelektiere(_CurrentUco.PreviousUco.EichprozessStatusReihenfolge)
            uco = _CurrentUco.PreviousUco
        End If

        'vorheriges Uco zu anzeige bringen
        ChangeActiveContentUserControl(uco)
        RaiseEvent UpdateNeeded(uco)
    End Sub

    ''' <summary>
    ''' In dem Event wird die maximale Größe des Hilfetextpanels angepasst, damit nur ein vertikaler Scrollbalken angezeigt wird und kein horizontaler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub SplitPanelContextHelpr_Resize(sender As Object, e As System.EventArgs) Handles SplitPanelContextHelp.Resize
        'TH größe des Labels auf die Breite des Panels legen. Der manuelle Weg wurde gewählt, so das nur ein Horizontaler SCrollbalken entsteht und kein Vertikaler
        RadLabelContextHelp.MaximumSize = New Size(RadScrollablePanelContextHelp.Size.Width - 30, 0)
    End Sub

    ''' <summary>
    ''' in diesem Event wird gesperrt status zurückgesetzt, falls ein Bearbeiter gerade sperrt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMainContainer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogModus = enuDialogModus.korrigierend Then
            If Not CurrentEichprozess Is Nothing Then
                clsWebserviceFunctions.SetzeSperrung(False, CurrentEichprozess.Vorgangsnummer)
            End If

        End If
        If Not AktuellerBenutzer.Instance Is Nothing And Not Me._CurrentUco Is Nothing Then
            SpeichereGridLayout()

        End If
    End Sub


#End Region

    ''' <summary>
    ''' Speichert Gridlayout als XML Stream, welcher in DB zum aktuellen Benutzer gespeichert wird
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SpeichereGridLayout()
        'speichere Layout der beiden Grids
        If Me._CurrentUco.GetType Is GetType(ucoEichprozessauswahlliste) Then
            Dim gridProzesse = CType(Me._CurrentUco, ucoEichprozessauswahlliste).RadGridViewAuswahlliste
            Dim gridProzesseRHEWA = CType(Me._CurrentUco, ucoEichprozessauswahlliste).RadGridViewRHEWAAlle
            Try
                Using stream As New MemoryStream()
                    gridProzesse.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)
                        AktuellerBenutzer.Instance.GridSettings = Convert.ToBase64String(buffer)
                        AktuellerBenutzer.SaveSettings()
                    End If
                End Using
            Catch ex As Exception

            End Try

            Try
                Using stream As New MemoryStream()
                    gridProzesseRHEWA.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)
                        AktuellerBenutzer.Instance.GridSettingsRhewa = Convert.ToBase64String(buffer)
                        AktuellerBenutzer.SaveSettings()
                    End If
                End Using
            Catch ex As Exception
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Triggered Event im UCO, welches den Versendenprozess startet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonVersenden_Click(sender As Object, e As EventArgs) Handles RadButtonVersenden.Click
        If MessageBox.Show(GlobaleLokalisierung.Frage_EichprotokollZuruecksenden, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

            'entsperren des DS
            If Not CurrentEichprozess Is Nothing Then
                If clsWebserviceFunctions.SetzeSperrung(False, CurrentEichprozess.Vorgangsnummer) Then
                    RaiseEvent VersendenNeeded(_CurrentUco)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' triggerd event im UCO, welches den Entsperrprozess startet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonEntsperren_Click(sender As Object, e As EventArgs) Handles RadButtonEntsperren.Click
        'wenn der status des aktuellen elementes eh schon auf fehlerhaft steht oder auf abgeschlossen, darf keine Änderung verschickt werden
        If CurrentEichprozess.FK_Bearbeitungsstatus = 3 Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_EichprotokollBereitsGenehmigt)
            Exit Sub
        End If

        If CurrentEichprozess.FK_Bearbeitungsstatus = 2 Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_EichprotokollBereitsAbgelehnt)
            Exit Sub
        End If

        'prüfen ob eine Sperrung des DS vorliegt und DS sperren wenn nicht
        If Not CurrentEichprozess Is Nothing Then
            If clsWebserviceFunctions.SetzeSperrung(True, CurrentEichprozess.Vorgangsnummer) Then
                RaiseEvent EntsperrungNeeded()
                RadButtonVersenden.Visible = True
                RadButtonEntsperren.Enabled = False
            End If
        End If

    End Sub


    ''' <summary>
    ''' Eventhandler für UCO Event. Wenn dort eine Eigenschaft geändert wird, wird ein Dirty Flag gesetzt. Durch das Dirty flag wird hier der Status des aktuellen Vorgangs angepasst.
    ''' Beispiel: Der Eichprozess befindet sich im 10. Schritt. im 8 wird aber etwas geändert. Dies kann folgen haben für die kommenden Schritte. Deswegen wird dann der aktuelle Schritt auf 8 zurückgesetzt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub _CurrentUco_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _CurrentUco.PropertyChanged
        If e.PropertyName.Equals("AktuellerStatusDirty") Then
            BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge
        End If
    End Sub
End Class
