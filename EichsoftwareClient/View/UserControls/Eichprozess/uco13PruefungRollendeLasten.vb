Public Class uco13PruefungRollendeLasten

    Inherits ucoContent
    Implements IRhewaEditingDialog
    Implements IRhewaPruefungDialog

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _currentObjPruefungRollendeLasten As PruefungRollendeLasten
    Private _ListPruefungRollendeLasten As New List(Of PruefungRollendeLasten)
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten
    End Sub
#End Region



#Region "Events"

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten

        LadeBilder()
        'daten füllen
        LoadFromDatabase()
    End Sub


#Region "Bereich Links"

    Private Sub RadTextBoxControlLastLinks1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlLastRechts3.Validating, RadTextBoxControlLastRechts2.Validating, RadTextBoxControlLastRechts1.Validating, RadTextBoxControllastLinks3.Validating, RadTextBoxControlLastLinks2.Validating, RadTextBoxControlLastLinks1.Validating, RadTextBoxControlAnzeigeRechts3.Validating, RadTextBoxControlAnzeigeRechts2.Validating, RadTextBoxControlAnzeigeRechts1.Validating, RadTextBoxControlAnzeigeLinks3.Validating, RadTextBoxControlAnzeigeLinks2.Validating, RadTextBoxControlAnzeigeLinks1.Validating
        BasicTextboxValidation(sender, e)
    End Sub


    ''' <summary>
    ''' wenn sich einer der Anzeige Werte ändert, müssen die Fehler und EFG neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlAnzeigeLinks1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeigeLinks3.TextChanged, RadTextBoxControlAnzeigeLinks2.TextChanged, RadTextBoxControlAnzeigeLinks1.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            BerechneFehlerundEFGLinks(sender)
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Sub RadTextBoxControlLastLinks1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlLastRechts3.TextChanged, RadTextBoxControlLastRechts2.TextChanged, RadTextBoxControlLastRechts1.TextChanged, RadTextBoxControllastLinks3.TextChanged, RadTextBoxControlLastLinks2.TextChanged, RadTextBoxControlLastLinks1.TextChanged
        If _suspendEvents = True Then Exit Sub

        RadTextBoxControlLastRechts3.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text
        RadTextBoxControlLastRechts2.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text
        RadTextBoxControlLastRechts1.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text
        RadTextBoxControllastLinks3.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text
        RadTextBoxControlLastLinks2.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text
        RadTextBoxControlLastLinks1.Text = CType(sender, Telerik.WinControls.UI.RadTextBox).Text

        BerechneFehlerundEFGLinks(sender)
        BerechneFehlerundEFGRechts(sender)
    End Sub

#Region "bereich Rechts"
    ''' <summary>
    ''' wenn sich einer der Anzeige Werte ändert, müssen die Fehler und EFG neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlAnzeigeRechts1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeigeRechts3.TextChanged, RadTextBoxControlAnzeigeRechts2.TextChanged, RadTextBoxControlAnzeigeRechts1.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub

            BerechneFehlerundEFGRechts(sender)
        Catch ex As Exception
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' Öffnen der Eichfehlergrenzen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonShowEFG_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFG.Click
        ShowEichfehlergrenzenDialog()
    End Sub
#End Region




#Region "Methods"
    Private Sub LadeBilder()
        If AktuellerBenutzer.Instance.AktuelleSprache.ToLower = "en" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_GB
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_GB
        ElseIf AktuellerBenutzer.Instance.AktuelleSprache = "de" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_DE
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_DE
        ElseIf AktuellerBenutzer.Instance.AktuelleSprache = "pl" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_PL
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_PL
        Else
            PictureBox2.Image = My.Resources.Verschiebeweg_GB
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_GB
        End If
    End Sub

    Private Sub BerechneFehlerundEFGLinks(Optional Sender As Telerik.WinControls.UI.RadTextBox = Nothing)
        AktuellerStatusDirty = True
        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'EFG
        BerechneEFG(lblEFGWertLinks)
        'fehler berechnen
        BeechneFehler(RadTextBoxControlFehlerLinks1, RadTextBoxControlAnzeigeLinks1, RadTextBoxControlLastLinks1, lblEFGWertLinks, RadCheckBoxAuffahrtLinks1)
        BeechneFehler(RadTextBoxControlFehlerLinks2, RadTextBoxControlAnzeigeLinks2, RadTextBoxControlLastLinks2, lblEFGWertLinks, RadCheckBoxAuffahrtLinks2)
        BeechneFehler(RadTextBoxControlFehlerLinks3, RadTextBoxControlAnzeigeLinks3, RadTextBoxControllastLinks3, lblEFGWertLinks, RadCheckBoxAuffahrtLinks3)
        _suspendEvents = False
    End Sub


    Private Sub BerechneFehlerundEFGRechts(Optional sender As Telerik.WinControls.UI.RadTextBox = Nothing)
        AktuellerStatusDirty = True
        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'neu berechnen der Fehler und EFG
        'EFG
        BerechneEFG(lblEFGWertRechts)
        'fehler berechnen
        BeechneFehler(RadTextBoxControlFehlerRechts1, RadTextBoxControlAnzeigeRechts1, RadTextBoxControlLastRechts1, lblEFGWertRechts, RadCheckBoxAuffahrtRechts1)
        BeechneFehler(RadTextBoxControlFehlerRechts2, RadTextBoxControlAnzeigeRechts2, RadTextBoxControlLastRechts2, lblEFGWertRechts, RadCheckBoxAuffahrtRechts2)
        BeechneFehler(RadTextBoxControlFehlerRechts3, RadTextBoxControlAnzeigeRechts3, RadTextBoxControlLastRechts3, lblEFGWertRechts, RadCheckBoxAuffahrtRechts3)
        _suspendEvents = False
    End Sub

    Private Sub FillControlsLinks()

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        Try
            _currentObjPruefungRollendeLasten = Nothing
            _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = 1 And o.AuffahrtSeite = "links").FirstOrDefault
            If Not _currentObjPruefungRollendeLasten Is Nothing Then
                RadTextBoxControlLastLinks1.Text = _currentObjPruefungRollendeLasten.Last
                RadTextBoxControlAnzeigeLinks1.Text = _currentObjPruefungRollendeLasten.Anzeige
                RadTextBoxControlFehlerLinks1.Text = _currentObjPruefungRollendeLasten.Fehler
                RadCheckBoxAuffahrtLinks1.Checked = _currentObjPruefungRollendeLasten.EFG
            End If
        Catch e As Exception
        End Try

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = 2 And o.AuffahrtSeite = "links").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastLinks2.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeLinks2.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerLinks2.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxAuffahrtLinks2.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = 3 And o.AuffahrtSeite = "links").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControllastLinks3.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeLinks3.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerLinks3.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxAuffahrtLinks3.Checked = _currentObjPruefungRollendeLasten.EFG
        End If
    End Sub

    Private Sub FillControlsRechts()
        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = "1" And o.AuffahrtSeite = "rechts").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastRechts1.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeRechts1.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerRechts1.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxAuffahrtRechts1.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = "2" And o.AuffahrtSeite = "rechts").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastRechts2.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeRechts2.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerRechts2.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxAuffahrtRechts2.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = "3" And o.AuffahrtSeite = "rechts").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastRechts3.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeRechts3.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerRechts3.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxAuffahrtRechts3.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

    End Sub

    Private Sub BerechneEFG(EFGWert As Telerik.WinControls.UI.RadMaskedEditBox)
        Try
            'nimm Max von den Bereichen
            'wenn die Last > ist als  2000e-3000e dann nehme EFG2000e sonst EFG500e
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If

                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        EFGWert.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
            End Select
        Catch e As Exception
        End Try
    End Sub

    Private Sub BeechneFehler(TextFehler As Telerik.WinControls.UI.RadTextBox,
                           TextAnzeige As Telerik.WinControls.UI.RadTextBox,
                           TextLast As Telerik.WinControls.UI.RadTextBox,
                           TextEFG As Telerik.WinControls.UI.RadMaskedEditBox,
                           CheckEG As Telerik.WinControls.UI.RadCheckBox)
        'neu berechnen der Fehler und EFG
        Try
            If IsNumeric(TextAnzeige.Text) And IsNumeric(TextLast.Text) Then
                TextFehler.Text = CDec(TextAnzeige.Text) - CDec(TextLast.Text)
            End If
            If IsNumeric(TextAnzeige.Text) And IsNumeric(TextLast.Text) And IsNumeric(TextEFG.Text) Then
                If CDec(TextAnzeige.Text) > CDec(TextLast.Text) + CDec(TextEFG.Text) Then
                    CheckEG.Checked = False
                ElseIf CDec(TextAnzeige.Text) < CDec(TextLast.Text) - CDec(TextEFG.Text) Then
                    CheckEG.Checked = False
                Else
                    CheckEG.Checked = True
                End If
            Else
                CheckEG.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LadePruefungen() Implements IRhewaPruefungDialog.LadePruefungen
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            LadePruefungenLeseModus()
        Else
            LadePruefungenBearbeitungsModus()
        End If
    End Sub

    Private Sub LadePruefungenBearbeitungsModus() Implements IRhewaPruefungDialog.LadePruefungenBearbeitungsModus
        _ListPruefungRollendeLasten.Clear()

        Try
            For Each obj In objEichprozess.Eichprotokoll.PruefungRollendeLasten
                obj.Eichprotokoll = objEichprozess.Eichprotokoll

                _ListPruefungRollendeLasten.Add(obj)
            Next
        Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
            Using context As New Entities
                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungRollendeLasten Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungRollendeLasten = query.ToList

            End Using
        End Try
    End Sub

    Private Sub LadePruefungenLeseModus() Implements IRhewaPruefungDialog.LadePruefungenLeseModus
        Using context As New Entities
            'neu laden des Objekts, diesmal mit den lookup Objekten
            objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

            'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
            Dim query = From a In context.PruefungRollendeLasten Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
            _ListPruefungRollendeLasten = query.ToList

        End Using
    End Sub


    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungRollendeLasten)
        If PObjPruefung.AuffahrtSeite = "links" Then
            If PObjPruefung.Belastungsstelle = "1" Then
                PObjPruefung.Last = RadTextBoxControlLastLinks1.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeLinks1.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerLinks1.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtLinks1.Checked
                PObjPruefung.EFGExtra = lblEFGWertLinks.Text
            End If
            If PObjPruefung.Belastungsstelle = "2" Then
                PObjPruefung.Last = RadTextBoxControlLastLinks2.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeLinks2.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerLinks2.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtLinks2.Checked
                PObjPruefung.EFGExtra = lblEFGWertLinks.Text
            End If
            If PObjPruefung.Belastungsstelle = "3" Then
                PObjPruefung.Last = RadTextBoxControllastLinks3.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeLinks3.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerLinks3.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtLinks3.Checked
                PObjPruefung.EFGExtra = lblEFGWertLinks.Text
            End If

        ElseIf PObjPruefung.AuffahrtSeite = "rechts" Then
            If PObjPruefung.Belastungsstelle = "1" Then
                PObjPruefung.Last = RadTextBoxControlLastRechts1.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeRechts1.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerRechts1.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtRechts1.Checked
                PObjPruefung.EFGExtra = lblEFGWertRechts.Text
            End If
            If PObjPruefung.Belastungsstelle = "2" Then
                PObjPruefung.Last = RadTextBoxControlLastRechts2.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeRechts2.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerRechts2.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtRechts2.Checked
                PObjPruefung.EFGExtra = lblEFGWertRechts.Text
            End If
            If PObjPruefung.Belastungsstelle = "3" Then
                PObjPruefung.Last = RadTextBoxControlLastRechts3.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeRechts3.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerRechts3.Text
                PObjPruefung.EFG = RadCheckBoxAuffahrtRechts3.Checked
                PObjPruefung.EFGExtra = lblEFGWertRechts.Text
            End If
        End If
    End Sub

    Private Sub UeberschreibePruefungsobjekte()
        objEichprozess.Eichprotokoll.PruefungRollendeLasten.Clear()
        For Each obj In _ListPruefungRollendeLasten
            objEichprozess.Eichprotokoll.PruefungRollendeLasten.Add(obj)
        Next

    End Sub



#End Region

#Region "Interface Methods"


    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungRollendeLasten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungRollendelasten
            Catch ex As Exception
            End Try
        End If
    End Sub


    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase

        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True
        LadePruefungen()

        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(RadScrollablePanel1.PanelContainer)

        End If
        'events abbrechen
        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:

        HoleNullstellen()

        'füllen der berechnenten Steuerelemente

        lblEFGWertLinks.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
        lblEFGWertRechts.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren

        'wenn es sich um das Staffel oder Fahrzeugwaagen verfahren handelt wird an dieser Stelle die Wiederholbarkeit nur mit MAX geprüft. MIN erfolgte dann bereits vorher

        FillControlsLinks()
        FillControlsRechts()
        BerechneFehlerundEFGLinks()
        BerechneFehlerundEFGRechts()

        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlAnzeigeLinks1.Focus()

    End Sub



    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        'neuen Context aufbauen
        Using Context As New Entities

            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungRollendeLasten
                Dim objPruefung = Context.PruefungRollendeLasten.FirstOrDefault(Function(value) value.ID = obj.ID)
                If Not objPruefung Is Nothing Then
                    'in lokaler DB gucken
                    UpdatePruefungsObject(objPruefung)
                Else 'es handelt sich um eine Serverobjekt im => Korrekturmodus
                    If DialogModus = enuDialogModus.korrigierend Then
                        UpdatePruefungsObject(obj)
                    End If
                End If
            Next

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

        If RadCheckBoxAuffahrtLinks1.Checked = False And RadCheckBoxAuffahrtLinks1.Visible = True Or
            RadCheckBoxAuffahrtLinks2.Checked = False And RadCheckBoxAuffahrtLinks2.Visible = True Or
            RadCheckBoxAuffahrtLinks3.Checked = False And RadCheckBoxAuffahrtLinks3.Visible = True Then
            AbortSaving = True

            RadTextBoxControlAnzeigeLinks1.TextBoxElement.Border.ForeColor = Color.Red
            RadTextBoxControlAnzeigeLinks2.TextBoxElement.Border.ForeColor = Color.Red
            RadTextBoxControlAnzeigeLinks3.TextBoxElement.Border.ForeColor = Color.Red

        End If

        If RadCheckBoxAuffahrtRechts1.Checked = False And RadCheckBoxAuffahrtRechts1.Visible = True Or
              RadCheckBoxAuffahrtRechts2.Checked = False And RadCheckBoxAuffahrtRechts2.Visible = True Or
              RadCheckBoxAuffahrtRechts3.Checked = False And RadCheckBoxAuffahrtRechts2.Visible = True Then
            AbortSaving = True
            RadTextBoxControlAnzeigeRechts1.TextBoxElement.Border.ForeColor = Color.Red
            RadTextBoxControlAnzeigeRechts2.TextBoxElement.Border.ForeColor = Color.Red
            RadTextBoxControlAnzeigeRechts3.TextBoxElement.Border.ForeColor = Color.Red
        End If

        'fehlermeldung anzeigen bei falscher validierung
        Dim result = Me.ShowValidationErrorBox(False)
        Return ProcessResult(result)

    End Function

    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadTextBoxControlAnzeigeLinks1.Text = RadTextBoxControlLastLinks1.Text
        RadTextBoxControlAnzeigeLinks2.Text = RadTextBoxControlLastLinks2.Text
        RadTextBoxControlAnzeigeLinks3.Text = RadTextBoxControllastLinks3.Text

        RadTextBoxControlAnzeigeRechts1.Text = RadTextBoxControlLastRechts1.Text
        RadTextBoxControlAnzeigeRechts2.Text = RadTextBoxControlLastRechts2.Text
        RadTextBoxControlAnzeigeRechts3.Text = RadTextBoxControlLastRechts3.Text
    End Sub




    Protected Friend Overrides Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt
        Using Context As New Entities
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess

                    'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                    If _ListPruefungRollendeLasten.Count = 0 Then
                        'anzahl Wiederholungen beträgt 3 um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                        For i As Integer = 1 To 3

                            'linke Last
                            Dim objPruefung = Context.PruefungRollendeLasten.Create

                            objPruefung.Belastungsstelle = i
                            objPruefung.AuffahrtSeite = "links"
                            UpdatePruefungsObject(objPruefung)

                            Context.SaveChanges()

                            objEichprozess.Eichprotokoll.PruefungRollendeLasten.Add(objPruefung)
                            Context.SaveChanges()

                            _ListPruefungRollendeLasten.Add(objPruefung)

                            'rechte Last
                            objPruefung = Nothing
                            objPruefung = Context.PruefungRollendeLasten.Create
                            'wenn es die eine itereation mehr ist:
                            objPruefung.AuffahrtSeite = "rechts"
                            objPruefung.Belastungsstelle = i
                            UpdatePruefungsObject(objPruefung)

                            Context.SaveChanges()

                            objEichprozess.Eichprotokoll.PruefungRollendeLasten.Add(objPruefung)
                            Context.SaveChanges()

                            _ListPruefungRollendeLasten.Add(objPruefung)
                        Next
                    Else ' es gibt bereits welche
                        'jedes objekt initialisieren und aus context laden und updaten
                        For Each objPruefung In _ListPruefungRollendeLasten
                            objPruefung = Context.PruefungRollendeLasten.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                            UpdatePruefungsObject(objPruefung)
                            Context.SaveChanges()
                        Next
                    End If

                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObjekt()
                    'Speichern in Datenbank
                    Context.SaveChanges()

                End If
            End If
        End Using
    End Sub


    Protected Friend Overrides Sub AktualisiereStatus() Implements IRhewaEditingDialog.AktualisiereStatus
        Using Context As New Entities
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess

                    'neuen Status zuweisen
                    'die reihenfolge wird hier je nach Verfahren verändert

                    If AktuellerStatusDirty = False Then
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        End If
                    ElseIf AktuellerStatusDirty = True Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                        AktuellerStatusDirty = False
                    End If

                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObjekt()
                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using
    End Sub

    Protected Friend Overrides Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Then
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If

        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function


    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco13PruefungRollendeLasten))
        Lokalisierung(Me, resources)
    End Sub



#End Region


    Private Sub RadCheckBoxAuffahrtLinks1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxAuffahrtRechts3.MouseClick, RadCheckBoxAuffahrtRechts2.MouseClick, RadCheckBoxAuffahrtRechts1.MouseClick, RadCheckBoxAuffahrtLinks3.MouseClick, RadCheckBoxAuffahrtLinks2.MouseClick, RadCheckBoxAuffahrtLinks1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        ShowEichfehlergrenzenDialog()
    End Sub


    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadScrollablePanel1.PanelContainer)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        UeberschreibePruefungsobjekte()
        'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
        CloneAndSendServerObjekt()
    End Sub
End Class