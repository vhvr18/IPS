Imports System.Data.SqlClient

Public Class Usuarios

    Dim letra1 As String
    Dim letra2 As String
    Dim letra3 As String

    ''Metodo para agregar los datos al listbox
    Public Sub RellenarList()

        sql = "Select usuario from Usuarios where NIVEL <> 'ADMINISTRADOR'"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox1.Items.Add(dr(0)) 'agregar los datos al listbox

        End While
        con.Close()

    End Sub

    ''Metodo para rellenar todo el formulario 
    Public Sub Datos()

        Dim nombre As String = ""
        Dim apellidop As String = ""
        Dim apellidom As String = ""
        Dim usuario As String = ""
        Dim contraseña As String = ""
        Dim pregunta As String = ""
        Dim respuesta As String = ""
        Dim nivel As String = ""



        sql = "select nombre, apellidop, apellidom, usuario,contraseña,pregunta,respuesta,nivel from usuarios where usuario = '" + ListBox1.SelectedItem + "'"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            nombre = dr(0) 'agregar los datos al listbox
            apellidop = dr(1)
            apellidom = dr(2)
            usuario = dr(3)
            contraseña = dr(4)
            pregunta = dr(5)
            respuesta = dr(6)
            nivel = dr(7)

        End While

        TextBox1.Text = nombre
        TextBox2.Text = apellidop
        TextBox6.Text = apellidom
        TextBox3.Text = usuario
        ComboBox1.Text = pregunta
        TextBox7.Text = respuesta
        ComboBox2.Text = nivel

        PictureBox1.Visible = False
        PictureBox2.Visible = True

    End Sub

    ''Load de l form 
    Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RellenarList() 'metodo para llenar el list
        TextBox1.Select()


        TextBox1.CharacterCasing = CharacterCasing.Upper  ''Upper pone los caracteres del textbox en mayusculas
        TextBox2.CharacterCasing = CharacterCasing.Upper

        TextBox3.CharacterCasing = CharacterCasing.Lower ''Upper pone los caracteres del textbox minusculas
        TextBox4.CharacterCasing = CharacterCasing.Lower
        TextBox5.CharacterCasing = CharacterCasing.Lower

        TextBox6.CharacterCasing = CharacterCasing.Upper
        TextBox7.CharacterCasing = CharacterCasing.Lower


        'Me.FormBorderStyle = FormBorderStyle.None ' Borrar Borde de la venta (Formulario)
        ''Me.Size = New Size(1119, 531)

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Datos()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False

        ComboBox1.Enabled = False
        ComboBox2.Enabled = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = "" Then

            MessageBox.Show("Por Favor Selecciona Primero un Usuario", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox7.Enabled = True

            ComboBox1.Enabled = True
            ComboBox2.Enabled = True

            PictureBox2.Visible = True
            PictureBox1.Visible = False

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True

        ComboBox1.Enabled = True
        ComboBox2.Enabled = True

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = Nothing
        TextBox7.Text = ""
        TextBox6.Text = ""
        ComboBox2.Text = Nothing

        ListBox1.Items.Clear()
        RellenarList()

        PictureBox1.Visible = True
        PictureBox2.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim nombre As String
        Dim apellidop As String
        Dim apellidom As String
        Dim usuario As String
        Dim contraseña As String
        Dim contraseña2 As String
        Dim pregunta As String
        Dim respuesta As String
        Dim nivel As String
        Dim usuario2 As String = ""


        nombre = TextBox1.Text
        apellidop = TextBox2.Text
        apellidom = TextBox6.Text
        usuario = TextBox3.Text
        contraseña = TextBox4.Text
        respuesta = TextBox7.Text

        contraseña2 = TextBox5.Text
        pregunta = ComboBox1.Text
        nivel = ComboBox2.Text

        sql = "select usuario from usuarios where usuario = '" + usuario + "'"
        Ejecutar(sql)


        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            usuario2 = dr(0)

        End While



        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox6.Text = "" Or TextBox5.Text = "" Or TextBox4.Text = "" Or TextBox7.Text = "" Or ComboBox1.Text = "[-seleccionar-]" Or ComboBox2.Text = "[-seleccionar-]" Then
            'comparación para verificar que todos los datos sean correctos

            MessageBox.Show("Ingrese todos los datos", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)


        ElseIf contraseña <> contraseña2 Then ' que se verifique la contraseña

            MessageBox.Show("Verificar la contraseña", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)


        ElseIf usuario = usuario2 Then 'compara si el usuario a fue ingresado y si esta lo actualiza

            sql = "exec pd_actualizar '" + nombre + "','" + apellidop + "','" + apellidom + "','" + usuario + "','" + contraseña + "','" + pregunta + "','" + respuesta + "','" + nivel + "'"
            Ejecutar(sql)

            MessageBox.Show("Cambios guardados", "Integrated Sales System")
            ListBox1.Items.Clear()
            RellenarList()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            ComboBox1.Text = "[-seleccionar-]"
            TextBox7.Text = ""
            TextBox6.Text = ""
            ComboBox2.Text = "[-seleccionar-]"
            PictureBox1.Visible = True
            PictureBox2.Visible = False



        Else 'insertar los datos en la bd mediante el procedimiento almacenado


            sql = "exec pd_insertar '" + nombre + "','" + apellidop + "','" + apellidom + "','" + usuario + "','" + contraseña + "','" + pregunta + "','" + respuesta + "','" + nivel + "'"
            Ejecutar(sql)

            MessageBox.Show("Se ha ingresado correctamente", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ListBox1.Items.Clear()
            RellenarList()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            ComboBox1.Text = "[-seleccionar-]"
            TextBox7.Text = ""
            TextBox6.Text = ""
            ComboBox2.Text = "[-seleccionar-]"
            PictureBox1.Visible = True
            PictureBox2.Visible = False



        End If

    End Sub

    ''Metodo para eliminar
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim resp As Integer


        If TextBox1.Text = "" Then

            MessageBox.Show("Por Favor Selecciona Primero un Usuario", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox7.Enabled = True

            ComboBox1.Enabled = True
            ComboBox2.Enabled = True


            resp = MsgBox("Seguro que desea eliminar el usuario", vbOKCancel, "Integrated Sales System")

            If resp = 1 Then


                sql = "Delete usuarios where usuario='" + TextBox3.Text + "'"
                Ejecutar(sql)

                MessageBox.Show("Usuario eliminado con éxito", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ListBox1.Items.Clear()
                RellenarList()

            End If

        End If


        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = "[-seleccionar-]"
        TextBox7.Text = ""
        TextBox6.Text = ""
        ComboBox2.Text = "[-seleccionar-]"

        PictureBox1.Visible = True
        PictureBox2.Visible = False


    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If TextBox1.Text <> "" Then

            letra1 = Trim(TextBox1.Text(0)) ' tomar el primer caracter y lo guardo en una variable global

        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        If TextBox2.Text <> "" Then

            letra2 = Trim(TextBox2.Text) ' tomar los primeros 5 caracteres de la cadena 

        End If

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

        If TextBox6.Text <> "" Then

            letra3 = TextBox6.Text(0) ' tomar el primer caracter y lo guardo en una variable global

            TextBox3.Text = letra1 + letra3 + letra2

        End If

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios
            e.Handled = True

        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios
            e.Handled = True

        End If

        If Char.IsNumber(e.KeyChar) Then 'quitar numeros    

            e.Handled = True

        End If


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

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then ' quitar espacios
            e.Handled = True

        End If

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

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios
            e.Handled = True

        End If

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios
            e.Handled = True

        End If

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios

            e.Handled = True

        End If


    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios

            e.Handled = True

        End If

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

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs)

        Me.Size = New Size(185, 59)
        Me.Location = New Point(100, 680)

    End Sub

    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs)

        Me.Size = New Size(1119, 531)
        '        Me.Location = New Point(250, 180)
        Me.Location = New Point(127, 100)

    End Sub


End Class