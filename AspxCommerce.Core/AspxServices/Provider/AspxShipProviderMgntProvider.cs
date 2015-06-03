using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RegisterModule;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxShipProviderMgntProvider
    {
        public AspxShipProviderMgntProvider()
        {

        }
        public static List<ShippingProviderNameListInfo> GetShippingProviderNameList(int offset, int limit, AspxCommonInfo aspxCommonObj, string shippingProviderName, System.Nullable<bool> isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", shippingProviderName));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                List<ShippingProviderNameListInfo> lstShipProvider = sqlH.ExecuteAsList<ShippingProviderNameListInfo>("[dbo].[usp_Aspx_GetShippingProviderNameList]", parameter);
                return lstShipProvider;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string LoadProviderSetting(int providerId, AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", providerId));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            SQLHandler sqlH = new SQLHandler();
            var reader = sqlH.ExecuteAsDataReader("[dbo].[usp_Aspx_GetShippingProviderSetting]", parameter);


            var dataQuery = from d in reader.Cast<System.Data.Common.DbDataRecord>()
                            select new
                            {
                                SettingKey = (String)d["SettingKey"],
                                SettingValue = (String)d["SettingValue"]

                            };
            var data = dataQuery.ToDictionary(z => z.SettingKey, z => z.SettingValue);
            string retString = JSONHelper.Serialize<Dictionary<string, string>>(data);
            return retString;
        }


        public static void ShippingProviderAddUpdate(List<ShippingMethod> methods,
            ShippingProvider provider, bool isAddedZip, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                if (!isAddedZip)
                {
                    List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                    parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", provider.ShippingProviderID));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingProviderServiceCode",
                                                                   provider.ShippingProviderServiceCode));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", provider.ShippingProviderName));
                    parameter.Add(new KeyValuePair<string, object>("@ShippingProviderAliasHelp",
                                                                   provider.ShippingProviderAliasHelp));
                    parameter.Add(new KeyValuePair<string, object>("@AssemblyName", null));
                    parameter.Add(new KeyValuePair<string, object>("@Namespace", null));
                    parameter.Add(new KeyValuePair<string, object>("@Class", null));
                    parameter.Add(new KeyValuePair<string, object>("@SettingControlSrc", null));
                    parameter.Add(new KeyValuePair<string, object>("@IsActive", provider.IsActive));
                    parameter.Add(new KeyValuePair<string, object>("@IsAddedZipFlag", false));

                    SQLHandler sqlH = new SQLHandler();
                    int id = sqlH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_ShippingProviderAddUpdate]", parameter);

                    //add providers provided shipping methods/services
                    List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
                    param.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
                    param.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
                    int displayorder = sqlH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_GetDisplayOrderForShippingMethod]", param);

                    if (methods != null)
                    {
                        if (provider.ShippingProviderID != 0)
                        {
                            foreach (ShippingMethod method in methods)
                            {
                                displayorder++;
                                List<KeyValuePair<string, object>> parameterz = new List<KeyValuePair<string, object>>();
                                parameterz.Add(new KeyValuePair<string, object>("@ShippingMethodName",
                                                                       method.ShippingMethodName));
                                parameterz.Add(new KeyValuePair<string, object>("@ShippingMethodCode",
                                                                    method.ShippingMethodCode));
                                parameterz.Add(new KeyValuePair<string, object>("@ImagePath", ""));
                                parameterz.Add(new KeyValuePair<string, object>("@AlternateText",
                                                                 method.AlternateText));
                                parameterz.Add(new KeyValuePair<string, object>("@DisplayOrder", displayorder));
                                parameterz.Add(new KeyValuePair<string, object>("@DeliveryTime",
                                                                 method.DeliveryTime));
                                parameterz.Add(new KeyValuePair<string, object>("@WeightLimitFrom",
                                                                 method.WeightLimitFrom));
                                parameterz.Add(new KeyValuePair<string, object>("@WeightLimitTo",
                                                                   method.WeightLimitTo));
                                parameterz.Add(new KeyValuePair<string, object>("@ShippingProviderID",
                                                                 provider.ShippingProviderID));
                                parameterz.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                                parameterz.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                                parameterz.Add(new KeyValuePair<string, object>("@IsActive", method.IsActive));
                                parameterz.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                                parameterz.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));

                                sqlH.ExecuteNonQuery("usp_Aspx_AddProvidersShippingMethod", parameterz);
                            }
                        }
                    }
                }
                else
                {
                    //add providers provided shipping methods/services
                    int displayorder = 0;
                    if (provider.ShippingProviderID != 0)
                    {
                        foreach (ShippingMethod method in methods)
                        {
                            displayorder++;
                            List<KeyValuePair<string, object>> parameterz = CommonParmBuilder.GetParamSPUC(aspxCommonObj);

                            parameterz.Add(new KeyValuePair<string, object>("@ShippingMethodName",
                                                                  method.ShippingMethodName));
                            parameterz.Add(new KeyValuePair<string, object>("@ShippingMethodCode",
                                                             method.ShippingMethodCode));
                            parameterz.Add(new KeyValuePair<string, object>("@ImagePath", ""));
                            parameterz.Add(new KeyValuePair<string, object>("@AlternateText", method.AlternateText));
                            parameterz.Add(new KeyValuePair<string, object>("@DisplayOrder", displayorder));
                            parameterz.Add(new KeyValuePair<string, object>("@DeliveryTime", method.DeliveryTime));
                            parameterz.Add(new KeyValuePair<string, object>("@WeightLimitFrom", provider.MinWeight));
                            parameterz.Add(new KeyValuePair<string, object>("@WeightLimitTo", provider.MaxWeight));
                            parameterz.Add(new KeyValuePair<string, object>("@ShippingProviderID",
                                                            provider.ShippingProviderID));
                            parameterz.Add(new KeyValuePair<string, object>("@IsActive", method.IsActive));
                            SQLHandler sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_AddProvidersShippingMethod", parameterz);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteShippingProviderByID(int shippingProviderID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderByID]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void DeleteShippingProviderMultipleSelected(string shippingProviderIDs, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ShippingProviderIDs", shippingProviderIDs));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderMultipleSelected]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckShippingProviderUniqueness(AspxCommonInfo aspxCommonObj, int shippingProviderId, string shippingProviderName)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", shippingProviderId));
                Parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", shippingProviderName));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckShippingProviderUniquness]", Parameter, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static void DeactivateShippingMethod(int shippingMethodId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@ShippingMethodId", shippingMethodId));
            paramCol.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            paramCol.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            paramCol.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
            paramCol.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
            SQLHandler sqlHl = new SQLHandler();
            sqlHl.ExecuteNonQuery("[dbo].[usp_Aspx_DeactiveShippingMethod]",
                                                                      paramCol);
        }

        public static List<ProviderShippingMethod> GetProviderRemainingMethod(int shippingProviderId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                paramCol.Add(new KeyValuePair<string, object>("@ShippingProviderId", shippingProviderId));
                paramCol.Add(new KeyValuePair<string, object>("@StoreId", aspxCommonObj.StoreID));
                paramCol.Add(new KeyValuePair<string, object>("@PortalId", aspxCommonObj.PortalID));
                SQLHandler sqlHl = new SQLHandler();
                ShippingProvider providerInfo = sqlHl.ExecuteAsObject<ShippingProvider>("[dbo].[usp_Aspx_GetProviderAssemblyInfo]",
                                                        paramCol);

                var obj1 =
                    (Dictionary<string, string>)
                    DynamicUtility.InvokeMethodAsObject(providerInfo.AssemblyName, providerInfo.ShippingProviderNamespace,
                                                        providerInfo.ShippingProviderClass, "GetAvailableServiceMethod");
                List<ShippingMethodInfo> storeMethod = AspxShipMethodMgntController.GetStoreProvidersAvailableMethod(shippingProviderId, aspxCommonObj);
                var services = obj1.Select(method => new ProviderShippingMethod()
                {
                    ShippingMethodCode = method.Key,
                    ShippingMethodName = method.Value
                }).ToList();

                var filterdmethods = new List<ProviderShippingMethod>();

                foreach (ShippingMethodInfo shippingMethod in storeMethod)
                {
                    foreach (var providerShippingMethod in services)
                    {
                        if (providerShippingMethod.ShippingMethodName == shippingMethod.ShippingMethodName)
                        {
                            services.Remove(providerShippingMethod);
                            break;

                        }
                    }

                }

                return services;
            }
            catch (Exception e)
            {

                throw e;
            }
        }







        public static int SaveUpdateProviderSetting(ShippingProvider provider, string settingKey, string settingValue, AspxCommonInfo commonInfo)
        {
            try
            {
                if (!provider.IsUnique)
                {
                    DeleteShippingProvider(provider.ShippingProviderID, commonInfo);
                }

                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();

                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", 0));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", commonInfo.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));

                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderServiceCode",
                                                               provider.ShippingProviderServiceCode));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", provider.ShippingProviderName));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderAliasHelp",
                                                               provider.ShippingProviderAliasHelp));
                parameter.Add(new KeyValuePair<string, object>("@AssemblyName", provider.AssemblyName));
                parameter.Add(new KeyValuePair<string, object>("@Namespace", provider.ShippingProviderNamespace));
                parameter.Add(new KeyValuePair<string, object>("@Class", provider.ShippingProviderClass));
                parameter.Add(new KeyValuePair<string, object>("@SettingControlSrc", provider.SettingControlPath));
                parameter.Add(new KeyValuePair<string, object>("@LabelControlSrc", provider.LabelControlPath));
                parameter.Add(new KeyValuePair<string, object>("@TrackControlSrc", provider.TrackControlPath));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", provider.IsActive));
                parameter.Add(new KeyValuePair<string, object>("@IsAddedZipFlag", true));

                SQLHandler sqlH = new SQLHandler();
                int providerId = sqlH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_ShippingProviderAddUpdate]", parameter);

                SaveSetting(providerId, settingKey, settingValue, commonInfo);
                SaveDynamicMethods(provider, providerId, commonInfo);
                ExtractFile(provider);

                return providerId;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void DeleteShippingProvider(int providerId, AspxCommonInfo commonInfo)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@StoreID", commonInfo.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", commonInfo.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", providerId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", commonInfo.CultureName));
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderByID]", parameter);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void ExtractFile(ShippingProvider provider)
        {
            //  TempFileName
            string extractedPath = "";
            ZipUtil.UnZipFiles(provider.TempFolderPath + "\\" + provider.TempFileName,
                               HttpContext.Current.Server.MapPath("~/" + provider.ModuleFolder),
                               ref extractedPath, SageFrame.Common.RegisterModule.Common.Password, false);

            FileHelperController fileHelper = new FileHelperController();
            string temp = "";
            temp = provider.TempFolderPath.Substring(provider.TempFolderPath.LastIndexOf("Module"));

            //var thread = new Thread(() => fileHelper.DeleteTempDirectory(temp));
            //thread.Start();

            fileHelper.DeleteTempDirectory(temp);
        }

        private static void SaveDynamicMethods(ShippingProvider provider, int providerId, AspxCommonInfo commonInfo)
        {
            foreach (var dynamicMethod in provider.DynamicMethods)
            {
                try
                {


                    List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
                    paraMeter.Add(new KeyValuePair<string, object>("@ShippingProviderId", providerId));
                    paraMeter.Add(new KeyValuePair<string, object>("@AssemblyName", provider.AssemblyName));
                    paraMeter.Add(new KeyValuePair<string, object>("@ClassName", provider.ShippingProviderClass));
                    paraMeter.Add(new KeyValuePair<string, object>("@NameSpace", provider.ShippingProviderNamespace));
                    paraMeter.Add(new KeyValuePair<string, object>("@MethodName", dynamicMethod.MethodName));
                    paraMeter.Add(new KeyValuePair<string, object>("@MethodType", dynamicMethod.MethodType));
                    paraMeter.Add(new KeyValuePair<string, object>("@ParameterCount",
                                                                   dynamicMethod.DynamicParams.Count()));
                    paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
                    paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.PortalID));
                    paraMeter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));
                    SQLHandler sqLh = new SQLHandler();
                    int dynamicmId = sqLh.ExecuteAsScalar<int>("usp_aspx_AddDynamicMethod", paraMeter);
                    SaveDynamicMethodParams(dynamicMethod.DynamicParams, dynamicmId,commonInfo);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        private static void SaveDynamicMethodParams(List<DynamicParam> pDynamicParams, int dynamiceMId, AspxCommonInfo commonInfo)
        {
            foreach (var pDynamicParam in pDynamicParams)
            {
                List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@DynamicMethodId", dynamiceMId));
                paraMeter.Add(new KeyValuePair<string, object>("@ParameterName", pDynamicParam.ParameterName));
                paraMeter.Add(new KeyValuePair<string, object>("@ParameterOrder", pDynamicParam.ParameterOrder));
                paraMeter.Add(new KeyValuePair<string, object>("@ParameterType", pDynamicParam.ParameterType));
                paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
                paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.PortalID));
                paraMeter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_aspx_AddDynamicMethodParams", paraMeter);
            }
        }

        public static void SaveSetting(int providerId, string settingKey, string settingValue,AspxCommonInfo commonInfo)
        {
            //save usps setting
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@SettingKey", settingKey));
            paraMeter.Add(new KeyValuePair<string, object>("@SettingValue", settingValue));
            paraMeter.Add(new KeyValuePair<string, object>("@ShippingProviderId", providerId));
            paraMeter.Add(new KeyValuePair<string, object>("@IsActive", true));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.PortalID));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
            paraMeter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));
            SQLHandler sqLh = new SQLHandler();
            sqLh.ExecuteNonQuery("[dbo].[usp_Aspx_AddShippingProviderSetting]", paraMeter);
        }

        public static void SaveShippingLabelInfo(ShippingLabelInfo labelInfo, AspxCommonInfo commonInfo)
        {

            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", labelInfo.OrderID));
            paraMeter.Add(new KeyValuePair<string, object>("@ShippingLabelImage", labelInfo.ShippingLabelImage));
            paraMeter.Add(new KeyValuePair<string, object>("@Extention", labelInfo.Extension));
            paraMeter.Add(new KeyValuePair<string, object>("@TrackingNo", labelInfo.TrackingNo));
            paraMeter.Add(new KeyValuePair<string, object>("@BarcodeNo", labelInfo.BarcodeNo));
            paraMeter.Add(new KeyValuePair<string, object>("@IsRealTime", labelInfo.IsRealTime));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.StoreID));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
            paraMeter.Add(new KeyValuePair<string, object>("@UserName", commonInfo.UserName));
            SQLHandler sqLh = new SQLHandler();
            sqLh.ExecuteNonQuery("[dbo].[usp_Aspx_AddOrderShippingLabel]", paraMeter);
        }

        public static bool IsShippingLabelCreated(int orderId, AspxCommonInfo commonInfo)
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.StoreID));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
            SQLHandler sqLh = new SQLHandler();
            bool isCreated = sqLh.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckLabelExist]", paraMeter, "@IsCreated");
            return isCreated;
        }

        public static ShippingLabelInfo GetShippingLabelInfo(int orderId, AspxCommonInfo commonInfo)
        {
            List<KeyValuePair<string, object>> paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalId", commonInfo.StoreID));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreId", commonInfo.StoreID));
            SQLHandler sqLh = new SQLHandler();
            ShippingLabelInfo obj = sqLh.ExecuteAsObject<ShippingLabelInfo>("[dbo].[usp_Aspx_GetShippingLabelInfo]", paraMeter);
            return obj;
        }
    }
}
