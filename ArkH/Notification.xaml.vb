Public Class Notification
    Public Sub screenmessage()
        lblNotification.Content = globvar_eventmessage


    End Sub

    Private Sub Notification_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Me.Left = 0
        Me.Top = 0
    End Sub
End Class
