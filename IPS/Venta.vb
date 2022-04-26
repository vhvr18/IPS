Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Imports System.Drawing.Printing

Public Class Venta

    Dim formaPago As String

    Dim totalApagar As Double           ''Variable para obtener el descuento 

    Dim Descuento As String               ''Variable para obtener el descuento

    Dim id As String = ""                                   'le asignamos un id al  ticket especifico que lo sacamos de la fecha


    ''Codigo para registrar ventas por articulo y por ticket 
    Public Sub RegistrarVentas()

        Dim resp As Integer         ''Variable para obtener la respuesta del ticket

        Dim codigoPreventa As String          ''Variable para obtener el codigo de barra de los productos que se van a vender 

        Dim codArticulo As String = ""                  ''Detalles del articulo 
        Dim descripccion As String = ""
        Dim descripcion2 As String = ""

        Dim cantidad As Integer
        Dim precio As Double
        Dim totalArt As Double


        Dim compraNegada As String = ""         ''Variable que se utiliza para registrar la venta por ticket 

        id = DateTime.Now.ToString("ddMMyyyyhhmmss")     ''Asignamos el id 

        Dim fecha As String = ""
        fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ''Variables para la hora de entrada


        For a = 0 To PuntoDeVenta.DataGridView1.Rows.Count - 1 ''ciclo para saber los articulos del data grid

            codigoPreventa = (PuntoDeVenta.DataGridView1.Item(0, a).Value) '' variable del codigo del grid


            If codigoPreventa > 0 Then ''condicion para que cuando el codigo del grid de 0 no lo agrege 

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
                    totalArt = dr(5)

                End While

                '' insertar a la tabla articulos la cual agregara los articulos del grid uno por uno

                If Val(TextBox5.Text) >= Val(TextBox3.Text) Or Val(TextBox6.Text) >= Val(TextBox3.Text) Then        ''Validaciones del pago 

                    If PuntoDeVenta.DataGridView1.Rows.Count = 1 Then        ''Validacion si en el carrito solo hay un articulo

                        If cantidad = 0 Or descripccion = "" Then     ''If para evitar el bug en los registros de ventas  ya que agrega ventas a la tabla tickets y articulos con valores en cero y sin descripcion 

                            ''No se realiza nada, ya que de lo contrario ingresa ventas sin cantidad o descripcion

                        Else

                            If Login.usuario = "" Then  ''Forzamos el usuario de venta ya que en ocasiones pone ventas sin nameuser
                                Login.usuario = Principal.user
                            Else

                            End If

                            sql = "insert into ventasArticulos values('" + fecha + "','" + id + "','" + codArticulo + "','" + descripccion +
                                       "','" + descripcion2 + "','" + ComboBox2.SelectedItem + "','" & cantidad &
                                       "','" & precio & "','" + ComboBox3.Text + "','" & totalApagar & "','" & totalArt &
                                       "','" & totalArt & "','" + Login.usuario + "')"


                            Ejecutar(sql)
                            con.Close()


                        End If

                    Else            ''De lo contrario son varios articulos

                        If cantidad = 0 Or descripccion = "" Then

                            ''No se realiza nada, ya que de lo contrario ingresa ventas sin cantidad o descripcion

                        Else

                            If Login.usuario = "" Then
                                Login.usuario = Principal.user
                            Else

                            End If

                            sql = "insert into ventasArticulos values('" + fecha + "','" + id + "','" + codArticulo + "','" + descripccion +
                                                            "','" + descripcion2 + "','" + ComboBox2.SelectedItem + "','" & cantidad &
                                                            "','" & precio & "','" + ComboBox3.Text + "','" & totalApagar & "','" & totalArt &
                                                            "','" & totalArt & "','" + Login.usuario + "')"


                            Ejecutar(sql)
                            con.Close()

                        End If

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

            MessageBox.Show("El monto debe ser mayor al total.", "Integrated Sales System")

            TextBox5.Text = ""
            TextBox5.Select()


        Else                ''Aqui se inserta la venta a la tabla por tickets

            If Login.usuario = "" Then
                Login.usuario = Principal.user
            Else

            End If

            ''insertar a la tabla ventasxtickets  la venta en general  venta por ticket 
            sql = "insert into ventasxTickets values('" + fecha + "','" + id + "','" + ComboBox2.SelectedItem + "','" & PuntoDeVenta.TextBox5.Text &
                                                "','" + ComboBox3.Text + "','" & totalApagar & "','" & TextBox1.Text & "','" & TextBox3.Text &
                                                "','" + Login.usuario + "')"

            Ejecutar(sql)
            con.Close()


            resp = MsgBox("¿Desea imprimir el ticket? ", vbOKCancel, "Integrated Sales System")  ''Codigo que confirma la eliminacion de un usuario

            If resp = 1 Then

                PrintDocument1.Print()

            Else

            End If


            PuntoDeVenta.Button3.PerformClick()

            Me.Close()


        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim resp As Integer


        resp = MsgBox("¿Ya realizaste el cobro al cliente? ", vbOKCancel, "Integrated Sales System")  ''Codigo que confirma la eliminacion de un usuario

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

            resp = MsgBox("¿Ya realizaste el cobro al cliente? ", vbOKCancel, "Integrated Sales System")  ''Codigo que confirma la eliminacion de un usuario

            If resp = 1 Then

                RegistrarVentas()

            Else

            End If


        End If

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        ''Estructura del ticket y configuracion
        ''Paginas a las que consulte
        ''https://www.dreamincode.net/forums/topic/190909-printform-how-to-adjust-margins/
        ''https://www.youtube.com/watch?v=8OHXvMStsfU
        ''https://www.youtube.com/watch?v=NCcsXxFN86k
        ''https://www.youtube.com/watch?v=UUp5LKR4Rts
        ''https://www.youtube.com/watch?v=UUp5LKR4Rts     Este es el chido en vb con printdocument 
        ''https://www.youtube.com/watch?v=UUp5LKR4Rts    Impresion con retardo


        Dim ReportFont As Font = New Drawing.Font("AR JULIAN", 14)
        Dim ReportFont2 As Font = New Drawing.Font("Time New Roman", 10)
        Dim ReportFont3 As Font = New Drawing.Font("Time New Roman", 12)

        ''Codigo con el cual doy configuracion a la hoja donde se va aa imprimir
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Left = 0
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Top = 0
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0
        PrintDocument1.PrinterSettings.DefaultPageSettings.Margins.Right = 0

        'id = DateTime.Now.ToString("ddMMyyyyhhmmss")     ''Asignamos el id 

        ''Cuerpo del ticket

        e.Graphics.DrawString("FARMACIA SAN VICENTE", ReportFont, Brushes.Chocolate, 35, 0)

        e.Graphics.DrawString("Numero de Ticket: " + id, ReportFont2, Brushes.Chocolate, 0, 30)

        e.Graphics.DrawString("Vendedor: " + Login.nombreCompleto, ReportFont2, Brushes.Chocolate, 0, 45)

        e.Graphics.DrawString("Fecha: " + DateTime.Now.ToString("dd-MM-yyyy - HH:mm:ss"), ReportFont2, Brushes.Chocolate, 0, 60)

        e.Graphics.DrawString("Cant:                   Precio:                   Importe:", ReportFont2, Brushes.Chocolate, 0, 80)

        e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ", ReportFont2, Brushes.Chocolate, 0, 90)


        Dim strPrint As String          ''Variable para juntar los prductos vendidos 
        Dim posY, pos2 As Integer           ''Variables para las posiciones en y 

        posY = 105
        pos2 = 105

        For a = 0 To PuntoDeVenta.DataGridView1.Rows.Count - 1               ''Ciclo para agregar los productos al ticket

            strPrint = strPrint & (PuntoDeVenta.DataGridView1.Item(1, a).Value) & vbCrLf & "#" & (PuntoDeVenta.DataGridView1.Item(3, a).Value) & "                        $" & (PuntoDeVenta.DataGridView1.Item(4, a).Value) & "                    $" & (PuntoDeVenta.DataGridView1.Item(5, a).Value) & vbCrLf
            e.Graphics.DrawString(strPrint, ReportFont2, Brushes.Chocolate, 0, posY)

        Next

        pos2 = pos2 + (30 * PuntoDeVenta.DataGridView1.Rows.Count)


        e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ", ReportFont2, Brushes.Chocolate, 0, pos2)

        pos2 = pos2 + 20

        e.Graphics.DrawString("Total Neto: $" & TextBox3.Text, ReportFont3, Brushes.Chocolate, 130, pos2)

        pos2 = pos2 + 15

        e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ", ReportFont3, Brushes.Chocolate, 0, pos2)

        pos2 = pos2 + 20

        e.Graphics.DrawString("Efectivo: $" & TextBox5.Text, ReportFont2, Brushes.Chocolate, 160, pos2)

        pos2 = pos2 + 20

        e.Graphics.DrawString("Cambio: $" & TextBox4.Text, ReportFont2, Brushes.Chocolate, 160, pos2)

        pos2 = pos2 + 50

        e.Graphics.DrawString("Gracias por su compra!!", ReportFont, Brushes.Chocolate, 35, pos2)

        pos2 = pos2 + 30

        e.Graphics.DrawString("Las Américas, 55070 Ecatepec de Morelos, Méx.", ReportFont2, Brushes.Chocolate, 5, pos2)


    End Sub



End Class