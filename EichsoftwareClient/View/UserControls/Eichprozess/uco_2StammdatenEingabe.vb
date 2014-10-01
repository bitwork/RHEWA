Public Class uco_2StammdatenEingabe
    Inherits ucoContent



#Region "Member Variables"
    Private _DatasourceDropdownListWaagentyp As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWaagenArt As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListAWG As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWZ As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWZHersteller As IEnumerable 'datenquelle für dropdown
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)

    '  Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Stammdateneingabe zurückgesetzt

    Private objDBFunctions As New clsDBFunctions 'Klasse mit Hilfsfunktionen zum arbeiten mit der lokalen SQL Compact DB
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe

    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' lade routine des ucos. Hilfetext und überschrift text setzen. ausserdem dropdownlisten füllen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Stammdaten

            Catch ex As Exception
            End Try
        End If

        'daten füllen
        LoadFromDatabase()

        'fokus setzen
        '        RadTextBoxStammdatenWaagenbaufirma.Focus()
        RadTextBoxWaageSeriennummer.Focus()
    End Sub


    'wenn die Art der Waage oder die WZ gewechselt wurde, muss kontrolliert werden, ob der Eichvorgang bereits fortschritten ist. Wenn also bereits ein späterer Status erreicht wurde, muss dieser zurückgesetzt werden auf die Stammdateneingabe
    Private Sub RadDropDownListWaagenArt_SelectedIndexChanged(sender As Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles RadDropDownListWaagenArt.SelectedIndexChanged, RadDropdownlistWaagenTyp.SelectedIndexChanged
        If _suspendEvents Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    ''' <summary>
    ''' Dirty flag 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxStammdatenWaagenbaufirma_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxWaageSeriennummer.TextChanged, RadTextBoxStammdatenWaagenbaufirma.TextChanged, RadTextBoxStammdatenStrasse.TextChanged, RadTextBoxStammdatenPLZ.TextChanged, RadTextBoxStammdatenOrt.TextChanged
        If _suspendEvents Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    Private Sub RadButtonNeueWaegezelle_Enter(sender As Object, e As EventArgs) Handles RadButtonNeueWaegezelle.Enter
        'Hilfetext setzen
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_StammdatenNeueWZ)
    End Sub

    Private Sub RadGroupBoxWZ_MouseEnter(sender As Object, e As EventArgs) Handles RadGroupBoxWZ.MouseEnter
        'Hilfetext setzen
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
    End Sub


    ''' <summary>
    ''' Füllt Textboxen anhand des gewählten AWGs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadDropdownlistAWGTyp_SelectedIndexChanged(sender As System.Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles RadDropdownlistAWGTyp.SelectedIndexChanged
        Try
            If _suspendEvents Then Exit Sub
            AktuellerStatusDirty = True
            If Not RadDropdownlistAWGTyp.Text.Equals(RadDropdownlistAWGTyp.NullText) Then
                'auslesen der Daten des AWGs anhand der ausgewählten ID
                Dim objAWG As Lookup_Auswertegeraet = (From AWG In _DatasourceDropdownListAWG Select AWG Where AWG.ID = RadDropdownlistAWGTyp.SelectedValue).FirstOrDefault
                If Not objAWG Is Nothing Then
                    RadTextBoxAWGHersteller.Text = objAWG.Hersteller
                    RadTextBoxAWGBauartzulassung.Text = objAWG.Bauartzulassung
                    RadTextBoxAWGPruefbericht.Text = objAWG.Pruefbericht
                    RadTextBoxWaageZulassungsinhaber.Text = objAWG.Hersteller
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Filtert Typen anhand des Herstellers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadDropdownlistlWZHersteller_SelectedIndexChanged(sender As System.Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles RadDropdownlistlWZHersteller.SelectedIndexChanged
        Try
            If _suspendEvents Then Exit Sub
            AktuellerStatusDirty = True
            If Not RadDropdownlistlWZHersteller.Text.Equals(RadDropdownlistlWZHersteller.NullText) Then
                'filtern des Typs durch aufrufen einer delagts funktion

                'weitere events unterbrechen
                _suspendEvents = True
                RadDropdownlistlWZTyp.Filter = Nothing 'filter leeren
                RadDropdownlistlWZTyp.Filter = New Predicate(Of Telerik.WinControls.UI.RadListDataItem)(AddressOf FilterItem)
                'zurücksetzten der anderen Werte
                RadDropdownlistlWZTyp.Text = RadDropdownlistlWZTyp.NullText 'zurücksetzen der auswahl
                RadTextBoxWZBauartzulassung.Text = ""
                RadTextBoxWZPruefbericht.Text = ""
                _suspendEvents = False

            Else 'entfernen des Typen Filters
                RadDropdownlistlWZTyp.Filter = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Füllt Textboxen anhand der gewählten WZ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadDropdownlistlWZTyp_SelectedIndexChanged(sender As System.Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles RadDropdownlistlWZTyp.SelectedIndexChanged
        Try
            If _suspendEvents Then Exit Sub
            If Not RadDropdownlistlWZTyp.Text.Equals(RadDropdownlistlWZTyp.NullText) Then
                'auslesen der Daten des AWGs anhand der ausgewählten ID
                Dim objWZ As Lookup_Waegezelle = (From WZ In _DatasourceDropdownListWZ Where WZ.ID = RadDropdownlistlWZTyp.SelectedValue).FirstOrDefault
                If Not objWZ Is Nothing Then

                    'events unterbrechen
                    _suspendEvents = True
                    RadDropdownlistlWZHersteller.Text = objWZ.Hersteller
                    RadTextBoxWZBauartzulassung.Text = objWZ.Bauartzulassung
                    RadTextBoxWZPruefbericht.Text = objWZ.Pruefbericht
                    _suspendEvents = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        '  UpdateObject()
        If Me.Equals(UserControl) = False Then Exit Sub


        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_2StammdatenEingabe))
        Me.RadGroupBoxStammdaten.Text = resources.GetString("RadGroupBoxStammdaten.Text")
        Me.lblOrt.Text = resources.GetString("lblOrt.Text")
        Me.lblPLZ.Text = resources.GetString("lblPLZ.Text")
        Me.lblStrasse.Text = resources.GetString("lblStrasse.Text")
        Me.lblWaagenbaufirma.Text = resources.GetString("lblWaagenbaufirma.Text")
        Me.RadGroupBoxWZ.Text = resources.GetString("RadGroupBoxWZ.Text")

        Me.lblAWZTyp.Text = resources.GetString("lblAWZTyp.Text")
        Try
            Me.lblAWZZulassung2.Text = resources.GetString("lblAWZZulassung2.Text")
        Catch ex As Exception
        End Try
        Me.lblAWZZulassung.Text = resources.GetString("lblAWZZulassung.Text")
        Me.lblAWZHersteller.Text = resources.GetString("lblAWZHersteller.Text")
        Me.RadGroupBoxWaage.Text = resources.GetString("RadGroupBoxWaage.Text")
        Me.RadLabel16.Text = resources.GetString("RadLabel16.Text")
        Me.lblAWaagenTyp.Text = resources.GetString("lblAWaagenTyp.Text")
        Me.lblAWaageSeriennummer.Text = resources.GetString("lblAWaageSeriennummer.Text")
        Me.RadGroupBoxAWG.Text = resources.GetString("RadGroupBoxAWG.Text")
        Me.lblAWGTyp.Text = resources.GetString("lblAWGTyp.Text")
        Me.lblAWGBauart2.Text = resources.GetString("lblAWGBauart2.Text")
        Me.lblAWGBauart.Text = resources.GetString("lblAWGBauart.Text")
        Me.lblAWGHersteller.Text = resources.GetString("lblAWGHersteller.Text")
        Me.lblWaagenart.Text = resources.GetString("lblWaagenart.Text")



        'je nach sprache andere werte aus DB abrufen für Waagentyp
        Select Case My.Settings.AktuelleSprache.ToLower
            Case Is = "en"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_EN"
                RadDropDownListWaagenArt.DisplayMember = "Art_EN"
            Case Is = "de"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ"
                RadDropDownListWaagenArt.DisplayMember = "Art"
            Case Is = "pl"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_PL"
                RadDropDownListWaagenArt.DisplayMember = "Art_PL"
            Case Else
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_EN"
                RadDropDownListWaagenArt.DisplayMember = "Art_EN"

        End Select
        'Hilfetext setzen
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
        'Überschrift setzen
        ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Stammdaten


    End Sub

    ''' <summary>
    ''' öffnen des dialoges zum speichern einer neuen Waegezelle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonNeueWaegezelle_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonNeueWaegezelle.Click
        'öffnen des dialoges zum speichern einer neuen Waegezelle
        Dim f As New FrmNeueWaegezele

        'wenn eine neue angelegt wurde, dropdownliste aktualisieren und das neue element auswählen
        If f.ShowDialog = DialogResult.OK Then
            Using Context As New EichsoftwareClientdatabaseEntities1
                'weitere events unterbrechen

                Dim objWZ As Lookup_Waegezelle = f.NeueWaegezelle
                AktuellerStatusDirty = True

                Context.SaveChanges()

                'objEichprozess.Lookup_Waegezelle = objWZ
                objEichprozess.FK_Waegezelle = objWZ.ID

                _suspendEvents = True
                'Laden der WZ Dropdownliste (alles)
                Dim db = From dbLookup In Context.Lookup_Waegezelle Select dbLookup
                _DatasourceDropdownListWZ = db.ToList
                'Laden der WZ Hersteller Dropdownliste (nur hersteller namen gruppiert)
                Dim db1 = From dblookup In Context.Lookup_Waegezelle Group By Hersteller = dblookup.Hersteller Into HerstellerGruppe = Group Select New With {.Hersteller = Hersteller} ' Into Group Select New Lookup_Waegezelle
                _DatasourceDropdownListWZHersteller = db1.ToList

                'neu zuweisen der Datenquellen
                RadDropdownlistlWZTyp.DataSource = Nothing
                RadDropdownlistlWZHersteller.DataSource = Nothing
                RadDropdownlistlWZTyp.DataSource = _DatasourceDropdownListWZ
                RadDropdownlistlWZHersteller.DataSource = _DatasourceDropdownListWZHersteller

                'Name der Propertie setzen, welche als Anzeigename verwendet wird
                RadDropdownlistlWZTyp.ValueMember = "ID"

                'RadDropdownlistlWZHersteller.ValueMember = "ID"

                'Name der Propertie setzen, welche als interner wert (ID) verwendet wird
                RadDropdownlistlWZTyp.DisplayMember = "Typ"
                RadDropdownlistlWZHersteller.DisplayMember = "Hersteller"


                'zuweisen des neuen Typs
                RadDropdownlistlWZHersteller.Text = objWZ.Hersteller
                RadTextBoxWZBauartzulassung.Text = objWZ.Bauartzulassung
                RadTextBoxWZPruefbericht.Text = objWZ.Pruefbericht


                RadDropdownlistlWZTyp.Filter = Nothing 'filter leeren
                RadDropdownlistlWZTyp.Filter = New Predicate(Of Telerik.WinControls.UI.RadListDataItem)(AddressOf FilterItem)

                RadDropdownlistlWZTyp.Text = objWZ.Typ 'zurücksetzen der auswahl
                _suspendEvents = False

            End Using

        End If

    End Sub

#End Region

#Region "Methods"


    Private Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess



        'events abbrechen
        _suspendEvents = True

        Using context As New EichsoftwareClientdatabaseEntities1

            'laaden der Wagentyp dropdownliste
            Dim db = (From dbLookup In context.Lookup_Waagentyp Order By dbLookup.Typ Select dbLookup)
            _DatasourceDropdownListWaagentyp = db.ToList
            'Laden der AWG Dropdownliste

            'workaround: Wenn ein Benutzer eine Eichung Anlegt darf er keine Deaktivierten AWGs auswählen. Wenn sie gelesen  darf es aber sein, das deaktivierte Elemente gewälht werden
            If DialogModus = enuDialogModus.lesend Then
                Dim db2 = From dbLookup In context.Lookup_Auswertegeraet Order By dbLookup.Typ Select dbLookup
                _DatasourceDropdownListAWG = db2.ToList
            Else
                Dim db2 = From dbLookup In context.Lookup_Auswertegeraet Where dbLookup.Deaktiviert = False Order By dbLookup.Typ Select dbLookup
                _DatasourceDropdownListAWG = db2.ToList
            End If
            'Laden der WZ Dropdownliste (alles)
            'workaround: Wenn ein Benutzer eine Eichung Anlegt darf er keine Deaktivierten AWGs auswählen. Wenn sie gelesen  darf es aber sein, das deaktivierte Elemente gewälht werden
            If DialogModus = enuDialogModus.lesend Then
                Dim db3 = From dbLookup In context.Lookup_Waegezelle Order By dbLookup.Typ Select dbLookup
                _DatasourceDropdownListWZ = db3.ToList
            Else
                Dim db3 = From dbLookup In context.Lookup_Waegezelle Where dbLookup.Deaktiviert = False Order By dbLookup.Typ Select dbLookup
                _DatasourceDropdownListWZ = db3.ToList
            End If

            'Laden der WZ Hersteller Dropdownliste (nur hersteller namen gruppiert)
            'workaround: Wenn ein Benutzer eine Eichung Anlegt darf er keine Deaktivierten AWGs auswählen. Wenn sie gelesen  darf es aber sein, das deaktivierte Elemente gewälht werden
            If DialogModus = enuDialogModus.lesend Then
                Dim db4 = From dblookup In context.Lookup_Waegezelle Order By dblookup.Hersteller Group By Hersteller = dblookup.Hersteller Into HerstellerGruppe = Group Select New With {.Hersteller = Hersteller} ' Into Group Select New Lookup_Waegezelle
                _DatasourceDropdownListWZHersteller = db4.ToList
            Else
                Dim db4 = From dblookup In context.Lookup_Waegezelle Where dblookup.Deaktiviert = False Order By dblookup.Hersteller Group By Hersteller = dblookup.Hersteller Into HerstellerGruppe = Group Select New With {.Hersteller = Hersteller} ' Into Group Select New Lookup_Waegezelle
                _DatasourceDropdownListWZHersteller = db4.ToList

            End If


            Dim db5 = (From dbLookup In context.Lookup_Waagenart Select dbLookup)
            _DatasourceDropdownListWaagenArt = db5.ToList

        End Using


        'steuerelemente mit werten aus DB füllen
        FillControls()
        'events nicht mehr abbrechen
        _suspendEvents = False



    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Beschaffenheitspruefungsobjekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        'dropdown controls füllen
        'zuweisen der Datenquelle
        RadDropdownlistWaagenTyp.DataSource = _DatasourceDropdownListWaagentyp
        RadDropDownListWaagenArt.DataSource = _DatasourceDropdownListWaagenArt
        RadDropdownlistAWGTyp.DataSource = _DatasourceDropdownListAWG
        RadDropdownlistlWZTyp.DataSource = _DatasourceDropdownListWZ
        RadDropdownlistlWZHersteller.DataSource = _DatasourceDropdownListWZHersteller

        'Name der Propertie setzen, welche als Anzeigename verwendet wird
        RadDropdownlistWaagenTyp.ValueMember = "ID"
        RadDropDownListWaagenArt.ValueMember = "ID"
        RadDropdownlistAWGTyp.ValueMember = "ID"
        RadDropdownlistlWZTyp.ValueMember = "ID"
        'RadDropdownlistlWZHersteller.ValueMember = "ID"

        'Name der Propertie setzen, welche als interner wert (ID) verwendet wird
        RadDropdownlistAWGTyp.DisplayMember = "Typ"
        RadDropdownlistlWZTyp.DisplayMember = "Typ"
        RadDropdownlistlWZHersteller.DisplayMember = "Hersteller"

        'je nach sprache andere werte aus DB abrufen für Waagentyp
        Select Case My.Settings.AktuelleSprache.ToLower
            Case Is = "en"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_EN"
                RadDropDownListWaagenArt.DisplayMember = "Art_EN"
            Case Is = "de"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ"
                RadDropDownListWaagenArt.DisplayMember = "Art"
            Case Is = "pl"
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_PL"
                RadDropDownListWaagenArt.DisplayMember = "Art_PL"
            Case Else
                RadDropdownlistWaagenTyp.DisplayMember = "Typ_EN"
                RadDropDownListWaagenArt.DisplayMember = "Art_EN"

        End Select


        If DialogModus = enuDialogModus.lesend Or DialogModus = enuDialogModus.korrigierend Then
            Try
                RadTextBoxAWGBauartzulassung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung

            Catch ex As System.ObjectDisposedException 'fehler im Clientseitigen Lesemodus (bei bereits abegschickter Eichung)
                objEichprozess = objDBFunctions.HoleNachschlageListenFuerEichprozess(objEichprozess)
                RadTextBoxAWGBauartzulassung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung

            End Try

            RadTextBoxAWGBauartzulassung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
            RadTextBoxAWGHersteller.Text = objEichprozess.Lookup_Auswertegeraet.Hersteller
            RadTextBoxAWGPruefbericht.Text = objEichprozess.Lookup_Auswertegeraet.Pruefbericht
            RadTextBoxStammdatenOrt.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
            RadTextBoxStammdatenPLZ.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
            RadTextBoxStammdatenStrasse.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse
            RadTextBoxStammdatenWaagenbaufirma.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller
            RadTextBoxWaageSeriennummer.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
            RadTextBoxWaageZulassungsinhaber.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
            RadTextBoxWZBauartzulassung.Text = objEichprozess.Lookup_Waegezelle.Bauartzulassung
            RadTextBoxWZPruefbericht.Text = objEichprozess.Lookup_Waegezelle.Pruefbericht
            RadDropdownlistAWGTyp.SelectedValue = objEichprozess.FK_Auswertegeraet
            RadDropdownlistlWZHersteller.Text = objEichprozess.Lookup_Waegezelle.Hersteller
            RadDropdownlistlWZTyp.SelectedValue = objEichprozess.FK_Waegezelle
            RadDropDownListWaagenArt.SelectedValue = objEichprozess.FK_WaagenArt
            RadDropdownlistWaagenTyp.SelectedValue = objEichprozess.FK_WaagenTyp

            'deaktvieren des neuen WZ Buttons
            RadButtonNeueWaegezelle.Visible = False

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
        Else
            'wenn bereits ein Objekt exisitert (z.b. zurück navigiert wurde) sollen die Werte aus der DB geladen werden
            If objEichprozess.ID <> 0 Then
                Using context As New EichsoftwareClientdatabaseEntities1
                    'neu laden des Objekts, diesmal mit den lookup Objekten
                    objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Lookup_Waegezelle").Include("Kompatiblitaetsnachweis").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                End Using

                RadTextBoxAWGBauartzulassung.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
                RadTextBoxAWGHersteller.Text = objEichprozess.Lookup_Auswertegeraet.Hersteller
                RadTextBoxAWGPruefbericht.Text = objEichprozess.Lookup_Auswertegeraet.Pruefbericht
                RadTextBoxStammdatenOrt.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
                RadTextBoxStammdatenPLZ.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
                RadTextBoxStammdatenStrasse.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse
                RadTextBoxStammdatenWaagenbaufirma.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller
                RadTextBoxWaageSeriennummer.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
                RadTextBoxWaageZulassungsinhaber.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber


                RadTextBoxWZBauartzulassung.Text = objEichprozess.Lookup_Waegezelle.Bauartzulassung
                RadTextBoxWZPruefbericht.Text = objEichprozess.Lookup_Waegezelle.Pruefbericht
              
                If Not ((From items In RadDropdownlistAWGTyp.Items Where items.Value = objEichprozess.FK_Auswertegeraet).FirstOrDefault) Is Nothing Then
                    RadDropdownlistAWGTyp.SelectedValue = objEichprozess.FK_Auswertegeraet
                Else
                    RadDropdownlistAWGTyp.Text = "Deaktiviert / deactivated"
                End If

                RadDropdownlistlWZHersteller.Text = objEichprozess.Lookup_Waegezelle.Hersteller

                If Not ((From items In RadDropdownlistlWZTyp.Items Where items.Value = objEichprozess.FK_Waegezelle).FirstOrDefault) Is Nothing Then
                    RadDropdownlistlWZTyp.SelectedValue = objEichprozess.FK_Waegezelle
                Else
                    RadDropdownlistlWZTyp.Text = "Deaktiviert / deactivated"
                End If
                RadDropdownlistlWZTyp.SelectedValue = objEichprozess.FK_Waegezelle

                RadDropDownListWaagenArt.SelectedValue = objEichprozess.FK_WaagenArt
                RadDropdownlistWaagenTyp.SelectedValue = objEichprozess.FK_WaagenTyp
            Else
                'Stammdaten aus lokaler Lizenz laden
                Dim objLic As Lizensierung = objDBFunctions.HoleLizenzObjekt
                If Not objLic Is Nothing Then
                    RadTextBoxStammdatenOrt.Text = objLic.FirmaOrt
                    RadTextBoxStammdatenPLZ.Text = objLic.FirmaPLZ
                    RadTextBoxStammdatenStrasse.Text = objLic.FirmaStrasse
                    RadTextBoxStammdatenWaagenbaufirma.Text = objLic.Firma
                End If

                'nulltext auswählen, so das keine vorauswahl getroffen wird
                RadDropdownlistWaagenTyp.Text = RadDropdownlistWaagenTyp.NullText
                RadDropDownListWaagenArt.Text = RadDropDownListWaagenArt.NullText
                RadDropdownlistAWGTyp.Text = RadDropdownlistAWGTyp.NullText
                RadDropdownlistlWZTyp.Text = RadDropdownlistlWZTyp.NullText
                RadDropdownlistlWZHersteller.Text = RadDropdownlistlWZHersteller.NullText
            End If
        End If



        'fokus setzen
        RadTextBoxStammdatenWaagenbaufirma.Focus()
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Stammdaten
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso. Ergänzung: war ausdokumentiert, weil damit die Werte der NSW und WZ übeschrieben werden wenn man auf zurück klickt. Wenn es allerdings ausdokumenterit ist, funktioniert das anlegen einer neuen WZ nicht
        End If
    End Sub


    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()

        objEichprozess.FK_Auswertegeraet = RadDropdownlistAWGTyp.SelectedValue
        objEichprozess.FK_WaagenArt = RadDropDownListWaagenArt.SelectedValue
        objEichprozess.FK_WaagenTyp = RadDropdownlistWaagenTyp.SelectedValue
        objEichprozess.FK_Waegezelle = RadDropdownlistlWZTyp.SelectedValue

        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort = RadTextBoxStammdatenOrt.Text
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse = RadTextBoxStammdatenStrasse.Text
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller = RadTextBoxStammdatenWaagenbaufirma.Text
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = RadTextBoxStammdatenPLZ.Text

        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = RadTextBoxAWGBauartzulassung.Text 'laut HErrn Strack kriegt die Waaage die Zulassung des AWGs
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = RadTextBoxWaageSeriennummer.Text
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = "III"
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = RadTextBoxWaageZulassungsinhaber.Text



        'zuweisen der aktualisierten Objekt instanz an Hauptformular
        ParentFormular.CurrentEichprozess = objEichprozess
    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        Me.AbortSaveing = False
        'prüfen ob alle Felder ausgefüllt sind
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadTextBox Then
                        If Control.readonly = False Then
                            If Control.Text.trim.Equals("") Then

                                Me.AbortSaveing = True
                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red

                                CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                            Else
                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)

                                CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                            End If

                        End If
                    ElseIf TypeOf Control Is Telerik.WinControls.UI.RadDropDownList Then
                        If Control.Text.trim.Equals("-") Or Control.Text.trim.Equals("") Then
                            Me.AbortSaveing = True

                            'etwas umständlich, aber sonst komme ich nicht an den Rahmen der Textbox in der Dropdownliste
                            Try
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).BottomColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).TopColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).LeftColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).RightColor = Color.Red
                            Catch ex As Exception

                            End Try
                            CType(Control, Telerik.WinControls.UI.RadDropDownList).Focus()
                        ElseIf Control.Text.ToLower.Contains("deaktiviert") Then
                            Me.AbortSaveing = True

                            'etwas umständlich, aber sonst komme ich nicht an den Rahmen der Textbox in der Dropdownliste
                            Try
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).BottomColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).TopColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).LeftColor = Color.Red
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).RightColor = Color.Red
                            Catch ex As Exception

                            End Try
                            CType(Control, Telerik.WinControls.UI.RadDropDownList).Focus()
                        Else
                            Try
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).BottomColor = Color.Transparent
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).TopColor = Color.Transparent
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).LeftColor = Color.Transparent
                                DirectCast(CType(Control, Telerik.WinControls.UI.RadDropDownList).DropDownListElement.Children(0), Telerik.WinControls.Primitives.BorderPrimitive).RightColor = Color.Transparent
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Next
            End If
        Next
          'fehlermeldung anzeigen bei falscher validierung
        If Me.AbortSaveing = True Then
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
        End If
        Return True
    End Function

    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If


            If ValidateControls() Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If objEichprozess.ID = 0 Then 'Neue ID also CREATE Operation
                        If objEichprozess.Kompatiblitaetsnachweis Is Nothing Then
                            objEichprozess.Kompatiblitaetsnachweis = Context.Kompatiblitaetsnachweis.Create
                        End If
                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                        objEichprozess.FK_Bearbeitungsstatus = 4 'noch nicht versandt
                        Context.Eichprozess.Add(objEichprozess)
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    Else 'UPDATE


                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dbobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                        If Not dbobjEichprozess Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dbobjEichprozess

                            'prüfen ob es änderungen am Objekt gab
                            If AktuellerStatusDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
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



    ''' <summary>
    ''' methode zum Filtern der Dropdownliste
    ''' </summary>
    ''' <param name="listitem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function FilterItem(listitem As Telerik.WinControls.UI.RadListDataItem) As Boolean
        Try
            If CType(listitem.DataBoundItem, Lookup_Waegezelle).Hersteller = RadDropdownlistlWZHersteller.Text Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


#Region "Overrides"
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
                ' objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Eichbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()

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
#End Region
#Region "Workaround für Telerik Bug"


    'in der aktuellen Telerik Version q1 2013 gibt es ein Bug mit den Dropdownlisten. Diese erhalten keinen Fokus wenn man sie mit Tab ansteuert
    Private Sub RadDropdownlist_GotFocus(sender As Object, e As EventArgs) Handles RadDropdownlistAWGTyp.GotFocus, RadDropdownlistlWZHersteller.GotFocus, RadDropdownlistlWZTyp.GotFocus, RadDropDownListWaagenArt.GotFocus, RadDropdownlistWaagenTyp.GotFocus
        If _suspendEvents Then Exit Sub
        Dim c As Telerik.WinControls.UI.RadDropDownList

        If (TypeOf sender Is Telerik.WinControls.UI.RadDropDownList) Then
            c = CType(sender, Telerik.WinControls.UI.RadDropDownList)

            If (c.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList) AndAlso (Not c.DropDownListElement.ContainsFocus) Then

                c.DropDownListElement.Focus()
                'If c.DropDownListElement.IsPopupOpen = False Then
                '    c.DropDownListElement.ShowPopup()
                'End If

            End If
        End If
    End Sub
#End Region







End Class
