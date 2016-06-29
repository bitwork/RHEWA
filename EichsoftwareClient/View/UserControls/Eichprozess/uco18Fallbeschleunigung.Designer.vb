<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uco18Fallbeschleunigung
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco18Fallbeschleunigung))
        Me.lblBeschreibung = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel()
        Me.RadCheckBoxSchwerkraft = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadTextBoxControlG = New Telerik.WinControls.UI.RadTextBox()
        Me.lblPflichtfeld1 = New System.Windows.Forms.Label()
        Me.lblPflichtfeld2 = New System.Windows.Forms.Label()
        CType(Me.lblBeschreibung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxSchwerkraft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControlG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblBeschreibung
        '
        resources.ApplyResources(Me.lblBeschreibung, "lblBeschreibung")
        Me.lblBeschreibung.Name = "lblBeschreibung"
        '
        '
        '
        Me.lblBeschreibung.RootElement.AccessibleDescription = Nothing
        Me.lblBeschreibung.RootElement.AccessibleName = Nothing
        '
        'RadLabel1
        '
        resources.ApplyResources(Me.RadLabel1, "RadLabel1")
        Me.RadLabel1.Name = "RadLabel1"
        '
        '
        '
        Me.RadLabel1.RootElement.AccessibleDescription = Nothing
        Me.RadLabel1.RootElement.AccessibleName = Nothing
        '
        'RadLabel3
        '
        resources.ApplyResources(Me.RadLabel3, "RadLabel3")
        Me.RadLabel3.Name = "RadLabel3"
        '
        '
        '
        Me.RadLabel3.RootElement.AccessibleDescription = Nothing
        Me.RadLabel3.RootElement.AccessibleName = Nothing
        '
        'RadCheckBoxSchwerkraft
        '
        resources.ApplyResources(Me.RadCheckBoxSchwerkraft, "RadCheckBoxSchwerkraft")
        Me.RadCheckBoxSchwerkraft.Name = "RadCheckBoxSchwerkraft"
        '
        '
        '
        Me.RadCheckBoxSchwerkraft.RootElement.AccessibleDescription = Nothing
        Me.RadCheckBoxSchwerkraft.RootElement.AccessibleName = Nothing
        '
        'RadTextBoxControlG
        '
        resources.ApplyResources(Me.RadTextBoxControlG, "RadTextBoxControlG")
        Me.RadTextBoxControlG.MaxLength = 10
        Me.RadTextBoxControlG.Name = "RadTextBoxControlG"
        '
        'lblPflichtfeld1
        '
        resources.ApplyResources(Me.lblPflichtfeld1, "lblPflichtfeld1")
        Me.lblPflichtfeld1.ForeColor = System.Drawing.Color.Red
        Me.lblPflichtfeld1.Name = "lblPflichtfeld1"
        '
        'lblPflichtfeld2
        '
        resources.ApplyResources(Me.lblPflichtfeld2, "lblPflichtfeld2")
        Me.lblPflichtfeld2.ForeColor = System.Drawing.Color.Red
        Me.lblPflichtfeld2.Name = "lblPflichtfeld2"
        '
        'uco18Fallbeschleunigung
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblPflichtfeld2)
        Me.Controls.Add(Me.lblPflichtfeld1)
        Me.Controls.Add(Me.RadTextBoxControlG)
        Me.Controls.Add(Me.RadCheckBoxSchwerkraft)
        Me.Controls.Add(Me.RadLabel3)
        Me.Controls.Add(Me.RadLabel1)
        Me.Controls.Add(Me.lblBeschreibung)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "uco18Fallbeschleunigung"
        CType(Me.lblBeschreibung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxSchwerkraft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControlG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblBeschreibung As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadCheckBoxSchwerkraft As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadTextBoxControlG As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblPflichtfeld1 As System.Windows.Forms.Label
    Friend WithEvents lblPflichtfeld2 As System.Windows.Forms.Label

End Class
