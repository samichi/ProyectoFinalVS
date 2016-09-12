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

  
End Class

