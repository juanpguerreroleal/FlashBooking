Imports FlashBooking.FlashBooking

Public Class GetGuestListResponse
    Public GuestList As List(Of Guest)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Int32
    Public Exception As Exception
    Public Sub New()
        If (GuestList Is Nothing) Then
            GuestList = New List(Of Guest)
        End If
    End Sub
End Class
