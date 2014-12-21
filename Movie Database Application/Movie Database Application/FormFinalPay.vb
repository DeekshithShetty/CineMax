Public Class FormFinalPay

    Private Sub FormFinalPay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label2.Text = FormPayment.randomcode
        Label3.Text = FormHom2.moviename
        Label5.Text = FormPayment.loc
        Label7.Text = FormPayment.noOfSeats
        Label9.Text = FormPayment.showTimings
        Label12.Text = FormPayment.showDate
        Dim total = FormPayment.noOfSeats * FormPayment.seatCost
        Label14.Text = "Rs." + total.ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class