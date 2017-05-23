Imports System
Imports System.IO
Imports Telerik.WinControls.UI

Public Class frmFirmenZusatzdaten

#Region "Deklaration"
    Private EditMode As Boolean = False 'gibt an ob ich im Editierungsmodus bin (beim Klick auf bearbeiten)
#End Region

#Region "Methoden"
    Private Sub FormatGrid()
        Try
            Try
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("ID").VisibleInColumnChooser = False
            Catch e As Exception
            End Try
            Try
                RadGridView1.Columns("Firma").ReadOnly = True
            Catch e As Exception
            End Try
            Try
                RadGridView1.Columns("Land").ReadOnly = True
            Catch e As Exception
            End Try

            Try
                RadGridView1.Columns("BeginnVertrag").FormatString = "{0: dd.MM.yyyy}"
                RadGridView1.Columns("EndeVertrag").FormatString = "{0: dd.MM.yyyy}"
                RadGridView1.Columns("Erstschulung").FormatString = "{0: dd.MM.yyyy}"
                RadGridView1.Columns("MonatJahrZertifikat").FormatString = "{0: dd.MM.yyyy}"
                RadGridView1.Columns("Nachschulung").FormatString = "{0: dd.MM.yyyy}"
                RadGridView1.Columns("LetztesAudit").FormatString = "{0: dd.MM.yyyy}"
            Catch ex As Exception

            End Try

            Try
                RadGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None
                RadGridView1.AllowAutoSizeColumns = True
                RadGridView1.BestFitColumns()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        RadGridView1.BestFitColumns()
    End Sub

    Private Sub LoadFromDatabase()
        Try
            SuspendLayout()
            'Firmen Grid
            Using Context As New HerstellerersteichungEntities
                'Daten aus eichmarkenverwaltungstabelle und Benutzertabelle zusammenführen. Fürs Databinding Casten in einen neuen Typen
                Dim Data = From FirmenZusatzdaten In Context.ServerFirmenZusatzdaten
                           Join Firma In Context.Firmen On Firma.ID Equals FirmenZusatzdaten.Firmen_FK
                           Select New With {
                                    FirmenZusatzdaten.ID,
                                            .Firma = Firma.Name,
                               Firma.Land,
                                            FirmenZusatzdaten.Abrechnungsmodell,
                                            FirmenZusatzdaten.BeginnVertrag,
                                            FirmenZusatzdaten.EndeVertrag,
                                            FirmenZusatzdaten.Erstschulung,
                                            FirmenZusatzdaten.LetztesAudit,
                                            FirmenZusatzdaten.MonatJahrZertifikat,
                                            FirmenZusatzdaten.Nachschulung,
                                           FirmenZusatzdaten.Qualifizierungspauschale
                           }

                'databinding
                RadGridView1.DataSource = Data.ToList
                '##################################################################

                'Kindtabelle Firmendaten

                '##################################################################

                'Grid Formatieren
                FormatGrid()
            End Using

            ResumeLayout()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Iteritert das Grid und speichert alle Änderungen in die Datenbank. Setzt ausserdem den gesperrt Status zurück
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveGrid()
        Try
            Dim SelectedId As String

            Using context As New HerstellerersteichungEntities
                For Each row In RadGridView1.Rows
                    SelectedId = row.Cells("ID").Value
                    Dim objZusatzdaten As ServerFirmenZusatzdaten = (From FirmenZusatzdaten In context.ServerFirmenZusatzdaten Where FirmenZusatzdaten.ID = SelectedId Select FirmenZusatzdaten).FirstOrDefault

                    If Not row.DataBoundItem Is Nothing Then
                        If Not objZusatzdaten Is Nothing Then
                            Try
                                objZusatzdaten.Abrechnungsmodell = row.Cells("Abrechnungsmodell").Value
                                objZusatzdaten.BeginnVertrag = row.Cells("BeginnVertrag").Value
                                objZusatzdaten.EndeVertrag = row.Cells("EndeVertrag").Value
                                objZusatzdaten.Erstschulung = row.Cells("Erstschulung").Value
                                objZusatzdaten.LetztesAudit = row.Cells("LetztesAudit").Value
                                objZusatzdaten.MonatJahrZertifikat = row.Cells("MonatJahrZertifikat").Value
                                objZusatzdaten.Nachschulung = row.Cells("Nachschulung").Value
                                objZusatzdaten.Qualifizierungspauschale = row.Cells("Qualifizierungspauschale").Value

                                context.SaveChanges()
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                Next
            End Using

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Methode die die DS Sperrung vom aktuellen Benutzer aufhebt.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EntsperreDS()
        Try
            Dim username As String = System.Environment.UserName
            Using context As New HerstellerersteichungEntities
                Dim CollectionEichmarken = (From Eichmarkenverwaltung In context.ServerEichmarkenverwaltung Where Eichmarkenverwaltung.ZurBearbeitungGesperrtDurch = username Select Eichmarkenverwaltung)

                For Each Eichmarkenverwaltung In CollectionEichmarken
                    Try
                        'zurücksetzten des gesperrt flags
                        Eichmarkenverwaltung.ZurBearbeitungGesperrtDurch = ""
                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)
                    End Try
                Next
                context.SaveChanges()

            End Using
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Lädt Gridlayout aus settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LadeGridLayout()
        Try
            Using stream As New MemoryStream(Convert.FromBase64String(My.Settings.GridSettingsAuswahllisteEichmarkenverwaltung))
                RadGridView1.LoadLayout(stream)
            End Using
        Catch ex As Exception
            'zurücksetzten des Layouts
            My.Settings.GridSettingsAuswahllisteEichmarkenverwaltung = ""
            My.Settings.Save()
        End Try
    End Sub

    ''' <summary>
    ''' Speichert Grid in Settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SpeichereGridLayout()
        'speichere Layout der beiden Grids
        Dim grid = RadGridView1

        Try
            Using stream As New MemoryStream()
                grid.SaveLayout(stream)
                stream.Position = 0
                Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                stream.Read(buffer, 0, buffer.Length)
                My.Settings.GridSettingsAuswahllisteEichmarkenverwaltung = Convert.ToBase64String(buffer)
                My.Settings.Save()
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Form Events"

    Private Sub FrmAuswahllisteWZ_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadFromDatabase()
    End Sub

    Private Sub FrmEichmarkenverwaltung_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'prüfen Edit Mode
        If EditMode Then
            If MessageBox.Show("Möchten Sie Ihre Änderungen wirklich rückgängig machen?", "Frage", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If
        End If
        SpeichereGridLayout()

        'gesperrte DS des aktuellen Nutzers entsperren
        EntsperreDS()

    End Sub

#Region "Bearbeitungs / Speicheroutine"
    Private Sub RadButtonBearbeiten_Click(sender As Object, e As EventArgs) Handles RadButtonBearbeiten.Click
        RadButtonBearbeiten.Visible = False
        RadButtonAbbrechen.Visible = True
        RadButtonSpeichern.Visible = True
        EditMode = True
        RadGridView1.AllowEditRow = True

        LoadFromDatabase()
    End Sub

    Private Sub RadButtonSpeichern_Click(sender As Object, e As EventArgs) Handles RadButtonSpeichern.Click
        RadButtonBearbeiten.Visible = True
        RadButtonAbbrechen.Visible = False
        RadButtonSpeichern.Visible = False
        RadGridView1.AllowEditRow = False
        EditMode = False
        'alle DS die von einem Selbstgesperrt wurden suchen und in DB Speichern

        SaveGrid()
        LoadFromDatabase()
    End Sub

    Private Sub RadButtonAbbrechen_Click(sender As Object, e As EventArgs) Handles RadButtonAbbrechen.Click
        RadButtonBearbeiten.Visible = True
        RadButtonAbbrechen.Visible = False
        RadButtonSpeichern.Visible = False
        EditMode = False

        RadGridView1.AllowEditRow = False

        LoadFromDatabase()
    End Sub

#End Region

#End Region

End Class