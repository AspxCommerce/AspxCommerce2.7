using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.PaymentGateWay
{

    public class PaymentGatewaySetting
    {
        public PayPalSetting PayPal { get; set; }     
    }
    public class PayPalSetting
    {
        public string RedirectURL { get; set; }
        public string CancelURL { get; set; }
        public string BusinessAccount { get; set; }
        public string AuthToken { get; set; }
        public int Price { get; set; }
        public bool IsTestPayPal { get; set; }
    }
}
