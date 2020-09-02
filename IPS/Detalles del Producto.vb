Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail


Public Class Detalles_del_Producto


    ''Metodo para obtener la info del productos 
    Public Sub Informacion()

        sql = "Select * from productos where Codigo_Producto = '" & Busqueda_de_Articulo.codart & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read               ''Codigo donde obtengo información de la sucursal para sacar los correos a donde se enviaran 

            Label15.Text = dr(0)
            Label16.Text = dr(1)
            Label17.Text = dr(2)
            Label18.Text = dr(3)
            Label19.Text = dr(4)
            Label20.Text = dr(5)
            Label21.Text = dr(6)
            Label22.Text = dr(7)

        End While
        con.Close()


        sql = "Select * from inventario where Codigo_Producto = '" & Busqueda_de_Articulo.codart & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            Label23.Text = dr(4)
            Label24.Text = dr(8)


        End While



        sql = "Select * from ubicacion where Codigo_Producto = '" & Busqueda_de_Articulo.codart & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            Label25.Text = dr(2)
            Label26.Text = dr(3)
            Label27.Text = dr(4)


        End While


        Busqueda_de_Articulo.codart = ""


    End Sub

    Private Sub Detalles_del_Producto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Informacion()

    End Sub


End Class