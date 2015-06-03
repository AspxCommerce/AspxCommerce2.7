using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace SageFrame.PaymentGateWay
{
    /// <summary>
    /// Business logic for Payment Gateway
    /// </summary>
    public class PaymentController
    {
        /// <summary>
        /// Saves Payment Gateway and their setting in json data format.
        /// </summary>
        /// <param name="objPaymentInfo"></param>
        public void SavePaymentGateways(PaymentGatewayInfo objPaymentInfo)
        {
            try
            {
                PaymentDataProvider objDataProvider = new PaymentDataProvider();
                objDataProvider.SavePaymentGateways(objPaymentInfo);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Returns  Payment Gateways and their setting values.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>PaymentGatewayInfo object containg settingkey and values of Payment Gateway.</returns>
        public PaymentGatewayInfo GetPaymentGatewaysSetting(int portalID)
        {
            try
            {
                PaymentDataProvider objDataProvider = new PaymentDataProvider();
                return objDataProvider.GetPaymentGatewaysSetting(portalID);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Fetch payment gateway settings key value collection 
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>PaymentGatewaySetting object containing the payment gateway setting keys and values</returns>
        public PaymentGatewaySetting FetchSetting(int portalID)
        {
            try
            {
                PaymentGatewayInfo objGatewayInfo = new PaymentGatewayInfo();
                objGatewayInfo = GetPaymentGatewaysSetting(portalID);
                PaymentGatewaySetting objSettings = new JavaScriptSerializer().Deserialize<PaymentGatewaySetting>(objGatewayInfo.SettingValue);
                return objSettings;
            }
            catch
            {
                throw;
            }
        }
    }
}
