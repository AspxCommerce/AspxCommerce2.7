using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class RecentlyAddedItemInfo : AddItemToCartInfo
    {
        public string IP { get; set; }
        public string CountryName { get; set; }
        public string SKU { get; set; }
    }
}
