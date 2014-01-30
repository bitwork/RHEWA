<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucoDummy
    Inherits ucoContent

    'UserControl overrides dispose to clean up the component list.
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
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(438, 301)
        '
        '
        '
        Me.RadScrollablePanel1.RootElement.Padding = New System.Windows.Forms.Padding(1)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(440, 303)
        Me.RadScrollablePanel1.TabIndex = 0
        Me.RadScrollablePanel1.Text = "RadScrollablePanel1"
        '
        'ucoDummy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadScrollablePanel1)
        Me.Name = "ucoDummy"
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel

End Class
