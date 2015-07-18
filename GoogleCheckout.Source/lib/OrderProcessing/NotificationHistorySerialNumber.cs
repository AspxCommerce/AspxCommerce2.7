/*************************************************
 * Copyright (C) 2011 Google Inc.
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
 *  4-20-2011   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GCheckout.OrderProcessing {
  
  /// <summary>
  /// This is needed due to the string overloads in the NotificationHistoryRequest.
  /// </summary>
  public class NotificationHistorySerialNumber {

    /// <summary>
    /// The Serial Number of the message.
    /// </summary>
    public string SerialNumber {
      get;
      set;
    }

    /// <summary>
    /// Create a new NotificationHistorySerialNumber
    /// </summary>
    /// <param name="serialNumber">The serial number to request from the Notification History API</param>
    public NotificationHistorySerialNumber(string serialNumber) {
      this.SerialNumber = serialNumber;
    }
  
  }
}
