using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AspxCommerce.Core;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsController
    {
        public List<SpecialItemsInfo> GetSpecialItems(AspxCommonInfo aspxCommonObj, int count)
        {
            SpecialItemsProvider sip = new SpecialItemsProvider();
            List<SpecialItemsInfo> slInfo = sip.GetSpecialItems(aspxCommonObj, count);
            return slInfo;
        }

        public SpecialItemsSettingInfo GetSpecialItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SpecialItemsProvider sip = new SpecialItemsProvider();
                SpecialItemsSettingInfo objSpecialSetting;
                objSpecialSetting = sip.GetSpecialItemsSetting(aspxCommonObj);
                return objSpecialSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateSpecialSetting(AspxCommonInfo aspxCommonObj, SpecialItemsSettingKeyPairInfo specialObj)
        {
            SpecialItemsProvider sip = new SpecialItemsProvider();
            sip.SaveAndUpdateSpecialItemsSetting(aspxCommonObj, specialObj);
        }

        public List<RssFeedItemInfo> GetItemRssFeedContents(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {
                SpecialItemsProvider sip = new SpecialItemsProvider();
                List<RssFeedItemInfo> itemRss = sip.GetItemRssContent(aspxCommonObj, rssOption, count);
                return itemRss;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SpecialItemsInfo> GetAllSpecialItems(int offset, int limit, AspxCommonInfo aspxCommonObj, int sortBy, int rowTotal)
        {
            try
            {
                SpecialItemsProvider sip = new SpecialItemsProvider();
                List<SpecialItemsInfo> lstSpeDetail = sip.GetAllSpecialItems(offset, limit, aspxCommonObj, sortBy, rowTotal);
                return lstSpeDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetSpecialItemsandSettingDataSet(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                return SpecialItemsProvider.GetSpecialItemsandSettingDataSet(aspxCommonObj);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
