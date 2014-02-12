Imports System.Resources

Public Class ucoEichprozessauswahlliste
    Inherits ucoContent
#Region "Member Variables"
    Private WithEvents _ParentForm As FrmMainContainer
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        _ParentForm = Nothing
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
    End Sub

    Sub New(pParentForm As FrmMainContainer)
        MyBase.New(pParentForm, Nothing, Nothing)
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
        Me.RadGridView1.MasterTemplate.AutoExpandGroups = True
        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _ParentForm = pParentForm
    End Sub
#End Region

#Region "Formular Logiken"

    Private Sub FormatTable()
        'ausblenden von internen spalten
        RadGridViewAuswahlliste.Columns("ID").IsVisible = False
        RadGridViewAuswahlliste.Columns("Ausgeblendet").IsVisible = False
        RadGridViewAuswahlliste.Columns("Vorgangsnummer").IsVisible = False

        'übersetzen der Spaltenköpfe
        RadGridViewAuswahlliste.Columns("Lookup_Waegezelle").HeaderText = My.Resources.GlobaleLokalisierung.Waegezelle
        RadGridViewAuswahlliste.Columns("Lookup_Auswertegeraet").HeaderText = My.Resources.GlobaleLokalisierung.AuswerteGeraet
        RadGridViewAuswahlliste.Columns("Lookup_Waagentyp").HeaderText = My.Resources.GlobaleLokalisierung.Waagentyp
        RadGridViewAuswahlliste.Columns("Lookup_Waagenart").HeaderText = My.Resources.GlobaleLokalisierung.Waagenart
        RadGridViewAuswahlliste.Columns("Fabriknummer").HeaderText = My.Resources.GlobaleLokalisierung.Fabriknummer

        'spaltengrößen anpassen (so viel platz wie möglich nehmen)
        RadGridViewAuswahlliste.BestFitColumns()
        RadGridViewAuswahlliste.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        RadGridViewAuswahlliste.BestFitColumns()
        RadGridViewAuswahlliste.EnableAlternatingRowColor = True
        RadGridViewAuswahlliste.ShowNoDataText = True




    End Sub

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)
        'übersetzen und formatierung der Tabelle
        LoadFromDatabase()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoEichprozessauswahlliste))

        Me.RadButtonClientAusblenden.Text = resources.GetString("RadButtonClientAusblenden.Text")
        Me.RadButtonClientBearbeiten.Text = resources.GetString("RadButtonClientBearbeiten.Text")
        Me.RadButtonClientUpdateDatabase.Text = resources.GetString("RadButtonClientUpdateDatabase.Text")
        Me.RadButtonClientNeu.Text = resources.GetString("RadButtonClientNeu.Text")
        Me.RadCheckBoxAusblendenClientGeloeschterDokumente.Text = resources.GetString("RadCheckBoxAusblendenClientGeloeschterDokumente.Text")

        'Hilfetext setzen
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Auswahlliste)
        'Überschrift setzen
        ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Hauptmenue
    End Sub

    Private Sub LoadFromDatabase()
        Me.Enabled = False


        If Not BackgroundWorkerLoadFromDatabase.IsBusy Then
            BackgroundWorkerLoadFromDatabase.RunWorkerAsync()
        End If

        If My.Settings.RHEWALizenz Then
            If Not BackgroundWorkerLoadFromDatabaseRHEWA.IsBusy Then
                BackgroundWorkerLoadFromDatabaseRHEWA.RunWorkerAsync()
            End If
        End If
    End Sub
    Private Sub ForceActivation()
        Try
        

                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob die Lizenz gültig ist
                    Dim name As String = "Tim"
                    Dim Schluessel As String = "Hill"


                    Dim objLic As New Lizensierung
                    objLic.FK_SuperofficeBenutzer = name
                    objLic.Lizenzschluessel = Schluessel

                        objLic.RHEWALizenz = True
                  

                    Try
                        'löschen der lokalen DB
                        For Each lic In DBContext.Lizensierung
                            DBContext.Lizensierung.Remove(lic)
                        Next
                        DBContext.SaveChanges()
                    Catch ex As Exception
                    End Try
                    Try
                        'speichern in lokaler DB
                        DBContext.Lizensierung.Add(objLic)
                        DBContext.SaveChanges()
                    Catch ex As Exception
                    End Try
                    My.Settings.Lizensiert = True
                    My.Settings.RHEWALizenz = objLic.RHEWALizenz
                    My.Settings.Save()

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler)
      
        End Try
    End Sub


    Private Sub ucoEichprozessauswahlliste_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Hauptmenue)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Hauptmenue
            Catch ex As Exception
            End Try
        End If

        RadButtonEinstellungen.Visible = True
        RadButtonEinstellungen.Enabled = True

        'lizenz eingabe überspringen
        If Debugger.IsAttached Then
            ForceActivation()
        End If

        If My.Settings.RHEWALizenz = True Then
        Else
            RadPageView1.Pages.RemoveAt(1)
            RadPageView1.Pages(0).Text = ""
        End If

        'daten füllen
        'für den Fall das die Anwendung gerade erst installiert wurde, oder die einstellung zur Synchronisierung geändert wurde, sollen alle Eichungen vom RHEWA Server geholt werden, die einmal angelegt wurden
        If My.Settings.HoleAlleEigenenEichungenVomServer = True Then
            VerbindeMitWebserviceUndHoleAlles()
            Exit Sub
        End If


        If My.Settings.Lizensiert Then
            LoadFromDatabase()

        End If


    End Sub
#End Region

#Region "Routinen Client"


    Private Sub RadButtonNeu_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientNeu.Click
        CreateNewEichprozess()
    End Sub

    Private Sub CreateNewEichprozess()
        Dim objEichprozess As Eichprozess = Nothing
        Using context As New EichsoftwareClientdatabaseEntities1
            objEichprozess = context.Eichprozess.Create
            'pflichtfelder füllen
            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
            objEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
        End Using

        'anzeigen des Dialogs zur Bearbeitung der Eichung
        Dim f As New FrmMainContainer(objEichprozess)
        f.ShowDialog()

        'nach dem schließen des Dialogs aktualisieren
        LoadFromDatabase()
    End Sub

    Private Sub RadButtonBearbeiten_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientBearbeiten.Click
        EditEichprozess()
    End Sub

    Private Sub RadGridViewAuswahlliste_DoubleClick(sender As Object, e As EventArgs) Handles RadGridViewAuswahlliste.CellDoubleClick
        EditEichprozess()
    End Sub



    Private Sub EditEichprozess()
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der ID des aktuellen Prozesses
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value


                Using Context As New EichsoftwareClientdatabaseEntities1

                    '  Dim obj = webContext.GetEichProzess(SelectedID)




                    Dim objEichprozess = (From Obj In Context.Eichprozess Select Obj Where Obj.ID = SelectedID).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)
                    If Not objEichprozess Is Nothing Then
                        If objEichprozess.FK_Bearbeitungsstatus = 4 Or objEichprozess.FK_Bearbeitungsstatus = 2 Then 'nur wenn neu oder fehlerhaft darf eine Änderung vorgenommen werrden

                            'anzeigen des Dialogs zur Bearbeitung der Eichung
                            Dim f As New FrmMainContainer(objEichprozess)
                            f.ShowDialog()
                            'ElseIf objEichprozess.FK_Bearbeitungsstatus = 2 Then
                            '    Dim f As New FrmMainContainer(objEichprozess, FrmMainContainer.enuDialogModus.korrigierend)
                            '    f.ShowDialog()
                        Else
                            objEichprozess = (From Obj In Context.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select Obj Where Obj.ID = SelectedID).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)

                            objEichprozess.Lookup_Vorgangsstatus = (From f1 In Context.Lookup_Vorgangsstatus Where f1.ID = objEichprozess.FK_Vorgangsstatus Select f1).FirstOrDefault

                            objEichprozess.Lookup_Waagenart = (From f1 In Context.Lookup_Waagenart Where f1.ID = objEichprozess.FK_WaagenArt Select f1).FirstOrDefault
                            objEichprozess.Lookup_Waagentyp = (From f1 In Context.Lookup_Waagentyp Where f1.ID = objEichprozess.FK_WaagenTyp Select f1).FirstOrDefault
                            objEichprozess.Lookup_Bearbeitungsstatus = (From f1 In Context.Lookup_Bearbeitungsstatus Where f1.ID = objEichprozess.FK_Bearbeitungsstatus Select f1).FirstOrDefault
                            objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f1 In Context.Lookup_Konformitaetsbewertungsverfahren Where f1.ID = objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f1).FirstOrDefault

                            Dim f As New FrmMainContainer(objEichprozess, FrmMainContainer.enuDialogModus.lesend)
                            f.ShowDialog()
                        End If
                        'nach dem schließen des Dialogs aktualisieren
                        LoadFromDatabase()
                    End If
                End Using


            End If
        End If
    End Sub
    ''' <summary>
    ''' ausblenden bzw wieder einblenden des aktuellen eichvorgangs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadButtonAusblenden_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientAusblenden.Click
        HideEichprozess()

    End Sub

    Private Sub HideEichprozess()
        'prüfen ob es ausgewählte elemente gibt
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = ""
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value


                Using context As New EichsoftwareClientdatabaseEntities1
                    Dim objEichprozess = (From Obj In context.Eichprozess Select Obj Where Obj.ID = SelectedID).FirstOrDefault 'firstor default um erstes element zurückzugeben
                    If Not objEichprozess Is Nothing Then
                        'umdrehen des ausgeblendet Statuses
                        objEichprozess.Ausgeblendet = Not objEichprozess.Ausgeblendet
                        context.SaveChanges()
                    End If

                    'neu laden der Liste
                    LoadFromDatabase()
                End Using
            End If
        End If

    End Sub
    ''' <summary>
    ''' ein und ausblenden der als gelöscht markierten Elementen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadCheckBoxAusblendenGeloeschterDokumente_ToggleStateChanged(sender As System.Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxAusblendenClientGeloeschterDokumente.ToggleStateChanged
        'laden der benötigten Liste mit nur den benötigten Spalten
        LoadFromDatabase()
    End Sub


    Private Sub BackgroundWorkerLoadFromDatabase_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerLoadFromDatabase.DoWork
        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
            Context.Configuration.LazyLoadingEnabled = True
            'je nach Sprache die Abfrage anpassen um die entsprechenden Übersetzungen der Lookupwerte aus der DB zu laden
            Select Case My.Settings.AktuelleSprache
                Case "de"
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                              Where Eichprozess.Ausgeblendet = RadCheckBoxAusblendenClientGeloeschterDokumente.Checked _
                                                               Select New With _
                                                 { _
                                                         .Status = Eichprozess.Lookup_Vorgangsstatus.Status, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status, _
                                                         Eichprozess.ID, _
                                                         Eichprozess.Vorgangsnummer, _
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                         .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                         .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, _
                                                         .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, _
                                                         .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                         Eichprozess.Ausgeblendet _
                                                 }




                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    e.Result = Data.ToList
                Case "en"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                       Where Eichprozess.Ausgeblendet = RadCheckBoxAusblendenClientGeloeschterDokumente.Checked _
                               Select New With _
                                                { _
                                                              .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN, _
                                                        Eichprozess.ID, _
                                                        Eichprozess.Vorgangsnummer, _
                                                          .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_EN, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    e.Result = Data.ToList
                Case "pl"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                  Where Eichprozess.Ausgeblendet = RadCheckBoxAusblendenClientGeloeschterDokumente.Checked _
                               Select New With _
                                                { _
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_PL, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_PL, _
                                                        Eichprozess.ID, _
                                                        Eichprozess.Vorgangsnummer, _
                                                           .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_PL, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    e.Result = Data.ToList
                Case Else
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                            Where Eichprozess.Ausgeblendet = RadCheckBoxAusblendenClientGeloeschterDokumente.Checked _
                               Select New With _
                                                { _
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN, _
                                                        Eichprozess.ID, Eichprozess.Vorgangsnummer, _
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, .Lookup_Waagentyp_EN = Eichprozess.Lookup_Waagentyp.Typ_EN, .Lookup_Waagentyp_PL = Eichprozess.Lookup_Waagentyp.Typ_PL, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, .Lookup_Waagenart_EN = Eichprozess.Lookup_Waagenart.Art_EN, .Lookup_Waagenart_PL = Eichprozess.Lookup_Waagenart.Art_PL, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    e.Result = Data.ToList
            End Select


        End Using
    End Sub

    Private Sub BackgroundWorkerLoadFromDatabase_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerLoadFromDatabase.RunWorkerCompleted
        'zuweisen der Ergebnismenge als Datenquelle für das Grid
        RadGridViewAuswahlliste.DataSource = e.Result
        'Spalten ein und ausblenden und formatieren
        FormatTable()
        Me.Enabled = True
    End Sub
#End Region

#Region "Routinen Server"
    Private Sub BackgroundWorkerLoadFromDatabaseRHEWA_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerLoadFromDatabaseRHEWA.DoWork
        Try
            'neuen Context aufbauen
            Using WebContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    WebContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using dbcontext As New EichsoftwareClientdatabaseEntities1
                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                    Try

                        e.Result = WebContext.GetAlleEichprozesse(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                    Catch ex As Exception
                    End Try

                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorkerLoadFromDatabaseRHEWA_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerLoadFromDatabaseRHEWA.RunWorkerCompleted
        'zuweisen der Ergebnismenge als Datenquelle für das Grid
        RadGridView1.DataSource = e.Result
        Try
            Try
                'Spalten ein und ausblenden und formatieren
                'ausblenden von internen spalten
                RadGridView1.Columns("ID").IsVisible = False
                RadGridView1.Columns("Vorgangsnummer").IsVisible = False
            Catch ex As Exception
            End Try

            RadGridView1.BestFitColumns()
            RadGridView1.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
            RadGridView1.BestFitColumns()
            RadGridView1.EnableAlternatingRowColor = True
            RadGridView1.ShowNoDataText = True
            Me.Enabled = True
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ShowEichprozess()
        If RadGridView1.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridView1.SelectedRows(0).Cells("Vorgangsnummer").Value

                'neue Datenbankverbindung
                Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                    Try
                        webContext.Open()


                    Catch ex As Exception
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Using dbcontext As New EichsoftwareClientdatabaseEntities1

                        Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                        Dim objClientEichprozess = dbcontext.Eichprozess.Create
                        Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, SelectedID, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


                        If objServerEichprozess Is Nothing Then
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        'umwandeln des Serverobjektes in Clientobject
                        clsServerHelper.CopyObjectPropertiesWithAllLookups(objClientEichprozess, objServerEichprozess)
                        clsServerHelper.GetLookupValuesServer(objClientEichprozess)

                        'anzeigen des Dialogs zur Bearbeitung der Eichung
                        Dim f As New FrmMainContainer(objClientEichprozess, FrmMainContainer.enuDialogModus.lesend)
                        f.ShowDialog()

                        'nach dem schließen des Dialogs aktualisieren
                        LoadFromDatabase()

                    End Using
                End Using

            End If
        End If
    End Sub

    Private Sub ShowClientEichprozess()
        If RadGridViewAuswahlliste.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridViewAuswahlliste.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridViewAuswahlliste.SelectedRows(0).Cells("ID").Value

                'neue Datenbankverbindung

                Using dbcontext As New EichsoftwareClientdatabaseEntities1


                    Dim objClientEichprozess = dbcontext.Eichprozess.Create
                    'anzeigen des Dialogs zur Bearbeitung der Eichung
                    Dim f As New FrmMainContainer(objClientEichprozess, FrmMainContainer.enuDialogModus.lesend)
                    f.ShowDialog()

                    'nach dem schließen des Dialogs aktualisieren
                    LoadFromDatabase()

                End Using
            End If
        End If
    End Sub

    Private Sub RadButtonEichprozessGenehmigen_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessGenehmigenRHEWA.Click
        If RadGridView1.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridView1.SelectedRows(0).Cells("ID").Value
                If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Genehmigen, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    'neue Datenbankverbindung
                    Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                        Try
                            webContext.Open()


                        Catch ex As Exception
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Using dbcontext As New EichsoftwareClientdatabaseEntities1

                            Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                            webContext.SetEichprozessGenehmight(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, SelectedID, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                            'nach dem schließen des Dialogs aktualisieren
                            LoadFromDatabase()

                        End Using
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub RadButtonEichprozessAblehnen_click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessAblehnenRHEWA.Click
        If RadGridView1.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridView1.SelectedRows(0).Cells("ID").Value

                If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Ablehnen, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'neue Datenbankverbindung
                    Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                        Try
                            webContext.Open()


                        Catch ex As Exception
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Using dbcontext As New EichsoftwareClientdatabaseEntities1

                            Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                            webContext.SetEichprozessUngueltig(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, SelectedID, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                            'nach dem schließen des Dialogs aktualisieren
                            LoadFromDatabase()

                        End Using
                    End Using


                End If


            End If
        End If
    End Sub


    Private Sub RadButtonBearbeitenRHEWA_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichungAnsehenRHEWA.Click
        ShowEichprozess()
    End Sub

    Private Sub RadGridView1_DoubleClick(sender As Object, e As EventArgs) Handles RadGridView1.DoubleClick
        ShowEichprozess()
    End Sub

#End Region







    ''' <summary>
    ''' aktivieren und deaktiveren der Schalter zum Genehmigen und Ablehnen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadGridView1_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles RadGridView1.SelectionChanged
        Try
            RadButtonEichprozessAblehnenRHEWA.Enabled = False
            RadButtonEichprozessGenehmigenRHEWA.Enabled = False

            If RadGridView1.SelectedRows.Count > 0 Then
                'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
                If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                    Dim SelectedStatus As String = "" 'Variable zum Speichern des BearbeitungsStatuses des aktuellen Prozesses
                    SelectedStatus = RadGridView1.SelectedRows(0).Cells("Bearbeitungsstatus").Value

                    If SelectedStatus.ToLower = "genehmigt" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    ElseIf SelectedStatus.ToLower = "fehlerhaft" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    ElseIf SelectedStatus.ToLower = "wartet auf bearbeitung" Then
                        RadButtonEichprozessAblehnenRHEWA.Enabled = True
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = True
                    Else
                        RadButtonEichprozessAblehnenRHEWA.Enabled = False
                        RadButtonEichprozessGenehmigenRHEWA.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            RadButtonEichprozessAblehnenRHEWA.Enabled = False
            RadButtonEichprozessGenehmigenRHEWA.Enabled = False
        End Try
    End Sub
    Private Sub GetLokaleKopieVonEichprozess()
        If RadGridView1.SelectedRows.Count > 0 Then
            'prüfen ob das ausgewählte element eine REcord Row und kein Groupheader, Filter oder anderes ist
            If TypeOf RadGridView1.SelectedRows(0) Is Telerik.WinControls.UI.GridViewDataRowInfo Then
                Dim SelectedID As String = "" 'Variable zum Speichern der Vorgangsnummer des aktuellen Prozesses
                SelectedID = RadGridView1.SelectedRows(0).Cells("Vorgangsnummer").Value

                If MessageBox.Show(My.Resources.GlobaleLokalisierung.Frage_Kopieren, My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'neue Datenbankverbindung
                    Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                        Try
                            webContext.Open()


                        Catch ex As Exception
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Using dbcontext As New EichsoftwareClientdatabaseEntities1
                            Try


                                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                                Dim objClientEichprozess = dbcontext.Eichprozess.Create
                                Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, SelectedID, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


                                If objServerEichprozess Is Nothing Then
                                    MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If

                                'umwandeln des Serverobjektes in Clientobject
                                clsServerHelper.CopyObjectPropertiesWithNewIDs(objClientEichprozess, objServerEichprozess)

                                'vorgangsnummer editieren
                                objClientEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
                                objClientEichprozess.FK_Bearbeitungsstatus = 4 'noch nichts
                                objClientEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
                                dbcontext.Eichprozess.Add(objClientEichprozess)

                                Try
                                    dbcontext.SaveChanges()
                                Catch ex As Entity.Infrastructure.DbUpdateException
                                    messagebox.show(ex.InnerException.InnerException.Message)
                                    MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalie, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK)
                                    If My.Settings.RHEWALizenz Then
                                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalieRhewaZusatztext, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK)
                                    End If

                                    Exit Sub
                                Catch ex2 As Entity.Validation.DbEntityValidationException
                                    For Each o In ex2.EntityValidationErrors
                                        For Each v In o.ValidationErrors
                                            messagebox.show(v.ErrorMessage & " " & v.PropertyName)
                                        Next
                                    Next
                                    Exit Sub
                                End Try


                                'anzeigen des Dialogs zur Bearbeitung der Eichung
                                Dim f As New FrmMainContainer(objClientEichprozess)
                                f.ShowDialog()

                                'nach dem schließen des Dialogs aktualisieren
                                LoadFromDatabase()

                            Catch ex As Exception

                            End Try
                        End Using
                    End Using


                End If


            End If
        End If
    End Sub
    Private Sub RadButtonEichprozessKopieren_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonEichprozessKopierenRHEWA.Click
        GetLokaleKopieVonEichprozess()
    End Sub

#Region "Updates aus Webservice"

    Private Sub VerbindeMitWebserviceUndHoleAlles()
        Try
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                webContext.Open()
                webContext.Close()
            End Using
        Catch ex As Exception

            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        'für den Fall das die Anwendung gerade erst installiert wurde, oder die einstellung zur Synchronisierung geändert wurde, sollen alle Eichungen vom RHEWA Server geholt werden, die einmal angelegt wurden

        LoescheLokaleDatenbank()
        My.Settings.LetztesUpdate = "01.01.2000"
        My.Settings.Save()
        'hole alle WZ
        GetNeueWZ(False)
        'prüfen ob es neue AWG gibt
        GetNeuesAWG(False)

        My.Settings.LetztesUpdate = Date.Now
        My.Settings.Save()


        GetEichprotokolleVomServer()
        My.Settings.HoleAlleEigenenEichungenVomServer = False
        My.Settings.Save()

      

    End Sub
    ''' <summary>
    ''' Methode welche sich mit dem Webservice verbinduet und nach aktualisierungen für WZ, AWGs und eigenen Eichungen guckt
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VerbindeMitWebServiceUndAktualisiere()
        Try
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                webContext.Open()
                webContext.Close()
            End Using
        Catch ex As Exception

            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Exit Sub
        End Try

        'variablen zur Ausgabe ob es änderungen gibt:
        Dim bolNeuWZ As Boolean = False
        Dim bolNeuAWG As Boolean = False
        Dim bolNeuGenehmigung As Boolean = False



        'prüfen ob es neue WZ gibt
        GetNeueWZ(bolNeuWZ)
        'prüfen ob es neue AWG gibt
        GetNeuesAWG(bolNeuAWG)

        My.Settings.LetztesUpdate = Date.Now
        My.Settings.Save()


        'prüfen ob Eichprozesse die versendet wurden genehmigt oder abgelehnt wurden
        GetGenehmigungsstatus(bolNeuGenehmigung)


        Dim returnMessage As String = My.Resources.GlobaleLokalisierung.Aktualisierung_Erfolgreich
        If bolNeuWZ Then
            returnMessage += "/ " + My.Resources.GlobaleLokalisierung.Aktualisierung_NeuWZ
        End If
        If bolNeuAWG Then
            returnMessage += "/ " + My.Resources.GlobaleLokalisierung.Aktualisierung_NeuAWG
        End If

        If bolNeuGenehmigung Then
            returnMessage += "/ " + My.Resources.GlobaleLokalisierung.Aktualisierung_NeuEichung
        End If
        MessageBox.Show(returnMessage)
    End Sub

    'löscht lokale Datenbank, für resyncronisierung
    Private Sub LoescheLokaleDatenbank()
        Try
            Using DBContext As New EichsoftwareClientdatabaseEntities1
                Dim Name As String
                'Dim Schluessel As String

                ''lizenzisierung holen
                'Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault

                'Name = objLiz.FK_SuperofficeBenutzer
                'Schluessel = objLiz.Lizenzschluessel

                For Each obj In DBContext.Eichprozess
                    DBContext.Eichprozess.Remove(obj)
                Next
                For Each obj In DBContext.Eichprotokoll
                    DBContext.Eichprotokoll.Remove(obj)
                Next
                For Each obj In DBContext.Beschaffenheitspruefung
                    DBContext.Beschaffenheitspruefung.Remove(obj)
                Next
                For Each obj In DBContext.Kompatiblitaetsnachweis
                    DBContext.Kompatiblitaetsnachweis.Remove(obj)
                Next
                For Each obj In DBContext.PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next
                For Each obj In DBContext.PruefungEichfehlergrenzen
                    DBContext.PruefungEichfehlergrenzen.Remove(obj)
                Next
                For Each obj In DBContext.PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next
                For Each obj In DBContext.PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next
                For Each obj In DBContext.PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next
                For Each obj In DBContext.PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next
                For Each obj In DBContext.PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next

                'DBContext.Database.Delete()
                'DBContext.Database.CreateIfNotExists()

                'Dim objLic As New Lizensierung
                'objLic.FK_SuperofficeBenutzer = Name
                'objLic.Lizenzschluessel = Schluessel
                'objLic.Aktiv = True
                'objLic.RHEWALizenz = My.Settings.RHEWALizenz

                'DBContext.Lizensierung.Add(objLic)
                DBContext.SaveChanges()

            End Using
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'holt alle einem zugehörigen Eichprotokolle vom RHEWA Server. z.B. wenn die anwendung auf einem neuem PC installiert wurde
    Private Sub GetEichprotokolleVomServer()
        Try

            'abrufen des Statusts für jeden versendeten Eichprozess
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'Eingrenzen welche Daten synchronisiert werden müssen, je nach Einstllung des Benutzers 
                    Dim StartDatum As Date = #1/1/2000#
                    Dim EndDatum As Date = #12/31/2999#

                    If My.Settings.Syncronisierungsmodus = "Alles" Then
                    ElseIf My.Settings.Syncronisierungsmodus = "Ab" Then
                        StartDatum = My.Settings.SyncAb
                    ElseIf My.Settings.Syncronisierungsmodus = "Zwischen" Then
                        StartDatum = My.Settings.SyncAb
                        EndDatum = My.Settings.SyncBis
                    Else
                        My.Settings.Syncronisierungsmodus = "Alles"
                        My.Settings.Save()
                    End If

                    'lizenz objekt
                    Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault

                    Try
                        'wenn es eine Änderung gab, wird das geänderte Objekt vom Server abgerufen. Damit können änderungen die von einem RHEWA Mitarbeiter durchgeführt wurden übernommen werden
                        'neue Datenbankverbindung
                        Dim objServerEichprozesse = webContext.GetAlleEichprozesseImZeitraum(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)


                        If objServerEichprozesse Is Nothing Then
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        For Each objectServerEichprozess In objServerEichprozesse
                            'umwandeln des Serverobjektes in Clientobject
                            Dim Eichprozess = DBContext.Eichprozess.Create

                            'erzeuge lokale Kopie
                            clsServerHelper.CopyObjectPropertiesWithNewIDs(Eichprozess, objectServerEichprozess, True)
                            Try
                                DBContext.Eichprozess.Add(Eichprozess)
                                DBContext.SaveChanges()
                            Catch ex As Entity.Infrastructure.DbUpdateException
                                MessageBox.Show(ex.InnerException.InnerException.Message)
                            Catch ex2 As Entity.Validation.DbEntityValidationException
                                For Each o In ex2.EntityValidationErrors
                                    For Each v In o.ValidationErrors
                                        MessageBox.Show(v.ErrorMessage & " " & v.PropertyName)
                                    Next
                                Next
                            End Try


                        Next

                        'aktualisieren des Grids
                        LoadFromDatabase()
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_Speichern, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' holt aktuellen Status eigenerer Eichungenen (z.b. Abgelehnt oder Erfolgreich) aus DB über Webservice
    ''' </summary>
    ''' <param name="bolNeuGenehmigung">Variable welche für eine Erfolgsmeldung genutzt wird. Wird beim aktualisieren auf True gesetzt um den Nutzer darauf hinzuweisen, dass etwas neues heruntergeladen wurde.</param>
    ''' <remarks></remarks>
    Private Sub GetGenehmigungsstatus(ByRef bolNeuGenehmigung As Boolean)
        Try

            'abrufen des Statusts für jeden versendeten Eichprozess
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault

                    'hole die prozesse mit dem status 1 = in bearbeitung bei rhewa
                    Dim query = From db In DBContext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select db Where db.FK_Bearbeitungsstatus = 1

                    For Each Eichprozess In query
                        Try
                            Dim NeuerStatus As String = webContext.CheckGueltigkeitEichprozess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, Eichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                            If Not NeuerStatus Is Nothing Then
                                If Eichprozess.FK_Bearbeitungsstatus <> NeuerStatus Then

                                    'wenn es eine Änderung gab, wird das geänderte Objekt vom Server abgerufen. Damit können änderungen die von einem RHEWA Mitarbeiter durchgeführt wurden übernommen werden
                                    'todo abrufen des neuen Objektess

                                    '###################
                                    'neue Datenbankverbindung

                                    Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, Eichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


                                    If objServerEichprozess Is Nothing Then
                                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End If

                                    'umwandeln des Serverobjektes in Clientobject
                                    clsServerHelper.CopyObjectPropertiesWithOwnIDs(Eichprozess, objServerEichprozess)

                                    Eichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'überschreiben des Statuses
                                    Eichprozess.FK_Bearbeitungsstatus = NeuerStatus
                                    Try
                                        DBContext.SaveChanges()
                                        bolNeuGenehmigung = True
                                    Catch ex As Entity.Infrastructure.DbUpdateException
                                        messagebox.show(ex.InnerException.InnerException.Message)
                                    Catch ex2 As Entity.Validation.DbEntityValidationException
                                        For Each o In ex2.EntityValidationErrors
                                            For Each v In o.ValidationErrors
                                                messagebox.show(v.ErrorMessage & " " & v.PropertyName)
                                            Next
                                        Next
                                    End Try

                                    clsServerHelper.UpdateForeignTables(Eichprozess, objServerEichprozess)

                                    LoadFromDatabase()

                                    '###################



                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_Speichern, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' holt neue oder aktualisierte WZ aus DB über Webservice
    ''' </summary>
    ''' <param name="bolNeuWZ">Variable welche für eine Erfolgsmeldung genutzt wird. Wird beim aktualisieren auf True gesetzt um den Nutzer darauf hinzuweisen, dass etwas neues heruntergeladen wurde.</param>
    ''' <remarks></remarks>
    Private Sub GetNeueWZ(ByRef bolNeuWZ As Boolean)
        Try


            'abrufen neuer WZ aus Server, basierend auf dem Wert des letzten erfolgreichen updates
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1

                    'Eingrenzen welche Daten synchronisiert werden müssen, je nach Einstllung des Benutzers 
                    Dim StartDatum As Date = #1/1/2000#
                    Dim EndDatum As Date = #12/31/2999#

                    If My.Settings.Syncronisierungsmodus = "Alles" Then
                    ElseIf My.Settings.Syncronisierungsmodus = "Ab" Then
                        StartDatum = My.Settings.SyncAb
                    ElseIf My.Settings.Syncronisierungsmodus = "Zwischen" Then
                        StartDatum = My.Settings.SyncAb
                        EndDatum = My.Settings.SyncBis
                    Else
                        My.Settings.Syncronisierungsmodus = "Alles"
                        My.Settings.Save()
                    End If

                    'lizenzisierung holen
                    Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault
                    Dim objWZResultList = webContext.GetNeueWZ(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, My.Settings.LetztesUpdate, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)

                    If Not objWZResultList Is Nothing Then

                        'alle neuen Artikel aus Server iterieren
                        Dim tObjServerWZ As EichsoftwareWebservice.ServerLookup_Waegezelle 'hilfsvariable für Linq abfrage. Es gibt sonst eine Warnung wenn in Linq mit einer for each variablen gearbeitet wird
                        For Each objServerArtikel As EichsoftwareWebservice.ServerLookup_Waegezelle In objWZResultList
                            tObjServerWZ = objServerArtikel
                            'alle Artikel abrufen in denen die ID mit dem neuem Serverartikel übereinstimmt
                            Try
                                Dim query = From d In DBContext.Lookup_Waegezelle Where d.ID = tObjServerWZ._ID


                                'prüfen ob es bereits einen Artikel in der lokalen DB gibt, mit dem aktuellen ID-Wert
                                If query.Count = 0 Then 'Es gbit den Artikel noch nicht in der lokalen Datebank => insert 
                                    Dim newWZ As New Lookup_Waegezelle


                                    newWZ.ID = objServerArtikel._ID
                                    newWZ.Hoechsteteilungsfaktor = objServerArtikel._Hoechsteteilungsfaktor
                                    newWZ.Kriechteilungsfaktor = objServerArtikel._Kriechteilungsfaktor
                                    newWZ.MaxAnzahlTeilungswerte = objServerArtikel._MaxAnzahlTeilungswerte
                                    newWZ.Mindestvorlast = objServerArtikel._Mindestvorlast
                                    newWZ.MinTeilungswert = objServerArtikel._MinTeilungswert
                                    newWZ.RueckkehrVorlastsignal = objServerArtikel._RueckkehrVorlastsignal
                                    newWZ.Waegezellenkennwert = objServerArtikel._Waegezellenkennwert
                                    newWZ.WiderstandWaegezelle = objServerArtikel._WiderstandWaegezelle
                                    newWZ.Bauartzulassung = objServerArtikel._Bauartzulassung
                                    newWZ.BruchteilEichfehlergrenze = objServerArtikel._BruchteilEichfehlergrenze
                                    newWZ.Genauigkeitsklasse = objServerArtikel._Genauigkeitsklasse
                                    newWZ.GrenzwertTemperaturbereichMAX = objServerArtikel._GrenzwertTemperaturbereichMAX
                                    newWZ.GrenzwertTemperaturbereichMIN = objServerArtikel._GrenzwertTemperaturbereichMIN
                                    newWZ.Hersteller = objServerArtikel._Hersteller
                                    newWZ.Pruefbericht = objServerArtikel._Pruefbericht
                                    newWZ.Typ = objServerArtikel._Typ
                                    newWZ.deaktiviert = objServerArtikel._Deaktiviert
                                    'hinzufügen des neu erzeugten Artikels in Lokale Datenbank

                                    DBContext.Lookup_Waegezelle.Add(newWZ)
                                    Try
                                        DBContext.SaveChanges()
                                        bolNeuWZ = True
                                    Catch e As Exception
                                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_Speichern, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        MessageBox.Show(e.StackTrace, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End Try

                                Else 'Es gibt den Artikel bereits, er wird geupdated
                                For Each objWZ As Lookup_Waegezelle In query 'es sollte nur einen Artikel Geben, da die IDs eindeutig sind.
                                        objWZ.Hoechsteteilungsfaktor = objServerArtikel._Hoechsteteilungsfaktor
                                        objWZ.Kriechteilungsfaktor = objServerArtikel._Kriechteilungsfaktor
                                        objWZ.MaxAnzahlTeilungswerte = objServerArtikel._MaxAnzahlTeilungswerte
                                        objWZ.Mindestvorlast = objServerArtikel._Mindestvorlast
                                        objWZ.MinTeilungswert = objServerArtikel._MinTeilungswert
                                        objWZ.RueckkehrVorlastsignal = objServerArtikel._RueckkehrVorlastsignal
                                        objWZ.Waegezellenkennwert = objServerArtikel._Waegezellenkennwert
                                        objWZ.WiderstandWaegezelle = objServerArtikel._WiderstandWaegezelle
                                        objWZ.Bauartzulassung = objServerArtikel._Bauartzulassung
                                        objWZ.BruchteilEichfehlergrenze = objServerArtikel._BruchteilEichfehlergrenze
                                        objWZ.Genauigkeitsklasse = objServerArtikel._Genauigkeitsklasse
                                        objWZ.GrenzwertTemperaturbereichMAX = objServerArtikel._GrenzwertTemperaturbereichMAX
                                        objWZ.GrenzwertTemperaturbereichMIN = objServerArtikel._GrenzwertTemperaturbereichMIN
                                        objWZ.Hersteller = objServerArtikel._Hersteller
                                        objWZ.Pruefbericht = objServerArtikel._Pruefbericht
                                        objWZ.Typ = objServerArtikel._Typ
                                        objWZ.deaktiviert = objServerArtikel._Deaktiviert
                                    Next
                                End If
                            Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
                        Next

                    End If
                    Try
                        DBContext.SaveChanges()
                    Catch ex As Exception

                        MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' holt neue oder aktualisierte AWGs aus DB über Webservice
    ''' </summary>
    ''' <param name="bolNeuAWG">Variable welche für eine Erfolgsmeldung genutzt wird. Wird beim aktualisieren auf True gesetzt um den Nutzer darauf hinzuweisen, dass etwas neues heruntergeladen wurde.</param>
    ''' <remarks></remarks>
    Private Sub GetNeuesAWG(ByRef bolNeuAWG As Boolean)
        Try

            'abrufen neuer WZ aus Server, basierend auf dem Wert des letzten erfolgreichen updates
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'Eingrenzen welche Daten synchronisiert werden müssen, je nach Einstllung des Benutzers 
                    Dim StartDatum As Date = #1/1/2000#
                    Dim EndDatum As Date = #12/31/2999#

                    If My.Settings.Syncronisierungsmodus = "Alles" Then
                    ElseIf My.Settings.Syncronisierungsmodus = "Ab" Then
                        StartDatum = My.Settings.SyncAb
                    ElseIf My.Settings.Syncronisierungsmodus = "Zwischen" Then
                        StartDatum = My.Settings.SyncAb
                        EndDatum = My.Settings.SyncBis
                    Else
                        My.Settings.Syncronisierungsmodus = "Alles"
                        My.Settings.Save()
                    End If

                    'lizenzisierung holen

                    Dim objLiz = (From db In DBContext.Lizensierung Select db).FirstOrDefault
                    Dim objAWGResultList = webContext.GetNeuesAWG(objLiz.FK_SuperofficeBenutzer, objLiz.Lizenzschluessel, My.Settings.LetztesUpdate, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)

                    If Not objAWGResultList Is Nothing Then

                        'alle neuen Artikel aus Server iterieren
                        Dim tObjServerAWG As EichsoftwareWebservice.ServerLookup_Auswertegeraet 'hilfsvariable für Linq abfrage. Es gibt sonst eine Warnung wenn in Linq mit einer for each variablen gearbeitet wird
                        For Each objServerArtikel As EichsoftwareWebservice.ServerLookup_Auswertegeraet In objAWGResultList
                            tObjServerAWG = objServerArtikel
                            'alle Artikel abrufen in denen die ID mit dem neuem Serverartikel übereinstimmt
                            Dim query = From d In DBContext.Lookup_Auswertegeraet Where d.ID = tObjServerAWG._ID

                            'prüfen ob es bereits einen Artikel in der lokalen DB gibt, mit dem aktuellen ID-Wert
                            If query.Count = 0 Then 'Es gbit den Artikel noch nicht in der lokalen Datebank => insert 
                                Dim newAWG As New Lookup_Auswertegeraet


                                newAWG.ID = objServerArtikel._ID
                                newAWG.Bauartzulassung = objServerArtikel._Bauartzulassung
                                newAWG.BruchteilEichfehlergrenze = objServerArtikel._BruchteilEichfehlergrenze
                                newAWG.Genauigkeitsklasse = objServerArtikel._Genauigkeitsklasse
                                newAWG.GrenzwertLastwiderstandMAX = objServerArtikel._GrenzwertLastwiderstandMAX
                                newAWG.GrenzwertLastwiderstandMIN = objServerArtikel._GrenzwertLastwiderstandMIN
                                newAWG.GrenzwertTemperaturbereichMAX = objServerArtikel._GrenzwertTemperaturbereichMAX
                                newAWG.GrenzwertTemperaturbereichMIN = objServerArtikel._GrenzwertTemperaturbereichMIN
                                newAWG.Hersteller = objServerArtikel._Hersteller
                                newAWG.KabellaengeQuerschnitt = objServerArtikel._KabellaengeQuerschnitt
                                newAWG.MAXAnzahlTeilungswerteEinbereichswaage = objServerArtikel._MAXAnzahlTeilungswerteEinbereichswaage
                                newAWG.MAXAnzahlTeilungswerteMehrbereichswaage = objServerArtikel._MAXAnzahlTeilungswerteMehrbereichswaage
                                newAWG.Mindesteingangsspannung = objServerArtikel._Mindesteingangsspannung
                                newAWG.Mindestmesssignal = objServerArtikel._Mindestmesssignal
                                newAWG.Pruefbericht = objServerArtikel._Pruefbericht
                                newAWG.Speisespannung = objServerArtikel._Speisespannung
                                newAWG.Typ = objServerArtikel._Typ
                                'hinzufügen des neu erzeugten Artikels in Lokale Datenbank
                                newAWG.deaktiviert = objServerArtikel._Deaktiviert
                                DBContext.Lookup_Auswertegeraet.Add(newAWG)
                                DBContext.SaveChanges()
                                bolNeuAWG = True


                            Else 'Es gibt den Artikel bereits, er wird geupdated
                                For Each objAWG As Lookup_Auswertegeraet In query 'es sollte nur einen Artikel Geben, da die IDs eindeutig sind.
                                 
                                        objAWG.Bauartzulassung = objServerArtikel._Bauartzulassung
                                        objAWG.BruchteilEichfehlergrenze = objServerArtikel._BruchteilEichfehlergrenze
                                        objAWG.Genauigkeitsklasse = objServerArtikel._Genauigkeitsklasse
                                        objAWG.GrenzwertLastwiderstandMAX = objServerArtikel._GrenzwertLastwiderstandMAX
                                        objAWG.GrenzwertLastwiderstandMIN = objServerArtikel._GrenzwertLastwiderstandMIN
                                        objAWG.GrenzwertTemperaturbereichMAX = objServerArtikel._GrenzwertTemperaturbereichMAX
                                        objAWG.GrenzwertTemperaturbereichMIN = objServerArtikel._GrenzwertTemperaturbereichMIN
                                        objAWG.Hersteller = objServerArtikel._Hersteller
                                        objAWG.KabellaengeQuerschnitt = objServerArtikel._KabellaengeQuerschnitt
                                        objAWG.MAXAnzahlTeilungswerteEinbereichswaage = objServerArtikel._MAXAnzahlTeilungswerteEinbereichswaage
                                        objAWG.MAXAnzahlTeilungswerteMehrbereichswaage = objServerArtikel._MAXAnzahlTeilungswerteMehrbereichswaage
                                        objAWG.Mindesteingangsspannung = objServerArtikel._Mindesteingangsspannung
                                        objAWG.Mindestmesssignal = objServerArtikel._Mindestmesssignal
                                        objAWG.Pruefbericht = objServerArtikel._Pruefbericht
                                        objAWG.Speisespannung = objServerArtikel._Speisespannung
                                    objAWG.Typ = objServerArtikel._Typ
                                    objAWG.Deaktiviert = objServerArtikel._Deaktiviert
                                    bolNeuAWG = True
                                Next
                            End If
                        Next

                    End If
                    Try
                        DBContext.SaveChanges()
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_Laden, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RadButtonClientUpdateDatabase_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonClientUpdateDatabase.Click
        VerbindeMitWebServiceUndAktualisiere()
    End Sub
#End Region
   
    Private Sub RadButtonEinstellungen_Click(sender As Object, e As EventArgs) Handles RadButtonEinstellungen.Click
        Dim f As New frmEinstellungen
        f.showdialog()
        If f.dialogresult = DialogResult.OK Then

        End If
    End Sub
End Class
