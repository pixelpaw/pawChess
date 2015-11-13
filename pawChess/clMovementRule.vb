Option Strict On
Option Explicit On

Public Class clMovementRule

    Public Property Name As String
    Public Property DirectionRow As Integer
    Public Property DirectionCol As Integer
    Public Property Steps As Integer
    Public Property OnlyOnHit As Boolean
    Public Property OnlyFirstMove As Boolean

    Public Sub New()

    End Sub

    Public Sub New(ByVal strName As String, ByVal nDirectionRow As Integer, ByVal nDirectionCol As Integer, ByVal nSteps As Integer, ByVal bOnlyOnHit As Boolean, ByVal bOnlyFirstMove As Boolean)
        Me.Name = strName
        Me.DirectionRow = nDirectionRow
        Me.DirectionCol = nDirectionCol
        Me.Steps = nSteps
        Me.OnlyOnHit = bOnlyOnHit
        Me.OnlyFirstMove = bOnlyFirstMove
    End Sub

End Class
