
Imports System.Data.SqlClient



Public Class Pedidos

    Dim da As SqlDataAdapter
    Dim dx As DataTable            ''Para llenar grids 

    ''Metodo para llenar el excel 
    Public Sub LlenarExcel()

        'Creamos las variables

        Dim exApp As New Microsoft.Office.Interop.Excel.Application

        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook

        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

        Try

            'Añadimos el Libro al programa, y la hoja al libro

            exLibro = exApp.Workbooks.Add

            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?

            Dim NCol As Integer = DataGridView1.ColumnCount

            Dim NRow As Integer = DataGridView1.RowCount

            'Aqui recorremos todas las filas, y por cada fila todas las columnas

            'y vamos escribiendo.

            For i As Integer = 1 To NCol

                exHoja.Cells.Item(1, i) = DataGridView1.Columns(i - 1).Name.ToString

            Next

            For Fila As Integer = 0 To NRow - 1

                For Col As Integer = 0 To NCol - 1

                    exHoja.Cells.Item(Fila + 2, Col + 1) = DataGridView1.Item(Col, Fila).Value

                Next

            Next

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna

            'se ajuste al texto

            exHoja.Rows.Item(1).Font.Bold = 1

            exHoja.Rows.Item(1).HorizontalAlignment = 3

            exHoja.Columns.AutoFit()

            'Aplicación visible

            exApp.Application.Visible = True

            exHoja = Nothing

            exLibro = Nothing

            exApp = Nothing

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")


        End Try



    End Sub


    Private Sub Pedidos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ''Al cargar la pantalla manda una lista recomendada de productos a surtir

        dx = New DataTable

        sql = "select productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,inventario.Existencia, inventario.Costo ,inventario.Precio 
                    from productos productos inner join inventario  inventario on productos.Codigo_Producto = inventario.Codigo_Producto 
                    where inventario.Existencia  <= '" & 3 & "'"

        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

        TextBox1.Select()


    End Sub


    Public Sub GenerarPedido(objeto As String)

        DataGridView1.Columns.Clear()  ''Limpia las columnas para que devuelva de manera ordenada la informacion en el dgv 

        If TextBox1.Text = "" Then                 ''if por si el usuario no ingreso nada 

            MessageBox.Show("Debes ingresar a partir de cuantas existencias " + vbLf + "quieres generar el pedido.", "Integrated Pharmacy System", MessageBoxButtons.OK)
            TextBox1.Text = ""
            TextBox1.Select()

        Else


            If objeto <> "" Then                 ''Si selecciono un numero de existncias el programa generara la lista

                dx = New DataTable

                sql = "select productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,inventario.Existencia, inventario.Costo ,inventario.Precio 
                    from productos productos inner join inventario  inventario on productos.Codigo_Producto = inventario.Codigo_Producto 
                    where inventario.Existencia  <= '" & objeto & "' order by inventario.Existencia  asc"

                Conectar()
                da = New SqlDataAdapter(sql, con)
                da.Fill(dx)
                DataGridView1.DataSource = dx
                con.Close()

                TextBox1.Text = ""


            End If

        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        GenerarPedido(TextBox1.Text)


    End Sub

    Private Sub Pedidos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        MessageBox.Show("Este es un pedido recomendado por el sistema." + vbLf + "Tomando en cuenta productos con menos de 3 existencias.", "Integrated Pharmacy System", MessageBoxButtons.OK)

    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LlenarExcel()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GenerarPedido(TextBox1.Text)
        End If
    End Sub
End Class