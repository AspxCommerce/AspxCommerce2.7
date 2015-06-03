using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
 public  class AspxRewardPointsProvider
    {
     public AspxRewardPointsProvider()
       {
         
       }
     public static void RewardPointsSaveByCore(int rewardRuleID, string uName, string email, AspxCommonInfo aspxCommonObj)
     {
         try
         {
             SQLHandler sqlH = new SQLHandler();
             List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

             parameter.Add(new KeyValuePair<string, object>("@RewardRuleID", rewardRuleID));
             parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
             parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
             parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
             parameter.Add(new KeyValuePair<string, object>("@UserName", uName));
             parameter.Add(new KeyValuePair<string, object>("@Email", email));
             sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsSaveByCore", parameter);
         }
         catch (Exception ex)
         {
             throw ex;
         }

     }

     public static void RewardPointsDeleteByCore(int rewardRuleID, AspxCommonInfo aspxCommonObj)
     {
         try
         {
             List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
             parameterCollection.Add(new KeyValuePair<string, object>("@RewardRuleID", rewardRuleID));
             parameterCollection.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
             parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
             parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
             parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
             parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));

             SQLHandler sqlH = new SQLHandler();
             sqlH.ExecuteNonQuery("usp_Aspx_RewardPointsDeleteByCore", parameterCollection);
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }



    }
}
