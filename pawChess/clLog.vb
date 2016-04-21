Option Strict On
Option Explicit On

Public Class clLog

    Public Property LogEntryList As Generic.Dictionary(Of Integer, clMove)
    Public Property Spielstart As DateTime = Nothing
    Public Property ChessMoveCount As Integer = 0

    Public Sub New()
        LogEntryList = New Generic.Dictionary(Of Integer, clMove)
        Spielstart = Now()
    End Sub

    Public Sub NewChessMoveEntry(ByVal oNewEntry As clMove)
        LogEntryList.Add(ChessMoveCount, oNewEntry)
        ChessMoveCount += 1

        If ChessMoveCount > LogEntryList.Count Then
            MsgBox("Fehler im Log" & vbCrLf & "MoveCounter (" & ChessMoveCount.ToString & ") > LogEntryList (" & LogEntryList.Count.ToString & ")")
        End If
    End Sub

    Public Function Write(ByVal oMove As clMove) As ListViewItem
        NewChessMoveEntry(oMove)

        Dim LogItem As New ListViewItem(oMove.MoveNrFull)

        LogItem.SubItems.Add(oMove.TimeStamp.ToLongTimeString)
        LogItem.SubItems.Add(mdTools.GetEnumDescription(oMove.PlayerColor))
        'LogItem.SubItems.Add(oMove.MoveString)
        LogItem.SubItems.Add(oMove.MoveStringText)

        Return LogItem
    End Function

End Class