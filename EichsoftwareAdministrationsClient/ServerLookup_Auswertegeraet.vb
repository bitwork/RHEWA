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

Partial Public Class ServerLookup_Auswertegeraet
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
    Public Property ErstellDatum As Nullable(Of Date)
    Public Property Deaktiviert As Boolean
    Public Property TaraeinrichtungTaraeingabe As Boolean
    Public Property TaraeinrichtungSelbsttaetig As Boolean
    Public Property TaraeinrichtungHalbSelbsttaetig As Boolean
    Public Property NullstellungNullnachfuehrung As Boolean
    Public Property NullstellungSelbsttaetig As Boolean
    Public Property NullstellungHalbSelbsttaetig As Boolean

    Public Overridable Property ServerMogelstatistik As ICollection(Of ServerMogelstatistik) = New HashSet(Of ServerMogelstatistik)

End Class
