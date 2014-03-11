Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class frmAuswahllisteFirmenVertragspartnerZuordnung

    Private Sub RadButtonNeu_Click(sender As Object, e As EventArgs) Handles RadButtonNeu.Click
        CreateNewZuordnung()
    End Sub
    Private Sub CreateNewZuordnung()
        Dim f As New frmEingabeFirmenVertragspartnerZuordnung
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub


    Private Sub LoadFromDatabase()
        Using Context As New EichenEntities

            Try
                'abrufen alle Lizenzdaten mit Join auf Benutzer und firmentabelle
                Dim Data = From Eichenzuordnung In Context.ServerLookupVertragspartnerFirma
                   Join Firma In Context.Firmen On Firma.ID Equals Eichenzuordnung.Firma_FK
                   Join Firma2 In Context.Firmen On Firma2.ID Equals Eichenzuordnung.Vertragspartner_FK
                    Select New With
                 {
                     Eichenzuordnung.ID, _
                    .Hauptfirma = Firma.Name, _
                .Vertragspartner = Firma2.Name, _
                .HauptfirmaID = Firma.ID, _
                .VertragspartnerID = Firma2.ID
          }
                RadGridViewAuswahlliste.DataSource = Nothing
                RadGridViewAuswahlliste.DataSource = Data.ToList

                FormatGrid()
            Catch e As Exception
            End Try


        End Using
    End Sub
    Private Sub FormatGrid()
        Try
            RadGridViewAuswahlliste.Columns("ID").IsVisible = False
            RadGridViewAuswahlliste.Columns("HauptfirmaID").IsVisible = False
            RadGridViewAuswahlliste.Columns("VertragspartnerID").IsVisible = False
        Catch ex As Exception
        End Try

        'Gruppierung
        Try
            Dim descriptor As New GroupDescriptor()
            descriptor.GroupNames.Add("Hauptfirma", ListSortDirection.Ascending)
            Me.RadGridViewAuswahlliste.GroupDescriptors.Add(descriptor)
        Catch e As Exception
        End Try
        Try
            Dim sortdescriptor = New SortDescriptor()
            sortdescriptor.Direction = ListSortDirection.Ascending
            sortdescriptor.PropertyName = "Hauptfirma"
            Me.RadGridViewAuswahlliste.SortDescriptors.Add(sortdescriptor)
        Catch e As Exception

        End Try
        Try
            Dim sortdescriptor = New SortDescriptor()
            sortdescriptor.Direction = ListSortDirection.Ascending
            sortdescriptor.PropertyName = "Vertragspartner"
            Me.RadGridViewAuswahlliste.SortDescriptors.Add(sortdescriptor)
        Catch e As Exception

        End Try

        RadGridViewAuswahlliste.AutoExpandGroups = True
        RadGridViewAuswahlliste.ShowGroupedColumns = True


        RadGridViewAuswahlliste.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
    End Sub


    Private Sub EditFirmenzuordnung()
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value

                'neue Datenbankverbindung
                'anzeigen des Dialogs zur Bearbeitung der Eichung
                Dim f As New frmEingabeFirmenVertragspartnerZuordnung(SelectedID)
                f.ShowDialog()

                'nach dem schließen des Dialogs aktualisieren
                LoadFromDatabase()

            End If
        End If
    End Sub

    Private Sub RadButtonBearbeiten_Click(sender As Object, e As EventArgs) Handles RadButtonBearbeiten.Click
        EditFirmenzuordnung()
    End Sub

    Private Sub FrmAuswahllisteWZ_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()
    End Sub
    Private Sub RadGridViewAuswahlliste_CellDoubleClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridViewAuswahlliste.CellDoubleClick
        If e.Row.RowElementType.Equals(GetType(Telerik.WinControls.UI.GridDataRowElement)) Then
            EditFirmenzuordnung()
        End If
    End Sub
End Class
