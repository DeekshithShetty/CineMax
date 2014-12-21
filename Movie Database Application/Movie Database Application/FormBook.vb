Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class FormBook
    Public noOfSeats As String
    Public showTimings As String
    Public showDate As String
    Public loc As String
    Public seatCost As Decimal
    Public username = FormHom2.username
    Public cid As Integer
    Public randomCode As String
    Public seatType As String
    Public movieId As Integer

    Dim SQL As New SQLControl

    Private Sub FormBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label1.Text = FormHom2.moviename
        Label13.Text = username

        Dim date1 As String = Date.Today
        Dim date2 As String = Date.Today.AddDays(1)
        Dim arr As String() = {date1, date2}

        SQL.RunQuery("Select * " & _
                    "From Movie where M_NAME = '" & FormHom2.moviename & "'")
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows

                Dim imageData As Byte() = DirectCast(i.item("M_IMG"), Byte())
                If Not imageData Is Nothing Then
                    Using ms As New MemoryStream(imageData, 0, imageData.Length)
                        ms.Write(imageData, 0, imageData.Length)
                        PictureBox1.Image = Image.FromStream(ms, True)

                    End Using
                End If
            Next
        End If

        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            SQL.RunQuery("Select * " & _
                    "From Movie,Mov_Location Where M_ID= MOV_ID and M_NAME = '" & FormHom2.moviename & "'")
            ComboBox1.DataSource = SQL.SQLDataset.Tables(0)
            ComboBox1.ValueMember = "MOV_LOC"
            ComboBox1.DisplayMember = "MOV_LOC"
        End If

        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            SQL.RunQuery("Select * " & _
                    "From Seat")
            ComboBox2.DataSource = SQL.SQLDataset.Tables(0)
            ComboBox2.ValueMember = "seat_type"
            ComboBox2.DisplayMember = "seat_type"
        End If

        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            SQL.RunQuery("Select * " & _
                    "From Movie,Show_Time Where M_ID= MOV_ID and M_NAME = '" & FormHom2.moviename & "'")
            ComboBox3.DataSource = SQL.SQLDataset.Tables(0)
            ComboBox3.ValueMember = "S_time"
            ComboBox3.DisplayMember = "S_time"
        End If

        ComboBox4.DataSource = arr

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please enter the no seats!")
            Exit Sub
        End If

        noOfSeats = TextBox1.Text
        showTimings = ComboBox3.Text
        loc = ComboBox1.Text


        If Convert.ToInt32(noOfSeats) > Convert.ToInt32(Label7.Text) Then
            MsgBox("Seats Not Available")
            Exit Sub
        End If

        SQL.RunQuery("Select * " & _
                    "From Seat where seat_type = '" & ComboBox2.Text & "'")
        For Each i As Object In SQL.SQLDataset.Tables(0).Rows
            seatCost = i.item("seat_cost").ToString
        Next

        SQL.RunQuery("Select * " & _
                    "From MOVIE where M_NAME = '" & Label1.Text & "'")
        For Each i As Object In SQL.SQLDataset.Tables(0).Rows
            movieId = i.item("M_ID")
        Next

        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Customer VALUES(@C_NAME,@MOV_ID,@C_SEATS,@C_SEAT_TYPE,@C_TOTCOST,@C_RANCODE,@C_LOC,@C_SHOWTIME,@C_SHOWDATE)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@C_NAME", Label13.Text)




            cmd.Parameters.AddWithValue("@C_LOC", loc)
            seatType = ComboBox2.Text
            cmd.Parameters.AddWithValue("@C_SEAT_TYPE", ComboBox2.Text)
            cmd.Parameters.AddWithValue("@C_SEATS", noOfSeats)
            cmd.Parameters.AddWithValue("@C_SHOWTIME", showTimings)
            showDate = ComboBox4.Text
            cmd.Parameters.AddWithValue("@C_SHOWDATE", showDate)
            cmd.Parameters.AddWithValue("@MOV_ID", movieId)



            cmd.Parameters.AddWithValue("@C_TOTCOST", noOfSeats * seatCost)

            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Dim validChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
            Dim sb As New StringBuilder()
            Dim rand As New Random()
            For i As Integer = 1 To 6
                Dim idx As Integer = rand.Next(0, validChars.Length)
                Dim randomChar As Char = validChars(idx)
                sb.Append(randomChar)
            Next i
            randomCode = sb.ToString()

            cmd.Parameters.AddWithValue("@C_RANCODE", randomCode)

            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
        

        

        '----------------------------------------------------------------------------------------------------------------------------------
        'TO STORE C_ID
        SQL.RunQuery("Select * " & _
                    "From CUSTOMER where C_NAME = '" & Label13.Text & "'" & " AND C_RANCODE = '" & randomCode & "'")
        For Each i As Object In SQL.SQLDataset.Tables(0).Rows
            cid = i.item("c_id")
        Next
        '---------------------------------------------------------------------------------------------------------------------------------------
        FormPayment.Show()
        Me.Close()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        FormHom2.Close()
        FormHome.Show()
        Me.Close()
        'FormBook.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        Dim str As Integer
        SQL.RunQuery("Select * " & _
                    "From MOVIE where M_NAME = '" & Label1.Text & "'")
        For Each i As Object In SQL.SQLDataset.Tables(0).Rows
            str = i.item("M_ID")
        Next
        Try
            SQL.RunQuery("Select * " & _
                    "From Movie_Seats where seat_type = '" & ComboBox2.Text & "' and MOV_ID = " & str)
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                Label7.Text = i.item("SEAT_LEFT").ToString
            Next
        Catch ex As Exception

        End Try

    End Sub

End Class