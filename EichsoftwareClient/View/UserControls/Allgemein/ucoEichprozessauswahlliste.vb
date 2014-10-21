Public Class ucoEichprozessauswahlliste
    Inherits ucoContent
#Region "Member Variables"
    Private WithEvents _ParentForm As FrmMainContainer
    Private objWebserviceFunctions As New clsWebserviceFunctions
    'Private objDBFunctions As New clsDBFunctions
    Private WithEvents objFTP As New clsFTP
    Private objPage As Telerik.WinControls.UI.RadPageViewPage
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        _ParentForm = Nothing
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    End Sub

    Sub New(pParentForm As FrmMainContainer)
        MyBase.New(pParentForm, Nothing, Nothing)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        Me.RadGridViewRHEWAAlle.MasterTemplate.AutoExpandGroups = True
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _ParentForm = pParentForm
    End Sub
#End Region

#Region "Properties"
    Private ReadOnly Property VorgangsnummerGridClient As String
        Get
            Try
                If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
                    'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
                    If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                        Return RadGridViewAuswahlliste.SelectedRows(0).Cells("Vorgangsnummer").Value
                    End If
                End If
                Return ""
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Private ReadOnly Property VorgangsnummerGridServer As String
        Get
            Try
                If RadGridViewRHEWAAlle.SelectedRows.Count > 0 Then
                    'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
                    If TypeOf RadGridViewRHEWAAlle.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                        Return RadGridViewRHEWAAlle.SelectedRows(0).Cells("Vorgangsnummer").Value
                    End If
                End If
                Return ""
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
#End Region

#Region "Formular Logiken"
    Private Sub ucoEichprozessauswahlliste_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        laderoutine()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButtonRefresh.Click
        LoadFromDatabase()
    End Sub

    Friend Sub LadeRoutine()
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Hauptmenue)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Hauptmenue

                'benutzer auswahl einblenden
                Dim uco As New ucoBenutzerwechsel(Me)
                uco.objLizenz = AktuellerBenutzer.Instance.Lizenz
                uco.Dock = DockStyle.Fill
                ParentFormular.RadScrollablePanelTrafficLightBreadcrumb.Controls.Add(uco)
                ParentFormular.objUCOBenutzerwechsel = uco

            Catch ex As Exception
            End Try
        End If

        RadButtonEinstellungen.Visible = True
        RadButtonEinstellungen.Enabled = True


        'Tabelle mit Server Items ausblenden
        If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz = False Then
            Try
                objPage = RadPageView1.Pages(1) 'falls der benutzer gewechselt wird, und so zwischen mit RHEWA Lizenz und ohne RHEWA Lizenz gewechselt wird
                RadPageView1.Pages.RemoveAt(1)
                RadPageView1.Pages(0).Text = ""
            Catch ex As ArgumentOutOfRangeException
                'ignorieren
            End Try
            'großer button
            RadButtonClientNeu.Width = 192
            RadButtonClientNeu.Height = 63
            RadButtonClientNeu.TextAlignment = ContentAlignment.MiddleCenter
            RadButtonNeuStandardwaage.Visible = False
        Else
            RadButtonClientNeu.Width = 94
            RadButtonClientNeu.Height = 63
            RadButtonClientNeu.TextAlignment = ContentAlignment.MiddleRight
            RadButtonNeuStandardwaage.Visible = True

            If RadPageView1.Pages.Count = 1 Then
                RadPageView1.Pages(0).Text = "Eigene"

                RadPageView1.Pages.Add(objPage) 'wieder hinzufügen der Page falls vorher entfernt
                RadPageView1.Pages(1).Text = "Alle"
            End If

        End If

        'für den Fall das die Anwendung gerade erst installiert wurde, oder die einstellung zur Synchronisierung geändert wurde, sollen alle Eichungen vom RHEWA Server geholt werden, die einmal angelegt wurden
        If Not AktuellerBenutzer.Instance.Lizenz Is Nothing And AktuellerBenutzer.Instance.HoleAlleeigenenEichungenVomServer = True Then
            VerbindeMitWebserviceUndHoleAlles()
        Else
            LoadFromDatabase()
        End If
    End Sub

    ''' <summary>
    ''' Ein und ausblenden sowie lokalisierung des Grids 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FormatTable()
        'ausblenden von internen spalten
        RadGridViewAuswahlliste.Columns("ID").IsVisible = False
        RadGridViewAuswahlliste.Columns("Ausgeblendet").IsVisible = False
        RadGridViewAuswahlliste.Columns("Vorgangsnummer").IsVisible = False

        'übersetzen der Spaltenköpfe
        RadGridViewAuswahlliste.Columns("Bearbeitungsstatus").HeaderText = My.Resources.GlobaleLokalisierung.Bearbeitungsstatus
        RadGridViewAuswahlliste.Columns("Lookup_Waegezelle").HeaderText = My.Resources.GlobaleLokalisierung.Waegezelle
        RadGridViewAuswahlliste.Columns("Lookup_Auswertegeraet").HeaderText = My.Resources.GlobaleLokalisierung.AuswerteGeraet
        RadGridViewAuswahlliste.Columns("Lookup_Waagentyp").HeaderText = My.Resources.GlobaleLokalisierung.Waagentyp
        RadGridViewAuswahlliste.Columns("Lookup_Waagenart").HeaderText = My.Resources.GlobaleLokalisierung.Waagenart
        RadGridViewAuswahlliste.Columns("Fabriknummer").HeaderText = My.Resources.GlobaleLokalisierung.Fabriknummer

        'spaltengrößen anpassen (so viel platz wie möglich nehmen)
        RadGridViewAuswahlliste.BestFitColumns()
        RadGridViewAuswahlliste.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        RadGridViewAuswahlliste.BestFitColumns()
        RadGridViewAuswahlliste.EnableAlternatingRowColor = True
        RadGridViewAuswahlliste.ShowNoDataText = True
    End Sub

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) Then
            MyBase.LokalisierungNeeded(UserControl)
            'übersetzen und formatierung der Tabelle
            LoadFromDatabase()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoEichprozessauswahlliste))

            Me.RadButtonClientAusblenden.Text = resources.GetString("RadButtonClientAusblenden.Text")
            Me.RadButtonClientBearbeiten.Text = resources.GetString("RadButtonClientBearbeiten.Text")
            Me.RadButtonClientUpdateDatabase.Text = resources.GetString("RadButtonClientUpdateDatabase.Text")
            Me.RadButtonClientNeu.Text = resources.GetString("RadButtonClientNeu.Text")
            Me.RadCheckBoxAusblendenClientGeloeschterDokumente.Text = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.Text")

            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Auswahlliste)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Hauptmenue
        End If
    End Sub

    ''' <summary>
    ''' Initiert background threads die aus lokaler Client DB und Server Webservice die vorhandenen Eichungen abrufen
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LoadFromDatabase()
        Me.Enabled = False

        If Not BackgroundWorkerLoadFromDatabase.IsBusy Then
            BackgroundWorkerLoadFromDatabase.RunWorkerAsync()
        End If

        If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then
            If Not BackgroundWorkerLoadFromDatabaseRHEWA.IsBusy Then
                BackgroundWorkerLoadFromDatabaseRHEWA.RunWorkerAsync()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Konfigurationsdialog anzeigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonEinstellungen_Click(sender As Object, e As EventArgs) Handles RadButtonEinstellungen.Click
        ZeigeKonfigurationsDialog()
    End Sub

    ''' <summary>
    ''' Konfigurationsdialog anzeigen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ZeigeKonfigurationsDialog()
        Dim f As New FrmEinstellungen
        f.ShowDialog()
        If f.DialogResult = DialogResult.OK Then
            'neu aktualisierung der Eichungen
            VerbindeMitWebserviceUndHoleAlles()
            'aktualisieren des Grids
            LoadFromDatabase()
        End If
    End Sub

    ''' <summary>
    ''' aktivieren und deaktiveren der Schalter zum Genehmigen und Ablehnen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadGridView1_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles RadGridViewRHEWAAlle.SelectionChanged
        Try
            RadButtonEichprozessAblehnenRHEWA.Enabled = False
            RadButtonEichprozessGenehmigenRHEWA.Enabled = False

            If RadGridViewRHEWAAlle.SelectedRows.Count > 0 Then
                'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
                If TypeOf RadGridViewRHEWAAlle.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                    Dim SelectedStatus As String = "" 'Variable zum Speichern des BearbeitungsStatuses des aktuellen Prozesses
                    SelectedStatus = RadGridViewRHEWAAlle.SelectedRows(0).Cells("Bearbeitungsstatus").Value

                    If SelectedStatus.ToLower = "genehmigt" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    ElseIf SelectedStatus.ToLower = "fehlerhaft" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    ElseIf SelectedStatus.ToLower = "wartet auf bearbeitung" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = True
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = True
                    Else
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            RadButtonEichprozessAblehnenRHEWA.Enabled = False
            RadButtonEichprozessGenehmigenRHEWA.Enabled = False
        End Try
    End Sub
#End Region

#Region "Konformitätsbewertungsprozess Routinen Client"
    ''' <summary>
    ''' Neuen Eichprozess anlegen 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonNeu_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientNeu.Click
        OeffneNeuenEichprozess()
    End Sub

    ''' <summary>
    ''' Eichprozess bearbeiten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonBearbeiten_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientBearbeiten.Click
        BearbeiteEichprozess()
    End Sub

    ''' <summary>
    ''' Eichprozess bearbeiten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGridViewAuswahlliste_DoubleClick(sender As Object, e As EventArgs) Handles RadGridViewAuswahlliste.CellDoubleClick
        BearbeiteEichprozess()
    End Sub

    ''' <summary>
    ''' Neuen Eichprozess anlegen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OeffneNeuenEichprozess()
        Dim objEichprozess As Eichprozess = clsDBFunctions.ErzeugeNeuenEichprozess
        If Not objEichprozess Is Nothing Then
            'anzeigen des Dialogs zur Bearbeitung der Eichung
            Dim f As New FrmMainContainer(objEichprozess)
            f.ShowDialog()

            'nach dem schließen des Dialogs aktualisieren
            LoadFromDatabase()
        End If
    End Sub

    ''' <summary>
    ''' Eichprozess bearbeiten
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BearbeiteEichprozess()
        If Not Me.VorgangsnummerGridClient.Equals("") Then
            Dim objEichprozess = clsDBFunctions.HoleVorhandenenEichprozess(VorgangsnummerGridClient)
            If Not objEichprozess Is Nothing Then
                If objEichprozess.FK_Bearbeitungsstatus = 4 Or objEichprozess.FK_Bearbeitungsstatus = 2 Then 'nur wenn neu oder fehlerhaft darf eine Änderung vorgenommen werrden
                    'anzeigen des Dialogs zur Bearbeitung der Eichung
                    Dim f As New FrmMainContainer(objEichprozess)
                    f.ShowDialog()
                Else
                    'es gibt ihn schon und er ist bereits abgeschickt. nur lesend öffnen
                    objEichprozess = clsDBFunctions.HoleNachschlageListenFuerEichprozess(objEichprozess)
                    Dim f As New FrmMainContainer(objEichprozess, FrmMainContainer.enuDialogModus.lesend)
                    f.ShowDialog()
                End If

                'nach dem schließen des Dialogs aktualisieren
                LoadFromDatabase()
            End If
        End If
    End Sub

    ''' <summary>
    ''' ausblenden bzw wieder einblenden des aktuellen Konformitätsbewertungsvorgangs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadButtonAusblenden_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientAusblenden.Click
        EichprozessAusblendenEinblenden()
    End Sub

    ''' <summary>
    ''' ausblenden bzw wieder einblenden des aktuellen eichvorgangs
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub EichprozessAusblendenEinblenden()
        If Not Me.VorgangsnummerGridClient.Equals("") Then

            clsDBFunctions.BlendeEichprozessAus(VorgangsnummerGridClient)
            'neu laden der Liste
            LoadFromDatabase()
        End If
    End Sub

    ''' <summary>
    ''' ein und ausblenden der als gelöscht markierten Elementen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadCheckBoxAusblendenGeloeschterDokumente_ToggleStateChanged(sender As System.Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxAusblendenClientGeloeschterDokumente.ToggleStateChanged
        'laden der benötigten Liste mit nur den benötigten Spalten
        LoadFromDatabase()
    End Sub

    ''' <summary>
    ''' Lokale Eichprozesse für Gridview laden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerLoadFromDatabase_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerLoadFromDatabase.DoWork
        'background worker result enthält anschließend alle lokalen Eichprozesse
        e.Result = clsDBFunctions.LadeLokaleEichprozessListe(RadCheckBoxAusblendenClientGeloeschterDokumente.Checked)
    End Sub

    ''' <summary>
    '''   ''' <summary>
    ''' Databinding und Formatierung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerLoadFromDatabase_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerLoadFromDatabase.RunWorkerCompleted
        'zuweisen der Ergebnismenge als Datenquelle für das Grid
        RadGridViewAuswahlliste.DataSource = e.Result
        'Spalten ein und ausblenden und formatieren
        FormatTable()
        Me.Enabled = True
    End Sub
#End Region

#Region "Eichprozses Routinen Server"
    ''' <summary>
    ''' Lade eichprozessliste vom Server
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerLoadFromDatabaseRHEWA_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerLoadFromDatabaseRHEWA.DoWork
        'background worker result enthält anschließend alle serverseitigen Eichprozesse
        e.Result = clsWebserviceFunctions.GetServerEichprotokollListe()
    End Sub

    ''' <summary>
    ''' Databinding und Formatierung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorkerLoadFromDatabaseRHEWA_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerLoadFromDatabaseRHEWA.RunWorkerCompleted
        'zuweisen der Ergebnismenge als Datenquelle für das Grid
        RadGridViewRHEWAAlle.DataSource = e.Result
        Try
            Try
                'Spalten ein und ausblenden und formatieren
                'ausblenden von internen spalten
                RadGridViewRHEWAAlle.Columns("ID").IsVisible = False
                RadGridViewRHEWAAlle.Columns("Vorgangsnummer").IsVisible = False
                RadGridViewRHEWAAlle.Columns("Gesperrtdurch").HeaderText = "Gesperrt durch"
                RadGridViewRHEWAAlle.Columns("AnhangPfad").HeaderText = "Anhang"
                RadGridViewRHEWAAlle.Columns("Eichbevollmaechtigter").HeaderText = "Konformitätsbewertungsbevollmächtigter"
                RadGridViewRHEWAAlle.Columns("NeueWZ").HeaderText = "Neue WZ"
                RadGridViewRHEWAAlle.Columns("Gesperrtdurch").HeaderText = "Gesperrt durch"

            Catch ex As Exception
            End Try

            RadGridViewRHEWAAlle.BestFitColumns()
            RadGridViewRHEWAAlle.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
            RadGridViewRHEWAAlle.BestFitColumns()
            RadGridViewRHEWAAlle.EnableAlternatingRowColor = True
            RadGridViewRHEWAAlle.ShowNoDataText = True
            Me.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Öffnet einen Eichprozess vom Server zum lesen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ZeigeServerEichprozess()
        If Not Me.VorgangsnummerGridServer.Equals("") Then

            Dim objClientEichprozess = clsWebserviceFunctions.ZeigeServerEichprozess(VorgangsnummerGridServer)
            'anzeigen des Dialogs zur Bearbeitung der Eichung
            If Not objClientEichprozess Is Nothing Then

                ' lese modus, dann soll beliebig im Eichprozess hin und her gewechselt werden können => Eichprozessstatus auf Versenden setzen
                objClientEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden

                Dim f As New FrmMainContainer(objClientEichprozess, FrmMainContainer.enuDialogModus.lesend)
                f.ShowDialog()
                'nach dem schließen des Dialogs aktualisieren
                LoadFromDatabase()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Einblenden eines Anhang Symbols für Anträge mit Dateianhang
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGridViewRHEWAAlle_CellFormatting(sender As Object, e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles RadGridViewRHEWAAlle.CellFormatting
        Try
            If (RadGridViewRHEWAAlle.Columns(e.ColumnIndex).Name = "AnhangPfad") Then
                'einblenden des symbols
                If Not e.CellElement.Text.Trim.Equals("") Then
                    e.CellElement.Value = e.CellElement.Text
                    e.CellElement.Text = ""
                    e.CellElement.Image = My.Resources.attach
                Else
                    e.CellElement.Image = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Kopiert eichprozess vom Server in ein Client Objekt als Vorlage
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RadButtonEichprozessKopieren_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessKopierenRHEWA.Click
        GetLokaleKopieVonEichprozess()
    End Sub

    ''' <summary>
    ''' Kopiert eichprozess vom Server in ein Client Objekt als Vorlage 1 zu 1
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetLokaleKopieVonEichprozess()
        If Not Me.VorgangsnummerGridServer.Equals("") Then
            If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Kopieren, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim objClientEichprozess = clsWebserviceFunctions.old_GetLokaleKopieVonEichprozess(VorgangsnummerGridServer)

                If Not objClientEichprozess Is Nothing Then
                    'anzeigen des Dialogs zur Bearbeitung der Eichung
                    Dim f As New FrmMainContainer(objClientEichprozess)
                    f.ShowDialog()
                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()
                End If
            End If
        End If
    End Sub


#Region "Genehmigen / Ablehnen"
    ''' <summary>
    ''' Eichprozess genehmigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonEichprozessGenehmigen_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessGenehmigenRHEWA.Click
        GenehnmigeGewaehltenVorgang()
    End Sub

    ''' <summary>
    ''' Eichprozess genehmigen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenehnmigeGewaehltenVorgang()
        If Not Me.VorgangsnummerGridServer.Equals("") Then
            If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Genehmigen, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If clsWebserviceFunctions.GenehmigeEichprozess(VorgangsnummerGridServer) Then
                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Eichprozess ablehnen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RadButtonEichprozessAblehnen_click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessAblehnenRHEWA.Click
        If Not Me.VorgangsnummerGridServer.Equals("") Then
            If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Ablehnen, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If clsWebserviceFunctions.AblehnenEichprozess(VorgangsnummerGridServer) Then
                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Eichprozess ablehnen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RadButtonBearbeitenRHEWA_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichungAnsehenRHEWA.Click
        ZeigeServerEichprozess()
    End Sub
#End Region

#Region "FTP Download"
    ''' <summary>
    ''' öffnen des Dateianhangs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadGridViewRHEWAAlle_CellDoubleClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridViewRHEWAAlle.CellDoubleClick
        Try
            If (RadGridViewRHEWAAlle.Columns(e.ColumnIndex).Name = "AnhangPfad") Then

                If e.Value.Trim.Equals("") = False Then
                    Dim Vorgangsnummer As String
                    Vorgangsnummer = e.Row.Cells("Vorgangsnummer").Value
                    Try
                        OeffneDateiVonFTP(e.Value, Vorgangsnummer)
                    Catch ex As Exception
                        Debug.WriteLine(ex.ToString)
                    End Try
                Else
                    ZeigeServerEichprozess()
                End If
            Else
                ZeigeServerEichprozess()
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub OeffneDateiVonFTP(ByVal LokalerPfad As String, ByVal vorgangsnummer As String)
        If IO.File.Exists(LokalerPfad) Then
            Dim proc As Process = New Process()
            proc.StartInfo.FileName = LokalerPfad
            proc.StartInfo.UseShellExecute = True
            proc.Start()
        Else
            If Not BackgroundWorkerDownloadFromFTP.IsBusy Then
                Me.Enabled = False
                Me.RadProgressBar.Visible = True

                Me.RadProgressBar.Maximum = 100
                BackgroundWorkerDownloadFromFTP.RunWorkerAsync(vorgangsnummer)
            End If
        End If


    End Sub

    Public Function InitDownloadDateiVonFTP(ByVal vorgangsnummer As String) As String

        Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                Webcontext.Open()
            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try


            Dim objFTPDaten = Webcontext.GetFTPCredentials(AktuellerBenutzer.Instance.Lizenz.HEKennung, AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel, vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


            'aufbereiten der für FTP benötigten Verbindungsdaten
            If Not objFTPDaten Is Nothing Then

                'get decrypted Password and configuration from Database

                Dim password As String = objFTPDaten.FTPEncryptedPassword
                ' original plaintext
                Dim passPhrase As String = "Pas5pr@se"
                ' can be any string
                Dim saltValue As String = objFTPDaten.FTPSaltKey
                ' can be any string
                Dim hashAlgorithm As String = "SHA1"
                ' can be "MD5"
                Dim passwordIterations As Integer = 2
                ' can be any number
                Dim initVector As String = "@1B2c3D4e5F6g7H8"
                ' must be 16 bytes
                Dim keySize As Integer = 256
                ' can be 192 or 128

                password = RijndaelSimple.Decrypt(password, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, _
                    keySize)


                Dim file As New IO.FileInfo(objFTPDaten.FTPFilePath)


                'download Ordner anlegen wenn benötigt
                Dim folder As New IO.DirectoryInfo(file.DirectoryName)
                Try
                    If folder.Exists = False Then
                        folder.Create()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return ""
                End Try

                'downloadgroße ermitteln
                Dim filesize As Long
                filesize = objFTP.GetFileSize(objFTPDaten.FTPServername, objFTPDaten.FTPUserName, password, file.Name)
                If filesize = 0 Then Return "" 'abbruch 
                BackgroundWorkerDownloadFromFTP.ReportProgress(filesize)
                Try
                    'datei download. FTPUploadPath bekommt den reelen Pfad auf dem FTP Server
                    If objFTP.DownloadFileFromFTP(objFTPDaten.FTPServername, objFTPDaten.FTPUserName, password, file.Name, file.FullName) Then
                        Return file.FullName
                    Else
                        Try
                            file.Delete()
                        Catch ex As Exception
                            MessageBox.Show("Konnte fehlerhafte Datei nicht löschen " & file.FullName)
                        End Try
                        Return ""
                    End If
                Catch ex As Exception
                    Try
                        file.Delete()
                    Catch ex2 As Exception
                        MessageBox.Show("Konnte fehlerhafte Datei nicht löschen " & file.FullName)
                    End Try
                    Return ""
                End Try
            End If
        End Using

        Return ""
    End Function



    Private Sub BackgroundWorkerDownloadFromFTP_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerDownloadFromFTP.DoWork
        Dim vorgangsnummer As String = e.Argument
        e.Result = InitDownloadDateiVonFTP(vorgangsnummer)
    End Sub

    Private Sub BackgroundWorkerDownloadFromFTP_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerDownloadFromFTP.RunWorkerCompleted
        Me.Enabled = True
        Me.RadProgressBar.Visible = False
        Me.RadProgressBar.Value1 = 0
        RadProgressBar.Text = ""


        Dim filepath As String = e.Result
        If filepath.Equals("") = False Then
            Dim proc As Process = New Process()
            proc.StartInfo.FileName = filepath
            proc.StartInfo.UseShellExecute = True
            proc.Start()
        Else
            MessageBox.Show("Konnte Datei am Pfad nicht finden. Sie konnte auch nicht heruntergeladen werden.")
        End If
    End Sub

    Private Sub BackgroundWorkerDownloadFromFTP_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerDownloadFromFTP.ProgressChanged
        Try
            If e.ProgressPercentage = 0 Then
                RadProgressBar.Value1 = e.UserState
                RadProgressBar.Text = CInt(CInt(e.UserState) / 1024) & " KB/ " & CInt(CInt(RadProgressBar.Maximum) / 1024) & " KB"
                Me.Refresh()
            Else
                RadProgressBar.Maximum = e.ProgressPercentage
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub objFTP_ReportFTPProgress(Progress As Integer) Handles objFTP.ReportFTPProgress
        Try
            BackgroundWorkerDownloadFromFTP.ReportProgress(0, Progress)
        Catch ex As Exception
        End Try
    End Sub
#End Region

#End Region

#Region "Updates aus Webservice"

    ''' <summary>
    ''' Synchronisiert Daten wie WZ, AWG, Stammdaten mit Webservice
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VerbindeMitWebserviceUndHoleAlles()
        Dim bolSyncData As Boolean = True 'Wert der genutzt wird um ggfs die Synchrosierung abzubrechen, falls ein Benutzer noch ungesendete Konformitätsbewertungsvorgänge hat
        If clsWebserviceFunctions.TesteVerbindung() Then

            'variablen zur Ausgabe ob es änderungen gibt:
            Dim bolNeuStammdaten As Boolean = False
            Dim bolNeuWZ As Boolean = False
            Dim bolNeuAWG As Boolean = False
            Dim bolNeuGenehmigung As Boolean = False

            'prüfen ob noch nicht abgeschickte Eichungen vorlieren. Wenn ja Hinweismeldung und Abbruchmöglichkeit für Benutzer
            If clsDBFunctions.PruefeAufUngesendeteEichungen() = True Then
                If MessageBox.Show(My.Resources.GlobaleLokalisierung.Warnung_EichungenWerdenGeloescht, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    bolSyncData = True
                Else
                    AktuellerBenutzer.Instance.HoleAlleeigenenEichungenVomServer = False
                    AktuellerBenutzer.SaveSettings()
                    bolSyncData = False
                End If
            End If

            If bolSyncData Then

                'für den Fall das die Anwendung gerade erst installiert wurde, oder die einstellung zur Synchronisierung geändert wurde, sollen alle Eichungen vom RHEWA Server geholt werden, die einmal angelegt wurden
                If clsDBFunctions.LoescheLokaleDatenbank() Then
                    AktuellerBenutzer.Instance.LetztesUpdate = "01.01.2000"
                    AktuellerBenutzer.SaveSettings()
                    'neue Stammdaten zum Benutzer holen
                    clsWebserviceFunctions.GetNeueStammdaten(bolNeuStammdaten)
                    'hole alle WZ
                    clsWebserviceFunctions.GetNeueWZ(bolNeuWZ)
                    'prüfen ob es neue AWG gibt
                    clsWebserviceFunctions.GetNeuesAWG(bolNeuAWG)

                    'prüfen ob Eichprozesse die versendet wurden genehmigt oder abgelehnt wurden
                    clsWebserviceFunctions.GetGenehmigungsstatus(bolNeuGenehmigung)


                    AktuellerBenutzer.Instance.LetztesUpdate = Date.Now
                    AktuellerBenutzer.SaveSettings()

                    clsWebserviceFunctions.GetEichprotokolleVomServer()

                    AktuellerBenutzer.Instance.HoleAlleeigenenEichungenVomServer = False
                    AktuellerBenutzer.SaveSettings()

                    Dim returnMessage As String = My.Resources.GlobaleLokalisierung.Aktualisierung_Erfolgreich
                    If bolNeuWZ Then
                        returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuWZ & " "
                    End If
                    If bolNeuAWG Then
                        returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuAWG & " "
                    End If

                    If bolNeuGenehmigung Then
                        returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuEichung & " "
                    End If

                    If bolNeuStammdaten Then

                    End If
                End If
            End If
        Else
            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung)
        End If
    End Sub

    ''' <summary>
    ''' Methode welche sich mit dem Webservice verbinduet und nach aktualisierungen für WZ, AWGs und eigenen Eichungen guckt
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VerbindeMitWebServiceUndAktualisiere()
        If clsWebserviceFunctions.TesteVerbindung() Then

            'variablen zur Ausgabe ob es änderungen gibt:
            Dim bolNeuStammdaten As Boolean = False
            Dim bolNeuWZ As Boolean = False
            Dim bolNeuAWG As Boolean = False
            Dim bolNeuGenehmigung As Boolean = False


            'neue Stammdaten zum Benutzer holen
            clsWebserviceFunctions.GetNeueStammdaten(bolNeuStammdaten)

            'prüfen ob es neue WZ gibt
            clsWebserviceFunctions.GetNeueWZ(bolNeuWZ)
            'prüfen ob es neue AWG gibt
            clsWebserviceFunctions.GetNeuesAWG(bolNeuAWG)



            AktuellerBenutzer.Instance.LetztesUpdate = Date.Now
            AktuellerBenutzer.SaveSettings()


            'prüfen ob Eichprozesse die versendet wurden genehmigt oder abgelehnt wurden
            clsWebserviceFunctions.GetGenehmigungsstatus(bolNeuGenehmigung)

            'refresh
            LoadFromDatabase()

            Dim returnMessage As String = My.Resources.GlobaleLokalisierung.Aktualisierung_Erfolgreich
            If bolNeuWZ Then
                returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuWZ & " "
            End If
            If bolNeuAWG Then
                returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuAWG & " "
            End If

            If bolNeuGenehmigung Then
                returnMessage += vbNewLine & vbNewLine & My.Resources.GlobaleLokalisierung.Aktualisierung_NeuEichung & " "
            End If

            If bolNeuStammdaten Then

            End If
            MessageBox.Show(returnMessage)
        Else
            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung)
        End If
    End Sub

    ''' <summary>
    ''' Methode welche sich mit dem Webservice verbinduet und nach aktualisierungen für WZ, AWGs und eigenen Eichungen guckt
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RadButtonClientUpdateDatabase_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientUpdateDatabase.Click
        VerbindeMitWebServiceUndAktualisiere()
    End Sub
#End Region



  
    Private Sub RadButtonNeuStandardwaage_Click(sender As Object, e As EventArgs) Handles RadButtonNeuStandardwaage.Click
        Dim f As New FrmAuswahlStandardwaage
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub
End Class
