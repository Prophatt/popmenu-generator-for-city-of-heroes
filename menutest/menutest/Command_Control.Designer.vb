<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Command_Control
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Command_Control))
        Me.cmd_lbl = New System.Windows.Forms.Label()
        Me.cmd_category_cmbbx = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cmd_lbl
        '
        Me.cmd_lbl.AutoSize = True
        Me.cmd_lbl.Location = New System.Drawing.Point(12, 16)
        Me.cmd_lbl.Name = "cmd_lbl"
        Me.cmd_lbl.Size = New System.Drawing.Size(107, 13)
        Me.cmd_lbl.TabIndex = 0
        Me.cmd_lbl.Text = "Command Categories"
        '
        'cmd_category_cmbbx
        '
        Me.cmd_category_cmbbx.FormattingEnabled = True
        Me.cmd_category_cmbbx.Items.AddRange(New Object() {"Chat", "Powers", "PvP", "Team", "Supergroup", "Interfaces"})
        Me.cmd_category_cmbbx.Location = New System.Drawing.Point(15, 32)
        Me.cmd_category_cmbbx.Name = "cmd_category_cmbbx"
        Me.cmd_category_cmbbx.Size = New System.Drawing.Size(104, 21)
        Me.cmd_category_cmbbx.TabIndex = 1
        '
        'Command_Control
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 476)
        Me.Controls.Add(Me.cmd_category_cmbbx)
        Me.Controls.Add(Me.cmd_lbl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Command_Control"
        Me.Text = "Add Command Control"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmd_lbl As System.Windows.Forms.Label
    Friend WithEvents cmd_category_cmbbx As System.Windows.Forms.ComboBox
End Class
