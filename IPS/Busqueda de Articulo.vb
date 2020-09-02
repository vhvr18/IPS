Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail


Public Class Busqueda_de_Articulo


    Dim da As SqlDataAdapter
    Dim dx As DataTable            ''Para llenar grids 


    Public codart As String ''Variable para mandar el producuto selecccionado 

    Public codLog As String  ''Variable para identificar que interfaz esta haciendo uso de la pantalla 


    ''Codgio para hacer la busqueda de producto con un like y ver inofrmacion


    ''' <summary>
    ''' Consultas bien mamalonas 
    ''' </summary>


    Public Sub BuscarArticulos(objeto As String)

        DataGridView1.Columns.Clear()  ''Limpia las columnas para que el devuelva de manera ordenada la informacion el el dgv 

        If objeto = "" Then                 ''if por si el usuario no ingreso nada 

            MessageBox.Show("Debes ingresar un código de barras o descripción de producto ", "Integrated Pharmacy System", MessageBoxButtons.OK)
            TextBox1.Select()

        Else


            If Val(objeto) > 0 Then                 ''Si ingreso un numero busca por codigo de barra 

                dx = New DataTable


                sql = "select productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,inventario.Existencia, inventario.Precio 
                    from productos productos inner join inventario  inventario on productos.Codigo_Producto = inventario.Codigo_Producto 
                    where productos.Codigo_Producto  = '" & objeto & "' order by inventario.Existencia desc"

                Conectar()
                da = New SqlDataAdapter(sql, con)
                da.Fill(dx)
                DataGridView1.DataSource = dx
                con.Close()



            Else            ''De lo contrario busca por descripcion o descripcion secundaria

                dx = New DataTable


                sql = "select productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,inventario.Existencia, inventario.Precio 
                    from productos productos inner join inventario  inventario on productos.Codigo_Producto = inventario.Codigo_Producto 
                    where productos.Descripcion  like '%" & objeto & "%' or productos.Descripcion_Secundaria like '%" & objeto & "%' order by inventario.Existencia desc"

                Conectar()
                da = New SqlDataAdapter(sql, con)
                da.Fill(dx)
                DataGridView1.DataSource = dx
                con.Close()


            End If


        End If




    End Sub

    Private Sub Busqueda_de_Articulo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Select()
        TextBox1.CharacterCasing = CharacterCasing.Upper


    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then

            BuscarArticulos(TextBox1.Text)
            TextBox1.Text = ""
            TextBox1.Select()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        BuscarArticulos(TextBox1.Text)
        TextBox1.Text = ""
        TextBox1.Select()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        DataGridView1.SelectionMode = DataGridView1.SelectionMode.FullRowSelect

        If DataGridView1.SelectedCells.Count <> 0 Then

            codart = DataGridView1.SelectedCells(0).Value.ToString

        End If

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        DataGridView1.SelectionMode = DataGridView1.SelectionMode.FullRowSelect

        If codLog = "ubicacion" Then

            If DataGridView1.SelectedCells.Count <> 0 Then

                codart = DataGridView1.SelectedCells(0).Value.ToString

            End If

            Ubicaciones.TextBox1.Text = codart
            Ubicaciones.MostrarDatos()
            Me.Close()
            codLog = ""



        End If



        If codLog = "venta" Then

            If DataGridView1.SelectedCells.Count <> 0 Then

                codart = DataGridView1.SelectedCells(0).Value.ToString

            End If

            PuntoDeVenta.TextBox1.Text = codart
            PuntoDeVenta.BuscarArticulo(codart)
            PuntoDeVenta.Operacion()
            PuntoDeVenta.TextBox1.Text = ""
            Me.Close()
            codLog = ""

        End If


        If codLog = "costos" Then

            If DataGridView1.SelectedCells.Count <> 0 Then

                codart = DataGridView1.SelectedCells(0).Value.ToString

            End If

            Actualizar_Costos_y_Precios.TextBox1.Text = codart
            Actualizar_Costos_y_Precios.MostrarDatos()

            Me.Close()
            codLog = ""

        End If






    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If codart = "" Then

            MessageBox.Show("Debes seleccionar un producto ", "Integrated Pharmacy System", MessageBoxButtons.OK)

        Else

            Detalles_del_Producto.Show()

        End If

    End Sub
End Class