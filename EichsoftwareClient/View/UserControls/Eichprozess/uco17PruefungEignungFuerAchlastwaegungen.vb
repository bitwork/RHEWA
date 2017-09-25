Public Class uco17PruefungEignungFuerAchlastwaegungen

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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen
    End Sub

#End Region

#Region "Events"


    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen

        'daten füllen
        LoadFromDatabase()
    End Sub


    Private Sub RadCheckBoxZufahrtenInOrdnung_Click(sender As System.Object, e As System.EventArgs) Handles RadCheckBoxZufahrtenInOrdnung.Click, RadCheckBoxWaageNichtGeeignet.Click, RadCheckBoxWaagegeprueft.Click
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub



#End Region

#Region "Methods"


#End Region

#Region "Interface Methods"
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

                    If AktuellerStatusDirty = False Then
                        ' wenn der aktuelle status kleiner ist als der für die beschaffenheitspruefung, wird dieser überschrieben. sonst würde ein aktuellere status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        End If
                    ElseIf AktuellerStatusDirty = True Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
                        AktuellerStatusDirty = False
                    End If

                    'Speichern in Datenbank
                    Context.SaveChanges()
                End If
            End If
        End Using
    End Sub

    Protected Friend Overrides Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Then
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If

        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function


    Protected Friend Overrides Sub LoadFromDatabase()
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

        If Not objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene Is Nothing Then
            RadCheckBoxZufahrtenInOrdnung.Checked = objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene
        End If

        If Not objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet Is Nothing Then
            RadCheckBoxWaageNichtGeeignet.Checked = objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet
        End If
        If Not objEichprozess.Eichprotokoll.EignungAchlastwaegungen_Geprueft Is Nothing Then
            RadCheckBoxWaagegeprueft.Checked = objEichprozess.Eichprotokoll.EignungAchlastwaegungen_Geprueft
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

        objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene = RadCheckBoxZufahrtenInOrdnung.Checked
        objEichprozess.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet = RadCheckBoxWaageNichtGeeignet.Checked
        objEichprozess.Eichprotokoll.EignungAchlastwaegungen_Geprueft = RadCheckBoxWaagegeprueft.Checked
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

        'fehlermeldung anzeigen bei falscher validierung
        Dim result = Me.ShowValidationErrorBox(False)
        Return ProcessResult(result)

    End Function

    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadCheckBoxWaagegeprueft.Checked = True
        RadCheckBoxWaageNichtGeeignet.Checked = True
        RadCheckBoxZufahrtenInOrdnung.Checked = True
    End Sub
    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungEignungAchslastwaegungen)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEignungAchslastwaegungen
            Catch ex As Exception
            End Try
        End If
    End Sub





    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco17PruefungEignungFuerAchlastwaegungen))
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