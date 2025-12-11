Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.Security.Cryptography


Public Class Principal

    Dim da As SqlDataAdapter

    Dim usuarioA As String                  ''Es la variable con la cual sabremos que usario esta en sesion

    Dim horatotal As Integer                    ''Variables para calcular el tiempo laborado
    Dim horamin As Integer
    Dim tot As String

    Dim entrada As String = ""

    Dim id As Integer = 0

    Dim idRegistro As Integer

    Public user As String = ""     ''Variable para guardar el usuario activo

    ''Variable con la cual voy a controlar que ventana de valores voy a abrir 
    Public Opcion As Integer = 0


    Public sucursal As String = ""
    Public idSucursal As String = ""

    ''Bloque de codigo para darle color al fondo del menu

    Public Sub BackGround()

        Dim ctl As Control
        Dim ctlMDI As MdiClient

        For Each ctl In Me.Controls

            Try

                ctlMDI = CType(ctl, MdiClient)
                ctlMDI.BackColor = Me.BackColor

            Catch exc As InvalidCastException


            End Try

        Next

    End Sub

    ''Calcular el tiempo trabajado 
    Public Sub TiempoTrabajado(usuario As String)

        Dim idResgistro As String = ""
        Dim año As Integer
        Dim mes As Integer
        Dim dia As Integer

        Dim laboro As String = ""


        año = Now.Year

        mes = Now.Month

        dia = Now.Day


        Dim horacompa As Integer = 0           ''Variable donde asignaremos el tiempo que trabajo el empleado 

        sql = "select DATEDIFF(minute,[entrada],[salida]) as tlaboral from registros where usuario = '" + usuario + "' and  DatePart(yy,salida) = '" & año & "' and DATEPART(mm,salida) = '" & mes & "' and datepart (dd,salida) = '" & dia & "'"     ''Consulta para saber el tiempo que el empleado trabajo
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            horacompa = dr(0) 'consulta de la hora laborada pero la da solo en minutos

        End While



        sql = "select fechaHora from entradas where usuario ='" + usuario + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            entrada = dr(0)                 ''Obtengo la hora de entrada

        End While


        sql = "select idRegistro from registros where usuario = '" + usuario + "' and  DatePart(yy,salida) = '" & año & "' and DATEPART(mm,salida) = '" & mes & "' and datepart (dd,salida) = '" & dia & "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            laboro = dr(0)   '' Leemos el valor

            If dr(0) = True Then                ''If donde el dato leido es dbnull significa que hay algo  y a la variable le asignamos algo 
                laboro = "algo"
            Else
                laboro = ""                         ''De lo contrario se le asinara un nulo para la comparacion posterior 

            End If

        End While

        horatotal = (horacompa / 60) 'regla de tres para saber que hora seria el total

        horamin = (horacompa Mod 60) 'sacamos el residuo de la division para saber los minutos exactos




        If laboro <> "" Then

            sql = "update registros set horas_laboradas = '" & horatotal & "' where usuario = '" + usuario + "' and  DatePart(yy,salida) = '" & año & "' and DATEPART(mm,salida) = '" & mes & "' and datepart (dd,salida) = '" & dia & "'"
            Ejecutar(sql)

            sql = "update registros set minutos_laborados = '" & horamin & "' where usuario = '" + usuario + "' and  DatePart(yy,salida) = '" & año & "' and DATEPART(mm,salida) = '" & mes & "' and datepart (dd,salida) = '" & dia & "'"
            Ejecutar(sql)

            MessageBox.Show("Has trabajado un total de " & horatotal & " horas con " & horamin & " minutos", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.None)

        Else

            MessageBox.Show("Solo mensaje: Has trabajado un total de " & horatotal & " horas con " & horamin & " minutos", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.None)


        End If



    End Sub

    ''Hace el check de entrada y salida 
    Public Sub Check(clave As String)

        Dim contraseña As String = ""           ''variable para saber si el usuario esta en la tabla entradas 
        Dim usuario As String = ""

        Dim nombre As String = ""               ''Variables para consultar al empleado
        Dim apellidop As String = ""
        Dim apellidom As String = ""

        Dim esta As String = ""

        Dim fechaEntradaHora As String = ""
        Dim fechaSalidaHora As String = ""

        Dim año As Integer                  ''Variables para obtener año,dia y mes que me ayudan para consultar con un like si el dia de registro ya se hizo
        Dim mes As Integer
        Dim dia As Integer

        Dim estaRegistrado As String = ""     ''Variable que uso para saber si el empleado esta en la tabla registrado


        fechaEntradaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ''Variables para la hora de entrada


        sql = "exec sp_numeroids"               ''Consulta para obtener el numero de registros 
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            idRegistro = dr(0) 'obtenemos el numero de registros que hay 

        End While

        idRegistro = idRegistro + 1


        ''Consulta para obtener la info del usuario  
        sql = "select * from usuarios where contraseña = '" + clave + "' and usuario = '" + Login.usuario + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            nombre = dr(1) 'cobtenemos los datos del usuarios
            apellidop = dr(2)
            apellidom = dr(3)
            usuario = dr(4)
            contraseña = dr(5)

        End While


        sql = "Select usuario from entradas where usuario = '" + usuario + "'"  ''consulta para saber si el usuario esta en entradas 
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            esta = dr(0)   '' Leemos el valor

        End While



        If clave <> "" Then


            If contraseña = clave Then

                If esta <> Login.usuario Then


                    sql = "Exec sp_InsertEntrada '" + nombre + "','" + apellidop + "','" + apellidom + "','" + usuario + "','" + fechaEntradaHora + "'"
                    Ejecutar(sql)


                    MessageBox.Show("El usuario " + nombre + " " + apellidop + " realizo un registro de entrada" + vbLf + "[" + fechaEntradaHora + "]", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    CorreoIN(nombre, fechaEntradaHora)


                Else

                    año = Now.Year

                    mes = Now.Month

                    dia = Now.Day


                    sql = "Exec sp_likeSalida '" & año & "','" & mes & "','" & dia & "','" + usuario + "'"
                    Ejecutar(sql)

                    com = New SqlCommand(sql, con)
                    dr = com.ExecuteReader

                    While dr.Read

                        estaRegistrado = dr(4)   '' Leemos el valor

                    End While


                    If estaRegistrado = usuario Then

                        fechaSalidaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                        MessageBox.Show("El usuario " + nombre + " " + apellidop + " realizo un registro de salida" + vbLf + "[" + fechaSalidaHora + "]", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TiempoTrabajado(usuario)

                        CorreoOut(nombre, fechaSalidaHora)

                    Else

                        fechaSalidaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                        sql = "Exec sp_InsertRegistro '" + nombre + "','" + apellidop + "','" + apellidom + "','" + usuario + "','" + fechaSalidaHora + "','" & idRegistro & "'"
                        Ejecutar(sql)


                        MessageBox.Show("El usuario " + nombre + " " + apellidop + " realizo un registro de salida" + vbLf + "[" + fechaSalidaHora + "]", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TiempoTrabajado(usuario)

                        CorreoOut(nombre, fechaSalidaHora)


                    End If


                End If

            Else

                MessageBox.Show("Contraseña incorrecta", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Else

        End If



    End Sub

    ''Load de la ventana 
    Private Sub Principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        user = Login.usuario

        If user = "" Then
            user = Login.TextBox1.Text
        Else

        End If


        ''Para que el contenido del menu se vea mas grande se le cambia la  configuracion ImageScalling 

        BackGround()


        Dim registradoEntrada As String = ""


        ''Codigo que hace la revision si el ultimo registro coincide con la fecha auctual y sino limpia laa tabla para hacer registros nuevos

        sql = "exec sp_RevisionEntrada '" & Now.Year & "','" & Now.Month & "','" & Now.Day & "','" + Login.usuario + "'"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            registradoEntrada = dr(0)

        End While
        con.Close()


        If registradoEntrada <> "" Then

        Else

            sql = "delete entradas where usuario= '" + Login.usuario + "'"
            Ejecutar(sql)

        End If

        ''Codigo para habilitar y deshabilitar botonos segun el nivel de usuario

        If Login.nivel = "CAJERO" Then

            UsuariosToolStripMenuItem.Enabled = False
            SistemaToolStripMenuItem.Enabled = False
            HistorialDeProductosToolStripMenuItem.Enabled = False
            AltaDeArtículoToolStripMenuItem.Enabled = False
            EntradaDeArtículosToolStripMenuItem.Enabled = False
            SalidasDeArtículosToolStripMenuItem.Enabled = False
            EntradasToolStripMenuItem.Enabled = False
            SalidasToolStripMenuItem.Enabled = False
            ValorTotalToolStripMenuItem.Enabled = False
            PedidosToolStripMenuItem.Enabled = False
            ModificarCostoYPrecioToolStripMenuItem.Enabled = False
            EntradaEmpleadosToolStripMenuItem.Enabled = False
            ResetInventarioToolStripMenuItem.Visible = False

        ElseIf Login.nivel = "ADMINISTRADOR" Then

        ElseIf Login.nivel = "GERENTE" Then
            SistemaToolStripMenuItem.Enabled = False

        End If


        ''Codigo que pone el menu con el reloj 

        sql = "Select * from Sucursales"
        Ejecutar(sql)


        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            idSucursal = dr(0)
            sucursal = dr(1)

        End While
        con.Close()



        ToolStripLabel1.Text = "SUCURSAL: " + sucursal + "  ID: " + idSucursal + "  USUARIO: " + Login.nombreCompleto + " - " + Login.nivel ''Imprimo en variable el usuario que ingreso



    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosToolStripMenuItem.Click

        Usuarios.MdiParent = Me
        Usuarios.Show()



    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click


        Application.Exit()              ''Sale completamente del sistema 


    End Sub

    '' Timer para usar reloj
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Label2.Text = Today.Date + " " + TimeOfDay

    End Sub



    Private Sub SistemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SistemaToolStripMenuItem.Click

        Configuracion.MdiParent = Me
        Configuracion.Show()

    End Sub


    Private Sub InventarioToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Inventarios.Show()

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)


        'Try

        '    Dim Manual As String = "C:\Manual de Usuario.pdf"

        '    Dim Documento As New Object
        '    Documento = CreateObject("Pdf.Application")
        '    With (Documento)
        '        .Application.Documents.Open(Manual)
        '    End With
        '    Documento = Nothing

        'Catch

        'End Try

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        Valores.MdiParent = Me
        Valores.Show()

    End Sub


    Private Sub Principal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        'Dim resp As Integer

        'resp = MsgBox("¿Seguro que desea salir del sistema? ", vbOKCancel, "Integrated Pharmacy System")  ''Codigo que confirma la eliminacion de un usuario

        'If resp = 1 Then

        '    Application.Exit()              ''Sale completamente del sistema 

        'Else

        'End If


        'If MessageBox.Show("¿Seguro que desea salir del sistema? ", "Integrated Pharmacy System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

        Application.Exit()


        'Else

        '    e.Cancel = True

        'End If

    End Sub

    Private Sub EntradasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntradasToolStripMenuItem.Click

        Historial_de_Productos.MdiParent = Me
        Historial_de_Productos.Show()

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs)
        Pedidos.Show()

    End Sub
    Private Sub SalidasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalidasToolStripMenuItem.Click

        Historial_de_Salida_de_Poductos.MdiParent = Me
        Historial_de_Salida_de_Poductos.Show()

    End Sub

    Private Sub ToolStripButton3_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton3.Click

        PuntoDeVenta.MdiParent = Me
        PuntoDeVenta.Show()

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click

        CheckInOut.MdiParent = Me
        CheckInOut.Show()

    End Sub

    Private Sub VentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasToolStripMenuItem.Click

        Reportes.MdiParent = Me
        Reportes.Show()

    End Sub

    Private Sub AltaDeArtículoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AltaDeArtículoToolStripMenuItem.Click

        Articulo.MdiParent = Me
        Articulo.Show()

    End Sub

    Private Sub EntradaDeArtículosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntradaDeArtículosToolStripMenuItem.Click

        Entrada_Articulos.MdiParent = Me
        Entrada_Articulos.Show()

    End Sub

    Private Sub SalidasDeArtículosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalidasDeArtículosToolStripMenuItem.Click
        Salidas_Articulos.MdiParent = Me
        Salidas_Articulos.Show()

    End Sub

    Private Sub UbicacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UbicacionesToolStripMenuItem.Click
        Ubicaciones.MdiParent = Me
        Ubicaciones.Show()

    End Sub
    Private Sub PedidosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PedidosToolStripMenuItem.Click
        Pedidos.MdiParent = Me
        Pedidos.Show()

    End Sub

    Private Sub InventarioToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles InventarioToolStripMenuItem.Click

        Inventarios.MdiParent = Me
        Inventarios.Show()

    End Sub

    Private Sub ValorTotalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValorTotalToolStripMenuItem.Click

        ValorTotal.MdiParent = Me
        ValorTotal.Show()

    End Sub

    Private Sub HistorialDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistorialDeProductosToolStripMenuItem.Click

    End Sub

    Private Sub ModificarCostoYPrecioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificarCostoYPrecioToolStripMenuItem.Click

        Actualizar_Costos_y_Precios.MdiParent = Me
        Actualizar_Costos_y_Precios.Show()

    End Sub

    Private Sub EntradaEmpleadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntradaEmpleadosToolStripMenuItem.Click

        ReporteEmpleados.MdiParent = Me
        ReporteEmpleados.Show()

    End Sub

    Public Sub ResetInInventario()
        Dim respuesta = MessageBox.Show(
            "Esta acción pondrá todas las existencias en 0." & vbCrLf &
            "Este proceso NO es reversible." & vbCrLf &
            vbCrLf &
            "¿Deseas continuar?", "Integrated Sales System",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

        If respuesta = DialogResult.Yes Then
            ' Ejecutar el reset de inventario
            ' Abrir el formulario de confirmación
            Dim frm As New FrmConfirmarPassword
            frm.ShowDialog()

            ' Evaluar si se autorizó
            If frm.Autorizado = False Then
                MessageBox.Show("Operación cancelada.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                ' ---------------------------
                ' Ejecutar reset de inventario
                ' ---------------------------
                sql = "update inventario set Existencia = 0 "
                Ejecutar(sql)

                MessageBox.Show("Inventario reiniciado correctamente.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If



        Else
            ' Cancelado por el usuario
        End If
    End Sub

    Private Sub ResetInventarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetInventarioToolStripMenuItem.Click

        ResetInInventario()

    End Sub
End Class