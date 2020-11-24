
Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class RoomOptionsViewModel
    Inherits BaseViewModel
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
    Public Sub New()
        _dataService = New BussinessService()
        RoomsList = New ObservableCollection(Of RoomTypeModel)
        LoadData()
        _refreshCommand = New DelegateCommand(AddressOf RefreshData, AddressOf CanRefresh)
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
    Public Sub RefreshData()
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
