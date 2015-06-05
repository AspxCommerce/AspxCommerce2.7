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
using System.Collections;

namespace GCheckout.Checkout {

  /// <summary>
  /// Class used to allow Buyer Messages to be appended to the shopping cart
  /// </summary>
  public class BuyerMessages {
    
    private bool _includeGiftReceipt = false;
    private ArrayList _messages = new ArrayList();

    /// <summary>
    /// Create a new instance of the BuyerMessages
    /// </summary>
    public BuyerMessages() {
    
    }

    ///<remarks></remarks>
    public void AddInTributeOfMessage(string to, string from, string message) {
      _messages.Add(new BuyerMessage(to, from, message,
        BuyerMessageType.InTributeOfMessage));
    }

    ///<remarks></remarks>
    public void AddInTributeOfMessage(bool allowTo, bool allowFrom, 
      string message) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InTributeOfMessage));
    }

    ///<remarks></remarks>
    public void AddInTributeOfMessage(string to, string from, string message,
      int maximumCharacters, int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(to, from, message, 
        BuyerMessageType.InTributeOfMessage, maximumCharacters, maxLineLength,
        maxLines));

    }

    ///<remarks></remarks>
    public void AddInTributeOfMessage(bool allowTo, bool allowFrom, 
      string message,
      int maximumCharacters, int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InTributeOfMessage, maximumCharacters, maxLineLength,
        maxLines));
    }

    ///<remarks></remarks>
    public void AddInHonorOfMessage(string to, string from, string message) {
      _messages.Add(new BuyerMessage(to, from, message,
        BuyerMessageType.InHonorOfMessage));
    }

    ///<remarks></remarks>
    public void AddInHonorOfMessage(bool allowTo, bool allowFrom, 
      string message) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InHonorOfMessage));
    }

    ///<remarks></remarks>
    public void AddInHonorOfMessage(string to, string from, string message,
      int maximumCharacters, int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(to, from, message,
        BuyerMessageType.InHonorOfMessage, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddInHonorOfMessage(bool allowTo, bool allowFrom, 
      string message, int maximumCharacters, int maxLineLength, 
      int maxLines) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InHonorOfMessage, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddInMemoryOfMessage(string to, string from, string message) {
      _messages.Add(new BuyerMessage(to, from, message,
        BuyerMessageType.InTributeOfMessage));
    }

    ///<remarks></remarks>
    public void AddInMemoryOfMessage(bool allowTo, bool allowFrom, 
      string message) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InMemoryOfMessage));
    }

    ///<remarks></remarks>
    public void AddInMemoryOfMessage(string to, string from, string message,
      int maximumCharacters, int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(to, from, message,
        BuyerMessageType.InTributeOfMessage, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddInMemoryOfMessage(bool allowTo, bool allowFrom, 
      string message, int maximumCharacters, int maxLineLength, 
      int maxLines) {
      _messages.Add(new BuyerMessage(allowTo, allowFrom, message,
        BuyerMessageType.InMemoryOfMessage, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddGiftMessage(string message) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.GiftMessage));
    }

    ///<remarks></remarks>
    public void AddGiftMessage(string message, int maximumCharacters, 
      int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.GiftMessage, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddBuyerNote(string message) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.BuyerNote));
    }

    ///<remarks></remarks>
    public void AddBuyerNote(string message, int maximumCharacters, 
      int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.BuyerNote, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddSpecialInstructions(string message) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.SpecialInstructions));
    }

    ///<remarks></remarks>
    public void AddSpecialInstructions(string message, int maximumCharacters, 
      int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.SpecialInstructions, maximumCharacters, 
        maxLineLength, maxLines));
    }
    
    ///<remarks></remarks>
    public void AddDeliveryInstructions(string message) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.DeliveryInstructions));
    }

    ///<remarks></remarks>
    public void AddDeliveryInstructions(string message, int maximumCharacters, 
      int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.DeliveryInstructions, maximumCharacters, 
        maxLineLength, maxLines));
    }

    ///<remarks></remarks>
    public void AddSpecialRequests(string message) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.SpecialRequests));
    }

    ///<remarks></remarks>
    public void AddSpecialRequests(string message, int maximumCharacters, 
      int maxLineLength, int maxLines) {
      _messages.Add(new BuyerMessage(false, false, message,
        BuyerMessageType.SpecialRequests, maximumCharacters, 
        maxLineLength, maxLines));
    }

    /// <summary>
    /// If your API request includes the &lt;include-gift-receipt&gt; tag and the tag's
    /// value is true, then Google Checkout will indicate that a gift receipt will be
    /// included with the order. The buyer will have the option of removing the
    /// gift receipt from the order before completing the purchase.
    /// </summary>
    public bool IncludeGiftReceipt {
      get {
        return _includeGiftReceipt;
      }
      set {
        _includeGiftReceipt = true;
      }
    }

    /// <summary>
    /// Convert the messages to the AutoGen Messages used by the Xml Post.
    /// </summary>
    /// <returns></returns>
    public object[] ConvertMessages() {
      ArrayList retVal = new ArrayList();
      foreach (BuyerMessage message in _messages) {
          retVal.Add(message.GetXSDMessage());
      }

      if (IncludeGiftReceipt) {
//        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
//        doc.LoadXml("<include-gift-receipt>true</include-gift-receipt>");
        retVal.Add("true");
      }

      object[] ret = new object[retVal.Count];
      retVal.CopyTo(0, ret, 0, retVal.Count);
      return ret;
    }
  }
}
