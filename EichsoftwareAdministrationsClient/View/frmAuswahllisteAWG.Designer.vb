<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAuswahllisteAWG
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
        Me.RadButtonBearbeiten = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonNeu = New Telerik.WinControls.UI.RadButton()
        Me.RadGridViewAuswahlliste = New Telerik.WinControls.UI.RadGridView()
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonNeu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewAuswahlliste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewAuswahlliste.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButtonBearbeiten
        '
        Me.RadButtonBearbeiten.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonBearbeiten.Location = New System.Drawing.Point(490, 489)
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
        Me.RadButtonBearbeiten.TabIndex = 5
        Me.RadButtonBearbeiten.Text = "Bearbeiten"
        Me.RadButtonBearbeiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'RadButtonNeu
        '
        Me.RadButtonNeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonNeu.Location = New System.Drawing.Point(606, 489)
        Me.RadButtonNeu.Name = "RadButtonNeu"
        '
        '
        '
        Me.RadButtonNeu.RootElement.AccessibleDescription = Nothing
        Me.RadButtonNeu.RootElement.AccessibleName = Nothing
        Me.RadButtonNeu.RootElement.Alignment = System.Drawing.ContentAlignment.TopLeft
        Me.RadButtonNeu.RootElement.AngleTransform = 0.0!
        Me.RadButtonNeu.RootElement.FlipText = False
        Me.RadButtonNeu.RootElement.Margin = New System.Windows.Forms.Padding(0)
        Me.RadButtonNeu.RootElement.Padding = New System.Windows.Forms.Padding(0)
        Me.RadButtonNeu.RootElement.Text = Nothing
        Me.RadButtonNeu.RootElement.TextOrientation = System.Windows.Forms.Orientation.Horizontal
        Me.RadButtonNeu.Size = New System.Drawing.Size(110, 42)
        Me.RadButtonNeu.TabIndex = 4
        Me.RadButtonNeu.Text = "Neu"
        Me.RadButtonNeu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'RadGridViewAuswahlliste
        '
        Me.RadGridViewAuswahlliste.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridViewAuswahlliste.Location = New System.Drawing.Point(12, 12)
        '
        'RadGridViewAuswahlliste
        '
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewAuswahlliste.MasterTemplate.AllowEditRow = False
        Me.RadGridViewAuswahlliste.MasterTemplate.EnableAlternatingRowColor = True
        Me.RadGridViewAuswahlliste.MasterTemplate.EnableFiltering = True
        Me.RadGridViewAuswahlliste.Name = "RadGridViewAuswahlliste"
        '
        '
        '
        Me.RadGridViewAuswahlliste.RootElement.AccessibleDescription = Nothing
        Me.RadGridViewAuswahlliste.RootElement.AccessibleName = Nothing
        Me.RadGridViewAuswahlliste.RootElement.Alignment = System.Drawing.ContentAlignment.TopLeft
        Me.RadGridViewAuswahlliste.RootElement.AngleTransform = 0.0!
        Me.RadGridViewAuswahlliste.RootElement.FlipText = False
        Me.RadGridViewAuswahlliste.RootElement.Margin = New System.Windows.Forms.Padding(0)
        Me.RadGridViewAuswahlliste.RootElement.Padding = New System.Windows.Forms.Padding(0)
        Me.RadGridViewAuswahlliste.RootElement.Text = Nothing
        Me.RadGridViewAuswahlliste.RootElement.TextOrientation = System.Windows.Forms.Orientation.Horizontal
        Me.RadGridViewAuswahlliste.ShowGroupPanel = False
        Me.RadGridViewAuswahlliste.ShowNoDataText = False
        Me.RadGridViewAuswahlliste.Size = New System.Drawing.Size(704, 461)
        Me.RadGridViewAuswahlliste.TabIndex = 3
        Me.RadGridViewAuswahlliste.Text = "RadGridView1"
        '
        'FrmAuswahllisteAWG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 543)
        Me.Controls.Add(Me.RadButtonBearbeiten)
        Me.Controls.Add(Me.RadButtonNeu)
        Me.Controls.Add(Me.RadGridViewAuswahlliste)
        Me.Name = "FrmAuswahllisteAWG"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Auswahlliste Auswertegeräte"
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonNeu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewAuswahlliste.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewAuswahlliste, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadButtonBearbeiten As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonNeu As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGridViewAuswahlliste As Telerik.WinControls.UI.RadGridView
End Class

