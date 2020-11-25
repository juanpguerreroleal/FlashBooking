Public Class MainViewModel
    Inherits BaseViewModel
    Private _currentPageViewModel As BaseViewModel

    Private _title As String
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
            OnPropertyChanged(NameOf(Title))
        End Set
    End Property

    Public Property CurrentPageViewModel As BaseViewModel
        Get
            Return _currentPageViewModel
        End Get
        Set(value As BaseViewModel)
            _currentPageViewModel = value
            OnPropertyChanged(NameOf(CurrentPageViewModel))
        End Set
    End Property
    Public Sub New()
        CurrentPageViewModel = New LoginViewModel(Me)
    End Sub
    Public Sub ChangeView(viewId As Int32)
        Select Case viewId
            Case GeneralEnums.Views.Login
                CurrentPageViewModel = New LoginViewModel(Me)
                Title = "Iniciar sesion"
            Case GeneralEnums.Views.Home
                CurrentPageViewModel = New HomeViewModel(Me)
                Title = "FlashBooking"
            Case GeneralEnums.Views.CreateBookings
                CurrentPageViewModel = New CreateBookingViewModel(Me)
                Title = "Crear reservacion"
            Case GeneralEnums.Views.Register
                CurrentPageViewModel = New RegisterViewModel(Me)
                Title = "Registrar usuario"
            Case GeneralEnums.Views.CreateRoomType
                CurrentPageViewModel = New CreateRoomTypeViewModel(Me)
                Title = "Crear tipo de habitacion"
            Case GeneralEnums.Views.CreateRoom
                CurrentPageViewModel = New CreateRoomViewModel(Me)
                Title = "Crear habitacion"
        End Select
    End Sub
End Class
