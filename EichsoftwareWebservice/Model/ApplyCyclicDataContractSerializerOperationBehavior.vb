Imports System.ServiceModel.Description
Imports System.Xml
'Quelle http://chabster.blogspot.de/2008/02/wcf-cyclic-references-support.html
'WCF DataContract serializer isn't by default aware of cyclic object graphs. If you encounter the Object graph for type 'X.Y.Z' contains cycles and cannot be serialized if reference tracking is disabled error - read to the end.

Friend Class ApplyCyclicDataContractSerializerOperationBehavior

    Inherits DataContractSerializerOperationBehavior
    Private ReadOnly _maxItemsInObjectGraph As Int32
    Private ReadOnly _ignoreExtensionDataObject As Boolean
    Private ReadOnly _preserveObjectReferences As Boolean

    Public Sub New(operationDescription As OperationDescription, maxItemsInObjectGraph As Int32, ignoreExtensionDataObject As Boolean, preserveObjectReferences As Boolean)
        MyBase.New(operationDescription)
        _maxItemsInObjectGraph = maxItemsInObjectGraph
        _ignoreExtensionDataObject = ignoreExtensionDataObject
        _preserveObjectReferences = preserveObjectReferences
    End Sub

    Public Overrides Function CreateSerializer(type As Type, name As string, ns As string, knownTypes As IList(Of Type)) As XmlObjectSerializer
        'dataContractSurrogate
        Return (New DataContractSerializer(type, name, ns, knownTypes, _maxItemsInObjectGraph, _ignoreExtensionDataObject, _
            _preserveObjectReferences, Nothing))
    End Function

    Public Overrides Function CreateSerializer(type As Type, name As XmlDictionaryString, ns As XmlDictionaryString, knownTypes As IList(Of Type)) As XmlObjectSerializer
        'dataContractSurrogate
        Return (New DataContractSerializer(type, name, ns, knownTypes, _maxItemsInObjectGraph, _ignoreExtensionDataObject, _
            _preserveObjectReferences, Nothing))
    End Function

End Class
