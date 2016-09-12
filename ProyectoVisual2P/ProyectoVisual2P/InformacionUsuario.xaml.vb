Imports System.Data
Imports System.Data.OleDb

Public Class InformacionUsuario
    Public nuevoUsuario As Boolean
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    
End Class
