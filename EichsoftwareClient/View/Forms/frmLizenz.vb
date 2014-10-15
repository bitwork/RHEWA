Imports System.Globalization

Public Class FrmLizenz




    Private Sub FrmLizenz_Load(sender As Object, e As EventArgs) Handles Me.Load
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

                    'verbindung zweimal testen, falls server beim ersten mal nicht erreichbar war
                    If Not clsWebserviceFunctions.TesteVerbindung() Then
                        clsWebserviceFunctions.TesteVerbindung()
                    End If


                    If webContext.AktiviereLizenz(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then

                        'prüfen ob bereits lokales Objekt existiert
                        Dim Lics = (From Lizensen In DBContext.Lizensierung Where Lizensen.Lizenzschluessel.ToLower = Lizenzschluessel.ToLower And Lizensen.HEKennung.ToLower = HEKennung.ToLower).FirstOrDefault

                        If Lics Is Nothing Then


                            Dim objLic As New Lizensierung
                            objLic.HEKennung = HEKennung
                            objLic.Lizenzschluessel = Lizenzschluessel

                            If webContext.PruefeObRHEWALizenz(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name) Then
                                objLic.RHEWALizenz = True
                            Else
                                objLic.RHEWALizenz = False
                            End If


                            Try
                                'hole zusätliche Lizenzdaten
                                Dim objLizenzdaten As EichsoftwareWebservice.clsLizenzdaten = webContext.GetLizenzdaten(HEKennung, Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                                objLic.Name = objLizenzdaten.Name
                                objLic.Vorname = objLizenzdaten.Vorname
                                objLic.Firma = objLizenzdaten.Firma
                                objLic.FirmaOrt = objLizenzdaten.FirmaOrt
                                objLic.FirmaPLZ = objLizenzdaten.FirmaPLZ
                                objLic.FirmaStrasse = objLizenzdaten.FirmaStrasse
                                objLic.FK_BenutzerID = objLizenzdaten.BenutzerID
                                objLic.Aktiv = objLizenzdaten.Aktiv
                                DBContext.SaveChanges()
                            Catch ex As Exception
                            End Try

                            Try
                                'speichern in lokaler DB
                                DBContext.Lizensierung.Add(objLic)
                                DBContext.SaveChanges()

                                'anlegen einer lokalen Konfiguration
                                Dim objKonfig As New Konfiguration
                                objKonfig.AktuelleSprache = Threading.Thread.CurrentThread.CurrentUICulture.ToString
                                objKonfig.BenutzerLizenz = objLic.Lizenzschluessel
                                objKonfig.GridSettings = ""
                                objKonfig.GridSettingsRHEWA = ""
                                objKonfig.HoleAlleeigenenEichungenVomServer = True
                                objKonfig.LetztesUpdate = #1/1/2000#
                                objKonfig.SyncAb = "01.01.2000"
                                objKonfig.SyncBis = "31.12.2999"
                                objKonfig.Synchronisierungsmodus = "Alles"

                                'speichern in lokaler DB
                                DBContext.Konfiguration.Add(objKonfig)
                                DBContext.SaveChanges()
                            Catch ex As Exception
                            End Try


                            'abschluss des dialoges
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                        Else 'lizenz existiert bereits
                            'abschluss des dialoges
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                        End If
                    Else
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_UngueltigeLizenz, My.Resources.GlobaleLokalisierung.Fehler)
                        My.Settings.LetzterBenutzer = ""
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

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLizenz))
        'übersetzung der Formular elemente von frmMainContainer
        Me.lblLizenzabkommen.Text = resources.GetString("lblLizenzabkommen.Text")
        Me.lblLizenzschuessel.Text = resources.GetString("lblLizenzschuessel.Text")
        Me.lblName.Text = resources.GetString("lblName.Text")

        Me.RadCheckBoxAkzeptieren.Text = resources.GetString("RadCheckBoxAkzeptieren.Text")

    End Sub
#End Region

End Class
