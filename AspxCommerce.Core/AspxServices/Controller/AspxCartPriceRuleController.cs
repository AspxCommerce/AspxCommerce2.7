using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxCartPriceRuleController
    {
       public AspxCartPriceRuleController()
       {
       }

       public static List<ShippingMethodInfo> GetShippingMethods(System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<ShippingMethodInfo> lstShipMethod = AspxCartPriceRuleProvider.GetShippingMethods(isActive,aspxCommonObj);
               return lstShipMethod;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
       {
           List<CartPricingRuleAttributeInfo> lst = AspxCartPriceRuleProvider.GetCartPricingRuleAttributes(aspxCommonObj);
           return lst;
       }

       public static int SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, AspxCommonInfo aspxCommonObj, List<int> parentID)
       {
           SQLHandler sqlH = new SQLHandler();
           SqlTransaction tran;
           tran = (SqlTransaction)sqlH.GetTransaction();
           try
           {
               int cartPriceRuleID = -1;
               cartPriceRuleID = AspxCartPriceRuleProvider.CartPriceRuleAdd(objCartPriceRule.CartPriceRule, tran, aspxCommonObj);

               AspxCartPriceRuleProvider.RuleConditionAdd(objCartPriceRule.LstRuleCondition, cartPriceRuleID, parentID, tran, aspxCommonObj);

               foreach (CartPriceRuleRole cartPriceRuleRole in objCartPriceRule.LstCartPriceRuleRoles)
               {
                   cartPriceRuleRole.CartPriceRuleID = cartPriceRuleID;
                   AspxCartPriceRuleProvider.CartPriceRuleRoleAdd(cartPriceRuleRole, tran, aspxCommonObj);
               }

               //foreach (CartPriceRuleStore cartPriceRuleStore in objCartPriceRule.LstCartPriceRuleStores)
               //{
               //    cartPriceRuleStore.CartPriceRuleID = cartPriceRuleID;
               //    AspxCartPriceRuleProvider.CartPriceRuleStoreAdd(cartPriceRuleStore, tran, aspxCommonObj);
               //}
               tran.Commit();
               return cartPriceRuleID;
           }

           catch (SqlException sqlEX)
           {
               tran.Rollback();
               throw new ArgumentException(sqlEX.Message);
           }
           catch (Exception ex)
           {
               tran.Rollback();
               throw ex;
           }
       }

       public static List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
       {
           List<CartPriceRulePaging> lstCartPriceRule = AspxCartPriceRuleProvider.GetCartPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
           return lstCartPriceRule;
       }


       public static CartPricingRuleInfo GetCartPriceRules(Int32 cartPriceRuleID, AspxCommonInfo aspxCommonObj)
       {
           CartPricingRuleInfo cartPricingRuleInfo = new CartPricingRuleInfo();
           DataSet ds = new DataSet();
           ds = AspxCartPriceRuleProvider.GetCartPriceRule(cartPriceRuleID, aspxCommonObj);
           DataTable dtCartPricingRule = ds.Tables[0];
           DataTable dtRuleConditions = ds.Tables[1];
           DataTable dtCartPriceRuleRoles = ds.Tables[2];
           DataTable dtCartPriceRuleStores = ds.Tables[3];

           List<CartPriceRule> lstCartPriceRule;
           lstCartPriceRule = DataSourceHelper.FillCollection<CartPriceRule>(dtCartPricingRule);
           List<RuleCondition> lstRuleConditions;
           lstRuleConditions = DataSourceHelper.FillCollection<RuleCondition>(dtRuleConditions);
           List<CartPriceRuleRole> lstCartPriceRuleRole;
           lstCartPriceRuleRole = DataSourceHelper.FillCollection<CartPriceRuleRole>(dtCartPriceRuleRoles);
           List<CartPriceRuleStore> lstCartPriceRuleStore;
           lstCartPriceRuleStore = DataSourceHelper.FillCollection<CartPriceRuleStore>(dtCartPriceRuleStores);

           cartPricingRuleInfo.CartPriceRule = lstCartPriceRule[0];
           List<RuleCondition> lstRC = new List<RuleCondition>();
           foreach (RuleCondition rc in lstRuleConditions)
           {
               RuleCondition objRC = new RuleCondition();
               objRC.ParentID = rc.ParentID;
               objRC.RuleConditionID = rc.RuleConditionID;
               objRC.RuleConditionType = rc.RuleConditionType;
               objRC.CartPriceRuleID = rc.CartPriceRuleID;

               if (rc.RuleConditionType.ToUpper().Trim() == "PAC".ToUpper().Trim())
               {
                   objRC.LstProductAttributeRuleConditions =
                       AspxCartPriceRuleProvider.GetCartPriceProductAttributeConditions(rc.RuleConditionID, aspxCommonObj.PortalID);
                   objRC.LstProductAttributeRuleConditions[0].LstCartConditionDetails =
                       AspxCartPriceRuleProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID, rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                   lstRC.Add(objRC);
               }
               else if (rc.RuleConditionType.ToUpper().Trim() == "PS".ToUpper().Trim())
               {
                   objRC.LstProductSublectionRuleConditions =
                       AspxCartPriceRuleProvider.GetCartPriceSubSelections(rc.RuleConditionID, aspxCommonObj.PortalID);
                   objRC.LstProductSublectionRuleConditions[0].LstCartConditionDetails =
                      AspxCartPriceRuleProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID, rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                   lstRC.Add(objRC);

               }
               else if (rc.RuleConditionType.ToUpper().Trim() == "CC".ToUpper().Trim())
               {
                   objRC.LstCartPriceRuleConditions =
                       AspxCartPriceRuleProvider.GetCartPriceRuleConditions(rc.RuleConditionID, aspxCommonObj.PortalID);
                   objRC.LstCartPriceRuleConditions[0].LstCartConditionDetails =
                      AspxCartPriceRuleProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID, rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                   lstRC.Add(objRC);
               }
           }
           cartPricingRuleInfo.LstRuleCondition = lstRC;
           cartPricingRuleInfo.LstCartPriceRuleRoles = lstCartPriceRuleRole;
           cartPricingRuleInfo.LstCartPriceRuleStores = lstCartPriceRuleStore;

           return cartPricingRuleInfo;
       }

       public static int CartPriceRuleDelete(int cartPriceRuleID, AspxCommonInfo aspxCommonObj)
       {
           int retValue = AspxCartPriceRuleProvider.CartPriceRuleDelete(cartPriceRuleID, aspxCommonObj);
           return retValue;
       }

       public static int CartPriceMultipleRulesDelete(string cartRulesIds, AspxCommonInfo aspxCommonObj)
       {
           int retValue= AspxCartPriceRuleProvider.CartPriceRulesMultipleDelete(cartRulesIds, aspxCommonObj);
           return retValue;
       }

       public static bool CheckCartPricePriorityUniqueness(int cartPriceRuleID, int priority, int portalID)
       {
           try
           {
               bool isUnique = AspxCartPriceRuleProvider.CheckCartPricePriorityUniqueness(cartPriceRuleID, priority, portalID);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

    }
}
