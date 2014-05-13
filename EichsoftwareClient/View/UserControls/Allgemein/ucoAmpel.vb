Imports System.Drawing
Imports System.Drawing.Imaging
''' <summary>
''' UCO für die Amplefunktion oben Rechts. Navigation und Anzeige des Statuses
''' </summary>
''' <remarks></remarks>
''' <author></author>
''' <commentauthor></commentauthor>
Public Class ucoAmpel
    Public Event Navigieren(ByVal GewaehlterVorgang As GlobaleEnumeratoren.enuEichprozessStatus)
    Private Event NotifyPropertyChanged()

    Private Datasource As New DataTable
    Private WithEvents _ParentForm As FrmMainContainer



    Private _aktuellerGewaehlterVorgang As GlobaleEnumeratoren.enuEichprozessStatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe

#Region "Konstruktoren"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Me.RadListView1.AllowEdit = False
        Me.RadListView1.AllowRemove = False
        Me.RadListView1.FullRowSelect = True
        Me.RadListView1.ItemSize = New Size(32, 32)
        Me.RadListView1.Location = New Point(0, 0)
        Me.RadListView1.Name = "radListView1"
        Me.RadListView1.Size = New Size(287, 106)
        Me.RadListView1.TabIndex = 0
        Me.RadListView1.Text = "radListView1"
        Me.RadListView1.ViewType = Telerik.WinControls.UI.ListViewType.IconsView

        Me.RadListView1.ItemSize = New Size(150, 100)
        'Me.RadListView1.AllowArbitraryItemHeight = True
        Me.RadListView1.ItemSpacing = 5
        Me.RadListView1.EnableKineticScrolling = False
        Me.RadListView1.ListViewElement.ViewElement.ViewElement.Margin = New Padding(0, 5, 0, 5)
        Me.RadListView1.ListViewElement.ViewElement.Orientation = Orientation.Horizontal
        RadListView1.ListViewElement.NotifyParentOnMouseInput = True
        RadListView1.ListViewElement.ShouldHandleMouseInput = True
        ' Add any initialization after the InitializeComponent() call.

        Datasource.TableName = "Bullets"
        Datasource.Columns.Add("Title", GetType(String))

        Datasource.Columns.Add("Status", GetType(String))
        Try
            Dim colArray(1) As Data.DataColumn
            colArray(0) = Datasource.Columns("Status")
            Datasource.PrimaryKey = colArray
        Catch e As Exception
        End Try
        Datasource.Columns.Add("Image", GetType(Byte()))

        FillDataset()

    End Sub

    Sub New(ByVal pParentForm As Form)

        ' This call is required by the designer.
        InitializeComponent()

        Me.RadListView1.AllowEdit = False
        Me.RadListView1.AllowRemove = False
        Me.RadListView1.FullRowSelect = True
        Me.RadListView1.ItemSize = New Size(32, 32)
        Me.RadListView1.Location = New Point(0, 0)
        Me.RadListView1.Name = "radListView1"
        Me.RadListView1.Size = New Size(287, 106)
        Me.RadListView1.TabIndex = 0
        Me.RadListView1.Text = "radListView1"
        Me.RadListView1.ViewType = Telerik.WinControls.UI.ListViewType.IconsView

        Me.RadListView1.ItemSize = New Size(150, 100)
        'Me.RadListView1.AllowArbitraryItemHeight = True
        Me.RadListView1.ItemSpacing = 5
        Me.RadListView1.EnableKineticScrolling = False
        Me.RadListView1.ListViewElement.ViewElement.ViewElement.Margin = New Padding(0, 5, 0, 5)
        Me.RadListView1.ListViewElement.ViewElement.Orientation = Orientation.Horizontal
        ' Add any initialization after the InitializeComponent() call.
        RadListView1.ListViewElement.NotifyParentOnMouseInput = True
        RadListView1.ListViewElement.ShouldHandleMouseInput = True

        Datasource.TableName = "Bullets"
        Datasource.Columns.Add("Title", GetType(String))
        Datasource.Columns.Add("Status", GetType(String))
        Try
            Dim colArray(1) As Data.DataColumn
            colArray(0) = Datasource.Columns("Status")
            Datasource.PrimaryKey = colArray
        Catch e As Exception
        End Try
        Datasource.Columns.Add("Image", GetType(Byte()))

        FillDataset()

        Try
            _ParentForm = pParentForm
        Catch ex As Exception
        End Try
    End Sub

#End Region



#Region "properties"

    Public Property AktuellerGewaehlterVorgang As GlobaleEnumeratoren.enuEichprozessStatus
        Get
            Return _aktuellerGewaehlterVorgang
        End Get
        Set(value As GlobaleEnumeratoren.enuEichprozessStatus)
            _aktuellerGewaehlterVorgang = value
            RaiseEvent NotifyPropertyChanged()
        End Set
    End Property

 


#End Region

#Region "enumeratoren"
    Public Enum enuImage
        grün = 1
        gelb = 2
        rot = 3
    End Enum
#End Region

#Region "Methoden"

    Private Sub LokalisierungNeeded() Handles _ParentForm.LokalisierungNeeded
        FillDataset()
        Changes()
    End Sub

    Private Sub Changes() Handles Me.NotifyPropertyChanged
      
        'ändern des bildes
        ChangeImageOfElement(AktuellerGewaehlterVorgang)

        If Not _ParentForm.CurrentEichprozess Is Nothing Then
            'erneutes überprüfen auf Stati die nun ungültig sind
            HideElement(GetListeUngueltigeStati(_ParentForm.CurrentEichprozess))
        End If
        FindeElementUndSelektiere(AktuellerGewaehlterVorgang)
    End Sub

    ''' <summary>
    ''' Ändert die Icons des gewählten Elements und alle denen davor. Wenn ein Element auf grün ist, müssen davor auch alle grün sein. Dann kann es max 1 gelbes geben und alles danach ist rot
    ''' </summary>
    ''' <param name="pStatus"></param>
    ''' <remarks></remarks>
    Private Sub ChangeImageOfElement(ByVal pStatus As GlobaleEnumeratoren.enuEichprozessStatus)
        Try
            For Each item As DataRow In Datasource.Rows
                If CInt(item("Status")) < pStatus Then
                    item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_green)

                ElseIf CInt(item("Status")) = pStatus Then
                    'sonderfall versenden = fertig
                    If pStatus = GlobaleEnumeratoren.enuEichprozessStatus.Versenden Then
                        If Not _ParentForm.CurrentEichprozess Is Nothing Then
                            If _ParentForm.CurrentEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet Or _
 _ParentForm.CurrentEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.Fehlerhaft Then
                                item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_yellow)
                            Else
                                item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_green)
                                Datasource.AcceptChanges()
                                'FindeElementUndSelektiere(GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe)
                                Exit Sub
                            End If
                        Else
                            item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_yellow)
                        End If
                    Else
                        item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_yellow)
                    End If


                ElseIf CInt(item("Status")) > pStatus Then
                    item("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)

                End If

            Next
            Datasource.AcceptChanges()

            FindeElementUndSelektiere(pStatus)
        Catch ex As Exception

        End Try

    End Sub

    ' ''' <summary>
    ' ''' Markiert das übergebenen Element als Aktives und setzt den Fokus
    ' ''' </summary>
    ' ''' <remarks></remarks>
    ' ''' <author></author>
    ' ''' <commentauthor></commentauthor>
    'Public Sub FindeElementUndSelektiere(ByVal objEichprozess As Eichprozess) ' pStatus As GlobaleEnumeratoren.enuEichprozessStatus)
    '    Try
    '        Dim items(0) As Telerik.WinControls.UI.ListViewDataItem

    '        For Each item In RadListView1.Items
    '            'If item.Value = CInt(pStatus) Then
    '            If item.Value = CInt(objEichprozess.FK_Vorgangsstatus) Then
    '                If objEichprozess.FK_Bearbeitungsstatus = GlobaleEnumeratoren.enuBearbeitungsstatus.noch_nicht_versendet Then
    '                    items(0) = item
    '                    RadListView1.Select(items)
    '                    Exit For
    '                Else
    '                    items(0) = RadListView1.Items(0)
    '                    RadListView1.Select(items)
    '                    Exit For
    '                End If

    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''' <summary>
    ''' Markiert das übergebenen Element als Aktives und setzt den Fokus
    ''' </summary>
    ''' <param name="pStatus"></param>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Public Sub FindeElementUndSelektiere(ByVal pStatus As GlobaleEnumeratoren.enuEichprozessStatus)
        Try
            Dim items(0) As Telerik.WinControls.UI.ListViewDataItem

            'workaounrd für fokussierungsfehler. Damit das aktuell gewählte element in der Mitte angezeigt wird, wird zunächst das vorherige Element ausgewählt und dann erst das korrekte

            'vorheriges Element finden und selektieren
            Dim Listitem = (From raditem In RadListView1.Items Where raditem.Value = pStatus - 1 And raditem.Visible = True).FirstOrDefault
            If Listitem Is Nothing Then
                Listitem = (From raditem In RadListView1.Items Where raditem.Value = pStatus - 2 And raditem.Visible = True).FirstOrDefault
            End If

            items(0) = Listitem
            RadListView1.Select(items)
            RadListView1.Focus()

            'tatsächliches element finden und selektieren
            Listitem = (From raditem In RadListView1.Items Where raditem.Value = pStatus And raditem.Visible = True).FirstOrDefault
            If Listitem Is Nothing Then
                Listitem = (From raditem In RadListView1.Items Where raditem.Value = pStatus - 1 And raditem.Visible = True).FirstOrDefault
            End If
            items(0) = Listitem
            RadListView1.SelectedItem = Nothing
            RadListView1.Select(items)
            RadListView1.Focus()
          
        Catch ex As Exception

        End Try
    End Sub



    ''' <summary>
    ''' Füllt das Grid mit den bisher bekannten Vorgangsstati
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor>In dem Grid stehen dann aber alle Stati. Es werden nicht unbenötigte ausgeblendet</commentauthor>
    Private Sub FillDataset()
        Try
            Datasource.AcceptChanges()
            Datasource.Clear()
            Datasource.AcceptChanges()
        Catch ex As Exception

        End Try
        Dim nrow As DataRow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Stammdaten
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)


        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_KompatiblitaetsnachweisErgebnis
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Beschaffenheitspruefung
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Beschaffenheitspruefung)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Eichprotokollverfahrensauswahl
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.AuswahlKonformitätsverfahren)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_EichprotokollStammdaten
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.EichprotokollStammdaten)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungNullstellungundAussermittigeBelastung
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderGenauigkeitderNullstellungUndAussermittigeBelastung)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerRichtigkeitMitNormallast
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStaffelverfahren
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungDerWiederholbarkeit
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderWiederholbarkeit)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungUeberlastAnzeige
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderÜberlastanzeige)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungRollendelasten
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungAnsprechvermoegen
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungdesAnsprechvermögens)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungStabilitaet
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungTaraEinrichtung
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Taraeinrichtung)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEignungAchslastwaegungen
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungFallbeschleunigung
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.BerücksichtigungderFallbeschleunigung)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_PruefungEichtechnischeSicherung
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.EichtechnischeSicherungundDatensicherung)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Exports
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Export)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)

        nrow = Datasource.NewRow
        nrow("Title") = My.Resources.GlobaleLokalisierung.Ueberschrift_Versenden
        nrow("Status") = CInt(GlobaleEnumeratoren.enuEichprozessStatus.Versenden)
        nrow("Image") = ConvertBitmapToByteArray(My.Resources.bullet_red)
        Datasource.Rows.Add(nrow)


        RadListView1.DataSource = Datasource
        RadListView1.DisplayMember = "Title"
        RadListView1.ValueMember = "Status"

        ' give each of the data items a key (= the id of the usercontrol)
        For Each item As Telerik.WinControls.UI.ListViewDataItem In RadListView1.Items
            item.Key = item.Value
        Next

        Datasource.AcceptChanges()
    End Sub

    ''' <summary>
    ''' Blendet Vorgangs Element aus
    ''' </summary>
    ''' <param name="pStatus"></param>
    ''' <remarks></remarks>
    Public Sub HideElement(ByVal pStatus As List(Of GlobaleEnumeratoren.enuEichprozessStatus))
        'Dim view As New DataView
        'view.Table = Datasource
        'view.RowFilter = "Status = '" & pStatus & "'"

        'For Each item As DataRowView In view 'sollte nur eines sein
        '    Datasource.Rows.Remove(item.Row)
        'Next
        If pStatus Is Nothing Then Exit Sub
        Try
            For Each item In RadListView1.Items
                If pStatus.Contains(item.Value) Then
                    item.Visible = False
                Else
                    item.Visible = True
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

#End Region



#Region "telerik"
    Private Function ConvertBitmapToByteArray(ByVal img As Bitmap) As Byte()
        Dim stream As New IO.MemoryStream()
        img.Save(stream, ImageFormat.Png)

        Dim byteArray As Byte() = stream.GetBuffer()
        Return byteArray
    End Function

    ''' <summary>
    ''' navigieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadListView1_ItemMouseClick(sender As Object, e As Telerik.WinControls.UI.ListViewItemEventArgs) Handles RadListView1.ItemMouseClick
        'abbruch falls Status noch rot. hier darf nicht hingesprungen werden
        Try
            Dim ItemImage = DirectCast(e.Item("Image"), Byte())
            Dim CompareImage = ConvertBitmapToByteArray(My.Resources.bullet_red)

            'im debugger zur einfachheit, kann per click auf jeden Status gesprungen werden
            If Debugger.IsAttached Then
                RaiseEvent Navigieren(e.Item("Status"))
                Exit Sub
            Else
                'prüfen ob das gewählte element rot ist. wenn nicht das letzte gelbe element wählen
                If ItemImage.SequenceEqual(CompareImage) Then
                    Me.FindeElementUndSelektiere(Me.AktuellerGewaehlterVorgang)
                    Exit Sub
                End If
            End If


            RaiseEvent Navigieren(e.Item("Status"))
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub
  
    Private Sub radListView1_VisualItemCreating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ListViewVisualItemCreatingEventArgs) Handles RadListView1.VisualItemCreating
        e.VisualItem = New CustomVisualItem()
    End Sub

#End Region


#Region "Hilfsfunktionen"

    ''' <summary>
    ''' Erzeugt eine Liste von Stati die für den aktuellen Eichrpozess ungültig sind. Z.B. Staffelverfahren bei einbereichwaagen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Wird z.b. für UCOAmpel genutzt umben�tigte Felder auszublenden</remarks>
    Private Function GetListeUngueltigeStati(objEichprozess As Eichprozess) As List(Of GlobaleEnumeratoren.enuEichprozessStatus)
        Try
            Dim returnlist As New List(Of GlobaleEnumeratoren.enuEichprozessStatus)

            'wenn es sich nicht um ein flüchtiges Object handelt (server seitiges objekt (kopie von Server objekt)
            'versuche aufruf,
            Try
                'Versuch
                Dim obj = objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                'ansonsten reinitialsiere Objekt aus lokaler DB
                LadeVonServerObjekt(objEichprozess, returnlist)
            Catch ex As NullReferenceException
                'neu laden des Objekts, diesmal mit den lookup Objekten
                LadeVonLokalerDB(objEichprozess, returnlist)
            Catch ex As ObjectDisposedException
                'neu laden des Objekts, diesmal mit den lookup Objekten
                LadeVonLokalerDB(objEichprozess, returnlist)
            End Try

            Return returnlist
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Debug.WriteLine(ex.StackTrace)

            Return Nothing
        End Try
    End Function

    Private Sub LadeVonLokalerDB(ByRef objEichprozess As Eichprozess, ByRef returnlist As List(Of GlobaleEnumeratoren.enuEichprozessStatus))
        Using context As New EichsoftwareClientdatabaseEntities1
            Dim vorgangsnummer As String = objEichprozess.Vorgangsnummer
            objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = vorgangsnummer).FirstOrDefault
            If Not objEichprozess Is Nothing Then
                'Achslastw�gungen
                If Not objEichprozess.Eichprotokoll Is Nothing Then
                    Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                        Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                            ' Wenn der aktuelle Status kleiner ist als der f�r die Beschaffenheitspruefung, wird dieser �berschrieben. Sonst w�rde ein aktuellere Status mit dem vorherigen �berschrieben
                            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
                            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
                        Case Is = "Fahrzeugwaagen"
                    End Select

                    Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                        Case Is = "über 60kg mit Normalien"
                            ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
                        Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                            ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
                    End Select


                    If objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                        returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
                    End If
                Else
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
                End If
            Else
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
            End If
        End Using

    End Sub

    Private Sub LadeVonServerObjekt(ByRef objEichprozess As Eichprozess, ByRef returnlist As List(Of GlobaleEnumeratoren.enuEichprozessStatus))
        If Not objEichprozess Is Nothing Then
            'Achslastwügungen
            If Not objEichprozess.Eichprotokoll Is Nothing Then
                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien", "über 60kg im Staffelverfahren"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
                        returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
                    Case Is = "Fahrzeugwaagen"
                End Select

                Select Case objEichprozess.Eichprotokoll.Lookup_Konformitaetsbewertungsverfahren.Verfahren
                    Case Is = "über 60kg mit Normalien"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
                    Case Is = "Fahrzeugwaagen", "über 60kg im Staffelverfahren"
                        ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben
                        returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
                End Select


                If objEichprozess.Eichprotokoll.Verwendungszweck_Drucker = False Then
                    returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
                End If
            Else
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
                returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
            End If
        Else
            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitNormallastLinearitaet)
            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.EignungfürAchslastwägungen)
            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.WaagenFuerRollendeLasten)
            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderRichtigkeitmitErsatzlast)
            returnlist.Add(GlobaleEnumeratoren.enuEichprozessStatus.PrüfungderStabilitätderGleichgewichtslage)
        End If
    End Sub
#End Region

End Class

