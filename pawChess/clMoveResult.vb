Option Strict On
Option Explicit On

Public Class clMoveResult

    Public Property Chess As Boolean
    Public Property Matt As Boolean
    Public Property Heat As Integer
    Public Property HitsInRange As Generic.List(Of String)
    Public Property PossibleImpacts As Generic.List(Of String)

    Public Sub New()

    End Sub

    Public Sub New(ByVal Board As clBoard, ByVal TargetField As ucField, ByVal SourceField As ucField)
        Me.Matt = mdTools.CheckFieldForFigure(TargetField, SourceField.Figure.PlayerColor, mdPublicEnums.enFigures.King)

        CheckNeighbors(Board, TargetField, SourceField)

        Me.Heat = CalcHeat()
    End Sub

    Private Sub CheckNeighbors(ByVal Board As clBoard, ByVal TargetField As ucField, ByVal SourceField As ucField)
        ' Wen kann ich treffen?
        mdTools.CheckMovement2(Board, SourceField.Figure.PlayerColor, TargetField, SourceField.Figure)

        ' Werde ich getroffen?
        For Each oPair As Generic.KeyValuePair(Of String, ucField) In Board.colFields
            Dim FieldToCheck As ucField = oPair.Value

            If FieldToCheck IsNot Nothing Then
                If mdTools.CheckFieldForFigure(FieldToCheck, SourceField.Figure.PlayerColor) Then
                    mdTools.CheckMovement2(Board, SourceField.Figure.PlayerColor, FieldToCheck, FieldToCheck.Figure)
                End If
            End If
        Next
    End Sub

    Public Function CalcHeat() As Integer
        Return 0
    End Function

End Class
