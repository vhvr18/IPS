
Imports System.Data.SqlClient

Public Class Salidas_Articulos


    Dim da As SqlDataAdapter        ''Variables para los datagrids
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

    ''Metodo para calcular el numero de articulos retirados

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

        sql = "select Existencia,Stock_Min,Stock_Max from inventario where Codigo_Producto= '" + codigo_producto + "'"
        Conectar()
        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        If dr.Read Then

            TextBox4.Text = (dr(0))
            TextBox5.Text = (dr(1))
            TextBox6.Text = (dr(2))

        End If
        con.Close()

    End Sub

    Public Sub Llenar_grids()

        dx = New DataTable
        sql = "Select * From SalidaDeArticulos"
        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

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

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    ''Busca el articulo
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        Dim codigo As String
        Dim codigo2 As String = ""

        codigo = TextBox1.Text

        If TextBox1.Text = "" Then
            codigo = "algo"
        End If

        sql = "select Codigo_Producto from productos where Codigo_Producto = '" & TextBox1.Text & "'"
        Ejecutar(Sql)

        com = New SqlCommand(Sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo2 = dr(0)

        End While
        con.Close()

        If e.KeyCode = Keys.Enter Then

            If codigo = codigo2 Then

                Codigo_Producto()

                TextBox1.Enabled = False
                TextBox7.Select()


            ElseIf codigo = "algo" Then

                TextBox1.Select()
                MessageBox.Show("Debes ingresar un código de Barra", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                MessageBox.Show("Este producto no ha sido dado de alta", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

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

        TextBox1.Select()

    End Sub

    ''Salida de Articulos
    Public Sub Baja_Articulos()

        Dim fecha As String = ""
        fecha = DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") ''Variables para la hora de entrada
        'fecha = Date.Today.GetDateTimeFormats.ToString("yyyy-MM-dd HH:mm:ss")


        Dim codigo_producto As String
        Dim codigo_producto2 As String = ""
        Dim confirmacion As Integer

        codigo_producto = TextBox1.Text

        sql = "select Codigo_Producto from inventario where Codigo_Producto = '" & TextBox1.Text & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            codigo_producto2 = dr(0)

        End While
        con.Close()

        If TextBox7.Text = "" Or TextBox7.Text = "0" Then

            MessageBox.Show("No hay nada por retirar", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        ElseIf Val(TextBox7.Text) > Val(TextBox4.Text) Then

            MessageBox.Show("No Puedes retirar mas piezas de las que existen", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            TextBox7.Text = ""

        ElseIf codigo_producto = codigo_producto2 Then

            confirmacion = MsgBox("¿Seguro que deseas retirar estos articulos?", vbOKCancel, "Integrated Sales System")

            If confirmacion = 1 Then

                sql = "update inventario set Existencia = Existencia - " & TextBox7.Text & " where Codigo_Producto = '" & TextBox1.Text & "'"
                Ejecutar(sql)
                con.Close()

                sql = "Insert into SalidaDeArticulos values ('" & TextBox1.Text & "','" + TextBox2.Text + "','" & TextBox7.Text & "','" & TextBox4.Text & "','" & (Val(TextBox4.Text) - Val(TextBox7.Text)) & "')"
                Ejecutar(sql)
                con.Close()

                ''Insert en donde llevamos el historial 
                sql = "Insert into Registro_SalidaDeArticulos values  ('" + fecha + "','" + ComboBox1.SelectedItem + "','" & TextBox1.Text & "','" + TextBox2.Text + "','" & TextBox7.Text & "','" & TextBox4.Text & "','" & (Val(TextBox4.Text) - Val(TextBox7.Text)) & "','" + Login.usuario + "')"
                Ejecutar(sql)
                con.Close()

                MessageBox.Show("Se han retirado los articulos de manera exitosa", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)


                TextBox1.Enabled = True
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""

                TextBox1.Select()


                Llenar_grids()

            End If

        End If

    End Sub

    Private Sub Salidas_Articulos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Today
        sql = "delete SalidaDeArticulos"
        Ejecutar(sql)
        con.Close()


    End Sub

    Private Sub TextBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyDown

        If e.KeyCode = Keys.Enter Then

            Baja_Articulos()
            CountItem()

        End If


    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LlenarExcel()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        sql = "delete SalidaDeArticulos"
        Ejecutar(sql)
        con.Close()

        Llenar_grids()

        CountItem()


    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Select()
    End Sub
End Class