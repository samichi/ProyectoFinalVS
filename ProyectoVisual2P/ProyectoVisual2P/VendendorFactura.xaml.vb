Imports System.Data
Imports System.Data.OleDb

Public Class VendendorFactura
    Public nombreUsuario As String
    Public porcentajeDevolucion As Double
    Public dsDetalle As New DataSet("Detalle")
    Public administrador As Boolean
    Public idUsuario As Integer
    Public idBook As Integer
    Public IVA As Double
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    Private Sub btnCargarClientes_Click(sender As Object, e As RoutedEventArgs) Handles btnCargarClientes.Click
        Dim winListadoCliente As New ListadoClientes
        winListadoCliente.Owner = Me
        winListadoCliente.Show()

    End Sub

    Private Sub frmVendedorFactura_Loaded(sender As Object, e As RoutedEventArgs) Handles frmVendedorFactura.Loaded
        'cmbProvincia.Items.Clear()
        'cmbTipoPago.Items.Clear()

        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM TipoPago" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsTipoPago As New DataSet("TipoPago") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsTipoPago, "TipoPago") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            For Each tipoPag As DataRow In dsTipoPago.Tables("TipoPago").Rows
                cmbTipoPago.Items.Add(tipoPag(1))
            Next

            txtFecha.IsEnabled = False

            'Dim iva As Iva = TryCast(Me.DataContext, Iva)
            'If Not (iva Is Nothing) Then
            '    cmbTipoPago.SelectedValue = iva.Provincia
            'End If
        End Using
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT * FROM Provincia" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsProvincia As New DataSet("Provincia") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsProvincia, "Provincia") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            For Each prov As DataRow In dsProvincia.Tables("Provincia").Rows
                cmbProvincia.Items.Add(prov(1))
            Next
            'Dim iva As Iva = TryCast(Me.DataContext, Iva)
            'If Not (iva Is Nothing) Then
            '    cmbProvincia.SelectedValue = iva.Provincia
            'End If
        End Using
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT IdFactura FROM Factura" ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsProvincia As New DataSet("Provincia") 'nombre que yo quiera
            'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
            dbAdapter.Fill(dsProvincia, "Provincia") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            For Each prov As DataRow In dsProvincia.Tables("Provincia").Rows
                lblNumFactura2.Content = (prov(0)) + 1
            Next
            'Dim iva As Iva = TryCast(Me.DataContext, Iva)
            'If Not (iva Is Nothing) Then
            '    cmbProvincia.SelectedValue = iva.Provincia
            'End If
        End Using
        txtFecha.Text = Date.Now.Date
        lblNombreVendedor.Content = nombreUsuario
        'nombre que yo quiera
        'CARGAR LOS DATOS QUE ESTAN DENTRO DE ESA BASE DE DATOS
        'dbAdapter.Fill(dsLibros, "Libros") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT DetalleFactura.ISBN, DetalleFactura.Cantidad, DetalleFactura.Titulo, DetalleFactura.Precio, DetalleFactura.PrecioTotal FROM DetalleFactura INNER JOIN Factura ON  DetalleFactura.IdFactura =Factura.IdFactura WHERE DetalleFactura.IdFactura=" & lblNumFactura2.Content ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            dbAdapter.Fill(dsDetalle, "Detalle") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado
            dtgDetalle.DataContext = dsDetalle
        End Using
    End Sub

    Public Sub UpdateDataGrid()
        Me.frmVendedorFactura_Loaded(Nothing, Nothing)
    End Sub

    Public suma As Double = 0

    Private Sub btnAgregar_Click(sender As Object, e As RoutedEventArgs) Handles btnAgregar.Click
        Dim winDescripcionDetfact As New Categorias_Vendedor

        winDescripcionDetfact.Owner = Me
        winDescripcionDetfact.ShowDialog()
        sumar()
        'MsgBox(suma)
        'Me.Hide()
    End Sub
    Function sumar()
        suma = 0
        For number As Int32 = 0 To dsDetalle.Tables(0).Rows.Count - 1 Step 1
            suma = suma + dsDetalle.Tables(0).Rows(number).Item(4)
        Next
        lblSubtotal2.Content = suma
        lblTotalPagar2.Content = lblSubtotal2.Content + (lblSubtotal2.Content * lblIVA2.Content)
        lblDevolucion2.Content = lblTotalPagar2.Content * porcentajeDevolucion
        Return suma
    End Function
    Private Sub btnEliminar_Click(sender As Object, e As RoutedEventArgs) Handles btnEliminar.Click
        Dim a As Int32
        a = dtgDetalle.SelectedIndex
        dsDetalle.Tables(0).Rows.RemoveAt(a)

        Me.UpdateDataGrid()
        sumar()
    End Sub

    Private Sub cmbProvincia_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProvincia.SelectionChanged
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT IVA FROM Provincia where IdProvincia=" & cmbProvincia.SelectedIndex + 1 ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsPro As New DataSet("Provincia") 'nombre que yo quiera
            dbAdapter.Fill(dsPro, "Provincia")
            For Each u As DataRow In dsPro.Tables("Provincia").Rows
                lblIVA2.Content = u(0)
            Next
            sumar()
        End Using
    End Sub

    Private Sub cmbTipoPago_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbTipoPago.SelectionChanged
        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)
            Console.WriteLine("Conexion exitosa")
            Dim strQuery As String = "SELECT PorcentajeDevolucion FROM TipoPago where IdTipoPago=" & cmbTipoPago.SelectedIndex + 1 ' crear una consulta
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion) ' instanciado con la consulta y la coneccion

            Dim dsTipoPago As New DataSet("TipoPago") 'nombre que yo quiera
            dbAdapter.Fill(dsTipoPago, "TipoPago")
            For Each u As DataRow In dsTipoPago.Tables("TipoPago").Rows
                porcentajeDevolucion = u(0)
            Next
        End Using
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As RoutedEventArgs) Handles btnAceptar.Click
        'nombreusuario
        'idusuario
        'Dim idCliente As Int32
        Dim winPrincipalVendedor As PrincipalVendedor = Me.Owner

        Using dbConexion As New System.Data.OleDb.OleDbConnection(strConexion)

            Console.WriteLine("Conexion exitosa")
            Dim strQueryFactura As String = "SELECT * FROM Factura;" '"SELECT IdCliente FROM Clientes where Cedula=" & lblCedulaCliente.Content ' crear una consulta
            Dim dbAdapterFactura As New OleDbDataAdapter(New OleDbCommand(strQueryFactura, dbConexion)) ' instanciado con la consulta y la coneccion
            Dim facturfCmdBuilder = New OleDbCommandBuilder(dbAdapterFactura)
            Dim dsFactura As New DataSet("Factura") 'nombre que yo quiera
            dbAdapterFactura.Fill(dsFactura, "Factura")

            'For Each u As DataRow In dsTipoPago.Tables("TipoPago").Rows
            '    idCliente = u(0)
            'Next
            'IdCliente

            Dim strQueryCliente As String = "SELECT * FROM Clientes;"
            Dim dbAdapterCliente As New OleDbDataAdapter(strQueryCliente, dbConexion)
            Dim clienteCmdBuilder = New OleDbCommandBuilder(dbAdapterCliente)
            Dim dsCliente As New DataSet("Clientes") 'nombre que yo quiera
            dbAdapterCliente.Fill(dsCliente, "Clientes")

            Dim cli As Int32
            For Each clie As DataRow In dsCliente.Tables("Clientes").Rows
                If clie("NombreCliente") = Me.lblNombreCliente.Content Then
                    cli = clie("IdCliente")
                    Exit For
                End If
            Next

            'IdUsuario
            Dim strQueryUsuario As String = "SELECT * FROM InformacionUsuario;"
            Dim dbAdapterUsuario As New OleDbDataAdapter(strQueryUsuario, dbConexion)
            Dim usuarioCmdBuilder = New OleDbCommandBuilder(dbAdapterUsuario)
            Dim dsUsuario As New DataSet("Usuarios") 'nombre que yo quiera
            dbAdapterUsuario.Fill(dsUsuario, "Usuarios")

            'Dim usua As Int32
            For Each usu As DataRow In dsUsuario.Tables("Usuarios").Rows
                If usu("UsuarioName") = Me.lblNombreVendedor.Content Then
                    'usua = usu("IdUsuario")
                    Exit For
                End If
            Next

            If dsFactura Is Nothing Then
                Exit Sub
            Else
                dsFactura.Tables(0).Rows.Add(lblNumFactura2.Content, Me.txtFecha.Text, Me.cmbTipoPago.SelectedItem, Me.cmbProvincia.SelectedItem, nombreUsuario, Me.lblNombreCliente.Content, Me.lblCedulaCliente.Content, Me.lblDireccionCliente.Content, Me.lblTelefonoCliente.Content, Me.lblSubtotal2.Content, Me.lblIVA2.Content, Me.lblTotalPagar2.Content, lblDevolucion2.Content)
                'dsFactura.Tables(0).Rows.Add(lblNumFactura2.Content, cli, usua, Me.txtFecha.Text, Me.cmbProvincia.SelectedIndex + 1, Me.cmbTipoPago.SelectedIndex + 1, lblSubtotal2.Content, lblTotalPagar2.Content, lblDevolucion2.Content)
                'MsgBox("hola " & lblNumFactura2.Content & " " & cli & " " & usua & " " & Me.txtFecha.Text & " " & Me.cmbProvincia.SelectedIndex + 1 & " " & Me.cmbTipoPago.SelectedIndex + 1 & " " & lblSubtotal2.Content & " " & lblTotalPagar2.Content & " " & lblDevolucion2.Content)
                MsgBox("Se estan guardando los datos...", vbOKOnly, "Esperando")
                Try
                    dbAdapterFactura.Update(dsFactura.Tables("Factura"))
                Catch es As Exception
                    MsgBox("Error al actualizar...", vbOKOnly, "Error")
                End Try
                Me.Close()
            End If


            Dim strQueryDetalleFactura As String = "SELECT * FROM DetalleFactura"
            Dim dbAdapterDetalleFactura As New OleDbDataAdapter(strQueryDetalleFactura, dbConexion) ' instanciado con la consulta y la coneccion
            Dim detalleCmdBuilder = New OleDbCommandBuilder(dbAdapterDetalleFactura)
            Dim dsDeta As New DataSet("DetalleFactura")
            dbAdapterDetalleFactura.Fill(dsDeta, "DetalleFactura") ' empleado renombra a tbl_master. Puede incluso llamarse tbl_master y no Empleado

            Dim winCategoriasVendedor As New Categorias_Vendedor

            Dim InfPrecioLibro As Double
            Dim precioTotal As Double
            Dim cantidadProductos As Int32
            Dim numFactura As Int32
            Dim idLibro As Int32
            Dim InfNombreLibro As String


            Dim strQueryLibro As String = "SELECT * FROM Libros;"
            Dim dbAdapterLibro As New OleDbDataAdapter(strQueryLibro, dbConexion)
            Dim libroCmdBuilder = New OleDbCommandBuilder(dbAdapterLibro)
            Dim dsLibro As New DataSet("Libros") 'nombre que yo quiera
            Dim isbn As String
            dbAdapterLibro.Fill(dsLibro, "Libros")

            numFactura = lblNumFactura2.Content

            Dim datoNumFactura As Int32 = dsDeta.Tables(0).Rows.Count() + 1
            For number As Int32 = 0 To dsDetalle.Tables(0).Rows.Count - 1 Step 1
                precioTotal = dsDetalle.Tables(0).Rows(number).Item(4)
                cantidadProductos = dsDetalle.Tables(0).Rows(number).Item(1)
                isbn = CStr(dsDetalle.Tables(0).Rows(number).Item(0))
                For Each u As DataRow In dsLibro.Tables("Libros").Rows
                    If isbn = u(1) Then
                        idLibro = u(0)
                        InfNombreLibro = u(2)
                        InfPrecioLibro = u(6)
                    End If
                Next
                'MsgBox("Id: " & idLibro & " ISBN: " & isbn)
                'MsgBox("Mostrar " & " " & datoNumFactura & " " & cantidadProductos & "   " & InfNombreLibro & "   " & numFactura & "   " & precioTotal)
                If dsDeta Is Nothing Then
                    Exit Sub
                Else
                    dsDeta.Tables(0).Rows.Add(datoNumFactura, isbn, cantidadProductos, InfNombreLibro, InfPrecioLibro, precioTotal, numFactura)
                    'dsDeta.Tables(0).Rows.Add(cont, cantidadProductos, idLibro, numFactura, precioTotal)
                    Try
                        dbAdapterDetalleFactura.Update(dsDeta.Tables("DetalleFactura"))
                        MsgBox("Se ha guardado la factura correctamente...", vbOKOnly, "Confirmación")
                        datoNumFactura = +1
                    Catch es As Exception
                        MsgBox("Error al actualizar...", vbOKOnly, "Error")
                    End Try
                    Me.Close()
                End If

            Next



            Me.UpdateDataGrid()
        End Using
        Me.Hide()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelar.Click
        Dim winVend As New PrincipalVendedor
        winVend = Me.Owner
        winVend.administrador = Me.administrador
        winVend.nombreUsuario = Me.nombreUsuario
        winVend.idUsuario = Me.idUsuario
        winVend.Show()
        Me.Hide()

    End Sub

    Private Sub frmVendedorFactura_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles frmVendedorFactura.Closing
        End
    End Sub
End Class
''IdProvincia
'Dim strQueryProvincia As String = "SELECT * FROM Provincia WHERE IdProvincia = " & (CInt(cmbProvincia.SelectedIndex) + 1)
'Dim dbAdapterProvincia As New OleDbDataAdapter(strQueryProvincia, dbConexion)
'Dim provinciaCmdBuilder = New OleDbCommandBuilder(dbAdapterProvincia)
'Dim dsProvincia As New DataSet("Provincia") 'nombre que yo quiera
'dbAdapterProvincia.Fill(dsProvincia, "Provincia")

''IdTipoPago
'Dim strQueryTipoPago As String = "SELECT * FROM TipoPago WHERE IdTipoPago = " & (CInt(cmbTipoPago.SelectedIndex) + 1)
'Dim dbAdapterTipoPago As New OleDbDataAdapter(strQueryTipoPago, dbConexion)
'Dim tipoPagoCmdBuilder = New OleDbCommandBuilder(dbAdapterTipoPago)
'Dim dsTipoPago As New DataSet("TipoPago") 'nombre que yo quiera
'dbAdapterTipoPago.Fill(dsTipoPago, "TipoPago")

