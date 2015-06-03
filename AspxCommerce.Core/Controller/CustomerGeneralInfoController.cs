using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CustomerGeneralInfoController
    {
        public static CustomerGeneralInfo CustomerIDGetByUsername(string userName, int portalID, int storeID)
        {
            try
            {
                CustomerGeneralInfo sageCustInfo = CustomerGeneralInfoSQLProvider.CustomerIDGetByUsername(userName, portalID, storeID);
                return sageCustInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}