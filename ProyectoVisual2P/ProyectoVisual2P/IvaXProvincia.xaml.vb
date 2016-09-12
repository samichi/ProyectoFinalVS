Imports System.Data.OleDb
Imports System.Data

Public Class IvaXProvincia
    Public idIva As Integer
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath

    
End Class
