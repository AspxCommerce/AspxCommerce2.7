using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxShoppingBagController
    {
        public static void SetShoppingBagSetting(string bagType, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxShoppingBagProvider.SetShoppingBagSetting(bagType, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string GetShoppingBagSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string bagType = AspxShoppingBagProvider.GetShoppingBagSetting(aspxCommonObj);
                return bagType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
