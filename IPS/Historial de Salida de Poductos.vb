Imports System.Data.SqlClient



Public Class Historial_de_Salida_de_Poductos

    Dim da As SqlDataAdapter
    Dim dx As DataTable            ''Para llenar grids 

    Public Sub Mapear_Medicamento(code As String)


        dx = New DataTable             ''Para llenar grids 

        sql = "SELECT [Fecha]
                ,[Concepto]
                ,[Codigo_Producto]
                ,[Descripcion]
                ,[Existencia_Anterior]
                ,[Cantidad_Retirada]
                ,[Nueva_Existencia]
                ,[usuario]
                FROM [dbo].[Registro_SalidaDeArticulos] WHERE Codigo_Producto = " & code & " ORDER BY Fecha DESC"

        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

        TextBox1.Text = ""
        TextBox1.Select()

    End Sub


    Private Sub Historial_de_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then


            If TextBox1.Text = "" Then

                MessageBox.Show("Debes ingresar un Codigo de Barras", "Integrated Pharmacy System", MessageBoxButtons.OK, MessageBoxIcon.None)
                TextBox1.Select()


            Else

                Mapear_Medicamento(Trim(TextBox1.Text))

            End If



        End If


    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)      ''Codigo para que solo escriba numeros 
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            ''MessageBox.Show("Solo puedes digitar numeros ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class