<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmZwischenwerte

    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cmdOK = New Telerik.WinControls.UI.RadButton()
        Me.cmdAbbrechen = New Telerik.WinControls.UI.RadButton()
        Me.UcoZwischenwerte = New EichsoftwareClient.ucoZwischenwerte()
        CType(Me.cmdOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdAbbrechen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(833, 232)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(110, 24)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        '
        'cmdAbbrechen
        '
        Me.cmdAbbrechen.Location = New System.Drawing.Point(949, 232)
        Me.cmdAbbrechen.Name = "cmdAbbrechen"
        Me.cmdAbbrechen.Size = New System.Drawing.Size(110, 24)
        Me.cmdAbbrechen.TabIndex = 1
        Me.cmdAbbrechen.Text = "Abbrechen"
        '
        'UcoZwischenwerte1
        '
        Me.UcoZwischenwerte.Location = New System.Drawing.Point(2, 3)
        Me.UcoZwischenwerte.Name = "UcoZwischenwerte1"
        Me.UcoZwischenwerte.Size = New System.Drawing.Size(1067, 239)
        Me.UcoZwischenwerte.TabIndex = 2
        '
        'frmZwischenwerte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 268)
        Me.Controls.Add(Me.cmdAbbrechen)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.UcoZwischenwerte)
        Me.Name = "frmZwischenwerte"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        CType(Me.cmdOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdAbbrechen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdAbbrechen As Telerik.WinControls.UI.RadButton
    Friend WithEvents UcoZwischenwerte As ucoZwischenwerte
End Class
