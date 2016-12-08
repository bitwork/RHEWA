Imports System.Net.FtpClient
Imports System.IO
Imports System.Net

Public Class clsFTP
    ''' <summary>
    ''' Event welches Fortschrittin KByte des Uploads / Downloads weiterreicht
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <remarks></remarks>
    Public Event ReportFTPProgress(ByVal Progress As Integer)

    ''' <summary>
    ''' Lädt Datei vom Pfad an FTP hoch
    ''' </summary>
    ''' <param name="pFTPServer"></param>
    ''' <param name="pUsername"></param>
    ''' <param name="pPassword"></param>
    ''' <param name="UploadfilePath"></param>
    ''' <returns>Voller Pfad der Datei auf FTP Server</returns>
    ''' <remarks></remarks>
    Public Function UploadFiletoFTP(ByVal pFTPServer As String, ByVal pUsername As String, ByVal pPassword As String, ByVal UploadfilePath As String) As String
        Dim FTPUploadPath As String = ""

        Using conn As New FtpClient()
            'FTP Upload
            conn.Host = pFTPServer
            conn.Credentials = New NetworkCredential(pUsername, pPassword)
            conn.Connect()

            If conn.IsConnected Then
                Dim file As New FileInfo(UploadfilePath)

                Dim extension As String = file.Extension
                Dim Filename As String = file.Name.Replace(extension, "")
                Dim FileNameOriginal As String = Filename

                'check if file exists
                Dim counter As Integer = 0
                If conn.FileExists(Filename & extension) Then
                    'versuche es erneut
                    Do
                        counter += 1
                        Filename = FileNameOriginal & "(" & counter & ")"
                    Loop While conn.FileExists(Filename & extension)
                End If

                Using ostream As Stream = conn.OpenWrite(Filename & extension)
                    ' istream.Position is incremented accordingly to the writes you perform
                    FTPUploadPath = Filename & extension

                    Try
                        Dim sumbytes As Integer
                        Const buffer As Integer = 2048
                        Dim contentRead As Byte() = New Byte(buffer - 1) {}
                        Dim bytesRead As Integer

                        Using fs As FileStream = file.OpenRead()
                            Do
                                bytesRead = fs.Read(contentRead, 0, buffer)
                                sumbytes += bytesRead

                                If bytesRead > 0 Then
                                    ostream.Write(contentRead, 0, bytesRead)
                                    RaiseEvent ReportFTPProgress(sumbytes)

                                End If
                            Loop While (bytesRead > 0)
                            fs.Close()
                        End Using

                    Finally

                        ostream.Close()
                    End Try
                End Using
            End If

            Return FTPUploadPath
        End Using
    End Function
    ''' <summary>
    ''' Lädt Datei vom Pfad an FTP hoch
    ''' </summary>
    ''' <param name="pFTPServer"></param>
    ''' <param name="pUsername"></param>
    ''' <param name="pPassword"></param>
    ''' <param name="UploadfilePath"></param>
    ''' <returns>Voller Pfad der Datei auf FTP Server</returns>
    ''' <remarks></remarks>
    Public Function UploadDatabaseFiletoFTP(ByVal pFTPServer As String, ByVal pUsername As String,
                                            ByVal pPassword As String, ByVal UploadfilePath As String,
                                            WindowsUsername As String) As String
        Dim FTPUploadPath As String = ""

        Using conn As New FtpClient()
            'FTP Upload
            conn.Host = pFTPServer
            conn.Credentials = New NetworkCredential(pUsername, pPassword)
            conn.Connect()

            If conn.IsConnected Then
                Dim file As New FileInfo(UploadfilePath)

                Dim extension As String = file.Extension
                Dim Filename As String = file.Name.Replace(extension, "")
                Dim ServerFilename As String = "Datenbanken\" + Filename + WindowsUsername
                Dim FileNameOriginal As String = Filename
                'check if file exists
                Dim counter As Integer = 0
                If conn.FileExists(Filename & extension) Then
                    'versuche es erneut
                    Do
                        counter += 1
                        Filename = FileNameOriginal & "(" & counter & ")"
                    Loop While conn.FileExists(Filename & extension)
                End If

                Using ostream As Stream = conn.OpenWrite(ServerFilename & extension)
                    ' istream.Position is incremented accordingly to the writes you perform
                    FTPUploadPath = Filename & extension

                    Try
                        Dim sumbytes As Integer
                        Const buffer As Integer = 2048
                        Dim contentRead As Byte() = New Byte(buffer - 1) {}
                        Dim bytesRead As Integer

                        Using fs As FileStream = file.OpenRead()
                            Do
                                bytesRead = fs.Read(contentRead, 0, buffer)
                                sumbytes += bytesRead

                                If bytesRead > 0 Then
                                    ostream.Write(contentRead, 0, bytesRead)
                                    RaiseEvent ReportFTPProgress(sumbytes)

                                End If
                            Loop While (bytesRead > 0)
                            fs.Close()
                        End Using

                    Finally

                        ostream.Close()
                    End Try
                End Using
            End If

            Return FTPUploadPath
        End Using
    End Function
    ''' <summary>
    ''' Lädt Datei vom FTP Herunter
    ''' </summary>
    ''' <param name="pFTPServer"></param>
    ''' <param name="pUsername"></param>
    ''' <param name="pPassword"></param>
    ''' <param name="FTPFilePath"></param>
    ''' <param name="LocalFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadFileFromFTP(ByVal pFTPServer As String, ByVal pUsername As String, ByVal pPassword As String, ByVal FTPFilePath As String, ByVal LocalFilePath As String) As String

        Using conn As New FtpClient()
            'FTP Upload
            conn.Host = pFTPServer
            conn.Credentials = New NetworkCredential(pUsername, pPassword)
            conn.Connect()

            If conn.IsConnected Then
                If conn.FileExists(FTPFilePath) Then
                    Using ostream As Stream = conn.OpenRead(FTPFilePath)

                        Try
                            Dim sumbytes As Integer
                            Const buffer As Integer = 8192 * 1024
                            Dim contentRead As Byte() = New Byte(buffer - 1) {}
                            Dim bytesRead As Integer

                            Using fs As FileStream = File.OpenWrite(LocalFilePath)
                                Do

                                    Try
                                        bytesRead = ostream.Read(contentRead, 0, buffer)
                                        sumbytes += bytesRead

                                        If bytesRead > 0 Then
                                            fs.Write(contentRead, 0, bytesRead)
                                            RaiseEvent ReportFTPProgress(sumbytes)
                                        End If
                                    Catch ex As IOException
                                        MessageBox.Show("Connection Lost")
                                        Return False
                                        Exit Do
                                    End Try

                                Loop While (bytesRead > 0)
                                fs.Close()
                                Return True
                            End Using

                        Finally

                            ostream.Close()
                        End Try
                    End Using
                End If

            End If
            Return False

        End Using
    End Function

    ''' <summary>
    ''' Prüft der Größe einer Datei auf dem FTP Server
    ''' </summary>
    ''' <param name="pFTPServer"></param>
    ''' <param name="pUsername"></param>
    ''' <param name="pPassword"></param>
    ''' <param name="FTPFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFileSize(ByVal pFTPServer As String, ByVal pUsername As String, ByVal pPassword As String, ByVal FTPFilePath As String) As Long
        Using conn As New FtpClient()
            'FTP Upload
            conn.Host = pFTPServer
            conn.Credentials = New NetworkCredential(pUsername, pPassword)
            Try
                conn.Connect()
            Catch ex As Sockets.SocketException

            End Try

            If conn.IsConnected Then
                If conn.FileExists(FTPFilePath) Then
                    Return conn.GetFileSize(FTPFilePath)
                End If
            End If
            Return False
        End Using
    End Function
End Class