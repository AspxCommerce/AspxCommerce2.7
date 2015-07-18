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
  /// A subscription portion of the Item. This contains all of the information needed to support a subscription.
  /// </summary>
  public class Subscription : ICloneable {

    private DateTime? _noChargeAfter;
    private List<SubscriptionPayment> _payments
      = new List<SubscriptionPayment>();
    private IShoppingCartItem _recurrentItem;
    private AutoGen.DatePeriod _period = GCheckout.AutoGen.DatePeriod.MONTHLY;
    private DateTime? _startDate;
    private SubscriptionType _type;

    /// <summary>
    /// The no-charge-after attribute specifies the latest date and time 
    /// that you can charge the customer for the subscription. 
    /// </summary>
    public DateTime? NoChargeAfter {
      get {
        return _noChargeAfter;
      }
      set {
        _noChargeAfter = value;
      }
    }

    /// <summary>
    /// Required. The period attribute specifies how frequently 
    /// you will charge the customer for the subscription. 
    /// Valid values for this attribute are DAILY, WEEKLY, 
    /// SEMI_MONTHLY, MONTHLY, EVERY_TWO_MONTHS, QUARTERLY, and YEARLY..
    /// </summary>
    public AutoGen.DatePeriod Period {
      get {
        return _period;
      }
      set {
        _period = value;
      }
    }

    /// <summary>
    /// The Item that is used for the subscription
    /// </summary>
    public List<SubscriptionPayment> Payments {
      get {
        return _payments;
      }
    }

    /// <summary>
    /// The Item that is used for the subscription
    /// </summary>
    public IShoppingCartItem RecurrentItem {
      get {
        return _recurrentItem;
      }
      set {
        if (value != null) {
          if (value.Subscription != null) {
            throw new ApplicationException(
              "A RecurrentItem may not contain a subscription.");
          }
        }
        _recurrentItem = value;
      }
    }

    /// <summary>
    /// The start-date attribute specifies the date that the 
    /// subscription's recurrence period will begin.
    /// </summary>
    public DateTime? StartDate {
      get {
        return _startDate;
      }
      set {
        _startDate = value;
      }
    }

    /// <summary>
    /// The type attribute identifies the type of subscription that you are creating. 
    /// The valid values for this attribute are merchant and google, 
    /// and this specifies who handles the recurrences.
    /// </summary>
    public SubscriptionType Type {
      get {
        return _type;
      }
      set {
        _type = value;
      }
    }

    /// <summary>
    /// Create a new subscription item.
    /// </summary>
    public Subscription() {

    }

    /// <summary>
    /// Create a new subscription item passing in the recurrentItem
    /// </summary>
    /// <param name="recurrentItem">The item for the subscription</param>
    /// <param name="period">The period attribute specifies how frequently 
    /// you will charge the customer for the subscription</param>
    /// <param name="type">
    /// The type attribute identifies the type of subscription that you are creating. 
    /// The valid values for this attribute are merchant and google, 
    /// and this specifies who handles the recurrences.
    /// </param>
    public Subscription(IShoppingCartItem recurrentItem, AutoGen.DatePeriod period,
      SubscriptionType type) {
      this.RecurrentItem = recurrentItem;
      this.Period = period;
      this.Type = type;
    }

    /// <summary>
    /// Create a new subscription item passing in the item and the payment
    /// </summary>
    /// <param name="recurrentItem"></param>
    /// <param name="payment"></param>
    /// <param name="period">The period attribute specifies how frequently 
    /// you will charge the customer for the subscription</param>
    /// <param name="type">
    /// The type attribute identifies the type of subscription that you are creating. 
    /// The valid values for this attribute are merchant and google, 
    /// and this specifies who handles the recurrences.
    /// </param>
    public Subscription(IShoppingCartItem recurrentItem, SubscriptionPayment payment,
      AutoGen.DatePeriod period, SubscriptionType type) {
      this.RecurrentItem = recurrentItem;
      this.Payments.Add(payment);
      this.Period = period;
      this.Type = type;
    }

    internal Subscription(AutoGen.Subscription subscription) {
      if (subscription == null)
        throw new ArgumentNullException("subscription");
      if (subscription.nochargeafterSpecified)
        this.NoChargeAfter = subscription.nochargeafter;
      if (subscription.payments != null) {
        foreach (AutoGen.SubscriptionPayment item in subscription.payments) {
          this.Payments.Add(new SubscriptionPayment(item));
        }
      }
      this.Period = subscription.period;
      this.RecurrentItem = new ShoppingCartItem(subscription.recurrentitem);
      this.StartDate = subscription.startdate;
      this.Type = (SubscriptionType)Enum.Parse(typeof(SubscriptionType), subscription.type, true);
    }

    /// <summary>
    /// Add a payment to the subscription
    /// </summary>
    /// <param name="payment"></param>
    public void AddSubscriptionPayment(SubscriptionPayment payment) {
      if (_payments.Count > 0)
        throw new ApplicationException(
          "Only one payment is supported for a subscription item.");
      _payments.Add(payment);
    }

    #region ICloneable Members

    /// <summary>
    /// Clone the object.
    /// </summary>
    /// <returns></returns>
    public object Clone() {
      Subscription retVal = new Subscription();
      retVal.NoChargeAfter = this.NoChargeAfter;
      foreach (SubscriptionPayment payment in this.Payments) {
        retVal.Payments.Add(payment.Clone() as SubscriptionPayment);
      }
      retVal.Period = this.Period;
      ICloneable clone = this.RecurrentItem as ICloneable;
      if (clone != null)
        retVal.RecurrentItem = clone.Clone() as IShoppingCartItem;
      retVal.StartDate = this.StartDate;
      retVal.Type = this.Type;

      return retVal;
    }

    #endregion
  }

}
