Option Strict On
Option Explicit On

Public Class clPawn
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Pawn
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Pawn, enFiguresColored.White_Pawn)
        Me.FigureID = mdTools.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdTools.GetEnumDescription(Me.FigureColored)
        Me.ChessNoteID = mdTools.GetFigureChessNoteID(Me.Figure)
        Me.Value = 1
        Me.MoveCounter = 0
        Me.Movement = If(oPlayerColor = enPlayerColor.Black, enFigureMovement.PawnDown, enFigureMovement.PawnUp)
        Me.MaxSteps = 1

        Dim ListOfMovementRules As New List(Of clMoveRule)

        If Me.Movement = enFigureMovement.PawnUp Then
            ListOfMovementRules.Add(New clMoveRule("oben", -1, 0, 1, False, False, False))
            ListOfMovementRules.Add(New clMoveRule("Startzug oben", -1, 0, 2, False, False, True))
            ListOfMovementRules.Add(New clMoveRule("diagonal oben links", -1, -1, 1, True, True, False))
            ListOfMovementRules.Add(New clMoveRule("diagonal oben rechts", -1, 1, 1, True, True, False))

        ElseIf Me.Movement = enFigureMovement.PawnDown Then
            ListOfMovementRules.Add(New clMoveRule("unten", 1, 0, 1, False, False, False))
            ListOfMovementRules.Add(New clMoveRule("Startzug unten", 1, 0, 2, False, False, True))
            ListOfMovementRules.Add(New clMoveRule("diagonal unten rechts", 1, 1, 1, True, True, False))
            ListOfMovementRules.Add(New clMoveRule("diagonal unten links", 1, -1, 1, True, True, False))

        End If

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
