Option Strict On
Option Explicit On

Public Class clPlayer

    Public Property PlayerColor As mdPublicEnums.enPlayerColor
    Public Property PlayerType As mdPublicEnums.enPlayerType
    Public Property Name As String = ""

    Public Sub New(ByVal oColor As mdPublicEnums.enPlayerColor, ByVal oPlayerType As mdPublicEnums.enPlayerType)
        PlayerColor = oColor
        PlayerType = oPlayerType
    End Sub

End Class
