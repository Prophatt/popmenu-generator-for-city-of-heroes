<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class help_about
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
        Me.about_title = New System.Windows.Forms.Label()
        Me.about_author = New System.Windows.Forms.Label()
        Me.about_close_btn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'about_title
        '
        Me.about_title.AutoSize = True
        Me.about_title.Location = New System.Drawing.Point(29, 21)
        Me.about_title.Name = "about_title"
        Me.about_title.Size = New System.Drawing.Size(117, 13)
        Me.about_title.TabIndex = 0
        Me.about_title.Text = "Popmenu Generator v1"
        '
        'about_author
        '
        Me.about_author.AutoSize = True
        Me.about_author.Location = New System.Drawing.Point(29, 46)
        Me.about_author.Name = "about_author"
        Me.about_author.Size = New System.Drawing.Size(82, 13)
        Me.about_author.TabIndex = 1
        Me.about_author.Text = "Written by Profit"
        '
        'about_close_btn
        '
        Me.about_close_btn.Location = New System.Drawing.Point(32, 76)
        Me.about_close_btn.Name = "about_close_btn"
        Me.about_close_btn.Size = New System.Drawing.Size(92, 36)
        Me.about_close_btn.TabIndex = 2
        Me.about_close_btn.Text = "Close"
        Me.about_close_btn.UseVisualStyleBackColor = True
        '
        'help_about
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(170, 135)
        Me.Controls.Add(Me.about_close_btn)
        Me.Controls.Add(Me.about_author)
        Me.Controls.Add(Me.about_title)
        Me.Name = "help_about"
        Me.Text = "About.."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents about_title As System.Windows.Forms.Label
    Friend WithEvents about_author As System.Windows.Forms.Label
    Friend WithEvents about_close_btn As System.Windows.Forms.Button
End Class
