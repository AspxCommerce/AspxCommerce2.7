using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class GroupItemsOutOfStockInfo
    {
        public int RetValue{ get; set; }// 1 for cost variant Item 2 for Out of stock
        public int ItemID { get; set; }
        public bool IsOutOfStock { get; set; }
        public string ItemSKU { get; set; }

        public GroupItemsOutOfStockInfo(int retValue, int itemID,string itemSKU,bool isOutOfStock)
        {
            this.RetValue = retValue;
            this.ItemID = itemID;
            this.IsOutOfStock = isOutOfStock;
            this.ItemSKU = itemSKU;
        }
    }

    public class GroupItemsQtyInfo
    {
        public int ItemID { get; set; }
        public decimal Qty { get; set; }
        public decimal UserQty { get; set; }

        public GroupItemsQtyInfo(int itemID, decimal qty,decimal userQty)
        {
            this.ItemID = itemID;
            this.Qty = qty;
            this.UserQty = userQty;
        }
    }
}
