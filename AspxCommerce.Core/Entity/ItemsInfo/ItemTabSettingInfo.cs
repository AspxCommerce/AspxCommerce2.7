using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ItemTabSettingInfo
    {
        public ItemTabSettingInfo() { }
        public bool EnableCostVariantOption { get; set; }
        public bool EnableGroupPrice { get; set; }
        public bool EnableTierPrice { get; set; }
        public bool EnableRelatedItem { get; set; }
        public bool EnableCrossSellItem { get; set; }
        public bool EnableUpSellItem { get; set; }
    }
}
