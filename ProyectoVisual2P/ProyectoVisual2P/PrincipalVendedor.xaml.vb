Imports System.Data.OleDb
Imports System.Data

Public Class PrincipalVendedor
    Public nombreUsuario As String
    Public administrador As String
    Public idUsuario As Integer
    Public idFactura As Integer
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    Private Sub frmPrincipalVendedor_Loaded(sender As Object, e As RoutedEventArgs) Handles frmPrincipalVendedor.Loaded
        Using dbConexion As New OleDbConnection(strConexion)
            Dim strQuery As String = "SELECT Factura.IdFactura, Factura.Fecha, Factura.TipoPago, Factura.Provincia, Factura.NombreCliente, Factura.TotalAPagar, Factura.Devolucion FROM Factura WHERE Factura.Vendedor='" & nombreUsuario & "'" ' crear una consulta"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion
            Dim dsFacturas As New DataSet("NuevaFactura") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsFacturas, "NuevaFactura") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgVendFacturas.DataContext = dsFacturas
        End Using
    End Sub

    Private Sub mniProducto_Click(sender As Object, e As RoutedEventArgs) Handles mniProducto.Click
        Dim winListadoProducto As New Categorias
        winListadoProducto.Owner = Me
        winListadoProducto.administrador = Me.administrador
        winListadoProducto.nombreUsuario = Me.nombreUsuario
        winListadoProducto.idUsuario = Me.idUsuario
        winListadoProducto.Show()

    End Sub

    Private Sub mniAyuda_Click(sender As Object, e As RoutedEventArgs) Handles mniAyuda.Click
        MsgBox("Creado por María Baque e Isabel León", vbOKOnly, "Ayuda")
    End Sub

    Private Sub mniSalir_Click(sender As Object, e As RoutedEventArgs) Handles mniSalir.Click
        Dim winLogin As New Login
        winLogin.Owner = Me
        winLogin.LimpiarDatos()
        Me.Hide()
        winLogin.ShowDialog()
        Me.LimpiarDatos()


    End Sub

    Public Sub LimpiarDatos()
        nombreUsuario = ""
        idUsuario = 0
    End Sub

    Private Sub mniIvaProvincia_Click(sender As Object, e As RoutedEventArgs) Handles mniIvaProvincia.Click
        Dim winListadoIvaProv As New IvaXProvincia
        winListadoIvaProv.Owner = Me
        winListadoIvaProv.administrador = Me.administrador
        winListadoIvaProv.nombreUsuario = Me.nombreUsuario
        winListadoIvaProv.idUsuario = Me.idUsuario
        winListadoIvaProv.ShowDialog()
    End Sub

    Private Sub mniClientes_Click(sender As Object, e As RoutedEventArgs) Handles mniClientes.Click
        Dim winListClientes As New ListadoClientes
        winListClientes.Owner = Me
        winListClientes.administrador = Me.administrador
        winListClientes.nombreUsuario = Me.nombreUsuario
        winListClientes.idUsuario = Me.idUsuario
        winListClientes.Show()

    End Sub

    Private Sub mniFactura_Click(sender As Object, e As RoutedEventArgs) Handles mniFactura.Click
        Dim winCrearFactura As New VendendorFactura
        winCrearFactura.administrador = Me.administrador
        winCrearFactura.nombreUsuario = Me.nombreUsuario
        winCrearFactura.idUsuario = Me.idUsuario
        winCrearFactura.Owner = Me
        winCrearFactura.Show()
        Me.UpdateDataGrid()
    End Sub

    Public Sub UpdateDataGrid()
        Me.frmPrincipalVendedor_Loaded(Nothing, Nothing)
    End Sub

    Private Sub frmPrincipalVendedor_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmPrincipalVendedor.Closing
        End
    End Sub

    Private Sub dtgVendFacturas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgVendFacturas.SelectionChanged

        Dim fila As DataRowView = sender.SelectedItem

        Dim winarticulo As New FacturaDetalle
        idFactura = fila(0)
        winarticulo.idFactura = Me.idFactura

        winarticulo.ShowDialog()
    End Sub
End Class

