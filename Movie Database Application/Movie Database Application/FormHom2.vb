
Imports System.Data.SqlClient
Imports System.IO
Public Class FormHom2
    Public moviename As String
    Public username
    Public char2
    Public send As System.Object

    Private Sub FormHom2_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SQL As New SQLControl
        username = File.ReadAllText("data.txt")
        Me.Text = "Welcome " & username
        char2 = FormSignIn.char1

        SQL.RunQuery("Select * " & _
                    "From Account Where usr_name = '" & username & "'")
        For Each i As Object In SQL.SQLDataset.Tables(0).Rows
            char2 = i.item("admin")
        Next
        If char2 = "y" Then
        Else
            Me.Button10.Visible = False
            Me.Panel2.Location = New System.Drawing.Point(849, 71)

        End If
        'MsgBox("Welcome, " & username)
        Label13.Text = username

        ComboBox1.SelectedText = "Mangalore"
        ComboBox2.SelectedText = "All"
        initializeMovies("Mangalore", "All")


        '------------------------------------COMING SOON-------------------------------------
        SQL.RunQuery("Select * " & _
                    "From Movie Where M_STATUS = 'c' ")
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                Dim pan As Panel = New Panel
                pan.Height = 460
                pan.Width = 227
                pan.BackColor = Color.DimGray
                pan.BackColor = Color.FromArgb(40, 40, 40)
                pan.Parent = FlowLayoutPanel2

                Dim pictureBox As PictureBox = New PictureBox
                pictureBox.Parent = pan
                pictureBox.Height = 298
                pictureBox.Width = 200
                pictureBox.Location = New System.Drawing.Point(9, 15)
                pictureBox.BackColor = Color.DimGray
                If Not i.item("M_IMG").GetType Is GetType(System.DBNull) Then
                    Dim imageData As Byte() = DirectCast(i.item("M_IMG"), Byte())
                    If Not imageData Is Nothing Then
                        Using ms As New MemoryStream(imageData, 0, imageData.Length)
                            ms.Write(imageData, 0, imageData.Length)
                            pictureBox.Image = Image.FromStream(ms, True)

                        End Using
                    Else
                        pictureBox.Image = Image.FromFile("notFound.png")
                    End If
                End If
                


                Dim lab1 As Label = New Label()
                lab1.Parent = pan
                lab1.Location = New System.Drawing.Point(6, 329)
                lab1.Text = i.item("M_NAME")
                lab1.AutoSize = False
                lab1.Size = New System.Drawing.Size(147, 41)
                lab1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12)
                lab1.ForeColor = Color.White

                Dim lab2 As Label = New Label()
                lab2.Parent = pan
                lab2.Location = New System.Drawing.Point(6, 368)
                lab2.Size = New System.Drawing.Size(78, 16)
                lab2.Text = "Language :"
                lab2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab2.ForeColor = Color.White

                Dim lab3 As Label = New Label()
                lab3.Parent = pan
                lab3.Location = New System.Drawing.Point(90, 370)
                lab3.Text = i.item("M_LANG").ToString
                lab3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab3.ForeColor = Color.White

                Dim lab4 As Label = New Label()
                lab4.Parent = pan
                lab4.Location = New System.Drawing.Point(6, 400)
                lab4.Size = New System.Drawing.Size(56, 16)
                lab4.Text = "Rating :"
                lab4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab4.ForeColor = Color.White

                Dim lab5 As Label = New Label()
                lab5.Parent = pan
                lab5.Location = New System.Drawing.Point(67, 400)
                lab5.Text = i.item("M_CNSRRAT").ToString
                lab5.AutoSize = True
                lab5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab5.ForeColor = Color.White


                Dim but2 As Button = New Button()
                but2.Parent = pan
                but2.Location = New System.Drawing.Point(49, 433)
                but2.Text = "SYNOPSIS"
                but2.Tag = lab1.Text
                but2.BackColor = Color.Red
                but2.FlatStyle = FlatStyle.Flat
                but2.FlatAppearance.BorderSize = 0
                but2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                but2.ForeColor = Color.White
                but2.Size = New System.Drawing.Size(113, 23)
                AddHandler but2.Click, AddressOf GenericClick

            Next

        End If
    End Sub

    Public Sub GenericClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        moviename = sender.tag
        FormSynopsis.Show()
    End Sub

    Public Sub ButtonBook(ByVal sender As System.Object, ByVal e As System.EventArgs)
        moviename = sender.tag
        FormBook.Show()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        FormHome.Show()
        Me.Close()
        FormBook.Close()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If char2 = "y" Then
            FormAdmin.Show()
            Me.Close()
        Else
            MsgBox("You are not an administrator")
        End If
    End Sub

    Public Sub initializeMovies(ByVal str1 As String, ByVal str2 As String)
        FlowLayoutPanel1.Controls.Clear()
        Dim SQL As New SQLControl

        If str2 = "All" Then
            SQL.RunQuery("Select * " & _
                    "From Movie,Mov_Location Where M_ID = MOV_ID and M_STATUS = 'o' and MOV_LOC = '" & str1 & "'")
        Else
            SQL.RunQuery("Select * " & _
                    "From Movie,Mov_Location Where M_ID = MOV_ID and M_STATUS = 'o' and M_LANG = '" & str2 & "' and MOV_LOC = '" & str1 & "'")
        End If
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows
                Dim pan As Panel = New Panel
                pan.Height = 460
                pan.Width = 227
                pan.BackColor = Color.DimGray
                pan.BackColor = Color.FromArgb(40, 40, 40)
                pan.Parent = FlowLayoutPanel1

                Dim pictureBox As PictureBox = New PictureBox
                pictureBox.Parent = pan
                pictureBox.Height = 298
                pictureBox.Width = 200
                pictureBox.Location = New System.Drawing.Point(9, 15)
                pictureBox.BackColor = Color.DimGray
                If Not i.item("M_IMG").GetType Is GetType(System.DBNull) Then
                    Dim imageData As Byte() = DirectCast(i.item("M_IMG"), Byte())
                    If Not imageData Is Nothing Then
                        Using ms As New MemoryStream(imageData, 0, imageData.Length)
                            ms.Write(imageData, 0, imageData.Length)
                            pictureBox.Image = Image.FromStream(ms, True)

                        End Using
                    End If
                Else
                    pictureBox.Image = Image.FromFile("notFound.png")
                End If



                Dim lab1 As Label = New Label()
                lab1.Parent = pan
                lab1.Location = New System.Drawing.Point(6, 329)
                lab1.Text = i.item("M_NAME")
                lab1.AutoSize = False
                lab1.Size = New System.Drawing.Size(147, 41)
                lab1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12)
                lab1.ForeColor = Color.White

                Dim lab2 As Label = New Label()
                lab2.Parent = pan
                lab2.Location = New System.Drawing.Point(6, 368)
                lab2.Size = New System.Drawing.Size(78, 16)
                lab2.Text = "Language :"
                lab2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab2.ForeColor = Color.White

                Dim lab3 As Label = New Label()
                lab3.Parent = pan
                lab3.Location = New System.Drawing.Point(90, 370)
                lab3.Text = i.item("M_LANG").ToString
                lab3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab3.ForeColor = Color.White

                Dim lab4 As Label = New Label()
                lab4.Parent = pan
                lab4.Location = New System.Drawing.Point(6, 400)
                lab4.Size = New System.Drawing.Size(56, 16)
                lab4.Text = "Rating :"
                lab4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab4.ForeColor = Color.White

                Dim lab5 As Label = New Label()
                lab5.Parent = pan
                lab5.Location = New System.Drawing.Point(67, 400)
                lab5.Text = i.item("M_CNSRRAT").ToString
                lab5.AutoSize = True
                lab5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                lab5.ForeColor = Color.White

                Dim but1 As Button = New Button()
                but1.Parent = pan
                but1.Location = New System.Drawing.Point(111, 397)
                but1.Text = "BOOK"
                but1.Tag = lab1.Text
                but1.BackColor = Color.Red
                but1.FlatStyle = FlatStyle.Flat
                but1.FlatAppearance.BorderSize = 0
                but1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                but1.ForeColor = Color.White
                but1.Size = New System.Drawing.Size(113, 23)
                AddHandler but1.Click, AddressOf ButtonBook

                Dim but2 As Button = New Button()
                but2.Parent = pan
                but2.Location = New System.Drawing.Point(111, 426)
                but2.Text = "SYNOPSIS"
                but2.Tag = lab1.Text
                but2.BackColor = Color.Red
                but2.FlatStyle = FlatStyle.Flat
                but2.FlatAppearance.BorderSize = 0
                but2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75)
                but2.ForeColor = Color.White
                but2.Size = New System.Drawing.Size(113, 23)
                AddHandler but2.Click, AddressOf GenericClick

            Next
        Else
            Dim pan1 As Panel = New Panel
            pan1.Height = 461
            pan1.Width = 978
            pan1.BackColor = Color.LightGray
            pan1.Parent = FlowLayoutPanel1

            Dim lab9 As Label = New Label()
            lab9.Parent = pan1
            lab9.Location = New System.Drawing.Point(257, 200)
            lab9.Text = "Sorry No Movies Found :("
            lab9.Font = New System.Drawing.Font("Segoe UI Semibold", 18.25)
            lab9.AutoSize = False
            lab9.Size = New System.Drawing.Size(300, 51)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        initializeMovies(ComboBox1.Text, ComboBox2.Text)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        initializeMovies(ComboBox1.Text, ComboBox2.Text)
    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        UserInfo.Show()
    End Sub
End Class