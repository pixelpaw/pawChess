Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ucField
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Delegate Sub TestChanged(ByVal col As Integer, ByVal row As Integer)
    Public Event tmp_PropertyChanged As TestChanged

    Public Property InnerField As Label
    Public Property FieldTyp As mdSettings.enFieldTyp
    Public Property Index As String
    Public Property IndexCol As Integer
    Public Property IndexRow As Integer
    Public Property IsChessField As Boolean = False
    Public Property Figure As clChessFigure = Nothing
    Public Property GlowState As mdSettings.enGlowMode = enGlowMode.Off

    Public Sub NotifyPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        RaiseEvent tmp_PropertyChanged(Me.IndexCol, Me.IndexRow)
    End Sub

    Public Sub New(ByVal oTyp As mdSettings.enFieldTyp)
        InitializeComponent()

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

        AddHandler Me.PropertyChanged, AddressOf NotifyPropertyChanged
    End Sub

    Public Sub SetFigure(ByVal oFigure As clChessFigure)
        Me.Figure = oFigure
        Me.RefreshFigure()
    End Sub

    Public Sub RefreshFigure()
        If Me.Figure IsNot Nothing Then
            Me.InnerField.Text = Me.Figure.FigureID
        Else
            Me.InnerField.Text = ""
        End If
    End Sub

    Public Function GetGlowColor(ByVal GlowMode As mdSettings.enGlowMode) As Color
        Select Case GlowMode
            Case enGlowMode.Bad : Return mdSettings.moColor_GlowBad
            Case enGlowMode.Good : Return mdSettings.moColor_GlowGood
            Case enGlowMode.Neutral : Return mdSettings.moColor_GlowNeutral
            Case enGlowMode.Off : Return mdSettings.moColor_GlowOff
        End Select
    End Function

    Public Sub Glow(ByVal bGlowOn As Boolean, Optional ByVal GlowMode As mdSettings.enGlowMode = enGlowMode.Neutral)
        If bGlowOn Then
            Me.GlowOn(GlowMode)
        Else
            Me.GlowOff()
        End If
    End Sub

    Public Sub GlowOn(Optional ByVal GlowMode As mdSettings.enGlowMode = enGlowMode.Neutral)
        Me.InnerField.BackColor = GetGlowColor(GlowMode)
    End Sub

    Public Sub GlowOff()
        Me.InnerField.BackColor = Color.Transparent
    End Sub

End Class
