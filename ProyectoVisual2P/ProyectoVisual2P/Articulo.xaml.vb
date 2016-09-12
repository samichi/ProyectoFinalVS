Imports System.Data
Imports System.Data.OleDb

Public Class Articulo
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmArticulo_Loaded(sender As Object, e As RoutedEventArgs) Handles frmArticulo.Loaded
        CargarCategorias()
        Dim book As Libros = TryCast(Me.DataContext, Libros)
        If Not (book Is Nothing) Then
            cmbCategoria.SelectedValue = book.IdCategoria
        End If

        If Not administrador Then
            SoloLectura()

        End If
    End Sub

    Private Sub CargarCategorias()
        Dim winCategorias As Categorias = Me.Owner
        Using dbConexion As New OleDbConnection(winCategorias.strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Categorias" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsCategorias As New DataSet("Libros") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCategorias, "Categorias") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            Dim comboSource As New Dictionary(Of Integer, String)()
            For Each catg As DataRow In dsCategorias.Tables("Categorias").Rows
                cmbCategoria.Items.Add(catg(1))
                'comboSource.Add(catg(0), catg(1))
            Next
            Dim book As Articulo = DirectCast(Me.DataContext, Articulo)
            If Not book Is Nothing Then
                'cmbCategoria.SelectedValue = book.IdCategoria
            End If

        End Using
    End Sub

    Private Sub SoloLectura()
        Me.cmbCategoria.IsReadOnly = True
        Me.txtISBN.IsReadOnly = True
        Me.txtTitulo.IsReadOnly = True
        Me.txtAutor.IsReadOnly = True
        Me.txtEditorial.IsReadOnly = True
        Me.txtGenero.IsReadOnly = True
        Me.txtPrecio.IsReadOnly = True
        Me.cmbCategoria.IsEnabled = False
        Me.btnAceptar.Visibility = True 'Probar en casa


    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Close()

    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click

        Dim winCategorias As Categorias = Me.Owner
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Libros;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim libroCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsLibros As New DataSet("Libros")
            dbAdapter.Fill(dsLibros, "Libros")
            'dbAdapter.FillSchema(dsLibros, SchemaType.Source)
            'Actualizar el producto
            Dim foundBook = False
            Dim idFoundLibro As Integer
            If dsLibros Is Nothing Then
                Exit Sub
            Else
                For Each libro As DataRow In dsLibros.Tables("Libros").Rows
                    If libro("ISBN") = Me.txtISBN.Text Then
                        idFoundLibro = libro("IdLibro")
                        libro("Titulo") = Me.txtTitulo.Text
                        libro("Autor") = Me.txtAutor.Text
                        libro("Editorial") = Me.txtEditorial.Text
                        libro("Genero") = Me.txtGenero.Text
                        libro("Precio") = Me.txtPrecio.Text
                        foundBook = True
                        Exit For

                    End If
                Next
                If Not foundBook Then
                    Dim idLibro As Integer = dsLibros.Tables(0).Rows.Count
                    If foundBook Then
                        dsLibros.Tables(0).Rows.Add(idFoundLibro, Me.txtISBN.Text, Me.txtTitulo.Text, Me.txtAutor.Text, Me.txtEditorial.Text, Me.txtGenero.Text, Me.txtPrecio.Text, Me.cmbCategoria.SelectedIndex + 1)
                    Else
                        dsLibros.Tables(0).Rows.Add(idLibro + 1, Me.txtISBN.Text, Me.txtTitulo.Text, Me.txtAutor.Text, Me.txtEditorial.Text, Me.txtGenero.Text, Me.txtPrecio.Text, Me.cmbCategoria.SelectedIndex + 1)
                    End If
                End If
                Try
                    dbAdapter.Update(dsLibros.Tables("Libros"))
                    If foundBook Then
                        MsgBox("Se actualizó el producto...", vbOKOnly, "Confirmación")
                    Else
                        MsgBox("Se agregó el producto...", vbOKOnly, "Confirmación")
                    End If

                Catch es As Exception
                    MsgBox("Error al actualizar...", vbOKOnly, "Error")
                End Try
                Me.Close()
            End If
            winCategorias.UpdateDataGrid()

        End Using


    End Sub

    Private Sub frmArticulo_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmArticulo.Closing
        End
    End Sub
End Class
