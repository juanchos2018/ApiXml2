Imports ClsSigmaWs
Public Class Form1
    ' Dim sw As New ClsSunat
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'sw.Obtenercdr()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ws As String = "https://e-factura.sunat.gob.pe/ol-it-wsconscpegem/billConsultService"
        Dim ruc As String = "20511037001"
        Dim user_sol As String = "GRUP0STA"
        Dim pws As String = "Fac%Elec17"
        Dim con As New ClsSunat(ws, ruc, user_sol, pws)
        Dim mensaje As String() = con.ObtenerEstado("20511037001", "01", "F001", "0000326")
        MessageBox.Show(mensaje(0) & "-->" & mensaje(1))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ws As String = "https://e-factura.sunat.gob.pe/ol-it-wsconsvalidcpe/billValidService"
        Dim ruc As String = "20511037001"
        Dim user_sol As String = "GRUP0STA"
        Dim pws As String = "Fac%Elec17"
        Dim con As New ClsSunat_Valida(ws, ruc, user_sol, pws)
        Dim mensaje As String() = con.Valida_Cpe("20565683951", "01", "E001", "97", "6", "20449213166", "22/09/2018", 3936.0) 'con.ObtenerEstado("20511037001", "01", "F001", "0000326")
        MessageBox.Show(mensaje(0) & "-->" & mensaje(1))
    End Sub
End Class
