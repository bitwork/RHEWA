Public Class AktuellerBenutzer

    Private mvarLetztesUpdate As DateTime
    Private mvarAktuelleSprache As String
    Private mvarSynchronisierungsmodus As String
    Private mvarSyncAb As DateTime
    Private mvarSyncBis As DateTime
    Private mvarHoleAlleeigenenEichungenVomServer As Boolean
    Private mvarGridSettings As String
    Private mvarGridSettingsRHEWA As String

    Private mvarObjLizenz As Lizensierung

    Private Shared mobjSingletonObject As AktuellerBenutzer
    ''' <summary>
    ''' Gets the Lizenz.
    ''' </summary>
    ''' <value>The  lizenz.</value>
    Public ReadOnly Property Lizenz() As Lizensierung
        Get
            Return mvarObjLizenz
        End Get
    End Property

    ''' <summary>
    ''' Gets the  letztes update.
    ''' </summary>
    ''' <value>The  letztes update.</value>
    Public  Property LetztesUpdate() As DateTime
        Get
            Return mvarLetztesUpdate
        End Get
        Set(value As DateTime)
            mvarLetztesUpdate = value
        End Set
    End Property


    ''' <summary>
    ''' Gets the  aktuelle sprache.
    ''' </summary>
    ''' <value>The  aktuelle sprache.</value>
    Public Property AktuelleSprache() As String
        Get
            Return mvarAktuelleSprache
        End Get
        Set(value As String)
            mvarAktuelleSprache = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  synchronisierungsmodus.
    ''' </summary>
    ''' <value>The  synchronisierungsmodus.</value>
    Public Property Synchronisierungsmodus() As String
        Get
            Return mvarSynchronisierungsmodus
        End Get
        Set(value As String)
            'speichern in Konfig DB
            mvarSynchronisierungsmodus = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  sync ab.
    ''' </summary>
    ''' <value>The  sync ab.</value>
    Public Property SyncAb() As DateTime
        Get
            Return mvarSyncAb
        End Get
        Set(value As DateTime)
            mvarSyncAb = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  sync bis.
    ''' </summary>
    ''' <value>The  sync bis.</value>
    Public Property SyncBis() As DateTime
        Get
            Return mvarSyncBis
        End Get
        Set(value As DateTime)
            mvarSyncBis = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  hole alleeigenen eichungen vom server.
    ''' </summary>
    ''' <value>The  hole alleeigenen eichungen vom server.</value>
    Public Property HoleAlleeigenenEichungenVomServer() As Boolean
        Get
            Return mvarHoleAlleeigenenEichungenVomServer
        End Get
        Set(value As Boolean)
            mvarHoleAlleeigenenEichungenVomServer = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  grid settings.
    ''' </summary>
    ''' <value>The  grid settings.</value>
    Public Property GridSettings() As String
        Get
            Return mvarGridSettings
        End Get
        Set(value As String)
            mvarGridSettings = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  grid settings rhewa.
    ''' </summary>
    ''' <value>The  grid settings rhewa.</value>
    Public Property GridSettingsRhewa() As String
        Get
            Return mvarGridSettingsRHEWA
        End Get
        Set(value As String)
            mvarGridSettingsRHEWA = value
        End Set
    End Property


    Public Shared ReadOnly Property Instance() As AktuellerBenutzer
        Get
            Return mobjSingletonObject
        End Get
    End Property

    Public Shared Function GetNewInstance(ByVal pLizenzschluessel As String)
        mobjSingletonObject = New AktuellerBenutzer
        mobjSingletonObject.mvarObjLizenz = clsDBFunctions.HoleLizenzObjekt(pLizenzschluessel)

        If mobjSingletonObject.mvarObjLizenz Is Nothing Then
            Return Nothing
        End If

        Using Context As New EichsoftwareClientdatabaseEntities1
            Context.Configuration.LazyLoadingEnabled = True
            Dim Konfig = (From Konfiguration In Context.Konfiguration Where Konfiguration.BenutzerLizenz = mobjSingletonObject.mvarObjLizenz.Lizenzschluessel).FirstOrDefault

            mobjSingletonObject.mvarAktuelleSprache = Konfig.AktuelleSprache
            mobjSingletonObject.mvarGridSettings = Konfig.GridSettings
            mobjSingletonObject.mvarGridSettingsRHEWA = Konfig.GridSettingsRHEWA
            mobjSingletonObject.mvarHoleAlleeigenenEichungenVomServer = Konfig.HoleAlleeigenenEichungenVomServer
            mobjSingletonObject.mvarLetztesUpdate = Konfig.LetztesUpdate
            mobjSingletonObject.mvarSyncAb = Konfig.SyncAb
            mobjSingletonObject.mvarSyncBis = Konfig.SyncBis
            mobjSingletonObject.mvarSynchronisierungsmodus = Konfig.Synchronisierungsmodus
         

        End Using


        Return mobjSingletonObject


    End Function

    Public Shared Function SaveSettings()
        Using Context As New EichsoftwareClientdatabaseEntities1
            Context.Configuration.LazyLoadingEnabled = True
            Dim Konfig = (From Konfiguration In Context.Konfiguration Where Konfiguration.BenutzerLizenz = mobjSingletonObject.mvarObjLizenz.Lizenzschluessel).FirstOrDefault

            Konfig.AktuelleSprache = mobjSingletonObject.mvarAktuelleSprache
            Konfig.GridSettings = mobjSingletonObject.mvarGridSettings
            Konfig.GridSettingsRHEWA = mobjSingletonObject.mvarGridSettingsRHEWA
            Konfig.HoleAlleeigenenEichungenVomServer = mobjSingletonObject.mvarHoleAlleeigenenEichungenVomServer
            Konfig.LetztesUpdate = mobjSingletonObject.mvarLetztesUpdate
            Konfig.SyncAb = mobjSingletonObject.mvarSyncAb
            Konfig.SyncBis = mobjSingletonObject.mvarSyncBis
            Konfig.Synchronisierungsmodus = mobjSingletonObject.mvarSynchronisierungsmodus

            Context.SaveChanges()
            Return True
        End Using
    End Function



    Private Sub New()

    End Sub

End Class
