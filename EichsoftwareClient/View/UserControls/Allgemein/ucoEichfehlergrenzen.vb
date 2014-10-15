Public Class ucoEichfehlergrenzen
    Inherits ucoContent

#Region "Member Variables"
    Private _suspendEvents As Boolean = False  'Variable zum temporären stoppen der Eventlogiken 
    Private _parentForm As FrmMainContainer
#End Region

#Region "Constructors"
    Sub New()
        MyBase.New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()
    End Sub
    Sub New(ByRef pParentform As FrmMainContainer, ByRef pObjEichprozess As Eichprozess, Optional ByRef pPreviousUco As ucoContent = Nothing, Optional ByRef pNextUco As ucoContent = Nothing, Optional ByVal pEnuModus As enuDialogModus = enuDialogModus.normal)
        MyBase.New(pParentform, pObjEichprozess, pPreviousUco, pNextUco, pEnuModus)
        _parentForm = pParentform
        _suspendEvents = True
        InitializeComponent()
        _suspendEvents = False

    End Sub
#End Region

#Region "Events"

    Private Sub ucoEichfehlergrenzen_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'daten füllen
            LoadFromDatabase()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Methods"
    Private Sub LoadFromDatabase()
        'events abbrechen
        _suspendEvents = True
        Using context As New EichsoftwareClientdatabaseEntities1
            'neu laden des Objekts, diesmal mit den lookup Objekten
            objEichprozess = (From a In context.Eichprozess.Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault
            If objEichprozess Is Nothing Then _parentForm.Close()

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
        'dynamisches laden der Nullstellen:

        HoleNullstellen()


        'Steuerlemente füllen
        Select Case objEichprozess.Lookup_Waagenart.Art
            Case Is = "Einbereichswaage"
                RadGroupBox2.Visible = False
                RadGroupBox3.Visible = False
                Me.Height = Me.Height / 3
            Case Is = "Zweibereichswaage", "Zweiteilungswaage"
                RadGroupBox2.Visible = True
                RadGroupBox3.Visible = False
                Me.Height = Me.Height / 3 * 2
            Case Is = "Dreibereichswaage", "Dreieilungswaage"
                RadGroupBox2.Visible = True
                RadGroupBox3.Visible = True
                Me.Height = Me.Height / 3 * 3
        End Select

        Try
            'Dim s As String = "11.20"
            'Dim c1 As Decimal = Decimal.Parse(s, New Globalization.CultureInfo("pl"))
            'Dim c2 As Decimal = Decimal.Parse(s, New Globalization.CultureInfo("en"))
            'Dim c3 As Decimal = Decimal.Parse(s, New Globalization.CultureInfo("de"))
            'Console.WriteLine(c1)
            'Console.WriteLine(c2)



            'Bereiche berechnen
            RadTextBoxControlBereich1e20.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1e20Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1e500.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1e500Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1e2000.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1e2000Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast1), _intNullstellenE, MidpointRounding.AwayFromZero)

            'EFG Berechnen

            RadTextBoxControlBereich1EFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich1EFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich1EFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren

            RadTextBoxControlBereich1EFG20e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1EFG500e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1EFG2000e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert1 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

            'VFG Berechnen
            RadTextBoxControlBereich1VFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich1VFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich1VFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich1VFG20e.Text = Math.Round(CDec(RadTextBoxControlBereich1EFG20e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1VFG500e.Text = Math.Round(CDec(RadTextBoxControlBereich1EFG500e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich1VFG2000e.Text = Math.Round(CDec(RadTextBoxControlBereich1EFG2000e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
        Catch ex As Exception
        End Try


        Try
            'Bereiche berechnen
            RadTextBoxControlBereich2e20.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2e20Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2e500.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2e500Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2e2000.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2e2000Bis.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast2), _intNullstellenE, MidpointRounding.AwayFromZero)

            'EFG Berechnen
            RadTextBoxControlBereich2EFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich2EFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich2EFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren

            RadTextBoxControlBereich2EFG20e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2EFG500e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2EFG2000e.Text = Math.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert2 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

            'VFG Berechnen
            RadTextBoxControlBereich2VFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich2VFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich2VFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich2VFG20e.Text = Math.Round(CDec(RadTextBoxControlBereich2EFG20e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2VFG500e.Text = Math.Round(CDec(RadTextBoxControlBereich2EFG500e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich2VFG2000e.Text = Math.Round(CDec(RadTextBoxControlBereich2EFG2000e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
        Catch ex As Exception
        End Try


        Try
            'Bereiche berechnen
            RadTextBoxControlBereich3e20.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 20), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3e20Bis.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3e500.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 500), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3e500Bis.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3e2000.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 2000), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3e2000Bis.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Hoechstlast3), _intNullstellenE, MidpointRounding.AwayFromZero)

            'EFG Berechnen
            RadTextBoxControlBereich3EFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3EFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3EFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3EFG20e.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 0.5), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3EFG500e.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3EFG2000e.Text = Decimal.Round(CDec(objEichprozess.Kompatiblitaetsnachweis.Kompatiblitaet_Waage_Eichwert3 * 1.5), _intNullstellenE, MidpointRounding.AwayFromZero)

            'VFG Berechnen
            RadTextBoxControlBereich3VFG20e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3VFG500e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3VFG2000e.Mask = "F" & _intNullstellenE 'anzahl nullstellen für Textcontrol definieren
            RadTextBoxControlBereich3VFG20e.Text = Decimal.Round(CDec(RadTextBoxControlBereich3EFG20e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3VFG500e.Text = Decimal.Round(CDec(RadTextBoxControlBereich3EFG500e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
            RadTextBoxControlBereich3VFG2000e.Text = Decimal.Round(CDec(RadTextBoxControlBereich3EFG2000e.Text * 2), _intNullstellenE, MidpointRounding.AwayFromZero)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Overrides"
    Protected Overrides Sub LokalisierungNeeded(UserControl As System.Windows.Forms.UserControl)
        If Me.Equals(UserControl) = False Then Exit Sub

        MyBase.LokalisierungNeeded(UserControl)

        'lokalisierung: Leider kann ich den automatismus von .NET nicht nutzen. Dieser funktioniert nur sauber, wenn ein Dialog erzeugt wird. Zur Laufzeit aber gibt es diverse Probleme mit dem Automatischen Ändern der Sprache,
        'da auch informationen wie Positionen und Größen "lokalisiert" gespeichert werden. Wenn nun zur Laufzeit, also das Fenster größer gemacht wurde, setzt er die Anchor etc. auf die Ursprungsgröße 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucoEichfehlergrenzen))

        Me.lblBis1.Text = resources.GetString("lblBis1.Text")
        Me.lblBis2.Text = resources.GetString("lblBis1.Text")
        Me.lblBis3.Text = resources.GetString("lblBis1.Text")
        Me.lblBis4.Text = resources.GetString("lblBis1.Text")
        Me.lblBis5.Text = resources.GetString("lblBis1.Text")
        Me.lblBis6.Text = resources.GetString("lblBis1.Text")
        Me.lblBis7.Text = resources.GetString("lblBis1.Text")
        Me.lblBis8.Text = resources.GetString("lblBis1.Text")
        Me.lblBis9.Text = resources.GetString("lblBis1.Text")

        Me.lblEFG1.Text = resources.GetString("lblEFG1.Text")
        Me.lblEFG2.Text = resources.GetString("lblEFG1.Text")
        Me.lblEFG3.Text = resources.GetString("lblEFG1.Text")

        Me.lblVFG1.Text = resources.GetString("lblVFG1.Text")
        Me.lblVFG2.Text = resources.GetString("lblVFG1.Text")
        Me.lblVFG3.Text = resources.GetString("lblVFG1.Text")

        Me.RadGroupBox1.Text = resources.GetString("RadGroupBox1.text")
        Me.RadGroupBox2.Text = resources.GetString("RadGroupBox2.text")
        Me.RadGroupBox3.Text = resources.GetString("RadGroupBox3.text")
    End Sub


#End Region


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
                objEichprozess = (From a In dbcontext.Eichprozess.Include("Eichprotokoll").Include("Lookup_Auswertegeraet").Include("Kompatiblitaetsnachweis").Include("Lookup_Waegezelle").Include("Lookup_Waagenart").Include("Lookup_Waagentyp").Include("Mogelstatistik") Select a Where a.Vorgangsnummer = objEichprozess.Vorgangsnummer).FirstOrDefault

                Dim objServerEichprozess As New EichsoftwareWebservice.ServerEichprozess
                'auf fehlerhaft Status setzen
                objEichprozess.FK_Bearbeitungsstatus = 2
                objEichprozess.FK_Vorgangsstatus = GlobaleEnumeratoren.enuEichprozessStatus.Stammdateneingabe 'auf die erste Seite "zurückblättern" damit Konformitätsbewertungsbevollmächtigter sich den DS von Anfang angucken muss

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
                        Webcontext.AddEichprozess(AktuellerBenutzer.Instance.Lizenz.HEKennung, AktuellerBenutzer.Instance.Lizenz.Lizenzschluessel, objServerEichprozess, My.User.Name, System.Environment.UserDomainName, My.Computer.Name)

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


End Class
