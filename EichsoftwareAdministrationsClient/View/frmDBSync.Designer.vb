<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDBSync
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Me.RadioButtonSyncStratoRHEWA = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSyncRHEWAStrato = New System.Windows.Forms.RadioButton()
        Me.RadWaitingBar1 = New Telerik.WinControls.UI.RadWaitingBar()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadListControlLog = New System.Windows.Forms.TextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadListControlSQLQuery = New System.Windows.Forms.TextBox()
        Me.TimerLog = New System.Windows.Forms.Timer(Me.components)
        Me.ButtonSync = New Telerik.WinControls.UI.RadButton()
        Me.RadioButtonSyncStratoDEV = New System.Windows.Forms.RadioButton()
        CType(Me.RadWaitingBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.ButtonSync, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButtonSyncStratoRHEWA
        '
        Me.RadioButtonSyncStratoRHEWA.AutoSize = True
        Me.RadioButtonSyncStratoRHEWA.Checked = True
        Me.RadioButtonSyncStratoRHEWA.Location = New System.Drawing.Point(13, 13)
        Me.RadioButtonSyncStratoRHEWA.Name = "RadioButtonSyncStratoRHEWA"
        Me.RadioButtonSyncStratoRHEWA.Size = New System.Drawing.Size(211, 17)
        Me.RadioButtonSyncStratoRHEWA.TabIndex = 0
        Me.RadioButtonSyncStratoRHEWA.Text = "Sync von Strato zu RHEWA (Standard)"
        Me.RadioButtonSyncStratoRHEWA.UseVisualStyleBackColor = True
        '
        'RadioButtonSyncRHEWAStrato
        '
        Me.RadioButtonSyncRHEWAStrato.AutoSize = True
        Me.RadioButtonSyncRHEWAStrato.Location = New System.Drawing.Point(13, 36)
        Me.RadioButtonSyncRHEWAStrato.Name = "RadioButtonSyncRHEWAStrato"
        Me.RadioButtonSyncRHEWAStrato.Size = New System.Drawing.Size(318, 17)
        Me.RadioButtonSyncRHEWAStrato.TabIndex = 1
        Me.RadioButtonSyncRHEWAStrato.Text = "Sync von RHEWA zu Strato (Nach Ausfall des Strato Servers)"
        Me.RadioButtonSyncRHEWAStrato.UseVisualStyleBackColor = True
        '
        'RadWaitingBar1
        '
        Me.RadWaitingBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadWaitingBar1.Location = New System.Drawing.Point(0, 510)
        Me.RadWaitingBar1.Name = "RadWaitingBar1"
        Me.RadWaitingBar1.Size = New System.Drawing.Size(781, 24)
        Me.RadWaitingBar1.TabIndex = 4
        Me.RadWaitingBar1.Text = "RadWaitingBar1"
        Me.RadWaitingBar1.Visible = False
        '
        'BackgroundWorker1
        '
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(13, 114)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(756, 378)
        Me.SplitContainer1.SplitterDistance = 366
        Me.SplitContainer1.TabIndex = 6
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadListControlLog)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Log"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(366, 378)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Log"
        '
        'RadListControlLog
        '
        Me.RadListControlLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadListControlLog.BackColor = System.Drawing.Color.White
        Me.RadListControlLog.Location = New System.Drawing.Point(2, 31)
        Me.RadListControlLog.Multiline = True
        Me.RadListControlLog.Name = "RadListControlLog"
        Me.RadListControlLog.ReadOnly = True
        Me.RadListControlLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RadListControlLog.Size = New System.Drawing.Size(362, 345)
        Me.RadListControlLog.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadListControlSQLQuery)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Erzeugtes SQL Query"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(386, 378)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Erzeugtes SQL Query"
        '
        'RadListControlSQLQuery
        '
        Me.RadListControlSQLQuery.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadListControlSQLQuery.BackColor = System.Drawing.Color.White
        Me.RadListControlSQLQuery.Location = New System.Drawing.Point(2, 31)
        Me.RadListControlSQLQuery.Multiline = True
        Me.RadListControlSQLQuery.Name = "RadListControlSQLQuery"
        Me.RadListControlSQLQuery.ReadOnly = True
        Me.RadListControlSQLQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RadListControlSQLQuery.Size = New System.Drawing.Size(382, 345)
        Me.RadListControlSQLQuery.TabIndex = 0
        '
        'TimerLog
        '
        Me.TimerLog.Interval = 6000
        '
        'ButtonSync
        '
        Me.ButtonSync.Location = New System.Drawing.Point(15, 73)
        Me.ButtonSync.Name = "ButtonSync"
        Me.ButtonSync.Size = New System.Drawing.Size(110, 24)
        Me.ButtonSync.TabIndex = 2
        Me.ButtonSync.Text = "Daten abgleichen"
        '
        'RadioButtonSyncStratoDEV
        '
        Me.RadioButtonSyncStratoDEV.AutoSize = True
        Me.RadioButtonSyncStratoDEV.Location = New System.Drawing.Point(358, 13)
        Me.RadioButtonSyncStratoDEV.Name = "RadioButtonSyncStratoDEV"
        Me.RadioButtonSyncStratoDEV.Size = New System.Drawing.Size(140, 17)
        Me.RadioButtonSyncStratoDEV.TabIndex = 7
        Me.RadioButtonSyncStratoDEV.Text = "Sync von Strato zu DEV"
        Me.RadioButtonSyncStratoDEV.UseVisualStyleBackColor = True
        '
        'frmDBSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(781, 534)
        Me.Controls.Add(Me.RadioButtonSyncStratoDEV)
        Me.Controls.Add(Me.ButtonSync)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadWaitingBar1)
        Me.Controls.Add(Me.RadioButtonSyncRHEWAStrato)
        Me.Controls.Add(Me.RadioButtonSyncStratoRHEWA)
        Me.Name = "frmDBSync"
        Me.Text = "DB Sync"
        CType(Me.RadWaitingBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.ButtonSync, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButtonSyncStratoRHEWA As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSyncRHEWAStrato As System.Windows.Forms.RadioButton
    Friend WithEvents RadWaitingBar1 As Telerik.WinControls.UI.RadWaitingBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadListControlSQLQuery As System.Windows.Forms.TextBox
    Friend WithEvents RadListControlLog As System.Windows.Forms.TextBox
    Friend WithEvents TimerLog As System.Windows.Forms.Timer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ButtonSync As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadioButtonSyncStratoDEV As System.Windows.Forms.RadioButton

End Class
