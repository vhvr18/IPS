Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Public Class Configuracion


    ''Metodo que mostrara los datos de la informacion.
    Public Sub Cargar_Datos()

        sql = "Select * from Sucursales"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            TextBox1.Text = dr(0)
            TextBox2.Text = dr(1)
            TextBox3.Text = dr(2)
            TextBox4.Text = dr(3)
            TextBox5.Text = dr(4)

        End While
        con.Close()

    End Sub

    ''Metodo para guardar los datos ya sean nuevos o editados 
    Public Sub Guardar_Datos()

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then

            MessageBox.Show("Debes completar la información para ser guardada.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            sql = "Update dbo.Sucursales set Nombre = '" + TextBox2.Text + "',Direccion = '" + TextBox3.Text + "', Telefono = '" & TextBox4.Text & "', Correo_Electronico= '" + TextBox5.Text + "' where Id_Sucursal = '" & TextBox1.Text & "'"
            Ejecutar(sql)

            MessageBox.Show("Información editada.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.None)

            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False

            Cargar_Datos()
            Button1.Select()

        End If



    End Sub


    Private Sub Configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Cargar_Datos()
        Button1.Select()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True

        TextBox2.Select()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Guardar_Datos()

    End Sub
End Class