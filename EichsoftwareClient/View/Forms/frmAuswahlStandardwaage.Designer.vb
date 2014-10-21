<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAuswahlStandardwaage
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
        Me.RadGridViewStandardwaagen = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonZuordnen = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGridViewStandardwaagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridViewStandardwaagen.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonZuordnen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridViewStandardwaagen
        '
        Me.RadGridViewStandardwaagen.Location = New System.Drawing.Point(0, 3)
        '
        'RadGridViewStandardwaagen
        '
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowAddNewRow = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowColumnChooser = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowColumnReorder = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowDeleteRow = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowDragToGroup = False
        Me.RadGridViewStandardwaagen.MasterTemplate.AllowEditRow = False
        Me.RadGridViewStandardwaagen.MasterTemplate.EnableFiltering = True
        Me.RadGridViewStandardwaagen.Name = "RadGridViewStandardwaagen"
        Me.RadGridViewStandardwaagen.Size = New System.Drawing.Size(1142, 521)
        Me.RadGridViewStandardwaagen.TabIndex = 0
        Me.RadGridViewStandardwaagen.Text = "RadGridView1"
        '
        'RadButtonZuordnen
        '
        Me.RadButtonZuordnen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonZuordnen.Location = New System.Drawing.Point(979, 530)
        Me.RadButtonZuordnen.Name = "RadButtonZuordnen"
        Me.RadButtonZuordnen.Size = New System.Drawing.Size(163, 24)
        Me.RadButtonZuordnen.TabIndex = 5
        Me.RadButtonZuordnen.Text = "Vorgang erzeugen"
        '
        'FrmAuswahlStandardwaage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1145, 557)
        Me.Controls.Add(Me.RadButtonZuordnen)
        Me.Controls.Add(Me.RadGridViewStandardwaagen)
        Me.Name = "FrmAuswahlStandardwaage"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Auswahl einer Standardwaage"
        CType(Me.RadGridViewStandardwaagen.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridViewStandardwaagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonZuordnen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGridViewStandardwaagen As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonZuordnen As Telerik.WinControls.UI.RadButton
End Class

