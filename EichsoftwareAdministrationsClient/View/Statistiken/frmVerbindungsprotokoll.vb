Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class FrmVerbindungsprotokoll



    Private Sub FrmVerbindungsprotokoll_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()


    End Sub

    Private Sub LoadFromDatabase()
        Using Context As New EichenEntities
            Dim Data = From Verbindungsprotokoll In Context.ServerVerbindungsprotokoll Select Verbindungsprotokoll
                       
            RadGridView1.DataSource = Data.ToList
            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("Lizenzschluessel_FK").HeaderText = "Lizenzschlüssel"

                'Gruppierung
                Try
                    Dim descriptor As New GroupDescriptor()
                    descriptor.GroupNames.Add("Lizenzschluessel_FK", ListSortDirection.Ascending)
                    Me.RadGridView1.GroupDescriptors.Add(descriptor)
                Catch e As Exception
                End Try

                Try
                    Dim sortdescriptor = New SortDescriptor()
                    sortdescriptor.Direction = ListSortDirection.Ascending
                    sortdescriptor.PropertyName = "Timestamp"
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
