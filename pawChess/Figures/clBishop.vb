Option Strict On
Option Explicit On

Public Class clBishop
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Bishop
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Bishop, enFiguresColored.White_Bishop)
        Me.FigureID = mdSettings.GetFigureUnicode(Me.FigureColored)
        Me.Value = 3
        Me.MoveCounter = 0
    End Sub

End Class
