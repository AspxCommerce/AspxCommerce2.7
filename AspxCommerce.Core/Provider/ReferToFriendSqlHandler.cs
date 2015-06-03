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

namespace AspxCommerce.Core
{
    public class ReferToFriendSqlHandler
    {
        public void SaveEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@ItemID", referToFriendObj.ItemID));
            parameter.Add(new KeyValuePair<string, object>("@SenderName", referToFriendObj.SenderName));
            parameter.Add(new KeyValuePair<string, object>("@SenderEmail", referToFriendObj.SenderEmail));
            parameter.Add(new KeyValuePair<string, object>("@ReceiverName", referToFriendObj.ReceiverName));
            parameter.Add(new KeyValuePair<string, object>("@Receiveremail", referToFriendObj.ReceiverEmail));
            parameter.Add(new KeyValuePair<string, object>("@Subject", referToFriendObj.Subject));
            parameter.Add(new KeyValuePair<string, object>("@Message", referToFriendObj.Message));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_SaveMessage", parameter);
        }

        public void SendEmail(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
        {
            try
            {
                EmailTemplate.SendEmailForReferFriend(aspxCommonObj, referToFriendObj, messageBodyDetail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SaveShareWishListEmailMessage(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@ItemIDs", wishlistObj.ItemID));
            parameter.Add(new KeyValuePair<string, object>("@SenderName", wishlistObj.SenderName));
            parameter.Add(new KeyValuePair<string, object>("@SenderEmail", wishlistObj.SenderEmail));
            parameter.Add(new KeyValuePair<string, object>("@ReceiverEmailID", wishlistObj.ReceiverEmail));
            parameter.Add(new KeyValuePair<string, object>("@Subject", wishlistObj.Subject));
            parameter.Add(new KeyValuePair<string, object>("@Message", wishlistObj.Message));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[usp_Aspx_SaveShareWishListEmail]", parameter);

        }

        public void SendShareWishItemEmail(AspxCommonInfo aspxCommonObj, WishItemsEmailInfo wishlistObj)
        {
            try
            {
                EmailTemplate.SendEmailForSharedWishList(aspxCommonObj,wishlistObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
