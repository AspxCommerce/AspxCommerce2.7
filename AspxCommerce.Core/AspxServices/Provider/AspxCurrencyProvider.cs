using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Web;

namespace AspxCommerce.Core
{
    public class AspxCurrencyProvider
    {
        public AspxCurrencyProvider()
        {
        }

        public static List<CurrencyInfo> BindCurrencyList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CurrencyInfo>("usp_Aspx_BindCurrencyList", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static List<CurrencyInfo> BindCurrencyAddedLists(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset",offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CurrencyInfo>("usp_Aspx_BindCurrencyAddedList", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CurrencyInfoByCode> GetDetailsByCountryCode(string countryCode, string countryName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CountryCode", countryCode));
                parameter.Add(new KeyValuePair<string, object>("@CountryName", countryName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CurrencyInfoByCode>("[dbo].[usp_Aspx_GetDetailsByCountryCode]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsertNewCurrency(AspxCommonInfo aspxCommonObj, CurrencyInfo currencyInsertObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CurrencyID", currencyInsertObj.CurrencyID));
                parameter.Add(new KeyValuePair<string, object>("@CurrencyName", currencyInsertObj.CurrencyName));
                parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyInsertObj.CurrencyCode));
                parameter.Add(new KeyValuePair<string, object>("@CurrencySymbol", currencyInsertObj.CurrencySymbol));
                parameter.Add(new KeyValuePair<string, object>("@CountryName", currencyInsertObj.CountryName));
                parameter.Add(new KeyValuePair<string, object>("@Region", currencyInsertObj.Region));
                parameter.Add(new KeyValuePair<string, object>("@ConversionRate", currencyInsertObj.ConversionRate));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", currencyInsertObj.DisplayOrder));
                parameter.Add(new KeyValuePair<string, object>("@BaseImage", currencyInsertObj.BaseImage));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", currencyInsertObj.IsActive));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("[usp_Aspx_AddUpdateCurrency]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniquenessForDisplayOrderForCurrency(AspxCommonInfo aspxCommonObj, int value, int currencyID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@Value", value));
                Parameter.Add(new KeyValuePair<string, object>("@CurrencyID", currencyID));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForDisplayOrderForCurrency]", Parameter, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckCurrencyCodeUniqueness(AspxCommonInfo aspxCommonObj, string currencyCode, int currencyID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
                Parameter.Add(new KeyValuePair<string, object>("@CurrencyID", currencyID));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCurrCodeUniqueness]", Parameter, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static double GetRatefromTable(AspxCommonInfo aspxCommonObj, string currencyCode)
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
            Parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
            decimal rate = sqlH.ExecuteAsScalar<decimal>("[dbo].[usp_Aspx_GetRateFromTable]", Parameter);
            return Convert.ToDouble(rate);
        }

        public static void UpdateRealTimeRate(AspxCommonInfo aspxCommonObj, string currencyCode, double rate)
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
            Parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
            Parameter.Add(new KeyValuePair<string, object>("@Rate", rate));
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateRealTimeRate]", Parameter);

        }

        public static void SetStorePrimary(AspxCommonInfo aspxCommonObj, string currencyCode)
        {
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
            Parameter.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SetStorePrimary]", Parameter);
            StoreSettingConfig ssc = new StoreSettingConfig();
            HttpContext.Current.Cache.Remove("AspxStoreSetting" + aspxCommonObj.PortalID.ToString() + aspxCommonObj.StoreID.ToString());
            ssc.ResetStoreSettingKeys(aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
        }
        public static void DeleteMultipleCurrencyByCurrencyID(string currencyIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@CurrencyIDs", currencyIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleCurrencyByCurrencyID", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<CurrrencyRateInfo> GetCountryCodeRates(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);               
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CurrrencyRateInfo>("[dbo].[usp_Aspx_GetCountryCodeRates]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
