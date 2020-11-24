Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class BookingOptionsViewModel
    Inherits BaseViewModel
#Region "Properties"
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _bookingList As ObservableCollection(Of BookingModel)
    Public Property BookingList() As ObservableCollection(Of BookingModel)
        Get
            Return _bookingList
        End Get
        Set(ByVal value As ObservableCollection(Of BookingModel))
            _bookingList = value
            OnPropertyChanged(NameOf(BookingList))
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub RefreshData()
        Dim bookingListResponse = _dataService.GetBookingList()
        If (bookingListResponse.HasSucceeded) Then
            Dim bookings = New ObservableCollection(Of BookingModel)
            For Each item As Booking In bookingListResponse.BookingList
                Dim booking = New BookingModel
                booking.CreationDate = item.CreationDate
                booking.CheckIn = item.CheckInDateTime
                booking.CheckOut = item.CheckOutDateTime
                booking.Id = item.Id
                Dim guest = New GuestModel
                guest.Name = item.Guest.FirstOrDefault().Name
                guest.LastName = item.Guest.FirstOrDefault().LastName
                guest.City = item.Guest.FirstOrDefault().City
                guest.PhoneNumber = item.Guest.FirstOrDefault().PhoneNumber
                guest.BookingId = item.Guest.FirstOrDefault().Booking.Id
                booking.Guest = guest
                bookings.Add(booking)
            Next
            Me.BookingList = bookings
        End If
    End Sub
    Private Function CanRefresh(ByVal param As Object) As Boolean
        Return True
    End Function
    Public Sub CreateBooking()
        _context.ChangeView(GeneralEnums.Views.CreateBookings)
    End Sub
    Private Function CanCreateBooking(ByVal param As Object) As Boolean
        Return True
    End Function
#End Region
#Region "Constructor"

    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        _bookingList = New ObservableCollection(Of BookingModel)
        _refreshCommand = New DelegateCommand(AddressOf RefreshData, AddressOf CanRefresh)
        _createCommand = New DelegateCommand(AddressOf CreateBooking, AddressOf CanCreateBooking)
        RefreshData()
    End Sub
#End Region
#Region "Commands"
    Private _refreshCommand As ICommand
    Public Property RefreshCommand() As ICommand
        Get
            Return _refreshCommand
        End Get
        Set(ByVal value As ICommand)
            _refreshCommand = value
            OnPropertyChanged(NameOf(RefreshCommand))
        End Set
    End Property
    Private _createCommand As ICommand
    Public Property CreateCommand() As ICommand
        Get
            Return _createCommand
        End Get
        Set(ByVal value As ICommand)
            _createCommand = value
            OnPropertyChanged(NameOf(CreateCommand))
        End Set
    End Property
#End Region
End Class
