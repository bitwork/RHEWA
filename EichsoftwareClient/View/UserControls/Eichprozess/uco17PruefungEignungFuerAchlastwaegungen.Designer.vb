<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uco17PruefungEignungFuerAchlastwaegungen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco17PruefungEignungFuerAchlastwaegungen))
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel()
        Me.lblZufahrtenInOrdnung = New Telerik.WinControls.UI.RadLabel()
        Me.RadCheckBoxWaagegeprueft = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblWaageNichtGeeignet = New Telerik.WinControls.UI.RadLabel()
        Me.RadCheckBoxWaageNichtGeeignet = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblWaagegeprueft = New Telerik.WinControls.UI.RadLabel()
        Me.RadCheckBoxZufahrtenInOrdnung = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me.lblZufahrtenInOrdnung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxWaagegeprueft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWaageNichtGeeignet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxWaageNichtGeeignet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWaagegeprueft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBoxZufahrtenInOrdnung, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadScrollablePanel1
        '
        resources.ApplyResources(Me.RadScrollablePanel1, "RadScrollablePanel1")
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.lblZufahrtenInOrdnung)
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadCheckBoxWaagegeprueft)
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.lblWaageNichtGeeignet)
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadCheckBoxWaageNichtGeeignet)
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.lblWaagegeprueft)
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadCheckBoxZufahrtenInOrdnung)
        resources.ApplyResources(Me.RadScrollablePanel1.PanelContainer, "RadScrollablePanel1.PanelContainer")
        '
        'lblZufahrtenInOrdnung
        '
        resources.ApplyResources(Me.lblZufahrtenInOrdnung, "lblZufahrtenInOrdnung")
        Me.lblZufahrtenInOrdnung.Name = "lblZufahrtenInOrdnung"
        '
        'RadCheckBoxWaagegeprueft
        '
        resources.ApplyResources(Me.RadCheckBoxWaagegeprueft, "RadCheckBoxWaagegeprueft")
        Me.RadCheckBoxWaagegeprueft.Name = "RadCheckBoxWaagegeprueft"
        '
        'lblWaageNichtGeeignet
        '
        resources.ApplyResources(Me.lblWaageNichtGeeignet, "lblWaageNichtGeeignet")
        Me.lblWaageNichtGeeignet.Name = "lblWaageNichtGeeignet"
        '
        'RadCheckBoxWaageNichtGeeignet
        '
        resources.ApplyResources(Me.RadCheckBoxWaageNichtGeeignet, "RadCheckBoxWaageNichtGeeignet")
        Me.RadCheckBoxWaageNichtGeeignet.Name = "RadCheckBoxWaageNichtGeeignet"
        '
        'lblWaagegeprueft
        '
        resources.ApplyResources(Me.lblWaagegeprueft, "lblWaagegeprueft")
        Me.lblWaagegeprueft.Name = "lblWaagegeprueft"
        '
        'RadCheckBoxZufahrtenInOrdnung
        '
        resources.ApplyResources(Me.RadCheckBoxZufahrtenInOrdnung, "RadCheckBoxZufahrtenInOrdnung")
        Me.RadCheckBoxZufahrtenInOrdnung.Name = "RadCheckBoxZufahrtenInOrdnung"
        '
        'uco17PruefungEignungFuerAchlastwaegungen
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadScrollablePanel1)
        Me.Name = "uco17PruefungEignungFuerAchlastwaegungen"
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        Me.RadScrollablePanel1.PanelContainer.PerformLayout()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        CType(Me.lblZufahrtenInOrdnung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxWaagegeprueft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWaageNichtGeeignet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxWaageNichtGeeignet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWaagegeprueft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBoxZufahrtenInOrdnung, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadCheckBoxWaagegeprueft As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadCheckBoxWaageNichtGeeignet As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadCheckBoxZufahrtenInOrdnung As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblWaagegeprueft As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblWaageNichtGeeignet As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblZufahrtenInOrdnung As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel

End Class
