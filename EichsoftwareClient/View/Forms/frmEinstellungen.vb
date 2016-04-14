Public Class FrmEinstellungen

#Region "Events"
    Private Sub FrmEinstellungen_Load(sender As Object, e As EventArgs) Handles Me.Load
        'vorauswahl der Radioboxen anhand von gespeicherten Settingwerten
        SetRadioButtons()
        'formatierung der Datumswerte:
        Formatcontrols()
    End Sub

    ''' <summary>
    '''  Speichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonOK_Click(sender As Object, e As EventArgs) Handles RadButtonOK.Click
        Speichern()
    End Sub

    ''' <summary>
    ''' schliessen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonAbbrechen_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        Me.Close()


    End Sub

    Private Sub RadButtonGridSettingsZuruecksetzen_Click(sender As Object, e As EventArgs) Handles RadButtonGridSettingsZuruecksetzen.Click
        AktuellerBenutzer.Instance.GridSettings = AktuellerBenutzer.Instance.GridDefaultSettings
        AktuellerBenutzer.Instance.GridSettingsRhewa = AktuellerBenutzer.Instance.GridDefaultSettingsRhewa
        AktuellerBenutzer.SaveSettings()
        Me.DialogResult = DialogResult.Retry
        Me.Close()
    End Sub

    ''' <summary>
    ''' setzen der Radioboxen und de/aktivieren der Steuerelemente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' speicher methode der einstellungen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Speichern()
        Dim alterSyncmodus As String = AktuellerBenutzer.Instance.Synchronisierungsmodus 'variable wird genutzt um zu prüfen ob überhaupt Änderungen vorgenommen werden müssen
        Dim alterSyncAbWert As String = AktuellerBenutzer.Instance.SyncAb
        Dim alterSyncBisWert As String = AktuellerBenutzer.Instance.SyncBis


        If RadRadioButtonSyncAlles.IsChecked Then
            AktuellerBenutzer.Instance.Synchronisierungsmodus = "Alles"
            AktuellerBenutzer.Instance.SyncAb = "01.01.2000"
            AktuellerBenutzer.Instance.SyncBis = "31.12.2999"
        ElseIf RadRadioButtonSyncSeit.IsChecked Then
            AktuellerBenutzer.Instance.Synchronisierungsmodus = "Ab"
            AktuellerBenutzer.Instance.SyncAb = RadDateTimePickerSince.Value
            AktuellerBenutzer.Instance.SyncBis = "31.12.2999"
        ElseIf RadRadioButtonSyncZwischen.IsChecked Then
            AktuellerBenutzer.Instance.Synchronisierungsmodus = "Zwischen"
            AktuellerBenutzer.Instance.SyncAb = RadDateTimePickerStart.Value
            AktuellerBenutzer.Instance.SyncBis = RadDateTimePickerEnd.Value
        End If

        'nehme Änderung an Syncverhalten vor
        If alterSyncmodus <> AktuellerBenutzer.Instance.Synchronisierungsmodus Or alterSyncAbWert <> AktuellerBenutzer.Instance.SyncAb Or alterSyncBisWert <> AktuellerBenutzer.Instance.SyncBis Then
            AktuellerBenutzer.Instance.LetztesUpdate = "01.01.2000"
            AktuellerBenutzer.Instance.HoleAlleeigenenEichungenVomServer = True
            AktuellerBenutzer.SaveSettings()

            'initiere neuen Download der Daten durch Dialogresult = ok. dies wird in ucoEichprozessauswahlliste abgefragt
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            Exit Sub
        End If

        'es wurde nichts geändert, also muss auch nichts heruntergeladen werden
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' vorauswahl der Radioboxen anhand von gespeicherten Settingwerten und setzten der Datumswerte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRadioButtons()
        'vorauswahl der Radioboxen anhand von gespeicherten Settingwerten und setzten der Datumswerte
        RadDateTimePickerSince.Value = Date.Now
        RadDateTimePickerStart.Value = Date.Now
        RadDateTimePickerEnd.Value = Date.Now

        Select Case AktuellerBenutzer.Instance.Synchronisierungsmodus
            Case Is = "Alles"
                RadRadioButtonSyncAlles.IsChecked = True
            Case Is = "Ab"
                RadRadioButtonSyncSeit.IsChecked = True
                RadDateTimePickerSince.Value = AktuellerBenutzer.Instance.SyncAb
            Case Is = "Zwischen"
                RadRadioButtonSyncZwischen.IsChecked = True
                RadDateTimePickerStart.Value = AktuellerBenutzer.Instance.SyncAb
                RadDateTimePickerEnd.Value = AktuellerBenutzer.Instance.SyncBis
        End Select
    End Sub

    ''' <summary>
    ''' Lokalisierung
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Formatcontrols()
        'formatierung der Datumswerte:
        Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
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
                RadDateTimePickerEnd.Culture = New System.Globalization.CultureInfo("en")
                RadDateTimePickerStart.Culture = New System.Globalization.CultureInfo("en")
                RadDateTimePickerSince.Culture = New System.Globalization.CultureInfo("en")
        End Select
    End Sub


#End Region

End Class
