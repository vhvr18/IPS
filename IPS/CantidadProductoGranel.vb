Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class CantidadProductoGranel

    Public precioIn As Double
    Public codigo As Integer

    Private Sub CantidadProductoGranel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        sql = "SELECT p.Descripcion,inv.Precio FROM productos p INNER JOIN inventario inv ON p.Codigo_Producto = inv.Codigo_Producto WHERE p.Codigo_Producto = '" & codigo & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            Label1.Text = dr(0)
            precioIn = dr(1)
            Label4.Text = "Precio por Kilo= " + dr(1).ToString


        End While
        con.Close()

    End Sub

    Public Sub cargaCodigo(codigoEnv As String)         ''Metodo con el cual cargaremos el codigo de producto que se envia desde la pantalla de punto de venta
        codigo = Val(codigoEnv)
    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        'TextBox1.Text = ""
        Dim peso As Double

        peso = (Fix(Val(TextBox2.Text) * 1000) / precioIn)

        TextBox1.Text = Format(peso, "fixed")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PuntoDeVenta.pesajeProducto = TextBox1.Text
        Me.Close()

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyCode = Keys.Enter Then

        End If

    End Sub
End Class