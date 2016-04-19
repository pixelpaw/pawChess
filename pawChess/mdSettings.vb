Option Strict On
Option Explicit On

Imports System.ComponentModel
Public Module mdSettings

    Public mnFieldTimerIntervall As Integer = 250

    Public mScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public mScreenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Public moColor_GamePanel As Color = Color.Crimson
    Public moColor_InnerPanel As Color = Color.PaleGoldenrod
    Public moColor_CornerField As Color = Color.Moccasin  'Color.DarkSalmon
    Public moColor_MapField As Color = Color.Moccasin
    Public moColor_BrightField As Color = Color.PapayaWhip
    Public moColor_DarkField As Color = Color.Sienna

    Public moColor_GlowBad As Color = Color.FromArgb(100, Color.Red)
    Public moColor_GlowChess As Color = Color.FromArgb(100, Color.Red)
    Public moColor_GlowGood As Color = Color.FromArgb(100, Color.LightGreen)
    Public moColor_GlowNeutral As Color = Color.FromArgb(100, Color.RoyalBlue)
    Public moColor_GlowOff As Color = Color.Transparent

    Public mnDefaultPos As Integer = 12

    Public mnSize_Small As Integer = 35
    Public mnSize_Big As Integer = 75
    Public mnSize_LogPanel As Integer = 450
    Public mnSize_LogLabel As Integer = 25
    Public mnSize_GamePanel As Integer = mnSize_Small * 2 + mnSize_Big * 8 + 2  ' + 2 durch die Border des InnerPanels
    Public mnSize_ClientSize_Width As Integer = mnSize_GamePanel + mnSize_LogPanel + (mnDefaultPos * 3)
    Public mnSize_ClientSize_Height As Integer = mnSize_GamePanel + (mnDefaultPos * 2)

    Public mstrNameSpaceH As String = "0ABCDEFGH9"
    Public mstrNameSpaceV As String = "0123456789"

    Public mFigures_Font As Font = New Font("Times New Roman", 50)

    Public mFont_Header As Font = New Font("Segoe UI", 12, FontStyle.Regular)
    Public mFont_Small_Regular As Font = New Font("Segoe UI", 9, FontStyle.Regular)
    Public mFont_Small_Bold As Font = New Font("Segoe UI", 9, FontStyle.Bold)

    Public mstrWhite_King As String = Strings.ChrW(&H2654)
    Public mstrWhite_Queen As String = Strings.ChrW(&H2655)
    Public mstrWhite_Rook As String = Strings.ChrW(&H2656)
    Public mstrWhite_Bishop As String = Strings.ChrW(&H2657)
    Public mstrWhite_Knight As String = Strings.ChrW(&H2658)
    Public mstrWhite_Pawn As String = Strings.ChrW(&H2659)

    Public mstrBlack_King As String = Strings.ChrW(&H265A)
    Public mstrBlack_Queen As String = Strings.ChrW(&H265B)
    Public mstrBlack_Rook As String = Strings.ChrW(&H265C)
    Public mstrBlack_Bishop As String = Strings.ChrW(&H265D)
    Public mstrBlack_Knight As String = Strings.ChrW(&H265E)
    Public mstrBlack_Pawn As String = Strings.ChrW(&H265F)

    ' Variablen für die Schach-Notationen
    ' Nur ausführliche Notation -> https://de.wikipedia.org/wiki/Schachnotation
    Public mCN_Move As String = "-"
    Public mCN_Hit As String = "x"
    Public mCN_Chess As String = "+"
    Public mCN_Matt As String = "++"
    Public mCN_Remis As String = "="
    Public mCN_RochadeShort As String = "0-0"
    Public mCN_RochadeLong As String = "0-0-0"
    Public mCN_enPassant As String = "e.p."
    Public mCN_Separator As String = " "
    Public mCN_Delimiter As String = ";"

    Public mCN_King As String = "K"
    Public mCN_Queen As String = "Q"
    Public mCN_Rook As String = "R"
    Public mCN_Bishop As String = "B"
    Public mCN_Knight As String = "N"
    Public mCN_Pawn As String = "P"

    Public mCN_CommentStart As String = "{"
    Public mCN_CommentEnd As String = "}"
    Public mCN_CommentMoveIsBrilliant As String = "!!"
    Public mCN_CommentMoveIsGood As String = "!"
    Public mCN_CommentMoveIsBad As String = "?"
    Public mCN_CommentMoveIsVeryBad As String = "??"
    Public mCN_CommentMoveIsInteresting As String = "!?"
    Public mCN_CommentMoveIsQuestionable As String = "?!"

End Module
