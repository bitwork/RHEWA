Public Class FrmStandardwaagen

    Private Sub FrmStandardwaagen_Load(sender As Object, e As EventArgs) Handles Me.Load
        LadeDaten()
    End Sub

    Private Sub RadButtonZuordnen_Click(sender As Object, e As EventArgs) Handles RadButtonZuordnen.Click
        StandardwaageDeklarieren()
    End Sub

    Private Sub RadGridView1_CellDoubleClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridView1.CellDoubleClick
        StandardwaageDeklarieren()
    End Sub

    Private Sub LadeDaten()
        Using Context As New HerstellerersteichungEntities

            Context.Configuration.LazyLoadingEnabled = False
            Context.Configuration.ProxyCreationEnabled = False

            Dim Data = From Eichprozess In Context.ServerEichprozess.Include("ServerLookup_Waegezelle").Include("ServerEichprotokoll") Where Eichprozess.FK_Bearbeitungsstatus = 3
                       Join Lookup2 In Context.ServerLookup_Bearbeitungsstatus On Eichprozess.FK_Bearbeitungsstatus Equals Lookup2.ID
                       Select New With
{
Eichprozess.ID,
.Standardwaage = Eichprozess.Standardwaage,
.Pruefscheinnummer = Eichprozess.ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer,
                              Eichprozess.Vorgangsnummer,
      .Fabriknummer = Eichprozess.ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
      .Lookup_Waegezelle = Eichprozess.ServerLookup_Waegezelle.Typ,
      .Lookup_Waagentyp = Eichprozess.ServerLookup_Waagentyp.Typ,
      .Lookup_Waagenart = Eichprozess.ServerLookup_Waagenart.Art,
      .Lookup_Auswertegeraet = Eichprozess.ServerLookup_Auswertegeraet.Typ,
      .Sachbearbeiter = Eichprozess.ServerEichprotokoll.Identifikationsdaten_Pruefer,
.ZurBearbeitungGesperrtDurch = Eichprozess.ZurBearbeitungGesperrtDurch,
          .NeuWZ = Eichprozess.ServerLookup_Waegezelle.Neu,
.Bearbeitungsstatus = Lookup2.Status,
.Uploaddatum = Eichprozess.UploadDatum
       }

            'databinding
            RadGridView1.DataSource = Data.ToArray

            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("Vorgangsnummer").IsVisible = False
                RadGridView1.Columns("Bearbeitungsstatus").IsVisible = False
                RadGridView1.Columns("ZurBearbeitungGesperrtDurch").IsVisible = False
                RadGridView1.Columns("Pruefscheinnummer").HeaderText = "Prüfscheinnummer"
                RadGridView1.Columns("Lookup_Waegezelle").HeaderText = "WZ"
                RadGridView1.Columns("Lookup_Waagentyp").HeaderText = "Waagentyp"
                RadGridView1.Columns("Lookup_Waagenart").HeaderText = "Waagenart"
                RadGridView1.Columns("Lookup_Auswertegeraet").HeaderText = "AWG"
                RadGridView1.Columns("NeuWZ").HeaderText = "neue WZ"
            Catch ex As Exception
            End Try

            RadGridView1.AutoExpandGroups = True
            RadGridView1.BestFitColumns()
        End Using
    End Sub

    Private Sub StandardwaageDeklarieren()
        If Not RadGridView1.Rows.Count = 0 Then
            If Not RadGridView1.SelectedRows.Count = 0 Then
                If Not RadGridView1.SelectedRows(0) Is Nothing Then
                    Dim ID As String
                    ID = RadGridView1.SelectedRows(0).Cells("ID").Value

                    Dim Standardwaage As Boolean = False
                    Standardwaage = RadGridView1.SelectedRows(0).Cells("Standardwaage").Value

                    Dim Fragetext As String
                    If Standardwaage = True Then
                        Fragetext = "Möchten Sie den aktuellen Datensatz von der Auswahlliste der Standardwaagen entfernen?"
                    Else
                        Fragetext = "Möchten Sie den aktuellen Datensatz als Standardwaagen deklarieren?"
                    End If
                    If MessageBox.Show(Fragetext, "Frage", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        Using context As New HerstellerersteichungEntities
                            Dim Prozess = (From prozesse In context.ServerEichprozess Where prozesse.ID = ID).FirstOrDefault
                            If Not Prozess Is Nothing Then
                                If Not Prozess.Standardwaage Is Nothing Then
                                    Prozess.Standardwaage = Not Prozess.Standardwaage  'umkehren der Auswahl
                                Else
                                    Prozess.Standardwaage = True
                                End If
                            End If
                            context.SaveChanges()
                        End Using
                    End If
                End If
            End If
        End If
            LadeDaten()
    End Sub
End Class