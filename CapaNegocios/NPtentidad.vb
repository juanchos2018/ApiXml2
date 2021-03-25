Imports CapaDatos
Public Class NPtentidad
    Dim sql As New ClsConexion
#Region "Declarations"

    Public Property identidad As String
    Public Property nombre As String
    Public Property direccion As String
    Public Property ruc As String
    Public Property pais As String
    Public Property departamento As String
    Public Property provincia As String
    Public Property distrito As String
    Public Property bdref As String
    Public Property entidadant As String
    Public Property anioejercicio As String
    Public Property contacto As String
    Public Property tel1 As String
    Public Property tel2 As String
    Public Property fax As String
    Public Property email As String
    Public Property usuariocrea As String
    Public Property fechacrea As System.DateTime
    Public Property usuariomod As String
    Public Property fechamod As System.DateTime
    Public Property idestado As String
    Public Property agentepercepcion As Boolean
    Public Property idtipodocumento As String
    Public Property codubigeo As String
    Public Property rutapfx As String
    Public Property rutacer As String
    Public Property pws As String
    Public Property rssunat As String
    Public Property nombrecomercial As String
    Public Property signalias As String
    Public Property rssunat1 As String
    Public Property logo As String
    Public Property url As String
    Public Property user_sol As String
    Public Property pws_sol As String
    Public Property rsper As String
    Public Property rsret As String

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region


#Region "Metodos"
    Public Sub Agregar(d As NPtentidad)

        Dim parametros() As Object = {"@identidad", "@nombre", "@direccion", "@ruc", "@pais", "@departamento", "@provincia", "@distrito", "@bdref", "@entidadant", "@anioejercicio", "@contacto", "@tel1", "@tel2", "@fax", "@email", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@idestado", "@agentepercepcion", "@idtipodocumento", "@codubigeo", "@rutapfx", "@rutacer", "@pws", "@rssunat", "@nombrecomercial", "@signalias", "@rssunat1", "@logo", "@url", "@user_sol", "@pws_sol", "@rsper", "@rsret"}
        Dim tipoParametro() As Object = {SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.Bit, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.identidad, d.nombre, d.direccion, d.ruc, d.pais, d.departamento, d.provincia, d.distrito, d.bdref, d.entidadant, d.anioejercicio, d.contacto, d.tel1, d.tel2, d.fax, d.email, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.idestado, d.agentepercepcion, d.idtipodocumento, d.codubigeo, d.rutapfx, d.rutacer, d.pws, d.rssunat, d.nombrecomercial, d.signalias, d.rssunat1, d.logo, d.url, d.user_sol, d.pws_sol, d.rsper, d.rsret}
        sql.EjecutarProcedure("Str_PTentidad_I", parametros, valores, tipoParametro, 37)
    End Sub
    Public Sub Actualizar(d As NPtentidad)
        Dim parametros() As Object = {"@identidad", "@nombre", "@direccion", "@ruc", "@pais", "@departamento", "@provincia", "@distrito", "@bdref", "@entidadant", "@anioejercicio", "@contacto", "@tel1", "@tel2", "@fax", "@email", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@idestado", "@agentepercepcion", "@idtipodocumento", "@codubigeo", "@rutapfx", "@rutacer", "@pws", "@rssunat", "@nombrecomercial", "@signalias", "@rssunat1", "@logo", "@url", "@user_sol", "@pws_sol", "@rsper", "@rsret"}
        Dim tipoParametro() As Object = {SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.Bit, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.identidad, d.nombre, d.direccion, d.ruc, d.pais, d.departamento, d.provincia, d.distrito, d.bdref, d.entidadant, d.anioejercicio, d.contacto, d.tel1, d.tel2, d.fax, d.email, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.idestado, d.agentepercepcion, d.idtipodocumento, d.codubigeo, d.rutapfx, d.rutacer, d.pws, d.rssunat, d.nombrecomercial, d.signalias, d.rssunat1, d.logo, d.url, d.user_sol, d.pws_sol, d.rsper, d.rsret}
        sql.EjecutarProcedure("Str_PTentidad_U", parametros, valores, tipoParametro, 37)
    End Sub
    Public Sub Eliminar(d As NPtentidad)
        Dim parametros() As Object = {"@identidad"}
        Dim tipoParametro() As Object = {SqlDbType.Char}
        Dim valores() As Object = {d.identidad}
        sql.EjecutarProcedure("Str_PTentidad_D", parametros, valores, tipoParametro, 1)
    End Sub
    Public Function Lista() As DataTable
        Dim parametros() As Object = {"@identidad"}
        Dim tipoParametro() As Object = {SqlDbType.Char}
        Dim valores() As Object = {DBNull.Value}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_PTentidad_S", parametros, valores, tipoParametro, 1).Tables(0)
        Return dt
    End Function
    Public Function Lista(d As NPtentidad) As DataTable
        Dim parametros() As Object = {"@identidad"}
        Dim tipoParametro() As Object = {SqlDbType.Char}
        Dim valores() As Object = {d.identidad}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_PTentidad_S", parametros, valores, tipoParametro, 1).Tables(0)
        Return dt
    End Function
    Public Function Registro(d As NPtentidad) As NPtentidad
        Dim parametros() As Object = {"@identidad"}
        Dim tipoParametro() As Object = {SqlDbType.Char}
        Dim valores() As Object = {d.identidad}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_PTentidad_S", parametros, valores, tipoParametro, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            d.identidad = IIf(dt.Rows(0).Item("identidad") Is DBNull.Value, Nothing, dt.Rows(0).Item("identidad"))
            d.nombre = IIf(dt.Rows(0).Item("nombre") Is DBNull.Value, Nothing, dt.Rows(0).Item("nombre"))
            d.direccion = IIf(dt.Rows(0).Item("direccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("direccion"))
            d.ruc = IIf(dt.Rows(0).Item("ruc") Is DBNull.Value, Nothing, dt.Rows(0).Item("ruc"))
            d.pais = IIf(dt.Rows(0).Item("pais") Is DBNull.Value, Nothing, dt.Rows(0).Item("pais"))
            d.departamento = IIf(dt.Rows(0).Item("departamento") Is DBNull.Value, Nothing, dt.Rows(0).Item("departamento"))
            d.provincia = IIf(dt.Rows(0).Item("provincia") Is DBNull.Value, Nothing, dt.Rows(0).Item("provincia"))
            d.distrito = IIf(dt.Rows(0).Item("distrito") Is DBNull.Value, Nothing, dt.Rows(0).Item("distrito"))
            d.bdref = IIf(dt.Rows(0).Item("bdref") Is DBNull.Value, Nothing, dt.Rows(0).Item("bdref"))
            d.entidadant = IIf(dt.Rows(0).Item("entidadant") Is DBNull.Value, Nothing, dt.Rows(0).Item("entidadant"))
            d.anioejercicio = IIf(dt.Rows(0).Item("anioejercicio") Is DBNull.Value, Nothing, dt.Rows(0).Item("anioejercicio"))
            d.contacto = IIf(dt.Rows(0).Item("contacto") Is DBNull.Value, Nothing, dt.Rows(0).Item("contacto"))
            d.tel1 = IIf(dt.Rows(0).Item("tel1") Is DBNull.Value, Nothing, dt.Rows(0).Item("tel1"))
            d.tel2 = IIf(dt.Rows(0).Item("tel2") Is DBNull.Value, Nothing, dt.Rows(0).Item("tel2"))
            d.fax = IIf(dt.Rows(0).Item("fax") Is DBNull.Value, Nothing, dt.Rows(0).Item("fax"))
            d.email = IIf(dt.Rows(0).Item("email") Is DBNull.Value, Nothing, dt.Rows(0).Item("email"))
            d.usuariocrea = IIf(dt.Rows(0).Item("usuariocrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuariocrea"))
            d.fechacrea = IIf(dt.Rows(0).Item("fechacrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechacrea"))
            d.usuariomod = IIf(dt.Rows(0).Item("usuariomod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuariomod"))
            d.fechamod = IIf(dt.Rows(0).Item("fechamod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechamod"))
            d.idestado = IIf(dt.Rows(0).Item("idestado") Is DBNull.Value, Nothing, dt.Rows(0).Item("idestado"))
            d.agentepercepcion = IIf(dt.Rows(0).Item("agentepercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("agentepercepcion"))
            d.idtipodocumento = IIf(dt.Rows(0).Item("idtipodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idtipodocumento"))
            d.codubigeo = IIf(dt.Rows(0).Item("codubigeo") Is DBNull.Value, Nothing, dt.Rows(0).Item("codubigeo"))
            d.rutapfx = IIf(dt.Rows(0).Item("rutapfx") Is DBNull.Value, Nothing, dt.Rows(0).Item("rutapfx"))
            d.rutacer = IIf(dt.Rows(0).Item("rutacer") Is DBNull.Value, Nothing, dt.Rows(0).Item("rutacer"))
            d.pws = IIf(dt.Rows(0).Item("pws") Is DBNull.Value, Nothing, dt.Rows(0).Item("pws"))
            d.rssunat = IIf(dt.Rows(0).Item("rssunat") Is DBNull.Value, Nothing, dt.Rows(0).Item("rssunat"))
            d.nombrecomercial = IIf(dt.Rows(0).Item("nombrecomercial") Is DBNull.Value, Nothing, dt.Rows(0).Item("nombrecomercial"))
            d.signalias = IIf(dt.Rows(0).Item("signalias") Is DBNull.Value, Nothing, dt.Rows(0).Item("signalias"))
            d.rssunat1 = IIf(dt.Rows(0).Item("rssunat1") Is DBNull.Value, Nothing, dt.Rows(0).Item("rssunat1"))
            d.logo = IIf(dt.Rows(0).Item("logo") Is DBNull.Value, Nothing, dt.Rows(0).Item("logo"))
            d.url = IIf(dt.Rows(0).Item("url") Is DBNull.Value, Nothing, dt.Rows(0).Item("url"))
            d.user_sol = IIf(dt.Rows(0).Item("user_sol") Is DBNull.Value, Nothing, dt.Rows(0).Item("user_sol"))
            d.pws_sol = IIf(dt.Rows(0).Item("pws_sol") Is DBNull.Value, Nothing, dt.Rows(0).Item("pws_sol"))
            d.rsper = IIf(dt.Rows(0).Item("rsper") Is DBNull.Value, Nothing, dt.Rows(0).Item("rsper"))
            d.rsret = IIf(dt.Rows(0).Item("rsret") Is DBNull.Value, Nothing, dt.Rows(0).Item("rsret"))
        Else
            d.identidad = Nothing
            d.nombre = Nothing
            d.direccion = Nothing
            d.ruc = Nothing
            d.pais = Nothing
            d.departamento = Nothing
            d.provincia = Nothing
            d.distrito = Nothing
            d.bdref = Nothing
            d.entidadant = Nothing
            d.anioejercicio = Nothing
            d.contacto = Nothing
            d.tel1 = Nothing
            d.tel2 = Nothing
            d.fax = Nothing
            d.email = Nothing
            d.usuariocrea = Nothing
            d.fechacrea = Nothing
            d.usuariomod = Nothing
            d.fechamod = Nothing
            d.idestado = Nothing
            d.agentepercepcion = Nothing
            d.idtipodocumento = Nothing
            d.codubigeo = Nothing
            d.rutapfx = Nothing
            d.rutacer = Nothing
            d.pws = Nothing
            d.rssunat = Nothing
            d.nombrecomercial = Nothing
            d.signalias = Nothing
            d.rssunat1 = Nothing
            d.logo = Nothing
            d.url = Nothing
            d.user_sol = Nothing
            d.pws_sol = Nothing
            d.rsper = Nothing
            d.rsret = Nothing
        End If
        Return d
    End Function
    Public Function item(d As NPtentidad) As NPtentidad
        Dim parametros() As Object = {"@IdEntidad"}
        Dim tipoparametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.IdEntidad}
        Dim dt As DataTable = sql.ProcedureSQL("Str_FndPtentidad", parametros, valores, tipoparametro, 1).Tables(0)
        If dt.Rows.Count Then
            With dt.Rows(0)
                d.IdEntidad = .Item("IdEntidad")
                d.Nombre = .Item("Nombre")
                d.NombreComercial = .Item("NombreComercial")
                d.Direccion = .Item("Direccion")
                d.RUC = .Item("RUC")
                d.Pais = .Item("Pais")
                d.Departamento = .Item("Departamento")
                d.Provincia = .Item("Provincia")
                d.Distrito = .Item("Distrito")
                d.Contacto = .Item("Contacto")
                d.Tel1 = .Item("Tel1")
                d.Tel2 = .Item("Tel2")
                d.Fax = .Item("Fax")
                d.EMail = .Item("Email")
                d.UsuarioCrea = .Item("UsuarioCrea")
                d.FechaCrea = .Item("FechaCrea")
                d.IdTipoDocumento = .Item("IdTipoDocumento")
                d.CodUbigeo = .Item("CodUbigeo")
                d.rutacer = .Item("rutacer")
                d.rutapfx = .Item("rutapfx")
                d.pws = .Item("pws")
                d.RsSunat = .Item("RsSunat")
                d.RsSunat1 = .Item("RsSunat1")
                d.SignAlias = .Item("SignAlias")
                d.logo = .Item("logo")
                d.Url = .Item("Url")
                d.User_Sol = .Item("User_Sol")
                d.pws_sol = .Item("pws_sol")
                d.anioejercicio = .Item("AnioEjercicio")
            End With
        End If
        Return d
    End Function
    Public Function itemTbl(d As NPtentidad) As DataTable
        Dim parametros() As Object = {"@IdEntidad"}
        Dim tipoparametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.IdEntidad}
        Dim dt As DataTable = sql.ProcedureSQL("Str_FndPtentidad", parametros, valores, tipoparametro, 1).Tables(0)
        Return dt
    End Function
    '''' <summary>
    '''' Devuelve la relacion de bases de datos del sistema comercial
    '''' </summary>
    '''' <returns></returns>
    'Public Function listadb() As DataSet
    '    Dim dt As DataSet = sql.EjecutarConsulta("listada", "exec listadb")
    '    Return dt
    'End Function

    ''' <summary>
    '''  Devuelve la relacion de bases de datos del sistema comercial
    ''' </summary>
    ''' <returns></returns>
    Public Function Lista_emrpesas() As DataTable
        Return sql.EjecutarConsulta("listado", "exec Str_ListaBases").Tables(0)
    End Function
    Public Function Lista_Tablas(empresa As String) As DataTable
        Dim s As String
        s = " select CAST(0 AS BIT) AS OK,TABLE_NAME as Tabla from " & empresa & ".INFORMATION_SCHEMA.TABLES  where TABLE_TYPE = 'BASE TABLE' "
        s += " and TABLE_NAME IN('COMPROBANTE','DETALLECOMPROBANTE','CLIENTE') "
        Return sql.EjecutarConsulta("d", s).Tables(0)
    End Function

    Public Function cadenaconexion() As String
        Dim s As New CapaDatos.ClsConexion
        Return s.CadenaConexion
    End Function

#End Region

End Class
