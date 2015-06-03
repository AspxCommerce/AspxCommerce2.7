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
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using System.Web.Script.Serialization;

namespace AspxCommerce.Core
{
    public class PriceRuleSqlProvider
    {
        public string ConnectionString
        {
            get { return SystemSetting.SageFrameConnectionString; }
        }
        public List<PortalStoreInfo> GetPortalSeoName(int portalID, string userName)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", userName));
            return sqlHandler.ExecuteAsList<PortalStoreInfo>("sp_PortalGetByPortalID", paramList);
        }
        public List<PortalRole> GetPortalRoles(int portalID, bool isAll, string userName)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("@IsAll", isAll));
            paramList.Add(new KeyValuePair<string, object>("@UserName", userName));
            return sqlHandler.ExecuteAsList<PortalRole>("sp_PortalRoleList", paramList);
        }


        #region Catalog Pricing Rule
        public List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return sqlHandler.ExecuteAsList<PricingRuleAttributeInfo>("usp_Aspx_GetPricingRuleAttr", paramList);
        }

        public DataSet GetCatalogPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CatalogPriceRuleID", catalogPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            DataSet ds = sqlHandler.ExecuteAsDataSet("usp_Aspx_GetPricingRuleInfoByID", paramList);
            return ds;
        }

        public List<CatalogPriceRulePaging> GetCatalogPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@offset", offset));
            paramList.Add(new KeyValuePair<string, object>("@limit", limit));
            paramList.Add(new KeyValuePair<string, object>("@RuleName", ruleName));
            paramList.Add(new KeyValuePair<string, object>("@StartDate", startDate));
            paramList.Add(new KeyValuePair<string, object>("@EndDate", endDate));
            paramList.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            paramList.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            List<CatalogPriceRulePaging> lstCatalogPriceRule = sqlHandler.ExecuteAsList<CatalogPriceRulePaging>("usp_Aspx_GetPricingRules", paramList);
            return lstCatalogPriceRule;
        }

        public int CatalogPriceRuleAdd(CatalogPriceRule catalogPriceRule, AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleID", catalogPriceRule.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleName", catalogPriceRule.CatalogPriceRuleName));
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleDescription", catalogPriceRule.CatalogPriceRuleDescription));
            sqlCommand.Parameters.Add(new SqlParameter("@Apply", catalogPriceRule.Apply));
            sqlCommand.Parameters.Add(new SqlParameter("@Value", catalogPriceRule.Value));
            sqlCommand.Parameters.Add(new SqlParameter("@IsFurtherProcessing", catalogPriceRule.IsFurtherProcessing));
            sqlCommand.Parameters.Add(new SqlParameter("@FromDate", catalogPriceRule.FromDate));
            sqlCommand.Parameters.Add(new SqlParameter("@ToDate", catalogPriceRule.ToDate));
            sqlCommand.Parameters.Add(new SqlParameter("@Priority", catalogPriceRule.Priority));
            sqlCommand.Parameters.Add(new SqlParameter("@IsActive", catalogPriceRule.IsActive));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleAdd";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int CatalogPriceRuleConditionAdd(CatalogPriceRuleCondition catalogPriceRuleCondition, AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleID", catalogPriceRuleCondition.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsAll", catalogPriceRuleCondition.IsAll));
            sqlCommand.Parameters.Add(new SqlParameter("@IsTrue", catalogPriceRuleCondition.IsTrue));
            sqlCommand.Parameters.Add(new SqlParameter("@ParentID", catalogPriceRuleCondition.ParentID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsActive", true));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleConditionAdd";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                if (Convert.ToInt16(val) > 0)
                {
                    int catalogConditionDetailID = -1;
                    PriceRuleSqlProvider priceRuleSqlProvider = new PriceRuleSqlProvider();
                    foreach (CatalogConditionDetail catalogConditionDetail in catalogPriceRuleCondition.CatalogConditionDetail)
                    {
                        if (catalogConditionDetail != null)
                        {
                            catalogConditionDetail.CatalogPriceRuleConditionID = Convert.ToInt16(val);
                            catalogConditionDetail.CatalogPriceRuleID = catalogPriceRuleCondition.CatalogPriceRuleID;
                            catalogConditionDetailID =
                                priceRuleSqlProvider.CatalogConditionDetailAdd(catalogConditionDetail,aspxCommonObj);
                            if (!(catalogConditionDetailID > 0))
                            {
                            }
                        }
                    }
                }
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }           
        }

        public int CatalogPriceRuleRoleAdd(CatalogPriceRuleRole catalogPriceRuleRole, AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleID", catalogPriceRuleRole.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@RoleID", catalogPriceRuleRole.RoleID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsActive", true));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleRoleAdd";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int CatalogConditionDetailAdd(CatalogConditionDetail catalogConditionDetail, AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleConditionID", catalogConditionDetail.CatalogPriceRuleConditionID));
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleID", catalogConditionDetail.CatalogPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@AttributeID", catalogConditionDetail.AttributeID));
            sqlCommand.Parameters.Add(new SqlParameter("@RuleOperatorID", catalogConditionDetail.RuleOperatorID));
            sqlCommand.Parameters.Add(new SqlParameter("@Value", catalogConditionDetail.Value));
            sqlCommand.Parameters.Add(new SqlParameter("@Priority", catalogConditionDetail.Priority));
            sqlCommand.Parameters.Add(new SqlParameter("@IsActive", true));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogConditionDetailAdd";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int CatalogPriceRuleDelete(int catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRuleID", catalogPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRuleDelete";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int CatalogPriceRulesMultipleDelete(string catRulesIds, AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CatalogPriceRulesIDs", catRulesIds));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CatalogPriceRulesDeleteMultiple";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        #endregion

        #region Cart Pricing rule

        public List<CartPricingRuleAttributeInfo> GetCartPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            List<CartPricingRuleAttributeInfo> lst;
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            lst = sqlHandler.ExecuteAsList<CartPricingRuleAttributeInfo>("usp_Aspx_GetCartPricingRuleAttr", paramList);
            return lst;
        }

        public DataSet GetCartPricingRule(Int32 cartPriceRuleID, Int32 portalID, string userName, string culture)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", userName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", culture));
            DataSet ds = sqlHandler.ExecuteAsDataSet("usp_Aspx_GetCartPricingRuleInfoByID", paramList);
            return ds;
        }

        public DataSet GetCartPriceRule(Int32 cartPriceRuleID,AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            DataSet ds = sqlHandler.ExecuteAsDataSet("[dbo].[usp_Aspx_GetCartPricingRule]", paramList);
            return ds;
        }

        public DataTable GetPriceRuleConditions(Int32 cartPriceRuleID, Int32 portalID, string userName)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", userName));
            DataSet ds = sqlHandler.ExecuteAsDataSet("[dbo].[usp_Aspx_GetPricingRuleConditions]", paramList);
            return ds.Tables[0];
        }
        public List<CartPriceRuleCondition> GetCartPriceRuleConditions(Int32? ruleConditionID, Int32 portalID)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            return sqlHandler.ExecuteAsList<CartPriceRuleCondition>("[dbo].[usp_Aspx_GetCartPriceConditions]", paramList);
        }
        public List<ProductAttributeRuleCondition> GetCartPriceProductAttributeConditions(Int32? ruleConditionID, Int32 portalID)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            return sqlHandler.ExecuteAsList<ProductAttributeRuleCondition>("[dbo].[usp_Aspx_GetProductAttributeCombinations]", paramList);
        }
        public List<ProductSubSelectionRuleCondition> GetCartPriceSubSelections(Int32? ruleConditionID, Int32 portalID)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            return sqlHandler.ExecuteAsList<ProductSubSelectionRuleCondition>("[dbo].[usp_Aspx_GetProductSubSelections]", paramList);
        }

        public List<CartConditionDetail> GetCartPriceRuleConditionDetails(Int32 cartPriceRuleID,Int32? ruleConditionID, Int32 portalID, string userName)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", userName));
            return sqlHandler.ExecuteAsList<CartConditionDetail>("[dbo].[usp_Aspx_CartPriceRuleConditionDetails]", paramList);
        }


        public List<CartPriceRulePaging> GetCartPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@offset", offset));
            paramList.Add(new KeyValuePair<string, object>("@limit", limit));
            paramList.Add(new KeyValuePair<string, object>("@RuleName", ruleName));
            paramList.Add(new KeyValuePair<string, object>("@StartDate", startDate));
            paramList.Add(new KeyValuePair<string, object>("@EndDate", endDate));
            paramList.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            paramList.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            List<CartPriceRulePaging> lstCartPriceRule = sqlHandler.ExecuteAsList<CartPriceRulePaging>("usp_Aspx_GetCartPrincingRules", paramList);
            return lstCartPriceRule;
        }

        public int CartPriceRuleAdd(CartPriceRule cartPriceRule,SqlTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleName", cartPriceRule.CartPriceRuleName));
            parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleDescription", cartPriceRule.CartPriceRuleDescription));
            parameter.Add(new KeyValuePair<string, object>("@Apply", cartPriceRule.Apply));
            parameter.Add(new KeyValuePair<string, object>("@Value", cartPriceRule.Value));
            parameter.Add(new KeyValuePair<string, object>("@ApplytoShippingAmount", cartPriceRule.ApplytoShippingAmount));
            parameter.Add(new KeyValuePair<string, object>("@DiscountQuantity", cartPriceRule.DiscountQuantity));
            parameter.Add(new KeyValuePair<string, object>("@DiscountStep", cartPriceRule.DiscountStep));
            parameter.Add(new KeyValuePair<string, object>("@FreeShipping", cartPriceRule.FreeShipping));
            parameter.Add(new KeyValuePair<string, object>("@IsFurtherProcessing", cartPriceRule.IsFurtherProcessing));
            parameter.Add(new KeyValuePair<string, object>("@FromDate", cartPriceRule.FromDate));
            parameter.Add(new KeyValuePair<string, object>("@ToDate", cartPriceRule.ToDate));
            parameter.Add(new KeyValuePair<string, object>("@Priority", cartPriceRule.Priority));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", cartPriceRule.IsActive));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
           
            try
            {
                SQLHandler sqlH = new SQLHandler();

                if (cartPriceRule.CartPriceRuleID > 0)
                    DeleteCartPricingRuleForEdit(tran, cartPriceRule.CartPriceRuleID, aspxCommonObj.PortalID);
               
                return sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleAdd", parameter, "@CartPriceRuleID");
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCartPricingRuleForEdit(SqlTransaction tran, Int32 cartPriceRuleID, Int32 portalID)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            sqlHandler.ExecuteNonQuery(tran,CommandType.StoredProcedure,"usp_Aspx_DeleteCartPriceForEdit", paramList);
        }


        public int RuleConditionAdd(List<RuleCondition> lstRuleCondition,int cartPriceRuleID,object parentID ,SqlTransaction tran,AspxCommonInfo aspxCommonObj)
        {
            try
            {
                int ruleConditionID = 0;
                int count = 0;
                int rcID = 0;
                List<object> lstParent = new JavaScriptSerializer().ConvertToType<List<object>>(parentID);

                foreach (RuleCondition objRuleCondition in lstRuleCondition)
                {
                    if (count == 1 && ruleConditionID > 1)
                        rcID = ruleConditionID - 1;

                    List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                    parameter.Add(new KeyValuePair<string, object>("@RuleConditionType",objRuleCondition.RuleConditionType));
                    parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
                    parameter.Add(new KeyValuePair<string, object>("@ParentID",
                                                                   rcID + int.Parse(lstParent[count].ToString())));
                    parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                    parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));

                    SQLHandler sqlH = new SQLHandler();
                    ruleConditionID = sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,"[dbo].[usp_Aspx_RuleConditionAdd]",parameter, "@RuleConditionID");

                    if (objRuleCondition.RuleConditionType == "CC" && objRuleCondition.LstCartPriceRuleConditions != null && objRuleCondition.LstCartPriceRuleConditions.Count>0)
                    {
                        CartPriceRuleConditionAdd(objRuleCondition.LstCartPriceRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                    }
                    else if (objRuleCondition.RuleConditionType == "PAC" && objRuleCondition.LstProductAttributeRuleConditions != null &&  objRuleCondition.LstProductAttributeRuleConditions.Count>0)
                    {
                        ProductAttributeRuleConditionAdd(objRuleCondition.LstProductAttributeRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                    }
                    else if (objRuleCondition.RuleConditionType == "PS" && objRuleCondition.LstProductSublectionRuleConditions != null && objRuleCondition.LstProductSublectionRuleConditions.Count>0)
                    {
                        SubselectionRuleConditionAdd(objRuleCondition.LstProductSublectionRuleConditions, cartPriceRuleID, ruleConditionID, tran, aspxCommonObj);
                    }

                    count++;
                }
                return ruleConditionID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCartPriceRuleConditions()
        {
            SQLHandler sqlHandler = new SQLHandler();
            return sqlHandler.ExecuteAsScalar<int>("[dbo].[usp_Aspx_GetRuleConditions]");
        }
        
        public void CartPriceRuleConditionAdd(List<CartPriceRuleCondition> lstCartPriceRuleCondition, int cartPriceRuleID,int ruleConditionID, SqlTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            parameter.Add(new KeyValuePair<string, object>("@IsAll", lstCartPriceRuleCondition[0].IsAll));
            parameter.Add(new KeyValuePair<string, object>("@IsTrue", lstCartPriceRuleCondition[0].IsTrue));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           
            SQLHandler sqlH = new SQLHandler();
            try
            {
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_Aspx_CartPriceRuleConditionAdd]", parameter);
                if (lstCartPriceRuleCondition[0].LstCartConditionDetails != null && lstCartPriceRuleCondition[0].LstCartConditionDetails.Count > 0)
                {
                    CartConditionDetailAdd(lstCartPriceRuleCondition[0].LstCartConditionDetails,
                                           ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ProductAttributeRuleConditionAdd(List<ProductAttributeRuleCondition> lstPACRuleCondition, int cartPriceRuleID, int ruleConditionID, SqlTransaction tran,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            parameter.Add(new KeyValuePair<string, object>("@IsAll", lstPACRuleCondition[0].IsAll));
            parameter.Add(new KeyValuePair<string, object>("@IsFound", lstPACRuleCondition[0].IsFound));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));

            SQLHandler sqlH = new SQLHandler();
            try
            {
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_Aspx_ProductAttributeRuleConditionAdd]", parameter);
                if (lstPACRuleCondition[0].LstCartConditionDetails != null && lstPACRuleCondition[0].LstCartConditionDetails.Count > 0)
                {
                    CartConditionDetailAdd(lstPACRuleCondition[0].LstCartConditionDetails,
                                           ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public void SubselectionRuleConditionAdd(List<ProductSubSelectionRuleCondition> lstPSRuleCondition, int cartPriceRuleID, int ruleConditionID, SqlTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
            parameter.Add(new KeyValuePair<string, object>("@IsAll", lstPSRuleCondition[0].IsAll));
            parameter.Add(new KeyValuePair<string, object>("@IsQuantity", lstPSRuleCondition[0].IsQuantity));
            parameter.Add(new KeyValuePair<string, object>("@Value", lstPSRuleCondition[0].Value));
            parameter.Add(new KeyValuePair<string, object>("@RuleOperatorID", lstPSRuleCondition[0].RuleOperatorID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
           
            SQLHandler sqlH = new SQLHandler();
            try
            {
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_Aspx_ProductSubSelectionRuleConditionAdd]", parameter);
                if (lstPSRuleCondition[0].LstCartConditionDetails != null && lstPSRuleCondition[0].LstCartConditionDetails.Count > 0)
                {
                    CartConditionDetailAdd(lstPSRuleCondition[0].LstCartConditionDetails,
                                           ruleConditionID, cartPriceRuleID, tran, aspxCommonObj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CartConditionDetailAdd(List<CartConditionDetail> lstCartConditionDetail, int? ruleConditionID, int cartPriceRuleID, SqlTransaction tran, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                foreach (CartConditionDetail objCartConditionDetail in lstCartConditionDetail)
                {
                    if (objCartConditionDetail != null)
                    {
                        List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                        parameter.Add(new KeyValuePair<string, object>("@RuleConditionID", ruleConditionID));
                        parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleID));
                        parameter.Add(new KeyValuePair<string, object>("@AttributeID",
                                                                       objCartConditionDetail.AttributeID));
                        parameter.Add(new KeyValuePair<string, object>("@RuleOperatorID",
                                                                       objCartConditionDetail.RuleOperatorID));
                        parameter.Add(new KeyValuePair<string, object>("@Value", objCartConditionDetail.Value));
                        parameter.Add(new KeyValuePair<string, object>("@Priority", objCartConditionDetail.Priority));
                        parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                        parameter.Add(new KeyValuePair<string, object>("@IsActive", true));
                        parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                        parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));

                        SQLHandler sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure,
                                             "[dbo].[usp_Aspx_CartConditionDetailAdd]", parameter);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CartPriceRuleRoleAdd(CartPriceRuleRole cartPriceRuleRole,SqlTransaction tran,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleRole.CartPriceRuleID));
            parameter.Add(new KeyValuePair<string, object>("@RoleID", cartPriceRuleRole.RoleID));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", true));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            SQLHandler sqlH =new SQLHandler();
            try
            {
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "[dbo].[usp_Aspx_CartPriceRuleRoleAdd]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CartPriceRuleStoreAdd(CartPriceRuleStore cartPriceRuleStore,SqlTransaction tran,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CartPriceRuleID", cartPriceRuleStore.CartPriceRuleID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", cartPriceRuleStore.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@IsActive", true));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            
            try
            {
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery(tran, CommandType.StoredProcedure, "usp_Aspx_CartPriceRuleStoreAdd", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CartPriceRuleDelete(int cartPriceRuleID,AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CartPriceRuleID", cartPriceRuleID));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CartPriceRuleDelete";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
          
        public int CartPriceRulesMultipleDelete(string cartRulesIds,AspxCommonInfo aspxCommonObj)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@CartPriceRulesIDs", cartRulesIds));
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID",aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@UserName", aspxCommonObj.UserName));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.CommandText = "usp_Aspx_CartPriceRulesDeleteMultiple";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                object val = sqlCommand.ExecuteScalar();
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        #endregion
    }
}
