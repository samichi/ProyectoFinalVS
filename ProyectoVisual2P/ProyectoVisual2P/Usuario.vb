Public Class Usuario
    Public loggedIn As Boolean
    Private _usuario As String
    Public Property Usuario() As String
        Get
            Return _usuario
        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property

    Private _password As String
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _flagAdministrador As String
    Public Property Administrador() As String
        Get
            Return _flagAdministrador
        End Get
        Set(ByVal value As String)
            _flagAdministrador = value
        End Set
    End Property

    Sub New(p1 As Object, p2 As Object, p3 As Object, p4 As Object)
        ' TODO: Complete member initialization 
        Me._usuario = p1
        Me._password = p2
        Me._nombre = p3
        Me._flagAdministrador = p4
    End Sub

    Public Function Login(usuarios As ArrayList)
        For Each u As Usuario In usuarios
            If Me.Usuario = u.Usuario And Me.Password = u.Password Then
                Me.Nombre = u.Nombre
                Me.Administrador = u.Administrador
                Me.loggedIn = True

                Exit For
            End If
        Next
        Return loggedIn
    End Function
    Public Function FlagAdmin()
        Return Me._flagAdministrador
    End Function

    Public Function Logout()
        Me.Nombre = ""
        Me.Usuario = ""
        Me.Password = ""
        Me.Administrador = False
        Me.loggedIn = False
        Return True
    End Function

    Sub New(p1 As String, p2 As String)
        ' TODO: Complete member initialization 
        Me._usuario = p1
        Me._password = p2
    End Sub

End Class
