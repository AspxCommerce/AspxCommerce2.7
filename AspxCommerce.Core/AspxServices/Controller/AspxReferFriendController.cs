using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxReferFriendController
    {
       public AspxReferFriendController()
       {
       }

       //-------------------------Save AND SendEmail Messages For Refer-A-Friend----------------
       public static void SaveAndSendEmailMessage(AspxCommonInfo aspxCommonObj, ReferToFriendEmailInfo referToFriendObj, WishItemEmailInfo messageBodyDetail)
       {
           try
           {
               AspxReferFriendProvider.SaveAndSendEmailMessage(aspxCommonObj, referToFriendObj, messageBodyDetail);
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
               List<ReferToFriendInfo> bind = AspxReferFriendProvider.GetAllReferToAFriendEmailList(offset, limit, senderName, senderEmail, receiverName, receiverEmail, subject, storeID, portalID, userName);
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
               AspxReferFriendProvider.DeleteReferToFriendEmailUser(emailAFriendIDs, aspxCommonObj);
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
               List<ReferToFriendInfo> lstReferFriend = AspxReferFriendProvider.GetUserReferredFriends(offset, limit, aspxCommonObj);
               return lstReferFriend;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
