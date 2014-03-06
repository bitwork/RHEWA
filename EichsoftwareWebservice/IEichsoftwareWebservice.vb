' HINWEIS: Mit dem Befehl "Umbenennen" im Kontextmenü können Sie den Schnittstellennamen "IService1" sowohl im Code als auch in der Konfigurationsdatei ändern.
<ServiceContract()> _
<CyclicReferencesAware(True)> _
Public Interface IEichsoftwareWebservice
    <OperationContract()> _
    Function Test() As Boolean

    <OperationContract()> _
    Function PruefeLizenz(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean
    <OperationContract()> _
    Function GetLizenzdaten(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsLizenzdaten
    <OperationContract()>
    Function AktiviereLizenz(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()> _
    Function PruefeObRHEWALizenz(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()>
    Function GetNeueWZ(ByVal HEKennung As String, Lizenzschluessel As String, ByVal LetztesUpdate As Date, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerLookup_Waegezelle()

    <OperationContract()>
    Sub SchreibeVerbindungsprotokoll(ByVal Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, ByVal Aktivitaet As String)

    <OperationContract()>
    Function GetNeuesAWG(ByVal HEKennung As String, Lizenzschluessel As String, ByVal LetztesUpdate As Date, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerLookup_Auswertegeraet()

    <OperationContract()>
    Function AddEichmarkenverwaltung(ByVal HEKennung As String, Lizenzschluessel As String, ByVal BenutzerFK As String, ByVal AnzahlBenannteStelle As Integer, ByVal AnzahlEichsiegel13x13 As Integer, ByVal AnzahlEichsiegelRund As Integer, ByVal AnzahlHinweismarke As Integer,
                                    ByVal AnzahlGruenesM As Integer, ByVal AnzahlCE As Integer, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean
    <OperationContract()>
    Function AddEichprozess(ByVal HEKennung As String, Lizenzschluessel As String, ByRef pObjEichprozess As ServerEichprozess, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()>
    Function AddWaegezelle(ByVal HEKennung As String, Lizenzschluessel As String, ByVal pObjWZ As ServerLookup_Waegezelle, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()>
    Function GetAlleEichprozesse(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsEichprozessFuerAuswahlliste()
    <OperationContract()>
    Function GetAlleEichprozesseImZeitraum(ByVal HEKennung As String, Lizenzschluessel As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String, Optional ByVal SyncAllesSeit As Date = #1/1/2000#, Optional ByVal SyncAllesBis As Date = #12/31/2999#) As ServerEichprozess()

    <OperationContract()>
    Function GetEichProzess(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As ServerEichprozess

    <OperationContract()>
    Function SetEichprozessUngueltig(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()>
    Function SetEichprozessGenehmight(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As Boolean

    <OperationContract()>
    Function CheckGueltigkeitEichprozess(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String


    <OperationContract()>
    Function CheckSperrung(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String

    <OperationContract()>
    Function SetSperrung(ByVal bolSperren As Boolean, ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As String

    <OperationContract()>
    Function GetFTPCredentials(ByVal HEKennung As String, Lizenzschluessel As String, ByVal Vorgangsnummer As String, ByVal WindowsUsername As String, ByVal Domainname As String, ByVal Computername As String) As clsServerFTPDaten

End Interface

#Region "Klassen für Client"
'muss als Serializable gekennezeichnet sein, statt DataContract. sonst ist die ergebnismenge leer. Liegt wohl daran, dass die Klasse Artikel ansich schon als Datacontract angegeb en ist
<Serializable()>
Public Class Beschaffenheitspruefung
End Class
<Serializable()>
Public Class Eichprotokoll
End Class
<Serializable()>
Public Class ServerEichmarkenverwaltung
End Class
<Serializable()>
Public Class ServerKomatiblitaetsnachweis
End Class
<Serializable()>
Public Class ServerEichprozess
End Class
<Serializable()>
Public Class ServerLizensierung
End Class
<Serializable()>
Public Class ServerLookup_Auswertegeraet
End Class
<Serializable()>
Public Class ServerLookup_Konformitaetsbewertungsverfahren
End Class
<Serializable()>
Public Class ServerLookup_Vorgangsstatus
End Class
<Serializable()>
Public Class ServerLookup_Waagenart
End Class
<Serializable()>
Public Class ServerLookup_Waagentyp
End Class
<Serializable()>
Public Class ServerLookup_Waegezelle
End Class
<Serializable()>
Public Class ServerMogelstatistik
End Class
<Serializable()>
Public Class ServerPruefungAussermittigeBelastung
End Class
<Serializable()>
Public Class ServerPruefungEichfehlergrenzen
End Class
<Serializable()>
Public Class ServerPruefungLinearitaetFallend
End Class
<Serializable()>
Public Class ServerPruefungLinearitaetSteigend
End Class
<Serializable()>
Public Class ServerPruefungAnsprechvermoegen
End Class
<Serializable()>
Public Class ServerPruefungRollendeLasten
End Class
<Serializable()>
Public Class ServerPruefungStabilitaetGleichgewichtslage
End Class
<Serializable()>
Public Class ServerPruefungStaffelverfahrenErsatzlast
End Class
<Serializable()>
Public Class ServerPruefungStaffelverfahrenNormallast
End Class
<Serializable()>
Public Class ServerPruefungWiederholbarkeit
End Class



#End Region
