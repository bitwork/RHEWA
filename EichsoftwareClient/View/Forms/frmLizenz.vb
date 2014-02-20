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
                    If webContext.AktiviereLizenz(RadTextBoxControl1.Text, RadTextBoxControl2.Text, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then

                        Dim objLic As New Lizensierung
                        objLic.FK_Benutzer = RadTextBoxControl1.Text
                        objLic.Lizenzschluessel = RadTextBoxControl2.Text

                        If webContext.PruefeObRHEWALizenz(RadTextBoxControl1.Text, RadTextBoxControl2.Text, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then
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
                            Dim objLizenzdaten As EichsoftwareWebservice.clsLizenzdaten = webContext.GetLizenzdaten(RadTextBoxControl1.Text, RadTextBoxControl2.Text, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                            objLic.Name = objLizenzdaten.Name
                            objLic.Vorname = objLizenzdaten.Vorname
                            objLic.Firma = objLizenzdaten.Firma
                            objLic.FirmaOrt = objLizenzdaten.FirmaOrt
                            objLic.FirmaPLZ = objLizenzdaten.FirmaPLZ
                            objLic.FirmaStrasse = objLizenzdaten.FirmaStrasse
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
End Class
