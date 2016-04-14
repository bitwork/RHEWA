Public Class FrmAuswahlStandardwaage

    Private Sub FrmAuswahlStandardwaage_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadStandardwagen()
    End Sub

    Private Sub LoadStandardwagen()
        'laden der Daten vom Webservice
        Dim datasource = clsWebserviceFunctions.GetServerStandardwaagen
        'databinding
        RadGridViewStandardwaagen.DataSource = datasource
        RadGridViewStandardwaagen.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill

        'formatierung des Grids
        RadGridViewStandardwaagen.Columns("ID").IsVisible = False
        RadGridViewStandardwaagen.Columns("Vorgangsnummer").IsVisible = False
        RadGridViewStandardwaagen.Columns("Bearbeitungsstatus").IsVisible = False
        RadGridViewStandardwaagen.Columns("GesperrtDurch").IsVisible = False
        RadGridViewStandardwaagen.Columns("NeueWZ").IsVisible = False
        RadGridViewStandardwaagen.Columns("AnhangPfad").IsVisible = False
        RadGridViewStandardwaagen.Columns("Eichbevollmaechtigter").IsVisible = False
        RadGridViewStandardwaagen.Columns("Fabriknummer").IsVisible = True

    End Sub


    Private Sub RadGridViewStandardwaagen_CellClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridViewStandardwaagen.CellDoubleClick

        StandardwaageDeklarieren()

    End Sub


    Private Sub RadButtonZuordnen_Click(sender As Object, e As EventArgs) Handles RadButtonZuordnen.Click
        StandardwaageDeklarieren()
    End Sub

    ''' <summary>
    ''' markiert / entfernt Flag zur Deklaration einer Standardwaage eines Vorgangs 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StandardwaageDeklarieren()
        If Not RadGridViewStandardwaagen.Rows.Count = 0 Then
            If Not RadGridViewStandardwaagen.SelectedRows(0) Is Nothing Then
                If TypeOf RadGridViewStandardwaagen.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then

                    Dim ID As String
                    ID = RadGridViewStandardwaagen.SelectedRows(0).Cells("Vorgangsnummer").Value
                    If Not ID.Equals("") Then
                        If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Kopieren, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                            Dim Fabriknummer As String
                            Fabriknummer = InputBox("Neue Fabriknummer")

                            'holen einer lokalen Kopie des Server eichprozesses
                            Dim objClientEichprozess = clsWebserviceFunctions.GetLokaleKopieVonEichprozess(ID, Fabriknummer)
                            If Not objClientEichprozess Is Nothing Then
                                'anzeigen des Dialogs zur Bearbeitung der Eichung
                                Dim f As New FrmMainContainer(objClientEichprozess)
                                f.ShowDialog()
                                'nach dem schließen des Dialogs aktualisieren
                                Me.Close()
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class
