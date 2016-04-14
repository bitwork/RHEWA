''' <summary>
''' hilfsklasse. Enthält Informationen und Einstellungen des aktuell angemeldeten Benutzers
''' </summary>
''' <remarks></remarks>
Public Class AktuellerBenutzer

    Private mvarLetztesUpdate As DateTime
    Private mvarAktuelleSprache As String
    Private mvarSynchronisierungsmodus As String
    Private mvarSyncAb As DateTime
    Private mvarSyncBis As DateTime
    Private mvarHoleAlleeigenenEichungenVomServer As Boolean
    Private mvarGridSettings As String
    Private mvarGridSettingsRHEWA As String
    Private mvarDefaultGridSettings As String = "PFJhZEdyaWRWaWV3IFNob3dOb0RhdGFUZXh0PSJGYWxzZSIgVGV4dD0iUmFkR3JpZFZpZXcxIiBBdXRvU2Nyb2xsPSJUcnVlIiBUYWJJbmRleD0iMCI+PE1hc3RlclRlbXBsYXRlIEFsbG93RHJhZ1RvR3JvdXA9IkZhbHNlIiBBbGxvd0VkaXRSb3c9IkZhbHNlIiBBbGxvd0NlbGxDb250ZXh0TWVudT0iRmFsc2UiIEFsbG93RGVsZXRlUm93PSJGYWxzZSIgQWxsb3dBZGROZXdSb3c9IkZhbHNlIiBTaG93R3JvdXBlZENvbHVtbnM9IlRydWUiPjxWaWV3RGVmaW5pdGlvbiB4c2k6dHlwZT0iVGVsZXJpay5XaW5Db250cm9scy5VSS5UYWJsZVZpZXdEZWZpbml0aW9uIiB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiAvPjwvTWFzdGVyVGVtcGxhdGU+PC9SYWRHcmlkVmlldz4="
    Private mvarDefaultGridSettingsRHEWA As String = "PFJhZEdyaWRWaWV3IFNob3dOb0RhdGFUZXh0PSJGYWxzZSIgVGV4dD0iUmFkR3JpZFZpZXcxIiBBdXRvU2Nyb2xsPSJUcnVlIiBUYWJJbmRleD0iNSI+PE1hc3RlclRlbXBsYXRlIEFsbG93RWRpdFJvdz0iRmFsc2UiIEFsbG93Q2VsbENvbnRleHRNZW51PSJGYWxzZSIgQWxsb3dEZWxldGVSb3c9IkZhbHNlIiBBbGxvd0FkZE5ld1Jvdz0iRmFsc2UiIFNob3dHcm91cGVkQ29sdW1ucz0iVHJ1ZSIgQXV0b0V4cGFuZEdyb3Vwcz0iVHJ1ZSI+PFZpZXdEZWZpbml0aW9uIHhzaTp0eXBlPSJUZWxlcmlrLldpbkNvbnRyb2xzLlVJLlRhYmxlVmlld0RlZmluaXRpb24iIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIC8+PC9NYXN0ZXJUZW1wbGF0ZT48L1JhZEdyaWRWaWV3Pg=="

    Private mvarObjLizenz As Lizensierung

    ''' <summary>
    ''' Obsolete. Keine Singleton Instanz mehr im eigentlichen Sinne
    ''' </summary>
    ''' <remarks></remarks>
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
    Public Property LetztesUpdate() As DateTime
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


    ''' <summary>
    ''' Gets the  grid settings.
    ''' </summary>
    ''' <value>The  grid settings.</value>
    Public ReadOnly Property GridDefaultSettings() As String
        Get
            Return mvarDefaultGridSettings
        End Get
    End Property

    ''' <summary>
    ''' Gets the  grid settings rhewa.
    ''' </summary>
    ''' <value>The  grid settings rhewa.</value>
    Public ReadOnly Property GridDefaultSettingsRhewa() As String
        Get
            Return mvarDefaultGridSettingsRHEWA
        End Get
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

    ''' <summary>
    ''' speichert benutzerbezogene Daten in Datenbank. Etwa die Einstellung der Grids
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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



    ''' <summary>
    ''' Speichert Gridlayout als XML Stream, welcher in DB zum aktuellen Benutzer gespeichert wird
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SpeichereGridLayout(ByVal uco As ucoEichprozessauswahlliste)
        'speichere Layout der beiden Grids
        If uco.GetType Is GetType(ucoEichprozessauswahlliste) Then
            Dim gridProzesse = CType(uco, ucoEichprozessauswahlliste).RadGridViewAuswahlliste
            Dim gridProzesseRHEWA = CType(uco, ucoEichprozessauswahlliste).RadGridViewRHEWAAlle
            Try
                Using stream As New IO.MemoryStream()
                    gridProzesse.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)

                        AktuellerBenutzer.Instance.GridSettings = Convert.ToBase64String(buffer)
                    End If
                End Using
            Catch ex As Exception

            End Try

            Try
                Using stream As New IO.MemoryStream()
                    gridProzesseRHEWA.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)
                        AktuellerBenutzer.Instance.GridSettingsRhewa = Convert.ToBase64String(buffer)
                    End If
                End Using
            Catch ex As Exception
            End Try
        End If
        AktuellerBenutzer.SaveSettings()

    End Sub

    Friend Shared Sub LadeGridLayout(uco As ucoEichprozessauswahlliste)
        'laden des Grid Layouts aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettings.ToString.Equals("") Then
                Using stream As New IO.MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettings))
                    uco.RadGridViewAuswahlliste.LoadLayout(stream)

                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try

        'laden des RHEWA Grids aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettingsRhewa.ToString.Equals("") Then
                Using stream As New IO.MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettingsRhewa))
                    uco.RadGridViewRHEWAAlle.LoadLayout(stream)
                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub
End Class
