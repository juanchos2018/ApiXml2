Imports System.IO
Imports System.Net
Imports Ionic.Zip
Public Class ClsSunat_Valida
    Dim wService As GetSunat_Valida.billValidServiceClient
    Sub New()
        wService = New GetSunat_Valida.billValidServiceClient
        ServicePointManager.UseNagleAlgorithm = True
        ServicePointManager.Expect100Continue = False
        ServicePointManager.CheckCertificateRevocationList = True
    End Sub
    Sub New(ByVal endpointurl As String, Ruc As String, UserName As String, pws As String)
        '********* bloque
        ServicePointManager.UseNagleAlgorithm = True
        ServicePointManager.CheckCertificateRevocationList = True
        'ServicePointManager.Expect100Continue = False
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        If endpointurl <> "" Then
            Dim behavior = New PasswordDigestBehavior(Ruc + UserName, pws)
            wService = New GetSunat_Valida.billValidServiceClient("BillValidServicePort", endpointurl)
            wService.Endpoint.Behaviors.Add(behavior)
        End If
    End Sub
    Public Sub openWs()
        wService.Open()
    End Sub
    Public Sub CerrarWS()
        wService.Close()
    End Sub
    Public Function Valida_Cpe(rucEmisor As String, Td As String, Serie As String, NumeroDocumento As String, TipoDocReceptor As String, numeroDocIdReceptor As String, fechaEmision As String, importeTotal As Double, Optional esventa As Boolean = False) As String()
        Dim retorno As String()
        Try
            Dim returnstring As GetSunat_Valida.statusResponse
            If esventa = True Then
                returnstring = wService.validaCDPcriterios(rucEmisor, Td, Serie, NumeroDocumento, "-", Nothing, fechaEmision, importeTotal, "")
            Else
                If TipoDocReceptor = "-" Then
                    returnstring = wService.validaCDPcriterios(rucEmisor, Td, Serie, NumeroDocumento, "-", Nothing, fechaEmision, importeTotal, "")
                Else
                    returnstring = wService.validaCDPcriterios(rucEmisor, Td, Serie, NumeroDocumento, TipoDocReceptor, numeroDocIdReceptor, fechaEmision, importeTotal, "")
                End If
            End If
            retorno = {returnstring.statusCode, returnstring.statusMessage}
        Catch ex As System.ServiceModel.FaultException
            retorno = {ex.Code.Name, "Error"}
        End Try
        Return retorno
    End Function

End Class
