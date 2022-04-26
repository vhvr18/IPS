Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class CantidadProductoGranel

    Public precioIn As Double

    Private Sub CantidadProductoGranel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        sql = "SELECT p.Descripcion,inv.Precio FROM productos p INNER JOIN inventario inv ON p.Codigo_Producto = inv.Codigo_Producto WHERE p.Codigo_Producto = '1510'  "
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            Label1.Text = dr(0)
            precioIn = dr(1)
            Label4.Text = "Precio por Unidad= " + dr(1).ToString


        End While
        con.Close()

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        'TextBox1.Text = ""
        Dim peso As Double

        peso = (Fix(Val(TextBox2.Text) * 1000) / precioIn)

        TextBox1.Text = peso

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        'TextBox2.Text = ""

        TextBox2.Text = (Fix(Val(TextBox1.Text) * precioIn) / 1000)

    End Sub

End Class