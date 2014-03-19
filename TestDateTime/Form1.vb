Imports System.Globalization

Public Class Form1

    Private Sub RadButtonChangeLanguage_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToGerman.Click, RadButtonChangeLanguageToEnglish.Click, RadButtonChangeLanguageToPolish.Click
        If sender.Equals(RadButtonChangeLanguageToEnglish) Then
            changeCulture("en")
        ElseIf sender.Equals(RadButtonChangeLanguageToGerman) Then
            changeCulture("de")
        ElseIf sender.Equals(RadButtonChangeLanguageToPolish) Then
            changeCulture("pl")
        End If
    End Sub

    Private Sub changeCulture(ByVal Code As String)
        Dim culture As CultureInfo = CultureInfo.GetCultureInfo(Code)
        Threading.Thread.CurrentThread.CurrentUICulture = culture

        Using ef As New HerstellerersteichungEntities
            Dim obj As New Table_1
            obj.date = Now
            ef.Table_1.Add(obj)
            ef.SaveChanges()
        End Using
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Using ef As New HerstellerersteichungEntities
            Dim obj As New Table_1
            obj.date = Now
            ef.Table_1.Add(obj)
            ef.SaveChanges()
        End Using
    End Sub
End Class
