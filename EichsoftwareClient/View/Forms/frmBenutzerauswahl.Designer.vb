<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBenutzerauswahl

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
        Me.RadListControlBenutzer = New Telerik.WinControls.UI.RadListControl()
        Me.RadButtonOK = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonNeuerBenutzer = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadListControlBenutzer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonNeuerBenutzer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadListControlBenutzer
        '
        Me.RadListControlBenutzer.Location = New System.Drawing.Point(3, 3)
        Me.RadListControlBenutzer.Name = "RadListControlBenutzer"
        Me.RadListControlBenutzer.Size = New System.Drawing.Size(336, 391)
        Me.RadListControlBenutzer.TabIndex = 0
        Me.RadListControlBenutzer.Text = "RadListControl1"
        '
        'RadButtonOK
        '
        Me.RadButtonOK.Location = New System.Drawing.Point(345, 370)
        Me.RadButtonOK.Name = "RadButtonOK"
        Me.RadButtonOK.Size = New System.Drawing.Size(132, 24)
        Me.RadButtonOK.TabIndex = 1
        Me.RadButtonOK.Text = "OK"
        '
        'RadButtonNeuerBenutzer
        '
        Me.RadButtonNeuerBenutzer.Image = Global.EichsoftwareClient.My.Resources.Resources.user_add
        Me.RadButtonNeuerBenutzer.Location = New System.Drawing.Point(345, 3)
        Me.RadButtonNeuerBenutzer.Name = "RadButtonNeuerBenutzer"
        Me.RadButtonNeuerBenutzer.Size = New System.Drawing.Size(132, 77)
        Me.RadButtonNeuerBenutzer.TabIndex = 2
        Me.RadButtonNeuerBenutzer.Text = "<html><p>Neue Lizenz</p><p>Add license</p></html>"
        Me.RadButtonNeuerBenutzer.TextWrap = True
        '
        'FrmBenutzerauswahl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 397)
        Me.Controls.Add(Me.RadButtonNeuerBenutzer)
        Me.Controls.Add(Me.RadButtonOK)
        Me.Controls.Add(Me.RadListControlBenutzer)
        Me.KeyPreview = True
        Me.Name = "FrmBenutzerauswahl"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Benutzerauswahl / User selection"
        CType(Me.RadListControlBenutzer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonNeuerBenutzer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadListControlBenutzer As Telerik.WinControls.UI.RadListControl
    Friend WithEvents RadButtonOK As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonNeuerBenutzer As Telerik.WinControls.UI.RadButton
End Class

