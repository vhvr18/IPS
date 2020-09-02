Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail



Public Class Valores

    ''Codigo con el que muestro todas las listas
    Public Sub MostrasListas()

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        ListBox5.Items.Clear()
        ListBox6.Items.Clear()
        ListBox7.Items.Clear()

        sql = "Select * From Proveedores order by Nombre_Proveedor asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox1.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Categorias order by Nombre_De_Categoria asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox2.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Unidades_de_Medicion order by Unidad_de_Medicion asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox3.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Fabricantes order by Nombre_del_Fabricante asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox6.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Anaqueles order by Anaquel asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox5.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Niveles order by Nivel asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox4.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Tipo_de_Articulos order by Tipo_de_Articulo asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox7.Items.Add(dr(0))

        End While
        con.Close()


    End Sub

    ''Codigo que cuando selecciona un proveedor de la lista te da la info
    Public Sub MostrarDetallesProveedor()


        sql = "Select * From Proveedores where Nombre_Proveedor = '" + ListBox1.SelectedItem + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            If IsDBNull(dr(0)) = True Then              ''Comparacion por si no se lleno toda la info del proveedor y se guardo
                TextBox1.Text = ""
            Else
                TextBox1.Text = dr(0)
            End If



            If IsDBNull(dr(1)) = True Then
                TextBox2.Text = ""
            Else
                TextBox2.Text = dr(1)
            End If



            If IsDBNull(dr(2)) = True Then
                TextBox3.Text = ""
            Else
                TextBox3.Text = dr(2)
            End If



            If IsDBNull(dr(3)) = True Then
                TextBox4.Text = ""
            Else
                TextBox4.Text = dr(3)
            End If



            If IsDBNull(dr(4)) = True Then
                TextBox5.Text = ""
            Else
                TextBox5.Text = dr(4)
            End If


        End While
        con.Close()


    End Sub

    ''Codigo para guardad o editar y guardar
    Public Sub GuardarProveedor(seleccionado As String)

        Dim existe As String = ""

        Dim proveedor, contacto, direccion, telefono, email As String

        proveedor = Trim(TextBox1.Text)                 ''Asigno variables a los valores de los textbox
        contacto = Trim(TextBox2.Text)
        direccion = Trim(TextBox3.Text)
        telefono = Trim(TextBox4.Text)
        email = Trim(TextBox5.Text)


        sql = "Select * from Proveedores where Nombre_Proveedor = '" + seleccionado + "'"  ''consulta para saber si el proveedor ya esta dado de alta 
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read
            existe = dr(0)
        End While

        con.Close()

        If existe <> "" Then                ''Validacion por si se va a guardar un nuevo proveedor o se va a actualizar

            If TextBox1.Text = "" Then     ''Validacion por si no hay nombre del proveedor

                MessageBox.Show("Debes ingresar el nombre del proveedor.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else


                If TextBox2.Text = "" Then              ''Asignacion un valor por si no escribio nada

                    contacto = "0"

                End If

                If TextBox3.Text = "" Then

                    direccion = "0"

                End If

                If TextBox4.Text = "" Then

                    telefono = "0"

                End If

                If TextBox5.Text = "" Then

                    email = "0"

                End If

                ''Actualizacion 
                sql = "update dbo.Proveedores set Nombre_Proveedor = '" + proveedor + "', Nombre_Contacto = '" + contacto + "',Direccion = '" + direccion +
                    "', Telefono = '" + telefono + "',Email = '" + email + "' where Nombre_Proveedor = '" + proveedor + "'"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Registro editado con éxito", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""


                ListBox1.Items.Clear()
                MostrasListas()


            End If


        Else

            If TextBox1.Text = "" Then          ''Validacion por si no hay nombre de proveedor 

                MessageBox.Show("Debes ingresar el nombre del proveedor.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                ''if por si el usuario no ingresa algun dato el sistema le pone 0
                If TextBox2.Text = "" Then

                    contacto = "0"

                End If

                If TextBox3.Text = "" Then

                    direccion = "0"

                End If

                If TextBox4.Text = "" Then

                    telefono = "0"

                End If

                If TextBox5.Text = "" Then

                    email = "0"

                End If

                ''Guardas el proveedor que estas dando de alta
                sql = "insert into Proveedores values('" + proveedor + "','" + contacto + "','" + direccion + "','" + telefono + "','" + email + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Registro guardado con éxito", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""


                ListBox1.Items.Clear()
                MostrasListas()


            End If

        End If


    End Sub

    ''Codgio para eliminar proveedor
    Public Sub EliminarProveedor(seleccionado As String)

        If seleccionado <> "" Then              ''


            sql = "delete Proveedores where Nombre_Proveedor = '" + seleccionado + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Proveedor eliminado con éxito.", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""


            ListBox1.Items.Clear()
            MostrasListas()


        Else

            MessageBox.Show("Debes seleccionar un proveedor para eliminar.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If


    End Sub

    Private Sub Valores_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label6.BackColor = Color.FromArgb(192, 200, 255)

        Panel1.Visible = True

        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        TextBox1.Select()


        MostrasListas()

        TextBox1.CharacterCasing = CharacterCasing.Upper
        TextBox2.CharacterCasing = CharacterCasing.Upper

        TextBox6.CharacterCasing = CharacterCasing.Upper
        TextBox7.CharacterCasing = CharacterCasing.Upper
        TextBox8.CharacterCasing = CharacterCasing.Upper
        TextBox9.CharacterCasing = CharacterCasing.Upper
        TextBox10.CharacterCasing = CharacterCasing.Upper
        TextBox11.CharacterCasing = CharacterCasing.Upper


    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

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

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        MostrarDetallesProveedor()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        GuardarProveedor(TextBox1.Text)
        TextBox1.Select()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox1.Select()

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        EliminarProveedor(TextBox1.Text)
        TextBox1.Select()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

        Label6.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label7.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke

        Panel1.Visible = True

        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        ''COdigo para que cuando vuelva a elegir la opcion de Proveedor se limpien los textbox 

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

        TextBox1.Select()

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

        Label7.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke


        Panel2.Visible = True

        Panel1.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        TextBox10.Text = ""
        TextBox10.Select()


    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

        Label8.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label7.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke


        Panel3.Visible = True

        Panel1.Visible = False
        Panel2.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        TextBox6.Text = ""
        TextBox6.Select()

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

        Label9.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label7.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke

        Panel4.Visible = True

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        TextBox9.Text = ""
        TextBox9.Select()

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

        Label10.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label7.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke

        Panel5.Visible = True

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False

        TextBox8.Text = ""
        TextBox8.Select()


    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

        Label11.BackColor = Color.FromArgb(192, 200, 255)  ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label7.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label12.BackColor = Color.WhiteSmoke

        Panel6.Visible = True

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel7.Visible = False

        TextBox7.Text = ""
        TextBox7.Select()

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

        Label12.BackColor = Color.FromArgb(192, 200, 255) ''Codigo para sobrear el label elejido 

        Label6.BackColor = Color.WhiteSmoke
        Label8.BackColor = Color.WhiteSmoke
        Label9.BackColor = Color.WhiteSmoke
        Label7.BackColor = Color.WhiteSmoke
        Label10.BackColor = Color.WhiteSmoke
        Label11.BackColor = Color.WhiteSmoke

        Panel7.Visible = True

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False

        TextBox11.Text = ""
        TextBox11.Select()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        TextBox10.Text = ""
        TextBox10.Select()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        TextBox6.Text = ""
        TextBox6.Select()

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click

        TextBox8.Text = ""
        TextBox8.Select()

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

        TextBox9.Text = ""
        TextBox9.Select()

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        TextBox7.Text = ""
        TextBox7.Select()

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click

        TextBox11.Text = ""
        TextBox11.Select()

    End Sub

    ''Paquete de codigo para categorias eliminar y guardar.

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim categoria As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        categoria = Trim(TextBox10.Text)


        If TextBox10.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes ingresar una categoria.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox10.Select()

        Else


            sql = "Select * From Categorias where Nombre_De_Categoria = '" + categoria + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("La categoria ya existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox10.Select()

            Else

                sql = "Insert into Categorias values('" + categoria + "')"
                Ejecutar(sql)

                MessageBox.Show("Categoria guardada con éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox10.Text = ""
                MostrasListas()

                TextBox10.Select()


            End If

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If TextBox10.Text = "" Then

            MessageBox.Show("Debes seleccionar una categoria .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox10.Select()

        Else

            sql = "delete Categorias where Nombre_De_Categoria = '" + TextBox10.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Categoria eliminada con éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox10.Text = ""
            MostrasListas()

            TextBox10.Select()


        End If

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

        TextBox10.Text = ListBox2.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

    ''Paquete para Unidades de medicion 
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Dim medicion As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        medicion = Trim(TextBox6.Text)


        If TextBox6.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes Ingresar una Unidad de Medición.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox6.Select()

        Else


            sql = "Select * From Unidades_de_Medicion where Unidad_de_Medicion = '" + medicion + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("La unidad ya existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox6.Select()

            Else

                sql = "Insert into Unidades_de_Medicion values('" + medicion + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Unidad guardada con éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox6.Text = ""
                MostrasListas()

                TextBox6.Select()


            End If

        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If TextBox6.Text = "" Then

            MessageBox.Show("Debes seleccionar una unidad .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox6.Select()

        Else

            sql = "delete Unidades_de_Medicion where Unidad_de_Medicion = '" + TextBox10.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Unidad eliminada con éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox6.Text = ""
            MostrasListas()

            TextBox6.Select()


        End If

    End Sub

    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged

        TextBox6.Text = ListBox3.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

    ''Paquete para Fabricantes
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

        Dim marca As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        marca = Trim(TextBox9.Text)


        If TextBox9.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes Ingresar un Fabricante/Marca.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox9.Select()

        Else


            sql = "Select * From Fabricantes where Nombre_del_Fabricante = '" + marca + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("El Fabricante o Marca ya existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox9.Select()

            Else

                sql = "Insert into Fabricantes values('" + marca + "')"
                Ejecutar(sql)

                MessageBox.Show("Fabricante guardado con éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox9.Text = ""
                MostrasListas()

                TextBox9.Select()


            End If

        End If

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click

        If TextBox9.Text = "" Then

            MessageBox.Show("Debes Seleccionar una Marca o Fabricante .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox9.Select()

        Else

            sql = "delete Fabricantes where Nombre_del_Fabricante = '" + TextBox9.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Fabricante Eliminado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox9.Text = ""
            MostrasListas()

            TextBox9.Select()


        End If

    End Sub

    Private Sub ListBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox6.SelectedIndexChanged

        TextBox9.Text = ListBox6.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

    ''Paquete Ubicacion(Anaquel)
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click

        Dim anaquel As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        anaquel = Trim(TextBox8.Text)


        If TextBox8.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes Ingresar un Anaquel/Vitrina.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox8.Select()

        Else


            sql = "Select * From Anaqueles where Anaquel = '" + anaquel + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("El Anaquel o Vitrina ya existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox8.Select()

            Else

                sql = "Insert into Anaqueles values('" + anaquel + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Anaquel/Vitrina Guardado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox8.Text = ""
                MostrasListas()

                TextBox8.Select()


            End If

        End If

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        If TextBox8.Text = "" Then

            MessageBox.Show("Debes Seleccionar un Anaquel/Vitrina .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox8.Select()

        Else

            sql = "delete Anaqueles where Anaquel = '" + TextBox8.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Anaquel/Vitrina Eliminado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox8.Text = ""
            MostrasListas()

            TextBox8.Select()


        End If

    End Sub

    Private Sub ListBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox5.SelectedIndexChanged

        TextBox8.Text = ListBox5.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

    ''Paquete Ubicacion(Niveles)
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Dim nivel As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        nivel = Trim(TextBox7.Text)


        If TextBox7.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes Ingresar un Nivel.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox7.Select()

        Else


            sql = "Select * From Niveles where Nivel = '" + nivel + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("El Nivel ya Existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox7.Select()

            Else

                sql = "Insert into Niveles values('" + nivel + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Nivel Guardado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox7.Text = ""
                MostrasListas()

                TextBox7.Select()


            End If

        End If

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        If TextBox7.Text = "" Then

            MessageBox.Show("Debes Seleccionar un Nivel .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox7.Select()

        Else

            sql = "delete Niveles where Nivel = '" + TextBox7.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Nivel Eliminado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox7.Text = ""
            MostrasListas()

            TextBox7.Select()


        End If

    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged

        TextBox7.Text = ListBox4.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

    ''Paquete Tipo de Articulos
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click

        Dim tipo As String    ''Variable para obtener lo que da el usuario 

        Dim existe As String = ""     ''Variable con la cual sabre si el dato que estoy ingresando ya existe 

        tipo = Trim(TextBox11.Text)


        If TextBox11.Text = "" Then             '' if por si no ingreso nada al textbox 

            MessageBox.Show("Debes Ingresar un Tipo de Artículo.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox11.Select()

        Else


            sql = "Select * From Tipo_de_Articulos where Tipo_de_Articulo = '" + tipo + "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                existe = dr(0)

            End While
            con.Close()

            If existe <> "" Then       ''if por si lo que ingreso ya existe 

                MessageBox.Show("El Tipo de Artículo ya Existe.", "Integrated Pharmacy System", MessageBoxButtons.OK)
                TextBox11.Select()

            Else

                sql = "Insert into Tipo_de_Articulos values('" + tipo + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Tipo de Artículo Guardado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

                TextBox11.Text = ""
                MostrasListas()

                TextBox11.Select()


            End If

        End If

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click

        If TextBox11.Text = "" Then

            MessageBox.Show("Debes Seleccionar un Tipo de Artículo .", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox11.Select()

        Else

            sql = "delete Tipo_de_Articulos where Tipo_de_Articulo = '" + TextBox11.Text + "'"
            Ejecutar(sql)
            con.Close()

            MessageBox.Show("Tipo de Artículo Eliminado con Éxito .", "Integrated Pharmacy System", MessageBoxButtons.OK)

            TextBox11.Text = ""
            MostrasListas()

            TextBox11.Select()


        End If

    End Sub

    Private Sub ListBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox7.SelectedIndexChanged

        TextBox11.Text = ListBox7.SelectedItem      ''Codigo que arroja el valor que se selecciono 

    End Sub

End Class