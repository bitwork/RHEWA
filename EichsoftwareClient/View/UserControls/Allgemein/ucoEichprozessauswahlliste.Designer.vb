﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucoEichprozessauswahlliste
    Inherits EichsoftwareClient.ucoContent

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoEichprozessauswahlliste))
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGridViewAuswahlliste = New Telerik.WinControls.UI.RadGridView()
        Me.BackgroundWorkerLoadFromDatabase = New System.ComponentModel.BackgroundWorker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPageEigene = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadButtonNeuStandardwaage = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEinstellungen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonClientUpdateDatabase = New Telerik.WinControls.UI.RadButton()
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadButtonClientAusblenden = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonClientNeu = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonClientBearbeiten = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPageAlle = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadButtonRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadCheckBoxLadeAlleEichprozesse = New Telerik.WinControls.UI.RadCheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadButtonEichprozessKopierenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessAblehnenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessGenehmigenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadGridViewRHEWAAlle = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonEichungAnsehenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.RadProgressBar = New Telerik.WinControls.UI.RadProgressBar()
        Me.BackgroundWorkerLoadFromDatabaseRHEWA = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorkerDownloadFromFTP = New System.ComponentModel.BackgroundWorker()
        CType(Me.RadGridViewAuswahlliste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewAuswahlliste.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPageEigene.SuspendLayout()
        CType(Me.RadButtonNeuStandardwaage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEinstellungen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientUpdateDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientAusblenden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientNeu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientBearbeiten, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPageAlle.SuspendLayout()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxLadeAlleEichprozesse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewRHEWAAlle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewRHEWAAlle.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichungAnsehenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridViewAuswahlliste
        '
        resources.ApplyResources(Me.RadGridViewAuswahlliste, "RadGridViewAuswahlliste")
        '
        '
        '
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowEditRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridViewAuswahlliste.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.RadGridViewAuswahlliste.Name = "RadGridViewAuswahlliste"
        '
        '
        '
        Me.RadGridViewAuswahlliste.RootElement.AccessibleDescription = resources.GetString("RadGridViewAuswahlliste.RootElement.AccessibleDescription")
        Me.RadGridViewAuswahlliste.RootElement.AccessibleName = resources.GetString("RadGridViewAuswahlliste.RootElement.AccessibleName")
        Me.RadGridViewAuswahlliste.RootElement.Alignment = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadGridViewAuswahlliste.RootElement.AngleTransform = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.AngleTransform"), Single)
        Me.RadGridViewAuswahlliste.RootElement.FlipText = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.FlipText"), Boolean)
        Me.RadGridViewAuswahlliste.RootElement.Margin = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadGridViewAuswahlliste.RootElement.Text = resources.GetString("RadGridViewAuswahlliste.RootElement.Text")
        Me.RadGridViewAuswahlliste.RootElement.TextOrientation = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadGridViewAuswahlliste.ShowNoDataText = False
        '
        'BackgroundWorkerLoadFromDatabase
        '
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPageEigene)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPageAlle)
        resources.ApplyResources(Me.RadPageView1, "RadPageView1")
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = resources.GetString("RadPageView1.RootElement.AccessibleDescription")
        Me.RadPageView1.RootElement.AccessibleName = resources.GetString("RadPageView1.RootElement.AccessibleName")
        Me.RadPageView1.RootElement.Alignment = CType(resources.GetObject("RadPageView1.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadPageView1.RootElement.AngleTransform = CType(resources.GetObject("RadPageView1.RootElement.AngleTransform"), Single)
        Me.RadPageView1.RootElement.FlipText = CType(resources.GetObject("RadPageView1.RootElement.FlipText"), Boolean)
        Me.RadPageView1.RootElement.Margin = CType(resources.GetObject("RadPageView1.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadPageView1.RootElement.Text = resources.GetString("RadPageView1.RootElement.Text")
        Me.RadPageView1.RootElement.TextOrientation = CType(resources.GetObject("RadPageView1.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadPageView1.SelectedPage = Me.RadPageViewPageAlle
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(0).GetChildAt(1), Telerik.WinControls.UI.StripViewButtonsPanel).Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadPageViewPageEigene
        '
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonNeuStandardwaage)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonEinstellungen)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientUpdateDatabase)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadGridViewAuswahlliste)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadCheckBoxAusblendenClientGeloeschterDokumente)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientAusblenden)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientNeu)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientBearbeiten)
        Me.RadPageViewPageEigene.ItemSize = New System.Drawing.SizeF(49.0!, 25.0!)
        resources.ApplyResources(Me.RadPageViewPageEigene, "RadPageViewPageEigene")
        Me.RadPageViewPageEigene.Name = "RadPageViewPageEigene"
        '
        'RadButtonNeuStandardwaage
        '
        resources.ApplyResources(Me.RadButtonNeuStandardwaage, "RadButtonNeuStandardwaage")
        Me.RadButtonNeuStandardwaage.Image = Global.EichsoftwareClient.My.Resources.Resources.report_add
        Me.RadButtonNeuStandardwaage.Name = "RadButtonNeuStandardwaage"
        '
        '
        '
        Me.RadButtonNeuStandardwaage.RootElement.AccessibleDescription = resources.GetString("RadButtonNeuStandardwaage.RootElement.AccessibleDescription")
        Me.RadButtonNeuStandardwaage.RootElement.AccessibleName = resources.GetString("RadButtonNeuStandardwaage.RootElement.AccessibleName")
        Me.RadButtonNeuStandardwaage.RootElement.Alignment = CType(resources.GetObject("RadButtonNeuStandardwaage.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonNeuStandardwaage.RootElement.AngleTransform = CType(resources.GetObject("RadButtonNeuStandardwaage.RootElement.AngleTransform"), Single)
        Me.RadButtonNeuStandardwaage.RootElement.FlipText = CType(resources.GetObject("RadButtonNeuStandardwaage.RootElement.FlipText"), Boolean)
        Me.RadButtonNeuStandardwaage.RootElement.Margin = CType(resources.GetObject("RadButtonNeuStandardwaage.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonNeuStandardwaage.RootElement.Text = resources.GetString("RadButtonNeuStandardwaage.RootElement.Text")
        Me.RadButtonNeuStandardwaage.RootElement.TextOrientation = CType(resources.GetObject("RadButtonNeuStandardwaage.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonNeuStandardwaage.TextWrap = True
        '
        'RadButtonEinstellungen
        '
        resources.ApplyResources(Me.RadButtonEinstellungen, "RadButtonEinstellungen")
        Me.RadButtonEinstellungen.BackColor = System.Drawing.Color.Transparent
        Me.RadButtonEinstellungen.Image = Global.EichsoftwareClient.My.Resources.Resources.cog
        Me.RadButtonEinstellungen.Name = "RadButtonEinstellungen"
        '
        '
        '
        Me.RadButtonEinstellungen.RootElement.AccessibleDescription = resources.GetString("RadButtonEinstellungen.RootElement.AccessibleDescription")
        Me.RadButtonEinstellungen.RootElement.AccessibleName = resources.GetString("RadButtonEinstellungen.RootElement.AccessibleName")
        Me.RadButtonEinstellungen.RootElement.Alignment = CType(resources.GetObject("RadButtonEinstellungen.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonEinstellungen.RootElement.AngleTransform = CType(resources.GetObject("RadButtonEinstellungen.RootElement.AngleTransform"), Single)
        Me.RadButtonEinstellungen.RootElement.FlipText = CType(resources.GetObject("RadButtonEinstellungen.RootElement.FlipText"), Boolean)
        Me.RadButtonEinstellungen.RootElement.Margin = CType(resources.GetObject("RadButtonEinstellungen.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEinstellungen.RootElement.Text = resources.GetString("RadButtonEinstellungen.RootElement.Text")
        Me.RadButtonEinstellungen.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEinstellungen.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        CType(Me.RadButtonEinstellungen.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Image = Global.EichsoftwareClient.My.Resources.Resources.cog
        CType(Me.RadButtonEinstellungen.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        CType(Me.RadButtonEinstellungen.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        CType(Me.RadButtonEinstellungen.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Text = resources.GetString("resource.Text")
        CType(Me.RadButtonEinstellungen.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Shape = Nothing
        '
        'RadButtonClientUpdateDatabase
        '
        resources.ApplyResources(Me.RadButtonClientUpdateDatabase, "RadButtonClientUpdateDatabase")
        Me.RadButtonClientUpdateDatabase.Image = Global.EichsoftwareClient.My.Resources.Resources.database_refresh
        Me.RadButtonClientUpdateDatabase.Name = "RadButtonClientUpdateDatabase"
        '
        '
        '
        Me.RadButtonClientUpdateDatabase.RootElement.AccessibleDescription = resources.GetString("RadButtonClientUpdateDatabase.RootElement.AccessibleDescription")
        Me.RadButtonClientUpdateDatabase.RootElement.AccessibleName = resources.GetString("RadButtonClientUpdateDatabase.RootElement.AccessibleName")
        Me.RadButtonClientUpdateDatabase.RootElement.Alignment = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonClientUpdateDatabase.RootElement.AngleTransform = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.AngleTransform"), Single)
        Me.RadButtonClientUpdateDatabase.RootElement.FlipText = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.FlipText"), Boolean)
        Me.RadButtonClientUpdateDatabase.RootElement.Margin = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientUpdateDatabase.RootElement.Text = resources.GetString("RadButtonClientUpdateDatabase.RootElement.Text")
        Me.RadButtonClientUpdateDatabase.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadCheckBoxAusblendenClientGeloeschterDokumente
        '
        resources.ApplyResources(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, "RadCheckBoxAusblendenClientGeloeschterDokumente")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.Name = "RadCheckBoxAusblendenClientGeloeschterDokumente"
        '
        '
        '
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AccessibleDescription = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AccessibleDescription" & _
        "")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AccessibleName = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AccessibleName")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Alignment = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AngleTransform = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.AngleTransform"), Single)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.FlipText = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.FlipText"), Boolean)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Margin = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Text = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Text")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.TextOrientation = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadButtonClientAusblenden
        '
        resources.ApplyResources(Me.RadButtonClientAusblenden, "RadButtonClientAusblenden")
        Me.RadButtonClientAusblenden.Image = Global.EichsoftwareClient.My.Resources.Resources.report_delete
        Me.RadButtonClientAusblenden.Name = "RadButtonClientAusblenden"
        '
        '
        '
        Me.RadButtonClientAusblenden.RootElement.AccessibleDescription = resources.GetString("RadButtonClientAusblenden.RootElement.AccessibleDescription")
        Me.RadButtonClientAusblenden.RootElement.AccessibleName = resources.GetString("RadButtonClientAusblenden.RootElement.AccessibleName")
        Me.RadButtonClientAusblenden.RootElement.Alignment = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonClientAusblenden.RootElement.AngleTransform = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.AngleTransform"), Single)
        Me.RadButtonClientAusblenden.RootElement.FlipText = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.FlipText"), Boolean)
        Me.RadButtonClientAusblenden.RootElement.Margin = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientAusblenden.RootElement.Text = resources.GetString("RadButtonClientAusblenden.RootElement.Text")
        Me.RadButtonClientAusblenden.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientAusblenden.TextWrap = True
        '
        'RadButtonClientNeu
        '
        resources.ApplyResources(Me.RadButtonClientNeu, "RadButtonClientNeu")
        Me.RadButtonClientNeu.Image = Global.EichsoftwareClient.My.Resources.Resources.report_add
        Me.RadButtonClientNeu.Name = "RadButtonClientNeu"
        '
        '
        '
        Me.RadButtonClientNeu.RootElement.AccessibleDescription = resources.GetString("RadButtonClientNeu.RootElement.AccessibleDescription")
        Me.RadButtonClientNeu.RootElement.AccessibleName = resources.GetString("RadButtonClientNeu.RootElement.AccessibleName")
        Me.RadButtonClientNeu.RootElement.Alignment = CType(resources.GetObject("RadButtonClientNeu.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonClientNeu.RootElement.AngleTransform = CType(resources.GetObject("RadButtonClientNeu.RootElement.AngleTransform"), Single)
        Me.RadButtonClientNeu.RootElement.FlipText = CType(resources.GetObject("RadButtonClientNeu.RootElement.FlipText"), Boolean)
        Me.RadButtonClientNeu.RootElement.Margin = CType(resources.GetObject("RadButtonClientNeu.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientNeu.RootElement.Text = resources.GetString("RadButtonClientNeu.RootElement.Text")
        Me.RadButtonClientNeu.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientNeu.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientNeu.TextWrap = True
        '
        'RadButtonClientBearbeiten
        '
        resources.ApplyResources(Me.RadButtonClientBearbeiten, "RadButtonClientBearbeiten")
        Me.RadButtonClientBearbeiten.Image = Global.EichsoftwareClient.My.Resources.Resources.report_edit
        Me.RadButtonClientBearbeiten.Name = "RadButtonClientBearbeiten"
        '
        '
        '
        Me.RadButtonClientBearbeiten.RootElement.AccessibleDescription = resources.GetString("RadButtonClientBearbeiten.RootElement.AccessibleDescription")
        Me.RadButtonClientBearbeiten.RootElement.AccessibleName = resources.GetString("RadButtonClientBearbeiten.RootElement.AccessibleName")
        Me.RadButtonClientBearbeiten.RootElement.Alignment = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonClientBearbeiten.RootElement.AngleTransform = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.AngleTransform"), Single)
        Me.RadButtonClientBearbeiten.RootElement.FlipText = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.FlipText"), Boolean)
        Me.RadButtonClientBearbeiten.RootElement.Margin = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientBearbeiten.RootElement.Text = resources.GetString("RadButtonClientBearbeiten.RootElement.Text")
        Me.RadButtonClientBearbeiten.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientBearbeiten.TextWrap = True
        '
        'RadPageViewPageAlle
        '
        Me.RadPageViewPageAlle.Controls.Add(Me.Label3)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis)
        Me.RadPageViewPageAlle.Controls.Add(Me.Label2)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonRefresh)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadCheckBoxLadeAlleEichprozesse)
        Me.RadPageViewPageAlle.Controls.Add(Me.Label1)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessKopierenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessAblehnenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessGenehmigenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadGridViewRHEWAAlle)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichungAnsehenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.FlowLayoutPanel1)
        Me.RadPageViewPageAlle.ItemSize = New System.Drawing.SizeF(33.0!, 25.0!)
        resources.ApplyResources(Me.RadPageViewPageAlle, "RadPageViewPageAlle")
        Me.RadPageViewPageAlle.Name = "RadPageViewPageAlle"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'RadDateTimePickerFilterMonatLadeAlleEichprozesseBis
        '
        resources.ApplyResources(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis, "RadDateTimePickerFilterMonatLadeAlleEichprozesseBis")
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.Name = "RadDateTimePickerFilterMonatLadeAlleEichprozesseBis"
        '
        '
        '
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AccessibleDescription = resources.GetString("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AccessibleDescrip" & _
        "tion")
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AccessibleName = resources.GetString("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AccessibleName")
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Alignment = CType(resources.GetObject("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AngleTransform = CType(resources.GetObject("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.AngleTransform"), Single)
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.FlipText = CType(resources.GetObject("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.FlipText"), Boolean)
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Margin = CType(resources.GetObject("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Text = resources.GetString("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.Text")
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.TextOrientation = CType(resources.GetObject("RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.ShowUpDown = True
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.TabStop = False
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis.Value = New Date(2015, 7, 2, 9, 5, 20, 614)
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'RadButtonRefresh
        '
        Me.RadButtonRefresh.Image = Global.EichsoftwareClient.My.Resources.Resources.database_refresh
        resources.ApplyResources(Me.RadButtonRefresh, "RadButtonRefresh")
        Me.RadButtonRefresh.Name = "RadButtonRefresh"
        '
        '
        '
        Me.RadButtonRefresh.RootElement.AccessibleDescription = resources.GetString("RadButtonRefresh.RootElement.AccessibleDescription")
        Me.RadButtonRefresh.RootElement.AccessibleName = resources.GetString("RadButtonRefresh.RootElement.AccessibleName")
        Me.RadButtonRefresh.RootElement.Alignment = CType(resources.GetObject("RadButtonRefresh.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonRefresh.RootElement.AngleTransform = CType(resources.GetObject("RadButtonRefresh.RootElement.AngleTransform"), Single)
        Me.RadButtonRefresh.RootElement.FlipText = CType(resources.GetObject("RadButtonRefresh.RootElement.FlipText"), Boolean)
        Me.RadButtonRefresh.RootElement.Margin = CType(resources.GetObject("RadButtonRefresh.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonRefresh.RootElement.Text = resources.GetString("RadButtonRefresh.RootElement.Text")
        Me.RadButtonRefresh.RootElement.TextOrientation = CType(resources.GetObject("RadButtonRefresh.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadCheckBoxLadeAlleEichprozesse
        '
        resources.ApplyResources(Me.RadCheckBoxLadeAlleEichprozesse, "RadCheckBoxLadeAlleEichprozesse")
        Me.RadCheckBoxLadeAlleEichprozesse.Name = "RadCheckBoxLadeAlleEichprozesse"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'RadDateTimePickerFilterMonatLadeAlleEichprozesseVon
        '
        resources.ApplyResources(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon, "RadDateTimePickerFilterMonatLadeAlleEichprozesseVon")
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon.Name = "RadDateTimePickerFilterMonatLadeAlleEichprozesseVon"
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon.ShowUpDown = True
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon.TabStop = False
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon.Value = New Date(2015, 7, 2, 9, 5, 20, 614)
        '
        'RadButtonEichprozessKopierenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichprozessKopierenRHEWA, "RadButtonEichprozessKopierenRHEWA")
        Me.RadButtonEichprozessKopierenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources.report_add
        Me.RadButtonEichprozessKopierenRHEWA.Name = "RadButtonEichprozessKopierenRHEWA"
        '
        '
        '
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.AccessibleDescription = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.AccessibleDescription")
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.AccessibleName = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.AccessibleName")
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Alignment = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.AngleTransform = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.AngleTransform"), Single)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.FlipText = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.FlipText"), Boolean)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadButtonEichprozessAblehnenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichprozessAblehnenRHEWA, "RadButtonEichprozessAblehnenRHEWA")
        Me.RadButtonEichprozessAblehnenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources._error
        Me.RadButtonEichprozessAblehnenRHEWA.Name = "RadButtonEichprozessAblehnenRHEWA"
        '
        '
        '
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.AccessibleDescription = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.AccessibleDescription")
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.AccessibleName = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.AccessibleName")
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Alignment = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.AngleTransform = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.AngleTransform"), Single)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.FlipText = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.FlipText"), Boolean)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadButtonEichprozessGenehmigenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichprozessGenehmigenRHEWA, "RadButtonEichprozessGenehmigenRHEWA")
        Me.RadButtonEichprozessGenehmigenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources.accept
        Me.RadButtonEichprozessGenehmigenRHEWA.Name = "RadButtonEichprozessGenehmigenRHEWA"
        '
        '
        '
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.AccessibleDescription = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.AccessibleDescription")
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.AccessibleName = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.AccessibleName")
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Alignment = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.AngleTransform = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.AngleTransform"), Single)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.FlipText = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.FlipText"), Boolean)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadGridViewRHEWAAlle
        '
        resources.ApplyResources(Me.RadGridViewRHEWAAlle, "RadGridViewRHEWAAlle")
        '
        '
        '
        Me.RadGridViewRHEWAAlle.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewRHEWAAlle.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewRHEWAAlle.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewRHEWAAlle.MasterTemplate.AllowEditRow = False
        Me.RadGridViewRHEWAAlle.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridViewRHEWAAlle.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.RadGridViewRHEWAAlle.Name = "RadGridViewRHEWAAlle"
        '
        '
        '
        Me.RadGridViewRHEWAAlle.RootElement.AccessibleDescription = resources.GetString("RadGridViewRHEWAAlle.RootElement.AccessibleDescription")
        Me.RadGridViewRHEWAAlle.RootElement.AccessibleName = resources.GetString("RadGridViewRHEWAAlle.RootElement.AccessibleName")
        Me.RadGridViewRHEWAAlle.RootElement.Alignment = CType(resources.GetObject("RadGridViewRHEWAAlle.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadGridViewRHEWAAlle.RootElement.AngleTransform = CType(resources.GetObject("RadGridViewRHEWAAlle.RootElement.AngleTransform"), Single)
        Me.RadGridViewRHEWAAlle.RootElement.FlipText = CType(resources.GetObject("RadGridViewRHEWAAlle.RootElement.FlipText"), Boolean)
        Me.RadGridViewRHEWAAlle.RootElement.Margin = CType(resources.GetObject("RadGridViewRHEWAAlle.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadGridViewRHEWAAlle.RootElement.Text = resources.GetString("RadGridViewRHEWAAlle.RootElement.Text")
        Me.RadGridViewRHEWAAlle.RootElement.TextOrientation = CType(resources.GetObject("RadGridViewRHEWAAlle.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadGridViewRHEWAAlle.ShowNoDataText = False
        '
        'RadButtonEichungAnsehenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichungAnsehenRHEWA, "RadButtonEichungAnsehenRHEWA")
        Me.RadButtonEichungAnsehenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources.report_edit
        Me.RadButtonEichungAnsehenRHEWA.Name = "RadButtonEichungAnsehenRHEWA"
        '
        '
        '
        Me.RadButtonEichungAnsehenRHEWA.RootElement.AccessibleDescription = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.AccessibleDescription")
        Me.RadButtonEichungAnsehenRHEWA.RootElement.AccessibleName = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.AccessibleName")
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Alignment = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.AngleTransform = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.AngleTransform"), Single)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.FlipText = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.FlipText"), Boolean)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Text = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.Text")
        Me.RadButtonEichungAnsehenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'FlowLayoutPanel1
        '
        resources.ApplyResources(Me.FlowLayoutPanel1, "FlowLayoutPanel1")
        Me.FlowLayoutPanel1.Controls.Add(Me.RadProgressBar)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        '
        'RadProgressBar
        '
        resources.ApplyResources(Me.RadProgressBar, "RadProgressBar")
        Me.RadProgressBar.Name = "RadProgressBar"
        '
        '
        '
        Me.RadProgressBar.RootElement.AccessibleDescription = resources.GetString("RadProgressBar.RootElement.AccessibleDescription")
        Me.RadProgressBar.RootElement.AccessibleName = resources.GetString("RadProgressBar.RootElement.AccessibleName")
        Me.RadProgressBar.RootElement.Alignment = CType(resources.GetObject("RadProgressBar.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadProgressBar.RootElement.AngleTransform = CType(resources.GetObject("RadProgressBar.RootElement.AngleTransform"), Single)
        Me.RadProgressBar.RootElement.FlipText = CType(resources.GetObject("RadProgressBar.RootElement.FlipText"), Boolean)
        Me.RadProgressBar.RootElement.Margin = CType(resources.GetObject("RadProgressBar.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadProgressBar.RootElement.Text = resources.GetString("RadProgressBar.RootElement.Text")
        Me.RadProgressBar.RootElement.TextOrientation = CType(resources.GetObject("RadProgressBar.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'BackgroundWorkerLoadFromDatabaseRHEWA
        '
        '
        'BackgroundWorkerDownloadFromFTP
        '
        Me.BackgroundWorkerDownloadFromFTP.WorkerReportsProgress = True
        '
        'ucoEichprozessauswahlliste
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "ucoEichprozessauswahlliste"
        CType(Me.RadGridViewAuswahlliste.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewAuswahlliste, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPageEigene.ResumeLayout(False)
        Me.RadPageViewPageEigene.PerformLayout()
        CType(Me.RadButtonNeuStandardwaage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEinstellungen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientUpdateDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientAusblenden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientNeu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientBearbeiten, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPageAlle.ResumeLayout(False)
        Me.RadPageViewPageAlle.PerformLayout()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseBis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxLadeAlleEichprozesse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewRHEWAAlle.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewRHEWAAlle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichungAnsehenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGridViewAuswahlliste As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonClientNeu As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonClientBearbeiten As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonClientAusblenden As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadCheckBoxAusblendenClientGeloeschterDokumente As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents BackgroundWorkerLoadFromDatabase As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPageEigene As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPageAlle As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGridViewRHEWAAlle As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonEichungAnsehenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents BackgroundWorkerLoadFromDatabaseRHEWA As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadButtonEichprozessAblehnenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichprozessGenehmigenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichprozessKopierenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonClientUpdateDatabase As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEinstellungen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadProgressBar As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents BackgroundWorkerDownloadFromFTP As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadButtonRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents RadButtonNeuStandardwaage As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadDateTimePickerFilterMonatLadeAlleEichprozesseVon As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadCheckBoxLadeAlleEichprozesse As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RadDateTimePickerFilterMonatLadeAlleEichprozesseBis As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
