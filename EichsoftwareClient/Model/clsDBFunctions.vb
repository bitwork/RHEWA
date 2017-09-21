Imports System.IO
Imports System.Runtime.Serialization
Imports EichsoftwareClient

''' <summary>
''' Klasse mit Funktionen für spezielle Aktionen gegen die Client Datenbank
''' </summary>
Public Class clsDBFunctions

    ' ''' <summary>
    ' ''' DEBUG Funktion um Lizenzdialog aus Testzwecken zu überspringen
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Shared Sub ForceActivation()
    '    Try
    '        Using DBContext As New Entities
    '            'prüfen ob die Lizenz gültig ist
    '            Dim HEKennung As String = "tim"
    '            Dim Schluessel As String = "Hill"

    '            Dim objLic As New Lizensierung
    '            objLic.HEKennung = HEKennung
    '            objLic.Lizenzschluessel = Schluessel

    '            objLic.RHEWALizenz = True

    '            Try
    '                'löschen der lokalen DB
    '                For Each lic In DBContext.Lizensierung
    '                    DBContext.Lizensierung.Remove(lic)
    '                Next
    '                DBContext.SaveChanges()
    '            Catch ex As Exception
    '            End Try
    '            Try
    '                'speichern in lokaler DB
    '                DBContext.Lizensierung.Add(objLic)
    '                DBContext.SaveChanges()
    '            Catch ex As Exception
    '            End Try

    '            My.Settings.LetzterBenutzer = objLic.Lizenzschluessel
    '            My.Settings.Save()
    '            AktuellerBenutzer.Instance.Lizenz.RHEWALizenz = objLic.RHEWALizenz

    '        End Using

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, My.Resources.GlobaleLokalisierung.Fehler)

    '    End Try
    'End Sub

    ''' <summary>
    ''' alle lokalen Benutzer aus Client DB löschen
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function LoescheLokaleBenutzer() As Boolean
        'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
        Using DBContext As New Entities
            For Each o In DBContext.Lizensierung.ToList
                DBContext.Lizensierung.Remove(o)

            Next
            For Each o In DBContext.Konfiguration.ToList
                DBContext.Konfiguration.Remove(o)
            Next
            DBContext.SaveChanges()
        End Using
    End Function

    ''' <summary>
    ''' Löscht die Hill Testlizenz
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function LoescheDevBenutzer() As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New Entities
                For Each o In DBContext.Lizensierung.Where(Function(p) p.Lizenzschluessel = "hill").ToList
                    DBContext.Lizensierung.Remove(o)

                Next
                For Each o In DBContext.Konfiguration.Where(Function(p) p.BenutzerLizenz = "hill").ToList
                    DBContext.Konfiguration.Remove(o)
                Next
                DBContext.SaveChanges()
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Verbindung zur DB öffnen
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CheckLocalDatabaseExists() As Boolean
        Using DBContext As New Entities
            Try
                DBContext.Database.Connection.Open()

            Catch ex As SqlServerCe.SqlCeException
                Return False
            End Try
            Return True
        End Using
    End Function
    ''' <summary>
    ''' Damit die Client DB bei Änderungen in der Entwicklung nicht überschrieben wird (copy allways), wird sie in einem Unterverzeichnis gespeichert. wenn beim Benutzer keine DB vorhanden ist, wird sie aus dem Unterverzeichnis in das vom Entity Framework verwendete Verzeichnis verschoben.
    ''' Änderungen an der DB müssen über die DB Updateroutine durchgeführt werden
    ''' </summary>
    ''' <returns></returns>
    Public Shared Sub CopyLocalDatabaseToApplicationFolder()
        Dim currentpath = Reflection.Assembly.GetExecutingAssembly().Location.Replace(Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name, "")
        Dim databasename = "EichsoftwareClientdatabase.sdf"
        If IO.File.Exists(currentpath & databasename) = False Then
            IO.File.Copy(currentpath & "Resources\" & databasename, currentpath & databasename)
        End If
    End Sub

    ''' <summary>
    ''' prüfen ob DB vorhanden ist und wenn nicht erzeugen
    ''' </summary>
    Friend Shared Sub prepareDatabase()
        If clsDBFunctions.CheckLocalDatabaseExists() = False Then
            clsDBFunctions.CopyLocalDatabaseToApplicationFolder()
        End If
    End Sub

    ''' <summary>
    ''' Updateroutine für Client Datenbanken
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function UpdateClientDatenbank()
        'IM Entity Framework dieser Version ist es nicht so einfach möglich zu prüfen, ob es felder gibt oder nicht. Ursprünglich gab es gar keine Versionierung der Datenbank
        'Sprich das Feld DBVersion welches geprüft wird, gab es noch gar nicht. Das kann ich nur durch die Exception herausfinden.
        'SPäter kam die Erkenntnis, dass es nicht sinnvoll ist die DB Version in der Konfiguration zu speichern, da diese Tabelle pro Lizenz eine Zeile einträgt
        'Die DB Version aber nicht an der Lizenz hängt. Deswegen wurde im späteren Schritt v4 auf eine neue Tabelle umgestellt, die nur zur Versionsverwaltung dient. Diese kann auch ohne Lizenz Updates durchführen
        UpdateClientDatenbankAlt()


        'Ab hier kann nun sauber mit der DB Version gearbeit werden. Der Code oben ist historisch gewachsen. Ab hier sollte es nun vernünftig funktioneren
        Using DBContext As New Entities
            Dim objVersion = DBContext.Datenbankversion.FirstOrDefault
            If Not objVersion Is Nothing Then
                If objVersion.version < 5 Then
                    UpdateVersion5(DBContext)
                End If
                'TODO bei neuen updates entsprechend weiterführen
                'If objVersion.version < 6 Then
                '    UpdateVersion6(DBContext)
                'End If
                'If objVersion.version < 7 Then
                '    UpdateVersion7(DBContext)
                'End If
            End If
        End Using


        If Debugger.IsAttached = False Then
            'Löscht die Hill Testlizenz falls vorhanden
            LoescheDevBenutzer()
        End If

        Return True
    End Function

    ''' <summary>
    ''' IM Entity Framework dieser Version ist es nicht so einfach möglich zu prüfen, ob es felder gibt oder nicht. Ursprünglich gab es gar keine Versionierung der Datenbank
    '''Sprich das Feld DBVersion welches geprüft wird, gab es noch gar nicht. Das kann ich nur durch die Exception herausfinden.
    '''SPäter kam die Erkenntnis, dass es nicht sinnvoll ist die DB Version in der Konfiguration zu speichern, da diese Tabelle pro Lizenz eine Zeile einträgt
    '''Die DB Version aber nicht an der Lizenz hängt. Deswegen wurde im späteren Schritt v4 auf eine neue Tabelle umgestellt, die nur zur Versionsverwaltung dient. Diese kann auch ohne Lizenz Updates durchführen
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function UpdateClientDatenbankAlt() As Boolean
        'WICHTIG HIER DRAN NICHTS MEHR ÄNDERN
        Try
            Using DBContext As New Entities

                Dim config = DBContext.Konfiguration.FirstOrDefault
                If config.DBVersion < 2 Then
                    'Der Aufruf wird nur getätigt, damit die Exception auftrtt, falls das Feld DBVersion gar nicht existiert
                End If
            End Using
        Catch ex As Entity.Core.EntityCommandExecutionException
            Try
                Using DBContext As New Entities
                    'Spalte DB Version existiert noch nicht
                    Dim updateScript As String = ""
                    updateScript = "ALTER TABLE [Konfiguration] ADD [DBVersion] nvarchar(25) DEFAULT '1' NULL"
                    DBContext.Database.ExecuteSqlCommand(updateScript)
                    'erneuter aufruf der Update Routine
                    UpdateClientDatenbank()
                End Using
            Catch ex2 As Exception
            End Try
        Catch ex As InvalidOperationException ' keine Lizenz eingetragen
            Return False
        End Try

        Using DBContext As New Entities
            Dim config = DBContext.Konfiguration.FirstOrDefault
            If config.DBVersion < 2 Then
                UpdateVersion2(DBContext)
            End If

            If config.DBVersion < 3 Then
                UpdateVersion3(DBContext)
            End If
        End Using

        'WICHTIG ab hier neue Tabelle mit Versionsinformationen damit keine Lizenz vorhanden sein muss um die änderungen einzutragen...
        Try
            Using DBContext As New Entities


                Dim objVersion = DBContext.Datenbankversion.FirstOrDefault
                If objVersion Is Nothing Then

                End If
            End Using
        Catch ex As Entity.Core.EntityCommandExecutionException
            Try
                Using DBContext As New Entities
                    updateVersion4(DBContext)
                End Using
            Catch ex2 As Exception
            End Try
        End Try
    End Function



#Region "DB Updates"
    ''' <summary>
    ''' erhöhen der maximalen Feldgröße von den Identifkationsdaten
    ''' </summary>
    ''' <param name="DBContext"></param>
    Private Shared Sub UpdateVersion2(DBContext As Entities)

        Dim updateScript As String = ""
        updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Aufstellungsort] nvarchar(4000) NULL"
        DBContext.Database.ExecuteSqlCommand(updateScript)

        updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Pruefer] nvarchar(4000) NULL"
        DBContext.Database.ExecuteSqlCommand(updateScript)

        updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Identifikationsdaten_Benutzer] nvarchar(4000) NULL"
        DBContext.Database.ExecuteSqlCommand(updateScript)

        updateScript = "Alter TABLE [Eichprotokoll] Alter Column  [Beschaffenheitspruefung_Pruefscheinnummer] nvarchar(4000) NULL"

        DBContext.Database.ExecuteSqlCommand(updateScript)

        Dim Konfigs = DBContext.Konfiguration
        For Each Konfig In Konfigs
            Konfig.DBVersion = 2
        Next

        DBContext.SaveChanges()
    End Sub

    ''' <summary>
    ''' Eichmarkenverwaltung wurde umgebaut
    ''' </summary>
    ''' <param name="DBContext"></param>
    Private Shared Sub UpdateVersion3(DBContext As Entities)
        Dim updateScript As String = ""


        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_SicherungsmarkeKlein] bit DEFAULT ((0)) NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] SET Sicherung_SicherungsmarkeKlein = Sicherung_Eichsiegel13x13"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_Eichsiegel13x13]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_SicherungsmarkeKleinAnzahl] smallint NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] Set Sicherung_SicherungsmarkeKleinAnzahl = Sicherung_Eichsiegel13x13Anzahl"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_Eichsiegel13x13Anzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_SicherungsmarkeGross] bit DEFAULT ((0)) NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] SET Sicherung_SicherungsmarkeGross = Sicherung_EichsiegelRund"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_EichsiegelRund]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_SicherungsmarkeGrossAnzahl] smallint NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] Set Sicherung_SicherungsmarkeGrossAnzahl = Sicherung_EichsiegelRundAnzahl"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_EichsiegelRundAnzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_Hinweismarke] bit DEFAULT ((0)) NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] SET Sicherung_Hinweismarke= Sicherung_HinweismarkeGelocht"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_HinweismarkeGelocht]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] ADD [Sicherung_HinweismarkeAnzahl] smallint NULL "
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "UPDATE [Eichprotokoll] Set Sicherung_HinweismarkeAnzahl = Sicherung_HinweismarkeGelochtAnzahl"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_HinweismarkeGelochtAnzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_GruenesM]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_GruenesMAnzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_CE]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_CEAnzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_CE2016]"
        DBContext.Database.ExecuteSqlCommand(updateScript)
        updateScript = "ALTER TABLE [Eichprotokoll] DROP COLUMN [Sicherung_CE2016Anzahl]"
        DBContext.Database.ExecuteSqlCommand(updateScript)



        Dim Konfigs = DBContext.Konfiguration
        For Each Konfig In Konfigs
            Konfig.DBVersion = 3
        Next

        DBContext.SaveChanges()
    End Sub


    ''' <summary>
    ''' neue Tabelle für Versionierung erzeugen
    ''' </summary>
    ''' <param name="DBContext"></param>
    Private Shared Sub updateVersion4(DBContext As Entities)
        Try
            Dim updateScript As String = ""
            updateScript = "CREATE TABLE [Datenbankversion] (  [id] Int IDENTITY (1, 1) Not NULL, [version] int Not NULL)"
            DBContext.Database.ExecuteSqlCommand(updateScript)
            updateScript = "ALTER TABLE [Datenbankversion] ADD CONSTRAINT [PK_Datenbankversion] PRIMARY KEY ([id])"
            DBContext.Database.ExecuteSqlCommand(updateScript)
        Catch ex As Exception

        End Try

        Try
            Dim objVersion = DBContext.Datenbankversion.FirstOrDefault
            If objVersion Is Nothing Then
                objVersion = New Datenbankversion
                objVersion.version = 4
                DBContext.Datenbankversion.Add(objVersion)
                DBContext.SaveChanges()
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' anpassen polnische lokalisierung
    ''' Erhöhen der Wägezellenfabrik nummer auf 250 zeichen
    ''' </summary>
    ''' <param name="DBContext"></param>
    Private Shared Sub UpdateVersion5(DBContext As Entities)
        Dim updateScript As String = ""

        updateScript = "ALTER TABLE [Eichprotokoll] Alter Column  [Komponenten_WaegezellenFabriknummer] nvarchar(250) NULL"
        DBContext.Database.ExecuteSqlCommand(updateScript)

        'übersätzung an Status 
        DBContext.Lookup_Vorgangsstatus.Where(Function(x) x.ID = 2).First.Status_PL = "Wpis danych"
        DBContext.Lookup_Vorgangsstatus.Where(Function(x) x.ID = 18).First.Status_PL = "Przyspieszenie grawitacyjne"
        DBContext.Lookup_Vorgangsstatus.Where(Function(x) x.ID = 21).First.Status_PL = "Wysłane"



        Dim objVersion = DBContext.Datenbankversion.FirstOrDefault
        If Not objVersion Is Nothing Then
                objVersion.version = 5
                DBContext.SaveChanges()
            End If

    End Sub
#End Region


    ''' <summary>
    '''  löscht lokale Datenbank, für resyncronisierung des aktuellen Benutzers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LoescheLokaleDatenbank() As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New Entities
                DBContext.Configuration.ProxyCreationEnabled = False
                Dim Eichprozesse = (From eichprozess In DBContext.Eichprozess Where eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel And eichprozess.FK_Bearbeitungsstatus <> GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet).ToList

                'liste mit gelöschten eichprozessen
                Dim listIDsEichprozess As New List(Of String)
                Dim listIDsEichprotokoll As New List(Of String)
                Dim listIDsKompatiblitaetsnachweis As New List(Of String)
                DBContext.Configuration.AutoDetectChangesEnabled = False

                'aufbereiten der Listen mit allen IDS.
                For Each obj In Eichprozesse
                    If obj.FK_Eichprotokoll Is Nothing Then Continue For
                    listIDsEichprozess.Add(obj.ID)
                    listIDsEichprotokoll.Add(obj.FK_Eichprotokoll)

                    If Not listIDsKompatiblitaetsnachweis.Contains(obj.FK_Kompatibilitaetsnachweis) Then
                        listIDsKompatiblitaetsnachweis.Add(obj.FK_Kompatibilitaetsnachweis)
                    End If

                    DBContext.Eichprozess.Remove(obj)
                Next

                Dim listMogelstatistik = (From Mogelstatistik In DBContext.Mogelstatistik).ToList
                For Each ID In listIDsEichprozess
                    Dim Mogelstatistiken = From Mogelstatistik In listMogelstatistik Where Mogelstatistik.FK_Eichprozess = ID
                    For Each obj In Mogelstatistiken
                        DBContext.Mogelstatistik.Remove(obj)
                    Next
                Next

                Dim listKompnachweis = (From Kompatiblitaetsnachweis In DBContext.Kompatiblitaetsnachweis).ToList
                For Each ID In listIDsKompatiblitaetsnachweis
                    Dim Kompatiblitaetsnachweise = From Kompatiblitaetsnachweis In listKompnachweis Where Kompatiblitaetsnachweis.ID = ID
                    For Each obj In Kompatiblitaetsnachweise
                        DBContext.Kompatiblitaetsnachweis.Remove(obj)
                    Next
                Next

                Dim listEichprotokoll = (From Kompatiblitaetsnachweis In DBContext.Eichprotokoll).ToList

                Dim listPruefungAnsprechvermoegen = DBContext.PruefungAnsprechvermoegen.ToList
                Dim listPruefungLinearitaetFallend = DBContext.PruefungLinearitaetFallend.ToList
                Dim listPruefungLinearitaetSteigend = DBContext.PruefungLinearitaetSteigend.ToList
                Dim listPruefungRollendeLasten = DBContext.PruefungRollendeLasten.ToList
                Dim listPruefungAussermittigeBelastung = DBContext.PruefungAussermittigeBelastung.ToList
                Dim listPruefungStabilitaetGleichgewichtslage = DBContext.PruefungStabilitaetGleichgewichtslage.ToList
                Dim listPruefungStaffelverfahrenErsatzlast = DBContext.PruefungStaffelverfahrenErsatzlast.ToList
                Dim listPruefungStaffelverfahrenNormallast = DBContext.PruefungStaffelverfahrenNormallast.ToList
                Dim listPruefungWiederholbarkeit = DBContext.PruefungWiederholbarkeit.ToList

                Dim eichprotokolle = From eichprotokoll In listEichprotokoll Where listIDsEichprotokoll.Contains(eichprotokoll.ID)
                For Each obj In eichprotokolle
                    DBContext.Eichprotokoll.Remove(obj)
                Next

                Dim PruefungAnsprechvermoegen = From pruefung In listPruefungAnsprechvermoegen Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next

                Dim PruefungLinearitaetFallend = From pruefung In listPruefungLinearitaetFallend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next

                Dim PruefungLinearitaetSteigend = From pruefung In listPruefungLinearitaetSteigend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next

                Dim PruefungRollendeLasten = From pruefung In listPruefungRollendeLasten Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next

                Dim PruefungAussermittigeBelastung = From pruefung In listPruefungAussermittigeBelastung Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)

                For Each obj In PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next

                Dim PruefungStabilitaetGleichgewichtslage = From pruefung In listPruefungStabilitaetGleichgewichtslage Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenErsatzlast = From pruefung In listPruefungStaffelverfahrenErsatzlast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenNormallast = From pruefung In listPruefungStaffelverfahrenNormallast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next

                Dim PruefungWiederholbarkeit = From pruefung In listPruefungWiederholbarkeit Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next
                DBContext.ChangeTracker.DetectChanges()

                DBContext.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                MessageBox.Show(ex.InnerException.StackTrace, ex.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    MessageBox.Show(ex.InnerException.InnerException.StackTrace, ex.InnerException.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex3 As Exception
                End Try
            Catch ex2 As Exception
            End Try

            Return False
        End Try
    End Function

    ''' <summary>
    ''' Funktion welche prüft ob es noch lokale Eichvorgänge gibt, die nicht an RHEWA versand wurden
    ''' </summary>
    ''' <returns>True wenn noch Eichungen gefunden wurden</returns>
    ''' <remarks>Wird z.b. Genutzt um zu Warnen, bevor ein Benutzer seine lokale DB löscht um eine Teilsynchronisierung vorzunehmen</remarks>
    Public Shared Function PruefeAufUngesendeteEichungen() As Boolean
        Using dbcontext As New Entities
            Dim query = From eichungen In dbcontext.Eichprozess Where eichungen.FK_Vorgangsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet
            If query.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Using
    End Function

    ''' <summary>
    ''' Holt Lizenzobjekt aus DB passend zum Lizenzschlüssel
    ''' </summary>
    ''' <param name="pLizenzschluessel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleLizenzObjekt(ByVal pLizenzschluessel As String) As Lizensierung
        Try
            Using context As New Entities
                Dim objLic = (From lizenz In context.Lizensierung Where lizenz.Lizenzschluessel = pLizenzschluessel)
                Dim listLics As New List(Of Lizensierung)
                listLics = objLic.ToList
                If listLics.Count = 1 Then
                    Return listLics(0)
                Else
                    MessageBox.Show("Kein Eindeutiger Schlüssel gefunden. Es gibt Benutzer mit dem selben Lizenzschlüssel")
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Erzeugt neues leeres Eichprozessobject aus Datenbank
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ErzeugeNeuenEichprozess() As Eichprozess
        Dim objEichprozess As Eichprozess = Nothing
        Using context As New Entities
            objEichprozess = context.Eichprozess.Create
            'pflichtfelder füllen
            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
            objEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
        End Using

        Return objEichprozess
    End Function

    ''' <summary>
    ''' Holt Eichprozess Objekt aus lokaler DB anhand von Vorgangsnummer
    ''' </summary>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleVorhandenenEichprozess(ByVal Vorgangsnummer As String) As Eichprozess
        Using Context As New Entities
            Dim objEichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = Vorgangsnummer).FirstOrDefault
            Return objEichprozess
        End Using
    End Function

    ''' <summary>
    ''' Lädt alle Zusatztabellen zum Eichprozess Objekt aus DB. Wird verwendet wenn Lazy Loading deaktiviert wurde
    ''' </summary>
    ''' <param name="objEichprozess"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HoleNachschlageListenFuerEichprozess(ByVal objEichprozess As Eichprozess) As Eichprozess
        Using Context As New Entities
            objEichprozess = (From Obj In Context.Eichprozess.AsNoTracking.Include("Eichprotokoll") _
                              .Include("Lookup_Auswertegeraet").AsNoTracking _
                              .Include("Kompatiblitaetsnachweis").AsNoTracking _
                              .Include("Lookup_Waegezelle").AsNoTracking _
                              .Include("Lookup_Waagenart").AsNoTracking _
                              .Include("Lookup_Waagentyp").AsNoTracking _
                                                            .Include("Mogelstatistik").AsNoTracking
                              Select Obj Where Obj.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)

            objEichprozess.Lookup_Vorgangsstatus = (From f1 In Context.Lookup_Vorgangsstatus.AsNoTracking Where f1.ID = objEichprozess.FK_Vorgangsstatus Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagenart = (From f1 In Context.Lookup_Waagenart.AsNoTracking Where f1.ID = objEichprozess.FK_WaagenArt Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagentyp = (From f1 In Context.Lookup_Waagentyp.AsNoTracking Where f1.ID = objEichprozess.FK_WaagenTyp Select f1).FirstOrDefault
            objEichprozess.Lookup_Bearbeitungsstatus = (From f1 In Context.Lookup_Bearbeitungsstatus.AsNoTracking Where f1.ID = objEichprozess.FK_Bearbeitungsstatus Select f1).FirstOrDefault
            objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f1 In Context.Lookup_Konformitaetsbewertungsverfahren.AsNoTracking Where f1.ID = objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f1).FirstOrDefault
            objEichprozess.Kompatiblitaetsnachweis = (From f1 In Context.Kompatiblitaetsnachweis.AsNoTracking Where f1.ID = objEichprozess.FK_Kompatibilitaetsnachweis Select f1).FirstOrDefault
            objEichprozess.Lookup_Auswertegeraet = (From f1 In Context.Lookup_Auswertegeraet.AsNoTracking Where f1.ID = objEichprozess.FK_Auswertegeraet Select f1).FirstOrDefault
            objEichprozess.Lookup_Waegezelle = (From f1 In Context.Lookup_Waegezelle.AsNoTracking Where f1.ID = objEichprozess.FK_Waegezelle Select f1).FirstOrDefault

            Return objEichprozess
        End Using

    End Function

    ''' <summary>
    ''' Setzt flag zum ausblenden / einblenden eines Vorgangs
    ''' </summary>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function BlendeEichprozessAus(ByVal Vorgangsnummer As String) As Boolean
        Using context As New Entities
            Dim objEichprozess = (From Obj In context.Eichprozess Select Obj Where Obj.Vorgangsnummer = Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben
            If Not objEichprozess Is Nothing Then
                'umdrehen des ausgeblendet Statuses
                objEichprozess.Ausgeblendet = Not objEichprozess.Ausgeblendet
                context.SaveChanges()
                Return True
            End If
            Return False
        End Using
    End Function

    ''' <summary>
    ''' Lädt alle lokalen DS des aktuellen Benutzers
    ''' </summary>
    ''' <param name="bolAusgeblendeteElementeAnzeigen"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LadeLokaleEichprozessListe(ByVal bolAusgeblendeteElementeAnzeigen As Boolean) As Object
        'neuen Context aufbauen
        Using Context As New Entities
            Context.Configuration.LazyLoadingEnabled = True

            'je nach Sprache die Abfrage anpassen um die entsprechenden Übersetzungen der Lookupwerte aus der DB zu laden
            Select Case AktuellerBenutzer.Instance.AktuelleSprache.ToLower
                Case "de"

                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                 {
                         .Status = Eichprozess.Lookup_Vorgangsstatus.Status,
                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status,
                         Eichprozess.ID,
                         Eichprozess.Vorgangsnummer,
                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                         .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                         .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ,
                         .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art,
                         .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                         Eichprozess.Ausgeblendet,
                         Eichprozess.Bearbeitungsdatum,
                           .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                                      }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "en"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                              .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN,
                                                        Eichprozess.ID,
                                                        Eichprozess.Vorgangsnummer,
                                                          .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_EN,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                           .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "pl"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_PL,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_PL,
                                                        Eichprozess.ID,
                                                        Eichprozess.Vorgangsnummer,
                                                           .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_PL,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_PL,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                         .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case Else
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können.
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess
                               Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen And Eichprozess.ErzeugerLizenz = AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel
                               Select New With
                                                {
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN,
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN,
                                                        Eichprozess.ID, Eichprozess.Vorgangsnummer,
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer,
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ,
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, .Lookup_Waagentyp_EN = Eichprozess.Lookup_Waagentyp.Typ_EN, .Lookup_Waagentyp_PL = Eichprozess.Lookup_Waagentyp.Typ_PL,
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, .Lookup_Waagenart_EN = Eichprozess.Lookup_Waagenart.Art_EN, .Lookup_Waagenart_PL = Eichprozess.Lookup_Waagenart.Art_PL,
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ,
                                                        Eichprozess.Ausgeblendet,
                                                         Eichprozess.Bearbeitungsdatum,
                                                    .Bemerkung = Eichprozess.Eichprotokoll.Sicherung_Bemerkungen
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
            End Select

        End Using
    End Function

    ''' <summary>
    ''' Support Option um die Datenbank auf den RHEWA FTP zu schieben. Benutzt im Optionsdialog
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function SendLocalDatabaseToRHEWAFTP()
        Dim objFTP As New clsFTP
        Dim path As String
        Try 'clickonce pfad
            Try
                path = Deployment.Application.ApplicationDeployment.CurrentDeployment?.DataDirectory & "\EichsoftwareClientdatabase.sdf"
            Catch ex2 As Exception
                path = Application.StartupPath & "\EichsoftwareClientdatabase.sdf"
            End Try

            If path = "" Then Return False

            Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                Try
                    Webcontext.Open()
                Catch ex As Exception
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'daten von WebDB holen
                Dim objFTPDaten = Webcontext.GetFTPCredentials("internal", "999999", "999999", My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                'aufbereiten der für FTP benötigten Verbindungsdaten
                If Not objFTPDaten Is Nothing Then

                    'get decrypted Password and configuration from Database

                    Dim password As String = objFTPDaten.FTPEncryptedPassword
                    ' original plaintext
                    Dim passPhrase As String = "Pas5pr@se"
                    ' can be any string
                    Dim saltValue As String = objFTPDaten.FTPSaltKey
                    ' can be any string
                    Dim hashAlgorithm As String = "SHA1"
                    ' can be "MD5"
                    Dim passwordIterations As Integer = 2
                    ' can be any number
                    Dim initVector As String = "@1B2c3D4e5F6g7H8"
                    ' must be 16 bytes
                    Dim keySize As Integer = 256
                    ' can be 192 or 128
                    Dim UploadfilePath = path

                    password = RijndaelSimple.Decrypt(password, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
                    keySize)

                    'datei upload. FTPUploadPath bekommt den reelen Pfad auf dem FTP Server
                    Dim FTPUploadPath = objFTP.UploadDatabaseFiletoFTP(objFTPDaten.FTPServername, objFTPDaten.FTPUserName, password, UploadfilePath, My.Computer.Name)
                End If
            End Using

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    ''' <summary>
    ''' lokalen eichprozess löschen
    ''' </summary>
    ''' <param name="Vorgangsnummer"></param>
    ''' <returns></returns>
    Public Shared Function LoescheEichprozess(Vorgangsnummer As String) As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New Entities
                DBContext.Configuration.ProxyCreationEnabled = False
                Dim Eichprozesse = (From eichprozess In DBContext.Eichprozess Where eichprozess.Vorgangsnummer = Vorgangsnummer).ToList

                'liste mit gelöschten eichprozessen
                Dim listIDsEichprozess As New List(Of String)
                Dim listIDsEichprotokoll As New List(Of String)
                Dim listIDsKompatiblitaetsnachweis As New List(Of String)
                DBContext.Configuration.AutoDetectChangesEnabled = False

                'aufbereiten der Listen mit allen IDS.
                For Each obj In Eichprozesse
                    DBContext.Eichprozess.Remove(obj)
                    listIDsEichprozess.Add(obj.ID)
                    If obj.FK_Eichprotokoll Is Nothing Then Continue For

                    listIDsEichprotokoll.Add(obj.FK_Eichprotokoll)

                    If Not listIDsKompatiblitaetsnachweis.Contains(obj.FK_Kompatibilitaetsnachweis) Then
                        listIDsKompatiblitaetsnachweis.Add(obj.FK_Kompatibilitaetsnachweis)
                    End If

                Next

                Dim listMogelstatistik = (From Mogelstatistik In DBContext.Mogelstatistik).ToList
                For Each ID In listIDsEichprozess
                    Dim Mogelstatistiken = From Mogelstatistik In listMogelstatistik Where Mogelstatistik.FK_Eichprozess = ID
                    For Each obj In Mogelstatistiken
                        DBContext.Mogelstatistik.Remove(obj)
                    Next
                Next

                Dim listKompnachweis = (From Kompatiblitaetsnachweis In DBContext.Kompatiblitaetsnachweis).ToList
                For Each ID In listIDsKompatiblitaetsnachweis
                    Dim Kompatiblitaetsnachweise = From Kompatiblitaetsnachweis In listKompnachweis Where Kompatiblitaetsnachweis.ID = ID
                    For Each obj In Kompatiblitaetsnachweise
                        DBContext.Kompatiblitaetsnachweis.Remove(obj)
                    Next
                Next

                Dim listEichprotokoll = (From Kompatiblitaetsnachweis In DBContext.Eichprotokoll).ToList

                Dim listPruefungAnsprechvermoegen = DBContext.PruefungAnsprechvermoegen.ToList
                Dim listPruefungLinearitaetFallend = DBContext.PruefungLinearitaetFallend.ToList
                Dim listPruefungLinearitaetSteigend = DBContext.PruefungLinearitaetSteigend.ToList
                Dim listPruefungRollendeLasten = DBContext.PruefungRollendeLasten.ToList
                Dim listPruefungAussermittigeBelastung = DBContext.PruefungAussermittigeBelastung.ToList
                Dim listPruefungStabilitaetGleichgewichtslage = DBContext.PruefungStabilitaetGleichgewichtslage.ToList
                Dim listPruefungStaffelverfahrenErsatzlast = DBContext.PruefungStaffelverfahrenErsatzlast.ToList
                Dim listPruefungStaffelverfahrenNormallast = DBContext.PruefungStaffelverfahrenNormallast.ToList
                Dim listPruefungWiederholbarkeit = DBContext.PruefungWiederholbarkeit.ToList

                Dim eichprotokolle = From eichprotokoll In listEichprotokoll Where listIDsEichprotokoll.Contains(eichprotokoll.ID)
                For Each obj In eichprotokolle
                    DBContext.Eichprotokoll.Remove(obj)
                Next

                Dim PruefungAnsprechvermoegen = From pruefung In listPruefungAnsprechvermoegen Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next

                Dim PruefungLinearitaetFallend = From pruefung In listPruefungLinearitaetFallend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next

                Dim PruefungLinearitaetSteigend = From pruefung In listPruefungLinearitaetSteigend Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next

                Dim PruefungRollendeLasten = From pruefung In listPruefungRollendeLasten Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next

                Dim PruefungAussermittigeBelastung = From pruefung In listPruefungAussermittigeBelastung Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)

                For Each obj In PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next

                Dim PruefungStabilitaetGleichgewichtslage = From pruefung In listPruefungStabilitaetGleichgewichtslage Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenErsatzlast = From pruefung In listPruefungStaffelverfahrenErsatzlast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next

                Dim PruefungStaffelverfahrenNormallast = From pruefung In listPruefungStaffelverfahrenNormallast Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next

                Dim PruefungWiederholbarkeit = From pruefung In listPruefungWiederholbarkeit Where listIDsEichprotokoll.Contains(pruefung.FK_Eichprotokoll)
                For Each obj In PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next
                DBContext.ChangeTracker.DetectChanges()

                DBContext.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Try
                MessageBox.Show(ex.InnerException.StackTrace, ex.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    MessageBox.Show(ex.InnerException.InnerException.StackTrace, ex.InnerException.InnerException.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex3 As Exception
                End Try
            Catch ex2 As Exception
            End Try

            Return False
        End Try
    End Function
End Class