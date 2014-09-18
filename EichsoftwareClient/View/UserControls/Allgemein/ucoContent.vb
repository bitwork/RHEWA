Imports System.ComponentModel
Public Class ucoContent
    Implements INotifyPropertyChanged


#Region "Member Variables"
    Private WithEvents _ParentForm As FrmMainContainer
    Private _PreviousUco As ucoContent
    Private _NextUco As ucoContent
    'Private _Breadcrumb As ucoStatusBullet
    Private _objEichprozess As Eichprozess

    Private _bolSuspendRefresh As Boolean = False

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



    'Protected ReadOnly Property ListUeberspringeStatus As List(Of GlobaleEnumeratoren.enuEichprozessStatus)
    '    Get
    '        Return objEichprozess.GetListeUngueltigeStati
    '    End Get
    'End Property


    Protected Friend Property objEichprozess As Eichprozess
        Get
            Return _objEichprozess
        End Get
        Set(value As Eichprozess)
            _objEichprozess = value
        End Set
    End Property

    Protected Friend Property ParentFormular As FrmMainContainer
        Get
            Return _ParentForm
        End Get
        Set(value As FrmMainContainer)
            _ParentForm = value
        End Set
    End Property

    Protected Friend Property PreviousUco As ucoContent
        Get
            Return _PreviousUco
        End Get
        Set(value As ucoContent)
            _PreviousUco = value
        End Set
    End Property

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
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

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


    Protected Overridable Sub EntsperrungNeeded() Handles _ParentForm.EntsperrungNeeded

    End Sub


    Protected Overridable Sub VersendenNeeded(ByVal TargetUserControl As UserControl) Handles _ParentForm.VersendenNeeded

    End Sub


#End Region


End Class
