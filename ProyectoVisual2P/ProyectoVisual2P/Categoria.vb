Public Class Categoria
    Private _nombreCategoria As String
    Private _idCategoria As Integer
    Public Property IdCategoria() As Integer
        Get
            Return _idCategoria
        End Get
        Set(ByVal value As Integer)
            _idCategoria = value
        End Set
    End Property

    Public Property NombreCategoria() As String
        Get
            Return _nombreCategoria
        End Get
        Set(ByVal value As String)
            _nombreCategoria = value
        End Set
    End Property

    Sub New(p1 As Object)
        ' TODO: Complete member initialization 
        _idCategoria = p1
    End Sub

    'Sub New(p1 As Object)
    '    ' TODO: Complete member initialization 
    '    _nombreCategoria = p1
    'End Sub

    Sub New(p0 As Object, p1 As Object)
        ' TODO: Complete member initialization 
        _idCategoria = p0
        _nombreCategoria = p1
    End Sub
End Class
