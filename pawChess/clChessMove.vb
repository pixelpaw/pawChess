Option Strict On
Option Explicit On

Public Class clChessMove

    Public Property MoveNr As Integer
    Public Property MoveTyp As clChessMove.enMoveTyp
    Public Property PlayerColor As mdPublicEnums.enPlayerColor
    Public Property MoveString As String
    Public Property IndexSourceField As String
    Public Property IndexTargetField As String

    Public Enum enMoveTyp
        Hit
        Move
        Rochade
        Chess
        Matt
    End Enum

    Public Sub New()

    End Sub

    Public Shared Function WriteChessMove(ByVal SourceField As ucField, ByVal TargetField As ucField) As String
        Dim strChessMove As String = ""
        Return strChessMove
    End Function

    Public Shared Function GetChessMove(ByVal strMoveString As String) As clChessMove
        Dim oNewMove As New clChessMove



        Return oNewMove
    End Function

End Class
