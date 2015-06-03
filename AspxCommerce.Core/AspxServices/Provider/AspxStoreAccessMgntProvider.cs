using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxStoreAccessMgntProvider
    {
        public AspxStoreAccessMgntProvider()
        {
        }

        public static List<StoreAccessAutocomplete> SearchStoreAccess(string text, int keyID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", keyID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", text));
                List<StoreAccessAutocomplete> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessAutocomplete>("[dbo].[usp_Aspx_GetSearchAutoComplete]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SaveUpdateStoreAccess(int edit, int storeAccessKeyID, string storeAccessData, string reason, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", storeAccessKeyID));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", storeAccessData));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameter.Add(new KeyValuePair<string, object>("@Reason", reason));
                parameter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", edit));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_StoreAccessAddUpdate]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeletStoreAccess(int storeAccessID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessID", storeAccessID));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_StoreAccessDelete]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AspxUserList> GetAspxUser(string userName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                SQLHandler sqlH = new SQLHandler();
                List<AspxUserList> lstUser = sqlH.ExecuteAsList<AspxUserList>("[dbo].[usp_Aspx_GetListOfCurrentCustomer]", parameter);
                return lstUser;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AspxUserList> GetAspxUserEmail(string email, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@Email", email));
                SQLHandler sqlH = new SQLHandler();
                List<AspxUserList> lstUserEmail = sqlH.ExecuteAsList<AspxUserList>("[dbo].[usp_Aspx_GetListOfCurrentCustomerEmail]", parameter);
                return lstUserEmail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessKey> GetStoreKeyID()
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<StoreAccessKey> lstStAccessKey = sqlH.ExecuteAsList<StoreAccessKey>("[dbo].[usp_Aspx_GetStoreAccessKeyID]");
                return lstStAccessKey;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessInfo> LoadStoreAccessCustomer(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                List<StoreAccessInfo> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessCustomer]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessInfo> LoadStoreAccessEmails(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                List<StoreAccessInfo> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessEmail]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessInfo> LoadStoreAccessIPs(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                List<StoreAccessInfo> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessIP]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessInfo> LoadStoreAccessDomains(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));

                List<StoreAccessInfo> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessDomain]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<StoreAccessInfo> LoadStoreAccessCreditCards(int offset, int limit, string search, System.Nullable<DateTime> startDate, System.Nullable<DateTime> endDate, System.Nullable<bool> status, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@Search", search));
                parameter.Add(new KeyValuePair<string, object>("@StartDate", startDate));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", endDate));
                parameter.Add(new KeyValuePair<string, object>("@Status", status));
                List<StoreAccessInfo> lstStoreAccess = sqlH.ExecuteAsList<StoreAccessInfo>("[dbo].[usp_Aspx_GetStoreAccessCreditCard]", parameter);
                return lstStoreAccess;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckExisting(AspxCommonInfo aspxCommonObj, int storeAccesskeyId, string accessData)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessKeyID", storeAccesskeyId));
                parameter.Add(new KeyValuePair<string, object>("@StoreAccessData", accessData));
                bool isUnique= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckExistingStoreAccess]", parameter, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
