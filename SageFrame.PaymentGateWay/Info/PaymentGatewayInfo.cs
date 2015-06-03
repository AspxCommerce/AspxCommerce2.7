using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.PaymentGateWay
{
    public class PaymentGatewayInfo
    {
        /// <summary>
        /// Returns or retains PaymentID
        /// </summary>
        public int PaymentID { get; set; }

        /// <summary>
        /// Returns or retains SettingValues
        /// </summary>
        public string SettingValue { get; set; }

        /// <summary>
        /// Returns or retains UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Returns or retains SecureToken
        /// </summary>
        public string SecureToken { get; set; }

        /// <summary>
        /// Returns or retains UserModuleID
        /// </summary>
        public int UserModuleID { get; set; }

        /// <summary>
        /// Returns or retains PortalID
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Returns or retains CultureCode
        /// </summary>
        public int CultureCode { get; set; }
    }
}
