Public Class frmMain
#Region "Constructor"
    Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        Telerik.WinControls.ThemeResolutionService.LoadPackageResource("EichsoftwareAdministrationsClient.RHEWAGREEN.tssp") 'Pfad zur Themedatei
        Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "RHEWAGREEN" 'standard Themename
    End Sub
#End Region

  

    Private Sub RadButtonAuswertegeraetAuswertegeraet_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonAuswertegeraet.Click
        Dim f As New FrmAuswahllisteAWG
        f.ShowDialog()
    End Sub

    Private Sub RadButtonWaegezelle_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonWaegezelle.Click
        Dim f As New FrmAuswahllisteWZ
        f.ShowDialog()
    End Sub

    Private Sub RadButtonLizenzen_Click(sender As Object, e As EventArgs) Handles RadButtonLizenzen.Click
        Dim f As New FrmAuswahllisteLizenzen
        f.Show()
    End Sub

    Private Sub RadButtonEichmarkenStatistik_Click(sender As Object, e As EventArgs) Handles RadButtonEichmarkenStatistik.Click
        Dim f As New FrmAuswahllisteEichmarkenverwaltung
        f.Show()
    End Sub

    Private Sub RadButtonVerbindungsprotokoll_Click(sender As Object, e As EventArgs) Handles RadButtonVerbindungsprotokoll.Click
        Dim f As New frmVerbindungsprotokoll
        f.Show()
    End Sub

    Private Sub RadButtonMogelstatistik_Click(sender As Object, e As EventArgs) Handles RadButtonMogelstatistik.Click
        Dim f As New FrmMogelstatistik
        f.Show()
    End Sub

    Private Sub RadButtonFirmenZuordnung_Click(sender As Object, e As EventArgs) Handles RadButtonFirmenZuordnung.Click
        Dim f As New frmAuswahllisteFirmenVertragspartnerZuordnung
        f.Show()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim f As New frmDBSync
        f.show()
    End Sub
End Class
