Imports System.Data.OleDb
Imports System.Data

Public Class EditIva
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    Private Sub frmEditIva_Loaded(sender As Object, e As RoutedEventArgs) Handles frmEditIva.Loaded
        CargarEditIva()
        Dim prov As Iva = TryCast(Me.DataContext, Iva)
    End Sub

    Private Sub CargarEditIva()
        Dim winIvaXProvincia As IvaXProvincia = Me.Owner
        Using dbConexion As New OleDbConnection(winIvaXProvincia.strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Provincia" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsProvincia As New DataSet("Provincia") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsProvincia, "Provincia") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
        End Using

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim winIvaXProvincia As IvaXProvincia = Me.Owner
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Provincia;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim provCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsProvincia As New DataSet("Provincia")
            dbAdapter.Fill(dsProvincia, "Provincia")
            'dbAdapter.FillSchema(dsLibros, SchemaType.Source)
            'Actualizar el producto
            Dim foundProvincia = False


            If dsProvincia Is Nothing Then
                Exit Sub
            Else
                For Each clie As DataRow In dsProvincia.Tables(0).Rows
                    If Me.txtProvincia.Text = clie(1) Then
                        clie(2) = Me.txtIva.Text
                        foundProvincia = True
                        Exit For

                    End If
                Next
                If Not foundProvincia Then
                    dsProvincia.Tables("Provincia").Rows.Add(Me.txtProvincia.Text, Me.txtIva.Text)

                End If
                Try
                    dbAdapter.Update(dsProvincia.Tables("Provincia"))
                    If foundProvincia Then
                        MsgBox("Se actualizó la provincia...", vbOKOnly, "Confirmación")
                    End If

                Catch es As Exception
                    MsgBox("Error al actualizar...", vbOKOnly, "Error")
                End Try
                Me.Close()
            End If
            'winIvaXProvincia.UpdateDataGrid()


        End Using
    End Sub

    Private Sub frmEditIva_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmEditIva.Closing
        End
    End Sub
End Class
