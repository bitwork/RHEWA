Public Class ucoPruefungWiederholbarkeitBelastung
Inherits ucoContent


    #Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _currentObjPruefungWiederholbarkeit As PruefungWiederholbarkeit
    Private _ListPruefungWiederholbarkeit As New List(Of PruefungWiederholbarkeit)
    #End Region

    #Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub

    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        _suspendEvents = True
        InitializeComponent()
        _suspendEvents = False

        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit

    End Sub

    #End Region

    #Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerWiederholbarkeit)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerWiederholbarkeit
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit

        'daten füllen
        LoadFromDatabase()
    End Sub

#Region "Bereich 1/2 MAX"
    Private Sub RadTextBoxControlBereich1DisplayWeight1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich1DisplayWeight1.Validating, _
  RadTextBoxControlBereich1DisplayWeight2.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, _
  RadTextBoxControlBereich1Weight1.Validating, _
  RadTextBoxControlBereich1Weight2.Validating, RadTextBoxControlBereich1Weight3.Validating

        Dim result As Decimal
        If Not sender.isreadonly = True Then

            'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
            If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text.Equals("") Then
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
                Exit Sub
            End If

            'versuchen ob der Text in eine Zahl konvertiert werden kann
            If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text, result) Then
                e.Cancel = True
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                System.Media.SystemSounds.Exclamation.Play()

            Else 'rahmen zurücksetzen
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
            End If
        End If
    End Sub
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 1
        RadTextBoxControlBereich1Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text


        'neu berechnen der Fehler und EFG

        Try
            RadTextBoxControlBereich1ErrorLimit1.Text = CDec(RadTextBoxControlBereich1DisplayWeight1.Text) - CDec(RadTextBoxControlBereich1Weight1.Text)
            If RadTextBoxControlBereich1DisplayWeight1.Text > CDec(RadTextBoxControlBereich1Weight1.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL1.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight1.Text < CDec(RadTextBoxControlBereich1Weight1.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL1.Checked = False
            Else
                RadCheckBoxBereich1VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit2.Text = CDec(RadTextBoxControlBereich1DisplayWeight2.Text) - CDec(RadTextBoxControlBereich1Weight2.Text)
            If RadTextBoxControlBereich1DisplayWeight2.Text > CDec(RadTextBoxControlBereich1Weight2.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL2.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight2.Text < CDec(RadTextBoxControlBereich1Weight2.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL2.Checked = False
            Else
                RadCheckBoxBereich1VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try


        Try
            RadTextBoxControlBereich1ErrorLimit3.Text = CDec(RadTextBoxControlBereich1DisplayWeight3.Text) - CDec(RadTextBoxControlBereich1Weight3.Text)
            If RadTextBoxControlBereich1DisplayWeight3.Text > CDec(RadTextBoxControlBereich1Weight3.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL3.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight3.Text < CDec(RadTextBoxControlBereich1Weight3.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL3.Checked = False
            Else
                RadCheckBoxBereich1VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub

#End Region

#Region "bereich MAX"
    Private Sub RadTextBoxControlBereich2DisplayWeight1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich2DisplayWeight1.Validating, _
    RadTextBoxControlBereich2DisplayWeight2.Validating, RadTextBoxControlBereich2DisplayWeight3.Validating, _
       RadTextBoxControlBereich2Weight1.Validating, _
    RadTextBoxControlBereich2Weight2.Validating, RadTextBoxControlBereich2Weight3.Validating


        Dim result As Decimal
        If Not sender.isreadonly = True Then

            'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
            If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text.Equals("") Then
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
                Exit Sub
            End If

            'versuchen ob der Text in eine Zahl konvertiert werden kann
            If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text, result) Then
                e.Cancel = True
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                System.Media.SystemSounds.Exclamation.Play()

            Else 'rahmen zurücksetzen
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
            End If
        End If
    End Sub


    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich2_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 1
        RadTextBoxControlBereich2Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text


        'neu berechnen der Fehler und EFG
        Try
            RadTextBoxControlBereich2ErrorLimit1.Text = CDec(RadTextBoxControlBereich2DisplayWeight1.Text) - CDec(RadTextBoxControlBereich2Weight1.Text)
            If RadTextBoxControlBereich2DisplayWeight1.Text > CDec(RadTextBoxControlBereich2Weight1.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL1.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight1.Text < CDec(RadTextBoxControlBereich2Weight1.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL1.Checked = False
            Else
                RadCheckBoxBereich2VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit2.Text = CDec(RadTextBoxControlBereich2DisplayWeight2.Text) - CDec(RadTextBoxControlBereich2Weight2.Text)
            If RadTextBoxControlBereich2DisplayWeight2.Text > CDec(RadTextBoxControlBereich2Weight2.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL2.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight2.Text < CDec(RadTextBoxControlBereich2Weight2.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL2.Checked = False
            Else
                RadCheckBoxBereich2VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try


        Try
            RadTextBoxControlBereich2ErrorLimit3.Text = CDec(RadTextBoxControlBereich2DisplayWeight3.Text) - CDec(RadTextBoxControlBereich2Weight3.Text)
            If RadTextBoxControlBereich2DisplayWeight3.Text > CDec(RadTextBoxControlBereich2Weight3.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL3.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight3.Text < CDec(RadTextBoxControlBereich2Weight3.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL3.Checked = False
            Else
                RadCheckBoxBereich2VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub
#End Region
    Private Sub RadCheckBoxBereich1VEL1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich1VEL1.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL3.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub
    ''' <summary>
    ''' Öffnen der Eichfehlergrenzen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonShowEFG_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFG2.Click
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()
    End Sub
#End Region
#Region "Methods"
    Private Sub LoadFromDatabase()

        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                'je nach verwahrenswahl (über 60 kg mit normalien) wurde noch keine Wiederholbarkeit geprueft. Wenn eni anderes verfahren gewählt wurde, gibt es an dieser stelle aber schon die halbe wiederholbarkeit
                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien"

                        'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                        Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                        _ListPruefungWiederholbarkeit = query.ToList
                    Case Else

                        'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen und "voll" sind. Halbe wurden an andere Stelle schon abgearbeitet
                        Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID And a.Belastung = "voll"
                        _ListPruefungWiederholbarkeit = query.ToList
                End Select

            End Using
        Else
            'je nach verwahrenswahl (über 60 kg mit normalien) wurde noch keine Wiederholbarkeit geprueft. Wenn eni anderes verfahren gewählt wurde, gibt es an dieser stelle aber schon die halbe wiederholbarkeit
            Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                Case Is = "über 60kg mit Normalien"
                    For Each obj In objEichprozess.Eichprotokoll.PruefungWiederholbarkeit
                        _ListPruefungWiederholbarkeit.Add(obj)
                    Next
                Case Else
                    For Each obj In objEichprozess.Eichprotokoll.PruefungWiederholbarkeit
                        If obj.Belastung = "voll" Then
                            _ListPruefungWiederholbarkeit.Add(obj)
                        End If
                    Next
            End Select

        End If


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
        'events abbrechen
        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:
        Try
            _intNullstellenE1 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1) '.Replace(",", "."))  + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE2 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2) '.Replace(",", ".")) + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE3 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3) '.Replace(",", "."))  + 1
        Catch ex As Exception
        End Try

        If _intNullstellenE1 > _intNullstellenE2 AndAlso _intNullstellenE1 > _intNullstellenE3 Then
            _intNullstellenE = _intNullstellenE1
        ElseIf _intNullstellenE2 > _intNullstellenE1 AndAlso _intNullstellenE2 > _intNullstellenE3 Then
            _intNullstellenE = _intNullstellenE2
        ElseIf _intNullstellenE3 > _intNullstellenE1 AndAlso _intNullstellenE3 > _intNullstellenE2 Then
            _intNullstellenE = _intNullstellenE3
        Else 'alles ist gleih
            _intNullstellenE = _intNullstellenE1
        End If

        'füllen der berechnenten Steuerelemente

        lblBereich1EFGSpeziallBerechnung.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
        lblBereich2EFGSpeziallBerechnung.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren



        'wenn es sich um das Staffel oder Fahrzeugwaagen verfahren handelt wird an dieser Stelle die Wiederholbarkeit nur mit MAX geprüft. MIN erfolgte dann bereits vorher
        Select Case objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
                FillControlsHalBMax()
                FillControlsBereichMAX()
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen, Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
                RadGroupBoxBereich1.Visible = False
                FillControlsBereichMAX()
        End Select
     
        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlBereich1Weight1.Focus()

    End Sub
    Private Sub FillControlsHalBMax()

        'bereich 1
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                lblBereich1EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                lblBereich2EFGSpeziallBerechnung.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

                RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
                RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
                RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                lblBereich1EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
                lblBereich2EFGSpeziallBerechnung.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

                RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
                RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
                RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                lblBereich1EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
                lblBereich2EFGSpeziallBerechnung.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

                RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
                RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
                RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
        End Select


        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "1" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich1Weight1.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich1DisplayWeight1.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "2" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich1Weight2.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich1DisplayWeight2.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "3" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich1Weight3.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich1DisplayWeight3.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If
    End Sub

    Private Sub FillControlsBereichMAX()
        'bereich 2
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
                RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
                RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
                RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
                RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
                RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
                RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        End Select


        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "1" And o.Belastung = "voll").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich2Weight1.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich2DisplayWeight1.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "2" And o.Belastung = "voll").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich2Weight2.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich2DisplayWeight2.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "3" And o.Belastung = "voll").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlBereich2Weight3.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlBereich2DisplayWeight3.Text = _currentObjPruefungWiederholbarkeit.Anzeige
        End If


    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungWiederholbarkeit)
        If PObjPruefung.Belastung = "halb" Then
            If PObjPruefung.Wiederholung = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL1.Checked
                PObjPruefung.EFG_Extra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL2.Checked
                PObjPruefung.EFG_Extra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL3.Checked
                PObjPruefung.EFG_Extra = lblBereich1EFGSpeziallBerechnung.Text
            End If

        ElseIf PObjPruefung.Belastung = "voll" Then
            If PObjPruefung.Wiederholung = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL1.Checked
                PObjPruefung.EFG_Extra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL2.Checked
                PObjPruefung.EFG_Extra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL3.Checked
                PObjPruefung.EFG_Extra = lblBereich2EFGSpeziallBerechnung.Text
            End If


        End If
    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaveing = False
        'wenn sie sie sichtbar ist (normalien verfahren) muss validiert werden
        If RadGroupBoxBereich1.Visible = True Then
            If RadCheckBoxBereich1VEL1.Checked = False And RadCheckBoxBereich1VEL1.Visible = True Or _
            RadCheckBoxBereich1VEL2.Checked = False And RadCheckBoxBereich1VEL2.Visible = True Or _
            RadCheckBoxBereich1VEL3.Checked = False And RadCheckBoxBereich1VEL3.Visible = True Then
                AbortSaveing = True

            End If
        End If


        If RadCheckBoxBereich2VEL1.Checked = False And RadCheckBoxBereich2VEL1.Visible = True Or _
              RadCheckBoxBereich2VEL2.Checked = False And RadCheckBoxBereich2VEL2.Visible = True Or _
              RadCheckBoxBereich2VEL3.Checked = False And RadCheckBoxBereich2VEL3.Visible = True Then
            AbortSaveing = True
        End If



        If AbortSaveing Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.EichfehlergrenzenNichtEingehalten, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True

    End Function

#Region "Bereich 1"
    Private Sub RadTextBoxControlBereich1DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit1.Text = CDec(RadTextBoxControlBereich1DisplayWeight1.Text) - CDec(RadTextBoxControlBereich1Weight1.Text)
            If RadTextBoxControlBereich1DisplayWeight1.Text > CDec(RadTextBoxControlBereich1Weight1.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL1.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight1.Text < CDec(RadTextBoxControlBereich1Weight1.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL1.Checked = False
            Else
                RadCheckBoxBereich1VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit2.Text = CDec(RadTextBoxControlBereich1DisplayWeight2.Text) - CDec(RadTextBoxControlBereich1Weight2.Text)
            If RadTextBoxControlBereich1DisplayWeight2.Text > CDec(RadTextBoxControlBereich1Weight2.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL2.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight2.Text < CDec(RadTextBoxControlBereich1Weight2.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL2.Checked = False
            Else
                RadCheckBoxBereich1VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit3.Text = CDec(RadTextBoxControlBereich1DisplayWeight3.Text) - CDec(RadTextBoxControlBereich1Weight3.Text)
            If RadTextBoxControlBereich1DisplayWeight3.Text > CDec(RadTextBoxControlBereich1Weight3.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL3.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight3.Text < CDec(RadTextBoxControlBereich1Weight3.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL3.Checked = False
            Else
                RadCheckBoxBereich1VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Bereich 2"
    Private Sub RadTextBoxControlBereich2DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit1.Text = CDec(RadTextBoxControlBereich2DisplayWeight1.Text) - CDec(RadTextBoxControlBereich2Weight1.Text)
            If RadTextBoxControlBereich2DisplayWeight1.Text > CDec(RadTextBoxControlBereich2Weight1.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL1.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight1.Text < CDec(RadTextBoxControlBereich2Weight1.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL1.Checked = False
            Else
                RadCheckBoxBereich2VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit2.Text = CDec(RadTextBoxControlBereich2DisplayWeight2.Text) - CDec(RadTextBoxControlBereich2Weight2.Text)
            If RadTextBoxControlBereich2DisplayWeight2.Text > CDec(RadTextBoxControlBereich2Weight2.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL2.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight2.Text < CDec(RadTextBoxControlBereich2Weight2.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL2.Checked = False
            Else
                RadCheckBoxBereich2VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit3.Text = CDec(RadTextBoxControlBereich2DisplayWeight3.Text) - CDec(RadTextBoxControlBereich2Weight3.Text)
            If RadTextBoxControlBereich2DisplayWeight3.Text > CDec(RadTextBoxControlBereich2Weight3.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL3.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight3.Text < CDec(RadTextBoxControlBereich2Weight3.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL3.Checked = False
            Else
                RadCheckBoxBereich2VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If ValidateControls() = True Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1



                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                        If Not dobjEichprozess Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dobjEichprozess


                            'hier muss unterschieden werden, welches verfahren gewählt wurde.
                            'wenn es nicht mit normalien ist, dann wird an dieser stelle volle und halbe last eingetragen
                            'ansonsten wird nur noch die volle eingetragen

                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungWiederholbarkeit.Count = 0 Then
                                'anzahl Wiederholungen beträgt 3 um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                                For i As Integer = 1 To 3

                                    'halbe Last
                                    Dim objPruefung = Context.PruefungWiederholbarkeit.Create

                                    'muss nur im normalien verfahren gespeichert werden. wenn ein Staffelverfahren gewählt wurde wird die Groupbox ausgelbendet
                                    If RadGroupBoxBereich1.Visible = True Then
                                        objPruefung.Wiederholung = i
                                        objPruefung.Belastung = "halb"
                                        UpdatePruefungsObject(objPruefung)

                                        Context.SaveChanges()

                                        objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(objPruefung)
                                        Context.SaveChanges()

                                        _ListPruefungWiederholbarkeit.Add(objPruefung)
                                    End If


                                    'max Last
                                    objPruefung = Nothing
                                    objPruefung = Context.PruefungWiederholbarkeit.Create
                                    'wenn es die eine itereation mehr ist:
                                    objPruefung.Belastung = "voll"
                                    objPruefung.Wiederholung = i
                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungWiederholbarkeit.Add(objPruefung)
                                Next
                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungWiederholbarkeit
                                    objPruefung = Context.PruefungWiederholbarkeit.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next
                            End If


                            'neuen Status zuweisen

                            If AktuellerStatusDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige
                                AktuellerStatusDirty = False
                            End If



                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()
                            'Speichern in Datenbank
                            Context.SaveChanges()
                        End If
                    End If


                End Using



                ParentFormular.CurrentEichprozess = objEichprozess
            End If

        End If
    End Sub

    Protected Friend Overrides Sub SaveWithoutValidationNeeded(usercontrol As UserControl)
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
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.Include("Eichprotokoll").FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess



                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungWiederholbarkeit.Count = 0 Then
                            'anzahl Wiederholungen beträgt 3 um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            For i As Integer = 1 To 3

                                'halbe Last
                                Dim objPruefung = Context.PruefungWiederholbarkeit.Create
                                'muss nur im normalien verfahren gespeichert werden. wenn ein Staffelverfahren gewählt wurde wird die Groupbox ausgelbendet
                                If RadGroupBoxBereich1.Visible = True Then
                                    objPruefung.Wiederholung = i
                                    objPruefung.Belastung = "halb"
                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungWiederholbarkeit.Add(objPruefung)
                                End If

                                'max Last
                                objPruefung = Nothing
                                objPruefung = Context.PruefungWiederholbarkeit.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.Belastung = "voll"
                                objPruefung.Wiederholung = i
                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(objPruefung)
                                Context.SaveChanges()

                                _ListPruefungWiederholbarkeit.Add(objPruefung)
                            Next
                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungWiederholbarkeit
                                objPruefung = Context.PruefungWiederholbarkeit.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next
                        End If

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                End If
            End Using

            ParentFormular.CurrentEichprozess = objEichprozess
        End If
    End Sub

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoPruefungWiederholbarkeitBelastung))



        Me.RadGroupBoxBereich1.Text = resources.GetString("RadGroupBoxBereich1.Text")
        Me.RadGroupBoxBereich2.Text = resources.GetString("RadGroupBoxBereich2.Text")
        Me.RadGroupBoxPruefungAussermittigeBelastung.Text = resources.GetString("RadGroupBoxPruefungAussermittigeBelastung.Text")
        Me.lblBereich1AnzeigeGewicht.Text = resources.GetString("lblBereich1AnzeigeGewicht.Text")
        Me.lblBereich1EFGSpezial.Text = resources.GetString("lblBereich1EFGSpezial.Text")
        Me.lblBereich1EFGSpeziallBerechnung.Text = resources.GetString("lblBereich1EFGSpeziallBerechnung.Text")
        Me.lblBereich1FehlerGrenzen.Text = resources.GetString("lblBereich1FehlerGrenzen.Text")
        Me.lblBereich1Gewicht.Text = resources.GetString("lblBereich1Gewicht.Text")


        Me.lblBereich2AnzeigeGewicht.Text = resources.GetString("lblBereich2AnzeigeGewicht.Text")
        Me.lblBereich2EFGSpezial.Text = resources.GetString("lblBereich2EFGSpezial.Text")
        Me.lblBereich2EFGSpeziallBerechnung.Text = resources.GetString("lblBereich2EFGSpeziallBerechnung.Text")
        Me.lblBereich2FehlerGrenzen.Text = resources.GetString("lblBereich2FehlerGrenzen.Text")
        Me.lblBereich2Gewicht.Text = resources.GetString("lblBereich2Gewicht.Text")



        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerWiederholbarkeit)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerWiederholbarkeit
            Catch ex As Exception
            End Try
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerWiederholbarkeit)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerWiederholbarkeit
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region
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


        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)
            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Eichbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()

                'erzeuegn eines Server Objektes auf basis des aktuellen DS
                objServerEichprozess = clsClientServerConversionFunctions.CopyObjectProperties(objServerEichprozess, objEichprozess)
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
                        Webcontext.AddEichprozess(objLiz.HEKennung, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

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
