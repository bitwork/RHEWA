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
        Try
            'Grid formatieren
            Dim objCondition As New Telerik.WinControls.UI.ExpressionFormattingObject("Fehlerhaft", "WertAusConfig <> WertAusSoftware", True)
            objCondition.RowBackColor = Color.FromArgb(254, 120, 110)
            RadGridViewVergleichswerte.Columns(0).ConditionalFormattingObjectList.Add(objCondition)
        Catch ex As Exception
        End Try
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
        Pruefe()
    End Sub

    Private Sub Pruefe()
        If Not _objEichprozess Is Nothing Then
            Dim objPlausiblitaet As New clsPlausibilitaetspruefung
            objPlausiblitaet.LadeWerte(RadDropDownListWaegebruecke.SelectedIndex + 1, RadTextBoxControlPath.Text, _objEichprozess)

            FuelleGrid(objPlausiblitaet)
            FillControls(objPlausiblitaet)
            BerechneUebersetzungsverhaeltnis(objPlausiblitaet)
        End If
    End Sub

    Private Sub FillControls(objPlausiblitaet As clsPlausibilitaetspruefung)
        Me.RadTextBoxControlAnalogwert1.Text = objPlausiblitaet.AnalogwertJustagepunktMinConfig
        Me.RadTextBoxControlAnalogwert2.Text = objPlausiblitaet.AnalogwertJustagepunktMaxConfig
        Me.RadTextBoxControlJustagePunkt1.Text = objPlausiblitaet.GewichtswertJustagepunktMinConfig
        Me.RadTextBoxControlJustagePunkt2.Text = objPlausiblitaet.GewichtswertJustagepunktMaxConfig
        Me.RadTextBoxControlWZKennwert.Text = _objEichprozess.Lookup_Waegezelle.Waegezellenkennwert
        Me.RadTextBoxControlWZNennlast.Text = _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast
        Me.RadTextBoxControlAnzahlWZ.Text = _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        Me.RadTextBoxControlUebersetzungsverhaeltnis.Text = _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis

        If _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = 1 Then
            RadTextBoxControlLastWaegebrueckeBerechnung1.Visible = False
            RadTextBoxControlLastWaegebrueckeBerechnung2.Visible = False
            RadTextBoxControlLastWaegebrueckeBerechnung3.Visible = False
            RadTextBoxControlLastWaegebrueckeBerechnung4.Visible = False
            LabelLastWaegebruecke.Visible = False
            LabelArrow3.Visible = False

        Else
            RadTextBoxControlLastWaegebrueckeBerechnung1.Visible = True
            RadTextBoxControlLastWaegebrueckeBerechnung2.Visible = True
            RadTextBoxControlLastWaegebrueckeBerechnung3.Visible = True
            RadTextBoxControlLastWaegebrueckeBerechnung4.Visible = True
            LabelLastWaegebruecke.Visible = True
            LabelArrow3.Visible = True
        End If
    End Sub

    Private Sub BerechneUebersetzungsverhaeltnis(objPlausiblitaet As clsPlausibilitaetspruefung)
        Try
            RadTextBoxControlADWertBerechnung1.Text = objPlausiblitaet.AnalogwertJustagepunktMinConfig
            RadTextBoxControlADWertBerechnung2.Text = objPlausiblitaet.AnalogwertJustagepunktMaxConfig
            RadTextBoxControlADWertBerechnung3.Text = CDec(objPlausiblitaet.AnalogwertJustagepunktMaxConfig) - CDec(objPlausiblitaet.AnalogwertJustagepunktMinConfig)
            RadTextBoxControlADWertBerechnung4.Text = CDec(_objEichprozess.Lookup_Waegezelle.Waegezellenkennwert) / (2 + 1 / 12)

            RadTextBoxControlProzentualeAuslasungWZBerechnung1.Text = (CDec(RadTextBoxControlADWertBerechnung1.Text) / CDec(RadTextBoxControlADWertBerechnung4.Text)) * 100
            RadTextBoxControlProzentualeAuslasungWZBerechnung2.Text = (CDec(RadTextBoxControlADWertBerechnung2.Text) / CDec(RadTextBoxControlADWertBerechnung4.Text)) * 100
            RadTextBoxControlProzentualeAuslasungWZBerechnung3.Text = (CDec(RadTextBoxControlADWertBerechnung3.Text) / CDec(RadTextBoxControlADWertBerechnung4.Text)) * 100
            RadTextBoxControlProzentualeAuslasungWZBerechnung4.Text = (CDec(RadTextBoxControlADWertBerechnung4.Text) / CDec(RadTextBoxControlADWertBerechnung4.Text)) * 100

            RadTextBoxControlLastWZBerechnung1.Text = (Math.Round(CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast) * CDec(RadTextBoxControlProzentualeAuslasungWZBerechnung1.Value), 2, MidpointRounding.AwayFromZero)) * _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
            RadTextBoxControlLastWZBerechnung2.Text = (Math.Round(CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast) * CDec(RadTextBoxControlProzentualeAuslasungWZBerechnung2.Value), 2, MidpointRounding.AwayFromZero)) * _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
            RadTextBoxControlLastWZBerechnung3.Text = (Math.Round(CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast) * CDec(RadTextBoxControlProzentualeAuslasungWZBerechnung3.Value), 2, MidpointRounding.AwayFromZero)) * _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

            If _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = 1 Then
                RadTextBoxControlLastWZBerechnung4.Text = Math.Round(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast * _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen, 2, MidpointRounding.AwayFromZero)
                RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text = (Math.Round(CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast) * CDec(RadTextBoxControlProzentualeAuslasungWZBerechnung3.Value) / (CDec(objPlausiblitaet.GewichtswertJustagepunktMaxConfig) - CDec(objPlausiblitaet.GewichtswertJustagepunktMinConfig)), 4, MidpointRounding.AwayFromZero)) * _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
            Else
                RadTextBoxControlLastWZBerechnung4.Text = Math.Round(CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast), 2, MidpointRounding.AwayFromZero)
                RadTextBoxControlLastWaegebrueckeBerechnung3.Text = Math.Round(CDec(objPlausiblitaet.GewichtswertJustagepunktMaxConfig) - CDec(objPlausiblitaet.GewichtswertJustagepunktMinConfig), 2, MidpointRounding.AwayFromZero)
                RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text = Math.Round((CDec(_objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast) * CDec(RadTextBoxControlProzentualeAuslasungWZBerechnung3.Value)) / (CDec(objPlausiblitaet.GewichtswertJustagepunktMaxConfig) - CDec(objPlausiblitaet.GewichtswertJustagepunktMinConfig)), 4, MidpointRounding.AwayFromZero)
                RadTextBoxControlLastWaegebrueckeBerechnung1.Text = Math.Round(CDec(RadTextBoxControlLastWZBerechnung1.Text) / CDec(RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text), 2, MidpointRounding.AwayFromZero)
                RadTextBoxControlLastWaegebrueckeBerechnung2.Text = Math.Round(CDec(RadTextBoxControlLastWZBerechnung2.Text) / CDec(RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text), 2, MidpointRounding.AwayFromZero)
                RadTextBoxControlLastWaegebrueckeBerechnung4.Text = Math.Round(CDec(RadTextBoxControlLastWZBerechnung4.Text) / CDec(RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text), 2, MidpointRounding.AwayFromZero)
            End If
        Catch ex As Exception
            RadTextBoxControlADWertBerechnung1.Text = "ungültiger Wert"
            RadTextBoxControlADWertBerechnung2.Text = "ungültiger Wert"
            RadTextBoxControlADWertBerechnung3.Text = "ungültiger Wert"
            RadTextBoxControlADWertBerechnung4.Text = "ungültiger Wert"
            RadTextBoxControlProzentualeAuslasungWZBerechnung1.Text = "0"
            RadTextBoxControlProzentualeAuslasungWZBerechnung2.Text = "0"
            RadTextBoxControlProzentualeAuslasungWZBerechnung3.Text = "0"
            RadTextBoxControlProzentualeAuslasungWZBerechnung4.Text = "0"
            RadTextBoxControlLastWZBerechnung1.Text = "ungültiger Wert"
            RadTextBoxControlLastWZBerechnung2.Text = "ungültiger Wert"
            RadTextBoxControlLastWZBerechnung3.Text = "ungültiger Wert"
            RadTextBoxControlLastWZBerechnung4.Text = "ungültiger Wert"
            RadTextBoxControlErrechnetesUebersetzungsverhaeltnis.Text = "ungültiger Wert"
            RadTextBoxControlLastWaegebrueckeBerechnung1.Text = "ungültiger Wert"
            RadTextBoxControlLastWaegebrueckeBerechnung2.Text = "ungültiger Wert"
            RadTextBoxControlLastWaegebrueckeBerechnung3.Text = "ungültiger Wert"
            RadTextBoxControlLastWaegebrueckeBerechnung4.Text = "ungültiger Wert"
        End Try

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