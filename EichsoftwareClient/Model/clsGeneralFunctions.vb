Public Class clsGeneralFunctions
    ''' <summary>
    ''' Methode zum ermitteln der anzahl der benötigten Nullstellen nach dem RHEWA System
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRHEWADecimalDigits(ByVal value As String) As Integer
        Try
            If value Is Nothing Then
                Return 0
            End If
            If value = "" Then
                Return 0
            End If
            If CDec(value) >= 1 Then
                Return 1
            ElseIf CDec(value) >= 0.1 Then
                Return 3
            ElseIf CDec(value) >= 0.01 Then
                Return 3
            ElseIf CDec(value) >= 0.001 Then
                Return 4
            ElseIf CDec(value) >= 0.0001 Then
                Return 5
            Else
                Return 6
            End If

        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class