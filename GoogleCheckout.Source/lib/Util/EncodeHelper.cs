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

using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace GCheckout.Util {
  
  /// <summary>
  /// Collection of various encode-related static methods.
  /// </summary>
  public sealed class EncodeHelper {

    /// <summary>
    /// Converts a string to bytes in UTF-8 encoding.
    /// </summary>
    /// <param name="InString">The string to convert.</param>
    /// <returns>The UTF-8 bytes.</returns>
    public static byte[] StringToUtf8Bytes(string InString) {
      UTF8Encoding utf8encoder = new UTF8Encoding(false, true);
      return utf8encoder.GetBytes(InString);
    }

    /// <summary>
    /// Converts bytes in UTF-8 encoding to a regular string.
    /// </summary>
    /// <param name="InBytes">The UTF-8 bytes.</param>
    /// <returns>The input bytes as a string.</returns>
    public static string Utf8BytesToString(byte[] InBytes) {
      UTF8Encoding utf8encoder = new UTF8Encoding(false, true);
      return utf8encoder.GetString(InBytes);
    }

    /// <summary>
    /// Converts UTF8-encoded bytes from a stream to a string.
    /// </summary>
    /// <param name="Utf8Stream">The UTF8 stream.</param>
    /// <returns>
    /// The full stream contents as a string. Also closes the stream.
    /// </returns>
    public static string Utf8StreamToString(Stream Utf8Stream) {
      using (StreamReader SReader = 
               new StreamReader(Utf8Stream, Encoding.UTF8)) {
        return SReader.ReadToEnd();
      }
    }

    /// <summary>
    /// Gets the top element of an XML string.
    /// </summary>
    /// <param name="Xml">
    /// The XML string to extract the top element from.
    /// </param>
    /// <returns>
    /// The name of the first regular XML element.
    /// </returns>
    /// <example>
    /// Calling GetTopElement(Xml) where Xml is:
    /// <code>
    /// &lt;?xml version="1.0" encoding="UTF-8"?&gt;
    /// &lt;new-order-notification xmlns="http://checkout.google.com/schema/2"
    /// serial-number="85f54628-538a-44fc-8605-ae62364f6c71"&gt;
    /// &lt;google-order-number&gt;841171949013218&lt;/google-order-number&gt;
    /// ...
    /// &lt;new-order-notification&gt;
    /// </code>
    /// will return the string <b>new-order-notification</b>.
    /// </example>
    public static string GetTopElement(byte[] Xml) {
      using (MemoryStream ms = new MemoryStream(Xml)) {
        return GetTopElement(ms);
      }
    }

    /// <summary>
    /// Gets the top element of an XML string.
    /// </summary>
    /// <param name="Xml">
    /// The XML string to extract the top element from.
    /// </param>
    /// <returns>
    /// The name of the first regular XML element.
    /// </returns>
    /// <example>
    /// Calling GetTopElement(Xml) where Xml is:
    /// <code>
    /// &lt;?xml version="1.0" encoding="UTF-8"?&gt;
    /// &lt;new-order-notification xmlns="http://checkout.google.com/schema/2"
    /// serial-number="85f54628-538a-44fc-8605-ae62364f6c71"&gt;
    /// &lt;google-order-number&gt;841171949013218&lt;/google-order-number&gt;
    /// ...
    /// &lt;new-order-notification&gt;
    /// </code>
    /// will return the string <b>new-order-notification</b>.
    /// </example>
    public static string GetTopElement(string Xml) {
      return GetTopElement(StringToUtf8Bytes(Xml));
    }

    /// <summary>
    /// Gets the top element of an XML string.
    /// </summary>
    /// <param name="Xml">
    /// The XML string to extract the top element from.
    /// </param>
    /// <returns>
    /// The name of the first regular XML element.
    /// </returns>
    /// <example>
    /// Calling GetTopElement(Xml) where Xml is:
    /// <code>
    /// &lt;?xml version="1.0" encoding="UTF-8"?&gt;
    /// &lt;new-order-notification xmlns="http://checkout.google.com/schema/2"
    /// serial-number="85f54628-538a-44fc-8605-ae62364f6c71"&gt;
    /// &lt;google-order-number&gt;841171949013218&lt;/google-order-number&gt;
    /// ...
    /// &lt;new-order-notification&gt;
    /// </code>
    /// will return the string <b>new-order-notification</b>.
    /// </example>
    public static string GetTopElement(Stream Xml) {
      //we know that network streams can have issues so we may want to add
      //code to see if someone passed in a network stream
      //For now we are going to run unit tests and see if any issues come back

      //set the begin postion so we can set the stream back when we are done.
      long beginPos = Xml.Position;
      Xml.Position = 0;
      XmlTextReader XReader = new XmlTextReader(Xml);
      XReader.WhitespaceHandling = WhitespaceHandling.None;
      XReader.Read();
      XReader.Read();
      string RetVal = XReader.Name;
      //Do not close the stream, we will still need it for additional
      //operations.
      //XReader.Close();
      //reposition the stream to where it started
      Xml.Position = beginPos;
      return RetVal;
    }

    /// <summary>
    /// Gets the value of the first element or attribute in a piece of XML.
    /// </summary>
    /// <param name="Xml">
    /// The XML to extract the element or attribute value from.
    /// </param>
    /// <param name="Element">
    /// Name of the element or attribute to search for.
    /// </param>
    /// <returns>
    /// The value of the first element or attribute. If there is no such
    /// element or attribute in the XML, an empty string is returned.
    /// </returns>
    /// <example>
    /// Assume the string variable Xml has this value:
    /// <code>
    /// &lt;?xml version="1.0" encoding="UTF-8"?&gt;
    /// &lt;new-order-notification xmlns="http://checkout.google.com/schema/2"
    /// serial-number="85f54628-538a-44fc-8605-ae62364f6c71"&gt;
    /// &lt;google-order-number&gt;841171949013218&lt;/google-order-number&gt;
    /// ...
    /// &lt;/new-order-notification&gt;
    /// </code>
    /// Calling <b>GetElementValue(Xml, "google-order-number")</b> will return 
    /// <b>841171949013218</b>. Calling 
    /// <b>GetElementValue(Xml, "serial-number")</b> will return 
    /// <b>85f54628-538a-44fc-8605-ae62364f6c71</b>.
    /// </example>
    public static string GetElementValue(string Xml, string Element) {
      string RetVal = string.Empty;
      using (StringReader SReader = new StringReader(Xml)) {
        XmlTextReader XReader = new XmlTextReader(SReader);
        XReader.WhitespaceHandling = WhitespaceHandling.None;
        XReader.Read();
        while (!XReader.EOF) {
          if (XReader.Name == Element) {
            XReader.Read();
            RetVal = XReader.Value;
            break;
          }
          if (XReader.MoveToAttribute(Element)) {
            RetVal = XReader.Value;
            break;
          }
          XReader.Read();
        }
      }
      return RetVal;
    }

    /// <summary>
    /// Makes an object out of the specified XML.
    /// </summary>
    /// <param name="Xml">The XML that should be made into an object.</param>
    /// <param name="ThisType">
    /// Type of object that produced the XML. 
    /// </param>
    /// <returns>The reconstituted object.</returns>
    /// <example>
    /// <code>
    /// Car MyCar1 = new Car();
    /// byte[] CarBytes = EncodeHelper.Serialize(MyCar1);
    /// string CarXml = EncodeHelper.Utf8BytesToString(CarBytes);
    /// Car MyCar2 = (Car) Deserialize(CarXml, typeof(Car));
    /// // MyCar2 is now a copy of MyCar1.
    /// </code>
    /// </example>
    public static object Deserialize(string Xml, Type ThisType) {
      XmlSerializer myDeserializer = new XmlSerializer(ThisType);

      try {
        using (StringReader myReader = new StringReader(Xml)) {
          return myDeserializer.Deserialize(myReader);
        }
      }
      catch (Exception e) {
        //we need to find out what happened. 
        //for now we are going to wrap the exception.
        //if we have a bom, we may have a config issue with the server
        //or Google Checkout is sending a BOM.
        bool containsBom = false;
        if (Xml != null) {
          if (Xml.StartsWith(Encoding.UTF8.GetString( 
            Encoding.UTF8.GetPreamble()))) {
            containsBom = true;
          }
        }
        throw new ApplicationException(
          string.Format("Couldn't parse XML: '{0}'; Contains BOM: {1}; Type: {2}.", 
          Xml, containsBom, ThisType.FullName), e);
      }
    }

    /// <summary>
    /// Generic Deserialize method that will attempt to deserialize any class
    /// in the GCheckout.AutoGen namespace.
    /// </summary>
    /// <param name="Xml">The XML that should be made into an object.</param>
    /// <returns>The reconstituted object.</returns>
    public static object Deserialize(byte[] Xml) { 
      using (MemoryStream ms = new MemoryStream(Xml)) {
        return Deserialize(ms);
      }
    }

    /// <summary>
    /// Generic Deserialize method that will attempt to deserialize any class
    /// in the GCheckout.AutoGen namespace.
    /// </summary>
    /// <param name="Xml">The XML that should be made into an object.</param>
    /// <returns>The reconstituted object.</returns>
    public static object Deserialize(string Xml) {
      byte[] newXml = StringToUtf8Bytes(Xml);
      return Deserialize(newXml);
    }

    /// <summary>
    /// Generic Deserialize method that will attempt to deserialize any class
    /// in the GCheckout.AutoGen namespace.
    /// </summary>
    /// <param name="Xml">The XML that should be made into an object.</param>
    /// <returns>The reconstituted object.</returns>
    public static object Deserialize(Stream Xml) {

      long originalPos = Xml.Position;
      //Keep the XmlReader from closing the stream on a failure.
      StreamWrapper wrap = new StreamWrapper(Xml);
      wrap.Position = 0;

      try {
        //get the name of the node, this is what we will use for the lookup.
        string nodeName = GetTopElement(wrap);
        
        string nameSpace = typeof(AutoGen.Hello).Namespace;
        Type[] types
          = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        Type reflectedType = GetReflectedType(types, nameSpace, nodeName);

        //ok either the type is supported or it is not
        //if we could not find the correct type then we must have either
        //an incorrect dll for the message, or the message
        //is not a Google Checkout message. Return null if the type
        //can't be deserialized.
        if (reflectedType != null) {
          XmlSerializer myDeserializer = new XmlSerializer(reflectedType);
          object retVal = myDeserializer.Deserialize(wrap);
          wrap.Position = originalPos;
          return retVal;
        }
        else {
          System.Diagnostics.Debug.WriteLine("Deserialize was not able" +
            "To locate a message of type:" + nodeName + "");
          wrap.Position = originalPos;
          return null;
        }
      }
      catch (Exception e) {
        wrap.Position = 0;
        string passedXml;
        using (StreamReader reader = new StreamReader(wrap)) {
          passedXml = reader.ReadToEnd();
        }

        bool containsBom = false;
        if (wrap != null && wrap.Length >= 3) {
          byte[] theBom = Encoding.UTF8.GetPreamble();
          wrap.Position = 0;
          //try to determine we have a bom in the message,
          //which may cause the deserializer to fail.
          if (wrap.ReadByte() == theBom[0]
            && wrap.ReadByte() == theBom[1]
            && wrap.ReadByte() == theBom[2]) {
            containsBom = true;
          }
        }

        wrap.Position = originalPos;

        throw new ApplicationException(
          string.Format("Couldn't parse XML: '{0}'; Contains BOM: {1}.", 
          passedXml, containsBom), e);
      }
    }

    private static Type GetReflectedType(Type[] types, string ns, string nodeName) {
      foreach(Type t in types) {
        if (t.Namespace == ns && t.IsClass) {
          //Console.WriteLine(t.Name);
          XmlRootAttribute[] att 
            = t.GetCustomAttributes(typeof(XmlRootAttribute), true)
            as XmlRootAttribute[];
          //if we found the custom attribute, then we need to see
          //if the element name is correct.
          if (att != null && att.Length > 0) {
            if (att[0].ElementName == nodeName) {
              return t;
            }
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Makes XML out of an object.
    /// </summary>
    /// <param name="ObjectToSerialize">The object to serialize.</param>
    /// <returns>An XML string representing the object.</returns>
    /// <example>
    /// <code>
    /// Car MyCar1 = new Car();
    /// byte[] CarBytes = EncodeHelper.Serialize(MyCar1);
    /// string CarXml = EncodeHelper.Utf8BytesToString(CarBytes);
    /// Car MyCar2 = (Car) Deserialize(CarXml, typeof(Car));
    /// // MyCar2 is now a copy of MyCar1.
    /// </code>
    /// </example>
    public static byte[] Serialize(object ObjectToSerialize) {
      XmlSerializer Ser = new XmlSerializer(ObjectToSerialize.GetType());
      using (MemoryStream MS = new MemoryStream()) {
        XmlTextWriter W = new XmlTextWriter(MS, new UTF8Encoding(false));
        W.Formatting = Formatting.Indented;
        Ser.Serialize(W, ObjectToSerialize);
        W.Flush();
        W.Close();
        return MS.ToArray();
      }
    }

    /// <summary>
    /// Escapes XML characters &lt; &gt; and &amp;.
    /// </summary>
    /// <param name="In">
    /// String that could contain &lt; &gt; and &amp; characters.
    /// </param>
    /// <returns>
    /// A new string where 
    /// <b>&amp;</b> has been replaced by <b>&amp;#x26;</b>,
    /// <b>&lt;</b> has been replaced by <b>&amp;#x3c;</b> and
    /// <b>&gt;</b> has been replaced by <b>&amp;#x3e;</b>.
    /// These replacements are mandated here in the Dev Guide:
    /// <a href="http://code.google.com/apis/checkout/developer/index.html#api_request_guidelines">
    /// http://code.google.com/apis/checkout/developer/index.html#api_request_guidelines
    /// </a>
    /// </returns>
    public static string EscapeXmlChars(string In) {
      string RetVal = In;
      RetVal = RetVal.Replace("&", "&#x26;");
      RetVal = RetVal.Replace("<", "&#x3c;");
      RetVal = RetVal.Replace(">", "&#x3e;");
      return RetVal;
    }

    /// <summary>
    /// Get the Cart Signature used in the &quot;signature&quot; 
    /// form field that is posted to Google Checkout.
    /// </summary>
    /// <param name="cartXML">A string version of the Cart Xml returned from the
    /// <see cref="GCheckout.Checkout.CheckoutShoppingCartRequest.GetXml"/>
    /// method.</param>
    /// <param name="merchantKey">Your Google Merchant Key</param>
    /// <returns>A Base64 encoded string of the cart signature</returns>
    public static string GetCartSignature(string cartXML, string merchantKey) {
      System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
      byte[] cart = encoding.GetBytes(cartXML);
      return GetCartSignature(cart, merchantKey);
    }

    /// <summary>
    /// Get the Cart Signature used in the &quot;signature&quot;
    /// form field that is posted to Google Checkout.
    /// </summary>
    /// <param name="cart">The Cart Xml returned from the 
    /// <see cref="GCheckout.Checkout.CheckoutShoppingCartRequest.GetXml"/>
    /// method.</param>
    /// <param name="merchantKey">Your Google Merchant Key</param>
    /// <returns>A Base64 encoded string of the cart signature</returns>
    public static string GetCartSignature(byte[] cart, string merchantKey) {

      System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
      byte[] key = encoding.GetBytes(merchantKey);

      using (System.Security.Cryptography.HMACSHA1 cryptobj = new
               System.Security.Cryptography.HMACSHA1(key)) {

        string retVal = 
          System.Convert.ToBase64String(cryptobj.ComputeHash(cart));

        cryptobj.Clear();
        return retVal;
      }
    }

    /// <summary>
    /// Encode a url that is not already encoded
    /// </summary>
    /// <param name="url">The URL to encode.</param>
    /// <returns></returns>
    internal static string UrlEncodeUrl(string url) {
      Uri uri = new Uri(url);
      if (!uri.UserEscaped && uri.Query != null && uri.Query.Length > 1) {
        StringBuilder sb = new StringBuilder(url.Length);
        sb.Append(uri.Scheme).Append(":").Append("//");
        sb.Append(uri.Host);
        if (!uri.IsDefaultPort) {
          sb.Append(":").Append(uri.Port);
        }
        sb.Append(uri.AbsolutePath).Append("?");

        Encoding utf = Encoding.UTF8;
 
        string[] nameValue = uri.Query.Substring(1).Split('&');

        if (nameValue != null && nameValue.Length > 1) {
          for (int i = 0; i < nameValue.Length; i++) {
            string[] parts = nameValue[i].Split('=');
            sb.Append(parts[0]);
            sb.Append("=");
            sb.Append(System.Web.HttpUtility.UrlEncode(parts[1], utf));
            if (i + 1 < nameValue.Length) {
              sb.Append("&amp;");
            }
          }

          url = sb.ToString();
        }      
      }

      return url;
    }

    /// <summary>
    /// Crean a string and return an String.Empty or a trimmed string.
    /// </summary>
    /// <param name="val">The string to Trim</param>
    /// <returns></returns>
    internal static string CleanString(string val) {
      string retVal = val;
      if (retVal == null)
        retVal = string.Empty;
      retVal = retVal.Trim();
      return retVal;
    }

    /// <summary>
    /// Create a Money Object given the decimal value
    /// </summary>
    /// <remarks>The Currency Must be specified</remarks>
    /// <param name="value">The Value</param>
    /// <returns></returns>
    public static AutoGen.Money Money(decimal value) {
      value = Math.Round(value, 2); //fix for sending in fractional cents
      return Money(GCheckout.Util.GCheckoutConfigurationHelper.Currency, value);
    }

    /// <summary>
    /// Create a Money Object given the decimal value
    /// </summary>
    /// <remarks>The Currency Must be specified</remarks>
    /// <param name="currency">The Currency</param>
    /// <param name="value">The Value</param>
    /// <returns></returns>
    public static AutoGen.Money Money(string currency, decimal value) {
      value = Math.Round(value, 2); //fix for sending in fractional cents
      AutoGen.Money retval = new GCheckout.AutoGen.Money();
      retval.currency = currency;
      retval.Value = value;
      return retval;
    }
  }
}
