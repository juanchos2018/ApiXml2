Public Class ClsSeguridad
    Public Function Desencriptar(ByVal aString As String) As String
        Dim st As String = "", i As Integer
        For i = 0 To aString.Length - 1
            st += Denc(aString.Substring(i, 1))
        Next
        Return st
    End Function
    Private Function Enc(ByVal aChar As Char) As Char
        Dim ctem As Char, minuscula As Boolean = False
        If Char.IsLower(aChar) Then
            minuscula = True
            aChar = Char.ToUpper(aChar)
        End If
        ctem = "-"

        Select Case aChar
            Case Is = "A" : ctem = "Y"
            Case Is = "B" : ctem = "S"
            Case Is = "C" : ctem = "A"
            Case Is = "D" : ctem = "R"
            Case Is = "E" : ctem = "X"
            Case Is = "F" : ctem = "B"
            Case Is = "G" : ctem = "T"
            Case Is = "H" : ctem = "F"
            Case Is = "I" : ctem = "H"
            Case Is = "J" : ctem = "L"
            Case Is = "K" : ctem = "O"
            Case Is = "L" : ctem = "P"
            Case Is = "M" : ctem = "Ñ"
            Case Is = "N" : ctem = "C"
            Case Is = "Ñ" : ctem = "D"
            Case Is = "O" : ctem = "G"
            Case Is = "P" : ctem = "I"
            Case Is = "Q" : ctem = "W"
            Case Is = "R" : ctem = "Z"
            Case Is = "S" : ctem = "K"
            Case Is = "T" : ctem = "V"
            Case Is = "U" : ctem = "E"
            Case Is = "V" : ctem = "M"
            Case Is = "W" : ctem = "N"
            Case Is = "X" : ctem = "J"
            Case Is = "Y" : ctem = "Q"
            Case Is = "Z" : ctem = "U"
            Case Is = "0" : ctem = "("
            Case Is = "1" : ctem = "*"
            Case Is = "2" : ctem = "["
            Case Is = "3" : ctem = ")"
            Case Is = "4" : ctem = "$"
            Case Is = "5" : ctem = "#"
            Case Is = "6" : ctem = "."
            Case Is = "7" : ctem = "]"
            Case Is = "8" : ctem = "+"
            Case Is = "9" : ctem = "{"
            Case Is = "&" : ctem = "9"
            Case Is = "*" : ctem = "&"
            Case Is = "-" : ctem = "}"
            Case Is = "+" : ctem = "6"
            Case Is = "." : ctem = "4"
            Case Is = "(" : ctem = "8"
            Case Is = ")" : ctem = "2"
            Case Is = "[" : ctem = "3"
            Case Is = "]" : ctem = "-"
            Case Is = "{" : ctem = "5"
            Case Is = "}" : ctem = "7"
            Case Is = "?" : ctem = "0"
            Case Is = "$" : ctem = "?"
            Case Is = "#" : ctem = "@"
            Case Is = "@" : ctem = "1"
            Case Is = "%" : ctem = "%"
            Case Else : ctem = aChar
        End Select
        If minuscula = True Then ctem = Char.ToLower(ctem)
        Return ctem
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
            Case Is = "6" : ctem = "+"
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
            Case Is = "%" : ctem = "%"
            Case Else : ctem = aChar
        End Select
        If minuscula = True Then ctem = Char.ToLower(ctem)
        Return ctem
    End Function
    Public Function Encriptar(ByVal aString As String) As String
        Dim st As String = "", i As Integer
        For i = 0 To aString.Length - 1
            st += Enc(aString.Substring(i, 1))
        Next
        Return st
    End Function


End Class
