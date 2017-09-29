Imports System.Globalization

Public Class FrmLizenz
    ''' <summary>
    ''' Fokusierung des Lizenzdialoges, damit dieser nicht ausversehen im hintergrund verschwindet
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

            If checkIfTestLizenz() Then
                createTestLizenz()
                Exit Sub
            End If
            If clsWebserviceFunctions.InsertLizenz(RadTextBoxControl1.Text, RadTextBoxControl2.Text) Then
                'abschluss des dialoges
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler)
        Finally
            RadButtonOK.Enabled = True
        End Try

    End Sub



    Private Sub createTestLizenz()
        Using DBContext As New Entities

            'prüfen ob die Lizenz gültig ist
            Dim HEKennung As String = RadTextBoxControl1.Text
            Dim Lizenzschluessel As String = RadTextBoxControl2.Text


            'prüfen ob bereits lokales Objekt existiert
            Dim Lics = (From Lizensen In DBContext.Lizensierung Where Lizensen.Lizenzschluessel.ToLower = Lizenzschluessel.ToLower And Lizensen.HEKennung.ToLower = HEKennung.ToLower).FirstOrDefault
                If Lics Is Nothing Then

                    Dim objLic As New Lizensierung
                    objLic.HEKennung = HEKennung
                    objLic.Lizenzschluessel = Lizenzschluessel

                objLic.RHEWALizenz = False

                Try
                    'hole zusätliche Lizenzdaten

                    objLic.Name = "Test"
                    objLic.Vorname = "Test"
                    objLic.Firma = "Test Firma"
                    objLic.FirmaOrt = "Test Ort"
                    objLic.FirmaPLZ = "40000"
                    objLic.FirmaStrasse = "Teststr. 1"
                    objLic.FK_BenutzerID = -1
                    objLic.Aktiv = True
                    DBContext.SaveChanges()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
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
                    objKonfig.HoleAlleeigenenEichungenVomServer = False
                    objKonfig.LetztesUpdate = #1/1/2000#
                        objKonfig.SyncAb = "01.01.2000"
                        objKonfig.SyncBis = "31.12.2999"
                        objKonfig.Synchronisierungsmodus = "Alles"

                        'speichern in lokaler DB
                        DBContext.Konfiguration.Add(objKonfig)
                        DBContext.SaveChanges()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)

                    End Try

                    'abschluss des dialoges
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                Else 'lizenz existiert bereits
                    'abschluss des dialoges
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If

        End Using

    End Sub

    Private Function checkIfTestLizenz() As Boolean
        If RadTextBoxControl1.Text.ToLower = "test" And RadTextBoxControl2.Text.ToLower = "test" Then
            Return True
        Else
            Return False
        End If
    End Function

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

    Private Sub changeCulture(ByVal Code As string)
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