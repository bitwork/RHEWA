Public Class frmEingabeWaegezelle
    Private _ID As String = "-1"
    Private _objWZ As ServerLookup_Waegezelle
    Private _bolSuspendEvents As Boolean = False
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
                _objWZ = (From WZ In context.ServerLookup_Waegezelle Where WZ.ID = _ID).FirstOrDefault
            Else 'erzeugen einer neuen Entität
                _objWZ = context.ServerLookup_Waegezelle.Create

                _objWZ.ID = "0"
            End If

            'füllen der Steuerelemente
            FillControls()
        End Using

    End Sub

    Private Sub FillControls()
        Try
            _bolSuspendEvents = True
            RadTextBoxControlWaegezelleBauartzulassung.Text = _objWZ.Bauartzulassung
            RadTextBoxControlWaegezelleBruchteilEichfehlergrenze.Text = _objWZ.BruchteilEichfehlergrenze
            RadTextBoxControlWaegezelleGenauigkeitsklasse.Text = _objWZ.Genauigkeitsklasse
            RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMAX.Text = _objWZ.GrenzwertTemperaturbereichMAX
            RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMIN.Text = _objWZ.GrenzwertTemperaturbereichMIN
            RadTextBoxControlWaegezelleHersteller.Text = _objWZ.Hersteller
            RadTextBoxControWaegezelleHoechstteilungsfaktor.Text = _objWZ.Hoechsteteilungsfaktor
            RadTextBoxControlWaegezelleKriechteilungsfaktor.Text = _objWZ.Kriechteilungsfaktor
            RadTextBoxControlWaegezelleMAXAnzahlTeilungswerte.Text = _objWZ.MaxAnzahlTeilungswerte
            RadTextBoxControlWaegezelleMindestvorlast.Text = _objWZ.Mindestvorlast
            RadTextBoxControlWaegezelleMindestvorlastProzent.Text = _objWZ.MindestvorlastProzent

            RadTextBoxControlWaegezelleMinTeilungswert.Text = _objWZ.MinTeilungswert
            RadTextBoxControlWaegezellePruefbericht.Text = _objWZ.Pruefbericht
            RadTextBoxControlWaegezelleRueckkehrVorlastsignal.Text = _objWZ.RueckkehrVorlastsignal
            RadTextBoxControlWaegezelleWaegezellenkennwert.Text = _objWZ.Waegezellenkennwert
            RadTextBoxControlWaegezelleWiderstandWaegezelle.Text = _objWZ.WiderstandWaegezelle
            RadTextBoxControlWaegezelleTyp.Text = _objWZ.Typ

            Try
                RadCheckBoxDeaktiviert.Checked = _objWZ.Deaktiviert
            Catch e As Exception
            End Try
            Try
                RadCheckBoxNeueWZ.Checked = _objWZ.Neu
            Catch e As Exception
            End Try
        Catch e As Exception
            MessageBox.Show(e.StackTrace, e.Message)
        End Try
        _bolSuspendEvents = False
    End Sub

    Private Sub UpdateObject()
        _objWZ.Hoechsteteilungsfaktor = RadTextBoxControWaegezelleHoechstteilungsfaktor.Text
        _objWZ.Kriechteilungsfaktor = RadTextBoxControlWaegezelleKriechteilungsfaktor.Text
        _objWZ.MaxAnzahlTeilungswerte = RadTextBoxControlWaegezelleMAXAnzahlTeilungswerte.Text
        _objWZ.Mindestvorlast = RadTextBoxControlWaegezelleMindestvorlast.Text
        _objWZ.MindestvorlastProzent = RadTextBoxControlWaegezelleMindestvorlastProzent.Text

        _objWZ.MinTeilungswert = RadTextBoxControlWaegezelleMinTeilungswert.Text
        _objWZ.RueckkehrVorlastsignal = RadTextBoxControlWaegezelleRueckkehrVorlastsignal.Text
        _objWZ.Waegezellenkennwert = RadTextBoxControlWaegezelleWaegezellenkennwert.Text
        _objWZ.WiderstandWaegezelle = RadTextBoxControlWaegezelleWiderstandWaegezelle.Text
        _objWZ.Bauartzulassung = RadTextBoxControlWaegezelleBauartzulassung.Text
        _objWZ.BruchteilEichfehlergrenze = RadTextBoxControlWaegezelleBruchteilEichfehlergrenze.Text
        _objWZ.Genauigkeitsklasse = RadTextBoxControlWaegezelleGenauigkeitsklasse.Text
        _objWZ.GrenzwertTemperaturbereichMAX = RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMAX.Text
        _objWZ.GrenzwertTemperaturbereichMIN = RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMIN.Text
        _objWZ.Hersteller = RadTextBoxControlWaegezelleHersteller.Text
        _objWZ.Pruefbericht = RadTextBoxControlWaegezellePruefbericht.Text
        _objWZ.Typ = RadTextBoxControlWaegezelleTyp.Text

        _objWZ.Deaktiviert = RadCheckBoxDeaktiviert.Checked
        _objWZ.Neu = RadCheckBoxNeueWZ.Checked

        _objWZ.ErstellDatum = Now
    End Sub

    Private Sub RadButtonSpeichern_Click(sender As Object, e As EventArgs) Handles RadButtonSpeichern.Click
        'speichern
        Save()
    End Sub

    Friend Sub Save()
        If ValidateControls() = True Then

            'neuen Context aufbauen
            Using Context As New EichenEntities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If _objWZ.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjWZ As ServerLookup_Waegezelle = Context.ServerLookup_Waegezelle.FirstOrDefault(Function(value) value.ID = _objWZ.ID)
                    If Not dobjWZ Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objWZ = dobjWZ

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                Else
                    _objWZ = Context.ServerLookup_Waegezelle.Create

                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObject()
                    _objWZ.ID = Guid.NewGuid.ToString

                    Context.ServerLookup_Waegezelle.Add(_objWZ)
                    'Speichern in Datenbank
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

        ''If Debugger.IsAttached Then 'für debugzwecke
        ''    Return True
        ''End If
        'prüfen ob alle Felder ausgefüllt sind
        _bolSuspendEvents = True
        For Each Control In Me.Controls
            If TypeOf Control Is Telerik.WinControls.UI.RadTextBoxControl Then
                If Control.Equals(RadTextBoxControlWaegezelleBauartzulassung) Then Continue For
                If Control.Equals(RadTextBoxControlWaegezelleMinTeilungswert) Then Continue For
                If Control.Equals(RadTextBoxControlWaegezelleRueckkehrVorlastsignal) Then Continue For

                If Control.Text.trim.Equals("") Then
                    AbortSaveing = True
                    '      CType(Control, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder
                    CType(Control, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                    CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Focus()
                End If
            End If
        Next
        If AbortSaveing Then
            MessageBox.Show("Bitte füllen Sie alle Felder aus", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            _bolSuspendEvents = False
            Return False
        End If

        If RadTextBoxControlWaegezelleGenauigkeitsklasse.Text.ToUpper = "A" Or RadTextBoxControlWaegezelleGenauigkeitsklasse.Text.ToUpper = "B" Or RadTextBoxControlWaegezelleGenauigkeitsklasse.Text.ToUpper = "C" Or RadTextBoxControlWaegezelleGenauigkeitsklasse.Text = "D".ToUpper Then
        Else
            'Ungültiger Wert für Genauigikeitsklasse
            MessageBox.Show("Bitte geben Sie eine gültige Genauigkeitsklasse ein (A,B,C oder D)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            AbortSaveing = True
            RadTextBoxControlWaegezelleGenauigkeitsklasse.TextBoxElement.BorderColor = Color.Red
            RadTextBoxControlWaegezelleGenauigkeitsklasse.Focus()
            _bolSuspendEvents = False
            Return False
        End If

        'Speichern soll nicht abgebrochen werden, da alles okay ist
        _bolSuspendEvents = False
        Return True

    End Function

    Private Sub RadTextBoxControlWaegezelleBruchteilEichfehlergrenze_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControWaegezelleHoechstteilungsfaktor.Validating, RadTextBoxControlWaegezelleWiderstandWaegezelle.Validating, RadTextBoxControlWaegezelleWaegezellenkennwert.Validating, RadTextBoxControlWaegezelleRueckkehrVorlastsignal.Validating, RadTextBoxControlWaegezelleMinTeilungswert.Validating, RadTextBoxControlWaegezelleMindestvorlast.Validating, RadTextBoxControlWaegezelleMindestvorlastProzent.Validating, RadTextBoxControlWaegezelleMAXAnzahlTeilungswerte.Validating, RadTextBoxControlWaegezelleKriechteilungsfaktor.Validating, RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMIN.Validating, RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMAX.Validating, RadTextBoxControlWaegezelleBruchteilEichfehlergrenze.Validating
        Dim result As Decimal
        If _bolSuspendEvents = True Then Exit Sub
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
                'prüfen ob negative zahlen eingegeben wurden
                'prüfen ob negative zahlen eingegeben wurden
                If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Name.Equals(RadTextBoxControlWaegezelleGrenzwertTemperaturbereichMIN.Name) Then
                    CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
                ElseIf sender.text.ToString.Trim.StartsWith("-") Then
                    e.Cancel = True
                    CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                    System.Media.SystemSounds.Exclamation.Play()

                Else
                    CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)

                End If

            End If
        End If
    End Sub

    Private Sub RadButtonAbbrechen_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        Me.Close()
    End Sub

End Class