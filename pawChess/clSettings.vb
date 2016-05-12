Option Strict On
Option Explicit On

Public Class clSettings

    Public Property StartingPlayer As mdPublicEnums.enPlayerColor
    Public PlayerWhite As clPlayer = Nothing
    Public PlayerBlack As clPlayer = Nothing

    Public Sub New()
        LoadDefaults()
    End Sub

    Public Sub LoadDefaults()
        StartingPlayer = enPlayerColor.White
        PlayerWhite = New clPlayer(enPlayerColor.White, enPlayerType.Human)
        PlayerBlack = New clPlayer(enPlayerColor.Black, enPlayerType.Human)
    End Sub

    Public Sub ShowSettings()
        'frmSettings.LoadWithSettings(Me)
    End Sub

End Class
