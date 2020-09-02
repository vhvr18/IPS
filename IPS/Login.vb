Imports System.Data.SqlClient
Public Class Login

    Dim da As SqlDataAdapter


    Public usuario As String            ''Esta en publica porque la usaremos para saber quien esta en sesion el la principal
    Public nombreCompleto As String
    Public nivel As String


    Public password As String

    Dim usuario2 As String
    Dim password2 As String

    Dim intento As Integer = 0


    ''Codigo del logeo
    Public Sub Log()

        If TextBox1.Text = "" And TextBox2.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Then

            MessageBox.Show("Ingresa lo datos solicitados", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            intento = intento + 1

        Else

            usuario = Trim(TextBox1.Text)
            password = TextBox2.Text

            sql = "select usuario,contraseña,nombre,apellidop,nivel from usuarios where usuario = '" + usuario + "' and  contraseña = '" & password & "'"
            Ejecutar(sql)


            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                usuario2 = dr(0)
                password2 = dr(1)
                nombreCompleto = dr(2) + " " + dr(3)
                nivel = dr(4)

            End While

            If usuario = usuario2 And password = password2 Then
                Me.Hide()
                Principal.Show()
            Else

                MessageBox.Show("Contraseña Incorrecta", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox1.Select()


                intento = intento + 1

            End If

        End If

        If intento = 3 Then

            MessageBox.Show("Lo sentimos excediste el número de intentos", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()

        End If


    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)

        Log()

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Hide()

        RecuperarContraseña.Show()

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Text = ""
        TextBox2.Text = ""

        TextBox1.Select()
        TextBox1.CharacterCasing = CharacterCasing.Lower  ''Pone los caracteres del textbox en minusculas
        Me.FormBorderStyle = FormBorderStyle.None ' Borrar Borde de la venta (Formulario)


    End Sub


    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyCode = Keys.Enter Then
            Log()
        End If

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        If Char.IsLetter(e.KeyChar) Then
            e.Handled = False                       ''Codigo para que solo escriba letras dentro del textbox
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsSeparator(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Log()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Application.Exit()

    End Sub

End Class
