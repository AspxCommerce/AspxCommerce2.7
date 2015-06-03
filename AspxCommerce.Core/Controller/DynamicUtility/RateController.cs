using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class RateController
    {
      
        public List<CountryList> LoadCountry()
        {
            var sqlHandler = new SQLHandler();
            List<CountryList> cl = sqlHandler.ExecuteAsList<CountryList>("usp_Aspx_GetCountryList");
            return cl;
        }
      
        public List<States> GetStatesByCountry(string countryCode)
        {
            var sqlHandler = new SQLHandler();
            var paramList = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("@CountryCode", countryCode) };
            List<States> cl = sqlHandler.ExecuteAsList<States>("usp_Aspx_GetStateList", paramList);
           return cl;
        }

      
        public List<CommonRateList> GetRate(ItemListDetails itemsDetail)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                var rateInfo = new List<CommonRateList>();
                //to get Dynamic Fuctions info
                List<MethodList> rateMethods = GetAllMethodsFromProvider(itemsDetail.CommonInfo.StoreID,
                                                                         itemsDetail.CommonInfo.PortalID);
                WareHouseAddress originAddress = GetWareHouseAddress(itemsDetail.CommonInfo.StoreID,
                                                                     itemsDetail.CommonInfo.PortalID);


                itemsDetail.WareHouseAddress = originAddress;


                if (itemsDetail.BasketItems.Count > 0)
                {

                    foreach (var method in rateMethods)
                    {
                        List<ParamList> paramList = GetParamsOfMethod(method.DynamicMethodId);
                        foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            Type type = ass.GetType(method.NameSpace + "." + method.ClassName, false);
                            var paramCollection = new List<object>();


                            if (type != null)
                            {

                                for (int i = 0; i < paramList.Count; i++)
                                {
                                    var param = ass.GetType(method.NameSpace + "." + paramList[i].ParameterName, false);
                                    //  Type listType = typeof (List<>).MakeGenericType(new Type[] {param});
                                    Type t = itemsDetail.GetType();

                                    if (param != null)
                                    {
                                        switch (paramList[i].ParameterType)
                                        {
                                            case "list":
                                                //itemslist
                                                var itemsList =
                                                    DynamicUtility.TransferDataToList(itemsDetail.BasketItems,
                                                                                      itemsDetail,
                                                                                      param, method.AssemblyName);
                                                paramCollection.Add(itemsList);
                                                break;

                                            default:
                                                var pa = DynamicUtility.PassMembersValue(itemsDetail, param,
                                                                                         method.AssemblyName);
                                                paramCollection.Add(pa);
                                                break;

                                        }
                                    }
                                    if (param == null && paramList[i].ParameterName != "")
                                    {
                                        switch (paramList[i].ParameterName)
                                        {
                                            case "storeId":
                                                paramCollection.Add(itemsDetail.CommonInfo.StoreID);
                                                break;
                                            case "portalId":
                                                paramCollection.Add(itemsDetail.CommonInfo.PortalID);
                                                break;
                                            case "providerId":
                                                paramCollection.Add(method.ShippingProviderId);
                                                break;
                                        }
                                    }

                                    // Type listType1 = param1.MakeGenericType(new Type[] { param1 });
                                    //  ((method.ClassName) Activator.CreateInstance(Type.GetType(method.ClassName)));
                                    // DynamicUtility.Cast<listType>(originAddress);
                                }



                                //var obj = new Object[] {originAddress, da, packagedimension};

                                object instance = Activator.CreateInstance(type);
                                MethodInfo fn = type.GetMethod(method.MethodName);
                                var obj = paramCollection.ToArray();
                                System.Net.ServicePointManager.Expect100Continue = false;
                                var rateResponse = fn.Invoke(instance,
                                                             BindingFlags.InvokeMethod | BindingFlags.Public |
                                                             BindingFlags.Static,
                                                             null, obj, null);

                                List<CommonRateList> cl = DynamicUtility.CastToList<CommonRateList>(rateResponse);

                                //list of available shipping method of store
                                rateInfo.AddRange(cl);

                                break;
                            }

                        }

                    }
                }
                //GetProvidersAvailableMethod
                var allowedshippingMethods = GetProvidersAvailableMethod(itemsDetail.CommonInfo.StoreID,
                                                                         itemsDetail.CommonInfo.PortalID);
                //filtering allowed shipping methods only
                var filterdmethods = new List<CommonRateList>();
                //  filterdmethods = rateInfo.Where(x => allowedshippingMethods.Any(y => x.ShippingMethodName == y.ShippingMethodName)).
                // ToList();

                foreach (var commonRateList in allowedshippingMethods)
                {
                    foreach (var info in rateInfo)
                    {
                        if (info.ShippingMethodName == commonRateList.ShippingMethodName)
                        {
                            var filterdmethod = info;
                            filterdmethod.ShippingMethodId = commonRateList.ShippingMethodID;
                            filterdmethods.Add(filterdmethod);
                            break;
                        }
                    }
                }

                var coreService = new AspxCommerceWebService();
                List<ShippingMethodInfo> flatRates =
                    coreService.GetShippingMethodByWeight(itemsDetail.CommonInfo.StoreID,
                                                          itemsDetail.CommonInfo.PortalID,
                                                          itemsDetail.CommonInfo.
                                                              CustomerID,
                                                          itemsDetail.CommonInfo.UserName,
                                                          itemsDetail.CommonInfo.
                                                              CultureName,
                                                          itemsDetail.CommonInfo.
                                                              SessionCode);
                foreach (var item in flatRates)
                {
                    var cr = new CommonRateList
                                 {
                                     CurrencyCode = "",
                                     ImagePath = item.ImagePath,
                                     ShippingMethodId = item.ShippingMethodID,
                                     DeliveryTime = item.DeliveryTime,
                                     ShippingMethodName = item.ShippingMethodName,
                                     TotalCharges = decimal.Parse(item.ShippingCost)
                                 };
                    filterdmethods.Insert(0, cr);
                    //filterdmethods.Add(cr);
                }

                return filterdmethods;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<ShippingMethodInfo> GetProvidersAvailableMethod(int storeId, int portalId)
        {
            var sqlHandler = new SQLHandler();
            var paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", "en-US"));
            return sqlHandler.ExecuteAsList<ShippingMethodInfo>("[usp_Aspx_GetShippingMethodsOfRealTime]", paramList);

        }

        private WareHouseAddress GetWareHouseAddress(int storeId, int portalId)
        {

            var sqlHandler = new SQLHandler();
            var paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            var cl = sqlHandler.ExecuteAsObject<WareHouseAddress>("[usp_Aspx_GetActiveWareHouse]", paramList);
            return cl;
        }

        private List<MethodList> GetAllMethodsFromProvider(int storeId, int portalId)
        {
            var sqlHandler = new SQLHandler();
            var paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            List<MethodList> cl = sqlHandler.ExecuteAsList<MethodList>("dbo.usp_Aspx_GetDynamicRateMethodList", paramList);
            return cl;
        }
        private List<ParamList> GetParamsOfMethod(int id)
        {
            var sqlHandler = new SQLHandler();
            var paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@DynamicMethodID", id));
            List<ParamList> cl = sqlHandler.ExecuteAsList<ParamList>("dbo.usp_Aspx_GetParamListByID", paramList);
            return cl;
        }
    }
}
