using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using System.Data;

namespace AspxCommerce.LatestItems
{
    public class AspxLatestItemsController
    {
        public AspxLatestItemsController()
        {
        }

        public List<LatestItemsInfo> GetLatestItemsByCount(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                AspxLatestItemsProvider objLatestItems = new AspxLatestItemsProvider();
                List<LatestItemsInfo> LatestItems = objLatestItems.GetLatestItemsByCount(aspxCommonObj, count);
                return LatestItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetLatestItemsInfo(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxLatestItemsProvider objLatestItems = new AspxLatestItemsProvider();
               return objLatestItems.GetLatestItemsByCount(aspxCommonObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LatestItemSettingInfo GetLatestItemSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxLatestItemsProvider objLatestItems = new AspxLatestItemsProvider();
                LatestItemSettingInfo objLatestSetting = new LatestItemSettingInfo();
                objLatestSetting = objLatestItems.GetLatestItemSetting(aspxCommonObj);
                return objLatestSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LatestItemSettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxLatestItemsProvider objLatestItems = new AspxLatestItemsProvider();
                objLatestItems.LatestItemSettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LatestItemRssInfo> GetLatestRssFeedContent(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
              
                AspxLatestItemsProvider objLatestItem = new AspxLatestItemsProvider();
                List<LatestItemRssInfo> itemRss = objLatestItem.GetLatestItemRssContent(aspxCommonObj, count);
                return itemRss;
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
