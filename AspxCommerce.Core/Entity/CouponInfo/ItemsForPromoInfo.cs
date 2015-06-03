using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class ItemsForPromoInfo
    {
       public int RowTotal { get; set; }
       public int ItemID { get; set; }
       public string Name { get; set; }
       public decimal Price { get; set; }
       public int Quantity { get; set; }
       public string Visibility { get; set; }
       public bool IsActive { get; set; }
       public bool IsExists { get; set; }
    }
}
