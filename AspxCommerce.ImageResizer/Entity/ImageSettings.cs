using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspxCommerce.Core;

namespace AspxCommerce.ImageResizer
{
    public class ImageSettings
    {
        public int ItemLargeThumbNailHeight { get; set; }
        public int ItemLargeThumbNailWidth { get; set; }
        public int ItemMediumThumbNailHeight { get; set; }
        public int ItemMediumThumbNailWidth { get; set; }
        public int ItemSmallThumbNailHeight { get; set; }
        public int ItemSmallThumbNailWidth { get; set; }
        public string ResizeImagesProportionally { get; set; }
        public ImageSettings(AspxCommonInfo aspxCommonObj)
        {
            int storeId = aspxCommonObj.StoreID;
            int portalId = aspxCommonObj.PortalID;
            String culture = aspxCommonObj.CultureName;
            StoreSettingConfig ssc = new StoreSettingConfig();
            this.ItemLargeThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, storeId, portalId, culture));
            this.ItemLargeThumbNailWidth =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, storeId, portalId, culture));
            this.ItemMediumThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, storeId, portalId, culture));
            this.ItemMediumThumbNailWidth =
               Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, storeId, portalId, culture));
            this.ItemSmallThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, storeId, portalId, culture));
            this.ItemSmallThumbNailWidth =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, storeId, portalId, culture));
            this.ResizeImagesProportionally =
               ssc.GetStoreSettingsByKey(StoreSetting.ResizeImagesProportionally, storeId, portalId, culture);
      
        }

    }
}
