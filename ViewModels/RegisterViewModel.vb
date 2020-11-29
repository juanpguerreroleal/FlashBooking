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
        If (Not String.IsNullOrEmpty(obj.Password) AndAlso Not String.IsNullOrEmpty(UserModel.UserName) AndAlso Not String.IsNullOrEmpty(UserModel.Name) AndAlso Not String.IsNullOrEmpty(UserModel.LastName)) Then
            Dim hashed = Encode(obj.Password)
            user.PasswordHash = hashed
            Dim response = _authService.Register(user)
            If (response.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Login)
            ElseIf (Not response.HasSucceeded) Then
                If (response.ErrorMessageId.Equals(DBEnums.Errors.DBContextError)) Then
                    MessageBox.Show("Ocurrió un error al conectarse a la base de datos, verifique su conexión a internet.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error)
                ElseIf (response.ErrorMessageId.Equals(DBEnums.Errors.UserNameInUse)) Then
                    MessageBox.Show("El usuario ya se encuentra en uso.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error)
                End If
            End If
        ElseIf (String.IsNullOrEmpty(obj.Password)) Then
            MessageBox.Show("Introduzca una contraseña.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(UserModel.UserName)) Then
            MessageBox.Show("Introduzca un correo.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(UserModel.Name)) Then
            MessageBox.Show("Introduzca su nombre.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error)
        ElseIf (String.IsNullOrEmpty(UserModel.LastName)) Then
            MessageBox.Show("Introduzca su apellido.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error)
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
