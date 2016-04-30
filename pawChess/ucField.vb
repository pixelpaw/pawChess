Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ucField
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property InnerField As Label
    Public Property FieldTyp As mdPublicEnums.enFieldTyp
    Public Property Index As String
    Public Property IndexCol As Integer
    Public Property IndexRow As Integer
    Public Property IsChessField As Boolean = False

    Public FieldTimer As Timer = Nothing

    Private moFigure As clChessFigure = Nothing
    Public Property Figure() As clChessFigure
        Get
            Return Me.moFigure
        End Get

        Set(ByVal value As clChessFigure)
            If (value Is Nothing Or Me.moFigure Is Nothing) OrElse (value.FigureName <> Me.moFigure.FigureName) Then
                Me.moFigure = value
                NotifyPropertyChanged("Figure")
            End If
        End Set
    End Property

    Private moGlowState As mdPublicEnums.enGlowMode = enGlowMode.Off
    Public Property GlowState() As mdPublicEnums.enGlowMode
        Get
            Return Me.moGlowState
        End Get

        Set(ByVal value As mdPublicEnums.enGlowMode)
            If Not (value = moGlowState) Then
                Me.moGlowState = value
                NotifyPropertyChanged("GlowState")

                If Me.moGlowState = enGlowMode.Chess Then
                    Me.FieldTimer.Start()
                Else
                    Me.FieldTimer.Stop()
                End If
            End If
        End Set
    End Property

    Public Sub NotifyPropertyChanged(ByVal info As String)
        Select Case info
            Case "GlowState" : Me.RefreshGlow()
            Case "Figure" : Me.RefreshFigure()
        End Select

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Sub New(ByVal oTyp As mdPublicEnums.enFieldTyp)
        InitializeComponent()

        Me.FieldTimer = New Timer()
        Me.FieldTimer.Interval = mdSettings.mnFieldTimerIntervall
        Me.FieldTimer.Stop()

        AddHandler FieldTimer.Tick, AddressOf FieldTimer_Tick

        Me.FieldTyp = oTyp

        Select Case oTyp
            Case enFieldTyp.Corner
                Me.BackColor = mdSettings.moColor_CornerField
                Me.Size = New Size(mdSettings.mnSize_Small + 1, mdSettings.mnSize_Small + 1)

            Case enFieldTyp.MapHorizontal
                Me.BackColor = mdSettings.moColor_MapField
                Me.Size = New Size(mdSettings.mnSize_Big, mdSettings.mnSize_Small)

            Case enFieldTyp.MapVertical
                Me.BackColor = mdSettings.moColor_MapField
                Me.Size = New Size(mdSettings.mnSize_Small, mdSettings.mnSize_Big)

            Case enFieldTyp.Bright
                Me.BackColor = mdSettings.moColor_BrightField
                Me.Size = New Size(mdSettings.mnSize_Big, mdSettings.mnSize_Big)
                Me.IsChessField = True

            Case enFieldTyp.Dark
                Me.BackColor = mdSettings.moColor_DarkField
                Me.Size = New Size(mdSettings.mnSize_Big, mdSettings.mnSize_Big)
                Me.IsChessField = True

        End Select

        InnerField = New Label
        InnerField.Parent = Me
        InnerField.Location = New Point(0, 0)
        InnerField.Size = Me.Size
        InnerField.BackColor = Color.Transparent
        InnerField.TextAlign = ContentAlignment.MiddleCenter
        InnerField.Font = If(Me.IsChessField, mdSettings.mFigures_Font, mdSettings.mFont_Small_Bold)
        InnerField.Margin = New Padding(0)
        InnerField.Padding = New Padding(6, 0, 0, 0)

        Me.Controls.Add(InnerField)

    End Sub

    Public Sub FieldTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If Me.InnerField.BackColor <> mdSettings.moColor_GlowOff Then
            Me.InnerField.BackColor = mdSettings.moColor_GlowOff
        Else
            Me.InnerField.BackColor = mdSettings.moColor_GlowChess
        End If
    End Sub

    Public Sub RefreshFigure()
        If Me.Figure IsNot Nothing Then
            Me.InnerField.Text = Me.Figure.FigureID
        Else
            Me.InnerField.Text = ""
        End If
    End Sub

    Public Sub RefreshGlow()
        Me.InnerField.BackColor = GetGlowColor(Me.GlowState)
    End Sub

    Public Function GetGlowColor(ByVal GlowMode As mdPublicEnums.enGlowMode) As Color
        Select Case GlowMode
            Case enGlowMode.Hit : Return mdSettings.moColor_GlowBad
            Case enGlowMode.Good : Return mdSettings.moColor_GlowGood
            Case enGlowMode.Move : Return mdSettings.moColor_GlowNeutral
            Case enGlowMode.Chess : Return mdSettings.moColor_GlowChess
            Case enGlowMode.Off : Return mdSettings.moColor_GlowOff
            Case enGlowMode.Special : Return mdSettings.moColor_GlowBad
        End Select
    End Function

    Public Sub GlowOff()
        Me.GlowState = enGlowMode.Off
    End Sub

End Class
