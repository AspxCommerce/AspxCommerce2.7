using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class Kit
    {

       public int ID { get; set; }
       public int KitID { get; set; }
       public string KitName { get; set; }
       public decimal Price { get; set; }
       public int Quantity { get; set; }
       public decimal Weight { get; set; }
       public int KitComponentID { get; set; }

    }
   public class KitInfo : Kit {

       public int RowTotal { get; set; }
   }
   public class CartKit {
       public string Description { get; set; }
       public string Price { get; set; }
       public string Weight { get; set; }
       public List<CartKitDetail> Data { get; set; }

   }
   public class CartKitDetail:KitComponent {
       public List<Kit> SelectedKits { get; set; }
   }

}
