Imports System.ComponentModel
''' <summary>
''' Das UcoContent bietet die Basisklasse für alle im Eichprozess genutzten Ucos. Es enthält allgemein gültige Methoden und Eigenschaften
''' </summary>
''' <remarks></remarks>
Public Class ucoContent
    Implements INotifyPropertyChanged


#Region "Member Variables"
    Private WithEvents _ParentForm As FrmMainContainer 'Bezug zum Parentcontrol.
    Private _PreviousUco As ucoContent 'mit dieser variable merkt sich das aktuelle UCO, welches Uco VOR ihm im Eichprozess dran kam
    Private _NextUco As ucoContent 'mit dieser variable merkt sich das aktuelle UCO, welches Uco NACH ihm im Eichprozess dran kam
    Private _objEichprozess As Eichprozess 'das aktuelle Eichprozess Objekt
    'Private _bolSuspendRefresh As Boolean = False
    Protected Friend _intNullstellenE1 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE2 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE3 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE As Integer = 0
    Private _bolEichprozessIsDirty = False

    ''' <summary>
    ''' sobald gravierende Änderungen im aktuellen Status vorgenommen werden, wird das Dirty Flag gesetzt. So kann überprüft werden ob Updates durchgeführt werden müssen und ob der aktuelle Vorgangsstatus zurückgesetzt werden muss
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Property AktuellerStatusDirty As Boolean
        Get
            Return _bolEichprozessIsDirty
        End Get
        Set(value As Boolean)
            _bolEichprozessIsDirty = value
            onpropertychanged(_bolEichprozessIsDirty)
        End Set
    End Property

#Region "Property Changed Event für Dirty Flag, damit Ampel UCO darauf reagieren kann"
    ' Declare the event 
    Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub onpropertychanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region

#Region "Enumartoren"
    Enum enuDialogModus
        normal = 0
        lesend = 1
        korrigierend = 2
    End Enum
#End Region

#End Region
#Region "Properties"
    ''' <summary>
    ''' Property die genutzt wird um den Speicher und Blättervorgang zu unterbrechen (wenn z.b. nicht alle Pflichtfelder ausgefüllt wurden)
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Property AbortSaveing As Boolean
    ''' <summary>
    ''' Mit dieser Property kann der LEsemodus des UCOs eingestellt werden. Normal meint einen Client der eine Eichung anlegend. Im Lesenden Modus darf keine änderung vorgenommen / gespeichert werden. Im Korrigierenden Modus ändern RHEWA die Eichung eines externen bevollmächtigten
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Property DialogModus As enuDialogModus = enuDialogModus.normal
    ''' <summary>
    ''' Property die genutzt wird, für das Rückwärts navigieren. Das Eichprozess Objekt kann vom Status bereits viel weiter sein, dennoch muss ein Navigieren durch die UCOs ermöglicht werden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Property EichprozessStatusReihenfolge As GlobaleEnumeratoren.enuEichprozessStatus

    ''' <summary>
    ''' Gets or sets the obj eichprozess.
    ''' </summary>
    ''' <value>The obj eichprozess.</value>
    Protected Friend Property objEichprozess As Eichprozess
        Get
            Return _objEichprozess
        End Get
        Set(value As Eichprozess)
            _objEichprozess = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the parent formular.
    ''' </summary>
    ''' <value>The parent formular.</value>
    Protected Friend Property ParentFormular As FrmMainContainer
        Get
            Return _ParentForm
        End Get
        Set(value As FrmMainContainer)
            _ParentForm = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the previous uco.
    ''' </summary>
    ''' <value>The previous uco.</value>
    Protected Friend Property PreviousUco As ucoContent
        Get
            Return _PreviousUco
        End Get
        Set(value As ucoContent)
            _PreviousUco = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the next uco.
    ''' </summary>
    ''' <value>The next uco.</value>
    Protected Friend Property NextUco As ucoContent
        Get
            Return _NextUco
        End Get
        Set(value As ucoContent)
            _NextUco = value
        End Set
    End Property

#End Region

#Region "Constructors"
    Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        _ParentForm = Nothing
        _PreviousUco = Nothing
        _NextUco = Nothing
    End Sub


    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        'TH zuweisen des Eltern elements
        _ParentForm = pParentform
        'TH zuweisen des vorherigen Steuerelements
        _PreviousUco = pPreviousUco
        'TH zuweisen des nachfolgenden Steuerelements, wenn es schon existiert
        _NextUco = pNextUco
        _objEichprozess = pObjEichprozess
        ''TH die Zuweisungen werden für die BLätterfunktionalität genutzt
        '_Breadcrumb = pBreadcrum
        'TH zuweisen des Zugriffsmoduses auf den Dialog
        DialogModus = pEnuModus

        If Not _ParentForm Is Nothing Then
            _ParentForm.SETContextHelpText("")
        End If

    End Sub


#End Region

#Region "Overidables"
    ''' <summary>
    ''' holt Steuerelement Objekt anhand von Namen als String
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThroughAttribute> _
    Protected Overridable Function FindControl(ByVal Name As String) As Control
        Dim myControl As Control()
        myControl = Me.Controls.Find(Name, True)

        If myControl.Count > 0 Then
            Return myControl(0)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Nullstellen Berechnung. Anzahl der Nullstellen ist abhängig vom Eichwert
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub HoleNullstellen()
        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:
        Try
            _intNullstellenE1 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1) '.Replace(",", "."))  + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE2 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2) '.Replace(",", ".")) + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE3 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3) '.Replace(",", "."))  + 1
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
    End Sub


    ''' <summary>
    ''' SaveNeeded wird vom Container ParentForm abgefeuert und gibt dem Usercontrol an das es zu speichern hat
    ''' Die Überladene Methode muss sich dann um die Speicherlogik kümmern, sofern das übergebende Usercontrol dem eigenem entspricht
    ''' </summary>
    ''' <param name="UserControl">Das Usercontrol welches zu Speichern hat</param>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor>Die Überladene Routine sollte überprüfen ob me.equals(Usercontrol) = true ist, um nicht unnötig oft alles zu speichern</commentauthor>
    Protected Overridable Sub SaveNeeded(ByVal UserControl As UserControl) Handles _ParentForm.SaveNeeded
    End Sub
    ''' <summary>
    ''' SaveWithoutValidationNeeded wird vom Container ParentForm abgefeuert und gibt dem Usercontrol an das es zu speichern hat, dabei muss nicht auf vollständigkeit geachtet werden. Z.b. beim Rückwärts blättern
    ''' Die Überladene Methode muss sich dann um die Speicherlogik kümmern, sofern das übergebende Usercontrol dem eigenem entspricht
    ''' </summary>
    ''' <param name="UserControl">Das Usercontrol welches zu Speichern hat</param>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor>Die Überladene Routine sollte überprüfen ob me.equals(Usercontrol) = true ist, um nicht unnötig oft alles zu speichern</commentauthor>
    Protected Overridable Sub SaveWithoutValidationNeeded(ByVal usercontrol As UserControl) Handles _ParentForm.SaveWithoutValidationNeeded
    End Sub


    ''' <summary>
    ''' Ermöglicht das aufrufen von UCO bezogenden Code zur lokalisierung
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub LokalisierungNeeded(ByVal UserControl As UserControl) Handles _ParentForm.LokalisierungNeeded
    End Sub

    ''' <summary>
    ''' ermöglicht das das UCO noch einmal die Daten aktualisiert (z.b. wenn in den Stammdaten die Art der waage geändert wurde, muss der Kompatiblitätsnachweis darauf reagieren können
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub UpdateNeeded(ByVal UserControl As UserControl) Handles _ParentForm.UpdateNeeded
    End Sub

    ''' <summary>
    ''' Entsperren des Eichprozesses durch RHEWA seitige Korrektur
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub EntsperrungNeeded() Handles _ParentForm.EntsperrungNeeded
    End Sub

    ''' <summary>
    ''' Zurücksenden an den Eichbevollmächtigten durch RHEWA
    ''' </summary>
    ''' <param name="TargetUserControl"></param>
    ''' <remarks></remarks>
    Protected Overridable Sub VersendenNeeded(ByVal TargetUserControl As UserControl) Handles _ParentForm.VersendenNeeded
    End Sub
#End Region

    Friend Function ShowValidationErrorBox() As Boolean
        If Me.AbortSaveing = True Then
            If Debugger.IsAttached Then
                If MessageBox.Show("Validierung überspringen?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Me.AbortSaveing = False
                    Return True
                Else
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.AbortSaveing = True
                    Return False
                End If
            Else
                MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.AbortSaveing = True
                Return False
            End If
        End If

        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True
    End Function

    Friend Sub DisableControls(ByVal controlcontainer As Control)
        For Each Control In controlcontainer.Controls
            If TypeOf Control Is Label Then Continue For
            If TypeOf Control Is PictureBox Then Continue For
            If TypeOf Control Is Telerik.WinControls.UI.RadLabel Then Continue For
            If TypeOf Control Is Telerik.WinControls.UI.RadSeparator Then Continue For

            If TypeOf Control Is GroupBox Then
                DisableControls(Control)
                Continue For
            End If
            If TypeOf Control Is Panel Then
                DisableControls(Control)
                Continue For
            End If
            If TypeOf Control Is Telerik.WinControls.UI.RadGroupBox Then
                DisableControls(Control)
                Continue For
            End If
            If TypeOf Control Is Telerik.WinControls.UI.RadButton Then
                Control.enabled = False
                Continue For
            End If
            If TypeOf Control Is Telerik.WinControls.UI.RadCheckBox Then
                Control.enabled = False
                Continue For
            End If

            If TypeOf Control Is Telerik.WinControls.UI.RadRadioButton Then
                Control.enabled = False
                Continue For
            End If

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
    End Sub

    Friend Function ShowValidationErrorBoxStandardwaage(enu As GlobaleEnumeratoren.enuEichprozessStatus) As Boolean
        Dim Displaytext As String = ""
        Select Case enu
            Case GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                Displaytext = "Ist die Wiederholbarkeit wirklich gegeben?"
            Case Is = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                Displaytext = "Werden die Daten wirklich nur bei Stillstand übermittelt?"
        End Select

        If Not Displaytext.Equals("") Then
            If MessageBox.Show(Displaytext, "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Me.AbortSaveing = False
                Return True
            Else
                Me.AbortSaveing = True
                MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Else
            Return True
        End If


    End Function




#Region "Hilfsfunktionen"
    ''' <summary>
    ''' Berechnet die Eichfehlergrenzen anhand der Last (wählt somit den EFG Bereich aus und des Eichwerts bei Mehrbereichswaagen entpsrechend dem Bereich)
    ''' </summary>
    ''' <param name="Gewicht"></param>
    ''' <param name="Bereich"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetEFG(ByVal Gewicht As Decimal, ByVal Bereich As Integer) As Decimal
        Try
            Dim Eichwert = objEichprozess.Kompatiblitaetsnachweis.GetType().GetProperty(String.Format("Kompatiblitaet_Waage_Eichwert{0}", Bereich)).GetValue(objEichprozess.Kompatiblitaetsnachweis, Nothing)
            Dim MaxLoad = objEichprozess.Kompatiblitaetsnachweis.GetType().GetProperty(String.Format("Kompatiblitaet_Waage_Hoechstlast{0}", Bereich)).GetValue(objEichprozess.Kompatiblitaetsnachweis, Nothing)

            'TH Die Formel wurde in Excel nach diesem Festen schema definiert. Allerdings sollen die EFGS nun nach den korrekten Gewichtsgrenzen definiert werden
            'If Gewicht > 2000 Then
            '    Return Math.Round(CDec(value * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            'ElseIf Gewicht > 500 Then
            '    Return Math.Round(CDec(value * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            'Else
            '    Return Math.Round(CDec(value * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            'End If

            'neue Formel mit vom Eichwert abhängigen Gewichtsgrenzen
            '2000 e - maxload oder 3000e
            If Gewicht > MaxLoad Then
                Return 0
            ElseIf Gewicht > Eichwert * 2000 And Gewicht <= MaxLoad Then
                Return Math.Round(CDec(Eichwert * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            ElseIf Gewicht > Eichwert * 500 And Gewicht <= Eichwert * 2000 Then
                Return Math.Round(CDec(Eichwert * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            ElseIf Gewicht >= Eichwert * 20 And Gewicht <= Eichwert * 500 Then
                Return Math.Round(CDec(Eichwert * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            Else
                Return 0
            End If
        Catch e As Exception
            Return -1
        End Try
    End Function


    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welchen Bereich es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetBereich(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Bereich As String = ""
            ControlName = CType(sender, Control).Name
            If ControlName.Contains("Bereich1") Then
                Bereich = 1
            ElseIf ControlName.Contains("Bereich2") Then
                Bereich = 2
            ElseIf ControlName.Contains("Bereich3") Then
                Bereich = 3
            End If
            Return Bereich
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welche Staffel es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetStaffel(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Staffel As String = ""
            ControlName = CType(sender, Control).Name
            If ControlName.Contains("Staffel1") Then
                Staffel = 1
            ElseIf ControlName.Contains("Staffel2") Then
                Staffel = 2
            ElseIf ControlName.Contains("Staffel3") Then
                Staffel = 3
            ElseIf ControlName.Contains("Staffel4") Then
                Staffel = 4
            ElseIf ControlName.Contains("Staffel5") Then
                Staffel = 5
            End If
            Return Staffel
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welchen Belastungsort es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetBelastungsort(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Ort As String
            ControlName = CType(sender, Control).Name
            If ControlName.EndsWith("Mitte") Then
                Ort = "Mitte"
            Else
                Ort = ControlName.Last
            End If
            Return Ort
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welchen pruefung (linearität) es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetPruefung(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Pruefung As String = ""
            ControlName = CType(sender, Control).Name

            If ControlName.Contains("Fallend") Then
                Pruefung = "Fallend"
            Else
                Pruefung = ""
            End If
            Return Pruefung
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welchen messpunkt es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetMesspunkt(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Messpunkt As String = ""

            ControlName = CType(sender, Control).Name
            Messpunkt = ControlName.Last
            Return Messpunkt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Erwartet z.b. ein Steuerelement, prüft den Namen und gibt zurück um welche wiederholung es sich handelt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetWiederholung(ByVal sender As Object) As String
        Try
            Dim ControlName As String
            Dim Messpunkt As String = ""

            ControlName = CType(sender, Control).Name
            Messpunkt = ControlName.Last
            Return Messpunkt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

End Class
