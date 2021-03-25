Imports CapaDatos
Public Class NAlmacen
    Dim sql As New ClsConexion
#Region "Declarations"

    Public Property idalmacen As String
    Public Property agencia As String
    Public Property descripcion As String
    Public Property direccion As String
    Public Property distrito As String
    Public Property telefono As String
    Public Property controlnumeracion As String
    Public Property numeroentrada As Decimal
    Public Property numerosalida As Decimal
    Public Property serieguiaremision As Decimal
    Public Property numeracionguiaremision As Decimal
    Public Property numerofinalguiaremision As Decimal
    Public Property usuariocrea As String
    Public Property usuariomod As String
    Public Property fechacrea As System.DateTime
    Public Property fechamod As System.DateTime
    Public Property tipoalmacen As String
    Public Property provincia As String
    Public Property departamento As String
    Public Property a1_ccodcli As String
    Public Property descripcion2 As String
    Public Property direccion2 As String
    Public Property a1_ccosto As String
    Public Property ubigeo As String
    Public Property CodEstableSunat As String



#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

#Region "Metodos"
    Public Sub Agregar(d As NAlmacen)

        Dim parametros() As Object = {"@idAlmacen", "@agencia", "@descripcion", "@direccion", "@distrito", "@telefono", "@controlNumeracion", "@numeroEntrada", "@numeroSalida", "@serieGuiaRemision", "@numeracionGuiaRemision", "@numeroFinalGuiaRemision", "@usuarioCrea", "@usuarioMod", "@fechaCrea", "@fechaMod", "@tipoAlmacen", "@provincia", "@departamento", "@a1_CCODCLI", "@descripcion2", "@direccion2", "@a1_CCOSTO", "@ubigeo", "@CodEstableSunat"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen, d.agencia, d.descripcion, d.direccion, d.distrito, d.telefono, d.controlnumeracion, d.numeroentrada, d.numerosalida, d.serieguiaremision, d.numeracionguiaremision, d.numerofinalguiaremision, d.usuariocrea, d.usuariomod, d.fechacrea, d.fechamod, d.tipoalmacen, d.provincia, d.departamento, d.a1_ccodcli, d.descripcion2, d.direccion2, d.a1_ccosto, d.ubigeo, d.CodEstableSunat}
        sql.EjecutarProcedure("Str_Almacen_I", parametros, valores, tipoParametro, 25)
    End Sub
    Public Sub Actualizar(d As NAlmacen)
        Dim parametros() As Object = {"@idAlmacen", "@agencia", "@descripcion", "@direccion", "@distrito", "@telefono", "@controlNumeracion", "@numeroEntrada", "@numeroSalida", "@serieGuiaRemision", "@numeracionGuiaRemision", "@numeroFinalGuiaRemision", "@usuarioCrea", "@usuarioMod", "@fechaCrea", "@fechaMod", "@tipoAlmacen", "@provincia", "@departamento", "@a1_CCODCLI", "@descripcion2", "@direccion2", "@a1_CCOSTO", "@ubigeo", "@CodEstableSunat"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen, d.agencia, d.descripcion, d.direccion, d.distrito, d.telefono, d.controlnumeracion, d.numeroentrada, d.numerosalida, d.serieguiaremision, d.numeracionguiaremision, d.numerofinalguiaremision, d.usuariocrea, d.usuariomod, d.fechacrea, d.fechamod, d.tipoalmacen, d.provincia, d.departamento, d.a1_ccodcli, d.descripcion2, d.direccion2, d.a1_ccosto, d.ubigeo, d.CodEstableSunat}
        sql.EjecutarProcedure("Str_Almacen_U", parametros, valores, tipoParametro, 25)
    End Sub
    Public Sub Eliminar(d As NAlmacen)
        Dim parametros() As Object = {"@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen}
        Sql.EjecutarProcedure("Str_Almacen_D", parametros, valores, tipoParametro, 1)
    End Sub
    Public Function Lista() As DataTable
        Dim parametros() As Object = {"@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {DBNull.Value}
        Dim dt As New DataTable
        dt = Sql.ProcedureSQL("Str_Almacen_S", parametros, valores, tipoParametro, 1).Tables(0)
        Return dt
    End Function
    Public Function Lista(d As NAlmacen) As DataTable
        Dim parametros() As Object = {"@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen}
        Dim dt As New DataTable
        dt = Sql.ProcedureSQL("Str_Almacen_S", parametros, valores, tipoParametro, 1).Tables(0)
        Return dt
    End Function
    Public Function Existe_Almacen(d As NAlmacen)
        Dim parametros() As Object = {"@idalmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen}
        Dim resultado As Integer
        Dim bandera As Boolean = False
        resultado = sql.procedimiento_escalar("Existe_Almacen", parametros, valores, tipoParametro, 1)
        If resultado = 1 Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function
    Public Function Registro(d As NAlmacen) As NAlmacen
        Dim parametros() As Object = {"@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.idalmacen}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_Almacen_S", parametros, valores, tipoParametro, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            d.idalmacen = IIf(dt.Rows(0).Item("idAlmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAlmacen"))
            d.agencia = IIf(dt.Rows(0).Item("agencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("agencia"))
            d.descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.direccion = IIf(dt.Rows(0).Item("direccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("direccion"))
            d.distrito = IIf(dt.Rows(0).Item("distrito") Is DBNull.Value, Nothing, dt.Rows(0).Item("distrito"))
            d.telefono = IIf(dt.Rows(0).Item("telefono") Is DBNull.Value, Nothing, dt.Rows(0).Item("telefono"))
            d.controlnumeracion = IIf(dt.Rows(0).Item("controlNumeracion") Is DBNull.Value, Nothing, dt.Rows(0).Item("controlNumeracion"))
            d.numeroentrada = IIf(dt.Rows(0).Item("numeroEntrada") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroEntrada"))
            d.numerosalida = IIf(dt.Rows(0).Item("numeroSalida") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroSalida"))
            d.serieguiaremision = IIf(dt.Rows(0).Item("serieGuiaRemision") Is DBNull.Value, Nothing, dt.Rows(0).Item("serieGuiaRemision"))
            d.numeracionguiaremision = IIf(dt.Rows(0).Item("numeracionGuiaRemision") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeracionGuiaRemision"))
            d.numerofinalguiaremision = IIf(dt.Rows(0).Item("numeroFinalGuiaRemision") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroFinalGuiaRemision"))
            d.usuariocrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.usuariomod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.fechacrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.fechamod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.tipoalmacen = IIf(dt.Rows(0).Item("tipoAlmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoAlmacen"))
            d.provincia = IIf(dt.Rows(0).Item("provincia") Is DBNull.Value, Nothing, dt.Rows(0).Item("provincia"))
            d.departamento = IIf(dt.Rows(0).Item("departamento") Is DBNull.Value, Nothing, dt.Rows(0).Item("departamento"))
            d.a1_ccodcli = IIf(dt.Rows(0).Item("a1_CCODCLI") Is DBNull.Value, Nothing, dt.Rows(0).Item("a1_CCODCLI"))
            d.descripcion2 = IIf(dt.Rows(0).Item("descripcion2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion2"))
            d.direccion2 = IIf(dt.Rows(0).Item("direccion2") Is DBNull.Value, Nothing, dt.Rows(0).Item("direccion2"))
            d.a1_ccosto = IIf(dt.Rows(0).Item("a1_CCOSTO") Is DBNull.Value, Nothing, dt.Rows(0).Item("a1_CCOSTO"))
            d.ubigeo = IIf(dt.Rows(0).Item("ubigeo") Is DBNull.Value, Nothing, dt.Rows(0).Item("ubigeo"))
            d.CodEstableSunat = IIf(dt.Rows(0).Item("CodEstableSunat") Is DBNull.Value, Nothing, dt.Rows(0).Item("CodEstableSunat"))
        Else
            d.idalmacen = Nothing
            d.agencia = Nothing
            d.descripcion = Nothing
            d.direccion = Nothing
            d.distrito = Nothing
            d.telefono = Nothing
            d.controlnumeracion = Nothing
            d.numeroentrada = Nothing
            d.numerosalida = Nothing
            d.serieguiaremision = Nothing
            d.numeracionguiaremision = Nothing
            d.numerofinalguiaremision = Nothing
            d.usuariocrea = Nothing
            d.usuariomod = Nothing
            d.fechacrea = Nothing
            d.fechamod = Nothing
            d.tipoalmacen = Nothing
            d.provincia = Nothing
            d.departamento = Nothing
            d.a1_ccodcli = Nothing
            d.descripcion2 = Nothing
            d.direccion2 = Nothing
            d.a1_ccosto = Nothing
            d.ubigeo = Nothing
            d.CodEstableSunat = Nothing
        End If
        Return d
    End Function

    Public Sub IncrementarSalida(n As NAlmacen)
        Sql.Editar("almacen", "NumeroSalida=" & n.numerosalida & "", "idalmacen='" & n.idalmacen & "'")
    End Sub
    Public Sub IncrementarIngreso(n As NAlmacen)
        Sql.Editar("almacen", "NumeroEntrada=" & n.numeroentrada & "", "idalmacen='" & n.idalmacen & "'")
    End Sub
#End Region

End Class
