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
    End Sub

End Class
