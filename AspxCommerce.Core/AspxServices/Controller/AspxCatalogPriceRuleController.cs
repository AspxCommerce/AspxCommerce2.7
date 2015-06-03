using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxCatalogPriceRuleController
    {
       public AspxCatalogPriceRuleController()
       {
       }

       public static List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
       {
           List<PricingRuleAttributeInfo> lstPriceRuleAttr = AspxCatalogPriceRuleProvider.GetPricingRuleAttributes(aspxCommonObj);
           return lstPriceRuleAttr;
       }

       public static List<CatalogPriceRulePaging> GetCatalogPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
       {
           List<CatalogPriceRulePaging> lstCatalogPriceRule = AspxCatalogPriceRuleProvider.GetCatalogPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
           return lstCatalogPriceRule;
       }

       public static CatalogPricingRuleInfo GetCatalogPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
       {
           DataSet ds = new DataSet();
           ds = AspxCatalogPriceRuleProvider.GetCatalogPricingRule(catalogPriceRuleID, aspxCommonObj);
           DataTable dtCatalogPricingRule = ds.Tables[0];
           DataTable dtCatalogPriceRuleCondition = ds.Tables[1];
           DataTable dtCatalogConditionDetails = ds.Tables[2];
           DataTable dtCatalogPriceRuleRoles = ds.Tables[3];
           List<CatalogPriceRule> lstCatalogPriceRule;
           lstCatalogPriceRule = DataSourceHelper.FillCollection<CatalogPriceRule>(dtCatalogPricingRule);

           List<CatalogPriceRuleCondition> lstCatalogPriceRuleCondition;
           lstCatalogPriceRuleCondition = DataSourceHelper.FillCollection<CatalogPriceRuleCondition>(dtCatalogPriceRuleCondition);

           List<CatalogPriceRuleRole> lstCatalogPriceRuleRole;
           lstCatalogPriceRuleRole = DataSourceHelper.FillCollection<CatalogPriceRuleRole>(dtCatalogPriceRuleRoles);

           List<CatalogConditionDetail> lstCatalogConditionDetail;
           lstCatalogConditionDetail = DataSourceHelper.FillCollection<CatalogConditionDetail>(dtCatalogConditionDetails);

           CatalogPricingRuleInfo catalogPricingRuleInfo = new CatalogPricingRuleInfo();
           CatalogPriceRule catalogPriceRule = lstCatalogPriceRule[0];
           catalogPricingRuleInfo.CatalogPriceRule = catalogPriceRule;
           List<CatalogPriceRuleCondition> lstCPRC = new List<CatalogPriceRuleCondition>();
           foreach (CatalogPriceRuleCondition catalogPriceRuleCondition in lstCatalogPriceRuleCondition)
           {
               List<CatalogConditionDetail> lstCCD = new List<CatalogConditionDetail>();
               foreach (CatalogConditionDetail catalogConditionDetail in lstCatalogConditionDetail)
               {
                   if (catalogPriceRuleCondition.CatalogPriceRuleConditionID == catalogConditionDetail.CatalogPriceRuleConditionID)
                   {
                       lstCCD.Add(catalogConditionDetail);
                   }
               }
               catalogPriceRuleCondition.CatalogConditionDetail = lstCCD;
               lstCPRC.Add(catalogPriceRuleCondition);
           }
           catalogPricingRuleInfo.CatalogPriceRuleConditions = lstCPRC;

           List<CatalogPriceRuleRole> lstCPRR = new List<CatalogPriceRuleRole>();
           foreach (CatalogPriceRuleRole catalogPriceRuleRole in lstCatalogPriceRuleRole)
           {
               if (catalogPriceRuleRole.CatalogPriceRuleID == catalogPriceRule.CatalogPriceRuleID)
               {
                   lstCPRR.Add(catalogPriceRuleRole);
               }
           }
           catalogPricingRuleInfo.CatalogPriceRuleRoles = lstCPRR;

           return catalogPricingRuleInfo;
       }

       public static int SaveCatalogPricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, List<int> parentID)
       {

           int catalogPriceRuleID = -1;
           catalogPriceRuleID = AspxCatalogPriceRuleProvider.CatalogPriceRuleAdd(objCatalogPricingRuleInfo.CatalogPriceRule, aspxCommonObj);

           if (catalogPriceRuleID > 0)
           {
               int count = 0;
               int cpID = 0;
               int catalogConditionID = 0;
               List<int> lstParent = parentID;//new JavaScriptSerializer().ConvertToType<List<object>>(parentID);

               foreach (CatalogPriceRuleCondition catalogPriceRuleCondition in objCatalogPricingRuleInfo.CatalogPriceRuleConditions)
               {
                   if (count == 1 && catalogConditionID > 1)
                       cpID = catalogConditionID - 1;
                   catalogPriceRuleCondition.ParentID = cpID + int.Parse(lstParent[count].ToString());
                   catalogPriceRuleCondition.CatalogPriceRuleID = catalogPriceRuleID;
                   catalogConditionID = AspxCatalogPriceRuleProvider.CatalogPriceRuleConditionAdd(catalogPriceRuleCondition, aspxCommonObj);
                   count++;
               }

               int catalogPriceRuleRoleID = -1;
               foreach (CatalogPriceRuleRole catalogPriceRuleRole in objCatalogPricingRuleInfo.CatalogPriceRuleRoles)
               {
                   catalogPriceRuleRole.CatalogPriceRuleID = catalogPriceRuleID;
                   catalogPriceRuleRoleID = AspxCatalogPriceRuleProvider.CatalogPriceRuleRoleAdd(catalogPriceRuleRole, aspxCommonObj);
               }
           }
           return 1;
       }

       public void ApplyCatalogPricingRule(AspxCommonInfo aspxCommonObj)
       {
           AspxCatalogPriceRuleProvider objCatalog = new AspxCatalogPriceRuleProvider();
           objCatalog.ApplyCatalogPricingRule(aspxCommonObj);
          
       }

       public bool CheckCatalogRuleExist(AspxCommonInfo aspxCommonObj)
       {
           AspxCatalogPriceRuleProvider objCatalog = new AspxCatalogPriceRuleProvider();
           return objCatalog.CheckCatalogRuleExist(aspxCommonObj);
       }
       public static int CatalogPriceRuleDelete(int catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
       {
           int retValue = AspxCatalogPriceRuleProvider.CatalogPriceRuleDelete(catalogPriceRuleID, aspxCommonObj);
           return retValue;
       }

       public static int CatalogPriceMultipleRulesDelete(string catRulesIds, AspxCommonInfo aspxCommonObj)
       {
           int retValue= AspxCatalogPriceRuleProvider.CatalogPriceRulesMultipleDelete(catRulesIds, aspxCommonObj);
           return retValue;
       }

       public static bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               bool isUnique = AspxCatalogPriceRuleProvider.CheckCatalogPriorityUniqueness(catalogPriceRuleID, priority, aspxCommonObj);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
