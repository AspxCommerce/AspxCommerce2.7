/*************************************************
 * Copyright (C) 2009 Google Inc.
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
 *  June 2009   Joe Feser joe.feser@joefeser.com
 *  Initial Release.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace GCheckout.Checkout {

  /// <summary>
  /// A subsctription payment
  /// </summary>
  public class SubscriptionPayment : ICloneable {

    private decimal _maximumCharge;
    private int _times = 0;

    /// <summary>
    /// The times attribute indicates how many times you will charge the customer
    /// for a defined subscription payment. A subscription may have multiple 
    /// payment schedules, and the times attribute lets you indicate how many 
    /// times each charge will be assessed. For example, you might charge the
    /// customer a reduced rate for the first three months of a subscription
    /// and then charge the standard rate each month thereafter.
    /// </summary>
    public int Times {
      get {
        return _times;
      }
      set {
        _times = value;
      }
    }

    /// <summary>
    /// Specifies the maximum amount that you will be allowed to charge 
    /// the customer, including tax, for all recurrences.
    /// </summary>
    public decimal MaximumCharge {
      get {
        return _maximumCharge;
      }
      set {
        _maximumCharge = value;
      }
    }

    /// <summary>
    /// Create a new subscription payment
    /// </summary>
    public SubscriptionPayment() {

    }

    /// <summary>
    /// Create new instance
    /// </summary>
    /// <param name="payment">The payment information</param>
    public SubscriptionPayment(AutoGen.SubscriptionPayment payment) {
      if (payment == null)
        throw new ArgumentNullException("payment");
      if (payment.maximumcharge != null)
        this.MaximumCharge = payment.maximumcharge.Value;
      if (payment.timesSpecified)
        this.Times = payment.times;
    }

    /// <summary>
    /// Create a new subscription payment
    /// </summary>
    /// <param name="times">
    ///The times attribute indicates how many times you will charge the customer
    /// for a defined subscription payment. A subscription may have multiple 
    /// payment schedules, and the times attribute lets you indicate how many 
    /// times each charge will be assessed. For example, you might charge the
    /// customer a reduced rate for the first three months of a subscription
    /// and then charge the standard rate each month thereafter.
    /// </param>
    /// <param name="maximumCharge">Specifies the maximum amount that you will
    /// be allowed to charge the customer, including tax, for all recurrences.
    /// </param>
    public SubscriptionPayment(int times, decimal maximumCharge) {
      this.Times = times;
      this.MaximumCharge = maximumCharge;
    }

    #region ICloneable Members

    /// <summary>
    /// Clone the object
    /// </summary>
    /// <returns></returns>
    public object Clone() {
      SubscriptionPayment retVal = new SubscriptionPayment();
      retVal.MaximumCharge = this.MaximumCharge;
      retVal.Times = this.Times;
      return retVal;
    }

    #endregion
  }
}
