Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports System.Security.Cryptography.X509Certificates

Public Class ReporteEmpleados

    Dim da As SqlDataAdapter        ''Variables para hacer funcionar el DataGrid
    Dim dx As DataTable

    Dim horarioEntrada As String
    Dim hora, minutos, segundos As String
    Dim minutos2, hora2 As Int16

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        LlenarGrid()

    End Sub

    Private Sub ReporteEmpleados_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ''Ponemos los dateTimepicker en el dia actual. 
        DateTimePicker1.Value = Today
        DateTimePicker2.Value = Today

        GetUsers()


    End Sub

    ''Metodo para traer los usuarios que hay en el sistema. 
    Public Sub GetUsers()

        Try

            Dim usuario As String = ""

            sql = "Select usuario from usuarios where id_Empleado >= 1"
            Ejecutar(sql)
            com = New SqlCommand(sql, con)                          ''Obtenemos los usuario que hay en el sistema
            dr = com.ExecuteReader

            While dr.Read

                usuario = dr(0) '
                ComboBox1.Items.Add(usuario)

            End While
            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error al obtener los usuarios de la base de datos " + ex.Message, "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub



    Public Sub LlenarGrid()             '' Llena el grid del reporte 

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

        Dim horarioEntrada As String = ComboBox2.Text
        Dim tipoReporte As Integer



        If fecha > Fecha2 Then      ''Indica que la fecha1 debe ser menor a la fecha2 

            MessageBox.Show("La primer fecha debe ser menor a la segunda ", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Else

            If ComboBox2.Text = "" Then

                MessageBox.Show("Debes selecionar una hora de entrada ", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else

                If ComboBox1.SelectedIndex = 0 Then       ''Bloque donde identificamos que tipo de reporte usara
                    tipoReporte = 1
                Else
                    tipoReporte = 2
                End If

                If ComboBox1.Text = "" Then  ''Indica que debe seleccionar algo en el combo box de usuarios


                    MessageBox.Show("Necesitas seleccionar un usuario ", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    ''si el usuario releciona todos los usuarios, la consulta no tendra en el where el usuario solo tendra parametros las fechas
                ElseIf ComboBox1.SelectedIndex = 0 Then

                    dx = New DataTable
                    sql = "EXEC spGetReportEmpleados " & tipoReporte & ",'" + horarioEntrada + "','" + Label6.Text + "','" + CStr(hora2) + ":" + CStr(minutos2) + ":" + CStr(segundos) +
                        "','" + fecha + "','" + Fecha2 + "','" + ComboBox1.Text + "'"
                    Conectar()
                    da = New SqlDataAdapter(sql, con)
                    da.Fill(dx)
                    DataGridView1.DataSource = dx

                    con.Close()

                Else

                    ''Procedemos a realizar el reporte con usuario en especifico y fechas validas

                    dx = New DataTable
                    sql = "EXEC spGetReportEmpleados " & tipoReporte & ",'" + horarioEntrada + "','" + Label6.Text + "','" + CStr(hora2) + ":" + CStr(minutos2) + ":" + CStr(segundos) +
                        "','" + fecha + "','" + Fecha2 + "','" + ComboBox1.Text + "'"
                    Conectar()
                    da = New SqlDataAdapter(sql, con)
                    da.Fill(dx)
                    DataGridView1.DataSource = dx

                    con.Close()


                End If
            End If
        End If


    End Sub

    Public Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        horarioEntrada = ComboBox2.Text

        Dim array() As String = horarioEntrada.Split(":")

        hora = array(0)
        minutos = array(1)
        segundos = array(2)

        minutos2 = CInt(minutos) + 15

        If minutos2 = 60 Then
            hora2 = CInt(hora) + 1
            minutos2 = 0
        Else
            hora2 = CInt(hora)
        End If

        Label6.Text = hora2 & ":" & minutos2 & ":" & segundos

    End Sub
End Class