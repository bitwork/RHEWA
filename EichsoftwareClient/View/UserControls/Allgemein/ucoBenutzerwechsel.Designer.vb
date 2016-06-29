<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucoBenutzerwechsel
    Inherits System.Windows.Forms.UserControl

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
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButton1
        '
        Me.RadButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.RadButton1.Image = Global.EichsoftwareClient.My.Resources.Resources.user
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.TopRight
        Me.RadButton1.Location = New System.Drawing.Point(0, 0)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(526, 55)
        Me.RadButton1.TabIndex = 2
        Me.RadButton1.Text = "RadButton1"
        Me.RadButton1.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.RadButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.RadButton1.TextWrap = True
        '
        'ucoBenutzerwechsel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadButton1)
        Me.DoubleBuffered = True
        Me.Name = "ucoBenutzerwechsel"
        Me.Size = New System.Drawing.Size(526, 55)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton

End Class
