using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

  /// <summary>
  /// Notification Types used for the Notification History Request.
  /// </summary>
  public enum NotificationTypes {
    /// <remarks/>
    [EnumSerilizedName("authorization-amount")]
    AuthorizationAmount,
    /// <remarks/>
    [EnumSerilizedName("charge-amount")]
    ChargeAmount,
    /// <remarks/>
    [EnumSerilizedName("chargeback-amount")]
    ChargebackAmount,
    /// <remarks/>
    [EnumSerilizedName("new-order")]
    NewOrder,
    /// <remarks/>
    [EnumSerilizedName("order-state-change")]
    OrderStateChange,
    /// <remarks/>
    [EnumSerilizedName("refund-amount")]
    RefundAmount,
    /// <remarks/>
    [EnumSerilizedName("risk-information")]
    RiskInformation
  }
}
