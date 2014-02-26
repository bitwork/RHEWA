Imports Telerik.WinControls.UI
Imports System.IO

Public Class FrmAuswahllisteEichmarkenverwaltung




#Region "Methoden"
    Private Sub LoadFromDatabase()
        Me.SuspendLayout()
        Using Context As New EichenEntities
            Dim Data = From Eichmarken In Context.ServerEichmarkenverwaltung Select Eichmarken
            RadGridView1.DataSource = Data.ToList
            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("ID").VisibleInColumnChooser = False

                'Unterstriche in Spaltennamen entfernen
                For Each col In RadGridView1.Columns
                    col.HeaderText = col.HeaderText.Replace("_", " ")
                Next
                'RadGridView1.Columns("BenannteStelleAnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("Eichsiegel13x13AnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("EichsiegelRundAnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("HinweismarkeGelochtAnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("GruenesMAnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("CEAnzahlMeldestand").IsVisible = False
                'RadGridView1.Columns("BenannteStelleAnzahlMeldestand").IsVisible = False
            Catch ex As Exception

            End Try

            Try
                'RadGridView1.Columns("BenannteStelleAnzahlMeldestand").HeaderText = "Benanntestelle Anzahl Meldestand"
                'RadGridView1.Columns("Eichsiegel13x13AnzahlMeldestand").HeaderText = "Eichsiegel 13x13 Anzahl Meldestand"
                'RadGridView1.Columns("EichsiegelRundAnzahlMeldestand").HeaderText = "Eichsiegel Rund Anzahl Meldestand"
                'RadGridView1.Columns("HinweismarkeGelochtAnzahlMeldestand").HeaderText = "Hinweismarke Gelocht Anzahl Meldestand"
                'RadGridView1.Columns("GruenesMAnzahlMeldestand").HeaderText = "Grünes M Anzahl Meldestand"
                'RadGridView1.Columns("CEAnzahlMeldestand").HeaderText = "CE Anzahl Meldestand"
                'RadGridView1.Columns("BenannteStelleAnzahlMeldestand").HeaderText = "Benannte StelleAnzahl Meldestand"

                'RadGridView1.Columns("HEKennung").HeaderText = "HE-Kennung "
                'RadGridView1.Columns("BenannteStelleAnzahl").HeaderText = "Benannte Stelle Anzahl "
                'RadGridView1.Columns("Eichsiegel13x13Anzahl").HeaderText = "Eichsiegel 13x13 Anzahl  "
                'RadGridView1.Columns("EichsiegelRundAnzahl").HeaderText = "Eichsiegel Rund Anzahl  "
                'RadGridView1.Columns("HinweismarkeGelochtAnzahl").HeaderText = "Hinweismarke Gelocht Anzahl"
                'RadGridView1.Columns("GruenesMAnzahl").HeaderText = "Grünes M Anzahl "
                'RadGridView1.Columns("CEAnzahl").HeaderText = "CE Anzahl"
                'RadGridView1.Columns("zerstoerteMarke0103").HeaderText = "zerstörte Marke 0103 "
                'RadGridView1.Columns("FehlmengeBenannteStelle0103").HeaderText = "Fehlmenge Benannte Stelle 0103 "
                'RadGridView1.Columns("FehlmengeBenannteStelle0111").HeaderText = "Fehlmenge Benannte Stelle 0111  "
                'RadGridView1.Columns("FehlmengeSicherungsmarkeklein").HeaderText = "Fehlmenge Sicherungsmarke Klein "
                'RadGridView1.Columns("FehlmengeSicherungsmarkegross").HeaderText = "Fehlmenge Sicherungsmarke Groß  "
                'RadGridView1.Columns("FehlmengeHinweismarken").HeaderText = "Fehlmenge Hinweismarken  "
                'RadGridView1.Columns("Archiv2007_Eichungen").HeaderText = "Benannte  "
                'RadGridView1.Columns("Archiv2007_BenannteStelle0298").HeaderText = "Benannte  "
                'RadGridView1.Columns("Archiv2007_Hinweismarke").HeaderText = "Benannte  "
                'RadGridView1.Columns("Archiv2007_SicherungsmarkeGross").HeaderText = "Benannte  "
                'RadGridView1.Columns("Archiv2007_SicherungsmarkeKlein").HeaderText = "Benannte  "



            Catch ex As Exception

            End Try


            RadGridView1.BestFitColumns()
        End Using
        Me.ResumeLayout()
    End Sub


    'Private Sub EditEichmarkenverwaltung()
    '    If RadGridView1.SelectedRows.Count > 0 Then
    '        'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
    '        If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
    '            Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
    '            SelectedID = RadGridView1.SelectedRows(0).Cells("ID").Value

    '            'neue Datenbankverbindung
    '            Using context As New EichenEntities
    '                'anzeigen des Dialogs zur Bearbeitung der Eichung
    '                Dim f As New FrmEichmarkenverwaltung(SelectedID)
    '                f.ShowDialog()

    '                'nach dem schließen des Dialogs aktualisieren
    '                LoadFromDatabase()
    '            End Using

    '        End If
    '    End If
    'End Sub
#End Region

#Region "Form Events"

    Private Sub FrmAuswahllisteWZ_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()

        'laden des Grid Layouts
        Try
            Using stream As New MemoryStream(Convert.FromBase64String(My.Settings.GridSettingsAuswahllisteEichmarkenverwaltung))
                RadGridView1.LoadLayout(stream)
            End Using
        Catch ex As Exception
            'konnte layout nicht finden
        End Try
    End Sub

    Private Sub FrmEichmarkenverwaltung_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'speichere Layout der beiden Grids

        Dim grid = Me.RadGridView1

        Using stream As New MemoryStream()
            grid.SaveLayout(stream)
            stream.Position = 0
            Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
            stream.Read(buffer, 0, buffer.Length)
            My.Settings.GridSettingsAuswahllisteEichmarkenverwaltung = Convert.ToBase64String(buffer)
            My.Settings.Save()
        End Using

    End Sub

    Private Sub RadButtonBearbeiten_Click(sender As Object, e As EventArgs) Handles RadButtonBearbeiten.Click
        'EditEichmarkenverwaltung()
    End Sub

#End Region

#Region "Grid Events"
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

    'Private Sub RadGridView1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles RadGridView1.CellFormatting
    '    Try
    '        Dim template As GridViewTemplate = e.Row.ViewTemplate

    '        If (e.Column.Name = "Benannte_Stelle_Anzahl" And e.Row.Cells("Benannte_Stelle_Anzahl").Value <= e.Row.Cells("Benannte_Stelle_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Eichsiegel_13x13_Anzahl" And e.Row.Cells("Eichsiegel_13x13_Anzahl").Value <= e.Row.Cells("Eichsiegel_13x13_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Eichsiegel_Rund_Anzahl" And e.Row.Cells("Eichsiegel_Rund_Anzahl").Value <= e.Row.Cells("Eichsiegel_Rund_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Hinweismarke_Gelocht_Anzahl" And e.Row.Cells("Hinweismarke_Gelocht_Anzahl").Value <= e.Row.Cells("Hinweismarke_Gelocht_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Grünes_M_Anzahl" And e.Row.Cells("Grünes_M_Anzahl").Value <= e.Row.Cells("Grünes_M_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "CE_Anzahl" And e.Row.Cells("CE_Anzahl").Value <= e.Row.Cells("CE_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        Else
    '            e.CellElement.DrawFill = False
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Linear
    '            e.CellElement.BackColor = Color.White



    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub RadGridView1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles RadGridView1.CellDoubleClick
        'EditEichmarkenverwaltung()

    End Sub
#End Region


End Class
