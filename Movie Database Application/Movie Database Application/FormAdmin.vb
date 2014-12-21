Imports System.Data.SqlClient
Imports System.IO


Public Class FormAdmin
    Dim SQL As New SQLControl

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Movie VALUES(@M_NAME,@M_RELEASE_DATE,@M_DESCR,@M_IMG,@M_STATUS,@M_LANG,@M_DIRECTOR,@M_CNSRRAT)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@M_NAME", TextBox1.Text)
            cmd.Parameters.AddWithValue("@M_RELEASE_DATE", TextBox2.Text)
            cmd.Parameters.AddWithValue("@M_DESCR", TextBox3.Text)
            cmd.Parameters.AddWithValue("@M_STATUS", ComboBox1.Text)
            cmd.Parameters.AddWithValue("@M_LANG", ComboBox2.Text)
            cmd.Parameters.AddWithValue("@M_DIRECTOR", TextBox12.Text)
            cmd.Parameters.AddWithValue("@M_CNSRRAT", ComboBox3.Text)

            Dim ms As New MemoryStream()
            PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@M_IMG", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()


            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Movie")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DGV.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            SQL.SQLCon.Close()
        End Try
    End Sub

    Private Sub DGV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub

    Private Sub DGV_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DGV.DataError

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Try
            SQL.RunQuery("Select * from Movie")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DGV.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If OpenFileDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox2.BackgroundImage = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Actor VALUES(@A_NAME ,@A_IMG,@A_SEX)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@A_NAME", TextBox5.Text)
            cmd.Parameters.AddWithValue("@A_SEX", TextBox13.Text)
            Dim ms As New MemoryStream()
            PictureBox2.BackgroundImage.Save(ms, PictureBox2.BackgroundImage.RawFormat)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@A_IMG", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()


            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Actor")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView1.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            SQL.RunQuery("Select * from Actor")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView1.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Acts_For VALUES(@A_NAME ,@M_ID)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@A_NAME", TextBox10.Text)
            cmd.Parameters.AddWithValue("@M_ID", TextBox4.Text)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()


            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Acts_For")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView4.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Try
            SQL.RunQuery("Select * from Acts_For")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView4.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Show_Time VALUES(@MOV_ID,@S_TIME,@DURATION_MINS)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd.Parameters.AddWithValue("@S_TIME", TextBox7.Text)
            cmd.Parameters.AddWithValue("@DURATION_MINS", TextBox8.Text)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()

            '---------------------------------------------------------------------------------------
            
            '---------------------------------------------------------------------------------------

            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Show_Time")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView2.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try
            SQL.RunQuery("Select * from Show_Time")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView2.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView2_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DataGridView2.DataError

    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO Mov_Location VALUES(@MOV_ID ,@MOV_LOC)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            cmd.Parameters.AddWithValue("@MOV_ID ", TextBox9.Text)
            cmd.Parameters.AddWithValue("@MOV_LOC", TextBox11.Text)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()


            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Mov_Location")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView3.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            SQL.RunQuery("Select * from Mov_Location")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DataGridView3.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            If Not PictureBox1.Image Is Nothing Then
                SQL.SQLCon.Open()
                Dim query As String = "UPDATE Movie SET M_IMG = @M_IMG1 WHERE M_NAME = @M_NAME1"
                Dim cmd As New SqlCommand(query, SQL.SQLCon)
                cmd.Parameters.AddWithValue("@M_NAME1", TextBox1.Text)
                Dim ms As New MemoryStream()
                PictureBox1.BackgroundImage.Save(ms, PictureBox1.BackgroundImage.RawFormat)
                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@M_IMG1", SqlDbType.Image)
                p.Value = data
                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()
                SQL.SQLCon.Close()
            ElseIf Not ComboBox1.Text Is Nothing Then
                SQL.SQLCon.Open()
                Dim query As String = "UPDATE Movie SET M_STATUS = @M_STATUS1 WHERE M_NAME = @M_NAME1"
                Dim cmd As New SqlCommand(query, SQL.SQLCon)
                cmd.Parameters.AddWithValue("@M_NAME1", TextBox1.Text)
                cmd.Parameters.AddWithValue("@M_STATUS1", ComboBox1.Text)
                cmd.ExecuteNonQuery()
                SQL.SQLCon.Close()
            End If
            


            'SQL.InsertMovie(TextBox1.Text, TextBox2.Text, 2, TextBox3.Text, data)
            SQL.RunQuery("Select * from Movie")
            If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
                DGV.DataSource = SQL.SQLDataset.Tables(0)
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form1.Show()
        Me.Close()
    End Sub

   
    Private Sub Button12_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'iNSERTING THE SEATS FOR A MOVIE
        Try
            SQL.SQLCon.Open()
            Dim query2 As String = "INSERT INTO Movie_Seats VALUES(@MOV_ID,@SEAT_TYPE,@SEAT_LEFT)"
            Dim cmd2 As New SqlCommand(query2, SQL.SQLCon)
            cmd2.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd2.Parameters.AddWithValue("@SEAT_TYPE", "Silver")
            cmd2.Parameters.AddWithValue("@SEAT_LEFT", 50)
            cmd2.ExecuteNonQuery()
            SQL.SQLCon.Close()

            SQL.SQLCon.Open()
            Dim query3 As String = "INSERT INTO Movie_Seats VALUES(@MOV_ID,@SEAT_TYPE,@SEAT_LEFT)"
            Dim cmd3 As New SqlCommand(query3, SQL.SQLCon)
            cmd3.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd3.Parameters.AddWithValue("@SEAT_TYPE", "Gold")
            cmd3.Parameters.AddWithValue("@SEAT_LEFT", 30)
            cmd3.ExecuteNonQuery()
            SQL.SQLCon.Close()

            SQL.SQLCon.Open()
            Dim query4 As String = "INSERT INTO Movie_Seats VALUES(@MOV_ID,@SEAT_TYPE,@SEAT_LEFT)"
            Dim cmd4 As New SqlCommand(query4, SQL.SQLCon)
            cmd4.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd4.Parameters.AddWithValue("@SEAT_TYPE", "Platinum")
            cmd4.Parameters.AddWithValue("@SEAT_LEFT", 20)
            cmd4.ExecuteNonQuery()
            SQL.SQLCon.Close()
            MsgBox("Seats inserted to movie with id " & TextBox6.Text)
        Catch ex As Exception
            MsgBox("Seats have been already inserted")
            SQL.SQLCon.Close()
        End Try
        
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Try
            SQL.SQLCon.Open()
            Dim query2 As String = "UPDATE Movie_Seats SET SEAT_LEFT = @SEAT_LEFT WHERE MOV_ID = @MOV_ID AND SEAT_TYPE = @SEAT_TYPE"
            Dim cmd2 As New SqlCommand(query2, SQL.SQLCon)
            cmd2.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd2.Parameters.AddWithValue("@SEAT_TYPE", "Silver")
            cmd2.Parameters.AddWithValue("@SEAT_LEFT", 50)
            cmd2.ExecuteNonQuery()
            SQL.SQLCon.Close()

            SQL.SQLCon.Open()
            Dim query3 As String = "UPDATE Movie_Seats SET SEAT_LEFT = @SEAT_LEFT WHERE MOV_ID = @MOV_ID AND SEAT_TYPE = @SEAT_TYPE"
            Dim cmd3 As New SqlCommand(query3, SQL.SQLCon)
            cmd3.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd3.Parameters.AddWithValue("@SEAT_TYPE", "Gold")
            cmd3.Parameters.AddWithValue("@SEAT_LEFT", 30)
            cmd3.ExecuteNonQuery()
            SQL.SQLCon.Close()

            SQL.SQLCon.Open()
            Dim query4 As String = "UPDATE Movie_Seats SET SEAT_LEFT = @SEAT_LEFT WHERE MOV_ID = @MOV_ID AND SEAT_TYPE = @SEAT_TYPE"
            Dim cmd4 As New SqlCommand(query4, SQL.SQLCon)
            cmd4.Parameters.AddWithValue("@MOV_ID", TextBox6.Text)
            cmd4.Parameters.AddWithValue("@SEAT_TYPE", "Platinum")
            cmd4.Parameters.AddWithValue("@SEAT_LEFT", 20)
            cmd4.ExecuteNonQuery()
            SQL.SQLCon.Close()
            MsgBox("Seats updated to movie with id " & TextBox6.Text)

        Catch ex As Exception
            MsgBox("ERROR")
            SQL.SQLCon.Close()
        End Try
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        FormHom2.Show()
        Me.Close()
    End Sub
End Class