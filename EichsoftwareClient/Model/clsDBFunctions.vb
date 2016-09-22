Public Class clsDBFunctions

    ' ''' <summary>
    ' ''' DEBUG Funktion um Lizenzdialog aus Testzwecken zu überspringen
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Shared Sub ForceActivation()
    '    Try
    '        Using DBContext As New EichsoftwareClientdatabaseEntities1
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

    ''' <summary>
    '''  löscht lokale Datenbank, für resyncronisierung des aktuellen Benutzers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LoescheLokaleDatenbank() As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New EichsoftwareClientdatabaseEntities1
                Dim Eichprozesse = From eichprozess In DBContext.Eichprozess Where eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And eichprozess.FK_Bearbeitungsstatus <> GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet

                'liste mit gelöschten eichprozessen
                Dim listIDsEichprozess As New List(Of String)
                Dim listIDsEichprotokoll As New List(Of String)
                Dim listIDsKompatiblitaetsnachweis As New List(Of String)

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

                For Each ID In listIDsEichprozess
                    Dim Mogelstatistiken = From Mogelstatistik In DBContext.Mogelstatistik Where Mogelstatistik.FK_Eichprozess = ID
                    For Each obj In Mogelstatistiken
                        DBContext.Mogelstatistik.Remove(obj)
                    Next
                Next

                For Each ID In listIDsKompatiblitaetsnachweis
                    Dim Kompatiblitaetsnachweise = From Kompatiblitaetsnachweis In DBContext.Kompatiblitaetsnachweis Where Kompatiblitaetsnachweis.ID = ID
                    For Each obj In Kompatiblitaetsnachweise
                        DBContext.Kompatiblitaetsnachweis.Remove(obj)
                    Next
                Next

                For Each ID In listIDsEichprotokoll
                    Dim eichprotokolle = From eichprotokoll In DBContext.Eichprotokoll Where eichprotokoll.ID = ID
                    For Each obj In eichprotokolle
                        DBContext.Eichprotokoll.Remove(obj)
                    Next

                    Dim PruefungAnsprechvermoegen = From pruefung In DBContext.PruefungAnsprechvermoegen Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungAnsprechvermoegen
                        DBContext.PruefungAnsprechvermoegen.Remove(obj)
                    Next

                    Dim PruefungLinearitaetFallend = From pruefung In DBContext.PruefungLinearitaetFallend Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungLinearitaetFallend
                        DBContext.PruefungLinearitaetFallend.Remove(obj)
                    Next

                    Dim PruefungLinearitaetSteigend = From pruefung In DBContext.PruefungLinearitaetSteigend Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungLinearitaetSteigend
                        DBContext.PruefungLinearitaetSteigend.Remove(obj)
                    Next

                    Dim PruefungRollendeLasten = From pruefung In DBContext.PruefungRollendeLasten Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungRollendeLasten
                        DBContext.PruefungRollendeLasten.Remove(obj)
                    Next

                    Dim PruefungAussermittigeBelastung = From pruefung In DBContext.PruefungAussermittigeBelastung Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungAussermittigeBelastung
                        DBContext.PruefungAussermittigeBelastung.Remove(obj)
                    Next

                    Dim PruefungStabilitaetGleichgewichtslage = From pruefung In DBContext.PruefungStabilitaetGleichgewichtslage Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungStabilitaetGleichgewichtslage
                        DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                    Next

                    Dim PruefungStaffelverfahrenErsatzlast = From pruefung In DBContext.PruefungStaffelverfahrenErsatzlast Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungStaffelverfahrenErsatzlast
                        DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                    Next

                    Dim PruefungStaffelverfahrenNormallast = From pruefung In DBContext.PruefungStaffelverfahrenNormallast Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungStaffelverfahrenNormallast
                        DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                    Next

                    Dim PruefungWiederholbarkeit = From pruefung In DBContext.PruefungWiederholbarkeit Where pruefung.FK_Eichprotokoll = ID
                    For Each obj In PruefungWiederholbarkeit
                        DBContext.PruefungWiederholbarkeit.Remove(obj)
                    Next

                Next

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
        Using dbcontext As New EichsoftwareClientdatabaseEntities1
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
    Public Shared Function HoleLizenzObjekt(ByVal pLizenzschluessel As string) As Lizensierung
        Try
            Using context As New EichsoftwareClientdatabaseEntities1
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
        Using context As New EichsoftwareClientdatabaseEntities1
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
    Public Shared Function HoleVorhandenenEichprozess(ByVal Vorgangsnummer As string) As Eichprozess
        Using Context As New EichsoftwareClientdatabaseEntities1
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
        Using Context As New EichsoftwareClientdatabaseEntities1
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
    Public Shared Function BlendeEichprozessAus(ByVal Vorgangsnummer As string) As Boolean
        Using context As New EichsoftwareClientdatabaseEntities1
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
        Using Context As New EichsoftwareClientdatabaseEntities1
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
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN,
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

End Class