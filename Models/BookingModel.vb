Public Class BookingModel
    Inherits BaseViewModel
    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
            OnPropertyChanged(NameOf(Id))
        End Set
    End Property
    Private _creationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return _creationDate
        End Get
        Set(ByVal value As DateTime)
            _creationDate = value
            OnPropertyChanged(NameOf(CreationDate))
        End Set
    End Property
    Private _checkIn As DateTime
    Public Property CheckIn() As DateTime
        Get
            Return _checkIn
        End Get
        Set(ByVal value As DateTime)
            _checkIn = value
            OnPropertyChanged(NameOf(CheckIn))
        End Set
    End Property
    Private _checkOut As DateTime
    Public Property CheckOut() As DateTime
        Get
            Return _checkOut
        End Get
        Set(ByVal value As DateTime)
            _checkOut = value
            OnPropertyChanged(NameOf(CheckOut))
        End Set
    End Property
    Private _guest As GuestModel
    Public Property Guest() As GuestModel
        Get
            Return _guest
        End Get
        Set(ByVal value As GuestModel)
            _guest = value
            OnPropertyChanged(NameOf(Guest))
        End Set
    End Property
    Public Sub New()
        CheckIn = DateTime.Now
        CheckOut = DateTime.Now.AddDays(1)
    End Sub
End Class
