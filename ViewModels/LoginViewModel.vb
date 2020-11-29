﻿Public Class LoginViewModel
    Inherits BaseViewModel
    Private _authService As AuthenticationService
    Private _context As MainViewModel
    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
            OnPropertyChanged(NameOf(UserName))
        End Set
    End Property
    Public Password As String

    Public Sub New(context As MainViewModel)
        _context = context
        _authService = New AuthenticationService
        LogInCommand = New DelegateCommand(AddressOf LogIn, AddressOf CanLogIn)
        RegisterCommand = New DelegateCommand(AddressOf Register, AddressOf CanRegister)
        UserName = "test@gmail.com"
    End Sub

    Private _logInCommand As ICommand
    Public Property LogInCommand() As ICommand
        Get
            Return _logInCommand
        End Get
        Set(ByVal value As ICommand)
            _logInCommand = value
            OnPropertyChanged(NameOf(LogInCommand))
        End Set
    End Property
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


    Public Sub LogIn(obj As PasswordBox)
        Dim PasswordBox = obj
        ValidatePassword(UserName, PasswordBox.Password)
    End Sub
    Public Sub Register()
        _context.ChangeView(GeneralEnums.Views.Register)
    End Sub

    Private Function CanLogIn(ByVal param As Object) As Boolean
        Return True
    End Function
    Private Function CanRegister(ByVal param As Object) As Boolean
        Return True
    End Function

    Private Function ValidatePassword(userName As String, password As String)
        If (userName IsNot Nothing And password IsNot Nothing) Then
            Dim hashed = Encode(password)
            Dim result = _authService.Login(userName, hashed)
            If (Not result.HasSucceeded) Then
                If (result.ErrorMessageId.Equals(DBEnums.Errors.WrongPassword)) Then
                    MessageBox.Show("El usuario o la contraseña es incorrecto.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error)
                ElseIf (result.ErrorMessageId.Equals(DBEnums.Errors.UnexistentUserName)) Then
                    MessageBox.Show("Introduzca un usuario válido.",
                                  "Error",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error)
                ElseIf (result.ErrorMessageId.Equals(DBEnums.Errors.DBContextError)) Then
                    MessageBox.Show("Ocurrió un error conectadose a la base de datos, revise su conexión a internet.",
                              "Message",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error)
                End If
            ElseIf (result.HasSucceeded) Then
                _context.ChangeView(GeneralEnums.Views.Home)
            End If
        End If
    End Function
    Public Function Encode(value As String) As String
        Dim hash = System.Security.Cryptography.SHA1.Create()
        Dim encoder = New System.Text.ASCIIEncoding()
        Dim combined = encoder.GetBytes(value)
        Return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "")
    End Function
End Class
