Option Strict On
Option Explicit On

Public Module mdTools

    Public Function GetHistoryListViewSize(ByVal nLogPanelHeight As Integer, ByVal nLogPanelWidth As Integer) As Size
        Dim oResult As New Size(nLogPanelWidth - mdSettings.mnDefaultPos * 2, nLogPanelHeight - (mdSettings.mnSize_LogLabel * 2) - (mdSettings.mnDefaultPos * 4))
        Return oResult
    End Function

    Public Function GetChessMoveString(ByVal SourceField As ucField, ByVal TargetField As ucField, Optional ByVal bChess As Boolean = False, Optional ByVal strComment As String = "") As String
        Dim strResult As String = ""

        Dim strMove As String = mdSettings.mCN_Move
        If bChess Then
            strMove = mdSettings.mCN_Chess
        Else
            If TargetField.Figure IsNot Nothing Then
                If TargetField.Figure.Figure = mdPublicEnums.enFigures.King Then
                    strMove = mdSettings.mCN_Matt

                ElseIf TargetField.Figure.PlayerColor = SourceField.Figure.PlayerColor AndAlso TargetField.Figure.Figure = enFigures.Rook And SourceField.Figure.Figure = enFigures.King Then
                    If TargetField.IndexCol = 8 Then
                        strMove = mdSettings.mCN_RochadeShort
                    ElseIf TargetField.IndexCol = 1 Then
                        strMove = mdSettings.mCN_RochadeLong
                    End If

                ElseIf 1 = 2 Then
                    ' ToDo : en Passant
                Else
                    strMove = mdSettings.mCN_Hit
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(strComment) Then
            strComment = mdSettings.mCN_CommentStart & strComment & mdSettings.mCN_CommentEnd
        End If

        Dim strSourceFigureID As String = SourceField.Figure.ChessNoteID
        Dim strTargetFigureID As String = If(TargetField.Figure IsNot Nothing, TargetField.Figure.ChessNoteID, "")

        Dim strSourceField As String = SourceField.Name
        Dim strTargetField As String = TargetField.Name

        strResult &= strSourceFigureID
        strResult &= strSourceField
        strResult &= strMove
        strResult &= strTargetFigureID
        strResult &= strTargetField
        strResult &= mdSettings.mCN_Separator
        strResult &= strComment
        strResult &= mdSettings.mCN_Delimiter

        Return strResult
    End Function

    Public Function CheckMovement(ByVal Board As clBoard, ByVal oField As ucField, Optional ByVal bAllowGlow As Boolean = True, Optional ByVal oWantedField As ucField = Nothing, Optional ByVal oFieldToIgnore As ucField = Nothing, Optional ByVal oFigure As clChessFigure = Nothing, Optional ByVal bOnlyHits As Boolean = False) As Boolean
        Dim strResult As Generic.List(Of String) = CheckMovement2(Board, oField, bAllowGlow, oWantedField, oFieldToIgnore, oFigure, bOnlyHits)

        If strResult.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CheckMovement2(ByVal Board As clBoard, ByVal oField As ucField, Optional ByVal bAllowGlow As Boolean = False, Optional ByVal oWantedField As ucField = Nothing, Optional ByVal oFieldToIgnore As ucField = Nothing, Optional ByVal oFigure As clChessFigure = Nothing, Optional ByVal bOnlyHits As Boolean = False) As Generic.List(Of String)
        Dim lResult As New Generic.List(Of String)

        If bAllowGlow Then oField.GlowState = enGlowMode.Move

        ' Key = Index von ucField
        ' Value = mdPublicEnums.enGlowMode
        Dim colValidFields As Generic.Dictionary(Of String, mdPublicEnums.enGlowMode) = GetValidMoves(Board, oField, oWantedField, oFieldToIgnore, oFigure)

        If colValidFields IsNot Nothing AndAlso colValidFields.Count > 0 Then
            For Each oPair As KeyValuePair(Of String, mdPublicEnums.enGlowMode) In colValidFields
                Dim tmpField As ucField = Board.GetField(oPair.Key)

                If bAllowGlow Then tmpField.GlowState = CType(oPair.Value, mdPublicEnums.enGlowMode)

                If Not bOnlyHits Or (bOnlyHits And (oPair.Value = enGlowMode.Chess Or oPair.Value = enGlowMode.Hit)) Then lResult.Add(tmpField.Index)
            Next
        End If

        Return lResult
    End Function

    Public Function GetValidMoves(ByVal Board As clBoard, ByVal oSourceField As ucField, Optional ByVal oWantedField As ucField = Nothing, Optional ByVal oFieldToIgnore As ucField = Nothing, Optional ByVal oFigure As clChessFigure = Nothing) As Generic.Dictionary(Of String, mdPublicEnums.enGlowMode)
        If Not oSourceField.IsChessField Then Return Nothing

        If oFigure Is Nothing Then oFigure = oSourceField.Figure
        If oFigure Is Nothing Then Return Nothing

        Dim nCol As Integer = oSourceField.IndexCol
        Dim nRow As Integer = oSourceField.IndexRow

        Dim colResult As New Generic.Dictionary(Of String, mdPublicEnums.enGlowMode)

        For Each Rule As clMoveRule In oFigure.MovementRules
            If Rule.OnlyFirstMove And oFigure.MoveCounter > 0 Then Continue For

            For i As Integer = 1 To Rule.Steps
                nRow = oSourceField.IndexRow + (Rule.DirectionRow * i)
                nCol = oSourceField.IndexCol + (Rule.DirectionCol * i)

                If nCol < 1 Or nCol > 8 Or nRow < 1 Or nRow > 8 Then Continue For

                Dim tmpField As ucField = Board.GetField(nCol, nRow)

                If oWantedField IsNot Nothing Then
                    If oWantedField.Index <> tmpField.Index Then
                        Continue For
                    Else
                        If Not colResult.ContainsKey(oSourceField.Index) Then colResult.Add(oSourceField.Index, mdPublicEnums.enGlowMode.Special)
                    End If

                ElseIf oFieldToIgnore IsNot Nothing AndAlso oFieldToIgnore.Index = tmpField.Index Then
                    If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Move)

                Else
                    If tmpField.Figure Is Nothing Then
                        If Rule.OnlyOnHit Then
                            If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Off)
                        Else
                            If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Move)
                        End If
                    Else
                        If tmpField.Figure.PlayerColor = oFigure.PlayerColor Then
                            If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Off)
                            Exit For

                        Else
                            If Rule.AllowHit Then
                                If tmpField.Figure.Figure = mdPublicEnums.enFigures.King Then
                                    If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Chess)
                                Else
                                    If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Hit)
                                End If
                                Exit For
                            Else
                                If Not colResult.ContainsKey(tmpField.Index) Then colResult.Add(tmpField.Index, mdPublicEnums.enGlowMode.Off)
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next
        Next

        Return colResult
    End Function

    Public Function CheckFieldForFigure(ByVal TargetField As ucField, ByVal enPlayerColor As mdPublicEnums.enPlayerColor, Optional ByVal enFigure As mdPublicEnums.enFigures = mdPublicEnums.enFigures.EmptyFigure) As Boolean
        Dim enResult As mdPublicEnums.enCheckFieldForFigureErrorCode = mdTools.CheckFieldForFigure2(TargetField, enPlayerColor, enFigure)

        Select Case enResult
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.NoResult : Return False
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.FieldFigureIsNothing : Return False
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.EmptyFigure : Return False
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.FriendlyFigureFound : Return False
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.FigureFound : Return True
            Case mdPublicEnums.enCheckFieldForFigureErrorCode.SearchedFigureFound : Return True
            Case Else : Return False
        End Select
    End Function

    Public Function CheckFieldForFigure2(ByVal TargetField As ucField, ByVal enPlayerColor As mdPublicEnums.enPlayerColor, Optional ByVal enFigure As mdPublicEnums.enFigures = mdPublicEnums.enFigures.EmptyFigure) As mdPublicEnums.enCheckFieldForFigureErrorCode
        If TargetField.Figure Is Nothing Then
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.FieldFigureIsNothing

        ElseIf TargetField.Figure.Figure = mdPublicEnums.enFigures.EmptyFigure Then
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.EmptyFigure

        ElseIf TargetField.Figure.PlayerColor = enPlayerColor Then
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.FriendlyFigureFound

        ElseIf TargetField.Figure.Figure = enFigure Then
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.SearchedFigureFound

        ElseIf TargetField.Figure.Figure <> mdPublicEnums.enFigures.EmptyFigure Then
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.FigureFound

        Else
            Return mdPublicEnums.enCheckFieldForFigureErrorCode.NoResult

        End If
    End Function


    Public Function GetFigureChessNoteID(ByVal nFigure As enFigures) As String
        Select Case nFigure
            Case enFigures.King : Return mCN_King
            Case enFigures.Queen : Return mCN_Queen
            Case enFigures.Rook : Return mCN_Rook
            Case enFigures.Bishop : Return mCN_Bishop
            Case enFigures.Knight : Return mCN_Knight
            Case enFigures.Pawn : Return mCN_Pawn
            Case Else : Return ""
        End Select
    End Function

    Public Function GetFigureUnicode(ByVal nFigure As enFiguresColored) As String
        Select Case nFigure
            Case enFiguresColored.White_King : Return mdSettings.mstrWhite_King
            Case enFiguresColored.White_Queen : Return mdSettings.mstrWhite_Queen
            Case enFiguresColored.White_Rook : Return mdSettings.mstrWhite_Rook
            Case enFiguresColored.White_Bishop : Return mdSettings.mstrWhite_Bishop
            Case enFiguresColored.White_Knight : Return mdSettings.mstrWhite_Knight
            Case enFiguresColored.White_Pawn : Return mdSettings.mstrWhite_Pawn

            Case enFiguresColored.Black_King : Return mdSettings.mstrBlack_King
            Case enFiguresColored.Black_Queen : Return mdSettings.mstrBlack_Queen
            Case enFiguresColored.Black_Rook : Return mdSettings.mstrBlack_Rook
            Case enFiguresColored.Black_Bishop : Return mdSettings.mstrBlack_Bishop
            Case enFiguresColored.Black_Knight : Return mdSettings.mstrBlack_Knight
            Case enFiguresColored.Black_Pawn : Return mdSettings.mstrBlack_Pawn
            Case Else : Return ""
        End Select
    End Function

    Public Function GetChessMoveType(ByVal strMove As String) As mdPublicEnums.enChessMoveType
        Select Case strMove
            Case mdSettings.mCN_RochadeLong : Return mdPublicEnums.enChessMoveType.RochadeLong
            Case mdSettings.mCN_RochadeShort : Return mdPublicEnums.enChessMoveType.RochadeShort
            Case mdSettings.mCN_Move : Return mdPublicEnums.enChessMoveType.Move
            Case mdSettings.mCN_Matt : Return mdPublicEnums.enChessMoveType.Matt
            Case mdSettings.mCN_Chess : Return mdPublicEnums.enChessMoveType.Chess
            Case mdSettings.mCN_Remis : Return mdPublicEnums.enChessMoveType.Remis
            Case mdSettings.mCN_enPassant : Return mdPublicEnums.enChessMoveType.enPassant
            Case mdSettings.mCN_Hit : Return mdPublicEnums.enChessMoveType.Hit
            Case Else : Return Nothing
        End Select
    End Function

    Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String
        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        If fi Is Nothing Then Return EnumConstant.ToString()
        Dim aattr() As System.ComponentModel.DescriptionAttribute = TryCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())
        If aattr.Length > 0 Then
            Return aattr(0).Description
        Else
            Return EnumConstant.ToString()
        End If
    End Function

    Public Function IsBetween(ByVal nValue As Object, ByVal nLBound As Object, ByVal nUBound As Object, Optional ByVal bHandleAsInteger As Boolean = True) As Boolean
        Try
            If bHandleAsInteger Then
                Return CInt(nLBound) <= CInt(nValue) And CInt(nValue) <= CInt(nUBound)
            Else
                Return CDbl(nLBound) <= CDbl(nValue) And CDbl(nValue) <= CDbl(nUBound)
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Wir brauchen einen Dummy-Parameter, denn Nothing resultiert in 0
    Const cnDummy As Integer = Integer.MaxValue
    Public Function IsOneOf(ByVal nValue As Integer, Optional ByVal n0 As Integer = cnDummy, Optional ByVal n1 As Integer = cnDummy, Optional ByVal n2 As Integer = cnDummy, Optional ByVal n3 As Integer = cnDummy, Optional ByVal n4 As Integer = cnDummy, Optional ByVal n5 As Integer = cnDummy, Optional ByVal n6 As Integer = cnDummy, Optional ByVal n7 As Integer = cnDummy, Optional ByVal n8 As Integer = cnDummy, Optional ByVal n9 As Integer = cnDummy) As Boolean
        Return nValue = n0 OrElse nValue = n1 OrElse nValue = n2 OrElse nValue = n3 OrElse nValue = n4 OrElse nValue = n5 OrElse nValue = n6 OrElse nValue = n7 OrElse nValue = n8 OrElse nValue = n9
    End Function

    ' Wir brauchen einen Dummy-Parameter, denn Nothing ist auch ein erlaubter Wert
    Const cstrDummy As String = "__MISSING_PARAMETER__"
    Public Function IsOneOf(ByVal strValue As String, Optional ByVal o0 As String = cstrDummy, Optional ByVal o1 As String = cstrDummy, Optional ByVal o2 As String = cstrDummy, Optional ByVal o3 As String = cstrDummy, Optional ByVal o4 As String = cstrDummy, Optional ByVal o5 As String = cstrDummy, Optional ByVal o6 As String = cstrDummy, Optional ByVal o7 As String = cstrDummy, Optional ByVal o8 As String = cstrDummy, Optional ByVal o9 As String = cstrDummy) As Boolean
        If strValue Is Nothing Then
            Return o0 Is Nothing OrElse o1 Is Nothing OrElse o2 Is Nothing OrElse o3 Is Nothing OrElse o4 Is Nothing OrElse o5 Is Nothing OrElse o6 Is Nothing OrElse o7 Is Nothing OrElse o8 Is Nothing OrElse o9 Is Nothing
        Else
            Return strValue.Equals(o0) OrElse strValue.Equals(o1) OrElse strValue.Equals(o2) OrElse strValue.Equals(o3) OrElse strValue.Equals(o4) OrElse strValue.Equals(o5) OrElse strValue.Equals(o6) OrElse strValue.Equals(o7) OrElse strValue.Equals(o8) OrElse strValue.Equals(o9)
        End If
    End Function

    Public Function ContainsOneOf(ByVal strValue As String, Optional ByVal arrItems As String() = Nothing) As String
        If arrItems IsNot Nothing Then
            For Each strItem As String In arrItems
                If strValue.Contains(strItem) Then Return strItem
            Next
        End If
        Return Nothing
    End Function

End Module
