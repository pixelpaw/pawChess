Option Strict On
Option Explicit On

Public Class clKing
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.King
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_King, enFiguresColored.White_King)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 1
        Me.MoveCounter = 0
    End Sub

End Class
