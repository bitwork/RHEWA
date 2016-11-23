<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLizenz

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLizenz))
        Me.lblName = New Telerik.WinControls.UI.RadLabel()
        Me.RadTextBoxControl1 = New Telerik.WinControls.UI.RadTextBox()
        Me.RadTextBoxControl2 = New Telerik.WinControls.UI.RadTextBox()
        Me.lblLizenzschuessel = New Telerik.WinControls.UI.RadLabel()
        Me.RadButtonOK = New Telerik.WinControls.UI.RadButton()
        Me.RadCheckBoxAkzeptieren = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadTextBoxControl3 = New Telerik.WinControls.UI.RadTextBox()
        Me.lblLizenzabkommen = New Telerik.WinControls.UI.RadLabel()
        Me.RadButtonChangeLanguageToPolish = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonChangeLanguageToEnglish = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonChangeLanguageToGerman = New Telerik.WinControls.UI.RadButton()
        Me.PictureBoxRHEWALogo = New System.Windows.Forms.PictureBox()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLizenzschuessel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxAkzeptieren, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBoxControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLizenzabkommen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonChangeLanguageToPolish, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonChangeLanguageToEnglish, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonChangeLanguageToGerman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRHEWALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblName
        '
        resources.ApplyResources(Me.lblName, "lblName")
        Me.lblName.Name = "lblName"
        '
        'RadTextBoxControl1
        '
        resources.ApplyResources(Me.RadTextBoxControl1, "RadTextBoxControl1")
        Me.RadTextBoxControl1.Name = "RadTextBoxControl1"
        '
        'RadTextBoxControl2
        '
        resources.ApplyResources(Me.RadTextBoxControl2, "RadTextBoxControl2")
        Me.RadTextBoxControl2.Name = "RadTextBoxControl2"
        '
        'lblLizenzschuessel
        '
        resources.ApplyResources(Me.lblLizenzschuessel, "lblLizenzschuessel")
        Me.lblLizenzschuessel.Name = "lblLizenzschuessel"
        '
        'RadButtonOK
        '
        resources.ApplyResources(Me.RadButtonOK, "RadButtonOK")
        Me.RadButtonOK.Name = "RadButtonOK"
        '
        'RadCheckBoxAkzeptieren
        '
        resources.ApplyResources(Me.RadCheckBoxAkzeptieren, "RadCheckBoxAkzeptieren")
        Me.RadCheckBoxAkzeptieren.Name = "RadCheckBoxAkzeptieren"
        '
        'RadTextBoxControl3
        '
        resources.ApplyResources(Me.RadTextBoxControl3, "RadTextBoxControl3")
        Me.RadTextBoxControl3.Multiline = True
        Me.RadTextBoxControl3.Name = "RadTextBoxControl3"
        Me.RadTextBoxControl3.ReadOnly = True
        Me.RadTextBoxControl3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.RadTextBoxControl3.TabStop = False
        '
        'lblLizenzabkommen
        '
        resources.ApplyResources(Me.lblLizenzabkommen, "lblLizenzabkommen")
        Me.lblLizenzabkommen.Name = "lblLizenzabkommen"
        '
        'RadButtonChangeLanguageToPolish
        '
        resources.ApplyResources(Me.RadButtonChangeLanguageToPolish, "RadButtonChangeLanguageToPolish")
        Me.RadButtonChangeLanguageToPolish.BackColor = System.Drawing.Color.Transparent
        Me.RadButtonChangeLanguageToPolish.Image = Global.EichsoftwareClient.My.Resources.Resources.flag_poland
        Me.RadButtonChangeLanguageToPolish.Name = "RadButtonChangeLanguageToPolish"
        Me.RadButtonChangeLanguageToPolish.TabStop = False
        CType(Me.RadButtonChangeLanguageToPolish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Image = Global.EichsoftwareClient.My.Resources.Resources.flag_poland
        CType(Me.RadButtonChangeLanguageToPolish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        CType(Me.RadButtonChangeLanguageToPolish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Text = resources.GetString("resource.Text")
        CType(Me.RadButtonChangeLanguageToPolish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Shape = Nothing
        '
        'RadButtonChangeLanguageToEnglish
        '
        resources.ApplyResources(Me.RadButtonChangeLanguageToEnglish, "RadButtonChangeLanguageToEnglish")
        Me.RadButtonChangeLanguageToEnglish.BackColor = System.Drawing.Color.Transparent
        Me.RadButtonChangeLanguageToEnglish.Image = Global.EichsoftwareClient.My.Resources.Resources.flag_usa
        Me.RadButtonChangeLanguageToEnglish.Name = "RadButtonChangeLanguageToEnglish"
        Me.RadButtonChangeLanguageToEnglish.TabStop = False
        CType(Me.RadButtonChangeLanguageToEnglish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Image = Global.EichsoftwareClient.My.Resources.Resources.flag_usa
        CType(Me.RadButtonChangeLanguageToEnglish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        CType(Me.RadButtonChangeLanguageToEnglish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Text = resources.GetString("resource.Text1")
        CType(Me.RadButtonChangeLanguageToEnglish.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Shape = Nothing
        '
        'RadButtonChangeLanguageToGerman
        '
        resources.ApplyResources(Me.RadButtonChangeLanguageToGerman, "RadButtonChangeLanguageToGerman")
        Me.RadButtonChangeLanguageToGerman.BackColor = System.Drawing.Color.Transparent
        Me.RadButtonChangeLanguageToGerman.Image = Global.EichsoftwareClient.My.Resources.Resources.flag_germany
        Me.RadButtonChangeLanguageToGerman.Name = "RadButtonChangeLanguageToGerman"
        Me.RadButtonChangeLanguageToGerman.TabStop = False
        CType(Me.RadButtonChangeLanguageToGerman.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Image = Global.EichsoftwareClient.My.Resources.Resources.flag_germany
        CType(Me.RadButtonChangeLanguageToGerman.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        CType(Me.RadButtonChangeLanguageToGerman.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Text = resources.GetString("resource.Text2")
        CType(Me.RadButtonChangeLanguageToGerman.GetChildAt(0), Telerik.WinControls.UI.RadButtonElement).Shape = Nothing
        '
        'PictureBoxRHEWALogo
        '
        resources.ApplyResources(Me.PictureBoxRHEWALogo, "PictureBoxRHEWALogo")
        Me.PictureBoxRHEWALogo.BackColor = System.Drawing.Color.Transparent
        Me.PictureBoxRHEWALogo.Image = Global.EichsoftwareClient.My.Resources.Resources.RHEWA_Logo_35mm_600dpi
        Me.PictureBoxRHEWALogo.Name = "PictureBoxRHEWALogo"
        Me.PictureBoxRHEWALogo.TabStop = False
        '
        'FrmLizenz
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadButtonChangeLanguageToPolish)
        Me.Controls.Add(Me.RadButtonChangeLanguageToEnglish)
        Me.Controls.Add(Me.RadButtonChangeLanguageToGerman)
        Me.Controls.Add(Me.PictureBoxRHEWALogo)
        Me.Controls.Add(Me.lblLizenzabkommen)
        Me.Controls.Add(Me.RadTextBoxControl3)
        Me.Controls.Add(Me.RadCheckBoxAkzeptieren)
        Me.Controls.Add(Me.RadButtonOK)
        Me.Controls.Add(Me.RadTextBoxControl2)
        Me.Controls.Add(Me.lblLizenzschuessel)
        Me.Controls.Add(Me.RadTextBoxControl1)
        Me.Controls.Add(Me.lblName)
        Me.Name = "FrmLizenz"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(822, 520)
        Me.ShowIcon = False
        Me.ShowItemToolTips = False
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLizenzschuessel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxAkzeptieren, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBoxControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLizenzabkommen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonChangeLanguageToPolish, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonChangeLanguageToEnglish, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonChangeLanguageToGerman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRHEWALogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblName As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadTextBoxControl1 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadTextBoxControl2 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblLizenzschuessel As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadButtonOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadCheckBoxAkzeptieren As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadTextBoxControl3 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents lblLizenzabkommen As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadButtonChangeLanguageToPolish As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonChangeLanguageToEnglish As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonChangeLanguageToGerman As Telerik.WinControls.UI.RadButton
    Friend WithEvents PictureBoxRHEWALogo As System.Windows.Forms.PictureBox
End Class

