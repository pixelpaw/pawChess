Option Strict On
Option Explicit On

Public Class frmSettings

    Public Settings As clSettings = Nothing

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        Me.KeyPreview = True

        InitForm()
    End Sub

    Private Sub frmSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Public Sub LoadWithSettings(ByVal oSettings As clSettings)
        Me.Settings = oSettings
        Me.Show()
    End Sub

    Private Sub InitForm()
        'Me.ClientSize = New Size((mdDefaultValues.mnDefaultPos * 3) + mdDefaultValues.mnSize_Label_Width + mdDefaultValues.mnSize_ComboBox_Width, 250)
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.MinimumSize = Me.Size
        Me.MaximumSize = Me.Size
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        Me.Size = New Size(Me.Size.Width, Me.Size.Height + 45)

        ' Button Cancel
        Dim cmdCancel As New Button
        cmdCancel.Parent = Me
        cmdCancel.Text = "Abbruch"
        cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        cmdCancel.Size = New Size(mdDefaultValues.mnSize_Label_Width, mdDefaultValues.mnSize_Label_Height)
        cmdCancel.Location = New Point(Me.ClientSize.Width - cmdCancel.Width - mdDefaultValues.mnDefaultPos, Me.ClientSize.Height - cmdCancel.Height - mdDefaultValues.mnDefaultPos)

        ' Button Ok
        Dim cmdOk As New Button
        cmdOk.Parent = Me
        cmdOk.Text = "Ok"
        cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        cmdOk.Size = New Size(mdDefaultValues.mnSize_Label_Width, mdDefaultValues.mnSize_Label_Height)
        cmdOk.Location = New Point(cmdCancel.Location.X - cmdOk.Width - mdDefaultValues.mnDefaultPos, cmdCancel.Location.Y)

        '' Label StartingPlayer 
        'Dim lblStartingPlayer As New Label
        'lblStartingPlayer.Parent = Me
        'lblStartingPlayer.AutoSize = False
        'lblStartingPlayer.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos)
        'lblStartingPlayer.Font = mdDefaultValues.mFont_Small_Regular
        'lblStartingPlayer.Name = "lblStartingPlayer"
        'lblStartingPlayer.Text = "1. Zug"
        'lblStartingPlayer.Size = New Size(mdDefaultValues.mnSize_Label_Width, mdDefaultValues.mnSize_Label_Height)

        '' ComboBox StartingPlayer
        'Dim cmbStartingPlayer As New ComboBox
        'cmbStartingPlayer.Parent = Me
        'cmbStartingPlayer.Location = New Point((lblStartingPlayer.Location.X * 2) + lblStartingPlayer.Size.Width, mdDefaultValues.mnDefaultPos)
        'cmbStartingPlayer.Size = New Size(mdDefaultValues.mnSize_ComboBox_Width, mdDefaultValues.mnSize_ComboBox_Height)
        'cmbStartingPlayer.DisplayMember = "Key"
        'cmbStartingPlayer.ValueMember = "Value"


        '' Label Spieler Weiss 
        'Dim lblPlayerWhite As New Label
        'lblPlayerWhite.Parent = Me
        'lblPlayerWhite.AutoSize = False
        'lblPlayerWhite.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos)
        'lblPlayerWhite.Font = mdDefaultValues.mFont_Small_Regular
        'lblPlayerWhite.Name = "lblStartingPlayer"
        'lblPlayerWhite.Text = "1. Zug"
        'lblPlayerWhite.Size = New Size(mdDefaultValues.mnSize_Label_Width, mdDefaultValues.mnSize_Label_Height)

        '' ComboBox Spieler Weiss
        'Dim cmbPlayerWhite As New ComboBox
        'cmbPlayerWhite.Parent = Me
        'cmbPlayerWhite.Location = New Point((lblStartingPlayer.Location.X * 2) + lblStartingPlayer.Size.Width, mdDefaultValues.mnDefaultPos)
        'cmbPlayerWhite.Size = New Size(mdDefaultValues.mnSize_ComboBox_Width, mdDefaultValues.mnSize_ComboBox_Height)
        'cmbPlayerWhite.DisplayMember = "Key"
        'cmbPlayerWhite.ValueMember = "Value"


        '' Label Spieler Schwarz 
        'Dim lblPlayerBlack As New Label
        'lblPlayerBlack.Parent = Me
        'lblPlayerBlack.AutoSize = False
        'lblPlayerBlack.Location = New Point(mdDefaultValues.mnDefaultPos, mdDefaultValues.mnDefaultPos)
        'lblPlayerBlack.Font = mdDefaultValues.mFont_Small_Regular
        'lblPlayerBlack.Name = "lblStartingPlayer"
        'lblPlayerBlack.Text = "1. Zug"
        'lblPlayerBlack.Size = New Size(mdDefaultValues.mnSize_Label_Width, mdDefaultValues.mnSize_Label_Height)

        '' ComboBox Spieler Schwarz
        'Dim cmbPlayerBlack As New ComboBox
        'cmbPlayerBlack.Parent = Me
        'cmbPlayerBlack.Location = New Point((lblStartingPlayer.Location.X * 2) + lblStartingPlayer.Size.Width, mdDefaultValues.mnDefaultPos)
        'cmbPlayerBlack.Size = New Size(mdDefaultValues.mnSize_ComboBox_Width, mdDefaultValues.mnSize_ComboBox_Height)
        'cmbPlayerBlack.DisplayMember = "Key"
        'cmbPlayerBlack.ValueMember = "Value"

    End Sub

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class