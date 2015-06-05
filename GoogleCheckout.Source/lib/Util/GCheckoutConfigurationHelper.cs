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
 *  8-2-2012   Joe Feser joe.feser@joefeser.com
 *  Added LogDirectoryXml Support
 * 
*/
using System;
using System.Configuration;
using System.Xml;

namespace GCheckout.Util {

  /// <summary>
  /// Entry point to obtain the Google Checkout Settings
  /// </summary>
  public class GCheckoutConfigurationHelper {

    private static object __syncRoot = new object();
    private static GCheckoutConfigSection __configSection = null;
    private static bool _initialized = false;
    private static Exception configException = null;

    /// <summary>
    /// The Config Section defined
    /// </summary>
    protected static GCheckoutConfigSection ConfigSection {
      get {
        if (__configSection == null) {
          Initialize();
        }
        return __configSection;
      }
    }

    private static void Initialize() {
      if (!_initialized) {
        try {
          //the reason for this is when the web server starts up
          //a lot of threads may attempt to initialize if the static
          //initializer fails. so until that happens we want to
          //lock the sync root object, then check again
          //this will keep double initializations from firing.
          lock (__syncRoot) {
            if (!_initialized) {
              __configSection =
                ConfigurationManager.GetSection("Google/GoogleCheckout")
                as GCheckoutConfigSection;
              _initialized = true;
              configException = null;
            }
          }
        }
        catch (Exception ex) {
          _initialized = false;
          configException = ex;
          System.Diagnostics.Debug.Write(
            "Config Section Missing or error:" + ex.ToString());
          throw;
        }
      }
    }

    /// <summary>
    /// Your numeric Merchant ID. To see it, log in to Google,
    /// select the Settings tab, click the Integration link.
    /// </summary>
    public static long MerchantID {
      get {
        if (ConfigSection != null) {
          return ConfigSection.MerchantID;
        }
        else if (GetConfigValue("GoogleMerchantID").Length > 0)
          return long.Parse(GetConfigValue("GoogleMerchantID"));
        else {
          throw new ConfigurationErrorsException(
            "Set the 'GoogleMerchantID' key in the config file.");
        }
      }
    }

    /// <summary>
    /// Your alpha-numeric Merchant Key. To see it, log in to
    ///Google, select the Settings tab, click the Integration link.
    /// </summary>
    public static string MerchantKey {
      get {
        if (ConfigSection != null) {
          return ConfigSection.MerchantKey;
        }
        else if (GetConfigValue("GoogleMerchantKey").Length > 0)
          return GetConfigValue("GoogleMerchantKey");
        else {
          throw new ConfigurationErrorsException(
            "Set the 'GoogleMerchantKey' key in the config file.");
        }
      }
    }

    /// <summary>
    /// The Current Environment
    /// </summary>
    public static EnvironmentType Environment {
      get {
        if (ConfigSection != null) {
          return ConfigSection.Environment;
        }
        else if (GetConfigValue("GoogleEnvironment").Length > 0)
          return (EnvironmentType)Enum.Parse(typeof(EnvironmentType),
            GetConfigValue("GoogleEnvironment"), true);
        else {
          throw new ConfigurationErrorsException(
            "Set the 'GoogleMerchantKey' key in the config file.");
        }
      }
    }

    /// <summary>
    /// The &lt;platform-id&gt; tag should only be used by eCommerce providers
    /// who make API requests on behalf of a merchant. The tag's value contains
    /// a Google Checkout merchant ID that identifies the eCommerce provider.
    /// </summary>
    /// <remarks>
    /// <seealso href="http://code.google.com/apis/checkout/developer/index.html#tag_platform-id"/>
    /// </remarks>
    public static long PlatformID {
      get {
        if (ConfigSection != null) {
          if (ConfigSection.PlatformID <= 0) {
            throw new ConfigurationErrorsException(
              "Set the 'PlatformID' attribute in the config section.");
          }
          return ConfigSection.PlatformID;
        }
        else if (GetConfigValue("PlatformID").Length > 0)
          return long.Parse(GetConfigValue("PlatformID"));
        else {
          throw new ConfigurationErrorsException(
            "Set the 'PlatformID' key in the config file.");
        }
      }
    }

    /// <summary>
    /// Is Logging Configured
    /// </summary>
    public static bool Logging {
      get {
        if (ConfigSection != null) {
          return ConfigSection.Logging;
        }
        else if (GetConfigValue("Logging").Length > 0)
          return GetConfigValue("Logging").ToLower() == "true";
        else {
          return false; //no key is required.
        }
      }
    }

    /// <summary>
    /// The Log Directory
    /// </summary>
    public static string LogDirectory {
      get {
        if (Logging) {
          if (ConfigSection != null) {
            if (ConfigSection.LogDirectory.Length == 0) {
              throw new ConfigurationErrorsException(
                "Set the 'LogDirectory' key in the config section.");
            }
            return ConfigSection.LogDirectory;
          }
          else if (GetConfigValue("LogDirectory").Length > 0)
            return GetConfigValue("LogDirectory");
          else {
            throw new ConfigurationErrorsException(
              "Set the 'LogDirectory' key in the config file.");
          }
        }
        else {
          return string.Empty;
        }
      }
    }

    /// <summary>
    /// The Log Directory
    /// </summary>
    public static string LogDirectoryXml {
      get {
        if (Logging) {
          if (ConfigSection != null) {
            return ConfigSection.LogDirectoryXml;
          }
          else if (GetConfigValue("LogDirectoryXml").Length > 0)
            return GetConfigValue("LogDirectoryXml");
          else {
            Log.Debug("Set the 'LogDirectoryXml' key in the config file.");
          }
          return string.Empty;
        }
        else {
          return string.Empty;
        }
      }
    }

    /// <summary>
    /// Attempt to call the Currency method, but if a key is not found
    /// do not throw an exception, just return the default currency (USD)
    /// </summary>
    /// <remarks>Internally this is used to set the button and
    /// callback currency. It is designed so we do not change
    /// the default behavior of the application.</remarks>
    internal static string DefaultCurrency {
      get {
        try {
          return Currency;
        }
        catch (Exception) {
          return "USD";
        }
      }
    }

    /// <summary>
    /// The currency attribute identifies the unit of currency associated
    /// with the tag's value. The value of the currency attribute
    /// should be a three-letter ISO 4217 currency code.
    /// </summary>
    public static string Currency {
      get {
        if (ConfigSection != null) {
          if (ConfigSection.Currency.Length == 0) {
            throw new ConfigurationErrorsException(
              "Set the 'Currency' key in the config section.");
          }
          return ConfigSection.Currency;
        }
        else if (GetConfigValue("Currency").Length > 0)
          return GetConfigValue("Currency");
        else {
          throw new ConfigurationErrorsException(
            "Set the 'Currency' key in the config file.");
        }
      }
    }

    /// <summary>
    /// True or False if a Proxy Server should be used.
    /// </summary>
    /// <remarks>
    /// If True, the <see cref="ProxyHost"/> ,
    /// <see cref="ProxyUserName"/> and
    /// <see cref="ProxyPassword"/>
    /// (App Settings GoogleProxyHost,
    /// GoogleProxyUserName and
    /// GoogleProxyPassword)
    /// must be set.
    /// </remarks>
    public static bool UseProxy {
      get {
        if (ConfigSection != null) {
          return ConfigSection.UseProxy;
        }
        else if (GetConfigValue("GoogleUseProxy").Length > 0)
          return GetConfigValue("GoogleUseProxy").ToLower() == "true";
        else {
          return false;
        }
      }
    }

    /// <summary>
    /// The Proxy Server Host
    /// </summary>
    public static string ProxyHost {
      get {
        if (UseProxy) {
          if (ConfigSection != null) {
            if (ConfigSection.ProxyHost.Length == 0) {
              throw new ConfigurationErrorsException(
                "Set the 'ProxyHost' key in the config section.");
            }
            return ConfigSection.ProxyHost;
          }
          else if (GetConfigValue("GoogleProxyHost").Length > 0) {
            //what we are trying to do is give the user advanced warning
            //that the host is not valid.
            try {
              Uri proxyUrl = new Uri(GetConfigValue("GoogleProxyHost"));
            }
            catch (Exception ex) {
              throw new ConfigurationErrorsException("Error Reading GoogleProxyHost", ex);
            }
            return GetConfigValue("GoogleProxyHost");
          }
          else {
            throw new ConfigurationErrorsException(
              "Set the 'GoogleProxyHost' key in the config file.");
          }
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// The Proxy Server User Name
    /// </summary>
    public static string ProxyUserName {
      get {
        if (UseProxy) {
          if (ConfigSection != null) {
            //this is not required, so returning an empty string is valid
            return ConfigSection.ProxyUserName;
          }
          else if (GetConfigValue("GoogleProxyUserName").Length > 0)
            return GetConfigValue("GoogleProxyUserName");
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// The Proxy Server Password
    /// </summary>
    public static string ProxyPassword {
      get {
        if (UseProxy) {
          if (ConfigSection != null) {
            //this is not required, so returning an empty string is valid
            return ConfigSection.ProxyPassword;
          }
          else if (GetConfigValue("GoogleProxyPassword").Length > 0)
            return GetConfigValue("GoogleProxyPassword");
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// The Domain to authenticate the Proxy User
    /// </summary>
    public static string ProxyDomain {
      get {
        if (UseProxy) {
          if (ConfigSection != null) {
            //this is not required, so returning an empty string is valid
            return ConfigSection.ProxyDomain;
          }
          else if (GetConfigValue("GoogleProxyDomain").Length > 0)
            return GetConfigValue("GoogleProxyDomain");
        }
        return string.Empty;
      }
    }

    /// <summary>
    /// Create a new Configuration helper.
    /// </summary>
    public GCheckoutConfigurationHelper() {

    }

    /// <summary>
    /// Get the Configuration Key.
    /// </summary>
    /// <param name="key">The Key to obtain</param>
    /// <returns>Empty String or the value</returns>
    internal static string GetConfigValue(string key) {
      string retVal = ConfigurationManager.AppSettings[key];
      if (retVal != null)
        retVal = retVal.Trim();
      else
        retVal = string.Empty;

      return retVal;
    }

    /// <summary>
    /// Helper method for retrieving enum values from XmlNode.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="attribute"></param>
    /// <param name="enumType"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    internal static XmlNode GetEnumValue(XmlNode node, string attribute,
      Type enumType, ref int val) {
      XmlNode a = node.Attributes.RemoveNamedItem(attribute);
      if (a == null)
        throw new ConfigurationErrorsException("Google Checkout Config Section " +
          "attribute required: " + attribute);
      if (Enum.IsDefined(enumType, a.Value))
        val = (int)Enum.Parse(enumType, a.Value, true);
      else
        throw new ConfigurationErrorsException("Google Checkout Config Section " +
          "Invalid Enumeration Value: '" + a.Value + "'", a);
      return a;
    }

    /// <summary>
    /// Helper method for retrieving string values from xmlnode.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="attribute"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    internal static XmlNode GetStringValue(XmlNode node, string attribute,
      ref string val) {
      return GetStringValue(node, attribute, true, ref val);
    }

    /// <summary>
    /// Helper method for retrieving string values from xmlnode.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="attribute"></param>
    /// <param name="required"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    internal static XmlNode GetStringValue(XmlNode node, string attribute,
      bool required, ref string val) {
      XmlNode a = node.Attributes.RemoveNamedItem(attribute);
      if (a == null) {
        if (required)
          throw new ConfigurationErrorsException("Google Checkout Config Section " +
            "attribute required: " + attribute);
      }
      else
        val = a.Value;
      return a;
    }
  }
}
