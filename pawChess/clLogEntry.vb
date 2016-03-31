Option Strict On
Option Explicit On

Public Class clLogEntry

    Public Property Runde As Integer = Nothing
    Public Property TimeStamp As DateTime = Nothing
    Public Property Player As String = Nothing
    Public Property SourceField As String = Nothing
    Public Property TargetField As String = Nothing
    Public Property Move As mdPublicEnums.enFigureMovement = N

    Public Sub New()
    End Sub

End Class
