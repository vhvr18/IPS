Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class Articulo


    Dim da As SqlDataAdapter                ''Para grids


    ''Metodo para llenar los combox con su info de las bd 

    Public Sub LlenarCombox()

        sql = "Select * From Proveedores order by Nombre_Proveedor asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox5.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Categorias order by Nombre_De_Categoria asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox1.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Unidades_de_Medicion order by Unidad_de_Medicion asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox3.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Fabricantes order by Nombre_del_Fabricante asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox2.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Anaqueles order by Anaquel asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox6.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Niveles order by Nivel asc"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox7.Items.Add(dr(0))

        End While
        con.Close()


        sql = "Select * From Tipo_de_Articulos order by Tipo_de_Articulo asc "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ComboBox4.Items.Add(dr(0))

        End While
        con.Close()

    End Sub

    Public Sub Codigo_Producto() 'Metodo para mostrar datos de un articulo guardado

        Dim codigo_producto As String
        codigo_producto = TextBox1.Text

        sql = "select Descripcion,Descripcion_Secundaria,Categoria,Fabricante,Unidad_Medicion,Presentacion,Tipo_Articulo from productos where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox2.Text = (dr(0))
            TextBox13.Text = (dr(1))
            ComboBox1.Text = (dr(2))
            ComboBox2.Text = (dr(3))
            ComboBox3.Text = (dr(4))
            TextBox3.Text = (dr(5))
            ComboBox4.Text = (dr(6))

        End If

        sql = "select Proveedor,Numero_Documento,Fecha,Existencia,Stock_Min,Stock_Max,Costo,Precio,Utilidad from inventario where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            ComboBox5.Text = (dr(0))
            TextBox4.Text = (dr(1))
            DateTimePicker1.Value = (dr(2))
            TextBox6.Text = (dr(3))
            TextBox7.Text = (dr(4))
            TextBox8.Text = (dr(5))
            TextBox9.Text = Format(dr(6), "fixed")
            TextBox10.Text = Format(dr(7), "fixed")
            TextBox11.Text = Format(dr(8), "fixed")

        End If

        sql = "select Sucursal,Anaquel,Nivel from ubicacion where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox12.Text = (dr(0))
            ComboBox6.Text = (dr(1))
            ComboBox7.Text = (dr(2))

        End If

        TextBox1.Enabled = False
        ComboBox5.Enabled = False
        TextBox4.Enabled = False
        DateTimePicker1.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim codigo_producto As String
        Dim codigo_producto2 As String = ""

        Dim descripcion As String
        Dim descripcion_secundaria As String

        Dim categoria As String
        Dim fabricante As String
        Dim unidad_medicion As String
        Dim presentacion As String
        Dim tipo_articulo As String

        Dim proveedor As String
        Dim numero_documento As String
        Dim año As String = DateTimePicker1.Value.Year
        Dim mes As String = DateTimePicker1.Value.Month
        Dim dia As String = DateTimePicker1.Value.Day
        Dim hora As String = DateTimePicker1.Value.Hour
        Dim minutos As String = DateTimePicker1.Value.Minute
        Dim segundos As String = DateTimePicker1.Value.Second
        Dim existencia As String
        Dim stock_min As String
        Dim stock_max As String
        Dim costo As String
        Dim precio As String
        Dim utilidad As String

        Dim sucursal As String
        Dim anaquel As String
        Dim nivel As String

        Dim confirmacion As Integer

        codigo_producto = TextBox1.Text
        descripcion = Trim(TextBox2.Text)
        descripcion_secundaria = Trim(TextBox13.Text)
        categoria = ComboBox1.Text
        fabricante = ComboBox2.Text
        unidad_medicion = ComboBox3.Text
        presentacion = Trim(TextBox3.Text)
        tipo_articulo = ComboBox4.Text

        proveedor = ComboBox5.Text
        numero_documento = Trim(TextBox4.Text)
        existencia = Trim(TextBox6.Text)
        stock_min = Trim(TextBox7.Text)
        stock_max = Trim(TextBox8.Text)
        costo = Trim(TextBox9.Text)
        precio = Trim(TextBox10.Text)
        utilidad = Trim(TextBox11.Text)

        sucursal = Trim(TextBox12.Text)
        anaquel = ComboBox6.Text
        nivel = ComboBox7.Text

        sql = "select Codigo_Producto from productos where Codigo_Producto = '" + codigo_producto + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo_producto2 = dr(0)


        End While


        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "[-seleccionar-]" Or ComboBox2.Text = "[-seleccionar-]" Or ComboBox3.Text = "[-seleccionar-]" Or TextBox3.Text = "" Or ComboBox4.Text = "[-seleccionar-]" Or ComboBox5.Text = "[-seleccionar-]" Or TextBox4.Text = "" Or DateTimePicker1.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Or TextBox10.Text = "" Or TextBox11.Text = "" Or TextBox12.Text = "" Or ComboBox6.Text = "[-seleccionar-]" Or ComboBox7.Text = "[-seleccionar-]" Then

            'Comparación para verificar que todos los datos sean correctos y no queden campos vacios
            MessageBox.Show("Asegúrese de haber ingresado todos los datos", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Select()

        Else


            If Val(TextBox10.Text) < Val(TextBox9.Text) Then

                MessageBox.Show("El costo no puede ser mayor al precio" + vbLf + " Por favor verifiquelo", "Integrated Pharmacy System")

                TextBox9.Text = ""
                TextBox10.Text = ""
                TextBox11.Text = ""

                TextBox9.Select()

            Else

                If Trim(TextBox1.Text) = codigo_producto2 Then

                    'Compara si existe el dato para modificar los datos y actualizar

                    confirmacion = MsgBox("¿Seguro que desea guardar los cambios realizados a este articulo?", vbOKCancel, "Integrated Pharmacy System")

                    If confirmacion = 1 Then

                        sql = "exec pd_actualizar_productos '" + codigo_producto + "','" + descripcion + "','" + descripcion_secundaria + "','" + categoria + "','" + fabricante + "','" + unidad_medicion + "','" + presentacion + "','" + tipo_articulo + "'"
                        Ejecutar(sql)

                        sql = "exec pd_actualizar_ubicacion '" + codigo_producto + "','" + TextBox2.Text + "','" + sucursal + "','" + anaquel + "','" + nivel + "'"
                        Ejecutar(sql)

                        MessageBox.Show("Los cambios realizados han sido guardados de manera exitosa", "Integrated Pharmacy System")

                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox13.Text = ""
                        ComboBox1.Text = Nothing
                        ComboBox2.Text = Nothing
                        ComboBox3.Text = Nothing
                        TextBox3.Text = ""
                        ComboBox4.Text = Nothing
                        ComboBox5.Text = Nothing
                        TextBox4.Text = ""
                        DateTimePicker1.Text = ""
                        TextBox6.Text = ""
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        TextBox9.Text = ""
                        TextBox10.Text = ""
                        TextBox11.Text = ""
                        ComboBox6.Text = Nothing
                        ComboBox7.Text = Nothing

                        TextBox1.Enabled = True
                        ComboBox5.Enabled = True
                        TextBox4.Enabled = True
                        DateTimePicker1.Enabled = True
                        TextBox6.Enabled = True
                        TextBox7.Enabled = True
                        TextBox8.Enabled = True
                        TextBox9.Enabled = True
                        TextBox10.Enabled = True
                        TextBox11.Enabled = True

                        TextBox1.Select()

                    End If

                Else

                    'Si todos los campos estan llenos guarda los datos atraves de los procedimientos almacenados

                    confirmacion = MsgBox("¿Seguro que desea dar de alta este articulo?", vbOKCancel, "Integrated Pharmacy System")

                    If confirmacion = 1 Then

                        sql = "exec pd_productos '" + codigo_producto + "','" + descripcion + "','" + descripcion_secundaria + "','" + categoria + "','" + fabricante + "','" + unidad_medicion + "','" + presentacion + "','" + tipo_articulo + "'"
                        Ejecutar(sql)

                        sql = "exec pd_inventario '" + codigo_producto + "','" + proveedor + "','" + numero_documento + "','" + año + "/" + mes + "/" + dia + " " + hora + ":" + minutos + ":" + segundos + "','" + existencia + "','" + stock_min + "','" + stock_max + "','" + costo + "','" + precio + "','" + utilidad + "'"
                        Ejecutar(sql)

                        sql = "exec pd_ubicacion '" + codigo_producto + "','" + TextBox2.Text + "','" + sucursal + "','" + anaquel + "','" + nivel + "'"
                        Ejecutar(sql)

                        MessageBox.Show("Se ha dado de alta al articulo exitosamente", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox13.Text = ""
                        ComboBox1.Text = Nothing
                        ComboBox2.Text = Nothing
                        ComboBox3.Text = Nothing
                        TextBox3.Text = ""
                        ComboBox4.Text = Nothing
                        ComboBox5.Text = Nothing
                        TextBox4.Text = ""
                        DateTimePicker1.Text = ""
                        TextBox6.Text = "0"
                        TextBox7.Text = "0"
                        TextBox8.Text = "0"
                        TextBox9.Text = "0"
                        TextBox10.Text = "0"
                        TextBox11.Text = "0"
                        ComboBox6.Text = Nothing
                        ComboBox7.Text = Nothing

                        TextBox1.Select()
                    End If

                End If


            End If

        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox11.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        Dim codigo As String
        Dim codigo2 As String = ""

        codigo = trim(TextBox1.Text)

        If Trim(TextBox1.Text) = "" Then
            codigo = "algo"
        End If

        sql = "select Codigo_Producto from productos where Codigo_producto = '" & TextBox1.Text & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo2 = dr(0)

        End While

        If e.KeyCode = Keys.Enter Then

            If codigo = codigo2 Then

                Codigo_Producto()

            Else

                TextBox2.Select()

            End If

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox13.Text = ""
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        ComboBox3.Text = Nothing
        TextBox3.Text = ""
        ComboBox4.Text = Nothing
        ComboBox5.Text = Nothing
        TextBox4.Text = ""
        DateTimePicker1.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""

        ComboBox6.Text = Nothing
        ComboBox7.Text = Nothing

        TextBox1.Enabled = True
        ComboBox5.Enabled = True
        TextBox4.Enabled = True
        DateTimePicker1.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True
        TextBox11.Enabled = True

        TextBox1.Select()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim confirmacion As Integer

        If TextBox1.Text = "" Then

            MessageBox.Show("Ingresa el código de barras del producto a eliminar", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Select()


        Else

            confirmacion = MsgBox("Seguro que desea eliminar este articulo", vbOKCancel, "Integrated Pharmacy System")

            If confirmacion = 1 Then

                sql = "Delete productos where Codigo_Producto ='" + TextBox1.Text + "'"
                Ejecutar(sql)

                sql = "Delete inventario where Codigo_Producto ='" + TextBox1.Text + "'"
                Ejecutar(sql)

                sql = "Delete ubicacion where Codigo_Producto ='" + TextBox1.Text + "'"
                Ejecutar(sql)

                MessageBox.Show("Articulo eliminado con éxito", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox13.Text = ""
                ComboBox1.Text = Nothing
                ComboBox2.Text = Nothing
                ComboBox3.Text = Nothing
                TextBox3.Text = ""
                ComboBox4.Text = Nothing
                ComboBox5.Text = Nothing
                TextBox4.Text = ""
                DateTimePicker1.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                TextBox11.Text = ""
                ComboBox6.Text = Nothing
                ComboBox7.Text = Nothing

                TextBox1.Enabled = True
                ComboBox5.Enabled = True
                TextBox4.Enabled = True
                DateTimePicker1.Enabled = True
                TextBox6.Enabled = True
                TextBox7.Enabled = True
                TextBox8.Enabled = True
                TextBox9.Enabled = True
                TextBox10.Enabled = True
                TextBox11.Enabled = True

                TextBox1.Select()


            End If

        End If

    End Sub

    Private Sub Articulo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = Today
        LlenarCombox()

        sql = "Select * from Sucursales"
        Ejecutar(sql)
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox12.Text = (dr(1))

        End If

        TextBox2.CharacterCasing = CharacterCasing.Upper            ''Poner los textbox en mayusculas o minisculas
        TextBox13.CharacterCasing = CharacterCasing.Upper

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

        If Val(TextBox10.Text) >= Val(TextBox9.Text) Then

            TextBox11.Text = Val(TextBox10.Text) - Val(TextBox9.Text)

        ElseIf TextBox10.Text = "" Or Val(TextBox10.Text) < Val(TextBox9.Text) Then

            TextBox11.Text = "0"

        End If

    End Sub


End Class
