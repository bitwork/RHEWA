Public Class frmEingabeAuswertegeraet
    Private _ID As String = "-1"
    Private _objAWG As ServerLookup_Auswertegeraet
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
                _objAWG = (From AWG In context.ServerLookup_Auswertegeraet Where AWG.ID = _ID).FirstOrDefault
            Else 'erzeugen einer neuen Entität
                _objAWG = context.ServerLookup_Auswertegeraet.Create
                _objAWG.ID = "0"
            End If

            'füllen der Steuerelemente
            FillControls()
        End Using

    End Sub

    Private Sub FillControls()
        Try
            _bolSuspendEvents = True
            RadTextBoxControlAuswertegeraetBauartzulassung.Text = _objAWG.Bauartzulassung
            RadTextBoxControlAuswertegeraetBruchteilEichfehlergrenze.Text = _objAWG.BruchteilEichfehlergrenze
            RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text = _objAWG.Genauigkeitsklasse
            RadTextBoxControlAuswertegeraetGrenzWertLastwiderstandMAX.Text = _objAWG.GrenzwertLastwiderstandMAX
            RadTextBoxControlAuswertegeraetGrenzwertLastwiderstandMIN.Text = _objAWG.GrenzwertLastwiderstandMIN
            RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMAX.Text = _objAWG.GrenzwertTemperaturbereichMAX
            RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMIN.Text = _objAWG.GrenzwertTemperaturbereichMIN
            RadTextBoxControlAuswertegeraetHersteller.Text = _objAWG.Hersteller
            RadTextBoxControlAuswertegeraetKabellaengeQuerschnitt.Text = _objAWG.KabellaengeQuerschnitt
            RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswertEinbereichswaage.Text = _objAWG.MAXAnzahlTeilungswerteEinbereichswaage
            RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswerteMehrbereichswaage.Text = _objAWG.MAXAnzahlTeilungswerteMehrbereichswaage
            RadTextBoxControlAuswertegeraetMindesteingangsspannung.Text = _objAWG.Mindesteingangsspannung
            RadTextBoxControlAuswertegeraetMindestmesssignal.Text = _objAWG.Mindestmesssignal
            RadTextBoxControlAuswertegeraetPruefbericht.Text = _objAWG.Pruefbericht
            RadTextBoxControlAuswertegeraetSpeisespannung.Text = _objAWG.Speisespannung
            RadTextBoxControlAuswertegeraetTyp.Text = _objAWG.Typ

            Try
                RadCheckBoxDeaktiviert.Checked = _objAWG.Deaktiviert
            Catch e As Exception
            End Try
        Catch e As Exception
            MessageBox.Show(e.StackTrace, e.Message)
        End Try
        _bolSuspendEvents = False
    End Sub

    Private Sub UpdateObject()
        _objAWG.Bauartzulassung = RadTextBoxControlAuswertegeraetBauartzulassung.Text
        _objAWG.BruchteilEichfehlergrenze = RadTextBoxControlAuswertegeraetBruchteilEichfehlergrenze.Text
        _objAWG.Genauigkeitsklasse = RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text
        _objAWG.GrenzwertLastwiderstandMAX = RadTextBoxControlAuswertegeraetGrenzWertLastwiderstandMAX.Text
        _objAWG.GrenzwertLastwiderstandMIN = RadTextBoxControlAuswertegeraetGrenzwertLastwiderstandMIN.Text
        _objAWG.GrenzwertTemperaturbereichMAX = RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMAX.Text
        _objAWG.GrenzwertTemperaturbereichMIN = RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMIN.Text
        _objAWG.Hersteller = RadTextBoxControlAuswertegeraetHersteller.Text
        _objAWG.KabellaengeQuerschnitt = RadTextBoxControlAuswertegeraetKabellaengeQuerschnitt.Text
        _objAWG.MAXAnzahlTeilungswerteEinbereichswaage = RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswertEinbereichswaage.Text
        _objAWG.MAXAnzahlTeilungswerteMehrbereichswaage = RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswerteMehrbereichswaage.Text
        _objAWG.Mindesteingangsspannung = RadTextBoxControlAuswertegeraetMindesteingangsspannung.Text
        _objAWG.Mindestmesssignal = RadTextBoxControlAuswertegeraetMindestmesssignal.Text
        _objAWG.Pruefbericht = RadTextBoxControlAuswertegeraetPruefbericht.Text
        _objAWG.Speisespannung = RadTextBoxControlAuswertegeraetSpeisespannung.Text
        _objAWG.Typ = RadTextBoxControlAuswertegeraetTyp.Text
        _objAWG.ErstellDatum = Now

        _objAWG.Deaktiviert = RadCheckBoxDeaktiviert.Checked
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
                If _objAWG.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjAWG As ServerLookup_Auswertegeraet = Context.ServerLookup_Auswertegeraet.FirstOrDefault(Function(value) value.ID = _objAWG.ID)
                    If Not dobjAWG Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objAWG = dobjAWG
                        'neuen Status zuweisen

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Try
                            Context.SaveChanges()
                        Catch ex As Entity.Validation.DbEntityValidationException
                            For Each ex2 In ex.EntityValidationErrors
                                For Each entry In ex2.ValidationErrors
                                    MessageBox.Show(entry.ErrorMessage)
                                Next

                            Next
                            Exit Sub

                        End Try

                    End If
                Else
                    _objAWG = Context.ServerLookup_Auswertegeraet.Create
                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObject()
                    _objAWG.ID = Guid.NewGuid.ToString
                    'Speichern in Datenbank
                    Context.ServerLookup_Auswertegeraet.Add(_objAWG)
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

        _bolSuspendEvents = True
        For Each Control In Me.Controls
            If TypeOf Control Is Telerik.WinControls.UI.RadTextBoxControl Then
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

        If RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text.ToUpper = "I" Or RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text.ToUpper = "II" Or RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text.ToUpper = "III" Or RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Text = "IV".ToUpper Then
        Else
            'Ungültiger Wert für Genauigikeitsklasse
            MessageBox.Show("Bitte geben Sie eine gültige Genauigkeitsklasse ein (I,II,III,IV)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            AbortSaveing = True
            RadTextBoxControlAuswertegeraetGenauigkeitsklasse.TextBoxElement.BorderColor = Color.Red
            RadTextBoxControlAuswertegeraetGenauigkeitsklasse.Focus()
            _bolSuspendEvents = False
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        _bolSuspendEvents = False
        Return True

    End Function

    Private Sub RadTextBoxControlAuswertegeraetBruchteilEichfehlergrenze_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlAuswertegeraetSpeisespannung.Validating, RadTextBoxControlAuswertegeraetMindestmesssignal.Validating, RadTextBoxControlAuswertegeraetMindesteingangsspannung.Validating, RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswerteMehrbereichswaage.Validating, RadTextBoxControlAuswertegeraetMAXAnzahlTeilungswertEinbereichswaage.Validating, RadTextBoxControlAuswertegeraetKabellaengeQuerschnitt.Validating, RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMIN.Validating, RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMAX.Validating, RadTextBoxControlAuswertegeraetGrenzwertLastwiderstandMIN.Validating, RadTextBoxControlAuswertegeraetGrenzWertLastwiderstandMAX.Validating, RadTextBoxControlAuswertegeraetBruchteilEichfehlergrenze.Validating
        If _bolSuspendEvents = True Then Exit Sub


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
                'prüfen ob negative zahlen eingegeben wurden
                If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Name.Equals(RadTextBoxControlAuswertegeraetGrenzwertTemperaturbereichMIN.Name) Then
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
