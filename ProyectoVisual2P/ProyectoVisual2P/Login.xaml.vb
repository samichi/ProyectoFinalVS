Imports System.Data
Imports System.Data.OleDb

Class Login
    Public recogerDato As String
    Public administrador As Boolean
    Public nombreUsuario As String
    Public idUsuario As Integer
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Public dbAdapter As New OleDbDataAdapter
    Public dsUsuario As New DataSet

    Private Sub frmLogin_Loaded(sender As Object, e As RoutedEventArgs) Handles frmLogin.Loaded
        txtUsuario.Focus()

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click

        Using dbConexion As New OleDbConnection(strConexion)
            Dim banAdmin = 0 'Bandera de confirmar lo del administrador

            Dim strQuery As String = "SELECT * FROM InformacionUsuario" ' crear una consulta
            dbAdapter = New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            dsUsuario = New DataSet("Usuario") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsUsuario, "Usuario")
            For Each u As DataRow In dsUsuario.Tables("Usuario").Rows
                If Me.txtUsuario.Text = u(1) And Me.txtPassword.Password = u(2) Then
                    idUsuario = u(0)
                    recogerDato = u(4)
                    nombreUsuario = u(3)
                    banAdmin = 1
                    If recogerDato = "Activo" Then
                        administrador = True
                    Else
                        administrador = False
                    End If
                    If administrador = True Then
                        Dim winPrincAdmin As New PrincipalAdministrador
                        winPrincAdmin.Owner = Me
                        winPrincAdmin.administrador = Me.administrador
                        winPrincAdmin.idUsuario = Me.idUsuario
                        winPrincAdmin.nombreUsuario = Me.nombreUsuario
                        Me.Hide()
                        MsgBox("Bienvenido: " + nombreUsuario, vbOKOnly, "Welcome")
                        winPrincAdmin.Show()
                    Else
                        Dim winPrincVend As New PrincipalVendedor
                        winPrincVend.Owner = Me
                        winPrincVend.idUsuario = Me.idUsuario
                        winPrincVend.administrador = Me.administrador
                        winPrincVend.nombreUsuario = Me.nombreUsuario
                        Me.Hide()
                        MsgBox("Bienvenido: " + nombreUsuario, vbOKOnly, "Welcome")
                        winPrincVend.Show()
                    End If
                    Exit For
                End If

            Next
            
            If banAdmin = 0 Then
                MsgBox("Error al ingresar el usuario y/o contraseña ", MsgBoxStyle.OkOnly, "Error")
                LimpiarDatos()
                txtUsuario.Focus()
            End If
        End Using

    End Sub

    Public Sub LimpiarDatos()
        nombreUsuario = ""
        idUsuario = 0
        txtUsuario.Text = ""
        txtPassword.Password = ""
        txtUsuario.Focus()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        LimpiarDatos()
    End Sub

    Private Sub frmLogin_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmLogin.Closing
        End
    End Sub


End Class
