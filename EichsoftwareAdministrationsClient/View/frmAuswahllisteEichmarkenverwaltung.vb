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
                'die Vorarbeit für das Jahr 2015. Dann können diese Spalten eingeblendet werden
                If Date.Now.Year = 2014 Then
                    RadGridView1.Columns("Archiv2014_Eichungen").IsVisible = False
                    RadGridView1.Columns("Archiv2014_BenannteStelle0103").IsVisible = False
                    RadGridView1.Columns("Archiv2014_BenannteStelle0111").IsVisible = False
                    RadGridView1.Columns("Archiv2014_Hinweismarke").IsVisible = False
                    RadGridView1.Columns("Archiv2014_SicherungsmarkeGross").IsVisible = False
                    RadGridView1.Columns("Archiv2014_SicherungsmarkeKlein").IsVisible = False

                    RadGridView1.Columns("Archiv2014_Eichungen").VisibleInColumnChooser = False
                    RadGridView1.Columns("Archiv2014_BenannteStelle0103").VisibleInColumnChooser = False
                    RadGridView1.Columns("Archiv2014_BenannteStelle0111").VisibleInColumnChooser = False
                    RadGridView1.Columns("Archiv2014_Hinweismarke").VisibleInColumnChooser = False
                    RadGridView1.Columns("Archiv2014_SicherungsmarkeGross").VisibleInColumnChooser = False
                    RadGridView1.Columns("Archiv2014_SicherungsmarkeKlein").VisibleInColumnChooser = False
                End If
            Catch ex As Exception
            End Try

            Try
                'Eltern Spaltenüberschriften Formatieren
                For Each col In RadGridView1.Columns

                    If col.HeaderText.Equals("HEKennung") Then
                        col.HeaderText = "HE-Kennung"
                    ElseIf col.HeaderText.Equals("CEAnzahl") Then
                        col.HeaderText = "CE Anzahl"
                    ElseIf col.HeaderText.Equals("CEAnzahlMeldestand") Then
                        col.HeaderText = "CE Anzahl Meldestand"
                    ElseIf col.HeaderText.Equals("CE2016Anzahl") Then
                        col.HeaderText = "CE 2016 Anzahl"
                    ElseIf col.HeaderText.Equals("CE2016AnzahlMeldestand") Then
                        col.HeaderText = "CE 2016 Anzahl Meldestand"
                    ElseIf col.HeaderText.Equals("Eichsiegel13x13Anzahl") Then
                        col.HeaderText = "  Eichsiegel 13x13 Anzahl"
                    ElseIf col.HeaderText.Equals("Eichsiegel13x13AnzahlMeldestand") Then
                        col.HeaderText = "  Eichsiegel 13x13 Anzahl Meldestand"
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
            Using Context As New EichenEntities
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
                               Eichmarken.BenannteStelleAnzahl,
                               Eichmarken.BenannteStelleAnzahlMeldestand,
                               Eichmarken.CEAnzahl,
                               Eichmarken.CEAnzahlMeldestand,
                                 Eichmarken.CE2016Anzahl,
                               Eichmarken.CE2016AnzahlMeldestand,
                               Eichmarken.Eichsiegel13x13Anzahl,
                               Eichmarken.Eichsiegel13x13AnzahlMeldestand,
                               Eichmarken.EichsiegelRundAnzahl,
                               Eichmarken.EichsiegelRundAnzahlMeldestand,
                               Eichmarken.GruenesMAnzahl,
                               Eichmarken.GruenesMAnzahlMeldestand,
                               Eichmarken.HinweismarkeGelochtAnzahl,
                               Eichmarken.HinweismarkeGelochtAnzahlMeldestand,
                               Eichmarken.FehlmengeBenannteStelle0103,
                               Eichmarken.FehlmengeBenannteStelle0111,
                               Eichmarken.FehlmengeHinweismarken,
                               Eichmarken.FehlmengeSicherungsmarkegross,
                               Eichmarken.FehlmengeSicherungsmarkeklein,
                               Eichmarken.zerstoerteMarke0103,
                               Eichmarken.Archiv2007_BenannteStelle0298,
                               Eichmarken.Archiv2007_Eichungen,
                               Eichmarken.Archiv2007_Hinweismarke,
                               Eichmarken.Archiv2007_SicherungsmarkeGross,
                               Eichmarken.Archiv2007_SicherungsmarkeKlein,
                               Eichmarken.Archiv2008_BenannteStelle0298,
                               Eichmarken.Archiv2008_Eichungen,
                               Eichmarken.Archiv2008_Hinweismarke,
                               Eichmarken.Archiv2008_SicherungsmarkeGross,
                               Eichmarken.Archiv2008_SicherungsmarkeKlein,
                               Eichmarken.Archiv2009_BenannteStelle0298,
                               Eichmarken.Archiv2009_Eichungen,
                               Eichmarken.Archiv2009_Hinweismarke,
                               Eichmarken.Archiv2009_SicherungsmarkeGross,
                               Eichmarken.Archiv2009_SicherungsmarkeKlein,
                               Eichmarken.Archiv2010_BenannteStelle0298,
                               Eichmarken.Archiv2010_Eichungen,
                               Eichmarken.Archiv2010_Hinweismarke,
                               Eichmarken.Archiv2010_SicherungsmarkeGross,
                               Eichmarken.Archiv2010_SicherungsmarkeKlein,
                               Eichmarken.Archiv2011_BenannteStelle0298,
                               Eichmarken.Archiv2011_BenannteStelle0103,
                               Eichmarken.Archiv2011_Eichungen,
                               Eichmarken.Archiv2011_Hinweismarke,
                               Eichmarken.Archiv2011_SicherungsmarkeGross,
                               Eichmarken.Archiv2011_SicherungsmarkeKlein,
                               Eichmarken.Archiv2012_BenannteStelle0103,
                               Eichmarken.Archiv2012_Eichungen,
                               Eichmarken.Archiv2012_Hinweismarke,
                               Eichmarken.Archiv2012_SicherungsmarkeGross,
                               Eichmarken.Archiv2012_SicherungsmarkeKlein,
                               Eichmarken.Archiv2013_BenannteStelle0103,
                               Eichmarken.Archiv2013_Eichungen,
                               Eichmarken.Archiv2013_Hinweismarke,
                               Eichmarken.Archiv2013_SicherungsmarkeGross,
                               Eichmarken.Archiv2013_SicherungsmarkeKlein,
                               Eichmarken.Archiv2014_BenannteStelle0103,
                               Eichmarken.Archiv2014_BenannteStelle0111,
                               Eichmarken.Archiv2014_Eichungen,
                               Eichmarken.Archiv2014_Hinweismarke,
                               Eichmarken.Archiv2014_SicherungsmarkeGross,
                               Eichmarken.Archiv2014_SicherungsmarkeKlein,
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

                    Try
                        nrow.Item("Abrechnungsmodell") = CDate(Firma.Abrechnungsmodell).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try
                    Try
                        nrow.Item("BeginnVertrag") = CDate(Firma.BeginnVertrag).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try
                    Try
                        nrow.Item("EndeVertrag") = CDate(Firma.EndeVertrag).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try
                    Try
                        nrow.Item("Erstschulung") = CDate(Firma.Erstschulung).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try
                    Try
                        nrow.Item("LetztesAudit") = CDate(Firma.LetztesAudit).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try
                    Try
                        nrow.Item("Nachschulung") = CDate(Firma.Nachschulung).Date.ToShortDateString
                    Catch e As Exception
                        Debug.WriteLine(e.Message)
                        Debug.WriteLine(e.StackTrace)
                    End Try

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

    'Private Sub EditEichmarkenverwaltung()
    '    If RadGridView1.SelectedRows.Count > 0 Then
    '        'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
    '        If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
    '            Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
    '            SelectedID = RadGridView1.SelectedRows(0).Cells("ID").Value

    '            'neue Datenbankverbindung
    '            Using context As New EichenEntities
    '                'anzeigen des Dialogs zur Bearbeitung der Eichung
    '                Dim f As New FrmEichmarkenverwaltung(SelectedID)
    '                f.ShowDialog()

    '                'nach dem schließen des Dialogs aktualisieren
    '                LoadFromDatabase()
    '            End Using

    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ''' Iteritert das Grid und speichert alle Änderungen in die Datenbank. Setzt ausserdem den gesperrt Status zurück
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveGrid()
        Try
            Dim SelectedId As String

            Using context As New EichenEntities
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
            Using context As New EichenEntities
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
            TargetObj.BenannteStelleAnzahlMeldestand = SourceObj.BenannteStelleAnzahlMeldestand
            TargetObj.CEAnzahl = SourceObj.CEAnzahl
            TargetObj.CEAnzahlMeldestand = SourceObj.CEAnzahlMeldestand
            TargetObj.CE2016Anzahl = SourceObj.CE2016Anzahl
            TargetObj.CE2016AnzahlMeldestand = SourceObj.CE2016AnzahlMeldestand
            TargetObj.Eichsiegel13x13Anzahl = SourceObj.Eichsiegel13x13Anzahl
            TargetObj.Eichsiegel13x13AnzahlMeldestand = SourceObj.Eichsiegel13x13AnzahlMeldestand
            TargetObj.EichsiegelRundAnzahl = SourceObj.EichsiegelRundAnzahl
            TargetObj.EichsiegelRundAnzahlMeldestand = SourceObj.EichsiegelRundAnzahlMeldestand
            TargetObj.FehlmengeBenannteStelle0103 = SourceObj.FehlmengeBenannteStelle0103
            TargetObj.FehlmengeBenannteStelle0111 = SourceObj.FehlmengeBenannteStelle0111
            TargetObj.FehlmengeHinweismarken = SourceObj.FehlmengeHinweismarken
            TargetObj.FehlmengeSicherungsmarkegross = SourceObj.FehlmengeSicherungsmarkegross
            TargetObj.FehlmengeSicherungsmarkeklein = SourceObj.FehlmengeSicherungsmarkeklein
            TargetObj.GruenesMAnzahl = SourceObj.GruenesMAnzahl
            TargetObj.GruenesMAnzahlMeldestand = SourceObj.GruenesMAnzahlMeldestand
            TargetObj.HinweismarkeGelochtAnzahl = SourceObj.HinweismarkeGelochtAnzahl
            TargetObj.HinweismarkeGelochtAnzahlMeldestand = SourceObj.HinweismarkeGelochtAnzahlMeldestand
            TargetObj.zerstoerteMarke0103 = SourceObj.zerstoerteMarke0103
        Catch ex As Exception

        End Try

        Try
            TargetObj.Archiv2007_BenannteStelle0298 = SourceObj.Archiv2007_BenannteStelle0298
            TargetObj.Archiv2007_Eichungen = SourceObj.Archiv2007_Eichungen
            TargetObj.Archiv2007_Hinweismarke = SourceObj.Archiv2007_Hinweismarke
            TargetObj.Archiv2007_SicherungsmarkeGross = SourceObj.Archiv2007_SicherungsmarkeGross
            TargetObj.Archiv2007_SicherungsmarkeKlein = SourceObj.Archiv2007_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2008_BenannteStelle0298 = SourceObj.Archiv2008_BenannteStelle0298
            TargetObj.Archiv2008_Eichungen = SourceObj.Archiv2008_Eichungen
            TargetObj.Archiv2008_Hinweismarke = SourceObj.Archiv2008_Hinweismarke
            TargetObj.Archiv2008_SicherungsmarkeGross = SourceObj.Archiv2008_SicherungsmarkeGross
            TargetObj.Archiv2008_SicherungsmarkeKlein = SourceObj.Archiv2008_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2009_BenannteStelle0298 = SourceObj.Archiv2009_BenannteStelle0298
            TargetObj.Archiv2009_Eichungen = SourceObj.Archiv2009_Eichungen
            TargetObj.Archiv2009_Hinweismarke = SourceObj.Archiv2009_Hinweismarke
            TargetObj.Archiv2009_SicherungsmarkeGross = SourceObj.Archiv2009_SicherungsmarkeGross
            TargetObj.Archiv2009_SicherungsmarkeKlein = SourceObj.Archiv2009_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2010_BenannteStelle0298 = SourceObj.Archiv2010_BenannteStelle0298
            TargetObj.Archiv2010_Eichungen = SourceObj.Archiv2010_Eichungen
            TargetObj.Archiv2010_Hinweismarke = SourceObj.Archiv2010_Hinweismarke
            TargetObj.Archiv2010_SicherungsmarkeGross = SourceObj.Archiv2010_SicherungsmarkeGross
            TargetObj.Archiv2010_SicherungsmarkeKlein = SourceObj.Archiv2010_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2011_BenannteStelle0298 = SourceObj.Archiv2011_BenannteStelle0298
            TargetObj.Archiv2011_Eichungen = SourceObj.Archiv2011_Eichungen
            TargetObj.Archiv2011_Hinweismarke = SourceObj.Archiv2011_Hinweismarke
            TargetObj.Archiv2011_SicherungsmarkeGross = SourceObj.Archiv2011_SicherungsmarkeGross
            TargetObj.Archiv2011_SicherungsmarkeKlein = SourceObj.Archiv2011_SicherungsmarkeKlein
            TargetObj.Archiv2011_BenannteStelle0103 = SourceObj.Archiv2011_BenannteStelle0103
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2012_BenannteStelle0103 = SourceObj.Archiv2012_BenannteStelle0103
            TargetObj.Archiv2012_Eichungen = SourceObj.Archiv2012_Eichungen
            TargetObj.Archiv2012_Hinweismarke = SourceObj.Archiv2012_Hinweismarke
            TargetObj.Archiv2012_SicherungsmarkeGross = SourceObj.Archiv2012_SicherungsmarkeGross
            TargetObj.Archiv2012_SicherungsmarkeKlein = SourceObj.Archiv2012_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2013_BenannteStelle0103 = SourceObj.Archiv2013_BenannteStelle0103
            TargetObj.Archiv2013_Eichungen = SourceObj.Archiv2013_Eichungen
            TargetObj.Archiv2013_Hinweismarke = SourceObj.Archiv2013_Hinweismarke
            TargetObj.Archiv2013_SicherungsmarkeGross = SourceObj.Archiv2013_SicherungsmarkeGross
            TargetObj.Archiv2013_SicherungsmarkeKlein = SourceObj.Archiv2013_SicherungsmarkeKlein
        Catch e As Exception
        End Try
        Try
            TargetObj.Archiv2014_BenannteStelle0103 = SourceObj.Archiv2014_BenannteStelle0103
            TargetObj.Archiv2014_BenannteStelle0111 = SourceObj.Archiv2014_BenannteStelle0111
            TargetObj.Archiv2014_Eichungen = SourceObj.Archiv2014_Eichungen
            TargetObj.Archiv2014_Hinweismarke = SourceObj.Archiv2014_Hinweismarke
            TargetObj.Archiv2014_SicherungsmarkeGross = SourceObj.Archiv2014_SicherungsmarkeGross
            TargetObj.Archiv2014_SicherungsmarkeKlein = SourceObj.Archiv2014_SicherungsmarkeKlein
        Catch e2 As Exception
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

#Region "Grid Events"
    Private Sub RadGridView1_CellFormatting(sender As Object, e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles RadGridView1.CellFormatting
        Try
            Dim template As GridViewTemplate = e.Row.ViewTemplate

            'Formatierung muss nur für Elterntabelle durchgeführt werden
            If template.Equals(RadGridView1.MasterTemplate) Then

                If (e.Column.Name = "BenannteStelleAnzahl" And e.Row.Cells("BenannteStelleAnzahl").Value <= e.Row.Cells("BenannteStelleAnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)

                ElseIf (e.Column.Name = "Eichsiegel13x13Anzahl" And e.Row.Cells("Eichsiegel13x13Anzahl").Value <= e.Row.Cells("Eichsiegel13x13AnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
                ElseIf (e.Column.Name = "EichsiegelRundAnzahl" And e.Row.Cells("EichsiegelRundAnzahl").Value <= e.Row.Cells("EichsiegelRundAnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
                ElseIf (e.Column.Name = "HinweismarkeGelochtAnzahl" And e.Row.Cells("HinweismarkeGelochtAnzahl").Value <= e.Row.Cells("HinweismarkeGelochtAnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
                ElseIf (e.Column.Name = "GruenesMAnzahl" And e.Row.Cells("GruenesMAnzahl").Value <= e.Row.Cells("GruenesMAnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
                ElseIf (e.Column.Name = "CEAnzahl" And e.Row.Cells("CEAnzahl").Value <= e.Row.Cells("CEAnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
                ElseIf (e.Column.Name = "CE2016Anzahl" And e.Row.Cells("CE2016Anzahl").Value <= e.Row.Cells("CE2016AnzahlMeldestand").Value) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.FromArgb(255, 209, 140)

                ElseIf e.Column.Name.ToLower.Contains("archiv") Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
                    e.CellElement.BackColor = Color.LightGray

                Else
                    e.CellElement.DrawFill = False
                    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Linear
                    e.CellElement.BackColor = Color.White

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
        Using Context As New EichenEntities

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

    'Private Sub RadGridView1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles RadGridView1.CellFormatting
    '    Try
    '        Dim template As GridViewTemplate = e.Row.ViewTemplate

    '        If (e.Column.Name = "Benannte_Stelle_Anzahl" And e.Row.Cells("Benannte_Stelle_Anzahl").Value <= e.Row.Cells("Benannte_Stelle_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Eichsiegel_13x13_Anzahl" And e.Row.Cells("Eichsiegel_13x13_Anzahl").Value <= e.Row.Cells("Eichsiegel_13x13_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Eichsiegel_Rund_Anzahl" And e.Row.Cells("Eichsiegel_Rund_Anzahl").Value <= e.Row.Cells("Eichsiegel_Rund_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Hinweismarke_Gelocht_Anzahl" And e.Row.Cells("Hinweismarke_Gelocht_Anzahl").Value <= e.Row.Cells("Hinweismarke_Gelocht_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "Grünes_M_Anzahl" And e.Row.Cells("Grünes_M_Anzahl").Value <= e.Row.Cells("Grünes_M_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        ElseIf (e.Column.Name = "CE_Anzahl" And e.Row.Cells("CE_Anzahl").Value <= e.Row.Cells("CE_Anzahl_Meldestand").Value) Then
    '            e.CellElement.DrawFill = True
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid
    '            e.CellElement.BackColor = Color.FromArgb(255, 209, 140)
    '        Else
    '            e.CellElement.DrawFill = False
    '            e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Linear
    '            e.CellElement.BackColor = Color.White

    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

#End Region

End Class