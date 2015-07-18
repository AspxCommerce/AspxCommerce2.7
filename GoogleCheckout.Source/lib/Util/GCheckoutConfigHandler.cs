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
using System.Web;
using System.Xml;
using System.Configuration;

namespace GCheckout.Util {
  /// <summary>
  /// Summary description for GCheckoutConfigHandler.
  /// </summary>
  public class GCheckoutConfigHandler : IConfigurationSectionHandler {

    /// <summary>
    /// Create a new instance of the Config Handler
    /// </summary>
    public GCheckoutConfigHandler() {

    }

    #region IConfigurationSectionHandler Members

    /// <summary>
    /// Create the application Context
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="configContext"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    public object Create(object parent, object configContext,
      XmlNode section) {

      GCheckoutConfigSection retVal = new GCheckoutConfigSection();

      string productionMerchantID = string.Empty;
      string productionMerchantKey = string.Empty;
      string sandboxMerchantID = string.Empty;
      string sandboxMerchantKey = string.Empty;
      string currency = string.Empty;
      long platformID = 0;
      int envTemp = 0;
      bool useProxy = false;
      string proxyHost = string.Empty;
      string proxyUserName = string.Empty;
      string proxyPassword = string.Empty;
      string proxyDomain = string.Empty;

      EnvironmentType environment = EnvironmentType.Unknown;

      bool logging = false;
      string logTemp = string.Empty;
      string platformIDTemp = string.Empty;

      string logDirectory = string.Empty;
      string logDirectoryXml = string.Empty;

      GCheckoutConfigurationHelper.GetStringValue(
        section, "SandboxMerchantID", ref sandboxMerchantID);
      GCheckoutConfigurationHelper.GetStringValue(
        section, "SandboxMerchantKey", ref sandboxMerchantKey);
      GCheckoutConfigurationHelper.GetStringValue(
        section, "ProductionMerchantID", ref productionMerchantID);
      GCheckoutConfigurationHelper.GetStringValue(
        section, "ProductionMerchantKey", ref productionMerchantKey);
      GCheckoutConfigurationHelper.GetStringValue(
        section, "Currency", false, ref currency);

      GCheckoutConfigurationHelper.GetEnumValue(
        section, "Environment", typeof(EnvironmentType), ref envTemp);
      environment = (EnvironmentType)envTemp;

      GCheckoutConfigurationHelper.GetStringValue(
        section, "Logging", false, ref logTemp);
      if (logTemp != null && logTemp.Length > 0) {
        logging = logTemp.ToLower() == "true";
      }

      //ensure we do not throw an exception.
      try {
        GCheckoutConfigurationHelper.GetStringValue(
          section, "PlatformID", false, ref platformIDTemp);
        if (platformIDTemp != null && platformIDTemp.Length > 0) {
          platformID = long.Parse(platformIDTemp);
        }
      }
      catch (Exception ex) {
        throw new ConfigurationErrorsException(
          "Error Setting PlatformID", ex);
      }

      GCheckoutConfigurationHelper.GetStringValue(
        section, "LogDirectory", false, ref logDirectory);

      GCheckoutConfigurationHelper.GetStringValue(
        section, "LogDirectoryXml", false, ref logDirectoryXml);


      try {
        retVal.ProductionMerchantID = long.Parse(productionMerchantID);
      }
      catch (Exception ex) {
        throw new ConfigurationErrorsException(
          "Error Setting ProductionMerchantID", ex);
      }
      retVal.ProductionMerchantKey = productionMerchantKey;

      try {
        retVal.SandboxMerchantID = long.Parse(sandboxMerchantID);
      }
      catch (Exception ex) {
        throw new ConfigurationErrorsException(
          "Error Setting SandboxMerchantID", ex);
      }

      //try to read the UseProxy Key.
      try {
        string useProxyVal = null;
        GCheckoutConfigurationHelper.GetStringValue(
          section, "UseProxy", false, ref useProxyVal);
        if (useProxyVal != null && useProxyVal.Length > 0) {
          useProxy = bool.Parse(useProxyVal);
        }
      }
      catch (Exception ex) {
        throw new ConfigurationErrorsException(
          "Error Setting UseProxy", ex);
      }

      //if we have the key, then attempt to read the other values
      //if true then all other values are required
      //someday someone may request us to support a blank password, 
      //but we are not going to support that
      //since it is a security issue.
      if (useProxy) {
        GCheckoutConfigurationHelper.GetStringValue(
          section, "ProxyHost", ref proxyHost);

        //what we are going to do is attempt to validate the uri of the proxy
        if (proxyHost != string.Empty) {
          try {
            Uri proxyUrl = new Uri(proxyHost);
          }
          catch (Exception ex) {
            throw new ConfigurationErrorsException("Error Setting ProxyHost", ex);
          }
        }

        GCheckoutConfigurationHelper.GetStringValue(
          section, "ProxyUserName", false, ref proxyUserName);
        GCheckoutConfigurationHelper.GetStringValue(
          section, "ProxyPassword", false, ref proxyPassword);
        GCheckoutConfigurationHelper.GetStringValue(
          section, "ProxyDomain", false, ref proxyDomain);
      }

      retVal.SandboxMerchantKey = sandboxMerchantKey;
      retVal.Environment = environment;
      retVal.Logging = logging;
      retVal.LogDirectory = logDirectory;
      retVal.LogDirectoryXml = logDirectoryXml;
      retVal.PlatformID = platformID;
      retVal.Currency = currency;
      retVal.UseProxy = useProxy;
      retVal.ProxyHost = proxyHost;
      retVal.ProxyUserName = proxyUserName;
      retVal.ProxyPassword = proxyPassword;
      retVal.ProxyDomain = proxyDomain;

      return retVal;
    }

    #endregion
  }
}
