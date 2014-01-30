<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLizenz
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
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControl1 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadTextBoxControl2 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 13)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(36, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Name"
        '
        'RadTextBoxControl1
        '
        Me.RadTextBoxControl1.Location = New System.Drawing.Point(97, 13)
        Me.RadTextBoxControl1.Name = "RadTextBoxControl1"
        Me.RadTextBoxControl1.Size = New System.Drawing.Size(199, 20)
        Me.RadTextBoxControl1.TabIndex = 0
        '
        'RadTextBoxControl2
        '
        Me.RadTextBoxControl2.Location = New System.Drawing.Point(97, 39)
        Me.RadTextBoxControl2.Name = "RadTextBoxControl2"
        Me.RadTextBoxControl2.Size = New System.Drawing.Size(199, 20)
        Me.RadTextBoxControl2.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(13, 39)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(37, 18)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "Lizenz"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(97, 85)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(110, 24)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "OK"
        '
        'FrmLizenz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 109)
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.RadTextBoxControl2)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.RadTextBoxControl1)
        Me.Controls.Add(Me.RadLabel1)
        Me.Name = "FrmLizenz"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lizenz"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControl1 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadTextBoxControl2 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
End Class

