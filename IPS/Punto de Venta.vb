Imports System.Data.SqlClient

Public Class PuntoDeVenta

    Dim da As SqlDataAdapter        ''Variables para hacer funcionar el DataGrid
    Dim dx As DataTable

    Dim cont As Integer

    Dim codart As String                ''Variable para quitar articlos de la tabla de preventas o agregar 


    Public totalArticulos As Integer       ''Variable para calcular el total de los articulos
    Public totalVenta As Double              ''Variable para pasar el monto total 


    Public Sub LlenarGrid()             '' Llena el grid de la preventa 

        dx = New DataTable
        sql = " select * from preventas "
        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx

        con.Close()

    End Sub

    ''Calcula el total de la compra
    Public Sub Operacion()

        Dim total As Single                 ''Variable para calcular el total de venta
        Dim totalArti As Integer


        For a = 0 To DataGridView1.Rows.Count - 1

            total = total + (Val(DataGridView1.Item(3, a).Value * DataGridView1.Item(4, a).Value))

        Next

        For a = 0 To DataGridView1.Rows.Count - 1

            totalArti = totalArti + (Val(DataGridView1.Item(3, a).Value))

        Next

        TextBox4.Text = Format(total, "$#,##0.00")
        TextBox5.Text = totalArti

    End Sub

    ''Busca el articulo, sus existencias y lo agrega al carrito
    Public Sub BuscarArticulo(producto As String)

        Dim existencia As Integer ''Variable con la cual reviso si hay existencias del prdoucto

        Dim descripcion As String = ""      ''Variable que nos ayuda a saber si ya hay un producto en el carrito 

        Dim cant As Integer         ''Variable para saber cuantos productos hay en preventas


        Dim existe As String = ""           ''Variable que ayuda para saber si el producto existe en la tabla 

        Dim precio As Double     ''Variable para llevar el control del total en la tabla preventas

        Format(precio, "00.00")

        sql = "Select Codigo_Producto from productos where Codigo_Producto= '" + producto + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)                          ''Leemos la tabla productos para saber si el prodcuto existe 
        dr = com.ExecuteReader

        While dr.Read

            existe = dr(0)

        End While
        con.Close()

        sql = "Select Existencia,Precio from inventario where Codigo_Producto= '" + producto + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)                          ''Leemos en la tabla inventarios si tenemos existencias 
        dr = com.ExecuteReader

        While dr.Read

            existencia = dr(0)
            precio = dr(1)

        End While
        con.Close()

        sql = "select * from preventas where codigo_producto = '" + producto + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader                          ''Leemos la tabla preventas para ver si agregamos solo cantidad o todo el producto al grid 

        While dr.Read

            descripcion = dr(1) '
            cant = dr(3)

            If cant = Nothing Then
                cant = 0
            End If

        End While
        con.Close()

        If existe = "" Then

            MessageBox.Show("Este producto no esta dado de alta", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.None)

        Else

            If descripcion = "" Then

                If existencia > 0 Then                  ''Se ingresa por primera vez en el grid

                    sql = "Exec sp_InsVenta '" + producto + "'"
                    Ejecutar(sql)
                    con.Close()

                    LlenarGrid()

                    cont = cant
                    producto = ""

                Else

                    MessageBox.Show("El producto no tiene existencias.", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.None)


                End If
            Else

                If cant >= existencia Then

                    MessageBox.Show("No hay producto suficiente ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.None)

                Else

                    cont = cant + 1


                    sql = "update dbo.preventas set cantidad = '" & cont & "', Total = '" & (cont * precio) & "' where codigo_producto = '" + producto + "'"
                    Ejecutar(sql)

                    con.Close()

                    LlenarGrid()

                End If


            End If

        End If

        cont = cant

        producto = ""

        con.Close()

    End Sub

    ''Quita articulos del carrito de compra
    Public Sub ELiminaCantidad(codigo As String)

        Dim cantidad As Integer
        Dim precio As Double
        Dim total As Double

        sql = "select * from preventas where Codigo_Producto = '" + codigo + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)                          ''Leemos la tabla productos para saber si el prodcuto existe 
        dr = com.ExecuteReader

        While dr.Read

            cantidad = dr(3) '
            precio = dr(4)
            total = dr(5)

        End While
        con.Close()

        If cantidad <= 1 Then

            sql = "delete preventas where Codigo_Producto = '" + codigo + "'"
            Ejecutar(sql)
            con.Close()

            LlenarGrid()
            Operacion()

        Else

            sql = "Update preventas set cantidad = cantidad - 1, Total ='" & (total - precio) & "' where Codigo_Producto = '" + codigo + "'"
            Ejecutar(sql)
            con.Close()

            LlenarGrid()
            Operacion()



        End If


        con.Close()

    End Sub

    ''Agrega la cantidad con un numero 
    Public Sub AgrgarCantidad()


        Dim cantidad As String
        Dim existencia As Integer

        Dim precio As Double

        Try

            If codart = "" Then                     ''Codigo que valida si ya selecciono un producto en el grid 

                MessageBox.Show("Debes seleccionar un producto ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else                                       ''Si lo selecciono arrojara un inputbox

                cantidad = InputBox("Agrega la cantidad deseada", "Integrated Pharmacy System")


                If cantidad = "" Then                                   ''Si cancela la operacion no pasara nada 

                Else                                                           ''De lo contrario buscara el producto y vera si hay existencias 

                    sql = "Select * from inventario where Codigo_Producto= '" & codart & "'"
                    Ejecutar(sql)

                    com = New SqlCommand(sql, con)                          ''Leemos la tabla productos para saber si el prodcuto existe 
                    dr = com.ExecuteReader

                    While dr.Read

                        existencia = dr(4)
                        precio = dr(8)

                    End While
                    con.Close()

                    If existencia = 0 Then                                  ''valida las existencias 

                        MessageBox.Show("El Producto no tiene existencias ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else

                        If Val(cantidad) > existencia Then              ''Revisa si la cantidad solicitada no es mayor a la existencia del producto

                            MessageBox.Show("El Producto no tiene suficiente existencias ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Else

                            If cantidad = 0 Then                    ''Si el usuario mete un 0 le mandara un mensaje de que no puede hacerlo 

                                MessageBox.Show("Debes ingresar una cantidad mayor a 0 ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            Else                            ''Si cumple todo agregara la cantidad solicitada

                                sql = "update dbo.preventas set cantidad = '" & cantidad & "',Total = '" & (cantidad * precio) & "' where codigo_producto = '" + codart + "'"
                                Ejecutar(sql)

                                con.Close()

                                LlenarGrid()

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Debes ingresar la cantidad con números ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Operacion()




    End Sub

    ''Load de la ventana
    Private Sub PuntoDeVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        sql = "delete preventas"
        Ejecutar(sql)


        TextBox1.Select()
        TextBox1.CharacterCasing = CharacterCasing.Lower  ''Pone los caracteres del textbox en minusculas
        '' Me.FormBorderStyle = FormBorderStyle.None ' Borrar Borde de la venta (Formulario)


        ''Fecha del dia de hoy
        TextBox2.Text = Now.Day & "/" & Now.Month & "/" & Now.Year

    End Sub

    ''Salir del modulo ventas
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click


        Dim resp As Integer

        resp = MsgBox("¿Seguro que desea salir del Punto de Venta? ", vbOKCancel, "Integrated Pharmacy System")  ''Codigo que confirma la eliminacion de un usuario

        If resp = 1 Then

            If Val(TextBox4.Text) > 0 Then
                sql = "delete preventas"
                Ejecutar(sql)


            End If

            Me.Close()
            Principal.Show()

        Else

        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        If Char.IsWhiteSpace(e.KeyChar) Then 'quitar espacios
            e.Handled = True

        End If

        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        Dim producto As String

        If e.KeyCode = Keys.Enter Then

            producto = Trim(TextBox1.Text)

            BuscarArticulo(producto)
            Operacion()
            TextBox1.Text = ""

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If codart = "" Then
            MessageBox.Show("Debes seleccionar un producto ", "Integrated Pharmacy System", MessageBoxButtons.OK)
            TextBox1.Select()

        Else
            ELiminaCantidad(codart)
            TextBox1.Select()

        End If

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick


        DataGridView1.SelectionMode = DataGridView1.SelectionMode.FullRowSelect

        If DataGridView1.SelectedCells.Count <> 0 Then

            codart = DataGridView1.SelectedCells(0).Value.ToString

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        DataGridView1.Columns.Clear()  ''Limpia las columnas para que el devuelva de manera ordenada la informacion el el dgv 


        sql = "delete preventas"                ''Cancelar la compra 
        Ejecutar(sql)

        LlenarGrid()
        Operacion()
        TextBox1.Select()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Ticket.Show()   ''Quitar esta linea 


        If TextBox4.Text = "0" Or TextBox4.Text = "0.00" Then

        Else

            totalArticulos = TextBox5.Text
            'totalVenta = Format((TextBox4.Text), "fixed")
            totalVenta = Format(TextBox4.Text, "fixed")
            Venta.Show()

        End If

    End Sub

    ''Codigo para que agregue una cantidad con numero 
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        AgrgarCantidad()
        TextBox1.Select()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Busqueda_de_Articulo.Show()
        TextBox1.Select()
        Busqueda_de_Articulo.codLog = "venta"

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Reportes.MdiParent = Principal
        Reportes.Show()



    End Sub
End Class