<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain

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
        Me.RadButtonAuswertegeraet = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonWaegezelle = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonLizenzen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonEichmarkenStatistik = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButtonEichungenloeschen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonStandardwaagen = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonFirmenZusatzDaten = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonFirmenZuordnung = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButtonPruefscheinnummersuche = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonMogelstatistik = New Telerik.WinControls.UI.RadButton()
        Me.RadButtonVerbindungsprotokoll = New Telerik.WinControls.UI.RadButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RadButtonSync = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButtonImportPruefscheine = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadButtonAuswertegeraet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonWaegezelle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonLizenzen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonEichmarkenStatistik, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadButtonEichungenloeschen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonStandardwaagen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonFirmenZusatzDaten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonFirmenZuordnung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadButtonPruefscheinnummersuche, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonMogelstatistik, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonVerbindungsprotokoll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButtonSync, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadButtonImportPruefscheine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadButtonAuswertegeraet
        '
        Me.RadButtonAuswertegeraet.Location = New System.Drawing.Point(5, 37)
        Me.RadButtonAuswertegeraet.Name = "RadButtonAuswertegeraet"
        Me.RadButtonAuswertegeraet.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonAuswertegeraet.TabIndex = 0
        Me.RadButtonAuswertegeraet.Text = "Auswertegeräte verwalten"
        '
        'RadButtonWaegezelle
        '
        Me.RadButtonWaegezelle.Location = New System.Drawing.Point(5, 67)
        Me.RadButtonWaegezelle.Name = "RadButtonWaegezelle"
        Me.RadButtonWaegezelle.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonWaegezelle.TabIndex = 1
        Me.RadButtonWaegezelle.Text = "Wägezellen verwalten"
        '
        'RadButtonLizenzen
        '
        Me.RadButtonLizenzen.Location = New System.Drawing.Point(5, 97)
        Me.RadButtonLizenzen.Name = "RadButtonLizenzen"
        Me.RadButtonLizenzen.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonLizenzen.TabIndex = 2
        Me.RadButtonLizenzen.Text = "Lizenzen verwalten"
        '
        'RadButtonEichmarkenStatistik
        '
        Me.RadButtonEichmarkenStatistik.Location = New System.Drawing.Point(7, 37)
        Me.RadButtonEichmarkenStatistik.Name = "RadButtonEichmarkenStatistik"
        Me.RadButtonEichmarkenStatistik.Size = New System.Drawing.Size(206, 24)
        Me.RadButtonEichmarkenStatistik.TabIndex = 3
        Me.RadButtonEichmarkenStatistik.Text = "Eichmarken Übersicht"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadButtonEichungenloeschen)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonStandardwaagen)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonFirmenZusatzDaten)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonFirmenZuordnung)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonAuswertegeraet)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonWaegezelle)
        Me.RadGroupBox1.Controls.Add(Me.RadButtonLizenzen)
        Me.RadGroupBox1.HeaderText = "Verwaltung"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(213, 258)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Verwaltung"
        '
        'RadButtonEichungenloeschen
        '
        Me.RadButtonEichungenloeschen.Location = New System.Drawing.Point(5, 217)
        Me.RadButtonEichungenloeschen.Name = "RadButtonEichungenloeschen"
        Me.RadButtonEichungenloeschen.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonEichungenloeschen.TabIndex = 6
        Me.RadButtonEichungenloeschen.Text = "Eichungen löschen"
        '
        'RadButtonStandardwaagen
        '
        Me.RadButtonStandardwaagen.Location = New System.Drawing.Point(5, 187)
        Me.RadButtonStandardwaagen.Name = "RadButtonStandardwaagen"
        Me.RadButtonStandardwaagen.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonStandardwaagen.TabIndex = 5
        Me.RadButtonStandardwaagen.Text = "Standardwaagen definieren"
        '
        'RadButtonFirmenZusatzDaten
        '
        Me.RadButtonFirmenZusatzDaten.Location = New System.Drawing.Point(5, 157)
        Me.RadButtonFirmenZusatzDaten.Name = "RadButtonFirmenZusatzDaten"
        Me.RadButtonFirmenZusatzDaten.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonFirmenZusatzDaten.TabIndex = 4
        Me.RadButtonFirmenZusatzDaten.Text = "Firmen Zusatzdaten pflegen"
        '
        'RadButtonFirmenZuordnung
        '
        Me.RadButtonFirmenZuordnung.Location = New System.Drawing.Point(5, 127)
        Me.RadButtonFirmenZuordnung.Name = "RadButtonFirmenZuordnung"
        Me.RadButtonFirmenZuordnung.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonFirmenZuordnung.TabIndex = 3
        Me.RadButtonFirmenZuordnung.Text = "Firmen Zuordnung"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadButtonPruefscheinnummersuche)
        Me.RadGroupBox2.Controls.Add(Me.RadButtonMogelstatistik)
        Me.RadGroupBox2.Controls.Add(Me.RadButtonVerbindungsprotokoll)
        Me.RadGroupBox2.Controls.Add(Me.RadButtonEichmarkenStatistik)
        Me.RadGroupBox2.HeaderText = "Statistik / Auswertung"
        Me.RadGroupBox2.Location = New System.Drawing.Point(231, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(218, 258)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = "Statistik / Auswertung"
        '
        'RadButtonPruefscheinnummersuche
        '
        Me.RadButtonPruefscheinnummersuche.Location = New System.Drawing.Point(7, 127)
        Me.RadButtonPruefscheinnummersuche.Name = "RadButtonPruefscheinnummersuche"
        Me.RadButtonPruefscheinnummersuche.Size = New System.Drawing.Size(206, 24)
        Me.RadButtonPruefscheinnummersuche.TabIndex = 5
        Me.RadButtonPruefscheinnummersuche.Text = "Prüfscheinnummer Suche"
        '
        'RadButtonMogelstatistik
        '
        Me.RadButtonMogelstatistik.Location = New System.Drawing.Point(7, 97)
        Me.RadButtonMogelstatistik.Name = "RadButtonMogelstatistik"
        Me.RadButtonMogelstatistik.Size = New System.Drawing.Size(206, 24)
        Me.RadButtonMogelstatistik.TabIndex = 4
        Me.RadButtonMogelstatistik.Text = "Mogel Statistik"
        '
        'RadButtonVerbindungsprotokoll
        '
        Me.RadButtonVerbindungsprotokoll.Location = New System.Drawing.Point(7, 67)
        Me.RadButtonVerbindungsprotokoll.Name = "RadButtonVerbindungsprotokoll"
        Me.RadButtonVerbindungsprotokoll.Size = New System.Drawing.Size(206, 24)
        Me.RadButtonVerbindungsprotokoll.TabIndex = 4
        Me.RadButtonVerbindungsprotokoll.Text = "Verbindungsprotokoll"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.EichsoftwareAdministrationsClient.My.Resources.Resources.RHEWA_Logo_35mm_600dpi
        Me.PictureBox1.Location = New System.Drawing.Point(9, 276)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(146, 62)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'RadButtonSync
        '
        Me.RadButtonSync.Location = New System.Drawing.Point(238, 314)
        Me.RadButtonSync.Name = "RadButtonSync"
        Me.RadButtonSync.Size = New System.Drawing.Size(203, 24)
        Me.RadButtonSync.TabIndex = 4
        Me.RadButtonSync.Text = "Datenbank Synchronisieren"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadButtonImportPruefscheine)
        Me.RadGroupBox3.HeaderText = "Statistik / Auswertung"
        Me.RadGroupBox3.Location = New System.Drawing.Point(455, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(218, 258)
        Me.RadGroupBox3.TabIndex = 6
        Me.RadGroupBox3.Text = "Statistik / Auswertung"
        '
        'RadButtonImportPruefscheine
        '
        Me.RadButtonImportPruefscheine.Location = New System.Drawing.Point(7, 37)
        Me.RadButtonImportPruefscheine.Name = "RadButtonImportPruefscheine"
        Me.RadButtonImportPruefscheine.Size = New System.Drawing.Size(206, 24)
        Me.RadButtonImportPruefscheine.TabIndex = 3
        Me.RadButtonImportPruefscheine.Text = "Prüfscheine importieren"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 350)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.RadButtonSync)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmMain"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Herstellerersteichung Verwaltung und Statistik"
        CType(Me.RadButtonAuswertegeraet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonWaegezelle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonLizenzen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonEichmarkenStatistik, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadButtonEichungenloeschen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonStandardwaagen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonFirmenZusatzDaten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonFirmenZuordnung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.RadButtonPruefscheinnummersuche, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonMogelstatistik, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonVerbindungsprotokoll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButtonSync, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.RadButtonImportPruefscheine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadButtonAuswertegeraet As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonWaegezelle As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonLizenzen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichmarkenStatistik As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents RadButtonVerbindungsprotokoll As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonMogelstatistik As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonFirmenZuordnung As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonSync As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonFirmenZusatzDaten As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonPruefscheinnummersuche As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonStandardwaagen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButtonEichungenloeschen As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadButtonImportPruefscheine As Telerik.WinControls.UI.RadButton
End Class

