﻿Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Windows.Win32.System

Public Class Form6
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Private selectedRowIndex As Integer = -1

    Private WithEvents slotStatusTimer As New Timer()
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        slotStatusTimer.Start()
        slotStatusTimer.Interval = 60000
    End Sub

    Private Sub Form6_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Stop the timer when the form is closing
        slotStatusTimer.Stop()
        slotStatusTimer.Dispose() ' Optionally, dispose of the timer
        Module1.CloseAllOtherForms(Me)
        conn.Close()

    End Sub

    Private Sub slotStatusTimer_Tick(sender As Object, e As EventArgs) Handles slotStatusTimer.Tick
        ' This event will be triggered every second (as per the timer interval)
        LoadUsers()
    End Sub


    Public Sub LoadUsers()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT id, name, username, password, role FROM users", conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dataTable As New DataTable()

            adapter.Fill(dataTable)

            ' Set DataGridView properties to not generate columns automatically
            DataGridView1.AutoGenerateColumns = False

            ' Bind specific columns in the DataTable to columns in the DataGridView
            DataGridView1.Columns("Column1").DataPropertyName = "id"
            DataGridView1.Columns("Column2").DataPropertyName = "name"
            DataGridView1.Columns("Column3").DataPropertyName = "username"
            DataGridView1.Columns("Column4").DataPropertyName = "password"
            DataGridView1.Columns("Column5").DataPropertyName = "role"

            ' Assign the DataTable as the DataGridView's DataSource
            DataGridView1.DataSource = dataTable
        Catch ex As Exception

            Application.Exit()
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function EncryptPassword(password As String) As String
        ' Perform encryption logic here (e.g., hashing algorithm)
        ' For example, you can use SHA256 hashing
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To hashedBytes.Length - 1
                builder.Append(hashedBytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub InsertUser()
        Dim name As String = TextBox1.Text
        Dim username As String = TextBox2.Text
        Dim role As String = ComboBox1.SelectedItem?.ToString()

        ' Check if any of the textboxes are empty
        If String.IsNullOrWhiteSpace(name) OrElse String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) OrElse String.IsNullOrWhiteSpace(role) Then
            MessageBox.Show("Please fill in all the fields.")
            Return ' Exit the method if any textbox is empty
        End If

        Dim password As String = EncryptPassword(TextBox3.Text) ' Encrypt the password before storing

        Dim query As String = "INSERT INTO users (name, username, password, role) VALUES (@name, @username, @password, @role)"

        Using connection As New MySqlConnection(connString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@name", name)
                command.Parameters.AddWithValue("@username", username)
                command.Parameters.AddWithValue("@password", password) ' Store the encrypted password
                command.Parameters.AddWithValue("@role", role)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("New user added successfully.")
    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Get the selected row index
        If e.RowIndex >= 0 Then
            selectedRowIndex = e.RowIndex

            ' Populate TextBoxes with data from the selected row
            TextBox1.Text = DataGridView1.Rows(selectedRowIndex).Cells("Column2").Value.ToString()
            TextBox2.Text = DataGridView1.Rows(selectedRowIndex).Cells("Column3").Value.ToString()
            TextBox3.Text = DataGridView1.Rows(selectedRowIndex).Cells("Column4").Value.ToString()
            ComboBox1.SelectedItem = DataGridView1.Rows(selectedRowIndex).Cells("Column5").Value.ToString()
        End If
    End Sub

    Private Sub DeleteUser()
        If selectedRowIndex >= 0 Then
            Try
                conn.Open()
                Dim id As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("Column1").Value)

                ' Delete the selected user from the database
                Dim query As String = "DELETE FROM users WHERE id = @id"

                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@id", id)
                    command.ExecuteNonQuery()
                End Using

                MessageBox.Show("User deleted successfully.")
            Catch ex As Exception

                Application.Exit()
            Finally
                conn.Close()
                LoadUsers() ' Reload data in the DataGridView after deletion
            End Try
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
    End Sub

    Private Sub ClearTextBoxes()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.SelectedIndex = -1 ' Clear combobox selection
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DeleteUser()
        ClearTextBoxes()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If selectedRowIndex >= 0 Then
            Try
                conn.Open()
                Dim id As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("Column1").Value)

                ' Encrypt the new password before updating the database
                Dim newPassword As String = EncryptPassword(TextBox3.Text)

                ' Update the database with the edited values
                Dim query As String = "UPDATE users SET name = @name, username = @username, password = @password, role = @role WHERE id = @id"

                Using command As New MySqlCommand(query, conn)
                    command.Parameters.AddWithValue("@name", TextBox1.Text)
                    command.Parameters.AddWithValue("@username", TextBox2.Text)
                    command.Parameters.AddWithValue("@password", newPassword) ' Store the encrypted password
                    command.Parameters.AddWithValue("@role", ComboBox1.SelectedItem.ToString())
                    command.Parameters.AddWithValue("@id", id)

                    command.ExecuteNonQuery()
                End Using

                MessageBox.Show("Data updated successfully.")
            Catch ex As Exception

                Application.Exit()
            Finally
                conn.Close()
                LoadUsers() ' Reload data in the DataGridView after update
                ClearTextBoxes()
            End Try
        Else
            MessageBox.Show("Please select a row to update.")
        End If
    End Sub


    Private Sub button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        InsertUser()
        LoadUsers()
        ClearTextBoxes()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Form4.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Form7.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Form5.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class