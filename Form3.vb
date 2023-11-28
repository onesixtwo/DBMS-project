Imports MySql.Data.MySqlClient
Public Class Form3
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()
        ' Query to check user credentials in the database
        Dim query As String = "SELECT role, Name FROM users WHERE username = @username AND password = @password"

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", username)
            cmd.Parameters.AddWithValue("@password", password)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim userRole As String = reader("role").ToString()
                Dim name As String = reader("Name").ToString()

                If userRole = "admin" Then
                    ' Admin user logged in
                    MessageBox.Show("Welcome " & name)
                    ' Show admin form
                    Form4.Show()
                    Form6.Show()
                Else
                    ' Regular user logged in
                    MessageBox.Show("Welcome " & name)
                    ' Show regular user form
                    Form2.Show()
                End If
            Else
                MessageBox.Show("Invalid username or password")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class