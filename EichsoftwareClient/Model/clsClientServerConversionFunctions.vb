Imports System.Runtime.Serialization
Imports System.IO

''' <summary>
''' Diese Klasse enthält funktionen um Entity Framework Objekte von der lokalen SQL Kompakt in Server Objekte und umgegekeht umwandeln zu können
''' Die Lokale Datenbank ist der Serverdatenbank sehr ähnlich, aber eben nicht gleich
''' </summary>
''' <remarks></remarks>
Public Class clsClientServerConversionFunctions
    Public Enum enuModus
        ClientSendetAnRhewa = 0
        RHEWASendetAnClient = 1
    End Enum

    Public Shared Sub CopyWZObjectProperties(ByRef TargetObject As EichsoftwareWebservice.ServerLookup_Waegezelle, ByRef SourceObject As Lookup_Waegezelle)
        If SourceObject.Neu Then
            TargetObject._Bauartzulassung = SourceObject.Bauartzulassung
            TargetObject._BruchteilEichfehlergrenze = SourceObject.BruchteilEichfehlergrenze
            TargetObject._Deaktiviert = True
            TargetObject._ErstellDatum = Date.Now
            TargetObject._Genauigkeitsklasse = SourceObject.Genauigkeitsklasse
            TargetObject._GrenzwertTemperaturbereichMAX = SourceObject.GrenzwertTemperaturbereichMAX
            TargetObject._GrenzwertTemperaturbereichMIN = SourceObject.GrenzwertTemperaturbereichMIN
            TargetObject._Hersteller = SourceObject.Hersteller
            TargetObject._Hoechsteteilungsfaktor = SourceObject.Hoechsteteilungsfaktor
            TargetObject._ID = SourceObject.ID
            TargetObject._Kriechteilungsfaktor = SourceObject.Kriechteilungsfaktor
            TargetObject._MaxAnzahlTeilungswerte = SourceObject.MaxAnzahlTeilungswerte
            TargetObject._Mindestvorlast = SourceObject.Mindestvorlast
            TargetObject._Neu = SourceObject.Neu
            TargetObject._Pruefbericht = SourceObject.Pruefbericht
            TargetObject._Revisionsnummer = SourceObject.Revisionsnummer
            TargetObject._RueckkehrVorlastsignal = SourceObject.RueckkehrVorlastsignal
            TargetObject._Typ = SourceObject.Typ
            TargetObject._MinTeilungswert = SourceObject.MinTeilungswert

            TargetObject._Waegezellenkennwert = SourceObject.Waegezellenkennwert
            TargetObject._WiderstandWaegezelle = SourceObject.WiderstandWaegezelle
        End If
    End Sub

    ''' <summary>
    ''' Schreibt werte von einem lokalen Eichprozess in ein Server Object
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyObjectProperties(ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess, ByVal pModus As enuModus)
        'eichprozess
        TargetObject._Ausgeblendet = SourceObject.Ausgeblendet
        TargetObject._FK_Auswertegeraet = SourceObject.FK_Auswertegeraet
        TargetObject._FK_Beschaffenheitspruefung = SourceObject.FK_Beschaffenheitspruefung
        TargetObject._FK_Eichprotokoll = SourceObject.FK_Eichprotokoll
        TargetObject._FK_Kompatibilitaetsnachweis = SourceObject.FK_Kompatibilitaetsnachweis
        TargetObject._FK_Vorgangsstatus = SourceObject.FK_Vorgangsstatus
        TargetObject._FK_WaagenArt = SourceObject.FK_WaagenArt
        TargetObject._FK_WaagenTyp = SourceObject.FK_WaagenTyp
        TargetObject._FK_Bearbeitungsstatus = SourceObject.FK_Bearbeitungsstatus
        TargetObject._UploadFilePath = SourceObject.UploadFilePath


        If Not SourceObject.Lookup_Waegezelle.Neu Then
            TargetObject._FK_Waegezelle = SourceObject.FK_Waegezelle

        Else
            'anlegen
        End If
        ' TargetObject._ID = SourceObject.ID
        TargetObject._Vorgangsnummer = SourceObject.Vorgangsnummer

        'kompatiblitätsnachweis
        '  TargetObject._ServerKompatiblitaetsnachweis.ID = SourceObject.Kompatiblitaetsnachweis.ID
        TargetObject._ServerKompatiblitaetsnachweis = New EichsoftwareWebservice.ServerKompatiblitaetsnachweis
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Hersteller = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Ort = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Strasse = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
        TargetObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast


        'beschaffenheitsprüfung

        TargetObject._ServerBeschaffenheitspruefung = New EichsoftwareWebservice.ServerBeschaffenheitspruefung
        TargetObject._ServerBeschaffenheitspruefung.AWG_Auslieferungszustand = SourceObject.Beschaffenheitspruefung.AWG_Auslieferungszustand
        TargetObject._ServerBeschaffenheitspruefung.AWG_KabelUnbeschaedigt = SourceObject.Beschaffenheitspruefung.AWG_KabelUnbeschaedigt
        TargetObject._ServerBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = SourceObject.Beschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
        TargetObject._ServerBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = SourceObject.Beschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
        TargetObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = SourceObject.Beschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
        TargetObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = SourceObject.Beschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt
        TargetObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = SourceObject.Beschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
        TargetObject._ServerBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei = SourceObject.Beschaffenheitspruefung.Waegebruecke_Korrosionsfrei
        TargetObject._ServerBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = SourceObject.Beschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
        TargetObject._ServerBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = SourceObject.Beschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
        TargetObject._ServerBeschaffenheitspruefung.WZ_AnschraubplattenEben = SourceObject.Beschaffenheitspruefung.WZ_AnschraubplattenEben
        TargetObject._ServerBeschaffenheitspruefung.WZ_KabelUnbeschaedigt = SourceObject.Beschaffenheitspruefung.WZ_KabelUnbeschaedigt
        TargetObject._ServerBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = SourceObject.Beschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
        TargetObject._ServerBeschaffenheitspruefung.WZ_VergussUnbeschaedigt = SourceObject.Beschaffenheitspruefung.WZ_VergussUnbeschaedigt
        TargetObject._ServerBeschaffenheitspruefung.WZ_ZulassungOIMLR60 = SourceObject.Beschaffenheitspruefung.WZ_ZulassungOIMLR60


        'Eichprotokoll
        TargetObject._ServerEichprotokoll = New EichsoftwareWebservice.ServerEichprotokoll

        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden = SourceObject.Eichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        TargetObject._ServerEichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung
        TargetObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft
        TargetObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        TargetObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        TargetObject._ServerEichprotokoll.Fallbeschleunigung_g = SourceObject.Eichprotokoll.Fallbeschleunigung_g
        TargetObject._ServerEichprotokoll.Fallbeschleunigung_ms2 = SourceObject.Eichprotokoll.Fallbeschleunigung_ms2
        TargetObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Benutzer = SourceObject.Eichprotokoll.Identifikationsdaten_Benutzer
        TargetObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Aufstellungsort = SourceObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Baujahr = SourceObject.Eichprotokoll.Identifikationsdaten_Baujahr
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Datum = SourceObject.Eichprotokoll.Identifikationsdaten_Datum
        TargetObject._ServerEichprotokoll.Identifikationsdaten_HybridMechanisch = SourceObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Min1 = SourceObject.Eichprotokoll.Identifikationsdaten_Min1
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Min2 = SourceObject.Eichprotokoll.Identifikationsdaten_Min2
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Min3 = SourceObject.Eichprotokoll.Identifikationsdaten_Min3
        TargetObject._ServerEichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = SourceObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Pruefer = SourceObject.Eichprotokoll.Identifikationsdaten_Pruefer
        TargetObject._ServerEichprotokoll.Identifikationsdaten_Selbsteinspielend = SourceObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend
        TargetObject._ServerEichprotokoll.Komponenten_Eichzaehlerstand = SourceObject.Eichprotokoll.Komponenten_Eichzaehlerstand
        TargetObject._ServerEichprotokoll.Komponenten_Softwarestand = SourceObject.Eichprotokoll.Komponenten_Softwarestand
        TargetObject._ServerEichprotokoll.Komponenten_WaegezellenFabriknummer = SourceObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer
        TargetObject._ServerEichprotokoll.Pruefverfahren_BetragNormallast = SourceObject.Eichprotokoll.Pruefverfahren_BetragNormallast
        TargetObject._ServerEichprotokoll.Pruefverfahren_VolleNormallast = SourceObject.Eichprotokoll.Pruefverfahren_VolleNormallast
        TargetObject._ServerEichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren
        TargetObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
        TargetObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
        TargetObject._ServerEichprotokoll.Sicherung_AlibispeicherEingerichtet = SourceObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet
        TargetObject._ServerEichprotokoll.Sicherung_Bemerkungen = SourceObject.Eichprotokoll.Sicherung_Bemerkungen
        TargetObject._ServerEichprotokoll.Sicherung_BenannteStelle = SourceObject.Eichprotokoll.Sicherung_BenannteStelle
        TargetObject._ServerEichprotokoll.Sicherung_BenannteStelleAnzahl = SourceObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl
        TargetObject._ServerEichprotokoll.Sicherung_CE = SourceObject.Eichprotokoll.Sicherung_CE
        TargetObject._ServerEichprotokoll.Sicherung_CEAnzahl = SourceObject.Eichprotokoll.Sicherung_CEAnzahl
        TargetObject._ServerEichprotokoll.Sicherung_DatenAusgelesen = SourceObject.Eichprotokoll.Sicherung_DatenAusgelesen
        TargetObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13 = SourceObject.Eichprotokoll.Sicherung_Eichsiegel13x13
        TargetObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13Anzahl = SourceObject.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl
        TargetObject._ServerEichprotokoll.Sicherung_EichsiegelRund = SourceObject.Eichprotokoll.Sicherung_EichsiegelRund
        TargetObject._ServerEichprotokoll.Sicherung_EichsiegelRundAnzahl = SourceObject.Eichprotokoll.Sicherung_EichsiegelRundAnzahl
        TargetObject._ServerEichprotokoll.Sicherung_GruenesM = SourceObject.Eichprotokoll.Sicherung_GruenesM
        TargetObject._ServerEichprotokoll.Sicherung_GruenesMAnzahl = SourceObject.Eichprotokoll.Sicherung_GruenesMAnzahl
        TargetObject._ServerEichprotokoll.Sicherung_HinweismarkeGelocht = SourceObject.Eichprotokoll.Sicherung_HinweismarkeGelocht
        TargetObject._ServerEichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = SourceObject.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
        TargetObject._ServerEichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
        TargetObject._ServerEichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = SourceObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
        TargetObject._ServerEichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
        TargetObject._ServerEichprotokoll.Ueberlastanzeige_Max = SourceObject.Eichprotokoll.Ueberlastanzeige_Max
        TargetObject._ServerEichprotokoll.Ueberlastanzeige_Ueberlast = SourceObject.Eichprotokoll.Ueberlastanzeige_Ueberlast
        TargetObject._ServerEichprotokoll.Verwendungszweck_Automatisch = SourceObject.Eichprotokoll.Verwendungszweck_Automatisch
        TargetObject._ServerEichprotokoll.Verwendungszweck_AutoTara = SourceObject.Eichprotokoll.Verwendungszweck_AutoTara
        TargetObject._ServerEichprotokoll.Verwendungszweck_Drucker = SourceObject.Eichprotokoll.Verwendungszweck_Drucker
        TargetObject._ServerEichprotokoll.Verwendungszweck_Druckertyp = SourceObject.Eichprotokoll.Verwendungszweck_Druckertyp
        TargetObject._ServerEichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = SourceObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
        TargetObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension
        TargetObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
        TargetObject._ServerEichprotokoll.Verwendungszweck_HalbAutomatisch = SourceObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch
        TargetObject._ServerEichprotokoll.Verwendungszweck_HandTara = SourceObject.Eichprotokoll.Verwendungszweck_HandTara
        TargetObject._ServerEichprotokoll.Verwendungszweck_Nullnachfuehrung = SourceObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung
        TargetObject._ServerEichprotokoll.Verwendungszweck_PC = SourceObject.Eichprotokoll.Verwendungszweck_PC
        TargetObject._ServerEichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = SourceObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
        TargetObject._ServerEichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien

        'prüfungen übertragen. je nach Modus aus Client SDF laden oder aus Speicher laden (Serverobjekt)
        UeberschreibePruefungen(pModus, TargetObject, SourceObject)

    

        Return TargetObject
    End Function

    ''' <summary>
    ''' wird genutzt im vom Server ein Element zu lesen. Der Nachteil ist, es kann nicht gespeichert werden
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyObjectPropertiesWithAllLookups(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        ' TargetObject.ID = SourceObject._ID
        TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath

        'kompatiblitätsnachweis
        '  TargetObject.Kompatiblitaetsnachweis.ID = SourceObject._ServerKompatiblitaetsnachweis.ID

        TargetObject.Kompatiblitaetsnachweis = New Kompatiblitaetsnachweis
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Hersteller
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Ort
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Strasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

        'beschaffenheitsprüfung
        TargetObject.Beschaffenheitspruefung = New Beschaffenheitspruefung

        TargetObject.Beschaffenheitspruefung.AWG_Auslieferungszustand = SourceObject._ServerBeschaffenheitspruefung.AWG_Auslieferungszustand
        TargetObject.Beschaffenheitspruefung.AWG_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.AWG_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = SourceObject._ServerBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
        TargetObject.Beschaffenheitspruefung.ID = SourceObject._ServerBeschaffenheitspruefung.ID
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_Korrosionsfrei = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
        TargetObject.Beschaffenheitspruefung.WZ_AnschraubplattenEben = SourceObject._ServerBeschaffenheitspruefung.WZ_AnschraubplattenEben
        TargetObject.Beschaffenheitspruefung.WZ_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = SourceObject._ServerBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
        TargetObject.Beschaffenheitspruefung.WZ_VergussUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_VergussUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_ZulassungOIMLR60 = SourceObject._ServerBeschaffenheitspruefung.WZ_ZulassungOIMLR60


        'Eichprotokoll
        TargetObject.Eichprotokoll = New Eichprotokoll

        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        TargetObject.Eichprotokoll.Fallbeschleunigung_g = SourceObject._ServerEichprotokoll.Fallbeschleunigung_g
        TargetObject.Eichprotokoll.Fallbeschleunigung_ms2 = SourceObject._ServerEichprotokoll.Fallbeschleunigung_ms2
        TargetObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
        TargetObject.Eichprotokoll.Identifikationsdaten_Benutzer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Benutzer
        TargetObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung
        TargetObject.Eichprotokoll.ID = SourceObject._ServerEichprotokoll.ID
        TargetObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort = SourceObject._ServerEichprotokoll.Identifikationsdaten_Aufstellungsort
        TargetObject.Eichprotokoll.Identifikationsdaten_Baujahr = SourceObject._ServerEichprotokoll.Identifikationsdaten_Baujahr
        TargetObject.Eichprotokoll.Identifikationsdaten_Datum = SourceObject._ServerEichprotokoll.Identifikationsdaten_Datum
        TargetObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch = SourceObject._ServerEichprotokoll.Identifikationsdaten_HybridMechanisch
        TargetObject.Eichprotokoll.Identifikationsdaten_Min1 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min1
        TargetObject.Eichprotokoll.Identifikationsdaten_Min2 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min2
        TargetObject.Eichprotokoll.Identifikationsdaten_Min3 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min3
        TargetObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_NichtSelbsteinspielend
        TargetObject.Eichprotokoll.Identifikationsdaten_Pruefer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Pruefer
        TargetObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_Selbsteinspielend
        TargetObject.Eichprotokoll.Komponenten_Eichzaehlerstand = SourceObject._ServerEichprotokoll.Komponenten_Eichzaehlerstand
        TargetObject.Eichprotokoll.Komponenten_Softwarestand = SourceObject._ServerEichprotokoll.Komponenten_Softwarestand
        TargetObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer = SourceObject._ServerEichprotokoll.Komponenten_WaegezellenFabriknummer
        TargetObject.Eichprotokoll.Pruefverfahren_BetragNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_BetragNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VolleNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_VolleNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject._ServerEichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherEingerichtet
        TargetObject.Eichprotokoll.Sicherung_Bemerkungen = SourceObject._ServerEichprotokoll.Sicherung_Bemerkungen
        TargetObject.Eichprotokoll.Sicherung_BenannteStelle = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelle
        TargetObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelleAnzahl
        TargetObject.Eichprotokoll.Sicherung_CE = SourceObject._ServerEichprotokoll.Sicherung_CE
        TargetObject.Eichprotokoll.Sicherung_CEAnzahl = SourceObject._ServerEichprotokoll.Sicherung_CEAnzahl
        TargetObject.Eichprotokoll.Sicherung_DatenAusgelesen = SourceObject._ServerEichprotokoll.Sicherung_DatenAusgelesen
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13 = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13Anzahl
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRund = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRund
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRundAnzahl
        TargetObject.Eichprotokoll.Sicherung_GruenesM = SourceObject._ServerEichprotokoll.Sicherung_GruenesM
        TargetObject.Eichprotokoll.Sicherung_GruenesMAnzahl = SourceObject._ServerEichprotokoll.Sicherung_GruenesMAnzahl
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelocht = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelocht
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
        TargetObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
        TargetObject.Eichprotokoll.Ueberlastanzeige_Max = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Max
        TargetObject.Eichprotokoll.Ueberlastanzeige_Ueberlast = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Ueberlast
        TargetObject.Eichprotokoll.Verwendungszweck_Automatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_Automatisch
        TargetObject.Eichprotokoll.Verwendungszweck_AutoTara = SourceObject._ServerEichprotokoll.Verwendungszweck_AutoTara
        TargetObject.Eichprotokoll.Verwendungszweck_Drucker = SourceObject._ServerEichprotokoll.Verwendungszweck_Drucker
        TargetObject.Eichprotokoll.Verwendungszweck_Druckertyp = SourceObject._ServerEichprotokoll.Verwendungszweck_Druckertyp
        TargetObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = SourceObject._ServerEichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
        TargetObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_HalbAutomatisch
        TargetObject.Eichprotokoll.Verwendungszweck_HandTara = SourceObject._ServerEichprotokoll.Verwendungszweck_HandTara
        TargetObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = SourceObject._ServerEichprotokoll.Verwendungszweck_Nullnachfuehrung
        TargetObject.Eichprotokoll.Verwendungszweck_PC = SourceObject._ServerEichprotokoll.Verwendungszweck_PC
        TargetObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = SourceObject._ServerEichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
        TargetObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject._ServerEichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien

        'verfahren
        'TargetObject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = New Lookup_Konformitaetsbewertungsverfahren
        'TargetObject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.ID = SourceObject._ServerEichprotokoll.ServerLookup_Konformitaetsbewertungsverfahren._ID
        'TargetObject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren = SourceObject._ServerEichprotokoll.ServerLookup_Konformitaetsbewertungsverfahren._Verfahren
        'TargetObject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren_EN = SourceObject._ServerEichprotokoll.ServerLookup_Konformitaetsbewertungsverfahren._Verfahren_EN
        'TargetObject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren_PL = SourceObject._ServerEichprotokoll.ServerLookup_Konformitaetsbewertungsverfahren._Verfahren_PL

        'prüfungen
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen
                Dim targeto = New PruefungAnsprechvermoegen
                targeto.Anzeige = sourceo._Anzeige
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                targeto.Last1d = sourceo._Last1d
                targeto.LastL = sourceo._LastL
                targeto.Ziffernsprung = sourceo._Ziffernsprung
                TargetObject.Eichprotokoll.PruefungAnsprechvermoegen.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung
                Dim targeto = New PruefungAussermittigeBelastung
                targeto.Anzeige = sourceo._Anzeige
                targeto.Belastungsort = sourceo._Belastungsort
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.EFGExtra = sourceo._EFGExtra
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungAussermittigeBelastung.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try



        'TargetObject.ServerEichprotokoll.ServerPruefungLinearitaetFallend = SourceObject._ServerEichprotokoll.PruefungLinearitaetFallend
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend
                Dim targeto = New PruefungLinearitaetFallend
                targeto.Anzeige = sourceo._Anzeige
                targeto.Messpunkt = sourceo._Messpunkt
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungLinearitaetFallend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungLinearitaetSteigend = SourceObject._ServerEichprotokoll.PruefungLinearitaetSteigend
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend
                Dim targeto = New PruefungLinearitaetSteigend
                targeto.Anzeige = sourceo._Anzeige
                targeto.Messpunkt = sourceo._Messpunkt
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                TargetObject.Eichprotokoll.PruefungLinearitaetSteigend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        'TargetObject.ServerEichprotokoll.ServerPruefungRollendeLasten = SourceObject._ServerEichprotokoll.PruefungRollendeLasten
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungRollendeLasten
                Dim targeto = New PruefungRollendeLasten
                targeto.Anzeige = sourceo._Anzeige
                targeto.AuffahrtSeite = sourceo._AuffahrtSeite
                targeto.Belastungsstelle = sourceo._Belastungsstelle
                targeto.EFG = sourceo._EFG
                targeto.EFGExtra = sourceo._EFGExtra
                targeto.Fehler = sourceo._Fehler
                targeto.Last = sourceo._Last
                targeto.ID = sourceo._ID
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                TargetObject.Eichprotokoll.PruefungRollendeLasten.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage = SourceObject._ServerEichprotokoll.PruefungStabilitaetGleichgewichtslage
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage
                Dim targeto = New PruefungStabilitaetGleichgewichtslage
                targeto.Anzeige = sourceo._Anzeige
                targeto.AbdruckOK = sourceo._AbdruckOK
                targeto.Durchlauf = sourceo._Durchlauf
                targeto.MAX = sourceo._MAX
                targeto.MIN = sourceo._MIN
                targeto.Last = sourceo._Last
                targeto.ID = sourceo._ID
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                TargetObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast = SourceObject._ServerEichprotokoll.PruefungStaffelverfahrenErsatzlast
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast
                Dim targeto = New PruefungStaffelverfahrenErsatzlast
                targeto.Bereich = sourceo._Bereich
                targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                targeto.Ersatzlast_Ist = sourceo._Ersatzlast_Ist
                targeto.Ersatzlast_Soll = sourceo._Ersatzlast_Soll
                targeto.Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist
                targeto.Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll
                targeto.ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist
                targeto.ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll
                targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                targeto.Staffel = sourceo._Staffel
                targeto.ID = sourceo._ID
                targeto.ZusaetzlicheErsatzlast_Soll = sourceo._ZusaetzlicheErsatzlast_Soll
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                TargetObject.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try



        'TargetObject.ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast = SourceObject._ServerEichprotokoll.PruefungStaffelverfahrenNormallast
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast
                Dim targeto = New PruefungStaffelverfahrenNormallast
                targeto.Bereich = sourceo._Bereich
                targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                targeto.NormalLast_Anzeige_1 = sourceo._NormalLast_Anzeige_1
                targeto.NormalLast_Anzeige_2 = sourceo._NormalLast_Anzeige_2
                targeto.NormalLast_Anzeige_3 = sourceo._NormalLast_Anzeige_3
                targeto.NormalLast_Anzeige_4 = sourceo._NormalLast_Anzeige_4
                targeto.NormalLast_EFG_1 = sourceo._NormalLast_EFG_1
                targeto.NormalLast_EFG_2 = sourceo._NormalLast_EFG_2
                targeto.NormalLast_EFG_3 = sourceo._NormalLast_EFG_3
                targeto.NormalLast_EFG_4 = sourceo._NormalLast_EFG_4
                targeto.NormalLast_Fehler_1 = sourceo._NormalLast_Fehler_1
                targeto.NormalLast_Fehler_2 = sourceo._NormalLast_Fehler_2
                targeto.NormalLast_Fehler_3 = sourceo._NormalLast_Fehler_3
                targeto.NormalLast_Fehler_4 = sourceo._NormalLast_Fehler_1
                targeto.NormalLast_Last_1 = sourceo._NormalLast_Last_1
                targeto.NormalLast_Last_2 = sourceo._NormalLast_Last_2
                targeto.NormalLast_Last_3 = sourceo._NormalLast_Last_3
                targeto.NormalLast_Last_4 = sourceo._NormalLast_Last_4
                targeto.Staffel = sourceo._Staffel
                targeto.ID = sourceo._ID
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                TargetObject.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungWiederholbarkeit = SourceObject._ServerEichprotokoll.PruefungWiederholbarkeit
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit
                Dim targeto = New PruefungWiederholbarkeit
                targeto.Anzeige = sourceo._Anzeige
                targeto.Belastung = sourceo._Belastung
                targeto.Wiederholung = sourceo._Wiederholung
                targeto.EFG = sourceo._EFG
                targeto.EFG_Extra = sourceo._EFG_Extra
                targeto.Fehler = sourceo._Fehler
                targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerMogelstatistik = SourceObject._ServerEichprotokoll.Mogelstatistik
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerMogelstatistik
                Dim targeto = New Mogelstatistik
                targeto.FK_Auswertegeraet = TargetObject.FK_Auswertegeraet
                targeto.FK_Eichprozess = TargetObject.ID
                targeto.FK_Waegezelle = TargetObject.FK_Waegezelle
                targeto.Kompatiblitaet_AnschriftWaagenbaufirma = sourceo._Kompatiblitaet_AnschriftWaagenbaufirma
                targeto.Kompatiblitaet_AWG_Anschlussart = sourceo._Kompatiblitaet_AWG_Anschlussart
                targeto.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = sourceo._Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
                targeto.Kompatiblitaet_Waage_AdditiveTarahoechstlast = sourceo._Kompatiblitaet_Waage_AdditiveTarahoechstlast
                targeto.Kompatiblitaet_Waage_AnzahlWaegezellen = sourceo._Kompatiblitaet_Waage_AnzahlWaegezellen
                targeto.Kompatiblitaet_Waage_Bauartzulassung = sourceo._Kompatiblitaet_Waage_Bauartzulassung
                targeto.Kompatiblitaet_Waage_Ecklastzuschlag = sourceo._Kompatiblitaet_Waage_Ecklastzuschlag
                targeto.Kompatiblitaet_Waage_Einschaltnullstellbereich = sourceo._Kompatiblitaet_Waage_Einschaltnullstellbereich
                targeto.Kompatiblitaet_Waage_FabrikNummer = sourceo._Kompatiblitaet_Waage_FabrikNummer
                targeto.Kompatiblitaet_Waage_Genauigkeitsklasse = sourceo._Kompatiblitaet_Waage_Genauigkeitsklasse
                targeto.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = sourceo._Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
                targeto.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = sourceo._Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2
                targeto.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3
                targeto.Kompatiblitaet_Waage_Kabellaenge = sourceo._Kompatiblitaet_Waage_Kabellaenge
                targeto.Kompatiblitaet_Waage_Kabelquerschnitt = sourceo._Kompatiblitaet_Waage_Kabelquerschnitt
                targeto.Kompatiblitaet_Waage_Revisionsnummer = sourceo._Kompatiblitaet_Waage_Revisionsnummer
                targeto.Kompatiblitaet_Waage_Totlast = sourceo._Kompatiblitaet_Waage_Totlast
                targeto.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = sourceo._Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
                targeto.Kompatiblitaet_Waage_Zulassungsinhaber = sourceo._Kompatiblitaet_Waage_Zulassungsinhaber
                targeto.Kompatiblitaet_WZ_Hoechstlast = sourceo._Kompatiblitaet_WZ_Hoechstlast

                TargetObject.Mogelstatistik.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'auswerte gerät
        TargetObject.Lookup_Auswertegeraet = New Lookup_Auswertegeraet
        TargetObject.Lookup_Auswertegeraet.Bauartzulassung = SourceObject._ServerLookup_Auswertegeraet._Bauartzulassung
        TargetObject.Lookup_Auswertegeraet.BruchteilEichfehlergrenze = SourceObject._ServerLookup_Auswertegeraet._BruchteilEichfehlergrenze
        TargetObject.Lookup_Auswertegeraet.Genauigkeitsklasse = SourceObject._ServerLookup_Auswertegeraet._Genauigkeitsklasse
        TargetObject.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX = SourceObject._ServerLookup_Auswertegeraet._GrenzwertLastwiderstandMAX
        TargetObject.Lookup_Auswertegeraet.GrenzwertLastwiderstandMIN = SourceObject._ServerLookup_Auswertegeraet._GrenzwertLastwiderstandMIN
        TargetObject.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX = SourceObject._ServerLookup_Auswertegeraet._GrenzwertTemperaturbereichMAX
        TargetObject.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN = SourceObject._ServerLookup_Auswertegeraet._GrenzwertTemperaturbereichMIN
        TargetObject.Lookup_Auswertegeraet.Hersteller = SourceObject._ServerLookup_Auswertegeraet._Hersteller
        TargetObject.Lookup_Auswertegeraet.ID = TargetObject.FK_Auswertegeraet
        TargetObject.Lookup_Auswertegeraet.KabellaengeQuerschnitt = SourceObject._ServerLookup_Auswertegeraet._KabellaengeQuerschnitt
        TargetObject.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteEinbereichswaage = SourceObject._ServerLookup_Auswertegeraet._MAXAnzahlTeilungswerteEinbereichswaage
        TargetObject.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage = SourceObject._ServerLookup_Auswertegeraet._MAXAnzahlTeilungswerteMehrbereichswaage
        TargetObject.Lookup_Auswertegeraet.Mindesteingangsspannung = SourceObject._ServerLookup_Auswertegeraet._Mindesteingangsspannung
        TargetObject.Lookup_Auswertegeraet.Mindestmesssignal = SourceObject._ServerLookup_Auswertegeraet._Mindestmesssignal
        TargetObject.Lookup_Auswertegeraet.Pruefbericht = SourceObject._ServerLookup_Auswertegeraet._Pruefbericht
        TargetObject.Lookup_Auswertegeraet.Speisespannung = SourceObject._ServerLookup_Auswertegeraet._Speisespannung
        TargetObject.Lookup_Auswertegeraet.Typ = SourceObject._ServerLookup_Auswertegeraet._Typ


        'wenn neu
        TargetObject.Lookup_Waegezelle = New Lookup_Waegezelle
        TargetObject.Lookup_Waegezelle.Bauartzulassung = SourceObject._ServerLookup_Waegezelle._Bauartzulassung
        TargetObject.Lookup_Waegezelle.BruchteilEichfehlergrenze = SourceObject._ServerLookup_Waegezelle._BruchteilEichfehlergrenze
        TargetObject.Lookup_Waegezelle.Genauigkeitsklasse = SourceObject._ServerLookup_Waegezelle._Genauigkeitsklasse
        TargetObject.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX = SourceObject._ServerLookup_Waegezelle._GrenzwertTemperaturbereichMAX
        TargetObject.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN = SourceObject._ServerLookup_Waegezelle._GrenzwertTemperaturbereichMIN
        TargetObject.Lookup_Waegezelle.Hersteller = SourceObject._ServerLookup_Waegezelle._Hersteller
        TargetObject.Lookup_Waegezelle.Hoechsteteilungsfaktor = SourceObject._ServerLookup_Waegezelle._Hoechsteteilungsfaktor
        TargetObject.Lookup_Waegezelle.ID = TargetObject.FK_Waegezelle
        TargetObject.Lookup_Waegezelle.Kriechteilungsfaktor = SourceObject._ServerLookup_Waegezelle._Kriechteilungsfaktor
        TargetObject.Lookup_Waegezelle.MaxAnzahlTeilungswerte = SourceObject._ServerLookup_Waegezelle._MaxAnzahlTeilungswerte
        TargetObject.Lookup_Waegezelle.Mindestvorlast = SourceObject._ServerLookup_Waegezelle._Mindestvorlast
        TargetObject.Lookup_Waegezelle.MinTeilungswert = SourceObject._ServerLookup_Waegezelle._MinTeilungswert
        TargetObject.Lookup_Waegezelle.Pruefbericht = SourceObject._ServerLookup_Waegezelle._Pruefbericht
        TargetObject.Lookup_Waegezelle.Revisionsnummer = SourceObject._ServerLookup_Waegezelle._Revisionsnummer
        TargetObject.Lookup_Waegezelle.RueckkehrVorlastsignal = SourceObject._ServerLookup_Waegezelle._RueckkehrVorlastsignal
        TargetObject.Lookup_Waegezelle.Typ = SourceObject._ServerLookup_Waegezelle._Typ
        TargetObject.Lookup_Waegezelle.Waegezellenkennwert = SourceObject._ServerLookup_Waegezelle._Waegezellenkennwert
        TargetObject.Lookup_Waegezelle.WiderstandWaegezelle = SourceObject._ServerLookup_Waegezelle._WiderstandWaegezelle

        Return TargetObject
    End Function

    ''' <summary>
    ''' behält alle IDS bei um ein Update zu ermöglichen
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyObjectPropertiesWithOwnIDs(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        ' TargetObject.ID = SourceObject._ID
        TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath

        'kompatiblitätsnachweis
        '  TargetObject.Kompatiblitaetsnachweis.ID = SourceObject._ServerKompatiblitaetsnachweis.ID

        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Hersteller
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Ort
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Strasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

        'beschaffenheitsprüfung

        TargetObject.Beschaffenheitspruefung.AWG_Auslieferungszustand = SourceObject._ServerBeschaffenheitspruefung.AWG_Auslieferungszustand
        TargetObject.Beschaffenheitspruefung.AWG_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.AWG_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = SourceObject._ServerBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_Korrosionsfrei = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
        TargetObject.Beschaffenheitspruefung.WZ_AnschraubplattenEben = SourceObject._ServerBeschaffenheitspruefung.WZ_AnschraubplattenEben
        TargetObject.Beschaffenheitspruefung.WZ_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = SourceObject._ServerBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
        TargetObject.Beschaffenheitspruefung.WZ_VergussUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_VergussUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_ZulassungOIMLR60 = SourceObject._ServerBeschaffenheitspruefung.WZ_ZulassungOIMLR60


        'Eichprotokoll

        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        TargetObject.Eichprotokoll.Fallbeschleunigung_g = SourceObject._ServerEichprotokoll.Fallbeschleunigung_g
        TargetObject.Eichprotokoll.Fallbeschleunigung_ms2 = SourceObject._ServerEichprotokoll.Fallbeschleunigung_ms2
        TargetObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
        TargetObject.Eichprotokoll.Identifikationsdaten_Benutzer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Benutzer
        TargetObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung
        TargetObject.Eichprotokoll.ID = TargetObject.FK_Eichprotokoll
        TargetObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort = SourceObject._ServerEichprotokoll.Identifikationsdaten_Aufstellungsort
        TargetObject.Eichprotokoll.Identifikationsdaten_Baujahr = SourceObject._ServerEichprotokoll.Identifikationsdaten_Baujahr
        TargetObject.Eichprotokoll.Identifikationsdaten_Datum = SourceObject._ServerEichprotokoll.Identifikationsdaten_Datum
        TargetObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch = SourceObject._ServerEichprotokoll.Identifikationsdaten_HybridMechanisch
        TargetObject.Eichprotokoll.Identifikationsdaten_Min1 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min1
        TargetObject.Eichprotokoll.Identifikationsdaten_Min2 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min2
        TargetObject.Eichprotokoll.Identifikationsdaten_Min3 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min3
        TargetObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_NichtSelbsteinspielend
        TargetObject.Eichprotokoll.Identifikationsdaten_Pruefer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Pruefer
        TargetObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_Selbsteinspielend
        TargetObject.Eichprotokoll.Komponenten_Eichzaehlerstand = SourceObject._ServerEichprotokoll.Komponenten_Eichzaehlerstand
        TargetObject.Eichprotokoll.Komponenten_Softwarestand = SourceObject._ServerEichprotokoll.Komponenten_Softwarestand
        TargetObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer = SourceObject._ServerEichprotokoll.Komponenten_WaegezellenFabriknummer
        TargetObject.Eichprotokoll.Pruefverfahren_BetragNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_BetragNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VolleNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_VolleNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject._ServerEichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherEingerichtet
        TargetObject.Eichprotokoll.Sicherung_Bemerkungen = SourceObject._ServerEichprotokoll.Sicherung_Bemerkungen
        TargetObject.Eichprotokoll.Sicherung_BenannteStelle = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelle
        TargetObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelleAnzahl
        TargetObject.Eichprotokoll.Sicherung_CE = SourceObject._ServerEichprotokoll.Sicherung_CE
        TargetObject.Eichprotokoll.Sicherung_CEAnzahl = SourceObject._ServerEichprotokoll.Sicherung_CEAnzahl
        TargetObject.Eichprotokoll.Sicherung_DatenAusgelesen = SourceObject._ServerEichprotokoll.Sicherung_DatenAusgelesen
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13 = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13Anzahl
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRund = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRund
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRundAnzahl
        TargetObject.Eichprotokoll.Sicherung_GruenesM = SourceObject._ServerEichprotokoll.Sicherung_GruenesM
        TargetObject.Eichprotokoll.Sicherung_GruenesMAnzahl = SourceObject._ServerEichprotokoll.Sicherung_GruenesMAnzahl
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelocht = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelocht
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
        TargetObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
        TargetObject.Eichprotokoll.Ueberlastanzeige_Max = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Max
        TargetObject.Eichprotokoll.Ueberlastanzeige_Ueberlast = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Ueberlast
        TargetObject.Eichprotokoll.Verwendungszweck_Automatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_Automatisch
        TargetObject.Eichprotokoll.Verwendungszweck_AutoTara = SourceObject._ServerEichprotokoll.Verwendungszweck_AutoTara
        TargetObject.Eichprotokoll.Verwendungszweck_Drucker = SourceObject._ServerEichprotokoll.Verwendungszweck_Drucker
        TargetObject.Eichprotokoll.Verwendungszweck_Druckertyp = SourceObject._ServerEichprotokoll.Verwendungszweck_Druckertyp
        TargetObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = SourceObject._ServerEichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
        TargetObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_HalbAutomatisch
        TargetObject.Eichprotokoll.Verwendungszweck_HandTara = SourceObject._ServerEichprotokoll.Verwendungszweck_HandTara
        TargetObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = SourceObject._ServerEichprotokoll.Verwendungszweck_Nullnachfuehrung
        TargetObject.Eichprotokoll.Verwendungszweck_PC = SourceObject._ServerEichprotokoll.Verwendungszweck_PC
        TargetObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = SourceObject._ServerEichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
        TargetObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject._ServerEichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien





        ''auswerte gerät

        Return TargetObject
    End Function

    ''' <summary>
    ''' Methode welche alle N:1 Verbindungen auf einen Eichprozess entfernt und mit neuen Werten neu anlegt. (Es koennen z.b. neue Pruefstaffeln eingetragen worden sein, somit ist es einfacher alles zu löschen und neu anzulegen als zu updaten)
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateForeignTables(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        Using dbcontext As New EichsoftwareClientdatabaseEntities1
            Dim EichprotokollID As String = TargetObject.Eichprotokoll.ID

            'neu laden der instanz damit TRacking des Contextes aktiv ist
            TargetObject = (From d In dbcontext.Eichprozess.Include("Eichprotokoll") Where d.ID = EichprotokollID Select d).FirstOrDefault

            'prüfungen
            Try
                'aufräumen und alte löschen
                Dim query = From a In dbcontext.PruefungAnsprechvermoegen Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query
                    dbcontext.PruefungAnsprechvermoegen.Remove(obj)
                Next
                dbcontext.SaveChanges()


                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen
                    Dim targeto = New PruefungAnsprechvermoegen
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last
                    targeto.Last1d = sourceo._Last1d
                    targeto.LastL = sourceo._LastL
                    targeto.Ziffernsprung = sourceo._Ziffernsprung
                    TargetObject.Eichprotokoll.PruefungAnsprechvermoegen.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()



            Try
                'aufräumen und alte löschen
                Dim query2 = From a In dbcontext.PruefungAussermittigeBelastung Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query2
                    dbcontext.PruefungAussermittigeBelastung.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung
                    Dim targeto = New PruefungAussermittigeBelastung
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.Belastungsort = sourceo._Belastungsort
                    targeto.Bereich = sourceo._Bereich
                    targeto.EFG = sourceo._EFG
                    targeto.EFGExtra = sourceo._EFGExtra
                    targeto.Fehler = sourceo._Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last

                    TargetObject.Eichprotokoll.PruefungAussermittigeBelastung.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try
                Dim query4 = From a In dbcontext.PruefungLinearitaetFallend Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query4
                    dbcontext.PruefungLinearitaetFallend.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend
                    Dim targeto = New PruefungLinearitaetFallend
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.Messpunkt = sourceo._Messpunkt
                    targeto.Bereich = sourceo._Bereich
                    targeto.EFG = sourceo._EFG
                    targeto.Fehler = sourceo._Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last

                    TargetObject.Eichprotokoll.PruefungLinearitaetFallend.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try
                'aufräumen und alte löschen
                Dim query5 = From a In dbcontext.PruefungLinearitaetSteigend Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query5
                    dbcontext.PruefungLinearitaetSteigend.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend
                    Dim targeto = New PruefungLinearitaetSteigend
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.Messpunkt = sourceo._Messpunkt
                    targeto.Bereich = sourceo._Bereich
                    targeto.EFG = sourceo._EFG
                    targeto.Fehler = sourceo._Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last
                    TargetObject.Eichprotokoll.PruefungLinearitaetSteigend.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try

                'aufräumen und alte löschen
                Dim query6 = From a In dbcontext.PruefungRollendeLasten Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query6
                    dbcontext.PruefungRollendeLasten.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungRollendeLasten
                    Dim targeto = New PruefungRollendeLasten
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.AuffahrtSeite = sourceo._AuffahrtSeite
                    targeto.Belastungsstelle = sourceo._Belastungsstelle
                    targeto.EFG = sourceo._EFG
                    targeto.EFGExtra = sourceo._EFGExtra
                    targeto.Fehler = sourceo._Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last
                    TargetObject.Eichprotokoll.PruefungRollendeLasten.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try
                'aufräumen und alte löschen
                Dim query6 = From a In dbcontext.PruefungStabilitaetGleichgewichtslage Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query6
                    dbcontext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage
                    Dim targeto = New PruefungStabilitaetGleichgewichtslage
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.AbdruckOK = sourceo._AbdruckOK
                    targeto.Durchlauf = sourceo._Durchlauf
                    targeto.MAX = sourceo._MAX
                    targeto.MIN = sourceo._MIN
                    targeto.Last = sourceo._Last
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    TargetObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try
                'aufräumen und alte löschen
                Dim query7 = From a In dbcontext.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query7
                    dbcontext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast
                    Dim targeto = New PruefungStaffelverfahrenErsatzlast
                    targeto.Bereich = sourceo._Bereich
                    targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                    targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                    targeto.Ersatzlast_Ist = sourceo._Ersatzlast_Ist
                    targeto.Ersatzlast_Soll = sourceo._Ersatzlast_Soll
                    targeto.Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist
                    targeto.Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll
                    targeto.ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist
                    targeto.ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                    targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                    targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                    targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                    targeto.Staffel = sourceo._Staffel
                    targeto.ZusaetzlicheErsatzlast_Soll = sourceo._ZusaetzlicheErsatzlast_Soll

                    TargetObject.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(targeto)

                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            'aufräumen und alte löschen

            Try
                Dim query8 = From a In dbcontext.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query8
                    dbcontext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next
                dbcontext.SaveChanges()
                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast
                    Dim targeto = New PruefungStaffelverfahrenNormallast
                    targeto.Bereich = sourceo._Bereich
                    targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                    targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                    targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                    targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                    targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                    targeto.NormalLast_Anzeige_1 = sourceo._NormalLast_Anzeige_1
                    targeto.NormalLast_Anzeige_2 = sourceo._NormalLast_Anzeige_2
                    targeto.NormalLast_Anzeige_3 = sourceo._NormalLast_Anzeige_3
                    targeto.NormalLast_Anzeige_4 = sourceo._NormalLast_Anzeige_4
                    targeto.NormalLast_EFG_1 = sourceo._NormalLast_EFG_1
                    targeto.NormalLast_EFG_2 = sourceo._NormalLast_EFG_2
                    targeto.NormalLast_EFG_3 = sourceo._NormalLast_EFG_3
                    targeto.NormalLast_EFG_4 = sourceo._NormalLast_EFG_4
                    targeto.NormalLast_Fehler_1 = sourceo._NormalLast_Fehler_1
                    targeto.NormalLast_Fehler_2 = sourceo._NormalLast_Fehler_2
                    targeto.NormalLast_Fehler_3 = sourceo._NormalLast_Fehler_3
                    targeto.NormalLast_Fehler_4 = sourceo._NormalLast_Fehler_1
                    targeto.NormalLast_Last_1 = sourceo._NormalLast_Last_1
                    targeto.NormalLast_Last_2 = sourceo._NormalLast_Last_2
                    targeto.NormalLast_Last_3 = sourceo._NormalLast_Last_3
                    targeto.NormalLast_Last_4 = sourceo._NormalLast_Last_4
                    targeto.Staffel = sourceo._Staffel

                    TargetObject.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(targeto)

                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()

            Try
                'aufräumen und alte löschen
                Dim query9 = From a In dbcontext.PruefungWiederholbarkeit Where a.FK_Eichprotokoll = EichprotokollID
                For Each obj In query9
                    dbcontext.PruefungWiederholbarkeit.Remove(obj)
                Next
                dbcontext.SaveChanges()

                For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit
                    Dim targeto = New PruefungWiederholbarkeit
                    targeto.Anzeige = sourceo._Anzeige
                    targeto.Belastung = sourceo._Belastung
                    targeto.Wiederholung = sourceo._Wiederholung
                    targeto.EFG = sourceo._EFG
                    targeto.EFG_Extra = sourceo._EFG_Extra
                    targeto.Fehler = sourceo._Fehler
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.Last = sourceo._Last

                    TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()
        End Using
    End Sub


    ''' <summary>
    ''' wird beim kopieren genutzt. überschreibt alle properties from source ins targetobject erzeugt aber neue IDS
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyObjectPropertiesWithNewIDs(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, Optional ByVal bolBehalteVorgangsnummer As Boolean = False)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Beschaffenheitspruefung = SourceObject._FK_Beschaffenheitspruefung
        TargetObject.FK_Eichprotokoll = SourceObject._FK_Eichprotokoll
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        ' TargetObject.ID = SourceObject._ID
        If bolBehalteVorgangsnummer Then
            TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        Else
            TargetObject.Vorgangsnummer = Guid.NewGuid.ToString
        End If
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath

        'kompatiblitätsnachweis


        TargetObject.Kompatiblitaetsnachweis = New Kompatiblitaetsnachweis
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Hersteller
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Ort
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Strasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
        TargetObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = SourceObject._ServerKompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast
        '   TargetObject.FK_Kompatibilitaetsnachweis = TargetObject.Kompatiblitaetsnachweis.ID



        'beschaffenheitsprüfung
        TargetObject.Beschaffenheitspruefung = New Beschaffenheitspruefung
        TargetObject.Beschaffenheitspruefung.AWG_Auslieferungszustand = SourceObject._ServerBeschaffenheitspruefung.AWG_Auslieferungszustand
        TargetObject.Beschaffenheitspruefung.AWG_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.AWG_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden = SourceObject._ServerBeschaffenheitspruefung.AWG_MetrologischeAngabenVorhanden
        'TargetObject.Beschaffenheitspruefung.ID = SourceObject._ServerBeschaffenheitspruefung.ID
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_DichtigkeitGegeben
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelNichtSproede = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelNichtSproede
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelTemperaturGeschuetzt
        TargetObject.Beschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.Verbindungselemente_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_Korrosionsfrei = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_Korrosionsfrei
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WiegeaufgabeAusgelegt
        TargetObject.Beschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene = SourceObject._ServerBeschaffenheitspruefung.Waegebruecke_WZAufnahmenInEbene
        TargetObject.Beschaffenheitspruefung.WZ_AnschraubplattenEben = SourceObject._ServerBeschaffenheitspruefung.WZ_AnschraubplattenEben
        TargetObject.Beschaffenheitspruefung.WZ_KabelUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_KabelUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC = SourceObject._ServerBeschaffenheitspruefung.WZ_KrafteinteilungKonformWELMEC
        TargetObject.Beschaffenheitspruefung.WZ_VergussUnbeschaedigt = SourceObject._ServerBeschaffenheitspruefung.WZ_VergussUnbeschaedigt
        TargetObject.Beschaffenheitspruefung.WZ_ZulassungOIMLR60 = SourceObject._ServerBeschaffenheitspruefung.WZ_ZulassungOIMLR60


        'Eichprotokoll
        TargetObject.Eichprotokoll = New Eichprotokoll
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AnzeigenAbdruckeInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufschriftenKennzeichnungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_AufstellungsbedingungenInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_KompatibilitaetsnachweisVorhanden
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_MesstechnischeMerkmaleInOrdnung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_ZulassungsunterlagenInLesbarerFassung
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        TargetObject.Eichprotokoll.Fallbeschleunigung_g = SourceObject._ServerEichprotokoll.Fallbeschleunigung_g
        TargetObject.Eichprotokoll.Fallbeschleunigung_ms2 = SourceObject._ServerEichprotokoll.Fallbeschleunigung_ms2
        TargetObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
        TargetObject.Eichprotokoll.Identifikationsdaten_Benutzer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Benutzer
        TargetObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung
        '  TargetObject.Eichprotokoll.ID = SourceObject._ServerEichprotokoll.ID
        TargetObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort = SourceObject._ServerEichprotokoll.Identifikationsdaten_Aufstellungsort
        TargetObject.Eichprotokoll.Identifikationsdaten_Baujahr = SourceObject._ServerEichprotokoll.Identifikationsdaten_Baujahr

        TargetObject.Eichprotokoll.Identifikationsdaten_Datum = SourceObject._ServerEichprotokoll.Identifikationsdaten_Datum
        TargetObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch = SourceObject._ServerEichprotokoll.Identifikationsdaten_HybridMechanisch
        TargetObject.Eichprotokoll.Identifikationsdaten_Min1 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min1
        TargetObject.Eichprotokoll.Identifikationsdaten_Min2 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min2
        TargetObject.Eichprotokoll.Identifikationsdaten_Min3 = SourceObject._ServerEichprotokoll.Identifikationsdaten_Min3
        TargetObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_NichtSelbsteinspielend
        TargetObject.Eichprotokoll.Identifikationsdaten_Pruefer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Pruefer
        TargetObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend = SourceObject._ServerEichprotokoll.Identifikationsdaten_Selbsteinspielend
        TargetObject.Eichprotokoll.Komponenten_Eichzaehlerstand = SourceObject._ServerEichprotokoll.Komponenten_Eichzaehlerstand
        TargetObject.Eichprotokoll.Komponenten_Softwarestand = SourceObject._ServerEichprotokoll.Komponenten_Softwarestand
        TargetObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer = SourceObject._ServerEichprotokoll.Komponenten_WaegezellenFabriknummer
        TargetObject.Eichprotokoll.Pruefverfahren_BetragNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_BetragNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VolleNormallast = SourceObject._ServerEichprotokoll.Pruefverfahren_VolleNormallast
        TargetObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject._ServerEichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
        TargetObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet = SourceObject._ServerEichprotokoll.Sicherung_AlibispeicherEingerichtet
        TargetObject.Eichprotokoll.Sicherung_Bemerkungen = SourceObject._ServerEichprotokoll.Sicherung_Bemerkungen
        TargetObject.Eichprotokoll.Sicherung_BenannteStelle = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelle
        TargetObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl = SourceObject._ServerEichprotokoll.Sicherung_BenannteStelleAnzahl
        TargetObject.Eichprotokoll.Sicherung_CE = SourceObject._ServerEichprotokoll.Sicherung_CE
        TargetObject.Eichprotokoll.Sicherung_CEAnzahl = SourceObject._ServerEichprotokoll.Sicherung_CEAnzahl
        TargetObject.Eichprotokoll.Sicherung_DatenAusgelesen = SourceObject._ServerEichprotokoll.Sicherung_DatenAusgelesen
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13 = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13
        TargetObject.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = SourceObject._ServerEichprotokoll.Sicherung_Eichsiegel13x13Anzahl
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRund = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRund
        TargetObject.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = SourceObject._ServerEichprotokoll.Sicherung_EichsiegelRundAnzahl
        TargetObject.Eichprotokoll.Sicherung_GruenesM = SourceObject._ServerEichprotokoll.Sicherung_GruenesM
        TargetObject.Eichprotokoll.Sicherung_GruenesMAnzahl = SourceObject._ServerEichprotokoll.Sicherung_GruenesMAnzahl
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelocht = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelocht
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
        TargetObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
        TargetObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject._ServerEichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
        TargetObject.Eichprotokoll.Ueberlastanzeige_Max = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Max
        TargetObject.Eichprotokoll.Ueberlastanzeige_Ueberlast = SourceObject._ServerEichprotokoll.Ueberlastanzeige_Ueberlast
        TargetObject.Eichprotokoll.Verwendungszweck_Automatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_Automatisch
        TargetObject.Eichprotokoll.Verwendungszweck_AutoTara = SourceObject._ServerEichprotokoll.Verwendungszweck_AutoTara
        TargetObject.Eichprotokoll.Verwendungszweck_Drucker = SourceObject._ServerEichprotokoll.Verwendungszweck_Drucker
        TargetObject.Eichprotokoll.Verwendungszweck_Druckertyp = SourceObject._ServerEichprotokoll.Verwendungszweck_Druckertyp
        TargetObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = SourceObject._ServerEichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension
        TargetObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject._ServerEichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
        TargetObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch = SourceObject._ServerEichprotokoll.Verwendungszweck_HalbAutomatisch
        TargetObject.Eichprotokoll.Verwendungszweck_HandTara = SourceObject._ServerEichprotokoll.Verwendungszweck_HandTara
        TargetObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = SourceObject._ServerEichprotokoll.Verwendungszweck_Nullnachfuehrung
        TargetObject.Eichprotokoll.Verwendungszweck_PC = SourceObject._ServerEichprotokoll.Verwendungszweck_PC
        TargetObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = SourceObject._ServerEichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
        TargetObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject._ServerEichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien

      
        'prüfungen
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen
                Dim targeto = New PruefungAnsprechvermoegen
                targeto.Anzeige = sourceo._Anzeige
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '   targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                targeto.Last1d = sourceo._Last1d
                targeto.LastL = sourceo._LastL
                targeto.Ziffernsprung = sourceo._Ziffernsprung
                TargetObject.Eichprotokoll.PruefungAnsprechvermoegen.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungAussermittigeBelastung = SourceObject._ServerEichprotokoll.PruefungAussermittigeBelastung
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung
                Dim targeto = New PruefungAussermittigeBelastung
                targeto.Anzeige = sourceo._Anzeige
                targeto.Belastungsort = sourceo._Belastungsort
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.EFGExtra = sourceo._EFGExtra
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '   targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungAussermittigeBelastung.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungLinearitaetFallend = SourceObject._ServerEichprotokoll.PruefungLinearitaetFallend
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend
                Dim targeto = New PruefungLinearitaetFallend
                targeto.Anzeige = sourceo._Anzeige
                targeto.Messpunkt = sourceo._Messpunkt
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '   targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungLinearitaetFallend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungLinearitaetSteigend = SourceObject._ServerEichprotokoll.PruefungLinearitaetSteigend
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend
                Dim targeto = New PruefungLinearitaetSteigend
                targeto.Anzeige = sourceo._Anzeige
                targeto.Messpunkt = sourceo._Messpunkt
                targeto.Bereich = sourceo._Bereich
                targeto.EFG = sourceo._EFG
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '  targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                TargetObject.Eichprotokoll.PruefungLinearitaetSteigend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        'TargetObject.ServerEichprotokoll.ServerPruefungRollendeLasten = SourceObject._ServerEichprotokoll.PruefungRollendeLasten
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungRollendeLasten
                Dim targeto = New PruefungRollendeLasten
                targeto.Anzeige = sourceo._Anzeige
                targeto.AuffahrtSeite = sourceo._AuffahrtSeite
                targeto.Belastungsstelle = sourceo._Belastungsstelle
                targeto.EFG = sourceo._EFG
                targeto.EFGExtra = sourceo._EFGExtra
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '   targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last
                TargetObject.Eichprotokoll.PruefungRollendeLasten.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage = SourceObject._ServerEichprotokoll.PruefungStabilitaetGleichgewichtslage
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage
                Dim targeto = New PruefungStabilitaetGleichgewichtslage
                targeto.Anzeige = sourceo._Anzeige
                targeto.AbdruckOK = sourceo._AbdruckOK
                targeto.Durchlauf = sourceo._Durchlauf
                targeto.MAX = sourceo._MAX
                targeto.MIN = sourceo._MIN
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '   targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast = SourceObject._ServerEichprotokoll.PruefungStaffelverfahrenErsatzlast
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast
                Dim targeto = New PruefungStaffelverfahrenErsatzlast
                targeto.Bereich = sourceo._Bereich
                targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                targeto.Ersatzlast_Ist = sourceo._Ersatzlast_Ist
                targeto.Ersatzlast_Soll = sourceo._Ersatzlast_Soll
                targeto.Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist
                targeto.Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll
                targeto.ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist
                targeto.ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '  targeto.ID = sourceo._ID
                targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                targeto.Staffel = sourceo._Staffel
                targeto.ZusaetzlicheErsatzlast_Soll = sourceo._ZusaetzlicheErsatzlast_Soll

                TargetObject.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try



        'TargetObject.ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast = SourceObject._ServerEichprotokoll.PruefungStaffelverfahrenNormallast
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast
                Dim targeto = New PruefungStaffelverfahrenNormallast
                targeto.Bereich = sourceo._Bereich
                targeto.DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG
                targeto.DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '  targeto.ID = sourceo._ID
                targeto.MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG
                targeto.MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler
                targeto.MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG
                targeto.MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler
                targeto.NormalLast_Anzeige_1 = sourceo._NormalLast_Anzeige_1
                targeto.NormalLast_Anzeige_2 = sourceo._NormalLast_Anzeige_2
                targeto.NormalLast_Anzeige_3 = sourceo._NormalLast_Anzeige_3
                targeto.NormalLast_Anzeige_4 = sourceo._NormalLast_Anzeige_4
                targeto.NormalLast_EFG_1 = sourceo._NormalLast_EFG_1
                targeto.NormalLast_EFG_2 = sourceo._NormalLast_EFG_2
                targeto.NormalLast_EFG_3 = sourceo._NormalLast_EFG_3
                targeto.NormalLast_EFG_4 = sourceo._NormalLast_EFG_4
                targeto.NormalLast_Fehler_1 = sourceo._NormalLast_Fehler_1
                targeto.NormalLast_Fehler_2 = sourceo._NormalLast_Fehler_2
                targeto.NormalLast_Fehler_3 = sourceo._NormalLast_Fehler_3
                targeto.NormalLast_Fehler_4 = sourceo._NormalLast_Fehler_1
                targeto.NormalLast_Last_1 = sourceo._NormalLast_Last_1
                targeto.NormalLast_Last_2 = sourceo._NormalLast_Last_2
                targeto.NormalLast_Last_3 = sourceo._NormalLast_Last_3
                targeto.NormalLast_Last_4 = sourceo._NormalLast_Last_4
                targeto.Staffel = sourceo._Staffel

                TargetObject.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try


        'TargetObject.ServerEichprotokoll.ServerPruefungWiederholbarkeit = SourceObject._ServerEichprotokoll.PruefungWiederholbarkeit
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit
                Dim targeto = New PruefungWiederholbarkeit
                targeto.Anzeige = sourceo._Anzeige
                targeto.Belastung = sourceo._Belastung
                targeto.Wiederholung = sourceo._Wiederholung
                targeto.EFG = sourceo._EFG
                targeto.EFG_Extra = sourceo._EFG_Extra
                targeto.Fehler = sourceo._Fehler
                targeto.FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                '  targeto.ID = sourceo._ID
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Return TargetObject
    End Function

    ''' <summary>
    ''' noch fehlende Nachschlage Listen laden (wie waagenart und typ)
    ''' </summary>
    ''' <param name="Targetobject"></param>
    ''' <remarks></remarks>
    Public Shared Sub GetLookupValuesServer(ByVal Targetobject As Eichprozess)
        Using dbContext As New EichsoftwareClientdatabaseEntities1
            Targetobject.Lookup_Vorgangsstatus = (From f In dbContext.Lookup_Vorgangsstatus Where f.ID = Targetobject.FK_Vorgangsstatus Select f).FirstOrDefault
            Targetobject.Lookup_Waagenart = (From f In dbContext.Lookup_Waagenart Where f.ID = Targetobject.FK_WaagenArt Select f).FirstOrDefault
            Targetobject.Lookup_Waagentyp = (From f In dbContext.Lookup_Waagentyp Where f.ID = Targetobject.FK_WaagenTyp Select f).FirstOrDefault
            Targetobject.Lookup_Bearbeitungsstatus = (From f In dbContext.Lookup_Bearbeitungsstatus Where f.ID = Targetobject.FK_Bearbeitungsstatus Select f).FirstOrDefault
            Targetobject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f In dbContext.Lookup_Konformitaetsbewertungsverfahren Where f.ID = Targetobject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f).FirstOrDefault
        End Using
    End Sub

    Private Shared Sub UeberschreibePruefungen(pModus As enuModus, ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess)
        Dim EichID As String = SourceObject.Eichprotokoll.ID
        Dim EichprozessID As String = SourceObject.ID
        Dim query = Nothing



                    'prüfungen
                    Using dbcontext As New EichsoftwareClientdatabaseEntities1

                    Try
                        If pModus = enuModus.RHEWASendetAnClient Then
                            query = SourceObject.Eichprotokoll.PruefungAnsprechvermoegen
                        Else
                    query = (From db In dbcontext.PruefungAnsprechvermoegen Where db.FK_Eichprotokoll = EichID).ToList
                        End If
                        ReDim TargetObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen(query.Count - 1)
                        Dim intCounter As Integer = 0
                        For Each sourceo In query
                            Dim targeto = New EichsoftwareWebservice.ServerPruefungAnsprechvermoegen
                            targeto._Anzeige = sourceo.Anzeige
                            targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                            targeto._Last = sourceo.Last
                            targeto._Last1d = sourceo.Last1d
                            targeto._LastL = sourceo.LastL
                            targeto._Ziffernsprung = sourceo.Ziffernsprung
                            TargetObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen(intCounter) = targeto
                            intCounter += 1
                        Next
                    Catch e As Exception
                    End Try



                    Try
                        If pModus = enuModus.RHEWASendetAnClient Then
                            query = SourceObject.Eichprotokoll.PruefungAussermittigeBelastung
                        Else
                    query = (From db In dbcontext.PruefungAussermittigeBelastung Where db.FK_Eichprotokoll = EichID).ToList

                        End If

                        ReDim TargetObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung(query.Count - 1)
                        Dim intCounter As Integer = 0
                        For Each sourceo In query
                            Dim targeto = New EichsoftwareWebservice.ServerPruefungAussermittigeBelastung
                            targeto._Anzeige = sourceo.Anzeige
                            targeto._Belastungsort = sourceo.Belastungsort
                            targeto._Bereich = sourceo.Bereich
                            targeto._EFG = sourceo.EFG
                            targeto._EFGExtra = sourceo.EFGExtra
                            targeto._Fehler = sourceo.Fehler
                            targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                            targeto._Last = sourceo.Last

                            TargetObject._ServerEichprotokoll.ServerPruefungAussermittigeBelastung(intCounter) = targeto
                            intCounter += 1
                        Next
                    Catch e As Exception
                    End Try




            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungLinearitaetFallend
                Else
                    query = (From db In dbcontext.PruefungLinearitaetFallend Where db.FK_Eichprotokoll = EichID).ToList
                End If


                ReDim TargetObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungLinearitaetFallend
                    targeto._Anzeige = sourceo.Anzeige
                    targeto._Messpunkt = sourceo.Messpunkt
                    targeto._Bereich = sourceo.Bereich
                    targeto._EFG = sourceo.EFG
                    targeto._Fehler = sourceo.Fehler
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._Last = sourceo.Last

                    TargetObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try


            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungLinearitaetSteigend

                Else
                    query = (From db In dbcontext.PruefungLinearitaetSteigend Where db.FK_Eichprotokoll = EichID).ToList

                End If

                ReDim TargetObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungLinearitaetSteigend
                    targeto._Anzeige = sourceo.Anzeige
                    targeto._Messpunkt = sourceo.Messpunkt
                    targeto._Bereich = sourceo.Bereich
                    targeto._EFG = sourceo.EFG
                    targeto._Fehler = sourceo.Fehler
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._Last = sourceo.Last
                    TargetObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try

            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungRollendeLasten

                Else
                    query = (From db In dbcontext.PruefungRollendeLasten Where db.FK_Eichprotokoll = EichID).ToList

                End If

                ReDim TargetObject._ServerEichprotokoll.ServerPruefungRollendeLasten(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungRollendeLasten
                    targeto._Anzeige = sourceo.Anzeige
                    targeto._AuffahrtSeite = sourceo.AuffahrtSeite
                    targeto._Belastungsstelle = sourceo.Belastungsstelle
                    targeto._EFG = sourceo.EFG
                    targeto._EFGExtra = sourceo.EFGExtra
                    targeto._Fehler = sourceo.Fehler
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._Last = sourceo.Last
                    TargetObject._ServerEichprotokoll.ServerPruefungRollendeLasten(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try


            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage

                Else
                    query = (From db In dbcontext.PruefungStabilitaetGleichgewichtslage Where db.FK_Eichprotokoll = EichID).ToList

                End If


                ReDim TargetObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStabilitaetGleichgewichtslage
                    targeto._Anzeige = sourceo.Anzeige
                    targeto._AbdruckOK = sourceo.AbdruckOK
                    targeto._Durchlauf = sourceo.Durchlauf
                    targeto._MAX = sourceo.MAX
                    targeto._MIN = sourceo.MIN
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._Last = sourceo.Last

                    TargetObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try


            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungStaffelverfahrenErsatzlast
                Else
                    query = (From db In dbcontext.PruefungStaffelverfahrenErsatzlast Where db.FK_Eichprotokoll = EichID).ToList
                End If


                ReDim TargetObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStaffelverfahrenErsatzlast
                    targeto._Bereich = sourceo.Bereich
                    targeto._DifferenzAnzeigewerte_EFG = sourceo.DifferenzAnzeigewerte_EFG
                    targeto._DifferenzAnzeigewerte_Fehler = sourceo.DifferenzAnzeigewerte_Fehler
                    targeto._Ersatzlast_Ist = sourceo.Ersatzlast_Ist
                    targeto._Ersatzlast_Soll = sourceo.Ersatzlast_Soll
                    targeto._Ersatzlast2_Ist = sourceo.Ersatzlast2_Ist
                    targeto._Ersatzlast2_Soll = sourceo.Ersatzlast2_Soll
                    targeto._ErsatzUndNormallast_Ist = sourceo.ErsatzUndNormallast_Ist
                    targeto._ErsatzUndNormallast_Soll = sourceo.ErsatzUndNormallast_Soll
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._MessabweichungStaffel_EFG = sourceo.MessabweichungStaffel_EFG
                    targeto._MessabweichungStaffel_Fehler = sourceo.MessabweichungStaffel_Fehler
                    targeto._MessabweichungWaage_EFG = sourceo.MessabweichungWaage_EFG
                    targeto._MessabweichungWaage_Fehler = sourceo.MessabweichungWaage_Fehler
                    targeto._Staffel = sourceo.Staffel
                    targeto._ZusaetzlicheErsatzlast_Soll = sourceo.ZusaetzlicheErsatzlast_Soll

                    TargetObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try

            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungStaffelverfahrenNormallast

                Else
                    query = (From db In dbcontext.PruefungStaffelverfahrenNormallast Where db.FK_Eichprotokoll = EichID).ToList

                End If

                ReDim TargetObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStaffelverfahrenNormallast
                    targeto._Bereich = sourceo.Bereich
                    targeto._DifferenzAnzeigewerte_EFG = sourceo.DifferenzAnzeigewerte_EFG
                    targeto._DifferenzAnzeigewerte_Fehler = sourceo.DifferenzAnzeigewerte_Fehler
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._MessabweichungStaffel_EFG = sourceo.MessabweichungStaffel_EFG
                    targeto._MessabweichungStaffel_Fehler = sourceo.MessabweichungStaffel_Fehler
                    targeto._MessabweichungWaage_EFG = sourceo.MessabweichungWaage_EFG
                    targeto._MessabweichungWaage_Fehler = sourceo.MessabweichungWaage_Fehler
                    targeto._NormalLast_Anzeige_1 = sourceo.NormalLast_Anzeige_1
                    targeto._NormalLast_Anzeige_2 = sourceo.NormalLast_Anzeige_2
                    targeto._NormalLast_Anzeige_3 = sourceo.NormalLast_Anzeige_3
                    targeto._NormalLast_Anzeige_4 = sourceo.NormalLast_Anzeige_4
                    targeto._NormalLast_EFG_1 = sourceo.NormalLast_EFG_1
                    targeto._NormalLast_EFG_2 = sourceo.NormalLast_EFG_2
                    targeto._NormalLast_EFG_3 = sourceo.NormalLast_EFG_3
                    targeto._NormalLast_EFG_4 = sourceo.NormalLast_EFG_4
                    targeto._NormalLast_Fehler_1 = sourceo.NormalLast_Fehler_1
                    targeto._NormalLast_Fehler_2 = sourceo.NormalLast_Fehler_2
                    targeto._NormalLast_Fehler_3 = sourceo.NormalLast_Fehler_3
                    targeto._NormalLast_Fehler_4 = sourceo.NormalLast_Fehler_1
                    targeto._NormalLast_Last_1 = sourceo.NormalLast_Last_1
                    targeto._NormalLast_Last_2 = sourceo.NormalLast_Last_2
                    targeto._NormalLast_Last_3 = sourceo.NormalLast_Last_3
                    targeto._NormalLast_Last_4 = sourceo.NormalLast_Last_4
                    targeto._Staffel = sourceo.Staffel

                    TargetObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try



                        'TargetObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit = SourceObject.Eichprotokoll.PruefungWiederholbarkeit
            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungWiederholbarkeit

                Else
                    query = (From db In dbcontext.PruefungWiederholbarkeit Where db.FK_Eichprotokoll = EichID).ToList

                End If


                ReDim TargetObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungWiederholbarkeit
                    targeto._Anzeige = sourceo.Anzeige
                    targeto._Belastung = sourceo.Belastung
                    targeto._Wiederholung = sourceo.Wiederholung
                    targeto._EFG = sourceo.EFG
                    targeto._EFG_Extra = sourceo.EFG_Extra
                    targeto._Fehler = sourceo.Fehler
                    targeto._FK_Eichprotokoll = sourceo.FK_Eichprotokoll
                    targeto._Last = sourceo.Last

                    TargetObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try

            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Mogelstatistik

                Else
                    query = (From db In dbcontext.Mogelstatistik Where db.FK_Eichprozess = EichprozessID).ToList

                End If


                ReDim TargetObject._ServerMogelstatistik(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerMogelstatistik
                    targeto._FK_Auswertegeraet = sourceo.FK_Auswertegeraet
                    targeto._FK_Eichprozess = sourceo.FK_Eichprozess
                    targeto._FK_Waegezelle = sourceo.FK_Waegezelle
                    targeto._Kompatiblitaet_AnschriftWaagenbaufirma = sourceo.Kompatiblitaet_AnschriftWaagenbaufirma
                    targeto._Kompatiblitaet_AWG_Anschlussart = sourceo.Kompatiblitaet_AWG_Anschlussart
                    targeto._Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = sourceo.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
                    targeto._Kompatiblitaet_Waage_AdditiveTarahoechstlast = sourceo.Kompatiblitaet_Waage_AdditiveTarahoechstlast
                    targeto._Kompatiblitaet_Waage_AnzahlWaegezellen = sourceo.Kompatiblitaet_Waage_AnzahlWaegezellen
                    targeto._Kompatiblitaet_Waage_Bauartzulassung = sourceo.Kompatiblitaet_Waage_Bauartzulassung
                    targeto._Kompatiblitaet_Waage_Ecklastzuschlag = sourceo.Kompatiblitaet_Waage_Ecklastzuschlag
                    targeto._Kompatiblitaet_Waage_Einschaltnullstellbereich = sourceo.Kompatiblitaet_Waage_Einschaltnullstellbereich
                    targeto._Kompatiblitaet_Waage_FabrikNummer = sourceo.Kompatiblitaet_Waage_FabrikNummer
                    targeto._Kompatiblitaet_Waage_Genauigkeitsklasse = sourceo.Kompatiblitaet_Waage_Genauigkeitsklasse
                    targeto._Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = sourceo.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
                    targeto._Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = sourceo.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2
                    targeto._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3
                    targeto._Kompatiblitaet_Waage_Kabellaenge = sourceo.Kompatiblitaet_Waage_Kabellaenge
                    targeto._Kompatiblitaet_Waage_Kabelquerschnitt = sourceo.Kompatiblitaet_Waage_Kabelquerschnitt
                    targeto._Kompatiblitaet_Waage_Revisionsnummer = sourceo.Kompatiblitaet_Waage_Revisionsnummer
                    targeto._Kompatiblitaet_Waage_Totlast = sourceo.Kompatiblitaet_Waage_Totlast
                    targeto._Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = sourceo.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
                    targeto._Kompatiblitaet_Waage_Zulassungsinhaber = sourceo.Kompatiblitaet_Waage_Zulassungsinhaber
                    targeto._Kompatiblitaet_WZ_Hoechstlast = sourceo.Kompatiblitaet_WZ_Hoechstlast

                    TargetObject._ServerMogelstatistik(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try

                    End Using
    End Sub



  

End Class
