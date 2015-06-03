using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.AdvanceSearch
{
    public class ItemsByDynamicAdvanceSearchInfo
    {

        public int? CategoryID { get; set; }
        public bool IsGiftCard { get; set; }
        public string SearchText { get; set; }
        public int BrandID { get; set; }
        public float? PriceFrom { get; set; }
        public float? PriceTo { get; set; }
        public string AttributeIDs { get; set; }
        public int RowTotal { get; set; }
        public int SortBy { get; set; }
    }
}
  