<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.RadGridViewAuswahlliste = New Telerik.WinControls.UI.RadGridView()
        Me.BackgroundWorkerLoadFromDatabase = New System.ComponentModel.BackgroundWorker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPageEigene = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadButtonClientUpdateDatabase = New Telerik.WinControls.UI.RadButton()
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadButtonClientAusblenden = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonClientNeu = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonClientBearbeiten = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPageAlle = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadButtonEichprozessKopierenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessAblehnenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessGenehmigenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadGridView1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonEichungAnsehenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.BackgroundWorkerLoadFromDatabaseRHEWA = New System.ComponentModel.BackgroundWorker()
        CType(Me.RadGridViewAuswahlliste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewAuswahlliste.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPageEigene.SuspendLayout()
        CType(Me.RadButtonClientUpdateDatabase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientAusblenden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientNeu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonClientBearbeiten, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPageAlle.SuspendLayout()
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichungAnsehenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridViewAuswahlliste
        '
        resources.ApplyResources(Me.RadGridViewAuswahlliste, "RadGridViewAuswahlliste")
        '
        'RadGridViewAuswahlliste
        '
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowEditRow = False
        resources.ApplyResources(Me.RadGridViewAuswahlliste.MasterTemplate, "RadGridViewAuswahlliste")
        Me.RadGridViewAuswahlliste.Name = "RadGridViewAuswahlliste"
        '
        '
        '
        Me.RadGridViewAuswahlliste.RootElement.AccessibleDescription = resources.GetString("RadGridViewAuswahlliste.RootElement.AccessibleDescription")
        Me.RadGridViewAuswahlliste.RootElement.AccessibleName = resources.GetString("RadGridViewAuswahlliste.RootElement.AccessibleName")
        Me.RadGridViewAuswahlliste.RootElement.Alignment = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadGridViewAuswahlliste.RootElement.AngleTransform = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.AngleTransform"), Single)
        Me.RadGridViewAuswahlliste.RootElement.FlipText = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.FlipText"), Boolean)
        Me.RadGridViewAuswahlliste.RootElement.KeyTip = resources.GetString("RadGridViewAuswahlliste.RootElement.KeyTip")
        Me.RadGridViewAuswahlliste.RootElement.Margin = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadGridViewAuswahlliste.RootElement.Padding = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadGridViewAuswahlliste.RootElement.Text = resources.GetString("RadGridViewAuswahlliste.RootElement.Text")
        Me.RadGridViewAuswahlliste.RootElement.TextOrientation = CType(resources.GetObject("RadGridViewAuswahlliste.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadGridViewAuswahlliste.RootElement.ToolTipText = resources.GetString("RadGridViewAuswahlliste.RootElement.ToolTipText")
        Me.RadGridViewAuswahlliste.ShowGroupPanel = False
        Me.RadGridViewAuswahlliste.ShowNoDataText = False
        '
        'BackgroundWorkerLoadFromDatabase
        '
        '
        'RadPageView1
        '
        resources.ApplyResources(Me.RadPageView1, "RadPageView1")
        Me.RadPageView1.Controls.Add(Me.RadPageViewPageEigene)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPageAlle)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = resources.GetString("RadPageView1.RootElement.AccessibleDescription")
        Me.RadPageView1.RootElement.AccessibleName = resources.GetString("RadPageView1.RootElement.AccessibleName")
        Me.RadPageView1.RootElement.Alignment = CType(resources.GetObject("RadPageView1.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadPageView1.RootElement.AngleTransform = CType(resources.GetObject("RadPageView1.RootElement.AngleTransform"), Single)
        Me.RadPageView1.RootElement.FlipText = CType(resources.GetObject("RadPageView1.RootElement.FlipText"), Boolean)
        Me.RadPageView1.RootElement.KeyTip = resources.GetString("RadPageView1.RootElement.KeyTip")
        Me.RadPageView1.RootElement.Margin = CType(resources.GetObject("RadPageView1.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadPageView1.RootElement.Padding = CType(resources.GetObject("RadPageView1.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadPageView1.RootElement.Text = resources.GetString("RadPageView1.RootElement.Text")
        Me.RadPageView1.RootElement.TextOrientation = CType(resources.GetObject("RadPageView1.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadPageView1.RootElement.ToolTipText = resources.GetString("RadPageView1.RootElement.ToolTipText")
        Me.RadPageView1.SelectedPage = Me.RadPageViewPageEigene
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(0).GetChildAt(1), Telerik.WinControls.UI.StripViewButtonsPanel).Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadPageViewPageEigene
        '
        resources.ApplyResources(Me.RadPageViewPageEigene, "RadPageViewPageEigene")
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientUpdateDatabase)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadGridViewAuswahlliste)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadCheckBoxAusblendenClientGeloeschterDokumente)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientAusblenden)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientNeu)
        Me.RadPageViewPageEigene.Controls.Add(Me.RadButtonClientBearbeiten)
        Me.RadPageViewPageEigene.Name = "RadPageViewPageEigene"
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
        Me.RadButtonClientUpdateDatabase.RootElement.KeyTip = resources.GetString("RadButtonClientUpdateDatabase.RootElement.KeyTip")
        Me.RadButtonClientUpdateDatabase.RootElement.Margin = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientUpdateDatabase.RootElement.Padding = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonClientUpdateDatabase.RootElement.Text = resources.GetString("RadButtonClientUpdateDatabase.RootElement.Text")
        Me.RadButtonClientUpdateDatabase.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientUpdateDatabase.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientUpdateDatabase.RootElement.ToolTipText = resources.GetString("RadButtonClientUpdateDatabase.RootElement.ToolTipText")
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
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.KeyTip = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.KeyTip")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Margin = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Padding = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Text = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.Text")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.TextOrientation = CType(resources.GetObject("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.ToolTipText = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.RootElement.ToolTipText")
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
        Me.RadButtonClientAusblenden.RootElement.KeyTip = resources.GetString("RadButtonClientAusblenden.RootElement.KeyTip")
        Me.RadButtonClientAusblenden.RootElement.Margin = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientAusblenden.RootElement.Padding = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonClientAusblenden.RootElement.Text = resources.GetString("RadButtonClientAusblenden.RootElement.Text")
        Me.RadButtonClientAusblenden.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientAusblenden.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientAusblenden.RootElement.ToolTipText = resources.GetString("RadButtonClientAusblenden.RootElement.ToolTipText")
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
        Me.RadButtonClientNeu.RootElement.KeyTip = resources.GetString("RadButtonClientNeu.RootElement.KeyTip")
        Me.RadButtonClientNeu.RootElement.Margin = CType(resources.GetObject("RadButtonClientNeu.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientNeu.RootElement.Padding = CType(resources.GetObject("RadButtonClientNeu.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonClientNeu.RootElement.Text = resources.GetString("RadButtonClientNeu.RootElement.Text")
        Me.RadButtonClientNeu.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientNeu.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientNeu.RootElement.ToolTipText = resources.GetString("RadButtonClientNeu.RootElement.ToolTipText")
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
        Me.RadButtonClientBearbeiten.RootElement.KeyTip = resources.GetString("RadButtonClientBearbeiten.RootElement.KeyTip")
        Me.RadButtonClientBearbeiten.RootElement.Margin = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonClientBearbeiten.RootElement.Padding = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonClientBearbeiten.RootElement.Text = resources.GetString("RadButtonClientBearbeiten.RootElement.Text")
        Me.RadButtonClientBearbeiten.RootElement.TextOrientation = CType(resources.GetObject("RadButtonClientBearbeiten.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonClientBearbeiten.RootElement.ToolTipText = resources.GetString("RadButtonClientBearbeiten.RootElement.ToolTipText")
        '
        'RadPageViewPageAlle
        '
        resources.ApplyResources(Me.RadPageViewPageAlle, "RadPageViewPageAlle")
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessKopierenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessAblehnenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessGenehmigenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadGridView1)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichungAnsehenRHEWA)
        Me.RadPageViewPageAlle.Name = "RadPageViewPageAlle"
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
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.KeyTip = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.KeyTip")
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Padding = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessKopierenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonEichprozessKopierenRHEWA.RootElement.ToolTipText = resources.GetString("RadButtonEichprozessKopierenRHEWA.RootElement.ToolTipText")
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
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.KeyTip = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.KeyTip")
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Padding = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessAblehnenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonEichprozessAblehnenRHEWA.RootElement.ToolTipText = resources.GetString("RadButtonEichprozessAblehnenRHEWA.RootElement.ToolTipText")
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
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.KeyTip = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.KeyTip")
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Padding = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.Text = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.Text")
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichprozessGenehmigenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonEichprozessGenehmigenRHEWA.RootElement.ToolTipText = resources.GetString("RadButtonEichprozessGenehmigenRHEWA.RootElement.ToolTipText")
        '
        'RadGridView1
        '
        resources.ApplyResources(Me.RadGridView1, "RadGridView1")
        '
        '
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowEditRow = False
        Me.RadGridView1.MasterTemplate.Caption = resources.GetString("RadGridView1.MasterTemplate.Caption")
        Me.RadGridView1.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridView1.Name = "RadGridView1"
        '
        '
        '
        Me.RadGridView1.RootElement.AccessibleDescription = resources.GetString("RadGridView1.RootElement.AccessibleDescription")
        Me.RadGridView1.RootElement.AccessibleName = resources.GetString("RadGridView1.RootElement.AccessibleName")
        Me.RadGridView1.RootElement.Alignment = CType(resources.GetObject("RadGridView1.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadGridView1.RootElement.AngleTransform = CType(resources.GetObject("RadGridView1.RootElement.AngleTransform"), Single)
        Me.RadGridView1.RootElement.FlipText = CType(resources.GetObject("RadGridView1.RootElement.FlipText"), Boolean)
        Me.RadGridView1.RootElement.KeyTip = resources.GetString("RadGridView1.RootElement.KeyTip")
        Me.RadGridView1.RootElement.Margin = CType(resources.GetObject("RadGridView1.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadGridView1.RootElement.Padding = CType(resources.GetObject("RadGridView1.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadGridView1.RootElement.Text = resources.GetString("RadGridView1.RootElement.Text")
        Me.RadGridView1.RootElement.TextOrientation = CType(resources.GetObject("RadGridView1.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadGridView1.RootElement.ToolTipText = resources.GetString("RadGridView1.RootElement.ToolTipText")
        Me.RadGridView1.ShowNoDataText = False
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
        Me.RadButtonEichungAnsehenRHEWA.RootElement.KeyTip = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.KeyTip")
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Margin = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Padding = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.Text = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.Text")
        Me.RadButtonEichungAnsehenRHEWA.RootElement.TextOrientation = CType(resources.GetObject("RadButtonEichungAnsehenRHEWA.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonEichungAnsehenRHEWA.RootElement.ToolTipText = resources.GetString("RadButtonEichungAnsehenRHEWA.RootElement.ToolTipText")
        '
        'BackgroundWorkerLoadFromDatabaseRHEWA
        '
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
        CType(Me.RadButtonClientUpdateDatabase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientAusblenden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientNeu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonClientBearbeiten, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPageAlle.ResumeLayout(False)
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichungAnsehenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonEichungAnsehenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents BackgroundWorkerLoadFromDatabaseRHEWA As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadButtonEichprozessAblehnenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichprozessGenehmigenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichprozessKopierenRHEWA As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonClientUpdateDatabase As Telerik.WinControls.UI.RadButton

End Class
