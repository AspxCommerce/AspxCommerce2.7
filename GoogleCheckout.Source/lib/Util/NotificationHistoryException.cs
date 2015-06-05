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
 *  9-7-2008   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.OrderProcessing;

namespace GCheckout.Util {

  /// <summary>
  /// Exception used to wrap an exception thrown when parsing the response.
  /// </summary>
  public class NotificationHistoryException : ApplicationException {

    private const string MESSAGE = "The response received was not a " +
      "NotificationHistoryResponse. This " +
      "will only exist while processing the RetrieveAllNotifications flag.";

    private string _nextPageToken;
    private NotificationHistoryResponse _response;

    /// <summary>
    /// Return the Next Page token that failed.
    /// </summary>
    public string NextPageToken {
      get {
        return _nextPageToken;
      }
    }

    /// <summary>
    /// Return the Response upto this point.
    /// </summary>
    public NotificationHistoryResponse Response {
      get {
        return _response;
      }
    }

    /// <summary>
    /// Create a new instance of the Exception
    /// </summary>
    /// <param name="response">The original
    /// NotificationHistoryResponse if it exists</param>
    /// <param name="nextPageToken">The Next Page Token if it exists</param>
    /// <param name="innerException">The inner exception that was throw</param>
    public NotificationHistoryException(NotificationHistoryResponse response,
      string nextPageToken, Exception innerException)
      : base(MESSAGE, innerException) {
      _nextPageToken = nextPageToken;
      _response = response;
    }


  }
}
