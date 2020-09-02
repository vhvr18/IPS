

Imports System.Data.SqlClient


Public Class Actualizar_Costos_y_Precios

    Public codigo As String            ''Variable en donde obtengo el código de barras 

    ''Metodo para llenar el listbox de los productos que no tienen costo o precio 

    Public Sub WithoutCost()

        ListBox1.Items.Clear()
        sql = "Select Codigo_Producto from inventario where costo = 0 or precio = 0 order by Existencia desc "
        Conectar()

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ListBox1.Items.Add(dr(0)) 'agregar los datos al listbox

        End While
        con.Close()

        Label15.Text = "Productos sin costo o precio: " & ListBox1.Items.Count

    End Sub

    ''Codigo para mostrar los datos del producto  ya sea digitando el codigo o seleccionandolo de la lista
    Public Sub MostrarDatos()



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


        If existe = "" Then                                                 ''Verificamos que el articulo exista 

            MessageBox.Show("El producto no esta dado de Alta", "Integrated Pharmacy System")

            TextBox1.Text = Nothing


        Else

            ''Consultamos infomracion del producto 

            sql = "Select Existencia, Costo, Precio, Utilidad from inventario where Codigo_Producto ='" & codigo & "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                Label17.Text = dr(0) 'agregar los datos a las cajas 
                TextBox2.Text = Format(dr(1), "0.00")            '' dar formato de moneda 
                TextBox5.Text = Format(dr(2), "0.00")            '' dar formato de moneda 
                TextBox6.Text = Format(dr(3), "0.00")            '' dar formato de moneda 

            End While
            con.Close()


            sql = "Select Descripcion,Descripcion_Secundaria,Categoria from productos where Codigo_Producto ='" & codigo & "'"
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                Label10.Text = dr(0) ''agregar los datos a las cajas 
                Label11.Text = dr(1) ''agregar los datos a las cajas 


            End While
            con.Close()

            TextBox1.Enabled = False
            TextBox2.Select()

        End If


    End Sub

    ''Metodo para actualizar la ifnormacion del producto en este caso precio, costo y utilidad 
    Public Sub UpdateData()

        If TextBox2.Text = "" Then
            TextBox2.Text = 0
        End If

        If TextBox5.Text = "" Then
            TextBox5.Text = 0
        End If



        If Val(TextBox2.Text) = 0 Or Val(TextBox5.Text) = 0 Then                  ''Validamos que hayamos puesto el precio y costo

            MessageBox.Show("No puedes dejar el precio o costo en 0", "Integrated Pharmacy System")
            TextBox2.Text = ""
            TextBox2.Select()


        Else

            If Val(TextBox5.Text) >= Val(TextBox2.Text) Then                  ''Validamos que el precio sea igual o mayor al costo

                sql = "Update dbo.inventario set Costo =" & TextBox2.Text & ", Precio= " & TextBox5.Text & ",Utilidad= " & TextBox6.Text & " WHERE Codigo_Producto='" & TextBox1.Text & "'"
                Ejecutar(sql)
                con.Close()

                WithoutCost()
                ClearBoxes()


            Else

                MessageBox.Show("El precio debe ser mayor o igual al costo", "Integrated Pharmacy System")
                TextBox2.Text = ""
                TextBox2.Select()

            End If




        End If




    End Sub


    Public Sub CLearSelectedItem()

        TextBox1.Text = Nothing

        TextBox2.Text = Nothing
        TextBox5.Text = Nothing
        TextBox6.Text = Nothing

        Label10.Text = Nothing
        Label11.Text = Nothing
        Label17.Text = Nothing

    End Sub

    Public Sub ClearBoxes()

        TextBox1.Text = Nothing
        TextBox1.Enabled = True
        TextBox1.Select()

        TextBox2.Text = Nothing
        TextBox5.Text = Nothing
        TextBox6.Text = Nothing

        Label10.Text = Nothing
        Label11.Text = Nothing
        Label17.Text = Nothing

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

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

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress

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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ''Indicamos al modulo ubicaciones que estamos entrando desde la actualizacion de costos
        Busqueda_de_Articulo.codLog = "costos"
        Busqueda_de_Articulo.Show()

    End Sub

    Private Sub Actualizar_Costos_y_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
        WithoutCost()

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        CLearSelectedItem()
        MostrarDatos()

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

        ClearBoxes()

    End Sub

    Private Sub TextBox5_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox5.MouseClick
        TextBox5.Select()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

        If Val(TextBox5.Text) > Val(TextBox2.Text) Then

            TextBox6.Text = Val(TextBox5.Text) - Val(TextBox2.Text)

        Else

            TextBox6.Text = "0"

        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateData()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        If Val(TextBox5.Text) > Val(TextBox2.Text) Then

            TextBox6.Text = Val(TextBox5.Text) - Val(TextBox2.Text)

        Else

            TextBox6.Text = "0"

        End If

    End Sub
End Class