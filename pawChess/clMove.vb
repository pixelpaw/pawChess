Option Strict On
Option Explicit On

Public Class clMove

    Dim mstrMoveNr As Integer
    Public Property MoveNr() As Integer
        Get
            Return mstrMoveNr
        End Get
        Set(ByVal Value As Integer)
            mstrMoveNr = Value
            MoveNrFull = Value.ToString.PadLeft(3, "0"c)
        End Set
    End Property

    ' Der MoveString ist eigentlich nur kurze Teil ohne Kommentar.
    ' Der MoveStringFull ist der komplette MoveString inkl. Kommentar.
    ' Der MoveStringText ist der Zug im Klartext ohne Kommentar.
    Dim mstrMoveStringSimple As String
    Dim mstrMoveStringFull As String
    Public Property MoveStringFull As String
    Public Property MoveStringText As String
    Public Property MoveString As String
        Get
            Return mstrMoveStringSimple
        End Get
        Set(ByVal Value As String)
            mstrMoveStringFull = Value
            mstrMoveStringSimple = GetMoveStringSimple()

            MoveStringFull = mstrMoveStringFull

            Comment = GetComment()
            MoveType = GetMoveType()
        End Set
    End Property

    Public Property Comment As String
    Public Property FigurePlayed As mdPublicEnums.enFigures = mdPublicEnums.enFigures.EmptyFigure
    Public Property FigureHit As mdPublicEnums.enFigures = mdPublicEnums.enFigures.EmptyFigure
    Public Property MoveNrFull As String
    Public Property MoveResult As clMoveResult
    Public Property MoveType As mdPublicEnums.enChessMoveType = Nothing
    Public Property PlayerColor As mdPublicEnums.enPlayerColor = Nothing
    Public Property SourceFieldIndex As String
    Public Property SourceFieldName As String
    Public Property TargetFieldIndex As String
    Public Property TargetFieldName As String
    Public Property TimeStamp As DateTime

    Public Sub New()
    End Sub

    Public Sub New(ByVal SourceField As ucField, ByVal TargetField As ucField, ByVal nMoveCounter As Integer, ByVal oMoveResult As clMoveResult)
        Me.TimeStamp = Now()
        Me.MoveNr = nMoveCounter
        Me.MoveResult = oMoveResult
        Me.FigurePlayed = SourceField.Figure.Figure
        Me.FigureHit = If(IsNothing(TargetField.Figure), mdPublicEnums.enFigures.EmptyFigure, TargetField.Figure.Figure)
        Me.MoveString = mdTools.GetChessMoveString(SourceField, TargetField, oMoveResult.Chess)
        Me.SourceFieldIndex = SourceField.Index
        Me.SourceFieldName = SourceField.Name
        Me.TargetFieldIndex = TargetField.Index
        Me.TargetFieldName = TargetField.Name
        Me.PlayerColor = SourceField.Figure.PlayerColor
        Me.MoveStringText = GetMoveStringText()
    End Sub

    Private Function StringContainsComment() As Boolean
        If Not mstrMoveStringFull.Contains(mdSettings.mCN_CommentStart) Then Return False
        If Not mstrMoveStringFull.Contains(mdSettings.mCN_CommentEnd) Then Return False
        Return True
    End Function

    Private Function GetComment() As String
        If StringContainsComment() Then
            Dim nStartIndex As Integer = mstrMoveStringFull.IndexOf(mdSettings.mCN_CommentStart)
            Dim nLen As Integer = mstrMoveStringFull.IndexOf(mdSettings.mCN_CommentEnd) - mstrMoveStringFull.IndexOf(mdSettings.mCN_CommentStart)
            Return mstrMoveStringFull.Substring(nStartIndex, nLen)
        End If
        Return ""
    End Function

    Private Function GetMoveStringSimple() As String
        If StringContainsComment() Then
            Return mstrMoveStringFull.Substring(0, mstrMoveStringFull.IndexOf(mdSettings.mCN_CommentStart)) & mdSettings.mCN_Delimiter
        Else
            Return mstrMoveStringFull
        End If
        Return Nothing
    End Function

    Private Function GetMoveType() As mdPublicEnums.enChessMoveType
        Dim strMoves() As String = New String() {mdSettings.mCN_RochadeLong, mdSettings.mCN_RochadeShort, mdSettings.mCN_Move, mdSettings.mCN_Chess, mdSettings.mCN_Hit, mdSettings.mCN_Remis, mdSettings.mCN_enPassant, mdSettings.mCN_Hit}
        Return mdTools.GetChessMoveType(mdTools.ContainsOneOf(Me.MoveString, strMoves))
    End Function

    Public Function GetMoveStringText() As String
        Dim strResult As New System.Text.StringBuilder()

        strResult.Append(AddSpace(mdTools.GetEnumDescription(Me.FigurePlayed)))
        strResult.Append(AddSpace(Me.SourceFieldName))
        strResult.Append(AddSpace(mdTools.GetEnumDescription(Me.MoveType)))
        strResult.Append(AddSpace("nach"))
        strResult.Append(AddSpace(If(Not IsNothing(Me.FigureHit) AndAlso Me.FigureHit > 0, mdTools.GetEnumDescription(Me.FigureHit), "")))
        strResult.Append(AddSpace(Me.TargetFieldName))

        Return strResult.ToString
    End Function

    Public Function AddSpace(ByVal strToCheck As String) As String
        If String.IsNullOrEmpty(strToCheck) Then
            Return ""
        Else
            Return strToCheck & " "
        End If
    End Function
End Class
