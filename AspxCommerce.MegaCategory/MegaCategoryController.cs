using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;

namespace AspxCommerce.MegaCategory
{
    public class MegaCategoryController
    {
        public  MegaCategorySettingInfo GetMegaCategorySetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                MegaCategoryProvider mega = new MegaCategoryProvider();
                MegaCategorySettingInfo lstGetMegaCategorySetting = mega.GetMegaCategorySetting(aspxCommonObj);
                return lstGetMegaCategorySetting;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  List<MegaCategorySettingInfo> MegaCategorySettingUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                MegaCategoryProvider mega = new MegaCategoryProvider();
                List<MegaCategorySettingInfo> lstProductLister = mega.MegaCategorySettingUpdate(SettingValues, SettingKeys, aspxCommonObj);
                return lstProductLister;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  List<MegaCategoryViewInfo> GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
        {
            MegaCategoryProvider mega = new MegaCategoryProvider();
            List<MegaCategoryViewInfo> catInfo = mega.GetCategoryMenuList(aspxCommonObj);
            return catInfo;
        }

    }
}
