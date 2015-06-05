using System;
using System.IO;
using System.Net;
using System.Text;

namespace NotificationSender {
  public class XmlPost {
    protected string _MerchantID;
    protected string _MerchantKey;
    protected string _RequestUrl;
    protected string _RequestXml;
    protected int _TimeoutMilliseconds;

    public XmlPost(string MerchantID, string MerchantKey, string RequestUrl, 
      string RequestXmlFile, int TimeoutMilliseconds) {
      _MerchantID = MerchantID;
      _MerchantKey = MerchantKey;
      _RequestUrl = RequestUrl;
      StreamReader InFile = new StreamReader(RequestXmlFile);
      _RequestXml = InFile.ReadToEnd();
      InFile.Close();
      _TimeoutMilliseconds = TimeoutMilliseconds;
    }

    private static string GetAuthorization(string User, string Password) {
      return Convert.ToBase64String(StringToUtf8Bytes(
        string.Format("{0}:{1}", User, Password)));
    }

    public string Send() {
      string RetVal = "\n\n";
      DateTime StartTime = DateTime.MinValue;
      byte[] Data = StringToUtf8Bytes(_RequestXml);
      // Prepare web request.
      System.Net.ServicePointManager.CertificatePolicy = 
        new TrustAllCertificatePolicy();
      HttpWebRequest myRequest = 
        (HttpWebRequest)WebRequest.Create(_RequestUrl);
      myRequest.Method = "POST";
      myRequest.ContentLength = Data.Length;
      myRequest.Headers.Add("Authorization", 
        string.Format("Basic {0}", 
        GetAuthorization(_MerchantID, _MerchantKey)));
      myRequest.ContentType = "application/xml";
      myRequest.Accept = "application/xml";
      myRequest.Timeout = _TimeoutMilliseconds;
      // Send the data.
      try {
        Stream newStream = myRequest.GetRequestStream();
        newStream.Write(Data, 0, Data.Length);
        newStream.Close();
        // Read the response.
        try {
          StartTime = DateTime.Now;
          HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
          RetVal += string.Format("Status code: {0}\n", myResponse.StatusCode);
          Stream ResponseStream = myResponse.GetResponseStream();
          StreamReader ResponseStreamReader = new StreamReader(ResponseStream);
          RetVal += ResponseStreamReader.ReadToEnd();
          ResponseStreamReader.Close();
          RetVal += string.Format("\nResponse time: {0} ms", DateTime.Now.Subtract(StartTime).TotalMilliseconds);
        }
        catch (WebException WebExcp) {
          if(WebExcp.Response != null) {
            HttpWebResponse HttpWResponse = (HttpWebResponse)WebExcp.Response;
            RetVal += string.Format("Status code: {0}\n", HttpWResponse.StatusCode);
            StreamReader sr = new
              StreamReader(HttpWResponse.GetResponseStream());
            RetVal += sr.ReadToEnd();
            sr.Close();
            RetVal += string.Format("\nResponse time: {0} ms", DateTime.Now.Subtract(StartTime).TotalMilliseconds);
          }
        }
      }
      catch (WebException ex) {
        RetVal = ex.Message;
      }
      return RetVal;
    }

    private static byte[] StringToUtf8Bytes(string InString) {
      UTF8Encoding utf8encoder = new UTF8Encoding(false, true);
      return utf8encoder.GetBytes(InString);
    }

  }

}
