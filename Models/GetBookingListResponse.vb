Imports FlashBooking.FlashBooking
Public Class GetBookingListResponse

    Public BookingList As List(Of Booking)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Integer
    Public Exception As Exception
    Public Sub New()
        If (BookingList Is Nothing) Then
            BookingList = New List(Of Booking)
        End If
    End Sub
End Class
