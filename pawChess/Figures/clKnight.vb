Option Strict On
Option Explicit On

Public Class clKnight
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Knight
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Knight, enFiguresColored.White_Knight)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 3
        Me.MoveCounter = 0
        Me.Movement = mdSettings.enFigureMovement.Normal
        Me.MaxSteps = 1

        Dim oDirections As New List(Of Integer())
        oDirections.Add(New Integer() {-2, -1})
        oDirections.Add(New Integer() {-2, 1})
        oDirections.Add(New Integer() {-1, -2})
        oDirections.Add(New Integer() {-1, 2})
        oDirections.Add(New Integer() {1, 2})
        oDirections.Add(New Integer() {1, -2})
        oDirections.Add(New Integer() {2, 1})
        oDirections.Add(New Integer() {2, -1})

        Me.Directions = oDirections
    End Sub

End Class
