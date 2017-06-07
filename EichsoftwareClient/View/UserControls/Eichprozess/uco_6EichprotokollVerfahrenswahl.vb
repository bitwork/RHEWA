Public Class uco_6EichprotokollVerfahrenswahl

    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Verfahrenswahl zurückgesetzt
    Private _objEichprotokoll As Eichprotokoll

#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        _suspendEvents = True
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        _suspendEvents = True
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Validations the needed.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Overrides Function ValidationNeeded() As Boolean
        Return ValidateControls()
    End Function

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaving = False
        If RadRadioButtonFahrzeugwaagen.IsChecked Or RadRadioButtonNormalien.IsChecked Or RadRadioButtonStaffelverfahren.IsChecked Then
            Return True
        Else
            AbortSaving = True
            'fehlermeldung anzeigen bei falscher validierung
            Dim result = Me.ShowValidationErrorBox(True)

            If result = DialogResult.Yes Or result = DialogResult.Ignore Then
                Return True
            ElseIf result = DialogResult.Retry Then
                Me.RadRadioButtonNormalien.IsChecked = True
                Return True
            Else
                Return False
            End If
            Return False
        End If
    End Function
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _suspendEvents = True
        If Not ParentFormular Is Nothing Then

            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Eichprotokollverfahrensauswahl)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Eichprotokollverfahrensauswahl
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren

        'daten füllen
        LoadFromDatabase()
        _suspendEvents = False
    End Sub

    Protected Friend Overrides Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New Entities
                'neu laden des Objekts, diesmal mit den lookup Objekten
                'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End Using
        End If
        _objEichprotokoll = objEichprozess.Eichprotokoll

        'steuerelemente mit werten aus DB füllen
        FillControls()
        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(Me)
        End If
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Beschaffenheitspruefungsobjekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        'je nach Art der Waage MAX1, Max2 oder MAX3 auslesen. Wenn dieser Wert unter 1000KG liegt, wird automatisch ü.60 KG mit normalien gewählt
        EnableDisableRadioButtons()

        'wenn keiner der Fälle zugetroffen hat, ist die auswahl nach dem Verfahren frei

        If Not objEichprozess.Eichprotokoll Is Nothing Then
            If _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen Then
                RadRadioButtonFahrzeugwaagen.IsChecked = True
            ElseIf _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien Then
                RadRadioButtonNormalien.IsChecked = True
            ElseIf _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren Then
                RadRadioButtonStaffelverfahren.IsChecked = True
                'Else
                '    RadRadioButtonNormalien.IsChecked = True
                '    _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
            End If
        End If

        '  RadRadioButtonNormalien.Focus()

    End Sub
    ''' <summary>
    ''' je nach Art der Waage MAX1, Max2 oder MAX3 auslesen. Wenn dieser Wert unter 1000KG liegt, wird automatisch ü.60 KG mit normalien gewählt
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnableDisableRadioButtons()
        Dim bolLock As Boolean = False
        Dim Waagenart As String = objEichprozess.Lookup_Waagenart.Art

        Select Case Waagenart
            Case Is = "Einbereichswaage"
                If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 < 1000 Then
                    bolLock = True
                End If
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 < 1000 Then
                    bolLock = True
                End If
            Case Is = "Dreibereichswaage", "Dreiteilungswaage"
                If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 < 1000 Then
                    bolLock = True
                End If
        End Select

        'If bolLock Then
        '    '  objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
        '    RadRadioButtonNormalien.IsChecked = True
        'Else
        '    '    objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
        '    RadRadioButtonStaffelverfahren.IsChecked = True
        'End If

        RadRadioButtonFahrzeugwaagen.Enabled = Not bolLock
        RadRadioButtonStaffelverfahren.Enabled = Not bolLock
    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now
        'je nachdem ob das Objekt schon existiert (man ist im Vorgang bereits weiter)  oder nicht, das eine oder andere Objekt ansprechen. Der Hintergrund kommt leider vom Entity Framework
        'ich habe erst eine ID im Eichprotokoll, wenn ich dieses Speichere. Somit kann ich dem Eichprozess FK aufs Eichprotokoll nur zuweisen, wenn das Eichprotokoll bereits gespeichert ist
        If Not objEichprozess.Eichprotokoll Is Nothing Then

            If RadRadioButtonFahrzeugwaagen.IsChecked Then
                objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen
            ElseIf RadRadioButtonNormalien.IsChecked Then
                objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
            ElseIf RadRadioButtonStaffelverfahren.IsChecked Then
                objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
            End If

        ElseIf Not _objEichprotokoll Is Nothing Then 'passiert im Falle eines neuen Eichprotokolls. Dort gibt es noch keine Zuweisung zu Eichprozess an dieser Setlle
            If RadRadioButtonFahrzeugwaagen.IsChecked Then
                _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen
            ElseIf RadRadioButtonNormalien.IsChecked Then
                _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
            ElseIf RadRadioButtonStaffelverfahren.IsChecked Then
                _objEichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
            End If

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
            Me.LokalisierungNeeded(UserControl)

            LoadFromDatabase()
        End If
    End Sub

    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
            'neuen Context aufbauen
            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If ValidateControls() = True Then

                Using Context As New Entities

                    If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren

                        If _objEichprotokoll Is Nothing Then           'neues Eichprotokoll anlegen und verfahrens Art zuweisen
                            _objEichprotokoll = Context.Eichprotokoll.Create
                            Context.Eichprotokoll.Add(_objEichprotokoll)

                        Else
                            Dim dobjEichprotkoll As Eichprotokoll = Context.Eichprotokoll.FirstOrDefault(Function(value) value.ID = _objEichprotokoll.ID)
                            _objEichprotokoll = dobjEichprotkoll
                        End If

                        'Füllt das Objekt mit den Werten aus den Steuerlementen
                        UpdateObject()
                        'Speichern in Datenbank
                        Context.SaveChanges()

                        'zuweisen des eichprotokolls an den eichprozess

                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                        If Not dobjEichprozess Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dobjEichprozess
                            objEichprozess.FK_Eichprotokoll = _objEichprotokoll.ID 'zuweisen des eichprotokolls an den eichprozess
                            objEichprozess.Eichprotokoll = _objEichprotokoll
                            'neuen Status zuweisen
                            If AktuellerStatusDirty = False Then
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten
                            End If
                            ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben

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
            Using Context As New Entities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = (From a In Context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
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
#End Region

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Name.Equals(UserControl.Name) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_6EichprotokollVerfahrenswahl))
        Lokalisierung(Me, resources)

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Eichprotokollverfahrensauswahl)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Eichprotokollverfahrensauswahl
            Catch ex As Exception
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Status des Verfahrens speichern
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub RadRadioButton_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadRadioButtonNormalien.ToggleStateChanged, RadRadioButtonFahrzeugwaagen.ToggleStateChanged, RadRadioButtonStaffelverfahren.ToggleStateChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(Me)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Overrides Sub VersendenNeeded(TargetUserControl As UserControl)

        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)

            UpdateObject()
            'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
            CloneAndSendServerObjekt()
        End If
    End Sub
End Class