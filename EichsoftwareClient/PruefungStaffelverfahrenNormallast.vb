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

Partial Public Class PruefungStaffelverfahrenNormallast
    Public Property ID As Integer
    Public Property FK_Eichprotokoll As Integer
    Public Property Staffel As Byte
    Public Property Bereich As Byte
    Public Property NormalLast_Last_1 As string
    Public Property NormalLast_Last_2 As string
    Public Property NormalLast_Last_3 As string
    Public Property NormalLast_Last_4 As string
    Public Property NormalLast_Anzeige_1 As string
    Public Property NormalLast_Fehler_1 As string
    Public Property NormalLast_EFG_1 As string
    Public Property NormalLast_Anzeige_2 As string
    Public Property NormalLast_Fehler_2 As string
    Public Property NormalLast_EFG_2 As string
    Public Property NormalLast_Anzeige_4 As string
    Public Property NormalLast_Fehler_4 As string
    Public Property NormalLast_EFG_4 As string
    Public Property NormalLast_Anzeige_3 As string
    Public Property NormalLast_Fehler_3 As string
    Public Property NormalLast_EFG_3 As string
    Public Property DifferenzAnzeigewerte_Fehler As string
    Public Property DifferenzAnzeigewerte_EFG As string
    Public Property MessabweichungStaffel_Fehler As string
    Public Property MessabweichungStaffel_EFG As string
    Public Property MessabweichungWaage_Fehler As string
    Public Property MessabweichungWaage_EFG As string

    Public Overridable Property Eichprotokoll As Eichprotokoll

End Class
