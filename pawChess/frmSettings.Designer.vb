<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbStartingPlayer = New System.Windows.Forms.ComboBox()
        Me.cmbPlayerWhiteTyp = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.frameAllgemein = New System.Windows.Forms.GroupBox()
        Me.framePlayerWhite = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.frameAllgemein.SuspendLayout()
        Me.framePlayerWhite.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbStartingPlayer
        '
        Me.cmbStartingPlayer.FormattingEnabled = True
        Me.cmbStartingPlayer.Location = New System.Drawing.Point(110, 19)
        Me.cmbStartingPlayer.Name = "cmbStartingPlayer"
        Me.cmbStartingPlayer.Size = New System.Drawing.Size(170, 21)
        Me.cmbStartingPlayer.TabIndex = 0
        '
        'cmbPlayerWhiteTyp
        '
        Me.cmbPlayerWhiteTyp.FormattingEnabled = True
        Me.cmbPlayerWhiteTyp.Location = New System.Drawing.Point(110, 19)
        Me.cmbPlayerWhiteTyp.Name = "cmbPlayerWhiteTyp"
        Me.cmbPlayerWhiteTyp.Size = New System.Drawing.Size(170, 21)
        Me.cmbPlayerWhiteTyp.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 21)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "1. Zug"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frameAllgemein
        '
        Me.frameAllgemein.Controls.Add(Me.Label1)
        Me.frameAllgemein.Controls.Add(Me.cmbStartingPlayer)
        Me.frameAllgemein.Location = New System.Drawing.Point(12, 12)
        Me.frameAllgemein.Name = "frameAllgemein"
        Me.frameAllgemein.Size = New System.Drawing.Size(286, 53)
        Me.frameAllgemein.TabIndex = 6
        Me.frameAllgemein.TabStop = False
        Me.frameAllgemein.Text = "Allgemein"
        '
        'framePlayerWhite
        '
        Me.framePlayerWhite.Controls.Add(Me.Label4)
        Me.framePlayerWhite.Controls.Add(Me.Label3)
        Me.framePlayerWhite.Controls.Add(Me.ComboBox1)
        Me.framePlayerWhite.Controls.Add(Me.Label2)
        Me.framePlayerWhite.Controls.Add(Me.cmbPlayerWhiteTyp)
        Me.framePlayerWhite.Location = New System.Drawing.Point(12, 71)
        Me.framePlayerWhite.Name = "framePlayerWhite"
        Me.framePlayerWhite.Size = New System.Drawing.Size(286, 126)
        Me.framePlayerWhite.TabIndex = 7
        Me.framePlayerWhite.TabStop = False
        Me.framePlayerWhite.Text = "Spieler Weiss"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 21)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Spielertyp"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 21)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Spielertyp"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(110, 46)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(170, 21)
        Me.ComboBox1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Spielertyp"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(472, 379)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Label5"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 410)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.framePlayerWhite)
        Me.Controls.Add(Me.frameAllgemein)
        Me.Name = "frmSettings"
        Me.Text = "frmSettings"
        Me.frameAllgemein.ResumeLayout(False)
        Me.framePlayerWhite.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbStartingPlayer As ComboBox
    Friend WithEvents cmbPlayerWhiteTyp As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents frameAllgemein As GroupBox
    Friend WithEvents framePlayerWhite As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
