﻿Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class frmMogelstatistik

    Private Sub FrmVerbindungsprotokoll_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()

    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New HerstellerersteichungEntities
            Dim Data = From mogelstatistik In Context.ServerMogelstatistik Select mogelstatistik

            RadGridView1.DataSource = Data.ToArray
            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("ServerLookup_Waegezelle").IsVisible = False
                RadGridView1.Columns("ServerEichprozess").IsVisible = False
                RadGridView1.Columns("ServerLookup_Auswertegeraet").IsVisible = False

                'Gruppierung
                Try
                    Dim descriptor As New GroupDescriptor()
                    descriptor.GroupNames.Add("FK_Eichprozess", ListSortDirection.Ascending)
                    Me.RadGridView1.GroupDescriptors.Add(descriptor)
                Catch e As Exception
                End Try

                Try
                    Dim sortdescriptor = New SortDescriptor()
                    sortdescriptor.Direction = ListSortDirection.Ascending
                    sortdescriptor.PropertyName = "Kompatiblitaet_Waage_FabrikNummer"
                    Me.RadGridView1.SortDescriptors.Add(sortdescriptor)
                Catch e As Exception

                End Try

            Catch ex As Exception
            End Try
            RadGridView1.AutoExpandGroups = True
            RadGridView1.BestFitColumns()
        End Using
    End Sub
End Class