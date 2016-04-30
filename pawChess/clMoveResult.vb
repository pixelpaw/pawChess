Option Strict On
Option Explicit On

Public Class clMoveResult

    Public Property Chess As Boolean
    Public Property Matt As Boolean
    Public Property HitsInRange As New Generic.List(Of String)
    Public Property PossibleImpacts As New Generic.List(Of String)

    Public Sub New()

    End Sub

    Public Sub New(ByVal Board As clBoard, ByVal TargetField As ucField, ByVal SourceField As ucField)
        Me.Matt = mdTools.CheckFieldForFigure(TargetField, SourceField.Figure.PlayerColor, mdPublicEnums.enFigures.King)

        CheckNeighbors(Board, TargetField, SourceField)
    End Sub

    Private Sub CheckNeighbors(ByVal Board As clBoard, ByVal oTargetField As ucField, ByVal oSourceField As ucField)
        ' Wen kann ich treffen?
        Me.HitsInRange = mdTools.CheckMovement2(Board, oTargetField, , , oSourceField, oSourceField.Figure, True)
        If Me.HitsInRange.Count > 0 Then
            For Each strFieldIndex As String In HitsInRange
                Dim oCurField As ucField = Board.GetField(strFieldIndex)
                If oCurField.Figure.Figure = mdPublicEnums.enFigures.King Then Me.Chess = True
            Next
        End If

        ' Werde ich getroffen?
        Dim nPlayerColor As mdPublicEnums.enPlayerColor = oSourceField.Figure.PlayerColor
        Dim nEnemyColor As mdPublicEnums.enPlayerColor = If(nPlayerColor = mdPublicEnums.enPlayerColor.White, mdPublicEnums.enPlayerColor.Black, mdPublicEnums.enPlayerColor.White)

        For Each oPair As Generic.KeyValuePair(Of String, ucField) In Board.colChessFields
            Dim FieldToCheck As ucField = oPair.Value

            If FieldToCheck IsNot Nothing Then
                If mdTools.CheckFieldForFigure(FieldToCheck, nPlayerColor) Then
                    Dim tmpList As Generic.List(Of String) = mdTools.CheckMovement2(Board, FieldToCheck, False, oTargetField)

                    If tmpList.Count() > 0 Then
                        For Each strTmpItem As String In tmpList
                            Me.PossibleImpacts.Add(strTmpItem)
                        Next
                    End If
                End If
            End If
        Next
    End Sub

End Class
