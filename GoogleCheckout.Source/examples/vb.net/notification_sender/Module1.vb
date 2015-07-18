Imports System
Imports System.Configuration

Module Module1

    Sub Main()
        Dim Req As XmlPost = New XmlPost(GetSetting("GoogleMerchantId"), _
        GetSetting("GoogleMerchantKey"), GetSetting("NotificationListenerUrl"), _
        GetSetting("NotificationFile"), _
        Integer.Parse(GetSetting("TimeoutMilliseconds")))
        Console.WriteLine(Req.Send)
    End Sub

    Private Function GetSetting(ByVal Key As String) As String
        Dim RetVal As String = ConfigurationSettings.AppSettings(Key)
        If RetVal = Nothing OrElse RetVal = String.Empty Then
            Throw New ApplicationException(String.Format("Add key '{0}' to config file", Key))
        End If
        Return RetVal
    End Function


End Module
