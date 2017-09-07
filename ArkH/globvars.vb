Module globvars
    Public globvar_applicationname As String
    Public globvar_applicationversion As String
    Public globvar_gamewindowname As String
    Public globvar_pathtogame As String
    Public globvar_autostartgame As Boolean
    Public globvar_enableoverlay As Boolean

    Public globvar_gamerunning As Boolean

    'keys
    Public keys_autowalk
    Public keys_matehelper
    Public keys_autofeed
    Public keys_autoeat
    Public keys_autodrink
    Public keys_autoattack
    Public keys_autogather
    Public keys_showhide
    Public keys_overlay
    Public keys_starttimer
    Public keys_switchprofile


    'keybinds
    Public bind_use
    Public bind_attack
    Public bind_forward
    Public bind_whistle

    'state
    Public globvar_autowalkstate = False
    Public globvar_matehelperstate = False
    Public globvar_autofeedstate = False
    Public globvar_autoeatstate = False
    Public globvar_autodrinkstate = False
    Public globvar_autoattackstate = False
    Public globvar_autogatherstate = False
    Public globvar_globalstate = False

    Public globvar_APIversion As String
    Public globvar_APistatus As String
    Public globvar_APInews As String
    Public globvar_serverlist As String
    Public globvar_eventactive As Boolean
    Public globvar_eventmessage As String = ""


    Public Sub updateglobalstate()
        If globvar_autowalkstate = False Then
            If globvar_matehelperstate = False Then
                If globvar_autofeedstate = False Then
                    If globvar_autoeatstate = False Then
                        If globvar_autodrinkstate = False Then
                            If globvar_autoattackstate = False Then
                                If globvar_autogatherstate = False Then
                                    globvar_globalstate = False
                                Else
                                    globvar_globalstate = True
                                End If
                            Else
                                globvar_globalstate = True
                            End If
                        Else
                            globvar_globalstate = True
                        End If
                    Else
                        globvar_globalstate = True
                    End If
                Else
                    globvar_globalstate = True
                End If
            Else
                globvar_globalstate = True

            End If

        Else
            globvar_globalstate = True
        End If
    End Sub

End Module
