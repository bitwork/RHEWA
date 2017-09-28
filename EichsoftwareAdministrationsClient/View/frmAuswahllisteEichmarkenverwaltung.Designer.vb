<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAuswahllisteEichmarkenverwaltung

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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGridView1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonBearbeiten = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonAbbrechen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonSpeichern = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonAddMarkenbestand = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonSpeichern, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonAddMarkenbestand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridView1
        '
        Me.RadGridView1.AllowShowFocusCues = True
        Me.RadGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridView1.AutoGenerateHierarchy = True
        Me.RadGridView1.Location = New System.Drawing.Point(12, 12)
        '
        '
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowEditRow = False
        Me.RadGridView1.MasterTemplate.AutoExpandGroups = True
        Me.RadGridView1.MasterTemplate.EnableFiltering = True
        Me.RadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.RadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ShowHeaderCellButtons = True
        Me.RadGridView1.Size = New System.Drawing.Size(950, 489)
        Me.RadGridView1.TabIndex = 0
        '
        'RadButtonBearbeiten
        '
        Me.RadButtonBearbeiten.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonBearbeiten.Location = New System.Drawing.Point(736, 507)
        Me.RadButtonBearbeiten.Name = "RadButtonBearbeiten"
        Me.RadButtonBearbeiten.Size = New System.Drawing.Size(226, 42)
        Me.RadButtonBearbeiten.TabIndex = 7
        Me.RadButtonBearbeiten.Text = "Bearbeitungsmodus"
        Me.RadButtonBearbeiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'RadButtonAbbrechen
        '
        Me.RadButtonAbbrechen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonAbbrechen.Location = New System.Drawing.Point(736, 507)
        Me.RadButtonAbbrechen.Name = "RadButtonAbbrechen"
        Me.RadButtonAbbrechen.Size = New System.Drawing.Size(110, 42)
        Me.RadButtonAbbrechen.TabIndex = 8
        Me.RadButtonAbbrechen.Text = "Abbrechen"
        Me.RadButtonAbbrechen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RadButtonAbbrechen.Visible = False
        '
        'RadButtonSpeichern
        '
        Me.RadButtonSpeichern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonSpeichern.Location = New System.Drawing.Point(851, 507)
        Me.RadButtonSpeichern.Name = "RadButtonSpeichern"
        Me.RadButtonSpeichern.Size = New System.Drawing.Size(110, 42)
        Me.RadButtonSpeichern.TabIndex = 9
        Me.RadButtonSpeichern.Text = "Speichern"
        Me.RadButtonSpeichern.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.RadButtonSpeichern.Visible = False
        '
        'RadButtonAddMarkenbestand
        '
        Me.RadButtonAddMarkenbestand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonAddMarkenbestand.Location = New System.Drawing.Point(516, 507)
        Me.RadButtonAddMarkenbestand.Name = "RadButtonAddMarkenbestand"
        Me.RadButtonAddMarkenbestand.Size = New System.Drawing.Size(192, 42)
        Me.RadButtonAddMarkenbestand.TabIndex = 10
        Me.RadButtonAddMarkenbestand.Text = "Markenbestand Addieren"
        Me.RadButtonAddMarkenbestand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'FrmAuswahllisteEichmarkenverwaltung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(975, 561)
        Me.Controls.Add(Me.RadButtonAddMarkenbestand)
        Me.Controls.Add(Me.RadButtonSpeichern)
        Me.Controls.Add(Me.RadButtonAbbrechen)
        Me.Controls.Add(Me.RadButtonBearbeiten)
        Me.Controls.Add(Me.RadGridView1)
        Me.Name = "FrmAuswahllisteEichmarkenverwaltung"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Statistik Eichmarkenverwaltung"
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonBearbeiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAbbrechen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonSpeichern, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonAddMarkenbestand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonBearbeiten As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonAbbrechen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonSpeichern As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonAddMarkenbestand As Telerik.WinControls.UI.RadButton
End Class

