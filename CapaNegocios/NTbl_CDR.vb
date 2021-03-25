Imports CapaDatos
Public Class NTbl_CDR
    Dim sql As New ClsConexion
#Region "Declarations"
    Private _nroIdSunat As String
    Private _fechaRecepcion As System.DateTime
    Private _horaRecepcion As String
    Private _fechaCRD As System.DateTime
    Private _horaCDR As String
    Private _nota As String
    Private _nroDocEnviado As String
    Private _codRecepcion As String
    Private _descriponerror As String
    Private _nroDocFirmado As String
    Private _idAquiriente As String
    Private _observaciones As String
#End Region

#Region "Properties"

    Public Property NroIdSunat As String
        Get
            Return _nroIdSunat
        End Get
        Set
            _nroIdSunat = Value
        End Set
    End Property

    Public Property FechaRecepcion As System.DateTime
        Get
            Return _fechaRecepcion
        End Get
        Set
            _fechaRecepcion = Value
        End Set
    End Property

    Public Property HoraRecepcion As String
        Get
            Return _horaRecepcion
        End Get
        Set
            _horaRecepcion = Value
        End Set
    End Property

    Public Property FechaCRD As System.DateTime
        Get
            Return _fechaCRD
        End Get
        Set
            _fechaCRD = Value
        End Set
    End Property

    Public Property HoraCDR As String
        Get
            Return _horaCDR
        End Get
        Set
            _horaCDR = Value
        End Set
    End Property

    Public Property Nota As String
        Get
            Return _nota
        End Get
        Set
            _nota = Value
        End Set
    End Property

    Public Property NroDocEnviado As String
        Get
            Return _nroDocEnviado
        End Get
        Set
            _nroDocEnviado = Value
        End Set
    End Property

    Public Property CodRecepcion As String
        Get
            Return _codRecepcion
        End Get
        Set
            _codRecepcion = Value
        End Set
    End Property

    Public Property Descriponerror As String
        Get
            Return _descriponerror
        End Get
        Set
            _descriponerror = Value
        End Set
    End Property

    Public Property NroDocFirmado As String
        Get
            Return _nroDocFirmado
        End Get
        Set
            _nroDocFirmado = Value
        End Set
    End Property

    Public Property IdAquiriente As String
        Get
            Return _idAquiriente
        End Get
        Set
            _idAquiriente = Value
        End Set
    End Property

    Public Property Observaciones As String
        Get
            Return _observaciones
        End Get
        Set
            _observaciones = Value
        End Set
    End Property


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(ByVal nroIdSunat As String, ByVal fechaRecepcion As System.DateTime, ByVal horaRecepcion As String, ByVal fechaCRD As System.DateTime, ByVal horaCDR As String, ByVal nota As String, ByVal nroDocEnviado As String, ByVal codRecepcion As String, ByVal descriponerror As String, ByVal nroDocFirmado As String, ByVal idAquiriente As String, ByVal observaciones As String)
        Me.New()
    End Sub

#End Region
#Region "Metodo"
    Public Sub Agregar(d As NTbl_CDR)
        Dim parametros() As Object = {"@nroIdSunat", "@fechaRecepcion", "@horaRecepcion", "@fechaCRD", "@horaCDR", "@nota", "@nroDocEnviado", "@codRecepcion", "@descriponerror", "@nroDocFirmado", "@idAquiriente", "@observaciones"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.NroIdSunat, d.FechaRecepcion, d.HoraRecepcion, d.FechaCRD, d.HoraCDR, d.Nota, d.NroDocEnviado, d.CodRecepcion, d.Descriponerror, d.NroDocFirmado, d.IdAquiriente, d.Observaciones}
        sql.EjecutarProcedure("Str_AgregarCDR", parametros, valores, tipoParametro, 12)
    End Sub

    Public Function listar() As DataTable
        Dim ca As String = " select IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,GETDATE() as Hoy,DATEDIFF(day,FechaDocumento,GETDATE())as DiasTranscurridos, "
        ca += " dateadd(day,7,FechaDocumento)as PlazoMaximo,datediff(day,GETDATE(),dateadd(day,7,FechaDocumento))as DiasRestantes,IdCliente,NombreCliente,IdMoneda,ImporteTotal,NumeroOrden,Estado,FechaCrea,EstadoSunat from Comprobante "
        ca += " where ISNUMERIC(SERIE)=0 and (isnull(EstadoSunat,'1')='1' or  isnull(EstadoSunat,'1')='') "
        ca += " And idtipodocumento<>'BV' and importetotal<>0"
        Return sql.EjecutarConsulta("D", ca).Tables(0)

    End Function
#End Region


End Class

