Imports Telerik.WinControls.UI

Public Class frmEingabeFirmenVertragspartnerZuordnung

    Private _ID As String = "-1"
    Private _objFirmenzuordnung As ServerLookupVertragspartnerFirma 'dieser Eintrag wird mit anlegen der Lizenz erzeugt
    Private _bolNew As Boolean = False
    Sub New(ByVal pID As string)
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

    Private Sub frmFirmenzuordnung_Load(sender As Object, e As EventArgs) Handles Me.Load
        LadeObjekt()

        'füllen der Steuerelemente
        LadeDropDownDatenquelle()
        FillControls()
    End Sub

    Private Sub LadeObjekt()
        Using context As New HerstellerersteichungEntities()
            'abrufen der Entität aus der Datenbank
            If _ID <> "-1" Then
                _objFirmenzuordnung = (From Firmen In context.ServerLookupVertragspartnerFirma Where Firmen.ID = _ID).FirstOrDefault
                _bolNew = False
            Else 'erzeugen einer neuen Entität
                _objFirmenzuordnung = context.ServerLookupVertragspartnerFirma.Create
                _objFirmenzuordnung.ID = "0"
                _bolNew = True
            End If
        End Using
    End Sub

    Private Sub FillControls()
        If _bolNew Then
            RadButtonLoeschen.Visible = False
        Else
            RadButtonLoeschen.Visible = True
        End If

        RadDropDownListHauptfirma.SelectedValue = _objFirmenzuordnung.Firma_FK
        RadDropDownListUnterfirma.SelectedValue = _objFirmenzuordnung.Vertragspartner_FK
    End Sub

    Private Sub LadeDropDownDatenquelle()
        Try
            Using context As New HerstellerersteichungEntities
                'abrufen der Entität aus der Datenbank
                Dim query = From Firma In context.Firmen Order By Firma.Name
                'ab hier sind alle Benutzer bekannt.
                'nun kontrollieren ob es für die Benutzer bereits einen Eintrag in der Lizenztabelle gibt
                Dim sourceFirmen = query.ToList
                Dim sourceVertragspartnerFirmen = query.ToList
                RadDropDownListHauptfirma.DataSource = sourceFirmen
                RadDropDownListHauptfirma.ValueMember = "ID"
                RadDropDownListHauptfirma.DisplayMember = "Name"

                RadDropDownListUnterfirma.DataSource = sourceVertragspartnerFirmen
                RadDropDownListUnterfirma.ValueMember = "ID"
                RadDropDownListUnterfirma.DisplayMember = "Name"
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub UpdateObject()
        Try
            Dim selecteditem = RadDropDownListHauptfirma.SelectedItem
            Dim id As String = selecteditem.DataBoundItem.id
            _objFirmenzuordnung.Firma_FK = id
        Catch ex As Exception
            MessageBox.Show("Konnte Hauptfirma nicht auslesen")
        End Try

        Try
            Dim selecteditem = RadDropDownListUnterfirma.SelectedItem
            Dim id As String = selecteditem.DataBoundItem.id
            _objFirmenzuordnung.Vertragspartner_FK = id
        Catch ex As Exception
            MessageBox.Show("Konnte vertragspartner nicht auslesen")
        End Try
    End Sub

    Private Sub RadButtonSpeichern_Click(sender As Object, e As EventArgs) Handles RadButtonSpeichern.Click
        'speichern
        Save()
    End Sub

    Friend Sub Save()
        If ValidateControls() = True Then
            'neuen Context aufbauen
            Using Context As New HerstellerersteichungEntities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If _objFirmenzuordnung.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann

                    Dim dbobjFirmenzuordnung As ServerLookupVertragspartnerFirma = (From firma In Context.ServerLookupVertragspartnerFirma Where firma.ID = _objFirmenzuordnung.ID).FirstOrDefault

                    If Not dbobjFirmenzuordnung Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.

                        _objFirmenzuordnung = dbobjFirmenzuordnung
                        'neuen Status zuweisen

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                Else
                    _objFirmenzuordnung = Context.ServerLookupVertragspartnerFirma.Create
                    _objFirmenzuordnung.ID = Guid.NewGuid.ToString
                    'Füllt das Objekt mit den Werten aus den Steuerlementen
                    UpdateObject()

                    'Speichern in Datenbank
                    Context.ServerLookupVertragspartnerFirma.Add(_objFirmenzuordnung)
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
        Dim idHauptfirma As String = ""
        Dim idUnterfirma As String = ""

        'prüfen ob die ID der Hauptfirma aus der Dropdownbox gelesen werden kann
        If RadDropDownListHauptfirma.SelectedItem Is Nothing Then
            AbortSaveing = True
        Else
            Try
                Dim selecteditem = RadDropDownListHauptfirma.SelectedItem
                idHauptfirma = selecteditem.DataBoundItem.id
                If idHauptfirma Is Nothing Then
                    AbortSaveing = True
                End If
            Catch ex As Exception
                AbortSaveing = True
            End Try
        End If

        'prüfen ob die ID des Vertragspartners aus der Dropdownbox gelesen werden kann
        If RadDropDownListUnterfirma.SelectedItem Is Nothing Then
            AbortSaveing = True
        Else
            Try
                Dim selecteditem = RadDropDownListUnterfirma.SelectedItem
                idUnterfirma = selecteditem.DataBoundItem.id
                If idUnterfirma Is Nothing Then
                    AbortSaveing = True
                End If
            Catch ex As Exception
                AbortSaveing = True
            End Try
        End If

        If AbortSaveing Then
            MessageBox.Show("Bitte füllen Sie alle Felder aus", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        ' prüfen ob Hauptfirma = Nebenfirma
        If idHauptfirma.Equals(idUnterfirma) Then
            MessageBox.Show("Die Hauptfirma kann nicht der Vertragspartner sein.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        '################################################################

        ' prüfen ob Nebenfirma die Hauptfirma zugeordnet wird, nicht bereits der nebenfirma zugeordnet ist (kreuzverweis)
        Try
            Using context As New HerstellerersteichungEntities
                Dim query = From FirmenZuordnung In context.ServerLookupVertragspartnerFirma Where FirmenZuordnung.Firma_FK = idUnterfirma And FirmenZuordnung.Vertragspartner_FK = idHauptfirma

                If query.Count <> 0 Then
                    MessageBox.Show("Die gewählte Hauptfirma ist bereits Vertragspartner des jetzt ausgewählten Vertragspartners (Kreuzverweis).", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
            Return False
        End Try
        '################################################################
        'prüfen ob wenn bolNeu = true die aktuell gewählte Zuordnung von Firmen bereits existiert
        Try
            If _bolNew Then

                Using context As New HerstellerersteichungEntities
                    Dim query = From FirmenZuordnung In context.ServerLookupVertragspartnerFirma Where FirmenZuordnung.Firma_FK = idHauptfirma And FirmenZuordnung.Vertragspartner_FK = idUnterfirma

                    If query.Count <> 0 Then
                        MessageBox.Show("Die gewählte Zuordnung ist bereits vorhanden.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
            Return False
        End Try
        '##############################################################
        'prüfen ob es bereits zum gewählten Vertragspartner eine Zurodnung gibt. Laut Herrn Strack darf eine Hauptfirma zwar mehrere vertragspartner haben, aber eine Unterfirma nur einen Hauptfirma
        Try
            If _bolNew Then

                Using context As New HerstellerersteichungEntities
                    Dim query = From FirmenZuordnung In context.ServerLookupVertragspartnerFirma Where FirmenZuordnung.Vertragspartner_FK = idUnterfirma And FirmenZuordnung.Firma_FK IsNot Nothing

                    If query.Count <> 0 Then
                        MessageBox.Show("Es gibt zum aktuellen Vertragspartner bereis eine andere Hauptfirma", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
            Return False
        End Try

        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Return True

    End Function

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        Me.Close()
    End Sub

    Private Sub RadButtonLoeschen_Click(sender As Object, e As EventArgs) Handles RadButtonLoeschen.Click
        Try
            LoescheZuordnung()
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoescheZuordnung()
        If ValidateControls() = True Then
            'neuen Context aufbauen
            Using Context As New HerstellerersteichungEntities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If _objFirmenzuordnung.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann

                    Dim dbobjFirmenzuordnung As ServerLookupVertragspartnerFirma = (From firma In Context.ServerLookupVertragspartnerFirma Where firma.ID = _objFirmenzuordnung.ID).FirstOrDefault

                    If Not dbobjFirmenzuordnung Is Nothing Then
                        Context.ServerLookupVertragspartnerFirma.Remove(dbobjFirmenzuordnung)
                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
                Else
                    Exit Sub
                End If
            End Using
            Me.Close()
        End If
    End Sub
End Class