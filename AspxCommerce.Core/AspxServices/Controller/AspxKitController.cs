using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class AspxKitController
    {
        public bool CheckKitComponentExist(string ComponentName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxKitProvider provider = new AspxKitProvider();
                bool isUnique = provider.CheckKitComponentExist(ComponentName, aspxCommonObj);
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

                AspxKitProvider provider = new AspxKitProvider();
                provider.DeleteKit(kitIds, commonInfo);

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
                AspxKitProvider provider = new AspxKitProvider();
                provider.DeleteKitComponent(kitComponentIds, commonInfo);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ItemKit> GetItemKits(int itemID, AspxCommonInfo commonInfo)
        {

            try
            {
                AspxKitProvider provider = new AspxKitProvider();
                return provider.GetItemKits(itemID, commonInfo);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void SaveKits(KitConfiguration configuration, int itemID, AspxCommonInfo commonInfo)
        {
            DeleteItemKits(configuration.KitDeleted, itemID, commonInfo);
           
            SaveItemKits(configuration.KitConfig, itemID, commonInfo);

        }
        public void DeleteItemKits(List<ItemKit> mappedKits, int itemID, AspxCommonInfo commonInfo) {
            try
            {
                foreach (var kit in mappedKits)
                {
                  AspxKitProvider provider = new AspxKitProvider();
                  provider.DeleteItemKits(kit.KitComponentID, kit.KitID, itemID, commonInfo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SaveItemKits(List<ItemKit> mappedKits, int itemID, AspxCommonInfo commonInfo)
        {
            try
            {
                AspxKitProvider provider = new AspxKitProvider();
                provider.SaveItemKits(mappedKits, itemID, commonInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int SaveComponent(KitComponent kitcomponent, AspxCommonInfo commonInfo)
        {

            try
            {
                AspxKitProvider provider = new AspxKitProvider();
                return provider.SaveComponent(kitcomponent, commonInfo);
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
                AspxKitProvider provider = new AspxKitProvider();
                return provider.SaveKit(kit, commonInfo);
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
                AspxKitProvider provider = new AspxKitProvider();
                return provider.GetComponents(commonInfo);
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
                AspxKitProvider provider = new AspxKitProvider();
                return provider.GetKits(commonInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<KitInfo> LoadKits(int offset, int limit, string kitname, AspxCommonInfo commonInfo)
        {
            try
            {

                AspxKitProvider provider = new AspxKitProvider();
                return provider.GetKits(offset, limit, kitname, commonInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
