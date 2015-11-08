Option Strict On
Option Explicit On

Public Class frmMain

    Public Game As clGameLogic = Nothing

    Protected screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Protected screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "ppChess"
        Me.ClientSize = New Size(mdSettings.mnSize_ClientSize_Width, mdSettings.mnSize_ClientSize_Height)
        Me.MinimumSize = Me.Size
        Me.Location = New Point(CInt((screenWidth - Me.Size.Width) / 4), CInt((screenHeight - Me.Size.Height) / 3))

        InitGame()
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ResizeGame()
    End Sub

    Private Sub frmMain_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        ResizeGame()
    End Sub

    Private Sub InitGame()
        Game = New clGameLogic
    End Sub

    Private Sub ResizeGame()
        If Game IsNot Nothing Then Game.ResizeBoard()
    End Sub

End Class
