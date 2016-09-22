<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPruefscheinnummersuche

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
        Me.RadTextBoxControlPruefscheinnummer = New Telerik.WinControls.UI.RadTextBoxControl()
        Me.Label1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadButtonSuchen = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControlPruefscheinnummer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonSuchen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridView1
        '
        Me.RadGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridView1.Location = New System.Drawing.Point(0, 40)
        '
        'RadGridView1
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowEditRow = False
        Me.RadGridView1.MasterTemplate.EnableAlternatingRowColor = True
        Me.RadGridView1.MasterTemplate.EnableFiltering = True
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ReadOnly = True
        Me.RadGridView1.ShowNoDataText = False
        Me.RadGridView1.Size = New System.Drawing.Size(992, 414)
        Me.RadGridView1.TabIndex = 2
        Me.RadGridView1.Text = "RadGridView1"
        '
        'RadTextBoxControlPruefscheinnummer
        '
        Me.RadTextBoxControlPruefscheinnummer.Location = New System.Drawing.Point(115, 11)
        Me.RadTextBoxControlPruefscheinnummer.Name = "RadTextBoxControlPruefscheinnummer"
        Me.RadTextBoxControlPruefscheinnummer.Size = New System.Drawing.Size(125, 20)
        Me.RadTextBoxControlPruefscheinnummer.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Prüfscheinnummer:"
        '
        'RadButtonSuchen
        '
        Me.RadButtonSuchen.Location = New System.Drawing.Point(246, 10)
        Me.RadButtonSuchen.Name = "RadButtonSuchen"
        Me.RadButtonSuchen.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonSuchen.TabIndex = 5
        Me.RadButtonSuchen.Text = "Suchen"
        '
        'FrmPruefscheinnummersuche
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 454)
        Me.Controls.Add(Me.RadButtonSuchen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadTextBoxControlPruefscheinnummer)
        Me.Controls.Add(Me.RadGridView1)
        Me.Name = "FrmPruefscheinnummersuche"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Prüfscheinnummersuche"
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControlPruefscheinnummer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonSuchen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadTextBoxControlPruefscheinnummer As Telerik.WinControls.UI.RadTextBoxControl
    Friend WithEvents Label1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadButtonSuchen As Telerik.WinControls.UI.RadButton
End Class

