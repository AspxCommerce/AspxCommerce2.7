using System;
using System.Configuration;
using System.Web;
using AspxCommerce.Core;
using System.Collections.Generic;

namespace AspxCommerce.RewardPoints
{
    public class RewardPointsController
    {
        public static void RewardPointsSaveGeneralSettings(GeneralSettingsCommonInfo generalSettingobj,
                                                           AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsSaveGeneralSettings(generalSettingobj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static List<GeneralSettingInfo> GetGeneralSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<GeneralSettingInfo> lstGeneralSet = RewardPointsProvider.GetGeneralSetting(aspxCommonObj);
                return lstGeneralSet;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void RewardPointsSaveUpdateNewRule(RewardPointsCommonInfo rewardPointsCommonObj,
                                                         AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsSaveUpdateNewRule(rewardPointsCommonObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public static List<RewardPointsSettingInfo> RewardPointsSettingGetAll(int offset, int limit,
                                                                              AspxCommonInfo aspxCommonObj,
                                                                              RewardPointsCommonInfo
                                                                                  rewardPointsCommonObj)
        {
            try
            {
                List<RewardPointsSettingInfo> lstRewardPointSet = RewardPointsProvider.RewardPointsSettingGetAll(
                    offset, limit, aspxCommonObj, rewardPointsCommonObj);
                return lstRewardPointSet;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public static void RewardPointsRuleDelete(RewardPointsCommonInfo rewardPointCommonObj,
                                                  AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsRuleDelete(rewardPointCommonObj, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<RewardRuleListInfo> RewardPointsRuleListBind(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<RewardRuleListInfo> rulelist = RewardPointsProvider.RewardPointsRuleListBind(aspxCommonObj);

                return rulelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<MyRewardPointsInfo> GetMyRewardPointsHistory(int offset, int limit,
                                                                        AspxCommonInfo aspxCommonObj,
                                                                        RewardPointsHistoryCommonInfo
                                                                            RewardPointsHistoryCommonObj)
        {
            try
            {
                List<MyRewardPointsInfo> history = RewardPointsProvider.GetMyRewardPointsHistory(offset, limit,
                                                                                                 aspxCommonObj,
                                                                                                 RewardPointsHistoryCommonObj);
                ;
                return history;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<RewardPointsHistryInfo> RewardPointsHistoryGetAll(int offset, int limit,
                                                                             AspxCommonInfo aspxCommonObj,
                                                                             RewardPointsHistoryCommonInfo
                                                                                 RewardPointsHistoryCommonObj)
        {
            try
            {
                List<RewardPointsHistryInfo> histryInfos = RewardPointsProvider.RewardPointsHistoryGetAll(offset, limit,
                                                                                                          aspxCommonObj,
                                                                                                          RewardPointsHistoryCommonObj);
                return histryInfos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void RewardPointsSaveNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo,
                                                      AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsSaveNewsLetter(rewardPointsInfo, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void RewardPointsSavePolling(RewardPointsPollCommonInfo rewardPointsInfo,
                                                      AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsSavePolling(rewardPointsInfo, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void RewardPointsDeleteNewsLetter(RewardPointsNLCommonInfo rewardPointsInfo,
                                                        AspxCommonInfo aspxCommonObj)
        {
            try
            {
                RewardPointsProvider.RewardPointsDeleteNewsLetter(rewardPointsInfo, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void SetSessionVariable(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        //public static bool RewardPointsIschecked(string key)
        //{
        //    if (System.Web.HttpContext.Current.Session[key] != null)
        //    {
        //        string i = System.Web.HttpContext.Current.Session[key].ToString();
        //        return Convert.ToBoolean(i.ToString());
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


        public static decimal RewardPointsSelectedValue(string key)
        {
            if (System.Web.HttpContext.Current.Session[key] != null)
            {
                string i = System.Web.HttpContext.Current.Session[key].ToString();
                return Convert.ToDecimal(i.ToString());
            }
            else
            {
                return 0;
            }

        }

        public static bool IsPurchaseActive(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isPurchaseActive = RewardPointsProvider.IsPurchaseActive(aspxCommonObj);
                return isPurchaseActive;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool RewardPointsGeneralSettingsIsActive(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool IsActive = RewardPointsProvider.RewardPointsGeneralSettingsIsActive(aspxCommonObj);
                return IsActive;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
