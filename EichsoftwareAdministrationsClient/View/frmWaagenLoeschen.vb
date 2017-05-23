Public Class frmWaagenLoeschen

    Private Sub FrmStandardwaagen_Load(sender As Object, e As EventArgs) Handles Me.Load
        LadeDaten()
    End Sub

    Private Sub RadButtonZuordnen_Click(sender As Object, e As EventArgs) Handles RadButtonZuordnen.Click
        Loeschen()
    End Sub

    Private Sub RadGridView1_CellDoubleClick(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles RadGridView1.CellDoubleClick
        Loeschen()
    End Sub

    Private Sub LadeDaten()
        Using Context As New HerstellerersteichungEntities

            Context.Configuration.LazyLoadingEnabled = False
            Context.Configuration.ProxyCreationEnabled = False

            Dim Data = From Eichprozess In Context.ServerEichprozess.Include("ServerLookup_Waegezelle").Include("ServerEichprotokoll")
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
                RadGridView1.Columns("Bearbeitungsstatus").IsVisible = True
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

    Private Sub Loeschen()
        If Not RadGridView1.SelectedRows.Count = 0 Then
            If Not RadGridView1.SelectedRows.Count = 0 Then
                If Not RadGridView1.SelectedRows(0) Is Nothing Then
                    Dim ID As String
                    ID = RadGridView1.SelectedRows(0).Cells("ID").Value

                    Dim Standardwaage As Boolean = False
                    Standardwaage = RadGridView1.SelectedRows(0).Cells("Standardwaage").Value

                    Dim Fragetext As String
                    Fragetext = "Möchten Sie den aktuellen Datensatz wirklich entfernen? Er kann nicht wiederhergestellt werden"

                    If MessageBox.Show(Fragetext, "Frage", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        Using context As New HerstellerersteichungEntities
                            Dim Prozess = (From prozesse In context.ServerEichprozess Where prozesse.ID = ID).FirstOrDefault
                            Dim EichprotokollID = Prozess.FK_Eichprotokoll
                            Dim Protokoll = (From Protokolle In context.ServerEichprotokoll Where Protokolle.ID = EichprotokollID).FirstOrDefault

                            Dim Konform = (From Vorgaenge In context.ServerKompatiblitaetsnachweis Where Vorgaenge.ID = Prozess.FK_Kompatibilitaetsnachweis).FirstOrDefault
                            Dim queryMogelstatistik = From a In context.ServerMogelstatistik Where a.FK_Eichprozess = Prozess.ID
                            For Each obj In queryMogelstatistik
                                context.ServerMogelstatistik.Remove(obj)
                            Next
                            Dim query = From a In context.ServerPruefungAnsprechvermoegen Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query
                                context.ServerPruefungAnsprechvermoegen.Remove(obj)
                            Next
                            Dim query2 = From a In context.ServerPruefungAussermittigeBelastung Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query2
                                context.ServerPruefungAussermittigeBelastung.Remove(obj)
                            Next
                            Dim query4 = From a In context.ServerPruefungLinearitaetFallend Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query4
                                context.ServerPruefungLinearitaetFallend.Remove(obj)
                            Next
                            Dim query5 = From a In context.ServerPruefungLinearitaetSteigend Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query5
                                context.ServerPruefungLinearitaetSteigend.Remove(obj)
                            Next
                            Dim query6 = From a In context.ServerPruefungStabilitaetGleichgewichtslage Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query6
                                context.ServerPruefungStabilitaetGleichgewichtslage.Remove(obj)
                            Next
                            Dim query7 = From a In context.ServerPruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query7
                                context.ServerPruefungStaffelverfahrenErsatzlast.Remove(obj)
                            Next
                            Dim query8 = From a In context.ServerPruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query8
                                context.ServerPruefungStaffelverfahrenNormallast.Remove(obj)
                            Next
                            Dim query9 = From a In context.ServerPruefungWiederholbarkeit Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query9
                                context.ServerPruefungWiederholbarkeit.Remove(obj)
                            Next
                            Dim query10 = From a In context.ServerPruefungRollendeLasten Where a.FK_Eichprotokoll = EichprotokollID
                            For Each obj In query10
                                context.ServerPruefungRollendeLasten.Remove(obj)
                            Next

                            If Konform Is Nothing = False Then
                                context.ServerKompatiblitaetsnachweis.Remove(Konform)

                            End If
                            context.ServerEichprotokoll.Remove(Protokoll)
                            context.ServerEichprozess.Remove(Prozess)
                            context.SaveChanges()
                        End Using
                    End If
                End If
            End If
        End If
        LadeDaten()
    End Sub
End Class