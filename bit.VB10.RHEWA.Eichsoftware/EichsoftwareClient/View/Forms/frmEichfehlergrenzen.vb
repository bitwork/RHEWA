Public Class frmEichfehlergrenzen

    Sub New(ByVal pObjEichprozess As Eichprozess)

        ' This call is required by the designer.
        InitializeComponent()

        Me.Controls.Remove(Me.UcoEichfehlergrenzen1)

        Me.UcoEichfehlergrenzen1 = New ucoEichfehlergrenzen(Nothing, pObjEichprozess)
        ' Add any initialization after the InitializeComponent() call.
        Me.Controls.Add(UcoEichfehlergrenzen1)
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub
End Class