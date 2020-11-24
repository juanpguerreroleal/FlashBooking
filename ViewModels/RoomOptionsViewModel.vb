
Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class RoomOptionsViewModel
    Inherits BaseViewModel
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _roomsList As ObservableCollection(Of RoomTypeModel)
    Public Property RoomsList() As ObservableCollection(Of RoomTypeModel)
        Get
            Return _roomsList
        End Get
        Set(ByVal value As ObservableCollection(Of RoomTypeModel))
            _roomsList = value
            OnPropertyChanged(NameOf(RoomsList))
        End Set
    End Property
    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        RoomsList = New ObservableCollection(Of RoomTypeModel)
        LoadData()
        _refreshRoomTypesCommand = New DelegateCommand(AddressOf RefreshRoomTypes, AddressOf CanRefresh)
        _createRoomTypeCommand = New DelegateCommand(AddressOf CreateRoomType, AddressOf CanCreateRoomType)
    End Sub
    Public Sub LoadData()
        Dim roomsListResponse = _dataService.GetRoomsList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomTypeModel)
            For Each item As RoomType In roomsListResponse.RoomsList
                Dim room = New RoomTypeModel
                room.Name = item.Name
                room.Description = item.Description
                room.AdultsCapacity = item.AdultsCapacity
                room.ChildrenCapacity = item.ChildrenCapacity
                room.TotalCapacity = item.TotalCapacity
                rooms.Add(room)
            Next
            Me.RoomsList = rooms
        End If
    End Sub
    Public Sub RefreshRoomTypes()
        Dim roomsListResponse = _dataService.GetRoomTypeList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomTypeModel)
            For Each item As RoomType In roomsListResponse.RoomTypeList
                Dim room = New RoomTypeModel
                room.Name = item.Name
                room.Description = item.Description
                room.AdultsCapacity = item.AdultsCapacity
                room.ChildrenCapacity = item.ChildrenCapacity
                room.TotalCapacity = item.TotalCapacity
                rooms.Add(room)
            Next
            Me.RoomsList = rooms
        End If
    End Sub
    Private Function CanRefresh(ByVal param As Object) As Boolean
        Return True
    End Function
    Public Sub CreateRoomType()
        _context.ChangeView(GeneralEnums.Views.CreateRoomType)
    End Sub
    Private Function CanCreateRoomType(ByVal param As Object) As Boolean
        Return True
    End Function
#Region "Commands"
    Private _refreshRoomTypesCommand As ICommand
    Public Property RefreshRoomTypesCommand() As ICommand
        Get
            Return _refreshRoomTypesCommand
        End Get
        Set(ByVal value As ICommand)
            _refreshRoomTypesCommand = value
            OnPropertyChanged(NameOf(RefreshRoomTypesCommand))
        End Set
    End Property
    Private _createRoomTypeCommand As ICommand
    Public Property CreateRoomTypeCommand() As ICommand
        Get
            Return _createRoomTypeCommand
        End Get
        Set(ByVal value As ICommand)
            _createRoomTypeCommand = value
            OnPropertyChanged(NameOf(CreateRoomTypeCommand))
        End Set
    End Property
#End Region
End Class
