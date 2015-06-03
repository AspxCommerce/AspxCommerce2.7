using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core.Mobile
{
   public class OrderItem
    {  
       public int ItemId { get; set; }
       public int Quantity { get; set; }
       public string CostVariantIds { get; set; }
       public bool IsGiftCard { get; set; }
       public bool IsDownloadable { get; set; }
       public int CartItemId{get;set;}

    }
}
