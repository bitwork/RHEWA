Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class FrmPruefscheinnummersuche

    Private Sub RadButtonSuchen_Click(sender As Object, e As EventArgs) Handles RadButtonSuchen.Click
        Suchen(RadTextBoxControlPruefscheinnummer.Text)
    End Sub

    Private Sub Suchen(ByVal Suchtext As String)
        Using Context As New EichenEntities

            Context.Configuration.LazyLoadingEnabled = False
            Context.Configuration.ProxyCreationEnabled = False

            Dim Data = From Eichprozess In Context.ServerEichprozess.Include("ServerLookup_Waegezelle").Include("ServerEichprotokoll") Where Eichprozess.ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer.Contains(Suchtext)
                        Join Lookup In Context.ServerLookup_Vorgangsstatus On Eichprozess.FK_Vorgangsstatus Equals Lookup.ID _
                        Join Lookup2 In Context.ServerLookup_Bearbeitungsstatus On Eichprozess.FK_Bearbeitungsstatus Equals Lookup2.ID _
                                             Select New With _
           { _
                Eichprozess.ID, _
                .Pruefscheinnummer = Eichprozess.ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer, _
                            .Status = Lookup.Status, _
                            Eichprozess.Vorgangsnummer, _
                            .Fabriknummer = Eichprozess.ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                            .Lookup_Waegezelle = Eichprozess.ServerLookup_Waegezelle.Typ, _
                            .Lookup_Waagentyp = Eichprozess.ServerLookup_Waagentyp.Typ, _
                            .Lookup_Waagenart = Eichprozess.ServerLookup_Waagenart.Art, _
                            .Lookup_Auswertegeraet = Eichprozess.ServerLookup_Auswertegeraet.Typ, _
                            .Sachbearbeiter = Eichprozess.ServerEichprotokoll.Identifikationsdaten_Pruefer, _
                   .ZurBearbeitungGesperrtDurch = Eichprozess.ZurBearbeitungGesperrtDurch, _
                                .NeuWZ = Eichprozess.ServerLookup_Waegezelle.Neu, _
                .Bearbeitungsstatus = Lookup2.Status, _
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


                'Gruppierung
                'Try
                '    Dim descriptor As New GroupDescriptor()
                '    descriptor.GroupNames.Add("FK_Eichprozess", ListSortDirection.Ascending)
                '    Me.RadGridView1.GroupDescriptors.Add(descriptor)
                'Catch e As Exception
                'End Try


                'Try
                '    Dim sortdescriptor = New SortDescriptor()
                '    sortdescriptor.Direction = ListSortDirection.Ascending
                '    sortdescriptor.PropertyName = "ID"
                '    Me.RadGridView1.SortDescriptors.Add(sortdescriptor)
                'Catch e As Exception

                'End Try


            Catch ex As Exception
            End Try
            RadGridView1.AutoExpandGroups = True
            RadGridView1.BestFitColumns()
        End Using
    End Sub
End Class
