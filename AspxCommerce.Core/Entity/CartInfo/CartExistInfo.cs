using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class CartExistInfo
    {
       public  CartExistInfo()
       {
           
       }
       public bool CartStatus{get; set; }
       public decimal CartSubTotal { get; set; }
    }

    public class UpdateCartInfo
    {
        public int CartID { get; set; }
        public string CartItemIDs { get; set; }
        public string Quantities { get; set; }
        public string AllowOutOfStock { get; set; }
    }

    public class GroupProductCartInfo
    {
        public string CartItemIDs { get; set; }
        public string CartItemQtys { get; set; }
        public string CartItemPrices { get; set; }
        public string CartItemSKUs { get; set; }
      
    }
    public class GroupProductCartReturnInfo
    {   public string CartItemIDs { get; set; }
        public string CartItemSkus { get; set; }
        public string CartItemReturnVals { get; set; }
    }

}
