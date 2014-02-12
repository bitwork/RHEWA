Public Class FrmEinstellungen

    Private Sub RadRadioButtonSyncAlles_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadRadioButtonSyncZwischen.ToggleStateChanged, RadRadioButtonSyncSeit.ToggleStateChanged, RadRadioButtonSyncAlles.ToggleStateChanged
        If RadRadioButtonSyncAlles.IsChecked Then
            RadDateTimePickerSince.Enabled = False
            RadDateTimePickerEnd.Enabled = False
            RadDateTimePickerStart.Enabled = False
        ElseIf RadRadioButtonSyncSeit.IsChecked Then
            RadDateTimePickerSince.Enabled = True
            RadDateTimePickerEnd.Enabled = False
            RadDateTimePickerStart.Enabled = False
        ElseIf RadRadioButtonSyncZwischen.IsChecked Then
            RadDateTimePickerSince.Enabled = False
            RadDateTimePickerEnd.Enabled = True
            RadDateTimePickerStart.Enabled = True
        End If
    End Sub

    Private Sub RadButtonOK_Click(sender As Object, e As EventArgs) Handles RadButtonOK.Click
        Dim alterSyncmodus As String = My.Settings.Syncronisierungsmodus

        If RadRadioButtonSyncAlles.IsChecked Then
            My.Settings.Syncronisierungsmodus = "Alles"
            My.Settings.SyncAb = "01.01.2000"
            My.Settings.SyncBis = "31.12.2999"

        ElseIf RadRadioButtonSyncSeit.IsChecked Then
            My.Settings.Syncronisierungsmodus = "Ab"
            My.Settings.SyncAb = RadDateTimePickerSince.Value
            My.Settings.SyncBis = "31.12.2999"
        ElseIf RadRadioButtonSyncZwischen.IsChecked Then
            My.Settings.Syncronisierungsmodus = "Zwischen"
            My.Settings.SyncAb = RadDateTimePickerStart.Value
            My.Settings.SyncBis = RadDateTimePickerEnd.Value
        End If

        'nehme Änderung an Syncverhalten vor
        If alterSyncmodus <> My.Settings.Syncronisierungsmodus Then
            My.Settings.LetztesUpdate = "01.01.2000"
            My.Settings.HoleAlleEigenenEichungenVomServer = True
            My.Settings.Save()


            'versuche Verbindung zu RHEWA aufzubauen

            'wenn nicht möglich Abbruch

            'prüfe ob es noch nicht hochgeladende Eichungen gibt, wenn Ja Abbruch bzw. Hinweismeldung

            'lösche lokale Datenbank

            'syncronisiere Teildaten

            'TODO Syncbutton im Hauptmenü um Fallunterscheidung für Synchronisierungsmodus 

        End If
        Me.Close()
    End Sub

    Private Sub FrmEinstellungen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case My.Settings.Syncronisierungsmodus
            Case Is = "Alles"
                RadRadioButtonSyncAlles.IsChecked = True
            Case Is = "Ab"
                RadRadioButtonSyncSeit.IsChecked = True
            Case Is = "Zwischen"
                RadRadioButtonSyncZwischen.IsChecked = True

        End Select
    End Sub

    Private Sub RadButtonAbbrechen_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        Me.Close()

    End Sub
End Class
