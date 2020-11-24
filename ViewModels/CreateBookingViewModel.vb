Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class CreateBookingViewModel
    Inherits BaseViewModel
#Region "Properties"
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _roomTypeList As ObservableCollection(Of RoomTypeModel)
    Private _bookingItem As BookingModel
    Private _guestItem As GuestModel

    Public Property GuestItem() As GuestModel
        Get
            Return _guestItem
        End Get
        Set(ByVal value As GuestModel)
            _guestItem = value
            OnPropertyChanged(NameOf(GuestItem))
        End Set
    End Property
    Public Property BookingItem() As BookingModel
        Get
            Return _bookingItem
        End Get
        Set(ByVal value As BookingModel)
            _bookingItem = value
            OnPropertyChanged(NameOf(BookingItem))
        End Set
    End Property
    Public Property RoomTypeList() As ObservableCollection(Of RoomTypeModel)
        Get
            Return _roomTypeList
        End Get
        Set(ByVal value As ObservableCollection(Of RoomTypeModel))
            _roomTypeList = value
            OnPropertyChanged(NameOf(RoomTypeList))
        End Set
    End Property
    Private _selectedRoomType As RoomTypeModel
    Public Property SelectedRoomType() As RoomTypeModel
        Get
            Return _selectedRoomType
        End Get
        Set(ByVal value As RoomTypeModel)
            _selectedRoomType = value
            RoomList = New ObservableCollection(Of RoomModel)
            SelectedRoom = New RoomModel()
            LoadRooms()
            OnPropertyChanged(NameOf(SelectedRoomType))
        End Set
    End Property
    Private _selectedRoom As RoomModel
    Public Property SelectedRoom() As RoomModel
        Get
            Return _selectedRoom
        End Get
        Set(ByVal value As RoomModel)
            _selectedRoom = value
            OnPropertyChanged(NameOf(SelectedRoom))
        End Set
    End Property
    Private _selectedGuestType As GuestTypeModel
    Public Property SelectedGuestType() As GuestTypeModel
        Get
            Return _selectedGuestType
        End Get
        Set(ByVal value As GuestTypeModel)
            _selectedGuestType = value
            OnPropertyChanged(NameOf(SelectedGuestType))
        End Set
    End Property

    Private _roomList As ObservableCollection(Of RoomModel)
    Public Property RoomList() As ObservableCollection(Of RoomModel)
        Get
            Return _roomList
        End Get
        Set(ByVal value As ObservableCollection(Of RoomModel))
            _roomList = value
            OnPropertyChanged(NameOf(RoomList))
        End Set
    End Property
    Private _guestTypeList As ObservableCollection(Of GuestTypeModel)
    Public Property GuestTypeList() As ObservableCollection(Of GuestTypeModel)
        Get
            Return _guestTypeList
        End Get
        Set(ByVal value As ObservableCollection(Of GuestTypeModel))
            _guestTypeList = value
            OnPropertyChanged(NameOf(GuestTypeList))
        End Set
    End Property
#End Region
#Region "Constructor"
    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        CancelCommand = New DelegateCommand(AddressOf Cancel, AddressOf CanCancel)
        CreateCommand = New DelegateCommand(AddressOf Create, AddressOf CanCreate)
        RoomTypeList = New ObservableCollection(Of RoomTypeModel)
        RoomList = New ObservableCollection(Of RoomModel)
        BookingItem = New BookingModel()
        GuestItem = New GuestModel()
        LoadGuestTypes()
        LoadRoomTypes()
    End Sub
#End Region
#Region "Command"
    Private _cancelCommand As ICommand
    Public Property CancelCommand() As ICommand
        Get
            Return _cancelCommand
        End Get
        Set(ByVal value As ICommand)
            _cancelCommand = value
            OnPropertyChanged(NameOf(CancelCommand))
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
#Region "Methods"
    Public Sub Cancel()
        _context.ChangeView(GeneralEnums.Views.Home)
    End Sub
    Public Function CanCancel() As Boolean
        Return True
    End Function
    Public Sub LoadRoomTypes()
        Dim roomTypeListResponse = _dataService.GetRoomTypeList()
        If (roomTypeListResponse.HasSucceeded) Then
            Dim roomTypes = New ObservableCollection(Of RoomTypeModel)
            For Each item As RoomType In roomTypeListResponse.RoomTypeList
                Dim roomType = New RoomTypeModel
                roomType.Description = item.Description
                roomType.Name = item.Name
                roomType.Id = item.Id
                roomType.AdultsCapacity = item.AdultsCapacity
                roomType.ChildrenCapacity = item.ChildrenCapacity
                roomType.TotalCapacity = item.TotalCapacity
                roomTypes.Add(roomType)
            Next
            Me.RoomTypeList = roomTypes
        End If
    End Sub
    Public Sub LoadGuestTypes()
        Dim guestTypeListResponse = _dataService.GetGuestTypeList()
        If (guestTypeListResponse.HasSucceeded) Then
            Dim guestTypes = New ObservableCollection(Of GuestTypeModel)
            For Each item As GuestType In guestTypeListResponse.GuestTypeList
                Dim guestType = New GuestTypeModel
                guestType.Description = item.Description
                guestType.Name = item.Name
                guestType.Id = item.Id
                guestType.Cost = item.Cost
                guestTypes.Add(guestType)
            Next
            Me.GuestTypeList = guestTypes
        End If
    End Sub
    Public Sub LoadRooms()
        Dim roomListResponse = _dataService.GetAvailableRoomList(SelectedRoomType.Id, BookingItem.CheckIn, BookingItem.CheckOut)
        If (roomListResponse.HasSucceeded) Then
            Dim roomList = New ObservableCollection(Of RoomModel)
            For Each item As Room In roomListResponse.RoomList
                Dim room = New RoomModel
                room.Name = SelectedRoomType.Name + " (R" + item.Number.ToString() + ")"
                room.Id = item.Id
                room.Description = item.Description
                room.Cost = item.Cost
                room.Number = item.Number
                roomList.Add(room)
            Next
            Me.RoomList = roomList
        End If
    End Sub
    Public Sub Create()
        Dim roomItem = _dataService.GetRoomById(SelectedRoom.Id)
        If (roomItem IsNot Nothing) Then
            Dim guestModel = New Guest
            guestModel.Name = GuestItem.Name
            guestModel.LastName = GuestItem.LastName
            guestModel.PhoneNumber = GuestItem.PhoneNumber
            guestModel.City = GuestItem.City
            guestModel.Country = GuestItem.Country
            guestModel.BirthDate = GuestItem.BirthDate
            guestModel.GuestTypeId = SelectedGuestType.Id
            Dim booking = New Booking()
            booking.CreationDate = DateTime.Now
            booking.CheckInDateTime = BookingItem.CheckIn
            booking.CheckOutDateTime = BookingItem.CheckOut
            booking.IsConfirmed = False
            booking.Room = roomItem
            booking.Guest.Add(guestModel)
            Dim roomListResponse = _dataService.CreateBooking(booking)
            If (roomListResponse.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Home)
            End If
        End If
    End Sub
    Public Function CanCreate() As Boolean
        Return True
    End Function
#End Region
End Class
