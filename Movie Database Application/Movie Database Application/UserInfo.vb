Imports System.IO
Imports System.Data.SqlClient

Public Class UserInfo
    Dim SQL As New SQLControl
    Dim seatType As String
    Dim noOfSeats As Integer
    Dim user

    Private Sub UserInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        componentInitialize()

    End Sub

    Public Sub ButtonCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim mov_id As Integer
        Dim seatLeft As Integer
        Try

            SQL.RunQuery("Select * " & _
                    "From Movie,Customer Where M_NAME ='" & sender.tag & "' and C_Name = '" & user & "'")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                    mov_id = i.item("MOV_ID")
                Next
            End If


            SQL.SQLCon.Open()
            Dim query As String = "DELETE FROM CUSTOMER WHERE C_name ='" & user & "' and MOV_ID = " & mov_id
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()

            '--------Updating no of seats to back 
            SQL.RunQuery("Select * " & _
                        "From Movie_Seats Where MOV_ID = " & mov_id & " AND SEAT_TYPE = '" & seatType & "'")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                    seatleft = i.item("Seat_Left") + noOfSeats
                Next
            End If

            SQL.SQLCon.Open()
            query = "UPDATE Movie_Seats SET SEAT_LEFT = @SEAT_LEFT WHERE MOV_ID = @MOV_ID AND SEAT_TYPE = @SEAT_TYPE"
            Dim cmd2 As New SqlCommand(query, SQL.SQLCon)
            cmd2.Parameters.AddWithValue("@SEAT_LEFT", seatleft)
            cmd2.Parameters.AddWithValue("@MOV_ID", mov_id)
            cmd2.Parameters.AddWithValue("@SEAT_TYPE", seatType)
            cmd2.ExecuteNonQuery()
            SQL.SQLCon.Close()

            MsgBox("Movie Cancelled")
            componentInitialize()

        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub componentInitialize()
        FlowLayoutPanel1.Controls.Clear()
        user = File.ReadAllText("data.txt")
        Label1.Text = user
        SQL.RunQuery("Select * " & _
                    "From Movie,Customer Where M_ID = MOV_ID and C_Name = '" & user & "'")
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows

                Dim pan As Panel = New Panel
                pan.Height = 60
                pan.Width = 822
                pan.BackColor = Color.LightGray
                pan.Parent = FlowLayoutPanel1

                Dim lab1 As Label = New Label()
                lab1.Parent = pan
                lab1.Location = New System.Drawing.Point(6, 13)
                lab1.Text = i.item("M_NAME")
                lab1.AutoSize = False
                'lab1.Size = New System.Drawing.Size(147, 41)
                lab1.Height = 41
                lab1.Width = 147
                lab1.Font = New System.Drawing.Font("Arial Rounded MT", 9)
                'lab1.ForeColor = Color.White
                'lab1.BackColor = Color.Red
                'lab1.ForeColor = Color.White

                Dim lab2 As Label = New Label()
                lab2.Parent = pan
                lab2.Location = New System.Drawing.Point(168, 13)
                lab2.Text = i.item("C_SEATS")
                noOfSeats = i.item("C_SEATS")
                lab2.AutoSize = False
                lab2.Size = New System.Drawing.Size(39, 13)


                Dim lab3 As Label = New Label()
                lab3.Parent = pan
                lab3.Location = New System.Drawing.Point(263, 13)
                lab3.Text = i.item("C_SEAT_TYPE").ToString
                seatType = i.item("C_SEAT_TYPE").ToString
                lab1.AutoSize = False
                lab1.Size = New System.Drawing.Size(67, 13)

                'lab3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                'lab3.ForeColor = Color.White

                Dim lab4 As Label = New Label()
                lab4.Parent = pan
                lab4.Location = New System.Drawing.Point(362, 13)
                lab4.Text = i.item("C_LOC").ToString
                'lab4.Size = New System.Drawing.Size(56, 16)
                'lab4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                'lab4.ForeColor = Color.White

                Dim lab5 As Label = New Label()
                lab5.Parent = pan
                lab5.Location = New System.Drawing.Point(461, 13)
                lab5.Text = i.item("C_SHOWTIME").ToString

                Dim lab6 As Label = New Label()
                lab6.Parent = pan
                lab6.Location = New System.Drawing.Point(586, 13)
                lab6.Text = i.item("C_SHOWDATE").ToString

                Dim lab7 As Label = New Label()
                lab7.Parent = pan
                lab7.AutoSize = False
                lab7.Size = New System.Drawing.Size(39, 13)
                lab7.Location = New System.Drawing.Point(694, 13)
                lab7.Text = i.item("C_TOTCOST").ToString

                If Date.Today <= i.item("C_SHOWDATE") Then
                    Dim but1 As Button = New Button()
                    but1.Parent = pan
                    but1.Location = New System.Drawing.Point(757, 4)
                    but1.Text = "C"
                    but1.Tag = lab1.Text
                    but1.BackColor = Color.Red
                    but1.FlatStyle = FlatStyle.Flat
                    but1.FlatAppearance.BorderSize = 0
                    'but1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                    but1.ForeColor = Color.White
                    but1.Size = New System.Drawing.Size(40, 22)
                    AddHandler but1.Click, AddressOf ButtonCancel
                End If
            Next
        Else
            Dim pan1 As Panel = New Panel
            pan1.Height = 100
            pan1.Width = 776
            pan1.BackColor = Color.White
            pan1.Parent = FlowLayoutPanel1

            Dim lab9 As Label = New Label()
            lab9.Parent = pan1
            lab9.Location = New System.Drawing.Point(233, 29)
            lab9.Text = "You havent booked any movies yet!!"
            lab9.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25)
            lab9.AutoSize = False
            lab9.Size = New System.Drawing.Size(300, 51)
        End If
    End Sub

End Class