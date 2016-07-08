﻿Imports System.Data.OleDb
Imports System.Xml
Imports System.Xml.Linq

Public Class clsPlausibilitaetspruefung
    Private Const CONSTACCESSPASSWORD = "RHEWA_Konfig82"
    Private _filename As String
    Private _objEichprozess As Eichprozess

    Dim _Waegebereich As Integer

#Region "Vergleichswerte Konfiguration"
    Private Fabriknummer As String
    Private FirmwareVersion As String
    Private Model As String
    Private Eichzaehlerstand As String
    Private Erdbeschleunigung As String
    Private Mehrbereich As String

    Private Gewichtseinheit As String  ' 0 = tonnen ; 1 = kg 2; g

    Private Waegebereich1 As String
    Private Ziffernschritt1 As String
    Private Waegebereich2 As String
    Private Ziffernschritt2 As String
    Private Waegebereich3 As String
    Private Ziffernschritt3 As String
    Private Einschalltnullstellen As String

    Private AnzahlJustagepunkte As String
    Private AnalogwertJustagepunktMin As String
    Private AnalogwertJustagepunktMax As String
    Private GewichtswertJustagepunktMin As String
    Private GewichtswertJustagepunktMax As String
    Private Uebersetzungsverhaeltnis As String

#End Region

#Region "Properties"
#Region "Aus Config"
    Public ReadOnly Property FabriknummerConfig As String
        Get
            Return Fabriknummer
        End Get
    End Property

    Public ReadOnly Property FirmwareVersionConfig As String
        Get
            Return FirmwareVersion
        End Get
    End Property

    Public ReadOnly Property ModelConfig As String
        Get
            Return Model
        End Get
    End Property

    Public ReadOnly Property EichzaehlerstandConfig As String
        Get
            Return Eichzaehlerstand
        End Get
    End Property

    Public ReadOnly Property ErdbeschleunigungConfig As String
        Get
            Return Erdbeschleunigung
        End Get
    End Property

    Public ReadOnly Property MehrbereichConfig As String
        Get
            Return Mehrbereich
        End Get

    End Property

    Public ReadOnly Property GewichtseinheitConfig As String
        Get
            Return Gewichtseinheit
        End Get

    End Property

    Public ReadOnly Property Waegebereich1Config As String
        Get
            Return Waegebereich1
        End Get

    End Property

    Public ReadOnly Property Ziffernschritt1Config As String
        Get
            Return Ziffernschritt1
        End Get

    End Property

    Public ReadOnly Property Waegebereich2Config As String
        Get
            Return Waegebereich2
        End Get

    End Property

    Public ReadOnly Property Ziffernschritt2Config As String
        Get
            Return Ziffernschritt2
        End Get

    End Property

    Public ReadOnly Property Waegebereich3Config As String
        Get
            Return Waegebereich3
        End Get

    End Property

    Public ReadOnly Property Ziffernschritt3Config As String
        Get
            Return Ziffernschritt3
        End Get

    End Property

    Public ReadOnly Property EinschalltnullstellenConfig As String
        Get
            Return Einschalltnullstellen
        End Get

    End Property

    Public ReadOnly Property AnzahlJustagepunkteConfig As String
        Get
            Return AnzahlJustagepunkte
        End Get

    End Property

    Public ReadOnly Property AnalogwertJustagepunktMinConfig As String
        Get
            Return AnalogwertJustagepunktMin
        End Get

    End Property

    Public ReadOnly Property AnalogwertJustagepunktMaxConfig As String
        Get
            Return AnalogwertJustagepunktMax
        End Get

    End Property

    Public ReadOnly Property GewichtswertJustagepunktMinConfig As String
        Get
            Return GewichtswertJustagepunktMin
        End Get

    End Property

    Public ReadOnly Property GewichtswertJustagepunktMaxConfig As String
        Get
            Return GewichtswertJustagepunktMax
        End Get

    End Property

    Public ReadOnly Property UebersetzungsverhaeltnisConfig As String
        Get
            Return Uebersetzungsverhaeltnis
        End Get

    End Property
#End Region
#Region "AusSoftware"
    Public ReadOnly Property FabriknummerSoftware As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        End Get
    End Property

    Public ReadOnly Property FirmwareVersionSoftware As String
        Get
            Return _objEichprozess.Eichprotokoll.Komponenten_Softwarestand
        End Get
    End Property

    Public ReadOnly Property ModelSoftware As String
        Get
            Return _objEichprozess.Lookup_Auswertegeraet.Typ
        End Get
    End Property

    Public ReadOnly Property EichzaehlerstandSoftware As String
        Get
            Return Eichzaehlerstand
        End Get
    End Property

    Public ReadOnly Property ErdbeschleunigungSoftware As String
        Get
            Return _objEichprozess.Eichprotokoll.Fallbeschleunigung_g
        End Get
    End Property

    Public ReadOnly Property MehrbereichSoftware As String
        Get
            Return Mehrbereich
        End Get
    End Property

    Public ReadOnly Property GewichtseinheitSoftware As String
        Get
            Return "1"
        End Get
    End Property

    Public ReadOnly Property Waegebereich1Software As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1
        End Get
    End Property

    Public ReadOnly Property Ziffernschritt1Software As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1
        End Get
    End Property

    Public ReadOnly Property Waegebereich2Software As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2
        End Get
    End Property

    Public ReadOnly Property Ziffernschritt2Software As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2
        End Get
    End Property

    Public ReadOnly Property Waegebereich3Software As String
        Get '?
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3
        End Get
    End Property

    Public ReadOnly Property Ziffernschritt3Software As String
        Get '?
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3
        End Get
    End Property

    Public ReadOnly Property EinschalltnullstellenSoftware As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        End Get
    End Property

    Public ReadOnly Property UebersetzungsverhaeltnisSoftware As String
        Get
            Return Uebersetzungsverhaeltnis
        End Get
    End Property

#End Region
#End Region

    Public Function LadeWerte(waegebereich As Integer, filename As String, objEichprozess As Eichprozess) As Boolean
        _objEichprozess = objEichprozess
        _Waegebereich = waegebereich
        Eichzaehlerstand = _objEichprozess.Eichprotokoll.Komponenten_Eichzaehlerstand
        Uebersetzungsverhaeltnis = _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis

        _filename = filename
        If filename.ToLower.EndsWith(".xml") Then
            HoleWerteXML()
        ElseIf filename.ToLower.EndsWith(".mdb") Then
            HoleWerteACCDB()
        Else
            Return False
        End If

        'TODO Berechnung

        Return True
    End Function

    Private Function HoleWerteACCDB() As Boolean
        Dim conn As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", _filename, CONSTACCESSPASSWORD)
        Dim cmd As String = ""
        Dim adapter As OleDbDataAdapter

        Dim dtJustage As New DataTable
        Dim dtInformation As New DataTable
        Dim dtAllgemein As New DataTable
        Dim dtWBEinstellungen As New DataTable

        Try
            cmd = "Select Fabrik_Nr as Fabriknummer, konFirmware as Version, konGeraeteTyp as Typ   from tblKon_Information"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtInformation)
        Catch ex As Exception
            cmd = "Select konFabrikationsnummer as Fabriknummer, konProgrammversion as Version, konAuswertegeraetetyp as Typ from tblKonfiguration_Information"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtInformation)
        End Try

        Try
            cmd = "Select konGwert as gWert from tblKon_Allgemein"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtAllgemein)
        Catch ex As Exception
            cmd = "Select konErdbeschleunigung_gWert as gWert from tblKonfiguration_JustageAllgemein"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtAllgemein)
        End Try

        Try
            cmd = "Select konWaegebereich as Mehrbereich, konGewichtseinheit as Gewichtseinheit, konMaxBereich1 as Max1, konTeilungBereich1 as Teilung1, konMaxBereich2 as Max2, konTeilungBereich2 as Teilung2, konMaxBereich3 as Max3, konTeilungBereich3 as Teilung3, konEinschaltnustelle as Einschaltnullstelle from tblKon_WBEinstellungen"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtWBEinstellungen)
        Catch ex As Exception
            cmd = "Select konMehrbereich as Mehrbereich, konGewichtseinheit as Gewichtseinheit, konMax1 as Max1, konTeilung1 as Teilung1, konMax2 as Max2, konTeilung2 as Teilung2, konMax3 as Max3, konTeilung3 as Teilung3, konEinschaltnullstellen as Einschaltnullstelle from tblKonfiguration_WBEinstellungen"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtWBEinstellungen)
        End Try

        Try
            cmd = "Select konADWert1, konADWert2,konADWert3,konADWert4,konADWert5, konGewichtswert1, konGewichtswert2, konGewichtswert3, konGewichtswert4, konGewichtswert5 from tblKon_WBJustage"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtJustage)
        Catch ex As Exception
            cmd = "Select konADWert1, konADWert2,konADWert3,konADWert4,konADWert5, konGewichtswert1, konGewichtswert2, konGewichtswert3, konGewichtswert4, konGewichtswert5 from tblKonfiguration_WBJustage"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtJustage)
        End Try

        For Each row As DataRow In dtInformation.Rows
            Fabriknummer = row("Fabriknummer")
            FirmwareVersion = row("Version")
            Model = row("Typ")
            Exit For
        Next

        For Each row As DataRow In dtAllgemein.Rows
            Erdbeschleunigung = row("gWert")
            Exit For
        Next

        For Each row As DataRow In dtWBEinstellungen.Rows
            Mehrbereich = row("Mehrbereich")
            Gewichtseinheit = row("Gewichtseinheit")
            Waegebereich1 = row("Max1")
            Waegebereich2 = row("Max2")
            Waegebereich3 = row("Max3")
            Ziffernschritt1 = row("Teilung1")
            Ziffernschritt2 = row("Teilung2")
            Ziffernschritt3 = row("Teilung3")
            Einschalltnullstellen = row("Einschaltnullstelle")
            Exit For
        Next

        For Each row As DataRow In dtJustage.Rows
            Dim minGewicht As Decimal = 10
            Dim maxGewicht As Decimal = -10

            Dim minAnalog As Decimal = 10
            Dim maxAnalog As Decimal = -10

            Dim countjustagepunkte As Integer

            For Each column As DataColumn In dtJustage.Columns
                Try
                    If column.ColumnName.StartsWith("konADWert") Then
                        If IsNumeric(row(column)) AndAlso CDec(row(column)) < minAnalog AndAlso CDec(row(column)) <> 0 Then
                            minAnalog = CDec(row(column))
                        End If
                        If IsNumeric(row(column)) AndAlso CDec(row(column)) > maxAnalog Then
                            maxAnalog = CDec(row(column))
                        End If

                        If IsDBNull(row(column)) = False AndAlso row(column) <> "" AndAlso row(column) <> 0 Then
                            countjustagepunkte += 1
                        End If
                    ElseIf column.ColumnName.StartsWith("konGewichtswert") Then
                        If IsNumeric(row(column)) AndAlso CDec(row(column)) < minGewicht Then
                            minGewicht = CDec(row(column))
                        End If
                        If IsNumeric(row(column)) AndAlso CDec(row(column)) > maxGewicht AndAlso CDec(row(column)) <> 10000000 Then
                            maxGewicht = CDec(row(column))
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next

            AnzahlJustagepunkte = countjustagepunkte
            AnalogwertJustagepunktMin = minAnalog
            AnalogwertJustagepunktMax = maxAnalog
            GewichtswertJustagepunktMin = 0
            GewichtswertJustagepunktMax = maxGewicht
            Exit For
        Next

    End Function

    Private Function HoleWerteXML() As Boolean
        Dim xd As XmlDocument = New XmlDocument()
        xd.Load(_filename)

        Dim nl As XmlNodeList = xd.GetElementsByTagName("Information")
        For Each node As XmlNode In nl
            Dim Werte = node.InnerText.Split(";")
            Fabriknummer = Werte(1)
            FirmwareVersion = Werte(4)
            Model = Werte(0)
        Next node

        nl = xd.GetElementsByTagName("General")
        For Each node As XmlNode In nl
            Dim Werte = node.InnerText.Split(";")
            Erdbeschleunigung = Werte(0)
        Next node

        nl = xd.GetElementsByTagName("WP_Settings")
        For Each node As XmlNode In nl
            'besonderheit Erster Wert enthält länge des gesamten Blocks + ersten Wert
            Dim Originaltext = node.InnerText

            Dim GesuchterBereich As String = ""

            For i As Integer = 1 To 9
                Try

                    Dim LaengeBereich = CInt(Originaltext.Substring(0, 4))
                    Dim TextBereich As String = Originaltext.Substring(0, LaengeBereich + 4)
                    Dim WerteBereich As String = TextBereich.Substring(4, LaengeBereich)

                    Originaltext = Originaltext.Replace(TextBereich, "")
                    If i = _Waegebereich Then
                        GesuchterBereich = WerteBereich
                        Exit For
                    End If

                Catch ex As Exception

                End Try
            Next

            Dim Werte = GesuchterBereich.Split(";")
            Mehrbereich = _objEichprozess.Lookup_Waagenart.Art
            Gewichtseinheit = Werte(0) ' 0 = tonnen ; = kg 2; g
            Waegebereich1 = Werte(3)
            Ziffernschritt1 = Werte(4)
            Waegebereich2 = Werte(5)
            Ziffernschritt2 = Werte(6)
            Waegebereich3 = Werte(7)
            Ziffernschritt3 = Werte(8)
            Einschalltnullstellen = Werte(20)

        Next

        nl = xd.GetElementsByTagName("WP_Adjustment")
        For Each node As XmlNode In nl
            'besonderheit Erster Wert enthält länge des gesamten Blocks + ersten Wert
            Dim Originaltext = node.InnerText

            Dim GesuchterBereich As String = ""

            For i As Integer = 1 To 9
                Try
                    Dim LaengeBereich = CInt(Originaltext.Substring(0, 4))
                    Dim TextBereich As String = Originaltext.Substring(0, LaengeBereich + 4)
                    Dim WerteBereich As String = TextBereich.Substring(4, LaengeBereich)

                    Originaltext = Originaltext.Replace(TextBereich, "")
                    If i = _Waegebereich Then
                        GesuchterBereich = WerteBereich
                        Exit For
                    End If

                Catch ex As Exception

                End Try
            Next

            Dim Werte = GesuchterBereich.Split(";")

            Dim minGewicht As Decimal = 10
            Dim maxGewicht As Decimal = -10

            Dim minAnalog As Decimal = 10
            Dim maxAnalog As Decimal = -10

            Dim countjustagepunkte As Integer

            For i As Integer = 0 To 10
                If i = 0 OrElse i Mod 2 = 0 Then 'die geraden Werte entsprechen den Gewichten
                    Dim Gewicht = Werte(i)
                    Select Case Gewichtseinheit
                        Case = 0
                            Gewicht = CDec(Gewicht) * 1000
                        Case = 1
                        Case = 2
                            Try
                                Gewicht = CDec(Gewicht) / 1000
                            Catch ex As Exception
                            End Try

                    End Select
                    If Gewicht < minGewicht Then
                        minGewicht = Gewicht
                    End If
                    If Gewicht > maxGewicht AndAlso Werte(i) <> 10000000 Then 'hier wird der unmodifizierte Gewichtswert verglichen da 1000000 als default wert eingetragen wird
                        maxGewicht = Gewicht
                    End If
                Else ' die ungeraden den Analogwerten
                    Dim Analogwert = Werte(i)

                    If Analogwert < minAnalog AndAlso Analogwert <> 0 Then
                        minAnalog = Analogwert
                    End If
                    If Analogwert > maxAnalog Then
                        maxAnalog = Analogwert
                    End If

                    If Analogwert <> 0 Then
                        countjustagepunkte += 1
                    End If

                End If
            Next

            AnzahlJustagepunkte = countjustagepunkte
            AnalogwertJustagepunktMin = minAnalog
            AnalogwertJustagepunktMax = maxAnalog
            GewichtswertJustagepunktMin = 0
            GewichtswertJustagepunktMax = maxGewicht

        Next

        'Dim nl As XmlNodeList = xd.GetElementsByTagName("WP_Adjustment")
        'For Each node As XmlNode In nl
        '    Console.WriteLine(node.InnerText)
        'Next node
    End Function

    Public Function getAsDatasource() As List(Of PlausibilitaetDatasource)
        Dim Werte As New List(Of PlausibilitaetDatasource)

        Werte.Add(New PlausibilitaetDatasource("Fabriknummer", Me.FabriknummerConfig, Me.FabriknummerSoftware))
        Werte.Add(New PlausibilitaetDatasource("Firmware-Version", Me.FirmwareVersionConfig, Me.FirmwareVersionSoftware))
        Werte.Add(New PlausibilitaetDatasource("Model", Me.ModelConfig, Me.ModelSoftware))
        Werte.Add(New PlausibilitaetDatasource("Eichzählerstand", Me.EichzaehlerstandConfig, Me.EichzaehlerstandSoftware))
        Werte.Add(New PlausibilitaetDatasource("Erdbeschleunigung", Me.ErdbeschleunigungConfig, Me.ErdbeschleunigungSoftware))
        Werte.Add(New PlausibilitaetDatasource("Mehrbereich", Me.MehrbereichConfig, Me.MehrbereichSoftware))
        Werte.Add(New PlausibilitaetDatasource("Gewichtseinheit", Me.GewichtseinheitConfig, Me.GewichtseinheitSoftware))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 1", Me.Waegebereich1Config, Me.Waegebereich1Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 1", Me.Ziffernschritt1Config, Me.Ziffernschritt1Software))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 2", Me.Waegebereich2Config, Me.Waegebereich2Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 2", Me.Ziffernschritt2Config, Me.Ziffernschritt2Software))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 3", Me.Waegebereich3Config, Me.Waegebereich3Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 3", Me.Ziffernschritt3Config, Me.Ziffernschritt3Software))
        Werte.Add(New PlausibilitaetDatasource("Einschalltnullstellen", Me.EinschalltnullstellenConfig, Me.EinschalltnullstellenSoftware))

        Return Werte

    End Function

End Class

Public Class PlausibilitaetDatasource
    Public Sub New(eigenschaft As String, wertAusConfig As String, wertAusSoftware As String)
        Me.Eigenschaft = eigenschaft
        Me.WertAusConfig = wertAusConfig
        Me.WertAusSoftware = wertAusSoftware
    End Sub

    Public Property Eigenschaft As String
    Public Property WertAusConfig As String
    Public Property WertAusSoftware As String

End Class