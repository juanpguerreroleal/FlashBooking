Imports FlashBooking.FlashBooking

Public Class AuthenticationService
    Public Function Login(userName As String, hash As String) As LoginResponse
        Dim response = New LoginResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim user = db.Users.Where(Function(obj) obj.UserName.Equals(userName)).FirstOrDefault()
                If (user IsNot Nothing) Then
                    If (user.PasswordHash.Equals(hash)) Then
                        response.HasSucceeded = True
                    Else
                        response.ErrorMessageId = DBEnums.Errors.WrongPassword
                    End If
                Else
                    response.ErrorMessageId = DBEnums.Errors.UnexistentUserName
                End If
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
    Public Function Register(user As User) As RegisterResponse
        Dim response = New RegisterResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                db.Users.Add(user)
                db.SaveChanges()
            End Using
            response.HasSucceeded = True
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
End Class
