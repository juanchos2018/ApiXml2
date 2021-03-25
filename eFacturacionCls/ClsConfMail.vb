Public Class ClsConfMail


    Public Function EnviarCorreoDominio(ByVal MasteMail As String, MasterMailAlias As String, ByVal MasterPws As String, ByVal Smtp As String, Port As String, SSl As Boolean, asunto As String, CuerpoMail As String, ByVal mailCliente As String, isCC As Boolean, CCmail As String) As String
        Dim _Message As New System.Net.Mail.MailMessage()
        Dim _SMTP As New System.Net.Mail.SmtpClient
        Dim rpt As String = ""
        'Dim archivo As New System.Net.Mail.Attachment(ruta & "\" & PdfName & ".pdf")
        'CONFIGURACIÓN DEL STMP
        _SMTP.Credentials = New System.Net.NetworkCredential(MasteMail, MasterPws) ' correo que enviar el email
        _SMTP.Host = Smtp
        _SMTP.Port = Port
        _SMTP.EnableSsl = SSl
        '   _SMTP.UseDefaultCredentials = True


        ' CONFIGURACION DEL MENSAJE
        _Message.[To].Add(mailCliente)
        _Message.From = New System.Net.Mail.MailAddress(MasteMail, MasterMailAlias, System.Text.Encoding.UTF8) 'Quien lo envía
        _Message.Subject = asunto  ' & " - " & PdfName
        _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
        _Message.Body = CuerpoMail             'Me.txtMensaje.Text.ToString 'contenido del mail
        _Message.BodyEncoding = System.Text.Encoding.UTF8
        _Message.Priority = System.Net.Mail.MailPriority.Normal
        If isCC = True Then
            _Message.CC.Add(CCmail)
        End If


        _Message.IsBodyHtml = False
        '  _Message.Attachments.Add(archivo)
        'ENVIO
        Try
            _SMTP.Send(_Message)
            rpt = "OK"
            'MessageBox.Show("Mensaje enviado correctamene", "Exito!", MessageBoxButtons.OK)
        Catch ex As System.Net.Mail.SmtpException
            'MessageBox.Show("Error!", MessageBoxButtons.OK)
            rpt = ex.ToString
        End Try
        Return rpt
    End Function
End Class
