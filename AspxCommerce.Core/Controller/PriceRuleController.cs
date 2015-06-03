/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SageFrame.Web;
using System.Data;
using SageFrame.Web.Utilities;
using System.Web.Script.Serialization;

namespace AspxCommerce.Core
{
    public class PriceRuleController
    {
        public List<PortalRole> GetPortalRoles(int portalID, bool isAll, string userName)
        {
            string rolePrefix = string.Empty;
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            List<PortalStoreInfo> portalInfoList = priceRuleSqlProvider.GetPortalSeoName(portalID, userName);
            if (portalInfoList.Count > 0)
            {
                rolePrefix = portalInfoList[0].SEOName.Trim() + "_";
            }

            List<PortalRole> portalRoleList = priceRuleSqlProvider.GetPortalRoles(portalID, isAll, userName);
            foreach (PortalRole pr in portalRoleList)
            {
                bool isSystemRole = false;
                foreach (string sysRole in SystemSetting.SYSTEM_ROLES)
                {
                    if (sysRole.ToLower() == pr.RoleName.ToLower())
                    {
                        isSystemRole = true;
                    }
                }
                if (!isSystemRole)
                {
                    pr.RoleName = pr.RoleName.Replace(rolePrefix, "");
                }
            }
            return portalRoleList;
        }

        public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(int portalID, int storeID, string userName, string cultureName)
        {
            List<PricingRuleAttributeInfo> listPricingRuleAttributeInfo = new List<PricingRuleAttributeInfo>();
            return listPricingRuleAttributeInfo;
        }

        public List<CatalogPriceRulePaging> GetCatalogPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            List<CatalogPriceRulePaging> lstCatalogPriceRule = priceRuleSqlProvider.GetCatalogPricingRules(ruleName, startDate, endDate,isActive,aspxCommonObj, offset, limit);
            return lstCatalogPriceRule;
        }

        public CatalogPricingRuleInfo GetCatalogPricingRule(Int32 catalogPriceRuleID,AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            DataSet ds = new DataSet();
            ds = priceRuleSqlProvider.GetCatalogPricingRule(catalogPriceRuleID,aspxCommonObj);
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

        public int SaveCatalogPricingRule(CatalogPricingRuleInfo objCatalogPricingRuleInfo, AspxCommonInfo aspxCommonObj, object parentID)
        {

            int catalogPriceRuleID = -1;
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            catalogPriceRuleID = priceRuleSqlProvider.CatalogPriceRuleAdd(objCatalogPricingRuleInfo.CatalogPriceRule, aspxCommonObj);

            if (catalogPriceRuleID > 0)
            {
                int count = 0;
                int cpID = 0;
                int catalogConditionID = 0;
                List<object> lstParent = new JavaScriptSerializer().ConvertToType<List<object>>(parentID);

                foreach (CatalogPriceRuleCondition catalogPriceRuleCondition in objCatalogPricingRuleInfo.CatalogPriceRuleConditions)
                {
                    if (count == 1 && catalogConditionID > 1)
                        cpID = catalogConditionID - 1;
                    catalogPriceRuleCondition.ParentID = cpID + int.Parse(lstParent[count].ToString());
                    catalogPriceRuleCondition.CatalogPriceRuleID = catalogPriceRuleID;
                    catalogConditionID = priceRuleSqlProvider.CatalogPriceRuleConditionAdd(catalogPriceRuleCondition, aspxCommonObj);
                    count++;
                }

                int catalogPriceRuleRoleID = -1;
                foreach (CatalogPriceRuleRole catalogPriceRuleRole in objCatalogPricingRuleInfo.CatalogPriceRuleRoles)
                {
                    catalogPriceRuleRole.CatalogPriceRuleID = catalogPriceRuleID;
                    catalogPriceRuleRoleID = priceRuleSqlProvider.CatalogPriceRuleRoleAdd(catalogPriceRuleRole,aspxCommonObj);
                }
            }
            return 1;
        }
        
        public int CatalogPriceRuleDelete(int catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            return priceRuleSqlProvider.CatalogPriceRuleDelete(catalogPriceRuleID, aspxCommonObj);
        }

        public int CatalogPriceMultipleRulesDelete(string catRulesIds, AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            return priceRuleSqlProvider.CatalogPriceRulesMultipleDelete(catRulesIds, aspxCommonObj);
        }

        public int SaveCartPricingRule(CartPricingRuleInfo objCartPriceRule, AspxCommonInfo aspxCommonObj, object parentID)
        {
            SQLHandler sqlH = new SQLHandler();
            SqlTransaction tran;
            tran = (SqlTransaction) sqlH.GetTransaction();
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            try
            {
                int cartPriceRuleID = -1;
                cartPriceRuleID = priceRuleSqlProvider.CartPriceRuleAdd(objCartPriceRule.CartPriceRule, tran,aspxCommonObj);

                priceRuleSqlProvider.RuleConditionAdd(objCartPriceRule.LstRuleCondition,cartPriceRuleID,parentID, tran,aspxCommonObj);

                foreach (CartPriceRuleRole cartPriceRuleRole in objCartPriceRule.LstCartPriceRuleRoles)
                {
                    cartPriceRuleRole.CartPriceRuleID = cartPriceRuleID;
                    priceRuleSqlProvider.CartPriceRuleRoleAdd(cartPriceRuleRole, tran,aspxCommonObj);
                }

                foreach (CartPriceRuleStore cartPriceRuleStore in objCartPriceRule.LstCartPriceRuleStores)
                {
                    cartPriceRuleStore.CartPriceRuleID = cartPriceRuleID;
                    priceRuleSqlProvider.CartPriceRuleStoreAdd(cartPriceRuleStore, tran,aspxCommonObj);
                }
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

        public int CartPriceRuleDelete(int cartPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            return priceRuleSqlProvider.CartPriceRuleDelete(cartPriceRuleID,aspxCommonObj);
        }

        public int CartPriceMultipleRulesDelete(string cartRulesIds, AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            return priceRuleSqlProvider.CartPriceRulesMultipleDelete(cartRulesIds,aspxCommonObj);
        }
        
        public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            List<CartPriceRulePaging> lstCartPriceRule = priceRuleSqlProvider.GetCartPricingRules(ruleName, startDate, endDate, isActive, aspxCommonObj, offset, limit);
            
            return lstCartPriceRule;
        }

        public CartPricingRuleInfo GetCartPriceRules(Int32 cartPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
            CartPricingRuleInfo cartPricingRuleInfo = new CartPricingRuleInfo();

            DataSet ds = new DataSet();
            ds = priceRuleSqlProvider.GetCartPriceRule(cartPriceRuleID,aspxCommonObj);
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
                        priceRuleSqlProvider.GetCartPriceProductAttributeConditions(rc.RuleConditionID, aspxCommonObj.PortalID);
                    objRC.LstProductAttributeRuleConditions[0].LstCartConditionDetails =
                        priceRuleSqlProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID,rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                    lstRC.Add(objRC);
                }
                else if (rc.RuleConditionType.ToUpper().Trim() == "PS".ToUpper().Trim())
                {
                    objRC.LstProductSublectionRuleConditions =
                        priceRuleSqlProvider.GetCartPriceSubSelections(rc.RuleConditionID, aspxCommonObj.PortalID);
                    objRC.LstProductSublectionRuleConditions[0].LstCartConditionDetails =
                       priceRuleSqlProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID, rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                    lstRC.Add(objRC);

                }
                else if (rc.RuleConditionType.ToUpper().Trim() == "CC".ToUpper().Trim())
                {
                    objRC.LstCartPriceRuleConditions =
                        priceRuleSqlProvider.GetCartPriceRuleConditions(rc.RuleConditionID, aspxCommonObj.PortalID);
                    objRC.LstCartPriceRuleConditions[0].LstCartConditionDetails =
                       priceRuleSqlProvider.GetCartPriceRuleConditionDetails(rc.CartPriceRuleID, rc.RuleConditionID, aspxCommonObj.PortalID, aspxCommonObj.UserName);
                    lstRC.Add(objRC);
                }
            }
            cartPricingRuleInfo.LstRuleCondition = lstRC;
            cartPricingRuleInfo.LstCartPriceRuleRoles = lstCartPriceRuleRole;
            cartPricingRuleInfo.LstCartPriceRuleStores = lstCartPriceRuleStore;

            return cartPricingRuleInfo;
        }
    }
}