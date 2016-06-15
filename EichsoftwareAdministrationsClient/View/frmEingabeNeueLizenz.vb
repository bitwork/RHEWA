Imports Telerik.WinControls.UI

Public Class frmEingabeNeueLizenz
    Private _ID As String = "-1"
    Private _objLizen As ServerLizensierung
    Private _objEichmarkenverwaltung As ServerEichmarkenverwaltung 'dieser Eintrag wird mit anlegen der Lizenz erzeugt
    Private _bolNew As Boolean = False
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
        LadeObjekt()

        'füllen der Steuerelemente
        LadeDropDownDatenquelle()
        FormatiereDropDown()
        FillControls()
    End Sub

    Private Sub LadeObjekt()
        Using context As New EichenEntities()
            'abrufen der Entität aus der Datenbank
            If _ID <> "-1" Then
                _objLizen = (From AWG In context.ServerLizensierung Where AWG.ID = _ID).FirstOrDefault
                _bolNew = False
            Else 'erzeugen einer neuen Entität
                _objLizen = context.ServerLizensierung.Create
                _objLizen.ID = "0"
                _objLizen.Lizenzschluessel = Guid.NewGuid.ToString
                _objLizen.Aktiv = True
                _bolNew = True

                'ausserdem anlegen eines neuen Eichmarkenverwaltungsds
            End If
        End Using
    End Sub

    Private Sub FillControls()
        ''todo Dropdown Wert zuweisen aus Objekt

        RadTextBoxControl1.Text = _objLizen.HEKennung
        RadTextBoxControl2.Text = _objLizen.Lizenzschluessel
        RadCheckBox1.Checked = _objLizen.RHEWALizenz
        RadCheckBox2.Checked = _objLizen.Aktiv

        'GRID umschalten, je nachdem ob ein DS geöffnet wird oder ein neuer angelegt wird
        RadMultiColumnComboBoxBenutzer.Enabled = _bolNew
    End Sub

    Private Sub LadeDropDownDatenquelle()
        Try

            Using context As New EichenEntities
                Dim Benutzer As Entity.DbSet(Of Benutzer) = context.Benutzer
                Dim Firmen As Entity.DbSet(Of Firmen) = context.Firmen

                If _bolNew Then 'lade alle ohne Eintrag in Lizenztabelle
                    'ausfilterung von DS die bereits einen Eintrag in der Lizenz Tabelle haben

                    'abrufen der Entität aus der Datenbank
                    Dim query = From Benutz In Benutzer
                                Join Firma In Firmen On Benutz.Firma_FK Equals Firma.ID Order By Benutz.Nachname
                                Select New With
                                {
                                    Benutz.ID,
                                    Benutz.HEKennung,
                                    Benutz.Nachname,
                                    Benutz.Vorname,
                                    .Firma = Firma.Name,
                                    .Anzeigename = Benutz.Nachname + ", " + Benutz.Vorname
                                }

                    'ab hier sind alle Benutzer bekannt.
                    'nun kontrollieren ob es für die Benutzer bereits einen Eintrag in der Lizenztabelle gibt

                    Dim ListFilteredBenutzer As New List(Of Object)
                    Dim ListLizenzBenutzerFKs As New List(Of String)

                    Dim Lizenzen = From lic In context.ServerLizensierung
                    For Each objLiz In Lizenzen
                        If Not ListLizenzBenutzerFKs.Contains(objLiz.FK_BenutzerID) Then
                            ListLizenzBenutzerFKs.Add(objLiz.FK_BenutzerID)
                        End If
                    Next
                    'ab hier sind alle vergebenen Benutzerlizenzen bekannt
                    For Each objBenutzer In query
                        If Not ListLizenzBenutzerFKs.Contains(objBenutzer.ID) Then
                            ListFilteredBenutzer.Add(objBenutzer)
                        End If
                    Next
                    'databinding
                    RadMultiColumnComboBoxBenutzer.DataSource = ListFilteredBenutzer
                    RadMultiColumnComboBoxBenutzer.DisplayMember = "Anzeigename"
                Else ' lade Benutzer ahnhand von ID
                    'abrufen der Entität aus der Datenbank
                    Dim query = From Benutz In Benutzer Join Firma In Firmen On Benutz.Firma_FK Equals Firma.ID Where Benutz.ID = _objLizen.FK_BenutzerID
                                Select New With
                   {
                       Benutz.ID,
                       Benutz.HEKennung,
                        Benutz.Nachname,
                        Benutz.Vorname,
                        .Firma = Firma.Name,
                    .Anzeigename = Benutz.Nachname + ", " + Benutz.Vorname
                    }

                    'databinding
                    RadMultiColumnComboBoxBenutzer.DataSource = query.ToList
                    RadMultiColumnComboBoxBenutzer.DisplayMember = "Anzeigename"
                End If

            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FormatiereDropDown()
        Try
            'Ändern des Dropdown Designs
            Dim combo As RadMultiColumnComboBoxElement = RadMultiColumnComboBoxBenutzer.MultiColumnComboBoxElement ' get a reference to the grid viewDimgridAsRadGridView = combo.EditorControl
            ' get a reference to the grid viewDimgridAsRadGridView = combo.EditorControl
            Dim grid As RadGridView = combo.EditorControl

            grid.BestFitColumns()
            grid.ShowFilteringRow = True

            RadMultiColumnComboBoxBenutzer.MultiColumnComboBoxElement.Columns("Anzeigename").IsVisible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadMultiColumnComboBoxBenutzer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadMultiColumnComboBoxBenutzer.SelectedIndexChanged
        Try
            Dim selecteditem = RadMultiColumnComboBoxBenutzer.SelectedItem
            Dim id As String = selecteditem.databounditem.id
            If Not id Is Nothing Then
                Using context As New EichenEntities()
                    Dim Benutzer = (From Benut In context.Benutzer Where Benut.ID = id).FirstOrDefault
                    RadTextBoxControl1.Text = Benutzer.HEKennung
                End Using
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateObject()
        Try
            Dim selecteditem = RadMultiColumnComboBoxBenutzer.SelectedItem
            Dim id As String = selecteditem.databounditem.id
            _objLizen.FK_BenutzerID = id
        Catch ex As Exception
            MessageBox.Show("Konnte BenutzerID nicht auslesen")
        End Try

        _objLizen.HEKennung = RadTextBoxControl1.Text
        _objLizen.Lizenzschluessel = RadTextBoxControl2.Text
        _objLizen.RHEWALizenz = RadCheckBox1.Checked
        _objLizen.Aktiv = RadCheckBox2.Checked
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
                    Dim dobjLic As ServerLizensierung = (From Lic In Context.ServerLizensierung Where Lic.ID = _objLizen.ID).FirstOrDefault
                    Dim dobjEichmarkenverwaltung As ServerEichmarkenverwaltung = (From eichmark In Context.ServerEichmarkenverwaltung Where eichmark.FK_BenutzerID = _objLizen.FK_BenutzerID).FirstOrDefault

                    If Not dobjLic Is Nothing AndAlso Not dobjEichmarkenverwaltung Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objLizen = dobjLic
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

                    _objEichmarkenverwaltung.HEKennung = RadTextBoxControl1.Text

                    Try
                        Dim selecteditem = RadMultiColumnComboBoxBenutzer.SelectedItem
                        Dim id As String = selecteditem.databounditem.id
                        _objLizen.FK_BenutzerID = id
                    Catch ex As Exception
                        MessageBox.Show("Konnte BenutzerID nicht auslesen")
                    End Try

                    _objEichmarkenverwaltung.FK_BenutzerID = _objLizen.FK_BenutzerID
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

        'prüfen ob die ID des Benutzer aus der Dropdownbox gelesen werden kann
        If RadMultiColumnComboBoxBenutzer.SelectedItem Is Nothing Then
            AbortSaveing = True
        Else
            Try
                Dim selecteditem = RadMultiColumnComboBoxBenutzer.SelectedItem
                Dim id As String = selecteditem.databounditem.id
                If id Is Nothing Then
                    AbortSaveing = True
                End If
            Catch ex As Exception
                AbortSaveing = True
            End Try
        End If
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