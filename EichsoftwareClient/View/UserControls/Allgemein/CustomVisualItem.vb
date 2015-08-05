Imports System.Text
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Layouts
Imports Telerik.WinControls

''' <summary>
''' Klasse zum Rendern eines Custom Usercontrol List Elements. Wird genutzt für die Breadcrump Navigation. 
''' </summary>
''' <remarks>DebuggerStepThrough aktiviert. Er wird hier also nie reindebuggen, solange die Property gesetzt ist.</remarks>
<DebuggerStepThrough()>
Friend Class CustomVisualItem
    Inherits Telerik.WinControls.UI.IconListViewVisualItem
    Private imageElement As LightVisualElement
    Private titleElement As LightVisualElement
    Private stackLayout As StackLayoutPanel

    Public Sub SetBackcolor()

        Me.BackColor = Color.Red
    End Sub

    Protected Overrides Sub CreateChildElements()
        MyBase.CreateChildElements()

        stackLayout = New StackLayoutPanel()
        stackLayout.Orientation = Orientation.Vertical

        imageElement = New LightVisualElement()
        imageElement.DrawText = False
        imageElement.ImageLayout = ImageLayout.Center
        imageElement.StretchVertically = False
        imageElement.Margin = New Padding(5, 0, 5, -10)
        imageElement.NotifyParentOnMouseInput = True
        imageElement.ShouldHandleMouseInput = False


        stackLayout.Children.Add(imageElement)

        titleElement = New LightVisualElement()
        titleElement.TextAlignment = ContentAlignment.TopLeft
        titleElement.TextWrap = True
        titleElement.TextImageRelation = Windows.Forms.TextImageRelation.TextBeforeImage
        titleElement.Margin = New Padding(0, 5, -15, 0)
        titleElement.Font = New Font("Segoe UI", 10, FontStyle.Regular, GraphicsUnit.Point)
        titleElement.NotifyParentOnMouseInput = True
        titleElement.ShouldHandleMouseInput = False




        stackLayout.Children.Add(titleElement)
        stackLayout.NotifyParentOnMouseInput = True
        stackLayout.ShouldHandleMouseInput = False

        Me.Children.Add(stackLayout)
        Me.Padding = New Padding(5, 5, 5, 5)
        Me.Margin = New Padding(20, 5, 5, 10)
        Me.Shape = New RoundRectShape(3)
        Me.BorderColor = Color.FromArgb(255, 20, 100, 0)
        Me.BorderGradientStyle = GradientStyles.Solid
        Me.DrawBorder = True
        Me.DrawFill = True
        Me.BackColor = Color.White
        Me.GradientStyle = GradientStyles.Solid

        Me.NotifyParentOnMouseInput = True
        Me.ShouldHandleMouseInput = True
    End Sub

    Protected Overrides Sub SynchronizeProperties()
        Try
            MyBase.SynchronizeProperties()
        Catch ex As RowNotInTableException

        End Try
        Me.Text = ""
        Me.imageElement.Image = Image.FromStream(New System.IO.MemoryStream(CType(Me.Data("Image"), Byte())))
        Me.titleElement.Text = Convert.ToString(Me.Data("Title"))
        'Me.artistElement.Text = Convert.ToString(Me.Data("ArtistName"))
    End Sub

    Protected Overloads Overrides Function MeasureOverride(ByVal availableSize As SizeF) As SizeF
        Dim measuredSize As SizeF = MyBase.MeasureOverride(availableSize)

        Me.stackLayout.Measure(measuredSize)

        Return measuredSize
    End Function

    Protected Overrides Function ArrangeOverride(ByVal finalSize As SizeF) As SizeF
        MyBase.ArrangeOverride(finalSize)

        Me.stackLayout.Arrange(New RectangleF(PointF.Empty, finalSize))

        Return finalSize
    End Function

    Protected Overrides ReadOnly Property ThemeEffectiveType() As Type
        Get
            Return GetType(SimpleListViewVisualItem)
        End Get
    End Property

End Class

