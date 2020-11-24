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
                Dim bookings = db.Bookings.Where(Function(booking) booking.CreationDate > DateTime.Today).Include(Function(obj) obj.Guest).ToList()
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
    Public Function CreateBooking(booking As Booking) As GetCreateBookingResponse
        Dim response = New GetCreateBookingResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                db.Bookings.Add(booking)
                db.SaveChanges()
                response.HasSucceeded = True
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
    Public Function CreateRoomType(roomType As RoomType) As GetCreateRoomTypeResponse
        Dim response = New GetCreateRoomTypeResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                db.RoomTypes.Add(roomType)
                db.SaveChanges()
                response.HasSucceeded = True
            End Using
        Catch ex As Exception
            response.ErrorMessageId = DBEnums.Errors.DBContextError
            response.Exception = ex
        End Try
        Return response
    End Function
#End Region
#Region "Rooms"
    Public Function GetRoomById(id As Integer) As Room
        Try
            Using db As New DBContainer
                Dim room = db.Rooms.Where(Function(obj) obj.Id.Equals(id)).FirstOrDefault()
                Return room
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetRoomsList() As GetRoomsListResponse
        Dim response = New GetRoomsListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim rooms = db.RoomTypes.ToList()
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
    Public Function GetAvailableRoomList(roomTypeId As Integer, checkIn As System.DateTime, checkOut As System.DateTime) As GetAvailableRoomListResponse
        Dim response = New GetAvailableRoomListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim rooms = db.Rooms.Include(Function(x) x.Booking).Where(Function(obj) obj.RoomTypeId.Equals(roomTypeId) And obj.Booking.Where(Function(booking) booking.CheckInDateTime < checkIn And booking.CheckOutDateTime > checkOut).Count.Equals(0)).ToList()
                If (rooms IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.RoomList = rooms
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
#Region "GuestTypes"
    Public Function GetGuestTypeList() As GetGuestTypeListResponse
        Dim response = New GetGuestTypeListResponse
        response.HasSucceeded = False
        Try
            Using db As New DBContainer
                Dim rooms = db.GuestTypes.ToList()
                If (rooms IsNot Nothing) Then
                    response.HasSucceeded = True
                    response.GuestTypeList = rooms
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
