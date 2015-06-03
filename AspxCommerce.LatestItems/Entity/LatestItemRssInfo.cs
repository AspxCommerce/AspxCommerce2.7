using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.LatestItems
{
    public class LatestItemRssInfo
    {
        public LatestItemRssInfo()
        {
        }
        public int ItemID { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemName { get; set; }
        public string SKU { get; set; }
        public string ImagePath { get; set; }
        public string ShortDescription { get; set; }
        public string AddedOn { get; set; }
        public string AlternateText { get; set; }
    }
}
