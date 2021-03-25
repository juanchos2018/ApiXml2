Imports CapaDatos
Public Class NDet_Entidad
    Dim sql As New ClsConexion
#Region "Declarations"

    Private _id As Integer
    Private _idEntidad As String
    Private _emisor_Certificado As String
    Private _nroSerie_cert As String
    Private _fechaEmision As System.DateTime
    Private _fechaCaducidad As System.DateTime
    Private _estado As Boolean
    Private _pfx_file As Byte()
    Private _cer_file As Byte()
    Private _validado_por As String
    Private _dni_rep As String
    Private _nombre_rep As String
    Private _contacto As String
    Private _ruc As String
    Private _razonSocial As String

#End Region

#Region "Properties"

    Public Property id As Integer
        Get
            Return _id
        End Get
        Set
            _id = Value
        End Set
    End Property

    Public Property IdEntidad As String
        Get
            Return _idEntidad
        End Get
        Set
            _idEntidad = Value
        End Set
    End Property

    Public Property Emisor_Certificado As String
        Get
            Return _emisor_Certificado
        End Get
        Set
            _emisor_Certificado = Value
        End Set
    End Property

    Public Property NroSerie_cert As String
        Get
            Return _nroSerie_cert
        End Get
        Set
            _nroSerie_cert = Value
        End Set
    End Property

    Public Property FechaEmision As System.DateTime
        Get
            Return _fechaEmision
        End Get
        Set
            _fechaEmision = Value
        End Set
    End Property

    Public Property FechaCaducidad As System.DateTime
        Get
            Return _fechaCaducidad
        End Get
        Set
            _fechaCaducidad = Value
        End Set
    End Property

    Public Property Estado As Boolean
        Get
            Return _estado
        End Get
        Set
            _estado = Value
        End Set
    End Property

    Public Property pfx_file As Byte()
        Get
            Return _pfx_file
        End Get
        Set
            _pfx_file = Value
        End Set
    End Property

    Public Property cer_file As Byte()
        Get
            Return _cer_file
        End Get
        Set
            _cer_file = Value
        End Set
    End Property

    Public Property validado_por As String
        Get
            Return _validado_por
        End Get
        Set
            _validado_por = Value
        End Set
    End Property

    Public Property dni_rep As String
        Get
            Return _dni_rep
        End Get
        Set
            _dni_rep = Value
        End Set
    End Property

    Public Property nombre_rep As String
        Get
            Return _nombre_rep
        End Get
        Set
            _nombre_rep = Value
        End Set
    End Property

    Public Property contacto As String
        Get
            Return _contacto
        End Get
        Set
            _contacto = Value
        End Set
    End Property

    Public Property ruc As String
        Get
            Return _ruc
        End Get
        Set
            _ruc = Value
        End Set
    End Property

    Public Property RazonSocial As String
        Get
            Return _razonSocial
        End Get
        Set
            _razonSocial = Value
        End Set
    End Property


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Agrega un registro tipo certificado a la tabla det_entidad
    ''' </summary>
    ''' <param name="d"></param>
    Public Sub Agregar(d As NDet_Entidad)
        Dim parametros() As Object = {"@idEntidad", "@emisor_Certificado", "@nroSerie_cert", "@fechaEmision", "@fechaCaducidad", "@estado", "@pfx_file", "@cer_file", "@validado_por", "@dni_rep", "@nombre_rep", "@contacto", "@ruc", "@razonSocial"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Bit, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdEntidad, d.Emisor_Certificado, d.NroSerie_cert, d.FechaEmision, d.FechaCaducidad, d.Estado, d.pfx_file, d.cer_file, d.validado_por, d.dni_rep, d.nombre_rep, d.contacto, d.ruc, d.RazonSocial}
        sql.EjecutarProcedure("Str_Det_Entidad_I", parametros, valores, tipoParametro, 14)
    End Sub
    ''' <summary>
    ''' Actualizar la tabla certificado a la tabla det_entidad
    ''' </summary>
    ''' <param name="d"></param>
    Public Sub Actualizar(d As NDet_Entidad)
        Dim parametros() As Object = {"@idEntidad", "@emisor_Certificado", "@nroSerie_cert", "@fechaEmision", "@fechaCaducidad", "@estado", "@pfx_file", "@cer_file"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Bit, SqlDbType.VarBinary, SqlDbType.VarBinary}
        Dim valores() As Object = {d.IdEntidad, d.Emisor_Certificado, d.NroSerie_cert, d.FechaEmision, d.FechaCaducidad, d.Estado, d.pfx_file, d.cer_file}
        sql.EjecutarProcedure("Str_Det_Entidad_U", parametros, valores, tipoParametro, 14)
    End Sub
    Public Function Item(d As NDet_Entidad) As NDet_Entidad
        Dim parametros() As Object = {"@idEntidad"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.IdEntidad}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_Det_Entidad_S", parametros, valores, tipoParametro, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdEntidad = dt.Rows(0).Item("IdEntidad")
            d.nombre_rep = dt.Rows(0).Item("nombre_rep")
            d.NroSerie_cert = dt.Rows(0).Item("NroSerie_cert")
            d.RazonSocial = dt.Rows(0).Item("RazonSocial").ToString
            d.dni_rep = dt.Rows(0).Item("dni_rep")
            d.nombre_rep = dt.Rows(0).Item("nombre_rep")
            d.ruc = dt.Rows(0).Item("Ruc")
            d.validado_por = dt.Rows(0).Item("Validado_por")
            d.contacto = dt.Rows(0).Item("Contacto")
            d.Emisor_Certificado = dt.Rows(0).Item("Emisor_Certificado")
            d.FechaEmision = dt.Rows(0).Item("FechaEmision")
            d.FechaCaducidad = dt.Rows(0).Item("FechaCaducidad")
            d.cer_file = dt.Rows(0).Item("cer_file")
            d.pfx_file = dt.Rows(0).Item("pfx_file")
        End If
        Return d
    End Function

#End Region


End Class
