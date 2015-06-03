using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.BrandView
{
    public class BrandRssInfo
    {
        public BrandRssInfo()
        {
        }
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemName { get; set; }
        public string SKU { get; set; }
        public string ImagePath { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandImageUrl { get; set; }
        public string AddedOn { get; set; }
    }
}
