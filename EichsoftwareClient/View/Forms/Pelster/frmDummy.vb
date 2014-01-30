Public Class frmDummy

    Private Sub RadButton1_Click(sender As System.Object, e As System.EventArgs) Handles RadButton1.Click
        Dim u As New ucoEichtechnischeSicherung
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton2_Click(sender As System.Object, e As System.EventArgs) Handles RadButton2.Click
        Dim u As New ucoFallbeschleunigung
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton4_Click(sender As System.Object, e As System.EventArgs) Handles RadButton4.Click
        Dim u As New ucoPruefungAnsprechvermoegen
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton3_Click(sender As System.Object, e As System.EventArgs) Handles RadButton3.Click
        Dim u As New ucoPruefungRollendeLasten
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton6_Click(sender As System.Object, e As System.EventArgs) Handles RadButton6.Click
        Dim u As New ucoPruefungStaffelverfahren
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton5_Click(sender As System.Object, e As System.EventArgs) Handles RadButton5.Click
        Dim u As New ucoPruefungUeberlastanzeige
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton8_Click(sender As System.Object, e As System.EventArgs) Handles RadButton8.Click
        Dim u As New ucoPruefungStabilitaet
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub

    Private Sub RadButton7_Click(sender As System.Object, e As System.EventArgs) Handles RadButton7.Click
        Dim u As New ucoTaraeinrichtung
        u.Dock = DockStyle.Fill
        RadScrollablePanel1.Controls.Add(u)

    End Sub
End Class


