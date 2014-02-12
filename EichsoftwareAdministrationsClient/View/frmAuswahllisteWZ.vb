Public Class FrmAuswahllisteWZ

    Private Sub RadButtonNeu_Click(sender As Object, e As EventArgs) Handles RadButtonNeu.Click
        CreateNewEichprozess()
    End Sub
    Private Sub CreateNewEichprozess()
        Dim f As New frmWaegezelle
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New EichenEntities
            Dim Data = From Lookup_Waegezelle In Context.ServerLookup_Waegezelle Select Lookup_Waegezelle
            RadGridViewAuswahlliste.DataSource = Data.ToList

            Try
                RadGridViewAuswahlliste.Columns("ID").IsVisible = False
                RadGridViewAuswahlliste.Columns("ServerEichprozess").IsVisible = False
                RadGridViewAuswahlliste.Columns("ServerMogelstatistik").IsVisible = False
                RadGridViewAuswahlliste.Columns("Hoechsteteilungsfaktor").HeaderText = "Höchstteilungsfaktor"
            Catch ex As Exception

            End Try
            RadGridViewAuswahlliste.BestFitColumns()
        End Using
    End Sub

    Private Sub EditEichprozess()
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value

                'neue Datenbankverbindung
                Using context As New EichenEntities
                    'anzeigen des Dialogs zur Bearbeitung der Eichung
                    Dim f As New frmWaegezelle(SelectedID)
                    f.ShowDialog()

                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()
                End Using

            End If
        End If
    End Sub

    Private Sub RadButtonBearbeiten_Click(sender As Object, e As EventArgs) Handles RadButtonBearbeiten.Click
        EditEichprozess()
    End Sub


   

    Private Sub FrmAuswahllisteWZ_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()
    End Sub

    Private Sub RadGridViewAuswahlliste_CellDoubleClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridViewAuswahlliste.CellDoubleClick
        EditEichprozess()
    End Sub
End Class
