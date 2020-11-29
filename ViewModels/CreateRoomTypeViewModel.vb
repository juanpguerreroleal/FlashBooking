Imports FlashBooking.FlashBooking

Public Class CreateRoomTypeViewModel
    Inherits BaseViewModel
    Private _context As MainViewModel
    Private _dataService As BussinessService
    Private _roomTypeItem As RoomTypeModel
    Public Property RoomTypeItem() As RoomTypeModel
        Get
            Return _roomTypeItem
        End Get
        Set(ByVal value As RoomTypeModel)
            _roomTypeItem = value
            OnPropertyChanged(NameOf(RoomTypeItem))
        End Set
    End Property
    Public Sub New(context As MainViewModel)
        _context = context
        _dataService = New BussinessService()
        CancelCommand = New DelegateCommand(AddressOf Cancel, AddressOf CanCancel)
        CreateCommand = New DelegateCommand(AddressOf Create, AddressOf CanCreate)
        RoomTypeItem = New RoomTypeModel()
    End Sub
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

    Public Sub Create()
        If (Not String.IsNullOrEmpty(RoomTypeItem.Name) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.Description) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.TotalCapacity) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.ChildrenCapacity) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.AdultsCapacity)) Then
            Dim roomType = New RoomType()
            roomType.Name = RoomTypeItem.Name
            roomType.Description = RoomTypeItem.Description
            roomType.AdultsCapacity = RoomTypeItem.AdultsCapacity
            roomType.ChildrenCapacity = RoomTypeItem.ChildrenCapacity
            roomType.TotalCapacity = RoomTypeItem.TotalCapacity
            Dim response = _dataService.CreateRoomType(roomType)
            If (response.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Home)
            ElseIf (Not response.HasSucceeded) Then
                MessageBox.Show("Ocurrió un error conectadose a la base de datos, revise su conexión a internet.",
                              "Message",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error)
            End If
        ElseIf (String.IsNullOrEmpty(RoomTypeItem.Name)) Then
            MessageBox.Show("Introduce un nombre para el tipo de habitación.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomTypeItem.Description)) Then
            MessageBox.Show("Introduce una descripción para el tipo de habitación.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomTypeItem.TotalCapacity)) Then
            MessageBox.Show("Introduce una capacidad total de inquilinos.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomTypeItem.ChildrenCapacity)) Then
            MessageBox.Show("Introduce una capacidad total de inquilinos niños.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(RoomTypeItem.AdultsCapacity)) Then
            MessageBox.Show("Introduce una capacidad total de inquilinos adultos.",
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
