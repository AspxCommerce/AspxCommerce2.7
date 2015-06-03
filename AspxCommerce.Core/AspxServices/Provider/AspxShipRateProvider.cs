using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxShipRateProvider
    {
        public AspxShipRateProvider()
        {
        }

        public static List<CountryList> LoadCountry()
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<CountryList> lstCountry = sqlHandler.ExecuteAsList<CountryList>("usp_Aspx_GetCountryList");
            return lstCountry;
        }

        public static List<States> GetStatesByCountry(string countryCode)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@CountryCode", countryCode));
            List<States> lstState = sqlHandler.ExecuteAsList<States>("usp_Aspx_GetStateList", paramList);
            return lstState;
        }


        public static List<CommonRateList> GetRate(ItemListDetails itemsDetail)
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
                        List<ParamList> paramList = GetParamsOfMethod(method.DynamicMethodId, itemsDetail.CommonInfo.StoreID,itemsDetail.CommonInfo.PortalID);
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
                                                                         itemsDetail.CommonInfo.PortalID, itemsDetail.CommonInfo.CultureName);
                //filtering allowed shipping methods only
                var filterdmethods = new List<CommonRateList>();
                //  filterdmethods = rateInfo.Where(x => allowedshippingMethods.Any(y => x.ShippingMethodName == y.ShippingMethodName)).
                // ToList();

                AspxCommerceWebService coreService = new AspxCommerceWebService();
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
                    //filterdmethods.Insert(0, cr);
                    filterdmethods.Add(cr);
                }

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


                return filterdmethods;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private static List<ShippingMethodInfo> GetProvidersAvailableMethod(int storeId, int portalId,string cultureName)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            paramList.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            List<ShippingMethodInfo> lstShipMethod = sqlHandler.ExecuteAsList<ShippingMethodInfo>("[usp_Aspx_GetShippingMethodsOfRealTime]", paramList);
            return lstShipMethod;

        }

        private static WareHouseAddress GetWareHouseAddress(int storeId, int portalId)
        {

            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            WareHouseAddress objWareHouse = sqlHandler.ExecuteAsObject<WareHouseAddress>("[usp_Aspx_GetActiveWareHouse]", paramList);
            return objWareHouse;
        }

        private static List<MethodList> GetAllMethodsFromProvider(int storeId, int portalId)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            List<MethodList> lstMethod = sqlHandler.ExecuteAsList<MethodList>("dbo.usp_Aspx_GetDynamicRateMethodList", paramList);
            return lstMethod;
        }
        private static List<ParamList> GetParamsOfMethod(int id, int storeId, int portalId)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
            paramList.Add(new KeyValuePair<string, object>("@DynamicMethodID", id));
            paramList.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paramList.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            List<ParamList> lstParamList = sqlHandler.ExecuteAsList<ParamList>("dbo.usp_Aspx_GetParamListByID", paramList);
            return lstParamList;
        }
    }
}
