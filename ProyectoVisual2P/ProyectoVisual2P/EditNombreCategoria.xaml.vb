Imports System.Data.OleDb
Imports System.Data

Public Class EditNombreCategoria
    Public idCategoria As Integer
    Public idUsuario As Integer
    Public foundCat = False
    Public idFoundCat As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmCategoria_Loaded(sender As Object, e As RoutedEventArgs) Handles frmCategoria.Loaded
        CargarCategoria()
        Dim objCategoria As Categoria = TryCast(Me.DataContext, Categoria)
    End Sub

    Private Sub CargarCategoria()
        Dim winCategorias As Categorias = Me.Owner
        Using dbConexion As New OleDbConnection(winCategorias.strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Categorias;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsCateg As New DataSet("Categorias") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCateg, "Categorias") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            Dim categoria As Categoria = DirectCast(Me.DataContext, Categoria)
            For Each row As DataRow In dsCateg.Tables(0).Rows
                Me.txtCategoria.Text = row(1)
                
            Next
        End Using
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim winCategorias As Categorias = Me.Owner
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Categorias;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim libroCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsCategEdit As New DataSet("Categ")
            dbAdapter.Fill(dsCategEdit, "Categ")
            'dbAdapter.FillSchema(dsLibros, SchemaType.Source)
            'Actualizar el producto


            If dsCategEdit Is Nothing Then
                Exit Sub
            Else
                For Each objCat As DataRow In dsCategEdit.Tables(0).Rows
                    If objCat("NombreCategoria") = Me.txtCategoria.Text Then
                        idFoundCat = objCat("IdCategoria")
                        foundCat = True
                        Exit For

                    End If
                Next
                If Not foundCat Then
                    Dim idNewCat As Integer = dsCategEdit.Tables(0).Rows.Count + 1
                    If foundCat Then
                        dsCategEdit.Tables(0).Rows.Add(idFoundCat, Me.txtCategoria.Text)
                    Else
                        dsCategEdit.Tables(0).Rows.Add(idNewCat, Me.txtCategoria.Text)
                    End If
                End If
                Try
                    dbAdapter.Update(dsCategEdit.Tables(0))
                    If foundCat Then
                        MsgBox("Se actualizó la categoría...", vbOKOnly, "Confirmación")
                    Else
                        MsgBox("Se agregó la categoría...", vbOKOnly, "Confirmación")
                    End If

                Catch es As Exception
                    MsgBox("Error al actualizar...", vbOKOnly, "Error")
                End Try
                Me.Close()
            End If

        End Using
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class
