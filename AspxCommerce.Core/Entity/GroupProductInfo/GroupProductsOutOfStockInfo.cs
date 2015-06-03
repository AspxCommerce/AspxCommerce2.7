using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class GroupProductsOutOfStockInfo
    {
        public int RetValue{ get; set; }// 1 for cost variant Item 2 for Out of stock
        public int ItemID { get; set; }
        public bool IsOutOfStock { get; set; }

        public  GroupProductsOutOfStockInfo(int retValue, int itemID,bool isOutOfStock)
        {
            this.RetValue = retValue;
            this.ItemID = itemID;
            this.IsOutOfStock = isOutOfStock;
        }
    }

    public class GroupProductsQtyInfo
    {
        public int ItemID { get; set; }
        public decimal Qty { get; set; }

        public GroupProductsQtyInfo(int itemID, decimal qty)
        {
            this.ItemID = itemID;
            this.Qty = qty;
        }
    }
}
