Imports System.Data.SqlClient


Public Class Historial_de_Productos

    Dim da As SqlDataAdapter
    Dim dx As DataTable            ''Para llenar grids 

    Public Sub Mapear_Medicamento(code As String)

        dx = New DataTable             ''Para llenar grids 

        sql = "SELECT [Fecha]
                ,[Concepto]
                ,[Codigo_Producto]
                ,[Descripcion]
                ,[Costo_Anterior]
                ,[Nuevo_Costo]
                ,[Precio_Anterior]
                ,[Nuevo_Precio]
                ,[Existencia_Antetior]
                ,[Cantidad_Ingresada]
                ,[Nueva_Existencia]
                ,[usuario]
                FROM [dbo].[Registro_EntradadeArticulos] WHERE Codigo_Producto = '" + code + "' ORDER BY Fecha DESC"

        Conectar()
        da = New SqlDataAdapter(sql, con)
        da.Fill(dx)
        DataGridView1.DataSource = dx
        con.Close()

        TextBox1.Text = ""
        TextBox1.Select()

    End Sub


    Private Sub Historial_de_Productos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Today
        DateTimePicker2.Value = Today
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        CheckBox2.Checked = False
        CheckBox3.Checked = False

        DateTimePicker1.Enabled = True
        DateTimePicker2.Enabled = True

        TextBox1.Enabled = False

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        CheckBox1.Checked = False
        CheckBox3.Checked = False

        TextBox1.Enabled = True

        DateTimePicker1.Enabled = False
        DateTimePicker2.Enabled = False

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged

        CheckBox1.Checked = False
        CheckBox2.Checked = False

        TextBox1.Enabled = True

        DateTimePicker1.Enabled = True
        DateTimePicker2.Enabled = True

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click

        CheckBox1.Checked = True
        CheckBox2.Checked = False
        CheckBox3.Checked = False

    End Sub

    Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click

        CheckBox1.Checked = False
        CheckBox2.Checked = True
        CheckBox3.Checked = False

    End Sub

    Private Sub CheckBox3_Click(sender As Object, e As EventArgs) Handles CheckBox3.Click

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = True

    End Sub

End Class