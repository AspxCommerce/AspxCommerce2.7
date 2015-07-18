Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Public Class XmlPost

    Protected _MerchantID As String
    Protected _MerchantKey As String
    Protected _RequestUrl As String
    Protected _RequestXml As String
    Protected _TimeoutMilliseconds As Integer

    Public Sub New(ByVal MerchantID As String, ByVal MerchantKey As String, _
        ByVal RequestUrl As String, ByVal RequestXmlFile As String, _
        ByVal TimeoutMilliseconds As Integer)
        _MerchantID = MerchantID
        _MerchantKey = MerchantKey
        _RequestUrl = RequestUrl
        Dim InFile As StreamReader = New StreamReader(RequestXmlFile)
        _RequestXml = InFile.ReadToEnd
        InFile.Close()
        _TimeoutMilliseconds = TimeoutMilliseconds
    End Sub

    Private Shared Function GetAuthorization(ByVal User As String, ByVal Password As String) As String
        Return Convert.ToBase64String(StringToUtf8Bytes(String.Format("{0}:{1}", User, Password)))
    End Function

    Public Function Send() As String
        Dim RetVal As String = "" & vbLf & vbLf
        Dim StartTime As DateTime = DateTime.MinValue
        Dim Data() As Byte = StringToUtf8Bytes(_RequestXml)
        ' Prepare web request.
        System.Net.ServicePointManager.CertificatePolicy = New TrustAllCertificatePolicy
        Dim myRequest As HttpWebRequest = CType(WebRequest.Create(_RequestUrl), HttpWebRequest)
        myRequest.Method = "POST"
        myRequest.ContentLength = Data.Length
        myRequest.Headers.Add("Authorization", _
            String.Format("Basic {0}", GetAuthorization(_MerchantID, _MerchantKey)))
        myRequest.ContentType = "application/xml"
        myRequest.Accept = "application/xml"
        myRequest.Timeout = _TimeoutMilliseconds
        ' Send the data.
        Try
            Dim newStream As Stream = myRequest.GetRequestStream
            newStream.Write(Data, 0, Data.Length)
            newStream.Close()
            ' Read the response.
            Try
                StartTime = DateTime.Now
                Dim myResponse As HttpWebResponse = CType(myRequest.GetResponse, HttpWebResponse)
                RetVal = (RetVal + String.Format("Status code: {0}" & vbLf, myResponse.StatusCode))
                Dim ResponseStream As Stream = myResponse.GetResponseStream
                Dim ResponseStreamReader As StreamReader = New StreamReader(ResponseStream)
                RetVal = (RetVal + ResponseStreamReader.ReadToEnd)
                ResponseStreamReader.Close()
                RetVal = (RetVal + String.Format("" & vbLf & "Response time: {0} ms", _
                    DateTime.Now.Subtract(StartTime).TotalMilliseconds))
            Catch WebExcp As WebException
                If (Not (WebExcp.Response) Is Nothing) Then
                    Dim HttpWResponse As HttpWebResponse = CType(WebExcp.Response, HttpWebResponse)
                    RetVal = (RetVal + String.Format("Status code: {0}" & vbLf, HttpWResponse.StatusCode))
                    Dim sr As StreamReader = New StreamReader(HttpWResponse.GetResponseStream)
                    RetVal = (RetVal + sr.ReadToEnd)
                    sr.Close()
                    RetVal = (RetVal + String.Format("" & vbLf & "Response time: {0} ms", _
                        DateTime.Now.Subtract(StartTime).TotalMilliseconds))
                End If
            End Try
        Catch ex As WebException
            RetVal = ex.Message
        End Try
        Return RetVal
    End Function

    Private Shared Function StringToUtf8Bytes(ByVal InString As String) As Byte()
        Dim utf8encoder As UTF8Encoding = New UTF8Encoding(False, True)
        Return utf8encoder.GetBytes(InString)
    End Function
End Class

