Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class FrmPruefscheinnummersuche

    Private Sub RadButtonSuchen_Click(sender As Object, e As EventArgs) Handles RadButtonSuchen.Click
        Suchen(RadTextBoxControlPruefscheinnummer.Text)
    End Sub

    Private Sub Suchen(ByVal Suchtext As string)
        Using Context As New EichenEntities

            Context.Configuration.LazyLoadingEnabled = False
            Context.Configuration.ProxyCreationEnabled = False

            Dim Data = From Eichprozess In Context.ServerEichprozess.Include("ServerLookup_Waegezelle").Include("ServerEichprotokoll") Where Eichprozess.ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer.Contains(Suchtext)
                       Join Lookup In Context.ServerLookup_Vorgangsstatus On Eichprozess.FK_Vorgangsstatus Equals Lookup.ID
                       Join Lookup2 In Context.ServerLookup_Bearbeitungsstatus On Eichprozess.FK_Bearbeitungsstatus Equals Lookup2.ID
                       Select New With
{
Eichprozess.ID,
.Pruefscheinnummer = Eichprozess.ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer,
      .Status = Lookup.Status,
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

            '   Dim Data = From Eichprozesse In Context.ServerEichprozess.Include("ServerEichprotokoll") Select Eichprozesse

            RadGridView1.DataSource = Data.ToArray

            Try
                RadGridView1.Columns("ID").IsVisible = False

                RadGridView1.Columns("Pruefscheinnummer").HeaderText = "Prüfscheinnummer"
                RadGridView1.Columns("Lookup_Waegezelle").HeaderText = "WZ"
                RadGridView1.Columns("Lookup_Waagentyp").HeaderText = "Waagentyp"
                RadGridView1.Columns("Lookup_Waagenart").HeaderText = "Waagenart"
                RadGridView1.Columns("Lookup_Auswertegeraet").HeaderText = "AWG"
                RadGridView1.Columns("ZurBearbeitungGesperrtDurch").HeaderText = "gesperrt durch"
                RadGridView1.Columns("NeuWZ").HeaderText = "neue WZ"

            Catch ex As Exception
            End Try
            RadGridView1.AutoExpandGroups = True
            RadGridView1.BestFitColumns()
        End Using
    End Sub
End Class