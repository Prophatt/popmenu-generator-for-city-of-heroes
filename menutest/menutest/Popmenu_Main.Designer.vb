<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.poptree = New System.Windows.Forms.TreeView()
        Me.poptree_right_clk = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.display_lbl = New System.Windows.Forms.Label()
        Me.display_txtbox = New System.Windows.Forms.TextBox()
        Me.remove_node_btn = New System.Windows.Forms.Button()
        Me.info_radio_btn = New System.Windows.Forms.RadioButton()
        Me.menu_radio_btn = New System.Windows.Forms.RadioButton()
        Me.cmd_radio = New System.Windows.Forms.RadioButton()
        Me.badge_radio_btn = New System.Windows.Forms.RadioButton()
        Me.command_label = New System.Windows.Forms.Label()
        Me.commandtxtbox = New System.Windows.Forms.TextBox()
        Me.badges_cmbbx_lbl = New System.Windows.Forms.Label()
        Me.badges_cmbbx = New System.Windows.Forms.ComboBox()
        Me.menu_name_lbl = New System.Windows.Forms.Label()
        Me.menu_name_txtbox = New System.Windows.Forms.TextBox()
        Me.save_btn = New System.Windows.Forms.Button()
        Me.load_btn = New System.Windows.Forms.Button()
        Me.export_game_btn = New System.Windows.Forms.Button()
        Me.exit_btn = New System.Windows.Forms.Button()
        Me.cmd_cntrl_btn = New System.Windows.Forms.Button()
        Me.item_radio_label = New System.Windows.Forms.Label()
        Me.written_by_lbl = New System.Windows.Forms.Label()
        Me.written_by_txtbox = New System.Windows.Forms.TextBox()
        Me.version_num_lbl = New System.Windows.Forms.Label()
        Me.version_num_txtbox = New System.Windows.Forms.TextBox()
        Me.divider_button = New System.Windows.Forms.Button()
        Me.title_radio = New System.Windows.Forms.RadioButton()
        Me.delete_all_btn = New System.Windows.Forms.Button()
        Me.badge_reference_label = New System.Windows.Forms.Label()
        Me.add_menu_item_btn = New System.Windows.Forms.Button()
        Me.preview_btn = New System.Windows.Forms.Button()
        Me.main_menustrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadSavedFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveCurrentTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportGameFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.move_up_btn = New System.Windows.Forms.Button()
        Me.poptree_right_clk.SuspendLayout()
        Me.main_menustrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'poptree
        '
        Me.poptree.ContextMenuStrip = Me.poptree_right_clk
        Me.poptree.HideSelection = False
        Me.poptree.Location = New System.Drawing.Point(12, 27)
        Me.poptree.Name = "poptree"
        Me.poptree.Size = New System.Drawing.Size(366, 533)
        Me.poptree.TabIndex = 0
        '
        'poptree_right_clk
        '
        Me.poptree_right_clk.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditItemToolStripMenuItem})
        Me.poptree_right_clk.Name = "poptree_right_clk"
        Me.poptree_right_clk.Size = New System.Drawing.Size(122, 26)
        '
        'EditItemToolStripMenuItem
        '
        Me.EditItemToolStripMenuItem.Name = "EditItemToolStripMenuItem"
        Me.EditItemToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
        Me.EditItemToolStripMenuItem.Text = "Edit Item"
        '
        'display_lbl
        '
        Me.display_lbl.AutoSize = True
        Me.display_lbl.Location = New System.Drawing.Point(384, 211)
        Me.display_lbl.Name = "display_lbl"
        Me.display_lbl.Size = New System.Drawing.Size(65, 13)
        Me.display_lbl.TabIndex = 1
        Me.display_lbl.Text = "Display Text"
        '
        'display_txtbox
        '
        Me.display_txtbox.Location = New System.Drawing.Point(387, 227)
        Me.display_txtbox.Name = "display_txtbox"
        Me.display_txtbox.Size = New System.Drawing.Size(229, 20)
        Me.display_txtbox.TabIndex = 9
        '
        'remove_node_btn
        '
        Me.remove_node_btn.Location = New System.Drawing.Point(389, 483)
        Me.remove_node_btn.Name = "remove_node_btn"
        Me.remove_node_btn.Size = New System.Drawing.Size(92, 36)
        Me.remove_node_btn.TabIndex = 14
        Me.remove_node_btn.Text = "Remove Item from Menu"
        Me.remove_node_btn.UseVisualStyleBackColor = True
        '
        'info_radio_btn
        '
        Me.info_radio_btn.AutoSize = True
        Me.info_radio_btn.Location = New System.Drawing.Point(490, 158)
        Me.info_radio_btn.Name = "info_radio_btn"
        Me.info_radio_btn.Size = New System.Drawing.Size(78, 17)
        Me.info_radio_btn.TabIndex = 5
        Me.info_radio_btn.TabStop = True
        Me.info_radio_btn.Text = "Info display"
        Me.info_radio_btn.UseVisualStyleBackColor = True
        '
        'menu_radio_btn
        '
        Me.menu_radio_btn.AutoSize = True
        Me.menu_radio_btn.Location = New System.Drawing.Point(490, 181)
        Me.menu_radio_btn.Name = "menu_radio_btn"
        Me.menu_radio_btn.Size = New System.Drawing.Size(73, 17)
        Me.menu_radio_btn.TabIndex = 7
        Me.menu_radio_btn.TabStop = True
        Me.menu_radio_btn.Text = "menu item"
        Me.menu_radio_btn.UseVisualStyleBackColor = True
        '
        'cmd_radio
        '
        Me.cmd_radio.AutoSize = True
        Me.cmd_radio.Location = New System.Drawing.Point(385, 181)
        Me.cmd_radio.Name = "cmd_radio"
        Me.cmd_radio.Size = New System.Drawing.Size(95, 17)
        Me.cmd_radio.TabIndex = 6
        Me.cmd_radio.TabStop = True
        Me.cmd_radio.Text = "Command Item"
        Me.cmd_radio.UseVisualStyleBackColor = True
        '
        'badge_radio_btn
        '
        Me.badge_radio_btn.AutoSize = True
        Me.badge_radio_btn.Location = New System.Drawing.Point(569, 181)
        Me.badge_radio_btn.Name = "badge_radio_btn"
        Me.badge_radio_btn.Size = New System.Drawing.Size(99, 17)
        Me.badge_radio_btn.TabIndex = 8
        Me.badge_radio_btn.TabStop = True
        Me.badge_radio_btn.Text = "Light Up Badge"
        Me.badge_radio_btn.UseVisualStyleBackColor = True
        '
        'command_label
        '
        Me.command_label.AutoSize = True
        Me.command_label.Location = New System.Drawing.Point(384, 263)
        Me.command_label.Name = "command_label"
        Me.command_label.Size = New System.Drawing.Size(105, 13)
        Me.command_label.TabIndex = 11
        Me.command_label.Text = "Command Text Entry"
        '
        'commandtxtbox
        '
        Me.commandtxtbox.Location = New System.Drawing.Point(387, 294)
        Me.commandtxtbox.Multiline = True
        Me.commandtxtbox.Name = "commandtxtbox"
        Me.commandtxtbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.commandtxtbox.Size = New System.Drawing.Size(290, 58)
        Me.commandtxtbox.TabIndex = 10
        '
        'badges_cmbbx_lbl
        '
        Me.badges_cmbbx_lbl.AutoSize = True
        Me.badges_cmbbx_lbl.Location = New System.Drawing.Point(387, 355)
        Me.badges_cmbbx_lbl.Name = "badges_cmbbx_lbl"
        Me.badges_cmbbx_lbl.Size = New System.Drawing.Size(43, 13)
        Me.badges_cmbbx_lbl.TabIndex = 13
        Me.badges_cmbbx_lbl.Text = "Badges"
        '
        'badges_cmbbx
        '
        Me.badges_cmbbx.FormattingEnabled = True
        Me.badges_cmbbx.Location = New System.Drawing.Point(388, 371)
        Me.badges_cmbbx.Name = "badges_cmbbx"
        Me.badges_cmbbx.Size = New System.Drawing.Size(255, 21)
        Me.badges_cmbbx.TabIndex = 11
        '
        'menu_name_lbl
        '
        Me.menu_name_lbl.AutoSize = True
        Me.menu_name_lbl.Location = New System.Drawing.Point(384, 27)
        Me.menu_name_lbl.Name = "menu_name_lbl"
        Me.menu_name_lbl.Size = New System.Drawing.Size(65, 13)
        Me.menu_name_lbl.TabIndex = 15
        Me.menu_name_lbl.Text = "Menu Name"
        '
        'menu_name_txtbox
        '
        Me.menu_name_txtbox.Location = New System.Drawing.Point(385, 41)
        Me.menu_name_txtbox.Name = "menu_name_txtbox"
        Me.menu_name_txtbox.Size = New System.Drawing.Size(229, 20)
        Me.menu_name_txtbox.TabIndex = 1
        '
        'save_btn
        '
        Me.save_btn.Location = New System.Drawing.Point(585, 440)
        Me.save_btn.Name = "save_btn"
        Me.save_btn.Size = New System.Drawing.Size(92, 36)
        Me.save_btn.TabIndex = 17
        Me.save_btn.Text = "Save"
        Me.save_btn.UseVisualStyleBackColor = True
        '
        'load_btn
        '
        Me.load_btn.Location = New System.Drawing.Point(585, 398)
        Me.load_btn.Name = "load_btn"
        Me.load_btn.Size = New System.Drawing.Size(92, 36)
        Me.load_btn.TabIndex = 16
        Me.load_btn.Text = "Load"
        Me.load_btn.UseVisualStyleBackColor = True
        '
        'export_game_btn
        '
        Me.export_game_btn.Location = New System.Drawing.Point(584, 482)
        Me.export_game_btn.Name = "export_game_btn"
        Me.export_game_btn.Size = New System.Drawing.Size(93, 36)
        Me.export_game_btn.TabIndex = 19
        Me.export_game_btn.Text = "Export to Game File"
        Me.export_game_btn.UseVisualStyleBackColor = True
        '
        'exit_btn
        '
        Me.exit_btn.Location = New System.Drawing.Point(585, 524)
        Me.exit_btn.Name = "exit_btn"
        Me.exit_btn.Size = New System.Drawing.Size(92, 36)
        Me.exit_btn.TabIndex = 18
        Me.exit_btn.Text = "Exit"
        Me.exit_btn.UseVisualStyleBackColor = True
        '
        'cmd_cntrl_btn
        '
        Me.cmd_cntrl_btn.Location = New System.Drawing.Point(524, 253)
        Me.cmd_cntrl_btn.Name = "cmd_cntrl_btn"
        Me.cmd_cntrl_btn.Size = New System.Drawing.Size(92, 36)
        Me.cmd_cntrl_btn.TabIndex = 21
        Me.cmd_cntrl_btn.Text = "Open Command Helper"
        Me.cmd_cntrl_btn.UseVisualStyleBackColor = True
        '
        'item_radio_label
        '
        Me.item_radio_label.AutoSize = True
        Me.item_radio_label.Location = New System.Drawing.Point(386, 142)
        Me.item_radio_label.Name = "item_radio_label"
        Me.item_radio_label.Size = New System.Drawing.Size(115, 13)
        Me.item_radio_label.TabIndex = 23
        Me.item_radio_label.Text = "PopMenu Item Options"
        '
        'written_by_lbl
        '
        Me.written_by_lbl.AutoSize = True
        Me.written_by_lbl.Location = New System.Drawing.Point(386, 64)
        Me.written_by_lbl.Name = "written_by_lbl"
        Me.written_by_lbl.Size = New System.Drawing.Size(56, 13)
        Me.written_by_lbl.TabIndex = 24
        Me.written_by_lbl.Text = "Written By"
        '
        'written_by_txtbox
        '
        Me.written_by_txtbox.Location = New System.Drawing.Point(387, 80)
        Me.written_by_txtbox.Name = "written_by_txtbox"
        Me.written_by_txtbox.Size = New System.Drawing.Size(229, 20)
        Me.written_by_txtbox.TabIndex = 2
        '
        'version_num_lbl
        '
        Me.version_num_lbl.AutoSize = True
        Me.version_num_lbl.Location = New System.Drawing.Point(386, 103)
        Me.version_num_lbl.Name = "version_num_lbl"
        Me.version_num_lbl.Size = New System.Drawing.Size(82, 13)
        Me.version_num_lbl.TabIndex = 26
        Me.version_num_lbl.Text = "Version Number"
        '
        'version_num_txtbox
        '
        Me.version_num_txtbox.Location = New System.Drawing.Point(385, 119)
        Me.version_num_txtbox.Name = "version_num_txtbox"
        Me.version_num_txtbox.Size = New System.Drawing.Size(229, 20)
        Me.version_num_txtbox.TabIndex = 3
        '
        'divider_button
        '
        Me.divider_button.Location = New System.Drawing.Point(390, 440)
        Me.divider_button.Name = "divider_button"
        Me.divider_button.Size = New System.Drawing.Size(92, 36)
        Me.divider_button.TabIndex = 13
        Me.divider_button.Text = "Add Divider"
        Me.divider_button.UseVisualStyleBackColor = True
        '
        'title_radio
        '
        Me.title_radio.AutoSize = True
        Me.title_radio.Location = New System.Drawing.Point(385, 158)
        Me.title_radio.Name = "title_radio"
        Me.title_radio.Size = New System.Drawing.Size(45, 17)
        Me.title_radio.TabIndex = 4
        Me.title_radio.TabStop = True
        Me.title_radio.Text = "Title"
        Me.title_radio.UseVisualStyleBackColor = True
        '
        'delete_all_btn
        '
        Me.delete_all_btn.Location = New System.Drawing.Point(487, 482)
        Me.delete_all_btn.Name = "delete_all_btn"
        Me.delete_all_btn.Size = New System.Drawing.Size(92, 36)
        Me.delete_all_btn.TabIndex = 15
        Me.delete_all_btn.Text = "Clear Entire Tree"
        Me.delete_all_btn.UseVisualStyleBackColor = True
        '
        'badge_reference_label
        '
        Me.badge_reference_label.AutoSize = True
        Me.badge_reference_label.Location = New System.Drawing.Point(384, 250)
        Me.badge_reference_label.Name = "badge_reference_label"
        Me.badge_reference_label.Size = New System.Drawing.Size(127, 13)
        Me.badge_reference_label.TabIndex = 32
        Me.badge_reference_label.Text = "Badge Engine Reference"
        '
        'add_menu_item_btn
        '
        Me.add_menu_item_btn.Location = New System.Drawing.Point(390, 398)
        Me.add_menu_item_btn.Name = "add_menu_item_btn"
        Me.add_menu_item_btn.Size = New System.Drawing.Size(92, 36)
        Me.add_menu_item_btn.TabIndex = 12
        Me.add_menu_item_btn.Text = "Save Item to Menu"
        Me.add_menu_item_btn.UseVisualStyleBackColor = True
        '
        'preview_btn
        '
        Me.preview_btn.Location = New System.Drawing.Point(488, 398)
        Me.preview_btn.Name = "preview_btn"
        Me.preview_btn.Size = New System.Drawing.Size(92, 36)
        Me.preview_btn.TabIndex = 34
        Me.preview_btn.Text = "Preview a Saved File"
        Me.preview_btn.UseVisualStyleBackColor = True
        Me.preview_btn.Visible = False
        '
        'main_menustrip
        '
        Me.main_menustrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.main_menustrip.Location = New System.Drawing.Point(0, 0)
        Me.main_menustrip.Name = "main_menustrip"
        Me.main_menustrip.Size = New System.Drawing.Size(703, 24)
        Me.main_menustrip.TabIndex = 35
        Me.main_menustrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadSavedFileToolStripMenuItem, Me.SaveCurrentTreeToolStripMenuItem, Me.ExportGameFileToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'LoadSavedFileToolStripMenuItem
        '
        Me.LoadSavedFileToolStripMenuItem.Name = "LoadSavedFileToolStripMenuItem"
        Me.LoadSavedFileToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.LoadSavedFileToolStripMenuItem.Text = "Load Saved File"
        '
        'SaveCurrentTreeToolStripMenuItem
        '
        Me.SaveCurrentTreeToolStripMenuItem.Name = "SaveCurrentTreeToolStripMenuItem"
        Me.SaveCurrentTreeToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.SaveCurrentTreeToolStripMenuItem.Text = "Save Current Popmenu"
        '
        'ExportGameFileToolStripMenuItem
        '
        Me.ExportGameFileToolStripMenuItem.Name = "ExportGameFileToolStripMenuItem"
        Me.ExportGameFileToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ExportGameFileToolStripMenuItem.Text = "Export Game File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewHelpToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ViewHelpToolStripMenuItem
        '
        Me.ViewHelpToolStripMenuItem.Name = "ViewHelpToolStripMenuItem"
        Me.ViewHelpToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.ViewHelpToolStripMenuItem.Text = "View Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'move_up_btn
        '
        Me.move_up_btn.Location = New System.Drawing.Point(391, 526)
        Me.move_up_btn.Name = "move_up_btn"
        Me.move_up_btn.Size = New System.Drawing.Size(90, 33)
        Me.move_up_btn.TabIndex = 36
        Me.move_up_btn.Text = "Move Up"
        Me.move_up_btn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 578)
        Me.Controls.Add(Me.move_up_btn)
        Me.Controls.Add(Me.main_menustrip)
        Me.Controls.Add(Me.preview_btn)
        Me.Controls.Add(Me.add_menu_item_btn)
        Me.Controls.Add(Me.badge_reference_label)
        Me.Controls.Add(Me.delete_all_btn)
        Me.Controls.Add(Me.title_radio)
        Me.Controls.Add(Me.divider_button)
        Me.Controls.Add(Me.version_num_txtbox)
        Me.Controls.Add(Me.version_num_lbl)
        Me.Controls.Add(Me.written_by_txtbox)
        Me.Controls.Add(Me.written_by_lbl)
        Me.Controls.Add(Me.item_radio_label)
        Me.Controls.Add(Me.cmd_cntrl_btn)
        Me.Controls.Add(Me.exit_btn)
        Me.Controls.Add(Me.export_game_btn)
        Me.Controls.Add(Me.load_btn)
        Me.Controls.Add(Me.save_btn)
        Me.Controls.Add(Me.menu_name_txtbox)
        Me.Controls.Add(Me.menu_name_lbl)
        Me.Controls.Add(Me.badges_cmbbx)
        Me.Controls.Add(Me.badges_cmbbx_lbl)
        Me.Controls.Add(Me.commandtxtbox)
        Me.Controls.Add(Me.command_label)
        Me.Controls.Add(Me.badge_radio_btn)
        Me.Controls.Add(Me.cmd_radio)
        Me.Controls.Add(Me.menu_radio_btn)
        Me.Controls.Add(Me.info_radio_btn)
        Me.Controls.Add(Me.remove_node_btn)
        Me.Controls.Add(Me.display_txtbox)
        Me.Controls.Add(Me.display_lbl)
        Me.Controls.Add(Me.poptree)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.main_menustrip
        Me.Name = "Form1"
        Me.Text = "Popmenu Creator v1"
        Me.poptree_right_clk.ResumeLayout(False)
        Me.main_menustrip.ResumeLayout(False)
        Me.main_menustrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents poptree As System.Windows.Forms.TreeView
    Friend WithEvents display_lbl As System.Windows.Forms.Label
    Friend WithEvents display_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents remove_node_btn As System.Windows.Forms.Button
    Friend WithEvents info_radio_btn As System.Windows.Forms.RadioButton
    Friend WithEvents menu_radio_btn As System.Windows.Forms.RadioButton
    Friend WithEvents cmd_radio As System.Windows.Forms.RadioButton
    Friend WithEvents badge_radio_btn As System.Windows.Forms.RadioButton
    Friend WithEvents command_label As System.Windows.Forms.Label
    Friend WithEvents commandtxtbox As System.Windows.Forms.TextBox
    Friend WithEvents badges_cmbbx_lbl As System.Windows.Forms.Label
    Friend WithEvents badges_cmbbx As System.Windows.Forms.ComboBox
    Friend WithEvents menu_name_lbl As System.Windows.Forms.Label
    Friend WithEvents menu_name_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents save_btn As System.Windows.Forms.Button
    Friend WithEvents load_btn As System.Windows.Forms.Button
    Friend WithEvents export_game_btn As System.Windows.Forms.Button
    Friend WithEvents exit_btn As System.Windows.Forms.Button
    Friend WithEvents cmd_cntrl_btn As System.Windows.Forms.Button
    Friend WithEvents item_radio_label As System.Windows.Forms.Label
    Friend WithEvents written_by_lbl As System.Windows.Forms.Label
    Friend WithEvents written_by_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents version_num_lbl As System.Windows.Forms.Label
    Friend WithEvents version_num_txtbox As System.Windows.Forms.TextBox
    Friend WithEvents divider_button As System.Windows.Forms.Button
    Friend WithEvents title_radio As System.Windows.Forms.RadioButton
    Friend WithEvents delete_all_btn As System.Windows.Forms.Button
    Friend WithEvents badge_reference_label As System.Windows.Forms.Label
    Friend WithEvents add_menu_item_btn As System.Windows.Forms.Button
    Friend WithEvents preview_btn As System.Windows.Forms.Button
    Friend WithEvents poptree_right_clk As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents main_menustrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadSavedFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveCurrentTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportGameFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents move_up_btn As System.Windows.Forms.Button

End Class
