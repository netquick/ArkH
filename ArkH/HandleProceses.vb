Module HandleProceses
    Private Sub checkprocess()
        If System.Diagnostics.Process.GetProcessesByName("shootergame").Count > 0 Then
            'Hier gehts weiter wenn der Prozess an ist.
            globvar_gamerunning = True
        Else
            globvar_gamerunning = False


        End If




    End Sub

    Public Sub startgame()
        If System.Diagnostics.Process.GetProcessesByName("shootergame").Count = 0 Then

            Try
                If globvar_pathtogame.Contains("Shootergame.exe") = True Then
                    Process.Start(globvar_pathtogame)
                Else
                    Process.Start(globvar_pathtogame & "\shootergame.exe")

                End If
            Catch ex As Exception
                MsgBox("Can't start process")
            End Try

        End If
    End Sub





End Module
