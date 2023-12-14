Imports MySql.Data.MySqlClient

Public Class Form7
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)

    Private WithEvents slotStatusTimer As New Timer()

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentLogs()
        LoadParkingTally()

        slotStatusTimer.Start()
        slotStatusTimer.Interval = 30000

    End Sub

    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Stop the timer when the form is closing
        slotStatusTimer.Stop()
        slotStatusTimer.Dispose() ' Optionally, dispose of the timer
        Module1.CloseAllOtherForms(Me)
        conn.Close()

    End Sub

    Private Sub slotStatusTimer_Tick(sender As Object, e As EventArgs) Handles slotStatusTimer.Tick
        ' This event will be triggered every second (as per the timer interval)
        LoadPaymentLogs()
        LoadParkingTally()
    End Sub

    Private Sub LoadPaymentLogs()
        Dim query As String = "SELECT recieptNo, slotNo, date FROM payment"
        Using connection As New MySqlConnection(connString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    DataGridView1.DataSource = dataTable
                Catch ex As Exception
                    MessageBox.Show("Error loading payment logs: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadParkingTally()
        Dim query As String = "SELECT date, COUNT(*) AS parkings FROM payment GROUP BY date"
        Using connection As New MySqlConnection(connString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Dim adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    DataGridView2.DataSource = dataTable
                Catch ex As Exception
                    MessageBox.Show("Error loading parking tally from payment table: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form4.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form5.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form6.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class
