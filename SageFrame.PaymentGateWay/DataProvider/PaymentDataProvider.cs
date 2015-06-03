using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.PaymentGateWay
{
    /// <summary>
    /// Manipulates data for Payment Gateway
    /// </summary>
    public class PaymentDataProvider
    {
        /// <summary>
        /// Connects to database and saves Payment Gateway and their setting in json data format.
        /// </summary>
        /// <param name="objPaymentInfo"> Object of class PaymentGatewayInfo.</param>
        public void SavePaymentGateways(PaymentGatewayInfo objPaymentInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@SettingValue", objPaymentInfo.SettingValue));
                Param.Add(new KeyValuePair<string, object>("@PortalID", objPaymentInfo.PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserModuleID", objPaymentInfo.UserModuleID));
                Param.Add(new KeyValuePair<string, object>("@UserName", objPaymentInfo.UserName));
                Param.Add(new KeyValuePair<string, object>("@CultureCode", objPaymentInfo.CultureCode));
                SQLHandler sqlh = new SQLHandler();
                sqlh.ExecuteNonQuery("usp_PaymentGatewaySetting_Save", Param);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns  Payment Gateways and their setting values.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>PaymentGatewayInfo object containg settingkey and values of Payment Gateway.</returns>
        public PaymentGatewayInfo GetPaymentGatewaysSetting(int portalID)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                SQLHandler sqlh = new SQLHandler();
                return sqlh.ExecuteAsObject<PaymentGatewayInfo>("usp_PaymentGatewaySetting_GetSettingValue", Param);
            }
            catch
            {
                throw;
            }
        }
    }
}
