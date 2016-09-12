Imports System.Data
Imports System.Data.OleDb

Public Class FacturaDetalle
    Public idFactura As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmFacturaDetalle_Loaded(sender As Object, e As RoutedEventArgs) Handles frmFacturaDetalle.Loaded
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Dim strQuery As String = "SELECT DetalleFactura.ISBN, DetalleFactura.Cantidad, DetalleFactura.Titulo, DetalleFactura.Precio, DetalleFactura.PrecioTotal FROM DetalleFactura INNER JOIN Factura ON  DetalleFactura.IdFactura =Factura.IdFactura WHERE DetalleFactura.IdFactura=" & idFactura ' crear una consulta

            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion
            Dim dsDetalle As New DataSet("Detalle") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsDetalle, "Detalle") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgDetalle.DataContext = dsDetalle
            dtgDetalle.IsEnabled = False
        End Using


        'DATOS DE CABECERA
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Dim strQuery As String = "SELECT * FROM Factura WHERE Factura.IdFactura=" & idFactura ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion
            Dim dsCabecera As New DataSet("Cabecera") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCabecera, "Cabecera") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado

            For Each row As DataRow In dsCabecera.Tables(0).Rows

                'Dim id As String = row("ID_CLIENTE")
                lblNumFactura2.Content = row("IdFactura")
                lblFech.Content = row("Fecha")
                lblTipoPag.Content = row("TipoPago")
                lblProvinci.Content = row("Provincia")
                lblNombreVendedor.Content = row("Vendedor")
                lblNombreCliente.Content = row("NombreCliente")
                lblCedulaCliente.Content = row("Cedula")
                lblDireccionCliente.Content = row("Direccion")
                lblTelefonoCliente.Content = row("Telefono")
                lblSubtotal2.Content = row("Subtotal")
                lblIVA2.Content = row("IVA")
                lblTotalPagar2.Content = row("TotalAPagar")
                lblDevolucion2.Content = row("Devolucion")

            Next

        End Using

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Hide()
    End Sub

    Private Sub frmFacturaDetalle_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmFacturaDetalle.Closing
        End
    End Sub
End Class
