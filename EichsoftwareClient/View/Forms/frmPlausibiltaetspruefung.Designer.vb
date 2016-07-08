<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlausibiltaetspruefung
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.RadTextBoxControlPath = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadButtonOpen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonPruefe = New Telerik.WinControls.UI.RadButton()
        Me.BackgroundWorkerDownloadFromFTP = New System.ComponentModel.BackgroundWorker()
        Me.RadProgressBar = New Telerik.WinControls.UI.RadProgressBar()
        Me.RadButtonAnhang = New Telerik.WinControls.UI.RadButton()
        Me.RadDropDownListWaegebruecke = New Telerik.WinControls.UI.RadDropDownList()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelOption = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.RadGridViewVergleichswerte = New Telerik.WinControls.UI.RadGridView()
        Me.PlausibilitaetDatasourceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RadTextBoxControl1 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadTextBoxControl2 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RadTextBoxControl3 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RadTextBoxControl4 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadTextBoxControl5 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RadTextBoxControl6 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.RadTextBoxControlPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonPruefe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAnhang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownListWaegebruecke, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.RadGridViewVergleichswerte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewVergleichswerte.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PlausibilitaetDatasourceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "XML Konfiguration|*.xml|Access|*.mdb"
        '
        'RadTextBoxControlPath
        '
        Me.RadTextBoxControlPath.Enabled = False
        Me.RadTextBoxControlPath.Location = New System.Drawing.Point(119, 3)
        Me.RadTextBoxControlPath.Name = "RadTextBoxControlPath"
        Me.RadTextBoxControlPath.Size = New System.Drawing.Size(341, 20)
        Me.RadTextBoxControlPath.TabIndex = 0
        '
        'RadButtonOpen
        '
        Me.RadButtonOpen.Location = New System.Drawing.Point(466, 3)
        Me.RadButtonOpen.Name = "RadButtonOpen"
        Me.RadButtonOpen.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonOpen.TabIndex = 1
        Me.RadButtonOpen.Text = "Datei auswählen"
        '
        'RadButtonPruefe
        '
        Me.RadButtonPruefe.Enabled = False
        Me.RadButtonPruefe.Location = New System.Drawing.Point(155, 86)
        Me.RadButtonPruefe.Name = "RadButtonPruefe"
        Me.RadButtonPruefe.Size = New System.Drawing.Size(153, 30)
        Me.RadButtonPruefe.TabIndex = 2
        Me.RadButtonPruefe.Text = "Auf Plausiblität prüfen"
        '
        'BackgroundWorkerDownloadFromFTP
        '
        Me.BackgroundWorkerDownloadFromFTP.WorkerReportsProgress = True
        '
        'RadProgressBar
        '
        Me.RadProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadProgressBar.Location = New System.Drawing.Point(12, 528)
        Me.RadProgressBar.Name = "RadProgressBar"
        Me.RadProgressBar.Size = New System.Drawing.Size(651, 10)
        Me.RadProgressBar.TabIndex = 12
        Me.RadProgressBar.Text = Nothing
        Me.RadProgressBar.Visible = False
        '
        'RadButtonAnhang
        '
        Me.RadButtonAnhang.Location = New System.Drawing.Point(3, 3)
        Me.RadButtonAnhang.Name = "RadButtonAnhang"
        Me.RadButtonAnhang.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonAnhang.TabIndex = 2
        Me.RadButtonAnhang.Text = "Anhang öffnen"
        Me.RadButtonAnhang.Visible = False
        '
        'RadDropDownListWaegebruecke
        '
        RadListDataItem1.Selected = True
        RadListDataItem1.Text = "Wägebrücke 1"
        RadListDataItem2.Text = "Wägebrücke 2"
        RadListDataItem3.Text = "Wägebrücke 3"
        RadListDataItem4.Text = "Wägebrücke 4"
        RadListDataItem5.Text = "Wägebrücke 5"
        RadListDataItem6.Text = "Wägebrücke 6"
        RadListDataItem7.Text = "Wägebrücke 7"
        RadListDataItem8.Text = "Wägebrücke 8"
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem1)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem2)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem3)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem4)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem5)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem6)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem7)
        Me.RadDropDownListWaegebruecke.Items.Add(RadListDataItem8)
        Me.RadDropDownListWaegebruecke.Location = New System.Drawing.Point(15, 92)
        Me.RadDropDownListWaegebruecke.Name = "RadDropDownListWaegebruecke"
        Me.RadDropDownListWaegebruecke.Size = New System.Drawing.Size(125, 22)
        Me.RadDropDownListWaegebruecke.TabIndex = 13
        Me.RadDropDownListWaegebruecke.Text = "Wägebrücke 1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "2. Wägebrücke prüfen"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "1. Pfad auswählen"
        '
        'LabelOption
        '
        Me.LabelOption.AutoSize = True
        Me.LabelOption.Location = New System.Drawing.Point(12, 24)
        Me.LabelOption.Name = "LabelOption"
        Me.LabelOption.Size = New System.Drawing.Size(314, 13)
        Me.LabelOption.TabIndex = 15
        Me.LabelOption.Text = "Option: Anhängte Datei öffnen, ablegen und als Datei auswählen"
        Me.LabelOption.Visible = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.RadButtonAnhang)
        Me.FlowLayoutPanel1.Controls.Add(Me.RadTextBoxControlPath)
        Me.FlowLayoutPanel1.Controls.Add(Me.RadButtonOpen)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(12, 40)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(651, 33)
        Me.FlowLayoutPanel1.TabIndex = 16
        '
        'RadGridViewVergleichswerte
        '
        Me.RadGridViewVergleichswerte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridViewVergleichswerte.AutoSizeRows = True
        Me.RadGridViewVergleichswerte.Location = New System.Drawing.Point(15, 122)
        '
        '
        '
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowColumnChooser = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowColumnReorder = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowEditRow = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AllowRowHeaderContextMenu = False
        Me.RadGridViewVergleichswerte.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        GridViewTextBoxColumn1.FieldName = "Eigenschaft"
        GridViewTextBoxColumn1.HeaderText = "Eigenschaft"
        GridViewTextBoxColumn1.IsAutoGenerated = True
        GridViewTextBoxColumn1.Name = "Eigenschaft"
        GridViewTextBoxColumn1.Width = 212
        GridViewTextBoxColumn2.FieldName = "WertAusConfig"
        GridViewTextBoxColumn2.HeaderText = "Wert aus Config"
        GridViewTextBoxColumn2.IsAutoGenerated = True
        GridViewTextBoxColumn2.Name = "WertAusConfig"
        GridViewTextBoxColumn2.Width = 212
        GridViewTextBoxColumn3.FieldName = "WertAusSoftware"
        GridViewTextBoxColumn3.HeaderText = "Wert aus Software"
        GridViewTextBoxColumn3.IsAutoGenerated = True
        GridViewTextBoxColumn3.Name = "WertAusSoftware"
        GridViewTextBoxColumn3.Width = 202
        Me.RadGridViewVergleichswerte.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3})
        Me.RadGridViewVergleichswerte.MasterTemplate.DataSource = Me.PlausibilitaetDatasourceBindingSource
        Me.RadGridViewVergleichswerte.MasterTemplate.EnableGrouping = False
        Me.RadGridViewVergleichswerte.MasterTemplate.EnableSorting = False
        Me.RadGridViewVergleichswerte.MasterTemplate.ShowFilteringRow = False
        Me.RadGridViewVergleichswerte.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RadGridViewVergleichswerte.Name = "RadGridViewVergleichswerte"
        Me.RadGridViewVergleichswerte.ReadOnly = True
        Me.RadGridViewVergleichswerte.ShowGroupPanel = False
        Me.RadGridViewVergleichswerte.ShowNoDataText = False
        Me.RadGridViewVergleichswerte.Size = New System.Drawing.Size(648, 242)
        Me.RadGridViewVergleichswerte.TabIndex = 17
        Me.RadGridViewVergleichswerte.Text = "RadGridView1"
        '
        'PlausibilitaetDatasourceBindingSource
        '
        Me.PlausibilitaetDatasourceBindingSource.DataSource = GetType(EichsoftwareClient.PlausibilitaetDatasource)
        '
        'RadTextBoxControl1
        '
        Me.RadTextBoxControl1.Location = New System.Drawing.Point(102, 378)
        Me.RadTextBoxControl1.Name = "RadTextBoxControl1"
        Me.RadTextBoxControl1.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl1.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 382)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Justagepunkt 1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 411)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Justagepunkt 2"
        '
        'RadTextBoxControl2
        '
        Me.RadTextBoxControl2.Location = New System.Drawing.Point(102, 407)
        Me.RadTextBoxControl2.Name = "RadTextBoxControl2"
        Me.RadTextBoxControl2.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl2.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(233, 382)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(20, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Kg"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(233, 411)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Kg"
        '
        'RadTextBoxControl3
        '
        Me.RadTextBoxControl3.Location = New System.Drawing.Point(347, 407)
        Me.RadTextBoxControl3.Name = "RadTextBoxControl3"
        Me.RadTextBoxControl3.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl3.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(260, 411)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Analogwert 2"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(260, 382)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Analogwert 1"
        '
        'RadTextBoxControl4
        '
        Me.RadTextBoxControl4.Location = New System.Drawing.Point(347, 378)
        Me.RadTextBoxControl4.Name = "RadTextBoxControl4"
        Me.RadTextBoxControl4.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl4.TabIndex = 24
        '
        'RadTextBoxControl5
        '
        Me.RadTextBoxControl5.Location = New System.Drawing.Point(102, 433)
        Me.RadTextBoxControl5.Name = "RadTextBoxControl5"
        Me.RadTextBoxControl5.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl5.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 437)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Nennlast WZ 2"
        '
        'RadTextBoxControl6
        '
        Me.RadTextBoxControl6.Location = New System.Drawing.Point(347, 433)
        Me.RadTextBoxControl6.Name = "RadTextBoxControl6"
        Me.RadTextBoxControl6.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControl6.TabIndex = 31
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(260, 437)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Justagepunkt 2"
        '
        'frmPlausibiltaetspruefung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(669, 550)
        Me.Controls.Add(Me.RadTextBoxControl6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.RadTextBoxControl5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.RadTextBoxControl3)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.RadTextBoxControl4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.RadTextBoxControl2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.RadTextBoxControl1)
        Me.Controls.Add(Me.RadGridViewVergleichswerte)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.LabelOption)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadDropDownListWaegebruecke)
        Me.Controls.Add(Me.RadProgressBar)
        Me.Controls.Add(Me.RadButtonPruefe)
        Me.Name = "frmPlausibiltaetspruefung"
        Me.Text = "Plausbilitätsprüfung"
        CType(Me.RadTextBoxControlPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonPruefe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAnhang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownListWaegebruecke, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.RadGridViewVergleichswerte.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewVergleichswerte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PlausibilitaetDatasourceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents RadTextBoxControlPath As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadButtonOpen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonPruefe As Telerik.WinControls.UI.RadButton
    Friend WithEvents BackgroundWorkerDownloadFromFTP As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadProgressBar As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents RadButtonAnhang As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadDropDownListWaegebruecke As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelOption As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents RadGridViewVergleichswerte As Telerik.WinControls.UI.RadGridView
    Friend WithEvents PlausibilitaetDatasourceBindingSource As BindingSource
    Friend WithEvents RadTextBoxControl1 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents RadTextBoxControl2 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents RadTextBoxControl3 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents RadTextBoxControl4 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadTextBoxControl5 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label7 As Label
    Friend WithEvents RadTextBoxControl6 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label8 As Label
End Class
