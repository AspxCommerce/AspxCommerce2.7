using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.CompareItem
{
    public class SaveCompareItemInfo : AddItemToCartInfo
    {
        public string IP { get; set; }
        public string CountryName { get; set; }
        public string SKU { get; set; }
    }
}
