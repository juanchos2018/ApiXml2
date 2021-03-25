Imports CapaDatos
Public Class NComprobante
    Dim sql As New ClsConexion
#Region "Declarations"
    Public Property idagencia As String
    Public Property idtipodocumento As String
    Public Property serie As String
    Public Property numerodocumento As String
    Public Property numeropedido As String
    Public Property fechadocumento As System.DateTime
    Public Property fechavencimineto As System.DateTime
    Public Property debehaber As String
    Public Property idvendedor As String
    Public Property idcaja As String
    Public Property idcliente As String
    Public Property nombrecliente As String
    Public Property direccion As String
    Public Property ruc As String
    Public Property idalmacen As String
    Public Property idformaventa As String
    Public Property idmoneda As String
    Public Property tipocambio As Decimal
    Public Property importetotal As Decimal
    Public Property importeigv As Decimal
    Public Property saldo As Decimal
    Public Property importedescuento As Decimal
    Public Property numeroorden As String
    Public Property idtipodocumento1 As String
    Public Property serie1 As String
    Public Property numerodocumento1 As String
    Public Property descripcion As String
    Public Property estado As String
    Public Property facturaguia As String
    Public Property idtransportista As String
    Public Property idcentrocosto As String
    Public Property idmaquina As String
    Public Property destino As String
    Public Property idtipofactura As String
    Public Property idtipoanexo As String
    Public Property idanexo As String
    Public Property descuneto1 As Decimal
    Public Property descuento2 As Decimal
    Public Property flete As Decimal
    Public Property embalaje As Decimal
    Public Property tasa As Decimal
    Public Property idusuariooperador As String
    Public Property idusuariosectorista As String
    Public Property idcadena As String
    Public Property idinternocadena As String
    Public Property idautorizacion As String
    Public Property reparto As String
    Public Property usuariocrea As String
    Public Property fechacrea As System.DateTime
    Public Property usuariomod As String
    Public Property fechamod As System.DateTime
    Public Property idtiponotacredito As String
    Public Property linea As String
    Public Property impreso As String
    Public Property anuladonc As String
    Public Property idvendedor1 As String
    Public Property igv As Decimal
    Public Property idchofer As String
    Public Property idzonaventa As String
    Public Property idtipodocumento2 As String
    Public Property numerodocumento2 As String
    Public Property idsubdiario As String
    Public Property nrocontable As String
    Public Property importetotalus As Decimal
    Public Property importetotalmn As Decimal
    Public Property importeigvus As Decimal
    Public Property importeigvmn As Decimal
    Public Property estadosunat As String
    Public Property codigohas As String
    Public Property barrapdf417 As Byte()
    Public Property signaturevalue As String
    Public Property tipooperacion As String
    Public Property tipoafecigv As String
    Public Property islote As String
    Public Property idturno As String
    Public Property fechadocumento2 As System.DateTime
    Public Property importerefencia As Decimal
    Public Property valorventa As Decimal
    Public Property imagedocumento As Byte()
    Public Property isliq As Boolean
    Public Property tipo_preventa As String
    Public Property nro_preventa As String
    Public Property ispercepcion As Boolean
    Public Property percepcion As Decimal
    Public Property chkbancarizar As Boolean
    Public Property importeexo As Decimal
    Public Property importeinf As Decimal
    Public Property importegrav As Decimal
    Public Property importegrat As Decimal
    Public Property importeexous As Decimal
    Public Property importeexomn As Decimal
    Public Property importeinfus As Decimal
    Public Property importeinfmn As Decimal
    Public Property importegravus As Decimal
    Public Property importegravmn As Decimal
    Public Property importegratus As Decimal
    Public Property importegratmn As Decimal
    Public Property Detalle As New List(Of NDetalleComprobante)()


#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

#End Region


#Region "Metodos"
    Public Sub add(c As NComprobante)
        Dim params() As Object = {
            "@IdAgencia", "@IdTipoDocumento", "@Serie", "@NumeroDocumento", "@NumeroPedido", "@FechaDocumento",
            "@FechaVencimineto", "@DebeHaber", "@IdVendedor", "@IdCaja", "@IdCliente", "@NombreCliente",
            "@Direccion", "@RUC", "@IdAlmacen", "@IdFormaVenta", "@IdMoneda", "@TipoCambio", "@ImporteTotal",
            "@ImporteIGV", "@Saldo", "@ImporteDescuento", "@NumeroOrden", "@IdTipoDocumento1", "@Serie1",
            "@NumeroDocumento1", "@Descripcion", "@Estado", "@FacturaGuia", "@IdTransportista", "@IdCentroCosto",
             "@IdMaquina", "@Destino", "@IdTipoFactura", "@IdTipoAnexo", "@IdAnexo", "@Descuneto1", "@Descuento2",
        "@Flete", "@Embalaje", "@Tasa", "@IdUsuarioOperador", "@IdUsuarioSectorista", "@IdCadena",
        "@IdInternoCadena", "@IdAutorizacion", "@Reparto", "@UsuarioCrea", "@FechaCrea", "@UsuarioMod",
                "@FechaMod", "@IdTipoNotaCredito", "@Linea", "@Impreso", "@AnuladoNC", "@IdVendedor1", "@IGV",
        "@Idchofer", "@IdZonaVenta", "@IdTipoDocumento2", "@NumeroDocumento2", "@EstadoSunat", "@TipoOperacion",
           "@TipoAfecIGV", "@IsLote", "@IdTurno", "@FechaDocumento2", "@ImporteRefencia", "@Tipo_PreVenta", "@Nro_PreVenta",
           "@Ispercepcion", "@percepcion"}
        Dim tipoParametro() As Object = {
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime,
            SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal,
            SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
           SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal,
        SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
            SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar,
            SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Decimal,
         SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar,
          SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar,
          SqlDbType.Bit, SqlDbType.Decimal}

        Dim vals() As Object = {
            c.IdAgencia, c.IdTipoDocumento, c.Serie, c.NumeroDocumento, c.NumeroPedido, c.FechaDocumento,
            c.FechaVencimineto, c.DebeHaber, c.IdVendedor, c.IdCaja, c.IdCliente, c.NombreCliente,
            c.Direccion, c.RUC, c.IdAlmacen, c.IdFormaVenta, c.IdMoneda, c.TipoCambio, c.ImporteTotal,
            c.ImporteIGV, c.Saldo, c.ImporteDescuento, c.NumeroOrden, c.IdTipoDocumento1, c.Serie1,
            c.NumeroDocumento1, c.Descripcion, c.Estado, c.FacturaGuia, c.IdTransportista, c.IdCentroCosto,
            c.IdMaquina, c.Destino, c.IdTipoFactura, c.IdTipoAnexo, c.IdAnexo, c.Descuneto1, c.Descuento2,
            c.Flete, c.Embalaje, c.Tasa, c.IdUsuarioOperador, c.IdUsuarioSectorista, c.IdCadena,
            c.IdInternoCadena, c.IdAutorizacion, c.Reparto, c.UsuarioCrea, c.FechaCrea, c.UsuarioMod,
            c.FechaMod, c.IdTipoNotaCredito, c.Linea, c.Impreso, c.AnuladoNC, c.IdVendedor1, c.IGV,
            c.Idchofer, c.IdZonaVenta, c.IdTipoDocumento2, c.NumeroDocumento2, c.EstadoSunat, c.TipoOperacion,
            c.TipoAfecIGV, c.IsLote, c.IdTurno, c.FechaDocumento2, c.ImporteRefencia, c.Tipo_PreVenta, c.Nro_PreVenta,
            c.Ispercepcion, c.Percepcion}
        sql.EjecutarProcedure("Str_AddComprobante", params, vals, tipoParametro, 72)
    End Sub

    Public Function Existe(p As NComprobante) As Boolean
        Dim existeC As String
        Dim bandera As Boolean = False
        Dim valoresC() As Object = {"'" & p.IdAgencia & "'", "'" & p.IdTipoDocumento & "'", "'" & Trim(p.Serie) & "'", "'" & Trim(p.NumeroDocumento) & "'"}
        existeC = sql.ValorEscalar("dbo.venta_Existe", valoresC, 4)
        If existeC = "1" Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function
    Public Function cabeceraCPE(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento", "@IdCliente"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.IdAgencia, p.IdAlmacen, p.IdTipoDocumento, p.Serie, p.NumeroDocumento, p.IdCliente}
        Return sql.ProcedureSQL("Str_FndComprobante", parametros, valores, tipoparametros, 6).Tables(0)
    End Function
    Public Function cabeceraCPE21(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndComprobante21", parametros, valores, tipoparametros, 3).Tables(0)
    End Function
    Public Function cabeceraCPE21cpe(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndComprobante21cpe", parametros, valores, tipoparametros, 3).Tables(0)
    End Function
    Public Function cabeceraPDF(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.IdAgencia, p.IdAlmacen, p.IdTipoDocumento, p.Serie, p.NumeroDocumento}
        Return sql.ProcedureSQL("Str_FndComprobante_pdf", parametros, valores, tipoparametros, 5).Tables(0)
    End Function
    Public Function cabeceraPDFCPE(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idagencia, p.idalmacen, p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndComprobante_pdfCPE", parametros, valores, tipoparametros, 5).Tables(0)
    End Function
    Public Function cabeceraPDFSerie(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idagencia, p.idalmacen, p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndComprobante_pdf_01", parametros, valores, tipoparametros, 5).Tables(0)
    End Function
    Public Function cabeceraPDFSerieCPE(p As NComprobante) As DataTable
        Dim parametros() As Object = {"@IdAgencia", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@NumeroDocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {p.idagencia, p.idalmacen, p.idtipodocumento, p.serie, p.numerodocumento}
        Return sql.ProcedureSQL("Str_FndComprobante_pdf_01CPE", parametros, valores, tipoparametros, 5).Tables(0)
    End Function
    Public Function Venta_Asientos(di As Date, df As Date, idalmacen As String, idtipodocumento As String, serie As String, bandera As Boolean) As DataTable
        Dim parametros() As Object = {"@FechaI", "@FechaF", "@IdAlmacen", "@IdTipoDocumento", "@Serie", "@IsTodos"}
        Dim tipoparametros() As Object = {SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Bit}
        Dim valores() As Object = {di, df, idalmacen, idtipodocumento, serie, bandera}
        Return sql.ProcedureSQL("Str_Venta_Asientos", parametros, valores, tipoparametros, 6).Tables(0)
    End Function

    Public Function Comprobate_ticket(idtipodocumento As String, serie As String, Numerodocumento As String) As DataSet
        Dim sParametro As Object() = {"@IdTipoDocumento", "@Serie", "@numerodocumento"}
        Dim typeParam As Object() = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim vParametro As Object() = {idtipodocumento, serie, Numerodocumento}
        Return Me.sql.ProcedureSQL("Str_Comprobante_ticket", sParametro, vParametro, typeParam, 3)
    End Function

    Public Function Lista(p As NComprobante) As DataTable
        Dim cadena As String = "select distinct p.IdTipoDocumento,p.Serie,p.NumeroDocumento,p.FechaDocumento,p.IdCliente,p.Nombrecliente,p.importetotal,p.IdAlmacen,d.IdAgencia from Comprobante p     "
        cadena += " inner join detallecomprobante d on p.idalmacen=d.idalmacen and p.idtipodocumento=d.idtipodocumento and p.serie=d.serie and p.numerodocumento=d.numerodocumento "
        cadena += "where isnull(p.Estado,'V')='V' and saldoentrega<>0 and p.idAlmacen='" & p.IdAlmacen & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("compro", cadena).Tables(0)
        Return dt
    End Function
    Public Function ListaBuscar(d As NComprobante) As DataTable
        Dim cadena As String = "
                DECLARE @idtipodocumento1 VARCHAR(2)
                SET @idtipodocumento1 = '" + d.idtipodocumento1 + "'
                select 
                  [IdTipoDocumento] ,[Serie],[NumeroDocumento],[FechaDocumento] ,[IdCliente]
                  ,[NombreCliente],[Direccion], [RUC] ,[IdAlmacen] ,[NumeroPedido]
                  ,[ImporteTotal] ,[ImporteIGV] ,[IdTipoDocumento1] ,[Serie1]
                  ,[NumeroDocumento1] ,[Descripcion] ,[Estado] ,[IGV] ,[ImporteTotalUS] 
                  ,[ImporteTotalMN] ,[ImporteIGVUS] ,[ImporteIGVMN] ,[EstadoSunat],[ValorVenta]
                from comprobante 
                where (idtipodocumento1=@idtipodocumento1 or @idtipodocumento1 is null or @idtipodocumento1='')
                order by fechadocumento desc"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("comprobante", cadena).Tables(0)
        Return dt
    End Function
    Public Function Lista(p As NComprobante, tipofactura As String) As DataTable
        Dim cadena As String = "select distinct p.IdTipoDocumento,p.Serie,p.NumeroDocumento,p.FechaDocumento,p.IdCliente,p.Nombrecliente,p.importetotal,p.IdAlmacen,d.IdAgencia from Comprobante p     "
        cadena += " inner join detallecomprobante d on p.idalmacen=d.idalmacen and p.idtipodocumento=d.idtipodocumento and p.serie=d.serie and p.numerodocumento=d.numerodocumento "
        cadena += "where isnull(p.Estado,'V')='V' and saldoentrega<>0 and p.idAlmacen='" & p.IdAlmacen & "' and IdTipoFactura='" & tipofactura & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("compro", cadena).Tables(0)
        Return dt
    End Function
    Public Function Lista_pendientes(i As DateTime, f As DateTime) As DataTable
        Dim parametros() As Object = {"@fechaI", "@fechaF"}
        Dim tipoparametros() As Object = {SqlDbType.DateTime, SqlDbType.DateTime}
        Dim valores() As Object = {i, f}
        Return sql.ProcedureSQL("Str_Lista_pendiente", parametros, valores, tipoparametros, 2).Tables(0)
    End Function

    Public Function Lista(tipofactura As String) As DataTable
        Dim cadena As String = "select distinct p.IdTipoDocumento,p.Serie,p.NumeroDocumento,p.FechaDocumento,p.IdCliente,p.Nombrecliente,p.importetotal,p.IdAlmacen,d.IdAgencia from Comprobante p     "
        cadena += " inner join detallecomprobante d on p.idalmacen=d.idalmacen and p.idtipodocumento=d.idtipodocumento and p.serie=d.serie and p.numerodocumento=d.numerodocumento "
        cadena += "where isnull(p.Estado,'V')='V' and saldoentrega<>0 and IdTipoFactura='" & tipofactura & "'"
        Dim dt As New DataTable
        dt = sql.EjecutarConsulta("compro", cadena).Tables(0)
        Return dt
    End Function
    Public Function ObtenerCabecera(p As NComprobante) As NComprobante
        Dim dt_cab As New DataTable
        Dim cadena As String = " SELECT  IdAgencia, IdTipoDocumento, Serie, NumeroDocumento, NumeroPedido, FechaDocumento, FechaVencimineto, DebeHaber, IdVendedor, IdCaja, IdCliente,  "
        cadena += " NombreCliente, Direccion, RUC, IdAlmacen, IdFormaVenta, IdMoneda, TipoCambio, ImporteTotal, ImporteIGV, Saldo, ImporteDescuento, ISNULL(NumeroOrden,'')AS NumeroOrden,  "
        cadena += " IdTipoDocumento1, Serie1, NumeroDocumento1, Descripcion, Estado, FacturaGuia, IdTransportista, IdCentroCosto, IdMaquina, Destino, IdTipoFactura, IdTipoAnexo, "
        cadena += " IdAnexo, Descuneto1, Descuento2, Flete, Embalaje, Tasa, IdUsuarioOperador, IdUsuarioSectorista, IdCadena, IdInternoCadena, IdAutorizacion, Reparto, UsuarioCrea, "
        cadena += " FechaCrea, UsuarioMod, FechaMod, IdTipoNotaCredito, Linea, Impreso, AnuladoNC, IdVendedor1, IGV, Idchofer, IdZonaVenta,Tipo_PreVenta,Nro_PreVenta "
        cadena += "  ,EstadoSunat,tipooperacion,tipoafecigv,isnull(idSubdiario,'')as IdSubdiario,isnull(NroContable,'') as NroContable FROM Comprobante "
        cadena += " where Serie='" & p.Serie & "' and IdTipoDocumento='" & p.IdTipoDocumento & "' and NumeroDocumento='" & p.NumeroDocumento & "' "
        dt_cab = sql.EjecutarConsulta("cab", cadena).Tables(0)
        If dt_cab.Rows.Count > 0 Then
            With dt_cab
                p.IdAgencia = .Rows(0).Item("IdAgencia").ToString
                p.IdVendedor = .Rows(0).Item("IdVendedor").ToString
                p.IdCliente = .Rows(0).Item("IdCliente").ToString
                p.NombreCliente = .Rows(0).Item("NombreCliente").ToString
                p.Direccion = .Rows(0).Item("Direccion").ToString
                p.RUC = .Rows(0).Item("RUC").ToString
                p.IdFormaVenta = .Rows(0).Item("IdFormaVenta").ToString
                p.IdMoneda = .Rows(0).Item("IdMoneda")
                p.TipoCambio = .Rows(0).Item("TipoCambio")
                p.ImporteTotal = .Rows(0).Item("ImporteTotal")
                p.ImporteIGV = .Rows(0).Item("ImporteIGV")
                p.Descripcion = .Rows(0).Item("Descripcion").ToString
                p.IGV = .Rows(0).Item("IGV")
                p.Idchofer = .Rows(0).Item("IdChofer").ToString
                p.NumeroOrden = .Rows(0).Item("NumeroOrden").ToString
                p.FechaDocumento = .Rows(0).Item("FechaDocumento").ToString
                p.IdAlmacen = .Rows(0).Item("IdAlmacen").ToString
                p.Tipo_PreVenta = .Rows(0).Item("Tipo_PreVenta").ToString
                p.Nro_PreVenta = .Rows(0).Item("Nro_PreVenta").ToString
                p.IdTipoFactura = .Rows(0).Item("IdTipoFactura").ToString
                p.estadosunat = .Rows(0).Item("EstadoSunat").ToString
                p.tipoafecigv = .Rows(0).Item("tipoafecigv").ToString
                p.tipooperacion = .Rows(0).Item("tipooperacion").ToString
                p.idsubdiario = .Rows(0).Item("IdSubdiario").ToString
                p.nrocontable = .Rows(0).Item("nrocontable").ToString
            End With
        End If
        Return p
    End Function
    ''' <summary>
    ''' Lista los comprobantes por Almacen
    ''' </summary>
    ''' <param name="idalmacen"></param>
    ''' <returns></returns>
    Public Function documentos(idalmacen As String) As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,IdCliente,NombreCliente,ImporteTotal,rtrim(NumeroOrden) as NumeroOrden from comprobante "
        cadena += " where isnull(Estado,'V')='V' and IdTipoDocumento IN('BV','FT','NV') and idalmacen='" & idalmacen & "' and  isnull(NumeroPedido,'')=''"
        Dim dt As DataTable = sql.EjecutarConsulta("ca", cadena).Tables(0)
        Return dt
    End Function
    Public Function documentos_aduana(idalmacen As String) As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,IdCliente,NombreCliente,Idmoneda,ImporteTotal,ImporteRefencia as Saldo,f.descripcion as Condicion, rtrim(NumeroOrden) as NumeroOrden  from comprobante c inner join formaventa f on c.idformaventa=f.idformaventa "
        cadena += " where isnull(Estado,'V')='V'     and idalmacen='" & idalmacen & "' and  isnull(NumeroPedido,'')='' "
        Dim dt As DataTable = sql.EjecutarConsulta("ca", cadena).Tables(0)
        Return dt
    End Function
    Public Function ListaDeTipo1(TipoDocumento As String, idalmacen As String) As DataTable
        Dim parametros() As Object = {"@idtipodocumento1", "@idalmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {TipoDocumento, idalmacen}
        Return sql.ProcedureSQL("Str_ComprobanteListaTipo1_S", parametros, valores, tipoParametro, 2).Tables(0)
    End Function
    ''' <summary>
    ''' Lista los comprobantes por almacen y serie
    ''' </summary>
    ''' <param name="idalmacen"></param>
    ''' <param name="serie"></param>
    ''' <returns></returns>
    Public Function documentos(idalmacen As String, serie As String) As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,IdCliente,NombreCliente,ImporteTotal from comprobante "
        cadena += " where isnull(Estado,'V')='V' and IdTipoDocumento IN('BV','FT') and idalmacen='" & idalmacen & "' and Serie='" & serie & "' and isnull(NumeroPedido,'')=''"
        Dim dt As DataTable = sql.EjecutarConsulta("ca", cadena).Tables(0)
        Return dt
    End Function
    ''' <summary>
    ''' Lista los comprobantes por almacen serie y tipo de documento
    ''' </summary>
    ''' <param name="idalmacen"></param>
    ''' <param name="serie"></param>
    ''' <param name="tipodocumento"></param>
    ''' <returns></returns>
    Public Function documentos(idalmacen As String, serie As String, tipodocumento As String) As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,IdCliente,NombreCliente,ImporteTotal from comprobante "
        cadena += " where isnull(Estado,'V')='V' and IdTipoDocumento='" & tipodocumento & "' and idalmacen='" & idalmacen & "' and Serie='" & serie & "' and isnull(NumeroPedido,'')=''"
        Dim dt As DataTable = sql.EjecutarConsulta("ca", cadena).Tables(0)
        Return dt
    End Function
    Public Function documentosGuias(idalmacen As String) As DataTable
        Dim cadena As String = " select IdAlmacen,IdTipoDocumento,Serie,NumeroDocumento,FechaDocumento,IdCliente,NombreCliente,ImporteTotal from comprobante "
        cadena += " where isnull(Estado,'V')='V' and idtipodocumento1='GR' 
                    and idalmacen='" & idalmacen & "'  and isnull(NumeroPedido,'')=''"
        Dim dt As DataTable = sql.EjecutarConsulta("ca", cadena).Tables(0)
        Return dt
    End Function
    Public Sub Eliminar(d As NComprobante)
        sql.Eliminar_Items("Comprobante", "IdAlmacen='" & d.IdAlmacen & "' and Serie='" & d.Serie & "' and IdTipoDocumento='" & d.IdTipoDocumento & "' and NumeroDocumento='" & d.NumeroDocumento & "'")
    End Sub
    Public Function existe_movi(d As NComprobante) As String
        Dim nro As String = ""
        Dim dt As New DataTable
        Dim parametros() As Object = {"@TipoDocumento", "@IdCliente", "@IdTipoDocumento", "@Numerodocumento"}
        Dim tipoparametros() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {"PS", d.idcliente, d.idtipodocumento, d.serie & d.numerodocumento}
        dt = sql.ProcedureSQL("Str_Documentos_relacionados", parametros, valores, tipoparametros, 4).Tables(0)
        If dt.Rows.Count > 0 Then
            nro = dt.Rows(0).Item(0).ToString
        End If
        Return nro
    End Function
    Public Function Pendiente_Despacho(ff As DateTime, Optional idalmacen As String = "", Optional InArticulo As String = "", Optional inCliente As String = "", Optional archivados As Boolean = False) As DataTable
        Dim cadena As String = " select c.FechaDocumento,c.idcliente,c.nombrecliente,d.IdAlmacen,d.IdTipoDocumento,d.Serie,d.NumeroDocumento,d.idarticulo,d.descripcion, "
        cadena += " d.unidad,d.Cantidad,MovSaldo.Cantidad as  SaldoEntrega,c.Idmoneda,PrecioVenta,PrecioSIGV as ValorVenta,d.ImporteIGV,(d.PrecioSIGV+d.ImporteIGV)as ImporteTotal,a.descripcion as Almacen,d.importemn as Total,d.SaldoEntrega*d.PrecioVenta as SaldoTotal from comprobante c "
        cadena += " inner join detallecomprobante d on c.idtipodocumento=d.idtipodocumento and  "
        cadena += " c.serie=d.serie and c.numerodocumento=d.numerodocumento inner join almacen a on d.idalmacen=a.idalmacen "
        cadena += " left join (select id,idarticulo,sum(cantidad) as Cantidad from VMovimientoPendientes where FechaDocumento<='" & ff & "' group by id,idarticulo) as MovSaldo "
        cadena += " on rtrim(c.IdTipoDocumento)+rtrim(c.Serie)+rtrim(c.NumeroDocumento)=MovSaldo.ID and d.IdArticulo=MovSaldo.idarticulo "
        cadena += " where isnull(idtipoFactura,'') in('03','05') and c.estado='V' and MovSaldo.Cantidad<>0 and FechaDocumento<='" & ff & "'"
        If idalmacen.Length > 0 Then
            cadena += " and c.IdAlmacen ='" & idalmacen & "'"
        End If
        If InArticulo.Length > 0 Then
            cadena += " and d.idarticulo in(" & InArticulo & ") "
        End If
        If inCliente.Length > 0 Then
            cadena += " and c.idCliente in(" & inCliente & ") "
        End If
        If archivados = False Then
            cadena += " and isnull(c.reparto,'')<>1 "
        End If
        Return sql.EjecutarConsulta("tabla", cadena).Tables(0)
    End Function
    Public Function Entregas_Realizadas(fi As DateTime, ff As DateTime, Optional idalmacen As String = "", Optional InArticulo As String = "", Optional inCliente As String = "") As DataTable
        Dim cadena As String = " select unidad,m.FechaDocumento,m.Idcliente,m.NombreCliente,m.TipoMovimiento,d.IdAlmacen,d.TipoDocumento,TipoDocumento2,NumeroDocumento2,d.NumeroDocumento,d.codigomovimiento,d.idarticulo,d.descripcion,d.cantidad,d.cantidadFacturar,d.saldo,TIPODOCUMENTO3,NUMERODOCUMENTO3,al.Descripcion as Almacen from movimiento m "
        cadena += " inner join detallemovimiento d on m.idalmacen=d.idalmacen and m.tipodocumento=d.tipodocumento and m.numerodocumento=d.NumeroDocumento "
        cadena += " inner join articulo ar on d.idarticulo=ar.idarticulo inner join almacen al on d.idalmacen=al.idalmacen "
        cadena += " where d.TipoDocumento='PS' and ISNULL(m.situacion,'V')='V' and m.idmovimiento in('S1','EP') and m.FechaDocumento between '" & fi & "' and '" & ff & "'"
        If idalmacen.Length > 0 Then
            cadena += " and d.IdAlmacen ='" & idalmacen & "'"
        End If
        If InArticulo.Length > 0 Then
            cadena += " and d.idarticulo in(" & InArticulo & ") "
        End If
        If inCliente.Length > 0 Then
            cadena += " and m.idCliente in(" & inCliente & ") "
        End If
        Return sql.EjecutarConsulta("tabla", cadena).Tables(0)
    End Function
    Public Function Movimiento_Entregas(ff As DateTime, Optional idalmacen As String = "", Optional InArticulo As String = "", Optional inCliente As String = "", Optional archivado As Boolean = False) As DataTable
        Dim cadena As String = " select * from VMovimientoPendientes "
        cadena += " where FechaDocumento<='" & ff & "'"
        If idalmacen.Length > 0 Then
            cadena += " and IdAlmacen ='" & idalmacen & "'"
        End If
        If InArticulo.Length > 0 Then
            cadena += " and idarticulo in(" & InArticulo & ") "
        End If
        If inCliente.Length > 0 Then
            cadena += " and idCliente in(" & inCliente & ") "
        End If
        If archivado = False Then
            cadena += " and isnull(reparto,'')<>1 "
        End If
        cadena += " order by idcliente,ID,idarticulo,fechaDocumento,orden "

        Return sql.EjecutarConsulta("tabla", cadena).Tables(0)
    End Function

    Public Function ResumenBoleta(fecha As DateTime) As DataTable
        Dim parametros() As Object = {"@fechaDocumento"}
        Dim tipoParametro() As Object = {SqlDbType.DateTime}
        Dim valores() As Object = {fecha}
        Return sql.ProcedureSQL("Str_ResumenBoleta1_1", parametros, valores, tipoParametro, 1).Tables(0)
    End Function
    Public Function ResumenBoleta(fecha As DateTime, soloanulado As Boolean) As DataTable
        Dim parametros() As Object = {"@fechaDocumento", "@Soloanulados"}
        Dim tipoParametro() As Object = {SqlDbType.DateTime, SqlDbType.Bit}
        Dim valores() As Object = {fecha, soloanulado}
        Return sql.ProcedureSQL("Str_ResumenBoleta1_1", parametros, valores, tipoParametro, 2).Tables(0)
    End Function
    Public Sub Agregar(d As NComprobante)
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@numeropedido", "@fechadocumento", "@fechavencimineto", "@debehaber", "@idvendedor", "@idcaja", "@idcliente", "@nombrecliente", "@direccion", "@ruc", "@idalmacen", "@idformaventa", "@idmoneda", "@tipocambio", "@importetotal", "@importeigv", "@saldo", "@importedescuento", "@numeroorden", "@idtipodocumento1", "@serie1", "@numerodocumento1", "@descripcion", "@estado", "@facturaguia", "@idtransportista", "@idcentrocosto", "@idmaquina", "@destino", "@idtipofactura", "@idtipoanexo", "@idanexo", "@descuneto1", "@descuento2", "@flete", "@embalaje", "@tasa", "@idusuariooperador", "@idusuariosectorista", "@idcadena", "@idinternocadena", "@idautorizacion", "@reparto", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@idtiponotacredito", "@linea", "@impreso", "@anuladonc", "@idvendedor1", "@igv", "@idchofer", "@idzonaventa", "@idtipodocumento2", "@numerodocumento2", "@idsubdiario", "@nrocontable", "@estadosunat", "@codigohas", "@barrapdf417", "@signaturevalue", "@tipooperacion", "@tipoafecigv", "@islote", "@idturno", "@fechadocumento2", "@importerefencia", "@imagedocumento", "@isliq", "@tipo_preventa", "@nro_preventa", "@ispercepcion", "@percepcion", "@chkbancarizar", "@ImporteExo", "@ImporteInf", "@ImporteGrav", "@ImporteGrat"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarBinary, SqlDbType.Bit, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.numeropedido, d.fechadocumento, d.fechavencimineto, d.debehaber, d.idvendedor, d.idcaja, d.idcliente, d.nombrecliente, d.direccion, d.ruc, d.idalmacen, d.idformaventa, d.idmoneda, d.tipocambio, d.importetotal, d.importeigv, d.saldo, d.importedescuento, d.numeroorden, d.idtipodocumento1, d.serie1, d.numerodocumento1, d.descripcion, d.estado, d.facturaguia, d.idtransportista, d.idcentrocosto, d.idmaquina, d.destino, d.idtipofactura, d.idtipoanexo, d.idanexo, d.descuneto1, d.descuento2, d.flete, d.embalaje, d.tasa, d.idusuariooperador, d.idusuariosectorista, d.idcadena, d.idinternocadena, d.idautorizacion, d.reparto, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.idtiponotacredito, d.linea, d.impreso, d.anuladonc, d.idvendedor1, d.igv, d.idchofer, d.idzonaventa, d.idtipodocumento2, d.numerodocumento2, d.idsubdiario, d.nrocontable, d.estadosunat, d.codigohas, d.barrapdf417, d.signaturevalue, d.tipooperacion, d.tipoafecigv, d.islote, d.idturno, d.fechadocumento2, d.importerefencia, d.imagedocumento, d.isliq, d.tipo_preventa, d.nro_preventa, d.ispercepcion, d.percepcion, d.chkbancarizar, d.importeexo, d.importeinf, d.importegrav, d.importegrat}
        sql.EjecutarProcedure("Str_Comprobante_I", parametros, valores, tipoParametro, 84)
    End Sub
    Public Sub Actualizar(d As NComprobante)
        Dim parametros() As Object = {"@idagencia", "@idtipodocumento", "@serie", "@numerodocumento", "@numeropedido", "@fechadocumento", "@fechavencimineto", "@debehaber", "@idvendedor", "@idcaja", "@idcliente", "@nombrecliente", "@direccion", "@ruc", "@idalmacen", "@idformaventa", "@idmoneda", "@tipocambio", "@importetotal", "@importeigv", "@saldo", "@importedescuento", "@numeroorden", "@idtipodocumento1", "@serie1", "@numerodocumento1", "@descripcion", "@estado", "@facturaguia", "@idtransportista", "@idcentrocosto", "@idmaquina", "@destino", "@idtipofactura", "@idtipoanexo", "@idanexo", "@descuneto1", "@descuento2", "@flete", "@embalaje", "@tasa", "@idusuariooperador", "@idusuariosectorista", "@idcadena", "@idinternocadena", "@idautorizacion", "@reparto", "@usuariocrea", "@fechacrea", "@usuariomod", "@fechamod", "@idtiponotacredito", "@linea", "@impreso", "@anuladonc", "@idvendedor1", "@igv", "@idchofer", "@idzonaventa", "@idtipodocumento2", "@numerodocumento2", "@idsubdiario", "@nrocontable", "@estadosunat", "@codigohas", "@barrapdf417", "@signaturevalue", "@tipooperacion", "@tipoafecigv", "@islote", "@idturno", "@fechadocumento2", "@importerefencia", "@imagedocumento", "@isliq", "@tipo_preventa", "@nro_preventa", "@ispercepcion", "@percepcion", "@chkbancarizar", "@importeexo", "@importeinf", "@importegrav", "@importegrat"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Char, SqlDbType.Char, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarBinary, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.Decimal, SqlDbType.VarBinary, SqlDbType.Bit, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Bit, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal, SqlDbType.Decimal}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.numeropedido, d.fechadocumento, d.fechavencimineto, d.debehaber, d.idvendedor, d.idcaja, d.idcliente, d.nombrecliente, d.direccion, d.ruc, d.idalmacen, d.idformaventa, d.idmoneda, d.tipocambio, d.importetotal, d.importeigv, d.saldo, d.importedescuento, d.numeroorden, d.idtipodocumento1, d.serie1, d.numerodocumento1, d.descripcion, d.estado, d.facturaguia, d.idtransportista, d.idcentrocosto, d.idmaquina, d.destino, d.idtipofactura, d.idtipoanexo, d.idanexo, d.descuneto1, d.descuento2, d.flete, d.embalaje, d.tasa, d.idusuariooperador, d.idusuariosectorista, d.idcadena, d.idinternocadena, d.idautorizacion, d.reparto, d.usuariocrea, d.fechacrea, d.usuariomod, d.fechamod, d.idtiponotacredito, d.linea, d.impreso, d.anuladonc, d.idvendedor1, d.igv, d.idchofer, d.idzonaventa, d.idtipodocumento2, d.numerodocumento2, d.idsubdiario, d.nrocontable, d.estadosunat, d.codigohas, d.barrapdf417, d.signaturevalue, d.tipooperacion, d.tipoafecigv, d.islote, d.idturno, d.fechadocumento2, d.importerefencia, d.imagedocumento, d.isliq, d.tipo_preventa, d.nro_preventa, d.ispercepcion, d.percepcion, d.chkbancarizar, d.importeexo, d.importeinf, d.importegrav, d.importegrat}
        sql.EjecutarProcedure("Str_Comprobante_U", parametros, valores, tipoParametro, 84)
    End Sub
    Public Sub excluir(d As NComprobante)
        sql.Editar("Comprobante", "Reparto=1", "IdTipoDocumento='" & d.idtipodocumento & "' and serie='" & d.serie & "' and numerodocumento='" & d.numerodocumento & "'")
    End Sub
    Public Function Registro(d As NComprobante) As NComprobante
        Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.idalmacen}
        Dim dt As New DataTable
        dt = sql.ProcedureSQL("Str_comprobante_S", parametros, valores, tipoParametro, 5).Tables(0)
        If dt.Rows.Count > 0 Then
            d.idagencia = IIf(dt.Rows(0).Item("idAgencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAgencia"))
            d.idtipodocumento = IIf(dt.Rows(0).Item("idTipoDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento"))
            d.serie = IIf(dt.Rows(0).Item("serie") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie"))
            d.numerodocumento = IIf(dt.Rows(0).Item("numeroDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento"))
            d.numeropedido = IIf(dt.Rows(0).Item("numeroPedido") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroPedido"))
            d.fechadocumento = IIf(dt.Rows(0).Item("fechaDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaDocumento"))
            d.fechavencimineto = IIf(dt.Rows(0).Item("fechaVencimineto") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaVencimineto"))
            d.debehaber = IIf(dt.Rows(0).Item("debeHaber") Is DBNull.Value, Nothing, dt.Rows(0).Item("debeHaber"))
            d.idvendedor = IIf(dt.Rows(0).Item("idVendedor") Is DBNull.Value, Nothing, dt.Rows(0).Item("idVendedor"))
            d.idcaja = IIf(dt.Rows(0).Item("idCaja") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCaja"))
            d.idcliente = IIf(dt.Rows(0).Item("idCliente") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCliente"))
            d.nombrecliente = IIf(dt.Rows(0).Item("nombreCliente") Is DBNull.Value, Nothing, dt.Rows(0).Item("nombreCliente"))
            d.direccion = IIf(dt.Rows(0).Item("direccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("direccion"))
            d.ruc = IIf(dt.Rows(0).Item("rUC") Is DBNull.Value, Nothing, dt.Rows(0).Item("rUC"))
            d.idalmacen = IIf(dt.Rows(0).Item("idAlmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAlmacen"))
            d.idformaventa = IIf(dt.Rows(0).Item("idFormaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idFormaVenta"))
            d.idmoneda = IIf(dt.Rows(0).Item("idMoneda") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMoneda"))
            d.tipocambio = IIf(dt.Rows(0).Item("tipoCambio") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoCambio"))
            d.importetotal = IIf(dt.Rows(0).Item("importeTotal") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotal"))
            d.importeigv = IIf(dt.Rows(0).Item("importeIGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGV"))
            d.saldo = IIf(dt.Rows(0).Item("saldo") Is DBNull.Value, Nothing, dt.Rows(0).Item("saldo"))
            d.importedescuento = IIf(dt.Rows(0).Item("importeDescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeDescuento"))
            d.numeroorden = IIf(dt.Rows(0).Item("numeroOrden") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroOrden"))
            d.idtipodocumento1 = IIf(dt.Rows(0).Item("idTipoDocumento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento1"))
            d.serie1 = IIf(dt.Rows(0).Item("serie1") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie1"))
            d.numerodocumento1 = IIf(dt.Rows(0).Item("numeroDocumento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento1"))
            d.descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.facturaguia = IIf(dt.Rows(0).Item("facturaGuia") Is DBNull.Value, Nothing, dt.Rows(0).Item("facturaGuia"))
            d.idtransportista = IIf(dt.Rows(0).Item("idTransportista") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTransportista"))
            d.idcentrocosto = IIf(dt.Rows(0).Item("idCentroCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCentroCosto"))
            d.idmaquina = IIf(dt.Rows(0).Item("idMaquina") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMaquina"))
            d.destino = IIf(dt.Rows(0).Item("destino") Is DBNull.Value, Nothing, dt.Rows(0).Item("destino"))
            d.idtipofactura = IIf(dt.Rows(0).Item("idTipoFactura") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoFactura"))
            d.idtipoanexo = IIf(dt.Rows(0).Item("idTipoAnexo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoAnexo"))
            d.idanexo = IIf(dt.Rows(0).Item("idAnexo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAnexo"))
            d.descuneto1 = IIf(dt.Rows(0).Item("descuneto1") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuneto1"))
            d.descuento2 = IIf(dt.Rows(0).Item("descuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento2"))
            d.flete = IIf(dt.Rows(0).Item("flete") Is DBNull.Value, Nothing, dt.Rows(0).Item("flete"))
            d.embalaje = IIf(dt.Rows(0).Item("embalaje") Is DBNull.Value, Nothing, dt.Rows(0).Item("embalaje"))
            d.tasa = IIf(dt.Rows(0).Item("tasa") Is DBNull.Value, Nothing, dt.Rows(0).Item("tasa"))
            d.idusuariooperador = IIf(dt.Rows(0).Item("idUsuarioOperador") Is DBNull.Value, Nothing, dt.Rows(0).Item("idUsuarioOperador"))
            d.idusuariosectorista = IIf(dt.Rows(0).Item("idUsuarioSectorista") Is DBNull.Value, Nothing, dt.Rows(0).Item("idUsuarioSectorista"))
            d.idcadena = IIf(dt.Rows(0).Item("idCadena") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCadena"))
            d.idinternocadena = IIf(dt.Rows(0).Item("idInternoCadena") Is DBNull.Value, Nothing, dt.Rows(0).Item("idInternoCadena"))
            d.idautorizacion = IIf(dt.Rows(0).Item("idAutorizacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAutorizacion"))
            d.reparto = IIf(dt.Rows(0).Item("reparto") Is DBNull.Value, Nothing, dt.Rows(0).Item("reparto"))
            d.usuariocrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.fechacrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.usuariomod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.fechamod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.idtiponotacredito = IIf(dt.Rows(0).Item("idTipoNotaCredito") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoNotaCredito"))
            d.linea = IIf(dt.Rows(0).Item("linea") Is DBNull.Value, Nothing, dt.Rows(0).Item("linea"))
            d.impreso = IIf(dt.Rows(0).Item("impreso") Is DBNull.Value, Nothing, dt.Rows(0).Item("impreso"))
            d.anuladonc = IIf(dt.Rows(0).Item("anuladoNC") Is DBNull.Value, Nothing, dt.Rows(0).Item("anuladoNC"))
            d.idvendedor1 = IIf(dt.Rows(0).Item("idVendedor1") Is DBNull.Value, Nothing, dt.Rows(0).Item("idVendedor1"))
            d.igv = IIf(dt.Rows(0).Item("iGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("iGV"))
            d.idchofer = IIf(dt.Rows(0).Item("idchofer") Is DBNull.Value, Nothing, dt.Rows(0).Item("idchofer"))
            d.idzonaventa = IIf(dt.Rows(0).Item("idZonaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idZonaVenta"))
            d.idtipodocumento2 = IIf(dt.Rows(0).Item("idTipoDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento2"))
            d.numerodocumento2 = IIf(dt.Rows(0).Item("numeroDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento2"))
            d.idsubdiario = IIf(dt.Rows(0).Item("idSubdiario") Is DBNull.Value, Nothing, dt.Rows(0).Item("idSubdiario"))
            d.nrocontable = IIf(dt.Rows(0).Item("nrocontable") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrocontable"))
            d.importetotalus = IIf(dt.Rows(0).Item("importeTotalUS") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotalUS"))
            d.importetotalmn = IIf(dt.Rows(0).Item("importeTotalMN") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotalMN"))
            d.importeigvus = IIf(dt.Rows(0).Item("importeIGVUS") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGVUS"))
            d.importeigvmn = IIf(dt.Rows(0).Item("importeIGVMN") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGVMN"))
            d.estadosunat = IIf(dt.Rows(0).Item("estadoSunat") Is DBNull.Value, Nothing, dt.Rows(0).Item("estadoSunat"))
            d.codigohas = IIf(dt.Rows(0).Item("codigohas") Is DBNull.Value, Nothing, dt.Rows(0).Item("codigohas"))
            d.barrapdf417 = IIf(dt.Rows(0).Item("barrapdf417") Is DBNull.Value, Nothing, dt.Rows(0).Item("barrapdf417"))
            d.signaturevalue = IIf(dt.Rows(0).Item("signatureValue") Is DBNull.Value, Nothing, dt.Rows(0).Item("signatureValue"))
            d.tipooperacion = IIf(dt.Rows(0).Item("tipoOperacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoOperacion"))
            d.tipoafecigv = IIf(dt.Rows(0).Item("tipoAfecIGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoAfecIGV"))
            d.islote = IIf(dt.Rows(0).Item("isLote") Is DBNull.Value, Nothing, dt.Rows(0).Item("isLote"))
            d.idturno = IIf(dt.Rows(0).Item("idTurno") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTurno"))
            d.fechadocumento2 = IIf(dt.Rows(0).Item("fechaDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaDocumento2"))
            d.importerefencia = IIf(dt.Rows(0).Item("importeRefencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeRefencia"))
            d.valorventa = IIf(dt.Rows(0).Item("valorVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("valorVenta"))
            d.imagedocumento = IIf(dt.Rows(0).Item("imageDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("imageDocumento"))
            'd.isliq = IIf(dt.Rows(0).Item("isliq") Is DBNull.Value, Nothing, dt.Rows(0).Item("isliq"))
            d.tipo_preventa = IIf(dt.Rows(0).Item("tipo_PreVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipo_PreVenta"))
            d.nro_preventa = IIf(dt.Rows(0).Item("nro_PreVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("nro_PreVenta"))
            d.ispercepcion = IIf(dt.Rows(0).Item("isPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("isPercepcion"))
            d.percepcion = IIf(dt.Rows(0).Item("percepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("percepcion"))
            d.chkbancarizar = IIf(dt.Rows(0).Item("chkbancarizar") Is DBNull.Value, Nothing, dt.Rows(0).Item("chkbancarizar"))
        Else
            'd.IdAgencia = Nothing
            'd.IdTipoDocumento = Nothing
            'd.Serie = Nothing
            'd.NumeroDocumento = Nothing
            d.numeropedido = Nothing
            d.fechadocumento = Nothing
            d.fechavencimineto = Nothing
            d.debehaber = Nothing
            d.idvendedor = Nothing
            d.idcaja = Nothing
            d.idcliente = Nothing
            d.nombrecliente = Nothing
            d.direccion = Nothing
            d.ruc = Nothing
            'd.IdAlmacen = Nothing
            d.idformaventa = Nothing
            d.idmoneda = Nothing
            d.tipocambio = Nothing
            d.importetotal = Nothing
            d.importeigv = Nothing
            d.saldo = Nothing
            d.importedescuento = Nothing
            d.numeroorden = Nothing
            d.idtipodocumento1 = Nothing
            d.serie1 = Nothing
            d.numerodocumento1 = Nothing
            d.descripcion = Nothing
            d.estado = Nothing
            d.facturaguia = Nothing
            d.idtransportista = Nothing
            d.idcentrocosto = Nothing
            d.idmaquina = Nothing
            d.destino = Nothing
            d.idtipofactura = Nothing
            d.idtipoanexo = Nothing
            d.idanexo = Nothing
            d.descuneto1 = Nothing
            d.descuento2 = Nothing
            d.flete = Nothing
            d.embalaje = Nothing
            d.tasa = Nothing
            d.idusuariooperador = Nothing
            d.idusuariosectorista = Nothing
            d.idcadena = Nothing
            d.idinternocadena = Nothing
            d.idautorizacion = Nothing
            d.reparto = Nothing
            d.usuariocrea = Nothing
            d.fechacrea = Nothing
            d.usuariomod = Nothing
            d.fechamod = Nothing
            d.idtiponotacredito = Nothing
            d.linea = Nothing
            d.impreso = Nothing
            d.anuladonc = Nothing
            d.idvendedor1 = Nothing
            d.igv = Nothing
            d.idchofer = Nothing
            d.idzonaventa = Nothing
            d.idtipodocumento2 = Nothing
            d.numerodocumento2 = Nothing
            d.idsubdiario = Nothing
            d.nrocontable = Nothing
            d.importetotalus = Nothing
            d.importetotalmn = Nothing
            d.importeigvus = Nothing
            d.importeigvmn = Nothing
            d.estadosunat = Nothing
            d.codigohas = Nothing
            d.barrapdf417 = Nothing
            d.signaturevalue = Nothing
            d.tipooperacion = Nothing
            d.tipoafecigv = Nothing
            d.islote = Nothing
            d.idturno = Nothing
            d.fechadocumento2 = Nothing
            d.importerefencia = Nothing
            d.valorventa = Nothing
            'd.imageDocumento = Nothing
            'd.isliq = Nothing
            d.tipo_preventa = Nothing
            d.nro_preventa = Nothing
            d.ispercepcion = Nothing
            d.percepcion = Nothing
            d.chkbancarizar = Nothing
        End If
        Return d
    End Function

    Public Function RegistroRegistroMaestroDetalle(d As NComprobante) As NComprobante
        Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.idalmacen}
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim det As DataTable
        ds = sql.ProcedureSQL("Str_Comprobante_Maestro_Detalle", parametros, valores, tipoParametro, 5)
        det = ds.Tables(1)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            d.idagencia = IIf(dt.Rows(0).Item("idAgencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAgencia"))
            d.idtipodocumento = IIf(dt.Rows(0).Item("idTipoDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento"))
            d.serie = IIf(dt.Rows(0).Item("serie") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie"))
            d.numerodocumento = IIf(dt.Rows(0).Item("numeroDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento"))
            d.numeropedido = IIf(dt.Rows(0).Item("numeroPedido") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroPedido"))
            d.fechadocumento = IIf(dt.Rows(0).Item("fechaDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaDocumento"))
            d.fechavencimineto = IIf(dt.Rows(0).Item("fechaVencimineto") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaVencimineto"))
            d.debehaber = IIf(dt.Rows(0).Item("debeHaber") Is DBNull.Value, Nothing, dt.Rows(0).Item("debeHaber"))
            d.idvendedor = IIf(dt.Rows(0).Item("idVendedor") Is DBNull.Value, Nothing, dt.Rows(0).Item("idVendedor"))
            d.idcaja = IIf(dt.Rows(0).Item("idCaja") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCaja"))
            d.idcliente = IIf(dt.Rows(0).Item("idCliente") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCliente"))
            d.nombrecliente = IIf(dt.Rows(0).Item("nombreCliente") Is DBNull.Value, Nothing, dt.Rows(0).Item("nombreCliente"))
            d.direccion = IIf(dt.Rows(0).Item("direccion") Is DBNull.Value, Nothing, dt.Rows(0).Item("direccion"))
            d.ruc = IIf(dt.Rows(0).Item("rUC") Is DBNull.Value, Nothing, dt.Rows(0).Item("rUC"))
            d.idalmacen = IIf(dt.Rows(0).Item("idAlmacen") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAlmacen"))
            d.idformaventa = IIf(dt.Rows(0).Item("idFormaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idFormaVenta"))
            d.idmoneda = IIf(dt.Rows(0).Item("idMoneda") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMoneda"))
            d.tipocambio = IIf(dt.Rows(0).Item("tipoCambio") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoCambio"))
            d.importetotal = IIf(dt.Rows(0).Item("importeTotal") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotal"))
            d.importeigv = IIf(dt.Rows(0).Item("importeIGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGV"))
            d.saldo = IIf(dt.Rows(0).Item("saldo") Is DBNull.Value, Nothing, dt.Rows(0).Item("saldo"))
            d.importedescuento = IIf(dt.Rows(0).Item("importeDescuento") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeDescuento"))
            d.numeroorden = IIf(dt.Rows(0).Item("numeroOrden") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroOrden"))
            d.idtipodocumento1 = IIf(dt.Rows(0).Item("idTipoDocumento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento1"))
            d.serie1 = IIf(dt.Rows(0).Item("serie1") Is DBNull.Value, Nothing, dt.Rows(0).Item("serie1"))
            d.numerodocumento1 = IIf(dt.Rows(0).Item("numeroDocumento1") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento1"))
            d.descripcion = IIf(dt.Rows(0).Item("descripcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("descripcion"))
            d.estado = IIf(dt.Rows(0).Item("estado") Is DBNull.Value, Nothing, dt.Rows(0).Item("estado"))
            d.facturaguia = IIf(dt.Rows(0).Item("facturaGuia") Is DBNull.Value, Nothing, dt.Rows(0).Item("facturaGuia"))
            d.idtransportista = IIf(dt.Rows(0).Item("idTransportista") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTransportista"))
            d.idcentrocosto = IIf(dt.Rows(0).Item("idCentroCosto") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCentroCosto"))
            d.idmaquina = IIf(dt.Rows(0).Item("idMaquina") Is DBNull.Value, Nothing, dt.Rows(0).Item("idMaquina"))
            d.destino = IIf(dt.Rows(0).Item("destino") Is DBNull.Value, Nothing, dt.Rows(0).Item("destino"))
            d.idtipofactura = IIf(dt.Rows(0).Item("idTipoFactura") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoFactura"))
            d.idtipoanexo = IIf(dt.Rows(0).Item("idTipoAnexo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoAnexo"))
            d.idanexo = IIf(dt.Rows(0).Item("idAnexo") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAnexo"))
            d.descuneto1 = IIf(dt.Rows(0).Item("descuneto1") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuneto1"))
            d.descuento2 = IIf(dt.Rows(0).Item("descuento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("descuento2"))
            d.flete = IIf(dt.Rows(0).Item("flete") Is DBNull.Value, Nothing, dt.Rows(0).Item("flete"))
            d.embalaje = IIf(dt.Rows(0).Item("embalaje") Is DBNull.Value, Nothing, dt.Rows(0).Item("embalaje"))
            d.tasa = IIf(dt.Rows(0).Item("tasa") Is DBNull.Value, Nothing, dt.Rows(0).Item("tasa"))
            d.idusuariooperador = IIf(dt.Rows(0).Item("idUsuarioOperador") Is DBNull.Value, Nothing, dt.Rows(0).Item("idUsuarioOperador"))
            d.idusuariosectorista = IIf(dt.Rows(0).Item("idUsuarioSectorista") Is DBNull.Value, Nothing, dt.Rows(0).Item("idUsuarioSectorista"))
            d.idcadena = IIf(dt.Rows(0).Item("idCadena") Is DBNull.Value, Nothing, dt.Rows(0).Item("idCadena"))
            d.idinternocadena = IIf(dt.Rows(0).Item("idInternoCadena") Is DBNull.Value, Nothing, dt.Rows(0).Item("idInternoCadena"))
            d.idautorizacion = IIf(dt.Rows(0).Item("idAutorizacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("idAutorizacion"))
            d.reparto = IIf(dt.Rows(0).Item("reparto") Is DBNull.Value, Nothing, dt.Rows(0).Item("reparto"))
            d.usuariocrea = IIf(dt.Rows(0).Item("usuarioCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioCrea"))
            d.fechacrea = IIf(dt.Rows(0).Item("fechaCrea") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaCrea"))
            d.usuariomod = IIf(dt.Rows(0).Item("usuarioMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("usuarioMod"))
            d.fechamod = IIf(dt.Rows(0).Item("fechaMod") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaMod"))
            d.idtiponotacredito = IIf(dt.Rows(0).Item("idTipoNotaCredito") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoNotaCredito"))
            d.linea = IIf(dt.Rows(0).Item("linea") Is DBNull.Value, Nothing, dt.Rows(0).Item("linea"))
            d.impreso = IIf(dt.Rows(0).Item("impreso") Is DBNull.Value, Nothing, dt.Rows(0).Item("impreso"))
            d.anuladonc = IIf(dt.Rows(0).Item("anuladoNC") Is DBNull.Value, Nothing, dt.Rows(0).Item("anuladoNC"))
            d.idvendedor1 = IIf(dt.Rows(0).Item("idVendedor1") Is DBNull.Value, Nothing, dt.Rows(0).Item("idVendedor1"))
            d.igv = IIf(dt.Rows(0).Item("iGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("iGV"))
            d.idchofer = IIf(dt.Rows(0).Item("idchofer") Is DBNull.Value, Nothing, dt.Rows(0).Item("idchofer"))
            d.idzonaventa = IIf(dt.Rows(0).Item("idZonaVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("idZonaVenta"))
            d.idtipodocumento2 = IIf(dt.Rows(0).Item("idTipoDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTipoDocumento2"))
            d.numerodocumento2 = IIf(dt.Rows(0).Item("numeroDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("numeroDocumento2"))
            d.idsubdiario = IIf(dt.Rows(0).Item("idSubdiario") Is DBNull.Value, Nothing, dt.Rows(0).Item("idSubdiario"))
            d.nrocontable = IIf(dt.Rows(0).Item("nrocontable") Is DBNull.Value, Nothing, dt.Rows(0).Item("nrocontable"))
            d.importetotalus = IIf(dt.Rows(0).Item("importeTotalUS") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotalUS"))
            d.importetotalmn = IIf(dt.Rows(0).Item("importeTotalMN") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeTotalMN"))
            d.importeigvus = IIf(dt.Rows(0).Item("importeIGVUS") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGVUS"))
            d.importeigvmn = IIf(dt.Rows(0).Item("importeIGVMN") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeIGVMN"))
            d.estadosunat = IIf(dt.Rows(0).Item("estadoSunat") Is DBNull.Value, Nothing, dt.Rows(0).Item("estadoSunat"))
            d.codigohas = IIf(dt.Rows(0).Item("codigohas") Is DBNull.Value, Nothing, dt.Rows(0).Item("codigohas"))
            '  d.barrapdf417 = IIf(dt.Rows(0).Item("barrapdf417") Is DBNull.Value, Nothing, dt.Rows(0).Item("barrapdf417"))
            d.signaturevalue = IIf(dt.Rows(0).Item("signatureValue") Is DBNull.Value, Nothing, dt.Rows(0).Item("signatureValue"))
            d.tipooperacion = IIf(dt.Rows(0).Item("tipoOperacion") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoOperacion"))
            d.tipoafecigv = IIf(dt.Rows(0).Item("tipoAfecIGV") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipoAfecIGV"))
            d.islote = IIf(dt.Rows(0).Item("isLote") Is DBNull.Value, Nothing, dt.Rows(0).Item("isLote"))
            d.idturno = IIf(dt.Rows(0).Item("idTurno") Is DBNull.Value, Nothing, dt.Rows(0).Item("idTurno"))
            d.fechadocumento2 = IIf(dt.Rows(0).Item("fechaDocumento2") Is DBNull.Value, Nothing, dt.Rows(0).Item("fechaDocumento2"))
            d.importerefencia = IIf(dt.Rows(0).Item("importeRefencia") Is DBNull.Value, Nothing, dt.Rows(0).Item("importeRefencia"))
            d.valorventa = IIf(dt.Rows(0).Item("valorVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("valorVenta"))
            ' d.imageDocumento = IIf(dt.Rows(0).Item("imageDocumento") Is DBNull.Value, Nothing, dt.Rows(0).Item("imageDocumento"))
            'd.isliq = IIf(dt.Rows(0).Item("isliq") Is DBNull.Value, Nothing, dt.Rows(0).Item("isliq"))
            d.tipo_preventa = IIf(dt.Rows(0).Item("tipo_PreVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("tipo_PreVenta"))
            d.nro_preventa = IIf(dt.Rows(0).Item("nro_PreVenta") Is DBNull.Value, Nothing, dt.Rows(0).Item("nro_PreVenta"))
            d.ispercepcion = IIf(dt.Rows(0).Item("isPercepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("isPercepcion"))
            d.percepcion = IIf(dt.Rows(0).Item("percepcion") Is DBNull.Value, Nothing, dt.Rows(0).Item("percepcion"))
            d.chkbancarizar = IIf(dt.Rows(0).Item("chkbancarizar") Is DBNull.Value, Nothing, dt.Rows(0).Item("chkbancarizar"))

            '   Dim dl As New NDetalleComprobante
            Detalle.Clear()
            For Each dr As DataRow In det.Rows
                Dim dl As New NDetalleComprobante
                dl.idagencia = IIf(dr.Item("idagencia") Is DBNull.Value, Nothing, dr.Item("idagencia"))
                dl.idtipodocumento = IIf(dr.Item("idtipodocumento") Is DBNull.Value, Nothing, dr.Item("idtipodocumento"))
                dl.serie = IIf(dr.Item("serie") Is DBNull.Value, Nothing, dr.Item("serie"))
                dl.numerodocumento = IIf(dr.Item("numerodocumento") Is DBNull.Value, Nothing, dr.Item("numerodocumento"))
                dl.item = IIf(dr.Item("item") Is DBNull.Value, Nothing, dr.Item("item"))
                dl.idarticulo = IIf(dr.Item("idarticulo") Is DBNull.Value, Nothing, dr.Item("idarticulo"))
                dl.descripcion = IIf(dr.Item("descripcion") Is DBNull.Value, Nothing, dr.Item("descripcion"))
                dl.texto = IIf(dr.Item("texto") Is DBNull.Value, Nothing, dr.Item("texto"))
                dl.cantidad = IIf(dr.Item("cantidad") Is DBNull.Value, Nothing, dr.Item("cantidad"))
                dl.unidad = IIf(dr.Item("unidad") Is DBNull.Value, Nothing, dr.Item("unidad"))
                dl.serie1 = IIf(dr.Item("serie1") Is DBNull.Value, Nothing, dr.Item("serie1"))
                dl.cantidad1 = IIf(dr.Item("cantidad1") Is DBNull.Value, Nothing, dr.Item("cantidad1"))
                dl.unidadenvase = IIf(dr.Item("unidadenvase") Is DBNull.Value, Nothing, dr.Item("unidadenvase"))
                dl.numeroenvase = IIf(dr.Item("numeroenvase") Is DBNull.Value, Nothing, dr.Item("numeroenvase"))
                dl.saldoentrega = IIf(dr.Item("saldoentrega") Is DBNull.Value, Nothing, dr.Item("saldoentrega"))
                dl.precioventa = IIf(dr.Item("precioventa") Is DBNull.Value, Nothing, dr.Item("precioventa"))
                dl.precioventah = IIf(dr.Item("precioventah") Is DBNull.Value, Nothing, dr.Item("precioventah"))
                dl.precioventaimportacion = IIf(dr.Item("precioventaimportacion") Is DBNull.Value, Nothing, dr.Item("precioventaimportacion"))
                dl.precioventaimportacionh = IIf(dr.Item("precioventaimportacionh") Is DBNull.Value, Nothing, dr.Item("precioventaimportacionh"))
                dl.preciosigv = IIf(dr.Item("preciosigv") Is DBNull.Value, Nothing, dr.Item("preciosigv"))
                dl.importedescuento = IIf(dr.Item("importedescuento") Is DBNull.Value, Nothing, dr.Item("importedescuento"))
                dl.descuentodocumento = IIf(dr.Item("descuentodocumento") Is DBNull.Value, Nothing, dr.Item("descuentodocumento"))
                dl.cargodistribucion = IIf(dr.Item("cargodistribucion") Is DBNull.Value, Nothing, dr.Item("cargodistribucion"))
                dl.igv = IIf(dr.Item("igv") Is DBNull.Value, Nothing, dr.Item("igv"))
                dl.importeigv = IIf(dr.Item("importeigv") Is DBNull.Value, Nothing, dr.Item("importeigv"))
                dl.importeus = IIf(dr.Item("importeus") Is DBNull.Value, Nothing, dr.Item("importeus"))
                dl.importemn = IIf(dr.Item("importemn") Is DBNull.Value, Nothing, dr.Item("importemn"))
                dl.idtipoitemdescuento = IIf(dr.Item("idtipoitemdescuento") Is DBNull.Value, Nothing, dr.Item("idtipoitemdescuento"))
                dl.descuento1 = IIf(dr.Item("descuento1") Is DBNull.Value, Nothing, dr.Item("descuento1"))
                dl.importedescuento1 = IIf(dr.Item("importedescuento1") Is DBNull.Value, Nothing, dr.Item("importedescuento1"))
                dl.descuento2 = IIf(dr.Item("descuento2") Is DBNull.Value, Nothing, dr.Item("descuento2"))
                dl.importedescuento2 = IIf(dr.Item("importedescuento2") Is DBNull.Value, Nothing, dr.Item("importedescuento2"))
                dl.descuento3 = IIf(dr.Item("descuento3") Is DBNull.Value, Nothing, dr.Item("descuento3"))
                dl.importedescuento3 = IIf(dr.Item("importedescuento3") Is DBNull.Value, Nothing, dr.Item("importedescuento3"))
                dl.descuento4 = IIf(dr.Item("descuento4") Is DBNull.Value, Nothing, dr.Item("descuento4"))
                dl.importedescuento4 = IIf(dr.Item("importedescuento4") Is DBNull.Value, Nothing, dr.Item("importedescuento4"))
                dl.descuento5 = IIf(dr.Item("descuento5") Is DBNull.Value, Nothing, dr.Item("descuento5"))
                dl.importedescuento5 = IIf(dr.Item("importedescuento5") Is DBNull.Value, Nothing, dr.Item("importedescuento5"))
                dl.descuento6 = IIf(dr.Item("descuento6") Is DBNull.Value, Nothing, dr.Item("descuento6"))
                dl.estado = IIf(dr.Item("estado") Is DBNull.Value, Nothing, dr.Item("estado"))
                dl.vendedor = IIf(dr.Item("vendedor") Is DBNull.Value, Nothing, dr.Item("vendedor"))
                dl.idalmacen = IIf(dr.Item("idalmacen") Is DBNull.Value, Nothing, dr.Item("idalmacen"))
                dl.numerocaja = IIf(dr.Item("numerocaja") Is DBNull.Value, Nothing, dr.Item("numerocaja"))
                dl.stock = IIf(dr.Item("stock") Is DBNull.Value, Nothing, dr.Item("stock"))
                dl.fechasdocumento = IIf(dr.Item("fechasdocumento") Is DBNull.Value, Nothing, dr.Item("fechasdocumento"))
                dl.idlinea = IIf(dr.Item("idlinea") Is DBNull.Value, Nothing, dr.Item("idlinea"))
                dl.idcampania = IIf(dr.Item("idcampania") Is DBNull.Value, Nothing, dr.Item("idcampania"))
                dl.numeropaquete = IIf(dr.Item("numeropaquete") Is DBNull.Value, Nothing, dr.Item("numeropaquete"))
                dl.nrodescuentofinaciero = IIf(dr.Item("nrodescuentofinaciero") Is DBNull.Value, Nothing, dr.Item("nrodescuentofinaciero"))
                dl.nrodescuentolaboratorio = IIf(dr.Item("nrodescuentolaboratorio") Is DBNull.Value, Nothing, dr.Item("nrodescuentolaboratorio"))
                dl.nrodescuentoadicional = IIf(dr.Item("nrodescuentoadicional") Is DBNull.Value, Nothing, dr.Item("nrodescuentoadicional"))
                dl.nrodescuentobonificacion = IIf(dr.Item("nrodescuentobonificacion") Is DBNull.Value, Nothing, dr.Item("nrodescuentobonificacion"))
                dl.nrodescuentoflag = IIf(dr.Item("nrodescuentoflag") Is DBNull.Value, Nothing, dr.Item("nrodescuentoflag"))
                dl.comision = IIf(dr.Item("comision") Is DBNull.Value, Nothing, dr.Item("comision"))
                dl.importecomision = IIf(dr.Item("importecomision") Is DBNull.Value, Nothing, dr.Item("importecomision"))
                dl.usuariocrea = IIf(dr.Item("usuariocrea") Is DBNull.Value, Nothing, dr.Item("usuariocrea"))
                dl.fechacrea = IIf(dr.Item("fechacrea") Is DBNull.Value, Nothing, dr.Item("fechacrea"))
                dl.preciounitarioorigen = IIf(dr.Item("preciounitarioorigen") Is DBNull.Value, Nothing, dr.Item("preciounitarioorigen"))
                dl.idvendedor2 = IIf(dr.Item("idvendedor2") Is DBNull.Value, Nothing, dr.Item("idvendedor2"))
                dl.identrada = IIf(dr.Item("identrada") Is DBNull.Value, Nothing, dr.Item("identrada"))
                dl.npfacturado = IIf(dr.Item("npfacturado") Is DBNull.Value, Nothing, dr.Item("npfacturado"))
                dl.idlista = IIf(dr.Item("idlista") Is DBNull.Value, Nothing, dr.Item("idlista"))
                dl.loteserie = IIf(dr.Item("loteserie") Is DBNull.Value, Nothing, dr.Item("loteserie"))
                dl.lado = IIf(dr.Item("lado") Is DBNull.Value, Nothing, dr.Item("lado"))
                dl.tipo_preventa = IIf(dr.Item("tipo_preventa") Is DBNull.Value, Nothing, dr.Item("tipo_preventa"))
                dl.nro_preventa = IIf(dr.Item("nro_preventa") Is DBNull.Value, Nothing, dr.Item("nro_preventa"))
                dl.preciototal = IIf(dr.Item("preciototal") Is DBNull.Value, Nothing, dr.Item("preciototal"))
                dl.dtipooperacion = IIf(dr.Item("dtipooperacion") Is DBNull.Value, Nothing, dr.Item("dtipooperacion"))
                dl.dtipoafecigv = IIf(dr.Item("dtipoafecigv") Is DBNull.Value, Nothing, dr.Item("dtipoafecigv"))
                dl.igv = IIf(dr.Item("igv") Is DBNull.Value, Nothing, dr.Item("igv"))
                Detalle.Add(dl)
            Next

        Else
            'd.IdAgencia = Nothing
            'd.IdTipoDocumento = Nothing
            'd.Serie = Nothing
            'd.NumeroDocumento = Nothing
            d.numeropedido = Nothing
            d.fechadocumento = Nothing
            d.fechavencimineto = Nothing
            d.debehaber = Nothing
            d.idvendedor = Nothing
            d.idcaja = Nothing
            d.idcliente = Nothing
            d.nombrecliente = Nothing
            d.direccion = Nothing
            d.ruc = Nothing
            'd.IdAlmacen = Nothing
            d.idformaventa = Nothing
            d.idmoneda = Nothing
            d.tipocambio = Nothing
            d.importetotal = Nothing
            d.importeigv = Nothing
            d.saldo = Nothing
            d.importedescuento = Nothing
            d.numeroorden = Nothing
            d.idtipodocumento1 = Nothing
            d.serie1 = Nothing
            d.numerodocumento1 = Nothing
            d.descripcion = Nothing
            d.estado = Nothing
            d.facturaguia = Nothing
            d.idtransportista = Nothing
            d.idcentrocosto = Nothing
            d.idmaquina = Nothing
            d.destino = Nothing
            d.idtipofactura = Nothing
            d.idtipoanexo = Nothing
            d.idanexo = Nothing
            d.descuneto1 = Nothing
            d.descuento2 = Nothing
            d.flete = Nothing
            d.embalaje = Nothing
            d.tasa = Nothing
            d.idusuariooperador = Nothing
            d.idusuariosectorista = Nothing
            d.idcadena = Nothing
            d.idinternocadena = Nothing
            d.idautorizacion = Nothing
            d.reparto = Nothing
            d.usuariocrea = Nothing
            d.fechacrea = Nothing
            d.usuariomod = Nothing
            d.fechamod = Nothing
            d.idtiponotacredito = Nothing
            d.linea = Nothing
            d.impreso = Nothing
            d.anuladonc = Nothing
            d.idvendedor1 = Nothing
            d.igv = Nothing
            d.idchofer = Nothing
            d.idzonaventa = Nothing
            d.idtipodocumento2 = Nothing
            d.numerodocumento2 = Nothing
            d.idsubdiario = Nothing
            d.nrocontable = Nothing
            d.importetotalus = Nothing
            d.importetotalmn = Nothing
            d.importeigvus = Nothing
            d.importeigvmn = Nothing
            d.estadosunat = Nothing
            d.codigohas = Nothing
            ' d.barrapdf417 = Nothing
            d.signaturevalue = Nothing
            d.tipooperacion = Nothing
            d.tipoafecigv = Nothing
            d.islote = Nothing
            d.idturno = Nothing
            d.fechadocumento2 = Nothing
            d.importerefencia = Nothing
            d.valorventa = Nothing
            'd.imageDocumento = Nothing
            'd.isliq = Nothing
            d.tipo_preventa = Nothing
            d.nro_preventa = Nothing
            d.ispercepcion = Nothing
            d.percepcion = Nothing
            d.chkbancarizar = Nothing
            d.Detalle = Nothing
        End If
        Return d
    End Function

    Public Function lista_nc() As DataTable
        Dim c As String = " select ct.idcliente,cl.Nombre,ct.IdMoneda,ct.IdTipoDocumento,ct.NumeroDocumento,ct.Importe,ct.Saldo,c.IdTipoDocumento2,c.NumeroDocumento2,isnull(n.idcuentac,ct.idcuenta) as IdCuenta  from deuda ct left join "
        c += " Comprobante c on ct.idtipodocumento=c.idtipodocumento and ct.NumeroDocumento=rtrim(c.Serie)+rtrim(c.NumeroDocumento) "
        c += " left join Cliente cl on ct.IdCliente=cl.IdCliente "
        c += " left join numeracion n on c.idtipodocumento=n.idtipodocumento and c.serie=n.serie where ct.IdTipoDocumento='NA' "
        c += " and ct.saldo<>0 "
        Return sql.EjecutarConsulta("d", c).Tables(0)
    End Function


    Public Function Existe_Comprobante(d As NComprobante) As Boolean
        Dim parametros() As Object = {"@idAgencia", "@idTipoDocumento", "@serie", "@numeroDocumento", "@idAlmacen"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {d.idagencia, d.idtipodocumento, d.serie, d.numerodocumento, d.idalmacen}
        Dim resultado As Integer
        Dim bandera As Boolean = False
        resultado = sql.procedimiento_escalar("Existe_Comprobante", parametros, valores, tipoParametro, 5)
        If resultado = 1 Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function

    '

    Public Function lista_documentosCPE(alm As String, fi As DateTime, ff As DateTime, estado As String) As DataTable
        Dim parametros() As Object = {"@Idalmacen", "@FechaI", "@FechaF", "@Estado"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Char}
        Dim valores() As Object = {alm, fi, ff, estado}
        Return sql.ProcedureSQL("Str_listadocumento_s", parametros, valores, tipoParametro, 4).Tables(0)
    End Function

    ''' <summary>
    ''' Actualiza codigo has, signatuve y barras QR y estado sunat
    ''' </summary>
    ''' <param name="d"></param>
    Public Sub CodigoBarraras(d As NComprobante)
        sql.Editar("Comprobante", "EstadoSunat='1',Codigohas='" & d.codigohas & "', signatureValue='" & d.signaturevalue & "'", "IdTipoDocumento='" & d.idtipodocumento & "' and serie='" & d.serie & "' and numerodocumento='" & d.numerodocumento & "'")
    End Sub

    Public Sub CopiaCabecera(di As String, df As String, Optional nametable As String = "tmpComprobante")
        Dim s As String = " IF OBJECT_ID('[dbo].[" & nametable & "]') IS NOT NULL BEGIN     DROP TABLE [dbo].[" & nametable & "] END select *  into " & nametable & " from comprobante where FechaDocumento between '" & di & "' and '" & df & "'"
        sql.EjecutarConsulta("d", s)
    End Sub
    Public Sub CopiaDetalle(di As String, df As String, Optional nametable As String = "tmpDetalleComprobante")
        Dim s As String = "  IF OBJECT_ID('[dbo].[" & nametable & "]') IS NOT NULL BEGIN     DROP TABLE [dbo].[" & nametable & "] END  select d.*   into " & nametable & " from detallecomprobante d inner join comprobante c
            on c.IdAgencia =d.IdAgencia and c.IdTipoDocumento=d.IdTipoDocumento
            and c.Serie=d.Serie and c.NumeroDocumento=d.numerodocumento
            where c.FechaDocumento between '" & di & "' and '" & df & "'"
        sql.EjecutarConsulta("d", s)
    End Sub
    Public Sub CopiaCliente(di As String, df As String, Optional nametable As String = "tmpCliente")
        Dim s As String = "  IF OBJECT_ID('[dbo].[" & nametable & "]') IS NOT NULL BEGIN     DROP TABLE [dbo].[" & nametable & "] END  select cl.*   into " & nametable & "  from cliente cl inner join  "
        s += " (select idcliente from comprobante where FechaDocumento between '" & di & "' and '" & df & "'"
        s += " group by idcliente ) as cli on cl.IdCliente=cli.IdCliente "
        sql.EjecutarConsulta("d", s)
    End Sub
    Public Function Lista_DocumentoMigrar(alm As String, fi As DateTime, ff As DateTime) As DataTable
        Dim strTexto As String = ""
        strTexto = " SELECT    C.IdAgencia, C.IdTipoDocumento AS TD, C.Serie, C.NumeroDocumento AS Numero, "
        strTexto += "          C.FechaDocumento AS Fecha, C.IdCliente, C.NombreCliente AS RazonSocial,c.valorVenta, "
        strTexto += "          C.ImporteIGV, C.ImporteTotal,C.Estado,c.IdAlmacen,tipocambio"
        strTexto += " FROM     Comprobante C "
        strTexto += " where c.idalmacen='" & alm & "' and c.FechaDocumento between '" & fi & "' and '" & ff & "' "
        strTexto += "order by 1,2,3,4,5 "
        Return sql.EjecutarConsulta("d", strTexto).Tables(0)
    End Function

    Public Function Existe_Movimiento_Relacionado(d As NComprobante) As Boolean
        Dim parametros() As Object = {"@TipoDocumento", "@IdCliente", "@IdTipoDocumento", "@Numerodocumento"}
        Dim tipoParametro() As Object = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores() As Object = {"PS", d.idcliente, d.idtipodocumento, d.serie & d.numerodocumento}
        Dim resultado As Integer
        Dim bandera As Boolean = False
        resultado = sql.procedimiento_escalar("ExisteMov_Relacionado", parametros, valores, tipoParametro, 4)
        If resultado = 1 Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function
    Public Function Comprobante_Combustible(i As DateTime, f As DateTime, Optional esliq As Boolean = False) As DataTable
        Dim s As String = " select Cast(0 as bit) as ok,IdTipoDocumento,c.Serie,NumeroDocumento,FechaDocumento,FechaDocumento2,c.idcaja,vc.Cajas "
        s += " ,c.idturno,t.Turno,c.usuariocrea,pt.alias as Usuario,c.Estado from Comprobante c inner join VTurno t on c.IdTurno=t.IdTurno "
        s += " inner join VCajas vc on c.IdCaja=vc.IdCaja inner join ptusuario pt on c.usuariocrea=pt.IdUsuario "
        If esliq = False Then
            s += " Where FechaDocumento between '" & Format(i, "dd/MM/yy") & "' and '" & Format(f, "dd/MM/yy") & "'"
        End If
        If esliq = True Then
            s += " Where FechaDocumento2 between '" & Format(i, "dd/MM/yy") & "' and '" & Format(f, "dd/MM/yy") & "'"
        End If
        s += " Order by  IdTipoDocumento,c.Serie,NumeroDocumento "
        Return sql.EjecutarConsulta("d", s).Tables(0)
    End Function
    Public Sub Actualizar_grifo(d As NComprobante)
        sql.Editar("Comprobante", "idTurno='" & d.idturno & "',idCaja='" & d.idcaja & "',FechaDocumento2='" & d.fechadocumento2 & "'", "IdTipoDocumento='" & d.idtipodocumento & "' and serie='" & d.serie & "' and numerodocumento='" & d.numerodocumento & "'")
    End Sub
    Public Sub Actualizar_EstadoSunat(d As NComprobante)
        sql.Editar("Comprobante", "EstadoSunat='" & d.estadosunat & "'", "IdTipoDocumento='" & d.idtipodocumento & "' and serie='" & d.serie & "' and numerodocumento='" & d.numerodocumento & "'")
    End Sub

#End Region
    Public Function lista_fpt(d As NComprobante, Optional desde As String = Nothing, Optional hasta As String = Nothing) As DataTable
        Dim dt As New DataTable()
        Dim parametros As Object() = New Object() {"@estado_sunat", "@fechaemision", "@desde", "@hasta", "@idtipodocumento"}
        Dim tipoParametro As SqlDbType() = New SqlDbType() {SqlDbType.[Char], SqlDbType.[Date], SqlDbType.[Date], SqlDbType.[Date], SqlDbType.VarChar}
        Dim valores As Object() = New Object() {d.estadosunat, d.fechadocumento, desde, hasta, d.idtipodocumento}
        dt = sql.ProcedureSQL("Str_Comprobanteftp_fecha_S", parametros, valores, tipoParametro, 5).Tables(0)
        Return dt
    End Function

    Public Function LiquidacionGeneral(mon As String, fi As DateTime, ff As DateTime, ai As String, af As String) As DataTable
        Dim dt As New DataTable()
        Dim parametros As Object() = New Object() {"@IdMoneda", "@FechaI", "@FechaF", "@IdAlmacenI", "@IdAlmacenF"}
        Dim tipoParametro As SqlDbType() = {SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores As Object() = New Object() {mon, fi, ff, ai, af}
        dt = sql.ProcedureSQL("Str_LiquidacionGeneral", parametros, valores, tipoParametro, 5).Tables(0)
        Return dt
    End Function
    Public Function LiquidacionGeneral_Acumulado(mon As String, fi As DateTime, ff As DateTime, ai As String, af As String) As DataTable
        Dim dt As New DataTable()
        Dim parametros As Object() = New Object() {"@IdMoneda", "@FechaI", "@FechaF", "@IdAlmacenI", "@IdAlmacenF"}
        Dim tipoParametro As SqlDbType() = {SqlDbType.VarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim valores As Object() = New Object() {mon, fi, ff, ai, af}
        dt = sql.ProcedureSQL("Str_LiquidacionGeneral_Saldo", parametros, valores, tipoParametro, 5).Tables(0)
        Return dt
    End Function

    Public Function Str_Detalle_Resumen_Documento(fi As Date, ff As Date, isdetalle As Boolean) As DataTable
        Dim dt As New DataTable()
        Dim parametros As Object() = New Object() {"@FechaI", "@FechaF", "@IsDetalle"}
        Dim tipoParametro As SqlDbType() = {SqlDbType.Date, SqlDbType.Date, SqlDbType.Bit}
        Dim valores As Object() = New Object() {fi, ff, isdetalle}
        dt = sql.ProcedureSQL("Str_Detalle_Resumen_Documento", parametros, valores, tipoParametro, 3).Tables(0)
        Return dt
    End Function

    Public Function Generar_Faltante(dtr As DataTable, dtl As DataTable) As DataTable
        Dim sParametro As Object() = {"@Resumen", "@Detalle"}
        Dim typeParam As Object() = {SqlDbType.Structured, SqlDbType.Structured}
        Dim vParametro As Object() = {dtr, dtl}
        Return Me.sql.ProcedureSQL("Str_GeneraFaltante_S", sParametro, vParametro, typeParam, 2).Tables(0)
    End Function
    ''' <summary>
    ''' Retorna un booleano que confirma si existe o no documentos vencidos
    ''' </summary>
    ''' <param name="dias"></param>
    ''' <returns></returns>
    Public Function Documentos_Vencidos(dias As Integer) As Boolean
        Dim sParametro As Object() = {"@Diastranscurridos"}
        Dim typeParam As Object() = {SqlDbType.Int}
        Dim vParametro As Object() = {dias}
        Dim dt As New DataTable
        Dim bandera As Boolean = False
        dt = sql.ProcedureSQL("Str_Documentos_Vencidos", sParametro, vParametro, typeParam, 1).Tables(0)
        If dt.Rows.Count > 0 Then
            bandera = True
        Else
            bandera = False
        End If
        Return bandera
    End Function
    ''' <summary>
    ''' Retorna en formato lista algunos documentos vencidos
    ''' </summary>
    ''' <param name="dias"></param>
    ''' <returns></returns>
    Public Function lista_Vencidos(dias As Integer) As DataTable
        Dim sParametro As Object() = {"@Diastranscurridos"}
        Dim typeParam As Object() = {SqlDbType.Int}
        Dim vParametro As Object() = {dias}
        Return sql.ProcedureSQL("Str_lista_vencidos", sParametro, vParametro, typeParam, 1).Tables(0)

    End Function
    ''' <summary>
    ''' Registro de ventas el tipo de reporte 1 detallado, 2 resumen por dia y serie, 3 resumen por dia
    ''' </summary>
    ''' <param name="fi"></param>
    ''' <param name="ff"></param>
    ''' <param name="mon"></param>
    ''' <param name="cliente"></param>
    ''' <param name="tiporeporte"></param>
    ''' <returns></returns>
    Public Function RegistrodeVentas(fi As DateTime, ff As DateTime, mon As String, cliente As DataTable, tiporeporte As String) As DataTable
        Dim sParametro As Object() = {"@FechaI", "@FechaF", "@idmoneda", "@cliente", "@tiporeporte"}
        Dim typeParam As Object() = {SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Structured, SqlDbType.Char}
        Dim vParametro As Object() = {fi, ff, mon, cliente, tiporeporte}
        Return sql.ProcedureSQL("Str_RegistrodeVentas", sParametro, vParametro, typeParam, 5).Tables(0)
    End Function

    Public Function RegistrodeVentasDetalle(fi As DateTime, ff As DateTime, cliente As DataTable, almacen As String) As DataTable
        Dim sParametro As Object() = {"@FechaI", "@FechaF", "@cliente", "@idalmacen"}
        Dim typeParam As Object() = {SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Structured, SqlDbType.VarChar}
        Dim vParametro As Object() = {fi, ff, cliente, almacen}
        Return sql.ProcedureSQL("Str_RegistrodeVentasDetalle", sParametro, vParametro, typeParam, 4).Tables(0)
    End Function

    Public Function Registo_asientos(Fi As DateTime, Ff As DateTime, Idanul As String) As DataTable
        Dim cadena As String = " SELECT     idsubdiario, idcuenta, debehaber, idtipodocumento, serie, numerodocumento, Fechacom, FechaVen, idcliente, cliente, ruc, valorventa, igv, precioventa, idmoneda, idformaventa, descripcion,IdCtaIGV,IdCtaValorVta  "
        cadena += " FROM         dbo.FComprobante01('" & Fi & "', '" & Ff & "', '" & Idanul & "')"
        cadena += " order by  idsubdiario,idtipodocumento,Serie,cast(numerodocumento as int)asc  "
        Return sql.EjecutarConsulta("d", cadena).Tables(0)
    End Function

    Public Function Resumen_Subdiario(Fi As DateTime, Ff As DateTime, Idanul As String) As DataTable
        Dim cadena As String = " SELECT idsubdiario, '' as Nro  "
        cadena += " FROM dbo.FComprobante01('" & Fi & "', '" & Ff & "', '" & Idanul & "')"
        cadena += " group by idsubdiario  "
        Return sql.EjecutarConsulta("d", cadena).Tables(0)
    End Function
    Public Function Asiento_venta(fi As DateTime, ff As DateTime, codanull As String, numero As DataTable) As DataTable
        Dim sParametro As Object() = {"@FechaI", "@FechaF", "@CodAnull", "@Nume"}
        Dim typeParam As Object() = {SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.Structured}
        Dim vParametro As Object() = {fi, ff, codanull, numero}
        Return sql.ProcedureSQL("Str_AsientoVenta", sParametro, vParametro, typeParam, 4).Tables(0)
    End Function

    Public Function Detalle_Asiento(idtipodcumento As String, serie As String, numerodocumento As String) As DataTable
        Dim sParametro As Object() = {"@Idtipodocumento", "@Serie", "@NumeroDocumento"}
        Dim typeParam As Object() = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim vParametro As Object() = {idtipodcumento, serie, numerodocumento}
        Return sql.ProcedureSQL("Str_Detalle_Asiento", sParametro, vParametro, typeParam, 3).Tables(0)
    End Function
    Public Function Rpt_NotaVenta(fechadesde As String, fechahasta As String) As DataTable
        Dim sParametro As Object() = {"@fechaI", "@fechaF"}
        Dim typeParam As Object() = {SqlDbType.Date, SqlDbType.Date}
        Dim vParametro As Object() = {fechadesde, fechahasta}
        Return sql.ProcedureSQL("Rpt_Despacho_NotadeVenta_S", sParametro, vParametro, typeParam, 2).Tables(0)
    End Function
    Public Function ListaComprobanteElectronico(Serie As String, IdTipoDocumento As String, NumeroDocumento As String) As DataTable
        Dim sParametro As Object() = {"@serie", "@IdTipoDocumento", "@NumeroDocumento"}
        Dim typeParam As Object() = {SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar}
        Dim vParametro As Object() = {Serie, IdTipoDocumento, NumeroDocumento}
        Return sql.ProcedureSQL("Str_ComprobanteElectronico_S", sParametro, vParametro, typeParam, 3).Tables(0)
    End Function
End Class
