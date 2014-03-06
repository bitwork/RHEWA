Public Class FrmAuswahllisteAWG

    Private Sub RadButtonNeu_Click(sender As Object, e As EventArgs) Handles RadButtonNeu.Click
        CreateNewEichprozess()
    End Sub
    Private Sub CreateNewEichprozess()
        Dim f As New FrmAuswertegeraet
        f.ShowDialog()
        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New EichenEntities
            Dim Data = From Lookup_Auswertegeraet In Context.ServerLookup_Auswertegeraet Select Lookup_Auswertegeraet
            RadGridViewAuswahlliste.DataSource = Data.ToList
            Try
                RadGridViewAuswahlliste.Columns("ID").IsVisible = False
                '   RadGridViewAuswahlliste.Columns("ServerEichprozess").IsVisible = False
                RadGridViewAuswahlliste.Columns("ServerMogelstatistik").IsVisible = False
            Catch ex As Exception

            End Try

            'unbenennugn der Spalten
            Try
                RadGridViewAuswahlliste.Columns("Pruefbericht").HeaderText = "Prüfbericht"
                RadGridViewAuswahlliste.Columns("MAXAnzahlTeilungswerteEinbereichswaage").HeaderText = "Maximale Anzahl Teilungswerte Einbereichswaage"
                RadGridViewAuswahlliste.Columns("MAXAnzahlTeilungswerteMehrbereichswaage").HeaderText = "Maximale Anzahl Teilungswerte Mehrbereichswaage"
                RadGridViewAuswahlliste.Columns("GrenzwertLastwiderstandMIN").HeaderText = "Grenzwert Lastwiderstand MIN"
                RadGridViewAuswahlliste.Columns("GrenzwertLastwiderstandMAX").HeaderText = "Grenzwert Lastwiderstand MAX"
                RadGridViewAuswahlliste.Columns("GrenzwertTemperaturbereichMIN").HeaderText = "Grenzwert Temperaturbereich MIN"
                RadGridViewAuswahlliste.Columns("GrenzwertTemperaturbereichMAX").HeaderText = "Grenzwert Temperaturbereich MAX"
                RadGridViewAuswahlliste.Columns("BruchteilEichfehlergrenze").HeaderText = "Bruchteil Eichfehlergrenze"
                RadGridViewAuswahlliste.Columns("KabellaengeQuerschnitt").HeaderText = "Kabellänge/ Querschnitt "
                RadGridViewAuswahlliste.Columns("ErstellDatum").HeaderText = "Erstellungsdatum"




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
                    Dim f As New FrmAuswertegeraet(SelectedID)
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
        If e.Row.RowElementType.Equals(GetType(Telerik.WinControls.UI.GridDataRowElement)) Then

            EditEichprozess()
        End If

    End Sub
End Class
