Imports CapaDatos

Public Class NNumeracion
    Dim sql As New ClsConexion


#Region "Declarations"

    Public Property idtipodocumento As String
    Public Property serie As String
    Public Property numeroinicial As Decimal
    Public Property numerofinal As Decimal
    Public Property descripcion As String
    Public Property numeroactual As Decimal
    Public Property usuariocrea As String
    Public Property fechacrea As System.DateTime
    Public Property usuariomod As String
    Public Property fechamod As System.DateTime
    Public Property formato As String
    Public Property idagencia As String
    Public Property tn_cestaci As String
    Public Property tn_cpuerto As String
    Public Property items As Integer
    Public Property idsubdiario As String
    Public Property idcuenta As String
    Public Property idcuentaventa As String
    Public Property ctacja As String
    Public Property idsubdiariocja As String
    Public Property idctaigv As String
    Public Property idctavalorvta As String
    Public Property idsubdiarioc As String
    Public Property idcuentac As String
    Public Property idctaigvc As String
    Public Property idctavalorvtac As String
    Public Property idcaja As String
    'Public Property idalmacen As String
    Public Property idalmacenC As String
    Public Property esitinerante As Boolean
    Public Property eszofratacna As Boolean

#End Region

#Region "Constructors"
    Public Sub New()
    End Sub
    Public Sub New(ByVal idTipoDocumento As String, ByVal serie As String, ByVal numeroInicial As Decimal, ByVal numeroFinal As Decimal, ByVal descripcion As String, ByVal numeroActual As Decimal, ByVal usuarioCrea As String, ByVal fechaCrea As System.DateTime, ByVal usuarioMod As String, ByVal fechaMod As System.DateTime, ByVal formato As String, ByVal idAgencia As String, ByVal tN_CESTACI As String, ByVal tN_CPUERTO As String, ByVal items As Integer, ByVal idSubDiario As String, ByVal idCuenta As String, ByVal idcuentaventa As String, ByVal ctaCja As String, ByVal idSubdiarioCja As String, ByVal idCtaIGV As String, ByVal idCtaValorVta As String, ByVal idSubdiarioC As String, ByVal idCuentaC As String, ByVal idCtaIGVC As String, ByVal idCtaValorVtaC As String, ByVal idCaja As String)
        Me.New()
    End Sub
#End Region

#Region "Metodos"
    Public Sub Incrementar(n As NNumeracion)
        sql.Editar("Numeracion", "NumeroActual=" & Val(n.NumeroActual).ToString, "idtipodocumento='" & n.IdTipoDocumento & "' and serie='" & n.Serie & "'")
    End Sub
    Public Function Item(n As NNumeracion) As NNumeracion
        Dim cadena As String = "select IdTipoDocumento,Serie,Numeroinicial,NumeroFinal,Descripcion,NumeroActual,UsuarioCrea,"
        cadena += " FechaCrea,UsuarioMod,FechaMod,Formato,IdAgencia,items,IdSubdiario,IdCuenta,idcuentaventa, "
        cadena += " IdCtaIGv,IdCtaValorVta,IdSubdiarioC,IdCuentaC,IdCtaIGVC,IdCtaValorVtaC from numeracion "
        cadena += " where idtipodocumento='" & n.idtipodocumento.Trim & "' and serie='" & n.serie.Trim & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("dt", cadena).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            With row
                n.idtipodocumento = .Item("IdTipoDocumento").ToString
                n.serie = .Item("Serie").ToString
                n.numeroinicial = .Item("NumeroInicial").ToString
                n.numerofinal = .Item("NumeroFinal").ToString
                n.numeroactual = .Item("NumeroActual").ToString
                n.descripcion = .Item("Descripcion").ToString
                n.usuariocrea = .Item("UsuarioCrea").ToString
                n.items = .Item("Items").ToString
                n.formato = .Item("Formato").ToString
                n.idagencia = .Item("IdAgencia").ToString
                n.idsubdiario = .Item("IdSubdiario").ToString
                n.idcuenta = .Item("IdCuenta").ToString
                n.idcuentaventa = .Item("IdCuentaventa").ToString
                n.idctaigv = .Item("IdCtaIGV").ToString
                n.idctavalorvta = .Item("IdCtaValorVta").ToString
                n.idsubdiarioc = .Item("IdSubdiarioC").ToString
                n.idcuentac = .Item("IdCuentaC").ToString
                n.idctaigvc = .Item("IdCtaIGVC").ToString
                n.idctavalorvtac = .Item("IdCtaValorVtaC").ToString
                n.formato = .Item("Formato")
            End With
        End If
        Return n
    End Function
    Public Function listar() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "select IdTipoDocumento,Serie from numeracion ").Tables(0)
        Return dt
    End Function
    Public Function Serie_lst(d As NNumeracion) As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "select Serie from numeracion where IdTipoDocumento='" & d.IdTipoDocumento & "' order by serie").Tables(0)
        Return dt
    End Function
    Public Function Serie_lst(d As NNumeracion, idcaja As String) As DataTable
        Dim dt As New DataTable
        If idcaja.Trim <> "" Then
            dt = sql.EjecutarConsulta("d", "select Serie from numeracion where IdTipoDocumento='" & d.IdTipoDocumento & "' and isnull(IdCaja,'')='" & idcaja & "' order by serie").Tables(0)
        Else
            dt = sql.EjecutarConsulta("d", "select Serie from numeracion where IdTipoDocumento='" & d.IdTipoDocumento & "' order by serie").Tables(0)
        End If
        Return dt
    End Function
    Public Sub Agregar(d As NNumeracion)
        Dim parametros() As Object = {"@idtipodocumento", "@serie", "@numeroinicial", "@numerofinal", "@descripcion", "@numeroactual", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@formato", "@idagencia", "@tn_cestaci", "@tn_cpuerto", "@items", "@idsubdiario", "@idcuenta", "@idcuentaventa", "@ctacja", "@idsubdiariocja", "@idctaigv", "@idctavalorvta", "@idsubdiarioc", "@idcuentac", "@idctaigvc", "@idctavalorvtac", "@idcaja", "@idalmacenc", "@esitinerante", "@eszofratacna"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.Bit}
        Dim valores() As Object = {d.idtipodocumento, d.serie, d.numeroinicial, d.numerofinal, d.descripcion, d.numeroactual, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.formato, d.idagencia, d.tn_cestaci, d.tn_cpuerto, d.items, d.idsubdiario, d.idcuenta, d.idcuentaventa, d.ctacja, d.idsubdiariocja, d.idctaigv, d.idctavalorvta, d.idsubdiarioc, d.idcuentac, d.idctaigvc, d.idctavalorvtac, d.idcaja, d.idalmacenc, d.esitinerante, d.eszofratacna}
        sql.EjecutarProcedure("Str_Numeracion_I", parametros, valores, tipoParametro, 30)
    End Sub
    Public Function Getserie(TipoDocumento As String, idAgencia As String) As DataTable

        Return sql.EjecutarConsulta("Serie", "select Serie from numeracion where idtipoDocumento='" + TipoDocumento +
         "' and idAgencia='" + idAgencia + "' order by serie").Tables(0)

    End Function

    Public Sub Actualizar(d As NNumeracion)
        Dim parametros() As Object = {"@idtipodocumento", "@serie", "@numeroinicial", "@numerofinal", "@descripcion", "@numeroactual", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@formato", "@idagencia", "@tn_cestaci", "@tn_cpuerto", "@items", "@idsubdiario", "@idcuenta", "@idcuentaventa", "@ctacja", "@idsubdiariocja", "@idctaigv", "@idctavalorvta", "@idsubdiarioc", "@idcuentac", "@idctaigvc", "@idctavalorvtac", "@idcaja", "@idalmacenc", "@esitinerante", "@eszofratacna"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.Bit}
        Dim valores() As Object = {d.idtipodocumento, d.serie, d.numeroinicial, d.numerofinal, d.descripcion, d.numeroactual, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.formato, d.idagencia, d.tn_cestaci, d.tn_cpuerto, d.items, d.idsubdiario, d.idcuenta, d.idcuentaventa, d.ctacja, d.idsubdiariocja, d.idctaigv, d.idctavalorvta, d.idsubdiarioc, d.idcuentac, d.idctaigvc, d.idctavalorvtac, d.idcaja, d.idalmacenc, d.esitinerante, d.eszofratacna}
        sql.EjecutarProcedure("Str_Numeracion_U", parametros, valores, tipoParametro, 30)
    End Sub
    Public Sub Eliminar(d As NNumeracion)
        Dim parametros() As Object = {"@idTipoDocumento", "@serie"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie}
        sql.EjecutarProcedure("Str_numeracion_D", parametros, valores, tipoParametro, 2)
    End Sub
    Public Function Lista() As DataTable
        Dim parametros() As Object = {"@idTipoDocumento", "@serie"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {DBNull.Value, DBNull.Value}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_numeracion_S", parametros, valores, tipoParametro, 2).Tables(0)
        Return dt
    End Function
    Public Function numeracionCaja() As DataTable
        Dim ca As String = " SELECT     n.IdTipoDocumento, n.Serie, n.Descripcion, n.NumeroInicial, n.NumeroFinal, n.NumeroActual, n.Formato, n.IdAgencia, n.IdSubDiario, n.IdCuenta, v.IdCaja, v.Cajas, n.items, n.IdCtaValorVta,  "
        ca += " n.IdSubdiarioC, n.IdCuentaC, n.IdCtaIGVC, n.IdCtaValorVtaC, n.UsuarioCrea, n.FechaCrea, n.usuarioMod, n.FechaMod,n.idalmacenC "
        ca += " FROM Numeracion AS n LEFT OUTER JOIN VCajas AS v ON n.IdCaja = v.IdCaja ORDER BY n.IdTipoDocumento, n.Serie "
        Return sql.EjecutarConsulta("d", ca).Tables(0)
    End Function
    Public Function Lista(d As NNumeracion) As DataTable
        Dim parametros() As Object = {"@idTipoDocumento", "@serie"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_numeracion_S", parametros, valores, tipoParametro, 2).Tables(0)
        Return dt
    End Function
    Public Function Registro(d As NNumeracion) As NNumeracion
        Dim parametros() As Object = {"@idTipoDocumento", "@serie"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_numeracion_S", parametros, valores, tipoParametro, 2).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdTipoDocumento = IIf(dt.Rows(0).Item("idTipoDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento"))
            d.Serie = IIf(dt.Rows(0).Item("serie") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie"))
            d.NumeroInicial = IIf(dt.Rows(0).Item("numeroInicial") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroInicial"))
            d.NumeroFinal = IIf(dt.Rows(0).Item("numeroFinal") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroFinal"))
            d.Descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.NumeroActual = IIf(dt.Rows(0).Item("numeroActual") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroActual"))
            d.UsuarioCrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.FechaCrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.usuarioMod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.FechaMod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.Formato = IIf(dt.Rows(0).Item("formato") Is DBNull.Value, Nothing, dt.Rows(0).Item("formato"))
            d.IdAgencia = IIf(dt.Rows(0).Item("idAgencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAgencia"))
            d.TN_CESTACI = IIf(dt.Rows(0).Item("tN_CESTACI") Is DBNull.Value, Nothing, dt.Rows(0).Item("tN_CESTACI"))
            d.TN_CPUERTO = IIf(dt.Rows(0).Item("tN_CPUERTO") Is DBNull.Value, Nothing, dt.Rows(0).Item("tN_CPUERTO"))
            d.items = IIf(dt.Rows(0).Item("items") Is DBNull.Value, Nothing, dt.Rows(0).Item("items"))
            d.IdSubDiario = IIf(dt.Rows(0).Item("idSubDiario") Is DBNull.Value, Nothing, dt.Rows(0).Item("idSubDiario"))
            d.IdCuenta = IIf(dt.Rows(0).Item("idCuenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCuenta"))
            d.idcuentaventa = IIf(dt.Rows(0).Item("idcuentaventa") Is DBNull.Value, Nothing, dt.Rows(0).Item("idcuentaventa"))
            d.CtaCja = IIf(dt.Rows(0).Item("ctaCja") Is DBNull.Value, Nothing, dt.Rows(0).Item("ctaCja"))
            d.IdSubdiarioCja = IIf(dt.Rows(0).Item("idSubdiarioCja") Is DBNull.Value, Nothing, dt.Rows(0).Item("idSubdiarioCja"))
            d.IdCtaIGV = IIf(dt.Rows(0).Item("idCtaIGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCtaIGV"))
            d.IdCtaValorVta = IIf(dt.Rows(0).Item("idCtaValorVta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCtaValorVta"))
            d.IdSubdiarioC = IIf(dt.Rows(0).Item("idSubdiarioC") Is DBNull.Value, Nothing, dt.Rows(0).Item("idSubdiarioC"))
            d.IdCuentaC = IIf(dt.Rows(0).Item("idCuentaC") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCuentaC"))
            d.IdCtaIGVC = IIf(dt.Rows(0).Item("idCtaIGVC") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCtaIGVC"))
            d.IdCtaValorVtaC = IIf(dt.Rows(0).Item("idCtaValorVtaC") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCtaValorVtaC"))
            d.idcaja = IIf(dt.Rows(0).Item("idCaja") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCaja"))
            d.idalmacenC = IIf(dt.Rows(0).Item("idAlmacenC") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAlmacenC"))
            d.esitinerante = IIf(dt.Rows(0).Item("esitinerante") Is DBNull.Value, Nothing, dt.Rows(0).Item("esitinerante"))
            d.eszofratacna = IIf(dt.Rows(0).Item("eszofratacna") Is DBNull.Value, Nothing, dt.Rows(0).Item("eszofratacna"))
        Else
            d.NumeroInicial = Nothing
            d.NumeroFinal = Nothing
            d.Descripcion = Nothing
            d.NumeroActual = Nothing
            d.UsuarioCrea = Nothing
            d.FechaCrea = Nothing
            d.usuarioMod = Nothing
            d.FechaMod = Nothing
            d.Formato = Nothing
            d.IdAgencia = Nothing
            d.TN_CESTACI = Nothing
            d.TN_CPUERTO = Nothing
            d.items = Nothing
            d.IdSubDiario = Nothing
            d.IdCuenta = Nothing
            d.idcuentaventa = Nothing
            d.CtaCja = Nothing
            d.IdSubdiarioCja = Nothing
            d.IdCtaIGV = Nothing
            d.IdCtaValorVta = Nothing
            d.IdSubdiarioC = Nothing
            d.IdCuentaC = Nothing
            d.IdCtaIGVC = Nothing
            d.IdCtaValorVtaC = Nothing
            d.idcaja = Nothing
            d.idalmacenC = Nothing
            d.esitinerante = Nothing
            d.eszofratacna = Nothing

        End If
        Return d
    End Function
#End Region
End Class
