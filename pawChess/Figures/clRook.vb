Option Strict On
Option Explicit On

Public Class clRook
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Rook
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Rook, enFiguresColored.White_Rook)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 5
        Me.MoveCounter = 0
        Me.MaxSteps = 7

        Dim ListOfDirections As New List(Of Integer())
        ListOfDirections.Add(New Integer() {-1, 0})
        ListOfDirections.Add(New Integer() {0, 1})
        ListOfDirections.Add(New Integer() {1, 0})
        ListOfDirections.Add(New Integer() {0, -1})

        Me.Directions = ListOfDirections

        Dim ListOfMovementRules As New List(Of clMovementRule)
        Dim oNewRule As clMovementRule

        oNewRule = New clMovementRule
        oNewRule.Name = "Bewegung nach oben"
        oNewRule.DirectionRow = -1
        oNewRule.DirectionCol = 0
        oNewRule.Steps = 7
        oNewRule.OnlyOnHit = False
        oNewRule.OnlyFirstMove = False

        ListOfMovementRules.Add(oNewRule)

        oNewRule = New clMovementRule
        oNewRule.Name = "Bewegung nach rechts"
        oNewRule.DirectionRow = 0
        oNewRule.DirectionCol = 1
        oNewRule.Steps = 7
        oNewRule.OnlyOnHit = False
        oNewRule.OnlyFirstMove = False

        ListOfMovementRules.Add(oNewRule)

        oNewRule = New clMovementRule
        oNewRule.Name = "Bewegung nach unten"
        oNewRule.DirectionRow = 1
        oNewRule.DirectionCol = 0
        oNewRule.Steps = 7
        oNewRule.OnlyOnHit = False
        oNewRule.OnlyFirstMove = False

        ListOfMovementRules.Add(oNewRule)

        oNewRule = New clMovementRule
        oNewRule.Name = "Bewegung nach links"
        oNewRule.DirectionRow = 0
        oNewRule.DirectionCol = -1
        oNewRule.Steps = 7
        oNewRule.OnlyOnHit = False
        oNewRule.OnlyFirstMove = False

        ListOfMovementRules.Add(oNewRule)

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
