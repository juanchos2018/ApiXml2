'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace GetSunat_Valida
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://service.sunat.gob.pe", ConfigurationName:="GetSunat_Valida.billValidService")>  _
    Public Interface billValidService
        
        'CODEGEN: El parámetro 'cdpvalidado' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        <System.ServiceModel.OperationContractAttribute(Action:="urn:validaCDPcriterios", ReplyAction:="http://service.sunat.gob.pe/billValidService/validaCDPcriteriosResponse"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function validaCDPcriterios(ByVal request As GetSunat_Valida.validaCDPcriteriosRequest) As <System.ServiceModel.MessageParameterAttribute(Name:="cdpvalidado")> GetSunat_Valida.validaCDPcriteriosResponse
        
        'CODEGEN: El parámetro 'archivoverificado' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        <System.ServiceModel.OperationContractAttribute(Action:="urn:verificaCPEarchivo", ReplyAction:="http://service.sunat.gob.pe/billValidService/verificaCPEarchivoResponse"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function verificaCPEarchivo(ByVal request As GetSunat_Valida.verificaCPEarchivoRequest) As <System.ServiceModel.MessageParameterAttribute(Name:="archivoverificado")> GetSunat_Valida.verificaCPEarchivoResponse
    End Interface
    
    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://service.sunat.gob.pe")>  _
    Partial Public Class statusResponse
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private contentField() As Byte
        
        Private statusCodeField As String
        
        Private statusMessageField As String
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType:="base64Binary", Order:=0)>  _
        Public Property content() As Byte()
            Get
                Return Me.contentField
            End Get
            Set
                Me.contentField = value
                Me.RaisePropertyChanged("content")
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property statusCode() As String
            Get
                Return Me.statusCodeField
            End Get
            Set
                Me.statusCodeField = value
                Me.RaisePropertyChanged("statusCode")
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property statusMessage() As String
            Get
                Return Me.statusMessageField
            End Get
            Set
                Me.statusMessageField = value
                Me.RaisePropertyChanged("statusMessage")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="validaCDPcriterios", WrapperNamespace:="http://service.sunat.gob.pe", IsWrapped:=true)>  _
    Partial Public Class validaCDPcriteriosRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public rucEmisor As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=1),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public tipoCDP As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=2),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public serieCDP As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=3),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public numeroCDP As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=4),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public tipoDocIdReceptor As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=5),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public numeroDocIdReceptor As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=6),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public fechaEmision As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=7),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public importeTotal As Double
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=8),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public nroAutorizacion As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal rucEmisor As String, ByVal tipoCDP As String, ByVal serieCDP As String, ByVal numeroCDP As String, ByVal tipoDocIdReceptor As String, ByVal numeroDocIdReceptor As String, ByVal fechaEmision As String, ByVal importeTotal As Double, ByVal nroAutorizacion As String)
            MyBase.New
            Me.rucEmisor = rucEmisor
            Me.tipoCDP = tipoCDP
            Me.serieCDP = serieCDP
            Me.numeroCDP = numeroCDP
            Me.tipoDocIdReceptor = tipoDocIdReceptor
            Me.numeroDocIdReceptor = numeroDocIdReceptor
            Me.fechaEmision = fechaEmision
            Me.importeTotal = importeTotal
            Me.nroAutorizacion = nroAutorizacion
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="validaCDPcriteriosResponse", WrapperNamespace:="http://service.sunat.gob.pe", IsWrapped:=true)>  _
    Partial Public Class validaCDPcriteriosResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public cdpvalidado As GetSunat_Valida.statusResponse
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal cdpvalidado As GetSunat_Valida.statusResponse)
            MyBase.New
            Me.cdpvalidado = cdpvalidado
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="verificaCPEarchivo", WrapperNamespace:="http://service.sunat.gob.pe", IsWrapped:=true)>  _
    Partial Public Class verificaCPEarchivoRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public nombre As String
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=1),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public archivo As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal nombre As String, ByVal archivo As String)
            MyBase.New
            Me.nombre = nombre
            Me.archivo = archivo
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(WrapperName:="verificaCPEarchivoResponse", WrapperNamespace:="http://service.sunat.gob.pe", IsWrapped:=true)>  _
    Partial Public Class verificaCPEarchivoResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://service.sunat.gob.pe", Order:=0),  _
         System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public archivoverificado As GetSunat_Valida.statusResponse
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal archivoverificado As GetSunat_Valida.statusResponse)
            MyBase.New
            Me.archivoverificado = archivoverificado
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface billValidServiceChannel
        Inherits GetSunat_Valida.billValidService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class billValidServiceClient
        Inherits System.ServiceModel.ClientBase(Of GetSunat_Valida.billValidService)
        Implements GetSunat_Valida.billValidService
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function GetSunat_Valida_billValidService_validaCDPcriterios(ByVal request As GetSunat_Valida.validaCDPcriteriosRequest) As GetSunat_Valida.validaCDPcriteriosResponse Implements GetSunat_Valida.billValidService.validaCDPcriterios
            Return MyBase.Channel.validaCDPcriterios(request)
        End Function
        
        Public Function validaCDPcriterios(ByVal rucEmisor As String, ByVal tipoCDP As String, ByVal serieCDP As String, ByVal numeroCDP As String, ByVal tipoDocIdReceptor As String, ByVal numeroDocIdReceptor As String, ByVal fechaEmision As String, ByVal importeTotal As Double, ByVal nroAutorizacion As String) As GetSunat_Valida.statusResponse
            Dim inValue As GetSunat_Valida.validaCDPcriteriosRequest = New GetSunat_Valida.validaCDPcriteriosRequest()
            inValue.rucEmisor = rucEmisor
            inValue.tipoCDP = tipoCDP
            inValue.serieCDP = serieCDP
            inValue.numeroCDP = numeroCDP
            inValue.tipoDocIdReceptor = tipoDocIdReceptor
            inValue.numeroDocIdReceptor = numeroDocIdReceptor
            inValue.fechaEmision = fechaEmision
            inValue.importeTotal = importeTotal
            inValue.nroAutorizacion = nroAutorizacion
            Dim retVal As GetSunat_Valida.validaCDPcriteriosResponse = CType(Me,GetSunat_Valida.billValidService).validaCDPcriterios(inValue)
            Return retVal.cdpvalidado
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function GetSunat_Valida_billValidService_verificaCPEarchivo(ByVal request As GetSunat_Valida.verificaCPEarchivoRequest) As GetSunat_Valida.verificaCPEarchivoResponse Implements GetSunat_Valida.billValidService.verificaCPEarchivo
            Return MyBase.Channel.verificaCPEarchivo(request)
        End Function
        
        Public Function verificaCPEarchivo(ByVal nombre As String, ByVal archivo As String) As GetSunat_Valida.statusResponse
            Dim inValue As GetSunat_Valida.verificaCPEarchivoRequest = New GetSunat_Valida.verificaCPEarchivoRequest()
            inValue.nombre = nombre
            inValue.archivo = archivo
            Dim retVal As GetSunat_Valida.verificaCPEarchivoResponse = CType(Me,GetSunat_Valida.billValidService).verificaCPEarchivo(inValue)
            Return retVal.archivoverificado
        End Function
    End Class
End Namespace
