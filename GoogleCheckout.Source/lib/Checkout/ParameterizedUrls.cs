/*************************************************
 * Copyright (C) 2007 Google Inc.
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
using System;
using System.Text;
using System.Collections;
using GCheckout.Util;
using System.Reflection;

namespace GCheckout.Checkout {

  /// <summary>
  /// The &lt;parameterized-urls&gt; tag
  /// </summary>
  /// <remarks>
  /// It contains information about all of the
  /// web beacons that the merchant wants to add to the Google Checkout order 
  /// confirmation page. This tag encapsulates a list of one or more
  /// &lt;parameterized-url&gt; (<see cref="ParameterizedUrl"/>) tags.
  /// See
  /// http://code.google.com/apis/checkout/developer/checkout_pixel_tracking.html
  /// For additional information on Third-Party Conversion Tracking
  /// </remarks>
  public class ParameterizedUrls {
    private ArrayList _ParameterizedUrl = new ArrayList();

    /// <summary>
    /// Return the <see cref="ParameterizedUrl" />[] that were added to the collection. 
    /// </summary>
    public ParameterizedUrl[] Urls {
      get {
        ParameterizedUrl[] retVal =
         new ParameterizedUrl[_ParameterizedUrl.Count];
        _ParameterizedUrl.CopyTo(retVal);
        return retVal;
      }
    }

    /// <summary>
    /// Add an already <see cref="System.Web.HttpUtility.UrlEncode(string)"/> Url
    /// </summary>
    /// <param name="url">The UrlEncoded &lt;parameterized-url&gt; to add to the collection</param>
    public ParameterizedUrl AddUrl(string url) {
      return AddUrl(url, false);
    }

    /// <summary>
    /// Add a Third Party Tracking URL.
    /// </summary>
    /// <param name="url">The &lt;parameterized-url&gt; to add to the collection</param>
    /// <param name="urlEncode">true if you need the url to be <see cref="System.Web.HttpUtility.UrlEncode(string)"/></param>
    /// <returns>A new <see cref="ParameterizedUrl" /></returns>
    public ParameterizedUrl AddUrl(string url, bool urlEncode) {
      if (urlEncode) {
        url = EncodeHelper.UrlEncodeUrl(url);
      }

      ParameterizedUrl retVal = new ParameterizedUrl(url);
      _ParameterizedUrl.Add(retVal);
      return retVal;
    }

    /// <summary>
    /// Add a Third Party Tracking URL.
    /// </summary>
    /// <param name="url">The &lt;parameterized-url&gt; object to add to the collection</param>
    /// <returns>A new <see cref="ParameterizedUrl" /></returns>
    public void AddUrl(ParameterizedUrl url) {
      _ParameterizedUrl.Add(url);
    }

  }

  /// <summary>
  /// The &lt;parameterized-url&gt; tag
  /// </summary>
  /// <remarks>It contains information about an
  /// individual web beacon that will be included on the Google Checkout
  /// order confirmation page.</remarks>
  public class ParameterizedUrl {

    private string _URL = string.Empty;

    /// <summary>
    /// The url used for the Third Party Tracking.
    /// </summary>
    public string URL {
      get {
        return _URL;
      }
    }

    private ArrayList _UrlParamters = new ArrayList();

    /// <summary>
    /// Return the <see cref="UrlParamter" />[] that were added to the collection. />
    /// </summary>
    public UrlParamter[] Params {
      get {
        UrlParamter[] retVal =
         new UrlParamter[_UrlParamters.Count];
        _UrlParamters.CopyTo(retVal);
        return retVal;
      }
    }

    /// <summary>
    /// Create a new &lt;parameterized-url&gt;
    /// </summary>
    /// <param name="url">The url used for the Third Party Tracking.</param>
    /// <returns></returns>
    public ParameterizedUrl(string url) {
      _URL = url;
    }

    /// <summary>
    /// Add a url parameter and the type to the collection.
    /// </summary>
    /// <remarks>The ParamName will automatically be URLEncoded since it should only be a string.</remarks>
    /// <param name="paramName">The url parameter to add</param>
    /// <param name="paramType">The <see cref="UrlParameterType"/> To add.</param>
    public void AddParameter(string paramName, UrlParameterType paramType) {
      _UrlParamters.Add(new UrlParamter(paramName, paramType));
    }
  }

  /// <summary>
  /// The Url Paramter class used in the <see cref="ParameterizedUrl"/>
  /// </summary>
  public class UrlParamter {

    private string _Name;

    /// <summary>
    /// The url parameter
    /// </summary>
    public string Name {
      get {
        return _Name;
      }
    }

    private UrlParameterType _ParamType = UrlParameterType.Unknown;

    /// <summary>
    /// The <see cref="UrlParameterType"/>
    /// </summary>
    public UrlParameterType ParamType {
      get {
        return _ParamType;
      }
    }

    /// <summary>
    /// The url parameter
    /// </summary>
    public string SerializedValue {
      get {
        Type t = ParamType.GetType();
        FieldInfo fi = t.GetField(ParamType.ToString());
        return EnumSerilizedNameAttribute.GetValue(fi);
      }
    }

    /// <summary>
    /// Create a new UrlParamer
    /// </summary>
    /// <param name="name">The name of the parameter</param>
    /// <param name="paramType">The <see cref="UrlParameterType"/></param>
    public UrlParamter(string name, UrlParameterType paramType) {
      if (name == null || name.Length == 0)
        throw new ArgumentException("name");
      if (paramType == UrlParameterType.Unknown)
        throw new ArgumentException("paramType", "The paramType must be defined as a valid type.");
      _Name = System.Web.HttpUtility.UrlEncode(name, Encoding.UTF8);
      _ParamType = paramType;
    }

  }
}
