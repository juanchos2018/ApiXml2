Imports CapaNegocios
Public Class Cls_Hosting

    Public Sub EnviarArchivoFTP(ByVal RutaFile As String, ByVal FtpURL As String, ByVal NombreDestino As String, mailftp As String, pwsftp As String)
        Dim clsRequest As System.Net.FtpWebRequest
        'Dim conexion As Net.Sockets.TcpClient
        clsRequest = DirectCast(System.Net.WebRequest.Create(FtpURL & "/" & NombreDestino), Net.FtpWebRequest)
        clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
        'clsRequest.Credentials = New System.Net.NetworkCredential("jcalderon@aveoperu.com", "20449266448") ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        clsRequest.Credentials = New System.Net.NetworkCredential(mailftp, pwsftp) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        'clsRequest.Credentials = New System.Net.NetworkCredential("usuario@aveoperu.com", "20449266448@@") ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        clsRequest.UsePassive = False

        Try
            Dim bFile() As Byte = System.IO.File.ReadAllBytes(RutaFile)
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message + ". El Archivo no pudo ser enviado, intente en otro momento")
        End Try
    End Sub

    Public Sub EnviarArchivarFtp_Zip(ByVal FtpURL As String, ByVal NombreDestino As String, mailftp As String, pwsftp As String, file As Byte())
        Dim clsRequest As System.Net.FtpWebRequest
        clsRequest = DirectCast(System.Net.WebRequest.Create(FtpURL & "/" & NombreDestino), Net.FtpWebRequest)
        clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
        clsRequest.Credentials = New System.Net.NetworkCredential(mailftp, pwsftp) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        clsRequest.UsePassive = False

        Try
            Dim bFile() As Byte = file
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message + ". El Archivo no pudo ser enviado, intente en otro momento")
        End Try
    End Sub
    Public Sub EnviarArchivoFTP_PDF(ByVal FtpURL As String, ByVal NombreDestino As String, mailftp As String, pwsftp As String, file As Byte())
        Dim clsRequest As System.Net.FtpWebRequest
        clsRequest = DirectCast(System.Net.WebRequest.Create(FtpURL & "/" & NombreDestino), Net.FtpWebRequest)
        clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
        clsRequest.Credentials = New System.Net.NetworkCredential(mailftp, pwsftp) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        clsRequest.UsePassive = False
        Try
            Dim bFile() As Byte = file
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message + ". El Archivo no pudo ser enviado, intente en otro momento")
        End Try
    End Sub
    Public Function EnviarArchivoFTP_CDR(ByVal FtpURL As String, ByVal NombreDestino As String, mailftp As String, pwsftp As String, file As Byte()) As String
        Dim clsRequest As System.Net.FtpWebRequest
        Dim estadosunat As String = "2"
        clsRequest = DirectCast(System.Net.WebRequest.Create(FtpURL & "/" & NombreDestino), Net.FtpWebRequest)
        clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
        clsRequest.Credentials = New System.Net.NetworkCredential(mailftp, pwsftp) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        'clsRequest.UsePassive = False
        clsRequest.Method = "STOR"
        clsRequest.UsePassive = True
        Try
            Dim bFile() As Byte = file
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()
            estadosunat = "3"
        Catch ex As Exception
            MsgBox(ex.Message + " " & vbNewLine & NombreDestino & vbNewLine & ". El Archivo no pudo ser enviado, intente en otro momento")
            estadosunat = "2"
        End Try
        Return estadosunat
    End Function

End Class
