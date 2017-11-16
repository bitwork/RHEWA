<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEichfehlergrenzen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEichfehlergrenzen))
        Me.UcoEichfehlergrenzen = New EichsoftwareClient.ucoEichfehlergrenzen()
        Me.cmdOK = New Telerik.WinControls.UI.RadButton()
        CType(Me.cmdOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UcoEichfehlergrenzen1
        '
        resources.ApplyResources(Me.UcoEichfehlergrenzen, "UcoEichfehlergrenzen1")
        Me.UcoEichfehlergrenzen.Name = "UcoEichfehlergrenzen1"
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.Name = "cmdOK"
        '
        'frmEichfehlergrenzen
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.UcoEichfehlergrenzen)
        Me.Name = "frmEichfehlergrenzen"
        CType(Me.cmdOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcoEichfehlergrenzen As EichsoftwareClient.ucoEichfehlergrenzen
    Friend WithEvents cmdOK As Telerik.WinControls.UI.RadButton
End Class
