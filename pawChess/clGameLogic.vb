Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
        'Board.SetFiguresStartingPositions()
    End Sub

End Class
