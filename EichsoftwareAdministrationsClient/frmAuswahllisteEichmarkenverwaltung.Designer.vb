<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAuswahllisteEichmarkenverwaltung
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
        Me.RadGridView1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonBearbeiten = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridView1
        '
        Me.RadGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridView1.Location = New System.Drawing.Point(12, 12)
        '
        'RadGridView1
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowEditRow = False
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.Size = New System.Drawing.Size(950, 489)
        Me.RadGridView1.TabIndex = 0
        Me.RadGridView1.Text = "RadGridView1"
        '
        'RadButtonBearbeiten
        '
        Me.RadButtonBearbeiten.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonBearbeiten.Location = New System.Drawing.Point(852, 507)
        Me.RadButtonBearbeiten.Name = "RadButtonBearbeiten"
        '
        '
        '
        Me.RadButtonBearbeiten.RootElement.AccessibleDescription = Nothing
        Me.RadButtonBearbeiten.RootElement.AccessibleName = Nothing
        Me.RadButtonBearbeiten.RootElement.Alignment = System.Drawing.ContentAlignment.TopLeft
        Me.RadButtonBearbeiten.RootElement.AngleTransform = 0.0!
        Me.RadButtonBearbeiten.RootElement.FlipText = False
        Me.RadButtonBearbeiten.RootElement.Margin = New System.Windows.Forms.Padding(0)
        Me.RadButtonBearbeiten.RootElement.Padding = New System.Windows.Forms.Padding(0)
        Me.RadButtonBearbeiten.RootElement.Text = Nothing
        Me.RadButtonBearbeiten.RootElement.TextOrientation = System.Windows.Forms.Orientation.Horizontal
        Me.RadButtonBearbeiten.Size = New System.Drawing.Size(110, 42)
        Me.RadButtonBearbeiten.TabIndex = 7
        Me.RadButtonBearbeiten.Text = "Bearbeiten"
        Me.RadButtonBearbeiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'FrmAuswahllisteEichmarkenverwaltung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(975, 561)
        Me.Controls.Add(Me.RadButtonBearbeiten)
        Me.Controls.Add(Me.RadGridView1)
        Me.Name = "FrmAuswahllisteEichmarkenverwaltung"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Statistik Eichmarkenverwaltung"
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonBearbeiten As Telerik.WinControls.UI.RadButton
End Class

