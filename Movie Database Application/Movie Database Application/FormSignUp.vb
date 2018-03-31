
Imports System.Data.SqlClient
Imports System.IO

Public Class FormSignUp
    Public username As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim SQL As New SQLControl

        Try
            SQL.SQLCon.Open()
            Dim query As String = "INSERT INTO ACCOUNT VALUES(@USR_NAME ,@PSSWD ,@MOBILE_NO,@ADMIN)"
            Dim cmd As New SqlCommand(query, SQL.SQLCon)
            If TextBox1.Text Is Nothing Then
                Label5.Text = "Please enter a valid username"
                SQL.SQLCon.Close()
                Exit Sub
            Else
                username = TextBox1.Text
                File.WriteAllText("data.txt", String.Copy(username))
                cmd.Parameters.AddWithValue("@USR_NAME ", TextBox1.Text)
            End If
            If TextBox2.Text Is Nothing Then
                Label5.Text = "Please enter a valid password"
                SQL.SQLCon.Close()
                Exit Sub
            Else
                cmd.Parameters.AddWithValue("@PSSWD", TextBox2.Text)
            End If
            If String.Compare(TextBox2.Text, TextBox3.Text) <> 0 Then
                Label5.Text = "password doesnt match please enter again"
                TextBox2.Text = ""
                TextBox3.Text = ""
                SQL.SQLCon.Close()
                Exit Sub 
            End If
            If TextBox4.Text Is Nothing Then
                Label5.Text = "Please enter a valid mobile number"
                SQL.SQLCon.Close()
                Exit Sub
            Else
                cmd.Parameters.AddWithValue("@MOBILE_NO", TextBox4.Text)
            End If

            Dim char1 As String
            If CheckBox1.Checked Then
                char1 = "y"
            Else
                char1 = "n"
            End If
            cmd.Parameters.AddWithValue("@ADMIN", char1)
            cmd.ExecuteNonQuery()
            SQL.SQLCon.Close()

            FormHom2.Show()
            FormHome.Close()

            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub FormSignUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class