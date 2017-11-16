Public Class ucoZwischenwerte
    Inherits ucoContent
    Implements IRhewaEditingDialog


#Region "Member Variables"

    Private _suspendEvents As Boolean = False  'Variable zum temporären stoppen der Eventlogiken
    Private _parentForm As FrmMainContainer
    Private _objEichprozess As Eichprozess
    Private StaffelNr As String
    Private BereichNr As String
    Private _Ersatzglast As Decimal

    Private EFG_20 As Decimal
    Private EFG_500 As Decimal
    Private EFG_2000 As Decimal
    Private EFG_max As Decimal

    Private EFG_20_Absolut As Decimal
    Private EFG_500_Absolut As Decimal
    Private EFG_Max_Absolut As Decimal

#End Region

#Region "Constructors"

    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub

    'Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
    '    MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
    '    _parentForm = pParentform
    '    _suspendEvents = True
    '    InitializeComponent()
    '    _suspendEvents = False

    'End Sub

    ''' <summary>
    ''' bitwork - Dennis Ostroga - Neuer Konstruktor für die Zwischenwerte
    ''' </summary>
    ''' <param name="pParentform"></param>
    ''' <param name="pObjEichprozess"></param>
    ''' <param name="pStaffel"></param>
    ''' <param name="pBereich"></param>
    ''' <param name="pPreviousUco"></param>
    ''' <param name="pNextUco"></param>
    ''' <param name="pEnuModus"></param>
    Sub New(ByRef pParentform As FrmMainContainer, pObjEichprozess As Eichprozess, pStaffel As String, pBereich As String, pErsatzgewicht As Decimal, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        _parentForm = pParentform
        _objEichprozess = pObjEichprozess
        _Ersatzglast = pErsatzgewicht

        _suspendEvents = True
        Me.StaffelNr = pStaffel
        Me.BereichNr = pBereich
        InitializeComponent()
        _suspendEvents = False

    End Sub

#End Region

#Region "Events"
    Private Sub ucoEichfehlergrenzen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'daten füllen
            LoadFromDatabase()
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Methods"

    'Private Sub GetBereich3()
    '    Try
    '        'Bereiche berechnen
    '        EFG_20 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_500 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_2000 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_max = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        'EFG Berechnen

    '        EFG_20_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_500_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_Max_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)


    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub GetBereich2()
    '    Try
    '        'Bereiche berechnen
    '        EFG_20 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_500 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_2000 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_max = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        'EFG Berechnen

    '        EFG_20_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_500_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_Max_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)


    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub GetBereich1()
    '    Try
    '        'Bereiche berechnen
    '        EFG_20 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_500 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_2000 = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        EFG_max = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1), _intNullstellenE, MidpointRounding.AwayFromZero)

    '        'EFG Berechnen

    '        EFG_20_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_500_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
    '        EFG_Max_Absolut = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)


    '    Catch ex As Exception
    '    End Try
    'End Sub




    Private Sub SetDataToFormFields()

        RadLabelStaffelNr.Text = Me.StaffelNr.ToString
        RadLabelBereichNr.Text = Me.BereichNr.ToString

        RadTextBoxZwischenwert1Ersatzlast.Text = _Ersatzglast
        RadTextBoxZwischenwert2Ersatzlast.Text = _Ersatzglast





    End Sub

#End Region

#Region "Interface Methods"

    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        Return True
    End Function
    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase

        'events abbrechen
        _suspendEvents = True
        Using context As New Entities
            'neu laden des Objekts, diesmal mit den lookup Objekten
            objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            If objEichprozess Is Nothing Then _parentForm.Close()

        End Using
        'steuerelemente mit werten aus DB füllen
        FillControls()
        'events abbrechen
        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
        'dynamisches Laden der Felder
        SetDataToFormFields()

        'dynamisches laden der Nullstellen:

        HoleNullstellen()

        'EFG Kategorie füllen

        GetZwischenwert1()
        GetZwischenwert2()

        'Select Case BereichNr
        '    Case 1
        '        GetBereich1()
        '    Case 2
        '        GetBereich2()
        '    Case 3
        '        GetBereich3()
        'End Select


    End Sub


    Private Sub GetZwischenwert1()
        Try
            Dim Gewicht As Decimal = _Ersatzglast + CType(RadTextBoxZwischenwert1Normallast.Text, Decimal)
            RadTextBoxZwischenwert1EFG.Text = GetEFG(Gewicht, BereichNr)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub GetZwischenwert2()
        Try
            Dim Gewicht As Decimal = _Ersatzglast + CType(RadTextBoxZwischenwert2Normallast.Text, Decimal)
            RadTextBoxZwischenwert2EFG.Text = GetEFG(Gewicht, BereichNr)
        Catch ex As Exception

        End Try
    End Sub


    Protected Friend Overrides Sub Lokalisiere()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoZwischenwerte))
        Lokalisierung(Me, resources)
    End Sub

    Private Sub RadTextBoxZwischenwert1Normallast_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxZwischenwert1Normallast.TextChanged
        GetZwischenwert1()
    End Sub

    Private Sub RadTextBoxZwischenwert2Normallast_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxZwischenwert2Normallast.TextChanged
        GetZwischenwert2()
    End Sub











#End Region

End Class