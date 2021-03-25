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


Public Class ClsGenerarXmlTicket
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
    'Public Sub Generarxml(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String)
    '    Dim ca As String = "select isnull(SignAlias,'') as SingAlias from ptentidad where identidad='001'"
    '    Dim Xml_zipBinary, PDF_Binary As Byte()

    '    Dim dx As DataTable
    '    dx = go_Sql.EjecutarConsulta("d", ca).Tables(0)
    '    If dx.Rows.Count > 0 Then
    '        If dx.Rows(0).Item(0) <> "" Then
    '            eFacturacionCls.ModAlias.Asignar_Alias(dx.Rows(0).Item(0))
    '        Else
    '            MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
    '            Exit Sub
    '        End If
    '    Else
    '        eFacturacionCls.ModAlias.Asignar_Alias("SignAveo")
    '    End If
    '    Dim detentidad As New NDet_Entidad
    '    Dim Ruc As String
    '    Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
    '    Dim dt_En As New DataTable
    '    dt_En = go_Sql.EjecutarConsulta("entidad", "select Ruc,Nombre,Direccion,Pais,Departamento,Provincia,Distrito,IdTipoDocumento,CodUbigeo,isnull(rutapfx,'') as rutapfx,isnull(rutacer,'') as rutacer,isnull(pws,'') as pws,isnull(RsSunat,'') as RsSunat,isnull(NombreComercial,'-') as NombreComercial,isnull(RsSunat1,'') as RsSunat1,isnull(logo,'') as Logo,Isnull(Url,'')as Url from ptentidad").Tables(0)
    '    Dim rutapfx As String = Nothing
    '    Dim rutacer As String = Nothing
    '    Dim pws As String = Nothing
    '    If dt_En.Rows.Count > 0 Then
    '        rutapfx = dt_En.Rows(0).Item("rutapfx")
    '        rutacer = dt_En.Rows(0).Item("rutacer")
    '        pws = lo_estilo.Desencriptar(dt_En.Rows(0).Item("pws"))
    '    End If
    '    If rutacer = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")


    '        Exit Sub

    '    End If
    '    If rutapfx = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If pws = "" Then
    '        MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
    '        Exit Sub
    '    End If

    '    Dim Cabecera As New DataTable
    '    Dim detalle As New DataTable


    '    Dim cliente As String = " select c.IdAgencia,c.idalmacen,c.IdTipoDocumento,c.serie,c.numerodocumento,c.idcliente,c.nombreCliente,c.ruc,vtd.TdSunat,Cast(c.importeTotal-c.importeIgv as Decimal(18,2)) as ValorTotal,Cast(c.importeTotal as Decimal(18,2)) as ImporteTotal,Cast(c.ImporteIGV as Decimal(18,2)) as ImporteIGV,IdTipoDocumento1,Serie1,NumeroDocumento1,cl.TipoDocSunat,Vtd1.TdSunat as TdSunatRef,NumeroDocumento2,cl.departamento,pais,cl.Direccion,cl.Distrito,cl.Provincia,dbo.Fnumeroletra(ImporteTotal) as ImporteLetra,c.Descripcion as Obs,IdTipoNotaCredito,TdNC,o.descripcion as Motivo,c.IdFormaVenta,fv.Descripcion as Formaventa ,CodIGV as AfecIGV,IGV,TipoOperacion,ImporteDescuento,Fechadocumento,IdMoneda,codigoHas,signaturevalue,vtd.tipodocumento,isnull(EstadoSunat,0) as EstadoSunat,isnull(NumeroOrden,'') as NumeroOrden,isnull(IdTransportista,'') as IdTransportista from comprobante c inner join vtipodocumento vtd on c.IdTipoDocumento=vtd.idTipodocumento "
    '    cliente += " inner join cliente cl on c.IdCliente=cl.IdCliente  left join VTipoDocumento as vtd1 on c.IdTipoDocumento2=vtd1.idTipodocumento left join "
    '    cliente += " (SELECT 'NA'as Td,iDCODIGO,dESCRIPCION,TdNC FROM VTipoNotacredito  UNION ALL  SELECT  'ND'as Td,iDCODIGO,dESCRIPCION,TdNC FROM VTIPONOTADEBITO  )AS  o on   c.IdTipoDocumento=o.td and IdTipoNotaCredito=o.idcodigo  left join FormaVenta fv on c.IdFormaVenta=fv.IdFormaVenta "
    '    cliente += " left join VCatalogo_Sunat07 vigv on isnull(c.TipoAfecIGV,'G10')=vigv.IdCodigo where c.idcliente='" & idcliente & "' "
    '    cliente += " and c.IdAgencia='" & Agencia & "' and c.IdAlmacen='" & Almacen & "' and c.IdTipoDocumento='" & Td & "' and c.serie='" & serie & "' and c.numerodocumento='" & numero & "'"
    '    Cabecera = go_Sql.EjecutarConsulta("cliente", cliente).Tables(0)

    '    If validar1(Cabecera)(1) = "1" Then
    '        MessageBox.Show(validar1(Cabecera)(0))
    '        Exit Sub
    '    End If

    '    Dim SDetalle As String = " select Item, case when isnull(dbo.GetDua(IdArticulo),'')='' then IdArticulo else dbo.GetDua(IdArticulo) end as IdArticulo,d.Descripcion,Cantidad,Unidad,PrecioVenta,PrecioSIGV,d.ImporteIGV,ImporteUS,ImporteMN ,C.IGV,CodIGV as AfecIGV,IdFormaVenta,isnull(vu.UndRef,'NIU')as UndRef from detallecomprobante  d  inner join comprobante c on d.IdAlmacen=c.IdAlmacen and d.IdTipoDocumento=c.IdTipoDocumento and d.Serie=c.Serie and d.NumeroDocumento=c.NumeroDocumento "
    '    SDetalle += " left join VCatalogo_Sunat07 vigv on isnull(c.TipoAfecIGV,'G10')=vigv.IdCodigo left join VUnidMed vu on d.Unidad=vu.IdCodigo "
    '    SDetalle += " Where c.IdAgencia='" & Agencia & "' and c.IdAlmacen='" & Almacen & "' and c.IdTipoDocumento='" & Td & "' and c.serie='" & serie & "' and c.numerodocumento='" & numero & "'"
    '    detalle = go_Sql.EjecutarConsulta("ds", SDetalle).Tables(0)

    '    If dt_En.Rows.Count > 0 Then
    '        Dim efact As Byte() = Nothing
    '        With dt_En
    '            Ruc = .Rows(0).Item("RUC").ToString
    '        End With
    '        Version = "2.0" : OCustomId = "1.0"
    '        deletefile(Application.StartupPath & "\tempxml")
    '        OTd = Cabecera.Rows(0).Item("TdSunat")
    '        OComprobante = serie & "-" & numero
    '        OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
    '        FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
    '        If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
    '            Moneda = "PEN"
    '        Else
    '            Moneda = "USD"
    '        End If
    '        If OTd = "01" Then
    '            EFObj.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = EFObj.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "03" Then
    '            BE.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = BE.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "07" Then
    '            NC.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = NC.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "08" Then
    '            Nd.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = Nd.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        Dim has As String = Nothing
    '        Dim signature As String = Nothing
    '        Try

    '            detentidad.IdEntidad = "001"
    '            detentidad = detentidad.Item(detentidad)
    '            Dim Key As X509Certificate2
    '            If detentidad.pfx_file IsNot Nothing Then
    '                Key = New X509Certificate2(detentidad.pfx_file, pws)
    '            Else
    '                Key = New X509Certificate2(rutapfx, pws)
    '            End If
    '            Dim Key1 As X509Certificate2
    '            If detentidad.cer_file IsNot Nothing Then
    '                Key1 = New X509Certificate2(detentidad.cer_file, pws)
    '            Else
    '                Key1 = New X509Certificate2(rutacer, pws)
    '            End If

    '            'Dim Key As X509Certificate2 = New X509Certificate2(rutapfx, pws)
    '            'Dim Key1 As X509Certificate2 = New X509Certificate2(rutacer, pws)


    '            'cls_firma.SignXmlFile_509(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
    '            cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
    '            has = cls_firma.ReturCodHas()
    '            signature = cls_firma.Retursignaturevalue()
    '            go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")

    '            Dim bm As Bitmap = Nothing
    '            Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
    '            'bm = BarCode.PDF417(cadenaBarra, 2)
    '            bm = QRbarra(cadenaBarra)
    '            Dim valor = Agencia & Almacen & Td & serie & numero
    '            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm))
    '            Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
    '            If result = False Then
    '                MsgBox("La firma es adulterado")
    '            End If

    '            Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")

    '        Catch ex As CryptographicException
    '            MsgBox(ex.Message)
    '        End Try
    '    End If
    '    Dim valores() As String
    '    Dim campos() As String
    '    campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "TipoDocumento", "url"}
    '    If serie.Trim.Substring(0, 1) = "B" Then
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat1"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_En.Rows(0).Item("url")}
    '    Else
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_En.Rows(0).Item("url")}
    '    End If

    '    Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
    '    Dim cadena As String = " select dt.IdAgencia, dt.IdTipoDocumento, dt.Serie, dt.NumeroDocumento, dt.Item, case when isnull(dbo.GetDua(IdArticulo),'')='' then dt.IdArticulo else dbo.GetDua(dt.IdArticulo) end as IdArticulo, dt.Descripcion, dt.Cantidad, dt.Unidad, dt.SaldoEntrega, dt.PrecioVenta,"
    '    cadena += " dt.PrecioSIGV, dt.ImporteDescuento, dt.IGV, dt.ImporteIGV, dt.ImporteUS, dt.ImporteMN, dt.Estado, dt.IdAlmacen, dt.Stock, dt.IdLista, dt.LoteSerie, "
    '    cadena += " c.Codigohas, c.signatureValue,c.barrapdf417,ISNULL(c.RUC,IdCliente) As Ruc, c.NombreCliente,c.Direccion,FechaDocumento,c.idmoneda,ImporteTotal ,dbo.FNumeroLetra(c.ImporteTotal)As ImporteLetras,EstadoSunat  "
    '    cadena += " ,c.idformaventa,isnull(TipoOperacion,'GRAV') AS TipoOperacion,FechaVencimineto,Nombre as NombreVendedor,f.Descripcion as FormaVenta,vt.tipoDocumento,VNC.Descripcion as MotivoAnulacion "
    '    cadena += " ,Vtf.TdSunat as TdSunatRef,c.NumeroDocumento2,c.importeDescuento as DescuentoGlobal ,c.importeIGV as TotalIGV,(c.ImporteTotal-c.ImporteIGV)as ValorTotal,isnull(islote,'0') as islote,loteserie,IdTransportista from comprobante c inner  join detallecomprobante dt "
    '    cadena += " on c.Idagencia=dt.idagencia and c.idalmacen=dt.idalmacen and c.idtipodocumento=dt.idtipodocumento and c.serie=dt.serie and c.numerodocumento=dt.Numerodocumento left join vendedor v on "
    '    cadena += " c.IdVendedor=v.IdVendedor  left join FormaVenta f on c.IdFormaVenta=f.IdFormaVenta   left join vtipodocumento vt on c.IdTipoDocumento=vt.idTipoDocumento "
    '    cadena += " left join (select ('H'+IdCodigo) AS IdCodigo,Descripcion from VTipoNotaCredito union all select ('D'+IdCodigo) as IdCodigo,Descripcion from VTipoNotaDebito) Vnc on RTRIM(debehaber)+rtrim(c.IdTipoNotaCredito)=RTRIM(VNC.IdCodigo) "
    '    cadena += " left join VTipoDocumento Vtf on c.IdTipoDocumento2=vtf.idTipoDocumento "
    '    cadena += " Where dt.IdAgencia='" & Agencia & "' and dt.IdAlmacen='" & Almacen & "' and dt.IdTipoDocumento='" & Td & "' and dt.serie='" & serie & "' and dt.numerodocumento='" & numero & "'"
    '    Dim dt As DataTable
    '    dt = go_Sql.EjecutarConsulta("fac", cadena).Tables(0)
    '    If Td = "FT" Or Td = "BV" Then
    '        PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
    '        lo_imprimir.ToTicket(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
    '    End If

    '    Dim archivo As New NComprobante_CPE
    '    With archivo
    '        .IdAgencia = Agencia
    '        .IdAlmacen = Almacen
    '        .IdTipoDocumento = Td
    '        .Serie = serie
    '        .NumeroDocumento = numero
    '        .xml_zip = Xml_zipBinary
    '        .pdf_pdf = PDF_Binary
    '        .cdr_zip = Nothing
    '        .Eliminar(archivo)
    '        .agregar(archivo)
    '    End With

    'End Sub
    '''' <summary>
    '''' Genera el XML en formato ticket y si el valor isprint es true envia a impresion el documento
    '''' </summary>
    '''' <param name="Agencia"></param>
    '''' <param name="Almacen"></param>
    '''' <param name="Td"></param>
    '''' <param name="serie"></param>
    '''' <param name="numero"></param>
    '''' <param name="idcliente"></param>
    '''' <param name="isprint"></param>
    'Public Sub Generarxml_21(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional isprint As Boolean = False)
    '    Dim Xml_zipBinary, PDF_Binary As Byte()
    '    Dim entidad As New NPtentidad
    '    entidad.identidad = "001"
    '    entidad = entidad.Registro(entidad)
    '    Dim dt_en As DataTable = entidad.itemTbl(entidad)
    '    If entidad.signalias.Trim <> "" Then
    '        eFacturacionCls.ModAlias.Asignar_Alias(entidad.signalias)
    '    Else
    '        MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
    '        Exit Sub
    '    End If
    '    Dim detentidad As New NDet_Entidad
    '    Dim Ruc As String
    '    Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
    '    Dim rutapfx As String = Nothing
    '    Dim rutacer As String = Nothing
    '    Dim pws As String = Nothing
    '    rutapfx = entidad.rutapfx
    '    rutacer = entidad.rutacer
    '    pws = lo_estilo.Desencriptar(entidad.pws)
    '    If rutacer = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If rutapfx = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If pws = "" Then
    '        MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
    '        Exit Sub
    '    End If

    '    Dim Cabecera As New DataTable
    '    Dim detalle As New DataTable
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    Cabecera = cab.cabeceraCPE21(cab)
    '    'If validar1(Cabecera)(1) = "1" Then
    '    '    MessageBox.Show(validar1(Cabecera)(0))
    '    '    Exit Sub
    '    'End If
    '    det.idtipodocumento = Td
    '    det.serie = serie
    '    det.numerodocumento = numero
    '    detalle = det.DetalleCPE21(det)
    '    If dt_en.Rows.Count > 0 Then
    '        Dim efact As Byte() = Nothing
    '        With dt_en
    '            Ruc = .Rows(0).Item("RUC").ToString
    '        End With
    '        Version = "2.1" : OCustomId = "2.0"
    '        deletefile(Application.StartupPath & "\tempxml")
    '        OTd = Cabecera.Rows(0).Item("TdSunat")
    '        OComprobante = serie & "-" & numero
    '        OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
    '        FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
    '        If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
    '            Moneda = "PEN"
    '        Else
    '            Moneda = "USD"
    '        End If
    '        If OTd = "01" Then
    '            ef.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = ef.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "03" Then
    '            eb.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = eb.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "07" Then
    '            enc.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = enc.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "08" Then
    '            ent.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = ent.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If

    '        Dim has As String = Nothing
    '        Dim signature As String = Nothing
    '        Try

    '            detentidad.IdEntidad = "001"
    '            detentidad = detentidad.Item(detentidad)
    '            Dim Key As X509Certificate2
    '            If detentidad.pfx_file IsNot Nothing Then
    '                Key = New X509Certificate2(detentidad.pfx_file, pws)
    '            Else
    '                Key = New X509Certificate2(rutapfx, pws)
    '            End If
    '            Dim Key1 As X509Certificate2
    '            If detentidad.cer_file IsNot Nothing Then
    '                Key1 = New X509Certificate2(detentidad.cer_file, pws)
    '            Else
    '                Key1 = New X509Certificate2(rutacer, pws)
    '            End If
    '            cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)

    '            has = cls_firma.ReturCodHas()
    '            signature = cls_firma.Retursignaturevalue()
    '            go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")

    '            Dim bm As Bitmap = Nothing
    '            Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
    '            'Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|" & has & "|" & signature & "|"
    '            'bm = BarCode.PDF417(cadenaBarra, 2)
    '            bm = QRbarra(cadenaBarra)
    '            Dim valor = Agencia & Almacen & Td & serie & numero
    '            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm))

    '            Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
    '            If result = False Then
    '                MsgBox("La firma es adulterado")
    '            End If

    '            Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")

    '        Catch ex As CryptographicException
    '            MsgBox(ex.Message)
    '        End Try
    '    End If
    '    Dim valores() As String
    '    Dim campos() As String
    '    campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "TipoDocumento", "url"}
    '    If serie.Trim.Substring(0, 1) = "B" Then
    '        valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat1"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_en.Rows(0).Item("url")}
    '    Else
    '        valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_en.Rows(0).Item("url")}
    '    End If
    '    Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
    '    cab.idagencia = Agencia
    '    cab.idalmacen = Almacen
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    Dim dt As DataTable = cab.cabeceraPDF(cab)
    '    If Td = "FT" Or Td = "BV" Then
    '        PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores, True)
    '        If isprint = True Then
    '            lo_imprimir.ToTicket(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
    '        End If
    '    End If

    '    Dim archivo As New NComprobante_CPE
    '    With archivo
    '        .IdAgencia = Agencia
    '        .IdAlmacen = Almacen
    '        .IdTipoDocumento = Td
    '        .Serie = serie
    '        .NumeroDocumento = numero
    '        .xml_zip = Xml_zipBinary
    '        .pdf_pdf = PDF_Binary
    '        .cdr_zip = Nothing
    '        .Eliminar(archivo)
    '        .agregar(archivo)
    '    End With

    'End Sub

    'Private Sub Comprimir(ByVal Ruta As String, ByVal FileToZip As String, ByVal FileZip As String)
    '    Using zip As ZipFile = New ZipFile()
    '        zip.AddFile(Ruta & "\" & FileToZip, "")
    '        zip.Save(Ruta & "\" & FileZip)
    '    End Using
    'End Sub
    'Public Sub GenerarxmlMatriz(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional isprint As Boolean = False)
    '    Dim ca As String = "select isnull(SignAlias,'') as SingAlias from ptentidad where identidad='001'"
    '    Dim dx As DataTable
    '    Dim Xml_zipBinary, PDF_Binary As Byte()
    '    dx = go_Sql.EjecutarConsulta("d", ca).Tables(0)
    '    If dx.Rows.Count > 0 Then
    '        If dx.Rows(0).Item(0) <> "" Then
    '            eFacturacionCls.ModAlias.Asignar_Alias(dx.Rows(0).Item(0))
    '        Else
    '            MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
    '            Exit Sub
    '        End If
    '    Else
    '        eFacturacionCls.ModAlias.Asignar_Alias("SignAveo")
    '    End If
    '    Dim detentidad As New NDet_Entidad
    '    Dim Ruc As String
    '    Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
    '    Dim dt_En As New DataTable
    '    dt_En = go_Sql.EjecutarConsulta("entidad", "select Ruc,Nombre,Direccion,Pais,Departamento,Provincia,Distrito,IdTipoDocumento,CodUbigeo,isnull(rutapfx,'') as rutapfx,isnull(rutacer,'') as rutacer,isnull(pws,'') as pws,isnull(RsSunat,'') as RsSunat,isnull(NombreComercial,'-') as NombreComercial,isnull(RsSunat1,'') as RsSunat1,isnull(logo,'') as Logo,Isnull(Url,'')as Url from ptentidad").Tables(0)
    '    Dim rutapfx As String = Nothing
    '    Dim rutacer As String = Nothing
    '    Dim pws As String = Nothing
    '    If dt_En.Rows.Count > 0 Then
    '        rutapfx = dt_En.Rows(0).Item("rutapfx")
    '        rutacer = dt_En.Rows(0).Item("rutacer")
    '        pws = lo_estilo.Desencriptar(dt_En.Rows(0).Item("pws"))
    '    End If
    '    If rutacer = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub

    '    End If
    '    If rutapfx = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If pws = "" Then
    '        MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
    '        Exit Sub
    '    End If

    '    Dim Cabecera As New DataTable
    '    Dim detalle As New DataTable

    '    cab.idagencia = Agencia
    '    cab.idalmacen = Almacen
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    cab.idcliente = idcliente
    '    Cabecera = cab.cabeceraCPE(cab)

    '    If validar1(Cabecera)(1) = "1" Then
    '        MessageBox.Show(validar1(Cabecera)(0))
    '        Exit Sub
    '    End If



    '    det.idagencia = Agencia
    '    det.idalmacen = Almacen
    '    det.idtipodocumento = Td
    '    det.serie = serie
    '    det.numerodocumento = numero
    '    detalle = det.DetalleCPE(det)
    '    If dt_En.Rows.Count > 0 Then
    '        Dim efact As Byte() = Nothing
    '        Dim efact_firma As Byte() = Nothing
    '        With dt_En
    '            Ruc = .Rows(0).Item("RUC").ToString
    '        End With
    '        Version = "2.0" : OCustomId = "1.0"
    '        deletefile(Application.StartupPath & "\tempxml")
    '        OTd = Cabecera.Rows(0).Item("TdSunat")
    '        OComprobante = serie & "-" & numero
    '        OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
    '        FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
    '        If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
    '            Moneda = "PEN"
    '        Else
    '            Moneda = "USD"
    '        End If
    '        If OTd = "01" Then
    '            EFObj.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = EFObj.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "03" Then
    '            BE.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = BE.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "07" Then
    '            NC.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = NC.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "08" Then
    '            Nd.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = Nd.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        Dim has As String = Nothing
    '        Dim Signature As String = Nothing
    '        Try
    '            detentidad.IdEntidad = "001"
    '            detentidad = detentidad.Item(detentidad)
    '            Dim Key As X509Certificate2
    '            If detentidad.pfx_file IsNot Nothing Then
    '                Key = New X509Certificate2(detentidad.pfx_file, pws)
    '            Else
    '                Key = New X509Certificate2(rutapfx, pws)
    '            End If
    '            Dim Key1 As X509Certificate2
    '            If detentidad.cer_file IsNot Nothing Then
    '                Key1 = New X509Certificate2(detentidad.cer_file, pws)
    '            Else
    '                Key1 = New X509Certificate2(rutacer, pws)
    '            End If
    '            cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
    '            has = cls_firma.ReturCodHas()
    '            Signature = cls_firma.Retursignaturevalue()
    '            go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & Signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")

    '            Dim bm As Bitmap = Nothing
    '            'Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|" & has & "|" & Signature & "|"
    '            Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
    '            'bm = BarCode.PDF417(cadenaBarra, 2)
    '            bm = QRbarra(cadenaBarra)
    '            Dim valor = Agencia & Almacen & Td & serie & numero
    '            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm))
    '            Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
    '            If result = False Then
    '                MsgBox("La firma es adulterado")
    '            End If

    '            Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")
    '        Catch ex As CryptographicException
    '            MsgBox(ex.Message)
    '        End Try
    '    End If
    '    Dim valores() As String
    '    Dim campos() As String
    '    campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "Serie", "NumeroDocumento", "TipoDocumento", "Logo", "url", "RsRet", "RsPer"}
    '    If serie.Trim.Substring(0, 1) = "B" Then
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat1"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_En.Rows(0).Item("Logo"), dt_En.Rows(0).Item("url"), "", ""}
    '    Else
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_En.Rows(0).Item("Logo"), dt_En.Rows(0).Item("url"), "", ""}
    '    End If
    '    Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
    '    cab.idagencia = Agencia
    '    cab.idalmacen = Almacen
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    Dim dt As DataTable = cab.cabeceraPDF(cab)
    '    If dt.Rows.Count > 0 Then
    '        Dim i As Integer = dt.Rows.Count
    '        Dim max As Integer = 33
    '        For i = dt.Rows.Count To max
    '            dt.Rows.Add()
    '        Next
    '    End If
    '    If Td = "FT" Or Td = "BV" Then
    '        PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
    '        If isprint = True Then
    '            lo_imprimir.ToA4(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
    '        End If
    '    End If
    '    Dim archivo As New NComprobante_CPE
    '    With archivo
    '        .IdAgencia = Agencia
    '        .IdAlmacen = Almacen
    '        .IdTipoDocumento = Td
    '        .Serie = serie
    '        .NumeroDocumento = numero
    '        .xml_zip = Xml_zipBinary
    '        .pdf_pdf = PDF_Binary
    '        .agregar(archivo)
    '    End With
    'End Sub
    'Public Sub GenerarxmlMatriz_21(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional isprint As Boolean = False)
    '    Dim Xml_zipBinary, PDF_Binary As Byte()
    '    Dim entidad As New NPtentidad
    '    entidad.identidad = "001"
    '    entidad = entidad.Registro(entidad)
    '    Dim dt_en As DataTable = entidad.itemTbl(entidad)
    '    If entidad.signalias.Trim <> "" Then
    '        eFacturacionCls.ModAlias.Asignar_Alias(entidad.signalias)
    '    Else
    '        MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
    '        Exit Sub
    '    End If


    '    Dim detentidad As New NDet_Entidad
    '    Dim Ruc As String
    '    Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
    '    Dim rutapfx As String = Nothing
    '    Dim rutacer As String = Nothing
    '    Dim pws As String = Nothing
    '    rutapfx = entidad.rutapfx
    '    rutacer = entidad.rutacer
    '    pws = lo_estilo.Desencriptar(entidad.pws)

    '    If dt_en.Rows.Count > 0 Then
    '        rutapfx = dt_en.Rows(0).Item("rutapfx")
    '        rutacer = dt_en.Rows(0).Item("rutacer")
    '        pws = lo_estilo.Desencriptar(dt_en.Rows(0).Item("pws"))
    '    End If
    '    If rutacer = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub

    '    End If
    '    If rutapfx = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If pws = "" Then
    '        MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
    '        Exit Sub
    '    End If

    '    Dim Cabecera As New DataTable
    '    Dim detalle As New DataTable

    '    cab.idagencia = Agencia
    '    cab.idalmacen = Almacen
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    cab.idcliente = idcliente
    '    Cabecera = cab.cabeceraCPE21(cab)

    '    If validar1(Cabecera)(1) = "1" Then
    '        MessageBox.Show(validar1(Cabecera)(0))
    '        Exit Sub
    '    End If



    '    det.idagencia = Agencia
    '    det.idalmacen = Almacen
    '    det.idtipodocumento = Td
    '    det.serie = serie
    '    det.numerodocumento = numero
    '    detalle = det.DetalleCPE21(det)
    '    If dt_en.Rows.Count > 0 Then
    '        Dim efact As Byte() = Nothing
    '        Dim efact_firma As Byte() = Nothing
    '        With dt_en
    '            Ruc = .Rows(0).Item("RUC").ToString
    '        End With
    '        Version = "2.1" : OCustomId = "2.0"
    '        deletefile(Application.StartupPath & "\tempxml")
    '        OTd = Cabecera.Rows(0).Item("TdSunat")
    '        OComprobante = serie & "-" & numero
    '        OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
    '        FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
    '        If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
    '            Moneda = "PEN"
    '        Else
    '            Moneda = "USD"
    '        End If
    '        If OTd = "01" Then
    '            ef.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = ef.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "03" Then
    '            eb.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = eb.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "07" Then
    '            enc.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = enc.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        If OTd = "08" Then
    '            ent.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = ent.CreateInvoice(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle)
    '        End If
    '        Dim has As String = Nothing
    '        Dim Signature As String = Nothing
    '        Try
    '            detentidad.IdEntidad = "001"
    '            detentidad = detentidad.Item(detentidad)
    '            Dim Key As X509Certificate2
    '            If detentidad.pfx_file IsNot Nothing Then
    '                Key = New X509Certificate2(detentidad.pfx_file, pws)
    '            Else
    '                Key = New X509Certificate2(rutapfx, pws)
    '            End If
    '            Dim Key1 As X509Certificate2
    '            If detentidad.cer_file IsNot Nothing Then
    '                Key1 = New X509Certificate2(detentidad.cer_file, pws)
    '            Else
    '                Key1 = New X509Certificate2(rutacer, pws)
    '            End If
    '            cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
    '            has = cls_firma.ReturCodHas()
    '            Signature = cls_firma.Retursignaturevalue()
    '            go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & Signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")

    '            Dim bm As Bitmap = Nothing
    '            Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
    '            'Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|" & has & "|" & Signature & "|"
    '            'bm = BarCode.PDF417(cadenaBarra, 2)
    '            bm = QRbarra(cadenaBarra)
    '            Dim valor = Agencia & Almacen & Td & serie & numero
    '            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm))
    '            Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
    '            If result = False Then
    '                MsgBox("La firma es adulterado")
    '            End If

    '            Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")
    '        Catch ex As CryptographicException
    '            MsgBox(ex.Message)
    '        End Try
    '    End If
    '    Dim valores() As String
    '    Dim campos() As String
    '    campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "Serie", "NumeroDocumento", "TipoDocumento", "Logo", "url", "RsRet", "RsPer"}
    '    If serie.Trim.Substring(0, 1) = "B" Then
    '        valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat1"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_en.Rows(0).Item("Logo"), dt_en.Rows(0).Item("url"), "", ""}
    '    Else
    '        valores = {dt_en.Rows(0).Item("RUC").ToString, dt_en.Rows(0).Item("RsSunat"), dt_en.Rows(0).Item("Nombre"), dt_en.Rows(0).Item("Direccion"), dt_en.Rows(0).Item("Departamento") & "-" & dt_en.Rows(0).Item("Provincia") & "-" & dt_en.Rows(0).Item("Distrito"), dt_en.Rows(0).Item("NombreComercial"), serie, numero, Cabecera.Rows(0).Item("tipodocumento").ToString.Trim, Application.StartupPath & "\" & dt_en.Rows(0).Item("Logo"), dt_en.Rows(0).Item("url"), "", ""}
    '    End If
    '    Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
    '    cab.idagencia = Agencia
    '    cab.idalmacen = Almacen
    '    cab.idtipodocumento = Td
    '    cab.serie = serie
    '    cab.numerodocumento = numero
    '    Dim dt As DataTable = cab.cabeceraPDF(cab)
    '    If dt.Rows.Count > 0 Then
    '        Dim i As Integer = dt.Rows.Count
    '        Dim max As Integer = 33
    '        For i = dt.Rows.Count To max
    '            dt.Rows.Add()
    '        Next
    '    End If
    '    If Td = "FT" Or Td = "BV" Then
    '        PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
    '        If isprint = True Then
    '            lo_imprimir.ToA4(dt, "EInvoiceLogo.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 13, campos, valores)
    '        End If
    '    End If
    '    Dim archivo As New NComprobante_CPE
    '    With archivo
    '        .IdAgencia = Agencia
    '        .IdAlmacen = Almacen
    '        .IdTipoDocumento = Td
    '        .Serie = serie
    '        .NumeroDocumento = numero
    '        .xml_zip = Xml_zipBinary
    '        .pdf_pdf = PDF_Binary
    '        .cdr_zip = Nothing
    '        .Eliminar(archivo)
    '        .agregar(archivo)
    '    End With
    'End Sub
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

    'Public Sub ImprimirNotaVenta(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String, Optional isprint As Boolean = False)
    '    Dim pe As New NPtentidad
    '    pe.identidad = "001"
    '    pe = pe.Registro(pe)
    '    Dim valores() As String
    '    Dim campos() As String
    '    Dim campo As String() = {"Logo", "TipoDoumento", "NumeroDocumento"}
    '    campos = {"Logo", "TipoDoumento", "NumeroDocumento"}
    '    Dim parametros As Object = {"IdTDoc", "@Serie", "@NumDoc", "@Idcliente"}
    '    Dim tipoparametro As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
    '    Dim val As Object = {Td, serie, numero, idcliente}
    '    Dim dt As DataTable = go_Sql.ProcedureSQL("procRep_ComprobanteVenta", parametros, val, tipoparametro, 4).Tables(0)
    '    valores = {Application.StartupPath & "\" & pe.logo, dt.Rows(0).Item("tipodocumento").ToString.Trim, serie & "-" & numero}
    '    If dt.Rows.Count > 0 Then
    '        Dim i As Integer = dt.Rows.Count
    '        Dim max As Integer = 18
    '        For i = dt.Rows.Count To max
    '            dt.Rows.Add()
    '        Next
    '    End If
    '    lo_imprimir.ToA8(dt, "RepNotaVenta.rdl", 3, campos, valores)
    'End Sub

    'Public Shared Sub imprimir(fileName As String)
    '    Dim info As New ProcessStartInfo()
    '    info.Verb = "print"
    '    info.FileName = fileName
    '    info.CreateNoWindow = True
    '    info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
    '    Dim p As New System.Diagnostics.Process()
    '    p.StartInfo = info
    '    p.Start()
    '    System.Threading.Thread.Sleep(3000)
    '    Try
    '        If False = p.CloseMainWindow() Then
    '            p.Kill()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("No se logro imprimir el archivo, imprima desde Comprobantes electrónicos")
    '    End Try
    'End Sub
    Private Sub deletefile(ByVal ruta As String)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(ruta, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
            My.Computer.FileSystem.DeleteFile(foundFile)
        Next
    End Sub
    'Public Sub Generarxml_TicketPunto(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String)
    '    Dim ca As String = "select isnull(SignAlias,'') as SingAlias from ptentidad where identidad='001'"
    '    Dim dx As DataTable
    '    Dim Xml_zipBinary, PDF_Binary As Byte()
    '    dx = go_Sql.EjecutarConsulta("d", ca).Tables(0)
    '    If dx.Rows.Count > 0 Then
    '        If dx.Rows(0).Item(0) <> "" Then
    '            eFacturacionCls.ModAlias.Asignar_Alias(dx.Rows(0).Item(0))
    '        Else
    '            MsgBox("No existe el URI para el XML, favor de configurar el la opción firma digital")
    '            Exit Sub
    '        End If
    '    Else
    '        eFacturacionCls.ModAlias.Asignar_Alias("SignAveo")
    '    End If
    '    Dim detentidad As New NDet_Entidad
    '    Dim Ruc As String
    '    Dim Version As String, OCustomId As String, OComprobante As String = Nothing, OFEmision As DateTime, OTd As String = Nothing, Moneda As String
    '    Dim dt_En As New DataTable
    '    dt_En = go_Sql.EjecutarConsulta("entidad", "select Ruc,Nombre,Direccion,Pais,Departamento,Provincia,Distrito,IdTipoDocumento,CodUbigeo,isnull(rutapfx,'') as rutapfx,isnull(rutacer,'') as rutacer,isnull(pws,'') as pws,isnull(RsSunat,'') as RsSunat,isnull(NombreComercial,'-') as NombreComercial,isnull(RsSunat1,'') as RsSunat1,isnull(logo,'') as Logo,Isnull(Url,'')as Url from ptentidad").Tables(0)
    '    Dim rutapfx As String = Nothing
    '    Dim rutacer As String = Nothing
    '    Dim pws As String = Nothing
    '    If dt_En.Rows.Count > 0 Then
    '        rutapfx = dt_En.Rows(0).Item("rutapfx")
    '        rutacer = dt_En.Rows(0).Item("rutacer")
    '        pws = lo_estilo.Desencriptar(dt_En.Rows(0).Item("pws"))
    '    End If
    '    If rutacer = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub

    '    End If
    '    If rutapfx = "" Then
    '        MessageBox.Show("No existe certificado, no procede la generación del XML")
    '        Exit Sub
    '    End If
    '    If pws = "" Then
    '        MessageBox.Show("Los certificados no tienen asignados una contraseña no puede abrir el certificado")
    '        Exit Sub
    '    End If

    '    Dim Cabecera As New DataTable
    '    Dim detalle As New DataTable


    '    Dim cliente As String = " select c.IdAgencia,c.idalmacen,c.IdTipoDocumento,c.serie,c.numerodocumento,c.idcliente,c.nombreCliente,c.ruc,vtd.TdSunat,Cast(c.importeTotal-c.importeIgv as Decimal(18,2)) as ValorTotal,Cast(c.importeTotal as Decimal(18,2)) as ImporteTotal,Cast(c.ImporteIGV as Decimal(18,2)) as ImporteIGV,IdTipoDocumento1,Serie1,NumeroDocumento1,cl.TipoDocSunat,Vtd1.TdSunat as TdSunatRef,NumeroDocumento2,cl.departamento,pais,cl.Direccion,cl.Distrito,cl.Provincia,dbo.Fnumeroletra(ImporteTotal) as ImporteLetra,c.Descripcion as Obs,IdTipoNotaCredito,TdNC,o.descripcion as Motivo,c.IdFormaVenta,fv.Descripcion as Formaventa ,CodIGV as AfecIGV,IGV,TipoOperacion,ImporteDescuento,Fechadocumento,IdMoneda,codigoHas,signaturevalue,vtd.tipodocumento,isnull(EstadoSunat,0) as EstadoSunat,isnull(NumeroOrden,'') as NumeroOrden,IdTransportista from comprobante c inner join vtipodocumento vtd on c.IdTipoDocumento=vtd.idTipodocumento "
    '    cliente += " inner join cliente cl on c.IdCliente=cl.IdCliente  left join VTipoDocumento as vtd1 on c.IdTipoDocumento2=vtd1.idTipodocumento left join "
    '    cliente += " (SELECT 'NA'as Td,iDCODIGO,dESCRIPCION,TdNC FROM VTipoNotacredito  UNION ALL  SELECT  'ND'as Td,iDCODIGO,dESCRIPCION,TdNC FROM VTIPONOTADEBITO  )AS  o on   c.IdTipoDocumento=o.td and IdTipoNotaCredito=o.idcodigo  left join FormaVenta fv on c.IdFormaVenta=fv.IdFormaVenta "
    '    cliente += " left join VCatalogo_Sunat07 vigv on isnull(c.TipoAfecIGV,'G10')=vigv.IdCodigo where c.idcliente='" & idcliente & "' "
    '    cliente += " and c.IdAgencia='" & Agencia & "' and c.IdAlmacen='" & Almacen & "' and c.IdTipoDocumento='" & Td & "' and c.serie='" & serie & "' and c.numerodocumento='" & numero & "'"
    '    Cabecera = go_Sql.EjecutarConsulta("cliente", cliente).Tables(0)

    '    If validar1(Cabecera)(1) = "1" Then
    '        MessageBox.Show(validar1(Cabecera)(0))
    '        Exit Sub
    '    End If

    '    Dim SDetalle As String = " select Item, case when isnull(dbo.GetDua(IdArticulo),'')='' then IdArticulo else dbo.GetDua(IdArticulo) end as IdArticulo,d.Descripcion,Cantidad,Unidad,PrecioVenta,PrecioSIGV,d.ImporteIGV,ImporteUS,ImporteMN ,C.IGV,CodIGV as AfecIGV,IdFormaVenta,isnull(vu.UndRef,'NIU')as UndRef from detallecomprobante  d  inner join comprobante c on d.IdAlmacen=c.IdAlmacen and d.IdTipoDocumento=c.IdTipoDocumento and d.Serie=c.Serie and d.NumeroDocumento=c.NumeroDocumento "
    '    SDetalle += " left join VCatalogo_Sunat07 vigv on isnull(c.TipoAfecIGV,'G10')=vigv.IdCodigo left join VUnidMed vu on d.Unidad=vu.IdCodigo "
    '    SDetalle += " Where c.IdAgencia='" & Agencia & "' and c.IdAlmacen='" & Almacen & "' and c.IdTipoDocumento='" & Td & "' and c.serie='" & serie & "' and c.numerodocumento='" & numero & "'"
    '    detalle = go_Sql.EjecutarConsulta("ds", SDetalle).Tables(0)

    '    If dt_En.Rows.Count > 0 Then
    '        Dim efact As Byte() = Nothing
    '        Dim efact_firma As Byte() = Nothing
    '        With dt_En
    '            Ruc = .Rows(0).Item("RUC").ToString
    '        End With
    '        Version = "2.0" : OCustomId = "1.0"
    '        deletefile(Application.StartupPath & "\tempxml")
    '        OTd = Cabecera.Rows(0).Item("TdSunat")
    '        OComprobante = serie & "-" & numero
    '        OFEmision = Cabecera.Rows(0).Item("FechaDocumento")
    '        FileNamexml = Ruc & "-" & OTd & "-" & OComprobante
    '        If Cabecera.Rows(0).Item("IdMoneda") = "MN" Then
    '            Moneda = "PEN"
    '        Else
    '            Moneda = "USD"
    '        End If
    '        If OTd = "01" Then
    '            EFObj.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = EFObj.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "03" Then
    '            BE.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = BE.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "07" Then
    '            NC.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = NC.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If
    '        If OTd = "08" Then
    '            Nd.Pro_Moneda(Cabecera.Rows(0).Item("IdMoneda"))
    '            efact = Nd.CreatePO(Application.StartupPath & "\tempxml\" & FileNamexml & "tmp.xml", Ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_En, Cabecera, detalle)
    '        End If

    '        Dim has As String = Nothing
    '        Dim signature As String = Nothing
    '        Try
    '            detentidad.IdEntidad = "001"
    '            detentidad = detentidad.Item(detentidad)
    '            Dim Key As X509Certificate2
    '            If detentidad.pfx_file IsNot Nothing Then
    '                Key = New X509Certificate2(detentidad.pfx_file, pws)
    '            Else
    '                Key = New X509Certificate2(rutapfx, pws)
    '            End If
    '            Dim Key1 As X509Certificate2
    '            If detentidad.cer_file IsNot Nothing Then
    '                Key1 = New X509Certificate2(detentidad.cer_file, pws)
    '            Else
    '                Key1 = New X509Certificate2(rutacer, pws)
    '            End If
    '            cls_firma.firmaBinari(efact, Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key, False)
    '            has = cls_firma.ReturCodHas()
    '            signature = cls_firma.Retursignaturevalue()
    '            go_Sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & has & "', signatureValue='" & signature & "'", "IdAgencia='" & Agencia & "' and IdAlmacen='" & Almacen & "' and IdTipoDocumento='" & Td & "' and serie='" & serie & "' and numerodocumento='" & numero & "'")
    '            Dim bm As Bitmap = Nothing
    '            'Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|" & has & "|" & signature & "|"
    '            Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"

    '            'bm = BarCode.PDF417(cadenaBarra, 2)
    '            bm = QRbarra(cadenaBarra)
    '            Dim valor = Agencia & Almacen & Td & serie & numero
    '            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm))
    '            Dim result As Boolean = cls_firma.VerifyXmlFile_509(Application.StartupPath & "\XmlInvoice\" & FileNamexml & ".xml", Key1)
    '            'Dim result As Boolean = cls_firma.VerifyXmlFile_509(efact_firma, Key1)
    '            If result = False Then
    '                MsgBox("La firma es adulterado")
    '            End If
    '            Xml_zipBinary = Zip.ComprimirToBinary(Application.StartupPath & "\XmlInvoice", FileNamexml & ".xml", FileNamexml & ".zip")
    '        Catch ex As CryptographicException
    '            MsgBox(ex.Message)
    '        End Try
    '    End If
    '    Dim valores() As String
    '    Dim campos() As String
    '    campos = {"RucEmisonElectronico", "ResolucionSunat", "RazonSocial", "Direccion", "DireccionComplementaria", "NombreComercial", "TipoDocumento", "url"}

    '    If serie.Trim.Substring(0, 1) = "B" Then
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat1"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_En.Rows(0).Item("url")}
    '    Else
    '        valores = {dt_En.Rows(0).Item("RUC").ToString, dt_En.Rows(0).Item("RsSunat"), dt_En.Rows(0).Item("Nombre"), dt_En.Rows(0).Item("Direccion"), dt_En.Rows(0).Item("Departamento") & "-" & dt_En.Rows(0).Item("Provincia") & "-" & dt_En.Rows(0).Item("Distrito"), dt_En.Rows(0).Item("NombreComercial"), Cabecera.Rows(0).Item("TipoDocumento").ToString.Trim, dt_En.Rows(0).Item("url")}
    '    End If
    '    Dim concat As String = Format(Cabecera.Rows(0).Item("FechaDocumento"), "ddMMyy") & Cabecera.Rows(0).Item("ImporteTotal").ToString.Replace(".", "")
    '    Dim cadena As String = " select dt.IdAgencia, dt.IdTipoDocumento, dt.Serie, dt.NumeroDocumento, dt.Item, case when isnull(dbo.GetDua(IdArticulo),'')='' then dt.IdArticulo else dbo.GetDua(dt.IdArticulo) end as IdArticulo, dt.Descripcion, dt.Cantidad, dt.Unidad, dt.SaldoEntrega, dt.PrecioVenta,"
    '    cadena += " dt.PrecioSIGV, dt.ImporteDescuento, dt.IGV, dt.ImporteIGV, dt.ImporteUS, dt.ImporteMN, dt.Estado, dt.IdAlmacen, dt.Stock, dt.IdLista, dt.LoteSerie, "
    '    cadena += " c.Codigohas, c.signatureValue,c.barrapdf417,ISNULL(c.RUC,IdCliente) As Ruc, c.NombreCliente,c.Direccion,FechaDocumento,c.idmoneda,ImporteTotal ,dbo.FNumeroLetra(c.ImporteTotal)As ImporteLetras,EstadoSunat  "
    '    cadena += " ,c.idformaventa,isnull(TipoOperacion,'GRAV') AS TipoOperacion,FechaVencimineto,Nombre as NombreVendedor,f.Descripcion as FormaVenta,vt.tipoDocumento,VNC.Descripcion as MotivoAnulacion "
    '    cadena += " ,Vtf.TdSunat as TdSunatRef,c.NumeroDocumento2,c.importeDescuento as DescuentoGlobal ,c.importeIGV as TotalIGV,(c.ImporteTotal-c.ImporteIGV)as ValorTotal,isnull(islote,'0') as islote,loteserie,IdTransportista,NumeroOrden from comprobante c inner  join detallecomprobante dt "
    '    cadena += " on c.Idagencia=dt.idagencia and c.idalmacen=dt.idalmacen and c.idtipodocumento=dt.idtipodocumento and c.serie=dt.serie and c.numerodocumento=dt.Numerodocumento left join vendedor v on "
    '    cadena += " c.IdVendedor=v.IdVendedor  left join FormaVenta f on c.IdFormaVenta=f.IdFormaVenta   left join vtipodocumento vt on c.IdTipoDocumento=vt.idTipoDocumento "
    '    cadena += " left join (select ('H'+IdCodigo) AS IdCodigo,Descripcion from VTipoNotaCredito union all select ('D'+IdCodigo) as IdCodigo,Descripcion from VTipoNotaDebito) Vnc on RTRIM(debehaber)+rtrim(c.IdTipoNotaCredito)=RTRIM(VNC.IdCodigo) "
    '    cadena += " left join VTipoDocumento Vtf on c.IdTipoDocumento2=vtf.idTipoDocumento "
    '    cadena += " Where dt.IdAgencia='" & Agencia & "' and dt.IdAlmacen='" & Almacen & "' and dt.IdTipoDocumento='" & Td & "' and dt.serie='" & serie & "' and dt.numerodocumento='" & numero & "'"
    '    Dim dt As DataTable
    '    dt = go_Sql.EjecutarConsulta("fac", cadena).Tables(0)

    '    If Td = "FT" Or Td = "BV" Then
    '        PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
    '    End If

    '    'If IsNothing(Xml_zipBinary) = False Then
    '    '    go_Sql.saveimagen("Articulo", "FotoArt", "IdArticulo", Me.txtcodigo.Text, lo_estilo.Image2Bytes(Me.PictureBox1.Image))
    '    'End If


    '    Dim archivo As New NComprobante_CPE
    '    With archivo
    '        .IdAgencia = Agencia
    '        .IdAlmacen = Almacen
    '        .IdTipoDocumento = Td
    '        .Serie = serie
    '        .NumeroDocumento = numero
    '        .xml_zip = Xml_zipBinary
    '        .pdf_pdf = PDF_Binary
    '        .cdr_zip = Nothing
    '        .Eliminar(archivo)
    '        .agregar(archivo)

    '        ' .Actualizar(archivo)

    '    End With
    'End Sub
    Public Sub Generarxml_TicketPunto21(Agencia As String, Almacen As String, Td As String, serie As String, numero As String, idcliente As String)
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
        Cabecera = cab.cabeceraCPE21(cab)
        If validar1(Cabecera)(1) = "1" Then
            MessageBox.Show(validar1(Cabecera)(0))
            Exit Sub
        End If
        det.idtipodocumento = Td
        det.serie = serie
        det.numerodocumento = numero
        detalle = det.DetalleCPE21(det)

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
                '****** actualizar los codigos de barras
                cab = cab.Registro(cab)
                cab.signaturevalue = signature
                cab.codigohas = has
                cab.estadosunat = "1"
                Dim bm As Bitmap = Nothing
                Dim cadenaBarra As String = Ruc.Trim & "|" & OTd.Trim & "|" & serie.Trim & "|" & numero.Trim & "|" & Cabecera.Rows(0).Item("importeIGV") & "|" & Cabecera.Rows(0).Item("ImporteTotal") & "|" & Cabecera.Rows(0).Item("FechaDocumento") & "|" & Cabecera.Rows(0).Item("TipoDocSunat").trim & "|" & Cabecera.Rows(0).Item("Ruc").trim & "|"
                bm = QRbarra(cadenaBarra)
                cab.barrapdf417 = lo_estilo.Image2Bytes(bm)
                cab.Actualizar(cab)
                '*** end barra
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
        Dim dt As DataTable = cab.cabeceraPDF(cab)
        If Td = "FT" Or Td = "BV" Then
            PDF_Binary = lo_imprimir.ToPdfBinario(dt, "EInvoiceTicket.rdl", FileNamexml & concat, Application.StartupPath & "\XmlInvoice", 8, campos, valores)
        End If
        Dim archivo As New NComprobante_CPE
        With archivo
            .IdAgencia = Agencia
            .IdAlmacen = Almacen
            .IdTipoDocumento = Td
            .Serie = serie
            .NumeroDocumento = numero
            .xml_zip = Xml_zipBinary
            .pdf_pdf = PDF_Binary
            .cdr_zip = Nothing
            .Eliminar(archivo)
            .agregar(archivo)
        End With
    End Sub
End Class
