Imports System.Data
Imports System.Data.OleDb

Public Class InformacionUsuario
    Public nuevoUsuario As Boolean
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmInformacionUsuario_Loaded(sender As Object, e As RoutedEventArgs) Handles frmInformacionUsuario.Loaded

        cmbAdministrador.Items.Add("Activo")
        cmbAdministrador.Items.Add("Inactivo")
        If nuevoUsuario Then
        Else
            CargarUsuario()
            Dim user As Usuario = TryCast(Me.DataContext, Usuario)
            If Not (user Is Nothing) Then
                cmbAdministrador.SelectedValue = user.Administrador
            End If
        End If

    End Sub

    Private Sub CargarUsuario()
        Dim winListadoUsuario As ListadoUsuarios = Me.Owner
        Using dbConexion As New OleDbConnection(winListadoUsuario.strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM InformacionUsuario" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsUsers As New DataSet("InformacionUsuario") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsUsers, "InformacionUsuario") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            'Dim comboSource As New Dictionary(Of Integer, String)()
            'For Each catg As DataRow In dsUsers.Tables("Usuarios").Rows
            '    cmbAdministrador.Items.Add(catg(4))
            'Next
            Dim usuario As Usuario = DirectCast(Me.DataContext, Usuario)


        End Using

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim winUsuarios As ListadoUsuarios = Me.Owner
       Using dbConexions As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM InformacionUsuario;" ' crear una consulta
            Dim db As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexions)) ' instanciado con la consulta y la coneccion
            Dim userCmdBuilder = New OleDbCommandBuilder(db)
            Dim dsInfUsuarios As New DataSet("InformacionUsuario")
            db.Fill(dsInfUsuarios, "InformacionUsuario")
            Dim foundUser = False
            Dim idFoundUsuario As Integer
            If dsInfUsuarios Is Nothing Then
                Exit Sub
            Else
                For Each user As DataRow In dsInfUsuarios.Tables("InformacionUsuario").Rows
                    If txtUsuario.Text = user("UsuarioName") Then
                        idFoundUsuario = user("IdInformacionUsuario")
                        txtPassword.Text = user("Contrasenia")
                        txtNombre.Text = user("NombreUsuarioReal")
                        foundUser = True
                        Exit For

                    End If
                Next
                If Not foundUser Then
                    Dim idLibro As Integer = dsInfUsuarios.Tables(0).Rows.Count
                    If foundUser Then
                        dsInfUsuarios.Tables(0).Rows.Add(idLibro, Me.txtUsuario.Text, Me.txtPassword.Text, Me.txtNombre.Text, cmbAdministrador.SelectedItem)
                    Else
                        dsInfUsuarios.Tables(0).Rows.Add(idLibro + 1, Me.txtUsuario.Text, Me.txtPassword.Text, Me.txtNombre.Text, cmbAdministrador.SelectedItem)
                    End If
                End If

                Try
                    db.Update(dsInfUsuarios.Tables("InformacionUsuario"))
                    If foundUser Then
                        MsgBox("Se actualizó el usuario...", vbOKOnly, "Confirmación")
                    Else
                        MsgBox("Se agregó el usuario...", vbOKOnly, "Confirmación")
                    End If

                Catch es As Exception
                    MsgBox("Error al actualizar..." & es.ToString, vbOKOnly, "Error")
                End Try
                Me.Close()
            End If

        End Using
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


    Private Sub frmInformacionUser_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmInformacionUser.Closing
        End
    End Sub
End Class
