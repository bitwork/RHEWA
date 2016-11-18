Imports Telerik.WinControls.UI
Imports System.Deployment.Application

Public Class FrmBenutzerauswahl

    Private mvarGewaehlteBenutzerLizenz As Lizensierung 'aktuell gewählter Benutzer

    ''' <summary>
    ''' Gets the gewaehlte benutzer lizenz.
    ''' </summary>
    ''' <value>The gewaehlte benutzer lizenz.</value>
    Public ReadOnly Property GewaehlteBenutzerLizenz As Lizensierung
        Get
            Return mvarGewaehlteBenutzerLizenz
        End Get
    End Property

    Private Sub RadButtonOK_Click(sender As Object, e As EventArgs) Handles RadButtonOK.Click
        BestaetigeDialog()
    End Sub

    Private Sub FrmBenutzerauswahl_Load(sender As Object, e As EventArgs) Handles Me.Load
        'liste füllen
        LadeBenutzer()

        'wenn noch kein Element in der auflistung vorhanden ist, direkt Lizenzeingabe Dialog öffnen
        If RadListControlBenutzer.Items.Count = 0 Then
            ShowNeueLizenz()
        End If

        Dim myVersion As Version

        Try
            If ApplicationDeployment.IsNetworkDeployed Then
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion
                Me.Text += " " + myVersion.ToString(3)
            End If
        Catch ex As Exception
        End Try

    End Sub

    ''' <summary>
    ''' lädt lokale Lizenzen ins Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LadeBenutzer()
        Using context As New EichsoftwareClientdatabaseEntities1
            Dim data = From Benutzer In context.Lizensierung Select New With
                                                 {
                                                     .Name = Benutzer.Vorname & " " & Benutzer.Name,
                                                     .Lizenzschluessel = Benutzer.Lizenzschluessel,
                                                     .Aktiv = Benutzer.Aktiv
                                              }

            RadListControlBenutzer.DataSource = data.ToArray
            RadListControlBenutzer.DisplayMember = "Name"
            RadListControlBenutzer.ValueMember = "Lizenzschluessel"
            RadListControlBenutzer.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending
        End Using

        Try
            'markieren des zuletzte gewählten Benutzers, falls vorhanden
            RadListControlBenutzer.SelectedValue = My.Settings.LetzterBenutzer
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadButtonNeuerBenutzer_Click(sender As Object, e As EventArgs) Handles RadButtonNeuerBenutzer.Click
        ShowNeueLizenz()
    End Sub

    ''' <summary>
    ''' Validierung
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BestaetigeDialog()
        'abbruch wenn leer
        If RadListControlBenutzer.Items.Count = 0 Then
            ShowNeueLizenz()
            Exit Sub
        End If

        If RadListControlBenutzer.SelectedValue Is Nothing Then
            Exit Sub
        End If

        If RadListControlBenutzer.SelectedItem.VisualItem.Data("Aktiv") = False Then
            'online prüfen ob die Lizenz reaktiviert wurde
            AktuellerBenutzer.GetNewInstance(RadListControlBenutzer.SelectedValue)
            If clsWebserviceFunctions.GetNeueStammdaten(True) = False Then
                Exit Sub
            End If
        End If
        Try

            AktuellerBenutzer.GetNewInstance(RadListControlBenutzer.SelectedValue)

            Dim error1 As Boolean = False
            'versuchen sonderfälle abzudecken
            If AktuellerBenutzer.Instance Is Nothing Then
                MessageBox.Show("There is an error with your User Account. Please retry entering your license 1")
                error1 = True
            End If
            If AktuellerBenutzer.Instance.Lizenz Is Nothing Then
                MessageBox.Show("There is an error with your User Account. Please retry entering your license 2")
                error1 = True

            End If

            mvarGewaehlteBenutzerLizenz = AktuellerBenutzer.Instance.Lizenz
            If mvarGewaehlteBenutzerLizenz Is Nothing Then
                MessageBox.Show("There is an error with your User Account. Please retry entering your license 3")
                error1 = True

            End If
            If mvarGewaehlteBenutzerLizenz.Lizenzschluessel Is Nothing Then
                MessageBox.Show("There is an error with your User Account. Please retry entering your license 4")
                error1 = True

            End If

            If error1 = True Then
                clsDBFunctions.LoescheLokaleDatenbank()
                clsDBFunctions.LoescheLokaleBenutzer()
                Application.Restart()
                Exit Sub

            End If

            My.Settings.LetzterBenutzer = mvarGewaehlteBenutzerLizenz.Lizenzschluessel
            My.Settings.Save()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(String.Format("{0} Stacktrace:{1}", ex.Message, ex.StackTrace))
        End Try
    End Sub

    ''' <summary>
    ''' dialog zur Eingabe einer Lizenz öffnen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowNeueLizenz()
        'dialog zum anlegen einer Lizenz öffnen
        Dim f As New FrmLizenz
        If f.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            'neu laden
            LadeBenutzer()
        End If
    End Sub

    ''' <summary>
    '''    Enter Taste abfangen für schnelleres vorblättern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Enter für schnelleres vorblättern
        If e.KeyCode = Keys.Return Then
            BestaetigeDialog()
        End If
    End Sub

    ''' <summary>
    '''    Doppelklick abfangen für schnelleres vorblättern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadListControlBenutzer_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles RadListControlBenutzer.MouseDoubleClick
        BestaetigeDialog()
    End Sub

    ''' <summary>
    ''' Markieren einer vom Server deaktivierten Lizenz. Diese wird dann in roten Text geschrieben
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadListControlBenutzer_VisualItemFormatting(sender As Object, e As Telerik.WinControls.UI.VisualItemFormattingEventArgs) Handles RadListControlBenutzer.VisualItemFormatting

        If e.VisualItem.Data("Aktiv") = False Then
            e.VisualItem.NumberOfColors = 1
            e.VisualItem.ForeColor = Color.Red
            e.VisualItem.BorderColor = Color.Blue
            e.VisualItem.Font = Font
        Else
            e.VisualItem.ResetValue(LightVisualElement.NumberOfColorsProperty, Telerik.WinControls.ValueResetFlags.Local)
            e.VisualItem.ResetValue(LightVisualElement.BackColorProperty, Telerik.WinControls.ValueResetFlags.Local)
            e.VisualItem.ResetValue(LightVisualElement.ForeColorProperty, Telerik.WinControls.ValueResetFlags.Local)
            e.VisualItem.ResetValue(LightVisualElement.BorderColorProperty, Telerik.WinControls.ValueResetFlags.Local)
            e.VisualItem.ResetValue(LightVisualElement.FontProperty, Telerik.WinControls.ValueResetFlags.Local)
        End If
    End Sub
End Class