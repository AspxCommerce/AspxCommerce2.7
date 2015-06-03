using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.USPS.Entity
{
    public class UspsSetting
    {
        public string UspsUserId { get; set; }
        public decimal UspsMinWeight { get; set; }
        public decimal UspsMaxWeight { get; set; }
        public string UspsRateApiUrl { get; set; }
        public string UspsTrackApiUrl { get; set; }
        public string UspsShipmentApiUrl { get; set; }
        public string UspsWeightUnit { get; set; }
        public bool UspsTestMode { get; set; }
    }
}
