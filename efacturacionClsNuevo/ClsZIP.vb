Imports System.IO
Imports Ionic.Zip

Public Class ClsZIP

    Public Sub MyZip(ByVal ContentFolder As String, ByVal CreateZip As String)
        Using zip As ZipFile = New ZipFile()
            zip.AddDirectory(ContentFolder)

            zip.Save(CreateZip & ".zip")
        End Using

    End Sub
    Public Sub Comprimir(ByVal Ruta As String, ByVal FileToZip As String, ByVal FileZip As String)
        Using zip As ZipFile = New ZipFile()
            zip.AddFile(Ruta & "\" & FileToZip, "")
            zip.Save(Ruta & "\" & FileZip)
        End Using
    End Sub
    Public Function ComprimirToBinary(ByVal Ruta As String, ByVal FileToZip As String, ByVal FileZip As String) As Byte()
        Dim ms As New MemoryStream
        Using zip As ZipFile = New ZipFile()
            zip.AddFile(Ruta & "\" & FileToZip, "")
            zip.Save(ms)
        End Using
        Return ms.ToArray
    End Function
    'Public Function ComprimirToBinary(a As Byte()) As Byte()
    '    Dim ms As New MemoryStream(a)
    '    'Using zip As ZipFile = New ZipFile(String.Format("{0}.zip"))
    '    Using zip As ZipFile = New ZipFile()
    '        zip.AddFile(New MemoryStream(a), "zip")
    '        zip.Save(ms)
    '    End Using
    '    Return ms.ToArray
    'End Function
    Public Function ExtrarToByte(a As Byte()) As Byte()
        Dim ms As New MemoryStream(a)
        Dim msxml As New MemoryStream()
        Using zip As ZipFile = ZipFile.Read(ms)
            Dim e As ZipEntry
            For Each e In zip
                e.Extract(msxml)
            Next
        End Using
        Return msxml.ToArray
    End Function

    Public Sub MyExtract(ByVal ZipToUnpack As String, ByVal UnpackDirectory As String)
        Using zip As ZipFile = ZipFile.Read(ZipToUnpack)
            Dim e As ZipEntry
            For Each e In zip
                e.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub
End Class
