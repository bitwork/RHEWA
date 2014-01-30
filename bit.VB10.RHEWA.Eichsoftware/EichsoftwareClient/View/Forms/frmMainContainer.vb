Imports System.Resources
Imports System.Globalization
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Word


Public Class FrmMainContainer
#Region "Membervariables"
    Private ListofUcos As New List(Of UserControl)
    ''' <summary>
    ''' Das aktuelle UserControl welches im Inhaltsfenster angezeigt wird
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private _CurrentUco As UserControl

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


    Public Property CurrentEichprozessID As Integer
    Public Property CurrentEichprozess As Eichprozess
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



    Private Sub ChangeActiveContentUserControl(ByVal uco As UserControl)
        If ListofUcos.Contains(uco) Then
            _CurrentUco = uco

            If Not Me.SplitPanelContent1.Controls.Contains(_CurrentUco) Then
                Me.SplitPanelContent1.Controls.Add(_CurrentUco)
            End If

            _CurrentUco.BringToFront()
            _CurrentUco.Dock = DockStyle.Fill
        End If
        If CType(_CurrentUco, ucoContent).PreviousUco Is Nothing Then
            If Not CurrentEichprozess Is Nothing Then
                Select Case CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge
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

    Private Sub NavigiereZuUco(ByVal Status As GlobaleEnumeratoren.enuEichprozessStatus) Handles BreadCrumb.Navigieren
        Try
            'prüfen ob zu hoher Status gewählt wurde
            If Status > CurrentEichprozess.FK_Vorgangsstatus Then
                Exit Sub
            Else
                If Status > CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge Then
                    Do While Status > CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge
                        RadButtonNavigateForwards_Click(Nothing, Nothing)
                    Loop
                ElseIf Status < CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge Then
                    Do While Status < CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge
                        RadButtonNavigateBackwards_Click(Nothing, Nothing)
                    Loop
                Else
                    Exit Sub 'weil gleich
                End If
              
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


            Select Case My.Settings.AktuelleSprache
                Case Is = "en"
                    RadButtonChangeLanguageToEnglish_Click(Nothing, Nothing)
                Case Is = "de"
                    RadButtonChangeLanguageToGerman_Click(Nothing, Nothing)
                Case Is = "pl"
                    RadButtonChangeLanguageToPolish_Click(Nothing, Nothing)
            End Select

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
                    MessageBox.Show("unbekannter Status")
                    Me.Close()
                    Exit Sub
            End Select

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

            ''breadcrumb erweitern
            'If Not BreadCrumb Is Nothing Then
            '    FlowLayoutPanelTrafficLightBreadcrumb.Controls.Add(BreadCrumb)
            'End If
            ListofUcos.Add(uco)
            RadButtonNavigateBackwards.Enabled = True
            RadButtonNavigateForwards.Enabled = True
            RadButtonNavigateBackwards.Visible = True
            RadButtonNavigateForwards.Visible = True


            ChangeActiveContentUserControl(uco)

        End If
    End Sub

    Private Sub RadButtonNavigateForwards_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNavigateForwards.Click
        'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
        RaiseEvent SaveNeeded(_CurrentUco)

        BreadCrumb.GETSETStatusDesAktuellGewaehltenVorgans = ucoNewStatustlist.enuImage.gelb
        If _CurrentUco Is Nothing Then

            Dim uco As Object = Nothing
            'navigationsleiste anpassen
            ' 
            BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus

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
                    MessageBox.Show("unbekannter Status")
                    Me.Close()
                    Exit Sub
            End Select


            ListofUcos.Add(uco)
            'neues UCO in vordergrund bringen
            ChangeActiveContentUserControl(uco)
        Else
            'abbruch des Vorgangs,wenn die validierung einen fehler erzeugt hat
            If CType(_CurrentUco, ucoContent).AbortSaveing = True Then Exit Sub
            If CType(_CurrentUco, ucoContent).NextUco Is Nothing Then

                Dim uco As Object = Nothing

                BreadCrumb.AktuellerGewaehlterVorgang = CurrentEichprozess.FK_Vorgangsstatus
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
                        MessageBox.Show("unbekannter Status")
                        Me.Close()
                        Exit Sub
                End Select



                ListofUcos.Add(uco)
                CType(_CurrentUco, ucoContent).NextUco = uco
                'neues UCO in vordergrund bringen
                ChangeActiveContentUserControl(uco)
            Else
                BreadCrumb.FindeElementUndSelektiere(CType(_CurrentUco, ucoContent).NextUco.EichprozessStatusReihenfolge) '.CurrentEichprozess.FK_Vorgangsstatus
                ChangeActiveContentUserControl(CType(_CurrentUco, ucoContent).NextUco)
                'ermöglichen das das UCO noch einmal die Daten aktualisiert (z.b. wenn in den Stammdaten die Art der waage geändert wurde, muss der Kompatiblitätsnachweis darauf reagieren können
                RaiseEvent UpdateNeeded(_CurrentUco)
            End If
        End If

        ''automatisch nach rechts scrollen
        'FlowLayoutPanelTrafficLightBreadcrumb.HorizontalScroll.Value = FlowLayoutPanelTrafficLightBreadcrumb.HorizontalScroll.Maximum

    End Sub

    Private Sub RadButtonNavigateBackwards_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNavigateBackwards.Click
        If Not _CurrentUco Is Nothing Then
            Dim uco As ucoContent = Nothing

            'TH Event abfeuern, damit steuerelemente bescheid wissen, das sie in DB speichern müssen
            RaiseEvent SaveWithoutValidationNeeded(_CurrentUco)
            'prüfen ob das vorherige UCO bereits geladen wurde, ansonsten erzeugen
            If CType(_CurrentUco, ucoContent).PreviousUco Is Nothing Then

                'das vorherige UCO msus in abhängigkeit das aktuellen UCOs und nicht des Eichprozesses geladen werden, da dieser ja in einem viel weiteren Status sein kann
                'rückwärts navigieren
                BreadCrumb.FindeElementUndSelektiere(CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge - 1)
                Select Case CType(_CurrentUco, ucoContent).EichprozessStatusReihenfolge
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                        'hier gibt es kein vorheriges UCO
                        RadButtonNavigateBackwards.Enabled = False 'solte bereits deaktivert sein, so das es nie zu diesem code kommt
                    Case Is = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                        uco = New ucoStammdatenEingabe(Me, CurrentEichprozess, Nothing, _CurrentUco, DialogModus)
                        CType(_CurrentUco, ucoContent).PreviousUco = uco
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
                        MessageBox.Show("unbekannter Status")
                        Me.Close()
                        Exit Sub
                End Select

                uco.NextUco = _CurrentUco
                CType(_CurrentUco, ucoContent).PreviousUco = uco
                ListofUcos.Add(uco)
            Else
                BreadCrumb.FindeElementUndSelektiere(CType(_CurrentUco, ucoContent).PreviousUco.EichprozessStatusReihenfolge)
                uco = CType(_CurrentUco, ucoContent).PreviousUco


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

#End Region



    Private Sub RadButtonVersenden_Click(sender As Object, e As EventArgs) Handles RadButtonVersenden.Click
        If MessageBox.Show("Möchten Sie das korrigierte Eichprotokoll wirklich an den Eichbevollmächtigten zurücksenden? Der Status wird dabei auf fehlerhaft gesetzt.", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            RaiseEvent VersendenNeeded(_CurrentUco)
        End If
    End Sub

    Private Sub RadButtonEntsperren_Click(sender As Object, e As EventArgs) Handles RadButtonEntsperren.Click
        'wenn der status des aktuellen elementes eh schon auf fehlerhaft steht oder auf abgeschlossen, darf keine Änderung verschickt werden
        If CurrentEichprozess.FK_Bearbeitungsstatus = 3 Then
            MessageBox.Show("Der Vorgang kann nicht bearbeitet werden. Er ist schon als genehmigt markiert")

            Exit Sub
        End If

        If CurrentEichprozess.FK_Bearbeitungsstatus = 2 Then
            MessageBox.Show("Der Vorgang kann nicht bearbeitet werden. Er ist schon als fehlerhaft markiert und wird bearbeitet")

            Exit Sub
        End If

        RaiseEvent EntsperrungNeeded()
        RadButtonVersenden.Visible = True
        RadButtonEntsperren.Enabled = False
    End Sub
End Class
