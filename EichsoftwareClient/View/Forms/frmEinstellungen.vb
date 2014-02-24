Public Class FrmEinstellungen

#Region "Events"
    Private Sub FrmEinstellungen_Load(sender As Object, e As EventArgs) Handles Me.Load
        'vorauswahl der Radioboxen anhand von gespeicherten Settingwerten
        SetRadioButtons()
        'formatierung der Datumswerte:
        Formatcontrols()
    End Sub

    Private Sub RadButtonOK_Click(sender As Object, e As EventArgs) Handles RadButtonOK.Click
        Speichern()
    End Sub

    Private Sub RadButtonAbbrechen_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        Me.Close()
    End Sub

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
#End Region

#Region "Funktionen"
    Private Sub Speichern()
        Dim alterSyncmodus As String = My.Settings.Syncronisierungsmodus 'variable wird genutzt um zu prüfen ob überhaupt Änderungen vorgenommen werden müssen
        Dim alterSyncAbWert As String = My.Settings.SyncAb
        Dim alterSyncBisWert As String = My.Settings.SyncBis


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
        If alterSyncmodus <> My.Settings.Syncronisierungsmodus Or alterSyncAbWert <> My.Settings.SyncAb Or alterSyncBisWert <> My.Settings.SyncBis Then
            My.Settings.LetztesUpdate = "01.01.2000"
            My.Settings.HoleAlleEigenenEichungenVomServer = True
            My.Settings.Save()

            'initiere neuen Download der Daten durch Dialogresult = ok. dies wird in ucoEichprozessauswahlliste abgefragt
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            Exit Sub

        End If

        'es wurde nichts geändert, also muss auch nichts heruntergeladen werden
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub SetRadioButtons()
        'vorauswahl der Radioboxen anhand von gespeicherten Settingwerten und setzten der Datumswerte
        RadDateTimePickerSince.Value = Date.Now
        RadDateTimePickerStart.Value = Date.Now
        RadDateTimePickerEnd.Value = Date.Now

        Select Case My.Settings.Syncronisierungsmodus
            Case Is = "Alles"
                RadRadioButtonSyncAlles.IsChecked = True
            Case Is = "Ab"
                RadRadioButtonSyncSeit.IsChecked = True
                RadDateTimePickerSince.Value = My.Settings.SyncAb
            Case Is = "Zwischen"
                RadRadioButtonSyncZwischen.IsChecked = True
                RadDateTimePickerStart.Value = My.Settings.SyncAb
                RadDateTimePickerEnd.Value = My.Settings.SyncBis
        End Select
    End Sub
    Private Sub Formatcontrols()
        'formatierung der Datumswerte:
        Select Case My.Settings.AktuelleSprache.ToLower
            Case Is = "de"
                RadDateTimePickerEnd.Culture = New System.Globalization.CultureInfo("de")
                RadDateTimePickerStart.Culture = New System.Globalization.CultureInfo("de")
                RadDateTimePickerSince.Culture = New System.Globalization.CultureInfo("de")

            Case Is = "pl"
                RadDateTimePickerEnd.Culture = New System.Globalization.CultureInfo("pl")
                RadDateTimePickerStart.Culture = New System.Globalization.CultureInfo("pl")
                RadDateTimePickerSince.Culture = New System.Globalization.CultureInfo("pl")

            Case Is = "en"
                RadDateTimePickerEnd.Culture = New System.Globalization.CultureInfo("en")
                RadDateTimePickerStart.Culture = New System.Globalization.CultureInfo("en")
                RadDateTimePickerSince.Culture = New System.Globalization.CultureInfo("en")

            Case Else
        End Select
    End Sub
#End Region
  
End Class
