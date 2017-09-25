Public Class uco19EichtechnischeSicherung

    Inherits ucoContent
    Implements IRhewaEditingDialog
#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _objEichprotokoll As Eichprotokoll

    'Public objEichmarkenComparer As EichmarkenComparable
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        _suspendEvents = True

        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
    End Sub
#End Region

#Region "Events"

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung

        'daten füllen
        LoadFromDatabase()
    End Sub




#Region "Checkboxen EVents mit Textboxen"

    Private Sub RadCheckBoxEichsiegel13x13_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxSicherungsmarkeKlein.ToggleStateChanged
        RadTextBoxControlSicherungsmarkeKlein.ReadOnly = Not RadCheckBoxSicherungsmarkeKlein.Checked
        PictureBoxSicherungsmarkeKlein.Visible = Not RadCheckBoxSicherungsmarkeKlein.Checked

        If RadCheckBoxSicherungsmarkeKlein.Checked = False Then
            RadTextBoxControlSicherungsmarkeKlein.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxEichsiegelRund_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxSicherungsmarkeGross.ToggleStateChanged
        RadTextBoxControlSicherungsmarkeGross.ReadOnly = Not RadCheckBoxSicherungsmarkeGross.Checked
        PictureBoxSicherungsmarkeGross.Visible = Not RadCheckBoxSicherungsmarkeGross.Checked

        If RadCheckBoxSicherungsmarkeGross.Checked = False Then
            RadTextBoxControlSicherungsmarkeGross.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxHinweismarke_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxHinweismarke.ToggleStateChanged
        RadTextBoxControlHinweismarke.ReadOnly = Not RadCheckBoxHinweismarke.Checked
        PictureBoxHinweismarke.Visible = Not RadCheckBoxHinweismarke.Checked

        If RadCheckBoxHinweismarke.Checked = False Then
            RadTextBoxControlHinweismarke.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxBenannteStelle_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxBenannteStelle.ToggleStateChanged
        RadTextBoxControlBenannteStelle.ReadOnly = Not RadCheckBoxBenannteStelle.Checked
        PictureBoxBenannteStelle.Visible = Not RadCheckBoxBenannteStelle.Checked

        If RadCheckBoxBenannteStelle.Checked = False Then
            RadTextBoxControlBenannteStelle.Text = ""
        End If
    End Sub

#End Region

    Private Sub RadTextBoxControlBenannteStelle_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlHinweismarke.Validating, RadTextBoxControlSicherungsmarkeKlein.Validating, RadTextBoxControlSicherungsmarkeGross.Validating, RadTextBoxControlBenannteStelle.Validating
        Dim result As Decimal
        If Not sender.readonly = True Then

            If sender.name.Equals("RadTextBoxControlBenannteStelle") Then
                If sender.text <> "1" Then

                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                    System.Media.SystemSounds.Exclamation.Play()
                    e.Cancel = True
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.EinsEintragen)
                End If
            End If

            'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
            If CType(sender, Telerik.WinControls.UI.RadTextBox).Text.Equals("") Then
                CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
                Exit Sub
            End If

            'versuchen ob der Text in eine Zahl konvertiert werden kann
            If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBox).Text, result) Then
                e.Cancel = True
                CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                System.Media.SystemSounds.Exclamation.Play()

            Else 'rahmen zurücksetzen
                CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
            End If
        End If
    End Sub

    Private Sub RadCheckBoxAufbewahrungsdauer_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxAufbewahrungsdauer.ToggleStateChanged
        RadTextBoxControlAufbewahrungsdauer.ReadOnly = Not RadCheckBoxAufbewahrungsdauer.Checked
        PictureBoxAilbi.Visible = Not RadCheckBoxAufbewahrungsdauer.Checked

        If RadCheckBoxAufbewahrungsdauer.Checked = False Then
            RadTextBoxControlAufbewahrungsdauer.Text = ""
        End If
    End Sub


    Private Sub RadTextBoxControlBenannteStelle_TextChanged(sender As System.Object, e As System.EventArgs) Handles RadTextBoxControlHinweismarke.TextChanged, RadTextBoxControlSicherungsmarkeGross.TextChanged, RadTextBoxControlSicherungsmarkeKlein.TextChanged, RadTextBoxControlBemerkungen.TextChanged, RadTextBoxControlAufbewahrungsdauer.TextChanged, RadTextBoxControlBenannteStelle.TextChanged, RadTextBoxControlSicherungsmarkeKlein.TextChanged, RadTextBoxControlSicherungsmarkeGross.TextChanged, RadTextBoxControlBenannteStelle.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    Private Sub RadCheckBoxBenannteStelle_Click(sender As System.Object, e As System.EventArgs) Handles RadCheckBoxKonfigurationsProgramm.Click, RadCheckBoxHinweismarke.Click, RadCheckBoxSicherungsmarkeGross.Click, RadCheckBoxSicherungsmarkeKlein.Click, RadCheckBoxAufbewahrungsdauer.Click, RadCheckBoxAlibispeicher.Click, RadCheckBoxBenannteStelle.Click, RadCheckBoxSicherungsmarkeKlein.Click, RadCheckBoxSicherungsmarkeGross.Click, RadCheckBoxBenannteStelle.Click
        If _suspendEvents = True Then Exit Sub
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
        Else
            _objEichprotokoll = objEichprozess.Eichprotokoll

        End If

        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(Me.RadScrollablePanel1.PanelContainer)
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
        'TODO Falls ein neues Gerät auf den Markt kommt, muss ein neues Bild in die Resourcen geladen werden und hier verknüpft werden, damit das korrekte angezeigt wird
        'pictureboxen anzeigen
        Try
            If objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82alpha") Then
                PictureBox1.Image = My.Resources._82Alpha_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82Alpha_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82basic") Then
                PictureBox1.Image = My.Resources._82Basic_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82Basic_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82b plus") Then
                PictureBox1.Image = My.Resources._82b_plus_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82b_plus_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82c plus") Then
                PictureBox1.Image = My.Resources._82c_plus_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82c_plus_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82comfort") Then
                PictureBox1.Image = My.Resources._82comfort_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82comfort_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82e plus") Then
                PictureBox1.Image = My.Resources._82e_plus_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82e_plus_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("82edition") Then
                PictureBox1.Image = My.Resources._82edition_front
                PictureBox2.Image = My.Resources._82_Seite
                PictureBox3.Image = My.Resources._82edition_typenschild
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("83sigma") Then
                PictureBox1.Image = My.Resources._83sigma_front
                PictureBox2.Image = My.Resources._83sigma_typenschild
                PictureBox3.Image = Nothing
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("83z") Then
                PictureBox1.Image = My.Resources._83Z_front
                PictureBox2.Image = My.Resources._83Z_typenschild
                PictureBox3.Image = Nothing
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("84") Or objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("84evo") Then
                PictureBox1.Image = My.Resources._84_front
                PictureBox2.Image = My.Resources._84_typenschild
                PictureBox3.Image = Nothing
            ElseIf objEichprozess.Lookup_Auswertegeraet.Typ.ToLower.Equals("84vario") Then
                PictureBox1.Image = My.Resources._84Vario_front
                PictureBox2.Image = My.Resources._84Vario_typenschild
                PictureBox3.Image = Nothing
            Else
                PictureBox1.Image = PictureBox1.ErrorImage
                PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                PictureBox2.Image = PictureBox2.ErrorImage
                PictureBox2.SizeMode = PictureBoxSizeMode.CenterImage
                PictureBox3.Image = PictureBox3.ErrorImage
                PictureBox3.SizeMode = PictureBoxSizeMode.CenterImage
            End If
        Catch ex As Exception
            PictureBox1.Image = PictureBox1.ErrorImage
            PictureBox2.Image = PictureBox2.ErrorImage
            PictureBox3.Image = PictureBox3.ErrorImage
        End Try





        'checkboxen

        If Not objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKlein Is Nothing Then
            RadCheckBoxSicherungsmarkeKlein.Checked = objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKlein
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGross Is Nothing Then
            RadCheckBoxSicherungsmarkeGross.Checked = objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGross
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_Hinweismarke Is Nothing Then
            RadCheckBoxHinweismarke.Checked = objEichprozess.Eichprotokoll.Sicherung_Hinweismarke
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_BenannteStelle Is Nothing Then
            RadCheckBoxBenannteStelle.Checked = objEichprozess.Eichprotokoll.Sicherung_BenannteStelle
        End If




        'anzahl

        If Not objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl Is Nothing Then
            RadTextBoxControlSicherungsmarkeKlein.Text = objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl Is Nothing Then
            RadTextBoxControlSicherungsmarkeGross.Text = objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_HinweismarkeAnzahl Is Nothing Then
            RadTextBoxControlHinweismarke.Text = objEichprozess.Eichprotokoll.Sicherung_HinweismarkeAnzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl Is Nothing Then
            RadTextBoxControlBenannteStelle.Text = objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl
        End If



        'alibi
        If Not objEichprozess.Eichprotokoll.Sicherung_DatenAusgelesen Is Nothing Then
            RadCheckBoxKonfigurationsProgramm.Checked = objEichprozess.Eichprotokoll.Sicherung_DatenAusgelesen
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_AlibispeicherEingerichtet Is Nothing Then
            RadCheckBoxAlibispeicher.Checked = objEichprozess.Eichprotokoll.Sicherung_AlibispeicherEingerichtet
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert Is Nothing Then
            RadCheckBoxAufbewahrungsdauer.Checked = objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung Is Nothing Then
            RadTextBoxControlAufbewahrungsdauer.Text = objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_Bemerkungen Is Nothing Then
            RadTextBoxControlBemerkungen.Text = objEichprozess.Eichprotokoll.Sicherung_Bemerkungen
        End If

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        'checkboxen
        objEichprozess.Eichprotokoll.Sicherung_BenannteStelle = RadCheckBoxBenannteStelle.Checked
        objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKlein = RadCheckBoxSicherungsmarkeKlein.Checked
        objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGross = RadCheckBoxSicherungsmarkeGross.Checked
        objEichprozess.Eichprotokoll.Sicherung_Hinweismarke = RadCheckBoxHinweismarke.Checked

        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

        'anzahl
        Try
            If RadTextBoxControlBenannteStelle.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl = RadTextBoxControlBenannteStelle.Text

            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlSicherungsmarkeKlein.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeKleinAnzahl = RadTextBoxControlSicherungsmarkeKlein.Text

            End If
        Catch ex As Exception
        End Try
        Try

            If RadTextBoxControlSicherungsmarkeGross.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_SicherungsmarkeGrossAnzahl = RadTextBoxControlSicherungsmarkeGross.Text
            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlHinweismarke.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_HinweismarkeAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_HinweismarkeAnzahl = RadTextBoxControlHinweismarke.Text

            End If
        Catch ex As Exception
        End Try



        'alibi
        objEichprozess.Eichprotokoll.Sicherung_DatenAusgelesen = RadCheckBoxKonfigurationsProgramm.Checked
        objEichprozess.Eichprotokoll.Sicherung_AlibispeicherEingerichtet = RadCheckBoxAlibispeicher.Checked
        objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert = RadCheckBoxAufbewahrungsdauer.Checked
        objEichprozess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung = RadTextBoxControlAufbewahrungsdauer.Text
        objEichprozess.Eichprotokoll.Sicherung_Bemerkungen = RadTextBoxControlBemerkungen.Text

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

        If RadCheckBoxSicherungsmarkeKlein.Checked Then
            If RadTextBoxControlSicherungsmarkeKlein.Text = "" Then
                AbortSaving = True
                RadTextBoxControlSicherungsmarkeKlein.Focus()
            End If
        End If

        If RadCheckBoxSicherungsmarkeGross.Checked Then
            If RadTextBoxControlSicherungsmarkeGross.Text = "" Then
                AbortSaving = True
                RadTextBoxControlSicherungsmarkeGross.Focus()
            End If
        End If

        If RadCheckBoxHinweismarke.Checked Then
            If RadTextBoxControlHinweismarke.Text = "" Then
                AbortSaving = True
                RadTextBoxControlHinweismarke.Focus()
            End If
        End If

        If RadCheckBoxBenannteStelle.Checked Then
            If RadTextBoxControlBenannteStelle.Text = "" Then
                AbortSaving = True
                RadTextBoxControlBenannteStelle.Focus()
            End If
        End If

        If RadCheckBoxAufbewahrungsdauer.Checked Then
            If RadTextBoxControlAufbewahrungsdauer.Text = "" Then
                AbortSaving = True
                RadTextBoxControlAufbewahrungsdauer.Focus()
            End If
        End If

        If AbortSaving Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaving = False
        Return True

    End Function

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
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                            objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
                        End If
                    ElseIf AktuellerStatusDirty = True Then
                        objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
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
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function

    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungEichtechnischeSicherung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEichtechnischeSicherung
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco19EichtechnischeSicherung))
        Lokalisierung(Me, resources)
    End Sub
#End Region



    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(Me.RadScrollablePanel1.PanelContainer)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        'Erzeugen eines Server Objektes auf basis des aktuellen DS. Setzt es auf es ausserdem auf Fehlerhaft
        CloneAndSendServerObjekt()
    End Sub

End Class