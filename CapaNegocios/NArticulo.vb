Imports CapaDatos
Public Class NArticulo
    Dim sql As New ClsConexion
#Region "Declarations"

    Private _idArticulo As String
    Private _descripcion1 As String
    Private _descripcion2 As String
    Private _idArticulo2 As String
    Private _unidad As String
    Private _idCuentaContable As String
    Private _precio1 As Decimal
    Private _precio2 As Decimal
    Private _precio3 As Decimal
    Private _precio4 As Decimal
    Private _precio5 As Decimal
    Private _precio6 As Decimal
    Private _monedaVenta As String
    Private _iGV As Decimal
    Private _iSC As Decimal
    Private _tipoArticulo As String
    Private _controlRotable As String
    Private _tipoDescuento As String
    Private _descuento As Decimal
    Private _descuento2 As Decimal
    Private _porcentajeDistribuidor As Decimal
    Private _porcentajeComision As Decimal
    Private _idGrupo As String
    Private _idFamilia As String
    Private _idModelo As String
    Private _idLinea As String
    Private _peso As Decimal
    Private _volumen As Decimal
    Private _area As Decimal
    Private _factor As Decimal
    Private _ancho As Decimal
    Private _largo As Decimal
    Private _monedaCosteo As String
    Private _precioCosto As Decimal
    Private _fechaCosto As System.DateTime
    Private _idMonedaCompra As String
    Private _precioCompra As Decimal
    Private _fechaCompra As System.DateTime
    Private _idProveedor As String
    Private _monedaFOB As String
    Private _precioFOB As Decimal
    Private _margenUtilidad1 As Decimal
    Private _margenUtilidad2 As Decimal
    Private _claseArticulo As String
    Private _partidaArancelaria As String
    Private _tecnicaRotable As String
    Private _catalogoRotable As String
    Private _categoriaArancelaria As String
    Private _swSinCosteo As String
    Private _observacion As String
    Private _unidadReferencia As String
    Private _factorReferencia As Decimal
    Private _swReferencia As String
    Private _swControlStock As String
    Private _swDecimal As String
    Private _swPrecioLibre As String
    Private _swDescuentoImporte As String
    Private _swSerie As String
    Private _swLote As String
    Private _swArticuloRotable As String
    Private _usuarioCrea As String
    Private _usuarioMod As String
    Private _estado As String
    Private _fechaCrea As System.DateTime
    Private _fechaMod As System.DateTime
    Private _idArticulo3 As String
    Private _porcDetraccion As Decimal
    Private _medida As String
    Private _aR_CANNO As String
    Private _grosor As String
    Private _rutaImagen As Decimal
    Private _aR_CFECABC As String
    Private _longSerie As Decimal
    Private _swCelular As String
    Private _longCelular As Decimal
    Private _idMarca As String
    Private _interior As Decimal
    Private _exterior As Decimal
    Private _margenUtilidad3 As Decimal
    Private _precioMinimo As Decimal
    Private _chkPercepcion As Boolean
    Private _tasaPercepcion As Decimal
    Private _fotoArt As Byte()
    Private _tipoPrecio As String
    Private _fotoArt1 As Byte()
    Private _fotoArt2 As Byte()
    Private _observacion1 As Byte()
    Private _procedencia As String

#End Region

#Region "Properties"

    Public Property IdArticulo As String
        Get
            Return _idArticulo
        End Get
        Set
            _idArticulo = Value
        End Set
    End Property

    Public Property Descripcion1 As String
        Get
            Return _descripcion1
        End Get
        Set
            _descripcion1 = Value
        End Set
    End Property

    Public Property Descripcion2 As String
        Get
            Return _descripcion2
        End Get
        Set
            _descripcion2 = Value
        End Set
    End Property

    Public Property IdArticulo2 As String
        Get
            Return _idArticulo2
        End Get
        Set
            _idArticulo2 = Value
        End Set
    End Property

    Public Property Unidad As String
        Get
            Return _unidad
        End Get
        Set
            _unidad = Value
        End Set
    End Property

    Public Property IdCuentaContable As String
        Get
            Return _idCuentaContable
        End Get
        Set
            _idCuentaContable = Value
        End Set
    End Property

    Public Property Precio1 As Decimal
        Get
            Return _precio1
        End Get
        Set
            _precio1 = Value
        End Set
    End Property

    Public Property Precio2 As Decimal
        Get
            Return _precio2
        End Get
        Set
            _precio2 = Value
        End Set
    End Property

    Public Property Precio3 As Decimal
        Get
            Return _precio3
        End Get
        Set
            _precio3 = Value
        End Set
    End Property

    Public Property Precio4 As Decimal
        Get
            Return _precio4
        End Get
        Set
            _precio4 = Value
        End Set
    End Property

    Public Property Precio5 As Decimal
        Get
            Return _precio5
        End Get
        Set
            _precio5 = Value
        End Set
    End Property

    Public Property Precio6 As Decimal
        Get
            Return _precio6
        End Get
        Set
            _precio6 = Value
        End Set
    End Property

    Public Property MonedaVenta As String
        Get
            Return _monedaVenta
        End Get
        Set
            _monedaVenta = Value
        End Set
    End Property

    Public Property IGV As Decimal
        Get
            Return _iGV
        End Get
        Set
            _iGV = Value
        End Set
    End Property

    Public Property ISC As Decimal
        Get
            Return _iSC
        End Get
        Set
            _iSC = Value
        End Set
    End Property

    Public Property TipoArticulo As String
        Get
            Return _tipoArticulo
        End Get
        Set
            _tipoArticulo = Value
        End Set
    End Property

    Public Property ControlRotable As String
        Get
            Return _controlRotable
        End Get
        Set
            _controlRotable = Value
        End Set
    End Property

    Public Property TipoDescuento As String
        Get
            Return _tipoDescuento
        End Get
        Set
            _tipoDescuento = Value
        End Set
    End Property

    Public Property Descuento As Decimal
        Get
            Return _descuento
        End Get
        Set
            _descuento = Value
        End Set
    End Property

    Public Property Descuento2 As Decimal
        Get
            Return _descuento2
        End Get
        Set
            _descuento2 = Value
        End Set
    End Property

    Public Property PorcentajeDistribuidor As Decimal
        Get
            Return _porcentajeDistribuidor
        End Get
        Set
            _porcentajeDistribuidor = Value
        End Set
    End Property

    Public Property PorcentajeComision As Decimal
        Get
            Return _porcentajeComision
        End Get
        Set
            _porcentajeComision = Value
        End Set
    End Property

    Public Property IdGrupo As String
        Get
            Return _idGrupo
        End Get
        Set
            _idGrupo = Value
        End Set
    End Property

    Public Property IdFamilia As String
        Get
            Return _idFamilia
        End Get
        Set
            _idFamilia = Value
        End Set
    End Property

    Public Property IdModelo As String
        Get
            Return _idModelo
        End Get
        Set
            _idModelo = Value
        End Set
    End Property

    Public Property IdLinea As String
        Get
            Return _idLinea
        End Get
        Set
            _idLinea = Value
        End Set
    End Property

    Public Property Peso As Decimal
        Get
            Return _peso
        End Get
        Set
            _peso = Value
        End Set
    End Property

    Public Property Volumen As Decimal
        Get
            Return _volumen
        End Get
        Set
            _volumen = Value
        End Set
    End Property

    Public Property Area As Decimal
        Get
            Return _area
        End Get
        Set
            _area = Value
        End Set
    End Property

    Public Property Factor As Decimal
        Get
            Return _factor
        End Get
        Set
            _factor = Value
        End Set
    End Property

    Public Property Ancho As Decimal
        Get
            Return _ancho
        End Get
        Set
            _ancho = Value
        End Set
    End Property

    Public Property Largo As Decimal
        Get
            Return _largo
        End Get
        Set
            _largo = Value
        End Set
    End Property

    Public Property MonedaCosteo As String
        Get
            Return _monedaCosteo
        End Get
        Set
            _monedaCosteo = Value
        End Set
    End Property

    Public Property PrecioCosto As Decimal
        Get
            Return _precioCosto
        End Get
        Set
            _precioCosto = Value
        End Set
    End Property

    Public Property FechaCosto As System.DateTime
        Get
            Return _fechaCosto
        End Get
        Set
            _fechaCosto = Value
        End Set
    End Property

    Public Property IdMonedaCompra As String
        Get
            Return _idMonedaCompra
        End Get
        Set
            _idMonedaCompra = Value
        End Set
    End Property

    Public Property PrecioCompra As Decimal
        Get
            Return _precioCompra
        End Get
        Set
            _precioCompra = Value
        End Set
    End Property

    Public Property FechaCompra As System.DateTime
        Get
            Return _fechaCompra
        End Get
        Set
            _fechaCompra = Value
        End Set
    End Property

    Public Property IdProveedor As String
        Get
            Return _idProveedor
        End Get
        Set
            _idProveedor = Value
        End Set
    End Property

    Public Property MonedaFOB As String
        Get
            Return _monedaFOB
        End Get
        Set
            _monedaFOB = Value
        End Set
    End Property

    Public Property PrecioFOB As Decimal
        Get
            Return _precioFOB
        End Get
        Set
            _precioFOB = Value
        End Set
    End Property

    Public Property MargenUtilidad1 As Decimal
        Get
            Return _margenUtilidad1
        End Get
        Set
            _margenUtilidad1 = Value
        End Set
    End Property

    Public Property MargenUtilidad2 As Decimal
        Get
            Return _margenUtilidad2
        End Get
        Set
            _margenUtilidad2 = Value
        End Set
    End Property

    Public Property ClaseArticulo As String
        Get
            Return _claseArticulo
        End Get
        Set
            _claseArticulo = Value
        End Set
    End Property

    Public Property PartidaArancelaria As String
        Get
            Return _partidaArancelaria
        End Get
        Set
            _partidaArancelaria = Value
        End Set
    End Property

    Public Property TecnicaRotable As String
        Get
            Return _tecnicaRotable
        End Get
        Set
            _tecnicaRotable = Value
        End Set
    End Property

    Public Property CatalogoRotable As String
        Get
            Return _catalogoRotable
        End Get
        Set
            _catalogoRotable = Value
        End Set
    End Property

    Public Property CategoriaArancelaria As String
        Get
            Return _categoriaArancelaria
        End Get
        Set
            _categoriaArancelaria = Value
        End Set
    End Property

    Public Property swSinCosteo As String
        Get
            Return _swSinCosteo
        End Get
        Set
            _swSinCosteo = Value
        End Set
    End Property

    Public Property Observacion As String
        Get
            Return _observacion
        End Get
        Set
            _observacion = Value
        End Set
    End Property

    Public Property UnidadReferencia As String
        Get
            Return _unidadReferencia
        End Get
        Set
            _unidadReferencia = Value
        End Set
    End Property

    Public Property FactorReferencia As Decimal
        Get
            Return _factorReferencia
        End Get
        Set
            _factorReferencia = Value
        End Set
    End Property

    Public Property swReferencia As String
        Get
            Return _swReferencia
        End Get
        Set
            _swReferencia = Value
        End Set
    End Property

    Public Property swControlStock As String
        Get
            Return _swControlStock
        End Get
        Set
            _swControlStock = Value
        End Set
    End Property

    Public Property swDecimal As String
        Get
            Return _swDecimal
        End Get
        Set
            _swDecimal = Value
        End Set
    End Property

    Public Property swPrecioLibre As String
        Get
            Return _swPrecioLibre
        End Get
        Set
            _swPrecioLibre = Value
        End Set
    End Property

    Public Property swDescuentoImporte As String
        Get
            Return _swDescuentoImporte
        End Get
        Set
            _swDescuentoImporte = Value
        End Set
    End Property

    Public Property swSerie As String
        Get
            Return _swSerie
        End Get
        Set
            _swSerie = Value
        End Set
    End Property

    Public Property swLote As String
        Get
            Return _swLote
        End Get
        Set
            _swLote = Value
        End Set
    End Property

    Public Property swArticuloRotable As String
        Get
            Return _swArticuloRotable
        End Get
        Set
            _swArticuloRotable = Value
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

    Public Property UsuarioMod As String
        Get
            Return _usuarioMod
        End Get
        Set
            _usuarioMod = Value
        End Set
    End Property

    Public Property Estado As String
        Get
            Return _estado
        End Get
        Set
            _estado = Value
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

    Public Property FechaMod As System.DateTime
        Get
            Return _fechaMod
        End Get
        Set
            _fechaMod = Value
        End Set
    End Property

    Public Property IdArticulo3 As String
        Get
            Return _idArticulo3
        End Get
        Set
            _idArticulo3 = Value
        End Set
    End Property

    Public Property PorcDetraccion As Decimal
        Get
            Return _porcDetraccion
        End Get
        Set
            _porcDetraccion = Value
        End Set
    End Property

    Public Property Medida As String
        Get
            Return _medida
        End Get
        Set
            _medida = Value
        End Set
    End Property

    Public Property AR_CANNO As String
        Get
            Return _aR_CANNO
        End Get
        Set
            _aR_CANNO = Value
        End Set
    End Property

    Public Property Grosor As String
        Get
            Return _grosor
        End Get
        Set
            _grosor = Value
        End Set
    End Property

    Public Property RutaImagen As Decimal
        Get
            Return _rutaImagen
        End Get
        Set
            _rutaImagen = Value
        End Set
    End Property

    Public Property AR_CFECABC As String
        Get
            Return _aR_CFECABC
        End Get
        Set
            _aR_CFECABC = Value
        End Set
    End Property

    Public Property LongSerie As Decimal
        Get
            Return _longSerie
        End Get
        Set
            _longSerie = Value
        End Set
    End Property

    Public Property swCelular As String
        Get
            Return _swCelular
        End Get
        Set
            _swCelular = Value
        End Set
    End Property

    Public Property LongCelular As Decimal
        Get
            Return _longCelular
        End Get
        Set
            _longCelular = Value
        End Set
    End Property

    Public Property IdMarca As String
        Get
            Return _idMarca
        End Get
        Set
            _idMarca = Value
        End Set
    End Property

    Public Property Interior As Decimal
        Get
            Return _interior
        End Get
        Set
            _interior = Value
        End Set
    End Property

    Public Property Exterior As Decimal
        Get
            Return _exterior
        End Get
        Set
            _exterior = Value
        End Set
    End Property

    Public Property MargenUtilidad3 As Decimal
        Get
            Return _margenUtilidad3
        End Get
        Set
            _margenUtilidad3 = Value
        End Set
    End Property

    Public Property PrecioMinimo As Decimal
        Get
            Return _precioMinimo
        End Get
        Set
            _precioMinimo = Value
        End Set
    End Property

    Public Property ChkPercepcion As Boolean
        Get
            Return _chkPercepcion
        End Get
        Set
            _chkPercepcion = Value
        End Set
    End Property

    Public Property TasaPercepcion As Decimal
        Get
            Return _tasaPercepcion
        End Get
        Set
            _tasaPercepcion = Value
        End Set
    End Property

    Public Property FotoArt As Byte()
        Get
            Return _fotoArt
        End Get
        Set
            _fotoArt = Value
        End Set
    End Property

    Public Property TipoPrecio As String
        Get
            Return _tipoPrecio
        End Get
        Set
            _tipoPrecio = Value
        End Set
    End Property

    Public Property FotoArt1 As Byte()
        Get
            Return _fotoArt1
        End Get
        Set
            _fotoArt1 = Value
        End Set
    End Property

    Public Property FotoArt2 As Byte()
        Get
            Return _fotoArt2
        End Get
        Set
            _fotoArt2 = Value
        End Set
    End Property

    Public Property Observacion1 As Byte()
        Get
            Return _observacion1
        End Get
        Set
            _observacion1 = Value
        End Set
    End Property

    Public Property procedencia As String
        Get
            Return _procedencia
        End Get
        Set
            _procedencia = Value
        End Set
    End Property


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(ByVal idArticulo As String, ByVal descripcion1 As String, ByVal descripcion2 As String, ByVal idArticulo2 As String, ByVal unidad As String, ByVal idCuentaContable As String, ByVal precio1 As Decimal, ByVal precio2 As Decimal, ByVal precio3 As Decimal, ByVal precio4 As Decimal, ByVal precio5 As Decimal, ByVal precio6 As Decimal, ByVal monedaVenta As String, ByVal iGV As Decimal, ByVal iSC As Decimal, ByVal tipoArticulo As String, ByVal controlRotable As String, ByVal tipoDescuento As String, ByVal descuento As Decimal, ByVal descuento2 As Decimal, ByVal porcentajeDistribuidor As Decimal, ByVal porcentajeComision As Decimal, ByVal idGrupo As String, ByVal idFamilia As String, ByVal idModelo As String, ByVal idLinea As String, ByVal peso As Decimal, ByVal volumen As Decimal, ByVal area As Decimal, ByVal factor As Decimal, ByVal ancho As Decimal, ByVal largo As Decimal, ByVal monedaCosteo As String, ByVal precioCosto As Decimal, ByVal fechaCosto As System.DateTime, ByVal idMonedaCompra As String, ByVal precioCompra As Decimal, ByVal fechaCompra As System.DateTime, ByVal idProveedor As String, ByVal monedaFOB As String, ByVal precioFOB As Decimal, ByVal margenUtilidad1 As Decimal, ByVal margenUtilidad2 As Decimal, ByVal claseArticulo As String, ByVal partidaArancelaria As String, ByVal tecnicaRotable As String, ByVal catalogoRotable As String, ByVal categoriaArancelaria As String, ByVal swSinCosteo As String, ByVal observacion As String, ByVal unidadReferencia As String, ByVal factorReferencia As Decimal, ByVal swReferencia As String, ByVal swControlStock As String, ByVal swDecimal As String, ByVal swPrecioLibre As String, ByVal swDescuentoImporte As String, ByVal swSerie As String, ByVal swLote As String, ByVal swArticuloRotable As String, ByVal usuarioCrea As String, ByVal usuarioMod As String, ByVal estado As String, ByVal fechaCrea As System.DateTime, ByVal fechaMod As System.DateTime, ByVal idArticulo3 As String, ByVal porcDetraccion As Decimal, ByVal medida As String, ByVal aR_CANNO As String, ByVal grosor As String, ByVal rutaImagen As Decimal, ByVal aR_CFECABC As String, ByVal longSerie As Decimal, ByVal swCelular As String, ByVal longCelular As Decimal, ByVal idMarca As String, ByVal interior As Decimal, ByVal exterior As Decimal, ByVal margenUtilidad3 As Decimal, ByVal precioMinimo As Decimal, ByVal chkPercepcion As Boolean, ByVal tasaPercepcion As Decimal, ByVal fotoArt As Byte(), ByVal tipoPrecio As String, ByVal fotoArt1 As Byte(), ByVal fotoArt2 As Byte(), ByVal observacion1 As Byte(), ByVal procedencia As String)
        Me.New()
    End Sub

#End Region

#Region "Metodos"
    Public Sub Agregar(d As NArticulo)
        Dim parametros() As Object = {"@idArticulo", "@descripcion1", "@descripcion2", "@idArticulo2", "@unidad", "@idCuentaContable", "@precio1", "@precio2", "@precio3", "@precio4", "@precio5", "@precio6", "@monedaVenta", "@iGV", "@iSC", "@tipoArticulo", "@controlRotable", "@tipoDescuento", "@descuento", "@descuento2", "@porcentajeDistribuidor", "@porcentajeComision", "@idGrupo", "@idFamilia", "@idModelo", "@idLinea", "@peso", "@volumen", "@area", "@factor", "@ancho", "@largo", "@monedaCosteo", "@precioCosto", "@fechaCosto", "@idMonedaCompra", "@precioCompra", "@fechaCompra", "@idProveedor", "@monedaFOB", "@precioFOB", "@margenUtilidad1", "@margenUtilidad2", "@claseArticulo", "@partidaArancelaria", "@tecnicaRotable", "@catalogoRotable", "@categoriaArancelaria", "@swSinCosteo", "@observacion", "@unidadReferencia", "@factorReferencia", "@swReferencia", "@swControlStock", "@swDecimal", "@swPrecioLibre", "@swDescuentoImporte", "@swSerie", "@swLote", "@swArticuloRotable", "@usuarioCrea", "@usuarioMod", "@estado", "@fechaCrea", "@fechaMod", "@idArticulo3", "@porcDetraccion", "@medida", "@aR_CANNO", "@grosor", "@rutaImagen", "@aR_CFECABC", "@longSerie", "@swCelular", "@longCelular", "@idMarca", "@interior", "@exterior", "@margenUtilidad3", "@precioMinimo", "@chkPercepcion", "@tasaPercepcion", "@fotoArt", "@tipoPrecio", "@fotoArt1", "@fotoArt2", "@observacion1", "@procedencia"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.VarBinary, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdArticulo, d.Descripcion1, d.Descripcion2, d.IdArticulo2, d.Unidad, d.IdCuentaContable, d.Precio1, d.Precio2, d.Precio3, d.Precio4, d.Precio5, d.Precio6, d.MonedaVenta, d.IGV, d.ISC, d.TipoArticulo, d.ControlRotable, d.TipoDescuento, d.Descuento, d.Descuento2, d.PorcentajeDistribuidor, d.PorcentajeComision, d.IdGrupo, d.IdFamilia, d.IdModelo, d.IdLinea, d.Peso, d.Volumen, d.Area, d.Factor, d.Ancho, d.Largo, d.MonedaCosteo, d.PrecioCosto, d.FechaCosto, d.IdMonedaCompra, d.PrecioCompra, d.FechaCompra, d.IdProveedor, d.MonedaFOB, d.PrecioFOB, d.MargenUtilidad1, d.MargenUtilidad2, d.ClaseArticulo, d.PartidaArancelaria, d.TecnicaRotable, d.CatalogoRotable, d.CategoriaArancelaria, d.swSinCosteo, d.Observacion, d.UnidadReferencia, d.FactorReferencia, d.swReferencia, d.swControlStock, d.swDecimal, d.swPrecioLibre, d.swDescuentoImporte, d.swSerie, d.swLote, d.swArticuloRotable, d.UsuarioCrea, d.UsuarioMod, d.Estado, d.FechaCrea, d.FechaMod, d.IdArticulo3, d.PorcDetraccion, d.Medida, d.AR_CANNO, d.Grosor, d.RutaImagen, d.AR_CFECABC, d.LongSerie, d.swCelular, d.LongCelular, d.IdMarca, d.Interior, d.Exterior, d.MargenUtilidad3, d.PrecioMinimo, d.ChkPercepcion, d.TasaPercepcion, d.FotoArt, d.TipoPrecio, d.FotoArt1, d.FotoArt2, d.Observacion1, d.procedencia}
        sql.EjecutarProcedure("Str_Articulo_I", parametros, valores, tipoParametro, 88)
    End Sub
    Public Sub Actualizar(d As NArticulo)
        Dim parametros() As Object = {"@idArticulo", "@descripcion1", "@descripcion2", "@idArticulo2", "@unidad", "@idCuentaContable", "@precio1", "@precio2", "@precio3", "@precio4", "@precio5", "@precio6", "@monedaVenta", "@iGV", "@iSC", "@tipoArticulo", "@controlRotable", "@tipoDescuento", "@descuento", "@descuento2", "@porcentajeDistribuidor", "@porcentajeComision", "@idGrupo", "@idFamilia", "@idModelo", "@idLinea", "@peso", "@volumen", "@area", "@factor", "@ancho", "@largo", "@monedaCosteo", "@precioCosto", "@fechaCosto", "@idMonedaCompra", "@precioCompra", "@fechaCompra", "@idProveedor", "@monedaFOB", "@precioFOB", "@margenUtilidad1", "@margenUtilidad2", "@claseArticulo", "@partidaArancelaria", "@tecnicaRotable", "@catalogoRotable", "@categoriaArancelaria", "@swSinCosteo", "@observacion", "@unidadReferencia", "@factorReferencia", "@swReferencia", "@swControlStock", "@swDecimal", "@swPrecioLibre", "@swDescuentoImporte", "@swSerie", "@swLote", "@swArticuloRotable", "@usuarioCrea", "@usuarioMod", "@estado", "@fechaCrea", "@fechaMod", "@idArticulo3", "@porcDetraccion", "@medida", "@aR_CANNO", "@grosor", "@rutaImagen", "@aR_CFECABC", "@longSerie", "@swCelular", "@longCelular", "@idMarca", "@interior", "@exterior", "@margenUtilidad3", "@precioMinimo", "@chkPercepcion", "@tasaPercepcion", "@fotoArt", "@tipoPrecio", "@fotoArt1", "@fotoArt2", "@observacion1", "@procedencia"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.VarBinary, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarBinary, SqlDbType.VarChar}
        Dim valores() As Object = {d.IdArticulo, d.Descripcion1, d.Descripcion2, d.IdArticulo2, d.Unidad, d.IdCuentaContable, d.Precio1, d.Precio2, d.Precio3, d.Precio4, d.Precio5, d.Precio6, d.MonedaVenta, d.IGV, d.ISC, d.TipoArticulo, d.ControlRotable, d.TipoDescuento, d.Descuento, d.Descuento2, d.PorcentajeDistribuidor, d.PorcentajeComision, d.IdGrupo, d.IdFamilia, d.IdModelo, d.IdLinea, d.Peso, d.Volumen, d.Area, d.Factor, d.Ancho, d.Largo, d.MonedaCosteo, d.PrecioCosto, d.FechaCosto, d.IdMonedaCompra, d.PrecioCompra, d.FechaCompra, d.IdProveedor, d.MonedaFOB, d.PrecioFOB, d.MargenUtilidad1, d.MargenUtilidad2, d.ClaseArticulo, d.PartidaArancelaria, d.TecnicaRotable, d.CatalogoRotable, d.CategoriaArancelaria, d.swSinCosteo, d.Observacion, d.UnidadReferencia, d.FactorReferencia, d.swReferencia, d.swControlStock, d.swDecimal, d.swPrecioLibre, d.swDescuentoImporte, d.swSerie, d.swLote, d.swArticuloRotable, d.UsuarioCrea, d.UsuarioMod, d.Estado, d.FechaCrea, d.FechaMod, d.IdArticulo3, d.PorcDetraccion, d.Medida, d.AR_CANNO, d.Grosor, d.RutaImagen, d.AR_CFECABC, d.LongSerie, d.swCelular, d.LongCelular, d.IdMarca, d.Interior, d.Exterior, d.MargenUtilidad3, d.PrecioMinimo, d.ChkPercepcion, d.TasaPercepcion, d.FotoArt, d.TipoPrecio, d.FotoArt1, d.FotoArt2, d.Observacion1, d.procedencia}
        sql.EjecutarProcedure("Str_Articulo_U", parametros, valores, tipoParametro, 88)
    End Sub
    Public Sub Eliminar(d As NArticulo)
        Dim parametros() As Object = {"@idArticulo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.IdArticulo}
        sql.EjecutarProcedure("Str_Articulo_D", parametros, valores, tipoParametro, 1)
    End Sub
    Public Function Lista() As DataTable
        Dim parametros() As Object = {"@idArticulo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {DBNull.Value}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_Articulo_S", parametros, valores, tipoParametro, 1).Tables(0)
        Return dt
    End Function
    Public Function Articulo_Lista() As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "select cast(0 as bit) as Flg, IdArticulo,Descripcion1,vf.descripcion as Familia from Articulo a left join vfamilia vf on a.idfamilia=vf.idcodigo ").Tables(0)
        Return dt
    End Function
    Public Function Buscar(id As String) As DataTable
        Dim dt As New DataTable
        If IsNumeric(id) Then
            Return sql.EjecutarConsulta("d", "select a.idarticulo, a.descripcion1, a.unidad, a.idGrupo,isnull(s.StockDisponible,0)" +
                            " AS StockDisponible,isnull(s.StockComprometido,0) as StockComprometido,a.swControlStock," +
                            "swPrecioLibre,Precio1,isnull(TipoPrecio,'A') AS TipoPrecio " +
                            "FROM Articulo AS a LEFT OUTER JOIN  Stock AS s ON a.IdArticulo = s.IdArticulo " +
                            "where cast(right(rtrim(a.idarticulo),5) as int)='" + (id.Trim()) + "' " +
                            "and isnumeric(a.idarticulo)=1 ").Tables(0)
        Else
            Return sql.EjecutarConsulta("d", " select a.idarticulo, a.descripcion1, a.unidad, a.idGrupo,isnull(s.StockDisponible,0) " +
                            "AS StockDisponible,isnull(s.StockComprometido,0) as StockComprometido ,a.swControlStock," +
                            "swPrecioLibre,Precio1,isnull(TipoPrecio,'A') AS TipoPrecio FROM Articulo AS a " +
                            "LEFT OUTER JOIN  Stock AS s ON a.IdArticulo = s.IdArticulo where a.idarticulo='" +
                            (id.Trim()) + "'").Tables(0)
        End If


    End Function
    Public Function Conversion(IdArticulo As String) As DataTable
        Dim ca As String = " select idventa,unidadDestino,medida from tbl_Articulo_UnidadVenta a " +
                "inner join unidadequivalenxarticulo u "
        ca += " on a.IdVenta =u.ID where a.idarticulo='" + IdArticulo + "'"
        Return sql.EjecutarConsulta("D", ca).Tables(0)
    End Function
    Public Function ObtenerPrecio(IdArticulo As String) As DataTable
        Dim ca As String = "select  rtrim(a.idarticulo)as Idarticulo, a.descripcion1, a.unidad, a.idGrupo," +
                "isnull(s.StockDisponible,0) AS StockDisponible,a.swControlStock,swPrecioLibre,Precio1," +
                "isnull(TipoPrecio,'A') AS TipoPrecio FROM Articulo AS a LEFT OUTER JOIN  Stock AS s ON a.IdArticulo = s.IdArticulo " +
                "where cast(right(rtrim(a.idarticulo),5) as int)='" + IdArticulo + "' and isnumeric(a.idarticulo)=1"
        Return sql.EjecutarConsulta("D", ca).Tables(0)
    End Function
    Public Function Articulo_factura(id As String) As DataTable
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("d", "Select IdArticulo, Descripcion1 As Descripcion,case when FotoArt IS NULL  then '' else 'S' end as RutaImagen from Articulo where idarticulo like '" & id & "%'").Tables(0)
        Return dt
    End Function
    Public Function catalogo_articulo() As DataTable
        Dim cad As String = " SELECT   a.IdArticulo, a.Descripcion1, m.IdMarca, m.Marca, a.IdFamilia, f.Descripcion AS familia,
        a.IdGrupo, g.Descripcion AS grupo, a.Unidad, a.IdCuentaContable, a.Peso, a.swControlStock, 
        a.swLote, a.IdArticulo3, a.Descripcion2 "
        cad += " FROM Articulo AS a LEFT OUTER JOIN VMarca AS m ON a.IdMarca = m.IdMarca LEFT OUTER JOIN VFAMILIA AS f ON a.IdFamilia = f.IdCodigo LEFT OUTER JOIN VGrupo AS g ON a.IdGrupo = g.IdCodigo "
        Return sql.EjecutarConsulta("d", cad).Tables(0)
    End Function
    Public Function catalogo_articuloStock() As DataTable
        Dim cad As String = "SELECT rtrim(a.IdArticulo) AS IdArticulo, a.Descripcion1, m.Marca, 
                    a.Unidad,  
	                ISNULL(s.StockDisponible,0) StockDisponible, 
	                isnull(s.StockComprometido,0) as StockComprometido,
	                a.swDescuentoImporte
                    FROM Articulo AS a 
                    LEFT  JOIN VMarca AS m ON a.IdMarca = m.IdMarca 
                    left join Stock s on a.IdArticulo= s.IdArticulo"
        Return sql.EjecutarConsulta("d", cad).Tables(0)
    End Function
    Public Function catalogo_pedido() As DataTable
        Dim cad As String = " SELECT    cast(0 as bit) as chec, IdtipoDocumento, Serie,NumeroDocumento, IdCliente, NombreCliente "
        cad += " FROM  Pedido "
        Return sql.EjecutarConsulta("d", cad).Tables(0)
    End Function
    Public Function Registro(d As NArticulo) As NArticulo
        Dim parametros() As Object = {"@idArticulo"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar}
        Dim valores() As Object = {d.IdArticulo}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_Articulo_S", parametros, valores, tipoParametro, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdArticulo = IIf(dt.Rows(0).Item("idArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo"))
            d.Descripcion1 = IIf(dt.Rows(0).Item("descripcion1") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion1"))
            d.Descripcion2 = IIf(dt.Rows(0).Item("descripcion2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion2"))
            d.IdArticulo2 = IIf(dt.Rows(0).Item("idArticulo2") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo2"))
            d.Unidad = IIf(dt.Rows(0).Item("unidad") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidad"))
            d.IdCuentaContable = IIf(dt.Rows(0).Item("idCuentaContable") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCuentaContable"))
            d.Precio1 = IIf(dt.Rows(0).Item("precio1") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio1"))
            d.Precio2 = IIf(dt.Rows(0).Item("precio2") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio2"))
            d.Precio3 = IIf(dt.Rows(0).Item("precio3") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio3"))
            d.Precio4 = IIf(dt.Rows(0).Item("precio4") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio4"))
            d.Precio5 = IIf(dt.Rows(0).Item("precio5") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio5"))
            d.Precio6 = IIf(dt.Rows(0).Item("precio6") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio6"))
            d.MonedaVenta = IIf(dt.Rows(0).Item("monedaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaVenta"))
            d.IGV = IIf(dt.Rows(0).Item("iGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("iGV"))
            d.ISC = IIf(dt.Rows(0).Item("iSC") Is DBNull.Value, Nothing, dt.Rows(0).Item("iSC"))
            d.TipoArticulo = IIf(dt.Rows(0).Item("tipoArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoArticulo"))
            d.ControlRotable = IIf(dt.Rows(0).Item("controlRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("controlRotable"))
            d.TipoDescuento = IIf(dt.Rows(0).Item("tipoDescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoDescuento"))
            d.Descuento = IIf(dt.Rows(0).Item("descuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento"))
            d.Descuento2 = IIf(dt.Rows(0).Item("descuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento2"))
            d.PorcentajeDistribuidor = IIf(dt.Rows(0).Item("porcentajeDistribuidor") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcentajeDistribuidor"))
            d.PorcentajeComision = IIf(dt.Rows(0).Item("porcentajeComision") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcentajeComision"))
            d.IdGrupo = IIf(dt.Rows(0).Item("idGrupo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idGrupo"))
            d.IdFamilia = IIf(dt.Rows(0).Item("idFamilia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idFamilia"))
            d.IdModelo = IIf(dt.Rows(0).Item("idModelo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idModelo"))
            d.IdLinea = IIf(dt.Rows(0).Item("idLinea") Is DBNull.Value, Nothing, dt.Rows(0).Item("idLinea"))
            d.Peso = IIf(dt.Rows(0).Item("peso") Is DBNull.Value, Nothing, dt.Rows(0).Item("peso"))
            d.Volumen = IIf(dt.Rows(0).Item("volumen") Is DBNull.Value, Nothing, dt.Rows(0).Item("volumen"))
            d.Area = IIf(dt.Rows(0).Item("area") Is DBNull.Value, Nothing, dt.Rows(0).Item("area"))
            d.Factor = IIf(dt.Rows(0).Item("factor") Is DBNull.Value, Nothing, dt.Rows(0).Item("factor"))
            d.Ancho = IIf(dt.Rows(0).Item("ancho") Is DBNull.Value, Nothing, dt.Rows(0).Item("ancho"))
            d.Largo = IIf(dt.Rows(0).Item("largo") Is DBNull.Value, Nothing, dt.Rows(0).Item("largo"))
            d.MonedaCosteo = IIf(dt.Rows(0).Item("monedaCosteo") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaCosteo"))
            d.PrecioCosto = IIf(dt.Rows(0).Item("precioCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioCosto"))
            d.FechaCosto = IIf(dt.Rows(0).Item("fechaCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCosto"))
            d.IdMonedaCompra = IIf(dt.Rows(0).Item("idMonedaCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMonedaCompra"))
            d.PrecioCompra = IIf(dt.Rows(0).Item("precioCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioCompra"))
            d.FechaCompra = IIf(dt.Rows(0).Item("fechaCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCompra"))
            d.IdProveedor = IIf(dt.Rows(0).Item("idProveedor") Is DBNull.Value, Nothing, dt.Rows(0).Item("idProveedor"))
            d.MonedaFOB = IIf(dt.Rows(0).Item("monedaFOB") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaFOB"))
            d.PrecioFOB = IIf(dt.Rows(0).Item("precioFOB") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioFOB"))
            d.MargenUtilidad1 = IIf(dt.Rows(0).Item("margenUtilidad1") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad1"))
            d.MargenUtilidad2 = IIf(dt.Rows(0).Item("margenUtilidad2") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad2"))
            d.ClaseArticulo = IIf(dt.Rows(0).Item("claseArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("claseArticulo"))
            d.PartidaArancelaria = IIf(dt.Rows(0).Item("partidaArancelaria") Is DBNull.Value, Nothing, dt.Rows(0).Item("partidaArancelaria"))
            d.TecnicaRotable = IIf(dt.Rows(0).Item("tecnicaRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("tecnicaRotable"))
            d.CatalogoRotable = IIf(dt.Rows(0).Item("catalogoRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("catalogoRotable"))
            d.CategoriaArancelaria = IIf(dt.Rows(0).Item("categoriaArancelaria") Is DBNull.Value, Nothing, dt.Rows(0).Item("categoriaArancelaria"))
            d.swSinCosteo = IIf(dt.Rows(0).Item("swSinCosteo") Is DBNull.Value, Nothing, dt.Rows(0).Item("swSinCosteo"))
            d.Observacion = IIf(dt.Rows(0).Item("observacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("observacion"))
            d.UnidadReferencia = IIf(dt.Rows(0).Item("unidadReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidadReferencia"))
            d.FactorReferencia = IIf(dt.Rows(0).Item("factorReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("factorReferencia"))
            d.swReferencia = IIf(dt.Rows(0).Item("swReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("swReferencia"))
            d.swControlStock = IIf(dt.Rows(0).Item("swControlStock") Is DBNull.Value, Nothing, dt.Rows(0).Item("swControlStock"))
            d.swDecimal = IIf(dt.Rows(0).Item("swDecimal") Is DBNull.Value, Nothing, dt.Rows(0).Item("swDecimal"))
            d.swPrecioLibre = IIf(dt.Rows(0).Item("swPrecioLibre") Is DBNull.Value, Nothing, dt.Rows(0).Item("swPrecioLibre"))
            d.swDescuentoImporte = IIf(dt.Rows(0).Item("swDescuentoImporte") Is DBNull.Value, Nothing, dt.Rows(0).Item("swDescuentoImporte"))
            d.swSerie = IIf(dt.Rows(0).Item("swSerie") Is DBNull.Value, Nothing, dt.Rows(0).Item("swSerie"))
            d.swLote = IIf(dt.Rows(0).Item("swLote") Is DBNull.Value, Nothing, dt.Rows(0).Item("swLote"))
            d.swArticuloRotable = IIf(dt.Rows(0).Item("swArticuloRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("swArticuloRotable"))
            d.UsuarioCrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.UsuarioMod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.Estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.FechaCrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.FechaMod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.IdArticulo3 = IIf(dt.Rows(0).Item("idArticulo3") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo3"))
            d.PorcDetraccion = IIf(dt.Rows(0).Item("porcDetraccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcDetraccion"))
            d.Medida = IIf(dt.Rows(0).Item("medida") Is DBNull.Value, Nothing, dt.Rows(0).Item("medida"))
            d.AR_CANNO = IIf(dt.Rows(0).Item("aR_CANNO") Is DBNull.Value, Nothing, dt.Rows(0).Item("aR_CANNO"))
            d.Grosor = IIf(dt.Rows(0).Item("grosor") Is DBNull.Value, Nothing, dt.Rows(0).Item("grosor"))
            d.RutaImagen = IIf(dt.Rows(0).Item("rutaImagen") Is DBNull.Value, Nothing, dt.Rows(0).Item("rutaImagen"))
            d.AR_CFECABC = IIf(dt.Rows(0).Item("aR_CFECABC") Is DBNull.Value, Nothing, dt.Rows(0).Item("aR_CFECABC"))
            d.LongSerie = IIf(dt.Rows(0).Item("longSerie") Is DBNull.Value, Nothing, dt.Rows(0).Item("longSerie"))
            d.swCelular = IIf(dt.Rows(0).Item("swCelular") Is DBNull.Value, Nothing, dt.Rows(0).Item("swCelular"))
            d.LongCelular = IIf(dt.Rows(0).Item("longCelular") Is DBNull.Value, Nothing, dt.Rows(0).Item("longCelular"))
            d.IdMarca = IIf(dt.Rows(0).Item("idMarca") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMarca"))
            d.Interior = IIf(dt.Rows(0).Item("interior") Is DBNull.Value, Nothing, dt.Rows(0).Item("interior"))
            d.Exterior = IIf(dt.Rows(0).Item("exterior") Is DBNull.Value, Nothing, dt.Rows(0).Item("exterior"))
            d.MargenUtilidad3 = IIf(dt.Rows(0).Item("margenUtilidad3") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad3"))
            d.PrecioMinimo = IIf(dt.Rows(0).Item("precioMinimo") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioMinimo"))
            d.ChkPercepcion = IIf(dt.Rows(0).Item("chkPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("chkPercepcion"))
            d.TasaPercepcion = IIf(dt.Rows(0).Item("tasaPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("tasaPercepcion"))
            d.FotoArt = IIf(dt.Rows(0).Item("fotoArt") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt"))
            d.TipoPrecio = IIf(dt.Rows(0).Item("tipoPrecio") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoPrecio"))
            d.FotoArt1 = IIf(dt.Rows(0).Item("fotoArt1") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt1"))
            d.FotoArt2 = IIf(dt.Rows(0).Item("fotoArt2") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt2"))
            d.Observacion1 = IIf(dt.Rows(0).Item("observacion1") Is DBNull.Value, Nothing, dt.Rows(0).Item("observacion1"))
            d.procedencia = IIf(dt.Rows(0).Item("procedencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("procedencia"))
        Else
            d.IdArticulo = Nothing
            d.Descripcion1 = Nothing
            d.Descripcion2 = Nothing
            d.IdArticulo2 = Nothing
            d.Unidad = Nothing
            d.IdCuentaContable = Nothing
            d.Precio1 = Nothing
            d.Precio2 = Nothing
            d.Precio3 = Nothing
            d.Precio4 = Nothing
            d.Precio5 = Nothing
            d.Precio6 = Nothing
            d.MonedaVenta = Nothing
            d.IGV = Nothing
            d.ISC = Nothing
            d.TipoArticulo = Nothing
            d.ControlRotable = Nothing
            d.TipoDescuento = Nothing
            d.Descuento = Nothing
            d.Descuento2 = Nothing
            d.PorcentajeDistribuidor = Nothing
            d.PorcentajeComision = Nothing
            d.IdGrupo = Nothing
            d.IdFamilia = Nothing
            d.IdModelo = Nothing
            d.IdLinea = Nothing
            d.Peso = Nothing
            d.Volumen = Nothing
            d.Area = Nothing
            d.Factor = Nothing
            d.Ancho = Nothing
            d.Largo = Nothing
            d.MonedaCosteo = Nothing
            d.PrecioCosto = Nothing
            d.FechaCosto = Nothing
            d.IdMonedaCompra = Nothing
            d.PrecioCompra = Nothing
            d.FechaCompra = Nothing
            d.IdProveedor = Nothing
            d.MonedaFOB = Nothing
            d.PrecioFOB = Nothing
            d.MargenUtilidad1 = Nothing
            d.MargenUtilidad2 = Nothing
            d.ClaseArticulo = Nothing
            d.PartidaArancelaria = Nothing
            d.TecnicaRotable = Nothing
            d.CatalogoRotable = Nothing
            d.CategoriaArancelaria = Nothing
            d.swSinCosteo = Nothing
            d.Observacion = Nothing
            d.UnidadReferencia = Nothing
            d.FactorReferencia = Nothing
            d.swReferencia = Nothing
            d.swControlStock = Nothing
            d.swDecimal = Nothing
            d.swPrecioLibre = Nothing
            d.swDescuentoImporte = Nothing
            d.swSerie = Nothing
            d.swLote = Nothing
            d.swArticuloRotable = Nothing
            d.UsuarioCrea = Nothing
            d.UsuarioMod = Nothing
            d.Estado = Nothing
            d.FechaCrea = Nothing
            d.FechaMod = Nothing
            d.IdArticulo3 = Nothing
            d.PorcDetraccion = Nothing
            d.Medida = Nothing
            d.AR_CANNO = Nothing
            d.Grosor = Nothing
            d.RutaImagen = Nothing
            d.AR_CFECABC = Nothing
            d.LongSerie = Nothing
            d.swCelular = Nothing
            d.LongCelular = Nothing
            d.IdMarca = Nothing
            d.Interior = Nothing
            d.Exterior = Nothing
            d.MargenUtilidad3 = Nothing
            d.PrecioMinimo = Nothing
            d.ChkPercepcion = Nothing
            d.TasaPercepcion = Nothing
            d.FotoArt = Nothing
            d.TipoPrecio = Nothing
            d.FotoArt1 = Nothing
            d.FotoArt2 = Nothing
            d.Observacion1 = Nothing
            d.procedencia = Nothing
        End If
        Return d
    End Function

    Public Function ControlStock(s As NArticulo) As Boolean
        Dim cadena As String = "select swControlStock from ARTICULO WHERE IDARTICULO='" & s.IdArticulo & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("stock", cadena).Tables(0)
        Dim bandera As Boolean = False
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item(0) = "S" Then
                bandera = True
            Else
                bandera = False
            End If
        End If
        Return bandera
    End Function

    Public Function Existe(a As NArticulo) As Boolean
        Dim existeC As String
        Dim bandera As Boolean = False
        Dim valoresC() As Object = {"'" & a.IdArticulo & "'"}
        existeC = sql.ValorEscalar("dbo.Articulo_Existe", valoresC, 1)
        If existeC = "1" Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function

    Public Function Articulo(a As NArticulo) As NArticulo
        Dim dt As New DataTable
        Dim valores() As Object = {a.IdArticulo}
        Dim campos() As Object = {"@IdArticulo"}
        Dim tipodatos() As Object = {SqlDbType.Char}
        dt = sql.ProcedureSQL("dbo.Str_FndArticulo", campos, valores, tipodatos, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            With row
                a.IdArticulo = .Item("Idarticulo")
                a.Descripcion1 = .Item("Descripcion1")
                a.IdArticulo2 = .Item("IdArticulo2")
                a.IdCuentaContable = .Item("IdCuentaContable").ToString
                a.swControlStock = .Item("swControlStock")
                a.Unidad = .Item("Unidad")
                a.ChkPercepcion = .Item("chkPercepcion")
                a.TasaPercepcion = .Item("TasaPercepcion")
                a.PrecioMinimo = Val(.Item("PrecioMinimo"))
                a.Precio1 = Val(.Item("Precio1"))
                a.swLote = .Item("swLote")
            End With
        End If
        Return a
    End Function
    ''' <summary>
    ''' Obtiene el codigo del producto a partir de la descricpion
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>
    Public Function item(d As NArticulo) As NArticulo
        Dim dt As New DataTable
        Dim valores() As Object = {d.IdArticulo, d.Descripcion1}
        Dim campos() As Object = {"@IdArticulo", "@Descripcion1"}
        Dim tipodatos() As Object = {SqlDbType.Char, SqlDbType.VarChar}
        dt = sql.ProcedureSQL("Str_Articulo_Descripcion", campos, valores, tipodatos, 2).Tables(0)
        If dt.Rows.Count > 0 Then
            d.IdArticulo = IIf(dt.Rows(0).Item("idArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo"))
            d.Descripcion1 = IIf(dt.Rows(0).Item("descripcion1") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion1"))
            d.Descripcion2 = IIf(dt.Rows(0).Item("descripcion2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion2"))
            d.IdArticulo2 = IIf(dt.Rows(0).Item("idArticulo2") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo2"))
            d.Unidad = IIf(dt.Rows(0).Item("unidad") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidad"))
            d.IdCuentaContable = IIf(dt.Rows(0).Item("idCuentaContable") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCuentaContable"))
            d.Precio1 = IIf(dt.Rows(0).Item("precio1") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio1"))
            d.Precio2 = IIf(dt.Rows(0).Item("precio2") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio2"))
            d.Precio3 = IIf(dt.Rows(0).Item("precio3") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio3"))
            d.Precio4 = IIf(dt.Rows(0).Item("precio4") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio4"))
            d.Precio5 = IIf(dt.Rows(0).Item("precio5") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio5"))
            d.Precio6 = IIf(dt.Rows(0).Item("precio6") Is DBNull.Value, Nothing, dt.Rows(0).Item("precio6"))
            d.MonedaVenta = IIf(dt.Rows(0).Item("monedaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaVenta"))
            d.IGV = IIf(dt.Rows(0).Item("iGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("iGV"))
            d.ISC = IIf(dt.Rows(0).Item("iSC") Is DBNull.Value, Nothing, dt.Rows(0).Item("iSC"))
            d.TipoArticulo = IIf(dt.Rows(0).Item("tipoArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoArticulo"))
            d.ControlRotable = IIf(dt.Rows(0).Item("controlRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("controlRotable"))
            d.TipoDescuento = IIf(dt.Rows(0).Item("tipoDescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoDescuento"))
            d.Descuento = IIf(dt.Rows(0).Item("descuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento"))
            d.Descuento2 = IIf(dt.Rows(0).Item("descuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento2"))
            d.PorcentajeDistribuidor = IIf(dt.Rows(0).Item("porcentajeDistribuidor") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcentajeDistribuidor"))
            d.PorcentajeComision = IIf(dt.Rows(0).Item("porcentajeComision") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcentajeComision"))
            d.IdGrupo = IIf(dt.Rows(0).Item("idGrupo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idGrupo"))
            d.IdFamilia = IIf(dt.Rows(0).Item("idFamilia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idFamilia"))
            d.IdModelo = IIf(dt.Rows(0).Item("idModelo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idModelo"))
            d.IdLinea = IIf(dt.Rows(0).Item("idLinea") Is DBNull.Value, Nothing, dt.Rows(0).Item("idLinea"))
            d.Peso = IIf(dt.Rows(0).Item("peso") Is DBNull.Value, Nothing, dt.Rows(0).Item("peso"))
            d.Volumen = IIf(dt.Rows(0).Item("volumen") Is DBNull.Value, Nothing, dt.Rows(0).Item("volumen"))
            d.Area = IIf(dt.Rows(0).Item("area") Is DBNull.Value, Nothing, dt.Rows(0).Item("area"))
            d.Factor = IIf(dt.Rows(0).Item("factor") Is DBNull.Value, Nothing, dt.Rows(0).Item("factor"))
            d.Ancho = IIf(dt.Rows(0).Item("ancho") Is DBNull.Value, Nothing, dt.Rows(0).Item("ancho"))
            d.Largo = IIf(dt.Rows(0).Item("largo") Is DBNull.Value, Nothing, dt.Rows(0).Item("largo"))
            d.MonedaCosteo = IIf(dt.Rows(0).Item("monedaCosteo") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaCosteo"))
            d.PrecioCosto = IIf(dt.Rows(0).Item("precioCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioCosto"))
            d.FechaCosto = IIf(dt.Rows(0).Item("fechaCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCosto"))
            d.IdMonedaCompra = IIf(dt.Rows(0).Item("idMonedaCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMonedaCompra"))
            d.PrecioCompra = IIf(dt.Rows(0).Item("precioCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioCompra"))
            d.FechaCompra = IIf(dt.Rows(0).Item("fechaCompra") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCompra"))
            d.IdProveedor = IIf(dt.Rows(0).Item("idProveedor") Is DBNull.Value, Nothing, dt.Rows(0).Item("idProveedor"))
            d.MonedaFOB = IIf(dt.Rows(0).Item("monedaFOB") Is DBNull.Value, Nothing, dt.Rows(0).Item("monedaFOB"))
            d.PrecioFOB = IIf(dt.Rows(0).Item("precioFOB") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioFOB"))
            d.MargenUtilidad1 = IIf(dt.Rows(0).Item("margenUtilidad1") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad1"))
            d.MargenUtilidad2 = IIf(dt.Rows(0).Item("margenUtilidad2") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad2"))
            d.ClaseArticulo = IIf(dt.Rows(0).Item("claseArticulo") Is DBNull.Value, Nothing, dt.Rows(0).Item("claseArticulo"))
            d.PartidaArancelaria = IIf(dt.Rows(0).Item("partidaArancelaria") Is DBNull.Value, Nothing, dt.Rows(0).Item("partidaArancelaria"))
            d.TecnicaRotable = IIf(dt.Rows(0).Item("tecnicaRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("tecnicaRotable"))
            d.CatalogoRotable = IIf(dt.Rows(0).Item("catalogoRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("catalogoRotable"))
            d.CategoriaArancelaria = IIf(dt.Rows(0).Item("categoriaArancelaria") Is DBNull.Value, Nothing, dt.Rows(0).Item("categoriaArancelaria"))
            d.swSinCosteo = IIf(dt.Rows(0).Item("swSinCosteo") Is DBNull.Value, Nothing, dt.Rows(0).Item("swSinCosteo"))
            d.Observacion = IIf(dt.Rows(0).Item("observacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("observacion"))
            d.UnidadReferencia = IIf(dt.Rows(0).Item("unidadReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("unidadReferencia"))
            d.FactorReferencia = IIf(dt.Rows(0).Item("factorReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("factorReferencia"))
            d.swReferencia = IIf(dt.Rows(0).Item("swReferencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("swReferencia"))
            d.swControlStock = IIf(dt.Rows(0).Item("swControlStock") Is DBNull.Value, Nothing, dt.Rows(0).Item("swControlStock"))
            d.swDecimal = IIf(dt.Rows(0).Item("swDecimal") Is DBNull.Value, Nothing, dt.Rows(0).Item("swDecimal"))
            d.swPrecioLibre = IIf(dt.Rows(0).Item("swPrecioLibre") Is DBNull.Value, Nothing, dt.Rows(0).Item("swPrecioLibre"))
            d.swDescuentoImporte = IIf(dt.Rows(0).Item("swDescuentoImporte") Is DBNull.Value, Nothing, dt.Rows(0).Item("swDescuentoImporte"))
            d.swSerie = IIf(dt.Rows(0).Item("swSerie") Is DBNull.Value, Nothing, dt.Rows(0).Item("swSerie"))
            d.swLote = IIf(dt.Rows(0).Item("swLote") Is DBNull.Value, Nothing, dt.Rows(0).Item("swLote"))
            d.swArticuloRotable = IIf(dt.Rows(0).Item("swArticuloRotable") Is DBNull.Value, Nothing, dt.Rows(0).Item("swArticuloRotable"))
            d.UsuarioCrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.UsuarioMod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.Estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.FechaCrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.FechaMod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.IdArticulo3 = IIf(dt.Rows(0).Item("idArticulo3") Is DBNull.Value, Nothing, dt.Rows(0).Item("idArticulo3"))
            d.PorcDetraccion = IIf(dt.Rows(0).Item("porcDetraccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("porcDetraccion"))
            d.Medida = IIf(dt.Rows(0).Item("medida") Is DBNull.Value, Nothing, dt.Rows(0).Item("medida"))
            d.AR_CANNO = IIf(dt.Rows(0).Item("aR_CANNO") Is DBNull.Value, Nothing, dt.Rows(0).Item("aR_CANNO"))
            d.Grosor = IIf(dt.Rows(0).Item("grosor") Is DBNull.Value, Nothing, dt.Rows(0).Item("grosor"))
            d.RutaImagen = IIf(dt.Rows(0).Item("rutaImagen") Is DBNull.Value, Nothing, dt.Rows(0).Item("rutaImagen"))
            d.AR_CFECABC = IIf(dt.Rows(0).Item("aR_CFECABC") Is DBNull.Value, Nothing, dt.Rows(0).Item("aR_CFECABC"))
            d.LongSerie = IIf(dt.Rows(0).Item("longSerie") Is DBNull.Value, Nothing, dt.Rows(0).Item("longSerie"))
            d.swCelular = IIf(dt.Rows(0).Item("swCelular") Is DBNull.Value, Nothing, dt.Rows(0).Item("swCelular"))
            d.LongCelular = IIf(dt.Rows(0).Item("longCelular") Is DBNull.Value, Nothing, dt.Rows(0).Item("longCelular"))
            d.IdMarca = IIf(dt.Rows(0).Item("idMarca") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMarca"))
            d.Interior = IIf(dt.Rows(0).Item("interior") Is DBNull.Value, Nothing, dt.Rows(0).Item("interior"))
            d.Exterior = IIf(dt.Rows(0).Item("exterior") Is DBNull.Value, Nothing, dt.Rows(0).Item("exterior"))
            d.MargenUtilidad3 = IIf(dt.Rows(0).Item("margenUtilidad3") Is DBNull.Value, Nothing, dt.Rows(0).Item("margenUtilidad3"))
            d.PrecioMinimo = IIf(dt.Rows(0).Item("precioMinimo") Is DBNull.Value, Nothing, dt.Rows(0).Item("precioMinimo"))
            d.ChkPercepcion = IIf(dt.Rows(0).Item("chkPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("chkPercepcion"))
            d.TasaPercepcion = IIf(dt.Rows(0).Item("tasaPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("tasaPercepcion"))
            d.FotoArt = IIf(dt.Rows(0).Item("fotoArt") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt"))
            d.TipoPrecio = IIf(dt.Rows(0).Item("tipoPrecio") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoPrecio"))
            d.FotoArt1 = IIf(dt.Rows(0).Item("fotoArt1") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt1"))
            d.FotoArt2 = IIf(dt.Rows(0).Item("fotoArt2") Is DBNull.Value, Nothing, dt.Rows(0).Item("fotoArt2"))
            d.Observacion1 = IIf(dt.Rows(0).Item("observacion1") Is DBNull.Value, Nothing, dt.Rows(0).Item("observacion1"))
            d.procedencia = IIf(dt.Rows(0).Item("procedencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("procedencia"))
        Else
            d.IdArticulo = Nothing
            d.Descripcion1 = Nothing
            d.Descripcion2 = Nothing
            d.IdArticulo2 = Nothing
            d.Unidad = Nothing
            d.IdCuentaContable = Nothing
            d.Precio1 = Nothing
            d.Precio2 = Nothing
            d.Precio3 = Nothing
            d.Precio4 = Nothing
            d.Precio5 = Nothing
            d.Precio6 = Nothing
            d.MonedaVenta = Nothing
            d.IGV = Nothing
            d.ISC = Nothing
            d.TipoArticulo = Nothing
            d.ControlRotable = Nothing
            d.TipoDescuento = Nothing
            d.Descuento = Nothing
            d.Descuento2 = Nothing
            d.PorcentajeDistribuidor = Nothing
            d.PorcentajeComision = Nothing
            d.IdGrupo = Nothing
            d.IdFamilia = Nothing
            d.IdModelo = Nothing
            d.IdLinea = Nothing
            d.Peso = Nothing
            d.Volumen = Nothing
            d.Area = Nothing
            d.Factor = Nothing
            d.Ancho = Nothing
            d.Largo = Nothing
            d.MonedaCosteo = Nothing
            d.PrecioCosto = Nothing
            d.FechaCosto = Nothing
            d.IdMonedaCompra = Nothing
            d.PrecioCompra = Nothing
            d.FechaCompra = Nothing
            d.IdProveedor = Nothing
            d.MonedaFOB = Nothing
            d.PrecioFOB = Nothing
            d.MargenUtilidad1 = Nothing
            d.MargenUtilidad2 = Nothing
            d.ClaseArticulo = Nothing
            d.PartidaArancelaria = Nothing
            d.TecnicaRotable = Nothing
            d.CatalogoRotable = Nothing
            d.CategoriaArancelaria = Nothing
            d.swSinCosteo = Nothing
            d.Observacion = Nothing
            d.UnidadReferencia = Nothing
            d.FactorReferencia = Nothing
            d.swReferencia = Nothing
            d.swControlStock = Nothing
            d.swDecimal = Nothing
            d.swPrecioLibre = Nothing
            d.swDescuentoImporte = Nothing
            d.swSerie = Nothing
            d.swLote = Nothing
            d.swArticuloRotable = Nothing
            d.UsuarioCrea = Nothing
            d.UsuarioMod = Nothing
            d.Estado = Nothing
            d.FechaCrea = Nothing
            d.FechaMod = Nothing
            d.IdArticulo3 = Nothing
            d.PorcDetraccion = Nothing
            d.Medida = Nothing
            d.AR_CANNO = Nothing
            d.Grosor = Nothing
            d.RutaImagen = Nothing
            d.AR_CFECABC = Nothing
            d.LongSerie = Nothing
            d.swCelular = Nothing
            d.LongCelular = Nothing
            d.IdMarca = Nothing
            d.Interior = Nothing
            d.Exterior = Nothing
            d.MargenUtilidad3 = Nothing
            d.PrecioMinimo = Nothing
            d.ChkPercepcion = Nothing
            d.TasaPercepcion = Nothing
            d.FotoArt = Nothing
            d.TipoPrecio = Nothing
            d.FotoArt1 = Nothing
            d.FotoArt2 = Nothing
            d.Observacion1 = Nothing
            d.procedencia = Nothing
        End If
        Return d
    End Function

    ''' <summary>
    ''' Retorna un registo en forma de datatable
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>
    Public Function itemRow(a As NArticulo) As DataTable
        Dim dt As New DataTable
        Dim valores() As Object = {a.IdArticulo}
        Dim campos() As Object = {"@IdArticulo"}
        Dim tipodatos() As Object = {SqlDbType.Char}
        dt = sql.ProcedureSQL("Str_Articulo_S", campos, valores, tipodatos, 1).Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' Obtiene el codigo de articulo ingresando la descripcion del articulo
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    Public Function CodigoArticulo(s As String) As String
        Dim txt As String = " select Idarticulo from articulo where IdArticulo='" & s & "' or Descripcion1='" & s & "'"
        Dim dt As New DataTable
        Dim idcliente As String = ""
        dt = sql.EjecutarConsulta("d", txt).Tables(0)
        If dt.Rows.Count > 0 Then
            idcliente = dt.Rows(0).Item("Idarticulo")
        End If
        Return idcliente.Trim
    End Function
    Public Function lista_ArticulosStock(idalmacen As String) As DataTable
        Dim cadena As String = " SELECt a.idarticulo,a.descripcion1,a.unidad,stockdisponible,isnull(swcontrolstock,'N') as swcontrolStock  "
        cadena += " FROM  Articulo AS a left join stock as b on a.idarticulo=b.idarticulo where isnull(IdAlmacen,'" & idalmacen & "')='" & idalmacen & "'"
        Dim dt As DataTable = sql.EjecutarConsulta("art", cadena).Tables(0)
        Return dt
    End Function

    Public Function lista_ArticulosStock(idalmacen As String, opcion As Integer, filtro As String) As DataTable
        Dim cadena As String = " SELECt a.idarticulo,a.descripcion1,a.descripcion2,a.unidad,stockdisponible,isnull(swcontrolstock,'N') as swcontrolStock  "
        cadena += " FROM  Articulo AS a left join stock as b on a.idarticulo=b.idarticulo where isnull(IdAlmacen,'" & idalmacen & "')='" & idalmacen & "'"
        If opcion = 1 Then
            cadena += " and idmarca='" & filtro & "'"
        End If
        If opcion = 2 Then
            cadena += " and idfamilia='" & filtro & "'"
        End If
        If opcion = 3 Then
            cadena += " and idgrupo='" & filtro & "'"
        End If
        cadena += " order by a.descripcion1 "
        Dim dt As DataTable = sql.EjecutarConsulta("art", cadena).Tables(0)
        Return dt
    End Function

    Public Function TieneCodigoSunat(d As DataTable) As Boolean
        Dim articu As New NArticulo
        Dim issunat As Boolean = True
        Dim tg As New NTablaGeneral

        Dim tgsunat As New NTablaGeneral
        tgsunat.IdCodigo = "Csunat"
        tgsunat.IdGeneral = "CPE"
        tgsunat = tgsunat.Registro(tgsunat)
        If IsNothing(tgsunat.Descripcion) = True Then
            issunat = True
            Return issunat
            Exit Function
        Else
            If tgsunat.Descripcion = "False" Then
                issunat = True
                Return issunat
                Exit Function
            End If
        End If

        For Each r As DataRow In d.Rows
            articu.IdArticulo = r.Item("IdArticulo").ToString.Trim
            articu = articu.Registro(articu)
            If IsNothing(articu.IdLinea) = False Then
                If articu.IdLinea.Trim.Length = 0 Then
                    'MessageBox.Show("el articulo : " + articu.Descripcion1 + " no tiene codigo de sunat")
                    issunat = False
                    Return issunat
                    Exit Function
                End If
            Else

                'MessageBox.Show("el articulo : " + articu.Descripcion1 + " no tiene codigo de sunat")
                issunat = False
                Return issunat
                Exit Function

            End If
        Next
        Return issunat


    End Function


    Public Function ListaSimple() As DataTable
        Dim dt As New DataTable
        Dim valores() As Object = {DBNull.Value}
        Dim campos() As Object = {"@IdArticulo"}
        Dim tipodatos() As Object = {SqlDbType.Char}
        dt = sql.ProcedureSQL("Str_ArticuloSimple_S", campos, valores, tipodatos, 1).Tables(0)
        Return dt
    End Function

    Public Function ListaDetalleImg(idartiulo As String, Optional item As String = Nothing) As DataTable
        Dim dt As New DataTable

        Dim campos() As Object = {"@IdArticulo", "@item"}
        Dim tipodatos() As Object = {SqlDbType.VarChar, SqlDbType.Int}
        Dim valores() As Object = {idartiulo, item}
        dt = sql.ProcedureSQL("Str_ArticuloDetalleImg_S", campos, valores, tipodatos, 2).Tables(0)
        Return dt
    End Function
    Public Function Lista_articulos_precios() As DataTable
        Dim dt As New DataTable
        dt = sql.FuncionProc("Str_producto_precios").Tables(0)
        Return dt
    End Function
    Public Function ConversionUnidadDestino(IdArticulo As String, unidaddestino As String) As DataTable
        Dim ca As String = " select idventa,unidadDestino,medida from tbl_Articulo_UnidadVenta a " +
                "inner join unidadequivalenxarticulo u "
        ca += " on a.IdVenta =u.ID where unidadDestino = '" + unidaddestino + "'  and a.idarticulo='" + IdArticulo + "' "
        Return sql.EjecutarConsulta("D", ca).Tables(0)
    End Function
    Public Function ConversionOrigenDestino(unidadorigen As String, unidaddestino As String) As DataTable
        Dim dt As DataTable
        Dim ca As String = " select id, unidadorigen, unidaddestino, factorconversion, Medida
                            from unidadequivalenxarticulo 
                            where unidadDestino = '" + unidaddestino + "' and unidadorigen = '" + unidadorigen + "'"
        dt = sql.EjecutarConsulta("D", ca).Tables(0)
        Return dt
    End Function
#End Region

End Class
