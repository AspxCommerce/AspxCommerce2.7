using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class KitConfiguration {

        public List<ItemKit> KitConfig { get; set; }
        public List<ItemKit> KitDeleted { get; set; }
    }

    public class ItemKit
    {
        public int ItemKitID { get; set; }
        public int ItemID { get; set; }
        public int KitID { get; set; }
        public string KitName { get; set; }
        public string KitComponentName { get; set; }
        public int KitComponentType { get; set; }
        public int KitComponentID { get; set; }
        public int KitComponentOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public bool IsDefault { get; set; }
        public int KitOrder { get; set; }


    }
}
