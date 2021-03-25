Imports CapaDatos
Public Class NDetalleComprobante
    Dim sql As New ClsConexion
#Region "Declarations"

    Public Property idagencia As String
    Public Property idtipodocumento As String
    Public Property serie As String
    Public Property numerodocumento As String
    Public Property item As String
    Public Property idarticulo As String
    Public Property descripcion As String
    Public Property texto As String
    Public Property cantidad As Decimal
    Public Property unidad As String
    Public Property serie1 As String
    Public Property cantidad1 As Decimal
    Public Property unidadenvase As String
    Public Property numeroenvase As Decimal
    Public Property saldoentrega As Decimal
    Public Property precioventa As Decimal
    Public Property precioventah As Decimal
    Public Property precioventaimportacion As Decimal
    Public Property precioventaimportacionh As Decimal
    Public Property preciosigv As Decimal
    Public Property importedescuento As Decimal
    Public Property descuentodocumento As Decimal
    Public Property cargodistribucion As Decimal
    Public Property igv As Decimal
    Public Property importeigv As Decimal
    Public Property importeus As Decimal
    Public Property importemn As Decimal
    Public Property idtipoitemdescuento As String
    Public Property descuento1 As Decimal
    Public Property importedescuento1 As Decimal
    Public Property descuento2 As Decimal
    Public Property importedescuento2 As Decimal
    Public Property descuento3 As Decimal
    Public Property importedescuento3 As Decimal
    Public Property descuento4 As Decimal
    Public Property importedescuento4 As Decimal
    Public Property descuento5 As Decimal
    Public Property importedescuento5 As Decimal
    Public Property descuento6 As Decimal
    Public Property estado As String
    Public Property vendedor As String
    Public Property idalmacen As String
    Public Property numerocaja As String
    Public Property stock As String
    Public Property fechasdocumento As System.DateTime
    Public Property idlinea As String
    Public Property idcampania As String
    Public Property numeropaquete As String
    Public Property nrodescuentofinaciero As String
    Public Property nrodescuentolaboratorio As String
    Public Property nrodescuentoadicional As String
    Public Property nrodescuentobonificacion As String
    Public Property nrodescuentoflag As String
    Public Property comision As Decimal
    Public Property importecomision As Decimal
    Public Property usuariocrea As String
    Public Property fechacrea As System.DateTime
    Public Property preciounitarioorigen As Decimal
    Public Property idvendedor2 As String
    Public Property identrada As String
    Public Property npfacturado As String
    Public Property idlista As Integer
    Public Property loteserie As String
    Public Property lado As String
    Public Property tipo_preventa As String
    Public Property nro_preventa As String
    Public Property preciototal As Decimal
    Public Property dtipooperacion As String
    Public Property dtipoafecigv As String

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region


#Region "Metodos"
    Public Sub Add(d As NDetalleComprobante)
        Dim params() As Object = {
            "@IdAgencia", "@IdTipoDocumento", "@Serie", "@NumeroDocumento", "@Item", "@IdArticulo",
            "@Descripcion", "@Texto", "@Cantidad", "@Unidad", "@SaldoEntrega", "@PrecioVenta",
            "@PrecioSIGV", "@IGV", "@ImporteIGV", "@ImporteUS", "@ImporteMN", "@Estado",
            "@Vendedor", "@IdAlmacen", "@Stock", "@UsuarioCrea", "@FechaCrea"
            }
        Dim tipoParametro() As Object = {
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime
            }

        Dim vals() As Object = {
            d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo,
            d.Descripcion, d.Texto, d.Cantidad, d.Unidad, d.SaldoEntrega, d.PrecioVenta,
            d.PrecioSIGV, d.IGV, d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.Estado,
            d.Vendedor, d.IdAlmacen, d.Stock, d.UsuarioCrea, d.FechaCrea}

        sql.EjecutarProcedure("Str_AddDetComprobante", params, vals, tipoParametro, 23)
    End Sub

    Public Sub additem(d As NDetalleComprobante)
        Dim parametros() As Object = {
            "@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@item", "@idArticulo",
            "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1",
            "@unidadEnvase", "@numeroEnvase", "@saldoEntrega", "@precioVenta", "@precioVentaH", "@precioVentaImportacion",
            "@precioVentaImportacionH", "@precioSIGV", "@importeDescuento", "@descuentoDocumento", "@cargoDistribucion", "@iGV",
            "@importeIGV", "@importeUS", "@importeMN", "@idTipoITemDescuento", "@descuento1", "@importeDescuento1",
            "@descuento2", "@importeDescuento2", "@descuento3", "@importeDescuento3", "@descuento4", "@importeDescuento4",
            "@descuento5", "@importeDescuento5", "@descuento6", "@estado", "@vendedor", "@idAlmacen",
            "@numeroCaja", "@stock", "@fechaSDocumento", "@idLinea", "@idCampania", "@numeroPaquete",
            "@nroDescuentoFinaciero", "@nroDescuentoLaboratorio", "@nroDescuentoAdicional", "@nroDescuentoBonificacion", "@nroDescuentoFlag", "@comision",
            "@importeComision", "@usuarioCrea", "@fechaCrea", "@precioUnitarioOrigen", "@idVendedor2", "@identrada",
            "@nPFacturado", "@idLista", "@loteSerie", "@lado", "@tipo_PreVenta", "@nro_PreVenta"}
        Dim tipoParametro() As Object = {
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}

        Dim valores() As Object = {
            d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo,
            d.Descripcion, d.Texto, d.Cantidad, d.Unidad, d.Serie1, d.Cantidad1,
            d.UnidadEnvase, d.NumeroEnvase, d.SaldoEntrega, d.PrecioVenta, d.PrecioVentaH, d.PrecioVentaImportacion,
            d.PrecioVentaImportacionH, d.PrecioSIGV, d.ImporteDescuento, d.DescuentoDocumento, d.CargoDistribucion, d.IGV,
            d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.IdTipoITemDescuento, d.Descuento1, d.ImporteDescuento1,
            d.Descuento2, d.ImporteDescuento2, d.Descuento3, d.ImporteDescuento3, d.Descuento4, d.ImporteDescuento4,
            d.Descuento5, d.ImporteDescuento5, d.Descuento6, d.Estado, d.Vendedor, d.IdAlmacen,
            d.NumeroCaja, d.Stock, d.FechaSDocumento, d.IdLinea, d.IdCampania, d.NumeroPaquete,
            d.NroDescuentoFinaciero, d.NroDescuentoLaboratorio, d.NroDescuentoAdicional, d.NroDescuentoBonificacion, d.NroDescuentoFlag, d.Comision,
            d.ImporteComision, d.UsuarioCrea, d.FechaCrea, d.PrecioUnitarioOrigen, d.IdVendedor2, d.identrada,
            d.NPFacturado, d.IdLista, d.LoteSerie, d.Lado, d.Tipo_PreVenta, d.Nro_PreVenta}

        sql.EjecutarProcedure("Str_AddDetComprobante", parametros, valores, tipoParametro, 66)

    End Sub
    Public Sub agregar(d As NDetalleComprobante)
        Dim paramsD() As Object = {"@IdAgencia", "@IdTipoDocumento", "@Serie", "@NumeroDocumento", "@Item", "@IdArticulo", "@Descripcion", "@Cantidad", "@Unidad", "@PrecioUnitario", "@PrecioSIGV", "@ImporteDescuento", "@IGV", "@ImporteIGV", "@ImporteUS", "@ImporteMN", "@Descuento1", "@ImporteDescuento1", "@Descuento2", "@ImporteDescuento2", "@Descuento3", "@ImporteDescuento3", "@IdAlmacen", "@Usuario", "@Fecha", "@idEntrada"}
        Dim tipoParametroD() As Object = {SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Float, SqlDbType.Char, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Char, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.Char}
        Dim valsD() As Object = {d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo, d.Descripcion, d.Cantidad,
        d.Unidad, d.PrecioVenta, d.PrecioSIGV, d.ImporteDescuento, d.IGV, d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.Descuento1, d.ImporteDescuento1,
        d.Descuento2, d.ImporteDescuento2, d.Descuento3, d.ImporteDescuento3, d.IdAlmacen, d.UsuarioCrea, d.FechaCrea, d.identrada}
        sql.EjecutarProcedure("proc_AddDetalleComprobante", paramsD, valsD, tipoParametroD, 26)
    End Sub
    Public Sub agregarUnidadEquivalente(d As NDetalleComprobante)
        Dim paramsD() As Object = {"@IdAgencia", "@IdTipoDocumento", "@Serie", "@NumeroDocumento", "@Item", "@IdArticulo", "@Descripcion", "@Cantidad",
            "@Unidad", "@PrecioUnitario", "@PrecioSIGV", "@ImporteDescuento", "@IGV", "@ImporteIGV", "@ImporteUS", "@ImporteMN", "@Descuento1",
            "@ImporteDescuento1", "@Descuento2", "@ImporteDescuento2", "@Descuento3", "@ImporteDescuento3", "@IdAlmacen", "@Usuario", "@Fecha",
            "@idEntrada", "@unidadenvase", "@numeroenvase", "@precioventah", "@cantidad1"}
        Dim tipoParametroD() As Object = {SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char,
            SqlDbType.Char, SqlDbType.Char, SqlDbType.Float, SqlDbType.Char, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float,
            SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Float,
            SqlDbType.Float, SqlDbType.Float, SqlDbType.Float, SqlDbType.Char, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.Char,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal}
        Dim valsD() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.item, d.idarticulo, d.descripcion, d.cantidad,
        d.unidad, d.precioventa, d.preciosigv, d.importedescuento, d.igv, d.importeigv, d.importeus, d.importemn, d.descuento1, d.importedescuento1,
        d.descuento2, d.importedescuento2, d.descuento3, d.importedescuento3, d.idalmacen, d.usuariocrea, d.fechacrea, d.identrada,
        d.unidadenvase, d.numeroenvase, d.precioventah, d.cantidad1}
        sql.EjecutarProcedure("proc_AddDetalleComprobanteConUnidadEquivalente", paramsD, valsD, tipoParametroD, 30)
    End Sub
    Public Sub SaldoItem(d As NDetalleComprobante)
        sql.Editar("dbo.detallecomprobante", "saldoentrega=isnull(saldoentrega,0.00)-" & d.saldoentrega & "", "idagencia='" & d.idagencia & "' and idtipodocumento='" & d.idtipodocumento & "' and rtrim(serie)='" & d.serie & "' and rtrim(numerodocumento)='" & d.numerodocumento & "' and idarticulo='" & Trim(d.idarticulo) & "'")
    End Sub
    Public Function DetalleCPE21(p As NDetalleComprobante) As DataTable
        Dim parametros() As Object = {"@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndDetalleComprobante21", parametros, valores, tipoparametros, 3).Tables(0)
    End Function
    Public Function DetalleCPE21CPE(p As NDetalleComprobante) As DataTable
        Dim parametros() As Object = {"@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndDetalleComprobante21CPE", parametros, valores, tipoparametros, 3).Tables(0)
    End Function

    Public Function DetalleCPE(p As NDetalleComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idagencia, p.idalmacen, p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndDetalleComprobante", parametros, valores, tipoparametros, 5).Tables(0)
    End Function
    Public Function Detalle(d As NDetalleComprobante) As DataTable
        Dim cadena As String = " SELECT Item, IdArticulo, Descripcion,Unidad, Cantidad,"
        cadena += " PrecioVenta as PUnit,ImporteUS, ImporteMN "
        cadena += " FROM  detallecomprobante "
        cadena += " WHERE     (IdAgencia = '" & d.IdAgencia & "') AND (IdTipoDocumento = '" & d.IdTipoDocumento & "') AND (Serie = '" & d.Serie & "') AND (NumeroDocumento = '" & d.NumeroDocumento & "')"
        Dim dt_det As New DataTable
        dt_det = sql.EjecutarConsulta("det", cadena).Tables(0)
        Return dt_det
    End Function
    Public Function Detalle(idarticulo As String) As DataTable
        Dim cadena As String = " select left(rtrim(IdArticulo),len(rtrim(idarticulo))-5) as NroCompra,SUBSTRING(Idarticulo,len(rtrim(idarticulo))-3,4)as Item,d.Descripcion, "
        cadena += " Cantidad,Unidad,PrecioVenta as PUnit,(d.PrecioSIGV+d.ImporteIGV)as Importe,(c.Serie+'-'+ c.NumeroDocumento)as NroComprobante,  "
        cadena += " c.IdTipoDocumento,c.FechaDocumento from detallecomprobante d inner join Comprobante c on d.IdTipoDocumento=c.IdTipoDocumento "
        cadena += " and d.Serie=c.Serie and d.NumeroDocumento=c.NumeroDocumento "
        cadena += " where IdArticulo like '" & idarticulo & "' AND c.estado='V'"
        Dim dt_det As New DataTable
        dt_det = sql.EjecutarConsulta("det", cadena).Tables(0)
        Return dt_det
    End Function

    Public Function obtenerDetalle(d As NDetalleComprobante) As DataTable
        Dim cadena As String = " SELECT IdAgencia, IdTipoDocumento, Serie, NumeroDocumento, Item, IdArticulo, Descripcion, Texto, Cantidad, Unidad, Serie1, Cantidad1, UnidadEnvase, NumeroEnvase,  "
        cadena += " SaldoEntrega, PrecioVenta, PrecioVentaH, PrecioVentaImportacion, PrecioVentaImportacionH, PrecioSIGV, ImporteDescuento, DescuentoDocumento,  "
        cadena += " CargoDistribucion, IGV, ImporteIGV, ImporteUS, ImporteMN, IdTipoITemDescuento, Descuento1, ImporteDescuento1, Descuento2, ImporteDescuento2, Descuento3, "
        cadena += " ImporteDescuento3, Descuento4, ImporteDescuento4, Descuento5, ImporteDescuento5, Descuento6, Estado, Vendedor, IdAlmacen, NumeroCaja, Stock, "
        cadena += " FechaSDocumento, IdLinea, IdCampania, NumeroPaquete, NroDescuentoFinaciero, NroDescuentoLaboratorio, NroDescuentoAdicional, NroDescuentoBonificacion, "
        cadena += " NroDescuentoFlag, Comision, ImporteComision, UsuarioCrea, FechaCrea, PrecioUnitarioOrigen, IdVendedor2, identrada, NPFacturado, IdLista "
        cadena += " FROM         detallecomprobante "
        cadena += " WHERE     (IdAgencia = '" & d.IdAgencia & "') AND (IdTipoDocumento = '" & d.IdTipoDocumento & "') AND (Serie = '" & d.Serie & "') AND (NumeroDocumento = '" & d.NumeroDocumento & "')"
        Dim dt_det As New DataTable
        dt_det = sql.EjecutarConsulta("det", cadena).Tables(0)
        Return dt_det
    End Function
    ''' <summary>
    ''' Agregar detalle de comprobante
    ''' </summary>
    ''' <param name="d"></param>
    'Public Sub Insertar(d As NDetalleComprobante)
    '    Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@item", "@idArticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadEnvase", "@numeroEnvase", "@saldoEntrega", "@precioVenta", "@precioVentaH", "@precioVentaImportacion", "@precioVentaImportacionH", "@precioSIGV", "@importeDescuento", "@descuentoDocumento", "@cargoDistribucion", "@iGV", "@importeIGV", "@importeUS", "@importeMN", "@idTipoITemDescuento", "@descuento1", "@importeDescuento1", "@descuento2", "@importeDescuento2", "@descuento3", "@importeDescuento3", "@descuento4", "@importeDescuento4", "@descuento5", "@importeDescuento5", "@descuento6", "@estado", "@vendedor", "@idAlmacen", "@numeroCaja", "@stock", "@fechaSDocumento", "@idLinea", "@idCampania", "@numeroPaquete", "@nroDescuentoFinaciero", "@nroDescuentoLaboratorio", "@nroDescuentoAdicional", "@nroDescuentoBonificacion", "@nroDescuentoFlag", "@comision", "@importeComision", "@usuarioCrea", "@fechaCrea", "@precioUnitarioOrigen", "@idVendedor2", "@identrada", "@nPFacturado", "@idLista", "@loteSerie", "@lado", "@tipo_PreVenta", "@nro_PreVenta"}
    '    Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
    '    Dim valores() As Object = {d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo, d.Descripcion, d.Texto, d.Cantidad, d.Unidad, d.Serie1, d.Cantidad1, d.UnidadEnvase, d.NumeroEnvase, d.SaldoEntrega, d.PrecioVenta, d.PrecioVentaH, d.PrecioVentaImportacion, d.PrecioVentaImportacionH, d.PrecioSIGV, d.ImporteDescuento, d.DescuentoDocumento, d.CargoDistribucion, d.IGV, d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.IdTipoITemDescuento, d.Descuento1, d.ImporteDescuento1, d.Descuento2, d.ImporteDescuento2, d.Descuento3, d.ImporteDescuento3, d.Descuento4, d.ImporteDescuento4, d.Descuento5, d.ImporteDescuento5, d.Descuento6, d.Estado, d.Vendedor, d.IdAlmacen, d.NumeroCaja, d.Stock, d.FechaSDocumento, d.IdLinea, d.IdCampania, d.NumeroPaquete, d.NroDescuentoFinaciero, d.NroDescuentoLaboratorio, d.NroDescuentoAdicional, d.NroDescuentoBonificacion, d.NroDescuentoFlag, d.Comision, d.ImporteComision, d.UsuarioCrea, d.FechaCrea, d.PrecioUnitarioOrigen, d.IdVendedor2, d.identrada, d.NPFacturado, d.IdLista, d.LoteSerie, d.Lado, d.Tipo_PreVenta, d.Nro_PreVenta}
    '    sql.EjecutarProcedure("Str_DetalleComprobante_I", parametros, valores, tipoParametro, 66)
    'End Sub
    'Public Sub Actualizar(d As NDetalleComprobante)
    '    Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@item", "@idArticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadEnvase", "@numeroEnvase", "@saldoEntrega", "@precioVenta", "@precioVentaH", "@precioVentaImportacion", "@precioVentaImportacionH", "@precioSIGV", "@importeDescuento", "@descuentoDocumento", "@cargoDistribucion", "@iGV", "@importeIGV", "@importeUS", "@importeMN", "@idTipoITemDescuento", "@descuento1", "@importeDescuento1", "@descuento2", "@importeDescuento2", "@descuento3", "@importeDescuento3", "@descuento4", "@importeDescuento4", "@descuento5", "@importeDescuento5", "@descuento6", "@estado", "@vendedor", "@idAlmacen", "@numeroCaja", "@stock", "@fechaSDocumento", "@idLinea", "@idCampania", "@numeroPaquete", "@nroDescuentoFinaciero", "@nroDescuentoLaboratorio", "@nroDescuentoAdicional", "@nroDescuentoBonificacion", "@nroDescuentoFlag", "@comision", "@importeComision", "@usuarioCrea", "@fechaCrea", "@precioUnitarioOrigen", "@idVendedor2", "@identrada", "@nPFacturado", "@idLista", "@loteSerie", "@lado", "@tipo_PreVenta", "@nro_PreVenta"}
    '    Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
    '    Dim valores() As Object = {d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo, d.Descripcion, d.Texto, d.Cantidad, d.Unidad, d.Serie1, d.Cantidad1, d.UnidadEnvase, d.NumeroEnvase, d.SaldoEntrega, d.PrecioVenta, d.PrecioVentaH, d.PrecioVentaImportacion, d.PrecioVentaImportacionH, d.PrecioSIGV, d.ImporteDescuento, d.DescuentoDocumento, d.CargoDistribucion, d.IGV, d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.IdTipoITemDescuento, d.Descuento1, d.ImporteDescuento1, d.Descuento2, d.ImporteDescuento2, d.Descuento3, d.ImporteDescuento3, d.Descuento4, d.ImporteDescuento4, d.Descuento5, d.ImporteDescuento5, d.Descuento6, d.Estado, d.Vendedor, d.IdAlmacen, d.NumeroCaja, d.Stock, d.FechaSDocumento, d.IdLinea, d.IdCampania, d.NumeroPaquete, d.NroDescuentoFinaciero, d.NroDescuentoLaboratorio, d.NroDescuentoAdicional, d.NroDescuentoBonificacion, d.NroDescuentoFlag, d.Comision, d.ImporteComision, d.UsuarioCrea, d.FechaCrea, d.PrecioUnitarioOrigen, d.IdVendedor2, d.identrada, d.NPFacturado, d.IdLista, d.LoteSerie, d.Lado, d.Tipo_PreVenta, d.Nro_PreVenta}
    '    sql.EjecutarProcedure("Str_DetalleComprobante_U", parametros, valores, tipoParametro, 66)
    ' End Sub

    Public Sub Insertar(d As NDetalleComprobante)
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@item", "@idarticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadenvase", "@numeroenvase", "@saldoentrega", "@precioventa", "@precioventah", "@precioventaimportacion", "@precioventaimportacionh", "@preciosigv", "@importedescuento", "@descuentodocumento", "@cargodistribucion", "@igv", "@importeigv", "@importeus", "@importemn", "@idtipoitemdescuento", "@descuento1", "@importedescuento1", "@descuento2", "@importedescuento2", "@descuento3", "@importedescuento3", "@descuento4", "@importedescuento4", "@descuento5", "@importedescuento5", "@descuento6", "@estado", "@vendedor", "@idalmacen", "@numerocaja", "@stock", "@fechasdocumento", "@idlinea", "@idcampania", "@numeropaquete", "@nrodescuentofinaciero", "@nrodescuentolaboratorio", "@nrodescuentoadicional", "@nrodescuentobonificacion", "@nrodescuentoflag", "@comision", "@importecomision", "@usuariocrea", "@fechacrea", "@preciounitarioorigen", "@idvendedor2", "@identrada", "@npfacturado", "@idlista", "@loteserie", "@lado", "@tipo_preventa", "@nro_preventa", "@dtipooperacion", "@dtipoafecigv"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.item, d.idarticulo, d.descripcion, d.texto, d.cantidad, d.unidad, d.serie1, d.cantidad1, d.unidadenvase, d.numeroenvase, d.saldoentrega, d.precioventa, d.precioventah, d.precioventaimportacion, d.precioventaimportacionh, d.preciosigv, d.importedescuento, d.descuentodocumento, d.cargodistribucion, d.igv, d.importeigv, d.importeus, d.importemn, d.idtipoitemdescuento, d.descuento1, d.importedescuento1, d.descuento2, d.importedescuento2, d.descuento3, d.importedescuento3, d.descuento4, d.importedescuento4, d.descuento5, d.importedescuento5, d.descuento6, d.estado, d.vendedor, d.idalmacen, d.numerocaja, d.stock, d.fechasdocumento, d.idlinea, d.idcampania, d.numeropaquete, d.nrodescuentofinaciero, d.nrodescuentolaboratorio, d.nrodescuentoadicional, d.nrodescuentobonificacion, d.nrodescuentoflag, d.comision, d.importecomision, d.usuariocrea, d.fechacrea, d.preciounitarioorigen, d.idvendedor2, d.identrada, d.npfacturado, d.idlista, d.loteserie, d.lado, d.tipo_preventa, d.nro_preventa, d.dtipooperacion, d.dtipoafecigv}
        sql.EjecutarProcedure("Str_DetalleComprobante_I", parametros, valores, tipoParametro, 68)
    End Sub
    ''' <summary>
    ''' Agregar un array de detalle comprobante
    ''' </summary>
    ''' <param name="de"></param>
    Public Sub Insertar(de As List(Of NDetalleComprobante))
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@item", "@idarticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadenvase", "@numeroenvase", "@saldoentrega", "@precioventa", "@precioventah", "@precioventaimportacion", "@precioventaimportacionh", "@preciosigv", "@importedescuento", "@descuentodocumento", "@cargodistribucion", "@igv", "@importeigv", "@importeus", "@importemn", "@idtipoitemdescuento", "@descuento1", "@importedescuento1", "@descuento2", "@importedescuento2", "@descuento3", "@importedescuento3", "@descuento4", "@importedescuento4", "@descuento5", "@importedescuento5", "@descuento6", "@estado", "@vendedor", "@idalmacen", "@numerocaja", "@stock", "@fechasdocumento", "@idlinea", "@idcampania", "@numeropaquete", "@nrodescuentofinaciero", "@nrodescuentolaboratorio", "@nrodescuentoadicional", "@nrodescuentobonificacion", "@nrodescuentoflag", "@comision", "@importecomision", "@usuariocrea", "@fechacrea", "@preciounitarioorigen", "@idvendedor2", "@identrada", "@npfacturado", "@idlista", "@loteserie", "@lado", "@tipo_preventa", "@nro_preventa", "@dtipooperacion", "@dtipoafecigv"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object
        For Each d As NDetalleComprobante In de
            valores = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.item, d.idarticulo, d.descripcion, d.texto, d.cantidad, d.unidad, d.serie1, d.cantidad1, d.unidadenvase, d.numeroenvase, d.saldoentrega, d.precioventa, d.precioventah, d.precioventaimportacion, d.precioventaimportacionh, d.preciosigv, d.importedescuento, d.descuentodocumento, d.cargodistribucion, d.igv, d.importeigv, d.importeus, d.importemn, d.idtipoitemdescuento, d.descuento1, d.importedescuento1, d.descuento2, d.importedescuento2, d.descuento3, d.importedescuento3, d.descuento4, d.importedescuento4, d.descuento5, d.importedescuento5, d.descuento6, d.estado, d.vendedor, d.idalmacen, d.numerocaja, d.stock, d.fechasdocumento, d.idlinea, d.idcampania, d.numeropaquete, d.nrodescuentofinaciero, d.nrodescuentolaboratorio, d.nrodescuentoadicional, d.nrodescuentobonificacion, d.nrodescuentoflag, d.comision, d.importecomision, d.usuariocrea, d.fechacrea, d.preciounitarioorigen, d.idvendedor2, d.identrada, d.npfacturado, d.idlista, d.loteserie, d.lado, d.tipo_preventa, d.nro_preventa, d.dtipooperacion, d.dtipoafecigv}
            sql.EjecutarProcedure("Str_DetalleComprobante_I", parametros, valores, tipoParametro, 68)
        Next
    End Sub
    Public Sub Actualizar(d As NDetalleComprobante)
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@item", "@idarticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadenvase", "@numeroenvase", "@saldoentrega", "@precioventa", "@precioventah", "@precioventaimportacion", "@precioventaimportacionh", "@preciosigv", "@importedescuento", "@descuentodocumento", "@cargodistribucion", "@igv", "@importeigv", "@importeus", "@importemn", "@idtipoitemdescuento", "@descuento1", "@importedescuento1", "@descuento2", "@importedescuento2", "@descuento3", "@importedescuento3", "@descuento4", "@importedescuento4", "@descuento5", "@importedescuento5", "@descuento6", "@estado", "@vendedor", "@idalmacen", "@numerocaja", "@stock", "@fechasdocumento", "@idlinea", "@idcampania", "@numeropaquete", "@nrodescuentofinaciero", "@nrodescuentolaboratorio", "@nrodescuentoadicional", "@nrodescuentobonificacion", "@nrodescuentoflag", "@comision", "@importecomision", "@usuariocrea", "@fechacrea", "@preciounitarioorigen", "@idvendedor2", "@identrada", "@npfacturado", "@idlista", "@loteserie", "@lado", "@tipo_preventa", "@nro_preventa", "@dtipooperacion", "@dtipoafecigv"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.item, d.idarticulo, d.descripcion, d.texto, d.cantidad, d.unidad, d.serie1, d.cantidad1, d.unidadenvase, d.numeroenvase, d.saldoentrega, d.precioventa, d.precioventah, d.precioventaimportacion, d.precioventaimportacionh, d.preciosigv, d.importedescuento, d.descuentodocumento, d.cargodistribucion, d.igv, d.importeigv, d.importeus, d.importemn, d.idtipoitemdescuento, d.descuento1, d.importedescuento1, d.descuento2, d.importedescuento2, d.descuento3, d.importedescuento3, d.descuento4, d.importedescuento4, d.descuento5, d.importedescuento5, d.descuento6, d.estado, d.vendedor, d.idalmacen, d.numerocaja, d.stock, d.fechasdocumento, d.idlinea, d.idcampania, d.numeropaquete, d.nrodescuentofinaciero, d.nrodescuentolaboratorio, d.nrodescuentoadicional, d.nrodescuentobonificacion, d.nrodescuentoflag, d.comision, d.importecomision, d.usuariocrea, d.fechacrea, d.preciounitarioorigen, d.idvendedor2, d.identrada, d.npfacturado, d.idlista, d.loteserie, d.lado, d.tipo_preventa, d.nro_preventa, d.dtipooperacion, d.dtipoafecigv}
        sql.EjecutarProcedure("Str_DetalleComprobante_U", parametros, valores, tipoParametro, 68)
    End Sub


    Public Sub Eliminar(d As NDetalleComprobante)
        'Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@item", "@idArticulo", "@descripcion", "@texto", "@cantidad", "@unidad", "@serie1", "@cantidad1", "@unidadEnvase", "@numeroEnvase", "@saldoEntrega", "@precioVenta", "@precioVentaH", "@precioVentaImportacion", "@precioVentaImportacionH", "@precioSIGV", "@importeDescuento", "@descuentoDocumento", "@cargoDistribucion", "@iGV", "@importeIGV", "@importeUS", "@importeMN", "@idTipoITemDescuento", "@descuento1", "@importeDescuento1", "@descuento2", "@importeDescuento2", "@descuento3", "@importeDescuento3", "@descuento4", "@importeDescuento4", "@descuento5", "@importeDescuento5", "@descuento6", "@estado", "@vendedor", "@idAlmacen", "@numeroCaja", "@stock", "@fechaSDocumento", "@idLinea", "@idCampania", "@numeroPaquete", "@nroDescuentoFinaciero", "@nroDescuentoLaboratorio", "@nroDescuentoAdicional", "@nroDescuentoBonificacion", "@nroDescuentoFlag", "@comision", "@importeComision", "@usuarioCrea", "@fechaCrea", "@precioUnitarioOrigen", "@idVendedor2", "@identrada", "@nPFacturado", "@idLista", "@loteSerie", "@lado", "@tipo_PreVenta", "@nro_PreVenta"}
        'Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        'Dim valores() As Object = {d.IdAgencia, d.IdTipoDocumento, d.Serie, d.NumeroDocumento, d.Item, d.IdArticulo, d.Descripcion, d.Texto, d.Cantidad, d.Unidad, d.Serie1, d.Cantidad1, d.UnidadEnvase, d.NumeroEnvase, d.SaldoEntrega, d.PrecioVenta, d.PrecioVentaH, d.PrecioVentaImportacion, d.PrecioVentaImportacionH, d.PrecioSIGV, d.ImporteDescuento, d.DescuentoDocumento, d.CargoDistribucion, d.IGV, d.ImporteIGV, d.ImporteUS, d.ImporteMN, d.IdTipoITemDescuento, d.Descuento1, d.ImporteDescuento1, d.Descuento2, d.ImporteDescuento2, d.Descuento3, d.ImporteDescuento3, d.Descuento4, d.ImporteDescuento4, d.Descuento5, d.ImporteDescuento5, d.Descuento6, d.Estado, d.Vendedor, d.IdAlmacen, d.NumeroCaja, d.Stock, d.FechaSDocumento, d.IdLinea, d.IdCampania, d.NumeroPaquete, d.NroDescuentoFinaciero, d.NroDescuentoLaboratorio, d.NroDescuentoAdicional, d.NroDescuentoBonificacion, d.NroDescuentoFlag, d.Comision, d.ImporteComision, d.UsuarioCrea, d.FechaCrea, d.PrecioUnitarioOrigen, d.IdVendedor2, d.identrada, d.NPFacturado, d.IdLista, d.LoteSerie, d.Lado, d.Tipo_PreVenta, d.Nro_PreVenta}
        'sql.EjecutarProcedure("Str_DetalleComprobante_D", parametros, valores, tipoParametro, 66)
        sql.Eliminar_Items("DetalleComprobante", "IdAlmacen='" & d.IdAlmacen & "' and Serie='" & d.Serie & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' and NumeroDocumento='" & d.NumeroDocumento & "'")

    End Sub
    'Public Function Lista() As DataTable
    '    Dim parametros() As Object = {"@NameFile"}
    '    Dim tipoParametro() As Object = {SqlDbType.VarChar}
    '    Dim valores() As Object = {DBNull.Value}
    '    Dim dt As New DataTable
    '    dt = sql.ProcedureSQL("Str_DetalleComprobante_S", parametros, valores, tipoParametro, 66).Tables(0)
    '    Return dt
    'End Function
    Public Function Registro(d As NDetalleComprobante) As NDetalleComprobante
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@item", "@idarticulo", "@idalmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.item, d.idarticulo, d.idalmacen}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_DetalleComprobante_S", parametros, valores, tipoParametro, 7).Tables(0)
        If dt.Rows.Count > 0 Then
            d.idagencia = IIf(dt.Rows(0).Item("idagencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idagencia"))
            d.idtipodocumento = IIf(dt.Rows(0).Item("idtipodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idtipodocumento"))
            d.serie = IIf(dt.Rows(0).Item("serie") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie"))
            d.numerodocumento = IIf(dt.Rows(0).Item("numerodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("numerodocumento"))
            d.item = IIf(dt.Rows(0).Item("item") Is DBNull.Value, Nothing, dt.Rows(0).Item("item"))
            d.idarticulo = IIf(dt.Rows(0).Item("idarticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idarticulo"))
            d.descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.texto = IIf(dt.Rows(0).Item("texto") Is DBNull.Value, Nothing, dt.Rows(0).Item("texto"))
            d.cantidad = IIf(dt.Rows(0).Item("cantidad") Is DBNull.Value, Nothing, dt.Rows(0).Item("cantidad"))
            d.unidad = IIf(dt.Rows(0).Item("unidad") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidad"))
            d.serie1 = IIf(dt.Rows(0).Item("serie1") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie1"))
            d.cantidad1 = IIf(dt.Rows(0).Item("cantidad1") Is DBNull.Value, Nothing, dt.Rows(0).Item("cantidad1"))
            d.unidadenvase = IIf(dt.Rows(0).Item("unidadenvase") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidadenvase"))
            d.numeroenvase = IIf(dt.Rows(0).Item("numeroenvase") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroenvase"))
            d.saldoentrega = IIf(dt.Rows(0).Item("saldoentrega") Is DBNull.Value, Nothing, dt.Rows(0).Item("saldoentrega"))
            d.precioventa = IIf(dt.Rows(0).Item("precioventa") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioventa"))
            d.precioventah = IIf(dt.Rows(0).Item("precioventah") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioventah"))
            d.precioventaimportacion = IIf(dt.Rows(0).Item("precioventaimportacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioventaimportacion"))
            d.precioventaimportacionh = IIf(dt.Rows(0).Item("precioventaimportacionh") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioventaimportacionh"))
            d.preciosigv = IIf(dt.Rows(0).Item("preciosigv") Is DBNull.Value, Nothing, dt.Rows(0).Item("preciosigv"))
            d.importedescuento = IIf(dt.Rows(0).Item("importedescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento"))
            d.descuentodocumento = IIf(dt.Rows(0).Item("descuentodocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuentodocumento"))
            d.cargodistribucion = IIf(dt.Rows(0).Item("cargodistribucion") Is DBNull.Value, Nothing, dt.Rows(0).Item("cargodistribucion"))
            d.igv = IIf(dt.Rows(0).Item("igv") Is DBNull.Value, Nothing, dt.Rows(0).Item("igv"))
            d.importeigv = IIf(dt.Rows(0).Item("importeigv") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeigv"))
            d.importeus = IIf(dt.Rows(0).Item("importeus") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeus"))
            d.importemn = IIf(dt.Rows(0).Item("importemn") Is DBNull.Value, Nothing, dt.Rows(0).Item("importemn"))
            d.idtipoitemdescuento = IIf(dt.Rows(0).Item("idtipoitemdescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idtipoitemdescuento"))
            d.descuento1 = IIf(dt.Rows(0).Item("descuento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento1"))
            d.importedescuento1 = IIf(dt.Rows(0).Item("importedescuento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento1"))
            d.descuento2 = IIf(dt.Rows(0).Item("descuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento2"))
            d.importedescuento2 = IIf(dt.Rows(0).Item("importedescuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento2"))
            d.descuento3 = IIf(dt.Rows(0).Item("descuento3") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento3"))
            d.importedescuento3 = IIf(dt.Rows(0).Item("importedescuento3") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento3"))
            d.descuento4 = IIf(dt.Rows(0).Item("descuento4") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento4"))
            d.importedescuento4 = IIf(dt.Rows(0).Item("importedescuento4") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento4"))
            d.descuento5 = IIf(dt.Rows(0).Item("descuento5") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento5"))
            d.importedescuento5 = IIf(dt.Rows(0).Item("importedescuento5") Is DBNull.Value, Nothing, dt.Rows(0).Item("importedescuento5"))
            d.descuento6 = IIf(dt.Rows(0).Item("descuento6") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento6"))
            d.estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.vendedor = IIf(dt.Rows(0).Item("vendedor") Is DBNull.Value, Nothing, dt.Rows(0).Item("vendedor"))
            d.idalmacen = IIf(dt.Rows(0).Item("idalmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("idalmacen"))
            d.numerocaja = IIf(dt.Rows(0).Item("numerocaja") Is DBNull.Value, Nothing, dt.Rows(0).Item("numerocaja"))
            d.stock = IIf(dt.Rows(0).Item("stock") Is DBNull.Value, Nothing, dt.Rows(0).Item("stock"))
            d.fechasdocumento = IIf(dt.Rows(0).Item("fechasdocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechasdocumento"))
            d.idlinea = IIf(dt.Rows(0).Item("idlinea") Is DBNull.Value, Nothing, dt.Rows(0).Item("idlinea"))
            d.idcampania = IIf(dt.Rows(0).Item("idcampania") Is DBNull.Value, Nothing, dt.Rows(0).Item("idcampania"))
            d.numeropaquete = IIf(dt.Rows(0).Item("numeropaquete") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeropaquete"))
            d.nrodescuentofinaciero = IIf(dt.Rows(0).Item("nrodescuentofinaciero") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrodescuentofinaciero"))
            d.nrodescuentolaboratorio = IIf(dt.Rows(0).Item("nrodescuentolaboratorio") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrodescuentolaboratorio"))
            d.nrodescuentoadicional = IIf(dt.Rows(0).Item("nrodescuentoadicional") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrodescuentoadicional"))
            d.nrodescuentobonificacion = IIf(dt.Rows(0).Item("nrodescuentobonificacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrodescuentobonificacion"))
            d.nrodescuentoflag = IIf(dt.Rows(0).Item("nrodescuentoflag") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrodescuentoflag"))
            d.comision = IIf(dt.Rows(0).Item("comision") Is DBNull.Value, Nothing, dt.Rows(0).Item("comision"))
            d.importecomision = IIf(dt.Rows(0).Item("importecomision") Is DBNull.Value, Nothing, dt.Rows(0).Item("importecomision"))
            d.usuariocrea = IIf(dt.Rows(0).Item("usuariocrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuariocrea"))
            d.fechacrea = IIf(dt.Rows(0).Item("fechacrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechacrea"))
            d.preciounitarioorigen = IIf(dt.Rows(0).Item("preciounitarioorigen") Is DBNull.Value, Nothing, dt.Rows(0).Item("preciounitarioorigen"))
            d.idvendedor2 = IIf(dt.Rows(0).Item("idvendedor2") Is DBNull.Value, Nothing, dt.Rows(0).Item("idvendedor2"))
            d.identrada = IIf(dt.Rows(0).Item("identrada") Is DBNull.Value, Nothing, dt.Rows(0).Item("identrada"))
            d.npfacturado = IIf(dt.Rows(0).Item("npfacturado") Is DBNull.Value, Nothing, dt.Rows(0).Item("npfacturado"))
            d.idlista = IIf(dt.Rows(0).Item("idlista") Is DBNull.Value, Nothing, dt.Rows(0).Item("idlista"))
            d.loteserie = IIf(dt.Rows(0).Item("loteserie") Is DBNull.Value, Nothing, dt.Rows(0).Item("loteserie"))
            d.lado = IIf(dt.Rows(0).Item("lado") Is DBNull.Value, Nothing, dt.Rows(0).Item("lado"))
            d.tipo_preventa = IIf(dt.Rows(0).Item("tipo_preventa") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipo_preventa"))
            d.nro_preventa = IIf(dt.Rows(0).Item("nro_preventa") Is DBNull.Value, Nothing, dt.Rows(0).Item("nro_preventa"))
            d.preciototal = IIf(dt.Rows(0).Item("preciototal") Is DBNull.Value, Nothing, dt.Rows(0).Item("preciototal"))
            d.dtipooperacion = IIf(dt.Rows(0).Item("dtipooperacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("dtipooperacion"))
            d.dtipoafecigv = IIf(dt.Rows(0).Item("dtipoafecigv") Is DBNull.Value, Nothing, dt.Rows(0).Item("dtipoafecigv"))
        Else
            d.idagencia = Nothing
            d.idtipodocumento = Nothing
            d.serie = Nothing
            d.numerodocumento = Nothing
            d.item = Nothing
            d.idarticulo = Nothing
            d.descripcion = Nothing
            d.texto = Nothing
            d.cantidad = Nothing
            d.unidad = Nothing
            d.serie1 = Nothing
            d.cantidad1 = Nothing
            d.unidadenvase = Nothing
            d.numeroenvase = Nothing
            d.saldoentrega = Nothing
            d.precioventa = Nothing
            d.precioventah = Nothing
            d.precioventaimportacion = Nothing
            d.precioventaimportacionh = Nothing
            d.preciosigv = Nothing
            d.importedescuento = Nothing
            d.descuentodocumento = Nothing
            d.cargodistribucion = Nothing
            d.igv = Nothing
            d.importeigv = Nothing
            d.importeus = Nothing
            d.importemn = Nothing
            d.idtipoitemdescuento = Nothing
            d.descuento1 = Nothing
            d.importedescuento1 = Nothing
            d.descuento2 = Nothing
            d.importedescuento2 = Nothing
            d.descuento3 = Nothing
            d.importedescuento3 = Nothing
            d.descuento4 = Nothing
            d.importedescuento4 = Nothing
            d.descuento5 = Nothing
            d.importedescuento5 = Nothing
            d.descuento6 = Nothing
            d.estado = Nothing
            d.vendedor = Nothing
            d.idalmacen = Nothing
            d.numerocaja = Nothing
            d.stock = Nothing
            d.fechasdocumento = Nothing
            d.idlinea = Nothing
            d.idcampania = Nothing
            d.numeropaquete = Nothing
            d.nrodescuentofinaciero = Nothing
            d.nrodescuentolaboratorio = Nothing
            d.nrodescuentoadicional = Nothing
            d.nrodescuentobonificacion = Nothing
            d.nrodescuentoflag = Nothing
            d.comision = Nothing
            d.importecomision = Nothing
            d.usuariocrea = Nothing
            d.fechacrea = Nothing
            d.preciounitarioorigen = Nothing
            d.idvendedor2 = Nothing
            d.identrada = Nothing
            d.npfacturado = Nothing
            d.idlista = Nothing
            d.loteserie = Nothing
            d.lado = Nothing
            d.tipo_preventa = Nothing
            d.nro_preventa = Nothing
            d.preciototal = Nothing
            d.dtipooperacion = Nothing
            d.dtipoafecigv = Nothing
        End If
        Return d
    End Function
#End Region
End Class
