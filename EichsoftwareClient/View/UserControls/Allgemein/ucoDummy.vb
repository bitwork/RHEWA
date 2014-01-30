Public Class ucoDummy
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False 'Variable zum temporären stoppen der Eventlogiken 
    Private _bolEichprozessIsDirty As Boolean = False 'variable die genutzt wird, um bei öffnen eines existierenden Eichprozesses speichern zu können wenn grundlegende Änderungen vorgenommen wurden. Wie das ändern der Waagenart und der Waegezelle. Dann wird der Vorgang auf Komptabilitätsnachweis zurückgesetzt
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco)
        _suspendEvents = True
        InitializeComponent()
        _suspendEvents = False


    End Sub
#End Region

#Region "Events"
    Private Sub ucoBeschaffenheitspruefung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                'TODO ausfüllen
                '      ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
                'Überschrift setzen
                'TODO ausfüllen
                ' ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            Catch ex As Exception
            End Try
        End If
        'TODO ausfüllen
        'EichprozessStatusReihenfolge = GlobaleEnumeratoren.enuEichprozessStatus.Kompatbilitaetsnachweis

        'daten füllen
        LoadFromDatabase()
    End Sub



    ''' <summary>
    ''' Event welches alle MouseHovers der Textboxen abfängt um den entsprechenden Hilfetext anzuzeigen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControlWaageKlasse_MouseHover(sender As Object, e As EventArgs)

        Dim senderControl As Telerik.WinControls.UI.RadTextBoxControl
        senderControl = TryCast(sender, Telerik.WinControls.UI.RadTextBoxControl)

        If Not senderControl Is Nothing Then
            Select Case senderControl.Name

                'TODO ausfüllen
                'Case Is = "RadTextBoxControlWaageTotlast"
                '    ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_Kompatiblitaetsnachweis_WaageTotlast)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' event welches prüft ob in den eingabefeldern auch nur gültige Zahlen eingegeben wurden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxContro_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'Dim result As Decimal
        'If Not sender.isreadonly = True Then

        '    'damit das Vorgehen nicht so aggresiv ist, wird es bei leerem Text ignoriert:
        '    If CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text.Equals("") Then
        '        CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
        '        Exit Sub
        '    End If

        '    'versuchen ob der Text in eine Zahl konvertiert werden kann
        '    If Not Decimal.TryParse(CType(sender, Telerik.WinControls.UI.RadTextBoxControl).Text, result) Then
        '        e.Cancel = True
        '        CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.Red
        '        System.Media.SystemSounds.Exclamation.Play()

        '    Else 'rahmen zurücksetzen
        '        CType(sender, Telerik.WinControls.UI.RadTextBoxControl).TextBoxElement.BorderColor = Color.FromArgb(0, 255, 255, 255)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' wenn an den Textboxen etwas geändert wurde, ist das objekt als Dirty zu markieren
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RadTextBoxControl_TextChanged(sender As Object, e As EventArgs)
        If _suspendEvents = True Then Exit Sub
        _bolEichprozessIsDirty = True
    End Sub
#End Region

#Region "Methods"
    Private Sub LoadFromDatabase()
        objEichprozess = ParentFormular.CurrentEichprozess
        'events abbrechen
        _suspendEvents = True
        Using context As New EichsoftwareClientdatabaseEntities1
            'neu laden des Objekts, diesmal mit den lookup Objekten
            objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.ID = objEichprozess.ID).FirstOrDefault
        End Using
        'steuerelemente mit werten aus DB füllen
        FillControls()
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

        'fokus setzen auf erstes Steuerelement
        'RadTextBoxControlWaageHoechstlast1.Focus()

    End Sub

    ''' <summary>
    ''' Füllt das Objekt mit den Werten aus den Steuerlementen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Sub UpdateObject()

    End Sub

    ''' <summary>
    ''' Gültigkeit der Eingaben überprüfen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author></author>
    ''' <commentauthor></commentauthor>
    Private Function ValidateControls() As Boolean
        'prüfen ob alle Felder ausgefüllt sind
        For Each GroupBox In RadScrollablePanel1.PanelContainer.Controls
            If TypeOf GroupBox Is Telerik.WinControls.UI.RadGroupBox Then
                For Each Control In GroupBox.controls
                    If TypeOf Control Is Telerik.WinControls.UI.RadTextBoxControl Then
                        Return False

                    End If
                Next
            End If
        Next
        If AbortSaveing Then
            MessageBox.Show(My.Resources.GlobaleLokalisierung.PflichtfelderAusfuellen, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        'Speichern soll nicht abgebrochen werden, da alles okay ist
        Me.AbortSaveing = False
        Return True

    End Function

#End Region

#Region "Overrides"
    'Speicherroutine
    Protected Friend Overrides Sub SaveNeeded(ByVal UserControl As UserControl)
        If Me.Equals(UserControl) Then

            If ValidateControls() = True Then


                'neuen Context aufbauen
                Using Context As New EichsoftwareClientdatabaseEntities1
                    'prüfen ob CREATE oder UPDATE durchgeführt werden muss
                    If objEichprozess.ID <> 0 Then 'an dieser stelle muss eine ID existieren
                        'prüfen ob das Objekt anhand der ID gefunden werden kann
                        Dim dobjEichprozess As Eichprozess = Context.Eichprozess.FirstOrDefault(Function(value) value.ID = objEichprozess.ID)
                        If Not dobjEichprozess Is Nothing Then
                            'lokale Variable mit Instanz aus DB überschreiben. Dies ist notwendig, damit das Entity Framework weiß, das ein Update vorgenommen werden muss.
                            objEichprozess = dobjEichprozess
                            'neuen Status zuweisen

                            If _bolEichprozessIsDirty = False Then
                                ' Wenn der aktuelle Status kleiner ist als der für die Beschaffenheitspruefung, wird dieser überschrieben. Sonst würde ein aktuellere Status mit dem vorherigen überschrieben


                                'TODO ausfüllen

                                'If objEichprozess.FK_Vorgangsstatus < GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis Then
                                '    objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                                'End If
                            ElseIf _bolEichprozessIsDirty = True Then
                                'TODO ausfüllen

                                'objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.KompatbilitaetsnachweisErgebnis
                                _bolEichprozessIsDirty = False
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

    Protected Friend Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoKompatiblititaetsnachweis))

        'TODO ausfüllen

        'Me.RadGroupBoxVerbindungselemente.Text = resources.GetString("RadGroupBoxVerbindungselemente.Text")


        If Not ParentFormular Is Nothing Then
            Try
                'Hilfetext setzen
                'TODO ausfüllen
                'ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
                'Überschrift setzen
                'TODO ausfüllen
                'ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            Catch ex As Exception
            End Try
        End If

        'breadcrumb setzen
        Try
            'TODO ausfüllen
            'Me.Breadcrumb.GETSETText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis

        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' aktualisieren der Oberfläche wenn nötig
    ''' </summary>
    ''' <param name="UserControl"></param>
    ''' <remarks></remarks>
    Protected Friend Overrides Sub UpdateNeeded(UserControl As UserControl)
        If Me.Equals(UserControl) Then
            MyBase.UpdateNeeded(UserControl)
            'Hilfetext setzen
            ParentFormular.SETContextHelpText(My.Resources.GlobaleLokalisierung.Hilfe_KompatiblitaetsnachweisHilfe)
            'Überschrift setzen
            ParentFormular.GETSETHeaderText = My.Resources.GlobaleLokalisierung.Ueberschrift_Kompatiblitaetsnachweis
            '   FillControls()
            LoadFromDatabase() 'war mal auskommentiert. ich weiß gerade nicht mehr wieso
        End If
    End Sub

#End Region

End Class
