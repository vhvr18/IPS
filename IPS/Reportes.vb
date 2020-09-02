Imports System.Data.SqlClient

Public Class Reportes

    Dim da As SqlDataAdapter        ''Variables para hacer funcionar el DataGrid
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

    ''Calculamos los datos estadisticos
    Public Sub EstadisticasXTicket()

        Dim noProductos, noTickets, noDescuentos As Integer     ''Variables para obtener los datos
        Dim Descuentos As String = ""

        noDescuentos = 0


        For a = 0 To DataGridView1.Rows.Count - 1   ''ciclo para calcular los datos como num de tickets, descuentos y de productos 

            noProductos = noProductos + (Val(DataGridView1.Item(3, a).Value))
            noTickets = DataGridView1.Rows.Count  'noTickets + (Val(DataGridView1.Item(3, a).Value))

            Descuentos = +(Val(DataGridView1.Item(5, a).Value))

            If Descuentos = "0" Then
            ElseIf Descuentos <> "0" Then
                noDescuentos = noDescuentos + 1
            End If

        Next

        Label6.Text = "No de Productos: " & noProductos                 ''Imprimimos informacion
        Label7.Text = "No de Tickets:" & noTickets
        Label8.Text = "No de Ventas con Descuentos % :" & noDescuentos

    End Sub

    ''EstadisticasXArticulos
    Public Sub EstadisticasXArticulos()

        Dim fecha As String = DateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss")         ''Obtenemos las fechas de la busqueda para saber el numero de tickets
        Dim Fecha2 As String = DateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59")

        Dim noProductos, noTickets, noDescuentos As Integer                 ''Variable para obtener informacion e imprimir 
        Dim Descuentos As String = ""
        Dim Tickets As String = ""
        Dim Tickets2 As String = ""


        For a = 0 To DataGridView1.Rows.Count - 1                           ''Ciclo para calcular los datos como descuentos y noprodcutos 

            noProductos = noProductos + (Val(DataGridView1.Item(6, a).Value))

            Descuentos = (Val(DataGridView1.Item(8, a).Value))

            If Descuentos = "0" Then
            ElseIf Descuentos <> "0" Then
                noDescuentos = noDescuentos + 1
            End If

        Next

        If ComboBox1.SelectedIndex = 0 Then    ''Condicion para saber si se hara busqueda por usuario en especifico o todos

            If fecha = Fecha2 Then

                sql = "Select count (*) from ventasxTickets where (DATEPART(yy, Fecha_Hora) = '" & DateTimePicker1.Value.Year.ToString() & "' AND DATEPART(mm, Fecha_Hora) = '" & DateTimePicker1.Value.Month.ToString() & "' AND DATEPART(dd, Fecha_Hora) = '" & DateTimePicker1.Value.Day.ToString() & "')"
                Ejecutar(sql)

                com = New SqlCommand(sql, con)                          ''asignamos el valor
                dr = com.ExecuteReader

                While dr.Read

                    noTickets = dr(0)

                End While
                con.Close()


            Else

                sql = "select count(*) from ventasxTickets where Fecha_Hora between '" + fecha + "' and '" + Fecha2 + "'"
                Ejecutar(sql)

                com = New SqlCommand(sql, con)                          ''asignamos el valor
                dr = com.ExecuteReader

                While dr.Read

                    noTickets = dr(0)

                End While
                con.Close()

            End If

        Else

            If fecha = Fecha2 Then

                sql = "Select count (*) from ventasxTickets where (DATEPART(yy, Fecha_Hora) = '" & DateTimePicker1.Value.Year.ToString() & "' AND DATEPART(mm, Fecha_Hora) = '" & DateTimePicker1.Value.Month.ToString() & "' AND DATEPART(dd, Fecha_Hora) = '" & DateTimePicker1.Value.Day.ToString() & "') and Usuario = '" + ComboBox1.SelectedItem + "'"
                Ejecutar(sql)

                com = New SqlCommand(sql, con)                          ''Asignamos el valor

                dr = com.ExecuteReader

                While dr.Read

                    noTickets = dr(0)

                End While
                con.Close()

            Else

                sql = "select count(*) from ventasxTickets where Fecha_Hora between '" + fecha + "' and '" + Fecha2 + "' and usuario = '" + ComboBox1.SelectedItem + "'"
                Ejecutar(sql)

                com = New SqlCommand(sql, con)                          ''Asignamos el valor

                dr = com.ExecuteReader

                While dr.Read

                    noTickets = dr(0)

                End While
                con.Close()

            End If


        End If


        Label6.Text = "No de Productos: " & noProductos
        Label7.Text = "No de Tickets:" & noTickets
        Label8.Text = "No de Ventas con Descuentos % :" & noDescuentos

    End Sub

    ''Calculamos los totales de las ventas x Tickets
    Public Sub TotalXTickets()

        Dim ventaTotal, descuentos, ventaNeta, ventaNeta2 As Double

        For a = 0 To DataGridView1.Rows.Count - 1

            ventaTotal = ventaTotal + (Val(DataGridView1.Item(7, a).Value))
            ventaNeta = ventaNeta + (Val(DataGridView1.Item(6, a).Value))
            descuentos = descuentos + (Val(DataGridView1.Item(5, a).Value))

        Next

        ventaNeta2 = ventaTotal - descuentos



        Label11.Text = "Venta Total " & (Format(ventaTotal, "$#,##0.00"))
        Label10.Text = "Descuentos " & (Format(descuentos, "$#,##0.00"))
        Label9.Text = "Venta Neta " & (Format(ventaNeta, "$#,##0.00"))


    End Sub

    ''Calculamos los totales de las ventas x Articulos
    Public Sub TotalXArticulos()

        Dim ventaTotal, descuentos, ventaNeta, ventaNeta2 As Double

        For a = 0 To DataGridView1.Rows.Count - 1

            ventaTotal = ventaTotal + (Val(DataGridView1.Item(11, a).Value))
            ventaNeta = ventaNeta + (Val(DataGridView1.Item(10, a).Value))
            descuentos = descuentos + (Val(DataGridView1.Item(9, a).Value))

        Next

        ventaNeta2 = ventaTotal - descuentos



        Label11.Text = "Venta Total " & (Format(ventaTotal, "$#,##0.00"))
        Label10.Text = "Descuentos " & (Format(descuentos, "$#,##0.00"))
        Label9.Text = "Venta Neta " & (Format(ventaNeta2, "$#,##0.00"))


    End Sub

    ''Calculamos los resultados de efectivo y credito por tickets 
    Public Sub EfectivoxTickets()

        Dim totalEfectivo As Double = 0
        Dim totalCredito As Double = 0
        Dim totalNeto As Double = 0

        For a = 0 To DataGridView1.Rows.Count - 1                           ''Ciclo para barrer si es una venta en efectivo y lo suma  

            If DataGridView1.Item(2, a).Value = "Efectivo" Then
                totalEfectivo = totalEfectivo + (Val(DataGridView1.Item(6, a).Value))
            End If

        Next

        For a = 0 To DataGridView1.Rows.Count - 1                           ''Ciclo para barrer si es una venta en efectivo y lo suma  

            If DataGridView1.Item(2, a).Value = "Tarjeta de Credito" Then
                totalCredito = totalCredito + (Val(DataGridView1.Item(6, a).Value))
            End If

        Next

        totalNeto = totalCredito + totalEfectivo

        Label15.Text = "Ventas en Efectivo " & (Format(totalEfectivo, "$#,##0.00"))
        Label17.Text = "= Total de Efectivo " & (Format(totalEfectivo, "$#,##0.00"))
        Label18.Text = "= Ingresos Netos " & (Format(totalNeto, "$#,##0.00"))
        Label19.Text = "Ventas con Tarjeta Bancaria " & (Format(totalCredito, "$#,##0.00"))
        Label20.Text = "Ventas en Efectivo " & (Format(totalEfectivo, "$#,##0.00"))


    End Sub

    ''Calculamos los resultados de efectivo y credito por articulos 
    Public Sub EfectivoxArticulos()

        Dim totalEfectivo As Double = 0
        Dim totalCredito As Double = 0
        Dim totalNeto As Double = 0

        For a = 0 To DataGridView1.Rows.Count - 1                           ''Ciclo para calcular los datos como descuentos y noprodcutos 

            If DataGridView1.Item(5, a).Value = "Efectivo" Then
                totalEfectivo = totalEfectivo + (Val(DataGridView1.Item(10, a).Value))
            End If

        Next

        For a = 0 To DataGridView1.Rows.Count - 1                           ''Ciclo para calcular los datos como descuentos y noprodcutos 

            If DataGridView1.Item(5, a).Value = "Tarjeta de Credito" Then
                totalCredito = totalCredito + (Val(DataGridView1.Item(10, a).Value))
            End If

        Next


        totalNeto = totalCredito + totalEfectivo

        Label15.Text = "Ventas en Efectivo " & (Format(totalEfectivo, "$#,##0.00"))
        Label17.Text = "= Total de Efectivo " & (Format(totalEfectivo, "$#,##0.00"))
        Label18.Text = "= Ingresos Netos $ " & (Format(totalNeto, "$#,##0.00"))
        Label19.Text = "Ventas con Tarjeta Bancaria " & (Format(totalCredito, "$#,##0.00"))
        Label20.Text = "Ventas en Efectivo " & (Format(totalEfectivo, "$#,##0.00"))

    End Sub

    ''Metodo para llenar el grid de los reportes de ventas

    Public Sub LlenarGrid()             '' Llena el grid de la preventa 

        DataGridView1.Columns.Clear()  ''Limpia las columnas para que el devuelva de manera ordenada la informacion el el dgv 

        Dim fecha As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")      ''Variables para asignar las fechas y hacer las consultas
        Dim Fecha2 As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")

        Dim fechaAxu As String = ""

        Dim dia As String = DateTimePicker1.Value.Day                           ''Variables para asignar las fechas y hacer las consultas
        Dim mes As String = DateTimePicker1.Value.Month
        Dim año As String = DateTimePicker1.Value.Year

        Dim dia2 As String = DateTimePicker2.Value.Day
        Dim mes2 As String = DateTimePicker2.Value.Month
        Dim año2 As String = DateTimePicker2.Value.Year


        ''Codigo cuando selecciona un usuario



        If fecha > Fecha2 Then      ''Indica que la fecha1 debe ser menor a la fecha2 

            MessageBox.Show("La primer fecha debe ser menor a la segunda ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            If ComboBox1.Text = "" Then  ''Indica que debe seleccionar algo en el combo box


                MessageBox.Show("Necesitas seleccionar un usuario ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)


            Else

                If CheckBox1.Checked = False And CheckBox2.Checked = False Then     ''Indica que debe seleccionar un metodo de busqueda

                    MessageBox.Show("Necesitas seleccionar un tipo de búsqueda ", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Else

                    ''Por articulos
                    If CheckBox2.Checked = True Then


                        If fecha = Fecha2 Then


                            dx = New DataTable
                            sql = "exec sp_likefecha '" + año + "','" + mes + "','" + dia + "','" + ComboBox1.SelectedItem + "'"
                            Conectar()
                            da = New SqlDataAdapter(sql, con)
                            da.Fill(dx)
                            DataGridView1.DataSource = dx

                            con.Close()

                            EstadisticasXArticulos()
                            TotalXArticulos()
                            EfectivoxArticulos()

                        Else
                            ''Se reparo la consulta poniendo hora en la fecha 2 para que tome la fecha dos con una hora
                            dx = New DataTable
                            sql = "select * from ventasArticulos where Fecha_Hora between '" & fecha & "' and '" & Fecha2 & " 23:59:59 ' and Usuario = '" + ComboBox1.SelectedItem + "' order by Fecha_Hora asc"
                            Conectar()
                            da = New SqlDataAdapter(sql, con)
                            da.Fill(dx)
                            DataGridView1.DataSource = dx

                            con.Close()

                            EstadisticasXArticulos()
                            TotalXArticulos()
                            EfectivoxArticulos()

                        End If

                    End If


                    ''Por ticket 
                    If CheckBox1.Checked Then


                        If fecha = Fecha2 Then

                            dx = New DataTable
                            sql = "exec sp_likefechaTickets '" + año + "','" + mes + "','" + dia + "','" + ComboBox1.SelectedItem + "'"
                            Conectar()
                            da = New SqlDataAdapter(sql, con)
                            da.Fill(dx)
                            DataGridView1.DataSource = dx

                            con.Close()

                            EstadisticasXTicket()
                            TotalXTickets()
                            EfectivoxTickets()

                        Else

                            ''Se reparo la consulta poniendo hora en la fecha 2 para que tome la fecha dos con una hora
                            dx = New DataTable
                            sql = "select * from ventasxTickets where Fecha_Hora between '" & fecha & "' and '" & Fecha2 & " 23:59:59'  and Usuario = '" + ComboBox1.SelectedItem + "' order by Fecha_Hora asc"
                            Conectar()
                            da = New SqlDataAdapter(sql, con)
                            da.Fill(dx)
                            DataGridView1.DataSource = dx

                            con.Close()

                            EstadisticasXTicket()
                            TotalXTickets()
                            EfectivoxTickets()

                        End If

                    End If


                    ''if por si desea consultar a todos

                    If ComboBox1.SelectedIndex = 0 And CheckBox1.Checked = True And fecha <> Fecha2 Then
                        ''Se reparo la consulta poniendo hora en la fecha 2 para que tome la fecha dos con una hora
                        dx = New DataTable
                        'sql = "select * from ventasxTickets  where  Fecha_Hora between '" & año & "-" & mes & "-" & dia & "' and '" & año2 & "-" & mes2 & "-" & dia2 & "' order by Fecha_Hora asc "
                        sql = "select * from ventasxTickets  where  Fecha_Hora between '" & fecha & "' and '" & Fecha2 & " 23:59:59' order by Fecha_Hora asc "

                        'MessageBox.Show(año & " y " & mes & " y " & dia)
                        'MessageBox.Show(año2 & " y " & mes2 & " y " & dia2)

                        Conectar()
                        da = New SqlDataAdapter(sql, con)
                        da.Fill(dx)
                        DataGridView1.DataSource = dx

                        con.Close()

                        EstadisticasXTicket()
                        TotalXTickets()
                        EfectivoxTickets()

                    End If

                    If ComboBox1.SelectedIndex = 0 And CheckBox2.Checked = True And fecha <> Fecha2 Then


                        dx = New DataTable
                        'sql = "select * from ventasArticulos  where  Fecha_Hora between '" & año & "-" & mes & "-" & dia & "' and '" & año2 & "-" & mes2 & "-" & dia2 & "' order by Fecha_Hora asc "
                        sql = "select * from ventasArticulos  where  Fecha_Hora between '" & fecha & "' and '" & Fecha2 & " 23:59:59' order by Fecha_Hora asc "

                        'MessageBox.Show(año & " y " & mes & "y " & dia)
                        'MessageBox.Show(año2 & " y " & mes2 & "y " & dia2)

                        Conectar()
                        da = New SqlDataAdapter(sql, con)
                        da.Fill(dx)
                        DataGridView1.DataSource = dx

                        con.Close()

                        EstadisticasXArticulos()
                        TotalXArticulos()
                        EfectivoxArticulos()

                    End If

                    ''Todos por ticket
                    If ComboBox1.SelectedIndex = 0 And CheckBox1.Checked = True And fecha = Fecha2 Then


                        dx = New DataTable
                        sql = "select * from ventasxTickets where  (DATEPART(yy, Fecha_Hora) ='" & año & "' and DATEPART(mm, Fecha_Hora) ='" & mes & "' and DATEPART(dd, Fecha_Hora) ='" & dia & "') order by Fecha_Hora asc"
                        ' sql = "select * from ventasxTickets  where  Fecha_Hora between '" & fecha & "' and '" & Fecha2 & "' order by Fecha_Hora asc "

                        'MessageBox.Show(año & " y " & mes & " y " & dia)
                        'MessageBox.Show(año2 & " y " & mes2 & " y " & dia2)

                        Conectar()
                        da = New SqlDataAdapter(sql, con)
                        da.Fill(dx)
                        DataGridView1.DataSource = dx

                        con.Close()

                        EstadisticasXTicket()
                        TotalXTickets()
                        EfectivoxTickets()

                    End If

                    If ComboBox1.SelectedIndex = 0 And CheckBox2.Checked = True And fecha = Fecha2 Then


                        dx = New DataTable
                        sql = "select * from ventasArticulos where  (DATEPART(yy, Fecha_Hora) ='" & año & "' and DATEPART(mm, Fecha_Hora) ='" & mes & "' and DATEPART(dd, Fecha_Hora) ='" & dia & "') order by Fecha_Hora asc"
                        'sql = "select * from ventasArticulos  where  Fecha_Hora between '" & fecha & "' and '" & Fecha2 & "' order by Fecha_Hora asc "

                        'MessageBox.Show(año & " y " & mes & "y " & dia)
                        'MessageBox.Show(año2 & " y " & mes2 & "y " & dia2)

                        Conectar()
                        da = New SqlDataAdapter(sql, con)
                        da.Fill(dx)
                        DataGridView1.DataSource = dx

                        con.Close()

                        EstadisticasXArticulos()
                        TotalXArticulos()
                        EfectivoxArticulos()

                    End If


                End If                      ''Cierre de if de alertas

            End If

        End If


    End Sub

    ''Load del form 
    Private Sub Reportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker1.Value = Today
        DateTimePicker2.Value = Today


        Dim usuario As String = ""

        sql = "Select usuario from usuarios where id_Empleado >= 1"
        Ejecutar(sql)
        com = New SqlCommand(sql, con)                          ''Leemos la tabla productos para saber si el prodcuto existe 
        dr = com.ExecuteReader

        While dr.Read

            usuario = dr(0) '
            ComboBox1.Items.Add(usuario)

        End While
        con.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        LlenarGrid()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then

            CheckBox2.Checked = False

        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked = True Then

            CheckBox1.Checked = False

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)



    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LlenarExcel()

    End Sub
End Class