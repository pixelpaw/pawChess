Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard = Nothing

    Public PlayerWhite As clPlayer = Nothing
    Public PlayerBlack As clPlayer = Nothing

    Dim SelectedField As ucField = Nothing
    Dim CurPlayer As mdPublicEnums.enPlayerColor

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
        Board.SetFiguresStartingPositions()

        PlayerWhite = New clPlayer(enPlayerColor.White, enPlayerType.Human)
        PlayerBlack = New clPlayer(enPlayerColor.Black, enPlayerType.Human)

        UpdatePlayer(True)
    End Sub

    Public Sub ResizeBoard(ByVal bIsMaximized As Boolean)
        If Board IsNot Nothing Then Board.ResizeBoard(bIsMaximized)
    End Sub

    Public Sub DisposeSelectedField()
        SelectedField = Nothing
        Board.Clear()
    End Sub

    Public Sub Board_Field_Click(ByVal oField As ucField) Handles Board.tmp_Field_Click
        If oField.IsChessField _
            AndAlso SelectedField IsNot Nothing _
            AndAlso (oField.GlowState = enGlowMode.Neutral Or oField.GlowState = enGlowMode.Bad) _
            AndAlso (oField.Figure Is Nothing OrElse oField.Figure.PlayerColor <> CurPlayer) Then

            If Board.MoveFigure(mdTools.GetMovementString(SelectedField, oField)) Then
                SelectedField = Nothing
                Board.Clear()
                UpdatePlayer()
            End If
        Else
            If Not oField.IsChessField Then
                DisposeSelectedField()

            ElseIf oField.Figure Is Nothing Then
                DisposeSelectedField()

            ElseIf oField.Figure.PlayerColor <> CurPlayer Then
                DisposeSelectedField()

            ElseIf SelectedField Is Nothing Then
                SelectedField = oField
                SelectedField.GlowState = enGlowMode.Good

            Else
                If SelectedField.Index = oField.Index Then
                    DisposeSelectedField()
                    CheckMovement(oField)
                Else
                    DisposeSelectedField()
                    CheckMovement(oField)
                    SelectedField = oField
                    SelectedField.GlowState = enGlowMode.Good
                End If
            End If
        End If
    End Sub

    Public Sub Board_Field_MouseEnter(ByVal oField As ucField) Handles Board.tmp_Field_MouseEnter
        If SelectedField IsNot Nothing Then Exit Sub

        If oField.IsChessField Then
            Board.lblFieldInfo.Text = "Feld " & oField.Name & " ( " & oField.Index & " ) " & If(oField.Figure IsNot Nothing, " | " & mdTools.GetEnumDescription(oField.Figure.FigureColored), "")

            If oField.Figure IsNot Nothing AndAlso oField.Figure.PlayerColor = CurPlayer Then
                CheckMovement(oField)
            End If
        End If
    End Sub

    Public Sub Board_Field_MouseLeave(ByVal oField As ucField) Handles Board.tmp_Field_MouseLeave
        If SelectedField IsNot Nothing Then Exit Sub
        Board.Clear()
    End Sub

    Public Sub UpdatePlayer(Optional ByVal bGameStart As Boolean = False)
        If bGameStart Then
            CurPlayer = mdPublicEnums.enPlayerColor.White
        Else
            CurPlayer = If(CurPlayer = mdPublicEnums.enPlayerColor.White, mdPublicEnums.enPlayerColor.Black, mdPublicEnums.enPlayerColor.White)
        End If

        Board.lblPlayer.Text = mdTools.GetEnumDescription(CurPlayer) & " am zug"
    End Sub

    Public Sub CheckMovement(ByVal oCurrentField As ucField)
        oCurrentField.GlowState = enGlowMode.Neutral

        Dim oFigure As clChessFigure = oCurrentField.Figure
        Dim nCol As Integer = oCurrentField.IndexCol
        Dim nRow As Integer = oCurrentField.IndexRow

        For Each Rule As clMovementRule In oFigure.MovementRules
            If Rule.OnlyFirstMove And oFigure.MoveCounter > 0 Then Continue For

            For i As Integer = 1 To Rule.Steps
                nRow = oCurrentField.IndexRow + (Rule.DirectionRow * i)
                nCol = oCurrentField.IndexCol + (Rule.DirectionCol * i)

                If nCol < 1 Or nCol > 8 Or nRow < 1 Or nRow > 8 Then Continue For

                Dim tmpField As ucField = Board.GetField(nCol, nRow)

                If tmpField.Figure Is Nothing Then
                    If Rule.OnlyOnHit Then
                        tmpField.GlowState = enGlowMode.Off
                    Else
                        tmpField.GlowState = enGlowMode.Neutral
                    End If
                Else
                    If tmpField.Figure.PlayerColor = CurPlayer Then
                        tmpField.GlowState = enGlowMode.Off
                        Exit For

                    Else
                        If Rule.AllowHit Then
                            tmpField.GlowState = enGlowMode.Bad
                            Exit For
                        Else
                            tmpField.GlowState = enGlowMode.Off
                            Exit For
                        End If
                    End If
                End If
            Next
        Next

    End Sub

End Class
