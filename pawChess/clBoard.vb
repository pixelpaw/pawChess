﻿Option Strict On
Option Explicit On

Public Class clBoard
    Inherits Panel

    ' Controls GameField
    Public GamePanel As Panel = Nothing
    Public InnerPanel As Panel = Nothing

    ' Controls Log
    Public LogPanel As Panel = Nothing
    Public lblPlayer As Label = Nothing
    Public lblFieldInfo As Label = Nothing

    Public colFields As New Generic.Dictionary(Of String, ucField)
    Public colFigures As New Generic.List(Of clChessFigure)

    Public Delegate Sub FieldClickHandler(ByVal col As Integer, ByVal row As Integer)
    Public Delegate Sub FieldGlowHandler(ByVal col As Integer, ByVal row As Integer)

    Public Event tmp_Field_Click As FieldClickHandler
    Public Event tmp_Field_MouseEnter As FieldGlowHandler
    Public Event tmp_Field_MouseLeave As FieldGlowHandler

    Dim nFieldSize As Integer

    Public Sub New()
        Me.BackColor = Color.Transparent
        Me.Size = frmMain.ClientSize
        Me.nFieldSize = mdSettings.mnSize_Big

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

        InnerPanel = New Panel
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

        lblPlayer = New Label
        lblPlayer.Parent = LogPanel
        lblPlayer.Size = New Size(LogPanel.Width - mdSettings.mnDefaultPos * 2, mdSettings.mnSize_LogLabel)
        lblPlayer.Location = New Point(mdSettings.mnDefaultPos, mdSettings.mnDefaultPos)
        lblPlayer.BackColor = Color.Transparent
        lblPlayer.TextAlign = ContentAlignment.MiddleCenter
        lblPlayer.Font = mdSettings.mFont_Small_Regular
        lblPlayer.BorderStyle = BorderStyle.FixedSingle

        LogPanel.Controls.Add(lblPlayer)

        lblFieldInfo = New Label
        lblFieldInfo.Parent = LogPanel
        lblFieldInfo.Size = New Size(LogPanel.Width - mdSettings.mnDefaultPos * 2, mdSettings.mnSize_LogLabel)
        lblFieldInfo.Location = New Point(mdSettings.mnDefaultPos, LogPanel.Size.Height - mdSettings.mnSize_LogLabel - mdSettings.mnDefaultPos - 2)
        lblFieldInfo.BackColor = Color.Transparent
        lblFieldInfo.TextAlign = ContentAlignment.MiddleCenter
        lblFieldInfo.Font = mdSettings.mFont_Small_Regular
        lblFieldInfo.BorderStyle = BorderStyle.FixedSingle

        LogPanel.Controls.Add(lblFieldInfo)

        Dim bBrightField As Boolean = True
        For i As Integer = 0 To 9
            For j As Integer = 0 To 9
                Dim strIndex As String = GetFieldIndex(j, i)
                Dim strNameH As String = mdSettings.mstrNameSpaceH.Substring(j, 1)
                Dim strNameV As String = mdSettings.mstrNameSpaceV.Substring(i, 1)
                Dim strName As String = strNameH & strNameV

                Dim oTyp As mdSettings.enFieldTyp
                If (i = 0 Or i = 9) AndAlso (j = 0 Or j = 9) Then
                    oTyp = enFieldTyp.Corner
                ElseIf (i = 0 Or i = 9) AndAlso (j >= 1 And j <= 8) Then
                    oTyp = enFieldTyp.MapHorizontal
                ElseIf (i >= 1 And i <= 8) AndAlso (j = 0 Or j = 9) Then
                    oTyp = enFieldTyp.MapVertical
                ElseIf (i >= 1 And i <= 8) And (j >= 1 And j <= 8) Then
                    oTyp = If(bBrightField, enFieldTyp.Bright, enFieldTyp.Dark)
                End If

                Dim oNewField As New ucField(oTyp)
                oNewField.Name = strName
                oNewField.Index = strIndex
                oNewField.IndexCol = j
                oNewField.IndexRow = i

                If oTyp = enFieldTyp.MapHorizontal Then
                    oNewField.InnerField.Text = strNameH
                ElseIf oTyp = enFieldTyp.MapVertical Then
                    oNewField.InnerField.Text = strNameV
                End If

                Dim nPosX, nPosY As Integer
                If oNewField.IsChessField Then
                    nPosX = mdSettings.mnSize_Big * Math.Max(j - 1, 0)
                    nPosY = mdSettings.mnSize_Big * Math.Max(i - 1, 0)

                    oNewField.Parent = InnerPanel
                    oNewField.Location = New Point(nPosX, nPosY)

                    InnerPanel.Controls.Add(oNewField)
                Else
                    nPosX = mdSettings.mnSize_Small * If(j = 0, 0, 1) + mdSettings.mnSize_Big * Math.Max(j - 1, 0)
                    nPosY = mdSettings.mnSize_Small * If(i = 0, 0, 1) + mdSettings.mnSize_Big * Math.Max(i - 1, 0)

                    If oTyp = enFieldTyp.MapHorizontal Then nPosX += 1
                    If oTyp = enFieldTyp.MapVertical Then nPosY += 1

                    oNewField.Parent = GamePanel
                    oNewField.Location = New Point(nPosX, nPosY)

                    GamePanel.Controls.Add(oNewField)
                End If

                AddHandler oNewField.InnerField.Click, AddressOf Field_Click
                AddHandler oNewField.InnerField.MouseEnter, AddressOf Field_MouseEnter
                AddHandler oNewField.InnerField.MouseLeave, AddressOf Field_MouseLeave

                colFields.Add(oNewField.Index, oNewField)

                If mdTools.IsBetween(i, 1, 8) AndAlso mdTools.IsBetween(j, 1, 8) Then bBrightField = Not bBrightField
            Next

            If mdTools.IsBetween(i, 1, 8) Then bBrightField = Not bBrightField
        Next

    End Sub

    Public Function GetFieldByName(ByVal strName As String) As ucField
        Dim oResult As ucField = Nothing

        For Each oPair As KeyValuePair(Of String, ucField) In colFields
            Dim oField As ucField = CType(oPair.Value, ucField)
            If oField.Name = strName Then oResult = oField
        Next

        Return oResult
    End Function

    Public Function GetField(ByVal strFieldIndex As String) As ucField
        Return Me.colFields(strFieldIndex)
    End Function

    Public Function GetField(ByVal nCol As Integer, ByVal nRow As Integer) As ucField
        Return GetField(GetFieldIndex(nCol, nRow))
    End Function

    Public Shared Function GetFieldIndex(ByVal nCol As Integer, ByVal nRow As Integer) As String
        Return nRow.ToString & nCol.ToString
    End Function

    Public Sub GlowOff()
        For Each oPair As KeyValuePair(Of String, ucField) In colFields
            CType(oPair.Value, ucField).GlowOff()
        Next
    End Sub

    Public Sub GlowFields(ByVal oListOfFields As Generic.List(Of String), ByVal bGlowOn As Boolean, ByVal GlowMode As mdSettings.enGlowMode)
        For Each strIndex In oListOfFields
            GetField(strIndex).Glow(bGlowOn, GlowMode)
        Next
    End Sub

    Public Sub GlowRow(ByVal oCurField As ucField, ByVal bGlowOn As Boolean)
        Select Case oCurField.FieldTyp
            Case enFieldTyp.Corner
                Dim nColStart As Integer = oCurField.IndexCol
                Dim nRowStart As Integer = oCurField.IndexRow

                Select Case oCurField.Index
                    Case "00", "99"
                        For i As Integer = 0 To 9
                            GetField(GetFieldIndex(i, i)).Glow(bGlowOn)
                        Next

                    Case "09", "90"
                        For i As Integer = 0 To 9
                            GetField(GetFieldIndex(9 - i, i)).Glow(bGlowOn)
                        Next

                End Select

            Case enFieldTyp.MapHorizontal
                For i As Integer = 0 To 9
                    GetField(GetFieldIndex(oCurField.IndexCol, i)).Glow(bGlowOn)
                Next

            Case enFieldTyp.MapVertical
                For i As Integer = 0 To 9
                    GetField(GetFieldIndex(i, oCurField.IndexRow)).Glow(bGlowOn)
                Next

            Case enFieldTyp.Bright, enFieldTyp.Dark
                For i As Integer = 0 To 9
                    GetField(GetFieldIndex(oCurField.IndexCol, i)).Glow(bGlowOn)
                Next

                For i As Integer = 0 To 9
                    GetField(GetFieldIndex(i, oCurField.IndexRow)).Glow(bGlowOn)
                Next
        End Select
    End Sub

    Public Sub Field_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)
        RaiseEvent tmp_Field_Click(oField.IndexCol, oField.IndexRow)
    End Sub

    Public Sub Field_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)

        GlowRow(oField, True)

        RaiseEvent tmp_Field_MouseEnter(oField.IndexCol, oField.IndexRow)
    End Sub

    Public Sub Field_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)

        GlowRow(oField, False)

        RaiseEvent tmp_Field_MouseLeave(oField.IndexCol, oField.IndexRow)
    End Sub

#Region "Resize - ToDo"

    Public Sub ResizeBoardControls(ByVal bIsMaximized As Boolean)
        If GamePanel IsNot Nothing AndAlso LogPanel IsNot Nothing Then
            Dim nNewSize_GamePanel As Integer = Math.Min(frmMain.ClientSize.Width, frmMain.ClientSize.Height) - mdSettings.mnDefaultPos * 2
            Dim nNewSize_InnerPanel As Integer = nNewSize_GamePanel - mdSettings.mnSize_Small * 2

            Dim nCalc As Double = (nNewSize_InnerPanel - 2) / 8
            While Not nCalc = Int(nCalc)
                nNewSize_GamePanel -= 1
                nNewSize_InnerPanel = nNewSize_GamePanel - mdSettings.mnSize_Small * 2
                nCalc = (nNewSize_InnerPanel - 2) / 8
            End While

            If bIsMaximized Then
                Me.nFieldSize = CInt(nCalc)
            Else
                Me.nFieldSize = mdSettings.mnSize_Big
            End If

            GamePanel.Size = New Size(nNewSize_GamePanel, nNewSize_GamePanel)
            InnerPanel.Size = New Size(nNewSize_InnerPanel, nNewSize_InnerPanel)

            LogPanel.Size = New Size(LogPanel.Width, nNewSize_GamePanel)
            LogPanel.Location = New Point(GamePanel.Width + mdSettings.mnDefaultPos * 2, mdSettings.mnDefaultPos)

            lblFieldInfo.Location = New Point(mdSettings.mnDefaultPos, LogPanel.Size.Height - mdSettings.mnSize_LogLabel - mdSettings.mnDefaultPos - 2)

            Me.Size = frmMain.ClientSize

            If bIsMaximized Then
                Dim nX1 As Integer = CInt((mdSettings.mScreenWidth - GamePanel.Width - LogPanel.Width - mdSettings.mnDefaultPos) / 2)
                Dim nX2 As Integer = GamePanel.Width + GamePanel.Location.X + mdSettings.mnDefaultPos

                GamePanel.Location = New Point(nX1, mdSettings.mnDefaultPos)
                LogPanel.Location = New Point(nX2, mdSettings.mnDefaultPos)
            Else
                GamePanel.Location = New Point(mdSettings.mnDefaultPos, mdSettings.mnDefaultPos)
                LogPanel.Location = New Point(mdSettings.mnSize_GamePanel + mdSettings.mnDefaultPos * 2, mdSettings.mnDefaultPos)
            End If

            For Each oPair As KeyValuePair(Of String, ucField) In colFields
                Dim oField As ucField = GetField(oPair.Key)
                Dim nPosX, nPosY As Integer

                If oField.IsChessField Then
                    nPosX = Me.nFieldSize * Math.Max(oField.IndexCol - 1, 0)
                    nPosY = Me.nFieldSize * Math.Max(oField.IndexRow - 1, 0)

                    oField.Location = New Point(nPosX, nPosY)
                    oField.Size = New Size(Me.nFieldSize, Me.nFieldSize)
                    oField.InnerField.Size = oField.Size
                Else
                    nPosX = mdSettings.mnSize_Small * If(oField.IndexCol = 0, 0, 1) + Me.nFieldSize * Math.Max(oField.IndexCol - 1, 0)
                    nPosY = mdSettings.mnSize_Small * If(oField.IndexRow = 0, 0, 1) + Me.nFieldSize * Math.Max(oField.IndexRow - 1, 0)

                    If oField.FieldTyp = enFieldTyp.MapHorizontal Then nPosX += 1
                    If oField.FieldTyp = enFieldTyp.MapVertical Then nPosY += 1

                    oField.Location = New Point(nPosX, nPosY)

                    Select Case oField.FieldTyp
                        Case enFieldTyp.MapHorizontal : oField.Size = New Size(Me.nFieldSize, mdSettings.mnSize_Small)
                        Case enFieldTyp.MapVertical : oField.Size = New Size(mdSettings.mnSize_Small, Me.nFieldSize)
                    End Select

                    oField.InnerField.Size = oField.Size
                End If
            Next
        End If
    End Sub

    Public Sub ResizeBoard(ByVal bIsMaximized As Boolean)
        ResizeBoardControls(bIsMaximized)

        'Dim tmpWidth As Integer = (GamePanel.Width + LogPanel.Width + mdSettings.mnDefaultPos * 3)
        'Dim tmpHeight As Integer = GamePanel.Height + mdSettings.mnDefaultPos * 2

        'If frmMain.ClientSize.Width < tmpWidth Then
        '    frmMain.ClientSize = New Size(tmpWidth, frmMain.ClientSize.Height)
        'End If

        'If frmMain.ClientSize.Height < tmpHeight Then
        '    frmMain.ClientSize = New Size(frmMain.ClientSize.Width, tmpHeight)
        'End If

        'ResizeBoardControls(bIsMaximized)
    End Sub

#End Region

End Class
