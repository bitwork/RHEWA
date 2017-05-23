Public Class FrmNeueWaegezele
    Public Property NeueWaegezelle As Lookup_Waegezelle

    ''' <summary>
    ''' validieren der Pflichtfelder
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function validateControls() As Boolean

        If RadTextBoxControlWZTyp.Text.Trim.Equals("") Then
            Return False
        End If
        If RadTextBoxControlWZHersteller.Text.Trim.Equals("") Then
            Return False
        End If

        'entweder oder
        If RadTextBoxControlWZPruefbericht.Text.Trim.Equals("") Then
            If RadTextBoxControlWZBauartzulassung.Text.Trim.Equals("") Then
                Return False
            End If
        End If
        If RadTextBoxControlWZBauartzulassung.Text.Trim.Equals("") Then
            If RadTextBoxControlWZPruefbericht.Text.Trim.Equals("") Then
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Speichern der neuen Wägezelle
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function SavetoDatebase() As Boolean
        If validateControls() = True Then
            Using context As New Entities
                Dim objWZ As New Lookup_Waegezelle

                objWZ.Bauartzulassung = RadTextBoxControlWZBauartzulassung.Text
                objWZ.Hersteller = RadTextBoxControlWZHersteller.Text
                objWZ.Pruefbericht = RadTextBoxControlWZPruefbericht.Text
                objWZ.Typ = RadTextBoxControlWZTyp.Text
                objWZ.ID = Guid.NewGuid.ToString
                objWZ.Neu = True
                objWZ.Deaktiviert = False
                context.Lookup_Waegezelle.Add(objWZ)
                context.SaveChanges()

                NeueWaegezelle = objWZ
            End Using
            Return True
        Else
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
    End Function

    ''' <summary>
    ''' speichern und schließen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonSpeichern_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonSpeichern.Click
        If SavetoDatebase() = True Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class