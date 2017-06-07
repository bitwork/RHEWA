Imports Telerik.WinControls.UI

Public Class frmEingabeNeuerEichmarkenbestand
    Private _ID As String = "-1"
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

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        LadeObjekt()

        'füllen der Steuerelemente
        FillControls()
    End Sub

    Private Sub LadeObjekt()
        Using context As New HerstellerersteichungEntities()
            'abrufen der Entität aus der Datenbank
            If _ID <> "-1" Then
                _objEichmarkenverwaltung = (From Eichmarke In context.ServerEichmarkenverwaltung Where Eichmarke.ID = _ID).FirstOrDefault
                _bolNew = False
            Else 'erzeugen einer neuen Entität
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub FillControls()
        Me.Text += "für die Lizenz von HE-Kennung " & _objEichmarkenverwaltung.HEKennung & " ID " & _objEichmarkenverwaltung.FK_BenutzerID
        RadTextBoxControl1.Text = 0
        RadTextBoxControl2.Text = 0
        RadTextBoxControl3.Text = 0
        RadTextBoxControl4.Text = 0
    End Sub




    Private Sub UpdateObject()
        _objEichmarkenverwaltung.BenannteStelleAnzahlAusgeteilt += RadTextBoxControl1.Text
        _objEichmarkenverwaltung.SicherungsmarkeKleinAnzahlAusgeteilt += RadTextBoxControl2.Text
        _objEichmarkenverwaltung.SicherungsmarkeGrossAnzahlAusgeteilt += RadTextBoxControl3.Text
        _objEichmarkenverwaltung.HinweismarkeAnzahlAusgeteilt += RadTextBoxControl4.Text
    End Sub

    Private Sub RadButtonSpeichern_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        'speichern
        Save()
    End Sub

    Friend Sub Save()
        If ValidateControls() = True Then

            'neuen Context aufbauen
            Using Context As New HerstellerersteichungEntities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If _objEichmarkenverwaltung.ID <> "0" Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichmarkenverwaltung As ServerEichmarkenverwaltung = (From eichmark In Context.ServerEichmarkenverwaltung Where eichmark.ID = _ID).FirstOrDefault

                    If Not dobjEichmarkenverwaltung Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        _objEichmarkenverwaltung = dobjEichmarkenverwaltung
                        'neuen Status zuweisen
                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
                    End If
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
        Return True
    End Function

End Class