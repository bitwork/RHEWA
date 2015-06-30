' 11.03.2014 hill EichsoftwareClient ucoPruefungLinearitaet.vb 
Imports System

Public Class uco_9PruefungLinearitaet
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
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
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerRichtigkeitMitNormallast)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerRichtigkeitMitNormallast
            Catch ex As Exception
            End Try
        End If
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
    Private Sub RadButtonShowEFG_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFGFallend.Click, RadButtonShowEFGSteigend.Click
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()
    End Sub

    Private Sub RadTextBoxControlBereich_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich3DisplayWeight8.Validating, RadTextBoxControlBereich3DisplayWeight7.Validating, RadTextBoxControlBereich3DisplayWeight6.Validating, RadTextBoxControlBereich3DisplayWeight5.Validating, RadTextBoxControlBereich3DisplayWeight4.Validating, RadTextBoxControlBereich3DisplayWeight3.Validating, RadTextBoxControlBereich3DisplayWeight2.Validating, RadTextBoxControlBereich3DisplayWeight1.Validating, RadTextBoxControlBereich2DisplayWeight8.Validating, RadTextBoxControlBereich2DisplayWeight7.Validating, RadTextBoxControlBereich2DisplayWeight6.Validating, RadTextBoxControlBereich2DisplayWeight5.Validating, RadTextBoxControlBereich2DisplayWeight4.Validating, RadTextBoxControlBereich2DisplayWeight3.Validating, RadTextBoxControlBereich2DisplayWeight2.Validating, RadTextBoxControlBereich2DisplayWeight1.Validating, RadTextBoxControlBereich1DisplayWeight8.Validating, RadTextBoxControlBereich1DisplayWeight7.Validating, RadTextBoxControlBereich1DisplayWeight6.Validating, RadTextBoxControlBereich1DisplayWeight5.Validating, RadTextBoxControlBereich1DisplayWeight4.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, RadTextBoxControlBereich1DisplayWeight2.Validating, RadTextBoxControlBereich1DisplayWeight1.Validating, _
    RadTextBoxControlBereich3Weight8.Validating, RadTextBoxControlBereich3Weight7.Validating, RadTextBoxControlBereich3Weight6.Validating, RadTextBoxControlBereich3Weight5.Validating, RadTextBoxControlBereich3Weight4.Validating, RadTextBoxControlBereich3Weight3.Validating, RadTextBoxControlBereich3Weight2.Validating, RadTextBoxControlBereich3Weight1.Validating, RadTextBoxControlBereich2Weight8.Validating, RadTextBoxControlBereich2Weight7.Validating, RadTextBoxControlBereich2Weight6.Validating, RadTextBoxControlBereich2Weight5.Validating, RadTextBoxControlBereich2Weight4.Validating, RadTextBoxControlBereich2Weight3.Validating, RadTextBoxControlBereich2Weight2.Validating, RadTextBoxControlBereich2Weight1.Validating, RadTextBoxControlBereich1Weight8.Validating, RadTextBoxControlBereich1Weight7.Validating, RadTextBoxControlBereich1Weight6.Validating, RadTextBoxControlBereich1Weight5.Validating, RadTextBoxControlBereich1Weight4.Validating, RadTextBoxControlBereich1Weight3.Validating, RadTextBoxControlBereich1Weight2.Validating, RadTextBoxControlBereich1Weight1.Validating


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
    ''' Verbieten vom Klicken auf die Checklisten (negiert die Eingabe um wieder den Ausgangswert zu erlangen)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxBereichVEL_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich3VEL8.MouseClick, RadCheckBoxBereich3VEL7.MouseClick, RadCheckBoxBereich3VEL6.MouseClick, RadCheckBoxBereich3VEL5.MouseClick, RadCheckBoxBereich3VEL4.MouseClick, RadCheckBoxBereich3VEL3.MouseClick, RadCheckBoxBereich3VEL2.MouseClick, RadCheckBoxBereich3VEL1.MouseClick, RadCheckBoxBereich2VEL8.MouseClick, RadCheckBoxBereich2VEL7.MouseClick, RadCheckBoxBereich2VEL6.MouseClick, RadCheckBoxBereich2VEL5.MouseClick, RadCheckBoxBereich2VEL4.MouseClick, RadCheckBoxBereich2VEL3.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich1VEL8.MouseClick, RadCheckBoxBereich1VEL7.MouseClick, RadCheckBoxBereich1VEL6.MouseClick, RadCheckBoxBereich1VEL5.MouseClick, RadCheckBoxBereich1VEL4.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL1.MouseClick, RadCheckBoxBereich3FallendVEL8.MouseClick, RadCheckBoxBereich3FallendVEL7.MouseClick, RadCheckBoxBereich3FallendVEL6.MouseClick, RadCheckBoxBereich3FallendVEL5.MouseClick, RadCheckBoxBereich3FallendVEL4.MouseClick, RadCheckBoxBereich3FallendVEL3.MouseClick, RadCheckBoxBereich3FallendVEL2.MouseClick, RadCheckBoxBereich3FallendVEL1.MouseClick, RadCheckBoxBereich2FallendVEL8.MouseClick, RadCheckBoxBereich2FallendVEL7.MouseClick, RadCheckBoxBereich2FallendVEL6.MouseClick, RadCheckBoxBereich2FallendVEL5.MouseClick, RadCheckBoxBereich2FallendVEL4.MouseClick, RadCheckBoxBereich2FallendVEL3.MouseClick, RadCheckBoxBereich2FallendVEL2.MouseClick, RadCheckBoxBereich2FallendVEL1.MouseClick, RadCheckBoxBereich1FallendVEL8.MouseClick, RadCheckBoxBereich1FallendVEL7.MouseClick, RadCheckBoxBereich1FallendVEL6.MouseClick, RadCheckBoxBereich1FallendVEL5.MouseClick, RadCheckBoxBereich1FallendVEL4.MouseClick, RadCheckBoxBereich1FallendVEL3.MouseClick, RadCheckBoxBereich1FallendVEL2.MouseClick, RadCheckBoxBereich1FallendVEL1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub



  

    

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

        Dim CtrlSteigend As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", CInt(Bereich), CInt(Messpunkt)))
        Dim CtrlFallend As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", CInt(Bereich), CInt(Messpunkt)))

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

    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1Weight8.TextChanged, RadTextBoxControlBereich1Weight7.TextChanged, RadTextBoxControlBereich1Weight6.TextChanged, RadTextBoxControlBereich1Weight5.TextChanged, RadTextBoxControlBereich1Weight4.TextChanged, RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged, _
         RadTextBoxControlBereich2Weight8.TextChanged, RadTextBoxControlBereich2Weight7.TextChanged, RadTextBoxControlBereich2Weight6.TextChanged, RadTextBoxControlBereich2Weight5.TextChanged, RadTextBoxControlBereich2Weight4.TextChanged, RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged, _
        RadTextBoxControlBereich3Weight8.TextChanged, RadTextBoxControlBereich3Weight7.TextChanged, RadTextBoxControlBereich3Weight6.TextChanged, RadTextBoxControlBereich3Weight5.TextChanged, RadTextBoxControlBereich3Weight4.TextChanged, RadTextBoxControlBereich3Weight3.TextChanged, RadTextBoxControlBereich3Weight2.TextChanged, RadTextBoxControlBereich3Weight1.TextChanged, _
        RadTextBoxControlBereich1FallendWeight8.TextChanged, RadTextBoxControlBereich1FallendWeight7.TextChanged, RadTextBoxControlBereich1FallendWeight6.TextChanged, RadTextBoxControlBereich1FallendWeight5.TextChanged, RadTextBoxControlBereich1FallendWeight4.TextChanged, RadTextBoxControlBereich1FallendWeight3.TextChanged, RadTextBoxControlBereich1FallendWeight2.TextChanged, RadTextBoxControlBereich1FallendWeight1.TextChanged, _
        RadTextBoxControlBereich2FallendWeight8.TextChanged, RadTextBoxControlBereich2FallendWeight7.TextChanged, RadTextBoxControlBereich2FallendWeight6.TextChanged, RadTextBoxControlBereich2FallendWeight5.TextChanged, RadTextBoxControlBereich2FallendWeight4.TextChanged, RadTextBoxControlBereich2FallendWeight3.TextChanged, RadTextBoxControlBereich2FallendWeight2.TextChanged, RadTextBoxControlBereich2FallendWeight1.TextChanged, _
        RadTextBoxControlBereich3FallendWeight8.TextChanged, RadTextBoxControlBereich3FallendWeight7.TextChanged, RadTextBoxControlBereich3FallendWeight6.TextChanged, RadTextBoxControlBereich3FallendWeight5.TextChanged, RadTextBoxControlBereich3FallendWeight4.TextChanged, RadTextBoxControlBereich3FallendWeight3.TextChanged, RadTextBoxControlBereich3FallendWeight2.TextChanged, RadTextBoxControlBereich3FallendWeight1.TextChanged

        ReicheEFGWeiter(sender)
    End Sub

    ''' <summary>
    ''' Logik zum erweitern der Messpunkte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonSteigendPlus_Click(sender As Object, e As EventArgs) Handles RadButtonSteigendPlus.Click, RadButtonFallendPlus.Click
        If _intAnzahlMesspunkte = 8 Then
            System.Media.SystemSounds.Exclamation.Play()
        Else
            AktuellerStatusDirty = True
            _intAnzahlMesspunkte += 1
            EinAusblendenVonMessBereichen()
        End If
    End Sub

    ''' <summary>
    ''' Logik zum veringern der Messpunkte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonSteigendMinus_Click(sender As Object, e As EventArgs) Handles RadButtonSteigendMinus.Click, RadButtonFallendMinus.Click
        If _intAnzahlMesspunkte = 5 Then
            System.Media.SystemSounds.Exclamation.Play()
        Else
            AktuellerStatusDirty = True
            _intAnzahlMesspunkte -= 1
            EinAusblendenVonMessBereichen()
        End If
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
                Dim query = From a In context.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetSteigend = query.ToList
                query = Nothing

                Dim query2 = From a In context.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetFallend = query2.ToList
                query2 = Nothing
            End Using
        Else

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
                Using context As New EichsoftwareClientdatabaseEntities1
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query = From a In context.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungPruefungLinearitaetSteigend = query.ToList
                    query = Nothing

                    Dim query2 = From a In context.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungPruefungLinearitaetFallend = query2.ToList
                    query2 = Nothing
                End Using
            End Try

        End If


        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In RadScrollablePanel1.PanelContainer.Controls
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

    Private Sub FillLinearitaetSteigend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente

        'Last Berechnen

        'Die Berechnung erfolgt wie folgt: Es wird die Anzahl MAX / der angezeigten Messschritte - 1 gerechnet multipliziert mit dem aktuellen Schritt

        'bereich 1
        RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20
        RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 2
        RadTextBoxControlBereich1Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        'RadTextBoxControlBereich1Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2500
        'RadTextBoxControlBereich1Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3000
        'RadTextBoxControlBereich1Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3500

        'bereich 2
        Try

            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 Is Nothing Then
                RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 20
                RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500
                RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 2
                RadTextBoxControlBereich2Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000
                RadTextBoxControlBereich2Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2

                'RadTextBoxControlBereich2Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2500
                'RadTextBoxControlBereich2Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3000
                'RadTextBoxControlBereich2Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3500
            End If
        Catch ex As Exception

        End Try
        'bereich 3
        Try
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 Is Nothing Then
                RadTextBoxControlBereich3Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 20
                RadTextBoxControlBereich3Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500
                RadTextBoxControlBereich3Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 2
                RadTextBoxControlBereich3Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000
                RadTextBoxControlBereich3Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
                'RadTextBoxControlBereich3Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2500
                'RadTextBoxControlBereich3Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3000
                'RadTextBoxControlBereich3Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3500
            End If
        Catch ex As Exception
        End Try

        For Each objPruefung In _ListPruefungPruefungLinearitaetSteigend
            Dim Messpunkt As Integer = objPruefung.Messpunkt
            Dim Bereich As Integer = objPruefung.Bereich

            Dim ctrlLast As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Messpunkt))
            Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Messpunkt))

            _currentObjPruefungLinearitaetSteigend = Nothing
            _currentObjPruefungLinearitaetSteigend = objPruefung

            If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
                ctrlLast.Text = _currentObjPruefungLinearitaetSteigend.Last
                ctrlAnzeige.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
            End If

        Next
    End Sub

    Private Sub FillLinearitaetFallend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente

        'Last Berechnen


        'bereich 1
        RadTextBoxControlBereich1FallendWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20
        RadTextBoxControlBereich1FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 2
        RadTextBoxControlBereich1FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1


        'bereich 2
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
        'bereich 3
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


        For Each objPruefung In _ListPruefungPruefungLinearitaetFallend
            Dim Messpunkt As Integer = objPruefung.Messpunkt
            Dim Bereich As Integer = objPruefung.Bereich

            Dim ctrlLast As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", Bereich, Messpunkt))
            Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendDisplayWeight{1}", Bereich, Messpunkt))

            _currentObjPruefungLinearitaetFallend = Nothing
            _currentObjPruefungLinearitaetFallend = objPruefung

            If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
                ctrlLast.Text = _currentObjPruefungLinearitaetFallend.Last
                ctrlAnzeige.Text = _currentObjPruefungLinearitaetFallend.Anzeige
            End If

        Next
    End Sub



    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()

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


        FillLinearitaetSteigend()
        FillLinearitaetFallend()

        Berechne("")
        Berechne("Fallend")
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

        Dim ctrlLast As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}Weight{1}", Bereich, Messpunkt))
        Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}DisplayWeight{1}", Bereich, Messpunkt))
        Dim ctrlFehler As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}ErrorLimit{1}", Bereich, Messpunkt))
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

        Dim ctrlLast As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendWeight{1}", Bereich, Messpunkt))
        Dim ctrlAnzeige As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendDisplayWeight{1}", Bereich, Messpunkt))
        Dim ctrlFehler As Telerik.WinControls.UI.RadTextBoxControl = FindControl(String.Format("RadTextBoxControlBereich{0}FallendErrorLimit{1}", Bereich, Messpunkt))
        Dim ctrlEFG As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}FallendVEL{1}", Bereich, Messpunkt))

        PObjPruefung.Last = ctrlLast.Text
        PObjPruefung.Anzeige = ctrlAnzeige.Text
        PObjPruefung.Fehler = ctrlFehler.Text
        PObjPruefung.EFG = ctrlEFG.Checked

    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        AbortSaveing = False

        For i As Integer = 1 To 8 'messpunkt
            For j As Integer = 1 To 3 'bereich
                Dim checkbox As Telerik.WinControls.UI.RadCheckBox = FindControl(String.Format("RadCheckBoxBereich{0}VEL{1}", j, i))
                If checkbox.Checked = False And checkbox.Visible = True Then
                    AbortSaveing = True
                End If

                checkbox = Nothing
                checkbox = FindControl(String.Format("RadCheckBoxBereich{0}FallendVEL{1}", j, i))
                If checkbox.Checked = False And checkbox.Visible = True Then
                    AbortSaveing = True
                End If
            Next
        Next

        'fehlermeldung anzeigen bei falscher validierung
        Return Me.ShowValidationErrorBox()

    End Function

  
    ''' <summary>
    ''' je nach anzahl von eingestellten Messpunkten werden die panels ein oder ausgeblendet
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EinAusblendenVonMessBereichen()
        For i As Integer = 4 To 8 'messpunkt
            For j As Integer = 1 To 3 'bereich
                Dim Panel As Windows.Forms.Panel = FindControl(String.Format("PanelBereich{0}WZ{1}", j, i))
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

    Private Sub BerechneUndWeiseZu(ByRef FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBoxControl, _
    ByRef AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl, _
    ByRef GewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl, _
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

            If CDec(AnzeigeGewichtTextbox.Text) > CDec(GewichtTextbox.Text) + GetEFG(CDec(GewichtTextbox.Text), bereich) Then
                Checkbox.Checked = False
            ElseIf CDec(AnzeigeGewichtTextbox.Text) < CDec(GewichtTextbox.Text) - GetEFG(CDec(GewichtTextbox.Text), bereich) Then
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
    ''' Führt Berechnung auf den Steuerelementen auf Basis von Steigender oder Fallender Prüfung durch und dem übergebenen Bereich und dem spezifierten Messpunkt
    ''' </summary>
    ''' <param name="Pruefung"></param>
    ''' <remarks></remarks>
    Private Sub Berechne(ByVal Pruefung As String, ByVal Bereich As String, ByVal Messpunkt As String)
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim Checkbox As Telerik.WinControls.UI.RadCheckBox



        Dim SuchstringFehlerGrenzeTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}ErrorLimit{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringAnzeigeGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}DisplayWeight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringGewichtTextbox As String = String.Format("RadTextBoxControlBereich{0}{1}Weight{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))
        Dim SuchstringCheckbox As String = String.Format("RadCheckBoxBereich{0}{1}VEL{2}", CInt(Bereich), Pruefung, CInt(Messpunkt))

        FehlerGrenzeTextbox = FindControl(SuchstringFehlerGrenzeTextbox)
        AnzeigeGewichtTextbox = FindControl(SuchstringAnzeigeGewichtTextbox)
        GewichtTextbox = FindControl(SuchstringGewichtTextbox)
        Checkbox = FindControl(SuchstringCheckbox)

        If Not FehlerGrenzeTextbox Is Nothing AndAlso _
            Not AnzeigeGewichtTextbox Is Nothing AndAlso _
            Not GewichtTextbox Is Nothing AndAlso _
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
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
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

            If Not FehlerGrenzeTextbox Is Nothing AndAlso _
                Not AnzeigeGewichtTextbox Is Nothing AndAlso _
                Not GewichtTextbox Is Nothing AndAlso _
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
        Dim FehlerGrenzeTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim AnzeigeGewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
        Dim GewichtTextbox As Telerik.WinControls.UI.RadTextBoxControl
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

                If Not FehlerGrenzeTextbox Is Nothing AndAlso _
                    Not AnzeigeGewichtTextbox Is Nothing AndAlso _
                    Not GewichtTextbox Is Nothing AndAlso _
                    Not Checkbox Is Nothing Then

                    BerechneUndWeiseZu(FehlerGrenzeTextbox, AnzeigeGewichtTextbox, GewichtTextbox, Checkbox, Bereich)
                Else
                    Exit For 'überspringen der folgenden Messpunkte
                End If
            Next
        Next


    End Sub

#Region "Berechnung Fehler und EFG"

    Private Sub TextboxenGewicht_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich3FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich3DisplayWeight8.TextChanged, RadTextBoxControlBereich3DisplayWeight7.TextChanged, RadTextBoxControlBereich3DisplayWeight6.TextChanged, RadTextBoxControlBereich3DisplayWeight5.TextChanged, RadTextBoxControlBereich3DisplayWeight4.TextChanged, RadTextBoxControlBereich3DisplayWeight3.TextChanged, RadTextBoxControlBereich3DisplayWeight2.TextChanged, RadTextBoxControlBereich3DisplayWeight1.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich2FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich2DisplayWeight8.TextChanged, RadTextBoxControlBereich2DisplayWeight7.TextChanged, RadTextBoxControlBereich2DisplayWeight6.TextChanged, RadTextBoxControlBereich2DisplayWeight5.TextChanged, RadTextBoxControlBereich2DisplayWeight4.TextChanged, RadTextBoxControlBereich2DisplayWeight3.TextChanged, RadTextBoxControlBereich2DisplayWeight2.TextChanged, RadTextBoxControlBereich2DisplayWeight1.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight8.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight7.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight6.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight5.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight4.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight3.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight2.TextChanged, RadTextBoxControlBereich1FallendDisplayWeight1.TextChanged, RadTextBoxControlBereich1DisplayWeight8.TextChanged, RadTextBoxControlBereich1DisplayWeight7.TextChanged, RadTextBoxControlBereich1DisplayWeight6.TextChanged, RadTextBoxControlBereich1DisplayWeight5.TextChanged, RadTextBoxControlBereich1DisplayWeight4.TextChanged, RadTextBoxControlBereich1DisplayWeight3.TextChanged, RadTextBoxControlBereich1DisplayWeight2.TextChanged, RadTextBoxControlBereich1DisplayWeight1.TextChanged
        If _suspendEvents Then Exit Sub

        Dim Messpunkt As String = GetMesspunkt(sender)
        Dim Bereich As String = GetBereich(sender)
        Dim Pruefung As String = GetPruefung(sender)

        Berechne(Pruefung, Bereich, Messpunkt)
    End Sub


#End Region
#End Region

    Private Sub UpdateObject()
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now
        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
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


#Region "Overrides"
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

                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

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
                                            If CType(Control, Panel).Visible = True Then
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

                            'linearität Fallend
                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
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
                                            If CType(Control, Panel).Visible = True Then
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

                        Dim intBereiche As Integer = 0
                        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                            intBereiche = 1
                        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                            intBereiche = 2
                        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                            intBereiche = 3
                        End If

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
                                        If CType(Control, Panel).Visible = True Then
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

                        'linearität Fallend
                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
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
                                        If CType(Control, Panel).Visible = True Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_9PruefungLinearitaet))



        RadGroupBoxBereich1.Text = resources.GetString("RadGroupBoxBereich1.Text")
        RadGroupBoxBereich2.Text = resources.GetString("RadGroupBoxBereich2.Text")
        RadGroupBoxBereich3.Text = resources.GetString("RadGroupBoxBereich3.Text")
        RadGroupBoxBereich1Fallend.Text = resources.GetString("RadGroupBoxBereich1Fallend.Text")
        RadGroupBoxBereich2Fallend.Text = resources.GetString("RadGroupBoxBereich2Fallend.Text")
        RadGroupBoxBereich3Fallend.Text = resources.GetString("RadGroupBoxBereich3Fallend.Text")
        RadGroupBoxPruefungLinearitaetFallend.Text = resources.GetString("RadGroupBoxPruefungLinearitaetFallend.Text")
        RadGroupBoxPruefungLinearitaetSteigend.Text = resources.GetString("RadGroupBoxPruefungLinearitaetSteigend.Text")


        lblBereich1AnzeigeGewicht.Text = resources.GetString("lblBereich1AnzeigeGewicht.Text")
        lblBereich2AnzeigeGewicht.Text = resources.GetString("lblBereich2AnzeigeGewicht.Text")
        lblBereich3AnzeigeGewicht.Text = resources.GetString("lblBereich3AnzeigeGewicht.Text")
        lblBereich1FallendAnzeigeGewicht.Text = resources.GetString("lblBereich1FallendAnzeigeGewicht.Text")
        lblBereich2FallendAnzeigeGewicht.Text = resources.GetString("lblBereich2FallendAnzeigeGewicht.Text")
        lblBereich3FallendAnzeigeGewicht.Text = resources.GetString("lblBereich3FallendAnzeigeGewicht.Text")

        lblBereich1Gewicht.Text = resources.GetString("lblBereich1Gewicht.Text")
        lblBereich2Gewicht.Text = resources.GetString("lblBereich2Gewicht.Text")
        lblBereich3Gewicht.Text = resources.GetString("lblBereich3Gewicht.Text")
        lblBereich1FallendGewicht.Text = resources.GetString("lblBereich1FallendGewicht.Text")
        lblBereich2FallendGewicht.Text = resources.GetString("lblBereich2FallendGewicht.Text")
        lblBereich3FallendGewicht.Text = resources.GetString("lblBereich3FallendGewicht.Text")

        lblBereich1FehlerGrenzen.Text = resources.GetString("lblBereich1FehlerGrenzen.Text")
        lblBereich2FehlerGrenzen.Text = resources.GetString("lblBereich2FehlerGrenzen.Text")
        lblBereich3FehlerGrenzen.Text = resources.GetString("lblBereich3FehlerGrenzen.Text")
        lblBereich1FallendFehlerGrenzen.Text = resources.GetString("lblBereich1FallendFehlerGrenzen.Text")
        lblBereich2FallendFehlerGrenzen.Text = resources.GetString("lblBereich2FallendFehlerGrenzen.Text")
        lblBereich3FallendFehlerGrenzen.Text = resources.GetString("lblBereich3FallendFehlerGrenzen.Text")

        lblBereich1EFGSpezial.Text = resources.GetString("lblBereich1EFGSpezial.Text")
        lblBereich2EFGSpezial.Text = resources.GetString("lblBereich2EFGSpezial.Text")
        lblBereich3EFGSpezial.Text = resources.GetString("lblBereich3EFGSpezial.Text")
        lblBereich1FallendEFGSpezial.Text = resources.GetString("lblBereich1FallendEFGSpezial.Text")
        lblBereich2FallendEFGSpezial.Text = resources.GetString("lblBereich2FallendEFGSpezial.Text")
        lblBereich3FallendEFGSpezial.Text = resources.GetString("lblBereich3FallendEFGSpezial.Text")


        RadButtonShowEFGSteigend.Text = resources.GetString("RadButtonShowEFGSteigend.Text")
        RadButtonShowEFGFallend.Text = resources.GetString("RadButtonShowEFGFallend.Text")



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

    ''' <summary>
    ''' aktualisieren der Oberfläche wenn nötig
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungDerRichtigkeitMitNormallast)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerRichtigkeitMitNormallast
            '   FillControls()
            LoadFromDatabase()
        End If
    End Sub

#End Region

#Region "Hilfetexte"

#End Region


    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In RadScrollablePanel1.PanelContainer.Controls
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
            '  objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

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
                    Webcontext.AddEichprozess(AktuellerBenutzer.Instance.Lizenz.HEKennung, AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

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


End Class
