Imports System.Data.OleDb
Imports System.Data

Public Class IvaXProvincia
    Public idIva As Integer
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    Private Sub frmIvaProvincias_Loaded(sender As Object, e As RoutedEventArgs) Handles frmIvaProvincias.Loaded
        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Provincia" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsLibros As New DataSet("Provincias") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsLibros, "Provincias") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgIva.DataContext = dsLibros
        End Using
        If Not administrador Then
            dtgIva.IsReadOnly = True
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As RoutedEventArgs) Handles btnSalir.Click
        If administrador Then
            Dim winAdmin As New PrincipalAdministrador
            winAdmin = Me.Owner
            winAdmin.administrador = Me.administrador
            winAdmin.nombreUsuario = Me.nombreUsuario
            winAdmin.idUsuario = Me.idUsuario
            winAdmin.Show()
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

    Sub UpdateDataGrid()
        frmIvaProvincias_Loaded(Nothing, Nothing)
    End Sub

    Private Sub frmIvaProvincias_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmIvaProvincias.Closing
        End
    End Sub

    Private Sub dtgIva_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles dtgIva.MouseDoubleClick
        If administrador Then
            Dim fila As DataRowView = sender.SelectedItem
            If fila Is Nothing Then
                Exit Sub
            End If
            Dim iva As New Iva(fila(1), fila(2))
            idIva = fila(0)
            Dim _EditIva As New EditIva
            _EditIva.Owner = Me
            _EditIva.DataContext = iva
            _EditIva.ShowDialog()
        End If
    End Sub
End Class
