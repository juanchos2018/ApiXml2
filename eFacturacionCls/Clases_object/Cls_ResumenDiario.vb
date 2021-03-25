Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.VisualBasic
Public Class Cls_ResumenDiario
    Dim ls_TipoMoneda As New CurrencyCodeContentType
    Public Sub Pro_Moneda(ByVal IdMon As String)
        If IdMon = "MN" Then
            ls_TipoMoneda = CurrencyCodeContentType.PEN
        End If
        If IdMon = "US" Then
            ls_TipoMoneda = CurrencyCodeContentType.USD
        End If
    End Sub
    Public Sub CreatePOFile(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OFEmsion As DateTime, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, nrobo As String)

        Dim serializer As New XmlSerializer(GetType(SummaryDocumentsType))

        '**********
        Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))


        Dim Invoice As New SummaryDocumentsType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("ns11", "urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1")
        myNamespaces.Add("ns12", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
        myNamespaces.Add("ns13", "urn:sunat:names:specification:ubl:peru:schema:xsd:Retention-1")
        myNamespaces.Add("ns14", "urn:sunat:names:specification:ubl:peru:schema:xsd:Perception-1")
        myNamespaces.Add("ns6", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
        myNamespaces.Add("ns7", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
        myNamespaces.Add("ns8", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2")
        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, NC As New IDType, FechaRef As New ReferenceDateType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        '    NC.Value = "RC-" & Format(Now.Date, "yyyyMMdd") & "-001" 'OComprobante
        NC.Value = nrobo
        Invoice.ID = NC
        '--------- Fecha de generación del resumen
        'fecha.Value = Now.Date
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha
        '-------------- fecha de emision de los documentos
        FechaRef.Value = OFEmsion   ' cambiar 
        Invoice.ReferenceDate = FechaRef
        '---------------Emisor de facturación electronica
        With DatosEE.Rows(0)
            '----------------- DATOS DE FIRMA
            Invoice.Signature = Firma(ls_IdSing, Ruc, .Item("Nombre"), "#" & ls_IdSing)
            Invoice.AccountingSupplierParty = Datos_Prov_SEE(.Item("Nombre"), "6", Ruc)
        End With

        '--------------------------Datos del cliente 
        Dim Tax_pru(2) As TaxTotalType

        '********************** impuestos
        'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
        Dim SumIgv As Decimal = 0
        Dim SumValor As Decimal = 0
        For Each Drow As DataRow In Cabecera.Rows
            SumIgv = SumIgv + Drow.Item("IGV")
            SumValor = SumValor + Drow.Item("ValorVenta")

        Next
        '******************** Totales Generales
        Dim ubl As New UBLExtensionType
        Dim ubl_1(1) As UBLExtensionType
        ubl_1(0) = UBLExtensions1x1()
        Invoice.UBLExtensions = ubl_1
        Invoice.SummaryDocumentsLine = DetalleItem(Cabecera.Rows.Count, Cabecera)

        '*************** DETALLE DE FACTURAS
        'detalle 

        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Close()
        writer.Close()

    End Sub
    Public Function CreatePO(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OFEmsion As DateTime, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, nrobo As String) As Byte()

        Dim serializer As New XmlSerializer(GetType(SummaryDocumentsType))

        '**********
        Dim ms As New MemoryStream
        Dim writer As New StreamWriter(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"))
        '*********
        'Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))


        Dim Invoice As New SummaryDocumentsType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("ns11", "urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1")
        myNamespaces.Add("ns12", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
        myNamespaces.Add("ns13", "urn:sunat:names:specification:ubl:peru:schema:xsd:Retention-1")
        myNamespaces.Add("ns14", "urn:sunat:names:specification:ubl:peru:schema:xsd:Perception-1")
        myNamespaces.Add("ns6", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
        myNamespaces.Add("ns7", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
        myNamespaces.Add("ns8", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2")
        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, NC As New IDType, FechaRef As New ReferenceDateType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        '    NC.Value = "RC-" & Format(Now.Date, "yyyyMMdd") & "-001" 'OComprobante
        NC.Value = nrobo
        Invoice.ID = NC
        '--------- Fecha de generación del resumen
        'fecha.Value = Now.Date
        fecha.Value = OFEmsion
        Invoice.IssueDate = fecha
        '-------------- fecha de emision de los documentos
        FechaRef.Value = OFEmsion   ' cambiar 
        Invoice.ReferenceDate = FechaRef
        '---------------Emisor de facturación electronica
        With DatosEE.Rows(0)
            '----------------- DATOS DE FIRMA
            Invoice.Signature = Firma(ls_IdSing, Ruc, .Item("Nombre"), "#" & ls_IdSing)
            Invoice.AccountingSupplierParty = Datos_Prov_SEE(.Item("Nombre"), "6", Ruc)
        End With

        '--------------------------Datos del cliente 
        Dim Tax_pru(2) As TaxTotalType

        '********************** impuestos
        'Tax_pru(0) = Tax_Impuesto(100, "2000", "ISC", "EXC")
        Dim SumIgv As Decimal = 0
        Dim SumValor As Decimal = 0
        For Each Drow As DataRow In Cabecera.Rows
            SumIgv = SumIgv + Drow.Item("IGV")
            SumValor = SumValor + Drow.Item("ValorVenta")

        Next
        '******************** Totales Generales
        Dim ubl As New UBLExtensionType
        Dim ubl_1(1) As UBLExtensionType
        ubl_1(0) = UBLExtensions1x1()
        Invoice.UBLExtensions = ubl_1
        Invoice.SummaryDocumentsLine = DetalleItem(Cabecera.Rows.Count, Cabecera)

        '*************** DETALLE DE FACTURAS
        'detalle 

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
        '   writer.Flush()
        writer.Close()

        Return ms.ToArray
    End Function
    Private Function UBLExtensions1x(ByVal Codigo As String, importe As Decimal, ByVal MontoLetra As String, idmoneda As CurrencyCodeContentType) As UBLExtensionType
        Dim UBLExtension1 As New UBLExtensionType
        Dim ExtensionContent1 As New ExtensionContentType
        Dim ExtensionContent_1(0) As ExtensionContentType
        ExtensionContent1.AdditionalInformation = informacionAddicional(Codigo, importe, MontoLetra, idmoneda)
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

    Private Function informacionAddicional(ByVal ID_ As String, PayableAmount_ As Decimal, ByVal MontoLetra As String, idmoneda As CurrencyCodeContentType) As AdditionalInformationType1()
        Dim AdditionalInformation1 As New AdditionalInformationType1
        Dim AdditionalInformation_1(0) As AdditionalInformationType1
        Dim AdditionalMonetaryTotal_1(2) As AdditionalMonetaryTotalType
        Dim AdditionalProperty(0) As AdditionalPropertyType
        AdditionalMonetaryTotal_1(0) = MontoGenerales(ID_, PayableAmount_, idmoneda)
        AdditionalInformation1.AdditionalMonetaryTotal = AdditionalMonetaryTotal_1
        AdditionalProperty(0) = MontoLetras("1000", MontoLetra)
        AdditionalInformation1.AdditionalProperty = AdditionalProperty
        AdditionalInformation_1(0) = AdditionalInformation1
        Return AdditionalInformation_1

    End Function
    Private Function MontoGenerales(ByVal ID_ As String, PayableAmount_ As Decimal, idmoneda As CurrencyCodeContentType) As AdditionalMonetaryTotalType
        Dim AdditionalMonetaryTotal1 As New AdditionalMonetaryTotalType
        Dim ID As New IDType
        Dim PayableAmount As New PayableAmountType
        PayableAmount.currencyID = idmoneda
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
    Private Function Get_TaxCategory(ByVal Id As String, Nam_Tax As String, Code_Tax As String, showTaxExcep As Boolean, TaxExcepCod As String) As Object
        Dim ID_tax As New IDType, Name_tax As New NameType1, TaxTypeCode_tax As New TaxTypeCodeType
        Dim TaxCategory As New TaxCategoryType
        ID_tax.Value = Id ' "1000"
        Name_tax.Value = Nam_Tax '"IGV"
        TaxTypeCode_tax.Value = Code_Tax '"VAT"
        Dim TaxScheme As New TaxSchemeType With {.ID = ID_tax, .Name = Name_tax, .TaxTypeCode = TaxTypeCode_tax}
        TaxCategory.TaxScheme = TaxScheme
        If showTaxExcep = True Then
            Dim TaxeCode As New TaxExemptionReasonCodeType With {.Value = TaxExcepCod}
            TaxCategory.TaxExemptionReasonCode = TaxeCode
        End If
        Return TaxCategory
    End Function
    Private Function Tax_Impuesto(ByVal Tax_importe As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, ShowPercent As Boolean, PercentTasa As Decimal, idmoneda As CurrencyCodeContentType) As TaxTotalType
        Dim TaxTotal1 As TaxTotalType = New TaxTotalType()
        Dim TaxAmaount1 As TaxAmountType = New TaxAmountType()
        Dim TaxSubtotal1(0) As TaxSubtotalType
        TaxAmaount1.currencyID = idmoneda
        TaxAmaount1.Value = Tax_importe
        TaxTotal1.TaxAmount = TaxAmaount1
        TaxSubtotal1(0) = Tax_SubImpuesto(Tax_importe, IdTax, Tax_Name, Tax_cod, ShowPercent, PercentTasa, idmoneda)
        TaxTotal1.TaxSubtotal = TaxSubtotal1
        Return TaxTotal1
    End Function
    Private Function Tax_SubImpuesto(ByVal Tax_SubImporte As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, ShowPercent As Boolean, PercentTasa As Decimal, idmoneda As CurrencyCodeContentType) As TaxSubtotalType
        Dim TaxSubTotal1 As TaxSubtotalType = New TaxSubtotalType()
        Dim TaxAmount1 As TaxAmountType = New TaxAmountType()
        TaxAmount1.currencyID = idmoneda
        TaxAmount1.Value = Tax_SubImporte
        TaxSubTotal1.TaxAmount = TaxAmount1
        TaxSubTotal1.TaxCategory = Get_TaxCategory(IdTax, Tax_Name, Tax_cod, False, 0)
        If ShowPercent = True Then
            Dim tax As New PercentType With {.Value = PercentTasa}
            TaxSubTotal1.Percent = tax
        End If
        Return TaxSubTotal1
    End Function
    Private Function Datos_Prov_SEE(ByVal razonsocial As String, ByVal TdSunat As String, ByVal IdRuc As String) As Object
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
        parytype.PartyLegalEntity = CType(Legal(razonsocial.Trim), PartyLegalEntityType())
        Supplier.Party = parytype
        Return Supplier
    End Function

    Private Function DetalleItem(item As Integer, ByVal datos As DataTable) As SummaryDocumentsLineType()
        Dim InvoiceItem(item) As SummaryDocumentsLineType
        For x As Integer = 0 To item - 1
            With datos
                InvoiceItem(x) = DetalleFactura(x + 1, .Rows(x).Item("TdSunat").ToString.Trim,
                                                .Rows(x).Item("Serie").ToString.Trim,
                                                .Rows(x).Item("NroInicial").ToString.Trim,
                                                .Rows(x).Item("NroFinal").ToString.Trim,
                                                CType(.Rows(x).Item("Total"), Decimal).ToString("#.00"),
                                                CType(0.0, Decimal).ToString("#.00"),
                                                CType(.Rows(x).Item("IGV"), Decimal).ToString("#.00"),
                                                CType(0.0, Decimal).ToString("#.00"),
                                                CType(.Rows(x).Item("ValorVenta"), Decimal).ToString("#.00"),
                                                CType(.Rows(x).Item("ValorExo"), Decimal).ToString("#.00"),
                                                CType(.Rows(x).Item("Inafecto"), Decimal).ToString("#.00"),
                                                CType(0.0, Decimal).ToString("#.00"), True,
                                                .Rows(x).Item("IdMoneda").ToString.Trim)
            End With
        Next
        Return InvoiceItem
    End Function
    Private Function DetalleFactura(Linea As String, TipoDoc As String, Serie As String, NroI As String, NroF As String, TotalG As Decimal,
                                    Isc As Decimal, Igv As Decimal, Otros As Decimal, OpGrav As Decimal, OpEx As Decimal, OpInf As Decimal,
                                    otrosCargos As Decimal, istrue As Boolean, idmoneda As String) As SummaryDocumentsLineType
        Dim InvoiceLine1 As New SummaryDocumentsLineType
        Dim TipoDocumento As New DocumentTypeCodeType
        Dim LineId As New LineIDType
        LineId.Value = Linea
        TipoDocumento.Value = TipoDoc
        InvoiceLine1.LineID = LineId
        InvoiceLine1.DocumentTypeCode = TipoDocumento
        Dim SerieDoc As New IdentifierType
        SerieDoc.Value = Serie
        InvoiceLine1.DocumentSerialID = SerieDoc

        Dim NroInicial As New IdentifierType
        NroInicial.Value = NroI
        InvoiceLine1.StartDocumentNumberID = NroInicial
        Dim NroFinal As New IdentifierType
        NroFinal.Value = NroF
        InvoiceLine1.EndDocumentNumberID = NroFinal
        Dim Total As New AmountType1
        Total.Value = TotalG
        Dim Moneda As Integer
        Moneda = DirectCast([Enum].Parse(GetType(CurrencyCodeContentType), idmoneda), Integer)

        Total.currencyID = Moneda
        InvoiceLine1.TotalAmount = Total
        '**** modificado
        Dim bill1(3) As PaymentType
        If OpGrav <> 0 Then
            bill1(0) = Billin(OpGrav, "01", Moneda)
        End If

        If OpEx <> 0 Then
            bill1(1) = Billin(OpEx, "02", Moneda)
        End If

        If OpInf <> 0 Then
            bill1(2) = Billin(OpInf, "03", Moneda)
        End If

        ' final modificacion

        InvoiceLine1.BillingPayment = bill1

        Dim Allowance1(0) As AllowanceChargeType
        If otrosCargos <> 0 Then
            Allowance1(0) = SumOtroscargos(otrosCargos, istrue, Moneda)
            InvoiceLine1.AllowanceCharge = Allowance1
        End If
        Dim Tax_pru(2) As TaxTotalType
        Tax_pru(0) = Tax_Impuesto(Isc, "2000", "ISC", "EXC", False, 0, Moneda)
        Tax_pru(1) = Tax_Impuesto(Igv, "1000", "IGV", "VAT", False, 0, Moneda)
        Tax_pru(2) = Tax_Impuesto(Otros, "9999", "OTROS", "OTH", False, 0, Moneda)
        InvoiceLine1.TaxTotal = Tax_pru
        Return InvoiceLine1
    End Function
    Private Function SumOtroscargos(ByVal valor As Decimal, indicador As Boolean, idmoneda As CurrencyCodeContentType) As AllowanceChargeType
        Dim Allowance As New AllowanceChargeType
        Dim Indicator As New ChargeIndicatorType
        Dim amount As New AmountType1
        Indicator.Value = indicador
        amount.Value = valor
        amount.currencyID = idmoneda
        Allowance.ChargeIndicator = Indicator
        Allowance.Amount = amount
        Return Allowance
    End Function
    Private Function Billin(ByVal paidAmount As Decimal, ByVal InstruccID As String, idmoneda As CurrencyCodeContentType) As PaymentType
        Dim Bill As New PaymentType()
        Dim paint As New PaidAmountType
        paint.Value = paidAmount
        paint.currencyID = idmoneda
        Dim intruc As New InstructionIDType
        intruc.Value = InstruccID
        Bill.PaidAmount = paint
        Bill.InstructionID = intruc
        Return Bill
    End Function
    Private Function Precio_SinIGv(ByVal PrecionSinTax As Decimal, idmoneda As CurrencyCodeContentType) As PriceType
        Dim Precio_un As New PriceType
        Dim price_am As New PriceAmountType
        price_am.currencyID = idmoneda
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
    Private Function PrecioUnitario(ByVal PriceTypeCode As String, precioUnit As Decimal, idmoneda As CurrencyCodeContentType) As PriceType()
        Dim AlternativeConditionPrice As New PriceType
        Dim price As New PriceAmountType
        Dim pricecode As New PriceTypeCodeType
        Dim pricecode1(0) As PriceType
        price.currencyID = idmoneda
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
        ' Dim cdata As XCData = <![CDATA[Can contain literal <XML> tags]]>
        
        RegistrationName.Value = RazonSocial

        '  RegistrationName.Value = GetCData(cdata)
        'RegistrationName.Value = GetCData("INTELIGENCIA DE VENTAS SAC")
        'Dim dato As New NameType
        ' dato = GetCData("<![CDATA[INTELIGENCIA DE VENTAS SAC]]>")
        'RegistrationName.Value = dato.Value

        Dim Entity As New PartyLegalEntityType()
        Entity.RegistrationName = RegistrationName
        Dim Entity_A(0) As PartyLegalEntityType
        Entity_A(0) = Entity
        Return Entity_A
    End Function
   
    'Private Function GetCData(ByVal value As String) As XmlCDataSection
    '    Static doc As New XmlDataDocument()
    '    Static cdata As XmlCDataSection = doc.CreateCDataSection(value)

    '    Return (cdata)
    'End Function
End Class
