Public Class uco13PruefungRollendeLasten
    Inherits ucoContent

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
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungRollendeLasten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungRollendelasten
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten

        If My.Settings.AktuelleSprache.ToLower = "en" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_GB
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_GB
        ElseIf My.Settings.AktuelleSprache = "de" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_DE
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_DE
        ElseIf My.Settings.AktuelleSprache = "pl" Then
            PictureBox2.Image = My.Resources.Verschiebeweg_PL
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_PL
        Else
            PictureBox2.Image = My.Resources.Verschiebeweg_GB
            PictureBox3.Image = My.Resources.Fahrzeug_Wenden_GB
        End If

        'daten füllen
        LoadFromDatabase()
    End Sub

#Region "Bereich Links"

    Private Sub RadTextBoxControlLastLinks1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlLastRechts3.Validating, RadTextBoxControlLastRechts2.Validating, RadTextBoxControlLastRechts1.Validating, RadTextBoxControllastLinks3.Validating, RadTextBoxControlLastLinks2.Validating, RadTextBoxControlLastLinks1.Validating, RadTextBoxControlAnzeigeRechts3.Validating, RadTextBoxControlAnzeigeRechts2.Validating, RadTextBoxControlAnzeigeRechts1.Validating, RadTextBoxControlAnzeigeLinks3.Validating, RadTextBoxControlAnzeigeLinks2.Validating, RadTextBoxControlAnzeigeLinks1.Validating

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

    Private Sub BerechneFehlerundEFGLinks(Optional Sender As Telerik.WinControls.UI.RadTextBoxControl = Nothing)
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        Try
            'zuweisen der last an alle Felder
            If Sender.Equals(RadTextBoxControlLastLinks1) Then
                RadTextBoxControlLastLinks1.Text = RadTextBoxControlLastLinks1.Text
                RadTextBoxControlLastLinks2.Text = RadTextBoxControlLastLinks1.Text
                RadTextBoxControllastLinks3.Text = RadTextBoxControlLastLinks1.Text
            ElseIf Sender.Equals(RadTextBoxControlLastLinks2) Then
                RadTextBoxControlLastLinks1.Text = RadTextBoxControlLastLinks2.Text
                RadTextBoxControlLastLinks2.Text = RadTextBoxControlLastLinks2.Text
                RadTextBoxControllastLinks3.Text = RadTextBoxControlLastLinks2.Text
            ElseIf Sender.Equals(RadTextBoxControllastLinks3) Then
                RadTextBoxControlLastLinks1.Text = RadTextBoxControllastLinks3.Text
                RadTextBoxControlLastLinks2.Text = RadTextBoxControllastLinks3.Text
                RadTextBoxControllastLinks3.Text = RadTextBoxControllastLinks3.Text
            End If

        Catch e As Exception
        End Try

        'EFG

        Try
            'nimm Max von den Bereichen
            'wenn die Last > ist als  2000e-3000e dann nehme EFG2000e sonst EFG500e
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If

                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    If CDec(RadTextBoxControlLastLinks1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertLinks.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
            End Select
        Catch e As Exception
        End Try

        'neu berechnen der Fehler und EFG
        Try
            RadTextBoxControlFehlerLinks1.Text = CDec(RadTextBoxControlAnzeigeLinks1.Text) - CDec(RadTextBoxControlLastLinks1.Text)
            If CDec(RadTextBoxControlAnzeigeLinks1.Text) > CDec(RadTextBoxControlLastLinks1.Text) + CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks1.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeLinks1.Text) < CDec(RadTextBoxControlLastLinks1.Text) - CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks1.Checked = False
            Else
                RadCheckBoxAuffahrtLinks1.Checked = True
            End If
        Catch ex As Exception
        End Try


        'fehler
        Try
            RadTextBoxControlFehlerLinks2.Text = CDec(RadTextBoxControlAnzeigeLinks2.Text) - CDec(RadTextBoxControlLastLinks2.Text)
            If CDec(RadTextBoxControlAnzeigeLinks2.Text) > CDec(RadTextBoxControlLastLinks2.Text) + CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks2.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeLinks2.Text) < CDec(RadTextBoxControlLastLinks2.Text) - CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks2.Checked = False
            Else
                RadCheckBoxAuffahrtLinks2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlFehlerLinks3.Text = CDec(RadTextBoxControlAnzeigeLinks3.Text) - CDec(RadTextBoxControllastLinks3.Text)
            If CDec(RadTextBoxControlAnzeigeLinks3.Text) > CDec(RadTextBoxControllastLinks3.Text) + CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks3.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeLinks3.Text) < CDec(RadTextBoxControllastLinks3.Text) - CDec(lblEFGWertLinks.Text) Then
                RadCheckBoxAuffahrtLinks3.Checked = False
            Else
                RadCheckBoxAuffahrtLinks3.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub
    Private Sub BerechneFehlerundEFGRechts(Optional sender As Telerik.WinControls.UI.RadTextBoxControl = Nothing)

        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True


        Try
            'zuweisen der last an alle Felder
            If Sender.Equals(RadTextBoxControlLastRechts1) Then
                RadTextBoxControlLastRechts1.Text = RadTextBoxControlLastRechts1.Text
                RadTextBoxControlLastRechts2.Text = RadTextBoxControlLastRechts1.Text
                RadTextBoxControlLastRechts3.Text = RadTextBoxControlLastRechts1.Text
            ElseIf Sender.Equals(RadTextBoxControlLastRechts2) Then
                RadTextBoxControlLastRechts1.Text = RadTextBoxControlLastRechts2.Text
                RadTextBoxControlLastRechts2.Text = RadTextBoxControlLastRechts2.Text
                RadTextBoxControlLastRechts3.Text = RadTextBoxControlLastRechts2.Text
            ElseIf Sender.Equals(RadTextBoxControlLastRechts3) Then
                RadTextBoxControlLastRechts1.Text = RadTextBoxControlLastRechts3.Text
                RadTextBoxControlLastRechts2.Text = RadTextBoxControlLastRechts3.Text
                RadTextBoxControlLastRechts3.Text = RadTextBoxControlLastRechts3.Text
            End If

        Catch e As Exception
        End Try

        'neu berechnen der Fehler und EFG

        'EFG

        Try
            'nimm Max von den Bereichen
            'wenn die Last > ist als  2000e-3000e dann nehme EFG2000e sonst EFG500e
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    If CDec(RadTextBoxControlLastRechts1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    If CDec(RadTextBoxControlLastRechts1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If

                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    If CDec(RadTextBoxControlLastRechts1.Text) > Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
                    Else
                        lblEFGWertRechts.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
                    End If
            End Select

        Catch e As Exception
        End Try

        'fehler

        Try
            RadTextBoxControlFehlerRechts1.Text = CDec(RadTextBoxControlAnzeigeRechts1.Text) - CDec(RadTextBoxControlLastRechts1.Text)
            If CDec(RadTextBoxControlAnzeigeRechts1.Text) > CDec(RadTextBoxControlLastRechts1.Text) + CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts1.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeRechts1.Text) < CDec(RadTextBoxControlLastRechts1.Text) - CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts1.Checked = False
            Else
                RadCheckBoxlblAuffahrtRechts1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlFehlerRechts2.Text = CDec(RadTextBoxControlAnzeigeRechts2.Text) - CDec(RadTextBoxControlLastRechts2.Text)
            If CDec(RadTextBoxControlAnzeigeRechts2.Text) > CDec(RadTextBoxControlLastRechts2.Text) + CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts2.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeRechts2.Text) < CDec(RadTextBoxControlLastRechts2.Text) - CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts2.Checked = False
            Else
                RadCheckBoxlblAuffahrtRechts2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlFehlerRechts3.Text = CDec(RadTextBoxControlAnzeigeRechts3.Text) - CDec(RadTextBoxControlLastRechts3.Text)
            If CDec(RadTextBoxControlAnzeigeRechts3.Text) > CDec(RadTextBoxControlLastRechts3.Text) + CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts3.Checked = False
            ElseIf CDec(RadTextBoxControlAnzeigeRechts3.Text) < CDec(RadTextBoxControlLastRechts3.Text) - CDec(lblEFGWertRechts.Text) Then
                RadCheckBoxlblAuffahrtRechts3.Checked = False
            Else
                RadCheckBoxlblAuffahrtRechts3.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub


    ''' <summary>
    ''' wenn sich einer der Anzeige Werte ändert, müssen die Fehler und EFG neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlAnzeigeLinks1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeigeLinks3.TextChanged, RadTextBoxControlAnzeigeLinks2.TextChanged, RadTextBoxControlAnzeigeLinks1.TextChanged, RadTextBoxControlLastLinks1.TextChanged, RadTextBoxControlLastLinks2.TextChanged, RadTextBoxControllastLinks3.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            BerechneFehlerundEFGLinks(sender)
        Catch ex As Exception
        End Try
    End Sub


#End Region

#Region "bereich Rechts"
    ''' <summary>
    ''' wenn sich einer der Anzeige Werte ändert, müssen die Fehler und EFG neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlAnzeigeRechts1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeigeRechts3.TextChanged, RadTextBoxControlAnzeigeRechts2.TextChanged, RadTextBoxControlAnzeigeRechts1.TextChanged, _
          RadTextBoxControlLastRechts1.TextChanged, RadTextBoxControlLastRechts2.TextChanged, RadTextBoxControlLastRechts3.TextChanged
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

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungRollendeLasten Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungRollendeLasten = query.ToList

            End Using
        Else
            _ListPruefungRollendeLasten.Clear()

            Try
                For Each obj In objEichprozess.Eichprotokoll.PruefungRollendeLasten
                    obj.Eichprotokoll = objEichprozess.Eichprotokoll

                    _ListPruefungRollendeLasten.Add(obj)
                Next
            Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
                Using context As New EichsoftwareClientdatabaseEntities1
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query = From a In context.PruefungRollendeLasten Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungRollendeLasten = query.ToList

                End Using
            End Try

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
            RadCheckBoxlblAuffahrtRechts1.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = "2" And o.AuffahrtSeite = "rechts").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastRechts2.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeRechts2.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerRechts2.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxlblAuffahrtRechts2.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungRollendeLasten = Nothing
        _currentObjPruefungRollendeLasten = (From o In _ListPruefungRollendeLasten Where o.Belastungsstelle = "3" And o.AuffahrtSeite = "rechts").FirstOrDefault
        If Not _currentObjPruefungRollendeLasten Is Nothing Then
            RadTextBoxControlLastRechts3.Text = _currentObjPruefungRollendeLasten.Last
            RadTextBoxControlAnzeigeRechts3.Text = _currentObjPruefungRollendeLasten.Anzeige
            RadTextBoxControlFehlerRechts3.Text = _currentObjPruefungRollendeLasten.Fehler
            RadCheckBoxlblAuffahrtRechts3.Checked = _currentObjPruefungRollendeLasten.EFG
        End If

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
                PObjPruefung.EFG = RadCheckBoxlblAuffahrtRechts1.Checked
                PObjPruefung.EFGExtra = lblEFGWertRechts.Text
            End If
            If PObjPruefung.Belastungsstelle = "2" Then
                PObjPruefung.Last = RadTextBoxControlLastRechts2.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeRechts2.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerRechts2.Text
                PObjPruefung.EFG = RadCheckBoxlblAuffahrtRechts2.Checked
                PObjPruefung.EFGExtra = lblEFGWertRechts.Text
            End If
            If PObjPruefung.Belastungsstelle = "3" Then
                PObjPruefung.Last = RadTextBoxControlLastRechts3.Text
                PObjPruefung.Anzeige = RadTextBoxControlAnzeigeRechts3.Text
                PObjPruefung.Fehler = RadTextBoxControlFehlerRechts3.Text
                PObjPruefung.EFG = RadCheckBoxlblAuffahrtRechts3.Checked
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


    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaveing = False

        If RadCheckBoxAuffahrtLinks1.Checked = False And RadCheckBoxAuffahrtLinks1.Visible = True Or _
            RadCheckBoxAuffahrtLinks2.Checked = False And RadCheckBoxAuffahrtLinks2.Visible = True Or _
            RadCheckBoxAuffahrtLinks3.Checked = False And RadCheckBoxAuffahrtLinks3.Visible = True Then
            AbortSaveing = True

        End If


        If RadCheckBoxlblAuffahrtRechts1.Checked = False And RadCheckBoxlblAuffahrtRechts1.Visible = True Or _
              RadCheckBoxlblAuffahrtRechts2.Checked = False And RadCheckBoxlblAuffahrtRechts2.Visible = True Or _
              RadCheckBoxlblAuffahrtRechts3.Checked = False And RadCheckBoxlblAuffahrtRechts2.Visible = True Then
            AbortSaveing = True
        End If



        'fehlermeldung anzeigen bei falscher validierung
        Return Me.ShowValidationErrorBox()
    End Function

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If


            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
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


            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                If DialogModus = enuDialogModus.lesend Then
                    UpdateObject()
                    ParentFormular.CurrentEichprozess = objEichprozess
                    Exit Sub
                End If

                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.Include("Eichprotokoll").FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
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
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
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
    Protected Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungRollendeLasten)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungRollendelasten
            '   FillControls()
            LoadFromDatabase()
        End If
    End Sub

#End Region


    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco13PruefungRollendeLasten))

        Me.lblAnzeigeLinks.Text = resources.GetString("RadLabelDisplay.Text")
        Me.lblAnzeigeRechts.Text = resources.GetString("lblAnzeigeRechts.Text")
        Me.lblAuffahrtLinks1.Text = resources.GetString("lblAuffahrtLinks1.Text")
        Me.lblAuffahrtLinks2.Text = resources.GetString("lblAuffahrtLinks2.Text")
        Me.lblAuffahrtLinks3.Text = resources.GetString("lblAuffahrtLinks3.Text")
        Me.lblAuffahrtRechts1.Text = resources.GetString("lblAuffahrtRechts1.Text")
        Me.lblAuffahrtRechts2.Text = resources.GetString("lblAuffahrtRechts2.Text")
        Me.lblAuffahrtRechts3.Text = resources.GetString("lblAuffahrtRechts3.Text")
        Me.lblBelastungsstelleLinks.Text = resources.GetString("lblBelastungsstelleLinks.Text")
        Me.lblBelastungsstelleRechts.Text = resources.GetString("lblBelastungsstelleRechts.Text")
        Me.lblEFGLinks.Text = resources.GetString("lblEFGLinks.Text")
        Me.lblEFGRechts.Text = resources.GetString("lblEFGRechts.Text")
        Me.lblEFGWertLinks.Text = resources.GetString("lblEFGWertLinks.Text")
        Me.lblEFGWertRechts.Text = resources.GetString("lblEFGWertRechts.Text")
        Me.lblEichfehlergrenze.Text = resources.GetString("lblEichfehlergrenze.Text")
        Me.lblFehlerLinks.Text = resources.GetString("lblFehlerLinks.Text")
        Me.lblFehlerRechts.Text = resources.GetString("lblFehlerRechts.Text")
        Me.lblLastLinks.Text = resources.GetString("lblLastLinks.Text")
        Me.lblLastRechts.Text = resources.GetString("lblLastRechts.Text")



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

    Private Sub RadCheckBoxAuffahrtLinks1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxlblAuffahrtRechts3.MouseClick, RadCheckBoxlblAuffahrtRechts2.MouseClick, RadCheckBoxlblAuffahrtRechts1.MouseClick, RadCheckBoxAuffahrtLinks3.MouseClick, RadCheckBoxAuffahrtLinks2.MouseClick, RadCheckBoxAuffahrtLinks1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub


    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()
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
                'objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

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
End Class
