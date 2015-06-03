using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class PromoItemsInfo
    {
        public int RowTotal { get; set; }
        public int itemID { get; set; }
        public string Name { get; set; }
        public decimal CouponAmount { get; set; }
        public bool IsPercentage { get; set; }
        public int UsesPerItem { get; set; }
    }
}
