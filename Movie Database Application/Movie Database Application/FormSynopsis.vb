Imports System.Data.SqlClient
Imports System.IO

Public Class FormSynopsis

    Private Sub FormSynopsis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SQL As New SQLControl
        If String.IsNullOrEmpty(FormHom2.moviename) Then
            Label1.Text = FormHome.moviename
            Me.Text = FormHome.moviename & ": Synopsis"
        Else
            Label1.Text = FormHom2.moviename
            Me.Text = FormHom2.moviename & ": Synopsis"
        End If
        Dim n As Integer = 2
        Dim number As Integer = 12
        FormHome.Opacity = 0.5
        FormHom2.Opacity = 0.5

        SQL.RunQuery("Select * " & _
                    "From Movie where M_NAME = '" & Label1.Text & "'")
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows

                PictureBox1.BackColor = Color.DimGray
                If Not i.item("M_IMG").GetType Is GetType(System.DBNull) Then
                    Dim imageData As Byte() = DirectCast(i.item("M_IMG"), Byte())
                    If Not imageData Is Nothing Then
                        Using ms As New MemoryStream(imageData, 0, imageData.Length)
                            ms.Write(imageData, 0, imageData.Length)
                            PictureBox1.Image = Image.FromStream(ms, True)

                        End Using
                    End If
                Else
                    PictureBox1.Image = Image.FromFile("notFound.png")
                End If
                


                Label3.Text = Date.Parse(i.item("M_RELEASE_DATE")).ToShortDateString.ToString
                Label8.Text = i.item("M_DIRECTOR").ToString
                Label6.Text = i.item("M_LANG").ToString
                Label11.Text = i.item("M_DESCR").ToString
            Next
        End If


        SQL.RunQuery("Select * " & _
                    "From Movie m,Acts_For f,Actor a Where m.M_ID= f.M_ID and f.A_NAME=a.A_NAME and M_NAME = '" & Label1.Text & "'")
        If SQL.SQLDataset.Tables(0).Rows.Count > 0 Then
            For Each i As Object In SQL.SQLDataset.Tables(0).Rows

                If n <= 3 Then

                    Dim no As String = n.ToString
                    Dim ss As String = "PictureBox" + no



                    Dim pb As PictureBox = DirectCast(Me.Controls.Find(ss, True).FirstOrDefault(), PictureBox)
                    If pb IsNot Nothing Then
                        If Not i.item("A_IMG").GetType Is GetType(System.DBNull) Then
                            Dim imageData As Byte() = DirectCast(i.item("A_IMG"), Byte())
                            If Not imageData Is Nothing Then
                                Using ms As New MemoryStream(imageData, 0, imageData.Length)
                                    ms.Write(imageData, 0, imageData.Length)
                                    pb.Image = Image.FromStream(ms, True)
                                    'pb.BackgroundImage.Dispose()

                                End Using
                            End If
                        Else
                            pb.Image = Image.FromFile("notFound.png")
                        End If
                        
                    End If

                    Dim l1 As String = "Label" + number.ToString
                    Dim lb1 As Label = DirectCast(Me.Controls.Find(l1, True).FirstOrDefault(), Label)
                    lb1.Text = i.item("A_NAME").ToString
                End If
                n = n + 1
                number = number + 1
            Next
        End If

    End Sub
    Private Sub FormSynopsis_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        FormHome.Opacity = 1
        FormHom2.Opacity = 1
    End Sub
End Class