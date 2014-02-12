Imports Telerik.WinControls.UI

Public Class FrmAuswahllisteEichmarkenverwaltung

    Private Sub RadButtonNeu_Click(sender As Object, e As EventArgs)
        CreateNewEichmarkenverwaltungObjekt()
    End Sub
    Private Sub CreateNewEichmarkenverwaltungObjekt()
        Dim f As New FrmAuswertegeraet
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New EichenEntities
            Dim Data = From Eichmarken In Context.ServerEichmarkenverwaltung Select Eichmarken
            RadGridView1.DataSource = Data.ToList
            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("BenannteStelleAnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("Eichsiegel13x13AnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("EichsiegelRundAnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("HinweismarkeGelochtAnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("GruenesMAnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("CEAnzahlMeldestand").IsVisible = False
                RadGridView1.Columns("BenannteStelleAnzahlMeldestand").IsVisible = False
            Catch ex As Exception

            End Try
            

            RadGridView1.BestFitColumns()
        End Using
    End Sub


    Private Sub EditEichmarkenverwaltung()
        If RadGridView1.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridView1.SelectedRows(0).Cells("ID").Value

                'neue Datenbankverbindung
                Using context As New EichenEntities
                    'anzeigen des Dialogs zur Bearbeitung der Eichung
                    Dim f As New FrmEichmarkenverwaltung(SelectedID)
                    f.ShowDialog()

                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()
                End Using

            End If
        End If
    End Sub

    Private Sub RadButtonBearbeiten_Click(sender As Object, e As EventArgs) Handles RadButtonBearbeiten.Click
        EditEichmarkenverwaltung()
    End Sub


 
    Private Sub FrmAuswahllisteWZ_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()
    End Sub

    Private Sub RadGridView1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles RadGridView1.CellFormatting
        Try
            Dim template As GridViewTemplate = e.Row.ViewTemplate

            If (e.Column.Name = "BenannteStelleAnzahl" And e.Row.Cells("BenannteStelleAnzahl").Value <= e.Row.Cells("BenannteStelleAnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
          
            ElseIf (e.Column.Name = "Eichsiegel13x13Anzahl" And e.Row.Cells("Eichsiegel13x13Anzahl").Value <= e.Row.Cells("Eichsiegel13x13AnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
            ElseIf (e.Column.Name = "EichsiegelRundAnzahl" And e.Row.Cells("EichsiegelRundAnzahl").Value <= e.Row.Cells("EichsiegelRundAnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
            ElseIf (e.Column.Name = "HinweismarkeGelochtAnzahl" And e.Row.Cells("HinweismarkeGelochtAnzahl").Value <= e.Row.Cells("HinweismarkeGelochtAnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
            ElseIf (e.Column.Name = "GruenesMAnzahl" And e.Row.Cells("GruenesMAnzahl").Value <= e.Row.Cells("GruenesMAnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
            ElseIf (e.Column.Name = "CEAnzahl" And e.Row.Cells("CEAnzahl").Value <= e.Row.Cells("CEAnzahlMeldestand").Value) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
            Else
                e.CellElement.DrawFill = False
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Linear
                e.CellElement.BackColor = Color.White



            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadGridView1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles RadGridView1.CellDoubleClick
        EditEichmarkenverwaltung()

    End Sub
End Class
