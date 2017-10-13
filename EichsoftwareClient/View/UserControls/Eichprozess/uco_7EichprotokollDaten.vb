Imports EichsoftwareClient.EichsoftwareWebservice

Public Class uco_7EichprotokollDaten

    Inherits ucoContent
    Implements IRhewaEditingDialog

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken
    Private _objEichprotokoll As Eichprotokoll
    Private ListPruefscheinnnummernGesperrt As List(Of StatusPrüfscheinnummer)
    Private ListPruefscheinnnummern As List(Of StatusPrüfscheinnummer)
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

    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetzeUeberschrift()
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

#End Region

#Region "Methods"
    Private Sub FillControlsDrucker()
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
    End Sub

    Private Sub FillControlsBeschaffenheitspruefung()
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse Is Nothing Then RadTextBoxControlNormalienGenauigkeitsklasse.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Genauigkeitsklasse
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall Is Nothing Then RadTextBoxControlNormalienPruefintervall.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefintervall
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung Is Nothing Then RadDateTimePickerNormalienLetztePruefung.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer Is Nothing Then RadTextBoxControlNormalienPruefscheinnummer.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer
        If Not objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma Is Nothing Then RadTextBoxControlNormalienEichfahrzeugFirma.Text = objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma
    End Sub

    Private Sub FillControlsVerwendungszweckTara()
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
            If objEichprozess.Eichprotokoll.Verwendungszweck_Automatisch = True Then RadRadioButtonNustellungAutomatisch.IsChecked = True
            If objEichprozess.Eichprotokoll.Verwendungszweck_Nullnachfuehrung = True Then RadRadioButtonNustellungNullNachfuehrung.IsChecked = True
            If objEichprozess.Eichprotokoll.Verwendungszweck_HalbAutomatisch Then RadRadioButtonNustellungHalbAutomatisch.IsChecked = True
            If objEichprozess.Eichprotokoll.Verwendungszweck_AutoTara Then RadRadioButtonAutoTara.IsChecked = True
            If objEichprozess.Eichprotokoll.Verwendungszweck_HandTara Then RadRadioButtonHandTara.IsChecked = True
            If objEichprozess.Eichprotokoll.Taraeinrichtung_Taraeingabe Then RadRadioButtonTaraeingabe.IsChecked = True
        End If
    End Sub

    Private Sub FillControlsWagentyp()
        If objEichprozess?.Lookup_Waagentyp IsNot Nothing Then
            Select Case AktuellerBenutzer.Instance.AktuelleSprache
                Case "de"
                    RadTextBoxControlWaagentyp.Text = objEichprozess.Lookup_Waagentyp.Typ

                Case "en"
                    RadTextBoxControlWaagentyp.Text = objEichprozess.Lookup_Waagentyp.Typ_EN

                Case "pl"
                    RadTextBoxControlWaagentyp.Text = objEichprozess.Lookup_Waagentyp.Typ_PL
            End Select
        End If
    End Sub

    Private Sub FillControlsKomponenten()
        'bereich Komponenten
        RadTextBoxControlAWG.Text = objEichprozess.Lookup_Auswertegeraet.Typ
        RadTextBoxControlSoftwarestand.Text = objEichprozess.Eichprotokoll.Komponenten_Softwarestand
        RadTextBoxControlEichzaehlerstand.Text = objEichprozess.Eichprotokoll.Komponenten_Eichzaehlerstand

        RadTextBoxControlWZHersteller.Text = objEichprozess.Lookup_Waegezelle.Hersteller
        RadTextBoxControlWZTyp.Text = objEichprozess.Lookup_Waegezelle.Typ
        RadTextBoxControlWZAnzahl.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        RadTextBoxControlWZFabriknummer.Text = objEichprozess.Eichprotokoll.Komponenten_WaegezellenFabriknummer
    End Sub

    Private Sub FillControlsSonderfaelleNachVerfahren(dMAXHoechlast As Decimal)
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
    End Sub
    ''' <summary>
    '''  volle normallast: wenn höchster MAX Wert unter 1000 dann "ja". darüber auswählbar
    '''  betrag normallast: Höchster MAX Wert. Wenn über 1000 dann eingebbar
    ''' </summary>
    ''' <param name="dMAXHoechlast"></param>
    Private Sub FillControlsNormallast(dMAXHoechlast As Decimal)
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
            If objEichprozess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren = GlobaleEnumeratoren.enuVerfahrensauswahl.ueber60kgimStaffelverfahren Then
                Label12.Visible = True
            End If
        End If
    End Sub

    Private Function GetHoechstlast() As Decimal
        Dim dMAXHoechlast As Decimal 'variable zum speichern der höchsten Hoechstlast (je nach Art der Waage Max1,2 oder 3)

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

        Return dMAXHoechlast
    End Function

    Private Sub FillControlsBerechnungHoechstwert()
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
    End Sub

    Private Sub FillControlsWaagenInformationen()
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


        'standardmäßig TRUE
        RadCheckBoxHalbSelbsteinspielend.Checked = True
        'standardmäßig False
        RadCheckBoxNichtselbsteinspielend.Checked = False
        Try
            RadCheckBoxHybridMechWaage.Checked = objEichprozess.Eichprotokoll.Identifikationsdaten_HybridMechanisch
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillControlsGenauigkeitsklasse()
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
    End Sub

    Private Sub FillControlsIdentifikationsdaten()
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
            If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then RadTextBoxControlDatum.Text = Date.Now.Date
        Else
            RadTextBoxControlDatum.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Datum
        End If
        If Not objEichprozess.Eichprotokoll.Identifikationsdaten_Pruefer Is Nothing Then
            RadTextBoxControlPruefer.Text = objEichprozess.Eichprotokoll.Identifikationsdaten_Pruefer
        Else
            'Stammdaten aus lokaler Lizenz laden
            RadTextBoxControlPruefer.Text = AktuellerBenutzer.Instance.Lizenz.Name & ", " & AktuellerBenutzer.Instance.Lizenz.Vorname & " (" + AktuellerBenutzer.Instance.Lizenz.HEKennung & ")"
        End If
        RadTextBoxControlFabriknummer.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
    End Sub

    Private Sub SetControlsSonderfaelleRHEWALizenz()
        If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then
            If objEichprozess.AusStandardwaageErzeugt = True Then
                lblTruck.Visible = False
                RadTextBoxControlNormalienEichfahrzeugFirma.Visible = False
                RadDateTimePickerNormalienLetztePruefung.Visible = False
                RadDateTimePickerNormalienLetztePruefung.Value = Now
                lblTestzeitraum.Visible = False
                Label10.Visible = False
            End If
        End If
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
#End Region


#Region "Prüfscheinnummern"
    Private Sub BackgroundWorkerPruefscheinnummern_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerPruefscheinnummern.DoWork
        If LadePruefscheinnummernAusWeb(e) = False Then
            Exit Sub
        End If
        If ValidierePruefscheinnummern(e) = False Then
            e.Result = ""
        End If
    End Sub

    Private Sub BackgroundWorkerPruefscheinnummern_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerPruefscheinnummern.RunWorkerCompleted
        ShowPruefscheinnummernFehlermeldung(e)
    End Sub

    ''' <summary>
    ''' stößt backgroundworker an der die aktuellen Werte aus der DB lädt
    ''' ACHTUNG  Prüfscheinnummern werden bisher  über Admin Client in die DB hochgeladen (als Excel import)
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub CheckPruefscheinnummer(sender As Object)
        Try
            Dim txt As Telerik.WinControls.UI.RadTextBox = DirectCast(sender, Telerik.WinControls.UI.RadTextBox)
            If txt Is RadTextBoxControlNormalienPruefscheinnummer Then
                If AktuellerBenutzer.Instance.Lizenz.RHEWALizenz Then
                    BackgroundWorkerPruefscheinnummern.RunWorkerAsync()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function ValidierePruefscheinnummern(ByRef e As System.ComponentModel.DoWorkEventArgs) As Boolean
        Dim Vergleichswerte = RadTextBoxControlNormalienPruefscheinnummer.Text.Replace(",", ";").Replace(" ", "").Trim
        Try
            Dim ArrVergleichswerte = Vergleichswerte.Split(";")
            Dim results = (From o In ListPruefscheinnnummernGesperrt Where ArrVergleichswerte.Contains(o.Nummer)).ToList
            Dim negativeResults = (From o In ArrVergleichswerte Where Not ListPruefscheinnnummern.Select(Function(c) c.Nummer.ToString).Contains(o)).ToList

            Dim returnString As String = ""

            For Each result In results
                If result.Gesperrt Then
                    returnString += String.Format("Das Gewicht mit der Nummer {0} darf nicht verwendet werden und muss aussortiert werden ", result.Nummer) & vbNewLine & vbNewLine
                ElseIf result.GesperrtDurchDatum Then
                    returnString += String.Format("Das Gewicht mit der Nummer {0} darf nur auf Anweisung verwendet werden. Der Prüfschein ist abgelaufen ", result.Nummer) & vbNewLine & vbNewLine
                End If
            Next

            For Each result In negativeResults
                If result = "" Then Continue For
                returnString += String.Format("Das Gewicht mit der Nummer {0} wurde nicht gefunden.", result) & vbNewLine & vbNewLine
            Next
            e.Result = returnString
            Return True

        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Private Function LadePruefscheinnummernAusWeb(ByRef e As System.ComponentModel.DoWorkEventArgs) As Boolean
        If ListPruefscheinnnummern Is Nothing Then
            ListPruefscheinnnummern = clsWebserviceFunctions.GetStatusPruefscheinnummern()

            'abbruch
            If ListPruefscheinnnummern Is Nothing Then
                e.Result = "Keine Prüfscheinnummern gefunden. Eventuell ist der Server gerade nicht erreichbar"
                Return False
            End If

            ListPruefscheinnnummernGesperrt = (From o In ListPruefscheinnnummern Where o.Gesperrt = True Or o.GesperrtDurchDatum = True).ToList
            ListPruefscheinnnummern = (From o In ListPruefscheinnnummern Where o.Gesperrt = False And o.GesperrtDurchDatum = False).ToList
        End If
        Return True
    End Function



    Private Sub ShowPruefscheinnummernFehlermeldung(e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If e.Result <> "" Then

            Dim tooltip As New Telerik.WinControls.UI.RadDesktopAlert()
            tooltip.IsPinned = True
            tooltip.ContentText = e.Result
            tooltip.FixedSize = New System.Drawing.Size(600, 450)
            tooltip.SoundToPlay = Media.SystemSounds.Exclamation
            tooltip.PlaySound = True
            '20 - vertical margins, 70 - caption height
            Dim graphics As Telerik.WinControls.MeasurementGraphics = Telerik.WinControls.MeasurementGraphics.CreateMeasurementGraphics()
            Dim sizeF As SizeF = graphics.Graphics.MeasureString(tooltip.ContentText, Me.Font, tooltip.FixedSize.Width - 20)

            tooltip.FixedSize = New Size(tooltip.FixedSize.Width - 20, CInt(sizeF.Height) + 80)
            tooltip.Show()

        End If
    End Sub

    Private Sub RadTextBoxControlNormalienPruefscheinnummer_Validated(sender As Object, e As EventArgs) Handles RadTextBoxControlNormalienPruefscheinnummer.Validated
        CheckPruefscheinnummer(sender)
    End Sub
#End Region

#Region "Inteferface Methods"
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

                Dim Coll As New AutoCompleteStringCollection
                Coll.AddRange((From o In context.Eichprotokoll Where Not o.Verwendungszweck_Druckertyp Is Nothing And o.Verwendungszweck_Druckertyp <> String.Empty Select o.Verwendungszweck_Druckertyp).Distinct.ToArray)
                RadTextBoxControlDruckerTyp.AutoCompleteDataSource = Coll
                RadTextBoxControlDruckerTyp.DataSource = Coll
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()
        'fokus setzen auf erstes Steuerelement
        RadTextBoxControlBenutzer.Focus()

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
    Protected Friend Overrides Sub FillControls() Implements IRhewaEditingDialog.FillControls
        Dim dMAXHoechlast As Decimal = GetHoechstlast()

        'Steuerlemente füllen

        SetControlsSonderfaelleRHEWALizenz()
        FillControlsIdentifikationsdaten()
        FillControlsGenauigkeitsklasse()
        FillControlsWaagenInformationen()
        FillControlsBerechnungHoechstwert()
        FillControlsNormallast(dMAXHoechlast)
        FillControlsSonderfaelleNachVerfahren(dMAXHoechlast)
        FillControlsKomponenten()
        FillControlsWagentyp()
        FillControlsVerwendungszweckTara()
        FillControlsBeschaffenheitspruefung()
        FillControlsDrucker()


    End Sub
    Protected Friend Overrides Sub Entsperrung() Implements IRhewaEditingDialog.Entsperrung
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

    Protected Friend Overrides Sub SetzeUeberschrift() Implements IRhewaEditingDialog.SetzeUeberschrift
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
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Protected Friend Overrides Sub UpdateObjekt() Implements IRhewaEditingDialog.UpdateObjekt
        If DialogModus = enuDialogModus.normal Then objEichprozess.Bearbeitungsdatum = Date.Now
        'Bereich Identifikationsdaten
        If RadTextBoxControlDatum.Text.Equals("") = False Then
            objEichprozess.Eichprotokoll.Identifikationsdaten_Datum = RadTextBoxControlDatum.Text
        End If

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
        If RadDateTimePickerNormalienLetztePruefung.Text.Equals("") = False Then
            objEichprozess.Eichprotokoll.Beschaffenheitspruefung_LetztePruefung = RadDateTimePickerNormalienLetztePruefung.Text

        End If

        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_Pruefscheinnummer = RadTextBoxControlNormalienPruefscheinnummer.Text
        objEichprozess.Eichprotokoll.Beschaffenheitspruefung_EichfahrzeugFirma = RadTextBoxControlNormalienEichfahrzeugFirma.Text

        objEichprozess.Eichprotokoll.Identifikationsdaten_HybridMechanisch = RadCheckBoxHybridMechWaage.Checked

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
                    UpdateObjekt()
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

    Protected Friend Overrides Sub Lokalisiere() Implements IRhewaEditingDialog.Lokalisiere
        Dim oldsuspendEvents = _suspendEvents
        _suspendEvents = True
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uco_7EichprotokollDaten))
        Lokalisierung(Me, resources)

        _suspendEvents = oldsuspendEvents
    End Sub


    Protected Friend Overrides Function CheckDialogModus() As Boolean Implements IRhewaEditingDialog.CheckDialogModus
        If DialogModus = enuDialogModus.lesend Then
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        If DialogModus = enuDialogModus.korrigierend Then
            UpdateObjekt()
            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung Then
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung
            End If
            ParentFormular.CurrentEichprozess = objEichprozess
            Return False
        End If
        Return True
    End Function


    Protected Friend Overrides Function ValidateControls() As Boolean Implements IRhewaEditingDialog.ValidateControls
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
                RadTextBoxControlDruckerTyp.Focus()
                AbortSaving = True
                MarkControlRed(RadTextBoxControlDruckerTyp)
            End If
        End If

        If IsNumeric(RadTextBoxControlBetragNormallast.Text) = False Then
            RadTextBoxControlBetragNormallast.Focus()
            AbortSaving = True
            MarkControlRed(RadTextBoxControlBetragNormallast)
        End If

        If IsDate(RadTextBoxControlDatum.Text) = False Then
            RadTextBoxControlDatum.Focus()
            AbortSaving = True
            MarkControlRed(RadTextBoxControlDatum)
        End If

        'fehlermeldung anzeigen bei falscher validierung
        Dim result = Me.ShowValidationErrorBox(True)
        Return ProcessResult(result)
    End Function

    Protected Friend Overrides Sub OverwriteIstSoll() Implements IRhewaEditingDialog.OverwriteIstSoll
        RadTextBoxControlBenutzer.Text = "Hill"
        RadTextBoxControlAufstellungsort.Text = "bitwork Halle 1"
        RadTextBoxControlBaujahr.Text = "2010"
        RadTextBoxControlSoftwarestand.Text = "1024"
        RadTextBoxControlEichzaehlerstand.Text = "1024"
        RadTextBoxControlWZFabriknummer.Text = "1024"
        RadDateTimePickerNormalienLetztePruefung.Text = DateTime.Now
        RadTextBoxControlNormalienPruefscheinnummer.Text = "1024"
    End Sub

    Protected Friend Overrides Sub Versenden() Implements IRhewaEditingDialog.Versenden
        UpdateObjekt()
        CloneAndSendServerObjekt()
    End Sub

#End Region

End Class