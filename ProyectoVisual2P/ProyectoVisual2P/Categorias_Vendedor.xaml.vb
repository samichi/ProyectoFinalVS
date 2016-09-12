Imports System.Data
Imports System.Data.OleDb

Public Class Categorias_Vendedor
    Public idUsuario As Integer
    Public idFactura As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public IdBook As Integer
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    'Public idLibro As Int32
    'Public cantidadProducto As Int32
    'Public precioTotal As Double
    Private Sub frmCategoriasVendedor_Loaded(sender As Object, e As RoutedEventArgs) Handles frmCategorias_Vendedor.Loaded
        'OTRA FORMA CON EL BLOQUE USING
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Libros" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsLibros As New DataSet("Libros") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsLibros, "Libros") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgCategorias.DataContext = dsLibros

        End Using

        Using dbConexion2 As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Categorias" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion2) ' instanciado con la consulta y la coneccion
            Dim dsCategorias As New DataSet("Categorias") 'nombre que yo quiera

            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCategorias, "Categorias") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            
        End Using
        txtcantidad.IsReadOnly = True
        btnAceptar.IsEnabled = False
        btnCalcular.IsEnabled = False


    End Sub

    Private Sub dtgCategorias_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgCategorias.SelectionChanged
        Dim fila As DataRowView = sender.SelectedItem
        txtcantidad.IsReadOnly = False

        btnCalcular.IsEnabled = True
        'idLibro = fila(0)

        'IdBook = fila(0)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As RoutedEventArgs) Handles btnSalir.Click
        If administrador Then
            Dim winAdministrador As New PrincipalAdministrador
            winAdministrador = Me.Owner
            winAdministrador.administrador = Me.administrador
            winAdministrador.nombreUsuario = Me.nombreUsuario
            winAdministrador.idUsuario = Me.idUsuario
            winAdministrador.Show()
            Me.Hide()
        Else
            Dim winVend As New PrincipalVendedor
            winVend = Me.Owner
            winVend.administrador = Me.administrador
            winVend.nombreUsuario = Me.nombreUsuario
            winVend.idUsuario = Me.idUsuario
            winVend.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub frmCategorias_Vendedor_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmCategorias_Vendedor.Closing
        End
    End Sub

    Public Sub UpdateDataGrid()
        Me.frmCategoriasVendedor_Loaded(Nothing, Nothing)
    End Sub

    'Private Sub dtgCategorias_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dtgCategorias.MouseDoubleClick
    '    Using dbConexion3 As New OleDbConnection(strConexion)
    '        Console.WriteLine("Conexion exitosa")
    '        Dim strQuery As String = "SELECT * FROM Libros WHERE IdCategoria = " & (CInt(cmbCategoriasListado.SelectedIndex) + 1) ' crear una consulta

    '        Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion3) ' instanciado con la consulta y la coneccion
    '        Dim dsLibros As New DataSet("Libros") 'nombre que yo quiera
    '        'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
    '        dbAdapter.Fill(dsLibros, "Libros") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
    '        dtgCategorias.DataContext = dsLibros
    '    End Using
    'End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim fila As DataRowView = dtgCategorias.SelectedItem
        Dim winVendedorFact As VendendorFactura = Me.Owner
        winVendedorFact.idBook = Me.IdBook
        Dim idLibro As Integer = winVendedorFact.dsDetalle.Tables(0).Rows.Count

        winVendedorFact.dsDetalle.Tables(0).Rows.Add(fila(1), txtcantidad.Text, fila(2), fila(6), lblSubtotal.Content)
        'winVendedorFact.dsDetalle.Tables(0).Rows.Add(fila(0), fila(1), fila(2), fila(6), txtcantidad.Text, lblSubtotal.Content)
        winVendedorFact.cmbProvincia.Items.Clear()
        winVendedorFact.cmbTipoPago.Items.Clear()
        'winVendedorFact.UpdateDataGrid()
        Me.Hide()
    End Sub

    Private Sub btnCalcular_Click(sender As Object, e As RoutedEventArgs) Handles btnCalcular.Click
        Dim fila As DataRowView = dtgCategorias.SelectedItem
        lblSubtotal.Content = fila(6) * txtcantidad.Text
        btnAceptar.IsEnabled = True
    End Sub
End Class