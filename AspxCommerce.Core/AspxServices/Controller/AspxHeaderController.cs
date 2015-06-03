using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxHeaderController
    {
        public AspxHeaderController()
        {
        }

        public static int GetCartItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int cartItCount = AspxHeaderProvider.GetCartItemsCount(aspxCommonObj);
                return cartItCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HeaderItemsCount GetHeaderItemsCount(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxHeaderProvider objHeader = new AspxHeaderProvider();
                return objHeader.GetHeaderItemsCount(aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int CountWishItems(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int wishItCount = AspxHeaderProvider.CountWishItems(aspxCommonObj);
                return wishItCount;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SetHeaderSetting(string headerType, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxHeaderProvider.SetHeaderSetting(headerType, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static HeaderSettingInfo GetHeaderSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
               return AspxHeaderProvider.GetHeaderSetting(aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
