Imports FlashBooking.FlashBooking

Public Class RegisterViewModel
    Inherits BaseViewModel
    Private _context As MainViewModel
    Private _authService As AuthenticationService
    Private _userModel As UserModel
    Public Property UserModel() As UserModel
        Get
            Return _userModel
        End Get
        Set(ByVal value As UserModel)
            _userModel = value
            OnPropertyChanged(NameOf(UserModel))
        End Set
    End Property
    Public Sub New(context As MainViewModel)
        _context = context
        _authService = New AuthenticationService()
        RegisterCommand = New DelegateCommand(AddressOf Register, AddressOf CanRegister)
        UserModel = New UserModel()
    End Sub
    Private _registerCommand As ICommand
    Public Property RegisterCommand() As ICommand
        Get
            Return _registerCommand
        End Get
        Set(ByVal value As ICommand)
            _registerCommand = value
            OnPropertyChanged(NameOf(RegisterCommand))
        End Set
    End Property
    Public Sub Register(obj As PasswordBox)
        Dim user = New User()
        user.Name = UserModel.Name
        user.LastName = UserModel.LastName
        user.UserName = UserModel.UserName
        user.CreationDate = DateTime.Now
        If (obj.Password IsNot Nothing) Then
            Dim hashed = Encode(obj.Password)
            user.PasswordHash = hashed
            Dim response = _authService.Register(user)
            If (response.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Login)
            End If
        End If
    End Sub
    Public Function Encode(value As String) As String
        Dim hash = System.Security.Cryptography.SHA1.Create()
        Dim encoder = New System.Text.ASCIIEncoding()
        Dim combined = encoder.GetBytes(value)
        Return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "")
    End Function
    Private Function CanRegister(ByVal param As Object) As Boolean
        Return True
    End Function
End Class
