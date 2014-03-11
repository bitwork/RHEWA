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
End Class
