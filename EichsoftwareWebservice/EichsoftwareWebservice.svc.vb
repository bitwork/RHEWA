' HINWEIS: Mit dem Befehl "Umbenennen" im Kontextmenü können Sie den Klassennamen "Service1" sowohl im Code als auch in der SVC-Datei und der Konfigurationsdatei ändern.
Public Class EichsoftwareWebservice
    Implements IEichsoftwareWebservice

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Prüft ob es sich um eine gültige noch aktive Lizenz handelt
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PruefeLizenz(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.PruefeLizenz
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Prüfe Lizenz")

        Using dbcontext As New EichenSQLDatabaseEntities1
            Dim ObjLizenz = (From lic In dbcontext.ServerLizensierung Where lic.HEKennung = HEKennung And lic.Lizenzschluessel = Lizenzschluessel And lic.Aktiv = True).FirstOrDefault
            If Not ObjLizenz Is Nothing Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' Aggregiert aus der Server DB Kunden und Benutzerinformationen, für Client Datenbank (diese hat keine Tabellen wie Firma, Benutzer etc. Dort sind alle Informationen der Lizenz zugeordnet
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLizenzdaten(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsLizenzdaten Implements IEichsoftwareWebservice.GetLizenzdaten
        Dim objLizenzdaten As New clsLizenzdaten
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole Lizenzdaten")
        Try
            Using dbcontext As New EichenSQLDatabaseEntities1
                'lizenz holen für Benutzer_FK
                Dim objLic = (From lic In dbcontext.ServerLizensierung Where lic.HEKennung = HEKennung And lic.Lizenzschluessel = Lizenzschluessel And lic.Aktiv = True).FirstOrDefault
                'Benutzer anhand von Benutzer_FK holen für Firma
                Dim objBenutzer = (From Benutzer In dbcontext.Benutzer Where Benutzer.HEKennung = objLic.HEKennung).FirstOrDefault
                'firma holen
                Dim objfirma = (From Firma In dbcontext.Firmen Where Firma.ID = objBenutzer.Firma_FK).FirstOrDefault

                objLizenzdaten.Vorname = objBenutzer.Vorname
                objLizenzdaten.Name = objBenutzer.Nachname
                objLizenzdaten.Firma = objfirma.Name
                objLizenzdaten.FirmaPLZ = objfirma.PLZ
                objLizenzdaten.FirmaOrt = objfirma.Ort
                objLizenzdaten.FirmaStrasse = objfirma.Strasse

                Return objLizenzdaten
            End Using
        Catch ex As Exception
            Return objLizenzdaten
        End Try
    End Function

    ''' <summary>
    ''' Funktion welche einen Eintrag in dem SQL Verbindnugsprotokoll vornimmt. Dieses dient zur Nachkontrolle über die Aktivitäten von Benutzern / Lizenzen
    ''' </summary>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <remarks></remarks>
    Public Sub SchreibeVerbindungsprotokoll(ByVal Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, ByVal Aktivitaet As String) Implements IEichsoftwareWebservice.SchreibeVerbindungsprotokoll
        Try
            Using dbcontext As New EichenSQLDatabaseEntities1

                Dim objProtokoll = New ServerVerbindungsprotokoll
                objProtokoll.Lizenzschluessel_FK = Lizenzschluessel
                objProtokoll.Computername = Computername
                objProtokoll.Domain = Domainname
                objProtokoll.Windowsbenutzer = WindowsUsername
                objProtokoll.Aktion = Aktivitaet
                objProtokoll.Zeitstempel = Date.Now

                dbcontext.ServerVerbindungsprotokoll.Add(objProtokoll)
                dbcontext.SaveChanges()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Prüft ob es sich um eine gültige noch aktive Lizenz handelt
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AktiviereLizenz(ByVal HEKennung As String, ByVal Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.AktiviereLizenz
        Using dbcontext As New EichenSQLDatabaseEntities1
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Aktiviere Lizenz")

            Dim ObjLizenz = (From lic In dbcontext.ServerLizensierung Where lic.HEKennung = HEKennung And lic.Lizenzschluessel = Lizenzschluessel And lic.Aktiv = True).FirstOrDefault
            If Not ObjLizenz Is Nothing Then
                ObjLizenz.LetzteAktivierung = Now
                dbcontext.SaveChanges()
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' gibt zurück ob es sich um einen RHEWA Mitarbeiter handelt
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PruefeObRHEWALizenz(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.PruefeObRHEWALizenz
        Using dbcontext As New EichenSQLDatabaseEntities1
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Prüfe ob RHEWA Mitarbeiter")

            Dim ObjLizenz = (From lic In dbcontext.ServerLizensierung Where lic.HEKennung = HEKennung And lic.Lizenzschluessel = Lizenzschluessel).FirstOrDefault
            If Not ObjLizenz Is Nothing Then
                Return ObjLizenz.RHEWALizenz
            Else
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' fügt einen Lokalen Eichprozess der Serverdb hinzu
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="pObjEichprozess"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddEichprozess(ByVal HEKennung As String, Lizenzschluessel As String, ByRef pObjEichprozess As ServerEichprozess, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.AddEichprozess
        Try
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Füge Eichprozess hinzu bzw. Aktualisiere")

            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            'neuen Context aufbauen
            'prüfen ob der eichprozess schoneinmal eingegangen ist anhand von Vorgangsnummer
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = True

                Dim Vorgangsnummer As String = pObjEichprozess.Vorgangsnummer

                Dim Serverob = (From db In DbContext.ServerEichprozess.Include("ServerEichprotokoll").Include("ServerBeschaffenheitspruefung").Include("ServerKompatiblitaetsnachweis") Select db Where db.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                If Serverob Is Nothing Then

                    'wenn neue WZ vorhanden ist
                    If pObjEichprozess.ServerLookup_Waegezelle.Neu Then
                        Try
                            DbContext.ServerLookup_Waegezelle.Add(pObjEichprozess.ServerLookup_Waegezelle)
                            DbContext.SaveChanges()
                        Catch ex As Exception
                        End Try
                    End If
                 
                    pObjEichprozess.UploadDatum = Date.Now
                    pObjEichprozess.ErzeugerLizenz = Lizenzschluessel 'lizenzschlüssel zur identifizierung des datensatztes hinzufügen
                    DbContext.ServerEichprozess.Add(pObjEichprozess)
                    DbContext.SaveChanges()
                Else 'update


                    'aufräumen und löschen der alten Einträge in der Datenbank
                    pObjEichprozess.UploadDatum = Serverob.UploadDatum
                    clsServerHelper.DeleteForeignTables(Serverob)

                    Serverob = (From db In DbContext.ServerEichprozess Select db Where db.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    DbContext.ServerEichprozess.Remove(Serverob)
                    DbContext.SaveChanges()
                    pObjEichprozess.BearbeitungsDatum = Date.Now
                    pObjEichprozess.ErzeugerLizenz = Lizenzschluessel 'lizenzschlüssel zur identifizierung des datensatztes hinzufügen

                    'wenn neue WZ vorhanden ist
                    If pObjEichprozess.ServerLookup_Waegezelle.Neu Then
                        Try
                            DbContext.ServerLookup_Waegezelle.Add(pObjEichprozess.ServerLookup_Waegezelle)
                            DbContext.SaveChanges()
                        Catch ex As Exception
                        End Try
                    End If

                    DbContext.ServerEichprozess.Add(pObjEichprozess)
                    DbContext.SaveChanges()


                End If
            End Using

            HEKennung = Nothing
            Lizenzschluessel = Nothing
            pObjEichprozess = Nothing
            Return True
        Catch ex As Entity.Infrastructure.DbUpdateException
            Debug.WriteLine(ex.InnerException.InnerException.Message)
        Catch ex As Entity.Validation.DbEntityValidationException
            For Each e In ex.EntityValidationErrors
                For Each v In e.ValidationErrors
                    Debug.WriteLine(v.ErrorMessage & " " & v.PropertyName)

                Next

            Next
            Return False
        End Try
    End Function

    ''' <summary>
    ''' holt einen bestimmten serverseitigen Eichprozess 
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEichProzess(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As ServerEichprozess Implements IEichsoftwareWebservice.GetEichProzess
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole Eichprozess")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Obj = (From Eichprozess In DbContext.ServerEichprozess.Include("ServerEichprotokoll") _
                               .Include("ServerLookup_Auswertegeraet").Include("ServerKompatiblitaetsnachweis") _
                               .Include("ServerLookup_Waegezelle").Include("ServerLookup_Waagenart") _
                               .Include("ServerLookup_Waagentyp").Include("ServerBeschaffenheitspruefung") _
                               Where Eichprozess.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    ''abruch
                    If Obj Is Nothing Then Return Nothing



                    'prüfungen
                    Dim EichID As String = Obj.ServerEichprotokoll.ID

                    Try
                        Dim query = From db In DbContext.ServerPruefungAnsprechvermoegen Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungAnsprechvermoegen.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerLookup_Vorgangsstatus Where db.ID = Obj.FK_Vorgangsstatus
                        For Each sourceo In query
                            Obj.ServerLookup_Vorgangsstatus = sourceo
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerPruefungAussermittigeBelastung Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungAussermittigeBelastung.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try


                    Try
                        Dim query = From db In DbContext.ServerPruefungEichfehlergrenzen Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungEichfehlergrenzen.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try


                    Try
                        Dim query = From db In DbContext.ServerPruefungLinearitaetFallend Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungLinearitaetFallend.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try


                    Try
                        Dim query = From db In DbContext.ServerPruefungLinearitaetSteigend Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungLinearitaetSteigend.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerPruefungRollendeLasten Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungRollendeLasten.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerPruefungStabilitaetGleichgewichtslage Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try


                    Try
                        Dim query = From db In DbContext.ServerPruefungStaffelverfahrenErsatzlast Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerPruefungStaffelverfahrenNormallast Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    Try
                        Dim query = From db In DbContext.ServerPruefungWiederholbarkeit Where db.FK_Eichprotokoll = EichID
                        For Each sourceo In query
                            Obj.ServerEichprotokoll.ServerPruefungWiederholbarkeit.Add(sourceo)
                        Next
                    Catch e As Exception
                    End Try

                    HEKennung = Nothing
                    Lizenzschluessel = Nothing
                    Vorgangsnummer = Nothing

                    Return Obj


                Catch ex As Exception
                    'hat nicht funktioniert
                    Return Nothing
                End Try
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' holt alle Eichprozesse des Nutzers (Lizenzschlüssel) im angebebenen Zeitraum. diese funktion ist z.b. für eine neuinstallation gedacht, damit der client wieder seine Eichungen bekommt
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <param name="SyncAllesSeit"></param>
    ''' <param name="SyncAllesBis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlleEichprozesseImZeitraum(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerEichprozess() Implements IEichsoftwareWebservice.GetAlleEichprozesseImZeitraum
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole alle Eichprozesse im Zeitraum")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1


                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Eichprozesse = (From Eichprozess In DbContext.ServerEichprozess.Include("ServerEichprotokoll") _
                               .Include("ServerLookup_Auswertegeraet").Include("ServerKompatiblitaetsnachweis") _
                               .Include("ServerLookup_Waegezelle").Include("ServerLookup_Waagenart") _
                               .Include("ServerLookup_Waagentyp").Include("ServerBeschaffenheitspruefung") Where Eichprozess.UploadDatum > SyncAllesSeit And Eichprozess.UploadDatum < SyncAllesBis And Eichprozess.ErzeugerLizenz = Lizenzschluessel)


                    ''abruch
                    If Eichprozesse Is Nothing Then Return Nothing
                    Dim returnlist As New List(Of ServerEichprozess)
                    For Each Obj In Eichprozesse


                        'prüfungen
                        Dim EichID As String = Obj.ServerEichprotokoll.ID

                        Try
                            Dim query = From db In DbContext.ServerPruefungAnsprechvermoegen Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungAnsprechvermoegen.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerLookup_Vorgangsstatus Where db.ID = Obj.FK_Vorgangsstatus
                            For Each sourceo In query
                                Obj.ServerLookup_Vorgangsstatus = sourceo
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerPruefungAussermittigeBelastung Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungAussermittigeBelastung.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try


                        Try
                            Dim query = From db In DbContext.ServerPruefungEichfehlergrenzen Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungEichfehlergrenzen.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try


                        Try
                            Dim query = From db In DbContext.ServerPruefungLinearitaetFallend Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungLinearitaetFallend.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try


                        Try
                            Dim query = From db In DbContext.ServerPruefungLinearitaetSteigend Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungLinearitaetSteigend.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerPruefungRollendeLasten Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungRollendeLasten.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerPruefungStabilitaetGleichgewichtslage Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try


                        Try
                            Dim query = From db In DbContext.ServerPruefungStaffelverfahrenErsatzlast Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerPruefungStaffelverfahrenNormallast Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try

                        Try
                            Dim query = From db In DbContext.ServerPruefungWiederholbarkeit Where db.FK_Eichprotokoll = EichID
                            For Each sourceo In query
                                Obj.ServerEichprotokoll.ServerPruefungWiederholbarkeit.Add(sourceo)
                            Next
                        Catch e As Exception
                        End Try
                        returnlist.Add(Obj)

                    Next
                    Return returnlist.ToArray

                Catch ex As Exception
                    'hat nicht funktioniert
                    Return Nothing
                End Try
            End Using


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Holt alle Eichprozesse als Datatable für RHEWA ansicht aller Eichprozesse
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlleEichprozesse(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsEichprozessFuerAuswahlliste() Implements IEichsoftwareWebservice.GetAlleEichprozesse
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole alle Eichprozesse")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                Dim lokalerPfadFuerAnhaenge As String = "" 'variable die genutzt wird um den Client den Pfad an dem die Anhänge zu finden sind mitzuteilen
                Try
                    lokalerPfadFuerAnhaenge = (From Config In DbContext.ServerKonfiguration Select Config.NetzwerkpfadFuerDateianhaenge).FirstOrDefault
                    If lokalerPfadFuerAnhaenge Is Nothing Then
                        lokalerPfadFuerAnhaenge = ""
                    End If
                Catch ex As Exception
                    lokalerPfadFuerAnhaenge = ""
                End Try

                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Query = From Eichprozess In DbContext.ServerEichprozess.Include("ServerLookup_Waegezelle") _
                            Join Lookup In DbContext.ServerLookup_Vorgangsstatus On Eichprozess.FK_Vorgangsstatus Equals Lookup.ID _
                            Join Lookup2 In DbContext.ServerLookup_Bearbeitungsstatus On Eichprozess.FK_Bearbeitungsstatus Equals Lookup2.ID _
                                                 Select New With _
               { _
                    Eichprozess.ID, _
                    .Status = Lookup.Status, _
                                Eichprozess.Vorgangsnummer, _
                                .Fabriknummer = Eichprozess.ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                .Lookup_Waegezelle = Eichprozess.ServerLookup_Waegezelle.Typ, _
                                .Lookup_Waagentyp = Eichprozess.ServerLookup_Waagentyp.Typ, _
                                .Lookup_Waagenart = Eichprozess.ServerLookup_Waagenart.Art, _
                                .Lookup_Auswertegeraet = Eichprozess.ServerLookup_Auswertegeraet.Typ, _
                                .Sachbearbeiter = Eichprozess.ServerEichprotokoll.Identifikationsdaten_Benutzer, _
                       .ZurBearbeitungGesperrtDurch = Eichprozess.ZurBearbeitungGesperrtDurch, _
                     .Anhangpfad = Eichprozess.UploadFilePath, _
                     .NeuWZ = Eichprozess.ServerLookup_Waegezelle.Neu, _
                    .Bearbeitungsstatus = Lookup2.Status
                                 }

                    Dim ReturnList As New List(Of clsEichprozessFuerAuswahlliste)

                    'Wrapper für die KLasse. Problematischer Weise kann man keine anoynmen Typen zurückgeben. Deswegen gibt es die behilfsklasse clsEichprozessFuerAuswahlliste.
                    'Diese hat exakt die Eigenschaften die benötigt werden und zusammengesetzt aus der Status Tabelle und dem Eichprozess zusammengebaut wird
                    For Each objeichprozess In Query
                        Dim objReturn As New clsEichprozessFuerAuswahlliste
                        objReturn.ID = objeichprozess.ID
                        If Not objeichprozess.Lookup_Auswertegeraet Is Nothing Then
                            objReturn.AWG = objeichprozess.Lookup_Auswertegeraet
                        End If

                        If Not objeichprozess.Fabriknummer Is Nothing Then
                            objReturn.Fabriknummer = objeichprozess.Fabriknummer
                        End If

                        If Not objeichprozess.Vorgangsnummer Is Nothing Then
                            objReturn.Vorgangsnummer = objeichprozess.Vorgangsnummer
                        End If

                        If Not objeichprozess.Lookup_Waagenart Is Nothing Then
                            objReturn.Waagenart = objeichprozess.Lookup_Waagenart
                        End If

                        If Not objeichprozess.Lookup_Waagentyp Is Nothing Then
                            objReturn.Waagentyp = objeichprozess.Lookup_Waagentyp
                        End If

                        If Not objeichprozess.Lookup_Waegezelle Is Nothing Then
                            objReturn.WZ = objeichprozess.Lookup_Waegezelle
                        End If

                        If Not objeichprozess.Sachbearbeiter Is Nothing Then
                            objReturn.Eichbevollmaechtigter = objeichprozess.Sachbearbeiter
                        End If

                        If Not objeichprozess.Bearbeitungsstatus Is Nothing Then
                            objReturn.Bearbeitungsstatus = objeichprozess.Bearbeitungsstatus
                        End If

                        If Not objeichprozess.ZurBearbeitungGesperrtDurch Is Nothing Then
                            objReturn.GesperrtDurch = objeichprozess.ZurBearbeitungGesperrtDurch
                        End If

                        If Not objeichprozess.NeuWZ Is Nothing Then
                            objReturn.NeueWZ = objeichprozess.NeuWZ
                        End If

                        'dateipfad zusammenbauen
                        If Not objeichprozess.Anhangpfad Is Nothing Then
                            If Not objeichprozess.Anhangpfad.Trim.Equals("") Then
                                If lokalerPfadFuerAnhaenge.EndsWith("\") Then
                                    objReturn.AnhangPfad = lokalerPfadFuerAnhaenge & objeichprozess.Anhangpfad
                                Else
                                    objReturn.AnhangPfad = lokalerPfadFuerAnhaenge & "\" & objeichprozess.Anhangpfad
                                End If
                            End If
                        End If
                        '   Dim ModelArtikel As New Model.clsArtikel(objArtikel.Id, objArtikel.HEKennung, objArtikel.Beschreibung, objArtikel.Preis, objArtikel.ErstellDatum)
                        ReturnList.Add(objReturn)
                    Next

                    'ergebnismenge zurückgeben
                    If Not ReturnList.Count = 0 Then
                        Return ReturnList.ToArray
                    Else
                        'es gibt keine neuerungen
                        Return Nothing
                    End If

                Catch ex As Exception
                    'hat nicht funktioniert
                    Return Nothing
                End Try
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Holt alle WZ im Zeitraum
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="LetztesUpdate"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <param name="SyncAllesSeit"></param>
    ''' <param name="SyncAllesBis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNeueWZ(ByVal HEKennung As String, Lizenzschluessel As String, ByVal LetztesUpdate As Date, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerLookup_Waegezelle() Implements IEichsoftwareWebservice.GetNeueWZ
        ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
        If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole WZ")


        Using DBContext As New EichenSQLDatabaseEntities1
            DBContext.Configuration.LazyLoadingEnabled = False
            DBContext.Configuration.ProxyCreationEnabled = False
            Try
                'fallunterscheidung für die zu holende Datenmenge:
                'fall A= Start und Enddate sind auf Default wert => Hole alles seit letztem Update
                'fall B = StartWert ist ungleich default werd, aber Enddate ist default => Holle alles ab dem Startdate und dann seit dem letzten Update
                'fall c = StartWert ist ungleich default werd, und Enddate ist ungleich default => Holle alles ab dem Startdate und Endwert, dann seit dem letzten Update wenn dieses nicht über dem Endwert liegt
                Dim query As System.Linq.IOrderedQueryable(Of ServerLookup_Waegezelle)

                If SyncAllesSeit = #1/1/2000# And SyncAllesBis = #12/31/2999# Then 'fall A
                    query = From d In DBContext.ServerLookup_Waegezelle Where d.ErstellDatum >= LetztesUpdate Order By d.ID
                ElseIf SyncAllesSeit <> #1/1/2000# And SyncAllesBis = #12/31/2999# Then 'fall b
                    query = From d In DBContext.ServerLookup_Waegezelle
                            Where d.ErstellDatum >= SyncAllesSeit And d.ErstellDatum >= LetztesUpdate
                            Order By d.ID
                ElseIf SyncAllesSeit <> #1/1/2000# And SyncAllesBis <> #12/31/2999# Then 'fall c
                    '#ndern des Letztes Update wertes
                    If LetztesUpdate > SyncAllesBis Then
                        LetztesUpdate = SyncAllesBis
                    End If
                    If LetztesUpdate < SyncAllesSeit Then
                        LetztesUpdate = SyncAllesSeit
                    End If
                    query = From d In DBContext.ServerLookup_Waegezelle
                            Where d.ErstellDatum >= SyncAllesSeit And d.ErstellDatum <= SyncAllesBis And d.ErstellDatum >= LetztesUpdate
                            Order By d.ID
                Else 'fall das Startwert gleich default aber endwert nicht, ist nicht vorgesehen. Fallback auf Sync Alles seit
                    query = From d In DBContext.ServerLookup_Waegezelle Where d.ErstellDatum >= LetztesUpdate Order By d.ID
                End If





                Dim ReturnList As New List(Of ServerLookup_Waegezelle)

                For Each objWZ In query
                    '   Dim ModelArtikel As New Model.clsArtikel(objArtikel.Id, objArtikel.HEKennung, objArtikel.Beschreibung, objArtikel.Preis, objArtikel.ErstellDatum)
                    ReturnList.Add(objWZ)
                Next

                'ergebnismenge zurückgeben
                If Not ReturnList.Count = 0 Then
                    Return ReturnList.ToArray
                Else
                    'es gibt keine neuerungen
                    Return Nothing
                End If

            Catch ex As Exception
                'hat nicht funktioniert
                Return Nothing
            End Try
        End Using


    End Function

    ''' <summary>
    ''' Holt alle AWGs im Zeitraum
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="LetztesUpdate"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <param name="SyncAllesSeit"></param>
    ''' <param name="SyncAllesBis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNeuesAWG(ByVal HEKennung As String, Lizenzschluessel As String, ByVal LetztesUpdate As Date, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerLookup_Auswertegeraet() Implements IEichsoftwareWebservice.GetNeuesAWG
        ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
        If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole AWG")

        Using DBContext As New EichenSQLDatabaseEntities1
            DBContext.Configuration.LazyLoadingEnabled = False
            DBContext.Configuration.ProxyCreationEnabled = False
            Try
                'fallunterscheidung für die zu holende Datenmenge:
                'fall A= Start und Enddate sind auf Default wert => Hole alles seit letztem Update
                'fall B = StartWert ist ungleich default werd, aber Enddate ist default => Holle alles ab dem Startdate und dann seit dem letzten Update
                'fall c = StartWert ist ungleich default werd, und Enddate ist ungleich default => Holle alles ab dem Startdate und Endwert, dann seit dem letzten Update wenn dieses nicht über dem Endwert liegt
                Dim query As System.Linq.IOrderedQueryable(Of ServerLookup_Auswertegeraet)

                If SyncAllesSeit = #1/1/2000# And SyncAllesBis = #12/31/2999# Then 'fall A
                    query = From d In DBContext.ServerLookup_Auswertegeraet Where d.ErstellDatum >= LetztesUpdate Order By d.ID
                ElseIf SyncAllesSeit <> #1/1/2000# And SyncAllesBis = #12/31/2999# Then 'fall b
                    query = From d In DBContext.ServerLookup_Auswertegeraet
                            Where d.ErstellDatum >= SyncAllesSeit And d.ErstellDatum >= LetztesUpdate
                            Order By d.ID
                ElseIf SyncAllesSeit <> #1/1/2000# And SyncAllesBis <> #12/31/2999# Then 'fall c
                    '#ndern des Letztes Update wertes
                    If LetztesUpdate > SyncAllesBis Then
                        LetztesUpdate = SyncAllesBis
                    End If
                    If LetztesUpdate < SyncAllesSeit Then
                        LetztesUpdate = SyncAllesSeit
                    End If
                    query = From d In DBContext.ServerLookup_Auswertegeraet
                            Where d.ErstellDatum >= SyncAllesSeit And d.ErstellDatum <= SyncAllesBis And d.ErstellDatum >= LetztesUpdate
                            Order By d.ID
                Else 'fall das Startwert gleich default aber endwert nicht, ist nicht vorgesehen. Fallback auf Sync Alles seit
                    query = From d In DBContext.ServerLookup_Auswertegeraet Where d.ErstellDatum >= LetztesUpdate Order By d.ID
                End If





                Dim ReturnList As New List(Of ServerLookup_Auswertegeraet)

                For Each objWZ In query
                    '   Dim ModelArtikel As New Model.clsArtikel(objArtikel.Id, objArtikel.HEKennung, objArtikel.Beschreibung, objArtikel.Preis, objArtikel.ErstellDatum)
                    ReturnList.Add(objWZ)
                Next

                'ergebnismenge zurückgeben
                If Not ReturnList.Count = 0 Then
                    Return ReturnList.ToArray
                Else
                    'es gibt keine neuerungen+
                    Return Nothing
                End If

            Catch ex As Exception
                'hat nicht funktioniert
                Return Nothing
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Funktion für die Statistikfunktion der Eichmarkenverwaltung. Dort können Marken eingetragen werden, die ausgeteilt wurden. Wenn ein Benutzer dann Marken im Eichprozess verwendet, werden sie dort abgezogen um einen Ist-Bestand zu erhalten
    ''' </summary>
    ''' <param name="BenutzerFK"></param>
    ''' <param name="AnzahlBenannteStelle"></param>
    ''' <param name="AnzahlEichsiegel13x13"></param>
    ''' <param name="AnzahlEichsiegelRund"></param>
    ''' <param name="AnzahlHinweismarke"></param>
    ''' <param name="AnzahlGruenesM"></param>
    ''' <param name="AnzahlCE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddEichmarkenverwaltung(ByVal HEKennung As String, Lizenzschluessel As String, ByVal BenutzerHEKennungFK As String, ByVal AnzahlBenannteStelle As Integer, ByVal AnzahlEichsiegel13x13 As Integer, _
                                            ByVal AnzahlEichsiegelRund As Integer, ByVal AnzahlHinweismarke As Integer, ByVal AnzahlGruenesM As Integer, ByVal AnzahlCE As Integer, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.AddEichmarkenverwaltung
        ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
        If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Aktualisiere Eichmarkenverwaltung")
        'FK_Benutzer und HEKennung sind eigentlich doppelt. Es stört aber auch nicht.
        Try
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False

                Dim Element = (From d In DbContext.ServerEichmarkenverwaltung Where d.HEKennung = BenutzerHEKennungFK Order By d.ID).FirstOrDefault

                Try
                    Element.BenannteStelleAnzahl -= AnzahlBenannteStelle
                Catch e As Exception
                End Try
                Try
                    Element.Eichsiegel13x13Anzahl -= AnzahlEichsiegel13x13
                Catch e As Exception
                End Try
                Try
                    Element.EichsiegelRundAnzahl -= AnzahlEichsiegelRund
                Catch e As Exception
                End Try
                Try
                    Element.HinweismarkeGelochtAnzahl -= AnzahlHinweismarke
                Catch e As Exception
                End Try
                Try
                    Element.GruenesMAnzahl -= AnzahlGruenesM
                Catch e As Exception
                End Try
                Try
                    Element.CEAnzahl -= AnzahlCE
                Catch e As Exception
                End Try

                Try
                    DbContext.SaveChanges()
                Catch e As Exception
                End Try

                Element = Nothing

                Return True
            End Using
        Catch e As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    '''  Der Client ruft diese Methode auf, mit allen seinen lokalen Prüfungen die im Status auf "wird geprüft stehen" auf
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Public Function CheckGueltigkeitEichprozess(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String Implements IEichsoftwareWebservice.CheckGueltigkeitEichprozess
        'da die ID im Server von der im Client abweichen kann wird hier mit der Vorgangsnummer gearbeitet die pro Prozess Eindeutig generiert wrid
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Prüfe gültigkeit des Eichprozesses")


            'neuen Context aufbauen
            'prüfen ob der eichprozess schoneinmal eingegangen ist anhand von Vorgangsnummer
            Using DbContext As New EichenSQLDatabaseEntities1
                Dim Serverob = (From db In DbContext.ServerEichprozess Select db Where db.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                If Not Serverob Is Nothing Then
                    HEKennung = Nothing
                    Lizenzschluessel = Nothing
                    Vorgangsnummer = Nothing
                    Return Serverob.FK_Bearbeitungsstatus
                End If

            End Using

            HEKennung = Nothing
            Lizenzschluessel = Nothing
            Vorgangsnummer = Nothing
            Return Nothing
        Catch ex As Entity.Infrastructure.DbUpdateException
            Debug.WriteLine(ex.InnerException.InnerException.Message)
            Return Nothing
        Catch ex As Entity.Validation.DbEntityValidationException
            For Each e In ex.EntityValidationErrors
                For Each v In e.ValidationErrors
                    Debug.WriteLine(v.ErrorMessage & " " & v.PropertyName)

                Next

            Next
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genehmigt übermittelten Eichprozess, so das Client ihn herunterladen kann
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetEichprozessUngueltig(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.SetEichprozessUngueltig
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Setze Eichprozess auf ungültig")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Obj = (From Eichprozess In DbContext.ServerEichprozess.Include("ServerEichprotokoll")
                               Where Eichprozess.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    ''abruch
                    If Obj Is Nothing Then Return Nothing

                    ''setzte Wert für Prüfer im Eichprotokoll
                    'Try
                    '    Obj.ServerEichprotokoll.Identifikationsdaten_Pruefer = WindowsUsername.Split("\")(1)
                    'Catch ex As Exception
                    '    Obj.ServerEichprotokoll.Identifikationsdaten_Pruefer = WindowsUsername
                    'End Try



                    'Fehlerhafter Status aus Lookup_Bearbeitungsstatus = 2
                    Obj.FK_Bearbeitungsstatus = 2
                    DbContext.SaveChanges()

                    HEKennung = Nothing
                    Lizenzschluessel = Nothing
                    Vorgangsnummer = Nothing

                    Return True


                Catch ex As Exception
                    'hat nicht funktioniert
                    Return False
                End Try
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lehnt Eichprozess ab. Client muss erst korrigieren und neu einsenden
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetEichprozessgenehmigt(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean Implements IEichsoftwareWebservice.SetEichprozessGenehmight
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Setze Eichprozess auf gültig")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Obj = (From Eichprozess In DbContext.ServerEichprozess.Include("ServerEichprotokoll")
                               Where Eichprozess.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    ''abruch
                    If Obj Is Nothing Then Return Nothing


                    ''setzte Wert für Prüfer im Eichprotokoll
                    'Try
                    '    Obj.ServerEichprotokoll.Identifikationsdaten_Pruefer = WindowsUsername.Split("\")(1)
                    'Catch ex As Exception
                    '    Obj.ServerEichprotokoll.Identifikationsdaten_Pruefer = WindowsUsername

                    'End Try


                    'Genehmighter Status aus Lookup_Bearbeitungsstatus = 3
                    Obj.FK_Bearbeitungsstatus = 3
                    DbContext.SaveChanges()

                    HEKennung = Nothing
                    Lizenzschluessel = Nothing
                    Vorgangsnummer = Nothing

                    Return True


                Catch ex As Exception
                    'hat nicht funktioniert
                    Return False
                End Try
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Prüft ob die aktuelle Eichung von jemand anderem Bearbeitet wird
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns>WindowsUsername des Sperrers wenn gesperrt</returns>
    ''' <remarks></remarks>
    Function CheckSperrung(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String Implements IEichsoftwareWebservice.CheckSperrung
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Prüfe Sperrung")

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Obj = (From Eichprozess In DbContext.ServerEichprozess
                               Where Eichprozess.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    ''abruch
                    If Obj Is Nothing Then Return ""

                    'Keine sperrung vorliegend
                    Try
                        If Obj.ZurBearbeitungGesperrtDurch Is Nothing Then
                            Return ""
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If IsDBNull(Obj.ZurBearbeitungGesperrtDurch) Then
                            Return ""
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Obj.ZurBearbeitungGesperrtDurch.Equals("") Then
                            Return ""
                        End If
                    Catch ex As Exception

                    End Try


                    'prüfen ob der Sperrer nicht einem selbst entspricht
                    If Obj.ZurBearbeitungGesperrtDurch = WindowsUsername Then
                        Return ""
                    Else
                        Return Obj.ZurBearbeitungGesperrtDurch
                    End If

                    'ansonsten 
                    Return ""

                Catch ex As Exception
                    'hat nicht funktioniert
                    Return "Fehler beim Prüfen"
                End Try
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Sperrt oder entsperrt aktuelle Eichung, so das ein anderer Mitarbeiter der sie öffnet einen Warnhinweis bekommt
    ''' </summary>
    ''' <param name="bolSperren"></param>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetSperrung(ByVal bolSperren As Boolean, ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String Implements IEichsoftwareWebservice.SetSperrung
        Try
            ''abruch falls irgend jemand den Service ohne gültige Lizenz aufruft
            If PruefeLizenz(HEKennung, Lizenzschluessel, WindowsUsername, Domainname, Computername) = False Then Return Nothing
            SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Setze Sperrung auf: " & bolSperren)

            'neuen Context aufbauen
            Using DbContext As New EichenSQLDatabaseEntities1
                DbContext.Configuration.LazyLoadingEnabled = False
                DbContext.Configuration.ProxyCreationEnabled = False
                Try
                    Dim Obj = (From Eichprozess In DbContext.ServerEichprozess
                               Where Eichprozess.Vorgangsnummer = Vorgangsnummer).FirstOrDefault

                    ''abruch
                    If Obj Is Nothing Then Return "Objekt nicht gefunden"


                    'entsperren wen bolsperren = false
                    If bolSperren Then
                        'Keine sperrung vorliegend, wir setzten sie
                        Try
                            If Obj.ZurBearbeitungGesperrtDurch Is Nothing Then
                                Obj.ZurBearbeitungGesperrtDurch = WindowsUsername
                                DbContext.SaveChanges()
                                Return ""
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            If IsDBNull(Obj.ZurBearbeitungGesperrtDurch) Then
                                Obj.ZurBearbeitungGesperrtDurch = WindowsUsername
                                DbContext.SaveChanges()
                                Return ""
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            If (Obj.ZurBearbeitungGesperrtDurch).Equals("") Then
                                Obj.ZurBearbeitungGesperrtDurch = WindowsUsername
                                DbContext.SaveChanges()
                                Return ""
                            End If
                        Catch ex As Exception

                        End Try


                        'prüfen ob man selbst gesperrt hat
                        If Obj.ZurBearbeitungGesperrtDurch = WindowsUsername Then
                            Return ""
                        End If


                        'ansonsten 
                        Return "Benutzer ist nicht der Sperrer"

                    Else
                        Obj.ZurBearbeitungGesperrtDurch = ""
                        DbContext.SaveChanges()
                        Return ""
                    End If


                Catch ex As Exception
                    'hat nicht funktioniert
                    Return ex.Message
                End Try
            End Using
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ''' <summary>
    ''' funktion welche die konfigurationsdatenbank von RHEWA ausliest und die benötigten Werte für eine FTP verschlüsselung zurückgibt
    ''' </summary>
    ''' <param name="HEKennung"></param>
    ''' <param name="Lizenzschluessel"></param>
    ''' <param name="Vorgangsnummer"></param>
    ''' <param name="WindowsUsername"></param>
    ''' <param name="Domainname"></param>
    ''' <param name="Computername"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFTPCredentials(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsServerFTPDaten Implements IEichsoftwareWebservice.GetFTPCredentials
        SchreibeVerbindungsprotokoll(Lizenzschluessel, WindowsUsername, Domainname, Computername, "Hole FTP Zugangsdaten")

        Using dbcontext As New EichenSQLDatabaseEntities1
            Dim ObjLizenz = (From lic In dbcontext.ServerLizensierung Where lic.HEKennung = HEKennung And lic.Lizenzschluessel = Lizenzschluessel And lic.Aktiv = True).FirstOrDefault
            If Not ObjLizenz Is Nothing Then

                Dim ObjFTPDaten = (From FTPDaten In dbcontext.ServerKonfiguration).FirstOrDefault
                If Not ObjFTPDaten Is Nothing Then

                    Dim objServerFTPDaten As New clsServerFTPDaten
                    objServerFTPDaten.FTPServername = ObjFTPDaten.FTPServerAdresse
                    objServerFTPDaten.FTPUserName = ObjFTPDaten.FTPServerBenutzername
                    objServerFTPDaten.FTPEncryptedPassword = ObjFTPDaten.FTPServerEnkodiertesPasswort
                    objServerFTPDaten.FTPSaltKey = ObjFTPDaten.FTPServerCryptoSaltKey

                    Return objServerFTPDaten
                End If
            End If
        End Using
        Return Nothing
    End Function


End Class
