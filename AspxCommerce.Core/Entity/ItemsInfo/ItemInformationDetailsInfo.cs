using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ItemInformationDetailsInfo
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public bool HidePrice { get; set; }
        public bool HideInRSSFeed { get; set; }
        public bool HideToAnonymous { get; set; }
        public string ActiveFrom { get; set; }
        public string ActiveTo { get; set; }
        public string SKU { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal?ListPrice { get; set; }
        public string NewFromDate { get; set; }
        public string NewToDate { get; set; }
        public int VisibilityOptionValueID { get; set; }
        public int IsFeaturedOptionValueID { get; set; }
        public string FeaturedFrom { get; set; }
        public string FeaturedTo { get; set; }
        public int IsSpecialOptionValueID { get; set; }
        public string SpecialFrom { get; set; }
        public string SpecialTo { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public int ServiceDuration { get; set; }
        public int IsPromo { get; set; }
        public bool IsManageInventory { get; set; }
        public bool IsUsedStoreSetting { get; set; }
        public int MinCartQuantity { get; set; }
        public int MaxCartQuantity { get; set; }
        public int LowStockQuantity { get; set; }
        public int OutOfStockQuantity { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SpecialPrice { get; set; }
        public string SpecialPriceFrom { get; set; }
        public string SpecialPriceTo { get; set; }
        public decimal? ManufacturerPrice { get; set; }
    }
}



