using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public  class AspxWareHouseController
    {
      public AspxWareHouseController()
      {
      }
      public static List<WareHouse> GetAllWareHouse(int offset, int limit, AspxCommonInfo aspxCommonObj)
      {
          List<WareHouse> wList = AspxWareHouseProvider.GetAllWareHouse(offset, limit, aspxCommonObj);
          return wList;
      }

      public static List<WareHouseAddress> GetAllWareHouseById(int wareHouseID, AspxCommonInfo aspxCommonObj)
      {
          List<WareHouseAddress> wList = AspxWareHouseProvider.GetAllWareHouseById(wareHouseID, aspxCommonObj);
          return wList;
      }

      public static void DeleteWareHouse(int wareHouseId, AspxCommonInfo aspxCommonObj)
      {
          AspxWareHouseProvider.DeleteWareHouse(wareHouseId, aspxCommonObj);
      }

      public static void AddUpDateWareHouse(WareHouseAddress wareHouse, AspxCommonInfo aspxCommonObj)
      {
          AspxWareHouseProvider.AddUpDateWareHouse(wareHouse, aspxCommonObj);
      }
    }
}
