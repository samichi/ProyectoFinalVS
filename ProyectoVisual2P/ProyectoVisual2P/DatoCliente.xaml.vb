Imports System.Data.OleDb
Imports System.Data

Public Class DatoCliente
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    Private Sub frmCliente_Loaded(sender As Object, e As RoutedEventArgs) Handles frmCliente.Loaded
        CargarCliente()
        Dim cons As Consumidor = TryCast(Me.DataContext, Consumidor)
    End Sub

    Private Sub CargarCliente()
        Dim winListCliente As ListadoClientes = Me.Owner
        Using dbConexion As New OleDbConnection(winListCliente.strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Clientes" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsCLie As New DataSet("Clientes") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsCLie, "Clientes") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
        End Using
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        Dim winListado As ListadoClientes = Me.Owner
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Clientes;" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(New OleDbCommand(strQuery, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim libroCmdBuilder = New OleDbCommandBuilder(dbAdapter)
            Dim dsClientes As New DataSet("Clientes")
            dbAdapter.Fill(dsClientes, "Clientes")

            'ACTUALIZAR EL CLIENTE
            Dim foundCliente = False
            Dim idFoundCliente As Integer
            If dsClientes Is Nothing Then
                Exit Sub
            Else
                For Each clie As DataRow In dsClientes.Tables("Clientes").Rows
                    If Me.txtNombre.Text = clie("NombreCliente") Then
                        'clie("NombreCliente") = txtNombre.Text
                        idFoundCliente = clie("IdCliente")
                        clie("Cedula") = Me.txtcedula.Text
                        clie("Direccion") = Me.txtDireccion.Text
                        clie("Telefono") = Me.txtTelefono.Text
                        clie("CorreoElectronico") = Me.txtCorreo.Text
                        foundCliente = True
                        Exit For

                    End If
                Next

                If Not foundCliente Then
                    Dim idCliente As Integer = dsClientes.Tables(0).Rows.Count
                    If foundCliente Then
                        'ACTUALIZAR EL CLIENTE
                        dsClientes.Tables("Clientes").Rows.Add(idFoundCliente, Me.txtNombre.Text, Me.txtcedula.Text, Me.txtDireccion.Text, Me.txtTelefono.Text, Me.txtCorreo.Text)
                    Else
                        'AGREGAR EL CLIENTE SI NO FUE ENCONTRADO
                        dsClientes.Tables("Clientes").Rows.Add(idCliente + 1, Me.txtNombre.Text, Me.txtcedula.Text, Me.txtDireccion.Text, Me.txtTelefono.Text, Me.txtCorreo.Text)
                    End If

                End If
                Try
                    dbAdapter.Update(dsClientes.Tables("Clientes"))
                    If foundCliente Then
                        MsgBox("Se actualizó el cliente...", vbOKOnly, "Confirmación")
                    Else
                        MsgBox("Se agregó el cliente...", vbOKOnly, "Confirmación")
                    End If

                Catch es As Exception
                    MsgBox("Error al actualizar...", vbOKOnly, "Error")
                End Try
                Me.Close()
            End If
            'ACTUALIZA EL DATAGRID
            winListado.UpdateDataGrid()
        End Using
    End Sub


    Private Sub frmCliente_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmCliente.Closing
        End
    End Sub
End Class
