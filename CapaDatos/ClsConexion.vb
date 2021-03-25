Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text
Imports System.Windows.Forms

Public Class ClsConexion
    Public Property CadenaConexion As String
    Public Sub Configuracionregional()
        Dim forceDotCulture As CultureInfo
        ' System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        forceDotCulture = Application.CurrentCulture.Clone
        forceDotCulture = New System.Globalization.CultureInfo("es-PE")
        forceDotCulture.NumberFormat.NumberDecimalSeparator = "."
        forceDotCulture.NumberFormat.NumberGroupSeparator = ","
        forceDotCulture.NumberFormat.CurrencyDecimalSeparator = "."
        forceDotCulture.NumberFormat.CurrencyGroupSeparator = ","
        forceDotCulture.DateTimeFormat.DateSeparator = "/"
        forceDotCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        forceDotCulture.DateTimeFormat.LongDatePattern = "dddd, dd' de 'MMMM' de 'aaaa"
        forceDotCulture.DateTimeFormat.ShortTimePattern = "hh:mm tt"
        forceDotCulture.DateTimeFormat.LongTimePattern = "hh:mm:ss tt"
        Application.CurrentCulture = forceDotCulture
        'Global.SIGMA.My.Resources.Culture = forceDotCulture
    End Sub

#Region "Atributos"
    Private po_Conexion As SqlConnection = Nothing
    Private po_Adaptador As SqlDataAdapter = Nothing
    Private po_Comando As SqlCommand = Nothing
    Private po_SQLParametro As SqlParameter = Nothing
#End Region

#Region "Metodos"

    Public Sub New()
        'pc_Servidor = "Administrador"
        'pc_BaseDatos = "master"
        'pc_Usuario = "sa"
        'pc_Contrasena = "perucom"
    End Sub
    Public Sub saveimagen(ByVal Tabla As String, ByVal campoImage As String, ByVal CampoKey As String, ByVal ValId As String, ByVal Valimage As Byte())
        Using conn = New SqlConnection("Server=" + pc_Servidor + "; DataBase=" +
                pc_BaseDatos + ";UID=" + pc_Usuario + "; PWD=" + pc_Contrasena + ";")
            Dim query As String = " update " & Tabla & " set " & campoImage & "= @foto where " & CampoKey & "='" & ValId.Trim & "'"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.Add("@foto", System.Data.SqlDbType.Image).Value = Valimage
            conn.Open()
            Try
                cmd.ExecuteNonQuery()
                conn.Close()

            Catch ex As Exception
                '   MessageBox.Show(ex.ToString())
            End Try
        End Using
    End Sub

    Private Function Conectar_BD() As Boolean
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        'Configuracionregional()
        Try
            If po_Conexion Is Nothing Then 'verifica que no exista conexion 
                po_Conexion = New SqlConnection("Server=" + pc_Servidor + "; DataBase=" +
                pc_BaseDatos + ";UID=" + pc_Usuario + "; PWD=" + pc_Contrasena + ";")
                ' po_Conexion = New SqlConnection("Data Source=WIN10-309; Initial Catalog=COM2044921316618; Integrated Security=True;UID=sa; PWD=123456;")
                po_Conexion.Open()
                Return True
            Else
                If po_Conexion.State.Equals(ConnectionState.Closed) Then
                    ' MsgBox("La Conexión Se encuentra Cerrada.")
                Else
                    ' MsgBox("La Conexión ya se encuentra abierta.")
                End If
            End If
        Catch ex As Exception
            'MsgBox(ex.ToString)
            ' MessageBox.Show("Datos Incorrectos: revise el ID de servidor y la contraseña de usuario")
            po_Conexion = Nothing
        End Try
        Return False
    End Function

    Public Sub Asignar_Servidor(ByVal aServidor As String, ByVal aUsuario As String, ByVal aContrasena As String)
        pc_Servidor = aServidor
        pc_Usuario = aUsuario
        pc_Contrasena = aContrasena
    End Sub
    Public Sub Asignar_Servidor(ByVal aServidor As String, ByVal aUsuario As String, ByVal aContrasena As String, ByVal aBaseDatos As String)
        pc_Servidor = aServidor
        pc_Usuario = aUsuario
        pc_Contrasena = aContrasena
        pc_BaseDatos = aBaseDatos
    End Sub

    'Procedimiento para asignar Base de Datos
    Public Sub Asignar_BD(ByVal aBaseDatos As String)
        pc_BaseDatos = aBaseDatos
    End Sub
    'Procedimiento que desconecta la Base de Datos del Servidor
    Public Sub Desconectar_BD()
        If Not (po_Conexion Is Nothing) Then
            If po_Conexion.State.Equals(ConnectionState.Open) Then
                po_Conexion.Close()
                po_Conexion = Nothing
            End If
        End If
    End Sub

    'Procedimiento para modificar el administrador y contraseña de usuario
    Public Sub Modificar_Usuario(ByVal Nombre_Servidor As String, ByVal Contrasenia As String)
        pc_Servidor = Nombre_Servidor
        pc_Contrasena = Contrasenia
    End Sub

    'Procedimiento para verificar si la conexion existe
    Public Function Conectado() As Boolean
        Dim lbln_sw As Boolean
        If po_Conexion Is Nothing Then 'verifica que no exista conexion 
            lbln_sw = True
        Else
            lbln_sw = False
        End If
        Return lbln_sw
    End Function

    'Procedimiento para verificar si el estado de la conexion es abierto
    Public Function BD_Abierto() As Boolean
        Dim lbln_sw As Boolean
        If po_Conexion.State.Equals(ConnectionState.Open) Then
            lbln_sw = True
        Else
            lbln_sw = False
        End If
        Return lbln_sw
    End Function

    'Procedimiento que Crea una Sentencia SQL para luego procesarla
    Public Sub Crear_Comando(ByVal SentenciaSQL As String)
        Me.po_Comando = New SqlCommand
        'Me.po_Comando.Connection = po_Conexion
        Me.po_Comando.CommandType = CommandType.Text
        Me.po_Comando.CommandText = SentenciaSQL
    End Sub

    'Procedimiento para Insertar registros a una tabla de la Base de Datos
    Public Sub Insertar_Items(ByVal TxtTabla As String, ByVal campos As String, ByVal valores As String)
        If Conectar_BD() = True Then ' conectando BD
            Try
                Me.po_Comando = New SqlCommand("INSERT INTO " & TxtTabla & " (" & campos & ") VALUES (" & valores & ")", po_Conexion)
                Me.po_Adaptador = New SqlDataAdapter
                Me.po_Adaptador.InsertCommand = po_Comando
                Me.po_Adaptador.InsertCommand.CommandTimeout = 120
                Me.po_Adaptador.InsertCommand.ExecuteNonQuery()
                Desconectar_BD()
            Catch ex As Exception
                'MessageBox.Show(ex.ToString)
                'po_Conexion = Nothing
                Desconectar_BD()
            End Try
        End If
    End Sub

    Public Sub Insertar_ItemsMasivo(ByVal TxtTabla As String, ByVal campos As String, ByVal valores As String)
        If Conectar_BD() = True Then ' conectando BD
            Try
                Me.po_Comando = New SqlCommand("INSERT INTO " & TxtTabla & " (" & campos & ") VALUES  " & valores, po_Conexion)
                Me.po_Adaptador = New SqlDataAdapter
                Me.po_Adaptador.InsertCommand = po_Comando
                Me.po_Adaptador.InsertCommand.CommandTimeout = 120
                Me.po_Adaptador.InsertCommand.ExecuteNonQuery()

                Desconectar_BD()
            Catch ex As Exception
                ' MessageBox.Show(ex.ToString)
                'po_Conexion = Nothing
                Desconectar_BD()
            End Try
        End If
    End Sub

    'Procedimiento para Eliminar registros de una tabla en la Base de Datos
    Public Sub Eliminar_Items(ByVal TxtTabla As String, ByVal Condicion As String)
        If Conectar_BD() = True Then ' conectando BD
            Me.po_Comando = New SqlCommand("DELETE FROM  " & TxtTabla & " WHERE " & Condicion, po_Conexion)
            Me.po_Adaptador = New SqlDataAdapter
            Me.po_Adaptador.DeleteCommand = po_Comando
            Me.po_Adaptador.DeleteCommand.ExecuteNonQuery()
            Desconectar_BD()
        End If
    End Sub
    Public Sub Editar(ByVal TxtTabla As String, ByVal Campos As String, ByVal Condicion As String)
        If Conectar_BD() = True Then ' conectando BD
            Me.po_Comando = New SqlCommand("UPDATE " & TxtTabla & " SET  " & Campos & " WHERE " & Condicion, po_Conexion)
            Me.po_Adaptador = New SqlDataAdapter
            Me.po_Adaptador.UpdateCommand = po_Comando
            Me.po_Adaptador.UpdateCommand.ExecuteNonQuery()
            Desconectar_BD()
        End If
    End Sub
    Public Function ValorEscalar(ByVal nomFuncion As String, ByVal vValores As Object, ByVal nValores As Integer) As String
        Dim Aindice As Integer, sParametro As New StringBuilder(), SALIDA As String
        sParametro.Length = 0
        For Aindice = 0 To (nValores - 1)
            sParametro.Append(vValores(Aindice))
            If Aindice <> (nValores - 1) Then sParametro.Append(", ")
        Next
        If Conectar_BD() = True Then ' conectando BD
            Dim comando1 As New SqlCommand("", po_Conexion)
            comando1.CommandText = "select " & nomFuncion & "(" & sParametro.ToString & ")"
            SALIDA = comando1.ExecuteScalar
            Desconectar_BD()
            Return SALIDA
        Else
            ' MessageBox.Show("La conexion ha fallado!!")
        End If
        Return ""
    End Function

    Public Function EjecutarConsulta(ByVal Tabla As String, ByVal SentenciaSQL As String) As DataSet
        Dim ds As New DataSet

        Me.po_Comando = New SqlCommand
        Me.po_Comando.CommandType = CommandType.Text
        Me.po_Comando.CommandText = SentenciaSQL
        Me.po_Comando.CommandTimeout = 200

        Me.po_Adaptador = New SqlDataAdapter
        If Conectar_BD() = True Then ' conectando BD
            po_Comando.Connection = po_Conexion
            Me.po_Adaptador.SelectCommand = po_Comando
            Try
                po_Adaptador.Fill(ds, Tabla)
            Catch EX As Exception
                'MsgError("Error en expresion!: " & EX.Message)
                'MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()
            'Else
            '    MessageBox.Show("La conexion ha fallado!!")
        End If
        Return ds
    End Function



    Public Function Proc_BindSource(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer) As BindingSource
        Dim lo_ds As New BindingSource, Aind As Integer
        Dim lo_params As SqlParameter
        Dim lo_comandoProc As SqlCommand
        lo_comandoProc = New SqlCommand
        Dim po_Adaptador As New SqlDataAdapter

        lo_comandoProc.CommandType = CommandType.StoredProcedure
        lo_comandoProc.CommandTimeout = 120
        lo_comandoProc.CommandText = TxtProc
        If nParametro <> 0 Then
            For Aind = 1 To nParametro
                lo_params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 200)
                lo_params.Direction = ParameterDirection.Input
                lo_comandoProc.Parameters.Add(lo_params)
                If IsNothing(vParametro(Aind - 1)) = True Then
                    lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                Else
                    lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                End If
            Next
        End If
        If Conectar_BD() = True Then ' conectando BD
            lo_comandoProc.Connection = po_Conexion

            Try
                Dim dread As SqlDataReader
                dread = lo_comandoProc.ExecuteReader()
                lo_ds.DataSource = dread
            Catch EX As Exception
                'MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()
            'Else
            '    MessageBox.Show("La conexion ha fallado!!")
        End If


        'po_Adaptador.Fill(lo_ds, "Tabla" & IIf(Len(TxtProc) > 5, TxtProc.Substring(4, Len(TxtProc) - 5), TxtProc))

        Return lo_ds
    End Function
    Public Function Proc_DataReader(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer) As DataTable
        Dim Aind As Integer
        Dim lo_params As SqlParameter
        Dim lo_comandoProc As SqlCommand
        lo_comandoProc = New SqlCommand
        Dim dt As New DataTable()
        Dim dread As SqlDataReader = Nothing
        lo_comandoProc.CommandType = CommandType.StoredProcedure
        lo_comandoProc.CommandTimeout = 120
        lo_comandoProc.CommandText = TxtProc
        If nParametro <> 0 Then
            For Aind = 1 To nParametro
                lo_params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 200)
                lo_params.Direction = ParameterDirection.Input
                lo_comandoProc.Parameters.Add(lo_params)
                If IsNothing(vParametro(Aind - 1)) = True Then
                    lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                Else
                    lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                End If
            Next
        End If
        If Conectar_BD() = True Then ' conectando BD
            lo_comandoProc.Connection = po_Conexion

            Try
                dread = lo_comandoProc.ExecuteReader()



                dt.Load(dread)

            Catch EX As Exception
                'MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()
        End If
        Return dt
    End Function


    Public Function ProcedureSQL(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer) As DataSet
        Dim lo_ds As New DataSet, Aind As Integer
        Dim lo_params As SqlParameter
        Dim lo_comandoProc As SqlCommand
        lo_comandoProc = New SqlCommand
        Dim po_Adaptador As New SqlDataAdapter

        lo_comandoProc.CommandType = CommandType.StoredProcedure
        lo_comandoProc.CommandTimeout = 120
        lo_comandoProc.CommandText = TxtProc

        For Aind = 1 To nParametro
            lo_params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 5000000)
            lo_params.Direction = ParameterDirection.Input
            lo_comandoProc.Parameters.Add(lo_params)
            If SqlDbType.Date.Equals(typeParam(Aind - 1)) = True Then
                If IsNothing(vParametro(Aind - 1)) = True Then
                    lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                Else
                    If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If

            Else
                If SqlDbType.DateTime.Equals(typeParam(Aind - 1)) = True Then
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                            lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                        Else
                            lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                        End If
                    End If
                Else
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If
            End If
        Next
        If Conectar_BD() = True Then ' conectando BD
            lo_comandoProc.Connection = po_Conexion
            po_Adaptador.SelectCommand = lo_comandoProc
            Try
                po_Adaptador.Fill(lo_ds, "T" & TxtProc)
            Catch EX As Exception
                'MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()
            'Else
            '    MessageBox.Show("La conexion ha fallado!!")
        End If


        'po_Adaptador.Fill(lo_ds, "Tabla" & IIf(Len(TxtProc) > 5, TxtProc.Substring(4, Len(TxtProc) - 5), TxtProc))

        Return lo_ds
    End Function
    ''' <summary>
    ''' Retorna un data table para anidarlo a un data set
    ''' </summary>
    ''' <param name="TxtProc"></param>
    ''' <param name="sParametro"></param>
    ''' <param name="vParametro"></param>
    ''' <param name="typeParam"></param>
    ''' <param name="nParametro"></param>
    ''' <param name="esdatatable"></param>
    ''' <returns></returns>
    Public Function ProcedureSQL(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer, esdatatable As Boolean) As DataTable
        Dim lo_ds As New DataTable, Aind As Integer
        Dim lo_params As SqlParameter
        Dim lo_comandoProc As SqlCommand
        lo_comandoProc = New SqlCommand
        Dim po_Adaptador As New SqlDataAdapter

        lo_comandoProc.CommandType = CommandType.StoredProcedure
        lo_comandoProc.CommandTimeout = 120
        lo_comandoProc.CommandText = TxtProc

        For Aind = 1 To nParametro
            lo_params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 5000000)
            lo_params.Direction = ParameterDirection.Input
            lo_comandoProc.Parameters.Add(lo_params)
            If SqlDbType.Date.Equals(typeParam(Aind - 1)) = True Then
                If IsNothing(vParametro(Aind - 1)) = True Then
                    lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                Else
                    If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If

            Else
                If SqlDbType.DateTime.Equals(typeParam(Aind - 1)) = True Then
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                            lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                        Else
                            lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                        End If
                    End If
                Else
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        lo_comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        lo_comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If
            End If
        Next
        If Conectar_BD() = True Then ' conectando BD
            lo_comandoProc.Connection = po_Conexion
            po_Adaptador.SelectCommand = lo_comandoProc
            Try
                po_Adaptador.Fill(lo_ds)
            Catch EX As Exception
                ' MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()

        End If
        Return lo_ds
    End Function
    Public Function FuncionProc(ByVal TxtProc As String, ByVal sparam As String, ByVal valor As Object) As DataSet
        Dim lo_ds As New DataSet
        Dim lo_comandoProc As New SqlCommand(TxtProc, po_Conexion)
        Dim lo_params As SqlParameter
        lo_comandoProc.CommandType = CommandType.StoredProcedure
        lo_comandoProc.CommandTimeout = 120

        lo_params = lo_comandoProc.Parameters.Add(sparam, SqlDbType.VarChar)
        lo_params.Direction = ParameterDirection.Input
        lo_params.Value = valor

        po_Adaptador.SelectCommand = lo_comandoProc
        po_Adaptador.Fill(lo_ds, "Tabla" & valor.ToString)
        Return lo_ds
    End Function
    Public Function FuncionProc(ByVal TxtProc As String) As DataSet
        Dim lo_ds As New DataSet
        If Conectar_BD() = True Then ' conectando BD
            Dim lo_comandoProc As New SqlCommand(TxtProc, po_Conexion)
            lo_comandoProc.CommandType = CommandType.StoredProcedure
            lo_comandoProc.CommandTimeout = 120
            Dim po_Adaptador As New SqlDataAdapter
            po_Adaptador.SelectCommand = lo_comandoProc
            Try
                po_Adaptador.Fill(lo_ds, "Tabla" & TxtProc)
            Catch EX As Exception
                ' MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End Try
            Desconectar_BD()
        Else
            ' MessageBox.Show("La conexion ha fallado!!")
        End If

        Return lo_ds
    End Function
    Public Sub EjecutarProcedure(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer)
        Dim ds As New DataSet, Aind As Integer
        Dim params As SqlParameter
        Dim comandoProc As SqlCommand = Nothing
        comandoProc = New SqlCommand
        comandoProc.CommandType = CommandType.StoredProcedure
        comandoProc.CommandTimeout = 120
        comandoProc.CommandText = TxtProc
        Dim obj As New Object
        For Aind = 1 To nParametro
            params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 5000000)
            params.Direction = ParameterDirection.Input
            comandoProc.Parameters.Add(params)
            If SqlDbType.Date.Equals(typeParam(Aind - 1)) = True Then
                If IsNothing(vParametro(Aind - 1)) = True Then
                    comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                Else
                    If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                        comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If

            Else
                If SqlDbType.DateTime.Equals(typeParam(Aind - 1)) = True Then
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        If vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a. m." Or vParametro(Aind - 1).ToString = "01/01/0001 12:00:00 a.m." Or vParametro(Aind - 1).ToString = "01/01/1900 12:00:00 a.m." Then
                            comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                        Else
                            comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                        End If
                    End If
                Else
                    If IsNothing(vParametro(Aind - 1)) = True Then
                        comandoProc.Parameters(Aind - 1).Value = DBNull.Value
                    Else
                        comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
                    End If
                End If
            End If
        Next
        If Conectar_BD() = True Then ' conectando BD
            comandoProc.Connection = po_Conexion
            Try

                comandoProc.ExecuteNonQuery()
                '  obj = {0, "Se realizó con exito"}
            Catch EX As SqlException
                ' MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' obj = {EX.Number, EX.Message, EX.Procedure}
            End Try
            Desconectar_BD()
        Else
            ' MessageBox.Show("La conexion ha fallado!!")
        End If

    End Sub


    ''' <summary>
    ''' Retorna un Valor entero
    ''' </summary>
    ''' <param name="TxtProc"></param>
    ''' <param name="sParametro"></param>
    ''' <param name="vParametro"></param>
    ''' <param name="typeParam"></param>
    ''' <param name="nParametro"></param>
    ''' <returns></returns>
    Public Function procedimiento_escalar(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer) As Integer
        Dim Aind As Integer
        Dim params As SqlParameter
        Dim comandoProc As SqlCommand = Nothing
        comandoProc = New SqlCommand
        comandoProc.Connection = po_Conexion
        comandoProc.CommandType = CommandType.StoredProcedure
        comandoProc.CommandTimeout = 120
        comandoProc.CommandText = TxtProc
        For Aind = 1 To nParametro
            params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 250)
            params.Direction = ParameterDirection.Input
            comandoProc.Parameters.Add(params)
            If IsNothing(vParametro(Aind - 1)) = True Then
                comandoProc.Parameters(Aind - 1).Value = DBNull.Value
            Else
                comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
            End If


        Next Aind
        params = New SqlParameter("@Resultado", SqlDbType.Int, 250)
        params.Direction = ParameterDirection.Output
        comandoProc.Parameters.Add(params)

        If Conectar_BD() = True Then ' conectando BD
            comandoProc.Connection = po_Conexion
            Try
                comandoProc.ExecuteNonQuery()
            Catch EX As SqlException
                ' MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Desconectar_BD()
        Else
            ' MessageBox.Show("La conexion ha fallado!!")
        End If
        Return CInt(comandoProc.Parameters("@Resultado").Value)
    End Function
    Public Function procedimiento_escalar(ByVal TxtProc As String, ByVal sParametro As Object, ByVal vParametro As Object, ByVal typeParam As Object, ByVal nParametro As Integer, Optional esstring As Boolean = False) As String
        Dim Aind As Integer
        Dim params As SqlParameter
        Dim comandoProc As SqlCommand = Nothing
        comandoProc = New SqlCommand
        comandoProc.Connection = po_Conexion
        comandoProc.CommandType = CommandType.StoredProcedure
        comandoProc.CommandTimeout = 120
        comandoProc.CommandText = TxtProc
        For Aind = 1 To nParametro
            params = New SqlParameter(sParametro(Aind - 1).ToString, typeParam(Aind - 1), 250)
            params.Direction = ParameterDirection.Input
            comandoProc.Parameters.Add(params)
            If IsNothing(vParametro(Aind - 1)) = True Then
                comandoProc.Parameters(Aind - 1).Value = DBNull.Value
            Else
                comandoProc.Parameters(Aind - 1).Value = vParametro(Aind - 1)
            End If


        Next Aind
        params = New SqlParameter("@Resultado", SqlDbType.VarChar, 250)
        params.Direction = ParameterDirection.Output
        comandoProc.Parameters.Add(params)

        If Conectar_BD() = True Then ' conectando BD
            comandoProc.Connection = po_Conexion
            Try
                comandoProc.ExecuteNonQuery()
            Catch EX As SqlException
                '  MessageBox.Show("Error en expresión!:" & EX.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Desconectar_BD()
        Else
            ' MessageBox.Show("La conexion ha fallado!!")
        End If
        Return (comandoProc.Parameters("@Resultado").Value)
    End Function
#End Region

End Class
