Option Strict On
Option Explicit On

Public Class frmSettings

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.KeyPreview = True

        Dim txtNew As New TextBox
        txtNew.Parent = Me

        Me.Controls.Add(txtNew)
    End Sub

    Public Sub frmSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Public Sub LoadWithSettings(ByVal oSettings As clSettings)
        Me.Show()
    End Sub

End Class