Imports System.Data.SqlClient
Imports System.Web.Services

Public Class FrmConfirmarPassword

    Public Property PasswordIn As String
    Public Property Autorizado As Boolean = False

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ValidarContrasena()

    End Sub

    Public Sub ValidarContrasena()
        PasswordIn = TextBox1.Text

        If TextBox1.Text <> "" Then

            sql = "SELECT * FROM usuarios WHERE contraseña = '" & PasswordIn & "'"  ' consulta a usuarios 
            Ejecutar(sql)

            com = New SqlCommand(sql, con)                          ''Leemos la tabla preventas para pasar parametros
            dr = com.ExecuteReader

            If dr.HasRows = True Then
                While dr.Read

                    If Not IsDBNull(dr(0)) Or Not IsDBNull(dr(5)) Or Not IsDBNull(dr(8)) Then
                        If dr(8) = "ADMINISTRADOR" Or dr(8) = "GERENTE" Then
                            Autorizado = True
                            Me.Hide()
                        Else
                            MessageBox.Show("El usuario no cuenta con los permisos.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.Hide()
                        End If

                    End If

                End While
            Else
                Autorizado = False
                MessageBox.Show("Contraseña no encontrada.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Hide()
            End If


        Else
            MessageBox.Show("Debes ingresar una contraseña.", "Integrated Sales System", MessageBoxButtons.OK, MessageBoxIcon.Error)

            TextBox1.Clear()
            TextBox1.Focus()
        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Autorizado = False
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' evita el sonido y el salto de línea
            ValidarContrasena()
        End If
    End Sub
End Class
