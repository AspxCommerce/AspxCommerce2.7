using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxTaxMgntProvider
    {
       public AspxTaxMgntProvider()
       {
       }

       //--------------item tax classes------------------
       
       public static List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ItemClassName", itemClassName));
               SQLHandler sqlh = new SQLHandler();
               List<TaxItemClassInfo> lstTaxItem= sqlh.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClasses", parameter);
               return lstTaxItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------------save item tax class--------------------
       
       public static void SaveAndUpdateTaxItemClass(int taxItemClassID, string taxItemClassName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxItemClassID", taxItemClassID));
               parameter.Add(new KeyValuePair<string, object>("@TaxItemClassName", taxItemClassName));
               SQLHandler sqlh = new SQLHandler();
               sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxItemClass", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckTaxClassUniqueness(AspxCommonInfo aspxCommonObj, string taxItemClassName)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxClassName", taxItemClassName));
               SQLHandler sqlh = new SQLHandler();
               bool isUnique = sqlh.ExecuteNonQueryAsBool("[usp_Aspx_CheckTaxClassUniqueness]", parameter, "@IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------------Delete tax item classes --------------------------------
       
       public static void DeleteTaxItemClass(string taxItemClassIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxItemClassIDs", taxItemClassIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxItemClass", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------customer tax classes------------------
       
       public static List<TaxCustomerClassInfo> GetTaxCustomerClassDetails(int offset, int limit, string className, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@ClassName", className));
               SQLHandler sqlh = new SQLHandler();
               List<TaxCustomerClassInfo> lstTaxCtClass= sqlh.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetTaxCustomerClass", parameter);
               return lstTaxCtClass;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------------save customer tax class--------------------
       
       public static void SaveAndUpdateTaxCustmerClass(int taxCustomerClassID, string taxCustomerClassName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassID", taxCustomerClassID));
               parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassName", taxCustomerClassName));
               SQLHandler sqlh = new SQLHandler();
               sqlh.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxCustomerClass", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-----------------Delete tax customer classes --------------------------------
       
       public static void DeleteTaxCustomerClass(string taxCustomerClassIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxCustomerClassIDs", taxCustomerClassIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxCustomerClass", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------tax rates------------------
       
       public static List<TaxRateInfo> GetTaxRateDetails(int offset, System.Nullable<int> limit, TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@TaxName", taxRateDataObj.TaxName));
               parameter.Add(new KeyValuePair<string, object>("@SearchCountry", taxRateDataObj.Country));
               parameter.Add(new KeyValuePair<string, object>("@SerachState", taxRateDataObj.State));
               parameter.Add(new KeyValuePair<string, object>("@Zip", taxRateDataObj.Zip));
               SQLHandler sqlh = new SQLHandler();
               List<TaxRateInfo> lstTaxRate= sqlh.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRates", parameter);
               return lstTaxRate;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //----------------- save and update tax rates--------------------------
       
       public static void SaveAndUpdateTaxRates(TaxRateDataTnfo taxRateDataObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxRateID", taxRateDataObj.TaxRateID));
               parameter.Add(new KeyValuePair<string, object>("@TaxRateTitle", taxRateDataObj.TaxRateTitle));
               parameter.Add(new KeyValuePair<string, object>("@TaxCountryCode", taxRateDataObj.Country));
               parameter.Add(new KeyValuePair<string, object>("@TaxStateCode", taxRateDataObj.State));
               parameter.Add(new KeyValuePair<string, object>("@ZipPostCode", taxRateDataObj.Zip));
               parameter.Add(new KeyValuePair<string, object>("@IsZipPostRange", taxRateDataObj.IsZipPostRange));
               parameter.Add(new KeyValuePair<string, object>("@TaxRateValue", taxRateDataObj.TaxRateValue));
               parameter.Add(new KeyValuePair<string, object>("@RateType", taxRateDataObj.RateType));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxRates", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------dalete Tax rates-----------------------
       
       public static void DeleteTaxRates(string taxRateIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxRateIDs", taxRateIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxRates", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //--------------------------get customer class----------------
       
       public static List<TaxManageRulesInfo> GetTaxRules(int offset, int limit, TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@RuleName", taxRuleDataObj.TaxManageRuleName));
               parameter.Add(new KeyValuePair<string, object>("@RoleName", taxRuleDataObj.RoleName));
               parameter.Add(new KeyValuePair<string, object>("@ItemClassName", taxRuleDataObj.TaxItemClassName));
               parameter.Add(new KeyValuePair<string, object>("@RateTitle", taxRuleDataObj.TaxRateTitle));
               parameter.Add(new KeyValuePair<string, object>("@SearchPriority", taxRuleDataObj.Priority));
               parameter.Add(new KeyValuePair<string, object>("@SearchDisplayOrder", taxRuleDataObj.DisplayOrder));
               SQLHandler sqlh = new SQLHandler();
               List<TaxManageRulesInfo> lstTaxManage= sqlh.ExecuteAsList<TaxManageRulesInfo>("usp_Aspx_GetTaxManageRules", parameter);
               return lstTaxManage;
           }
           catch (Exception e)
           {
               throw e;
           }
       }


       //------------------------bind tax customer class name list-------------------------------
       
       public static List<TaxCustomerClassInfo> GetCustomerTaxClass(int storeID, int portalID)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
               SQLHandler sqlH = new SQLHandler();
               List<TaxCustomerClassInfo> lstTaxCtClass= sqlH.ExecuteAsList<TaxCustomerClassInfo>("usp_Aspx_GetCustomerTaxClassList", parameter);
               return lstTaxCtClass;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------bind tax item class name list-------------------------------
       
       public static List<TaxItemClassInfo> GetItemTaxClass(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<TaxItemClassInfo> lstTaxItClass= sqlH.ExecuteAsList<TaxItemClassInfo>("usp_Aspx_GetItemTaxClassList", parameter);
               return lstTaxItClass;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //------------------------bind tax rate list-------------------------------

       public static List<TaxRateInfo> GetTaxRate(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<TaxRateInfo> lstTaxRate = sqlH.ExecuteAsList<TaxRateInfo>("usp_Aspx_GetTaxRateList", parameter);
               return lstTaxRate;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       //-------------------save and update tax rules--------------------------------------
       
       public static void SaveAndUpdateTaxRule(TaxRuleDataInfo taxRuleDataObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleID", taxRuleDataObj.TaxManageRuleID));
               parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleName", taxRuleDataObj.TaxManageRuleName));
               parameter.Add(new KeyValuePair<string, object>("@RoleID", taxRuleDataObj.RoleID));
               parameter.Add(new KeyValuePair<string, object>("@RoleName", taxRuleDataObj.RoleName));
               parameter.Add(new KeyValuePair<string, object>("@TaxItemClassID", taxRuleDataObj.TaxItemClassID));
               parameter.Add(new KeyValuePair<string, object>("@TaxRateID", taxRuleDataObj.TaxRateID));
               parameter.Add(new KeyValuePair<string, object>("@Priority", taxRuleDataObj.Priority));
               parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", taxRuleDataObj.DisplayOrder));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateTaxRules", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }  
       //-------------- delete Tax Rules----------------------------

       
       public static void DeleteTaxManageRules(string taxManageRuleIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleIDs", taxManageRuleIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteTaxRules", parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static bool CheckTaxUniqueness(AspxCommonInfo aspxCommonObj, int value, int taxRuleID)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@Value", value));
               Parameter.Add(new KeyValuePair<string, object>("@TaxRuleID", taxRuleID));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckUniquenessForTaxRuleDisplayOrder]", Parameter, "@IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       #region Sales Tax Report
       public static List<StoreTaxesInfo> GetStoreSalesTaxes(int offset, int limit, TaxDateData taxDataObj, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@TaxManageRuleName", taxDataObj.taxRuleName));
              
               SQLHandler sqlh = new SQLHandler();
               if (taxDataObj.monthly == true)
               {
                   return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxRuleForStoreTaxReport", parameter);
               }
               if (taxDataObj.weekly == true)
               {                  
                   return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxDetailsByCurrentMonth", parameter);
               }
               if (taxDataObj.hourly == true)
               {
                   return sqlh.ExecuteAsList<StoreTaxesInfo>("usp_Aspx_GetTaxReportDetailsBy24hours", parameter);
               }
               else
                   return new List<StoreTaxesInfo>();
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion
    }
}
