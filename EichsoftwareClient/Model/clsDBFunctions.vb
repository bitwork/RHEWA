Imports System.IO
Imports System.Runtime.Serialization
Imports EichsoftwareClient

Public Class clsDBFunctions

    ' ''' <summary>
    ' ''' DEBUG Funktion um Lizenzdialog aus Testzwecken zu überspringen
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Shared Sub ForceActivation()
    '    Try
    '        Using DBContext As New Entities
    '            'prüfen ob die Lizenz gültig ist
    '            Dim HEKennung As String = "tim"
    '            Dim Schluessel As String = "Hill"

    '            Dim objLic As New Lizensierung
    '            objLic.HEKennung = HEKennung
    '            objLic.Lizenzschluessel = Schluessel

    '            objLic.RHEWALizenz = True

    '            Try
    '                'löschen der lokalen DB
    '                For Each lic In DBContext.Lizensierung
    '                    DBContext.Lizensierung.Remove(lic)
    '                Next
    '                DBContext.SaveChanges()
    '            Catch ex As Exception
    '            End Try
    '            Try
    '                'speichern in lokaler DB
    '                DBContext.Lizensierung.Add(objLic)
    '                DBContext.SaveChanges()
    '            Catch ex As Exception
    '            End Try

    '            My.Settings.LetzterBenutzer = objLic.Lizenzschluessel
    '            My.Settings.Save()
    '            AktuellerBenutzer.Instance.Lizenz.RHEWALizenz = objLic.RHEWALizenz

    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler)

    '    End Try
    'End Sub

    Public Shared Function LoescheLokaleBenutzer() As Boolean
        'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
        Using DBContext As New Entities
            For Each o In DBContext.Lizensierung.ToList
                DBContext.Lizensierung.Remove(o)

            Next
            For Each o In DBContext.Konfiguration.ToList
                DBContext.Konfiguration.Remove(o)
            Next
            DBContext.SaveChanges()
        End Using
    End Function

    Public Shared Function UpdateClientDatenbank()
        Try


            Using DBContext As New Entities
                Dim config = DBContext.Konfiguration.First
                If Not config.DBVersion = 2 Then

                    Dim updateScript As String = ""
                    updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Aufstellungsort] nvarchar(4000) NULL"
                    DBContext.Database.ExecuteSqlCommand(updateScript)

                    updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Pruefer] nvarchar(4000) NULL"
                    DBContext.Database.ExecuteSqlCommand(updateScript)

                    updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Benutzer] nvarchar(4000) NULL"
                    DBContext.Database.ExecuteSqlCommand(updateScript)

                    updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Beschaffenheitspruefung_Pruefscheinnummer] nvarchar(4000) NULL"

                    DBContext.Database.ExecuteSqlCommand(updateScript)

                    Dim Konfigs = DBContext.Konfiguration
                    For Each Konfig In Konfigs
                        Konfig.DBVersion = 2
                    Next

                    DBContext.SaveChanges()
                End If
            End Using

        Catch ex As Entity.Core.EntityCommandExecutionException
            Using DBContext As New Entities
                'Spalte DB Version existiert noch nicht
                Dim updateScript As String = ""
                updateScript = "ALTER TABLE [Konfiguration] ADD [DBVersion] nvarchar(25) DEFAULT '1' NULL"
                DBContext.Database.ExecuteSqlCommand(updateScript)
                UpdateClientDatenbank()
            End Using
        End Try



    End Function

    ''' <summary>
    '''  löscht lokale Datenbank, für resyncronisierung des aktuellen Benutzers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LoescheLokaleDatenbank() As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New Entities
                DBContext.Configuration.ProxyCreationEnabled = False
                Dim Eichprozesse = (From eichprozess In DBContext.Eichprozess Where eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And eichprozess.FK_Bearbeitungsstatus <> GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet).ToList

                'liste mit gelöschten eichprozessen
                Dim listIDsEichprozess As New List(Of String)
                Dim listIDsEichprotokoll As New List(Of String)
                Dim listIDsKompatiblitaetsnachweis As New List(Of String)
                DBContext.Configuration.AutoDetectChangesEnabled = False

                'aufbereiten der Listen mit allen IDS.
                For Each obj In Eichprozesse
                    If obj.FK_Eichprotokoll Is Nothing Then Continue For
                    listIDsEichprozess.Add(obj.ID)
                    listIDsEichprotokoll.Add(obj.FK_Eichprotokoll)

                    If Not listIDsKompatiblitaetsnachweis.Contains(obj.FK_Kompatibilitaetsnachweis) Then
                        listIDsKompatiblitaetsnachweis.Add(obj.FK_Kompatibilitaetsnachweis)
                    End If

                    DBContext.Eichprozess.Remove(obj)
                Next

                Dim listMogelstatistik = (From Mogelstatistik In DBContext.Mogelstatistik).ToList
                For Each ID In listIDsEichprozess
                    Dim Mogelstatistiken = From Mogelstatistik In listMogelstatistik Where Mogelstatistik.FK_Eichprozess = ID
                    For Each obj In Mogelstatistiken
                        DBContext.Mogelstatistik.Remove(obj)
                    Next
                Next

                Dim listKompnachweis = (From Kompatiblitaetsnachweis In DBContext.Kompatiblitaetsnachweis).ToList
                For Each ID In listIDsKompatiblitaetsnachweis
                    Dim Kompatiblitaetsnachweise = From Kompatiblitaetsnachweis In listKompnachweis Where Kompatiblitaetsnachweis.ID = ID
                    For Each obj In Kompatiblitaetsnachweise
                        DBContext.Kompatiblitaetsnachweis.Remove(obj)
                    Next
                Next

                Dim listEichprotokoll = (From Kompatiblitaetsnachweis In DBContext.Eichprotokoll).ToList

                Dim listPruefungAnsprechvermoegen = DBContext.PruefungAnsprechvermoegen.ToList
                Dim listPruefungLinearitaetFallend = DBContext.PruefungLinearitaetFallend.ToList
                Dim listPruefungLinearitaetSteigend = DBContext.PruefungLinearitaetSteigend.ToList
                Dim listPruefungRollendeLasten = DBContext.PruefungRollendeLasten.ToList
                Dim listPruefungAussermittigeBelastung = DBContext.PruefungAussermittigeBelastung.ToList
                Dim listPruefungStabilitaetGleichgewichtslage = DBContext.PruefungStabilitaetGleichgewichtslage.ToList
                Dim listPruefungStaffelverfahrenErsatzlast = DBContext.PruefungStaffelverfahrenErsatzlast.ToList
                Dim listPruefungStaffelverfahrenNormallast = DBContext.PruefungStaffelverfahrenNormallast.ToList
                Dim listPruefungWiederholbarkeit = DBContext.PruefungWiederholbarkeit.ToList

                Dim eichprotokolle = From eichprotokoll In listEichprotokoll Where listIDsEichprotokoll.Contains(eichprotokoll.ID)
                For Each obj In eichprotokolle
                    DBContext.Eichprotokoll.Remove(obj)
                Next

                Dim PruefungAnsprechvermoegen = From pruefung In listPruefungAnsprechvermoegen Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next

                Dim PruefungLinearitaetFallend = From pruefung In listPruefungLinearitaetFallend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next

                Dim PruefungLinearitaetSteigend = From pruefung In listPruefungLinearitaetSteigend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next

                Dim PruefungRollendeLasten = From pruefung In listPruefungRollendeLasten Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next

                Dim PruefungAussermittigeBelastung = From pruefung In listPruefungAussermittigeBelastung Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)

                For Each obj In PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next

                Dim PruefungStabilitaetGleichgewichtslage = From pruefung In listPruefungStabilitaetGleichgewichtslage Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenErsatzlast = From pruefung In listPruefungStaffelverfahrenErsatzlast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenNormallast = From pruefung In listPruefungStaffelverfahrenNormallast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next

                Dim PruefungWiederholbarkeit = From pruefung In listPruefungWiederholbarkeit Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next
                DBContext.ChangeTracker.DetectChanges()

                DBContext.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                MessageBox.Show(ex.InnerException.StackTrace, ex.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    MessageBox.Show(ex.InnerException.InnerException.StackTrace, ex.InnerException.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex3 As Exception
                End Try
            Catch ex2 As Exception
            End Try

            Return False
        End Try
    End Function

    ''' <summary>
    ''' Funktion welche prüft ob es noch lokale Eichvorgänge gibt, die nicht an RHEWA versand wurden
    ''' </summary>
    ''' <returns>True wenn noch Eichungen gefunden wurden</returns>
    ''' <remarks>Wird z.b. Genutzt um zu Warnen, bevor ein Benutzer seine lokale DB löscht um eine Teilsynchronisierung vorzunehmen</remarks>
    Public Shared Function PruefeAufUngesendeteEichungen() As Boolean
        Using dbcontext As New Entities
            Dim query = From eichungen In dbcontext.Eichprozess Where eichungen.FK_Vorgangsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet
            If query.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Using
    End Function

    ''' <summary>
    ''' Holt Lizenzobjekt aus DB passend zum Lizenzschlüssel
    ''' </summary>
    ''' <param name="pLizenzschluessel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleLizenzObjekt(ByVal pLizenzschluessel As String) As Lizensierung
        Try
            Using context As New Entities
                Dim objLic = (From lizenz In context.Lizensierung Where lizenz.Lizenzschluessel = pLizenzschluessel)
                Dim listLics As New List(Of Lizensierung)
                listLics = objLic.ToList
                If listLics.Count = 1 Then
                    Return listLics(0)
                Else
                    MessageBox.Show("Kein Eindeutiger Schlüssel gefunden. Es gibt Benutzer mit dem selben Lizenzschlüssel")
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Erzeugt neues leeres Eichprozessobject aus Datenbank
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ErzeugeNeuenEichprozess() As Eichprozess
        Dim objEichprozess As Eichprozess = Nothing
        Using context As New Entities
            objEichprozess = context.Eichprozess.Create
            'pflichtfelder füllen
            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
            objEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
        End Using

        Return objEichprozess
    End Function

    ''' <summary>
    ''' Holt Eichprozess Objekt aus lokaler DB anhand von Vorgangsnummer
    ''' </summary>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleVorhandenenEichprozess(ByVal Vorgangsnummer As String) As Eichprozess
        Using Context As New Entities
            Dim objEichprozess = (From Obj In Context.Eichprozess Select Obj Where Obj.Vorgangsnummer = Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)
            Return objEichprozess
        End Using
    End Function

    ''' <summary>
    ''' Lädt alle Zusatztabellen zum Eichprozess Objekt aus DB. Wird verwendet wenn Lazy Loading deaktiviert wurde
    ''' </summary>
    ''' <param name="objEichprozess"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleNachschlageListenFuerEichprozess(ByVal objEichprozess As Eichprozess) As Eichprozess
        Using Context As New Entities
            objEichprozess = (From Obj In Context.Eichprozess.AsNoTracking.Include("Eichprotokoll") _
                              .Include("Lookup_Auswertegeraet").AsNoTracking _
                              .Include("Kompatiblitaetsnachweis").AsNoTracking _
                              .Include("Lookup_Waegezelle").AsNoTracking _
                              .Include("Lookup_Waagenart").AsNoTracking _
                              .Include("Lookup_Waagentyp").AsNoTracking _
                                                            .Include("Mogelstatistik").AsNoTracking
                              Select Obj Where Obj.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)

            objEichprozess.Lookup_Vorgangsstatus = (From f1 In Context.Lookup_Vorgangsstatus.AsNoTracking Where f1.ID = objEichprozess.FK_Vorgangsstatus Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagenart = (From f1 In Context.Lookup_Waagenart.AsNoTracking Where f1.ID = objEichprozess.FK_WaagenArt Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagentyp = (From f1 In Context.Lookup_Waagentyp.AsNoTracking Where f1.ID = objEichprozess.FK_WaagenTyp Select f1).FirstOrDefault
            objEichprozess.Lookup_Bearbeitungsstatus = (From f1 In Context.Lookup_Bearbeitungsstatus.AsNoTracking Where f1.ID = objEichprozess.FK_Bearbeitungsstatus Select f1).FirstOrDefault
            objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f1 In Context.Lookup_Konformitaetsbewertungsverfahren.AsNoTracking Where f1.ID = objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f1).FirstOrDefault
            objEichprozess.Kompatiblitaetsnachweis = (From f1 In Context.Kompatiblitaetsnachweis.AsNoTracking Where f1.ID = objEichprozess.FK_Kompatibilitaetsnachweis Select f1).FirstOrDefault
            objEichprozess.Lookup_Auswertegeraet = (From f1 In Context.Lookup_Auswertegeraet.AsNoTracking Where f1.ID = objEichprozess.FK_Auswertegeraet Select f1).FirstOrDefault
            objEichprozess.Lookup_Waegezelle = (From f1 In Context.Lookup_Waegezelle.AsNoTracking Where f1.ID = objEichprozess.FK_Waegezelle Select f1).FirstOrDefault

            Return objEichprozess
        End Using

    End Function

    ' ''' <summary>
    ' ''' Erzeugt einen 1zu1 Kopie eines Objektes
    ' ''' </summary>
    ' ''' <typeparam name="T"></typeparam>
    ' ''' <param name="obj"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Shared Function DataContractSerialization(Of T)(obj As T) As T
    '    Dim dcSer As New Runtime.Serialization.DataContractSerializer(obj.[GetType]())
    '    Dim memoryStream As New IO.MemoryStream()

    '    dcSer.WriteObject(memoryStream, obj)
    '    memoryStream.Position = 0

    '    Dim newObject As T = DirectCast(dcSer.ReadObject(memoryStream), T)
    '    Return newObject
    'End Function

    ''' <summary>
    ''' Setzt flag zum ausblenden / einblenden eines Vorgangs
    ''' </summary>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function BlendeEichprozessAus(ByVal Vorgangsnummer As String) As Boolean
        Using context As New Entities
            Dim objEichprozess = (From Obj In context.Eichprozess Select Obj Where Obj.Vorgangsnummer = Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben
            If Not objEichprozess Is Nothing Then
                'umdrehen des ausgeblendet Statuses
                objEichprozess.Ausgeblendet = Not objEichprozess.Ausgeblendet
                context.SaveChanges()
                Return True
            End If
            Return False
        End Using
    End Function

    ''' <summary>
    ''' Lädt alle lokalen DS des aktuellen Benutzers
    ''' </summary>
    ''' <param name="bolAusgeblendeteElementeAnzeigen"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LadeLokaleEichprozessListe(ByVal bolAusgeblendeteElementeAnzeigen As Boolean) As Object
        'neuen Context aufbauen
        Using Context As New Entities
            Context.Configuration.LazyLoadingEnabled = True

            'je nach Sprache die Abfrage anpassen um die entsprechenden Übersetzungen der Lookupwerte aus der DB zu laden
            Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
                Case "de"

                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                 {
                         .Status = Eichprozess.Lookup_Vorgangsstatus.Status,
                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status,
                         Eichprozess.ID,
                         Eichprozess.Vorgangsnummer,
                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                         .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                         .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ,
                         .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art,
                         .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                         Eichprozess.Ausgeblendet,
                         Eichprozess.Bearbeitungsdatum,
                           .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                                      }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "en"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                              .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN,
                                                        Eichprozess.ID,
                                                        Eichprozess.Vorgangsnummer,
                                                          .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_EN,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                           .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "pl"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_PL,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_PL,
                                                        Eichprozess.ID,
                                                        Eichprozess.Vorgangsnummer,
                                                           .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_PL,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_PL,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                         .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case Else
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN,
                                                        Eichprozess.ID, Eichprozess.Vorgangsnummer,
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, .Lookup_Waagentyp_EN = Eichprozess.Lookup_Waagentyp.Typ_EN, .Lookup_Waagentyp_PL = Eichprozess.Lookup_Waagentyp.Typ_PL,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, .Lookup_Waagenart_EN = Eichprozess.Lookup_Waagenart.Art_EN, .Lookup_Waagenart_PL = Eichprozess.Lookup_Waagenart.Art_PL,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                    .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
            End Select

        End Using
    End Function

    Public Shared Function Serialize(obj As Object) As String
        Using memoryStream As New MemoryStream()
            Using reader As New StreamReader(memoryStream)
                Dim serializer As New DataContractSerializer(obj.[GetType]())
                serializer.WriteObject(memoryStream, obj)
                memoryStream.Position = 0
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

    Public Shared Function Deserialize(xml As String, toType As Type) As Object
        Using stream As Stream = New MemoryStream()
            Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(xml)
            stream.Write(data, 0, data.Length)
            stream.Position = 0
            Dim deserializer As New DataContractSerializer(toType)
            Return deserializer.ReadObject(stream)
        End Using
    End Function

    Public Shared Function SendLocalDatabaseToRHEWAFTP()
        Dim objFTP As New clsFTP
        Dim path As String
        Try 'clickonce pfad
            Try
                path = Deployment.Application.ApplicationDeployment.CurrentDeployment?.DataDirectory & "\EichsoftwareClientdatabase.sdf"
            Catch ex2 As Exception
                path = Application.StartupPath & "\EichsoftwareClientdatabase.sdf"
            End Try

            If path = "" Then Return False

            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'daten von WebDB holen
                Dim objFTPDaten = Webcontext.GetFTPCredentials("internal", "999999", "999999", My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                'aufbereiten der für FTP benötigten Verbindungsdaten
                If Not objFTPDaten Is Nothing Then

                    'get decrypted Password and configuration from Database

                    Dim password As String = objFTPDaten.FTPEncryptedPassword
                    ' original plaintext
                    Dim passPhrase As String = "Pas5pr@se"
                    ' can be any string
                    Dim saltValue As String = objFTPDaten.FTPSaltKey
                    ' can be any string
                    Dim hashAlgorithm As String = "SHA1"
                    ' can be "MD5"
                    Dim passwordIterations As Integer = 2
                    ' can be any number
                    Dim initVector As String = "@1B2c3D4e5F6g7H8"
                    ' must be 16 bytes
                    Dim keySize As Integer = 256
                    ' can be 192 or 128
                    Dim UploadfilePath = path

                    password = RijndaelSimple.Decrypt(password, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
                    keySize)

                    'datei upload. FTPUploadPath bekommt den reelen Pfad auf dem FTP Server
                    Dim FTPUploadPath = objFTP.UploadDatabaseFiletoFTP(objFTPDaten.FTPServername, objFTPDaten.FTPUserName, password, UploadfilePath, My.Computer.Name)
                End If
            End Using

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    'Friend Shared Function LoescheEichprozess(Vorgangsnummer As String) As Boolean
    '    Try
    '        Using DBContext As New Entities

    '            Dim eichung = (DBContext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik").Where(Function(C) C.Vorgangsnummer = Vorgangsnummer)).FirstOrDefault
    '            'DBContext.PruefungAnsprechvermoegen.Remove(eichung.Eichprotokoll.PruefungAnsprechvermoegen)
    '            'DBContext.PruefungAussermittigeBelastung.Remove(eichung.Eichprotokoll.PruefungAussermittigeBelastung)
    '            'DBContext.PruefungLinearitaetFallend.Remove(eichung.Eichprotokoll.PruefungLinearitaetFallend)
    '            'DBContext.PruefungLinearitaetSteigend.Remove(eichung.Eichprotokoll.PruefungLinearitaetSteigend)
    '            'DBContext.PruefungRollendeLasten.Remove(eichung.Eichprotokoll.PruefungRollendeLasten)
    '            'DBContext.PruefungStabilitaetGleichgewichtslage.Remove(eichung.Eichprotokoll.PruefungStabilitaetGleichgewichtslage)
    '            'DBContext.PruefungStaffelverfahrenErsatzlast.Remove(eichung.Eichprotokoll.PruefungStaffelverfahrenErsatzlast)
    '            'DBContext.PruefungStaffelverfahrenNormallast.Remove(eichung.Eichprotokoll.PruefungStaffelverfahrenNormallast)
    '            'DBContext.PruefungWiederholbarkeit.Remove(eichung.Eichprotokoll.PruefungWiederholbarkeit)
    '            'DBContext.Eichprotokoll.Remove(eichung.Eichprotokoll)
    '            'DBContext.Mogelstatistik.Remove(eichung.Mogelstatistik)
    '            'DBContext.Kompatiblitaetsnachweis.Remove(eichung.Kompatiblitaetsnachweis)

    '            DBContext.Eichprozess.Remove(eichung)

    '            DBContext.SaveChanges()

    '        End Using
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Public Shared Function LoescheEichprozess(Vorgangsnummer As String) As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New Entities
                DBContext.Configuration.ProxyCreationEnabled = False
                Dim Eichprozesse = (From eichprozess In DBContext.Eichprozess Where eichprozess.Vorgangsnummer = Vorgangsnummer).ToList

                'liste mit gelöschten eichprozessen
                Dim listIDsEichprozess As New List(Of String)
                Dim listIDsEichprotokoll As New List(Of String)
                Dim listIDsKompatiblitaetsnachweis As New List(Of String)
                DBContext.Configuration.AutoDetectChangesEnabled = False

                'aufbereiten der Listen mit allen IDS.
                For Each obj In Eichprozesse
                    DBContext.Eichprozess.Remove(obj)
                    listIDsEichprozess.Add(obj.ID)
                    If obj.FK_Eichprotokoll Is Nothing Then Continue For

                    listIDsEichprotokoll.Add(obj.FK_Eichprotokoll)

                    If Not listIDsKompatiblitaetsnachweis.Contains(obj.FK_Kompatibilitaetsnachweis) Then
                        listIDsKompatiblitaetsnachweis.Add(obj.FK_Kompatibilitaetsnachweis)
                    End If

                Next

                Dim listMogelstatistik = (From Mogelstatistik In DBContext.Mogelstatistik).ToList
                For Each ID In listIDsEichprozess
                    Dim Mogelstatistiken = From Mogelstatistik In listMogelstatistik Where Mogelstatistik.FK_Eichprozess = ID
                    For Each obj In Mogelstatistiken
                        DBContext.Mogelstatistik.Remove(obj)
                    Next
                Next

                Dim listKompnachweis = (From Kompatiblitaetsnachweis In DBContext.Kompatiblitaetsnachweis).ToList
                For Each ID In listIDsKompatiblitaetsnachweis
                    Dim Kompatiblitaetsnachweise = From Kompatiblitaetsnachweis In listKompnachweis Where Kompatiblitaetsnachweis.ID = ID
                    For Each obj In Kompatiblitaetsnachweise
                        DBContext.Kompatiblitaetsnachweis.Remove(obj)
                    Next
                Next

                Dim listEichprotokoll = (From Kompatiblitaetsnachweis In DBContext.Eichprotokoll).ToList

                Dim listPruefungAnsprechvermoegen = DBContext.PruefungAnsprechvermoegen.ToList
                Dim listPruefungLinearitaetFallend = DBContext.PruefungLinearitaetFallend.ToList
                Dim listPruefungLinearitaetSteigend = DBContext.PruefungLinearitaetSteigend.ToList
                Dim listPruefungRollendeLasten = DBContext.PruefungRollendeLasten.ToList
                Dim listPruefungAussermittigeBelastung = DBContext.PruefungAussermittigeBelastung.ToList
                Dim listPruefungStabilitaetGleichgewichtslage = DBContext.PruefungStabilitaetGleichgewichtslage.ToList
                Dim listPruefungStaffelverfahrenErsatzlast = DBContext.PruefungStaffelverfahrenErsatzlast.ToList
                Dim listPruefungStaffelverfahrenNormallast = DBContext.PruefungStaffelverfahrenNormallast.ToList
                Dim listPruefungWiederholbarkeit = DBContext.PruefungWiederholbarkeit.ToList

                Dim eichprotokolle = From eichprotokoll In listEichprotokoll Where listIDsEichprotokoll.Contains(eichprotokoll.ID)
                For Each obj In eichprotokolle
                    DBContext.Eichprotokoll.Remove(obj)
                Next

                Dim PruefungAnsprechvermoegen = From pruefung In listPruefungAnsprechvermoegen Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next

                Dim PruefungLinearitaetFallend = From pruefung In listPruefungLinearitaetFallend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next

                Dim PruefungLinearitaetSteigend = From pruefung In listPruefungLinearitaetSteigend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next

                Dim PruefungRollendeLasten = From pruefung In listPruefungRollendeLasten Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next

                Dim PruefungAussermittigeBelastung = From pruefung In listPruefungAussermittigeBelastung Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)

                For Each obj In PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next

                Dim PruefungStabilitaetGleichgewichtslage = From pruefung In listPruefungStabilitaetGleichgewichtslage Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenErsatzlast = From pruefung In listPruefungStaffelverfahrenErsatzlast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenNormallast = From pruefung In listPruefungStaffelverfahrenNormallast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next

                Dim PruefungWiederholbarkeit = From pruefung In listPruefungWiederholbarkeit Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next
                DBContext.ChangeTracker.DetectChanges()

                DBContext.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                MessageBox.Show(ex.InnerException.StackTrace, ex.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    MessageBox.Show(ex.InnerException.InnerException.StackTrace, ex.InnerException.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex3 As Exception
                End Try
            Catch ex2 As Exception
            End Try

            Return False
        End Try
    End Function
End Class