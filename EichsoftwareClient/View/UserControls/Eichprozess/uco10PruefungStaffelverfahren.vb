' 13.05.2014 hill EichsoftwareClient uco10PruefungStaffelverfahren.vb
Imports System

Public Class uco10PruefungStaffelverfahren
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    Private _ListPruefungStaffelverfahrenNormallast As New List(Of PruefungStaffelverfahrenNormallast)
    Private _ListPruefungStaffelverfahrenErsatzlast As New List(Of PruefungStaffelverfahrenErsatzlast)

    Private AnzahlBereiche As Integer
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
    End Sub

#End Region

#Region "Events"
    ''' <summary>
    ''' Validations the needed.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Overrides Function ValidationNeeded() As Boolean
        LoadFromDatabase()
        Return ValidateControls()
    End Function
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast

        'daten füllen
        LoadFromDatabase()

    End Sub

    ''' <summary>
    ''' pflichtfelder rot umranden bei falschen oder fehlenden eingaben
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControl_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles _
    RadTextBoxControlStaffel5Bereich3Last2.Validating,
    RadTextBoxControlStaffel5Bereich3Last1.Validating,
    RadTextBoxControlStaffel5Bereich3Anzeige1.Validating,
    RadTextBoxControlStaffel5Bereich2Last2.Validating,
    RadTextBoxControlStaffel5Bereich2Last1.Validating,
    RadTextBoxControlStaffel5Bereich2Anzeige1.Validating,
    RadTextBoxControlStaffel5Bereich1Last2.Validating,
    RadTextBoxControlStaffel5Bereich1Last1.Validating,
    RadTextBoxControlStaffel5Bereich1Anzeige1.Validating,
    RadTextBoxControlStaffel4Bereich3Last2.Validating,
    RadTextBoxControlStaffel4Bereich3Last1.Validating,
    RadTextBoxControlStaffel4Bereich3Anzeige1.Validating,
    RadTextBoxControlStaffel4Bereich2Last2.Validating,
    RadTextBoxControlStaffel4Bereich2Last1.Validating,
    RadTextBoxControlStaffel4Bereich2Anzeige1.Validating,
    RadTextBoxControlStaffel4Bereich1Last2.Validating,
    RadTextBoxControlStaffel4Bereich1Last1.Validating,
    RadTextBoxControlStaffel4Bereich1Anzeige1.Validating,
    RadTextBoxControlStaffel3Bereich3Last2.Validating,
    RadTextBoxControlStaffel3Bereich3Last1.Validating,
    RadTextBoxControlStaffel3Bereich3Anzeige1.Validating,
    RadTextBoxControlStaffel3Bereich2Last2.Validating,
    RadTextBoxControlStaffel3Bereich2Last1.Validating,
    RadTextBoxControlStaffel3Bereich2Anzeige1.Validating,
    RadTextBoxControlStaffel3Bereich1Last2.Validating,
    RadTextBoxControlStaffel3Bereich1Last1.Validating,
    RadTextBoxControlStaffel3Bereich1Anzeige1.Validating,
    RadTextBoxControlStaffel2Bereich3Last2.Validating,
    RadTextBoxControlStaffel2Bereich3Last1.Validating,
    RadTextBoxControlStaffel2Bereich3Anzeige1.Validating,
    RadTextBoxControlStaffel2Bereich2Last2.Validating,
    RadTextBoxControlStaffel2Bereich2Last1.Validating,
    RadTextBoxControlStaffel2Bereich2Anzeige1.Validating,
    RadTextBoxControlStaffel2Bereich1Last2.Validating,
    RadTextBoxControlStaffel2Bereich1Last1.Validating,
    RadTextBoxControlStaffel2Bereich1Anzeige1.Validating,
    RadTextBoxControlStaffel1Bereich3Last4.Validating,
    RadTextBoxControlStaffel1Bereich3Last3.Validating,
    RadTextBoxControlStaffel1Bereich3Last1.Validating,
    RadTextBoxControlStaffel1Bereich3Anzeige4.Validating,
    RadTextBoxControlStaffel1Bereich3Anzeige3.Validating,
    RadTextBoxControlStaffel1Bereich3Anzeige1.Validating,
    RadTextBoxControlStaffel1Bereich2Last4.Validating,
    RadTextBoxControlStaffel1Bereich2Last3.Validating,
    RadTextBoxControlStaffel1Bereich2Last1.Validating,
    RadTextBoxControlStaffel1Bereich2Anzeige4.Validating,
    RadTextBoxControlStaffel1Bereich2Anzeige3.Validating,
    RadTextBoxControlStaffel1Bereich2Anzeige1.Validating,
    RadTextBoxControlStaffel1Bereich1Last4.Validating,
    RadTextBoxControlStaffel1Bereich1Last3.Validating,
    RadTextBoxControlStaffel1Bereich1Last1.Validating,
    RadTextBoxControlStaffel1Bereich1Anzeige4.Validating,
    RadTextBoxControlStaffel1Bereich1Anzeige3.Validating,
    RadTextBoxControlStaffel1Bereich1Anzeige1.Validating,
    RadTextBoxControlStaffel1Bereich1Last2.Validating,
    RadTextBoxControlStaffel1Bereich1Fehler2.Validating,
    RadTextBoxControlStaffel1Bereich1Anzeige2.Validating
        Try
            Dim result As Decimal
            If Not sender.readonly = True Then

                'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
                If CType(sender, Telerik.WinControls.UI.RadTextBox).Text.Equals("") Then
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
                    Exit Sub
                End If

                'versuchen ob der Text in eine Zahl konvertiert werden kann
                If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBox).Text, result) Then
                    e.Cancel = True
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                    System.Media.SystemSounds.Exclamation.Play()

                Else 'rahmen zurücksetzen
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Methods"
    Protected Friend Overrides Sub LoadFromDatabase()
        SuspendLayout()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungStaffelverfahrenNormallast = query.ToList

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query2 = From a In context.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungStaffelverfahrenErsatzlast = query2.ToList

            End Using

        Else
            _ListPruefungStaffelverfahrenNormallast.Clear()
            _ListPruefungStaffelverfahrenErsatzlast.Clear()
            Try
                For Each obj In objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast
                    obj.Eichprotokoll = objEichprozess.Eichprotokoll

                    _ListPruefungStaffelverfahrenNormallast.Add(obj)
                Next
                For Each obj In objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast
                    obj.Eichprotokoll = objEichprozess.Eichprotokoll

                    _ListPruefungStaffelverfahrenErsatzlast.Add(obj)
                Next
            Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
                Using context As New EichsoftwareClientdatabaseEntities1
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen

                    Dim query = From a In context.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungStaffelverfahrenNormallast = query.ToList

                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query2 = From a In context.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungStaffelverfahrenErsatzlast = query2.ToList
                End Using
            End Try

        End If

        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            DisableControls(RadGroupBoxStaffel1Bereich1)
            DisableControls(RadGroupBoxStaffel1Bereich2)
            DisableControls(RadGroupBoxStaffel1Bereich3)

            DisableControls(RadGroupBoxStaffel2Bereich1)
            DisableControls(RadGroupBoxStaffel2Bereich2)
            DisableControls(RadGroupBoxStaffel2Bereich3)
            DisableControls(RadGroupBoxStaffel3Bereich1)
            DisableControls(RadGroupBoxStaffel3Bereich2)
            DisableControls(RadGroupBoxStaffel3Bereich3)
            DisableControls(RadGroupBoxStaffel4Bereich1)
            DisableControls(RadGroupBoxStaffel4Bereich2)
            DisableControls(RadGroupBoxStaffel4Bereich3)
            DisableControls(RadGroupBoxStaffel5Bereich1)
            DisableControls(RadGroupBoxStaffel5Bereich2)
            DisableControls(RadGroupBoxStaffel5Bereich3)

        End If

        'events abbrechen
        _suspendEvents = False
        ResumeLayout()
    End Sub

    ''' <summary>
    ''' Entsprechend der Vorgaben die Anzahl der Nullstellen für alle erforderlichen Steuerelemente setzen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetzeNullstellen()
        Try
            'nullstellen maske für EFG erzwingen
            For Each Control In RadScrollablePanel1.PanelContainer.Controls
                If TypeOf Control Is Telerik.WinControls.UI.RadGroupBox Then
                    If CType(Control, Telerik.WinControls.UI.RadGroupBox).Visible = True Then
                        For Each control2 In CType(Control, Telerik.WinControls.UI.RadGroupBox).Controls
                            If TypeOf control2 Is Telerik.WinControls.UI.RadGroupBox Then
                                For Each control3 In CType(control2, Telerik.WinControls.UI.RadGroupBox).Controls
                                    If TypeOf control3 Is Telerik.WinControls.UI.RadMaskedEditBox Then
                                        If CType(control3, Telerik.WinControls.UI.RadMaskedEditBox).Name.Contains("Wert") Then
                                            CType(control3, Telerik.WinControls.UI.RadMaskedEditBox).Mask = String.Format("F{0}", _intNullstellenE)
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
            Next
        Catch e As Exception
        End Try
    End Sub

    ''' <summary>
    '''    je nach Art der Waage andere Bereichsgruppen ausblenden
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VersteckeBereiche()
        'je nach Art der Waage andere Bereichsgruppen ausblenden
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            AnzahlBereiche = 1
            RadGroupBoxStaffel1Bereich2.Visible = False
            RadGroupBoxStaffel1Bereich3.Visible = False
            RadGroupBoxStaffel2Bereich2.Visible = False
            RadGroupBoxStaffel2Bereich3.Visible = False
            RadGroupBoxStaffel3Bereich2.Visible = False
            RadGroupBoxStaffel3Bereich3.Visible = False
            RadGroupBoxStaffel4Bereich2.Visible = False
            RadGroupBoxStaffel4Bereich3.Visible = False
            RadGroupBoxStaffel5Bereich2.Visible = False
            RadGroupBoxStaffel5Bereich3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            AnzahlBereiche = 2
            RadGroupBoxStaffel1Bereich2.Visible = True
            RadGroupBoxStaffel1Bereich3.Visible = False
            RadGroupBoxStaffel2Bereich2.Visible = True
            RadGroupBoxStaffel2Bereich3.Visible = False
            RadGroupBoxStaffel3Bereich2.Visible = True
            RadGroupBoxStaffel3Bereich3.Visible = False
            RadGroupBoxStaffel4Bereich2.Visible = True
            RadGroupBoxStaffel4Bereich3.Visible = False
            RadGroupBoxStaffel5Bereich2.Visible = True
            RadGroupBoxStaffel5Bereich3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            AnzahlBereiche = 3
            RadGroupBoxStaffel1Bereich2.Visible = True
            RadGroupBoxStaffel1Bereich3.Visible = True
            RadGroupBoxStaffel2Bereich2.Visible = True
            RadGroupBoxStaffel2Bereich3.Visible = True
            RadGroupBoxStaffel3Bereich2.Visible = True
            RadGroupBoxStaffel3Bereich3.Visible = True
            RadGroupBoxStaffel4Bereich2.Visible = True
            RadGroupBoxStaffel4Bereich3.Visible = True
            RadGroupBoxStaffel5Bereich2.Visible = True
            RadGroupBoxStaffel5Bereich3.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        HoleNullstellen()
        SetzeNullstellen()

        'je nach Art der Waage andere Bereichsgruppen ausblenden
        VersteckeBereiche()

        'füllen der berechnenten Steuerelemente
        LadeStaffeln()

        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlStaffel1Bereich1Anzeige1.Focus()
    End Sub

    ''' <summary>
    ''' Methode welche Steuerelemente mit Werten aus übergebener Staffel und Bereich füllt. Lädt die Daten wenn vorhanden aus DB ansonsten werden Standardformeln verwendet
    ''' </summary>
    ''' <param name="Staffel"></param>
    ''' <param name="Bereich"></param>
    ''' <remarks></remarks>
    Private Sub LadeStaffel(ByVal Staffel As String, ByVal Bereich As String)
        Try
            If Staffel = "1" Then
                Dim _currentObjPruefungStaffelverfahrenNormallast As PruefungStaffelverfahrenNormallast

                _currentObjPruefungStaffelverfahrenNormallast = Nothing
                _currentObjPruefungStaffelverfahrenNormallast = (From o In _ListPruefungStaffelverfahrenNormallast Where o.Bereich = CStr(Bereich) And o.Staffel = CStr(Staffel)).FirstOrDefault

                If Not _currentObjPruefungStaffelverfahrenNormallast Is Nothing Then

                    For messpunkt As Integer = 1 To 7
                        Dim Last As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), CInt(messpunkt)))
                        Dim Anzeige As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), CInt(messpunkt)))
                        Dim Fehler As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), CInt(messpunkt)))
                        Dim EFG As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), CInt(messpunkt)))

                        If messpunkt <= 4 Then
                            If Not Last Is Nothing Then
                                Last.Text = _currentObjPruefungStaffelverfahrenNormallast.GetType().GetProperty(String.Format("NormalLast_Last_{0}", messpunkt)).GetValue(_currentObjPruefungStaffelverfahrenNormallast, Nothing)
                            End If

                            If Not Anzeige Is Nothing Then
                                Anzeige.Text = _currentObjPruefungStaffelverfahrenNormallast.GetType().GetProperty(String.Format("NormalLast_Anzeige_{0}", messpunkt)).GetValue(_currentObjPruefungStaffelverfahrenNormallast, Nothing)
                            End If

                            If Not Fehler Is Nothing Then
                                Fehler.Text = _currentObjPruefungStaffelverfahrenNormallast.GetType().GetProperty(String.Format("NormalLast_Fehler_{0}", messpunkt)).GetValue(_currentObjPruefungStaffelverfahrenNormallast, Nothing)
                            End If

                            If Not EFG Is Nothing Then
                                EFG.Text = _currentObjPruefungStaffelverfahrenNormallast.GetType().GetProperty(String.Format("NormalLast_EFG_{0}", messpunkt)).GetValue(_currentObjPruefungStaffelverfahrenNormallast, Nothing)
                            End If

                        ElseIf messpunkt = 5 Then
                            If Not Fehler Is Nothing Then
                                Fehler.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_Fehler
                            End If
                            If Not EFG Is Nothing Then
                                EFG.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_EFG
                            End If
                        ElseIf messpunkt = 6 Then
                            If Not Fehler Is Nothing Then
                                Fehler.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_Fehler
                            End If
                            If Not EFG Is Nothing Then
                                EFG.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_EFG
                            End If
                        ElseIf messpunkt = 7 Then
                            If Not Fehler Is Nothing Then
                                Fehler.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_Fehler
                            End If
                            If Not EFG Is Nothing Then
                                EFG.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_EFG
                            End If
                        End If

                    Next

                Else 'Bereich wurde nicht gefunden => DB Ojekt ist leer. also mit Standardwerten füllen

                    ' standardwerte eintragen
                    If Bereich = 1 AndAlso AnzahlBereiche >= 1 Then 'die Anzahlbereich Abfrage sorgt dafür, dass wenn es z.b. keinen 3. bereich gibt, dieser auch nicht gefüllt wird.
                        RadTextBoxControlStaffel1Bereich1Last1.Text = "0"
                        RadTextBoxControlStaffel1Bereich1Last2.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1

                        lblStaffel1Bereich1EFGWert5.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                        'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                        If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                            RadTextBoxControlStaffel1Bereich1Last3.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                        Else
                            RadTextBoxControlStaffel1Bereich1Last3.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                        End If
                        RadTextBoxControlStaffel1Bereich1Last4.Text = "0"
                    ElseIf Bereich = 2 AndAlso AnzahlBereiche >= 2 Then
                        RadTextBoxControlStaffel1Bereich2Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2

                        'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                        If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                            RadTextBoxControlStaffel1Bereich2Last3.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                        Else
                            RadTextBoxControlStaffel1Bereich2Last3.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                        End If

                        RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
                        RadTextBoxControlStaffel1Bereich2Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2

                        lblStaffel1Bereich2EFGWert5.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                    ElseIf Bereich = 3 AndAlso AnzahlBereiche >= 3 Then
                        RadTextBoxControlStaffel1Bereich3Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
                        'last 2 Berechnen
                        'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                        If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                            RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                        Else
                            RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                        End If
                        RadTextBoxControlStaffel1Bereich3Last4.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
                        RadTextBoxControlStaffel1Bereich3Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3

                        lblStaffel1Bereich3EFGWert5.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
                    End If

                End If

            Else 'staffel 2- 5 => andere Berechnungsroutinen
                Dim _currentObjPruefungStaffelverfahrenErsatzlast As PruefungStaffelverfahrenErsatzlast

                _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
                _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = CStr(Bereich) And o.Staffel = CStr(Staffel)).FirstOrDefault

                Dim Last1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 1))
                Dim Last2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 2))
                Dim Last3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 3))
                Dim Last4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 4))

                Dim Anzeige1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 1))
                Dim Anzeige3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 3))
                Dim Anzeige4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 4))

                Dim Fehler5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 5))
                Dim Fehler6 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 6))
                Dim Fehler7 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 7))

                Dim EFG5 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 5))
                Dim EFG6 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 6))
                Dim EFG7 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 7))

                If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                    Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                    Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                    Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                    Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                    Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                    Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                    Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                    Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                    Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                    Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                    EFG5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                    EFG6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                    EFG7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG
                Else 'standardweret eintragen, wenn DB Objekt nothing ist
                    Dim eichwert As Decimal
                    'eichwert auslesen
                    If Bereich = "1" AndAlso AnzahlBereiche >= 1 Then
                        eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                    ElseIf Bereich = "2" AndAlso AnzahlBereiche >= 2 Then
                        eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
                    ElseIf Bereich = "3" AndAlso AnzahlBereiche >= 3 Then
                        eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
                    End If
                    'EFG Normallast
                    EFG5.Text = Math.Round(eichwert, _intNullstellenE) / 5
                    'EFG Differenz der Staffel
                    'EFG differenz der Waage
                    '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                    If IsNumeric(Last3.Text) AndAlso IsNumeric(eichwert) Then
                        If CDec(Last3.Text) < Math.Round(CDec(eichwert * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                            EFG6.Text = Math.Round(CDec(eichwert), _intNullstellenE)
                            EFG7.Text = Math.Round(CDec(eichwert), _intNullstellenE)
                        Else
                            EFG6.Text = Math.Round(CDec(eichwert), _intNullstellenE) * 1.5
                            EFG7.Text = Math.Round(CDec(eichwert), _intNullstellenE) * 1.5
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' lädt alle Staffeln
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LadeStaffeln()
        For Staffel As Integer = 1 To 5
            For Bereich As Integer = 1 To 3
                LadeStaffel(Staffel, Bereich)
            Next
        Next
    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungStaffelverfahrenNormallast
                Dim objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = obj.ID)
                If Not objPruefung Is Nothing Then
                    'in lokaler DB gucken
                    UpdatePruefungsObject(objPruefung)
                Else 'es handelt sich um eine Serverobjekt im => Korrekturmodus
                    If DialogModus = enuDialogModus.korrigierend Then
                        UpdatePruefungsObject(obj)
                    End If
                End If
            Next

            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungStaffelverfahrenErsatzlast
                Dim objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = obj.ID)
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
        objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Clear()
        For Each obj In _ListPruefungStaffelverfahrenErsatzlast
            objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(obj)
        Next
        objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Clear()
        For Each obj In _ListPruefungStaffelverfahrenNormallast
            objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(obj)
        Next
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungStaffelverfahrenNormallast)
        Dim Staffel As String = PObjPruefung.Staffel
        Dim Bereich As String = PObjPruefung.Bereich

        Dim Last1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim Last2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 2))
        Dim Last3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim Last4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim Anzeige1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim Anzeige2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 2))
        Dim Anzeige3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim Anzeige4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim Fehler1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim Fehler2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 2))
        Dim Fehler3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim Fehler4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim Fehler5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 5))
        Dim Fehler6 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 6))
        Dim Fehler7 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 7))
        Dim EFG1 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim EFG2 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 2))
        Dim EFG3 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim EFG4 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim EFG5 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 5))
        Dim EFG6 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 6))
        Dim EFG7 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 7))

        PObjPruefung.NormalLast_Last_1 = Last1.Text

        If Bereich = "1" Then 'diese Felder gibt es nur im Bereich 1 weil dort zustäzlich gegen 0 geprüft werden muss
            PObjPruefung.NormalLast_Last_2 = Last2.Text
            PObjPruefung.NormalLast_Anzeige_2 = Anzeige2.Text
            PObjPruefung.NormalLast_Fehler_2 = Fehler2.Text
            PObjPruefung.NormalLast_EFG_2 = EFG2.Text
        Else
            PObjPruefung.NormalLast_Last_2 = 0
            PObjPruefung.NormalLast_Anzeige_2 = 0
            PObjPruefung.NormalLast_Fehler_2 = 0
            PObjPruefung.NormalLast_EFG_2 = 0
        End If

        PObjPruefung.NormalLast_Last_3 = Last3.Text
        PObjPruefung.NormalLast_Last_4 = Last4.Text
        PObjPruefung.NormalLast_Anzeige_1 = Anzeige1.Text

        PObjPruefung.NormalLast_Anzeige_3 = Anzeige3.Text
        PObjPruefung.NormalLast_Anzeige_4 = Anzeige4.Text
        PObjPruefung.NormalLast_Fehler_1 = Fehler1.Text
        PObjPruefung.NormalLast_Fehler_3 = Fehler3.Text
        PObjPruefung.NormalLast_Fehler_4 = Fehler4.Text
        PObjPruefung.NormalLast_EFG_1 = EFG1.Text
        PObjPruefung.NormalLast_EFG_3 = EFG3.Text
        PObjPruefung.NormalLast_EFG_4 = EFG4.Text
        PObjPruefung.DifferenzAnzeigewerte_Fehler = Fehler5.Text
        PObjPruefung.DifferenzAnzeigewerte_EFG = EFG5.Text
        PObjPruefung.MessabweichungStaffel_Fehler = Fehler6.Text
        PObjPruefung.MessabweichungStaffel_EFG = EFG6.Text
        PObjPruefung.MessabweichungWaage_Fehler = Fehler7.Text
        PObjPruefung.MessabweichungWaage_EFG = EFG7.Text
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungStaffelverfahrenErsatzlast)
        Dim Staffel As String = PObjPruefung.Staffel
        Dim Bereich As String = PObjPruefung.Bereich

        'suchen der Steuerelemente
        Dim Last1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim Last2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 2))
        Dim Last3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim Last4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim Anzeige1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 1))
        Dim Anzeige3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 3))
        Dim Anzeige4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 4))
        Dim Fehler5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 5))
        Dim Fehler6 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 6))
        Dim Fehler7 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 7))
        Dim EFG5 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 5))
        Dim EFG6 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 6))
        Dim EFG7 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 7))

        'überschreiben der Objekteigenschaften
        PObjPruefung.Ersatzlast_Soll = Last1.Text
        PObjPruefung.Ersatzlast_Ist = Anzeige1.Text
        PObjPruefung.ZusaetzlicheErsatzlast_Soll = Last2.Text
        PObjPruefung.ErsatzUndNormallast_Soll = Last3.Text
        PObjPruefung.ErsatzUndNormallast_Ist = Anzeige3.Text
        PObjPruefung.Ersatzlast2_Soll = Last4.Text
        PObjPruefung.Ersatzlast2_Ist = Anzeige4.Text
        PObjPruefung.DifferenzAnzeigewerte_Fehler = Fehler5.Text
        PObjPruefung.DifferenzAnzeigewerte_EFG = EFG5.Text
        PObjPruefung.MessabweichungStaffel_Fehler = Fehler6.Text
        PObjPruefung.MessabweichungStaffel_EFG = EFG6.Text
        PObjPruefung.MessabweichungWaage_Fehler = Fehler7.Text
        PObjPruefung.MessabweichungWaage_EFG = EFG7.Text
    End Sub

    ''' <summary>
    ''' prüft innerhalb einer Staffel ob alle Felder ausgefüllt sind und gibt true zurück wenn dem so ist, prüft nur wenn überhaupt eintragungen vorgenommen wurden
    ''' </summary>
    ''' <param name="StaffelGroupBox"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateStaffelAusgefuellt(StaffelGroupBox As Telerik.WinControls.UI.RadGroupBox) As Boolean
        Dim returnvalue As Boolean = True
        Dim einFeldGefuellt As Boolean = False 'nur rot markieren wenn überhaupt ein Feld gefüllt ist
        For Each BereichGroupBox In StaffelGroupBox.Controls
            If CType(BereichGroupBox, Telerik.WinControls.UI.RadGroupBox).Visible = True Then 'Nur wenn der Bereich auch sichtbar ist
                For Each Control In BereichGroupBox.controls
                    Try
                        If CType(Control, Telerik.WinControls.UI.RadTextBox).ReadOnly = False And CType(Control, Telerik.WinControls.UI.RadTextBox).Enabled = True Then
                            If CType(Control, Telerik.WinControls.UI.RadTextBox).Text.Trim = "" Then
                                returnvalue = False
                                If einFeldGefuellt = True Then
                                    CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                                End If
                            Else
                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Transparent
                                einFeldGefuellt = True
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If
        Next
        Return returnvalue
    End Function

    Private Sub ValidateStaffelEFG(staffel As Integer, bereich As Integer)
        Dim Fehler7 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(staffel), CInt(bereich), 7))
        Dim EFGWertStaffel As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(staffel), CInt(bereich), 7))

        Dim decAbsoluteFehlergrenze As Decimal = 0
        If Fehler7.Text.Trim.StartsWith("-") Then
            decAbsoluteFehlergrenze = CDec(Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
        Else
            decAbsoluteFehlergrenze = CDec(Fehler7.Text)
        End If

        If decAbsoluteFehlergrenze > CDec(EFGWertStaffel.Text) Then 'eichwerte unterschritten/überschritten
            Fehler7.TextBoxElement.Border.ForeColor = Color.Red
            AbortSaving = True
        Else
            Fehler7.TextBoxElement.Border.ForeColor = Color.Transparent
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
        AbortSaving = False

        Dim intausgefuellteStaffeln As Integer = 0
        If ValidateStaffelAusgefuellt(RadGroupBoxStaffel1) Then
            intausgefuellteStaffeln = 1
            If ValidateStaffelAusgefuellt(RadGroupBoxStaffel2) Then
                intausgefuellteStaffeln = 2
                If ValidateStaffelAusgefuellt(RadGroupBoxStaffel3) Then
                    intausgefuellteStaffeln = 3
                    If ValidateStaffelAusgefuellt(RadGroupBoxStaffel4) Then
                        intausgefuellteStaffeln = 4
                        If ValidateStaffelAusgefuellt(RadGroupBoxStaffel5) Then intausgefuellteStaffeln = 5
                    End If
                End If
            End If
        Else : AbortSaving = True 'eine Staffel muss ausgefüllt sein
        End If

        If AbortSaving Then
            'fehlermeldung anzeigen bei falscher validierung
            Dim result = Me.ShowValidationErrorBox(False)

            If result = DialogResult.Yes Or result = DialogResult.Ignore Then
                Return True
            ElseIf result = DialogResult.Retry Then
                ' Ist = soll
                OverwriteIstSoll()
                'rekursiver Aufruf
                Return ValidateControls()
            Else
                Return False
            End If
        End If

        'logik zum Valideren der Eichfehlergrenzen der einzelnen Staffeln. Abhängig davon wieviele Staffeln überhaupt ausgefüllt sind
        If (intausgefuellteStaffeln >= 1) Then
            'Staffel 1
            ValidateStaffelEFG(1, 1)
            If AnzahlBereiche >= 2 Then ValidateStaffelEFG(1, 2)
            If AnzahlBereiche >= 3 Then ValidateStaffelEFG(1, 3)
        End If

        'staffel 2
        If (intausgefuellteStaffeln >= 2) Then
            ValidateStaffelEFG(2, 1)
            If AnzahlBereiche >= 2 Then ValidateStaffelEFG(2, 2)
            If AnzahlBereiche >= 3 Then ValidateStaffelEFG(2, 3)
        End If
        'staffel 3
        If (intausgefuellteStaffeln >= 3) Then
            ValidateStaffelEFG(3, 1)
            If AnzahlBereiche >= 2 Then ValidateStaffelEFG(3, 2)
            If AnzahlBereiche >= 3 Then ValidateStaffelEFG(3, 3)
        End If
        'staffel 4
        If (intausgefuellteStaffeln >= 4) Then
            ValidateStaffelEFG(4, 1)
            If AnzahlBereiche >= 2 Then ValidateStaffelEFG(4, 2)
            If AnzahlBereiche >= 3 Then ValidateStaffelEFG(4, 3)
        End If
        'staffel 5
        If (intausgefuellteStaffeln >= 5) Then
            ValidateStaffelEFG(5, 1)
            If AnzahlBereiche >= 2 Then ValidateStaffelEFG(5, 2)
            If AnzahlBereiche >= 3 Then ValidateStaffelEFG(5, 3)
        End If

        'fehlermeldung anzeigen bei falscher validierung

        If AbortSaving Then
            'fehlermeldung anzeigen bei falscher validierung
            Dim result = Me.ShowValidationErrorBox(False, My.Resources.GlobaleLokalisierung.EichfehlergrenzenNichtEingehalten)

            If result = DialogResult.Yes Or result = DialogResult.Ignore Then
                Return True
            ElseIf result = DialogResult.Retry Then
                ' Ist = soll
                OverwriteIstSoll()
                'rekursiver Aufruf
                Return ValidateControls()
            Else
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub OverwriteIstSoll()
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Events die neue Berechnungen beim Ändern von Feldinformationen erfordern"

    Private Sub BerechneStaffelBereich(ByVal Staffel As String, ByVal Bereich As String)
        Try
            Dim Last1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 1))
            Dim Last2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 2))
            Dim Last3Normallast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 3))
            Dim Last4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Last{2}", CInt(Staffel), CInt(Bereich), 4))

            Dim Anzeige1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 1))
            Dim Anzeige2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 2))
            Dim Anzeige3Normallast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 3))
            Dim Anzeige4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Anzeige{2}", CInt(Staffel), CInt(Bereich), 4))

            Dim Fehler1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 1))
            Dim Fehler2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 2))
            Dim Fehler3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 3))
            Dim Fehler4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 4))
            Dim Fehler5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 5))
            Dim Fehler6 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 6))
            Dim Fehler7 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", CInt(Staffel), CInt(Bereich), 7))

            Dim EFG1 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 1))
            Dim EFG2 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 2))
            Dim EFG3 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 3))
            Dim EFG4 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 4))
            Dim EFG5 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 5))
            Dim EFG6 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 6))
            Dim EFG7 As Telerik.WinControls.UI.RadMaskedEditBox = FindControl(String.Format("lblStaffel{0}Bereich{1}EFGWert{2}", CInt(Staffel), CInt(Bereich), 7))

            Dim Eichwert As String = ""
            If Bereich = "1" AndAlso AnzahlBereiche >= 1 Then
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
            ElseIf Bereich = "2" AndAlso AnzahlBereiche >= 2 Then
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
            ElseIf Bereich = "3" AndAlso AnzahlBereiche >= 3 Then
                Eichwert = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            End If

            If Staffel = "1" Then

                'Im Bereich 1 wird gegen den nullwert geprüft. Ab bereich 2 gegen 20e
                If Bereich = "1" Then
                    'fehler berechnen
                    Try
                        Fehler1.Text = CDec(Anzeige1.Text) - CDec(Last1.Text)
                    Catch e As InvalidCastException
                    End Try
                    Try
                        Fehler2.Text = CDec(Anzeige2.Text) - CDec(Last2.Text)
                    Catch e As InvalidCastException
                    End Try
                Else
                    'fehler berechnen
                    Try
                        Fehler1.Text = CDec(Anzeige1.Text) - CDec(Last1.Text)
                    Catch e As InvalidCastException
                    End Try
                End If

                Try
                    Fehler3.Text = CDec(Anzeige3Normallast.Text) - CDec(Last3Normallast.Text)
                Catch e As InvalidCastException
                End Try
                Try
                    Fehler4.Text = CDec(Anzeige4.Text) - CDec(Last4.Text)
                Catch e As InvalidCastException
                End Try

                '4. last entspricht der ersten Last (üblicherweise 0)
                Last4.Text = Last1.Text

                'EFG Wert 1 und 2 Berechnen

                Try
                    If CDec(Last1.Text) < Math.Round(CDec(Eichwert * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        'Im Bereich 1 wird gegen den nullwert geprüft. Ab bereich 2 gegen 20e. dort fällt EFG2 weg. stattdessen wird EFG3 genutzt
                        If Bereich = "1" Then
                            EFG1.Text = Math.Round(CDec(Eichwert) * 0.5, _intNullstellenE)
                            EFG2.Text = Math.Round(CDec(Eichwert) * 0.5, _intNullstellenE)
                        Else
                            EFG1.Text = Math.Round(CDec(Eichwert) * 0.5, _intNullstellenE)
                            EFG3.Text = Math.Round(CDec(Eichwert) * 0.5, _intNullstellenE)
                        End If
                    Else
                        'Im Bereich 1 wird gegen den nullwert geprüft. Ab bereich 2 gegen 20e. dort fällt EFG2 weg. stattdessen wird EFG3 genutzt
                        If Bereich = "1" Then
                            EFG1.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                            EFG2.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                        Else

                            EFG1.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                            EFG3.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                        End If
                    End If
                Catch ex As InvalidCastException
                End Try

                'berechnen der Differenzen
                Try
                    Fehler5.Text = CDec(Anzeige4.Text) - CDec(Anzeige1.Text)
                Catch ex As InvalidCastException
                End Try

                'messabweichung berechnen (abgeändert von Excel mappe. hier wird statt der min. normalien die eingebene Normalien Menge genommen
                Try
                    'Fehler6.Text = CDec(Anzeige3.Text) - CDec(Anzeige1.Text) - CDec(Last3.Text) //alte Rechnung

                    'weitere Anpassung nach Absprache mit Herrn Lüling und Herrn Strack:
                    'Ermittlung der Messabweichung für eine Staffel

                    'Für die Ermittlung der Messabweichung der Staffel 1 für den Bereich 1 gilt folgende Formel:
                    'Messabweichung = Anzeigewert (Normallast) – Anzeigewert (Nullwert) – Normallast
                    If (Staffel = "1") Then
                        If Bereich = "1" Then
                            Fehler6.Text = CDec(Anzeige3Normallast.Text) - CDec(Anzeige1.Text) - CDec(Last3Normallast.Text)
                        Else
                            'Für die Ermittlung der Messabweichung der Staffel 1 für die Bereiche 2 und 3 wird in der bestehenden Prüfanweisung keine Vorgehensweise beschrieben. Deshalb wird an dieser Stelle der Berechnung folgende Vereinbarung zugrunde gelegt: Die Mindestlast bleibt bei der Ermittlung der Messabweichung an dieser Stelle unberücksichtigt. Lediglich die Differenz von SOLL und IST der Mindestlast wird berücksichtigt.
                            'Folgende Formel gilt:
                            'Messabweichung = Anzeigewert (Normallast) – (Anzeigewert (Mindestlast) – Mindestlast) – Normallast
                            Fehler6.Text = (CDec(Anzeige3Normallast.Text)) - (CDec(Anzeige1.Text) - CDec(Last1.Text)) - CDec(Last3Normallast.Text)
                        End If
                    Else
                        'Für die Ermittlung der Messabweichung der Staffeln 2-5 für alle Bereiche gilt folgende Formel:
                        'Messabweichung = Anzeigewert (Ersatzlast+Normallast) – Anzeigewert (Ersatzlast) – Normallast
                        Fehler6.Text = (CDec(Anzeige3Normallast.Text)) - CDec(Anzeige1.Text) - CDec(Last2.Text)
                    End If

                Catch ex As InvalidCastException
                End Try

                'messabweichung zur WAage
                Try
                    Fehler7.Text = Fehler6.Text
                Catch e As InvalidCastException
                End Try

                'EFG'Wert 3 Berechnen
                Try
                    '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                    If CDec(Last3Normallast.Text) < Math.Round(CDec(Eichwert * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFG3.Text = Math.Round(CDec(Eichwert * 0.5), _intNullstellenE)
                    Else
                        EFG3.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                    End If
                Catch e As InvalidCastException
                End Try

                'Berechnen von EFG  4
                Try
                    '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                    If CDec(Last4.Text) < Math.Round(CDec(Eichwert * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFG4.Text = Math.Round(CDec(Eichwert * 0.5), _intNullstellenE)
                    Else
                        EFG4.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                    End If
                Catch e As InvalidCastException
                End Try

                'berechnen von EFG 5
                Try
                    EFG5.Text = Math.Round(CDec(Eichwert), _intNullstellenE) / 5
                Catch e As InvalidCastException
                End Try

                'Berechnen von EFG   6 und 7
                Try
                    If CDec(Last3Normallast.Text) < Math.Round(CDec(Eichwert * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFG6.Text = Math.Round(CDec(Eichwert * 0.5), _intNullstellenE)
                        EFG7.Text = Math.Round(CDec(Eichwert * 0.5), _intNullstellenE)
                    Else
                        EFG6.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                        EFG7.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                    End If
                Catch e As InvalidCastException
                End Try

            Else 'staffel 2 - 7 werden anders berechnet
                'lasten
                Try
                    Last3Normallast.Text = CDec(Last2.Text) + CDec(Anzeige1.Text)
                Catch e As Exception
                End Try
                Try
                    Last4.Text = Anzeige1.Text
                Catch e As Exception
                End Try

                Try
                    'Differenz Staffel
                    If Last3Normallast.Text = "0" Then
                        Fehler6.Text = "0"
                    Else
                        Fehler6.Text = (CDec(Anzeige3Normallast.Text) - CDec(Anzeige1.Text)) - CDec(Last2.Text)
                    End If
                Catch e As Exception
                End Try

                Try
                    'Differenze Normallast
                    Fehler5.Text = CDec(Anzeige4.Text) - CDec(Anzeige1.Text)
                Catch e As Exception
                End Try

                Try
                    'berechnen von EFG 5
                    EFG5.Text = Math.Round(CDec(Eichwert), _intNullstellenE) / 5
                Catch e As Exception
                End Try
                Try

                    'Berechne EFG Wert 6 und 7
                    If CDec(Last3Normallast.Text) < Math.Round(CDec(Eichwert * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                        EFG6.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                        EFG7.Text = Math.Round(CDec(Eichwert), _intNullstellenE)
                    Else
                        EFG6.Text = Math.Round(CDec(Eichwert), _intNullstellenE) * 1.5
                        EFG7.Text = Math.Round(CDec(Eichwert), _intNullstellenE) * 1.5
                    End If
                Catch e As Exception
                End Try

            End If

        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Event welches bei allen Textboxen triggerd und die Berechnungsroutinen anstößt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlEingaben_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich1Last4.TextChanged, RadTextBoxControlStaffel5Bereich1Last3.TextChanged, RadTextBoxControlStaffel5Bereich1Last2.TextChanged, RadTextBoxControlStaffel5Bereich1Last1.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich1Last4.TextChanged, RadTextBoxControlStaffel4Bereich1Last3.TextChanged, RadTextBoxControlStaffel4Bereich1Last2.TextChanged, RadTextBoxControlStaffel4Bereich1Last1.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich1Last2.TextChanged, RadTextBoxControlStaffel3Bereich1Last1.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich1Last2.TextChanged, RadTextBoxControlStaffel2Bereich1Last1.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich1Last4.TextChanged, RadTextBoxControlStaffel1Bereich1Last3.TextChanged, RadTextBoxControlStaffel1Bereich1Last2.TextChanged, RadTextBoxControlStaffel1Bereich1Last1.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige2.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige1.TextChanged,
    RadTextBoxControlStaffel5Bereich2Last4.TextChanged, RadTextBoxControlStaffel5Bereich2Last3.TextChanged, RadTextBoxControlStaffel5Bereich2Last2.TextChanged, RadTextBoxControlStaffel5Bereich2Last1.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich2Last4.TextChanged, RadTextBoxControlStaffel4Bereich2Last3.TextChanged, RadTextBoxControlStaffel4Bereich2Last2.TextChanged, RadTextBoxControlStaffel4Bereich2Last1.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich2Last2.TextChanged, RadTextBoxControlStaffel3Bereich2Last1.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich2Last2.TextChanged, RadTextBoxControlStaffel2Bereich2Last1.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich2Last4.TextChanged, RadTextBoxControlStaffel1Bereich2Last3.TextChanged, RadTextBoxControlStaffel1Bereich2Last1.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige1.TextChanged,
    RadTextBoxControlStaffel5Bereich3Last4.TextChanged, RadTextBoxControlStaffel5Bereich3Last3.TextChanged, RadTextBoxControlStaffel5Bereich3Last2.TextChanged, RadTextBoxControlStaffel5Bereich3Last1.TextChanged, RadTextBoxControlStaffel5Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich3Last4.TextChanged, RadTextBoxControlStaffel4Bereich3Last3.TextChanged, RadTextBoxControlStaffel4Bereich3Last2.TextChanged, RadTextBoxControlStaffel4Bereich3Last1.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich3Last2.TextChanged, RadTextBoxControlStaffel3Bereich3Last1.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich3Last2.TextChanged, RadTextBoxControlStaffel2Bereich3Last1.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich3Last4.TextChanged, RadTextBoxControlStaffel1Bereich3Last3.TextChanged, RadTextBoxControlStaffel1Bereich3Last1.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige1.TextChanged,
    RadTextBoxControlStaffel5Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige3.TextChanged

        If _suspendEvents = True Then Exit Sub
        _suspendEvents = True
        AktuellerStatusDirty = True

        Dim staffel As String = GetStaffel(sender)
        Dim bereich As String = GetBereich(sender)
        BerechneStaffelBereich(staffel, bereich)
        BerechneMessabweichung(bereich)
        _suspendEvents = False
    End Sub

#End Region

#Region " Messabweichung der Waage [SEwi]"

    ''' <summary>
    ''' Messabweichung ergibt sich aus Summe der Fehler aller Staffeln
    ''' </summary>
    ''' <param name="Bereich"></param>
    ''' <remarks></remarks>
    Private Sub BerechneMessabweichung(ByVal Bereich As String)
        Try

            'die Messabweichung durch alle Staffeln durchreichen
            Dim Fehler6Staffel1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 1, CInt(Bereich), 6))
            Dim Fehler6Staffel2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 2, CInt(Bereich), 6))
            Dim Fehler6Staffel3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 3, CInt(Bereich), 6))
            Dim Fehler6Staffel4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 4, CInt(Bereich), 6))
            Dim Fehler6Staffel5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 5, CInt(Bereich), 6))

            Dim Fehler7Staffel1 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 1, CInt(Bereich), 7))
            Dim Fehler7Staffel2 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 2, CInt(Bereich), 7))
            Dim Fehler7Staffel3 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 3, CInt(Bereich), 7))
            Dim Fehler7Staffel4 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 4, CInt(Bereich), 7))
            Dim Fehler7Staffel5 As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlStaffel{0}Bereich{1}Fehler{2}", 5, CInt(Bereich), 7))

            Try
                'differenz der Waage Berechnen
                Fehler7Staffel1.Text = CDec(Fehler6Staffel1.Text)
            Catch ex As Exception
                Fehler7Staffel1.Text = ""
                Exit Sub
            End Try
            Try
                Fehler7Staffel2.Text = CDec(Fehler6Staffel1.Text) + CDec(Fehler6Staffel2.Text)
            Catch ex As Exception
                Fehler7Staffel2.Text = ""
                Exit Sub
            End Try
            Try
                Fehler7Staffel3.Text = CDec(Fehler6Staffel1.Text) + CDec(Fehler6Staffel2.Text) + CDec(Fehler6Staffel3.Text)
            Catch ex As Exception
                Fehler7Staffel3.Text = ""
                Exit Sub
            End Try
            Try
                Fehler7Staffel4.Text = CDec(Fehler6Staffel1.Text) + CDec(Fehler6Staffel2.Text) + CDec(Fehler6Staffel3.Text) + CDec(Fehler6Staffel4.Text)
            Catch ex As Exception
                Fehler7Staffel4.Text = ""
                Exit Sub
            End Try
            Try
                Fehler7Staffel5.Text = CDec(Fehler6Staffel1.Text) + CDec(Fehler6Staffel2.Text) + CDec(Fehler6Staffel3.Text) + CDec(Fehler6Staffel4.Text) + CDec(Fehler6Staffel5.Text)
            Catch ex As Exception
                Fehler7Staffel5.Text = ""
                Exit Sub
            End Try
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Hilfetexte"
    Private Sub RadTextBoxControlStaffel1Bereich1Last1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel1Bereich3Last4.MouseEnter, RadTextBoxControlStaffel1Bereich3Last3.MouseEnter, RadTextBoxControlStaffel1Bereich3Last1.MouseEnter, RadTextBoxControlStaffel1Bereich2Last4.MouseEnter, RadTextBoxControlStaffel1Bereich2Last3.MouseEnter, RadTextBoxControlStaffel1Bereich2Last1.MouseEnter, RadTextBoxControlStaffel1Bereich1Last4.MouseEnter, RadTextBoxControlStaffel1Bereich1Last3.MouseEnter, RadTextBoxControlStaffel1Bereich1Last1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenNormallast)
    End Sub

    Private Sub RadGroupBoxStaffel1Bereich1_MouseEnter(sender As Object, e As EventArgs) Handles RadGroupBoxStaffel5Bereich3.MouseEnter, RadGroupBoxStaffel5Bereich2.MouseEnter, RadGroupBoxStaffel5Bereich1.MouseEnter, RadGroupBoxStaffel3Bereich3.MouseEnter, RadGroupBoxStaffel3Bereich2.MouseEnter, RadGroupBoxStaffel3Bereich1.MouseEnter, RadGroupBoxStaffel2Bereich3.MouseEnter, RadGroupBoxStaffel2Bereich2.MouseEnter, RadGroupBoxStaffel2Bereich1.MouseEnter, RadGroupBoxStaffel1Bereich3.MouseEnter, RadGroupBoxStaffel1Bereich2.MouseEnter, RadGroupBoxStaffel1Bereich1.MouseEnter
        'zurücksetzen der Hilfetexte
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Last1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Last1.MouseEnter, RadTextBoxControlStaffel5Bereich2Last1.MouseEnter, RadTextBoxControlStaffel5Bereich1Last1.MouseEnter, RadTextBoxControlStaffel4Bereich3Last1.MouseEnter, RadTextBoxControlStaffel4Bereich2Last1.MouseEnter, RadTextBoxControlStaffel4Bereich1Last1.MouseEnter, RadTextBoxControlStaffel3Bereich3Last1.MouseEnter, RadTextBoxControlStaffel3Bereich2Last1.MouseEnter, RadTextBoxControlStaffel3Bereich1Last1.MouseEnter, RadTextBoxControlStaffel2Bereich3Last1.MouseEnter, RadTextBoxControlStaffel2Bereich2Last1.MouseEnter, RadTextBoxControlStaffel2Bereich1Last1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastSoll)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Anzeige1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastIst)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Last2_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Last2.MouseEnter, RadTextBoxControlStaffel5Bereich2Last2.MouseEnter, RadTextBoxControlStaffel5Bereich1Last2.MouseEnter, RadTextBoxControlStaffel4Bereich3Last2.MouseEnter, RadTextBoxControlStaffel4Bereich2Last2.MouseEnter, RadTextBoxControlStaffel4Bereich1Last2.MouseEnter, RadTextBoxControlStaffel3Bereich3Last2.MouseEnter, RadTextBoxControlStaffel3Bereich2Last2.MouseEnter, RadTextBoxControlStaffel3Bereich1Last2.MouseEnter, RadTextBoxControlStaffel2Bereich3Last2.MouseEnter, RadTextBoxControlStaffel2Bereich2Last2.MouseEnter, RadTextBoxControlStaffel2Bereich1Last2.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastNormallast)
    End Sub

    Private Sub RadTextBoxControlStaffel5Bereich3Anzeige3_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige3.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastErsatzPlusNormallast)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Anzeige4_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige4.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastErsatzlast2)
    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco10PruefungStaffelverfahren))

        For Each Control In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf Control Is Telerik.WinControls.UI.RadGroupBox Then
                If CType(Control, Telerik.WinControls.UI.RadGroupBox).Visible = True Then
                    CType(Control, Telerik.WinControls.UI.RadGroupBox).Text = resources.GetString(String.Format("{0}.Text", CType(Control, Telerik.WinControls.UI.RadGroupBox).Name))
                    For Each control2 In CType(Control, Telerik.WinControls.UI.RadGroupBox).Controls
                        If TypeOf control2 Is Telerik.WinControls.UI.RadGroupBox Then
                            CType(control2, Telerik.WinControls.UI.RadGroupBox).Text = resources.GetString(String.Format("{0}.Text", CType(control2, Telerik.WinControls.UI.RadGroupBox).Name))
                            For Each control3 In CType(control2, Telerik.WinControls.UI.RadGroupBox).Controls
                                If TypeOf control3 Is Telerik.WinControls.UI.RadLabel Then
                                    CType(control3, Telerik.WinControls.UI.RadLabel).Text = resources.GetString(String.Format("{0}.Text", CType(control3, Telerik.WinControls.UI.RadLabel).Name))
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        Next

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            Catch ex As Exception
            End Try
        End If

    End Sub

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadGroupBoxStaffel1Bereich1)
        EnableControls(RadGroupBoxStaffel1Bereich2)
        EnableControls(RadGroupBoxStaffel1Bereich3)
        EnableControls(RadGroupBoxStaffel2Bereich1)
        EnableControls(RadGroupBoxStaffel2Bereich2)
        EnableControls(RadGroupBoxStaffel2Bereich3)
        EnableControls(RadGroupBoxStaffel3Bereich1)
        EnableControls(RadGroupBoxStaffel3Bereich2)
        EnableControls(RadGroupBoxStaffel3Bereich3)
        EnableControls(RadGroupBoxStaffel4Bereich1)
        EnableControls(RadGroupBoxStaffel4Bereich2)
        EnableControls(RadGroupBoxStaffel4Bereich3)
        EnableControls(RadGroupBoxStaffel5Bereich1)
        EnableControls(RadGroupBoxStaffel5Bereich2)
        EnableControls(RadGroupBoxStaffel5Bereich3)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Overrides Sub VersendenNeeded(TargetUserControl As UserControl)

        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)
            Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
            'auf fehlerhaft Status setzen
            objEichprozess.FK_Bearbeitungsstatus = 2
            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss
            UpdateObject()
            UeberschreibePruefungsobjekte()

            'erzeuegn eines Server Objektes auf basis des aktuellen DS
            objServerEichprozess = clsClientServerConversionFunctions.CopyServerObjectProperties(objServerEichprozess, objEichprozess, clsClientServerConversionFunctions.enuModus.RHEWASendetAnClient)
            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Try
                    'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                    Webcontext.AddEichprozess(AktuellerBenutzer.Instance.Lizenz.HEKennung, AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, Version)

                    'schließen des dialoges
                    ParentFormular.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Status zurück setzen
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
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
                            If _ListPruefungStaffelverfahrenNormallast.Count = 0 Then
                                'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                                Dim intBereiche As Integer = 0
                                If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                    intBereiche = 1
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                    intBereiche = 2
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                    intBereiche = 3
                                End If

                                For intBereich = 1 To intBereiche

                                    Dim objPruefung = Context.PruefungStaffelverfahrenNormallast.Create
                                    'wenn es die eine itereation mehr ist:
                                    objPruefung.Bereich = intBereich
                                    objPruefung.Staffel = 1

                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungStaffelverfahrenNormallast.Add(objPruefung)
                                Next

                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungStaffelverfahrenNormallast
                                    objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next

                            End If

                            'ersatzlasten
                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungStaffelverfahrenErsatzlast.Count = 0 Then
                                'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                                Dim intBereiche As Integer = 0
                                If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                    intBereiche = 1
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                    intBereiche = 2
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                    intBereiche = 3
                                End If

                                For intStaffel As Integer = 2 To 5

                                    For intBereich = 1 To intBereiche

                                        Dim objPruefung = Context.PruefungStaffelverfahrenErsatzlast.Create
                                        'wenn es die eine itereation mehr ist:
                                        objPruefung.Bereich = intBereich
                                        objPruefung.Staffel = intStaffel

                                        UpdatePruefungsObject(objPruefung)

                                        Context.SaveChanges()

                                        objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                        Context.SaveChanges()

                                        _ListPruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                    Next
                                Next

                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungStaffelverfahrenErsatzlast
                                    objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next

                            End If

                            'neuen Status zuweisen
                            If AktuellerStatusDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
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
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess

                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungStaffelverfahrenNormallast.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

                            For intBereich = 1 To intBereiche

                                Dim objPruefung = Context.PruefungStaffelverfahrenNormallast.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.Bereich = intBereich
                                objPruefung.Staffel = 1

                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(objPruefung)
                                Try
                                    Context.SaveChanges()

                                Catch ex As Exception
                                    lblStaffel1Bereich1Anzeige.Text = ""
                                End Try

                                _ListPruefungStaffelverfahrenNormallast.Add(objPruefung)
                            Next

                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungStaffelverfahrenNormallast
                                objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next

                        End If

                        'ersatzlasten
                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungStaffelverfahrenErsatzlast.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

                            For intStaffel As Integer = 2 To 5

                                For intBereich = 1 To intBereiche

                                    Dim objPruefung = Context.PruefungStaffelverfahrenErsatzlast.Create
                                    'wenn es die eine itereation mehr ist:
                                    objPruefung.Bereich = intBereich
                                    objPruefung.Staffel = intStaffel

                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                Next
                            Next

                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungStaffelverfahrenErsatzlast
                                objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next

                        End If

                        ''Füllt das Objekt mit den Werten aus den Steuerlementen
                        'UpdateObject()
                        ''Speichern in Datenbank
                        'Context.SaveChanges()
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

End Class