using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ShippingLabelInfo
    {
        public int ShippingLabelID { get; set; }
        public int OrderID { get; set; }
        public string ShippingLabelImage { get; set; }
        public string Extension { get; set; }
        public string TrackingNo { get; set; }
        public string BarcodeNo { get; set; }
        public bool IsRealTime { get; set; }
    }
}
