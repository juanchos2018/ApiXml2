Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description
Imports System.ServiceModel.Dispatcher
Imports System.Xml
Imports Microsoft.Web.Services3.Security.Tokens
Imports System.ServiceModel.Dispatcher.ClientRuntime
Public Class PasswordDigestBehavior
    Implements IEndpointBehavior

    Private _Usuario As String
    Private _Password As String

    Public Property Usuario As String
        Get
            Return _Usuario
        End Get
        Set(value As String)
            _Usuario = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(value As String)
            _Password = value
        End Set
    End Property
    Public Sub New(username As String, pws As String)
        Usuario = username
        Password = pws
    End Sub
    Public Sub AddBindingParameters(endpoint As ServiceEndpoint, bindingParameters As BindingParameterCollection) Implements IEndpointBehavior.AddBindingParameters
        Return
    End Sub
    Public Sub ApplyClientBehavior(endpoint As ServiceEndpoint, clientRuntime As ClientRuntime) Implements IEndpointBehavior.ApplyClientBehavior
        clientRuntime.MessageInspectors.Add(New PasswordDigestMessageInspector(Usuario, Password))
        'Dim inspercto As New PasswordDigestMessageInspector()
        'inspercto.Username = usuario
        'inspercto.Password = password
        'clientRuntime.MessageInspectors.Add(inspercto)

    End Sub
    Public Sub ApplyDispatchBehavior(endpoint As ServiceEndpoint, endpointDispatcher As EndpointDispatcher) Implements IEndpointBehavior.ApplyDispatchBehavior
        Return
    End Sub
    Public Sub Validate(endopoint As ServiceEndpoint) Implements IEndpointBehavior.Validate
        Return
    End Sub

End Class