Public Class clsServerHelper

    ''' <summary>
    ''' Methode welche alle N:1 Verbindungen auf einen Eichprozess entfernt 
    ''' </summary>
    ''' <param name="TargetObject"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteForeignTables(ByRef TargetObject As ServerEichprozess)
        Try

            Using dbcontext As New EichenSQLDatabaseEntities1
                Dim EichprozessID As String = TargetObject.ID
                Dim EichprotokollID As String = TargetObject.ServerEichprotokoll.ID


                Dim mogelobjqery = (From db In dbcontext.ServerMogelstatistik Select db Where db.FK_Eichprozess = EichprozessID)
                For Each obj In mogelobjqery
                    dbcontext.ServerMogelstatistik.Remove(obj)
                Next
                dbcontext.SaveChanges()
                'neu laden der instanz damit TRacking des Contextes aktiv ist
                TargetObject = (From d In dbcontext.ServerEichprozess.Include("ServerEichprotokoll").Include("ServerBeschaffenheitspruefung").Include("ServerKompatiblitaetsnachweis") Where d.ID = EichprozessID Select d).FirstOrDefault

                'prüfungen
                Try
                    'aufräumen und alte löschen
                    Dim query = From a In dbcontext.ServerPruefungAnsprechvermoegen Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query
                        dbcontext.ServerPruefungAnsprechvermoegen.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try


                Try
                    'aufräumen und alte löschen
                    Dim query2 = From a In dbcontext.ServerPruefungAussermittigeBelastung Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query2
                        dbcontext.ServerPruefungAussermittigeBelastung.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try


                Try
                    Dim query4 = From a In dbcontext.ServerPruefungLinearitaetFallend Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query4
                        dbcontext.ServerPruefungLinearitaetFallend.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query5 = From a In dbcontext.ServerPruefungLinearitaetSteigend Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query5
                        dbcontext.ServerPruefungLinearitaetSteigend.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query6 = From a In dbcontext.ServerPruefungRollendeLasten Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query6
                        dbcontext.ServerPruefungRollendeLasten.Remove(obj)
                    Next
                    dbcontext.SaveChanges()


                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query6 = From a In dbcontext.ServerPruefungStabilitaetGleichgewichtslage Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query6
                        dbcontext.ServerPruefungStabilitaetGleichgewichtslage.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query7 = From a In dbcontext.ServerPruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query7
                        dbcontext.ServerPruefungStaffelverfahrenErsatzlast.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                'aufräumen und alte löschen

                Try
                    Dim query8 = From a In dbcontext.ServerPruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query8
                        dbcontext.ServerPruefungStaffelverfahrenNormallast.Remove(obj)
                    Next
                    dbcontext.SaveChanges()
                Catch e As Exception
                End Try

                Try
                    'aufräumen und alte löschen
                    Dim query9 = From a In dbcontext.ServerPruefungWiederholbarkeit Where a.FK_Eichprotokoll = EichprotokollID
                    For Each obj In query9
                        dbcontext.ServerPruefungWiederholbarkeit.Remove(obj)
                    Next
                    dbcontext.SaveChanges()

                Catch e As Exception
                End Try


                dbcontext.ServerEichprotokoll.Remove(TargetObject.ServerEichprotokoll)
                dbcontext.SaveChanges()
                dbcontext.ServerBeschaffenheitspruefung.Remove(TargetObject.ServerBeschaffenheitspruefung)
                dbcontext.SaveChanges()
                dbcontext.ServerKompatiblitaetsnachweis.Remove(TargetObject.ServerKompatiblitaetsnachweis)
                dbcontext.SaveChanges()
            End Using
        Catch ex As Exception
        End Try
    End Sub

  


End Class
