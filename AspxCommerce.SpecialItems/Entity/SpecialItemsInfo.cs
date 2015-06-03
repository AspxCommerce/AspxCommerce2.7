using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsInfo
    {
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string ImagePath { get; set; }

        public int AttributeSetID { get; set; }
        public decimal Price { get; set; }
        public decimal? ListPrice { get; set; }
        public int Quantity { get; set; }
        public bool? HidePrice { get; set; }
        public bool? HideInRSSFeed { get; set; }
        public bool? HideToAnonymous { get; set; }
        public bool? IsOutOfStock { get; set; }
        public DateTime? AddedOn { get; set; }
        public string AlternateText { get; set; }
        public string CostVariants { get; set; }
        public int RowTotal { get; set; }

        public string IsFeatured { get; set; }
        public string IsSpecial { get; set; }
        public string AttributeValues { get; set; }
        public int RowNumber { get; set; }
    }
}
