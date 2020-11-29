Imports System.Collections.ObjectModel
Imports FlashBooking.FlashBooking

Public Class CreateRoomViewModel
    Inherits BaseViewModel
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _roomItem As RoomModel
    Public Property RoomItem() As RoomModel
        Get
            Return _roomItem
        End Get
        Set(ByVal value As RoomModel)
            _roomItem = value
            OnPropertyChanged(NameOf(RoomItem))
        End Set
    End Property
#Region "Properties"
    Private _selectedRoomType As RoomTypeModel
    Public Property SelectedRoomType() As RoomTypeModel
        Get
            Return _selectedRoomType
        End Get
        Set(ByVal value As RoomTypeModel)
            _selectedRoomType = value
            OnPropertyChanged(NameOf(SelectedRoomType))
        End Set
    End Property
    Private _roomTypeList As ObservableCollection(Of RoomTypeModel)
    Public Property RoomTypeList() As ObservableCollection(Of RoomTypeModel)
        Get
            Return _roomTypeList
        End Get
        Set(ByVal value As ObservableCollection(Of RoomTypeModel))
            _roomTypeList = value
            OnPropertyChanged(NameOf(RoomTypeList))
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
        RoomItem = New RoomModel()
        LoadRoomTypes()
    End Sub
#End Region
#Region "Commands"
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
    Public Sub Create()
        If (RoomItem IsNot Nothing AndAlso SelectedRoomType IsNot Nothing AndAlso Not String.IsNullOrEmpty(RoomItem.Number) AndAlso Not String.IsNullOrEmpty(RoomItem.Cost) AndAlso Not String.IsNullOrEmpty(RoomItem.Description) AndAlso Not String.IsNullOrEmpty(RoomItem.Cost)) Then
            Dim room = New Room()
            room.Number = RoomItem.Number
            room.Description = RoomItem.Description
            room.Cost = RoomItem.Cost
            room.RoomTypeId = SelectedRoomType.Id
            Dim roomListResponse = _dataService.CreateRoom(room)
            If (roomListResponse.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Home)
            End If
        ElseIf (SelectedRoomType Is Nothing) Then
            MessageBox.Show("Selecciona un tipo de habitación.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomItem.Number)) Then
            MessageBox.Show("Introduce un numero de habitación.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (RoomItem.Description <= 0) Then
            MessageBox.Show("Introduce una descripción.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomItem.Cost)) Then
            MessageBox.Show("Introduce un costo.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        End If
    End Sub
    Public Function CanCreate() As Boolean
        Return True
    End Function
#End Region
End Class
