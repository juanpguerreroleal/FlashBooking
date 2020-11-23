Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class GuestOptionsViewModel
    Inherits BaseViewModel
    Private _dataService As BussinessService
    Private _guestList As ObservableCollection(Of GuestModel)
    Public Property GuestList() As ObservableCollection(Of GuestModel)
        Get
            Return _guestList
        End Get
        Set(ByVal value As ObservableCollection(Of GuestModel))
            _guestList = value
            OnPropertyChanged(NameOf(GuestList))
        End Set
    End Property
    Public Sub New()
        _dataService = New BussinessService()
        GuestList = New ObservableCollection(Of GuestModel)
        LoadData()
        _refreshCommand = New DelegateCommand(AddressOf RefreshData, AddressOf CanRefresh)
    End Sub
    Public Sub LoadData()
        Dim guestListResponse = _dataService.GetGuestList()
        If (guestListResponse.HasSucceeded) Then
            Dim guests = New ObservableCollection(Of GuestModel)
            For Each item As Guest In guestListResponse.GuestList
                Dim guest = New GuestModel
                guest.Name = item.Name
                guest.LastName = item.LastName
                guest.City = item.City
                guest.PhoneNumber = item.PhoneNumber
                guest.BookingId = item.Booking.Id
                guests.Add(guest)
            Next
            Me.GuestList = guests
        End If
    End Sub
    Public Sub RefreshData()
        Dim guestListResponse = _dataService.GetGuestList()
        If (guestListResponse.HasSucceeded) Then
            Dim guests = New ObservableCollection(Of GuestModel)
            For Each item As Guest In guestListResponse.GuestList
                Dim guest = New GuestModel
                guest.Name = item.Name
                guest.LastName = item.LastName
                guest.City = item.City
                guest.PhoneNumber = item.PhoneNumber
                guest.BookingId = item.Booking.Id
                guests.Add(guest)
            Next
            Me.GuestList = guests
        End If
    End Sub
    Private Function CanRefresh(ByVal param As Object) As Boolean
        Return True
    End Function
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
#End Region
End Class
