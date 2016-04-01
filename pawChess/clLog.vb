Option Strict On
Option Explicit On

Public Class clLog

    Public Property LogEntryList As Generic.Dictionary(Of Integer, clChessMove)
    Public Property Spielstart As DateTime = Nothing
    Public Property ChessMoveCount As Integer = 0

    Public Sub New()
        LogEntryList = New Generic.Dictionary(Of Integer, clChessMove)
        Spielstart = Now()
    End Sub

    Public Sub NewChessMoveEntry(ByVal oNewEntry As clChessMove)
        LogEntryList.Add(LogEntryList.Count + 1, oNewEntry)
        ChessMoveCount += 1

        If ChessMoveCount > LogEntryList.Count Then
            MsgBox("Fehler im Log" & vbCrLf & "MoveCounter (" & ChessMoveCount.ToString & ") > LogEntryList (" & LogEntryList.Count.ToString & ")")
        End If
    End Sub

End Class