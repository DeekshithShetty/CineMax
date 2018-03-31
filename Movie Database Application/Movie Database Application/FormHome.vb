Imports System.Data.SqlClient
Imports System.IO

Public Class FormHome
    Public moviename As String
    Private Sub FormHome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim SQL As New SQLControl
        '----------------------------------------------------------------------------------------------------
        SQL.RunQuery("Select * " & _
                    "From Movie Where M_STATUS = 'o' ")
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

        End If

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
        Else
            FlowLayoutPanel2.BackColor = Color.DimGray
        End If
    End Sub

    Public Sub GenericClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        moviename = sender.tag
        FormSynopsis.Show()
    End Sub

    Public Sub ButtonBook(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FormSignIn.Show()
    End Sub


    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        FormSignIn.Show()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        FormSignUp.Show()
    End Sub
End Class