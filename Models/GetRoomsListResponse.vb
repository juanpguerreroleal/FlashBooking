Imports FlashBooking.FlashBooking

Public Class GetRoomsListResponse
    Public RoomsList As List(Of Room)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Int32
    Public Exception As Exception
    Public Sub New()
        If (RoomsList Is Nothing) Then
            RoomsList = New List(Of Room)
        End If
    End Sub
End Class
