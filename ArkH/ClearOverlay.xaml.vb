Public Class ClearOverlay


    Public Sub refreshOL()
        updateglobalstate()






        OLstack.Children.Clear()
        OLstack.Children.Add(lblTitle)
        If globvar_eventactive = True Then
            lblEvent.Text = globvar_eventmessage
            'lblEvent.HorizontalContentAlignment = HorizontalAlignment.Center
            'txtEvent.Text = globvar_eventmessage
            OLstack.Children.Add(lblEvent)
        End If
        If globvar_globalstate = False Then
            OLstack.Children.Add(lblHelp)
        End If
        If globvar_autowalkstate = True Then
            OLstack.Children.Add(btnAutoWalk)

        End If
        If globvar_matehelperstate = True Then
            OLstack.Children.Add(btnMateHelper)
        End If

    End Sub

    Private Sub ClearOverlay_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Me.Left = 10
        Me.Top = 10
        refreshOL()

    End Sub
End Class
