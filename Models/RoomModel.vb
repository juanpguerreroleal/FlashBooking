Public Class RoomModel
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
    Private _adultsCapacity As String
    Public Property AdultsCapacity() As String
        Get
            Return _adultsCapacity
        End Get
        Set(ByVal value As String)
            _adultsCapacity = value
            OnPropertyChanged(NameOf(AdultsCapacity))
        End Set
    End Property
    Private _childrensCapacity As String
    Public Property ChildrensCapacity() As String
        Get
            Return _childrensCapacity
        End Get
        Set(ByVal value As String)
            _childrensCapacity = value
            OnPropertyChanged(NameOf(ChildrensCapacity))
        End Set
    End Property
    Private _totalCapacity As String
    Public Property TotalCapacity() As String
        Get
            Return _totalCapacity
        End Get
        Set(ByVal value As String)
            _totalCapacity = value
            OnPropertyChanged(NameOf(TotalCapacity))
        End Set
    End Property
End Class
