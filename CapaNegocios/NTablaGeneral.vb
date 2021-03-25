Imports CapaDatos
Public Class NTablaGeneral
    Dim sql As New ClsConexion
#Region "Declarations"

    Private _idGeneral As String
    Private _idCodigo As String
    Private _descripcion As String
    Private _usuarioCrea As String
    Private _fechaCrea As System.DateTime
    Private _usuarioMod As String
    Private _fechaMod As System.DateTime
    Private _permiso As String

#End Region

#Region "Properties"

    Public Property IdGeneral As String
        Get
            Return _idGeneral
        End Get
        Set
            _idGeneral = Value
        End Set
    End Property

    Public Property IdCodigo As String
        Get
            Return _idCodigo
        End Get
        Set
            _idCodigo = Value
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return _descripcion
        End Get
        Set
            _descripcion = Value
        End Set
    End Property

    Public Property UsuarioCrea As String
        Get
            Return _usuarioCrea
        End Get
        Set
            _usuarioCrea = Value
        End Set
    End Property

    Public Property FechaCrea As System.DateTime
        Get
            Return _fechaCrea
        End Get
        Set
            _fechaCrea = Value
        End Set
    End Property

    Public Property UsuarioMod As String
        Get
            Return _usuarioMod
        End Get
        Set
            _usuarioMod = Value
        End Set
    End Property

    Public Property FechaMod As System.DateTime
        Get
            Return _fechaMod
        End Get
        Set
            _fechaMod = Value
        End Set
    End Property

    Public Property Permiso As String
        Get
            Return _permiso
        End Get
        Set
            _permiso = Value
        End Set
    End Property


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(ByVal idGeneral As String, ByVal idCodigo As String, ByVal descripcion As String, ByVal usuarioCrea As String, ByVal fechaCrea As System.DateTime, ByVal usuarioMod As String, ByVal fechaMod As System.DateTime, ByVal permiso As String)
        Me.New()
    End Sub

#End Region

#Region "Metodos"
    Public Function lista() As DataTable
        Return sql.EjecutarConsulta("F", "select IdCodigo, (rtrim(IdCodigo) +'  '+ rtrim(Descripcion)) as Descripcion from tablageneral where idgeneral='120'  AND IdCodigo='PV' order by 1").Tables(0)
    End Function
    'Public Function lista(d As String) As DataTable
    '    Return sql.EjecutarConsulta("F", "select IdCodigo, rtrim(IdCodigo) +'  '+ rtrim(Descripcion) from tablageneral where idgeneral='120'  AND IdCodigo='NP' order by 1").Tables(0)
    'End Function
    Public Function TipoGuia() As DataTable
        Dim cadena As String = "select idcodigo,descripcion from tablageneral where idgeneral='a5'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("tipoguia", cadena).Tables(0)
        Return dt
    End Function
    Public Function Familia_Articulo() As DataTable
        Dim cadena As String = "select  cast(0 as bit) as Flg,IdCodigo,Descripcion from Vfamilia "
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("vf", cadena).Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' Lista la condicion de entraga en la guia remisión(venta, consignacion entre otros
    ''' </summary>
    ''' <returns></returns>
    Public Function TipoGuia_Condicion() As DataTable
        Dim cadena As String = "select IdCodigo,Descripcion,Flag from VCondicion_entrega"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("tipoguia", cadena).Tables(0)
        Return dt
    End Function
    Public Function TipoGuia_CondicionId(IdCodigo As String) As String
        Dim cadena As String = "select IdCodigo,Descripcion,Flag from VCondicion_entrega where idcodigo='" & IdCodigo & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("tipoguia", cadena).Tables(0)
        Dim respuesta As String
        If dt.Rows.Count > 0 Then
            respuesta = dt.Rows(0).Item("Descripcion").ToString.Trim
        Else
            respuesta = ""
        End If
        Return respuesta
    End Function
    Public Function TipoGuia_Condicion(idcodigo As String) As String
        Dim cadena As String = "select IdCodigo,Descripcion,Flag from VCondicion_entrega where idcodigo='" & idcodigo & "'"
        Dim dt As New DataTable
        Dim respuesta As String
        dt = sql.EjecutarConsulta("tipoguia", cadena).Tables(0)
        If dt.Rows.Count > 0 Then
            respuesta = dt.Rows(0).Item("Flag").ToString.Trim
        Else
            respuesta = ""
        End If
        Return respuesta
    End Function

    Public Function Caja() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "Select IdCaja,Cajas,Serie from vcajas").Tables(0)
        Return dt
    End Function
    Public Function Turno() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "Select IdTurno,Turno,Hora from vturno").Tables(0)
        Return dt
    End Function
    Public Function TipoCliente() As DataTable
        Dim cadena As String = "Select IdCodigo,Descripcion from vtipocliente"
        Dim dt As DataTable = sql.EjecutarConsulta("d", cadena).Tables(0)
        Return dt
    End Function
    Public Function TipoMoneda() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("mon", "Select IdTipoMoneda,left(TipoMoneda,39) As TipoMoneda from vtipomoneda").Tables(0)
        Return dt
    End Function
    Public Function TipoOperacion() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("mon", "Select IdCodigo,Descripcion,referencia from VTipoOperacion").Tables(0)
        Return dt
    End Function
    Public Function UnidadMedida() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("Und", "select IdCodigo,Descripcion,UndRef from VUnidMed").Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' Relación de comprobantes para operaciones (Facturas, bolestas, partes y guias)
    ''' </summary>
    ''' <returns></returns>
    Public Function TipoDocumento() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "Select IdTipoDocumento,TipoDocumento,TdSunat from vTipoDocumento order by tipodocumento").Tables(0)
        Return dt
    End Function
    Public Function TipoDocumento(idtipodoc As String) As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "Select IdTipoDocumento,TipoDocumento,TdSunat from vTipoDocumento where idtipodocumento='" & idtipodoc & "'").Tables(0)
        Return dt
    End Function
    Public Function TipoDocumento_Compra() As DataTable
        Dim dt As New DataTable
        'dt = sql.EjecutarConsulta("d", " Select IdCodigo  As IdTipoDocumento, (rtrim(IdCodigo) +'  '+ rtrim(Descripcion)) as TipoDocumento,right(rtrim(descripcion),1) as TipoMov from tablageneral where idgeneral='105'  and idcodigo in('BV','FT') order by 1 ").Tables(0)
        dt = sql.EjecutarConsulta("d", " select IdCodigo  as IdTipoDocumento, (rtrim(IdCodigo) +'  '+ rtrim(Descripcion)) as TipoDocumento,right(rtrim(descripcion),1) as TipoMov from tablageneral where idgeneral='105'  and idcodigo in('BV','FT','NA','ND') order by 1 ").Tables(0)
        Return dt
    End Function
    Public Function TipoDocumento_Compra(t As String) As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", " select IdCodigo  as IdTipoDocumento, (rtrim(IdCodigo) +'  '+ rtrim(Descripcion)) as TipoDocumento,right(rtrim(descripcion),1) as TipoMov from tablageneral where idgeneral='105' order by 1 ").Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function tipodocumentoGeneral() As DataTable
        Dim st As String = " select 'TODO' AS idcodigo,'TODOS LOS DOCUMENTOS' as TipoDoc union all select Idcodigo, rtrim(ltrim(idcodigo))+'   '+rtrim(ltrim(descripcion)) as TipoDoc  "
        st += " from tablageneral where idgeneral='120' "
        Return sql.EjecutarConsulta("td", st).Tables(0)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de pagos para operaciones de venta y compra
    ''' </summary>
    ''' <returns></returns>
    Public Function TipoPago() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", " SELECT IdGeneral,IdCodigo,substring(rtrim(Descripcion),0,50) as Descripcion,substring(rtrim(Descripcion),50,60) as Codigo2 FROM TABLAGENERAL where idgeneral='52'").Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' lista de tipos de gastos varios tabla general TGV/ para gastos sin detalle
    ''' </summary>
    ''' <returns></returns>
    Public Function TipoGastoVario() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("df", "SELECT IdCodigo,Substring(Descripcion,0,49) as Descripcion,ltrim(rtrim(substring(descripcion,50,10))) as Cuenta FROM TABLAGENERAL WHERE IDGENERAL='TGV'").Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' Centro de costos administrativo con referencia centro de costo contable
    ''' </summary>
    ''' <returns></returns>
    Public Function centroCostoAdm() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("df", "SELECT IdCodigo,Substring(Descripcion,0,49) as Descripcion,ltrim(rtrim(substring(descripcion,50,10))) as CCosto FROM TABLAGENERAL WHERE IDGENERAL='10'").Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' Tipo de bien tabla 30
    ''' </summary>
    ''' <returns></returns>

    Public Function TipoBien() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("df", "select IdCodigo,(Codigo +' - '+Descripcion) as Descripcion,Codigo from vtipobien").Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' Muesta los solicitantes para los movimientos de entrada y salida
    ''' </summary>
    ''' <returns></returns>
    Public Function Solicitante() As DataTable
        Dim dt As New DataTable
        Dim g As New NTablaGeneral
        g.IdGeneral = "12"
        g.IdCodigo = Nothing
        Return ListaG(g)
    End Function


    Public Function Distribucion_Gasto() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("df", "select IdCodigo,(IdCodigo +' - '+Descripcion) as Descripcion from tablageneral where idgeneral='DI'").Tables(0)
        Return dt
    End Function
    Public Function Existe_TablaGeneral(d As NTablaGeneral) As Boolean
        Dim sParametro As Object() = {"@idgeneral", "@idcodigo"}
        Dim typeParam As Object() = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim vParametro As Object() = {d.IdGeneral, d.IdCodigo}
        Dim num As Integer = Me.sql.procedimiento_escalar("Existe_TablaGeneral", sParametro, vParametro, typeParam, 2)
        Return num = 1
    End Function
    Public Function Agencia() As DataTable
        Return sql.EjecutarConsulta("d", "SELECT IdAgencia,Descripcion FROM  Vagencia").Tables(0)
    End Function
    Public Function ParamGeneral(id As String) As DataTable
        Return sql.EjecutarConsulta("param", "select IdCodigo,Flag from VparametroGeneral where IdCodigo='" & id & "'").Tables(0)
        ''OPPRV/OPVTA
    End Function

    Public Function TipoDocumentoIdentidad() As DataTable
        Return sql.EjecutarConsulta("ds", "select IdTDocumento as IdTipoDocumento,'['+rtrim(IdTDocumento)+'] '+TipoDocumento as TipoDocumento from VtDocumento").Tables(0)
    End Function
    Public Function FormaPago() As DataTable
        Return sql.EjecutarConsulta("ds", "select cast(1 as bit) as Ok,IdFormaPago,left(FormaPago,49) as FormaPago,ltrim(rtrim(Grupo))as Grupo from vformapago").Tables(0)
    End Function

    Public Sub Agregar(d As NTablaGeneral)
        Dim parametros() As Object = {"@idGeneral", "@idCodigo", "@descripcion", "@usuarioCrea", "@fechaCrea", "@usuarioMod", "@fechaMod", "@permiso"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdGeneral, d.IdCodigo, d.Descripcion, d.UsuarioCrea, d.FechaCrea, d.UsuarioMod, d.FechaMod, d.Permiso}
        sql.EjecutarProcedure("Str_TablaGeneral_I", parametros, valores, tipoParametro, 8)
    End Sub
    Public Sub Actualizar(d As NTablaGeneral)
        Dim parametros() As Object = {"@idGeneral", "@idCodigo", "@descripcion", "@usuarioCrea", "@fechaCrea", "@usuarioMod", "@fechaMod", "@permiso"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdGeneral, d.IdCodigo, d.Descripcion, d.UsuarioCrea, d.FechaCrea, d.UsuarioMod, d.FechaMod, d.Permiso}
        sql.EjecutarProcedure("Str_TablaGeneral_U", parametros, valores, tipoParametro, 8)
    End Sub
    Public Sub Eliminar(d As NTablaGeneral)
        Dim parametros() As Object = {"@idGeneral", "@idCodigo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdGeneral, d.IdCodigo}
        sql.EjecutarProcedure("Str_TablaGeneral_D", parametros, valores, tipoParametro, 2)
    End Sub
    Public Function ListaG() As DataTable
        Dim parametros() As Object = {"@idGeneral", "@idCodigo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {DBNull.Value, DBNull.Value}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_TablaGeneral_S", parametros, valores, tipoParametro, 2).Tables(0)
        Return dt
    End Function
    Public Function ListaG(d As NTablaGeneral) As DataTable
        Dim parametros() As Object = {"@idGeneral", "@idCodigo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdGeneral, d.IdCodigo}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_TablaGeneral_S", parametros, valores, tipoParametro, 2).Tables(0)
        Return dt
    End Function
    Public Function ListaG(d As NTablaGeneral, comienzo As String, tamano As String) As DataTable
        Dim ca As String = " SELECT IdGeneral,IdCodigo,substring(rtrim(Descripcion)," & comienzo & "," & tamano & ") as "
        ca += " Descripcion,substring(rtrim(Descripcion)," & CInt(comienzo) + CInt(tamano) & "," & tamano & ") as IdCodigoAlter  "
        ca += " FROM TABLAGENERAL where idgeneral='" & d.IdGeneral & "'"
        Return sql.EjecutarConsulta("d", ca).Tables(0)
    End Function
    Public Function Ubigeos_Ciudades() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", " select IdCodigo,'['+rtrim(IdCodigo) +']-'+Descripcion as Descripcion,rtrim(Descripcion) as Descripcion1,right(IdCodigo,4) as Departamento,right(IdCodigo,2) as Provincias from tablageneral where idgeneral='UBI'").Tables(0)
        Return dt
    End Function



    Public Function Registro(d As NTablaGeneral) As NTablaGeneral
        Dim parametros() As Object = {"@idGeneral", "@idCodigo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdGeneral, d.IdCodigo}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_TablaGeneral_S", parametros, valores, tipoParametro, 2).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdGeneral = IIf(dt.Rows(0).Item("idGeneral") Is DBNull.Value, Nothing, dt.Rows(0).Item("idGeneral"))
            d.IdCodigo = IIf(dt.Rows(0).Item("idCodigo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCodigo"))
            d.Descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.UsuarioCrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.FechaCrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.UsuarioMod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.FechaMod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.Permiso = IIf(dt.Rows(0).Item("permiso") Is DBNull.Value, Nothing, dt.Rows(0).Item("permiso"))
        Else
            d.Descripcion = Nothing
            d.UsuarioCrea = Nothing
            d.FechaCrea = Nothing
            d.UsuarioMod = Nothing
            d.FechaMod = Nothing
            d.Permiso = Nothing
        End If
        Return d
    End Function

    Public Function TasaIGV() As Decimal
        Dim v_temp() = {"'A'"}
        Return Convert.ToDecimal(sql.ValorEscalar("dbo.FObtenerIGV", v_temp, 1))
    End Function
    Public Function CentroCosto() As DataTable
        Dim s As String = " select CAST(0 as bit)as Ok, IdCodigo,Descripcion from TablaGeneral "
        s += " where IdGeneral='10' order by Descripcion "
        Return sql.EjecutarConsulta("d", s).Tables(0)
    End Function


#End Region

End Class
