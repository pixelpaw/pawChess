Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard = Nothing
    Public Options As clOptions = Nothing

    Public PlayerWhite As clPlayer = Nothing
    Public PlayerBlack As clPlayer = Nothing

    Dim SelectedField As ucField = Nothing
    Dim CurPlayer As mdPublicEnums.enPlayerColor

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
    End Sub

    Public Sub NewGame(Optional ByVal nStartAufstellung As Integer = 0)
        Board.Reset()
        Board.SetFiguresStartingPositions(nStartAufstellung)

        Options = New clOptions()

        PlayerWhite = Me.Options.PlayerWhite
        PlayerBlack = Me.Options.PlayerBlack

        UpdatePlayer(True)
    End Sub

    Public Sub Reset()
        Board.Reset()
    End Sub

    Public Sub ResizeBoard(ByVal bIsMaximized As Boolean)
        If Board IsNot Nothing Then Board.ResizeBoard(bIsMaximized)
    End Sub

    Public Sub DisposeSelectedField()
        SelectedField = Nothing
        Board.Clear()
    End Sub

    Public Sub Board_Log_Actions(ByVal oMove As clMove) Handles Board.tmp_Log_Click, Board.tmp_Log_MouseMove
        If oMove Is Nothing Then
            Board.Clear()
        Else
            Board.lblFieldInfo.Text = oMove.MoveStringText
        End If
    End Sub

    Public Sub Board_Field_Click(ByVal CurrentField As ucField) Handles Board.tmp_Field_Click
        If CurrentField.IsChessField _
            AndAlso SelectedField IsNot Nothing _
            AndAlso (CurrentField.GlowState = enGlowMode.Move Or CurrentField.GlowState = enGlowMode.Hit) _
            AndAlso (CurrentField.Figure Is Nothing OrElse CurrentField.Figure.PlayerColor <> CurPlayer) Then

            If Board.MoveFigure(CurrentField, SelectedField) Then
                SelectedField = Nothing
                Board.Clear()
                UpdatePlayer()
            End If
        Else
            If Not CurrentField.IsChessField Then
                DisposeSelectedField()

            ElseIf CurrentField.Figure Is Nothing Then
                DisposeSelectedField()

            ElseIf CurrentField.Figure.PlayerColor <> CurPlayer Then
                DisposeSelectedField()

            ElseIf SelectedField Is Nothing Then
                SelectedField = CurrentField
                SelectedField.GlowState = enGlowMode.Good

            Else
                If SelectedField.Index = CurrentField.Index Then
                    DisposeSelectedField()
                    mdTools.CheckMovement(Board, CurrentField)
                Else
                    DisposeSelectedField()
                    mdTools.CheckMovement(Board, CurrentField)
                    SelectedField = CurrentField
                    SelectedField.GlowState = enGlowMode.Good
                End If
            End If
        End If
    End Sub

    Public Sub Board_Field_MouseEnter(ByVal oCurrentField As ucField) Handles Board.tmp_Field_MouseEnter
        If SelectedField IsNot Nothing Then Exit Sub

        If oCurrentField.IsChessField Then
            Board.lblFieldInfo.Text = "Feld " & oCurrentField.Name & " ( " & oCurrentField.Index & " ) " & If(oCurrentField.Figure IsNot Nothing, " | " & mdTools.GetEnumDescription(oCurrentField.Figure.FigureColored), "")

            If oCurrentField.Figure IsNot Nothing AndAlso oCurrentField.Figure.PlayerColor = CurPlayer Then
                mdTools.CheckMovement(Board, oCurrentField)
            End If
        End If
    End Sub

    Public Sub Board_Field_MouseLeave(ByVal oCurrentField As ucField) Handles Board.tmp_Field_MouseLeave
        If SelectedField IsNot Nothing Then Exit Sub
        Board.Clear()
    End Sub

    Public Sub UpdatePlayer(Optional ByVal bGameStart As Boolean = False)
        If bGameStart Then
            CurPlayer = Me.Options.StartingPlayer
        Else
            CurPlayer = If(CurPlayer = mdPublicEnums.enPlayerColor.White, mdPublicEnums.enPlayerColor.Black, mdPublicEnums.enPlayerColor.White)
        End If

        Board.lblPlayer.Text = mdTools.GetEnumDescription(CurPlayer) & " am Zug"
    End Sub

End Class
