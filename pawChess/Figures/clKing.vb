Option Strict On
Option Explicit On

Public Class clKing
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.King
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_King, enFiguresColored.White_King)
        Me.FigureID = mdTools.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdTools.GetEnumDescription(Me.FigureColored)
        Me.ChessNoteID = mdTools.GetFigureChessNoteID(Me.Figure)
        Me.Value = 1
        Me.MoveCounter = 0
        Me.MaxSteps = 1

        Dim ListOfMovementRules As New List(Of clMoveRule)
        ListOfMovementRules.Add(New clMoveRule("diagonal oben links", -1, -1, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("oben", -1, 0, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal oben rechts", -1, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("rechts", 0, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal unten rechts", 1, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("unten", 1, 0, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal unten links", 1, -1, 1, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("links", 0, -1, 1, True, False, False))

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
