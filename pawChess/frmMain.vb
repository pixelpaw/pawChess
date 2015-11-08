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
        Dim bResizeControls As Boolean = False
        Dim nClientSizeWidth As Integer = Me.ClientSize.Width
        Dim nClientSizeHeight As Integer = Me.ClientSize.Height

        If Me.WindowState = FormWindowState.Maximized Then
        Else
            While (nClientSizeWidth Mod 2 <> 0) Or (nClientSizeHeight Mod 2 <> 0)
                If (nClientSizeWidth Mod 2 <> 0) Then nClientSizeWidth += 1
                If (nClientSizeHeight Mod 2 <> 0) Then nClientSizeHeight += 1
            End While

            If (nClientSizeWidth Mod 2 = 0) And (nClientSizeHeight Mod 2 = 0) Then
                bResizeControls = True
            End If
        End If

        If bResizeControls AndAlso Game IsNot Nothing Then
            Game.ResizeBoard()
        End If
    End Sub

End Class
