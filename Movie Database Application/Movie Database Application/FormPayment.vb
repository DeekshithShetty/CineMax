Imports System.Data.SqlClient
Imports System.IO

Public Class FormPayment
    Public noOfSeats = FormBook.noOfSeats
    Public showTimings = FormBook.showTimings
    Public showDate = FormBook.showDate
    Public loc = FormBook.loc
    Public seatCost = FormBook.seatCost
    Public seatType = FormBook.seatType
    Public username = FormHom2.username
    Public cid = FormBook.cid
    Public randomcode = FormBook.randomCode
    Public movieId = FormBook.movieId

    Dim SQL As New SQLControl

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then

            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Payment VALUES(@CUST_ID,@CARD_NAME,@CARD_NUM,@EXP_DATE,@CVV)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@CUST_ID", cid)

            If String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Then
                MsgBox("Please enter the required values")
                SQL.SQLCon.Close()
                Exit Sub
            End If

            cmd.Parameters.AddWithValue("@CARD_NAME", TextBox1.Text)
            cmd.Parameters.AddWithValue("@CARD_NUM", TextBox2.Text)
            Dim STR = ComboBox1.Text & "-" & ComboBox2.Text
            cmd.Parameters.AddWithValue("@EXP_DATE", STR)
            cmd.Parameters.AddWithValue("@CVV", TextBox3.Text)

            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()

            '----------------------------------------------------------------------------------------------------------

            Dim seatleft As Integer

            SQL.RunQuery("Select * " & _
                        "From Movie_Seats Where MOV_ID = " & movieId & " AND SEAT_TYPE = '" & seatType & "'")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                    seatleft = i.item("Seat_Left") - noOfSeats
                Next
            End If
            SQL.SQLCon.Open()
            query = "UPDATE Movie_Seats SET SEAT_LEFT = @SEAT_LEFT WHERE MOV_ID = @MOV_ID AND SEAT_TYPE = @SEAT_TYPE"
            Dim cmd2 As New SqlCommand(query, SQL.SQLCon)
            cmd2.Parameters.AddWithValue("@SEAT_LEFT", seatleft)
            cmd2.Parameters.AddWithValue("@MOV_ID", movieId)
            cmd2.Parameters.AddWithValue("@SEAT_TYPE", seatType)
            cmd2.ExecuteNonQuery()
            SQL.SQLCon.Close()

            '----------------------------------------------------------------------------------------------------------
            Threading.Thread.Sleep(2000)
            FormFinalPay.Show()
            Me.Close()

        Else
            MsgBox("Please accept the terms and conditions")
        End If
        
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        FormHom2.Close()
        SQL.RunQuery("Delete " & _
                    "From CUSTOMER where C_ID =" & cid)
        FormHome.Show()
        Me.Close()
        FormBook.Close()
    End Sub

    Private Sub FormPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label13.Text = username
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SQL.RunQuery("Delete " & _
                    "From CUSTOMER where C_ID = " & cid)
        FormHom2.Show()
        Me.Close()
    End Sub
End Class