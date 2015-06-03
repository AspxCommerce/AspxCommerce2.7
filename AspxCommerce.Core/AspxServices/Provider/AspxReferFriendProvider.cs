using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
  public class AspxReferFriendProvider
    {
      public AspxReferFriendProvider()
      {
      }
      //-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------
      public static void SaveAndSendEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
      {
          try
          {
              SaveEmailMessage(aspxCommonObj, referToFriendObj);
              SendEmail(aspxCommonObj, referToFriendObj, messageBodyDetail);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //--------------------bind Email list in Grid--------------------------

      public static List<ReferToFriendInfo> GetAllReferToAFriendEmailList(int offset, int limit, string senderName, string senderEmail, string receiverName, string receiverEmail, string subject, int storeID, int portalID, string userName)
      {
          try
          {
              List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
              parameter.Add(new KeyValuePair<string, object>("@offset", offset));
              parameter.Add(new KeyValuePair<string, object>("@limit", limit));
              parameter.Add(new KeyValuePair<string, object>("@SenderName", senderName));
              parameter.Add(new KeyValuePair<string, object>("@SenderEmail", senderEmail));
              parameter.Add(new KeyValuePair<string, object>("@ReceiverName", receiverName));
              parameter.Add(new KeyValuePair<string, object>("@ReceiverEmail", receiverEmail));
              parameter.Add(new KeyValuePair<string, object>("@Subject", subject));
              parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
              parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
              parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
              SQLHandler sqlH = new SQLHandler();
              List<ReferToFriendInfo> bind = sqlH.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetAllReferAFriendEmailsInGrid", parameter);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //-----------------Delete Email list --------------------------------

      public static void DeleteReferToFriendEmailUser(string emailAFriendIDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
              parameter.Add(new KeyValuePair<string, object>("@EmailAFriendID", emailAFriendIDs));
              SQLHandler sqlH = new SQLHandler();
              sqlH.ExecuteNonQuery("usp_Aspx_DeleteReferToFriendUser", parameter);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------Get UserReferred Friends--------------------------

      public static List<ReferToFriendInfo> GetUserReferredFriends(int offset, int limit, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
              parameter.Add(new KeyValuePair<string, object>("@offset", offset));
              parameter.Add(new KeyValuePair<string, object>("@limit", limit));
              SQLHandler sqlh = new SQLHandler();
              List<ReferToFriendInfo> lstReferFriend= sqlh.ExecuteAsList<ReferToFriendInfo>("usp_Aspx_GetUserReferredFriends", parameter);
              return lstReferFriend;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static void SaveEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj)
      {
          List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
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

      public static void SendEmail(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
      {
          try
          {
              EmailTemplate.SendEmailForReferFriend(aspxCommonObj, referToFriendObj, messageBodyDetail);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

    }
}
