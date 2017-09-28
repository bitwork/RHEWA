<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWaagenLoeschen
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadGridView1 = New Telerik.WinControls.UI.RadGridView()
        Me.RadButtonZuordnen = New Telerik.WinControls.UI.RadButton()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonZuordnen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGridView1
        '
        Me.RadGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGridView1.Location = New System.Drawing.Point(0, 47)
        '
        '
        '
        Me.RadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.RadGridView1.MasterTemplate.AllowCellContextMenu = False
        Me.RadGridView1.MasterTemplate.AllowDeleteRow = False
        Me.RadGridView1.MasterTemplate.AllowEditRow = False
        Me.RadGridView1.MasterTemplate.EnableAlternatingRowColor = True
        Me.RadGridView1.MasterTemplate.EnableFiltering = True
        Me.RadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RadGridView1.Name = "RadGridView1"
        Me.RadGridView1.ReadOnly = True
        Me.RadGridView1.ShowNoDataText = False
        Me.RadGridView1.Size = New System.Drawing.Size(1121, 435)
        Me.RadGridView1.TabIndex = 3
        Me.RadGridView1.Text = "RadGridView1"
        '
        'RadButtonZuordnen
        '
        Me.RadButtonZuordnen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButtonZuordnen.Location = New System.Drawing.Point(764, 488)
        Me.RadButtonZuordnen.Name = "RadButtonZuordnen"
        Me.RadButtonZuordnen.Size = New System.Drawing.Size(357, 24)
        Me.RadButtonZuordnen.TabIndex = 4
        Me.RadButtonZuordnen.Text = "Eichvorgang entfernen"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(774, 21)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Bitte denken Sie daran, beim Löschen von Eichungen die Fehlmengen der Eichmarken " &
    "zu korrigieren"
        '
        'frmWaagenLoeschen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 515)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadButtonZuordnen)
        Me.Controls.Add(Me.RadGridView1)
        Me.Name = "frmWaagenLoeschen"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "LÖSCHEN"
        CType(Me.RadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonZuordnen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGridView1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadButtonZuordnen As Telerik.WinControls.UI.RadButton
    Friend WithEvents Label1 As Label
End Class

