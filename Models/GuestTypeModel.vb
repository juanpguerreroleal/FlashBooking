Public Class GuestTypeModel
    Inherits BaseViewModel
    Private _id As String
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
            OnPropertyChanged(NameOf(Id))
        End Set
    End Property
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
            OnPropertyChanged(NameOf(Name))
        End Set
    End Property
    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
            OnPropertyChanged(NameOf(Description))
        End Set
    End Property
    Private _cost As Decimal
    Public Property Cost() As Decimal
        Get
            Return _cost
        End Get
        Set(ByVal value As Decimal)
            _cost = value
            OnPropertyChanged(NameOf(Cost))
        End Set
    End Property
End Class
