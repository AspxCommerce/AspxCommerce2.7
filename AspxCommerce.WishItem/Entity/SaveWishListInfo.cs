using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.WishItem
{
    public class SaveWishListInfo : AspxExtraCommonInfo
    {
        public int ItemID { get; set; }
        public string CostVariantValueIDs { get; set; }
    }
  
    //public class WishItemsEmailInfo : SendEmailInfo
    //{
    //    public string ItemID { get; set; }
    //}

}
