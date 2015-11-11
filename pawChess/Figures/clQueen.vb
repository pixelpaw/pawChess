Option Strict On
Option Explicit On

Public Class clQueen
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Queen
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Queen, enFiguresColored.White_Queen)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 9
        Me.MoveCounter = 0
    End Sub

End Class
