'Module MainModule
'    Sub main()
'        AddHandler Application.ThreadException, AddressOf YourExceptionHandler
'        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
'        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf YourExceptionHandler

'        Dim f As New FrmMainContainer
'        f.ShowDialog()

'    End Sub

'    Public Sub YourExceptionHandler()
'        MessageBox.Show("")
'    End Sub
'End Module
