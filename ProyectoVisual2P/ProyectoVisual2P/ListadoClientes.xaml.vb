Imports System.Data.OleDb
Imports System.Data

Public Class ListadoClientes
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    'Private Sub dtgListadoClientes_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgListadoClientes.SelectionChanged
    '    Dim fila As DataRowView = sender.SelectedItem

    '    If fila Is Nothing Then
    '        Exit Sub

    '    End If
    '    Dim _consumidor As New Consumidor(fila(1), fila(2), fila(3), fila(4), fila(5))
    '    Dim winCliente As New DatoCliente
    '    winCliente.Owner = Me
    '    winCliente.DataContext = _consumidor
    '    winCliente.ShowDialog()
    'End Sub

    Private Sub frmListadoClientes_Loaded(sender As Object, e As RoutedEventArgs) Handles frmListadoClientes.Loaded
        Using dbConexion As New OleDbConnection(strConexion)
            Dim strQuery As String = "SELECT *FROM Clientes"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion)
            Dim dsClientes As New DataSet("Clientes")
            dbAdapter.Fill(dsClientes, "Clientes")
            dtgListadoClientes.DataContext = dsClientes
        End Using
        If administrador Then
            btnAceptar.Visibility = True
        Else
            SoloLectura()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As RoutedEventArgs) Handles btnNuevo.Click
        Dim winConsum As New DatoCliente
        winConsum.Owner = Me
        winConsum.ShowDialog()

    End Sub

    Public Sub UpdateDataGrid()
        Me.frmListadoClientes_Loaded(Nothing, Nothing)
    End Sub

    Private Sub SoloLectura()
        btnEliminar.Visibility = True
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As RoutedEventArgs) Handles btnSalir.Click
        Me.Hide()
    End Sub

    Private Sub dtgListadoClientes_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dtgListadoClientes.MouseDoubleClick

        Dim fila As DataRowView = sender.SelectedItem

        If fila Is Nothing Then
            Exit Sub

        End If
        Dim _consumidor As New Consumidor(fila(1), fila(2), fila(3), fila(4), fila(5))
        Dim winCliente As New DatoCliente
        winCliente.Owner = Me
        winCliente.DataContext = _consumidor
        winCliente.ShowDialog()


    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim winVendedorFact As VendendorFactura = Me.Owner
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Clientes;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim libroCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsClientes As New DataSet("Clientes")
            dbAdapter.Fill(dsClientes, "Clientes")
            'dbAdapter.FillSchema(dsLibros, SchemaType.Source)
            'Actualizar el producto
            Dim fila As DataRowView = dtgListadoClientes.SelectedItem

            winVendedorFact.lblNombreCliente.Content = fila(1)
            winVendedorFact.lblCedulaCliente.Content = fila(2)
            winVendedorFact.lblDireccionCliente.Content = fila(3)
            winVendedorFact.lblTelefonoCliente.Content = fila(4)

            Me.Hide()

        End Using
    End Sub

    Private Sub frmListadoClientes_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmListadoClientes.Closing
        End
    End Sub
End Class
