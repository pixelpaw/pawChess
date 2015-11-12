Option Strict On
Option Explicit On

Public Class clPawn
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Pawn
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Pawn, enFiguresColored.White_Pawn)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 1
        Me.MoveCounter = 0
        Me.Movement = If(oPlayerColor = enPlayerColor.Black, enFigureMovement.PawnDown, enFigureMovement.PawnUp)
        Me.MaxSteps = 1

        Dim oDirections As New List(Of Integer())

        If Me.Movement = enFigureMovement.PawnUp Then
            oDirections.Add(New Integer() {-1, 0})
        ElseIf Me.Movement = enFigureMovement.PawnDown Then
            oDirections.Add(New Integer() {1, 0})
        End If

        Me.Directions = oDirections
    End Sub

End Class
