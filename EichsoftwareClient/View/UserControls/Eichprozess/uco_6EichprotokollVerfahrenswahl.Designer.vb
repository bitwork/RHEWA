<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uco_6EichprotokollVerfahrenswahl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_6EichprotokollVerfahrenswahl))
        Me.RadRadioButtonNormalien = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButtonStaffelverfahren = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButtonFahrzeugwaagen = New Telerik.WinControls.UI.RadRadioButton()
        CType(Me.RadRadioButtonNormalien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButtonStaffelverfahren, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButtonFahrzeugwaagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadRadioButtonNormalien
        '
        Me.RadRadioButtonNormalien.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.RadRadioButtonNormalien, "RadRadioButtonNormalien")
        Me.RadRadioButtonNormalien.Name = "RadRadioButtonNormalien"
        Me.RadRadioButtonNormalien.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadRadioButtonStaffelverfahren
        '
        resources.ApplyResources(Me.RadRadioButtonStaffelverfahren, "RadRadioButtonStaffelverfahren")
        Me.RadRadioButtonStaffelverfahren.Name = "RadRadioButtonStaffelverfahren"
        '
        'RadRadioButtonFahrzeugwaagen
        '
        resources.ApplyResources(Me.RadRadioButtonFahrzeugwaagen, "RadRadioButtonFahrzeugwaagen")
        Me.RadRadioButtonFahrzeugwaagen.Name = "RadRadioButtonFahrzeugwaagen"
        '
        'uco_6EichprotokollVerfahrenswahl
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadRadioButtonFahrzeugwaagen)
        Me.Controls.Add(Me.RadRadioButtonStaffelverfahren)
        Me.Controls.Add(Me.RadRadioButtonNormalien)
        Me.Name = "uco_6EichprotokollVerfahrenswahl"
        CType(Me.RadRadioButtonNormalien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButtonStaffelverfahren, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButtonFahrzeugwaagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadRadioButtonNormalien As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButtonStaffelverfahren As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButtonFahrzeugwaagen As Telerik.WinControls.UI.RadRadioButton

End Class
