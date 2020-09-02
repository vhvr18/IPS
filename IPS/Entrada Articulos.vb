
Imports System.Data.SqlClient

Public Class Entrada_Articulos


    Dim da As SqlDataAdapter
    Dim dx As DataTable

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


    ''Metodo para calcular el numero de articulos ingresados

    Public Sub CountItem()

        Dim totalArti As Integer

        For a = 0 To DataGridView1.Rows.Count - 1

            totalArti = totalArti + (Val(DataGridView1.Item(2, a).Value))

        Next

        Label20.Text = "Total de articulos: " & totalArti

    End Sub

    'Metodo para mostrar datos de un articulo guardado
    Public Sub Codigo_Producto()

        Dim codigo_producto As String
        codigo_producto = TextBox1.Text

        sql = "select Descripcion,Descripcion_Secundaria from productos where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox2.Text = (dr(0))
            TextBox3.Text = (dr(1))

        End If
        con.Close()

        sql = "select Existencia,Stock_Min,Stock_Max,Costo,Precio from inventario where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox4.Text = (dr(0))
            TextBox5.Text = (dr(1))
            TextBox6.Text = (dr(2))
            TextBox7.Text = Format(dr(3), "Fixed")
            TextBox8.Text = Format(dr(4), "fixed")

        End If

        con.Close()

    End Sub

    Public Sub Llenar_grids()

        dx = New DataTable
        sql = "select * from EntradadeArticulos "
        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()


    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ''Codigo para solo numeros 
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress

        e.Handled = True

    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress

        If Char.IsNumber(e.KeyChar) Then            ''Codigo para numeros y simbolos 
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
        Else
            e.Handled = True
        End If

    End Sub

    ''Busqueda del articulo
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        Dim codigo As String
        Dim codigo2 As String = ""

        codigo = TextBox1.Text

        If TextBox1.Text = "" Then
            codigo = "algo"
        End If

        sql = "select Codigo_Producto from productos where Codigo_Producto = '" + TextBox1.Text + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo2 = dr(0)

        End While
        con.Close()

        If e.KeyCode = Keys.Enter Then

            If codigo = codigo2 Then

                Codigo_Producto()

                TextBox1.Enabled = False
                TextBox11.Select()

            ElseIf codigo = "algo" Then

                TextBox1.Select()
                MessageBox.Show("Debes ingresar un código de barra", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                MessageBox.Show("Este producto no ha sido dado de alta", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

                TextBox1.Text = ""

            End If

        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox1.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = "0"
        TextBox10.Text = "0"
        TextBox11.Text = "0"
        TextBox12.Text = "0"

        TextBox1.Select()


    End Sub

    ''Entrada de articulos 
    Public Sub Entrada_Articulos()

        Dim codigo_producto As String = ""
        Dim codigo_producto2 As String = ""
        Dim descripcion As String = ""
        Dim cantidad As String = ""


        Dim existencia As String = ""
        Dim costo As String = ""
        Dim precio As String = ""
        Dim utilidad As String = ""
        Dim confirmacion As Integer

        Dim nuevaExistencia As Integer

        cantidad = TextBox11.Text
        codigo_producto = TextBox1.Text
        costo = Trim(TextBox9.Text)
        precio = Trim(TextBox10.Text)
        existencia = Trim(TextBox11.Text)
        utilidad = Trim(TextBox12.Text)
        descripcion = TextBox2.Text

        Dim fecha As String = ""
        fecha = DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") ''Variables para la hora de entrada

        Dim nuevoprecio As Double
        Dim nuevoCosto As Double
        Dim nuevaUtilida As Double

        nuevaUtilida = Trim(TextBox12.Text)
        nuevoCosto = Trim(TextBox9.Text)
        nuevoprecio = Trim(TextBox10.Text)

        sql = "select Codigo_Producto from inventario where Codigo_Producto = '" & TextBox1.Text & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo_producto2 = dr(0)

        End While
        con.Close()

        If TextBox1.Text = "" And TextBox2.Text = "" Then

            'Comparación para verificar que todos los datos sean correctos y no queden campos vacios
            MessageBox.Show("Debes ingresar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf Val(TextBox10.Text) < Val(TextBox9.Text) Then

            MessageBox.Show("El precio del producto no puede ser menor que el costo", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            TextBox9.Text = "0"
            TextBox10.Text = "0"
            TextBox12.Text = "0"

            TextBox9.Select()

        ElseIf codigo_producto = codigo_producto2 Then

            confirmacion = MsgBox("Seguro que deseas ingresar este producto", vbOKCancel, "Integrated Pharmacy System")

            If confirmacion = 1 Then

                'sql = "update inventario set Existencia = Existencia + '" + existencia + "' where Codigo_Producto = " + codigo_producto + "'"
                'Ejecutar(sql)

                nuevaExistencia = Val(TextBox4.Text) + Val(TextBox11.Text)

                If TextBox9.Text = "0" Or TextBox9.Text = "" Then                     ''Si no actualiza precio ni costo el sistema tomara el ultimo costo y precio 

                    nuevoCosto = Trim(TextBox7.Text)

                End If

                If TextBox10.Text = "0" Or TextBox10.Text = "" Then

                    nuevoprecio = Trim(TextBox8.Text)

                End If

                If TextBox12.Text = "0" Or TextBox12.Text = "" Then

                    nuevaUtilida = Val(TextBox8.Text) - Val(TextBox7.Text)

                End If


                sql = "insert into Entradadearticulos values ('" & codigo_producto & "','" + descripcion + "','" & cantidad & "','" & TextBox7.Text &
                                                             "','" & nuevoCosto & "','" & TextBox8.Text & "','" & nuevoprecio & "','" & TextBox4.Text & "','" & nuevaExistencia & "')"
                Ejecutar(sql)
                con.Close()

                sql = "insert into Registro_EntradadeArticulos values ('" + fecha + "','" + ComboBox1.SelectedItem + "','" & codigo_producto & "','" + descripcion + "','" & cantidad & "','" & TextBox7.Text &
                                                             "','" & nuevoCosto & "','" & TextBox8.Text & "','" & nuevoprecio & "','" & TextBox4.Text & "','" & nuevaExistencia & "','" + Login.usuario + "')"
                Ejecutar(sql)
                con.Close()


                sql = "update dbo.inventario set Costo = '" & nuevoCosto & "', Precio = '" & nuevoprecio & "', Existencia = '" & nuevaExistencia & "', Utilidad = '" & nuevaUtilida & "'  where Codigo_Producto = '" & TextBox1.Text & "'"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Producto ingresado", "Integrated Pharmacy System")

                Llenar_grids()

                TextBox1.Enabled = True
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = "0"
                TextBox10.Text = "0"
                TextBox11.Text = "0"
                TextBox12.Text = "0"

                TextBox1.Select()
            End If

        End If


    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

        If Val(TextBox10.Text) > Val(TextBox9.Text) Then

            TextBox12.Text = Val(TextBox10.Text) - Val(TextBox9.Text)

        Else

            TextBox12.Text = "0"

        End If

    End Sub

    Private Sub TextBox11_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox11.KeyDown

        If e.KeyCode = Keys.Enter Then

            Entrada_Articulos()
            CountItem()

        End If

    End Sub

    Private Sub Entrada_Articulos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Today

        sql = "delete EntradadeArticulos"
        Ejecutar(sql)
        con.Close()



        TextBox9.Text = "0"
        TextBox10.Text = "0"
        TextBox11.Text = "0"
        TextBox12.Text = "0"

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LlenarExcel()

    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sql = "delete EntradadeArticulos"
        Ejecutar(sql)
        con.Close()

        Llenar_grids()

        CountItem()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Select()
    End Sub
End Class