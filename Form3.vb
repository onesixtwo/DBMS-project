Imports MySql.Data.MySqlClient
Imports System.Diagnostics.Eventing
Imports System.Security.Cryptography
Imports System.Text

Public Class Form3
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Module1.CloseAllOtherForms(Me)
        conn.Close()

    End Sub


    Private Function EncryptPassword(password As String) As String
        ' Encryption logic here (e.g., hashing algorithm)
        ' For example, using SHA256 hashing
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To hashedBytes.Length - 1
                builder.Append(hashedBytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text.Trim()
        Dim enteredPassword As String = EncryptPassword(TextBox2.Text.Trim()) ' Encrypt the entered password

        ' Query to check user credentials in the database
        Dim query As String = "SELECT role, Name, password FROM users WHERE username = @username"

        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", username)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim storedPassword As String = reader("password").ToString()

                ' Log encrypted passwords for comparison
                Console.WriteLine("Entered Password (Encrypted): " & enteredPassword)
                Console.WriteLine("Stored Password (Encrypted): " & storedPassword)

                ' Compare the stored encrypted password with the encrypted password entered by the user
                If storedPassword = enteredPassword Then
                    ' Login successful
                    Dim userRole As String = reader("role").ToString()
                    Dim name As String = reader("Name").ToString()

                    If userRole = "admin" Then
                        ' Admin user logged in
                        MessageBox.Show("Welcome " & name)
                        ' Show admin form
                        Form4.Show()
                        Me.Hide()
                    Else
                        ' Regular user logged in
                        MessageBox.Show("Welcome " & name)
                        ' Show regular user form
                        Form2.Show()
                        Me.Hide()
                    End If
                    TextBox1.Clear()
                    TextBox2.Clear()
                Else
                    MessageBox.Show("Invalid Password or Username ")
                End If
            Else
                MessageBox.Show("Invalid Password or Username ")
            End If

        Catch ex As Exception
            MessageBox.Show("Cant connect to the Database")
        Finally
            conn.Close()
        End Try
    End Sub
End Class