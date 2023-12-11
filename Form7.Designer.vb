<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form7
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form7))
        DataGridView1 = New DataGridView()
        DataGridView2 = New DataGridView()
        Button3 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        Button4 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(37, 132)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(336, 257)
        DataGridView1.TabIndex = 0
        ' 
        ' DataGridView2
        ' 
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(425, 132)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.Size = New Size(336, 257)
        DataGridView2.TabIndex = 1
        ' 
        ' Button3
        ' 
        Button3.BackColor = Color.FromArgb(CByte(71), CByte(56), CByte(169))
        Button3.ForeColor = Color.White
        Button3.Location = New Point(196, 407)
        Button3.Name = "Button3"
        Button3.Size = New Size(138, 23)
        Button3.TabIndex = 35
        Button3.Text = "Account Management"
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.FromArgb(CByte(71), CByte(56), CByte(169))
        Button2.ForeColor = Color.White
        Button2.Location = New Point(108, 407)
        Button2.Name = "Button2"
        Button2.Size = New Size(84, 23)
        Button2.TabIndex = 34
        Button2.Text = "Maintenance"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(71), CByte(56), CByte(169))
        Button1.ForeColor = Color.White
        Button1.Location = New Point(18, 407)
        Button1.Name = "Button1"
        Button1.Size = New Size(84, 23)
        Button1.TabIndex = 33
        Button1.Text = "Catalog"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.FromArgb(CByte(71), CByte(56), CByte(169))
        Button4.ForeColor = Color.White
        Button4.Location = New Point(713, 415)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 23)
        Button4.TabIndex = 50
        Button4.Text = "Sign Out"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(800, 450)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView2)
        Controls.Add(DataGridView1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "Form7"
        Text = "Transactions Monitoring"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
End Class
