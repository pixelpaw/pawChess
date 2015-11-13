Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard = Nothing
    Public PlayerWhite As clPlayer = Nothing
    Public PlayerBlack As clPlayer = Nothing

    Dim CurPlayer As mdSettings.enPlayerColor

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
        Board.SetFiguresStartingPositions()

        PlayerWhite = New clPlayer(enPlayerColor.White, enPlayerType.Human)
        PlayerBlack = New clPlayer(enPlayerColor.Black, enPlayerType.Human)

        CurPlayer = mdSettings.enPlayerColor.White

        UpdatePlayer()
    End Sub

    Public Sub ResizeBoard(ByVal bIsMaximized As Boolean)
        If Board IsNot Nothing Then Board.ResizeBoard(bIsMaximized)
    End Sub

    Public Sub Board_Field_Click(ByVal oField As ucField) Handles Board.tmp_Field_Click
        oField.GlowState = enGlowMode.Good
    End Sub

    Public Sub Board_Field_MouseEnter(ByVal oField As ucField) Handles Board.tmp_Field_MouseEnter
        If oField.IsChessField Then
            Board.lblFieldInfo.Text = "Feld " & oField.Name & " ( " & oField.Index & " ) " & If(oField.Figure IsNot Nothing, " | " & GetDescription(oField.Figure.FigureColored), "")

            If oField.Figure IsNot Nothing AndAlso oField.Figure.PlayerColor = CurPlayer Then
                oField.GlowState = enGlowMode.Neutral
                CheckMovement(oField)
            End If
        End If
    End Sub

    Public Sub Board_Field_MouseLeave(ByVal oField As ucField) Handles Board.tmp_Field_MouseLeave
        Board.GlowOff()
        Board.ClearLog()
    End Sub

    Public Sub UpdatePlayer()
        Board.lblPlayer.Text = mdSettings.GetDescription(CurPlayer) & " am zug"
    End Sub

    Public Sub CheckMovement(ByVal oCurrentField As ucField)
        Dim oFigure As clChessFigure = oCurrentField.Figure
        Dim nCol As Integer = oCurrentField.IndexCol
        Dim nRow As Integer = oCurrentField.IndexRow

        For Each nDirection As Integer() In oFigure.Directions
            For i As Integer = 1 To oFigure.MaxSteps
                nRow = oCurrentField.IndexRow + (nDirection(0) * i)
                nCol = oCurrentField.IndexCol + (nDirection(1) * i)

                If nCol < 1 Or nCol > 8 Or nRow < 1 Or nRow > 8 Then Continue For

                Dim tmpField As ucField = Board.GetField(nCol, nRow)

                If tmpField.Figure Is Nothing Then
                    tmpField.GlowState = enGlowMode.Neutral
                ElseIf tmpField.Figure.PlayerColor = CurPlayer Then
                    tmpField.GlowState = enGlowMode.Off
                    Exit For
                ElseIf tmpField.Figure.PlayerColor <> CurPlayer Then
                    tmpField.GlowState = enGlowMode.Bad
                    Exit For
                Else
                    tmpField.GlowState = enGlowMode.Off
                End If
            Next

            ' Sonderregeln
            If oFigure.Figure = enFigures.Pawn Then
                ' einmal nach Links
                nRow = oCurrentField.IndexRow + (nDirection(0) * oFigure.MaxSteps)
                nCol = oCurrentField.IndexCol - 1

                If Not (nCol < 1 Or nCol > 8 Or nRow < 1 Or nRow > 8) Then
                    Dim tmpField As ucField = Board.GetField(nCol, nRow)
                    If tmpField.Figure IsNot Nothing Then
                        If tmpField.Figure.PlayerColor <> CurPlayer Then
                            tmpField.GlowState = enGlowMode.Bad
                        Else
                            tmpField.GlowState = enGlowMode.Off
                        End If
                    End If
                End If

                ' einmal nach rechts
                nRow = oCurrentField.IndexRow + (nDirection(0) * oFigure.MaxSteps)
                nCol = oCurrentField.IndexCol + 1

                If Not (nCol < 1 Or nCol > 8 Or nRow < 1 Or nRow > 8) Then
                    Dim tmpField As ucField = Board.GetField(nCol, nRow)
                    If tmpField.Figure IsNot Nothing Then
                        If tmpField.Figure.PlayerColor <> CurPlayer Then
                            tmpField.GlowState = enGlowMode.Bad
                        Else
                            tmpField.GlowState = enGlowMode.Off
                        End If
                    End If
                End If
            End If
        Next

    End Sub

End Class
