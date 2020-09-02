Public Class CheckInOut

    Public contraseña As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim clave As String ''La usuo para ingresar el check

        clave = TextBox1.Text

        Principal.Check(clave)
        TextBox1.Text = ""
        Me.Hide()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then

            Button1.PerformClick()

        End If

    End Sub

End Class