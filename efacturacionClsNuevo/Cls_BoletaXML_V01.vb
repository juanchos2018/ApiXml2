Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.VisualBasic
Imports CapaNegocios
Public Class Cls_BoletaXML_V01
    Dim alm As New NAlmacen
    Dim nu As New NNumeracion
    Dim ls_TipoMoneda As New CurrencyCodeContentType
    Dim MonedaDet As String = "MN"
    Public Sub Pro_Moneda(ByVal IdMon As String)
        If IdMon = "MN" Then
            ls_TipoMoneda = CurrencyCodeContentType.PEN
        End If
        If IdMon = "US" Then
            ls_TipoMoneda = CurrencyCodeContentType.USD
        End If
        MonedaDet = IdMon
    End Sub
    Private _UBLVersionID As New UBLVersionIDType
    Private _CustomizationID As New CustomizationIDType
    Private _IssueDate As New IssueDateType
    Private _InvoiceTypeCode As New InvoiceTypeCodeType
    Private _DocumentCurrencyCode As New DocumentCurrencyCodeType

    Public Function CreateInvoice(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OComprobante As String, OFEmsion As DateTime,
                         OTd As String, Moneda As String, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, ByVal detalle As DataTable) As Byte()
        Dim serializer As New XmlSerializer(GetType(InvoiceType))
        Dim ms As New MemoryStream
        Dim writer As New StreamWriter(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"))
        Dim Invoice As New InvoiceType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")

        'rn:un:unece:uncefact:data:specification:CoreComponentTypeSchemaModule:2
        UBLVersionID.Value = Oversion

        Dim pfid As New ProfileIDType
        pfid.Value = Cabecera.Rows(0).Item("Cod_Tip_Factura")    'Tabla 51 nuevo tag  implementar en formulario
        Invoice.ProfileID = pfid
        Invoice.ProfileID.schemeName = "Tipo de Operacion"
        Invoice.ProfileID.schemeAgencyName = "PE:SUNAT"
        Invoice.ProfileID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo17"


        Invoice.UBLVersionID = UBLVersionID
        CustomizationID.Value = OCustomId
        Invoice.CustomizationID = CustomizationID

        '********** Tipo Documento + Nro Comprobante
        Dim TipoNro As New IDType
        TipoNro.Value = OComprobante
        Invoice.ID = TipoNro
        '********** Fecha de Emision del Documento
        IssueDate.Value = OFEmsion
        Invoice.IssueDate = IssueDate
        'Invoice.IssueTime.Value = Format(Now, "hh:mm:ss.sss")
        'Invoice.DueDate = fecha 
        '********** Codigo del Tipo De Documento según Sunat
        InvoiceTypeCode.Value = OTd
        InvoiceTypeCode.listAgencyName = "PE:SUNAT"
        InvoiceTypeCode.listName = "Tipo de Documento"
        InvoiceTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01"
        InvoiceTypeCode.listID = Cabecera.Rows(0).Item("Cod_Tip_Factura")
        Invoice.InvoiceTypeCode = InvoiceTypeCode

        '
        Dim LetraArr(1) As NoteType
        Dim Letra As New NoteType
        Dim y As New NoteType
        Dim Itinerante As New NoteType
        Dim ZofraTacna As New NoteType
        nu.idtipodocumento = Cabecera.Rows(0).Item("IdTipoDocumento")
        nu.serie = Cabecera.Rows(0).Item("Serie")
        nu = nu.Registro(nu)


        Letra.languageLocaleID = "1000"
        Letra.Value = Cabecera.Rows(0).Item("ImporteLetra") '"CUATROCIENTOS VEINTITRES MIL DOSCIENTOS VEINTICINCO Y 00/100"
        LetraArr(0) = Letra
        If nu.esitinerante = True Then
            Itinerante.languageLocaleID = "2005"
            Itinerante.Value = "Venta realizada por emisor itinerante"
            LetraArr(2) = Itinerante
        End If
        If nu.eszofratacna = True Then
            ZofraTacna.languageLocaleID = "2008"
            'ZofraTacna.languageLocaleID = "7001"
            ZofraTacna.Value = "Venta exonerada del IGV-ISC-IPM. Prohibida la venta fuera de la zona comercial de Tacna"
            LetraArr(2) = ZofraTacna
        End If

        Invoice.Note = LetraArr

        '********** Tipo de Moneda PEN o USD
        DocumentCurrencyCode.Value = Moneda
        DocumentCurrencyCode.listID = "ISO 4217 Alpha"
        DocumentCurrencyCode.listName = "Currency"
        DocumentCurrencyCode.listAgencyName = "United Nations Economic Commission for Europe"
        Invoice.DocumentCurrencyCode = DocumentCurrencyCode

        Dim totalline As New LineCountNumericType
        totalline.Value = detalle.Rows.Count
        Invoice.LineCountNumeric = totalline

        Dim OrderReference As New OrderReferenceType
        Dim order_id As New IDType


        If Cabecera.Rows(0).Item("NumeroOrden").ToString.Trim <> "" Then
            order_id.Value = Cabecera.Rows(0).Item("NumeroOrden")
            OrderReference.ID = order_id
            Invoice.OrderReference = OrderReference
        End If
        '********************** Emisor de facturación electronica
        With DatosEE.Rows(0)
            'esAlmacen
            Dim codestsunat As String, ubigeo As String, Direccion As String, Departamento As String, Distrito As String, provincia As String = ""
            '************* DATOS DE FIRMA
            '"IDSignSP""#SignatureSP"
            alm.idalmacen = Cabecera.Rows(0).Item("IdAlmacen")
            alm = alm.Registro(alm)
            If IsNothing(alm.ubigeo) = False Then
                ubigeo = alm.ubigeo
                Direccion = alm.direccion
                Departamento = alm.departamento
                Distrito = alm.distrito
                provincia = alm.provincia
                codestsunat = alm.CodEstableSunat
            Else
                ubigeo = .Item("CodUbigeo")
                Direccion = .Item("Direccion")
                Departamento = .Item("Departamento")
                provincia = .Item("Provincia")
                Distrito = .Item("Distrito")
                codestsunat = "0000"
            End If

            '************* DATOS DE FIRMA
            Invoice.Signature = Firma(.Item("SignAlias"), Ruc, .Item("Nombre"), "#" & .Item("SignAlias"))
            Invoice.AccountingSupplierParty = Datos_Prov_SEE(ubigeo, .Item("Nombre"), .Item("IdTipoDocumento").ToString.Trim, Ruc, Direccion, "", Departamento, provincia, Distrito, "PE", .Item("NombreComercial"), codestsunat)
        End With
        '********************** Datos del cliente 
        With Cabecera.Rows(0)

            If .Item("TipoDocSunat").ToString.Trim = "0" Then
                Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("Ubigeo"), .Item("nombreCliente"), "-", "-", .Item("Direccion"), "", .Item("Departamento"), .Item("Provincia"), .Item("Distrito"), "PE", .Item("NombreComercial"))
            Else
                Invoice.AccountingCustomerParty = Datos_Cli_SEE(.Item("Ubigeo"), .Item("nombreCliente"), .Item("TipoDocSunat").ToString.Trim, .Item("Ruc").ToString.Trim, .Item("Direccion"), "", .Item("Departamento"), .Item("Provincia"), .Item("Distrito"), "PE", .Item("NombreComercial"))
            End If
            ' ********* DESCUENTO GLOBAL *************
            Dim AllowanceCharge As New AllowanceChargeType
            Dim ChargeIndicator As New ChargeIndicatorType
            ChargeIndicator.Value = False
            AllowanceCharge.ChargeIndicator = ChargeIndicator
            Dim AllowanceChargeReasonCode As New AllowanceChargeReasonCodeType1
            AllowanceChargeReasonCode.Value = "00"

            Dim MultiplierFactorNumeric As New MultiplierFactorNumericType
            MultiplierFactorNumeric.Value = CType(0.00, Decimal).ToString("#.00")
            Dim Amount As New AmountType1
            Amount.Value = CType(0.00, Decimal).ToString("#.00")
            Amount.currencyID = ls_TipoMoneda
            Dim BaseAmount As New BaseAmountType
            BaseAmount.Value = CType(.Item("ValorTotal"), Decimal).ToString("#.00")
            BaseAmount.currencyID = ls_TipoMoneda

            AllowanceCharge.AllowanceChargeReasonCode = AllowanceChargeReasonCode
            AllowanceCharge.MultiplierFactorNumeric = MultiplierFactorNumeric
            AllowanceCharge.Amount = Amount
            AllowanceCharge.BaseAmount = BaseAmount

            Invoice.AllowanceCharge = {AllowanceCharge}
            ' ********* END DESCUENTO GLOBAL *************

            Dim impuestos(1) As TaxTotalType
            impuestos(0) = Tax_Totales(Cabecera, False)
            Invoice.TaxTotal = impuestos

            '***************** END TOTALES IMPUESTOS

            Dim FormaVenta As New PaymentTermsType
            Dim FormaVenta1(1) As PaymentTermsType
            Dim Formvta As New NoteType
            Dim Formvta1(1) As NoteType
            Formvta.Value = .Item("Formaventa").ToString.Trim
            Formvta1(0) = Formvta
            FormaVenta.Note = Formvta1
            FormaVenta1(0) = FormaVenta
            Invoice.PaymentTerms = FormaVenta1

            '******************** TOTALES GENERALES
            Dim LegalMonetaryTotal As New MonetaryTotalType
            Dim LineExtensionAmount As New LineExtensionAmountType  'Total valor de venta
            Dim TaxInclusiveAmount As New TaxInclusiveAmountType    ' Total precio de venta
            Dim AllowanceTotalAmoun As New AllowanceTotalAmountType  ' Total descuento del comprobante
            Dim ChargeTotalAmount As New ChargeTotalAmountType 'Monto total de otros cargos del comprobante
            'Dim PrepaidAmount As New PrepaidAmountType 'Monto total de anticipos del comprobante
            Dim PayableAmount As New PayableAmountType  ' Monto total de cesion en uso -transferencia gratuita
            If .Item("idformaventa").ToString.Trim = "O" Then
                LineExtensionAmount.Value = CType(0.00, Decimal).ToString("#.00")
                TaxInclusiveAmount.Value = CType(0.00, Decimal).ToString("#.00")
                ChargeTotalAmount.Value = CType(0.00, Decimal).ToString("#.00")
                '  PrepaidAmount.Value = CType(0.00, Decimal).ToString("#.00")
                PayableAmount.Value = CType(0.00, Decimal).ToString("#.00")

                LineExtensionAmount.currencyID = ls_TipoMoneda
                TaxInclusiveAmount.currencyID = ls_TipoMoneda
                AllowanceTotalAmoun.currencyID = ls_TipoMoneda
                ChargeTotalAmount.currencyID = ls_TipoMoneda
                '  PrepaidAmount.currencyID = ls_TipoMoneda
                PayableAmount.currencyID = ls_TipoMoneda


                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.TaxInclusiveAmount = TaxInclusiveAmount
                LegalMonetaryTotal.AllowanceTotalAmount = AllowanceTotalAmoun
                LegalMonetaryTotal.ChargeTotalAmount = ChargeTotalAmount
                ' LegalMonetaryTotal.PrepaidAmount = PrepaidAmount
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            Else
                LineExtensionAmount.Value = CType(.Item("ValorTotal"), Decimal).ToString("#.00")
                TaxInclusiveAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")
                AllowanceTotalAmoun.Value = CType(.Item("ImporteDescuento"), Decimal).ToString("#.00")
                ChargeTotalAmount.Value = CType(0.00, Decimal).ToString("#.00")
                '  PrepaidAmount.Value = CType(0.00, Decimal).ToString("#.00")
                PayableAmount.Value = CType(.Item("ImporteTotal"), Decimal).ToString("#.00")

                LineExtensionAmount.currencyID = ls_TipoMoneda
                TaxInclusiveAmount.currencyID = ls_TipoMoneda
                AllowanceTotalAmoun.currencyID = ls_TipoMoneda
                ChargeTotalAmount.currencyID = ls_TipoMoneda
                '  PrepaidAmount.currencyID = ls_TipoMoneda
                PayableAmount.currencyID = ls_TipoMoneda

                LegalMonetaryTotal.LineExtensionAmount = LineExtensionAmount
                LegalMonetaryTotal.TaxInclusiveAmount = TaxInclusiveAmount
                LegalMonetaryTotal.AllowanceTotalAmount = AllowanceTotalAmoun
                LegalMonetaryTotal.ChargeTotalAmount = ChargeTotalAmount
                ' LegalMonetaryTotal.PrepaidAmount = PrepaidAmount
                LegalMonetaryTotal.PayableAmount = PayableAmount
                Invoice.LegalMonetaryTotal = LegalMonetaryTotal
            End If
            '**************** END TOTALES GENERALES

            Dim Adicional As New AdditionalInformationType
            Dim AdicionalItem(0) As AdditionalInformationType
            Dim ubl As New UBLExtensionType
            Dim ubl_1(1) As UBLExtensionType
            'ubl.ExtensionContent = ContentItem
            'ubl_1(0) = ubl
            ubl_1(0) = UBLExtensions()
            'ubl_1(2) = UBLExtensions1x("10004", 48357.15)
            Invoice.UBLExtensions = ubl_1
        End With
        '*************** DETALLE DE FACTURAS
        Invoice.InvoiceLine = DetalleItem(detalle.Rows.Count, detalle)
        '*************** END DETALLE FACTURA

        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Close()
        writer.Close()
        Return ms.ToArray
    End Function

#Region "Atributos"
    Private _UBLExtension As New UBLExtensionType
    Private _ExtensionContent() As ExtensionContentType
    Private _Content As New ExtensionContentType
    Private _Signature() As SignatureType
    Private _Tofirma As New SignatureType
    Private _ID_Firma As New IDType()
    Private _attachment As New AttachmentType
    Private _externalReference As New ExternalReferenceType
    Private _uri As New URIType
    Private _party As New PartyType
    Private _PartyIdentification As New PartyIdentificationType
    Private _partyIdentificationlist() As PartyIdentificationType
    Private _partyName As New PartyNameType
    Private _PartyNamelist() As PartyNameType
    Private _id As New IDType
    '  Private _Name As New NameType1
    Private _taxTypeCode As New TaxTypeCodeType
    Private _taxCategory As New TaxCategoryType
    Private _taxTotal As New TaxTotalType
    Private _taxAmount As New TaxAmountType
    Private _taxSubtotal As New TaxSubtotalType
    Private _taxableAmount As New TaxableAmountType

    Private _customerParty As New CustomerPartyType
    Private _customerAssignedAccountID As New CustomerAssignedAccountIDType
    Private _additionalAccountID As New AdditionalAccountIDType
    Private _partyLegalEntity As New PartyLegalEntityType
    Private _PartyTaxScheme As New PartyTaxSchemeType
    Private _registrationName As New RegistrationNameType
    Private _companyID As New CompanyIDType
    Private _taxScheme As New TaxSchemeType
    Private _address As New AddressType
    'Private _addressTypeCode As New AddressTypeCodeType
    Private _PartyTaxSchemelist() As PartyTaxSchemeType

    Private _supplierParty As New SupplierPartyType

    Private _alternativeConditionPrice As New PriceType
    Private _priceAmount As New PriceAmountType
    Private _priceTypeCode As New PriceTypeCodeType

    Private _item As New ItemType
    Private _description As New DescriptionType

    Private _sellersItemIdentification As New ItemIdentificationType
    Private _commodityClassification As New CommodityClassificationType
    Private _ItemClassificationCode As New ItemClassificationCodeType
    Private _price As New PriceType

    Private _invoiceLine As New InvoiceLineType
    Private _invoicedQuantity As New InvoicedQuantityType

    Private _lineExtensionAmount As New LineExtensionAmountType
    Private _pricingReference As New PricingReferenceType



    Public Property UBLExtension As UBLExtensionType
        Get
            Return _UBLExtension
        End Get
        Set(value As UBLExtensionType)
            _UBLExtension = value
        End Set
    End Property

    Public Property ExtensionContent As ExtensionContentType()
        Get
            Return _ExtensionContent
        End Get
        Set(value As ExtensionContentType())
            _ExtensionContent = value
        End Set
    End Property

    Public Property Content As ExtensionContentType
        Get
            Return _Content
        End Get
        Set(value As ExtensionContentType)
            _Content = value
        End Set
    End Property

    Public Property Signature As SignatureType()
        Get
            Return _Signature
        End Get
        Set(value As SignatureType())
            _Signature = value
        End Set
    End Property

    Public Property Tofirma As SignatureType
        Get
            Return _Tofirma
        End Get
        Set(value As SignatureType)
            _Tofirma = value
        End Set
    End Property

    Public Property ID_Firma As IDType
        Get
            Return _ID_Firma
        End Get
        Set(value As IDType)
            _ID_Firma = value
        End Set
    End Property

    Public Property Attachment As AttachmentType
        Get
            Return _attachment
        End Get
        Set(value As AttachmentType)
            _attachment = value
        End Set
    End Property

    Public Property ExternalReference As ExternalReferenceType
        Get
            Return _externalReference
        End Get
        Set(value As ExternalReferenceType)
            _externalReference = value
        End Set
    End Property

    Public Property Uri As URIType
        Get
            Return _uri
        End Get
        Set(value As URIType)
            _uri = value
        End Set
    End Property

    Public Property Party As PartyType
        Get
            Return _party
        End Get
        Set(value As PartyType)
            _party = value
        End Set
    End Property

    Public Property PartyIdentification As PartyIdentificationType
        Get
            Return _PartyIdentification
        End Get
        Set(value As PartyIdentificationType)
            _PartyIdentification = value
        End Set
    End Property

    Public Property PartyIdentificationlist As PartyIdentificationType()
        Get
            Return _partyIdentificationlist
        End Get
        Set(value As PartyIdentificationType())
            _partyIdentificationlist = value
        End Set
    End Property

    Public Property PartyName As PartyNameType
        Get
            Return _partyName
        End Get
        Set(value As PartyNameType)
            _partyName = value
        End Set
    End Property

    Public Property PartyNamelist As PartyNameType()
        Get
            Return _PartyNamelist
        End Get
        Set(value As PartyNameType())
            _PartyNamelist = value
        End Set
    End Property

    Public Property Id As IDType
        Get
            Return _id
        End Get
        Set(value As IDType)
            _id = value
        End Set
    End Property



    Public Property TaxTypeCode As TaxTypeCodeType
        Get
            Return _taxTypeCode
        End Get
        Set(value As TaxTypeCodeType)
            _taxTypeCode = value
        End Set
    End Property

    Public Property TaxCategory As TaxCategoryType
        Get
            Return _taxCategory
        End Get
        Set(value As TaxCategoryType)
            _taxCategory = value
        End Set
    End Property

    Public Property TaxTotal As TaxTotalType
        Get
            Return _taxTotal
        End Get
        Set(value As TaxTotalType)
            _taxTotal = value
        End Set
    End Property

    Public Property TaxAmount As TaxAmountType
        Get
            Return _taxAmount
        End Get
        Set(value As TaxAmountType)
            _taxAmount = value
        End Set
    End Property

    Public Property TaxSubtotal As TaxSubtotalType
        Get
            Return _taxSubtotal
        End Get
        Set(value As TaxSubtotalType)
            _taxSubtotal = value
        End Set
    End Property

    Public Property TaxableAmount As TaxableAmountType
        Get
            Return _taxableAmount
        End Get
        Set(value As TaxableAmountType)
            _taxableAmount = value
        End Set
    End Property

    Public Property CustomerParty As CustomerPartyType
        Get
            Return _customerParty
        End Get
        Set(value As CustomerPartyType)
            _customerParty = value
        End Set
    End Property

    Public Property CustomerAssignedAccountID As CustomerAssignedAccountIDType
        Get
            Return _customerAssignedAccountID
        End Get
        Set(value As CustomerAssignedAccountIDType)
            _customerAssignedAccountID = value
        End Set
    End Property

    Public Property AdditionalAccountID As AdditionalAccountIDType
        Get
            Return _additionalAccountID
        End Get
        Set(value As AdditionalAccountIDType)
            _additionalAccountID = value
        End Set
    End Property

    Public Property PartyLegalEntity As PartyLegalEntityType
        Get
            Return _partyLegalEntity
        End Get
        Set(value As PartyLegalEntityType)
            _partyLegalEntity = value
        End Set
    End Property

    Public Property PartyTaxScheme As PartyTaxSchemeType
        Get
            Return _PartyTaxScheme
        End Get
        Set(value As PartyTaxSchemeType)
            _PartyTaxScheme = value
        End Set
    End Property

    Public Property RegistrationName As RegistrationNameType
        Get
            Return _registrationName
        End Get
        Set(value As RegistrationNameType)
            _registrationName = value
        End Set
    End Property

    Public Property CompanyID As CompanyIDType
        Get
            Return _companyID
        End Get
        Set(value As CompanyIDType)
            _companyID = value
        End Set
    End Property

    Public Property TaxScheme As TaxSchemeType
        Get
            Return _taxScheme
        End Get
        Set(value As TaxSchemeType)
            _taxScheme = value
        End Set
    End Property



    'Public Property AddressTypeCode As AddressTypeCodeType
    '    Get
    '        Return _addressTypeCode
    '    End Get
    '    Set(value As AddressTypeCodeType)
    '        _addressTypeCode = value
    '    End Set
    'End Property

    Public Property PartyTaxSchemelist As PartyTaxSchemeType()
        Get
            Return _PartyTaxSchemelist
        End Get
        Set(value As PartyTaxSchemeType())
            _PartyTaxSchemelist = value
        End Set
    End Property

    Public Property SupplierParty As SupplierPartyType
        Get
            Return _supplierParty
        End Get
        Set(value As SupplierPartyType)
            _supplierParty = value
        End Set
    End Property

    Public Property Address As AddressType
        Get
            Return _address
        End Get
        Set(value As AddressType)
            _address = value
        End Set
    End Property

    Public Property AlternativeConditionPrice As PriceType
        Get
            Return _alternativeConditionPrice
        End Get
        Set(value As PriceType)
            _alternativeConditionPrice = value
        End Set
    End Property

    Public Property PriceAmount As PriceAmountType
        Get
            Return _priceAmount
        End Get
        Set(value As PriceAmountType)
            _priceAmount = value
        End Set
    End Property

    Public Property PriceTypeCode As PriceTypeCodeType
        Get
            Return _priceTypeCode
        End Get
        Set(value As PriceTypeCodeType)
            _priceTypeCode = value
        End Set
    End Property

    Public Property Item As ItemType
        Get
            Return _item
        End Get
        Set(value As ItemType)
            _item = value
        End Set
    End Property

    Public Property Description As DescriptionType
        Get
            Return _description
        End Get
        Set(value As DescriptionType)
            _description = value
        End Set
    End Property

    Public Property SellersItemIdentification As ItemIdentificationType
        Get
            Return _sellersItemIdentification
        End Get
        Set(value As ItemIdentificationType)
            _sellersItemIdentification = value
        End Set
    End Property

    Public Property CommodityClassification As CommodityClassificationType
        Get
            Return _commodityClassification
        End Get
        Set(value As CommodityClassificationType)
            _commodityClassification = value
        End Set
    End Property

    Public Property ItemClassificationCode As ItemClassificationCodeType
        Get
            Return _ItemClassificationCode
        End Get
        Set(value As ItemClassificationCodeType)
            _ItemClassificationCode = value
        End Set
    End Property

    Public Property Price As PriceType
        Get
            Return _price
        End Get
        Set(value As PriceType)
            _price = value
        End Set
    End Property

    Public Property InvoiceLine As InvoiceLineType
        Get
            Return _invoiceLine
        End Get
        Set(value As InvoiceLineType)
            _invoiceLine = value
        End Set
    End Property

    Public Property InvoicedQuantity As InvoicedQuantityType
        Get
            Return _invoicedQuantity
        End Get
        Set(value As InvoicedQuantityType)
            _invoicedQuantity = value
        End Set
    End Property

    Public Property LineExtensionAmount As LineExtensionAmountType
        Get
            Return _lineExtensionAmount
        End Get
        Set(value As LineExtensionAmountType)
            _lineExtensionAmount = value
        End Set
    End Property

    Public Property PricingReference As PricingReferenceType
        Get
            Return _pricingReference
        End Get
        Set(value As PricingReferenceType)
            _pricingReference = value
        End Set
    End Property

    Public Property UBLVersionID As UBLVersionIDType
        Get
            Return _UBLVersionID
        End Get
        Set(value As UBLVersionIDType)
            _UBLVersionID = value
        End Set
    End Property

    Public Property CustomizationID As CustomizationIDType
        Get
            Return _CustomizationID
        End Get
        Set(value As CustomizationIDType)
            _CustomizationID = value
        End Set
    End Property

    Public Property IssueDate As IssueDateType
        Get
            Return _IssueDate
        End Get
        Set(value As IssueDateType)
            _IssueDate = value
        End Set
    End Property

    Public Property InvoiceTypeCode As InvoiceTypeCodeType
        Get
            Return _InvoiceTypeCode
        End Get
        Set(value As InvoiceTypeCodeType)
            _InvoiceTypeCode = value
        End Set
    End Property

    Public Property DocumentCurrencyCode As DocumentCurrencyCodeType
        Get
            Return _DocumentCurrencyCode
        End Get
        Set(value As DocumentCurrencyCodeType)
            _DocumentCurrencyCode = value
        End Set
    End Property


#End Region
#Region "Metodos"
    Private Function UBLExtensions() As UBLExtensionType
        ExtensionContent = {Content}
        UBLExtension.ExtensionContent = ExtensionContent
        Return UBLExtension
    End Function

#End Region
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
    Private Function GetTaxCategory(ByVal Idx As String, Nam_Tax As String, Code_Tax As String, TaxExcepcionCode As String, tipo_impuesto As String, PercentIGV As Decimal, Optional isdetalle As Boolean = True) As TaxCategoryType
        Dim ID As New IDType
        Dim Name As New NameType1
        Dim TaxTypeCode As New TaxTypeCodeType
        Dim TaxCategory As New TaxCategoryType
        Dim TaxExemptionReasonCode As New TaxExemptionReasonCodeType
        ID.Value = tipo_impuesto ' S=IGV / E=EXOGNERADO  I=INAFECTO
        ID.schemeID = "UN/ECE 5305"
        ID.schemeName = "Tax Category Identifier"
        ID.schemeAgencyName = "United Nations Economic Commission for Europe"
        Name.Value = Nam_Tax '"IGV"
        TaxCategory.ID = ID
        If isdetalle = True Then
            TaxExemptionReasonCode.Value = TaxExcepcionCode
            TaxExemptionReasonCode.listAgencyName = "PE:SUNAT"
            TaxExemptionReasonCode.listName = "Afectacion del IGV"
            TaxExemptionReasonCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
            TaxCategory.TaxExemptionReasonCode = TaxExemptionReasonCode
        End If

        ' If PercentIGV <> 0.0 Then
        TaxCategory.Percent = New PercentType With {.Value = PercentIGV}
        'End If

        Dim ID_taxscheme As New IDType
        ID_taxscheme.Value = Idx
        TaxTypeCode.Value = Code_Tax '"VAT"
        Dim TaxScheme As New TaxSchemeType With {.ID = ID_taxscheme, .Name = Name, .TaxTypeCode = TaxTypeCode}
        TaxScheme.ID.schemeID = "UN/ECE 5153"
        'TaxScheme.ID.schemeAgencyID = "6"
        TaxScheme.ID.schemeName = "Codigo de tributos"
        TaxScheme.ID.schemeAgencyName = "PE:SUNAT"
        TaxCategory.TaxScheme = TaxScheme
        Return TaxCategory
    End Function
    Private Function Tax_Totales(cabecera As DataTable, Optional isdetalle As Boolean = True) As TaxTotalType
        Dim TaxAmount1 As New TaxAmountType
        Dim TaxTotal1 As New TaxTotalType
        TaxAmount1.currencyID = ls_TipoMoneda
        TaxAmount1.Value = 18.0
        TaxTotal1.TaxAmount = TaxAmount1
        Dim taxSub(cabecera.Rows.Count) As TaxSubtotalType
        Dim i As Int16 = 0
        For Each tax As DataRow In cabecera.Rows
            taxSub(i) = Tax_SubImpuesto(CType(tax.Item("PrecioSIGV"), Decimal).ToString("#.00"), CType(tax.Item("dImporteIGV"), Decimal).ToString("#.00"), tax.Item("Cod_Tipo_Trib"), tax.Item("Tipo_Tributo"), tax.Item("Tax"), tax.Item("AfecIGV").ToString.Trim, tax.Item("IGV").ToString.Trim, tax.Item("tax_Category").ToString.Trim, False)
            i += 1
        Next
        TaxTotal1.TaxSubtotal = taxSub   '{TaxSubtotal}
        Return TaxTotal1
    End Function

    Private Function Tax_Impuesto(ByVal ValorVenta As Decimal, ByVal Tax_importe As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, taxExcepCod As String, PercentIGV As Decimal, tax_cate As String, Optional isdetalle As Boolean = True) As TaxTotalType
        Dim TaxAmount1 As New TaxAmountType
        Dim TaxTotal1 As New TaxTotalType
        TaxAmount1.currencyID = ls_TipoMoneda
        ' If Tax_importe <> 0 Then
        TaxAmount1.Value = Tax_importe
        TaxTotal1.TaxAmount = TaxAmount1
        'End If
        TaxSubtotal = Tax_SubImpuesto(ValorVenta, Tax_importe, IdTax, Tax_Name, Tax_cod, taxExcepCod, PercentIGV, tax_cate, isdetalle)
        TaxTotal1.TaxSubtotal = {TaxSubtotal}
        Return TaxTotal1
    End Function
    Private Function Tax_SubImpuesto(ByVal ValorVenta As Decimal, ByVal Tax_SubImporte As Decimal, ByVal IdTax As String, Tax_Name As String, Tax_cod As String, taxExcepCod As String, PercentIGV As Decimal, tax_cate As String, Optional isdetalle As Boolean = True) As TaxSubtotalType
        Dim TaxableAmount As New TaxableAmountType
        Dim TaxSubtotal As New TaxSubtotalType
        Dim TaxAmount As New TaxAmountType
        TaxableAmount.currencyID = ls_TipoMoneda
        TaxableAmount.Value = ValorVenta
        TaxSubtotal.TaxableAmount = TaxableAmount
        TaxAmount.currencyID = ls_TipoMoneda

        TaxAmount.Value = Tax_SubImporte
        TaxSubtotal.TaxAmount = TaxAmount
        TaxSubtotal.TaxCategory = GetTaxCategory(IdTax, Tax_Name, Tax_cod, taxExcepCod, tax_cate, PercentIGV, isdetalle)

        Return TaxSubtotal
    End Function


    Private Function Datos_Cli_SEE(ByVal Ubigeo As String, ByVal razonsocial As String, ByVal TdSunat As String, ByVal IdRuc As String, ByVal Direccion As String, ByVal Urbanizacion As String, ciudad As String, provincia As String, distrito As String, CodPais As String, nomComercial As String) As CustomerPartyType
        Dim CustomerParty As New CustomerPartyType
        Dim Party As New PartyType
        Dim AdditionalAccountID As New AdditionalAccountIDType
        Dim RegistrationName As New RegistrationNameType
        Dim CompanyID As New CompanyIDType
        Dim AddressTypeCode As New AddressTypeCodeType
        Dim Address As New AddressType
        Dim TaxScheme As New TaxSchemeType
        Dim ID As New IDType
        Dim PartyTaxSchemelist() As PartyTaxSchemeType
        Dim PartyIdentification As New PartyIdentificationType
        Dim ID_PartyIdentification As New IDType
        ID_PartyIdentification.Value = IdRuc
        ID_PartyIdentification.schemeID = TdSunat.Trim
        ID_PartyIdentification.schemeName = "Documento de Identidad"
        ID_PartyIdentification.schemeAgencyName = "PE:SUNAT"
        ID_PartyIdentification.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
        PartyIdentification.ID = ID_PartyIdentification
        Party.PartyIdentification = {PartyIdentification}
        Dim PartyLegalEntity As New PartyLegalEntityType

        Party.PartyLegalEntity = {f_PartyLegalEntity(razonsocial, "0000", Direccion, ciudad, ciudad, distrito, "PE", Ubigeo)}
        '000 AS ESTABLECIMIENTO FISCAL, otros codigos segun establecimientos anexo.

        RegistrationName.Value = razonsocial
        CompanyID.Value = IdRuc.Trim
        CompanyID.schemeID = TdSunat.Trim
        CompanyID.schemeName = "Documento de Identidad"
        CompanyID.schemeAgencyName = "PE:SUNAT"
        CompanyID.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
        AddressTypeCode.Value = "0001"
        Address.AddressTypeCode = AddressTypeCode

        ID.Value = "-"
        TaxScheme.ID = ID
        With PartyTaxScheme
            .RegistrationName = RegistrationName
            .CompanyID = CompanyID
            .RegistrationAddress = Address
            .TaxScheme = TaxScheme
        End With
        PartyTaxSchemelist = {PartyTaxScheme}
        Party.PartyTaxScheme = PartyTaxSchemelist
        Party.PartyName = {ComercialName(nomComercial)}

        CustomerParty.Party = Party
        Return CustomerParty
    End Function
    Private Function Datos_Prov_SEE(ByVal Ubigeo As String, ByVal razonsocial As String, ByVal TdSunat As String, ByVal IdRuc As String, ByVal Direccion As String, ByVal Urbanizacion As String, ciudad As String, provincia As String, distrito As String, CodPais As String, nomComercial As String, Cod_Estable_Sunat As String) As SupplierPartyType
        Dim SupplierParty1 As New SupplierPartyType
        Dim Party1 As New PartyType
        Dim RegistrationName1 As New RegistrationNameType
        Dim CompanyID1 As New CompanyIDType
        Dim AddressTypeCode1 As New AddressTypeCodeType
        Dim Address1 As New AddressType
        Dim TaxScheme1 As New TaxSchemeType
        Dim ID1 As New IDType
        Dim PartyTaxSchemelist1() As PartyTaxSchemeType
        Dim PartyTaxScheme1 As New PartyTaxSchemeType
        Dim PartyIdentification1 As New PartyIdentificationType
        Dim ID_PartyIdentification As New IDType
        ID_PartyIdentification.Value = IdRuc
        ID_PartyIdentification.schemeID = TdSunat.Trim
        ID_PartyIdentification.schemeName = "Documento de Identidad"
        ID_PartyIdentification.schemeAgencyName = "PE:SUNAT"
        ID_PartyIdentification.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
        PartyIdentification1.ID = ID_PartyIdentification
        Party1.PartyIdentification = {PartyIdentification1}
        Party1.PartyLegalEntity = {f_PartyLegalEntity_Prov(razonsocial, Cod_Estable_Sunat, Direccion, ciudad, ciudad, distrito, "PE", Ubigeo)}
        '000 AS ESTABLECIMIENTO FISCAL, otros codigos segun establecimientos anexo.
        RegistrationName1.Value = razonsocial
        CompanyID1.Value = IdRuc.Trim
        CompanyID1.schemeID = TdSunat.Trim
        CompanyID1.schemeName = "Documento de Identidad"
        CompanyID1.schemeAgencyName = "PE:SUNAT"
        CompanyID1.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
        AddressTypeCode1.Value = Cod_Estable_Sunat
        Address1.AddressTypeCode = AddressTypeCode1
        ID1.Value = "-"
        TaxScheme1.ID = ID1
        With PartyTaxScheme1
            .RegistrationName = RegistrationName1
            .CompanyID = CompanyID1
            .RegistrationAddress = Address1
            .TaxScheme = TaxScheme1
        End With
        PartyTaxSchemelist1 = {PartyTaxScheme1}
        Party1.PartyTaxScheme = PartyTaxSchemelist1
        Party1.PartyName = {Prov_ComercialName(nomComercial)}
        SupplierParty1.Party = Party1
        Return SupplierParty1
    End Function
    Private Function f_PartyLegalEntity_Prov(rs As String, nro_establecimiento As String, direccion As String, ciudad As String, lugar_entraga_bien As String, distrito As String, Optional Nacionalidad As String = "PE", Optional codigoubigeo As String = "") As PartyLegalEntityType
        Dim PartyLegalEntity1 As New PartyLegalEntityType
        Dim RegistrationName1 As New RegistrationNameType
        Dim ID_address As New IDType
        Dim AddressTypeCode1 As New AddressTypeCodeType
        Dim CityName As New CityNameType
        Dim CountrySubentity As New CountrySubentityType
        Dim District As New DistrictType
        Dim Address As New AddressType

        RegistrationName1.Value = rs
        PartyLegalEntity1.RegistrationName = RegistrationName1
        ID_address.schemeName = "Ubigeos"
        ID_address.schemeAgencyName = "PE:INEI"
        ID_address.Value = codigoubigeo
        AddressTypeCode1.listAgencyName = "PE:SUNAT"
        AddressTypeCode1.listName = "Establecimientos anexos"
        AddressTypeCode1.Value = nro_establecimiento
        CityName.Value = ciudad
        CountrySubentity.Value = lugar_entraga_bien
        District.Value = distrito
        Address.ID = ID_address
        Address.AddressTypeCode = AddressTypeCode1
        Address.CityName = CityName
        Address.CountrySubentity = CountrySubentity
        Address.District = District
        Dim AddressLine As New AddressLineType
        Dim line As New LineType
        line.Value = direccion
        AddressLine.Line = line
        Address.AddressLine = {AddressLine}
        Dim Country As New CountryType
        Dim IdentificationCode As New IdentificationCodeType
        IdentificationCode.listID = "ISO 3166-1"
        IdentificationCode.listAgencyName = "United Nations Economic Commission for Europe"
        IdentificationCode.listName = "Country"
        IdentificationCode.Value = Nacionalidad
        Country.IdentificationCode = IdentificationCode
        Address.Country = Country

        PartyLegalEntity1.RegistrationAddress = Address
        Return PartyLegalEntity1
    End Function



    Private Function f_PartyLegalEntity(rs As String, nro_establecimiento As String, direccion As String, ciudad As String, lugar_entraga_bien As String, distrito As String, Optional Nacionalidad As String = "PE", Optional codigoubigeo As String = "") As PartyLegalEntityType
        Dim PartyLegalEntity As New PartyLegalEntityType
        Dim RegistrationName As New RegistrationNameType
        Dim ID_address As New IDType
        Dim AddressTypeCode As New AddressTypeCodeType
        Dim CityName As New CityNameType
        Dim CountrySubentity As New CountrySubentityType
        Dim District As New DistrictType
        RegistrationName.Value = rs
        PartyLegalEntity.RegistrationName = RegistrationName
        ID_address.schemeName = "Ubigeos"
        ID_address.schemeAgencyName = "PE:INEI"
        ID_address.Value = codigoubigeo
        AddressTypeCode.listAgencyName = "PE:SUNAT"
        AddressTypeCode.listName = "Establecimientos anexos"
        AddressTypeCode.Value = nro_establecimiento
        CityName.Value = ciudad
        CountrySubentity.Value = lugar_entraga_bien
        District.Value = distrito
        Address.ID = ID_address
        Address.AddressTypeCode = AddressTypeCode
        Address.CityName = CityName
        Address.CountrySubentity = CountrySubentity
        Address.District = District
        Dim AddressLine As New AddressLineType
        Dim line As New LineType
        line.Value = direccion
        AddressLine.Line = line
        Address.AddressLine = {AddressLine}
        Dim Country As New CountryType
        Dim IdentificationCode As New IdentificationCodeType
        IdentificationCode.listID = "ISO 3166-1"
        IdentificationCode.listAgencyName = "United Nations Economic Commission for Europe"
        IdentificationCode.listName = "Country"
        IdentificationCode.Value = Nacionalidad
        Country.IdentificationCode = IdentificationCode
        Address.Country = Country

        PartyLegalEntity.RegistrationAddress = Address
        Return PartyLegalEntity
    End Function
    'Private Function f_RegistrationAddress() As registration
    Private Function Prov_ComercialName(ByVal NomComercial As String) As PartyNameType
        Dim name1 As New NameType1
        Dim PartyName1 As New PartyNameType
        name1.Value = NomComercial
        PartyName1.Name = name1
        Return PartyName1
    End Function
    Private Function ComercialName(ByVal NomComercial As String) As PartyNameType
        Dim name1 As New NameType1
        Dim PartyName1 As New PartyNameType
        name1.Value = NomComercial
        PartyName1.Name = name1
        Return PartyName1
    End Function
    Private Function DetalleItem(item As Integer, ByVal datos As DataTable) As InvoiceLineType()
        Dim InvoiceItem(item) As InvoiceLineType
        Dim i As Integer = 0
        For Each r As DataRow In datos.Rows
            InvoiceItem(i) = DetalleFactura(CInt(r.Item("Item")), r)
            i += 1
        Next
        Return InvoiceItem
    End Function
    Private Function DetalleFactura(item As Integer, d As DataRow) As InvoiceLineType
        Dim InvoiceLine1 As New InvoiceLineType
        Dim InvoicedQuantity As New InvoicedQuantityType
        Dim LineExtensionAmount As New LineExtensionAmountType
        Dim PricingReference As New PricingReferenceType

        'convirte en un equivalente enum para comprar con la unidad de medida internacional UN/ECE rec 20- Unit Of Measure
        Dim UnidadMed As Integer
        UnidadMed = DirectCast([Enum].Parse(GetType(UnitCodeContentType), d.Item("UndRef")), Integer)
        InvoicedQuantity.unitCode = UnidadMed
        InvoicedQuantity.unitCodeSpecified = True
        InvoicedQuantity.unitCodeListID = "UN/ECE rec 20"
        InvoicedQuantity.unitCodeListAgencyName = "United Nations Economic Commission for Europe"
        InvoicedQuantity.Value = CType(d.Item("Cantidad"), Decimal).ToString("#.00")

        Dim TprecioItem As Decimal = CType(IIf(MonedaDet.Trim = "MN", d.Item("PrecioSIGV"), d.Item("PrecioSIGV")), Decimal).ToString("#.00")
        LineExtensionAmount.currencyID = ls_TipoMoneda
        LineExtensionAmount.Value = TprecioItem ' TprecioItem
        Dim FreeCharge As New FreeOfChargeIndicatorType

        Dim pricecode1(1) As PriceType
        Dim PriceTypeCode As String = IIf(d.Item("Tipo_Tributo").ToString.Trim = "GRA", "02", "01")
        If PriceTypeCode = "02" Then
            FreeCharge.Value = True
        Else
            FreeCharge.Value = False
        End If

        pricecode1(1) = PrecioUnitario(PriceTypeCode, CType(d.Item("PrecioVenta"), Decimal).ToString("#.00"))
        PricingReference.AlternativeConditionPrice = pricecode1

        Dim ID As New IDType
        ID.Value = item
        InvoiceLine1.ID = ID
        InvoiceLine1.InvoicedQuantity = InvoicedQuantity
        InvoiceLine1.LineExtensionAmount = LineExtensionAmount
        InvoiceLine1.FreeOfChargeIndicator = FreeCharge
        InvoiceLine1.PricingReference = PricingReference


        Dim Tax_pru(2) As TaxTotalType
        Tax_pru(1) = Tax_Impuesto(TprecioItem, CType(d.Item("ImporteIGV"), Decimal).ToString("#.00"), d.Item("Cod_Tipo_Trib"), d.Item("Tipo_Tributo"), d.Item("tax"), d.Item("AfecIGV").ToString.Trim, CType(d.Item("IGV"), Decimal).ToString("#.00"), d.Item("tax_category").ToString.Trim)
        InvoiceLine1.TaxTotal = Tax_pru

        InvoiceLine1.Item = itemarticulo(d.Item("IdArticulo").ToString.Trim, d.Item("Descripcion").ToString.Trim)
        If PriceTypeCode = "02" Then
            InvoiceLine1.Price = Precio_SinIGv(CType(0.00, Decimal).ToString("#.00"))
        Else
            InvoiceLine1.Price = Precio_SinIGv(CType(d.Item("PrecioSIGV") / d.Item("Cantidad"), Decimal).ToString("#.00"))
        End If

        Return InvoiceLine1
    End Function

    Private Function Precio_SinIGv(ByVal PrecionSinTax As Decimal) As PriceType
        Dim PriceAmount1 As New PriceAmountType
        Dim Price1 As New PriceType
        PriceAmount1.currencyID = ls_TipoMoneda
        PriceAmount1.Value = PrecionSinTax
        Price1.PriceAmount = PriceAmount1
        Return Price1
    End Function
    Private Function itemarticulo(ByVal IdArticulo As String, DescripcionArt As String) As ItemType
        Dim Description As New DescriptionType
        Dim Itemx As New ItemType
        Dim ItemClassificationCode As New ItemClassificationCodeType
        Dim CommodityClassification As New CommodityClassificationType
        Dim SellersItemIdentification As New ItemIdentificationType
        Dim ID As New IDType
        Description.Value = DescripcionArt.Trim
        Itemx.Description = {Description}

        ID.Value = IdArticulo.Trim
        SellersItemIdentification.ID = ID
        Itemx.SellersItemIdentification = SellersItemIdentification

        Dim art As New NArticulo
        art.IdArticulo = IdArticulo.Trim
        art = art.Registro(art)
        If IsNothing(art.IdLinea) = False Then
            If art.IdLinea.Trim.Length = 8 Then
                ''' solo para exportacion 
                ItemClassificationCode.Value = art.IdLinea.Trim '      IdArticulo.Trim()
                ItemClassificationCode.listID = "UNSPSC"
                ItemClassificationCode.listAgencyName = "GS1 US"
                ItemClassificationCode.listName = "Item Classification"
                CommodityClassification.ItemClassificationCode = ItemClassificationCode
                Itemx.CommodityClassification = {CommodityClassification}
            End If
        End If


        Return Itemx
    End Function
    Private Function PrecioUnitario(ByVal PriceTypeCode_string As String, precioUnit As Decimal) As PriceType
        Dim PriceAmount As New PriceAmountType
        Dim PriceTypeCode As New PriceTypeCodeType
        Dim AlternativeConditionPrice As New PriceType
        PriceAmount.currencyID = ls_TipoMoneda
        PriceAmount.Value = precioUnit
        PriceTypeCode.Value = PriceTypeCode_string.Trim
        PriceTypeCode.listName = "Tipo de Precio"
        PriceTypeCode.listAgencyName = "PE:SUNAT"
        PriceTypeCode.listURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16"
        AlternativeConditionPrice.PriceAmount = PriceAmount
        AlternativeConditionPrice.PriceTypeCode = PriceTypeCode
        Return AlternativeConditionPrice 'pricecode1
    End Function
End Class
