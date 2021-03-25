Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.VisualBasic
Imports CapaNegocios

Public Class Cls_NotaDebito
    Dim ls_TipoMoneda As New CurrencyCodeContentType
    Dim monedadet As String = "MN"
    Dim alm As New NAlmacen
    Public Sub Pro_Moneda(ByVal IdMon As String)
        If IdMon = "MN" Then
            ls_TipoMoneda = CurrencyCodeContentType.PEN
        End If
        If IdMon = "US" Then
            ls_TipoMoneda = CurrencyCodeContentType.USD
        End If
        monedadet = IdMon
    End Sub
    Public Function CreatePOFile(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OComprobante As String, OFEmsion As DateTime,
                         OTd As String, Moneda As String, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, ByVal detalle As DataTable)
        Dim serializer As New XmlSerializer(GetType(DebitNoteType))
        '**********
        Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))

        Dim Invoice As New DebitNoteType()
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
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, NC As New IDType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        NC.Value = OComprobante
        Invoice.ID = NC
        '--------- Fecha de Emision del Documento
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha

        '********** Tipo de Moneda PEN o USD
        xml_Mon.Value = Moneda
        Invoice.DocumentCurrencyCode = xml_Mon

        '---------- Nota de credito
        Dim DiscrepancyResponse As New ResponseType
        Dim DiscrepancyResponse1(0) As ResponseType
        Dim descripcionNote As New DescriptionType
        Dim descripcionNote1(1) As DescriptionType
        Dim ReferenciaID As New ReferenceIDType
        Dim ResponseCode As New ResponseCodeType
        descripcionNote.Value = Cabecera.Rows(0).Item("Motivo").ToString.Trim
        ReferenciaID.Value = Cabecera.Rows(0).Item("NumeroDocumento2").ToString.Trim

        ' ResponseCode.Value = OTd.Trim
        ResponseCode.Value = Cabecera.Rows(0).Item("TdNC").ToString.Trim
        descripcionNote1(0) = descripcionNote
        DiscrepancyResponse.Description = descripcionNote1
        DiscrepancyResponse.ReferenceID = ReferenciaID
        DiscrepancyResponse.ResponseCode = ResponseCode
        DiscrepancyResponse1(0) = DiscrepancyResponse
        Invoice.DiscrepancyResponse = DiscrepancyResponse1
        '--------------------Documento de referencia que modifica
        Dim Billreference As New BillingReferenceType
        Dim Billreference1(0) As BillingReferenceType
        Dim invoiceReferen As New DocumentReferenceType
        Dim CodDocumento As New DocumentTypeCodeType
        CodDocumento.Value = Cabecera.Rows(0).Item("TdSunatRef").ToString.Trim
        Factura.Value = Cabecera.Rows(0).Item("NumeroDocumento2").ToString.Trim
        invoiceReferen.ID = Factura
        invoiceReferen.DocumentTypeCode = CodDocumento
        Billreference.InvoiceDocumentReference = invoiceReferen
        Billreference1(0) = Billreference
        Invoice.BillingReference = Billreference1
        '---------------Emisor de facturación electronica
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

        '--------------------------Datos del cliente 
        With Cabecera.Rows(0)
            'select idcliente,cl.RUC,cl.TipoDocSunat,cl.nombre,cl.departamento,pais,cl.Direccion,cl.Distrito,cl.Provincia from cliente cl
            Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("TipoDocSunat"), .Item("Ruc"), .Item("nombreCliente")) 'AccountingCustomerParty

            Dim Tax_pru(2) As TaxTotalType
            '********************** impuestos
            'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
            Tax_pru(0) = Tax_Impuesto(CType(.Item("ImporteIGV"), Decimal).ToString("#.00"), "1000", "IGV", "VAT")
            'Tax_pru(2) = Tax_Impuesto(300, "9999", "OTROS", "OTH")
            Invoice.TaxTotal = Tax_pru

            '******************** Totales Generales
            Dim LegalMonetaryTotal As New MonetaryTotalType
            Dim PayableAmount As New PayableAmountType
            Dim LineExtensionAmount As New LineExtensionAmountType
            Dim TaxExclusiveAmount As New TaxExclusiveAmountType

            ' total general
            PayableAmount.currencyID = ls_TipoMoneda
            PayableAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")
            LegalMonetaryTotal.PayableAmount = PayableAmount
            Invoice.RequestedMonetaryTotal = LegalMonetaryTotal

            Dim ubl As New UBLExtensionType
            Dim ubl_1(1) As UBLExtensionType
            ubl_1(0) = UBLExtensions1x("1001", CType(.Item("ValorTotal"), Decimal).ToString("#.00"), .Item("ImporteLetra"))
            ubl_1(1) = UBLExtensions1x1()
            'ubl_1(2) = UBLExtensions1x("10004", 48357.15)
            Invoice.UBLExtensions = ubl_1
        End With
        '*************** DETALLE DE FACTURAS
        'detalle 
        Invoice.DebitNoteLine = DetalleItem(detalle.Rows.Count, detalle)
        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)

        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Close()
        writer.Close()
    End Function
    Public Function CreatePO(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OComprobante As String, OFEmsion As DateTime,
                         OTd As String, Moneda As String, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, ByVal detalle As DataTable) As Byte()
        Dim serializer As New XmlSerializer(GetType(DebitNoteType))
        '**********
        Dim ms As New MemoryStream
        Dim writer As New StreamWriter(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"))
        '*********
        ' Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))

        Dim Invoice As New DebitNoteType()
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
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, NC As New IDType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        NC.Value = OComprobante
        Invoice.ID = NC
        '--------- Fecha de Emision del Documento
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha

        '********** Tipo de Moneda PEN o USD
        xml_Mon.Value = Moneda
        Invoice.DocumentCurrencyCode = xml_Mon

        '---------- Nota de credito
        Dim DiscrepancyResponse As New ResponseType
        Dim DiscrepancyResponse1(0) As ResponseType
        Dim descripcionNote As New DescriptionType
        Dim descripcionNote1(1) As DescriptionType
        Dim ReferenciaID As New ReferenceIDType
        Dim ResponseCode As New ResponseCodeType
        descripcionNote.Value = Cabecera.Rows(0).Item("Motivo").ToString.Trim
        ReferenciaID.Value = Cabecera.Rows(0).Item("NumeroDocumento2").ToString.Trim

        ' ResponseCode.Value = OTd.Trim
        ResponseCode.Value = Cabecera.Rows(0).Item("TdNC").ToString.Trim
        descripcionNote1(0) = descripcionNote
        DiscrepancyResponse.Description = descripcionNote1
        DiscrepancyResponse.ReferenceID = ReferenciaID
        DiscrepancyResponse.ResponseCode = ResponseCode
        DiscrepancyResponse1(0) = DiscrepancyResponse
        Invoice.DiscrepancyResponse = DiscrepancyResponse1
        '--------------------Documento de referencia que modifica
        Dim Billreference As New BillingReferenceType
        Dim Billreference1(0) As BillingReferenceType
        Dim invoiceReferen As New DocumentReferenceType
        Dim CodDocumento As New DocumentTypeCodeType
        CodDocumento.Value = Cabecera.Rows(0).Item("TdSunatRef").ToString.Trim
        Factura.Value = Cabecera.Rows(0).Item("NumeroDocumento2").ToString.Trim
        invoiceReferen.ID = Factura
        invoiceReferen.DocumentTypeCode = CodDocumento
        Billreference.InvoiceDocumentReference = invoiceReferen
        Billreference1(0) = Billreference
        Invoice.BillingReference = Billreference1
        '---------------Emisor de facturación electronica
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

        '--------------------------Datos del cliente 
        With Cabecera.Rows(0)
            'select idcliente,cl.RUC,cl.TipoDocSunat,cl.nombre,cl.departamento,pais,cl.Direccion,cl.Distrito,cl.Provincia from cliente cl
            Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("TipoDocSunat"), .Item("Ruc"), .Item("nombreCliente")) 'AccountingCustomerParty

            Dim Tax_pru(2) As TaxTotalType
            '********************** impuestos
            'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
            Tax_pru(0) = Tax_Impuesto(CType(.Item("ImporteIGV"), Decimal).ToString("#.00"), "1000", "IGV", "VAT")
            'Tax_pru(2) = Tax_Impuesto(300, "9999", "OTROS", "OTH")
            Invoice.TaxTotal = Tax_pru

            '******************** Totales Generales
            Dim LegalMonetaryTotal As New MonetaryTotalType
            Dim PayableAmount As New PayableAmountType
            Dim LineExtensionAmount As New LineExtensionAmountType
            Dim TaxExclusiveAmount As New TaxExclusiveAmountType

            ' total general
            PayableAmount.currencyID = ls_TipoMoneda
            PayableAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")
            LegalMonetaryTotal.PayableAmount = PayableAmount
            Invoice.RequestedMonetaryTotal = LegalMonetaryTotal

            Dim ubl As New UBLExtensionType
            Dim ubl_1(1) As UBLExtensionType
            ubl_1(0) = UBLExtensions1x("1001", CType(.Item("ValorTotal"), Decimal).ToString("#.00"), .Item("ImporteLetra"))
            ubl_1(1) = UBLExtensions1x1()
            'ubl_1(2) = UBLExtensions1x("10004", 48357.15)
            Invoice.UBLExtensions = ubl_1
        End With
        '*************** DETALLE DE FACTURAS
        'detalle 
        Invoice.DebitNoteLine = DetalleItem(detalle.Rows.Count, detalle)
        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)

        'serializer.Serialize(xwriter, Invoice, myNamespaces)
        'xwriter.Close()
        'writer.Close()

        xwriter.Formatting = Formatting.Indented
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Flush()
        xwriter.Close()
        ' writer.Flush()
        writer.Close()

        Return ms.ToArray

    End Function
    Private Function UBLExtensions1x(ByVal Codigo As String, importe As Decimal, ByVal MontoLetra As String) As UBLExtensionType
        Dim UBLExtension1 As New UBLExtensionType
        Dim ExtensionContent1 As New ExtensionContentType
        Dim ExtensionContent_1(0) As ExtensionContentType
        ExtensionContent1.AdditionalInformation = informacionAddicional(Codigo, importe, MontoLetra)
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

    Private Function informacionAddicional(ByVal ID_ As String, PayableAmount_ As Decimal, ByVal MontoLetra As String) As AdditionalInformationType1()
        Dim AdditionalInformation1 As New AdditionalInformationType1
        Dim AdditionalInformation_1(0) As AdditionalInformationType1
        Dim AdditionalMonetaryTotal_1(2) As AdditionalMonetaryTotalType
        Dim AdditionalProperty(0) As AdditionalPropertyType
        AdditionalMonetaryTotal_1(0) = MontoGenerales(ID_, PayableAmount_)
        'AdditionalMonetaryTotal_1(1) = MontoGenerales(ID_, PayableAmount_ * 2)
        'AdditionalMonetaryTotal_1(2) = MontoGenerales(ID_, PayableAmount_ * -3)
        AdditionalInformation1.AdditionalMonetaryTotal = AdditionalMonetaryTotal_1
        AdditionalProperty(0) = MontoLetras("1000", MontoLetra)
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
    Private Function Get_TaxCategory(ByVal Id As String, Nam_Tax As String, Code_Tax As String) As Object
        Dim ID_tax As New IDType, Name_tax As New NameType1, TaxTypeCode_tax As New TaxTypeCodeType
        Dim TaxCategory As New TaxCategoryType
        ID_tax.Value = Id ' "1000"
        Name_tax.Value = Nam_Tax '"IGV"
        TaxTypeCode_tax.Value = Code_Tax '"VAT"
        Dim TaxScheme As New TaxSchemeType With {.ID = ID_tax, .Name = Name_tax, .TaxTypeCode = TaxTypeCode_tax}
        TaxCategory.TaxScheme = TaxScheme
        Dim TaxeCode As New TaxExemptionReasonCodeType With {.Value = "10"}
        TaxCategory.TaxExemptionReasonCode = TaxeCode
        Return TaxCategory
    End Function
    Private Function Tax_Impuesto(ByVal Tax_importe As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String) As TaxTotalType
        Dim TaxTotal1 As TaxTotalType = New TaxTotalType()
        Dim TaxAmaount1 As TaxAmountType = New TaxAmountType()
        Dim TaxSubtotal1(0) As TaxSubtotalType
        TaxAmaount1.currencyID = ls_TipoMoneda
        TaxAmaount1.Value = Tax_importe
        TaxTotal1.TaxAmount = TaxAmaount1
        TaxSubtotal1(0) = Tax_SubImpuesto(Tax_importe, IdTax, Tax_Name, Tax_cod)
        TaxTotal1.TaxSubtotal = TaxSubtotal1
        Return TaxTotal1
    End Function
    Private Function Tax_SubImpuesto(ByVal Tax_SubImporte As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String) As TaxSubtotalType
        Dim TaxSubTotal1 As TaxSubtotalType = New TaxSubtotalType()
        Dim TaxAmount1 As TaxAmountType = New TaxAmountType()
        TaxAmount1.currencyID = ls_TipoMoneda
        TaxAmount1.Value = Tax_SubImporte
        TaxSubTotal1.TaxAmount = TaxAmount1
        TaxSubTotal1.TaxCategory = Get_TaxCategory(IdTax, Tax_Name, Tax_cod)
        Dim tax As New PercentType With {.Value = 18.0}
        TaxSubTotal1.Percent = tax
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

    Private Function DetalleItem(item As Integer, ByVal datos As DataTable) As DebitNoteLineType()
        Dim InvoiceItem(item) As DebitNoteLineType
        For x As Integer = 0 To item - 1
            With datos
                ' InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), .Rows(x).Item("Cantidad"), .Rows(x).Item("ImporteMN"), "01", .Rows(x).Item("PrecioVenta"), 0, .Rows(x).Item("ImporteIGV"), 0, .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, .Rows(x).Item("PrecioSIGV"))
                InvoiceItem(x) = DetalleFactura(.Rows(x).Item("Item"), CType(.Rows(x).Item("Cantidad"), Decimal).ToString("#.00"), CType(IIf(monedadet.Trim = "MN", .Rows(x).Item("ImporteMN"), .Rows(x).Item("ImporteUS")), Decimal).ToString("#.00"), "01", CType(.Rows(x).Item("PrecioVenta"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), CType(.Rows(x).Item("ImporteIGV"), Decimal).ToString("#.00"), CType(0.00, Decimal).ToString("#.00"), .Rows(x).Item("IdArticulo").ToString.Trim, .Rows(x).Item("Descripcion").ToString.Trim, CType(.Rows(x).Item("PrecioSIGV"), Decimal).ToString("#.00"))
            End With
        Next
        Return InvoiceItem
    End Function
    Private Function DetalleFactura(item As Integer, cantidad As Decimal, TprecioItem As Decimal, PriceTypeCode As String, PrecioUnit As Decimal, Isc As Decimal, Igv As Decimal, Otros As Decimal, ByVal IdArticulo As String, DescripcionArt As String, precionsinTax As Decimal) As DebitNoteLineType
        Dim InvoiceLine1 As New DebitNoteLineType
        Dim InvoicedQuantity1 As New DebitedQuantityType
        InvoicedQuantity1.unitCode = UnitCodeContentType.NIU
        InvoicedQuantity1.unitCodeSpecified = True
        InvoicedQuantity1.Value = cantidad
        Dim LineExtensionAmount1 As New LineExtensionAmountType
        LineExtensionAmount1.currencyID = ls_TipoMoneda
        LineExtensionAmount1.Value = TprecioItem
        Dim PricingReference1 As New PricingReferenceType
        Dim PricingRef(0) As PricingReferenceType
        PricingReference1.AlternativeConditionPrice = PrecioUnitario(PriceTypeCode, PrecioUnit)
        Dim Id As New IDType
        Id.Value = item
        InvoiceLine1.ID = Id
        InvoiceLine1.DebitedQuantity = InvoicedQuantity1
        InvoiceLine1.LineExtensionAmount = LineExtensionAmount1
        InvoiceLine1.PricingReference = PricingReference1

        Dim Tax_pru(2) As TaxTotalType
        If Isc > 0 Then
            Tax_pru(0) = Tax_Impuesto(Isc, "2000", "ISC", "EXC")
        End If
        If Igv > 0 Then
            Tax_pru(1) = Tax_Impuesto(Igv, "1000", "IGV", "VAT")
        End If
        If Otros > 0 Then
            Tax_pru(2) = Tax_Impuesto(Otros, "9999", "OTROS", "OTH")
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
    Private Function PrecioUnitario(ByVal PriceTypeCode As String, precioUnit As Decimal) As PriceType()
        Dim AlternativeConditionPrice As New PriceType
        Dim price As New PriceAmountType
        Dim pricecode As New PriceTypeCodeType
        Dim pricecode1(0) As PriceType
        price.currencyID = ls_TipoMoneda
        price.Value = precioUnit
        pricecode.Value = PriceTypeCode.Trim
        AlternativeConditionPrice.PriceAmount = price
        AlternativeConditionPrice.PriceTypeCode = pricecode
        pricecode1(0) = AlternativeConditionPrice
        Return pricecode1
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
