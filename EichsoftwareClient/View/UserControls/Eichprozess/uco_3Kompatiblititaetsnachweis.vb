Public Class uco_3Kompatiblititaetsnachweis

    Inherits ucoContent

#Region "Member Variables"
    Private _objMogelstatistik As Mogelstatistik
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
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
        _suspendEvents = False
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis

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
        Me.SuspendLayout()

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis

        'daten füllen
        LoadFromDatabase()

        Me.ResumeLayout()

    End Sub

    Protected Friend Overrides Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True

        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()
        'events abbrechen
        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Beschaffenheitspruefungsobjekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        'art der Waage abrufen um dementsprechend Höchlast und Eichwerte Steuerlemente auszublenden
        'Dim Waagenart As Lookup_Waagenart = Nothing
        'waagenarten abrufen

        'nur überschreiben wenn leer. Wird genutzt für vor und zurück blättern-
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 Is Nothing Then
            RadTextBoxControlWaageHoechstlast1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        End If
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 Is Nothing Then
            RadTextBoxControlWaageHoechstlast2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        End If
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 Is Nothing Then
            RadTextBoxControlWaageHoechstlast3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        End If
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 Is Nothing Then
            RadTextBoxControlWaageEichwert1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        End If
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 Is Nothing Then
            RadTextBoxControlWaageEichwert2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        End If
        If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 Is Nothing Then
            RadTextBoxControlWaageEichwert3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        End If

        'nur wenn die Werte bereits geschrieben wurden, werden Sie in die Textboxen übernommen, da sonst die autowerte überschrieben würden
        If Not objEichprozess.Kompatiblitaetsnachweis Is Nothing Then
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis Is Nothing Then
                RadTextBoxControlWaageUebersetzungsverhaeltnis.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen Is Nothing Then
                RadTextBoxControlWaageAnzahlWaegezellen.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich Is Nothing Then
                RadTextBoxControlEinschaltnullstellbereich.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag Is Nothing Then
                RadTextBoxControlWaageEcklastzuschlag.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag

            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast Is Nothing Then
                RadTextBoxControlWaageTotlast.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast

            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge Is Nothing Then
                RadTextBoxControlWaageKabellaenge.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge

            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt Is Nothing Then
                RadTextBoxControlWaageKabelquerschnitt.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt

            End If
            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart Is Nothing Then
                RadTextBoxControlAWGAnschlussart.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart

            End If

            If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast Is Nothing Then
                RadTextBoxControlWZHoechstlast.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Split(";")(0)
            End If
        End If

        If DialogModus = enuDialogModus.lesend Then
            'falls der Konformitätsbewertungsvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            DisableControls(RadGroupBoxAWG)
            DisableControls(RadGroupBoxVerbindungselemente)
            DisableControls(RadGroupBoxWaage)
            DisableControls(RadGroupBoxWZ)

        End If

        If Not objEichprozess.FK_WaagenArt Is Nothing Then

            '  Waagenart = (From dbWaagenart In Context.Lookup_Waagenart Select dbWaagenart Where dbWaagenart.ID = objEichprozess.FK_WaagenArt).FirstOrDefault

            If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                'zweiten und dritten bereich ausblenden
                lblMax2.Visible = False
                RadTextBoxControlWaageHoechstlast2.Visible = False
                lblKGMax2.Visible = False

                lblPflichtfeld1.Visible = True
                lblPflichtfeld2.Visible = True
                lblPflichtfeld3.Visible = False
                lblPflichtfeld4.Visible = False
                lblPflichtfeld5.Visible = False
                lblPflichtfeld6.Visible = False

                lblE2.Visible = False
                RadTextBoxControlWaageEichwert2.Visible = False
                lblEKG2.Visible = False

                lblMax3.Visible = False
                RadTextBoxControlWaageHoechstlast3.Visible = False
                lblKGMax3.Visible = False

                lblE3.Visible = False
                RadTextBoxControlWaageEichwert3.Visible = False
                lblEKG3.Visible = False

                'umbennenen des Textes
                lblMax1.Text = "Max"
                lblE1.Text = "e"

            ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then

                'zweiten Bereich einblenden
                lblMax2.Visible = True
                RadTextBoxControlWaageHoechstlast2.Visible = True
                lblKGMax2.Visible = True

                lblE2.Visible = True
                RadTextBoxControlWaageEichwert2.Visible = True
                lblEKG2.Visible = True

                lblPflichtfeld1.Visible = True
                lblPflichtfeld2.Visible = True
                lblPflichtfeld3.Visible = True
                lblPflichtfeld4.Visible = True
                lblPflichtfeld5.Visible = False
                lblPflichtfeld6.Visible = False

                'dritten Ausblenden
                lblMax3.Visible = False
                RadTextBoxControlWaageHoechstlast3.Visible = False
                lblKGMax3.Visible = False

                lblE3.Visible = False
                RadTextBoxControlWaageEichwert3.Visible = False
                lblEKG3.Visible = False

                lblMax1.Text = "Max1"
                lblE1.Text = "e1"
            ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Or objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                'zweiten und dritten bereich einblenden

                'zweiten Bereich einblenden
                lblMax2.Visible = True
                RadTextBoxControlWaageHoechstlast2.Visible = True
                lblKGMax2.Visible = True

                lblPflichtfeld1.Visible = True
                lblPflichtfeld2.Visible = True
                lblPflichtfeld3.Visible = True
                lblPflichtfeld4.Visible = True
                lblPflichtfeld5.Visible = True
                lblPflichtfeld6.Visible = True

                lblE2.Visible = True
                RadTextBoxControlWaageEichwert2.Visible = True
                lblEKG2.Visible = True

                'dritten Ausblenden
                lblMax3.Visible = True
                RadTextBoxControlWaageHoechstlast3.Visible = True
                lblKGMax3.Visible = True

                lblE3.Visible = True
                RadTextBoxControlWaageEichwert3.Visible = True
                lblEKG3.Visible = True

                lblMax1.Text = "Max1"
                lblE1.Text = "e1"
            End If

        End If

        'vorfüllen der Werte abhängig vom AWG
        If Not objEichprozess.FK_Auswertegeraet Is Nothing Then

            '  Dim AWG = (From dbAWG In Context.Lookup_Auswertegeraet Select dbAWG Where dbAWG.ID = objEichprozess.FK_Auswertegeraet).FirstOrDefault
            Dim AWG = objEichprozess.Lookup_Auswertegeraet

            'abhängig von Waagenart den einen Wert oder den anderen auslesen
            If Not objEichprozess.Lookup_Waagenart Is Nothing Then
                If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                    RadTextBoxControlAWGTeilungswerte.Text = AWG.MAXAnzahlTeilungswerteEinbereichswaage
                Else
                    RadTextBoxControlAWGTeilungswerte.Text = AWG.MAXAnzahlTeilungswerteMehrbereichswaage
                End If
            End If
            RadTextBoxControlAWGSpeisespannung.Text = AWG.Speisespannung
            RadTextBoxControlAWGMindestmesssignal.Text = AWG.Mindestmesssignal
            RadTextBoxControlAWGGrenzwerteLastwiderstandMin.Text = AWG.GrenzwertLastwiderstandMIN
            RadTextBoxControlAWGGrenzwerteLastwiderstandMax.Text = AWG.GrenzwertLastwiderstandMAX
            RadTextBoxControlAWGKabellaenge.Text = AWG.KabellaengeQuerschnitt
            RadTextBoxControlAWGKlasse.Text = AWG.Genauigkeitsklasse

            RadTextBoxControlAWGTemperaturbereichMax.Text = AWG.GrenzwertTemperaturbereichMAX
            RadTextBoxControlAWGTemperaturbereichMin.Text = AWG.GrenzwertTemperaturbereichMIN
        End If

        'vorfüllen der Werte abhängig vom WZ
        If Not objEichprozess.FK_Waegezelle Is Nothing Then
            'Dim WZ = (From dbWZ In Context.Lookup_Waegezelle Select dbWZ Where dbWZ.ID = objEichprozess.FK_Waegezelle).FirstOrDefault
            Dim wz = objEichprozess.Lookup_Waegezelle

            If Not wz.Genauigkeitsklasse Is Nothing Then
                RadTextBoxControlWZGenauigkeitsklasse.Text = wz.Genauigkeitsklasse
            End If

            If Not wz.Mindestvorlast Is Nothing Then
                RadTextBoxControlWZMindestvorlast.Text = wz.Mindestvorlast
                If IsNumeric(RadTextBoxControlWZHoechstlast.Text) Then
                    Try
                        If Not objEichprozess.Lookup_Waegezelle.MindestvorlastProzent Is Nothing Then
                            RadTextBoxControlWZMindestvorlast.Text = (objEichprozess.Lookup_Waegezelle.MindestvorlastProzent / 100) * RadTextBoxControlWZHoechstlast.Text
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
            If Not wz.Waegezellenkennwert Is Nothing Then
                RadTextBoxControlWZWaegezellenkennwert.Text = wz.Waegezellenkennwert

            End If
            If Not wz.MaxAnzahlTeilungswerte Is Nothing Then
                RadTextBoxControlWZMaxTeilungswerte.Text = wz.MaxAnzahlTeilungswerte

            End If
            If Not wz.MinTeilungswert Is Nothing Then
                RadTextBoxControlWZMinTeilungswert.Text = wz.MinTeilungswert

            End If
            If Not wz.Hoechsteteilungsfaktor Is Nothing Then
                Dim wertHoechsteteilungsfaktor = objEichprozess.Lookup_Waegezelle.Hoechsteteilungsfaktor
                Dim wertHoechsteteilungsfaktorAufgedruckt = ""
                If Not objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast Is Nothing Then
                    If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Contains(";") Then
                        wertHoechsteteilungsfaktorAufgedruckt = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Split(";")(1)
                    End If
                End If

                RadTextBoxControlWZHoechstteilungsfaktor.Text = wertHoechsteteilungsfaktor
                RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.Text = wertHoechsteteilungsfaktorAufgedruckt

            End If

            If Not wz.Kriechteilungsfaktor Is Nothing Then
                RadTextBoxControlWZKriechteilungsfaktor.Text = wz.Kriechteilungsfaktor

            End If
            If Not wz.RueckkehrVorlastsignal Is Nothing Then
                RadTextBoxControlWZRueckkehrVorlastsignal.Text = wz.RueckkehrVorlastsignal

            End If
            If Not wz.WiderstandWaegezelle Is Nothing Then
                RadTextBoxControlWZWiderstand.Text = wz.WiderstandWaegezelle

            End If

            If Not wz.GrenzwertTemperaturbereichMAX Is Nothing Then
                RadTextBoxControlWZTemperaturbereichMAX.Text = wz.GrenzwertTemperaturbereichMAX

            End If
            If Not wz.GrenzwertTemperaturbereichMIN Is Nothing Then
                RadTextBoxControlWZTemperaturbereichMIN.Text = wz.GrenzwertTemperaturbereichMIN

            End If

        End If

        'wenn eine neue WZ angelegt wurde, dürfen hier auch Werte für dieses eingegeben werden
        If objEichprozess.Lookup_Waegezelle.Neu = True Then
            'auslbenden der Schloss bilder
            PictureBoxWZ1.Visible = False
            PictureBoxWZ2.Visible = False
            PictureBoxWZ3.Visible = False
            PictureBoxWZ4.Visible = False
            PictureBoxWZ5.Visible = False
            PictureBoxWZ6.Visible = False
            PictureBoxWZ7.Visible = False
            PictureBoxWZ8.Visible = False
            PictureBoxWZ9.Visible = False

            'felder beschreiben lassen
            RadTextBoxControlWZGenauigkeitsklasse.ReadOnly = False
            RadTextBoxControlWZGenauigkeitsklasse.Enabled = True

            RadTextBoxControlWZHoechstteilungsfaktor.ReadOnly = False
            RadTextBoxControlWZKriechteilungsfaktor.ReadOnly = False
            RadTextBoxControlWZMaxTeilungswerte.ReadOnly = False
            RadTextBoxControlWZMindestvorlast.ReadOnly = False
            RadTextBoxControlWZMinTeilungswert.ReadOnly = False
            RadTextBoxControlWZRueckkehrVorlastsignal.ReadOnly = False
            RadTextBoxControlWZWaegezellenkennwert.ReadOnly = False
            RadTextBoxControlWZWiderstand.ReadOnly = False

            'tabstops erlauben
            RadTextBoxControlWZGenauigkeitsklasse.TabStop = True
            RadTextBoxControlWZHoechstteilungsfaktor.TabStop = True
            RadTextBoxControlWZKriechteilungsfaktor.TabStop = True
            RadTextBoxControlWZMaxTeilungswerte.TabStop = True
            RadTextBoxControlWZMindestvorlast.TabStop = True
            RadTextBoxControlWZMinTeilungswert.TabStop = True
            RadTextBoxControlWZRueckkehrVorlastsignal.TabStop = True
            RadTextBoxControlWZWaegezellenkennwert.TabStop = True
            RadTextBoxControlWZWiderstand.TabStop = True
        Else
            PictureBoxWZ1.Visible = True
            PictureBoxWZ2.Visible = True
            PictureBoxWZ3.Visible = True
            PictureBoxWZ4.Visible = True
            PictureBoxWZ5.Visible = True
            PictureBoxWZ6.Visible = True
            PictureBoxWZ7.Visible = True
            PictureBoxWZ8.Visible = True
            PictureBoxWZ9.Visible = True
            PictureBoxWZ10.Visible = True
            PictureBoxWZ11.Visible = True

            RadTextBoxControlWZGenauigkeitsklasse.ReadOnly = True

            RadTextBoxControlWZHoechstteilungsfaktor.ReadOnly = True
            RadTextBoxControlWZKriechteilungsfaktor.ReadOnly = True
            RadTextBoxControlWZMaxTeilungswerte.ReadOnly = True
            RadTextBoxControlWZMindestvorlast.ReadOnly = True
            RadTextBoxControlWZMinTeilungswert.ReadOnly = True
            RadTextBoxControlWZRueckkehrVorlastsignal.ReadOnly = True
            RadTextBoxControlWZWaegezellenkennwert.ReadOnly = True
            RadTextBoxControlWZWiderstand.ReadOnly = True

            'tabstops verbieten
            RadTextBoxControlWZGenauigkeitsklasse.TabStop = False
            RadTextBoxControlWZHoechstteilungsfaktor.TabStop = False
            RadTextBoxControlWZKriechteilungsfaktor.TabStop = False
            RadTextBoxControlWZMaxTeilungswerte.TabStop = False
            RadTextBoxControlWZMindestvorlast.TabStop = False
            RadTextBoxControlWZMinTeilungswert.TabStop = False
            RadTextBoxControlWZRueckkehrVorlastsignal.TabStop = False
            RadTextBoxControlWZWaegezellenkennwert.TabStop = False
            RadTextBoxControlWZWiderstand.TabStop = False

        End If

        'fokus setzen
        RadTextBoxControlWaageHoechstlast1.Focus()

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now

        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart = RadTextBoxControlAWGAnschlussart.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast = RadTextBoxControlWZHoechstlast.Text.Trim.Split(";")(0)
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast = RadTextBoxControlWaageAdditiveTarahoechstlast.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen = RadTextBoxControlWaageAnzahlWaegezellen.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag = RadTextBoxControlWaageEcklastzuschlag.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich = RadTextBoxControlEinschaltnullstellbereich.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse = RadTextBoxControlWaageKlasse.Text.ToUpper.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = RadTextBoxControlWaageTemperaturbereichMax.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = RadTextBoxControlWaageTemperaturbereichMin.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge = RadTextBoxControlWaageKabellaenge.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt = RadTextBoxControlWaageKabelquerschnitt.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast = RadTextBoxControlWaageTotlast.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = RadTextBoxControlWaageUebersetzungsverhaeltnis.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 = RadTextBoxControlWaageHoechstlast1.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 = RadTextBoxControlWaageHoechstlast2.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 = RadTextBoxControlWaageHoechstlast3.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = RadTextBoxControlWaageEichwert1.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = RadTextBoxControlWaageEichwert2.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = RadTextBoxControlWaageEichwert3.Text.Trim
        objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer = ""

        If RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.Text.Trim <> "" Then
            objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast += ";" & RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.Text.Trim
        End If

        'im falle einer neuen WZ die Werte übernehmen
        If objEichprozess.Lookup_Waegezelle.Neu Then
            objEichprozess.Lookup_Waegezelle.Genauigkeitsklasse = RadTextBoxControlWZGenauigkeitsklasse.Text.ToUpper.Trim
            objEichprozess.Lookup_Waegezelle.Mindestvorlast = RadTextBoxControlWZMindestvorlast.Text.Trim
            objEichprozess.Lookup_Waegezelle.Waegezellenkennwert = RadTextBoxControlWZWaegezellenkennwert.Text.Trim
            objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte = RadTextBoxControlWZMaxTeilungswerte.Text.Trim
            objEichprozess.Lookup_Waegezelle.MinTeilungswert = RadTextBoxControlWZMinTeilungswert.Text.Trim
            objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor = RadTextBoxControlWZKriechteilungsfaktor.Text.Trim
            objEichprozess.Lookup_Waegezelle.Hoechsteteilungsfaktor = RadTextBoxControlWZHoechstteilungsfaktor.Text.Trim
            objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal = RadTextBoxControlWZRueckkehrVorlastsignal.Text.Trim
            objEichprozess.Lookup_Waegezelle.WiderstandWaegezelle = RadTextBoxControlWZWiderstand.Text.Trim
            objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN = RadTextBoxControlWZTemperaturbereichMIN.Text.Trim
            objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX = RadTextBoxControlWZTemperaturbereichMAX.Text.Trim
            objEichprozess.Lookup_Waegezelle.BruchteilEichfehlergrenze = RadTextBoxControlWZBruchteilEichfehlergrenze.Text.Trim
        End If

        'Mogelstatistik Objekt erzeugen. Dies passiert immer wenn geblättert wird.
        Dim objMogelstatistik As Mogelstatistik = New Mogelstatistik

        objMogelstatistik.Eichprozess = objEichprozess
        objMogelstatistik.FK_Auswertegeraet = objEichprozess.FK_Auswertegeraet
        objMogelstatistik.FK_Eichprozess = objEichprozess.ID
        objMogelstatistik.FK_Waegezelle = objEichprozess.FK_Waegezelle
        objMogelstatistik.Kompatiblitaet_AnschriftWaagenbaufirma = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller + " | " + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse + " " + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort + ", " + objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl
        objMogelstatistik.Kompatiblitaet_AWG_Anschlussart = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart
        objMogelstatistik.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze
        objMogelstatistik.Kompatiblitaet_Waage_AdditiveTarahoechstlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast
        objMogelstatistik.Kompatiblitaet_Waage_AnzahlWaegezellen = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        objMogelstatistik.Kompatiblitaet_Waage_Bauartzulassung = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung
        objMogelstatistik.Kompatiblitaet_Waage_Ecklastzuschlag = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag
        objMogelstatistik.Kompatiblitaet_Waage_Einschaltnullstellbereich = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        objMogelstatistik.Kompatiblitaet_Waage_FabrikNummer = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        objMogelstatistik.Kompatiblitaet_Waage_Genauigkeitsklasse = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
        objMogelstatistik.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX
        objMogelstatistik.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich1 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich2 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertMax_Bereich3 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich1 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich2 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        objMogelstatistik.Kompatiblitaet_Waage_HoechstlastEichwertE_Bereich3 = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        objMogelstatistik.Kompatiblitaet_Waage_Kabellaenge = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge
        objMogelstatistik.Kompatiblitaet_Waage_Kabelquerschnitt = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        objMogelstatistik.Kompatiblitaet_Waage_Totlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast
        objMogelstatistik.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis
        objMogelstatistik.Kompatiblitaet_Waage_Zulassungsinhaber = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber
        objMogelstatistik.Kompatiblitaet_WZ_Hoechstlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

        'Vergleichen ob Änderungen vorliegen
        If objMogelstatistik.Equals(_objMogelstatistik) Then
            _objMogelstatistik = Nothing
        Else
            _objMogelstatistik = objMogelstatistik
        End If

    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean

        'If Debugger.IsAttached Then 'für debugzwecke
        '    Return True
        'End If
        'prüfen ob alle Felder ausgefüllt sind
        AbortSaving = False
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadTextBox Then
                        If Control.readonly = False AndAlso Control.visible = True Then

                            'anzahl wZ limitieren
                            If Control.Equals(RadTextBoxControlWaageAnzahlWaegezellen) Then
                                'darf nicht höher als 12 sein
                                If Not String.IsNullOrWhiteSpace(RadTextBoxControlWaageAnzahlWaegezellen.Text) Then
                                    If CInt(RadTextBoxControlWaageAnzahlWaegezellen.Text) > 12 Then
                                        RadTextBoxControlWaageAnzahlWaegezellen.Text = 12
                                        CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                                        'CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                                        'Return False
                                        Me.AbortSaving = True
                                        Continue For
                                    End If
                                    If CInt(RadTextBoxControlWaageAnzahlWaegezellen.Text) < 1 Then
                                        RadTextBoxControlWaageAnzahlWaegezellen.Text = 1
                                        CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                                        'CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                                        'Return False
                                        Me.AbortSaving = True
                                        Continue For
                                    End If

                                End If
                            End If

                            If Control.Text.trim.Equals("") Then

                                'sonderfälle
                                'emin kein Pflichtfeld
                                If Control.Equals(RadTextBoxControlWZMindestvorlast) Then
                                    'standardwert auf 0 setzen
                                    RadTextBoxControlWZMindestvorlast.Text = 0
                                    Continue For
                                End If

                                'minteilungswert darf leer sein, wenn Hoechsteilungsfaktor gefüllt
                                If Control.Equals(RadTextBoxControlWZMinTeilungswert) Then
                                    'ist gültig wenn hoechstteilungsfaktor gefüllt
                                    If Not RadTextBoxControlWZHoechstteilungsfaktor.Text.Trim.Equals("") Then
                                        Continue For
                                    End If
                                End If

                                'Hoechsteilungsfaktor darf leer sein, wenn minteilungswert gefüllt
                                If Control.Equals(RadTextBoxControlWZHoechstteilungsfaktor) Then
                                    'ist gültig wenn min Teilungswert gefüllt
                                    If Not RadTextBoxControlWZMinTeilungswert.Text.Trim.Equals("") Then
                                        Continue For
                                    End If
                                End If
                                'RadTextBoxControlWZKriechteilungsfaktor darf leer sein wenn RadTextBoxControlWZRueckkehrVorlastsignal gefüllt ist
                                If Control.Equals(RadTextBoxControlWZKriechteilungsfaktor) Then
                                    'ist gültig wenn Rückkehr des Vorlastsignals gefüllt
                                    If Not RadTextBoxControlWZRueckkehrVorlastsignal.Text.Trim.Equals("") Then
                                        Continue For
                                    End If
                                End If
                                'RadTextBoxControlWZRueckkehrVorlastsignal darf leer sein wenn RadTextBoxControlWZKriechteilungsfaktor gefüllt ist
                                If Control.Equals(RadTextBoxControlWZRueckkehrVorlastsignal) Then
                                    'ist gültig wenn Kriechteilungsfaktor gefüllt
                                    If Not RadTextBoxControlWZKriechteilungsfaktor.Text.Trim.Equals("") Then
                                        Continue For
                                    End If
                                End If

                                Me.AbortSaving = True

                                CType(Control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                                ' CType(Control, Telerik.WinControls.UI.RadTextBox).Focus()
                                '  Return False

                            End If
                        End If
                    End If
                Next
            End If
        Next

        If RadTextBoxControlWZGenauigkeitsklasse.ReadOnly = False Then
            If RadTextBoxControlWZGenauigkeitsklasse.Text.ToUpper = "A" _
          Or RadTextBoxControlWZGenauigkeitsklasse.Text.ToUpper = "B" _
          Or RadTextBoxControlWZGenauigkeitsklasse.Text.ToUpper = "C" _
          Or RadTextBoxControlWZGenauigkeitsklasse.Text = "D".ToUpper _
          Then
            Else
                'Ungültiger Wert für Genauigikeitsklasse
                MessageBox.Show(My.Resources.GlobaleLokalisierung.Fehler_GenaugigkeitsklasseUnguelitg, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.AbortSaving = True
                RadTextBoxControlWZGenauigkeitsklasse.TextBoxElement.Border.ForeColor = Color.Red
                RadTextBoxControlWZGenauigkeitsklasse.Focus()
                Return False
            End If
        End If

        If Me.AbortSaving = True Then
            If Debugger.IsAttached Then 'standardwerte füllen für schnelleres testen
                If Me.ShowValidationErrorBox(True) = DialogResult.Retry Then
                    RadTextBoxControlWaageHoechstlast1.Text = "1000"
                    RadTextBoxControlWaageHoechstlast2.Text = "2000"
                    RadTextBoxControlWaageEichwert1.Text = "5"
                    RadTextBoxControlWaageEichwert2.Text = "25"
                    RadTextBoxControlWaageAnzahlWaegezellen.Text = "4"
                    RadTextBoxControlEinschaltnullstellbereich.Text = "1"
                    RadTextBoxControlWaageEcklastzuschlag.Text = "1"
                    RadTextBoxControlWaageTotlast.Text = "1"
                    RadTextBoxControlWZHoechstlast.Text = "2500"
                    Return True
                Else
                    MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            Else
                MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        End If

        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaving = False
        Return True

        'prüfen ob eine neue WZ angelegt wurde (über button und neuem Dialog vermutlich)
    End Function

    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
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
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                                AktuellerStatusDirty = False
                            End If

                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()
                            'Speichern in Datenbank
                            Context.SaveChanges()

                            'Mogelstatistik neuen Eintrag anlegen
                            If Not _objMogelstatistik Is Nothing Then
                                Context.Mogelstatistik.Add(_objMogelstatistik)
                                Context.SaveChanges()
                            End If
                        End If
                    End If
                End Using

                ParentFormular.CurrentEichprozess = objEichprozess
            End If

        End If
    End Sub

    Protected Overrides Sub SaveWithoutValidationNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then
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

                        'Mogelstatistik neuen Eintrag anlegen
                        If Not _objMogelstatistik Is Nothing Then
                            Context.Mogelstatistik.Add(_objMogelstatistik)
                            Context.SaveChanges()
                        End If
                    End If
                End If
            End Using

            ParentFormular.CurrentEichprozess = objEichprozess
        End If

    End Sub

#End Region

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_3Kompatiblititaetsnachweis))

        Me.RadGroupBoxVerbindungselemente.Text = resources.GetString("RadGroupBoxVerbindungselemente.Text")
        Me.RadGroupBoxWZ.Text = resources.GetString("RadGroupBoxWZ.Text")
        Me.RadGroupBoxWaage.Text = resources.GetString("RadGroupBoxWaage.Text")
        Me.RadGroupBoxAWG.Text = resources.GetString("RadGroupBoxAWG.Text")
        Me.lblVerbindungEichfehlergrenze.Text = resources.GetString("lblVerbindungEichfehlergrenze.Text")
        Me.lblWZEichfehlergrenze.Text = resources.GetString("lblWZEichfehlergrenze.Text")
        Me.lblWZTemperatur.Text = resources.GetString("lblWZTemperatur.Text")
        Me.lblWZKlasse.Text = resources.GetString("lblWZKlasse.Text")
        Me.lblWZWiderstandWZ.Text = resources.GetString("lblWZWiderstandWZ.Text")
        Me.lblWZRueckkehrVorlastsignal.Text = resources.GetString("lblWZRueckkehrVorlastsignal.Text")
        Me.lblwzKriechteilungsfaktor.Text = resources.GetString("lblwzKriechteilungsfaktor.Text")
        Me.lblWZHoechstteilungswert.Text = resources.GetString("lblWZHoechstteilungswert.Text")
        Me.lblWZMinTeilungswert.Text = resources.GetString("lblWZMinTeilungswert.Text")
        Me.lblWZTeilungswerte.Text = resources.GetString("lblWZTeilungswerte.Text")
        Me.lblWZWaegezellenkennwert.Text = resources.GetString("lblWZWaegezellenkennwert.Text")
        Me.lblWZMindestvorlast.Text = resources.GetString("lblWZMindestvorlast.Text")
        Me.lblWZHoechstlast.Text = resources.GetString("lblWZHoechstlast.Text")
        Me.lblAWGKlasse.Text = resources.GetString("lblAWGKlasse.Text")
        Me.lblAWGKabellaenge.Text = resources.GetString("lblAWGKabellaenge.Text")
        Me.lblAWGAnschlussart.Text = resources.GetString("lblAWGAnschlussart.Text")
        Me.lblAWGEichfehlergrenze.Text = resources.GetString("lblAWGEichfehlergrenze.Text")
        Me.lblAWGTemperatur.Text = resources.GetString("lblAWGTemperatur.Text")
        Me.lblAWGWiderstand.Text = resources.GetString("lblAWGWiderstand.Text")
        Me.lblAWGMinMessSignal.Text = resources.GetString("lblAWGMinMessSignal.Text")
        Me.lblAWGMindesteingangsspannung.Text = resources.GetString("lblAWGMindesteingangsspannung.Text")
        Me.lblAWGSpeisespannung.Text = resources.GetString("lblAWGSpeisespannung.Text")
        Me.lblAWGmaxAnzahlTeilungswerte.Text = resources.GetString("lblAWGmaxAnzahlTeilungswerte.Text")
        Me.lblEKG3.Text = resources.GetString("lblEKG3.Text")
        Me.lblEKG2.Text = resources.GetString("lblEKG2.Text")
        Me.lblEKG1.Text = resources.GetString("lblEKG1.Text")
        Me.lblKGMax3.Text = resources.GetString("lblKGMax3.Text")
        Me.lblKGMax2.Text = resources.GetString("lblKGMax2.Text")
        Me.lblKGMax1.Text = resources.GetString("lblKGMax1.Text")
        Me.lblKabelquerschnitt.Text = resources.GetString("lblKabelquerschnitt.Text")
        Me.lblKabellaenge.Text = resources.GetString("lblKabellaenge.Text")
        Me.lblGrenzenTemp.Text = resources.GetString("lblGrenzenTemp.Text")
        Me.lbladditiveTarahoechstlast.Text = resources.GetString("lbladditiveTarahoechstlast.Text")
        Me.lbltotlast.Text = resources.GetString("lbltotlast.Text")
        Me.lblEcklastzuschalg.Text = resources.GetString("lblEcklastzuschalg.Text")
        Me.lblEinschaltnullbereich.Text = resources.GetString("lblEinschaltnullbereich.Text")
        Me.lblAnzWaeegezellen.Text = resources.GetString("lblAnzWaeegezellen.Text")
        Me.lblUebersetzung.Text = resources.GetString("lblUebersetzung.Text")
        Me.lblEichwert.Text = resources.GetString("lblEichwert.Text")
        Me.lblWIMaxCap.Text = resources.GetString("lblWIMaxCap.Text")
        Me.lblWIKlasse.Text = resources.GetString("lblWIKlasse.Text")
        Me.lblAWGKlasse2.Text = resources.GetString("lblAWGKlasse2.Text")
        Me.lblWZKlasse2.Text = resources.GetString("lblWZKlasse2.Text")
        Me.lblWaageKlasse2.Text = resources.GetString("lblWaageKlasse2.Text")
        Me.lblEichwert.Text = resources.GetString("lblEichwert.Text")
        Me.lblKabel.Text = resources.GetString("lblKabel.Text")
        'werden nicht übersetzt
        'Me.lblE3.Text = resources.GetString("lblE3.Text")
        'Me.lblMax3.Text = resources.GetString("lblMax3.Text")
        'Me.lblE2.Text = resources.GetString("lblE2.Text")
        'Me.lblMax2.Text = resources.GetString("lblMax2.Text")
        'Me.lblE1.Text = resources.GetString("lblE1.Text")
        'Me.lblMax1.Text = resources.GetString("lblMax1.Text")

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            Catch ex As Exception
            End Try
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso. Ergänzung: war ausdokumentiert, weil damit die Werte der NSW und WZ übeschrieben werden wenn man auf zurück klickt. Wenn es allerdings ausdokumenterit ist, funktioniert das anlegen einer neuen WZ nicht
        End If
    End Sub

    ''' <summary>
    ''' Event welches alle MouseHovers der Textboxen abfängt um den entsprechenden Hilfetext anzuzeigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlWaageKlasse_GotFocus(sender As Object, e As EventArgs) _
        Handles RadTextBoxControlWaageTotlast.GotFocus,
                RadTextBoxControlWaageAdditiveTarahoechstlast.GotFocus,
                RadTextBoxControlWaageTemperaturbereichMin.GotFocus,
                RadTextBoxControlWaageEcklastzuschlag.GotFocus,
                RadTextBoxControlEinschaltnullstellbereich.GotFocus,
                RadTextBoxControlWaageAnzahlWaegezellen.GotFocus,
                RadTextBoxControlWaageUebersetzungsverhaeltnis.GotFocus,
                RadTextBoxControlWaageEichwert3.GotFocus,
                RadTextBoxControlWaageHoechstlast3.GotFocus,
                RadTextBoxControlWaageEichwert2.GotFocus,
                RadTextBoxControlWaageHoechstlast2.GotFocus,
                RadTextBoxControlWaageEichwert1.GotFocus,
                RadTextBoxControlWaageHoechstlast1.GotFocus,
                RadTextBoxControlWaageKlasse.GotFocus,
                RadTextBoxControlWaageKabelquerschnitt.GotFocus,
                RadTextBoxControlWaageKabellaenge.GotFocus,
                RadTextBoxControlWZGenauigkeitsklasse.GotFocus,
                RadTextBoxControlWZWiderstand.GotFocus,
                RadTextBoxControlWZRueckkehrVorlastsignal.GotFocus,
                RadTextBoxControlWZMinTeilungswert.GotFocus,
                RadTextBoxControlWZHoechstteilungsfaktor.GotFocus,
                RadTextBoxControlWZKriechteilungsfaktor.GotFocus,
                RadTextBoxControlWZMaxTeilungswerte.GotFocus,
                RadTextBoxControlWZWaegezellenkennwert.GotFocus,
                RadTextBoxControlWZMindestvorlast.GotFocus,
                RadTextBoxControlWZHoechstlast.GotFocus,
                RadTextBoxControlAWGTemperaturbereichMax.GotFocus,
                RadTextBoxControlAWGKlasse.GotFocus,
                RadTextBoxControlAWGKabellaenge.GotFocus,
                RadTextBoxControlAWGAnschlussart.GotFocus,
                RadTextBoxControlAWGGrenzwerteLastwiderstandMax.GotFocus,
                RadTextBoxControlAWGGrenzwerteLastwiderstandMin.GotFocus,
                RadTextBoxControlAWGTemperaturbereichMin.GotFocus,
                RadTextBoxControlAWGBruchteilEichfehlergrenze.GotFocus,
                RadTextBoxControlAWGMindestmesssignal.GotFocus,
                RadTextBoxControlAWGMindesteingangsspannung.GotFocus,
                RadTextBoxControlAWGSpeisespannung.GotFocus,
                RadTextBoxControlAWGTeilungswerte.GotFocus,
                RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze.GotFocus,
                RadTextBoxControlWZTemperaturbereichMAX.GotFocus,
                RadTextBoxControlWZBruchteilEichfehlergrenze.GotFocus,
                RadTextBoxControlWZTemperaturbereichMIN.GotFocus,
                RadTextBoxControlWaageTotlast.MouseHover,
                RadTextBoxControlWaageAdditiveTarahoechstlast.MouseHover,
                RadTextBoxControlWaageTemperaturbereichMin.MouseHover,
                RadTextBoxControlWaageEcklastzuschlag.MouseHover,
                RadTextBoxControlEinschaltnullstellbereich.MouseHover,
                RadTextBoxControlWaageAnzahlWaegezellen.MouseHover,
                RadTextBoxControlWaageUebersetzungsverhaeltnis.MouseHover,
                RadTextBoxControlWaageEichwert3.MouseHover,
                RadTextBoxControlWaageHoechstlast3.MouseHover,
                RadTextBoxControlWaageEichwert2.MouseHover,
                RadTextBoxControlWaageHoechstlast2.MouseHover,
                RadTextBoxControlWaageEichwert1.MouseHover,
                RadTextBoxControlWaageHoechstlast1.MouseHover,
                RadTextBoxControlWaageKlasse.MouseHover,
                RadTextBoxControlWaageKabelquerschnitt.MouseHover,
                RadTextBoxControlWaageKabellaenge.MouseHover,
                RadTextBoxControlWZGenauigkeitsklasse.MouseHover,
                RadTextBoxControlWZWiderstand.MouseHover,
                RadTextBoxControlWZRueckkehrVorlastsignal.MouseHover,
                RadTextBoxControlWZMinTeilungswert.MouseHover,
                RadTextBoxControlWZHoechstteilungsfaktor.MouseHover,
                RadTextBoxControlWZKriechteilungsfaktor.MouseHover,
                RadTextBoxControlWZMaxTeilungswerte.MouseHover,
                RadTextBoxControlWZWaegezellenkennwert.MouseHover,
                RadTextBoxControlWZMindestvorlast.MouseHover,
                RadTextBoxControlWZHoechstlast.MouseHover,
                RadTextBoxControlAWGTemperaturbereichMax.MouseHover,
                RadTextBoxControlAWGKlasse.MouseHover,
                RadTextBoxControlAWGKabellaenge.MouseHover,
                RadTextBoxControlAWGAnschlussart.MouseHover,
                RadTextBoxControlAWGGrenzwerteLastwiderstandMax.MouseHover,
                RadTextBoxControlAWGGrenzwerteLastwiderstandMin.MouseHover,
                RadTextBoxControlAWGTemperaturbereichMin.MouseHover,
                RadTextBoxControlAWGBruchteilEichfehlergrenze.MouseHover,
                RadTextBoxControlAWGMindestmesssignal.MouseHover,
                RadTextBoxControlAWGMindesteingangsspannung.MouseHover,
                RadTextBoxControlAWGSpeisespannung.MouseHover,
                RadTextBoxControlAWGTeilungswerte.MouseHover,
                RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze.MouseHover,
                RadTextBoxControlWZTemperaturbereichMAX.MouseHover,
                RadTextBoxControlWZBruchteilEichfehlergrenze.MouseHover,
                RadTextBoxControlWZTemperaturbereichMIN.MouseHover

        Dim senderControl As Telerik.WinControls.UI.RadTextBox
        senderControl = TryCast(sender, Telerik.WinControls.UI.RadTextBox)

        If Not senderControl Is Nothing Then
            Select Case senderControl.Name

                Case Is = "RadTextBoxControlWaageTotlast"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageTotlast)
                Case Is = "RadTextBoxControlWaageAdditiveTarahoechstlast"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageAdditiveTarahoechstlast)

                Case Is = "RadTextBoxControlWaageTemperaturbereichMin"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageTemperaturbereichMin)

                Case Is = "RadTextBoxControlWaageEcklastzuschlag"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageEcklastzuschlag)

                Case Is = "RadTextBoxControlEinschaltnullstellbereich"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageEinschaltnullstellbereich)

                Case Is = "RadTextBoxControlWaageAnzahlWaegezellen"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageAnzahlWaegezellen)

                Case Is = "RadTextBoxControlWaageUebersetzungsverhaeltnis"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageUebersetzungsverhaeltnis)

                Case Is = "RadTextBoxControlWaageEichwert3"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageEichwert3)

                Case Is = "RadTextBoxControlWaageHoechstlast3"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageHoechstlast3)

                Case Is = "RadTextBoxControlWaageEichwert2"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageEichwert2)

                Case Is = "RadTextBoxControlWaageHoechstlast2"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageHoechstlast2)

                Case Is = "RadTextBoxControlWaageEichwert1"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageEichwert1)

                Case Is = "RadTextBoxControlWaageHoechstlast1"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageHoechstlast1)

                Case Is = "RadTextBoxControlWaageKlasse"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageKlasse)

                Case Is = "RadTextBoxControlWaageKabelquerschnitt"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageKabelquerschnitt)

                Case Is = "RadTextBoxControlWaageKabellaenge"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageKabellaenge)

                Case Is = "RadTextBoxControlWZGenauigkeitsklasse"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZGenauigkeitsklasse)

                Case Is = "RadTextBoxControlWZWiderstand"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZWiderstand)

                Case Is = "RadTextBoxControlWZRueckkehrVorlastsignal"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZRueckkehrVorlastsignal)

                Case Is = "RadTextBoxControlWZMinTeilungswert"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZMinTeilungswert)

                Case Is = "RadTextBoxControlWZHoechstteilungsfaktor"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZHoechstteilungsfaktor)

                Case Is = "RadTextBoxControlWZKriechteilungsfaktor"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZKriechteilungsfaktor)

                Case Is = "RadTextBoxControlWZMaxTeilungswerte"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZMaxTeilungswerte)

                Case Is = "RadTextBoxControlWZWaegezellenkennwert"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZWaegezellenkennwert)

                Case Is = "RadTextBoxControlWZMindestvorlast"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZMindestvorlast)

                Case Is = "RadTextBoxControlWZHoechstlast"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZHoechstlast)

                Case Is = "RadTextBoxControlAWGTemperaturbereichMax"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGTemperaturbereichMax)

                Case Is = "RadTextBoxControlAWGKlasse"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGKlasse)

                Case Is = "RadTextBoxControlAWGKabellaenge"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGKabellaenge)

                Case Is = "RadTextBoxControlAWGAnschlussart"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGAnschlussart)

                Case Is = "RadTextBoxControlAWGGrenzwerteLastwiderstandMax"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGGrenzwerteLastwiderstandMax)

                Case Is = "RadTextBoxControlAWGGrenzwerteLastwiderstandMin"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGGrenzwerteLastwiderstandMin)

                Case Is = "RadTextBoxControlAWGTemperaturbereichMin"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGTemperaturbereichMin)

                Case Is = "RadTextBoxControlAWGBruchteilEichfehlergrenze"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGBruchteilEichfehlergrenze)

                Case Is = "RadTextBoxControlAWGMindestmesssignal"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGMindestmesssignal)

                Case Is = "RadTextBoxControlAWGMindesteingangsspannung"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGMindesteingangsspannung)

                Case Is = "RadTextBoxControlAWGSpeisespannung"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGSpeisespannung)

                Case Is = "RadTextBoxControlAWGTeilungswerte"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_AWGTeilungswerte)

                Case Is = "RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_VerbindungselementeBruchteilEichfehlergrenze)

                Case Is = "RadTextBoxControlWZTemperaturbereichMAX"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZTemperaturbereichMAX)

                Case Is = "RadTextBoxControlWZBruchteilEichfehlergrenze"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZBruchteilEichfehlergrenze)

                Case Is = "RadTextBoxControlWZTemperaturbereichMIN"
                    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WZTemperaturbereichMIN)

            End Select
        End If

    End Sub

    ''' <summary>
    ''' event welches prüft ob in den eingabefeldern auch nur gültige Zahlen eingegeben wurden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlWaageHoechstlast1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RadTextBoxControlWZWiderstand.Validating, RadTextBoxControlWZWaegezellenkennwert.Validating, RadTextBoxControlWZTemperaturbereichMIN.Validating, RadTextBoxControlWZTemperaturbereichMAX.Validating, RadTextBoxControlWZRueckkehrVorlastsignal.Validating, RadTextBoxControlWZMinTeilungswert.Validating, RadTextBoxControlWZMindestvorlast.Validating, RadTextBoxControlWZMaxTeilungswerte.Validating, RadTextBoxControlWZKriechteilungsfaktor.Validating, RadTextBoxControlWZHoechstteilungsfaktor.Validating, RadTextBoxControlWZHoechstlast.Validating, RadTextBoxControlWZBruchteilEichfehlergrenze.Validating, RadTextBoxControlWaageUebersetzungsverhaeltnis.Validating, RadTextBoxControlWaageTotlast.Validating, RadTextBoxControlWaageKabelquerschnitt.Validating, RadTextBoxControlWaageKabellaenge.Validating, RadTextBoxControlWaageHoechstlast3.Validating, RadTextBoxControlWaageHoechstlast2.Validating, RadTextBoxControlWaageHoechstlast1.Validating, RadTextBoxControlWaageEichwert3.Validating, RadTextBoxControlWaageEichwert2.Validating, RadTextBoxControlWaageEichwert1.Validating, RadTextBoxControlWaageEcklastzuschlag.Validating, RadTextBoxControlWaageAnzahlWaegezellen.Validating, RadTextBoxControlEinschaltnullstellbereich.Validating, RadTextBoxControlAWGAnschlussart.Validating
        Dim result As Decimal
        If Not sender.readonly = True Then

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
                'prüfen ob negative zahlen eingegeben wurden
                If sender.text.ToString.Trim.StartsWith("-") Then
                    e.Cancel = True
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
                    System.Media.SystemSounds.Exclamation.Play()

                Else
                    CType(sender, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)

                End If

            End If
        End If

    End Sub

    ''' <summary>
    ''' Es dürfen nur kommas aber keine Punkte eingegeben werden. Sonst wird das mit der Lokalisiernug zu komplex.
    ''' In EN gibt es z.b. , als 1000er trennzeichen . als kommatrennzeichen
    ''' in DE gibtt es das genau anders herum
    ''' und in PL gibt es gar kein 1000er Trennzeichen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlWaageKlasse_TextChanging(sender As Object, e As Telerik.WinControls.TextChangingEventArgs) Handles RadTextBoxControlWZWiderstand.TextChanging, RadTextBoxControlWZWaegezellenkennwert.TextChanging,
        RadTextBoxControlWZTemperaturbereichMIN.TextChanging, RadTextBoxControlWZTemperaturbereichMAX.TextChanging, RadTextBoxControlWZRueckkehrVorlastsignal.TextChanging,
        RadTextBoxControlWZMinTeilungswert.TextChanging, RadTextBoxControlWZMindestvorlast.TextChanging, RadTextBoxControlWZMaxTeilungswerte.TextChanging,
        RadTextBoxControlWZKriechteilungsfaktor.TextChanging, RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.TextChanging, RadTextBoxControlWZHoechstteilungsfaktor.TextChanging, RadTextBoxControlWZHoechstlast.TextChanging,
        RadTextBoxControlWZBruchteilEichfehlergrenze.TextChanging, RadTextBoxControlWaageUebersetzungsverhaeltnis.TextChanging,
        RadTextBoxControlWaageTotlast.TextChanging, RadTextBoxControlWaageTemperaturbereichMin.TextChanging, RadTextBoxControlWaageTemperaturbereichMax.TextChanging,
        RadTextBoxControlWaageKlasse.TextChanging, RadTextBoxControlWaageKabelquerschnitt.TextChanging, RadTextBoxControlWaageKabellaenge.TextChanging,
        RadTextBoxControlWaageHoechstlast3.TextChanging, RadTextBoxControlWaageHoechstlast2.TextChanging, RadTextBoxControlWaageHoechstlast1.TextChanging,
        RadTextBoxControlWaageEichwert3.TextChanging, RadTextBoxControlWaageEichwert2.TextChanging, RadTextBoxControlWaageEichwert1.TextChanging,
        RadTextBoxControlWaageEcklastzuschlag.TextChanging, RadTextBoxControlWaageAnzahlWaegezellen.TextChanging, RadTextBoxControlWaageAdditiveTarahoechstlast.TextChanging,
        RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze.TextChanging, RadTextBoxControlEinschaltnullstellbereich.TextChanging, RadTextBoxControlAWGTemperaturbereichMin.TextChanging,
        RadTextBoxControlAWGTemperaturbereichMax.TextChanging, RadTextBoxControlAWGTeilungswerte.TextChanging, RadTextBoxControlAWGSpeisespannung.TextChanging,
        RadTextBoxControlAWGMindestmesssignal.TextChanging, RadTextBoxControlAWGMindesteingangsspannung.TextChanging, RadTextBoxControlAWGKlasse.TextChanging,
        RadTextBoxControlAWGKabellaenge.TextChanging, RadTextBoxControlAWGGrenzwerteLastwiderstandMin.TextChanging, RadTextBoxControlAWGGrenzwerteLastwiderstandMax.TextChanging,
        RadTextBoxControlAWGBruchteilEichfehlergrenze.TextChanging, RadTextBoxControlAWGAnschlussart.TextChanging

        If _suspendEvents = True Then Exit Sub

        If e.NewValue.Contains(".") Then
            e.NewValue = e.NewValue.Replace(".", "")
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' wenn an den Textboxen etwas geändert wurde, ist das objekt als Dirty zu markieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlWaageKlasse_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlWZWiderstand.TextChanged, RadTextBoxControlWZWaegezellenkennwert.TextChanged, RadTextBoxControlWZTemperaturbereichMIN.TextChanged, RadTextBoxControlWZTemperaturbereichMAX.TextChanged, RadTextBoxControlWZRueckkehrVorlastsignal.TextChanged, RadTextBoxControlWZMinTeilungswert.TextChanged, RadTextBoxControlWZMindestvorlast.TextChanged, RadTextBoxControlWZMaxTeilungswerte.TextChanged, RadTextBoxControlWZKriechteilungsfaktor.TextChanged, RadTextBoxControlWZHoechstteilungsfaktor.TextChanged, RadTextBoxControlWZHoechstlast.TextChanged, RadTextBoxControlWZBruchteilEichfehlergrenze.TextChanged, RadTextBoxControlWaageUebersetzungsverhaeltnis.TextChanged, RadTextBoxControlWaageTotlast.TextChanged, RadTextBoxControlWaageTemperaturbereichMin.TextChanged, RadTextBoxControlWaageTemperaturbereichMax.TextChanged, RadTextBoxControlWaageKlasse.TextChanged, RadTextBoxControlWaageKabelquerschnitt.TextChanged, RadTextBoxControlWaageKabellaenge.TextChanged, RadTextBoxControlWaageHoechstlast3.TextChanged, RadTextBoxControlWaageHoechstlast2.TextChanged, RadTextBoxControlWaageHoechstlast1.TextChanged, RadTextBoxControlWaageEichwert3.TextChanged, RadTextBoxControlWaageEichwert2.TextChanged, RadTextBoxControlWaageEichwert1.TextChanged, RadTextBoxControlWaageEcklastzuschlag.TextChanged, RadTextBoxControlWaageAnzahlWaegezellen.TextChanged, RadTextBoxControlWaageAdditiveTarahoechstlast.TextChanged, RadTextBoxControlVerbindungselementeBruchteilEichfehlergrenze.TextChanged, RadTextBoxControlEinschaltnullstellbereich.TextChanged, RadTextBoxControlAWGTemperaturbereichMin.TextChanged, RadTextBoxControlAWGTemperaturbereichMax.TextChanged, RadTextBoxControlAWGTeilungswerte.TextChanged, RadTextBoxControlAWGSpeisespannung.TextChanged, RadTextBoxControlAWGMindestmesssignal.TextChanged, RadTextBoxControlAWGMindesteingangsspannung.TextChanged, RadTextBoxControlAWGKlasse.TextChanged, RadTextBoxControlAWGKabellaenge.TextChanged, RadTextBoxControlAWGGrenzwerteLastwiderstandMin.TextChanged, RadTextBoxControlAWGGrenzwerteLastwiderstandMax.TextChanged, RadTextBoxControlAWGBruchteilEichfehlergrenze.TextChanged, RadTextBoxControlAWGAnschlussart.TextChanged, RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True

        If sender.name = RadTextBoxControlWZHoechstlast.Name Then
            If IsNumeric(RadTextBoxControlWZHoechstlast.Text) Then
                If Not objEichprozess Is Nothing Then
                    If Not objEichprozess.Lookup_Waegezelle Is Nothing Then
                        Try
                            If Not objEichprozess.Lookup_Waegezelle.MindestvorlastProzent Is Nothing Then
                                RadTextBoxControlWZMindestvorlast.Text = (objEichprozess.Lookup_Waegezelle.MindestvorlastProzent / 100) * RadTextBoxControlWZHoechstlast.Text

                            End If
                        Catch ex As Exception
                        End Try
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub RadTextBoxControlWZGenauigkeitsklasse_TextChanging(sender As Object, e As Telerik.WinControls.TextChangingEventArgs) Handles RadTextBoxControlWZGenauigkeitsklasse.TextChanging
        If _suspendEvents = True Then
            e.Cancel = False
            Exit Sub
        End If

        If e.NewValue.ToUpper = "A" Or e.NewValue.ToUpper = "B" Or e.NewValue.ToUpper = "C" Or e.NewValue.ToUpper = "D" _
            Or e.NewValue.ToUpper = "I" Or e.NewValue.ToUpper = "II" Or e.NewValue.ToUpper = "III" Or e.NewValue.ToUpper = "IV" _
            Or e.NewValue.ToUpper = "" Then
            e.Cancel = False
        Else

            e.Cancel = True
        End If
    End Sub

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadGroupBoxAWG)
        EnableControls(RadGroupBoxVerbindungselemente)
        EnableControls(RadGroupBoxWaage)
        EnableControls(RadGroupBoxWZ)

        'ändern des Moduses
        DialogModus = enuDialogModus.korrigierend
        ParentFormular.DialogModus = FrmMainContainer.enuDialogModus.korrigierend
    End Sub

    Protected Overrides Sub VersendenNeeded(TargetUserControl As UserControl)

        If Me.Equals(TargetUserControl) Then
            MyBase.VersendenNeeded(TargetUserControl)

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

                Try
                    'add prüft anhand der Vorgangsnummer automatisch ob ein neuer Prozess angelegt, oder ein vorhandener aktualisiert wird
                    Webcontext.AddEichprozess(AktuellerBenutzer.Instance.Lizenz.HEKennung, AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name, Version)

                    'schließen des dialoges
                    ParentFormular.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' Status zurück setzen
                    Exit Sub
                End Try
            End Using

        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        RadTextBoxControlWZHoechstteilungsfaktorAufgedruckt.ReadOnly = False
    End Sub
End Class