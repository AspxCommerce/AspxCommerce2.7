using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxBreadCrumbController
    {
        public AspxBreadCrumbController()
        {
        }




        public static List<BreadCrumInfo> GetBreadCrumb(string SEOName, AspxCommonInfo commonInfo)
        {
            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbProvider.GetBreadCrumb(SEOName, commonInfo);
                return list;    //}
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static string GetCategoryForItem(int storeID, int portalID, string itemSku, string cultureName)
        {
            try
            {
                string retString = AspxBreadCrumbProvider.GetCategoryForItem(storeID, portalID, itemSku, cultureName);
                return retString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<BreadCrumInfo> GetCategoryName(string name, AspxCommonInfo commonInfo)
        {
            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbProvider.GetCategoryName(name, commonInfo);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static List<BreadCrumInfo> GetItemCategories(string itemName, AspxCommonInfo commonInfo)
        {

            try
            {
                List<BreadCrumInfo> list = AspxBreadCrumbProvider.GetItemCategories(itemName, commonInfo);
                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
