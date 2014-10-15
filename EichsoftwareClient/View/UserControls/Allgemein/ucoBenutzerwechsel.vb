Public Class ucoBenutzerwechsel
    Private mvarObjLizenz As Lizensierung
    Private ucoEichprozessauswahlliste As ucoEichprozessauswahlliste

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ucoEichprozessauswahlliste = Nothing
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub New(ByVal parent As ucoEichprozessauswahlliste)
        InitializeComponent()
        ucoEichprozessauswahlliste = parent
    End Sub
    Public Property objLizenz As Lizensierung
        Get
            Return mvarObjLizenz
        End Get
        Set(value As Lizensierung)
            mvarObjLizenz = value
            If Not mvarObjLizenz Is Nothing Then

                RadButton1.Text = My.Resources.GlobaleLokalisierung.Sitzung & " " & mvarObjLizenz.Vorname & " " & mvarObjLizenz.Name
            End If
        End Set
    End Property




    Private Sub OpenBenutzerAuswahl()
        Dim f As New FrmBenutzerauswahl
        f.ShowDialog()

        objLizenz = AktuellerBenutzer.Instance.Lizenz

        ucoEichprozessauswahlliste.LadeRoutine()
        ucoEichprozessauswahlliste.ParentFormular.TriggerLokalisierung() 'inkludiert LoadfromDatabase

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        OpenBenutzerAuswahl()

    End Sub
End Class
