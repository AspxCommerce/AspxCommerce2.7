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
 *  4-20-2011  Joe Feser joe.feser@joefeser.com
 *  Fixed issue allowing a serial number to be used.
 *  08-03-2012    Joe Feser joe.feser@joefeser.com
 *  Added logging
 * 
*/
using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Util;
using System.Reflection;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// Wrapper object to provide support for the
  /// Notification History API
  /// </summary>
  /// <remarks>
  /// The Notification History API lets you retrieve notifications 
  /// for a particular order or date range.
  /// </remarks>
  public class NotificationHistoryRequest : ReportRequestBase {

    private bool _retrieveAllNotifications;
    private string _nextPageToken;
    private DateTime _startTime = DateTime.MinValue;
    private DateTime _endTime = DateTime.MinValue;
    private List<NotificationTypes> _notificationTypes;
    private List<string> _orderNumbers;
    private NotificationHistorySerialNumber _serialNumber;

    /// <summary>
    /// If set to true, the code will automatically call the Next Page
    /// until all of the results are returned.
    /// </summary>
    public bool RetrieveAllNotifications {
      get {
        return _retrieveAllNotifications;
      }
      set {
        _retrieveAllNotifications = value;
      }
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest used to
    /// obtain more results for a previous request.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="nextPageToken">The Token needed to obtain the next page
    /// of messages</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment, string nextPageToken)
      : base(merchantID, merchantKey, environment) {
      _nextPageToken = nextPageToken;
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest used to
    /// obtain more results for a previous request.
    /// </summary>
    /// <param name="nextPageToken">The Token needed to obtain the next page
    /// of messages</param>
    public NotificationHistoryRequest(string nextPageToken)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _nextPageToken = nextPageToken;
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest used to
    /// obtain more results for a previous request.
    /// </summary>
    /// <param name="serialNumber">The message serial number</param>
    public NotificationHistoryRequest(NotificationHistorySerialNumber serialNumber)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _serialNumber = serialNumber;
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="startTime">The start time for the messages</param>
    /// <param name="endTime">The end time for the messages</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment, DateTime startTime,
      DateTime endTime)
      : base(merchantID, merchantKey, environment) {
      _startTime = startTime;
      _endTime = endTime;
      VerifyDateRange();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="startTime">The start time for the messages</param>
    /// <param name="endTime">The end time for the messages</param>
    public NotificationHistoryRequest(DateTime startTime,
      DateTime endTime)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _startTime = startTime;
      _endTime = endTime;
      VerifyDateRange();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="startTime">The start time for the messages</param>
    /// <param name="endTime">The end time for the messages</param>
    /// <param name="notificationTypes">The notification types to request</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment, DateTime startTime,
      DateTime endTime, List<NotificationTypes> notificationTypes)
      : base(merchantID, merchantKey, environment) {
      _startTime = startTime;
      _endTime = endTime;
      _notificationTypes = notificationTypes;
      VerifyDateRange();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="startTime">The start time for the messages</param>
    /// <param name="endTime">The end time for the messages</param>
    /// <param name="notificationTypes">The notification types to request</param>
    public NotificationHistoryRequest(DateTime startTime,
      DateTime endTime, List<NotificationTypes> notificationTypes)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _startTime = startTime;
      _endTime = endTime;
      _notificationTypes = notificationTypes;
      VerifyDateRange();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="orderNumbers">The order numbers to query</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment,
      List<string> orderNumbers)
      : base(merchantID, merchantKey, environment) {
      _orderNumbers = orderNumbers;
      VerifyOrderNumbers();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="serialNumber">The serial number to query</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment,
      NotificationHistorySerialNumber serialNumber)
      : base(merchantID, merchantKey, environment) {
      _serialNumber = serialNumber;
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="orderNumbers">The order numbers to query</param>
    public NotificationHistoryRequest(List<string> orderNumbers)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _orderNumbers = orderNumbers;
      VerifyOrderNumbers();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="merchantID">Google Checkout Merchant ID</param>
    /// <param name="merchantKey">Google Checkout Merchant Key</param>
    /// <param name="environment">A String representation of 
    /// <see cref="EnvironmentType"/></param>
    /// <param name="orderNumbers">The order numbers to query</param>
    /// <param name="notificationTypes">The notification types to request</param>
    public NotificationHistoryRequest(string merchantID,
      string merchantKey, string environment,
      List<string> orderNumbers, List<NotificationTypes> notificationTypes)
      : base(merchantID, merchantKey, environment) {
      _orderNumbers = orderNumbers;
      _notificationTypes = notificationTypes;
      VerifyOrderNumbers();
    }

    /// <summary>
    /// Create a new Instance of NotificationHistoryRequest
    /// based on a date range.
    /// </summary>
    /// <param name="orderNumbers">The order numbers to query</param>
    /// <param name="notificationTypes">The notification types to request</param>
    public NotificationHistoryRequest(List<string> orderNumbers,
      List<NotificationTypes> notificationTypes)
      : base(GCheckoutConfigurationHelper.MerchantID,
      GCheckoutConfigurationHelper.MerchantKey,
      GCheckoutConfigurationHelper.Environment) {
      _orderNumbers = orderNumbers;
      _notificationTypes = notificationTypes;
      VerifyOrderNumbers();
    }

    /// <summary>
    /// Parse the Message for a NotificationDataResponse
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected override GCheckoutResponse ParseResponse(string response) {

      NotificationHistoryResponse retVal = null;

      try {
        retVal = new NotificationHistoryResponse(response);
      }
      catch (Exception ex) {
        string token = null;
        if (retVal != null) {
          token = retVal.NextPageToken;
        }
        Log.Err("NotificationHistoryRequest ParseResponse:" + ex.Message);
        throw new NotificationHistoryException(retVal, token, ex);
      }

      //now process the additional results.
      if (retVal.HasMoreResults && RetrieveAllNotifications) {
        ProcessAdditionalTokens(retVal);
      }

      return retVal;
    }

    /// <summary>
    /// perform additional queries as needed.
    /// </summary>
    /// <param name="response"></param>
    private void ProcessAdditionalTokens(NotificationHistoryResponse response) {

      NotificationHistoryResponse currentResponse = response;

      while (string.IsNullOrEmpty(currentResponse.NextPageToken) == false) {
        NotificationHistoryRequest newRequest
          = new NotificationHistoryRequest(MerchantID, MerchantKey,
          Environment.ToString(), response.NextPageToken);
        try {
          NotificationHistoryResponse nextResponse = newRequest.Send()
            as NotificationHistoryResponse;
          response.AddAdditionalResponse(nextResponse);
          currentResponse = nextResponse;
        }
        catch (Exception ex) {
          Log.Err("NotificationHistoryRequest ProcessAdditionalTokens:" + ex.Message);
          throw new NotificationHistoryException(response, response.NextPageToken, ex);
        }
      }
    }

    /// <summary>
    /// Get the Xml for the message
    /// </summary>
    /// <returns></returns>
    public override byte[] GetXml() {
      AutoGen.NotificationHistoryRequest retVal
      = new GCheckout.AutoGen.NotificationHistoryRequest();

      if (!string.IsNullOrEmpty(_nextPageToken)) {
        retVal.nextpagetoken = _nextPageToken;
      }
      else {
        if (_startTime > DateTime.MinValue) {
          retVal.starttime = _startTime;
          retVal.starttimeSpecified = true;
          retVal.endtime = _endTime;
          retVal.endtimeSpecified = true;
        }
        if (_orderNumbers != null && _orderNumbers.Count > 0) {
          retVal.ordernumbers = _orderNumbers.ToArray();
        }
        if (_notificationTypes != null && _notificationTypes.Count > 0) {
          //we need to convert the list
          List<string> nt = new List<string>();
          Type t = typeof(NotificationTypes);
          foreach (NotificationTypes item in _notificationTypes) {
            FieldInfo fi = t.GetField(item.ToString());
            nt.Add(EnumSerilizedNameAttribute.GetValue(fi));
          }
        }
        if (_serialNumber != null && 
          !string.IsNullOrEmpty(_serialNumber.SerialNumber)) {
          retVal.serialnumber = _serialNumber.SerialNumber;
        }
      }
      return EncodeHelper.Serialize(retVal);
    }

    private void VerifyOrderNumbers() {
      if (_orderNumbers == null || _orderNumbers.Count == 0) {
        throw new ArgumentException("orderNumbers must be a valid list with at least one value.");
      }
    }

    private void VerifyDateRange() {
      if (_endTime < _startTime) {
        throw new ArgumentException("endTime must be larger or equal to startTime");
      }
    }
  }
}
