Imports System.Xml
Imports System.Xml.Linq
Imports EichsoftwareClient

Public Class frmPlausibiltaetspruefung
    Private WithEvents objFTP As New clsFTP

    Private _objEichprozess As Eichprozess

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub New(objEichProzess As Eichprozess)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._objEichprozess = objEichProzess
    End Sub

    Private Sub frmPlausibiltaetspruefung_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _objEichprozess Is Nothing Then Exit Sub

        If _objEichprozess.UploadFilePath <> "" Then
            LabelOption.Visible = True
            RadButtonAnhang.Visible = True
        End If

        Dim objCondition As New Telerik.WinControls.UI.ExpressionFormattingObject("Fehlerhaft", "WertAusConfig <> WertAusSoftware", True)
        objCondition.RowBackColor = Color.FromArgb(254, 120, 110)
        RadGridViewVergleichswerte.Columns(0).ConditionalFormattingObjectList.Add(objCondition)

    End Sub

    Private Sub RadButtonOpen_Click(sender As Object, e As EventArgs) Handles RadButtonOpen.Click
        RadButtonPruefe.Enabled = False

        If IO.File.Exists(My.Settings.LetzterOrdnerPlausiblitaet) Then
            OpenFileDialog1.FileName = My.Settings.LetzterOrdnerPlausiblitaet
        End If
        OpenFileDialog1.ShowDialog()

        RadTextBoxControlPath.Text = OpenFileDialog1.FileName

        If RadTextBoxControlPath.Text <> "" Then
            My.Settings.LetzterOrdnerPlausiblitaet = RadTextBoxControlPath.Text
            RadButtonPruefe.Enabled = True
        End If
    End Sub

    Private Sub RadButtonPruefe_Click(sender As Object, e As EventArgs) Handles RadButtonPruefe.Click
        If Not _objEichprozess Is Nothing Then
            Dim objPlausiblitaet As New clsPlausibilitaetspruefung
            objPlausiblitaet.LadeWerte(RadDropDownListWaegebruecke.SelectedIndex + 1, RadTextBoxControlPath.Text, _objEichprozess)

            FuelleGrid(objPlausiblitaet)

        End If
    End Sub

    Private Sub FuelleGrid(objPlausiblitaet As clsPlausibilitaetspruefung)
        RadGridViewVergleichswerte.DataSource = objPlausiblitaet.getAsDatasource
    End Sub

    Private Sub RadButtonAnhang_Click(sender As Object, e As EventArgs) Handles RadButtonAnhang.Click
        If clsWebserviceFunctions.CanPingStrato Then
            OeffneDateiVonFTP(_objEichprozess.Vorgangsnummer)
        End If
    End Sub

#Region "FTP"

    Private Sub OeffneDateiVonFTP(ByVal vorgangsnummer As String)
        If Not BackgroundWorkerDownloadFromFTP.IsBusy Then
            Me.Enabled = False
            Me.RadProgressBar.Visible = True

            Me.RadProgressBar.Maximum = 100
            BackgroundWorkerDownloadFromFTP.RunWorkerAsync(vorgangsnummer)
        End If
    End Sub

    Private Sub BackgroundWorkerDownloadFromFTP_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerDownloadFromFTP.DoWork
        Dim vorgangsnummer As String = e.Argument
        e.Result = clsWebserviceFunctions.InitDownloadDateiVonFTP(vorgangsnummer, objFTP, Me.BackgroundWorkerDownloadFromFTP)
    End Sub

    Private Sub BackgroundWorkerDownloadFromFTP_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerDownloadFromFTP.RunWorkerCompleted
        Me.Enabled = True
        Me.RadProgressBar.Visible = False
        Me.RadProgressBar.Value1 = 0
        RadProgressBar.Text = ""

        Dim filepath As String = e.Result
        If filepath.Equals("") = False Then
            Dim proc As Process = New Process()
            proc.StartInfo.FileName = filepath
            proc.StartInfo.UseShellExecute = True
            proc.Start()
        Else
            MessageBox.Show("Konnte Datei am Pfad nicht finden. Sie konnte auch nicht heruntergeladen werden.")
        End If
    End Sub

    Private Sub BackgroundWorkerDownloadFromFTP_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerDownloadFromFTP.ProgressChanged
        Try
            If e.ProgressPercentage = 0 Then
                RadProgressBar.Value1 = e.UserState
                RadProgressBar.Text = CInt(CInt(e.UserState) / 1024) & " KB/ " & CInt(CInt(RadProgressBar.Maximum) / 1024) & " KB"
                Me.Refresh()
            Else
                RadProgressBar.Maximum = e.ProgressPercentage
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Sub objFTP_ReportFTPProgress(Progress As Integer) Handles objFTP.ReportFTPProgress
        Try
            BackgroundWorkerDownloadFromFTP.ReportProgress(0, Progress)
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class