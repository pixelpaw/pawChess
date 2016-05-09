Option Strict On
Option Explicit On

Public Class frmMain

    Public Shared mnuMain As MenuStrip = Nothing
    Public Game As clGameLogic = Nothing

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMenu()

        Me.Text = "pawChess"
        Me.ClientSize = New Size(mdDefaultValues.mnSize_ClientSize_Width, mdDefaultValues.mnSize_ClientSize_Height + mdDefaultValues.mnSize_Menu_Height)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Location = New Point(CInt((mdDefaultValues.mScreenWidth - Me.Size.Width) / 4), CInt((mdDefaultValues.mScreenHeight - Me.Size.Height) / 3))

        InitGame()
    End Sub

    Private Sub InitGame()
        Game = New clGameLogic
        Game.NewGame()
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        ResizeGame()
    End Sub

    Private Sub frmMain_ResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ResizeEnd
        ResizeGame()
    End Sub

    Private Sub ResizeGame()
        Dim bIsMaximized As Boolean = Me.WindowState = FormWindowState.Maximized

        If Game IsNot Nothing Then
            Game.ResizeBoard(bIsMaximized)
        End If
    End Sub

    Private Sub ShowSettings()
        frmSettings.Show()
        '        Dim frmSettings As Form2
        'Set myFirstForm = New Form2
        'Set mySecondForm = New Form2
        'Set myThirdForm = New Form2
        'myFirstForm.Show
    End Sub

    Private Sub LoadMenu()
        mnuMain = New MenuStrip()
        mnuMain.Dock = DockStyle.Top

        ' Teil 1 - Spiel
        Dim mnuItem_Game As New ToolStripMenuItem("Spiel")
        Dim mnuItem_Game_NewGame As New ToolStripMenuItem("Neues Spiel", Nothing, New EventHandler(AddressOf Menu_Click), CType(Keys.Control + Keys.N, Keys))
        Dim mnuItem_Game_NewGame_Option1 As New ToolStripMenuItem("Neues Spiel - Param 1", Nothing, New EventHandler(AddressOf Menu_Click), CType(Keys.Control + Keys.D1, Keys))
        Dim mnuItem_Game_NewGame_Option2 As New ToolStripMenuItem("Neues Spiel - Param 2", Nothing, New EventHandler(AddressOf Menu_Click), CType(Keys.Control + Keys.D2, Keys))
        Dim mnuItem_Game_NewGame_Option3 As New ToolStripMenuItem("Neues Spiel - Param 3", Nothing, New EventHandler(AddressOf Menu_Click), CType(Keys.Control + Keys.D3, Keys))
        Dim mnuItem_Game_Quit As New ToolStripMenuItem("Beenden", Nothing, New EventHandler(AddressOf Menu_Click))

        mnuMain.Items.Add(mnuItem_Game)
        mnuItem_Game.DropDownItems.AddRange(New ToolStripItem() {
            mnuItem_Game_NewGame,
            New ToolStripSeparator(),
            mnuItem_Game_NewGame_Option1,
            mnuItem_Game_NewGame_Option2,
            mnuItem_Game_NewGame_Option3,
            New ToolStripSeparator(),
            mnuItem_Game_Quit})

        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowImageMargin = False
        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowCheckMargin = True

        ' Teil 2 - Optionen
        Dim mnuItem_Options As New ToolStripMenuItem("Optionen")
        Dim mnuItem_Options_Settings As New ToolStripMenuItem("Einstellungen", Nothing, New EventHandler(AddressOf Menu_Click), Keys.F9)

        mnuMain.Items.Add(mnuItem_Options)
        mnuItem_Options.DropDownItems.AddRange(New ToolStripItem() {
            mnuItem_Options_Settings})

        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowImageMargin = False
        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowCheckMargin = True

        ' Teil 3 - Hilfe
        Dim mnuItem_Help As New ToolStripMenuItem("Hilfe")
        Dim mnuItem_Help_Chess As New ToolStripMenuItem("über Schach", Nothing, New EventHandler(AddressOf Menu_Click))
        Dim mnuItem_Help_Info As New ToolStripMenuItem("über pawChess", Nothing, New EventHandler(AddressOf Menu_Click))

        mnuMain.Items.Add(mnuItem_Help)
        mnuItem_Help.DropDownItems.AddRange(New ToolStripItem() {
            mnuItem_Help_Chess,
            mnuItem_Help_Info})

        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowImageMargin = False
        CType(mnuItem_Game.DropDown, ToolStripDropDownMenu).ShowCheckMargin = True

        mnuMain.Items.Add(mnuItem_Game)
        mnuMain.Items.Add(mnuItem_Options)
        mnuMain.Items.Add(mnuItem_Help)
        mnuMain.MdiWindowListItem = mnuItem_Game

        Me.MainMenuStrip = mnuMain
        Me.Controls.Add(mnuMain)

        mdDefaultValues.mnSize_Menu_Height = mnuMain.Height
    End Sub

    Private Sub Menu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim strSelectedOption As String = CType(sender, ToolStripMenuItem).Text

        Select Case strSelectedOption
            Case "Neues Spiel" : Me.Game.NewGame()
            Case "Neues Spiel - Param 1" : Me.Game.NewGame(1)
            Case "Neues Spiel - Param 2" : Me.Game.NewGame(2)
            Case "Neues Spiel - Param 3" : Me.Game.NewGame(3)

            Case "Beenden" : Me.Dispose()

            Case "Einstellungen" : Me.Game.Settings.ShowSettings()

            Case "über Schach" : Process.Start("https: //de.wikipedia.org/wiki/Schach")
            Case "über pawChess"
        End Select
    End Sub

End Class
