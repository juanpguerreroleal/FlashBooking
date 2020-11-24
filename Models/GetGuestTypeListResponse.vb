Imports FlashBooking.FlashBooking

Public Class GetGuestTypeListResponse
    Public GuestTypeList As List(Of GuestType)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Int32
    Public Exception As Exception
    Public Sub New()
        If (GuestTypeList Is Nothing) Then
            GuestTypeList = New List(Of GuestType)
        End If
    End Sub
End Class
