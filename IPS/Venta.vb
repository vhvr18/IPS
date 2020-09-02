Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail


Public Class Venta

    Dim formaPago As String

    Dim totalApagar As Double           ''Variable para obtener el descuento 

    Dim Descuento As String               ''Variable para obtener el descuento

    ''Codigo para registrar ventas por articulo y por ticket 
    Public Sub RegistrarVentas()

        Dim codigoPreventa As String          ''Variable para obtener el codigo de barra de los productos que se van a vender 

        Dim codArticulo As String = ""
        Dim descripccion As String = ""
        Dim descripcion2 As String = ""

        Dim cantidad As Integer
        Dim precio As Double


        Dim compraNegada As String = ""

        Dim id As String = ""                                   'le asignamos un id al  ticket especifico que lo sacamos de la fecha
        id = DateTime.Now.ToString("ddMMyyyyhhmmss")

        Dim fecha As String = ""
        fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ''Variables para la hora de entrada


        For a = 0 To PuntoDeVenta.DataGridView1.Rows.Count - 1 ''ciclo para saber los articulos del data grid

            codigoPreventa = (PuntoDeVenta.DataGridView1.Item(0, a).Value) '' variable del codigo del grid


            If codigoPreventa > 0 Then ''condicion para cuando el codigo del grid de 0 no lo agrege 

                sql = "Select * from preventas where Codigo_Producto = '" & codigoPreventa & "'"  ' consulta a preventas 
                Ejecutar(sql)

                com = New SqlCommand(sql, con)                          ''Leemos la tabla preventas para pasar parametros
                dr = com.ExecuteReader

                While dr.Read

                    codArticulo = dr(0)
                    descripccion = dr(1)
                    descripcion2 = dr(2)
                    cantidad = dr(3)
                    precio = dr(4)

                End While

                '' insertar a la tabla articulos la cual agregara los articulos del grid uno por uno

                If Val(TextBox5.Text) >= Val(TextBox3.Text) Or Val(TextBox6.Text) >= Val(TextBox3.Text) Then

                    If PuntoDeVenta.DataGridView1.Rows.Count = 1 Then

                        sql = "insert into ventasArticulos values('" + fecha + "','" + id + "','" + codArticulo + "','" + descripccion +
                                        "','" + descripcion2 + "','" + ComboBox2.SelectedItem + "','" & cantidad &
                                        "','" & precio & "','" + ComboBox3.Text + "','" & totalApagar & "','" & (precio * cantidad) &
                                        "','" & Val(TextBox1.Text) & "','" + Login.usuario + "')"


                        Ejecutar(sql)
                        con.Close()
                    Else
                        sql = "insert into ventasArticulos values('" + fecha + "','" + id + "','" + codArticulo + "','" + descripccion +
                                                            "','" + descripcion2 + "','" + ComboBox2.SelectedItem + "','" & cantidad &
                                                            "','" & precio & "','" + ComboBox3.Text + "','" & totalApagar & "','" & ((precio * cantidad) - totalApagar) &
                                                            "','" & (precio * cantidad) & "','" + Login.usuario + "')"


                        Ejecutar(sql)
                        con.Close()

                    End If


                    'actualizar la tabla de inventarios el campo de cantidad
                    sql = "update dbo.inventario set Existencia = Existencia - " & cantidad & " where Codigo_Producto = '" + codArticulo + "'"
                    Ejecutar(sql)
                    con.Close()


                Else

                    compraNegada = "No"

                End If


            End If


        Next


        If compraNegada = "No" Then

            MessageBox.Show("El monto debe ser mayor al total.", "Integrated Pharmacy System")

            TextBox5.Text = ""
            TextBox5.Select()


        Else

            ''insertar a la tabla ventasxtickets  la venta en general  venta por ticket 
            sql = "insert into ventasxTickets values('" + fecha + "','" + id + "','" + ComboBox2.SelectedItem + "','" & PuntoDeVenta.TextBox5.Text &
                                                "','" + ComboBox3.Text + "','" & totalApagar & "','" & TextBox3.Text & "','" & TextBox1.Text &
                                                "','" + Login.usuario + "')"

            Ejecutar(sql)
            con.Close()

            PuntoDeVenta.Button3.PerformClick()

            Me.Close()


        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim resp As Integer


        resp = MsgBox("¿Ya realizaste el cobro al cliente? ", vbOKCancel, "Integrated Pharmacy System")  ''Codigo que confirma la eliminacion de un usuario

        If resp = 1 Then

            RegistrarVentas()

        Else

        End If

    End Sub

    Private Sub Venta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        PuntoDeVenta.Enabled = True



        ComboBox2.Select()
        ComboBox2.DroppedDown = True   ''desplegar combo


        TextBox1.Text = Format(PuntoDeVenta.totalVenta, "fixed")
        TextBox2.Text = PuntoDeVenta.totalArticulos
        TextBox3.Text = Format(PuntoDeVenta.totalVenta, "fixed")

        ComboBox2.Text = "Seleccionar"
        ComboBox3.Text = "0%"


        'If PuntoDeVenta.DataGridView1.Rows.Count = 1 Then

        '    ComboBox3.Enabled = True

        'Else
        '    ComboBox3.Enabled = False

        'End If


    End Sub

    ''Codigo para calcular porcentajes
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged


        If ComboBox3.SelectedIndex = 0 Then

            totalApagar = TextBox1.Text * 0.0

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 1 Then

            totalApagar = TextBox1.Text * 0.1

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 2 Then

            totalApagar = TextBox1.Text * 0.2

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 3 Then

            totalApagar = TextBox1.Text * 0.3

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 4 Then

            totalApagar = TextBox1.Text * 0.4

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 5 Then

            totalApagar = TextBox1.Text * 0.5

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 6 Then

            totalApagar = TextBox1.Text * 0.6

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If


        If ComboBox3.SelectedIndex = 7 Then

            totalApagar = TextBox1.Text * 0.7

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If

        If ComboBox3.SelectedIndex = 8 Then

            totalApagar = TextBox1.Text * 0.8

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If

        If ComboBox3.SelectedIndex = 9 Then

            totalApagar = TextBox1.Text * 0.9

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

        End If

        If ComboBox3.SelectedIndex = 10 Then

            totalApagar = TextBox1.Text * 1

            TextBox3.Text = Format(TextBox1.Text - totalApagar, "fixed")

            Descuento = ComboBox3.SelectedItem

            TextBox5.Text = 0
            TextBox5.Enabled = False

        End If


    End Sub

    ''Codigo para identificar la forma de pago 
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        If ComboBox2.SelectedIndex = 0 Then

            formaPago = ComboBox3.SelectedItem
            TextBox5.Enabled = True
            TextBox5.Text = "0.00"
            TextBox6.Enabled = False
            TextBox6.Text = ""
            TextBox5.Select()



        End If

        If ComboBox2.SelectedIndex = 1 Then

            formaPago = ComboBox3.SelectedItem
            TextBox5.Enabled = False
            TextBox6.Enabled = True
            TextBox6.Text = "0.00"
            TextBox5.Text = ""
            TextBox6.Select()

        End If


    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

        If Val(TextBox6.Text) >= Val(TextBox3.Text) Then

            TextBox4.Text = Val(TextBox6.Text) - Val(TextBox3.Text)

        Else

            TextBox4.Text = "0"

        End If

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

        If Val(TextBox5.Text) >= Val(TextBox3.Text) And Val(TextBox3.Text) > 0 Then

            TextBox4.Text = Format(Val(TextBox5.Text - TextBox3.Text), "fixed")

        ElseIf TextBox5.Text = "0" Or Val(TextBox5.Text) < Val(TextBox3.Text) Then

            TextBox4.Text = "0"

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()

    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress

        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    ''Codigo para cuando de enter se realice la compra

    Private Sub TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyDown


        Dim resp As Integer


        If e.KeyCode = Keys.Enter Then

            resp = MsgBox("¿Ya realizaste el cobro al cliente? ", vbOKCancel, "Integrated Pharmacy System")  ''Codigo que confirma la eliminacion de un usuario

            If resp = 1 Then

                RegistrarVentas()

            Else

            End If


        End If

    End Sub


End Class