using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxStoreBranchMgntProvider
    {
       public AspxStoreBranchMgntProvider()
       {
       }
  
       public static bool CheckBranchNameUniqueness(AspxCommonInfo aspxCommonObj, int storeBranchId, string storeBranchName)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
               Parameter.Add(new KeyValuePair<string, object>("@StoreBranchName", storeBranchName));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckBranchNameUniquness]", Parameter, "@IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
  
       public static void SaveAndUpdateStorebranch(string branchName, string branchImage, AspxCommonInfo aspxCommonObj, int storeBranchId)
       {
           try
           {
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@BranchName", branchName));
               Parameter.Add(new KeyValuePair<string, object>("@BranchImage", branchImage));
               Parameter.Add(new KeyValuePair<string, object>("@StoreBranchID", storeBranchId));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SaveAndUpdateStoreBranch]", Parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    
       public static List<BranchDetailsInfo> GetStoreBranchList(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               SQLHandler sqlH = new SQLHandler();
               List<BranchDetailsInfo> lstBrDetail= sqlH.ExecuteAsList<BranchDetailsInfo>("[dbo].[usp_Aspx_GetStoreBranches]", parameter);
               return lstBrDetail;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteStoreBranches(string storeBranchIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               Parameter.Add(new KeyValuePair<string, object>("@StoreBranchIDs", storeBranchIds));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteStoreBranches]", Parameter);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
