Option Strict On
Option Explicit On

Imports System.ComponentModel
Public Module mdSettings

#Region "Enums"

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

#End Region

#Region "Functions"

    Public Function GetFigureUnicode(ByVal nFigure As enFiguresColored) As String
        Select Case nFigure
            Case enFiguresColored.White_King : Return Strings.ChrW(&H2654)
            Case enFiguresColored.White_Queen : Return Strings.ChrW(&H2655)
            Case enFiguresColored.White_Rook : Return Strings.ChrW(&H2656)
            Case enFiguresColored.White_Bishop : Return Strings.ChrW(&H2657)
            Case enFiguresColored.White_Knight : Return Strings.ChrW(&H2658)
            Case enFiguresColored.White_Pawn : Return Strings.ChrW(&H2659)

            Case enFiguresColored.Black_King : Return Strings.ChrW(&H265A)
            Case enFiguresColored.Black_Queen : Return Strings.ChrW(&H265B)
            Case enFiguresColored.Black_Rook : Return Strings.ChrW(&H265C)
            Case enFiguresColored.Black_Bishop : Return Strings.ChrW(&H265D)
            Case enFiguresColored.Black_Knight : Return Strings.ChrW(&H265E)
            Case enFiguresColored.Black_Pawn : Return Strings.ChrW(&H265F)
            Case Else : Return ""
        End Select
    End Function

    Public Function GetDescription(ByVal EnumConstant As [Enum]) As String
        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        If fi Is Nothing Then Return EnumConstant.ToString()
        Dim aattr() As System.ComponentModel.DescriptionAttribute = TryCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())
        If aattr.Length > 0 Then
            Return aattr(0).Description
        Else
            Return EnumConstant.ToString()
        End If
    End Function

#End Region

#Region "Variablen"

    Public mScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public mScreenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

    Public moColor_GamePanel As Color = Color.Crimson
    Public moColor_InnerPanel As Color = Color.PaleGoldenrod
    Public moColor_CornerField As Color = Color.Moccasin  'Color.DarkSalmon
    Public moColor_MapField As Color = Color.Moccasin
    Public moColor_BrightField As Color = Color.PapayaWhip
    Public moColor_DarkField As Color = Color.Sienna

    Public moColor_GlowBad As Color = Color.FromArgb(100, Color.Red)
    Public moColor_GlowGood As Color = Color.FromArgb(100, Color.LightGreen)
    Public moColor_GlowNeutral As Color = Color.FromArgb(100, Color.RoyalBlue)
    Public moColor_GlowOff As Color = Color.Transparent

    Public mnDefaultPos As Integer = 12

    Public mnSize_Small As Integer = 35
    Public mnSize_Big As Integer = 75
    Public mnSize_LogPanel As Integer = 250
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

    ' Variablen für die Schach-Notationen
    Public mCN_Move As String = "-"
    Public mCN_Hit As String = "x"
    Public mCN_Chess As String = "+"
    Public mCN_Matt As String = "++"
    Public mCN_RochadeShort As String = "0-0"
    Public mCN_RochadeLong As String = "0-0-0"
    Public mCN_enPassant As String = "e.p."
    Public mCN_CommentStart As String = "{"
    Public mCN_CommentEnd As String = "}"
    Public mCN_Separator As String = " "
    Public mCN_Delimiter As String = ";"
    Public mCH_King As String = "K"
    Public mCH_Queen As String = "Q"
    Public mCH_Rook As String = "R"
    Public mCH_Bishop As String = "B"
    Public mCH_Knight As String = "K"
    Public mCH_Pawn As String = ""

#End Region

End Module
