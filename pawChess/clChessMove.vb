Option Strict On
Option Explicit On

Public Class clChessMove

    Public Property TimeStamp As DateTime
    Public Property MoveNr As Integer
    Public Property MoveTyp As mdPublicEnums.enChessMoveType
    Public Property PlayerColor As mdPublicEnums.enPlayerColor
    Public Property MoveString As String
    Public Property IndexSourceField As String
    Public Property IndexTargetField As String
    Public Property Comment As String

    Public Sub New()
    End Sub

End Class
