'------------------------------------------------------------------------------
' <auto-generated>
'    Dieser Code wurde aus einer Vorlage generiert.
'
'    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
'    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class ServerPruefungStaffelverfahrenErsatzlast
    Public Property ID As Integer
    Public Property FK_Eichprotokoll As Integer
    Public Property Staffel As Byte
    Public Property Bereich As Byte
    Public Property Ersatzlast_Soll As String
    Public Property Ersatzlast_Ist As String
    Public Property ZusaetzlicheErsatzlast_Soll As String
    Public Property ErsatzUndNormallast_Soll As String
    Public Property ErsatzUndNormallast_Ist As String
    Public Property Ersatzlast2_Soll As String
    Public Property Ersatzlast2_Ist As String
    Public Property DifferenzAnzeigewerte_Fehler As String
    Public Property DifferenzAnzeigewerte_EFG As String
    Public Property MessabweichungStaffel_Fehler As String
    Public Property MessabweichungStaffel_EFG As String
    Public Property MessabweichungWaage_Fehler As String
    Public Property MessabweichungWaage_EFG As String

    Public Overridable Property ServerEichprotokoll As ServerEichprotokoll

End Class
