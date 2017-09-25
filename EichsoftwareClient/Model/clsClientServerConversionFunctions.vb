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

#Region "Property Zuweisungen"
#Region "Server => Client"
    Private Shared Sub CopyClientWZ(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, ByVal bolNewWZ As Boolean)
        'auswerte gerät
        If bolNewWZ Then
            'wenn neu
            TargetObject.Lookup_Waegezelle = New Lookup_Waegezelle
        End If

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
    End Sub

    Private Shared Sub CopyClientAWG(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, ByVal bolNewAWG As Boolean)
        'auswerte gerät
        If bolNewAWG Then
            TargetObject.Lookup_Auswertegeraet = New Lookup_Auswertegeraet
        End If
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
        TargetObject.Lookup_Auswertegeraet.TaraeinrichtungHalbSelbsttaetig = SourceObject._ServerLookup_Auswertegeraet._TaraeinrichtungHalbSelbsttaetig
        TargetObject.Lookup_Auswertegeraet.TaraeinrichtungSelbsttaetig = SourceObject._ServerLookup_Auswertegeraet._TaraeinrichtungSelbsttaetig
        TargetObject.Lookup_Auswertegeraet.TaraeinrichtungTaraeingabe = SourceObject._ServerLookup_Auswertegeraet._TaraeinrichtungTaraeingabe
        TargetObject.Lookup_Auswertegeraet.NullstellungHalbSelbsttaetig = SourceObject._ServerLookup_Auswertegeraet._NullstellungHalbSelbsttaetig
        TargetObject.Lookup_Auswertegeraet.NullstellungSelbsttaetig = SourceObject._ServerLookup_Auswertegeraet._NullstellungSelbsttaetig
        TargetObject.Lookup_Auswertegeraet.NullstellungNullnachfuehrung = SourceObject._ServerLookup_Auswertegeraet._NullstellungNullnachfuehrung
    End Sub

    Private Shared Sub CopyClientKompNachweis(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, ByVal BolNewKompnachweis As Boolean)
        'Eichprotokol
        If BolNewKompnachweis Then
            TargetObject.Kompatiblitaetsnachweis = New Kompatiblitaetsnachweis
        End If

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
    End Sub

    Private Shared Sub CopyClientEichprotokoll(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, ByVal BolNewEichprotkoll As Boolean, ByVal NewEichProtokollSameServerID As Boolean)
        'Eichprotokol
        If BolNewEichprotkoll Then
            TargetObject.Eichprotokoll = New Eichprotokoll
            If NewEichProtokollSameServerID Then
                TargetObject.Eichprotokoll.ID = SourceObject._ServerEichprotokoll.ID
            End If
        Else
            TargetObject.Eichprotokoll.ID = TargetObject.FK_Eichprotokoll
        End If

        TargetObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_Geprueft
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        TargetObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject._ServerEichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genehmigt = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genehmigt
        TargetObject.Eichprotokoll.Taraeinrichtung_Taraeingabe = SourceObject._ServerEichprotokoll.Taraeinrichtung_Taraeingabe
        TargetObject.Eichprotokoll.Fallbeschleunigung_g = SourceObject._ServerEichprotokoll.Fallbeschleunigung_g
        TargetObject.Eichprotokoll.Fallbeschleunigung_ms2 = SourceObject._ServerEichprotokoll.Fallbeschleunigung_ms2
        TargetObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject._ServerEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
        TargetObject.Eichprotokoll.Identifikationsdaten_Benutzer = SourceObject._ServerEichprotokoll.Identifikationsdaten_Benutzer
        TargetObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = SourceObject._ServerEichprotokoll.GenauigkeitNullstellung_InOrdnung
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
        TargetObject.Eichprotokoll.Sicherung_DatenAusgelesen = SourceObject._ServerEichprotokoll.Sicherung_DatenAusgelesen
        TargetObject.Eichprotokoll.Sicherung_SicherungsmarkeKlein = SourceObject._ServerEichprotokoll.Sicherung_SicherungsmarkeKlein
        TargetObject.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl = SourceObject._ServerEichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl
        TargetObject.Eichprotokoll.Sicherung_SicherungsmarkeGross = SourceObject._ServerEichprotokoll.Sicherung_SicherungsmarkeGross
        TargetObject.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl = SourceObject._ServerEichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl
        TargetObject.Eichprotokoll.Sicherung_Hinweismarke = SourceObject._ServerEichprotokoll.Sicherung_Hinweismarke
        TargetObject.Eichprotokoll.Sicherung_HinweismarkeAnzahl = SourceObject._ServerEichprotokoll.Sicherung_HinweismarkeAnzahl
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
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefintervall
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_LetztePruefung
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        TargetObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject._ServerEichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
    End Sub

    Private Shared Sub CopyClientPruefungen(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, ByVal bolNewIDs As Boolean, ByVal bolMogelstatistik As Boolean)
        'prüfungen
        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen
                Dim targeto = New PruefungAnsprechvermoegen With {
                    .Anzeige = sourceo._Anzeige,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If

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
                Dim targeto = New PruefungAussermittigeBelastung With {
                    .Anzeige = sourceo._Anzeige,
                    .Belastungsort = sourceo._Belastungsort,
                    .Bereich = sourceo._Bereich,
                    .EFG = sourceo._EFG,
                    .EFGExtra = sourceo._EFGExtra,
                    .Fehler = sourceo._Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungAussermittigeBelastung.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetFallend
                Dim targeto = New PruefungLinearitaetFallend With {
                    .Anzeige = sourceo._Anzeige,
                    .Messpunkt = sourceo._Messpunkt,
                    .Bereich = sourceo._Bereich,
                    .EFG = sourceo._EFG,
                    .Fehler = sourceo._Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungLinearitaetFallend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungLinearitaetSteigend
                Dim targeto = New PruefungLinearitaetSteigend With {
                    .Anzeige = sourceo._Anzeige,
                    .Messpunkt = sourceo._Messpunkt,
                    .Bereich = sourceo._Bereich,
                    .EFG = sourceo._EFG,
                    .Fehler = sourceo._Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last
                TargetObject.Eichprotokoll.PruefungLinearitaetSteigend.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungRollendeLasten
                Dim targeto = New PruefungRollendeLasten With {
                    .Anzeige = sourceo._Anzeige,
                    .AuffahrtSeite = sourceo._AuffahrtSeite,
                    .Belastungsstelle = sourceo._Belastungsstelle,
                    .EFG = sourceo._EFG,
                    .EFGExtra = sourceo._EFGExtra,
                    .Fehler = sourceo._Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last
                TargetObject.Eichprotokoll.PruefungRollendeLasten.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStabilitaetGleichgewichtslage
                Dim targeto = New PruefungStabilitaetGleichgewichtslage With {
                    .Anzeige = sourceo._Anzeige,
                    .AbdruckOK = sourceo._AbdruckOK,
                    .Durchlauf = sourceo._Durchlauf,
                    .MAX = sourceo._MAX,
                    .MIN = sourceo._MIN,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungStabilitaetGleichgewichtslage.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenErsatzlast
                Dim targeto = New PruefungStaffelverfahrenErsatzlast With {
                    .Bereich = sourceo._Bereich,
                    .DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG,
                    .DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler,
                    .Ersatzlast_Ist = sourceo._Ersatzlast_Ist,
                    .Ersatzlast_Soll = sourceo._Ersatzlast_Soll,
                    .Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist,
                    .Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll,
                    .ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist,
                    .ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
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

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungStaffelverfahrenNormallast
                Dim targeto = New PruefungStaffelverfahrenNormallast With {
                    .Bereich = sourceo._Bereich,
                    .DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG,
                    .DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
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

        Try
            Dim intCounter As Integer = 0
            For Each sourceo In SourceObject._ServerEichprotokoll.ServerPruefungWiederholbarkeit
                Dim targeto = New PruefungWiederholbarkeit With {
                    .Anzeige = sourceo._Anzeige,
                    .Belastung = sourceo._Belastung,
                    .Wiederholung = sourceo._Wiederholung,
                    .EFG = sourceo._EFG,
                    .EFG_Extra = sourceo._EFG_Extra,
                    .Fehler = sourceo._Fehler,
                    .FK_Eichprotokoll = sourceo._FK_Eichprotokoll
                }
                If bolNewIDs Then
                    targeto.FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    targeto.ID = sourceo._ID
                End If
                targeto.Last = sourceo._Last

                TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
                intCounter += 1
            Next
        Catch e As Exception
        End Try

        If bolMogelstatistik Then
            Try
                Dim intCounter As Integer = 0
                For Each sourceo In SourceObject._ServerMogelstatistik
                    Dim targeto = New Mogelstatistik With {
                        .FK_Auswertegeraet = TargetObject.FK_Auswertegeraet,
                        .FK_Eichprozess = TargetObject.ID,
                        .FK_Waegezelle = TargetObject.FK_Waegezelle,
                        .Kompatiblitaet_AnschriftWaagenbaufirma = sourceo._Kompatiblitaet_AnschriftWaagenbaufirma,
                        .Kompatiblitaet_AWG_Anschlussart = sourceo._Kompatiblitaet_AWG_Anschlussart,
                        .Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = sourceo._Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze,
                        .Kompatiblitaet_Waage_AdditiveTarahoechstlast = sourceo._Kompatiblitaet_Waage_AdditiveTarahoechstlast,
                        .Kompatiblitaet_Waage_AnzahlWaegezellen = sourceo._Kompatiblitaet_Waage_AnzahlWaegezellen,
                        .Kompatiblitaet_Waage_Bauartzulassung = sourceo._Kompatiblitaet_Waage_Bauartzulassung,
                        .Kompatiblitaet_Waage_Ecklastzuschlag = sourceo._Kompatiblitaet_Waage_Ecklastzuschlag,
                        .Kompatiblitaet_Waage_Einschaltnullstellbereich = sourceo._Kompatiblitaet_Waage_Einschaltnullstellbereich,
                        .Kompatiblitaet_Waage_FabrikNummer = sourceo._Kompatiblitaet_Waage_FabrikNummer,
                        .Kompatiblitaet_Waage_Genauigkeitsklasse = sourceo._Kompatiblitaet_Waage_Genauigkeitsklasse,
                        .Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = sourceo._Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX,
                        .Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = sourceo._Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN,
                        .Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1,
                        .Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2,
                        .Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3,
                        .Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1,
                        .Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2,
                        .Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3 = sourceo._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3,
                        .Kompatiblitaet_Waage_Kabellaenge = sourceo._Kompatiblitaet_Waage_Kabellaenge,
                        .Kompatiblitaet_Waage_Kabelquerschnitt = sourceo._Kompatiblitaet_Waage_Kabelquerschnitt,
                        .Kompatiblitaet_Waage_Revisionsnummer = sourceo._Kompatiblitaet_Waage_Revisionsnummer,
                        .Kompatiblitaet_Waage_Totlast = sourceo._Kompatiblitaet_Waage_Totlast,
                        .Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = sourceo._Kompatiblitaet_Waage_Uebersetzungsverhaeltnis,
                        .Kompatiblitaet_Waage_Zulassungsinhaber = sourceo._Kompatiblitaet_Waage_Zulassungsinhaber,
                        .Kompatiblitaet_WZ_Hoechstlast = sourceo._Kompatiblitaet_WZ_Hoechstlast
                    }

                    TargetObject.Mogelstatistik.Add(targeto)
                    intCounter += 1
                Next
            Catch e As Exception
            End Try
        End If
    End Sub

#End Region

#Region "Client -> Server"
    Private Shared Sub CopyServerPruefungen(pModus As enuModus, ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess)
        Dim EichID As String = SourceObject.Eichprotokoll.ID
        Dim EichprozessID As String = SourceObject.ID
        Dim query = Nothing

        'prüfungen
        Using dbcontext As New Entities

            Try
                If pModus = enuModus.RHEWASendetAnClient Then
                    query = SourceObject.Eichprotokoll.PruefungAnsprechvermoegen
                Else
                    query = (From db In dbcontext.PruefungAnsprechvermoegen Where db.FK_Eichprotokoll = EichID).ToList
                End If
                ReDim TargetObject._ServerEichprotokoll.ServerPruefungAnsprechvermoegen(query.Count - 1)
                Dim intCounter As Integer = 0
                For Each sourceo In query
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungAnsprechvermoegen With {
                        ._Anzeige = sourceo.Anzeige,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last,
                        ._Last1d = sourceo.Last1d,
                        ._LastL = sourceo.LastL,
                        ._Ziffernsprung = sourceo.Ziffernsprung
                    }
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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungAussermittigeBelastung With {
                        ._Anzeige = sourceo.Anzeige,
                        ._Belastungsort = sourceo.Belastungsort,
                        ._Bereich = sourceo.Bereich,
                        ._EFG = sourceo.EFG,
                        ._EFGExtra = sourceo.EFGExtra,
                        ._Fehler = sourceo.Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungLinearitaetFallend With {
                        ._Anzeige = sourceo.Anzeige,
                        ._Messpunkt = sourceo.Messpunkt,
                        ._Bereich = sourceo.Bereich,
                        ._EFG = sourceo.EFG,
                        ._Fehler = sourceo.Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungLinearitaetSteigend With {
                        ._Anzeige = sourceo.Anzeige,
                        ._Messpunkt = sourceo.Messpunkt,
                        ._Bereich = sourceo.Bereich,
                        ._EFG = sourceo.EFG,
                        ._Fehler = sourceo.Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }
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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungRollendeLasten With {
                        ._Anzeige = sourceo.Anzeige,
                        ._AuffahrtSeite = sourceo.AuffahrtSeite,
                        ._Belastungsstelle = sourceo.Belastungsstelle,
                        ._EFG = sourceo.EFG,
                        ._EFGExtra = sourceo.EFGExtra,
                        ._Fehler = sourceo.Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }
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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStabilitaetGleichgewichtslage With {
                        ._Anzeige = sourceo.Anzeige,
                        ._AbdruckOK = sourceo.AbdruckOK,
                        ._Durchlauf = sourceo.Durchlauf,
                        ._MAX = sourceo.MAX,
                        ._MIN = sourceo.MIN,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStaffelverfahrenErsatzlast With {
                        ._Bereich = sourceo.Bereich,
                        ._DifferenzAnzeigewerte_EFG = sourceo.DifferenzAnzeigewerte_EFG,
                        ._DifferenzAnzeigewerte_Fehler = sourceo.DifferenzAnzeigewerte_Fehler,
                        ._Ersatzlast_Ist = sourceo.Ersatzlast_Ist,
                        ._Ersatzlast_Soll = sourceo.Ersatzlast_Soll,
                        ._Ersatzlast2_Ist = sourceo.Ersatzlast2_Ist,
                        ._Ersatzlast2_Soll = sourceo.Ersatzlast2_Soll,
                        ._ErsatzUndNormallast_Ist = sourceo.ErsatzUndNormallast_Ist,
                        ._ErsatzUndNormallast_Soll = sourceo.ErsatzUndNormallast_Soll,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._MessabweichungStaffel_EFG = sourceo.MessabweichungStaffel_EFG,
                        ._MessabweichungStaffel_Fehler = sourceo.MessabweichungStaffel_Fehler,
                        ._MessabweichungWaage_EFG = sourceo.MessabweichungWaage_EFG,
                        ._MessabweichungWaage_Fehler = sourceo.MessabweichungWaage_Fehler,
                        ._Staffel = sourceo.Staffel,
                        ._ZusaetzlicheErsatzlast_Soll = sourceo.ZusaetzlicheErsatzlast_Soll
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungStaffelverfahrenNormallast With {
                        ._Bereich = sourceo.Bereich,
                        ._DifferenzAnzeigewerte_EFG = sourceo.DifferenzAnzeigewerte_EFG,
                        ._DifferenzAnzeigewerte_Fehler = sourceo.DifferenzAnzeigewerte_Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._MessabweichungStaffel_EFG = sourceo.MessabweichungStaffel_EFG,
                        ._MessabweichungStaffel_Fehler = sourceo.MessabweichungStaffel_Fehler,
                        ._MessabweichungWaage_EFG = sourceo.MessabweichungWaage_EFG,
                        ._MessabweichungWaage_Fehler = sourceo.MessabweichungWaage_Fehler,
                        ._NormalLast_Anzeige_1 = sourceo.NormalLast_Anzeige_1,
                        ._NormalLast_Anzeige_2 = sourceo.NormalLast_Anzeige_2,
                        ._NormalLast_Anzeige_3 = sourceo.NormalLast_Anzeige_3,
                        ._NormalLast_Anzeige_4 = sourceo.NormalLast_Anzeige_4,
                        ._NormalLast_EFG_1 = sourceo.NormalLast_EFG_1,
                        ._NormalLast_EFG_2 = sourceo.NormalLast_EFG_2,
                        ._NormalLast_EFG_3 = sourceo.NormalLast_EFG_3,
                        ._NormalLast_EFG_4 = sourceo.NormalLast_EFG_4,
                        ._NormalLast_Fehler_1 = sourceo.NormalLast_Fehler_1,
                        ._NormalLast_Fehler_2 = sourceo.NormalLast_Fehler_2,
                        ._NormalLast_Fehler_3 = sourceo.NormalLast_Fehler_3,
                        ._NormalLast_Fehler_4 = sourceo.NormalLast_Fehler_1,
                        ._NormalLast_Last_1 = sourceo.NormalLast_Last_1,
                        ._NormalLast_Last_2 = sourceo.NormalLast_Last_2,
                        ._NormalLast_Last_3 = sourceo.NormalLast_Last_3,
                        ._NormalLast_Last_4 = sourceo.NormalLast_Last_4,
                        ._Staffel = sourceo.Staffel
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerPruefungWiederholbarkeit With {
                        ._Anzeige = sourceo.Anzeige,
                        ._Belastung = sourceo.Belastung,
                        ._Wiederholung = sourceo.Wiederholung,
                        ._EFG = sourceo.EFG,
                        ._EFG_Extra = sourceo.EFG_Extra,
                        ._Fehler = sourceo.Fehler,
                        ._FK_Eichprotokoll = sourceo.FK_Eichprotokoll,
                        ._Last = sourceo.Last
                    }

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
                    Dim targeto = New EichsoftwareWebservice.ServerMogelstatistik With {
                        ._FK_Auswertegeraet = sourceo.FK_Auswertegeraet,
                        ._FK_Eichprozess = sourceo.FK_Eichprozess,
                        ._FK_Waegezelle = sourceo.FK_Waegezelle,
                        ._Kompatiblitaet_AnschriftWaagenbaufirma = sourceo.Kompatiblitaet_AnschriftWaagenbaufirma,
                        ._Kompatiblitaet_AWG_Anschlussart = sourceo.Kompatiblitaet_AWG_Anschlussart,
                        ._Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = sourceo.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze,
                        ._Kompatiblitaet_Waage_AdditiveTarahoechstlast = sourceo.Kompatiblitaet_Waage_AdditiveTarahoechstlast,
                        ._Kompatiblitaet_Waage_AnzahlWaegezellen = sourceo.Kompatiblitaet_Waage_AnzahlWaegezellen,
                        ._Kompatiblitaet_Waage_Bauartzulassung = sourceo.Kompatiblitaet_Waage_Bauartzulassung,
                        ._Kompatiblitaet_Waage_Ecklastzuschlag = sourceo.Kompatiblitaet_Waage_Ecklastzuschlag,
                        ._Kompatiblitaet_Waage_Einschaltnullstellbereich = sourceo.Kompatiblitaet_Waage_Einschaltnullstellbereich,
                        ._Kompatiblitaet_Waage_FabrikNummer = sourceo.Kompatiblitaet_Waage_FabrikNummer,
                        ._Kompatiblitaet_Waage_Genauigkeitsklasse = sourceo.Kompatiblitaet_Waage_Genauigkeitsklasse,
                        ._Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = sourceo.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX,
                        ._Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = sourceo.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2,
                        ._Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3 = sourceo.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3,
                        ._Kompatiblitaet_Waage_Kabellaenge = sourceo.Kompatiblitaet_Waage_Kabellaenge,
                        ._Kompatiblitaet_Waage_Kabelquerschnitt = sourceo.Kompatiblitaet_Waage_Kabelquerschnitt,
                        ._Kompatiblitaet_Waage_Revisionsnummer = sourceo.Kompatiblitaet_Waage_Revisionsnummer,
                        ._Kompatiblitaet_Waage_Totlast = sourceo.Kompatiblitaet_Waage_Totlast,
                        ._Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = sourceo.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis,
                        ._Kompatiblitaet_Waage_Zulassungsinhaber = sourceo.Kompatiblitaet_Waage_Zulassungsinhaber,
                        ._Kompatiblitaet_WZ_Hoechstlast = sourceo.Kompatiblitaet_WZ_Hoechstlast
                    }

                    TargetObject._ServerMogelstatistik(intCounter) = targeto
                    intCounter += 1
                Next
            Catch e As Exception
            End Try

        End Using
    End Sub

    Public Shared Sub CopyServerObjectPropertieWZs(ByRef TargetObject As EichsoftwareWebservice.ServerLookup_Waegezelle, ByRef SourceObject As Lookup_Waegezelle)
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

    Private Shared Sub CopyServerKompNachweis(ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess)
        TargetObject._ServerKompatiblitaetsnachweis = New EichsoftwareWebservice.ServerKompatiblitaetsnachweis With {
            .Kompatiblitaet_AWG_Anschlussart = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart,
            .Kompatiblitaet_Hersteller = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller,
            .Kompatiblitaet_Ort = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Ort,
            .Kompatiblitaet_Postleitzahl = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl,
            .Kompatiblitaet_Strasse = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse,
            .Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze,
            .Kompatiblitaet_Waage_AdditiveTarahoechstlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast,
            .Kompatiblitaet_Waage_AnzahlWaegezellen = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen,
            .Kompatiblitaet_Waage_Bauartzulassung = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung,
            .Kompatiblitaet_Waage_Ecklastzuschlag = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag,
            .Kompatiblitaet_Waage_Eichwert1 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1,
            .Kompatiblitaet_Waage_Eichwert2 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2,
            .Kompatiblitaet_Waage_Eichwert3 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3,
            .Kompatiblitaet_Waage_Einschaltnullstellbereich = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich,
            .Kompatiblitaet_Waage_FabrikNummer = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
            .Kompatiblitaet_Waage_Genauigkeitsklasse = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse,
            .Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX,
            .Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN,
            .Kompatiblitaet_Waage_Hoechstlast1 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1,
            .Kompatiblitaet_Waage_Hoechstlast2 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2,
            .Kompatiblitaet_Waage_Hoechstlast3 = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3,
            .Kompatiblitaet_Waage_Kabellaenge = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge,
            .Kompatiblitaet_Waage_Kabelquerschnitt = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt,
            .Kompatiblitaet_Waage_Revisionsnummer = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer,
            .Kompatiblitaet_Waage_Totlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast,
            .Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis,
            .Kompatiblitaet_Waage_Zulassungsinhaber = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber,
            .Kompatiblitaet_WZ_Hoechstlast = SourceObject.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast
        }

    End Sub

    Private Shared Sub CopyServerEichprotokoll(ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess)
        TargetObject._ServerEichprotokoll = New EichsoftwareWebservice.ServerEichprotokoll With {
            .EignungAchlastwaegungen_Geprueft = SourceObject.Eichprotokoll.EignungAchlastwaegungen_Geprueft,
            .EignungAchlastwaegungen_WaagenbrueckeEbene = SourceObject.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene,
            .EignungAchlastwaegungen_WaageNichtGeeignet = SourceObject.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet,
            .Beschaffenheitspruefung_Genehmigt = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Genehmigt,
            .Fallbeschleunigung_g = SourceObject.Eichprotokoll.Fallbeschleunigung_g,
            .Fallbeschleunigung_ms2 = SourceObject.Eichprotokoll.Fallbeschleunigung_ms2,
            .FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = SourceObject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren,
            .Identifikationsdaten_Benutzer = SourceObject.Eichprotokoll.Identifikationsdaten_Benutzer,
            .GenauigkeitNullstellung_InOrdnung = SourceObject.Eichprotokoll.GenauigkeitNullstellung_InOrdnung,
            .Identifikationsdaten_Aufstellungsort = SourceObject.Eichprotokoll.Identifikationsdaten_Aufstellungsort,
            .Identifikationsdaten_Baujahr = SourceObject.Eichprotokoll.Identifikationsdaten_Baujahr,
            .Identifikationsdaten_Datum = SourceObject.Eichprotokoll.Identifikationsdaten_Datum,
            .Identifikationsdaten_HybridMechanisch = SourceObject.Eichprotokoll.Identifikationsdaten_HybridMechanisch,
            .Identifikationsdaten_Min1 = SourceObject.Eichprotokoll.Identifikationsdaten_Min1,
            .Identifikationsdaten_Min2 = SourceObject.Eichprotokoll.Identifikationsdaten_Min2,
            .Identifikationsdaten_Min3 = SourceObject.Eichprotokoll.Identifikationsdaten_Min3,
            .Identifikationsdaten_NichtSelbsteinspielend = SourceObject.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend,
            .Identifikationsdaten_Pruefer = SourceObject.Eichprotokoll.Identifikationsdaten_Pruefer,
            .Identifikationsdaten_Selbsteinspielend = SourceObject.Eichprotokoll.Identifikationsdaten_Selbsteinspielend,
            .Komponenten_Eichzaehlerstand = SourceObject.Eichprotokoll.Komponenten_Eichzaehlerstand,
            .Komponenten_Softwarestand = SourceObject.Eichprotokoll.Komponenten_Softwarestand,
            .Komponenten_WaegezellenFabriknummer = SourceObject.Eichprotokoll.Komponenten_WaegezellenFabriknummer,
            .Pruefverfahren_BetragNormallast = SourceObject.Eichprotokoll.Pruefverfahren_BetragNormallast,
            .Pruefverfahren_VolleNormallast = SourceObject.Eichprotokoll.Pruefverfahren_VolleNormallast,
            .Pruefverfahren_VollstaendigesStaffelverfahren = SourceObject.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren,
            .Sicherung_AlibispeicherAufbewahrungsdauerReduziert = SourceObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert,
            .Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = SourceObject.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung,
            .Sicherung_AlibispeicherEingerichtet = SourceObject.Eichprotokoll.Sicherung_AlibispeicherEingerichtet,
            .Sicherung_Bemerkungen = SourceObject.Eichprotokoll.Sicherung_Bemerkungen,
            .Sicherung_BenannteStelle = SourceObject.Eichprotokoll.Sicherung_BenannteStelle,
            .Sicherung_BenannteStelleAnzahl = SourceObject.Eichprotokoll.Sicherung_BenannteStelleAnzahl,
            .Sicherung_DatenAusgelesen = SourceObject.Eichprotokoll.Sicherung_DatenAusgelesen,
            .Sicherung_SicherungsmarkeKlein = SourceObject.Eichprotokoll.Sicherung_SicherungsmarkeKlein,
            .Sicherung_SicherungsmarkeKleinAnzahl = SourceObject.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl,
            .Sicherung_SicherungsmarkeGross = SourceObject.Eichprotokoll.Sicherung_SicherungsmarkeGross,
            .Sicherung_SicherungsmarkeGrossAnzahl = SourceObject.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl,
            .Sicherung_Hinweismarke = SourceObject.Eichprotokoll.Sicherung_Hinweismarke,
            .Sicherung_HinweismarkeAnzahl = SourceObject.Eichprotokoll.Sicherung_HinweismarkeAnzahl,
            .Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = SourceObject.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK,
            .Taraeinrichtung_GenauigkeitTarierungOK = SourceObject.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK,
            .Taraeinrichtung_TaraausgleichseinrichtungOK = SourceObject.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK,
            .Taraeinrichtung_Taraeingabe = SourceObject.Eichprotokoll.Taraeinrichtung_Taraeingabe,
            .Ueberlastanzeige_Max = SourceObject.Eichprotokoll.Ueberlastanzeige_Max,
            .Ueberlastanzeige_Ueberlast = SourceObject.Eichprotokoll.Ueberlastanzeige_Ueberlast,
            .Verwendungszweck_Automatisch = SourceObject.Eichprotokoll.Verwendungszweck_Automatisch,
            .Verwendungszweck_AutoTara = SourceObject.Eichprotokoll.Verwendungszweck_AutoTara,
            .Verwendungszweck_Drucker = SourceObject.Eichprotokoll.Verwendungszweck_Drucker,
            .Verwendungszweck_Druckertyp = SourceObject.Eichprotokoll.Verwendungszweck_Druckertyp,
            .Verwendungszweck_EichfaehigerDatenspeicher = SourceObject.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher,
            .Verwendungszweck_Fahrzeugwaagen_Dimension = SourceObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension,
            .Verwendungszweck_Fahrzeugwaagen_MxM = SourceObject.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM,
            .Verwendungszweck_HalbAutomatisch = SourceObject.Eichprotokoll.Verwendungszweck_HalbAutomatisch,
            .Verwendungszweck_HandTara = SourceObject.Eichprotokoll.Verwendungszweck_HandTara,
            .Verwendungszweck_Nullnachfuehrung = SourceObject.Eichprotokoll.Verwendungszweck_Nullnachfuehrung,
            .Verwendungszweck_PC = SourceObject.Eichprotokoll.Verwendungszweck_PC,
            .Verwendungszweck_ZubehoerVerschiedenes = SourceObject.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes,
            .Wiederholbarkeit_Staffelverfahren_MINNormalien = SourceObject.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien,
            .Beschaffenheitspruefung_Genauigkeitsklasse = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse,
            .Beschaffenheitspruefung_Pruefintervall = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall,
            .Beschaffenheitspruefung_LetztePruefung = SourceObject.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung,
            .Beschaffenheitspruefung_Pruefscheinnummer = SourceObject.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer,
            .Beschaffenheitspruefung_EichfahrzeugFirma = SourceObject.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
        }
    End Sub

#End Region

#End Region

#Region "Methoden"
#Region "Server => Client"
    ''' <summary>
    ''' wird genutzt im vom Server ein Element zu lesen. Der Nachteil ist, es kann nicht gespeichert werden
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyClientObjectPropertiesWithAllLookups(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath
        TargetObject.ErzeugerLizenz = SourceObject._ErzeugerLizenz

        'kompatiblitätsnachweis
        CopyClientKompNachweis(TargetObject, SourceObject, True)

        'Eichprotokoll
        CopyClientEichprotokoll(TargetObject, SourceObject, True, True)

        'prüfungen
        CopyClientPruefungen(TargetObject, SourceObject, True, True)

        'AWG
        CopyClientAWG(TargetObject, SourceObject, True)

        'WZ
        CopyClientWZ(TargetObject, SourceObject, True)

        Return TargetObject
    End Function

    ''' <summary>
    ''' behält alle IDS bei um ein Update zu ermöglichen
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyClientObjectPropertiesWithOwnIDs(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath
        TargetObject.ErzeugerLizenz = SourceObject._ErzeugerLizenz

        CopyClientKompNachweis(TargetObject, SourceObject, False)

        'Eichprotokoll
        CopyClientEichprotokoll(TargetObject, SourceObject, False, False)
        Return TargetObject
    End Function

    ''' <summary>
    ''' wird beim kopieren genutzt. überschreibt alle properties from source ins targetobject erzeugt aber neue IDS
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyClientObjectPropertiesWithNewIDs(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess, Optional ByVal bolBehalteVorgangsnummer As Boolean = False)
        'eichprozess
        TargetObject.Ausgeblendet = SourceObject._Ausgeblendet
        TargetObject.FK_Auswertegeraet = SourceObject._FK_Auswertegeraet
        TargetObject.FK_Eichprotokoll = SourceObject._FK_Eichprotokoll
        TargetObject.FK_Vorgangsstatus = SourceObject._FK_Vorgangsstatus
        TargetObject.FK_WaagenArt = SourceObject._FK_WaagenArt
        TargetObject.FK_WaagenTyp = SourceObject._FK_WaagenTyp
        TargetObject.FK_Waegezelle = SourceObject._FK_Waegezelle
        TargetObject.Bearbeitungsdatum = SourceObject._UploadDatum
        If bolBehalteVorgangsnummer Then
            TargetObject.Vorgangsnummer = SourceObject._Vorgangsnummer
        Else
            TargetObject.Vorgangsnummer = Guid.NewGuid.ToString
        End If
        TargetObject.FK_Bearbeitungsstatus = SourceObject._FK_Bearbeitungsstatus
        TargetObject.UploadFilePath = SourceObject._UploadFilePath
        TargetObject.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel

        'kompatiblitätsnachweis
        CopyClientKompNachweis(TargetObject, SourceObject, True)

        'Eichprotokoll
        CopyClientEichprotokoll(TargetObject, SourceObject, True, False)
        'prüfungen
        CopyClientPruefungen(TargetObject, SourceObject, False, False)

        Return TargetObject
    End Function
#End Region

#Region "Client => Server"
    ''' <summary>
    ''' Schreibt werte von einem lokalen Eichprozess in ein Server Object
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <param name="SourceObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CopyServerObjectProperties(ByRef TargetObject As EichsoftwareWebservice.ServerEichprozess, ByRef SourceObject As Eichprozess, ByVal pModus As enuModus)
        'eichprozess
        TargetObject._Ausgeblendet = SourceObject.Ausgeblendet
        TargetObject._FK_Auswertegeraet = SourceObject.FK_Auswertegeraet
        TargetObject._FK_Eichprotokoll = SourceObject.FK_Eichprotokoll
        TargetObject._FK_Kompatibilitaetsnachweis = SourceObject.FK_Kompatibilitaetsnachweis
        TargetObject._FK_Vorgangsstatus = SourceObject.FK_Vorgangsstatus
        TargetObject._FK_WaagenArt = SourceObject.FK_WaagenArt
        TargetObject._FK_WaagenTyp = SourceObject.FK_WaagenTyp
        TargetObject._FK_Bearbeitungsstatus = SourceObject.FK_Bearbeitungsstatus
        TargetObject._UploadFilePath = SourceObject.UploadFilePath
        TargetObject._ErzeugerLizenz = SourceObject.ErzeugerLizenz
        TargetObject._Vorgangsnummer = SourceObject.Vorgangsnummer

        If Not SourceObject.Lookup_Waegezelle.Neu Then
            TargetObject._FK_Waegezelle = SourceObject.FK_Waegezelle
        End If

        'kompatiblitätsnachweis
        CopyServerKompNachweis(TargetObject, SourceObject)

        'Eichprotokoll
        CopyServerEichprotokoll(TargetObject, SourceObject)

        'prüfungen übertragen. je nach Modus aus Client SDF laden oder aus Speicher laden (Serverobjekt)
        CopyServerPruefungen(pModus, TargetObject, SourceObject)

        Return TargetObject
    End Function
#End Region

#End Region

#Region "Hilfsfunktionen"

    ''' <summary>
    ''' Methode welche alle N:1 Verbindungen auf einen Eichprozess entfernt und mit neuen Werten neu anlegt. (Es koennen z.b. neue Pruefstaffeln eingetragen worden sein, somit ist es einfacher alles zu löschen und neu anzulegen als zu updaten)
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateForeignTables(ByRef TargetObject As Eichprozess, ByRef SourceObject As EichsoftwareWebservice.ServerEichprozess)
        If SourceObject Is Nothing OrElse TargetObject Is Nothing Then
            Return
        End If
        Using dbcontext As New Entities
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
                    Dim targeto = New PruefungAnsprechvermoegen With {
                        .Anzeige = sourceo._Anzeige,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last,
                        .Last1d = sourceo._Last1d,
                        .LastL = sourceo._LastL,
                        .Ziffernsprung = sourceo._Ziffernsprung
                    }
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
                    Dim targeto = New PruefungAussermittigeBelastung With {
                        .Anzeige = sourceo._Anzeige,
                        .Belastungsort = sourceo._Belastungsort,
                        .Bereich = sourceo._Bereich,
                        .EFG = sourceo._EFG,
                        .EFGExtra = sourceo._EFGExtra,
                        .Fehler = sourceo._Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last
                    }

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
                    Dim targeto = New PruefungLinearitaetFallend With {
                        .Anzeige = sourceo._Anzeige,
                        .Messpunkt = sourceo._Messpunkt,
                        .Bereich = sourceo._Bereich,
                        .EFG = sourceo._EFG,
                        .Fehler = sourceo._Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last
                    }

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
                    Dim targeto = New PruefungLinearitaetSteigend With {
                        .Anzeige = sourceo._Anzeige,
                        .Messpunkt = sourceo._Messpunkt,
                        .Bereich = sourceo._Bereich,
                        .EFG = sourceo._EFG,
                        .Fehler = sourceo._Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last
                    }
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
                    Dim targeto = New PruefungRollendeLasten With {
                        .Anzeige = sourceo._Anzeige,
                        .AuffahrtSeite = sourceo._AuffahrtSeite,
                        .Belastungsstelle = sourceo._Belastungsstelle,
                        .EFG = sourceo._EFG,
                        .EFGExtra = sourceo._EFGExtra,
                        .Fehler = sourceo._Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last
                    }
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
                    Dim targeto = New PruefungStabilitaetGleichgewichtslage With {
                        .Anzeige = sourceo._Anzeige,
                        .AbdruckOK = sourceo._AbdruckOK,
                        .Durchlauf = sourceo._Durchlauf,
                        .MAX = sourceo._MAX,
                        .MIN = sourceo._MIN,
                        .Last = sourceo._Last,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID
                    }
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
                    Dim targeto = New PruefungStaffelverfahrenErsatzlast With {
                        .Bereich = sourceo._Bereich,
                        .DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG,
                        .DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler,
                        .Ersatzlast_Ist = sourceo._Ersatzlast_Ist,
                        .Ersatzlast_Soll = sourceo._Ersatzlast_Soll,
                        .Ersatzlast2_Ist = sourceo._Ersatzlast2_Ist,
                        .Ersatzlast2_Soll = sourceo._Ersatzlast2_Soll,
                        .ErsatzUndNormallast_Ist = sourceo._ErsatzUndNormallast_Ist,
                        .ErsatzUndNormallast_Soll = sourceo._ErsatzUndNormallast_Soll,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG,
                        .MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler,
                        .MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG,
                        .MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler,
                        .Staffel = sourceo._Staffel,
                        .ZusaetzlicheErsatzlast_Soll = sourceo._ZusaetzlicheErsatzlast_Soll
                    }

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
                    Dim targeto = New PruefungStaffelverfahrenNormallast With {
                        .Bereich = sourceo._Bereich,
                        .DifferenzAnzeigewerte_EFG = sourceo._DifferenzAnzeigewerte_EFG,
                        .DifferenzAnzeigewerte_Fehler = sourceo._DifferenzAnzeigewerte_Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .MessabweichungStaffel_EFG = sourceo._MessabweichungStaffel_EFG,
                        .MessabweichungStaffel_Fehler = sourceo._MessabweichungStaffel_Fehler,
                        .MessabweichungWaage_EFG = sourceo._MessabweichungWaage_EFG,
                        .MessabweichungWaage_Fehler = sourceo._MessabweichungWaage_Fehler,
                        .NormalLast_Anzeige_1 = sourceo._NormalLast_Anzeige_1,
                        .NormalLast_Anzeige_2 = sourceo._NormalLast_Anzeige_2,
                        .NormalLast_Anzeige_3 = sourceo._NormalLast_Anzeige_3,
                        .NormalLast_Anzeige_4 = sourceo._NormalLast_Anzeige_4,
                        .NormalLast_EFG_1 = sourceo._NormalLast_EFG_1,
                        .NormalLast_EFG_2 = sourceo._NormalLast_EFG_2,
                        .NormalLast_EFG_3 = sourceo._NormalLast_EFG_3,
                        .NormalLast_EFG_4 = sourceo._NormalLast_EFG_4,
                        .NormalLast_Fehler_1 = sourceo._NormalLast_Fehler_1,
                        .NormalLast_Fehler_2 = sourceo._NormalLast_Fehler_2,
                        .NormalLast_Fehler_3 = sourceo._NormalLast_Fehler_3,
                        .NormalLast_Fehler_4 = sourceo._NormalLast_Fehler_1,
                        .NormalLast_Last_1 = sourceo._NormalLast_Last_1,
                        .NormalLast_Last_2 = sourceo._NormalLast_Last_2,
                        .NormalLast_Last_3 = sourceo._NormalLast_Last_3,
                        .NormalLast_Last_4 = sourceo._NormalLast_Last_4,
                        .Staffel = sourceo._Staffel
                    }

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
                    Dim targeto = New PruefungWiederholbarkeit With {
                        .Anzeige = sourceo._Anzeige,
                        .Belastung = sourceo._Belastung,
                        .Wiederholung = sourceo._Wiederholung,
                        .EFG = sourceo._EFG,
                        .EFG_Extra = sourceo._EFG_Extra,
                        .Fehler = sourceo._Fehler,
                        .FK_Eichprotokoll = TargetObject.Eichprotokoll.ID,
                        .Last = sourceo._Last
                    }

                    TargetObject.Eichprotokoll.PruefungWiederholbarkeit.Add(targeto)
                Next
            Catch e As Exception
            End Try
            dbcontext.SaveChanges()
        End Using
    End Sub

    ''' <summary>
    ''' noch fehlende Nachschlage Listen laden (wie waagenart und typ)
    ''' </summary>
    ''' <param name="Targetobject"></param>
    ''' <remarks></remarks>
    Public Shared Sub GetLookupValuesServer(ByVal Targetobject As Eichprozess)
        Using dbContext As New Entities
            Targetobject.Lookup_Vorgangsstatus = (From f In dbContext.Lookup_Vorgangsstatus Where f.ID = Targetobject.FK_Vorgangsstatus Select f).FirstOrDefault
            Targetobject.Lookup_Waagenart = (From f In dbContext.Lookup_Waagenart Where f.ID = Targetobject.FK_WaagenArt Select f).FirstOrDefault
            Targetobject.Lookup_Waagentyp = (From f In dbContext.Lookup_Waagentyp Where f.ID = Targetobject.FK_WaagenTyp Select f).FirstOrDefault
            Targetobject.Lookup_Bearbeitungsstatus = (From f In dbContext.Lookup_Bearbeitungsstatus Where f.ID = Targetobject.FK_Bearbeitungsstatus Select f).FirstOrDefault
            Targetobject.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f In dbContext.Lookup_Konformitaetsbewertungsverfahren Where f.ID = Targetobject.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f).FirstOrDefault
        End Using
    End Sub

    ''' <summary>
    ''' zurücksetzen einiger Werte für die neue Standardwaage wie Vorgangsnummer und Prüfungsergebnisse
    ''' </summary>
    ''' <param name="NeueFabriknummer"></param>
    ''' <param name="objClientEichprozess"></param>
    ''' <returns></returns>
    Public Shared Function SetDefaultWerteFuerStandardwaage(NeueFabriknummer As String, objClientEichprozess As Eichprozess) As Eichprozess
        'vorgangsnummer editieren
        objClientEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
        objClientEichprozess.FK_Bearbeitungsstatus = 4 'noch nichts
        objClientEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe

        'standardwaaeg zur identifizierung des verkürrtzten Prozesses setzen
        objClientEichprozess.AusStandardwaageErzeugt = True
        'neue Fabriknummer
        objClientEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer = NeueFabriknummer
        'Prüfscheinnummer leeren
        objClientEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = ""
        'Außermittige Belastung Leeren
        objClientEichprozess.Eichprotokoll.PruefungAussermittigeBelastung.Clear()
        'Genauigkeit Nullstellung leeren
        objClientEichprozess.Eichprotokoll.GenauigkeitNullstellung_InOrdnung = False
        'Prüfung Linearität leeren
        objClientEichprozess.Eichprotokoll.PruefungLinearitaetFallend.Clear()
        objClientEichprozess.Eichprotokoll.PruefungLinearitaetSteigend.Clear()
        'Überlastanzeige leeren
        objClientEichprozess.Eichprotokoll.Ueberlastanzeige_Ueberlast = False
        'Fallbeschleunigung wird geleert
        objClientEichprozess.Eichprotokoll.Fallbeschleunigung_ms2 = False
        objClientEichprozess.Eichprotokoll.Identifikationsdaten_Datum = Date.Now.Date
        objClientEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = Nothing
        objClientEichprozess.Eichprotokoll.Identifikationsdaten_Baujahr = Date.Now.Year
        Return objClientEichprozess
    End Function
#End Region

End Class