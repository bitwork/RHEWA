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

Partial Public Class ServerPruefungAussermittigeBelastung
    Public Property ID As Integer
    Public Property FK_Eichprotokoll As Integer
    Public Property Bereich As Byte
    Public Property Belastungsort As string
    Public Property Last As string
    Public Property Anzeige As string
    Public Property Fehler As string
    Public Property EFG As Boolean
    Public Property EFGExtra As string

    Public Overridable Property ServerEichprotokoll As ServerEichprotokoll

End Class
