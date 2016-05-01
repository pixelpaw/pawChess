Option Strict On
Option Explicit On

Public Class clOptions

    Public Property StartingPlayer As mdPublicEnums.enPlayerColor
    Public PlayerWhite As clPlayer = Nothing
    Public PlayerBlack As clPlayer = Nothing

    Public Sub New()
        StartingPlayer = enPlayerColor.White
        PlayerWhite = New clPlayer(enPlayerColor.White, enPlayerType.Human)
        PlayerBlack = New clPlayer(enPlayerColor.Black, enPlayerType.Human)
    End Sub

End Class
