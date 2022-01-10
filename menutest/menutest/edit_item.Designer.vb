<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class edit_item
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.edit_display = New System.Windows.Forms.Label()
        Me.edit_display_txtbx = New System.Windows.Forms.TextBox()
        Me.edit_type_title = New System.Windows.Forms.RadioButton()
        Me.edit_type_command = New System.Windows.Forms.RadioButton()
        Me.edit_type_info = New System.Windows.Forms.RadioButton()
        Me.edit_type_menu = New System.Windows.Forms.RadioButton()
        Me.edit_type_badge = New System.Windows.Forms.RadioButton()
        Me.edit_command_txt_box = New System.Windows.Forms.TextBox()
        Me.edit_command_box = New System.Windows.Forms.Label()
        Me.edit_badge_command = New System.Windows.Forms.Label()
        Me.edit_badge_dropdown = New System.Windows.Forms.Label()
        Me.edit_badge_cmbbx = New System.Windows.Forms.ComboBox()
        Me.edit_save_button = New System.Windows.Forms.Button()
        Me.edit_exit_btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'edit_display
        '
        Me.edit_display.AutoSize = True
        Me.edit_display.Location = New System.Drawing.Point(25, 24)
        Me.edit_display.Name = "edit_display"
        Me.edit_display.Size = New System.Drawing.Size(86, 13)
        Me.edit_display.TabIndex = 0
        Me.edit_display.Text = "Edit Display Text"
        '
        'edit_display_txtbx
        '
        Me.edit_display_txtbx.Location = New System.Drawing.Point(26, 48)
        Me.edit_display_txtbx.Name = "edit_display_txtbx"
        Me.edit_display_txtbx.Size = New System.Drawing.Size(258, 20)
        Me.edit_display_txtbx.TabIndex = 1
        '
        'edit_type_title
        '
        Me.edit_type_title.AutoSize = True
        Me.edit_type_title.Location = New System.Drawing.Point(26, 74)
        Me.edit_type_title.Name = "edit_type_title"
        Me.edit_type_title.Size = New System.Drawing.Size(45, 17)
        Me.edit_type_title.TabIndex = 2
        Me.edit_type_title.TabStop = True
        Me.edit_type_title.Text = "Title"
        Me.edit_type_title.UseVisualStyleBackColor = True
        '
        'edit_type_command
        '
        Me.edit_type_command.AutoSize = True
        Me.edit_type_command.Location = New System.Drawing.Point(26, 97)
        Me.edit_type_command.Name = "edit_type_command"
        Me.edit_type_command.Size = New System.Drawing.Size(72, 17)
        Me.edit_type_command.TabIndex = 3
        Me.edit_type_command.TabStop = True
        Me.edit_type_command.Text = "Command"
        Me.edit_type_command.UseVisualStyleBackColor = True
        '
        'edit_type_info
        '
        Me.edit_type_info.AutoSize = True
        Me.edit_type_info.Location = New System.Drawing.Point(104, 74)
        Me.edit_type_info.Name = "edit_type_info"
        Me.edit_type_info.Size = New System.Drawing.Size(80, 17)
        Me.edit_type_info.TabIndex = 4
        Me.edit_type_info.TabStop = True
        Me.edit_type_info.Text = "Info Display"
        Me.edit_type_info.UseVisualStyleBackColor = True
        '
        'edit_type_menu
        '
        Me.edit_type_menu.AutoSize = True
        Me.edit_type_menu.Location = New System.Drawing.Point(104, 97)
        Me.edit_type_menu.Name = "edit_type_menu"
        Me.edit_type_menu.Size = New System.Drawing.Size(52, 17)
        Me.edit_type_menu.TabIndex = 5
        Me.edit_type_menu.TabStop = True
        Me.edit_type_menu.Text = "Menu"
        Me.edit_type_menu.UseVisualStyleBackColor = True
        '
        'edit_type_badge
        '
        Me.edit_type_badge.AutoSize = True
        Me.edit_type_badge.Location = New System.Drawing.Point(185, 97)
        Me.edit_type_badge.Name = "edit_type_badge"
        Me.edit_type_badge.Size = New System.Drawing.Size(99, 17)
        Me.edit_type_badge.TabIndex = 6
        Me.edit_type_badge.TabStop = True
        Me.edit_type_badge.Text = "Light Up Badge"
        Me.edit_type_badge.UseVisualStyleBackColor = True
        '
        'edit_command_txt_box
        '
        Me.edit_command_txt_box.Location = New System.Drawing.Point(26, 161)
        Me.edit_command_txt_box.Multiline = True
        Me.edit_command_txt_box.Name = "edit_command_txt_box"
        Me.edit_command_txt_box.Size = New System.Drawing.Size(258, 76)
        Me.edit_command_txt_box.TabIndex = 7
        Me.edit_command_txt_box.Visible = False
        '
        'edit_command_box
        '
        Me.edit_command_box.AutoSize = True
        Me.edit_command_box.Location = New System.Drawing.Point(25, 145)
        Me.edit_command_box.Name = "edit_command_box"
        Me.edit_command_box.Size = New System.Drawing.Size(146, 13)
        Me.edit_command_box.TabIndex = 8
        Me.edit_command_box.Text = "Command Text Entry/Review"
        Me.edit_command_box.Visible = False
        '
        'edit_badge_command
        '
        Me.edit_badge_command.AutoSize = True
        Me.edit_badge_command.Location = New System.Drawing.Point(25, 132)
        Me.edit_badge_command.Name = "edit_badge_command"
        Me.edit_badge_command.Size = New System.Drawing.Size(127, 13)
        Me.edit_badge_command.TabIndex = 9
        Me.edit_badge_command.Text = "Badge Engine Reference"
        Me.edit_badge_command.Visible = False
        '
        'edit_badge_dropdown
        '
        Me.edit_badge_dropdown.AutoSize = True
        Me.edit_badge_dropdown.Location = New System.Drawing.Point(23, 250)
        Me.edit_badge_dropdown.Name = "edit_badge_dropdown"
        Me.edit_badge_dropdown.Size = New System.Drawing.Size(43, 13)
        Me.edit_badge_dropdown.TabIndex = 10
        Me.edit_badge_dropdown.Text = "Badges"
        Me.edit_badge_dropdown.Visible = False
        '
        'edit_badge_cmbbx
        '
        Me.edit_badge_cmbbx.FormattingEnabled = True
        Me.edit_badge_cmbbx.Location = New System.Drawing.Point(26, 266)
        Me.edit_badge_cmbbx.Name = "edit_badge_cmbbx"
        Me.edit_badge_cmbbx.Size = New System.Drawing.Size(216, 21)
        Me.edit_badge_cmbbx.TabIndex = 8
        Me.edit_badge_cmbbx.Visible = False
        '
        'edit_save_button
        '
        Me.edit_save_button.Location = New System.Drawing.Point(28, 304)
        Me.edit_save_button.Name = "edit_save_button"
        Me.edit_save_button.Size = New System.Drawing.Size(92, 36)
        Me.edit_save_button.TabIndex = 9
        Me.edit_save_button.Text = "Save"
        Me.edit_save_button.UseVisualStyleBackColor = True
        '
        'edit_exit_btn
        '
        Me.edit_exit_btn.Location = New System.Drawing.Point(192, 304)
        Me.edit_exit_btn.Name = "edit_exit_btn"
        Me.edit_exit_btn.Size = New System.Drawing.Size(92, 36)
        Me.edit_exit_btn.TabIndex = 10
        Me.edit_exit_btn.Text = "Exit"
        Me.edit_exit_btn.UseVisualStyleBackColor = True
        '
        'edit_item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 358)
        Me.Controls.Add(Me.edit_exit_btn)
        Me.Controls.Add(Me.edit_save_button)
        Me.Controls.Add(Me.edit_badge_cmbbx)
        Me.Controls.Add(Me.edit_badge_dropdown)
        Me.Controls.Add(Me.edit_badge_command)
        Me.Controls.Add(Me.edit_command_box)
        Me.Controls.Add(Me.edit_command_txt_box)
        Me.Controls.Add(Me.edit_type_badge)
        Me.Controls.Add(Me.edit_type_menu)
        Me.Controls.Add(Me.edit_type_info)
        Me.Controls.Add(Me.edit_type_command)
        Me.Controls.Add(Me.edit_type_title)
        Me.Controls.Add(Me.edit_display_txtbx)
        Me.Controls.Add(Me.edit_display)
        Me.Name = "edit_item"
        Me.Text = "Edit Existing Item"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents edit_display As System.Windows.Forms.Label
    Friend WithEvents edit_display_txtbx As System.Windows.Forms.TextBox
    Friend WithEvents edit_type_title As System.Windows.Forms.RadioButton
    Friend WithEvents edit_type_command As System.Windows.Forms.RadioButton
    Friend WithEvents edit_type_info As System.Windows.Forms.RadioButton
    Friend WithEvents edit_type_menu As System.Windows.Forms.RadioButton
    Friend WithEvents edit_type_badge As System.Windows.Forms.RadioButton
    Friend WithEvents edit_command_txt_box As System.Windows.Forms.TextBox
    Friend WithEvents edit_command_box As System.Windows.Forms.Label
    Friend WithEvents edit_badge_command As System.Windows.Forms.Label
    Friend WithEvents edit_badge_dropdown As System.Windows.Forms.Label
    Friend WithEvents edit_badge_cmbbx As System.Windows.Forms.ComboBox
    Friend WithEvents edit_save_button As System.Windows.Forms.Button
    Friend WithEvents edit_exit_btn As System.Windows.Forms.Button
End Class
