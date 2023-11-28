Imports MySql.Data.MySqlClient

Public Class Form1
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Dim selectedSlotNumber As Integer = -1 ' Initialize with an invalid slot number

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                        Else
                            slotButton.BackColor = Color.Red
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


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub PrintTicket(slotNumber As Integer, currentDate As DateTime)
        Try
            conn.Open()
            Dim query As String = "INSERT INTO `payment` (`slotNo`, `date`) VALUES (@slotNo, @date)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@slotNo", slotNumber)
                cmd.Parameters.AddWithValue("@date", currentDate)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub UpdateSlotAndPrintTicket(slotNumber As Integer, newStatus As Integer)
        ' Update the status in the database
        UpdateSlotStatus(slotNumber, newStatus)

        ' Get the current date
        Dim currentDate As DateTime = DateTime.Now

        ' Print ticket by adding current date and slot number to the database
        PrintTicket(slotNumber, currentDate)
    End Sub

    Private Sub slot_Click(sender As Object, e As EventArgs) Handles slot1.Click, slot2.Click, slot3.Click, slot4.Click, slot5.Click, slot6.Click,
        slot7.Click, slot8.Click, slot9.Click, slot10.Click, slot11.Click, slot12.Click, slot13.Click, slot14.Click, slot15.Click,
        slot16.Click, slot17.Click, slot18.Click, slot19.Click, slot20.Click, slot21.Click, slot22.Click, slot23.Click,
        slot24.Click, slot25.Click, slot26.Click, slot27.Click, slot28.Click, slot29.Click, slot30.Click, slot31.Click,
        slot32.Click, slot33.Click, slot34.Click, slot35.Click, slot36.Click
        ' Handle slot click event
        Dim clickedSlot As Button = DirectCast(sender, Button)
        Dim slotNumber As Integer = Integer.Parse(clickedSlot.Text)

        If clickedSlot.BackColor = Color.Red Then

            Dim confirmPayment As DialogResult = MessageBox.Show("Has payment been made for this slot?", "Confirm Payment", MessageBoxButtons.YesNo)

            If confirmPayment = DialogResult.Yes Then
                UpdateSlotStatus(slotNumber, 1) ' Update slot status to available
                clickedSlot.BackColor = Color.Lime
                UpdateAvailableSlotsCount()
            End If
        ElseIf clickedSlot.BackColor = Color.Lime Then
            clickedSlot.BackColor = Color.Red
            selectedSlotNumber = Integer.Parse(clickedSlot.Text)
            UpdateAvailableSlotsCount()
        End If

        For Each frm As Form In Application.OpenForms
            If frm.Name = "Form5" Then
                Dim form5 As Form5 = DirectCast(frm, Form5)
                Dim slotButton As Button = DirectCast(form5.Controls("slot" & slotNumber), Button)
                If clickedSlot.BackColor = Color.Red Then
                    slotButton.BackColor = Color.Red ' Update Form1's UI color to Orange
                ElseIf clickedSlot.BackColor = Color.Lime Then
                    slotButton.BackColor = Color.Lime ' Update Form1's UI color to Red
                End If
            End If
        Next frm
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

    Private Sub btnPrintTicket_Click(sender As Object, e As EventArgs) Handles btnPrintTicket.Click
        ' Handle print ticket button click
        If selectedSlotNumber <> -1 Then ' Check if a valid slot is selected
            Dim confirmationMsg As String = $"Receipt No.: {GenerateReceiptNumber()} {Environment.NewLine}" &
                                       $"Slot No.: {selectedSlotNumber} {Environment.NewLine}" &
                                       $"Date: {DateTime.Now.ToString()} {Environment.NewLine}" &
                                       $"Parking Price: 50Php {Environment.NewLine}" &
                                       $"Confirm printing this ticket?"

            If MessageBox.Show(confirmationMsg, "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                ' Update the status in the database and print ticket only for the selected slot
                UpdateSlotAndPrintTicket(selectedSlotNumber, If(DirectCast(Controls("slot" & selectedSlotNumber), Button).BackColor = Color.Lime, 1, 0))
                UpdateAvailableSlotsCount()
            End If
        Else
            MessageBox.Show("Please select a slot before printing.")
        End If
    End Sub

    Private Function GenerateReceiptNumber() As String
        ' Generate receipt number logic here (e.g., based on date/time or any specific pattern)
        ' Return a unique receipt number
        Return Guid.NewGuid().ToString().Substring(0, 8) ' Example: Using a substring of GUID
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()
    End Sub

End Class
