Imports System.Globalization
Imports System.Threading
Imports System.ComponentModel
''' <summary>
''' Klasse mit Funktionen zum lokalisieren von Dialogen zur Laufzeit. Normalerweise erfordert das .NET Framework ein neu initialisieren der Objekte (Forms) um eine Lokalisierung anzuwenden. Hiermit funktioniert es on demand zur Laufzeit
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class RuntimeLocalizer
    Private Sub New()
    End Sub
    Public Shared Sub ChangeCulture(frm As Form, cultureCode As String)
        Dim culture As CultureInfo = CultureInfo.GetCultureInfo(cultureCode)

        Thread.CurrentThread.CurrentUICulture = culture
        AktuellerBenutzer.Instance.AktuelleSprache = cultureCode
        AktuellerBenutzer.SaveSettings()
        Dim resources As New ComponentResourceManager(frm.[GetType]())

        ApplyResourceToControl(resources, frm, culture)
        resources.ApplyResources(frm, "$this", culture)
    End Sub

    Public Shared Sub ChangeCulture(uco As UserControl, cultureCode As String)
        Dim culture As CultureInfo = CultureInfo.GetCultureInfo(cultureCode)

        Thread.CurrentThread.CurrentUICulture = culture
        AktuellerBenutzer.Instance.AktuelleSprache = cultureCode
        AktuellerBenutzer.SaveSettings()
        Dim resources As New ComponentResourceManager(uco.[GetType]())

        ApplyResourceToControl(resources, uco, culture)
        resources.ApplyResources(uco, "$this", culture)
    End Sub

    Private Shared Sub ApplyResourceToControl(res As ComponentResourceManager, control As Control, lang As CultureInfo)
        'If control.[GetType]() = GetType(Telerik.WinControls.UI.RadMenu) Then
        '    ' See if this is a menuStrip
        '    Dim strip As Telerik.WinControls.UI.RadMenu = DirectCast(control, Telerik.WinControls.UI.RadMenu)

        '    ApplyResourceToToolStripItemCollection(strip.Items, res, lang)
        'End If

        For Each c As Control In control.Controls
            ' Apply to all sub-controls
            If TypeOf c Is Label Or TypeOf c Is Telerik.WinControls.UI.RadLabel Or TypeOf c Is Button Or TypeOf c Is Telerik.WinControls.UI.RadButton Or TypeOf c Is CheckBox Or TypeOf c Is Telerik.WinControls.UI.RadCheckBox Or TypeOf c Is GroupBox Or TypeOf c Is Telerik.WinControls.UI.RadGroupBox Or TypeOf c Is RadioButton Or TypeOf c Is Telerik.WinControls.UI.RadRadioButton Then
                ApplyResourceToControl(res, c, lang)
                res.ApplyResources(c, c.Name, lang)
            End If

        Next

        ' Apply to self
        If TypeOf control Is Label Or TypeOf control Is Telerik.WinControls.UI.RadLabel Or TypeOf control Is Button Or TypeOf control Is Telerik.WinControls.UI.RadButton Or TypeOf control Is CheckBox Or TypeOf control Is Telerik.WinControls.UI.RadCheckBox Or TypeOf control Is GroupBox Or TypeOf control Is Telerik.WinControls.UI.RadGroupBox Or TypeOf control Is RadioButton Or TypeOf control Is Telerik.WinControls.UI.RadRadioButton Then

            res.ApplyResources(control, control.Name, lang)
        End If
    End Sub

    'Private Shared Sub ApplyResourceToToolStripItemCollection(col As ToolStripItemCollection, res As ComponentResourceManager, lang As CultureInfo)
    '    For i As Integer = 0 To col.Count - 1
    '        ' Apply to all sub items
    '        Dim item As ToolStripItem = DirectCast(col(i), ToolStripMenuItem)

    '        If item.[GetType]() = GetType(ToolStripMenuItem) Then
    '            Dim menuitem As ToolStripMenuItem = DirectCast(item, ToolStripMenuItem)
    '            ApplyResourceToToolStripItemCollection(menuitem.Items, res, lang)
    '        End If

    '        res.ApplyResources(item, item.Name, lang)
    '    Next
    'End Sub

    'Private Shared Sub ApplyResourceToToolStripItemCollection(col As Telerik.WinControls.UI.RadItemsContainer, res As ComponentResourceManager, lang As CultureInfo)
    '    For i As Integer = 0 To col.Items.Count - 1
    '        ' Apply to all sub items
    '        Dim item As ToolStripItem = DirectCast(col(i), ToolStripMenuItem)

    '        If item.[GetType]() = GetType(ToolStripMenuItem) Then
    '            Dim menuitem As ToolStripMenuItem = DirectCast(item, ToolStripMenuItem)
    '            ApplyResourceToToolStripItemCollection(menuitem.Items, res, lang)
    '        End If

    '        res.ApplyResources(item, item.Name, lang)
    '    Next
    'End Sub
End Class