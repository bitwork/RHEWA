Public Class uco16Taraeinrichtung

    Inherits ucoContent
    Implements IRhewaEditingDialog
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
        SetzeUeberschrift()
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


#End Region

#Region "Interface Methods"
    Protected Friend Overrides Sub LoadFromDatabase() Implements IRhewaEditingDialog.LoadFromDatabase
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New Entities
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren").Include("Lookup_Bearbeitungsstatus").Include("Lookup_Vorgangsstatus").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                _objEichprotokoll = objEichprozess.Eichprotokoll
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(RadScrollablePanel1.PanelContainer)

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
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
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
    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

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
    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaving = False

        If RadCheckBoxTaraausgleicheinrichtungOK.Checked = False Then
            AbortSaving = True
            RadCheckBoxTaraausgleicheinrichtungOK.Focus()
        End If

        If RadCheckBoxTaraErweiterteRichtigkeitspruefung.Checked = False Then
            AbortSaving = True
            RadCheckBoxTaraErweiterteRichtigkeitspruefung.Focus()

        End If


        'fehlermeldung anzeigen bei falscher validierung
        Dim result = Me.ShowValidationErrorBox(False)
        Return ProcessResult(result)
    End Function

    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadCheckBoxTaraausgleicheinrichtungOK.Checked = True
        RadCheckBoxTaraErweiterteRichtigkeitspruefung.Checked = True
    End Sub
    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
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


    Protected Friend Overrides Sub SaveObjekt() Implements IRhewaEditingDialog.SaveObjekt
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
                    UpdateObjekt()
                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using
    End Sub
    Protected Friend Overrides Sub AktualisiereStatus() Implements IRhewaEditingDialog.AktualisiereStatus
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

                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using
    End Sub
    Protected Friend Overrides Function CheckDialogmodus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Or DialogModus = enuDialogModus.korrigierend Then
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObjekt()
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
            Return False
        End If
        Return True
    End Function



    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco16Taraeinrichtung))
        Lokalisierung(Me, resources)
    End Sub



    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadScrollablePanel1.PanelContainer)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub


    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
        CloneAndSendServerObjekt()
    End Sub

#End Region

End Class