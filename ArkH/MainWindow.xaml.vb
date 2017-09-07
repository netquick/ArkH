Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices

Class MainWindow
    Dim overlaywin As New Overlay()
    Dim notifywin As New Notification()
    Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal nVirtKey As keys) As Short
    Private WithEvents Bgworker As New ComponentModel.BackgroundWorker

    Public Enum keys

        ' // UNASSIGNED // = &HFFFF0000    ' // [Modifiers] = -65536
        ' // UNASSIGNED // = &H0    ' // [None] = 000

        VK_LBUTTON = &H1        ' // [LButton] = 001
        VK_RBUTTON = &H2        ' // [RButton] = 002
        VK_CANCEL = &H3         ' // [Cancel] = 003
        VK_MBUTTON = &H4        ' // [MButton] = 004    ' NOT contiguous with L & RBUTTON
        VK_XBUTTON1 = &H5       ' // [XButton1] = 005   ' NOT contiguous with L & RBUTTON
        VK_XBUTTON2 = &H6       ' // [XButton2] = 006   ' NOT contiguous with L & RBUTTON
        ' // UNASSIGNED // = &H7    ' // ''UNASSIGNED = 007

        VK_BACK = &H8           ' // [Back] = 008
        VK_TAB = &H9        ' // [Tab] = 009
        ' // RESERVED // = &HA        ' // [LineFeed] = 010
        ' // RESERVED // = &HB        ' // ''UNASSIGNED = 011
        VK_CLEAR = &HC          ' // [Clear] = 012
        VK_RETURN = &HD         ' // [Return] = 013
        ' // UNDEFINED //       ' // [Enter] = 013
        VK_SHIFT = &H10         ' // [ShiftKey] = 016
        VK_CONTROL = &H11       ' // [ControlKey] = 017
        VK_MENU = &H12          ' // [Menu] = 018
        VK_PAUSE = &H13         ' // [Pause] = 019
        VK_CAPITAL = &H14       ' // [Capital] = 020
        ' // UNDEFINED //       ' // [CapsLock] = 020

        VK_HANGUL = &H15        ' // [HangulMode] = 021
        VK_HANGEUL = &H15       ' // [HanguelMode] = 021 ' old name (compatibility)
        VK_KANA = &H15          ' // [KanaMode] = 021
        VK_JUNJA = &H17         ' // [JunjaMode] = 023
        VK_FINAL = &H18         ' // [FinalMode] = 024
        VK_KANJI = &H19         ' // [KanjiMode] = 025
        VK_HANJA = &H19         ' // [HanjaMode] = 025

        VK_ESCAPE = &H1B        ' // [Escape] = 027

        VK_CONVERT = &H1C       ' // [IMEConvert] = 028
        VK_NONCONVERT = &H1D    ' // [IMENonconvert] = 029
        VK_ACCEPT = &H1E        ' // [IMEAccept] = 030
        VK_MODECHANGE = &H1F    ' // [IMEModeChange] = 031

        VK_SPACE = &H20         ' // [Space] = 032
        VK_PRIOR = &H21         ' // [Prior] = 033
        ' // UNDEFINED //       ' // [PageUp] = 033
        VK_NEXT = &H22          ' // [Next] = 034
        ' // UNDEFINED //       ' // [PageDown] = 034
        VK_END = &H23           ' // [End] = 035
        VK_HOME = &H24          ' // [Home] = 036

        VK_LEFT = &H25          ' // [Left] = 037
        VK_UP = &H26        ' // [Up] = 038
        VK_RIGHT = &H27         ' // [Right] = 039
        VK_DOWN = &H28          ' // [Down] = 040

        VK_SELECT = &H29        ' // [Select] = 041
        VK_PRINT = &H2A         ' // [Print] = 042
        VK_EXECUTE = &H2B       ' // [Execute] = 043
        VK_SNAPSHOT = &H2C      ' // [Snapshot] = 044
        ' // UNDEFINED //       ' // [PrintScreen] = 044
        VK_INSERT = &H2D        ' // [Insert] = 045
        VK_DELETE = &H2E        ' // [Delete] = 046
        VK_HELP = &H2F          ' // [Help] = 047

        VK_0 = &H30         ' // [D0] = 048
        VK_1 = &H31         ' // [D1] = 049
        VK_2 = &H32         ' // [D2] = 050
        VK_3 = &H33         ' // [D3] = 051
        VK_4 = &H34         ' // [D4] = 052
        VK_5 = &H35         ' // [D5] = 053
        VK_6 = &H36         ' // [D6] = 054
        VK_7 = &H37         ' // [D7] = 055
        VK_8 = &H38         ' // [D8] = 056
        VK_9 = &H39         ' // [D9] = 057

        ' // UNASSIGNED // = &H40 to &H4F (058 to 064)

        VK_A = &H41         ' // [A] = 065
        VK_B = &H42         ' // [B] = 066
        VK_C = &H43         ' // [C] = 067
        VK_D = &H44         ' // [D] = 068
        VK_E = &H45         ' // [E] = 069
        VK_F = &H46         ' // [F] = 070
        VK_G = &H47         ' // [G] = 071
        VK_H = &H48         ' // [H] = 072
        VK_I = &H49         ' // [I] = 073
        VK_J = &H4A         ' // [J] = 074
        VK_K = &H4B         ' // [K] = 075
        VK_L = &H4C         ' // [L] = 076
        VK_M = &H4D         ' // [M] = 077
        VK_N = &H4E         ' // [N] = 078
        VK_O = &H4F         ' // [O] = 079
        VK_P = &H50         ' // [P] = 080
        VK_Q = &H51         ' // [Q] = 081
        VK_R = &H52         ' // [R] = 082
        VK_S = &H53         ' // [S] = 083
        VK_T = &H54         ' // [T] = 084
        VK_U = &H55         ' // [U] = 085
        VK_V = &H56         ' // [V] = 086
        VK_W = &H57         ' // [W] = 087
        VK_X = &H58         ' // [X] = 088
        VK_Y = &H59         ' // [Y] = 089
        VK_Z = &H5A         ' // [Z] = 090

        VK_LWIN = &H5B          ' // [LWin] = 091
        VK_RWIN = &H5C          ' // [RWin] = 092
        VK_APPS = &H5D          ' // [Apps] = 093
        ' // RESERVED // = &H5E        ' // ''UNASSIGNED = 094
        VK_SLEEP = &H5F         ' // [Sleep] = 095

        VK_NUMPAD0 = &H60       ' // [NumPad0] = 096
        VK_NUMPAD1 = &H61       ' // [NumPad1] = 097
        VK_NUMPAD2 = &H62       ' // [NumPad2] = 098
        VK_NUMPAD3 = &H63       ' // [NumPad3] = 099
        VK_NUMPAD4 = &H64       ' // [NumPad4] = 100
        VK_NUMPAD5 = &H65       ' // [NumPad5] = 101
        VK_NUMPAD6 = &H66       ' // [NumPad6] = 102
        VK_NUMPAD7 = &H67       ' // [NumPad7] = 103
        VK_NUMPAD8 = &H68       ' // [NumPad8] = 104
        VK_NUMPAD9 = &H69       ' // [NumPad9] = 105

        VK_MULTIPLY = &H6A      ' // [Multiply] = 106
        VK_ADD = &H6B           ' // [Add] = 107
        VK_SEPARATOR = &H6C     ' // [Separator] = 108
        VK_SUBTRACT = &H6D      ' // [Subtract] = 109
        VK_DECIMAL = &H6E       ' // [Decimal] = 110
        VK_DIVIDE = &H6F        ' // [Divide] = 111

        VK_F1 = &H70        ' // [F1] = 112
        VK_F2 = &H71        ' // [F2] = 113
        VK_F3 = &H72        ' // [F3] = 114
        VK_F4 = &H73        ' // [F4] = 115
        VK_F5 = &H74        ' // [F5] = 116
        VK_F6 = &H75        ' // [F6] = 117
        VK_F7 = &H76        ' // [F7] = 118
        VK_F8 = &H77        ' // [F8] = 119
        VK_F9 = &H78        ' // [F9] = 120
        VK_F10 = &H79           ' // [F10] = 121
        VK_F11 = &H7A           ' // [F11] = 122
        VK_F12 = &H7B           ' // [F12] = 123

        VK_F13 = &H7C           ' // [F13] = 124
        VK_F14 = &H7D           ' // [F14] = 125
        VK_F15 = &H7E           ' // [F15] = 126
        VK_F16 = &H7F           ' // [F16] = 127
        VK_F17 = &H80           ' // [F17] = 128
        VK_F18 = &H81           ' // [F18] = 129
        VK_F19 = &H82           ' // [F19] = 130
        VK_F20 = &H83           ' // [F20] = 131
        VK_F21 = &H84           ' // [F21] = 132
        VK_F22 = &H85           ' // [F22] = 133
        VK_F23 = &H86           ' // [F23] = 134
        VK_F24 = &H87           ' // [F24] = 135

        ' // UNASSIGNED // = &H88 to &H8F (136 to 143)

        VK_NUMLOCK = &H90       ' // [NumLock] = 144
        VK_SCROLL = &H91        ' // [Scroll] = 145

        VK_OEM_NEC_EQUAL = &H92     ' // [NEC_Equal] = 146    ' NEC PC-9800 kbd definitions "=" key on numpad
        VK_OEM_FJ_JISHO = &H92      ' // [Fujitsu_Masshou] = 146    ' Fujitsu/OASYS kbd definitions "Dictionary" key
        VK_OEM_FJ_MASSHOU = &H93    ' // [Fujitsu_Masshou] = 147    ' Fujitsu/OASYS kbd definitions "Unregister word" key
        VK_OEM_FJ_TOUROKU = &H94    ' // [Fujitsu_Touroku] = 148    ' Fujitsu/OASYS kbd definitions "Register word" key  
        VK_OEM_FJ_LOYA = &H95       ' // [Fujitsu_Loya] = 149    ' Fujitsu/OASYS kbd definitions "Left OYAYUBI" key
        VK_OEM_FJ_ROYA = &H96       ' // [Fujitsu_Roya] = 150    ' Fujitsu/OASYS kbd definitions "Right OYAYUBI" key

        ' // UNASSIGNED // = &H97 to &H9F (151 to 159)

        ' NOTE :: &HA0 to &HA5 (160 to 165) = left and right Alt, Ctrl and Shift virtual keys.
        ' NOTE :: Used only as parameters to GetAsyncKeyState() and GetKeyState().
        ' NOTE :: No other API or message will distinguish left and right keys in this way.
        VK_LSHIFT = &HA0        ' // [LShiftKey] = 160
        VK_RSHIFT = &HA1        ' // [RShiftKey] = 161
        VK_LCONTROL = &HA2      ' // [LControlKey] = 162
        VK_RCONTROL = &HA3      ' // [RControlKey] = 163
        VK_LMENU = &HA4         ' // [LMenu] = 164
        VK_RMENU = &HA5         ' // [RMenu] = 165

        VK_BROWSER_BACK = &HA6      ' // [BrowserBack] = 166
        VK_BROWSER_FORWARD = &HA7   ' // [BrowserForward] = 167
        VK_BROWSER_REFRESH = &HA8   ' // [BrowserRefresh] = 168
        VK_BROWSER_STOP = &HA9      ' // [BrowserStop] = 169
        VK_BROWSER_SEARCH = &HAA    ' // [BrowserSearch] = 170
        VK_BROWSER_FAVORITES = &HAB ' // [BrowserFavorites] = 171
        VK_BROWSER_HOME = &HAC      ' // [BrowserHome] = 172

        VK_VOLUME_MUTE = &HAD       ' // [VolumeMute] = 173
        VK_VOLUME_DOWN = &HAE       ' // [VolumeDown] = 174
        VK_VOLUME_UP = &HAF     ' // [VolumeUp] = 175

        VK_MEDIA_NEXT_TRACK = &HB0  ' // [MediaNextTrack] = 176
        VK_MEDIA_PREV_TRACK = &HB1  ' // [MediaPreviousTrack] = 177
        VK_MEDIA_STOP = &HB2    ' // [MediaStop] = 178
        VK_MEDIA_PLAY_PAUSE = &HB3  ' // [MediaPlayPause] = 179

        VK_LAUNCH_MAIL = &HB4       ' // [LaunchMail] = 180
        VK_LAUNCH_MEDIA_SELECT = &HB5 ' // [SelectMedia] = 181
        VK_LAUNCH_APP1 = &HB6       ' // [LaunchApplication1] = 182
        VK_LAUNCH_APP2 = &HB7       ' // [LaunchApplication2] = 183
        ' // UNASSIGNED // = &HB8   ' // ''UNASSIGNED = 184
        ' // UNASSIGNED // = &HB9   ' // ''UNASSIGNED = 185

        VK_OEM_1 = &HBA         ' // [Oem1] = 186           ' ";:" for USA
        ' // UNDEFINED //       ' // [OemSemicolon] = 186       ' ";:" for USA
        VK_OEM_PLUS = &HBB      ' // [Oemplus] = 187        ' "+" any country
        VK_OEM_COMMA = &HBC     ' // [Oemcomma] = 188       ' "," any country
        VK_OEM_MINUS = &HBD     ' // [OemMinus] = 189       ' "-" any country
        VK_OEM_PERIOD = &HBE    ' // [OemPeriod] = 190      ' "." any country
        VK_OEM_2 = &HBF         ' // [Oem2] = 191           ' "/?" for USA
        ' // UNDEFINED //       ' // [OemQuestion] = 191    ' "/?" for USA
        ' // UNDEFINED //       ' // [Oemtilde] = 192       ' "'~" for USA
        VK_OEM_3 = &HC0         ' // [Oem3] = 192           ' "'~" for USA

        ' // RESERVED // = &HC1 to &HD7 (193 to 215)
        ' // UNASSIGNED // = &HD8 to &HDA (216 to 218)

        VK_OEM_4 = &HDB         ' // [Oem4] = 219           ' "[{" for USA
        ' // UNDEFINED //       ' // [OemOpenBrackets] = 219    ' "[{" for USA
        ' // UNDEFINED //       ' // [OemPipe] = 220        ' "\|" for USA
        VK_OEM_5 = &HDC         ' // [Oem5] = 220           ' "\|" for USA
        VK_OEM_6 = &HDD         ' // [Oem6] = 221           ' "]}" for USA
        ' // UNDEFINED //       ' // [OemCloseBrackets] = 221   ' "]}" for USA
        ' // UNDEFINED //       ' // [OemQuotes] = 222      ' "'"" for USA
        VK_OEM_7 = &HDE         ' // [Oem7] = 222           ' "'"" for USA
        VK_OEM_8 = &HDF         ' // [Oem8] = 223

        ' // RESERVED // = &HE0        ' // ''UNASSIGNED = 224
        VK_OEM_AX = &HE1        ' // [OEMAX] = 225          ' "AX" key on Japanese AX kbd
        ' // UNDEFINED //       ' // [OemBackslash] = 226       ' "<>" or "\|" on RT 102-key kbd
        VK_OEM_102 = &HE2       ' // [Oem102] = 226         ' "<>" or "\|" on RT 102-key kbd
        VK_ICO_HELP = &HE3      ' // [ICOHelp] = 227        ' Help key on ICO
        VK_ICO_00 = &HE4        ' // [ICO00] = 228          ' 00 key on ICO

        VK_PROCESSKEY = &HE5    ' // [ProcessKey] = 229
        VK_ICO_CLEAR = &HE6     ' // [ICOClear] = 230
        VK_PACKET = &HE7        ' // [Packet] = 231
        ' // UNASSIGNED // = &HE8   ' // ''UNASSIGNED = 232

        ' NOTE :: Nokia/Ericsson definitions
        VK_OEM_RESET = &HE9     ' // [OEMReset] = 233
        VK_OEM_JUMP = &HEA      ' // [OEMJump] = 234
        VK_OEM_PA1 = &HEB       ' // [OEMPA1] = 235
        VK_OEM_PA2 = &HEC       ' // [OEMPA2] = 236
        VK_OEM_PA3 = &HED       ' // [OEMPA3] = 237
        VK_OEM_WSCTRL = &HEE    ' // [OEMWSCtrl] = 238
        VK_OEM_CUSEL = &HEF     ' // [OEMCUSel] = 239
        VK_OEM_ATTN = &HF0      ' // [OEMATTN] = 240
        VK_OEM_FINISH = &HF1    ' // [OEMFinish] = 241
        VK_OEM_COPY = &HF2      ' // [OEMCopy] = 242
        VK_OEM_AUTO = &HF3      ' // [OEMAuto] = 243
        VK_OEM_ENLW = &HF4      ' // [OEMENLW] = 244
        VK_OEM_BACKTAB = &HF5       ' // [OEMBackTab] = 245

        VK_ATTN = &HF6          ' // [Attn] = 246
        VK_CRSEL = &HF7         ' // [Crsel] = 247
        VK_EXSEL = &HF8         ' // [Exsel] = 248
        VK_EREOF = &HF9         ' // [EraseEof] = 249
        VK_PLAY = &HFA          ' // [Play] = 250
        VK_ZOOM = &HFB          ' // [Zoom] = 251
        VK_NONAME = &HFC        ' // [NoName] = 252
        VK_PA1 = &HFD           ' // [Pa1] = 253
        VK_OEM_CLEAR = &HFE     ' // [OemClear] = 254

        ' // UNASSIGNED // = &HFFFF    ' // [KeyCode] = 65535
        ' // UNASSIGNED // = &H10000    ' // [Shift] = 65536
        ' // UNASSIGNED // = &H20000    ' // [Control] = 131072
        ' // UNASSIGNED // = &H40000    ' // [Alt] = 262144

    End Enum

    Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized

        Me.Left = 150
        Me.Top = 150
        Try
            initializeapp()
        Catch ex As Exception
            MsgBox("could not initialize")
        End Try
        addprofileoptions()
        Bgworker.RunWorkerAsync()

        If globvar_autostartgame = True Then
            startgame()
        End If
        globaltimer()
        updatemainwin()
    End Sub
    Private Sub Bgworker_DoWork(sender As Object, e As DoWorkEventArgs) Handles Bgworker.DoWork

        getAPIdataFromWeb()

    End Sub
    Public Sub updatemainwin()
        updateglobalstate()

        lblTitle.Content = globvar_applicationname & " " & globvar_applicationversion
        txtGameWindowName.Text = globvar_gamewindowname
        txtPathToGame.Text = globvar_pathtogame
        chkAutoStartGame.IsChecked = globvar_autostartgame
        If globvar_autostartgame = True Then
            txtPathToGame.IsEnabled = True
            btnBrowsePathToGame.IsEnabled = True
        Else
            txtPathToGame.IsEnabled = False
            btnBrowsePathToGame.IsEnabled = False
        End If


        If globvar_enableoverlay = True Then
            notifywin.Show()

            overlaywin.Show()
            notifywin.screenmessage()

            overlaywin.OL_Update()
            chkEnableOverlay.IsChecked = True
        Else
            chkEnableOverlay.IsChecked = False
            overlaywin.Hide()
            notifywin.Hide()
        End If
        overlaywin.OL_Update()
        notifywin.screenmessage()
        'binds
        txtKeyForward.Text = bind_forward
        txtKeyUse.Text = bind_use
        txtKeyWhistle.Text = bind_whistle
        cmbAttackBind.SelectedIndex = bind_attack
        'MsgBox(globvar_eventmessage)
    End Sub


    Private Sub globaltimer()
        Dim globaltick As New System.Windows.Threading.DispatcherTimer()
        AddHandler globaltick.Tick, AddressOf dispatcherTimer_Tick
        globaltick.Interval = TimeSpan.FromSeconds(0.1)
        globaltick.Start()

    End Sub
    Dim gltick As Integer = 0
    Dim glticks As Integer = 0
    Dim gltickm As Integer = 0
    Dim gltickh As Integer = 0
    Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        gltick = gltick + 1
        'action every tick (10times second)
        action_tick()
        If gltick = 10 Then
            glticks = glticks + 1
            gltick = 0
            'action every second
            action_ticks()
        End If
        If glticks = 60 Then
            gltickm = gltickm + 1
            glticks = 0
            'action every minute
            action_tickm()
        End If
        If gltickm = 60 Then
            gltickh = gltickh + 1
            gltickm = 0
            'action every hour
            action_tickh()
        End If
    End Sub
    Private Sub action_tick()
        updatecounter()




        If globvar_autowalkstate = True Then
            AppActivate(globvar_gamewindowname)
            'keypress(keys.VK_W)
            'Dim tempkc = getkeycode(txtKeyForward.Text)
            'Dim vkeycode As Integer = getkeycode(txtKeyForward.Text) And &HFF

            'MsgBox(tempkc & vbCrLf & ConvertCharToVirtualKey(txtKeyWalk.Text))
            keycodepress(ConvertCharToVirtualKey(txtKeyForward.Text))
        End If
        If globvar_matehelperstate = True Then
            AppActivate(globvar_gamewindowname)
            keycodepress(ConvertCharToVirtualKey(txtKeyWhistle.Text))
        End If



    End Sub
    Private Sub action_ticks()
        'Watch for Keypresses to toggle functions
        If GetAsyncKeyState(keys.VK_F5) <> 0 Then
            toggleautowalk()
        End If
        If GetAsyncKeyState(keys.VK_F6) <> 0 Then
            togglematehelper()

        End If
        If GetAsyncKeyState(keys.VK_LMENU) <> 0 Then
            togglemainwin()
        End If
    End Sub
    Private Sub action_tickm()
        Bgworker.RunWorkerAsync()
        updatemainwin()
    End Sub
    Private Sub action_tickh()

    End Sub


    Public Sub toggleautowalk()
        If globvar_autowalkstate = False Then
            globvar_autowalkstate = True
            'overlaywin.toggleautowalkbutton(True)
            overlaywin.OL_Update()
        Else
            globvar_autowalkstate = False
            overlaywin.OL_Update()
            'overlaywin.toggleautowalkbutton(False)
            'keyrelease(keys.VK_W)
            keycoderelease(ConvertCharToVirtualKey(txtKeyForward.Text))
        End If
        updateglobalstate()
    End Sub
    Public Sub togglematehelper()
        If globvar_matehelperstate = False Then
            globvar_matehelperstate = True
            overlaywin.OL_Update()
        Else
            globvar_matehelperstate = False
            overlaywin.OL_Update()
            keycoderelease(ConvertCharToVirtualKey(txtKeyWhistle.Text))
        End If
        updateglobalstate()

    End Sub


    Public Sub togglemainwin()
        Me.Topmost = True
        Me.Topmost = False



    End Sub


    Private Sub updatecounter()
        Dim counter As String
        counter = String.Format(gltickh.ToString("00")) & ":" & String.Format(gltickm.ToString("00")) & ":" & String.Format(glticks.ToString("00")) & "." & gltick
        lblCounter.Content = counter
    End Sub
    Private Sub txtGameWindowName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtGameWindowName.TextChanged
        globvar_gamewindowname = txtGameWindowName.Text
    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs) Handles btnsave.Click
        writeoptions()
    End Sub
    Public Sub initializeapp()
        'check if config file is already there
        Try
            If Not System.IO.File.Exists("config.ini") Then
                createconfig()
            End If
        Catch ex As Exception
            MsgBox("Could not create config file")
        End Try
        Try
            readoptions()
        Catch ex As Exception
            MsgBox("Could not read options")
        End Try
    End Sub

    'creates default config file
    Private Sub createconfig()
        Dim OptionsList As New List(Of String)
        OptionsList.Add("[General]")
        OptionsList.Add("Applicationname=Arkh!")
        OptionsList.Add("Applicationversion=1.0")

        OptionsList.Add("GameWindowName=ARK: Survival Evolved")
        OptionsList.Add("PathToGame=E:\Games\SteamLibrary\steamapps\common\ARK\ShooterGame\Binaries\Win64")
        OptionsList.Add("EnableOverlay=True")
        OptionsList.Add("AutoStartGame=False")
        OptionsList.Add("[Keys]")



        OptionsList.Add("[Binds]")

        OptionsList.Add("Forward=w")
        OptionsList.Add("Use=e")
        OptionsList.Add("WhistleStop=u")
        OptionsList.Add("AttackBind=0")



        File.WriteAllLines("config.ini", OptionsList.ToArray())



        ' Dim writelines() As String = {"[General]", "globvar_applicationname=""ArkH!!""", "globvar_applicationversion=""1.0""", "globvar_gamewindowname=""ARK: Survival Evolved""", "globvar_pathtogame=""""", "globvar_autostartgame=""FALSE""", "[Keys]", "keys_autowalk=""F5""", "keys_matehelper=""F6""", "keys_autofeed=""F7""", "keys_autoeat=""F8""", "keys_autodrink=""F9""", "keys_autoattack=""F10""", "keys_autogather=""F11""", "keys_showhide=""F3""", "keys_overlay=""F4""", "keys_starttimer=""F2""", "keys_switchprofile=""F1""", "[Bindings]", "bind_use=""e""", "bind_attack=""mbutton""", "bind_forward=""w""", "bind_whistle=""u"""}


        '    Using outputfile As New StreamWriter("config.ini")
        '  For Each line As String In writelines
        '  outputfile.WriteLine(line)

        ' Next
        ' End Using
    End Sub






    Public Sub readoptions()
        Dim ioFile As New StreamReader("config.ini")
        Dim ioLines As String = ""
        ioLines = ioFile.ReadToEnd
        ioFile.Close()
        ioFile.Dispose()

        Dim seperatelines() As String = ioLines.Split(CChar(vbCrLf))
        Dim max As Integer = UBound(seperatelines)

        For mycounter = 0 To max - 1
            If seperatelines(mycounter).Contains("[") = True And
                seperatelines(mycounter).Contains("]") = True Then
            ElseIf seperatelines(mycounter).Contains(";") = True Then
            Else
                If seperatelines(mycounter).Contains("=") = True Then
                    Dim parts() As String = seperatelines(mycounter).Split("=")


                    If parts(0).Contains("GameWindowName") = True Then
                        globvar_gamewindowname = parts(1)
                    ElseIf parts(0).Contains("Applicationname") = True Then
                        globvar_applicationname = parts(1)
                    ElseIf parts(0).Contains("Applicationversion") = True Then
                        globvar_applicationversion = parts(1)
                    ElseIf parts(0).Contains("PathToGame") = True Then
                        globvar_pathtogame = parts(1)
                    ElseIf parts(0).Contains("AutoStartGame") = True Then
                        globvar_autostartgame = stringtobool(parts(1))
                    ElseIf parts(0).Contains("EnableOverlay") = True Then
                        globvar_enableoverlay = stringtobool(parts(1))
                    ElseIf parts(0).Contains("Forward") = True Then
                        bind_forward = parts(1)
                    ElseIf parts(0).Contains("Use") = True Then
                        bind_use = parts(1)
                    ElseIf parts(0).Contains("WhistleStop") = True Then
                        bind_whistle = parts(1)
                    ElseIf parts(0).Contains("AttackBind") = True Then
                        bind_attack = parts(1)


                    End If
                End If
            End If
        Next
    End Sub

    Public Sub writeoptions()
        Dim optionslist As New List(Of String)
        optionslist.Add("[General]")

        optionslist.Add("Applicationname=" & globvar_applicationname)
        optionslist.Add("Applicationversion=" & globvar_applicationversion)
        optionslist.Add("GameWindowName=" & globvar_gamewindowname)

        optionslist.Add("PathToGame=" & globvar_pathtogame)

        optionslist.Add("AutoStartGame=" & booltostring(globvar_autostartgame))
        optionslist.Add("EnableOverlay=" & booltostring(globvar_enableoverlay))
        optionslist.Add("[Keys]")
        optionslist.Add("[Binds")
        optionslist.Add("Forward=" & bind_forward)
        optionslist.Add("Use=" & bind_use)
        optionslist.Add("WhistleStop=" & bind_whistle)
        optionslist.Add("AttackBind=" & bind_attack)



        File.WriteAllLines("config.ini", optionslist.ToArray())

    End Sub

    Private Sub chkAutoStartGame_Checked(sender As Object, e As RoutedEventArgs) Handles chkAutoStartGame.Checked
        If chkAutoStartGame.IsChecked = True Then
            globvar_autostartgame = True
        Else
            globvar_autostartgame = False

        End If
        updatemainwin()
        writeoptions()

    End Sub

    Private Sub chkAutoStartGame_IsEnabledChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles chkAutoStartGame.IsEnabledChanged
        updatemainwin()

    End Sub

    Private Sub chkAutoStartGame_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkAutoStartGame.Unchecked
        globvar_autostartgame = False
        updatemainwin()
        writeoptions()
    End Sub

    Private Sub txtPathToGame_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtPathToGame.TextChanged
        globvar_pathtogame = txtPathToGame.Text

    End Sub

    Private Sub MainWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        writeoptions()
        overlaywin.Close()
        notifywin.Close()
    End Sub

    Private Sub btnSetDefault_Click(sender As Object, e As RoutedEventArgs) Handles btnSetDefault.Click
        createconfig()
        readoptions()

        updatemainwin()


    End Sub

    Private Sub chkEnableOverlay_Checked(sender As Object, e As RoutedEventArgs) Handles chkEnableOverlay.Checked
        globvar_enableoverlay = True
        overlaywin.Show()
    End Sub

    Private Sub chkEnableOverlay_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkEnableOverlay.Unchecked
        globvar_enableoverlay = False
        overlaywin.Hide()
    End Sub

    Private Function getkeycode(ByVal s As String) As keys
        Dim kc As KeyConverter = New KeyConverter()
        Try
            Return CType(kc.ConvertFromString(s), keys)
        Catch
            Return Nothing

        End Try
    End Function








    Public Shared Function ConvertCharToVirtualKey(ch As Char) As keys
        Dim vkey As Short = VkKeyScan(ch)
        Dim retval As keys = DirectCast(vkey And &HFF, keys)
        Dim modifiers As Integer = vkey >> 8
        If (modifiers And 1) <> 0 Then
            retval = retval Or keys.VK_SHIFT
        End If
        If (modifiers And 2) <> 0 Then
            retval = retval Or keys.VK_CONTROL
        End If

        Return retval
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function VkKeyScan(ch As Char) As Short
    End Function

    Private Sub txtKeyForward_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtKeyForward.TextChanged
        bind_forward = txtKeyForward.Text
    End Sub

    Private Sub cmbAttackBind_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAttackBind.SelectionChanged
        bind_attack = cmbAttackBind.SelectedIndex

    End Sub

    Private Sub txtKeyUse_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtKeyUse.TextChanged
        bind_use = txtKeyUse.Text
    End Sub

    Private Sub txtKeyWhistle_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtKeyWhistle.TextChanged
        bind_whistle = txtKeyWhistle.Text
    End Sub

    Private Sub addprofileoptions()
        cmbProfileSelect.Items.Clear()
        cmbProfileSelect.Items.Add("Achatina")
        cmbProfileSelect.Items.Add("Allosaurus")
        cmbProfileSelect.Items.Add("Alpha Carno")
        cmbProfileSelect.Items.Add("Ammonite")
        cmbProfileSelect.Items.Add("Anglerfish")
        cmbProfileSelect.Items.Add("Ankylosaurus")
        cmbProfileSelect.Items.Add("Araneo")
        cmbProfileSelect.Items.Add("Archaeopteryx")
        cmbProfileSelect.Items.Add("Argentavis")
        cmbProfileSelect.Items.Add("Arthropluera")
        cmbProfileSelect.Items.Add("Baryonyx")
        cmbProfileSelect.Items.Add("Basilosaurus")
        cmbProfileSelect.Items.Add("Beelzebufo")
        cmbProfileSelect.Items.Add("BunnyDodo")
        cmbProfileSelect.Items.Add("BunnyOviraptor")
        cmbProfileSelect.Items.Add("Brontosaurus")
        cmbProfileSelect.Items.Add("Broodmother Lysrix")
        cmbProfileSelect.Items.Add("Chalicotherium")
        cmbProfileSelect.Items.Add("Carbonemys")
        cmbProfileSelect.Items.Add("Carnotaurus")
        cmbProfileSelect.Items.Add("Castoroides")
        cmbProfileSelect.Items.Add("Cnidaria")
        cmbProfileSelect.Items.Add("Coelacanth")
        cmbProfileSelect.Items.Add("Compy")
        cmbProfileSelect.Items.Add("Daeodon")
        cmbProfileSelect.Items.Add("Deathworm")
        cmbProfileSelect.Items.Add("Dilophosaur")
        cmbProfileSelect.Items.Add("Dimetrodon")
        cmbProfileSelect.Items.Add("Dimorphodon")
        cmbProfileSelect.Items.Add("Diplocaulus")
        cmbProfileSelect.Items.Add("Diplodocus")
        cmbProfileSelect.Items.Add("Direbear")
        cmbProfileSelect.Items.Add("Direwolf")
        cmbProfileSelect.Items.Add("Dodo")
        cmbProfileSelect.Items.Add("DodoRex")
        cmbProfileSelect.Items.Add("Dodo Wyvern")
        cmbProfileSelect.Items.Add("Doedicurus")
        cmbProfileSelect.Items.Add("Dragon")
        cmbProfileSelect.Items.Add("Dung Beetle")
        cmbProfileSelect.Items.Add("Dunkleosteus")
        cmbProfileSelect.Items.Add("Electrophorus")
        cmbProfileSelect.Items.Add("Equus")
        cmbProfileSelect.Items.Add("Unicorn")
        cmbProfileSelect.Items.Add("Eurypterid")
        cmbProfileSelect.Items.Add("Gallimimus")
        cmbProfileSelect.Items.Add("Giant Bee")
        cmbProfileSelect.Items.Add("Giganotosaurus")
        cmbProfileSelect.Items.Add("Gigantopithecus")
        cmbProfileSelect.Items.Add("Griffin")
        cmbProfileSelect.Items.Add("Hesperornis")
        cmbProfileSelect.Items.Add("Human (Male)")
        cmbProfileSelect.Items.Add("Human (Female)")
        cmbProfileSelect.Items.Add("Hyaenodon")
        cmbProfileSelect.Items.Add("Ichthyornis")
        cmbProfileSelect.Items.Add("Ichthyosaurus")
        cmbProfileSelect.Items.Add("Iguanodon")
        cmbProfileSelect.Items.Add("Jerboa")
        cmbProfileSelect.Items.Add("Skeletal Jerboa")
        cmbProfileSelect.Items.Add("Jug Bug")
        cmbProfileSelect.Items.Add("Oil Jug Bug")
        cmbProfileSelect.Items.Add("Water Jug Bug")
        cmbProfileSelect.Items.Add("Kairuku")
        cmbProfileSelect.Items.Add("Kaprosuchus")
        cmbProfileSelect.Items.Add("Kentrosaurus")
        cmbProfileSelect.Items.Add("Leech")
        cmbProfileSelect.Items.Add("Diseased Leech")
        cmbProfileSelect.Items.Add("Leedsichthys")
        cmbProfileSelect.Items.Add("Liopleurodon")
        cmbProfileSelect.Items.Add("Lymantria")
        cmbProfileSelect.Items.Add("Lystrosaurus")
        cmbProfileSelect.Items.Add("Mammoth")
        cmbProfileSelect.Items.Add("Manta")
        cmbProfileSelect.Items.Add("Manticore")
        cmbProfileSelect.Items.Add("Mantis")
        cmbProfileSelect.Items.Add("Megalania")
        cmbProfileSelect.Items.Add("Megaloceros")
        cmbProfileSelect.Items.Add("Megalodon")
        cmbProfileSelect.Items.Add("Megalosaurus")
        cmbProfileSelect.Items.Add("Meganeura")
        cmbProfileSelect.Items.Add("Megapithecus")
        cmbProfileSelect.Items.Add("Megatherium")
        cmbProfileSelect.Items.Add("Mesopithecus")
        cmbProfileSelect.Items.Add("Microraptor")
        cmbProfileSelect.Items.Add("Morellatops")
        cmbProfileSelect.Items.Add("Mosasaurus")
        cmbProfileSelect.Items.Add("Moschops")
        cmbProfileSelect.Items.Add("Onychonycteris")
        cmbProfileSelect.Items.Add("Oviraptor")
        cmbProfileSelect.Items.Add("Ovis")
        cmbProfileSelect.Items.Add("Pachy")
        cmbProfileSelect.Items.Add("Pachyrhinosaurus")
        cmbProfileSelect.Items.Add("Paraceratherium")
        cmbProfileSelect.Items.Add("Parasaurolophus")
        cmbProfileSelect.Items.Add("Pegomastax")
        cmbProfileSelect.Items.Add("Pelagornis")
        cmbProfileSelect.Items.Add("Phiomia")
        cmbProfileSelect.Items.Add("Piranha")
        cmbProfileSelect.Items.Add("Plesiosaur")
        cmbProfileSelect.Items.Add("Procoptodon")
        cmbProfileSelect.Items.Add("Pteranodon")
        cmbProfileSelect.Items.Add("Pulmonoscorpius")
        cmbProfileSelect.Items.Add("Purlovia")
        cmbProfileSelect.Items.Add("Quetzalcoatlus")
        cmbProfileSelect.Items.Add("Raptor")
        cmbProfileSelect.Items.Add("Rex")
        cmbProfileSelect.Items.Add("Rock Elemental")
        cmbProfileSelect.Items.Add("Rubble Golem")
        cmbProfileSelect.Items.Add("Sabertooth")
        cmbProfileSelect.Items.Add("Sabertooth Salmon")
        cmbProfileSelect.Items.Add("Sarco")
        cmbProfileSelect.Items.Add("Skeletal Bronto")
        cmbProfileSelect.Items.Add("Skeletal Carnotaurus")
        cmbProfileSelect.Items.Add("Skeletal Giganotosaurus")
        cmbProfileSelect.Items.Add("Skeletal Quetzal")
        cmbProfileSelect.Items.Add("Skeletal Raptor")
        cmbProfileSelect.Items.Add("Skeletal Rex")
        cmbProfileSelect.Items.Add("Skeletal Stego")
        cmbProfileSelect.Items.Add("Skeletal Trike")
        cmbProfileSelect.Items.Add("Spino")
        cmbProfileSelect.Items.Add("Stegosaurus")
        cmbProfileSelect.Items.Add("Tapejara")
        cmbProfileSelect.Items.Add("Terror Bird")
        cmbProfileSelect.Items.Add("Therizinosaur")
        cmbProfileSelect.Items.Add("Thorny Dragon")
        cmbProfileSelect.Items.Add("Thylacoleo")
        cmbProfileSelect.Items.Add("Titanoboa")
        cmbProfileSelect.Items.Add("Titanomyrma")
        cmbProfileSelect.Items.Add("Titanomyrma")
        cmbProfileSelect.Items.Add("Titanosaur")
        cmbProfileSelect.Items.Add("Triceratops")
        cmbProfileSelect.Items.Add("Trilobite")
        cmbProfileSelect.Items.Add("Troodon")
        cmbProfileSelect.Items.Add("Turkey")
        cmbProfileSelect.Items.Add("Super Turkey")
        cmbProfileSelect.Items.Add("Tusoteuthis")
        cmbProfileSelect.Items.Add("Vulture")
        cmbProfileSelect.Items.Add("Wyvern")
        cmbProfileSelect.Items.Add("Fire Wyvern")
        cmbProfileSelect.Items.Add("Lightning Wyvern")
        cmbProfileSelect.Items.Add("Poison Wyvern")
        cmbProfileSelect.Items.Add("Ice Wyvern")
        cmbProfileSelect.Items.Add("Bone Fire Wyvern")
        cmbProfileSelect.Items.Add("Dodo Wyvern")
        cmbProfileSelect.Items.Add("Zombie Fire Wyvern")
        cmbProfileSelect.Items.Add("Zombie Lightning Wyvern")
        cmbProfileSelect.Items.Add("Zombie Poison Wyvern")
        cmbProfileSelect.Items.Add("Woolly Rhino")
        cmbProfileSelect.Items.Add("Yeti")
        cmbProfileSelect.Items.Add("Yutyrannus")
        cmbProfileSelect.Items.Add("Zomdodo")

    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
