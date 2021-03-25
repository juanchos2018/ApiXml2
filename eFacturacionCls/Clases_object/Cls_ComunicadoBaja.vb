Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.VisualBasic
Public Class Cls_ComunicadoBaja
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
        Dim serializer As New XmlSerializer(GetType(VoidedDocumentsType))
        '**********

        Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))


        Dim Invoice As New VoidedDocumentsType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("ns11", "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1")
        myNamespaces.Add("ns12", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
        myNamespaces.Add("ns13", "urn:sunat:names:specification:ubl:peru:schema:xsd:Retention-1")
        myNamespaces.Add("ns14", "urn:sunat:names:specification:ubl:peru:schema:xsd:Perception-1")
        myNamespaces.Add("ns6", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
        myNamespaces.Add("ns7", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
        myNamespaces.Add("ns8", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2")

        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, RA As New IDType, FechaRef As New ReferenceDateType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        '   RA.Value = "RA-" & Format(Now.Date, "yyyyMMdd") & "-001" 'OComprobante
        RA.Value = nrobo
        Invoice.ID = RA
        '--------- Fecha de generación del resumen
        fecha.Value = Now.Date
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

        Dim ubl As New UBLExtensionType
        Dim ubl_1(1) As UBLExtensionType
        ubl_1(0) = UBLExtensions1x1()
        Invoice.UBLExtensions = ubl_1

        Invoice.VoidedDocumentsLine = DetalleItem(Cabecera.Rows.Count, Cabecera)

        '*************** Escribe el xml
        Dim xwriter As XmlTextWriter = New XmlTextWriter(writer)
        xwriter.WriteStartDocument(False)
        serializer.Serialize(xwriter, Invoice, myNamespaces)
        xwriter.Close()
        writer.Close()

    End Sub
    Public Function CreatePO(filename As String, Ruc As String, ByVal Oversion As String, OCustomId As String, OFEmsion As DateTime, ByVal DatosEE As DataTable, ByVal Cabecera As DataTable, nrobo As String) As Byte()
        Dim serializer As New XmlSerializer(GetType(VoidedDocumentsType))
        '**********
        Dim ms As New MemoryStream
        Dim writer As New StreamWriter(ms, System.Text.Encoding.GetEncoding("ISO-8859-1"))
        '*********
        'Dim writer As New StreamWriter(filename, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))


        Dim Invoice As New VoidedDocumentsType()
        Dim myNamespaces As New XmlSerializerNamespaces()
        myNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        myNamespaces.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")
        myNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        myNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2")
        myNamespaces.Add("ccts", "urn:un:unece:uncefact:documentation:2")
        myNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        myNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2")
        myNamespaces.Add("ns11", "urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1")
        myNamespaces.Add("ns12", "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2")
        myNamespaces.Add("ns13", "urn:sunat:names:specification:ubl:peru:schema:xsd:Retention-1")
        myNamespaces.Add("ns14", "urn:sunat:names:specification:ubl:peru:schema:xsd:Perception-1")
        myNamespaces.Add("ns6", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
        myNamespaces.Add("ns7", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2")
        myNamespaces.Add("ns8", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2")

        myNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        myNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#")

        Dim Version As New UBLVersionIDType, CustomizationId As New CustomizationIDType, Factura As New IDType, fecha As New IssueDateType,
           Cod_Doc As New InvoiceTypeCodeType, xml_Mon As New DocumentCurrencyCodeType, RA As New IDType, FechaRef As New ReferenceDateType

        '--------version de xml
        Version.Value = Oversion
        Invoice.UBLVersionID = Version
        CustomizationId.Value = OCustomId
        Invoice.CustomizationID = CustomizationId
        '--------- Tipo Documento + Nro Comprobante
        '   RA.Value = "RA-" & Format(Now.Date, "yyyyMMdd") & "-001" 'OComprobante
        RA.Value = nrobo
        Invoice.ID = RA
        '--------- Fecha de generación del resumen
        fecha.Value = Now.Date
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

        Dim ubl As New UBLExtensionType
        Dim ubl_1(1) As UBLExtensionType
        ubl_1(0) = UBLExtensions1x1()
        Invoice.UBLExtensions = ubl_1

        Invoice.VoidedDocumentsLine = DetalleItem(Cabecera.Rows.Count, Cabecera)

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
        '  writer.Flush()
        writer.Close()

        Return ms.ToArray
    End Function
    Private Function UBLExtensions1x1() As UBLExtensionType
        Dim UBLExtension1Y As New UBLExtensionType
        Dim ExtensionContent1Y As New ExtensionContentType
        Dim ExtensionContent_1Y(0) As ExtensionContentType
        ExtensionContent_1Y(0) = ExtensionContent1Y
        UBLExtension1Y.ExtensionContent = ExtensionContent_1Y
        Return UBLExtension1Y
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

    Private Function DetalleItem(item As Integer, ByVal datos As DataTable) As VoidedDocumentsLineType()
        Dim InvoiceItem(item) As VoidedDocumentsLineType
        For x As Integer = 0 To item - 1
            With datos
                InvoiceItem(x) = DetalleFactura(x + 1, .Rows(x).Item("TdSunat").ToString.Trim, .Rows(x).Item("Serie").ToString.Trim, .Rows(x).Item("NroInicial").ToString.Trim, .Rows(x).Item("MotivoBaja").ToString.Trim)
            End With
        Next
        Return InvoiceItem
    End Function
    Private Function DetalleFactura(Linea As String, TipoDoc As String, Serie As String, NroI As String, MotivodeBaja As String) As VoidedDocumentsLineType
        Dim InvoiceLine1 As New VoidedDocumentsLineType
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
        InvoiceLine1.DocumentNumberID = NroInicial
        Dim VoidReasonDescription As New TextType
        VoidReasonDescription.Value = MotivodeBaja
        InvoiceLine1.VoidReasonDescription = VoidReasonDescription
        Return InvoiceLine1
    End Function
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
