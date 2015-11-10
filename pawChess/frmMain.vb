Option Strict On
Option Explicit On

Public Class frmMain

    Public Game As clGameLogic = Nothing

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "ppChess"
        Me.ClientSize = New Size(mdSettings.mnSize_ClientSize_Width, mdSettings.mnSize_ClientSize_Height)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Location = New Point(CInt((mdSettings.mScreenWidth - Me.Size.Width) / 4), CInt((mdSettings.mScreenHeight - Me.Size.Height) / 3))

        InitGame()
    End Sub

    Private Sub InitGame()
        Game = New clGameLogic
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        ResizeGame()
    End Sub

    Private Sub frmMain_ResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd
        ResizeGame()
    End Sub

    Private Sub ResizeGame()
        Dim bIsMaximized As Boolean = Me.WindowState = FormWindowState.Maximized

        If Game IsNot Nothing Then
            Game.ResizeBoard(bIsMaximized)
        End If
    End Sub

End Class
