Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class Inventarios

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

    Private Sub Inventarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        dx = New DataTable

        sql = "select productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,
	    inventario.Existencia, inventario.Precio, ubicacion.Anaquel, ubicacion.Nivel    
        from productos productos, inventario  inventario, ubicacion ubicacion where  productos.Codigo_Producto = inventario.Codigo_Producto and productos.Codigo_Producto = ubicacion.Codigo_Producto  order by Existencia asc
        "

        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LlenarExcel()

    End Sub
End Class