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
using SageFrame.Web;
using SageFrame.Message;
using System.Collections;
using SageFrame.SageFrameClass.MessageManagement;

namespace AspxCommerce.Core
{
    public class CouponManageSQLProvider
    {
        public List<CouponInfo> BindAllCouponDetails(int offset, int limit, GetCouponDetailsInfo couponDetailObj, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponDetailObj.CouponTypeID));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponDetailObj.CouponCode));
            parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", couponDetailObj.ValidateFrom));
            parameter.Add(new KeyValuePair<string, object>("@ValidateTo", couponDetailObj.ValidateTo));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponInfo>("usp_Aspx_GetCouponDetails", parameter);
        }

        public List<CouponPortalUserListInfo> GetPortalUsersList(int offset, int limit, int couponID, string customerName, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CustomerName", customerName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponPortalUserListInfo>("usp_Aspx_GetAllPortalUserLists", parameter);
        }

        public void AddUpdateCoupons(CouponSaveObj couponSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponID", couponSaveObj.CouponID));
                parameter.Add(new KeyValuePair<string, object>("@CouponTypeID", couponSaveObj.CouponTypeID));
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponSaveObj.CouponCode));
                parameter.Add(new KeyValuePair<string, object>("@CouponAmount", couponSaveObj.CouponAmount));
                parameter.Add(new KeyValuePair<string, object>("@IsPercentage", couponSaveObj.IsPercentage));
                parameter.Add(new KeyValuePair<string, object>("@ValidateFrom", couponSaveObj.ValidateFrom));
                parameter.Add(new KeyValuePair<string, object>("@ValidateTo", couponSaveObj.ValidateTo));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", couponSaveObj.IsActive));
                parameter.Add(new KeyValuePair<string, object>("@SettingIDs", couponSaveObj.SettingIDs));
                parameter.Add(new KeyValuePair<string, object>("@SettingValues", couponSaveObj.SettingValues));
                parameter.Add(new KeyValuePair<string, object>("@PortalUser_UserName", couponSaveObj.PortalUser_UserName));

                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_AddUpdateCoupons", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CouponStatusInfo> BindCouponStatus()
        {
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponStatusInfo>("usp_Aspx_GetCouponStatus");
        }

        public List<CouponSettingKeyValueInfo> GetCouponSettingKeyValueInfo(int couponID, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponSettingKeyValueInfo>("usp_Aspx_GetCouponSettingKeyValueByCouponID", parameter);
        }

        public void DeleteCoupons(string couponIDs, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponID", couponIDs));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCoupons", parameter);
        }

        public CouponVerificationInfo VerifyUserCoupon(decimal totalCost, string couponCode,AspxCommonInfo aspxCommonObj, int appliedCount)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@totalCost", totalCost));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameter.Add(new KeyValuePair<string, object>("@AppliedCount", appliedCount));            
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsObject<CouponVerificationInfo>("[usp_Aspx_VerifyCouponCode]",parameter);
        }

        public void UpdateCouponUserRecord(string couponCode, int storeID, int portalID, string userName,int orderID)
        {
            if (System.Web.HttpContext.Current.Session["CouponApplied"] != null)
            {
                int ac =int.Parse( System.Web.HttpContext.Current.Session["CouponApplied"].ToString());
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponCode));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CouponUsedCount", ac));
                parameter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", parameter);
            }
        }

        public List<CouponUserInfo> GetCouponUserDetails(int offset, int? limit, GetCouponUserDetailInfo couponUserObj, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", couponUserObj.CouponCode));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponUserObj.CouponStatusID));
            parameter.Add(new KeyValuePair<string, object>("@ValidFrom", couponUserObj.ValidFrom));
            parameter.Add(new KeyValuePair<string, object>("@ValidTo", couponUserObj.ValidTo));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponUserInfo>("usp_Aspx_CouponUserDetails", parameter);
        }

        public List<CouponUserListInfo> GetCouponUserList(int offset, int limit, CouponCommonInfo bindCouponUserObj, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@CouponID", bindCouponUserObj.CouponID));
            parameter.Add(new KeyValuePair<string, object>("@CouponCode", bindCouponUserObj.CouponCode));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", bindCouponUserObj.CouponStatusID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<CouponUserListInfo>("[usp_Aspx_GetCouponUserList]", parameter);
        }


        public void DeleteCouponUser(string couponUserID,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteCouponUser", parameter);
        }
        public void UpdateCouponUser(int couponUserID,int couponStatusID,AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@CouponUserID", couponUserID));
            parameter.Add(new KeyValuePair<string, object>("@CouponStatusID", couponStatusID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUser", parameter);
        }

        //public void SendCouponCodeEmail(string senderEmail, string receiverEmailDs, string subject,ArrayList messageBody)
        public void SendCouponCodeEmail(CouponEmailInfo couponEmailObj)
        {
            string[] receiverIDs;
            char[] spliter = { '#' };
        //    receiverIDs = receiverEmailDs.Split(spliter);
            receiverIDs = couponEmailObj.ReceiverEmail.Split(spliter);

            for (int i = 0; i < receiverIDs.Length; i++)
            {
                string receiverEmailID = receiverIDs[i];
                string emailSuperAdmin;
                string emailSiteAdmin;
                SageFrameConfig pagebase = new SageFrameConfig();
                emailSuperAdmin =pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
              //string individualMsgBody = messageBody[i].ToString();
                string individualMsgBody = couponEmailObj.MessageBodyTemplate[i].ToString();
           //   MailHelper.SendMailNoAttachment(senderEmail, receiverEmailID, subject, individualMsgBody, emailSiteAdmin, emailSuperAdmin);
                MailHelper.SendMailNoAttachment(couponEmailObj.SenderEmail, receiverEmailID, couponEmailObj.Subject, individualMsgBody, emailSiteAdmin, emailSuperAdmin);
           }
        }
    }
}
