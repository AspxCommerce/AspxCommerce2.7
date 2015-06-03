using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxShipRateController
    {
        public AspxShipRateController()
        {
        }
        public static List<CountryList> LoadCountry()
        {
            List<CountryList> lstCountry = AspxShipRateProvider.LoadCountry();
            return lstCountry;
        }

        public static List<States> GetStatesByCountry(string countryCode)
        {
            List<States> lstState = AspxShipRateProvider.GetStatesByCountry(countryCode);
            return lstState;
        }


        public static List<CommonRateList> GetRate(ItemListDetails itemsDetail)
        {
            try
            {
                List<CommonRateList> lstCommonRate = AspxShipRateProvider.GetRate(itemsDetail);
                return lstCommonRate;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
