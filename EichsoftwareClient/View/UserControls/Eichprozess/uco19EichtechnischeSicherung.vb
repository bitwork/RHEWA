Public Class uco19EichtechnischeSicherung
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
        _suspendEvents = True

        InitializeComponent()
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung
    End Sub
#End Region



#Region "Events"
    ''' <summary>
    ''' Validations the needed.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Overrides Function ValidationNeeded() As Boolean
        LoadFromDatabase()
        Return ValidateControls()
    End Function
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungEichtechnischeSicherung)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEichtechnischeSicherung
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung

        'daten füllen
        LoadFromDatabase()
    End Sub

#End Region

#Region "Methods"
    Protected Friend Overrides Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
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
    Private Sub FillControls()
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
        If Not objEichprozess.Eichprotokoll.Sicherung_BenannteStelle Is Nothing Then
            RadCheckBoxBenannteStelle.Checked = objEichprozess.Eichprotokoll.Sicherung_BenannteStelle
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13 Is Nothing Then
            RadCheckBoxEichsiegel13x13.Checked = objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_EichsiegelRund Is Nothing Then
            RadCheckBoxEichsiegelRund.Checked = objEichprozess.Eichprotokoll.Sicherung_EichsiegelRund
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelocht Is Nothing Then
            RadCheckBoxHinweismarke.Checked = objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelocht
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_GruenesM Is Nothing Then
            RadCheckBoxGruenesM.Checked = objEichprozess.Eichprotokoll.Sicherung_GruenesM
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_CE Is Nothing Then
            RadCheckBoxCEKennzeichen.Checked = objEichprozess.Eichprotokoll.Sicherung_CE
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_CE2016 Is Nothing Then
            RadCheckBoxCE2016Kennzeichen.Checked = objEichprozess.Eichprotokoll.Sicherung_CE2016
        End If


        'anzahl
        If Not objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl Is Nothing Then
            RadTextBoxControlBenannteStelle.Text = objEichprozess.Eichprotokoll.Sicherung_BenannteStelleAnzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl Is Nothing Then
            RadTextBoxControlEichsiegel13x13.Text = objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl Is Nothing Then
            RadTextBoxControlEichsiegelRund.Text = objEichprozess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl
        End If

        If Not objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl Is Nothing Then
            RadTextBoxControlHinweismarke.Text = objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_GruenesMAnzahl Is Nothing Then
            RadTextBoxControlGruenesM.Text = objEichprozess.Eichprotokoll.Sicherung_GruenesMAnzahl
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_CEAnzahl Is Nothing Then
            RadTextBoxControlCEKennzeichen.Text = objEichprozess.Eichprotokoll.Sicherung_CEAnzahl
        End If
        If Not objEichprozess.Eichprotokoll.Sicherung_CE2016Anzahl Is Nothing Then
            RadTextBoxControlCE2016Kennzeichen.Text = objEichprozess.Eichprotokoll.Sicherung_CE2016Anzahl
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
    Private Sub UpdateObject()
        'checkboxen
        objEichprozess.Eichprotokoll.Sicherung_BenannteStelle = RadCheckBoxBenannteStelle.Checked
        objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13 = RadCheckBoxEichsiegel13x13.Checked
        objEichprozess.Eichprotokoll.Sicherung_EichsiegelRund = RadCheckBoxEichsiegelRund.Checked
        objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelocht = RadCheckBoxHinweismarke.Checked
        objEichprozess.Eichprotokoll.Sicherung_GruenesM = RadCheckBoxGruenesM.Checked
        objEichprozess.Eichprotokoll.Sicherung_CE = RadCheckBoxCEKennzeichen.Checked
        objEichprozess.Eichprotokoll.Sicherung_CE2016 = RadCheckBoxCE2016Kennzeichen.Checked


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
            If RadTextBoxControlEichsiegel13x13.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl = RadTextBoxControlEichsiegel13x13.Text

            End If
        Catch ex As Exception
        End Try
        Try

            If RadTextBoxControlEichsiegelRund.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl = RadTextBoxControlEichsiegelRund.Text
            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlHinweismarke.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl = RadTextBoxControlHinweismarke.Text

            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlGruenesM.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_GruenesMAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_GruenesMAnzahl = RadTextBoxControlGruenesM.Text

            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlCEKennzeichen.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_CEAnzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_CEAnzahl = RadTextBoxControlCEKennzeichen.Text

            End If
        Catch ex As Exception
        End Try
        Try
            If RadTextBoxControlCE2016Kennzeichen.Text.Trim.Equals("") Then
                objEichprozess.Eichprotokoll.Sicherung_CE2016Anzahl = 0
            Else
                objEichprozess.Eichprotokoll.Sicherung_CE2016Anzahl = RadTextBoxControlCE2016Kennzeichen.Text

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
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        Me.AbortSaving = False

        If RadCheckBoxBenannteStelle.Checked Then
            If RadTextBoxControlBenannteStelle.Text = "" Then
                AbortSaving = True
                RadTextBoxControlBenannteStelle.Focus()
            End If
        End If

        If RadCheckBoxEichsiegel13x13.Checked Then
            If RadTextBoxControlEichsiegel13x13.Text = "" Then
                AbortSaving = True
                RadTextBoxControlEichsiegel13x13.Focus()
            End If
        End If

        If RadCheckBoxEichsiegelRund.Checked Then
            If RadTextBoxControlEichsiegelRund.Text = "" Then
                AbortSaving = True
                RadTextBoxControlEichsiegelRund.Focus()
            End If
        End If

        If RadCheckBoxHinweismarke.Checked Then
            If RadTextBoxControlHinweismarke.Text = "" Then
                AbortSaving = True
                RadTextBoxControlHinweismarke.Focus()
            End If
        End If

        If RadCheckBoxGruenesM.Checked Then
            If RadTextBoxControlGruenesM.Text = "" Then
                AbortSaving = True
                RadTextBoxControlGruenesM.Focus()
            End If
        End If

        If RadCheckBoxCEKennzeichen.Checked Then
            If RadTextBoxControlCEKennzeichen.Text = "" Then
                AbortSaving = True
                RadTextBoxControlCEKennzeichen.Focus()
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

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
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

                            If AktuellerStatusDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Export Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Export
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
                    Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
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
    Protected Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_PruefungEichtechnischeSicherung)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEichtechnischeSicherung
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco19EichtechnischeSicherung))

        Me.lblAlibispeicher.Text = resources.GetString("lblAlibispeicher.Text")
        Me.lblAnzahl.Text = resources.GetString("lblAnzahl.Text")
        Me.lblAufbewahrungsdauer.Text = resources.GetString("lblAufbewahrungsdauer.Text")
        Me.lblBemerkungen.Text = resources.GetString("lblBemerkungen.Text")
        Me.lblBenannteStelle.Text = resources.GetString("lblBenannteStelle.Text")
        Me.lblCEKennzeichen.Text = resources.GetString("lblCEKennzeichen.Text")
        Me.lblDatenKonfiguration.Text = resources.GetString("lblDatenKonfiguration.Text")
        Me.lblEichsiegel13x13.Text = resources.GetString("lblEichsiegel13x13.Text")
        Me.lblEichsiegelRund.Text = resources.GetString("lblEichsiegelRund.Text")
        Me.lblGruenesM.Text = resources.GetString("lblGruenesM.Text")
        Me.lblHinweismarke.Text = resources.GetString("lblHinweismarke.Text")
        Me.lblSicherungshinweise.Text = resources.GetString("lblSicherungshinweise.Text")



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

#Region "Checkboxen EVents mit Textboxen"
    Private Sub RadCheckBoxBenannteStelle_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxBenannteStelle.ToggleStateChanged
        RadTextBoxControlBenannteStelle.ReadOnly = Not RadCheckBoxBenannteStelle.Checked
        PictureBoxBenannteStelle.Visible = Not RadCheckBoxBenannteStelle.Checked

        If RadCheckBoxBenannteStelle.Checked = False Then
            RadTextBoxControlBenannteStelle.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxEichsiegel13x13_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxEichsiegel13x13.ToggleStateChanged
        RadTextBoxControlEichsiegel13x13.ReadOnly = Not RadCheckBoxEichsiegel13x13.Checked
        PictureBoxEichsiegel13x13.Visible = Not RadCheckBoxEichsiegel13x13.Checked

        If RadCheckBoxEichsiegel13x13.Checked = False Then
            RadTextBoxControlEichsiegel13x13.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxEichsiegelRund_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxEichsiegelRund.ToggleStateChanged
        RadTextBoxControlEichsiegelRund.ReadOnly = Not RadCheckBoxEichsiegelRund.Checked
        PictureBoxEichsiegelRund.Visible = Not RadCheckBoxEichsiegelRund.Checked

        If RadCheckBoxEichsiegelRund.Checked = False Then
            RadTextBoxControlEichsiegelRund.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxHinweismarke_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxHinweismarke.ToggleStateChanged
        RadTextBoxControlHinweismarke.ReadOnly = Not RadCheckBoxHinweismarke.Checked
        PictureBoxHinweismarke.Visible = Not RadCheckBoxHinweismarke.Checked

        If RadCheckBoxHinweismarke.Checked = False Then
            RadTextBoxControlHinweismarke.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxGrunesM_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxGruenesM.ToggleStateChanged
        RadTextBoxControlGruenesM.ReadOnly = Not RadCheckBoxGruenesM.Checked
        PictureBoxGruenesM.Visible = Not RadCheckBoxGruenesM.Checked

        If RadCheckBoxGruenesM.Checked = False Then
            RadTextBoxControlGruenesM.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxCE_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxCEKennzeichen.ToggleStateChanged
        RadTextBoxControlCEKennzeichen.ReadOnly = Not RadCheckBoxCEKennzeichen.Checked
        PictureBoxCE.Visible = Not RadCheckBoxCEKennzeichen.Checked

        If RadCheckBoxCEKennzeichen.Checked = False Then
            RadTextBoxControlCEKennzeichen.Text = ""
        End If
    End Sub

    Private Sub RadCheckBoxCE2016_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxCE2016Kennzeichen.ToggleStateChanged
        RadTextBoxControlCE2016Kennzeichen.ReadOnly = Not RadCheckBoxCE2016Kennzeichen.Checked
        PictureBoxCE2016.Visible = Not RadCheckBoxCE2016Kennzeichen.Checked

        If RadCheckBoxCE2016Kennzeichen.Checked = False Then
            RadTextBoxControlCE2016Kennzeichen.Text = ""
        End If
    End Sub



#End Region


    Private Sub RadTextBoxControlBenannteStelle_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlHinweismarke.Validating, RadTextBoxControlGruenesM.Validating, RadTextBoxControlEichsiegelRund.Validating, RadTextBoxControlEichsiegel13x13.Validating, RadTextBoxControlCEKennzeichen.Validating, RadTextBoxControlBenannteStelle.Validating, RadTextBoxControlCE2016Kennzeichen.Validating
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
                'objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss
                UpdateObject()

                'erzeuegn eines Server Objektes auf basis des aktuellen DS
                objServerEichprozess = clsClientServerConversionFunctions.CopyServerObjectProperties(objServerEichprozess, objEichprozess, clsClientServerConversionFunctions.enuModus.RHEWASendetAnClient)
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

    Private Sub RadTextBoxControlBenannteStelle_TextChanged(sender As System.Object, e As System.EventArgs) Handles RadTextBoxControlHinweismarke.TextChanged, RadTextBoxControlGruenesM.TextChanged, RadTextBoxControlEichsiegelRund.TextChanged, RadTextBoxControlEichsiegel13x13.TextChanged, RadTextBoxControlCEKennzeichen.TextChanged, RadTextBoxControlBenannteStelle.TextChanged, RadTextBoxControlBemerkungen.TextChanged, RadTextBoxControlAufbewahrungsdauer.TextChanged, RadTextBoxControlCE2016Kennzeichen.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub

    Private Sub RadCheckBoxBenannteStelle_Click(sender As System.Object, e As System.EventArgs) Handles RadCheckBoxKonfigurationsProgramm.Click, RadCheckBoxHinweismarke.Click, RadCheckBoxGruenesM.Click, RadCheckBoxEichsiegelRund.Click, RadCheckBoxEichsiegel13x13.Click, RadCheckBoxCEKennzeichen.Click, RadCheckBoxBenannteStelle.Click, RadCheckBoxAufbewahrungsdauer.Click, RadCheckBoxAlibispeicher.Click, RadCheckBoxCE2016Kennzeichen.Click
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
    End Sub
End Class
