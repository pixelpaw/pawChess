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
        ChessMoveCount += 1
        LogEntryList.Add(ChessMoveCount, oNewEntry)

        If ChessMoveCount > LogEntryList.Count Then
            MsgBox("Fehler im Log" & vbCrLf & "MoveCounter (" & ChessMoveCount.ToString & ") > LogEntryList (" & LogEntryList.Count.ToString & ")")
        End If
    End Sub

    Public Function Write(ByVal oMove As clChessMove) As ListViewItem
        NewChessMoveEntry(oMove)

        Dim LogItem As New ListViewItem(oMove.MoveNrFull)

        LogItem.SubItems.Add(oMove.TimeStamp.ToLongTimeString)
        LogItem.SubItems.Add(mdTools.GetEnumDescription(oMove.PlayerColor))
        LogItem.SubItems.Add(oMove.MoveString)

        Return LogItem
    End Function

End Class