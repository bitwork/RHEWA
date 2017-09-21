Imports System.ServiceModel.Description
'Quelle http://chabster.blogspot.de/2008/02/wcf-cyclic-references-support.html
'WCF DataContract serializer isn't by default aware of cyclic object graphs. If you encounter the Object graph for type 'X.Y.Z' contains cycles and cannot be serialized if reference tracking is disabled error - read to the end.

<AttributeUsage(AttributeTargets.[Interface] Or AttributeTargets.Method)>
Public Class CyclicReferencesAwareAttribute

    Inherits Attribute
    Implements IContractBehavior
    Implements IOperationBehavior
    Private ReadOnly _on As Boolean = True

    Public Sub New([on] As Boolean)
        _on = [on]
    End Sub

    Public ReadOnly Property [On]() As Boolean
        Get
            Return (_on)
        End Get
    End Property

#Region "IOperationBehavior Members"

    Private Sub IOperationBehavior_AddBindingParameters(operationDescription As OperationDescription, bindingParameters As System.ServiceModel.Channels.BindingParameterCollection) Implements IOperationBehavior.AddBindingParameters
    End Sub

    Private Sub IOperationBehavior_ApplyClientBehavior(operationDescription As OperationDescription, clientOperation As System.ServiceModel.Dispatcher.ClientOperation) Implements IOperationBehavior.ApplyClientBehavior
        CyclicReferencesAwareContractBehavior.ReplaceDataContractSerializerOperationBehavior(operationDescription, [On])
    End Sub

    Private Sub IOperationBehavior_ApplyDispatchBehavior(operationDescription As OperationDescription, dispatchOperation As System.ServiceModel.Dispatcher.DispatchOperation) Implements IOperationBehavior.ApplyDispatchBehavior
        CyclicReferencesAwareContractBehavior.ReplaceDataContractSerializerOperationBehavior(operationDescription, [On])
    End Sub

    Private Sub IOperationBehavior_Validate(operationDescription As OperationDescription) Implements IOperationBehavior.Validate
    End Sub

#End Region

#Region "IContractBehavior Members"

    Private Sub IContractBehavior_AddBindingParameters(contractDescription As ContractDescription, endpoint As ServiceEndpoint, bindingParameters As System.ServiceModel.Channels.BindingParameterCollection) Implements IContractBehavior.AddBindingParameters
    End Sub

    Private Sub IContractBehavior_ApplyClientBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, clientRuntime As System.ServiceModel.Dispatcher.ClientRuntime) Implements IContractBehavior.ApplyClientBehavior
        CyclicReferencesAwareContractBehavior.ReplaceDataContractSerializerOperationBehaviors(contractDescription, [On])
    End Sub

    Private Sub IContractBehavior_ApplyDispatchBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, dispatchRuntime As System.ServiceModel.Dispatcher.DispatchRuntime) Implements IContractBehavior.ApplyDispatchBehavior
        CyclicReferencesAwareContractBehavior.ReplaceDataContractSerializerOperationBehaviors(contractDescription, [On])
    End Sub

    Private Sub IContractBehavior_Validate(contractDescription As ContractDescription, endpoint As ServiceEndpoint) Implements IContractBehavior.Validate
    End Sub

#End Region
End Class