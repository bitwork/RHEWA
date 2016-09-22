<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uco12PruefungUeberlastanzeige

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco12PruefungUeberlastanzeige))
        Me.lblUeberlast = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControlMaxE = New Telerik.WinControls.UI.RadTextBox()
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel()
        Me.lblAnzeige = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.RadCheckBoxUeberlast = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblKg = New Telerik.WinControls.UI.RadLabel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblPflichtfeld1 = New Telerik.WinControls.UI.RadLabel()
        CType(Me.lblUeberlast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControlMaxE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAnzeige, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxUeberlast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUeberlast
        '
        resources.ApplyResources(Me.lblUeberlast, "lblUeberlast")
        Me.lblUeberlast.Name = "lblUeberlast"
        '
        'RadTextBoxControlMaxE
        '
        resources.ApplyResources(Me.RadTextBoxControlMaxE, "RadTextBoxControlMaxE")
        Me.RadTextBoxControlMaxE.Name = "RadTextBoxControlMaxE"
        Me.RadTextBoxControlMaxE.ReadOnly = True
        '
        'RadLabel2
        '
        resources.ApplyResources(Me.RadLabel2, "RadLabel2")
        Me.RadLabel2.Name = "RadLabel2"
        '
        'lblAnzeige
        '
        resources.ApplyResources(Me.lblAnzeige, "lblAnzeige")
        Me.lblAnzeige.Name = "lblAnzeige"
        '
        'RadLabel3
        '
        resources.ApplyResources(Me.RadLabel3, "RadLabel3")
        Me.RadLabel3.Name = "RadLabel3"
        '
        'RadCheckBoxUeberlast
        '
        resources.ApplyResources(Me.RadCheckBoxUeberlast, "RadCheckBoxUeberlast")
        Me.RadCheckBoxUeberlast.Name = "RadCheckBoxUeberlast"
        '
        'lblKg
        '
        resources.ApplyResources(Me.lblKg, "lblKg")
        Me.lblKg.Name = "lblKg"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.EichsoftwareClient.My.Resources.Resources.lock
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'lblPflichtfeld1
        '
        resources.ApplyResources(Me.lblPflichtfeld1, "lblPflichtfeld1")
        Me.lblPflichtfeld1.ForeColor = System.Drawing.Color.Red
        Me.lblPflichtfeld1.Name = "lblPflichtfeld1"
        '
        'uco12PruefungUeberlastanzeige
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblPflichtfeld1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lblKg)
        Me.Controls.Add(Me.RadCheckBoxUeberlast)
        Me.Controls.Add(Me.RadLabel3)
        Me.Controls.Add(Me.lblAnzeige)
        Me.Controls.Add(Me.RadLabel2)
        Me.Controls.Add(Me.RadTextBoxControlMaxE)
        Me.Controls.Add(Me.lblUeberlast)
        Me.Name = "uco12PruefungUeberlastanzeige"
        CType(Me.lblUeberlast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControlMaxE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAnzeige, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxUeberlast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUeberlast As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControlMaxE As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblAnzeige As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadCheckBoxUeberlast As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblKg As Telerik.WinControls.UI.RadLabel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPflichtfeld1 As Telerik.WinControls.UI.RadLabel

End Class
