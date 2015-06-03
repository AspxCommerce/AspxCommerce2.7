using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class ItemSetting
    {

        public bool BuyAsGiftEnable { get; set; }
        public bool GiftWrapAvailable { get; set; }
        public bool EMIOptionEnable { get; set; }
        public bool TryNBuyEnable { get; set; }

        public bool IsManageInventory { get; set; } 
        public bool IsUsedStoreSetting { get; set; }
        public int MinCartQuantity { get; set; }
        public int MaxCartQuantity { get; set; }
        public int LowStockQuantity { get; set; }
        public int OutOfStockQuantity { get; set; }
    }
}
