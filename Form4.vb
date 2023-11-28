﻿Imports MySql.Data.MySqlClient
Public Class Form4
    Dim connString As String = "Server=localhost;Port=3306;Database=parkingsystem;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(connString)
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form5.Show()
    End Sub
End Class