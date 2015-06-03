using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxRewardPointsController
    {
        public AspxRewardPointsController()
        {

        }
        public static void RewardPointsSaveByCore(int rewardRuleID, string uName, string email, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRewardPointsProvider.RewardPointsSaveByCore(rewardRuleID, uName, email, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public static void RewardPointsDeleteByCore(int rewardRuleID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxRewardPointsProvider.RewardPointsDeleteByCore(rewardRuleID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
