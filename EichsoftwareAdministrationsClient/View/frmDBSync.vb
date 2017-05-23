Imports System.Data.SqlClient

Public Class frmDBSync
    Private Const DBTABLES As String = "ServerVerbindungsprotokoll,ServerLookupVertragspartnerFirma,ServerKonfiguration,Servereichmarkenverwaltung,Firmen,ServerFirmenZusatzdaten,Benutzer,ServerLizensierung,ServerLookup_Waagenart,ServerKompatiblitaetsnachweis,ServerLookup_Vorgangsstatus,ServerLookup_Auswertegeraet,ServerLookup_Waegezelle,ServerLookup_Bearbeitungsstatus,ServerLookup_Waagentyp,ServerLookup_Konformitaetsbewertungsverfahren,ServerEichprotokoll,ServerEichprozess,ServerMogelstatistik,ServerPruefungAnsprechvermoegen,ServerPruefungAussermittigeBelastung,ServerPruefungLinearitaetFallend,ServerPruefungLinearitaetSteigend,ServerPruefungRollendeLasten,ServerPruefungStabilitaetGleichgewichtslage,ServerPruefungStaffelverfahrenErsatzlast,ServerPruefungStaffelverfahrenNormallast,ServerPruefungWiederholbarkeit"

    Private Sub Start()
        'einblenden des DEV Syncs
        If Debugger.IsAttached Then RadioButtonSyncStratoDEV.Visible = True Else RadioButtonSyncStratoDEV.Visible = False
        If Not BackgroundWorker1.IsBusy Then
            'formatierung
            RadWaitingBar1.Visible = True
            RadWaitingBar1.StartWaiting()

            Me.RadioButtonSyncStratoRHEWA.Enabled = False
            Me.RadioButtonSyncRHEWAStrato.Enabled = False
            Me.ButtonSync.Enabled = False

            'textboxen leeren
            RadListControlSQLQuery.Text = ""
            RadListControlLog.Text = ""

            'Starten des Threads
            TimerLog.Enabled = True
            'BackgroundWorker1.RunWorkerAsync(conn)
            BackgroundWorker1.RunWorkerAsync()

        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'rückgabewert

        'starten des Shellscriptes
        Dim PathScript As String = AppDomain.CurrentDomain.BaseDirectory & "Repository\Call_DTS.bat"
        Dim PathWorkingDirectory As String = AppDomain.CurrentDomain.BaseDirectory & "Repository"
        Dim PathSQL As String = AppDomain.CurrentDomain.BaseDirectory & "Repository\FixSQLFile.sql"
        Dim PathLog As String = AppDomain.CurrentDomain.BaseDirectory & "Repository\OfflineSync.log"

        'Connection String initialisieren
        Dim conn As New SqlClient.SqlConnection
        If RadioButtonSyncStratoRHEWA.IsChecked Then
            'TODO DB PFad von RHEWA Eintragen
            conn.ConnectionString = "Data Source=RHEWASBS2;Initial Catalog=Herstellerersteichung;Persist Security Info=True;User ID=Herstellerersteichung;Password=Eichen2013"
            PathScript = AppDomain.CurrentDomain.BaseDirectory & "Repository\Call_DTS STRATO RHEWA.bat"

        ElseIf RadioButtonSyncRHEWAStrato.IsChecked Then
            conn.ConnectionString = "Data Source=h2223265.stratoserver.net;Initial Catalog=Herstellerersteichung;Persist Security Info=True;User ID=Eichen;Password=Eichen2013"
            PathScript = AppDomain.CurrentDomain.BaseDirectory & "Repository\Call_DTS RHEWA STRATO.bat"
        ElseIf RadioButtonSyncStratoDEV.IsChecked Then
            conn.ConnectionString = "Data Source=WIN7MOBDEV01;Initial Catalog=Herstellerersteichung;Persist Security Info=True;User ID=sa;Password=Test1234"
            PathScript = AppDomain.CurrentDomain.BaseDirectory & "Repository\Call_DTS STRATO DEV.bat"
        End If

        Dim returnvalue As Tuple(Of String, String) = Nothing
        Dim fileContentsSQL As String = ""
        Dim fileContentsLOG As String = ""

        Dim psi As New ProcessStartInfo(PathScript)
        If Debugger.IsAttached Then
            psi.CreateNoWindow = False
        Else
            psi.CreateNoWindow = True
        End If
        psi.UseShellExecute = False
        psi.RedirectStandardError = False
        psi.RedirectStandardOutput = False
        psi.WindowStyle = ProcessWindowStyle.Normal

        psi.WorkingDirectory = PathWorkingDirectory

        Dim process As Process = Process.Start(psi)

        'Code erst weiterführen wenn Shellscript abgeschlossen wurde
        process.WaitForExit()

        'Referenz auf Ergebnisdatei holen
        Dim SQLFile As New IO.FileInfo(PathSQL)
        If SQLFile.Exists Then

            'lesen der Datei in String
            fileContentsSQL = My.Computer.FileSystem.ReadAllText(SQLFile.FullName)

            Try
                'löschen der Datei
                SQLFile.Delete()
            Catch ex As Exception
            End Try

            'wenn in der datei etwas steht (somit liegen Änderungen vor)
            If Not fileContentsSQL.Equals("") Then
                'formatieren des Textes
                fileContentsSQL = fileContentsSQL.Replace("N'NULL'", "NULL") 'wirft sonst fehler
                fileContentsSQL = "set dateformat ymd;" & vbNewLine & fileContentsSQL 'damit das Datum korrekt behandelt wird

                'Deaktivieren der Foreign Key Constrains der DB für Massen import
                fileContentsSQL = GETActivateConstraintsSkript() & " " & fileContentsSQL
                fileContentsSQL = fileContentsSQL & " " & GETDeactivateConstraintsSkript()

                'Erzeugen eines SQL Command objektes
                Dim cmd As New SqlClient.SqlCommand
                cmd.Connection = conn
                cmd.CommandText = fileContentsSQL
                cmd.CommandType = CommandType.Text

                'öffnen der DB Verbindung
                Dim previousConnectionState As ConnectionState
                previousConnectionState = conn.State
                Try
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    'Ausführen der Abfrage
                    cmd.ExecuteNonQuery()
                Catch ex As SqlException
                    MessageBox.Show(ex.Message)
                Finally
                    'schliessen der Verbindung
                    If previousConnectionState = ConnectionState.Closed Then
                        conn.Close()
                    End If
                End Try

            End If

            'auslesen der LogDatei
            Dim LogFile As New IO.FileInfo(PathLog)
            Try
                If LogFile.Exists Then
                    'lesen der Datei in String
                    fileContentsLOG = My.Computer.FileSystem.ReadAllText(LogFile.FullName)
                    LogFile.Delete()
                End If
            Catch ex As Exception
            End Try

            'schreiben des rückgabe wertes
            returnvalue = New Tuple(Of String, String)(fileContentsSQL, fileContentsLOG)

            'zurückgeben der Textinhalte
            e.Result = returnvalue
        Else
            'auslesen der LogDatei
            Dim LogFile As New IO.FileInfo(PathLog)
            fileContentsLOG = My.Computer.FileSystem.ReadAllText(LogFile.FullName)
            returnvalue = New Tuple(Of String, String)("Datei wurde nicht erzeugt. Fehler", fileContentsLOG)
            e.Result = returnvalue
        End If

    End Sub

    Private Function GETActivateConstraintsSkript() As string
        Dim Tables As String() = DBTABLES.Split(",")
        Dim ConstraintSkript As String = ""
        For Each table In Tables
            ConstraintSkript += String.Format("ALTER TABLE {0} NOCHECK CONSTRAINT ALL " & vbNewLine, table)
        Next
        Return ConstraintSkript
    End Function

    Private Function GETDeactivateConstraintsSkript() As string
        Dim Tables As String() = DBTABLES.Split(",")
        Dim ConstraintSkript As String = ""
        For Each table In Tables
            ConstraintSkript += String.Format("ALTER TABLE {0} CHECK CONSTRAINT ALL " & vbNewLine, table)
        Next
        Return ConstraintSkript
    End Function

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        RadWaitingBar1.Visible = False
        RadWaitingBar1.StopWaiting()

        Me.RadioButtonSyncStratoRHEWA.Enabled = True
        Me.RadioButtonSyncRHEWAStrato.Enabled = True
        Me.ButtonSync.Enabled = True
        TimerLog.Enabled = False
        If Not e.Result Is Nothing Then
            'ausgeben der Logs in Textboxen
            RadListControlSQLQuery.Text = CType(e.Result, Tuple(Of String, String)).Item1

            If RadListControlSQLQuery.Text.Equals("") Then
                RadListControlSQLQuery.Text = "Es wurde kein SQL Code generiert. Überprüfen Sie das Log"
            End If

            RadListControlLog.Text = CType(e.Result, Tuple(Of String, String)).Item2
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If BackgroundWorker1.IsBusy Then
            MessageBox.Show("Der Hintergrund Thread läuft noch und darf nicht abgebrochen werden. Bitte warten Sie.")
            e.Cancel = True
        End If
    End Sub

    Private Sub TimerLog_Tick(sender As Object, e As EventArgs) Handles TimerLog.Tick
        Try
            'auslesen der LogDatei
            Dim PathLog As String = AppDomain.CurrentDomain.BaseDirectory & "Repository\OfflineSync.log"

            Dim LogFile As New IO.FileInfo(PathLog)
            Try
                If LogFile.Exists Then

                    'leeren der text box
                    RadListControlLog.Text = ""
                    'lesen der Datei in String
                    Using fileStream = New IO.FileStream(PathLog, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                        Using textReader = New IO.StreamReader(fileStream)
                            Dim content As String = ""

                            'formatieren des Textes
                            Dim splitter(0) As String
                            splitter(0) = "seconds." 'dieser Text wird am Ende jeder Operation angezeigt. Ab hier leerzeilen einfügen
                            Dim tmp() As String = textReader.ReadToEnd().Split(splitter, StringSplitOptions.RemoveEmptyEntries)

                            For Each Str As String In tmp
                                If Not Str.Trim.Equals("") Then
                                    content = Str.Trim & " seconds." & vbNewLine & vbNewLine + content
                                End If
                            Next
                            RadListControlLog.Text = content
                        End Using
                    End Using

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles ButtonSync.Click
        Start()
    End Sub
End Class