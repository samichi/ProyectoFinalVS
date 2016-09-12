Imports System.Data
Imports System.Data.OleDb

Public Class ListadoUsuarios
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmListadoUsuarios_Loaded(sender As Object, e As RoutedEventArgs) Handles frmListadoUsuarios.Loaded
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM InformacionUsuario" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsUsuarios As New DataSet("Usuarios") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsUsuarios, "Usuarios") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgUsuarios.DataContext = dsUsuarios
        End Using
    End Sub

    Private Sub dtgUsuarios_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dtgUsuarios.MouseDoubleClick

        Dim fila As DataRowView = sender.SelectedItem
        If fila Is Nothing Then
            Exit Sub
        End If
        Dim usuario As New Usuario(fila(1), fila(2), fila(3), fila(4))
        Dim winDatoUser As New InformacionUsuario
        winDatoUser.Owner = Me
        winDatoUser.DataContext = usuario
        winDatoUser.ShowDialog()


    End Sub

    Public Sub UpdateDataGrid()
        Me.frmListadoUsuarios_Loaded(Nothing, Nothing)
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As RoutedEventArgs) Handles btnEliminar.Click
        Using dbConexion As New OleDbConnection(strConexion)
            Dim id As Int32
            id = dtgUsuarios.SelectedIndex + 1
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "DELETE FROM Usuarios WHERE Usuarios.IdUsuario = " & id ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim userCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsUsuarios As New DataSet("Usuarios")
            dbAdapter.Fill(dsUsuarios, "Usuarios")

            ''Eliminar el usuario 
            'dsUsuarios.Tables("Usuarios").Rows.RemoveAt(id)

            Try
                dbAdapter.Update(dsUsuarios.Tables("Usuarios"))
                MsgBox("Se elimino el usuario...", vbOKOnly, "Confirmación")
            Catch es As Exception
                MsgBox("Se elimino el usuario...", vbOKOnly, "Confirmación")
            End Try
        End Using
        UpdateDataGrid()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As RoutedEventArgs) Handles btnNuevo.Click
        Dim winNewUser As New InformacionUsuario
        winNewUser.Owner = Me
        winNewUser.nuevoUsuario = True
        winNewUser.ShowDialog()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As RoutedEventArgs) Handles btnSalir.Click
        Dim winAdministrador As New PrincipalAdministrador
        winAdministrador = Me.Owner
        winAdministrador.administrador = Me.administrador
        winAdministrador.nombreUsuario = Me.nombreUsuario
        winAdministrador.idUsuario = Me.idUsuario
        winAdministrador.Show()
        Me.Hide()
    End Sub

    Private Sub frmListadoUsuarios_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmListadoUsuarios.Closing
        End
    End Sub
End Class
