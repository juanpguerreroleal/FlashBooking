Public Class UserModel
    Inherits BaseViewModel
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
            OnPropertyChanged(NameOf(Name))
        End Set
    End Property
    Private _lastName As String
    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
            OnPropertyChanged(NameOf(LastName))
        End Set
    End Property
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
    Private _password As String
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
            OnPropertyChanged(NameOf(Password))
        End Set
    End Property
End Class
