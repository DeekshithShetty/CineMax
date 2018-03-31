Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class SQLControl

    Public SQLCon As New SqlConnection With {.ConnectionString = "Server=DEEKSHITH;Database=MOVIEDB;Integrated Security=true;"}
    'Public SQLCon As New SqlConnection With {.ConnectionString = "Server=LENOVO-PC;Database=MOVIEDB;Integrated Security=true;"}
    Public SQLCmd As SqlCommand
    Public SQLDA As SqlDataAdapter
    Public SQLDataset As DataSet

    Public Function HasConnection() As Boolean
        Try
            SQLCon.Open()

            SQLCon.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Sub RunQuery(ByVal Query As String)
        Try
            SQLCon.Open()

            SQLCmd = New SqlCommand(Query, SQLCon)

            'Load sql records for datagrid
            SQLDA = New SqlDataAdapter(SQLCmd)
            SQLDataset = New DataSet
            SQLDA.Fill(SQLDataset)


            SQLCon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
        End Try
    End Sub

    Public Sub InsertMovie(ByVal MovieName As String, ByVal ReleaseDate As String, ByVal n As Integer, ByVal Description As String, ByVal Data As Byte)
        Try
            Dim query As String = "INSERT INTO Movie VALUES(" & "'" & MovieName & "'," & "'" & ReleaseDate & "'," & n & "," & "'" & Description & "'," & Data & ");"

            SQLCon.Open()

            SQLCmd = New SqlCommand(query, SQLCon)

            SQLCmd.ExecuteNonQuery()

            SQLCon.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
