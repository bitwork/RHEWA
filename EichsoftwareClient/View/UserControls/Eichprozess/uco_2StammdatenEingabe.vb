Imports EichsoftwareClient

Public Class uco_2StammdatenEingabe

    Inherits ucoContent
    Implements IRhewaEditingDialog

#Region "Member Variables"
    Private _DatasourceDropdownListWaagentyp As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWaagenArt As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListAWG As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWZ As IEnumerable 'datenquelle für dropdown
    Private _DatasourceDropdownListWZHersteller As IEnumerable 'datenquelle für dropdown
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)

    '  Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Stammdateneingabe zurückgesetzt

    'Private objDBFunctions As New clsDBFunctions 'Klasse mit Hilfsfunktionen zum arbeiten mit der lokalen SQL Compact DB
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
        Me.SuspendLayout()
        SetzeUeberschrift()

        'daten füllen
        LoadFromDatabase()

        'fokus setzen
        RadTextBoxWaageSeriennummer.Focus()
        Me.ResumeLayout()
    End Sub



    'wenn die Art der Waage oder die WZ gewechselt wurde, muss kontrolliert werden, ob der Konformitätsbewertungsvorgang bereits fortschritten ist. Wenn also bereits ein späterer Status erreicht wurde, muss dieser zurückgesetzt werden auf die Stammdateneingabe
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
            Using Context As New Entities
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

                'RadDropdownlistlWZTyp.Text = objWZ.Typ 'zurücksetzen der auswahl
                RadDropdownlistlWZTyp.SelectedValue = objWZ.ID
                _suspendEvents = False

            End Using

        End If

    End Sub

#End Region

#Region "Methods"


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

#Region "Interface Methods"

    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Stammdaten

            Catch ex As Exception
            End Try
        End If
    End Sub
    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_2StammdatenEingabe))
        Lokalisierung(Me, resources)

        'je nach sprache andere werte aus DB abrufen für Waagentyp
        Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
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
    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase
        objEichprozess = ParentFormular.CurrentEichprozess

        'events abbrechen
        _suspendEvents = True

        Using context As New Entities

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

            Dim db5 = (From dbLookup In context.Lookup_Waagenart Where dbLookup.Art <> "Zweiteilungswaage" And dbLookup.Art <> "Dreiteilungswaage" Select dbLookup)
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
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
        MyBase.FillControls()
        'dropdown controls füllen
        'zuweisen der Datenquelle
        RadDropdownlistWaagenTyp.DataSource = _DatasourceDropdownListWaagentyp
        RadDropDownListWaagenArt.DataSource = _DatasourceDropdownListWaagenArt
        RadDropdownlistAWGTyp.DataSource = _DatasourceDropdownListAWG
        RadDropdownlistAWGTyp.SelectedIndex = -1
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
        Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
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
                objEichprozess = clsDBFunctions.HoleNachschlageListenFuerEichprozess(objEichprozess)
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

                'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
                DisableControls(RadGroupBoxAWG)
                DisableControls(RadGroupBoxStammdaten)
                DisableControls(RadGroupBoxWaage)
                DisableControls(RadGroupBoxWZ)
            End If
        Else
            'wenn bereits ein Objekt exisitert (z.b. zurück navigiert wurde) sollen die Werte aus der DB geladen werden
            If objEichprozess.ID <> 0 Then
                Using context As New Entities
                    'neu laden des Objekts, diesmal mit den lookup Objekten
                    objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
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

                RadTextBoxStammdatenOrt.Text = AktuellerBenutzer.Instance.Lizenz.FirmaOrt
                RadTextBoxStammdatenPLZ.Text = AktuellerBenutzer.Instance.Lizenz.FirmaPLZ
                RadTextBoxStammdatenStrasse.Text = AktuellerBenutzer.Instance.Lizenz.FirmaStrasse
                RadTextBoxStammdatenWaagenbaufirma.Text = AktuellerBenutzer.Instance.Lizenz.Firma

                'nulltext auswählen, so das keine vorauswahl getroffen wird
                RadDropdownlistWaagenTyp.SelectedIndex = -1
                RadDropDownListWaagenArt.SelectedIndex = -1
                RadDropdownlistAWGTyp.SelectedIndex = -1
                RadDropdownlistlWZTyp.SelectedIndex = -1
                RadDropdownlistlWZHersteller.SelectedIndex = -1


            End If
        End If

        'fokus setzen
        RadTextBoxStammdatenWaagenbaufirma.Focus()
    End Sub
    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt

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

        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

        'bei korrekturen gibt es rhewa seitig das Objekt nicht in der Datenbank. Dennoch müssen die Referenzen auf die Lookups für die Folgedialoge aktualisiert werden
        If DialogModus = enuDialogModus.korrigierend Then
            If Not objEichprozess.Lookup_Auswertegeraet Is Nothing And Not objEichprozess.Lookup_Waagenart Is Nothing And Not objEichprozess.Lookup_Waagentyp Is Nothing And Not objEichprozess.Lookup_Waegezelle Is Nothing Then
                Using Context As New Entities
                    objEichprozess.Lookup_Auswertegeraet = Context.Lookup_Auswertegeraet.Where(Function(x) x.ID = objEichprozess.FK_Auswertegeraet).FirstOrDefault
                    objEichprozess.Lookup_Waagenart = Context.Lookup_Waagenart.Where(Function(x) x.ID = objEichprozess.FK_WaagenArt).FirstOrDefault
                    objEichprozess.Lookup_Waagentyp = Context.Lookup_Waagentyp.Where(Function(x) x.ID = objEichprozess.FK_WaagenTyp).FirstOrDefault
                    objEichprozess.Lookup_Waegezelle = Context.Lookup_Waegezelle.Where(Function(x) x.ID = objEichprozess.FK_Waegezelle).FirstOrDefault
                End Using
            End If

        End If

        'zuweisen der aktualisierten Objekt instanz an Hauptformular
        ParentFormular.CurrentEichprozess = objEichprozess
    End Sub



    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadTextBoxAWGBauartzulassung.Text = "Bauartzulassung"
        RadTextBoxAWGHersteller.Text = "bitwork GmbH"
        RadTextBoxAWGPruefbericht.Text = "Prüfbereicht"
        RadTextBoxStammdatenOrt.Text = "Ort"
        RadTextBoxStammdatenPLZ.Text = "PLZ"
        RadTextBoxStammdatenStrasse.Text = "Strasse"
        RadTextBoxStammdatenWaagenbaufirma.Text = "Waagenbaufirma"
        RadTextBoxWaageSeriennummer.Text = "Seriennummer"
        RadTextBoxWaageZulassungsinhaber.Text = "Zulassungsinhaber"
        RadTextBoxWZBauartzulassung.Text = "Bauartzulassung"
        RadTextBoxWZPruefbericht.Text = "Pruefbereicht"
        RadDropdownlistAWGTyp.SelectedIndex = 1
        RadDropdownlistlWZHersteller.Text = "Hersteller"
        RadDropdownlistlWZTyp.SelectedIndex = 1
        RadDropDownListWaagenArt.SelectedIndex = 1
        RadDropdownlistWaagenTyp.SelectedIndex = 1
    End Sub

    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        Me.AbortSaving = False
        'prüfen ob alle Felder ausgefüllt sind
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadTextBox Then
                        If Control.readonly = False Then
                            If Control.Text.trim.Equals("") Then

                                Me.AbortSaving = True
                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red

                                CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                            Else
                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)

                                CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                            End If

                        End If
                    ElseIf TypeOf Control Is Telerik.WinControls.UI.RadDropDownList Then
                        If Control.Text.trim.Equals("-") Or Control.Text.trim.Equals("") Then
                            Me.AbortSaving = True

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
                            Me.AbortSaving = True

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
        If Me.AbortSaving = True Then
            If Debugger.IsAttached Then 'standardwerte füllen für schnelleres testen
                If Me.ShowValidationErrorBox(True) = DialogResult.Retry Then
                    OverwriteIstSoll()
                    Return True
                Else
                    Return False
                End If
            Else
                MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

        End If
        Return True
    End Function
    Protected Friend Overrides Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt

        'neuen Context aufbauen
        Using Context As New Entities
            'prüfen ob CREATE oder UPDATE durchgeführt werden muss
            If objEichprozess.ID = 0 Then 'Neue ID also CREATE Operation
                If objEichprozess.Kompatiblitaetsnachweis Is Nothing Then
                    objEichprozess.Kompatiblitaetsnachweis = Context.Kompatiblitaetsnachweis.Create
                End If
                'Füllt das Objekt mit den Werten aus den Steuerlementen
                UpdateObjekt()
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
                objEichprozess.FK_Bearbeitungsstatus = 4 'noch nicht versandt
                objEichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                Context.Eichprozess.Add(objEichprozess)
                'Speichern in Datenbank
                Context.SaveChanges()
            Else 'UPDATE

                'prüfen ob das Objekt anhand der ID gefunden werden kann
                Dim dbobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
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
                    UpdateObjekt()
                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using

        ParentFormular.CurrentEichprozess = objEichprozess
    End Sub

    Protected Friend Overrides Sub AktualisiereStatus() Implements IRhewaEditingDialog.AktualisiereStatus
        'nichts in diesem Dialog
    End Sub
    Protected Friend Overrides Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Then
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Entsperrroutine
    ''' </summary>
    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadGroupBoxAWG)
        EnableControls(RadGroupBoxStammdaten)
        EnableControls(RadGroupBoxWaage)
        EnableControls(RadGroupBoxWZ)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
        CloneAndSendServerObjekt()
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

    Private Sub RadGroupBoxWaage_MouseEnter(sender As Object, e As EventArgs) Handles RadGroupBoxWZ.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_StammdateBereichWZ)
    End Sub
    Private Sub Uco_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Stammdaten)
    End Sub






#End Region


End Class