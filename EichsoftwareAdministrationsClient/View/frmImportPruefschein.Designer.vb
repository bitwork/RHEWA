<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportPruefschein
    Inherits Telerik.WinControls.UI.RadForm

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadBrowseEditor1 = New Telerik.WinControls.UI.RadBrowseEditor()
        Me.RadGridView1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonImport = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadBrowseEditor1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadBrowseEditor1
        '
        Me.RadBrowseEditor1.Location = New System.Drawing.Point(0, 13)
        Me.RadBrowseEditor1.Name = "RadBrowseEditor1"
        Me.RadBrowseEditor1.Size = New System.Drawing.Size(391, 20)
        Me.RadBrowseEditor1.TabIndex = 0
        Me.RadBrowseEditor1.Text = "RadBrowseEditor1"
        '
        'RadGridView1
        '
        Me.RadGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadGridView1.Location = New System.Drawing.Point(0, 75)
        '
        '
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowColumnReorder = False
        Me.RadGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.RadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ReadOnly = True
        Me.RadGridView1.Size = New System.Drawing.Size(540, 350)
        Me.RadGridView1.TabIndex = 1
        Me.RadGridView1.Text = "RadGridView1"
        '
        'RadButtonImport
        '
        Me.RadButtonImport.Location = New System.Drawing.Point(411, 8)
        Me.RadButtonImport.Name = "RadButtonImport"
        Me.RadButtonImport.Size = New System.Drawing.Size(110, 38)
        Me.RadButtonImport.TabIndex = 2
        Me.RadButtonImport.Text = "Import"
        '
        'FrmImportPruefschein
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 425)
        Me.Controls.Add(Me.RadButtonImport)
        Me.Controls.Add(Me.RadGridView1)
        Me.Controls.Add(Me.RadBrowseEditor1)
        Me.Name = "FrmImportPruefschein"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmImportPruefschein"
        CType(Me.RadBrowseEditor1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadBrowseEditor1 As Telerik.WinControls.UI.RadBrowseEditor
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonImport As Telerik.WinControls.UI.RadButton
End Class

