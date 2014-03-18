Public Class clsGeneralFunctions
    ''' <summary>
    ''' Funktion zum Zählen von Nullstellen von einem übergebenen Wert
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CountDecimalDigits(value As String) As Integer
        Dim possibleChars As Char() = "0123456789,".ToCharArray()
        Dim decimalPoints As Integer = 0
        For Each ch As Char In value
            If Array.IndexOf(possibleChars, ch) < 0 Then
                Throw New Exception()
            End If
            If ch = ","c Then
                decimalPoints += 1
            End If
        Next
        If decimalPoints > 1 Then
            Throw New Exception()
        End If
        If decimalPoints = 0 Then
            Return 0
        End If
        Return value.Length - value.IndexOf(","c) - 1
    End Function

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
