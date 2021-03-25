Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Xml
Imports Ionic.Zip
Imports System.IO
Imports AxAcroPDFLib
Imports System.ComponentModel
Imports CapaNegocios
Imports QRCoder
Imports System.Windows.Forms
Imports System.Drawing
Imports efacturacionClsNuevo
Imports CapaFtp

Public Class ClsGenerarXmlApi
    Dim EFObj As New eFacturacionCls.Cls_FObjeto
    Dim BE As New eFacturacionCls.Cls_Boleta
    Dim NC As New eFacturacionCls.Cls_NotaCredito
    Dim Nd As New eFacturacionCls.Cls_NotaDebito


    Dim ef As New efacturacionClsNuevo.Cls_FacturaXml
    Dim eb As New efacturacionClsNuevo.Cls_BoletaXML
    Dim enc As New efacturacionClsNuevo.Cls_NotaCreditoXML
    Dim ent As New efacturacionClsNuevo.Cls_NotaDebitoXML


    Dim Mail As New eFacturacionCls.ClsConfMail
    Dim ReadCDR As New ReadCdrXml
    Dim cls_firma As New eFacturacionCls.ClsFirma
    Dim lo_estilo As New CapaEstilo.ClsEstilo
    'Dim lo_imprimir As New eFacturacionCls.ClsImprimirInvoice
    Dim lo_imprimir As New efacturacionClsNuevo.ClsImprimirInvoice
    Dim lo_sendft As New eFacturacionCls.Cls_Hosting
    Dim FileNamexml As String = ""
    Dim lo_view As DataView
    'Dim ToPrintRdl As New ModImprimirRdl
    'Dim print As New ClsImpresion
    Dim Zip As New ClsZIP
    Dim cab As New NComprobante
    Dim det As New NDetalleComprobante
    Public Property setImpresora As String
    Private Function validar1(dg As DataTable) As String()
        Dim mensaje(2) As String
        mensaje(0) = "Los datos son conformes"
        mensaje(1) = "0"
        With dg
            If .Rows(0).Item("TdSunat").ToString.Trim = "" Then
                mensaje(0) = "El Tipo de Documento no es valido, favor de configurar en tablas generales"
                mensaje(1) = "1"
                Return mensaje
                Exit Function
            End If
            If .Rows(0).Item("TipoDocSunat").ToString.Trim = "" Then
                mensaje(0) = "El Tipo de Documento de identidad del cliente no es valido, favor de configurar en tablas generales"
                mensaje(1) = "1"
                Return mensaje
                Exit Function
            End If
            If .Rows(0).Item("TdSunat").ToString.Trim = "01" And Len(.Rows(0).Item("Ruc").ToString.Trim) <> 11 Then
                mensaje(0) = "No se puede emitir una factura para personas que no tengan Ruc"
                mensaje(1) = "1"
                Return mensaje
                Exit Function
            End If
            If .Rows(0).Item("TdSunat").ToString.Trim = "03" And Len(.Rows(0).Item("Ruc").ToString.Trim) = 11 Then
                mensaje(0) = "No se puede Girar una boleta de Venta para un ruc"
                mensaje(1) = "1"
                Return mensaje
                Exit Function
            End If
            'If .Rows(0).Item("EstadoSunat").ToString.Trim = "2" Then
            '    mensaje(0) = "No se puede volver a generar el documento ya se envió a Sunat"
            '    mensaje(1) = "1"
            '    Return mensaje
            '    Exit Function
            'End If
        End With
        Return mensaje
    End Function

    Dim iconSize As Integer = 0 ' se crea para el tamaño del qr
    Dim iconPath As New TextBox 'se crear para la ruta de carga de un archivo
    Private Function GetIconBitmap() As Bitmap
        iconPath.Text = ""
        Dim img As Bitmap = Nothing
        If iconPath.Text.Length > 0 Then
            Try
                img = New Bitmap(iconPath.Text)
            Catch generatedExceptionName As Exception
            End Try
        End If
        Return img
    End Function
    Private Sub deletefile(ByVal ruta As String)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(ruta, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
            My.Computer.FileSystem.DeleteFile(foundFile)
        Next
    End Sub
    Private Function QRbarra(cadena_barra As String) As Image
        Dim level As String = "Q" 'comboBoxECC.SelectedItem.ToString()
        Dim eccLevel As QRCodeGenerator.ECCLevel = CType(If(level = "L", 0, If(level = "M", 1, If(level = "Q", 2, 3))), QRCodeGenerator.ECCLevel)
        Using qrGenerator As New QRCodeGenerator()
            Using qrCodeData As QRCodeData = qrGenerator.CreateQrCode(cadena_barra, eccLevel)
                Using qrCode As New QRCode(qrCodeData)
                    Return qrCode.GetGraphic(20, Color.Black, Color.White, GetIconBitmap(), CType(iconSize, Integer))
                End Using
            End Using
        End Using
    End Function
    ''' <summary>
    ''' Genera y enviar el comprobante electrónico al servidor
    ''' </summary>
    ''' <param name="Td"></param>
    ''' <param name="serie"></param>
    ''' <param name="numero"></param>
    ''' <param name="idcliente"></param>
    ''' <param name="iscpe"></param>
    Public Sub Generarxml_UrlAPi(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional iscpe As Boolean = False, Optional urlSet As String = "")
        Dim Xml_zipBinary As Byte() = Nothing, PDF_Binary As Byte() = Nothing
        Dim entidad As New NPtentidad
        entidad.identidad = "001"
        entidad = entidad.Registro(entidad)
        Dim dt_en As DataTable = entidad.itemTbl(entidad)
        If entidad.signalias.Trim <> "" Then
            eFacturacionCls.ModAlias.Asignar_Alias(entidad.signalias)
        Else
            MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
            Exit Sub
        End If
        Dim detentidad As New NDet_Entidad
        Dim Ruc As String
        Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
        Dim rutapfx As String = Nothing
        Dim rutacer As String = Nothing
        Dim pws As String = Nothing
        rutapfx = entidad.rutapfx
        rutacer = entidad.rutacer
        pws = lo_estilo.Desencriptar(entidad.pws)
        If rutacer = "" Then
            MessageBox.Show("No existe certificado, no procede la generación del XML")
            Exit Sub

        End If
        If rutapfx = "" Then
            MessageBox.Show("No existe certificado, no procede la generación del XML")
            Exit Sub
        End If
        If pws = "" Then
            MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
            Exit Sub
        End If


        Dim Cabecera As New DataTable
        Dim detalle As New DataTable
        cab.idtipodocumento = Td
        cab.serie = serie
        cab.numerodocumento = numero
        If iscpe = True Then
            Cabecera = cab.cabeceraCPE21cpe(cab)
        Else
            Cabecera = cab.cabeceraCPE21(cab)
        End If
        If validar1(Cabecera)(1) = "1" Then
            MessageBox.Show(validar1(Cabecera)(0))
            Exit Sub
        End If
        det.idtipodocumento = Td
        det.serie = serie
        det.numerodocumento = numero
        If iscpe = True Then
            detalle = det.DetalleCPE21CPE(det)
        Else
            detalle = det.DetalleCPE21(det)
        End If


        If entidad.identidad = "001" Then
            Dim efact As Byte() = Nothing
            Dim efact_firma As Byte() = Nothing
            Ruc = entidad.ruc

            Version = "2.1" : OCustomId = "2.0"
            deletefile(Application.StartupPath & "\tempxml")
            OTd = Cabecera.Rows(0).Item("TdSunat")
            OComprobante = serie & "-" & numero
            OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
            FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
            If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
                Moneda = "PEN"
            Else
                Moneda = "USD"
            End If
            If OTd = "01" Then
                If iscpe = True Then
                    'efcpe.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                    'efact = efcpe.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
                Else
                    ef.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                    efact = ef.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
                End If
            End If
            If OTd = "03" Then
                If iscpe = True Then
                    'ebcpe.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                    'efact = ebcpe.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
                Else
                    eb.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                    efact = eb.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
                End If
            End If
            Dim has As String = Nothing
            Dim signature As String = Nothing
            Try
                detentidad.IdEntidad = "001"
                detentidad = detentidad.Item(detentidad)
                Dim Key As X509Certificate2
                If detentidad.pfx_file IsNot Nothing Then
                    Key = New X509Certificate2(detentidad.pfx_file, pws)
                Else
                    Key = New X509Certificate2(rutapfx, pws)
                End If
                Dim Key1 As X509Certificate2
                If detentidad.cer_file IsNot Nothing Then
                    Key1 = New X509Certificate2(detentidad.cer_file, pws)
                Else
                    Key1 = New X509Certificate2(rutacer, pws)
                End If
                cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
                has = cls_firma.ReturCodHas()
                signature = cls_firma.Retursignaturevalue()
                'go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")
                Dim co As New NComprobante
                co.idagencia = Agencia
                co.idtipodocumento = Td
                co.serie = serie
                co.numerodocumento = numero
                co.idalmacen = Almacen

                Dim bm As Bitmap = Nothing
                Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
                bm = QRbarra(cadenaBarra)

                co = co.Registro(co)
                co.estadosunat = "1"
                co.signaturevalue = signature
                co.codigohas = has
                co.barrapdf417 = lo_estilo.Image2Bytes(bm)
                co.Actualizar(co)

                Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
                If result = False Then
                    MsgBox("La firma es adulterado")
                End If
                Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")
            Catch ex As CryptographicException
                MsgBox(ex.Message)
            End Try
        End If

        Dim valores() As String
        Dim campos() As String
        campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "TipoDocumento", "url"}
        If serie.Trim.Substring(0, 1) = "B" Then
            valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat1"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_en.Rows(0).Item("url")}
        Else
            valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_en.Rows(0).Item("url")}
        End If
        Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
        cab.idagencia = Agencia
        cab.idalmacen = Almacen
        cab.idtipodocumento = Td
        cab.serie = serie
        cab.numerodocumento = numero

        Dim dt As New DataTable '= cab.cabeceraPDF(cab)

        'dt = cab.cabeceraPDFSerie(cab)
        Dim tg As New NTablaGeneral
        tg.IdGeneral = "TCK"
        tg.IdCodigo = "OP"
        tg = tg.Registro(tg)
        If IsNothing(tg.Descripcion) = False Then
            If tg.Descripcion.Trim = "2" Then  ' informacion para celulares
                If iscpe = True Then
                    dt = cab.cabeceraPDFSerieCPE(cab)
                Else
                    dt = cab.cabeceraPDFSerie(cab)
                End If

            Else
                If iscpe = True Then
                    dt = cab.cabeceraPDFCPE(cab)
                Else
                    dt = cab.cabeceraPDF(cab)
                End If
            End If
        Else
            If iscpe = True Then
                dt = cab.cabeceraPDFCPE(cab)
            Else
                dt = cab.cabeceraPDF(cab)
            End If
        End If

        If Td = "FT" Or Td = "BV" Then
            If iscpe = True Then
                PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket_CPE.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
            Else
                PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores, True)
            End If

        End If


        Dim ruta As String = Application.StartupPath & "\XmlInvoice"
        File.WriteAllBytes(ruta & "\" & FileNamexml & ".zip", Xml_zipBinary)
        File.WriteAllBytes(ruta & "\" & FileNamexml & ".pdf", PDF_Binary)

        Dim enviar As New ClsFtp.ClsEnviarCpeHttp
        enviar.SendFilehttp(urlSet, FileNamexml, ruta, "xml|zip|pdf", True)

    End Sub

    Public Sub Generarxml_Api(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional isprint As Boolean = False, Optional print As String = "", Optional urlSet As String = "")
        setImpresora = print
        Dim Xml_zipBinary, PDF_Binary As Byte()
        Dim entidad As New NPtentidad
        entidad.identidad = "001"
        entidad = entidad.Registro(entidad)
        Dim dt_en As DataTable = entidad.itemTbl(entidad)
        If entidad.signalias.Trim <> "" Then
            eFacturacionCls.ModAlias.Asignar_Alias(entidad.signalias)
        Else
            MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
            Exit Sub
        End If


        Dim detentidad As New NDet_Entidad
        Dim Ruc As String
        Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
        Dim rutapfx As String = Nothing
        Dim rutacer As String = Nothing
        Dim pws As String = Nothing
        rutapfx = entidad.rutapfx
        rutacer = entidad.rutacer
        pws = lo_estilo.Desencriptar(entidad.pws)

        If dt_en.Rows.Count > 0 Then
            rutapfx = dt_en.Rows(0).Item("rutapfx")
            rutacer = dt_en.Rows(0).Item("rutacer")
            pws = lo_estilo.Desencriptar(dt_en.Rows(0).Item("pws"))
        End If
        If rutacer = "" Then
            MessageBox.Show("No existe certificado, no procede la generación del XML")
            Exit Sub

        End If
        If rutapfx = "" Then
            MessageBox.Show("No existe certificado, no procede la generación del XML")
            Exit Sub
        End If
        If pws = "" Then
            MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
            Exit Sub
        End If

        Dim Cabecera As New DataTable
        Dim detalle As New DataTable

        cab.idagencia = Agencia
        cab.idalmacen = Almacen
        cab.idtipodocumento = Td
        cab.serie = serie
        cab.numerodocumento = numero
        cab.idcliente = idcliente
        Cabecera = cab.cabeceraCPE21(cab)

        If validar1(Cabecera)(1) = "1" Then
            MessageBox.Show(validar1(Cabecera)(0))
            Exit Sub
        End If



        det.idagencia = Agencia
        det.idalmacen = Almacen
        det.idtipodocumento = Td
        det.serie = serie
        det.numerodocumento = numero
        detalle = det.DetalleCPE21(det)





        If dt_en.Rows.Count > 0 Then
            Dim efact As Byte() = Nothing
            Dim efact_firma As Byte() = Nothing
            With dt_en
                Ruc = .Rows(0).Item("RUC").ToString
            End With
            Version = "2.1" : OCustomId = "2.0"
            deletefile(Application.StartupPath & "\tempxml")
            OTd = Cabecera.Rows(0).Item("TdSunat")
            OComprobante = serie & "-" & numero
            OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
            FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
            If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
                Moneda = "PEN"
            Else
                Moneda = "USD"
            End If
            If OTd = "01" Then
                ef.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                efact = ef.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
            End If
            If OTd = "03" Then
                eb.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                efact = eb.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
            End If
            If OTd = "07" Then
                enc.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                efact = enc.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
            End If
            If OTd = "08" Then
                ent.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
                efact = ent.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
            End If
            Dim has As String = Nothing
            Dim Signature As String = Nothing
            Try
                detentidad.IdEntidad = "001"
                detentidad = detentidad.Item(detentidad)
                Dim Key As X509Certificate2
                If detentidad.pfx_file IsNot Nothing Then
                    Key = New X509Certificate2(detentidad.pfx_file, pws)
                Else
                    Key = New X509Certificate2(rutapfx, pws)
                End If
                Dim Key1 As X509Certificate2
                If detentidad.cer_file IsNot Nothing Then
                    Key1 = New X509Certificate2(detentidad.cer_file, pws)
                Else
                    Key1 = New X509Certificate2(rutacer, pws)
                End If
                cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
                has = cls_firma.ReturCodHas()
                Signature = cls_firma.Retursignaturevalue()

                Dim co As New NComprobante
                co.idagencia = Agencia
                co.idtipodocumento = Td
                co.serie = serie
                co.numerodocumento = numero
                co.idalmacen = Almacen

                Dim bm As Bitmap = Nothing
                Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
                bm = QRbarra(cadenaBarra)

                co = co.Registro(co)
                co.estadosunat = "1"
                co.signaturevalue = Signature
                co.codigohas = has
                co.barrapdf417 = lo_estilo.Image2Bytes(bm)
                co.Actualizar(co)

                Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
                If result = False Then
                    MsgBox("La firma es adulterado")
                End If


                Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")

            Catch ex As CryptographicException
                MsgBox(ex.Message)
            End Try

        End If
        Dim valores() As String
        Dim campos() As String
        campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "Serie", "NumeroDocumento", "TipoDocumento", "Logo", "url", "RsRet", "RsPer"}
        If serie.Trim.Substring(0, 1) = "B" Then
            valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat1"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_en.Rows(0).Item("Logo"), dt_en.Rows(0).Item("url"), "", ""}
        Else
            valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_en.Rows(0).Item("Logo"), dt_en.Rows(0).Item("url"), "", ""}
        End If
        Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
        cab.idagencia = Agencia
        cab.idalmacen = Almacen
        cab.idtipodocumento = Td
        cab.serie = serie
        cab.numerodocumento = numero
        Dim dt As DataTable = cab.cabeceraPDF(cab)
        If dt.Rows.Count > 0 Then
            Dim i As Integer = dt.Rows.Count
            Dim max As Integer = 33
            For i = dt.Rows.Count To max
                dt.Rows.Add()
            Next
        End If
        If Td = "FT" Or Td = "BV" Then
            PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
            If isprint = True Then
                lo_imprimir.setImpresora = setImpresora
                lo_imprimir.ToA4(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores, print)
            End If
        End If
        If Td = "NA" Then
            PDF_Binary = lo_imprimir.ToPdfBinario(dt, "ENotaCreditologo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
            If isprint = True Then
                lo_imprimir.setImpresora = setImpresora
                lo_imprimir.ToA4(dt, "ENotaCreditologo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores, print)
            End If
        End If

        Dim ruta As String = Application.StartupPath & "\XmlInvoice"
        File.WriteAllBytes(ruta & "\" & FileNamexml & ".zip", Xml_zipBinary)
        File.WriteAllBytes(ruta & "\" & FileNamexml & ".pdf", PDF_Binary)

        Dim enviar As New ClsFtp.ClsEnviarCpeHttp
        enviar.SendFilehttp(urlSet, FileNamexml, ruta, "xml|zip|pdf", True)


    End Sub

End Class
