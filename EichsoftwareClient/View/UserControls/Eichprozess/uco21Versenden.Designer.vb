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
        '
        '
        Me.RadLabel1.RootElement.AccessibleDescription = resources.GetString("RadLabel1.RootElement.AccessibleDescription")
        Me.RadLabel1.RootElement.AccessibleName = resources.GetString("RadLabel1.RootElement.AccessibleName")
        Me.RadLabel1.RootElement.Alignment = CType(resources.GetObject("RadLabel1.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadLabel1.RootElement.AngleTransform = CType(resources.GetObject("RadLabel1.RootElement.AngleTransform"), Single)
        Me.RadLabel1.RootElement.FlipText = CType(resources.GetObject("RadLabel1.RootElement.FlipText"), Boolean)
        Me.RadLabel1.RootElement.Margin = CType(resources.GetObject("RadLabel1.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadLabel1.RootElement.Text = resources.GetString("RadLabel1.RootElement.Text")
        Me.RadLabel1.RootElement.TextOrientation = CType(resources.GetObject("RadLabel1.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadTextBoxControlUploadPath
        '
        Me.RadTextBoxControlUploadPath.ReadOnly = True
        resources.ApplyResources(Me.RadTextBoxControlUploadPath, "RadTextBoxControlUploadPath")
        Me.RadTextBoxControlUploadPath.Name = "RadTextBoxControlUploadPath"
        '
        '
        '
        Me.RadTextBoxControlUploadPath.RootElement.AccessibleDescription = resources.GetString("RadTextBoxControlUploadPath.RootElement.AccessibleDescription")
        Me.RadTextBoxControlUploadPath.RootElement.AccessibleName = resources.GetString("RadTextBoxControlUploadPath.RootElement.AccessibleName")
        Me.RadTextBoxControlUploadPath.RootElement.Alignment = CType(resources.GetObject("RadTextBoxControlUploadPath.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadTextBoxControlUploadPath.RootElement.AngleTransform = CType(resources.GetObject("RadTextBoxControlUploadPath.RootElement.AngleTransform"), Single)
        Me.RadTextBoxControlUploadPath.RootElement.FlipText = CType(resources.GetObject("RadTextBoxControlUploadPath.RootElement.FlipText"), Boolean)
        Me.RadTextBoxControlUploadPath.RootElement.Margin = CType(resources.GetObject("RadTextBoxControlUploadPath.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadTextBoxControlUploadPath.RootElement.Text = resources.GetString("RadTextBoxControlUploadPath.RootElement.Text")
        Me.RadTextBoxControlUploadPath.RootElement.TextOrientation = CType(resources.GetObject("RadTextBoxControlUploadPath.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadButtonUploadPath
        '
        resources.ApplyResources(Me.RadButtonUploadPath, "RadButtonUploadPath")
        Me.RadButtonUploadPath.Name = "RadButtonUploadPath"
        '
        '
        '
        Me.RadButtonUploadPath.RootElement.AccessibleDescription = resources.GetString("RadButtonUploadPath.RootElement.AccessibleDescription")
        Me.RadButtonUploadPath.RootElement.AccessibleName = resources.GetString("RadButtonUploadPath.RootElement.AccessibleName")
        Me.RadButtonUploadPath.RootElement.Alignment = CType(resources.GetObject("RadButtonUploadPath.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonUploadPath.RootElement.AngleTransform = CType(resources.GetObject("RadButtonUploadPath.RootElement.AngleTransform"), Single)
        Me.RadButtonUploadPath.RootElement.FlipText = CType(resources.GetObject("RadButtonUploadPath.RootElement.FlipText"), Boolean)
        Me.RadButtonUploadPath.RootElement.Margin = CType(resources.GetObject("RadButtonUploadPath.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonUploadPath.RootElement.Text = resources.GetString("RadButtonUploadPath.RootElement.Text")
        Me.RadButtonUploadPath.RootElement.TextOrientation = CType(resources.GetObject("RadButtonUploadPath.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        '
        'RadButtonAnRhewaSenden
        '
        resources.ApplyResources(Me.RadButtonAnRhewaSenden, "RadButtonAnRhewaSenden")
        Me.RadButtonAnRhewaSenden.Name = "RadButtonAnRhewaSenden"
        '
        '
        '
        Me.RadButtonAnRhewaSenden.RootElement.AccessibleDescription = resources.GetString("RadButtonAnRhewaSenden.RootElement.AccessibleDescription")
        Me.RadButtonAnRhewaSenden.RootElement.AccessibleName = resources.GetString("RadButtonAnRhewaSenden.RootElement.AccessibleName")
        Me.RadButtonAnRhewaSenden.RootElement.Alignment = CType(resources.GetObject("RadButtonAnRhewaSenden.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonAnRhewaSenden.RootElement.AngleTransform = CType(resources.GetObject("RadButtonAnRhewaSenden.RootElement.AngleTransform"), Single)
        Me.RadButtonAnRhewaSenden.RootElement.FlipText = CType(resources.GetObject("RadButtonAnRhewaSenden.RootElement.FlipText"), Boolean)
        Me.RadButtonAnRhewaSenden.RootElement.Margin = CType(resources.GetObject("RadButtonAnRhewaSenden.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonAnRhewaSenden.RootElement.Text = resources.GetString("RadButtonAnRhewaSenden.RootElement.Text")
        Me.RadButtonAnRhewaSenden.RootElement.TextOrientation = CType(resources.GetObject("RadButtonAnRhewaSenden.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
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
        '
        '
        Me.RadProgressBar.RootElement.AccessibleDescription = resources.GetString("RadProgressBar.RootElement.AccessibleDescription")
        Me.RadProgressBar.RootElement.AccessibleName = resources.GetString("RadProgressBar.RootElement.AccessibleName")
        Me.RadProgressBar.RootElement.Alignment = CType(resources.GetObject("RadProgressBar.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadProgressBar.RootElement.AngleTransform = CType(resources.GetObject("RadProgressBar.RootElement.AngleTransform"), Single)
        Me.RadProgressBar.RootElement.FlipText = CType(resources.GetObject("RadProgressBar.RootElement.FlipText"), Boolean)
        Me.RadProgressBar.RootElement.Margin = CType(resources.GetObject("RadProgressBar.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadProgressBar.RootElement.Text = resources.GetString("RadProgressBar.RootElement.Text")
        Me.RadProgressBar.RootElement.TextOrientation = CType(resources.GetObject("RadProgressBar.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
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
