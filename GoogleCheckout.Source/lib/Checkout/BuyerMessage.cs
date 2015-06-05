/*************************************************
 * Copyright (C) 2008-2010 Google Inc.
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
 *  5-22-2010   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 * 
*/
using System;
using System.Reflection;
using GCheckout.Util;

namespace GCheckout.Checkout {

  /// <summary>
  /// Class used to support the Shopping Cart Buyer Message property.
  /// </summary>
  public class BuyerMessage {

    private BuyerMessageType _messageType = BuyerMessageType.Unknown;
    private string _from;
    private int _maxchars;
    private int _maxlinelength;
    private int _maxlines;
    private string _to;
    private string _value;
    private bool _allowTo;
    private bool _allowFrom;

    /// <summary>
    /// The Message Type we wish to create
    /// </summary>
    public BuyerMessageType MessageType {
      get {
        return _messageType;
      }
      set {
        _messageType = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public string From {
      get {
        return _from;
      }
      set {
        if (value != null && value.Length > 100)
          throw new ArgumentOutOfRangeException("From", 
            "Maximum Length is 100");
        _from = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public bool AllowFrom {
      get {
        return _allowFrom;
      }
      set {
        _allowFrom = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public int MaximumCharacters {
      get {
        return _maxchars;
      }
      set {
        VerifyPositive("MaximumCharacters", value);
        if (value > 1000)
          throw new ArgumentOutOfRangeException("MaximumCharacters", 
            "Maximum Length is 1000");
        _maxchars = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public int MaxLineLength {
      get {
        return _maxlinelength;
      }
      set {
        VerifyPositive("MaxLineLength", value);
        if (value > 50)
          throw new ArgumentOutOfRangeException("MaxLineLength", 
            "Maximum Length is 50");
        _maxlinelength = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public int MaxLines {
      get {
        return _maxlines;
      }
      set {
        VerifyPositive("MaxLines", value);
        if (value > 20)
          throw new ArgumentOutOfRangeException("MaxLines", 
            "Maximum Length is 20");
        _maxlines = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public string To {
      get {
        return _to;
      }
      set {
        if (value != null && value.Length > 100)
          throw new ArgumentOutOfRangeException("From", 
            "Maximum Length is 100");
        _to = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public bool AllowTo {
      get {
        return _allowTo;
      }
      set {
        _allowTo = value;
      }
    }

    /// <summary>
    /// waiting for docs
    /// </summary>
    public string Value {
      get {
        return _value;
      }
      set {
        _value = value;
      }
    }

    /// <summary>
    /// Create a new instance of the buyer message.
    /// </summary>
    public BuyerMessage() {
    
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="to">The To Name</param>
    /// <param name="from">The From Name</param>
    /// <param name="messageType">The type of message to create.</param>
    public BuyerMessage(string to, string from, 
      BuyerMessageType messageType) {
      From = from;
      To = to;
      MessageType = messageType;
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="to">The To Name</param>
    /// <param name="from">The From Name</param>
    /// <param name="message">The initial message</param>
    /// <param name="messageType">The type of message to create.</param>
    public BuyerMessage(string to, string from,  string message,
      BuyerMessageType messageType) {
      From = from;
      To = to;
      Value = message;
      MessageType = messageType;
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="to">The To Name</param>
    /// <param name="from">The From Name</param>
    /// <param name="message">The Initial Message</param>
    /// <param name="messageType">The type of message to create.</param>
    /// <param name="maximumCharacters">The maximum number of characters</param>
    /// <param name="maxLineLength">The maximum line length</param>
    /// <param name="maxLines">The maximum number of lines</param>
    public BuyerMessage(string to, string from, string message,
      BuyerMessageType messageType,
      int maximumCharacters, int maxLineLength, int maxLines) {
      From = from;
      To = to;
      Value = message;
      MessageType = messageType;
      MaximumCharacters = maximumCharacters;
      MaxLineLength = maxLineLength;
      MaxLines = maxLines;
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="allowTo">The To Name</param>
    /// <param name="allowFrom">The From Name</param>
    /// <param name="messageType">The type of message to create.</param>
    public BuyerMessage(bool allowTo, bool allowFrom, BuyerMessageType messageType) {
      AllowFrom = allowFrom;
      AllowTo = allowTo;
      MessageType = messageType;
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="allowTo">The To Name</param>
    /// <param name="allowFrom">The From Name</param>
    /// <param name="message">The initial message</param>
    /// <param name="messageType">The type of message to create.</param>
    public BuyerMessage(bool allowTo, bool allowFrom, string message,
      BuyerMessageType messageType) {
      AllowFrom = allowFrom;
      AllowTo = allowTo;
      Value = message;
      MessageType = messageType;
    }

    /// <summary>
    /// Create a new buyer message passing in the from, to and the message type.
    /// </summary>
    /// <param name="allowTo">The To Name</param>
    /// <param name="allowFrom">The From Name</param>
    /// <param name="message">The initial message</param>
    /// <param name="messageType">The type of message to create.</param>
    /// <param name="maximumCharacters">The maximum number of characters</param>
    /// <param name="maxLineLength">The maximum line length</param>
    /// <param name="maxLines">The maximum number of lines</param>
    public BuyerMessage(bool allowTo, bool allowFrom, string message,
      BuyerMessageType messageType,
      int maximumCharacters, int maxLineLength, int maxLines) {
      AllowFrom = allowFrom;
      AllowTo = allowTo;
      Value = message;
      MessageType = messageType;
      MaximumCharacters = maximumCharacters;
      MaxLineLength = maxLineLength;
      MaxLines = maxLines;
    }

    /// <summary>
    /// Return the Message expected by the 
    /// </summary>
    /// <returns></returns>
    public object GetXSDMessage() {
      if (MessageType == BuyerMessageType.Unknown)
        return null;

      Type theType = Type.GetType("GCheckout.AutoGen." + MessageType.ToString());

      object retVal = Activator.CreateInstance(theType);

      if (retVal != null) {

        if (ReflectionHelper.FieldExists(retVal, "from")) {
          if (From != null && From.Length > 0){
            ReflectionHelper.SetFieldValue("from", false, retVal, From);
          }
          else if (AllowFrom) {
            ReflectionHelper.SetFieldValue("from", false, retVal, string.Empty);
          }
        }
        if (MaximumCharacters > 0) {
          ReflectionHelper.SetFieldValue("maxchars", true, retVal, MaximumCharacters);
        }
        if (MaxLineLength > 0) {
          ReflectionHelper.SetFieldValue("maxlinelength", true, retVal, MaxLineLength);
        }
        if (MaxLines > 0) {
          ReflectionHelper.SetFieldValue("maxlines", true, retVal, MaxLines);
        }

        if (ReflectionHelper.FieldExists(retVal, "to")) {
          if (To != null && To.Length > 0){
            ReflectionHelper.SetFieldValue("to", false, retVal, To);
          }
          else if (AllowTo) {
            ReflectionHelper.SetFieldValue("to", false, retVal, string.Empty);
          }
        }

        if (Value != null && Value.Length > 0) {
          ReflectionHelper.SetFieldValue("Value", false, retVal, Value);
        }

      }
      else {
        throw new ApplicationException(
          string.Format("BuyerMessage unable to create {0} message.", 
          MessageType));
      }
      return retVal;
    }

   

    private void VerifyPositive(string param, int value) {
      if (value < 0)
        throw new ArgumentOutOfRangeException(
          param, value, "Value must be positive");
    }

  }
}
