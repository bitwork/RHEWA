Public Class ucoKompatiblititaetsnachweisErgebnis
    Inherits ucoContent


#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)
    Private _bolLoaded As Boolean = False ' variable die sicherstellen soll, dass nicht das LOAD Event und das GotFocus Event doppelt läuft
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
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
    End Sub
#End Region

#Region "Events"

    Private Sub ucoKompatiblititaetsnachweisErgebnis_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        'Wenn z.b. lesend auf die Eichung zugegriffen wird udn vor und zurück geblättert wird, wird das LOAD Event nicht mehr aufgerufen. Damit aber trotzdem die Formeln korrigert werden, führt das GotFocus Event dasselbe aus
        If Not _bolLoaded Then
            LadeDialog()
        End If
    End Sub
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LadeDialog()
        _bolLoaded = True
    End Sub

    Private Sub LadeDialog()
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisErgebnisHilfe)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_KompatiblitaetsnachweisErgebnis
            Catch ex As Exception
            End Try
        End If
        EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis

        'daten füllen
        LoadFromDatabase()
    End Sub



#End Region


#Region "Methods"

    Private Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'Nur laden wenn es sich um eine Bearbeitung handelt (sonst würde das in Memory Objekt überschrieben werden)
        If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
            Using context As New EichsoftwareClientdatabaseEntities1
                'neu laden des Objekts, diesmal mit den lookup Objekten
                objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End Using
        End If
        'steuerelemente mit werten aus DB füllen
        FillControls()

        If DialogModus = enuDialogModus.lesend Then
            'falls der Eichvorgang nur lesend betrchtet werden soll, wird versucht alle Steuerlemente auf REadonly zu setzen. Wenn das nicht klappt,werden sie disabled
            For Each Control In Me.FlowLayoutPanel1.Controls
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
    End Sub

    ''' <summary>
    ''' Lädt die Werte aus dem Beschaffenheitspruefungsobjekt in die Steuerlemente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub FillControls()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoKompatiblititaetsnachweisErgebnis))


        _suspendEvents = True
        'befüllen der Controls aus dem Eichprozessobjekt
        RadTextBoxFabriknummer.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer


        '(1) Genauigkeitsklassen von WZ, AWG und NSW
        '=WENN('Daten-Eingabe'!$G$36="";"";'Daten-Eingabe'!$G$36)
        RadTextBoxPunkt1LC.Text = objEichprozess.Lookup_Waegezelle.Genauigkeitsklasse
        RadTextBoxPunkt1IND.Text = objEichprozess.Lookup_Auswertegeraet.Genauigkeitsklasse
        RadTextBoxPunkt1WI.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse

        'genauigkeitsklassen umwandeln
        Dim WZGenauigkeitsklasse As Integer = 0
        Select Case objEichprozess.Lookup_Waegezelle.Genauigkeitsklasse
            Case Is = "A"
                WZGenauigkeitsklasse = 1
            Case Is = "B"
                WZGenauigkeitsklasse = 2
            Case Is = "C"
                WZGenauigkeitsklasse = 3
            Case Is = "D"
                WZGenauigkeitsklasse = 4
            Case Else
                WZGenauigkeitsklasse = 5

        End Select

        Dim awgGenauigkeitsklasse As Integer = 0
        Select Case objEichprozess.Lookup_Auswertegeraet.Genauigkeitsklasse
            Case Is = "I"
                awgGenauigkeitsklasse = 1
            Case Is = "II"
                awgGenauigkeitsklasse = 2
            Case Is = "III"
                awgGenauigkeitsklasse = 3
            Case Is = "IV"
                awgGenauigkeitsklasse = 4
            Case Else
                awgGenauigkeitsklasse = 5
        End Select

        Dim WaageGenauigkeitsklasse As Integer = 0
        Select Case objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse
            Case Is = "I"
                WaageGenauigkeitsklasse = 1
            Case Is = "II"
                WaageGenauigkeitsklasse = 2
            Case Is = "III"
                WaageGenauigkeitsklasse = 3
            Case Is = "IV"
                WaageGenauigkeitsklasse = 4
            Case Else
                WaageGenauigkeitsklasse = 5

        End Select
        If WaageGenauigkeitsklasse = 0 OrElse WZGenauigkeitsklasse > WaageGenauigkeitsklasse OrElse awgGenauigkeitsklasse > WaageGenauigkeitsklasse Then
            RadCheckBoxPunkt1.Checked = False
        Else
            RadCheckBoxPunkt1.Checked = True
        End If


        ' (2) Temperaturbereiche von WZ und AWG im Vergleich zum Temperaturbereich der NSW   in ° C
        RadTextBoxPunkt2LCMin.Text = objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN
        RadTextBoxPunkt2INDMin.Text = objEichprozess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN
        RadTextBoxPunkt2WIMin.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN
        RadTextBoxPunkt2LCMax.Text = objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX
        RadTextBoxPunkt2INDMax.Text = objEichprozess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX
        RadTextBoxPunkt2WIMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX

        '=WENN(ODER($G$14="";$G$14>100);"NEIN";WENN($C$14>$G$14;"NEIN";WENN($E$14>$G$14;"NEIN";"JA")))
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN = "" _
            OrElse objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN > objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN _
            OrElse objEichprozess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN > objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN Then

            RadCheckBoxPunkt2TMin.Checked = False
        Else
            RadCheckBoxPunkt2TMin.Checked = True
        End If

        '=WENN(ODER($C15>150;$E$15>100);"NEIN";WENN($G$15="";"NEIN";WENN($C$15<$G$15;"NEIN";WENN($E$15<$G$15;"NEIN";"JA"))))
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX = "" _
         OrElse objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX > 150 _
         OrElse objEichprozess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX > 150 _
         OrElse objEichprozess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX < objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX _
         OrElse objEichprozess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX < objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX _
         Then
            RadCheckBoxPunkt2TMAX.Checked = False
        Else
            RadCheckBoxPunkt2TMAX.Checked = True
        End If


        '(3) Summe der Quadrate der Fehlergrenzenanteile von Verbindungselementen, AWG und WZ
        RadTextBoxPunkt3pcon.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze ^ 2
        RadTextBoxPunkt3pind.Text = objEichprozess.Lookup_Auswertegeraet.BruchteilEichfehlergrenze ^ 2
        RadTextBoxPunkt3Plc.Text = objEichprozess.Lookup_Waegezelle.BruchteilEichfehlergrenze ^ 2

        '=WENN(ODER($F$20="";$F$20>(0,8)^2;$F$20<(0,3)^2;$D$20="";$D$20>(0,8)^2;$D$20<(0,3)^2;$B$20="");"NEIN";WENN($B$20+$D$20+$F$20>1;"NEIN";"JA"))
        If RadTextBoxPunkt3Plc.Text = "" OrElse RadTextBoxPunkt3Plc.Text > (0.8) ^ 2 OrElse RadTextBoxPunkt3Plc.Text < (0.3) ^ 2 OrElse RadTextBoxPunkt3pind.Text = "" OrElse RadTextBoxPunkt3pind.Text > (0.8) ^ 2 OrElse RadTextBoxPunkt3pind.Text < (0.3) ^ 2 OrElse RadTextBoxPunkt3pcon.Text = "" Then
            RadCheckBoxPunkt3.Checked = False
        ElseIf CDec(RadTextBoxPunkt3pcon.Text) + CDec(RadTextBoxPunkt3pind.Text) + CDec(RadTextBoxPunkt3Plc.Text) > 1 Then
            RadCheckBoxPunkt3.Checked = False
        Else
            RadCheckBoxPunkt3.Checked = True
        End If


        '(4) Größte zul. Anzahl der Teilungswerte des AWG und Anzahl der Eichwerte der NSW
        'je nach Waagenart die Größte Anzahl der Teilungswerte zuweisen
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                RadTextBoxPunkt4Nind2.Visible = False
                RadTextBoxPunkt4Max2.Visible = False
                RadTextBoxPunkt4Nind3.Visible = False
                RadTextBoxPunkt4Max3.Visible = False
                lblPunkt4I2.Visible = False
                lblPunkt4I3.Visible = False
                lblPunkt4I2Eq.Visible = False
                lblPunkt4I3Eq.Visible = False

                RadCheckBoxPunkt4Max2.Visible = False
                RadCheckBoxPunkt4Max3.Visible = False


                RadTextBoxPunkt4Nind1.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteEinbereichswaage
                RadTextBoxPunkt4Nind2.Text = ""
                RadTextBoxPunkt4Nind3.Text = ""
                RadTextBoxPunkt4Max2.Text = ""
                RadTextBoxPunkt4Max3.Text = ""
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                RadTextBoxPunkt4Nind2.Visible = True
                RadTextBoxPunkt4Max2.Visible = True
                RadTextBoxPunkt4Nind3.Visible = False
                RadTextBoxPunkt4Max3.Visible = False

                lblPunkt4I2.Visible = True
                lblPunkt4I2Eq.Visible = True
                lblPunkt4I3.Visible = False
                lblPunkt4I3Eq.Visible = False

                RadCheckBoxPunkt4Max2.Visible = True
                RadCheckBoxPunkt4Max3.Visible = False


                RadTextBoxPunkt4Nind1.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
                RadTextBoxPunkt4Nind2.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
                RadTextBoxPunkt4Nind3.Text = ""
                RadTextBoxPunkt4Max3.Text = ""

            Case Else
                RadTextBoxPunkt4Nind2.Visible = True
                RadTextBoxPunkt4Nind3.Visible = True
                RadTextBoxPunkt4Max2.Visible = True
                RadTextBoxPunkt4Max3.Visible = True
                lblPunkt4I2.Visible = True
                lblPunkt4I3.Visible = True
                lblPunkt4I2Eq.Visible = True
                lblPunkt4I3Eq.Visible = True
                RadCheckBoxPunkt4Max2.Visible = True
                RadCheckBoxPunkt4Max3.Visible = True

                RadTextBoxPunkt4Nind1.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
                RadTextBoxPunkt4Nind2.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
                RadTextBoxPunkt4Nind3.Text = objEichprozess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
        End Select

        '=WENN('Daten-Eingabe'!$H$11="";"";
        'WENN('Daten-Eingabe'!$H$11=0;"";
        'Daten-Eingabe'!$G$11/'Daten-Eingabe'!$H$11))
        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = "" OrElse objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 = "0" Then
            RadTextBoxPunkt4Max1.Text = ""
        Else
            RadTextBoxPunkt4Max1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        End If

        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = "" OrElse objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 = "0" Then
            RadTextBoxPunkt4Max2.Text = ""
        Else
            RadTextBoxPunkt4Max2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        End If

        If objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = "" OrElse objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 = "0" Then
            RadTextBoxPunkt4Max3.Text = ""
        Else
            RadTextBoxPunkt4Max3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        End If


        '=WENN(ODER($E$26="";$E$26>100000000;$G$26="");"NEIN";WENN($G$26>$E$26;"NEIN";"JA"))
        If RadTextBoxPunkt4Nind1.Text = "" OrElse CDec(RadTextBoxPunkt4Nind1.Text) > 100000000 OrElse RadTextBoxPunkt4Max1.Text = "" Then
            RadCheckBoxPunkt4Max1.Checked = False
        ElseIf CDec(RadTextBoxPunkt4Max1.Text) > CDec(RadTextBoxPunkt4Nind1.Text) Then
            RadCheckBoxPunkt4Max1.Checked = False
        Else
            RadCheckBoxPunkt4Max1.Checked = True
        End If

        If RadCheckBoxPunkt4Max2.Visible = True Then
            If RadTextBoxPunkt4Nind2.Text = "" OrElse CDec(RadTextBoxPunkt4Nind2.Text) > 100000000 OrElse RadTextBoxPunkt4Max2.Text = "" Then
                RadCheckBoxPunkt4Max2.Checked = False
            ElseIf CDec(RadTextBoxPunkt4Max2.Text) > CDec(RadTextBoxPunkt4Nind2.Text) Then
                RadCheckBoxPunkt4Max2.Checked = False
            Else
                RadCheckBoxPunkt4Max2.Checked = True
            End If
        End If

        If RadCheckBoxPunkt4Max3.Visible = True Then
            If RadTextBoxPunkt4Nind3.Text = "" OrElse CDec(RadTextBoxPunkt4Nind3.Text) > 100000000 OrElse RadTextBoxPunkt4Max3.Text = "" Then
                RadCheckBoxPunkt4Max3.Checked = False
            ElseIf CDec(RadTextBoxPunkt4Max3.Text) > CDec(RadTextBoxPunkt4Nind3.Text) Then
                RadCheckBoxPunkt4Max3.Checked = False
            Else
                RadCheckBoxPunkt4Max3.Checked = True
            End If
        End If


        '5) Höchstlasten von NSW und WZ

        'je nach art der Waage andere Höchstlasten wählen gemäß
        '=WENN('Daten-Eingabe'!$G$19="";"DL fehlt";WENN('Daten-Eingabe'!$G$18="";"NUD fehlt";WENN('Daten-Eingabe'!$G$17="";"IZRS fehlt";WENN('Daten-Eingabe'!$G$11="";"";WENN('Daten-Eingabe'!$G$11=0;"";('Daten-Eingabe'!$G$11+'Daten-Eingabe'!$G$17+'Daten-Eingabe'!$G$18+'Daten-Eingabe'!$G$19+'Daten-Eingabe'!$G$20)/'Daten-Eingabe'!$G$11)))))
        'Bzw 
        '=WENN('Daten-Eingabe'!$G$19="";"DL fehlt";WENN('Daten-Eingabe'!$G$18="";"NUD fehlt";WENN('Daten-Eingabe'!$G$17="";"IZRS fehlt";WENN('Daten-Eingabe'!$G$13="";"";WENN('Daten-Eingabe'!$G$13=0;"";('Daten-Eingabe'!$G$13+'Daten-Eingabe'!$G$17+'Daten-Eingabe'!$G$18+'Daten-Eingabe'!$G$19+'Daten-Eingabe'!$G$20)/'Daten-Eingabe'!$G$13)))))
        'bzw
        '=WENN('Daten-Eingabe'!$G$19="";"DL fehlt";WENN('Daten-Eingabe'!$G$18="";"NUD fehlt";WENN('Daten-Eingabe'!$G$17="";"IZRS fehlt";WENN('Daten-Eingabe'!$G$14="";"";WENN('Daten-Eingabe'!$G$14=0;"";('Daten-Eingabe'!$G$14+'Daten-Eingabe'!$G$17+'Daten-Eingabe'!$G$18+'Daten-Eingabe'!$G$19+'Daten-Eingabe'!$G$20)/'Daten-Eingabe'!$G$14)))))

        'ist im exceldokument mit 3 nachkommastellen formatiert
        Try
            objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis), 3, MidpointRounding.AwayFromZero)
        Catch ex As Exception
        End Try


        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                RadTextBoxPunkt5Faktor.Text = (CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast)) / CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1)




                '=WENN('Daten-Eingabe'!$G$15="";"R fehlt";WENN($G$33="";"Q fehlt";WENN('Daten-Eingabe'!$G$16="";"Anzahl  N  fehlt";($G$33*'Daten-Eingabe'!$G$11*'Daten-Eingabe'!$G$15)/'Daten-Eingabe'!$G$16)))
                'in der Excelmappe ohne Nachkommastellen angegeben deswegen CINT()
                 RadTextBoxPunkt5QMax.Text = (RadTextBoxPunkt5Faktor.Text * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis) / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

                'vorher bereits runden des Faktorwertes (mit 2 Dezimalstellen angegeben), da damit gerechnet wird
                If Not RadTextBoxPunkt5Faktor.Text = "" Then
                    RadTextBoxPunkt5Faktor.Text = Math.Round(CDec(RadTextBoxPunkt5Faktor.Text), 2, MidpointRounding.AwayFromZero)
                End If

            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                RadTextBoxPunkt5Faktor.Text = (CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast)) / CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2)



                '=WENN('Daten-Eingabe'!$G$15="";"R fehlt";WENN($G$33="";"Q fehlt";WENN('Daten-Eingabe'!$G$16="";"Anzahl  N  fehlt";($G$33*'Daten-Eingabe'!$G$13*'Daten-Eingabe'!$G$15)/'Daten-Eingabe'!$G$16)))
                RadTextBoxPunkt5QMax.Text = (RadTextBoxPunkt5Faktor.Text * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis) / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

                'vorher bereits runden des Faktorwertes (mit 2 Dezimalstellen angegeben), da damit gerechnet wird
                If Not RadTextBoxPunkt5Faktor.Text = "" Then
                    RadTextBoxPunkt5Faktor.Text = Math.Round(CDec(RadTextBoxPunkt5Faktor.Text), 2, MidpointRounding.AwayFromZero)
                End If
            Case Else
                RadTextBoxPunkt5Faktor.Text = (CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag) + CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast) + _
                                               CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast)) / CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3)



                '=WENN('Daten-Eingabe'!$G$15="";"R fehlt";WENN($G$33="";"Q fehlt";WENN('Daten-Eingabe'!$G$16="";"Anzahl  N  fehlt";($G$33*'Daten-Eingabe'!$G$14*'Daten-Eingabe'!$G$15)/'Daten-Eingabe'!$G$16)))
                RadTextBoxPunkt5QMax.Text = (RadTextBoxPunkt5Faktor.Text * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis) / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

                'vorher bereits runden des Faktorwertes (mit 2 Dezimalstellen angegeben), da damit gerechnet wird
                If Not RadTextBoxPunkt5Faktor.Text = "" Then
                    RadTextBoxPunkt5Faktor.Text = Math.Round(CDec(RadTextBoxPunkt5Faktor.Text), 2, MidpointRounding.AwayFromZero)
                End If 'Decimal.Round(CDec(RadTextBoxPunkt5Faktor.Text), 2)

        End Select
        'in der Excelmappe ohne Nachkommastellen angegeben deswegen CINT()
        If Not RadTextBoxPunkt5QMax.Text.Equals("") Then
            RadTextBoxPunkt5QMax.Text = CInt(RadTextBoxPunkt5QMax.Text)
        End If
        RadTextBoxPunkt5EMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

        '=WENN(ODER($G$33="";$G$35="";$G$35>10000000);"NEIN";WENN($D$35>$G$35;"NEIN";"JA"))
        If RadTextBoxPunkt5Faktor.Text = "" OrElse RadTextBoxPunkt5EMax.Text = "" OrElse CDec(RadTextBoxPunkt5EMax.Text) > 10000000 Then
            RadCheckBoxPunkt5.Checked = False
        ElseIf CDec(RadTextBoxPunkt5QMax.Text) > CDec(RadTextBoxPunkt5EMax.Text) Then
            RadCheckBoxPunkt5.Checked = False
        Else
            RadCheckBoxPunkt5.Checked = True
            RadCheckBoxPunkt5.Enabled = True
        End If

        '(6a) Größte zul. Anzahl der Teilungswerte der WZ und Anzahl der Eichwerte der NSW 


        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                RadTextBoxPunkt6aNind1.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                RadTextBoxPunkt6aNind2.Text = ""
                RadTextBoxPunkt6aNind3.Text = ""
                RadTextBoxPunkt6aMax2.Text = ""
                RadTextBoxPunkt6aMax3.Text = ""

                RadTextBoxPunkt6aNind2.Visible = False
                RadTextBoxPunkt6aNind3.Visible = False
                RadTextBoxPunkt6aMax2.Visible = False
                RadTextBoxPunkt6aMax3.Visible = False

                lblPunkt6aI2.Visible = False
                lblPunkt6aI3.Visible = False
                lblPunkt6aI2Eq.Visible = False
                lblPunkt6aI3Eq.Visible = False

                RadCheckBoxPunkt6aMax2.Visible = False
                RadCheckBoxPunkt6aMax3.Visible = False

                '=WENN('Daten-Eingabe'!$H$12="";"";WENN('Daten-Eingabe'!$H$12=0;"";'Daten-Eingabe'!$G$12/'Daten-Eingabe'!$H$12))
                RadTextBoxPunkt6aMax1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                RadTextBoxPunkt6aNind1.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                RadTextBoxPunkt6aNind2.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                RadTextBoxPunkt6aNind3.Text = ""
                RadTextBoxPunkt6aMax3.Text = ""

                RadTextBoxPunkt6aNind2.Visible = True
                RadTextBoxPunkt6aMax2.Visible = True
                RadTextBoxPunkt6aNind3.Visible = False
                RadTextBoxPunkt6aMax3.Visible = False

                lblPunkt6aI2.Visible = True
                lblPunkt6aI2Eq.Visible = True
                lblPunkt6aI3.Visible = False
                lblPunkt6aI3Eq.Visible = False

                RadCheckBoxPunkt6aMax2.Visible = True
                RadCheckBoxPunkt6aMax3.Visible = False

                '=WENN('Daten-Eingabe'!$H$13="";"";WENN('Daten-Eingabe'!$H$13=0;"";'Daten-Eingabe'!$G$13/'Daten-Eingabe'!$H$13))
                RadTextBoxPunkt6aMax1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                RadTextBoxPunkt6aMax2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
            Case Else
                RadTextBoxPunkt6aNind2.Visible = True
                RadTextBoxPunkt6aMax2.Visible = True
                RadTextBoxPunkt6aNind3.Visible = True
                RadTextBoxPunkt6aMax3.Visible = True

                lblPunkt6aI2.Visible = True
                lblPunkt6aI2Eq.Visible = True
                lblPunkt6aI3.Visible = True
                lblPunkt6aI3Eq.Visible = True
                RadCheckBoxPunkt6aMax2.Visible = True
                RadCheckBoxPunkt6aMax3.Visible = True

                RadTextBoxPunkt6aNind1.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                RadTextBoxPunkt6aNind2.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                RadTextBoxPunkt6aNind3.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte

                '=WENN('Daten-Eingabe'!$H$14="";"";WENN('Daten-Eingabe'!$H$14=0;"";'Daten-Eingabe'!$G$14/'Daten-Eingabe'!$H$14))
                RadTextBoxPunkt6aMax1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
                RadTextBoxPunkt6aMax2.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
                RadTextBoxPunkt6aMax3.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

        End Select


        Try
            '=WENN(ODER($G$41=0;$G$41="";$E$41="";$E$41>100000000);"NEIN";WENN($G$41>$E$41;"NEIN";"JA"))
            If RadTextBoxPunkt6aNind1.Text = "" OrElse RadTextBoxPunkt6aNind1.Text = "0" OrElse RadTextBoxPunkt6aMax1.Text = "" OrElse RadTextBoxPunkt6aMax1.Text = "0" OrElse CDec(RadTextBoxPunkt6aNind1.Text) > 100000000 Then
                RadCheckBoxPunkt6aMax1.Checked = False
            ElseIf CDec(RadTextBoxPunkt6aMax1.Text) > CDec(RadTextBoxPunkt6aNind1.Text) Then
                RadCheckBoxPunkt6aMax1.Checked = False
            Else
                RadCheckBoxPunkt6aMax1.Checked = True
            End If

            If RadTextBoxPunkt6aNind2.Text = "" OrElse RadTextBoxPunkt6aNind2.Text = "0" OrElse RadTextBoxPunkt6aMax2.Text = "" OrElse RadTextBoxPunkt6aMax2.Text = "0" OrElse CDec(RadTextBoxPunkt6aNind2.Text) > 100000000 Then
                RadCheckBoxPunkt6aMax2.Checked = False
            ElseIf CDec(RadTextBoxPunkt6aMax2.Text) > CDec(RadTextBoxPunkt6aNind2.Text) Then
                RadCheckBoxPunkt6aMax2.Checked = False
            Else
                RadCheckBoxPunkt6aMax2.Checked = True
            End If

            If RadTextBoxPunkt6aNind3.Text = "" OrElse RadTextBoxPunkt6aNind3.Text = "0" OrElse RadTextBoxPunkt6aMax3.Text = "" OrElse RadTextBoxPunkt6aMax3.Text = "0" OrElse CDec(RadTextBoxPunkt6aNind3.Text) > 100000000 Then
                RadCheckBoxPunkt6aMax3.Checked = False
            ElseIf CDec(RadTextBoxPunkt6aMax3.Text) > CDec(RadTextBoxPunkt6aNind3.Text) Then
                RadCheckBoxPunkt6aMax3.Checked = False
            Else
                RadCheckBoxPunkt6aMax3.Checked = True
            End If
        Catch e As Exception
        End Try


        '(6b) Rückkehr des Vorlastsignals der WZ und kleinster Eichwert e1 einer Mehrteilungswaage
        'nur bei mehrteilungswaagen
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Zweiteilungswaage", "Dreiteilungswaage"
                RadGroupBoxPunkt6b.Visible = True

                '=WENN('Daten-Eingabe'!$G$43<>0;'Daten-Eingabe'!$G$43;(WENN('Daten-Eingabe'!$G$44<>0;'Daten-Eingabe'!$G$37/(2*'Daten-Eingabe'!$G$44);'Daten-Eingabe'!$G$40)))
                If objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor <> "0" And objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor <> "" Then
                    RadTextBoxPunkt6bNLC.Text = objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor
                ElseIf objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal <> "0" And objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal <> "" Then
                    RadTextBoxPunkt6bNLC.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast / (2 * objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal)
                Else
                    RadTextBoxPunkt6bNLC.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                End If
            Case Else
                RadGroupBoxPunkt6b.Visible = False
                RadTextBoxPunkt6bNLC.Text = ""
                RadTextBoxPunkt6bMax.Text = ""
        End Select

        'runden auf 0 Nachkommastellen
        If RadTextBoxPunkt6bNLC.Visible = True Then

            If Not RadTextBoxPunkt6bNLC.Text.Equals("") Then
                RadTextBoxPunkt6bNLC.Text = CInt(RadTextBoxPunkt6bNLC.Text)
            End If

        End If

        'eMAX
        '=WENN('Daten-Eingabe'!$H$12="";"";WENN('Daten-Eingabe'!$H$12=0;"";'Daten-Eingabe'!$G$13/'Daten-Eingabe'!$H$12))
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Zweiteilungswaage"
                RadTextBoxPunkt6bMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
            Case Is = "Dreiteilungswaage"
                RadTextBoxPunkt6bMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        End Select

        '=WENN(D48>10000000;"NEIN";WENN($G$48="";"NEIN";WENN($G$48>$D$48;"NEIN";"JA")))
        If RadTextBoxPunkt6bMax.Text = "" OrElse RadTextBoxPunkt6bNLC.Text = "" OrElse CDec(RadTextBoxPunkt6bMax.Text) > 10000000 Then
            RadCheckBoxPunkt6b.Checked = False
        ElseIf CDec(RadTextBoxPunkt6bMax.Text) > CDec(RadTextBoxPunkt6bNLC.Text) Then
            RadCheckBoxPunkt6b.Checked = False
        Else
            RadCheckBoxPunkt6b.Checked = True
        End If

        '(6c) Rückkehr des Vorlastsignals der WZ und kleinster Eichwert e1 einer Mehrbereichswaage
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage", "Dreiteilungswaage", "Zweiteilungswaage"
                RadGroupBoxPunkt6c.Visible = False
                RadTextBoxPunkt6cNLC.Text = ""
                RadTextBoxPunkt6cMAX.Text = ""
            Case Else
                RadGroupBoxPunkt6c.Visible = True
                '=WENN('Daten-Eingabe'!$G$43<>0;'Daten-Eingabe'!$G$43;(WENN('Daten-Eingabe'!$G$44<>0;'Daten-Eingabe'!$G$37/(2*'Daten-Eingabe'!$G$44);'Daten-Eingabe'!$G$40)))
                If objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor <> "0" And objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor.ToString <> "" Then
                    RadTextBoxPunkt6cNLC.Text = objEichprozess.Lookup_Waegezelle.Kriechteilungsfaktor
                ElseIf objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal <> "0" And objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal <> "" Then
                    RadTextBoxPunkt6cNLC.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast / (2 * objEichprozess.Lookup_Waegezelle.RueckkehrVorlastsignal)
                Else
                    RadTextBoxPunkt6cNLC.Text = objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
                End If

                'andere hoechlast je nach Waagenart
                Select Case objEichprozess.Lookup_Waagenart.Art
                    Case Is = "Zweibereichswaage"
                        '=WENN('Daten-Eingabe'!$H$12="";"";WENN('Daten-Eingabe'!$H$12=0;"";'Daten-Eingabe'!$G$13/'Daten-Eingabe'!$H$12*0,4))
                        RadTextBoxPunkt6cMAX.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.4
                    Case Is = "Dreibereichswaage"
                        '=WENN('Daten-Eingabe'!$H$12="";"";WENN('Daten-Eingabe'!$H$12=0;"";'Daten-Eingabe'!$G$14/'Daten-Eingabe'!$H$12*0,4))
                        RadTextBoxPunkt6cMAX.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.4
                End Select

        End Select
        'runden auf 0 nachkommastellen
        If RadTextBoxPunkt6cNLC.Visible = True Then
            If Not RadTextBoxPunkt6cNLC.Text.Equals("") Then
                RadTextBoxPunkt6cNLC.Text = CInt(RadTextBoxPunkt6cNLC.Text)
            End If
        End If

        '=WENN($D$53>10000000;"NEIN";WENN($G$53="";"NEIN";WENN($G$53>$D$53;"NEIN";"JA")))
        If RadTextBoxPunkt6cMAX.Text = "" OrElse RadTextBoxPunkt6cNLC.Text = "" OrElse CDec(RadTextBoxPunkt6cNLC.Text) > 10000000 Then
            RadCheckBoxPunkt6c.Checked = False
        ElseIf CDec(RadTextBoxPunkt6cMAX.Text) > CDec(RadTextBoxPunkt6cNLC.Text) Then
            RadCheckBoxPunkt6c.Checked = False
        Else
            RadCheckBoxPunkt6c.Checked = True

        End If


        '(6d) Totlast des Lastträgers der Waagenbrücke und Mindestvorlast der WZ  in kg
        RadTextBoxPunkt6dDL.Text = (objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis) / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen
        '=WENN('Daten-Eingabe'!$G$38="";"";'Daten-Eingabe'!$G$38)
        RadTextBoxPunkt6dEMin.Text = objEichprozess.Lookup_Waegezelle.Mindestvorlast


        'runden auf 0 nachkommastellen
        If RadTextBoxPunkt6dDL.Visible = True Then
            If Not RadTextBoxPunkt6dDL.Text.Equals("") Then
                RadTextBoxPunkt6dDL.Text = CInt(RadTextBoxPunkt6dDL.Text)
            End If
        End If

        '=WENN(ODER($E$58="";$G$58="");"NEIN";WENN($G$58>$E$58;"NEIN";"JA"))
        If RadTextBoxPunkt6dDL.Text = "" OrElse RadTextBoxPunkt6dDL.Text = "" OrElse RadTextBoxPunkt6dEMin.Text = "" Then
            RadCheckBoxPunkt6d.Checked = False
        ElseIf CDec(RadTextBoxPunkt6dEMin.Text) > CDec(RadTextBoxPunkt6dDL.Text) Then
            RadCheckBoxPunkt6d.Checked = False
        Else
            RadCheckBoxPunkt6d.Checked = True
        End If


        '(7) Eichwert der NSW und kleinster zulässiger Teilungswert der WZ  in  kg
        '=WENN('Daten-Eingabe'!$G$16="";"";WENN('Daten-Eingabe'!$G$16=0;"";'Daten-Eingabe'!$H$11*'Daten-Eingabe'!$G$15/'Daten-Eingabe'!$G$16^0,5))
        RadTextBoxPunkt7e1.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen ^ 0.5

        '=WENN('Daten-Eingabe'!$G$37="";""; WENN('Daten-Eingabe'!$G$42<>0;'Daten-Eingabe'!$G$37/'Daten-Eingabe'!$G$42; WENN('Daten-Eingabe'!$G$41<>0;'Daten-Eingabe'!$G$41; WENN('Daten-Eingabe'!$G$40=0;"";
        'Daten-Eingabe'!$G$37/'Daten-Eingabe'!$G$40))))
        If objEichprozess.Lookup_Waegezelle.Hoechsteteilungsfaktor <> "0" And objEichprozess.Lookup_Waegezelle.Hoechsteteilungsfaktor <> "" Then
            RadTextBoxPunkt7eMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast / objEichprozess.Lookup_Waegezelle.Hoechsteteilungsfaktor
        ElseIf objEichprozess.Lookup_Waegezelle.MinTeilungswert <> "0" And objEichprozess.Lookup_Waegezelle.MinTeilungswert <> "" Then
            RadTextBoxPunkt7eMax.Text = objEichprozess.Lookup_Waegezelle.MinTeilungswert
        Else
            RadTextBoxPunkt7eMax.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast / objEichprozess.Lookup_Waegezelle.MaxAnzahlTeilungswerte
        End If

        'runden auf 3 Nachomastellen
        RadTextBoxPunkt7e1.Text = Decimal.Round(CDec(RadTextBoxPunkt7e1.Text), 4, MidpointRounding.AwayFromZero)

        '=WENN(ODER($E$63="";$G$63="");"NEIN";WENN($G$63>$E$63;"NEIN";"JA"))
        If RadTextBoxPunkt7e1.Text = "" OrElse RadTextBoxPunkt7eMax.Text = "" Then
            RadCheckBoxPunkt7.Checked = False
        ElseIf CDec(RadTextBoxPunkt7eMax.Text) > CDec(RadTextBoxPunkt7e1.Text) Then
            RadCheckBoxPunkt7.Checked = False
        Else
            RadCheckBoxPunkt7.Checked = True
        End If



        '(8) Mindesteingangssignal für AWG, Mindestmesssignal pro Eichwert und Berechnung
        '=WENN('Daten-Eingabe'!$G$39="";"";WENN(ODER('Daten-Eingabe'!$G$37=0;'Daten-Eingabe'!$G$16=0);"";'Daten-Eingabe'!$G$39*'Daten-Eingabe'!$G$27*'Daten-Eingabe'!$G$15*'Daten-Eingabe'!$G$19/('Daten-Eingabe'!$G$37*'Daten-Eingabe'!$G$16)))
        RadTextBoxPunkt8U.Text = objEichprozess.Lookup_Waegezelle.Waegezellenkennwert * objEichprozess.Lookup_Auswertegeraet.Speisespannung * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast / (objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen)
        RadTextBoxPunkt8UMin.Text = objEichprozess.Lookup_Auswertegeraet.Mindesteingangsspannung
        RadTextBoxPunkt8D.Text = objEichprozess.Lookup_Waegezelle.Waegezellenkennwert * 1000 * objEichprozess.Lookup_Auswertegeraet.Speisespannung * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 / (objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast * objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen)
        RadTextBoxPunkt8DMIN.Text = objEichprozess.Lookup_Auswertegeraet.Mindestmesssignal

        'runden auf 2 nachkommastellen
        If Not RadTextBoxPunkt8U.Text = "" Then
            RadTextBoxPunkt8U.Text = Math.Round(CDec(RadTextBoxPunkt8U.Text), 2, MidpointRounding.AwayFromZero)

        End If
        If Not RadTextBoxPunkt8D.Text = "" Then
            RadTextBoxPunkt8D.Text = Math.Round(CDec(RadTextBoxPunkt8D.Text), 2, MidpointRounding.AwayFromZero)
        End If

        '=WENN(ODER($D$68>1000000;$G$68="";$D$68="");"NEIN";WENN($G$68>$D$68;"NEIN";"JA"))
        If RadTextBoxPunkt8U.Text = "" OrElse RadTextBoxPunkt8UMin.Text = "" OrElse CDec(RadTextBoxPunkt8U.Text) > 1000000 Then
            RadCheckBoxPunkt8U.Checked = False
        ElseIf CDec(RadTextBoxPunkt8UMin.Text) > CDec(RadTextBoxPunkt8U.Text) Then
            RadCheckBoxPunkt8U.Checked = False
        Else
            RadCheckBoxPunkt8U.Checked = True
        End If

        '=WENN(ODER($D$71>1000000;$G$71="";$D$71="");"NEIN";WENN($G$71>$D$71;"NEIN";"JA"))
        If RadTextBoxPunkt8D.Text = "" OrElse RadTextBoxPunkt8DMIN.Text = "" OrElse CDec(RadTextBoxPunkt8D.Text) > 1000000 Then
            RadCheckBoxPunkt8D.Checked = False
        ElseIf CDec(RadTextBoxPunkt8DMIN.Text) > CDec(RadTextBoxPunkt8D.Text) Then
            RadCheckBoxPunkt8D.Checked = False
        Else
            RadCheckBoxPunkt8D.Checked = True
        End If

        '(9) Vergleich der Lastwiderstände von AWG und WZ  in  W
        '=WENN('Daten-Eingabe'!$G$30="";"fehlt";'Daten-Eingabe'!$G$30)
        RadTextBoxPunkt9RLmin.Text = objEichprozess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMIN
        '=WENN('Daten-Eingabe'!G45="";"R LC  fehlt";WENN('Daten-Eingabe'!$G$16="";"";WENN('Daten-Eingabe'!$G$16=0;"";'Daten-Eingabe'!$G$45/'Daten-Eingabe'!$G$16)))
        RadTextBoxPunkt9RLC.Text = objEichprozess.Lookup_Waegezelle.WiderstandWaegezelle / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

        '=WENN('Daten-Eingabe'!$H$30>1000000;"fehlt";WENN('Daten-Eingabe'!$H$30="";"fehlt";'Daten-Eingabe'!$H$30))
        If objEichprozess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX <> "0" And objEichprozess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX <> "" And objEichprozess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX > 1000000 Then
            RadTextBoxPunkt9RLMax.Text = ""
        Else
            RadTextBoxPunkt9RLMax.Text = objEichprozess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX
        End If

        'runden auf 0 Nachkommastellen
        If Not RadTextBoxPunkt9RLC.Text.Equals("") Then
            RadTextBoxPunkt9RLC.Text = CInt(RadTextBoxPunkt9RLC.Text)
        End If

        '=WENN($C$76=0;"NEIN";WENN($G$76="fehlt";"NEIN";WENN($E$76="";"NEIN";WENN($E$76<$C$76;"NEIN";WENN($E$76>$G$76;"NEIN";"JA")))))
        If RadTextBoxPunkt9RLmin.Text = "" OrElse RadTextBoxPunkt9RLC.Text = "" OrElse RadTextBoxPunkt9RLMax.Text = "" Then
            RadCheckBoxPunkt9.Checked = False
        ElseIf CDec(RadTextBoxPunkt9RLC.Text) < CDec(RadTextBoxPunkt9RLmin.Text) OrElse CDec(RadTextBoxPunkt9RLC.Text) > CDec(RadTextBoxPunkt9RLMax.Text) Then
            RadCheckBoxPunkt9.Checked = False
        Else
            RadCheckBoxPunkt9.Checked = True
        End If

        '(10) Verlängerungskabel zum Anschluss der WZ: Kabellänge pro Leiterquerschnitt  in  m/mm²
        RadTextBoxPunkt10LA.Text = objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge / objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt
        RadTextBoxPunkt10LAMax.Text = objEichprozess.Lookup_Auswertegeraet.KabellaengeQuerschnitt

        'runden auf 2 nachkommastellen
        RadTextBoxPunkt10LA.Text = Math.Round(CDec(RadTextBoxPunkt10LA.Text), 2, MidpointRounding.AwayFromZero)

        '=WENN($G$81>1000000;"NEIN";WENN($G$81="";"NEIN";WENN($G$81<$E$81;"NEIN";"JA")))
        If RadTextBoxPunkt10LA.Text = "" OrElse RadTextBoxPunkt10LAMax.Text = "" OrElse CDec(RadTextBoxPunkt10LAMax.Text) > 1000000 Then
            RadCheckBoxPunkt10.Checked = False
        ElseIf CDec(RadTextBoxPunkt10LAMax.Text) < CDec(RadTextBoxPunkt10LA.Text) Then
            RadCheckBoxPunkt10.Checked = False
        Else
            RadCheckBoxPunkt10.Checked = True
        End If


        'je nach Art der waage Steuerlemente ein oder ausblenden
        Using Context As New EichsoftwareClientdatabaseEntities1
            If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
                objEichprozess = (From a In Context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End If
            Dim Waagenart = (From dbWaagenart In Context.Lookup_Waagenart Select dbWaagenart Where dbWaagenart.ID = objEichprozess.FK_WaagenArt).FirstOrDefault
            If Not Waagenart Is Nothing Then


                'je nach Art der Waage fallen bestimmte Punkte weg oder Formeln ändern sich
                If Waagenart.Art = "Einbereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max+DL+IZSR+NUD+T+)/Max ="
                    lblPunkt5QMAX.Text = "(Q*Max*R)/N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "" 'gibts heir nicht

                ElseIf Waagenart.Art = "Zweibereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = True
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "<html>Last-Korrekturfaktor: Q = (Max₂+DL+IZSR+NUD+T+)/Max<span lang="">₂</span>=</html>" '"Last-Korrekturfaktor: Q = (Max2+DL+IZSR+NUD+T+)/Max2 ="
                    lblPunkt5QMAX.Text = "(Q * Max₂ * R) / N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "0,4*Max₂ /e₁"
                ElseIf Waagenart.Art = "Dreibereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = True
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "<html>Last-Korrekturfaktor: Q = (Max₃+DL+IZSR+NUD+T+)/Max₃<span lang="">₃</span>=</html>" '"Last-Korrekturfaktor: Q = (Max3+DL+IZSR+NUD+T+)/Max3 ="
                    lblPunkt5QMAX.Text = "(Q * Max₃ * R) / N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "0,4*Max₃ /e₁"
                ElseIf Waagenart.Art = "Zweiteilungswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = True
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "<html>Last-Korrekturfaktor: Q = (Max₂+DL+IZSR+NUD+T+)/Max<span lang="">₂</span>=</html>" '"Last-Korrekturfaktor: Q = (Max2+DL+IZSR+NUD+T+)/Max2 ="
                    lblPunkt5QMAX.Text = "(Q*Max₂*R)/N"
                    lblPunkt6bMax.Text = "Max₂ / e₁"
                    lblPunkt6cMax.Text = "" 'gibts heir nicht
                ElseIf Waagenart.Art = "Dreiteilungswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = True
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "<html>Last-Korrekturfaktor: Q = (Max₃+DL+IZSR+NUD+T+)/Max<span lang="">₃</span>=</html>" ' "Last-Korrekturfaktor: Q = (Max3+DL+IZSR+NUD+T+)/Max3 ="
                    lblPunkt5QMAX.Text = "(Q*Max₃*R)/N"
                    lblPunkt6bMax.Text = "Max₃ / e₁"
                    lblPunkt6cMax.Text = "" 'gibts heir nicht
                End If

                Select Case My.Settings.AktuelleSprache.ToLower
                    Case Is = "de"
                        lblPunkt4WaagenArt.Text = Waagenart.Art
                        lblPunkt6aWaagenart.Text = Waagenart.Art
                    Case Is = "en"
                        lblPunkt4WaagenArt.Text = Waagenart.Art_EN
                        lblPunkt6aWaagenart.Text = Waagenart.Art_EN
                    Case Is = "pl"
                        lblPunkt4WaagenArt.Text = Waagenart.Art_PL
                        lblPunkt6aWaagenart.Text = Waagenart.Art_PL
                    Case Else
                        lblPunkt4WaagenArt.Text = Waagenart.Art_EN
                        lblPunkt6aWaagenart.Text = Waagenart.Art_EN
                End Select
            End If
        End Using

        'Je nach Erfolg der Prüfung werden die Checkboxen eingefärbt
        For Each GroupBox In FlowLayoutPanel1.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadCheckBox Then
                        If CType(Control, Telerik.WinControls.UI.RadCheckBox).Visible = True AndAlso CType(Control, Telerik.WinControls.UI.RadCheckBox).Checked = False Then

                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).GroupBoxElement.BorderColor = Color.Red
                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).GroupBoxElement.BorderWidth = 4
                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).BackColor = Color.FromArgb(4, Color.Red)
                        Else
                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).GroupBoxElement.BorderColor = System.Drawing.Color.FromArgb(255, 196, 199, 182)
                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).GroupBoxElement.BorderWidth = 1
                            CType(GroupBox, Telerik.WinControls.UI.RadGroupBox).BackColor = Color.White
                        End If
                    End If
                Next
            End If
        Next




        _suspendEvents = False
    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        AbortSaveing = False
        'prüfen ob alle Felder ausgefüllt sind
        For Each GroupBox In FlowLayoutPanel1.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadCheckBox Then
                        If CType(Control, Telerik.WinControls.UI.RadCheckBox).Visible = True AndAlso CType(Control, Telerik.WinControls.UI.RadCheckBox).Checked = False Then
                            CType(Control, Telerik.WinControls.UI.RadCheckBox).Focus()
                            MessageBox.Show(My.Resources.GlobaleLokalisierung.KompatiblitätNichtGeleistet, My.Resources.GlobaleLokalisierung.Fehler, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            AbortSaveing = True
                            Return False
                        End If

                    End If
                Next
            End If
        Next
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True

    End Function


#End Region
#Region "Events"

    ''' <summary>
    ''' event zum unterbinden des ändern des Checkstates. Da es kein direktes readonly in den checkboxen gibt von Telerik
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub RadCheckBoxPunkt_ToggleStateChanging(sender As Object, args As Telerik.WinControls.UI.StateChangingEventArgs) Handles RadCheckBoxPunkt1.ToggleStateChanging, _
        RadCheckBoxPunkt2TMin.ToggleStateChanging, RadCheckBoxPunkt2TMAX.ToggleStateChanging, RadCheckBoxPunkt3.ToggleStateChanging, _
        RadCheckBoxPunkt4Max1.ToggleStateChanging, RadCheckBoxPunkt4Max2.ToggleStateChanging, RadCheckBoxPunkt4Max3.ToggleStateChanging, _
        RadCheckBoxPunkt5.ToggleStateChanging, RadCheckBoxPunkt6aMax1.ToggleStateChanging, RadCheckBoxPunkt6aMax2.ToggleStateChanging, _
        RadCheckBoxPunkt6aMax3.ToggleStateChanging, RadCheckBoxPunkt6b.ToggleStateChanging, RadCheckBoxPunkt6c.ToggleStateChanging, _
        RadCheckBoxPunkt6d.ToggleStateChanging, RadCheckBoxPunkt7.ToggleStateChanging, RadCheckBoxPunkt8D.ToggleStateChanging, _
        RadCheckBoxPunkt8U.ToggleStateChanging, RadCheckBoxPunkt9.ToggleStateChanging, RadCheckBoxPunkt10.ToggleStateChanging

        If _suspendEvents = True Then Exit Sub 'Variable zum temporären stoppen der Eventlogiken (z.b. selected index changed beim laden des Formulars)

        args.Cancel = True

    End Sub

#End Region
#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If DialogModus = enuDialogModus.lesend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If
       
            If DialogModus = enuDialogModus.korrigierend Then
                If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung Then
                    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                End If
                ParentFormular.CurrentEichprozess = objEichprozess
                Exit Sub
            End If

            If ValidateControls() Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If objEichprozess.ID <> 0 Then 'Neue ID also CREATE Operation
                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dbObjBeschaffenheitspruefung As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.Vorgangsnummer = objEichprozess.Vorgangsnummer)
                        If Not dbObjBeschaffenheitspruefung Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dbObjBeschaffenheitspruefung

                            ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                            If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung Then
                                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung
                            End If

                            'Speichern in Datenbank
                            Context.SaveChanges()

                        End If
                    End If
                    ParentFormular.CurrentEichprozess = objEichprozess
                End Using
            End If
        End If

    End Sub

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoKompatiblititaetsnachweisErgebnis))
        Me.lblFabriknummer.Text = resources.GetString("lblFabriknummer.Text")
        Me.lblPunkt1AWG.Text = resources.GetString("lblPunkt1AWG.Text")
        Me.lblPunkt1GleichBesser1.Text = resources.GetString("lblPunkt1GleichBesser1.Text")
        Me.lblPunkt1Gleichbesser2.Text = resources.GetString("lblPunkt1Gleichbesser2.Text")
        Me.lblPunkt1Waage.Text = resources.GetString("lblPunkt1Waage.Text")
        Me.lblPunkt1WZ.Text = resources.GetString("lblPunkt1WZ.Text")
        Me.lblPunkt2AWG.Text = resources.GetString("lblPunkt2AWG.Text")
        Me.lblPunkt2Waage.Text = resources.GetString("lblPunkt2Waage.Text")
        Me.lblPunkt2WZ.Text = resources.GetString("lblPunkt2WZ.Text")
        Me.lblPunkt4WaagenArt.Text = resources.GetString("lblPunkt4WaagenArt.Text")
        Me.lblPunkt5Faktor.Text = resources.GetString("lblPunkt5Faktor.Text")
        Me.lblPunkt6aWaagenart.Text = resources.GetString("lblPunkt6aWaagenart.Text")
        Me.lblPunkt8Eingangspannung.Text = resources.GetString("lblPunkt8Eingangspannung.Text")
        Me.lblPunkt8MinVol.Text = resources.GetString("lblPunkt8MinVol.Text")
        Me.lblPunkt8OhneLast.Text = resources.GetString("lblPunkt8OhneLast.Text")

        Me.RadGroupBoxPunkt1.Text = resources.GetString("RadGroupBoxPunkt1.Text")
        Me.RadGroupBoxPunkt2TMin.Text = resources.GetString("RadGroupBoxPunkt2TMin.Text")
        Me.RadGroupBoxPunkt3.Text = resources.GetString("RadGroupBoxPunkt3.Text")
        Me.RadGroupBoxPunkt4.Text = resources.GetString("RadGroupBoxPunkt4.Text")
        Me.RadGroupBoxPunkt5.Text = resources.GetString("RadGroupBoxPunkt5.Text")
        Me.RadGroupBoxPunkt6a.Text = resources.GetString("RadGroupBoxPunkt6a.Text")
        Me.RadGroupBoxPunkt6b.Text = resources.GetString("RadGroupBoxPunkt6b.Text")
        Me.RadGroupBoxPunkt6c.Text = resources.GetString("RadGroupBoxPunkt6c.Text")
        Me.RadGroupBoxPunkt6d.Text = resources.GetString("RadGroupBoxPunkt6d.Text")
        Me.RadGroupBoxPunkt7.Text = resources.GetString("RadGroupBoxPunkt7.Text")
        Me.RadGroupBoxPunkt8.Text = resources.GetString("RadGroupBoxPunkt8.Text")
        Me.RadGroupBoxPunkt9.Text = resources.GetString("RadGroupBoxPunkt9.Text")
        Me.RadGroupBoxPunkt10.Text = resources.GetString("RadGroupBoxPunkt10.Text")

        Using Context As New EichsoftwareClientdatabaseEntities1
            If Not DialogModus = enuDialogModus.lesend And Not DialogModus = enuDialogModus.korrigierend Then
                objEichprozess = (From a In Context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            End If

            Dim Waagenart = (From dbWaagenart In Context.Lookup_Waagenart Select dbWaagenart Where dbWaagenart.ID = objEichprozess.FK_WaagenArt).FirstOrDefault
            Waagenart = objEichprozess.Lookup_Waagenart

            If Not Waagenart Is Nothing Then

                'je nach Art der Waage fallen bestimmte Punkte weg oder Formeln ändern sich
                If Waagenart.Art = "Einbereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max+DL+IZSR+NUD+T+)/Max ="
                    lblPunkt5QMAX.Text = "(Q*Max*R)/N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "" 'gibts heir nicht

                ElseIf Waagenart.Art = "Zweibereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = True
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max2+DL+IZSR+NUD+T+)/Max2 ="
                    lblPunkt5QMAX.Text = "(Q * Max2 * R) / N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "0,4*Max2 /e1"

                ElseIf Waagenart.Art = "Dreibereichswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = False
                    RadGroupBoxPunkt6c.Visible = True
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max3+DL+IZSR+NUD+T+)/Max3 ="
                    lblPunkt5QMAX.Text = "(Q * Max3 * R) / N"
                    lblPunkt6bMax.Text = "" 'gibts heir nicht
                    lblPunkt6cMax.Text = "0,4*Max3 /e1"
                ElseIf Waagenart.Art = "Zweiteilungswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = True
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max2+DL+IZSR+NUD+T+)/Max2 ="
                    lblPunkt5QMAX.Text = "(Q*Max2*R)/N"
                    lblPunkt6bMax.Text = "Max2 / e1"
                    lblPunkt6cMax.Text = "" 'gibts heir nicht
                ElseIf Waagenart.Art = "Dreiteilungswaage" Then
                    RadGroupBoxPunkt6a.Visible = True
                    RadGroupBoxPunkt6b.Visible = True
                    RadGroupBoxPunkt6c.Visible = False
                    RadGroupBoxPunkt6d.Visible = True

                    lblPunkt5Faktor.Text = "Last-Korrekturfaktor: Q = (Max3+DL+IZSR+NUD+T+)/Max3 ="
                    lblPunkt5QMAX.Text = "(Q*Max3*R)/N"
                    lblPunkt6bMax.Text = "Max3 / e1"
                    lblPunkt6cMax.Text = "" 'gibts heir nicht
                End If

                Select Case My.Settings.AktuelleSprache.ToLower
                    Case Is = "de"
                        lblPunkt4WaagenArt.Text = Waagenart.Art
                        lblPunkt6aWaagenart.Text = Waagenart.Art
                    Case Is = "en"
                        lblPunkt4WaagenArt.Text = Waagenart.Art_EN
                        lblPunkt6aWaagenart.Text = Waagenart.Art_EN
                    Case Is = "pl"
                        lblPunkt4WaagenArt.Text = Waagenart.Art_PL
                        lblPunkt6aWaagenart.Text = Waagenart.Art_PL
                    Case Else
                        lblPunkt4WaagenArt.Text = Waagenart.Art_EN
                        lblPunkt6aWaagenart.Text = Waagenart.Art_EN
                End Select
            End If

        End Using

        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisErgebnisHilfe)
                'Überschrift setzen
                ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_KompatiblitaetsnachweisErgebnis
            Catch ex As Exception
            End Try
        End If

     

    End Sub

    ''' <summary>
    ''' aktualisieren der Oberfläche wenn nötig
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Friend Overrides Sub UpdateNeeded(UserControl As UserControl)
        MyBase.UpdateNeeded(UserControl)
        'Hilfetext setzen
        ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisErgebnisHilfe)
        'Überschrift setzen
        ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_KompatiblitaetsnachweisErgebnis
        If Me.Equals(UserControl) Then
            LoadFromDatabase()
        End If
    End Sub



    'Entsperrroutine
    Protected Friend Overrides Sub EntsperrungNeeded()
        MyBase.EntsperrungNeeded()

        'Hiermit wird ein lesender Vorgang wieder entsperrt. 
        For Each Control In Me.FlowLayoutPanel1.Controls
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
    Protected Friend Overrides Sub VersendenNeeded(TargetUserControl As UserControl)
        MyBase.VersendenNeeded(TargetUserControl)

        If Me.Equals(TargetUserControl) Then

            Using dbcontext As New EichsoftwareClientdatabaseEntities1
                objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Beschaffenheitspruefung").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Eichbevollmächtigter sich den DS von Anfang angucken muss

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
#End Region



End Class
