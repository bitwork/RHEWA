Public Class uco_7EichprotokollDaten
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    'Private AktuellerStatusDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
    Private _objEichprotokoll As Eichprotokoll
    'Private _objDBFunctions As New clsDBFunctions
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

        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten

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
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_EichprotokollStammdaten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_EichprotokollStammdaten
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten

        'daten füllen
        LoadFromDatabase()
    End Sub

    ''' <summary>
    ''' wenn an den Textboxen etwas geändert wurde, ist das objekt als Dirty zu markieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlBenutzer_TextChanged(sender As Object, e As EventArgs) Handles RadTextBoxControlWZFabriknummer.TextChanged, RadTextBoxControlSoftwarestand.TextChanged, RadTextBoxControlNormalienPruefscheinnummer.TextChanged, RadTextBoxControlNormalienPruefintervall.TextChanged, RadTextBoxControlNormalienGenauigkeitsklasse.TextChanged, RadTextBoxControlNormalienEichfahrzeugFirma.TextChanged, RadTextBoxControlMxM.TextChanged, RadTextBoxControlMin3.TextChanged, RadTextBoxControlMin2.TextChanged, RadTextBoxControlMin1.TextChanged, RadTextBoxControlEichzaehlerstand.TextChanged, RadTextBoxControlBetragNormallast.TextChanged, RadTextBoxControlBenutzer.TextChanged, RadTextBoxControlBaujahr.TextChanged, RadTextBoxControlAufstellungsort.TextChanged
        If _suspendEvents = True Then Exit Sub
        AktuellerStatusDirty = True
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
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Eichprotokoll") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
                _objEichprotokoll = objEichprozess.Eichprotokoll

                Dim Coll As New AutoCompleteStringCollection
                Coll.AddRange((From o In context.Eichprotokoll Select o.Verwendungszweck_Druckertyp).Distinct.ToArray)
                RadTextBoxControlDruckerTyp.AutoCompleteDataSource = Coll
                RadTextBoxControlDruckerTyp.DataSource = Coll
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            DisableControls(RadGroupBoxBeschaffenheitspruefung)
            DisableControls(RadGroupBoxBeschaffenheitspruefungNormalien)
            DisableControls(RadGroupBoxIdentifikationsdaten)
            DisableControls(RadGroupBoxKomponenten)
            DisableControls(RadGroupBoxMax1)
            DisableControls(RadGroupBoxMax2)
            DisableControls(RadGroupBoxMax3)
            DisableControls(RadGroupBoxPruefverfahren)
            DisableControls(RadGroupBoxVerwendungszweck)
            DisableControls(RadGroupBoxVerwendungszweckArtderWaage)
            DisableControls(RadGroupBoxVerwendungszweckEquipment)
            DisableControls(RadGroupBoxVerwendungszweckNullstellung)
            DisableControls(RadGroupBoxVerwendungszweckTara)

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

        If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then
            If objEichprozess.AusStandardwaageErzeugt = True Then
                lblTruck.Visible = False
                RadTextBoxControlNormalienEichfahrzeugFirma.Visible = False
                RadDateTimePickerNormalienLetztePruefung.Visible = False
                RadDateTimePickerNormalienLetztePruefung.Value = Now
                lblPruefzeitraum.Visible = False
            End If
        End If
        Dim dMAXHoechlast As Decimal 'variable zum speichern der höchsten Hoechstlast (je nach Art der Waage Max1,2 oder 3)

        'Steuerlemente füllen

        'Bereich Identifikationsdaten
        If Not objEichprozess.Eichprotokoll.Identifikationsdaten_Benutzer Is Nothing Then
            RadTextBoxControlBenutzer.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Benutzer
        End If

        If Not objEichprozess.Eichprotokoll.Identifikationsdaten_Aufstellungsort Is Nothing Then
            RadTextBoxControlAufstellungsort.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Aufstellungsort
        End If

        If Not objEichprozess.Eichprotokoll.Identifikationsdaten_Baujahr Is Nothing Then
            RadTextBoxControlBaujahr.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Baujahr
        End If

        If objEichprozess.Eichprotokoll.Identifikationsdaten_Datum Is Nothing Then
            RadTextBoxControlDatum.Text = Date.Now.Date
        Else
            RadTextBoxControlDatum.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Datum
        End If

        'Stammdaten aus lokaler Lizenz laden
        RadTextBoxControlPruefer.Text = AktuellerBenutzer.Instance.Lizenz.Name & ", " & AktuellerBenutzer.Instance.Lizenz.Vorname & " (" + AktuellerBenutzer.Instance.Lizenz.HEKennung & ")"

        RadTextBoxControlFabriknummer.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

        If objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" OrElse objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" Then
            RadCheckBoxMehrbereichswaage.Checked = True
            RadCheckBoxMehrteilungswaage.Checked = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" OrElse objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            RadCheckBoxMehrbereichswaage.Checked = False
            RadCheckBoxMehrteilungswaage.Checked = True
        Else
            RadCheckBoxMehrbereichswaage.Checked = False
            RadCheckBoxMehrteilungswaage.Checked = False
        End If

        Select Case objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse.ToUpper
            Case "I"
                PictureBoxGenauigkeitsklasse.Image = My.Resources.Genauigkeitsklasse1
            Case "II"
                PictureBoxGenauigkeitsklasse.Image = My.Resources.Genauigkeitsklasse2
            Case "III"
                PictureBoxGenauigkeitsklasse.Image = My.Resources.Genauigkeitsklasse3
            Case "IV"
                PictureBoxGenauigkeitsklasse.Image = My.Resources.Genauigkeitsklasse4
        End Select

        'standardmäßig TRUE
        RadCheckBoxHalbSelbsteinspielend.Checked = True
        'standardmäßig False
        RadCheckBoxNichtselbsteinspielend.Checked = False
        Try
            RadCheckBoxHybridMechWaage.Checked = objEichprozess.Eichprotokoll.Identifikationsdaten_HybridMechanisch
        Catch ex As Exception
        End Try
        Try
            RadTextBoxControl1Hoechstwert1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1

            If objEichprozess.Eichprotokoll.Identifikationsdaten_Min1 Is Nothing Then
                'berechnen 20*e
                RadTextBoxControlMin1.Text = 20 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
            Else
                RadTextBoxControlMin1.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min1
            End If
            RadTextBoxControlEichwert1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        Catch ex As Exception
        End Try

        Try
            RadTextBoxControl1Hoechstwert2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
            '   RadTextBoxControlMin2.Text = 20 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

            If objEichprozess.Eichprotokoll.Identifikationsdaten_Min2 Is Nothing Then
                'berechnen 20*e
                RadTextBoxControlMin2.Text = 20 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
            Else
                RadTextBoxControlMin2.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min2
            End If
            RadTextBoxControlEichwert2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        Catch ex As Exception
        End Try
        Try
            RadTextBoxControl1Hoechstwert3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3

            ' RadTextBoxControlMin3.Text = 20 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            If objEichprozess.Eichprotokoll.Identifikationsdaten_Min3 Is Nothing Then
                'berechnen 20*e
                RadTextBoxControlMin3.Text = 20 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
            Else
                RadTextBoxControlMin3.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Min3
            End If
            RadTextBoxControlEichwert3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        Catch ex As Exception
        End Try
        'bereich prüfverfahren

        'volle normallast und Bertrag normallast herausfinden: wenn höchster MAX Wert unter 1000 dann "ja". darüber auswählbar
        'höchstenMAX Wert auslesen
        If objEichprozess.Lookup_Waagenart.Art = "Einbereichswaage" Then
            dMAXHoechlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1

            'hoechstwerte ausblenden
            RadGroupBoxMax2.Visible = False
            RadGroupBoxMax3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Zweibereichswaage" OrElse objEichprozess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
            dMAXHoechlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
            'hoechstwerte ausblenden
            RadGroupBoxMax2.Visible = True
            RadGroupBoxMax3.Visible = False
        ElseIf objEichprozess.Lookup_Waagenart.Art = "Dreibereichswaage" OrElse objEichprozess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
            dMAXHoechlast = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
            'hoechstwerte ausblenden
            RadGroupBoxMax2.Visible = True
            RadGroupBoxMax3.Visible = True

        End If

        'volle normallast: wenn höchster MAX Wert unter 1000 dann "ja". darüber auswählbar
        'betrag normallast: Höchster MAX Wert. Wenn über 1000 dann eingebbar
        If objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast Is Nothing Then 'prüfen ob bereits ein Wert eingegeben wurde
            Label12.Visible = False
            If dMAXHoechlast < 1000 Then
                RadCheckBoxVolleNormallast.Checked = True
                RadTextBoxControlBetragNormallast.Text = dMAXHoechlast
            Else
                If Not objEichprozess.Eichprotokoll.Pruefverfahren_VolleNormallast Is Nothing Then
                    RadCheckBoxVolleNormallast.Checked = objEichprozess.Eichprotokoll.Pruefverfahren_VolleNormallast
                End If
                RadCheckBoxVolleNormallast.Enabled = True
                RadTextBoxControlBetragNormallast.Text = dMAXHoechlast
            End If

            If objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren Then
                RadTextBoxControlBetragNormallast.Text = ""
                Label12.Visible = True
            End If
        Else 'WErte übernehmen aus DB
            If Not objEichprozess.Eichprotokoll.Pruefverfahren_VolleNormallast Is Nothing Then
                RadCheckBoxVolleNormallast.Checked = objEichprozess.Eichprotokoll.Pruefverfahren_VolleNormallast
            End If
            RadTextBoxControlBetragNormallast.Text = objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast

        End If

        Select Case objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien
                'vollständiges Staffelverfahren NEIN
                RadCheckBoxVolleNormallast.Checked = True
                RadCheckBoxVolleNormallast.Enabled = True
                RadCheckBoxVollstaendigesStaffelverfahren.Checked = False
                RadCheckBoxVollstaendigesStaffelverfahren.Visible = False
                PictureBox5.Visible = False

            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
                'vollständiges Staffelverfahren Ja
                RadCheckBoxVolleNormallast.Checked = False
                RadCheckBoxVolleNormallast.Enabled = False
                RadCheckBoxVollstaendigesStaffelverfahren.Checked = True
                RadCheckBoxVollstaendigesStaffelverfahren.Visible = True
                PictureBox5.Visible = True
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen
                'vollständiges Staffelverfahren Ja
                RadCheckBoxVolleNormallast.Checked = False
                RadCheckBoxVolleNormallast.Enabled = False
                RadCheckBoxVollstaendigesStaffelverfahren.Checked = True
                RadCheckBoxVollstaendigesStaffelverfahren.Visible = True
                PictureBox5.Visible = True

        End Select

        'Sonderfall durch Herrn Strack definiert:
        If dMAXHoechlast < 1000 Then
            RadCheckBoxVolleNormallast.Checked = True
            RadCheckBoxVolleNormallast.Enabled = False
            RadCheckBoxVolleNormallast.ReadOnly = True
            RadTextBoxControlBetragNormallast.Enabled = False
        Else
            RadCheckBoxVolleNormallast.Enabled = True
            RadTextBoxControlBetragNormallast.Enabled = True
        End If

        'bereich Komponenten
        RadTextBoxControlAWG.Text = objEichprozess.Lookup_Auswertegeraet.Typ
        RadTextBoxControlSoftwarestand.Text = objEichprozess.Eichprotokoll.Komponenten_Softwarestand
        RadTextBoxControlEichzaehlerstand.Text = objEichprozess.Eichprotokoll.Komponenten_Eichzaehlerstand

        RadTextBoxControlWZHersteller.Text = objEichprozess.Lookup_Waegezelle.Hersteller
        RadTextBoxControlWZTyp.Text = objEichprozess.Lookup_Waegezelle.Typ
        RadTextBoxControlWZAnzahl.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        RadTextBoxControlWZFabriknummer.Text = objEichprozess.Eichprotokoll.Komponenten_WaegezellenFabriknummer

        'bereich Verwendungszweck
        RadTextBoxControlWaagentyp.Text = objEichprozess.Lookup_Waagentyp.Typ

        Select Case objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgmitNormalien, GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren
                RadTextBoxControlMxM.Visible = False
                RadTextBoxControlDimension.Visible = False
                RadLabel38.Visible = False
                Label7.Visible = False
                lblDimension.Visible = False
                PictureBox13.Visible = False
            Case Is = GlobaleEnumeratoren.enuVerfahrensauswahl.Fahrzeugwaagen
                RadTextBoxControlMxM.Text = objEichprozess.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM
                RadTextBoxControlMxM.Visible = True
                Label7.Visible = True
                RadTextBoxControlMxM.ReadOnly = False
                RadTextBoxControlDimension.Text = My.Resources.GlobaleLokalisierung.Eichprotokoll_Dimension
                RadTextBoxControlDimension.ReadOnly = True
                lblDimension.Visible = True
                RadLabel38.Visible = True
                PictureBox13.Visible = True
        End Select

        'validieren ob die Werte bereits einmal geschrieben wurden. Wenn nicht, Standardwerte aus AWG laden.
        If objEichprozess.Eichprotokoll.Verwendungszweck_Automatisch Is Nothing Then
            Try
                RadRadioButtonNustellungHalbAutomatisch.IsChecked = objEichprozess.Lookup_Auswertegeraet.NullstellungHalbSelbsttaetig
                RadRadioButtonNustellungAutomatisch.IsChecked = objEichprozess.Lookup_Auswertegeraet.NullstellungSelbsttaetig
                RadRadioButtonNustellungNullNachfuehrung.IsChecked = objEichprozess.Lookup_Auswertegeraet.NullstellungNullnachfuehrung

                RadRadioButtonHandTara.IsChecked = objEichprozess.Lookup_Auswertegeraet.TaraeinrichtungHalbSelbsttaetig
                RadRadioButtonAutoTara.IsChecked = objEichprozess.Lookup_Auswertegeraet.TaraeinrichtungSelbsttaetig
                RadRadioButtonTaraeingabe.IsChecked = objEichprozess.Lookup_Auswertegeraet.TaraeinrichtungTaraeingabe
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Else 'werte aus DB Laden
            If objEichprozess.Eichprotokoll.Verwendungszweck_Automatisch = True Then
                RadRadioButtonNustellungAutomatisch.IsChecked = True
            End If
            If objEichprozess.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = True Then
                RadRadioButtonNustellungNullNachfuehrung.IsChecked = True
            End If
            If objEichprozess.Eichprotokoll.Verwendungszweck_HalbAutomatisch Then
                RadRadioButtonNustellungHalbAutomatisch.IsChecked = True
            End If

            If objEichprozess.Eichprotokoll.Verwendungszweck_AutoTara Then
                RadRadioButtonAutoTara.IsChecked = True
            End If
            If objEichprozess.Eichprotokoll.Verwendungszweck_HandTara Then
                RadRadioButtonHandTara.IsChecked = True
            End If

            If objEichprozess.Eichprotokoll.Taraeinrichtung_Taraeingabe Then
                RadRadioButtonTaraeingabe.IsChecked = True
            End If
        End If

        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse Is Nothing Then RadTextBoxControlNormalienGenauigkeitsklasse.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall Is Nothing Then RadTextBoxControlNormalienPruefintervall.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung Is Nothing Then RadDateTimePickerNormalienLetztePruefung.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer Is Nothing Then RadTextBoxControlNormalienPruefscheinnummer.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma Is Nothing Then RadTextBoxControlNormalienEichfahrzeugFirma.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma

        Try
            RadCheckBoxDrucker.Checked = objEichprozess.Eichprotokoll.Verwendungszweck_Drucker
        Catch ex As Exception
        End Try
        RadTextBoxControlDruckerTyp.Text = objEichprozess.Eichprotokoll.Verwendungszweck_Druckertyp

        If RadCheckBoxDrucker.Checked Then
            RadTextBoxControlDruckerTyp.Enabled = True
        Else
            RadTextBoxControlDruckerTyp.Text = ""
            RadTextBoxControlDruckerTyp.Enabled = False
        End If
        Try
            RadCheckBoxEichfaehigerSpeicher.Checked = objEichprozess.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher
        Catch ex As Exception
        End Try
        Try
            RadCheckBoxPC.Checked = objEichprozess.Eichprotokoll.Verwendungszweck_PC
        Catch ex As Exception
        End Try
        Try
            RadCheckBoxSonstiges.Checked = objEichprozess.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes
        Catch ex As Exception
        End Try

        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlBenutzer.Focus()

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now
        'Bereich Identifikationsdaten
        objEichprozess.Eichprotokoll.Identifikationsdaten_Datum = RadTextBoxControlDatum.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Pruefer = RadTextBoxControlPruefer.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Benutzer = RadTextBoxControlBenutzer.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Aufstellungsort = RadTextBoxControlAufstellungsort.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Min1 = RadTextBoxControlMin1.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Min2 = RadTextBoxControlMin2.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Min3 = RadTextBoxControlMin3.Text
        objEichprozess.Eichprotokoll.Identifikationsdaten_Selbsteinspielend = RadCheckBoxHalbSelbsteinspielend.Checked
        objEichprozess.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend = RadCheckBoxNichtselbsteinspielend.Checked
        objEichprozess.Eichprotokoll.Identifikationsdaten_HybridMechanisch = RadCheckBoxHybridMechWaage.Checked
        objEichprozess.Eichprotokoll.Identifikationsdaten_Baujahr = RadTextBoxControlBaujahr.Text
        'bereich prüfverfahren

        objEichprozess.Eichprotokoll.Pruefverfahren_VolleNormallast = RadCheckBoxVolleNormallast.Checked
        objEichprozess.Eichprotokoll.Pruefverfahren_BetragNormallast = RadTextBoxControlBetragNormallast.Text
        objEichprozess.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren = RadCheckBoxVollstaendigesStaffelverfahren.Visible
        objEichprozess.Eichprotokoll.Komponenten_Softwarestand = RadTextBoxControlSoftwarestand.Text
        objEichprozess.Eichprotokoll.Komponenten_Eichzaehlerstand = RadTextBoxControlEichzaehlerstand.Text
        objEichprozess.Eichprotokoll.Komponenten_WaegezellenFabriknummer = RadTextBoxControlWZFabriknummer.Text
        'bereich Verwendungszweck
        objEichprozess.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM = RadTextBoxControlMxM.Text
        objEichprozess.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension = RadTextBoxControlDimension.Text
        objEichprozess.Eichprotokoll.Verwendungszweck_Automatisch = RadRadioButtonNustellungAutomatisch.IsChecked
        objEichprozess.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = RadRadioButtonNustellungNullNachfuehrung.IsChecked
        objEichprozess.Eichprotokoll.Verwendungszweck_HalbAutomatisch = RadRadioButtonNustellungHalbAutomatisch.IsChecked
        objEichprozess.Eichprotokoll.Verwendungszweck_AutoTara = RadRadioButtonAutoTara.IsChecked
        objEichprozess.Eichprotokoll.Verwendungszweck_HandTara = RadRadioButtonHandTara.IsChecked
        objEichprozess.Eichprotokoll.Taraeinrichtung_Taraeingabe = RadRadioButtonTaraeingabe.IsChecked

        objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = RadCheckBoxDrucker.Checked
        objEichprozess.Eichprotokoll.Verwendungszweck_Druckertyp = RadTextBoxControlDruckerTyp.Text
        objEichprozess.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher = RadCheckBoxEichfaehigerSpeicher.Checked
        objEichprozess.Eichprotokoll.Verwendungszweck_PC = RadCheckBoxPC.Checked
        objEichprozess.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes = RadCheckBoxSonstiges.Checked

        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse = RadTextBoxControlNormalienGenauigkeitsklasse.Text
        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall = RadTextBoxControlNormalienPruefintervall.Text
        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = RadDateTimePickerNormalienLetztePruefung.Text
        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = RadTextBoxControlNormalienPruefscheinnummer.Text
        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = RadTextBoxControlNormalienEichfahrzeugFirma.Text

        objEichprozess.Eichprotokoll.Identifikationsdaten_HybridMechanisch = RadCheckBoxHybridMechWaage.Checked

    End Sub

    Private Sub MarkControlRed(ByVal control As Control)
        Try
            CType(control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.Red
            System.Media.SystemSounds.Exclamation.Play()
        Catch e As Exception
        End Try

        Try
            CType(control, Telerik.WinControls.UI.RadDateTimePicker).DateTimePickerElement.TextBoxElement.Border.ForeColor = Color.Red
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MarkControlNormal(ByVal control As Control)
        Try
            CType(control, Telerik.WinControls.UI.RadTextBox).TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
        Catch ex As Exception
        End Try

        Try
            CType(control, Telerik.WinControls.UI.RadDateTimePicker).DateTimePickerElement.TextBoxElement.Border.ForeColor = Color.FromArgb(0, 255, 255, 255)
        Catch ex As Exception
        End Try
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
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadTextBox Then
                        If CType(Control, Telerik.WinControls.UI.RadTextBox).Visible = True AndAlso CType(Control, Telerik.WinControls.UI.RadTextBox).ReadOnly = False Then
                            If Control.text.Equals("") Then
                                Control.focus()
                                AbortSaving = True
                                MarkControlRed(Control)
                            Else
                                MarkControlNormal(Control)
                            End If
                        End If
                    End If
                    If TypeOf Control Is Telerik.WinControls.UI.RadDateTimePicker Then
                        If CType(Control, Telerik.WinControls.UI.RadDateTimePicker).Visible = True Then
                            If CType(Control, Telerik.WinControls.UI.RadDateTimePicker).Text.Equals(CType(Control, Telerik.WinControls.UI.RadDateTimePicker).NullText) Then
                                Control.focus()
                                AbortSaving = True
                            End If
                        End If
                    End If
                    If TypeOf Control Is Telerik.WinControls.UI.RadGroupBox Then
                        For Each Control2 In Control.controls

                            If TypeOf Control2 Is Telerik.WinControls.UI.RadTextBox Then
                                If CType(Control2, Telerik.WinControls.UI.RadTextBox).Visible = True AndAlso CType(Control2, Telerik.WinControls.UI.RadTextBox).ReadOnly = False Then
                                    If Control2.text.Equals("") Then

                                        'sonderfall Eichfahrzeug
                                        If Control2.Name.Equals(RadTextBoxControlNormalienEichfahrzeugFirma.Name) Then
                                            Continue For
                                        End If

                                        'sonderfall drucker
                                        If Control2.Name.Equals(RadTextBoxControlDruckerTyp.Name) Then
                                            Continue For
                                        End If

                                        Control2.focus()
                                        AbortSaving = True
                                        MarkControlRed(Control2)
                                    Else
                                        MarkControlNormal(Control2)
                                    End If
                                End If
                            End If
                            If TypeOf Control2 Is Telerik.WinControls.UI.RadDateTimePicker Then
                                If CType(Control2, Telerik.WinControls.UI.RadDateTimePicker).Visible = True Then
                                    If CType(Control2, Telerik.WinControls.UI.RadDateTimePicker).Text.Equals(CType(Control2, Telerik.WinControls.UI.RadDateTimePicker).NullText) Then
                                        Control2.focus()
                                        AbortSaving = True
                                    End If
                                End If
                            End If

                        Next
                    End If
                Next
            End If
        Next

        If RadCheckBoxDrucker.Checked Then
            If RadTextBoxControlDruckerTyp.Text = "" Then
                AbortSaving = True
            End If
        End If

        'fehlermeldung anzeigen bei falscher validierung
        Return Me.ShowValidationErrorBox(True)

    End Function

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
            If DialogModus = enuDialogModus.korrigierend Then
                UpdateObject()
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
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
                                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                                End If
                            ElseIf AktuellerStatusDirty = True Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
                                AktuellerStatusDirty = False
                            End If

                            'Füllt das Objekt mit den Werten aus den Steuerlementen
                            UpdateObject()
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

    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_7EichprotokollDaten))

        lblAnzahlWZ.Text = resources.GetString("lblAnzahlWZ.Text")
        lblArtWaage.Text = resources.GetString("lblArtWaage.Text")
        lblAWG.Text = resources.GetString("lblAWG.Text")
        lblBenutzer.Text = resources.GetString("lblBenutzer.Text")
        lblDatum.Text = resources.GetString("lblDatum.Text")
        lblDimension.Text = resources.GetString("lblDimension.Text")
        lblDruckerart.Text = resources.GetString("lblDruckerart.Text")
        lblFabriknummer.Text = resources.GetString("lblFabriknummer.Text")
        lblFabriknummernWZ.Text = resources.GetString("lblFabriknummernWZ.Text")
        lblGenauigkeitsklasse.Text = resources.GetString("lblGenauigkeitsklasse.Text")
        lblGenauigkeitsklasse2.Text = resources.GetString("lblGenauigkeitsklasse2.Text")
        lblHerstellerWZ.Text = resources.GetString("lblHerstellerWZ.Text")
        lblHerstellungsjahr.Text = resources.GetString("lblHerstellungsjahr.Text")
        lblNormalienGewichte.Text = resources.GetString("lblNormalienGewichte.Text")
        lblOrt.Text = resources.GetString("lblOrt.Text")
        lblPruefer.Text = resources.GetString("lblPruefer.Text")
        lblPruefzeitraum.Text = resources.GetString("lblPruefzeitraum.Text")
        lblSoftwareversion.Text = resources.GetString("lblSoftwareversion.Text")
        lblTestzeitraum.Text = resources.GetString("lblTestzeitraum.Text")
        lblTruck.Text = resources.GetString("lblTruck.Text")
        lblTyp.Text = resources.GetString("lblTyp.Text")
        lblZeichen.Text = resources.GetString("lblZeichen.Text")
        lblZertifikatnr.Text = resources.GetString("lblZertifikatnr.Text")
        RadRadioButtonAutoTara.Text = resources.GetString("RadRadioButtonAutoTara.Text")
        RadRadioButtonHandTara.Text = resources.GetString("RadRadioButtonHandTara.Text")
        RadRadioButtonTaraeingabe.Text = resources.GetString("RadRadioButtonTaraeingabe.Text")

        RadRadioButtonNustellungAutomatisch.Text = resources.GetString("RadRadioButtonNustellungAutomatisch.Text")
        RadRadioButtonNustellungHalbAutomatisch.Text = resources.GetString("RadRadioButtonNustellungHalbAutomatisch.Text")
        RadRadioButtonNustellungNullNachfuehrung.Text = resources.GetString("RadRadioButtonNustellungNullNachfuehrung.Text")
        RadCheckBoxDrucker.Text = resources.GetString("RadCheckBoxDrucker.Text")
        RadCheckBoxEichfaehigerSpeicher.Text = resources.GetString("RadCheckBoxEichfaehigerSpeicher.Text")
        RadCheckBoxHalbSelbsteinspielend.Text = resources.GetString("RadCheckBoxHalbSelbsteinspielend.Text")
        RadCheckBoxHybridMechWaage.Text = resources.GetString("RadCheckBoxHybridMechWaage.Text")
        RadCheckBoxMehrbereichswaage.Text = resources.GetString("RadCheckBoxMehrbereichswaage.Text")
        RadCheckBoxMehrteilungswaage.Text = resources.GetString("RadCheckBoxMehrteilungswaage.Text")
        RadCheckBoxNichtselbsteinspielend.Text = resources.GetString("RadCheckBoxNichtselbsteinspielend.Text")
        RadCheckBoxPC.Text = resources.GetString("RadCheckBoxPC.Text")
        RadCheckBoxSonstiges.Text = resources.GetString("RadCheckBoxSonstiges.Text")
        RadCheckBoxVolleNormallast.Text = resources.GetString("RadCheckBoxVolleNormallast.Text")
        RadCheckBoxVollstaendigesStaffelverfahren.Text = resources.GetString("RadCheckBoxVollstaendigesStaffelverfahren.Text")
        RadDateTimePickerNormalienLetztePruefung.Text = resources.GetString("RadDateTimePickerNormalienLetztePruefung.Text")

        RadTextBoxControlNormalienGenauigkeitsklasse.Text = resources.GetString("RadTextBoxControlNormalienGenauigkeitsklasse.Text")
        RadTextBoxControlNormalienPruefintervall.Text = resources.GetString("RadTextBoxControlNormalienPruefintervall.Text")

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_EichprotokollStammdaten)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_EichprotokollStammdaten
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
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_EichprotokollStammdaten)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_EichprotokollStammdaten
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Diese Checkboxen (siehe Handles) sollen nur readonly sein.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxVollstaendigesStaffelverfahren_ToggleStateChanging(sender As Object, args As Telerik.WinControls.UI.StateChangingEventArgs) Handles RadCheckBoxVollstaendigesStaffelverfahren.ToggleStateChanging, RadCheckBoxNichtselbsteinspielend.ToggleStateChanging, RadCheckBoxMehrteilungswaage.ToggleStateChanging, RadCheckBoxMehrbereichswaage.ToggleStateChanging, RadCheckBoxHalbSelbsteinspielend.ToggleStateChanging
        If _suspendEvents = False Then

            args.Cancel = True
        End If
    End Sub

    Private Sub RadCheckBoxDrucker_ToggleStateChanged(sender As Object, args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadCheckBoxDrucker.ToggleStateChanged
        If _suspendEvents Then Exit Sub
        RadTextBoxControlDruckerTyp.Enabled = RadCheckBoxDrucker.Checked
        AktuellerStatusDirty = True
        If RadTextBoxControlDruckerTyp.Enabled = False Then
            RadTextBoxControlDruckerTyp.Text = ""
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs)
        Dim f As New frmEichfehlergrenzen(objEichprozess)
        f.Show()

    End Sub

    'Entsperrroutine
    Protected Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt.
        EnableControls(RadGroupBoxBeschaffenheitspruefung)
        EnableControls(RadGroupBoxBeschaffenheitspruefungNormalien)
        EnableControls(RadGroupBoxIdentifikationsdaten)
        EnableControls(RadGroupBoxKomponenten)
        EnableControls(RadGroupBoxMax1)
        EnableControls(RadGroupBoxMax2)
        EnableControls(RadGroupBoxMax3)
        EnableControls(RadGroupBoxPruefverfahren)
        EnableControls(RadGroupBoxVerwendungszweck)
        EnableControls(RadGroupBoxVerwendungszweckArtderWaage)
        EnableControls(RadGroupBoxVerwendungszweckEquipment)
        EnableControls(RadGroupBoxVerwendungszweckNullstellung)
        EnableControls(RadGroupBoxVerwendungszweckTara)

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

End Class