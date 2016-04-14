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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
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
        Me.RadGridViewRHEWAAlle = New Telerik.WinControls.UI.RadGridView()
        Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadButtonEichprozessKopierenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessAblehnenRHEWA = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichprozessGenehmigenRHEWA = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.RadGridViewRHEWAAlle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewRHEWAAlle.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowColumnChooser = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowEditRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowRowHeaderContextMenu = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowSearchRow = True
        Me.RadGridViewAuswahlliste.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridViewAuswahlliste.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RadGridViewAuswahlliste.Name = "RadGridViewAuswahlliste"
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
        Me.RadPageView1.SelectedPage = Me.RadPageViewPageEigene
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
        Me.RadPageViewPageEigene.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        resources.ApplyResources(Me.RadPageViewPageEigene, "RadPageViewPageEigene")
        Me.RadPageViewPageEigene.Name = "RadPageViewPageEigene"
        '
        'RadButtonNeuStandardwaage
        '
        resources.ApplyResources(Me.RadButtonNeuStandardwaage, "RadButtonNeuStandardwaage")
        Me.RadButtonNeuStandardwaage.Image = Global.EichsoftwareClient.My.Resources.Resources.report_add
        Me.RadButtonNeuStandardwaage.Name = "RadButtonNeuStandardwaage"
        Me.RadButtonNeuStandardwaage.TextWrap = True
        '
        'RadButtonEinstellungen
        '
        resources.ApplyResources(Me.RadButtonEinstellungen, "RadButtonEinstellungen")
        Me.RadButtonEinstellungen.BackColor = System.Drawing.Color.Transparent
        Me.RadButtonEinstellungen.Image = Global.EichsoftwareClient.My.Resources.Resources.cog
        Me.RadButtonEinstellungen.Name = "RadButtonEinstellungen"
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
        Me.RadButtonClientUpdateDatabase.TextWrap = True
        '
        'RadCheckBoxAusblendenClientGeloeschterDokumente
        '
        resources.ApplyResources(Me.RadCheckBoxAusblendenClientGeloeschterDokumente, "RadCheckBoxAusblendenClientGeloeschterDokumente")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.Name = "RadCheckBoxAusblendenClientGeloeschterDokumente"
        '
        'RadButtonClientAusblenden
        '
        resources.ApplyResources(Me.RadButtonClientAusblenden, "RadButtonClientAusblenden")
        Me.RadButtonClientAusblenden.Image = Global.EichsoftwareClient.My.Resources.Resources.report_delete
        Me.RadButtonClientAusblenden.Name = "RadButtonClientAusblenden"
        Me.RadButtonClientAusblenden.TextWrap = True
        '
        'RadButtonClientNeu
        '
        resources.ApplyResources(Me.RadButtonClientNeu, "RadButtonClientNeu")
        Me.RadButtonClientNeu.Image = Global.EichsoftwareClient.My.Resources.Resources.report_add
        Me.RadButtonClientNeu.Name = "RadButtonClientNeu"
        Me.RadButtonClientNeu.TextWrap = True
        '
        'RadButtonClientBearbeiten
        '
        resources.ApplyResources(Me.RadButtonClientBearbeiten, "RadButtonClientBearbeiten")
        Me.RadButtonClientBearbeiten.Image = Global.EichsoftwareClient.My.Resources.Resources.report_edit
        Me.RadButtonClientBearbeiten.Name = "RadButtonClientBearbeiten"
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
        Me.RadPageViewPageAlle.Controls.Add(Me.RadGridViewRHEWAAlle)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessKopierenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessAblehnenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichprozessGenehmigenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.RadButtonEichungAnsehenRHEWA)
        Me.RadPageViewPageAlle.Controls.Add(Me.FlowLayoutPanel1)
        Me.RadPageViewPageAlle.ItemSize = New System.Drawing.SizeF(35.0!, 28.0!)
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
        Me.RadGridViewRHEWAAlle.MasterTemplate.AllowSearchRow = True
        Me.RadGridViewRHEWAAlle.MasterTemplate.ShowGroupedColumns = True
        Me.RadGridViewRHEWAAlle.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.RadGridViewRHEWAAlle.Name = "RadGridViewRHEWAAlle"
        Me.RadGridViewRHEWAAlle.ShowNoDataText = False
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
        'RadButtonEichprozessAblehnenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichprozessAblehnenRHEWA, "RadButtonEichprozessAblehnenRHEWA")
        Me.RadButtonEichprozessAblehnenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources._error
        Me.RadButtonEichprozessAblehnenRHEWA.Name = "RadButtonEichprozessAblehnenRHEWA"
        '
        'RadButtonEichprozessGenehmigenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichprozessGenehmigenRHEWA, "RadButtonEichprozessGenehmigenRHEWA")
        Me.RadButtonEichprozessGenehmigenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources.accept
        Me.RadButtonEichprozessGenehmigenRHEWA.Name = "RadButtonEichprozessGenehmigenRHEWA"
        '
        'RadButtonEichungAnsehenRHEWA
        '
        resources.ApplyResources(Me.RadButtonEichungAnsehenRHEWA, "RadButtonEichungAnsehenRHEWA")
        Me.RadButtonEichungAnsehenRHEWA.Image = Global.EichsoftwareClient.My.Resources.Resources.report_edit
        Me.RadButtonEichungAnsehenRHEWA.Name = "RadButtonEichungAnsehenRHEWA"
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
        CType(Me.RadGridViewRHEWAAlle.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewRHEWAAlle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDateTimePickerFilterMonatLadeAlleEichprozesseVon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessKopierenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessAblehnenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichprozessGenehmigenRHEWA, System.ComponentModel.ISupportInitialize).EndInit()
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
