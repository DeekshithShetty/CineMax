Imports System.Runtime.InteropServices
Imports System.IO


Public Class FormSignIn
    Public username As String
    Public char1 As String

    Dim waitTime As Integer = 500 'ms
    Dim speed As Integer = 60
    Dim speedText As Integer = 600 'ms

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function
    Private Sub FormSignIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        animationTimer.Interval = speed
        waitTimer.Interval = waitTime
        'Timer3.Interval = speedText
        waitTimer.Enabled = True



        SendMessage(Me.TextBox1.Handle, &H1501, 0, "Enter your name")
        SendMessage(Me.TextBox2.Handle, &H1501, 0, "Password")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim SQL As New SQLControl
        Try
            'SQL.SQLCon.Open()
            If CheckBox1.Checked Then
                char1 = "y"
            Else
                char1 = "n"
            End If
            SQL.RunQuery("Select * from ACCOUNT where usr_name =" & "'" & TextBox1.Text & "'" & " and psswd = " & "'" & TextBox2.Text & "'" & " and admin = " & "'" & char1 & "'")
            If SQL.SQLDataset.Tables(0).Rows.Count <= 0 Then
                SQL.SQLCon.Close()
                Label3.Text = "Incorrect Username or password"
            Else
                SQL.SQLCon.Close()
                username = TextBox1.Text
                File.WriteAllText("data.txt", String.Copy(username))
                FormHom2.Show()
                Me.Close()
                FormHome.Close()
            End If
        Catch ex As Exception
            SQL.SQLCon.Close()
            MsgBox("Hello")
            MsgBox(ex.Message)
        Finally
            SQL.SQLCon.Close()
        End Try
    End Sub


    Private Sub animationTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles animationTimer.Tick
        PictureBox1.Location = New Point(PictureBox1.Location.X, PictureBox1.Location.Y + 5)
        If PictureBox1.Location.Y = 110 Then animationTimer.Enabled = False

    End Sub

    Private Sub waitTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles waitTimer.Tick
        waitTimer.Enabled = False
        animationTimer.Enabled = True
        'Timer3.Enabled = True

    End Sub
End Class