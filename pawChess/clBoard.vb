Option Strict On
Option Explicit On

Public Class clBoard
    Inherits Panel

    Public GamePanel As Panel = Nothing
    Public LogPanel As Panel = Nothing

    Public colFields As New Generic.Dictionary(Of String, ucField)
    Public colFigures As New Generic.List(Of clChessFigure)

    Public Delegate Sub FieldClickHandler(ByVal col As Integer, ByVal row As Integer)
    Public Delegate Sub FieldGlowHandler(ByVal col As Integer, ByVal row As Integer)

    Public Event tmp_Field_Click As FieldClickHandler
    Public Event tmp_Field_MouseEnter As FieldGlowHandler
    Public Event tmp_Field_MouseLeave As FieldGlowHandler

    Public Sub New()
        Me.BackColor = Color.Transparent
        Me.Size = frmMain.ClientSize
        frmMain.Controls.Add(Me)
    End Sub

    Public Sub DrawBoard()
        ' Schachfeld
        GamePanel = New Panel
        GamePanel.Parent = Me
        GamePanel.Location = New Point(mdSettings.mnDefaultPos, mdSettings.mnDefaultPos)
        GamePanel.BackColor = mdSettings.moColor_GamePanel
        GamePanel.Size = New Size(mdSettings.mnSize_GamePanel, mdSettings.mnSize_GamePanel)
        GamePanel.MinimumSize = GamePanel.Size
        GamePanel.Padding = New Padding(0)
        GamePanel.Margin = New Padding(0)
        GamePanel.BorderStyle = BorderStyle.FixedSingle

        Me.Controls.Add(GamePanel)

        Dim InnerPanel As New Panel
        InnerPanel.Parent = GamePanel
        InnerPanel.Location = New Point(mdSettings.mnSize_Small, mdSettings.mnSize_Small)
        InnerPanel.BackColor = mdSettings.moColor_InnerPanel
        InnerPanel.Size = New Size(mdSettings.mnSize_Big * 8 + 2, mdSettings.mnSize_Big * 8 + 2)
        InnerPanel.Padding = New Padding(0)
        InnerPanel.Margin = New Padding(0)
        InnerPanel.BorderStyle = BorderStyle.FixedSingle

        GamePanel.Controls.Add(InnerPanel)

        ' LogPanel
        LogPanel = New Panel
        LogPanel.Parent = Me
        LogPanel.Location = New Point(mdSettings.mnSize_GamePanel + mdSettings.mnDefaultPos * 2, mdSettings.mnDefaultPos)
        LogPanel.BackColor = mdSettings.moColor_CornerField
        LogPanel.Size = New Size(mdSettings.mnSize_LogPanel, mdSettings.mnSize_GamePanel)
        LogPanel.MinimumSize = LogPanel.Size
        LogPanel.Padding = New Padding(0)
        LogPanel.Margin = New Padding(0)
        LogPanel.BorderStyle = BorderStyle.FixedSingle

        Me.Controls.Add(LogPanel)

    End Sub

    Public Sub ResizeBoard()
        If GamePanel IsNot Nothing AndAlso LogPanel IsNot Nothing Then
            GamePanel.Width = Math.Min(frmMain.ClientSize.Width, frmMain.ClientSize.Height) - mdSettings.mnDefaultPos * 2
            GamePanel.Height = GamePanel.Width

            For Each ctr In GamePanel.Controls

                ' alle Panels
                If ctr.GetType() = GetType(Panel) Then
                    Dim oCtr As Panel = CType(ctr, Panel)

                    If oCtr.Name = "InnerPanel" Then
                        oCtr.Width = 12
                        oCtr.Height = oCtr.Width
                    End If
                End If

                ' alle ucFields
                If ctr.GetType() = GetType(ucField) Then
                    Dim oCtr As ucField = CType(ctr, ucField)
                End If
            Next

            LogPanel.Location = New Point(GamePanel.Width + mdSettings.mnDefaultPos * 2, mdSettings.mnDefaultPos)
            LogPanel.Width = frmMain.ClientSize.Width - GamePanel.Width - mdSettings.mnDefaultPos * 3
            LogPanel.Height = GamePanel.Height

            If frmMain.ClientSize.Width < (GamePanel.Width + LogPanel.Width + mdSettings.mnDefaultPos * 3) Then
                frmMain.ClientSize = New Size((GamePanel.Width + LogPanel.Width + mdSettings.mnDefaultPos * 3), frmMain.ClientSize.Height)
            End If

            If frmMain.ClientSize.Height < (GamePanel.Height + mdSettings.mnDefaultPos * 2) Then
                frmMain.ClientSize = New Size(frmMain.ClientSize.Width, GamePanel.Height + mdSettings.mnDefaultPos * 2)
            End If

            Me.Size = frmMain.ClientSize

        End If
    End Sub

End Class
