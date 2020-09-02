Imports System.Data.SqlClient

Public Class RecuperarContraseña

    Dim da As SqlDataAdapter

    Dim intentos As Integer = 0         ''Variable para cerrar el sistema en caso de que exceda el numero de intentos

    ''Metodo que valida si el usuario existe y si es correcto al cual le pasamos un parametro 
    Public Sub ValidarUsuario(usuario As String)

        Dim ususarioSQL As String = ""                  ''Variable que uso para comparar 
        Dim pregunta As String = ""                     ''Variable donde alojo la respuesta de la consulta     

        sql = "Select usuario,pregunta from usuarios where usuario = '" + usuario + "'"         ''Consulta que me sirve para determinar si el usuario existe 
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ususarioSQL = dr(0)                             ''Asigno a las variables los valores de la consulta
            pregunta = dr(1)

        End While
        con.Close()

        If ususarioSQL = usuario Then                   ''Hago la comparacion si el usuario es correcto 

            TextBox2.Text = pregunta
            TextBox3.Select()

        Else

            MessageBox.Show("Usuario Incorrecto", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Text = ""
            TextBox1.Text = ""                             ''En caso de que se equivoque de usuario mandara un error y sumara uno a la variable intnetos

            intentos = intentos + 1


        End If

        If intentos = 3 Then                            ''Si la varialle intentos llega a 3 el sistema cerra el modulo recuperar contraseñas 

            MessageBox.Show("Lo sentimos exediste el número de intentos" + vbLf + "Intenta mas tarde", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Login.Show()

        End If


    End Sub

    ''Modulo para comparar la respuesta 
    Public Sub ValidarRespuesta(respuesta As String)

        Dim respuestaSQL As String = ""                  ''Variable que uso para comparar 
        Dim contraseña As String = ""
        Dim nombre As String = ""

        sql = "Select respuesta,nombre,contraseña from usuarios where respuesta = '" + respuesta + "' and usuario = '" + TextBox1.Text + "'"         ''Consulta que me sirve para determinar si el usuario existe 
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            nombre = dr(1)
            respuestaSQL = dr(0)                             ''Asigno a las variables los valores de la consulta
            contraseña = dr(2)

        End While

        If respuestaSQL = respuesta Then                   ''Hago la comparacion si el usuario es correcto 

            MessageBox.Show("Hola " + nombre + " hemos recuperado tu contraseña." + vbLf + "Tu contraseña es: " + contraseña, "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
            Login.TextBox1.Text = ""
            Login.TextBox2.Text = ""
            Login.Show()



        Else

            MessageBox.Show("Respuesta Incorrecta", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

            intentos = intentos + 1

            TextBox1.Select()


        End If

        If intentos = 3 Then
            MessageBox.Show("Lo sentimos exediste el número de intentos" + vbLf + "Intenta mas tarde", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Login.TextBox1.Text = ""
            Login.TextBox2.Text = ""
            Login.Show()

        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Login.Show()

    End Sub

    Private Sub RecuperarContraseña_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Select()
        TextBox1.CharacterCasing = CharacterCasing.Lower  ''Pone los caracteres del textbox en minusculas
        'Me.FormBorderStyle = FormBorderStyle.None ' Borrar Borde de la venta (Formulario)

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

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then

            ValidarUsuario(TextBox1.Text)       ''Llamo el metodo que valida el usuario 

        End If

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown

        If e.KeyCode = Keys.Enter Then

            ValidarRespuesta(TextBox3.Text)       ''llamo el metodo que valida la respuesta

        End If

    End Sub

    ''Boton aceptar que valida la respuesta y en caso de que no haya nada en los textbox manda error
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Or TextBox3.Text = "" Then

            MessageBox.Show("Ingresa los datos que se te piden", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            intentos = intentos + 1
        Else

            ValidarRespuesta(TextBox3.Text)

        End If

        If intentos = 3 Then
            MessageBox.Show("Lo sentimos exediste el número de intentos" + vbLf + "Intenta mas tarde", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Login.Show()

        End If

    End Sub


End Class