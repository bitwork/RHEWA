'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class ServerKompatiblitaetsnachweis
    Public Property ID As Integer
    Public Property Kompatiblitaet_Waage_Hoechstlast1 As string
    Public Property Kompatiblitaet_Waage_Hoechstlast2 As string
    Public Property Kompatiblitaet_Waage_Hoechstlast3 As string
    Public Property Kompatiblitaet_Waage_Eichwert1 As string
    Public Property Kompatiblitaet_Waage_Eichwert2 As string
    Public Property Kompatiblitaet_Waage_Eichwert3 As string
    Public Property Kompatiblitaet_Waage_FabrikNummer As string
    Public Property Kompatiblitaet_Waage_Bauartzulassung As string
    Public Property Kompatiblitaet_Waage_Revisionsnummer As string
    Public Property Kompatiblitaet_Waage_Zulassungsinhaber As string
    Public Property Kompatiblitaet_Waage_Genauigkeitsklasse As string
    Public Property Kompatiblitaet_Waage_AdditiveTarahoechstlast As string
    Public Property Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN As string
    Public Property Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX As string
    Public Property Kompatiblitaet_Waage_Uebersetzungsverhaeltnis As string
    Public Property Kompatiblitaet_Waage_AnzahlWaegezellen As string
    Public Property Kompatiblitaet_Waage_Einschaltnullstellbereich As string
    Public Property Kompatiblitaet_Waage_Ecklastzuschlag As string
    Public Property Kompatiblitaet_Waage_Totlast As string
    Public Property Kompatiblitaet_Waage_Kabellaenge As string
    Public Property Kompatiblitaet_Waage_Kabelquerschnitt As string
    Public Property Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze As string
    Public Property Kompatiblitaet_Hersteller As string
    Public Property Kompatiblitaet_Strasse As string
    Public Property Kompatiblitaet_Ort As string
    Public Property Kompatiblitaet_Postleitzahl As string
    Public Property Kompatiblitaet_AWG_Anschlussart As string
    Public Property Kompatiblitaet_WZ_Hoechstlast As string

    Public Overridable Property ServerEichprozess As ICollection(Of ServerEichprozess) = New HashSet(Of ServerEichprozess)

End Class
