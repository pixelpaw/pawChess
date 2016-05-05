Option Strict On
Option Explicit On

Public Class clBoard
    Inherits Panel

    ' Controls GameField
    Public WithEvents GamePanel As Panel = Nothing
    Public WithEvents InnerPanel As Panel = Nothing

    ' Controls Log
    Public Property Log As clLog = Nothing

    Public LogPanel As Panel = Nothing
    Public lblPlayer As Label = Nothing
    Public lblFieldInfo As Label = Nothing
    Public lvHistory As ListView = Nothing

    Public colFields As New Generic.Dictionary(Of String, ucField)
    Public colChessFields As New Generic.Dictionary(Of String, ucField)
    'Public colFigures As New Generic.List(Of clChessFigure)
    'Public colChessMoves As New Generic.Dictionary(Of Integer, clMove)

    Public Delegate Sub FieldClickHandler(ByVal oField As ucField)
    Public Delegate Sub FieldMouseMoveHandler(ByVal oField As ucField)

    Public Delegate Sub LogClickHandler(ByVal oMove As clMove)
    Public Delegate Sub LogMouseMoveHandler(ByVal oMove As clMove)

    Public Event tmp_Field_Click As FieldClickHandler
    Public Event tmp_Field_MouseEnter As FieldMouseMoveHandler
    Public Event tmp_Field_MouseLeave As FieldMouseMoveHandler

    Public Event tmp_Log_Click As LogClickHandler
    Public Event tmp_Log_MouseMove As LogMouseMoveHandler

    Dim nFieldSize As Integer

    Public Sub New()
        Me.Log = New clLog()

        Me.BackColor = Color.SaddleBrown
        Me.Size = frmMain.ClientSize
        Me.nFieldSize = mdDefaultValues.mnSize_Big

        frmMain.Controls.Add(Me)
    End Sub

    Public Sub Reset()
        For Each oPair As KeyValuePair(Of String, ucField) In colFields
            CType(oPair.Value, ucField).Reset()
        Next

        lblFieldInfo.Text = ""
        lblPlayer.Text = ""

        Me.Log = New clLog()
        lvHistory.Items.Clear()
    End Sub

    Public Sub Clear()
        Me.GlowOff()
        lblFieldInfo.Text = ""
    End Sub

    Public Function MoveFigure(ByVal TargetField As ucField, ByVal SourceField As ucField) As Boolean
        Dim oMoveResult As New clMoveResult(Me, TargetField, SourceField)
        Dim oMove As New clMove(SourceField, TargetField, Log.ChessMoveCount, oMoveResult)

        TargetField.Figure = SourceField.Figure
        SourceField.Figure = Nothing
        TargetField.Figure.MoveCounter += 1

        WriteLog(oMove)

        Return True
    End Function

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
        Return nCol.ToString & nRow.ToString
    End Function

    Public Sub GlowOff()
        For Each oPair As KeyValuePair(Of String, ucField) In colFields
            CType(oPair.Value, ucField).GlowOff()
        Next
    End Sub

    Public Sub Log_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oMove As clMove = Nothing

        If lvHistory.Items.Count > 0 AndAlso lvHistory.SelectedItems.Count > 1 Then
            Dim oItem As ListViewItem = lvHistory.SelectedItems.Item(0)
            oMove = Me.Log.LogEntryList.Item(CInt(oItem.SubItems(0).Text))
        End If

        RaiseEvent tmp_Log_Click(oMove)
    End Sub

    Public Sub Log_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim oMove As clMove = Nothing

        If lvHistory.Items.Count > 0 AndAlso lvHistory.GetItemAt(e.X, e.Y) IsNot Nothing Then
            Dim oItem As ListViewItem = lvHistory.GetItemAt(e.X, e.Y)
            oMove = Me.Log.LogEntryList.Item(CInt(oItem.SubItems(0).Text))
        End If

        RaiseEvent tmp_Log_MouseMove(oMove)
    End Sub

    Public Sub Field_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)

        RaiseEvent tmp_Field_Click(oField)
    End Sub

    Public Sub Field_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)

        RaiseEvent tmp_Field_MouseEnter(oField)
    End Sub

    Public Sub Field_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Dim oLabel As Label = CType(sender, Label)
        Dim oField As ucField = CType(oLabel.Parent, ucField)

        RaiseEvent tmp_Field_MouseLeave(oField)
    End Sub

    Public Sub WriteLog(ByVal oMove As clMove)
        lvHistory.Items.Add(Log.Write(oMove))
    End Sub

    Public Sub DrawBoard()
        ' Schachfeld
        GamePanel = New Panel
        GamePanel.Parent = Me
        GamePanel.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos + 24)
        GamePanel.BackColor = mdDefaultValues.moColor_GamePanel
        GamePanel.Size = New Size(mdDefaultValues.mnSize_GamePanel, mdDefaultValues.mnSize_GamePanel)
        GamePanel.MinimumSize = GamePanel.Size
        GamePanel.Padding = New Padding(0)
        GamePanel.Margin = New Padding(0)
        GamePanel.BorderStyle = BorderStyle.FixedSingle

        Me.Controls.Add(GamePanel)

        InnerPanel = New Panel
        InnerPanel.Parent = GamePanel
        InnerPanel.Location = New Point(mdDefaultValues.mnSize_Small, mdDefaultValues.mnSize_Small)
        InnerPanel.BackColor = mdDefaultValues.moColor_InnerPanel
        InnerPanel.Size = New Size(mdDefaultValues.mnSize_Big * 8 + 2, mdDefaultValues.mnSize_Big * 8 + 2)
        InnerPanel.Padding = New Padding(0)
        InnerPanel.Margin = New Padding(0)
        InnerPanel.BorderStyle = BorderStyle.FixedSingle

        GamePanel.Controls.Add(InnerPanel)

        ' LogPanel
        LogPanel = New Panel
        LogPanel.Parent = Me
        LogPanel.Location = New Point(mdDefaultValues.mnSize_GamePanel + mdDefaultValues.mnDefaultPos * 2, mdDefaultValues.mnDefaultPos + mdDefaultValues.mnSize_Menu_Height)
        LogPanel.BackColor = mdDefaultValues.moColor_CornerField
        LogPanel.Size = New Size(mdDefaultValues.mnSize_LogPanel, mdDefaultValues.mnSize_GamePanel)
        LogPanel.MinimumSize = LogPanel.Size
        LogPanel.Padding = New Padding(0)
        LogPanel.Margin = New Padding(0)
        LogPanel.BorderStyle = BorderStyle.FixedSingle

        Me.Controls.Add(LogPanel)

        lblPlayer = New Label
        lblPlayer.Parent = LogPanel
        lblPlayer.Size = New Size(LogPanel.Width - mdDefaultValues.mnDefaultPos * 2, mdDefaultValues.mnSize_LogLabel)
        lblPlayer.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos)
        lblPlayer.BackColor = Color.Transparent
        lblPlayer.TextAlign = ContentAlignment.MiddleCenter
        lblPlayer.Font = mdDefaultValues.mFont_Small_Regular
        lblPlayer.BorderStyle = BorderStyle.FixedSingle

        LogPanel.Controls.Add(lblPlayer)

        lblFieldInfo = New Label
        lblFieldInfo.Parent = LogPanel
        lblFieldInfo.Size = New Size(LogPanel.Width - mdDefaultValues.mnDefaultPos * 2, mdDefaultValues.mnSize_LogLabel)
        lblFieldInfo.Location = New Point(mdDefaultValues.mnDefaultPos, LogPanel.Size.Height - mdDefaultValues.mnSize_LogLabel - mdDefaultValues.mnDefaultPos - 2)
        lblFieldInfo.BackColor = Color.Transparent
        lblFieldInfo.TextAlign = ContentAlignment.MiddleCenter
        lblFieldInfo.Font = mdDefaultValues.mFont_Small_Regular
        lblFieldInfo.BorderStyle = BorderStyle.FixedSingle

        LogPanel.Controls.Add(lblFieldInfo)

        lvHistory = New ListView()
        lvHistory.Size = GetHistoryListViewSize(LogPanel.Height, LogPanel.Width)
        lvHistory.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos + mdDefaultValues.mnSize_LogLabel + mdDefaultValues.mnDefaultPos)
        lvHistory.BackColor = LogPanel.BackColor
        lvHistory.Font = mdDefaultValues.mFont_Small_Regular
        lvHistory.BorderStyle = BorderStyle.FixedSingle
        lvHistory.HeaderStyle = ColumnHeaderStyle.None
        lvHistory.View = View.Details
        lvHistory.AllowColumnReorder = False
        lvHistory.MultiSelect = False
        lvHistory.FullRowSelect = True
        lvHistory.GridLines = True
        lvHistory.Sorting = SortOrder.Descending

        lvHistory.Columns.Add("colMoveNr", mdDefaultValues.mnSize_Small)
        lvHistory.Columns.Add("colTimeStamp", 75, HorizontalAlignment.Center)
        lvHistory.Columns.Add("colPlayerColor", 75, HorizontalAlignment.Left)
        lvHistory.Columns.Add("colMoveString", -2, HorizontalAlignment.Left)

        LogPanel.Controls.Add(lvHistory)

        AddHandler lvHistory.Click, AddressOf Log_Click
        AddHandler lvHistory.MouseMove, AddressOf Log_MouseMove

        Dim bBrightField As Boolean = True
        For i As Integer = 0 To 9
            For j As Integer = 0 To 9
                Dim strIndex As String = GetFieldIndex(j, i)
                Dim strNameH As String = mdDefaultValues.mstrNameSpaceH.Substring(j, 1)
                Dim strNameV As String = mdDefaultValues.mstrNameSpaceV.Substring(i, 1)
                Dim strName As String = strNameH & strNameV

                Dim oTyp As mdPublicEnums.enFieldTyp = mdPublicEnums.enFieldTyp.None
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
                    nPosX = mdDefaultValues.mnSize_Big * Math.Max(j - 1, 0)
                    nPosY = mdDefaultValues.mnSize_Big * Math.Max(i - 1, 0)

                    oNewField.Parent = InnerPanel
                    oNewField.Location = New Point(nPosX, nPosY)

                    InnerPanel.Controls.Add(oNewField)
                Else
                    nPosX = mdDefaultValues.mnSize_Small * If(j = 0, 0, 1) + mdDefaultValues.mnSize_Big * Math.Max(j - 1, 0)
                    nPosY = mdDefaultValues.mnSize_Small * If(i = 0, 0, 1) + mdDefaultValues.mnSize_Big * Math.Max(i - 1, 0)

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
                If oNewField.IsChessField Then colChessFields.Add(oNewField.Index, oNewField)

                If mdTools.IsBetween(i, 1, 8) AndAlso mdTools.IsBetween(j, 1, 8) Then bBrightField = Not bBrightField
            Next

            If mdTools.IsBetween(i, 1, 8) Then bBrightField = Not bBrightField
        Next

    End Sub

    Public Sub SetFiguresStartingPositions(Optional ByVal nStartAufstellung As Integer = 0)
        ' Testfall 1
        If 1 = nStartAufstellung Then
            Me.colFields(GetFieldIndex(1, 1)).Figure = New clKnight(enPlayerColor.White)
            Me.colFields(GetFieldIndex(2, 3)).Figure = New clBishop(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(5, 3)).Figure = New clRook(enPlayerColor.Black)

            Exit Sub
        End If

        ' Testfall 2
        If 2 = nStartAufstellung Then
            Me.colFields(GetFieldIndex(4, 5)).Figure = New clKnight(enPlayerColor.White)
            Me.colFields(GetFieldIndex(3, 3)).Figure = New clKnight(enPlayerColor.White)
            Me.colFields(GetFieldIndex(8, 4)).Figure = New clRook(enPlayerColor.White)
            Me.colFields(GetFieldIndex(3, 5)).Figure = New clPawn(enPlayerColor.White)
            Me.colFields(GetFieldIndex(3, 7)).Figure = New clPawn(enPlayerColor.White)
            Me.colFields(GetFieldIndex(2, 7)).Figure = New clPawn(enPlayerColor.White)
            Me.colFields(GetFieldIndex(1, 7)).Figure = New clPawn(enPlayerColor.White)
            Me.colFields(GetFieldIndex(1, 6)).Figure = New clBishop(enPlayerColor.White)

            Me.colFields(GetFieldIndex(2, 4)).Figure = New clQueen(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(4, 4)).Figure = New clBishop(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(2, 6)).Figure = New clKing(enPlayerColor.Black)

            Exit Sub
        End If

        ' Testfall 3
        If 3 = nStartAufstellung Then
            Me.colFields(GetFieldIndex(8, 4)).Figure = New clRook(enPlayerColor.White)

            Me.colFields(GetFieldIndex(4, 4)).Figure = New clRook(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(2, 6)).Figure = New clKing(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(6, 8)).Figure = New clBishop(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(6, 5)).Figure = New clKnight(enPlayerColor.Black)
            Me.colFields(GetFieldIndex(8, 1)).Figure = New clPawn(enPlayerColor.Black)

            Exit Sub
        End If

        ' schwarze Figuren
        ' Bauern
        For i As Integer = 1 To 8
            Me.colFields(GetFieldIndex(i, 2)).Figure = New clPawn(enPlayerColor.Black)
        Next

        ' Türme
        Me.colFields(GetFieldIndex(1, 1)).Figure = New clRook(enPlayerColor.Black)
        Me.colFields(GetFieldIndex(8, 1)).Figure = New clRook(enPlayerColor.Black)

        ' Springer
        Me.colFields(GetFieldIndex(2, 1)).Figure = New clKnight(enPlayerColor.Black)
        Me.colFields(GetFieldIndex(7, 1)).Figure = New clKnight(enPlayerColor.Black)

        ' Läufer
        Me.colFields(GetFieldIndex(3, 1)).Figure = New clBishop(enPlayerColor.Black)
        Me.colFields(GetFieldIndex(6, 1)).Figure = New clBishop(enPlayerColor.Black)

        ' Königin
        Me.colFields(GetFieldIndex(4, 1)).Figure = New clQueen(enPlayerColor.Black)

        ' König
        Me.colFields(GetFieldIndex(5, 1)).Figure = New clKing(enPlayerColor.Black)

        ' weisse Figuren
        ' Bauern
        For i As Integer = 1 To 8
            Me.colFields(GetFieldIndex(i, 7)).Figure = New clPawn(enPlayerColor.White)
        Next

        ' Türme
        Me.colFields(GetFieldIndex(1, 8)).Figure = New clRook(enPlayerColor.White)
        Me.colFields(GetFieldIndex(8, 8)).Figure = New clRook(enPlayerColor.White)

        ' Springer
        Me.colFields(GetFieldIndex(2, 8)).Figure = New clKnight(enPlayerColor.White)
        Me.colFields(GetFieldIndex(7, 8)).Figure = New clKnight(enPlayerColor.White)

        ' Läufer
        Me.colFields(GetFieldIndex(3, 8)).Figure = New clBishop(enPlayerColor.White)
        Me.colFields(GetFieldIndex(6, 8)).Figure = New clBishop(enPlayerColor.White)

        ' Königin
        Me.colFields(GetFieldIndex(4, 8)).Figure = New clQueen(enPlayerColor.White)

        ' König
        Me.colFields(GetFieldIndex(5, 8)).Figure = New clKing(enPlayerColor.White)

    End Sub

    Public Sub ResizeBoardControls(ByVal bIsMaximized As Boolean)
        If GamePanel IsNot Nothing AndAlso LogPanel IsNot Nothing Then
            Dim nNewSize_GamePanel As Integer = Math.Min(Me.Parent.ClientSize.Width, Me.Parent.ClientSize.Height - mdDefaultValues.mnSize_Menu_Height) - mdDefaultValues.mnDefaultPos * 2
            Dim nNewSize_InnerPanel As Integer = nNewSize_GamePanel - mdDefaultValues.mnSize_Small * 2
            Dim oNewFont As Font = mdDefaultValues.mFigures_Font

            Dim nCalc As Double = (nNewSize_InnerPanel - 2) / 8
            While Not nCalc = Int(nCalc)
                nNewSize_GamePanel -= 1
                nNewSize_InnerPanel = nNewSize_GamePanel - mdDefaultValues.mnSize_Small * 2
                nCalc = (nNewSize_InnerPanel - 2) / 8
            End While

            If bIsMaximized Then
                Me.nFieldSize = CInt(nCalc)

                Dim nSteigerung As Double = nCalc / mdDefaultValues.mnSize_Big * 100
                oNewFont = New Font(mdDefaultValues.mFigures_Font.FontFamily, CInt(mdDefaultValues.mFigures_Font.SizeInPoints / 100 * nSteigerung))
            Else
                Me.nFieldSize = mdDefaultValues.mnSize_Big
            End If

            GamePanel.Size = New Size(nNewSize_GamePanel, nNewSize_GamePanel)
            InnerPanel.Size = New Size(nNewSize_InnerPanel, nNewSize_InnerPanel)

            LogPanel.Size = New Size(LogPanel.Width, nNewSize_GamePanel)
            lvHistory.Size = GetHistoryListViewSize(LogPanel.Height, LogPanel.Width)
            lblFieldInfo.Location = New Point(mdDefaultValues.mnDefaultPos, LogPanel.Size.Height - mdDefaultValues.mnSize_LogLabel - mdDefaultValues.mnDefaultPos - 2)

            Me.Size = frmMain.ClientSize

            If bIsMaximized Then
                Dim nX1 As Integer = CInt((Me.Parent.ClientSize.Width - GamePanel.Width - LogPanel.Width - mdDefaultValues.mnDefaultPos) / 2)
                GamePanel.Location = New Point(nX1, mdDefaultValues.mnDefaultPos + mdDefaultValues.mnSize_Menu_Height)
            Else
                GamePanel.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos + mdDefaultValues.mnSize_Menu_Height)
            End If

            LogPanel.Location = New Point(GamePanel.Width + GamePanel.Location.X + mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos + mdDefaultValues.mnSize_Menu_Height)

            For Each oPair As KeyValuePair(Of String, ucField) In colFields
                Dim oField As ucField = GetField(oPair.Key)
                Dim nPosX, nPosY As Integer

                If oField.IsChessField Then
                    nPosX = Me.nFieldSize * Math.Max(oField.IndexCol - 1, 0)
                    nPosY = Me.nFieldSize * Math.Max(oField.IndexRow - 1, 0)

                    oField.Location = New Point(nPosX, nPosY)
                    oField.Size = New Size(Me.nFieldSize, Me.nFieldSize)
                    oField.InnerField.Size = oField.Size
                    oField.InnerField.Font = oNewFont
                Else
                    nPosX = mdDefaultValues.mnSize_Small * If(oField.IndexCol = 0, 0, 1) + Me.nFieldSize * Math.Max(oField.IndexCol - 1, 0)
                    nPosY = mdDefaultValues.mnSize_Small * If(oField.IndexRow = 0, 0, 1) + Me.nFieldSize * Math.Max(oField.IndexRow - 1, 0)

                    If oField.FieldTyp = enFieldTyp.MapHorizontal Then nPosX += 1
                    If oField.FieldTyp = enFieldTyp.MapVertical Then nPosY += 1

                    oField.Location = New Point(nPosX, nPosY)

                    Select Case oField.FieldTyp
                        Case enFieldTyp.MapHorizontal : oField.Size = New Size(Me.nFieldSize, mdDefaultValues.mnSize_Small)
                        Case enFieldTyp.MapVertical : oField.Size = New Size(mdDefaultValues.mnSize_Small, Me.nFieldSize)
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

End Class
