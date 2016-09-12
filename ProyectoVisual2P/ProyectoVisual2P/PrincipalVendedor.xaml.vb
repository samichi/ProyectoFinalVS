Imports System.Data.OleDb
Imports System.Data

Public Class PrincipalVendedor
    Public nombreUsuario As String
    Public administrador As String
    Public idUsuario As Integer
    Public idFactura As Integer
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

   
End Class

