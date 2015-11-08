Option Strict On
Option Explicit On

Public Class clGameLogic

    Public WithEvents Board As clBoard = Nothing

    Public Sub New()
        Board = New clBoard
        Board.DrawBoard()
        'Board.SetFiguresStartingPositions()
    End Sub

    Public Sub ResizeBoard()
        If Board IsNot Nothing Then Board.ResizeBoard()
    End Sub

End Class
