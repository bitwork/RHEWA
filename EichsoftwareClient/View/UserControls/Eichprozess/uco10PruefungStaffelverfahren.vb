Public Class uco10PruefungStaffelverfahren
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _ListPruefungStaffelverfahrenNormallast As New List(Of PruefungStaffelverfahrenNormallast)
    Private _currentObjPruefungStaffelverfahrenNormallast As PruefungStaffelverfahrenNormallast

    Private _ListPruefungStaffelverfahrenErsatzlast As New List(Of PruefungStaffelverfahrenErsatzlast)
    Private _currentObjPruefungStaffelverfahrenErsatzlast As PruefungStaffelverfahrenErsatzlast
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast
    End Sub
#End Region


#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast

        'daten füllen
        LoadFromDatabase()

    End Sub

    Private Sub RadTextBoxControlStaffel1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlStaffel5Bereich3Last2.Validating, RadTextBoxControlStaffel5Bereich3Last1.Validating, RadTextBoxControlStaffel5Bereich3Anzeige1.Validating, RadTextBoxControlStaffel5Bereich2Last2.Validating, RadTextBoxControlStaffel5Bereich2Last1.Validating, RadTextBoxControlStaffel5Bereich2Anzeige1.Validating, RadTextBoxControlStaffel5Bereich1Last2.Validating, RadTextBoxControlStaffel5Bereich1Last1.Validating, RadTextBoxControlStaffel5Bereich1Anzeige1.Validating, RadTextBoxControlStaffel4Bereich3Last2.Validating, RadTextBoxControlStaffel4Bereich3Last1.Validating, RadTextBoxControlStaffel4Bereich3Anzeige1.Validating, RadTextBoxControlStaffel4Bereich2Last2.Validating, RadTextBoxControlStaffel4Bereich2Last1.Validating, RadTextBoxControlStaffel4Bereich2Anzeige1.Validating, RadTextBoxControlStaffel4Bereich1Last2.Validating, RadTextBoxControlStaffel4Bereich1Last1.Validating, RadTextBoxControlStaffel4Bereich1Anzeige1.Validating, RadTextBoxControlStaffel3Bereich3Last2.Validating, RadTextBoxControlStaffel3Bereich3Last1.Validating, RadTextBoxControlStaffel3Bereich3Anzeige1.Validating, RadTextBoxControlStaffel3Bereich2Last2.Validating, RadTextBoxControlStaffel3Bereich2Last1.Validating, RadTextBoxControlStaffel3Bereich2Anzeige1.Validating, RadTextBoxControlStaffel3Bereich1Last2.Validating, RadTextBoxControlStaffel3Bereich1Last1.Validating, RadTextBoxControlStaffel3Bereich1Anzeige1.Validating, RadTextBoxControlStaffel2Bereich3Last2.Validating, RadTextBoxControlStaffel2Bereich3Last1.Validating, RadTextBoxControlStaffel2Bereich3Anzeige1.Validating, RadTextBoxControlStaffel2Bereich2Last2.Validating, RadTextBoxControlStaffel2Bereich2Last1.Validating, RadTextBoxControlStaffel2Bereich2Anzeige1.Validating, RadTextBoxControlStaffel2Bereich1Last2.Validating, RadTextBoxControlStaffel2Bereich1Last1.Validating, RadTextBoxControlStaffel2Bereich1Anzeige1.Validating, RadTextBoxControlStaffel1Bereich3Last4.Validating, RadTextBoxControlStaffel1Bereich3Last3.Validating, RadTextBoxControlStaffel1Bereich3Last1.Validating, RadTextBoxControlStaffel1Bereich3Anzeige4.Validating, RadTextBoxControlStaffel1Bereich3Anzeige3.Validating, RadTextBoxControlStaffel1Bereich3Anzeige1.Validating, RadTextBoxControlStaffel1Bereich2Last4.Validating, RadTextBoxControlStaffel1Bereich2Last3.Validating, RadTextBoxControlStaffel1Bereich2Last1.Validating, RadTextBoxControlStaffel1Bereich2Anzeige4.Validating, RadTextBoxControlStaffel1Bereich2Anzeige3.Validating, RadTextBoxControlStaffel1Bereich2Anzeige1.Validating, RadTextBoxControlStaffel1Bereich1Last4.Validating, RadTextBoxControlStaffel1Bereich1Last3.Validating, RadTextBoxControlStaffel1Bereich1Last1.Validating, RadTextBoxControlStaffel1Bereich1Anzeige4.Validating, RadTextBoxControlStaffel1Bereich1Anzeige3.Validating, RadTextBoxControlStaffel1Bereich1Anzeige1.Validating, RadTextBoxControlStaffel1Bereich3Last2.Validating, RadTextBoxControlStaffel1Bereich3Fehler2.Validating, RadTextBoxControlStaffel1Bereich3Anzeige2.Validating, RadTextBoxControlStaffel1Bereich2Last2.Validating, RadTextBoxControlStaffel1Bereich2Fehler2.Validating, RadTextBoxControlStaffel1Bereich2Anzeige2.Validating, RadTextBoxControlStaffel1Bereich1Last2.Validating, RadTextBoxControlStaffel1Bereich1Fehler2.Validating, RadTextBoxControlStaffel1Bereich1Anzeige2.Validating


        Try
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
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Methods"
    Private Sub LoadFromDatabase()
        Me.SuspendLayout()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query = From a In context.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungStaffelverfahrenNormallast = query.ToList

                'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                Dim query2 = From a In context.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                _ListPruefungStaffelverfahrenErsatzlast = query2.ToList

            End Using

        Else
            _ListPruefungStaffelverfahrenNormallast.Clear()
            _ListPruefungStaffelverfahrenErsatzlast.Clear()
            Try
                For Each obj In objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast
                    _ListPruefungStaffelverfahrenNormallast.Add(obj)
                Next
                For Each obj In objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast
                    _ListPruefungStaffelverfahrenErsatzlast.Add(obj)
                Next
            Catch ex As System.ObjectDisposedException
                Using context As New EichsoftwareClientdatabaseEntities1
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query = From a In context.PruefungStaffelverfahrenNormallast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungStaffelverfahrenNormallast = query.ToList

                    'abrufen aller Prüfungs entitäten die sich auf dieses eichprotokoll beziehen
                    Dim query2 = From a In context.PruefungStaffelverfahrenErsatzlast Where a.FK_Eichprotokoll = objEichprozess.Eichprotokoll.ID
                    _ListPruefungStaffelverfahrenErsatzlast = query2.ToList
                End Using
            End Try

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
        Me.ResumeLayout()
    End Sub


    Private Sub SetzeNullstellen()
        Try
            'nullstellen maske für EFG erzwingen
            'staffel1
            lblStaffel1Bereich1EFGWert1.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert2.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert3.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert4.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich1EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel1Bereich2EFGWert1.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert2.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert3.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert4.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich2EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel1Bereich3EFGWert1.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert2.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert3.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert4.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel1Bereich3EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            'Staffel2
            lblStaffel2Bereich1EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich1EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich1EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)


            lblStaffel2Bereich2EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich2EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich2EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel2Bereich3EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich3EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel2Bereich3EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            'Staffel3
            lblStaffel3Bereich1EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich1EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich1EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)


            lblStaffel3Bereich2EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich2EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich2EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel3Bereich3EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich3EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel3Bereich3EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            'Staffel4
            lblStaffel4Bereich1EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich1EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich1EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)


            lblStaffel4Bereich2EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich2EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich2EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel4Bereich3EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich3EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel4Bereich3EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            'Staffel5
            lblStaffel5Bereich1EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich1EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich1EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)


            lblStaffel5Bereich2EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich2EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich2EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)

            lblStaffel5Bereich3EFGWert5.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich3EFGWert6.Mask = String.Format("F{0}", _intNullstellenE)
            lblStaffel5Bereich3EFGWert7.Mask = String.Format("F{0}", _intNullstellenE)
        Catch e As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Objekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()

        'Steuerlemente füllen
        'dynamisches laden der Nullstellen:
        Try
            _intNullstellenE1 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1) '.Replace(",", "."))  + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE2 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2) '.Replace(",", ".")) + 1
        Catch ex As Exception
        End Try
        Try
            _intNullstellenE3 = clsGeneralFunctions.GetRHEWADecimalDigits(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3) '.Replace(",", "."))  + 1
        Catch ex As Exception
        End Try

        If _intNullstellenE1 > _intNullstellenE2 AndAlso _intNullstellenE1 > _intNullstellenE3 Then
            _intNullstellenE = _intNullstellenE1
        ElseIf _intNullstellenE2 > _intNullstellenE1 AndAlso _intNullstellenE2 > _intNullstellenE3 Then
            _intNullstellenE = _intNullstellenE2
        ElseIf _intNullstellenE3 > _intNullstellenE1 AndAlso _intNullstellenE3 > _intNullstellenE2 Then
            _intNullstellenE = _intNullstellenE3
        Else 'alles ist gleih
            _intNullstellenE = _intNullstellenE1
        End If

        SetzeNullStellen()

        'je nach Art der Waage andere Bereichsgruppen ausblenden
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            RadGroupBoxStaffel1Bereich2.Visible = False
            RadGroupBoxStaffel1Bereich3.Visible = False
            RadGroupBoxStaffel2Bereich2.Visible = False
            RadGroupBoxStaffel2Bereich3.Visible = False
            RadGroupBoxStaffel3Bereich2.Visible = False
            RadGroupBoxStaffel3Bereich3.Visible = False
            RadGroupBoxStaffel4Bereich2.Visible = False
            RadGroupBoxStaffel4Bereich3.Visible = False
            RadGroupBoxStaffel5Bereich2.Visible = False
            RadGroupBoxStaffel5Bereich3.Visible = False

        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            RadGroupBoxStaffel1Bereich2.Visible = True
            RadGroupBoxStaffel1Bereich3.Visible = False
            RadGroupBoxStaffel2Bereich2.Visible = True
            RadGroupBoxStaffel2Bereich3.Visible = False
            RadGroupBoxStaffel3Bereich2.Visible = True
            RadGroupBoxStaffel3Bereich3.Visible = False
            RadGroupBoxStaffel4Bereich2.Visible = True
            RadGroupBoxStaffel4Bereich3.Visible = False
            RadGroupBoxStaffel5Bereich2.Visible = True
            RadGroupBoxStaffel5Bereich3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            RadGroupBoxStaffel1Bereich2.Visible = True
            RadGroupBoxStaffel1Bereich3.Visible = True
            RadGroupBoxStaffel2Bereich2.Visible = True
            RadGroupBoxStaffel2Bereich3.Visible = True
            RadGroupBoxStaffel3Bereich2.Visible = True
            RadGroupBoxStaffel3Bereich3.Visible = True
            RadGroupBoxStaffel4Bereich2.Visible = True
            RadGroupBoxStaffel4Bereich3.Visible = True
            RadGroupBoxStaffel5Bereich2.Visible = True
            RadGroupBoxStaffel5Bereich3.Visible = True

        End If

        'füllen der berechnenten Steuerelemente

        LadeStaffel1()
        Try
            LadeStaffel2()
        Catch ex As Exception
        End Try
        Try
            LadeStaffel3()
        Catch e As Exception
        End Try
        Try
            LadeStaffel4()
        Catch e1 As Exception
        End Try

        Try
            LadeStaffel5()
        Catch e As Exception
        End Try

        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlStaffel1Bereich1Anzeige1.Focus()

    End Sub


    Private Sub LadeStaffel1()
        'staffel 1
        'bereich 1
        Try
            _currentObjPruefungStaffelverfahrenNormallast = Nothing
            _currentObjPruefungStaffelverfahrenNormallast = (From o In _ListPruefungStaffelverfahrenNormallast Where o.Bereich = "1" And o.Staffel = "1").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenNormallast Is Nothing Then
                RadTextBoxControlStaffel1Bereich1Last1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_1
                RadTextBoxControlStaffel1Bereich1Last2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_2
                RadTextBoxControlStaffel1Bereich1Last3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_3
                RadTextBoxControlStaffel1Bereich1Last4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_4

                RadTextBoxControlStaffel1Bereich1Anzeige1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_1
                RadTextBoxControlStaffel1Bereich1Anzeige2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_2
                RadTextBoxControlStaffel1Bereich1Anzeige3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_3
                RadTextBoxControlStaffel1Bereich1Anzeige4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_4

                RadTextBoxControlStaffel1Bereich1Fehler1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_1
                RadTextBoxControlStaffel1Bereich1Fehler2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_2
                RadTextBoxControlStaffel1Bereich1Fehler3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_3
                RadTextBoxControlStaffel1Bereich1Fehler4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_4

                RadTextBoxControlStaffel1Bereich1Fehler5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel1Bereich1Fehler6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel1Bereich1Fehler7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_Fehler

                lblStaffel1Bereich1EFGWert1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_1
                lblStaffel1Bereich1EFGWert2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_2
                lblStaffel1Bereich1EFGWert3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_3
                lblStaffel1Bereich1EFGWert4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_4

                lblStaffel1Bereich1EFGWert5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_EFG
                lblStaffel1Bereich1EFGWert6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_EFG
                lblStaffel1Bereich1EFGWert7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_EFG
            Else
                RadTextBoxControlStaffel1Bereich1Last1.Text = "0"
                RadTextBoxControlStaffel1Bereich1Last2.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1

                lblStaffel1Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                    RadTextBoxControlStaffel1Bereich1Last3.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                Else
                    RadTextBoxControlStaffel1Bereich1Last3.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                End If
                RadTextBoxControlStaffel1Bereich1Last4.Text = "0"
            End If

            Try
                'staffel 1 bereich 2
                _currentObjPruefungStaffelverfahrenNormallast = Nothing
                _currentObjPruefungStaffelverfahrenNormallast = (From o In _ListPruefungStaffelverfahrenNormallast Where o.Bereich = "2" And o.Staffel = "1").FirstOrDefault

                If Not _currentObjPruefungStaffelverfahrenNormallast Is Nothing Then
                    RadTextBoxControlStaffel1Bereich2Last1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_1
                    RadTextBoxControlStaffel1Bereich2Last2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_2
                    RadTextBoxControlStaffel1Bereich2Last3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_3
                    RadTextBoxControlStaffel1Bereich2Last4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_4

                    RadTextBoxControlStaffel1Bereich2Anzeige1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_1
                    RadTextBoxControlStaffel1Bereich2Anzeige2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_2
                    RadTextBoxControlStaffel1Bereich2Anzeige3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_3
                    RadTextBoxControlStaffel1Bereich2Anzeige4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_4

                    RadTextBoxControlStaffel1Bereich2Fehler1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_1
                    RadTextBoxControlStaffel1Bereich2Fehler2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_2
                    RadTextBoxControlStaffel1Bereich2Fehler3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_3
                    RadTextBoxControlStaffel1Bereich2Fehler4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_4

                    RadTextBoxControlStaffel1Bereich2Fehler5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_Fehler
                    RadTextBoxControlStaffel1Bereich2Fehler6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_Fehler
                    RadTextBoxControlStaffel1Bereich2Fehler7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_Fehler

                    lblStaffel1Bereich2EFGWert1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_1
                    lblStaffel1Bereich2EFGWert2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_2
                    lblStaffel1Bereich2EFGWert3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_3
                    lblStaffel1Bereich2EFGWert4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_4

                    lblStaffel1Bereich2EFGWert5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_EFG
                    lblStaffel1Bereich2EFGWert6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_EFG
                    lblStaffel1Bereich2EFGWert7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_EFG

                Else
                    RadTextBoxControlStaffel1Bereich2Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2

                    'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                    If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                        RadTextBoxControlStaffel1Bereich2Last3.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                    Else
                        RadTextBoxControlStaffel1Bereich2Last3.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                    End If

                    RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
                    RadTextBoxControlStaffel1Bereich2Last2.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2

                    lblStaffel1Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                End If
            Catch ex As Exception
            End Try


            'staffel 1 bereich 3
            Try
                _currentObjPruefungStaffelverfahrenNormallast = Nothing
                _currentObjPruefungStaffelverfahrenNormallast = (From o In _ListPruefungStaffelverfahrenNormallast Where o.Bereich = "3" And o.Staffel = "1").FirstOrDefault

                If Not _currentObjPruefungStaffelverfahrenNormallast Is Nothing Then
                    RadTextBoxControlStaffel1Bereich3Last1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_1
                    RadTextBoxControlStaffel1Bereich3Last2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_2
                    RadTextBoxControlStaffel1Bereich3Last3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_3
                    RadTextBoxControlStaffel1Bereich3Last4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Last_4

                    RadTextBoxControlStaffel1Bereich3Anzeige1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_1
                    RadTextBoxControlStaffel1Bereich3Anzeige2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_2
                    RadTextBoxControlStaffel1Bereich3Anzeige3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_3
                    RadTextBoxControlStaffel1Bereich3Anzeige4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Anzeige_4

                    RadTextBoxControlStaffel1Bereich3Fehler1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_1
                    RadTextBoxControlStaffel1Bereich3Fehler2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_2
                    RadTextBoxControlStaffel1Bereich3Fehler3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_3
                    RadTextBoxControlStaffel1Bereich3Fehler4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_Fehler_4

                    RadTextBoxControlStaffel1Bereich3Fehler5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_Fehler
                    RadTextBoxControlStaffel1Bereich3Fehler6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_Fehler
                    RadTextBoxControlStaffel1Bereich3Fehler7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_Fehler

                    lblStaffel1Bereich3EFGWert1.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_1
                    lblStaffel1Bereich3EFGWert2.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_2
                    lblStaffel1Bereich3EFGWert3.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_3
                    lblStaffel1Bereich3EFGWert4.Text = _currentObjPruefungStaffelverfahrenNormallast.NormalLast_EFG_4

                    lblStaffel1Bereich3EFGWert5.Text = _currentObjPruefungStaffelverfahrenNormallast.DifferenzAnzeigewerte_EFG
                    lblStaffel1Bereich3EFGWert6.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungStaffel_EFG
                    lblStaffel1Bereich3EFGWert7.Text = _currentObjPruefungStaffelverfahrenNormallast.MessabweichungWaage_EFG

                Else
                    RadTextBoxControlStaffel1Bereich3Last1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
                    'last 2 Berechnen
                    'last 2 Berechnen Wenn es mehr Normallast gibt, als Min benötigt wird, kann der MIN wert übernommen werden. sonst muss soviel genommen werden, wie da ist
                    If CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) > CDec(objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien) Then
                        RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien
                    Else
                        RadTextBoxControlStaffel1Bereich2Last4.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast
                    End If
                    RadTextBoxControlStaffel1Bereich3Last4.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
                    RadTextBoxControlStaffel1Bereich3Last2.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3

                    lblStaffel1Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5

                End If


            Catch e As Exception
            End Try

            CalculateStaffel1Bereich1()
            CalculateStaffel1Bereich2()
            CalculateStaffel1Bereich3()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub LadeStaffel2()

        Try

            'staffel 2
            'bereich1
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "1" And o.Staffel = "2").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel2Bereich1Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel2Bereich1Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel2Bereich1Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel2Bereich1Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel2Bereich1Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel2Bereich1Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel2Bereich1Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel2Bereich1Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel2Bereich1Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel2Bereich1Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel2Bereich1EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel2Bereich1EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel2Bereich1EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel2Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel2Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel2Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel2Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                Else
                    lblStaffel2Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                    lblStaffel2Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                End If


            End If
            'bereich 2
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "2" And o.Staffel = "2").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel2Bereich2Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel2Bereich2Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel2Bereich2Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel2Bereich2Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel2Bereich2Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel2Bereich2Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel2Bereich2Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel2Bereich2Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel2Bereich2Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel2Bereich2Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel2Bereich2EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel2Bereich2EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel2Bereich2EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel2Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel2Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel2Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel2Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                Else
                    lblStaffel2Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                    lblStaffel2Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                End If

            End If

            'bereich 3
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "3" And o.Staffel = "2").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel2Bereich3Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel2Bereich3Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel2Bereich3Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel2Bereich3Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel2Bereich3Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel2Bereich3Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel2Bereich3Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel2Bereich3Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel2Bereich3Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel2Bereich3Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel2Bereich3EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel2Bereich3EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel2Bereich3EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel2Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel2Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel2Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel2Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                Else
                    lblStaffel2Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                    lblStaffel2Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                End If

            End If

        Catch ex As Exception
        End Try


        CalculateStaffel2Bereich1()
        CalculateStaffel2Bereich2()
        CalculateStaffel2Bereich3()
    End Sub
    Private Sub LadeStaffel3()
        Try
            'staffel 3
            'bereich1
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "1" And o.Staffel = "3").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel3Bereich1Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel3Bereich1Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel3Bereich1Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel3Bereich1Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel3Bereich1Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel3Bereich1Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel3Bereich1Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel3Bereich1Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel3Bereich1Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel3Bereich1Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel3Bereich1EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel3Bereich1EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel3Bereich1EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel3Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel3Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel3Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel3Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                Else
                    lblStaffel3Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                    lblStaffel3Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                End If


            End If
            'bereich 2
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "2" And o.Staffel = "3").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel3Bereich2Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel3Bereich2Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel3Bereich2Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel3Bereich2Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel3Bereich2Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel3Bereich2Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel3Bereich2Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel3Bereich2Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel3Bereich2Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel3Bereich2Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel3Bereich2EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel3Bereich2EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel3Bereich2EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel3Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel3Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel3Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel3Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                Else
                    lblStaffel3Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                    lblStaffel3Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                End If

            End If

            'bereich 3
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "3" And o.Staffel = "3").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel3Bereich3Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel3Bereich3Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel3Bereich3Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel3Bereich3Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel3Bereich3Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel3Bereich3Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel3Bereich3Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel3Bereich3Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel3Bereich3Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel3Bereich3Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel3Bereich3EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel3Bereich3EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel3Bereich3EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel3Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel3Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel3Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel3Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                Else
                    lblStaffel3Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                    lblStaffel3Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                End If

            End If

        Catch e As Exception
        End Try
        CalculateStaffel3Bereich1()
        CalculateStaffel3Bereich2()
        CalculateStaffel3Bereich3()
    End Sub
    Private Sub LadeStaffel4()
        'staffel 4
        'bereich1
        Try
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "1" And o.Staffel = "4").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel4Bereich1Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel4Bereich1Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel4Bereich1Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel4Bereich1Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel4Bereich1Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel4Bereich1Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel4Bereich1Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel4Bereich1Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel4Bereich1Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel4Bereich1Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel4Bereich1EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel4Bereich1EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel4Bereich1EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel4Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel4Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel4Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel4Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                Else
                    lblStaffel4Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                    lblStaffel4Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                End If


            End If
            'bereich 2
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "2" And o.Staffel = "4").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel4Bereich2Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel4Bereich2Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel4Bereich2Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel4Bereich2Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel4Bereich2Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel4Bereich2Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel4Bereich2Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel4Bereich2Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel4Bereich2Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel4Bereich2Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel4Bereich2EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel4Bereich2EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel4Bereich2EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel4Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel4Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel4Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel4Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                Else
                    lblStaffel4Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                    lblStaffel4Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                End If

            End If

            'bereich 3
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "3" And o.Staffel = "4").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel4Bereich3Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel4Bereich3Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel4Bereich3Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel4Bereich3Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel4Bereich3Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel4Bereich3Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel4Bereich3Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel4Bereich3Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel4Bereich3Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel4Bereich3Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel4Bereich3EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel4Bereich3EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel4Bereich3EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel4Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel4Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel4Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel4Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                Else
                    lblStaffel4Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                    lblStaffel4Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                End If

            End If

        Catch e As Exception
        End Try

        CalculateStaffel4Bereich1()
        CalculateStaffel4Bereich2()
        CalculateStaffel4Bereich3()
    End Sub
    Private Sub LadeStaffel5()
        'staffel 5
        'bereich1
        Try
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "1" And o.Staffel = "5").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel5Bereich1Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel5Bereich1Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel5Bereich1Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel5Bereich1Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel5Bereich1Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel5Bereich1Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel5Bereich1Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel5Bereich1Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel5Bereich1Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel5Bereich1Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel5Bereich1EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel5Bereich1EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel5Bereich1EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel5Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel5Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel5Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel5Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                Else
                    lblStaffel5Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                    lblStaffel5Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                End If


            End If
            'bereich 2
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "2" And o.Staffel = "5").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel5Bereich2Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel5Bereich2Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel5Bereich2Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel5Bereich2Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel5Bereich2Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel5Bereich2Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel5Bereich2Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel5Bereich2Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel5Bereich2Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel5Bereich2Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel5Bereich2EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel5Bereich2EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel5Bereich2EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel5Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel5Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel5Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel5Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                Else
                    lblStaffel5Bereich2EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                    lblStaffel5Bereich2EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                End If

            End If

            'bereich 3
            _currentObjPruefungStaffelverfahrenErsatzlast = Nothing
            _currentObjPruefungStaffelverfahrenErsatzlast = (From o In _ListPruefungStaffelverfahrenErsatzlast Where o.Bereich = "3" And o.Staffel = "5").FirstOrDefault

            If Not _currentObjPruefungStaffelverfahrenErsatzlast Is Nothing Then
                RadTextBoxControlStaffel5Bereich3Last1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Soll
                RadTextBoxControlStaffel5Bereich3Last2.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ZusaetzlicheErsatzlast_Soll
                RadTextBoxControlStaffel5Bereich3Last3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Soll
                RadTextBoxControlStaffel5Bereich3Last4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Soll

                RadTextBoxControlStaffel5Bereich3Anzeige1.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast_Ist
                RadTextBoxControlStaffel5Bereich3Anzeige3.Text = _currentObjPruefungStaffelverfahrenErsatzlast.ErsatzUndNormallast_Ist
                RadTextBoxControlStaffel5Bereich3Anzeige4.Text = _currentObjPruefungStaffelverfahrenErsatzlast.Ersatzlast2_Ist

                RadTextBoxControlStaffel5Bereich3Fehler5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_Fehler
                RadTextBoxControlStaffel5Bereich3Fehler6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_Fehler
                RadTextBoxControlStaffel5Bereich3Fehler7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_Fehler

                lblStaffel5Bereich3EFGWert5.Text = _currentObjPruefungStaffelverfahrenErsatzlast.DifferenzAnzeigewerte_EFG
                lblStaffel5Bereich3EFGWert6.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungStaffel_EFG
                lblStaffel5Bereich3EFGWert7.Text = _currentObjPruefungStaffelverfahrenErsatzlast.MessabweichungWaage_EFG

            Else
                'EFG Normallast
                lblStaffel5Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
                'EFG Differenz der Staffel
                'EFG differenz der Waage
                '=WENN(B142<$B$50;WERT(1*$B$15);(1,5*$B$15))
                If CDec(RadTextBoxControlStaffel5Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel5Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel5Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                Else
                    lblStaffel5Bereich3EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                    lblStaffel5Bereich3EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                End If

            End If

        Catch e As Exception
        End Try

        CalculateStaffel5Bereich1()
        CalculateStaffel5Bereich2()
        CalculateStaffel5Bereich3()
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
            For Each objPruefung In _ListPruefungStaffelverfahrenNormallast
                objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsObject(objPruefung)
            Next

            'jedes objekt initialisieren und aus context laden und updaten
            For Each objPruefung In _ListPruefungStaffelverfahrenErsatzlast
                objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                UpdatePruefungsObject(objPruefung)
            Next

        End Using
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungStaffelverfahrenNormallast)
        If PObjPruefung.Staffel = 1 Then
            If PObjPruefung.Bereich = 1 Then
                PObjPruefung.NormalLast_Last_1 = RadTextBoxControlStaffel1Bereich1Last1.Text
                PObjPruefung.NormalLast_Last_2 = RadTextBoxControlStaffel1Bereich1Last2.Text
                PObjPruefung.NormalLast_Last_3 = RadTextBoxControlStaffel1Bereich1Last3.Text
                PObjPruefung.NormalLast_Last_4 = RadTextBoxControlStaffel1Bereich1Last4.Text
                PObjPruefung.NormalLast_Anzeige_1 = RadTextBoxControlStaffel1Bereich1Anzeige1.Text
                PObjPruefung.NormalLast_Anzeige_2 = RadTextBoxControlStaffel1Bereich1Anzeige2.Text
                PObjPruefung.NormalLast_Anzeige_3 = RadTextBoxControlStaffel1Bereich1Anzeige3.Text
                PObjPruefung.NormalLast_Anzeige_4 = RadTextBoxControlStaffel1Bereich1Anzeige4.Text
                PObjPruefung.NormalLast_Fehler_1 = RadTextBoxControlStaffel1Bereich1Fehler1.Text
                PObjPruefung.NormalLast_Fehler_2 = RadTextBoxControlStaffel1Bereich1Fehler2.Text
                PObjPruefung.NormalLast_Fehler_3 = RadTextBoxControlStaffel1Bereich1Fehler3.Text
                PObjPruefung.NormalLast_Fehler_4 = RadTextBoxControlStaffel1Bereich1Fehler4.Text
                PObjPruefung.NormalLast_EFG_1 = lblStaffel1Bereich1EFGWert1.Text
                PObjPruefung.NormalLast_EFG_2 = lblStaffel1Bereich1EFGWert2.Text
                PObjPruefung.NormalLast_EFG_3 = lblStaffel1Bereich1EFGWert3.Text
                PObjPruefung.NormalLast_EFG_4 = lblStaffel1Bereich1EFGWert4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel1Bereich1Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel1Bereich1EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel1Bereich1Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel1Bereich1EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel1Bereich1Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel1Bereich1EFGWert7.Text
            ElseIf PObjPruefung.Bereich = 2 Then
                PObjPruefung.NormalLast_Last_1 = RadTextBoxControlStaffel1Bereich2Last1.Text
                PObjPruefung.NormalLast_Last_2 = RadTextBoxControlStaffel1Bereich2Last2.Text
                PObjPruefung.NormalLast_Last_3 = RadTextBoxControlStaffel1Bereich2Last3.Text
                PObjPruefung.NormalLast_Last_4 = RadTextBoxControlStaffel1Bereich2Last4.Text
                PObjPruefung.NormalLast_Anzeige_1 = RadTextBoxControlStaffel1Bereich2Anzeige1.Text
                PObjPruefung.NormalLast_Anzeige_2 = RadTextBoxControlStaffel1Bereich2Anzeige2.Text
                PObjPruefung.NormalLast_Anzeige_3 = RadTextBoxControlStaffel1Bereich2Anzeige3.Text
                PObjPruefung.NormalLast_Anzeige_4 = RadTextBoxControlStaffel1Bereich2Anzeige4.Text
                PObjPruefung.NormalLast_Fehler_1 = RadTextBoxControlStaffel1Bereich2Fehler1.Text
                PObjPruefung.NormalLast_Fehler_2 = RadTextBoxControlStaffel1Bereich2Fehler2.Text
                PObjPruefung.NormalLast_Fehler_3 = RadTextBoxControlStaffel1Bereich2Fehler3.Text
                PObjPruefung.NormalLast_Fehler_4 = RadTextBoxControlStaffel1Bereich2Fehler4.Text
                PObjPruefung.NormalLast_EFG_1 = lblStaffel1Bereich2EFGWert1.Text
                PObjPruefung.NormalLast_EFG_2 = lblStaffel1Bereich2EFGWert2.Text
                PObjPruefung.NormalLast_EFG_3 = lblStaffel1Bereich2EFGWert3.Text
                PObjPruefung.NormalLast_EFG_4 = lblStaffel1Bereich2EFGWert4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel1Bereich2Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel1Bereich2EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel1Bereich2Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel1Bereich2EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel1Bereich2Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel1Bereich2EFGWert7.Text
            ElseIf PObjPruefung.Bereich = 3 Then
                PObjPruefung.NormalLast_Last_1 = RadTextBoxControlStaffel1Bereich3Last1.Text
                PObjPruefung.NormalLast_Last_2 = RadTextBoxControlStaffel1Bereich3Last2.Text
                PObjPruefung.NormalLast_Last_3 = RadTextBoxControlStaffel1Bereich3Last3.Text
                PObjPruefung.NormalLast_Last_4 = RadTextBoxControlStaffel1Bereich3Last4.Text
                PObjPruefung.NormalLast_Anzeige_1 = RadTextBoxControlStaffel1Bereich3Anzeige1.Text
                PObjPruefung.NormalLast_Anzeige_2 = RadTextBoxControlStaffel1Bereich3Anzeige2.Text
                PObjPruefung.NormalLast_Anzeige_3 = RadTextBoxControlStaffel1Bereich3Anzeige3.Text
                PObjPruefung.NormalLast_Anzeige_4 = RadTextBoxControlStaffel1Bereich3Anzeige4.Text
                PObjPruefung.NormalLast_Fehler_1 = RadTextBoxControlStaffel1Bereich3Fehler1.Text
                PObjPruefung.NormalLast_Fehler_2 = RadTextBoxControlStaffel1Bereich3Fehler2.Text
                PObjPruefung.NormalLast_Fehler_3 = RadTextBoxControlStaffel1Bereich3Fehler3.Text
                PObjPruefung.NormalLast_Fehler_4 = RadTextBoxControlStaffel1Bereich3Fehler4.Text
                PObjPruefung.NormalLast_EFG_1 = lblStaffel1Bereich3EFGWert1.Text
                PObjPruefung.NormalLast_EFG_2 = lblStaffel1Bereich3EFGWert2.Text
                PObjPruefung.NormalLast_EFG_3 = lblStaffel1Bereich3EFGWert3.Text
                PObjPruefung.NormalLast_EFG_4 = lblStaffel1Bereich3EFGWert4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel1Bereich3Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel1Bereich3EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel1Bereich3Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel1Bereich3EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel1Bereich3Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel1Bereich3EFGWert7.Text
            End If
        End If
    End Sub

    Private Sub UpdatePruefungsObject(ByVal PObjPruefung As PruefungStaffelverfahrenErsatzlast)
        If PObjPruefung.Staffel = "2" Then
            If PObjPruefung.Bereich = "1" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel2Bereich1Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel2Bereich1Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel2Bereich1Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel2Bereich1Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel2Bereich1Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel2Bereich1Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel2Bereich1Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel2Bereich1Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel2Bereich1EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel2Bereich1Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel2Bereich1EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel2Bereich1Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel2Bereich1EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "2" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel2Bereich2Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel2Bereich2Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel2Bereich2Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel2Bereich2Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel2Bereich2Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel2Bereich2Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel2Bereich2Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel2Bereich2Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel2Bereich2EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel2Bereich2Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel2Bereich2EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel2Bereich2Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel2Bereich2EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "3" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel2Bereich3Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel2Bereich3Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel2Bereich3Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel2Bereich3Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel2Bereich3Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel2Bereich3Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel2Bereich3Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel2Bereich3Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel2Bereich3EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel2Bereich3Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel2Bereich3EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel2Bereich3Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel2Bereich3EFGWert7.Text
            End If
        ElseIf PObjPruefung.Staffel = "3" Then
            If PObjPruefung.Bereich = "1" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel3Bereich1Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel3Bereich1Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel3Bereich1Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel3Bereich1Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel3Bereich1Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel3Bereich1Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel3Bereich1Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel3Bereich1Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel3Bereich1EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel3Bereich1Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel3Bereich1EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel3Bereich1Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel3Bereich1EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "2" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel3Bereich2Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel3Bereich2Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel3Bereich2Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel3Bereich2Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel3Bereich2Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel3Bereich2Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel3Bereich2Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel3Bereich2Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel3Bereich2EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel3Bereich2Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel3Bereich2EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel3Bereich2Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel3Bereich2EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "3" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel3Bereich3Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel3Bereich3Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel3Bereich3Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel3Bereich3Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel3Bereich3Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel3Bereich3Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel3Bereich3Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel3Bereich3Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel3Bereich3EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel3Bereich3Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel3Bereich3EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel3Bereich3Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel3Bereich3EFGWert7.Text
            End If
        ElseIf PObjPruefung.Staffel = "4" Then
            If PObjPruefung.Bereich = "1" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel4Bereich1Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel4Bereich1Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel4Bereich1Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel4Bereich1Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel4Bereich1Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel4Bereich1Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel4Bereich1Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel4Bereich1Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel4Bereich1EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel4Bereich1Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel4Bereich1EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel4Bereich1Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel4Bereich1EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "2" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel4Bereich2Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel4Bereich2Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel4Bereich2Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel4Bereich2Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel4Bereich2Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel4Bereich2Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel4Bereich2Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel4Bereich2Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel4Bereich2EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel4Bereich2Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel4Bereich2EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel4Bereich2Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel4Bereich2EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "3" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel4Bereich3Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel4Bereich3Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel4Bereich3Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel4Bereich3Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel4Bereich3Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel4Bereich3Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel4Bereich3Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel4Bereich3Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel4Bereich3EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel4Bereich3Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel4Bereich3EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel4Bereich3Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel4Bereich3EFGWert7.Text
            End If
        ElseIf PObjPruefung.Staffel = "5" Then
            If PObjPruefung.Bereich = "1" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel5Bereich1Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel5Bereich1Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel5Bereich1Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel5Bereich1Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel5Bereich1Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel5Bereich1Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel5Bereich1Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel5Bereich1Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel5Bereich1EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel5Bereich1Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel5Bereich1EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel5Bereich1Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel5Bereich1EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "2" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel5Bereich2Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel5Bereich2Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel5Bereich2Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel5Bereich2Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel5Bereich2Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel5Bereich2Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel5Bereich2Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel5Bereich2Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel5Bereich2EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel5Bereich2Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel5Bereich2EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel5Bereich2Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel5Bereich2EFGWert7.Text
            ElseIf PObjPruefung.Bereich = "3" Then
                PObjPruefung.Ersatzlast_Soll = RadTextBoxControlStaffel5Bereich3Last1.Text
                PObjPruefung.Ersatzlast_Ist = RadTextBoxControlStaffel5Bereich3Anzeige1.Text
                PObjPruefung.ZusaetzlicheErsatzlast_Soll = RadTextBoxControlStaffel5Bereich3Last2.Text
                PObjPruefung.ErsatzUndNormallast_Soll = RadTextBoxControlStaffel5Bereich3Last3.Text
                PObjPruefung.ErsatzUndNormallast_Ist = RadTextBoxControlStaffel5Bereich3Anzeige3.Text
                PObjPruefung.Ersatzlast2_Soll = RadTextBoxControlStaffel5Bereich3Last4.Text
                PObjPruefung.Ersatzlast2_Ist = RadTextBoxControlStaffel5Bereich3Anzeige4.Text
                PObjPruefung.DifferenzAnzeigewerte_Fehler = RadTextBoxControlStaffel5Bereich3Fehler5.Text
                PObjPruefung.DifferenzAnzeigewerte_EFG = lblStaffel5Bereich3EFGWert5.Text
                PObjPruefung.MessabweichungStaffel_Fehler = RadTextBoxControlStaffel5Bereich3Fehler6.Text
                PObjPruefung.MessabweichungStaffel_EFG = lblStaffel5Bereich3EFGWert6.Text
                PObjPruefung.MessabweichungWaage_Fehler = RadTextBoxControlStaffel5Bereich3Fehler7.Text
                PObjPruefung.MessabweichungWaage_EFG = lblStaffel5Bereich3EFGWert7.Text
            End If
        End If
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

        Dim intausgefuellteStaffeln As Integer = 3
        For Each StaffelGroupBox In RadScrollablePanel1.PanelContainer.Controls
            'nur die ersten 3 Staffeln sind Pflicht
            If StaffelGroupBox.name.ToString.EndsWith("4") Or StaffelGroupBox.name.ToString.EndsWith("5") Then
                If RadTextBoxControlStaffel4Bereich1Anzeige1.Text = "" Then
                    Continue For
                Else
                    For Each BereichGroupBox In StaffelGroupBox.controls
                        If CType(BereichGroupBox, Telerik.WinControls.UI.RadGroupBox).Visible = True Then
                            For Each Control In BereichGroupBox.controls
                                Try
                                    If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).IsReadOnly = False Then
                                        If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Text.Trim = "" Then
                                            AbortSaveing = True
                                            intausgefuellteStaffeln = 3 'die staffel wurde nicht voll ausgefüllt
                                        Else
                                            intausgefuellteStaffeln = 4 'die 4 Staffel wurde auch ausgefüllt
                                        End If

                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                    Next
                End If


                If intausgefuellteStaffeln = 4 Then ' nur prüfen wenn die 4. Staffel ausgefüllt wurde

                    If RadTextBoxControlStaffel5Bereich1Anzeige1.Text = "" Then
                        Continue For
                    Else
                        For Each BereichGroupBox In StaffelGroupBox.controls
                            If CType(BereichGroupBox, Telerik.WinControls.UI.RadGroupBox).Visible = True Then
                                For Each Control In BereichGroupBox.controls
                                    Try
                                        If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).IsReadOnly = False Then
                                            If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Text.Trim = "" Then
                                                AbortSaveing = True
                                                intausgefuellteStaffeln = 4 'die staffel wurde nicht voll ausgefüllt
                                            Else
                                                intausgefuellteStaffeln = 5 'die 5 Staffel wurde auch ausgefüllt
                                            End If
                                        End If
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If
                        Next
                    End If
                End If
            End If

            For Each BereichGroupBox In StaffelGroupBox.controls
                If CType(BereichGroupBox, Telerik.WinControls.UI.RadGroupBox).Visible = True Then
                    For Each Control In BereichGroupBox.controls
                        Try
                            If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).IsReadOnly = False Then
                                If CType(Control, Telerik.WinControls.UI.RadTextBoxControl).Text.Trim = "" Then
                                    AbortSaveing = True
                                End If
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Next
        Next




        If AbortSaveing Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        'logik zum Valideren der Eichfehlergrenzen der einzelnen Staffeln. Abhängig davon wieviele Staffeln überhaupt ausgefüllt sind
        'staffel 1- 3 sind ab dieser Stelle im Code aufjedenfall ausgefüllt
        Dim decAbsoluteFehlergrenze As Decimal = 0

        'Staffel 1
        Try
            If RadTextBoxControlStaffel1Bereich1Fehler7.Text.Trim.StartsWith("-") Then
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel1Bereich1Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
            Else
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel1Bereich1Fehler7.Text)
            End If

            If decAbsoluteFehlergrenze >= CDec(lblStaffel1Bereich1EFGWert7.Text) Then 'eichwerte unterschritten/überschritten
                AbortSaveing = True
            End If
        Catch e As Exception
        End Try
        'staffel 2
        Try
            If RadTextBoxControlStaffel2Bereich1Fehler7.Text.Trim.StartsWith("-") Then
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel2Bereich1Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
            Else
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel2Bereich1Fehler7.Text)
            End If

            If decAbsoluteFehlergrenze >= CDec(lblStaffel2Bereich1EFGWert7.Text) Then 'eichwerte unterschritten/überschritten
                AbortSaveing = True
            End If
        Catch e As Exception
        End Try
        'staffel 3
        Try
            If RadTextBoxControlStaffel3Bereich1Fehler7.Text.Trim.StartsWith("-") Then
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel3Bereich1Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
            Else
                decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel3Bereich1Fehler7.Text)
            End If

            If decAbsoluteFehlergrenze >= CDec(lblStaffel3Bereich1EFGWert7.Text) Then 'eichwerte unterschritten/überschritten
                AbortSaveing = True
            End If
        Catch e As Exception
        End Try

        Select Case intausgefuellteStaffeln
            Case Is = 4
                Try
                    If RadTextBoxControlStaffel4Bereich1Fehler7.Text.Trim.StartsWith("-") Then
                        decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel4Bereich1Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
                    Else
                        decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel4Bereich1Fehler7.Text)
                    End If

                    If decAbsoluteFehlergrenze >= CDec(lblStaffel4Bereich1EFGWert7.Text) Then 'eichwerte unterschritten/überschritten
                        AbortSaveing = True
                    End If
                Catch e As Exception
                End Try
            Case Is = 5
                Try
                    If RadTextBoxControlStaffel5Bereich1Fehler7.Text.Trim.StartsWith("-") Then
                        decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel5Bereich1Fehler7.Text.Replace("-", "")) 'entfernen des minuszeichens
                    Else
                        decAbsoluteFehlergrenze = CDec(RadTextBoxControlStaffel5Bereich1Fehler7.Text)
                    End If

                    If decAbsoluteFehlergrenze >= CDec(lblStaffel5Bereich1EFGWert7.Text) Then 'eichwerte unterschritten/überschritten
                        AbortSaveing = True
                    End If
                Catch e As Exception
                End Try
        End Select

        If AbortSaveing = True Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.EichfehlergrenzenNichtEingehalten, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
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



                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungStaffelverfahrenNormallast.Count = 0 Then
                                'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                                Dim intBereiche As Integer = 0
                                If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                    intBereiche = 1
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                    intBereiche = 2
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                    intBereiche = 3
                                End If

                                For intBereich = 1 To intBereiche

                                    Dim objPruefung = Context.PruefungStaffelverfahrenNormallast.Create
                                    'wenn es die eine itereation mehr ist:
                                    objPruefung.Bereich = intBereich
                                    objPruefung.Staffel = 1

                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungStaffelverfahrenNormallast.Add(objPruefung)
                                Next

                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungStaffelverfahrenNormallast
                                    objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next

                            End If

                            'ersatzlasten
                            'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                            If _ListPruefungStaffelverfahrenErsatzlast.Count = 0 Then
                                'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                                Dim intBereiche As Integer = 0
                                If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                    intBereiche = 1
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                    intBereiche = 2
                                ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                    intBereiche = 3
                                End If

                                For intStaffel As Integer = 2 To 5

                                    For intBereich = 1 To intBereiche

                                        Dim objPruefung = Context.PruefungStaffelverfahrenErsatzlast.Create
                                        'wenn es die eine itereation mehr ist:
                                        objPruefung.Bereich = intBereich
                                        objPruefung.Staffel = intStaffel

                                        UpdatePruefungsObject(objPruefung)

                                        Context.SaveChanges()

                                        objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                        Context.SaveChanges()

                                        _ListPruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                    Next
                                Next

                            Else ' es gibt bereits welche
                                'jedes objekt initialisieren und aus context laden und updaten
                                For Each objPruefung In _ListPruefungStaffelverfahrenErsatzlast
                                    objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                    UpdatePruefungsObject(objPruefung)
                                    Context.SaveChanges()
                                Next

                            End If


                            'neuen Status zuweisen
                            If AktuellerStatusDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit
                                AktuellerStatusDirty = False
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
        MyBase.SaveWithoutValidationNeeded(usercontrol)
        If Me.Equals(usercontrol) Then


            'neuen Context aufbauen
            Using Context As New EichsoftwareClientdatabaseEntities1
                If DialogModus = enuDialogModus.lesend Then
                    UpdateObject()
                    ParentFormular.CurrentEichprozess = objEichprozess
                    Exit Sub
                End If

                'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                    'prüfen ob das Objekt anhand der ID gefunden werden kann
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                    If Not dobjEichprozess Is Nothing Then
                        'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                        objEichprozess = dobjEichprozess



                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungStaffelverfahrenNormallast.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

                            For intBereich = 1 To intBereiche

                                Dim objPruefung = Context.PruefungStaffelverfahrenNormallast.Create
                                'wenn es die eine itereation mehr ist:
                                objPruefung.Bereich = intBereich
                                objPruefung.Staffel = 1

                                UpdatePruefungsObject(objPruefung)

                                Context.SaveChanges()

                                objEichprozess.Eichprotokoll.PruefungStaffelverfahrenNormallast.Add(objPruefung)
                                Context.SaveChanges()

                                _ListPruefungStaffelverfahrenNormallast.Add(objPruefung)
                            Next

                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungStaffelverfahrenNormallast
                                objPruefung = Context.PruefungStaffelverfahrenNormallast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next

                        End If

                        'ersatzlasten
                        'wenn es defintiv noch keine pruefungen gibt, neue Anlegen
                        If _ListPruefungStaffelverfahrenErsatzlast.Count = 0 Then
                            'anzahl Bereiche auslesen um damit die anzahl der benötigten Iterationen und Objekt Erzeugungen zu erfahren
                            Dim intBereiche As Integer = 0
                            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                                intBereiche = 1
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                                intBereiche = 2
                            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                                intBereiche = 3
                            End If

                            For intStaffel As Integer = 2 To 5

                                For intBereich = 1 To intBereiche

                                    Dim objPruefung = Context.PruefungStaffelverfahrenErsatzlast.Create
                                    'wenn es die eine itereation mehr ist:
                                    objPruefung.Bereich = intBereich
                                    objPruefung.Staffel = intStaffel

                                    UpdatePruefungsObject(objPruefung)

                                    Context.SaveChanges()

                                    objEichprozess.Eichprotokoll.PruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                    Context.SaveChanges()

                                    _ListPruefungStaffelverfahrenErsatzlast.Add(objPruefung)
                                Next
                            Next

                        Else ' es gibt bereits welche
                            'jedes objekt initialisieren und aus context laden und updaten
                            For Each objPruefung In _ListPruefungStaffelverfahrenErsatzlast
                                objPruefung = Context.PruefungStaffelverfahrenErsatzlast.FirstOrDefault(Function(value) value.ID = objPruefung.ID)
                                UpdatePruefungsObject(objPruefung)
                                Context.SaveChanges()
                            Next

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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco10PruefungStaffelverfahren))

        Me.lblStaffel1Bereich1Anzeige.Text = resources.GetString("lblStaffel1Bereich1Anzeige.Text")
        Me.lblStaffel1Bereich1Differenz.Text = resources.GetString("lblStaffel1Bereich1Differenz.Text")
        Me.lblStaffel1Bereich1EFG.Text = resources.GetString("lblStaffel1Bereich1EFG.Text")
        Me.lblStaffel1Bereich1Fehler.Text = resources.GetString("lblStaffel1Bereich1Fehler.Text")
        Me.lblStaffel1Bereich1Last.Text = resources.GetString("lblStaffel1Bereich1Last.Text")
        Me.lblStaffel1Bereich1MessabweichungStaffel.Text = resources.GetString("lblStaffel1Bereich1MessabweichungStaffel.Text")
        Me.lblStaffel1Bereich1MessabweichungWaage.Text = resources.GetString("lblStaffel1Bereich1MessabweichungWaage.Text")
        Me.lblStaffel1Bereich1NormalLast.Text = resources.GetString("lblStaffel1Bereich1NormalLast.Text")
        Me.lblStaffel1Bereich2Anzeige.Text = resources.GetString("lblStaffel1Bereich2Anzeige.Text")
        Me.lblStaffel1Bereich2Differenz.Text = resources.GetString("lblStaffel1Bereich2Differenz.Text")
        Me.lblStaffel1Bereich2EFG.Text = resources.GetString("lblStaffel1Bereich2EFG.Text")
        Me.lblStaffel1Bereich2Fehler.Text = resources.GetString("lblStaffel1Bereich2Fehler.Text")
        Me.lblStaffel1Bereich2Last.Text = resources.GetString("lblStaffel1Bereich2Last.Text")
        Me.lblStaffel1Bereich2MessabweichungStaffel.Text = resources.GetString("lblStaffel1Bereich2MessabweichungStaffel.Text")
        Me.lblStaffel1Bereich2MessabweichungWaage.Text = resources.GetString("lblStaffel1Bereich2MessabweichungWaage.Text")
        Me.lblStaffel1Bereich2NormalLast.Text = resources.GetString("lblStaffel1Bereich2NormalLast.Text")
        Me.lblStaffel1Bereich3Anzeige.Text = resources.GetString("lblStaffel1Bereich3Anzeige.Text")
        Me.lblStaffel1Bereich3Differenz.Text = resources.GetString("lblStaffel1Bereich3Differenz.Text")
        Me.lblStaffel1Bereich3EFG.Text = resources.GetString("lblStaffel1Bereich3EFG.Text")
        Me.lblStaffel1Bereich3Fehler.Text = resources.GetString("lblStaffel1Bereich3Fehler.Text")
        Me.lblStaffel1Bereich3Last.Text = resources.GetString("lblStaffel1Bereich3Last.Text")
        Me.lblStaffel1Bereich3MessabweichungStaffel.Text = resources.GetString("lblStaffel1Bereich3MessabweichungStaffel.Text")
        Me.lblStaffel1Bereich3MessabweichungWaage.Text = resources.GetString("lblStaffel1Bereich3MessabweichungWaage.Text")
        Me.lblStaffel1Bereich3NormalLast.Text = resources.GetString("lblStaffel1Bereich3NormalLast.Text")
        Me.lblStaffel2Bereich1Anzeige.Text = resources.GetString("lblStaffel2Bereich1Anzeige.Text")
        Me.lblStaffel2Bereich1Differenz.Text = resources.GetString("lblStaffel2Bereich1Differenz.Text")
        Me.lblStaffel2Bereich1EFG.Text = resources.GetString("lblStaffel2Bereich1EFG.Text")
        Me.lblStaffel2Bereich1Fehler.Text = resources.GetString("lblStaffel2Bereich1Fehler.Text")
        Me.lblStaffel2Bereich1Last.Text = resources.GetString("lblStaffel2Bereich1Last.Text")
        Me.lblStaffel2Bereich1MessabweichungStaffel.Text = resources.GetString("lblStaffel2Bereich1MessabweichungStaffel.Text")
        Me.lblStaffel2Bereich1MessabweichungWaage.Text = resources.GetString("lblStaffel2Bereich1MessabweichungWaage.Text")
        Me.lblStaffel2Bereich1NormalLast.Text = resources.GetString("lblStaffel2Bereich1NormalLast.Text")
        Me.lblStaffel2Bereich2Anzeige.Text = resources.GetString("lblStaffel2Bereich2Anzeige.Text")
        Me.lblStaffel2Bereich2Differenz.Text = resources.GetString("lblStaffel2Bereich2Differenz.Text")
        Me.lblStaffel2Bereich2EFG.Text = resources.GetString("lblStaffel2Bereich2EFG.Text")
        Me.lblStaffel2Bereich2Fehler.Text = resources.GetString("lblStaffel2Bereich2Fehler.Text")
        Me.lblStaffel2Bereich2Last.Text = resources.GetString("lblStaffel2Bereich2Last.Text")
        Me.lblStaffel2Bereich2MessabweichungStaffel.Text = resources.GetString("lblStaffel2Bereich2MessabweichungStaffel.Text")
        Me.lblStaffel2Bereich2MessabweichungWaage.Text = resources.GetString("lblStaffel2Bereich2MessabweichungWaage.Text")
        Me.lblStaffel2Bereich2NormalLast.Text = resources.GetString("lblStaffel2Bereich2NormalLast.Text")
        Me.lblStaffel2Bereich3Anzeige.Text = resources.GetString("lblStaffel2Bereich3Anzeige.Text")
        Me.lblStaffel2Bereich3Differenz.Text = resources.GetString("lblStaffel2Bereich3Differenz.Text")
        Me.lblStaffel2Bereich3EFG.Text = resources.GetString("lblStaffel2Bereich3EFG.Text")
        Me.lblStaffel2Bereich3Fehler.Text = resources.GetString("lblStaffel2Bereich3Fehler.Text")
        Me.lblStaffel2Bereich3Last.Text = resources.GetString("lblStaffel2Bereich3Last.Text")
        Me.lblStaffel2Bereich3MessabweichungStaffel.Text = resources.GetString("lblStaffel2Bereich3MessabweichungStaffel.Text")
        Me.lblStaffel2Bereich3MessabweichungWaage.Text = resources.GetString("lblStaffel2Bereich3MessabweichungWaage.Text")
        Me.lblStaffel2Bereich3NormalLast.Text = resources.GetString("lblStaffel2Bereich3NormalLast.Text")
        Me.lblStaffel3Bereich1Anzeige.Text = resources.GetString("lblStaffel3Bereich1Anzeige.Text")
        Me.lblStaffel3Bereich1Differenz.Text = resources.GetString("lblStaffel3Bereich1Differenz.Text")
        Me.lblStaffel3Bereich1EFG.Text = resources.GetString("lblStaffel3Bereich1EFG.Text")
        Me.lblStaffel3Bereich1Fehler.Text = resources.GetString("lblStaffel3Bereich1Fehler.Text")
        Me.lblStaffel3Bereich1Last.Text = resources.GetString("lblStaffel3Bereich1Last.Text")
        Me.lblStaffel3Bereich1MessabweichungStaffel.Text = resources.GetString("lblStaffel3Bereich1MessabweichungStaffel.Text")
        Me.lblStaffel3Bereich1MessabweichungWaage.Text = resources.GetString("lblStaffel3Bereich1MessabweichungWaage.Text")
        Me.lblStaffel3Bereich1NormalLast.Text = resources.GetString("lblStaffel3Bereich1NormalLast.Text")
        Me.lblStaffel3Bereich2Anzeige.Text = resources.GetString("lblStaffel3Bereich2Anzeige.Text")
        Me.lblStaffel3Bereich2Differenz.Text = resources.GetString("lblStaffel3Bereich2Differenz.Text")
        Me.lblStaffel3Bereich2EFG.Text = resources.GetString("lblStaffel3Bereich2EFG.Text")
        Me.lblStaffel3Bereich2Fehler.Text = resources.GetString("lblStaffel3Bereich2Fehler.Text")
        Me.lblStaffel3Bereich2Last.Text = resources.GetString("lblStaffel3Bereich2Last.Text")
        Me.lblStaffel3Bereich2MessabweichungStaffel.Text = resources.GetString("lblStaffel3Bereich2MessabweichungStaffel.Text")
        Me.lblStaffel3Bereich2MessabweichungWaage.Text = resources.GetString("lblStaffel3Bereich2MessabweichungWaage.Text")
        Me.lblStaffel3Bereich2NormalLast.Text = resources.GetString("lblStaffel3Bereich2NormalLast.Text")
        Me.lblStaffel3Bereich3Anzeige.Text = resources.GetString("lblStaffel3Bereich3Anzeige.Text")
        Me.lblStaffel3Bereich3Differenz.Text = resources.GetString("lblStaffel3Bereich3Differenz.Text")
        Me.lblStaffel3Bereich3EFG.Text = resources.GetString("lblStaffel3Bereich3EFG.Text")
        Me.lblStaffel3Bereich3Fehler.Text = resources.GetString("lblStaffel3Bereich3Fehler.Text")
        Me.lblStaffel3Bereich3Last.Text = resources.GetString("lblStaffel3Bereich3Last.Text")
        Me.lblStaffel3Bereich3MessabweichungStaffel.Text = resources.GetString("lblStaffel3Bereich3MessabweichungStaffel.Text")
        Me.lblStaffel3Bereich3MessabweichungWaage.Text = resources.GetString("lblStaffel3Bereich3MessabweichungWaage.Text")
        Me.lblStaffel3Bereich3NormalLast.Text = resources.GetString("lblStaffel3Bereich3NormalLast.Text")
        Me.lblStaffel4Bereich1Anzeige.Text = resources.GetString("lblStaffel4Bereich1Anzeige.Text")
        Me.lblStaffel4Bereich1Differenz.Text = resources.GetString("lblStaffel4Bereich1Differenz.Text")
        Me.lblStaffel4Bereich1EFG.Text = resources.GetString("lblStaffel4Bereich1EFG.Text")
        Me.lblStaffel4Bereich1Fehler.Text = resources.GetString("lblStaffel4Bereich1Fehler.Text")
        Me.lblStaffel4Bereich1Last.Text = resources.GetString("lblStaffel4Bereich1Last.Text")
        Me.lblStaffel4Bereich1MessabweichungStaffel.Text = resources.GetString("lblStaffel4Bereich1MessabweichungStaffel.Text")
        Me.lblStaffel4Bereich1MessabweichungWaage.Text = resources.GetString("lblStaffel4Bereich1MessabweichungWaage.Text")
        Me.lblStaffel4Bereich1NormalLast.Text = resources.GetString("lblStaffel4Bereich1NormalLast.Text")
        Me.lblStaffel4Bereich2Anzeige.Text = resources.GetString("lblStaffel4Bereich2Anzeige.Text")
        Me.lblStaffel4Bereich2Differenz.Text = resources.GetString("lblStaffel4Bereich2Differenz.Text")
        Me.lblStaffel4Bereich2EFG.Text = resources.GetString("lblStaffel4Bereich2EFG.Text")
        Me.lblStaffel4Bereich2Fehler.Text = resources.GetString("lblStaffel4Bereich2Fehler.Text")
        Me.lblStaffel4Bereich2Last.Text = resources.GetString("lblStaffel4Bereich2Last.Text")
        Me.lblStaffel4Bereich2MessabweichungStaffel.Text = resources.GetString("lblStaffel4Bereich2MessabweichungStaffel.Text")
        Me.lblStaffel4Bereich2MessabweichungWaage.Text = resources.GetString("lblStaffel4Bereich2MessabweichungWaage.Text")
        Me.lblStaffel4Bereich2NormalLast.Text = resources.GetString("lblStaffel4Bereich2NormalLast.Text")
        Me.lblStaffel4Bereich3Anzeige.Text = resources.GetString("lblStaffel4Bereich3Anzeige.Text")
        Me.lblStaffel4Bereich3Differenz.Text = resources.GetString("lblStaffel4Bereich3Differenz.Text")
        Me.lblStaffel4Bereich3EFG.Text = resources.GetString("lblStaffel4Bereich3EFG.Text")
        Me.lblStaffel4Bereich3Fehler.Text = resources.GetString("lblStaffel4Bereich3Fehler.Text")
        Me.lblStaffel4Bereich3Last.Text = resources.GetString("lblStaffel4Bereich3Last.Text")
        Me.lblStaffel4Bereich3MessabweichungStaffel.Text = resources.GetString("lblStaffel4Bereich3MessabweichungStaffel.Text")
        Me.lblStaffel4Bereich3MessabweichungWaage.Text = resources.GetString("lblStaffel4Bereich3MessabweichungWaage.Text")
        Me.lblStaffel4Bereich3NormalLast.Text = resources.GetString("lblStaffel4Bereich3NormalLast.Text")
        Me.lblStaffel5Bereich1Anzeige.Text = resources.GetString("lblStaffel5Bereich1Anzeige.Text")
        Me.lblStaffel5Bereich1Differenz.Text = resources.GetString("lblStaffel5Bereich1Differenz.Text")
        Me.lblStaffel5Bereich1EFG.Text = resources.GetString("lblStaffel5Bereich1EFG.Text")
        Me.lblStaffel5Bereich1Fehler.Text = resources.GetString("lblStaffel5Bereich1Fehler.Text")
        Me.lblStaffel5Bereich1Last.Text = resources.GetString("lblStaffel5Bereich1Last.Text")
        Me.lblStaffel5Bereich1MessabweichungStaffel.Text = resources.GetString("lblStaffel5Bereich1MessabweichungStaffel.Text")
        Me.lblStaffel5Bereich1MessabweichungWaage.Text = resources.GetString("lblStaffel5Bereich1MessabweichungWaage.Text")
        Me.lblStaffel5Bereich1NormalLast.Text = resources.GetString("lblStaffel5Bereich1NormalLast.Text")
        Me.lblStaffel5Bereich2Anzeige.Text = resources.GetString("lblStaffel5Bereich2Anzeige.Text")
        Me.lblStaffel5Bereich2Differenz.Text = resources.GetString("lblStaffel5Bereich2Differenz.Text")
        Me.lblStaffel5Bereich2EFG.Text = resources.GetString("lblStaffel5Bereich2EFG.Text")
        Me.lblStaffel5Bereich2Fehler.Text = resources.GetString("lblStaffel5Bereich2Fehler.Text")
        Me.lblStaffel5Bereich2Last.Text = resources.GetString("lblStaffel5Bereich2Last.Text")
        Me.lblStaffel5Bereich2MessabweichungStaffel.Text = resources.GetString("lblStaffel5Bereich2MessabweichungStaffel.Text")
        Me.lblStaffel5Bereich2MessabweichungWaage.Text = resources.GetString("lblStaffel5Bereich2MessabweichungWaage.Text")
        Me.lblStaffel5Bereich2NormalLast.Text = resources.GetString("lblStaffel5Bereich2NormalLast.Text")
        Me.lblStaffel5Bereich3Anzeige.Text = resources.GetString("lblStaffel5Bereich3Anzeige.Text")
        Me.lblStaffel5Bereich3Differenz.Text = resources.GetString("lblStaffel5Bereich3Differenz.Text")
        Me.lblStaffel5Bereich3EFG.Text = resources.GetString("lblStaffel5Bereich3EFG.Text")
        Me.lblStaffel5Bereich3Fehler.Text = resources.GetString("lblStaffel5Bereich3Fehler.Text")
        Me.lblStaffel5Bereich3Last.Text = resources.GetString("lblStaffel5Bereich3Last.Text")
        Me.lblStaffel5Bereich3MessabweichungStaffel.Text = resources.GetString("lblStaffel5Bereich3MessabweichungStaffel.Text")
        Me.lblStaffel5Bereich3MessabweichungWaage.Text = resources.GetString("lblStaffel5Bereich3MessabweichungWaage.Text")
        Me.lblStaffel5Bereich3NormalLast.Text = resources.GetString("lblStaffel5Bereich3NormalLast.Text")

        Me.RadGroupBoxStaffel1.Text = resources.GetString("RadGroupBoxStaffel1.Text")
        Me.RadGroupBoxStaffel1Bereich1.Text = resources.GetString("RadGroupBoxStaffel1Bereich1.Text")
        Me.RadGroupBoxStaffel1Bereich2.Text = resources.GetString("RadGroupBoxStaffel1Bereich2.Text")
        Me.RadGroupBoxStaffel1Bereich3.Text = resources.GetString("RadGroupBoxStaffel1Bereich3.Text")

        Me.RadGroupBoxStaffel2.Text = resources.GetString("RadGroupBoxStaffel2.Text")
        Me.RadGroupBoxStaffel2Bereich1.Text = resources.GetString("RadGroupBoxStaffel2Bereich1.Text")
        Me.RadGroupBoxStaffel2Bereich2.Text = resources.GetString("RadGroupBoxStaffel2Bereich2.Text")
        Me.RadGroupBoxStaffel2Bereich3.Text = resources.GetString("RadGroupBoxStaffel2Bereich3.Text")

        Me.RadGroupBoxStaffel3.Text = resources.GetString("RadGroupBoxStaffel3.Text")
        Me.RadGroupBoxStaffel3Bereich1.Text = resources.GetString("RadGroupBoxStaffel3Bereich1.Text")
        Me.RadGroupBoxStaffel3Bereich2.Text = resources.GetString("RadGroupBoxStaffel3Bereich2.Text")
        Me.RadGroupBoxStaffel3Bereich3.Text = resources.GetString("RadGroupBoxStaffel3Bereich3.Text")

        Me.RadGroupBoxStaffel4.Text = resources.GetString("RadGroupBoxStaffel4.Text")
        Me.RadGroupBoxStaffel4Bereich1.Text = resources.GetString("RadGroupBoxStaffel4Bereich1.Text")
        Me.RadGroupBoxStaffel4Bereich2.Text = resources.GetString("RadGroupBoxStaffel4Bereich2.Text")
        Me.RadGroupBoxStaffel4Bereich3.Text = resources.GetString("RadGroupBoxStaffel4Bereich3.Text")

        Me.RadGroupBoxStaffel5.Text = resources.GetString("RadGroupBoxStaffel5.Text")
        Me.RadGroupBoxStaffel5Bereich1.Text = resources.GetString("RadGroupBoxStaffel5Bereich1.Text")
        Me.RadGroupBoxStaffel5Bereich2.Text = resources.GetString("RadGroupBoxStaffel5Bereich2.Text")
        Me.RadGroupBoxStaffel5Bereich3.Text = resources.GetString("RadGroupBoxStaffel5Bereich3.Text")

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen

                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
                'Überschrift setzen

                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
            Catch ex As Exception
            End Try
        End If


    End Sub

#Region "Events die neue Berechnungen beim Ändern von Feldinformationen erfordern"


#Region "staffel1"

#Region "Bereich1"
    Private Sub RadTextBoxControlStaffel1Bereich1Last1_TextChanged_1(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel1Bereich1Last4.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige4.TextChanged

        If _suspendEvents = True Then Exit Sub
        CalculateStaffel1Bereich1()
    End Sub

    Private Sub CalculateStaffel1Bereich1()
        Try

            '4. last entspricht der ersten Last (üblicherweise 0)
            RadTextBoxControlStaffel1Bereich1Last4.Text = RadTextBoxControlStaffel1Bereich1Last1.Text


            'fehler berechnen
            Try
                RadTextBoxControlStaffel1Bereich1Fehler1.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige1.Text) - CDec(RadTextBoxControlStaffel1Bereich1Last1.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich1Fehler2.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige2.Text) - CDec(RadTextBoxControlStaffel1Bereich1Last2.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich1Fehler3.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich1Last3.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich1Fehler4.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich1Last4.Text)
            Catch e As Exception
            End Try

            Try
                'EFG Wert 1 und 2 Berechnen

                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich1Last1.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich1EFGWert1.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1) * 0.5, _intNullstellenE)
                    lblStaffel1Bereich1EFGWert2.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1) * 0.5, _intNullstellenE)
                Else
                    lblStaffel1Bereich1EFGWert1.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel1Bereich1EFGWert2.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

            'EFG Wert 5 Berechnen
            lblStaffel2Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
            'berechnen der Differenzen
            Try
                RadTextBoxControlStaffel1Bereich1Fehler5.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich1Anzeige1.Text)
            Catch ex As Exception
            End Try

            Try
                'messabweichung berechnen (abgeändert von Excel mappe. hier wird statt der min. normalien die eingebene Normalien Menge genommen
                RadTextBoxControlStaffel1Bereich1Fehler6.Text = CDec(RadTextBoxControlStaffel1Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich1Anzeige1.Text) - CDec(RadTextBoxControlStaffel1Bereich1Last3.Text)
            Catch ex As Exception
            End Try

            Try
                'messabweichung zur WAage
                RadTextBoxControlStaffel1Bereich1Fehler7.Text = RadTextBoxControlStaffel1Bereich1Fehler6.Text
            Catch e As Exception
            End Try

            Try
                'EFG'Wert 3 Berechnen
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich1EFGWert3.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich1EFGWert3.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

            Try
                'Berechnen von EFG  4 
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich1Last4.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich1EFGWert4.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich1EFGWert4.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                End If
            Catch e As Exception
            End Try
            Try
                'berechnen von EFG 5
                lblStaffel1Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
            Catch e As Exception
            End Try

            Try
                'Berechnen von EFG   6 und 7
                If CDec(RadTextBoxControlStaffel1Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE)
                    lblStaffel1Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich1EFGWert6.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                    lblStaffel1Bereich1EFGWert7.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

        Catch ex As Exception
        End Try

    End Sub


#End Region

#Region "Bereich2"
    Private Sub CalculateStaffel1Bereich2()
        Try

            '4. last entspricht der ersten Last (üblicherweise 0)
            RadTextBoxControlStaffel1Bereich2Last4.Text = RadTextBoxControlStaffel1Bereich2Last1.Text


            'fehler berechnen

            Try
                RadTextBoxControlStaffel1Bereich2Fehler1.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige1.Text) - CDec(RadTextBoxControlStaffel1Bereich2Last1.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich2Fehler2.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige2.Text) - CDec(RadTextBoxControlStaffel1Bereich2Last2.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich2Fehler3.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich2Last3.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich2Fehler4.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich2Last4.Text)
            Catch e As Exception
            End Try

            'EFG Wert 1 und 2 Berechnen

            Try
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich2Last1.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich2EFGWert1.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2) * 0.5, _intNullstellenE)
                    lblStaffel1Bereich2EFGWert2.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2) * 0.5, _intNullstellenE)
                Else
                    lblStaffel1Bereich2EFGWert1.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel1Bereich2EFGWert2.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

            Try
                'berechnen der Differenzen
                RadTextBoxControlStaffel1Bereich2Fehler5.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich2Anzeige1.Text)
            Catch ex As Exception

            End Try


            Try
                'messabweichung berechnen
                RadTextBoxControlStaffel1Bereich2Fehler6.Text = CDec(RadTextBoxControlStaffel1Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich2Anzeige1.Text) - CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) + CDec(objEichprozess.Eichprotokoll.Identifikationsdaten_Min2)
            Catch e As Exception
            End Try
            'messabweichung zur WAage
            Try
                RadTextBoxControlStaffel1Bereich2Fehler7.Text = RadTextBoxControlStaffel1Bereich2Fehler6.Text
            Catch e As Exception
            End Try

            Try
                'EFG'Wert 3 Berechnen
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich2EFGWert3.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich2EFGWert3.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                End If

            Catch e As Exception
            End Try
            Try
                'Berechnen von EFG  4 
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich2Last4.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich2EFGWert4.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich2EFGWert4.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                End If
            Catch e As Exception
            End Try
            Try
                'berechnen von EFG 5
                lblStaffel1Bereich2EFGWert5.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
            Catch e As Exception
            End Try

            Try
                'Berechnen von EFG   6 und 7
                If CDec(RadTextBoxControlStaffel1Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE)
                    lblStaffel1Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                    lblStaffel1Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

        Catch ex As Exception
        End Try

    End Sub


    Private Sub RadTextBoxControlStaffel1Bereich2Last1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel1Bereich2Last4.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige4.TextChanged
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel1Bereich2()
    End Sub


#End Region

#Region "Bereich3"
    Private Sub CalculateStaffel1Bereich3()
        Try

            '4. last entspricht der ersten Last (üblicherweise 0)
            RadTextBoxControlStaffel1Bereich3Last4.Text = RadTextBoxControlStaffel1Bereich3Last1.Text


            'fehler berechnen

            Try
                RadTextBoxControlStaffel1Bereich3Fehler1.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige1.Text) - CDec(RadTextBoxControlStaffel1Bereich3Last1.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich3Fehler2.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige2.Text) - CDec(RadTextBoxControlStaffel1Bereich3Last2.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich3Fehler3.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich3Last3.Text)
            Catch e As Exception
            End Try
            Try
                RadTextBoxControlStaffel1Bereich3Fehler4.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich3Last4.Text)
            Catch e As Exception
            End Try

            'EFG Wert 1 und 2 Berechnen

            Try
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich3Last1.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich3EFGWert1.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3) * 0.5, _intNullstellenE)
                    lblStaffel1Bereich3EFGWert2.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3) * 0.5, _intNullstellenE)
                Else
                    lblStaffel1Bereich3EFGWert1.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel1Bereich3EFGWert2.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

            Try
                'berechnen der Differenzen
                RadTextBoxControlStaffel1Bereich3Fehler5.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel1Bereich3Anzeige1.Text)
            Catch ex As Exception

            End Try


            Try
                'messabweichung berechnen
                RadTextBoxControlStaffel1Bereich3Fehler6.Text = CDec(RadTextBoxControlStaffel1Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel1Bereich3Anzeige1.Text) - CDec(objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast) + CDec(objEichprozess.Eichprotokoll.Identifikationsdaten_Min2)
            Catch e As Exception
            End Try
            'messabweichung zur WAage
            Try
                RadTextBoxControlStaffel1Bereich3Fehler7.Text = RadTextBoxControlStaffel1Bereich3Fehler6.Text
            Catch e As Exception
            End Try

            Try
                'EFG'Wert 3 Berechnen
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich3EFGWert3.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich3EFGWert3.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                End If

            Catch e As Exception
            End Try
            Try
                'Berechnen von EFG  4 
                '=WENN(B130<$B$49;WERT(0,5*$B$15);(1*$B$15))
                If CDec(RadTextBoxControlStaffel1Bereich3Last4.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich3EFGWert4.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich3EFGWert4.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                End If
            Catch e As Exception
            End Try
            Try
                'berechnen von EFG 5
                lblStaffel1Bereich3EFGWert5.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
            Catch e As Exception
            End Try

            Try
                'Berechnen von EFG   6 und 7
                If CDec(RadTextBoxControlStaffel1Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                    lblStaffel1Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE)
                    lblStaffel1Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE)
                Else
                    lblStaffel1Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                    lblStaffel1Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                End If
            Catch e As Exception
            End Try

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RadTextBoxControlStaffel1Bereich3Last1_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel1Bereich3Last4.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige4.TextChanged
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel1Bereich3()
    End Sub

#End Region
#End Region

#Region "Staffel2"

#Region "Bereich1"
    Private Sub CalculateStaffel2Bereich1()


        'lasten
        Try
            RadTextBoxControlStaffel2Bereich1Last3.Text = CDec(RadTextBoxControlStaffel2Bereich1Last2.Text) + CDec(RadTextBoxControlStaffel2Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel2Bereich1Last4.Text = RadTextBoxControlStaffel2Bereich1Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel2Bereich1Last3.Text = "0" Then
                RadTextBoxControlStaffel2Bereich1Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel2Bereich1Fehler6.Text = (CDec(RadTextBoxControlStaffel2Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel2Bereich1Anzeige1.Text)) - CDec(RadTextBoxControlStaffel2Bereich1Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel2Bereich1Fehler5.Text = CDec(RadTextBoxControlStaffel2Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel2Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try


        Try
            'berechnen von EFG 5
            lblStaffel2Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try

            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel2Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel2Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                lblStaffel2Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
            Else
                lblStaffel2Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                lblStaffel2Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1_TextChanged(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel2Bereich1()
    End Sub
#End Region

#Region "Bereich2"
    Private Sub CalculateStaffel2Bereich2()



        'lasten
        Try
            RadTextBoxControlStaffel2Bereich2Last3.Text = CDec(RadTextBoxControlStaffel2Bereich2Last2.Text) + CDec(RadTextBoxControlStaffel2Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel2Bereich2Last4.Text = RadTextBoxControlStaffel2Bereich2Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel2Bereich2Last3.Text = "0" Then
                RadTextBoxControlStaffel2Bereich2Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel2Bereich2Fehler6.Text = (CDec(RadTextBoxControlStaffel2Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel2Bereich2Anzeige1.Text)) - CDec(RadTextBoxControlStaffel2Bereich2Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel2Bereich2Fehler5.Text = CDec(RadTextBoxControlStaffel2Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel2Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try

        Try
            'berechnen von EFG 5
            lblStaffel2Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
        Catch e As Exception
        End Try

        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel2Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel2Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                lblStaffel2Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
            Else
                lblStaffel2Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                lblStaffel2Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich2_TextChanged_1(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel2Bereich2()
    End Sub
#End Region

#Region "Bereich3"
    Private Sub CalculateStaffel2Bereich3()



        'lasten
        Try
            RadTextBoxControlStaffel2Bereich3Last3.Text = CDec(RadTextBoxControlStaffel2Bereich3Last2.Text) + CDec(RadTextBoxControlStaffel2Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel2Bereich3Last4.Text = RadTextBoxControlStaffel2Bereich3Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel2Bereich3Last3.Text = "0" Then
                RadTextBoxControlStaffel2Bereich3Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel2Bereich3Fehler6.Text = (CDec(RadTextBoxControlStaffel2Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel2Bereich3Anzeige1.Text)) - CDec(RadTextBoxControlStaffel2Bereich3Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel2Bereich3Fehler5.Text = CDec(RadTextBoxControlStaffel2Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel2Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            'berechnen von EFG 5
            lblStaffel2Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel2Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel2Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                lblStaffel2Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
            Else
                lblStaffel2Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                lblStaffel2Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich3_TextChanged_1(sender As Object, e As EventArgs)

        If _suspendEvents = True Then Exit Sub
        CalculateStaffel2Bereich3()
    End Sub

#End Region

#End Region

#Region "Staffel3"

#Region "Bereich1"
    Private Sub CalculateStaffel3Bereich1()


        'lasten
        Try
            RadTextBoxControlStaffel3Bereich1Last3.Text = CDec(RadTextBoxControlStaffel3Bereich1Last2.Text) + CDec(RadTextBoxControlStaffel3Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel3Bereich1Last4.Text = RadTextBoxControlStaffel3Bereich1Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel3Bereich1Last3.Text = "0" Then
                RadTextBoxControlStaffel3Bereich1Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel3Bereich1Fehler6.Text = (CDec(RadTextBoxControlStaffel3Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel3Bereich1Anzeige1.Text)) - CDec(RadTextBoxControlStaffel3Bereich1Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel3Bereich1Fehler5.Text = CDec(RadTextBoxControlStaffel3Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel3Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try


        Try
            'berechnen von EFG 5
            lblStaffel3Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try

            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel3Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel3Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                lblStaffel3Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
            Else
                lblStaffel3Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                lblStaffel3Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel3Bereich1_TextChanged(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel3Bereich1()
    End Sub
#End Region

#Region "Bereich2"
    Private Sub CalculateStaffel3Bereich2()



        'lasten
        Try
            RadTextBoxControlStaffel3Bereich2Last3.Text = CDec(RadTextBoxControlStaffel3Bereich2Last2.Text) + CDec(RadTextBoxControlStaffel3Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel3Bereich2Last4.Text = RadTextBoxControlStaffel3Bereich2Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel3Bereich2Last3.Text = "0" Then
                RadTextBoxControlStaffel3Bereich2Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel3Bereich2Fehler6.Text = (CDec(RadTextBoxControlStaffel3Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel3Bereich2Anzeige1.Text)) - CDec(RadTextBoxControlStaffel3Bereich2Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel3Bereich2Fehler5.Text = CDec(RadTextBoxControlStaffel3Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel3Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try

        Try
            'berechnen von EFG 5
            lblStaffel3Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
        Catch e As Exception
        End Try

        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel3Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel3Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                lblStaffel3Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
            Else
                lblStaffel3Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                lblStaffel3Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel3Bereich2_TextChanged_1(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel3Bereich2()
    End Sub
#End Region

#Region "Bereich3"
    Private Sub CalculateStaffel3Bereich3()



        'lasten
        Try
            RadTextBoxControlStaffel3Bereich3Last3.Text = CDec(RadTextBoxControlStaffel3Bereich3Last2.Text) + CDec(RadTextBoxControlStaffel3Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel3Bereich3Last4.Text = RadTextBoxControlStaffel3Bereich3Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel3Bereich3Last3.Text = "0" Then
                RadTextBoxControlStaffel3Bereich3Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel3Bereich3Fehler6.Text = (CDec(RadTextBoxControlStaffel3Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel3Bereich3Anzeige1.Text)) - CDec(RadTextBoxControlStaffel3Bereich3Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel3Bereich3Fehler5.Text = CDec(RadTextBoxControlStaffel3Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel3Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            'berechnen von EFG 5
            lblStaffel3Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel3Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel3Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                lblStaffel3Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
            Else
                lblStaffel3Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                lblStaffel3Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel3Bereich3_TextChanged_1(sender As Object, e As EventArgs)

        If _suspendEvents = True Then Exit Sub
        CalculateStaffel3Bereich3()
    End Sub

#End Region

#End Region


#Region "Staffel4"

#Region "Bereich1"
    Private Sub CalculateStaffel4Bereich1()


        'lasten
        Try
            RadTextBoxControlStaffel4Bereich1Last3.Text = CDec(RadTextBoxControlStaffel4Bereich1Last2.Text) + CDec(RadTextBoxControlStaffel4Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel4Bereich1Last4.Text = RadTextBoxControlStaffel4Bereich1Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel4Bereich1Last3.Text = "0" Then
                RadTextBoxControlStaffel4Bereich1Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel4Bereich1Fehler6.Text = (CDec(RadTextBoxControlStaffel4Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel4Bereich1Anzeige1.Text)) - CDec(RadTextBoxControlStaffel4Bereich1Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel4Bereich1Fehler5.Text = CDec(RadTextBoxControlStaffel4Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel4Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try


        Try
            'berechnen von EFG 5
            lblStaffel4Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try

            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel4Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel4Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                lblStaffel4Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
            Else
                lblStaffel4Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                lblStaffel4Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel4Bereich1_TextChanged(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel4Bereich1()
    End Sub
#End Region

#Region "Bereich2"
    Private Sub CalculateStaffel4Bereich2()



        'lasten
        Try
            RadTextBoxControlStaffel4Bereich2Last3.Text = CDec(RadTextBoxControlStaffel4Bereich2Last2.Text) + CDec(RadTextBoxControlStaffel4Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel4Bereich2Last4.Text = RadTextBoxControlStaffel4Bereich2Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel4Bereich2Last3.Text = "0" Then
                RadTextBoxControlStaffel4Bereich2Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel4Bereich2Fehler6.Text = (CDec(RadTextBoxControlStaffel4Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel4Bereich2Anzeige1.Text)) - CDec(RadTextBoxControlStaffel4Bereich2Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel4Bereich2Fehler5.Text = CDec(RadTextBoxControlStaffel4Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel4Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try

        Try
            'berechnen von EFG 5
            lblStaffel4Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
        Catch e As Exception
        End Try

        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel4Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel4Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                lblStaffel4Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
            Else
                lblStaffel4Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                lblStaffel4Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel4Bereich2_TextChanged_1(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel4Bereich2()
    End Sub
#End Region

#Region "Bereich3"
    Private Sub CalculateStaffel4Bereich3()



        'lasten
        Try
            RadTextBoxControlStaffel4Bereich3Last3.Text = CDec(RadTextBoxControlStaffel4Bereich3Last2.Text) + CDec(RadTextBoxControlStaffel4Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel4Bereich3Last4.Text = RadTextBoxControlStaffel4Bereich3Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel4Bereich3Last3.Text = "0" Then
                RadTextBoxControlStaffel4Bereich3Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel4Bereich3Fehler6.Text = (CDec(RadTextBoxControlStaffel4Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel4Bereich3Anzeige1.Text)) - CDec(RadTextBoxControlStaffel4Bereich3Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel4Bereich3Fehler5.Text = CDec(RadTextBoxControlStaffel4Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel4Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            'berechnen von EFG 5
            lblStaffel4Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel4Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel4Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                lblStaffel4Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
            Else
                lblStaffel4Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                lblStaffel4Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel4Bereich3_TextChanged_1(sender As Object, e As EventArgs)

        If _suspendEvents = True Then Exit Sub
        CalculateStaffel4Bereich3()
    End Sub

#End Region

#End Region



#Region "Staffel5"

#Region "Bereich1"
    Private Sub CalculateStaffel5Bereich1()


        'lasten
        Try
            RadTextBoxControlStaffel5Bereich1Last3.Text = CDec(RadTextBoxControlStaffel5Bereich1Last2.Text) + CDec(RadTextBoxControlStaffel5Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel5Bereich1Last4.Text = RadTextBoxControlStaffel5Bereich1Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel5Bereich1Last3.Text = "0" Then
                RadTextBoxControlStaffel5Bereich1Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel5Bereich1Fehler6.Text = (CDec(RadTextBoxControlStaffel5Bereich1Anzeige3.Text) - CDec(RadTextBoxControlStaffel5Bereich1Anzeige1.Text)) - CDec(RadTextBoxControlStaffel5Bereich1Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel5Bereich1Fehler5.Text = CDec(RadTextBoxControlStaffel5Bereich1Anzeige4.Text) - CDec(RadTextBoxControlStaffel5Bereich1Anzeige1.Text)
        Catch e As Exception
        End Try


        Try
            'berechnen von EFG 5
            lblStaffel5Bereich1EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try

            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel5Bereich1Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel5Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
                lblStaffel5Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE)
            Else
                lblStaffel5Bereich1EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
                lblStaffel5Bereich1EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel5Bereich1_TextChanged(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel5Bereich1()
    End Sub
#End Region

#Region "Bereich2"
    Private Sub CalculateStaffel5Bereich2()



        'lasten
        Try
            RadTextBoxControlStaffel5Bereich2Last3.Text = CDec(RadTextBoxControlStaffel5Bereich2Last2.Text) + CDec(RadTextBoxControlStaffel5Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel5Bereich2Last4.Text = RadTextBoxControlStaffel5Bereich2Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel5Bereich2Last3.Text = "0" Then
                RadTextBoxControlStaffel5Bereich2Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel5Bereich2Fehler6.Text = (CDec(RadTextBoxControlStaffel5Bereich2Anzeige3.Text) - CDec(RadTextBoxControlStaffel5Bereich2Anzeige1.Text)) - CDec(RadTextBoxControlStaffel5Bereich2Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel5Bereich2Fehler5.Text = CDec(RadTextBoxControlStaffel5Bereich2Anzeige4.Text) - CDec(RadTextBoxControlStaffel5Bereich2Anzeige1.Text)
        Catch e As Exception
        End Try

        Try
            'berechnen von EFG 5
            lblStaffel5Bereich2EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) / 5
        Catch e As Exception
        End Try

        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel5Bereich2Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel5Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
                lblStaffel5Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE)
            Else
                lblStaffel5Bereich2EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
                lblStaffel5Bereich2EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel5Bereich2_TextChanged_1(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        CalculateStaffel5Bereich2()
    End Sub
#End Region

#Region "Bereich3"
    Private Sub CalculateStaffel5Bereich3()



        'lasten
        Try
            RadTextBoxControlStaffel5Bereich3Last3.Text = CDec(RadTextBoxControlStaffel5Bereich3Last2.Text) + CDec(RadTextBoxControlStaffel5Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            RadTextBoxControlStaffel5Bereich3Last4.Text = RadTextBoxControlStaffel5Bereich3Anzeige1.Text
        Catch e As Exception
        End Try

        Try
            'Differenz Staffel
            If RadTextBoxControlStaffel5Bereich3Last3.Text = "0" Then
                RadTextBoxControlStaffel5Bereich3Fehler6.Text = "0"
            Else
                RadTextBoxControlStaffel5Bereich3Fehler6.Text = (CDec(RadTextBoxControlStaffel5Bereich3Anzeige3.Text) - CDec(RadTextBoxControlStaffel5Bereich3Anzeige1.Text)) - CDec(RadTextBoxControlStaffel5Bereich3Last2.Text)
            End If
        Catch e As Exception
        End Try

        Try
            'Differenze Normallast
            RadTextBoxControlStaffel5Bereich3Fehler5.Text = CDec(RadTextBoxControlStaffel5Bereich3Anzeige4.Text) - CDec(RadTextBoxControlStaffel5Bereich3Anzeige1.Text)
        Catch e As Exception
        End Try
        Try
            'berechnen von EFG 5
            lblStaffel5Bereich3EFGWert5.text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) / 5
        Catch e As Exception
        End Try
        Try
            'Berechne EFG Wert 6 und 7
            If CDec(RadTextBoxControlStaffel5Bereich3Last3.Text) < Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero) Then
                lblStaffel5Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
                lblStaffel5Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE)
            Else
                lblStaffel5Bereich3EFGWert6.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
                lblStaffel5Bereich3EFGWert7.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3), _intNullstellenE) * 1.5
            End If
        Catch e As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel5Bereich3_TextChanged_1(sender As Object, e As EventArgs)

        If _suspendEvents = True Then Exit Sub
        CalculateStaffel5Bereich3()
    End Sub

#End Region

#End Region



#End Region


#Region " Messabweichung der Waage [SEwi]"
    Private Sub RadTextBoxControlStaffel1Bereich1Fehler5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich1Fehler6.TextChanged, _
        RadTextBoxControlStaffel4Bereich1Fehler6.TextChanged, RadTextBoxControlStaffel3Bereich1Fehler6.TextChanged, RadTextBoxControlStaffel2Bereich1Fehler6.TextChanged, RadTextBoxControlStaffel1Bereich1Fehler6.TextChanged
        Try
            Try
                'differenz der Waage Berechnen
                RadTextBoxControlStaffel1Bereich1Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich1Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel1Bereich1Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel2Bereich1Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich1Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel2Bereich1Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel3Bereich1Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich1Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel3Bereich1Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel4Bereich1Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich1Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel4Bereich1Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel5Bereich1Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich1Fehler6.Text) + CDec(RadTextBoxControlStaffel5Bereich1Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel5Bereich1Fehler7.Text = ""
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel1bereich2Fehler5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich2Fehler6.TextChanged, RadTextBoxControlStaffel4Bereich2Fehler6.TextChanged, RadTextBoxControlStaffel3Bereich2Fehler6.TextChanged, RadTextBoxControlStaffel2Bereich2Fehler6.TextChanged, RadTextBoxControlStaffel1Bereich2Fehler6.TextChanged
        Try
            Try
                'differenz der Waage Berechnen
                RadTextBoxControlStaffel1Bereich2Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich2Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel1Bereich2Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel2Bereich2Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich2Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel2Bereich2Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel3Bereich2Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich2Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel3Bereich2Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel4Bereich2Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich2Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel4Bereich2Fehler7.Text = ""
            End Try
            Try
                RadTextBoxControlStaffel5Bereich2Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich2Fehler6.Text) + CDec(RadTextBoxControlStaffel5Bereich2Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel5Bereich2Fehler7.Text = ""
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadTextBoxControlStaffel1bereich3Fehler5_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Fehler6.TextChanged, RadTextBoxControlStaffel4Bereich3Fehler6.TextChanged, RadTextBoxControlStaffel3Bereich3Fehler6.TextChanged, RadTextBoxControlStaffel2Bereich3Fehler6.TextChanged, RadTextBoxControlStaffel1Bereich3Fehler6.TextChanged
        Try
            Try
                'differenz der Waage Berechnen
                RadTextBoxControlStaffel1Bereich3Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich3Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel1Bereich3Fehler7.Text = ""

            End Try
            Try
                RadTextBoxControlStaffel2Bereich3Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich3Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel2Bereich3Fehler7.Text = ""

            End Try
            Try
                RadTextBoxControlStaffel3Bereich3Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich3Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel3Bereich3Fehler7.Text = ""

            End Try
            Try
                RadTextBoxControlStaffel4Bereich3Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich3Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel4Bereich3Fehler7.Text = ""

            End Try
            Try
                RadTextBoxControlStaffel5Bereich3Fehler7.Text = CDec(RadTextBoxControlStaffel1Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel2Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel3Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel4Bereich3Fehler6.Text) + CDec(RadTextBoxControlStaffel5Bereich3Fehler6.Text)
            Catch ex As Exception
                RadTextBoxControlStaffel5Bereich3Fehler7.Text = ""

            End Try
        Catch ex As Exception
        End Try
    End Sub

#End Region


#Region "Hilfetexte"
    Private Sub RadTextBoxControlStaffel1Bereich1Last1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel1Bereich3Last4.MouseEnter, RadTextBoxControlStaffel1Bereich3Last3.MouseEnter, RadTextBoxControlStaffel1Bereich3Last1.MouseEnter, RadTextBoxControlStaffel1Bereich2Last4.MouseEnter, RadTextBoxControlStaffel1Bereich2Last3.MouseEnter, RadTextBoxControlStaffel1Bereich2Last1.MouseEnter, RadTextBoxControlStaffel1Bereich1Last4.MouseEnter, RadTextBoxControlStaffel1Bereich1Last3.MouseEnter, RadTextBoxControlStaffel1Bereich1Last1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenNormallast)
    End Sub

    Private Sub RadGroupBoxStaffel1Bereich1_MouseEnter(sender As Object, e As EventArgs) Handles RadGroupBoxStaffel5Bereich3.MouseEnter, RadGroupBoxStaffel5Bereich2.MouseEnter, RadGroupBoxStaffel5Bereich1.MouseEnter, RadGroupBoxStaffel3Bereich3.MouseEnter, RadGroupBoxStaffel3Bereich2.MouseEnter, RadGroupBoxStaffel3Bereich1.MouseEnter, RadGroupBoxStaffel2Bereich3.MouseEnter, RadGroupBoxStaffel2Bereich2.MouseEnter, RadGroupBoxStaffel2Bereich1.MouseEnter, RadGroupBoxStaffel1Bereich3.MouseEnter, RadGroupBoxStaffel1Bereich2.MouseEnter, RadGroupBoxStaffel1Bereich1.MouseEnter
        'zurücksetzen der Hilfetexte
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahren)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Last1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Last1.MouseEnter, RadTextBoxControlStaffel5Bereich2Last1.MouseEnter, RadTextBoxControlStaffel5Bereich1Last1.MouseEnter, RadTextBoxControlStaffel4Bereich3Last1.MouseEnter, RadTextBoxControlStaffel4Bereich2Last1.MouseEnter, RadTextBoxControlStaffel4Bereich1Last1.MouseEnter, RadTextBoxControlStaffel3Bereich3Last1.MouseEnter, RadTextBoxControlStaffel3Bereich2Last1.MouseEnter, RadTextBoxControlStaffel3Bereich1Last1.MouseEnter, RadTextBoxControlStaffel2Bereich3Last1.MouseEnter, RadTextBoxControlStaffel2Bereich2Last1.MouseEnter, RadTextBoxControlStaffel2Bereich1Last1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastSoll)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Anzeige1_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige1.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige1.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastIst)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Last2_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Last2.MouseEnter, RadTextBoxControlStaffel5Bereich2Last2.MouseEnter, RadTextBoxControlStaffel5Bereich1Last2.MouseEnter, RadTextBoxControlStaffel4Bereich3Last2.MouseEnter, RadTextBoxControlStaffel4Bereich2Last2.MouseEnter, RadTextBoxControlStaffel4Bereich1Last2.MouseEnter, RadTextBoxControlStaffel3Bereich3Last2.MouseEnter, RadTextBoxControlStaffel3Bereich2Last2.MouseEnter, RadTextBoxControlStaffel3Bereich1Last2.MouseEnter, RadTextBoxControlStaffel2Bereich3Last2.MouseEnter, RadTextBoxControlStaffel2Bereich2Last2.MouseEnter, RadTextBoxControlStaffel2Bereich1Last2.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastNormallast)
    End Sub

    Private Sub RadTextBoxControlStaffel5Bereich3Anzeige3_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige3.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige3.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastErsatzPlusNormallast)
    End Sub

    Private Sub RadTextBoxControlStaffel2Bereich1Anzeige4_MouseEnter(sender As Object, e As EventArgs) Handles RadTextBoxControlStaffel5Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel5Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel5Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel4Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel3Bereich1Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich3Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich2Anzeige4.MouseEnter, RadTextBoxControlStaffel2Bereich1Anzeige4.MouseEnter
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungStaffelverfahrenErsatzlastErsatzlast2)
    End Sub
#End Region





    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
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

    ''' <summary>
    ''' Dirty Flag für Speicherroutine
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub RadTextBoxControlStaffel1Bereich1Last1_TextChanged(sender As System.Object, e As System.EventArgs) Handles RadTextBoxControlStaffel5Bereich3Last2.TextChanged, RadTextBoxControlStaffel5Bereich3Last1.TextChanged, RadTextBoxControlStaffel5Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel5Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel5Bereich2Last2.TextChanged, RadTextBoxControlStaffel5Bereich2Last1.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel5Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel5Bereich1Last2.TextChanged, RadTextBoxControlStaffel5Bereich1Last1.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel5Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich3Last2.TextChanged, RadTextBoxControlStaffel4Bereich3Last1.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich2Last2.TextChanged, RadTextBoxControlStaffel4Bereich2Last1.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel4Bereich1Last2.TextChanged, RadTextBoxControlStaffel4Bereich1Last1.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel4Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich3Last2.TextChanged, RadTextBoxControlStaffel3Bereich3Last1.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich2Last2.TextChanged, RadTextBoxControlStaffel3Bereich2Last1.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel3Bereich1Last2.TextChanged, RadTextBoxControlStaffel3Bereich1Last1.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel3Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich3Last2.TextChanged, RadTextBoxControlStaffel2Bereich3Last1.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich2Last2.TextChanged, RadTextBoxControlStaffel2Bereich2Last1.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel2Bereich1Last2.TextChanged, RadTextBoxControlStaffel2Bereich1Last1.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige4.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel2Bereich1Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich3Last3.TextChanged, RadTextBoxControlStaffel1Bereich3Last2.TextChanged, RadTextBoxControlStaffel1Bereich3Last1.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige2.TextChanged, RadTextBoxControlStaffel1Bereich3Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich2Last3.TextChanged, RadTextBoxControlStaffel1Bereich2Last2.TextChanged, RadTextBoxControlStaffel1Bereich2Last1.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige2.TextChanged, RadTextBoxControlStaffel1Bereich2Anzeige1.TextChanged, RadTextBoxControlStaffel1Bereich1Last3.TextChanged, RadTextBoxControlStaffel1Bereich1Last2.TextChanged, RadTextBoxControlStaffel1Bereich1Last1.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige3.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige2.TextChanged, RadTextBoxControlStaffel1Bereich1Anzeige1.TextChanged
        'nur wenn bereits aus der DB geladen wurde
        If _suspendEvents Then Exit Sub
        AktuellerStatusDirty = True
    End Sub
End Class
