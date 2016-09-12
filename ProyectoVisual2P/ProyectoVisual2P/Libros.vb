Public Class Libros
    Private _isbn As String
    Public Property ISBN() As String
        Get
            Return _isbn
        End Get
        Set(ByVal value As String)
            _isbn = value
        End Set
    End Property

    Private _titulo As String
    Public Property Titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Private _autor As String
    Public Property Autor() As String
        Get
            Return _autor
        End Get
        Set(ByVal value As String)
            _autor = value
        End Set
    End Property

    Private _editorial As String
    Public Property Editorial() As String
        Get
            Return _editorial
        End Get
        Set(ByVal value As String)
            _editorial = value
        End Set
    End Property

    Private _genero As String
    Public Property Genero() As String
        Get
            Return _genero
        End Get
        Set(ByVal value As String)
            _genero = value
        End Set
    End Property

    Private _precio As Double
    Public Property Precio() As Double
        Get
            Return _precio
        End Get
        Set(ByVal value As Double)
            _precio = value
        End Set
    End Property

    Private _idCategoria As Integer
    Public Property IdCategoria() As Integer
        Get
            Return _idCategoria
        End Get
        Set(ByVal value As Integer)
            _idCategoria = value
        End Set
    End Property

    Public Sub New(v1 As Object, v2 As Object, v3 As Object, v4 As Object, v5 As Object, v6 As Object, v7 As Object)
        Me._isbn = v1
        Me._titulo = v2
        Me._autor = v3
        Me._editorial = v4
        Me._genero = v5
        Me._precio = v6
        Me.IdCategoria = v7
    End Sub
End Class
