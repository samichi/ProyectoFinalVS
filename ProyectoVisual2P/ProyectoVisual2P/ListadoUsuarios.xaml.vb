﻿Imports System.Data
Imports System.Data.OleDb

Public Class ListadoUsuarios
    Public idUsuario As Integer
    Public nombreUsuario As String
    Public administrador As Boolean
    Public bdPath As String = "..\..\..\LibriShop.mdb"
    Public strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & bdPath
    
End Class