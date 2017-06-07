Imports Telerik.WinControls.UI

Public Class FrmAuswahllisteLizenzen

    Private Sub RadButtonNeu_Click(sender As Object, e As EventArgs) Handles RadButtonNeu.Click
        CreateNewEichprozess()
    End Sub
    Private Sub CreateNewEichprozess()
        Dim f As New frmEingabeNeueLizenz
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New HerstellerersteichungEntities

            Try
                'abrufen alle Lizenzdaten mit Join auf Benutzer und firmentabelle
                Dim Data = From Lizenz In Context.ServerLizensierung
                           Join Benutz In Context.Benutzer On Benutz.ID Equals Lizenz.FK_BenutzerID
                           Join Firma In Context.Firmen On Firma.ID Equals Benutz.Firma_FK
                           Select New With
                                  {
                                      Lizenz.ID,
                                      Lizenz.LetzteAktivierung,
                                      Lizenz.Aktiv,
                                      Lizenz.RHEWALizenz,
                                      Lizenz.HEKennung,
                                      Benutz.Nachname,
                                      Benutz.Vorname,
                                  .APPlusLink = "http://rhewaapplus/APplusProd6/MasterData/adresseRec.aspx?adresse=" + Benutz.Telefon,
                                  Lizenz.Lizenzschluessel,
                                     .Firma = Firma.Name
                           }

                RadGridViewAuswahlliste.DataSource = Data.ToList

                clsTelerikHelper.CreateHyperlinkColumn(RadGridViewAuswahlliste, "APPlusLink")

                Try
                    RadGridViewAuswahlliste.Columns("ID").IsVisible = False
                Catch ex As Exception

                End Try

                'unbenennugn der Spalten
                Try
                    RadGridViewAuswahlliste.Columns("HEKennung").HeaderText = "HE-Kennung"
                    RadGridViewAuswahlliste.Columns("RHEWALizenz").HeaderText = "RHEWA Lizenz"
                    RadGridViewAuswahlliste.Columns("LetzteAktivierung").HeaderText = "Letzte Aktivierung"
                    RadGridViewAuswahlliste.Columns("Lizenzschluessel").HeaderText = "Lizenzschlüssel"

                Catch e As Exception
                End Try
                RadGridViewAuswahlliste.BestFitColumns()
            Catch e As Exception
            End Try

        End Using
    End Sub


    Private Sub CreateHyperlinkColumn()

    End Sub
    'Private Sub LoadFromDatabase()
    '    Using Context As New HerstellerersteichungEntities

    '        Try
    '            Dim Data = From Lizenz In Context.ServerLizensierung Select Lizenz
    '            RadGridViewAuswahlliste.DataSource = Data.ToList
    '            Try
    '                RadGridViewAuswahlliste.Columns("ID").IsVisible = False
    '                RadGridViewAuswahlliste.Columns("Lizenzschluessel").IsVisible = False
    '                RadGridViewAuswahlliste.Columns("FK_BenutzerID").IsVisible = False

    '            Catch ex As Exception

    '            End Try

    '            'unbenennugn der Spalten
    '            Try
    '                RadGridViewAuswahlliste.Columns("HEKennung").HeaderText = "HE-Kennung"
    '                RadGridViewAuswahlliste.Columns("RHEWALizenz").HeaderText = "RHEWA Lizenz"
    '                RadGridViewAuswahlliste.Columns("LetzteAktivierung").HeaderText = "Letzte Aktivierung"

    '            Catch e As Exception
    '            End Try
    '            RadGridViewAuswahlliste.BestFitColumns()
    '        Catch e As Exception
    '        End Try

    '    End Using
    'End Sub
    Private Sub EditEichprozess()
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value

                'neue Datenbankverbindung
                'anzeigen des Dialogs zur Bearbeitung der Eichung
                Dim f As New frmEingabeNeueLizenz(SelectedID)
                f.ShowDialog()

                'nach dem schließen des Dialogs aktualisieren
                LoadFromDatabase()

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