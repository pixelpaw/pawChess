Option Strict On
Option Explicit On

Public Class frmMain

    Public Game As clGameLogic
    Protected screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Protected screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "ppChess"
        Me.Size = New Size(1024, 732)
        Me.MinimumSize = Me.Size
        Me.Location = New Point(CInt((screenWidth - Me.Size.Width) / 4), CInt((screenHeight - Me.Size.Height) / 3))

        InitGame()
    End Sub
    Private Sub InitGame()
        Game = New clGameLogic
    End Sub

End Class
