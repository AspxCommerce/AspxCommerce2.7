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
using System.Configuration;
using GCheckout.NotificationQueue;
using GCheckout.AutoGen;
using GCheckout.Util;

namespace GCheckout {
	class Class1 {
		[STAThread]
		static void Main(string[] args) {
      INotificationQueue Q = new FileSystemNotificationQueue(
        GetPathFromConfigFile("InboxDir"), 
        GetPathFromConfigFile("InProcessDir"),
        GetPathFromConfigFile("SuccessDir"),
        GetPathFromConfigFile("FailureDir"));
      while (true) {
        Console.WriteLine("\nWaiting for the next notification.");
        NotificationQueueMessage M = Q.Receive();
        Console.WriteLine("Processing {0} for order {1}.", M.Type, M.OrderId);
        try {
          ProcessNotification(M.Type, M.Xml);
          Q.ProcessingSucceeded(M);
        }
        catch (Exception e) {
          Console.WriteLine(e.ToString());
          Q.ProcessingFailed(M);
        }
        Console.WriteLine("{0} notifications in queue.", Q.GetLength());
      }
		}

    private static string GetPathFromConfigFile(string Key) {
      string RetVal = ConfigurationSettings.AppSettings[Key];
      if (RetVal == null) throw new ApplicationException(
        "Set the '" + Key + "' key in the config file.");
      return RetVal;
    }

    private static void ProcessNotification(string Type, string Xml) {
      switch (Type) {
        case "new-order-notification":
          NewOrderNotification N1 = (NewOrderNotification)
            EncodeHelper.Deserialize(Xml, typeof(NewOrderNotification));
          // Add call to existing business system here, passing data from N1.
          break;
        case "risk-information-notification":
          RiskInformationNotification N2 = (RiskInformationNotification)
            EncodeHelper.Deserialize(Xml, typeof(RiskInformationNotification));
          // Add call to existing business system here, passing data from N2.
          break;
        case "order-state-change-notification":
          OrderStateChangeNotification N3 = (OrderStateChangeNotification)
            EncodeHelper.Deserialize(Xml, typeof(OrderStateChangeNotification));
          // Add call to existing business system here, passing data from N3.
          break;
        case "charge-amount-notification":
          ChargeAmountNotification N4 = (ChargeAmountNotification)
            EncodeHelper.Deserialize(Xml, typeof(ChargeAmountNotification));
          // Add call to existing business system here, passing data from N4.
          break;
        case "refund-amount-notification":
          RefundAmountNotification N5 = (RefundAmountNotification)
            EncodeHelper.Deserialize(Xml, typeof(RefundAmountNotification));
          // Add call to existing business system here, passing data from N5.
          break;
        case "chargeback-amount-notification":
          ChargebackAmountNotification N6 = (ChargebackAmountNotification)
            EncodeHelper.Deserialize(Xml, typeof(ChargebackAmountNotification));
          // Add call to existing business system here, passing data from N6.
          break;
        default:
          throw new ApplicationException("Unknown notification type: " + Type);
      }
    }
  }
}
