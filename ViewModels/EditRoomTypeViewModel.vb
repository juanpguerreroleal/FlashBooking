Imports FlashBooking.FlashBooking

Public Class EditRoomTypeViewModel
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
    Private _roomType As RoomType
    Public Sub New(context As MainViewModel, id As Integer)
        _context = context
        _dataService = New BussinessService
        If (Not id.Equals(0)) Then
            _roomType = _dataService.GetRoomTypeById(id)
            If (_roomType Is Nothing) Then
                _context.ChangeView(GeneralEnums.Views.Home)
            ElseIf (_roomType IsNot Nothing) Then
                UpdateView()
                CancelCommand = New DelegateCommand(AddressOf Cancel, AddressOf CanCancel)
                EditCommand = New DelegateCommand(AddressOf Edit, AddressOf CanEdit)
            End If
        Else
            _context.ChangeView(GeneralEnums.Views.Home)
        End If

    End Sub
#Region "Methods"
    Public Sub UpdateView()
        RoomTypeItem = New RoomTypeModel()
        RoomTypeItem.Name = _roomType.Name
        RoomTypeItem.Description = _roomType.Description
        RoomTypeItem.AdultsCapacity = _roomType.AdultsCapacity
        RoomTypeItem.ChildrenCapacity = _roomType.ChildrenCapacity
        RoomTypeItem.TotalCapacity = _roomType.TotalCapacity
    End Sub
    Public Sub Cancel()
        _context.ChangeView(GeneralEnums.Views.Home)
    End Sub
    Public Function CanCancel() As Boolean
        Return True
    End Function

    Public Sub Edit()
        If (Not String.IsNullOrEmpty(RoomTypeItem.Name) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.Description) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.TotalCapacity) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.ChildrenCapacity) AndAlso Not String.IsNullOrEmpty(RoomTypeItem.AdultsCapacity)) Then
            _roomType.Name = RoomTypeItem.Name
            _roomType.Description = RoomTypeItem.Description
            _roomType.AdultsCapacity = RoomTypeItem.AdultsCapacity
            _roomType.ChildrenCapacity = RoomTypeItem.ChildrenCapacity
            _roomType.TotalCapacity = RoomTypeItem.TotalCapacity
            Dim response = _dataService.EditRoomType(_roomType)
            If (response.HasSucceeded) Then
                MessageBox.Show("Se actualizó correctamente.",
                              "Guardado",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information)
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
    Public Function CanEdit() As Boolean
        Return True
    End Function
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
    Private _editCommand As ICommand
    Public Property EditCommand() As ICommand
        Get
            Return _editCommand
        End Get
        Set(ByVal value As ICommand)
            _editCommand = value
            OnPropertyChanged(NameOf(EditCommand))
        End Set
    End Property
#End Region
End Class
