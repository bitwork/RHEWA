' 11.03.2014 hill EichsoftwareAdministrationsClient frmAuswahllisteEichmarkenverwaltung.vb
Imports System
Imports System.IO
Imports Telerik.WinControls.UI

Public Class FrmAuswahllisteEichmarkenverwaltung

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
                RadGridView1.Columns("Firma_FK").IsVisible = False
                RadGridView1.Columns("Firma_FK").VisibleInColumnChooser = False
            Catch e As Exception
            End Try
            Try
                RadGridView1.Columns("FK_BenutzerID").IsVisible = False
                RadGridView1.Columns("FK_BenutzerID").VisibleInColumnChooser = False
            Catch e As Exception
            End Try

            Try
                RadGridView1.Templates(0).Columns("Hauptfirma_FK").IsVisible = False
                RadGridView1.Templates(0).Columns("Hauptfirma_FK").VisibleInColumnChooser = False
                Try
                    RadGridView1.Templates(0).Templates(0).Columns("Hauptfirma_FK").IsVisible = False
                    RadGridView1.Templates(0).Templates(0).Columns("Hauptfirma_FK").VisibleInColumnChooser = False
                Catch
                End Try
            Catch e As Exception
            End Try

            Try
                RadGridView1.Templates(0).Columns("ID").IsVisible = False
                RadGridView1.Templates(0).Columns("ID").VisibleInColumnChooser = False
                Try
                    RadGridView1.Templates(0).Templates(0).Columns("ID").IsVisible = False
                    RadGridView1.Templates(0).Templates(0).Columns("ID").VisibleInColumnChooser = False
                Catch
                End Try
            Catch e As Exception
            End Try

            Try
                RadGridView1.Templates(0).AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None
                RadGridView1.Templates(0).Templates(0).AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None
                RadGridView1.Templates(0).BestFitColumns()
                RadGridView1.Templates(0).Templates(0).BestFitColumns()
            Catch ex As Exception

            End Try

            Try

                'Schreibschutz
                RadGridView1.Columns("ZurBearbeitungGesperrtDurch").ReadOnly = True
                RadGridView1.Columns("HEKennung").ReadOnly = True
                RadGridView1.Columns("Nachname").ReadOnly = True
                RadGridView1.Columns("Vorname").ReadOnly = True

                RadGridView1.Columns("Land").ReadOnly = True
                RadGridView1.Columns("Firma").ReadOnly = True
            Catch e As Exception
            End Try



            Try
                'Eltern Spaltenüberschriften Formatieren
                For Each col In RadGridView1.Columns

                    If col.HeaderText.Equals("HEKennung") Then
                        col.HeaderText = "Kennung"
                    ElseIf col.HeaderText.Equals("BenannteStelleAnzahl") Then
                        col.HeaderText = "  Benannte Stelle  Anzahl gemeldet durch Software"
                    ElseIf col.HeaderText.Equals("BenannteStelleAnzahlMeldestand") Then
                        col.HeaderText = "  Benannte Stelle Meldestand um Rot zu markieren"

                    ElseIf col.HeaderText.Equals("SicherungsmarkeKleinAnzahl") Then
                        col.HeaderText = "  Sicherungsmarke klein Anzahl gemeldet durch Software"
                    ElseIf col.HeaderText.Equals("SicherungsmarkeKleinAnzahlMeldestand") Then
                        col.HeaderText = "  Sicherungsmarke klein Meldestand um Rot zu markieren"

                    ElseIf col.HeaderText.Equals("SicherungsmarkeGrossAnzahl") Then
                        col.HeaderText = "  Sicherungsmarke groß Anzahl gemeldet durch Software"
                    ElseIf col.HeaderText.Equals("SicherungsmarkeGrossAnzahlMeldestand") Then
                        col.HeaderText = "  Sicherungsmarke groß Anzahl um Rot zu markieren"

                    ElseIf col.HeaderText.Equals("HinweismarkeAnzahl") Then
                        col.HeaderText = "  Hinweismarke  Anzahl gemeldet durch Software"
                    ElseIf col.HeaderText.Equals("HinweismarkeAnzahlMeldestand") Then
                        col.HeaderText = "  Hinweismarke  Anzahl um Rot zu markieren"
                    Else
                        FormatColumnHeader(col)
                    End If
                Next
            Catch e As Exception
            End Try
            Try

                'Kinds Spaltenüberschriften Formatieren
                For Each template In RadGridView1.Templates
                    For Each col In template.Columns
                        FormatColumnHeader(col)
                    Next
                Next
            Catch e As Exception
            End Try
            Try
                'Kinds Kinds Spaltenüberschriften Formatieren
                For Each template In RadGridView1.Templates(0).Templates
                    For Each col In template.Columns
                        FormatColumnHeader(col)
                    Next

                Next
            Catch e As Exception
            End Try
        Catch ex As Exception

        End Try

        RadGridView1.BestFitColumns()
    End Sub

    Private Sub FormatColumnHeader(ByVal col As Telerik.WinControls.UI.GridViewColumn)
        If Not col Is Nothing Then
            'Unterstriche in Spaltennamen entfernen
            col.HeaderText = col.HeaderText.Replace("_", " ")
            'Leerzeichen zwischen Buchstaben in CamelCase Schreibweise einfügen
            col.HeaderText = System.Text.RegularExpressions.Regex.Replace(col.HeaderText, "(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1")
            'Umlaute umwandeln
            col.HeaderText = col.HeaderText.Replace("ue", "ü")
            col.HeaderText = col.HeaderText.Replace("ae", "ä")
            col.HeaderText = col.HeaderText.Replace("oe", "ö")
        End If
    End Sub

    Private Sub LoadFromDatabase()
        Try

            SuspendLayout()
            'Eichmarken Grid
            Using Context As New HerstellerersteichungEntities
                'Daten aus eichmarkenverwaltungstabelle und Benutzertabelle zusammenführen. Fürs Databinding Casten in einen neuen Typen
                Dim Data = From Eichmarken In Context.ServerEichmarkenverwaltung
                           Join Benutzer In Context.Benutzer On Eichmarken.FK_BenutzerID Equals Benutzer.ID
                           Join Firma In Context.Firmen On Firma.ID Equals Benutzer.Firma_FK
                           Select New With {
                               Eichmarken.ID,
                               Benutzer.Firma_FK,
                               Eichmarken.ZurBearbeitungGesperrtDurch,
                               Benutzer.Nachname,
                               Benutzer.Vorname,
                               .Firma = Firma.Name,
                               Firma.Land,
                               Eichmarken.HEKennung,
                               Eichmarken.Bemerkung,
                               Eichmarken.BenannteStelleAnzahlAusgeteilt,
                               Eichmarken.BenannteStelleAnzahl,
                               Eichmarken.BenannteStelleFehlmenge,
                             Eichmarken.SicherungsmarkeKleinAnzahlAusgeteilt,
                             Eichmarken.SicherungsmarkeKleinAnzahl,
                             Eichmarken.SicherungsmarkeKleinFehlmenge,
                                   Eichmarken.SicherungsmarkeGrossAnzahlAusgeteilt,
                                   Eichmarken.SicherungsmarkeGrossAnzahl,
                             Eichmarken.SicherungsmarkeGrossFehlmenge,
                            Eichmarken.HinweismarkeAnzahlAusgeteilt,
                            Eichmarken.HinweismarkeAnzahl,
                               Eichmarken.HinweismarkeFehlmenge,
                               Eichmarken.BenannteStelleAnzahlMeldestand,
                             Eichmarken.SicherungsmarkeKleinAnzahlMeldestand,
                             Eichmarken.SicherungsmarkeGrossAnzahlMeldestand,
                               Eichmarken.HinweismarkeAnzahlMeldestand,
                               Eichmarken.FK_BenutzerID
                           }

                'databinding
                RadGridView1.DataSource = Data.ToList
                '##################################################################

                'Kindtabelle Firmendaten

                '##################################################################

                'alle Firmen auslesen

                'Das entityframework machte hier Probleme mit dem doppelten left outer join. deswegen ein umweg.
                'es werden erst alle Firmendaten geladen, dann alle Zuordnungsdaten und dann diese Daten in einer Datatable zusammengeführt. Alles in einem Schritt wäre natürlich schöner gewesen
                Dim Firmen = From firma In Context.Firmen
                             From firmenzusatzdaten In firma.ServerFirmenZusatzdaten.DefaultIfEmpty
                             Select New With {
                                 .id = firma.ID,
                                 firma.Name,
                                 firma.Telefon,
                                 firma.Strasse,
                                 firma.Ort,
                                 firma.PLZ,
                                 firma.Land,
                                 firmenzusatzdaten.Abrechnungsmodell,
                                 firmenzusatzdaten.BeginnVertrag,
                                 firmenzusatzdaten.EndeVertrag,
                                 firmenzusatzdaten.Erstschulung,
                                 firmenzusatzdaten.LetztesAudit,
                                 firmenzusatzdaten.MonatJahrZertifikat,
                                 firmenzusatzdaten.Nachschulung,
                                 firmenzusatzdaten.Qualifizierungspauschale
                             }

                'es gibt DB Seitig eine Abhängigkeit zwischen Firmen und Firmenzusatzdaten. Deswegen wurde hier kein JOIN gewählt. WICHTIG: die Firmen zusatzdaten werden nicht aus dem Context geladen, sondern von "firma". Dies klappt nur weil durch die Beziehung eine Navigation property existiert

                Dim FirmenZuordnung = (From Zuordnung In Context.ServerLookupVertragspartnerFirma).ToList

                Dim dtFirmenKomplett As New DataTable("Firmen")
                dtFirmenKomplett.Columns.Add("ID")
                dtFirmenKomplett.Columns.Add("Name")
                dtFirmenKomplett.Columns.Add("Telefon")
                dtFirmenKomplett.Columns.Add("Strasse")
                dtFirmenKomplett.Columns.Add("Ort")
                dtFirmenKomplett.Columns.Add("PLZ")
                dtFirmenKomplett.Columns.Add("Land")
                dtFirmenKomplett.Columns.Add("Abrechnungsmodell")
                dtFirmenKomplett.Columns.Add("BeginnVertrag")
                dtFirmenKomplett.Columns.Add("EndeVertrag")
                dtFirmenKomplett.Columns.Add("Erstschulung")
                dtFirmenKomplett.Columns.Add("LetztesAudit")
                dtFirmenKomplett.Columns.Add("MonatJahrZertifikat")
                dtFirmenKomplett.Columns.Add("Nachschulung")
                dtFirmenKomplett.Columns.Add("Qualifizierungspauschale")

                dtFirmenKomplett.Columns.Add("Hauptfirma_FK") 'falls die aktuelle Firma eine Vertragsfirma ist, steht hier drin der Verweis zur Hauptfirma

                'umwandeln von entität zu DT. Leider notwendig.
                For Each Firma In Firmen
                    Dim nrow As DataRow = dtFirmenKomplett.NewRow
                    nrow.Item("ID") = Firma.id
                    nrow.Item("Name") = Firma.Name
                    nrow.Item("Telefon") = Firma.Telefon
                    nrow.Item("Strasse") = Firma.Strasse
                    nrow.Item("Ort") = Firma.Ort
                    nrow.Item("PLZ") = Firma.PLZ
                    nrow.Item("Land") = Firma.Land

                    nrow.Item("Abrechnungsmodell") = Firma.Abrechnungsmodell

                    If Firma.BeginnVertrag.HasValue Then nrow.Item("BeginnVertrag") = Firma.BeginnVertrag.Value.ToShortDateString
                    If Firma.EndeVertrag.HasValue Then nrow.Item("EndeVertrag") = Firma.EndeVertrag.Value.ToShortDateString
                    If Firma.Erstschulung.HasValue Then nrow.Item("Erstschulung") = Firma.Erstschulung.Value.ToShortDateString
                    If Firma.LetztesAudit.HasValue Then nrow.Item("LetztesAudit") = Firma.LetztesAudit.Value.ToShortDateString
                    If Firma.Nachschulung.HasValue Then nrow.Item("Nachschulung") = Firma.Nachschulung.Value.ToShortDateString
                    nrow.Item("MonatJahrZertifikat") = Firma.MonatJahrZertifikat
                    nrow.Item("Qualifizierungspauschale") = Firma.Qualifizierungspauschale

                    'Über Firma prüfen ob es eine Hauptfirma gibt
                    If FirmenZuordnung.Contains((From firm In FirmenZuordnung Where firm.Vertragspartner_FK = Firma.id).FirstOrDefault) Then
                        'laut Herrn Strack, kann eine Firma immer nur eine Hauptfirma haben. Eine Hauptfirma aber n Firmen
                        Dim hauptfirmaID = (From firm In FirmenZuordnung Where firm.Vertragspartner_FK = Firma.id Select firm.Firma_FK).FirstOrDefault
                        nrow.Item("Hauptfirma_FK") = hauptfirmaID 'falls die aktuelle Firma eine Vertragsfirma ist, steht hier drin der Verweis zur Hauptfirma

                    End If

                    dtFirmenKomplett.Rows.Add(nrow)
                Next

                '###########################################
                'MAPPING über Benutzer die Firmen Hierarisch zuordnen
                '##########################################

                'Mapping zur Firmentabelle
                Dim SecondLevelTemplateFirmen As New GridViewTemplate()
                SecondLevelTemplateFirmen.DataSource = dtFirmenKomplett.DefaultView
                SecondLevelTemplateFirmen.AllowAddNewRow = False
                SecondLevelTemplateFirmen.AllowColumnChooser = False
                SecondLevelTemplateFirmen.AllowColumnHeaderContextMenu = False
                SecondLevelTemplateFirmen.AllowColumnReorder = False
                SecondLevelTemplateFirmen.AllowDeleteRow = False
                SecondLevelTemplateFirmen.AllowDragToGroup = False
                SecondLevelTemplateFirmen.AllowEditRow = False
                SecondLevelTemplateFirmen.BestFitColumns()

                'Relation aufbauen zwischen Parent und Child über Firma des Benutzers
                RadGridView1.MasterTemplate.Templates.Add(SecondLevelTemplateFirmen)

                Dim relation As New GridViewRelation(RadGridView1.MasterTemplate)
                relation.ChildTemplate = SecondLevelTemplateFirmen
                relation.RelationName = "BenutzerFirma"
                relation.ParentColumnNames.Add("Firma_FK")
                relation.ChildColumnNames.Add("ID")

                RadGridView1.Relations.Add(relation)

                '#########################################################
                ' MAPPING Selbstreferenz des Templates erzeugen um Hauptfirma zum Vertragspartner zuzuordnen
                '#########################################################
                '      Me.RadGridView1.Relations.AddSelfReference(template, "Hauptfirma_FK", "ID")

                Dim dtHauptfirmenView As New DataView(dtFirmenKomplett)
                dtHauptfirmenView.RowFilter = "Hauptfirma_FK is NULL"

                'Mapping zur Firmentabelle
                Dim ThirdLevelTemplateHauptfirmen = New GridViewTemplate()
                ThirdLevelTemplateHauptfirmen.DataSource = dtHauptfirmenView.ToTable
                ThirdLevelTemplateHauptfirmen.AllowAddNewRow = False
                ThirdLevelTemplateHauptfirmen.AllowColumnChooser = False
                ThirdLevelTemplateHauptfirmen.AllowColumnHeaderContextMenu = False
                ThirdLevelTemplateHauptfirmen.AllowColumnReorder = False
                ThirdLevelTemplateHauptfirmen.AllowDeleteRow = False
                ThirdLevelTemplateHauptfirmen.AllowDragToGroup = False
                ThirdLevelTemplateHauptfirmen.AllowEditRow = False
                ThirdLevelTemplateHauptfirmen.BestFitColumns()

                'Relation aufbauen zwischen Parent und Child über Firma und Hauptfirma
                SecondLevelTemplateFirmen.Templates.Add(ThirdLevelTemplateHauptfirmen)

                Dim relation2 As New GridViewRelation(SecondLevelTemplateFirmen)
                relation2.ChildTemplate = ThirdLevelTemplateHauptfirmen
                relation2.RelationName = "FirmaHauptfirma"
                relation2.ParentColumnNames.Add("Hauptfirma_FK")
                relation2.ChildColumnNames.Add("ID")

                RadGridView1.Relations.Add(relation2)

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
                    If row.Cells("ZurBearbeitungGesperrtDurch").Value.Equals(System.Environment.UserName) Then
                        SelectedId = row.Cells("ID").Value
                        Dim objEichmarke As ServerEichmarkenverwaltung = (From Eichmarkenverwaltung In context.ServerEichmarkenverwaltung Where Eichmarkenverwaltung.ID = SelectedId Select Eichmarkenverwaltung).FirstOrDefault

                        If Not row.DataBoundItem Is Nothing Then
                            If Not objEichmarke Is Nothing Then

                                Try
                                    MergeEichmarkenItems(row.DataBoundItem, objEichmarke)
                                    'zurücksetzten des gesperrt flags
                                    objEichmarke.ZurBearbeitungGesperrtDurch = ""
                                    context.SaveChanges()
                                Catch ex As Exception

                                End Try
                            End If
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
    ''' Auf grund von limtierungen des Entity Frameworks und den sogenannten Kontext in dem eine DB Entität geladen wird, muss hier eine 1:1 Wert zuweisung von einem Objekt in ein anderes passieren.
    ''' </summary>
    ''' <param name="SourceObj"></param>
    ''' <param name="TargetObj"></param>
    ''' <remarks></remarks>
    Private Sub MergeEichmarkenItems(ByVal SourceObj As Object, ByVal TargetObj As ServerEichmarkenverwaltung)
        Try
            TargetObj.Bemerkung = SourceObj.Bemerkung

            TargetObj.BenannteStelleAnzahl = SourceObj.BenannteStelleAnzahl
            TargetObj.BenannteStelleAnzahlAusgeteilt = SourceObj.BenannteStelleAnzahlAusgeteilt
            TargetObj.BenannteStelleAnzahlMeldestand = SourceObj.BenannteStelleAnzahlMeldestand
            TargetObj.BenannteStelleFehlmenge = SourceObj.BenannteStelleFehlmenge

            TargetObj.SicherungsmarkeKleinAnzahl = SourceObj.SicherungsmarkeKleinAnzahl
            TargetObj.SicherungsmarkeKleinAnzahlAusgeteilt = SourceObj.SicherungsmarkeKleinAnzahlAusgeteilt
            TargetObj.SicherungsmarkeKleinAnzahlMeldestand = SourceObj.SicherungsmarkeKleinAnzahlMeldestand
            TargetObj.SicherungsmarkeKleinFehlmenge = SourceObj.SicherungsmarkeKleinFehlmenge

            TargetObj.SicherungsmarkeGrossAnzahl = SourceObj.SicherungsmarkeGrossAnzahl
            TargetObj.SicherungsmarkeGrossAnzahlAusgeteilt = SourceObj.SicherungsmarkeGrossAnzahlAusgeteilt
            TargetObj.SicherungsmarkeGrossAnzahlMeldestand = SourceObj.SicherungsmarkeGrossAnzahlMeldestand
            TargetObj.SicherungsmarkeGrossFehlmenge = SourceObj.SicherungsmarkeGrossFehlmenge

            TargetObj.HinweismarkeAnzahl = SourceObj.HinweismarkeAnzahl
            TargetObj.HinweismarkeAnzahlAusgeteilt = SourceObj.HinweismarkeAnzahlAusgeteilt
            TargetObj.HinweismarkeAnzahlMeldestand = SourceObj.HinweismarkeAnzahlMeldestand
            TargetObj.HinweismarkeFehlmenge = SourceObj.HinweismarkeFehlmenge

        Catch ex As Exception

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

        '' laden des Grid Layouts
        'If Debugger.IsAttached = False Then
        '    LadeGridLayout()

        'End If
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


    Private Function GetNeedFill(e As Telerik.WinControls.UI.CellFormattingEventArgs, ByVal ColumnNameAnzahl As String, ByVal ColumnNameAusgeteilte As String, ByVal columnNameMeldestand As String, ByVal ColumnNameFehler As String) As Boolean
        Try
            'Ausgeteilte - (istAnzahl + Fehlanzahl) <= Meldestand      dann rot
            Dim ValueAnzahl As Decimal = CDec(e.Row.Cells(ColumnNameAnzahl).Value)
            Dim ValueAusgeteilt As Decimal = CDec(e.Row.Cells(ColumnNameAusgeteilte).Value)
            Dim ValueMeldestand As Decimal = CDec(e.Row.Cells(columnNameMeldestand).Value)
            Dim ValueFehler As Decimal = CDec(e.Row.Cells(ColumnNameFehler).Value)

            If ValueAusgeteilt - (ValueAnzahl + ValueFehler) <= ValueMeldestand Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return False

    End Function

#Region "Grid Events"
    Private Sub RadGridView1_CellFormatting(sender As Object, e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles RadGridView1.CellFormatting
        Try
            Dim template As GridViewTemplate = e.Row.ViewTemplate

            'Formatierung muss nur für Elterntabelle durchgeführt werden
            If template.Equals(RadGridView1.MasterTemplate) Then

                'Ausgeteilte - (istAnzahl + Fehlanzahl) <= Meldestand      dann rot

                If (e.Column.Name = "BenannteStelleAnzahl") Then
                    If GetNeedFill(e, "BenannteStelleAnzahl", "BenannteStelleAnzahlAusgeteilt", "BenannteStelleAnzahlMeldestand", "BenannteStelleFehlmenge") Then 'e.Row.Cells("BenannteStelleAnzahl").Value <= e.Row.Cells("BenannteStelleAnzahlMeldestand").Value) Then
                        e = fillRed(e)
                    End If
                ElseIf (e.Column.Name = "SicherungsmarkeKleinAnzahl") Then
                    If GetNeedFill(e, "SicherungsmarkeKleinAnzahl", "SicherungsmarkeKleinAnzahlAusgeteilt", "SicherungsmarkeKleinAnzahlMeldestand", "SicherungsmarkeKleinFehlmenge") Then ' And e.Row.Cells("SicherungsmarkeKleinAnzahl").Value <= e.Row.Cells("SicherungsmarkeKleinAnzahlMeldestand").Value) Then
                        e = fillRed(e)
                    End If

                ElseIf (e.Column.Name = "SicherungsmarkeGrossAnzahl") Then
                    If GetNeedFill(e, "SicherungsmarkeGrossAnzahl", "SicherungsmarkeGrossAnzahlAusgeteilt", "SicherungsmarkeGrossAnzahlMeldestand", "SicherungsmarkeGrossFehlmenge") Then ' And e.Row.Cells("SicherungsmarkeGrossAnzahl").Value <= e.Row.Cells("SicherungsmarkeGrossAnzahlMeldestand").Value) Then
                        e = fillRed(e)
                    End If

                ElseIf (e.Column.Name = "HinweismarkeAnzahl") Then
                    If GetNeedFill(e, "HinweismarkeAnzahl", "HinweismarkeAnzahlAusgeteilt", "HinweismarkeAnzahlMeldestand", "HinweismarkeFehlmenge") Then 'And e.Row.Cells("HinweismarkeAnzahl").Value <= e.Row.Cells("HinweismarkeAnzahlMeldestand").Value) Then
                        e = fillRed(e)
                    End If

                Else
                    e = fillWhite(e)
                End If

                'Formatieren der Kind und Kindskind Tabelle
            ElseIf template.Equals(RadGridView1.Templates(0)) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(157, 206, 140)
            ElseIf template.Equals(RadGridView1.Templates(0).Templates(0)) Then
                e.CellElement.DrawFill = True
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                e.CellElement.BackColor = Color.FromArgb(206, 209, 0)

            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Debug.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Shared Function fillWhite(e As CellFormattingEventArgs) As CellFormattingEventArgs
        e.CellElement.DrawFill = False
        e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Linear
        e.CellElement.BackColor = Color.White
        Return e
    End Function

    Private Shared Function fillRed(e As CellFormattingEventArgs) As CellFormattingEventArgs
        e.CellElement.DrawFill = True
        e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
        e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
        Return e
    End Function

    ''' <summary>
    ''' Durch dieses Event wird dafür gesorgt, das nur, sofern auch Kindelemente existieren, der EXPAND Button angezeigt wird. Standardmäßig ist dieser nämlich bei jedem DS. Es hat aber nicht jede Firma eine Hauptfirma
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radGridView1_ViewCellFormatting(sender As Object, e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles RadGridView1.ViewCellFormatting
        Dim cell As GridGroupExpanderCellElement = TryCast(e.CellElement, GridGroupExpanderCellElement)
        If cell IsNot Nothing AndAlso TypeOf e.CellElement.RowElement Is GridDataRowElement Then
            If Not clsTelerikHelper.IsExpandable(cell.RowInfo) Then
                cell.Expander.Visibility = Telerik.WinControls.ElementVisibility.Hidden
            Else
                cell.Expander.Visibility = Telerik.WinControls.ElementVisibility.Visible
            End If
        End If
    End Sub
    ''' <summary>
    ''' Verhindert das Aufklappen einer Childrow, wenn gar kein DS vorliegt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radGridView1_ChildViewExpanding(sender As Object, e As ChildViewExpandingEventArgs) Handles RadGridView1.ChildViewExpanding
        e.Cancel = Not clsTelerikHelper.IsExpandable(e.ParentRow)
    End Sub

    Private Sub RadGridView1_CellBeginEdit(sender As Object, e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles RadGridView1.CellBeginEdit
        Dim SelectedId As String
        SelectedId = e.Row.Cells("ID").Value
        'auf Sperrung prüfen
        Using Context As New HerstellerersteichungEntities

            Dim objEichmarke As ServerEichmarkenverwaltung = (From Eichmarkenverwaltung In Context.ServerEichmarkenverwaltung Where Eichmarkenverwaltung.ID = SelectedId Select Eichmarkenverwaltung).FirstOrDefault
            If objEichmarke Is Nothing Then
                Exit Sub
            End If
            If objEichmarke.ZurBearbeitungGesperrtDurch Is Nothing Then
                'Sperren des DS
                objEichmarke.ZurBearbeitungGesperrtDurch = System.Environment.UserName
                e.Row.Cells("ZurBearbeitungGesperrtDurch").Value = System.Environment.UserName
                Context.SaveChanges()
            Else
                If objEichmarke.ZurBearbeitungGesperrtDurch.Equals("") Then
                    'Sperren des DS
                    objEichmarke.ZurBearbeitungGesperrtDurch = System.Environment.UserName
                    e.Row.Cells("ZurBearbeitungGesperrtDurch").Value = System.Environment.UserName
                    Context.SaveChanges()
                Else
                    e.Row.Cells("ZurBearbeitungGesperrtDurch").Value = objEichmarke.ZurBearbeitungGesperrtDurch

                    'prüfen ob man selbst den DS sperrt
                    If objEichmarke.ZurBearbeitungGesperrtDurch.Equals(System.Environment.UserName) Then
                        e.Cancel = False

                    Else
                        e.Cancel = True
                        Dim alert As New Telerik.WinControls.UI.RadDesktopAlert
                        alert.CaptionText = "Datensatz gesperrt "
                        alert.ContentText = String.Format("Gesperrt druch: {0}", objEichmarke.ZurBearbeitungGesperrtDurch)
                        alert.AutoCloseDelay = 1
                        alert.Show()

                    End If

                End If
            End If

        End Using
    End Sub

    Private Sub RadButtonAddMarkenbestand_Click(sender As Object, e As EventArgs) Handles RadButtonAddMarkenbestand.Click
        For Each row In RadGridView1.MasterTemplate.SelectedRows

            If row.Cells("ZurBearbeitungGesperrtDurch").Value Is Nothing OrElse row.Cells("ZurBearbeitungGesperrtDurch").Value.Equals(System.Environment.UserName) Or row.Cells("ZurBearbeitungGesperrtDurch").Value = "" Then
                Dim SelectedId = row.Cells("ID").Value

                Dim f As New frmEingabeNeuerEichmarkenbestand(SelectedId)
                f.ShowDialog()
                LoadFromDatabase()

                Exit For
            End If
        Next

    End Sub



#End Region

End Class