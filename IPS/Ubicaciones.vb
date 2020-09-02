
Imports System.Data.SqlClient


Public Class Ubicaciones


    ''Metodo para llenar el listbox de los productos que no tienen ubicación 

    Public Sub SinUbicacion()

        ListBox1.Items.Clear()
        sql = "Select Codigo_Producto from Ubicacion where Anaquel  = 'POR DEFINIR' or Nivel='POR DEFINIR'"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox1.Items.Add(dr(0)) 'agregar los datos al listbox

        End While
        con.Close()

        Label15.Text = "Productos sin ubicar: " & ListBox1.Items.Count



    End Sub

    ''Metodo para que llene los combos

    Public Sub LlenarCombos()

        sql = "Select * from Anaqueles order by Anaquel asc"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox1.Items.Add(dr(0))
            ComboBox3.Items.Add(dr(0))

        End While
        con.Close()

        sql = "Select * from Niveles order by Nivel asc"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox2.Items.Add(dr(0)) 'agregar los datos al listbox
            ComboBox4.Items.Add(dr(0))

        End While
        con.Close()


        ''Llenar el combo de categorias 
        sql = "Select * from Categorias order by Nombre_De_Categoria asc"
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox5.Items.Add(dr(0)) 'agregar los datos al listbox

        End While
        con.Close()


    End Sub

    ''Modulo para que se muestre la info del producto

    Public Sub MostrarDatos()

        Dim codigo As String            ''Variable en donde obtengo el código de barras 

        Dim existe As String = ""     ''Variable en la que checo si el producto existe

        If TextBox1.Text = "" Then

            codigo = ListBox1.SelectedItem

            TextBox1.Text = codigo

        Else

            codigo = TextBox1.Text


        End If

        sql = "Select * from inventario where Codigo_Producto ='" & codigo & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            existe = dr(0)

        End While
        con.Close()


        If existe = "" Then

            MessageBox.Show("El producto no esta dado de Alta", "Integrated Pharmacy System")

            TextBox1.Text = ""

            TextBox3.Text = ""
            TextBox4.Text = ""
            Label17.Text = ""
            Label18.Text = ""

            ComboBox1.Text = Nothing
            ComboBox2.Text = Nothing



        Else

            sql = "Select Existencia, Precio  from inventario where Codigo_Producto ='" & codigo & "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                Label17.Text = dr(0) 'agregar los datos a las cajas 
                Label18.Text = Format(dr(1), "$0.00")            '' dar formato de mopneda 


            End While
            con.Close()


            sql = "Select Descripcion,Descripcion_Secundaria,Categoria from productos where Codigo_Producto ='" & codigo & "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                TextBox3.Text = dr(0) ''agregar los datos a las cajas 
                TextBox4.Text = dr(1)
                ComboBox5.Text = dr(2)

            End While
            con.Close()


            sql = "Select Anaquel,Nivel from ubicacion where Codigo_Producto ='" & codigo & "'"
            Ejecutar(sql)


            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                ComboBox1.Text = dr(0) 'agregar los datos al listbox
                ComboBox2.Text = dr(1)

            End While
            con.Close()

            TextBox1.Enabled = False
        End If


    End Sub


    ''Metodo para vaciar al momento de dar click en la lista 

    Public Sub CLearSelectedItem()

        TextBox1.Text = Nothing
        TextBox3.Text = Nothing
        TextBox4.Text = Nothing

        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing


        ComboBox5.Text = Nothing


    End Sub


    ''Modulo para actualizar info

    Public Sub Actualizar()

        Dim esta As String = ""

        sql = "select * from productos where Codigo_Producto = '" + TextBox1.Text + "'"
        Ejecutar(sql)


        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            esta = dr(0)

        End While
        con.Close()


        If TextBox1.Text = "" Then

            MessageBox.Show("Debes ingresar un un código de barra ", "Integrated Pharmacy System")

        Else


            If esta = "" Then

                MessageBox.Show("El producto que ingresaste no existe", "Integrated Pharmacy System")

                TextBox1.Text = ""

                TextBox1.Select()

                TextBox3.Text = ""
                TextBox4.Text = ""
                Label17.Text = ""
                Label18.Text = ""

                ComboBox1.Text = Nothing
                ComboBox2.Text = Nothing

            Else

                If TextBox3.Text = "" Then
                    MessageBox.Show("El articulo debe contener una descripción", "Integrated Pharmacy System")

                Else



                    sql = "update dbo.Ubicacion set Anaquel = '" + ComboBox1.Text + "', Descripcion = '" + TextBox3.Text +
                    "', Nivel = '" + ComboBox2.Text + "' where Codigo_Producto = '" + TextBox1.Text + "'"
                    Ejecutar(sql)


                    sql = "update dbo.Productos set Descripcion = '" + TextBox3.Text + "', Descripcion_Secundaria = '" + TextBox4.Text +
                    "', Categoria = '" + ComboBox5.Text + "' where Codigo_Producto = '" + TextBox1.Text + "'"
                    Ejecutar(sql)

                    MessageBox.Show("Información actualizada correctamente", "Integrated Pharmacy System")

                    TextBox1.Text = ""
                    TextBox1.Enabled = True
                    TextBox1.Select()

                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    Label17.Text = ""
                    Label18.Text = ""

                    ComboBox1.Text = Nothing
                    ComboBox2.Text = Nothing
                    ComboBox5.Text = Nothing



                    SinUbicacion()   ''Se carga de nuevo los articulos sin ubicacion

                End If

            End If

        End If


    End Sub

    '' Bloque de metodos para actualizar informacion por grupo de productos


    ''Metodo que revisa que el producto exista y lo lleva a la lista 

    Public Sub AgregarLista()


        Dim esta As String = ""                 ''Consulta en la cual vemos si el producto existe

        Dim valueInList As Boolean               ''Variable en donde asignaremos si el producto en la lista 


        sql = "select * from productos where Codigo_Producto = '" + TextBox2.Text + "'"
        Ejecutar(sql)


        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            esta = dr(0)

        End While
        con.Close()




        If TextBox2.Text = "" Then              ''Consulta en la cual revisamos si el text esta vacio 

            MessageBox.Show("Debes ingresar un código de barra ", "Integrated Pharmacy System")

        Else


            If esta = "" Then           ''Validamos si el producto existe

                MessageBox.Show("El producto que ingresaste no existe", "Integrated Pharmacy System")

                TextBox2.Text = ""
                TextBox2.Select()

            Else

                For a = 0 To ListBox2.Items.Count - 1               ''Ciclo en el cual reviso que el valor del textbox no este en la lista y asi no se repitan datos
                    If Trim(TextBox2.Text) = ListBox2.Items(a) Then
                        valueInList = True
                    Else
                        valueInList = False
                    End If
                Next


                If valueInList = True Then
                    MessageBox.Show("El producto que ingresaste ya esta en la lista", "Integrated Pharmacy System")

                    TextBox2.Text = ""
                    TextBox2.Select()
                Else
                    ListBox2.Items.Add(Trim(TextBox2.Text))

                    TextBox2.Text = ""
                    TextBox2.Select()
                End If


            End If

        End If

    End Sub

    ''Metodo que actuliza la lista de producto seleccionada

    Public Sub ActualizaLista()

        Dim codigo As String = ""   ''Variable con la cual ire asignando cada codigo de barra de la lista 


        If ListBox2.Items.Count = 0 Then

            MessageBox.Show("Debes ingresar al menos un producto para asignar ubicación ", "Integrated Pharmacy System")


        Else


            If ComboBox3.Text = "" Or ComboBox4.Text = "" Then

                MessageBox.Show("Debes asignar la ubicación Anaquel/Nivel ", "Integrated Pharmacy System")


            Else

                Dim resp As Integer

                resp = MsgBox("¿Seguro que deseas actualizar este grupo de productos? ", vbOKCancel, "Integrated Pharmacy System")  ''Codigo que confirma la eliminacion de un usuario

                If resp = 1 Then

                    For i = 0 To ListBox2.Items.Count - 1           ''For en el cual ire haciendo el recorrido y la actualizacion de cada prodcuto

                        codigo = ListBox2.Items(i)

                        sql = "update dbo.ubicacion set Anaquel = '" + ComboBox3.SelectedItem + "', Nivel = '" + ComboBox4.SelectedItem +
                            "' where Codigo_Producto = '" + codigo + "'"
                        Ejecutar(sql)


                        ComboBox3.Text = Nothing
                        ComboBox4.Text = Nothing


                    Next

                    MessageBox.Show("Productos actualizados satisfactoriamente", "Integrated Pharmacy System")
                    ListBox2.Items.Clear()

                Else

                End If

            End If

        End If


    End Sub


    Private Sub Ubicaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Select()
        SinUbicacion()
        LlenarCombos()
        CheckBox1.Checked = True
        CheckBox2.Checked = False

        TextBox3.CharacterCasing = CharacterCasing.Upper      ''Poner textbox en mayusculas o minusculas 
        TextBox4.CharacterCasing = CharacterCasing.Upper

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        CLearSelectedItem()
        MostrarDatos()
        ComboBox1.Select()


    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then              ''Codigo cuando ingresa el codigo de barras en el textbox y da enter

            If TextBox1.Text = "" Then

                MessageBox.Show("Debes ingresar un codgio de barra o seleccionarlo de la lista ", "Integrated Pharmacy System")


            Else


                MostrarDatos()


            End If

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox1.Text = ""
        TextBox1.Select()

        TextBox1.Enabled = True

        TextBox3.Text = ""
        TextBox4.Text = ""
        Label17.Text = ""
        Label18.Text = ""

        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing


        ComboBox5.Text = Nothing

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Busqueda_de_Articulo.codLog = "ubicacion"
        Busqueda_de_Articulo.Show()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Actualizar()


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        CheckBox2.Checked = False
        Panel1.Visible = True
        Panel2.Visible = False
        TextBox1.Select()
        SinUbicacion()

        TextBox1.Text = ""
        TextBox1.Enabled = True

        TextBox3.Text = ""
        TextBox4.Text = ""
        Label17.Text = ""
        Label18.Text = ""

        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        ComboBox5.Text = Nothing

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        CheckBox1.Checked = False
        Panel1.Visible = False
        Panel2.Visible = True
        TextBox2.Select()

        ComboBox3.Text = Nothing
        ComboBox4.Text = Nothing

        ListBox2.Items.Clear()

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyCode = Keys.Enter Then              ''Codigo cuando ingresa el codigo de barras en el textbox y da enter

            AgregarLista()

        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ActualizaLista()


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        ListBox2.Items.Clear()
        TextBox2.Text = ""
        ComboBox3.Text = Nothing
        ComboBox4.Text = Nothing
        TextBox2.Select()



    End Sub

    Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click
        CheckBox2.Checked = True
        CheckBox1.Checked = False

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        CheckBox1.Checked = True
        CheckBox2.Checked = False
    End Sub
End Class