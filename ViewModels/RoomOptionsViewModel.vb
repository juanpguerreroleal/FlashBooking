
Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class RoomOptionsViewModel
    Inherits BaseViewModel
    Private _dataService As BussinessService
    Private _roomsList As ObservableCollection(Of RoomModel)
    Public Property RoomsList() As ObservableCollection(Of RoomModel)
        Get
            Return _roomsList
        End Get
        Set(ByVal value As ObservableCollection(Of RoomModel))
            _roomsList = value
            OnPropertyChanged(NameOf(RoomsList))
        End Set
    End Property
    Public Sub New()
        _dataService = New BussinessService()
        RoomsList = New ObservableCollection(Of RoomModel)
        LoadData()
        _refreshCommand = New DelegateCommand(AddressOf RefreshData, AddressOf CanRefresh)
    End Sub
    Public Sub LoadData()
        Dim roomsListResponse = _dataService.GetRoomsList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomModel)
            For Each item As RoomType In roomsListResponse.RoomsList
                Dim room = New RoomModel
                room.Name = item.Name
                room.Description = item.Description
                room.AdultsCapacity = item.AdultsCapacity
                room.ChildrensCapacity = item.ChildrenCapacity
                room.TotalCapacity = item.TotalCapacity
                rooms.Add(room)
            Next
            Me.RoomsList = rooms
        End If
    End Sub
    Public Sub RefreshData()
        Dim roomsListResponse = _dataService.GetRoomsList()
        If (roomsListResponse.HasSucceeded) Then
            Dim rooms = New ObservableCollection(Of RoomModel)
            For Each item As RoomType In roomsListResponse.RoomsList
                Dim room = New RoomModel
                room.Name = item.Name
                room.Description = item.Description
                room.AdultsCapacity = item.AdultsCapacity
                room.ChildrensCapacity = item.ChildrenCapacity
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
