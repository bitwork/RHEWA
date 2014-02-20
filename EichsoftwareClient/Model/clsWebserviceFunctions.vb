Public Class clsWebserviceFunctions

    ''' <summary>
    ''' Prüft ob Webservice aufgerufen werden kann
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TesteVerbindung() As Boolean
        Try
            'versuche Verbindung zu RHEWA aufzubauen
            'wenn nicht möglich Abbruch
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                webContext.Open()
                webContext.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Holt der Benutzerlizenz zugeordnete Stammdaten wie Name der Firma vom Webservice
    ''' </summary>
    ''' <param name="bolneuStammdaten"></param>
    ''' <remarks></remarks>
    Public Sub GetNeueStammdaten(ByRef bolneuStammdaten As Boolean)
        Try
            'abrufen neuer WZ aus Server, basierend auf dem Wert des letzten erfolgreichen updates
            Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    webContext.Open()
                Catch ex As Exception
                    Exit Sub
                End Try
                Using DBContext As New EichsoftwareClientdatabaseEntities1
                    'lizenzisierung holen
                    Dim objLic = (From db In DBContext.Lizensierung Select db).FirstOrDefault
                    'hole zusätliche Lizenzdaten
                    Dim objLizenzdaten As EichsoftwareWebservice.clsLizenzdaten = webContext.GetLizenzdaten(objLic.FK_Benutzer, objLic.Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                    objLic.Name = objLizenzdaten.Name
                    objLic.Vorname = objLizenzdaten.Vorname
                    objLic.Firma = objLizenzdaten.Firma
                    objLic.FirmaOrt = objLizenzdaten.FirmaOrt
                    objLic.FirmaPLZ = objLizenzdaten.FirmaPLZ
                    objLic.FirmaStrasse = objLizenzdaten.FirmaStrasse
                    DBContext.SaveChanges()
                End Using
            End Using
        Catch ex As Exception

        End Try



    End Sub

    ''' <summary>
    ''' holt neue oder aktualisierte WZ aus DB über Webservice
    ''' </summary>
    ''' <param name="bolNeuWZ">Variable welche für eine Erfolgsmeldung genutzt wird. Wird beim aktualisieren auf True gesetzt um den Nutzer darauf hinzuweisen, dass etwas neues heruntergeladen wurde.</param>
    ''' <remarks></remarks>
    Public Sub GetNeueWZ(ByRef bolNeuWZ As Boolean)
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
                    Dim objWZResultList = webContext.GetNeueWZ(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, My.Settings.LetztesUpdate, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)

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
                                    newWZ.Deaktiviert = objServerArtikel._Deaktiviert
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
                                        objWZ.Deaktiviert = objServerArtikel._Deaktiviert
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
    Public Sub GetNeuesAWG(ByRef bolNeuAWG As Boolean)
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
                    Dim objAWGResultList = webContext.GetNeuesAWG(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, My.Settings.LetztesUpdate, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)

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
                                newAWG.Deaktiviert = objServerArtikel._Deaktiviert
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


    ''' <summary>
    ''' holt aktuellen Status eigenerer Eichungenen (z.b. Abgelehnt oder Erfolgreich) aus DB über Webservice
    ''' </summary>
    ''' <param name="bolNeuGenehmigung">Variable welche für eine Erfolgsmeldung genutzt wird. Wird beim aktualisieren auf True gesetzt um den Nutzer darauf hinzuweisen, dass etwas neues heruntergeladen wurde.</param>
    ''' <remarks></remarks>
    Public Sub GetGenehmigungsstatus(ByRef bolNeuGenehmigung As Boolean)
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
                            Dim NeuerStatus As String = webContext.CheckGueltigkeitEichprozess(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Eichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                            If Not NeuerStatus Is Nothing Then
                                If Eichprozess.FK_Bearbeitungsstatus <> NeuerStatus Then

                                    'wenn es eine Änderung gab, wird das geänderte Objekt vom Server abgerufen. Damit können änderungen die von einem RHEWA Mitarbeiter durchgeführt wurden übernommen werden
                                    '###################
                                    'neue Datenbankverbindung

                                    Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Eichprozess.Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


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
                                        MessageBox.Show(ex.InnerException.InnerException.Message)
                                    Catch ex2 As Entity.Validation.DbEntityValidationException
                                        For Each o In ex2.EntityValidationErrors
                                            For Each v In o.ValidationErrors
                                                MessageBox.Show(v.ErrorMessage & " " & v.PropertyName)
                                            Next
                                        Next
                                    End Try

                                    clsServerHelper.UpdateForeignTables(Eichprozess, objServerEichprozess)


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
    ''' holt alle einem zugehörigen Eichprotokolle vom RHEWA Server. z.B. wenn die anwendung auf einem neuem PC installiert wurde
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetEichprotokolleVomServer()
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
                        Dim objServerEichprozesse = webContext.GetAlleEichprozesseImZeitraum(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, StartDatum, EndDatum)


                        If objServerEichprozesse Is Nothing Then
                            Exit Sub
                            'MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Function GetServerEichprotokollListe() As EichsoftwareWebservice.clsEichprozessFuerAuswahlliste()
        Try
            'neuen Context aufbauen
            Using WebContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    WebContext.Open()
                Catch ex As Exception
                    Return Nothing
                End Try

                Using dbcontext As New EichsoftwareClientdatabaseEntities1
                    Dim objLic = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                    Try
                        Dim data = WebContext.GetAlleEichprozesse(objLic.FK_Benutzer, objLic.Lizenzschluessel, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                        Return data
                    Catch ex As Exception
                        Return Nothing
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetLokaleKopieVonEichprozess(ByVal Vorgangsnummer As String) As Eichprozess
        'neue Datenbankverbindung
        Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                webContext.Open()


            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                Try


                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                    Dim objClientEichprozess = dbcontext.Eichprozess.Create
                    Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


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
                        MessageBox.Show(ex.InnerException.InnerException.Message)
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalie, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK)
                        If My.Settings.RHEWALizenz Then
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_SpeicherAnomalieRhewaZusatztext, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK)
                        End If

                        Return Nothing
                    Catch ex2 As Entity.Validation.DbEntityValidationException
                        For Each o In ex2.EntityValidationErrors
                            For Each v In o.ValidationErrors
                                MessageBox.Show(v.ErrorMessage & " " & v.PropertyName)
                            Next
                        Next
                        Return Nothing
                    End Try

                    Return objClientEichprozess

                Catch ex As Exception
                    Return Nothing
                End Try
            End Using
        End Using

    End Function

    Public Function GenehmigeEichprozess(ByVal Vorgangsnummer As String)
        'neue Datenbankverbindung
        Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                webContext.Open()
            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
            Using dbcontext As New EichsoftwareClientdatabaseEntities1

                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                'prüfen ob der datensatz von jemand anderem in Bearbeitung ist
                Dim bolSetGueltig As Boolean = True 'variable zum abbrechen des Prozesses, falls jemand anderes an dem DS arbeitet
                Dim Messagetext As String = ""
                Messagetext = webContext.CheckSperrung(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                If Messagetext.Equals("") = False Then
                    'rhewa arbeitet in deutsch und hat keine lokalisierung gewünscht
                    Dim result As String
                    result = webContext.SetSperrung(True, objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                    If result = "" Then
                        bolSetGueltig = True
                    Else
                        MessageBox.Show(result)
                        bolSetGueltig = False
                        Return False
                    End If
                End If
                If bolSetGueltig Then
                    webContext.SetEichprozessGenehmight(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                    Return True
                End If
            End Using
        End Using
        Return False
    End Function

    Public Function AblehnenEichprozess(ByVal Vorgangsnummer As String) As Boolean
        'neue Datenbankverbindung
        Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                webContext.Open()
            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                'prüfen ob der datensatz von jemand anderem in Bearbeitung ist
                Dim bolSetUnueltig As Boolean = True 'variable zum abbrechen des Prozesses, falls jemand anderes an dem DS arbeitet
                Dim Messagetext As String = ""
                Messagetext = webContext.CheckSperrung(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                If Messagetext.Equals("") = False Then
                    'rhewa arbeitet in deutsch und hat keine lokalisierung gewünscht
                    If MessageBox.Show("Dieser Eichprozess wird von '" & Messagetext & "' bearbeitet. Möchten Sie seine Arbeit wirklich überschreiben und den Prozess ablehnen?", My.Resources.GlobaleLokalisierung.Frage, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim result As String
                        result = webContext.SetSperrung(True, objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                        If result = "" Then
                            bolSetUnueltig = True
                        Else
                            MessageBox.Show(result)
                            bolSetUnueltig = False
                            Return False
                        End If
                    Else
                        bolSetUnueltig = False
                    End If
                End If
                If bolSetUnueltig Then
                    webContext.SetEichprozessUngueltig(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)
                    Return True
                End If
            End Using
        End Using
        Return False
    End Function


    Public Function ZeigeServerEichprozess(ByVal Vorgangsnummer As String) As Eichprozess
        'neue Datenbankverbindung
        Using webContext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
            Try
                webContext.Open()


            Catch ex As Exception
                MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try

            Using dbcontext As New EichsoftwareClientdatabaseEntities1

                Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault
                Dim objClientEichprozess = dbcontext.Eichprozess.Create
                Dim objServerEichprozess = webContext.GetEichProzess(objLiz.FK_Benutzer, objLiz.Lizenzschluessel, Vorgangsnummer, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)


                If objServerEichprozess Is Nothing Then
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_KeinServerObjektEichung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                'umwandeln des Serverobjektes in Clientobject
                clsServerHelper.CopyObjectPropertiesWithAllLookups(objClientEichprozess, objServerEichprozess)
                clsServerHelper.GetLookupValuesServer(objClientEichprozess)

                Return objClientEichprozess
            End Using
        End Using
        Return Nothing
    End Function
End Class
