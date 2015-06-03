using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxKitProvider
    {
        public List<KitInfo> GetKits(int offset, int limit, string kitName, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(commonInfo);
                ParaMeter.Add(new KeyValuePair<string, object>("@offset", offset));
                ParaMeter.Add(new KeyValuePair<string, object>("@limit", limit));
                ParaMeter.Add(new KeyValuePair<string, object>("@kitName", kitName));
                SQLHandler sqLH = new SQLHandler();
                List<KitInfo> kits = sqLH.ExecuteAsList<KitInfo>("[dbo].[usp_Aspx_GetKitsForGrid]", ParaMeter);
                return kits;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<ItemKit> GetItemKits(int itemID, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(commonInfo);
                ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqLH = new SQLHandler();
                List<ItemKit> itemKits = sqLH.ExecuteAsList<ItemKit>("[dbo].[usp_Aspx_GetItemKits]", ParaMeter);
                return itemKits;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void SaveItemKits(List<ItemKit> mappedKits, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                foreach (var kit in mappedKits)
                {

                    KitComponent obj = new KitComponent() { KitComponentID = kit.KitComponentID, KitComponentName = kit.KitComponentName, KitComponentType = kit.KitComponentType };
                    kit.KitComponentID = SaveComponent(obj, commonInfo);

                    SaveItemKitConfig(kit, itemID, commonInfo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SaveItemKitConfig(ItemKit kitconfig, int itemId, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("@KitComponentID", kitconfig.KitComponentID));
                parameter.Add(new KeyValuePair<string, object>("@KitComponentOrder", kitconfig.KitComponentOrder));
                parameter.Add(new KeyValuePair<string, object>("@Price", kitconfig.Price));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", kitconfig.Quantity));
                parameter.Add(new KeyValuePair<string, object>("@Weight", kitconfig.Weight));
                parameter.Add(new KeyValuePair<string, object>("@IsDefault", kitconfig.IsDefault));
                parameter.Add(new KeyValuePair<string, object>("@KitID", kitconfig.KitID));
                parameter.Add(new KeyValuePair<string, object>("@KitOrder", kitconfig.KitOrder));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("[dbo].[usp_Aspx_MapKitToItem]", parameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void DeleteItemKits(int kitComponentID, int kitID, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("@KitComponentID", kitComponentID));
                parameter.Add(new KeyValuePair<string, object>("@KitID", kitID));
                parameter.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                SQLHandler sqLH = new SQLHandler();
                sqLH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteKitFromItem]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int SaveComponent(KitComponent kitcomponent, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                parameter.Add(new KeyValuePair<string, object>("@KitComponentID", kitcomponent.KitComponentID));
                parameter.Add(new KeyValuePair<string, object>("@ComponentName", kitcomponent.KitComponentName));
                parameter.Add(new KeyValuePair<string, object>("@KitComponentType", kitcomponent.KitComponentType));
                SQLHandler sqLh = new SQLHandler();
                int kitComponentID = sqLh.ExecuteAsScalar<int>("[dbo].[usp_Aspx_AddUpdateKitComponent]", parameter);
                return kitComponentID;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public int SaveKit(Kit kit, AspxCommonInfo commonInfo)
        {

            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("@KitID", kit.KitID));
                parameter.Add(new KeyValuePair<string, object>("@KitName", kit.KitName));
                parameter.Add(new KeyValuePair<string, object>("@Price", kit.Price));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", kit.Quantity));
                parameter.Add(new KeyValuePair<string, object>("@Weight", kit.Weight));
                parameter.Add(new KeyValuePair<string, object>("@KitComponentID", kit.KitComponentID));

                SQLHandler sqLh = new SQLHandler();
                int kitComponentID = sqLh.ExecuteAsScalar<int>("[dbo].[usp_Aspx_AddUpdateKit]", parameter);
                return kitComponentID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public List<KitComponent> GetComponents(AspxCommonInfo commonInfo)
        {
            try
            {


                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(commonInfo);
                SQLHandler sqLh = new SQLHandler();
                return sqLh.ExecuteAsList<KitComponent>("[dbo].[usp_Aspx_GetKitComponents]", parameter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Kit> GetKits(AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(commonInfo);
                SQLHandler sqLh = new SQLHandler();
                return sqLh.ExecuteAsList<Kit>("[dbo].[usp_Aspx_GetKits]", parameter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckKitComponentExist(string ComponentName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@ComponentName", ComponentName));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_CheckKitComponentExist", parameterCollection, "@IsComponentExist");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void DeleteKit(string kitIds, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("@KitIDs", kitIds));

                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteKit]", parameter);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void DeleteKitComponent(string kitComponentIds, AspxCommonInfo commonInfo)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(commonInfo);
                //  parameter.Add(new KeyValuePair<string, object>("@ID", kit.ID));
                parameter.Add(new KeyValuePair<string, object>("@KitComponentIDs", kitComponentIds));

                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteKitComponent]", parameter);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }






    }
}
