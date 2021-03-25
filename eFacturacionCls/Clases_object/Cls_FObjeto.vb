Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.VisualBasic
Imports CapaNegocios
Public Class Cls_FObjeto
    Dim ls_TipoMoneda As New CurrencyCodeContentType
    Dim MonedaDet As String = "MN"
    Dim alm As New NAlmacen
    Public Sub Pro_Moneda(ByVal IdMon As String)
        If IdMon = "MN" Then
            ls_TipoMoneda = CurrencyCodeContentType.PEN
        End If
        If IdMon = "US" Then
            ls_TipoMoneda = CurrencyCodeContentType.USD
        End If
        MonedaDet = IdMon
    End Sub
    Public Function CreatePOFile(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OComprobante As String, OFEmsion As DateTime,
                         OTd As String, Moneda As String, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, ByVal detalle As DataTable)
        Dim serializer As New XmlSerializer(GetType(InvoiceType))
        '**********
        Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))

        Dim Invoice As New InvoiceType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '********** Tipo Documento + Nro Comprobante
        Factura.Value = OComprobante
        Invoice.ID = Factura
        '********** Fecha de Emision del Documento
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha
        '********** Codigo del Tipo De Documento según Sunat
        Cod_Doc.Value = OTd
        Invoice.InvoiceTypeCode = Cod_Doc
        '********** Tipo de Moneda PEN o USD
        xml_Mon.Value = Moneda
        Invoice.DocumentCurrencyCode = xml_Mon





        '********************** Emisor de facturación electronica
        '"select Ruc,Nombre,Direccion,Pais,Departamento,Provincia,Distrito from ptentidad"
        With DatosEE.Rows(0)
            'esAlmacen
            Dim ubigeo As String, Direccion As String, Departamento As String, Distrito As String, provincia As String = ""
            '************* DATOS DE FIRMA
            '"IDSignSP""#SignatureSP"
            alm.IdAlmacen = Cabecera.Rows(0).Item("IdAlmacen")
            alm = alm.Registro(alm)
            If IsNothing(alm.ubigeo) = False Then
                ubigeo = alm.ubigeo
                Direccion = alm.Direccion
                Departamento = alm.Departamento
                Distrito = alm.Distrito
                provincia = alm.Provincia
            Else
                ubigeo = .Item("CodUbigeo")
                Direccion = .Item("Direccion")
                Departamento = .Item("Departamento")
                provincia = .Item("Provincia")
                Distrito = .Item("Distrito")
            End If
            Invoice.Signature = Firma(ls_IdSing, Ruc, .Item("Nombre"), "#" & ls_IdSing)
            Invoice.AccountingSupplierParty = Datos_Prov_SEE(ubigeo, .Item("Nombre"), .Item("IdTipoDocumento").ToString.Trim, Ruc, Direccion, "", Departamento, provincia, Distrito, "PE", .Item("NombreComercial"))
        End With


        '********************** Datos del cliente 
        With Cabecera.Rows(0)

            If .Item("Ruc").ToString.Trim = "0001" Then
                Invoice.AccountingCustomerParty = Datos_Cli_SEE("-", "-", .Item("nombreCliente")) 'AccountingCustomerParty
            Else
                Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("TipoDocSunat"), .Item("Ruc"), .Item("nombreCliente")) 'AccountingCustomerParty
            End If

            'select idcliente,cl.RUC,cl.TipoDocSunat,cl.nombre,cl.departamento,pais,cl.Direccion,cl.Distrito,cl.Provincia from cliente cl



            Dim FormaVenta As New PaymentTermsType
            Dim FormaVenta1(1) As PaymentTermsType
            Dim Formvta As New NoteType
            Dim Formvta1(1) As NoteType
            Formvta.Value = .Item("Formaventa").ToString.Trim
            ' Formvta.languageID
            Formvta1(0) = Formvta
            FormaVenta.Note = Formvta1
            FormaVenta1(0) = FormaVenta
            Invoice.PaymentTerms = FormaVenta1
            Dim Tax_pru(2) As TaxTotalType

            '********************** impuestos
            'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
            If .Item("idformaventa").ToString.Trim = "O" Then
                Tax_pru(0) = Tax_Impuesto(CType(0.00, Decimal).ToString("#.00"), "1000", "IGV", "VAT", .Item("AfecIGV").ToString.Trim, .Item("IGV").ToString.Trim)
            Else
                Tax_pru(0) = Tax_Impuesto(CType(.Item("ImporteIGV"), Decimal).ToString("#.00"), "1000", "IGV", "VAT", .Item("AfecIGV").ToString.Trim, .Item("IGV").ToString.Trim)
            End If
            'Tax_pru(2) = Tax_Impuesto(300, "9999", "OTROS", "OTH")
            Invoice.TaxTotal = Tax_pru

            '******************** Totales Generales
            Dim LegalMonetaryTotal As New MonetaryTotalType
            Dim PayableAmount As New PayableAmountType
            Dim LineExtensionAmount As New LineExtensionAmountType
            Dim TaxExclusiveAmount As New TaxExclusiveAmountType
            Dim AllowanceTotalAmoun As New AllowanceTotalAmountType

            ' valor de venta
            LineExtensionAmount.currencyID = ls_TipoMoneda
            ' impuesto IGV
            TaxExclusiveAmount.currencyID = ls_TipoMoneda
            AllowanceTotalAmoun.currencyID = ls_TipoMoneda

            If .Item("idformaventa").ToString.Trim = "O" Then
                LineExtensionAmount.Value = CType(0.00, Decimal).ToString("#.00")
                TaxExclusiveAmount.Value = CType(0.00, Decimal).ToString("#.00")
                PayableAmount.currencyID = ls_TipoMoneda
                PayableAmount.Value = CType(0.00, Decimal).ToString("#.00")
                LegalMonetaryTotal.TaxExclusiveAmount = TaxExclusiveAmount
                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            Else
                LineExtensionAmount.Value = CType(.Item("ValorTotal"), Decimal).ToString("#.00")
                TaxExclusiveAmount.Value = CType(.Item("ImporteIGV"), Decimal).ToString("#.00")
                AllowanceTotalAmoun.Value = CType(.Item("ImporteDescuento"), Decimal).ToString("#.00")
                PayableAmount.currencyID = ls_TipoMoneda
                PayableAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")
                LegalMonetaryTotal.TaxExclusiveAmount = TaxExclusiveAmount
                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.AllowanceTotalAmount = AllowanceTotalAmoun
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            End If
            ' total general
            '
            Dim Adicional As New AdditionalInformationType1

            Dim AdicionalItem(0) As AdditionalInformationType1
            Dim Monetary As New AdditionalMonetaryTotalType
            Dim MonetaryItem(3) As AdditionalMonetaryTotalType
            Dim Leyenda As New AdditionalPropertyType
            Dim LeyendaItem(3) As AdditionalPropertyType
            Dim Content As New ExtensionContentType
            Dim ContentItem(0) As ExtensionContentType
            If .Item("idformaventa").ToString.Trim = "O" Then

                MonetaryItem(0) = MontoGenerales("1001", CType(0.00, Decimal).ToString("#.00"))
                MonetaryItem(1) = MontoGenerales("1004", CType(.Item("ImporteTotal"), Decimal).ToString("#.00"))
                LeyendaItem(0) = MontoLetras("1002", "TRANSFERENCIA GRATUITA")
            Else
                If .Item("TipoOperacion").ToString.Trim = "EXO" Then
                    MonetaryItem(0) = MontoGenerales("1003", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
                If .Item("TipoOperacion").ToString.Trim = "GRAV" Then
                    MonetaryItem(0) = MontoGenerales("1001", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
                If .Item("TipoOperacion").ToString.Trim = "INAF" Then
                    MonetaryItem(0) = MontoGenerales("1002", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
            End If
            Adicional.AdditionalMonetaryTotal = MonetaryItem

            Adicional.AdditionalProperty = LeyendaItem

            '' placa del vehiculo para grifo
            'Dim placa As New LicensePlateIDType
            'placa.Value = "TU PLACA"
            'Dim transpor As New SUNATRoadTransportType
            'Dim tp(0) As SUNATRoadTransportType
            'transpor.LicensePlateID = placa
            'tp(0) = transpor

            'Dim trans As New SUNATEmbededDespatchAdviceType
            'Dim transx(0) As SUNATEmbededDespatchAdviceType
            'trans.SUNATRoadTransport = tp
            '' transx(0) = trans

            'Adicional.SUNATEmbededDespatchAdvice = trans

            AdicionalItem(0) = Adicional
            Content.AdditionalInformation = AdicionalItem
            ContentItem(0) = Content

            Dim ubl As New UBLExtensionType
            Dim ubl_1(1) As UBLExtensionType
            ubl.ExtensionContent = ContentItem
            ubl_1(0) = ubl
            ubl_1(1) = UBLExtensions1x1()
            'ubl_1(2) = UBLExtensions1x("10004", 48357.15)
            Invoice.UBLExtensions = ubl_1

        End With
        '*************** DETALLE DE FACTURAS
        'detalle 
        Invoice.InvoiceLine = DetalleItem(detalle.Rows.Count, detalle)
        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Close()
        writer.Close()


    End Function
    Public Function CreatePO(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OComprobante As String, OFEmsion As DateTime,
                         OTd As String, Moneda As String, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, ByVal detalle As DataTable) As Byte()
        Dim serializer As New XmlSerializer(GetType(InvoiceType))
        '**********
        Dim ms As New MemoryStream
        Dim writer As New StreamWriter(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"))
        '*********
        'Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))

        Dim Invoice As New InvoiceType()

        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '********** Tipo Documento + Nro Comprobante
        Factura.Value = OComprobante
        Invoice.ID = Factura
        '********** Fecha de Emision del Documento
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha
        '********** Codigo del Tipo De Documento según Sunat
        Cod_Doc.Value = OTd
        Invoice.InvoiceTypeCode = Cod_Doc
        '********** Tipo de Moneda PEN o USD
        xml_Mon.Value = Moneda
        Invoice.DocumentCurrencyCode = xml_Mon

        If Cabecera.Rows(0).Item("NumeroOrden").ToString.Trim <> "" Then
            Dim docref(3) As DocumentReferenceType
            docref(0) = DocuentoReferencia(Cabecera.Rows(0).Item("NumeroOrden"), "99")
            Invoice.AdditionalDocumentReference = docref
        End If



        '********************** Emisor de facturación electronica
        '"select Ruc,Nombre,Direccion,Pais,Departamento,Provincia,Distrito from ptentidad"
        'With DatosEE.Rows(0)
        '    '************* DATOS DE FIRMA
        '    '"IDSignSP""#SignatureSP"

        '    Invoice.Signature = Firma(ls_IdSing, Ruc, .Item("Nombre"), "#" & ls_IdSing)

        '    Invoice.AccountingSupplierParty = Datos_Prov_SEE(.Item("CodUbigeo"), .Item("Nombre"), .Item("IdTipoDocumento").ToString.Trim, Ruc, .Item("Direccion"), "", .Item("Departamento"), .Item("Provincia"), .Item("Distrito"), "PE", .Item("NombreComercial"))

        'End With

        With DatosEE.Rows(0)
            'esAlmacen
            Dim ubigeo As String, Direccion As String, Departamento As String, Distrito As String, provincia As String = ""
            '************* DATOS DE FIRMA
            '"IDSignSP""#SignatureSP"
            alm.IdAlmacen = Cabecera.Rows(0).Item("IdAlmacen")
            alm = alm.Registro(alm)
            If IsNothing(alm.ubigeo) = False Then
                ubigeo = alm.ubigeo
                Direccion = alm.Direccion
                Departamento = alm.Departamento
                Distrito = alm.Distrito
                provincia = alm.Provincia
            Else
                ubigeo = .Item("CodUbigeo")
                Direccion = .Item("Direccion")
                Departamento = .Item("Departamento")
                provincia = .Item("Provincia")
                Distrito = .Item("Distrito")
            End If
            Invoice.Signature = Firma(ls_IdSing, Ruc, .Item("Nombre"), "#" & ls_IdSing)
            Invoice.AccountingSupplierParty = Datos_Prov_SEE(ubigeo, .Item("Nombre"), .Item("IdTipoDocumento").ToString.Trim, Ruc, Direccion, "", Departamento, provincia, Distrito, "PE", .Item("NombreComercial"))
        End With


        '********************** Datos del cliente 
        With Cabecera.Rows(0)
            If .Item("Ruc").ToString.Trim = "0001" Then
                Invoice.AccountingCustomerParty = Datos_Cli_SEE("-", "-", .Item("nombreCliente")) 'AccountingCustomerParty
            Else
                Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("TipoDocSunat"), .Item("Ruc"), .Item("nombreCliente")) 'AccountingCustomerParty
            End If
            Dim FormaVenta As New PaymentTermsType
            Dim FormaVenta1(1) As PaymentTermsType
            Dim Formvta As New NoteType
            Dim Formvta1(1) As NoteType
            Formvta.Value = .Item("Formaventa").ToString.Trim
            Formvta1(0) = Formvta
            FormaVenta.Note = Formvta1
            FormaVenta1(0) = FormaVenta
            Invoice.PaymentTerms = FormaVenta1
            Dim Tax_pru(2) As TaxTotalType

            '********************** impuestos
            'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
            If .Item("idformaventa").ToString.Trim = "O" Then
                Tax_pru(0) = Tax_Impuesto(CType(0.00, Decimal).ToString("#.00"), "1000", "IGV", "VAT", .Item("AfecIGV").ToString.Trim, .Item("IGV").ToString.Trim)
            Else
                Tax_pru(0) = Tax_Impuesto(CType(.Item("ImporteIGV"), Decimal).ToString("#.00"), "1000", "IGV", "VAT", .Item("AfecIGV").ToString.Trim, .Item("IGV").ToString.Trim)
            End If

            'Tax_pru(2) = Tax_Impuesto(300, "9999", "OTROS", "OTH")
            Invoice.TaxTotal = Tax_pru

            '******************** Totales Generales
            Dim LegalMonetaryTotal As New MonetaryTotalType
            Dim PayableAmount As New PayableAmountType
            Dim LineExtensionAmount As New LineExtensionAmountType
            Dim TaxExclusiveAmount As New TaxExclusiveAmountType
            Dim AllowanceTotalAmoun As New AllowanceTotalAmountType

            ' valor de venta
            LineExtensionAmount.currencyID = ls_TipoMoneda
            ' impuesto IGV
            TaxExclusiveAmount.currencyID = ls_TipoMoneda
            AllowanceTotalAmoun.currencyID = ls_TipoMoneda

            If .Item("idformaventa").ToString.Trim = "O" Then
                LineExtensionAmount.Value = CType(0.00, Decimal).ToString("#.00")
                TaxExclusiveAmount.Value = CType(0.00, Decimal).ToString("#.00")
                PayableAmount.currencyID = ls_TipoMoneda
                PayableAmount.Value = CType(0.00, Decimal).ToString("#.00")
                LegalMonetaryTotal.TaxExclusiveAmount = TaxExclusiveAmount
                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            Else
                LineExtensionAmount.Value = CType(.Item("ValorTotal"), Decimal).ToString("#.00")
                TaxExclusiveAmount.Value = CType(.Item("ImporteIGV"), Decimal).ToString("#.00")
                AllowanceTotalAmoun.Value = CType(.Item("ImporteDescuento"), Decimal).ToString("#.00")
                PayableAmount.currencyID = ls_TipoMoneda
                PayableAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")
                LegalMonetaryTotal.TaxExclusiveAmount = TaxExclusiveAmount
                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.AllowanceTotalAmount = AllowanceTotalAmoun
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            End If
            ' total general
            '
            Dim Adicional As New AdditionalInformationType1
            Dim AdicionalItem(0) As AdditionalInformationType1
            Dim Monetary As New AdditionalMonetaryTotalType
            Dim MonetaryItem(3) As AdditionalMonetaryTotalType
            Dim Leyenda As New AdditionalPropertyType
            Dim LeyendaItem(3) As AdditionalPropertyType
            Dim Content As New ExtensionContentType
            Dim ContentItem(0) As ExtensionContentType
            If .Item("idformaventa").ToString.Trim = "O" Then

                MonetaryItem(0) = MontoGenerales("1001", CType(0.00, Decimal).ToString("#.00"))
                MonetaryItem(1) = MontoGenerales("1004", CType(.Item("ImporteTotal"), Decimal).ToString("#.00"))
                LeyendaItem(0) = MontoLetras("1002", "TRANSFERENCIA GRATUITA")
            Else
                If .Item("TipoOperacion").ToString.Trim = "EXO" Then
                    MonetaryItem(0) = MontoGenerales("1003", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
                If .Item("TipoOperacion").ToString.Trim = "GRAV" Then
                    MonetaryItem(0) = MontoGenerales("1001", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
                If .Item("TipoOperacion").ToString.Trim = "INAF" Then
                    MonetaryItem(0) = MontoGenerales("1002", CType(.Item("ValorTotal"), Decimal).ToString("#.00"))
                    If .Item("ImporteDescuento") <> 0 Then
                        MonetaryItem(1) = MontoGenerales("2005", CType(.Item("ImporteDescuento"), Decimal).ToString("#.00"))
                    End If
                    LeyendaItem(0) = MontoLetras("1000", .Item("ImporteLetra"))
                End If
            End If
            Adicional.AdditionalMonetaryTotal = MonetaryItem

            Adicional.AdditionalProperty = LeyendaItem
            If IsDBNull(Cabecera.Rows(0).Item("IdTransportista")) = False Then
                Adicional.SUNATEmbededDespatchAdvice = SunatPlaca(Cabecera.Rows(0).Item("IdTransportista"))  'trans
            Else
                Adicional.SUNATEmbededDespatchAdvice = SunatPlaca(Cabecera.Rows(0).Item("IdTransportista").ToString)  'trans
            End If



            AdicionalItem(0) = Adicional
            Content.AdditionalInformation = AdicionalItem
            ContentItem(0) = Content

            Dim ubl As New UBLExtensionType
            Dim ubl_1(1) As UBLExtensionType
            ubl.ExtensionContent = ContentItem
            ubl_1(0) = ubl
            ubl_1(1) = UBLExtensions1x1()
            'ubl_1(2) = UBLExtensions1x("10004", 48357.15)
            Invoice.UBLExtensions = ubl_1
        End With
        '*************** DETALLE DE FACTURAS
        'detalle 
        Invoice.InvoiceLine = DetalleItem(detalle.Rows.Count, detalle)
        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)
        xwriter.Formatting = Formatting.Indented

        'Dim PItext As String = "type=""text/xsl"" href=""factura.xsl"""
        'xwriter.WriteProcessingInstruction("xml-stylesheet", PItext)

        '        serializer.Serialize(xwriter, Invoice)
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Flush()
        xwriter.Close()
        ' writer.Flush()
        writer.Close()

        Return ms.ToArray

    End Function
    Private Function SunatPlaca(Nroplaca As String) As SUNATEmbededDespatchAdviceType
        Dim tp(0) As SUNATRoadTransportType
        tp(0) = SUNATRoadTransport(Nroplaca)
        Dim trans As New SUNATEmbededDespatchAdviceType
        trans.SUNATRoadTransport = tp
        Return trans
    End Function
    Private Function Placa(nroplaca As String) As LicensePlateIDType
        Dim item As New LicensePlateIDType
        item.Value = nroplaca
        Return item
    End Function
    Private Function SUNATRoadTransport(nroplaca As String) As SUNATRoadTransportType
        Dim transpor As New SUNATRoadTransportType
        transpor.LicensePlateID = Placa(nroplaca)
        Return transpor
    End Function

    Private Function UBLExtensions1x(ByVal Codigo As String, importe As Decimal, ByVal MontoLetra As String, CodLeyenda As String) As UBLExtensionType
        Dim UBLExtension1 As New UBLExtensionType
        Dim ExtensionContent1 As New ExtensionContentType
        Dim ExtensionContent_1(0) As ExtensionContentType
        ExtensionContent1.AdditionalInformation = informacionAddicional(Codigo, importe, MontoLetra, CodLeyenda)
        ExtensionContent_1(0) = ExtensionContent1
        UBLExtension1.ExtensionContent = ExtensionContent_1
        Return UBLExtension1
    End Function
    Private Function UBLExtensions1x1() As UBLExtensionType
        Dim UBLExtension1Y As New UBLExtensionType
        Dim ExtensionContent1Y As New ExtensionContentType
        Dim ExtensionContent_1Y(0) As ExtensionContentType
        ExtensionContent_1Y(0) = ExtensionContent1Y
        UBLExtension1Y.ExtensionContent = ExtensionContent_1Y
        Return UBLExtension1Y
    End Function


    Private Function informacionAddicional(ByVal ID_ As String, PayableAmount_ As Decimal, ByVal MontoLetra As String, CodLeyenda As String) As AdditionalInformationType1()
        Dim AdditionalInformation1 As New AdditionalInformationType1
        Dim AdditionalInformation_1(0) As AdditionalInformationType1
        Dim AdditionalMonetaryTotal_1(2) As AdditionalMonetaryTotalType
        Dim AdditionalProperty(0) As AdditionalPropertyType
        AdditionalMonetaryTotal_1(0) = MontoGenerales(ID_, PayableAmount_)
        AdditionalInformation1.AdditionalMonetaryTotal = AdditionalMonetaryTotal_1
        AdditionalProperty(0) = MontoLetras(CodLeyenda, MontoLetra)
        AdditionalInformation1.AdditionalProperty = AdditionalProperty
        AdditionalInformation_1(0) = AdditionalInformation1
        Return AdditionalInformation_1

    End Function
    Private Function MontoGenerales(ByVal ID_ As String, PayableAmount_ As Decimal) As AdditionalMonetaryTotalType
        Dim AdditionalMonetaryTotal1 As New AdditionalMonetaryTotalType
        Dim ID As New IDType
        Dim PayableAmount As New PayableAmountType
        PayableAmount.currencyID = ls_TipoMoneda
        ID.Value = ID_
        PayableAmount.Value = PayableAmount_
        AdditionalMonetaryTotal1.ID = ID
        AdditionalMonetaryTotal1.PayableAmount = PayableAmount
        Return AdditionalMonetaryTotal1
    End Function
    Private Function DocuentoReferencia(Nroreferencia As String, tipodocumento As String) As DocumentReferenceType
        Dim adicional As New DocumentReferenceType
        Dim ID As New IDType
        Dim codigo As New DocumentTypeCodeType
        ID.Value = Nroreferencia
        codigo.Value = tipodocumento
        adicional.ID = ID
        adicional.DocumentTypeCode = codigo

        Return adicional
    End Function

    Private Function MontoLetras(ByVal ID_ As String, Letra As String) As AdditionalPropertyType
        Dim AdditionalPropertyType1 As New AdditionalPropertyType
        Dim ID As New IDType
        Dim Value_ As New ValueType
        ID.Value = ID_
        Value_.Value = Letra
        AdditionalPropertyType1.ID = ID
        AdditionalPropertyType1.Value = Value_
        Return AdditionalPropertyType1
    End Function


    Private Function Firma(ByVal IdFirma As String, ByVal Ir_Ruc As String, Name_Emision As String, Uri_Firma As String) As SignatureType()
        Dim Firma1 As New SignatureType
        Dim sig(0) As SignatureType
        Dim Id As New IDType()

        Id.Value = IdFirma '"IDSignSP"
        Firma1.ID = Id
        Firma1.SignatoryParty = SignatoryParty(Ir_Ruc, Name_Emision)
        Firma1.DigitalSignatureAttachment = AtacchFirma(Uri_Firma)
        sig(0) = Firma1
        Return sig
    End Function
    Private Function AtacchFirma(ByVal Uri_Firma As String) As AttachmentType
        Dim Firmaatach As New AttachmentType
        Dim ExternalReference As New ExternalReferenceType
        Dim uri As New URIType
        uri.Value = Uri_Firma ' "#SignatureSP"
        ExternalReference.URI = uri
        Firmaatach.ExternalReference = ExternalReference

        Return Firmaatach

    End Function
    Private Function SignatoryParty(ByVal Ir_Ruc As String, Name_Emision As String) As PartyType
        Dim sigparty As New PartyType
        Dim PartyIdentification1 As New PartyIdentificationType
        Dim PartyIdentification_1(0) As PartyIdentificationType
        Dim PartyName1 As New PartyNameType
        Dim PartyName_1(0) As PartyNameType

        Dim ID As New IDType
        Dim Name1 As New NameType1

        ID.Value = Ir_Ruc.Trim '"20600331176"
        PartyIdentification1.ID = ID
        Name1.Value = Name_Emision.Trim  '"INTELIGENCIA DE VENTAS SAC"
        PartyName1.Name = Name1
        PartyName_1(0) = PartyName1
        sigparty.PartyName = PartyName_1
        PartyIdentification_1(0) = PartyIdentification1
        sigparty.PartyIdentification = PartyIdentification_1
        Return sigparty
    End Function
    'Private Function Get_TaxCategory(ByVal Id As String, Nam_Tax As String, Code_Tax As String, TaxExcepcionCode As String) As Object
    '    Dim ID_tax As New IDType, Name_tax As New NameType1, TaxTypeCode_tax As New TaxTypeCodeType
    '    Dim TaxCategory As New TaxCategoryType
    '    ID_tax.Value = Id ' "1000"
    '    ID_tax.schemeID = "UN/ECE 5153"
    '    ID_tax.schemeName = "Tax Scheme Identifier"
    '    ID_tax.schemeAgencyName = "United Nations Economic Commission for Europe"
    '    Name_tax.Value = Nam_Tax '"IGV"
    '    TaxTypeCode_tax.Value = Code_Tax '"VAT"
    '    Dim TaxScheme As New TaxSchemeType With {.ID = ID_tax, .Name = Name_tax, .TaxTypeCode = TaxTypeCode_tax}


    '    TaxCategory.TaxScheme = TaxScheme
    '    Dim IDC As New IDType
    '    IDC.Value = "S"
    '    IDC.schemeID = "UN/ECE 5305"
    '    IDC.schemeName = "Tax Category Identifier"
    '    IDC.schemeAgencyName = "United Nations Economic Commission for Europe"
    '    TaxCategory.ID = IDC
    '    Dim tax As New PercentType With {.Value = 18.0}
    '    TaxCategory.Percent = tax
    '    'TaxCategory.ID =
    '    If Id = "1000" Then
    '        Dim TaxeCode As New TaxExemptionReasonCodeType With {.Value = TaxExcepcionCode}
    '        'TaxeCode.listAgencyName = "PE:SUNAT"
    '        'TaxeCode.listName = "SUNAT:Codigo de Tipo de AfectaciÃ³n del IGV"
    '        'TaxeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
    '        TaxCategory.TaxExemptionReasonCode = TaxeCode
    '    End If
    '    If Id = "2000" Then
    '        Dim TireRange As New TierRangeType With {.Value = TaxExcepcionCode}
    '        TaxCategory.TierRange = TireRange
    '    End If

    '    Return TaxCategory
    'End Function
    Private Function Get_TaxCategory(ByVal Id As String, Nam_Tax As String, Code_Tax As String, TaxExcepcionCode As String) As Object
        Dim ID_tax As New IDType, Name_tax As New NameType1, TaxTypeCode_tax As New TaxTypeCodeType
        Dim TaxCategory As New TaxCategoryType
        ID_tax.Value = Id ' "1000"
        Name_tax.Value = Nam_Tax '"IGV"
        TaxTypeCode_tax.Value = Code_Tax '"VAT"
        Dim TaxScheme As New TaxSchemeType With {.ID = ID_tax, .Name = Name_tax, .TaxTypeCode = TaxTypeCode_tax}
        TaxCategory.TaxScheme = TaxScheme
        If Id = "1000" Then
            Dim TaxeCode As New TaxExemptionReasonCodeType With {.Value = TaxExcepcionCode}
            TaxCategory.TaxExemptionReasonCode = TaxeCode
        End If
        If Id = "2000" Then
            Dim TireRange As New TierRangeType With {.Value = TaxExcepcionCode}
            TaxCategory.TierRange = TireRange
        End If

        Return TaxCategory
    End Function

    Private Function Tax_Impuesto(ByVal Tax_importe As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, taxExcepCod As String, PercentIGV As Decimal) As TaxTotalType
        Dim TaxTotal1 As TaxTotalType = New TaxTotalType()
        Dim TaxAmaount1 As TaxAmountType = New TaxAmountType()
        Dim TaxSubtotal1(0) As TaxSubtotalType
        TaxAmaount1.currencyID = ls_TipoMoneda
        TaxAmaount1.Value = Tax_importe
        TaxTotal1.TaxAmount = TaxAmaount1
        TaxSubtotal1(0) = Tax_SubImpuesto(Tax_importe, IdTax, Tax_Name, Tax_cod, taxExcepCod, PercentIGV)
        TaxTotal1.TaxSubtotal = TaxSubtotal1
        Return TaxTotal1
    End Function
    Private Function Tax_SubImpuesto(ByVal Tax_SubImporte As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, taxExcepCod As String, PercentIGV As Decimal) As TaxSubtotalType
        Dim TaxSubTotal1 As TaxSubtotalType = New TaxSubtotalType()
        Dim TaxAmount1 As TaxAmountType = New TaxAmountType()
        TaxAmount1.currencyID = ls_TipoMoneda
        TaxAmount1.Value = Tax_SubImporte
        TaxSubTotal1.TaxAmount = TaxAmount1
        TaxSubTotal1.TaxCategory = Get_TaxCategory(IdTax, Tax_Name, Tax_cod, taxExcepCod)
        If PercentIGV <> 0.0 Then
            Dim tax As New PercentType With {.Value = PercentIGV}
            TaxSubTotal1.Percent = tax
        End If
        Return TaxSubTotal1
    End Function


    Private Function Datos_Cli_SEE(ByVal TdSunat As String, ByVal Ruc_Cli As String, razonSocial As String) As Object

        Dim AccountingCustomerParty As New CustomerPartyType, CustomerAssignedAccountID As New CustomerAssignedAccountIDType, AdditionalAccountID As New AdditionalAccountIDType, oParty_Custom As New PartyType()
        Dim oPartyLegalEntity_custom As New PartyLegalEntityType
        Dim Add(0) As AdditionalAccountIDType
        Dim oP_custom(0) As PartyLegalEntityType
        Dim oRegistrationName_custom As New RegistrationNameType
        CustomerAssignedAccountID.Value = Ruc_Cli.Trim  '"20587896411"
        AccountingCustomerParty.CustomerAssignedAccountID = CustomerAssignedAccountID
        AdditionalAccountID.Value = TdSunat.Trim  ' "6"
        Add(0) = AdditionalAccountID
        AccountingCustomerParty.AdditionalAccountID = Add
        oRegistrationName_custom.Value = razonSocial.Trim  '"SERVICABINAS S.A."
        oPartyLegalEntity_custom.RegistrationName = oRegistrationName_custom
        oP_custom(0) = oPartyLegalEntity_custom
        oParty_Custom.PartyLegalEntity = oP_custom
        AccountingCustomerParty.Party = oParty_Custom
        Return AccountingCustomerParty
    End Function
    Private Function Datos_Prov_SEE(ByVal Ubigeo As String, ByVal razonsocial As String, ByVal TdSunat As String, ByVal IdRuc As String, ByVal Direccion As String, ByVal Urbanizacion As String, ciudad As String, provincia As String, distrito As String, CodPais As String, nomComercial As String) As Object
        Dim xml_ruc As New CustomerAssignedAccountIDType
        Dim Supplier As New SupplierPartyType
        Dim it As New AdditionalAccountIDType()
        Dim xml_Td_pro(0) As AdditionalAccountIDType
        xml_ruc.Value = IdRuc.Trim
        Supplier.CustomerAssignedAccountID = xml_ruc
        it.Value = TdSunat.Trim
        xml_Td_pro(0) = it
        Supplier.AdditionalAccountID = xml_Td_pro
        Dim parytype As New PartyType
        parytype.PostalAddress = CType(dir_ter_Fact(Ubigeo, Direccion, Urbanizacion, ciudad, provincia, distrito, CodPais), AddressType)
        parytype.PartyLegalEntity = CType(Legal(razonsocial.Trim), PartyLegalEntityType())
        parytype.PartyName = CType(ComercialName(nomComercial), PartyNameType())
        Supplier.Party = parytype
        Return Supplier
    End Function
    Private Function ComercialName(ByVal NomComercial As String) As PartyNameType()
        Dim PartyName As New PartyNameType
        Dim nombrecomercial As New NameType1
        nombrecomercial.Value = NomComercial
        PartyName.Name = nombrecomercial
        Dim PartyName_1(0) As PartyNameType
        PartyName_1(0) = PartyName
        Return PartyName_1
    End Function
    Private Function DetalleItem(item As Integer, ByVal datos As DataTable) As InvoiceLineType()
        Dim InvoiceItem(item) As InvoiceLineType
        For x As Integer = 0 To item - 1
            With datos
                If .Rows(x).Item("IdFormaVenta").ToString.Trim = "O" Then
                    InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), .Rows(x).Item("UndRef"), CType(.Rows(x).Item("Cantidad"), Decimal).ToString("#.00"), CType(IIf(MonedaDet.Trim = "MN", .Rows(x).Item("ImporteMN"), .Rows(x).Item("ImporteUS")), Decimal).ToString("#.00"), IIf(.Rows(x).Item("IdFormaVenta").ToString.Trim = "O", "02", "01"), CType(.Rows(x).Item("PrecioVenta"), Decimal).ToString("#.00000"), CType(0.00, Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("AfecIGV").ToString.Trim, CType(0.00, Decimal).ToString("#.00"))
                    'InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), .Rows(0).Item("UndRef"), CType(.Rows(x).Item("Cantidad"), Decimal).ToString("#.00"), CType(IIf(MonedaDet.Trim = "MN", .Rows(x).Item("ImporteMN"), .Rows(x).Item("ImporteUS")), Decimal).ToString("#.00"), IIf(.Rows(x).Item("IdFormaVenta").ToString.Trim = "O", "02", "01"), CType(.Rows(x).Item("PrecioVenta"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("AfecIGV").ToString.Trim, CType(0.00, Decimal).ToString("#.00"))
                Else
                    InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), .Rows(x).Item("UndRef"), CType(.Rows(x).Item("Cantidad"), Decimal).ToString("#.00"), CType(IIf(MonedaDet.Trim = "MN", .Rows(x).Item("ImporteMN"), .Rows(x).Item("ImporteUS")), Decimal).ToString("#.00"), IIf(.Rows(x).Item("IdFormaVenta").ToString.Trim = "O", "02", "01"), CType(.Rows(x).Item("PrecioVenta"), Decimal).ToString("#.00000"), CType(0.00, Decimal).ToString("#.00"), CType(.Rows(x).Item("ImporteIGV"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, CType(.Rows(x).Item("PrecioSIGV"), Decimal).ToString("#.00"), .Rows(x).Item("AfecIGV").ToString.Trim, .Rows(x).Item("IGV").ToString.Trim)
                    'InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), .Rows(0).Item("UndRef"), CType(.Rows(x).Item("Cantidad"), Decimal).ToString("#.00"), CType(IIf(MonedaDet.Trim = "MN", .Rows(x).Item("ImporteMN"), .Rows(x).Item("ImporteUS")), Decimal).ToString("#.00"), IIf(.Rows(x).Item("IdFormaVenta").ToString.Trim = "O", "02", "01"), CType(.Rows(x).Item("PrecioVenta"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), CType(.Rows(x).Item("ImporteIGV"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, CType(.Rows(x).Item("PrecioSIGV"), Decimal).ToString("#.00"), .Rows(x).Item("AfecIGV").ToString.Trim, .Rows(x).Item("IGV").ToString.Trim)
                End If
            End With
        Next
        Return InvoiceItem
    End Function
    Private Function DetalleFactura(item As Integer, UnidadMedida As String, cantidad As Decimal, TprecioItem As Decimal, PriceTypeCode As String, PrecioUnit As Decimal, Isc As Decimal, Igv As Decimal, Otros As Decimal, ByVal IdArticulo As String, DescripcionArt As String, precionsinTax As Decimal, taxExcepCod As String, PercentIGV As Decimal) As InvoiceLineType
        Dim InvoiceLine1 As New InvoiceLineType
        Dim InvoicedQuantity1 As New InvoicedQuantityType
        'convirte en un equivalente enum para comprar con la unidad de medida internacional UN/ECE rec 20- Unit Of Measure
        Dim UnidadMed As Integer
        UnidadMed = DirectCast([Enum].Parse(GetType(UnitCodeContentType), UnidadMedida), Integer)
        InvoicedQuantity1.unitCode = UnidadMed

        InvoicedQuantity1.unitCodeSpecified = True
        InvoicedQuantity1.Value = cantidad
        Dim LineExtensionAmount1 As New LineExtensionAmountType
        LineExtensionAmount1.currencyID = ls_TipoMoneda
        LineExtensionAmount1.Value = TprecioItem
        Dim PricingReference1 As New PricingReferenceType
        Dim PricingRef(0) As PricingReferenceType
        Dim pricecode1(1) As PriceType
        If PriceTypeCode = "02" Then
            pricecode1(0) = PrecioUnitario("01", CType(0.00, Decimal).ToString("#.00"))
            'PricingReference1.AlternativeConditionPrice = PrecioUnitario("01", 0.0)
        End If
        pricecode1(1) = PrecioUnitario(PriceTypeCode, PrecioUnit)
        PricingReference1.AlternativeConditionPrice = pricecode1

        Dim Id As New IDType
        Id.Value = item
        InvoiceLine1.ID = Id
        InvoiceLine1.InvoicedQuantity = InvoicedQuantity1
        InvoiceLine1.LineExtensionAmount = LineExtensionAmount1
        InvoiceLine1.PricingReference = PricingReference1

        Dim Tax_pru(2) As TaxTotalType
        If Isc > 0 Then
            Tax_pru(0) = Tax_Impuesto(Isc, "2000", "ISC", "EXC", "01", CType(0.00, Decimal).ToString("#.00"))
        End If
        If Igv >= 0 Then
            Tax_pru(1) = Tax_Impuesto(Igv, "1000", "IGV", "VAT", taxExcepCod, PercentIGV)
        End If
        If Otros > 0 Then
            Tax_pru(2) = Tax_Impuesto(Otros, "9999", "OTROS", "OTH", taxExcepCod, CType(0.00, Decimal).ToString("#.00"))
        End If
        InvoiceLine1.TaxTotal = Tax_pru
        InvoiceLine1.Item = itemarticulo(IdArticulo, DescripcionArt)
        InvoiceLine1.Price = Precio_SinIGv(precionsinTax)
        Return InvoiceLine1
    End Function
    Private Function Precio_SinIGv(ByVal PrecionSinTax As Decimal) As PriceType
        Dim Precio_un As New PriceType
        Dim price_am As New PriceAmountType
        price_am.currencyID = ls_TipoMoneda
        price_am.Value = PrecionSinTax
        Precio_un.PriceAmount = price_am
        Return Precio_un
    End Function
    Private Function itemarticulo(ByVal IdArticulo As String, DescripcionArt As String) As ItemType
        Dim Item1 As New ItemType
        Dim Description1 As New DescriptionType
        Dim Description_o1(0) As DescriptionType
        Description1.Value = DescripcionArt.Trim
        Description_o1(0) = Description1
        Item1.Description = Description_o1
        Dim SellersItemIdentification1 As New ItemIdentificationType
        Dim item_ID_o1 As New IDType
        item_ID_o1.Value = IdArticulo.Trim
        SellersItemIdentification1.ID = item_ID_o1
        Item1.SellersItemIdentification = SellersItemIdentification1
        Return Item1
    End Function
    Private Function PrecioUnitario(ByVal PriceTypeCode As String, precioUnit As Decimal) As PriceType
        Dim AlternativeConditionPrice As New PriceType
        Dim price As New PriceAmountType
        Dim pricecode As New PriceTypeCodeType
        'Dim pricecode1(0) As PriceType
        price.currencyID = ls_TipoMoneda
        price.Value = precioUnit
        pricecode.Value = PriceTypeCode.Trim
        AlternativeConditionPrice.PriceAmount = price
        AlternativeConditionPrice.PriceTypeCode = pricecode
        ' pricecode1(0) = AlternativeConditionPrice
        Return AlternativeConditionPrice 'pricecode1
    End Function
    'Direccion de la Empresa prestadora de servicio
    Private Function dir_ter_Fact(ByVal x_Id As String, x_streetname As String, x_CitySubdivisionName As String, x_CityName As String, x_CountrySubentity As String, x_District As String, x_Cod_pais As String) As Object
        Dim direccion As New AddressType()
        Dim StreetName As New StreetNameType
        Dim CitySubdivisionName As New CitySubdivisionNameType
        Dim CityName As New CityNameType
        Dim ID As New IDType
        Dim CountrySubentity As New CountrySubentityType
        Dim District As New DistrictType
        ID.Value = x_Id
        StreetName.Value = x_streetname.Trim
        CitySubdivisionName.Value = x_CitySubdivisionName.Trim
        CityName.Value = x_CityName.Trim
        CountrySubentity.Value = x_CountrySubentity.Trim
        District.Value = x_District.Trim
        direccion.ID = ID
        direccion.StreetName = StreetName
        direccion.CitySubdivisionName = CitySubdivisionName
        direccion.CityName = CityName
        direccion.CountrySubentity = CountrySubentity
        direccion.District = District
        Dim Country As New CountryType
        Dim Cod_pais As New IdentificationCodeType
        Cod_pais.Value = x_Cod_pais.Trim
        Country.IdentificationCode = Cod_pais
        direccion.Country = Country
        Return direccion
    End Function
    Private Function Legal(ByVal RazonSocial As String) As Object
        Dim RegistrationName As New RegistrationNameType
        RegistrationName.Value = RazonSocial

        Dim Entity As New PartyLegalEntityType()
        Entity.RegistrationName = RegistrationName
        Dim Entity_A(0) As PartyLegalEntityType
        Entity_A(0) = Entity
        Return Entity_A
    End Function

End Class
