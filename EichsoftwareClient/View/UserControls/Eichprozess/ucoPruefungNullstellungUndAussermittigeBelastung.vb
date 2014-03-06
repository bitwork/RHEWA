Imports System.Runtime.Remoting.Contexts
Imports System
Imports System.Data.Entity

Friend Class ucoPruefungNullstellungUndAussermittigeBelastung
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. 
    'Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _ListPruefungAussermittigeBelastung As New List(Of PruefungAussermittigeBelastung)
    Private _currentObjPruefungAussermittigeBelastung As PruefungAussermittigeBelastung
    Private _currentObjPruefungWiederholbarkeit As PruefungWiederholbarkeit
    Private _ListPruefungWiederholbarkeit As New List(Of PruefungWiederholbarkeit)

    Private _currentObjVerfahren As Lookup_Konformitaetsbewertungsverfahren
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

        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung

    End Sub

#End Region

#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAussermittigerBelastung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungNullstellungundAussermittigeBelastung
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung

        'daten füllen
        LoadFromDatabase()
    End Sub

    Private Sub RadTextBoxControlWeight_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlWeight3.Validating, RadTextBoxControlWeight2.Validating, RadTextBoxControlWeight1.Validating,
        RadTextBoxControlDisplayWeight3.Validating, RadTextBoxControlDisplayWeight2.Validating, RadTextBoxControlDisplayWeight1.Validating, RadTextBoxControlBereich3WeightMitte.Validating, RadTextBoxControlBereich3Weight9.Validating,
        RadTextBoxControlBereich3Weight8.Validating, RadTextBoxControlBereich3Weight7.Validating, RadTextBoxControlBereich3Weight6.Validating, RadTextBoxControlBereich3Weight5.Validating, RadTextBoxControlBereich3Weight4.Validating,
        RadTextBoxControlBereich3Weight3.Validating, RadTextBoxControlBereich3Weight2.Validating, RadTextBoxControlBereich3Weight12.Validating, RadTextBoxControlBereich3Weight11.Validating, RadTextBoxControlBereich3Weight10.Validating,
        RadTextBoxControlBereich3Weight1.Validating, RadTextBoxControlBereich3DisplayWeightMitte.Validating, RadTextBoxControlBereich3DisplayWeight9.Validating, RadTextBoxControlBereich3DisplayWeight8.Validating,
        RadTextBoxControlBereich3DisplayWeight7.Validating, RadTextBoxControlBereich3DisplayWeight6.Validating, RadTextBoxControlBereich3DisplayWeight5.Validating, RadTextBoxControlBereich3DisplayWeight4.Validating,
RadTextBoxControlBereich3DisplayWeight3.Validating, RadTextBoxControlBereich3DisplayWeight2.Validating, RadTextBoxControlBereich3DisplayWeight12.Validating, RadTextBoxControlBereich3DisplayWeight11.Validating,
RadTextBoxControlBereich3DisplayWeight10.Validating, RadTextBoxControlBereich3DisplayWeight1.Validating, RadTextBoxControlBereich2WeightMitte.Validating, RadTextBoxControlBereich2Weight9.Validating, RadTextBoxControlBereich2Weight8.Validating,
RadTextBoxControlBereich2Weight7.Validating, RadTextBoxControlBereich2Weight6.Validating, RadTextBoxControlBereich2Weight5.Validating, RadTextBoxControlBereich2Weight4.Validating, RadTextBoxControlBereich2Weight3.Validating,
RadTextBoxControlBereich2Weight2.Validating, RadTextBoxControlBereich2Weight12.Validating, RadTextBoxControlBereich2Weight11.Validating, RadTextBoxControlBereich2Weight10.Validating, RadTextBoxControlBereich2Weight1.Validating,
RadTextBoxControlBereich2DisplayWeightMitte.Validating, RadTextBoxControlBereich2DisplayWeight9.Validating, RadTextBoxControlBereich2DisplayWeight8.Validating, RadTextBoxControlBereich2DisplayWeight7.Validating,
RadTextBoxControlBereich2DisplayWeight6.Validating, RadTextBoxControlBereich2DisplayWeight5.Validating, RadTextBoxControlBereich2DisplayWeight4.Validating, RadTextBoxControlBereich2DisplayWeight3.Validating,
RadTextBoxControlBereich2DisplayWeight2.Validating, RadTextBoxControlBereich2DisplayWeight12.Validating, RadTextBoxControlBereich2DisplayWeight11.Validating, RadTextBoxControlBereich2DisplayWeight10.Validating,
RadTextBoxControlBereich2DisplayWeight1.Validating, RadTextBoxControlBereich1WeightMitte.Validating, RadTextBoxControlBereich1Weight9.Validating, RadTextBoxControlBereich1Weight8.Validating, RadTextBoxControlBereich1Weight7.Validating,
RadTextBoxControlBereich1Weight6.Validating, RadTextBoxControlBereich1Weight5.Validating, RadTextBoxControlBereich1Weight4.Validating, RadTextBoxControlBereich1Weight3.Validating, RadTextBoxControlBereich1Weight2.Validating,
RadTextBoxControlBereich1Weight12.Validating, RadTextBoxControlBereich1Weight11.Validating, RadTextBoxControlBereich1Weight10.Validating, RadTextBoxControlBereich1Weight1.Validating, RadTextBoxControlBereich1DisplayWeightMitte.Validating,
RadTextBoxControlBereich1DisplayWeight9.Validating, RadTextBoxControlBereich1DisplayWeight8.Validating, RadTextBoxControlBereich1DisplayWeight7.Validating, RadTextBoxControlBereich1DisplayWeight6.Validating,
RadTextBoxControlBereich1DisplayWeight5.Validating, RadTextBoxControlBereich1DisplayWeight4.Validating, RadTextBoxControlBereich1DisplayWeight3.Validating, RadTextBoxControlBereich1DisplayWeight2.Validating,
RadTextBoxControlBereich1DisplayWeight12.Validating, RadTextBoxControlBereich1DisplayWeight11.Validating, RadTextBoxControlBereich1DisplayWeight10.Validating, RadTextBoxControlBereich1DisplayWeight1.Validating
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

    Private Sub RadCheckBoxBereich1VEL1_MouseClick(sender As Object, e As MouseEventArgs) Handles RadCheckBoxBereich3VELMitte.MouseClick, RadCheckBoxBereich3VEL9.MouseClick, RadCheckBoxBereich3VEL8.MouseClick, RadCheckBoxBereich3VEL7.MouseClick,
          RadCheckBoxBereich3VEL6.MouseClick, RadCheckBoxBereich3VEL5.MouseClick, RadCheckBoxBereich3VEL4.MouseClick, RadCheckBoxBereich3VEL3.MouseClick, RadCheckBoxBereich3VEL2.MouseClick, RadCheckBoxBereich3VEL12.MouseClick,
          RadCheckBoxBereich3VEL11.MouseClick, RadCheckBoxBereich3VEL10.MouseClick, RadCheckBoxBereich3VEL1.MouseClick, RadCheckBoxBereich2VELMitte.MouseClick, RadCheckBoxBereich2VEL9.MouseClick, RadCheckBoxBereich2VEL8.MouseClick,
          RadCheckBoxBereich2VEL7.MouseClick, RadCheckBoxBereich2VEL6.MouseClick, RadCheckBoxBereich2VEL5.MouseClick, RadCheckBoxBereich2VEL4.MouseClick, RadCheckBoxBereich2VEL3.MouseClick, RadCheckBoxBereich2VEL2.MouseClick,
          RadCheckBoxBereich2VEL12.MouseClick, RadCheckBoxBereich2VEL11.MouseClick, RadCheckBoxBereich2VEL10.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich1VELMitte.MouseClick, RadCheckBoxBereich1VEL9.MouseClick,
          RadCheckBoxBereich1VEL8.MouseClick, RadCheckBoxBereich1VEL7.MouseClick, RadCheckBoxBereich1VEL6.MouseClick, RadCheckBoxBereich1VEL5.MouseClick, RadCheckBoxBereich1VEL4.MouseClick, RadCheckBoxBereich1VEL3.MouseClick,
          RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL12.MouseClick, RadCheckBoxBereich1VEL11.MouseClick, RadCheckBoxBereich1VEL10.MouseClick, RadCheckBoxBereich1VEL1.MouseClick, RadCheckBoxVEL1.Click, RadCheckBoxVEL2.Click,
          RadCheckBoxVEL3.Click
        CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked = Not CType(sender, Telerik.WinControls.UI.RadCheckBox).Checked
    End Sub

#Region "Wiederholbarkeit Text Changed Events"
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControl_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlWeight3.TextChanged, RadTextBoxControlWeight2.TextChanged, RadTextBoxControlWeight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 1
        RadTextBoxControlWeight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlWeight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlWeight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text


        'neu berechnen der Fehler und EFG

        Try
            RadTextBoxControlErrorLimit1.Text = CDec(RadTextBoxControlDisplayWeight1.Text) - CDec(RadTextBoxControlWeight1.Text)
            If RadTextBoxControlDisplayWeight1.Text > CDec(RadTextBoxControlWeight1.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL1.Checked = False
            ElseIf RadTextBoxControlDisplayWeight1.Text < CDec(RadTextBoxControlWeight1.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL1.Checked = False
            Else
                RadCheckBoxVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlErrorLimit2.Text = CDec(RadTextBoxControlDisplayWeight2.Text) - CDec(RadTextBoxControlWeight2.Text)
            If RadTextBoxControlDisplayWeight2.Text > CDec(RadTextBoxControlWeight2.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL2.Checked = False
            ElseIf RadTextBoxControlDisplayWeight2.Text < CDec(RadTextBoxControlWeight2.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL2.Checked = False
            Else
                RadCheckBoxVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try


        Try
            RadTextBoxControlErrorLimit3.Text = CDec(RadTextBoxControlDisplayWeight3.Text) - CDec(RadTextBoxControlWeight3.Text)
            If RadTextBoxControlDisplayWeight3.Text > CDec(RadTextBoxControlWeight3.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL3.Checked = False
            ElseIf RadTextBoxControlDisplayWeight3.Text < CDec(RadTextBoxControlWeight3.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL3.Checked = False
            Else
                RadCheckBoxVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub

    Private Sub RadTextBoxControlDisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlDisplayWeight1.TextChanged
        Try
            RadTextBoxControlErrorLimit1.Text = CDec(RadTextBoxControlDisplayWeight1.Text) - CDec(RadTextBoxControlWeight1.Text)
            If RadTextBoxControlDisplayWeight1.Text > CDec(RadTextBoxControlWeight1.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL1.Checked = False
            ElseIf RadTextBoxControlDisplayWeight1.Text < CDec(RadTextBoxControlWeight1.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL1.Checked = False
            Else
                RadCheckBoxVEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlDisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlDisplayWeight2.TextChanged
        Try
            RadTextBoxControlErrorLimit2.Text = CDec(RadTextBoxControlDisplayWeight2.Text) - CDec(RadTextBoxControlWeight2.Text)
            If RadTextBoxControlDisplayWeight2.Text > CDec(RadTextBoxControlWeight2.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL2.Checked = False
            ElseIf RadTextBoxControlDisplayWeight2.Text < CDec(RadTextBoxControlWeight2.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL2.Checked = False
            Else
                RadCheckBoxVEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlDisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlDisplayWeight3.TextChanged
        Try
            RadTextBoxControlErrorLimit3.Text = CDec(RadTextBoxControlDisplayWeight3.Text) - CDec(RadTextBoxControlWeight3.Text)
            If RadTextBoxControlDisplayWeight3.Text > CDec(RadTextBoxControlWeight3.Text) + CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL3.Checked = False
            ElseIf RadTextBoxControlDisplayWeight3.Text < CDec(RadTextBoxControlWeight3.Text) - CDec(lblEFGSpeziallBerechnung.Text) Then
                RadCheckBoxVEL3.Checked = False
            Else
                RadCheckBoxVEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Aussermittige Belastung Text changed Events"
#Region "bereich1"
    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich1_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich1WeightMitte.TextChanged, RadTextBoxControlBereich1Weight9.TextChanged, RadTextBoxControlBereich1Weight8.TextChanged, RadTextBoxControlBereich1Weight7.TextChanged, RadTextBoxControlBereich1Weight6.TextChanged, RadTextBoxControlBereich1Weight5.TextChanged, RadTextBoxControlBereich1Weight4.TextChanged, RadTextBoxControlBereich1Weight3.TextChanged, RadTextBoxControlBereich1Weight2.TextChanged, RadTextBoxControlBereich1Weight12.TextChanged, RadTextBoxControlBereich1Weight11.TextChanged, RadTextBoxControlBereich1Weight10.TextChanged, RadTextBoxControlBereich1Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 1
        RadTextBoxControlBereich1Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight4.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight5.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight6.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight7.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight8.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight9.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight10.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight11.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1Weight12.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich1WeightMitte.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text


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

        Try
            RadTextBoxControlBereich1ErrorLimit4.Text = CDec(RadTextBoxControlBereich1DisplayWeight4.Text) - CDec(RadTextBoxControlBereich1Weight4.Text)
            If RadTextBoxControlBereich1DisplayWeight4.Text > CDec(RadTextBoxControlBereich1Weight4.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL4.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight4.Text < CDec(RadTextBoxControlBereich1Weight4.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL4.Checked = False
            Else
                RadCheckBoxBereich1VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit5.Text = CDec(RadTextBoxControlBereich1DisplayWeight5.Text) - CDec(RadTextBoxControlBereich1Weight5.Text)
            If RadTextBoxControlBereich1DisplayWeight5.Text > CDec(RadTextBoxControlBereich1Weight5.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL5.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight5.Text < CDec(RadTextBoxControlBereich1Weight5.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL5.Checked = False
            Else
                RadCheckBoxBereich1VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit6.Text = CDec(RadTextBoxControlBereich1DisplayWeight6.Text) - CDec(RadTextBoxControlBereich1Weight6.Text)
            If RadTextBoxControlBereich1DisplayWeight6.Text > CDec(RadTextBoxControlBereich1Weight6.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL6.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight6.Text < CDec(RadTextBoxControlBereich1Weight6.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL6.Checked = False
            Else
                RadCheckBoxBereich1VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit7.Text = CDec(RadTextBoxControlBereich1DisplayWeight7.Text) - CDec(RadTextBoxControlBereich1Weight7.Text)
            If RadTextBoxControlBereich1DisplayWeight7.Text > CDec(RadTextBoxControlBereich1Weight7.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL7.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight7.Text < CDec(RadTextBoxControlBereich1Weight7.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL7.Checked = False
            Else
                RadCheckBoxBereich1VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit8.Text = CDec(RadTextBoxControlBereich1DisplayWeight8.Text) - CDec(RadTextBoxControlBereich1Weight8.Text)
            If RadTextBoxControlBereich1DisplayWeight8.Text > CDec(RadTextBoxControlBereich1Weight8.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL8.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight8.Text < CDec(RadTextBoxControlBereich1Weight8.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL8.Checked = False
            Else
                RadCheckBoxBereich1VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit9.Text = CDec(RadTextBoxControlBereich1DisplayWeight9.Text) - CDec(RadTextBoxControlBereich1Weight9.Text)
            If RadTextBoxControlBereich1DisplayWeight9.Text > CDec(RadTextBoxControlBereich1Weight9.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL9.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight9.Text < CDec(RadTextBoxControlBereich1Weight9.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL9.Checked = False
            Else
                RadCheckBoxBereich1VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit10.Text = CDec(RadTextBoxControlBereich1DisplayWeight10.Text) - CDec(RadTextBoxControlBereich1Weight10.Text)
            If RadTextBoxControlBereich1DisplayWeight10.Text > CDec(RadTextBoxControlBereich1Weight10.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL10.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight10.Text < CDec(RadTextBoxControlBereich1Weight10.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL10.Checked = False
            Else
                RadCheckBoxBereich1VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit11.Text = CDec(RadTextBoxControlBereich1DisplayWeight11.Text) - CDec(RadTextBoxControlBereich1Weight11.Text)
            If RadTextBoxControlBereich1DisplayWeight11.Text > CDec(RadTextBoxControlBereich1Weight11.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL11.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight11.Text < CDec(RadTextBoxControlBereich1Weight11.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL11.Checked = False
            Else
                RadCheckBoxBereich1VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimit12.Text = CDec(RadTextBoxControlBereich1DisplayWeight12.Text) - CDec(RadTextBoxControlBereich1Weight12.Text)
            If RadTextBoxControlBereich1DisplayWeight12.Text > CDec(RadTextBoxControlBereich1Weight12.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL12.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight12.Text < CDec(RadTextBoxControlBereich1Weight12.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL12.Checked = False
            Else
                RadCheckBoxBereich1VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich1ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich1DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich1WeightMitte.Text)
            If RadTextBoxControlBereich1DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich1WeightMitte.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich1WeightMitte.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VELMitte.Checked = False
            Else
                RadCheckBoxBereich1VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try


        _suspendEvents = False
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight1.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
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
            If _suspendEvents = True Then Exit Sub
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
            If _suspendEvents = True Then Exit Sub
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

    Private Sub RadTextBoxControlBereich1DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight4.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit4.Text = CDec(RadTextBoxControlBereich1DisplayWeight4.Text) - CDec(RadTextBoxControlBereich1Weight4.Text)
            If RadTextBoxControlBereich1DisplayWeight4.Text > CDec(RadTextBoxControlBereich1Weight4.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL4.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight4.Text < CDec(RadTextBoxControlBereich1Weight4.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL4.Checked = False
            Else
                RadCheckBoxBereich1VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight5.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit5.Text = CDec(RadTextBoxControlBereich1DisplayWeight5.Text) - CDec(RadTextBoxControlBereich1Weight5.Text)
            If RadTextBoxControlBereich1DisplayWeight5.Text > CDec(RadTextBoxControlBereich1Weight5.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL5.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight5.Text < CDec(RadTextBoxControlBereich1Weight5.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL5.Checked = False
            Else
                RadCheckBoxBereich1VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight6.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit6.Text = CDec(RadTextBoxControlBereich1DisplayWeight6.Text) - CDec(RadTextBoxControlBereich1Weight6.Text)
            If RadTextBoxControlBereich1DisplayWeight6.Text > CDec(RadTextBoxControlBereich1Weight6.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL6.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight6.Text < CDec(RadTextBoxControlBereich1Weight6.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL6.Checked = False
            Else
                RadCheckBoxBereich1VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight7.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit7.Text = CDec(RadTextBoxControlBereich1DisplayWeight7.Text) - CDec(RadTextBoxControlBereich1Weight7.Text)
            If RadTextBoxControlBereich1DisplayWeight7.Text > CDec(RadTextBoxControlBereich1Weight7.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL7.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight7.Text < CDec(RadTextBoxControlBereich1Weight7.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL7.Checked = False
            Else
                RadCheckBoxBereich1VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight8.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit8.Text = CDec(RadTextBoxControlBereich1DisplayWeight8.Text) - CDec(RadTextBoxControlBereich1Weight8.Text)
            If RadTextBoxControlBereich1DisplayWeight8.Text > CDec(RadTextBoxControlBereich1Weight8.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL8.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight8.Text < CDec(RadTextBoxControlBereich1Weight8.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL8.Checked = False
            Else
                RadCheckBoxBereich1VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight9_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight9.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit9.Text = CDec(RadTextBoxControlBereich1DisplayWeight9.Text) - CDec(RadTextBoxControlBereich1Weight9.Text)
            If RadTextBoxControlBereich1DisplayWeight9.Text > CDec(RadTextBoxControlBereich1Weight9.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL9.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight9.Text < CDec(RadTextBoxControlBereich1Weight9.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL9.Checked = False
            Else
                RadCheckBoxBereich1VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight10_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight10.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit10.Text = CDec(RadTextBoxControlBereich1DisplayWeight10.Text) - CDec(RadTextBoxControlBereich1Weight10.Text)
            If RadTextBoxControlBereich1DisplayWeight10.Text > CDec(RadTextBoxControlBereich1Weight10.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL10.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight10.Text < CDec(RadTextBoxControlBereich1Weight10.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL10.Checked = False
            Else
                RadCheckBoxBereich1VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight11_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight11.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit11.Text = CDec(RadTextBoxControlBereich1DisplayWeight11.Text) - CDec(RadTextBoxControlBereich1Weight11.Text)
            If RadTextBoxControlBereich1DisplayWeight11.Text > CDec(RadTextBoxControlBereich1Weight11.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL11.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight11.Text < CDec(RadTextBoxControlBereich1Weight11.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL11.Checked = False
            Else
                RadCheckBoxBereich1VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeight12_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeight12.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimit12.Text = CDec(RadTextBoxControlBereich1DisplayWeight12.Text) - CDec(RadTextBoxControlBereich1Weight12.Text)
            If RadTextBoxControlBereich1DisplayWeight12.Text > CDec(RadTextBoxControlBereich1Weight12.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL12.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeight12.Text < CDec(RadTextBoxControlBereich1Weight12.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VEL12.Checked = False
            Else
                RadCheckBoxBereich1VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich1DisplayWeightMitte_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich1DisplayWeightMitte.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich1ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich1DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich1WeightMitte.Text)
            If RadTextBoxControlBereich1DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich1WeightMitte.Text) + CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich1DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich1WeightMitte.Text) - CDec(lblBereich1EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich1VELMitte.Checked = False
            Else
                RadCheckBoxBereich1VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region
#Region "bereich2"

    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich2_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich2WeightMitte.TextChanged, RadTextBoxControlBereich2Weight9.TextChanged, RadTextBoxControlBereich2Weight8.TextChanged, RadTextBoxControlBereich2Weight7.TextChanged, RadTextBoxControlBereich2Weight6.TextChanged, RadTextBoxControlBereich2Weight5.TextChanged, RadTextBoxControlBereich2Weight4.TextChanged, RadTextBoxControlBereich2Weight3.TextChanged, RadTextBoxControlBereich2Weight2.TextChanged, RadTextBoxControlBereich2Weight12.TextChanged, RadTextBoxControlBereich2Weight11.TextChanged, RadTextBoxControlBereich2Weight10.TextChanged, RadTextBoxControlBereich2Weight1.TextChanged

        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 2
        RadTextBoxControlBereich2Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight4.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight5.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight6.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight7.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight8.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight9.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight10.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight11.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2Weight12.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich2WeightMitte.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text


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

        Try
            RadTextBoxControlBereich2ErrorLimit4.Text = CDec(RadTextBoxControlBereich2DisplayWeight4.Text) - CDec(RadTextBoxControlBereich2Weight4.Text)
            If RadTextBoxControlBereich2DisplayWeight4.Text > CDec(RadTextBoxControlBereich2Weight4.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL4.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight4.Text < CDec(RadTextBoxControlBereich2Weight4.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL4.Checked = False
            Else
                RadCheckBoxBereich2VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit5.Text = CDec(RadTextBoxControlBereich2DisplayWeight5.Text) - CDec(RadTextBoxControlBereich2Weight5.Text)
            If RadTextBoxControlBereich2DisplayWeight5.Text > CDec(RadTextBoxControlBereich2Weight5.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL5.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight5.Text < CDec(RadTextBoxControlBereich2Weight5.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL5.Checked = False
            Else
                RadCheckBoxBereich2VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit6.Text = CDec(RadTextBoxControlBereich2DisplayWeight6.Text) - CDec(RadTextBoxControlBereich2Weight6.Text)
            If RadTextBoxControlBereich2DisplayWeight6.Text > CDec(RadTextBoxControlBereich2Weight6.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL6.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight6.Text < CDec(RadTextBoxControlBereich2Weight6.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL6.Checked = False
            Else
                RadCheckBoxBereich2VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit7.Text = CDec(RadTextBoxControlBereich2DisplayWeight7.Text) - CDec(RadTextBoxControlBereich2Weight7.Text)
            If RadTextBoxControlBereich2DisplayWeight7.Text > CDec(RadTextBoxControlBereich2Weight7.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL7.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight7.Text < CDec(RadTextBoxControlBereich2Weight7.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL7.Checked = False
            Else
                RadCheckBoxBereich2VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit8.Text = CDec(RadTextBoxControlBereich2DisplayWeight8.Text) - CDec(RadTextBoxControlBereich2Weight8.Text)
            If RadTextBoxControlBereich2DisplayWeight8.Text > CDec(RadTextBoxControlBereich2Weight8.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL8.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight8.Text < CDec(RadTextBoxControlBereich2Weight8.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL8.Checked = False
            Else
                RadCheckBoxBereich2VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit9.Text = CDec(RadTextBoxControlBereich2DisplayWeight9.Text) - CDec(RadTextBoxControlBereich2Weight9.Text)
            If RadTextBoxControlBereich2DisplayWeight9.Text > CDec(RadTextBoxControlBereich2Weight9.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL9.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight9.Text < CDec(RadTextBoxControlBereich2Weight9.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL9.Checked = False
            Else
                RadCheckBoxBereich2VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit10.Text = CDec(RadTextBoxControlBereich2DisplayWeight10.Text) - CDec(RadTextBoxControlBereich2Weight10.Text)
            If RadTextBoxControlBereich2DisplayWeight10.Text > CDec(RadTextBoxControlBereich2Weight10.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL10.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight10.Text < CDec(RadTextBoxControlBereich2Weight10.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL10.Checked = False
            Else
                RadCheckBoxBereich2VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit11.Text = CDec(RadTextBoxControlBereich2DisplayWeight11.Text) - CDec(RadTextBoxControlBereich2Weight11.Text)
            If RadTextBoxControlBereich2DisplayWeight11.Text > CDec(RadTextBoxControlBereich2Weight11.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL11.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight11.Text < CDec(RadTextBoxControlBereich2Weight11.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL11.Checked = False
            Else
                RadCheckBoxBereich2VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimit12.Text = CDec(RadTextBoxControlBereich2DisplayWeight12.Text) - CDec(RadTextBoxControlBereich2Weight12.Text)
            If RadTextBoxControlBereich2DisplayWeight12.Text > CDec(RadTextBoxControlBereich2Weight12.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL12.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight12.Text < CDec(RadTextBoxControlBereich2Weight12.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL12.Checked = False
            Else
                RadCheckBoxBereich2VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich2ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich2DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich2WeightMitte.Text)
            If RadTextBoxControlBereich2DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich2WeightMitte.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich2WeightMitte.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VELMitte.Checked = False
            Else
                RadCheckBoxBereich2VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try

        _suspendEvents = False

    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight1.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
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
            If _suspendEvents = True Then Exit Sub
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
            If _suspendEvents = True Then Exit Sub
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

    Private Sub RadTextBoxControlBereich2DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight4.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit4.Text = CDec(RadTextBoxControlBereich2DisplayWeight4.Text) - CDec(RadTextBoxControlBereich2Weight4.Text)
            If RadTextBoxControlBereich2DisplayWeight4.Text > CDec(RadTextBoxControlBereich2Weight4.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL4.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight4.Text < CDec(RadTextBoxControlBereich2Weight4.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL4.Checked = False
            Else
                RadCheckBoxBereich2VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight5.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit5.Text = CDec(RadTextBoxControlBereich2DisplayWeight5.Text) - CDec(RadTextBoxControlBereich2Weight5.Text)
            If RadTextBoxControlBereich2DisplayWeight5.Text > CDec(RadTextBoxControlBereich2Weight5.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL5.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight5.Text < CDec(RadTextBoxControlBereich2Weight5.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL5.Checked = False
            Else
                RadCheckBoxBereich2VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight6.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit6.Text = CDec(RadTextBoxControlBereich2DisplayWeight6.Text) - CDec(RadTextBoxControlBereich2Weight6.Text)
            If RadTextBoxControlBereich2DisplayWeight6.Text > CDec(RadTextBoxControlBereich2Weight6.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL6.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight6.Text < CDec(RadTextBoxControlBereich2Weight6.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL6.Checked = False
            Else
                RadCheckBoxBereich2VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight7.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit7.Text = CDec(RadTextBoxControlBereich2DisplayWeight7.Text) - CDec(RadTextBoxControlBereich2Weight7.Text)
            If RadTextBoxControlBereich2DisplayWeight7.Text > CDec(RadTextBoxControlBereich2Weight7.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL7.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight7.Text < CDec(RadTextBoxControlBereich2Weight7.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL7.Checked = False
            Else
                RadCheckBoxBereich2VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight8.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit8.Text = CDec(RadTextBoxControlBereich2DisplayWeight8.Text) - CDec(RadTextBoxControlBereich2Weight8.Text)
            If RadTextBoxControlBereich2DisplayWeight8.Text > CDec(RadTextBoxControlBereich2Weight8.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL8.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight8.Text < CDec(RadTextBoxControlBereich2Weight8.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL8.Checked = False
            Else
                RadCheckBoxBereich2VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight9_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight9.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit9.Text = CDec(RadTextBoxControlBereich2DisplayWeight9.Text) - CDec(RadTextBoxControlBereich2Weight9.Text)
            If RadTextBoxControlBereich2DisplayWeight9.Text > CDec(RadTextBoxControlBereich2Weight9.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL9.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight9.Text < CDec(RadTextBoxControlBereich2Weight9.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL9.Checked = False
            Else
                RadCheckBoxBereich2VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight10_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight10.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit10.Text = CDec(RadTextBoxControlBereich2DisplayWeight10.Text) - CDec(RadTextBoxControlBereich2Weight10.Text)
            If RadTextBoxControlBereich2DisplayWeight10.Text > CDec(RadTextBoxControlBereich2Weight10.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL10.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight10.Text < CDec(RadTextBoxControlBereich2Weight10.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL10.Checked = False
            Else
                RadCheckBoxBereich2VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight11_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight11.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit11.Text = CDec(RadTextBoxControlBereich2DisplayWeight11.Text) - CDec(RadTextBoxControlBereich2Weight11.Text)
            If RadTextBoxControlBereich2DisplayWeight11.Text > CDec(RadTextBoxControlBereich2Weight11.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL11.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight11.Text < CDec(RadTextBoxControlBereich2Weight11.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL11.Checked = False
            Else
                RadCheckBoxBereich2VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeight12_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeight12.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimit12.Text = CDec(RadTextBoxControlBereich2DisplayWeight12.Text) - CDec(RadTextBoxControlBereich2Weight12.Text)
            If RadTextBoxControlBereich2DisplayWeight12.Text > CDec(RadTextBoxControlBereich2Weight12.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL12.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeight12.Text < CDec(RadTextBoxControlBereich2Weight12.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VEL12.Checked = False
            Else
                RadCheckBoxBereich2VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich2DisplayWeightMitte_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich2DisplayWeightMitte.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich2ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich2DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich2WeightMitte.Text)
            If RadTextBoxControlBereich2DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich2WeightMitte.Text) + CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich2DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich2WeightMitte.Text) - CDec(lblBereich2EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich2VELMitte.Checked = False
            Else
                RadCheckBoxBereich2VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region
#Region "bereich3"


    ''' <summary>
    ''' wenn sich eine der Last Werte ändert, muss es in allen anderen Textboxen nachgezogen werden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBereich3_TextChanged(sender As Object, e As EventArgs) Handles _
    RadTextBoxControlBereich3WeightMitte.TextChanged, RadTextBoxControlBereich3Weight9.TextChanged, RadTextBoxControlBereich3Weight8.TextChanged, RadTextBoxControlBereich3Weight7.TextChanged, RadTextBoxControlBereich3Weight6.TextChanged, RadTextBoxControlBereich3Weight5.TextChanged, RadTextBoxControlBereich3Weight4.TextChanged, RadTextBoxControlBereich3Weight3.TextChanged, RadTextBoxControlBereich3Weight2.TextChanged, RadTextBoxControlBereich3Weight12.TextChanged, RadTextBoxControlBereich3Weight11.TextChanged, RadTextBoxControlBereich3Weight10.TextChanged, RadTextBoxControlBereich3Weight1.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        'damit keine Event Kettenreaktion durchgeführt wird, werden die Events ab hier unterbrochen
        _suspendEvents = True
        'bereich 3
        RadTextBoxControlBereich3Weight1.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight2.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight3.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight4.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight5.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight6.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight7.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight8.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight9.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight10.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight11.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3Weight12.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text
        RadTextBoxControlBereich3WeightMitte.Text = CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text



        Try
            RadTextBoxControlBereich3ErrorLimit1.Text = CDec(RadTextBoxControlBereich3DisplayWeight1.Text) - CDec(RadTextBoxControlBereich3Weight1.Text)
            If RadTextBoxControlBereich3DisplayWeight1.Text > CDec(RadTextBoxControlBereich3Weight1.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL1.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight1.Text < CDec(RadTextBoxControlBereich3Weight1.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL1.Checked = False
            Else
                RadCheckBoxBereich3VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit2.Text = CDec(RadTextBoxControlBereich3DisplayWeight2.Text) - CDec(RadTextBoxControlBereich3Weight2.Text)
            If RadTextBoxControlBereich3DisplayWeight2.Text > CDec(RadTextBoxControlBereich3Weight2.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL2.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight2.Text < CDec(RadTextBoxControlBereich3Weight2.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL2.Checked = False
            Else
                RadCheckBoxBereich3VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit3.Text = CDec(RadTextBoxControlBereich3DisplayWeight3.Text) - CDec(RadTextBoxControlBereich3Weight3.Text)
            If RadTextBoxControlBereich3DisplayWeight3.Text > CDec(RadTextBoxControlBereich3Weight3.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL3.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight3.Text < CDec(RadTextBoxControlBereich3Weight3.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL3.Checked = False
            Else
                RadCheckBoxBereich3VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit4.Text = CDec(RadTextBoxControlBereich3DisplayWeight4.Text) - CDec(RadTextBoxControlBereich3Weight4.Text)
            If RadTextBoxControlBereich3DisplayWeight4.Text > CDec(RadTextBoxControlBereich3Weight4.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL4.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight4.Text < CDec(RadTextBoxControlBereich3Weight4.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL4.Checked = False
            Else
                RadCheckBoxBereich3VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit5.Text = CDec(RadTextBoxControlBereich3DisplayWeight5.Text) - CDec(RadTextBoxControlBereich3Weight5.Text)
            If RadTextBoxControlBereich3DisplayWeight5.Text > CDec(RadTextBoxControlBereich3Weight5.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL5.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight5.Text < CDec(RadTextBoxControlBereich3Weight5.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL5.Checked = False
            Else
                RadCheckBoxBereich3VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit6.Text = CDec(RadTextBoxControlBereich3DisplayWeight6.Text) - CDec(RadTextBoxControlBereich3Weight6.Text)
            If RadTextBoxControlBereich3DisplayWeight6.Text > CDec(RadTextBoxControlBereich3Weight6.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL6.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight6.Text < CDec(RadTextBoxControlBereich3Weight6.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL6.Checked = False
            Else
                RadCheckBoxBereich3VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit7.Text = CDec(RadTextBoxControlBereich3DisplayWeight7.Text) - CDec(RadTextBoxControlBereich3Weight7.Text)
            If RadTextBoxControlBereich3DisplayWeight7.Text > CDec(RadTextBoxControlBereich3Weight7.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL7.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight7.Text < CDec(RadTextBoxControlBereich3Weight7.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL7.Checked = False
            Else
                RadCheckBoxBereich3VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit8.Text = CDec(RadTextBoxControlBereich3DisplayWeight8.Text) - CDec(RadTextBoxControlBereich3Weight8.Text)
            If RadTextBoxControlBereich3DisplayWeight8.Text > CDec(RadTextBoxControlBereich3Weight8.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL8.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight8.Text < CDec(RadTextBoxControlBereich3Weight8.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL8.Checked = False
            Else
                RadCheckBoxBereich3VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit9.Text = CDec(RadTextBoxControlBereich3DisplayWeight9.Text) - CDec(RadTextBoxControlBereich3Weight9.Text)
            If RadTextBoxControlBereich3DisplayWeight9.Text > CDec(RadTextBoxControlBereich3Weight9.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL9.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight9.Text < CDec(RadTextBoxControlBereich3Weight9.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL9.Checked = False
            Else
                RadCheckBoxBereich3VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit10.Text = CDec(RadTextBoxControlBereich3DisplayWeight10.Text) - CDec(RadTextBoxControlBereich3Weight10.Text)
            If RadTextBoxControlBereich3DisplayWeight10.Text > CDec(RadTextBoxControlBereich3Weight10.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL10.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight10.Text < CDec(RadTextBoxControlBereich3Weight10.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL10.Checked = False
            Else
                RadCheckBoxBereich3VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit11.Text = CDec(RadTextBoxControlBereich3DisplayWeight11.Text) - CDec(RadTextBoxControlBereich3Weight11.Text)
            If RadTextBoxControlBereich3DisplayWeight11.Text > CDec(RadTextBoxControlBereich3Weight11.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL11.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight11.Text < CDec(RadTextBoxControlBereich3Weight11.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL11.Checked = False
            Else
                RadCheckBoxBereich3VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimit12.Text = CDec(RadTextBoxControlBereich3DisplayWeight12.Text) - CDec(RadTextBoxControlBereich3Weight12.Text)
            If RadTextBoxControlBereich3DisplayWeight12.Text > CDec(RadTextBoxControlBereich3Weight12.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL12.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight12.Text < CDec(RadTextBoxControlBereich3Weight12.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL12.Checked = False
            Else
                RadCheckBoxBereich3VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControlBereich3ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich3DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich3WeightMitte.Text)
            If RadTextBoxControlBereich3DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich3WeightMitte.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich3WeightMitte.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VELMitte.Checked = False
            Else
                RadCheckBoxBereich3VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try

        _suspendEvents = False
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight1.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit1.Text = CDec(RadTextBoxControlBereich3DisplayWeight1.Text) - CDec(RadTextBoxControlBereich3Weight1.Text)
            If RadTextBoxControlBereich3DisplayWeight1.Text > CDec(RadTextBoxControlBereich3Weight1.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL1.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight1.Text < CDec(RadTextBoxControlBereich3Weight1.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL1.Checked = False
            Else
                RadCheckBoxBereich3VEL1.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight2.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit2.Text = CDec(RadTextBoxControlBereich3DisplayWeight2.Text) - CDec(RadTextBoxControlBereich3Weight2.Text)
            If RadTextBoxControlBereich3DisplayWeight2.Text > CDec(RadTextBoxControlBereich3Weight2.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL2.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight2.Text < CDec(RadTextBoxControlBereich3Weight2.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL2.Checked = False
            Else
                RadCheckBoxBereich3VEL2.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight3.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit3.Text = CDec(RadTextBoxControlBereich3DisplayWeight3.Text) - CDec(RadTextBoxControlBereich3Weight3.Text)
            If RadTextBoxControlBereich3DisplayWeight3.Text > CDec(RadTextBoxControlBereich3Weight3.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL3.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight3.Text < CDec(RadTextBoxControlBereich3Weight3.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL3.Checked = False
            Else
                RadCheckBoxBereich3VEL3.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight4_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight4.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit4.Text = CDec(RadTextBoxControlBereich3DisplayWeight4.Text) - CDec(RadTextBoxControlBereich3Weight4.Text)
            If RadTextBoxControlBereich3DisplayWeight4.Text > CDec(RadTextBoxControlBereich3Weight4.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL4.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight4.Text < CDec(RadTextBoxControlBereich3Weight4.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL4.Checked = False
            Else
                RadCheckBoxBereich3VEL4.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight5.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit5.Text = CDec(RadTextBoxControlBereich3DisplayWeight5.Text) - CDec(RadTextBoxControlBereich3Weight5.Text)
            If RadTextBoxControlBereich3DisplayWeight5.Text > CDec(RadTextBoxControlBereich3Weight5.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL5.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight5.Text < CDec(RadTextBoxControlBereich3Weight5.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL5.Checked = False
            Else
                RadCheckBoxBereich3VEL5.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight6_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight6.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit6.Text = CDec(RadTextBoxControlBereich3DisplayWeight6.Text) - CDec(RadTextBoxControlBereich3Weight6.Text)
            If RadTextBoxControlBereich3DisplayWeight6.Text > CDec(RadTextBoxControlBereich3Weight6.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL6.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight6.Text < CDec(RadTextBoxControlBereich3Weight6.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL6.Checked = False
            Else
                RadCheckBoxBereich3VEL6.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight7_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight7.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit7.Text = CDec(RadTextBoxControlBereich3DisplayWeight7.Text) - CDec(RadTextBoxControlBereich3Weight7.Text)
            If RadTextBoxControlBereich3DisplayWeight7.Text > CDec(RadTextBoxControlBereich3Weight7.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL7.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight7.Text < CDec(RadTextBoxControlBereich3Weight7.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL7.Checked = False
            Else
                RadCheckBoxBereich3VEL7.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight8_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight8.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit8.Text = CDec(RadTextBoxControlBereich3DisplayWeight8.Text) - CDec(RadTextBoxControlBereich3Weight8.Text)
            If RadTextBoxControlBereich3DisplayWeight8.Text > CDec(RadTextBoxControlBereich3Weight8.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL8.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight8.Text < CDec(RadTextBoxControlBereich3Weight8.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL8.Checked = False
            Else
                RadCheckBoxBereich3VEL8.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight9_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight9.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit9.Text = CDec(RadTextBoxControlBereich3DisplayWeight9.Text) - CDec(RadTextBoxControlBereich3Weight9.Text)
            If RadTextBoxControlBereich3DisplayWeight9.Text > CDec(RadTextBoxControlBereich3Weight9.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL9.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight9.Text < CDec(RadTextBoxControlBereich3Weight9.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL9.Checked = False
            Else
                RadCheckBoxBereich3VEL9.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight10_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight10.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit10.Text = CDec(RadTextBoxControlBereich3DisplayWeight10.Text) - CDec(RadTextBoxControlBereich3Weight10.Text)
            If RadTextBoxControlBereich3DisplayWeight10.Text > CDec(RadTextBoxControlBereich3Weight10.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL10.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight10.Text < CDec(RadTextBoxControlBereich3Weight10.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL10.Checked = False
            Else
                RadCheckBoxBereich3VEL10.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight11_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight11.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit11.Text = CDec(RadTextBoxControlBereich3DisplayWeight11.Text) - CDec(RadTextBoxControlBereich3Weight11.Text)
            If RadTextBoxControlBereich3DisplayWeight11.Text > CDec(RadTextBoxControlBereich3Weight11.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL11.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight11.Text < CDec(RadTextBoxControlBereich3Weight11.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL11.Checked = False
            Else
                RadCheckBoxBereich3VEL11.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeight12_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeight12.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimit12.Text = CDec(RadTextBoxControlBereich3DisplayWeight12.Text) - CDec(RadTextBoxControlBereich3Weight12.Text)
            If RadTextBoxControlBereich3DisplayWeight12.Text > CDec(RadTextBoxControlBereich3Weight12.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL12.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeight12.Text < CDec(RadTextBoxControlBereich3Weight12.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VEL12.Checked = False
            Else
                RadCheckBoxBereich3VEL12.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlBereich3DisplayWeightMitte_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlBereich3DisplayWeightMitte.TextChanged
        Try
            If _suspendEvents = True Then Exit Sub
            RadTextBoxControlBereich3ErrorLimitMitte.Text = CDec(RadTextBoxControlBereich3DisplayWeightMitte.Text) - CDec(RadTextBoxControlBereich3WeightMitte.Text)
            If RadTextBoxControlBereich3DisplayWeightMitte.Text > CDec(RadTextBoxControlBereich3WeightMitte.Text) + CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VELMitte.Checked = False
            ElseIf RadTextBoxControlBereich3DisplayWeightMitte.Text < CDec(RadTextBoxControlBereich3WeightMitte.Text) - CDec(lblBereich3EFGSpeziallBerechnung.Text) Then
                RadCheckBoxBereich3VELMitte.Checked = False
            Else
                RadCheckBoxBereich3VELMitte.Checked = True
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#End Region

    ''' <summary>
    ''' Öffnen der Eichfehlergrenzen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonShowEFG_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFG.Click, RadButtonShowEFG2.Click
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()
    End Sub

#End Region

#Region "Methods"
    Private Sub LoadFromDatabase()
        'zurücksetzten der Groupboxen größen auf default (designer) werte. Sonst würden die Groupboxen immer kleiner gerechnet
        RadGroupBoxBereich1.Size = New Size(503, 489)
        RadGroupBoxBereich2.Size = New Size(503, 489)
        RadGroupBoxBereich3.Size = New Size(503, 489)

        RadGroupBoxBereich1.Location = New Size(21, 149)
        RadGroupBoxBereich2.Location = New Size(21, 644)
        RadGroupBoxBereich3.Location = New Size(21, 1139)



        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1

                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis"). _
                                  Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Eichprotokoll") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungAussermittigeBelastung Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungAussermittigeBelastung = query.ToList

                Dim query2 = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungWiederholbarkeit = query2.ToList

                _currentObjVerfahren = (From a In context.Lookup_Konformitaetsbewertungsverfahren Where a.ID = objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren).FirstOrDefault
            End Using
        Else
          
            Try
                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                For Each obj In objEichprozess.Eichprotokoll.PruefungAussermittigeBelastung
                    _ListPruefungAussermittigeBelastung.Add(obj)
                Next

                For Each obj In objEichprozess.Eichprotokoll.PruefungWiederholbarkeit
                    _ListPruefungWiederholbarkeit.Add(obj)
                Next
            Catch ex As System.ObjectDisposedException
                Using context As New EichsoftwareClientdatabaseEntities1
                    Dim query = From a In context.PruefungAussermittigeBelastung Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungAussermittigeBelastung = query.ToList

                    Dim query2 = From a In context.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungWiederholbarkeit = query2.ToList
                End Using
            End Try
        

            _currentObjVerfahren = objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren
        End If


        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.FlowLayoutPanel1.Controls
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

#Region "FillControls"
    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    ''' 
    Private Sub FillControls()

        'Steuerlemente füllen
        FillControlsNullstellung()
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

        FillControlsAussermittigeBelastung()

        'Nur wenn es sich um das Staffel oder Fahrzeugwaagen verfahren handelt wird an dieser Stelle die Wiederholbarkeit geprüft. sonst erfolgt dies an einer anderen Stelle
        Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
            Case Is = "über 60kg mit Normalien"
                RadGroupBoxWiederholungen.Visible = False

            Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                FillControlsWiederholbarkeit()
        End Select





        'fokus setzen auf erstes Steuerelement
        RadCheckBoxNullstellungOK.Focus()

    End Sub

    Private Sub FillControlsNullstellung()
        If Not objEichprozess.Eichprotokoll.GenauigkeitNullstellung_InOrdnung Is Nothing Then
            RadCheckBoxNullstellungOK.Checked = objEichprozess.Eichprotokoll.GenauigkeitNullstellung_InOrdnung

        End If
    End Sub

    Private Sub FillControlsAussermittigeBelastung()
        'je nach anzahl der WZ entsprechendes Bild laden
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen <= 12 Then
            PictureBox4LC.Visible = False
            PictureBox6LC.Visible = False
            PictureBox8LC.Visible = False
            PictureBox12LC.Visible = True
        End If
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen <= 8 Then
            PictureBox4LC.Visible = False
            PictureBox6LC.Visible = False
            PictureBox8LC.Visible = True
            PictureBox12LC.Visible = False
        End If
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen <= 6 Then
            PictureBox4LC.Visible = False
            PictureBox6LC.Visible = True
            PictureBox8LC.Visible = False
            PictureBox12LC.Visible = False
        End If
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen <= 4 Then
            PictureBox4LC.Visible = True
            PictureBox6LC.Visible = False
            PictureBox8LC.Visible = False
            PictureBox12LC.Visible = False
        End If


        'je nach Art der Waage andere Bereichsgruppen ausblenden
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            RadGroupBoxBereich2.Visible = False
            RadGroupBoxBereich3.Visible = False

        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            RadGroupBoxBereich2.Visible = True
            RadGroupBoxBereich3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            RadGroupBoxBereich2.Visible = True
            RadGroupBoxBereich3.Visible = True

        End If

        'ein ausblenden von WZ Bereichenen
        EinAusblendenVonWZBereichenen()

        'füllen der berechnenten Steuerelemente

        lblBereich1EFGSpeziallBerechnung.Mask = String.Format("F{0}", _intNullstellenE) 'anzahl nullstellen für Textcontrol definieren
        lblBereich2EFGSpeziallBerechnung.Mask = String.Format("F{0}", _intNullstellenE) 'anzahl nullstellen für Textcontrol definieren
        lblBereich3EFGSpeziallBerechnung.Mask = String.Format("F{0}", _intNullstellenE) 'anzahl nullstellen für Textcontrol definieren
        'bereich1
        '=WENN($G$25>4;WERT(0,5*$B$15);(1*$B$15))
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen > 4 Then
            lblBereich1EFGSpeziallBerechnung.Text = 0.5 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        Else

            lblBereich1EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        End If

        'bereich 2
        Try
            If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen > 4 Then
                lblBereich2EFGSpeziallBerechnung.Text = 0.5 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
            Else
                lblBereich2EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
            End If
        Catch ex As Exception
        End Try
        'bereich 3
        Try
            If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen > 4 Then
                lblBereich3EFGSpeziallBerechnung.Text = 0.5 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            Else
                lblBereich3EFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            End If
        Catch ex As Exception
        End Try

        'Last Berechnen
        '=PRODUKT(B13/3)

        'bereich 1
        RadTextBoxControlBereich1Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight9.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight10.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight11.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1Weight12.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        RadTextBoxControlBereich1WeightMitte.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / 3
        'bereich 2
        Try
            RadTextBoxControlBereich2Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight9.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight10.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight11.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2Weight12.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
            RadTextBoxControlBereich2WeightMitte.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / 3
        Catch ex As Exception

        End Try
        'bereich 3
        Try
            RadTextBoxControlBereich3Weight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight4.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight5.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight6.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight7.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight8.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight9.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight10.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight11.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3Weight12.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
            RadTextBoxControlBereich3WeightMitte.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / 3
        Catch ex As Exception

        End Try



        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "1" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight1.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight1.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit1.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL1.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "2" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight2.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight2.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich1ErrorLimit2.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL2.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "3" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight3.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight3.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit3.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL3.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "4" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight4.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight4.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich1ErrorLimit4.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL4.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        Try
            'anzeige KG Nur laden wenn schon etwas eingegeben wurde
            _currentObjPruefungAussermittigeBelastung = Nothing
            _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "5" And o.Bereich = "1").FirstOrDefault

            If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
                RadTextBoxControlBereich1Weight5.Text = _currentObjPruefungAussermittigeBelastung.Last
                RadTextBoxControlBereich1DisplayWeight5.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


                RadTextBoxControlBereich1ErrorLimit5.Text = _currentObjPruefungAussermittigeBelastung.Fehler
                RadCheckBoxBereich1VEL5.Checked = _currentObjPruefungAussermittigeBelastung.EFG
            End If
        Catch ex As Exception

        End Try
        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "6" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight6.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight6.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit6.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL6.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "7" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight7.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight7.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit7.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL7.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "8" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight8.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight8.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit8.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL8.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "9" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight9.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight9.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit9.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL9.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "10" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight10.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight10.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit10.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL10.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "11" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight11.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight11.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit11.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL11.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "12" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1Weight12.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeight12.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimit12.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VEL12.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "M" And o.Bereich = "1").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich1WeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich1DisplayWeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich1ErrorLimitMitte.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich1VELMitte.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If


        'bereich 2

        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "1" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight1.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight1.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit1.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL1.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "2" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight2.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight2.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich2ErrorLimit2.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL2.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "3" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight3.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight3.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit3.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL3.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "4" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight4.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight4.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich2ErrorLimit4.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL4.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        Try
            'anzeige KG Nur laden wenn schon etwas eingegeben wurde
            _currentObjPruefungAussermittigeBelastung = Nothing
            _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "5" And o.Bereich = "2").FirstOrDefault

            If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
                RadTextBoxControlBereich2Weight5.Text = _currentObjPruefungAussermittigeBelastung.Last
                RadTextBoxControlBereich2DisplayWeight5.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


                RadTextBoxControlBereich2ErrorLimit5.Text = _currentObjPruefungAussermittigeBelastung.Fehler
                RadCheckBoxBereich2VEL5.Checked = _currentObjPruefungAussermittigeBelastung.EFG
            End If
        Catch ex As Exception

        End Try
        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "6" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight6.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight6.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit6.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL6.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "7" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight7.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight7.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit7.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL7.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "8" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight8.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight8.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit8.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL8.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "9" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight9.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight9.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit9.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL9.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "10" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight10.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight10.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit10.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL10.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "11" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight11.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight11.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit11.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL11.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "12" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2Weight12.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeight12.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimit12.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VEL12.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "M" And o.Bereich = "2").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich2WeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich2DisplayWeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich2ErrorLimitMitte.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich2VELMitte.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If


        'bereich 3

        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "1" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight1.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight1.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit1.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL1.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "2" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight2.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight2.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich3ErrorLimit2.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL2.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "3" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight3.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight3.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit3.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL3.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "4" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight4.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight4.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


            RadTextBoxControlBereich3ErrorLimit4.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL4.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        Try
            'anzeige KG Nur laden wenn schon etwas eingegeben wurde
            _currentObjPruefungAussermittigeBelastung = Nothing
            _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "5" And o.Bereich = "3").FirstOrDefault

            If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
                RadTextBoxControlBereich3Weight5.Text = _currentObjPruefungAussermittigeBelastung.Last
                RadTextBoxControlBereich3DisplayWeight5.Text = _currentObjPruefungAussermittigeBelastung.Anzeige


                RadTextBoxControlBereich3ErrorLimit5.Text = _currentObjPruefungAussermittigeBelastung.Fehler
                RadCheckBoxBereich3VEL5.Checked = _currentObjPruefungAussermittigeBelastung.EFG
            End If
        Catch ex As Exception

        End Try
        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "6" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight6.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight6.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit6.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL6.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "7" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight7.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight7.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit7.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL7.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "8" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight8.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight8.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit8.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL8.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "9" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight9.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight9.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit9.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL9.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "10" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight10.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight10.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit10.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL10.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "11" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight11.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight11.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit11.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL11.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "12" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3Weight12.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeight12.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimit12.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VEL12.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungAussermittigeBelastung = Nothing
        _currentObjPruefungAussermittigeBelastung = (From o In _ListPruefungAussermittigeBelastung Where o.Belastungsort = "M" And o.Bereich = "3").FirstOrDefault

        If Not _currentObjPruefungAussermittigeBelastung Is Nothing Then
            RadTextBoxControlBereich3WeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Last
            RadTextBoxControlBereich3DisplayWeightMitte.Text = _currentObjPruefungAussermittigeBelastung.Anzeige

            RadTextBoxControlBereich3ErrorLimitMitte.Text = _currentObjPruefungAussermittigeBelastung.Fehler
            RadCheckBoxBereich3VELMitte.Checked = _currentObjPruefungAussermittigeBelastung.EFG
        End If

    End Sub

    Private Sub EinAusblendenVonWZBereichenen()
        'aus dem Designer sind alle panel bis auf 1 und Mitte visible = false und werden heir auf sichtbar geschaltet
        Select Case objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
            Case 1 'sonderfall 1 WZ = dann trotzdem 4 Belastungsorte
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ4.Location.X, PanelBereich1WZ4.Location.Y + 30)
            Case 2
                'bereich 1
                PanelBereich1WZ2.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ2.Location.X, PanelBereich1WZ2.Location.Y + 30)

            Case 3
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ3.Location.X, PanelBereich1WZ3.Location.Y + 30)
            Case 4
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ4.Location.X, PanelBereich1WZ4.Location.Y + 30)
            Case 5
                'bereich 1

                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ5.Location.X, PanelBereich1WZ5.Location.Y + 30)
            Case 6
                'bereich 1

                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ6.Location.X, PanelBereich1WZ6.Location.Y + 30)
            Case 7
                'bereich 1

                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ7.Location.X, PanelBereich1WZ7.Location.Y + 30)
            Case 8
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ8.Location.X, PanelBereich1WZ8.Location.Y + 30)
            Case 9
                'bereich 1

                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True
                PanelBereich1WZ9.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True
                PanelBereich2WZ9.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True
                PanelBereich3WZ9.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ9.Location.X, PanelBereich1WZ9.Location.Y + 30)
            Case 10
                'bereich 1

                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True
                PanelBereich1WZ9.Visible = True
                PanelBereich1WZ10.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True
                PanelBereich2WZ9.Visible = True
                PanelBereich2WZ10.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True
                PanelBereich3WZ9.Visible = True
                PanelBereich3WZ10.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ10.Location.X, PanelBereich1WZ10.Location.Y + 30)
            Case 11
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True
                PanelBereich1WZ9.Visible = True
                PanelBereich1WZ10.Visible = True
                PanelBereich1WZ11.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True
                PanelBereich2WZ9.Visible = True
                PanelBereich2WZ10.Visible = True
                PanelBereich2WZ11.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True
                PanelBereich3WZ9.Visible = True
                PanelBereich3WZ10.Visible = True
                PanelBereich3WZ11.Visible = True

                'zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ11.Location.X, PanelBereich1WZ11.Location.Y + 30)
            Case 12
                'bereich 1
                PanelBereich1WZ2.Visible = True
                PanelBereich1WZ3.Visible = True
                PanelBereich1WZ4.Visible = True
                PanelBereich1WZ5.Visible = True
                PanelBereich1WZ6.Visible = True
                PanelBereich1WZ7.Visible = True
                PanelBereich1WZ8.Visible = True
                PanelBereich1WZ9.Visible = True
                PanelBereich1WZ10.Visible = True
                PanelBereich1WZ11.Visible = True
                PanelBereich1WZ12.Visible = True
                'bereich 2
                PanelBereich2WZ2.Visible = True
                PanelBereich2WZ3.Visible = True
                PanelBereich2WZ4.Visible = True
                PanelBereich2WZ5.Visible = True
                PanelBereich2WZ6.Visible = True
                PanelBereich2WZ7.Visible = True
                PanelBereich2WZ8.Visible = True
                PanelBereich2WZ9.Visible = True
                PanelBereich2WZ10.Visible = True
                PanelBereich2WZ11.Visible = True
                PanelBereich2WZ12.Visible = True
                'bereich 3
                PanelBereich3WZ2.Visible = True
                PanelBereich3WZ3.Visible = True
                PanelBereich3WZ4.Visible = True
                PanelBereich3WZ5.Visible = True
                PanelBereich3WZ6.Visible = True
                PanelBereich3WZ7.Visible = True
                PanelBereich3WZ8.Visible = True
                PanelBereich3WZ9.Visible = True
                PanelBereich3WZ10.Visible = True
                PanelBereich3WZ11.Visible = True
                PanelBereich3WZ12.Visible = True

                ''zusätzlich noch das Panel "mitte" nach oben verschieben. Differenz zur  location des letzten sichtbaren panels
                PanelBereich1WZMitte.Location = New Size(PanelBereich1WZ12.Location.X, PanelBereich1WZ12.Location.Y + 30)
        End Select
        Try
            'berechnen der neuen höhe für Groupboxen

            'höhe = höhe - ((max WZ - Anzahl WZ)*Höhe VoN WZ Panel - Abstand zwischen den Panels (etwa 10))
            Dim NeueHoehe As Integer

            'sonderfall bei 1 WZ werden trotzdem 4 Bereiche und mitte angezeigt
            If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = 1 Then
                NeueHoehe = RadGroupBoxBereich1.Height - ((12 - 4) * PanelBereich1WZ1.Height) - ((12 - 4) * 8)

            Else
                NeueHoehe = RadGroupBoxBereich1.Height - ((12 - objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen) * PanelBereich1WZ1.Height) - ((12 - objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen) * 8)

            End If

            RadGroupBoxBereich1.Height = NeueHoehe
            RadGroupBoxBereich2.Height = NeueHoehe
            RadGroupBoxBereich3.Height = NeueHoehe

            'nicht nur werden die Groupboxen kleiner, sie müssen auch verschoben werden . (Passiert in Relation zur Vorherigen Groupbox)
            RadGroupBoxBereich2.Location = New Size(RadGroupBoxBereich2.Location.X, RadGroupBoxBereich1.Location.Y + NeueHoehe + 20)
            'dritte Groupbox muss sogar doppelt so hoch verschoben werden 
            RadGroupBoxBereich3.Location = New Size(RadGroupBoxBereich3.Location.X, RadGroupBoxBereich1.Location.Y + (NeueHoehe * 2) + 40)

            'zuweisen der 2. und 3. mitte in relation zur oben gewählten Location der Mitte 1
            PanelBereich2WZMitte.Location = PanelBereich1WZMitte.Location
            PanelBereich3WZMitte.Location = PanelBereich1WZMitte.Location


            'berechnen der äußeren Group Box Hoehe.
            'Der wert ergibt sich aus der neuen Position der letzten sichtbaren Groupbox + deren neuer höher+ einige Pixel Puffer

            If RadGroupBoxBereich3.Visible = True Then
                RadGroupBoxPruefungAussermittigeBelastung.Size = New Size(RadGroupBoxPruefungAussermittigeBelastung.Size.Width, RadGroupBoxBereich3.Location.Y + RadGroupBoxBereich3.Size.Height + 30)
            ElseIf RadGroupBoxBereich2.Visible = True Then
                RadGroupBoxPruefungAussermittigeBelastung.Size = New Size(RadGroupBoxPruefungAussermittigeBelastung.Size.Width, RadGroupBoxBereich2.Location.Y + RadGroupBoxBereich2.Size.Height + 30)
            ElseIf RadGroupBoxBereich1.Visible = True Then
                RadGroupBoxPruefungAussermittigeBelastung.Size = New Size(RadGroupBoxPruefungAussermittigeBelastung.Size.Width, RadGroupBoxBereich1.Location.Y + RadGroupBoxBereich1.Size.Height + 30)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FillControlsWiederholbarkeit()
        'füllen der berechnenten Steuerelemente

        lblEFGSpeziallBerechnung.Mask = String.Format("F{0}", _intNullstellenE) 'anzahl nullstellen für Textcontrol definieren
        'berechnen der EFGs

        Try
            RadTextBoxControlBetragNormallast.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien

        Catch ex As Exception

        End Try


        'bereich 1

        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                lblEFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                RadTextBoxControlWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
                RadTextBoxControlWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
                RadTextBoxControlWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * 0.5
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                lblEFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                RadTextBoxControlWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
                RadTextBoxControlWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
                RadTextBoxControlWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * 0.5
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                lblEFGSpeziallBerechnung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

                RadTextBoxControlWeight1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
                RadTextBoxControlWeight2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
                RadTextBoxControlWeight3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * 0.5
        End Select




        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        'bereich 1
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "1" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlWeight1.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlDisplayWeight1.Text = _currentObjPruefungWiederholbarkeit.Anzeige
            RadTextBoxControlErrorLimit1.Text = _currentObjPruefungWiederholbarkeit.Fehler
            RadCheckBoxVEL1.Checked = _currentObjPruefungWiederholbarkeit.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "2" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlWeight2.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlDisplayWeight2.Text = _currentObjPruefungWiederholbarkeit.Anzeige
            RadTextBoxControlErrorLimit2.Text = _currentObjPruefungWiederholbarkeit.Fehler
            RadCheckBoxVEL2.Checked = _currentObjPruefungWiederholbarkeit.EFG
        End If

        'anzeige KG Nur laden wenn schon etwas eingegeben wurde
        _currentObjPruefungWiederholbarkeit = Nothing
        _currentObjPruefungWiederholbarkeit = (From o In _ListPruefungWiederholbarkeit Where o.Wiederholung = "3" And o.Belastung = "halb").FirstOrDefault

        If Not _currentObjPruefungWiederholbarkeit Is Nothing Then
            RadTextBoxControlWeight3.Text = _currentObjPruefungWiederholbarkeit.Last
            RadTextBoxControlDisplayWeight3.Text = _currentObjPruefungWiederholbarkeit.Anzeige
            RadTextBoxControlErrorLimit3.Text = _currentObjPruefungWiederholbarkeit.Fehler
            RadCheckBoxVEL3.Checked = _currentObjPruefungWiederholbarkeit.EFG
        End If


    End Sub

#End Region

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        objEichprozess.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = RadCheckBoxNullstellungOK.Checked
        objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = RadTextBoxControlBetragNormallast.Text
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungAussermittigeBelastung)
        If PObjPruefung.Bereich = 1 Then
            If PObjPruefung.Belastungsort = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL1.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL2.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL3.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL4.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL5.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL6.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL7.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL8.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "9" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight9.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight9.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit9.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL9.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "10" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight10.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight10.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit10.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL10.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "11" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight11.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight11.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit11.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL11.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "12" Then
                PObjPruefung.Last = RadTextBoxControlBereich1Weight12.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeight12.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimit12.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VEL12.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "M" Then
                PObjPruefung.Last = RadTextBoxControlBereich1WeightMitte.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich1DisplayWeightMitte.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich1ErrorLimitMitte.Text
                PObjPruefung.EFG = RadCheckBoxBereich1VELMitte.Checked
                PObjPruefung.EFGExtra = lblBereich1EFGSpeziallBerechnung.Text
            End If

        ElseIf PObjPruefung.Bereich = "2" Then
            If PObjPruefung.Belastungsort = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL1.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL2.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL3.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL4.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL5.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL6.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL7.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL8.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "9" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight9.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight9.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit9.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL9.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "10" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight10.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight10.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit10.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL10.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "11" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight11.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight11.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit11.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL11.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "12" Then
                PObjPruefung.Last = RadTextBoxControlBereich2Weight12.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeight12.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimit12.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VEL12.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "M" Then
                PObjPruefung.Last = RadTextBoxControlBereich2WeightMitte.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich2DisplayWeightMitte.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich2ErrorLimitMitte.Text
                PObjPruefung.EFG = RadCheckBoxBereich2VELMitte.Checked
                PObjPruefung.EFGExtra = lblBereich2EFGSpeziallBerechnung.Text
            End If

        ElseIf PObjPruefung.Bereich = 3 Then
            If PObjPruefung.Belastungsort = "1" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL1.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "2" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL2.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "3" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL3.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "4" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight4.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight4.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit4.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL4.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "5" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight5.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight5.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit5.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL5.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "6" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight6.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight6.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit6.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL6.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "7" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight7.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight7.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit7.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL7.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "8" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight8.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight8.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit8.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL8.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "9" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight9.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight9.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit9.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL9.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "10" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight10.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight10.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit10.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL10.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "11" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight11.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight11.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit11.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL11.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

            If PObjPruefung.Belastungsort = "12" Then
                PObjPruefung.Last = RadTextBoxControlBereich3Weight12.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeight12.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimit12.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VEL12.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Belastungsort = "M" Then
                PObjPruefung.Last = RadTextBoxControlBereich3WeightMitte.Text
                PObjPruefung.Anzeige = RadTextBoxControlBereich3DisplayWeightMitte.Text
                PObjPruefung.Fehler = RadTextBoxControlBereich3ErrorLimitMitte.Text
                PObjPruefung.EFG = RadCheckBoxBereich3VELMitte.Checked
                PObjPruefung.EFGExtra = lblBereich3EFGSpeziallBerechnung.Text
            End If

        End If
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungWiederholbarkeit)
        If PObjPruefung.Belastung = "halb" Then
            If PObjPruefung.Wiederholung = "1" Then
                PObjPruefung.Last = RadTextBoxControlWeight1.Text
                PObjPruefung.Anzeige = RadTextBoxControlDisplayWeight1.Text
                PObjPruefung.Fehler = RadTextBoxControlErrorLimit1.Text
                PObjPruefung.EFG = RadCheckBoxVEL1.Checked
                PObjPruefung.EFG_Extra = lblEFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "2" Then
                PObjPruefung.Last = RadTextBoxControlWeight2.Text
                PObjPruefung.Anzeige = RadTextBoxControlDisplayWeight2.Text
                PObjPruefung.Fehler = RadTextBoxControlErrorLimit2.Text
                PObjPruefung.EFG = RadCheckBoxVEL2.Checked
                PObjPruefung.EFG_Extra = lblEFGSpeziallBerechnung.Text
            End If
            If PObjPruefung.Wiederholung = "3" Then
                PObjPruefung.Last = RadTextBoxControlWeight3.Text
                PObjPruefung.Anzeige = RadTextBoxControlDisplayWeight3.Text
                PObjPruefung.Fehler = RadTextBoxControlErrorLimit3.Text
                PObjPruefung.EFG = RadCheckBoxVEL3.Checked
                PObjPruefung.EFG_Extra = lblEFGSpeziallBerechnung.Text
            End If

        End If
    End Sub

#Region "validate Controls"
#Region "Nullstellung"
    Private Function ValidateControlsNullstellung() As Boolean
        If RadCheckBoxNullstellungOK.Checked = False Then
            AbortSaveing = True
            '   MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            RadCheckBoxNullstellungOK.Focus()
            Return False
        End If
        Return True
    End Function

#End Region
#Region "Wiederholbarkeit"
    Private Function ValidatecontrolsWiederholungen() As Boolean
        If RadGroupBoxWiederholungen.Visible = True Then
            If RadCheckBoxVEL1.Checked = False And RadCheckBoxVEL1.Visible = True Or _
      RadCheckBoxVEL2.Checked = False And RadCheckBoxVEL2.Visible = True Or _
      RadCheckBoxVEL3.Checked = False And RadCheckBoxVEL3.Visible = True Then
                AbortSaveing = True
                Return False

            End If
        End If
        Return True
    End Function

#End Region

#Region "aussermittige Belastung"
    Private Function ValidateControlsAussermittigeBelastung() As Boolean

        If RadCheckBoxBereich1VEL1.Checked = False And RadCheckBoxBereich1VEL1.Visible = True Or _
        RadCheckBoxBereich1VEL2.Checked = False And RadCheckBoxBereich1VEL2.Visible = True Or _
        RadCheckBoxBereich1VEL3.Checked = False And RadCheckBoxBereich1VEL3.Visible = True Or _
        RadCheckBoxBereich1VEL4.Checked = False And RadCheckBoxBereich1VEL4.Visible = True Or _
        RadCheckBoxBereich1VEL5.Checked = False And RadCheckBoxBereich1VEL5.Visible = True Or _
        RadCheckBoxBereich1VEL6.Checked = False And RadCheckBoxBereich1VEL6.Visible = True Or _
        RadCheckBoxBereich1VEL7.Checked = False And RadCheckBoxBereich1VEL7.Visible = True Or _
        RadCheckBoxBereich1VEL8.Checked = False And RadCheckBoxBereich1VEL8.Visible = True Or _
        RadCheckBoxBereich1VEL9.Checked = False And RadCheckBoxBereich1VEL9.Visible = True Or _
        RadCheckBoxBereich1VEL10.Checked = False And RadCheckBoxBereich1VEL10.Visible = True Or _
        RadCheckBoxBereich1VEL11.Checked = False And RadCheckBoxBereich1VEL11.Visible = True Or _
        RadCheckBoxBereich1VEL12.Checked = False And RadCheckBoxBereich1VEL12.Visible = True Or _
        RadCheckBoxBereich1VELMitte.Checked = False And RadCheckBoxBereich1VELMitte.Visible = True Or _
        RadCheckBoxBereich2VEL1.Checked = False And RadCheckBoxBereich2VEL1.Visible = True Or _
        RadCheckBoxBereich2VEL2.Checked = False And RadCheckBoxBereich2VEL2.Visible = True Or _
        RadCheckBoxBereich2VEL3.Checked = False And RadCheckBoxBereich2VEL3.Visible = True Or _
        RadCheckBoxBereich2VEL4.Checked = False And RadCheckBoxBereich2VEL4.Visible = True Or _
        RadCheckBoxBereich2VEL5.Checked = False And RadCheckBoxBereich2VEL5.Visible = True Or _
        RadCheckBoxBereich2VEL6.Checked = False And RadCheckBoxBereich2VEL6.Visible = True Or _
        RadCheckBoxBereich2VEL7.Checked = False And RadCheckBoxBereich2VEL7.Visible = True Or _
        RadCheckBoxBereich2VEL8.Checked = False And RadCheckBoxBereich2VEL8.Visible = True Or _
        RadCheckBoxBereich2VEL9.Checked = False And RadCheckBoxBereich2VEL9.Visible = True Or _
        RadCheckBoxBereich2VEL10.Checked = False And RadCheckBoxBereich2VEL10.Visible = True Or _
        RadCheckBoxBereich2VEL11.Checked = False And RadCheckBoxBereich2VEL11.Visible = True Or _
        RadCheckBoxBereich2VEL12.Checked = False And RadCheckBoxBereich2VEL12.Visible = True Or _
        RadCheckBoxBereich2VELMitte.Checked = False And RadCheckBoxBereich2VELMitte.Visible = True Or _
        RadCheckBoxBereich3VEL1.Checked = False And RadCheckBoxBereich3VEL1.Visible = True Or _
        RadCheckBoxBereich3VEL2.Checked = False And RadCheckBoxBereich3VEL2.Visible = True Or _
        RadCheckBoxBereich3VEL3.Checked = False And RadCheckBoxBereich3VEL3.Visible = True Or _
        RadCheckBoxBereich3VEL4.Checked = False And RadCheckBoxBereich3VEL4.Visible = True Or _
        RadCheckBoxBereich3VEL5.Checked = False And RadCheckBoxBereich3VEL5.Visible = True Or _
        RadCheckBoxBereich3VEL6.Checked = False And RadCheckBoxBereich3VEL6.Visible = True Or _
        RadCheckBoxBereich3VEL7.Checked = False And RadCheckBoxBereich3VEL7.Visible = True Or _
        RadCheckBoxBereich3VEL8.Checked = False And RadCheckBoxBereich3VEL8.Visible = True Or _
        RadCheckBoxBereich3VEL9.Checked = False And RadCheckBoxBereich3VEL9.Visible = True Or _
        RadCheckBoxBereich3VEL10.Checked = False And RadCheckBoxBereich3VEL10.Visible = True Or _
        RadCheckBoxBereich3VEL11.Checked = False And RadCheckBoxBereich3VEL11.Visible = True Or _
        RadCheckBoxBereich3VEL12.Checked = False And RadCheckBoxBereich3VEL12.Visible = True Or _
        RadCheckBoxBereich3VELMitte.Checked = False And RadCheckBoxBereich3VELMitte.Visible = True Then
            AbortSaveing = True
            Return False

        End If
        Return True
    End Function

#End Region
#End Region


    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        AbortSaveing = False

        If ValidateControlsNullstellung() Then
            If ValidateControlsAussermittigeBelastung() Then
                If ValidatecontrolsWiederholungen() Then
                Else
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    AbortSaveing = True
                    Return False
                End If
            Else
                MessageBox.Show(My.Resources.GlobaleLokalisierung.EichfehlergrenzenNichtEingehalten, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                AbortSaveing = True
                Return False
            End If
        Else
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            AbortSaveing = True
            Return False
        End If


        'Speichern soll nicht abgebrochen werden, da alles okay ist
        AbortSaveing = False
        Return True

    End Function

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        End If
                    Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        End If
                End Select
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                        End If
                    Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                        End If
                End Select
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



                            SaveAussermittigeBelastung(Context)
                            SaveWiederholungen(Context)


                            'neuen Status zuweisen
                            'die reihenfolge wird hier je nach Verfahren verändert
                            Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                                Case Is = "über 60kg mit Normalien"
                                    If AktuellerStatusDirty = False Then
                                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet Then
                                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                                        End If
                                    ElseIf AktuellerStatusDirty = True Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet
                                        AktuellerStatusDirty = False
                                    End If
                                Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                                    If AktuellerStatusDirty = False Then
                                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast Then
                                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                                        End If
                                    ElseIf AktuellerStatusDirty = True Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
                                        AktuellerStatusDirty = False
                                    End If
                            End Select



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

    Private Sub SaveAussermittigeBelastung(ByRef Context As EichsoftwareClientdatabaseEntities1)
        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
        If _ListPruefungAussermittigeBelastung.Count = 0 Then
            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
            Dim intBereiche As Integer = 0
            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                intBereiche = 1
            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                intBereiche = 2
            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                intBereiche = 3
            End If

            For j = 1 To intBereiche
                'sonderfall eine Wägezelle
                If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = 1 Then
                    For intBelastungsort As Integer = 1 To 5 'eine Mehr für Mitte
                        Dim objPruefung = Context.PruefungAussermittigeBelastung.Create
                        'wenn es die eine itereation mehr ist:
                        If intBelastungsort = 5 Then
                            'mitte anlegen
                            objPruefung.Belastungsort = "M"
                        Else 'sonst bereich zuweisen
                            objPruefung.Belastungsort = intBelastungsort
                        End If
                        objPruefung.Bereich = j
                        UpdatePruefungsObject(objPruefung)
                        Try
                            Context.SaveChanges()

                        Catch ex As Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                        End Try

                        objEichprozess.Eichprotokoll.PruefungAussermittigeBelastung.Add(objPruefung)
                        Try
                            Context.SaveChanges()

                        Catch ex As Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                        End Try

                        _ListPruefungAussermittigeBelastung.Add(objPruefung)
                    Next
                Else
                    For intBelastungsort As Integer = 1 To (objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen + 1) 'eine Mehr für Mitte
                        Dim objPruefung = Context.PruefungAussermittigeBelastung.Create
                        'wenn es die eine itereation mehr ist:
                        If intBelastungsort = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen + 1 Then
                            'mitte anlegen
                            objPruefung.Belastungsort = "M"
                        Else 'sonst bereich zuweisen
                            objPruefung.Belastungsort = intBelastungsort
                        End If
                        objPruefung.Bereich = j
                        UpdatePruefungsObject(objPruefung)

                        Try
                            Context.SaveChanges()

                        Catch ex As Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                        End Try


                        objEichprozess.Eichprotokoll.PruefungAussermittigeBelastung.Add(objPruefung)
                        Try
                            Context.SaveChanges()

                        Catch ex As Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                        End Try

                        _ListPruefungAussermittigeBelastung.Add(objPruefung)
                    Next
                End If


            Next
        Else ' es gibt bereits welche
            'jedes objekt initialisieren und aus context laden und updaten
            For Each objPruefung In _ListPruefungAussermittigeBelastung
                objPruefung = Context.PruefungAussermittigeBelastung.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsObject(objPruefung)
                Context.SaveChanges()
            Next

        End If

    End Sub

    Private Sub SaveWiederholungen(ByRef Context As EichsoftwareClientdatabaseEntities1)
        If RadGroupBoxWiederholungen.Visible = True Then

            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
            If _ListPruefungWiederholbarkeit.Count = 0 Then
                'anzahl Wiederholungen beträgt 3 um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                For i As Integer = 1 To 3

                    'halbe Last
                    Dim objPruefung = Context.PruefungWiederholbarkeit.Create
                    'wenn es die eine itereation mehr ist:
                    objPruefung.Wiederholung = i
                    objPruefung.Belastung = "halb"
                    UpdatePruefungsObject(objPruefung)

                    Try
                        Context.SaveChanges()

                    Catch ex As Validation.DbEntityValidationException
                        For Each e In ex.EntityValidationErrors
                            MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                        Next
                    End Try


                    objEichprozess.Eichprotokoll.PruefungWiederholbarkeit.Add(objPruefung)
                    Try
                        Context.SaveChanges()

                    Catch ex As Validation.DbEntityValidationException
                        For Each e In ex.EntityValidationErrors
                            MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                        Next
                    End Try


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
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.Include("Lookup_Waagenart").FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen

                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungAussermittigeBelastung.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

                            SaveAussermittigeBelastung(Context)
                            SaveWiederholungen(Context)

                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()
                            'Speichern in Datenbank
                            Context.SaveChanges()
                        End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoPruefungNullstellungUndAussermittigeBelastung))



        'Me.RadGroupBoxVerbindungselemente.Text = resources.GetString("RadGroupBoxVerbindungselemente.Text")
        Me.RadGroupBoxBereich1.Text = resources.GetString("RadGroupBoxBereich1.Text")
        Me.RadGroupBoxBereich2.Text = resources.GetString("RadGroupBoxBereich2.Text")
        Me.RadGroupBoxBereich3.Text = resources.GetString("RadGroupBoxBereich3.Text")
        Me.RadGroupBoxPruefungAussermittigeBelastung.Text = resources.GetString("RadGroupBoxPruefungAussermittigeBelastung.Text")
        Me.RadGroupBoxPruefungGenaugikeit.Text = resources.GetString("RadGroupBoxPruefungGenaugikeit.Text")
        Me.RadGroupBoxWiederholungen.Text = resources.GetString("RadGroupBoxWiederholungen.Text")
        Me.lblAnzeigeGewicht.Text = resources.GetString("lblAnzeigeGewicht.Text")
        Me.lblBereich1AnzeigeGewicht.Text = resources.GetString("lblBereich1AnzeigeGewicht.Text")
        Me.lblBereich2AnzeigeGewicht.Text = resources.GetString("lblBereich2AnzeigeGewicht.Text")
        Me.lblBereich3AnzeigeGewicht.Text = resources.GetString("lblBereich3AnzeigeGewicht.Text")

        Me.lblBereich1EFGSpezial.Text = resources.GetString("lblBereich1EFGSpezial.Text")
        Me.lblBereich1EFGSpeziallBerechnung.Text = resources.GetString("lblBereich1EFGSpeziallBerechnung.Text")
        Me.lblBereich1FehlerGrenzen.Text = resources.GetString("lblBereich1FehlerGrenzen.Text")
        Me.lblBereich1Gewicht.Text = resources.GetString("lblBereich1Gewicht.Text")
        Me.lblBereich1Mitte.Text = resources.GetString("lblBereich1Mitte.Text")

        Me.lblBereich2EFGSpezial.Text = resources.GetString("lblBereich2EFGSpezial.Text")
        Me.lblBereich2EFGSpeziallBerechnung.Text = resources.GetString("lblBereich2EFGSpeziallBerechnung.Text")
        Me.lblBereich2FehlerGrenzen.Text = resources.GetString("lblBereich2FehlerGrenzen.Text")
        Me.lblBereich2Gewicht.Text = resources.GetString("lblBereich2Gewicht.Text")

        Me.lblBereich3EFGSpezial.Text = resources.GetString("lblBereich3EFGSpezial.Text")
        Me.lblBereich3EFGSpeziallBerechnung.Text = resources.GetString("lblBereich3EFGSpeziallBerechnung.Text")
        Me.lblBereich3FehlerGrenzen.Text = resources.GetString("lblBereich3FehlerGrenzen.Text")
        Me.lblBereich3Gewicht.Text = resources.GetString("lblBereich3Gewicht.Text")


        Me.lblMengeStandardgewichte1.Text = resources.GetString("lblMengeStandardgewichte1.Text")
        Me.lblMengeStandardgewichte2.Text = resources.GetString("lblMengeStandardgewichte2.Text")
        Me.lblMessOrtBereich1.Text = resources.GetString("lblMessOrtBereich1.Text")
        Me.lblMessOrtBereich2.Text = resources.GetString("lblMessOrtBereich2.Text")
        Me.lblMessOrtBereich3.Text = resources.GetString("lblMessOrtBereich3.Text")
        Me.lblNullstellungOK.Text = resources.GetString("lblNullstellungOK.Text")
        Me.lblWiederholungen.Text = resources.GetString("lblWiederholungen.Text")


        Me.lblAnzeigeGewicht.Text = resources.GetString("lblAnzeigeGewicht.Text")
        Me.lblEFGSpezial.Text = resources.GetString("lblEFGSpezial.Text")
        Me.lblEFGSpeziallBerechnung.Text = resources.GetString("lblEFGSpeziallBerechnung.Text")
        Me.lblMengeStandardgewichte1.Text = resources.GetString("lblMengeStandardgewichte1.Text")
        Me.lblMengeStandardgewichte2.Text = resources.GetString("lblMengeStandardgewichte2.Text")
        Me.lblFehlerGrenzen.Text = resources.GetString("lblFehlerGrenzen.Text")
        Me.lblGewicht.Text = resources.GetString("lblGewicht.Text")



        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAussermittigerBelastung)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungNullstellungundAussermittigeBelastung
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAussermittigerBelastung)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungNullstellungundAussermittigeBelastung
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

#Region "Hilfetexte"
    Private Sub RadGroupBoxPruefungGenaugikeit_MouseHover(sender As Object, e As EventArgs) Handles RadGroupBoxPruefungGenaugikeit.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Nullstellung)
    End Sub

    Private Sub RadGroupBoxPruefungAussermittigeBelastung_MouseHover(sender As Object, e As EventArgs) Handles RadGroupBoxPruefungAussermittigeBelastung.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAussermittigerBelastung)
    End Sub

    Private Sub RadGroupBoxWiederholungen_MouseEnter(sender As Object, e As EventArgs) Handles RadGroupBoxWiederholungen.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungWiederholbarkeitStaffelverfahren)

    End Sub

#End Region






    Private Sub RadCheckBoxBereich1VEL1_MouseClick(sender As Object, e As EventArgs) Handles RadCheckBoxVEL3.Click, RadCheckBoxVEL2.Click, RadCheckBoxVEL1.Click, RadCheckBoxBereich3VELMitte.MouseClick, RadCheckBoxBereich3VEL9.MouseClick, RadCheckBoxBereich3VEL8.MouseClick, RadCheckBoxBereich3VEL7.MouseClick, RadCheckBoxBereich3VEL6.MouseClick, RadCheckBoxBereich3VEL5.MouseClick, RadCheckBoxBereich3VEL4.MouseClick, RadCheckBoxBereich3VEL3.MouseClick, RadCheckBoxBereich3VEL2.MouseClick, RadCheckBoxBereich3VEL12.MouseClick, RadCheckBoxBereich3VEL11.MouseClick, RadCheckBoxBereich3VEL10.MouseClick, RadCheckBoxBereich3VEL1.MouseClick, RadCheckBoxBereich2VELMitte.MouseClick, RadCheckBoxBereich2VEL9.MouseClick, RadCheckBoxBereich2VEL8.MouseClick, RadCheckBoxBereich2VEL7.MouseClick, RadCheckBoxBereich2VEL6.MouseClick, RadCheckBoxBereich2VEL5.MouseClick, RadCheckBoxBereich2VEL4.MouseClick, RadCheckBoxBereich2VEL3.MouseClick, RadCheckBoxBereich2VEL2.MouseClick, RadCheckBoxBereich2VEL12.MouseClick, RadCheckBoxBereich2VEL11.MouseClick, RadCheckBoxBereich2VEL10.MouseClick, RadCheckBoxBereich2VEL1.MouseClick, RadCheckBoxBereich1VELMitte.MouseClick, RadCheckBoxBereich1VEL9.MouseClick, RadCheckBoxBereich1VEL8.MouseClick, RadCheckBoxBereich1VEL7.MouseClick, RadCheckBoxBereich1VEL6.MouseClick, RadCheckBoxBereich1VEL5.MouseClick, RadCheckBoxBereich1VEL4.MouseClick, RadCheckBoxBereich1VEL3.MouseClick, RadCheckBoxBereich1VEL2.MouseClick, RadCheckBoxBereich1VEL12.MouseClick, RadCheckBoxBereich1VEL11.MouseClick, RadCheckBoxBereich1VEL10.MouseClick, RadCheckBoxBereich1VEL1.MouseClick

    End Sub

    Private Sub RadTextBoxControlErrorLimit1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlErrorLimit3.TextChanged, RadTextBoxControlErrorLimit2.TextChanged, RadTextBoxControlErrorLimit1.TextChanged
        Try

            Dim MAX20 As Decimal = 0
            Dim MAx35 As Decimal = 0
            Dim max50 As Decimal = 0
            Dim currentLast As Decimal = 0

            Try
                currentLast = RadTextBoxControlBetragNormallast.Text
            Catch ex As Exception
            End Try

            Dim newLast As Decimal = 0

            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                MAX20 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1) * 0.2
                MAx35 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1) * 0.35
                max50 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1) * 0.5
            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                MAX20 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2) * 0.2
                MAx35 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2) * 0.35
                max50 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2) * 0.5
            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                MAX20 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3) * 0.2
                MAx35 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3) * 0.35
                max50 = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3) * 0.5
            End If

            'wiederholung1

            Try
                If (CDec(RadTextBoxControlDisplayWeight1.Text) - CDec(RadTextBoxControlWeight1.Text)) > (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    newLast = max50
                ElseIf (CDec(RadTextBoxControlDisplayWeight1.Text) - CDec(RadTextBoxControlWeight1.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.22) Then
                    newLast = MAX20
                ElseIf (CDec(RadTextBoxControlDisplayWeight1.Text) - CDec(RadTextBoxControlWeight1.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    newLast = MAx35
                Else
                    newLast = MAX20
                End If
                RadTextBoxControlBetragNormallast.Text = newLast
            Catch ex As Exception
            End Try
            'wiederholung 2
            Try
                If (CDec(RadTextBoxControlDisplayWeight2.Text) - CDec(RadTextBoxControlWeight2.Text)) > (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    If newLast < max50 Then
                        newLast = max50
                    End If
                ElseIf (CDec(RadTextBoxControlDisplayWeight2.Text) - CDec(RadTextBoxControlWeight2.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.22) Then
                    If newLast < MAX20 Then
                        newLast = MAX20
                    End If
                ElseIf (CDec(RadTextBoxControlDisplayWeight2.Text) - CDec(RadTextBoxControlWeight2.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    If newLast < MAx35 Then
                        newLast = MAx35
                    End If
                Else
                    If newLast < MAX20 Then
                        newLast = MAX20
                    End If
                End If
                RadTextBoxControlBetragNormallast.Text = newLast
            Catch ex As Exception
            End Try

            'wiederholung 3
            Try
                If (CDec(RadTextBoxControlDisplayWeight3.Text) - CDec(RadTextBoxControlWeight3.Text)) > (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    If newLast < max50 Then
                        newLast = max50
                    End If
                ElseIf (CDec(RadTextBoxControlDisplayWeight3.Text) - CDec(RadTextBoxControlWeight3.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.22) Then
                    If newLast < MAX20 Then
                        newLast = MAX20
                    End If
                ElseIf (CDec(RadTextBoxControlDisplayWeight3.Text) - CDec(RadTextBoxControlWeight3.Text)) < (CDec(lblEFGSpeziallBerechnung.Text) * 0.33) Then
                    If newLast < MAx35 Then
                        newLast = MAx35
                    End If
                Else
                    If newLast < MAX20 Then
                        newLast = MAX20
                    End If
                End If
                RadTextBoxControlBetragNormallast.Text = newLast
            Catch ex As Exception
            End Try


        Catch ex As Exception
        End Try
    End Sub


    'Entsperrroutine
    Protected Friend Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.FlowLayoutPanel1.Controls
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
