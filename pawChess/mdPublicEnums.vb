Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Module mdPublicEnums

    Public Enum enChessMoveType
        <Description("normaler Zug")> Move
        <Description("Schlag")> Hit
        <Description("kurze Rochade")> RochadeShort
        <Description("lange Rochade")> RochadeLong
        <Description("en Passant")> enPassant
        <Description("Schach")> Chess
        <Description("Schach Matt")> Matt
    End Enum

    Public Enum enFigureMovement
        <Description("nach Unten")> PawnDown
        <Description("nach Oben")> PawnUp
        <Description("normal")> Normal
    End Enum

    Public Enum enFieldTyp
        <Description("Ecke")> Corner
        <Description("Rand Horizontal")> MapHorizontal
        <Description("Rand Vertikal")> MapVertical
        <Description("Helles Schachfeld")> Bright
        <Description("Dunkles Schachfeld")> Dark
        <Description("Platzhalter")> None
    End Enum

    Public Enum enGlowMode
        <Description("schlecht - Figur schlagen")> Bad
        <Description("gut")> Good
        <Description("neutral - normaler Zug")> Neutral
        <Description("aus - Zug verboten")> Off
    End Enum

    Public Enum enPlayerColor
        <Description("Weiss")> White
        <Description("Schwarz")> Black
    End Enum

    Public Enum enPlayerType
        <Description("Computer")> Bot
        <Description("Mensch")> Human
    End Enum

    Public Enum enFigures
        <Description("König")> King
        <Description("Königin")> Queen
        <Description("Turm")> Rook
        <Description("Läufer")> Bishop
        <Description("Springer")> Knight
        <Description("Bauer")> Pawn
    End Enum

    Public Enum enFiguresColored
        <Description("weisser König")> White_King
        <Description("weisse Königin")> White_Queen
        <Description("weisser Turm")> White_Rook
        <Description("weisser Läufer")> White_Bishop
        <Description("weisser Springer")> White_Knight
        <Description("weisser Bauer")> White_Pawn

        <Description("schwarzer König")> Black_King
        <Description("schwarze Königin")> Black_Queen
        <Description("schwarzer Turm")> Black_Rook
        <Description("schwarzer Läufer")> Black_Bishop
        <Description("schwarzer Springer")> Black_Knight
        <Description("schwarzer Bauer")> Black_Pawn
    End Enum

End Module