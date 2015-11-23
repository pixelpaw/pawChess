Option Strict On
Option Explicit On

Public Class clKing
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.King
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_King, enFiguresColored.White_King)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdSettings.GetDescription(Me.FigureColored)
        Me.Value = 1
        Me.MoveCounter = 0
        Me.MaxSteps = 1

        Dim ListOfMovementRules As New List(Of clMovementRule)
        ListOfMovementRules.Add(New clMovementRule("diagonal oben links", -1, -1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("oben", -1, 0, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("diagonal oben rechts", -1, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("rechts", 0, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("diagonal unten rechts", 1, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("unten", 1, 0, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("diagonal unten links", 1, -1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("links", 0, -1, 1, True, False, False))

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
