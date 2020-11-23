Imports System.Data.Entity
Imports FlashBooking.FlashBooking

Public Class BussinessService
#Region "Guests"
    Public Function GetGuestList() As GetGuestListResponse
        Dim response = New GetGuestListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim guests = db.Guests.Include(Function(obj) obj.Booking).ToList()
                If (guests IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.GuestList = guests
                Else
                    response.ErrorMessageId = DBEnums.Errors.NoData
                End If
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
#End Region
#Region "Booking"
    Public Function GetBookingList() As GetBookingListResponse
        Dim response = New GetBookingListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim bookings = db.Bookings.Where(Function(booking) booking.CreationDate.Date.Equals(DateTime.Now.Date)).Include(Function(obj) obj.Guest).ToList()
                If (bookings IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.BookingList = bookings
                Else
                    response.ErrorMessageId = DBEnums.Errors.NoData
                End If
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
#End Region
#Region "RoomTypes"
    Public Function GetRoomTypeList() As GetRoomTypeListResponse
        Dim response = New GetRoomTypeListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim roomTypes = db.RoomTypes.ToList()
                If (roomTypes IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.RoomTypeList = roomTypes
                Else
                    response.ErrorMessageId = DBEnums.Errors.NoData
                End If
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
#End Region
#Region "Rooms"
    Public Function GetRoomsList() As GetRoomsListResponse
        Dim response = New GetRoomsListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim rooms = db.RoomTypes.Include(Function(obj) obj.Id).ToList()
                If (rooms IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.RoomsList = rooms
                Else
                    response.ErrorMessageId = DBEnums.Errors.NoData
                End If
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
#End Region
End Class
