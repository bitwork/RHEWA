Public Class clsServerHelper

    ''' <summary>
    ''' Methode welche alle N:1 Verbindungen auf einen Eichprozess entfernt 
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteForeignTables(ByRef TargetObject As ServerEichprozess)
        Try

            Using dbcontext As New EichenSQLDatabaseEntities
                Dim EichprozessID As String = TargetObject.ID
                Dim EichprotokollID As String = TargetObject.ServerEichprotokoll.ID


                Dim mogelobjqery = (From db In dbcontext.ServerMogelstatistik Select db Where db.FK_Eichprozess = EichprozessID)
                For Each obj In mogelobjqery
                    dbcontext.ServerMogelstatistik.Remove(obj)
                Next
                dbcontext.SaveChanges()
                'neu laden der instanz damit TRacking des Contextes aktiv ist
                TargetObject = (From d In dbcontext.ServerEichprozess.Include("ServerEichprotokoll").Include("ServerBeschaffenheitspruefung").Include("ServerKompatiblitaetsnachweis") Where d.ID = EichprozessID Select d).FirstOrDefault

                'prüfungen
                Try
                    'aufräumen und alte löschen
                    Dim query = From a In dbcontext.ServerPruefungAnsprechvermoegen Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query
                        dbcontext.ServerPruefungAnsprechvermoegen.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try


                Try
                    'aufräumen und alte löschen
                    Dim query2 = From a In dbcontext.ServerPruefungAussermittigeBelastung Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query2
                        dbcontext.ServerPruefungAussermittigeBelastung.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try


                Try
                    Dim query4 = From a In dbcontext.ServerPruefungLinearitaetFallend Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query4
                        dbcontext.ServerPruefungLinearitaetFallend.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query5 = From a In dbcontext.ServerPruefungLinearitaetSteigend Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query5
                        dbcontext.ServerPruefungLinearitaetSteigend.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query6 = From a In dbcontext.ServerPruefungRollendeLasten Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query6
                        dbcontext.ServerPruefungRollendeLasten.Remove(obj)
                    Next
                    dbcontext.SaveChanges()


                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query6 = From a In dbcontext.ServerPruefungStabilitaetGleichgewichtslage Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query6
                        dbcontext.ServerPruefungStabilitaetGleichgewichtslage.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query7 = From a In dbcontext.ServerPruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query7
                        dbcontext.ServerPruefungStaffelverfahrenErsatzlast.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                'aufräumen und alte löschen

                Try
                    Dim query8 = From a In dbcontext.ServerPruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query8
                        dbcontext.ServerPruefungStaffelverfahrenNormallast.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query9 = From a In dbcontext.ServerPruefungWiederholbarkeit Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query9
                        dbcontext.ServerPruefungWiederholbarkeit.Remove(obj)
                    Next
                    dbcontext.SaveChanges()

                Catch e As Exception
                End Try


                dbcontext.ServerEichprotokoll.Remove(TargetObject.ServerEichprotokoll)
                dbcontext.SaveChanges()
                dbcontext.ServerBeschaffenheitspruefung.Remove(TargetObject.ServerBeschaffenheitspruefung)
                dbcontext.SaveChanges()
                dbcontext.ServerKompatiblitaetsnachweis.Remove(TargetObject.ServerKompatiblitaetsnachweis)
                dbcontext.SaveChanges()
            End Using
        Catch ex As Exception
        End Try
    End Sub

    '    ''' <summary>
    '    ''' behält alle IDS bei um ein Update zu ermöglichen
    '    ''' </summary>
    '    ''' <param name="TargetObject"></param>
    '    ''' <param name="SourceObject"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Shared Function CopyObjectPropertiesWithOwnIDs(ByRef TargetObject As ServerEichprozess, ByRef SourceObject As ServerEichprozess)
    '        'eichprozess
    '        TargetObject.Ausgeblendet = SourceObject.Ausgeblendet
    '        TargetObject.FK_Auswertegeraet = SourceObject.FK_Auswertegeraet
    '        TargetObject.FK_Vorgangsstatus = SourceObject.FK_Vorgangsstatus
    '        TargetObject.FK_WaagenArt = SourceObject.FK_WaagenArt
    '        TargetObject.FK_WaagenTyp = SourceObject.FK_WaagenTyp
    '        TargetObject.FK_Waegezelle = SourceObject.FK_Waegezelle
    '        TargetObject.Vorgangsnummer = SourceObject.Vorgangsnummer
    '        TargetObject.FK_Bearbeitungsstatus = SourceObject.FK_Bearbeitungsstatus
    '        TargetObject.UploadFilePath = SourceObject.UploadFilePath

    '        'kompatiblitätsnachweis
    '        '  TargetObject.Kompatiblitaetsnachweis.ID = SourceObject._ServerKompatiblitaetsnachweis.ID

    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Hersteller
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Ort
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Strasse
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
    '        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

    '        'beschaffenheitsprüfung

    '        TargetObject.Beschaffenheitspruefung.AWG_Auslieferungszustand = SourceObject._ServerBeschaffenheitspruefung.AWG_Auslieferungszustand
    '        TargetObject.Beschaffenheitspruefung.AWG_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.AWG_KabelUnbeschaedigt
    '        TargetObject.Beschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = SourceObject._ServerBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
    '        TargetObject.Beschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
    '        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
    '        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt
    '        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
    '        TargetObject.Beschaffenheitspruefung.Waegebruecke_Korrosionsfrei = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei
    '        TargetObject.Beschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
    '        TargetObject.Beschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
    '        TargetObject.Beschaffenheitspruefung.WZ_AnschraubplattenEben = SourceObject._ServerBeschaffenheitspruefung.WZ_AnschraubplattenEben
    '        TargetObject.Beschaffenheitspruefung.WZ_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_KabelUnbeschaedigt
    '        TargetObject.Beschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = SourceObject._ServerBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
    '        TargetObject.Beschaffenheitspruefung.WZ_VergussUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_VergussUnbeschaedigt
    '        TargetObject.Beschaffenheitspruefung.WZ_ZulassungOIMLR60 = SourceObject._ServerBeschaffenheitspruefung.WZ_ZulassungOIMLR60


    '        'Eichprotokoll

    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
    '        TargetObject.Eichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung
    '        TargetObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft
    '        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
    '        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
    '        TargetObject.Eichprotokoll.Fallbeschleunigung_g = SourceObject._ServerEichprotokoll.Fallbeschleunigung_g
    '        TargetObject.Eichprotokoll.Fallbeschleunigung_ms2 = SourceObject._ServerEichprotokoll.Fallbeschleunigung_ms2
    '        TargetObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
    '        TargetObject.Eichprotokoll.FK_Identifikationsdaten_SuperOfficeBenutzer = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_SuperOfficeBenutzer
    '        TargetObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung
    '        TargetObject.Eichprotokoll.ID = TargetObject.FK_Eichprotokoll
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort = SourceObject._ServerEichprotokoll.Identifikationsdaten_Aufstellungsort
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Baujahr = SourceObject._ServerEichprotokoll.Identifikationsdaten_Baujahr
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Benutzer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Benutzer
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Datum = SourceObject._ServerEichprotokoll.Identifikationsdaten_Datum
    '        TargetObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch = SourceObject._ServerEichprotokoll.Identifikationsdaten_HybridMechanisch
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Min1 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min1
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Min2 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min2
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Min3 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min3
    '        TargetObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_NichtSelbsteinspielend
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Pruefer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Pruefer
    '        TargetObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_Selbsteinspielend
    '        TargetObject.Eichprotokoll.Komponenten_Eichzaehlerstand = SourceObject._ServerEichprotokoll.Komponenten_Eichzaehlerstand
    '        TargetObject.Eichprotokoll.Komponenten_Softwarestand = SourceObject._ServerEichprotokoll.Komponenten_Softwarestand
    '        TargetObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer = SourceObject._ServerEichprotokoll.Komponenten_WaegezellenFabriknummer
    '        TargetObject.Eichprotokoll.Pruefverfahren_BetragNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_BetragNormallast
    '        TargetObject.Eichprotokoll.Pruefverfahren_VolleNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_VolleNormallast
    '        TargetObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject._ServerEichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren
    '        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
    '        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
    '        TargetObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherEingerichtet
    '        TargetObject.Eichprotokoll.Sicherung_Bemerkungen = SourceObject._ServerEichprotokoll.Sicherung_Bemerkungen
    '        TargetObject.Eichprotokoll.Sicherung_BenannteStelle = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelle
    '        TargetObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelleAnzahl
    '        TargetObject.Eichprotokoll.Sicherung_CE = SourceObject._ServerEichprotokoll.Sicherung_CE
    '        TargetObject.Eichprotokoll.Sicherung_CEAnzahl = SourceObject._ServerEichprotokoll.Sicherung_CEAnzahl
    '        TargetObject.Eichprotokoll.Sicherung_DatenAusgelesen = SourceObject._ServerEichprotokoll.Sicherung_DatenAusgelesen
    '        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13 = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13
    '        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13Anzahl
    '        TargetObject.Eichprotokoll.Sicherung_EichsiegelRund = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRund
    '        TargetObject.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRundAnzahl
    '        TargetObject.Eichprotokoll.Sicherung_GruenesM = SourceObject._ServerEichprotokoll.Sicherung_GruenesM
    '        TargetObject.Eichprotokoll.Sicherung_GruenesMAnzahl = SourceObject._ServerEichprotokoll.Sicherung_GruenesMAnzahl
    '        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelocht = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelocht
    '        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
    '        TargetObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
    '        TargetObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
    '        TargetObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
    '        TargetObject.Eichprotokoll.Ueberlastanzeige_Max = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Max
    '        TargetObject.Eichprotokoll.Ueberlastanzeige_Ueberlast = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Ueberlast
    '        TargetObject.Eichprotokoll.Verwendungszweck_Automatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_Automatisch
    '        TargetObject.Eichprotokoll.Verwendungszweck_AutoTara = SourceObject._ServerEichprotokoll.Verwendungszweck_AutoTara
    '        TargetObject.Eichprotokoll.Verwendungszweck_Drucker = SourceObject._ServerEichprotokoll.Verwendungszweck_Drucker
    '        TargetObject.Eichprotokoll.Verwendungszweck_Druckertyp = SourceObject._ServerEichprotokoll.Verwendungszweck_Druckertyp
    '        TargetObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = SourceObject._ServerEichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
    '        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension
    '        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
    '        TargetObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_HalbAutomatisch
    '        TargetObject.Eichprotokoll.Verwendungszweck_HandTara = SourceObject._ServerEichprotokoll.Verwendungszweck_HandTara
    '        TargetObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = SourceObject._ServerEichprotokoll.Verwendungszweck_Nullnachfuehrung
    '        TargetObject.Eichprotokoll.Verwendungszweck_PC = SourceObject._ServerEichprotokoll.Verwendungszweck_PC
    '        TargetObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = SourceObject._ServerEichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
    '        TargetObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject._ServerEichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien



    '        Return TargetObject
    '    End Function

    '    ''' <summary>
    '    ''' Methode welche alle N:1 Verbindungen auf einen Eichprozess entfernt und mit neuen Werten neu anlegt. (Es koennen z.b. neue Pruefstaffeln eingetragen worden sein, somit ist es einfacher alles zu löschen und neu anzulegen als zu updaten)
    '    ''' </summary>
    '    ''' <param name="TargetObject"></param>
    '    ''' <remarks></remarks>
    '    Public Shared Sub UpdateForeignTables(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
    '        Using dbcontext As New EichsoftwareClientdatabaseEntities1
    '            Dim EichprotokollID As String = TargetObject.Eichprotokoll.ID

    '            'neu laden der instanz damit TRacking des Contextes aktiv ist
    '            TargetObject = (From d In dbcontext.Eichprozess.Include("Eichprotokoll") Where d.ID = EichprotokollID Select d).FirstOrDefault

    '            'prüfungen
    '            Try
    '                'aufräumen und alte löschen
    '                Dim query = From a In dbcontext.PruefungAnsprechvermoegen Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query
    '                    dbcontext.PruefungAnsprechvermoegen.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()


    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen
    '                    Dim targeto = New PruefungAnsprechvermoegen
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last
    '                    targeto.Last1d = sourceo._Last1d
    '                    targeto.LastL = sourceo._LastL
    '                    targeto.Ziffernsprung = sourceo._Ziffernsprung
    '                    TargetObject.Eichprotokoll.PruefungAnsprechvermoegen.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()



    '            Try
    '                'aufräumen und alte löschen
    '                Dim query2 = From a In dbcontext.PruefungAussermittigeBelastung Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query2
    '                    dbcontext.PruefungAussermittigeBelastung.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung
    '                    Dim targeto = New PruefungAussermittigeBelastung
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.Belastungsort = sourceo._Belastungsort
    '                    targeto.Bereich = sourceo._Bereich
    '                    targeto.EFG = sourceo._EFG
    '                    targeto.EFGExtra = sourceo._EFGExtra
    '                    targeto.Fehler = sourceo._Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last

    '                    TargetObject.Eichprotokoll.PruefungAussermittigeBelastung.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()


    '            'Try
    '            '    'aufräumen und alte löschen
    '            '    Dim query3 = From a In dbcontext.PruefungEichfehlergrenzen Where a.FK_Eichprotokoll = EichprotokollID
    '            '    For Each obj In query3
    '            '        dbcontext.PruefungEichfehlergrenzen.Remove(obj)
    '            '    Next
    '            '    dbcontext.SaveChanges()
    '            '    For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungEichfehlergrenzen
    '            '        Dim targeto = New PruefungEichfehlergrenzen
    '            '        targeto.Bis = sourceo._Bis
    '            '        targeto.EFG = sourceo._EFG
    '            '        targeto.Elemente = sourceo._Elemente
    '            '        targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '            '        targeto.Messbereich = sourceo._Messbereich
    '            '        targeto.VFG = sourceo._VFG
    '            '        targeto.Von = sourceo._Von

    '            '        TargetObject.Eichprotokoll.PruefungEichfehlergrenzen.Add(targeto)
    '            '    Next
    '            'Catch e As Exception
    '            'End Try
    '            'dbcontext.SaveChanges()

    '            'aufräumen und alte löschen

    '            Try
    '                Dim query4 = From a In dbcontext.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query4
    '                    dbcontext.PruefungLinearitaetFallend.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend
    '                    Dim targeto = New PruefungLinearitaetFallend
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.Messpunkt = sourceo._Messpunkt
    '                    targeto.Bereich = sourceo._Bereich
    '                    targeto.EFG = sourceo._EFG
    '                    targeto.Fehler = sourceo._Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last

    '                    TargetObject.Eichprotokoll.PruefungLinearitaetFallend.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            Try
    '                'aufräumen und alte löschen
    '                Dim query5 = From a In dbcontext.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query5
    '                    dbcontext.PruefungLinearitaetSteigend.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend
    '                    Dim targeto = New PruefungLinearitaetSteigend
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.Messpunkt = sourceo._Messpunkt
    '                    targeto.Bereich = sourceo._Bereich
    '                    targeto.EFG = sourceo._EFG
    '                    targeto.Fehler = sourceo._Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last
    '                    TargetObject.Eichprotokoll.PruefungLinearitaetSteigend.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            Try

    '                'aufräumen und alte löschen
    '                Dim query6 = From a In dbcontext.PruefungRollendeLasten Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query6
    '                    dbcontext.PruefungRollendeLasten.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungRollendeLasten
    '                    Dim targeto = New PruefungRollendeLasten
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.AuffahrtSeite = sourceo._AuffahrtSeite
    '                    targeto.Belastungsstelle = sourceo._Belastungsstelle
    '                    targeto.EFG = sourceo._EFG
    '                    targeto.EFGExtra = sourceo._EFGExtra
    '                    targeto.Fehler = sourceo._Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last
    '                    TargetObject.Eichprotokoll.PruefungRollendeLasten.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            Try
    '                'aufräumen und alte löschen
    '                Dim query6 = From a In dbcontext.PruefungStabilitaetGleichgewichtslage Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query6
    '                    dbcontext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage
    '                    Dim targeto = New PruefungStabilitaetGleichgewichtslage
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.AbdruckOK = sourceo._AbdruckOK
    '                    targeto.Durchlauf = sourceo._Durchlauf
    '                    targeto.MAX = sourceo._MAX
    '                    targeto.MIN = sourceo._MIN
    '                    targeto.Last = sourceo._Last
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    TargetObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            Try
    '                'aufräumen und alte löschen
    '                Dim query7 = From a In dbcontext.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query7
    '                    dbcontext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast
    '                    Dim targeto = New PruefungStaffelverfahrenErsatzlast
    '                    targeto.Bereich = sourceo._Bereich
    '                    targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
    '                    targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
    '                    targeto.Ersatzlast_Ist = sourceo._Ersatzlast_Ist
    '                    targeto.Ersatzlast_Soll = sourceo._Ersatzlast_Soll
    '                    targeto.Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist
    '                    targeto.Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll
    '                    targeto.ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist
    '                    targeto.ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
    '                    targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
    '                    targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
    '                    targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
    '                    targeto.Staffel = sourceo._Staffel
    '                    targeto.ZusaetzlicheErsatzlast_Soll = sourceo._ZusaetzlicheErsatzlast_Soll

    '                    TargetObject.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(targeto)

    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            'aufräumen und alte löschen

    '            Try
    '                Dim query8 = From a In dbcontext.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query8
    '                    dbcontext.PruefungStaffelverfahrenNormallast.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()
    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast
    '                    Dim targeto = New PruefungStaffelverfahrenNormallast
    '                    targeto.Bereich = sourceo._Bereich
    '                    targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
    '                    targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
    '                    targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
    '                    targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
    '                    targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
    '                    targeto.NormalLast_Anzeige_1 = sourceo._NormalLast_Anzeige_1
    '                    targeto.NormalLast_Anzeige_2 = sourceo._NormalLast_Anzeige_2
    '                    targeto.NormalLast_Anzeige_3 = sourceo._NormalLast_Anzeige_3
    '                    targeto.NormalLast_Anzeige_4 = sourceo._NormalLast_Anzeige_4
    '                    targeto.NormalLast_EFG_1 = sourceo._NormalLast_EFG_1
    '                    targeto.NormalLast_EFG_2 = sourceo._NormalLast_EFG_2
    '                    targeto.NormalLast_EFG_3 = sourceo._NormalLast_EFG_3
    '                    targeto.NormalLast_EFG_4 = sourceo._NormalLast_EFG_4
    '                    targeto.NormalLast_Fehler_1 = sourceo._NormalLast_Fehler_1
    '                    targeto.NormalLast_Fehler_2 = sourceo._NormalLast_Fehler_2
    '                    targeto.NormalLast_Fehler_3 = sourceo._NormalLast_Fehler_3
    '                    targeto.NormalLast_Fehler_4 = sourceo._NormalLast_Fehler_1
    '                    targeto.NormalLast_Last_1 = sourceo._NormalLast_Last_1
    '                    targeto.NormalLast_Last_2 = sourceo._NormalLast_Last_2
    '                    targeto.NormalLast_Last_3 = sourceo._NormalLast_Last_3
    '                    targeto.NormalLast_Last_4 = sourceo._NormalLast_Last_4
    '                    targeto.Staffel = sourceo._Staffel

    '                    TargetObject.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(targeto)

    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()

    '            Try
    '                'aufräumen und alte löschen
    '                Dim query9 = From a In dbcontext.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = EichprotokollID
    '                For Each obj In query9
    '                    dbcontext.PruefungWiederholbarkeit.Remove(obj)
    '                Next
    '                dbcontext.SaveChanges()

    '                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit
    '                    Dim targeto = New PruefungWiederholbarkeit
    '                    targeto.Anzeige = sourceo._Anzeige
    '                    targeto.Belastung = sourceo._Belastung
    '                    targeto.Wiederholung = sourceo._Wiederholung
    '                    targeto.EFG = sourceo._EFG
    '                    targeto.EFG_Extra = sourceo._EFG_Extra
    '                    targeto.Fehler = sourceo._Fehler
    '                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
    '                    targeto.Last = sourceo._Last

    '                    TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
    '                Next
    '            Catch e As Exception
    '            End Try
    '            dbcontext.SaveChanges()
    '        End Using
    '    End Sub



End Class
