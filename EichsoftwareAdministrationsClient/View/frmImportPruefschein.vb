Public Class FrmImportPruefschein
    Private Sub FrmImportPruefschein_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadBrowseEditor1.Value = My.Settings.LetzterPfadPruefscheinImport
        ReloadData()
    End Sub

    Private Sub RadButtonImport_Click(sender As Object, e As EventArgs) Handles RadButtonImport.Click
        If IO.File.Exists(RadBrowseEditor1.Value) Then
            If RadBrowseEditor1.Value.EndsWith(".xlsx") Then
                ImportData(RadBrowseEditor1.Value)

            Else
                MsgBox("Bitte gültige Excel Datei wählen")
            End If
        End If

        ReloadData()
    End Sub

    Private Function ImportData(path As String) As List(Of String)
        Dim excelapp As New Microsoft.Office.Interop.Excel.Application
        Dim wb = excelapp.Workbooks.Open(path)

        Dim Cols As Integer() = {1, 2, 3}
        'Columns to loop
        'C, E, F
        Dim ReturnListstring As New List(Of String)
        Dim ListObjPruefscheinnummer As New List(Of StatusPrüfscheinnummer)
        For Each sheet As Microsoft.Office.Interop.Excel.Worksheet In wb.Worksheets

            For Each row As Microsoft.Office.Interop.Excel.Range In sheet.UsedRange.Rows
                Dim data As String = String.Empty
                For Each c As Integer In Cols
                    'changed here to loop through columns
                    data += sheet.Cells(row.Row, c).Value2.ToString() + ";"
                Next
                ReturnListstring.Add(data)
            Next
            Exit For
        Next

        excelapp.Quit()

        For Each row In ReturnListstring
            If IsNumeric(row.Split(";")(0)) = False Then
                Continue For
            End If
            Dim objPruefscheinnummer As New StatusPrüfscheinnummer
            objPruefscheinnummer.Nummer = row.Split(";")(0)
            'muss 3 stellig sein
            If objPruefscheinnummer.Nummer.ToString.Length = 2 Then
                objPruefscheinnummer.Nummer = "0" + objPruefscheinnummer.Nummer
            End If
            objPruefscheinnummer.Gesperrt = IIf(row.Split(";")(1) = "1", True, False)
            objPruefscheinnummer.GesperrtDurchDatum = IIf(row.Split(";")(2) = "1", True, False)

            Dim duplette = (From o In ListObjPruefscheinnummer Where o.Nummer = objPruefscheinnummer.Nummer).FirstOrDefault
            If duplette Is Nothing Then
                ListObjPruefscheinnummer.Add(objPruefscheinnummer)
            End If

        Next

        If ListObjPruefscheinnummer.Count > 0 Then
            DeleteData()
            addData(ListObjPruefscheinnummer)
        End If

    End Function

    Private Sub DeleteData()
        Using dbContext As New HerstellerersteichungEntities
            Dim oldValues = dbContext.StatusPrüfscheinnummer
            For Each schein In oldValues
                dbContext.StatusPrüfscheinnummer.Remove(schein)
            Next
            dbContext.SaveChanges()

        End Using
    End Sub

    Private Sub addData(values As List(Of StatusPrüfscheinnummer))
        Using dbContext As New HerstellerersteichungEntities
            For Each o In values
                dbContext.StatusPrüfscheinnummer.Add(o)
            Next
            dbContext.SaveChanges()
        End Using
    End Sub

    Private Sub ReloadData()
        Using dbContext As New HerstellerersteichungEntities
            RadGridView1.DataSource = dbContext.StatusPrüfscheinnummer.ToList
        End Using
    End Sub

    Private Sub RadBrowseEditor1_ValueChanging(sender As Object, e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles RadBrowseEditor1.ValueChanging
        If IO.File.Exists(e.NewValue) Then
            My.Settings.LetzterPfadPruefscheinImport = e.NewValue
            My.Settings.Save()
        End If
    End Sub
End Class