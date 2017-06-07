Public Class EichmarkenComparable


    Public Property CEAnzahl As Integer
    Public Property CE As Boolean

    Public Property SicherungsmarkeKleinAnzahl As Integer
    Public Property SicherungsmarkeKlein As Boolean

    Public Property SicherungsmarkeGrossAnzahl As Integer
    Public Property SicherungsmarkeGross As Boolean

    Public Property HinweismarkeAnzahl As Integer
    Public Property Hinweismarke As Boolean

    Public Function IsDirty(objEichprotokoll As Eichprotokoll) As Boolean
        If Not Me.CEAnzahl = objEichprotokoll.Sicherung_BenannteStelleAnzahl Then Return False
        If Not Me.CE = objEichprotokoll.Sicherung_BenannteStelle Then Return False
        If Not Me.SicherungsmarkeKleinAnzahl = objEichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl Then Return False
        If Not Me.SicherungsmarkeKlein = objEichprotokoll.Sicherung_SicherungsmarkeKlein Then Return False
        If Not Me.SicherungsmarkeGrossAnzahl = objEichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl Then Return False
        If Not Me.SicherungsmarkeGross = objEichprotokoll.Sicherung_SicherungsmarkeGross Then Return False
        If Not Me.HinweismarkeAnzahl = objEichprotokoll.Sicherung_HinweismarkeAnzahl Then Return False
        If Not Me.Hinweismarke = objEichprotokoll.Sicherung_Hinweismarke Then Return False

        Return True
    End Function


End Class
