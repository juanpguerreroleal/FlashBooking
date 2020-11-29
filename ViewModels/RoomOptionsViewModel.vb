
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
    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        RoomsList = New ObservableCollection(Of RoomTypeModel)
        RoomList = New ObservableCollection(Of RoomModel)
        LoadData()
        RefreshRooms()
        _refreshRoomTypesCommand = New DelegateCommand(AddressOf RefreshRoomTypes, AddressOf CanRefresh)
        _createRoomTypeCommand = New DelegateCommand(AddressOf CreateRoomType, AddressOf CanCreateRoomType)
        _refreshRoomsCommand = New DelegateCommand(AddressOf RefreshRooms, AddressOf CanRefreshRooms)
        _createRoomCommand = New DelegateCommand(AddressOf CreateRoom, AddressOf CanCreateRoom)
        _editRoomTypeCommand = New DelegateCommand(AddressOf EditRoomType, AddressOf CanEditRoomType)
    End Sub
    Public Sub LoadData()
        Dim roomsListResponse = _dataService.GetRoomTypeList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomTypeModel)
            For Each item As RoomType In roomsListResponse.RoomTypeList
                Dim room = New RoomTypeModel
                room.Id = item.Id
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
                room.Id = item.Id
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
    Private Function CanRefreshRooms(ByVal param As Object) As Boolean
        Return True
    End Function
    Public Sub RefreshRooms()
        Dim roomsListResponse = _dataService.GetRoomList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomModel)
            For Each item As Room In roomsListResponse.RoomsList
                Dim room = New RoomModel
                room.Description = item.Description
                room.Cost = item.Cost
                room.Number = item.Number
                room.Id = item.Id
                room.RoomTypeName = item.RoomType.Name
                rooms.Add(room)
            Next
            Me.RoomList = rooms
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

    Public Sub CreateRoom()
        _context.ChangeView(GeneralEnums.Views.CreateRoom)
    End Sub
    Private Function CanCreateRoom(ByVal param As Object) As Boolean
        Return True
    End Function

    Public Sub EditRoomType(id As Integer)
        _context.ChangeView(GeneralEnums.Views.EditRoomType, id)
    End Sub
    Private Function CanEditRoomType(ByVal param As Object) As Boolean
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
    Private _refreshRoomsCommand As ICommand
    Public Property RefreshRoomsCommand() As ICommand
        Get
            Return _refreshRoomsCommand
        End Get
        Set(ByVal value As ICommand)
            _refreshRoomsCommand = value
            OnPropertyChanged(NameOf(RefreshRoomsCommand))
        End Set
    End Property
    Private _createRoomCommand As ICommand
    Public Property CreateRoomCommand() As ICommand
        Get
            Return _createRoomCommand
        End Get
        Set(ByVal value As ICommand)
            _createRoomCommand = value
            OnPropertyChanged(NameOf(CreateRoomCommand))
        End Set
    End Property
    Private _editRoomTypeCommand As ICommand
    Public Property EditRoomTypeCommand() As ICommand
        Get
            Return _editRoomTypeCommand
        End Get
        Set(ByVal value As ICommand)
            _editRoomTypeCommand = value
            OnPropertyChanged(NameOf(EditRoomTypeCommand))
        End Set
    End Property
#End Region
End Class
