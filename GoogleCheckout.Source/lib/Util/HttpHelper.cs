/*************************************************
 * Copyright (C) 2008 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*************************************************/
/*
 Edit History:
 *  August 2008   Joe Feser joe.feser@joefeser.com
 *  All of the http requests are now done with this class.
 * 
*/
//TODO share the code from the GDATA Project. Create a base class.
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace GCheckout.Util {
  /// <summary>
  /// HTTP Helper class to allow all of the HTTP requests to go through one class.
  /// </summary>
  public static class HttpHelper {

    /// <summary>
    /// Send a message to Google Checkout.
    /// </summary>
    /// <param name="xml">The Xml To Send</param>
    /// <param name="postUrl">The URL to post the request</param>
    /// <param name="merchantID">The MerchantID</param>
    /// <param name="merchantKey">The MerchantKey</param>
    /// <returns></returns>
    public static string SendMessage(byte[] xml, string postUrl, string merchantID, string merchantKey) {
      byte[] Data = xml;
      // Prepare web request.
      HttpWebRequest myRequest =
        (HttpWebRequest)WebRequest.Create(postUrl);
      myRequest.Method = "POST";
      myRequest.ContentLength = Data.Length;
      myRequest.Headers.Add("Authorization",
        string.Format("Basic {0}",
        GetAuthorization(merchantID, merchantKey)));
      myRequest.ContentType = "application/xml; charset=UTF-8";
      myRequest.Accept = "application/xml; charset=UTF-8";
      myRequest.KeepAlive = false;

      //determine if we are using a proxy server
      if (GCheckoutConfigurationHelper.UseProxy) {
        Uri proxyUrl = new Uri(GCheckoutConfigurationHelper.ProxyHost);
        //create a proxy but set it to bypass on local (localhost)
        //someone may want this to be configurable.
        WebProxy proxy = new WebProxy(proxyUrl, true);
        proxy.Credentials = new NetworkCredential(
          GCheckoutConfigurationHelper.ProxyUserName,
          GCheckoutConfigurationHelper.ProxyPassword,
          GCheckoutConfigurationHelper.ProxyDomain);
        myRequest.Proxy = proxy;
      }

      // Send the data.
      using (Stream requestStream = myRequest.GetRequestStream()) {
        requestStream.Write(Data, 0, Data.Length);
      }

      // Read the response.
      string responseXml = string.Empty;
      try {
        using (HttpWebResponse myResponse =
          (HttpWebResponse)myRequest.GetResponse()) {
          using (Stream ResponseStream = myResponse.GetResponseStream()) {
            using (StreamReader ResponseStreamReader =
              new StreamReader(ResponseStream)) {
              responseXml = ResponseStreamReader.ReadToEnd();
            }
          }
        }
      }
      catch (WebException WebExcp) {
        if (WebExcp.Response != null) {
          using (HttpWebResponse HttpWResponse =
            (HttpWebResponse)WebExcp.Response) {
            using (StreamReader sr =
              new StreamReader(HttpWResponse.GetResponseStream())) {
              responseXml = sr.ReadToEnd();
            }
          }
        }
      }
      return responseXml;
    }

    private static string GetAuthorization(string User, string Password) {
      return Convert.ToBase64String(EncodeHelper.StringToUtf8Bytes(
        string.Format("{0}:{1}", User, Password)));
    }
  }
}
