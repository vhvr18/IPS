Imports System.Net
Imports System.Net.Mail

Imports System.Data.SqlClient

Module Modulos

    Public con As SqlConnection = New SqlConnection         ''Variables para las consultas y conecciones
    Public com As SqlCommand
    Public dr As SqlDataReader

    Public sql As String
    Public res As Integer


    Private correos As New MailMessage
    Private envios As New SmtpClient

    Public InOut As Integer         ''Variables para mandar los correos de la entrada y salida 
    Public fecha As String

    Public corres As String

    'Conexion central
    Public Sub Conectar()
        con = New SqlConnection
        con.ConnectionString = (" server =localhost;database = RECOVERY ; integrated security = true")
        con.Open()
    End Sub


    ''Conexion remota, solo cambiar parametros
    'Public Sub Conectar()
    '    con = New SqlConnection
    '    con.ConnectionString = ("Data Source=192.168.0.12,1433;Network Library=DBMSSOCN;Initial Catalog=RECOVERY;User ID=sa;Password=01900;")
    '    con.Open()
    'End Sub




    Public Sub Ejecutar(consulta As String)

        Conectar()
        com = New SqlCommand(consulta, con)
        res = com.ExecuteNonQuery

    End Sub

    ''Metodo para hacer la funcionalidad del correo 

    Sub EnviarCorreo(ByVal emisor As String, ByVal password As String, ByVal mensaje As String, ByVal asunto As String, ByVal destinatario As String)
        Try

            correos.To.Clear()
            correos.Body = ""
            correos.Subject = ""
            correos.Body = mensaje
            correos.Subject = asunto
            correos.IsBodyHtml = True
            correos.To.Add(Trim(destinatario))    ''Trim elimina los espacios que se ponen despues de un texto

            correos.From = New MailAddress(emisor)
            envios.Credentials = New NetworkCredential(emisor, password)

            'Datos importantes no modificables para tener acceso a las cuentas

            envios.Host = "smtp.gmail.com"
            envios.Port = 587
            envios.EnableSsl = True

            envios.Send(correos)
            MsgBox("El mensaje fue enviado correctamente. ", MsgBoxStyle.Information, "Mensaje")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Mensajeria 1.0 vb.net ®", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub CorreoIN(nombre As String, fecha As DateTime)

        sql = "Select * from Sucursales"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read               ''Codigo donde obtengo información de la sucursal para sacar los correos a donde se enviaran 

            corres = dr(4)

        End While
        con.Close()

        If corres = "" Then

        Else

            Try

                Dim emi As String = "sysintegratedcompany@gmail.com"
                Dim pass As String = "farmacia01900 "
                Dim asun As String = "Sucursal 0100 Tultitlan Registro de Entrada"

                Dim receptor1 As String = corres

                EnviarCorreo(emi, pass, "El Empleado " + nombre + " realizo un registro de Entrada [" +
                                 fecha + "]", asun, receptor1)

            Catch ex As Exception
                MessageBox.Show("Revisa tu conexion a internet", "Sin Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try


        End If


    End Sub

    Public Sub CorreoOut(nombre As String, fecha As DateTime)

        Dim entro As String = ""

        Dim venta As Double


        ''Codigo para sacar la info de la sucursal 
        sql = "Select * from Sucursales"
        Ejecutar(sql)

        com = New SqlCommand(sql, con)
        dr = com.ExecuteReader

        While dr.Read

            corres = dr(4)                  ''Obtenemos los correos a los que se les enviara 

        End While
        con.Close()

        If corres = "" Then                 ''Si no hay correos no hara nada solo registros 


        Else

            sql = "Exec  sp_VentaCorreo '" & Now.Year & "','" & Now.Month & "','" & Now.Day & "','" + Login.usuario + "'"       ''Procedimiento para obtener lo que vendio el empleado 
            Ejecutar(sql)

            com = New SqlCommand(sql, con)
            dr = com.ExecuteReader

            While dr.Read

                Try

                    venta = Format(dr(0), "fixed")             ''Obtengo la venta

                Catch ex As Exception                           ''En caso de que no haya vendido devolvera un valor null y habra error

                    Dim emi As String = "sysintegratedcompany@gmail.com"            ''por eso mandaremos el mensaje en un try catch 
                    Dim pass As String = "farmacia01900 "
                    Dim asun As String = "Sucursal 0100 Tultitlan Registro de Salida"
                    Dim receptor1 As String = corres


                    EnviarCorreo(emi, pass, "El Empleado " + nombre + " realizo un registro de Salida [" +
                                      fecha + "]" + vbLf + "Total de venta: $ 0.00", asun, receptor1)

                    entro = "si"

                End Try

            End While

            ''en caso de que si haya venta devolvera un valor y mandara el mensaje con la venta
            If entro = "si" Then

            Else
                Try

                    Dim emi2 As String = "sysintegratedcompany@gmail.com"
                    Dim pass2 As String = "farmacia01900 "
                    Dim asun2 As String = "Sucursal 0100 Tultitlan Registro de Salida"
                    Dim receptor12 As String = corres



                    EnviarCorreo(emi2, pass2, "El Empleado " + nombre + " realizo un registro de Salida [" +
                                 fecha + "]" + vbLf + "Total de venta: $" & venta, asun2, receptor12)

                Catch ex As Exception
                    MessageBox.Show("Revisa tu conexion a internet", "Sin Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try


            End If

        End If

    End Sub


End Module
