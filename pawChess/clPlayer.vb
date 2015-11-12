Option Strict On
Option Explicit On

Public Class clPlayer

    Public Property PlayerColor As mdSettings.enPlayerColor
    Public Property PlayerType As mdSettings.enPlayerType

    Public Sub New(ByVal oColor As mdSettings.enPlayerColor, ByVal oPlayerType As mdSettings.enPlayerType)
        PlayerColor = oColor
        PlayerType = oPlayerType
    End Sub

End Class
