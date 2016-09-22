<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEingabeFirmenVertragspartnerZuordnung

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
        Me.RadButtonSpeichern = New Telerik.WinControls.UI.RadButton()
        Me.RadLabelFirma = New Telerik.WinControls.UI.RadLabel()
        Me.RadDropDownListHauptfirma = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadDropDownListUnterfirma = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadLabelVertragspartner = New Telerik.WinControls.UI.RadLabel()
        Me.RadButtonAbbrechen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonLoeschen = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadButtonSpeichern, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabelFirma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownListHauptfirma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadDropDownListUnterfirma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabelVertragspartner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonLoeschen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButtonSpeichern
        '
        Me.RadButtonSpeichern.Location = New System.Drawing.Point(487, 198)
        Me.RadButtonSpeichern.Name = "RadButtonSpeichern"
        Me.RadButtonSpeichern.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonSpeichern.TabIndex = 8
        Me.RadButtonSpeichern.Text = "Speichern"
        '
        'RadLabelFirma
        '
        Me.RadLabelFirma.Location = New System.Drawing.Point(13, 15)
        Me.RadLabelFirma.Name = "RadLabelFirma"
        Me.RadLabelFirma.Size = New System.Drawing.Size(36, 18)
        Me.RadLabelFirma.TabIndex = 10
        Me.RadLabelFirma.Text = "Firma:"
        '
        'RadDropDownListHauptfirma
        '
        Me.RadDropDownListHauptfirma.Location = New System.Drawing.Point(138, 12)
        Me.RadDropDownListHauptfirma.Name = "RadDropDownListHauptfirma"
        Me.RadDropDownListHauptfirma.Size = New System.Drawing.Size(459, 20)
        Me.RadDropDownListHauptfirma.TabIndex = 11
        Me.RadDropDownListHauptfirma.Text = "RadDropDownList1"
        '
        'RadDropDownListUnterfirma
        '
        Me.RadDropDownListUnterfirma.Location = New System.Drawing.Point(138, 50)
        Me.RadDropDownListUnterfirma.Name = "RadDropDownListUnterfirma"
        Me.RadDropDownListUnterfirma.Size = New System.Drawing.Size(459, 20)
        Me.RadDropDownListUnterfirma.TabIndex = 13
        Me.RadDropDownListUnterfirma.Text = "RadDropDownList2"
        '
        'RadLabelVertragspartner
        '
        Me.RadLabelVertragspartner.Location = New System.Drawing.Point(13, 53)
        Me.RadLabelVertragspartner.Name = "RadLabelVertragspartner"
        Me.RadLabelVertragspartner.Size = New System.Drawing.Size(87, 18)
        Me.RadLabelVertragspartner.TabIndex = 12
        Me.RadLabelVertragspartner.Text = "Vertragspartner:"
        '
        'RadButtonAbbrechen
        '
        Me.RadButtonAbbrechen.Location = New System.Drawing.Point(371, 198)
        Me.RadButtonAbbrechen.Name = "RadButtonAbbrechen"
        Me.RadButtonAbbrechen.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonAbbrechen.TabIndex = 14
        Me.RadButtonAbbrechen.Text = "Abbrechen"
        '
        'RadButtonLoeschen
        '
        Me.RadButtonLoeschen.Location = New System.Drawing.Point(13, 198)
        Me.RadButtonLoeschen.Name = "RadButtonLoeschen"
        Me.RadButtonLoeschen.Size = New System.Drawing.Size(110, 24)
        Me.RadButtonLoeschen.TabIndex = 15
        Me.RadButtonLoeschen.Text = "Zuordnung löschen"
        '
        'frmFirmenZuordnung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 228)
        Me.Controls.Add(Me.RadButtonLoeschen)
        Me.Controls.Add(Me.RadButtonAbbrechen)
        Me.Controls.Add(Me.RadDropDownListUnterfirma)
        Me.Controls.Add(Me.RadLabelVertragspartner)
        Me.Controls.Add(Me.RadDropDownListHauptfirma)
        Me.Controls.Add(Me.RadLabelFirma)
        Me.Controls.Add(Me.RadButtonSpeichern)
        Me.Name = "frmFirmenZuordnung"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lizenzverwaltung"
        CType(Me.RadButtonSpeichern, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabelFirma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownListHauptfirma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadDropDownListUnterfirma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabelVertragspartner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonLoeschen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadButtonSpeichern As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabelFirma As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadDropDownListHauptfirma As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadDropDownListUnterfirma As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents RadLabelVertragspartner As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadButtonAbbrechen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonLoeschen As Telerik.WinControls.UI.RadButton
End Class

