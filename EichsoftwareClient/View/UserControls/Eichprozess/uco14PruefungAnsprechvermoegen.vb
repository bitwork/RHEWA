Public Class uco14PruefungAnsprechvermoegen
    Inherits ucoContent
#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _objEichprotokoll As Eichprotokoll

    Private _currentObjPruefungAnsprechvermoegen As PruefungAnsprechvermoegen
    Private _ListPruefungAnsprechvermoegen As New List(Of PruefungAnsprechvermoegen)
#End Region


#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens
    End Sub
#End Region




#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAnsprechvermoegen)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungAnsprechvermoegen
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens

        'daten füllen
        LoadFromDatabase()
    End Sub

#End Region

#Region "Methods"
    Private Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Kompatiblitaetsnachweis").Include("Lookup_Waagenart") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                _objEichprotokoll = objEichprozess.Eichprotokoll

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungAnsprechvermoegen Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungAnsprechvermoegen = query.ToList

            End Using
        Else

            _objEichprotokoll = objEichprozess.Eichprotokoll
            _ListPruefungAnsprechvermoegen.Clear()
            Try
                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                For Each obj In objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen
                    _ListPruefungAnsprechvermoegen.Add(obj)
                Next
            Catch ex As System.ObjectDisposedException
                Using context As New EichsoftwareClientdatabaseEntities1
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query = From a In context.PruefungAnsprechvermoegen Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungAnsprechvermoegen = query.ToList

                End Using
            End Try

        End If

        'steuerelemente mit werten aus DB füllen
        FillControls()


        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.Controls
                Try
                    Control.readonly = True
                Catch ex As Exception
                    Try
                        Control.isreadonly = True
                    Catch ex2 As Exception
                        Try
                            Control.enabled = False
                        Catch ex3 As Exception
                        End Try
                    End Try
                End Try
            Next
        End If
        'events abbrechen
        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        'Steuerlemente füllen
        'min
        _currentObjPruefungAnsprechvermoegen = Nothing
        _currentObjPruefungAnsprechvermoegen = (From o In _ListPruefungAnsprechvermoegen Where o.LastL = "Min").FirstOrDefault

        If Not _currentObjPruefungAnsprechvermoegen Is Nothing Then
            RadTextBoxControlLast1.Text = _currentObjPruefungAnsprechvermoegen.Last
            RadTextBoxControlAnzeige1.Text = _currentObjPruefungAnsprechvermoegen.Anzeige
            RadTextBoxControLastD1.Text = _currentObjPruefungAnsprechvermoegen.Last1d
            RadCheckBoxMin.Checked = _currentObjPruefungAnsprechvermoegen.Ziffernsprung
        Else
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    RadTextBoxControlLast1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    RadTextBoxControlLast1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    RadTextBoxControlLast1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
            End Select
            'anzeige + eichwert
        End If

        'halb
        _currentObjPruefungAnsprechvermoegen = Nothing
        _currentObjPruefungAnsprechvermoegen = (From o In _ListPruefungAnsprechvermoegen Where o.LastL = "Halb").FirstOrDefault


        If Not _currentObjPruefungAnsprechvermoegen Is Nothing Then
            RadTextBoxControlLast2.Text = _currentObjPruefungAnsprechvermoegen.Last
            RadTextBoxControlAnzeige2.Text = _currentObjPruefungAnsprechvermoegen.Anzeige
            RadTextBoxControlLastD2.Text = _currentObjPruefungAnsprechvermoegen.Last1d
            RadCheckBoxHalb.Checked = _currentObjPruefungAnsprechvermoegen.Ziffernsprung
        Else
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    RadTextBoxControlLast2.Text = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1) / 2
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    RadTextBoxControlLast2.Text = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2) / 2
                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    RadTextBoxControlLast2.Text = CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3) / 2
            End Select
        End If


        'max
        _currentObjPruefungAnsprechvermoegen = Nothing
        _currentObjPruefungAnsprechvermoegen = (From o In _ListPruefungAnsprechvermoegen Where o.LastL = "Max").FirstOrDefault

        If Not _currentObjPruefungAnsprechvermoegen Is Nothing Then
            RadTextBoxControlLast3.Text = _currentObjPruefungAnsprechvermoegen.Last
            RadTextBoxControlAnzeige3.Text = _currentObjPruefungAnsprechvermoegen.Anzeige
            RadTextBoxControlLastD3.Text = _currentObjPruefungAnsprechvermoegen.Last1d
            RadCheckBoxMax.Checked = _currentObjPruefungAnsprechvermoegen.Ziffernsprung
        Else
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    RadTextBoxControlLast3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    RadTextBoxControlLast3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    RadTextBoxControlLast3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
            End Select
        End If

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        'neuen Context aufbauen
        Using Context As New EichsoftwareClientdatabaseEntities1
            'jedes objekt initialisieren und aus context laden und updaten
            For Each objPruefung In _ListPruefungAnsprechvermoegen
                objPruefung = Context.PruefungAnsprechvermoegen.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsObject(objPruefung)
            Next
       
        End Using
    End Sub

    Private Sub UpdatePruefungsObject(ByVal pObjPruefung As PruefungAnsprechvermoegen)
        Select Case pObjPruefung.LastL
            Case Is = "Min"
                pObjPruefung.Last = RadTextBoxControlLast1.Text
                pObjPruefung.Anzeige = RadTextBoxControlAnzeige1.Text
                pObjPruefung.Last1d = RadTextBoxControLastD1.Text
                pObjPruefung.Ziffernsprung = RadCheckBoxMin.Checked
            Case Is = "Halb"
                pObjPruefung.Last = RadTextBoxControlLast2.Text
                pObjPruefung.Anzeige = RadTextBoxControlAnzeige2.Text
                pObjPruefung.Last1d = RadTextBoxControlLastD2.Text
                pObjPruefung.Ziffernsprung = RadCheckBoxHalb.Checked
            Case Is = "Max"
                pObjPruefung.Last = RadTextBoxControlLast3.Text
                pObjPruefung.Anzeige = RadTextBoxControlAnzeige3.Text
                pObjPruefung.Last1d = RadTextBoxControlLastD3.Text
                pObjPruefung.Ziffernsprung = RadCheckBoxMax.Checked
        End Select
    End Sub
    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaveing = False

        If RadCheckBoxHalb.Checked = False Then
            AbortSaveing = True
        End If
        If RadCheckBoxMax.Checked = False Then
            AbortSaveing = True
        End If

        If RadCheckBoxMin.Checked = False Then
            AbortSaveing = True
        End If

        If RadTextBoxControlAnzeige1.Text.Trim = "" Or _
            RadTextBoxControlAnzeige2.Text.Trim = "" Or _
            RadTextBoxControlAnzeige3.Text.Trim = "" Or _
RadTextBoxControlLast1.Text.Trim = "" Or _
RadTextBoxControlLast2.Text.Trim = "" Or _
            RadTextBoxControlLast3.Text.Trim = "" Then

            AbortSaveing = True
        End If

        If AbortSaveing Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True

    End Function

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
            If DialogModus = enuDialogModus.lesend Then
                'Wenn kein Drucker gewählt wurde entfällt die PRüfung der Stablität der GLeichgewichtslage
                If objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    End If
                Else
                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    End If
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                'Wenn kein Drucker gewählt wurde entfällt die PRüfung der Stablität der GLeichgewichtslage
                If objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                    End If

                Else
                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                    End If
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If ValidateControls() = True Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                        If Not dobjEichprozess Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dobjEichprozess
                            'neuen Status zuweisen

                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungAnsprechvermoegen.Count = 0 Then

                                Dim objPruefung = Context.PruefungAnsprechvermoegen.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.LastL = "Min"
                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                                Context.SaveChanges()

                                _ListPruefungAnsprechvermoegen.Add(objPruefung)

                                'halb
                                objPruefung = Nothing
                                objPruefung = Context.PruefungAnsprechvermoegen.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.LastL = "Halb"
                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                                Context.SaveChanges()

                                _ListPruefungAnsprechvermoegen.Add(objPruefung)

                                'max
                                objPruefung = Nothing
                                objPruefung = Context.PruefungAnsprechvermoegen.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.LastL = "Max"
                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                                Context.SaveChanges()

                                _ListPruefungAnsprechvermoegen.Add(objPruefung)

                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungAnsprechvermoegen
                                    objPruefung = Context.PruefungAnsprechvermoegen.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next
                            End If


                            'Wenn kein Drucker gewählt wurde entfällt die PRüfung der Stablität der GLeichgewichtslage
                            If objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                                If AktuellerStatusDirty = False Then


                                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                                    End If
                                ElseIf AktuellerStatusDirty = True Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
                                    AktuellerStatusDirty = False
                                End If
                            Else
                                If AktuellerStatusDirty = False Then


                                    ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                    If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                                    End If
                                ElseIf AktuellerStatusDirty = True Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage
                                    AktuellerStatusDirty = False
                                End If
                            End If




                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()
                            'Speichern in Datenbank
                            Context.SaveChanges()
                        End If
                    End If
                End Using

                ParentFormular.CurrentEichprozess = objEichprozess
            End If

        End If
    End Sub

    Protected Overrides Sub SaveWithoutValidationNeeded(usercontrol As UserControl)
        If Me.Equals(usercontrol) Then

            If DialogModus = enuDialogModus.lesend Then
                UpdateObject()
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.Include("Eichprotokoll").FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen

                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungAnsprechvermoegen.Count = 0 Then

                            Dim objPruefung = Context.PruefungAnsprechvermoegen.Create
                            'wenn es die eine itereation mehr ist:
                            objPruefung.LastL = "Min"
                            UpdatePruefungsObject(objPruefung)

                            Context.SaveChanges()

                            objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                            Context.SaveChanges()

                            _ListPruefungAnsprechvermoegen.Add(objPruefung)

                            'halb
                            objPruefung = Nothing
                            objPruefung = Context.PruefungAnsprechvermoegen.Create
                            'wenn es die eine itereation mehr ist:
                            objPruefung.LastL = "Halb"
                            UpdatePruefungsObject(objPruefung)

                            Context.SaveChanges()

                            objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                            Context.SaveChanges()

                            _ListPruefungAnsprechvermoegen.Add(objPruefung)

                            'max
                            objPruefung = Nothing
                            objPruefung = Context.PruefungAnsprechvermoegen.Create
                            'wenn es die eine itereation mehr ist:
                            objPruefung.LastL = "Max"
                            UpdatePruefungsObject(objPruefung)

                            Context.SaveChanges()

                            objEichprozess.Eichprotokoll.PruefungAnsprechvermoegen.Add(objPruefung)
                            Context.SaveChanges()

                            _ListPruefungAnsprechvermoegen.Add(objPruefung)

                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungAnsprechvermoegen
                                objPruefung = Context.PruefungAnsprechvermoegen.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next
                        End If
                    End If
                End If
            End Using

            ParentFormular.CurrentEichprozess = objEichprozess
        End If

    End Sub

    ''' <summary>
    ''' aktualisieren der Oberfläche wenn nötig
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAnsprechvermoegen)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungAnsprechvermoegen
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region




    Private Sub RadButtonShowEFGSteigend_Click(sender As Object, e As EventArgs) Handles RadButtonShowEFG.Click
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()

    End Sub



    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco14PruefungAnsprechvermoegen))

        Me.lblAnzeige.Text = resources.GetString("lblAnzeige.Text")
        Me.lblLast1d.Text = resources.GetString("lblLast1d.Text")
        Me.lblLastkg.Text = resources.GetString("lblLastkg.Text")
        Me.lblLastL.Text = resources.GetString("lblLastL.Text")
        Me.lblBeschreibung.Text = resources.GetString("lblBeschreibung.Text")
        Me.lblLastBeschreibung.Text = resources.GetString("lblLastBeschreibung.Text")

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungAnsprechvermoegen)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungAnsprechvermoegen
            Catch ex As Exception
            End Try
        End If


    End Sub



    'berechnen des D1 Wertes aus MIN Eichwert + Anzeige2
    Private Sub RadTextBoxControlLast1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeige1.TextChanged, RadTextBoxControlLast1.TextChanged
        'min
        If _suspendEvents = False Then
            AktuellerStatusDirty = True

        End If
        Try
            RadTextBoxControLastD1.Text = CDec(RadTextBoxControlAnzeige1.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        Catch ex As Exception
            RadTextBoxControLastD1.Text = 0
        End Try
    End Sub
    'berechnen des D2 Wertes aus MAX Eichwert + Anzeige2
    Private Sub RadTextBoxControlLast2_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeige2.TextChanged, RadTextBoxControlLast1.TextChanged
        Try
            If _suspendEvents = False Then
                AktuellerStatusDirty = True

            End If
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    RadTextBoxControlLastD2.Text = CDec(RadTextBoxControlAnzeige2.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    RadTextBoxControlLastD2.Text = CDec(RadTextBoxControlAnzeige2.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    RadTextBoxControlLastD2.Text = CDec(RadTextBoxControlAnzeige2.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

            End Select
        Catch ex As Exception
            RadTextBoxControlLastD2.Text = 0
        End Try
    End Sub

    'berechnen des D3 Wertes aus MAX Eichwert + Anzeige3
    Private Sub RadTextBoxControlLast3_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlAnzeige3.TextChanged, RadTextBoxControlLast1.TextChanged
        If _suspendEvents = False Then
            AktuellerStatusDirty = True

        End If

        Try
            Select Case objEichprozess.Lookup_Waagenart.Art
                Case Is = "Einbereichswaage"
                    RadTextBoxControlLastD3.Text = CDec(RadTextBoxControlAnzeige3.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                    RadTextBoxControlLastD3.Text = CDec(RadTextBoxControlAnzeige3.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
                Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                    RadTextBoxControlLastD3.Text = CDec(RadTextBoxControlAnzeige3.Text) + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            End Select
        Catch ex As Exception
            RadTextBoxControlLastD3.Text = 0
        End Try
    End Sub

    Private Sub RadTextBoxControlAnzeige1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlAnzeige3.Validating, RadTextBoxControlAnzeige2.Validating, RadTextBoxControlAnzeige1.Validating, _
        RadTextBoxControlLast3.Validating, RadTextBoxControlLast2.Validating, RadTextBoxControlLast1.Validating
        Dim result As Decimal
        If Not sender.isreadonly = True Then

            'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
            If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text.Equals("") Then
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
                Exit Sub
            End If

            'versuchen ob der Text in eine Zahl konvertiert werden kann
            If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text, result) Then
                e.Cancel = True
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
                System.Media.SystemSounds.Exclamation.Play()

            Else 'rahmen zurücksetzen
                CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
            End If
        End If
    End Sub

    Private Sub RadCheckBoxMin_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxMin.ToggleStateChanged, RadCheckBoxMax.ToggleStateChanged, RadCheckBoxHalb.ToggleStateChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.Controls
            Try
                Control.readonly = Not Control.readonly
            Catch ex As Exception
                Try
                    Control.isreadonly = Not Control.isReadonly
                Catch ex2 As Exception
                    Try
                        Control.enabled = Not Control.enabled
                    Catch ex3 As Exception
                    End Try
                End Try
            End Try
        Next

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Overrides Sub VersendenNeeded(TargetUserControl As UserControl)


        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)
            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Eichbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()

                'erzeuegn eines Server Objektes auf basis des aktuellen DS
                objServerEichprozess = clsClientServerConversionFunctions.CopyObjectProperties(objServerEichprozess, objEichprozess)
                Using Webcontext As New EichsoftwareWebservice.EichsoftwareWebserviceClient
                    Try
                        Webcontext.Open()
                    Catch ex As Exception
                        MessageBox.Show(My.Resources.GlobaleLokalisierung.KeineVerbindung, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    Dim objLiz = (From db In dbcontext.Lizensierung Select db).FirstOrDefault

                    Try
                        'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                        Webcontext.AddEichprozess(objLiz.HEKennung, objLiz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

                        'schließen des dialoges
                        ParentFormular.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ' Status zurück setzen
                        Exit Sub
                    End Try
                End Using
            End Using
        End If
    End Sub

    Private Sub RadTextBoxControlLast2_TextChanged_1(sender As System.Object, e As System.EventArgs) Handles RadTextBoxControlLast3.TextChanged, RadTextBoxControlLast2.TextChanged
        If _suspendEvents = False Then
            AktuellerStatusDirty = True
        End If
    End Sub
End Class
