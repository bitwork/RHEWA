Imports System.Resources
Imports System.Globalization
Imports EichsoftwareClient.My.Resources
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Word
Imports System.IO


Public Class FrmMainContainer
#Region "Membervariables"
    Private ListofUcos As New List(Of ucoContent)
    ''' <summary>
    ''' Das aktuelle UserControl welches im Inhaltsfenster angezeigt wird
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private WithEvents _CurrentUco As ucoContent

    Enum enuDialogModus
        normal = 0
        lesend = 1
        korrigierend = 2
    End Enum

    Private WithEvents BreadCrumb As ucoNewStatustlist
#End Region


#Region "Constructor"
    Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        Telerik.WinControls.ThemeResolutionService.LoadPackageResource("EichsoftwareClient.RHEWAGREEN.tssp") 'Pfad zur Themedatei
        Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "RHEWAGREEN" 'standard Themename



        'aktuelle Sprache der Anwendung auf vorher gewählte Sprache setzen
        RuntimeLocalizer.ChangeCulture(Me, My.Settings.AktuelleSprache)
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


    Public Property CurrentEichprozess As Eichprozess

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
        'Text in HTML Formatieren
        '  Dim provider As New Telerik.WinControls.RichTextBox.FileFormats.Html.HtmlFormatProvider
        'Formatierten Text dem Steuerelement zuweisen
        RadLabelContextHelp.Text = Helptext 'provider.Import(Helptext)

        'TH größe des Labels auf die Breite des Panels legen. Der manuelle Weg wurde gewählt, so das nur ein Horizontaler SCrollbalken entsteht und kein Vertikaler
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
        If ListofUcos.Contains(uco) Then
            _CurrentUco = uco

            If Not Me.SplitPanelContent1.Controls.Contains(_CurrentUco) Then
                Me.SplitPanelContent1.Controls.Add(_CurrentUco)
            End If

            _CurrentUco.BringToFront()
            _CurrentUco.Dock = DockStyle.Fill
        End If
        If _CurrentUco.PreviousUco Is Nothing Then
            If Not CurrentEichprozess Is Nothing Then
                Select Case _CurrentUco.EichprozessStatusReihenfolge
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                        RadButtonNavigateBackwards.Enabled = False
                    Case Else
                        RadButtonNavigateBackwards.Enabled = True

                        'case nur lesend
                        'case korrektur
                End Select
            Else
                RadButtonNavigateBackwards.Enabled = False

            End If


        Else
            RadButtonNavigateBackwards.Enabled = True
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
            'prüfen ob zu hoher Status gewählt wurde
            If Status > CurrentEichprozess.FK_Vorgangsstatus Then
                Exit Sub
            Else
            
                'If _CurrentUco.DialogModus = ucoContent.enuDialogModus.lesend Then
                '    'schnelles blättern im lese modus
                If Not Status = _CurrentUco.EichprozessStatusReihenfolge Then
                    SpringeZuUCO(Status)

                End If
                'Else
                '    If Status > _CurrentUco.EichprozessStatusReihenfolge Then
                '        Do While Status > _CurrentUco.EichprozessStatusReihenfolge
                '            RadButtonNavigateForwards_Click(Nothing, Nothing)
                '        Loop
                '    ElseIf Status < _CurrentUco.EichprozessStatusReihenfolge Then
                '        Do While Status < _CurrentUco.EichprozessStatusReihenfolge
                '            RadButtonNavigateBackwards_Click(Nothing, Nothing)
                '        Loop
                '    Else
                '    Exit Sub
                '    End If
                'End If


                'If Status > _CurrentUco.EichprozessStatusReihenfolge Then
                '    Do While Status > _CurrentUco.EichprozessStatusReihenfolge
                '        RadButtonNavigateForwards_Click(Nothing, Nothing)
                '    Loop
                'ElseIf Status < _CurrentUco.EichprozessStatusReihenfolge Then
                '    Do While Status < _CurrentUco.EichprozessStatusReihenfolge
                '        RadButtonNavigateBackwards_Click(Nothing, Nothing)
                '    Loop
                'Else
                '    Exit Sub 'weil gleich
                'End If

            End If
        Catch e As Exception
        End Try
    End Sub

#Region "Localization"
    Private Sub changeCulture(ByVal Code As String)
        Dim culture As CultureInfo = CultureInfo.GetCultureInfo(Code)

        Threading.Thread.CurrentThread.CurrentUICulture = culture
        My.Settings.AktuelleSprache = Code
        My.Settings.Save()

        'speichern der aktuellen Eingaben ins Objekt
        RaiseEvent LokalisierungNeeded(_CurrentUco)


        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMainContainer))
        'übersetzung der Formular elemente von frmMainContainer

        Me.RadButtonNavigateBackwards.Text = resources.GetString("RadButtonNavigateBackwards.Text")
        Me.RadButtonNavigateForwards.Text = resources.GetString("RadButtonNavigateForwards.Text")

    End Sub
    Private Sub RadButtonChangeLanguageToGerman_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToGerman.Click
        ' RuntimeLocalizer.ChangeCulture(Me, "de")
        '       RaiseEvent LokalisierungNeeded(_CurrentUco)
        changeCulture("de")

    End Sub

    Private Sub RadButtonChangeLanguageToEnglish_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToEnglish.Click
        'RuntimeLocalizer.ChangeCulture(Me, "en")
        'RaiseEvent LokalisierungNeeded(_CurrentUco)
        changeCulture("en")
    End Sub

    Private Sub RadButtonChangeLanguageToPolish_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToPolish.Click
        'RuntimeLocalizer.ChangeCulture(Me, "pl")
        'RaiseEvent LokalisierungNeeded(_CurrentUco)
        changeCulture("pl")
    End Sub
#End Region
#End Region

#Region "Formular Events"


    Private Sub FrmMainContainer_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'laden der Auswahliste
        'prüfen ob ein Vorgang vorliegt oder nicht
        If Me.CurrentEichprozess Is Nothing Then 'wenn kein vorgang vorliegt Auswahlliste anzeiegn
            'laden des benötigten UCOs
            Dim uco As New ucoEichprozessauswahlliste(Me)
            ListofUcos.Add(uco)
            ChangeActiveContentUserControl(uco)

            RadButtonNavigateBackwards.Visible = False
            RadButtonNavigateForwards.Visible = False




            'prüfen ob die Lizenz gültig ist
            Using DBContext As New EichsoftwareClientdatabaseEntities1
                Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault
                If objLiz Is Nothing Then
                    My.Settings.Lizensiert = False
                    My.Settings.Save()
                End If
            End Using



            'wenn keine Lizenz vorhanden ist, zur Eingabe auffordern
            If My.Settings.Lizensiert = False Then
                Dim f As New FrmLizenz
                If f.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If My.Settings.Lizensiert = False Then
                        System.Windows.Forms.Application.Exit()
                        Exit Sub
                    Else
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.BitteNeuStarten, "", MessageBoxButtons.OK)
                        System.Windows.Forms.Application.Exit()
                        Exit Sub
                    End If
                Else
                    System.Windows.Forms.Application.Exit()
                    Exit Sub
                End If
            End If

            'laden des Grid Layouts
            Try
                Using stream As New MemoryStream(Convert.FromBase64String(My.Settings.GridSettings))
                    uco.RadGridViewAuswahlliste.LoadLayout(stream)
                End Using
            Catch ex As Exception
                'konnte layout nicht finden
            End Try
            Try
                Using stream As New MemoryStream(Convert.FromBase64String(My.Settings.GridSettingsRHEWA))
                    uco.RadGridViewRHEWAAlle.LoadLayout(stream)
                End Using
            Catch ex As Exception
                'konnte layout nicht finden
            End Try
         

         


        Else
            'laden des benötigten UCOs
            Try
                BreadCrumb = New ucoNewStatustlist(Me)
                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls.Add(BreadCrumb)
                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls(0).Dock = DockStyle.Fill
                Me.RadScrollablePanelTrafficLightBreadcrumb.PanelContainer.Controls(0).Visible = True
            Catch ex As Exception
            End Try


            Dim uco As Object = Nothing
            BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus

            If Me.DialogModus = enuDialogModus.lesend Then 'falls RHEWA seitig ein DS angeguckt wird, ist dieser bereits fertig, soll aber dennoch von anfang an angeguckt werden
                ' CurrentEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
            Else
                Select Case CurrentEichprozess.FK_Vorgangsstatus
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                        uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                        uco = New ucoKompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                        uco = New ucoKompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung '"  Beschaffenheitsprüfung"
                        uco = New ucoBeschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                        uco = New ucoEichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                        uco = New ucoEichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                        uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        uco = New ucoPruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        uco = New ucoPruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                        uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                        uco = New ucoPruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                        uco = New ucoPruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                        uco = New ucoPruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                        uco = New ucoPruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                        uco = New ucoTaraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        uco = New ucoFallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                        uco = New ucoEichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                        uco = New ucoReports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                        uco = New UcoVersenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Else
                        Me.Close()
                        Exit Sub
                End Select
            End If


            'prüfen ob der Button zum entsperren eingeblendet werden soll
            If DialogModus = enuDialogModus.lesend Then
                'einblenden des entsperren Buttons wenn RHEWA Mitarbeiter
                Using dbcontext As New EichsoftwareClientdatabaseEntities1
                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                    If objLiz.RHEWALizenz = True Then
                        RadButtonEntsperren.Visible = True
                    End If
                End Using
            End If

           
            ListofUcos.Add(uco)
            RadButtonNavigateBackwards.Enabled = True
            RadButtonNavigateForwards.Enabled = True
            RadButtonNavigateBackwards.Visible = True
            RadButtonNavigateForwards.Visible = True

            ChangeActiveContentUserControl(uco)
        End If

        'sprache
        Select Case My.Settings.AktuelleSprache.ToLower
            Case Is = "en"
                RadButtonChangeLanguageToEnglish_Click(Nothing, Nothing)
            Case Is = "de"
                RadButtonChangeLanguageToGerman_Click(Nothing, Nothing)
            Case Is = "pl"
                RadButtonChangeLanguageToPolish_Click(Nothing, Nothing)
            Case Else
                RadButtonChangeLanguageToEnglish_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub SpringeZuUCO(ByVal Status As GlobaleEnumeratoren.enuEichprozessStatus)
        If _CurrentUco.DialogModus <> ucoContent.enuDialogModus.lesend Then
            'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
            RaiseEvent SaveWithoutValidationNeeded(_CurrentUco)
        End If

        If Not _CurrentUco Is Nothing Then

            Dim uco As Object = Nothing
            '    BreadCrumb.AktuellerGewaehlterVorgang = SpringeZuVorgangsstatus
            Select Case Status
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                    uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                    uco = New ucoKompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                    uco = New ucoKompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                    uco = New ucoBeschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                    uco = New ucoEichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                    uco = New ucoEichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                    uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                    uco = New ucoPruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                    uco = New ucoPruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                    uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                    uco = New ucoPruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                    uco = New ucoPruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                    uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                    uco = New ucoPruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    uco = New ucoPruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    uco = New ucoTaraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                    uco = New ucoFallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                    uco = New ucoEichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                    uco = New ucoReports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                    uco = New UcoVersenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                Case Else 'sonderfall der nicht eintreten sollte
                    Me.Close()
                    Exit Sub
            End Select

            If Not ListofUcos.Contains(uco) Then
                ListofUcos.Add(uco)

            End If
            _CurrentUco.NextUco = uco
            'neues UCO in vordergrund bringen
            ChangeActiveContentUserControl(uco)

        End If
    End Sub

    Private Sub BlaettereVorwaerts()
        'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen

        RaiseEvent SaveNeeded(_CurrentUco)
        BreadCrumb.StatusDesAktuellGewaehltenVorgangs = ucoNewStatustlist.enuImage.gelb


        If Not _CurrentUco Is Nothing Then
            'abbruch des Vorgangs,wenn die validierung einen fehler erzeugt hat
            If _CurrentUco.AbortSaveing = True Then Exit Sub
            If _CurrentUco.NextUco Is Nothing Then

                Dim uco As Object = Nothing
                BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus
                Select Case _CurrentUco.EichprozessStatusReihenfolge + 1
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                        uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                        uco = New ucoKompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                        uco = New ucoKompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                        uco = New ucoBeschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
                        uco = New ucoEichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                        uco = New ucoEichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                        uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        uco = New ucoPruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        uco = New ucoPruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                        uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                        uco = New ucoPruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                        uco = New ucoPruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                        uco = New ucoPruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                        uco = New ucoPruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                        uco = New ucoTaraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        uco = New ucoFallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                        uco = New ucoEichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                        uco = New ucoReports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                        uco = New UcoVersenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
                    Case Else 'sonderfall der nicht eintreten sollte
                        Me.Close()
                        Exit Sub
                End Select

                ListofUcos.Add(uco)
                _CurrentUco.NextUco = uco
                'neues UCO in vordergrund bringen
                ChangeActiveContentUserControl(uco)
            Else
                BreadCrumb.FindeElementUndSelektiere(_CurrentUco.NextUco.EichprozessStatusReihenfolge) '.CurrentEichprozess.FK_Vorgangsstatus
                ChangeActiveContentUserControl(_CurrentUco.NextUco)
                'ermöglichen das das UCO noch einmal die Daten aktualisiert (z.b. wenn in den Stammdaten die Art der waage geändert wurde, muss der Kompatiblitätsnachweis darauf reagieren können
                RaiseEvent UpdateNeeded(_CurrentUco)
            End If
        End If


        'If _CurrentUco Is Nothing Then

        '    Dim uco As Object = Nothing
        '    'navigationsleiste anpassen
        '    BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus

        '    Select Case CurrentEichprozess.FK_Vorgangsstatus
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
        '            uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
        '            uco = New ucoKompatiblititaetsnachweis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
        '            uco = New ucoKompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung '"  Beschaffenheitsprüfung"
        '            uco = New ucoBeschaffenheitspruefung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
        '            uco = New ucoEichprotokollVerfahrenswahl(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
        '            uco = New ucoEichprotokollDaten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
        '            uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
        '            uco = New ucoPruefungLinearitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
        '            uco = New ucoPruefungStaffelverfahren(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
        '            uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
        '            uco = New ucoPruefungUeberlastanzeige(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
        '            uco = New ucoPruefungRollendeLasten(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
        '            uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
        '            uco = New ucoPruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
        '            uco = New ucoPruefungStabilitaet(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
        '            uco = New ucoTaraeinrichtung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
        '            uco = New ucoFallbeschleunigung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
        '            uco = New ucoEichtechnischeSicherung(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
        '            uco = New ucoReports(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)
        '        Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
        '            uco = New UcoVersenden(Me, CurrentEichprozess, _CurrentUco, Nothing, DialogModus)

        '        Case Else

        '            Me.Close()
        '            Exit Sub
        '    End Select


        '    ListofUcos.Add(uco)
        '    'neues UCO in vordergrund bringen
        '    ChangeActiveContentUserControl(uco)
        'Else
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

    Private Sub RadButtonNavigateBackwards_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNavigateBackwards.Click
        BlaettereRueckwaerts()
    End Sub

    Private Sub BlaettereRueckwaerts()
        If Not _CurrentUco Is Nothing Then
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
                        uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        _CurrentUco.PreviousUco = uco
                        RadButtonNavigateBackwards.Enabled = False
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                        uco = New ucoKompatiblititaetsnachweis(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung '"  Beschaffenheitsprüfung"
                        uco = New ucoKompatiblititaetsnachweisErgebnis(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren '"  Beschaffenheitsprüfung"
                        uco = New ucoBeschaffenheitspruefung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten '"  Beschaffenheitsprüfung"
                        uco = New ucoEichprotokollVerfahrenswahl(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                        uco = New ucoEichprotokollDaten(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        uco = New ucoPruefungNullstellungUndAussermittigeBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                        'je nach verfahren
                        'normallast oder ersatzlast
                        If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien Then
                            uco = New ucoPruefungLinearitaet(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        Else
                            uco = New ucoPruefungStaffelverfahren(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        End If
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                        uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
                        uco = New ucoPruefungWiederholbarkeitBelastung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)

                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        'je nach verfahren rollende Lasten oder Überlastanzeige
                        If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen Then
                            uco = New ucoPruefungRollendeLasten(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        Else
                            uco = New ucoPruefungUeberlastanzeige(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        End If
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                        uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                        'je nach verfahren Eignung für Achlastwägungen oder Prüfung des Ansprechvermögens
                        If CurrentEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen Then
                            uco = New ucoPruefungEignungFuerAchlastwaegungen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        Else
                            uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        End If

                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                        'je nachdem ob ein Drucker gewählt wurde oder nicht
                        If CurrentEichprozess.Eichprotokoll.Verwendungszweck_Drucker = True Then
                            uco = New ucoPruefungStabilitaet(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        Else
                            uco = New ucoPruefungAnsprechvermoegen(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        End If

                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        uco = New ucoTaraeinrichtung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
                        uco = New ucoFallbeschleunigung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Export
                        uco = New ucoEichtechnischeSicherung(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                        uco = New ucoReports(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)



                    Case Else

                        Me.Close()
                        Exit Sub
                End Select

                uco.NextUco = _CurrentUco
                _CurrentUco.PreviousUco = uco
                ListofUcos.Add(uco)
            Else
                BreadCrumb.FindeElementUndSelektiere(_CurrentUco.PreviousUco.EichprozessStatusReihenfolge)
                uco = _CurrentUco.PreviousUco
            End If

            'vorheriges Uco zu anzeige bringen
            ChangeActiveContentUserControl(uco)
            RaiseEvent UpdateNeeded(uco)
        End If
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
            SetzeSperrung(False)
        End If

        'speichere Layout der beiden Grids
        If Me._CurrentUco.GetType Is GetType(ucoEichprozessauswahlliste) Then

            Dim gridProzesse = CType(Me._CurrentUco, ucoEichprozessauswahlliste).RadGridViewAuswahlliste
            Dim gridProzesseRHEWA = CType(Me._CurrentUco, ucoEichprozessauswahlliste).RadGridViewRHEWAAlle

            Using stream As New MemoryStream()
                gridProzesse.SaveLayout(stream)
                stream.Position = 0
                Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                stream.Read(buffer, 0, buffer.Length)
                My.Settings.GridSettings = Convert.ToBase64String(buffer)
                My.Settings.Save()
            End Using

            Using stream As New MemoryStream()
                gridProzesseRHEWA.SaveLayout(stream)
                stream.Position = 0
                Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                stream.Read(buffer, 0, buffer.Length)
                My.Settings.GridSettingsRHEWA = Convert.ToBase64String(buffer)
                My.Settings.Save()
            End Using
        End If
    End Sub
#End Region



    Private Sub RadButtonVersenden_Click(sender As Object, e As EventArgs) Handles RadButtonVersenden.Click
        If MessageBox.Show(GlobaleLokalisierung.Frage_EichprotokollZuruecksenden, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

            'entsperren des DS
            If SetzeSperrung(False) Then
                RaiseEvent VersendenNeeded(_CurrentUco)
            End If
        End If
    End Sub

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
        If SetzeSperrung(True) Then
            RaiseEvent EntsperrungNeeded()
            RadButtonVersenden.Visible = True
            RadButtonEntsperren.Enabled = False
        End If

    End Sub

    Private Function SetzeSperrung(ByVal bolSperren As Boolean) As Boolean
        'neue Datenbankverbindung
        Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                webContext.Open()
            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
            Using dbcontext As New EichsoftwareClientdatabaseEntities1

                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                'prüfen ob der datensatz von jemand anderem in Bearbeitung ist
                Dim bolSetSperrung As Boolean = True 'variable zum abbrechen des Prozesses, falls jemand anderes an dem DS arbeitet
                Dim Messagetext As String = ""
                Messagetext = webContext.CheckSperrung(objLiz.HEKennung, objLiz.Lizenzschluessel, CurrentEichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                If Messagetext.Equals("") = False Then
                    'rhewa arbeitet in deutsch und hat keine lokalisierung gewünscht
                    If MessageBox.Show("Dieser Eichprozess wird von '" & Messagetext & "' bearbeitet. Möchten Sie seine Arbeit wirklich überschreiben und den Prozess selbst bearbeiten?", My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        bolSetSperrung = True
                    Else
                        bolSetSperrung = False
                    End If
                End If

                If bolSetSperrung Then
                    Dim result As String
                    result = webContext.SetSperrung(bolSperren, objLiz.HEKennung, objLiz.Lizenzschluessel, CurrentEichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                    If result = "" Then
                        Return True
                    Else
                        MessageBox.Show(result)
                        Return False
                    End If
                Else
                    Return False
                End If
            End Using
        End Using
    End Function

    Private Sub _CurrentUco_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _CurrentUco.PropertyChanged
        If e.PropertyName.Equals("AktuellerStatusDirty") Then
            BreadCrumb.AktuellerGewaehlterVorgang = _CurrentUco.EichprozessStatusReihenfolge
        End If
    End Sub
End Class
