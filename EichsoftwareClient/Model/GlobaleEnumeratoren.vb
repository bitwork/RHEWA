Public Class GlobaleEnumeratoren
    ''' <summary>
    ''' WICHTIG DIE ENUMERATOREN MÜSSEN MIT DEN DATENKBANK IDs ÜBEREINSTIMMEN
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enuEichprozessStatus
        Stammdateneingabe = 2
        Kompatbilitaetsnachweis = 3
        KompatbilitaetsnachweisErgebnis = 4
        Beschaffenheitspruefung = 5
        AuswahlKonformitätsverfahren = 6
        EichprotokollStammdaten = 7
        PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung = 8
        PrüfungderRichtigkeitmitNormallastLinearitaet = 9
        PrüfungderRichtigkeitmitErsatzlast = 10
        PrüfungderWiederholbarkeit = 11
        PrüfungderÜberlastanzeige = 12
        WaagenFuerRollendeLasten = 13
        PrüfungdesAnsprechvermögens = 14
        PrüfungderStabilitätderGleichgewichtslage = 15
        Taraeinrichtung = 16
        EignungfürAchslastwägungen = 17
        BerücksichtigungderFallbeschleunigung = 18
        EichtechnischeSicherungundDatensicherung = 19
        Export = 20
        Versenden = 21
    End Enum

    Public Enum enuVerfahrensauswahl
        Fahrzeugwaagen = 1
        ueber60kgimStaffelverfahren = 2
        ueber60kgmitNormalien = 3
        nichts = 4
    End Enum

    Public Enum enuBearbeitungsstatus
        Wartet_auf_Bearbeitung = 1
        Fehlerhaft = 2
        Genehmigt = 3
        noch_nicht_versendet = 4
    End Enum
End Class
