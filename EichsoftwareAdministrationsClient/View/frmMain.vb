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
        f.ShowDialog()
    End Sub

    Private Sub RadButtonEichmarkenStatistik_Click(sender As Object, e As EventArgs) Handles RadButtonEichmarkenStatistik.Click
        Dim f As New FrmAuswahllisteEichmarkenverwaltung
        f.ShowDialog()
    End Sub

    Private Sub RadButtonVerbindungsprotokoll_Click(sender As Object, e As EventArgs) Handles RadButtonVerbindungsprotokoll.Click
        Dim f As New FrmVerbindungsprotokoll
        f.ShowDialog()
    End Sub

    Private Sub RadButtonMogelstatistik_Click(sender As Object, e As EventArgs) Handles RadButtonMogelstatistik.Click
        Dim f As New frmMogelstatistik
        f.ShowDialog()
    End Sub

    Private Sub RadButtonFirmenZuordnung_Click(sender As Object, e As EventArgs) Handles RadButtonFirmenZuordnung.Click
        Dim f As New frmAuswahllisteFirmenVertragspartnerZuordnung
        f.ShowDialog()
    End Sub

    Private Sub RadButtonSync_Click(sender As Object, e As EventArgs) Handles RadButtonSync.Click
        Dim f As New frmDBSync
        f.ShowDialog()
    End Sub

    Private Sub RadButtonFirmenZusatzDaten_Click(sender As Object, e As EventArgs) Handles RadButtonFirmenZusatzDaten.Click
        Dim f As New frmFirmenZusatzdaten
        f.ShowDialog()
    End Sub

    Private Sub RadButtonPruefscheinnummersuche_Click(sender As Object, e As EventArgs) Handles RadButtonPruefscheinnummersuche.Click
        Dim f As New FrmPruefscheinnummersuche
        f.ShowDialog()
    End Sub

    Private Sub RadButtonStandardwaagen_Click(sender As Object, e As EventArgs) Handles RadButtonStandardwaagen.Click
        Dim f As New FrmStandardwaagen
        f.ShowDialog()
    End Sub
End Class