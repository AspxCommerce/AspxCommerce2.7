using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxStoreBranchMgntController
    {
       public AspxStoreBranchMgntController()
       {
       }

       public static bool CheckBranchNameUniqueness(AspxCommonInfo aspxCommonObj, int storeBranchId, string storeBranchName)
       {
           try
           {
               bool isUnique = AspxStoreBranchMgntProvider.CheckBranchNameUniqueness(aspxCommonObj, storeBranchId, storeBranchName);
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
               AspxStoreBranchMgntProvider.SaveAndUpdateStorebranch(branchName, branchImage, aspxCommonObj, storeBranchId);
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
               List<BranchDetailsInfo> lstBrDetail = AspxStoreBranchMgntProvider.GetStoreBranchList(offset, limit, aspxCommonObj);
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
               AspxStoreBranchMgntProvider.DeleteStoreBranches(storeBranchIds, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
