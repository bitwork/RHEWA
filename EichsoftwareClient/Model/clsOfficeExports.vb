Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Word
Imports Microsoft.VisualBasic
''' <summary>
''' Code von Marco Pelster bitwork GmbH
''' </summary>
''' <remarks></remarks>
Public Class clsOfficeExports

    ''' <summary>
    ''' Kompatiblitätsnachweis DEUTSCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportKompatiblitaetsnachweisDE(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim objExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objExcelWorksheetDatenEingabe As Microsoft.Office.Interop.Excel.Worksheet

        Dim objExcelWorksheetTabelle1 As Microsoft.Office.Interop.Excel.Worksheet
        Dim ExcelSavePath As String
        Dim DocumentName As String = "Kompatibilitätsnachweis DE" & "_" & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & ".xls"
        For Each c As Char In System.IO.Path.GetInvalidFileNameChars()
            DocumentName = DocumentName.Replace(c, "_"c)
        Next
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Kompatibilitätsnachweis_DE
        Dim FolderBrowserDialog As New FolderBrowserDialog

        'Template excel dokument kopieren an Ort den der Benutzer über FolderBrowserDialog angibt
        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ExcelSavePath = FolderBrowserDialog.SelectedPath

            'Dokumentpfad erstellen
            CompletePath = ExcelSavePath & "\" & DocumentName

            'Dokument abspeichern
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            'excel dokument öffnen
            objExcelWorkbook = objExcelApp.Workbooks.Open(CompletePath)

            'Worksheets zuweisen
            objExcelWorksheetDatenEingabe = objExcelWorkbook.Worksheets("Daten-Eingabe")
            objExcelWorksheetTabelle1 = objExcelWorkbook.Worksheets("Tabelle1")

            'Die Rows stehen vorne und die Collumns hinten. G20 Entspricht also (20=20, 7=G).
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'AUSWAHLFELDER ANFANG
            '_________________________________________________________________________________________________________________________________

            'Waagentyp NSW befüllen in A14 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(14, 1).value = pEichProzess.Lookup_Waagentyp.Typ

            'Waagentyp  NSW befüllen in A10 auf Tabelle1
            If pEichProzess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 1
            End If

            If pEichProzess.Lookup_Waagenart.Art = "Zweibereichswaage" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 2
            End If

            If pEichProzess.Lookup_Waagenart.Art = "Dreibereichswaage" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 3
            End If

            If pEichProzess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 4
            End If

            If pEichProzess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 5
            End If

            '_________________________________________________________________________________________________________________________________
            'AUSWAHLFELDER ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'NSW ANFANG
            '_________________________________________________________________________________________________________________________________

            'Anschrift Waagenbaufirma befüllen in C4 TEIL1 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(4, 3).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller

            'Anschrift Waagenbaufirma befüllen in C5 TEIL2 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(5, 3).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort

            'Verbindungselemente_BruchteilEichfehlergrenze in G49 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(49, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze

            'Waage_AdditiveTarahoechstlast befüllen in G20 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(20, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast

            'Waage_AnzahlWaegezellen befüllen in G16 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(16, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

            'Waage_Bauartzulassung befüllen in A19 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(19, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung

            'Waage_Ecklastzuschlag befüllen in G18 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(18, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag

            If pEichProzess.Lookup_Waagenart.Art = "Einbereichswaage" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(11, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

            End If

            If pEichProzess.Lookup_Waagenart.Art = "Zweibereichswaage" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

            ElseIf pEichProzess.Lookup_Waagenart.Art = "Zweiteilungswaage" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

            End If

            If pEichProzess.Lookup_Waagenart.Art = "Dreibereichswaage" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                'Waage_Eichwert3 befüllen in H14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

            ElseIf pEichProzess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then

                'Waage_Eichwert1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                'Waage_Eichwert3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

            End If

            'Waage_Einschaltnullstellbereich befüllen in G17 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(17, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich

            'Waage_FabrikNummer befüllen in A12 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(12, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

            'Waage_Genauigkeitsklasse befüllen in G10 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(10, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse

            'Waage_GrenzenTemperaturbereichMAX befüllen in H21 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(21, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX

            'Waage_GrenzenTemperaturbereichMIN befüllen in G21 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(21, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN

            If pEichProzess.Lookup_Waagenart.Art = "Einbereichswaage" Then

                'Waage_Hoechstlast1 befüllen in G11 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(11, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

            End If

            If pEichProzess.Lookup_Waagenart.Art = "Zweibereichswaage" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

            ElseIf pEichProzess.Lookup_Waagenart.Art = "Zweiteilunsgwaage" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

            End If

            If pEichProzess.Lookup_Waagenart.Art = "Dreibereichswaage" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

                'Waage_Hoechstlast3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3()

            ElseIf pEichProzess.Lookup_Waagenart.Art = "Dreiteilungswaage" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

                'Waage_Hoechstlast3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3()

            End If

            'Waage_Kabellaenge befüllen in G22 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(22, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge

            'Waage_Kabelquerschnitt befüllen in G23 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(23, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt

            'Waage_Revisionsnummer befüllen in UNBEKANNT !

            'Waage_Totlast befüllen in G19 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(19, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast

            'Waage_Uebersetzungsverhaeltnis befüllen in G15 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(15, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis.ToString.Replace(",", ".")

            'Waage_Zulassungsinhaber befüllen in A23 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(23, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber

            'WZ_Hoechstlast befüllen in G37 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(37, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Split(";")(0)

            '_________________________________________________________________________________________________________________________________
            'NSW ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'AWG ANFANG
            '_________________________________________________________________________________________________________________________________

            'Anschlussart AWG befüllen G33 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(33, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart

            'Bauartzulassung AWG befüllen in A34 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(34, 1).value = pEichProzess.Lookup_Auswertegeraet.Bauartzulassung

            'BruchteilEichfehlergrenze AWG befüllen in G32 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(32, 7).value = pEichProzess.Lookup_Auswertegeraet.BruchteilEichfehlergrenze

            'Genauigkeitsklasse AWG befüllen in G25 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(25, 7).value = pEichProzess.Lookup_Auswertegeraet.Genauigkeitsklasse

            'GrenzwertLastwiderstandMAX AWG befüllen in H30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 8).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX

            'GrenzwertLastwiderstandMIN AWG befüllen in G30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 7).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMIN

            'Hersteller AWG befüllen in A27 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(27, 1).value = pEichProzess.Lookup_Auswertegeraet.Hersteller

            'KabellaengeQuerschnitt AWG befüllen in G34 auf Daten-Eingabe
            If IsNumeric(pEichProzess.Lookup_Auswertegeraet.KabellaengeQuerschnitt) Then
                objExcelWorksheetDatenEingabe.Cells(34, 7).value = CDec(pEichProzess.Lookup_Auswertegeraet.KabellaengeQuerschnitt)
            End If
            If Not pEichProzess.Lookup_Waagenart.Art = "Einbereichswaage" Then
                'MAXAnzahlTeilungswerteMehrbereichswaage AWG befüllen in G26 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(26, 7).Value = pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
            Else

                'MAXAnzahlTeilungswerteEinbereichswaage AWG befüllen in G26 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(26, 7).Value = pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteEinbereichswaage

            End If

            'GrenzwertTemperaturbereichMAX AWG befüllen in H31 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(31, 8).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX

            'GrenzwertTemperaturbereichMIN AWG befüllen in G31 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(31, 7).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN

            'Mindesteingangsspannung AWG befüllen in G28 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(28, 7).value = pEichProzess.Lookup_Auswertegeraet.Mindesteingangsspannung

            'Mindestmesssignal AWG befüllen in G29 auf Daten-Eingabe
            If IsNumeric(pEichProzess.Lookup_Auswertegeraet.Mindestmesssignal) Then
                objExcelWorksheetDatenEingabe.Cells(29, 7).value = CDec(pEichProzess.Lookup_Auswertegeraet.Mindestmesssignal)
            End If

            'Pruefbericht AWG befüllen in A32 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(32, 1).value = pEichProzess.Lookup_Auswertegeraet.Pruefbericht

            'Speisespannung AWG befüllen in G27 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(27, 7).value = pEichProzess.Lookup_Auswertegeraet.Speisespannung

            'Typ befüllen AWG in A30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 1).value = pEichProzess.Lookup_Auswertegeraet.Typ

            '_________________________________________________________________________________________________________________________________
            'AWG ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'WZ ANFANG
            '_________________________________________________________________________________________________________________________________

            'Bauartzulassung WZ befüllen in A47 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(47, 1).value = pEichProzess.Lookup_Waegezelle.Bauartzulassung

            'BruchteilEichfehlergrenze WZ befüllen in G47 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(47, 7).value = pEichProzess.Lookup_Waegezelle.BruchteilEichfehlergrenze

            'Genauigkeitsklasse WZ befüllen in G36 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(36, 7).value = pEichProzess.Lookup_Waegezelle.Genauigkeitsklasse

            'GrenzwertTemperaturbereichMAX WZ befüllen in H46 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(46, 8).value = pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX

            'GrenzwertTemperaturbereichMIN WZ befüllen in G46 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(46, 7).value = pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN

            'Hersteller WZ befüllen in A38 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(38, 1).value = pEichProzess.Lookup_Waegezelle.Hersteller

            'Hoechsteteilungsfaktor WZ befüllen in G42 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(42, 7).value = pEichProzess.Lookup_Waegezelle.Hoechsteteilungsfaktor

            'Kriechteilungsfaktor WZ befüllen in G43 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(43, 7).value = pEichProzess.Lookup_Waegezelle.Kriechteilungsfaktor

            'MaxAnzahlTeilungswerte WZ befüllen in G40 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(40, 7).value = pEichProzess.Lookup_Waegezelle.MaxAnzahlTeilungswerte

            'Mindestvorlast WZ befüllen in G38 auf Daten-Eingabe
            If Not pEichProzess.Lookup_Waegezelle.MindestvorlastProzent Is Nothing Then
                objExcelWorksheetDatenEingabe.Cells(38, 7).value = (pEichProzess.Lookup_Waegezelle.MindestvorlastProzent / 100) * pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

            Else
                objExcelWorksheetDatenEingabe.Cells(38, 7).value = pEichProzess.Lookup_Waegezelle.Mindestvorlast

            End If

            'MinTeilungswert WZ befüllen in G41 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(41, 7).value = pEichProzess.Lookup_Waegezelle.MinTeilungswert

            'Pruefbericht WZ befüllen in A44 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(44, 1).value = pEichProzess.Lookup_Waegezelle.Pruefbericht

            'RueckkehrVorlastsignal WZ befüllen in G44 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(44, 7).value = pEichProzess.Lookup_Waegezelle.RueckkehrVorlastsignal

            'Waegezellenkennwert WZ befüllen in G39 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(39, 7).value = pEichProzess.Lookup_Waegezelle.Waegezellenkennwert

            'Typ WZ befüllen in A41 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(41, 1).value = pEichProzess.Lookup_Waegezelle.Typ

            'WiderstandWaegezelle WZ befüllen in G45 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(45, 7).value = pEichProzess.Lookup_Waegezelle.WiderstandWaegezelle

            '_________________________________________________________________________________________________________________________________
            'WZ ENDE
            '_________________________________________________________________________________________________________________________________

            'aufrufen des Ausblenden Markos welche die EWerte Positioniert und je nach Waagenart die anderen Ergebnis Sheets einblendet
            objExcelApp.Run("Ausblenden")

            'excel dokument speichern
            objExcelWorkbook.Save()
            objExcelWorkbook.Close()
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorksheetDatenEingabe) : objExcelWorksheetDatenEingabe = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorkbook) : objExcelWorkbook = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelApp) : objExcelApp = Nothing

            Catch ex As Exception

            End Try

            'excel dokument Anzeigen
            Process.Start(CompletePath)
        End If
    End Sub

    ''' <summary>
    ''' Kompatiblitätsnachweis ENGLISCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportKompatiblitaetsnachweisEN(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim objExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objExcelWorksheetDatenEingabe As Microsoft.Office.Interop.Excel.Worksheet
        Dim objExcelWorksheetTabelle1 As Microsoft.Office.Interop.Excel.Worksheet
        Dim ExcelSavePath As String
        Dim DocumentName As String = "Kompatibilitätsnachweis EN" & "_" & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & ".xls"
        For Each c As Char In System.IO.Path.GetInvalidFileNameChars()
            DocumentName = DocumentName.Replace(c, "_"c)
        Next
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Kompatibilitätsnachweis_EN
        Dim FolderBrowserDialog As New FolderBrowserDialog

        'Template excel dokument kopieren an Ort den der Benutzer über FolderBrowserDialog angibt
        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ExcelSavePath = FolderBrowserDialog.SelectedPath

            'Dokumentpfad erstellen
            CompletePath = ExcelSavePath & "\" & DocumentName

            'Dokument abspeichern
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            'excel dokument öffnen
            objExcelWorkbook = objExcelApp.Workbooks.Open(CompletePath)

            'Worksheets zuweisen
            objExcelWorksheetDatenEingabe = objExcelWorkbook.Worksheets("Data-Input")
            objExcelWorksheetTabelle1 = objExcelWorkbook.Worksheets("Tabelle1")

            'Die Rows stehen vorne und die Collumns hinten. G20 Entspricht also (20=20, 7=G).
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'AUSWAHLFELDER ANFANG
            '_________________________________________________________________________________________________________________________________

            'Waagentyp NSW befüllen in A14 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(14, 1).value = pEichProzess.Lookup_Waagentyp.Typ_EN

            'Waagentyp  NSW befüllen in A10 auf Tabelle1
            If pEichProzess.Lookup_Waagenart.Art_EN = "One range WI" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 1
            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Two ranges WI" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 2
            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Three ranges WI" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 3
            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Two-interval WI" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 4
            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Three -interval WI" Then
                objExcelWorksheetTabelle1.Cells(10, 1).value = 5
            End If

            '_________________________________________________________________________________________________________________________________
            'AUSWAHLFELDER ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'NSW ANFANG
            '_________________________________________________________________________________________________________________________________

            'Anschrift Waagenbaufirma befüllen in C4 TEIL1 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(4, 3).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller

            'Anschrift Waagenbaufirma befüllen in C5 TEIL2 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(5, 3).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort

            'Verbindungselemente_BruchteilEichfehlergrenze in G49 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(49, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze

            'Waage_AdditiveTarahoechstlast befüllen in G20 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(20, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast

            'Waage_AnzahlWaegezellen befüllen in G16 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(16, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen

            'Waage_Bauartzulassung befüllen in A19 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(19, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung

            'Waage_Ecklastzuschlag befüllen in G18 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(18, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag

            If pEichProzess.Lookup_Waagenart.Art_EN = "One range WI" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(11, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Two ranges WI" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

            ElseIf pEichProzess.Lookup_Waagenart.Art_EN = "Two-interval WI" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Three ranges WI" Then

                'Waage_Eichwert1 befüllen in H12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in H13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                'Waage_Eichwert3 befüllen in H14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

            ElseIf pEichProzess.Lookup_Waagenart.Art_EN = "Three -interval WI" Then

                'Waage_Eichwert1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1

                'Waage_Eichwert2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2

                'Waage_Eichwert3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3

            End If

            'Waage_Einschaltnullstellbereich befüllen in G17 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(17, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich

            'Waage_FabrikNummer befüllen in A12 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(12, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

            'Waage_Genauigkeitsklasse befüllen in G10 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(10, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse

            'Waage_GrenzenTemperaturbereichMAX befüllen in H21 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(21, 8).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX

            'Waage_GrenzenTemperaturbereichMIN befüllen in G21 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(21, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN

            If pEichProzess.Lookup_Waagenart.Art_EN = "One range WI" Then

                'Waage_Hoechstlast1 befüllen in G11 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(11, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Two ranges WI" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

            ElseIf pEichProzess.Lookup_Waagenart.Art_EN = "Two-interval WI" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

            End If

            If pEichProzess.Lookup_Waagenart.Art_EN = "Three ranges WI" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

                'Waage_Hoechstlast3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3()

            ElseIf pEichProzess.Lookup_Waagenart.Art_EN = "Three -interval WI" Then

                'Waage_Hoechstlast1 befüllen in G12 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(12, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1()

                'Waage_Hoechstlast2 befüllen in G13 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(13, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2()

                'Waage_Hoechstlast3 befüllen in G14 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(14, 7).Value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3()

            End If

            'Waage_Kabellaenge befüllen in G22 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(22, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge

            'Waage_Kabelquerschnitt befüllen in G23 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(23, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt

            'Waage_Revisionsnummer befüllen in UNBEKANNT !

            'Waage_Totlast befüllen in G19 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(19, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast

            'Waage_Uebersetzungsverhaeltnis befüllen in G15 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(15, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis

            'Waage_Zulassungsinhaber befüllen in A23 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(23, 1).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber

            'WZ_Hoechstlast befüllen in G37 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(37, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Split(";")(0)

            '_________________________________________________________________________________________________________________________________
            'NSW ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'AWG ANFANG
            '_________________________________________________________________________________________________________________________________

            'Anschlussart AWG befüllen G33 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(33, 7).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart

            'Bauartzulassung AWG befüllen in A34 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(34, 1).value = pEichProzess.Lookup_Auswertegeraet.Bauartzulassung

            'BruchteilEichfehlergrenze AWG befüllen in G32 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(32, 7).value = pEichProzess.Lookup_Auswertegeraet.BruchteilEichfehlergrenze

            'Genauigkeitsklasse AWG befüllen in G25 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(25, 7).value = pEichProzess.Lookup_Auswertegeraet.Genauigkeitsklasse

            'GrenzwertLastwiderstandMAX AWG befüllen in H30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 8).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX

            'GrenzwertLastwiderstandMIN AWG befüllen in G30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 7).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMIN

            'Hersteller AWG befüllen in A27 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(27, 1).value = pEichProzess.Lookup_Auswertegeraet.Hersteller

            'KabellaengeQuerschnitt AWG befüllen in G34 auf Daten-Eingabe
            If IsNumeric(pEichProzess.Lookup_Auswertegeraet.KabellaengeQuerschnitt) Then
                objExcelWorksheetDatenEingabe.Cells(34, 7).value = CDec(pEichProzess.Lookup_Auswertegeraet.KabellaengeQuerschnitt)
            End If

            If Not pEichProzess.Lookup_Waagenart.Art_EN = "One range WI" Then

                'MAXAnzahlTeilungswerteMehrbereichswaage AWG befüllen in G26 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(26, 7).Value = pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage
            Else

                'MAXAnzahlTeilungswerteEinbereichswaage AWG befüllen in G26 auf Daten-Eingabe
                objExcelWorksheetDatenEingabe.Cells(26, 7).Value = pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteEinbereichswaage

            End If

            'GrenzwertTemperaturbereichMAX AWG befüllen in H31 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(31, 8).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX

            'GrenzwertTemperaturbereichMIN AWG befüllen in G31 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(31, 7).value = pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN

            'Mindesteingangsspannung AWG befüllen in G28 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(28, 7).value = pEichProzess.Lookup_Auswertegeraet.Mindesteingangsspannung

            'Mindestmesssignal AWG befüllen in G29 auf Daten-Eingabe
            If IsNumeric(pEichProzess.Lookup_Auswertegeraet.Mindestmesssignal) Then
                objExcelWorksheetDatenEingabe.Cells(29, 7).value = CDec(pEichProzess.Lookup_Auswertegeraet.Mindestmesssignal)
            End If

            'Pruefbericht AWG befüllen in A32 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(32, 1).value = pEichProzess.Lookup_Auswertegeraet.Pruefbericht

            'Speisespannung AWG befüllen in G27 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(27, 7).value = pEichProzess.Lookup_Auswertegeraet.Speisespannung

            'Typ befüllen AWG in A30 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(30, 1).value = pEichProzess.Lookup_Auswertegeraet.Typ

            '_________________________________________________________________________________________________________________________________
            'AWG ENDE
            '_________________________________________________________________________________________________________________________________

            '_________________________________________________________________________________________________________________________________
            'WZ ANFANG
            '_________________________________________________________________________________________________________________________________

            'Bauartzulassung WZ befüllen in A47 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(47, 1).value = pEichProzess.Lookup_Waegezelle.Bauartzulassung

            'BruchteilEichfehlergrenze WZ befüllen in G47 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(47, 7).value = pEichProzess.Lookup_Waegezelle.BruchteilEichfehlergrenze

            'Genauigkeitsklasse WZ befüllen in G36 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(36, 7).value = pEichProzess.Lookup_Waegezelle.Genauigkeitsklasse

            'GrenzwertTemperaturbereichMAX WZ befüllen in H46 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(46, 8).value = pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX

            'GrenzwertTemperaturbereichMIN WZ befüllen in G46 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(46, 7).value = pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN

            'Hersteller WZ befüllen in A38 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(38, 1).value = pEichProzess.Lookup_Waegezelle.Hersteller

            'Hoechsteteilungsfaktor WZ befüllen in G42 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(42, 7).value = pEichProzess.Lookup_Waegezelle.Hoechsteteilungsfaktor

            'Kriechteilungsfaktor WZ befüllen in G43 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(43, 7).value = pEichProzess.Lookup_Waegezelle.Kriechteilungsfaktor

            'MaxAnzahlTeilungswerte WZ befüllen in G40 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(40, 7).value = pEichProzess.Lookup_Waegezelle.MaxAnzahlTeilungswerte

            'Mindestvorlast WZ befüllen in G38 auf Daten-Eingabe

            If Not pEichProzess.Lookup_Waegezelle.MindestvorlastProzent Is Nothing Then
                objExcelWorksheetDatenEingabe.Cells(38, 7).value = (pEichProzess.Lookup_Waegezelle.MindestvorlastProzent / 100) * pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast

            Else
                objExcelWorksheetDatenEingabe.Cells(38, 7).value = pEichProzess.Lookup_Waegezelle.Mindestvorlast

            End If
            'MinTeilungswert WZ befüllen in G41 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(41, 7).value = pEichProzess.Lookup_Waegezelle.MinTeilungswert

            'Pruefbericht WZ befüllen in A44 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(44, 1).value = pEichProzess.Lookup_Waegezelle.Pruefbericht

            'RueckkehrVorlastsignal WZ befüllen in G44 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(44, 7).value = pEichProzess.Lookup_Waegezelle.RueckkehrVorlastsignal

            'Waegezellenkennwert WZ befüllen in G39 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(39, 7).value = pEichProzess.Lookup_Waegezelle.Waegezellenkennwert

            'Typ WZ befüllen in A41 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(41, 1).value = pEichProzess.Lookup_Waegezelle.Typ

            'WiderstandWaegezelle WZ befüllen in G45 auf Daten-Eingabe
            objExcelWorksheetDatenEingabe.Cells(45, 7).value = pEichProzess.Lookup_Waegezelle.WiderstandWaegezelle

            '_________________________________________________________________________________________________________________________________
            'WZ ENDE
            '_________________________________________________________________________________________________________________________________

            'excel dokument speichern
            objExcelWorkbook.Save()
            objExcelWorkbook.Close()
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorksheetDatenEingabe) : objExcelWorksheetDatenEingabe = Nothing
            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorkbook) : objExcelWorkbook = Nothing
            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelApp) : objExcelApp = Nothing
            Catch ex As Exception

            End Try

            'excel dokument Anzeigen
            Process.Start(CompletePath)
        End If
    End Sub

    ''' <summary>
    ''' Konformitätserklärung DEUTSCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportKonformitaetssnachweisDE(ByVal objEichProzess As Eichprozess)

        Dim objExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim objExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objExcelWorksheetKonformerk As Microsoft.Office.Interop.Excel.Worksheet
        Dim ExcelSavePath As String
        Dim DocumentName As String = "Konformitätserklärung DE" & "_" & objEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & ".xls"
        For Each c As Char In System.IO.Path.GetInvalidFileNameChars()
            DocumentName = DocumentName.Replace(c, "_"c)
        Next
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Konformitätserklärung_DE
        Dim FolderBrowserDialog As New FolderBrowserDialog

        'Template excel dokument kopieren an Ort den der Benutzer über FolderBrowserDialog angibt
        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ExcelSavePath = FolderBrowserDialog.SelectedPath

            'Dokumentpfad erstellen
            CompletePath = ExcelSavePath & "\" & DocumentName

            'Dokument abspeichern
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            'excel dokument öffnen
            objExcelWorkbook = objExcelApp.Workbooks.Open(CompletePath)

            'Worksheets zuweisen
            objExcelWorksheetKonformerk = objExcelWorkbook.Worksheets("Konformerk.")

            'Waagentyp befüllen in E21 auf Konformerk. (Werte kommen aus Typen)
            If Not objEichProzess.Lookup_Auswertegeraet.Typ Is Nothing Then
                objExcelWorksheetKonformerk.Cells(21, 5).value = objEichProzess.Lookup_Auswertegeraet.Typ.ToString
            End If

            'Fabrikationsnummer befüllen in E29 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(29, 5).value = objEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

            'Aufstellungsort befüllen in B55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 2).value = objEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort

            'Fallbeschleunigung befüllen in E55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 5).value = objEichProzess.Eichprotokoll.Fallbeschleunigung_g

            'excel dokument speichern
            objExcelWorkbook.Save()
            objExcelWorkbook.Close()
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorksheetKonformerk) : objExcelWorksheetKonformerk = Nothing
            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorkbook) : objExcelWorkbook = Nothing
            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelApp) : objExcelApp = Nothing
            Catch ex As Exception

            End Try

            'excel dokument Anzeigen
            Process.Start(CompletePath)
        End If
    End Sub

    ''' <summary>
    ''' Konformitätserklärung POLNISCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportKonformitaetssnachweisPL(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim objExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objExcelWorksheetKonformerk As Microsoft.Office.Interop.Excel.Worksheet
        Dim ExcelSavePath As String
        Dim DocumentName As String = "Konformitätserklärung PL" & "_" & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & ".xls"
        For Each c As Char In System.IO.Path.GetInvalidFileNameChars()
            DocumentName = DocumentName.Replace(c, "_"c)
        Next
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Konformitätserklärung_PL
        Dim FolderBrowserDialog As New FolderBrowserDialog

        'Template excel dokument kopieren an Ort den der Benutzer über FolderBrowserDialog angibt
        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ExcelSavePath = FolderBrowserDialog.SelectedPath

            'Dokumentpfad erstellen
            CompletePath = ExcelSavePath & "\" & DocumentName

            'Dokument abspeichern
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            'excel dokument öffnen
            objExcelWorkbook = objExcelApp.Workbooks.Open(CompletePath)

            'Worksheets zuweisen
            objExcelWorksheetKonformerk = objExcelWorkbook.Worksheets("Konformerk.")

            'Waagentyp befüllen in E21 auf Konformerk. (Werte kommen aus Typen)
            If Not pEichProzess.Lookup_Auswertegeraet.Typ Is Nothing Then
                objExcelWorksheetKonformerk.Cells(21, 5).value = pEichProzess.Lookup_Auswertegeraet.Typ.ToString
            End If

            'Fabrikationsnummer befüllen in E29 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(29, 5).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

            'Aufstellungsort befüllen in B55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 2).value = pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort

            'Fallbeschleunigung befüllen in E55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 5).value = pEichProzess.Eichprotokoll.Fallbeschleunigung_g

            'excel dokument speichern
            objExcelWorkbook.Save()
            objExcelWorkbook.Close()
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorksheetKonformerk) : objExcelWorksheetKonformerk = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorkbook) : objExcelWorkbook = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelApp) : objExcelApp = Nothing

            Catch ex As Exception

            End Try

            'excel dokument Anzeigen
            Process.Start(CompletePath)
        End If
    End Sub

    ''' <summary>
    ''' Konformitätserklärung RUMÄNISCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportKonformitaetssnachweisRU(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objExcelApp As New Microsoft.Office.Interop.Excel.Application
        Dim objExcelWorkbook As Microsoft.Office.Interop.Excel.Workbook
        Dim objExcelWorksheetKonformerk As Microsoft.Office.Interop.Excel.Worksheet
        Dim ExcelSavePath As String
        Dim DocumentName As String = "Konformitätserklärung RO" & "_" & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & ".xls"
        For Each c As Char In System.IO.Path.GetInvalidFileNameChars()
            DocumentName = DocumentName.Replace(c, "_"c)
        Next
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Konformitätserklärung_RO
        Dim FolderBrowserDialog As New FolderBrowserDialog

        'Template excel dokument kopieren an Ort den der Benutzer über FolderBrowserDialog angibt
        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ExcelSavePath = FolderBrowserDialog.SelectedPath

            'Dokumentpfad erstellen
            CompletePath = ExcelSavePath & "\" & DocumentName

            'Dokument abspeichern
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            'excel dokument öffnen
            objExcelWorkbook = objExcelApp.Workbooks.Open(CompletePath)

            'Worksheets zuweisen
            objExcelWorksheetKonformerk = objExcelWorkbook.Worksheets("Konformerk.")

            'Waagentyp befüllen in E21 auf Konformerk. (Werte kommen aus Typen)
            If Not pEichProzess.Lookup_Auswertegeraet.Typ Is Nothing Then
                objExcelWorksheetKonformerk.Cells(21, 5).value = pEichProzess.Lookup_Auswertegeraet.Typ.ToString
            End If

            'Fabrikationsnummer befüllen in E29 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(29, 5).value = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer

            'Aufstellungsort befüllen in B55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 2).value = pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort

            'Fallbeschleunigung befüllen in E55 auf Konformerk.
            objExcelWorksheetKonformerk.Cells(55, 5).value = pEichProzess.Eichprotokoll.Fallbeschleunigung_g

            'excel dokument speichern
            objExcelWorkbook.Save()
            objExcelWorkbook.Close()
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorksheetKonformerk) : objExcelWorksheetKonformerk = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelWorkbook) : objExcelWorkbook = Nothing

            Catch ex As Exception

            End Try
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcelApp) : objExcelApp = Nothing

            Catch ex As Exception

            End Try

            'excel dokument Anzeigen
            Process.Start(CompletePath)
        End If
    End Sub

    ''' <summary>
    ''' Ersteichung DEUTSCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportErsteichungDE(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objWordApp As New Microsoft.Office.Interop.Word.Application
        Dim objWordDoc As Microsoft.Office.Interop.Word.Document
        Dim FolderBrowserDialog As New FolderBrowserDialog
        Dim WordSavePath As String
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Ersteichung_DE

        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            WordSavePath = FolderBrowserDialog.SelectedPath
            CompletePath = WordSavePath & "Ersteichung_DE.doc"

            'Hier wird das Dokument gespeichert.
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            objWordDoc = objWordApp.Documents.Open(CompletePath)
            objWordApp.Visible = True

            With objWordDoc
                .FormFields("Nummer").Result = ""
                .FormFields("Typ").Result = pEichProzess.Lookup_Waagentyp.Typ
                .FormFields("FabrSerienNummer").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
                .FormFields("Auftraggeber").Result = .FormFields("Auftraggeber").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & ", " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
                .FormFields("Ort").Result = pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort.ToString
                .FormFields("Fallbeschleunigung").Result = pEichProzess.Eichprotokoll.Fallbeschleunigung_g.ToString
            End With
        End If
    End Sub

    ''' <summary>
    ''' Ersteichung ENGLISCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportErsteichungEN(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objWordApp As New Microsoft.Office.Interop.Word.Application
        Dim objWordDoc As Microsoft.Office.Interop.Word.Document
        Dim FolderBrowserDialog As New FolderBrowserDialog
        Dim WordSavePath As String
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Ersteichung_EN

        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            WordSavePath = FolderBrowserDialog.SelectedPath
            CompletePath = WordSavePath & "Ersteichung_EN.doc"

            'Hier wird das Dokument gespeichert.
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            objWordDoc = objWordApp.Documents.Open(CompletePath)
            objWordApp.Visible = True

            With objWordDoc
                .FormFields("Nummer").Result = ""
                .FormFields("Typ").Result = pEichProzess.Lookup_Waagentyp.Typ_EN
                .FormFields("FabrSerienNummer").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
                .FormFields("Auftraggeber").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & ", " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
                .FormFields("Ort").Result = pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort.ToString
                .FormFields("Fallbeschleunigung").Result = pEichProzess.Eichprotokoll.Fallbeschleunigung_g.ToString
            End With
        End If
    End Sub

    ''' <summary>
    ''' Ersteichung POLNISCH
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportErsteichungPL(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess

        Dim objWordApp As New Microsoft.Office.Interop.Word.Application
        Dim objWordDoc As Microsoft.Office.Interop.Word.Document
        Dim FolderBrowserDialog As New FolderBrowserDialog
        Dim WordSavePath As String
        Dim CompletePath As String
        Dim b() As Byte = My.Resources.Ersteichung_PL

        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            WordSavePath = FolderBrowserDialog.SelectedPath
            CompletePath = WordSavePath & "Ersteichung_PL.doc"

            'Hier wird das Dokument gespeichert.
            Try
                System.IO.File.WriteAllBytes(CompletePath, b)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
            objWordDoc = objWordApp.Documents.Open(CompletePath)
            objWordApp.Visible = True

            With objWordDoc
                .FormFields("Nummer").Result = ""
                .FormFields("Typ").Result = pEichProzess.Lookup_Waagentyp.Typ_PL
                .FormFields("FabrSerienNummer").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
                .FormFields("Auftraggeber").Result = pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & " " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & ", " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort
                .FormFields("Ort").Result = pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort.ToString
                .FormFields("Fallbeschleunigung").Result = pEichProzess.Eichprotokoll.Fallbeschleunigung_g.ToString
            End With
        End If
    End Sub

    ''' <summary>
    ''' Export des Eichprozesses (rudimentär)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExportEichprozess(ByVal objEichProzess As Eichprozess)
        Dim pEichProzess As Eichprozess = objEichProzess
        'word instanz

        Dim objWordApp As New Microsoft.Office.Interop.Word.Application
        Dim objWordDoc As Microsoft.Office.Interop.Word.Document
        Dim FolderBrowserDialog As New FolderBrowserDialog

        If FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            'Hier wird das Dokument gespeichert.

            objWordDoc = objWordApp.Documents.Add()
            objWordApp.Visible = True

            With objWordDoc
                Dim r = objWordDoc.Range(Nothing, Nothing)
                Dim para As Microsoft.Office.Interop.Word.Paragraph = objWordDoc.Paragraphs.Add()
                '  r.InsertAfter("Vorgangsnummer: " & pEichProzess.Vorgangsnummer + vbNewLine)

                'auswerte gerät
                para.Range.Style = "Überschrift 1"
                r.InsertAfter("Auswertegerät" + vbNewLine + vbNewLine)
                para.Range.Style = "Standard"
                r.InsertAfter("Bauartzulassung:  " & pEichProzess.Lookup_Auswertegeraet.Bauartzulassung + vbNewLine)
                r.InsertAfter("Bruchteil Eichfehlergrenze:  " & pEichProzess.Lookup_Auswertegeraet.BruchteilEichfehlergrenze & vbNewLine)
                r.InsertAfter("Genauigkeitsklasse:  " & pEichProzess.Lookup_Auswertegeraet.Genauigkeitsklasse & vbNewLine)
                r.InsertAfter("Grenzwert Lastwiderstand MAX:  " & pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMAX & vbNewLine)
                r.InsertAfter("Grenzwert Lastwiderstand MIN:  " & pEichProzess.Lookup_Auswertegeraet.GrenzwertLastwiderstandMIN & vbNewLine)
                r.InsertAfter("Grenzwert Temperaturbereich MAX:  " & pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMAX & vbNewLine)
                r.InsertAfter("GrenzwertTemperaturbereich MIN:  " & pEichProzess.Lookup_Auswertegeraet.GrenzwertTemperaturbereichMIN & vbNewLine)
                r.InsertAfter("Hersteller:  " & pEichProzess.Lookup_Auswertegeraet.Hersteller & vbNewLine)
                r.InsertAfter("Kabellaenge Querschnitt:  " & pEichProzess.Lookup_Auswertegeraet.KabellaengeQuerschnitt & vbNewLine)
                r.InsertAfter("MAX Anzahl Teilungswerte Einbereichswaage:  " & pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteEinbereichswaage & vbNewLine)
                r.InsertAfter("MAX Anzahl Teilungswerte Mehrbereichswaage:  " & pEichProzess.Lookup_Auswertegeraet.MAXAnzahlTeilungswerteMehrbereichswaage & vbNewLine)
                r.InsertAfter("Mindesteingangsspannung:  " & pEichProzess.Lookup_Auswertegeraet.Mindesteingangsspannung & vbNewLine)
                r.InsertAfter("Mindestmesssignal:  " & pEichProzess.Lookup_Auswertegeraet.Mindestmesssignal & vbNewLine)
                r.InsertAfter("Prüfbericht:  " & pEichProzess.Lookup_Auswertegeraet.Pruefbericht & vbNewLine)
                r.InsertAfter("Speisespannung:  " & pEichProzess.Lookup_Auswertegeraet.Speisespannung & vbNewLine)
                r.InsertAfter("Typ:  " & pEichProzess.Lookup_Auswertegeraet.Typ & vbNewLine)

                ''wz
                para.Range.Style = "Überschrift 1"
                r.InsertAfter("Wägezelle" + vbNewLine + vbNewLine)
                para.Range.Style = "Standard"
                r.InsertAfter("Bauartzulassung:  " & pEichProzess.Lookup_Waegezelle.Bauartzulassung & vbNewLine)
                r.InsertAfter("Bruchteil Eichfehlergrenze: " & pEichProzess.Lookup_Waegezelle.BruchteilEichfehlergrenze & vbNewLine)
                r.InsertAfter("Genauigkeitsklasse: " & pEichProzess.Lookup_Waegezelle.Genauigkeitsklasse & vbNewLine)
                r.InsertAfter("Grenzwert Temperaturbereich MAX: " & pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMAX & vbNewLine)
                r.InsertAfter("Grenzwert Temperaturbereich MIN: " & pEichProzess.Lookup_Waegezelle.GrenzwertTemperaturbereichMIN & vbNewLine)
                r.InsertAfter("Hersteller: " & pEichProzess.Lookup_Waegezelle.Hersteller & vbNewLine)
                r.InsertAfter("Höchsteteilungsfaktor: " & pEichProzess.Lookup_Waegezelle.Hoechsteteilungsfaktor & vbNewLine)
                r.InsertAfter("ID: " & pEichProzess.Lookup_Waegezelle.ID & vbNewLine)
                r.InsertAfter("Kriechteilungsfaktor: " & pEichProzess.Lookup_Waegezelle.Kriechteilungsfaktor & vbNewLine)
                r.InsertAfter("Max Anzahl Teilungswerte: " & pEichProzess.Lookup_Waegezelle.MaxAnzahlTeilungswerte & vbNewLine)
                r.InsertAfter("Mindestvorlast: " & pEichProzess.Lookup_Waegezelle.Mindestvorlast & vbNewLine)
                If Not pEichProzess.Lookup_Waegezelle.MindestvorlastProzent Is Nothing Then
                    r.InsertAfter("Mindestvorlast: " & (pEichProzess.Lookup_Waegezelle.MindestvorlastProzent / 100) * pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast)
                Else
                    r.InsertAfter("Mindestvorlast: " & pEichProzess.Lookup_Waegezelle.Mindestvorlast)
                End If
                r.InsertAfter("Min Teilungswert: " & pEichProzess.Lookup_Waegezelle.MinTeilungswert & vbNewLine)
                r.InsertAfter("Prüfbericht: " & pEichProzess.Lookup_Waegezelle.Pruefbericht & vbNewLine)
                r.InsertAfter("Revisionsnummer: " & pEichProzess.Lookup_Waegezelle.Revisionsnummer & vbNewLine)
                r.InsertAfter("Rückkehr Vorlastsignal: " & pEichProzess.Lookup_Waegezelle.RueckkehrVorlastsignal & vbNewLine)
                r.InsertAfter("Typ: " & pEichProzess.Lookup_Waegezelle.Typ & vbNewLine)
                r.InsertAfter("Wägezellenkennwert: " & pEichProzess.Lookup_Waegezelle.Waegezellenkennwert & vbNewLine)
                r.InsertAfter("Widerstand Wägezelle: " & pEichProzess.Lookup_Waegezelle.WiderstandWaegezelle & vbNewLine)

                ''kompatiblitätsnachweis
                para.Range.Style = "Überschrift 1"
                r.InsertAfter("Kompatibilitätsnachweis" + vbNewLine + vbNewLine)
                para.Range.Style = "Standard"
                r.InsertAfter("AWGAnschlussart: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_AWG_Anschlussart & vbNewLine)
                r.InsertAfter("Hersteller: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Hersteller & vbNewLine)
                r.InsertAfter("Ort: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Ort & vbNewLine)
                r.InsertAfter("Postleitzahl: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Postleitzahl & vbNewLine)
                r.InsertAfter("Strasse: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Strasse & vbNewLine)
                r.InsertAfter("Verbindungselemente Bruchteil Eichfehlergrenze: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Verbindungselemente_BruchteilEichfehlergrenze & vbNewLine)
                r.InsertAfter("Waage Additive Tarahöchstlast: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AdditiveTarahoechstlast & vbNewLine)
                r.InsertAfter("Waage Anzahl Wägezellen: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_AnzahlWaegezellen & vbNewLine)
                r.InsertAfter("Waage Bauartzulassung: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Bauartzulassung & vbNewLine)
                r.InsertAfter("Waage Ecklastzuschlag: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Ecklastzuschlag & vbNewLine)
                r.InsertAfter("Waage Eichwert1: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 & vbNewLine)
                r.InsertAfter("Waage Eichwert2: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 & vbNewLine)
                r.InsertAfter("Waage Eichwert3: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 & vbNewLine)
                r.InsertAfter("Waage Einschaltnullstellbereich: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich & vbNewLine)
                r.InsertAfter("Waage FabrikNummer: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer & vbNewLine)
                r.InsertAfter("Waage Genauigkeitsklasse: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Genauigkeitsklasse & vbNewLine)
                r.InsertAfter("Waage Grenzen Temperaturbereich MAX: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMAX & vbNewLine)
                r.InsertAfter("Waage Grenzen Temperaturbereich MIN: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_GrenzenTemperaturbereichMIN & vbNewLine)
                r.InsertAfter("Waage Höchstlast1: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1 & vbNewLine)
                r.InsertAfter("Waage Höchstlast2: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2 & vbNewLine)
                r.InsertAfter("Waage Höchstlast3: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3 & vbNewLine)
                r.InsertAfter("Waage Kabellänge: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabellaenge & vbNewLine)
                r.InsertAfter("Waage Kabelquerschnitt: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Kabelquerschnitt & vbNewLine)
                r.InsertAfter("Waage Revisionsnummer: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Revisionsnummer & vbNewLine)
                r.InsertAfter("Waage Totlast: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Totlast & vbNewLine)
                r.InsertAfter("Waage Übersetzungsverhaeltnis: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis & vbNewLine)
                r.InsertAfter("Waage Zulassungsinhaber: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Zulassungsinhaber & vbNewLine)
                r.InsertAfter("WZ Höchstlast: " & pEichProzess.Kompatiblitaetsnachweis.Kompatiblitaet_WZ_Hoechstlast.Split(";")(0) & vbNewLine)

                ''beschaffenheitsprüfung
                para.Range.Style = "Überschrift 1"
                r.InsertAfter("Beschaffenheitsprüfung" + vbNewLine + vbNewLine)
                para.Range.Style = "Standard"
                r.InsertAfter("Genehmigt: " & pEichProzess.Eichprotokoll.Beschaffenheitspruefung_Genehmigt & vbNewLine)

                ''Eichprotokoll
                para.Range.Style = "Überschrift 1"
                r.InsertAfter("Eichprotokoll" + vbNewLine + vbNewLine)
                para.Range.Style = "Standard"
                r.InsertAfter("Eignung Achlastwägungen Geprüft: " & pEichProzess.Eichprotokoll.EignungAchlastwaegungen_Geprueft & vbNewLine)
                r.InsertAfter("Eignung Achlastwägungen Waagenbruecke Ebene: " & pEichProzess.Eichprotokoll.EignungAchlastwaegungen_WaagenbrueckeEbene & vbNewLine)
                r.InsertAfter("Eignung Achlastwägungen Waage Nicht Geeignet: " & pEichProzess.Eichprotokoll.EignungAchlastwaegungen_WaageNichtGeeignet & vbNewLine)
                r.InsertAfter("Fallbeschleunigung g: " & pEichProzess.Eichprotokoll.Fallbeschleunigung_g & vbNewLine)
                r.InsertAfter("Fallbeschleunigung ms2: " & pEichProzess.Eichprotokoll.Fallbeschleunigung_ms2 & vbNewLine)
                r.InsertAfter("Konformitätsbewertungsverfahren: " & pEichProzess.Eichprotokoll.FK_Identifikationsdaten_Konformitaetsbewertungsverfahren & vbNewLine)
                r.InsertAfter("Eichprotokoll Name: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Benutzer & vbNewLine)
                r.InsertAfter("Genauigkeit Nullstellung In Ordnung: " & pEichProzess.Eichprotokoll.GenauigkeitNullstellung_InOrdnung & vbNewLine)
                r.InsertAfter("ID: " & pEichProzess.Eichprotokoll.ID & vbNewLine)
                r.InsertAfter("Aufstellungsort: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Aufstellungsort & vbNewLine)
                r.InsertAfter("Baujahr: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Baujahr & vbNewLine)
                r.InsertAfter("Datum: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Datum & vbNewLine)
                r.InsertAfter("HybridMechanisch: " & pEichProzess.Eichprotokoll.Identifikationsdaten_HybridMechanisch & vbNewLine)
                r.InsertAfter("Min1: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Min1 & vbNewLine)
                r.InsertAfter("Min2: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Min2 & vbNewLine)
                r.InsertAfter("Min3: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Min3 & vbNewLine)
                r.InsertAfter("Nicht Selbsteinspielend: " & pEichProzess.Eichprotokoll.Identifikationsdaten_NichtSelbsteinspielend & vbNewLine)
                r.InsertAfter("Prüfer: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Pruefer & vbNewLine)
                r.InsertAfter("Selbsteinspielend: " & pEichProzess.Eichprotokoll.Identifikationsdaten_Selbsteinspielend & vbNewLine)
                r.InsertAfter("Eichzählerstand: " & pEichProzess.Eichprotokoll.Komponenten_Eichzaehlerstand & vbNewLine)
                r.InsertAfter("Softwarestand: " & pEichProzess.Eichprotokoll.Komponenten_Softwarestand & vbNewLine)
                r.InsertAfter("Waegezellen Fabriknummer: " & pEichProzess.Eichprotokoll.Komponenten_WaegezellenFabriknummer & vbNewLine)
                r.InsertAfter("Betrag Normallast: " & pEichProzess.Eichprotokoll.Pruefverfahren_BetragNormallast & vbNewLine)
                r.InsertAfter("Volle Normallast: " & pEichProzess.Eichprotokoll.Pruefverfahren_VolleNormallast & vbNewLine)
                r.InsertAfter("Vollständiges Staffelverfahren: " & pEichProzess.Eichprotokoll.Pruefverfahren_VollstaendigesStaffelverfahren & vbNewLine)
                r.InsertAfter("Alibispeicher Aufbewahrungsdauer Reduziert: " & pEichProzess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziert & vbNewLine)
                r.InsertAfter("Alibispeicher Aufbewahrungsdauer Reduziert Begruendung: " & pEichProzess.Eichprotokoll.Sicherung_AlibispeicherAufbewahrungsdauerReduziertBegruendung & vbNewLine)
                r.InsertAfter("Alibispeicher Eingerichtet: " & pEichProzess.Eichprotokoll.Sicherung_AlibispeicherEingerichtet & vbNewLine)
                r.InsertAfter("Bemerkungen: " & pEichProzess.Eichprotokoll.Sicherung_Bemerkungen & vbNewLine)
                r.InsertAfter("Benannte Stelle: " & pEichProzess.Eichprotokoll.Sicherung_BenannteStelle & vbNewLine)
                r.InsertAfter("Benannte Stelle Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_BenannteStelleAnzahl & vbNewLine)
                r.InsertAfter("CE: " & pEichProzess.Eichprotokoll.Sicherung_CE & vbNewLine)
                r.InsertAfter("CE Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_CEAnzahl & vbNewLine)
                r.InsertAfter("CE 2016: " & pEichProzess.Eichprotokoll.Sicherung_CE2016 & vbNewLine)
                r.InsertAfter("CE 2016 Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_CE2016Anzahl & vbNewLine)
                r.InsertAfter("Daten Ausgelesen: " & pEichProzess.Eichprotokoll.Sicherung_DatenAusgelesen & vbNewLine)
                r.InsertAfter("Eichsiegel 13x13: " & pEichProzess.Eichprotokoll.Sicherung_Eichsiegel13x13 & vbNewLine)
                r.InsertAfter("Eichsiegel 13x13 Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_Eichsiegel13x13Anzahl & vbNewLine)
                r.InsertAfter("Eichsiegel Rund: " & pEichProzess.Eichprotokoll.Sicherung_EichsiegelRund & vbNewLine)
                r.InsertAfter("Eichsiegel Rund Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_EichsiegelRundAnzahl & vbNewLine)
                r.InsertAfter("Grünes M: " & pEichProzess.Eichprotokoll.Sicherung_GruenesM & vbNewLine)
                r.InsertAfter("Grünes M Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_GruenesMAnzahl & vbNewLine)
                r.InsertAfter("Hinweismarke Gelocht: " & pEichProzess.Eichprotokoll.Sicherung_HinweismarkeGelocht & vbNewLine)
                r.InsertAfter("Hinweismarke Gelocht Anzahl: " & pEichProzess.Eichprotokoll.Sicherung_HinweismarkeGelochtAnzahl & vbNewLine)
                r.InsertAfter("Erweiterte Richtigkeitsprüfung OK: " & pEichProzess.Eichprotokoll.Taraeinrichtung_ErweiterteRichtigkeitspruefungOK & vbNewLine)
                r.InsertAfter("Genauigkeit Tarierung OK: " & pEichProzess.Eichprotokoll.Taraeinrichtung_GenauigkeitTarierungOK & vbNewLine)
                r.InsertAfter("Taraausgleichseinrichtung OK: " & pEichProzess.Eichprotokoll.Taraeinrichtung_TaraausgleichseinrichtungOK & vbNewLine)
                r.InsertAfter("Überlastanzeige Max: " & pEichProzess.Eichprotokoll.Ueberlastanzeige_Max & vbNewLine)
                r.InsertAfter("Überlastanzeige Überlast: " & pEichProzess.Eichprotokoll.Ueberlastanzeige_Ueberlast & vbNewLine)
                r.InsertAfter("Automatisch: " & pEichProzess.Eichprotokoll.Verwendungszweck_Automatisch & vbNewLine)
                r.InsertAfter("Auto Tara: " & pEichProzess.Eichprotokoll.Verwendungszweck_AutoTara & vbNewLine)
                r.InsertAfter("Drucker: " & pEichProzess.Eichprotokoll.Verwendungszweck_Drucker & vbNewLine)
                r.InsertAfter("Druckertyp: " & pEichProzess.Eichprotokoll.Verwendungszweck_Druckertyp & vbNewLine)
                r.InsertAfter("Eichfaehiger Datenspeicher: " & pEichProzess.Eichprotokoll.Verwendungszweck_EichfaehigerDatenspeicher & vbNewLine)
                r.InsertAfter("Fahrzeugwaagen Dimension: " & pEichProzess.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_Dimension & vbNewLine)
                r.InsertAfter("Fahrzeugwaagen MxM: " & pEichProzess.Eichprotokoll.Verwendungszweck_Fahrzeugwaagen_MxM & vbNewLine)
                r.InsertAfter("Halb Automatisch: " & pEichProzess.Eichprotokoll.Verwendungszweck_HalbAutomatisch & vbNewLine)
                r.InsertAfter("Hand Tara: " & pEichProzess.Eichprotokoll.Verwendungszweck_HandTara & vbNewLine)
                r.InsertAfter("Nullnachführung: " & pEichProzess.Eichprotokoll.Verwendungszweck_Nullnachfuehrung & vbNewLine)
                r.InsertAfter("PC: " & pEichProzess.Eichprotokoll.Verwendungszweck_PC & vbNewLine)
                r.InsertAfter("Zubehör Verschiedenes: " & pEichProzess.Eichprotokoll.Verwendungszweck_ZubehoerVerschiedenes & vbNewLine)
                r.InsertAfter("Wiederholbarkeit Staffelverfahren MIN Normalien: " & pEichProzess.Eichprotokoll.Wiederholbarkeit_Staffelverfahren_MINNormalien & vbNewLine)

                'prüfungen()
                Using dbcontext As New Entities
                    Try
                        'hole mir aus der DB alle Pruefungen deren FK Eichprotokoll unserem aktuellem Eichprtookoll entsprechen
                        Dim query = From db In dbcontext.PruefungAnsprechvermoegen Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Ansprechvermögen" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)
                            r.InsertAfter("Last1d: " & sourceo.Last1d & vbNewLine)
                            r.InsertAfter("LastL: " & sourceo.LastL & vbNewLine)
                            r.InsertAfter("Ziffernsprung: " & sourceo.Ziffernsprung & vbNewLine)
                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungAussermittigeBelastung Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Aussermittige Belastung" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("Belastungsort: " & sourceo.Belastungsort & vbNewLine)
                            r.InsertAfter("Bereich: " & sourceo.Bereich & vbNewLine)
                            r.InsertAfter("EFG: " & sourceo.EFG & vbNewLine)
                            r.InsertAfter("EFGExtra: " & sourceo.EFGExtra & vbNewLine)
                            r.InsertAfter("Fehler: " & sourceo.Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungLinearitaetFallend Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Linearität Fallend" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("Messpunkt: " & sourceo.Messpunkt & vbNewLine)
                            r.InsertAfter("Bereich: " & sourceo.Bereich & vbNewLine)
                            r.InsertAfter("EFG: " & sourceo.EFG & vbNewLine)
                            r.InsertAfter("Fehler: " & sourceo.Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungLinearitaetSteigend Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Linearität Steigend" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("Messpunkt: " & sourceo.Messpunkt & vbNewLine)
                            r.InsertAfter("Bereich: " & sourceo.Bereich & vbNewLine)
                            r.InsertAfter("EFG: " & sourceo.EFG & vbNewLine)
                            r.InsertAfter("Fehler: " & sourceo.Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungRollendeLasten Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung rollende Lasten" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("AuffahrtSeite: " & sourceo.AuffahrtSeite & vbNewLine)
                            r.InsertAfter("Belastungsstelle: " & sourceo.Belastungsstelle & vbNewLine)
                            r.InsertAfter("EFG: " & sourceo.EFG & vbNewLine)
                            r.InsertAfter("EFGExtra: " & sourceo.EFGExtra & vbNewLine)
                            r.InsertAfter("Fehler: " & sourceo.Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungStabilitaetGleichgewichtslage Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Stabilität Gleichgewichtslage" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("AbdruckOK: " & sourceo.AbdruckOK & vbNewLine)
                            r.InsertAfter("Durchlauf: " & sourceo.Durchlauf & vbNewLine)
                            r.InsertAfter("MAX: " & sourceo.MAX & vbNewLine)
                            r.InsertAfter("MIN: " & sourceo.MIN & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungStaffelverfahrenErsatzlast Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Staffelverfahren Ersatzlast" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Bereich: " & sourceo.Bereich & vbNewLine)
                            r.InsertAfter("Differenz Anzeigewerte EFG: " & sourceo.DifferenzAnzeigewerte_EFG & vbNewLine)
                            r.InsertAfter("Differenz Anzeigewerte Fehler: " & sourceo.DifferenzAnzeigewerte_Fehler & vbNewLine)
                            r.InsertAfter("Ersatzlast Ist: " & sourceo.Ersatzlast_Ist & vbNewLine)
                            r.InsertAfter("Ersatzlast Soll: " & sourceo.Ersatzlast_Soll & vbNewLine)
                            r.InsertAfter("Ersatzlast2 Ist: " & sourceo.Ersatzlast2_Ist & vbNewLine)
                            r.InsertAfter("Ersatzlast2 Soll: " & sourceo.Ersatzlast2_Soll & vbNewLine)
                            r.InsertAfter("ErsatzUndNormallast Ist: " & sourceo.ErsatzUndNormallast_Ist & vbNewLine)
                            r.InsertAfter("ErsatzUndNormallast Soll: " & sourceo.ErsatzUndNormallast_Soll & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Messabweichung Staffel EFG: " & sourceo.MessabweichungStaffel_EFG & vbNewLine)
                            r.InsertAfter("Messabweichung Staffel Fehler: " & sourceo.MessabweichungStaffel_Fehler & vbNewLine)
                            r.InsertAfter("Messabweichung Waage EFG: " & sourceo.MessabweichungWaage_EFG & vbNewLine)
                            r.InsertAfter("Messabweichung Waage Fehler: " & sourceo.MessabweichungWaage_Fehler & vbNewLine)
                            r.InsertAfter("Staffel: " & sourceo.Staffel & vbNewLine)
                            r.InsertAfter("Zusätzliche Ersatzlast Soll: " & sourceo.ZusaetzlicheErsatzlast_Soll & vbNewLine)

                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungStaffelverfahrenNormallast Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Staffelverfahren Normallast" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Bereich: " & sourceo.Bereich & vbNewLine)
                            r.InsertAfter("Differenz Anzeigewerte EFG: " & sourceo.DifferenzAnzeigewerte_EFG & vbNewLine)
                            r.InsertAfter("Differenz Anzeigewerte Fehler: " & sourceo.DifferenzAnzeigewerte_Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Messabweichung Staffel EFG: " & sourceo.MessabweichungStaffel_EFG & vbNewLine)
                            r.InsertAfter("Messabweichung Staffel Fehler: " & sourceo.MessabweichungStaffel_Fehler & vbNewLine)
                            r.InsertAfter("Messabweichung Waage EFG: " & sourceo.MessabweichungWaage_EFG & vbNewLine)
                            r.InsertAfter("Messabweichung Waage Fehler: " & sourceo.MessabweichungWaage_Fehler & vbNewLine)
                            r.InsertAfter("Normal Last Anzeige 1: " & sourceo.NormalLast_Anzeige_1 & vbNewLine)
                            r.InsertAfter("Normal Last Anzeige 2: " & sourceo.NormalLast_Anzeige_2 & vbNewLine)
                            r.InsertAfter("Normal Last Anzeige 3: " & sourceo.NormalLast_Anzeige_3 & vbNewLine)
                            r.InsertAfter("Normal Last Anzeige 4: " & sourceo.NormalLast_Anzeige_4 & vbNewLine)
                            r.InsertAfter("Normal Last EFG 1: " & sourceo.NormalLast_EFG_1 & vbNewLine)
                            r.InsertAfter("Normal Last EFG 2: " & sourceo.NormalLast_EFG_2 & vbNewLine)
                            r.InsertAfter("Normal Last EFG 3: " & sourceo.NormalLast_EFG_3 & vbNewLine)
                            r.InsertAfter("Normal Last EFG 4: " & sourceo.NormalLast_EFG_4 & vbNewLine)
                            r.InsertAfter("Normal Last Fehler 1: " & sourceo.NormalLast_Fehler_1 & vbNewLine)
                            r.InsertAfter("Normal Last Fehler 2: " & sourceo.NormalLast_Fehler_2 & vbNewLine)
                            r.InsertAfter("Normal Last Fehler 3: " & sourceo.NormalLast_Fehler_3 & vbNewLine)
                            r.InsertAfter("Normal Last Fehler 4: " & sourceo.NormalLast_Fehler_4 & vbNewLine)
                            r.InsertAfter("Normal Last Last 1: " & sourceo.NormalLast_Last_1 & vbNewLine)
                            r.InsertAfter("Normal Last Last 2: " & sourceo.NormalLast_Last_2 & vbNewLine)
                            r.InsertAfter("Normal Last Last 3: " & sourceo.NormalLast_Last_3 & vbNewLine)
                            r.InsertAfter("Normal Last Last 4: " & sourceo.NormalLast_Last_4 & vbNewLine)
                            r.InsertAfter("Staffel: " & sourceo.Staffel & vbNewLine)
                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        Dim query = From db In dbcontext.PruefungWiederholbarkeit Where db.FK_Eichprotokoll = objEichProzess.Eichprotokoll.ID Select db
                        para.Range.Style = "Überschrift 1"
                        r.InsertAfter("Prüfung Wiederholbarkeit" + vbNewLine + vbNewLine)
                        para.Range.Style = "Standard"
                        For Each sourceo In query
                            r.InsertAfter("Anzeige: " & sourceo.Anzeige & vbNewLine)
                            r.InsertAfter("Belastung: " & sourceo.Belastung & vbNewLine)
                            r.InsertAfter("Wiederholung: " & sourceo.Wiederholung & vbNewLine)
                            r.InsertAfter("EFG: " & sourceo.EFG & vbNewLine)
                            r.InsertAfter("EFG Extra: " & sourceo.EFG_Extra & vbNewLine)
                            r.InsertAfter("Fehler: " & sourceo.Fehler & vbNewLine)
                            r.InsertAfter("Eichprotokoll: " & sourceo.FK_Eichprotokoll & vbNewLine)
                            r.InsertAfter("ID: " & sourceo.ID & vbNewLine)
                            r.InsertAfter("Last: " & sourceo.Last & vbNewLine)
                        Next
                    Catch ex As Exception
                    End Try
                End Using
            End With
        End If
    End Sub

End Class