Public Class FrmEichmarkenverwaltung
    Private _ID As String = "-1"
    Private _objEichmarkenverwaltung As ServerEichmarkenverwaltung

    Sub New(ByVal pID As String)

        ' This call is required by the designer.
        InitializeComponent()
        _ID = pID
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private Sub Auswertegeraet_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using context As New EichenEntities()
            'abrufen der Entität aus der Datenbank
            If _ID <> "-1" Then
                _objEichmarkenverwaltung = (From AWG In context.ServerEichmarkenverwaltung Where AWG.ID = _ID).FirstOrDefault
            Else 'erzeugen einer neuen Entität
                _objEichmarkenverwaltung = context.ServerEichmarkenverwaltung.Create
                _objEichmarkenverwaltung.ID = "0"

                _objEichmarkenverwaltung.BenannteStelleAnzahl = 0
                _objEichmarkenverwaltung.BenannteStelleAnzahlMeldestand = 3

                _objEichmarkenverwaltung.CEAnzahl = 0
                _objEichmarkenverwaltung.CEAnzahlMeldestand = 3

                _objEichmarkenverwaltung.Eichsiegel13x13Anzahl = 0
                _objEichmarkenverwaltung.Eichsiegel13x13AnzahlMeldestand = 3

                _objEichmarkenverwaltung.EichsiegelRundAnzahl = 0
                _objEichmarkenverwaltung.EichsiegelRundAnzahlMeldestand = 3

                _objEichmarkenverwaltung.GruenesMAnzahl = 0
                _objEichmarkenverwaltung.GruenesMAnzahlMeldestand = 3

                _objEichmarkenverwaltung.HinweismarkeGelochtAnzahl = 0
                _objEichmarkenverwaltung.HinweismarkeGelochtAnzahlMeldestand = 3



            End If

            'füllen der Steuerelemente
            FillControls()
        End Using

    End Sub

    Private Sub FillControls()
        RadTextBoxControlBenannteStelle.Text = _objEichmarkenverwaltung.BenannteStelleAnzahl
        RadTextBoxControlBenannteStelleMelde.Text = _objEichmarkenverwaltung.BenannteStelleAnzahlMeldestand

        RadTextBoxControlCEKennzeichen.Text = _objEichmarkenverwaltung.CEAnzahl
        RadTextBoxControlCEKennzeichenMelde.Text = _objEichmarkenverwaltung.CEAnzahlMeldestand

        RadTextBoxControlEichsiegel13x13.Text = _objEichmarkenverwaltung.Eichsiegel13x13Anzahl
        RadTextBoxControlEichsiegel13x13Melde.Text = _objEichmarkenverwaltung.Eichsiegel13x13AnzahlMeldestand

        RadTextBoxControlEichsiegelRund.Text = _objEichmarkenverwaltung.EichsiegelRundAnzahl
        RadTextBoxControlEichsiegelRundMelde.Text = _objEichmarkenverwaltung.EichsiegelRundAnzahlMeldestand

        RadTextBoxControlGruenesM.Text = _objEichmarkenverwaltung.GruenesMAnzahl
        RadTextBoxControlGruenesMMelde.Text = _objEichmarkenverwaltung.GruenesMAnzahlMeldestand

        RadTextBoxControlHinweismarke.Text = _objEichmarkenverwaltung.HinweismarkeGelochtAnzahl
        RadTextBoxControlHinweismarkeMelde.Text = _objEichmarkenverwaltung.HinweismarkeGelochtAnzahlMeldestand


        RadTextBoxControlZerstoerteMarke.Text = _objEichmarkenverwaltung.zerstoerteMarke0103
        RadTextBoxControlFehlmengeGross.Text = _objEichmarkenverwaltung.FehlmengeSicherungsmarkegross
        RadTextBoxControlFehlmengeKlein.Text = _objEichmarkenverwaltung.FehlmengeSicherungsmarkeklein
        RadTextBoxControlFehlmengeHinweismarken.Text = _objEichmarkenverwaltung.FehlmengeHinweismarken

    End Sub

    Private Sub UpdateObject()
        _objEichmarkenverwaltung.BenannteStelleAnzahl = RadTextBoxControlBenannteStelle.Text
        _objEichmarkenverwaltung.BenannteStelleAnzahlMeldestand = RadTextBoxControlBenannteStelleMelde.Text

        _objEichmarkenverwaltung.CEAnzahl = RadTextBoxControlCEKennzeichen.Text
        _objEichmarkenverwaltung.CEAnzahlMeldestand = RadTextBoxControlCEKennzeichenMelde.Text

        _objEichmarkenverwaltung.Eichsiegel13x13Anzahl = RadTextBoxControlEichsiegel13x13.Text
        _objEichmarkenverwaltung.Eichsiegel13x13AnzahlMeldestand = RadTextBoxControlEichsiegel13x13Melde.Text

        _objEichmarkenverwaltung.EichsiegelRundAnzahl = RadTextBoxControlEichsiegelRund.Text
        _objEichmarkenverwaltung.EichsiegelRundAnzahlMeldestand = RadTextBoxControlEichsiegelRundMelde.Text

        _objEichmarkenverwaltung.GruenesMAnzahl = RadTextBoxControlGruenesM.Text
        _objEichmarkenverwaltung.GruenesMAnzahlMeldestand = RadTextBoxControlGruenesMMelde.Text

        _objEichmarkenverwaltung.HinweismarkeGelochtAnzahl = RadTextBoxControlHinweismarke.Text
        _objEichmarkenverwaltung.HinweismarkeGelochtAnzahlMeldestand = RadTextBoxControlHinweismarkeMelde.Text



        _objEichmarkenverwaltung.zerstoerteMarke0103 = RadTextBoxControlZerstoerteMarke.Text
        _objEichmarkenverwaltung.FehlmengeSicherungsmarkegross = RadTextBoxControlFehlmengeGross.Text
        _objEichmarkenverwaltung.FehlmengeSicherungsmarkeklein = RadTextBoxControlFehlmengeKlein.Text
        _objEichmarkenverwaltung.FehlmengeHinweismarken = RadTextBoxControlFehlmengeHinweismarken.Text

    End Sub

    Private Sub RadButtonSpeichern_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        'speichern
        Save()
    End Sub

    Friend Sub Save()
        If ValidateControls() = True Then


            'neuen Context aufbauen
            Using Context As New EichenEntities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If _objEichmarkenverwaltung.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichmarkenverwaltung As ServerEichmarkenverwaltung = Context.ServerEichmarkenverwaltung.FirstOrDefault(Function(value) value.ID = _objEichmarkenverwaltung.ID)
                    If Not dobjEichmarkenverwaltung Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objEichmarkenverwaltung = dobjEichmarkenverwaltung
                        'neuen Status zuweisen

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                Else
                    _objEichmarkenverwaltung = Context.ServerEichmarkenverwaltung.Create
                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObject()
                    'Speichern in Datenbank
                    Context.ServerEichmarkenverwaltung.Add(_objEichmarkenverwaltung)
                    Try
                        Context.SaveChanges()
                    Catch e As Entity.Validation.DbEntityValidationException
                        MessageBox.Show(e.Message)
                        For Each s In e.EntityValidationErrors
                            For Each Errd In s.ValidationErrors
                                MessageBox.Show(Errd.ErrorMessage)
                            Next
                        Next
                    End Try
                End If
            End Using
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Dim AbortSaveing As Boolean = False

        'If Debugger.IsAttached Then 'für debugzwecke
        '    Return True
        'End If
        'prüfen ob alle Felder ausgefüllt sind
        For Each Control In Me.Controls
            If TypeOf Control Is Telerik.WinControls.UI.RadTextBoxControl Then
                If Control.Text.trim.Equals("") Then
                    AbortSaveing = True

                    CType(Control, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                    CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Focus()
                End If
            End If
        Next

        If AbortSaveing Then
            MessageBox.Show("Bitte füllen Sie alle Felder aus", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist

        Return True

    End Function


    Private Sub RadTextBoxControlCEKennzeichenMelde_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlHinweismarkeMelde.Validating, RadTextBoxControlHinweismarke.Validating, RadTextBoxControlGruenesMMelde.Validating, RadTextBoxControlGruenesM.Validating, RadTextBoxControlEichsiegelRundMelde.Validating, RadTextBoxControlEichsiegelRund.Validating, RadTextBoxControlEichsiegel13x13Melde.Validating, RadTextBoxControlEichsiegel13x13.Validating, RadTextBoxControlCEKennzeichenMelde.Validating, RadTextBoxControlCEKennzeichen.Validating, RadTextBoxControlBenannteStelleMelde.Validating, RadTextBoxControlBenannteStelle.Validating
        Dim result As Decimal
        If Not sender.isreadonly = True Then

            'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
            If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text.Equals("") Then
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
                Exit Sub
            End If

            'versuchen ob der Text in eine Zahl konvertiert werden kann
            If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text, result) Then
                e.Cancel = True
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                System.Media.SystemSounds.Exclamation.Play()

            Else 'rahmen zurücksetzen
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
            End If
        End If
    End Sub
End Class
