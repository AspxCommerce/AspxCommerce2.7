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
using System.Configuration;
using System.Web;
using System.Xml;

namespace GCheckout.Util {
  /// <summary>
  /// Google Checkout Config Section.
  /// </summary>
  /// <remarks>This will replace the AppSettings implementation.</remarks>
  public class GCheckoutConfigSection {
    private EnvironmentType _environment = EnvironmentType.Unknown;

    /// <summary>
    /// The Environment
    /// </summary>
    public virtual EnvironmentType Environment {
      get {
        return _environment;
      }
      set {
        _environment = value;
      }
    }

    private long _productionMerchantID;

    /// <summary>
    /// The Production Merchant ID
    /// </summary>
    public virtual long ProductionMerchantID {
      get {
        return _productionMerchantID;
      }
      set {
        if (value > 0)
          _productionMerchantID = value; 
      }
    }

    private string _productionMerchantKey;

    /// <summary>
    /// The Production Merchant Key
    /// </summary>
    public virtual string ProductionMerchantKey {
      get {
        return _productionMerchantKey;
      }
      set {
        if (value != null && value.Length > 0)
          _productionMerchantKey = value.Trim(); 
      }
    }

    private long _sandboxMerchantID;

    /// <summary>
    /// The Sandbox Merchant ID
    /// </summary>
    public virtual long SandboxMerchantID {
      get {
        return _sandboxMerchantID;
      }
      set {
        if (value > 0)
          _sandboxMerchantID = value; 
      }
    }

    private string _sandboxMerchantKey;

    /// <summary>
    /// The Sandbox Merchant Key
    /// </summary>
    public virtual string SandboxMerchantKey {
      get {
        return _sandboxMerchantKey;
      }
      set {
        if (value != null && value.Length > 0)
          _sandboxMerchantKey = value.Trim(); 
      }
    }

    /// <summary>
    /// The Current Merchant ID
    /// </summary>
    public virtual long MerchantID {
      get {
        if (Environment == EnvironmentType.Sandbox)
          return _sandboxMerchantID;
        else if (Environment == EnvironmentType.Production)
          return _productionMerchantID;
        else {
          throw new ConfigurationErrorsException("Environment Must be set.");   
        }
      }
    }

    /// <summary>
    /// The Current Merchant Key
    /// </summary>
    public virtual string MerchantKey {
      get {
        if (Environment == EnvironmentType.Sandbox)
          return _sandboxMerchantKey;
        else if (Environment == EnvironmentType.Production)
          return _productionMerchantKey;
        else {
          throw new ConfigurationErrorsException("Environment Must be set.");   
        }
      }
    }

    private long _platformID;

    /// <summary>
    /// The &lt;platform-id&gt; tag should only be used by eCommerce providers who 
    /// make API requests on behalf of a merchant. The tag's value contains 
    /// a Google Checkout merchant ID that identifies the eCommerce provider.
    /// </summary>
    /// <remarks>
    /// <seealso href="http://code.google.com/apis/checkout/developer/index.html#tag_platform-id"/>
    /// </remarks>
    public virtual long PlatformID {
      get {
        return _platformID;
      }
      set {
        if (value > 0)
          _platformID = value;
      }
    }

    private bool _logging = false;

    /// <summary>
    /// Enable Logging
    /// </summary>
    public virtual bool Logging {
      get {
        return _logging;
      }
      set {
        _logging = value; 
      }
    }

    private string _logDirectory;

    /// <summary>
    /// The Log Directory
    /// </summary>
    public virtual string LogDirectory {
      get {
        return _logDirectory;
      }
      set {
        if (value != null && value.Length > 0)
          _logDirectory = value.Trim(); 
      }
    }

    private string _logDirectoryXml;

    /// <summary>
    /// The Log Directory For Xml Files
    /// </summary>
    public virtual string LogDirectoryXml {
      get {
        return _logDirectoryXml;
      }
      set {
        if (value != null && value.Length > 0)
          _logDirectoryXml = value.Trim();
      }
    }

    private string _currency = string.Empty;

    /// <summary>
    /// The currency attribute identifies the unit of currency associated 
    /// with the tag's value. The value of the currency attribute 
    /// should be a three-letter ISO 4217 currency code.
    /// </summary>
    public virtual string Currency {
      get {
        return _currency;
      }
      set {
        if (value != null && value.Length > 0)
          _currency = value.Trim();
      }
    }

    private bool _useProxy = false;

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
    public virtual bool UseProxy {
      get {
        return _useProxy;
      }
      set {
        _useProxy = value;
      }
    }

    private string _proxyHost = string.Empty;

    /// <summary>
    /// The Proxy Server Host
    /// </summary>
    public virtual string ProxyHost {
      get {
        return _proxyHost;
      }
      set {
        if (value != null && value.Length > 0)
          _proxyHost = value.Trim();
      }
    }

    private string _proxyUserName = string.Empty;

    /// <summary>
    /// The Proxy Server User Name
    /// </summary>
    public virtual string ProxyUserName {
      get {
        return _proxyUserName;
      }
      set {
        if (value != null && value.Length > 0)
          _proxyUserName = value.Trim();
      }
    }

    private string _proxyPassword = string.Empty;

    /// <summary>
    /// The Proxy Server Password
    /// </summary>
    public virtual string ProxyPassword {
      get {
        return _proxyPassword;
      }
      set {
        if (value != null && value.Length > 0)
          _proxyPassword = value.Trim();
      }
    }

    private string _proxyDomain = string.Empty;

    /// <summary>
    /// The Domain to authenticate the Proxy User
    /// </summary>
    public virtual string ProxyDomain {
      get {
        return _proxyDomain;
      }
      set {
        if (value != null && value.Length > 0)
          _proxyDomain = value.Trim();
      }
    }

    /// <summary>
    /// Create a New Config Section
    /// </summary>
    public GCheckoutConfigSection() {

    }

  }
}
