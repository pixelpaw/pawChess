Option Strict On
Option Explicit On

Public Class clKnight
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Knight
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Knight, enFiguresColored.White_Knight)
        Me.FigureID = mdTools.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdTools.GetEnumDescription(Me.FigureColored)
        Me.ChessNoteID = mdTools.GetFigureChessNoteID(Me.Figure)
        Me.Value = 3
        Me.MoveCounter = 0
        Me.Movement = mdPublicEnums.enFigureMovement.Normal
        Me.MaxSteps = 1

        Dim ListOfMovementRules As New List(Of clMovementRule)
        ListOfMovementRules.Add(New clMovementRule("L-Schritt oben links", -2, -1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("L-Schritt oben rechts", -2, 1, 1, True, False, False))

        ListOfMovementRules.Add(New clMovementRule("L-Schritt unten rechts", 2, 1, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("L-Schritt unten links", 2, -1, 1, True, False, False))

        ListOfMovementRules.Add(New clMovementRule("L-Schritt rechts oben", -1, 2, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("L-Schritt rechts unten", 1, 2, 1, True, False, False))

        ListOfMovementRules.Add(New clMovementRule("L-Schritt link oben", -1, -2, 1, True, False, False))
        ListOfMovementRules.Add(New clMovementRule("L-Schritt links unten", 1, -2, 1, True, False, False))

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
