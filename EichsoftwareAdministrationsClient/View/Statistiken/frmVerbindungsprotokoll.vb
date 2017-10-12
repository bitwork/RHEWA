Imports Telerik.WinControls.Data
Imports System.ComponentModel

Public Class FrmVerbindungsprotokoll

    Private Sub FrmVerbindungsprotokoll_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase(False)
        FormatGrid()

    End Sub

    Private Sub RadButtonLoadAll_Click(sender As Object, e As EventArgs) Handles RadButtonLoadAll.Click
        LoadFromDatabase(True)
    End Sub

    Private Sub LoadFromDatabase(ByVal loadAll As Boolean)
        Using Context As New HerstellerersteichungEntities
            'Dim Data = From Verbindungsprotokoll In Context.ServerVerbindungsprotokoll Select Verbindungsprotokoll
            Dim Data As New List(Of ServerVerbindungsprotokoll)
            If loadAll Then
                Data.AddRange(Context.ServerVerbindungsprotokoll.ToList)
            Else
                Data.AddRange(Context.ServerVerbindungsprotokoll.OrderByDescending(Function(x) x.Zeitstempel).Take(4000).ToList)
            End If
            RadGridView1.DataSource = Data.ToList
        End Using
    End Sub

    Private Sub FormatGrid()
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
    End Sub
End Class