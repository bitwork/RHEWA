Public Class ucoPruefungLinearitaet
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _ListPruefungPruefungLinearitaetSteigend As New List(Of PruefungLinearitaetSteigend)
    Private _ListPruefungPruefungLinearitaetFallend As New List(Of PruefungLinearitaetFallend)
    Private _currentObjPruefungLinearitaetSteigend As PruefungLinearitaetSteigend
    Private _currentObjPruefungLinearitaetFallend As PruefungLinearitaetFallend

    Private _intAnzahlMesspunkte As Integer = 4
    Private _intAnzahlUrpsMessPunkte As Integer = 4 'Diese Variable wird genutzt um zu prüfen ob etwas an den Messpunkten geändert wurde um in der DB diee änderungen nachzupflegen
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

    Private Sub RadTextBoxControlBereich1DisplayWeight1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlBereich3DisplayWeight8.Validating, RadTextBoxControlBereich3DisplayWeight7.Validating, RadTextBoxControlBereich3DisplayWeight6.Validating, RadTextBoxControlBereich3DisplayWeight5.Validating, RadTextBoxControlBereich3DisplayWeight4.Validating, RadTextBoxControlBereich3DisplayWeight3.Validating, RadTextBoxControlBereich3DisplayWeight2.Validating, RadTextBoxControlBereich3DisplayWeight1.Validating, RadTextBoxControlBereich2DisplayWeight8.Validating, RadTextBoxControlBereich2DisplayWeight7.Validating, RadTextBoxControlBereich2DisplayWeight6.Validating, RadTextBoxControlBereich2DisplayWeight5.Validating, RadTextBoxControlBereich2DisplayWeight4.Validating, RadTextBoxControlBereich2DisplayWeight3.Validating, RadTextBoxControlBereich2DisplayWeight2.Validating, RadTextBoxControlBereich2DisplayWeight1.Validating, RadTextBoxControlBereich1DisplayWeight8.Validating, RadTextBoxControlBereich1DisplayWeight7.Validating, RadTextBoxControlBereich1DisplayWeight6.Validating, RadTextBoxControlBereich1DisplayWeight5.Validating, RadTextBoxControlBereich1DisplayWeight4.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, RadTextBoxControlBereich1DisplayWeight2.Validating, RadTextBoxControlBereich1DisplayWeight1.Validating, _
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
    Private Sub RadCheckBoxBereich1VEL1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich3VEL8.MouseClick, RadCheckBoxBereich3VEL7.MouseClick, RadCheckBoxBereich3VEL6.MouseClick, RadCheckBoxBereich3VEL5.MouseClick, RadCheckBoxBereich3VEL4.MouseClick, RadCheckBoxBereich3VEL3.MouseClick, RadCheckBoxBereich3VEL2.MouseClick, RadCheckBoxBereich3VEL1.MouseClick, RadCheckBoxBereich2VEL8.MouseClick, RadCheckBoxBereich2VEL7.MouseClick, RadCheckBoxBereich2VEL6.MouseClick, RadCheckBoxBereich2VEL5.MouseClick, RadCheckBoxBereich2VEL4.MouseClick, RadCheckBoxBereich2VEL3.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich1VEL8.MouseClick, RadCheckBoxBereich1VEL7.MouseClick, RadCheckBoxBereich1VEL6.MouseClick, RadCheckBoxBereich1VEL5.MouseClick, RadCheckBoxBereich1VEL4.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub
    ''' <summary>
    ''' Verbieten vom Klicken auf die Checklisten (negiert die Eingabe um wieder den Ausgangswert zu erlangen)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxBereich1FallendVEL1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich3FallendVEL8.MouseClick, RadCheckBoxBereich3FallendVEL7.MouseClick, RadCheckBoxBereich3FallendVEL6.MouseClick, RadCheckBoxBereich3FallendVEL5.MouseClick, RadCheckBoxBereich3FallendVEL4.MouseClick, RadCheckBoxBereich3FallendVEL3.MouseClick, RadCheckBoxBereich3FallendVEL2.MouseClick, RadCheckBoxBereich3FallendVEL1.MouseClick, RadCheckBoxBereich2FallendVEL8.MouseClick, RadCheckBoxBereich2FallendVEL7.MouseClick, RadCheckBoxBereich2FallendVEL6.MouseClick, RadCheckBoxBereich2FallendVEL5.MouseClick, RadCheckBoxBereich2FallendVEL4.MouseClick, RadCheckBoxBereich2FallendVEL3.MouseClick, RadCheckBoxBereich2FallendVEL2.MouseClick, RadCheckBoxBereich2FallendVEL1.MouseClick, RadCheckBoxBereich1FallendVEL8.MouseClick, RadCheckBoxBereich1FallendVEL7.MouseClick, RadCheckBoxBereich1FallendVEL6.MouseClick, RadCheckBoxBereich1FallendVEL5.MouseClick, RadCheckBoxBereich1FallendVEL4.MouseClick, RadCheckBoxBereich1FallendVEL3.MouseClick, RadCheckBoxBereich1FallendVEL2.MouseClick, RadCheckBoxBereich1FallendVEL1.MouseClick
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub

#Region "Steigend"

#End Region
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1Weight8.TextChanged, RadTextBoxControlBereich1Weight7.TextChanged, RadTextBoxControlBereich1Weight6.TextChanged, RadTextBoxControlBereich1Weight5.TextChanged, RadTextBoxControlBereich1Weight4.TextChanged, RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        If sender.Name = "RadTextBoxControlBereich1Weight1" Then
            RadTextBoxControlBereich1FallendWeight1.Text = RadTextBoxControlBereich1Weight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight2" Then
            RadTextBoxControlBereich1FallendWeight2.Text = RadTextBoxControlBereich1Weight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight3" Then
            RadTextBoxControlBereich1FallendWeight3.Text = RadTextBoxControlBereich1Weight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight4" Then
            RadTextBoxControlBereich1FallendWeight4.Text = RadTextBoxControlBereich1Weight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight5" Then
            RadTextBoxControlBereich1FallendWeight5.Text = RadTextBoxControlBereich1Weight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight6" Then
            RadTextBoxControlBereich1FallendWeight6.Text = RadTextBoxControlBereich1Weight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight7" Then
            RadTextBoxControlBereich1FallendWeight7.Text = RadTextBoxControlBereich1Weight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich1Weight8" Then
            RadTextBoxControlBereich1FallendWeight8.Text = RadTextBoxControlBereich1Weight8.Text
        End If

        Try
            RadTextBoxControlBereich1ErrorLimit1.Text = CDec(RadTextBoxControlBereich1DisplayWeight1.Text) - CDec(RadTextBoxControlBereich1Weight1.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight1.Text) > CDec(RadTextBoxControlBereich1Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight1.Text), 1) Then
                RadCheckBoxBereich1VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight1.Text) < CDec(RadTextBoxControlBereich1Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight1.Text), 1) Then
                RadCheckBoxBereich1VEL1.Checked = False
            Else
                RadCheckBoxBereich1VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit2.Text = CDec(RadTextBoxControlBereich1DisplayWeight2.Text) - CDec(RadTextBoxControlBereich1Weight2.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight2.Text) > CDec(RadTextBoxControlBereich1Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight2.Text), 1) Then
                RadCheckBoxBereich1VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight2.Text) < CDec(RadTextBoxControlBereich1Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight2.Text), 1) Then
                RadCheckBoxBereich1VEL2.Checked = False
            Else
                RadCheckBoxBereich1VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit3.Text = CDec(RadTextBoxControlBereich1DisplayWeight3.Text) - CDec(RadTextBoxControlBereich1Weight3.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight3.Text) > CDec(RadTextBoxControlBereich1Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight3.Text), 1) Then
                RadCheckBoxBereich1VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight3.Text) < CDec(RadTextBoxControlBereich1Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight3.Text), 1) Then
                RadCheckBoxBereich1VEL3.Checked = False
            Else
                RadCheckBoxBereich1VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit4.Text = CDec(RadTextBoxControlBereich1DisplayWeight4.Text) - CDec(RadTextBoxControlBereich1Weight4.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight4.Text) > CDec(RadTextBoxControlBereich1Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight4.Text), 1) Then
                RadCheckBoxBereich1VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight4.Text) < CDec(RadTextBoxControlBereich1Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight4.Text), 1) Then
                RadCheckBoxBereich1VEL4.Checked = False
            Else
                RadCheckBoxBereich1VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit5.Text = CDec(RadTextBoxControlBereich1DisplayWeight5.Text) - CDec(RadTextBoxControlBereich1Weight5.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight5.Text) > CDec(RadTextBoxControlBereich1Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight5.Text), 1) Then
                RadCheckBoxBereich1VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight5.Text) < CDec(RadTextBoxControlBereich1Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight5.Text), 1) Then
                RadCheckBoxBereich1VEL5.Checked = False
            Else
                RadCheckBoxBereich1VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit6.Text = CDec(RadTextBoxControlBereich1DisplayWeight6.Text) - CDec(RadTextBoxControlBereich1Weight6.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight6.Text) > CDec(RadTextBoxControlBereich1Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight6.Text), 1) Then
                RadCheckBoxBereich1VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight6.Text) < CDec(RadTextBoxControlBereich1Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight6.Text), 1) Then
                RadCheckBoxBereich1VEL6.Checked = False
            Else
                RadCheckBoxBereich1VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit7.Text = CDec(RadTextBoxControlBereich1DisplayWeight7.Text) - CDec(RadTextBoxControlBereich1Weight7.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight7.Text) > CDec(RadTextBoxControlBereich1Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight7.Text), 1) Then
                RadCheckBoxBereich1VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight7.Text) < CDec(RadTextBoxControlBereich1Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight7.Text), 1) Then
                RadCheckBoxBereich1VEL7.Checked = False
            Else
                RadCheckBoxBereich1VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit8.Text = CDec(RadTextBoxControlBereich1DisplayWeight8.Text) - CDec(RadTextBoxControlBereich1Weight8.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight8.Text) > CDec(RadTextBoxControlBereich1Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight8.Text), 1) Then
                RadCheckBoxBereich1VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight8.Text) < CDec(RadTextBoxControlBereich1Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight8.Text), 1) Then
                RadCheckBoxBereich1VEL8.Checked = False
            Else
                RadCheckBoxBereich1VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try


        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2Weight8.TextChanged, RadTextBoxControlBereich2Weight7.TextChanged, RadTextBoxControlBereich2Weight6.TextChanged, RadTextBoxControlBereich2Weight5.TextChanged, RadTextBoxControlBereich2Weight4.TextChanged, RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        If sender.name = "RadTextBoxControlBereich2Weight1" Then
            RadTextBoxControlBereich2FallendWeight1.Text = RadTextBoxControlBereich2Weight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight2" Then
            RadTextBoxControlBereich2FallendWeight2.Text = RadTextBoxControlBereich2Weight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight3" Then
            RadTextBoxControlBereich2FallendWeight3.Text = RadTextBoxControlBereich2Weight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight4" Then
            RadTextBoxControlBereich2FallendWeight4.Text = RadTextBoxControlBereich2Weight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight5" Then
            RadTextBoxControlBereich2FallendWeight5.Text = RadTextBoxControlBereich2Weight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight6" Then
            RadTextBoxControlBereich2FallendWeight6.Text = RadTextBoxControlBereich2Weight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight7" Then
            RadTextBoxControlBereich2FallendWeight7.Text = RadTextBoxControlBereich2Weight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich2Weight8" Then
            RadTextBoxControlBereich2FallendWeight8.Text = RadTextBoxControlBereich2Weight8.Text
        End If

        Try
            RadTextBoxControlBereich2ErrorLimit1.Text = CDec(RadTextBoxControlBereich2DisplayWeight1.Text) - CDec(RadTextBoxControlBereich2Weight1.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight1.Text) > CDec(RadTextBoxControlBereich2Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight1.Text), 2) Then
                RadCheckBoxBereich2VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight1.Text) < CDec(RadTextBoxControlBereich2Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight1.Text), 2) Then
                RadCheckBoxBereich2VEL1.Checked = False
            Else
                RadCheckBoxBereich2VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit2.Text = CDec(RadTextBoxControlBereich2DisplayWeight2.Text) - CDec(RadTextBoxControlBereich2Weight2.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight2.Text) > CDec(RadTextBoxControlBereich2Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight2.Text), 2) Then
                RadCheckBoxBereich2VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight2.Text) < CDec(RadTextBoxControlBereich2Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight2.Text), 2) Then
                RadCheckBoxBereich2VEL2.Checked = False
            Else
                RadCheckBoxBereich2VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit3.Text = CDec(RadTextBoxControlBereich2DisplayWeight3.Text) - CDec(RadTextBoxControlBereich2Weight3.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight3.Text) > CDec(RadTextBoxControlBereich2Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight3.Text), 2) Then
                RadCheckBoxBereich2VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight3.Text) < CDec(RadTextBoxControlBereich2Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight3.Text), 2) Then
                RadCheckBoxBereich2VEL3.Checked = False
            Else
                RadCheckBoxBereich2VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit4.Text = CDec(RadTextBoxControlBereich2DisplayWeight4.Text) - CDec(RadTextBoxControlBereich2Weight4.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight4.Text) > CDec(RadTextBoxControlBereich2Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight4.Text), 2) Then
                RadCheckBoxBereich2VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight4.Text) < CDec(RadTextBoxControlBereich2Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight4.Text), 2) Then
                RadCheckBoxBereich2VEL4.Checked = False
            Else
                RadCheckBoxBereich2VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit5.Text = CDec(RadTextBoxControlBereich2DisplayWeight5.Text) - CDec(RadTextBoxControlBereich2Weight5.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight5.Text) > CDec(RadTextBoxControlBereich2Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight5.Text), 2) Then
                RadCheckBoxBereich2VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight5.Text) < CDec(RadTextBoxControlBereich2Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight5.Text), 2) Then
                RadCheckBoxBereich2VEL5.Checked = False
            Else
                RadCheckBoxBereich2VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit6.Text = CDec(RadTextBoxControlBereich2DisplayWeight6.Text) - CDec(RadTextBoxControlBereich2Weight6.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight6.Text) > CDec(RadTextBoxControlBereich2Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight6.Text), 2) Then
                RadCheckBoxBereich2VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight6.Text) < CDec(RadTextBoxControlBereich2Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight6.Text), 2) Then
                RadCheckBoxBereich2VEL6.Checked = False
            Else
                RadCheckBoxBereich2VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit7.Text = CDec(RadTextBoxControlBereich2DisplayWeight7.Text) - CDec(RadTextBoxControlBereich2Weight7.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight7.Text) > CDec(RadTextBoxControlBereich2Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight7.Text), 2) Then
                RadCheckBoxBereich2VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight7.Text) < CDec(RadTextBoxControlBereich2Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight7.Text), 2) Then
                RadCheckBoxBereich2VEL7.Checked = False
            Else
                RadCheckBoxBereich2VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit8.Text = CDec(RadTextBoxControlBereich2DisplayWeight8.Text) - CDec(RadTextBoxControlBereich2Weight8.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight8.Text) > CDec(RadTextBoxControlBereich2Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight8.Text), 2) Then
                RadCheckBoxBereich2VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight8.Text) < CDec(RadTextBoxControlBereich2Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight8.Text), 2) Then
                RadCheckBoxBereich2VEL8.Checked = False
            Else
                RadCheckBoxBereich2VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try



        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3Weight8.TextChanged, RadTextBoxControlBereich3Weight7.TextChanged, RadTextBoxControlBereich3Weight6.TextChanged, RadTextBoxControlBereich3Weight5.TextChanged, RadTextBoxControlBereich3Weight4.TextChanged, RadTextBoxControlBereich3Weight3.TextChanged, RadTextBoxControlBereich3Weight2.TextChanged, RadTextBoxControlBereich3Weight1.TextChanged


        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        If sender.name = "RadTextBoxControlBereich3Weight1" Then
            RadTextBoxControlBereich3FallendWeight1.Text = RadTextBoxControlBereich3Weight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight2" Then
            RadTextBoxControlBereich3FallendWeight2.Text = RadTextBoxControlBereich3Weight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight3" Then
            RadTextBoxControlBereich3FallendWeight3.Text = RadTextBoxControlBereich3Weight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight4" Then
            RadTextBoxControlBereich3FallendWeight4.Text = RadTextBoxControlBereich3Weight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight5" Then
            RadTextBoxControlBereich3FallendWeight5.Text = RadTextBoxControlBereich3Weight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight6" Then
            RadTextBoxControlBereich3FallendWeight6.Text = RadTextBoxControlBereich3Weight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight7" Then
            RadTextBoxControlBereich3FallendWeight7.Text = RadTextBoxControlBereich3Weight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich3Weight8" Then
            RadTextBoxControlBereich3FallendWeight8.Text = RadTextBoxControlBereich3Weight8.Text
        End If

        Try
            RadTextBoxControlBereich3ErrorLimit1.Text = CDec(RadTextBoxControlBereich3DisplayWeight1.Text) - CDec(RadTextBoxControlBereich3Weight1.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight1.Text) > CDec(RadTextBoxControlBereich3Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight1.Text), 3) Then
                RadCheckBoxBereich3VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight1.Text) < CDec(RadTextBoxControlBereich3Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight1.Text), 3) Then
                RadCheckBoxBereich3VEL1.Checked = False
            Else
                RadCheckBoxBereich3VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit2.Text = CDec(RadTextBoxControlBereich3DisplayWeight2.Text) - CDec(RadTextBoxControlBereich3Weight2.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight2.Text) > CDec(RadTextBoxControlBereich3Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight2.Text), 3) Then
                RadCheckBoxBereich3VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight2.Text) < CDec(RadTextBoxControlBereich3Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight2.Text), 3) Then
                RadCheckBoxBereich3VEL2.Checked = False
            Else
                RadCheckBoxBereich3VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit3.Text = CDec(RadTextBoxControlBereich3DisplayWeight3.Text) - CDec(RadTextBoxControlBereich3Weight3.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight3.Text) > CDec(RadTextBoxControlBereich3Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight3.Text), 3) Then
                RadCheckBoxBereich3VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight3.Text) < CDec(RadTextBoxControlBereich3Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight3.Text), 3) Then
                RadCheckBoxBereich3VEL3.Checked = False
            Else
                RadCheckBoxBereich3VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit4.Text = CDec(RadTextBoxControlBereich3DisplayWeight4.Text) - CDec(RadTextBoxControlBereich3Weight4.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight4.Text) > CDec(RadTextBoxControlBereich3Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight4.Text), 3) Then
                RadCheckBoxBereich3VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight4.Text) < CDec(RadTextBoxControlBereich3Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight4.Text), 3) Then
                RadCheckBoxBereich3VEL4.Checked = False
            Else
                RadCheckBoxBereich3VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit5.Text = CDec(RadTextBoxControlBereich3DisplayWeight5.Text) - CDec(RadTextBoxControlBereich3Weight5.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight5.Text) > CDec(RadTextBoxControlBereich3Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight5.Text), 3) Then
                RadCheckBoxBereich3VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight5.Text) < CDec(RadTextBoxControlBereich3Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight5.Text), 3) Then
                RadCheckBoxBereich3VEL5.Checked = False
            Else
                RadCheckBoxBereich3VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit6.Text = CDec(RadTextBoxControlBereich3DisplayWeight6.Text) - CDec(RadTextBoxControlBereich3Weight6.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight6.Text) > CDec(RadTextBoxControlBereich3Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight6.Text), 3) Then
                RadCheckBoxBereich3VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight6.Text) < CDec(RadTextBoxControlBereich3Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight6.Text), 3) Then
                RadCheckBoxBereich3VEL6.Checked = False
            Else
                RadCheckBoxBereich3VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit7.Text = CDec(RadTextBoxControlBereich3DisplayWeight7.Text) - CDec(RadTextBoxControlBereich3Weight7.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight7.Text) > CDec(RadTextBoxControlBereich3Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight7.Text), 3) Then
                RadCheckBoxBereich3VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight7.Text) < CDec(RadTextBoxControlBereich3Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight7.Text), 3) Then
                RadCheckBoxBereich3VEL7.Checked = False
            Else
                RadCheckBoxBereich3VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit8.Text = CDec(RadTextBoxControlBereich3DisplayWeight8.Text) - CDec(RadTextBoxControlBereich3Weight8.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight8.Text) > CDec(RadTextBoxControlBereich3Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight8.Text), 3) Then
                RadCheckBoxBereich3VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight8.Text) < CDec(RadTextBoxControlBereich3Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight8.Text), 3) Then
                RadCheckBoxBereich3VEL8.Checked = False
            Else
                RadCheckBoxBereich3VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try


        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub

#Region "Fallend"

    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1Fallend_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendWeight8.TextChanged, RadTextBoxControlBereich1FallendWeight7.TextChanged, RadTextBoxControlBereich1FallendWeight6.TextChanged, RadTextBoxControlBereich1FallendWeight5.TextChanged, RadTextBoxControlBereich1FallendWeight4.TextChanged, RadTextBoxControlBereich1FallendWeight3.TextChanged, RadTextBoxControlBereich1FallendWeight2.TextChanged, RadTextBoxControlBereich1FallendWeight1.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True

        If sender.name = "RadTextBoxControlBereich1FallendWeight1" Then
            RadTextBoxControlBereich1Weight1.Text = RadTextBoxControlBereich1FallendWeight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight2" Then
            RadTextBoxControlBereich1Weight2.Text = RadTextBoxControlBereich1FallendWeight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight3" Then
            RadTextBoxControlBereich1Weight3.Text = RadTextBoxControlBereich1FallendWeight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight4" Then
            RadTextBoxControlBereich1Weight4.Text = RadTextBoxControlBereich1FallendWeight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight5" Then
            RadTextBoxControlBereich1Weight5.Text = RadTextBoxControlBereich1FallendWeight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight6" Then
            RadTextBoxControlBereich1Weight6.Text = RadTextBoxControlBereich1FallendWeight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight7" Then
            RadTextBoxControlBereich1Weight7.Text = RadTextBoxControlBereich1FallendWeight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich1FallendWeight8" Then
            RadTextBoxControlBereich1Weight8.Text = RadTextBoxControlBereich1FallendWeight8.Text
        End If

        Try
            RadTextBoxControlBereich1FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich1FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich1FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight1.Text), 1) Then
                RadCheckBoxBereich1FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich1FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight1.Text), 1) Then
                RadCheckBoxBereich1FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich1FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich1FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight2.Text), 1) Then
                RadCheckBoxBereich1FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich1FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight2.Text), 1) Then
                RadCheckBoxBereich1FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich1FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich1FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight3.Text), 1) Then
                RadCheckBoxBereich1FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich1FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight3.Text), 1) Then
                RadCheckBoxBereich1FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich1FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich1FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight4.Text), 1) Then
                RadCheckBoxBereich1FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich1FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight4.Text), 1) Then
                RadCheckBoxBereich1FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich1FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich1FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight5.Text), 1) Then
                RadCheckBoxBereich1FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich1FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight5.Text), 1) Then
                RadCheckBoxBereich1FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich1FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich1FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight6.Text), 1) Then
                RadCheckBoxBereich1FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich1FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight6.Text), 1) Then
                RadCheckBoxBereich1FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich1FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich1FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight7.Text), 1) Then
                RadCheckBoxBereich1FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich1FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight7.Text), 1) Then
                RadCheckBoxBereich1FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich1FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich1FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight8.Text), 1) Then
                RadCheckBoxBereich1FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich1FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight8.Text), 1) Then
                RadCheckBoxBereich1FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try


        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich2Fallend_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendWeight8.TextChanged, RadTextBoxControlBereich2FallendWeight7.TextChanged, RadTextBoxControlBereich2FallendWeight6.TextChanged, RadTextBoxControlBereich2FallendWeight5.TextChanged, RadTextBoxControlBereich2FallendWeight4.TextChanged, RadTextBoxControlBereich2FallendWeight3.TextChanged, RadTextBoxControlBereich2FallendWeight2.TextChanged, RadTextBoxControlBereich2FallendWeight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True


        If sender.name = "RadTextBoxControlBereich2FallendWeight1" Then
            RadTextBoxControlBereich2Weight1.Text = RadTextBoxControlBereich2FallendWeight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight2" Then
            RadTextBoxControlBereich2Weight2.Text = RadTextBoxControlBereich2FallendWeight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight3" Then
            RadTextBoxControlBereich2Weight3.Text = RadTextBoxControlBereich2FallendWeight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight4" Then
            RadTextBoxControlBereich2Weight4.Text = RadTextBoxControlBereich2FallendWeight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight5" Then
            RadTextBoxControlBereich2Weight5.Text = RadTextBoxControlBereich2FallendWeight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight6" Then
            RadTextBoxControlBereich2Weight6.Text = RadTextBoxControlBereich2FallendWeight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight7" Then
            RadTextBoxControlBereich2Weight7.Text = RadTextBoxControlBereich2FallendWeight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich2FallendWeight8" Then
            RadTextBoxControlBereich2Weight8.Text = RadTextBoxControlBereich2FallendWeight8.Text
        End If

        Try
            RadTextBoxControlBereich2FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich2FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich2FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight1.Text), 2) Then
                RadCheckBoxBereich2FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich2FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight1.Text), 2) Then
                RadCheckBoxBereich2FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich2FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich2FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight2.Text), 2) Then
                RadCheckBoxBereich2FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich2FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight2.Text), 2) Then
                RadCheckBoxBereich2FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich2FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich2FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight3.Text), 2) Then
                RadCheckBoxBereich2FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich2FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight3.Text), 2) Then
                RadCheckBoxBereich2FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich2FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich2FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight4.Text), 2) Then
                RadCheckBoxBereich2FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich2FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight4.Text), 2) Then
                RadCheckBoxBereich2FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich2FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich2FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight5.Text), 2) Then
                RadCheckBoxBereich2FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich2FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight5.Text), 2) Then
                RadCheckBoxBereich2FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich2FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich2FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight6.Text), 2) Then
                RadCheckBoxBereich2FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich2FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight6.Text), 2) Then
                RadCheckBoxBereich2FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich2FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich2FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight7.Text), 2) Then
                RadCheckBoxBereich2FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich2FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight7.Text), 2) Then
                RadCheckBoxBereich2FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich2FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich2FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight8.Text), 2) Then
                RadCheckBoxBereich2FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich2FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight8.Text), 2) Then
                RadCheckBoxBereich2FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try



        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss das Dirty Flag gesetzt werden und EFG und Error neu berechnet werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich3Fallend_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendWeight8.TextChanged, RadTextBoxControlBereich3FallendWeight7.TextChanged, RadTextBoxControlBereich3FallendWeight6.TextChanged, RadTextBoxControlBereich3FallendWeight5.TextChanged, RadTextBoxControlBereich3FallendWeight4.TextChanged, RadTextBoxControlBereich3FallendWeight3.TextChanged, RadTextBoxControlBereich3FallendWeight2.TextChanged, RadTextBoxControlBereich3FallendWeight1.TextChanged


        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True


        If sender.name = "RadTextBoxControlBereich3FallendWeight1" Then
            RadTextBoxControlBereich3Weight1.Text = RadTextBoxControlBereich3FallendWeight1.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight2" Then
            RadTextBoxControlBereich3Weight2.Text = RadTextBoxControlBereich3FallendWeight2.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight3" Then
            RadTextBoxControlBereich3Weight3.Text = RadTextBoxControlBereich3FallendWeight3.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight4" Then
            RadTextBoxControlBereich3Weight4.Text = RadTextBoxControlBereich3FallendWeight4.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight5" Then
            RadTextBoxControlBereich3Weight5.Text = RadTextBoxControlBereich3FallendWeight5.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight6" Then
            RadTextBoxControlBereich3Weight6.Text = RadTextBoxControlBereich3FallendWeight6.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight7" Then
            RadTextBoxControlBereich3Weight7.Text = RadTextBoxControlBereich3FallendWeight7.Text
        ElseIf sender.name = "RadTextBoxControlBereich3FallendWeight8" Then
            RadTextBoxControlBereich3Weight8.Text = RadTextBoxControlBereich3FallendWeight8.Text
        End If

        Try
            RadTextBoxControlBereich3FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich3FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich3FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight1.Text), 3) Then
                RadCheckBoxBereich3FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich3FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight1.Text), 3) Then
                RadCheckBoxBereich3FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich3FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich3FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight2.Text), 3) Then
                RadCheckBoxBereich3FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich3FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight2.Text), 3) Then
                RadCheckBoxBereich3FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich3FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich3FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight3.Text), 3) Then
                RadCheckBoxBereich3FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich3FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight3.Text), 3) Then
                RadCheckBoxBereich3FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich3FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich3FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight4.Text), 3) Then
                RadCheckBoxBereich3FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich3FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight4.Text), 3) Then
                RadCheckBoxBereich3FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich3FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich3FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight5.Text), 3) Then
                RadCheckBoxBereich3FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich3FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight5.Text), 3) Then
                RadCheckBoxBereich3FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich3FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich3FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight6.Text), 3) Then
                RadCheckBoxBereich3FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich3FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight6.Text), 3) Then
                RadCheckBoxBereich3FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich3FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich3FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight7.Text), 3) Then
                RadCheckBoxBereich3FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich3FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight7.Text), 3) Then
                RadCheckBoxBereich3FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich3FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich3FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight8.Text), 3) Then
                RadCheckBoxBereich3FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich3FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight8.Text), 3) Then
                RadCheckBoxBereich3FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try


        'neu berechnen der Fehler und EFG

        _suspendEvents = False

    End Sub


#End Region

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
        If _intAnzahlMesspunkte = 4 Then
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
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.ID = objEichprozess.ID).FirstOrDefault

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetSteigend = query.ToList
                query = Nothing

                Dim query2 = From a In context.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungPruefungLinearitaetFallend = query2.ToList
                query2 = Nothing
            End Using
        Else
            For Each obj In objEichprozess.Eichprotokoll.PruefungLinearitaetSteigend
                _ListPruefungPruefungLinearitaetSteigend.Add(obj)
            Next
            For Each obj In objEichprozess.Eichprotokoll.PruefungLinearitaetFallend
                _ListPruefungPruefungLinearitaetFallend.Add(obj)
            Next
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


    Private Sub FillLinearitaetSteigend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente

        'Last Berechnen

       

        'bereich 1
        RadTextBoxControlBereich1Weight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1
        RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1000
        RadTextBoxControlBereich1Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1500
        RadTextBoxControlBereich1Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2500
        RadTextBoxControlBereich1Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3000
        RadTextBoxControlBereich1Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3500

        'bereich 2
        Try
            RadTextBoxControlBereich2Weight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
            RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500
            RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1000
            RadTextBoxControlBereich2Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1500
            RadTextBoxControlBereich2Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000
            RadTextBoxControlBereich2Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2500
            RadTextBoxControlBereich2Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3000
            RadTextBoxControlBereich2Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3500

        Catch ex As Exception

        End Try
        'bereich 3
        Try
            RadTextBoxControlBereich3Weight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
            RadTextBoxControlBereich3Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500
            RadTextBoxControlBereich3Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1000
            RadTextBoxControlBereich3Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1500
            RadTextBoxControlBereich3Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000
            RadTextBoxControlBereich3Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2500
            RadTextBoxControlBereich3Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3000
            RadTextBoxControlBereich3Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3500
        Catch ex As Exception
        End Try


        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "1" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight1.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight1.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "2" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight2.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight2.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "3" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight3.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight3.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "4" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight4.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight4.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "5" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight5.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight5.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If
      
        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "6" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight6.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight6.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "7" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight7.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight7.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "8" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich1Weight8.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich1DisplayWeight8.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If


        'bereich 2

        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "1" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight1.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight1.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "2" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight2.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight2.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "3" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight3.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight3.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "4" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight4.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight4.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "5" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight5.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight5.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "6" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight6.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight6.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "7" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight7.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight7.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "8" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich2Weight8.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich2DisplayWeight8.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If


        'bereich 3

        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "1" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight1.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight1.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "2" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight2.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight2.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "3" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight3.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight3.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "4" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight4.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight4.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "5" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight5.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight5.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "6" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight6.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight6.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "7" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight7.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight7.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetSteigend = Nothing
        _currentObjPruefungLinearitaetSteigend = (From o In _ListPruefungPruefungLinearitaetSteigend Where o.Messpunkt = "8" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetSteigend Is Nothing Then
            RadTextBoxControlBereich3Weight8.Text = _currentObjPruefungLinearitaetSteigend.Last
            RadTextBoxControlBereich3DisplayWeight8.Text = _currentObjPruefungLinearitaetSteigend.Anzeige
        End If

    End Sub


    Private Sub FillLinearitaetFallend()
        ''ein ausblenden von WZ Bereichenen
        EinAusblendenVonMessBereichen()

        'füllen der berechnenten Steuerelemente

        'Last Berechnen


        'bereich 1
        RadTextBoxControlBereich1FallendWeight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1
        RadTextBoxControlBereich1FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500
        RadTextBoxControlBereich1FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1000
        RadTextBoxControlBereich1FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1500
        RadTextBoxControlBereich1FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000
        RadTextBoxControlBereich1FallendWeight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2500
        RadTextBoxControlBereich1FallendWeight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3000
        RadTextBoxControlBereich1FallendWeight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 3500

        'bereich 2
        Try
            RadTextBoxControlBereich2FallendWeight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
            RadTextBoxControlBereich2FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500
            RadTextBoxControlBereich2FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1000
            RadTextBoxControlBereich2FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1500
            RadTextBoxControlBereich2FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000
            RadTextBoxControlBereich2FallendWeight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2500
            RadTextBoxControlBereich2FallendWeight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3000
            RadTextBoxControlBereich2FallendWeight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 3500

        Catch ex As Exception

        End Try
        'bereich 3
        Try
            RadTextBoxControlBereich3FallendWeight1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
            RadTextBoxControlBereich3FallendWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500
            RadTextBoxControlBereich3FallendWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1000
            RadTextBoxControlBereich3FallendWeight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1500
            RadTextBoxControlBereich3FallendWeight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000
            RadTextBoxControlBereich3FallendWeight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2500
            RadTextBoxControlBereich3FallendWeight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3000
            RadTextBoxControlBereich3FallendWeight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 3500
        Catch ex As Exception
        End Try



        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "1" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight1.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight1.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "2" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight2.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight2.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "3" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight3.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight3.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "4" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight4.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight4.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "5" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight5.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight5.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "6" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight6.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight6.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "7" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight7.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight7.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "8" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich1FallendWeight8.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich1FallendDisplayWeight8.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If


        'bereich 2

        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "1" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight1.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight1.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "2" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight2.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight2.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "3" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight3.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight3.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "4" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight4.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight4.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "5" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight5.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight5.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "6" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight6.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight6.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "7" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight7.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight7.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "8" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich2FallendWeight8.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich2FallendDisplayWeight8.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If


        'bereich 3

        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "1" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight1.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight1.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "2" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight2.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight2.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "3" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight3.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight3.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "4" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight4.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight4.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "5" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight5.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight5.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "6" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight6.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight6.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "7" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight7.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight7.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungLinearitaetFallend = Nothing
        _currentObjPruefungLinearitaetFallend = (From o In _ListPruefungPruefungLinearitaetFallend Where o.Messpunkt = "8" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungLinearitaetFallend Is Nothing Then
            RadTextBoxControlBereich3FallendWeight8.Text = _currentObjPruefungLinearitaetFallend.Last
            RadTextBoxControlBereich3FallendDisplayWeight8.Text = _currentObjPruefungLinearitaetFallend.Anzeige
        End If

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

        FillLinearitaetFallend
       
        'fokus setzen auf erstes Steuerelement
        'RadTextBoxControlWaageHoechstlast1.Focus()

    End Sub


    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()

    End Sub

    Private Sub UpdatePruefungsLinSteigendObject(ByVal PObjPruefung As PruefungLinearitaetSteigend)
        If PObjPruefung.Bereich = 1 Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL2.Checked
            End If
            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL3.Checked
            End If
            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL4.Checked
            End If
            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL5.Checked
            End If
            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL6.Checked
            End If
            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL7.Checked
            End If
            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL8.Checked
            End If

        ElseIf PObjPruefung.Bereich = "2" Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL2.Checked
            End If
            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL3.Checked
            End If
            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL4.Checked
            End If
            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL5.Checked
            End If
            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL6.Checked
            End If
            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL7.Checked
            End If

            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL8.Checked
            End If


        ElseIf PObjPruefung.Bereich = 3 Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL2.Checked
            End If

            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL3.Checked
            End If

            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL4.Checked
            End If

            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL5.Checked
            End If

            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL6.Checked
            End If

            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL7.Checked
            End If

            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL8.Checked
            End If



        End If
    End Sub
    Private Sub UpdatePruefungsLinFallendObject(ByVal PObjPruefung As PruefungLinearitaetFallend)
        If PObjPruefung.Bereich = 1 Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL2.Checked
            End If
            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL3.Checked
            End If
            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL4.Checked
            End If
            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL5.Checked
            End If
            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL6.Checked
            End If
            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL7.Checked
            End If
            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich1FallendWeight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1FallendDisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1FallendErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich1FallendVEL8.Checked
            End If

        ElseIf PObjPruefung.Bereich = "2" Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL2.Checked
            End If
            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL3.Checked
            End If
            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL4.Checked
            End If
            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL5.Checked
            End If
            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL6.Checked
            End If
            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL7.Checked
            End If

            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich2FallendWeight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2FallendDisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2FallendErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich2FallendVEL8.Checked
            End If


        ElseIf PObjPruefung.Bereich = 3 Then
            If PObjPruefung.Messpunkt = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL1.Checked
            End If
            If PObjPruefung.Messpunkt = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL2.Checked
            End If

            If PObjPruefung.Messpunkt = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL3.Checked
            End If

            If PObjPruefung.Messpunkt = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL4.Checked
            End If

            If PObjPruefung.Messpunkt = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL5.Checked
            End If

            If PObjPruefung.Messpunkt = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL6.Checked
            End If

            If PObjPruefung.Messpunkt = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL7.Checked
            End If

            If PObjPruefung.Messpunkt = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich3FallendWeight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3FallendDisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3FallendErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich3FallendVEL8.Checked
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

        'steigend
        If RadCheckBoxBereich1VEL1.Checked = False And RadCheckBoxBereich1VEL1.Visible = True Or _
     RadCheckBoxBereich1VEL2.Checked = False And RadCheckBoxBereich1VEL2.Visible = True Or _
     RadCheckBoxBereich1VEL3.Checked = False And RadCheckBoxBereich1VEL3.Visible = True Or _
     RadCheckBoxBereich1VEL4.Checked = False And RadCheckBoxBereich1VEL4.Visible = True Or _
     RadCheckBoxBereich1VEL5.Checked = False And RadCheckBoxBereich1VEL5.Visible = True Or _
     RadCheckBoxBereich1VEL6.Checked = False And RadCheckBoxBereich1VEL6.Visible = True Or _
     RadCheckBoxBereich1VEL7.Checked = False And RadCheckBoxBereich1VEL7.Visible = True Or _
     RadCheckBoxBereich1VEL8.Checked = False And RadCheckBoxBereich1VEL8.Visible = True Or _
     RadCheckBoxBereich2VEL1.Checked = False And RadCheckBoxBereich2VEL1.Visible = True Or _
     RadCheckBoxBereich2VEL2.Checked = False And RadCheckBoxBereich2VEL2.Visible = True Or _
     RadCheckBoxBereich2VEL3.Checked = False And RadCheckBoxBereich2VEL3.Visible = True Or _
     RadCheckBoxBereich2VEL4.Checked = False And RadCheckBoxBereich2VEL4.Visible = True Or _
     RadCheckBoxBereich2VEL5.Checked = False And RadCheckBoxBereich2VEL5.Visible = True Or _
     RadCheckBoxBereich2VEL6.Checked = False And RadCheckBoxBereich2VEL6.Visible = True Or _
     RadCheckBoxBereich2VEL7.Checked = False And RadCheckBoxBereich2VEL7.Visible = True Or _
     RadCheckBoxBereich2VEL8.Checked = False And RadCheckBoxBereich2VEL8.Visible = True Or _
     RadCheckBoxBereich3VEL1.Checked = False And RadCheckBoxBereich3VEL1.Visible = True Or _
     RadCheckBoxBereich3VEL2.Checked = False And RadCheckBoxBereich3VEL2.Visible = True Or _
     RadCheckBoxBereich3VEL3.Checked = False And RadCheckBoxBereich3VEL3.Visible = True Or _
     RadCheckBoxBereich3VEL4.Checked = False And RadCheckBoxBereich3VEL4.Visible = True Or _
     RadCheckBoxBereich3VEL5.Checked = False And RadCheckBoxBereich3VEL5.Visible = True Or _
     RadCheckBoxBereich3VEL6.Checked = False And RadCheckBoxBereich3VEL6.Visible = True Or _
     RadCheckBoxBereich3VEL7.Checked = False And RadCheckBoxBereich3VEL7.Visible = True Or _
     RadCheckBoxBereich3VEL8.Checked = False And RadCheckBoxBereich3VEL8.Visible = True Then
            AbortSaveing = True
        End If

        'fallend
        If RadCheckBoxBereich1FallendVEL1.Checked = False And RadCheckBoxBereich1FallendVEL1.Visible = True Or _
    RadCheckBoxBereich1FallendVEL2.Checked = False And RadCheckBoxBereich1FallendVEL2.Visible = True Or _
    RadCheckBoxBereich1FallendVEL3.Checked = False And RadCheckBoxBereich1FallendVEL3.Visible = True Or _
    RadCheckBoxBereich1FallendVEL4.Checked = False And RadCheckBoxBereich1FallendVEL4.Visible = True Or _
    RadCheckBoxBereich1FallendVEL5.Checked = False And RadCheckBoxBereich1FallendVEL5.Visible = True Or _
    RadCheckBoxBereich1FallendVEL6.Checked = False And RadCheckBoxBereich1FallendVEL6.Visible = True Or _
    RadCheckBoxBereich1FallendVEL7.Checked = False And RadCheckBoxBereich1FallendVEL7.Visible = True Or _
    RadCheckBoxBereich1FallendVEL8.Checked = False And RadCheckBoxBereich1FallendVEL8.Visible = True Or _
    RadCheckBoxBereich2FallendVEL1.Checked = False And RadCheckBoxBereich2FallendVEL1.Visible = True Or _
    RadCheckBoxBereich2FallendVEL2.Checked = False And RadCheckBoxBereich2FallendVEL2.Visible = True Or _
    RadCheckBoxBereich2FallendVEL3.Checked = False And RadCheckBoxBereich2FallendVEL3.Visible = True Or _
    RadCheckBoxBereich2FallendVEL4.Checked = False And RadCheckBoxBereich2FallendVEL4.Visible = True Or _
    RadCheckBoxBereich2FallendVEL5.Checked = False And RadCheckBoxBereich2FallendVEL5.Visible = True Or _
    RadCheckBoxBereich2FallendVEL6.Checked = False And RadCheckBoxBereich2FallendVEL6.Visible = True Or _
    RadCheckBoxBereich2FallendVEL7.Checked = False And RadCheckBoxBereich2FallendVEL7.Visible = True Or _
    RadCheckBoxBereich2FallendVEL8.Checked = False And RadCheckBoxBereich2FallendVEL8.Visible = True Or _
    RadCheckBoxBereich3FallendVEL1.Checked = False And RadCheckBoxBereich3FallendVEL1.Visible = True Or _
    RadCheckBoxBereich3FallendVEL2.Checked = False And RadCheckBoxBereich3FallendVEL2.Visible = True Or _
    RadCheckBoxBereich3FallendVEL3.Checked = False And RadCheckBoxBereich3FallendVEL3.Visible = True Or _
    RadCheckBoxBereich3FallendVEL4.Checked = False And RadCheckBoxBereich3FallendVEL4.Visible = True Or _
    RadCheckBoxBereich3FallendVEL5.Checked = False And RadCheckBoxBereich3FallendVEL5.Visible = True Or _
    RadCheckBoxBereich3FallendVEL6.Checked = False And RadCheckBoxBereich3FallendVEL6.Visible = True Or _
    RadCheckBoxBereich3FallendVEL7.Checked = False And RadCheckBoxBereich3FallendVEL7.Visible = True Or _
    RadCheckBoxBereich3FallendVEL8.Checked = False And RadCheckBoxBereich3FallendVEL8.Visible = True Then
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


    ''' <summary>
    ''' Berechnet die Eichfehlergrenzen anhand der Last (wählt somit den EFG Bereich aus und des Eichwerts bei Mehrbereichswaagen entpsrechend dem Bereich)
    ''' </summary>
    ''' <param name="Gewicht"></param>
    ''' <param name="Bereich"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEFG(ByVal Gewicht As Decimal, ByVal Bereich As Integer) As Decimal
        If Bereich = 1 Then
            If Gewicht > 2000 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            ElseIf Gewicht > 500 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            Else
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            End If
        ElseIf Bereich = 2 Then
            If Gewicht > 2000 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            ElseIf Gewicht > 500 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            Else
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            End If
        ElseIf Bereich = 3 Then
            If Gewicht > 2000 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            ElseIf Gewicht > 500 Then
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            Else
                Return Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            End If
        Else
            Throw New Exception
        End If

    End Function
    ''' <summary>
    ''' je nach anzahl von eingestellten Messpunkten werden die panels ein oder ausgeblendet
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EinAusblendenVonMessBereichen()
        'messpunkte auslesen
      


        'Messpunkte ausblenden
        Select Case _intAnzahlMesspunkte
            Case 4
                'steigend
                PanelBereich1WZ5.Visible = False
                PanelBereich1WZ6.Visible = False
                PanelBereich1WZ7.Visible = False
                PanelBereich1WZ8.Visible = False

                PanelBereich2WZ5.Visible = False
                PanelBereich2WZ6.Visible = False
                PanelBereich2WZ7.Visible = False
                PanelBereich2WZ8.Visible = False

                PanelBereich3WZ5.Visible = False
                PanelBereich3WZ6.Visible = False
                PanelBereich3WZ7.Visible = False
                PanelBereich3WZ8.Visible = False
                'fallend
                PanelBereich1FallendWZ5.Visible = False
                PanelBereich1FallendWZ6.Visible = False
                PanelBereich1FallendWZ7.Visible = False
                PanelBereich1FallendWZ8.Visible = False

                PanelBereich2FallendWZ5.Visible = False
                PanelBereich2FallendWZ6.Visible = False
                PanelBereich2FallendWZ7.Visible = False
                PanelBereich2FallendWZ8.Visible = False

                PanelBereich3FallendWZ5.Visible = False
                PanelBereich3FallendWZ6.Visible = False
                PanelBereich3FallendWZ7.Visible = False
                PanelBereich3FallendWZ8.Visible = False
            Case 5
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = False
                PanelBereich1WZ7.Visible = False
                PanelBereich1WZ8.Visible = False

                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = False
                PanelBereich2WZ7.Visible = False
                PanelBereich2WZ8.Visible = False

                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = False
                PanelBereich3WZ7.Visible = False
                PanelBereich3WZ8.Visible = False

                'fallend
                PanelBereich1FallendWZ5.Visible = True
                PanelBereich1FallendWZ6.Visible = False
                PanelBereich1FallendWZ7.Visible = False
                PanelBereich1FallendWZ8.Visible = False

                PanelBereich2FallendWZ5.Visible = True
                PanelBereich2FallendWZ6.Visible = False
                PanelBereich2FallendWZ7.Visible = False
                PanelBereich2FallendWZ8.Visible = False

                PanelBereich3FallendWZ5.Visible = True
                PanelBereich3FallendWZ6.Visible = False
                PanelBereich3FallendWZ7.Visible = False
                PanelBereich3FallendWZ8.Visible = False
            Case 6
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = False
                PanelBereich1WZ8.Visible = False

                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = False
                PanelBereich2WZ8.Visible = False

                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = False
                PanelBereich3WZ8.Visible = False

                'fallend
                PanelBereich1FallendWZ5.Visible = True
                PanelBereich1FallendWZ6.Visible = True
                PanelBereich1FallendWZ7.Visible = False
                PanelBereich1FallendWZ8.Visible = False

                PanelBereich2FallendWZ5.Visible = True
                PanelBereich2FallendWZ6.Visible = True
                PanelBereich2FallendWZ7.Visible = False
                PanelBereich2FallendWZ8.Visible = False

                PanelBereich3FallendWZ5.Visible = True
                PanelBereich3FallendWZ6.Visible = True
                PanelBereich3FallendWZ7.Visible = False
                PanelBereich3FallendWZ8.Visible = False
            Case 7
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = False

                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = False

                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = False

                'fallend
                PanelBereich1FallendWZ5.Visible = True
                PanelBereich1FallendWZ6.Visible = True
                PanelBereich1FallendWZ7.Visible = True
                PanelBereich1FallendWZ8.Visible = False

                PanelBereich2FallendWZ5.Visible = True
                PanelBereich2FallendWZ6.Visible = True
                PanelBereich2FallendWZ7.Visible = True
                PanelBereich2FallendWZ8.Visible = False

                PanelBereich3FallendWZ5.Visible = True
                PanelBereich3FallendWZ6.Visible = True
                PanelBereich3FallendWZ7.Visible = True
                PanelBereich3FallendWZ8.Visible = False
            Case 8
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True

                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True

                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True

                'fallend
                PanelBereich1FallendWZ5.Visible = True
                PanelBereich1FallendWZ6.Visible = True
                PanelBereich1FallendWZ7.Visible = True
                PanelBereich1FallendWZ8.Visible = True

                PanelBereich2FallendWZ5.Visible = True
                PanelBereich2FallendWZ6.Visible = True
                PanelBereich2FallendWZ7.Visible = True
                PanelBereich2FallendWZ8.Visible = True

                PanelBereich3FallendWZ5.Visible = True
                PanelBereich3FallendWZ6.Visible = True
                PanelBereich3FallendWZ7.Visible = True
                PanelBereich3FallendWZ8.Visible = True
        End Select
    End Sub

   

#Region "Berechnung Fehler und EFG"
#Region "Steigend"
#Region "Bereich 1"
    Private Sub RadTextBoxControlBereich1DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit1.Text = CDec(RadTextBoxControlBereich1DisplayWeight1.Text) - CDec(RadTextBoxControlBereich1Weight1.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight1.Text) > CDec(RadTextBoxControlBereich1Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight1.Text), 1) Then
                RadCheckBoxBereich1VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight1.Text) < CDec(RadTextBoxControlBereich1Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight1.Text), 1) Then
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

            If CDec(RadTextBoxControlBereich1DisplayWeight2.Text) > CDec(RadTextBoxControlBereich1Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight2.Text), 1) Then
                RadCheckBoxBereich1VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight2.Text) < CDec(RadTextBoxControlBereich1Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight2.Text), 1) Then
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

            If CDec(RadTextBoxControlBereich1DisplayWeight3.Text) > CDec(RadTextBoxControlBereich1Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight3.Text), 1) Then
                RadCheckBoxBereich1VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight3.Text) < CDec(RadTextBoxControlBereich1Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight3.Text), 1) Then
                RadCheckBoxBereich1VEL3.Checked = False
            Else
                RadCheckBoxBereich1VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit4.Text = CDec(RadTextBoxControlBereich1DisplayWeight4.Text) - CDec(RadTextBoxControlBereich1Weight4.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight4.Text) > CDec(RadTextBoxControlBereich1Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight4.Text), 1) Then
                RadCheckBoxBereich1VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight4.Text) < CDec(RadTextBoxControlBereich1Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight4.Text), 1) Then
                RadCheckBoxBereich1VEL4.Checked = False
            Else
                RadCheckBoxBereich1VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit5.Text = CDec(RadTextBoxControlBereich1DisplayWeight5.Text) - CDec(RadTextBoxControlBereich1Weight5.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight5.Text) > CDec(RadTextBoxControlBereich1Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight5.Text), 1) Then
                RadCheckBoxBereich1VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight5.Text) < CDec(RadTextBoxControlBereich1Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight5.Text), 1) Then
                RadCheckBoxBereich1VEL5.Checked = False
            Else
                RadCheckBoxBereich1VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit6.Text = CDec(RadTextBoxControlBereich1DisplayWeight6.Text) - CDec(RadTextBoxControlBereich1Weight6.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight6.Text) > CDec(RadTextBoxControlBereich1Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight6.Text), 1) Then
                RadCheckBoxBereich1VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight6.Text) < CDec(RadTextBoxControlBereich1Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight6.Text), 1) Then
                RadCheckBoxBereich1VEL6.Checked = False
            Else
                RadCheckBoxBereich1VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit7.Text = CDec(RadTextBoxControlBereich1DisplayWeight7.Text) - CDec(RadTextBoxControlBereich1Weight7.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight7.Text) > CDec(RadTextBoxControlBereich1Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight7.Text), 1) Then
                RadCheckBoxBereich1VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight7.Text) < CDec(RadTextBoxControlBereich1Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight7.Text), 1) Then
                RadCheckBoxBereich1VEL7.Checked = False
            Else
                RadCheckBoxBereich1VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich1ErrorLimit8.Text = CDec(RadTextBoxControlBereich1DisplayWeight8.Text) - CDec(RadTextBoxControlBereich1Weight8.Text)

            If CDec(RadTextBoxControlBereich1DisplayWeight8.Text) > CDec(RadTextBoxControlBereich1Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich1Weight8.Text), 1) Then
                RadCheckBoxBereich1VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1DisplayWeight8.Text) < CDec(RadTextBoxControlBereich1Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich1Weight8.Text), 1) Then
                RadCheckBoxBereich1VEL8.Checked = False
            Else
                RadCheckBoxBereich1VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Bereich 2"
    Private Sub RadTextBoxControlBereich2DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit1.Text = CDec(RadTextBoxControlBereich2DisplayWeight1.Text) - CDec(RadTextBoxControlBereich2Weight1.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight1.Text) > CDec(RadTextBoxControlBereich2Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight1.Text), 2) Then
                RadCheckBoxBereich2VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight1.Text) < CDec(RadTextBoxControlBereich2Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight1.Text), 2) Then
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

            If CDec(RadTextBoxControlBereich2DisplayWeight2.Text) > CDec(RadTextBoxControlBereich2Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight2.Text), 2) Then
                RadCheckBoxBereich2VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight2.Text) < CDec(RadTextBoxControlBereich2Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight2.Text), 2) Then
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

            If CDec(RadTextBoxControlBereich2DisplayWeight3.Text) > CDec(RadTextBoxControlBereich2Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight3.Text), 2) Then
                RadCheckBoxBereich2VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight3.Text) < CDec(RadTextBoxControlBereich2Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight3.Text), 2) Then
                RadCheckBoxBereich2VEL3.Checked = False
            Else
                RadCheckBoxBereich2VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit4.Text = CDec(RadTextBoxControlBereich2DisplayWeight4.Text) - CDec(RadTextBoxControlBereich2Weight4.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight4.Text) > CDec(RadTextBoxControlBereich2Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight4.Text), 2) Then
                RadCheckBoxBereich2VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight4.Text) < CDec(RadTextBoxControlBereich2Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight4.Text), 2) Then
                RadCheckBoxBereich2VEL4.Checked = False
            Else
                RadCheckBoxBereich2VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit5.Text = CDec(RadTextBoxControlBereich2DisplayWeight5.Text) - CDec(RadTextBoxControlBereich2Weight5.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight5.Text) > CDec(RadTextBoxControlBereich2Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight5.Text), 2) Then
                RadCheckBoxBereich2VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight5.Text) < CDec(RadTextBoxControlBereich2Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight5.Text), 2) Then
                RadCheckBoxBereich2VEL5.Checked = False
            Else
                RadCheckBoxBereich2VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit6.Text = CDec(RadTextBoxControlBereich2DisplayWeight6.Text) - CDec(RadTextBoxControlBereich2Weight6.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight6.Text) > CDec(RadTextBoxControlBereich2Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight6.Text), 2) Then
                RadCheckBoxBereich2VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight6.Text) < CDec(RadTextBoxControlBereich2Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight6.Text), 2) Then
                RadCheckBoxBereich2VEL6.Checked = False
            Else
                RadCheckBoxBereich2VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit7.Text = CDec(RadTextBoxControlBereich2DisplayWeight7.Text) - CDec(RadTextBoxControlBereich2Weight7.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight7.Text) > CDec(RadTextBoxControlBereich2Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight7.Text), 2) Then
                RadCheckBoxBereich2VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight7.Text) < CDec(RadTextBoxControlBereich2Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight7.Text), 2) Then
                RadCheckBoxBereich2VEL7.Checked = False
            Else
                RadCheckBoxBereich2VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich2ErrorLimit8.Text = CDec(RadTextBoxControlBereich2DisplayWeight8.Text) - CDec(RadTextBoxControlBereich2Weight8.Text)

            If CDec(RadTextBoxControlBereich2DisplayWeight8.Text) > CDec(RadTextBoxControlBereich2Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich2Weight8.Text), 2) Then
                RadCheckBoxBereich2VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2DisplayWeight8.Text) < CDec(RadTextBoxControlBereich2Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich2Weight8.Text), 2) Then
                RadCheckBoxBereich2VEL8.Checked = False
            Else
                RadCheckBoxBereich2VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub





#End Region

#Region "Bereich 3"
    Private Sub RadTextBoxControlBereich3DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit1.Text = CDec(RadTextBoxControlBereich3DisplayWeight1.Text) - CDec(RadTextBoxControlBereich3Weight1.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight1.Text) > CDec(RadTextBoxControlBereich3Weight1.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight1.Text), 3) Then
                RadCheckBoxBereich3VEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight1.Text) < CDec(RadTextBoxControlBereich3Weight1.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight1.Text), 3) Then
                RadCheckBoxBereich3VEL1.Checked = False
            Else
                RadCheckBoxBereich3VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit2.Text = CDec(RadTextBoxControlBereich3DisplayWeight2.Text) - CDec(RadTextBoxControlBereich3Weight2.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight2.Text) > CDec(RadTextBoxControlBereich3Weight2.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight2.Text), 3) Then
                RadCheckBoxBereich3VEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight2.Text) < CDec(RadTextBoxControlBereich3Weight2.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight2.Text), 3) Then
                RadCheckBoxBereich3VEL2.Checked = False
            Else
                RadCheckBoxBereich3VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit3.Text = CDec(RadTextBoxControlBereich3DisplayWeight3.Text) - CDec(RadTextBoxControlBereich3Weight3.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight3.Text) > CDec(RadTextBoxControlBereich3Weight3.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight3.Text), 3) Then
                RadCheckBoxBereich3VEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight3.Text) < CDec(RadTextBoxControlBereich3Weight3.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight3.Text), 3) Then
                RadCheckBoxBereich3VEL3.Checked = False
            Else
                RadCheckBoxBereich3VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit4.Text = CDec(RadTextBoxControlBereich3DisplayWeight4.Text) - CDec(RadTextBoxControlBereich3Weight4.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight4.Text) > CDec(RadTextBoxControlBereich3Weight4.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight4.Text), 3) Then
                RadCheckBoxBereich3VEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight4.Text) < CDec(RadTextBoxControlBereich3Weight4.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight4.Text), 3) Then
                RadCheckBoxBereich3VEL4.Checked = False
            Else
                RadCheckBoxBereich3VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit5.Text = CDec(RadTextBoxControlBereich3DisplayWeight5.Text) - CDec(RadTextBoxControlBereich3Weight5.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight5.Text) > CDec(RadTextBoxControlBereich3Weight5.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight5.Text), 3) Then
                RadCheckBoxBereich3VEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight5.Text) < CDec(RadTextBoxControlBereich3Weight5.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight5.Text), 3) Then
                RadCheckBoxBereich3VEL5.Checked = False
            Else
                RadCheckBoxBereich3VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit6.Text = CDec(RadTextBoxControlBereich3DisplayWeight6.Text) - CDec(RadTextBoxControlBereich3Weight6.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight6.Text) > CDec(RadTextBoxControlBereich3Weight6.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight6.Text), 3) Then
                RadCheckBoxBereich3VEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight6.Text) < CDec(RadTextBoxControlBereich3Weight6.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight6.Text), 3) Then
                RadCheckBoxBereich3VEL6.Checked = False
            Else
                RadCheckBoxBereich3VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit7.Text = CDec(RadTextBoxControlBereich3DisplayWeight7.Text) - CDec(RadTextBoxControlBereich3Weight7.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight7.Text) > CDec(RadTextBoxControlBereich3Weight7.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight7.Text), 3) Then
                RadCheckBoxBereich3VEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight7.Text) < CDec(RadTextBoxControlBereich3Weight7.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight7.Text), 3) Then
                RadCheckBoxBereich3VEL7.Checked = False
            Else
                RadCheckBoxBereich3VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich3ErrorLimit8.Text = CDec(RadTextBoxControlBereich3DisplayWeight8.Text) - CDec(RadTextBoxControlBereich3Weight8.Text)

            If CDec(RadTextBoxControlBereich3DisplayWeight8.Text) > CDec(RadTextBoxControlBereich3Weight8.Text) + GetEFG(CDec(RadTextBoxControlBereich3Weight8.Text), 3) Then
                RadCheckBoxBereich3VEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3DisplayWeight8.Text) < CDec(RadTextBoxControlBereich3Weight8.Text) - GetEFG(CDec(RadTextBoxControlBereich3Weight8.Text), 3) Then
                RadCheckBoxBereich3VEL8.Checked = False
            Else
                RadCheckBoxBereich3VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub




#End Region
#End Region

#Region "Fallend"
#Region "Bereich 1"
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich1FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich1FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight1.Text), 1) Then
                RadCheckBoxBereich1FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich1FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight1.Text), 1) Then
                RadCheckBoxBereich1FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich1FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich1FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight2.Text), 1) Then
                RadCheckBoxBereich1FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich1FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight2.Text), 1) Then
                RadCheckBoxBereich1FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich1FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich1FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight3.Text), 1) Then
                RadCheckBoxBereich1FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich1FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight3.Text), 1) Then
                RadCheckBoxBereich1FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich1FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich1FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight4.Text), 1) Then
                RadCheckBoxBereich1FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich1FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight4.Text), 1) Then
                RadCheckBoxBereich1FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich1FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich1FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight5.Text), 1) Then
                RadCheckBoxBereich1FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich1FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight5.Text), 1) Then
                RadCheckBoxBereich1FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich1FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich1FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight6.Text), 1) Then
                RadCheckBoxBereich1FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich1FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight6.Text), 1) Then
                RadCheckBoxBereich1FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich1FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich1FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight7.Text), 1) Then
                RadCheckBoxBereich1FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich1FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight7.Text), 1) Then
                RadCheckBoxBereich1FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich1FallendDisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1FallendDisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich1FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich1FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich1FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich1FallendWeight8.Text), 1) Then
                RadCheckBoxBereich1FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich1FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich1FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich1FallendWeight8.Text), 1) Then
                RadCheckBoxBereich1FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich1FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Bereich 2"
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich2FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich2FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight1.Text), 2) Then
                RadCheckBoxBereich2FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich2FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight1.Text), 2) Then
                RadCheckBoxBereich2FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich2FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich2FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight2.Text), 2) Then
                RadCheckBoxBereich2FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich2FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight2.Text), 2) Then
                RadCheckBoxBereich2FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich2FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich2FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight3.Text), 2) Then
                RadCheckBoxBereich2FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich2FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight3.Text), 2) Then
                RadCheckBoxBereich2FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich2FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich2FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight4.Text), 2) Then
                RadCheckBoxBereich2FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich2FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight4.Text), 2) Then
                RadCheckBoxBereich2FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich2FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich2FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight5.Text), 2) Then
                RadCheckBoxBereich2FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich2FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight5.Text), 2) Then
                RadCheckBoxBereich2FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich2FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich2FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight6.Text), 2) Then
                RadCheckBoxBereich2FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich2FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight6.Text), 2) Then
                RadCheckBoxBereich2FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich2FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich2FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight7.Text), 2) Then
                RadCheckBoxBereich2FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich2FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight7.Text), 2) Then
                RadCheckBoxBereich2FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich2FallendDisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2FallendDisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich2FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich2FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich2FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich2FallendWeight8.Text), 2) Then
                RadCheckBoxBereich2FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich2FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich2FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich2FallendWeight8.Text), 2) Then
                RadCheckBoxBereich2FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich2FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub





#End Region

#Region "Bereich 3"
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight1.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit1.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) - CDec(RadTextBoxControlBereich3FallendWeight1.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) > CDec(RadTextBoxControlBereich3FallendWeight1.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight1.Text), 3) Then
                RadCheckBoxBereich3FallendVEL1.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight1.Text) < CDec(RadTextBoxControlBereich3FallendWeight1.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight1.Text), 3) Then
                RadCheckBoxBereich3FallendVEL1.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight2.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit2.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) - CDec(RadTextBoxControlBereich3FallendWeight2.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) > CDec(RadTextBoxControlBereich3FallendWeight2.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight2.Text), 3) Then
                RadCheckBoxBereich3FallendVEL2.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight2.Text) < CDec(RadTextBoxControlBereich3FallendWeight2.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight2.Text), 3) Then
                RadCheckBoxBereich3FallendVEL2.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight3.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit3.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) - CDec(RadTextBoxControlBereich3FallendWeight3.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) > CDec(RadTextBoxControlBereich3FallendWeight3.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight3.Text), 3) Then
                RadCheckBoxBereich3FallendVEL3.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight3.Text) < CDec(RadTextBoxControlBereich3FallendWeight3.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight3.Text), 3) Then
                RadCheckBoxBereich3FallendVEL3.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight4.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit4.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) - CDec(RadTextBoxControlBereich3FallendWeight4.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) > CDec(RadTextBoxControlBereich3FallendWeight4.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight4.Text), 3) Then
                RadCheckBoxBereich3FallendVEL4.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight4.Text) < CDec(RadTextBoxControlBereich3FallendWeight4.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight4.Text), 3) Then
                RadCheckBoxBereich3FallendVEL4.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight5.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit5.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) - CDec(RadTextBoxControlBereich3FallendWeight5.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) > CDec(RadTextBoxControlBereich3FallendWeight5.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight5.Text), 3) Then
                RadCheckBoxBereich3FallendVEL5.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight5.Text) < CDec(RadTextBoxControlBereich3FallendWeight5.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight5.Text), 3) Then
                RadCheckBoxBereich3FallendVEL5.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight6.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit6.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) - CDec(RadTextBoxControlBereich3FallendWeight6.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) > CDec(RadTextBoxControlBereich3FallendWeight6.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight6.Text), 3) Then
                RadCheckBoxBereich3FallendVEL6.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight6.Text) < CDec(RadTextBoxControlBereich3FallendWeight6.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight6.Text), 3) Then
                RadCheckBoxBereich3FallendVEL6.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight7.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit7.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) - CDec(RadTextBoxControlBereich3FallendWeight7.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) > CDec(RadTextBoxControlBereich3FallendWeight7.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight7.Text), 3) Then
                RadCheckBoxBereich3FallendVEL7.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight7.Text) < CDec(RadTextBoxControlBereich3FallendWeight7.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight7.Text), 3) Then
                RadCheckBoxBereich3FallendVEL7.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RadTextBoxControlBereich3FallendDisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3FallendDisplayWeight8.TextChanged
        Try
            RadTextBoxControlBereich3FallendErrorLimit8.Text = CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) - CDec(RadTextBoxControlBereich3FallendWeight8.Text)

            If CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) > CDec(RadTextBoxControlBereich3FallendWeight8.Text) + GetEFG(CDec(RadTextBoxControlBereich3FallendWeight8.Text), 3) Then
                RadCheckBoxBereich3FallendVEL8.Checked = False
            ElseIf CDec(RadTextBoxControlBereich3FallendDisplayWeight8.Text) < CDec(RadTextBoxControlBereich3FallendWeight8.Text) - GetEFG(CDec(RadTextBoxControlBereich3FallendWeight8.Text), 3) Then
                RadCheckBoxBereich3FallendVEL8.Checked = False
            Else
                RadCheckBoxBereich3FallendVEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub




#End Region
#End Region

#End Region

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
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
                        Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
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
                                    For Each pruefung In query
                                        Context.PruefungLinearitaetSteigend.Remove(pruefung)
                                    Next

                                    Context.SaveChanges()
                                End If

                            End If

                            'linearität Fallend
                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungPruefungLinearitaetFallend.Count = 0 Then
                                'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren

                                For j = 1 To intBereiche
                                    For intMesspunkt As Integer = 1 To _intAnzahlUrpsMessPunkte
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
                                    For Each pruefung In query
                                        Context.PruefungLinearitaetFallend.Remove(pruefung)
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

    Protected Friend Overrides Sub SaveWithoutValidationNeeded(usercontrol As UserControl)
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
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
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
                                For Each pruefung In query
                                    Context.PruefungLinearitaetSteigend.Remove(pruefung)
                                Next

                                Context.SaveChanges()
                            End If

                        End If

                        'linearität Fallend
                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungPruefungLinearitaetFallend.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren

                            For j = 1 To intBereiche
                                For intMesspunkt As Integer = 1 To _intAnzahlUrpsMessPunkte
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
                                For Each pruefung In query
                                    Context.PruefungLinearitaetFallend.Remove(pruefung)
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


    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoPruefungLinearitaet))



        Me.RadGroupBoxBereich1.Text = resources.GetString("RadGroupBoxBereich1.Text")
        Me.RadGroupBoxBereich2.Text = resources.GetString("RadGroupBoxBereich2.Text")
        Me.RadGroupBoxBereich3.Text = resources.GetString("RadGroupBoxBereich3.Text")
        Me.RadGroupBoxBereich1Fallend.Text = resources.GetString("RadGroupBoxBereich1Fallend.Text")
        Me.RadGroupBoxBereich2Fallend.Text = resources.GetString("RadGroupBoxBereich2Fallend.Text")
        Me.RadGroupBoxBereich3Fallend.Text = resources.GetString("RadGroupBoxBereich3Fallend.Text")
        Me.RadGroupBoxPruefungLinearitaetFallend.Text = resources.GetString("RadGroupBoxPruefungLinearitaetFallend.Text")
        Me.RadGroupBoxPruefungLinearitaetSteigend.Text = resources.GetString("RadGroupBoxPruefungLinearitaetSteigend.Text")


        Me.lblBereich1AnzeigeGewicht.Text = resources.GetString("lblBereich1AnzeigeGewicht.Text")
        Me.lblBereich2AnzeigeGewicht.Text = resources.GetString("lblBereich2AnzeigeGewicht.Text")
        Me.lblBereich3AnzeigeGewicht.Text = resources.GetString("lblBereich3AnzeigeGewicht.Text")
        Me.lblBereich1FallendAnzeigeGewicht.Text = resources.GetString("lblBereich1FallendAnzeigeGewicht.Text")
        Me.lblBereich2FallendAnzeigeGewicht.Text = resources.GetString("lblBereich2FallendAnzeigeGewicht.Text")
        Me.lblBereich3FallendAnzeigeGewicht.Text = resources.GetString("lblBereich3FallendAnzeigeGewicht.Text")

        Me.lblBereich1Gewicht.Text = resources.GetString("lblBereich1Gewicht.Text")
        Me.lblBereich2Gewicht.Text = resources.GetString("lblBereich2Gewicht.Text")
        Me.lblBereich3Gewicht.Text = resources.GetString("lblBereich3Gewicht.Text")
        Me.lblBereich1FallendGewicht.Text = resources.GetString("lblBereich1FallendGewicht.Text")
        Me.lblBereich2FallendGewicht.Text = resources.GetString("lblBereich2FallendGewicht.Text")
        Me.lblBereich3FallendGewicht.Text = resources.GetString("lblBereich3FallendGewicht.Text")

        Me.lblBereich1FehlerGrenzen.Text = resources.GetString("lblBereich1FehlerGrenzen.Text")
        Me.lblBereich2FehlerGrenzen.Text = resources.GetString("lblBereich2FehlerGrenzen.Text")
        Me.lblBereich3FehlerGrenzen.Text = resources.GetString("lblBereich3FehlerGrenzen.Text")
        Me.lblBereich1FallendFehlerGrenzen.Text = resources.GetString("lblBereich1FallendFehlerGrenzen.Text")
        Me.lblBereich2FallendFehlerGrenzen.Text = resources.GetString("lblBereich2FallendFehlerGrenzen.Text")
        Me.lblBereich3FallendFehlerGrenzen.Text = resources.GetString("lblBereich3FallendFehlerGrenzen.Text")

        Me.lblBereich1EFGSpezial.Text = resources.GetString("lblBereich1EFGSpezial.Text")
        Me.lblBereich2EFGSpezial.Text = resources.GetString("lblBereich2EFGSpezial.Text")
        Me.lblBereich3EFGSpezial.Text = resources.GetString("lblBereich3EFGSpezial.Text")
        Me.lblBereich1FallendEFGSpezial.Text = resources.GetString("lblBereich1FallendEFGSpezial.Text")
        Me.lblBereich2FallendEFGSpezial.Text = resources.GetString("lblBereich2FallendEFGSpezial.Text")
        Me.lblBereich3FallendEFGSpezial.Text = resources.GetString("lblBereich3FallendEFGSpezial.Text")


      


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
    Protected Friend Overrides Sub UpdateNeeded(UserControl As UserControl)
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
        MyBase.VersendenNeeded(TargetUserControl)

        If Me.Equals(TargetUserControl) Then

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
