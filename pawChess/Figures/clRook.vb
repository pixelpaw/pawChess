﻿Option Strict On
Option Explicit On

Public Class clRook
    Inherits clChessFigure

    Public Sub New(ByVal oPlayerColor As enPlayerColor)
        Me.PlayerColor = oPlayerColor
        Me.Figure = enFigures.Rook
        Me.FigureColored = If(oPlayerColor = enPlayerColor.Black, enFiguresColored.Black_Rook, enFiguresColored.White_Rook)
        Me.FigureID = mdTools.GetFigureUnicode(Me.FigureColored)
        Me.FigureName = mdTools.GetEnumDescription(Me.FigureColored)
        Me.ChessNoteID = mdTools.GetFigureChessNoteID(Me.Figure)
        Me.Value = 5
        Me.MoveCounter = 0
        Me.MaxSteps = 7

        Dim ListOfMovementRules As New List(Of clMoveRule)
        ListOfMovementRules.Add(New clMoveRule("oben", -1, 0, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("rechts", 0, 1, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("unten", 1, 0, 7, True, False, False))
        ListOfMovementRules.Add(New clMoveRule("links", 0, -1, 7, True, False, False))

        Me.MovementRules = ListOfMovementRules
    End Sub

End Class
