Public Class edit_item
    Private Sub reset_all()
        edit_display_txtbx.Text = ""
        edit_type_title.Checked = False
        edit_type_command.Checked = False
        edit_type_badge.Checked = False
        edit_type_info.Checked = False
        edit_type_menu.Checked = False

        edit_badge_command.Visible = False
        edit_command_box.Visible = False

        edit_command_txt_box.Text = ""
        edit_command_txt_box.Visible = False

        edit_badge_dropdown.Visible = False
        edit_badge_cmbbx.SelectedIndex = -1
        edit_badge_cmbbx.Visible = False
    End Sub
    Private Sub radio_clear()
        edit_badge_command.Visible = False
        edit_command_box.Visible = False
        edit_command_txt_box.Visible = False

        edit_badge_dropdown.Visible = False
        edit_badge_cmbbx.SelectedIndex = -1
        edit_badge_cmbbx.Visible = False


    End Sub

    Private Sub edit_save_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_save_button.Click
        Form1.poptree.SelectedNode.Text = edit_display_txtbx.Text

        Select Case item_type
            Case "Option"
                cmd_item = "-Type Option"
                treetag = cmd_item
            Case "Menu"
                cmd_item = "-Type Menu"
                treetag = cmd_item
            Case "Command"
                cmd_item = "-Type Command -Command_Text " + edit_command_txt_box.Text
                treetag = cmd_item
            Case "Badge"
                cmd_item = "-Type Badge -Command_Text " + edit_command_txt_box.Text
                treetag = cmd_item
            Case "Title"
                cmd_item = "-Type Title"
                treetag = cmd_item
            Case Else
        End Select

        Form1.poptree.SelectedNode.Tag = treetag
        Form1.poptree.SelectedNode.ExpandAll()
        Form1.poptree.SelectedNode = Nothing


        reset_all()
        Me.Hide()

    End Sub

    Private Sub edit_exit_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_exit_btn.Click
        reset_all()
        Me.Hide()
    End Sub

    Private Sub edit_badge_cmbbx_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_badge_cmbbx.SelectedIndexChanged
        Dim dictkey As String = edit_badge_cmbbx.SelectedItem

        If Not dictkey Is Nothing Then
            If badge_dictionary.ContainsKey(dictkey) Then
                Dim dictvalue As String = badge_dictionary(dictkey)
                edit_command_txt_box.Text = dictvalue
            End If
        End If
    End Sub

    Private Sub edit_type_title_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_type_title.CheckedChanged
        radio_clear()
        item_type = "Title"
        edit_display_txtbx.Visible = True
        edit_display.Visible = True
    End Sub

    Private Sub edit_type_info_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_type_info.CheckedChanged
        radio_clear()
        item_type = "Option"
        edit_display_txtbx.Visible = True
        edit_display.Visible = True
    End Sub

    Private Sub edit_type_command_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_type_command.CheckedChanged
        radio_clear()
        item_type = "Command"
        edit_command_txt_box.Visible = True
        edit_command_box.Visible = True
    End Sub

    Private Sub edit_type_menu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_type_menu.CheckedChanged
        radio_clear()
        item_type = "Menu"
        edit_display_txtbx.Visible = True
        edit_display.Visible = True
    End Sub

    Private Sub edit_type_badge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edit_type_badge.CheckedChanged
        radio_clear()
        item_type = "Badge"
        edit_display_txtbx.Visible = True
        edit_display.Visible = True
        edit_badge_dropdown.Visible = True
        edit_badge_cmbbx.Visible = True
        edit_command_txt_box.Visible = True
        edit_badge_command.Visible = True
        edit_command_txt_box.Enabled = False
        edit_command_txt_box.Text = ""
    End Sub
End Class