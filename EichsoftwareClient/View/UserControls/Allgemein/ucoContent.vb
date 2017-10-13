Imports System.ComponentModel
Imports EichsoftwareClient
''' <summary>
''' Das UcoContent bietet die Basisklasse für alle im Eichprozess genutzten Ucos. Es enthält allgemein gültige Methoden und Eigenschaften
''' </summary>
''' <remarks></remarks>
Public Class ucoContent
    Implements INotifyPropertyChanged
    Implements IRhewaEditingDialog


#Region "Member Variables"
    Private WithEvents _ParentForm As FrmMainContainer 'Bezug zum Parentcontrol.
    Private _PreviousUco As ucoContent 'mit dieser variable merkt sich das aktuelle UCO, welches Uco VOR ihm im Eichprozess dran kam
    Private _NextUco As ucoContent 'mit dieser variable merkt sich das aktuelle UCO, welches Uco NACH ihm im Eichprozess dran kam
    Private _objEichprozess As Eichprozess 'das aktuelle Eichprozess Objekt
    Private _bolEichprozessIsDirty = False

    Protected Friend _intNullstellenE1 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE2 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE3 As Integer = 0 'Variable zum Einstellen der Nullstellen für das Casten und runden der Werte. Abhängig von e Wert. Wenn e = 1 Nullstelle dann hier = 2. wenn e = 2 dann hier = 3. immer eine nullstelle mehr als E
    Protected Friend _intNullstellenE As Integer = 0

    Protected Friend _bolValidierungsmodus As Boolean = False 'wenn dieser Wert auf True steht, dürfen validierungen nicht mehr übersprungen werden
    Protected Friend calculationCulture As Globalization.CultureInfo = New Globalization.CultureInfo("de-DE")

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
            If _bolEichprozessIsDirty <> value Then
                _bolEichprozessIsDirty = value
                onpropertychanged("AktuellerStatusDirty")
            End If
        End Set
    End Property

#Region "Property Changed Event für Dirty Flag, damit Ampel UCO darauf reagieren kann"
    ' Declare the event
    Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub onpropertychanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region

#Region "Enumeratoren"
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
    Protected Friend Property AbortSaving As Boolean
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

    Protected Friend ReadOnly Property Version As String
        Get
            If Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
                Dim myVersion = Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
                Return myVersion.ToString(3)
            End If
            Return Application.ProductVersion.ToString
        End Get
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
        ' zuweisen des Eltern elements
        _ParentForm = pParentform
        ' zuweisen des vorherigen Steuerelements
        _PreviousUco = pPreviousUco
        ' zuweisen des nachfolgenden Steuerelements, wenn es schon existiert
        _NextUco = pNextUco
        _objEichprozess = pObjEichprozess
        ' zuweisen des Zugriffsmoduses auf den Dialog
        DialogModus = pEnuModus

        If Not _ParentForm Is Nothing Then
            _ParentForm.SETContextHelpText("")
        End If
    End Sub

#End Region

#Region "Globale Events" 'als UCO kann diese Klasse nicht als Mustinherited deklariert werden, somit klappt technisch kein Mustoverride. Wurde durch die Interface implementiert
    Private Sub ShowEFG(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyData.Equals("F1") Then
            ShowEichfehlergrenzenDialog()
        End If
    End Sub

    ''' <summary>
    ''' Validations the needed.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function ValidationNeeded() As Boolean Implements IRhewaEditingDialog.ValidationNeeded
        LoadFromDatabase()
        Return ValidateControls()
    End Function



    ''' <summary>
    ''' SaveNeeded wird vom Container ParentForm abgefeuert und gibt dem Usercontrol an das es zu speichern hat
    ''' Die Überladene Methode muss sich dann um die Speicherlogik kümmern, sofern das übergebende Usercontrol dem eigenem entspricht
    ''' </summary>
    ''' <param name="UserControl">Das Usercontrol welches zu Speichern hat</param>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor>Die Überladene Routine sollte überprüfen ob me.equals(Usercontrol) = true ist, um nicht unnötig oft alles zu speichern</commentauthor>
    Protected Sub SaveNeeded(ByVal UserControl As UserControl) Handles _ParentForm.SaveNeeded
        If Me.Equals(UserControl) Then

            If CheckDialogModus() = False Then
                Exit Sub
            End If

            If ValidateControls() Then
                SaveObjekt()
                AktualisiereStatus()
                ParentFormular.CurrentEichprozess = objEichprozess
            End If
        End If
    End Sub

    ''' <summary>
    ''' SaveWithoutValidationNeeded wird vom Container ParentForm abgefeuert und gibt dem Usercontrol an das es zu speichern hat, dabei muss nicht auf vollständigkeit geachtet werden. Z.b. beim Rückwärts blättern
    ''' Die Überladene Methode muss sich dann um die Speicherlogik kümmern, sofern das übergebende Usercontrol dem eigenem entspricht
    ''' </summary>
    ''' <param name="UserControl">Das Usercontrol welches zu Speichern hat</param>
    ''' <remarks></remarks>
    ''' <author>TH</author>
    ''' <commentauthor>Die Überladene Routine sollte überprüfen ob me.equals(Usercontrol) = true ist, um nicht unnötig oft alles zu speichern</commentauthor>
    Protected Sub SaveWithoutValidationNeeded(ByVal usercontrol As UserControl) Handles _ParentForm.SaveWithoutValidationNeeded
        If Me.Equals(usercontrol) Then
            If DialogModus = enuDialogModus.lesend Then
                UpdateObjekt()
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            'SaveWithoutValidation()
            SaveObjekt()
            ParentFormular.CurrentEichprozess = objEichprozess

        End If
    End Sub

    Protected Friend Overridable Sub AktualisiereStatus() Implements IRhewaEditingDialog.AktualisiereStatus
    End Sub



    ''' <summary>
    ''' Ermöglicht das aufrufen von UCO bezogenden Code zur lokalisierung
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Sub LokalisierungNeeded(ByVal UserControl As UserControl) Handles _ParentForm.LokalisierungNeeded
        If Me.Name.Equals(UserControl.Name) = False Then Exit Sub
        Lokalisiere()
        SetzeUeberschrift()
    End Sub

    ''' <summary>
    ''' ermöglicht das das UCO noch einmal die Daten aktualisiert (z.b. wenn in den Stammdaten die Art der waage geändert wurde, muss der Kompatiblitätsnachweis darauf reagieren können
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Sub UpdateNeeded(ByVal UserControl As UserControl) Handles _ParentForm.UpdateNeeded
        If Me.Equals(UserControl) Then
            Me.LokalisierungNeeded(UserControl)

            LoadFromDatabase()
        End If
    End Sub

    ''' <summary>
    ''' Entsperren des Eichprozesses durch RHEWA seitige Korrektur
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub EntsperrungNeeded() Handles _ParentForm.EntsperrungNeeded
        Entsperrung()
    End Sub

    ''' <summary>
    ''' Zurücksenden an den Eichbevollmächtigten durch RHEWA
    ''' </summary>
    ''' <param name="TargetUserControl"></param>
    ''' <remarks></remarks>
    Protected Sub VersendenNeeded(ByVal TargetUserControl As UserControl) Handles _ParentForm.VersendenNeeded
        If Me.Equals(TargetUserControl) Then
            Versenden()

        End If
    End Sub

#End Region

#Region "Interface Must Overrides"
    Protected Friend Overridable Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase
    End Sub

    Protected Friend Overridable Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
    End Sub
    Protected Friend Overridable Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        Return True
    End Function
    Protected Friend Overridable Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt
    End Sub

    'Protected Friend Overridable Sub SaveWithoutValidation() Implements IRhewaEditingDialog.SaveWithoutValidation
    'End Sub

    Protected Friend Overridable Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
    End Sub

    Protected Friend Overridable Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
    End Sub

    Protected Friend Overridable Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
    End Sub

    Protected Friend Overridable Sub Versenden() Implements IRhewaEditingDialog.Versenden
    End Sub

    Protected Friend Overridable Sub FillControls() Implements IRhewaEditingDialog.FillControls
    End Sub

    Protected Friend Overridable Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        Return Nothing
    End Function

    Protected Friend Overridable Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
    End Sub


#End Region



#Region "Hilfsfunktionen"

    ''' <summary>
    ''' Lokalisierte "Long Date" Strings in DateTime Objekt umwandeln
    ''' </summary>
    ''' <param name="datestring"></param>
    ''' <returns></returns>
    Protected Friend Function tryParseDateToLocal(datestring As String) As Date
        Dim resultDate As Date
        Dim cultureDE As IFormatProvider = New System.Globalization.CultureInfo("de", True)
        Date.TryParse(datestring, cultureDE, System.Globalization.DateTimeStyles.AssumeLocal, resultDate)

        If IsDate(resultDate) And resultDate <> Date.MinValue Then
            Return resultDate
        End If

        Dim culturePL As IFormatProvider = New System.Globalization.CultureInfo("pl", True)
        Date.TryParse(datestring, culturePL, System.Globalization.DateTimeStyles.AssumeLocal, resultDate)
        If IsDate(resultDate) And resultDate <> Date.MinValue Then
            Return resultDate
        End If

        Dim cultureEN As IFormatProvider = New System.Globalization.CultureInfo("en", True)
        Date.TryParse(datestring, cultureEN, System.Globalization.DateTimeStyles.AssumeLocal, resultDate)
        If IsDate(resultDate) And resultDate <> Date.MinValue Then
            Return resultDate
        End If
        MessageBox.Show("Das Datum " & datestring & " konnte nicht konvertiert werden")
        Return New Date
    End Function

    ''' <summary>
    ''' holt Steuerelement Objekt anhand von Namen als String
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThroughAttribute()>
    Protected Function FindControl(ByVal Name As String) As Control
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
    Protected Sub HoleNullstellen()
        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:
        Try
            _intNullstellenE1 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1)
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE2 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2)
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE3 = GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3)
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
    ''' Gibt Anzahl der Bereiche nach Waagenart zurück
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function GetEichwertBereich() As Integer
        Dim eichwertBereich As Integer
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                EichwertBereich = 1
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                EichwertBereich = 2
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                EichwertBereich = 3
        End Select

        Return EichwertBereich
    End Function

    Protected Friend Sub getEFGDifferenz(ByRef Fehler As Telerik.WinControls.UI.RadTextBox, ByRef EFG As Telerik.WinControls.UI.RadCheckBox, Spezial As Telerik.WinControls.UI.RadMaskedEditBox, ByRef min As Decimal, ByRef max As Decimal, AnzeigeMax1 As Telerik.WinControls.UI.RadTextBox, AnzeigeMax2 As Telerik.WinControls.UI.RadTextBox, AnzeigeMax3 As Telerik.WinControls.UI.RadTextBox)
        Dim listdecimals As New List(Of Decimal)
        If IsNumeric(AnzeigeMax1.Text) Then
            listdecimals.Add(AnzeigeMax1.Text)
        End If
        If IsNumeric(AnzeigeMax2.Text) Then
            listdecimals.Add(AnzeigeMax2.Text)
        End If
        If IsNumeric(AnzeigeMax3.Text) Then
            listdecimals.Add(AnzeigeMax3.Text)
        End If

        If listdecimals.Count = 3 Then
            max = listdecimals.Max
            min = listdecimals.Min

            Dim differenz As Decimal = max - min
            Fehler.Text = differenz
            Try

                If differenz <= CDec(Spezial.Text) And differenz >= -CDec(Spezial.Text) Then
                    EFG.Checked = True
                Else
                    EFG.Checked = False
                End If
            Catch ex As Exception
            End Try

        Else
            EFG.Checked = False
            Fehler.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Basis lokalisierung
    ''' </summary>
    ''' <param name="container"></param>
    ''' <param name="ressourcemanager"></param>
    Protected Sub Lokalisierung(ByVal container As Control, ByVal ressourcemanager As System.ComponentModel.ComponentResourceManager)
        ressourcemanager.ApplyResources(container, container.Name, New Globalization.CultureInfo(AktuellerBenutzer.Instance.AktuelleSprache))

        For Each c As Control In container.Controls
            For Each childControl In c.Controls
                Lokalisierung(childControl, ressourcemanager)
            Next
            ressourcemanager.ApplyResources(c, c.Name, New Globalization.CultureInfo(AktuellerBenutzer.Instance.AktuelleSprache))
        Next c
    End Sub

    ''' <summary>
    ''' Bietet die Option an validierungen zu überspringen wenn AbortSaving auf true steht.
    ''' </summary>
    ''' <param name="pDisplayMessage">Hinweistext der in der Messagebox auftauchen soll</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ShowValidationErrorBox(ByVal DebugOnly As Boolean, Optional ByVal pDisplayMessage As String = "") As DialogResult
        Dim DisplayMessage As String = pDisplayMessage
        If DisplayMessage.Equals("") Then DisplayMessage = My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen

        If Me.AbortSaving = True Then
            If _bolValidierungsmodus Then
                MessageBox.Show(DisplayMessage, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.AbortSaving = True
                Return False
            End If

            If DebugOnly = False Then
                Dim result As DialogResult
                If _objEichprozess.AusStandardwaageErzeugt Or AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then
                    result = MessageBox.Show(String.Format("Standardwaage {0}{0} - Klicken Sie ""Ignorieren"" um die Validierung bewusst zu überspringen {0}{0} Klicken Sie ""Wiederholen"" um die Soll-Werte mit den Ist-Werten gleichzusetzen  {0}{0} Klicken Sie ""Abbrechen"" um nichts zu ändern ", vbNewLine), "", MessageBoxButtons.AbortRetryIgnore)
                Else
                    result = MessageBox.Show(My.Resources.GlobaleLokalisierung.ValidierungUeberspringen, "", MessageBoxButtons.YesNo)
                End If

                If result = DialogResult.Ignore Or result = DialogResult.Yes Then 'Ignore = Validierung bewusst überspringen
                    Me.AbortSaving = False
                    Return result
                ElseIf result = DialogResult.Retry Then 'Werte automatisch einpflegen
                    Me.AbortSaving = False
                    Return result
                Else 'Validierung als Falsch akzeptieren
                    MessageBox.Show(DisplayMessage, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.AbortSaving = True
                    Return result
                End If
            Else
                If Debugger.IsAttached Then
                    Dim result As DialogResult

                    result = MessageBox.Show(String.Format("DEBUG ONLY Standardwaage {0}{0} - Klicken Sie ""Ignorieren"" um die Validierung bewusst zu überspringen {0}{0} Klicken Sie ""Wiederholen"" um die Soll-Werte mit den Ist-Werten gleichzusetzen  {0}{0} Klicken Sie ""Abbrechen"" um nichts zu ändern ", vbNewLine), "", MessageBoxButtons.AbortRetryIgnore)

                    If result = DialogResult.Ignore Or result = DialogResult.Yes Then 'Ignore = Validierung bewusst überspringen
                        Me.AbortSaving = False
                        Return result

                    ElseIf result = DialogResult.Retry Then 'Werte automatisch einpflegen
                        Me.AbortSaving = False
                        Return result

                    Else 'Validierung als Falsch akzeptieren
                        MessageBox.Show(DisplayMessage, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.AbortSaving = True
                        Return result

                    End If
                Else
                    Dim result As DialogResult = MessageBox.Show(DisplayMessage, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.AbortSaving = True
                    Return result

                End If
            End If
        End If

        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaving = False
        Return DialogResult.Yes
    End Function
    ''' <summary>
    ''' Hiearschisches deaktivieren der Steuerelemente
    ''' </summary>
    ''' <param name="controlcontainer"></param>
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
            If TypeOf Control Is Panel Or TypeOf Control Is Telerik.WinControls.UI.RadPanel Then
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

    ''' <summary>
    '''   Hiearschisches aktivieren der Steuerelemente
    ''' </summary>
    ''' <param name="controlcontainer"></param>
    Friend Sub EnableControls(ByVal controlcontainer As Control)
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
                Control.enabled = True
                Continue For
            End If
            If TypeOf Control Is Telerik.WinControls.UI.RadCheckBox Then
                Control.enabled = True
                Continue For
            End If

            If TypeOf Control Is Telerik.WinControls.UI.RadRadioButton Then
                Control.enabled = True
                Continue For
            End If

            Try
                Control.readonly = False
            Catch ex As Exception
                Try
                    Control.isreadonly = False
                Catch ex2 As Exception
                    Try
                        Control.enabled = True
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
                Me.AbortSaving = False
                Return True
            Else
                Me.AbortSaving = True
                MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Else
            Return True
        End If

    End Function
    ''' <summary>
    ''' Validierung ob in ein Text oder eine Zahl eingetragen wurde in Textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Friend Sub BasicTextboxValidation(sender As Object, e As System.ComponentModel.CancelEventArgs)
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
    End Sub

    ''' <summary>
    ''' validierung ob eine Zahl eingegeben wurde
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Friend Sub BasicTextboxNumberValidation(sender As Object, e As System.ComponentModel.CancelEventArgs)
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
                'prüfen ob negative zahlen eingegeben wurden
                If sender.text.ToString.Trim.StartsWith("-") Then
                    e.Cancel = True
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                    System.Media.SystemSounds.Exclamation.Play()
                Else
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Methode zum ermitteln der anzahl der benötigten Nullstellen nach dem RHEWA System
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRHEWADecimalDigits(ByVal value As String) As Integer
        Try
            If value Is Nothing Then
                Return 0
            End If
            If value = "" Then
                Return 0
            End If
            If CDec(value) >= 1 Then
                Return 1
            ElseIf CDec(value) >= 0.1 Then
                Return 3
            ElseIf CDec(value) >= 0.01 Then
                Return 3
            ElseIf CDec(value) >= 0.001 Then
                Return 4
            ElseIf CDec(value) >= 0.0001 Then
                Return 5
            Else
                Return 6
            End If

        Catch ex As Exception
            Return 0
        End Try
    End Function

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
    Friend Sub ShowEichfehlergrenzenDialog()
        If Not objEichprozess Is Nothing Then
            Dim f As New frmEichfehlergrenzen(objEichprozess)
            f.Show()
        End If
    End Sub
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
    ''' Gibt die anzahl der Bereiche zurück je nach Waagenart
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function GetAnzahlBereiche() As Integer
        Dim intBereiche As Integer = 0
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            intBereiche = 1
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            intBereiche = 2
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            intBereiche = 3
        End If

        Return intBereiche
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
                'sonderfall 10, 11, 12
                If ControlName.EndsWith("10") Then Ort = "10"
                If ControlName.EndsWith("11") Then Ort = "11"
                If ControlName.EndsWith("12") Then Ort = "12"
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


    ''' <summary>
    ''' Abbruch der Validierung durch Admin Rechte oder bewusstes ignorieren
    ''' </summary>
    ''' <param name="result"></param>
    ''' <returns></returns>
    Protected Function ProcessResult(result As DialogResult) As Boolean
        If result = DialogResult.Yes Or result = DialogResult.Ignore Then
            Return True
        ElseIf result = DialogResult.Retry Then
            OverwriteIstSoll()
            Return ValidateControls()
        Else
            Return False
        End If
    End Function

    Protected Sub CloneAndSendServerObjekt()
        Using dbcontext As New Entities
            Dim result = MessageBox.Show("Wählen Sie ""Ja"" um den Datensatz zu speichern und genehmigen, wählen Sie ""Nein"" um ihn zu speichern und abzulehnen oder abbrechen um den Datensatz weiter zu bearbeiten", "Frage", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Yes Then

                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Genehmigt
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss

            ElseIf result = DialogResult.No Then

                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Fehlerhaft
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss
            Else
                Exit Sub

            End If


            Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
            objServerEichprozess = clsClientServerConversionFunctions.CopyServerObjectProperties(objServerEichprozess, objEichprozess, clsClientServerConversionFunctions.enuModus.RHEWASendetAnClient)
            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                Dim objLiz = (From db In dbcontext.Lizensierung Where db.Lizenzschluessel = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And db.HEKennung = AktuellerBenutzer.Instance.Lizenz.HEKennung).FirstOrDefault

                Try
                    'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                    Webcontext.AddEichprozess(objLiz.HEKennung, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, Version)

                    'schließen des dialoges
                    ParentFormular.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Status zurück setzen
                    Exit Sub
                End Try

                'datensatz entsperren
                clsWebserviceFunctions.SetGesperrt(False, objEichprozess.Vorgangsnummer)
            End Using
        End Using
    End Sub

    <DebuggerStepThrough>
    Protected Friend Function GetDecimal(value As String) As Decimal
        Try
            Dim returnvalue = Double.Parse(value, calculationCulture.NumberFormat)
            Return returnvalue
        Catch ex As Exception
            Return -9999
        End Try
    End Function


#End Region

End Class