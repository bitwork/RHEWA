Imports Telerik.WinControls.UI

Public Class clsTelerikHelper
    ''' <summary>
    '''  a method which returns true if there is child data and false if there is no child data for a parent row
    ''' </summary>
    ''' <param name="rowInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsExpandable(rowInfo As GridViewRowInfo) As Boolean
        If rowInfo.ChildRows IsNot Nothing AndAlso rowInfo.ChildRows.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Public Shared Sub CreateHyperlinkColumn(radGridViewAuswahlliste As RadGridView, columnNameToReplace As String)
        Try
            For Each col As Telerik.WinControls.UI.GridViewColumn In radGridViewAuswahlliste.Columns
                If col.FieldName = columnNameToReplace Then
                    radGridViewAuswahlliste.Columns.RemoveAt(col.Index)
                    Dim newcol As Telerik.WinControls.UI.GridViewHyperlinkColumn = New Telerik.WinControls.UI.GridViewHyperlinkColumn()
                    col.Width = 300
                    radGridViewAuswahlliste.Columns.Add(newcol)
                    newcol.FieldName = columnNameToReplace
                    newcol.HeaderText = columnNameToReplace
                    Exit For

                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub CreateHyperlinkColumn(radGridViewAuswahlliste As RadMultiColumnComboBox, columnNameToReplace As String)
        Try
            For Each col As Telerik.WinControls.UI.GridViewColumn In radGridViewAuswahlliste.Columns
                If col.FieldName = columnNameToReplace Then
                    radGridViewAuswahlliste.Columns.RemoveAt(col.Index)
                    Dim newcol As Telerik.WinControls.UI.GridViewHyperlinkColumn = New Telerik.WinControls.UI.GridViewHyperlinkColumn()
                    col.Width = 300
                    radGridViewAuswahlliste.Columns.Add(newcol)
                    newcol.FieldName = columnNameToReplace
                    newcol.HeaderText = columnNameToReplace
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

End Class