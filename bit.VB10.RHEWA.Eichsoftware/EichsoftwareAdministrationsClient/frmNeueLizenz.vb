Public Class FrmNeueLizenz
    Private _ID As String = "-1"
    Private _objLizen As ServerLizensierung
    Private _objEichmarkenverwaltung As ServerEichmarkenverwaltung 'dieser Eintrag wird mit anlegen der Lizenz erzeugt

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
                _objLizen = (From AWG In context.ServerLizensierung Where AWG.ID = _ID).FirstOrDefault
            Else 'erzeugen einer neuen Entität
                _objLizen = context.ServerLizensierung.Create
                _objLizen.ID = "0"
                _objLizen.Lizenzschluessel = Guid.NewGuid.ToString
                _objLizen.Aktiv = True

                'ausserdem anlegen eines neuen Eichmarkenverwaltungsds
            End If

            'füllen der Steuerelemente
            FillControls()
        End Using

    End Sub

    Private Sub FillControls()
        RadTextBoxControl1.Text = _objLizen.FK_SuperofficeBenutzer
        RadTextBoxControl2.Text = _objLizen.Lizenzschluessel
        RadCheckBox1.Checked = _objLizen.RHEWALizenz
        RadCheckBox2.Checked = _objLizen.Aktiv

        RadTextBoxControlKennung.Text = _objLizen.Kennung
    End Sub

    Private Sub UpdateObject()
        _objLizen.FK_SuperofficeBenutzer = RadTextBoxControl1.Text
        _objLizen.Lizenzschluessel = RadTextBoxControl2.Text
        _objLizen.RHEWALizenz = RadCheckBox1.Checked
        _objLizen.Aktiv = RadCheckBox2.Checked

        _objLizen.Kennung = RadTextBoxControlKennung.Text


        
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
                If _objLizen.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjAWG As ServerLizensierung = Context.ServerLizensierung.FirstOrDefault(Function(value) value.ID = _objLizen.ID)
                    Dim dobjEichmarkenverwaltung As ServerEichmarkenverwaltung = Context.ServerEichmarkenverwaltung.FirstOrDefault(Function(value) value.ID = _objLizen.ID)

                    If Not dobjAWG Is Nothing AndAlso Not dobjEichmarkenverwaltung Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objLizen = dobjAWG
                        _objEichmarkenverwaltung = dobjEichmarkenverwaltung
                        'neuen Status zuweisen

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                Else
                    _objLizen = Context.ServerLizensierung.Create
                    _objEichmarkenverwaltung = Context.ServerEichmarkenverwaltung.Create
                    'eichmarken verwaltung
                    _objEichmarkenverwaltung.FK_SuperofficeBenutzer = RadTextBoxControl1.Text
                    _objEichmarkenverwaltung.BenannteStelleAnzahl = 0
                    _objEichmarkenverwaltung.BenannteStelleAnzahlMeldestand = 0
                    _objEichmarkenverwaltung.CEAnzahl = 0
                    _objEichmarkenverwaltung.CEAnzahlMeldestand = 0
                    _objEichmarkenverwaltung.Eichsiegel13x13Anzahl = 0
                    _objEichmarkenverwaltung.Eichsiegel13x13AnzahlMeldestand = 0
                    _objEichmarkenverwaltung.EichsiegelRundAnzahl = 0
                    _objEichmarkenverwaltung.EichsiegelRundAnzahlMeldestand = 0
                    _objEichmarkenverwaltung.FehlmengeHinweismarken = 0
                    _objEichmarkenverwaltung.FehlmengeSicherungsmarkegross = 0
                    _objEichmarkenverwaltung.FehlmengeSicherungsmarkeklein = 0
                    _objEichmarkenverwaltung.GruenesMAnzahl = 0
                    _objEichmarkenverwaltung.GruenesMAnzahlMeldestand = 0
                    _objEichmarkenverwaltung.HinweismarkeGelochtAnzahl = 0
                    _objEichmarkenverwaltung.HinweismarkeGelochtAnzahlMeldestand = 0
                    _objEichmarkenverwaltung.zerstoerteMarke0103 = 0

                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObject()
                    'Speichern in Datenbank
                    Context.ServerLizensierung.Add(_objLizen)
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
        'For Each Control In Me.Controls
        '    If TypeOf Control Is Telerik.WinControls.UI.RadTextBoxControl Then
        '        If Control.Text.trim.Equals("") Then
        '            AbortSaveing = True

        '            CType(Control, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
        '            CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Focus()
        '        End If
        '    End If
        'Next

        If AbortSaveing Then
            MessageBox.Show("Bitte füllen Sie alle Felder aus", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist

        Return True

    End Function


End Class
