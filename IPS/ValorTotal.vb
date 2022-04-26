Imports System.Data.SqlClient

Public Class ValorTotal

    Dim da As SqlDataAdapter
    Dim dx As DataTable

    ''Mtthod to get all items  
    Public Sub GetAllItems()

        ''Consulta donde sacamos los valores mas importantes, solo los que tienen existencias y los que son productos
        dx = New DataTable
        sql = "SELECT productos.Codigo_Producto,productos.Descripcion, productos.Descripcion_Secundaria,productos.Categoria,inventario.Existencia, inventario.Costo ,inventario.Precio, inventario.utilidad as Utilidad
                    FROM productos productos inner join inventario  inventario on productos.Codigo_Producto = inventario.Codigo_Producto 
                    WHERE inventario.Existencia > 0 ORDER BY inventario.Existencia,Categoria ASC "

        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

    End Sub

    Public Sub GetStats()

        Dim numPiezas As Integer
        Dim costo As Double
        Dim venta As Double
        Dim utilidad As Double

        For a = 0 To DataGridView1.Rows.Count - 1             ''Calculo para sacar el numero de piezas
            numPiezas = numPiezas + (DataGridView1.Item(4, a).Value)
        Next


        sql = "SELECT  SUM(Costo*Existencia)  as total FROM inventario  WHERE Existencia > 0"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            If Not IsDBNull(dr(0)) Then             ''Validación si el resultado de la consulta es nullo
                costo = dr(0)
            End If

        End While
        con.Close()

        sql = "SELECT  SUM(Precio*Existencia)  as total FROM inventario  WHERE Existencia > 0"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            If Not IsDBNull(dr(0)) Then         ''Validación si el resultado de la consulta es nullo
                venta = dr(0)
            End If

        End While
        con.Close()

        sql = "SELECT  SUM(Utilidad*Existencia)  as total FROM inventario  WHERE  Existencia > 0"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            If Not IsDBNull(dr(0)) Then                 ''Validación si el resultado de la consulta es nullo
                utilidad = dr(0)
            End If

        End While
        con.Close()


        Label1.Text = "# de Artículos " & DataGridView1.Rows.Count
        Label2.Text = "# de Piezas " & numPiezas
        Label3.Text = "Costo total " & (Format(costo, "$#,##0.00"))
        Label4.Text = "Precio total de Venta " & (Format(venta, "$#,##0.00"))
        Label5.Text = "Utilidad total " & (Format(utilidad, "$#,##0.00"))

    End Sub

    Private Sub ValorTotal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetAllItems()
        GetStats()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

End Class