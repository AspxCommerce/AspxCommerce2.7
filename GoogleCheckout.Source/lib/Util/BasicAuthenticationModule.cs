/*************************************************
 * Copyright (C) 2006-2012 Google Inc.
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
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
 * 
*/

using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace GCheckout.Util {

  /// <summary>
  /// If you are processing notifications or merchant calculation callback 
  /// requests, but you do not want to turn on basic HTTP authentication in 
  /// IIS, you can use this class to validate the incoming API requests from 
  /// Google Checkout.
  /// </summary>
  public class BasicAuthenticationModule : IHttpModule {

    private string _userName = string.Empty;

    /// <summary>
    /// The UserName performing the request
    /// </summary>
    protected string UserName {
      get {
        return _userName;
      }
      set {
        if (value != null)
          _userName = value;
      }
    }

    private string _password = string.Empty;

    /// <summary>
    /// The Password of the User performing the call.
    /// </summary>
    protected string Password {
      get {
        return _password;
      }
      set {
        if (value != null)
          _password = value;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="BasicAuthenticationModule"/> 
    /// class. This is done by IIS.
    /// </summary>
    public BasicAuthenticationModule() {
    }

    /// <summary>
    /// Disposes of the resources (other than memory) used by the
    /// module that implements <see langword="IHttpModule."/>
    /// </summary>
    public void Dispose() {
    }

    /// <summary>
    /// Inits the specified application. This will be called by the system.
    /// </summary>
    /// <param name="Application">The HTTP application.</param>
    public void Init(HttpApplication Application) {
      Application.AuthenticateRequest +=
        new EventHandler(this.OnAuthenticateRequest);
      Application.EndRequest += new EventHandler(this.OnEndRequest);
    }

    /// <summary>
    /// Called when IIS asks to authenticate an HTTP request.
    /// </summary>
    /// <param name="source">The calling HttpApplication.</param>
    /// <param name="eventArgs">
    /// The <see cref="System.EventArgs"/> instance 
    /// containing the event data.
    /// </param>
    public void OnAuthenticateRequest(object source, EventArgs eventArgs) {
      HttpApplication App = (HttpApplication)source;
      string AuthHeader = App.Request.Headers["Authorization"];
      UserName = GetUserName(AuthHeader);
      Password = GetPassword(AuthHeader);
      if (UserHasAccess(UserName, Password)) {
        App.Context.User = new GenericPrincipal(
          new GenericIdentity(UserName, "Google.Checkout.Basic"),
          new string[1] { "User" });
      }
      else {
        App.Response.StatusCode = 401;
        App.Response.StatusDescription = "Access Denied";
        App.Response.Write("401 Access Denied");
        App.CompleteRequest();
      }
    }

    /// <summary>
    /// Called by the system when the HTTP request ends.
    /// </summary>
    /// <param name="source">The calling HttpApplication.</param>
    /// <param name="eventArgs">
    /// The <see cref="System.EventArgs"/> instance 
    /// containing the event data.
    /// </param>
    public void OnEndRequest(object source, EventArgs eventArgs) {
      HttpApplication app = (HttpApplication)source;
      if (app.Response.StatusCode == 401) {
        app.Response.AppendHeader(
          "WWW-Authenticate", "Basic Realm=\"CheckoutCallbackRealm\"");
      }
    }

    /// <summary>
    /// Gets the name of the user from the "Authorization" HTTP header. That header has
    /// user name and password (as typed by the user) in a Base64-encoded form.
    /// </summary>
    /// <param name="AuthHeader">
    /// The value of the "Authorization" HTTP header.
    /// </param>
    /// <returns>The name of the user as typed by the user in the browser.</returns>
    public static string GetUserName(string AuthHeader) {
      return GetDecodedAndSplitAuthorizatonHeader(AuthHeader)[0];
    }

    /// <summary>
    /// Gets the password from the "Authorization" HTTP header. That header has
    /// user name and password (as typed by the user) in a Base64-encoded form.
    /// </summary>
    /// <param name="AuthHeader">
    /// The value of the "Authorization" HTTP header.
    /// </param>
    /// <returns>The password as typed by the user in the browser.</returns>
    public static string GetPassword(string AuthHeader) {
      return GetDecodedAndSplitAuthorizatonHeader(AuthHeader)[1];
    }

    private static string[] GetDecodedAndSplitAuthorizatonHeader(
      string AuthHeader) {
      string[] RetVal = new string[2] { string.Empty, string.Empty };
      if (AuthHeader != null && AuthHeader.StartsWith("Basic ")) {
        try {
          string EncodedString = AuthHeader.Substring(6);
          byte[] DecodedBytes = Convert.FromBase64String(EncodedString);
          string DecodedString = new ASCIIEncoding().GetString(DecodedBytes);
          RetVal = DecodedString.Split(new char[] { ':' });
        }
        catch (Exception ex) {
          Log.Err(
            "BasicAuthenticationModule.GetDecodedAndSplitAuthorizatonHeader:" 
            + ex.Message);
        }
      }
      return RetVal;
    }

    /// <summary>
    /// Verify if the user has access to perform the callback
    /// </summary>
    /// <param name="UserName">The UserName making the call.</param>
    /// <param name="Password">The Password of the user.</param>
    /// <returns>True or false if the user is able to perform the callback.</returns>
    protected virtual bool UserHasAccess(string UserName, string Password) {
      string ID = GCheckoutConfigurationHelper.MerchantID.ToString();
      if (ID == null)
        throw new ApplicationException(
          "Set the 'GoogleMerchantID' key in the config file.");
      string Key = GCheckoutConfigurationHelper.MerchantKey;
      if (Key == null)
        throw new ApplicationException(
          "Set the 'GoogleMerchantKey' key in the config file.");
      return (UserName == ID && Password == Key);
    }
  }
}
