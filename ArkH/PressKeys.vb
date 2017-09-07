Imports System.Runtime.InteropServices
Imports ArkH.MainWindow

Module PressKeys
    <DllImport("user32.dll")>
    Public Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UInteger)
    End Sub

    'these are virtual key codes for keys
    'Const VK_UP As Integer = &H26
    Const VK_UP As Integer = &H57
    'up key
    'Const VK_DOWN As Integer = &H28
    'down key
    'Const VK_LEFT As Integer = &H25
    'Const VK_RIGHT As Integer = &H27
    Const KEYEVENTF_KEYUP As UInteger = &H2
    Const KEYEVENTF_EXTENDEDKEY As UInteger = &H1
    Dim state As Boolean = False
    Public Function keypress(ByVal key As keys) As Integer
        'Press the key
        'MsgBox("Test")
        AppActivate(globvar_gamewindowname)
        keybd_event(CByte(key), 0, KEYEVENTF_EXTENDEDKEY Or 0, 0)
        Return 0

    End Function

    Public Function keyrelease(ByVal key As keys) As Integer
        'Release the key
        AppActivate(globvar_gamewindowname)

        keybd_event(CByte(key), 0, KEYEVENTF_EXTENDEDKEY Or KEYEVENTF_KEYUP, 0)
        Return 0
    End Function

    Public Function keycodepress(ByVal keycode As Integer)
        AppActivate(globvar_gamewindowname)
        keybd_event(keycode, 0, KEYEVENTF_EXTENDEDKEY Or 0, 0)
        Return 0
    End Function

    Public Function keycoderelease(ByVal keycode As Integer)
        AppActivate(globvar_gamewindowname)
        keybd_event(keycode, 0, KEYEVENTF_EXTENDEDKEY Or KEYEVENTF_KEYUP, 0)
        Return 0

    End Function
End Module