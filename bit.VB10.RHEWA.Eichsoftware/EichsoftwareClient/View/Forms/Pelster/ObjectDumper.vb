Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text

Public Class ObjectDumper
    Private _level As Integer
    Private ReadOnly _indentSize As Integer
    Private ReadOnly _stringBuilder As StringBuilder
    Private ReadOnly _hashListOfFoundElements As List(Of Integer)

    Private Sub New(indentSize As Integer)
        _indentSize = indentSize
        _stringBuilder = New StringBuilder()
        _hashListOfFoundElements = New List(Of Integer)()
    End Sub

    Public Shared Function Dump(element As Object) As String
        Return Dump(element, 2)
    End Function

    Public Shared Function Dump(element As Object, indentSize As Integer) As String
        Dim instance = New ObjectDumper(indentSize)
        Return instance.DumpElement(element)
    End Function

    Private Function DumpElement(element As Object) As String
        If element Is Nothing OrElse TypeOf element Is ValueType OrElse TypeOf element Is String Then
            Write(FormatValue(element))
        Else
            Dim objectType = element.[GetType]()
            If Not GetType(IEnumerable).IsAssignableFrom(objectType) Then
                Write("{{{0}}}", objectType.FullName)
                _hashListOfFoundElements.Add(element.GetHashCode())
                _level += 1
            End If

            Dim enumerableElement = TryCast(element, IEnumerable)
            If enumerableElement IsNot Nothing Then
                For Each item As Object In enumerableElement
                    If TypeOf item Is IEnumerable AndAlso Not (TypeOf item Is String) Then
                        _level += 1
                        DumpElement(item)
                        _level -= 1
                    Else
                        If Not AlreadyTouched(item) Then
                            DumpElement(item)
                        Else
                            Write("{{{0}}} <-- bidirectional reference found", item.[GetType]().FullName)
                        End If
                    End If
                Next
            Else
                Dim members As MemberInfo() = element.[GetType]().GetMembers(BindingFlags.[Public] Or BindingFlags.Instance)
                For Each memberInfo As Object In members
                    Dim fieldInfo = TryCast(memberInfo, FieldInfo)
                    Dim propertyInfo = TryCast(memberInfo, PropertyInfo)

                    If fieldInfo Is Nothing AndAlso propertyInfo Is Nothing Then
                        Continue For
                    End If

                    Dim type = If(fieldInfo IsNot Nothing, fieldInfo.FieldType, propertyInfo.PropertyType)
                    Try
                        Dim value As Object = If(fieldInfo IsNot Nothing, fieldInfo.GetValue(element), propertyInfo.GetValue(element, Nothing))

                    If type.IsValueType OrElse type Is GetType(String) Then
                        Write("{0}: {1}", memberInfo.Name, FormatValue(value))
                    Else
                        Dim isEnumerable = GetType(IEnumerable).IsAssignableFrom(type)
                        Write("{0}: {1}", memberInfo.Name, If(isEnumerable, "...", "{ }"))

                        Dim alreadyTouched__1 = Not isEnumerable AndAlso AlreadyTouched(value)
                        _level += 1
                        If Not alreadyTouched__1 Then
                            DumpElement(value)
                        Else
                            Write("{{{0}}} <-- bidirectional reference found", value.[GetType]().FullName)
                        End If
                        _level -= 1
                        End If
                    Catch ex As Exception

                    End Try
                Next
            End If

            If Not GetType(IEnumerable).IsAssignableFrom(objectType) Then
                _level -= 1
            End If
        End If

        Return _stringBuilder.ToString()
    End Function

    Private Function AlreadyTouched(value As Object) As Boolean
        Dim hash = value.GetHashCode()
        For i As Integer = 0 To _hashListOfFoundElements.Count - 1
            If _hashListOfFoundElements(i) = hash Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Write(value As String, ParamArray args As Object())
        Dim space = New String(" "c, _level * _indentSize)

        If args IsNot Nothing Then
            value = String.Format(value, args)
        End If

        _stringBuilder.AppendLine(space & value)
    End Sub

    Private Function FormatValue(o As Object) As String
        If o Is Nothing Then
            Return ("null")
        End If

        If TypeOf o Is DateTime Then
            Return (CType(o, DateTime).ToShortDateString())
        End If

        If TypeOf o Is String Then
            Return String.Format("""{0}""", o)
        End If

        If TypeOf o Is ValueType Then
            Return (o.ToString())
        End If

        If TypeOf o Is IEnumerable Then
            Return ("...")
        End If

        Return ("{ }")
    End Function
End Class