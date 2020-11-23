Imports FlashBooking.FlashBooking

Public Class GetRoomTypeListResponse
    Public RoomTypeList As List(Of RoomType)
    Public HasSucceeded As Boolean
    Public ErrorMessageId As Integer
    Public Exception As Exception
    Public Sub New()
        If (RoomTypeList Is Nothing) Then
            RoomTypeList = New List(Of RoomType)
        End If
    End Sub
End Class
