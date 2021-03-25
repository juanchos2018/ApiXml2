Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms

Public Class ClsEstilo

    Dim lo_PanelBotones As New Panel
    Dim lo_Botones As New ToolStrip
    Dim lo_Botones_Pie As New ToolStrip
    Dim lo_form As Form
    Dim isnavigator As Boolean = False
    Public Enum TipoButton
        Predeterminado
        Lista
        Operacion
        Dialogo
        NEGEC
        NEGECBS
        NGS
        NS
        GS
    End Enum

    Public Enum Estado
        Nuevo
        Editar
        Ver
    End Enum

    ''' <summary>
    ''' Lista de Botones en el ToolStrip
    ''' </summary>
    Public Enum BtnLista
        CmdAgregar
        CmdModificar
        CmdEliminar
        CmdBuscar
        CmdGuardar
        CmdSalir
        CmdCancelar
        CmdEditar
        CmdNuevo
    End Enum


    ''--------------- EVENTOS ***********

    Private m_events As EventHandlerList
    Private Declare Auto Function SendMessage Lib "user32.dll" (hWnd As IntPtr, msg As Integer, wParam As Integer, lParam As String) As Integer

#Region "EstilosControl"


    Public Sub EstiloLabel(ByVal objLabel As Label)
        If objLabel.Tag <> "NO" Then
            objLabel.BackColor = Color.Transparent
            objLabel.FlatStyle = FlatStyle.Flat
        End If
        If objLabel.Name = "lblTitulo" Then
            objLabel.BackColor = Color.Transparent
            objLabel.ForeColor = Color.White
        End If
        If objLabel.Name = "lblTituloDlg" Then
            objLabel.ForeColor = Color.White
            AddHandler objLabel.Paint, AddressOf label_Paint
        End If


    End Sub
    Public Sub EstiloDataGrid(ByVal objDataGrid As DataGridView)
        objDataGrid.Font = New Font("Calibri", 9.0!, FontStyle.Regular)
        'objDataGrid.Font = New Font("Arial", 8.0!, FontStyle.Regular)
        If objDataGrid.Name.Contains("free") = True Then
            objDataGrid.ReadOnly = False
        Else
            objDataGrid.ReadOnly = True
        End If
        objDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'applyGridTheme(objDataGrid)
        objDataGrid.AllowUserToAddRows = False
        objDataGrid.AllowUserToDeleteRows = False
        objDataGrid.BackgroundColor = System.Drawing.SystemColors.Window
        objDataGrid.AllowUserToResizeColumns = True
        objDataGrid.RowTemplate.Height = 20
        objDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        objDataGrid.RowHeadersWidth = 25
        '  AddHandler objDataGrid.CellPainting, AddressOf Grid_CellPainting
    End Sub
#Region "Estilo Grilla Excel"


    Public Shared anyRight As ContentAlignment = (ContentAlignment.BottomRight _
                Or (ContentAlignment.MiddleRight Or ContentAlignment.TopRight))

    Public Shared anyTop As ContentAlignment = (ContentAlignment.TopRight _
                Or (ContentAlignment.TopCenter Or ContentAlignment.TopLeft))

    Public Shared anyBottom As ContentAlignment = (ContentAlignment.BottomRight _
                Or (ContentAlignment.BottomCenter Or ContentAlignment.BottomLeft))

    Public Shared anyCenter As ContentAlignment = (ContentAlignment.BottomCenter _
                Or (ContentAlignment.MiddleCenter Or ContentAlignment.TopCenter))

    Public Shared anyMiddle As ContentAlignment = (ContentAlignment.MiddleRight _
                Or (ContentAlignment.MiddleCenter Or ContentAlignment.MiddleLeft))
    Private Sub TekenAchtergrond(ByVal g As Graphics, ByVal obj As Image, ByVal r As Rectangle, ByVal index As Integer)
        If (obj Is Nothing) Then
            Return
        End If
        Dim oWidth As Integer = obj.Width
        Dim lr As Rectangle = Rectangle.FromLTRB(0, 0, 0, 0)
        Dim r2 As Rectangle
        Dim r1 As Rectangle
        Dim x As Integer = ((index - 1) _
                    * oWidth)
        Dim y As Integer = 0
        Dim x1 As Integer = r.Left
        Dim y1 As Integer = r.Top
        If ((r.Height > obj.Height) _
                    AndAlso (r.Width <= oWidth)) Then
            r1 = New Rectangle(x, y, oWidth, lr.Top)
            r2 = New Rectangle(x1, y1, r.Width, lr.Top)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            r1 = New Rectangle(x, (y + lr.Top), oWidth, (obj.Height _
                            - (lr.Top - lr.Bottom)))
            r2 = New Rectangle(x1, (y1 + lr.Top), r.Width, (r.Height _
                            - (lr.Top - lr.Bottom)))
            If ((lr.Top + lr.Bottom) _
                        = 0) Then
                r1.Height = (r1.Height - 1)
            End If
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            r1 = New Rectangle(x, (y _
                            + (obj.Height - lr.Bottom)), oWidth, lr.Bottom)
            r2 = New Rectangle(x1, (y1 _
                            + (r.Height - lr.Bottom)), r.Width, lr.Bottom)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
        ElseIf ((r.Height <= obj.Height) _
                    AndAlso (r.Width > oWidth)) Then
            r1 = New Rectangle(x, y, lr.Left, obj.Height)
            r2 = New Rectangle(x1, y1, lr.Left, r.Height)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            r1 = New Rectangle((x + lr.Left), y, (oWidth _
                            - (lr.Left - lr.Right)), obj.Height)
            r2 = New Rectangle((x1 + lr.Left), y1, (r.Width _
                            - (lr.Left - lr.Right)), r.Height)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            r1 = New Rectangle((x _
                            + (oWidth - lr.Right)), y, lr.Right, obj.Height)
            r2 = New Rectangle((x1 _
                            + (r.Width - lr.Right)), y1, lr.Right, r.Height)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
        ElseIf ((r.Height <= obj.Height) _
                    AndAlso (r.Width <= oWidth)) Then
            r1 = New Rectangle(((index - 1) _
                            * oWidth), 0, oWidth, (obj.Height - 1))

            g.DrawImage(obj, New Rectangle(x1, y1, r.Width, r.Height), r1, GraphicsUnit.Pixel)
        ElseIf ((r.Height > obj.Height) _
                    AndAlso (r.Width > oWidth)) Then
            'top-left
            r1 = New Rectangle(x, y, lr.Left, lr.Top)
            r2 = New Rectangle(x1, y1, lr.Left, lr.Top)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'top-bottom
            r1 = New Rectangle(x, (y _
                            + (obj.Height - lr.Bottom)), lr.Left, lr.Bottom)
            r2 = New Rectangle(x1, (y1 _
                            + (r.Height - lr.Bottom)), lr.Left, lr.Bottom)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'left
            r1 = New Rectangle(x, (y + lr.Top), lr.Left, (obj.Height _
                            - (lr.Top - lr.Bottom)))
            r2 = New Rectangle(x1, (y1 + lr.Top), lr.Left, (r.Height _
                            - (lr.Top - lr.Bottom)))
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'top
            r1 = New Rectangle((x + lr.Left), y, (oWidth _
                            - (lr.Left - lr.Right)), lr.Top)
            r2 = New Rectangle((x1 + lr.Left), y1, (r.Width _
                            - (lr.Left - lr.Right)), lr.Top)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'right-top
            r1 = New Rectangle((x _
                            + (oWidth - lr.Right)), y, lr.Right, lr.Top)
            r2 = New Rectangle((x1 _
                            + (r.Width - lr.Right)), y1, lr.Right, lr.Top)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'Right
            r1 = New Rectangle((x _
                            + (oWidth - lr.Right)), (y + lr.Top), lr.Right, (obj.Height _
                            - (lr.Top - lr.Bottom)))
            r2 = New Rectangle((x1 _
                            + (r.Width - lr.Right)), (y1 + lr.Top), lr.Right, (r.Height _
                            - (lr.Top - lr.Bottom)))
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'right-bottom
            r1 = New Rectangle((x _
                            + (oWidth - lr.Right)), (y _
                            + (obj.Height - lr.Bottom)), lr.Right, lr.Bottom)
            r2 = New Rectangle((x1 _
                            + (r.Width - lr.Right)), (y1 _
                            + (r.Height - lr.Bottom)), lr.Right, lr.Bottom)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'bottom
            r1 = New Rectangle((x + lr.Left), (y _
                            + (obj.Height - lr.Bottom)), (oWidth _
                            - (lr.Left - lr.Right)), lr.Bottom)
            r2 = New Rectangle((x1 + lr.Left), (y1 _
                            + (r.Height - lr.Bottom)), (r.Width _
                            - (lr.Left - lr.Right)), lr.Bottom)
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
            'Center
            r1 = New Rectangle((x + lr.Left), (y + lr.Top), (oWidth _
                            - (lr.Left - lr.Right)), (obj.Height _
                            - (lr.Top - lr.Bottom)))
            r2 = New Rectangle((x1 + lr.Left), (y1 + lr.Top), (r.Width _
                            - (lr.Left - lr.Right)), (r.Height _
                            - (lr.Top - lr.Bottom)))
            g.DrawImage(obj, r2, r1, GraphicsUnit.Pixel)
        End If
    End Sub

    Private Function HAlignWithin(ByVal alignThis As Size, ByVal withinThis As Rectangle, ByVal align As ContentAlignment) As Rectangle
        If ((align And anyRight) _
                    <> CType(0, ContentAlignment)) Then
            withinThis.X = (withinThis.X _
                        + (withinThis.Width - alignThis.Width))
        ElseIf ((align And anyCenter) _
                    <> CType(0, ContentAlignment)) Then
            withinThis.X = (withinThis.X _
                        + (((withinThis.Width - alignThis.Width) _
                        + 1) _
                        / 2))
        End If
        withinThis.Width = alignThis.Width
        Return withinThis
    End Function

    Private Function VAlignWithin(ByVal alignThis As Size, ByVal withinThis As Rectangle, ByVal align As ContentAlignment) As Rectangle
        If ((align And anyBottom) _
                    <> CType(0, ContentAlignment)) Then
            withinThis.Y = (withinThis.Y _
                        + (withinThis.Height - alignThis.Height))
        ElseIf ((align And anyMiddle) _
                    <> CType(0, ContentAlignment)) Then
            withinThis.Y = (withinThis.Y _
                        + (((withinThis.Height - alignThis.Height) _
                        + 1) _
                        / 2))
        End If
        withinThis.Height = alignThis.Height
        Return withinThis
    End Function
    Private Sub Grid_CellPainting(ByVal sender As DataGridView, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)
        If e.RowIndex = -1 Then
            Dim img As Image
            img = My.Resources.Header
            TekenAchtergrond(e.Graphics, img, e.CellBounds, 1)
            Dim format1 As StringFormat
            format1 = New StringFormat
            format1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show
            Dim ef1 As SizeF = e.Graphics.MeasureString(e.Value, sender.Font, New SizeF(CType(e.CellBounds.Width, Single), CType(e.CellBounds.Height, Single)), format1)

            Dim txts As Size
            txts = Drawing.Size.Empty

            txts = Drawing.Size.Ceiling(ef1)
            e.CellBounds.Inflate(-4, -4)

            Dim txtr As Rectangle = e.CellBounds
            txtr = HAlignWithin(txts, txtr, ContentAlignment.MiddleCenter)
            txtr = VAlignWithin(txts, txtr, ContentAlignment.MiddleCenter)
            Dim brush2 As Brush
            format1 = New StringFormat
            format1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show

            brush2 = New SolidBrush(Color.FromArgb(21, 66, 139))

            e.Graphics.DrawString(e.Value, sender.Font, brush2, CType(txtr, RectangleF), format1)
            brush2.Dispose()
            Dim recBorder As New Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1)
            e.Graphics.DrawRectangle(Pens.LightSlateGray, recBorder)

            e.Handled = True
        End If
    End Sub
#End Region

    Public Sub EstiloMTextBox(ByVal objTextBox As MaskedTextBox)
        objTextBox.BackColor = System.Drawing.SystemColors.Window
        objTextBox.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Public Sub EstiloComboBox(ByVal objComboBox As ComboBox)
        objComboBox.FlatStyle = FlatStyle.Standard
        If objComboBox.Tag = "NO" Then
            objComboBox.DropDownStyle = ComboBoxStyle.DropDown
        Else
            objComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        objComboBox.ForeColor = Color.Black
        objComboBox.MaxDropDownItems = 20
    End Sub
    Public Sub EstiloTextBox(ByVal objTextBox As TextBox)
        objTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        SetCue(objTextBox, objTextBox.Tag)
        'If UCase(objTextBox.Name).Contains("CUENTA") = True Then
        '    autocomplete(objTextBox)
        'End If

    End Sub
    'Private Sub autocomplete(txt As TextBox)
    '    Dim dt As New DataTable
    '    Dim cad As String = " Select rtrim(IdCuenta) as IdCuenta from plancuenta where IdTipoCuenta<>'X' "
    '    '   cad += " union all "
    '    '   cad += " Select rtrim(Descripcion) as IdCuenta,idcuenta as id from plancuenta where IdTipoCuenta<>'X' ) as m order by id "
    '    '" Select rtrim(IdCuenta) as IdCuenta from plancuenta where IdTipoCuenta<>'X' ORDER BY 1 
    '    dt = Sql.EjecutarConsulta("x", cad).Tables(0)
    '    ' dt = conex.EjecutarConsulta("x", " Select (cast(RTrim(IdCuenta)As Char(12))+'|  '+ rtrim(descripcion)) as IdCuenta from plancuenta where IdTipoCuenta<>'X' ORDER BY 1 ").Tables(0)
    '    'autocompletArt(dtart, txt)
    '    Dim lst As New List(Of String)
    '    Dim MySource As New AutoCompleteStringCollection()
    '    For Each lista As DataRow In dt.Rows
    '        lst.Add(lista.Item("IdCuenta").ToString())
    '    Next
    '    MySource.AddRange(lst.ToArray)
    '    ' txt.AutoCompleteMode = AutoSizeMode.GrowOnly
    '    txt.AutoCompleteCustomSource = MySource
    '    txt.AutoCompleteMode = AutoCompleteMode.Suggest
    '    txt.AutoCompleteSource = AutoCompleteSource.CustomSource


    'End Sub

    Public Sub EstiloButton_nuevo(ByVal objButton As Button)
        If objButton.Tag <> "NO" Then
            If UCase(objButton.Name) = UCase("btnGuardar") Or UCase(objButton.Name) = UCase("CmdGuardar") Or UCase(objButton.Name) = UCase("BtnGuardar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.guardar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Guardar"
            End If
            If UCase(objButton.Name) = UCase("BtnEditar") Or UCase(objButton.Name) = UCase("CmdEditar") Or UCase(objButton.Name) = UCase("CmdModificar") Or UCase(objButton.Name) = UCase("btnGenerar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.Editar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Editar"
            End If
            If UCase(objButton.Name) = UCase("BtnEliminar") Or UCase(objButton.Name) = UCase("CmdEliminar") Or UCase(objButton.Name) = UCase("CmdAnular") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.eliminar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("btnCancelar") Or UCase(objButton.Name) = UCase("BtnCancelar") Or UCase(objButton.Name) = UCase("CmdCancelar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.salir16x16
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("CmdNuevo") Or UCase(objButton.Name) = UCase("BtnNuevo") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.agregar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("CmdBuscar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.buscar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("CmdCerrar") Or UCase(objButton.Name) = UCase("BtnSalir") Or UCase(objButton.Name) = UCase("CmdSalir") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.salir16x16
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("CmdVer") Or UCase(objButton.Name) = UCase("btnver") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.ver
                objButton.Size = New System.Drawing.Size(81, 49)
            End If
            If UCase(objButton.Name) = UCase("CmdImprimir") Or UCase(objButton.Name) = UCase("BtnImprimir") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.visualizar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Pantalla"
            End If
            If UCase(objButton.Name) = UCase("CmdAtras") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.atras16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Atras"
            End If
            If UCase(objButton.Name) = UCase("BtnExcel") Then
                objButton.Image = Global.CapaEstilo.My.Resources.Resources.excel16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Excel"
            End If
            If UCase(objButton.Name) = UCase("BtnAceptar") Or UCase(objButton.Name) = UCase("CmdAceptar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.aceptar16x16
                objButton.Size = New System.Drawing.Size(81, 49)
                objButton.Text = "&Aceptar"
            End If
            If UCase(objButton.Name) = UCase("BtnAgregar") Or UCase(objButton.Name) = UCase("CmdAgregar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.agregar16x16
                objButton.Size = New System.Drawing.Size(80, 30)
                objButton.Text = "&Agregar"
            End If
            If UCase(objButton.Name) = UCase("BtnQuitar") Or UCase(objButton.Name) = UCase("CmdQuitar") Then
                objButton.Image = Global.CapaEstilo.My.Resources.eliminar16x16
                objButton.Size = New System.Drawing.Size(80, 30)
                objButton.Text = "&Quitar"
            End If
            objButton.Size = New System.Drawing.Size(80, 38)
            objButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            ''objButton.BackgroundImage = My.Resources.Header 
            objButton.UseVisualStyleBackColor = True
        End If
    End Sub


#End Region


#Region "Evento_y_focos"
    ''' <summary>
    ''' Este metodo asigna los tabuladores a cada control
    ''' </summary>
    ''' <param name="control"></param>
    Public Sub Control_Evento(ByVal control As Control)
        'Recorremos con un ciclo for each cada control que hay en la colección Controls
        For Each contHijo As Control In control.Controls
            'Preguntamos si el control tiene uno o mas controles dentro de l mismo con la propiedad 'HasChildren'
            'Si el control tiene 1 o más controles, entonces llamamos al procedimiento de forma recursiva, para que siga
            'recorriendo los demás controles
            If contHijo.HasChildren Then
                Me.Control_Evento(contHijo)
            End If
            'Aqui va la lógica de lo queramos hacer, en mi ejemplo, voy a pintar de color azul el fondo de todos los controles
            ' contHijo.BackColor = Color.Blue
            If (TypeOf contHijo Is TextBox) Or (TypeOf contHijo Is MaskedTextBox) Or (TypeOf contHijo Is ComboBox) Or (TypeOf contHijo Is CheckBox) Or (TypeOf contHijo Is RadioButton) Or (TypeOf contHijo Is DateTimePicker) Then
                AddHandler contHijo.GotFocus, AddressOf Enfocar
                AddHandler contHijo.LostFocus, AddressOf DesEnfocar
                AddHandler contHijo.EnabledChanged, AddressOf ColorDisabled
                AddHandler contHijo.KeyDown, AddressOf PasarFoco
                'AddHandler AForm.Controls(i).KeyPress, AddressOf Mayuscula
                AddHandler contHijo.Click, AddressOf Enfocar
            End If

        Next
    End Sub

    Public Sub Enfocar(ByVal sender As Object, ByVal e As System.EventArgs)
        'sender.BackColor = Color.PaleGreen
        If (TypeOf sender Is CheckBox) = False Then
            'sender.BackColor = Color.AliceBlue
            sender.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        End If
        '
        If (TypeOf sender Is TextBox) Or (TypeOf sender Is MaskedTextBox) Then
            '  sender.SelectAll()
            '  sender.ShortcutsEnabled = True
        End If

    End Sub
    Public Sub DesEnfocar(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.BackColor = Color.Empty
    End Sub
    '---se modifico al publico
    Private Sub PasarFoco(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)  ' si es la tecla enter ...
        If e.KeyCode = Keys.Enter Then ' envía la pulsación de tecla Tab y pasa el foco a la siguiente caja de texto
            If (sender.Tag <> "NO") Then
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Private Sub Mayuscula(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub
    Public Sub ColorDisabled(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.BackColor = Color.WhiteSmoke
    End Sub
#End Region

#Region "Marca de agua textbox"
    Public Sub SetCue(textBox As TextBox, cue As String)
        SendMessage(textBox.Handle, 5377, 0, cue)
    End Sub
    Public Sub ClearCue(textBox As TextBox)
        SendMessage(textBox.Handle, 5377, 0, String.Empty)
    End Sub

#End Region
#Region "Fechas default"
    Public Function FechaInicial() As Date
        Dim fechaI As New DateTime(Date.Now.Year, Date.Now.Month, 1)
        Return fechaI
    End Function
    Public Function FechaFinal() As Date
        Dim fechaF = New DateTime(Date.Now.Year, Date.Now.Month, DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month))
        Return fechaF
    End Function
    Public Function FechaActual() As Date
        Return Now.Date
    End Function
#End Region



#Region "EstilosControl"

    Private Sub HabilitarRegistro(ByVal control As Control, ByVal IsModificar As Boolean)
        Dim i As Integer, ATextbox As TextBox, aCombo As ComboBox, AMtextbox As MaskedTextBox, Buton As Button, grid As DataGridView, rbtn As RadioButton, dtpck As DateTimePicker, grx As GroupBox
        For Each contHijo As Control In control.Controls
            If TypeOf contHijo Is TextBox Then
                ATextbox = contHijo
                ATextbox.Enabled = IsModificar
            End If
            If TypeOf contHijo Is ComboBox Then
                aCombo = contHijo
                aCombo.Enabled = IsModificar
            End If
            If TypeOf contHijo Is MaskedTextBox Then
                AMtextbox = contHijo
                AMtextbox.Enabled = IsModificar
            End If
            If TypeOf contHijo Is Button Then
                Buton = contHijo
                Buton.Enabled = IsModificar
            End If
            If TypeOf contHijo Is DataGridView Then
                grid = contHijo
                If grid.Tag <> "NO" Then
                    grid.Enabled = IsModificar
                End If
            End If
            If TypeOf contHijo Is RadioButton Then
                rbtn = contHijo
                rbtn.Enabled = IsModificar
            End If

            If TypeOf contHijo Is DateTimePicker Then
                dtpck = contHijo
                dtpck.Enabled = IsModificar
            End If
            If TypeOf contHijo Is GroupBox Then
                grx = contHijo
                grx.Enabled = IsModificar
            End If
            If contHijo.HasChildren Then

                HabilitarRegistro(contHijo, IsModificar)
            End If
        Next
    End Sub

    '------------------------------------------
    Public Custom Event Ingresar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Ingresar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Ingresar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Ingresar"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Editar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Editar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Editar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Editar"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Guardar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Guardar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Guardar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Guardar"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event

    Public Custom Event Cancelar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Cancelar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Cancelar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Cancelar"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Eliminar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Eliminar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Eliminar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Eliminar"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Imprimir_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Imprimir", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Imprimir", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Imprimir"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Nuevo_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Nuevo", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Nuevo", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Nuevo"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event
    Public Custom Event Ver_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Ver", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Ver", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Ver"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event

    Public Custom Event Buscar_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Buscar", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Buscar", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Buscar"), EventHandler).Invoke(sender, ea)
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event

    Public Custom Event Salir_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Salir", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Salir", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)

            If m_events IsNot Nothing Then CType(m_events("Click_Salir"), EventHandler).Invoke(sender, ea)
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event
    Public Custom Event Primero_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Primero", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Primero", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Primero"), EventHandler).Invoke(sender, ea)
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event

    Private EventHandlerListq As New ArrayList
    Public Custom Event Siguiente_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria

        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then
                m_events = New EventHandlerList
            End If
            m_events.AddHandler("Click_Siguiente", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Siguiente", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            '   If m_events IsNot Nothing Then
            '  CType(m_events("Click_Siguiente"), EventHandler).Invoke(sender, ea)
            '  End If
            If m_events IsNot Nothing Then
                CType(m_events("Click_Siguiente"), EventHandler).Invoke(sender, ea)
            End If
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event
    Public Custom Event Atras_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Atras", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Atras", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Atras"), EventHandler).Invoke(sender, ea)
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event
    Public Custom Event Ultimo_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Ultimo", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Ultimo", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Ultimo"), EventHandler).Invoke(sender, ea)
            'For Each handler As EventHandler In EventHandlerListq
            '    If handler IsNot Nothing Then
            '        handler.BeginInvoke(sender, ea, Nothing, Nothing)
            '    End If
            'Next
        End RaiseEvent
    End Event
    Public Custom Event Excel_Click As EventHandler
        '*** se crea solo cuando se agrega EventHandler, que es más eficaz en cuanto a la memoria
        AddHandler(ByVal value As EventHandler)
            If m_events Is Nothing Then m_events = New EventHandlerList
            m_events.AddHandler("Click_Excel", value) ' Crea el almacén a petición. 
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            m_events.RemoveHandler("Click_Excel", value)
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal ea As EventArgs)
            If m_events IsNot Nothing Then CType(m_events("Click_Excel"), EventHandler).Invoke(sender, ea)
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Envia opcion de aceptar o rechar el guardado de los botones
    ''' </summary>
    Public DialogoResultado As DialogResult

    Public Sub BtnBotones(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case CType(sender, ToolStripButton).Name
            Case "CmdNuevo"
                RaiseEvent Ingresar_Click(Me, EventArgs.Empty)
                'Me.HabilitarRegistro(lo_form, True)
                'HabilitarBotones(False)
                'If opcionButton = True Then
                '    MostrarBotonesLista(True)
                'End If
                'If isnavigator = True Then
                '    HabilitarBotones_pie(False)
                'End If
            Case "CmdEditar"
                'HabilitarBotones(False)
                'Me.HabilitarRegistro(lo_form, True)
                'If opcionButton = True Then
                '    MostrarBotonesLista(True)
                'End If
                RaiseEvent Editar_Click(Me, EventArgs.Empty)
                'If isnavigator = True Then
                '    HabilitarBotones_pie(False)
                'End If
            Case "CmdGuardar"
                'DialogoResultado = MessageBox.Show("¿Desea Guardar la Operación?", "Guardar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                'f DialogoResultado = Windows.Forms.DialogResult.Yes Then
                RaiseEvent Guardar_Click(Me, EventArgs.Empty)
                '    If DialogoResultado = Windows.Forms.DialogResult.Yes Then
                'Me.HabilitarRegistro(lo_form, False)
                'HabilitarBotones(True)
                'End If
                'End If

                'If isnavigator = True Then
                '    HabilitarBotones_pie(False)
                'End If
            Case "CmdCancelar"
                RaiseEvent Cancelar_Click(Me, EventArgs.Empty)
                'Me.HabilitarRegistro(lo_form, False)
                'HabilitarBotones(True)
                'If isnavigator = True Then
                '    HabilitarBotones_pie(False)
                'End If
            Case "CmdSalir"
                RaiseEvent Salir_Click(Me, EventArgs.Empty)
            Case "CmdPrimero"
                ' lo_form.CancelButton = sender
                RaiseEvent Primero_Click(Me, EventArgs.Empty)
            Case "CmdSiguiente"
                RaiseEvent Siguiente_Click(Me, EventArgs.Empty)
            Case "CmdAtras"
                RaiseEvent Atras_Click(Me, EventArgs.Empty)
            Case "CmdUltimo"
                RaiseEvent Ultimo_Click(Me, EventArgs.Empty)
            Case "CmdBuscar"
                RaiseEvent Buscar_Click(Me, EventArgs.Empty)
            Case "CmdExcel"
                RaiseEvent Excel_Click(Me, EventArgs.Empty)
            Case "CmdImprimir"
                RaiseEvent Imprimir_Click(Me, EventArgs.Empty)
            Case "CmdAgregar"
                RaiseEvent Nuevo_Click(Me, EventArgs.Empty)
            Case "CmdModificar"
                RaiseEvent Editar_Click(Me, EventArgs.Empty)
            Case "CmdVer"
                RaiseEvent Ver_Click(Me, EventArgs.Empty)
            Case "CmdEliminar"
                ' If MessageBox.Show("Esta seguro Eliminar el registro?", "Eliminar Registro", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                RaiseEvent Eliminar_Click(Me, EventArgs.Empty)
                '    Me.HabilitarRegistro(lo_form, False)
                'End If
                'If isnavigator = True Then
                '    HabilitarBotones_pie(False)
                'End If
        End Select

    End Sub
    Private Sub CrearBoton(ByVal Aboton As ToolStrip, ByVal nombre As String, ByVal texto As String, ByVal nomimag As Bitmap)
        Aboton.AutoSize = False
        Dim btn As New ToolStripButton(texto, Nothing, New EventHandler(AddressOf BtnBotones), nombre)
        Aboton.Height = 30
        '     Aboton.Width = 70
        EstiloToolButton(btn, nomimag)
        Aboton.Items.Add(btn)
        'Dim toolSeparator As New ToolStripSeparator()
        '' Aboton.Height = 30
        'Aboton.Items.Add(toolSeparator)
    End Sub
    Public Property opcionButton As Boolean = False
    Public Sub HabilitarBotones(ByVal opcion As Boolean)
        Try
            'lo_Botones.Items("CmdNuevo").Enabled = opcion
            'lo_Botones.Items("CmdEditar").Enabled = opcion
            'lo_Botones.Items("CmdGuardar").Enabled = Not opcion
            'lo_Botones.Items("CmdCancelar").Enabled = Not opcion
            'lo_Botones.Items("CmdEliminar").Enabled = opcion
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Muestra los botones para una lista en una grilla
    ''' </summary>
    ''' <param name="opcion"></param>
    Public Sub MostrarBotonesLista(ByVal opcion As Boolean)
        Try
            lo_Botones.Items("CmdNuevo").Visible = True
            lo_Botones.Items("CmdEditar").Visible = True
            lo_Botones.Items("CmdEliminar").Visible = True

            lo_Botones.Items("CmdNuevo").Enabled = True
            lo_Botones.Items("CmdEditar").Enabled = True
            lo_Botones.Items("CmdGuardar").Enabled = True
            lo_Botones.Items("CmdCancelar").Enabled = True
            lo_Botones.Items("CmdEliminar").Enabled = True
            If opcion = True Then

                lo_Botones.Items("CmdGuardar").Visible = False
                lo_Botones.Items("CmdVer").Visible = True
                lo_Botones.Items("CmdBuscar").Visible = False
                lo_Botones.Items("CmdExcel").Visible = True
                lo_Botones.Items("CmdImprimir").Visible = True
                lo_Botones.Items("CmdCancelar").Visible = False
                lo_Botones.Items("CmdSalir").Visible = True
            Else
                lo_Botones.Items("CmdGuardar").Visible = True
                lo_Botones.Items("CmdVer").Visible = False
                lo_Botones.Items("CmdBuscar").Visible = True
                lo_Botones.Items("CmdExcel").Visible = False
                lo_Botones.Items("CmdImprimir").Visible = False
                lo_Botones.Items("CmdCancelar").Visible = True
                lo_Botones.Items("CmdSalir").Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub MostrarBotonesDialogo()
        lo_Botones.Items("CmdNuevo").Visible = False
        lo_Botones.Items("CmdEditar").Visible = False
        lo_Botones.Items("CmdEliminar").Visible = False
        lo_Botones.Items("CmdGuardar").Visible = True
        lo_Botones.Items("CmdGuardar").Enabled = True
        lo_Botones.Items("CmdVer").Visible = False
        lo_Botones.Items("CmdBuscar").Visible = True
        lo_Botones.Items("CmdExcel").Visible = False
        lo_Botones.Items("CmdImprimir").Visible = False
        lo_Botones.Items("CmdCancelar").Visible = False
        lo_Botones.Items("CmdSalir").Visible = True
    End Sub

    Private Sub HabilitarBotones_pie(ByVal opcion As Boolean)
        lo_Botones_Pie.Items("CmdPrimero").Enabled = opcion
        lo_Botones_Pie.Items("CmdSiguiente").Enabled = opcion
        lo_Botones_Pie.Items("CmdAtras").Enabled = opcion
        lo_Botones_Pie.Items("CmdUltimo").Enabled = opcion
        lo_Botones_Pie.Items("CmdBuscar").Enabled = opcion
    End Sub
    Private Sub CrearBotones(ABotones As ToolStrip, Optional TipoButton As ClsEstilo.TipoButton = ClsEstilo.TipoButton.Predeterminado)
        Select Case TipoButton
            Case ClsEstilo.TipoButton.Lista
                Me.CrearBoton(ABotones, "CmdAgregar", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdEditar", "&Editar", My.Resources.Editar16x16)
                Me.CrearBoton(ABotones, "CmdVer", "&Ver", My.Resources.ver)
                Me.CrearBoton(ABotones, "CmdEliminar", "E&liminar", My.Resources.eliminar16x16)
                Me.CrearBoton(ABotones, "CmdExcel", "E&xcel", My.Resources.excel16x16)
                Me.CrearBoton(ABotones, "CmdImprimir", "&Imprimir", My.Resources.Print)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.Operacion
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdBuscar", "&Buscar", My.Resources.visualizar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.GS
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.NGS
                Me.CrearBoton(ABotones, "CmdAgregar", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.NS
                Me.CrearBoton(ABotones, "CmdAgregar", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.Dialogo
                Me.CrearBoton(ABotones, "CmdAceptar", "&Aceptar", My.Resources.aceptar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.NEGEC
                Me.CrearBoton(ABotones, "CmdAgregar", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdModificar", "&Editar", My.Resources.Editar16x16)
                Me.CrearBoton(ABotones, "CmdEliminar", "E&liminar", My.Resources.eliminar16x16)
                Me.CrearBoton(ABotones, "CmdBuscar", "&Buscar", My.Resources.visualizar16x16)
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
            Case ClsEstilo.TipoButton.NEGECBS
                Me.CrearBoton(ABotones, "CmdAgregar", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdModificar", "&Editar", My.Resources.Editar16x16)
                Me.CrearBoton(ABotones, "CmdEliminar", "E&liminar", My.Resources.eliminar16x16)
                Me.CrearBoton(ABotones, "CmdBuscar", "&Buscar", My.Resources.visualizar16x16)
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
                Me.CrearBoton(ABotones, "CmdCancelar", "&Cancelar", My.Resources.cancelar16x16)
            Case Else
                Me.CrearBoton(ABotones, "CmdNuevo", "&Nuevo", My.Resources.agregar16x16)
                Me.CrearBoton(ABotones, "CmdEditar", "&Editar", My.Resources.Editar16x16)
                Me.CrearBoton(ABotones, "CmdBuscar", "&Buscar", My.Resources.visualizar16x16)
                Me.CrearBoton(ABotones, "CmdEliminar", "E&liminar", My.Resources.eliminar16x16)
                Me.CrearBoton(ABotones, "CmdGuardar", "&Guardar", My.Resources.guardar16x16)
                Me.CrearBoton(ABotones, "CmdCancelar", "&Cancelar", My.Resources.cancelar16x16)
                Me.CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
        End Select


    End Sub
    Public Sub EstiloToolButton(ByVal objButton As ToolStripButton, ByVal nomImagen As String)
        objButton.CheckState = CheckState.Unchecked
        objButton.ForeColor = Color.DarkBlue
        '        objButton.ForeColor = Color.White
        objButton.Font = New Font("Arial", 8.0!, FontStyle.Bold)
        objButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        objButton.ImageScaling = ToolStripItemImageScaling.None
        If My.Computer.FileSystem.FileExists(Application.StartupPath + nomImagen) = True Then objButton.Image = Bitmap.FromFile(Application.StartupPath + nomImagen)

    End Sub
    Public Sub EstiloToolButton(ByVal objButton As ToolStripButton, ByVal im As Bitmap)
        objButton.CheckState = CheckState.Unchecked
        objButton.ForeColor = Color.DarkBlue
        objButton.Font = New Font("Arial", 8.0!, FontStyle.Regular)
        'objButton.ForeColor = Color.DarkOrange
        objButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        objButton.Image = im
        objButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        objButton.ToolTipText = objButton.Text
        'objButton.AutoSize = False
        'objButton.Height = 25
        'objButton.Width = 50
        'objButton.Height = 65

    End Sub

    Dim botones As String()
    ''' <summary>
    ''' Activa o deshactiva los botones según el nombre de boton que se da clic(Inicio,Nuevo,Editar,Eliminar
    ''' Guardar, Cancelar, Buscar
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="nombre"></param>
    Public Sub ActivarBotones(frm As Form, nombre As String, Optional Onlybutton As Boolean = False)
        If Onlybutton = False Then
            HabilitarRegistro(frm, False)
        End If

        Select Case nombre
            Case "Inicio"
                'botones = New[] { ClsEstilo.BtnLista.CmdEditar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString() };
                'lo_estilo.EnableBtn(this, botones, False);
                botones = {BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), BtnLista.CmdGuardar.ToString(), BtnLista.CmdCancelar.ToString()}
                EnableBtn(frm, botones, False)
            Case "Inicio1"
                'botones = New[] { ClsEstilo.BtnLista.CmdEditar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString() };
                'lo_estilo.EnableBtn(this, botones, False);
                botones = {BtnLista.CmdGuardar.ToString(), BtnLista.CmdCancelar.ToString()}
                EnableBtn(frm, botones, False)
            Case "Nuevo"
                botones = {ClsEstilo.BtnLista.CmdNuevo.ToString(), ClsEstilo.BtnLista.CmdAgregar.ToString(), ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdBuscar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString()}
                EnableBtn(frm, botones, False)
                HabilitarRegistro(frm, True)
            Case "Editar"
                botones = {ClsEstilo.BtnLista.CmdNuevo.ToString(), ClsEstilo.BtnLista.CmdAgregar.ToString(), ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdBuscar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString(), ClsEstilo.BtnLista.CmdSalir.ToString()}
                EnableBtn(frm, botones, False)
                HabilitarRegistro(frm, True)
            Case "Eliminar"
                ' botones = {ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString()}
                botones = {ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString()}
                EnableBtn(frm, botones, False)
            Case "Guardar"
                botones = {ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString()}
                EnableBtn(frm, botones, False)
            Case "Cancelar"
                botones = {ClsEstilo.BtnLista.CmdEditar.ToString(), BtnLista.CmdModificar.ToString(), ClsEstilo.BtnLista.CmdEliminar.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString(), ClsEstilo.BtnLista.CmdCancelar.ToString()}
                EnableBtn(frm, botones, False)

            Case "Buscar"
                botones = {ClsEstilo.BtnLista.CmdNuevo.ToString(), ClsEstilo.BtnLista.CmdAgregar.ToString(), ClsEstilo.BtnLista.CmdBuscar.ToString(), ClsEstilo.BtnLista.CmdSalir.ToString(), ClsEstilo.BtnLista.CmdGuardar.ToString()}
                EnableBtn(frm, botones, False)
        End Select



    End Sub
    Private Sub CrearBotones_Pie(ByVal ABotones As ToolStrip)
        'CrearBoton(ABotones, "CmdPrimero", "&Primero", My.Resources.primero)
        'CrearBoton(ABotones, "CmdSiguiente", "&Siguiente", My.Resources.siguiente)
        ''  CrearTexto(ABotones, "TxtBuscar")
        'CrearBoton(ABotones, "CmdAtras", "&Atras", My.Resources.atras)
        'CrearBoton(ABotones, "CmdUltimo", "&Ultimo", My.Resources.ultimo)
        'CrearBoton(ABotones, "CmdBuscar", "&Buscar", My.Resources.visualizar16x16)
        'CrearBoton(ABotones, "CmdSalir", "&Salir", My.Resources.cerrar16x16)
    End Sub

    'Public Sub Estilo_Form_form(ByVal objForm As Form, Optional estadoform As FormWindowState = FormWindowState.Normal)
    '    objForm.WindowState = estadoform
    '    objForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    '    objForm.Opacity = 80%
    '    objForm.FormBorderStyle = FormBorderStyle.FixedSingle
    '    objForm.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub
    Public Sub Control_Estilos(ByVal control As Control, Optional we As FormWindowState = FormWindowState.Normal, Optional opacidad As Double = 100)
        If TypeOf control Is Form Then
            Estilo_Form_form(control, we, opacidad)
        End If
        For Each contHijo As Control In control.Controls
            If TypeOf contHijo Is TextBox Then
                EstiloTextBox(contHijo)
            End If
            If TypeOf contHijo Is ComboBox Then
                EstiloComboBox(contHijo)
            End If
            If TypeOf contHijo Is MaskedTextBox Then
                EstiloMTextBox(contHijo)
            End If
            If TypeOf contHijo Is DataGridView Then
                EstiloDataGrid(contHijo)
            End If
            If TypeOf contHijo Is Label Then
                EstiloLabel(contHijo)
            End If
            If TypeOf contHijo Is Button Then
                EstiloButton_nuevo(contHijo)
            End If
            If TypeOf contHijo Is Panel Then
                Estilo_Panel(contHijo)
            End If
            If TypeOf contHijo Is GroupBox Then
                Estilo_Groupbox(contHijo)
            End If
            If TypeOf contHijo Is CheckBox Then
                Estilo_checkbox(contHijo)
            End If
            If TypeOf contHijo Is PictureBox Then
                Estilo_PictureBox(contHijo)
            End If
            If contHijo.HasChildren Then
                Me.Control_Estilos(contHijo)
            End If

        Next
    End Sub
    Public Sub Estilo_Panel(ByVal objTabPage As Panel)
        If objTabPage.Tag <> "NO" Then
            If objTabPage.Name.Contains("pnl") = False Then
                objTabPage.BackColor = Color.Transparent
            End If
            'bjTabPage.BackColor = Color.Transparent
        End If
        If objTabPage.Name = "pnlTitulo" Then

            objTabPage.BackColor = Color.Transparent
            objTabPage.BackgroundImage = My.Resources.pnlTitulo2
            objTabPage.BackgroundImageLayout = ImageLayout.Stretch
            objTabPage.Refresh()
        End If

    End Sub
    Public Sub Estilo_Groupbox(ByVal objTabPage As GroupBox)
        objTabPage.BackColor = Color.Transparent
    End Sub
    Public Sub Estilo_PictureBox(ByVal objTabPage As PictureBox)
        objTabPage.BackColor = Color.Transparent
    End Sub
    Private Sub Estilo_Form_form(ByVal objForm As Form, Optional estadoform As FormWindowState = FormWindowState.Normal, Optional opacidad As Double = 100)
        objForm.WindowState = estadoform
        objForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        objForm.Opacity = opacidad
        objForm.FormBorderStyle = FormBorderStyle.FixedSingle
        objForm.BackgroundImageLayout = ImageLayout.Stretch
        'objForm.BackColor = Color.White
        'objForm.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(194)), Convert.ToInt32(Convert.ToByte(217)), Convert.ToInt32(Convert.ToByte(217))) 'Color.SteelBlue 'Color.LightSkyBlue
        objForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        'System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(255))) 'Color.SteelBlue 'Color.LightSkyBlue
        '192; 255; 255
    End Sub

    Public Sub Estilo_form_Button(objForm As Form, Optional Navigator As Boolean = False, Optional ButtonPosition As DockStyle = DockStyle.Top, Optional tipobutton As ClsEstilo.TipoButton = ClsEstilo.TipoButton.Predeterminado)
        Me.lo_Botones.Name = "ToolBotones"
        Me.lo_Botones.Location = New Point(0, 0)
        Me.lo_Botones.AutoSize = False
        ' The following expression was wrapped in a checked-statement
        Me.lo_Botones.Size = CType(New Point(objForm.Width - 18, 24), Size)
        Me.lo_Botones.RenderMode = ToolStripRenderMode.ManagerRenderMode
        Me.lo_Botones.BackColor = Color.FromArgb(Convert.ToInt32(Convert.ToByte(194)), Convert.ToInt32(Convert.ToByte(217)), Convert.ToInt32(Convert.ToByte(247)))
        Me.CrearBotones(Me.lo_Botones, tipobutton)
        Me.lo_Botones.SendToBack()
        Me.lo_Botones.Dock = ButtonPosition
        objForm.Controls.Add(Me.lo_Botones)
        If Navigator Then
            Me.lo_Botones_Pie.Name = "ToolBotones_pie"
            Me.lo_Botones_Pie.Location = New Point(0, 0)
            Me.lo_Botones_Pie.AutoSize = False
            Me.lo_Botones_Pie.Size = CType(New Point(objForm.Width - 18, 35), Size)
            Me.CrearBotones_Pie(Me.lo_Botones_Pie)
            Me.lo_Botones_Pie.Dock = DockStyle.Bottom
            Me.lo_Botones_Pie.SendToBack()
            objForm.Controls.Add(Me.lo_Botones_Pie)
        End If
        '  Me.HabilitarBotones(True)
        isnavigator = Navigator
        lo_form = objForm
        lo_form.Refresh()
    End Sub
    Private Sub label_Paint(sender As Label, e As PaintEventArgs)
        'Dim alto As Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(9)), Convert.ToInt32(Convert.ToByte(130)), Convert.ToInt32(Convert.ToByte(203)))
        Dim alto As Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(194)), Convert.ToInt32(Convert.ToByte(217)), Convert.ToInt32(Convert.ToByte(247))) 'Color.SteelBlue
        Dim altop As Color = Color.DeepSkyBlue
        Dim lgb As New LinearGradientBrush(sender.ClientRectangle, alto, altop, 90.0F)
        Dim g As Graphics = e.Graphics
        g.FillRectangle(lgb, sender.ClientRectangle)
        sender.ForeColor = Color.White
        '194; 217; 247
        'Dim font As New Font("Tahoma", 12.0F, FontStyle.Bold)
        Dim brush As New LinearGradientBrush(New Rectangle(0, 0, sender.Width, sender.Height + 5), Color.White, Color.White, 90.0F) 'LinearGradientMode.Vertical
        'e.Graphics.DrawString(sender.Text, Font, brush, 0, 0)
        e.Graphics.DrawString(sender.Text, sender.Font, brush, 0, 0)

    End Sub
    Public Sub EstiloColumnaGrid(objDataGrid As DataGridView, NombreColumna As String, CaptionColumna As String, anchoColumna As Integer, Optional orientacion As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft, Optional mostrar As Boolean = True, Optional formato As String = "")
        'objDataGrid.Columns(NombreColumna).HeaderText = CaptionColumna
        'objDataGrid.Columns(NombreColumna).Width = anchoColumna
        'objDataGrid.Columns(NombreColumna).DefaultCellStyle.Alignment = orientacion
        'objDataGrid.Columns(NombreColumna).Visible = mostrar
        'Dim flag As Boolean = formato.Length > 0
        'If flag Then
        ' objDataGrid.Columns(NombreColumna).DefaultCellStyle.Format = formato
        'End If
    End Sub

    Private Sub toolStrip_Paint(sender As ToolStrip, e As PaintEventArgs)
        Dim alto As Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(9)), Convert.ToInt32(Convert.ToByte(130)), Convert.ToInt32(Convert.ToByte(203)))
        Dim altop As Color = Color.DeepSkyBlue
        'Dim alto As Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(192)), Convert.ToInt32(Convert.ToByte(255)), Convert.ToInt32(Convert.ToByte(255))) 'Color.SteelBlue
        'Dim altop As Color = Color.DeepSkyBlue
        Dim lgb As New LinearGradientBrush(sender.ClientRectangle, alto, altop, 90.0F)
        Dim g As Graphics = e.Graphics
        g.FillRectangle(lgb, sender.ClientRectangle)
    End Sub

    Private Sub panel_Paint(sender As Panel, e As PaintEventArgs)

        Dim alto As Color = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(9)), Convert.ToInt32(Convert.ToByte(130)), Convert.ToInt32(Convert.ToByte(203)))

        Dim altop As Color = Color.DeepSkyBlue
        Try
            Dim lgb As New LinearGradientBrush(sender.ClientRectangle, alto, altop, 90.0F)
            Dim g As Graphics = e.Graphics
            g.FillRectangle(lgb, sender.ClientRectangle)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub Estilo_checkbox(ByVal objLabel As CheckBox)
        objLabel.Font = New Font("Arial", 8.0!, FontStyle.Regular)
        objLabel.BackColor = Color.Transparent

    End Sub

#End Region
#Region "Valida Texto"
    Public Function ValidaTexto(ByVal keying As Char, ByVal validatestring As String, ByVal mayus As Boolean, ByVal TxT As String, ByVal maxleng As Integer) As Char
        Dim keyout As Integer
        Dim keyin As Integer = Asc(keying)
        If Trim(TxT).Length < maxleng Then
            If keyin >= 32 Then 'si es un carácter
                If InStr(1, validatestring, UCase(Chr(keyin)), 1) > 0 Then
                    If mayus Then
                        keyout = Asc(UCase(Chr(keyin)))
                    Else
                        keyout = keyin
                    End If
                Else
                    keyout = 0
                    Beep()
                End If
            End If
        Else
            keyout = 0
            Beep()
        End If
        If keyin = 8 Then keyout = keyin
        ValidaTexto = Chr(keyout)
    End Function
#End Region
    Public Function Desencriptar(ByVal aString As String) As String
        Dim st As String = "", i As Integer
        For i = 0 To aString.Length - 1
            st += Denc(aString.Substring(i, 1))
        Next
        Return st
    End Function
    Private Function Denc(ByVal aChar As Char) As Char
        Dim ctem As Char, minuscula As Boolean = False
        If Char.IsLower(aChar) Then
            minuscula = True
            aChar = Char.ToUpper(aChar)
        End If
        ctem = "-"
        Select Case aChar
            Case Is = "Y" : ctem = "A"
            Case Is = "S" : ctem = "B"
            Case Is = "A" : ctem = "C"
            Case Is = "R" : ctem = "D"
            Case Is = "X" : ctem = "E"
            Case Is = "B" : ctem = "F"
            Case Is = "T" : ctem = "G"
            Case Is = "F" : ctem = "H"
            Case Is = "H" : ctem = "I"
            Case Is = "L" : ctem = "J"
            Case Is = "O" : ctem = "K"
            Case Is = "P" : ctem = "L"
            Case Is = "Ñ" : ctem = "M"
            Case Is = "C" : ctem = "N"
            Case Is = "D" : ctem = "Ñ"
            Case Is = "G" : ctem = "O"
            Case Is = "I" : ctem = "P"
            Case Is = "W" : ctem = "Q"
            Case Is = "Z" : ctem = "R"
            Case Is = "K" : ctem = "S"
            Case Is = "V" : ctem = "T"
            Case Is = "E" : ctem = "U"
            Case Is = "M" : ctem = "V"
            Case Is = "N" : ctem = "W"
            Case Is = "J" : ctem = "X"
            Case Is = "Q" : ctem = "Y"
            Case Is = "U" : ctem = "Z"
            Case Is = "(" : ctem = "0"
            Case Is = "*" : ctem = "1"
            Case Is = "[" : ctem = "2"
            Case Is = ")" : ctem = "3"
            Case Is = "$" : ctem = "4"
            Case Is = "#" : ctem = "5"
            Case Is = "." : ctem = "6"
            Case Is = "]" : ctem = "7"
            Case Is = "+" : ctem = "8"
            Case Is = "{" : ctem = "9"
            Case Is = "9" : ctem = "&"
            Case Is = "&" : ctem = "*"
            '  Case Is = "}" : ctem = "}"
            Case Is = "6" : ctem = "6"
            'Case Is = "4" : ctem = "4"
            Case Is = "4" : ctem = "."
            Case Is = "8" : ctem = "8"
            Case Is = "2" : ctem = "2"
            Case Is = "3" : ctem = "3"
            Case Is = "-" : ctem = "-"
            Case Is = "5" : ctem = "5"
            Case Is = "7" : ctem = "7"
            Case Is = "0" : ctem = "0"
            Case Is = "?" : ctem = "$"
            Case Is = "@" : ctem = "#"
            Case Is = "}" : ctem = "-"
            Case Is = "1" : ctem = "@"
            Case Else : ctem = aChar
        End Select
        If minuscula = True Then ctem = Char.ToLower(ctem)
        Return ctem
    End Function
    ''' <summary>
    ''' Completa de un caracter especificado en el tipo valor relleno
    ''' </summary>
    ''' <param name="nume"></param>
    ''' <param name="size"></param>
    ''' <param name="relleno"></param>
    ''' <returns></returns>
    Public Function full(ByVal nume As Integer, Optional ByVal size As Integer = 4, Optional ByVal relleno As String = "0") As String
        Dim cadfull As String = Nothing
        If Trim(Str(nume)).Length <= size And Trim(relleno.Length) = 1 Then
            cadfull = StrDup(size - Trim(Str(nume)).Length, relleno) + Trim(Str(nume))
        Else
            MessageBox.Show("Error al generar el siguiente codigo automatico")
        End If
        Return cadfull
    End Function

#Region "Imagen"
    Public Function Image2Bytes(ByVal img As Image) As Byte()
        Dim sTemp As String = Path.GetTempFileName()
        Dim fs As New FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        img.Save(fs, System.Drawing.Imaging.ImageFormat.Png)
        fs.Position = 0
        Dim imgLength As Integer = CInt(fs.Length)
        Dim bytes(0 To imgLength - 1) As Byte
        fs.Read(bytes, 0, imgLength)
        fs.Close()
        Return bytes
    End Function
    Public Function Bytes2Image(ByVal bytes() As Byte) As Image
        If bytes Is Nothing Then Return Nothing
        Dim ms As New MemoryStream(bytes)
        Dim bm As Bitmap = Nothing
        Try
            bm = New Bitmap(ms)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
        Return bm
    End Function
    Public Function tempfile() As String
        Dim c As Integer
        c += 1
        Dim number As String
        Dim name1 As String
        number = Str(Rnd(c))
        number = CDbl(number) * Val(full(1, number.Length))
        name1 = "W" + number.Trim & Format(Now, "ddMMyyyyhhmmss")
        If File.Exists(Application.StartupPath & "\" & name1) = True Then
            File.Delete(Application.StartupPath & "\" & name1)
        End If
        Return name1
    End Function
    Public Function tempfile(s As Boolean) As String
        Dim c As Integer
        c += 1
        Dim name1 As String
        name1 = "W" + Format(Now, "ddMMyyyyhhmmss")
        If File.Exists(Application.StartupPath & "\" & name1) = True Then
            File.Delete(Application.StartupPath & "\" & name1)
        End If
        Return name1
    End Function
    Public Sub ImprimirReporte(ByVal t As DataTable, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String, ByVal NameRepor As String, Optional NameReport As String = "Reporte", Optional NamePeriodo As String = "", Optional frmmdi As Form = Nothing)
        Dim RV As New ReportViewer
        RV.ProcessingMode = ProcessingMode.Local
        RV.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        RV.LocalReport.DataSources.Add(RDS)
        RV.Dock = DockStyle.Fill
        RV.ZoomMode = ZoomMode.Percent
        RV.ZoomPercent = 100
        RV.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            RV.LocalReport.SetParameters(parametros)
        End If
        Dim FrmR As New Form
        FrmR.Size = New Size(750, 550)
        FrmR.Text = NameReport & " " & NamePeriodo
        If IsNothing(frmmdi) = False Then
            FrmR.MdiParent = frmmdi.MdiParent
        End If


        FrmR.Controls.Add(RV)
        FrmR.Show()
        FrmR.WindowState = FormWindowState.Maximized
        RV.RefreshReport()
    End Sub
    Public Sub ImprimirReporte(ByVal t As BindingSource, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String, ByVal NameRepor As String, Optional NameReport As String = "Reporte", Optional NamePeriodo As String = "")
        Dim RV As New ReportViewer
        RV.ProcessingMode = ProcessingMode.Local
        RV.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        RV.LocalReport.DataSources.Add(RDS)
        RV.Dock = DockStyle.Fill
        ' establece modo porcentaje
        RV.ZoomMode = ZoomMode.Percent
        RV.ZoomPercent = 100
        RV.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            RV.LocalReport.SetParameters(parametros)
        End If

        Dim FrmR As New Form
        FrmR.Size = New Size(750, 550)
        FrmR.Text = NameReport & " " & NamePeriodo

        FrmR.Controls.Add(RV)
        FrmR.Show()
        FrmR.WindowState = FormWindowState.Maximized
        RV.RefreshReport()
    End Sub

    Public Sub ImprimirReporteVarios(ByVal t As DataSet, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String, ByVal NameRepor As String, Optional percent As Integer = 100)
        Dim RV As New ReportViewer
        RV.ProcessingMode = ProcessingMode.Local
        RV.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        For Each dt As DataTable In t.Tables
            Dim RDS As New ReportDataSource(dt.TableName, dt)
            RV.LocalReport.DataSources.Add(RDS)
        Next
        RV.Dock = DockStyle.Fill
        RV.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        RV.ZoomPercent = 100
        RV.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)

        Dim x As Byte
        For x = 0 To nrval - 1
            parametros.Add(New ReportParameter(campos(x), val(x), True))
        Next
        RV.LocalReport.SetParameters(parametros)
        Dim FrmR As New Form
        FrmR.Size = New Size(750, 550)
        FrmR.Text = "Reporte"
        'RV.Dock = True
        FrmR.Controls.Add(RV)
        FrmR.Show()
        FrmR.WindowState = FormWindowState.Maximized
        RV.RefreshReport()

    End Sub
    Public Function ImprimirReporte(obj As Panel, t As BindingSource, nrval As Byte, campos As String(), val As String(), NameRepor As String, Optional percent As Integer = 100) As Panel
        obj.Controls.Clear()
        Dim RV As New ReportViewer
        RV.ProcessingMode = ProcessingMode.Local
        RV.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        RV.LocalReport.DataSources.Add(RDS)
        RV.Dock = DockStyle.Fill
        ' establece modo porcentaje
        RV.ZoomMode = ZoomMode.Percent
        RV.ZoomPercent = 100
        RV.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            RV.LocalReport.SetParameters(parametros)
        End If

        RV.DocumentMapCollapsed = True
        RV.LocalReport.Refresh()
        RV.RefreshReport()
        obj.Controls.Add(RV)
        Return obj
    End Function
    Public Function ImprimirReporteRV(rv As ReportViewer, t As BindingSource, nrval As Byte, campos As String(), val As String(), NameRepor As String, Optional percent As Integer = 100) As ReportViewer
        rv.ProcessingMode = ProcessingMode.Local
        rv.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        rv.LocalReport.DataSources.Add(RDS)
        rv.Dock = DockStyle.Fill
        ' establece modo porcentaje
        rv.ZoomMode = ZoomMode.Percent
        rv.ZoomPercent = 100
        rv.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            rv.LocalReport.SetParameters(parametros)
        End If

        rv.DocumentMapCollapsed = True
        rv.LocalReport.Refresh()
        rv.RefreshReport()
        Return rv
    End Function
    Public Function ImprimirReporteRV(rv As ReportViewer, t As DataTable, nrval As Byte, campos As String(), val As String(), NameRepor As String, Optional percent As Integer = 100) As ReportViewer
        rv.ProcessingMode = ProcessingMode.Local
        rv.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        rv.LocalReport.DataSources.Add(RDS)
        rv.Dock = DockStyle.Fill
        ' establece modo porcentaje
        rv.ZoomMode = ZoomMode.Percent
        rv.ZoomPercent = 100
        rv.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            rv.LocalReport.SetParameters(parametros)
        End If

        rv.DocumentMapCollapsed = True
        rv.LocalReport.Refresh()
        rv.RefreshReport()
        Return rv
    End Function

    Public Sub ToExcel(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)

        viewer.LocalReport.DataSources.Add(rptDataSource)
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            viewer.LocalReport.SetParameters(parametros)
        End If
        'Export to PDF. Get binary content.
        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim pdfContent As Byte() = Nothing

        Try
            pdfContent = viewer.LocalReport.Render("Excel", Nothing, mimeType, encoding, extension, streamids, warnings)
            Dim pdfPath As String = ruta & "\" & namearchivo & ".xls"
            Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
            pdfFile.Write(pdfContent, 0, pdfContent.Length)
            viewer.Clear()
            viewer.Reset()
            pdfFile.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Creatr PDF file on disk

    End Sub
    Public Sub ToExcel(ByVal t As BindingSource, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)

        viewer.LocalReport.DataSources.Add(rptDataSource)
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            viewer.LocalReport.SetParameters(parametros)
        End If
        'Export to PDF. Get binary content.
        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim pdfContent As Byte() = Nothing

        Try
            pdfContent = viewer.LocalReport.Render("Excel", Nothing, mimeType, encoding, extension, streamids, warnings)
            Dim pdfPath As String = ruta & "\" & namearchivo & ".xls"
            Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
            pdfFile.Write(pdfContent, 0, pdfContent.Length)
            viewer.Clear()
            viewer.Reset()

            pdfFile.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Creatr PDF file on disk

    End Sub
    ''' <summary>
    ''' Habilita o desabilita según el parametro esEnable que se envía
    ''' </summary>
    ''' <param name="co"></param>
    ''' <param name="objectos"></param>
    ''' <param name="esEnable"></param>
    Private Sub EnableBtn(co As Form, objectos As String(), esEnable As Boolean)
        Dim oToolStripButton As Windows.Forms.ToolStripButton
        Dim child As System.Windows.Forms.Control = Nothing
        For Each c As System.Windows.Forms.Control In co.Controls
            If TypeOf c Is System.Windows.Forms.ToolStrip Then

                Try
                    For Each oToolStripButton In CType(c, System.Windows.Forms.ToolStrip).Items
                        oToolStripButton.Enabled = True
                        If TypeOf oToolStripButton Is ToolStripButton Then
                            For Each boton As String In objectos
                                If oToolStripButton.Name.ToUpper = boton.ToUpper Then
                                    oToolStripButton.Enabled = False
                                End If
                            Next
                        End If
                    Next
                Catch ex As Exception

                End Try



            End If
        Next
    End Sub

    ''' <summary>
    ''' Genera una tabla html para los correo electrónicos
    ''' </summary>
    ''' <param name="td"></param>
    ''' <param name="nro"></param>
    ''' <param name="fecha"></param>
    ''' <param name="importe"></param>
    ''' <param name="empresa"></param>
    ''' <param name="Mon"></param>
    ''' <returns></returns>
    Public Function Table(td As String, nro As String, fecha As String, importe As String, empresa As String, Mon As String) As String
        Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()
        builder.Append(String.Format("<table>
<tr>
<td><font color =""purple"">Tipo Documento</font></td>
<td>:</td>
<td>" & td & "</td>
</tr>
<tr>
<td><font color =""purple"">Numero de comprobante</font></td>
<td>:</td>
<td>" & nro & "</td>
</tr>
<tr>
<td><font color =""purple"">Fecha Emisi&#243;n</font></td>
<td>:</td>
<td>" & fecha & "</td>
</tr>
<tr>
<td><font color =""purple"">Importe (" & IIf(Mon = "MN", "S/", "USD") & ")</font></td>
<td>:</td>
<td>" & importe & "</td>
</tr>
<tr>
<td colspan=""3""><br/></td>
</tr>
<tr>
<td colspan=""3""><br/></td>
</tr>
<tr>
<td colspan=""3"">Muy coordialmente,</td>
</tr>
<tr>
<td colspan=""3""><b>" & empresa & "</b></td>
</tr>
</table>
"))
        Return builder.ToString()
    End Function

#End Region
    Public Function ObtenerCamposDataTable(g As DataGridView, campochk As String, campoin As String) As DataTable
        Dim cadena As String = ""
        Dim lista As List(Of DataGridViewRow) = (From Rows In g.Rows.Cast(Of DataGridViewRow)()
                                                 Where CBool(Rows.Cells(campochk).Value) = True).ToList
        Dim cli As New DataTable()
        cli.Columns.Add("Id")

        For Each row As DataGridViewRow In lista
            Dim r As DataRow = cli.NewRow()
            r.Item("Id") = row.Cells(campoin).Value
            cli.Rows.Add(r)
        Next
        Return cli
    End Function
    Public Function ObtenerCamposempty(g As DataGridView, campochk As String, campoin As String) As DataTable
        Dim cli As New DataTable()
        cli.Columns.Add("Id")
        Dim r As DataRow = cli.NewRow()
        cli.Rows.Add(r)
        Return cli
    End Function


    Public Function ObtenerCampos(g As DataGridView, campochk As String) As List(Of DataGridViewRow)
        Dim cadena As String = ""
        Dim lista As List(Of DataGridViewRow) = (From Rows In g.Rows.Cast(Of DataGridViewRow)()
                                                 Where CBool(Rows.Cells(campochk).Value) = True).ToList

        Return lista
    End Function

    Public Sub AbriExcel(filename As String, Optional nameSheet As String = "", Optional InmobilizarPanel As String = "")
        Dim xlibro As Microsoft.Office.Interop.Excel.Application
        Dim strRutaExcel As String
        Try
            strRutaExcel = Application.StartupPath & "\" & filename & ".xls"
            xlibro = CreateObject("Excel.Application")
            xlibro.Workbooks.Open(strRutaExcel)
            xlibro.Visible = True
            If nameSheet = "" Then
                xlibro.Sheets(filename).Select()
            Else
                xlibro.Sheets(nameSheet).Select()
            End If
            Try
                xlibro.ActiveWindow.DisplayGridlines = True
                xlibro.ActiveWindow.SplitColumn = 0
                xlibro.ActiveWindow.SplitRow = 0
                If InmobilizarPanel.Length > 0 Then
                    xlibro.Range(InmobilizarPanel).Select()
                    xlibro.ActiveWindow.FreezePanes = True
                    xlibro.Cells.[Select]()
                    xlibro.Cells.WrapText = False
                    xlibro.Cells.Orientation = 0
                    xlibro.Cells.AddIndent = False
                    xlibro.Cells.IndentLevel = 0
                    xlibro.Cells.ShrinkToFit = False
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Obtiene los campos en una sucecion de in para una consulta sql
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="campochk"></param>
    ''' <param name="campoin"></param>
    ''' <returns></returns>
    Public Function ObtenerCamposIn(g As DataGridView, campochk As String, campoin As String) As String
        Dim cadena As String = ""
        Dim lista As List(Of DataGridViewRow) = (From Rows In g.Rows.Cast(Of DataGridViewRow)()
                                                 Where CBool(Rows.Cells(campochk).Value) = True).ToList
        If lista.Count > 0 Then
            cadena = "'"
        End If
        Dim i As Integer = 1
        For Each row As DataGridViewRow In lista
            If i = lista.Count Then
                cadena += row.Cells(campoin).Value.trim & "'"
            Else
                cadena += row.Cells(campoin).Value.trim & "','"
            End If
            i += 1
        Next
        Return cadena
    End Function

    Public Function Dgrid_to_datatable(g As DataGridView, campochk As String, campoin As String()) As DataTable
        Try
            Dim lista As List(Of DataGridViewRow) = (From Rows In g.Rows.Cast(Of DataGridViewRow)()
                                                     Where CBool(Rows.Cells(campochk).Value) = True).ToList
            If g.ColumnCount = 0 Then
                Return Nothing
            End If
            Dim dtSource As New DataTable()
            For Each col As DataGridViewColumn In g.Columns
                For Each ca As String In campoin
                    If ca.ToUpper = col.Name.ToUpper Then
                        dtSource.Columns.Add(col.Name, col.ValueType)
                        dtSource.Columns(col.Name).Caption = col.HeaderText
                    End If
                Next
            Next

            For Each row As DataGridViewRow In lista
                Dim r As DataRow = dtSource.NewRow()
                For Each ca As String In campoin
                    r.Item(ca) = row.Cells(ca).Value
                Next
                dtSource.Rows.Add(r)
            Next
            If dtSource.Columns.Count = 0 Then
                Return Nothing
            End If
            Return dtSource
        Catch
            Return Nothing
        End Try
    End Function
    Public Function ImprimirReporteVariosRV(rv As ReportViewer, ByVal t As DataSet, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String, ByVal NameRepor As String, Optional percent As Integer = 100) As ReportViewer
        rv.ProcessingMode = ProcessingMode.Local
        rv.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        For Each dt As DataTable In t.Tables
            Dim RDS As New ReportDataSource(dt.TableName, dt)
            RV.LocalReport.DataSources.Add(RDS)
        Next
        RV.Dock = DockStyle.Fill
        RV.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        RV.ZoomPercent = 100
        RV.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)

        Dim x As Byte
        For x = 0 To nrval - 1
            parametros.Add(New ReportParameter(campos(x), val(x), True))
        Next
        RV.LocalReport.SetParameters(parametros)
        '-----
        RV.DocumentMapCollapsed = True
        RV.LocalReport.Refresh()
        RV.RefreshReport()
        Return RV
    End Function

End Class
