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

        Dim dr As DialogResult
        Dim f As New frmSettings()

        dr = f.ShowDialog()
        If dr = DialogResult.OK Then
            MsgBox("User clicked OK button")
        ElseIf dr = DialogResult.Cancel Then
            MsgBox("User clicked Cancel button")
        End If

    End Sub

End Class
