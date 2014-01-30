'Public Class ucoStatusBullet

'#Region "Member Variables"
'    Private _State As EnumState 'Status für das Icon. 
'#End Region
'#Region "Properties"

'    ''' <summary>
'    ''' Zum ändern des Icons je nach Status
'    ''' </summary>
'    ''' <value></value>
'    ''' <remarks></remarks>
'    ''' <author></author>
'    ''' <commentauthor></commentauthor>
'    Public Property GETSETState As EnumState
'        Get
'            Return _State
'        End Get
'        Set(value As EnumState)
'            _State = value

'            'bild ändern
'            Select Case _State
'                Case EnumState.Green
'                    PictureBoxStatus.Image = My.Resources.bullet_green
'                Case EnumState.Yellow
'                    PictureBoxStatus.Image = My.Resources.bullet_yellow
'                Case EnumState.Red
'                    PictureBoxStatus.Image = My.Resources.bullet_red
'            End Select
'            Me.Refresh()
'        End Set
'    End Property

'    Public Property GETSETText As String
'        Get
'            Return lblCurrentElement.Text
'        End Get
'        Set(value As String)
'            lblCurrentElement.Text = value
'            Me.Refresh()
'        End Set
'    End Property

'#End Region


'#Region "Enums"
'    Public Enum EnumState
'        Red = 0
'        Yellow = 1
'        Green = 2
'    End Enum
'#End Region

'#Region "Constuctor"
'    Sub New()

'        ' Dieser Aufruf ist für den Designer erforderlich.
'        InitializeComponent()

'        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
'    End Sub

'    ''' <summary>
'    ''' Erzeugt eine neue Instanz des UCOs
'    ''' </summary>
'    ''' <param name="pText">Der anzuzeigende Text unter dem Bild</param>
'    ''' <param name="pState">Der Status des Bildes. </param>
'    ''' <remarks></remarks>
'    ''' <author></author>
'    ''' <commentauthor></commentauthor>
'    Sub New(ByVal pText As String, ByVal pState As EnumState)
'        ' Dieser Aufruf ist für den Designer erforderlich.
'        InitializeComponent()

'        GETSETState = pState
'        lblCurrentElement.Text = pText
'    End Sub

'#End Region

'    Private Sub ucoStatusBullet_MouseEnter(sender As Object, e As System.EventArgs) Handles Me.MouseEnter
'        Me.RectangleShape1.Visible = True
'    End Sub

'    Private Sub ucoStatusBullet_MouseLeave(sender As Object, e As System.EventArgs) Handles Me.MouseLeave
'        If Not ClientRectangle.Contains(PointToClient(Cursor.Position)) Then
'            Me.RectangleShape1.Visible = False
'        End If


'    End Sub



'End Class
