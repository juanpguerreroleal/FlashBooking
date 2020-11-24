Imports FlashBooking.FlashBooking

Public Class GetAvailableRoomListResponse
    Public RoomList As List(Of Room)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Int32
    Public Exception As Exception
    Public Sub New()
        If (RoomList Is Nothing) Then
            RoomList = New List(Of Room)
        End If
    End Sub
End Class
