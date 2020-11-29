Imports System.Text.RegularExpressions

Public Class EditRoomTypeView
    Private Sub NumberValidationTextBox(sender As Object, e As TextCompositionEventArgs)
        Dim regex = New Regex("[0-9]+")
        e.Handled = Not regex.IsMatch(e.Text)
    End Sub
End Class
