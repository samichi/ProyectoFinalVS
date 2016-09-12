Imports System.Data
Imports System.Data.OleDb

Public Class PrincipalAdministrador
    Public idUsuario As Integer
    Public idFactura As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    Private Sub frmPrincipalAdministrador_Loaded(sender As Object, e As RoutedEventArgs) Handles frmPrincipalAdministrador.Loaded
        Using dbConexion As New OleDbConnection(strConexion)
            Dim strQuery As String = "Select Factura.IdFactura, Factura.Fecha, Factura.NombreCliente, Factura.Vendedor, Factura.TotalAPagar, Factura.Devolucion FROM Factura" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsLibros As New DataSet("Factura") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsLibros, "Factura") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgListadoGeneral.DataContext = dsLibros
        End Using
    End Sub
    Private Sub dtgListadoGeneral_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgListadoGeneral.SelectionChanged
        Dim fila As DataRowView = sender.SelectedItem
        
        Dim winarticulo As New FacturaDetalle
        idFactura = fila(0)
        winarticulo.idFactura = Me.idFactura
        
        winarticulo.ShowDialog()
    End Sub

    Private Sub mniListadoProductos_Click(sender As Object, e As RoutedEventArgs) Handles mniListadoProductos.Click
        Dim winCategoriasAdministrador As New Categorias
        winCategoriasAdministrador.Owner = Me
        winCategoriasAdministrador.administrador = Me.administrador
        winCategoriasAdministrador.nombreUsuario = Me.nombreUsuario
        winCategoriasAdministrador.idUsuario = Me.idUsuario
        winCategoriasAdministrador.ShowDialog()

    End Sub

    Private Sub mniSalir_Click(sender As Object, e As RoutedEventArgs) Handles mniSalir.Click
        Dim winLogin As New Login
        winLogin.Owner = Me
        winLogin.LimpiarDatos()
        Me.Hide()
        winLogin.ShowDialog()
        Me.LimpiarDatos()
    End Sub

    Private Sub mniAyuda_Click(sender As Object, e As RoutedEventArgs) Handles mniAyuda.Click
        MsgBox("Creado por María Baque e Isabel León", vbOKOnly, "Ayuda")
    End Sub

    Public Sub LimpiarDatos()
        nombreUsuario = ""
        idUsuario = 0
    End Sub

    Private Sub mniIvaProv_Click(sender As Object, e As RoutedEventArgs) Handles mniIvaProv.Click
        Dim winIva As New IvaXProvincia
        winIva.Owner = Me
        winIva.idUsuario = Me.idUsuario
        winIva.nombreUsuario = Me.nombreUsuario
        winIva.administrador = Me.administrador
        winIva.idUsuario = Me.idUsuario
        winIva.Show()
    End Sub

    Private Sub mniListadoClientes_Click(sender As Object, e As RoutedEventArgs) Handles mniListadoClientes.Click
        Dim winListCliente As New ListadoClientes
        winListCliente.Owner = Me
        winListCliente.idUsuario = Me.idUsuario
        winListCliente.nombreUsuario = Me.nombreUsuario
        winListCliente.administrador = Me.administrador
        winListCliente.Show()
    End Sub

    Private Sub frmPrincipalAdministrador_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmPrincipalAdministrador.Closing
        End
    End Sub

    Private Sub mniListadoUsuario_Click(sender As Object, e As RoutedEventArgs) Handles mniListadoUsuario.Click
        Dim winListUser As New ListadoUsuarios
        winListUser.Owner = Me
        winListUser.idUsuario = Me.idUsuario
        winListUser.nombreUsuario = Me.nombreUsuario
        winListUser.administrador = Me.administrador
        winListUser.Show()

    End Sub
End Class
