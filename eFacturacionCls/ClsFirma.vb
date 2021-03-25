Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Text
Imports System.Xml

Public Class ClsFirma
    Private Str_codHash = ""
    Private Str_signaturevalue = ""
    Public Sub SignXmlFile_509(FileName As String, SignedFileName As String, Key As X509Certificate2, isFirstContex As Boolean)

        'Dim valordigest As New Integer
        Dim doc As New XmlDocument()
        'Abre el archivo ignorando los espacios
        'doc.PreserveWhitespace = True

        doc.PreserveWhitespace = False

        ' Load the passed XML file using its name.
        doc.Load(New XmlTextReader(FileName))
        ' Create a SignedXml object.
        Dim signedXml As New SignedXml(doc)
        signedXml.Signature.Id = ls_IdSing
        signedXml.SigningKey = Key.PrivateKey
        Dim reference As New Reference()
        reference.Uri = ""
        ' Add an enveloped transformation to the reference.
        Dim env As New XmlDsigEnvelopedSignatureTransform
        reference.AddTransform(env)
        signedXml.AddReference(reference)
        ' Compute the signature.
        Dim keyInfo As New KeyInfo
        keyInfo.AddClause(New KeyInfoX509Data(Key))
        signedXml.KeyInfo = keyInfo
        signedXml.ComputeSignature()
        If reference.DigestValue IsNot Nothing Then
            'el codigo hash para imprimir
            Str_codHash = Convert.ToBase64String(reference.DigestValue)
            Str_signaturevalue = Convert.ToBase64String(signedXml.SignatureValue)
        End If
        Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()
        ' poner en comentario
        xmlDigitalSignature.Prefix = "ds"

        'signedXml.ComputeSignature()

        If isFirstContex = True Then
            doc.DocumentElement.ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(doc.ImportNode(xmlDigitalSignature, True))
        Else
            doc.DocumentElement.ChildNodes(0).ChildNodes(1).ChildNodes(0).AppendChild(doc.ImportNode(xmlDigitalSignature, True))
        End If

        Dim xmltw As New XmlTextWriter(SignedFileName, Encoding.GetEncoding("ISO-8859-1"))
        doc.WriteTo(xmltw)
        xmltw.Close()

    End Sub
    ''' <summary>
    ''' Firma el xml desde un archivo binario
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="SignedFileName"></param>
    ''' <param name="Key"></param>
    ''' <param name="isFirstContex"></param>
    Public Sub firmaBinari(a As Byte(), SignedFileName As String, Key As X509Certificate2, isFirstContex As Boolean)
        Dim doc As New XmlDocument
        doc.PreserveWhitespace = True
        Dim ms As New MemoryStream(a)
        doc.Load(ms)
        Dim signedXml As New SignedXml(doc)
        signedXml.Signature.Id = ls_IdSing
        signedXml.SigningKey = Key.PrivateKey

        Dim reference As New Reference()
        reference.Uri = ""
        Dim env As New XmlDsigEnvelopedSignatureTransform
        reference.AddTransform(env)
        signedXml.AddReference(reference)
        ' Compute the signature.
        Dim keyInfo As New KeyInfo()
        Dim keyData As New KeyInfoX509Data(Key)
        'Dim x509Serial As X509IssuerSerial
        'x509Serial.IssuerName = Key.IssuerName.Name
        'x509Serial.SerialNumber = Key.SerialNumber
        'keyData.AddIssuerSerial(x509Serial.IssuerName, x509Serial.SerialNumber)
        keyData.AddSubjectName(Key.SubjectName.Name)
        keyInfo.AddClause(keyData)
        signedXml.KeyInfo = keyInfo
        signedXml.ComputeSignature()
        If reference.DigestValue IsNot Nothing Then
            Str_codHash = Convert.ToBase64String(reference.DigestValue)
            Str_signaturevalue = Convert.ToBase64String(signedXml.SignatureValue)
        End If

        Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()
        xmlDigitalSignature.Prefix = "ds"

        Dim nodeList As XmlNodeList = doc.GetElementsByTagName("ext:ExtensionContent")
        For Each node As XmlNode In nodeList
            If node.InnerText = "" Then
                node.AppendChild(doc.ImportNode(xmlDigitalSignature, True))
            End If
        Next

        Dim xmltw As New XmlTextWriter(SignedFileName, Encoding.GetEncoding("ISO-8859-1"))

        Try
            doc.WriteTo(xmltw)
        Finally
            xmltw.Flush()
            xmltw.Close()
        End Try

    End Sub

    Public Sub firmaBinari(filexml As String, SignedFileName As String, Key As X509Certificate2, isFirstContex As Boolean)
        Dim doc As New XmlDocument
        doc.PreserveWhitespace = True
        'Dim ms As New MemoryStream(a)
        ' doc.Load(filexml)
        'doc.InsertBefore(doc.CreateProcessingInstruction("xml-stylesheet", "type=""text/xsl"" href=""Factura.xsl"""), doc.DocumentElement)
        Dim signedXml As New SignedXml(doc)
        signedXml.Signature.Id = ls_IdSing
        ' Add the key to the SignedXml document. 
        signedXml.SigningKey = Key.PrivateKey
        'signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl
        'Dim canMethod As XmlDsigExcC14NTransform = CType(signedXml.SignedInfo.CanonicalizationMethodObject, XmlDsigExcC14NTransform)
        'canMethod.InclusiveNamespacesPrefixList = "ds"

        Dim reference As New Reference()
        reference.Uri = ""
        Dim env As New XmlDsigEnvelopedSignatureTransform
        reference.AddTransform(env)
        signedXml.AddReference(reference)
        ' Compute the signature.
        Dim keyInfo As New KeyInfo()
        'keyInfo.AddClause(New KeyInfoX509Data(Key))


        Dim keyData As New KeyInfoX509Data(Key)
        'Dim x509Serial As X509IssuerSerial
        'x509Serial.IssuerName = Key.IssuerName.Name
        'x509Serial.SerialNumber = Key.SerialNumber
        'keyData.AddIssuerSerial(x509Serial.IssuerName, x509Serial.SerialNumber)
        keyData.AddSubjectName(Key.SubjectName.Name)
        keyInfo.AddClause(keyData)


        signedXml.KeyInfo = keyInfo
        signedXml.ComputeSignature()

        If reference.DigestValue IsNot Nothing Then
            Str_codHash = Convert.ToBase64String(reference.DigestValue)
            Str_signaturevalue = Convert.ToBase64String(signedXml.SignatureValue)
        End If

        Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()
        xmlDigitalSignature.Prefix = "ds"

        Dim nodeList As XmlNodeList = doc.GetElementsByTagName("ext:ExtensionContent")
        For Each node As XmlNode In nodeList
            If node.InnerText = "" Then
                node.AppendChild(doc.ImportNode(xmlDigitalSignature, True))
            End If
        Next

        Dim xmltw As New XmlTextWriter(SignedFileName, Encoding.GetEncoding("ISO-8859-1"))

        Try
            doc.WriteTo(xmltw)
        Finally
            xmltw.Flush()
            xmltw.Close()
        End Try

    End Sub



    'Public Sub firmaBinari(a As Byte(), SignedFileName As String, Key As X509Certificate2, isFirstContex As Boolean)
    '    Dim xmlDocument As XmlDocument = New XmlDocument()
    '    Dim ms As New MemoryStream(a)
    '    xmlDocument.Load(ms)
    '    ' Creating the XML signing object.
    '    Dim sxml As New SignedXml(xmlDocument)
    '    sxml.SigningKey = Key.PrivateKey
    '    sxml.Signature.Id = ls_IdSing
    '    ' Set the canonicalization method for the document.
    '    sxml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl
    '    Dim reference As New Reference("")

    '    ' Create the XPath transform and add it to the reference list.
    '    reference.AddTransform(New XmlDsigEnvelopedSignatureTransform(False))

    '    ' Add the reference to the SignedXml object.
    '    sxml.AddReference(reference)

    '    ' Compute the signature.
    '    sxml.ComputeSignature()
    '    If reference.DigestValue IsNot Nothing Then
    '        Str_codHash = Convert.ToBase64String(reference.DigestValue)
    '        Str_signaturevalue = Convert.ToBase64String(sxml.SignatureValue)
    '    End If

    '    xmlDocument.PreserveWhitespace = True
    '    ' Get the signature XML and add it to the document element.
    '    Dim sig As XmlElement = sxml.GetXml()
    '    sig.Prefix = "ds"
    '    ' xmlDocument.DocumentElement.AppendChild(sig)
    '    If isFirstContex = True Then
    '        xmlDocument.DocumentElement.ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(xmlDocument.ImportNode(sig, True))
    '    Else
    '        xmlDocument.DocumentElement.ChildNodes(0).ChildNodes(1).ChildNodes(0).AppendChild(xmlDocument.ImportNode(sig, True))
    '    End If
    '    Dim writer As New XmlTextWriter(SignedFileName, Encoding.GetEncoding("ISO-8859-1"))
    '    ' writer.Formatting = Formatting.Indented
    '    Try
    '        xmlDocument.WriteTo(writer)
    '    Finally
    '        writer.Flush()
    '        writer.Close()
    '    End Try
    'End Sub

    Public Function ReturCodHas() As String
        If Str_codHash = "" Then
            Str_codHash = "No tiene Firma"
        End If
        Return Str_codHash
    End Function
    Public Function Retursignaturevalue() As String
        If Str_signaturevalue = "" Then
            Str_signaturevalue = "No tiene Firma"
        End If
        Return Str_signaturevalue
    End Function
    Public Function VerifyXmlFile_509(Name As [String], Key As X509Certificate2) As [Boolean]
        ' Create a new XML document.
        Dim xmlDocument As New XmlDocument()
        ' Load the passed XML file into the document. 
        xmlDocument.PreserveWhitespace = True
        xmlDocument.Load(Name)
        ' Create a new SignedXml object and pass it
        ' the XML document class.
        Dim signedXml As New SignedXml(xmlDocument)

        ' Find the "Signature" node and create a new
        Dim nodeList As XmlNodeList = xmlDocument.GetElementsByTagName("ds:Signature")   'xmlDocument.GetElementsByTagName("Signature")
        If nodeList.Count <> 1 Then
            Throw New Exception("Se produjo un error en la firma del documento")
        End If
        signedXml.LoadXml(CType(nodeList(0), XmlElement))
        ' Check the signature and return the result.
        Return signedXml.CheckSignature(Key, True)
    End Function
    Public Function VerifyXmlFile_509(a As Byte(), Key As X509Certificate2) As [Boolean]
        ' Create a new XML document.
        Dim xmlDocument As New XmlDocument()
        ' Load the passed XML file into the document. 
        xmlDocument.PreserveWhitespace = True
        Dim ms As New MemoryStream(a)
        xmlDocument.Load(ms)
        ' Create a new SignedXml object and pass it
        ' the XML document class.
        Dim signedXml As New SignedXml(xmlDocument)

        ' Find the "Signature" node and create a new
        Dim nodeList As XmlNodeList = xmlDocument.GetElementsByTagName("ds:Signature")   'xmlDocument.GetElementsByTagName("Signature")
        If nodeList.Count <> 1 Then
            Throw New Exception("Se produjo un error en la firma del documento")
        End If
        signedXml.LoadXml(CType(nodeList(0), XmlElement))
        ' Check the signature and return the result.
        Return signedXml.CheckSignature(Key, True)
    End Function

End Class
