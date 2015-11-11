Option Strict On
Option Explicit On

Public Class clChessFigure

    Public Property PlayerColor As mdSettings.enPlayerColor
    Public Property Figure As mdSettings.enFigures
    Public Property FigureColored As mdSettings.enFiguresColored
    Public Property FigureID As String
    Public Property Value As Integer
    Public Property MoveCounter As Integer

    Public Sub New()
    End Sub

End Class
