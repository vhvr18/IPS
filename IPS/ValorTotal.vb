Imports System.Data.SqlClient

Public Class ValorTotal

    Dim da As SqlDataAdapter
    Dim dx As DataTable

    ''Mtthod to get all items  
    Public Sub GetAllItems()

        ''Consulta donde sacamos los valores mas importantes, solo los que tienen existencias y los que son productos
        dx = New DataTable
        sql = "SELECT p.Codigo_Producto,p.Descripcion, p.Descripcion_Secundaria,p.Categoria,i.Existencia, i.Costo ,i.Precio, i.utilidad as Utilidad,p.Tipo_Articulo
                    FROM productos p 
                    INNER JOIN inventario i on p.Codigo_Producto = i.Codigo_Producto 
                    WHERE i.Existencia > 0 ORDER BY i.Existencia,Categoria ASC "

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
        Dim granel As Double
        Dim totalArticulosDB As Integer


        sql = "SELECT COUNT(*) as NumeroArticulos, 
                SUM(
                     CASE 
                        WHEN p.Tipo_Articulo <> 'GRANEL' THEN i.Existencia
                        ELSE 0 
                     END
                ) as NumeroPiezas,
                
                SUM(
                    CASE 
                        WHEN p.Tipo_Articulo = 'GRANEL' THEN i.Existencia
                        ELSE 0 
                    END
                ) as TotalGranelGramos
            FROM productos p 
            INNER JOIN inventario i
                ON p.Codigo_Producto= i.Codigo_Producto"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            If Not IsDBNull(dr(0)) Or Not IsDBNull(dr(1)) Or Not IsDBNull(dr(2)) Then             ''Validación si el resultado de la consulta es nullo
                numPiezas = dr(1)
                granel = dr(2)
                totalArticulosDB = dr(0)
            End If

        End While
        con.Close()


        sql = "WITH Totales AS (
                SELECT 
                    SUM(
                        COALESCE(
                            CASE 
                                WHEN p.Tipo_Articulo = 'Granel' 
                                    THEN (i.Existencia / 1000.0) * i.Costo 
                                ELSE 
                                    i.Existencia * i.Costo
                            END
                        ,0)
                    ) AS Costo_Total_Num,

                    SUM(
                        COALESCE(
                            CASE 
                                WHEN p.Tipo_Articulo = 'Granel' 
                                    THEN (i.Existencia / 1000.0) * i.Precio 
                                ELSE 
                                    i.Existencia * i.Precio
                            END
                        ,0)
                    ) AS Precio_Total_Num
                FROM productos p
                INNER JOIN inventario i ON p.Codigo_Producto = i.Codigo_Producto
                WHERE i.Existencia > 0
            )
            SELECT
                FORMAT(Costo_Total_Num, 'N2') AS Costo_Total,
                FORMAT(Precio_Total_Num, 'N2') AS Precio_Total,
                FORMAT(Precio_Total_Num - Costo_Total_Num, 'N2') AS Utilidad_Total
            FROM Totales"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            ''Validación si el resultado de la consulta es nullo
            If Not IsDBNull(dr(0)) Or Not IsDBNull(dr(1)) Or Not IsDBNull(dr(2)) Then
                costo = dr(0)
                venta = dr(1)
                utilidad = dr(2)
            End If

        End While
        con.Close()


        Label7.Text = "# de Articulos en Base de Datos: " & totalArticulosDB
        Label1.Text = "# de Artículos en Existencia: " & DataGridView1.Rows.Count
        Label2.Text = "# de Piezas: " & numPiezas
        Label3.Text = "Costo total: " & (Format(costo, "$#,##0.00"))
        Label4.Text = "Precio total de Venta: " & (Format(venta, "$#,##0.00"))
        Label5.Text = "Utilidad total: " & (Format(utilidad, "$#,##0.00"))
        Label6.Text = "Total Producto a Granel (g): " & granel

    End Sub

    Private Sub ValorTotal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetAllItems()
        GetStats()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

End Class