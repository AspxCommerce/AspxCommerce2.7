using System;
using System.Collections.Generic;
using System.Text;
using AspxCommerce.Core;
using System.Web;
using System.Xml;
using SageFrame.Web;
using System.Collections;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsController
    {
        public PopularTagsController() { }

        public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                List<TagDetailsInfo> lstTagDetail = ptp.GetAllPopularTags(aspxCommonObj, count);
                return lstTagDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                return ptp.GetPopularTagsSetting(aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                return ptp.GetPopularTagsSettingValueByKey(aspxCommonObj, settingKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUpdatePopularTagsSetting(AspxCommonInfo aspxCommonObj, PopularTagsSettingKeyPair pTSettingList)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                ptp.SaveUpdatePopularTagsSetting(aspxCommonObj, pTSettingList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PopularTagsRssFeedInfo> GetRssFeedContens(AspxCommonInfo aspxCommonObj, string pageURL, string rssOption, int count)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                List<PopularTagsRssFeedInfo> popularTagRss = ptp.GetPopularTagsRssContent(aspxCommonObj, rssOption, count);
                return popularTagRss;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Hashtable hst = null;
        private static string getLocale(string messageKey)
        {
            string modulePath = "~/Modules/AspxCommerce/AspxPopularTags/";
            hst = AppLocalized.getLocale(modulePath);
            string retStr = messageKey;
            if (hst != null && hst[messageKey] != null)
            {
                retStr = hst[messageKey].ToString();
            }
            return retStr;
        }

        public static List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> lstItemBasic = PopularTagsProvider.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
                return lstItemBasic;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<PopularTagsRssFeedInfo> GetPopularTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                List<PopularTagsRssFeedInfo> popularTagRss = ptp.GetPopularTagsRssContent(aspxCommonObj, rssOption, count);
                return popularTagRss;               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<RssFeedNewTags> GetNewItemTagRssFeedContent(AspxCommonInfo aspxCommonObj, XmlTextWriter rssXml, string pageURL, string rssOption, int count)
        {
            try
            {
                PopularTagsProvider ptp = new PopularTagsProvider();
                List<RssFeedNewTags> popularTagRss = ptp.GetNewTagsRssContent(aspxCommonObj, rssOption, count);
                return popularTagRss;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
