Public Class ucoTaraeinrichtung
    Inherits ucoContent
#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _objEichprotokoll As Eichprotokoll
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung
    End Sub


#End Region

#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungTaraEinrichtung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungTaraEinrichtung
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung

        'daten füllen
        LoadFromDatabase()
    End Sub


    ''' <summary>
    ''' Bei Änderungen DirtyFlag Setzen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadCheckBoxTaraErweiterteRichtigkeitspruefung_Click(sender As System.Object, e As System.EventArgs) Handles RadCheckBoxTaraGenauigkeitTarrierung.Click, RadCheckBoxTaraErweiterteRichtigkeitspruefung.Click, RadCheckBoxTaraausgleicheinrichtungOK.Click
        AktuellerStatusDirty = True
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
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                _objEichprotokoll = objEichprozess.Eichprotokoll
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.RadScrollablePanel1.PanelContainer.Controls
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

        If Not objEichprozess.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK Is Nothing Then
            RadCheckBoxTaraErweiterteRichtigkeitspruefung.Checked = objEichprozess.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK
        End If

        If Not objEichprozess.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK Is Nothing Then
            RadCheckBoxTaraausgleicheinrichtungOK.Checked = objEichprozess.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK
        End If
        If Not objEichprozess.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK Is Nothing Then
            RadCheckBoxTaraGenauigkeitTarrierung.Checked = objEichprozess.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK
        End If
    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        objEichprozess.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK = RadCheckBoxTaraErweiterteRichtigkeitspruefung.Checked
        objEichprozess.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK = RadCheckBoxTaraausgleicheinrichtungOK.Checked
        objEichprozess.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK = RadCheckBoxTaraGenauigkeitTarrierung.Checked
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

        If RadCheckBoxTaraausgleicheinrichtungOK.Checked = False Then
            AbortSaveing = True
            RadCheckBoxTaraausgleicheinrichtungOK.Focus()
        End If

        If RadCheckBoxTaraErweiterteRichtigkeitspruefung.Checked = False Then
            AbortSaveing = True
            RadCheckBoxTaraErweiterteRichtigkeitspruefung.Focus()

        End If
        'TH Kein Pflichtfeld laut Herrn Strack
        'If RadCheckBoxTaraGenauigkeitTarrierung.Checked = False Then
        '    AbortSaveing = True
        '    RadCheckBoxTaraGenauigkeitTarrierung.Focus()

        'End If

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
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

         
            If DialogModus = enuDialogModus.lesend Or DialogModus = enuDialogModus.korrigierend Then
                If DialogModus = enuDialogModus.korrigierend Then
                    UpdateObject()
                End If

                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        End If
                    Case Is = "Fahrzeugwaagen"
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                        End If
                End Select
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



                            Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                                Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                                    If AktuellerStatusDirty = False Then
                                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung Then
                                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                                        End If
                                    ElseIf AktuellerStatusDirty = True Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                                        AktuellerStatusDirty = False
                                    End If
                                Case Is = "Fahrzeugwaagen"
                                    If AktuellerStatusDirty = False Then
                                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen Then
                                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                                        End If
                                    ElseIf AktuellerStatusDirty = True Then
                                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
                                        AktuellerStatusDirty = False
                                    End If
                            End Select




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

    Protected Friend Overrides Sub SaveWithoutValidationNeeded(usercontrol As UserControl)
        MyBase.SaveWithoutValidationNeeded(usercontrol)

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

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()
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
    Protected Friend Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungTaraEinrichtung)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungTaraEinrichtung
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub



    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoTaraeinrichtung))

        Me.lblGenauigkeit.Text = resources.GetString("lblGenauigkeit.Text")
        Me.lblRichtigkeitspruefung.Text = resources.GetString("lblRichtigkeitspruefung.Text")
        Me.lblTaraAusrichtung.Text = resources.GetString("lblTaraAusrichtung.Text")


        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungTaraEinrichtung)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungTaraEinrichtung
            Catch ex As Exception
            End Try
        End If

    End Sub

    'Entsperrroutine
    Protected Friend Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.RadScrollablePanel1.PanelContainer.Controls
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

    Protected Friend Overrides Sub VersendenNeeded(TargetUserControl As UserControl)

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

#End Region


End Class
