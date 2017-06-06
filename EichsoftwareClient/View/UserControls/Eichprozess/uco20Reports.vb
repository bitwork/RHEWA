Public Class uco20Reports

    Inherits ucoContent

#Region "Member Variabeln"
    Private objOfficeExports As New clsOfficeExports
#End Region

#Region "Konstruktoren"
    'Private AktuellerStatusDirty As Boolean = False 'TH: Hier eigentlich obsolete. variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Export
    End Sub
#End Region

#Region "Lade Routine"

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Exports)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Exports
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Export

        'daten füllen
        LoadFromDatabase()
    End Sub

    Protected Friend Overrides Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        ParentFormular.RadButtonNavigateForwards.Enabled = True
        Try
            'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
            If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
                Using context As New Entities
                    context.Configuration.LazyLoadingEnabled = True
                    'neu laden des Objekts, diesmal mit den lookup Objekten
                    objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                End Using
            End If

        Catch e As Exception
        End Try

    End Sub

#End Region

#Region "Overrides"
    ''' <summary>
    ''' Validations the needed.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Overrides Function ValidationNeeded() As Boolean
        Return True
    End Function
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Versenden Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Versenden Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            'neuen Context aufbauen
            Using Context As New Entities
                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess
                        'neuen Status zuweisen

                        If AktuellerStatusDirty = False Then
                            ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Versenden Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                            End If
                        ElseIf AktuellerStatusDirty = True Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden
                            AktuellerStatusDirty = False
                        End If

                        'Speichern in Datenbank
                        Try
                            Context.SaveChanges()
                        Catch ex As Entity.Validation.DbEntityValidationException
                            For Each e In ex.EntityValidationErrors
                                MessageBox.Show(e.ValidationErrors(0).ErrorMessage)
                            Next
                        End Try
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
            Me.LokalisierungNeeded(UserControl)


            LoadFromDatabase()
        End If
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
            'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
            CloneAndSendServerObjekt()
        End If
    End Sub
#End Region

#Region "Exports"

    ''' <summary>
    ''' Kompatiblitätsnachweis DEUTSCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportKompatibliaetsnachweisDE_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportKompatibliaetsnachweisDE.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportKompatiblitaetsnachweisDE(objEichprozess)
    End Sub

    ''' <summary>
    ''' Kompatiblitätsnachweis ENGLISCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportKompatibliaetsnachweisEN_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportKompatibliaetsnachweisEN.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportKompatiblitaetsnachweisEN(objEichprozess)
    End Sub

    ''' <summary>
    ''' Konformitätserklärung DEUTSCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportKonformitaetserklaerungDE_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportKonformitaetserklaerungDE.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportKonformitaetssnachweisDE(objEichprozess)
    End Sub

    ''' <summary>
    ''' Konformitätserklärung POLNISCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportKonformitaetserklaerungPL_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportKonformitaetserklaerungPL.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportKonformitaetssnachweisPL(objEichprozess)
    End Sub

    ''' <summary>
    ''' Konformitätserklärung RUMÄNISCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportKonformitaetserklaerungRO_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportKonformitaetserklaerungRO.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportKonformitaetssnachweisRU(objEichprozess)
    End Sub

    ''' <summary>
    ''' Ersteichung DEUTSCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportErstEichungDE_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportErstEichungDE.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportErsteichungDE(objEichprozess)

    End Sub

    ''' <summary>
    ''' Ersteichung ENGLISCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportErstEichungEN_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportErstEichungEN.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportErsteichungEN(objEichprozess)
    End Sub

    ''' <summary>
    ''' Ersteichung POLNISCH
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportErstEichungPL_Click(sender As System.Object, e As System.EventArgs) Handles RadButtonExportErstEichungPL.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportErsteichungPL(objEichprozess)
    End Sub

    ''' <summary>
    ''' Export des Eichprozesses (rudimentär)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadButtonExportEichprozessDE_Click(sender As Object, e As EventArgs) Handles RadButtonExportEichprozessDE.Click
        AktuellerStatusDirty = True
        objOfficeExports.ExportEichprozess(objEichprozess)
    End Sub

#End Region

End Class