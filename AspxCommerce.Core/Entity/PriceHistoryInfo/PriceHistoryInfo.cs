using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class PriceHistoryInfo
    {
        public int RowTotal { get; set; }
        public int PriceHistoryID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ConvertedPrice { get; set; }
        public string AddedBy { get; set; }
        public string Date { get; set; }
    }
}
