Imports System.IO
Imports GCheckout.Util

Partial Public Class _Default
    Inherits System.Web.UI.Page

    'original source from http://www.capprime.com/software_development_weblog/2010/11/29/UsingTheGoogleCheckout25APIWithASPNETMVCTheMissingSample.aspx

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim serialNumber As String = Nothing
        Dim requestInputStream As Stream = MyBase.Request.InputStream
        Dim requestStreamAsString As String = Nothing

        Using oneStreamReader As StreamReader = New StreamReader(requestInputStream)
            requestStreamAsString = oneStreamReader.ReadToEnd
        End Using

        Dim requestStreamAsParts As String() = requestStreamAsString.Split(New Char() {"="c})
        If (requestStreamAsParts.Length >= 2) Then
            serialNumber = requestStreamAsParts(1)
        End If

        'Call NotificationHistory Google Checkout API to retrieve the notification for the given serial number and process the notification
        GoogleCheckoutHelper.ProcessNotification(serialNumber)

        'serialize the message to the output stream only if you could process the message.
        'Otherwise throw an http 500.

        Dim response As New GCheckout.AutoGen.NotificationAcknowledgment With { _
            .serialnumber = serialNumber _
        }
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.BinaryWrite(EncodeHelper.Serialize(response))

    End Sub

End Class