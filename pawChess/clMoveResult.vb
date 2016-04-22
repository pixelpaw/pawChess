Option Strict On
Option Explicit On

Public Class clMoveResult

    Public Property Chess As Boolean
    Public Property Matt As Boolean
    Public Property HitsInRange As Generic.List(Of String)
    Public Property PossibleImpacts As Generic.List(Of String)

    Public Sub New()

    End Sub

    Public Sub New(ByVal Board As clBoard, ByVal TargetField As ucField, ByVal SourceField As ucField)
        Me.Matt = mdTools.CheckFieldForFigure(TargetField, SourceField.Figure.PlayerColor, mdPublicEnums.enFigures.King)

        CheckNeighbors(Board, TargetField, SourceField)
    End Sub

    Private Sub CheckNeighbors(ByVal Board As clBoard, ByVal TargetField As ucField, ByVal SourceField As ucField)
        ' Wen kann ich treffen?
        Me.HitsInRange = mdTools.CheckMovement2(Board, SourceField.Figure.PlayerColor, TargetField, SourceField.Figure)
        If Me.HitsInRange.Count > 0 Then
            For Each strFieldIndex As String In HitsInRange
                Dim oCurField As ucField = Board.GetField(strFieldIndex)
                If oCurField.Figure.Figure = mdPublicEnums.enFigures.King Then Me.Chess = True
            Next
        End If


        ' Werde ich getroffen?
        Dim enEnemyColor As mdPublicEnums.enPlayerColor = If(SourceField.Figure.PlayerColor = mdPublicEnums.enPlayerColor.Black, mdPublicEnums.enPlayerColor.White, mdPublicEnums.enPlayerColor.Black)

        For Each oPair As Generic.KeyValuePair(Of String, ucField) In Board.colFields
            Dim FieldToCheck As ucField = oPair.Value

            If FieldToCheck IsNot Nothing Then
                If mdTools.CheckFieldForFigure(FieldToCheck, enEnemyColor) Then
                    Dim tmpList As New Generic.List(Of String)
                    tmpList = mdTools.CheckMovement2(Board, enEnemyColor, FieldToCheck, FieldToCheck.Figure)

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
