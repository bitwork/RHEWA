Imports System.Globalization

Public Class FrmLizenz

    Private Sub FrmLizenz_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        'deaktivieren des splashscreens
        If My.Application.SplashScreen.Visible = True Then
            My.Application.SplashScreen.Visible = False
        End If
    End Sub


    Private Sub FrmLizenz_Load(sender As Object, e As EventArgs) Handles Me.Load
        'deaktivieren des splashscreens
        If My.Application.SplashScreen.Visible = True Then
            My.Application.SplashScreen.Visible = False
        End If
        Me.BringToFront()
    End Sub

    ''' <summary>
    ''' übermitteln der Daten
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonOK_Click(sender As Object, e As EventArgs) Handles RadButtonOK.Click
        Try
            RadButtonOK.Enabled = False
            'verbindung zum Webservice aufbauen
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob die Lizenz gültig ist
                    Dim HEKennung As String = RadTextBoxControl1.Text
                    Dim Lizenzschluessel As String = RadTextBoxControl2.Text

                    If webContext.AktiviereLizenz(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then

                        Dim objLic As New Lizensierung
                        objLic.HEKennung = HEKennung
                        objLic.Lizenzschluessel = Lizenzschluessel

                        If webContext.PruefeObRHEWALizenz(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then
                            objLic.RHEWALizenz = True
                        Else
                            objLic.RHEWALizenz = False
                        End If

                        Try
                            'löschen der lokalen DB
                            For Each lic In DBContext.Lizensierung
                                DBContext.Lizensierung.Remove(lic)
                            Next
                            DBContext.SaveChanges()
                        Catch ex As Exception
                        End Try

                        My.Settings.Lizensiert = True
                        My.Settings.RHEWALizenz = objLic.RHEWALizenz
                        My.Settings.Save()


                        Try
                            'hole zusätliche Lizenzdaten
                            Dim objLizenzdaten As EichsoftwareWebservice.clsLizenzdaten = webContext.GetLizenzdaten(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                            objLic.Name = objLizenzdaten.Name
                            objLic.Vorname = objLizenzdaten.Vorname
                            objLic.Firma = objLizenzdaten.Firma
                            objLic.FirmaOrt = objLizenzdaten.FirmaOrt
                            objLic.FirmaPLZ = objLizenzdaten.FirmaPLZ
                            objLic.FirmaStrasse = objLizenzdaten.FirmaStrasse
                            objLic.fk_benutzerID = objLizenzdaten.BenutzerID
                            DBContext.SaveChanges()
                        Catch ex As Exception

                        End Try

                        Try
                            'speichern in lokaler DB
                            DBContext.Lizensierung.Add(objLic)
                            DBContext.SaveChanges()
                        Catch ex As Exception
                        End Try


                        'abschluss des dialoges
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()


                    Else
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_UngueltigeLizenz, My.Resources.GlobaleLokalisierung.Fehler)
                        My.Settings.Lizensiert = False
                        My.Settings.Save()
                    End If

                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler)
        Finally
            RadButtonOK.Enabled = True
        End Try

    End Sub

    ''' <summary>
    ''' aktivieren / deaktivieren des OK Buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxAkzeptieren_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxAkzeptieren.ToggleStateChanged
        RadButtonOK.Enabled = RadCheckBoxAkzeptieren.Checked
    End Sub


#Region "Lokalisierung"
    Private Sub RadButtonChangeLanguageToGerman_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonChangeLanguageToPolish.Click, RadButtonChangeLanguageToGerman.Click, RadButtonChangeLanguageToEnglish.Click
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
        My.Settings.AktuelleSprache = Code
        My.Settings.Save()


        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLizenz))
        'übersetzung der Formular elemente von frmMainContainer

        Me.RadTextBoxControl1.Text = resources.GetString("RadTextBoxControl1.Text")
        Me.RadTextBoxControl2.Text = resources.GetString("RadTextBoxControl2.Text")
        Me.RadTextBoxControl3.Text = resources.GetString("RadTextBoxControl3.Text")

        Me.lblLizenzabkommen.Text = resources.GetString("lblLizenzabkommen.Text")
        Me.lblLizenzschuessel.Text = resources.GetString("lblLizenzschuessel.Text")
        Me.lblName.Text = resources.GetString("lblName.Text")

        Me.RadCheckBoxAkzeptieren.Text = resources.GetString("RadCheckBoxAkzeptieren.Text")

    End Sub
#End Region

End Class
