Module functions
    Public Function booltostring(ByVal inbool As Boolean)
        Dim outstring As String
        If inbool = True Then
            outstring = "True"
        Else
            outstring = "False"
        End If
        Return outstring
    End Function
    Public Function stringtobool(ByVal instring As String)
        Dim outbool As Boolean
        If instring = "True" Then
            outbool = True
        Else
            outbool = False
        End If
        Return outbool
    End Function
End Module
