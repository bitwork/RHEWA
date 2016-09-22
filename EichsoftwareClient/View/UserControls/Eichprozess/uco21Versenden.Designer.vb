<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Uco21Versenden

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Uco21Versenden))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControlUploadPath = New Telerik.WinControls.UI.RadTextBox()
        Me.RadButtonUploadPath = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonAnRhewaSenden = New Telerik.WinControls.UI.RadButton()
        Me.BackgroundWorkerUploadFTP = New System.ComponentModel.BackgroundWorker()
        Me.RadProgressBar = New Telerik.WinControls.UI.RadProgressBar()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControlUploadPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonUploadPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAnRhewaSenden, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        '
        'RadLabel1
        '
        resources.ApplyResources(Me.RadLabel1, "RadLabel1")
        Me.RadLabel1.Name = "RadLabel1"
        '
        'RadTextBoxControlUploadPath
        '
        resources.ApplyResources(Me.RadTextBoxControlUploadPath, "RadTextBoxControlUploadPath")
        Me.RadTextBoxControlUploadPath.Name = "RadTextBoxControlUploadPath"
        Me.RadTextBoxControlUploadPath.ReadOnly = True
        '
        'RadButtonUploadPath
        '
        resources.ApplyResources(Me.RadButtonUploadPath, "RadButtonUploadPath")
        Me.RadButtonUploadPath.Name = "RadButtonUploadPath"
        '
        'RadButtonAnRhewaSenden
        '
        resources.ApplyResources(Me.RadButtonAnRhewaSenden, "RadButtonAnRhewaSenden")
        Me.RadButtonAnRhewaSenden.Name = "RadButtonAnRhewaSenden"
        '
        'BackgroundWorkerUploadFTP
        '
        Me.BackgroundWorkerUploadFTP.WorkerReportsProgress = True
        '
        'RadProgressBar
        '
        resources.ApplyResources(Me.RadProgressBar, "RadProgressBar")
        Me.RadProgressBar.Name = "RadProgressBar"
        '
        'Uco21Versenden
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadProgressBar)
        Me.Controls.Add(Me.RadButtonAnRhewaSenden)
        Me.Controls.Add(Me.RadButtonUploadPath)
        Me.Controls.Add(Me.RadTextBoxControlUploadPath)
        Me.Controls.Add(Me.RadLabel1)
        Me.Name = "Uco21Versenden"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControlUploadPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonUploadPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAnRhewaSenden, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadProgressBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControlUploadPath As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadButtonUploadPath As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonAnRhewaSenden As Telerik.WinControls.UI.RadButton
    Friend WithEvents BackgroundWorkerUploadFTP As System.ComponentModel.BackgroundWorker
    Friend WithEvents RadProgressBar As Telerik.WinControls.UI.RadProgressBar

End Class
