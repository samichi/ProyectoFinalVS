Imports System.Data
Imports System.Data.OleDb

Public Class Categorias
    Public idUsuario As Integer
    Public idFactura As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmCategorias_Loaded(sender As Object, e As RoutedEventArgs) Handles frmCategorias.Loaded
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
            'Dim comboSource As New Dictionary(Of Integer, String)()
            For Each catg As DataRow In dsCategorias.Tables("Categorias").Rows
                cmbCategoriasListado.Items.Add(catg(1))

            Next
        End Using

        If Not administrador Then
            SoloLectura()
        End If
    End Sub

    'Private Sub dtgCategorias_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgCategorias.SelectionChanged
    '    Dim fila As DataRowView = sender.SelectedItem
    '    Dim _libro As New Articulo
    '    _libro.Owner = Me
    '    If fila Is Nothing Then
    '        Exit Sub
    '    End If
    '    Dim producto As New Libros(fila(1), fila(2), fila(3), fila(4), fila(5), fila(6), fila(7))
    '    Dim winarticulo As New Articulo
    '    winarticulo.Owner = Me
    '    winarticulo.DataContext = producto
    '    winarticulo.cmbCategoria.SelectedIndex = fila(7) - 1
    '    winarticulo.ShowDialog()
    'End Sub

    Private Sub cmbCategoriasListado_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCategoriasListado.SelectionChanged
        Using dbConexion3 As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Libros WHERE IdCategoria = " & (CInt(cmbCategoriasListado.SelectedIndex) + 1) ' crear una consulta

            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion3) ' instanciado con la consulta y la coneccion
            Dim dsCat As New DataSet("Libros") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCat, "Libros") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgCategorias.DataContext = dsCat
        End Using

    End Sub

    Private Sub SoloLectura()
        Me.btnNuevo.Visibility = True
        Me.btnEliminar.Visibility = True
        Me.btnCatNuevo.Visibility = True

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

    Private Sub frmCategorias_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmCategorias.Closing
        End
    End Sub

    Public Sub UpdateDataGrid()
        Me.frmCategorias_Loaded(Nothing, Nothing)
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As RoutedEventArgs) Handles btnNuevo.Click
        Dim winLibro As New Articulo
        winLibro.Owner = Me
        winLibro.nombreUsuario = Me.nombreUsuario
        winLibro.administrador = Me.administrador
        winLibro.idUsuario = Me.idUsuario
        winLibro.ShowDialog()

    End Sub

    Private Sub dtgCategorias_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dtgCategorias.MouseDoubleClick
        If administrador Then
            Dim fila As DataRowView = sender.SelectedItem
            Dim _libro As New Articulo
            _libro.Owner = Me
            If fila Is Nothing Then
                Exit Sub
            End If
            Dim producto As New Libros(fila(1), fila(2), fila(3), fila(4), fila(5), fila(6), fila(7))
            Dim winarticulo As New Articulo
            winarticulo.Owner = Me
            winarticulo.DataContext = producto
            winarticulo.cmbCategoria.SelectedIndex = fila(7) - 1
            winarticulo.ShowDialog()
        Else
            dtgCategorias.IsReadOnly = True
        End If
        
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As RoutedEventArgs) Handles btnEliminar.Click
        Using dbConexion As New OleDbConnection(strConexion)
            Dim id As Int32
            id = dtgCategorias.SelectedIndex + 1
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "DELETE FROM Libros WHERE Libros.IdLibro = " & id ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim bookCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsUsuarios As New DataSet("Libros")
            dbAdapter.Fill(dsUsuarios, "Libros")

            'Eliminar el producto
            Try
                dbAdapter.Update(dsUsuarios.Tables("Libros"))
                MsgBox("Se elimino el producto...", vbOKOnly, "Confirmación")
            Catch es As Exception
                MsgBox("Se elimino el producto...", vbOKOnly, "Confirmación")
            End Try
        End Using
        UpdateDataGrid()
    End Sub

    'Private Sub btnCatEliminar_Click(sender As Object, e As RoutedEventArgs) Handles btnCatEliminar.Click
    '    Using dbConexion As New OleDbConnection(strConexion)
    '        Dim id As Int32
    '        id = cmbCategoriasListado.SelectedIndex + 1
    '        Console.WriteLine("Conexion exitosa")
    '        Dim strQuery As String = "DELETE FROM Categorias WHERE Categorias.IdCategoria = " & id ' crear una consulta
    '        Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
    '        Dim bookCmdBuilder = New OleDbCommandBuilder(dbAdapter)
    '        Dim dsCategorias As New DataSet("Categorias")
    '        dbAdapter.Fill(dsCategorias, "Categorias")

    '        'Eliminar la categoria
    '        Try
    '            dbAdapter.Update(dsCategorias.Tables("Categorias"))
    '            MsgBox("Se elimino la categoria...", vbOKOnly, "Confirmación")
    '        Catch es As Exception
    '            MsgBox("Se elimino la categoria...", vbOKOnly, "Confirmación")
    '        End Try
    '    End Using
    '    'cmbCategoriasListado.SelectedItem = -1
    '    UpdateDataGrid()
    'End Sub

    Private Sub btnCatNuevo_Click(sender As Object, e As RoutedEventArgs) Handles btnCatNuevo.Click
        Dim winNuevaCategoria As New EditNombreCategoria
        winNuevaCategoria.Owner = Me
        winNuevaCategoria.idUsuario = Me.idUsuario
        winNuevaCategoria.administrador = Me.administrador
        winNuevaCategoria.nombreUsuario = Me.nombreUsuario
        winNuevaCategoria.Show()
        winNuevaCategoria.txtCategoria.Focus()
        Me.Hide()

    End Sub

    Private Sub cmbCategoriasListado_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles cmbCategoriasListado.MouseDoubleClick
        If administrador Then
            Dim nombreCategoria As String = cmbCategoriasListado.SelectedItem
            Dim idCategoria As Integer = cmbCategoriasListado.SelectedIndex + 1
            'Dim filaRow As DataRowView = sender.SelectedIndex + 1
            'If fila Is Nothing Then
            '    Exit Sub
            'End If
            Dim ediCat As New Categoria(idCategoria, nombreCategoria)
            Dim winCategoriaEdit As New EditNombreCategoria
            winCategoriaEdit.Owner = Me
            winCategoriaEdit.DataContext = ediCat
            'winCategoriaEdit.DataContext = ediCat
            winCategoriaEdit.ShowDialog()
        Else
            cmbCategoriasListado.IsReadOnly = True
        End If
    End Sub
End Class
