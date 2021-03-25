Imports System.IO
Imports System.Net
Imports System.Text
Imports Ionic.Zip
Public Class ClsSendSunat
    Dim wService As GetSunat.billServiceClient
    Sub New()
        wService = New GetSunat.billServiceClient
        ServicePointManager.UseNagleAlgorithm = True
        ServicePointManager.Expect100Continue = False
        ServicePointManager.CheckCertificateRevocationList = True
    End Sub
    Sub New(ByVal endpointurl As String, Ruc As String, UserName As String, pws As String)
        '********* bloque
        ServicePointManager.UseNagleAlgorithm = True
        ServicePointManager.Expect100Continue = False
        ServicePointManager.CheckCertificateRevocationList = True
        If endpointurl <> "" Then
            Dim behavior = New PasswordDigestBehavior(Ruc + UserName, pws)
            wService = New GetSunat.billServiceClient("BillServicePort", endpointurl)
            wService.Endpoint.Behaviors.Add(behavior)
        End If
    End Sub
    Public Sub openWs()
        wService.Open()
    End Sub
    Public Sub CerrarWS()
        wService.Close()
    End Sub
    Private Function ExtrarToByte(a As Byte()) As Byte()
        Dim ms As New MemoryStream(a)
        Dim msxml As New MemoryStream()
        Using zip As ZipFile = ZipFile.Read(ms)
            Dim e As ZipEntry
            For Each e In zip
                e.Extract(msxml)
            Next
        End Using
        Return msxml.ToArray
    End Function
    Public Function EnviarDocumentoBynary(archivo As Byte(), ByVal FileNameXml As String) As Byte()
        Dim returnbyte As Byte() = Nothing
        Try
            wService.Open()
            returnbyte = wService.sendBill(FileNameXml & ".zip", archivo, Nothing)
            wService.Close()
        Catch ex As Exception

        End Try
        Return returnbyte
    End Function
    Public Function EnviarDocumento(archivo As Byte(), ByVal FileNameXml As String) As Byte()
        Dim returnbyte As Byte() = Nothing
        Try
            returnbyte = wService.sendBill(FileNameXml & ".zip", archivo)
        Catch ex As Exception
            returnbyte = Encoding.ASCII.GetBytes(ex.Message)
        End Try
        Return returnbyte
    End Function

    Public Function ObtenerEstado(Ruc As String, Td As String, Serie As String, NumeroDocumento As String) As String()
        Dim retorno As String()
        Try
            'wService.Open()
            Dim returnstring As GetSunat.statusResponse = wService.getStatus(Ruc, Td, Serie, Val(NumeroDocumento))
            retorno = {returnstring.statusCode, returnstring.statusMessage}
            'wService.Close()
        Catch ex As System.ServiceModel.FaultException
            retorno = {ex.Code.Name, "Error"}
        End Try
        Return retorno
    End Function

    'Public Function Obtenercdr(Ruc As String, Td As String, Serie As String, NumeroDocumento As String) As Byte()
    '    Dim returnbyte As Byte() = Nothing
    '    Try
    '        'wService.Open()
    '        Dim returnstring As GetSunat.statusResponse = wService.getStatusCdr(Ruc, Td, Serie, Val(NumeroDocumento))
    '        returnbyte = returnstring.content
    '        'wService.Close()
    '    Catch ex As System.ServiceModel.FaultException
    '        returnbyte = Nothing
    '    End Try
    '    Return returnbyte
    'End Function


End Class