Imports System.Data.SqlClient

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim psi As New ProcessStartInfo("D:\Projekte\GitHub\RHEWA\EichsoftwareSyncClientVB\Repository\Call_DTS.bat")
        psi.RedirectStandardError = False
        psi.RedirectStandardOutput = False
        psi.CreateNoWindow = False
        psi.WindowStyle = ProcessWindowStyle.Normal
        psi.UseShellExecute = False
        psi.WorkingDirectory = "D:\Projekte\GitHub\RHEWA\EichsoftwareSyncClientVB\Repository"

        Dim process As Process = process.Start(psi)

        process.WaitForExit()

        Dim file As New IO.FileInfo("D:\Projekte\GitHub\RHEWA\EichsoftwareSyncClientVB\Repository\FixSQLFile.sql")
        If file.Exists Then
            Dim fileContents As String
            fileContents = My.Computer.FileSystem.ReadAllText(file.FullName)

            If Not fileContents.Equals("") Then
                '     MessageBox.Show(fileContents)

                Dim conn As New SqlConnection()
                conn.ConnectionString = "Data Source=h2223265.stratoserver.net;Initial Catalog=Herstellerersteichung;Persist Security Info=True;User ID=Eichen;Password=Eichen2013"
                conn.ConnectionString = "Data Source=WIN7MOBDEV01;Initial Catalog=Herstellerersteichung;Persist Security Info=True;User ID=sa;Password=Test1234"

                Dim cmd As New SqlClient.SqlCommand
                cmd.Connection = conn
                cmd.CommandText = fileContents
                cmd.CommandType = CommandType.Text


                Dim rowCount As Integer
                Dim previousConnectionState As ConnectionState
                previousConnectionState = conn.State
                Try
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    rowCount = cmd.ExecuteNonQuery()
                Catch ex As SqlException
                    MessageBox.Show(ex.Message)
                Finally
                    If previousConnectionState = ConnectionState.Closed Then
                        conn.Close()
                    End If
                End Try

            End If
        End If

        Try
            file.Delete()
        Catch ex As Exception

        End Try
    End Sub
End Class
