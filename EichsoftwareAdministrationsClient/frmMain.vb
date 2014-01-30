Public Class frmMain
#Region "Constructor"
    Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        Telerik.WinControls.ThemeResolutionService.LoadPackageResource("EichsoftwareAdministrationsClient.RHEWAGREEN.tssp") 'Pfad zur Themedatei
        Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "RHEWAGREEN" 'standard Themename



        'aktuelle Sprache der Anwendung auf vorher gewählte Sprache setzen
        RuntimeLocalizer.ChangeCulture(Me, My.Settings.AktuelleSprache)
    End Sub
#End Region

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
      
    End Sub

    Private Sub RadButtonRadButtonAuswertegeraet_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonRadButtonAuswertegeraet.Click
        Dim f As New FrmAuswahllisteAWG
        f.ShowDialog()
    End Sub

    Private Sub RadButtonWaegezelle_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonWaegezelle.Click
        Dim f As New FrmAuswahllisteWZ
        f.ShowDialog()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim f As New FrmAuswahllisteLizenzen
        f.ShowDialog()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Dim f As New FrmAuswahllisteEichmarkenverwaltung
        f.ShowDialog()
    End Sub
End Class
