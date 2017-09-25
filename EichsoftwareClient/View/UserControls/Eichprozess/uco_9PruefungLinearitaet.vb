Public Class uco_9PruefungLinearitaet

    Inherits ucoContent
    Implements IRhewaEditingDialog
    Implements IRhewaPruefungDialog

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    Private _ListPruefungPruefungLinearitaetSteigend As New List(Of PruefungLinearitaetSteigend)
    Private _ListPruefungPruefungLinearitaetFallend As New List(Of PruefungLinearitaetFallend)
    Private _currentObjPruefungLinearitaetSteigend As PruefungLinearitaetSteigend
    Private _currentObjPruefungLinearitaetFallend As PruefungLinearitaetFallend

    Private _intAnzahlMesspunkte As Integer = 5
    Private _intAnzahlUrpsMessPunkte As Integer = 5 'Diese Variable wird genutzt um zu prüfen ob etwas an den Messpunkten geändert wurde um in der DB diee änderungen nachzupflegen
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
    End Sub

#End Region

#Region "Events"

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet

        'daten füllen
        LoadFromDatabase()
    End Sub

    ''' <summary>
    ''' Öffnen der Eichfehlergrenzen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonShowEFG_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFGSteigend.Click
        ShowEichfehlergrenzenDialog()
    End Sub

    Private Sub RadTextBoxControlBereich_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich3DisplayWeight8.Validating, RadTextBoxControlBereich3DisplayWeight7.Validating, RadTextBoxControlBereich3DisplayWeight6.Validating, RadTextBoxControlBereich3DisplayWeight5.Validating, RadTextBoxControlBereich3DisplayWeight4.Validating, RadTextBoxControlBereich3DisplayWeight3.Validating, RadTextBoxControlBereich3DisplayWeight2.Validating, RadTextBoxControlBereich3DisplayWeight1.Validating, RadTextBoxControlBereich2DisplayWeight8.Validating, RadTextBoxControlBereich2DisplayWeight7.Validating, RadTextBoxControlBereich2DisplayWeight6.Validating, RadTextBoxControlBereich2DisplayWeight5.Validating, RadTextBoxControlBereich2DisplayWeight4.Validating, RadTextBoxControlBereich2DisplayWeight3.Validating, RadTextBoxControlBereich2DisplayWeight2.Validating, RadTextBoxControlBereich2DisplayWeight1.Validating, RadTextBoxControlBereich1DisplayWeight8.Validating, RadTextBoxControlBereich1DisplayWeight7.Validating, RadTextBoxControlBereich1DisplayWeight6.Validating, RadTextBoxControlBereich1DisplayWeight5.Validating, RadTextBoxControlBereich1DisplayWeight4.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, RadTextBoxControlBereich1DisplayWeight2.Validating, RadTextBoxControlBereich1DisplayWeight1.Validating,
    RadTextBoxControlBereich3Weight8.Validating, RadTextBoxControlBereich3Weight7.Validating, RadTextBoxControlBereich3Weight6.Validating, RadTextBoxControlBereich3Weight5.Validating, RadTextBoxControlBereich3Weight4.Validating, RadTextBoxControlBereich3Weight3.Validating, RadTextBoxControlBereich3Weight2.Validating, RadTextBoxControlBereich3Weight1.Validating, RadTextBoxControlBereich2Weight8.Validating, RadTextBoxControlBereich2Weight7.Validating, RadTextBoxControlBereich2Weight6.Validating, RadTextBoxControlBereich2Weight5.Validating, RadTextBoxControlBereich2Weight4.Validating, RadTextBoxControlBereich2Weight3.Validating, RadTextBoxControlBereich2Weight2.Validating, RadTextBoxControlBereich2Weight1.Validating, RadTextBoxControlBereich1Weight8.Validating, RadTextBoxControlBereich1Weight7.Validating, RadTextBoxControlBereich1Weight6.Validating, RadTextBoxControlBereich1Weight5.Validating, RadTextBoxControlBereich1Weight4.Validating, RadTextBoxControlBereich1Weight3.Validating, RadTextBoxControlBereich1Weight2.Validating, RadTextBoxControlBereich1Weight1.Validating

        BasicTextboxValidation(sender, e)
    End Sub

    ''' <summary>
    ''' Verbieten vom Klicken auf die Checklisten (negiert die Eingabe um wieder den Ausgangswert zu erlangen)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxBereichVEL_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich3VEL8.MouseClick, RadCheckBoxBereich3VEL7.MouseClick, RadCheckBoxBereich3VEL6.MouseClick, RadCheckBoxBereich3VEL5.MouseClick, RadCheckBoxBereich3VEL4.MouseClick, RadCheckBoxBereich3VEL3.MouseClick, RadCheckBoxBereich3VEL2.MouseClick, RadCheckBoxBereich3VEL1.MouseClick, RadCheckBoxBereich2VEL8.MouseClick, RadCheckBoxBereich2VEL7.MouseClick, RadCheckBoxBereich2VEL6.MouseClick, RadCheckBoxBereich2VEL5.MouseClick, RadCheckBoxBereich2VEL4.MouseClick, RadCheckBoxBereich2VEL3.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich1VEL8.MouseClick, RadCheckBoxBereich1VEL7.MouseClick, RadCheckBoxBereich1VEL6.MouseClick, RadCheckBoxBereich1VEL5.MouseClick, RadCheckBoxBereich1VEL4.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL1.MouseClick, RadCheckBoxBereich3FallendVEL8.MouseClick, RadCheckBoxBereich3FallendVEL7.MouseClick, RadCheckBoxBereich3FallendVEL6.MouseClick, RadCheckBoxBereich3FallendVEL5.MouseClick, RadCheckBoxBereich3FallendVEL4.MouseClick, RadCheckBoxBereich3FallendVEL3.MouseClick, RadCheckBoxBereich3FallendVEL2.MouseClick, RadCheckBoxBereich3FallendVEL1.MouseClick, RadCheckBoxBereich2FallendVEL8.MouseClick, RadCheckBoxBereich2FallendVEL7.MouseClick, RadCheckBoxBereich2FallendVEL6.MouseClick, RadCheckBoxBereich2FallendVEL5.MouseClick, RadCheckBoxBereich2FallendVEL4.MouseClick, RadCheckBoxBereich2FallendVEL3.MouseClick, RadCheckBoxBereich2FallendVEL2.MouseClick, RadCheckBoxBereich2FallendVEL1.MouseClick, RadCheckBoxBereich1FallendVEL8.MouseClick, RadCheckBoxBereich1FallendVEL7.MouseClick, RadCheckBoxBereich1FallendVEL6.MouseClick, RadCheckBoxBereich1FallendVEL5.MouseClick, RadCheckBoxBereich1FallendVEL4.MouseClick, RadCheckBoxBereich1FallendVEL3.MouseClick, RadCheckBoxBereich1FallendVEL2.MouseClick, RadCheckBoxBereich1FallendVEL1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub


    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1Weight8.TextChanged, RadTextBoxControlBereich1Weight7.TextChanged, RadTextBoxControlBereich1Weight6.TextChanged, RadTextBoxControlBereich1Weight5.TextChanged, RadTextBoxControlBereich1Weight4.TextChanged, RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged,
         RadTextBoxControlBereich2Weight8.TextChanged, RadTextBoxControlBereich2Weight7.TextChanged, RadTextBoxControlBereich2Weight6.TextChanged, RadTextBoxControlBereich2Weight5.TextChanged, RadTextBoxControlBereich2Weight4.TextChanged, RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged,
        RadTextBoxControlBereich3Weight8.TextChanged, RadTextBoxControlBereich3Weight7.TextChanged, RadTextBoxControlBereich3Weight6.TextChanged, RadTextBoxControlBereich3Weight5.TextChanged, RadTextBoxControlBereich3Weight4.TextChanged, RadTextBoxControlBereich3Weight3.TextChanged, RadTextBoxControlBereich3Weight2.TextChanged, RadTextBoxControlBereich3Weight1.TextChanged,
        RadTextBoxControlBereich1FallendWeight8.TextChanged, RadTextBoxControlBereich1FallendWeight7.TextChanged, RadTextBoxControlBereich1FallendWeight6.TextChanged, RadTextBoxControlBereich1FallendWeight5.TextChanged, RadTextBoxControlBereich1FallendWeight4.TextChanged, RadTextBoxControlBereich1FallendWeight3.TextChanged, RadTextBoxControlBereich1FallendWeight2.TextChanged, RadTextBoxControlBereich1FallendWeight1.TextChanged,
        RadTextBoxControlBereich2FallendWeight8.TextChanged, RadTextBoxControlBereich2FallendWeight7.TextChanged, RadTextBoxControlBereich2FallendWeight6.TextChanged, RadTextBoxControlBereich2FallendWeight5.TextChanged, RadTextBoxControlBereich2FallendWeight4.TextChanged, RadTextBoxControlBereich2FallendWeight3.TextChanged, RadTextBoxControlBereich2FallendWeight2.TextChanged, RadTextBoxControlBereich2FallendWeight1.TextChanged,
        RadTextBoxControlBereich3FallendWeight8.TextChanged, RadTextBoxControlBereich3FallendWeight7.TextChanged, RadTextBoxControlBereich3FallendWeight6.TextChanged, RadTextBoxControlBereich3FallendWeight5.TextChanged, RadTextBoxControlBereich3FallendWeight4.TextChanged, RadTextBoxControlBereich3FallendWeight3.TextChanged, RadTextBoxControlBereich3FallendWeight2.TextChanged, RadTextBoxControlBereich3FallendWeight1.TextChanged

        ReicheEFGWeiter(sender)
    End Sub

    ''' <summary>
    ''' Logik zum erweitern der Messpunkte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonSteigendPlus_Click(sender As Object, e As EventArgs) Handles RadButtonSteigendPlus.Click
        ErhoeheMesspunkte()
    End Sub

    ''' <summary>
    ''' Logik zum veringern der Messpunkte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonSteigendMinus_Click(sender As Object, e As EventArgs) Handles RadButtonSteigendMinus.Click
        VerringereMesspunkte()
    End Sub
    Private Sub TextboxenGewicht_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich3DisplayWeight8.TextChanged, RadTextBoxControlBereich3DisplayWeight7.TextChanged, RadTextBoxControlBereich3DisplayWeight6.TextChanged, RadTextBoxControlBereich3DisplayWeight5.TextChanged, RadTextBoxControlBereich3DisplayWeight4.TextChanged, RadTextBoxControlBereich3DisplayWeight3.TextChanged, RadTextBoxControlBereich3DisplayWeight2.TextChanged, RadTextBoxControlBereich3DisplayWeight1.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich2DisplayWeight8.TextChanged, RadTextBoxControlBereich2DisplayWeight7.TextChanged, RadTextBoxControlBereich2DisplayWeight6.TextChanged, RadTextBoxControlBereich2DisplayWeight5.TextChanged, RadTextBoxControlBereich2DisplayWeight4.TextChanged, RadTextBoxControlBereich2DisplayWeight3.TextChanged, RadTextBoxControlBereich2DisplayWeight2.TextChanged, RadTextBoxControlBereich2DisplayWeight1.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich1DisplayWeight8.TextChanged, RadTextBoxControlBereich1DisplayWeight7.TextChanged, RadTextBoxControlBereich1DisplayWeight6.TextChanged, RadTextBoxControlBereich1DisplayWeight5.TextChanged, RadTextBoxControlBereich1DisplayWeight4.TextChanged, RadTextBoxControlBereich1DisplayWeight3.TextChanged, RadTextBoxControlBereich1DisplayWeight2.TextChanged, RadTextBoxControlBereich1DisplayWeight1.TextChanged
        If _suspendEvents Then Exit Sub

        Dim Messpunkt As String = GetMesspunkt(sender)
        Dim Bereich As String = GetBereich(sender)
        Dim Pruefung As String = GetPruefung(sender)

        Berechne(Pruefung, Bereich, Messpunkt)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' EFG Werte müssen jeweils bei Steigend und Fallend gleich sein. Diese funktion reicht sie weiter und berechnet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ReicheEFGWeiter(ByVal sender As Object)
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        Dim Bereich As String = GetBereich(sender)
        Dim Pruefung As String = GetPruefung(sender)
        Dim Messpunkt As String = GetMesspunkt(sender)

        Dim CtrlSteigend As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", CInt(Bereich), CInt(Messpunkt)))
        Dim CtrlFallend As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", CInt(Bereich), CInt(Messpunkt)))

        If Pruefung = "" Then
            CtrlFallend.Text = CtrlSteigend.Text
        Else
            CtrlSteigend.Text = CtrlFallend.Text
        End If

        'neu berechnen der Fehler und EFG
        Berechne("", Bereich)
        Berechne("Fallend", Bereich)

        _suspendEvents = False

    End Sub

    Private Sub VerringereMesspunkte()
        If _intAnzahlMesspunkte = 5 Then
            System.Media.SystemSounds.Exclamation.Play()
        Else
            AktuellerStatusDirty = True
            _intAnzahlMesspunkte -= 1
            EinAusblendenVonMessBereichen()
        End If
    End Sub

    Private Sub ErhoeheMesspunkte()
        If _intAnzahlMesspunkte = 8 Then
            System.Media.SystemSounds.Exclamation.Play()
        Else
            AktuellerStatusDirty = True
            _intAnzahlMesspunkte += 1
            EinAusblendenVonMessBereichen()
        End If
    End Sub


    Private Sub LadePruefungen() Implements IRhewaPruefungDialog.LadePruefungen
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            LadePruefungenLeseModus()
        Else
            LadePruefungenBearbeitungsModus()
        End If
    End Sub

    Private Sub LadePruefungenBearbeitungsModus() Implements IRhewaPruefungDialog.LadePruefungenBearbeitungsModus
        Try
            _ListPruefungPruefungLinearitaetSteigend.Clear()
            _ListPruefungPruefungLinearitaetFallend.Clear()
            'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
            For Each obj In objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend
                obj.Eichprotokoll = objEichprozess.Eichprotokoll
                _ListPruefungPruefungLinearitaetSteigend.Add(obj)
            Next
            For Each obj In objEichprozess.Eichprotokoll.PruefungLinearitaetFallend
                obj.Eichprotokoll = objEichprozess.Eichprotokoll
                _ListPruefungPruefungLinearitaetFallend.Add(obj)
            Next
        Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
            Using context As New Entities
                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetSteigend = query.ToList
                query = Nothing

                Dim query2 = From a In context.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetFallend = query2.ToList
                query2 = Nothing
            End Using
        End Try
    End Sub

    Private Sub LadePruefungenLeseModus() Implements IRhewaPruefungDialog.LadePruefungenLeseModus
        Using context As New Entities
            'neu laden des Objekts, diesmal mit den lookup Objekten
            objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

            'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
            Dim query = From a In context.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
            _ListPruefungPruefungLinearitaetSteigend = query.ToList
            query = Nothing

            Dim query2 = From a In context.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
            _ListPruefungPruefungLinearitaetFallend = query2.ToList
            query2 = Nothing
        End Using
    End Sub

    Private Sub FillLinearitaetSteigend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente
        'Last Berechnen
        'Die Berechnung erfolgt wie folgt: Es wird die Anzahl MAX / der angezeigten Messschritte - 1 gerechnet multipliziert mit dem aktuellen Schritt

        'bereich 1
        FillLinearitaetSteigendBereich1()
        'bereich 2
        FillLinearitaetSteigendBereich2()
        'bereich 3
        FillLinearitaetSteigendBereich3()

        For Each objPruefung In _ListPruefungPruefungLinearitaetSteigend
            Dim Messpunkt As Integer = objPruefung.Messpunkt
            Dim Bereich As Integer = objPruefung.Bereich

            Dim ctrlLast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Messpunkt))
            Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Messpunkt))

            _currentObjPruefungLinearitaetSteigend = Nothing
            _currentObjPruefungLinearitaetSteigend = objPruefung

            If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
                ctrlLast.Text = _currentObjPruefungLinearitaetSteigend.Last
                ctrlAnzeige.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
            End If

        Next
    End Sub

    Private Sub FillLinearitaetSteigendBereich3()
        Try
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 Is Nothing Then
                RadTextBoxControlBereich3Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 20
                RadTextBoxControlBereich3Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500
                RadTextBoxControlBereich3Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 2
                RadTextBoxControlBereich3Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000
                RadTextBoxControlBereich3Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillLinearitaetSteigendBereich2()
        Try

            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 Is Nothing Then
                RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 20
                RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500
                RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 2
                RadTextBoxControlBereich2Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000
                RadTextBoxControlBereich2Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillLinearitaetSteigendBereich1()
        RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20
        RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 2
        RadTextBoxControlBereich1Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
    End Sub

    Private Sub FillLinearitaetFallend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente
        'Last Berechnen
        'bereich 1
        FillLinearitaetFallendBereich1()

        'bereich 2
        FillLinearitaetFallendBereich2()
        'bereich 3
        FillLinearitaetFallendBereich3()

        For Each objPruefung In _ListPruefungPruefungLinearitaetFallend
            Dim Messpunkt As Integer = objPruefung.Messpunkt
            Dim Bereich As Integer = objPruefung.Bereich

            Dim ctrlLast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", Bereich, Messpunkt))
            Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendDisplayWeight{1}", Bereich, Messpunkt))

            _currentObjPruefungLinearitaetFallend = Nothing
            _currentObjPruefungLinearitaetFallend = objPruefung

            If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
                ctrlLast.Text = _currentObjPruefungLinearitaetFallend.Last
                ctrlAnzeige.Text = _currentObjPruefungLinearitaetFallend.Anzeige
            End If

        Next
    End Sub

    Private Sub FillLinearitaetFallendBereich3()
        Try
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 Is Nothing Then
                RadTextBoxControlBereich3FallendWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 20
                RadTextBoxControlBereich3FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500
                RadTextBoxControlBereich3FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 2
                RadTextBoxControlBereich3FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000
                RadTextBoxControlBereich3FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillLinearitaetFallendBereich2()
        Try
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 Is Nothing Then
                RadTextBoxControlBereich2FallendWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 20
                RadTextBoxControlBereich2FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500
                RadTextBoxControlBereich2FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 2
                RadTextBoxControlBereich2FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000
                RadTextBoxControlBereich2FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillLinearitaetFallendBereich1()
        RadTextBoxControlBereich1FallendWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20
        RadTextBoxControlBereich1FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 2
        RadTextBoxControlBereich1FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
    End Sub



    Private Sub LadeBereicheNachWaagenart()
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            RadGroupBoxBereich2.Visible = False
            RadGroupBoxBereich3.Visible = False

            RadGroupBoxBereich2Fallend.Visible = False
            RadGroupBoxBereich3Fallend.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            RadGroupBoxBereich2.Visible = True
            RadGroupBoxBereich3.Visible = False

            RadGroupBoxBereich2Fallend.Visible = True
            RadGroupBoxBereich3Fallend.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            RadGroupBoxBereich2.Visible = True
            RadGroupBoxBereich3.Visible = True

            RadGroupBoxBereich2Fallend.Visible = True
            RadGroupBoxBereich3Fallend.Visible = True
        End If
    End Sub



    ''' <summary>
    ''' je nach anzahl von eingestellten Messpunkten werden die panels ein oder ausgeblendet
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EinAusblendenVonMessBereichen()
        For i As Integer = 4 To 8 'messpunkt
            For j As Integer = 1 To 3 'bereich
                Dim Panel As Telerik.WinControls.UI.RadPanel = FindControl(String.Format("PanelBereich{0}WZ{1}", j, i))
                If _intAnzahlMesspunkte >= i Then
                    Panel.Visible = True
                Else
                    Panel.Visible = False
                End If

                'fallend
                Panel = Nothing
                Panel = FindControl(String.Format("PanelBereich{0}FallendWZ{1}", j, i))
                If _intAnzahlMesspunkte >= i Then
                    Panel.Visible = True
                Else
                    Panel.Visible = False
                End If
            Next
        Next
    End Sub

    Private Sub BerechneUndWeiseZu(ByRef FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBox,
    ByRef AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBox,
    ByRef GewichtTextbox As Telerik.WinControls.UI.RadTextBox,
    ByRef Checkbox As Telerik.WinControls.UI.RadCheckBox, ByVal Bereich As Integer)

        If _suspendEvents = False Then
            AktuellerStatusDirty = True
        End If

        If FehlerGrenzeTextbox.Visible = False Then Exit Sub 'abbruch wenn unsichtbar

        'abbruch wenn steuerelemente leer
        If AnzeigeGewichtTextbox.Text.Equals("") Or GewichtTextbox.Text.Equals("") Then
            Exit Sub
        End If

        'Hier gibt es kein EFG Feld. Deswegen wird für jeden DS einzeln durch GETEFG der EFG Wert errrechnet
        Try
            FehlerGrenzeTextbox.Text = CDec(AnzeigeGewichtTextbox.Text) - CDec(GewichtTextbox.Text)

            If CDec(AnzeigeGewichtTextbox.Text) > CDec(GewichtTextbox.Text) + GetEFG(CDec(GewichtTextbox.Text), Bereich) Then
                Checkbox.Checked = False
            ElseIf CDec(AnzeigeGewichtTextbox.Text) < CDec(GewichtTextbox.Text) - GetEFG(CDec(GewichtTextbox.Text), Bereich) Then
                Checkbox.Checked = False
            Else
                Checkbox.Checked = True
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Debug.WriteLine(ex.StackTrace)
        End Try

    End Sub



    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdatePruefungsLinSteigendObject(ByVal PObjPruefung As PruefungLinearitaetSteigend)
        Dim Messpunkt As Integer = PObjPruefung.Messpunkt
        Dim Bereich As Integer = PObjPruefung.Bereich

        Dim ctrlLast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Messpunkt))
        Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Messpunkt))
        Dim ctrlFehler As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}ErrorLimit{1}", Bereich, Messpunkt))
        Dim ctrlEFG As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}VEL{1}", Bereich, Messpunkt))

        PObjPruefung.Last = ctrlLast.Text
        PObjPruefung.Anzeige = ctrlAnzeige.Text
        PObjPruefung.Fehler = ctrlFehler.Text
        PObjPruefung.EFG = ctrlEFG.Checked

    End Sub
    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdatePruefungsLinFallendObject(ByVal PObjPruefung As PruefungLinearitaetFallend)
        Dim Messpunkt As Integer = PObjPruefung.Messpunkt
        Dim Bereich As Integer = PObjPruefung.Bereich

        Dim ctrlLast As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", Bereich, Messpunkt))
        Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendDisplayWeight{1}", Bereich, Messpunkt))
        Dim ctrlFehler As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextBoxControlBereich{0}FallendErrorLimit{1}", Bereich, Messpunkt))
        Dim ctrlEFG As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}FallendVEL{1}", Bereich, Messpunkt))

        PObjPruefung.Last = ctrlLast.Text
        PObjPruefung.Anzeige = ctrlAnzeige.Text
        PObjPruefung.Fehler = ctrlFehler.Text
        PObjPruefung.EFG = ctrlEFG.Checked

    End Sub

#Region "Berechnung Fehler und EFG"


    ''' <summary>
    ''' Führt Berechnung auf den Steuerelementen auf Basis von Steigender oder Fallender Prüfung durch und dem übergebenen Bereich und dem spezifierten Messpunkt
    ''' </summary>
    ''' <param name="Pruefung"></param>
    ''' <remarks></remarks>
    Private Sub Berechne(ByVal Pruefung As String, ByVal Bereich As String, ByVal Messpunkt As String)
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBox
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim Checkbox As Telerik.WinControls.UI.RadCheckBox

        Dim SuchstringFehlerGrenzeTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}ErrorLimit{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringAnzeigeGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}DisplayWeight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}Weight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringCheckbox As String = String.Format("RadCheckBoxBereich{0}{1}VEL{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))

        FehlerGrenzeTextbox = FindControl(SuchstringFehlerGrenzeTextbox)
        AnzeigeGewichtTextbox = FindControl(SuchstringAnzeigeGewichtTextbox)
        GewichtTextbox = FindControl(SuchstringGewichtTextbox)
        Checkbox = FindControl(SuchstringCheckbox)

        If Not FehlerGrenzeTextbox Is Nothing AndAlso
            Not AnzeigeGewichtTextbox Is Nothing AndAlso
            Not GewichtTextbox Is Nothing AndAlso
            Not Checkbox Is Nothing Then

            BerechneUndWeiseZu(FehlerGrenzeTextbox, AnzeigeGewichtTextbox, GewichtTextbox, Checkbox, Bereich)
        End If

    End Sub
    ''' <summary>
    ''' Führt Berechnung auf allen Steuerelementen auf Basis von Steigender oder Fallender Prüfung durch und dem übergebenen Bereich (alle Messpunkte)
    ''' </summary>
    ''' <param name="Pruefung"></param>
    ''' <remarks></remarks>
    Private Sub Berechne(ByVal Pruefung As String, ByVal Bereich As String)
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBox
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim Checkbox As Telerik.WinControls.UI.RadCheckBox

        For Messpunkt As Integer = 1 To 8 Step 1
            Dim SuchstringFehlerGrenzeTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}ErrorLimit{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
            Dim SuchstringAnzeigeGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}DisplayWeight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
            Dim SuchstringGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}Weight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
            Dim SuchstringCheckbox As String = String.Format("RadCheckBoxBereich{0}{1}VEL{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))

            FehlerGrenzeTextbox = FindControl(SuchstringFehlerGrenzeTextbox)
            AnzeigeGewichtTextbox = FindControl(SuchstringAnzeigeGewichtTextbox)
            GewichtTextbox = FindControl(SuchstringGewichtTextbox)
            Checkbox = FindControl(SuchstringCheckbox)

            If Not FehlerGrenzeTextbox Is Nothing AndAlso
                Not AnzeigeGewichtTextbox Is Nothing AndAlso
                Not GewichtTextbox Is Nothing AndAlso
                Not Checkbox Is Nothing Then

                BerechneUndWeiseZu(FehlerGrenzeTextbox, AnzeigeGewichtTextbox, GewichtTextbox, Checkbox, Bereich)
            End If
        Next

    End Sub

    ''' <summary>
    ''' Führt Berechnung auf allen Steuerelementen auf Basis von Steigender oder Fallender Prüfung durch (alle bereiche und alle Messpunkte)
    ''' </summary>
    ''' <param name="Pruefung"></param>
    ''' <remarks></remarks>
    Private Sub Berechne(ByVal Pruefung As String)
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBox
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBox
        Dim Checkbox As Telerik.WinControls.UI.RadCheckBox

        For Bereich As Integer = 1 To 3 Step 1
            For Messpunkt As Integer = 1 To 8 Step 1
                Dim SuchstringFehlerGrenzeTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}ErrorLimit{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
                Dim SuchstringAnzeigeGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}DisplayWeight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
                Dim SuchstringGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}Weight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
                Dim SuchstringCheckbox As String = String.Format("RadCheckBoxBereich{0}{1}VEL{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))

                FehlerGrenzeTextbox = FindControl(SuchstringFehlerGrenzeTextbox)
                AnzeigeGewichtTextbox = FindControl(SuchstringAnzeigeGewichtTextbox)
                GewichtTextbox = FindControl(SuchstringGewichtTextbox)
                Checkbox = FindControl(SuchstringCheckbox)

                If Not FehlerGrenzeTextbox Is Nothing AndAlso
                    Not AnzeigeGewichtTextbox Is Nothing AndAlso
                    Not GewichtTextbox Is Nothing AndAlso
                    Not Checkbox Is Nothing Then

                    BerechneUndWeiseZu(FehlerGrenzeTextbox, AnzeigeGewichtTextbox, GewichtTextbox, Checkbox, Bereich)
                Else
                    Exit For 'überspringen der folgenden Messpunkte
                End If
            Next
        Next

    End Sub


#End Region
#End Region


    Private Sub UeberschreibePruefungsobjekte()
        objEichprozess.Eichprotokoll.PruefungLinearitaetFallend.Clear()
        For Each obj In _ListPruefungPruefungLinearitaetFallend
            objEichprozess.Eichprotokoll.PruefungLinearitaetFallend.Add(obj)
        Next
        objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend.Clear()
        For Each obj In _ListPruefungPruefungLinearitaetSteigend
            objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend.Add(obj)
        Next
    End Sub


    Private Sub SaveRoutinePruefungLinearitaetFallend(Context As Entities, intBereiche As Integer)
        If _ListPruefungPruefungLinearitaetFallend.Count = 0 Then
            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren

            For j = 1 To intBereiche
                For intMesspunkt As Integer = 1 To _intAnzahlMesspunkte
                    Dim objPruefung = Context.PruefungLinearitaetFallend.Create
                    objPruefung.Messpunkt = intMesspunkt
                    objPruefung.Bereich = j

                    UpdatePruefungsLinFallendObject(objPruefung)
                    Context.SaveChanges()

                    objEichprozess.Eichprotokoll.PruefungLinearitaetFallend.Add(objPruefung)
                    Context.SaveChanges()
                    _ListPruefungPruefungLinearitaetFallend.Add(objPruefung)
                Next
            Next
        Else ' es gibt bereits welche
            'jedes objekt initialisieren und aus context laden und updaten
            For Each objPruefung In _ListPruefungPruefungLinearitaetFallend
                objPruefung = Context.PruefungLinearitaetFallend.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsLinFallendObject(objPruefung)
                Context.SaveChanges()
            Next

            'sonderfall es wurden neue messpunkte hinzugefügt

            'zählen der angezeigten messpunkte
            Dim neueAnzahlMesspunkte As Integer = 0
            If Not _ListPruefungPruefungLinearitaetFallend.Count = 0 Then
                'Zählen der eingetragenden Messpunkte

                For Each Control In RadGroupBoxBereich1.Controls
                    Try
                        If CType(Control, Telerik.WinControls.UI.RadPanel).Visible = True Then
                            neueAnzahlMesspunkte += 1

                        End If
                    Catch ex As Exception
                    End Try
                Next

            End If

            'hinzufügen
            If neueAnzahlMesspunkte > _intAnzahlUrpsMessPunkte Then
                'differenz der messpunkte ermitteln
                neueAnzahlMesspunkte = neueAnzahlMesspunkte - _intAnzahlUrpsMessPunkte
                For j = 1 To intBereiche
                    For i As Integer = 1 To neueAnzahlMesspunkte
                        Dim objPruefung = Context.PruefungLinearitaetFallend.Create
                        objPruefung.Messpunkt = _intAnzahlUrpsMessPunkte + i 'vorherige Messpunkte addiert mit schleifendurchlauf
                        objPruefung.Bereich = j

                        UpdatePruefungsLinFallendObject(objPruefung)
                        Context.SaveChanges()

                        objEichprozess.Eichprotokoll.PruefungLinearitaetFallend.Add(objPruefung)
                        Context.SaveChanges()
                        _ListPruefungPruefungLinearitaetFallend.Add(objPruefung)
                    Next
                Next
                'löschen
            ElseIf neueAnzahlMesspunkte < _intAnzahlUrpsMessPunkte Then

                Dim query = From o In Context.PruefungLinearitaetFallend Where CInt(o.Messpunkt) > neueAnzahlMesspunkte 'die DS abrufen in denen der bereich hoeher ist als das neue Max an Bereichen
                For Each Pruefung In query
                    Context.PruefungLinearitaetFallend.Remove(Pruefung)
                Next

                Context.SaveChanges()
            End If
        End If
    End Sub

    Private Sub SaveRoutinePruefungLinearitaetSteigend(Context As Entities, intBereiche As Integer)
        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
        If _ListPruefungPruefungLinearitaetSteigend.Count = 0 Then
            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren

            For j = 1 To intBereiche
                For intMesspunkt As Integer = 1 To _intAnzahlMesspunkte
                    Dim objPruefung = Context.PruefungLinearitaetSteigend.Create
                    objPruefung.Messpunkt = intMesspunkt
                    objPruefung.Bereich = j

                    UpdatePruefungsLinSteigendObject(objPruefung)
                    Context.SaveChanges()

                    objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend.Add(objPruefung)
                    Context.SaveChanges()
                    _ListPruefungPruefungLinearitaetSteigend.Add(objPruefung)
                Next
            Next
        Else ' es gibt bereits welche
            'jedes objekt initialisieren und aus context laden und updaten
            For Each objPruefung In _ListPruefungPruefungLinearitaetSteigend
                objPruefung = Context.PruefungLinearitaetSteigend.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsLinSteigendObject(objPruefung)
                Context.SaveChanges()
            Next

            'sonderfall es wurden neue messpunkte hinzugefügt

            'zählen der angezeigten messpunkte
            Dim neueAnzahlMesspunkte As Integer = 0
            If Not _ListPruefungPruefungLinearitaetSteigend.Count = 0 Then
                'Zählen der eingetragenden Messpunkte anhand der sichtparen panel

                For Each Control In RadGroupBoxBereich1.Controls
                    Try
                        If CType(Control, Telerik.WinControls.UI.RadPanel).Visible = True Then
                            neueAnzahlMesspunkte += 1

                        End If
                    Catch ex As Exception
                    End Try
                Next

            End If

            'hinzufügen
            If neueAnzahlMesspunkte > _intAnzahlUrpsMessPunkte Then
                'differenz der messpunkte ermitteln
                neueAnzahlMesspunkte = neueAnzahlMesspunkte - _intAnzahlUrpsMessPunkte
                For j = 1 To intBereiche
                    For i As Integer = 1 To neueAnzahlMesspunkte
                        Dim objPruefung = Context.PruefungLinearitaetSteigend.Create
                        objPruefung.Messpunkt = _intAnzahlUrpsMessPunkte + i 'vorherige Messpunkte addiert mit schleifendurchlauf
                        objPruefung.Bereich = j

                        UpdatePruefungsLinSteigendObject(objPruefung)
                        Context.SaveChanges()

                        objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend.Add(objPruefung)
                        Context.SaveChanges()
                        _ListPruefungPruefungLinearitaetSteigend.Add(objPruefung)
                    Next
                Next
                'löschen
            ElseIf neueAnzahlMesspunkte < _intAnzahlUrpsMessPunkte Then
                Dim query = From o In Context.PruefungLinearitaetSteigend Where CInt(o.Messpunkt) > neueAnzahlMesspunkte 'die DS abrufen in denen der bereich hoeher ist als das neue Max an Bereichen
                For Each Pruefung In query
                    Context.PruefungLinearitaetSteigend.Remove(Pruefung)
                Next

                Context.SaveChanges()
            End If

        End If
    End Sub

#Region "Interface Methods"

    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase
        Me.SuspendLayout()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        LadePruefungen()

        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(RadGroupBoxBereich1)
            DisableControls(RadGroupBoxBereich1Fallend)
            DisableControls(RadGroupBoxBereich2)
            DisableControls(RadGroupBoxBereich2Fallend)
            DisableControls(RadGroupBoxBereich3)
            DisableControls(RadGroupBoxBereich3Fallend)

            RadButtonSteigendMinus.Enabled = False
            RadButtonSteigendPlus.Enabled = False

        End If
        'events abbrechen
        _suspendEvents = False
        Me.ResumeLayout()
    End Sub
    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls

        If Not _ListPruefungPruefungLinearitaetFallend.Count = 0 Then
            'Zählen der eingetragenden Messpunkte
            Dim query = From o In _ListPruefungPruefungLinearitaetFallend Where o.Bereich = 1
            _intAnzahlMesspunkte = query.Count
            _intAnzahlUrpsMessPunkte = _intAnzahlMesspunkte
        End If

        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:

        HoleNullstellen()

        'je nach Art der Waage andere Bereichsgruppen ausblenden
        LadeBereicheNachWaagenart()

        FillLinearitaetSteigend()
        FillLinearitaetFallend()

        Berechne("")
        Berechne("Fallend")
    End Sub
    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        'prüfen ob alle Felder ausgefüllt sind
        AbortSaving = False

        For i As Integer = 1 To 8 'messpunkt
            For j As Integer = 1 To 3 'bereich
                Dim checkbox As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}VEL{1}", j, i))
                If checkbox.Checked = False And checkbox.Visible = True Then
                    AbortSaving = True
                    Dim textbox As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextboxControlBereich{0}DisplayWeight{1}", j, i))
                    textbox.TextBoxElement.Border.ForeColor = Color.Red
                End If

                checkbox = Nothing
                checkbox = FindControl(String.Format("RadCheckBoxBereich{0}FallendVEL{1}", j, i))
                If checkbox.Checked = False And checkbox.Visible = True Then
                    AbortSaving = True
                    Dim textbox As Telerik.WinControls.UI.RadTextBox = FindControl(String.Format("RadTextboxControlBereich{0}FallendDisplayWeight{1}", j, i))
                    textbox.TextBoxElement.Border.ForeColor = Color.Red
                End If
            Next
        Next

        'fehlermeldung anzeigen bei falscher validierung
        Dim result = Me.ShowValidationErrorBox(False)
        Return ProcessResult(result)


    End Function

    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadTextBoxControlBereich1DisplayWeight1.Text = RadTextBoxControlBereich1Weight1.Text
        RadTextBoxControlBereich1DisplayWeight2.Text = RadTextBoxControlBereich1Weight2.Text
        RadTextBoxControlBereich1DisplayWeight3.Text = RadTextBoxControlBereich1Weight3.Text
        RadTextBoxControlBereich1DisplayWeight4.Text = RadTextBoxControlBereich1Weight4.Text
        RadTextBoxControlBereich1DisplayWeight5.Text = RadTextBoxControlBereich1Weight5.Text
        RadTextBoxControlBereich1DisplayWeight6.Text = RadTextBoxControlBereich1Weight6.Text
        RadTextBoxControlBereich1DisplayWeight7.Text = RadTextBoxControlBereich1Weight7.Text
        RadTextBoxControlBereich1DisplayWeight8.Text = RadTextBoxControlBereich1Weight8.Text
        RadTextBoxControlBereich1FallendDisplayWeight1.Text = RadTextBoxControlBereich1FallendWeight1.Text
        RadTextBoxControlBereich1FallendDisplayWeight2.Text = RadTextBoxControlBereich1FallendWeight2.Text
        RadTextBoxControlBereich1FallendDisplayWeight3.Text = RadTextBoxControlBereich1FallendWeight3.Text
        RadTextBoxControlBereich1FallendDisplayWeight4.Text = RadTextBoxControlBereich1FallendWeight4.Text
        RadTextBoxControlBereich1FallendDisplayWeight5.Text = RadTextBoxControlBereich1FallendWeight5.Text
        RadTextBoxControlBereich1FallendDisplayWeight6.Text = RadTextBoxControlBereich1FallendWeight6.Text
        RadTextBoxControlBereich1FallendDisplayWeight7.Text = RadTextBoxControlBereich1FallendWeight7.Text
        RadTextBoxControlBereich1FallendDisplayWeight8.Text = RadTextBoxControlBereich1FallendWeight8.Text

        RadTextBoxControlBereich2DisplayWeight1.Text = RadTextBoxControlBereich2Weight1.Text
        RadTextBoxControlBereich2DisplayWeight2.Text = RadTextBoxControlBereich2Weight2.Text
        RadTextBoxControlBereich2DisplayWeight3.Text = RadTextBoxControlBereich2Weight3.Text
        RadTextBoxControlBereich2DisplayWeight4.Text = RadTextBoxControlBereich2Weight4.Text
        RadTextBoxControlBereich2DisplayWeight5.Text = RadTextBoxControlBereich2Weight5.Text
        RadTextBoxControlBereich2DisplayWeight6.Text = RadTextBoxControlBereich2Weight6.Text
        RadTextBoxControlBereich2DisplayWeight7.Text = RadTextBoxControlBereich2Weight7.Text
        RadTextBoxControlBereich2DisplayWeight8.Text = RadTextBoxControlBereich2Weight8.Text
        RadTextBoxControlBereich2FallendDisplayWeight1.Text = RadTextBoxControlBereich2FallendWeight1.Text
        RadTextBoxControlBereich2FallendDisplayWeight2.Text = RadTextBoxControlBereich2FallendWeight2.Text
        RadTextBoxControlBereich2FallendDisplayWeight3.Text = RadTextBoxControlBereich2FallendWeight3.Text
        RadTextBoxControlBereich2FallendDisplayWeight4.Text = RadTextBoxControlBereich2FallendWeight4.Text
        RadTextBoxControlBereich2FallendDisplayWeight5.Text = RadTextBoxControlBereich2FallendWeight5.Text
        RadTextBoxControlBereich2FallendDisplayWeight6.Text = RadTextBoxControlBereich2FallendWeight6.Text
        RadTextBoxControlBereich2FallendDisplayWeight7.Text = RadTextBoxControlBereich2FallendWeight7.Text
        RadTextBoxControlBereich2FallendDisplayWeight8.Text = RadTextBoxControlBereich2FallendWeight8.Text

        RadTextBoxControlBereich3DisplayWeight1.Text = RadTextBoxControlBereich3Weight1.Text
        RadTextBoxControlBereich3DisplayWeight2.Text = RadTextBoxControlBereich3Weight2.Text
        RadTextBoxControlBereich3DisplayWeight3.Text = RadTextBoxControlBereich3Weight3.Text
        RadTextBoxControlBereich3DisplayWeight4.Text = RadTextBoxControlBereich3Weight4.Text
        RadTextBoxControlBereich3DisplayWeight5.Text = RadTextBoxControlBereich3Weight5.Text
        RadTextBoxControlBereich3DisplayWeight6.Text = RadTextBoxControlBereich3Weight6.Text
        RadTextBoxControlBereich3DisplayWeight7.Text = RadTextBoxControlBereich3Weight7.Text
        RadTextBoxControlBereich3DisplayWeight8.Text = RadTextBoxControlBereich3Weight8.Text
        RadTextBoxControlBereich3FallendDisplayWeight1.Text = RadTextBoxControlBereich3FallendWeight1.Text
        RadTextBoxControlBereich3FallendDisplayWeight2.Text = RadTextBoxControlBereich3FallendWeight2.Text
        RadTextBoxControlBereich3FallendDisplayWeight3.Text = RadTextBoxControlBereich3FallendWeight3.Text
        RadTextBoxControlBereich3FallendDisplayWeight4.Text = RadTextBoxControlBereich3FallendWeight4.Text
        RadTextBoxControlBereich3FallendDisplayWeight5.Text = RadTextBoxControlBereich3FallendWeight5.Text
        RadTextBoxControlBereich3FallendDisplayWeight6.Text = RadTextBoxControlBereich3FallendWeight6.Text
        RadTextBoxControlBereich3FallendDisplayWeight7.Text = RadTextBoxControlBereich3FallendWeight7.Text
        RadTextBoxControlBereich3FallendDisplayWeight8.Text = RadTextBoxControlBereich3FallendWeight8.Text

    End Sub

    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now
        'neuen Context aufbauen
        Using Context As New Entities
            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungPruefungLinearitaetFallend
                Dim objPruefung = Context.PruefungLinearitaetFallend.FirstOrDefault(Function(value) value.ID = obj.ID)
                If Not objPruefung Is Nothing Then
                    'in lokaler DB gucken
                    UpdatePruefungsLinFallendObject(objPruefung)
                Else 'es handelt sich um eine Serverobjekt im => Korrekturmodus
                    If DialogModus = enuDialogModus.korrigierend Then
                        UpdatePruefungsLinFallendObject(obj)
                    End If
                End If
            Next
            'jedes objekt initialisieren und aus context laden und updaten
            For Each obj In _ListPruefungPruefungLinearitaetSteigend
                Dim objPruefung = Context.PruefungLinearitaetSteigend.FirstOrDefault(Function(value) value.ID = obj.ID)
                If Not objPruefung Is Nothing Then
                    'in lokaler DB gucken
                    UpdatePruefungsLinSteigendObject(objPruefung)
                Else 'es handelt sich um eine Serverobjekt im => Korrekturmodus
                    If DialogModus = enuDialogModus.korrigierend Then
                        UpdatePruefungsLinSteigendObject(obj)
                    End If
                End If
            Next
        End Using
    End Sub

    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
        EnableControls(RadGroupBoxBereich1)
        EnableControls(RadGroupBoxBereich1Fallend)
        EnableControls(RadGroupBoxBereich2)
        EnableControls(RadGroupBoxBereich2Fallend)
        EnableControls(RadGroupBoxBereich3)
        EnableControls(RadGroupBoxBereich3Fallend)

        RadButtonSteigendMinus.Enabled = True
        RadButtonSteigendPlus.Enabled = True

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

    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerRichtigkeitMitNormallast)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerRichtigkeitMitNormallast
            Catch ex As Exception
            End Try
        End If
    End Sub
    Protected Friend Overrides Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt
        'neuen Context aufbauen
        Using Context As New Entities
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess

                    Dim intBereiche As Integer = GetAnzahlBereiche()
                    'linearität steigend

                    SaveRoutinePruefungLinearitaetSteigend(Context, intBereiche)

                    'linearität Fallend
                    'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                    SaveRoutinePruefungLinearitaetFallend(Context, intBereiche)
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
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                If Not dobjEichprozess Is Nothing Then
                    'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                    objEichprozess = dobjEichprozess

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

                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using
    End Sub

    Protected Friend Overrides Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Then
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False

        End If

        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function



    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_9PruefungLinearitaet))
        Lokalisierung(Me, resources)
        SetzeUeberschrift()
    End Sub



#End Region



End Class