<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNeueLizenz
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
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControl1 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControl2 = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.RadCheckBox1 = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadCheckBox2 = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(96, 183)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(110, 24)
        Me.RadButton1.TabIndex = 5
        Me.RadButton1.Text = "OK"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(12, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(72, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "HE-Kennung:"
        '
        'RadTextBoxControl1
        '
        Me.RadTextBoxControl1.Location = New System.Drawing.Point(96, 12)
        Me.RadTextBoxControl1.Name = "RadTextBoxControl1"
        Me.RadTextBoxControl1.Size = New System.Drawing.Size(199, 20)
        Me.RadTextBoxControl1.TabIndex = 0
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(12, 76)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(81, 18)
        Me.RadLabel2.TabIndex = 2
        Me.RadLabel2.Text = "Lizenzschlüssel"
        '
        'RadTextBoxControl2
        '
        Me.RadTextBoxControl2.IsReadOnly = True
        Me.RadTextBoxControl2.Location = New System.Drawing.Point(96, 76)
        Me.RadTextBoxControl2.Name = "RadTextBoxControl2"
        Me.RadTextBoxControl2.Size = New System.Drawing.Size(199, 20)
        Me.RadTextBoxControl2.TabIndex = 2
        Me.RadTextBoxControl2.TabStop = False
        '
        'RadCheckBox1
        '
        Me.RadCheckBox1.AutoSize = False
        Me.RadCheckBox1.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.RadCheckBox1.Location = New System.Drawing.Point(12, 125)
        Me.RadCheckBox1.Name = "RadCheckBox1"
        Me.RadCheckBox1.Size = New System.Drawing.Size(283, 18)
        Me.RadCheckBox1.TabIndex = 3
        Me.RadCheckBox1.Text = "RHEWA Lizenz (Sonderrechte)"
        '
        'RadCheckBox2
        '
        Me.RadCheckBox2.AutoSize = False
        Me.RadCheckBox2.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.RadCheckBox2.Location = New System.Drawing.Point(12, 149)
        Me.RadCheckBox2.Name = "RadCheckBox2"
        Me.RadCheckBox2.Size = New System.Drawing.Size(283, 18)
        Me.RadCheckBox2.TabIndex = 4
        Me.RadCheckBox2.Text = "Aktiv (inaktive dürfen sich nicht anmelden)"
        '
        'FrmNeueLizenz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 219)
        Me.Controls.Add(Me.RadCheckBox2)
        Me.Controls.Add(Me.RadCheckBox1)
        Me.Controls.Add(Me.RadButton1)
        Me.Controls.Add(Me.RadTextBoxControl2)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.RadTextBoxControl1)
        Me.Controls.Add(Me.RadLabel1)
        Me.Name = "FrmNeueLizenz"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lizenzverwaltung"
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControl1 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControl2 As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents RadCheckBox1 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadCheckBox2 As Telerik.WinControls.UI.RadCheckBox
End Class

