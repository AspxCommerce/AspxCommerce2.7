using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SageFrame.Core;
using SageFrame.Message;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using AspxCommerce.Core.Mobile;
using SageFrame.SageFrameClass.MessageManagement;

namespace AspxCommerce.Core
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {
        }

        public static string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string> {};
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

        public static bool SendEmailForGiftCard(int portalId, int storeId, string cultureName, GiftCard giftCardinfo)
        {
            bool isemailSent = true;
            StoreSettingConfig ssc = new StoreSettingConfig();
            // sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, storeId, portalId,
                                                               cultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeId, portalId, cultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeId, portalId,
                                                       cultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, storeId, portalId, cultureName);

            if (bool.Parse(sendOrderNotice))
            {
                //  MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                // MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
                SageFrameConfig pagebase = new SageFrameConfig();
                //  var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_PLACED, portalId).SingleOrDefault();
                string messageTemplate =
                    "<table width='650' cellspacing='5' cellpadding='0' border='0' bgcolor='#e0e0e0' align='center' style='font:12px Arial, Helvetica, sans-serif;'> <tbody> <tr> <td valign='top' align='center'> <table width='680' cellspacing='0' cellpadding='0' border='0'> <tbody> <tr> <td><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td> <table width='680' cellspacing='0' cellpadding='0' border='0'> <tbody> <tr> <td width='300'><a href='http://%ServerPath%' target='_blank' style='outline:none; border:none;'><img width='143' height='62' src='%LogoSource%' alt='Logo' title='Logo' /></a></td> <td width='191' valign='middle' align='left'>&nbsp;</td> <td width='189' valign='middle' align='right'><b style='padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;'> Date: %DateTime%</b></td> </tr> </tbody> </table> </td> </tr> <tr> <td><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td bgcolor='#fff'> <div style='border:1px solid #c7c7c7; background:#fff; padding:20px'> <table width='650' cellspacing='0' cellpadding='0' border='0' bgcolor='#FFFFFF'> <tbody> <tr> <td> <p style='font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;'> Dear %RecipientName% </p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>You've got gift card from %SenderName% and it is now ready to be used for purchases at our store <a href='%ServerPath%' >%ServerPath%<a></p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card Details</p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card Code : %GiftCardCode%</p>  <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card PinCode :****%PinCode%****</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Balance : %Balance%</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Expiry Date : %ExpiryDate%</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Message from Sender : %Message%</p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Have a great shopping!</p> </td> </tr> </tbody> </table> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Thank You<br /> <span style='font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;'>AspxCommerce Team </span></p> </div> </td> </tr> <tr> <td><img width='1' height='20' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td valign='top' align='center'> <p style='font-size:11px; color:#4d4d4d'>&copy; %DateYear% AspxCommerce. All Rights Reserved.</p> </td> </tr> <tr> <td valign='top' align='center'><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> ";
                // if (template != null)
                // {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (string token in tokens)
                {
                    switch (token)
                    {
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                            break;
                        case "%Balance%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.Balance.ToString());
                            break;
                        case "%PinCode%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.GiftCardPinCode.ToString());

                            break;
                        case "%ExpiryDate%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.ExpireDate.ToString());
                            break;
                        case "%RecipientName%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.RecipientName);
                            break;
                        case "%SenderName%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.SenderName);
                            break;
                        case "%GiftCardCode%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.GiftCardCode);
                            break;
                        case "%Message%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.Messege);
                            break;
                        case "%LogoSource%":
                            // string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                            string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" +
                                         logosrc;
                            messageTemplate = messageTemplate.Replace(token, src);
                            break;
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("dd MMMM yyyy "));

                            break;
                        case "%InquiryEmail%":
                            string x =
                                "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                inquiry + "\" >" + inquiry + "</a>";
                            messageTemplate = messageTemplate.Replace(token, x);
                            break;
                        case "%StoreName%":
                            messageTemplate = messageTemplate.Replace(token, storeName);
                            break;
                        case "%ServerPath%":
                            string path = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                                          "/";
                            messageTemplate = messageTemplate.Replace(token, path);
                            break;
                    }
                }
                // return messageTemplate;
                string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                try
                {
                    MailHelper.SendMailNoAttachment(giftCardinfo.SenderEmail, giftCardinfo.RecipientEmail,
                                                    "You've got a Gift card", messageTemplate, emailSiteAdmin,
                                                    emailSuperAdmin);
                    isemailSent = true;
                }
                catch (Exception)
                {
                    isemailSent = false;
                }
            }
            return isemailSent;
        }

        public static bool SendEmailForGiftCardActivation(int portalId, int storeId, string cultureName, GiftCard giftCardinfo)
        {
            bool isemailSent = true;
            StoreSettingConfig ssc = new StoreSettingConfig();
            // sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, storeId, portalId,
                                                               cultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, storeId, portalId, cultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeId, portalId,
                                                       cultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, storeId, portalId, cultureName);

            if (bool.Parse(sendOrderNotice))
            {
                //  MessageTemplateDataContext dbMessageTemplate = new MessageTemplateDataContext(SystemSetting.SageFrameConnectionString);
                // MessageTokenDataContext messageTokenDB = new MessageTokenDataContext(SystemSetting.SageFrameConnectionString);
                SageFrameConfig pagebase = new SageFrameConfig();
                //  var template = dbMessageTemplate.sp_MessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_PLACED, portalId).SingleOrDefault();
                string messageTemplate =
                    "<table width='650' cellspacing='5' cellpadding='0' border='0' bgcolor='#e0e0e0' align='center' style='font:12px Arial, Helvetica, sans-serif;'> <tbody> <tr> <td valign='top' align='center'> <table width='680' cellspacing='0' cellpadding='0' border='0'> <tbody> <tr> <td><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td> <table width='680' cellspacing='0' cellpadding='0' border='0'> <tbody> <tr> <td width='300'><a href='http://%ServerPath%' target='_blank' style='outline:none; border:none;'><img width='143' height='62' src='%LogoSource%' alt='Logo' title='Logo' /></a></td> <td width='191' valign='middle' align='left'>&nbsp;</td> <td width='189' valign='middle' align='right'><b style='padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;'> Date: %DateTime%</b></td> </tr> </tbody> </table> </td> </tr> <tr> <td><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td bgcolor='#fff'> <div style='border:1px solid #c7c7c7; background:#fff; padding:20px'> <table width='650' cellspacing='0' cellpadding='0' border='0' bgcolor='#FFFFFF'> <tbody> <tr> <td> <p style='font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;'> Dear %RecipientName% </p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>You've got gift card from %SenderName% and it is now ready to be used for purchases at our store <a href='%ServerPath%' >%ServerPath%<a></p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card Details</p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card Code : %GiftCardCode%</p>  <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Gift Card PinCode :****%PinCode%****</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Balance : %Balance%</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Expiry Date : %ExpiryDate%</p> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Message from Sender : %Message%</p> </td> </tr> <tr> <td> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Have a great shopping!</p> </td> </tr> </tbody> </table> <p style='margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;'>Thank You<br /> <span style='font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;'>AspxCommerce Team </span></p> </div> </td> </tr> <tr> <td><img width='1' height='20' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> <tr> <td valign='top' align='center'> <p style='font-size:11px; color:#4d4d4d'>&copy; %DateYear% AspxCommerce. All Rights Reserved.</p> </td> </tr> <tr> <td valign='top' align='center'><img width='1' height='10' src='http://%ServerPath%/blank.gif' alt=' ' /></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> ";
                // if (template != null)
                // {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (string token in tokens)
                {
                    switch (token)
                    {
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                            break;
                        case "%Balance%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.Balance.ToString());
                            break;
                        case "%PinCode%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.GiftCardPinCode.ToString());

                            break;
                        case "%ExpiryDate%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.ExpireDate.ToString());
                            break;
                        case "%RecipientName%":
                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.RecipientName);
                            break;
                        case "%SenderName%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.SenderName);
                            break;
                        case "%GiftCardCode%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.GiftCardCode);
                            break;
                        case "%Message%":

                            messageTemplate = messageTemplate.Replace(token, giftCardinfo.Messege);
                            break;
                        case "%LogoSource%":
                            // string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                            string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" +
                                         logosrc;
                            messageTemplate = messageTemplate.Replace(token, src);
                            break;
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("dd MMMM yyyy "));

                            break;
                        case "%InquiryEmail%":
                            string x =
                                "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                inquiry + "\" >" + inquiry + "</a>";
                            messageTemplate = messageTemplate.Replace(token, x);
                            break;
                        case "%StoreName%":
                            messageTemplate = messageTemplate.Replace(token, storeName);
                            break;
                        case "%ServerPath%":
                            string path = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                                          "/";
                            messageTemplate = messageTemplate.Replace(token, path);
                            break;
                    }
                }
                // return messageTemplate;
                string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                try
                {
                    MailHelper.SendMailNoAttachment(giftCardinfo.SenderEmail, giftCardinfo.RecipientEmail,
                                                    "Activation of Your Gift Card", messageTemplate, emailSiteAdmin,
                                                    emailSuperAdmin);
                    isemailSent = true;
                }
                catch (Exception)
                {
                    isemailSent = false;
                }
            }
            return isemailSent;
        }

        public static void SendEmailForOrder(int portalID, OrderDetailsCollection orderdata, string addressPath,
                                             string templateName, string transID)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            SageFrameConfig pagebase = new SageFrameConfig();
            // sendEmailFrom = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, storeID, portalID, "en-US");
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification,
                                                               orderdata.ObjCommonInfo.StoreID, portalID,
                                                               orderdata.ObjCommonInfo.CultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, orderdata.ObjCommonInfo.StoreID,
                                                       portalID, orderdata.ObjCommonInfo.CultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom,
                                                       orderdata.ObjCommonInfo.StoreID, portalID,
                                                       orderdata.ObjCommonInfo.CultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, orderdata.ObjCommonInfo.StoreID,
                                                         portalID, orderdata.ObjCommonInfo.CultureName);

            if (bool.Parse(sendOrderNotice))
            {
                List<MessageManagementInfo> template =
                    MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                        SystemSetting.ORDER_PLACED, portalID);
                foreach (MessageManagementInfo messageToken in template)
                {
                    string messageTemplate = messageToken.Body;
                    if (template != null)
                    {
                        string[] tokens = GetAllToken(messageTemplate);
                        foreach (string token in tokens)
                        {
                            switch (token)
                            {
                                case "%OrderRemarks%":
                                    messageTemplate = messageTemplate.Replace(token, orderdata.ObjOrderDetails.Remarks);
                                    break;
                                case "%InvoiceNo%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjOrderDetails.InvoiceNumber);
                                    break;
                                case "%OrderStatus%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjOrderDetails.OrderStatus);
                                    break;
                                case "%OrderID%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjOrderDetails.OrderID.ToString
                                                                                  ());
                                    break;
                                case "%BillingAddress%":
                                    string billing = orderdata.ObjBillingAddressInfo.FirstName.ToString() + " " +
                                                     orderdata.ObjBillingAddressInfo.LastName.ToString() +
                                                     "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                    if (orderdata.ObjBillingAddressInfo.CompanyName != null)
                                    {
                                        billing += orderdata.ObjBillingAddressInfo.CompanyName.ToString() +
                                                   "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    billing += orderdata.ObjBillingAddressInfo.City.ToString() + ", " +
                                               orderdata.ObjBillingAddressInfo.Address.ToString() +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               orderdata.ObjBillingAddressInfo.Country.ToString() +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               orderdata.ObjBillingAddressInfo.EmailAddress.ToString() +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               orderdata.ObjBillingAddressInfo.Phone.ToString();
                                    messageTemplate = messageTemplate.Replace(token, billing);
                                    break;
                                case "%ShippingAddress%":
                                    string shipping = "";
                                    if (!orderdata.ObjOrderDetails.IsDownloadable)
                                    {
                                        if (orderdata.ObjOrderDetails.IsMultipleCheckOut == false)
                                        {
                                            shipping = orderdata.ObjShippingAddressInfo.FirstName.ToString() + " " +
                                                       orderdata.ObjShippingAddressInfo.LastName.ToString() +
                                                       "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                            if (orderdata.ObjShippingAddressInfo.CompanyName != null)
                                            {
                                                shipping += orderdata.ObjShippingAddressInfo.CompanyName.ToString() +
                                                            "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                            }

                                            shipping += orderdata.ObjShippingAddressInfo.City.ToString() + ", " +
                                                        orderdata.ObjShippingAddressInfo.Address.ToString() +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        orderdata.ObjShippingAddressInfo.Country.ToString() +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        orderdata.ObjShippingAddressInfo.EmailAddress.ToString() +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        orderdata.ObjShippingAddressInfo.Phone.ToString() +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                        }
                                        else
                                        {
                                            shipping = "Multiple addresses<br />Plese log in to view." +
                                                       "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        }

                                    }
                                    else
                                    {
                                        shipping = "Your Ordered Item is Downloadable Item." +
                                                   "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    messageTemplate = messageTemplate.Replace(token, shipping);
                                    break;
                                case "%UserFirstName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjBillingAddressInfo.FirstName);
                                    break;
                                case "%UserLastName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjBillingAddressInfo.LastName);
                                    break;
                                case "%TransactionID%":
                                    messageTemplate = messageTemplate.Replace(token, transID);
                                    break;
                                case "%PaymentMethodName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjPaymentInfo.PaymentMethodName);
                                    break;
                                case "%DateTimeDay%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                                    break;
                                case "%DateYear%":
                                    messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                                    break;
                                case "%CustomerID%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjOrderDetails.CustomerID.
                                                                                  ToString());
                                    break;
                                case "%PhoneNo%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderdata.ObjBillingAddressInfo.Phone);
                                    break;
                                case "%AccountLogin%":
                                    string account = "";
                                    if (orderdata.ObjCommonInfo.AddedBy.ToString().ToLower() == "anonymoususer" &&
                                        orderdata.ObjOrderDetails.CustomerID == 0)
                                    {
                                        // future login process for annoymoususr 
                                        account +=
                                            "Please Register and log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                        account += "<a  style=\"color: rgb(39, 142, 230);\"  href=" + addressPath +
                                                   "User-Registration.aspx" + ">account</a>";
                                    }
                                    else
                                    {
                                        account +=
                                            "  Please log in to your <span style=\"font-weight: bold; font-size: 11px;\">AspxCommerce</span>";

                                        account += " <a style=\"color: rgb(39, 142, 230);\"  href=" + addressPath +
                                                   "Login.aspx" + ">account</a>";
                                    }
                                    messageTemplate = messageTemplate.Replace(token, account);
                                    break;
                                case "%LogoSource%":
                                    // string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                                    string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                                                 "/" + logosrc;
                                    messageTemplate = messageTemplate.Replace(token, src);
                                    break;
                                case "%DateTime%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              DateTime.Now.ToString("dd MMMM yyyy "));

                                    break;
                                case "%InquiryEmail%":
                                    string x =
                                        "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                        inquiry + "\" >" + inquiry + "</a>";
                                    messageTemplate = messageTemplate.Replace(token, x);
                                    break;
                                case "%StoreName%":
                                    messageTemplate = messageTemplate.Replace(token, storeName);
                                    break;
                            }
                        }
                        // return messageTemplate;
                        string emailStoreAdmin = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom,
                                                                           orderdata.ObjCommonInfo.StoreID
                                                                           , portalID,
                                                                           orderdata.ObjCommonInfo.CultureName);
                        string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                        string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                        MailHelper.SendMailNoAttachment(emailStoreAdmin, orderdata.ObjBillingAddressInfo.EmailAddress,
                                                        messageToken.Subject, messageTemplate, emailSiteAdmin,
                                                        emailSuperAdmin);

                    }
                }
            }
        }

        public static void SendEmailForOrderMobile(OrderInfo orderInfo, UserAddressInfo billingAddress,
                                                   UserAddressInfo shippingAddress, string addressPath,
                                                   string templateName, string transID)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            SageFrameConfig pagebase = new SageFrameConfig();
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, orderInfo.StoreId,
                                                               orderInfo.PortalId, orderInfo.CultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, orderInfo.StoreId, orderInfo.PortalId,
                                                       orderInfo.CultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, orderInfo.StoreId,
                                                       orderInfo.PortalId, orderInfo.CultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, orderInfo.StoreId, orderInfo.PortalId,
                                                         orderInfo.CultureName);

            if (bool.Parse(sendOrderNotice))
            {
                List<MessageManagementInfo> template =
                    MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                        SystemSetting.ORDER_PLACED, orderInfo.PortalId);
                foreach (MessageManagementInfo messageToken in template)
                {

                    string messageTemplate = messageToken.Body;
                    if (template != null)
                    {
                        string[] tokens = GetAllToken(messageTemplate);
                        foreach (string token in tokens)
                        {
                            switch (token)
                            {
                                case "%OrderRemarks%":
                                    messageTemplate = messageTemplate.Replace(token, orderInfo.Remarks);
                                    break;
                                case "%InvoiceNo%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderInfo.InvoiceNumber);
                                    break;
                                case "%OrderID%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderInfo.OrderId.ToString());
                                    break;
                                case "%BillingAddress%":
                                    string billing = billingAddress.FirstName + " " +
                                                     billingAddress.LastName +
                                                     "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                    if (billingAddress.CompanyName != null)
                                    {
                                        billing += billingAddress.CompanyName +
                                                   "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    billing += billingAddress.City + ", " +
                                               billingAddress.Address +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               billingAddress.Country +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               billingAddress.EmailAddress +
                                               "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                               billingAddress.Phone;
                                    messageTemplate = messageTemplate.Replace(token, billing);
                                    break;
                                case "%ShippingAddress%":
                                    string shipping = "";
                                    if (!orderInfo.IsDownloadable || shippingAddress != null)
                                    {
                                        if (orderInfo.IsMultipleCheckOut == false)
                                        {
                                            shipping = shippingAddress.FirstName + " " +
                                                       shippingAddress.LastName +
                                                       "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                            if (shippingAddress.CompanyName != null)
                                            {
                                                shipping += shippingAddress.CompanyName +
                                                            "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                            }

                                            shipping += shippingAddress.City + ", " +
                                                        shippingAddress.Address +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        shippingAddress.Country +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        shippingAddress.EmailAddress +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">" +
                                                        shippingAddress.Phone +
                                                        "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";

                                        }
                                        else
                                        {
                                            shipping = "Multiple addresses<br />Plese log in to view." +
                                                       "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        }

                                    }
                                    else
                                    {
                                        shipping = "Your Ordered Item is Downloadable Item." +
                                                   "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    messageTemplate = messageTemplate.Replace(token, shipping);
                                    break;
                                case "%UserFirstName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              billingAddress.FirstName);
                                    break;
                                case "%UserLastName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              billingAddress.LastName);
                                    break;
                                case "%TransactionID%":
                                    messageTemplate = messageTemplate.Replace(token, transID);
                                    break;
                                case "%PaymentMethodName%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderInfo.PaymentMethodName);
                                    break;
                                case "%DateTimeDay%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                                    break;
                                case "%DateYear%":
                                    messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                                    break;
                                case "%CustomerID%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              orderInfo.CustomerId.ToString());
                                    break;
                                case "%PhoneNo%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              billingAddress.Phone);
                                    break;
                                case "%AccountLogin%":
                                    string account = "";
                                    if (orderInfo.AddedBy.ToLower() == "anonymoususer" &&
                                        orderInfo.CustomerId == 0)
                                    {
                                        // future login process for annoymoususr 
                                        account +=
                                            "Please Register and log in to your ";

                                        account += "<a  style=\"color: rgb(39, 142, 230);\"  href=" + addressPath +
                                                   "User-Registration.aspx" + ">account</a>";
                                    }
                                    else
                                    {
                                        account +=
                                            "  Please log in to your ";

                                        account += " <a style=\"color: rgb(39, 142, 230);\"  href=" + addressPath +
                                                   "Login.aspx" + ">account</a>";
                                    }
                                    messageTemplate = messageTemplate.Replace(token, account);
                                    break;
                                case "%LogoSource%":
                                    // string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                                    string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                                                 "/" + logosrc;
                                    messageTemplate = messageTemplate.Replace(token, src);
                                    break;
                                case "%DateTime%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              DateTime.Now.ToString("dd MMMM yyyy "));

                                    break;
                                case "%InquiryEmail%":
                                    string x =
                                        "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                        inquiry + "\" >" + inquiry + "</a>";
                                    messageTemplate = messageTemplate.Replace(token, x);
                                    break;
                                case "%StoreName%":
                                    messageTemplate = messageTemplate.Replace(token, storeName);
                                    break;
                            }
                        }
                        // return messageTemplate;
                        string emailStoreAdmin = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom,
                                                                           orderInfo.StoreId, orderInfo.PortalId
                                                                           , orderInfo.CultureName);
                        string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                        string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                        MailHelper.SendMailNoAttachment(emailStoreAdmin, billingAddress.EmailAddress,
                                                        messageToken.Subject
                                                        , messageTemplate, emailSiteAdmin, emailSuperAdmin);

                    }
                }
            }
        }

        public static void SendEmailForOrderSIM(int orderId, AspxCommonInfo aspxCommonObj, string custom, string billing,
                                                string billingadd, string billingcity, string shipping,
                                                string shippingadd, string shippingcity, string payment, string info,
                                                string templateName, string transID, string remarks)
        {
            string[] infos = info.Split('#');
            string[] payments = payment.Split('#');
            string[] ids = custom.Split('#');
            StoreSettingConfig ssc = new StoreSettingConfig();
            string sendOrderNotice = ssc.GetStoreSettingsByKey(StoreSetting.SendOrderNotification, aspxCommonObj.StoreID,
                                                               aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string storeName = ssc.GetStoreSettingsByKey(StoreSetting.StoreName, aspxCommonObj.StoreID,
                                                         aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);

            if (bool.Parse(sendOrderNotice))
            {
                List<MessageManagementInfo> template =
                    MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(SystemSetting.ORDER_PLACED,
                                                                                          aspxCommonObj.PortalID);
                SageFrameConfig pagebase = new SageFrameConfig();
                foreach (MessageManagementInfo messageToken in template)
                {
                    string messageTemplate = messageToken.Body;
                    if (template != null)
                    {
                        string[] tokens = GetAllToken(messageTemplate);
                        foreach (string token in tokens)
                        {
                            switch (token)
                            {
                                case "%OrderRemarks%":
                                    messageTemplate = messageTemplate.Replace(token, remarks);
                                    break;
                                case "%InvoiceNo%":
                                    messageTemplate = messageTemplate.Replace(token, infos[3].ToString());
                                    break;
                                case "%OrderID%":
                                    messageTemplate = messageTemplate.Replace(token, orderId.ToString());
                                    break;
                                case "%BillingAddress%":
                                    string billingfull = billing + billingadd + billingcity;
                                    messageTemplate = messageTemplate.Replace(token, billingfull);
                                    break;
                                case "%ShippingAddress%":
                                    string shippingFull = "";
                                    if (!bool.Parse(infos[5].ToString()))
                                    {
                                        if (bool.Parse(infos[6].ToString()) == false)
                                        {
                                            shippingFull = shipping + shippingcity + shippingadd;
                                        }
                                        else
                                        {
                                            shippingFull = "Multiple addresses<br />Plese log in to view." +
                                                           "</td></tr><tr><td height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                        }
                                    }
                                    else
                                    {
                                        shippingFull = "Your Ordered Item is Downloadable Item." +
                                                       "</td></tr><tr><td  height=\"32\" style=\"border-bottom:thin dashed #d1d1d1; padding:10px 0 5px 10px; font:normal 12px Arial, Helvetica, sans-serif\">";
                                    }

                                    messageTemplate = messageTemplate.Replace(token, shippingFull);
                                    break;
                                case "%UserFirstName%":
                                    messageTemplate = messageTemplate.Replace(token, infos[0].ToString());
                                    break;
                                case "%UserLastName%":
                                    messageTemplate = messageTemplate.Replace(token, "");
                                    break;
                                case "%TransactionID%":
                                    messageTemplate = messageTemplate.Replace(token, transID);
                                    break;
                                case "%PaymentMethodName%":
                                    messageTemplate = messageTemplate.Replace(token, payments[0].ToString());
                                    break;
                                case "%DateTimeDay%":
                                    messageTemplate = messageTemplate.Replace(token, payments[1].ToString());
                                    break;
                                case "%DateYear%":
                                    messageTemplate = messageTemplate.Replace(token, DateTime.Now.Year.ToString());
                                    break;
                                case "%CustomerID%":
                                    messageTemplate = messageTemplate.Replace(token, infos[2].ToString());
                                    break;
                                case "%PhoneNo%":
                                    messageTemplate = messageTemplate.Replace(token, infos[4].ToString());
                                    break;
                                case "%AccountLogin%":
                                    string account = "";
                                    if (infos[1].ToString().ToLower() == "anonymoususer" &&
                                        int.Parse(infos[2].ToString()) == 0)
                                    {
                                        // future login process for annoymoususr 
                                        account =
                                            "Please Register and log in to your";

                                        account += "<a  style=\"color: rgb(39, 142, 230);\" href=" +
                                                   ids[6].Replace("Home", "User - Registration") + ">account</a>";
                                    }
                                    else
                                    {
                                        account =
                                            "Please log in to your ";

                                        account += "<a style=\"color: rgb(39, 142, 230);\" href=" +
                                                   ids[6].Replace("Home", "Login") + ">account</a>";
                                    }
                                    messageTemplate = messageTemplate.Replace(token, account);
                                    break;
                                case "%LogoSource%":
                                    //    string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/Templates/" + templateName + "/images/aspxcommerce.png";
                                    string src = " http://" +
                                                 HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/" +
                                                 logosrc;
                                    messageTemplate = messageTemplate.Replace(token, src);
                                    break;
                                case "%DateTime%":
                                    messageTemplate = messageTemplate.Replace(token,
                                                                              DateTime.Now.ToString("dd MMMM yyyy "));
                                    break;
                                case "%StoreName%":
                                    messageTemplate = messageTemplate.Replace(token, storeName);
                                    break;
                                case "%InquiryEmail%":
                                    string x =
                                        "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                        inquiry + "\" >" + inquiry + "</a>";
                                    messageTemplate = messageTemplate.Replace(token, x);
                                    break;

                            }
                        }
                        // return messageTemplate;
                        string emailStoreAdmin = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom,
                                                                           aspxCommonObj.StoreID,
                                                                           aspxCommonObj.PortalID,
                                                                           aspxCommonObj.CultureName);
                        string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                        string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                        MailHelper.SendMailNoAttachment(emailStoreAdmin, infos[7].ToString(), messageToken.Subject,
                                                        messageTemplate,
                                                        emailSuperAdmin, emailSiteAdmin);

                    }
                }
            }
        }

        public static void SendEmailForOrderStatus(AspxCommonInfo aspxCommonObj, string recieverEmail,
                                                   string billingshipping, string tablebody, string additionalFields,
                                                   string templateName)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, "en-US");
            string inquiry = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, "en-US");
            List<MessageManagementInfo> template =
                MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                    SystemSetting.ORDER_STATUS_CHANGED, aspxCommonObj.PortalID);
            SageFrameConfig pagebase = new SageFrameConfig();
            foreach (MessageManagementInfo messageToken in template)
            {
                string messageTemplate = messageToken.Body.ToString();
                if (template != null)
                {
                    string[] tokens = GetAllToken(messageTemplate);
                    string[] fields = additionalFields.Split('#');

                    string orderstatus = fields[0];
                    string storeName = fields[1];
                    string storeDescription = fields[2];
                    string customerName = fields[3];
                    string orderID = fields[4];
                    string paymentMethod = fields[5];
                    string shipingMethod = fields[6];
                    string invoice = fields[7];
                    string fullname = GetFullName(aspxCommonObj.PortalID, int.Parse(orderID));
                    foreach (string token in tokens)
                    {
                        switch (token)
                        {
                            case "%OrderStatus%":
                                messageTemplate = messageTemplate.Replace(token, orderstatus);
                                break;
                            case "%StoreName%":
                                messageTemplate = messageTemplate.Replace(token, storeName);
                                break;
                            case "%StoreDescription%":
                                messageTemplate = messageTemplate.Replace(token, storeDescription);
                                break;
                            case "%ShippingMethod%":
                                messageTemplate = messageTemplate.Replace(token, shipingMethod);
                                break;
                            case "%InvoiceNo%":
                                messageTemplate = messageTemplate.Replace(token, invoice);
                                break;
                            case "%OrderID%":
                                messageTemplate = messageTemplate.Replace(token, orderID);
                                break;
                            case "%BillingShipping%":
                                messageTemplate = messageTemplate.Replace(token, billingshipping);
                                break;
                            case "%PaymentMethodName%":
                                messageTemplate = messageTemplate.Replace(token, paymentMethod);
                                break;
                            case "%DateTimeWithTime%":
                                messageTemplate = messageTemplate.Replace(token,
                                                                          DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                                break;
                            case "%DateTime%":
                                messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("MM/dd/yyyy"));
                                break;
                            case "%CustomerName%":
                                messageTemplate = messageTemplate.Replace(token, fullname);
                                break;
                            case "%LogoSource%":
                                string src = " http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                                             "/" + logosrc;
                                messageTemplate = messageTemplate.Replace(token, src);
                                break;
                            case "%ItemDetailsTable%":
                                messageTemplate = messageTemplate.Replace(token, tablebody);
                                break;
                            case "%UserFirstName%":
                                messageTemplate = messageTemplate.Replace(token, fullname);
                                break;
                            case "%UserLastName%":
                                messageTemplate = messageTemplate.Replace(token, "");
                                break;
                            case "%InquiryEmail%":
                                string x =
                                    "<a  target=\"_blank\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"mailto:" +
                                    inquiry + "\" >" + inquiry + "</a>";
                                messageTemplate = messageTemplate.Replace(token, x);
                                break;

                        }
                    }
                    // return messageTemplate;

                    //  string replacedMessageTemplate = EmailTemplate.GetTemplateForOrderStatus(template.Body, billingShipping, itemTable, additionalFields);
                    string emailStoreAdmin = ssc.GetStoreSettingsByKey(StoreSetting.SendEcommerceEmailsFrom,
                                                                       aspxCommonObj.StoreID,
                                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
                    string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                    string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                    MailHelper.SendMailNoAttachment(emailStoreAdmin, recieverEmail, messageToken.Subject,
                                                    messageTemplate, emailSuperAdmin, emailSiteAdmin);

                }
            }
        }

        public static void SendEmailForSharedWishList(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
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
                if (j%3 == 0)
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

                char[] spliter = {','};
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

        public static void SendEmailForReferFriend(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj,
                                                   WishItemEmailInfo messageBodyDetail)
        {
            var messageBodyHtml = "";
            messageBodyHtml +=
                "<table style='font:12px Arial, Helvetica, sans-serif;' width='100%' border='0' cellspacing='0' cellpadding='0'><tr>";
            messageBodyHtml +=
                "<td width='33%'><div style='border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;'>";
            messageBodyHtml += "<img src='" + messageBodyDetail.src + "' alt='" + messageBodyDetail.alt +
                               "' width='250' />";
            messageBodyHtml +=
                "<p style='margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;'> <span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'>";
            messageBodyHtml += messageBodyDetail.title + "</span><br />";
            messageBodyHtml +=
                "<span style='font-weight:bold; font-size:11px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;'>Price:</span>";
            messageBodyHtml += messageBodyDetail.price + "<br />";
            // messageBodyHtml += "<span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif;text-decoration:blink; text-shadow:1px 1px 0 #fff;'><a style='color: rgb(39, 142, 230);' href='" + href + "'>' + getLocale(AspxReferToFriend, 'click here to view all details') + '</a></span> ";
            messageBodyHtml +=
                "<span style='font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif;text-decoration:blink; text-shadow:1px 1px 0 #fff;'><a style='color: rgb(39, 142, 230);' href='" +
                messageBodyDetail.href + "'>click here to view all details</a></span>";
            messageBodyHtml += "</p> </div></td></tr> </table>";
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            List<MessageManagementInfo> template =
                MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                    SystemSetting.REFER_A_FRIEND_EMAIL, aspxCommonObj.PortalID);
            foreach (MessageManagementInfo messageToken in template)
            {
                string messageTemplate = messageToken.Body.ToString();
                string src = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                if (messageToken != null)
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
                                messageTemplate = messageTemplate.Replace(token, referToFriendObj.SenderName);
                                break;
                                //case "%senderEmail%":
                                //    messageTemplate = messageTemplate.Replace(token, senderEmail);
                                //    break;
                            case "%MessageDetails%":
                                messageTemplate = messageTemplate.Replace(token, referToFriendObj.Message);
                                break;
                            case "%ItemDetailsTable%":
                                messageTemplate = messageTemplate.Replace(token, messageBodyHtml);
                                break;
                            case "%LogoSource%":
                                string imgSrc = "http://" + src + logosrc;
                                messageTemplate = messageTemplate.Replace(token, imgSrc);
                                break;
                            case "%serverPath%":
                                messageTemplate = messageTemplate.Replace(token, "http://" + src);
                                break;
                            case "%DateYear%":
                                messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                                break;
                        }
                    }
                }

                string emailSuperAdmin;
                string emailSiteAdmin;
                SageFrameConfig pagebase = new SageFrameConfig();
                emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                MailHelper.SendMailNoAttachment(referToFriendObj.SenderEmail, referToFriendObj.ReceiverEmail,
                                                referToFriendObj.Subject, messageTemplate, emailSiteAdmin,
                                                emailSuperAdmin);
            }
        }

        public static string GetFullName(int portalId, int orderid)
        {
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            paramCol.Add(new KeyValuePair<string, object>("@OrderID", orderid));
            SQLHandler sageSQL = new SQLHandler();
            return sageSQL.ExecuteAsScalar<string>("[dbo].[usp_Aspx_GetUserFirstandLastNamebyorder]", paramCol);

        }

        public static void SendOutOfNotification(AspxCommonInfo aspxCommonObj, SendEmailInfo emailInfo, string variantId,
                                                 string variantValue, string Sku, string ProductUrl)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            List<MessageManagementInfo> template =
                MessageManagementController.GetMessageTemplateByMessageTemplateTypeID(
                    SystemSetting.OUT_OF_STOCK_NOTIFICATION, aspxCommonObj.PortalID);
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
                                messageTemplate = messageTemplate.Replace(token, emailInfo.SenderName);
                                break;
                            case "%MessageDetails%":
                                messageTemplate = messageTemplate.Replace(token, emailInfo.Subject);
                                break;
                            case "%ItemDetailsTable%":
                                messageTemplate = messageTemplate.Replace(token, emailInfo.MessageBody);
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

                char[] spliter = {','};
                emailInfo.ReceiverEmail = emailInfo.ReceiverEmail.Substring(0, emailInfo.ReceiverEmail.LastIndexOf(','));
                variantId = variantId.Substring(0, variantId.LastIndexOf(','));
                variantValue = variantValue.Substring(0, variantValue.LastIndexOf('@'));

                string[] receiverIDs = emailInfo.ReceiverEmail.Split(spliter);
                string[] variantIds = variantId.Split(spliter);
                string[] variantValues = variantValue.Split('@');
                for (int i = 0; i < receiverIDs.Length; i++)
                {
                    string msgTemplate = string.Empty;
                    string receiverEmailId = receiverIDs[i];
                    string varId = variantIds[i];
                    string varValue = variantValues[i];
                    msgTemplate = messageTemplate;
                    msgTemplate = msgTemplate.Replace("%UserEmail%", receiverEmailId);
                    if (varValue != "")
                    {
                        var sku = Sku + '(' + varValue + ')';
                        msgTemplate = msgTemplate.Replace("#sku#", sku);
                    }
                    else
                    {
                        msgTemplate = msgTemplate.Replace("#sku#", Sku);
                    }
                    if (varId != "")
                    {
                        msgTemplate = msgTemplate.Replace("#server#",
                                                          ProductUrl + "item/" + Sku + ".aspx?varId=" + varId);
                    }
                    else
                    {
                        msgTemplate = msgTemplate.Replace("#server#", ProductUrl + "item/" + Sku + ".aspx");
                    }
                    emailInfo.Subject = messageToken.Subject;
                    string emailSuperAdmin;
                    string emailSiteAdmin;
                    SageFrameConfig pagebase = new SageFrameConfig();
                    emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
                    emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                    MailHelper.SendMailNoAttachment(emailInfo.SenderEmail, receiverEmailId, emailInfo.Subject,
                                                    msgTemplate, emailSiteAdmin, emailSuperAdmin);

                }
            }
        }

        public static void SendEmailForReturns(AspxCommonInfo aspxCommonObj, SendEmailInfo sendEmailObj)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            string logosrc = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, aspxCommonObj.StoreID,
                                                       aspxCommonObj.PortalID, aspxCommonObj.CultureName);
            string src = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
            string imgSrc = "http://" + src + logosrc;
            string serverPath = "http://" + src;

            char[] spliter = {','};
            string s = sendEmailObj.MessageBody;
            string[] values = s.Split('#');
            string msgTemplate = string.Empty;
            string returnID = string.Empty;
            string orderID = string.Empty;
            string item = string.Empty;
            string variant = string.Empty;
            string qty = string.Empty;
            string status = string.Empty;
            string action = string.Empty;

            returnID = values[1];
            orderID = values[0];
            item = values[2];
            variant = values[3];
            qty = values[4];
            status = values[5];
            action = values[6];

            AspxGetTemplates agt = new AspxGetTemplates();

            string messageTemplate = agt.ReturnNotificationEmailTemplate().ToString();
            if (messageTemplate != null)
            {
                string[] tokens = GetAllToken(messageTemplate);
                foreach (var token in tokens)
                {
                    switch (token)
                    {
                        case "%LogoSource%":
                            messageTemplate = messageTemplate.Replace(token, imgSrc);
                            break;
                        case "%DateTime%":
                            messageTemplate = messageTemplate.Replace(token, DateTime.Now.ToString("MM/dd/yyyy"));
                            break;
                        case "%ReturnID%":
                            messageTemplate = messageTemplate.Replace(token, returnID);
                            break;
                        case "%OrderID%":
                            messageTemplate = messageTemplate.Replace(token, orderID);
                            break;
                        case "%ItemName%":
                            messageTemplate = messageTemplate.Replace(token, item);
                            break;
                        case "%Variant%":
                            messageTemplate = messageTemplate.Replace(token, variant.Length > 0 ? variant : "N/A");
                            break;
                        case "%Quantity%":
                            messageTemplate = messageTemplate.Replace(token, qty);
                            break;
                        case "%ReturnStatus%":
                            messageTemplate = messageTemplate.Replace(token, status.Length > 0 ? status : "N/A");
                            break;
                        case "%ReturnAction%":
                            messageTemplate = messageTemplate.Replace(token, action.Length > 0 ? action : "N/A");
                            break;
                        case "%ServerPath%":
                            messageTemplate = messageTemplate.Replace(token, src);
                            break;
                        case "%DateYear%":
                            messageTemplate = messageTemplate.Replace(token, System.DateTime.Now.Year.ToString());
                            break;
                    }
                }
            }
            msgTemplate = sendEmailObj.Message;
            string emailSuperAdmin;
            string emailSiteAdmin;

            SageFrameConfig pagebase = new SageFrameConfig();
            emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            MailHelper.SendMailNoAttachment(sendEmailObj.SenderEmail, sendEmailObj.ReceiverEmail, sendEmailObj.Subject,
                                            messageTemplate, emailSiteAdmin, emailSuperAdmin);
        }
    }
}
