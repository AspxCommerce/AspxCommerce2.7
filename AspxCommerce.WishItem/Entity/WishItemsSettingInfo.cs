using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.WishItem
{
    public class WishItemsSettingInfo
    {
        public bool IsEnableWishList { get; set; }
        public string WishListPageName { get; set; }
        public bool IsEnableImageInWishlist { get; set; }
        public int NoOfRecentAddedWishItems { get; set; }
    }

    public class WishItemsSettingKeyPairInfo
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
