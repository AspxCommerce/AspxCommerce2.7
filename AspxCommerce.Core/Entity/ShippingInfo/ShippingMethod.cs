using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ShippingMethod
    {
        public int ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShippingMethodCode { get; set; }
        public int ShippingProviderId { get; set; }
        public string ImagePath { get; set; }
        public string AlternateText { get; set; }
        public int DisplayOrder { get; set; }
        public string DeliveryTime { get; set; }
        public decimal? WeightLimitFrom { get; set; }
        public decimal? WeightLimitTo { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
    }
}
