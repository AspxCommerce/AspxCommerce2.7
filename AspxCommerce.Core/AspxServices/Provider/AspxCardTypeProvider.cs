using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxCardTypeProvider
    {
       public AspxCardTypeProvider()
       {
       }

       public static List<CardTypeInfo> GetAllCardTypeList(int offset, int limit, AspxCommonInfo aspxCommonObj, string cardTypeName, bool? isActive)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@CardTypeName", cardTypeName));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               SQLHandler sqlH = new SQLHandler();
               List<CardTypeInfo> lstCardType= sqlH.ExecuteAsList<CardTypeInfo>("usp_Aspx_GetCardTypeInGrid", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CardTypeID", cardTypeSaveObj.CardTypeID));
               parameter.Add(new KeyValuePair<string, object>("@CardTypeName", cardTypeSaveObj.CardTypeName));
               parameter.Add(new KeyValuePair<string, object>("@ImagePath", uploadedFile));
               parameter.Add(new KeyValuePair<string, object>("@AlternateText", cardTypeSaveObj.AlternateText));
               parameter.Add(new KeyValuePair<string, object>("@IsActive", cardTypeSaveObj.IsActive));
               SQLHandler sqlH = new SQLHandler();
               List<CardTypeInfo> lstCardType= sqlH.ExecuteAsList<CardTypeInfo>("[dbo].[usp_Aspx_AddUpdateCardType]", parameter);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CardTypeID", cardTypeID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeByID]", parameterCollection);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CardTypeIDs", cardTypeIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteCardTypeMultipleSelected]", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
