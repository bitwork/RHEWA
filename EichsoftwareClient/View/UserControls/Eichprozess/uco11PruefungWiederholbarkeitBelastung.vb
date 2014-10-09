Public Class uco11PruefungWiederholbarkeitBelastung
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


    Private Sub CalculateEFG(bereich As String, Wiederholung As String)

        Dim Last As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", bereich, Wiederholung))
        Dim Anzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", bereich, Wiederholung))
        Dim Fehler As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}ErrorLimit{1}", bereich, Wiederholung))
        Dim EFG As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}VEL{1}", bereich, Wiederholung))
        Dim Spezial As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblBereich{0}EFGSpeziallBerechnung", Bereich))


        'neu berechnen der Fehler und EFG


        ' Alte Formel
        'Try
        '    Fehler.Text = CDec(Anzeige.Text) - CDec(Last.Text)
        '    If Anzeige.Text > CDec(Last.Text) + CDec(Spezial.Text) Then
        '        EFG.Checked = False
        '    ElseIf Anzeige.Text < CDec(Last.Text) - CDec(Spezial.Text) Then
        '        EFG.Checked = False
        '    Else
        '        EFG.Checked = True
        '    End If
        'Catch ex As Exception
        'End Try



        'Neue EFG Formel nach Herrn Strack
        Try
            Fehler.Text = CDec(Anzeige.Text) - CDec(Last.Text)
        Catch ex As Exception
            'anzeige ist manchmal leer
        End Try

        Dim Faktor As Decimal = 0
        If CDec(Last.Text) <= 500 * CDec(Spezial.Text) Then
            Faktor = 0.5
        ElseIf CDec(Last.Text) > 500 * CDec(Spezial.Text) And CDec(Last.Text) <= 2000 * CDec(Spezial.Text) Then
            Faktor = 1
        ElseIf CDec(Last.Text) > 2000 * CDec(Spezial.Text) And CDec(Last.Text) <= 10000 * CDec(Spezial.Text) Then
            Faktor = 1.5
        Else
            Faktor = 1.5
        End If

        'sonderfall: EFG durch die Differenz zwischen den 3 Belastungen. Mit anderen Worten: Die Differenz der Wägeergebnisse bei der 3maligen Belastung darf nicht größer sein, als der Absolutwert der für diese Belastung geltenden Fehlergrenze der Waage.
        Dim wert1 As String = ""
        Dim wert2 As String = ""
        Dim wert3 As String = ""
        'TODO neue Formel implementieren
        Dim AnzeigeMax1 As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", bereich, 1))
        Dim AnzeigeMax2 As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", bereich, 2))
        Dim AnzeigeMax3 As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", bereich, 3))

        wert1 = AnzeigeMax1.Text
        If wert1 = "" Then
            wert1 = 0
        End If
        wert2 = AnzeigeMax2.Text
        If wert2 = "" Then
            wert2 = 0
        End If
        wert3 = AnzeigeMax3.Text
        If wert3 = "" Then
            wert3 = 0
        End If





        If CDec(Anzeige.Text) < (CDec(Last.Text) - (Faktor * CDec(Spezial.Text))) Or CDec(Anzeige.Text) > ((CDec(Last.Text) + (Faktor * CDec(Spezial.Text)))) Then
            EFG.Checked = False
        Else
            EFG.Checked = True
        End If


    End Sub

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

    Private Sub RadTextBoxControlBereich1DisplayWeight1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich1DisplayWeight1.Validating, _
       RadTextBoxControlBereich1DisplayWeight2.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, _
       RadTextBoxControlBereich1Weight1.Validating, _
       RadTextBoxControlBereich1Weight2.Validating, RadTextBoxControlBereich1Weight3.Validating,
       RadTextBoxControlBereich2DisplayWeight1.Validating, _
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

#Region "Bereich 1/2 MAX"

    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged,
    RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        Dim Bereich = GetBereich(sender)


        If Bereich = "1" Then
            'bereich 1
            RadTextBoxControlBereich1Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
            RadTextBoxControlBereich1Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
            RadTextBoxControlBereich1Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        ElseIf Bereich = "2" Then
            RadTextBoxControlBereich2Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
            RadTextBoxControlBereich2Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
            RadTextBoxControlBereich2Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        End If

        'neu berechnen der Fehler und EFG
        For Wiederholung As Integer = 1 To 3
            CalculateEFG(Bereich, Wiederholung)
        Next

        _suspendEvents = False
    End Sub

#End Region

#Region "bereich MAX"
   


    
#End Region
    Private Sub RadCheckBoxBereich1VEL1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich1VEL1.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL3.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub

    
#End Region
#Region "Methods"

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

                        'abrufen aller Prüfungs entitäten die sich auf dieses Messprotokoll beziehen
                        Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                        _ListPruefungWiederholbarkeit = query.ToList
                    Case Else

                        'abrufen aller Prüfungs entitäten die sich auf dieses Messprotokoll beziehen und "voll" sind. Halbe wurden an andere Stelle schon abgearbeitet
                        Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID And a.Belastung = "voll"
                        _ListPruefungWiederholbarkeit = query.ToList
                End Select

            End Using
        Else
            'je nach verwahrenswahl (über 60 kg mit normalien) wurde noch keine Wiederholbarkeit geprueft. Wenn eni anderes verfahren gewählt wurde, gibt es an dieser stelle aber schon die halbe wiederholbarkeit
            Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                Case Is = "über 60kg mit Normalien"

                    Try
                        _ListPruefungWiederholbarkeit.Clear()
                        For Each obj In objEichprozess.Eichprotokoll.PruefungWiederholbarkeit
                            obj.Eichprotokoll = objEichprozess.Eichprotokoll

                            _ListPruefungWiederholbarkeit.Add(obj)
                        Next
                    Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
                        Using context As New EichsoftwareClientdatabaseEntities1
                            'abrufen aller Prüfungs entitäten die sich auf dieses Messprotokoll beziehen
                            Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                            _ListPruefungWiederholbarkeit = query.ToList

                        End Using
                    End Try
                Case Else
                    _ListPruefungWiederholbarkeit.Clear()

                    Try
                        For Each obj In objEichprozess.Eichprotokoll.PruefungWiederholbarkeit
                            If obj.Belastung = "voll" Then
                                obj.Eichprotokoll = objEichprozess.Eichprotokoll
                                _ListPruefungWiederholbarkeit.Add(obj)
                            End If
                        Next
                    Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
                        Using context As New EichsoftwareClientdatabaseEntities1
                            'abrufen aller Prüfungs entitäten die sich auf dieses Messprotokoll beziehen und "voll" sind. Halbe wurden an andere Stelle schon abgearbeitet
                            Dim query = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID And a.Belastung = "voll"
                            _ListPruefungWiederholbarkeit = query.ToList
                        End Using
                    End Try
            End Select

        End If


        'steuerelemente mit werten aus DB füllen
        FillControls()
        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
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

        HoleNullstellen()

        'füllen der berechnenten Steuerelemente

        lblBereich1EFGSpeziallBerechnung.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
        lblBereich2EFGSpeziallBerechnung.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren

        'wenn es sich um das Staffel oder Fahrzeugwaagen verfahren handelt wird an dieser Stelle die Wiederholbarkeit nur mit MAX geprüft. MIN erfolgte dann bereits vorher
        Select Case objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
                FillControlsMax("halb")
                FillControlsMax("voll")
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen, Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
                RadGroupBoxBereich1.Visible = False
                FillControlsMax("voll")
        End Select

        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlBereich1Weight1.Focus()

    End Sub

    Private Sub FillControlsMax(Belastung As String)
        Dim Bereich As String
        Dim Eichwert As Decimal
        Dim Hoechstlast As Decimal
        Dim Faktor As Decimal

        If Belastung = "halb" Then
            Bereich = "1"
            Faktor = 0.5
        Else
            Bereich = "2"
            Faktor = 1
        End If


        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                Hoechstlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * Faktor
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
                Hoechstlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * Faktor
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
                Hoechstlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * Faktor
        End Select

        lblBereich1EFGSpeziallBerechnung.Text = Eichwert
        lblBereich2EFGSpeziallBerechnung.Text = Math.Round(Eichwert * 1.5, _intNullstellenE, MidpointRounding.AwayFromZero)

        If Belastung = "halb" Then
            RadTextBoxControlBereich1Weight1.Text = Hoechstlast
            RadTextBoxControlBereich1Weight2.Text = Hoechstlast
            RadTextBoxControlBereich1Weight3.Text = Hoechstlast
        ElseIf Belastung = "voll" Then
            RadTextBoxControlBereich2Weight1.Text = Hoechstlast
            RadTextBoxControlBereich2Weight2.Text = Hoechstlast
            RadTextBoxControlBereich2Weight3.Text = Hoechstlast
        End If

        For i As Integer = 1 To 3
            Dim Wiederholung As String = i

            'anzeige KG Nur laden wenn schon etwas eingegeben wurde
            _currentObjPruefungWiederholbarkeit = Nothing
            _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = Wiederholung And o.Belastung = Belastung).FirstOrDefault

            If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
                Dim last As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Wiederholung))
                Dim Anzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Wiederholung))

                last.Text = _currentObjPruefungWiederholbarkeit.Last
                Anzeige.Text = _currentObjPruefungWiederholbarkeit.Anzeige
            End If
        Next
    End Sub


    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungWiederholbarkeit
                Dim objPruefung = Context.PruefungWiederholbarkeit.FirstOrDefault(Function(value) value.ID = obj.ID)
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

    Private Sub UeberschreibePruefungsobjekte()
        objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Clear()
        For Each obj In _ListPruefungWiederholbarkeit
            objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(obj)
        Next
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungWiederholbarkeit)
        Dim Wiederholung = PObjPruefung.Wiederholung
        Dim Bereich As String = ""

        If PObjPruefung.Belastung = "halb" Then
            Bereich = "1"
        Else
            Bereich = "2"
        End If

        Dim Last As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Wiederholung))
        Dim Anzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Wiederholung))
        Dim Fehler As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}ErrorLimit{1}", Bereich, Wiederholung))
        Dim EFG As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}VEL{1}", Bereich, Wiederholung))
        Dim Spezial As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblBereich{0}EFGSpeziallBerechnung", Bereich))

        PObjPruefung.Last = Last.Text
        PObjPruefung.Anzeige = Anzeige.Text
        PObjPruefung.Fehler = Fehler.Text
        PObjPruefung.EFG = EFG.Checked
        PObjPruefung.EFG_Extra = Spezial.Text
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

        'fehlermeldung anzeigen bei falscher validierung
        Return Me.ShowValidationErrorBox()

    End Function

    Private Sub RadTextBoxControlBereich1DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles _
        RadTextBoxControlBereich1DisplayWeight1.TextChanged,
        RadTextBoxControlBereich1DisplayWeight2.TextChanged,
        RadTextBoxControlBereich1DisplayWeight3.TextChanged,
        RadTextBoxControlBereich2DisplayWeight1.TextChanged,
        RadTextBoxControlBereich2DisplayWeight2.TextChanged,
        RadTextBoxControlBereich2DisplayWeight3.TextChanged

        Try
            Dim Bereich As String = GetBereich(sender)
            Dim Wiederholung As String = GetWiederholung(sender)

            CalculateEFG(Bereich, Wiederholung)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
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

    Protected Overrides Sub SaveWithoutValidationNeeded(usercontrol As UserControl)
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

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco11PruefungWiederholbarkeitBelastung))



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
    Protected Overrides Sub UpdateNeeded(UserControl As UserControl)
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

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
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

    Protected Overrides Sub VersendenNeeded(TargetUserControl As UserControl)


        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)
            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                '  objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()
                UeberschreibePruefungsobjekte()

                'erzeuegn eines Server Objektes auf basis des aktuellen DS
                objServerEichprozess = clsClientServerConversionFunctions.CopyObjectProperties(objServerEichprozess, objEichprozess, clsClientServerConversionFunctions.enuModus.RHEWASendetAnClient)
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
#End Region

End Class
