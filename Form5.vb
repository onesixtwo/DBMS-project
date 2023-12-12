Imports MySql.Data.MySqlClient
Public Class Form5
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Dim selectedSlotNumber As Integer = -1 ' Initialize with an invalid slot number

    Private WithEvents slotStatusTimer As New Timer()
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSlotStatusFromDatabase()
        slotStatusTimer.Start()

        slotStatusTimer.Interval = 30000

    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Stop the timer when the form is closing
        slotStatusTimer.Stop()
        slotStatusTimer.Dispose() ' Optionally, dispose of the timer
        Module1.CloseAllOtherForms(Me)
    End Sub
    Private Sub slotStatusTimer_Tick(sender As Object, e As EventArgs) Handles slotStatusTimer.Tick
        ' This event will be triggered every second (as per the timer interval)
        LoadSlotStatusFromDatabase()
    End Sub
    Private Sub LoadSlotStatusFromDatabase()
        Try
            conn.Open()
            Dim query As String = "SELECT slotNo, status FROM parkslots"
            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim slotNo As Integer = Convert.ToInt32(reader("slotNo"))
                        Dim status As Integer = Convert.ToInt32(reader("status"))

                        ' Update UI based on slot status
                        Dim slotButton As Button = DirectCast(Controls("slot" & slotNo), Button)
                        If status = 1 Then
                            slotButton.BackColor = Color.Lime
                        ElseIf status = 0 Then
                            slotButton.BackColor = Color.Red
                        ElseIf status = 2 Then
                            slotButton.BackColor = Color.Orange
                        End If
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading slot status: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub UpdateAvailableSlotsCount()
        Try
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S1-12';"
            Using cmd As New MySqlCommand(query, conn)
                Dim availableSlotsCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                Form2.slotslabel.Text = $"{availableSlotsCount} SLOTS"
                Form4.slotslabel.Text = $"{availableSlotsCount} SLOTS"
            End Using

            ' Query for the slots in structure F1S13-24
            Dim queryF1S13_24 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S13-24';"
            Using cmd As New MySqlCommand(queryF1S13_24, conn)
                Dim slotsCountF1S20_25 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1S20-25
                Form2.slotslabel2.Text = $"{slotsCountF1S20_25} SLOTS"
                Form4.slotslabel2.Text = $"{slotsCountF1S20_25} SLOTS"
            End Using

            ' Query for the slots in structure F1S25-36
            Dim queryF1S25_36 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1S25-36';"
            Using cmd As New MySqlCommand(queryF1S25_36, conn)
                Dim slotsCountF1S20_25 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1S20-25
                Form2.slotslabel3.Text = $"{slotsCountF1S20_25} SLOTS"
                Form4.slotslabel3.Text = $"{slotsCountF1S20_25} SLOTS"
            End Using


            Dim queryF1M37_41 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1M37-41';"
            Using cmd As New MySqlCommand(queryF1M37_41, conn)
                Dim slotsCountF1M37_41 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1M37-41
                Form2.slotslabel3.Text = $"{slotsCountF1M37_41} SLOTS"
                Form4.slotslabel3.Text = $"{slotsCountF1M37_41} SLOTS"
            End Using


            Dim queryF1M42_46 As String = "SELECT COUNT(*) FROM `parkslots` WHERE `status` = 1 AND `structure` = 'F1M42-46';"
            Using cmd As New MySqlCommand(queryF1M42_46, conn)
                Dim slotsCountF1M42_46 As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Update the label for slots in structure F1M42-46
                Form2.slotslabel3.Text = $"{slotsCountF1M42_46} SLOTS"
                Form4.slotslabel3.Text = $"{slotsCountF1M42_46} SLOTS"
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Public Sub UpdateSlotStatus(slotNo As Integer, status As Integer)
        Try
            conn.Open()
            Dim query As String = "UPDATE `parkslots` SET `status` = @status WHERE `slotNo` = @slotNo;"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@status", status)
                cmd.Parameters.AddWithValue("@slotNo", slotNo)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Database updated successfully.")
                Else
                    MessageBox.Show("No rows were updated.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub slot_Click(sender As Object, e As EventArgs) Handles slot1.Click, slot2.Click, slot3.Click, slot4.Click, slot5.Click, slot6.Click,
    slot7.Click, slot8.Click, slot9.Click, slot10.Click, slot11.Click, slot12.Click, slot13.Click, slot14.Click, slot15.Click,
    slot16.Click, slot17.Click, slot18.Click, slot19.Click, slot20.Click, slot21.Click, slot22.Click, slot23.Click,
    slot24.Click, slot25.Click, slot26.Click, slot27.Click, slot28.Click, slot29.Click, slot30.Click, slot31.Click,
    slot32.Click, slot33.Click, slot34.Click, slot35.Click, slot36.Click, slot37.Click, slot38.Click, slot39.Click,
    slot40.Click, slot41.Click, slot42.Click, slot43.Click, slot44.Click, slot45.Click, slot46.Click
        ' Handle slot click event
        Dim clickedSlot As Button = DirectCast(sender, Button)
        Dim slotNumber As Integer = Integer.Parse(clickedSlot.Text)

        If clickedSlot.BackColor = Color.Lime OrElse clickedSlot.BackColor = Color.Red Then
            Dim slotmaintenance As DialogResult = MessageBox.Show("Is this slot under maintenance", "SLOT UNDER MAINTENANCE", MessageBoxButtons.YesNo)

            If slotmaintenance = DialogResult.Yes Then
                UpdateSlotStatus(slotNumber, 2) ' Update slot status to 2 (Orange) in the database
                clickedSlot.BackColor = Color.Orange ' Update UI color to Orange
                UpdateAvailableSlotsCount() ' Update slot counts
            End If
        ElseIf clickedSlot.BackColor = Color.Lime Then
            clickedSlot.BackColor = Color.Orange
            selectedSlotNumber = slotNumber
            UpdateAvailableSlotsCount()
        ElseIf clickedSlot.BackColor = Color.Orange Then
            clickedSlot.BackColor = Color.Lime
            selectedSlotNumber = -1 ' No slot selected
            UpdateSlotStatus(slotNumber, 1) ' Update slot status to 1 (Red) in the database
            UpdateAvailableSlotsCount()
        End If

        ' Reflect changes in Form1 as well
        For Each frm As Form In Application.OpenForms
            If frm.Name = "Form1" Then
                Dim form1 As Form1 = DirectCast(frm, Form1)
                Dim slotButton As Button = DirectCast(form1.Controls("slot" & slotNumber), Button)
                If clickedSlot.BackColor = Color.Orange Then
                    slotButton.BackColor = Color.Orange ' Update Form1's UI color to Orange
                ElseIf clickedSlot.BackColor = Color.Lime Then
                    slotButton.BackColor = Color.Lime ' Update Form1's UI color to Red
                End If
            End If
        Next frm
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form7.Show()
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