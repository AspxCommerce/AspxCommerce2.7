using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using SageFrame.Common;

namespace AspxCommerce.Core
{
    public class AspxShoppingBagProvider
    {
        public static string GetShoppingBagSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlHandle = new SQLHandler();
                string bagType = sqlHandle.ExecuteAsScalar<string>("[usp_Aspx_GetShoppingBagSettings]", parameterCollection);
                return bagType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void SetShoppingBagSetting(string bagType, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@BagType", bagType));
            SQLHandler sqlhandle = new SQLHandler();
            sqlhandle.ExecuteNonQuery("[usp_Aspx_UpdateShoppingBagSettings]", parameterCollection);
        }
    }
}
