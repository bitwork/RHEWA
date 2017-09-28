'DTO für vereinfachte Darstellung eines Eichprozesses z.b. für das Hauptgrid im Client
Public Class clsEichprozessFuerAuswahlliste
    Public Property ID As String = ""
    Public Property Vorgangsnummer As String = ""
    Public Property Fabriknummer As String = ""
    Public Property WZ As String = ""
    Public Property Waagentyp As String = ""
    Public Property Waagenart As String = ""
    Public Property AWG As String = ""
    Public Property Eichbevollmaechtigter As String = ""
    Public Property Bearbeitungsstatus As String = ""
    Public Property GesperrtDurch As String = ""
    Public Property AnhangPfad As String = ""
    Public Property NeueWZ As Boolean = False
    Public Property Uploaddatum As DateTime
    Public Property Pruefscheinnummer As String
    Public Property Bemerkung As String
    Public Property HKBDatum As String
End Class