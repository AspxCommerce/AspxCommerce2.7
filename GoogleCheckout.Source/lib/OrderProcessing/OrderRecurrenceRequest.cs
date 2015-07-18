using System;
using System.Collections.Generic;
using System.Text;
using GCheckout.Util;

namespace GCheckout.OrderProcessing {

    /// <summary>
    /// Wrapper for CreateOrderRecurrentRequest for Subscriptions
    /// The documentation is very sparse for this item and I assume it will change soon.
    /// </summary>
    public class OrderRecurrenceRequest : OrderProcessingBase {

        private AutoGen.ShoppingCart _cart;

        /// <summary>
        /// Base ctor to initialize the messages
        /// </summary>
        /// <param name="merchantID">Google Checkout Merchant ID</param>
        /// <param name="merchantKey">Google Checkout Merchant Key</param>
        /// <param name="env">A String representation of 
        /// <see cref="EnvironmentType"/></param>
        /// <param name="googleOrderNumber">The Google Order Number</param>
        /// <param name="cart">The shopping cart to post</param>
        public OrderRecurrenceRequest(string merchantID,
          string merchantKey, string env, string googleOrderNumber,
            AutoGen.ShoppingCart cart)
            : base(merchantID, merchantKey, env, googleOrderNumber) {
            _cart = cart;
        }

        /// <summary>
        /// Base ctor to initialize the messages
        /// </summary>
        /// <param name="googleOrderNumber">The Google Order Number</param>
        /// <param name="cart">The shopping cart to post</param>
        public OrderRecurrenceRequest(string googleOrderNumber,
            AutoGen.ShoppingCart cart)
            : base(GCheckoutConfigurationHelper.MerchantID.ToString(),
            GCheckoutConfigurationHelper.MerchantKey,
            GCheckoutConfigurationHelper.Environment.ToString(),
            googleOrderNumber) {
            _cart = cart;
        }

        /// <summary>
        /// Get the cart
        /// </summary>
        /// <returns></returns>
        public override byte[] GetXml() {
            AutoGen.CreateOrderRecurrenceRequest retVal 
                = new AutoGen.CreateOrderRecurrenceRequest();
            retVal.googleordernumber = GoogleOrderNumber;
            retVal.shoppingcart = _cart;
            return EncodeHelper.Serialize(retVal);
        }
    }
}
