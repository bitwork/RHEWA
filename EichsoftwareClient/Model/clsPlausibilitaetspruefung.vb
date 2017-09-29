Imports System.Data.OleDb
Imports System.Xml
Imports System.Xml.Linq
''' <summary>
''' klasse mit funktionen für die plausbilitätsprüfung
''' </summary>
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
    Private EinschalltnullstellenMax As String
    Private EinschalltnullstellenMin As String
    Private EichsiegelOffen As String

    Private OriginalStatus As String

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

    Public ReadOnly Property EichsiegelOffenConfig As String
        Get
            Return EichsiegelOffen
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

    Public ReadOnly Property EinschalltnullstellenConfigMax As String
        Get
            Return EinschalltnullstellenMax
        End Get

    End Property

    Public ReadOnly Property EinschalltnullstellenConfigMIN As String
        Get
            Return EinschalltnullstellenMin
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

    Public ReadOnly Property OriginalstatusConfig As String
        Get
            Return OriginalStatus
        End Get
    End Property

#End Region

#Region "AusSoftware"

    Public ReadOnly Property FabriknummerSoftware As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_FabrikNummer
        End Get
    End Property

    Public ReadOnly Property EichsiegelOffenSoftware As String
        Get
            Return "N/A"
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
            Return _objEichprozess.Eichprotokoll.Komponenten_Eichzaehlerstand
        End Get
    End Property

    Public ReadOnly Property ErdbeschleunigungSoftware As String
        Get
            Return _objEichprozess.Eichprotokoll.Fallbeschleunigung_g
        End Get
    End Property

    Public ReadOnly Property MehrbereichSoftware As String
        Get
            Return _objEichprozess.Lookup_Waagenart.Art
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

    Public ReadOnly Property EinschalltnullstellenSoftwareMax As String
        Get
            Return _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Einschaltnullstellbereich
        End Get
    End Property

    Public ReadOnly Property EinschalltnullstellenSoftwaremin As String
        Get
            Return "N/A"
        End Get
    End Property

    Public ReadOnly Property UebersetzungsverhaeltnisSoftware As String
        Get
            Return Uebersetzungsverhaeltnis
        End Get
    End Property

    Public ReadOnly Property OriginalstatusSoftware As String
        Get
            Return "N/A"
        End Get
    End Property

#End Region

#End Region

    Public Function LadeWerte(waegebereich As Integer, filename As String, objEichprozess As Eichprozess) As Boolean
        _objEichprozess = objEichprozess
        _Waegebereich = waegebereich

        Uebersetzungsverhaeltnis = _objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Uebersetzungsverhaeltnis

        _filename = filename
        If filename.ToLower.EndsWith(".xml") Then
            HoleWerteXML()
        ElseIf filename.ToLower.EndsWith(".mdb") Then
            HoleWerteACCDB()
        Else
            Return False
        End If

        Return True
    End Function

#Region "ACCDB"
    Private Function HoleWerteACCDB() As Boolean
        Dim conn As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", _filename, CONSTACCESSPASSWORD)
        Dim cmd As String = ""
        Dim adapter As OleDbDataAdapter = Nothing

        Dim dtJustage As New DataTable
        Dim dtInformation As New DataTable
        Dim dtAllgemein As New DataTable
        Dim dtWBEinstellungen As New DataTable
        Dim dtKonfiguration As New DataTable

        LadeInformationenACCDB(conn, cmd, adapter, dtInformation)
        LadeKonfigurationACCDB(conn, cmd, adapter, dtKonfiguration)
        LadeAllgemeineWerteACCDB(conn, cmd, adapter, dtAllgemein)
        LadeEinstellungenACCDB(conn, cmd, adapter, dtWBEinstellungen)
        LadeJustageWerteACCDB(conn, cmd, adapter, dtJustage)

        VerarbeiteInformationenACCDB(dtInformation)
        VerarbeiteKonfigurationACCDB(dtKonfiguration)
        VerarbeiteAllgemeineWerteACCDB(dtAllgemein)
        VerarbeiteEinstellungenACCDB(dtWBEinstellungen)
        VerarbeiteJustageWerteACCDB(dtJustage)
        Return True
    End Function

    Private Sub VerarbeiteJustageWerteACCDB(dtJustage As DataTable)
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
                        If IsNumeric(row(column)) AndAlso CDec(row(column)) > maxAnalog AndAlso CDec(row(column)) <> 10000000 Then
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
    End Sub

    Private Sub VerarbeiteEinstellungenACCDB(dtWBEinstellungen As DataTable)
        For Each row As DataRow In dtWBEinstellungen.Rows
            If Not IsDBNull(row("Mehrbereich")) Then
                Mehrbereich = row("Mehrbereich").ToString
            Else
                Mehrbereich = "N/A"
            End If
            If Not IsDBNull(row("Gewichtseinheit")) Then
                Gewichtseinheit = row("Gewichtseinheit")
            Else
                MsgBox("Gewichtseinheit ist nicht definiert, es wird mit kg gerechnet")
                Gewichtseinheit = 1
            End If

            Waegebereich1 = row("Max1").ToString
            Waegebereich2 = row("Max2").ToString
            Waegebereich3 = row("Max3").ToString
            Ziffernschritt1 = row("Teilung1").ToString
            Ziffernschritt2 = row("Teilung2").ToString
            Ziffernschritt3 = row("Teilung3").ToString
            Try
                EinschalltnullstellenMax = row("konObereToleranzE")
                EinschalltnullstellenMin = row("konUntereToleranzE")
            Catch ex As Exception

            End Try

            Exit For
        Next
    End Sub

    Private Sub VerarbeiteAllgemeineWerteACCDB(dtAllgemein As DataTable)
        For Each row As DataRow In dtAllgemein.Rows
            Erdbeschleunigung = row("gWert")
            Exit For
        Next
    End Sub

    Private Sub VerarbeiteKonfigurationACCDB(dtKonfiguration As DataTable)
        For Each row As DataRow In dtKonfiguration.Rows
            OriginalStatus = row("Original")
            Exit For
        Next
    End Sub

    Private Sub VerarbeiteInformationenACCDB(dtInformation As DataTable)
        For Each row As DataRow In dtInformation.Rows
            Fabriknummer = row("Fabriknummer")
            FirmwareVersion = row("Version")
            Model = row("Typ")
            Eichzaehlerstand = row("EichsiegelNr")
            Try
                EichsiegelOffen = row("konEichsiegel")
            Catch ex As Exception
                EichsiegelOffen = "N/A"
            End Try
            Exit For
        Next
    End Sub

    Private Shared Sub LadeJustageWerteACCDB(conn As String, ByRef cmd As String, ByRef adapter As OleDbDataAdapter, dtJustage As DataTable)
        Try
            cmd = "Select konADWert1, konADWert2,konADWert3,konADWert4,konADWert5, konGewichtswert1, konGewichtswert2, konGewichtswert3, konGewichtswert4, konGewichtswert5 from tblKon_WBJustage"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtJustage)
        Catch ex As Exception
            cmd = "Select konADWert1, konADWert2,konADWert3,konADWert4,konADWert5, konGewichtswert1, konGewichtswert2, konGewichtswert3, konGewichtswert4, konGewichtswert5 from tblKonfiguration_WBJustage"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtJustage)
        End Try
    End Sub

    Private Shared Sub LadeEinstellungenACCDB(conn As String, ByRef cmd As String, ByRef adapter As OleDbDataAdapter, dtWBEinstellungen As DataTable)
        Try
            cmd = "Select konWaegebereich as Mehrbereich, konGewichtseinheit as Gewichtseinheit, konMaxBereich1 as Max1, konTeilungBereich1 as Teilung1, konMaxBereich2 as Max2, konTeilungBereich2 as Teilung2, konMaxBereich3 as Max3, konTeilungBereich3 as Teilung3, konEinschaltnustelle as Einschaltnullstelle, konObereToleranzE, konUntereToleranzE from tblKon_WBEinstellungen"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtWBEinstellungen)
        Catch ex As Exception
            cmd = "Select konMehrbereich as Mehrbereich, konGewichtseinheit as Gewichtseinheit, konMax1 as Max1, konTeilung1 as Teilung1, konMax2 as Max2, konTeilung2 as Teilung2, konMax3 as Max3, konTeilung3 as Teilung3, konEinschaltnullstellen as Einschaltnullstelle, konObereToleranzE,konUntereToleranzE from tblKonfiguration_WBEinstellung"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtWBEinstellungen)
        End Try
    End Sub

    Private Shared Sub LadeAllgemeineWerteACCDB(conn As String, ByRef cmd As String, ByRef adapter As OleDbDataAdapter, dtAllgemein As DataTable)
        Try
            cmd = "Select konGwert as gWert from tblKon_Allgemein"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtAllgemein)
        Catch ex As Exception
            cmd = "Select konErdbeschleunigung_gWert as gWert from tblKonfiguration_JustageAllgemein"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtAllgemein)
        End Try
    End Sub

    Private Shared Sub LadeKonfigurationACCDB(conn As String, ByRef cmd As String, ByRef adapter As OleDbDataAdapter, dtKonfiguration As DataTable)
        Try
            cmd = "Select Original from tblKonfigurationen"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtKonfiguration)
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub LadeInformationenACCDB(conn As String, ByRef cmd As String, ByRef adapter As OleDbDataAdapter, dtInformation As DataTable)
        Try
            cmd = "Select Fabrik_Nr as Fabriknummer, konFirmware as Version, konGeraeteTyp as Typ, konEichsiegelnr as EichsiegelNr, konEichsiegel   from tblKon_Information"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtInformation)
        Catch ex As Exception
            cmd = "Select konFabrikationsnummer as Fabriknummer, konProgrammversion as Version, konAuswertegeraetetyp as Typ, konEichsiegelnr as EichsiegelNr,konEichsiegel from tblKonfiguration_Information"
            adapter = New OleDbDataAdapter(cmd, conn)
            adapter.Fill(dtInformation)
        End Try
    End Sub
#End Region

#Region "XML"

    Private Function HoleWerteXML() As Boolean
        Dim xd As XmlDocument = New XmlDocument()
        xd.Load(_filename)

        Dim nl As XmlNodeList = LadeInformationenXML(xd)

        nl = LadeAllgemeineWerteXML(xd)
        nl = LadeEinstellungenXML(xd)
        nl = LadeKonfigurationXML(xd)
        nl = LadeEichzaehlerstandXML(xd)
        nl = LadeEinstellbereichXML(xd)

        'Dim nl As XmlNodeList = xd.GetElementsByTagName("WP_Adjustment")
        'For Each node As XmlNode In nl
        '    Console.WriteLine(node.InnerText)
        'Next node
        Return True
    End Function

    Private Function LadeEinstellbereichXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("WP_Adjustment")
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

            For i As Integer = 0 To 9
                If i = 0 OrElse i Mod 2 = 0 Then 'die geraden Werte entsprechen den Gewichten
                    Dim Gewicht As Decimal
                    Try
                        If Werte(i) = "" Then Werte(i) = 0
                        Gewicht = CDec(Werte(i))
                    Catch ex As Exception
                        Gewicht = 0
                    End Try

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
                    Dim Analogwert As Decimal
                    Try
                        If Werte(i) = "" Then Werte(i) = 0
                        Analogwert = Werte(i)
                    Catch ex As Exception
                        Analogwert = 0
                    End Try

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

        Return nl
    End Function

    Private Function LadeEichzaehlerstandXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("VerificationInfo")
        For Each node As XmlNode In nl
            Dim Werte = node.InnerText.Split(";")

            Eichzaehlerstand = "0"
            EichsiegelOffen = Werte(0)(_Waegebereich - 1)
            'eine einheit bei werte(1) ist immer 36 Zeichen lang
            Dim werteeichsiegel = ChunksUpto(Werte(1), 36)
            For Each wert In werteeichsiegel
                If wert(0).ToString.Equals(_Waegebereich.ToString) Then 'Waegebruecke auslesen
                    If CInt(wert(5).ToString) > CInt(Eichzaehlerstand) Then
                        Eichzaehlerstand = wert(5)
                    End If
                End If

            Next
        Next

        Return nl
    End Function

    Private Function LadeKonfigurationXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("Configurations")
        For Each node As XmlNode In nl
            For Each subnode As XmlNode In node.ChildNodes
                If subnode.Name.ToString.Equals("Original") Then
                    OriginalStatus = subnode.FirstChild.Value
                End If
            Next
        Next

        Return nl
    End Function

    Private Function LadeEinstellungenXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("WP_Settings")
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
            Mehrbereich = "TODO noch nicht in Konfig gefunden"
            Gewichtseinheit = Werte(0) ' 0 = tonnen ; = kg 2; g
            Waegebereich1 = Werte(3)
            Ziffernschritt1 = Werte(4)
            Waegebereich2 = Werte(5)
            Ziffernschritt2 = Werte(6)
            Waegebereich3 = Werte(7)
            Ziffernschritt3 = Werte(8)
            EinschalltnullstellenMin = Werte(21)
            EinschalltnullstellenMax = Werte(22)

        Next

        Return nl
    End Function

    Private Function LadeAllgemeineWerteXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("General")
        For Each node As XmlNode In nl
            Dim Werte = node.InnerText.Split(";")
            Erdbeschleunigung = Werte(0)
        Next node

        Return nl
    End Function

    Private Function LadeInformationenXML(xd As XmlDocument) As XmlNodeList
        Dim nl As XmlNodeList = xd.GetElementsByTagName("Information")
        For Each node As XmlNode In nl
            Dim Werte = node.InnerText.Split(";")
            Fabriknummer = Werte(1)
            FirmwareVersion = Werte(4)
            Model = Werte(0)
        Next node

        Return nl
    End Function

    ''' <summary>
    ''' hilfsfunktion um teil eines Strings zu extrahieren
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="maxChunkSize"></param>
    ''' <returns></returns>
    Private Shared Iterator Function ChunksUpto(str As String, maxChunkSize As Integer) As IEnumerable(Of String)
        Dim i As Integer = 0
        While i < str.Length
            Yield str.Substring(i, Math.Min(maxChunkSize, str.Length - i))
            i += maxChunkSize
        End While
    End Function
#End Region

    ''' <summary>
    ''' wandelt die Ergebnisse in das DTO um
    ''' </summary>
    ''' <returns></returns>
    Public Function getAsDatasource() As List(Of PlausibilitaetDatasource)
        Dim Werte As New List(Of PlausibilitaetDatasource)

        Werte.Add(New PlausibilitaetDatasource("Fabriknummer", Me.FabriknummerConfig, Me.FabriknummerSoftware))
        Werte.Add(New PlausibilitaetDatasource("Firmware-Version", Me.FirmwareVersionConfig, Me.FirmwareVersionSoftware))
        Werte.Add(New PlausibilitaetDatasource("Model", Me.ModelConfig, Me.ModelSoftware))
        Werte.Add(New PlausibilitaetDatasource("Original", Me.OriginalstatusConfig, Me.OriginalstatusSoftware))

        Werte.Add(New PlausibilitaetDatasource("Eichzählerstand", Me.EichzaehlerstandConfig, Me.EichzaehlerstandSoftware))
        Werte.Add(New PlausibilitaetDatasource("Eichsiegel offen", Me.EichsiegelOffen, Me.EichsiegelOffenSoftware))
        Werte.Add(New PlausibilitaetDatasource("Erdbeschleunigung", Me.ErdbeschleunigungConfig, Me.ErdbeschleunigungSoftware))
        Werte.Add(New PlausibilitaetDatasource("Mehrbereich", Me.MehrbereichConfig, Me.MehrbereichSoftware))
        Werte.Add(New PlausibilitaetDatasource("Gewichtseinheit", Me.GewichtseinheitConfig, Me.GewichtseinheitSoftware))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 1", Me.Waegebereich1Config, Me.Waegebereich1Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 1", Me.Ziffernschritt1Config, Me.Ziffernschritt1Software))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 2", Me.Waegebereich2Config, Me.Waegebereich2Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 2", Me.Ziffernschritt2Config, Me.Ziffernschritt2Software))
        Werte.Add(New PlausibilitaetDatasource("Wägebereich 3", Me.Waegebereich3Config, Me.Waegebereich3Software))
        Werte.Add(New PlausibilitaetDatasource("Ziffernschritt 3", Me.Ziffernschritt3Config, Me.Ziffernschritt3Software))
        Werte.Add(New PlausibilitaetDatasource("Einschalltnullstellen untere Toleranz", Me.EinschalltnullstellenConfigMIN, Me.EinschalltnullstellenSoftwaremin))
        Werte.Add(New PlausibilitaetDatasource("Einschalltnullstellen obere Toleranz", Me.EinschalltnullstellenConfigMax, Me.EinschalltnullstellenSoftwareMax))

        Return Werte

    End Function

End Class

''' <summary>
''' DTO Objekt zur Darstellung der Ergebnisse, unabhängig davon, ob sie aus Access oder XML geladen wurden
''' </summary>
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