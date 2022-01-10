Imports System.Xml

Public Class Form1

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles remove_node_btn.Click
        'check for node selected, if selected remove menu node
        If chosennode = "" Then
            MsgBox("You didn't select a node to remove.")
            chosennode = ""
        Else
            poptree.SelectedNode.Remove()
            poptree.SelectedNode = Nothing
            chosennode = ""
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'initial load
        intial_directory()
        hide_all()
        show_tooltips()
        combo_load()

    End Sub

    Private Sub hide_all()
        'hide labels and boxes to reset to default
        display_lbl.Visible = False
        display_txtbox.Visible = False
        command_label.Visible = False
        commandtxtbox.Visible = False
        badges_cmbbx_lbl.Visible = False
        badges_cmbbx.Visible = False
        export_game_btn.Visible = True
        ' Button4.Visible = False
        cmd_cntrl_btn.Visible = False
        badge_reference_label.Visible = False

    End Sub
    Private Sub clear_all()
        'clear all boxes to reset to default
        title_radio.Checked = False
        menu_radio_btn.Checked = False
        cmd_radio.Checked = False
        info_radio_btn.Checked = False
        badge_radio_btn.Checked = False
        menu_name_txtbox.Text = ""
        written_by_txtbox.Text = ""
        version_num_txtbox.Text = ""
        commandtxtbox.Text = ""
    End Sub


    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles info_radio_btn.CheckedChanged
        'display info options
        hide_all()
        display_lbl.Visible = True
        display_txtbox.Visible = True


        item_type = "Option"
        commandtxtbox.Text = ""
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_radio_btn.CheckedChanged
        'display menu options
        hide_all()
        display_lbl.Visible = True
        display_txtbox.Visible = True


        item_type = "Menu"
        commandtxtbox.Text = ""
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_radio.CheckedChanged
        'display command options
        hide_all()
        display_lbl.Visible = True
        badge_reference_label.Visible = True
        display_txtbox.Visible = True
        command_label.Visible = True
        commandtxtbox.Visible = True
        commandtxtbox.Enabled = True
        commandtxtbox.Text = ""
        badge_reference_label.Visible = False
        item_type = "Command"

    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles badge_radio_btn.CheckedChanged
        'diplay badge options
        hide_all()
        display_lbl.Visible = True
        command_label.Visible = False
        display_txtbox.Visible = True
        badges_cmbbx_lbl.Visible = True
        badges_cmbbx.Visible = True
        commandtxtbox.Visible = True
        commandtxtbox.Enabled = False
        badges_cmbbx.SelectedIndex = -1
        badge_reference_label.Visible = True
        item_type = "Badge"

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save_btn.Click
        'save menu as an xml file to load for later editing
        Dim filesaver As New SaveFileDialog
        filesaver.DefaultExt = "xml"
        filesaver.Filter = "xml files|*.xml"
        filesaver.OverwritePrompt = True
        filesaver.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory() + "save_files"
        filesaver.ShowDialog()

        If filesaver.FileName <> "" Then
            Dim file As System.IO.FileStream
            file = System.IO.File.Create(filesaver.FileName)
            file.Close()

            Dim textWriter = New XmlTextWriter(filesaver.FileName, System.Text.Encoding.UTF8)
            textWriter.WriteStartDocument()
            Dim menuname As String
            menuname = menu_name_txtbox.Text
            textWriter.WriteStartElement("Root")
            textWriter.WriteAttributeString("menu_name", menu_name_txtbox.Text)
            textWriter.WriteAttributeString("written_By", written_by_txtbox.Text)
            textWriter.WriteAttributeString("version_number", version_num_txtbox.Text)

            textWriter.WriteStartElement("TopLevelMenu")
            textWriter.WriteAttributeString("id", menuname)

            Dim nodes As TreeNodeCollection
            nodes = poptree.Nodes
            Dim root As TreeNode
            For Each root In nodes

                textWriter.WriteStartElement("RootItem")
                textWriter.WriteAttributeString("id", root.Text)
                'textWriter.WriteAttributeString("Type", item_type)
                textWriter.WriteAttributeString("Parameters", root.Tag.ToString)
                textWriter.WriteAttributeString("Root_Order", root.Index)
                textWriter.WriteAttributeString("node_level", root.Level.ToString)
                textWriter.WriteAttributeString("Path", root.FullPath)

                If root.Nodes.Count > 0 Then
                    Dim level1 As TreeNodeCollection
                    level1 = root.Nodes
                    Dim child As TreeNode

                    For Each child In level1
                        textWriter.WriteStartElement("FirstLevelItem")
                        textWriter.WriteAttributeString("id", child.Text)
                        'textWriter.WriteAttributeString("Type", item_type)
                        textWriter.WriteAttributeString("Parameters", child.Tag.ToString)
                        'textWriter.WriteAttributeString("Parent_Node", root.Text)
                        textWriter.WriteAttributeString("Parent_Node", child.Parent.Text)
                        textWriter.WriteAttributeString("child_Order", child.Index)
                        textWriter.WriteAttributeString("node_level", child.Level.ToString)
                        textWriter.WriteAttributeString("Path", child.FullPath)


                        If child.Nodes.Count > 0 Then
                            Dim level2 As TreeNodeCollection
                            level2 = child.Nodes
                            Dim child2 As TreeNode
                            For Each child2 In level2
                                textWriter.WriteStartElement("SecondLevelItem")
                                textWriter.WriteAttributeString("id", child2.Text)
                                'textWriter.WriteAttributeString("Type", item_type)
                                textWriter.WriteAttributeString("Parameters", child2.Tag.ToString)
                                textWriter.WriteAttributeString("Parent_Node", child2.Parent.Text)
                                textWriter.WriteAttributeString("child_Order", child2.Index)
                                textWriter.WriteAttributeString("node_level", child2.Level.ToString)
                                textWriter.WriteAttributeString("Path", child2.FullPath)



                                'end second level loop

                                If child.Nodes.Count > 0 Then
                                    Dim level3 As TreeNodeCollection
                                    level3 = child2.Nodes
                                    Dim child3 As TreeNode
                                    For Each child3 In level3
                                        textWriter.WriteStartElement("ThirdLevelItem")
                                        textWriter.WriteAttributeString("id", child3.Text)
                                        'textWriter.WriteAttributeString("Type", item_type)
                                        textWriter.WriteAttributeString("Parameters", child3.Tag.ToString)
                                        textWriter.WriteAttributeString("Parent_Node", child3.Parent.Text)
                                        textWriter.WriteAttributeString("child_Order", child3.Index)
                                        textWriter.WriteAttributeString("node_level", child3.Level.ToString)
                                        textWriter.WriteAttributeString("Path", child3.FullPath)



                                        If child.Nodes.Count > 0 Then
                                            Dim level4 As TreeNodeCollection
                                            level4 = child3.Nodes
                                            Dim child4 As TreeNode
                                            For Each child4 In level4
                                                textWriter.WriteStartElement("FourthLevelItem")
                                                textWriter.WriteAttributeString("id", child4.Text)
                                                'textWriter.WriteAttributeString("Type", item_type)
                                                textWriter.WriteAttributeString("Parameters", child4.Tag.ToString)
                                                textWriter.WriteAttributeString("Parent_Node", child4.Parent.Text)
                                                textWriter.WriteAttributeString("child_Order", child4.Index)
                                                textWriter.WriteAttributeString("node_level", child4.Level.ToString)
                                                textWriter.WriteAttributeString("Path", child4.FullPath)



                                                If child.Nodes.Count > 0 Then
                                                    Dim level5 As TreeNodeCollection
                                                    level5 = child4.Nodes
                                                    Dim child5 As TreeNode
                                                    For Each child5 In level5
                                                        textWriter.WriteStartElement("FifthLevelItem")
                                                        textWriter.WriteAttributeString("id", child5.Text)
                                                        'textWriter.WriteAttributeString("Type", item_type)
                                                        textWriter.WriteAttributeString("Parameters", child5.Tag.ToString)
                                                        textWriter.WriteAttributeString("Parent_Node", child5.Parent.Text)
                                                        textWriter.WriteAttributeString("child_Order", child5.Index)
                                                        textWriter.WriteAttributeString("node_level", child5.Level.ToString)
                                                        textWriter.WriteAttributeString("Path", child5.FullPath)

                                                        textWriter.WriteEndElement()

                                                    Next
                                                End If

                                                textWriter.WriteEndElement()
                                            Next
                                        End If
                                        textWriter.WriteEndElement()

                                    Next
                                End If
                                'end third level loop

                                textWriter.WriteEndElement()
                            Next
                            'end second level child addition
                        End If
                        textWriter.WriteEndElement()
                        'end 1st level loop
                    Next

                    'end 1st level child addition
                End If

                textWriter.WriteEndElement()
                'end root loop
            Next


            'SaveNodes(TreeView.Nodes, textWriter)

            textWriter.WriteEndElement()
            textWriter.WriteEndElement()
            textWriter.Close()
            'end filesaver if

        End If

    End Sub

    Private Sub poptree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles poptree.AfterSelect
        'populate text boxes when node is selected
        chosennode = poptree.SelectedNode.Text
        'display_txtbox.Text = poptree.SelectedNode.Text
        nodelevel = poptree.SelectedNode.Level.ToString
        nodeinfo = poptree.SelectedNode.Tag.ToString

    End Sub

    Private Sub exit_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exit_btn.Click
        'exit program
        badge_dictionary.Clear()
        End
    End Sub

    Private Sub cmd_cntrl_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cntrl_btn.Click
        'unfinished bind creator
        Command_Control.Show()
    End Sub
    Private Sub show_tooltips()
        'tool tips to show on mouse over
        Dim TT As New ToolTip

        TT.ShowAlways = True
        TT.AutoPopDelay = 30000

        'Main Form TT
        TT.SetToolTip(Me.menu_name_lbl, "This is the name COH will use to load the menu, one word is advisable, or use _ instead of spaces.")
        TT.SetToolTip(Me.item_radio_label, "These are the different types of Items the menu will accept.")

        TT.SetToolTip(Me.display_lbl, "This is just a line of text that the menu will show in game." + vbCrLf + "Hint, if you want it to hotkey to the keyboard put a & in front of the letter/number.")




    End Sub

    Private Sub intial_directory()
        'create initial directories and dat files to be used later by bind creator dialogue
        Dim file As System.IO.FileStream

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "save_files") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "save_files")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "export_files") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "export_files")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary")

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "soldier.dat")
            file.Close()
            Dim soldierdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "soldier.dat"
            My.Computer.FileSystem.WriteAllText(soldierdat, "Single Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Pummel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Wide Area Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Heavy Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Bayonet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Venom Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(soldierdat, "Frag Grenade" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "bane.dat")
            file.Close()
            Dim banedat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "bane.dat"
            My.Computer.FileSystem.WriteAllText(banedat, "Bash" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Mace Beam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Mace Beam Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Mace Beam Volley" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Pulverize" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Poisonous Ray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Shatter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Placate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(banedat, "Crowd Control" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "crab.dat")
            file.Close()
            Dim crabdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Primary\" + "crab.dat"
            My.Computer.FileSystem.WriteAllText(crabdat, "Channelgun" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Slice" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Longfang" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Suppression" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Arm Lash" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Venom Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Frag Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Frenzy" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(crabdat, "Omega Maneuver" + vbCrLf, True)

            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Arachnos_Soldiers\Secondary")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary")

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Archery.dat")
            file.Close()
            Dim archerydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Archery.dat"
            My.Computer.FileSystem.WriteAllText(archerydat, "Snap Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Aimed Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Fistful of Arrows" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Blazing Arrow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Explosive Arrow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Ranged Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Stunning Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(archerydat, "Rain of Arrows" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Assault_Rifle.dat")
            file.Close()
            Dim assault_rifledat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Assault_Rifle.dat"
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Tranquilizer Dart" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Buckshot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "M30 Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Beanbag" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Sniper Rifle" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Flamethrower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Ignite" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(assault_rifledat, "Full Auto" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Beam_Rifle.dat")
            file.Close()
            Dim beam_rifledat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Beam_Rifle.dat"
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Single Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Charged Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Cutting Beam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Disintegrate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Lancer Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Penetrating Ray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Piercing Beam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(beam_rifledat, "Overcharge" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Dark_Blast.dat")
            file.Close()
            Dim dark_blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Dark_Blast.dat"
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Dark Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Gloom" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Umbral Torrent" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Moonbeam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Tenebrous Tentacles" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Abyssal Gaze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Life Drain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dark_blastdat, "Blackstar" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Dual_Pistols.dat")
            file.Close()
            Dim dual_pistolsdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Dual_Pistols.dat"
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Pistols" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Dual Wield" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Empty Clips" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Swap Ammo" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Cryo Ammunition" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Incendiary Ammunition" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Chemical Ammunition" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Bullet Rain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Suppressive Fire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Executioners Shot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Piercing Rounds" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(dual_pistolsdat, "Hail of Bullets" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Electrical_Blast.dat")
            file.Close()
            Dim Electrical_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Electrical_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Charged Bolts" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Lightning Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Ball Lightning" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Short Circuit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Zapp" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Tesla Cage" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Voltaic Sentinel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Blastdat, "Thunderous Blast" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Energy_Blast.dat")
            file.Close()
            Dim Energy_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Energy_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Power Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Power Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Energy Torrent" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Power Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Sniper Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Power Push" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Explosive Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Blastdat, "Nova" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Fire_Blast.dat")
            file.Close()
            Dim Fire_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Fire_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Flares" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Fire Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Fire Ball" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Rain of Fire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Fire Breath" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Blaze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Blazing Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Blastdat, "Inferno" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Ice_Blast.dat")
            file.Close()
            Dim Ice_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Ice_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Ice Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Ice Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Frost Breath" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Freeze Ray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Freezing Rain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Bitter Ice Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Bitter Freeze Ray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Blastdat, "Blizzard" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Psychic_Blast.dat")
            file.Close()
            Dim Psychic_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Psychic_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Psionic Dart" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Mental Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Telekinetic Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Will Domination" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Psionic Lance" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Psionic Tornado" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Scramble Thoughts" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Psychic_Blastdat, "Psychic Wail" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Radiation_Blast.dat")
            file.Close()
            Dim Radiation_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Radiation_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Neutrino Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "X-Ray Beam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Irradiate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Electron Haze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Aim" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Proton Volley" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Cosmic Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Neutron Bomb" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Radiation_Blastdat, "Atomic Blast" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Sonic_Attack.dat")
            file.Close()
            Dim Sonic_Attackdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Sonic_Attack.dat"
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Shriek" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Scream" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Howl" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Shockwave" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Shout" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Amplify" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Sirens Song" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Screech" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Sonic_Attackdat, "Dreadful Wail" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Water_Blast.dat")
            file.Close()
            Dim Water_Blastdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Primary\" + "Water_Blast.dat"
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Aqua Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Hydro Blast" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Water Burst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Whirlpool" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Tidal Forces" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Dehydrate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Water Jet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Steam Spray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Water_Blastdat, "Geyser" + vbCrLf, True)

            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary")

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Darkness_Manipulation.dat")
            file.Close()
            Dim Darkness_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Darkness_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Penumbral Grasp" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Smite" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Death Shroud" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Shadow Maul" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Soul Drain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Touch of Fear" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Dark Consumption" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Dark Pit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Darkness_Manipulationdat, "Midnight Grasp" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Electricity_Manipulation.dat")
            file.Close()
            Dim Electricity_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Electricity_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Electric Fence" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Charged Brawl" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Lightning Field" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Havok Punch" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Lightning Clap" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Thunder Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Power Sink" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electricity_Manipulationdat, "Shocking Grasp" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Energy_Manipulation.dat")
            file.Close()
            Dim Energy_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Energy_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Power Thrust" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Energy Punch" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Bone Smasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Conserve Power" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Stun" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Power Boost" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Boost Range" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Energy_Manipulationdat, "Total Focus" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Fire_Manipulation.dat")
            file.Close()
            Dim Fire_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Fire_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Ring of Fire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Fire Sword" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Combustion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Fire Sword Circle" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Blazing Aura" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Consume" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Burn" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Fire_Manipulationdat, "Hot Feet" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Gadgets.dat")
            file.Close()
            Dim Gadgetsdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Gadgets.dat"
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Web Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Caltrops" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Taser" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Targeting Drone" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Smoke Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Cloaking Device" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Trip Mine" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Time Bomb" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Gadgetsdat, "Auto Turret" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Ice_Manipulation.dat")
            file.Close()
            Dim Ice_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Ice_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Chilblain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Frozen Fists" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Ice Sword" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Chilling Embrace" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Ice Patch" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Shiver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Freezing Touch" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Ice_Manipulationdat, "Frozen Aura" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Martial_Manipulation.dat")
            file.Close()
            Dim Martial_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Martial_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Ki Push" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Storm Kick" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Reach for the Limit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Build Up Proc" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Burst of Speed" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Dragons Tail" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Reaction Time" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Inner Will" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Throw Sand" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Martial_Manipulationdat, "Eagles Claw" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Mental_Manipulation.dat")
            file.Close()
            Dim Mental_Manipulationdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Secondary\" + "Mental_Manipulation.dat"
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Subdual" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Mind Probe" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Telekinetic Thrust" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Psychic Scream" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Drain Psyche" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "World of Confusion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Scare" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Mental_Manipulationdat, "Psychic Shockwave" + vbCrLf, True)

            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic")

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Mace_Mastery.dat")
            file.Close()
            Dim Blaster_Mace_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Mace_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Blaster_Mace_Masterydat, "Web Envelope" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mace_Masterydat, "Scorpion Shield" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mace_Masterydat, "Mace Beam Volley" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mace_Masterydat, "Power Boost" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mace_Masterydat, "Web Cocoon" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Cold_Mastery.dat")
            file.Close()
            Dim Cold_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Cold_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Cold_Masterydat, "Snow Storm" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Cold_Masterydat, "Flash Freeze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Cold_Masterydat, "Hoarfrost" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Cold_Masterydat, "Frozen Armor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Cold_Masterydat, "Hibernate" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Electrical_Mastery.dat")
            file.Close()
            Dim Electrical_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Electrical_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Electrical_Masterydat, "Static Discharge" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Masterydat, "Shocking Bolt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Masterydat, "Charged Armor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Masterydat, "Surge of Power" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Electrical_Masterydat, "EM Pulse" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Flame_Mastery.dat")
            file.Close()
            Dim Flame_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Flame_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Flame_Masterydat, "Bonfire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Flame_Masterydat, "Char" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Flame_Masterydat, "Fire Shield" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Flame_Masterydat, "Melt Armor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Flame_Masterydat, "Rise of the Phoenix" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Force_Mastery.dat")
            file.Close()
            Dim Force_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Force_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Force_Masterydat, "Personal Force Field" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Force_Masterydat, "Repulsion Field" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Force_Masterydat, "Temp Invulnerability" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Force_Masterydat, "Repulsion Bomb" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Force_Masterydat, "Force of Nature" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Leviathan_Mastery.dat")
            file.Close()
            Dim Blaster_Leviathan_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Leviathan_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Blaster_Leviathan_Masterydat, "School of Sharks" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Leviathan_Masterydat, "Bile Spray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Leviathan_Masterydat, "Knockout Blow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Leviathan_Masterydat, "Shark Skin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Leviathan_Masterydat, "Spirit Shark Jaws" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Mu_Mastery.dat")
            file.Close()
            Dim Blaster_Mu_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Mu_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Blaster_Mu_Masterydat, "Static Discharge" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mu_Masterydat, "Charged Armor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mu_Masterydat, "Thunder Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mu_Masterydat, "Electrifying Fences" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_Mu_Masterydat, "Electric Shackles" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Munitions_Mastery.dat")
            file.Close()
            Dim Munitions_Masterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Munitions_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Munitions_Masterydat, "Body Armor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Munitions_Masterydat, "Cryo Freeze Ray" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Munitions_Masterydat, "Sleep Grenade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Munitions_Masterydat, "Surveillance" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Munitions_Masterydat, "LRM Rocket" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Soul_Mastery.dat")
            file.Close()
            Dim Blaster_SoulMasterydat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Blaster\Epic\" + "Blaster_Soul_Mastery.dat"
            My.Computer.FileSystem.WriteAllText(Blaster_SoulMasterydat, "Night Fall" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_SoulMasterydat, "Dark Embrace" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_SoulMasterydat, "Oppressive Gloom" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_SoulMasterydat, "Soul Tentacles" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Blaster_SoulMasterydat, "Soul Storm" + vbCrLf, True)
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary")

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Battle_Axe.dat")
            file.Close()
            Dim Battle_Axedat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Battle_Axe.dat"
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Gash" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Chop" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Beheader" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Swoop" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Taunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Whirling Axe" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Cleave" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Battle_Axedat, "Pendulum" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Street_Justice.dat")
            file.Close()
            Dim street_justicedat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Street_Justice.dat"
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Initial Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Heavy Blow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Sweeping Cross" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Combat Readiness" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Throat Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Taunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Spinning Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Low Kick" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(street_justicedat, "Crushing Uppercut" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Broad_Sword.dat")
            file.Close()
            Dim Broad_Sworddat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Broad_Sword.dat"
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Hack" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Slash" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Slice" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Build Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Parry" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Taunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Whirling Sword" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Disembowel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Broad_Sworddat, "Head Splitter" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Claws.dat")
            file.Close()
            Dim Clawsdat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Claws.dat"
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Swipe" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Strike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Slash" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Spin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Follow Up" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Taunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Focus" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Eviscerate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Clawsdat, "Shockwave" + vbCrLf, True)

            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Dark_Melee.dat")
            file.Close()
            Dim Dark_Meleedat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Primary\" + "Dark_Melee.dat"
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Shadow Punch" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Smite" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Shadow Maul" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Touch of Fear" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Siphon Life" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Taunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Dark Consumption" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Soul Drain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(Dark_Meleedat, "Midnight Grasp" + vbCrLf, True)









            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Brute\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Controller") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Controller")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Controller\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Controller\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Controller\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Corruptor") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Corruptor")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Corruptor\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Corruptor\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Corruptor\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Defender") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Defender")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Defender\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Defender\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Defender\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Dominator") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Dominator")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Dominator\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Dominator\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Dominator\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Mastermind") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Mastermind")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Mastermind\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Mastermind\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Mastermind\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Alpha")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Destiny")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Genesis")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Hybrid")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Interface")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Judgement")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Incarnate\Lore")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Inherent") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Inherent")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Peacebringer") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Peacebringer")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Peacebringer\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Peacebringer\Secondary")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Power_Pool") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Power_Pool")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Scrapper") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Scrapper")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Scrapper\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Scrapper\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Scrapper\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Stalker") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Stalker")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Stalker\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Stalker\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Stalker\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Tanker") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Tanker")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Tanker\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Tanker\Secondary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Tanker\Epic")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Temp_Powers") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Temp_Powers")
        End If

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Warshade") = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Warshade")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Warshade\Primary")
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory() + "data\Archtype\Warshade\Secondary")
        End If

        Dim strPathdialogue = System.AppDomain.CurrentDomain.BaseDirectory() + "data\badges.dat"
        If Not IO.File.Exists(strPathdialogue) Then
            file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "data\" + "badges.dat")
            file.Close()


            Dim badgedat = System.AppDomain.CurrentDomain.BaseDirectory() + "data\badges.dat"

            My.Computer.FileSystem.WriteAllText(badgedat, "Undefeated " + "|" + "AtlasParkTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Silent Sentinel " + "|" + "AtlasParkTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hero Corps Insider::Hero Corps Infiltrator " + "|" + "AtlasParkTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Patriot::International Spy " + "|" + "AtlasParkTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Top Dog " + "|" + "AtlasParkTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Freedom::Covert Operator " + "|" + "AtlasParkTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Edge of Chaos " + "|" + "AtlasParkTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Observant " + "|" + "AtlasParkTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bird Watcher " + "|" + "GalaxyCityTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blue Shield " + "|" + "GalaxyCityTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Brawler " + "|" + "GalaxyCityTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tank " + "|" + "GalaxyCityTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Land Locked " + "|" + "GalaxyCityTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Galactic Fan " + "|" + "GalaxyCityTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eye of the Gemini " + "|" + "GalaxyCityTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Orion's Belt " + "|" + "GalaxyCityTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Summoned " + "|" + "KingsRowTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Upgraded " + "|" + "KingsRowTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mystic King/Queen " + "|" + "KingsRowTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Keen Sighted " + "|" + "KingsRowTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Smokey " + "|" + "KingsRowTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wentworth History Buff " + "|" + "KingsRowTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pwned " + "|" + "KingsRowTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Man/Woman of Vengeance " + "|" + "KingsRowTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Secret Admirer " + "|" + "SteelCanyonTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hero Corps Recruit::Hero Corps Reject " + "|" + "SteelCanyonTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nimble Mynx " + "|" + "SteelCanyonTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bright Star " + "|" + "SteelCanyonTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dance Legend " + "|" + "SteelCanyonTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Controversial " + "|" + "SteelCanyonTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pet Project " + "|" + "SteelCanyonTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dirty Attorney " + "|" + "SteelCanyonTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Purifier::Defiler " + "|" + "SkywayCityTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Solace " + "|" + "SkywayCityTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dauntless " + "|" + "SkywayCityTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Healing Node " + "|" + "SkywayCityTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Better Days " + "|" + "SkywayCityTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Room for Expansion " + "|" + "SkywayCityTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Left Behind " + "|" + "SkywayCityTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bridge to Nowhere " + "|" + "SkywayCityTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Undammed " + "|" + "FaultlineTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Newsman/girl " + "|" + "FaultlineTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Faultless Mystic " + "|" + "FaultlineTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Apex " + "|" + "FaultlineTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Escape Artist " + "|" + "FaultlineTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Drowned Rat " + "|" + "FaultlineTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Old Fashioned " + "|" + "FaultlineTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Egg Hunter " + "|" + "FaultlineEaster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Minotaur " + "|" + "TalosIslandTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nature Lover::Eco-warrior " + "|" + "TalosIslandTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Whitecap " + "|" + "TalosIslandTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Last Line of Defense " + "|" + "TalosIslandTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Old Me " + "|" + "TalosIslandTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bridge Holder " + "|" + "TalosIslandTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Overtime Worker " + "|" + "TalosIslandTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spanky's Competitor " + "|" + "TalosIslandTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Havoc " + "|" + "IndependancePortTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Valorous " + "|" + "IndependancePortTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vigorous " + "|" + "IndependancePortTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Air Lifter " + "|" + "IndependencePortTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dead End " + "|" + "IndependencePortTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Family Partier " + "|" + "IndependencePortTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Connector " + "|" + "IndependencePortTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unlucky " + "|" + "IndependencePortTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grim Wanderer " + "|" + "CroatoaTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spiritual " + "|" + "CroatoaTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ensorcelled " + "|" + "CroatoaTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Jack's Wrath " + "|" + "CroatoaTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Barrier Holder " + "|" + "CroatoaTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sally Sightseer " + "|" + "CroatoaTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Waylon's Observer " + "|" + "CroatoaTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnighter's Perseverance " + "|" + "CroatoaTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mystic " + "|" + "BrickstownTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Inmate " + "|" + "BrickstownTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Flying Shark " + "|" + "BrickstownTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fugitive " + "|" + "BrickstownTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Forward Thinker " + "|" + "BrickstownTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Secret Path " + "|" + "BrickstownTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sixth Passenger " + "|" + "BrickstownTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unsubtle " + "|" + "BrickstownTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Foggy " + "|" + "FoundersFallsTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chaotician " + "|" + "FoundersFallsTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guardian Angel::Barely Contained " + "|" + "FoundersFallsTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Losing Paradise " + "|" + "FoundersFallsTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Watchful Eyes " + "|" + "FoundersFallsTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hamidon's Fury " + "|" + "FoundersFallsTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Founders' Hero::Founders' Loss " + "|" + "FoundersFallsTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Misunderstood " + "|" + "FoundersFallsTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Portal Parter " + "|" + "PeregrineIslandTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Use Might for Right::Rookie's Mistake " + "|" + "PeregrineIslandTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cause for Concern " + "|" + "PeregrineIslandTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Genetically Altered " + "|" + "PeregrineIslandTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rikti Gone Wild " + "|" + "PeregrineIslandTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shades of Arachnos " + "|" + "PeregrineIslandTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "All-Seeing " + "|" + "PeregrineIslandTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dark Omen " + "|" + "PeregrineIslandTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spirit of the City " + "|" + "SewerNetworkTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Plutonian " + "|" + "SewerNetworkTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Baumton Avenger " + "|" + "SewerNetworkTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sewer Queen/King " + "|" + "SewerNetworkTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sewer Stalker " + "|" + "SewerNetworkTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Downward Bound " + "|" + "SewerNetworkTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Terror of the Vahzilok " + "|" + "SewerNetworkTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seeker of the Lost " + "|" + "SewerNetworkTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Head of the Hydra " + "|" + "AbandonedSewersTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Boomtown Refugee " + "|" + "AbSewerNetworkTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hades Aspirant " + "|" + "AbSewerNetworkTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Secret of the City " + "|" + "AbSewerNetworkTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sprawl Survivor " + "|" + "AbSewerNetworkTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Underlord/lady " + "|" + "AbSewerNetworkTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "From Beneath You " + "|" + "AbSewerNetworkTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Living Dark " + "|" + "AbSewerNetworkTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Persephone Supplicant " + "|" + "AbSewerNetworkTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Parapsychologist " + "|" + "HollowsTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seeker of Monsters " + "|" + "HollowsTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Geologist " + "|" + "HollowsTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Backwoodsman " + "|" + "HollowsTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gangland Fury " + "|" + "HollowsTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Graffiti Communicator " + "|" + "HollowsTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Circle Seeker " + "|" + "HollowsTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ironic " + "|" + "HollowsTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Territorial " + "|" + "PerezParkTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Avatar " + "|" + "PerezParkTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Around the Bendis " + "|" + "PerezParkTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Doc Whedon " + "|" + "PerezParkTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Justice Avenger::Social Climber " + "|" + "PerezParkTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nebula's Memory " + "|" + "PerezParkTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blood Moss " + "|" + "PerezParkTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ophelia's Final Scene " + "|" + "PerezParkTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Phalanxer " + "|" + "BoomtownTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Regal " + "|" + "BoomtownTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vision of Despair " + "|" + "BoomtownTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Destined for Valhalla " + "|" + "BoomtownTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Towering Inferno " + "|" + "BoomtownTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Boomtown Troglodyte " + "|" + "BoomtownTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "End of the Line " + "|" + "BoomtownTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Corpse Box " + "|" + "BoomtownTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Super Spy " + "|" + "StrigaTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sea Dog " + "|" + "StrigaTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vulcanologist " + "|" + "StrigaTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Wolf's Snarl " + "|" + "StrigaTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Words of the Warrior " + "|" + "StrigaTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Unnamed " + "|" + "StrigaTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Wolf's Maw " + "|" + "StrigaTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vampyri Watcher " + "|" + "StrigaTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Conjunction Junction " + "|" + "TerraVoltaTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Meltdown " + "|" + "TerraVoltaTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nervous Dreck " + "|" + "TerraVoltaTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guardian of the Volts " + "|" + "TerraVoltaTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Danger! Danger! " + "|" + "TerraVoltaTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scrapheap of History " + "|" + "TerraVoltaTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Power Walker " + "|" + "TerraVoltaTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "High Voltage! " + "|" + "TerraVoltaTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dark Mystic " + "|" + "DarkAstoriaTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seeker of the Unknown " + "|" + "DarkAstoriaTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cairn Warder " + "|" + "DarkAstoriaTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Whisperer on Witchburn Hill " + "|" + "DarkAstoriaTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Phantom Radio " + "|" + "DarkAstoriaTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Too Dark Park " + "|" + "DarkAstoriaTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Astoria's Last Stand " + "|" + "DarkAstoriaTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Sleeper Below " + "|" + "DarkAstoriaTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Watcher " + "|" + "CreysFolleyTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Burning the Midnight Oil " + "|" + "CreysFolleyTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eyes of Nemesis " + "|" + "CreysFolleyTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hammer of the Rikti " + "|" + "CreysFolleyTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Veni, Vidi, Vici " + "|" + "CreysFolleyTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Cares " + "|" + "CreysFolleyTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Freak's Folly " + "|" + "CreysFolleyTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cirque du'Freak " + "|" + "CreysFolleyTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Fish " + "|" + "EdenTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unspoiled " + "|" + "EdenTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Call of Nature " + "|" + "EdenTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nature's Wrath " + "|" + "EdenTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Natural Law " + "|" + "EdenTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Devouring Earth Abides " + "|" + "EdenTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hive Mind " + "|" + "EdenTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Natural Selection " + "|" + "EdenTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Time Bandit " + "|" + "TheHiveTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heart of the Hamidon " + "|" + "TheHiveTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Babe in the Woods " + "|" + "TheHiveTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Wounded Earth " + "|" + "TheHiveTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wild At Heart " + "|" + "TheHiveTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Among the Giants " + "|" + "TheHiveTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Sound of Thunder " + "|" + "TheHiveTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Forest of Stone " + "|" + "TheHiveTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Defying Gravity " + "|" + "FirebaseZuluTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dimensional Sojourner " + "|" + "FirebaseZuluTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stormwatcher " + "|" + "FirebaseZuluTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alpha Ranger " + "|" + "FirebaseZuluTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shadow Architect " + "|" + "FirebaseZuluTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "X-Ray Spectator " + "|" + "FirebaseZuluTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dancer with Death " + "|" + "FirebaseZuluTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shard Leaper " + "|" + "FirebaseZuluTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Castaway " + "|" + "CascadeArchTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Peace Walker " + "|" + "CascadeArchTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Being and Nothingness " + "|" + "CascadeArchTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hearing Voices " + "|" + "CascadeArchTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Liquid Memory " + "|" + "CascadeArchTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Usurper of Worlds " + "|" + "CascadeArchTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "No Turning Back Now " + "|" + "CascadeArchTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Heart of Memory " + "|" + "CascadeArchTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "King/Queen of Pain " + "|" + "ChantryTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unfettered " + "|" + "ChantryTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Penitent " + "|" + "ChantryTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Misbegotten " + "|" + "ChantryTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bereaved " + "|" + "ChantryTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dispossessed " + "|" + "ChantryTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Red Fog " + "|" + "ChantryTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Demiurge " + "|" + "ChantryTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Courting Madness " + "|" + "StormPalaceTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Acolyte of Anger " + "|" + "StormPalaceTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Malice Aforethought " + "|" + "StormPalaceTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tormented " + "|" + "StormPalaceTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hate Machine " + "|" + "StormPalaceTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fist of Fury " + "|" + "StormPalaceTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eve of Destruction " + "|" + "StormPalaceTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lord/Lady of Storms " + "|" + "StormPalaceTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Jail Bird " + "|" + "TutorialTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cesspool " + "|" + "MercyIslandTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Snake Charmer " + "|" + "MercyIslandTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chum " + "|" + "MercyIslandTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fortified " + "|" + "MercyIslandTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Next Big Thing " + "|" + "MercyIslandTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Widower/Widow " + "|" + "MercyIslandTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "First Rule " + "|" + "MercyIslandTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tiki Fan " + "|" + "MercyIslandTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scurvy Dog " + "|" + "PortOakesTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Washed Up " + "|" + "PortOakesTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Powder Monkey " + "|" + "PortOakesTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Driller " + "|" + "PortOakesTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Long Walk " + "|" + "PortOakesTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Big Spider " + "|" + "PortOakesTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Commuter's Woe " + "|" + "PortOakesTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hidden Getaway " + "|" + "PortOakesTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sparky " + "|" + "CapauDiableTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Media Junky " + "|" + "CapauDiableTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Egghead " + "|" + "CapauDiableTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Steamed " + "|" + "CapauDiableTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sweet Tooth " + "|" + "CapauDiableTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Doom Sayer " + "|" + "CapAuDiableTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Science " + "|" + "CapAuDiableTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Circle Gazer " + "|" + "CapAuDiableTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Razor Toothed " + "|" + "SharkheadTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Forged by Hellfire " + "|" + "SharkheadTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Freak of Nature " + "|" + "SharkheadTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pit Viper " + "|" + "SharkheadTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sky Chaser " + "|" + "SharkheadTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unwelcome Guest " + "|" + "SharkheadTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sky Trader " + "|" + "SharkheadTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Carping the Diem " + "|" + "SharkheadTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Watcher on the Knoll " + "|" + "NervaTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Locked and Loaded " + "|" + "NervaTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nerva Wreck " + "|" + "NervaTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Primal Instinct " + "|" + "NervaTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tree Hugger " + "|" + "NervaTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unethical Tourist " + "|" + "NervaTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blind Eye " + "|" + "NervaTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Soother " + "|" + "NervaTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Deuces Wild " + "|" + "StMartialTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Paroled::Crimelord " + "|" + "StMartialTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stonekeeper " + "|" + "StMartialTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Camel Snot " + "|" + "StMartialTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dead Man's Tree " + "|" + "StMartialTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Social Worker::Slumlord " + "|" + "StMartialTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Infamous Rubble " + "|" + "StMartialTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Showstopper " + "|" + "StMartialTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Overlord " + "|" + "GrandvilleTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sewer Dweller " + "|" + "GrandvilleTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guttersnipe " + "|" + "GrandvilleTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master/Mistress of the Airwaves " + "|" + "GrandvilleTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Passing Fab " + "|" + "GrandvilleTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Image Crasher " + "|" + "GrandvilleTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Line Holder " + "|" + "GrandvilleTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gutter Bait " + "|" + "GrandvilleTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reborn " + "|" + "AbyssTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Geneticist " + "|" + "AbyssTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Here Be Dragons " + "|" + "AbyssTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Caged Beast " + "|" + "AbyssTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "No Escape " + "|" + "AbyssTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hamidon's Ire " + "|" + "AbyssTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Tree of Woe " + "|" + "AbyssTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Abyssal Gaze " + "|" + "AbyssTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monster Islander " + "|" + "MonsterIslandTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Roar of the Beast " + "|" + "MonsterIslandTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rikti Monkey Island " + "|" + "MonsterIslandTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monsters' Playthings " + "|" + "MonsterIslandTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dark Garden " + "|" + "MonsterIslandTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grim Fandango " + "|" + "MonsterIslandTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Misfit Monstrosity " + "|" + "MonsterIslandTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monster Factory " + "|" + "MonsterIslandTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Clockwork Mechanic " + "|" + "P_ClockworkMechanic" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Disappeared " + "|" + "P_Disappeared" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ferryman/woman of the Damned " + "|" + "P_FerrymanOfTheDamned" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Morbid " + "|" + "P_Morbid" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Secret Prisoner " + "|" + "P_SecretPrisoner" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trainspotter " + "|" + "P_Trainspotter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Urban Spelunker " + "|" + "P_UrbanSpelunker" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Warrior at the Gate " + "|" + "P_WarriorAtTheGate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ambitious " + "|" + "P_Ambitious" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Citizen Cole " + "|" + "P_CitizenCole" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Civic Minded " + "|" + "P_CivicMinded" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guardians of Justice " + "|" + "P_GuardiansOfJustice" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Into the Wild " + "|" + "P_IntoTheWild" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Technophile " + "|" + "P_Technophile" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Un-Civil Society " + "|" + "P_UnCivilSociety" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "On the Waterfront " + "|" + "P_Waterfront" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Broken Mind " + "|" + "P_BrokenMind" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Condemning " + "|" + "P_Condemning" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Engineer " + "|" + "P_Engineer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eyes of the Dark " + "|" + "P_EyesOfTheDark" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hoarder " + "|" + "P_Hoarder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lowlife " + "|" + "P_Lowlife" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Silent Witness " + "|" + "P_SilentWitness" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Underground Explorer " + "|" + "P_UndergroundExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gold Digger " + "|" + "P_GoldDigger" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mercy Missionary " + "|" + "P_MercyMissionary" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Park Ranger " + "|" + "P_ParkRanger" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Praetorian of Privilege " + "|" + "P_PraetorianOfPrivilege" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seen " + "|" + "P_Seen" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seer " + "|" + "P_Seer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tiberian Overseer " + "|" + "P_TiberianOverseer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tuned In " + "|" + "P_TunedIn" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Going Rouge " + "|" + "P_GoingRouge" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Binge Eater " + "|" + "P_BingeEater" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cluttered " + "|" + "P_Cluttered" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Drink Enriche! " + "|" + "P_DrinkEnriche" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grade F " + "|" + "P_GradeF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Keeping the Lights On " + "|" + "P_KeepingTheLightsOn" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ready for Anything " + "|" + "P_ReadyForAnything" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nailbiter " + "|" + "P_Nailbiter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The New Boss " + "|" + "P_TheNewBoss" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Airlift " + "|" + "P_Airlift" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Don't Drink It " + "|" + "P_DontDrinkIt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eyes to the Future " + "|" + "P_EyesToTheFuture" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longshoreman/woman " + "|" + "P_Longshoreman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Moar Power " + "|" + "P_MorePower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stockpiling " + "|" + "P_Stockpiling" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stuff of Life " + "|" + "P_StuffOfLife" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Urban Renewal " + "|" + "P_UrbanRenewal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "River Rat " + "|" + "P_RiverRat" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rail Rider " + "|" + "P_RailRider" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ace " + "|" + "RiktiCrashSiteTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vanguard Operative " + "|" + "RiktiWarZoneTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trespasser " + "|" + "RiktiWarZoneTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Powerful " + "|" + "RiktiWarZoneTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lifesaver " + "|" + "RiktiWarZoneTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Under Fire " + "|" + "RiktiWarZoneTour5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Base Jumper " + "|" + "RiktiWarZoneTour6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Homewrecker " + "|" + "RiktiWarZoneTour7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Luscious " + "|" + "RiktiWarZoneTour8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Junkyard Dog " + "|" + "RiktiWarZoneTour9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scarred " + "|" + "RiktiWarZoneTour10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Asunder " + "|" + "RiktiWarZoneTour11" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unabashed " + "|" + "RiktiWarZoneTour12" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eyewitness " + "|" + "RiktiWarZoneTour13" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Big Time " + "|" + "BigTime" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trucker " + "|" + "TruckerTour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lobbyist::Crooked Politician " + "|" + "BloodyBayTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hungry::All Consuming " + "|" + "BloodyBayTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Imploding " + "|" + "BloodyBayTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ghoulish " + "|" + "BloodyBayTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hangman/woman " + "|" + "SirensCallTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Politician " + "|" + "SirensCallTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Broad Shoulders " + "|" + "SirensCallTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Piratical " + "|" + "SirensCallTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weapon Inspector::Weapon of Mass Destruction " + "|" + "WarburgTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tunnel Rat " + "|" + "WarburgTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Triumphant " + "|" + "WarburgTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Globetrotter " + "|" + "ReclusesVictoryTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ragnarok " + "|" + "ReclusesVictoryTour2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temporal Fighter::Dark Victory " + "|" + "ReclusesVictoryTour3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Last Stand " + "|" + "ReclusesVictoryTour4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heart of the City::Hate of the City " + "|" + "SafeguardMap1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "King's Righteousness::King's Capriciousness " + "|" + "SafeguardMap2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Courage Driven::Rage Driven " + "|" + "SafeguardMap3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Freedom's Defender::Freedom's Crusher " + "|" + "SafeguardMap4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Honorable Captain::Dishonorable Captain " + "|" + "SafeguardMap5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Talos' Might::Talos' Blight " + "|" + "SafeguardMap6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cornerstone::Weak Point " + "|" + "SafeguardMap7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Founders' Protector::Founders' Invader " + "|" + "SafeguardMap8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gatekeeper::Keymaster " + "|" + "SafeguardMap9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Global Guardian::Global Threat " + "|" + "MayhemMap1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "King Maker " + "|" + "MayhemMap2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Road Raged " + "|" + "MayhemMap3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Steel Worker " + "|" + "MayhemMap4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Revolutionary::Tyrannical " + "|" + "MayhemMap5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Talon of Talos " + "|" + "MayhemMap6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Brickhouse " + "|" + "MayhemMap7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Libertarian::Anarchist " + "|" + "MayhemMap8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gate Closer::Gate Crasher " + "|" + "MayhemMap9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cage Fighter " + "|" + "CageFighter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Multidimensional " + "|" + "HydraDimension1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shrouded::Shady " + "|" + "BlackShroudDimension1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chrononaut " + "|" + "OuroborosTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Depths of Time " + "|" + "CimeroraTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnighter Club Member " + "|" + "MSClubTour1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Thrill Seeker " + "|" + "MissionArchitectTourism" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pupil " + "|" + "AtlasHistory" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Student " + "|" + "CitizenCrimeAct" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scholastic " + "|" + "DimensionalBarrier" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Authority " + "|" + "FreedomPhalanx" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Disciple " + "|" + "HeroCorps" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Expert " + "|" + "MightForRightAct" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Academic " + "|" + "NemesisSeizesControl" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Intellectual " + "|" + "PsychicFeedbackLoop" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Savant " + "|" + "RiktiWar" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Researcher " + "|" + "SpankyRabinowitz" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Historian " + "|" + "SpecialCouncil" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Just Said No to Superadine " + "|" + "Superadine" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scholar " + "|" + "WarOnDrugs" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Digger " + "|" + "Hollowing" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ghost Hunter " + "|" + "Ghostship" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alumnus " + "|" + "CroatoaHistory" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arachnos Rising " + "|" + "RecluseArrival" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lorekeeper " + "|" + "CoTMu" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Swashbuckler " + "|" + "PirateHistory" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Technofreak " + "|" + "Experiments" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Headjuiced " + "|" + "P_KnowledgeisPower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Starstruck " + "|" + "P_Starstruck" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnighter Archivist " + "|" + "Midnight_Mystery" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rule of Three " + "|" + "PositronRevampPart1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dam Hero::Dam Villain " + "|" + "PositronRevampPart2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Positron's Ally::Positron's Betrayer " + "|" + "PositronTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Synapse's Cohort::Synapse's Betrayer " + "|" + "SynapseTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sister Psyche's Comrade::Sister Psyche's Betrayer " + "|" + "SisterPsycheTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Citadel's Assistant::Citadel's Betrayer " + "|" + "BastionTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Manticore's Associate::Manticore's Betrayer " + "|" + "ManticoreTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Numina's Compatriot::Numina's Betrayer " + "|" + "NuminaTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Honorary Peacebringer::Alien Fighter " + "|" + "StrigaTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cabalist " + "|" + "CroatoaTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Burkholder's Bane " + "|" + "CouncilRobot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Column Breaker " + "|" + "5thColumnHero" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Portal Smasher " + "|" + "ShardTF1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Destroyer of Strength " + "|" + "ShardTF2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Protector of Kindness::Thorn Crusher " + "|" + "ShardTF3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Slayer of Madness " + "|" + "ShardTF4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Saved the World::Saved the World... For Later " + "|" + "STFVictory" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Peerless::Recluse's Rival " + "|" + "STFVictory2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Binder of Beasts " + "|" + "BatzulSF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pirate Hunter::Air Pirate " + "|" + "SkyRaiderSF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Leviathan " + "|" + "WaterTempleSF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crystal Keeper " + "|" + "SerafinaCrystalSF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Former Servant of Recluse::Servant of Recluse " + "|" + "LordRecluseTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arbiter " + "|" + "5thColumnVillain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Apocalyptic " + "|" + "VanguardCoOpTF" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pain Killer " + "|" + "CoPComplete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temporal Strife " + "|" + "TAComplete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alpha Struck " + "|" + "AlphaTF_Complete_TinMage" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weapon Master/Warrior Princess " + "|" + "AlphaTF_Complete_Apex" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Transcendent " + "|" + "HollowsTR" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Transmogrified " + "|" + "TerraVoltaTR" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Charmer " + "|" + "SewerTR" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Liberator::Destroyer of Earth " + "|" + "EdenTR" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Negotiator " + "|" + "SL2Mission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spelunker " + "|" + "SL2Point5Mission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Plague Stopper::Deadly Virus " + "|" + "SL3Mission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spirit Warrior " + "|" + "SL4MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pwnz " + "|" + "SL4MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Redeemer::Conqueror " + "|" + "SL5MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mystical Savior::Mystical Adept " + "|" + "SL5MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Corrupter " + "|" + "SL6MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "War Wall Defender::Saboteur " + "|" + "SL6MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Doctor's Ally " + "|" + "SL7MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Frontline " + "|" + "SL7MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Emancipator " + "|" + "SL8MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Meteorologist " + "|" + "SL8MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bodyguard " + "|" + "SL9MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agent " + "|" + "SL9MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Fairest::Turf Protector " + "|" + "I11HeroStoryArc1Complete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Singular Vision " + "|" + "I11HeroStoryArc2Complete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Helping Hands " + "|" + "I19HeroStoryArcComplete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Plague Carrier " + "|" + "VSL2.5MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mask Maker " + "|" + "VSL2.5MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stone Cold " + "|" + "VSL2MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bone Collector " + "|" + "VSL2MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Penitent of Vice::Paragon of Vice " + "|" + "VSL3MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seaweed " + "|" + "VSL3MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Strikebreaker " + "|" + "VSL4MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Slag Reaper " + "|" + "VSL4MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agent of Discord " + "|" + "VSL5MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Soul Taker " + "|" + "VSL5MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bad Luck " + "|" + "VSL6MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Exterminator " + "|" + "VSL6MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Skip Tracer " + "|" + "VSL7MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Portal Hopper " + "|" + "VSL7MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Party Crasher " + "|" + "VSL8MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mage Hunter " + "|" + "VSL8MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Efficiency Expert " + "|" + "VSL9MissionA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Couch Potato " + "|" + "VSL9MissionB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bane of Ajax " + "|" + "I11VillainStoryArc1Complete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Explosive Finale " + "|" + "I11VillainStoryArc2Complete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Keeper of the Coral Lore " + "|" + "I19VillainStoryArcComplete" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Member of Vanguard " + "|" + "VanguardEnabled" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Thief of Midnight " + "|" + "MSThief" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnight Squad " + "|" + "MSMember" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lost Savior " + "|" + "MSLost" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Origin of Power " + "|" + "MSOrigin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rescuer::Big Softie " + "|" + "Yin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Avid Reader " + "|" + "P_Tutorial_Reader" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Denial of Service " + "|" + "P_DenialofService" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Moral High Ground " + "|" + "P_5MoralChoice" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Resistance Member " + "|" + "P_Resistance" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Loyalist " + "|" + "P_Loyalist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tipped Off " + "|" + "P_GotTip" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Walking the Path " + "|" + "P_AlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ear to the Street " + "|" + "P_HeroAlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Streetwise " + "|" + "P_VigilanteAlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Well Informed " + "|" + "P_RogueAlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Inquisitor " + "|" + "P_VillainAlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tested the Water " + "|" + "P_OtherAlignmentMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Made a Stand " + "|" + "P_MoralityMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Empowered " + "|" + "P_BuildCommonIA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Initiate " + "|" + "P_BuildUncommonIA" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Speed Demon " + "|" + "SlolamGold" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Accelerated " + "|" + "SlolamSilver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Qualified " + "|" + "SlolamBronze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Speeder " + "|" + "SlolamGold2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agile " + "|" + "SlolamSilver2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Surefooted " + "|" + "SlolamBronze2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Laureate " + "|" + "P_Antilaureate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Protester " + "|" + "P_Protestor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Takedown Artist " + "|" + "P_TakedownArtist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reformed Firebug::Firebug " + "|" + "MayhemFirebug" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pedestrian::Impounder " + "|" + "MayhemImpounder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fire Marshal::Fired " + "|" + "SafeguardFireMarshal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bomb Squad::Blue Wire " + "|" + "SafeguardBombSquad" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "PPD Deputy::Disgraced Deputy " + "|" + "SafeguardPPDDeputy" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Security Expert::Inside Man/Woman " + "|" + "SafeguardSecurityExpert" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Interceptor " + "|" + "SafeguardInterceptor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Statesman's Task Force " + "|" + "MasterofStatesmansTaskForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Lord Recluse's Strike Force " + "|" + "MasterofLordReclusesStrikeForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of the 5th Column Strike Force " + "|" + "MasterofReichsmansStrikeForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of the 5th Column Task Force " + "|" + "MasterofReichsmansTaskForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of the Imperious Task Force " + "|" + "MasterofImperiousTaskForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Lady Grey's Task Force " + "|" + "MasterofLadyGreysTaskforce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Tin Mage Task Force " + "|" + "AlphaTF_Master_TinMage" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master of Apex Task Force " + "|" + "AlphaTF_Master_Apex" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Already Dead " + "|" + "AlphaTF_BattleMaidenQuick" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Army of Neu " + "|" + "AlphaTF_NeuronClones" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Burden Bearer::Arm's Length " + "|" + "AlphaTF_DronesUnused" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Drone Protector " + "|" + "AlphaTF_DronesAlive" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hacker " + "|" + "AlphaTF_ComputersQuick" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Kitty's Got Claws " + "|" + "AlphaTF_BobcatMaxed" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnight Dodger What Dodges at Midnight " + "|" + "AlphaTF_NoProximityBombs" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gallant " + "|" + "P_HeroAlignment" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Above the Law " + "|" + "P_VigilanteAlignment" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scoundrel " + "|" + "P_RogueAlignment" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dastardly " + "|" + "P_VillainAlignment" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heard the Call " + "|" + "P_HeroAlignmentPower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fearsome " + "|" + "P_VigilanteAlignmentPower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trickster " + "|" + "P_RogueAlignmentPower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Frenzied " + "|" + "P_VillainAlignmentPower" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ascended " + "|" + "P_Ascended" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Descended " + "|" + "P_Descended" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Come Full Circle " + "|" + "P_ComeFullCircle" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grass Is Meaner::Grass is Greener " + "|" + "P_TravelBetweenCities" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Protector of Innocents::Soldier " + "|" + "Level10P_Level_10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Keeper of Peace::Insider " + "|" + "Level20P_Level_20" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Defender of Truth::Wiseguy/gal " + "|" + "Level30P_Level_30" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Justice Incarnate::Captain " + "|" + "Level40P_Level_40" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hero of the City::Made " + "|" + "Level50P_Level_50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Praetorian Professional " + "|" + "P_Level_10Level10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Survivor of Praetoria " + "|" + "P_Level_20Level20" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rift Traveler " + "|" + "P_Level_30Level30" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Primal Praetorian " + "|" + "P_Level_40Level40" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Praetor " + "|" + "P_Level_50Level50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Celebrity::Bling " + "|" + "CelebrityP_Money_500K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sensation::Mr./Mrs. Big " + "|" + "SensationP_Money_2500K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Superstar::Midas Touch " + "|" + "SuperstarP_Money_10M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trendsetter " + "|" + "MultimillionaireP_Money_50M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Popular " + "|" + "BillionaireP_Money_250M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Leader " + "|" + "TrillionaireP_Money_500M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Who's Who " + "|" + "P_Money_500KCelebrity" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Acclaimed " + "|" + "P_Money_2500KSensation" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Illustrious " + "|" + "P_Money_10MSuperstar" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Renowned " + "|" + "P_Money_50MMultimillionaire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Living Legend " + "|" + "P_Money_250MBillionaire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Chosen One " + "|" + "P_Money_500MTrillionaire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tourist::Visitor " + "|" + "TouristP_s_10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Collector::Native " + "|" + "CollectorP_s_25" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Explorer::Obsessed " + "|" + "ExplorerP_s_50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pathfinder " + "|" + "PathfinderP_s_100" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trailblazer " + "|" + "TrailblazerP_s_200" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Seeker " + "|" + "500sP_s_500" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Adventurer " + "|" + "750sP_s_750" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Questing " + "|" + "1000sP_s_1000" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Voyager " + "|" + "1250sP_s_1250" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lead-Follower " + "|" + "P_s_10Tourist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Extractor of Secrets " + "|" + "P_s_25Collector" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Knows the Truth " + "|" + "P_s_50Explorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Knows He/She Knows Not " + "|" + "P_s_100Pathfinder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Emissary " + "|" + "P_s_200Trailblazer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Overachiever " + "|" + "P_s_500500s" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Doer " + "|" + "P_s_750750s" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Prepared " + "|" + "P_s_10001000s" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "No Road Not Taken " + "|" + "P_s_12501250s" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tough::Stoic " + "|" + "ToughP_Damage_100K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Indestructible::Hard Case " + "|" + "IndestructableP_Damage_500K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Adamant::Ironman/woman " + "|" + "AdamantP_Damage_1M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unbreakable " + "|" + "UnbreakableP_Damage_10M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nigh Indestructible " + "|" + "NighIndestructableP_Damage_25M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Invulnerable " + "|" + "InvulnerableP_Damage_50M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Immortal " + "|" + "ImmortalP_Damage_100M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Slammer " + "|" + "P_Damage_100KTough" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Big Dog " + "|" + "P_Damage_500KIndestructable" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Laughs it Off " + "|" + "P_Damage_1MAdamant" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Iron Willed " + "|" + "P_Damage_10MUnbreakable" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Concussed " + "|" + "P_Damage_25MNighIndestructable" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Marvel of Modern Medicine " + "|" + "P_Damage_50MInvulnerable" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Challenger of Gods " + "|" + "P_Damage_100MImmortal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Unwavering::Punch Drunk " + "|" + "UnwaveringFocusP_Debt_50K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Unyielding::Unbroken " + "|" + "UnyieldingWillP_Debt_100K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Unbroken Spirit::Relentless " + "|" + "UnbrokenSpiritP_Debt_200K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Deathless " + "|" + "DeathlessP_Debt_400K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Undying " + "|" + "UndyingP_Debt_600K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Exalted " + "|" + "ExaltedP_Debt_1M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Repaired " + "|" + "P_Debt_50KUnwaveringFocus" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Went the Extra Mile " + "|" + "P_Debt_100KUnyieldingWill" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Undaunted " + "|" + "P_Debt_200KUnbrokenSpirit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Impulsive " + "|" + "P_Debt_400KDeathless" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Never Learns " + "|" + "P_Debt_600KUndying" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Infinite Lives " + "|" + "P_Debt_1MExalted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Medic::Fixer " + "|" + "MedicP_Heal_250K" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Surgeon::Doc " + "|" + "SurgeonP_Heal_1M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Doctor::Mad Scientist " + "|" + "DoctorP_Heal_2M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Medical Specialist " + "|" + "MedicalSpecialistP_Heal_3M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Medicine Man/Woman " + "|" + "MedicineManP_Heal_5M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Empath " + "|" + "EmpathP_Heal_10M" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Do Gooder " + "|" + "P_Heal_250KMedic" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Safekeeper " + "|" + "P_Heal_1MSurgeon" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Clutch " + "|" + "P_Heal_2MDoctor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "To The Rescue " + "|" + "P_Heal_3MMedicalSpecialist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Savior " + "|" + "P_Heal_5MMedicineMan" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Death's Jailer " + "|" + "P_Heal_10MEmpath" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Restrained::Slacker " + "|" + "RestrainedP_Held_10Min" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Entangled::Sleepy " + "|" + "EntangledP_Held_30Min" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Imprisoned::Dazed and Confused " + "|" + "ImprisonedP_Held_60Min" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Confined " + "|" + "ConfinedP_Held_3Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Caged " + "|" + "CagedP_Held_6Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Jailed " + "|" + "JailedP_Held_12Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Waiting " + "|" + "P_Held_10MinRestrained" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Locked Out " + "|" + "P_Held_30MinEntangled" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trapped " + "|" + "P_Held_60MinImprisoned" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stuck " + "|" + "P_Held_3HourConfined" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Can't Do That " + "|" + "P_Held_6HourCaged" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lagged " + "|" + "P_Held_12HourJailed" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Advisor::Comrade " + "|" + "AdvisorP_Mentor_4Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guide::Drill Instructor " + "|" + "GuideP_Mentor_8Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Paragon::Svengali " + "|" + "ParagonP_Mentor_12Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Role Model " + "|" + "RoleModelP_Mentor_16Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Epitome " + "|" + "EpitomeP_Mentor_20Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Paradigm " + "|" + "ParadigmP_Mentor_24Hour" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Encourager " + "|" + "P_Mentor_4HourAdvisor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Motivator " + "|" + "P_Mentor_8HourGuide" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Teacher " + "|" + "P_Mentor_12HourParagon" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dean of Hard Knocks " + "|" + "P_Mentor_16HourRoleModel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Philosopher " + "|" + "P_Mentor_20HourEpitome" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Old-Timer " + "|" + "P_Mentor_24HourParadigm" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Annihilator " + "|" + "RogueHero" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Demolitionist " + "|" + "RWZBomber" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Firebase Zulu Security Detail::Firebase Zulu Deserter " + "|" + "FirebaseZuluSecurity" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Troll Task Force Member " + "|" + "TrollTaskForce" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "No One Left Behind " + "|" + "I19HeroStoryArcOptional" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Connected " + "|" + "I19VillainStoryArcOptional" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Conspiracy Theorist " + "|" + "CreySetHeadlineStealer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Archmage " + "|" + "MagusSetDemonic" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vanguard " + "|" + "RiktiWarSetMegalomaniac" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Geas of the Kind Ones " + "|" + "GeasoftheKindOnesMayhemForceOfNature" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Yesterday's News::Headline Stealer " + "|" + "HeadlineStealerCreySet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Exorcised::Demonic " + "|" + "DemonicMagusSet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "In Therapy::Megalomaniac " + "|" + "MegalomaniacRiktiWarSet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Uninsurable::Force of Nature " + "|" + "MayhemForceOfNatureGeasoftheKindOnes" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Field Crafter " + "|" + "InventorAccolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Watchman/woman " + "|" + "RIWEAccolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mission Engineer " + "|" + "ArchitectAccolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Received the Atlas Medallion::Atlas Shrugged " + "|" + "AtlasSetMarshal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Portal Jockey " + "|" + "DimensionalHopperSetBornInBattle" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Freedom Phalanx Reserve Member::Freedom Phalanx Fallen " + "|" + "FreedomPhalanxSetHighPainThreshold" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Task Force Commander::Task Force Abandoner " + "|" + "TaskForceCommanderMayhemInvader" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ex-Marshal::Marshal " + "|" + "MarshalAtlasSet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Born in Battle " + "|" + "BornInBattleDimensionalHopperSet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gotten Soft::High Pain Threshold " + "|" + "HighPainThresholdFreedomPhalanxSet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Return Visitor::Invader " + "|" + "MayhemInvaderTaskForceCommander" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "False Image::Mirage " + "|" + "MiragePatron" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shark Bait::Bloodletter " + "|" + "BloodInTheWaterPatron" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Anti-Venom::Spider's Kiss " + "|" + "SpidersKissPatron" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fwoosh::The Stinger " + "|" + "TheStingerPatron" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Received the Stalwart Medallion::Lost the Stalwart Medallion " + "|" + "StalwartMedallion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Earned the Statesman Star::Denied the Statesman Star " + "|" + "StatesmanStar" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Awarded the Freedom Cross::Stripped of the Freedom Cross " + "|" + "FreedomCross" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Thorn Robber " + "|" + "UnderTheKnife" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Thorn Thief " + "|" + "NipTuck" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Thorn Usurper " + "|" + "Facelifted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alchemist " + "|" + "DJ_Alchemist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Anti-Arachnos Activist::Web Weaver " + "|" + "DJ_Web_Weaver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Archaeologist " + "|" + "DJ_Archaeologist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blockade Runner::Profiteer " + "|" + "DJ_Black_Marketeer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Brood Leaver::Brood Leader " + "|" + "DJ_Brood_Leader" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Defector::Black Ops " + "|" + "DJ_Black_Ops" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Entrepreneur::Free Trade Advocate " + "|" + "DJ_Entrepreneur" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gladiator " + "|" + "DJ_Gladiator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master Architect " + "|" + "DJ_Master_Architect" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mercenary " + "|" + "DJ_Soldier" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Physician " + "|" + "DJ_Doctor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Police Chief::Corrupt Commissioner " + "|" + "DJ_Warden" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rapid Response Member::Trouble Maker " + "|" + "DJ_Rapid_Response_Member" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scientist::Crackpot " + "|" + "DJ_Scientist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Security Chief::Security Breach " + "|" + "DJ_Security_Chief" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Socialite " + "|" + "DJ_Trend_Setter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Time Lord " + "|" + "DJ_Time_Traveler" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Traveler::Border Crosser " + "|" + "DJ_Traveler" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Whistleblower::Crey Scientist " + "|" + "DJ_Crey_Scientist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Atlas Tour Guide " + "|" + "AtlasParkExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Galactic Explorer " + "|" + "GalaxyCityExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "PPD Informant::Former PPD Stoolie " + "|" + "KingsRowExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Canyon Carver " + "|" + "SteelCanyonExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sky Gazer " + "|" + "SkywayCityExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Faultline Finder " + "|" + "FaultlineExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Island Hopper " + "|" + "TalosIslandExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "IP Address " + "|" + "IndependencePortExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Citizen of Salamanca " + "|" + "CroatoaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Zig Warden::King/Queen of the Zig " + "|" + "BrickstownExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lost and Found " + "|" + "FoundersFallsExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Portal Corp Analyst " + "|" + "PeregrineIslandExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Apprentice Plumber " + "|" + "SewerNetworkExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master Plumber " + "|" + "AbSewerNetworkExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wincott's Ally::Wincott's Betrayer " + "|" + "HollowsExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Perez Park Perfection " + "|" + "PerezParkExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Boom... Goes the Town " + "|" + "BoomtownExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Straight Through Striga " + "|" + "StrigaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Utilities Commission " + "|" + "TerraVoltaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "A Light in Dark Astoria " + "|" + "DarkAstoriaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crazy for Crey's Folly " + "|" + "CreysFolleyExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Adam/Eve in Waiting " + "|" + "EdenExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Busy, Busy Bee " + "|" + "TheHiveExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "All Your Firebase... " + "|" + "FirebaseZuluExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cascade Cleansing " + "|" + "CascadeArchExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Can't Stop the Chant " + "|" + "ChantryExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Storming the Palace " + "|" + "StormPalaceExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mercy Mariner " + "|" + "MercyIslandExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Marcone Insider " + "|" + "PortOakesExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mad Science Supporter " + "|" + "CapAuDiableExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Jumped the Shark " + "|" + "SharkheadExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nerva Navigator " + "|" + "NervaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Johnny's Ex-Best Friend::Johnny's Go To Guy/Gal " + "|" + "StMartialExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eye on Arachnos::Arachnos Spymaster " + "|" + "GrandvilleExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Touched Bottom " + "|" + "AbyssExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Honorary Monster " + "|" + "MonsterIslandExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Knows Nova's Nooks " + "|" + "P_UndergroundNovaPraetoriaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pure Praetorian " + "|" + "P_NovaPraetoriaExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Beneath the Empire " + "|" + "P_UndergroundImperialCityExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Emperor for a Day " + "|" + "P_ImperialCityExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Neu You Could Do It " + "|" + "P_UndergroundNeutropolisExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "A Neu Man/Woman " + "|" + "P_NeutropolisExplorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alpha Unlocked " + "|" + "IncarnateAlphaSlot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agent of Order::Agent of Chaos " + "|" + "P_GoingRogueAccoladeP_P_GoingRogueAccolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agent of Praetoria " + "|" + "P_P_GoingRogueAccoladeP_GoingRogueAccolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arena All-Star " + "|" + "InitiateAllStar" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bug Hunter " + "|" + "BetaTester" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Force of Justice::Force of Injustice " + "|" + "SafeguardForceOfJustice" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hell and Back " + "|" + "Halloween09Accolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Meticulous " + "|" + "Halloween2010Accolade" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pocket D VIP Gold Club Member " + "|" + "GoldClub" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Praetoria's Son/Daughter " + "|" + "P_DVDEditionDVDEdition" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stouthearted::Stonehearted " + "|" + "SafeguardStouthearted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ten times the Victor " + "|" + "Victor10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "True to the Last " + "|" + "P_LoyaltyReward" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "V.I.P.::Destined One " + "|" + "DVDEditionP_DVDEdition" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vigilant::Determined " + "|" + "Vigilant" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Viva Praetoria " + "|" + "P_VivaPraetoria" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "5th Columnist " + "|" + "5thGladiator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "7th Generation Paragon Protector " + "|" + "ParagonProtect" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Abomination " + "|" + "Abomination" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Anathema " + "|" + "Anathema" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arachnobot " + "|" + "Arachnobot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arachnoid " + "|" + "Arachnoid" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arch-Mage of Agony " + "|" + "ArchMage" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Avalanche Shaman " + "|" + "Avalanche" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Behemoth Overlord " + "|" + "Behemoth" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bladegrass " + "|" + "Bladegrass" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Boulder " + "|" + "Boulder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Button Man Gunner " + "|" + "ButtonMan" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Caliban " + "|" + "Caliban" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chief Swiper " + "|" + "Swiper" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cobra " + "|" + "Cobra" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cog " + "|" + "Cog" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Coralax Blue Hybrid " + "|" + "Coralax" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crab Spider Longfang " + "|" + "CrabSpider" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crane Enforcer " + "|" + "Crane" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Power Tank " + "|" + "PowerTank" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ember Legacy of Flame " + "|" + "LegacyFlame" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fire Thorn Caster " + "|" + "FireCaster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fortunata Seer " + "|" + "Fortunata" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fungoid " + "|" + "Fungoid" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gremlin " + "|" + "Gremlin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hellfrost " + "|" + "Hellstrike" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hordeling Lasher " + "|" + "Lasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hercules Titan " + "|" + "Titan" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hydra Protean " + "|" + "Protean" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ink Man " + "|" + "InkMan" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Juicer " + "|" + "JuicerSniper" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Kaolin Legacy of Earth " + "|" + "LegacyEarth" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Nullifier " + "|" + "Nullifier" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Rifleman " + "|" + "LongbowRifleman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Spec-Ops " + "|" + "SpecOps" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Warden 1 " + "|" + "LongbowTanker" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Warden 2 " + "|" + "LongbowDefender" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lucent Legacy of Light " + "|" + "LegacyLight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Meson " + "|" + "Meson" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mob Specialist " + "|" + "MobSpec" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mook " + "|" + "Mook" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mook Capo " + "|" + "Capo" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mu Guardian " + "|" + "MuGuardian" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nebula Elite Buckshot " + "|" + "NebulaBuckshot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nemesis Soldier " + "|" + "NemesisSoldier" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Night Haunt " + "|" + "PirateGhost" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Night Widow " + "|" + "NightWidow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Outcast Slugger " + "|" + "Slugger" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Omega Wolf " + "|" + "OmegaWolf" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pariah Anchorite " + "|" + "Anchorite" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Penumbra Elite Adjutant " + "|" + "Penumbra" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Raider Engineer " + "|" + "RaiderEngineer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Red Cap " + "|" + "RedcapGladiator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Red Hand " + "|" + "RedHand" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Research Assistant " + "|" + "ResearchAsst" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rikti Drone " + "|" + "RiktiDrone" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shivan Destroyer " + "|" + "ShivanDestroyer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Slag Pile " + "|" + "SlagPile" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Smasher Elite " + "|" + "WarriorSmasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Snowbeast " + "|" + "WinterMinion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sorcerer " + "|" + "Sorcerer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Steel Strongman " + "|" + "Strongman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Swift Steel " + "|" + "SwiftSteel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tank Smasher " + "|" + "TankSmasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tarantula " + "|" + "Tarantula" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wailer " + "|" + "Wailer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wailer Queen " + "|" + "WailerQueen" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Warhulk " + "|" + "Warhulk" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wolf Spider Enforcer " + "|" + "WolfSpider" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wolf Spider Tac Ops " + "|" + "TacOps" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wraith " + "|" + "Wraith" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Zenith Warcry Mk I " + "|" + "Warcry" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grand Lanista " + "|" + "GrandLanista" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lanista " + "|" + "Lanista" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trustworthy " + "|" + "Veteran3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Faithful " + "|" + "Veteran6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dependable " + "|" + "Veteran9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Loyal " + "|" + "Veteran12" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Zealous " + "|" + "Veteran15" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Staunch " + "|" + "Veteran18" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Steadfast " + "|" + "Veteran21" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Devoted " + "|" + "Veteran24" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dedicated " + "|" + "Veteran27" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Committed " + "|" + "Veteran30" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unswerving " + "|" + "Veteran33" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Addicted " + "|" + "Veteran36" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ardent " + "|" + "Veteran39" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fervent " + "|" + "Veteran42" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eternal " + "|" + "Veteran45" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Battle Hardened " + "|" + "Veteran48" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tried and True " + "|" + "Veteran51" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Allegiant " + "|" + "Veteran54" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Abiding " + "|" + "Veteran57" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "City Traveler " + "|" + "Veteran60" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Honorable " + "|" + "Veteran63" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Enduring " + "|" + "Veteran66" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Genuine " + "|" + "Veteran69" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Distinguished " + "|" + "Veteran72" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Purposeful " + "|" + "Veteran78" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Resolute " + "|" + "Veteran75" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Agent Provocateur " + "|" + "AgentProvocateur" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Risk Taker::Most Wanted " + "|" + "MostWanted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vigilante::Wanted " + "|" + "Wanted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arena Victor " + "|" + "1ArenaWin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arena Duelist " + "|" + "1WinDuel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tournament Victor " + "|" + "1WinSwissDraw" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arena Survivalist " + "|" + "1WinFreeForAll" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pentad Victor " + "|" + "1WinPentad" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Strawweight Champion " + "|" + "Strawweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Flyweight Champion " + "|" + "Flyweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bantamweight Champion " + "|" + "Bantamweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Featherweight Champion " + "|" + "Featherweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lightweight Champion " + "|" + "Lightweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Welterweight Champion " + "|" + "Welterweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Middleweight Champion " + "|" + "Middleweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cruiserweight Champion " + "|" + "Cruiserweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heavyweight Champion " + "|" + "Heavyweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Super Heavyweight Champion " + "|" + "SuperHeavyweight" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reinforcement " + "|" + "Ace" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Forward Observer " + "|" + "Manhunter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Disruptor " + "|" + "Headhunter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Irradiated " + "|" + "BloodyBay" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Siren's Song::Raider " + "|" + "SirensCall" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Web Master " + "|" + "Warburg" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Time Traveler " + "|" + "RecluseVictory" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temporal Agent " + "|" + "RVHeavy10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temporal Spy " + "|" + "RVHeavy50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temporal Soldier " + "|" + "RVHeavy250" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Oppressor " + "|" + "RVStatesman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Destroyer " + "|" + "RVPositron" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dead-eye " + "|" + "RVManticore" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Interrogator " + "|" + "RVSisterPsyche" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Breakneck " + "|" + "RVSynapse" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Uppercut " + "|" + "RVBackAlleyBrawler" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Redeemed Blackguard::Blackguard " + "|" + "RVAllHeroes" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arachnophobic " + "|" + "RVLordRecluse" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wrangler " + "|" + "RVBlackScorpion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Poltergeist " + "|" + "RVGhostWidow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sandblasted " + "|" + "RVScirocco" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shark Hunter " + "|" + "RVCaptainMako" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Justiciar::Fallen Justiciar " + "|" + "RVAllVillains" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Time Machinist " + "|" + "RVPillbox10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Somewhere in Time " + "|" + "RVPillbox100" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Back From the Future " + "|" + "RVPillbox1000" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gunner " + "|" + "Gunner" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Power Liberator::Master Thief " + "|" + "MasterThief" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rocketman/woman " + "|" + "Rocketman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Artisan " + "|" + "InventionLevel1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master Artisan " + "|" + "InventionLevel2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Craftsman " + "|" + "InventionLevel3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master Craftsman " + "|" + "InventionLevel4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fabricator " + "|" + "InventionLevel5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Declining " + "|" + "InventionDeBuff1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Decaying " + "|" + "InventionDeBuff2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Despoiler " + "|" + "InventionDeBuff3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dismantler " + "|" + "InventionDeBuff4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Retrograde " + "|" + "InventionDeBuff5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Medicator " + "|" + "InventionHeal1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mender " + "|" + "InventionHeal2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rebuilder " + "|" + "InventionHeal3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Revivifier " + "|" + "InventionHeal4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reanimator " + "|" + "InventionHeal5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Armorer " + "|" + "InventionMitigation1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Guardian " + "|" + "InventionMitigation2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Protector " + "|" + "InventionMitigation3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Security " + "|" + "InventionMitigation4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Defensive " + "|" + "InventionMitigation5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blinding " + "|" + "InventionMez1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Beguiler " + "|" + "InventionMez2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Charming " + "|" + "InventionMez3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Charismatic " + "|" + "InventionMez4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mesmerizer " + "|" + "InventionMez5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Energy Conservationist " + "|" + "InventionEndurance1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Battery Powered " + "|" + "InventionEndurance2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nuclear Powered " + "|" + "InventionEndurance3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mystically Powered " + "|" + "InventionEndurance4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Perpetual " + "|" + "InventionEndurance5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trigger Man " + "|" + "InventionRateOfFire1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hired Gun " + "|" + "InventionRateOfFire2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hit-Man " + "|" + "InventionRateOfFire3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sniper " + "|" + "InventionRateOfFire4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sharpshooter " + "|" + "InventionRateOfFire5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Major " + "|" + "InventionAccuracy1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lt. Colonel " + "|" + "InventionAccuracy2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Colonel " + "|" + "InventionAccuracy3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Major General " + "|" + "InventionAccuracy4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "General " + "|" + "InventionAccuracy5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Munitionist " + "|" + "InventionDamage1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weaponeer " + "|" + "InventionDamage2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Warhead " + "|" + "InventionDamage3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arms Dealer " + "|" + "InventionDamage4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lord of War " + "|" + "InventionDamage5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pilgrim " + "|" + "InventionTravel1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vagabond " + "|" + "InventionTravel2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Wanderer " + "|" + "InventionTravel3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Walks the Earth " + "|" + "InventionTravel4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nomad " + "|" + "InventionTravel5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Inventor " + "|" + "InventTutorial" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Anger Manager::Vandal " + "|" + "MayhemVandal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Army of Me " + "|" + "Defeat8ClonesChallenge" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bane of Dannan " + "|" + "BaneofDannan" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Banisher " + "|" + "Banisher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Believer " + "|" + "Believer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Blindsider " + "|" + "P_DefeatSeers" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bloody Hands::Hero Slayer " + "|" + "MayhemHeroSlayer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bomb Specialist " + "|" + "RIWEDefeatUXB" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bonecrusher " + "|" + "Bonecrusher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cannibal " + "|" + "P_DefeatGhouls" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cap Buster " + "|" + "CapBuster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Centurion " + "|" + "Cimerorans" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chief " + "|" + "RIWEDefeatHeavies" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Clockstopper " + "|" + "Clockstopper" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Coldblooded::Coldhearted " + "|" + "ThornIsle1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dee Jay " + "|" + "TrollsRaveSmasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Demon Slayer " + "|" + "DemonSlayer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Devilfish " + "|" + "OctopusEvent" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dimensional Warder " + "|" + "DimensionalWarder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Diplomat::Breathes Easy " + "|" + "STFVictory3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ectoplasmic " + "|" + "GhostShipDefeats" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Electrician " + "|" + "DeathSurge" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Escapee " + "|" + "P_DefeatFailedExperiments" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ex-Archvillain::Archvillain " + "|" + "DefeatStatesman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eye of the Storm " + "|" + "P_DefeatMaelstrom" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Finder " + "|" + "Finder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fire Chief::Asbestos " + "|" + "HellionsFireGold" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Firefighter::Firestomper " + "|" + "HellionsFireSilver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fort Knox " + "|" + "FortKnox" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gangbuster " + "|" + "Gangbuster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gearsmasher " + "|" + "Gearsmasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Giant Killer " + "|" + "GiantKiller" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Goon Squad " + "|" + "GoonSquad" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gravedigger " + "|" + "Gravedigger" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hammer Down " + "|" + "Scrapyard" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hammerhead " + "|" + "Hammerhead" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Haunted " + "|" + "Haunted" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heat Seeker " + "|" + "ArachnosFlier" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "HellBane " + "|" + "ThornIsle2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hellspawned " + "|" + "Hellspawned" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Illusionist " + "|" + "Illusionist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Infiltrator " + "|" + "Infiltrator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Isolator " + "|" + "Quarantine" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Judge & Jury::Merciless " + "|" + "P_DefeatRGC" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Keeper of Secrets " + "|" + "KeeperOfSecrets" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Kill Skuls " + "|" + "Bonecrusher2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Knight Errant::Black Knight " + "|" + "ClockworkPaladinEvent" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Legionnaire " + "|" + "Legionnaire" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Locksmith::Safecracker " + "|" + "MayhemSafeCracker" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Man/Woman in Black " + "|" + "ManinBlack" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Marked for Death " + "|" + "P_DefeatSyndicate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master At Arms " + "|" + "RWZMasterAtArms" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Master/Mistress of Olympus " + "|" + "MasterOfOlympus" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monkeywrencher " + "|" + "Monkeywrencher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Mongoose " + "|" + "TheMongoose" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "No Authority " + "|" + "P_DefeatPPD" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pirate " + "|" + "GhostTrap50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Privateer " + "|" + "Privateer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Protectorate::Devourer of Earth " + "|" + "Protectorate" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pumpkin King/Queen " + "|" + "PumpkinKing" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pumpkin Master/Mistress " + "|" + "PumpkinMaster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Raver " + "|" + "TrollsRaveRoundup" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Redundant " + "|" + "P_DefeatTheDestroyers" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reformed::Villain " + "|" + "Villain" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Regenerator " + "|" + "Regenerator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Resistance is Futile " + "|" + "P_DefeatResistance" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Roman " + "|" + "RomulusDefeated" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sentry " + "|" + "RIWEDefeatInvaders" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Soul Binder " + "|" + "SoulBinder" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spectral " + "|" + "GhostTrap100" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spellbinding " + "|" + "Spellbinding" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Spider Smasher " + "|" + "SpiderSmasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Statesman's Pal::Praetoria's Bane " + "|" + "StatesmansPal" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Strike Buster " + "|" + "StrikeBuster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Surging " + "|" + "Surging" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tank Buster " + "|" + "Blitzkrieg" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Technophobe " + "|" + "P_DefeatClockwork" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Silver Bullet " + "|" + "SilverBullet" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Slayer " + "|" + "Slayer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Solution " + "|" + "TheSolution" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Thin Line::Outlaw " + "|" + "MayhemOutlaw" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tracer " + "|" + "Tracer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unleasher " + "|" + "Unleasher" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Untouchable " + "|" + "Untouchable" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unveiler " + "|" + "Unveiler" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Usurper " + "|" + "DefeatRecluse" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Venomous " + "|" + "Venomous" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Villain Disruptor " + "|" + "SafeguardVillainDisruptor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Visionary " + "|" + "Visionary" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Volcanic " + "|" + "Volcanic" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Volunteer Firefighter::Fire Bane " + "|" + "HellionsFireBronze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Warden::Cruel Warden " + "|" + "Warden" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weatherman/girl " + "|" + "Weatherman" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weed Whacker " + "|" + "WeedWhacker" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Whip Cracker " + "|" + "P_DefeatDesdemona" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Zookeeper " + "|" + "Zookeeper" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Celebrant " + "|" + "Anniversary1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reveler " + "|" + "Anniversary2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Merrymaker " + "|" + "Anniversary3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Entertainer " + "|" + "Anniversary4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Jubilant " + "|" + "Anniversary5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Exultant " + "|" + "Anniversary6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pursuer::Elusive " + "|" + "CoVAnniversary" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Partygoer " + "|" + "_PocketD_Jubilee" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heart of Light::Heart of Darkness " + "|" + "ValentineLogin" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Handsome/Beautiful " + "|" + "LovePotion" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Toothbreaker " + "|" + "Hunt" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Clothes Horse " + "|" + "Halloween20071" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fashionable " + "|" + "Halloween20072" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ostentatious " + "|" + "Halloween20073" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dark Fiend " + "|" + "Halloween09DarkFiend" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hell Hath No Fury " + "|" + "Halloween09WomenScorned" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Terror " + "|" + "Halloween09Terrors" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monster Masher " + "|" + "Halloween09Monster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monstrous " + "|" + "Halloween09MonsterGM" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ghost Touched " + "|" + "GhostTouched" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Iron Warrior " + "|" + "IronWarrior" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hallow Spirit " + "|" + "HallowSpirit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hunter " + "|" + "Hunter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Buster " + "|" + "Buster" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shifter " + "|" + "Shifter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dead Head " + "|" + "DeadHead" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Malleus " + "|" + "Malleus" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Secured " + "|" + "Halloween2010Secured" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Power Hungry " + "|" + "Halloween2010PowerHungry" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Artifact Destroyer " + "|" + "Halloween2010ArtifactDestroyer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Veiled " + "|" + "Halloween2010Veiled" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arriviste " + "|" + "Halloween2010Arriviste" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Trusting " + "|" + "Halloween2010Trusting" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Glimpsed the Abyss " + "|" + "Halloween20081" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Safety in Numbers " + "|" + "Halloween20082" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Evil's Resident " + "|" + "Halloween20083" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Apocalypse Survivor " + "|" + "Halloween20084" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Miraculous " + "|" + "Miraculous" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Joyful " + "|" + "Joyful" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Frosty " + "|" + "Frosty" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crystallized " + "|" + "Crystallized" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Longbow Reservist::Jet-Setter " + "|" + "Holiday20051" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cold Front " + "|" + "ColdFront" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gifted " + "|" + "Winter2007" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Frostbitten " + "|" + "Winter2008" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Festive " + "|" + "Winter2009" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cold Warrior " + "|" + "SnowmenDefeat" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Frozen Fury " + "|" + "SnowbeastDefeat" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Holiday Spirit::Scrooge " + "|" + "Holiday20052" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lord/Lady of Winter " + "|" + "Winter2009DefeatLordWinter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Toy Collector " + "|" + "Holiday20053" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "###MISSING INFO### " + "|" + "Holiday2010" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Passport " + "|" + "ServerTransfer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Troubleshooter " + "|" + "Flashback5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Adjuster " + "|" + "Flashback10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Calibrator " + "|" + "Flashback15" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Alterist " + "|" + "Flashback20" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Balancer " + "|" + "Flashback25" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Correctionist " + "|" + "Flashback30" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Repairman/Repairwoman " + "|" + "Flashback35" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Overhauler " + "|" + "Flashback40" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Time Fixer " + "|" + "Flashback45" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ouroboros Mender " + "|" + "Flashback50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "All For One, One For All " + "|" + "LivesTeamPool0" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Divided Mastery " + "|" + "LivesTeamPool1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Proportional Supremacy " + "|" + "LivesTeamPool3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shared Victory " + "|" + "LivesTeamPool5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bronze Medalist " + "|" + "TimeLimitBronze" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Silver Medalist " + "|" + "TimeLimitSilver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gold Medalist " + "|" + "TimeLimitGold" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Venturous " + "|" + "DeBuffedPartyatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Headstrong " + "|" + "DeBuffedPartyatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Adventurous " + "|" + "DeBuffedPartyatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Daredevil " + "|" + "DeBuffedPartyatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rash " + "|" + "DeBuffedPartyatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Venturesome " + "|" + "DeBuffedPartyatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Reckless " + "|" + "DeBuffedPartyatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Temerarious " + "|" + "DeBuffedPartyatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Audacious " + "|" + "BuffedEnemiesatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Courageous " + "|" + "BuffedEnemiesatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bold " + "|" + "BuffedEnemiesatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fearless " + "|" + "BuffedEnemiesatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Valiant " + "|" + "BuffedEnemiesatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stalwart " + "|" + "BuffedEnemiesatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lionhearted " + "|" + "BuffedEnemiesatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Intrepid " + "|" + "BuffedEnemiesatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Contributor " + "|" + "ArchetypePowersOnlyatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vocational " + "|" + "ArchetypePowersOnlyatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Professional " + "|" + "ArchetypePowersOnlyatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "All Business " + "|" + "ArchetypePowersOnlyatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Archetypical " + "|" + "ArchetypePowersOnlyatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Obligated " + "|" + "ArchetypePowersOnlyatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Missionary " + "|" + "ArchetypePowersOnlyatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Undertaker " + "|" + "ArchetypePowersOnlyatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Refrained " + "|" + "NoTravelPowersatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shut Down " + "|" + "NoTravelPowersatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Desisted " + "|" + "NoTravelPowersatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Barred " + "|" + "NoTravelPowersatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Still " + "|" + "NoTravelPowersatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stalled " + "|" + "NoTravelPowersatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Stemmed " + "|" + "NoTravelPowersatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Suspended " + "|" + "NoTravelPowersatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Diminished " + "|" + "NoTemporaryPowersatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Curtailed " + "|" + "NoTemporaryPowersatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Capped " + "|" + "NoTemporaryPowersatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Inhibited " + "|" + "NoTemporaryPowersatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Hindered " + "|" + "NoTemporaryPowersatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bound " + "|" + "NoTemporaryPowersatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Limited " + "|" + "NoTemporaryPowersatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Demarcated " + "|" + "NoTemporaryPowersatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cut-Off " + "|" + "NoEpicPowersatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Marginalized " + "|" + "NoEpicPowersatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cliche " + "|" + "NoInspirationsatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unimaginative " + "|" + "NoInspirationsatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Flat " + "|" + "NoInspirationsatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unoriginal " + "|" + "NoInspirationsatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dull " + "|" + "NoInspirationsatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Phoned It In " + "|" + "NoInspirationsatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Indifferent " + "|" + "NoInspirationsatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Uninspired " + "|" + "NoInspirationsatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Lessened " + "|" + "NoEnhancementsatSL2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Debilitated " + "|" + "NoEnhancementsatSL3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Anemic " + "|" + "NoEnhancementsatSL4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Weakened " + "|" + "NoEnhancementsatSL5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Deflated " + "|" + "NoEnhancementsatSL6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Decreased " + "|" + "NoEnhancementsatSL7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Devalued " + "|" + "NoEnhancementsatSL8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Unenhanced " + "|" + "NoEnhancementsatSL9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Entrusted with the Secret " + "|" + "OuroborosEnabled" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vendor " + "|" + "AuctionSeller1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Salesman/Saleswoman " + "|" + "AuctionSeller2" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Tradesman/Tradeswoman " + "|" + "AuctionSeller3" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Merchant " + "|" + "AuctionSeller4" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Peddler " + "|" + "AuctionSeller5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Retailer " + "|" + "AuctionSeller6" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dealer " + "|" + "AuctionSeller7" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Auctioneer " + "|" + "AuctionSeller8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Businessman/Businesswoman " + "|" + "AuctionSeller9" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Marketer::Black Martketeer " + "|" + "AuctionSeller10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shopkeeper " + "|" + "AuctionSeller11" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Power Seller " + "|" + "AuctionSeller12" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Broker " + "|" + "AuctionRecipes" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Inspiring " + "|" + "AuctionInspirations" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scrounger " + "|" + "AuctionSalvage" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Enhancer " + "|" + "AuctionEnhancements" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Arachnos Traitor::Arachnos Official " + "|" + "DJ_Arachnos_Official" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Architect " + "|" + "DJ_Architect" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Banker::Thief " + "|" + "DJ_Banker" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cannon Fodder::Arachnos Agent " + "|" + "DJ_Arachnos_Agent" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Caregiver::Pain Specialist " + "|" + "DJ_Caregiver" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Chronologist " + "|" + "DJ_Mender" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cimeroran " + "|" + "DJ_Cimeroran" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "City Official::Ousted Official " + "|" + "DJ_City_Official" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Clubber " + "|" + "DJ_Clubber" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Commuter::Fare Jumper " + "|" + "DJ_Commuter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey Test Subject::Crey Employee " + "|" + "DJ_Crey_Employee" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Day Trader::Marketeer " + "|" + "DJ_Auctioneer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dimensional Explorer::Dimensional Plunderer " + "|" + "DJ_Dimensional_Explorer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Duelist " + "|" + "DJ_Duelist" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fashion Designer " + "|" + "DJ_Clotheshorse" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grounded::Pilot " + "|" + "DJ_Pilot" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gunrunner::Smuggler " + "|" + "DJ_Smuggler" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Intern::Wage Slave " + "|" + "DJ_Intern" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Law Enforcer::Dirty Cop " + "|" + "DJ_Law_Enforcement" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Midnighter " + "|" + "DJ_Midnighter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Monitor Duty " + "|" + "DJ_Monitor_Duty" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mortician " + "|" + "DJ_Gravedigger" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Patroller (Praetorian Origin)::Criminal " + "|" + "P_DJ_On_Patrol" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Patroller::Criminal " + "|" + "DJ_On_Patrol" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Predator " + "|" + "DJ_Griefer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Professional Liar::Demagogue " + "|" + "DJ_Demagogue" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Professor " + "|" + "DJ_Scholar" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Shop Keeper::Price Gouger " + "|" + "DJ_Shop_Keeper" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Vanguard Recruit " + "|" + "DJ_Vanguard_Recruit" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Destructive " + "|" + "ArchitectDestroy1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Extractor " + "|" + "ArchitectRescue1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Poor Impulse Control " + "|" + "ArchitectClick10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Workaholic " + "|" + "ArchitectNonRequiredObjective25" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Admiring " + "|" + "ArchitectPlayDevChoice1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Critic " + "|" + "ArchitectHallofFame5" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cunning " + "|" + "ArchitectRogueMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Early Bird " + "|" + "ArchitectFirstPlay" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gamer " + "|" + "ArchitectPlay25" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Heroic " + "|" + "ArchitectHeroMissions10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Villainous " + "|" + "ArchitectVillainMissions10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Walks the Line " + "|" + "ArchitectVigilanteMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Author " + "|" + "ArchitectAuthor50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Recognized " + "|" + "ArchitectTotalStars100" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Among Friends " + "|" + "ArchitectTest8" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Bug Fixer " + "|" + "ArchitectTestOwnMission" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Virtual Destruction " + "|" + "ArchitectTestDestroy1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Virtual Extractor " + "|" + "ArchitectTestRescue1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Virtual Victim " + "|" + "ArchitectDefeatedinTest" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Virtual Victor " + "|" + "ArchitectTestKill10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Virtually Impulsive " + "|" + "ArchitectTestClick1" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Payoff " + "|" + "ArchitectPurchase" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ticket Taker " + "|" + "ArchitectTickets100" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Architect X " + "|" + "Architects10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Architect XXV " + "|" + "Architects25" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Assassin " + "|" + "ArchitectCustomKills50" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Customizer " + "|" + "ArchitectCustomBoss" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Energized " + "|" + "ArchitectInspirations10" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Atlas Park Beacon " + "|" + "BeaconAtlasPark" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Galaxy City Beacon " + "|" + "BeaconGalaxyCity" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Kings Row Beacon " + "|" + "BeaconKingsRow" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Steel Canyon Beacon " + "|" + "BeaconSteelCanyon" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Skyway City Beacon " + "|" + "BeaconSkywayCity" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Faultline Beacon " + "|" + "BeaconFaultline" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Talos Island Beacon " + "|" + "BeaconTalosIsland" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Independence Port Beacon " + "|" + "BeaconIndependencePort" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Croatoa Beacon " + "|" + "BeaconCroatoa" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Brickstown Beacon " + "|" + "BeaconBrickstown" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Founders Falls Beacon " + "|" + "BeaconFoundersFalls" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Peregrine Island Beacon " + "|" + "BeaconPeregrineIsland" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "The Hollows Beacon " + "|" + "BeaconTheHollows" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Perez Park Beacon " + "|" + "BeaconPerezPark" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Boomtown Beacon " + "|" + "BeaconBoomTown" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Striga Isle Beacon " + "|" + "BeaconStriga" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Terra Volta Beacon " + "|" + "BeaconTerraVolta" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Dark Astoria Beacon " + "|" + "BeaconDarkAstoria" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Crey's Folly Beacon " + "|" + "BeaconCreysFolly" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Eden Beacon " + "|" + "BeaconEden" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mercy Isle Beacon " + "|" + "BeaconMercy" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Port Oakes Beacon " + "|" + "BeaconPortOakes" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Cap au Diable Beacon " + "|" + "BeaconCap" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Sharkhead Isle Beacon " + "|" + "BeaconSharkhead" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Nerva Beacon " + "|" + "BeaconNerva" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "St. Martial Beacon " + "|" + "BeaconStMartial" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Grandville Beacon " + "|" + "BeaconGrandville" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Rikti War Zone Beacon " + "|" + "BeaconRiktiCrashSite" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Pocket D Beacon " + "|" + "BeaconPocketD" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Analyzer Base Defense " + "|" + "AnalyzerTurret" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Gas Trap Base Defense " + "|" + "GasTrap" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Improved Energy Turret " + "|" + "ImpEnergyBeam" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Repulsor Base Defense " + "|" + "Repulsor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Benedict DR Turret Plans " + "|" + "DampeningRay" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Benedict DR-3 Turret Plans " + "|" + "EliteDampeningRay" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Benedict DR-2 Turret Plans " + "|" + "ImpDampeningRay" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Supercomputer " + "|" + "Supercomputer" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Autonomous Expert System Plans " + "|" + "SyntheticIntel" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Mega Monitor Plans " + "|" + "EdgeMonitor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Emergency Capacitor Plans " + "|" + "EmergencyCapacitor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Auto-Doc Plans " + "|" + "MedAdvisor" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Robo-Surgery Plans " + "|" + "DoctorsDesk" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Combat Log Plans " + "|" + "CombatLog" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Fusion Generator Plans " + "|" + "FusionGenerator" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Improved Igniter " + "|" + "ImpIgniter" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Scorpian's Surprise " + "|" + "ScorpionTurret" + vbCrLf, True)
            My.Computer.FileSystem.WriteAllText(badgedat, "Ritki Plasma Turret " + "|" + "RiktiTurret" + vbCrLf, True)


        End If


    End Sub

    Private Sub combo_load()
        'load combo boxes with relevant information for badges in badge command dialogue

        Dim checkload As String = System.AppDomain.CurrentDomain.BaseDirectory() + "data\badges.dat"

        Dim textreader As New System.IO.StreamReader(checkload)
        badges_cmbbx.Items.Clear()
        badges_cmbbx.Items.Insert(0, "")

        edit_item.edit_badge_cmbbx.Items.Clear()
        edit_item.edit_badge_cmbbx.Items.Insert(0, "")

        Do While textreader.Peek() <> -1
            Dim textline As String = textreader.ReadLine() & vbNewLine
            Dim textsplit = textline.ToString.Split("|")
            badges_cmbbx.Items.Add(textsplit(0))
            badge_dictionary.Add(textsplit(0), textsplit(1))

            edit_item.edit_badge_cmbbx.Items.Add(textsplit(0))

        Loop
        badges_cmbbx.SelectedIndex = -1
        edit_item.edit_badge_cmbbx.SelectedIndex = -1


    End Sub

    Private Sub divider_button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles divider_button.Click
        'add a divider to popmenu
        cmd_item = "-Type Divider"
        treetag = cmd_item
        If chosennode = "" Then
            poptree.Nodes.Add("Divider").Tag = treetag
        Else
            poptree.SelectedNode.Nodes.Add("Divider").Tag = treetag
            poptree.SelectedNode.ExpandAll()
            poptree.SelectedNode = Nothing
            chosennode = ""
        End If
        poptree.SelectedNode = Nothing
        cmd_item = ""
        treetag = ""
    End Sub

    Private Sub title_radio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles title_radio.CheckedChanged
        'use title font in popmenu
        hide_all()
        display_lbl.Visible = True
        display_txtbox.Visible = True

        item_type = "Title"
        commandtxtbox.Text = ""
    End Sub



    Private Sub load_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles load_btn.Click
        'load saved xml file for editing
        poptree.Nodes.Clear()

        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strPathdialogue As String


        fd.Title = "Open a Save File"
        fd.Filter = "XML File|*.xml|Text|*.txt|All files|*.*"
        fd.FilterIndex = 1
        fd.RestoreDirectory = True

        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPathdialogue = fd.FileName


            'Dim doc As XmlReader = New XmlNodeReader(strPathdialogue)
            Dim document As XmlReader = New XmlTextReader(strPathdialogue)
            'create the xml reader
            'loop through the xml file
            While (document.Read())

                Dim type = document.NodeType

                'if node type was element
                If (type = XmlNodeType.Element) Then
                    If document.Name = "Root" Then
                        menu_name_txtbox.Text = document.GetAttribute("menu_name").ToString
                        written_by_txtbox.Text = document.GetAttribute("written_By").ToString
                        version_num_txtbox.Text = document.GetAttribute("version_number").ToString
                    End If
                    If document.Name = "RootItem" Then
                        Dim rootnodeid As String = document.GetAttribute("id").ToString
                        Dim rootnodeparams As String = document.GetAttribute("Parameters").ToString
                        Dim rootnodeorder As Integer = document.GetAttribute("Root_Order")
                        Dim rootnodelevel As Integer = document.GetAttribute("node_level")
                        Dim rootpath As String = document.GetAttribute("Path").ToString


                        rootnode = poptree.Nodes.Add(rootnodeid)
                        rootnode.Tag = rootnodeparams

                    End If

                    If document.Name = "FirstLevelItem" Then
                        Dim firstlvlid As String = document.GetAttribute("id").ToString
                        Dim firstlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfirst = rootnode.Nodes.Add(firstlvlid)
                        childfirst.Tag = firstlvlparams
                    End If

                    If document.Name = "SecondLevelItem" Then
                        Dim secondlvlid As String = document.GetAttribute("id").ToString
                        Dim secondlvlparams As String = document.GetAttribute("Parameters").ToString

                        childsecond = childfirst.Nodes.Add(secondlvlid)
                        childsecond.Tag = secondlvlparams
                    End If

                    If document.Name = "ThirdLevelItem" Then
                        Dim thirdlvlid As String = document.GetAttribute("id").ToString
                        Dim thirdlvlparams As String = document.GetAttribute("Parameters").ToString

                        childthird = childsecond.Nodes.Add(thirdlvlid)
                        childthird.Tag = thirdlvlparams
                    End If

                    If document.Name = "FourthLevelItem" Then
                        Dim fourthlvlid As String = document.GetAttribute("id").ToString
                        Dim fourthlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfourth = childthird.Nodes.Add(fourthlvlid)
                        childfourth.Tag = fourthlvlparams
                    End If

                    If document.Name = "FifthLevelItem" Then
                        Dim fifthlvlid As String = document.GetAttribute("id").ToString
                        Dim fifthlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfifth = childfourth.Nodes.Add(fifthlvlid)
                        childfifth.Tag = fifthlvlparams
                    End If
                End If



            End While
            document.Close()
        End If

        poptree.ExpandAll()

    End Sub

    Private Sub delete_all_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete_all_btn.Click
        'delete popmenu
        poptree.Nodes.Clear()
        'hide_all()
        'clear_all()

    End Sub

    Private Sub badges_cmbbx_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles badges_cmbbx.SelectedIndexChanged
        'load info into display box from combobox using a dictionary for relevant information
        Dim dictkey As String = badges_cmbbx.SelectedItem
        display_txtbox.Text = dictkey
        If Not dictkey Is Nothing Then
            If badge_dictionary.ContainsKey(dictkey) Then
                Dim dictvalue As String = badge_dictionary(dictkey)
                commandtxtbox.Text = dictvalue
            End If
        End If
    End Sub
    Private Sub add_clear()
        'reset to default
        chosennode = ""
        cmd_item = ""
        display_txtbox.Text = ""
        commandtxtbox.Text = ""
        title_radio.Checked = False
        menu_radio_btn.Checked = False
        cmd_radio.Checked = False
        info_radio_btn.Checked = False
        badge_radio_btn.Checked = False
        commandtxtbox.Visible = False
        display_lbl.Visible = False
        display_txtbox.Visible = False
        command_label.Visible = False
        badge_reference_label.Visible = False
        badges_cmbbx.Visible = False
        badges_cmbbx.SelectedIndex = -1
        badges_cmbbx_lbl.Visible = False
    End Sub


    Private Sub add_menu_item_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles add_menu_item_btn.Click
        'add a item to popmenu based on type of item being added
        If chosennode = "" Then
            Select Case item_type
                Case "Option"
                    cmd_item = "-Type Option"
                    treetag = cmd_item
                Case "Menu"
                    cmd_item = "-Type Menu"
                    treetag = cmd_item
                Case "Command"
                    cmd_item = "-Type Command -Command_Text " + commandtxtbox.Text
                    treetag = cmd_item
                Case "Badge"
                    cmd_item = "-Type Badge -Command_Text " + commandtxtbox.Text
                    treetag = cmd_item
                Case "Title"
                    cmd_item = "-Type Title"
                    treetag = cmd_item
                Case Else
            End Select

            poptree.Nodes.Add(display_txtbox.Text).Tag = treetag
            poptree.SelectedNode = Nothing
            add_clear()

            'restrict child nodes to 5 levels deep in menu as more levels generally needs a ton of screen space 
        ElseIf nodelevel = "5" Then
            MsgBox("You are only allowed to add up to 5 child nodes to a root node.")
        ElseIf Not poptree.SelectedNode.Tag.ToString.Contains("Menu") Then
            'nodeinfo.Contains("Menu") Then
            MsgBox("This is not a menu node and can not accept child nodes")
        Else
            Select Case item_type
                Case "Option"
                    cmd_item = "-Type Option"
                    treetag = cmd_item
                Case "Menu"
                    cmd_item = "-Type Menu"
                    treetag = cmd_item
                Case "Command"
                    cmd_item = "-Type Command -Command_Text " + commandtxtbox.Text
                    treetag = cmd_item
                Case "Badge"
                    cmd_item = "-Type Badge -Command_Text " + commandtxtbox.Text
                    treetag = cmd_item
                Case "Title"
                    cmd_item = "-Type Title"
                    treetag = cmd_item
                Case Else
            End Select

            poptree.SelectedNode.Nodes.Add(display_txtbox.Text).Tag = treetag
            poptree.SelectedNode.ExpandAll()
            poptree.SelectedNode = Nothing
            add_clear()

        End If
    End Sub

    Private Sub preview_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles preview_btn.Click
        'unfinished menu preview button, still unsure why when populating the toolstripmenu that you have to essentially populate them menu
        'by starting at the lowest level and then working up to the root item
        Dim item As New ToolStripMenuItem
        Dim subItem As New ToolStripMenuItem
        Dim subsubItem As New ToolStripMenuItem
        Dim menuindex As ToolStripMenuItem
        Dim firstlvlmenu As New ToolStripMenuItem
        Dim parent_node As String

        'subItem.DropDownItems.Add(subsubItem)
        'item.DropDownItems.Add(subItem)
        'Preview_Strip.MenuStrip1.Items.Add(item)

        'load preview from xml file
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strPathdialogue As String


        fd.Title = "Open a Save File"
        fd.Filter = "XML File|*.xml|Text|*.txt|All files|*.*"
        fd.FilterIndex = 1
        fd.RestoreDirectory = True

        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPathdialogue = fd.FileName


            'Dim doc As XmlReader = New XmlNodeReader(strPathdialogue)
            Dim document As XmlReader = New XmlTextReader(strPathdialogue)

            'create the xml reader
            'loop through the xml file

            'Dim save_file = XDocument.Load(strPathdialogue)
            Dim Index As Integer
            While document.EOF = False

                document.Read()

                'While (document.Read())

                Dim type = document.NodeType
                'if node type was element
                If (type = XmlNodeType.Element) Then
                    If document.Name = "TopLevelMenu" Then
                        item.Text = document.GetAttribute("id")

                        Preview_Strip.MenuStrip1.Items.Add(item)
                    End If

                    If document.Name = "RootItem" Then
                        menuindex = Preview_Strip.MenuStrip1.Items(0)

                        'subItem.Text = document.GetAttribute("id").ToString
                        'MsgBox(subItem.Text)
                        Index = menuindex.DropDownItems.Add(New ToolStripButton With {.Text = document.GetAttribute("id").ToString})
                        'item.DropDownItems.Add(New ToolStripMenuItem With {.Text = document.GetAttribute("id").ToString})
                        'item.DropDownItems.Add(subItem)
                        'item.Tag = document.GetAttribute("id").ToString

                    End If


                    If document.Name = "FirstLevelItem" Then
                        'CType(menuindex.DropDownItems(indexstring), ToolStripDropDown).add()
                        'MsgBox(whateveritem)
                        parent_node = document.GetAttribute("id").ToString
                        MsgBox(parent_node)




                        '.Item(count)
                        'menuindex.DropDownItems.Add(New ToolStripMenuItem With {.Text = document.GetAttribute("id").ToString})
                    End If

                End If

            End While
            Preview_Strip.Show()
        End If





    End Sub

    Private Sub EditItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditItemToolStripMenuItem.Click
        'tool strip edit item, can also be accessed by right clicking a node
        Dim edit_item_tag As String
        Dim right_select_name = poptree.SelectedNode.Text
        edit_item.edit_display_txtbx.Text = right_select_name
        edit_item_tag = poptree.SelectedNode.Tag.ToString

        If edit_item_tag.Contains("-Command_Text") Then
            edit_item_tag_pass = edit_item_tag.Split("-")
            edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
            edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
        Else
            edit_type_pass = edit_item_tag.Remove(0, 5)
        End If

        Select Case edit_type_pass.Trim()
            Case "Option"
                edit_item.edit_type_info.Checked = True
            Case "Badge"
                edit_item.edit_type_badge.Checked = True
                edit_item.edit_badge_dropdown.Visible = True
                edit_item.edit_badge_cmbbx.Visible = True
                edit_item.edit_command_txt_box.Visible = True
                edit_item.edit_badge_command.Visible = True
                Dim dictkey As String = edit_command_pass
                edit_item.edit_command_txt_box.Text = dictkey
                edit_item.edit_command_txt_box.Enabled = False

                For Each item In badge_dictionary
                    If item.Value = dictkey Then
                        edit_item.edit_badge_cmbbx.Text = item.Key
                    End If
                Next
            Case "Command"
                edit_item.edit_type_command.Checked = True
                edit_item.edit_command_txt_box.Visible = True
                edit_item.edit_command_box.Visible = True
                edit_item.edit_command_txt_box.Text = edit_command_pass
            Case "Title"
                edit_item.edit_type_title.Checked = True
            Case "Divider"
                MsgBox("Dividers can not be edited. Please remove the Divider using the Remove Item button. Be sure to select the Divider you want to remove.")
                Exit Sub
            Case "Menu"
                Dim Msg, Style, Title, Response  ' Help, Ctxt, , MyString
                Msg = "This is a menu item, if you alter it from a menu item your menu may not compile correctly for the game." + vbCrLf + "It is recommend mended you remove the menu item and child items and rebuild those items." + vbCrLf + "Do you wish to continue?" ' Define message.
                Style = vbYesNo + vbCritical + vbDefaultButton2    ' Define buttons.
                Title = "Popmenu Generator Warning!"    ' Define title.
                ' Help = "DEMO.HLP"    ' Define Help file.
                ' Ctxt = 1000    ' Define topic context. 
                ' Display message.
                Response = MsgBox(Msg, Style, Title) 'Help,  Ctxt
                If Response = vbYes Then    ' User chose Yes.
                    ' MyString = "Yes"    ' Perform some action.
                    edit_item.edit_type_menu.Checked = True
                Else    ' User chose No.
                    ' MyString = "No"    ' Perform some action.
                    Exit Sub
                End If

            Case Else
        End Select


        edit_item.Show()



    End Sub

    Private Sub JobTreeView_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles poptree.MouseDown
        'handle clicking on items in the treeview control and selecting the node
        Dim ClickPoint As Point
        Dim ClickedNode As TreeNode
        If e.Button = MouseButtons.Right Then
            ClickPoint = New Point(e.X, e.Y)
            ClickedNode = poptree.GetNodeAt(ClickPoint)
            poptree.SelectedNode = ClickedNode
        End If


    End Sub

    Private Sub LoadSavedFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadSavedFileToolStripMenuItem.Click
        'menu strip item to load a saved popmenu
        poptree.Nodes.Clear()

        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strPathdialogue As String


        fd.Title = "Open a Save File"
        fd.Filter = "XML File|*.xml|Text|*.txt|All files|*.*"
        fd.FilterIndex = 1
        fd.RestoreDirectory = True

        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPathdialogue = fd.FileName


            'Dim doc As XmlReader = New XmlNodeReader(strPathdialogue)
            Dim document As XmlReader = New XmlTextReader(strPathdialogue)
            'create the xml reader
            'loop through the xml file
            While (document.Read())

                Dim type = document.NodeType

                'if node type was element
                If (type = XmlNodeType.Element) Then
                    If document.Name = "Root" Then
                        menu_name_txtbox.Text = document.GetAttribute("menu_name").ToString
                        written_by_txtbox.Text = document.GetAttribute("written_By").ToString
                        version_num_txtbox.Text = document.GetAttribute("version_number").ToString
                    End If
                    If document.Name = "RootItem" Then
                        Dim rootnodeid As String = document.GetAttribute("id").ToString
                        Dim rootnodeparams As String = document.GetAttribute("Parameters").ToString
                        Dim rootnodeorder As Integer = document.GetAttribute("Root_Order")
                        Dim rootnodelevel As Integer = document.GetAttribute("node_level")
                        Dim rootpath As String = document.GetAttribute("Path").ToString


                        rootnode = poptree.Nodes.Add(rootnodeid)
                        rootnode.Tag = rootnodeparams

                    End If

                    If document.Name = "FirstLevelItem" Then
                        Dim firstlvlid As String = document.GetAttribute("id").ToString
                        Dim firstlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfirst = rootnode.Nodes.Add(firstlvlid)
                        childfirst.Tag = firstlvlparams
                    End If

                    If document.Name = "SecondLevelItem" Then
                        Dim secondlvlid As String = document.GetAttribute("id").ToString
                        Dim secondlvlparams As String = document.GetAttribute("Parameters").ToString

                        childsecond = childfirst.Nodes.Add(secondlvlid)
                        childsecond.Tag = secondlvlparams
                    End If

                    If document.Name = "ThirdLevelItem" Then
                        Dim thirdlvlid As String = document.GetAttribute("id").ToString
                        Dim thirdlvlparams As String = document.GetAttribute("Parameters").ToString

                        childthird = childsecond.Nodes.Add(thirdlvlid)
                        childthird.Tag = thirdlvlparams
                    End If

                    If document.Name = "FourthLevelItem" Then
                        Dim fourthlvlid As String = document.GetAttribute("id").ToString
                        Dim fourthlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfourth = childthird.Nodes.Add(fourthlvlid)
                        childfourth.Tag = fourthlvlparams
                    End If

                    If document.Name = "FifthLevelItem" Then
                        Dim fifthlvlid As String = document.GetAttribute("id").ToString
                        Dim fifthlvlparams As String = document.GetAttribute("Parameters").ToString

                        childfifth = childfourth.Nodes.Add(fifthlvlid)
                        childfifth.Tag = fifthlvlparams
                    End If
                End If



            End While
            document.Close()
        End If

        poptree.ExpandAll()

    End Sub

    Private Sub SaveCurrentTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveCurrentTreeToolStripMenuItem.Click
        'menu strip item to save a popmenu instead of using a button
        Dim filesaver As New SaveFileDialog
        filesaver.DefaultExt = "xml"
        filesaver.Filter = "xml files|*.xml"
        filesaver.OverwritePrompt = True
        filesaver.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory() + "save_files"
        filesaver.ShowDialog()

        If filesaver.FileName <> "" Then
            Dim file As System.IO.FileStream
            file = System.IO.File.Create(filesaver.FileName)
            file.Close()

            Dim textWriter = New XmlTextWriter(filesaver.FileName, System.Text.Encoding.UTF8)
            textWriter.WriteStartDocument()
            Dim menuname As String
            menuname = menu_name_txtbox.Text
            textWriter.WriteStartElement("Root")
            textWriter.WriteAttributeString("menu_name", menu_name_txtbox.Text)
            textWriter.WriteAttributeString("written_By", written_by_txtbox.Text)
            textWriter.WriteAttributeString("version_number", version_num_txtbox.Text)

            textWriter.WriteStartElement("TopLevelMenu")
            textWriter.WriteAttributeString("id", menuname)

            Dim nodes As TreeNodeCollection
            nodes = poptree.Nodes
            Dim root As TreeNode
            For Each root In nodes

                textWriter.WriteStartElement("RootItem")
                textWriter.WriteAttributeString("id", root.Text)
                'textWriter.WriteAttributeString("Type", item_type)
                textWriter.WriteAttributeString("Parameters", root.Tag.ToString)
                textWriter.WriteAttributeString("Root_Order", root.Index)
                textWriter.WriteAttributeString("node_level", root.Level.ToString)
                textWriter.WriteAttributeString("Path", root.FullPath)

                If root.Nodes.Count > 0 Then
                    Dim level1 As TreeNodeCollection
                    level1 = root.Nodes
                    Dim child As TreeNode

                    For Each child In level1
                        textWriter.WriteStartElement("FirstLevelItem")
                        textWriter.WriteAttributeString("id", child.Text)
                        'textWriter.WriteAttributeString("Type", item_type)
                        textWriter.WriteAttributeString("Parameters", child.Tag.ToString)
                        'textWriter.WriteAttributeString("Parent_Node", root.Text)
                        textWriter.WriteAttributeString("Parent_Node", child.Parent.Text)
                        textWriter.WriteAttributeString("child_Order", child.Index)
                        textWriter.WriteAttributeString("node_level", child.Level.ToString)
                        textWriter.WriteAttributeString("Path", child.FullPath)


                        If child.Nodes.Count > 0 Then
                            Dim level2 As TreeNodeCollection
                            level2 = child.Nodes
                            Dim child2 As TreeNode
                            For Each child2 In level2
                                textWriter.WriteStartElement("SecondLevelItem")
                                textWriter.WriteAttributeString("id", child2.Text)
                                'textWriter.WriteAttributeString("Type", item_type)
                                textWriter.WriteAttributeString("Parameters", child2.Tag.ToString)
                                textWriter.WriteAttributeString("Parent_Node", child2.Parent.Text)
                                textWriter.WriteAttributeString("child_Order", child2.Index)
                                textWriter.WriteAttributeString("node_level", child2.Level.ToString)
                                textWriter.WriteAttributeString("Path", child2.FullPath)



                                'end second level loop

                                If child.Nodes.Count > 0 Then
                                    Dim level3 As TreeNodeCollection
                                    level3 = child2.Nodes
                                    Dim child3 As TreeNode
                                    For Each child3 In level3
                                        textWriter.WriteStartElement("ThirdLevelItem")
                                        textWriter.WriteAttributeString("id", child3.Text)
                                        'textWriter.WriteAttributeString("Type", item_type)
                                        textWriter.WriteAttributeString("Parameters", child3.Tag.ToString)
                                        textWriter.WriteAttributeString("Parent_Node", child3.Parent.Text)
                                        textWriter.WriteAttributeString("child_Order", child3.Index)
                                        textWriter.WriteAttributeString("node_level", child3.Level.ToString)
                                        textWriter.WriteAttributeString("Path", child3.FullPath)



                                        If child.Nodes.Count > 0 Then
                                            Dim level4 As TreeNodeCollection
                                            level4 = child3.Nodes
                                            Dim child4 As TreeNode
                                            For Each child4 In level4
                                                textWriter.WriteStartElement("FourthLevelItem")
                                                textWriter.WriteAttributeString("id", child4.Text)
                                                'textWriter.WriteAttributeString("Type", item_type)
                                                textWriter.WriteAttributeString("Parameters", child4.Tag.ToString)
                                                textWriter.WriteAttributeString("Parent_Node", child4.Parent.Text)
                                                textWriter.WriteAttributeString("child_Order", child4.Index)
                                                textWriter.WriteAttributeString("node_level", child4.Level.ToString)
                                                textWriter.WriteAttributeString("Path", child4.FullPath)



                                                If child.Nodes.Count > 0 Then
                                                    Dim level5 As TreeNodeCollection
                                                    level5 = child4.Nodes
                                                    Dim child5 As TreeNode
                                                    For Each child5 In level5
                                                        textWriter.WriteStartElement("FifthLevelItem")
                                                        textWriter.WriteAttributeString("id", child5.Text)
                                                        'textWriter.WriteAttributeString("Type", item_type)
                                                        textWriter.WriteAttributeString("Parameters", child5.Tag.ToString)
                                                        textWriter.WriteAttributeString("Parent_Node", child5.Parent.Text)
                                                        textWriter.WriteAttributeString("child_Order", child5.Index)
                                                        textWriter.WriteAttributeString("node_level", child5.Level.ToString)
                                                        textWriter.WriteAttributeString("Path", child5.FullPath)

                                                        textWriter.WriteEndElement()

                                                    Next
                                                End If

                                                textWriter.WriteEndElement()
                                            Next
                                        End If
                                        textWriter.WriteEndElement()

                                    Next
                                End If
                                'end third level loop

                                textWriter.WriteEndElement()
                            Next
                            'end second level child addtion
                        End If
                        textWriter.WriteEndElement()
                        'end 1st level loop
                    Next

                    'end 1st level child addition
                End If

                textWriter.WriteEndElement()
                'end root loop
            Next


            'SaveNodes(TreeView.Nodes, textWriter)

            textWriter.WriteEndElement()
            textWriter.WriteEndElement()
            textWriter.Close()
            'end filesaver if

        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        help_about.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        badge_dictionary.Clear()
        End
    End Sub


    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_game_btn.Click
        'export loaded menu to .mnu file that the game recognizes as a popmenu
        If poptree.Nodes.Count = 0 Then
            Dim Msg, Style, Title, Response  ' Help, Ctxt, , MyString
            Msg = "You do not have a menu loaded." + vbCrLf + "Would you like to load a menu from a save file now?" ' Define message.
            Style = vbYesNo + vbCritical + vbDefaultButton2    ' Define buttons.
            Title = "Popmenu Generator Warning!"    ' Define title.
            ' Help = "DEMO.HLP"    ' Define Help file.
            ' Ctxt = 1000    ' Define topic context. 
            ' Display message.
            Response = MsgBox(Msg, Style, Title) 'Help,  Ctxt
            If Response = vbNo Then    ' User chose No.
                ' MyString = "Yes"    ' Perform some action.
                Exit Sub
            Else    ' User chose Yes.
                poptree.Nodes.Clear()

                Dim fd As OpenFileDialog = New OpenFileDialog()
                Dim strPathdialogue As String


                fd.Title = "Open a Save File"
                fd.Filter = "XML File|*.xml|Text|*.txt|All files|*.*"
                fd.FilterIndex = 1
                fd.RestoreDirectory = True

                If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    strPathdialogue = fd.FileName


                    'Dim doc As XmlReader = New XmlNodeReader(strPathdialogue)
                    Dim document As XmlReader = New XmlTextReader(strPathdialogue)
                    'create the xml reader
                    'loop through the xml file
                    While (document.Read())

                        Dim type = document.NodeType

                        'if node type was element
                        If (type = XmlNodeType.Element) Then
                            If document.Name = "Root" Then
                                menu_name_txtbox.Text = document.GetAttribute("menu_name").ToString
                                written_by_txtbox.Text = document.GetAttribute("written_By").ToString
                                version_num_txtbox.Text = document.GetAttribute("version_number").ToString
                            End If
                            If document.Name = "RootItem" Then
                                Dim rootnodeid As String = document.GetAttribute("id").ToString
                                Dim rootnodeparams As String = document.GetAttribute("Parameters").ToString
                                Dim rootnodeorder As Integer = document.GetAttribute("Root_Order")
                                Dim rootnodelevel As Integer = document.GetAttribute("node_level")
                                Dim rootpath As String = document.GetAttribute("Path").ToString


                                rootnode = poptree.Nodes.Add(rootnodeid)
                                rootnode.Tag = rootnodeparams

                            End If

                            If document.Name = "FirstLevelItem" Then
                                Dim firstlvlid As String = document.GetAttribute("id").ToString
                                Dim firstlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfirst = rootnode.Nodes.Add(firstlvlid)
                                childfirst.Tag = firstlvlparams
                            End If

                            If document.Name = "SecondLevelItem" Then
                                Dim secondlvlid As String = document.GetAttribute("id").ToString
                                Dim secondlvlparams As String = document.GetAttribute("Parameters").ToString

                                childsecond = childfirst.Nodes.Add(secondlvlid)
                                childsecond.Tag = secondlvlparams
                            End If

                            If document.Name = "ThirdLevelItem" Then
                                Dim thirdlvlid As String = document.GetAttribute("id").ToString
                                Dim thirdlvlparams As String = document.GetAttribute("Parameters").ToString

                                childthird = childsecond.Nodes.Add(thirdlvlid)
                                childthird.Tag = thirdlvlparams
                            End If

                            If document.Name = "FourthLevelItem" Then
                                Dim fourthlvlid As String = document.GetAttribute("id").ToString
                                Dim fourthlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfourth = childthird.Nodes.Add(fourthlvlid)
                                childfourth.Tag = fourthlvlparams
                            End If

                            If document.Name = "FifthLevelItem" Then
                                Dim fifthlvlid As String = document.GetAttribute("id").ToString
                                Dim fifthlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfifth = childfourth.Nodes.Add(fifthlvlid)
                                childfifth.Tag = fifthlvlparams
                            End If
                        End If



                    End While
                    document.Close()
                End If

                poptree.ExpandAll()


            End If 'end message box
        End If 'end message box trigger

        Dim menuname As String = menu_name_txtbox.Text
        Dim written_by As String = written_by_txtbox.Text
        Dim version_number As String = version_num_txtbox.Text


        Dim file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu")
        file.Close()

        Dim versionline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " " + menuname + ".mnu" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " Written By " + written_by + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " Version " + version_number + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "Menu" + " """ + menuname + """" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "{" + vbCrLf, True)

        Dim nodes As TreeNodeCollection
        nodes = poptree.Nodes
        Dim root As TreeNode
        For Each root In nodes


            Dim rootlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"

            Dim rootlvlid As String = root.Text
            Dim rootlvlparams As String = root.Tag

            If rootlvlparams.Contains("-Command_Text") Then
                edit_item_tag_pass = rootlvlparams.Split("-")
                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
            Else
                edit_type_pass = rootlvlparams.Remove(0, 5)
            End If

            Select Case edit_type_pass.Trim()
                Case "Option"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + rootlvlid + """" + vbCrLf, True)
                Case "Title"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Title " + """" + rootlvlid + """" + vbCrLf, True)
                Case "Badge"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "LockedOption" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "{" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "DisplayName " + """" + rootlvlid + """" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                Case "Command"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + rootlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                Case "Divider"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Divider" + vbCrLf, True)
                Case "Menu"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + rootlvlid + """" + " {" + vbCrLf, True)
                    If root.Nodes.Count > 0 Then
                        Dim level1 As TreeNodeCollection
                        level1 = root.Nodes
                        Dim child As TreeNode

                        Dim firstlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                        For Each child In level1
                            Dim firstlvlid As String = child.Text
                            Dim firstlvlparams As String = child.Tag

                            If firstlvlparams.Contains("-Command_Text") Then
                                edit_item_tag_pass = firstlvlparams.Split("-")
                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                            Else
                                edit_type_pass = firstlvlparams.Remove(0, 5)
                            End If

                            Select Case edit_type_pass.Trim()
                                Case "Option"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Option" + """" + firstlvlid + """" + vbCrLf, True)
                                Case "Title"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Title " + """" + firstlvlid + """" + vbCrLf, True)
                                Case "Badge"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "LockedOption" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "{" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "DisplayName " + """" + firstlvlid + """" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                Case "Command"
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + firstlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                Case "Divider"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Divider" + vbCrLf, True)
                                Case "Menu"
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + firstlvlid + """" + " {" + vbCrLf, True)

                                    If child.Nodes.Count > 0 Then
                                        Dim level2 As TreeNodeCollection
                                        level2 = child.Nodes
                                        Dim child2 As TreeNode

                                        Dim secondlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                        For Each child2 In level2
                                            Dim secondlvlid As String = child2.Text
                                            Dim secondlvlparams As String = child2.Tag

                                            If secondlvlparams.Contains("-Command_Text") Then
                                                edit_item_tag_pass = secondlvlparams.Split("-")
                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                            Else
                                                edit_type_pass = secondlvlparams.Remove(0, 5)
                                            End If

                                            Select Case edit_type_pass.Trim()
                                                Case "Option"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Option" + """" + secondlvlid + """" + vbCrLf, True)
                                                Case "Title"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Title " + """" + secondlvlid + """" + vbCrLf, True)
                                                Case "Badge"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "LockedOption" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "{" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "DisplayName " + """" + secondlvlid + """" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                Case "Command"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Option" + """" + secondlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                Case "Divider"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Divider" + vbCrLf, True)
                                                Case "Menu"
                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + secondlvlid + """" + " {" + vbCrLf, True)

                                                    If child.Nodes.Count > 0 Then
                                                        Dim level3 As TreeNodeCollection
                                                        level3 = child2.Nodes
                                                        Dim child3 As TreeNode

                                                        Dim thirdlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                        For Each child3 In level3
                                                            Dim thirdlvlid As String = child3.Text
                                                            Dim thirdlvlparams As String = child3.Tag

                                                            If thirdlvlparams.Contains("-Command_Text") Then
                                                                edit_item_tag_pass = thirdlvlparams.Split("-")
                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                            Else
                                                                edit_type_pass = thirdlvlparams.Remove(0, 5)
                                                            End If

                                                            Select Case edit_type_pass.Trim()
                                                                Case "Option"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Option" + """" + thirdlvlid + """" + vbCrLf, True)
                                                                Case "Title"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Title " + """" + thirdlvlid + """" + vbCrLf, True)
                                                                Case "Badge"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "LockedOption" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "{" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "DisplayName " + """" + thirdlvlid + """" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                Case "Command"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Option" + """" + thirdlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                Case "Divider"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Divider" + vbCrLf, True)
                                                                Case "Menu"
                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + thirdlvlid + """" + " {" + vbCrLf, True)

                                                                    If child.Nodes.Count > 0 Then
                                                                        Dim level4 As TreeNodeCollection
                                                                        level4 = child3.Nodes
                                                                        Dim child4 As TreeNode

                                                                        Dim fourthlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                                        For Each child4 In level4
                                                                            Dim fourthlvlid As String = child4.Text
                                                                            Dim fourthlvlparams As String = child4.Tag

                                                                            If fourthlvlparams.Contains("-Command_Text") Then
                                                                                edit_item_tag_pass = fourthlvlparams.Split("-")
                                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                                            Else
                                                                                edit_type_pass = fourthlvlparams.Remove(0, 5)
                                                                            End If

                                                                            Select Case edit_type_pass.Trim()
                                                                                Case "Option"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Option" + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                Case "Title"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Title " + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                Case "Badge"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "LockedOption" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "{" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "DisplayName " + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                                Case "Command"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Option" + """" + fourthlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                                Case "Divider"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Divider" + vbCrLf, True)
                                                                                Case "Menu"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Menu" + """" + fourthlvlid + """" + " {" + vbCrLf, True)

                                                                                    If child.Nodes.Count > 0 Then
                                                                                        Dim level5 As TreeNodeCollection
                                                                                        level5 = child4.Nodes
                                                                                        Dim child5 As TreeNode

                                                                                        Dim fifthlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                                                        For Each child5 In level5
                                                                                            Dim fifthlvlid As String = child5.Text
                                                                                            Dim fifthlvlparams As String = child5.Tag

                                                                                            If fifthlvlparams.Contains("-Command_Text") Then
                                                                                                edit_item_tag_pass = fifthlvlparams.Split("-")
                                                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                                                            Else
                                                                                                edit_type_pass = fifthlvlparams.Remove(0, 5)
                                                                                            End If

                                                                                            Select Case edit_type_pass.Trim()
                                                                                                Case "Option"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Option" + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                Case "Title"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Title " + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                Case "Badge"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "LockedOption" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "{" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "DisplayName " + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                                                Case "Command"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Option" + """" + fifthlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                                                Case "Divider"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Divider" + vbCrLf, True)

                                                                                                Case Else
                                                                                            End Select

                                                                                        Next 'end level 5 loop
                                                                                    End If 'end fifth level if child exists
                                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                                                                                Case Else
                                                                            End Select


                                                                        Next 'end level 4 loop
                                                                    End If 'end fourth level if child exists
                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)

                                                                Case Else
                                                            End Select


                                                        Next 'end level 3 loop
                                                    End If 'end third level if child count
                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)

                                                Case Else
                                            End Select


                                        Next 'end level 2 loop
                                    End If 'end level 2 if child count
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                                Case Else
                            End Select

                        Next 'end level 1 loop

                    End If 'end rootchild count
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                Case Else
            End Select

        Next 'end root loop
        My.Computer.FileSystem.WriteAllText(versionline, "}" + vbCrLf, True)
    End Sub

    Private Sub ExportGameFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportGameFileToolStripMenuItem.Click
        'menu strip item to export game file, like the button, really should consolidat ethis into one calle dfunction for both button and menustrip
        If poptree.Nodes.Count = 0 Then
            Dim Msg, Style, Title, Response  ' Help, Ctxt, , MyString
            Msg = "You do not have a menu loaded." + vbCrLf + "Would you like to load a menu from a save file now?" ' Define message.
            Style = vbYesNo + vbCritical + vbDefaultButton2    ' Define buttons.
            Title = "Popmenu Generator Warning!"    ' Define title.
            ' Help = "DEMO.HLP"    ' Define Help file.
            ' Ctxt = 1000    ' Define topic context. 
            ' Display message.
            Response = MsgBox(Msg, Style, Title) 'Help,  Ctxt
            If Response = vbNo Then    ' User chose No.
                ' MyString = "Yes"    ' Perform some action.
                Exit Sub
            Else    ' User chose Yes.
                poptree.Nodes.Clear()

                Dim fd As OpenFileDialog = New OpenFileDialog()
                Dim strPathdialogue As String


                fd.Title = "Open a Save File"
                fd.Filter = "XML File|*.xml|Text|*.txt|All files|*.*"
                fd.FilterIndex = 1
                fd.RestoreDirectory = True

                If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    strPathdialogue = fd.FileName


                    'Dim doc As XmlReader = New XmlNodeReader(strPathdialogue)
                    Dim document As XmlReader = New XmlTextReader(strPathdialogue)
                    'create the xml reader
                    'loop through the xml file
                    While (document.Read())

                        Dim type = document.NodeType

                        'if node type was element
                        If (type = XmlNodeType.Element) Then
                            If document.Name = "Root" Then
                                menu_name_txtbox.Text = document.GetAttribute("menu_name").ToString
                                written_by_txtbox.Text = document.GetAttribute("written_By").ToString
                                version_num_txtbox.Text = document.GetAttribute("version_number").ToString
                            End If
                            If document.Name = "RootItem" Then
                                Dim rootnodeid As String = document.GetAttribute("id").ToString
                                Dim rootnodeparams As String = document.GetAttribute("Parameters").ToString
                                Dim rootnodeorder As Integer = document.GetAttribute("Root_Order")
                                Dim rootnodelevel As Integer = document.GetAttribute("node_level")
                                Dim rootpath As String = document.GetAttribute("Path").ToString


                                rootnode = poptree.Nodes.Add(rootnodeid)
                                rootnode.Tag = rootnodeparams

                            End If

                            If document.Name = "FirstLevelItem" Then
                                Dim firstlvlid As String = document.GetAttribute("id").ToString
                                Dim firstlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfirst = rootnode.Nodes.Add(firstlvlid)
                                childfirst.Tag = firstlvlparams
                            End If

                            If document.Name = "SecondLevelItem" Then
                                Dim secondlvlid As String = document.GetAttribute("id").ToString
                                Dim secondlvlparams As String = document.GetAttribute("Parameters").ToString

                                childsecond = childfirst.Nodes.Add(secondlvlid)
                                childsecond.Tag = secondlvlparams
                            End If

                            If document.Name = "ThirdLevelItem" Then
                                Dim thirdlvlid As String = document.GetAttribute("id").ToString
                                Dim thirdlvlparams As String = document.GetAttribute("Parameters").ToString

                                childthird = childsecond.Nodes.Add(thirdlvlid)
                                childthird.Tag = thirdlvlparams
                            End If

                            If document.Name = "FourthLevelItem" Then
                                Dim fourthlvlid As String = document.GetAttribute("id").ToString
                                Dim fourthlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfourth = childthird.Nodes.Add(fourthlvlid)
                                childfourth.Tag = fourthlvlparams
                            End If

                            If document.Name = "FifthLevelItem" Then
                                Dim fifthlvlid As String = document.GetAttribute("id").ToString
                                Dim fifthlvlparams As String = document.GetAttribute("Parameters").ToString

                                childfifth = childfourth.Nodes.Add(fifthlvlid)
                                childfifth.Tag = fifthlvlparams
                            End If
                        End If



                    End While
                    document.Close()
                End If

                poptree.ExpandAll()


            End If 'end message box
        End If 'end message box trigger

        Dim menuname As String = menu_name_txtbox.Text
        Dim written_by As String = written_by_txtbox.Text
        Dim version_number As String = version_num_txtbox.Text


        Dim file = System.IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu")
        file.Close()

        Dim versionline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " " + menuname + ".mnu" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " Written By " + written_by + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + " Version " + version_number + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "//" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "Menu" + " """ + menuname + """" + vbCrLf, True)
        My.Computer.FileSystem.WriteAllText(versionline, "{" + vbCrLf, True)

        Dim nodes As TreeNodeCollection
        nodes = poptree.Nodes
        Dim root As TreeNode
        For Each root In nodes


            Dim rootlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"

            Dim rootlvlid As String = root.Text
            Dim rootlvlparams As String = root.Tag

            If rootlvlparams.Contains("-Command_Text") Then
                edit_item_tag_pass = rootlvlparams.Split("-")
                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
            Else
                edit_type_pass = rootlvlparams.Remove(0, 5)
            End If

            Select Case edit_type_pass.Trim()
                Case "Option"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + rootlvlid + """" + vbCrLf, True)
                Case "Title"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Title " + """" + rootlvlid + """" + vbCrLf, True)
                Case "Badge"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "LockedOption" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "{" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "DisplayName " + """" + rootlvlid + """" + vbCrLf, True)
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                Case "Command"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + rootlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                Case "Divider"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Divider" + vbCrLf, True)
                Case "Menu"
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + rootlvlid + """" + " {" + vbCrLf, True)
                    If root.Nodes.Count > 0 Then
                        Dim level1 As TreeNodeCollection
                        level1 = root.Nodes
                        Dim child As TreeNode

                        Dim firstlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                        For Each child In level1
                            Dim firstlvlid As String = child.Text
                            Dim firstlvlparams As String = child.Tag

                            If firstlvlparams.Contains("-Command_Text") Then
                                edit_item_tag_pass = firstlvlparams.Split("-")
                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                            Else
                                edit_type_pass = firstlvlparams.Remove(0, 5)
                            End If

                            Select Case edit_type_pass.Trim()
                                Case "Option"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Option" + """" + firstlvlid + """" + vbCrLf, True)
                                Case "Title"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Title " + """" + firstlvlid + """" + vbCrLf, True)
                                Case "Badge"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "LockedOption" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "{" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "DisplayName " + """" + firstlvlid + """" + vbCrLf, True)
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                Case "Command"
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Option" + """" + firstlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                Case "Divider"
                                    My.Computer.FileSystem.WriteAllText(firstlvlline, "Divider" + vbCrLf, True)
                                Case "Menu"
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + firstlvlid + """" + " {" + vbCrLf, True)

                                    If child.Nodes.Count > 0 Then
                                        Dim level2 As TreeNodeCollection
                                        level2 = child.Nodes
                                        Dim child2 As TreeNode

                                        Dim secondlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                        For Each child2 In level2
                                            Dim secondlvlid As String = child2.Text
                                            Dim secondlvlparams As String = child2.Tag

                                            If secondlvlparams.Contains("-Command_Text") Then
                                                edit_item_tag_pass = secondlvlparams.Split("-")
                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                            Else
                                                edit_type_pass = secondlvlparams.Remove(0, 5)
                                            End If

                                            Select Case edit_type_pass.Trim()
                                                Case "Option"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Option" + """" + secondlvlid + """" + vbCrLf, True)
                                                Case "Title"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Title " + """" + secondlvlid + """" + vbCrLf, True)
                                                Case "Badge"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "LockedOption" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "{" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "DisplayName " + """" + secondlvlid + """" + vbCrLf, True)
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                Case "Command"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Option" + """" + secondlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                Case "Divider"
                                                    My.Computer.FileSystem.WriteAllText(secondlvlline, "Divider" + vbCrLf, True)
                                                Case "Menu"
                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + secondlvlid + """" + " {" + vbCrLf, True)

                                                    If child.Nodes.Count > 0 Then
                                                        Dim level3 As TreeNodeCollection
                                                        level3 = child2.Nodes
                                                        Dim child3 As TreeNode

                                                        Dim thirdlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                        For Each child3 In level3
                                                            Dim thirdlvlid As String = child3.Text
                                                            Dim thirdlvlparams As String = child3.Tag

                                                            If thirdlvlparams.Contains("-Command_Text") Then
                                                                edit_item_tag_pass = thirdlvlparams.Split("-")
                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                            Else
                                                                edit_type_pass = thirdlvlparams.Remove(0, 5)
                                                            End If

                                                            Select Case edit_type_pass.Trim()
                                                                Case "Option"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Option" + """" + thirdlvlid + """" + vbCrLf, True)
                                                                Case "Title"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Title " + """" + thirdlvlid + """" + vbCrLf, True)
                                                                Case "Badge"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "LockedOption" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "{" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "DisplayName " + """" + thirdlvlid + """" + vbCrLf, True)
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                Case "Command"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Option" + """" + thirdlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                Case "Divider"
                                                                    My.Computer.FileSystem.WriteAllText(thirdlvlline, "Divider" + vbCrLf, True)
                                                                Case "Menu"
                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "Menu" + """" + thirdlvlid + """" + " {" + vbCrLf, True)

                                                                    If child.Nodes.Count > 0 Then
                                                                        Dim level4 As TreeNodeCollection
                                                                        level4 = child3.Nodes
                                                                        Dim child4 As TreeNode

                                                                        Dim fourthlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                                        For Each child4 In level4
                                                                            Dim fourthlvlid As String = child4.Text
                                                                            Dim fourthlvlparams As String = child4.Tag

                                                                            If fourthlvlparams.Contains("-Command_Text") Then
                                                                                edit_item_tag_pass = fourthlvlparams.Split("-")
                                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                                            Else
                                                                                edit_type_pass = fourthlvlparams.Remove(0, 5)
                                                                            End If

                                                                            Select Case edit_type_pass.Trim()
                                                                                Case "Option"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Option" + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                Case "Title"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Title " + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                Case "Badge"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "LockedOption" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "{" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "DisplayName " + """" + fourthlvlid + """" + vbCrLf, True)
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                                Case "Command"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Option" + """" + fourthlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                                Case "Divider"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Divider" + vbCrLf, True)
                                                                                Case "Menu"
                                                                                    My.Computer.FileSystem.WriteAllText(fourthlvlline, "Menu" + """" + fourthlvlid + """" + " {" + vbCrLf, True)

                                                                                    If child.Nodes.Count > 0 Then
                                                                                        Dim level5 As TreeNodeCollection
                                                                                        level5 = child4.Nodes
                                                                                        Dim child5 As TreeNode

                                                                                        Dim fifthlvlline = System.AppDomain.CurrentDomain.BaseDirectory() + "export_files\" + menuname + ".mnu"


                                                                                        For Each child5 In level5
                                                                                            Dim fifthlvlid As String = child5.Text
                                                                                            Dim fifthlvlparams As String = child5.Tag

                                                                                            If fifthlvlparams.Contains("-Command_Text") Then
                                                                                                edit_item_tag_pass = fifthlvlparams.Split("-")
                                                                                                edit_type_pass = edit_item_tag_pass(1).Remove(0, 5)
                                                                                                edit_command_pass = edit_item_tag_pass(2).Remove(0, 13)
                                                                                            Else
                                                                                                edit_type_pass = fifthlvlparams.Remove(0, 5)
                                                                                            End If

                                                                                            Select Case edit_type_pass.Trim()
                                                                                                Case "Option"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Option" + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                Case "Title"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Title " + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                Case "Badge"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "LockedOption" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "{" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "DisplayName " + """" + fifthlvlid + """" + vbCrLf, True)
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Badge " + edit_command_pass + " }" + vbCrLf, True)
                                                                                                Case "Command"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Option" + """" + fifthlvlid + """" + " <&" + edit_command_pass + "&>" + vbCrLf, True)
                                                                                                Case "Divider"
                                                                                                    My.Computer.FileSystem.WriteAllText(fifthlvlline, "Divider" + vbCrLf, True)

                                                                                                Case Else
                                                                                            End Select

                                                                                        Next 'end level 5 loop
                                                                                    End If 'end fifth level if child exists
                                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                                                                                Case Else
                                                                            End Select


                                                                        Next 'end level 4 loop
                                                                    End If 'end fourth level if child exists
                                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)

                                                                Case Else
                                                            End Select


                                                        Next 'end level 3 loop
                                                    End If 'end third level if child count
                                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)

                                                Case Else
                                            End Select


                                        Next 'end level 2 loop
                                    End If 'end level 2 if child count
                                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                                Case Else
                            End Select

                        Next 'end level 1 loop

                    End If 'end rootchild count
                    My.Computer.FileSystem.WriteAllText(rootlvlline, "}" + vbCrLf, True)
                Case Else
            End Select

        Next 'end root loop
        My.Computer.FileSystem.WriteAllText(versionline, "}" + vbCrLf, True)
    End Sub

    Private Sub move_up_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles move_up_btn.Click
        'handles reorganizing nodes in tree view control
        Dim parentnode As TreeNode = poptree.SelectedNode.Parent
        Dim originalIndex As Integer = poptree.SelectedNode.Index
        Dim originaltext As String = poptree.SelectedNode.Text
        Dim originaltag As String = poptree.SelectedNode.Tag

        If originalIndex = 0 Then

        Else
            Dim clonednode As TreeNode = poptree.SelectedNode.Clone
            poptree.SelectedNode.Remove()
            Dim newindex As Integer = originalIndex - 1
            parentnode.Nodes.Insert(newindex, originaltext)
            'parentnode.TreeView.SelectedNode = clonednode
        End If

    End Sub
End Class
