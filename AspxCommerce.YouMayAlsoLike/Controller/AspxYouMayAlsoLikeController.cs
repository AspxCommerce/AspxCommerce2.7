using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;

namespace AspxCommerce.YouMayAlsoLike
{
    public class AspxYouMayAlsoLikeController
    {
        public AspxYouMayAlsoLikeController()
        { 
        }
        public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItemsListByItemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                AspxYouMayAlsoLikeProvider objYouMayLike = new AspxYouMayAlsoLikeProvider();
                List<YouMayAlsoLikeInfo> lstYouMayLike = objYouMayLike.GetYouMayAlsoLikeItemsListByItemSKU(itemSKU, aspxCommonObj, count);
                return lstYouMayLike;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<YouMayAlsoLikeInfo> GetYouMayAlsoLikeItems(string itemSKU, AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                AspxYouMayAlsoLikeProvider objYouMayLike = new AspxYouMayAlsoLikeProvider();
                List<YouMayAlsoLikeInfo> lstYouMayLike = objYouMayLike.GetYouMayAlsoLikeItems(itemSKU, aspxCommonObj, count);
                return lstYouMayLike;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public YouMayAlsoLikeSettingInfo GetYouMayAlsoLikeSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxYouMayAlsoLikeProvider objYouMayLike = new AspxYouMayAlsoLikeProvider();
                YouMayAlsoLikeSettingInfo lstYouMayAlsoLike = objYouMayLike.GetYouMayAlsoLikeSetting(aspxCommonObj);
                return lstYouMayAlsoLike;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void YouMayAlsoLikeSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxYouMayAlsoLikeProvider objYouMayLike = new AspxYouMayAlsoLikeProvider();
                objYouMayLike.YouMayAlsoLikeSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
