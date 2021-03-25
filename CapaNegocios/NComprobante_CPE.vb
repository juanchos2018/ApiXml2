Imports CapaDatos
Public Class NComprobante_CPE
    Dim sql As New ClsConexion
#Region "Declarations"

#End Region

#Region "Properties"

    Public Property idagencia As String
    Public Property idalmacen As String
    Public Property idtipodocumento As String
    Public Property serie As String
    Public Property numerodocumento As String
    Public Property Tdsunat As String
    Public Property Ruc As String
    Public Property estado As String
    Public Property xml_zip As Byte()
    Public Property cdr_zip As Byte()
    Public Property pdf_pdf As Byte()
    Public Property correoenviao As Boolean


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region

    'Public Sub agregar(d As NComprobante_CPE)
    '    Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento", "@estado", "@xml_zip", "@cdr_zip", "@pdf_pdf"}
    '    Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarBinary}
    '    Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Estado, d.xml_zip, d.cdr_zip, d.pdf_pdf}
    '    sql.EjecutarProcedure("Str_Agregar_CPE", parametros, valores, tipoParametro, 9)
    'End Sub
    Public Sub agregar(d As NComprobante_CPE)
        Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento", "@estado", "@xml_zip", "@cdr_zip", "@pdf_pdf"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarBinary}
        Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Estado, d.xml_zip, d.cdr_zip, d.pdf_pdf}
        sql.EjecutarProcedure("Str_Comprobante_CPE_I", parametros, valores, tipoParametro, 9)
    End Sub
    Public Sub Actualizar(d As NComprobante_CPE)
        Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento", "@estado", "@xml_zip", "@cdr_zip", "@pdf_pdf"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarBinary}
        Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Estado, d.xml_zip, d.cdr_zip, d.pdf_pdf}
        sql.EjecutarProcedure("Str_Comprobante_CPE_U", parametros, valores, tipoParametro, 9)
    End Sub
    Public Sub Eliminar(d As NComprobante_CPE)
        Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento}
        sql.EjecutarProcedure("Str_Comprobante_CPE_D", parametros, valores, tipoParametro, 5)
    End Sub
    Public Sub Actualizar_CdR(d As NComprobante_CPE)
        Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento", "@cdr_zip"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary}
        Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.cdr_zip}
        sql.EjecutarProcedure("Str_uptCdr", parametros, valores, tipoParametro, 6)
    End Sub

    'Public Sub listaCPE(d As NComprobante_CPE)
    '    Dim parametros() As Object = {"@idAgencia", "@idAlmacen", "@idTipoDocumento", "@serie", "@numeroDocumento"}
    '    Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
    '    Dim valores() As Object = {d.idagencia, d.idalmacen, d.idtipodocumento, d.serie, d.numerodocumento}
    '    sql.EjecutarProcedure("Str_ComprobanteCPE_S", parametros, valores, tipoParametro, 5)
    'End Sub





    Public Function Registro(d As NComprobante_CPE) As NComprobante_CPE
        Dim parametros() As Object = {"@idagencia", "@idalmacen", "@idtipodocumento", "@serie", "@numerodocumento"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdAgencia, d.IdAlmacen, d.IdTipoDocumento, d.Serie, d.NumeroDocumento}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_Comprobante_CPE_S", parametros, valores, tipoParametro, 5).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdAgencia = IIf(dt.Rows(0).Item("idagencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idagencia"))
            d.IdAlmacen = IIf(dt.Rows(0).Item("idalmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("idalmacen"))
            d.IdTipoDocumento = IIf(dt.Rows(0).Item("idtipodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idtipodocumento"))
            d.Serie = IIf(dt.Rows(0).Item("serie") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie"))
            d.NumeroDocumento = IIf(dt.Rows(0).Item("numerodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("numerodocumento"))
            d.Estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.xml_zip = IIf(dt.Rows(0).Item("xml_zip") Is DBNull.Value, Nothing, dt.Rows(0).Item("xml_zip"))
            d.cdr_zip = IIf(dt.Rows(0).Item("cdr_zip") Is DBNull.Value, Nothing, dt.Rows(0).Item("cdr_zip"))
            d.pdf_pdf = IIf(dt.Rows(0).Item("pdf_pdf") Is DBNull.Value, Nothing, dt.Rows(0).Item("pdf_pdf"))
            d.correoenviao = IIf(dt.Rows(0).Item("correoenviao") Is DBNull.Value, Nothing, dt.Rows(0).Item("correoenviao"))
        Else
            d.IdAgencia = Nothing
            d.IdAlmacen = Nothing
            d.IdTipoDocumento = Nothing
            d.Serie = Nothing
            d.NumeroDocumento = Nothing
            d.Estado = Nothing
            d.xml_zip = Nothing
            d.cdr_zip = Nothing
            d.pdf_pdf = Nothing
            d.correoenviao = Nothing
        End If
        Return d
    End Function
    Public Function Lista() As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento from comprobante_cpe "
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", cadena).Tables(0)
        Return dt
    End Function
    Public Function existe(d As NComprobante_CPE) As Boolean
        Dim cadena As String = " select count(*) from comprobante_cpe where IdAlmacen='" & d.IdAlmacen & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' "
        cadena += " and serie='" & d.Serie & "' and NumeroDocumento='" & d.NumeroDocumento & "'"
        Dim dt As DataTable = sql.EjecutarConsulta("d", cadena).Tables(0)
        Dim bandera As Boolean = False
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) = "1" Then
                bandera = True
            Else
                bandera = False
            End If
        Else
            bandera = False
        End If
        Return bandera
    End Function
    Public Function xmlzip(d As NComprobante_CPE) As Byte()
        Dim cadena As String = " select xml_zip from comprobante_cpe "
        cadena += " where idalmacen='" & d.IdAlmacen & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' and Serie='" & d.Serie & "' and numerodocumento='" & d.NumeroDocumento & "' "
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", cadena).Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) IsNot DBNull.Value Then
                d.xml_zip = dt.Rows(0).Item(0)
            End If
        End If
        Return d.xml_zip
    End Function
    Public Function pdf(d As NComprobante_CPE) As Byte()
        Dim cadena As String = " select pdf_pdf from comprobante_cpe "
        cadena += " where idalmacen='" & d.IdAlmacen & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' and Serie='" & d.Serie & "' and numerodocumento='" & d.NumeroDocumento & "' "
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", cadena).Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) IsNot DBNull.Value Then
                d.pdf_pdf = dt.Rows(0).Item(0)
            End If
        End If
        Return d.pdf_pdf
    End Function
    Public Function cdrzip(d As NComprobante_CPE) As Byte()
        Dim cadena As String = " select cdr_zip from comprobante_cpe "
        cadena += " where idalmacen='" & d.IdAlmacen & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' and Serie='" & d.Serie & "' and numerodocumento='" & d.NumeroDocumento & "' "
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", cadena).Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) IsNot DBNull.Value Then
                d.cdr_zip = dt.Rows(0).Item(0)
            End If
        End If
        Return d.cdr_zip
    End Function

    Public Function listado(i As DateTime, f As DateTime) As DataTable
        'Dim dt As DataTable = sql.EjecutarConsulta("li", "exec dbo.Str_Lista_CPE").Tables(0)
        Dim parametros() As Object = {"@FechaI", "@FechaF"}
        Dim tipoParametro() As Object = {SqlDbType.DateTime, SqlDbType.DateTime}
        Dim valores() As Object = {i, f}
        Return sql.ProcedureSQL("Str_Lista_CPE", parametros, valores, tipoParametro, 2).Tables(0)
    End Function
    Public Function listadoV1(i As DateTime, f As DateTime) As DataTable
        'Dim dt As DataTable = sql.EjecutarConsulta("li", "exec dbo.Str_Lista_CPE").Tables(0)
        Dim parametros() As Object = {"@FechaI", "@FechaF"}
        Dim tipoParametro() As Object = {SqlDbType.DateTime, SqlDbType.DateTime}
        Dim valores() As Object = {i, f}
        Return sql.ProcedureSQL("Str_Lista_CPEv1", parametros, valores, tipoParametro, 2).Tables(0)
    End Function

    Public Function listado_ValidaCPE(i As DateTime, f As DateTime, td As String) As DataTable
        Dim parametros() As Object = {"@FechaI", "@FechaF", "@TipoDocumento"}
        Dim tipoParametro() As Object = {SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar}
        Dim valores() As Object = {i, f, td}
        Return sql.ProcedureSQL("Str_Lista_CPE_Validad", parametros, valores, tipoParametro, 3).Tables(0)
    End Function
End Class
