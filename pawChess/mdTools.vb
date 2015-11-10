Option Strict On
Option Explicit On

Public Module mdTools

    Public Function IsBetween(ByVal nValue As Object, ByVal nLBound As Object, ByVal nUBound As Object, Optional ByVal bHandleAsInteger As Boolean = True) As Boolean
        Try
            If bHandleAsInteger Then
                Return CInt(nLBound) <= CInt(nValue) And CInt(nValue) <= CInt(nUBound)
            Else
                Return CDbl(nLBound) <= CDbl(nValue) And CDbl(nValue) <= CDbl(nUBound)
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Const cstrDummy As String = "__MISSING_PARAMETER__"      ' Wir brauchen einen Dummy-Parameter, denn Nothing ist auch ein erlaubter Wert
    Public Function IsOneOf(ByVal strValue As String, Optional ByVal o0 As String = cstrDummy, Optional ByVal o1 As String = cstrDummy, Optional ByVal o2 As String = cstrDummy, Optional ByVal o3 As String = cstrDummy, Optional ByVal o4 As String = cstrDummy, Optional ByVal o5 As String = cstrDummy, Optional ByVal o6 As String = cstrDummy, Optional ByVal o7 As String = cstrDummy, Optional ByVal o8 As String = cstrDummy, Optional ByVal o9 As String = cstrDummy) As Boolean
        If strValue Is Nothing Then
            Return o0 Is Nothing OrElse o1 Is Nothing OrElse o2 Is Nothing OrElse o3 Is Nothing OrElse o4 Is Nothing OrElse o5 Is Nothing OrElse o6 Is Nothing OrElse o7 Is Nothing OrElse o8 Is Nothing OrElse o9 Is Nothing
        Else
            Return strValue.Equals(o0) OrElse strValue.Equals(o1) OrElse strValue.Equals(o2) OrElse strValue.Equals(o3) OrElse strValue.Equals(o4) OrElse strValue.Equals(o5) OrElse strValue.Equals(o6) OrElse strValue.Equals(o7) OrElse strValue.Equals(o8) OrElse strValue.Equals(o9)
        End If
    End Function

    Const cnDummy As Integer = Integer.MaxValue ' Wir brauchen einen Dummy-Parameter, denn Nothing resultiert in 0
    Public Function IsOneOf(ByVal nValue As Integer, Optional ByVal n0 As Integer = cnDummy, Optional ByVal n1 As Integer = cnDummy, Optional ByVal n2 As Integer = cnDummy, Optional ByVal n3 As Integer = cnDummy, Optional ByVal n4 As Integer = cnDummy, Optional ByVal n5 As Integer = cnDummy, Optional ByVal n6 As Integer = cnDummy, Optional ByVal n7 As Integer = cnDummy, Optional ByVal n8 As Integer = cnDummy, Optional ByVal n9 As Integer = cnDummy) As Boolean
        Return nValue = n0 OrElse nValue = n1 OrElse nValue = n2 OrElse nValue = n3 OrElse nValue = n4 OrElse nValue = n5 OrElse nValue = n6 OrElse nValue = n7 OrElse nValue = n8 OrElse nValue = n9
    End Function
End Module
