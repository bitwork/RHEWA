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

Partial Public Class ServerLookup_Waegezelle
    Public Property ID As string
    Public Property Hersteller As string
    Public Property Typ As string
    Public Property Pruefbericht As string
    Public Property Bauartzulassung As string
    Public Property Revisionsnummer As string
    Public Property Genauigkeitsklasse As string
    Public Property Mindestvorlast As string
    Public Property Waegezellenkennwert As string
    Public Property MaxAnzahlTeilungswerte As string
    Public Property MinTeilungswert As string
    Public Property Hoechsteteilungsfaktor As string
    Public Property Kriechteilungsfaktor As string
    Public Property RueckkehrVorlastsignal As string
    Public Property WiderstandWaegezelle As string
    Public Property GrenzwertTemperaturbereichMIN As string
    Public Property GrenzwertTemperaturbereichMAX As string
    Public Property BruchteilEichfehlergrenze As string
    Public Property ErstellDatum As Nullable(Of Date)
    Public Property Deaktiviert As Boolean
    Public Property Neu As Boolean
    Public Property MindestvorlastProzent As string
    Public Property Bemerkung As string

    Public Overridable Property ServerEichprozess As ICollection(Of ServerEichprozess) = New HashSet(Of ServerEichprozess)
    Public Overridable Property ServerMogelstatistik As ICollection(Of ServerMogelstatistik) = New HashSet(Of ServerMogelstatistik)

End Class
