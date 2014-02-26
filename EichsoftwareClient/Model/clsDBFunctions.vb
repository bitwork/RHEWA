Public Class clsDBFunctions

    ''' <summary>
    '''  löscht lokale Datenbank, für resyncronisierung
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoescheLokaleDatenbank() As Boolean
        Try
            'alle Tabellen iterieren und löschen. Das commit wird erst am Ende ausgeführt, deswegen ist die löschreihenefolge egal
            Using DBContext As New EichsoftwareClientdatabaseEntities1

                For Each obj In DBContext.Eichprozess
                    DBContext.Eichprozess.Remove(obj)
                Next
                For Each obj In DBContext.Eichprotokoll
                    DBContext.Eichprotokoll.Remove(obj)
                Next
                For Each obj In DBContext.Beschaffenheitspruefung
                    DBContext.Beschaffenheitspruefung.Remove(obj)
                Next
                For Each obj In DBContext.Mogelstatistik
                    DBContext.Mogelstatistik.Remove(obj)
                Next
                For Each obj In DBContext.Kompatiblitaetsnachweis
                    DBContext.Kompatiblitaetsnachweis.Remove(obj)
                Next
                For Each obj In DBContext.PruefungAnsprechvermoegen
                    DBContext.PruefungAnsprechvermoegen.Remove(obj)
                Next
                For Each obj In DBContext.PruefungEichfehlergrenzen
                    DBContext.PruefungEichfehlergrenzen.Remove(obj)
                Next
                For Each obj In DBContext.PruefungLinearitaetFallend
                    DBContext.PruefungLinearitaetFallend.Remove(obj)
                Next
                For Each obj In DBContext.PruefungLinearitaetSteigend
                    DBContext.PruefungLinearitaetSteigend.Remove(obj)
                Next
                For Each obj In DBContext.PruefungRollendeLasten
                    DBContext.PruefungRollendeLasten.Remove(obj)
                Next
                For Each obj In DBContext.PruefungAussermittigeBelastung
                    DBContext.PruefungAussermittigeBelastung.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStabilitaetGleichgewichtslage
                    DBContext.PruefungStabilitaetGleichgewichtslage.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStaffelverfahrenErsatzlast
                    DBContext.PruefungStaffelverfahrenErsatzlast.Remove(obj)
                Next
                For Each obj In DBContext.PruefungStaffelverfahrenNormallast
                    DBContext.PruefungStaffelverfahrenNormallast.Remove(obj)
                Next
                For Each obj In DBContext.PruefungWiederholbarkeit
                    DBContext.PruefungWiederholbarkeit.Remove(obj)
                Next

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
    Public Function PruefeAufUngesendeteEichungen() As Boolean
        Using dbcontext As New EichsoftwareClientdatabaseEntities1
            Dim query = From eichungen In dbcontext.Eichprozess Where eichungen.FK_Vorgangsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet
            If query.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Using
    End Function

    Public Function HoleLizenzObjekt() As Lizensierung
        Dim objLic As Lizensierung
        Using context As New EichsoftwareClientdatabaseEntities1
            objLic = (From lizenz In context.Lizensierung).FirstOrDefault
            Return objLic
        End Using
    End Function

    ''' <summary>
    ''' Erzeugt neues leeres Eichprozessobject aus Datenbank
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ErzeugeNeuenEichprozess() As Eichprozess
        Dim objEichprozess As Eichprozess = Nothing
        Using context As New EichsoftwareClientdatabaseEntities1
            objEichprozess = context.Eichprozess.Create
            'pflichtfelder füllen
            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe
            objEichprozess.Vorgangsnummer = Guid.NewGuid.ToString
        End Using

        Return objEichprozess
    End Function

    Public Function HoleVorhandenenEichprozess(ByVal Vorgangsnummer As String) As Eichprozess
        Using Context As New EichsoftwareClientdatabaseEntities1
            Dim objEichprozess = (From Obj In Context.Eichprozess Select Obj Where Obj.Vorgangsnummer = Vorgangsnummer).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)
            Return objEichprozess
        End Using
    End Function

    Public Function HoleNachschlageListenFuerEichprozess(ByVal objEichprozess As Eichprozess) As Eichprozess
        Using Context As New EichsoftwareClientdatabaseEntities1
            'es gibt ihn schon und er ist bereits abgeschickt. nur lesend öffnen
            objEichprozess = (From Obj In Context.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select Obj Where Obj.ID = objEichprozess.ID).FirstOrDefault 'firstor default um erstes element zurückzugeben das übereintrifft(bei ID Spalten sollte es eh nur 1 sein)
            objEichprozess.Lookup_Vorgangsstatus = (From f1 In Context.Lookup_Vorgangsstatus Where f1.ID = objEichprozess.FK_Vorgangsstatus Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagenart = (From f1 In Context.Lookup_Waagenart Where f1.ID = objEichprozess.FK_WaagenArt Select f1).FirstOrDefault
            objEichprozess.Lookup_Waagentyp = (From f1 In Context.Lookup_Waagentyp Where f1.ID = objEichprozess.FK_WaagenTyp Select f1).FirstOrDefault
            objEichprozess.Lookup_Bearbeitungsstatus = (From f1 In Context.Lookup_Bearbeitungsstatus Where f1.ID = objEichprozess.FK_Bearbeitungsstatus Select f1).FirstOrDefault
            objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren = (From f1 In Context.Lookup_Konformitaetsbewertungsverfahren Where f1.ID = objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren Select f1).FirstOrDefault

            Return objEichprozess
        End Using

    End Function

    Public Function BlendeEichprozessAus(ByVal Vorgangsnummer As String) As Boolean
        Using context As New EichsoftwareClientdatabaseEntities1
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

    Public Function LadeLokaleEichprozessListe(ByVal bolAusgeblendeteElementeAnzeigen As Boolean) As Object
        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
            Context.Configuration.LazyLoadingEnabled = True
            'je nach Sprache die Abfrage anpassen um die entsprechenden Übersetzungen der Lookupwerte aus der DB zu laden
            Select Case My.Settings.AktuelleSprache.ToLower
                Case "de"
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                              Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen _
                                                               Select New With _
                                                 { _
                                                         .Status = Eichprozess.Lookup_Vorgangsstatus.Status, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status, _
                                                         Eichprozess.ID, _
                                                         Eichprozess.Vorgangsnummer, _
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                         .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                         .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, _
                                                         .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, _
                                                         .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                         Eichprozess.Ausgeblendet _
                                                 }




                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "en"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                       Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen _
                               Select New With _
                                                { _
                                                              .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN, _
                                                        Eichprozess.ID, _
                                                        Eichprozess.Vorgangsnummer, _
                                                          .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_EN, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case "pl"
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                  Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen _
                               Select New With _
                                                { _
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_PL, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_PL, _
                                                        Eichprozess.ID, _
                                                        Eichprozess.Vorgangsnummer, _
                                                           .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ_PL, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art_EN, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
                Case Else
                    'laden der benötigten Liste mit nur den benötigten Spalten
                    'TH Diese Linq abfrage führt einen Join auf die Status Tabelle aus um den Status als Anzeigewert anzeigen zu können. 
                    'Außerdem werden durch die .Name = Wert Notatation im Kontext des "select NEW" eine neue temporäre "Klasse" erzeugt, die die übergebenen Werte beinhaltet - als kämen sie aus einer Datenbanktabelle
                    Dim Data = From Eichprozess In Context.Eichprozess _
                                                            Where Eichprozess.Ausgeblendet = bolAusgeblendeteElementeAnzeigen _
                               Select New With _
                                                { _
                                                          .Status = Eichprozess.Lookup_Vorgangsstatus.Status_EN, _
                                                         .Bearbeitungsstatus = Eichprozess.Lookup_Bearbeitungsstatus.Status_EN, _
                                                        Eichprozess.ID, Eichprozess.Vorgangsnummer, _
                                                         .Fabriknummer = Eichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer, _
                                                        .Lookup_Waegezelle = Eichprozess.Lookup_Waegezelle.Typ, _
                                                        .Lookup_Waagentyp = Eichprozess.Lookup_Waagentyp.Typ, .Lookup_Waagentyp_EN = Eichprozess.Lookup_Waagentyp.Typ_EN, .Lookup_Waagentyp_PL = Eichprozess.Lookup_Waagentyp.Typ_PL, _
                                                        .Lookup_Waagenart = Eichprozess.Lookup_Waagenart.Art, .Lookup_Waagenart_EN = Eichprozess.Lookup_Waagenart.Art_EN, .Lookup_Waagenart_PL = Eichprozess.Lookup_Waagenart.Art_PL, _
                                                        .Lookup_Auswertegeraet = Eichprozess.Lookup_Auswertegeraet.Typ, _
                                                        Eichprozess.Ausgeblendet _
                                                }

                    'zuweisen der Ergebnismenge als Datenquelle für das Grid
                    Return Data.ToList
            End Select


        End Using
    End Function

End Class
