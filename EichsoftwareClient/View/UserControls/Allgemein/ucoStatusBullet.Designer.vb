<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucoStatusBullet
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblCurrentElement = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.PictureBoxStatus = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCurrentElement
        '
        Me.lblCurrentElement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentElement.Location = New System.Drawing.Point(0, 27)
        Me.lblCurrentElement.Name = "lblCurrentElement"
        Me.lblCurrentElement.Size = New System.Drawing.Size(86, 50)
        Me.lblCurrentElement.TabIndex = 0
        Me.lblCurrentElement.Text = "Current Element Long Text"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(92, 80)
        Me.ShapeContainer1.TabIndex = 2
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RectangleShape1.BorderWidth = 2
        Me.RectangleShape1.Location = New System.Drawing.Point(1, 1)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(90, 75)
        Me.RectangleShape1.Visible = False
        '
        'PictureBoxStatus
        '
        Me.PictureBoxStatus.BackColor = System.Drawing.Color.Transparent
        Me.PictureBoxStatus.Image = Global.EichsoftwareClient.My.Resources.Resources.bullet_yellow
        Me.PictureBoxStatus.Location = New System.Drawing.Point(28, 0)
        Me.PictureBoxStatus.Name = "PictureBoxStatus"
        Me.PictureBoxStatus.Size = New System.Drawing.Size(32, 32)
        Me.PictureBoxStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBoxStatus.TabIndex = 1
        Me.PictureBoxStatus.TabStop = False
        '
        'ucoStatusBullet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Controls.Add(Me.lblCurrentElement)
        Me.Controls.Add(Me.PictureBoxStatus)
        Me.Name = "ucoStatusBullet"
        Me.Size = New System.Drawing.Size(92, 80)
        CType(Me.PictureBoxStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCurrentElement As System.Windows.Forms.Label
    Friend WithEvents PictureBoxStatus As System.Windows.Forms.PictureBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape

End Class
