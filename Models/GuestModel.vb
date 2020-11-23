Public Class GuestModel
    Inherits BaseViewModel
    Private Id As Integer
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
    Private _lastname As String
    Public Property LastName() As String
        Get
            Return _lastname
        End Get
        Set(ByVal value As String)
            _lastname = value
            OnPropertyChanged(NameOf(LastName))
        End Set
    End Property
    Private _city As String
    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
            OnPropertyChanged(NameOf(City))
        End Set
    End Property
    Private _phoneNumber As String
    Public Property PhoneNumber() As String
        Get
            Return _phoneNumber
        End Get
        Set(ByVal value As String)
            _phoneNumber = value
            OnPropertyChanged(NameOf(PhoneNumber))
        End Set
    End Property
    Private _bookingId As Integer
    Public Property BookingId() As Integer
        Get
            Return _bookingId
        End Get
        Set(ByVal value As Integer)
            _bookingId = value
            OnPropertyChanged(NameOf(BookingId))
        End Set
    End Property
End Class
