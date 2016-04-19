Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Module mdPublicEnums

    Public Enum enChessMoveType
        <Description("normaler Zug")> Move = 1
        <Description("Schlag")> Hit = 2
        <Description("kurze Rochade")> RochadeShort = 3
        <Description("lange Rochade")> RochadeLong = 4
        <Description("en Passant")> enPassant = 5
        <Description("Schach")> Chess = 6
        <Description("Schach Matt")> Matt = 7
        <Description("Remis")> Remis = 8
    End Enum

    Public Enum enFigureMovement
        <Description("nach Unten")> PawnDown = 1
        <Description("nach Oben")> PawnUp = 2
        <Description("normal")> Normal = 3
    End Enum

    Public Enum enFieldTyp
        <Description("Ecke")> Corner = 1
        <Description("Rand Horizontal")> MapHorizontal = 2
        <Description("Rand Vertikal")> MapVertical = 3
        <Description("Helles Schachfeld")> Bright = 4
        <Description("Dunkles Schachfeld")> Dark = 5
        <Description("Platzhalter")> None = 6
    End Enum

    Public Enum enGlowMode
        <Description("schlecht - Figur schlagen")> Bad = 1
        <Description("gut")> Good = 2
        <Description("neutral - normaler Zug")> Neutral = 3
        <Description("Schach")> Chess = 4
        <Description("aus - Zug verboten")> Off = 9
    End Enum

    Public Enum enPlayerColor
        <Description("Weiss")> White = 1
        <Description("Schwarz")> Black = 2
    End Enum

    Public Enum enPlayerType
        <Description("Computer")> Bot = 1
        <Description("Mensch")> Human = 2
    End Enum

    Public Enum enFigures
        <Description("König")> King = 1
        <Description("Königin")> Queen = 2
        <Description("Turm")> Rook = 3
        <Description("Läufer")> Bishop = 4
        <Description("Springer")> Knight = 5
        <Description("Bauer")> Pawn = 6
    End Enum

    Public Enum enFiguresColored
        <Description("weisser König")> White_King = 1
        <Description("weisse Königin")> White_Queen = 2
        <Description("weisser Turm")> White_Rook = 3
        <Description("weisser Läufer")> White_Bishop = 4
        <Description("weisser Springer")> White_Knight = 5
        <Description("weisser Bauer")> White_Pawn = 6

        <Description("schwarzer König")> Black_King = 10
        <Description("schwarze Königin")> Black_Queen = 11
        <Description("schwarzer Turm")> Black_Rook = 12
        <Description("schwarzer Läufer")> Black_Bishop = 13
        <Description("schwarzer Springer")> Black_Knight = 14
        <Description("schwarzer Bauer")> Black_Pawn = 15
    End Enum

End Module