using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.AdvanceSearch
{
    public class AdvanceSearchDetailsInfo
    {
        public int? RowTotal { get; set; }
        public string CostVariants { get; set; }
        public int? ItemTypeID { get; set; }
        public string SKU { get; set; }
        public bool? IsOutOfStock { get; set; }
        public string Name { get; set; }
        public int? ItemID { get; set; }
        public string IsFeatured { get; set; }
        public string IsSpecial { get; set; }
        public string BaseImage { get; set; }
        public string AlternateText { get; set; }
        public string Price { get; set; }
        public string ListPrice { get; set; }
        public string AttributeValues { get; set; }
        public int RowNumber { get; set; }
    }
}
