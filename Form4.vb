Imports MySql.Data.MySqlClient
Public Class Form4
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)

    Private WithEvents slotStatusTimer As New Timer()

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateAvailableSlotsCount()

        slotStatusTimer.Start()
        slotStatusTimer.Interval = 1000
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Stop the timer when the form is closing
        slotStatusTimer.Stop()
        slotStatusTimer.Dispose() ' Optionally, dispose of the timer
        Module1.CloseAllOtherForms(Me)
        conn.Close()

    End Sub


    Private Sub slotStatusTimer_Tick(sender As Object, e As EventArgs) Handles slotStatusTimer.Tick
        ' This event will be triggered every second (as per the timer interval)
        UpdateAvailableSlotsCount()
    End Sub

    Public Sub UpdateAvailableSlotsCount()
        Try
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S1-12';"
            Using cmd As New MySqlCommand(query, conn)
                Dim availableSlotsCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                slotslabel.Text = $"{availableSlotsCount} SLOTS" ' Update the label text
            End Using

            ' Query for the slots in structure F1S13-24
            Dim queryF1S13_24 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S13-24';"
            Using cmd As New MySqlCommand(queryF1S13_24, conn)
                Dim slotsCountF1S20_25 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1S20-25
                slotslabel2.Text = $"{slotsCountF1S20_25} SLOTS"
            End Using

            ' Query for the slots in structure F1S25-36
            Dim queryF1S25_36 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S25-36';"
            Using cmd As New MySqlCommand(queryF1S25_36, conn)
                Dim slotsCountF1S20_25 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1S20-25
                slotslabel3.Text = $"{slotsCountF1S20_25} SLOTS"
            End Using

            ' Query for the slots in structure F1M37-41 
            Dim queryF1M37_41 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1M37-41';"
            Using cmd As New MySqlCommand(queryF1M37_41, conn)
                Dim slotsCountF1M37_41 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1M37-41
                slotslabel4.Text = $"{slotsCountF1M37_41} SLOTS"
            End Using

            ' Query for the slots in structure F1M42-46
            Dim queryF1M42_46 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1M42-46';"
            Using cmd As New MySqlCommand(queryF1M42_46, conn)
                Dim slotsCountF1M42_46 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1M42-46
                slotslabel5.Text = $"{slotsCountF1M42_46} SLOTS"
            End Using

        Catch ex As Exception

            Application.Exit()
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form5.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form6.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form7.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class