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
    End Sub

End Class
