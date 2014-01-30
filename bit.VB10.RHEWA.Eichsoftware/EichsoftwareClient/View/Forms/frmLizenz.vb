﻿Public Class FrmLizenz


    Private Sub FrmLizenz_Load(sender As Object, e As EventArgs) Handles Me.Load
      
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            'verbindung zum Webservice aufbauen
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob die Lizenz gültig ist
                    If webContext.AktiviereLizenz(RadTextBoxControl1.Text, RadTextBoxControl2.Text) Then

                        Dim objLic As New Lizensierung
                        objLic.FK_SuperofficeBenutzer = RadTextBoxControl1.Text
                        objLic.Lizenzschluessel = RadTextBoxControl2.Text

                        If webContext.PruefeObRHEWALizenz(RadTextBoxControl1.Text, RadTextBoxControl2.Text) Then
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
                        Try
                            'speichern in lokaler DB
                            DBContext.Lizensierung.Add(objLic)
                            DBContext.SaveChanges()
                        Catch ex As Exception
                        End Try
                        My.Settings.Lizensiert = True
                        My.Settings.RHEWALizenz = objLic.RHEWALizenz
                        My.Settings.Save()



                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()


                    Else
                        MessageBox.Show("Ungültige Lizenz") 'My.Resources.GlobaleLokalisierung.UngueltigeLizenz, "")
                        My.Settings.Lizensiert = False
                        My.Settings.Save()
                    End If

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class
