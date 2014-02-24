<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEinstellungen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEinstellungen))
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadDateTimePickerEnd = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadDateTimePickerStart = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadDateTimePickerSince = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadRadioButtonSyncZwischen = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButtonSyncSeit = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadRadioButtonSyncAlles = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadButtonOK = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonAbbrechen = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadDateTimePickerEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDateTimePickerStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDateTimePickerSince, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButtonSyncZwischen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButtonSyncSeit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButtonSyncAlles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        resources.ApplyResources(Me.RadGroupBox1, "RadGroupBox1")
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadDateTimePickerEnd)
        Me.RadGroupBox1.Controls.Add(Me.RadDateTimePickerStart)
        Me.RadGroupBox1.Controls.Add(Me.RadDateTimePickerSince)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.RadRadioButtonSyncZwischen)
        Me.RadGroupBox1.Controls.Add(Me.RadRadioButtonSyncSeit)
        Me.RadGroupBox1.Controls.Add(Me.RadRadioButtonSyncAlles)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        '
        '
        '
        Me.RadGroupBox1.RootElement.AccessibleDescription = resources.GetString("RadGroupBox1.RootElement.AccessibleDescription")
        Me.RadGroupBox1.RootElement.AccessibleName = resources.GetString("RadGroupBox1.RootElement.AccessibleName")
        Me.RadGroupBox1.RootElement.Alignment = CType(resources.GetObject("RadGroupBox1.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadGroupBox1.RootElement.AngleTransform = CType(resources.GetObject("RadGroupBox1.RootElement.AngleTransform"), Single)
        Me.RadGroupBox1.RootElement.FlipText = CType(resources.GetObject("RadGroupBox1.RootElement.FlipText"), Boolean)
        Me.RadGroupBox1.RootElement.KeyTip = resources.GetString("RadGroupBox1.RootElement.KeyTip")
        Me.RadGroupBox1.RootElement.Margin = CType(resources.GetObject("RadGroupBox1.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadGroupBox1.RootElement.Padding = CType(resources.GetObject("RadGroupBox1.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadGroupBox1.RootElement.Text = resources.GetString("RadGroupBox1.RootElement.Text")
        Me.RadGroupBox1.RootElement.TextOrientation = CType(resources.GetObject("RadGroupBox1.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadGroupBox1.RootElement.ToolTipText = resources.GetString("RadGroupBox1.RootElement.ToolTipText")
        '
        'RadDateTimePickerEnd
        '
        resources.ApplyResources(Me.RadDateTimePickerEnd, "RadDateTimePickerEnd")
        Me.RadDateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.RadDateTimePickerEnd.Name = "RadDateTimePickerEnd"
        '
        '
        '
        Me.RadDateTimePickerEnd.RootElement.AccessibleDescription = resources.GetString("RadDateTimePickerEnd.RootElement.AccessibleDescription")
        Me.RadDateTimePickerEnd.RootElement.AccessibleName = resources.GetString("RadDateTimePickerEnd.RootElement.AccessibleName")
        Me.RadDateTimePickerEnd.RootElement.Alignment = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadDateTimePickerEnd.RootElement.AngleTransform = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.AngleTransform"), Single)
        Me.RadDateTimePickerEnd.RootElement.FlipText = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.FlipText"), Boolean)
        Me.RadDateTimePickerEnd.RootElement.KeyTip = resources.GetString("RadDateTimePickerEnd.RootElement.KeyTip")
        Me.RadDateTimePickerEnd.RootElement.Margin = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerEnd.RootElement.Padding = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerEnd.RootElement.Text = resources.GetString("RadDateTimePickerEnd.RootElement.Text")
        Me.RadDateTimePickerEnd.RootElement.TextOrientation = CType(resources.GetObject("RadDateTimePickerEnd.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadDateTimePickerEnd.RootElement.ToolTipText = resources.GetString("RadDateTimePickerEnd.RootElement.ToolTipText")
        Me.RadDateTimePickerEnd.TabStop = False
        Me.RadDateTimePickerEnd.Value = New Date(2014, 2, 12, 11, 45, 34, 423)
        '
        'RadDateTimePickerStart
        '
        resources.ApplyResources(Me.RadDateTimePickerStart, "RadDateTimePickerStart")
        Me.RadDateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.RadDateTimePickerStart.Name = "RadDateTimePickerStart"
        '
        '
        '
        Me.RadDateTimePickerStart.RootElement.AccessibleDescription = resources.GetString("RadDateTimePickerStart.RootElement.AccessibleDescription")
        Me.RadDateTimePickerStart.RootElement.AccessibleName = resources.GetString("RadDateTimePickerStart.RootElement.AccessibleName")
        Me.RadDateTimePickerStart.RootElement.Alignment = CType(resources.GetObject("RadDateTimePickerStart.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadDateTimePickerStart.RootElement.AngleTransform = CType(resources.GetObject("RadDateTimePickerStart.RootElement.AngleTransform"), Single)
        Me.RadDateTimePickerStart.RootElement.FlipText = CType(resources.GetObject("RadDateTimePickerStart.RootElement.FlipText"), Boolean)
        Me.RadDateTimePickerStart.RootElement.KeyTip = resources.GetString("RadDateTimePickerStart.RootElement.KeyTip")
        Me.RadDateTimePickerStart.RootElement.Margin = CType(resources.GetObject("RadDateTimePickerStart.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerStart.RootElement.Padding = CType(resources.GetObject("RadDateTimePickerStart.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerStart.RootElement.Text = resources.GetString("RadDateTimePickerStart.RootElement.Text")
        Me.RadDateTimePickerStart.RootElement.TextOrientation = CType(resources.GetObject("RadDateTimePickerStart.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadDateTimePickerStart.RootElement.ToolTipText = resources.GetString("RadDateTimePickerStart.RootElement.ToolTipText")
        Me.RadDateTimePickerStart.TabStop = False
        Me.RadDateTimePickerStart.Value = New Date(2014, 2, 12, 11, 45, 34, 423)
        '
        'RadDateTimePickerSince
        '
        resources.ApplyResources(Me.RadDateTimePickerSince, "RadDateTimePickerSince")
        Me.RadDateTimePickerSince.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.RadDateTimePickerSince.Name = "RadDateTimePickerSince"
        '
        '
        '
        Me.RadDateTimePickerSince.RootElement.AccessibleDescription = resources.GetString("RadDateTimePickerSince.RootElement.AccessibleDescription")
        Me.RadDateTimePickerSince.RootElement.AccessibleName = resources.GetString("RadDateTimePickerSince.RootElement.AccessibleName")
        Me.RadDateTimePickerSince.RootElement.Alignment = CType(resources.GetObject("RadDateTimePickerSince.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadDateTimePickerSince.RootElement.AngleTransform = CType(resources.GetObject("RadDateTimePickerSince.RootElement.AngleTransform"), Single)
        Me.RadDateTimePickerSince.RootElement.FlipText = CType(resources.GetObject("RadDateTimePickerSince.RootElement.FlipText"), Boolean)
        Me.RadDateTimePickerSince.RootElement.KeyTip = resources.GetString("RadDateTimePickerSince.RootElement.KeyTip")
        Me.RadDateTimePickerSince.RootElement.Margin = CType(resources.GetObject("RadDateTimePickerSince.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerSince.RootElement.Padding = CType(resources.GetObject("RadDateTimePickerSince.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadDateTimePickerSince.RootElement.Text = resources.GetString("RadDateTimePickerSince.RootElement.Text")
        Me.RadDateTimePickerSince.RootElement.TextOrientation = CType(resources.GetObject("RadDateTimePickerSince.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadDateTimePickerSince.RootElement.ToolTipText = resources.GetString("RadDateTimePickerSince.RootElement.ToolTipText")
        Me.RadDateTimePickerSince.TabStop = False
        Me.RadDateTimePickerSince.Value = New Date(2014, 2, 12, 11, 45, 34, 423)
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'RadRadioButtonSyncZwischen
        '
        resources.ApplyResources(Me.RadRadioButtonSyncZwischen, "RadRadioButtonSyncZwischen")
        Me.RadRadioButtonSyncZwischen.Name = "RadRadioButtonSyncZwischen"
        '
        '
        '
        Me.RadRadioButtonSyncZwischen.RootElement.AccessibleDescription = resources.GetString("RadRadioButtonSyncZwischen.RootElement.AccessibleDescription")
        Me.RadRadioButtonSyncZwischen.RootElement.AccessibleName = resources.GetString("RadRadioButtonSyncZwischen.RootElement.AccessibleName")
        Me.RadRadioButtonSyncZwischen.RootElement.Alignment = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadRadioButtonSyncZwischen.RootElement.AngleTransform = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.AngleTransform"), Single)
        Me.RadRadioButtonSyncZwischen.RootElement.FlipText = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.FlipText"), Boolean)
        Me.RadRadioButtonSyncZwischen.RootElement.KeyTip = resources.GetString("RadRadioButtonSyncZwischen.RootElement.KeyTip")
        Me.RadRadioButtonSyncZwischen.RootElement.Margin = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncZwischen.RootElement.Padding = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncZwischen.RootElement.Text = resources.GetString("RadRadioButtonSyncZwischen.RootElement.Text")
        Me.RadRadioButtonSyncZwischen.RootElement.TextOrientation = CType(resources.GetObject("RadRadioButtonSyncZwischen.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadRadioButtonSyncZwischen.RootElement.ToolTipText = resources.GetString("RadRadioButtonSyncZwischen.RootElement.ToolTipText")
        '
        'RadRadioButtonSyncSeit
        '
        resources.ApplyResources(Me.RadRadioButtonSyncSeit, "RadRadioButtonSyncSeit")
        Me.RadRadioButtonSyncSeit.Name = "RadRadioButtonSyncSeit"
        '
        '
        '
        Me.RadRadioButtonSyncSeit.RootElement.AccessibleDescription = resources.GetString("RadRadioButtonSyncSeit.RootElement.AccessibleDescription")
        Me.RadRadioButtonSyncSeit.RootElement.AccessibleName = resources.GetString("RadRadioButtonSyncSeit.RootElement.AccessibleName")
        Me.RadRadioButtonSyncSeit.RootElement.Alignment = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadRadioButtonSyncSeit.RootElement.AngleTransform = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.AngleTransform"), Single)
        Me.RadRadioButtonSyncSeit.RootElement.FlipText = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.FlipText"), Boolean)
        Me.RadRadioButtonSyncSeit.RootElement.KeyTip = resources.GetString("RadRadioButtonSyncSeit.RootElement.KeyTip")
        Me.RadRadioButtonSyncSeit.RootElement.Margin = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncSeit.RootElement.Padding = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncSeit.RootElement.Text = resources.GetString("RadRadioButtonSyncSeit.RootElement.Text")
        Me.RadRadioButtonSyncSeit.RootElement.TextOrientation = CType(resources.GetObject("RadRadioButtonSyncSeit.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadRadioButtonSyncSeit.RootElement.ToolTipText = resources.GetString("RadRadioButtonSyncSeit.RootElement.ToolTipText")
        '
        'RadRadioButtonSyncAlles
        '
        resources.ApplyResources(Me.RadRadioButtonSyncAlles, "RadRadioButtonSyncAlles")
        Me.RadRadioButtonSyncAlles.Name = "RadRadioButtonSyncAlles"
        '
        '
        '
        Me.RadRadioButtonSyncAlles.RootElement.AccessibleDescription = resources.GetString("RadRadioButtonSyncAlles.RootElement.AccessibleDescription")
        Me.RadRadioButtonSyncAlles.RootElement.AccessibleName = resources.GetString("RadRadioButtonSyncAlles.RootElement.AccessibleName")
        Me.RadRadioButtonSyncAlles.RootElement.Alignment = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadRadioButtonSyncAlles.RootElement.AngleTransform = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.AngleTransform"), Single)
        Me.RadRadioButtonSyncAlles.RootElement.FlipText = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.FlipText"), Boolean)
        Me.RadRadioButtonSyncAlles.RootElement.KeyTip = resources.GetString("RadRadioButtonSyncAlles.RootElement.KeyTip")
        Me.RadRadioButtonSyncAlles.RootElement.Margin = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncAlles.RootElement.Padding = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadRadioButtonSyncAlles.RootElement.Text = resources.GetString("RadRadioButtonSyncAlles.RootElement.Text")
        Me.RadRadioButtonSyncAlles.RootElement.TextOrientation = CType(resources.GetObject("RadRadioButtonSyncAlles.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadRadioButtonSyncAlles.RootElement.ToolTipText = resources.GetString("RadRadioButtonSyncAlles.RootElement.ToolTipText")
        Me.RadRadioButtonSyncAlles.TabStop = True
        Me.RadRadioButtonSyncAlles.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadButtonOK
        '
        resources.ApplyResources(Me.RadButtonOK, "RadButtonOK")
        Me.RadButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.RadButtonOK.Image = Global.EichsoftwareClient.My.Resources.Resources.disk
        Me.RadButtonOK.Name = "RadButtonOK"
        '
        '
        '
        Me.RadButtonOK.RootElement.AccessibleDescription = resources.GetString("RadButtonOK.RootElement.AccessibleDescription")
        Me.RadButtonOK.RootElement.AccessibleName = resources.GetString("RadButtonOK.RootElement.AccessibleName")
        Me.RadButtonOK.RootElement.Alignment = CType(resources.GetObject("RadButtonOK.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonOK.RootElement.AngleTransform = CType(resources.GetObject("RadButtonOK.RootElement.AngleTransform"), Single)
        Me.RadButtonOK.RootElement.FlipText = CType(resources.GetObject("RadButtonOK.RootElement.FlipText"), Boolean)
        Me.RadButtonOK.RootElement.KeyTip = resources.GetString("RadButtonOK.RootElement.KeyTip")
        Me.RadButtonOK.RootElement.Margin = CType(resources.GetObject("RadButtonOK.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonOK.RootElement.Padding = CType(resources.GetObject("RadButtonOK.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonOK.RootElement.Text = resources.GetString("RadButtonOK.RootElement.Text")
        Me.RadButtonOK.RootElement.TextOrientation = CType(resources.GetObject("RadButtonOK.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonOK.RootElement.ToolTipText = resources.GetString("RadButtonOK.RootElement.ToolTipText")
        '
        'RadButtonAbbrechen
        '
        resources.ApplyResources(Me.RadButtonAbbrechen, "RadButtonAbbrechen")
        Me.RadButtonAbbrechen.Name = "RadButtonAbbrechen"
        '
        '
        '
        Me.RadButtonAbbrechen.RootElement.AccessibleDescription = resources.GetString("RadButtonAbbrechen.RootElement.AccessibleDescription")
        Me.RadButtonAbbrechen.RootElement.AccessibleName = resources.GetString("RadButtonAbbrechen.RootElement.AccessibleName")
        Me.RadButtonAbbrechen.RootElement.Alignment = CType(resources.GetObject("RadButtonAbbrechen.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RadButtonAbbrechen.RootElement.AngleTransform = CType(resources.GetObject("RadButtonAbbrechen.RootElement.AngleTransform"), Single)
        Me.RadButtonAbbrechen.RootElement.FlipText = CType(resources.GetObject("RadButtonAbbrechen.RootElement.FlipText"), Boolean)
        Me.RadButtonAbbrechen.RootElement.KeyTip = resources.GetString("RadButtonAbbrechen.RootElement.KeyTip")
        Me.RadButtonAbbrechen.RootElement.Margin = CType(resources.GetObject("RadButtonAbbrechen.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RadButtonAbbrechen.RootElement.Padding = CType(resources.GetObject("RadButtonAbbrechen.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RadButtonAbbrechen.RootElement.Text = resources.GetString("RadButtonAbbrechen.RootElement.Text")
        Me.RadButtonAbbrechen.RootElement.TextOrientation = CType(resources.GetObject("RadButtonAbbrechen.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RadButtonAbbrechen.RootElement.ToolTipText = resources.GetString("RadButtonAbbrechen.RootElement.ToolTipText")
        '
        'FrmEinstellungen
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadButtonAbbrechen)
        Me.Controls.Add(Me.RadButtonOK)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmEinstellungen"
        '
        '
        '
        Me.RootElement.AccessibleDescription = resources.GetString("FrmEinstellungen.RootElement.AccessibleDescription")
        Me.RootElement.AccessibleName = resources.GetString("FrmEinstellungen.RootElement.AccessibleName")
        Me.RootElement.Alignment = CType(resources.GetObject("FrmEinstellungen.RootElement.Alignment"), System.Drawing.ContentAlignment)
        Me.RootElement.AngleTransform = CType(resources.GetObject("FrmEinstellungen.RootElement.AngleTransform"), Single)
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.FlipText = CType(resources.GetObject("FrmEinstellungen.RootElement.FlipText"), Boolean)
        Me.RootElement.KeyTip = resources.GetString("FrmEinstellungen.RootElement.KeyTip")
        Me.RootElement.Margin = CType(resources.GetObject("FrmEinstellungen.RootElement.Margin"), System.Windows.Forms.Padding)
        Me.RootElement.Padding = CType(resources.GetObject("FrmEinstellungen.RootElement.Padding"), System.Windows.Forms.Padding)
        Me.RootElement.Text = resources.GetString("FrmEinstellungen.RootElement.Text")
        Me.RootElement.TextOrientation = CType(resources.GetObject("FrmEinstellungen.RootElement.TextOrientation"), System.Windows.Forms.Orientation)
        Me.RootElement.ToolTipText = resources.GetString("FrmEinstellungen.RootElement.ToolTipText")
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadDateTimePickerEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDateTimePickerStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDateTimePickerSince, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButtonSyncZwischen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButtonSyncSeit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButtonSyncAlles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadRadioButtonSyncZwischen As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButtonSyncSeit As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButtonSyncAlles As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadButtonOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonAbbrechen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadDateTimePickerEnd As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadDateTimePickerStart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadDateTimePickerSince As Telerik.WinControls.UI.RadDateTimePicker
End Class

