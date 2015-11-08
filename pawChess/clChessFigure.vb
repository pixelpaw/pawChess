Option Strict On
Option Explicit On

Public Class clChessFigure

    Public Property PlayerColor As mdSettings.enPlayerColor
    Public Property Figure As mdSettings.enFigures
    Public Property FigureColored As mdSettings.enFiguresColored
    Public Property FigureID As String
    Public Property Value As Integer
    Public Property Moves As Integer
    Public Property MaxSteps As Integer
    Public Property PositionIndex As String

    Public Sub New()
    End Sub

End Class
