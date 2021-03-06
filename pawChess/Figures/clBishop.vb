﻿Option Strict On
Option Explicit On

Public Class clBishop
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Bishop
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Bishop, enFiguresColored.White_Bishop)
        Me.FigureID = mdTools.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdTools.GetEnumDescription(Me.FigureColored)
        Me.ChessNoteID = mdTools.GetFigureChessNoteID(Me.Figure)
        Me.Value = 3
        Me.MoveCounter = 0
        Me.MaxSteps = 7

        Dim ListOfMovementRules As New List(Of clMoveRule)
        ListOfMovementRules.Add(New clMoveRule("diagonal oben links", -1, -1, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal oben rechts", -1, 1, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal unten rechts", 1, 1, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("diagonal unten links", 1, -1, 7, True, False, False))

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
