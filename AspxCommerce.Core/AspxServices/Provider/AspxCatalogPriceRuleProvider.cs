using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Threading;

namespace AspxCommerce.Core
{
    public class AspxCatalogPriceRuleProvider
    {

        public AspxCatalogPriceRuleProvider()
        {
        }

        public static string ConnectionString
        {
            get { return SystemSetting.SageFrameConnectionString; }
        }

        public static List<PricingRuleAttributeInfo> GetPricingRuleAttributes(AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            List<PricingRuleAttributeInfo> lstPriceRuleAttr = sqlHandler.ExecuteAsList<PricingRuleAttributeInfo>("usp_Aspx_GetPricingRuleAttr", paramList);
            return lstPriceRuleAttr;
        }

        public static List<CatalogPriceRulePaging> GetCatalogPricingRules(string ruleName, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> isActive, AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("@offset", offset));
            paramList.Add(new KeyValuePair<string, object>("@limit", limit));
            paramList.Add(new KeyValuePair<string, object>("@RuleName", ruleName));
            paramList.Add(new KeyValuePair<string, object>("@StartDate", startDate));
            paramList.Add(new KeyValuePair<string, object>("@EndDate", endDate));
            paramList.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            List<CatalogPriceRulePaging> lstCatalogPriceRule = sqlHandler.ExecuteAsList<CatalogPriceRulePaging>("usp_Aspx_GetPricingRules", paramList);
            return lstCatalogPriceRule;
        }

        public static DataSet GetCatalogPricingRule(Int32 catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            paramList.Add(new KeyValuePair<string, object>("@CatalogPriceRuleID", catalogPriceRuleID));
            DataSet ds = sqlHandler.ExecuteAsDataSet("usp_Aspx_GetPricingRuleInfoByID", paramList);
            return ds;
        }

        public static int CatalogPriceRuleAdd(CatalogPriceRule catalogPriceRule, AspxCommonInfo aspxCommonObj)
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

        public void ApplyCatalogPricingRule(AspxCommonInfo aspxCommonObj)
        {
            int CurrentPortalID = 0;
            int CurrentOffset = 0;
            int CurrentLimit = 0;
            string CurrentCulture = string.Empty;
            CurrentPortalID = aspxCommonObj.PortalID;
            CurrentCulture = aspxCommonObj.CultureName;
            bool isExistCatalogRule = CheckCatalogRuleExist(aspxCommonObj);
            if (isExistCatalogRule)
            {
                int rowTotal = GetItemID(aspxCommonObj.PortalID);
                decimal intTempLoops = Math.Ceiling(Convert.ToDecimal((double)rowTotal / 1000));
                //List<Thread> listThreads = new List<Thread>();
                for (int i = 1; i <= intTempLoops; i++)
                {

                    //bool startNewThread = true;
                    //foreach (Thread thread in listThreads)
                    //{
                    //    if (thread.IsAlive)
                    //    {
                    //        startNewThread = false;
                    //    }
                    //}

                    //if (startNewThread)
                    //{
                        CurrentOffset = CurrentLimit + 1;
                        CurrentLimit = 1000 * i;
                        if (CurrentLimit >= rowTotal)
                        {
                            CurrentLimit = rowTotal;
                        }
                        AnalyseCatalogPrice(aspxCommonObj, CurrentOffset, CurrentLimit);
                        //listThreads.Add(newThread);
                        //newThread.Start();
                    //}
                    //else
                    //{
                    //    i--;
                    //}

                }

                //while (listThreads.Last().IsAlive)
                //{

                //}
                TruncateCatalogAffectedPrice(aspxCommonObj.PortalID);               
            }
            else
            {
                DeleteCatalogAffectedPrice(aspxCommonObj);
            }

        }

        public int GetItemID(int iPortalID)
        {
            int result = 0;

            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                SQLHandler sqLH = new SQLHandler();
                result = sqLH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_CatalogGetTotalItem]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public void TruncateCatalogAffectedPrice(int iPortalID)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                SQLHandler sqLH = new SQLHandler();
                sqLH.ExecuteNonQuery("[dbo].[usp_Aspx_CatalogTruncateAffectedPrice]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckCatalogRuleExist(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isExist = false;
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSP(aspxCommonObj);
                isExist = sqlHandler.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCatalogRuleExist]", paramList, "@IsExist");
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteCatalogAffectedPrice(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> paramList = CommonParmBuilder.GetParamSP(aspxCommonObj);
                sqlHandler.ExecuteNonQuery("[dbo].[usp_Aspx_CatalogAffectedPriceDelete]", paramList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AnalyseCatalogPrice(AspxCommonInfo aspxCommonObj, int offset, int limit)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", aspxCommonObj.StoreID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", aspxCommonObj.PortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", aspxCommonObj.CultureName));
            sqlCommand.Parameters.Add(new SqlParameter("@offset", offset));
            sqlCommand.Parameters.Add(new SqlParameter("@limit", limit));
            sqlCommand.CommandTimeout = 0;
            sqlCommand.CommandText = "usp_Aspx_ApplyCatalogPricingRuleForSchedular";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

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

        public static int CatalogPriceRuleConditionAdd(CatalogPriceRuleCondition catalogPriceRuleCondition, AspxCommonInfo aspxCommonObj)
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
                    foreach (CatalogConditionDetail catalogConditionDetail in catalogPriceRuleCondition.CatalogConditionDetail)
                    {
                        if (catalogConditionDetail != null)
                        {
                            catalogConditionDetail.CatalogPriceRuleConditionID = Convert.ToInt16(val);
                            catalogConditionDetail.CatalogPriceRuleID = catalogPriceRuleCondition.CatalogPriceRuleID;
                            catalogConditionDetailID =
                                AspxCatalogPriceRuleProvider.CatalogConditionDetailAdd(catalogConditionDetail, aspxCommonObj);
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

        public static int CatalogPriceRuleRoleAdd(CatalogPriceRuleRole catalogPriceRuleRole, AspxCommonInfo aspxCommonObj)
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

        public static int CatalogConditionDetailAdd(CatalogConditionDetail catalogConditionDetail, AspxCommonInfo aspxCommonObj)
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

        public static int CatalogPriceRuleDelete(int catalogPriceRuleID, AspxCommonInfo aspxCommonObj)
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

        public static int CatalogPriceRulesMultipleDelete(string catRulesIds, AspxCommonInfo aspxCommonObj)
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

        public static bool CheckCatalogPriorityUniqueness(int catalogPriceRuleID, int priority, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CatalogPriceRuleID", catalogPriceRuleID));
                parameterCollection.Add(new KeyValuePair<string, object>("@Priority", priority));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[usp_Aspx_CatalogPriorityUniquenessCheck]", parameterCollection, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
