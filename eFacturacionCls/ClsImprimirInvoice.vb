Imports Microsoft.Reporting.WinForms
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.Text

Public Class ClsImprimirInvoice
    Implements IDisposable

    Public Sub ImprimirReporte(ByVal Menu As Object, ByVal t As DataTable, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String, ByVal NameRepor As String)
        Dim RV As New ReportViewer
        RV.ProcessingMode = ProcessingMode.Local
        RV.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim RDS As New ReportDataSource("conexion", t)
        RV.LocalReport.DataSources.Add(RDS)
        'RV.Dock = DockStyle.Fill
        RV.Dock = DockStyle.Fill
        RV.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth
        'RV.ZoomMode = Microsoft.Reporting.WinForms.DisplayMode.PrintLayout
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
        'FrmR.Size = New Size(750, 550)
        FrmR.Text = "Reporte "
        '  FrmR.MdiParent = Menu.FrmMen
        FrmR.Controls.Add(RV)
        FrmR.Show()
        FrmR.WindowState = FormWindowState.Maximized
        RV.RefreshReport()
    End Sub
    Public Sub ToPdf(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)

        viewer.LocalReport.DataSources.Add(rptDataSource)
        viewer.LocalReport.EnableExternalImages = True
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
            pdfContent = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

            Dim pdfPath As String = ruta & "\" & namearchivo & ".pdf"
            Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
            pdfFile.Write(pdfContent, 0, pdfContent.Length)
            pdfFile.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function ToPdfBinario(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String) As Byte()
        Dim viewer As New ReportViewer()
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)
        viewer.LocalReport.DataSources.Add(rptDataSource)
        viewer.LocalReport.EnableExternalImages = True
        Dim parametros As New List(Of Microsoft.Reporting.WinForms.ReportParameter)
        Dim x As Byte
        If nrval <> 0 Then
            For x = 0 To nrval - 1
                parametros.Add(New ReportParameter(campos(x), val(x), True))
            Next
            viewer.LocalReport.SetParameters(parametros)
        End If
        Dim warnings As Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim pdfContent As Byte() = Nothing
        Dim pdfbyte As Byte() = Nothing
        Try
            pdfContent = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

            Dim pdfPath As String = ruta & "\" & namearchivo & ".pdf"
            Dim pdfFile As New System.IO.FileStream(pdfPath, System.IO.FileMode.Create)
            pdfbyte = pdfContent
            pdfFile.Write(pdfContent, 0, pdfContent.Length)
            viewer.Reset()
            viewer.Clear()
            pdfFile.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return pdfbyte
    End Function

    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Public Sub ToTicket(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)
        viewer.LocalReport.DataSources.Add(rptDataSource)
        viewer.LocalReport.EnableExternalImages = True
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
        '  Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat></DeviceInfo>"

        Dim deviceInfo As String =
          "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <PageWidth>8cm</PageWidth>" +
          "  <PageHeight>23cm</PageHeight>" +
          "  <MarginTop>0.54cm</MarginTop>" +
          "  <MarginLeft>0.5cm</MarginLeft>" +
          "  <MarginRight>0.5cm</MarginRight>" +
          "  <MarginBottom>0.54cm</MarginBottom>" +
          "</DeviceInfo>"
        Try
            m_streams = New List(Of Stream)()
            viewer.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream In m_streams
                stream.Position = 0
            Next
            m_currentPageIndex = 0
            PrintTicket()
            Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub ToA8(ByVal t As DataTable, ByVal NameRepor As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)
        viewer.LocalReport.DataSources.Add(rptDataSource)
        viewer.LocalReport.EnableExternalImages = True
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
        '  Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat></DeviceInfo>"

        Dim deviceInfo As String =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>15cm</PageWidth>" +
        "  <PageHeight>21cm</PageHeight>" +
        "  <MarginTop>0.5cm</MarginTop>" +
        "  <MarginLeft>0.5cm</MarginLeft>" +
        "  <MarginRight>0.5cm</MarginRight>" +
        "  <MarginBottom>0.5cm</MarginBottom>" +
        "</DeviceInfo>"
        Try
            m_streams = New List(Of Stream)()
            viewer.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream In m_streams
                stream.Position = 0
            Next
            m_currentPageIndex = 0
            Printa8()
            Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub ToA4(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        'Set local report
        'NOTE: MyAppNamespace refers to the namespace for the app.
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
        'Create Report Data Source
        Dim rptDataSource As New Microsoft.Reporting.WinForms.ReportDataSource("conexion", t)
        viewer.LocalReport.DataSources.Add(rptDataSource)
        viewer.LocalReport.EnableExternalImages = True
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
        '  Dim deviceInfo As String = "<DeviceInfo><OutputFormat>EMF</OutputFormat></DeviceInfo>"

        Dim deviceInfo As String =
        "<DeviceInfo>" +
        "  <OutputFormat>EMF</OutputFormat>" +
        "  <PageWidth>21cm</PageWidth>" +
        "  <PageHeight>29.7cm</PageHeight>" +
        "  <MarginTop>0.4cm</MarginTop>" +
        "  <MarginLeft>0.4cm</MarginLeft>" +
        "  <MarginRight>0.4cm</MarginRight>" +
        "  <MarginBottom>0.4cm</MarginBottom>" +
        "</DeviceInfo>"
        Try
            m_streams = New List(Of Stream)()
            viewer.LocalReport.Render("Image", deviceInfo, AddressOf CreateStream, warnings)
            For Each stream In m_streams
                stream.Position = 0
            Next
            m_currentPageIndex = 0
            Printa4()
            Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        If Not (m_streams Is Nothing) Then
            Dim stream As Stream
            For Each stream In m_streams
                stream.Close()
            Next
            m_streams = Nothing
        End If
    End Sub
    Private Function CreateStream(ByVal name As String, ByVal fileNameExtension As String, ByVal encoding As Encoding, ByVal mimeType As String, ByVal willSeek As Boolean) As Stream
        Dim stream As Stream = New FileStream(Application.StartupPath + "\" + name + "." + fileNameExtension, FileMode.Create)
        m_streams.Add(stream)
        Return stream
    End Function
    Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

        Dim pageImage As New Metafile(m_streams(m_currentPageIndex))
        'ev.Graphics.DrawImage(pageImage, ev.PageBounds)
        ev.Graphics.DrawImage(pageImage, ev.MarginBounds.Left, ev.MarginBounds.Top, ev.MarginBounds.Width, ev.MarginBounds.Height)
        '  e.Graphics.DrawImage(pcb.Image, e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height)
        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)
    End Sub
    Private Sub PrintTicket()
        If m_streams Is Nothing Or m_streams.Count = 0 Then
            Return
        End If
        Dim factorhoja As Double = 37.94076164
        Dim Ancho As Double = 8
        Dim alto As Double = 23
        Dim margins As New Margins(25, 25, 25, 25)
        'Dim instance As New PaperSize("Custom", Ancho * factorhoja, alto * factorhoja)
        Dim printDoc As New PrintDocument()
        printDoc.DefaultPageSettings.Margins = margins
        ' printDoc.DefaultPageSettings.PaperSize = instance
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        printDoc.Print()
    End Sub
    Private Sub Printa4()
        If m_streams Is Nothing Or m_streams.Count = 0 Then
            Return
        End If
        'Dim factorhoja As Double = 37.94076164
        'Dim Ancho As Double = 21
        'Dim alto As Double = 29.7
        Dim margins As New Margins(25, 25, 25, 25)

        'Dim instance As New PaperSize("Custom", Ancho * factorhoja, alto * factorhoja)
        Dim printDoc As New PrintDocument()
        printDoc.DefaultPageSettings.Margins = margins

        ' printDoc.DefaultPageSettings.PaperSize = instance
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        printDoc.Print()
    End Sub

    Private Sub Printa8()
        If m_streams Is Nothing Or m_streams.Count = 0 Then
            Return
        End If
        Dim factorhoja As Double = 37.94076164
        Dim Ancho As Double = 15
        Dim alto As Double = 21
        Dim margins As New Margins(25, 25, 25, 25)

        Dim instance As New PaperSize("Custom", Ancho * factorhoja, alto * factorhoja)
        Dim printDoc As New PrintDocument()
        printDoc.DefaultPageSettings.Margins = margins

        ' printDoc.DefaultPageSettings.PaperSize = instance
        AddHandler printDoc.PrintPage, AddressOf PrintPage
        printDoc.Print()
    End Sub

    Public Sub ToExcel(ByVal t As DataTable, ByVal NameRepor As String, ByVal namearchivo As String, ByVal ruta As String, ByVal nrval As Byte, ByVal campos As String(), ByVal val() As String)
        Dim viewer As New ReportViewer()
        viewer.LocalReport.ReportPath = Application.StartupPath & "\Reportes\" & NameRepor   ' Nombre del reporte .rdl
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
            viewer.Reset()
            viewer.Clear()
            pdfFile.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class
