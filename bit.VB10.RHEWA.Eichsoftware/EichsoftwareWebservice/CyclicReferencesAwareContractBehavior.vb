Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher

Public Class CyclicReferencesAwareContractBehavior
    Implements IContractBehavior
    Private Const maxItemsInObjectGraph As Int32 = &HFFFF
    Private Const ignoreExtensionDataObject As Boolean = False

    Private _on As Boolean

    Public Sub New([on] As Boolean)
        _on = [on]
    End Sub

#Region "IContractBehavior Members"

    Public Sub AddBindingParameters(contractDescription As ContractDescription, endpoint As ServiceEndpoint, bindingParameters As BindingParameterCollection) Implements IContractBehavior.AddBindingParameters
    End Sub

    Public Sub ApplyClientBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, clientRuntime As ClientRuntime) Implements IContractBehavior.ApplyClientBehavior
        ReplaceDataContractSerializerOperationBehaviors(contractDescription, _on)
    End Sub

    Public Sub ApplyDispatchBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, dispatchRuntime As DispatchRuntime) Implements IContractBehavior.ApplyDispatchBehavior
        ReplaceDataContractSerializerOperationBehaviors(contractDescription, _on)
    End Sub

    Friend Shared Sub ReplaceDataContractSerializerOperationBehaviors(contractDescription As ContractDescription, [on] As Boolean)
        For Each operation As Object In contractDescription.Operations
            ReplaceDataContractSerializerOperationBehavior(operation, [on])
        Next
    End Sub

    Friend Shared Sub ReplaceDataContractSerializerOperationBehavior(operation As OperationDescription, [on] As Boolean)
        If operation.Behaviors.Remove(GetType(DataContractSerializerOperationBehavior)) OrElse operation.Behaviors.Remove(GetType(ApplyCyclicDataContractSerializerOperationBehavior)) Then
            operation.Behaviors.Add(New ApplyCyclicDataContractSerializerOperationBehavior(operation, maxItemsInObjectGraph, ignoreExtensionDataObject, [on]))
        End If
    End Sub

    Public Sub Validate(contractDescription As ContractDescription, endpoint As ServiceEndpoint) Implements IContractBehavior.Validate
    End Sub

#End Region
End Class
