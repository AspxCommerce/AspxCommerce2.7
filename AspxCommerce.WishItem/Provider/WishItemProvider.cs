using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AspxCommerce.Core;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;
using SageFrame.Web;
using SageFrame.Web.Utilities;

namespace AspxCommerce.WishItem
{
    public class WishItemProvider
    {
        public WishItemProvider()
        {
        }

        public bool CheckWishItems(int ID, string costVariantValueIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ID));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantValueIDs", costVariantValueIDs));
                SQLHandler sqlH = new SQLHandler();
                bool isExist = sqlH.ExecuteNonQueryAsGivenType<bool>("[usp_Aspx_CheckWishItems]", parameter, "@IsExist");
                return isExist;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveWishItems(SaveWishListInfo saveWishListInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemID", saveWishListInfo.ItemID));
                parameter.Add(new KeyValuePair<string, object>("@WishItemID", 0));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantValueIDs", saveWishListInfo.CostVariantValueIDs));
                parameter.Add(new KeyValuePair<string, object>("@IP", saveWishListInfo.IP));
                parameter.Add(new KeyValuePair<string, object>("@CountryName", saveWishListInfo.CountryName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_SaveWishItems", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WishItemsInfo> GetWishItemList(int offset, int limit, AspxCommonInfo aspxCommonObj, string flagShowAll, int count, int sortBy)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@flag", flagShowAll));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                parameter.Add(new KeyValuePair<string, object>("@SortBy", sortBy));
                SQLHandler sqlH = new SQLHandler();
                List<WishItemsInfo> lstWishItem = sqlH.ExecuteAsList<WishItemsInfo>("usp_Aspx_GetWishItemList", parameter);
                return lstWishItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WishItemsInfo> GetRecentWishItemList(AspxCommonInfo aspxCommonObj, string flagShowAll, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@flag", flagShowAll));
                parameter.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                List<WishItemsInfo> lstWishItem = sqlH.ExecuteAsList<WishItemsInfo>("usp_Aspx_GetRecentWishItemList", parameter);
                return lstWishItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteWishItem(string wishItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@WishItemID", wishItemID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_DeleteWishItem", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateWishList(string wishItemID, string comment, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@WishItemID", wishItemID));
                parameter.Add(new KeyValuePair<string, object>("@Comment", comment));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateWishItem", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearWishList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_ClearWishItem", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int CountWishItems(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                int countWish = sqlH.ExecuteAsScalar<int>("usp_Aspx_GetWishItemsCount", parameter);
                return countWish;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveShareWishListEmailMessage(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ItemIDs", wishlistObj.ItemID));
                parameter.Add(new KeyValuePair<string, object>("@SenderName", wishlistObj.SenderName));
                parameter.Add(new KeyValuePair<string, object>("@SenderEmail", wishlistObj.SenderEmail));
                parameter.Add(new KeyValuePair<string, object>("@ReceiverEmailID", wishlistObj.ReceiverEmail));
                parameter.Add(new KeyValuePair<string, object>("@Subject", wishlistObj.Subject));
                parameter.Add(new KeyValuePair<string, object>("@Message", wishlistObj.Message));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[usp_Aspx_SaveShareWishListEmail]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SendShareWishItemEmail(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            try
            {
                SendEmailForSharedWishList(aspxCommonObj, wishlistObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string> { };
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            while (preIndex > -1)
            {
                returnValue.Add(template.Substring(preIndex, (postIndex - preIndex) + 1));
                template = template.Substring(postIndex + 1, (template.Length - postIndex) - 1);
                preIndex = template.IndexOf('%', 0);
                postIndex = template.IndexOf('%', preIndex + 1);
            }
            return returnValue.ToArray();
        }

        private void SendEmailForSharedWishList(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            string serverHostLoc = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
            string tdContent = string.Empty;
            var dataObj = JSONHelper.Deserialise<List<WishItemEmailInfo>>(wishlistObj.MessageBody);
            int length = dataObj.Count();
            string[] tdContentArray = new string[length];
            string shareWishMailHtml = "";
            shareWishMailHtml += "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td>";
            shareWishMailHtml += "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr>";
            for (int i = 0; i <= length - 1; i++)
            {
                tdContent +=
                    "<td width='33%'><div style='border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;'> <img src='" +
                    serverHostLoc + dataObj[i].src + "' alt='" + dataObj[i].alt + "' width='80' height='50' />";
                tdContent +=
                    "<p style='margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;'>";
                tdContent +=
                    "<span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'>" +
                    dataObj[i].title + "</span><br />"; //item name
                tdContent +=
                    "<span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'> <a href='" +
                    serverHostLoc + dataObj[i].href + "'>" + dataObj[i].hrefHtml + "</a></span><br />"; //item name
                tdContent +=
                    "<span style='font-weight:bold; font-size:11px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'>Price:</span>" +
                    dataObj[i].price + "<br />"; //price
                tdContent +=
                    "<span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'>Comments:</span> " +
                    dataObj[i].htmlComment + "</p></div></td>"; //comment
                tdContentArray[i] = tdContent;
                tdContent = "";
            }
            for (int j = 0; j <= length - 1; j++)
            {
                if (j % 3 == 0)
                {
                    shareWishMailHtml += "</tr><tr>" + tdContentArray[j];
                }
                else
                {
                    shareWishMailHtml += tdContentArray[j];
                }
            }
            shareWishMailHtml += "</tr></table></td></tr></table>";
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            List<MessageManagementInfo> template =
                MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                    SystemSetting.SHARED_WISHED_LIST, aspxCommonObj.PortalID);
            foreach (MessageManagementInfo messageToken in template)
            {
                string messageTemplate = messageToken.Body.ToString();
                string src = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                if (template != null)
                {
                    string[] tokens = GetAllToken(messageTemplate);
                    foreach (string token in tokens)
                    {
                        switch (token)
                        {
                            case "%DateTime%":
                                messageTemplate = messageTemplate.Replace(token,
                                                                          System.DateTime.Now.ToString("MM/dd/yyyy"));
                                break;
                            case "%Username%":
                                messageTemplate = messageTemplate.Replace(token, wishlistObj.SenderName);
                                break;
                            case "%UserEmail%":
                                messageTemplate = messageTemplate.Replace(token, wishlistObj.SenderEmail);
                                break;
                            case "%MessageDetails%":
                                messageTemplate = messageTemplate.Replace(token, wishlistObj.Message);
                                break;
                            case "%ItemDetailsTable%":
                                messageTemplate = messageTemplate.Replace(token, shareWishMailHtml);
                                break;
                            case "%LogoSource%":
                                string imgSrc = "http://" + src + logosrc;
                                messageTemplate = messageTemplate.Replace(token, imgSrc);
                                break;
                            case "%ServerPath%":
                                messageTemplate = messageTemplate.Replace(token, "http://" + src);
                                break;
                            case "%DateYear%":
                                messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                                break;
                        }
                    }
                }

                char[] spliter = { ',' };
                string[] receiverIDs = wishlistObj.ReceiverEmail.Split(spliter);

                for (int i = 0; i < receiverIDs.Length; i++)
                {
                    string receiverEmailID = receiverIDs[i];
                    string emailSuperAdmin;
                    string emailSiteAdmin;
                    SageFrameConfig pagebase = new SageFrameConfig();
                    emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                    emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                    MailHelper.SendMailNoAttachment(wishlistObj.SenderEmail, receiverEmailID, wishlistObj.Subject,
                                                    messageTemplate,
                                                    emailSiteAdmin, emailSuperAdmin);
                }
            }
        }

        public WishItemsSettingInfo GetWishItemsSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                WishItemsSettingInfo objWishSetting = new WishItemsSettingInfo();
                objWishSetting = sqlH.ExecuteAsObject<WishItemsSettingInfo>("[dbo].[usp_Aspx_WishItemsSettingsGet]", parameterCollection);
                return objWishSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetWishItemList(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsDataSet("[dbo].[usp_Aspx_GetWishListItem]", parameterCollection);
        }

        public void SaveAndUpdateWishItemsSetting(AspxCommonInfo aspxCommonObj, WishItemsSettingKeyPairInfo wishlist)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingKeys", wishlist.SettingKey));
                parameterCollection.Add(new KeyValuePair<string, object>("@SettingValues", wishlist.SettingValue));
                SQLHandler sqlhandle = new SQLHandler();
                sqlhandle.ExecuteNonQuery("[dbo].[usp_Aspx_WishItemsSettingsUpdate]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
