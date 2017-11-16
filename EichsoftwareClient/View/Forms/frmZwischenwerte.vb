Public Class frmZwischenwerte

    Sub New(ByVal pObjEichprozess As Eichprozess, pStaffel As String, pBereich As String, pErsatzgewicht As Decimal)

        ' This call is required by the designer.
        InitializeComponent()

        Me.Controls.Remove(Me.UcoZwischenwerte)

        Me.UcoZwischenwerte = New ucoZwischenwerte(Nothing, pObjEichprozess, pStaffel, pBereich, pErsatzgewicht)
        ' Add any initialization after the InitializeComponent() call.
        Me.Controls.Add(UcoZwischenwerte)
    End Sub

    Public Property dirtyFlag As Boolean = False

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub


    Private Sub cmdAbbrechen_Click(sender As Object, e As EventArgs) Handles cmdAbbrechen.Click
        If dirtyFlag Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class