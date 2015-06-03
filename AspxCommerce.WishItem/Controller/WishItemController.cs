using System;
using System.Collections.Generic;
using System.Data;
using AspxCommerce.Core;

namespace AspxCommerce.WishItem
{
    public class WishItemController
    {

        public WishItemController()
        {
        }

        public bool CheckWishItems(int ID, string costVariantValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                bool isExist = provider.CheckWishItems(ID, costVariantValueIDs, aspxCommonObj);
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveWishItems(SaveWishListInfo saveWishListInfo, AspxCommonInfo aspxCommonObj)
        {
            WishItemProvider provider = new WishItemProvider();
            provider.SaveWishItems(saveWishListInfo, aspxCommonObj);
        }

        public List<WishItemsInfo> GetWishItemList(int offset, int limit, AspxCommonInfo aspxCommonObj, string flagShowAll, int count, int sortBy)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                List<WishItemsInfo> lstWishItem = provider.GetWishItemList(offset, limit, aspxCommonObj, flagShowAll, count, sortBy);
                return lstWishItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WishItemsInfo> GetRecentWishItemList(AspxCommonInfo aspxCommonObj, string flagShowAll, int count)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                List<WishItemsInfo> lstWishItem = provider.GetRecentWishItemList(aspxCommonObj, flagShowAll, count);
                return lstWishItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteWishItem(string wishItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                provider.DeleteWishItem(wishItemID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateWishList(string wishItemID, string comment, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                provider.UpdateWishList(wishItemID, comment, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearWishList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                provider.ClearWishList(aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CountWishItems(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                int countWish = provider.CountWishItems(aspxCommonObj);
                return countWish;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveShareWishListEmailMessage(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                provider.SaveShareWishListEmailMessage(aspxCommonObj, wishlistObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendShareWishItemEmail(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            try
            {
                WishItemProvider provider = new WishItemProvider();
                provider.SendShareWishItemEmail(aspxCommonObj, wishlistObj);
                SaveShareWishListEmailMessage(aspxCommonObj, wishlistObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public WishItemsSettingInfo GetWishItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                WishItemProvider wip = new WishItemProvider();
                WishItemsSettingInfo wishSetting = wip.GetWishItemsSetting(aspxCommonObj);
                return wishSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetWishItemListDataSet(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return WishItemProvider.GetWishItemList(aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateWishItemsSetting(AspxCommonInfo aspxCommonObj, WishItemsSettingKeyPairInfo wishlist)
        {
            try
            {
                WishItemProvider wip = new WishItemProvider();
                wip.SaveAndUpdateWishItemsSetting(aspxCommonObj, wishlist);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
