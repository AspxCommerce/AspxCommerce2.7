using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxWareHouseProvider
    {
       public AspxWareHouseProvider()
       {
       }

       public static List<WareHouse> GetAllWareHouse(int offset, int limit, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           paramCol.Add(new KeyValuePair<string, object>("@limit", limit));
           paramCol.Add(new KeyValuePair<string, object>("@offset", offset));
           SQLHandler sageSQL = new SQLHandler();
           List<WareHouse> wList = sageSQL.ExecuteAsList<WareHouse>("[dbo].[usp_Aspx_SelectWareHousesAll]", paramCol);
           return wList;
       }
   
       public static List<WareHouseAddress> GetAllWareHouseById(int wareHouseID, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
           paramCol.Add(new KeyValuePair<string, object>("@WareHouseID", wareHouseID));
           SQLHandler sageSQL = new SQLHandler();
           List<WareHouseAddress> wList = sageSQL.ExecuteAsList<WareHouseAddress>("[usp_Aspx_SelectWareHouseByID]", paramCol);
           return wList;
       }

       public static void DeleteWareHouse(int wareHouseId, AspxCommonInfo aspxCommonObj)
       {

           List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSP(aspxCommonObj);
           paramCol.Add(new KeyValuePair<string, object>("@WareHouseID", wareHouseId));
           SQLHandler sageSQL = new SQLHandler();
           sageSQL.ExecuteNonQuery("[usp_Aspx_DeleteAspx_WareHouse]", paramCol);


       }

       public static void AddUpDateWareHouse(WareHouseAddress wareHouse, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           paramCol.Add(new KeyValuePair<string, object>("@WareHouseID", wareHouse.WareHouseID));
           paramCol.Add(new KeyValuePair<string, object>("@IsPrimary", wareHouse.IsPrimary));
           paramCol.Add(new KeyValuePair<string, object>("@Name", wareHouse.Name));
           paramCol.Add(new KeyValuePair<string, object>("@StreetAddress1", wareHouse.StreetAddress1));
           paramCol.Add(new KeyValuePair<string, object>("@StreetAddress2", wareHouse.StreetAddress2));
           paramCol.Add(new KeyValuePair<string, object>("@City", wareHouse.City));
           paramCol.Add(new KeyValuePair<string, object>("@Country", wareHouse.Country));
           paramCol.Add(new KeyValuePair<string, object>("@State", wareHouse.State));
           paramCol.Add(new KeyValuePair<string, object>("@PostalCode", wareHouse.PostalCode));
           paramCol.Add(new KeyValuePair<string, object>("@Phone", wareHouse.Phone));
           paramCol.Add(new KeyValuePair<string, object>("@Fax", wareHouse.Fax));
           paramCol.Add(new KeyValuePair<string, object>("@Email", wareHouse.Email));
           paramCol.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
           SQLHandler sageSQL = new SQLHandler();
           sageSQL.ExecuteNonQuery("[usp_Aspx_InsertUpdateAspx_WareHouse]", paramCol);
       }
    }
}
