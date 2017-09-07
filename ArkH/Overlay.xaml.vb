Public Class Overlay
    Dim lblHelp As New Label

    Private btnarray(7) As Button
    Private Sub Overlay_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized



        Me.Left = 10
        Me.Top = 10
        For i As Integer = 0 To btnarray.Length - 1
            btnarray(i) = New Button
            btnarray(i).Content = selectbtnname(i)
            btnarray(i).Opacity = 20%
            btnarray(i).Height = 50
        Next

        'redraw()
        OL_Update()




    End Sub
    Public Sub OL_Update()
        stackOL.Children.Clear()


        'Dim labeltitle As New Label
        'labeltitle.Content = globvar_applicationname & " " & globvar_applicationversion
        'labeltitle.FontSize = 18
        ' labeltitle.HorizontalContentAlignment = HorizontalAlignment.Center
        lblTitle.FontSize = 18
        lblTitle.Content = globvar_applicationname & " " & globvar_applicationversion

        'stackOL.Children.Add(labeltitle)
        stackOL.Children.Add(lblTitle)

        Dim lblHelp As New Label()
        lblHelp.Content = "AutoWalk: F5" & vbCrLf & "MateStop: F6" & vbCrLf & "Autofeed: F7" & vbCrLf & "AutoEat: F8" & vbCrLf & "AutoGather/PetHeal: F9" & vbCrLf & "AutoAttack: F10" & vbCrLf & "AutoDrink: F11"
        lblHelp.FontSize = 9
        updateglobalstate()

        If globvar_globalstate = False Then
            stackOL.Children.Add(lblHelp)

        End If

        If globvar_autowalkstate = True Then
            stackOL.Children.Add(btnarray(0))

        End If
        If globvar_matehelperstate = True Then
            stackOL.Children.Add(btnarray(1))
        End If
        'MsgBox(globvar_globalstate & vbCrLf & globvar_autowalkstate)



    End Sub

    Private Function selectbtnname(ByVal nrbtn As Integer)
        Dim btntitle As String = ""
        Select Case nrbtn
            Case 0
                btntitle = "AutoWalk (F5)"
            Case 1
                btntitle = "MateHelper (F6)"
            Case 2
                btntitle = "AutoFeed (F7)"
            Case 3
                btntitle = "AutoEat (F8)"
            Case 4
                btntitle = "AutoGather / PetHeal (F9)"
            Case 5
                btntitle = "AutoAttack (F10)"
            Case 6
                btntitle = "AutoDrink (F11)"




        End Select
        Return btntitle

    End Function
End Class
