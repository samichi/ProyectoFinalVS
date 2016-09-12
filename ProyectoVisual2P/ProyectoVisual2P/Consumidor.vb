Public Class Consumidor
    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nombreCliente As String
    Public Property NombreCliente() As String
        Get
            Return _nombreCliente
        End Get
        Set(ByVal value As String)
            _nombreCliente = value
        End Set
    End Property

    Private _cedula As String
    Public Property Cedula() As String
        Get
            Return _cedula
        End Get
        Set(ByVal value As String)
            _cedula = value
        End Set
    End Property

    Private _drireccion As String
    Public Property Direccion() As String
        Get
            Return _drireccion
        End Get
        Set(ByVal value As String)
            _drireccion = value
        End Set
    End Property

    Private _telefono As String
    Public Property Telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property

    Private _correoElectronico As String
    Public Property CorreoElectronico() As String
        Get
            Return _correoElectronico
        End Get
        Set(ByVal value As String)
            _correoElectronico = value
        End Set
    End Property

    'Public Sub New(v1 As String, v2 As String, v3 As String, v4 As String, v5 As String)
    '    Me.NombreCliente = v1
    '    Me.Cedula = v2
    '    Me.Direccion = v3
    '    Me.Telefono = v4
    '    Me.CorreoElectronico = v5
    'End Sub

    Sub New(p1 As Object, p2 As Object, p3 As Object, p4 As Object, p5 As Object)
        ' TODO: Complete member initialization 
        Me.NombreCliente = p1
        Me.Cedula = p2
        Me.Direccion = p3
        Me.Telefono = p4
        Me.CorreoElectronico = p5
    End Sub
    Sub New(p0 As Object, p1 As Object, p2 As Object, p3 As Object, p4 As Object, p5 As Object)
        ' TODO: Complete member initialization 
        Me.NombreCliente = p1
        Me.Cedula = p2
        Me.Direccion = p3
        Me.Telefono = p4
        Me.CorreoElectronico = p5
    End Sub

End Class
