Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class CreateBookingViewModel
    Inherits BaseViewModel
#Region "Properties"
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _roomTypeList As ObservableCollection(Of RoomTypeModel)
    Private _bookingItem As BookingModel
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
            LoadRooms()
            OnPropertyChanged(NameOf(SelectedRoomType))
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
#End Region
#Region "Constructor"
    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        CancelCommand = New DelegateCommand(AddressOf Cancel, AddressOf CanCancel)
        RoomTypeList = New ObservableCollection(Of RoomTypeModel)
        RoomList = New ObservableCollection(Of RoomModel)
        BookingItem = New BookingModel()
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
    Public Sub LoadRooms()
        'GetRooms
    End Sub
#End Region
End Class
