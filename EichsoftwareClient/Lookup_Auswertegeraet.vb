'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Lookup_Auswertegeraet
    Public Property ID As String
    Public Property Typ As String
    Public Property Hersteller As String
    Public Property Pruefbericht As String
    Public Property Bauartzulassung As String
    Public Property Genauigkeitsklasse As String
    Public Property MAXAnzahlTeilungswerteEinbereichswaage As String
    Public Property MAXAnzahlTeilungswerteMehrbereichswaage As String
    Public Property Speisespannung As String
    Public Property Mindesteingangsspannung As String
    Public Property Mindestmesssignal As String
    Public Property GrenzwertLastwiderstandMIN As String
    Public Property GrenzwertLastwiderstandMAX As String
    Public Property GrenzwertTemperaturbereichMIN As String
    Public Property GrenzwertTemperaturbereichMAX As String
    Public Property BruchteilEichfehlergrenze As String
    Public Property KabellaengeQuerschnitt As String
    Public Property Deaktiviert As Nullable(Of Boolean)
    Public Property TaraeinrichtungTaraeingabe As Nullable(Of Boolean)
    Public Property TaraeinrichtungSelbsttaetig As Nullable(Of Boolean)
    Public Property TaraeinrichtungHalbSelbsttaetig As Nullable(Of Boolean)
    Public Property NullstellungNullnachfuehrung As Nullable(Of Boolean)
    Public Property NullstellungSelbsttaetig As Nullable(Of Boolean)
    Public Property NullstellungHalbSelbsttaetig As Nullable(Of Boolean)

    Public Overridable Property Eichprozess As ICollection(Of Eichprozess) = New HashSet(Of Eichprozess)
    Public Overridable Property Mogelstatistik As ICollection(Of Mogelstatistik) = New HashSet(Of Mogelstatistik)

End Class
