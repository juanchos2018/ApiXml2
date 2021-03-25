Imports CapaDatos

Public Class NCuotas
    Dim sql As New ClsConexion
    Public Property IdTipoDocumento As String
    Public Property Serie As String
    Public Property NumeroDocumento As String
    Public Property item As Integer
    Public Property fechapago As Date
    Public Property Importe As Decimal
    Public Property IdMoneda As String
    Public Property TipoCambio As Decimal
    Public Function Lista(d As NCuotas) As DataTable
        Dim parametros() As Object = {"@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie, d.NumeroDocumento}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_ComprobanteCuota_S", parametros, valores, tipoParametro, 3).Tables(0)
        Return dt
    End Function
    Public Sub Agregar(d As NCuotas)
        Dim parametros() As Object = {"IdTipoDocumento", "Serie", "NumeroDocumento", "item", "fechapago", "Importe", "IdMoneda", "TipoCambio"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.Date, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.item, d.fechapago, d.Importe, d.IdMoneda, d.TipoCambio}
        sql.EjecutarProcedure("Str_ComprobanteCuota_I", parametros, valores, tipoParametro, 8)
    End Sub
    Public Sub Eliminar(d As NCuotas)
        Dim parametros() As Object = {"IdTipoDocumento", "Serie", "NumeroDocumento"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdTipoDocumento, d.Serie, d.NumeroDocumento}
        sql.EjecutarProcedure("Str_ComprobanteCuota_D", parametros, valores, tipoParametro, 3)
    End Sub
End Class
