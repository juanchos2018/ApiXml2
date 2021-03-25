Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description
Imports System.ServiceModel.Dispatcher
Imports System.Xml
Imports Microsoft.Web.Services3.Security.Tokens
Imports System.ServiceModel.Dispatcher.ClientRuntime

Public Class PasswordDigestMessageInspector
    Implements IClientMessageInspector

    Private _Username As String
    Private _Password As String



    Public Property Username As String
        Get
            Return _Username
        End Get
        Set(value As String)
            _Username = value
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

    Public Sub New(user As String, psw As String)
        Username = user
        Password = psw
    End Sub
    Public Sub New()
        'Username = Username
        'Password = Password
    End Sub


#Region "IClientMessageInspector Members"

    Public Sub AfterReceiveReply(ByRef reply As Message, correlationState As Object) Implements IClientMessageInspector.AfterReceiveReply
        Return
    End Sub

    Public Function BeforeSendRequest(ByRef request As Message, channel As System.ServiceModel.IClientChannel) As Object Implements IClientMessageInspector.BeforeSendRequest
        Dim token As New UsernameToken(Username, Password, PasswordOption.SendPlainText)

        Dim securityToken As XmlElement = token.GetXml(New XmlDocument())

        ' Modificamos el XML Generado.
        Dim nodo = securityToken.GetElementsByTagName("wsse:Nonce").Item(0)
        nodo.RemoveAll()
        ' Not Implemented
        ' nodo?.RemoveAll();

        'Dim securityHeader As MessageHeader = MessageHeader.CreateHeader("Security", EspacioNombres.wssecurity, securityToken, False)
        Dim securityHeader As MessageHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", securityToken, False)
        request.Headers.Add(securityHeader)

        Return Convert.DBNull
    End Function

#End Region

End Class