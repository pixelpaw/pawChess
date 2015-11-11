Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard = Nothing

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
        Board.SetFiguresStartingPositions()
    End Sub

    Public Sub ResizeBoard(ByVal bIsMaximized As Boolean)
        If Board IsNot Nothing Then Board.ResizeBoard(bIsMaximized)
    End Sub

    Public Sub Board_Field_Click(ByVal nCol As Integer, ByVal nRow As Integer) Handles Board.tmp_Field_Click
        Dim oField As ucField = Board.GetField(nCol, nRow)
        Dim oList As New List(Of String)

        oField.GlowState = enGlowMode.Good

        'oList.Add(oField.Index)
        'Board.GlowFields(oList, True, enGlowMode.Bad)
        'MsgBox(col.ToString & " | " & row.ToString)
    End Sub

    Public Sub Board_Field_MouseEnter(ByVal nCol As Integer, ByVal nRow As Integer) Handles Board.tmp_Field_MouseEnter
        Dim oField As ucField = Board.GetField(nCol, nRow)
        If oField.IsChessField Then
            Board.lblFieldInfo.Text = "Feld " & oField.Name & If(oField.Figure IsNot Nothing, " | " & GetDescription(oField.Figure.FigureColored), "")
        End If
    End Sub

    Public Sub Board_Field_MouseLeaver(ByVal col As Integer, ByVal row As Integer) Handles Board.tmp_Field_MouseLeave
        Board.ClearLog()
    End Sub

End Class
