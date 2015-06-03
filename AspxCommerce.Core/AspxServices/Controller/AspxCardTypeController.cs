using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxCardTypeController
    {
       public AspxCardTypeController()
       {
       }

       public static List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, AspxCommonInfo aspxCommonObj, string cardTypeName, bool? isActive)
       {
           try
           {

               List<CardTypeInfo> lstCardType = AspxCardTypeProvider.GetAllCardTypeList(offset, limit, aspxCommonObj, cardTypeName, isActive);
               return lstCardType;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<CardTypeInfo> AddUpdateCardType(AspxCommonInfo aspxCommonObj, CardTypeSaveInfo cardTypeSaveObj, string uploadedFile)
       {

           try
           {
               List<CardTypeInfo> lstCardType = AspxCardTypeProvider.AddUpdateCardType(aspxCommonObj,cardTypeSaveObj,uploadedFile);
               return lstCardType;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCardTypeByID(int cardTypeID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCardTypeProvider.DeleteCardTypeByID(cardTypeID, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void DeleteCardTypeMultipleSelected(string cardTypeIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCardTypeProvider.DeleteCardTypeMultipleSelected(cardTypeIDs, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
