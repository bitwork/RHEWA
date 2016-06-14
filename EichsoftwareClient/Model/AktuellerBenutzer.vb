Imports System.ComponentModel
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.UI
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
    Private mvarDefaultGridSettings As String = "PFJhZEdyaWRWaWV3IFNob3dOb0RhdGFUZXh0PSJGYWxzZSIgVGV4dD0iUmFkR3JpZFZpZXcxIiBBdXRvU2Nyb2xsPSJUcnVlIiBDdXJzb3I9IkRlZmF1bHQiIFRhYkluZGV4PSIwIj48TWFzdGVyVGVtcGxhdGUgRW5hYmxlQWx0ZXJuYXRpbmdSb3dDb2xvcj0iVHJ1ZSIgQWxsb3dFZGl0Um93PSJGYWxzZSIgQWxsb3dDb2x1bW5DaG9vc2VyPSJGYWxzZSIgQWxsb3dDb2x1bW5IZWFkZXJDb250ZXh0TWVudT0iRmFsc2UiIEFsbG93Um93SGVhZGVyQ29udGV4dE1lbnU9IkZhbHNlIiBBbGxvd0NlbGxDb250ZXh0TWVudT0iRmFsc2UiIEFsbG93RGVsZXRlUm93PSJGYWxzZSIgQWxsb3dBZGROZXdSb3c9IkZhbHNlIiBBbGxvd1NlYXJjaFJvdz0iVHJ1ZSIgU2hvd0dyb3VwZWRDb2x1bW5zPSJUcnVlIiBBdXRvRXhwYW5kR3JvdXBzPSJUcnVlIj48Q29sdW1ucz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjEyMCIgRmllbGROYW1lPSJTdGF0dXMiIE5hbWU9IlN0YXR1cyIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IlN0YXR1cyIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjE5NSIgRmllbGROYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIE5hbWU9IkJlYXJiZWl0dW5nc3N0YXR1cyIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkJlYXJiZWl0dW5nc3N0YXR1cyI+PENvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdExpc3Q+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0IFRWYWx1ZTE9IkZlaGxlcmhhZnQiIENlbGxGb3JlQ29sb3I9IiIgQ2VsbEJhY2tDb2xvcj0iMjQ3LCA4NywgNjciIFJvd0ZvcmVDb2xvcj0iIiBSb3dCYWNrQ29sb3I9IiIgTmFtZT0iRmVobGVyaGFmdCIgQXBwbHlUb1Jvdz0iVHJ1ZSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5Db25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3QgVFZhbHVlMT0iSW52YWxpZCIgQ2VsbEZvcmVDb2xvcj0iIiBDZWxsQmFja0NvbG9yPSIyNDcsIDg3LCA2NyIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iIiBOYW1lPSJpbnZhbGlkIiBBcHBseVRvUm93PSJUcnVlIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkNvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdCBUVmFsdWUxPSJHZW5laG1pZ3QiIENlbGxGb3JlQ29sb3I9IiIgQ2VsbEJhY2tDb2xvcj0iTGlnaHRHcmVlbiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iIiBOYW1lPSJHZW5laG1pZ3QiIEFwcGx5VG9Sb3c9IlRydWUiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0IFRWYWx1ZTE9IlZhbGlkIiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IkxpZ2h0R3JlZW4iIFJvd0ZvcmVDb2xvcj0iIiBSb3dCYWNrQ29sb3I9IiIgTmFtZT0iVmFsaWQiIEFwcGx5VG9Sb3c9IlRydWUiIC8+PC9Db25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3RMaXN0PjwvVGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdEZWNpbWFsQ29sdW1uIERhdGFUeXBlPSJTeXN0ZW0uSW50MzIiIEZpZWxkTmFtZT0iSUQiIE5hbWU9IklEIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iRmFsc2UiIEhlYWRlclRleHQ9IklEIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBGaWVsZE5hbWU9IlZvcmdhbmdzbnVtbWVyIiBOYW1lPSJWb3JnYW5nc251bW1lciIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IkZhbHNlIiBIZWFkZXJUZXh0PSJWb3JnYW5nc251bW1lciIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjEwMSIgRmllbGROYW1lPSJGYWJyaWtudW1tZXIiIE5hbWU9IkZhYnJpa251bW1lciIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkZhYnJpa251bW1lciIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjEzNCIgRmllbGROYW1lPSJMb29rdXBfV2FlZ2V6ZWxsZSIgTmFtZT0iTG9va3VwX1dhZWdlemVsbGUiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJXWiIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjkyIiBGaWVsZE5hbWU9Ikxvb2t1cF9XYWFnZW50eXAiIE5hbWU9Ikxvb2t1cF9XYWFnZW50eXAiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJXYWFnZW50eXAiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxMTMiIEZpZWxkTmFtZT0iTG9va3VwX1dhYWdlbmFydCIgTmFtZT0iTG9va3VwX1dhYWdlbmFydCIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IldhYWdlbmFydCIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjYzIiBGaWVsZE5hbWU9Ikxvb2t1cF9BdXN3ZXJ0ZWdlcmFldCIgTmFtZT0iTG9va3VwX0F1c3dlcnRlZ2VyYWV0IiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iQVdHIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3Q2hlY2tCb3hDb2x1bW4gRmllbGROYW1lPSJBdXNnZWJsZW5kZXQiIE5hbWU9IkF1c2dlYmxlbmRldCIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IkZhbHNlIiBIZWFkZXJUZXh0PSJBdXNnZWJsZW5kZXQiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdEYXRlVGltZUNvbHVtbiBEYXRhVHlwZT0iU3lzdGVtLk51bGxhYmxlYDFbW1N5c3RlbS5EYXRlVGltZSwgbXNjb3JsaWIsIFZlcnNpb249NC4wLjAuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1iNzdhNWM1NjE5MzRlMDg5XV0iIFdpZHRoPSIxMzQiIEZpZWxkTmFtZT0iQmVhcmJlaXR1bmdzZGF0dW0iIE5hbWU9IkJlYXJiZWl0dW5nc2RhdHVtIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iQmVhcmJlaXR1bmdzZGF0dW0iIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIyNzMiIEZpZWxkTmFtZT0iQmVtZXJrdW5nIiBOYW1lPSJCZW1lcmt1bmciIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJCZW1lcmt1bmciIC8+PC9Db2x1bW5zPjxHcm91cERlc2NyaXB0b3JzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuR3JvdXBEZXNjcmlwdG9yPjxHcm91cE5hbWVzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuU29ydERlc2NyaXB0b3IgUHJvcGVydHlOYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIC8+PC9Hcm91cE5hbWVzPjwvVGVsZXJpay5XaW5Db250cm9scy5EYXRhLkdyb3VwRGVzY3JpcHRvcj48L0dyb3VwRGVzY3JpcHRvcnM+PFZpZXdEZWZpbml0aW9uIHhzaTp0eXBlPSJUZWxlcmlrLldpbkNvbnRyb2xzLlVJLlRhYmxlVmlld0RlZmluaXRpb24iIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIC8+PC9NYXN0ZXJUZW1wbGF0ZT48L1JhZEdyaWRWaWV3Pg=="
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
                    '   SetzeGridLayoutString(gridProzesse)

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
                    '       SetzeGridLayoutString(gridProzesseRHEWA)
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

    Private Shared Sub SetzeGridLayoutString(RadGridView As RadGridView)
        RadGridView.XmlSerializationInfo.DisregardOriginalSerializationVisibility = True
        RadGridView.XmlSerializationInfo.SerializationMetadata.Clear()

        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(RadGridView), "MasterTemplate", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "Templates", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "Caption", DesignerSerializationVisibilityAttribute.Visible)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "Columns", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewDataColumn), "Name", DesignerSerializationVisibilityAttribute.Visible)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewDataColumn), "Width", DesignerSerializationVisibilityAttribute.Visible)
        'Groups Descriptors
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "GroupDescriptors", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GroupDescriptor), "GroupNames", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(SortDescriptor), "PropertyName", DesignerSerializationVisibilityAttribute.Visible)
        'Sort Descriptors            
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "SortDescriptors", DesignerSerializationVisibilityAttribute.Content)
        RadGridView.XmlSerializationInfo.SerializationMetadata.Add(GetType(SortDescriptor), "Direction", DesignerSerializationVisibilityAttribute.Visible)


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
