using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxStoreAccessMgntController
    {
       public AspxStoreAccessMgntController()
       {
       }

       public static List<StoreAccessAutocomplete> SearchStoreAccess(string text, int keyID)
       {
           try
           {
               List<StoreAccessAutocomplete> lstStoreAccess = AspxStoreAccessMgntProvider.SearchStoreAccess(text, keyID);
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
               AspxStoreAccessMgntProvider.SaveUpdateStoreAccess(edit, storeAccessKeyID, storeAccessData, reason, isActive, aspxCommonObj);
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
               AspxStoreAccessMgntProvider.DeletStoreAccess(storeAccessID, aspxCommonObj);
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

               List<AspxUserList> lstUser = AspxStoreAccessMgntProvider.GetAspxUser(userName, aspxCommonObj);
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
               List<AspxUserList> lstUserEmail = AspxStoreAccessMgntProvider.GetAspxUserEmail(email, aspxCommonObj);
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
               List<StoreAccessKey> lstStAccessKey = AspxStoreAccessMgntProvider.GetStoreKeyID();
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
               List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntProvider.LoadStoreAccessCustomer(offset, limit, search, startDate, endDate, status, aspxCommonObj);
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
               List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntProvider.LoadStoreAccessEmails(offset, limit, search, startDate, endDate, status, aspxCommonObj);
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
               List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntProvider.LoadStoreAccessIPs(offset, limit, search, startDate, endDate, status, aspxCommonObj);
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
               List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntProvider.LoadStoreAccessDomains(offset, limit, search, startDate, endDate, status, aspxCommonObj);
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
               List<StoreAccessInfo> lstStoreAccess = AspxStoreAccessMgntProvider.LoadStoreAccessCreditCards(offset, limit, search, startDate, endDate, status, aspxCommonObj);
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
               bool isUnique = AspxStoreAccessMgntProvider.CheckExisting(aspxCommonObj, storeAccesskeyId, accessData);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
