Public Class HomeViewModel
    Inherits BaseViewModel
    Private _context As MainViewModel
    Private _currentControlViewModel As BaseViewModel
    Public Property CurrentControlViewModel() As BaseViewModel
        Get
            Return _currentControlViewModel
        End Get
        Set(ByVal value As BaseViewModel)
            _currentControlViewModel = value
            Me.OnPropertyChanged(NameOf(CurrentControlViewModel))
        End Set
    End Property

    Private _changeOptionCommand As ICommand
    Public Property ChangeOptionCommand() As ICommand
        Get
            Return _changeOptionCommand
        End Get
        Set(ByVal value As ICommand)
            _changeOptionCommand = value
            OnPropertyChanged(NameOf(ChangeOptionCommand))
        End Set
    End Property

    Public Sub New(context As MainViewModel)
        _context = context
        _currentControlViewModel = New GuestOptionsViewModel()
        ChangeOptionCommand = New DelegateCommand(AddressOf ChangeOptionView, AddressOf CanChangeOption)
    End Sub
    Public Sub ChangeOptionView(optionView As Int32)
        Select Case optionView
            Case 1
                CurrentControlViewModel = New GuestOptionsViewModel()
            Case 2
                CurrentControlViewModel = New RoomOptionsViewModel(_context)
            Case 3
                CurrentControlViewModel = New BookingOptionsViewModel(_context)
        End Select
    End Sub
    Public Function CanChangeOption() As Boolean
        Return True
    End Function
End Class
