Option Strict On
Option Explicit On

Public Class clChessFigure

    Public Property PlayerColor As mdPublicEnums.enPlayerColor
    Public Property Figure As mdPublicEnums.enFigures
    Public Property FigureColored As mdPublicEnums.enFiguresColored
    Public Property FigureID As String
    Public Property FigureName As String
    Public Property ChessNoteID As String
    Public Property Value As Integer
    Public Property MoveCounter As Integer
    Public Property Movement As mdPublicEnums.enFigureMovement
    Public Property MaxSteps As Integer
    Public Property MovementRules As List(Of clMovementRule)

    Public Sub New()
    End Sub

End Class
