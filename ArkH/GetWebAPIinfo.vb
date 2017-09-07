Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Module getWebAPIinfo



    Public Sub getAPIdataFromWeb()
        globvar_APIversion = getwebAPIdata("http://arkdedicated.com/version")
        globvar_APistatus = getwebAPIdata("http://arkdedicated.com/officialserverstatus.ini")
        globvar_APInews = striptags(getwebAPIdata("http://arkdedicated.com/news.ini"))
        globvar_serverlist = getwebAPIdata("http://arkdedicated.com/officialservers.ini")
        findEvents()
        'MsgBox(globvar_eventmessage)
    End Sub

    Private Function getwebAPIdata(ByVal url As String)
        Dim request As WebRequest = WebRequest.Create(url)
        Dim response As WebResponse = request.GetResponse()
        Dim datastream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(datastream)
        Dim responsefromserver As String = reader.ReadToEnd()
        reader.Close()
        response.Close()
        Return responsefromserver
    End Function

    Private Function striptags(ByVal html As String) As String
        Return Regex.Replace(html, "<.*?>", "")
    End Function



    Private Sub findEvents()
        Dim Substrings() As String = globvar_APInews.Split(vbCrLf)
        globvar_eventmessage = ""
        globvar_eventactive = False
        For Each substring In Substrings
            'MsgBox(substring)
            If substring.Contains("Phoenix") = True Then
                globvar_eventactive = True
                globvar_eventmessage = substring
                ' MsgBox(substring)
            Else
            End If
        Next
        ' MsgBox(globvar_eventmessage)
    End Sub

End Module