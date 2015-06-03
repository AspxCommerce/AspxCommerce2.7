using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
  public class AspxTaxMgntController
    {
      public AspxTaxMgntController()
      {
      }

      //--------------item tax classes------------------

      public static List<TaxItemClassInfo> GetTaxItemClassDetails(int offset, int limit, string itemClassName, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<TaxItemClassInfo> lstTaxItem = AspxTaxMgntProvider.GetTaxItemClassDetails(offset, limit, itemClassName, aspxCommonObj);
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
              AspxTaxMgntProvider.SaveAndUpdateTaxItemClass(taxItemClassID, taxItemClassName, aspxCommonObj);
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

              bool isUnique = AspxTaxMgntProvider.CheckTaxClassUniqueness(aspxCommonObj, taxItemClassName);
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
              AspxTaxMgntProvider.DeleteTaxItemClass(taxItemClassIDs, aspxCommonObj);
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
              List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntProvider.GetTaxCustomerClassDetails(offset, limit, className, aspxCommonObj);
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
              AspxTaxMgntProvider.SaveAndUpdateTaxCustmerClass(taxCustomerClassID, taxCustomerClassName, aspxCommonObj);
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
              AspxTaxMgntProvider.DeleteTaxCustomerClass(taxCustomerClassIDs, aspxCommonObj);
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
              List<TaxRateInfo> lstTaxRate = AspxTaxMgntProvider.GetTaxRateDetails(offset, limit, taxRateDataObj, aspxCommonObj);
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
              AspxTaxMgntProvider.SaveAndUpdateTaxRates(taxRateDataObj, aspxCommonObj);
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
              AspxTaxMgntProvider.DeleteTaxRates(taxRateIDs, aspxCommonObj);
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

              List<TaxManageRulesInfo> lstTaxManage = AspxTaxMgntProvider.GetTaxRules(offset, limit, taxRuleDataObj, aspxCommonObj);
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
              List<TaxCustomerClassInfo> lstTaxCtClass = AspxTaxMgntProvider.GetCustomerTaxClass(storeID, portalID);
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
              List<TaxItemClassInfo> lstTaxItClass = AspxTaxMgntProvider.GetItemTaxClass(aspxCommonObj);
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
              List<TaxRateInfo> lstTaxRate = AspxTaxMgntProvider.GetTaxRate(aspxCommonObj);
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
              AspxTaxMgntProvider.SaveAndUpdateTaxRule(taxRuleDataObj, aspxCommonObj);
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
              AspxTaxMgntProvider.DeleteTaxManageRules(taxManageRuleIDs, aspxCommonObj);
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
              bool isUnique = AspxTaxMgntProvider.CheckTaxUniqueness(aspxCommonObj, value, taxRuleID);
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
              List<StoreTaxesInfo> lstStoreTax = AspxTaxMgntProvider.GetStoreSalesTaxes( offset,  limit, taxDataObj, aspxCommonObj);
              return lstStoreTax;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      #endregion
    }
}
