Public Module Parametros

#Region "Atributos"
    Private _pc_Servidor As String
    Private _pc_BaseDatos As String
    Private _pc_Usuario As String
    Private _pc_Contrasena As String
#End Region
#Region "Propiedades"
    Public Property pc_Servidor As String
        Get
            Return _pc_Servidor
        End Get
        Set(value As String)
            _pc_Servidor = value
        End Set
    End Property

    Public Property pc_BaseDatos As String
        Get
            Return _pc_BaseDatos
        End Get
        Set(value As String)
            _pc_BaseDatos = value
        End Set
    End Property

    Public Property pc_Usuario As String
        Get
            Return _pc_Usuario
        End Get
        Set(value As String)
            _pc_Usuario = value
        End Set
    End Property

    Public Property pc_Contrasena As String
        Get
            Return _pc_Contrasena
        End Get
        Set(value As String)
            _pc_Contrasena = value
        End Set
    End Property
#End Region
End Module
