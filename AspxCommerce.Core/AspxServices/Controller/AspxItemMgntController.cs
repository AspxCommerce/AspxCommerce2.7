using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.Core
{
    public class AspxItemMgntController
    {

        public AspxItemMgntController()
        {
        }
        public static List<ItemCommonInfo> GetItemsListByCategory(AspxCommonInfo aspxCommonObj, int categoryID)
        {
            try
            {
                List<ItemCommonInfo> itemsList = AspxItemMgntProvider.GetItemsListByCategory(aspxCommonObj, categoryID);
                return itemsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // ---For Gift Items List In Front End--- 
        public static List<LatestItemsInfo> GetAllGiftCards(int offset, int limit, int rowTotal, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<LatestItemsInfo> lstGiftItems = AspxItemMgntProvider.GetAllGiftCards(offset, limit, rowTotal, aspxCommonObj);
                return lstGiftItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To Bind grid with all Items
        /// </summary>
        public static List<ItemsInfo> GetAllItems(int offset, int limit, GetItemListInfo getItemObj, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            ml = AspxItemMgntProvider.GetAllItems(offset, limit, getItemObj, aspxCommonObj);
            return ml;
        }
        /// <summary>
        /// To Bind grid with all Related Items
        /// </summary>

        public static List<ItemsInfo> GetRelatedItemsByItemID(int offset, int limit, ItemDetailsCommonInfo IDCommonObj, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            ml = AspxItemMgntProvider.GetRelatedItemsByItemID(offset, limit, IDCommonObj, aspxCommonObj);
            return ml;
        }
        
        /// <summary>
        /// Get associated Items for group products
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="IDCommonObj"></param>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<ItemsInfo> GetAssociatedItemsByItemID(int offset, int limit, ItemDetailsCommonInfo IDCommonObj,int categoryID, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> itemsList;
            itemsList = AspxItemMgntProvider.GetAssociatedItemsByItemID(offset, limit, IDCommonObj,categoryID, aspxCommonObj);
            return itemsList;
        }

        /// <summary>
        /// To Bind grid with all UP Sell Items
        /// </summary>

        public static List<ItemsInfo> GetUpSellItemsByItemID(int offset, int limit, ItemDetailsCommonInfo UpSellCommonObj, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            ml = AspxItemMgntProvider.GetUpSellItemsByItemID(offset, limit, UpSellCommonObj, aspxCommonObj);
            return ml;
        }

        /// <summary>
        /// To Bind grid with all Cross Sell Items
        /// </summary>

        public static List<ItemsInfo> GetCrossSellItemsByItemID(int offset, int limit, ItemDetailsCommonInfo CrossSellCommonObj, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            ml = AspxItemMgntProvider.GetCrossSellItemsByItemID(offset, limit, CrossSellCommonObj, aspxCommonObj);
            return ml;
        }

        public static string GetAssociatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            string idsList;
            idsList = AspxItemMgntProvider.GetAssociatedCheckIDs(ItemID, aspxCommonObj);
            return idsList;
        }
        
        public static string GetRelatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            string ml;
            ml = AspxItemMgntProvider.GetRelatedCheckIDs(ItemID, aspxCommonObj);
            return ml;
        }
        public static string GetUpSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            string ml;
            ml = AspxItemMgntProvider.GetUpSellCheckIDs(ItemID, aspxCommonObj);
            return ml;
        }
        public static string GetCrossSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            string ml;
            ml = AspxItemMgntProvider.GetCrossSellCheckIDs(ItemID, aspxCommonObj);
            return ml;
        }

        /// <summary>
        /// To Delete Multiple Item IDs
        /// </summary>
        public static void DeleteMultipleItems(string itemIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.DeleteMultipleItems(itemIds, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Delete Single Item ID
        /// </summary>

        public static void DeleteSingleItem(string itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.DeleteSingleItem(itemId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            formAttributeList = AspxItemMgntProvider.GetItemFormAttributes(attributeSetID, itemTypeID, aspxCommonObj);
            return formAttributeList;
        }

        public static List<AttributeFormInfo> GetItemFormAttributesByItemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            formAttributeList = AspxItemMgntProvider.GetItemFormAttributesByItemSKUOnly(itemSKU, aspxCommonObj);
            return formAttributeList;
        }

        public static List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
        {
            List<TaxRulesInfo> lstTaxManageRule;
            lstTaxManageRule = AspxItemMgntProvider.GetAllTaxRules(storeID, portalID, isActive);
            return lstTaxManageRule;
        }

        /// <summary>
        /// make the Item active deactive
        /// </summary>
        public static void UpdateItemIsActive(int itemId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                AspxItemMgntProvider.UpdateItemIsActive(itemId, aspxCommonObj, isActive);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AttributeFormInfo> GetItemAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                itemAttributes = AspxItemMgntProvider.GetItemAttributesValuesByItemID(itemID, attributeSetID, itemTypeID, aspxCommonObj);
                return itemAttributes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryInfo> GetCategoryList(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int itemId, bool serviceBit)
        {
            List<CategoryInfo> catList;
            catList = AspxItemMgntProvider.GetCategoryList(prefix, isActive, aspxCommonObj, itemId, serviceBit);
            return catList;
        }

        public static bool CheckIsItemInGroupItem(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxItemMgntProvider.CheckIsItemInGroupItem(ItemID, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniqueSKUCode(string sku, int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                bool isUnique = AspxItemMgntProvider.CheckUniqueSKUCode(sku, itemId, aspxCommonObj);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteItemImageByItemID(int itemId)
        {
            try
            {
                AspxItemMgntProvider.DeleteItemImageByItemID(itemId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static List<AttributeFormInfo> GetItemDetailsInfoByItemSKU(string itemSKU, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                itemAttributes = AspxItemMgntProvider.GetItemDetailsInfoByItemSKU(itemSKU, attributeSetID, itemTypeID, aspxCommonObj);
                return itemAttributes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        public static ItemBasicDetailsInfo GetItemBasicInfo(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemBasicDetailsInfo itemBasicDetails;
                itemBasicDetails = AspxItemMgntProvider.GetItemBasicInfo(itemSKU, aspxCommonObj);
                return itemBasicDetails;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet GetItemDetailsInfo(AspxCommonInfo aspxCommonObj, string itemSKU, string userIP, string countryName)
        {

            return AspxItemMgntProvider.GetItemDetailsInfo(aspxCommonObj, itemSKU, userIP, countryName);

        }

        public static string GetItemVideos(string SKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {               
                string itemVideo = AspxItemMgntProvider.GetItemVideos(SKU, aspxCommonObj);
                return itemVideo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static List<ItemBasicDetailsInfo> GetGroupedItemsByGroupSKU(string groupSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemBasicDetailsInfo> groupedItems;
                groupedItems = AspxItemMgntProvider.GetGroupedItemsByGroupSKU(groupSKU, aspxCommonObj);
                return groupedItems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static decimal GetStartPriceOfGroupAfterDeletion(string groupItems, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                decimal startPrice = AspxItemMgntProvider.GetStartPriceOfGroupAfterDeletion(groupItems, aspxCommonObj);
                return startPrice;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddUpdateRecentlyViewedItems(RecentlyAddedItemInfo addUpdateRecentObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.AddUpdateRecentlyViewedItems(addUpdateRecentObj, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CostVariantInfo> GetAllCostVariantOptions(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantInfo> lstCostVar = AspxItemMgntProvider.GetAllCostVariantOptions(itemId, aspxCommonObj);
                return lstCostVar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------------ delete Item Cost Variants management------------------------    
        public static void DeleteSingleItemCostVariant(string itemCostVariantID, int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.DeleteSingleItemCostVariant(itemCostVariantID, itemId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //--------------------------GetMostViewedItems---------------------------
        public static List<MostViewedItemsInfo> GetAllMostViewedItems(int offset, int? limit, string name,string currencySymbol, AspxCommonInfo aspxCommonObj)
        {
            List<MostViewedItemsInfo> ml;
            ml = AspxItemMgntProvider.GetAllMostViewedItems(offset, limit, name,currencySymbol, aspxCommonObj);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get Low Stock Items----------------------------------------
        public static List<LowStockItemsInfo> GetAllLowStockItems(int offset, int? limit, ItemSmallCommonInfo lowStockObj, AspxCommonInfo aspxCommonObj, int lowStock)
        {
            List<LowStockItemsInfo> ml;
            ml = AspxItemMgntProvider.GetAllLowStockItems(offset, limit, lowStockObj, aspxCommonObj, lowStock);
            return ml;
        }
        //-------------------------------------------------------------------------------------------------
        //--------------------------Get Ordered Items List------------------------------------------------
        public static List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, AspxCommonInfo aspxCommonObj)
        {
            List<OrderItemsGroupByItemIDInfo> ml;
            ml = AspxItemMgntProvider.GetOrderedItemsList(offset, limit, name, aspxCommonObj);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get DownLoadable Items----------------------------------------
        public static List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, System.Nullable<int> limit, GetDownloadableItemInfo downloadableObj, AspxCommonInfo aspxCommonObj)
        {
            List<DownLoadableItemGetInfo> ml;
            ml = AspxItemMgntProvider.GetDownLoadableItemsList(offset, limit, downloadableObj, aspxCommonObj);
            return ml;
        }

        #region NeW CostVariant Combination

        public static List<VariantCombination> GetCostVariantCombinationbyItemSku(string itemSku, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<VariantCombination> lstVarCom = AspxItemMgntProvider.GetCostVariantCombinationbyItemSku(itemSku, aspxCommonObj);
                return lstVarCom;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ItemCostVariantsInfo> GetCostVariantsByItemSKU(string itemSku, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemCostVariantsInfo> lstItemCostVar = AspxItemMgntProvider.GetCostVariantsByItemSKU(itemSku, aspxCommonObj);
                return lstItemCostVar;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CostVariantInfo> GetCostVariantForItem(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantInfo> lstCostVar = AspxItemMgntProvider.GetCostVariantForItem(aspxCommonObj);
                return lstCostVar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CostVariantValuesInfo> GetCostVariantValues(int costVariantID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CostVariantValuesInfo> lstCostVarValue = AspxItemMgntProvider.GetCostVariantValues(costVariantID, aspxCommonObj);
                return lstCostVarValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteCostVariantForItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.DeleteCostVariantForItem(itemId, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<VariantCombination> GetCostVariantsOfItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<VariantCombination> lstVarComb = AspxItemMgntProvider.GetCostVariantsOfItem(itemId, aspxCommonObj);
                return lstVarComb;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SaveAndUpdateItemCostVariantCombination(CostVariantsCombination itemCostVariants, AspxCommonInfo aspxCommonObj, string cvCombinations)
        {
            try
            {
                AspxItemMgntProvider.SaveAndUpdateItemCostVariantCombination(itemCostVariants, aspxCommonObj, cvCombinations);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        public static List<TaxItemClassInfo> GetAllTaxItemClass(AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                List<TaxItemClassInfo> lstTaxItemClass = AspxItemMgntProvider.GetAllTaxItemClass(aspxCommonObj, isActive);
                return lstTaxItemClass;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int SaveUpdateItemAndAttributes(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj)
        {
            bool isModified = false;
            bool updateFlag = false;
            int storeId = aspxCommonObj.StoreID;
            int portalId = aspxCommonObj.PortalID;
            string culture = aspxCommonObj.CultureName;
            string userName = aspxCommonObj.UserName;
            if (itemObj.ItemId > 0)
            {
                isModified = true;
                updateFlag = true;
            }
            StoreSettingConfig ssc = new StoreSettingConfig();
            int itemLargeThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageHeight, storeId, portalId,
                                                          culture));
            int itemLargeThumbNailWidth =
               Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemLargeThumbnailImageWidth, storeId, portalId,
                                                         culture));
            int itemMediumThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageHeight, storeId, portalId,
                                                          culture));
            int itemMediumThumbNailWidth =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemMediumThumbnailImageWidth, storeId, portalId,
                                                          culture));
            int itemSmallThumbNailHeight =
                Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageHeight, storeId, portalId,
                                                          culture));
            int itemSmallThumbNailWidth =
               Convert.ToInt32(ssc.GetStoreSettingsByKey(StoreSetting.ItemSmallThumbnailImageWidth, storeId, portalId,
                                                         culture));
            int _attributeID = 0;
            int _inputTypeID = 0;
            int _ValidationTypeID = 8;
            int _GroupID = 0;
            bool _isIncludeInPriceRule = false;
            int _displayOrder = 0;

            bool toInsertIntoDB = true;
            bool isFormValid = true;
            string imageVar = string.Empty;
            int imageCounterFirst = 0;
            var formVars = JSONHelper.Deserialise<List<AspxNameValue>>(itemObj.FormVars);
            ItemInformationDetailsInfo itemInfo = new ItemInformationDetailsInfo();
            ItemSetting itemSettings = new ItemSetting();
            itemSettings = itemObj.Settings;
            itemInfo.NewFromDate = "1900/01/01";
            itemInfo.NewToDate = "2999/12/30";
            itemInfo.FeaturedFrom = "1900/01/01";
            itemInfo.FeaturedTo = "2999/12/30";
            itemInfo.SpecialFrom = "1900/01/01";
            itemInfo.SpecialTo = "2999/12/30";
            itemInfo.SpecialPriceFrom = "1900/01/01";
            itemInfo.SpecialPriceTo = "2999/12/30";
            itemInfo.IsManageInventory = itemSettings.IsManageInventory;
            itemInfo.IsUsedStoreSetting = itemSettings.IsUsedStoreSetting;
            itemInfo.MinCartQuantity = itemSettings.MinCartQuantity;
            itemInfo.MaxCartQuantity = itemSettings.MaxCartQuantity;
            itemInfo.LowStockQuantity = itemSettings.LowStockQuantity;
            itemInfo.OutOfStockQuantity = itemSettings.OutOfStockQuantity;

            List<ItemAttributeDetailsInfo> listItemAttributes = new List<ItemAttributeDetailsInfo>();
            bool hasSystemAttributesOnly = true;
            string attributeIDs = "1,2,3,4,5,6,7,8,9,10,11,13,14,15,19,20,23,24,25,26,27,28,29,30,31,32,33,34,44,45,46,47,48";
            for (int i = 0; i < formVars.Count; i++)
            {
                string attribValue = formVars[i].value;
                //string jsonResult = formVars[i].name.Replace('%', '_');
                string jsonResult = formVars[i].name.Replace('-', ' ');
                string[] jsonVar = jsonResult.Split('_');
                FormValidation formValidation = new FormValidation();
                ItemAttributeDetailsInfo itemAttribute = new ItemAttributeDetailsInfo();

                if (jsonVar.Length > 1)
                {
                    _attributeID = Int32.Parse(jsonVar[0]);
                    _inputTypeID = Int32.Parse(jsonVar[1]);
                    _ValidationTypeID = Int32.Parse(jsonVar[2]);
                    _GroupID = Int32.Parse(jsonVar[4]);
                    _isIncludeInPriceRule = bool.Parse(jsonVar[5]);
                    _displayOrder = Int32.Parse(jsonVar[6]);
                    itemAttribute.AttributeID = _attributeID;
                    itemAttribute.InputTypeID = _inputTypeID;
                    itemAttribute.ValidationTypeID = _ValidationTypeID;
                    itemAttribute.GroupID = _GroupID;
                    itemAttribute.IsIncludeInPriceRule = _isIncludeInPriceRule;
                    itemAttribute.DisplayOrder = _displayOrder;
                    if (_attributeID > 48)
                    {
                        if (hasSystemAttributesOnly)
                        {
                            hasSystemAttributesOnly = false;
                        }
                    }

                    if (_attributeID == 4)
                    {
                        itemInfo.SKU = formVars[i].value;
                    }
                    else if (_attributeID == 19)
                    {
                        itemInfo.ActiveFrom = formVars[i].value;
                    }
                    else if (_attributeID == 20)
                    {
                        itemInfo.ActiveTo = formVars[i].value;
                    }
                    else if (_attributeID == 23)
                    {
                        itemInfo.HidePrice = Convert.ToBoolean(formVars[i].value);
                    }
                    else if (_attributeID == 24)
                    {
                        itemInfo.HideInRSSFeed = Convert.ToBoolean(formVars[i].value);
                    }
                    else if (_attributeID == 25)
                    {
                        itemInfo.HideToAnonymous = Convert.ToBoolean(formVars[i].value);
                    }
                    else if (_attributeID == 1)
                    {
                        itemInfo.Name = formVars[i].value;
                    }
                    else if (_attributeID == 2)
                    {
                        itemInfo.Description = formVars[i].value;
                    }
                    else if (_attributeID == 3)
                    {
                        itemInfo.ShortDescription = formVars[i].value;
                    }
                    else if (_attributeID == 5)
                    {
                        itemInfo.Weight = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 6)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.NewFromDate =itemInfo.NewFromDate;
                        }
                        else
                        {
                            itemInfo.NewFromDate = formVars[i].value;
                        }
                    }
                    else if (_attributeID == 7)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.NewToDate = itemInfo.NewToDate;
                        }
                        else
                        {
                            itemInfo.NewToDate = formVars[i].value;
                        }
                    }
                    else if (_attributeID == 8)
                    {
                        itemInfo.Price = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 9)
                    {
                        itemInfo.MetaTitle = formVars[i].value;
                    }
                    else if (_attributeID == 10)
                    {
                        itemInfo.MetaKeyword = formVars[i].value;
                    }
                    else if (_attributeID == 11)
                    {
                        itemInfo.MetaDescription = formVars[i].value;
                    }
                    else if (_attributeID == 13 && formVars[i].value != "")
                    {
                        itemInfo.ListPrice = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 14)
                    {
                        itemInfo.VisibilityOptionValueID = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 15)
                    {
                        itemInfo.Quantity = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 26)
                    {
                        itemInfo.IsFeaturedOptionValueID = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 27)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.FeaturedFrom = itemInfo.FeaturedFrom;
                        }
                        else
                        {
                            itemInfo.FeaturedFrom = formVars[i].value;
                        }                      
                    }
                    else if (_attributeID == 28)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.FeaturedTo = itemInfo.FeaturedTo;
                        }
                        else
                        {
                            itemInfo.FeaturedTo = formVars[i].value;
                        }                        
                    }
                    else if (_attributeID == 29)
                    {
                        itemInfo.IsSpecialOptionValueID = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 30)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.SpecialFrom = itemInfo.SpecialFrom;
                        }
                        else
                        {
                            itemInfo.SpecialFrom = formVars[i].value;
                        }                     
                    }
                    else if (_attributeID == 31)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.SpecialTo = itemInfo.SpecialTo;
                        }
                        else
                        {
                            itemInfo.SpecialTo = formVars[i].value;
                        }                            
                    }
                    else if (_attributeID == 32 && formVars[i].value != "")
                    {
                        itemInfo.Length = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 33 && formVars[i].value != "")
                    {
                        itemInfo.Height = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 34 && formVars[i].value != "")
                    {
                        itemInfo.Width = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 35)
                    {
                        itemInfo.IsPromo = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 37)
                    {
                        itemInfo.ServiceDuration = Convert.ToInt32(formVars[i].value);
                    }
                    else if (_attributeID == 44 && formVars[i].value != "")
                    {
                        itemInfo.CostPrice = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 45 && formVars[i].value != "")
                    {
                        itemInfo.SpecialPrice = Convert.ToDecimal(formVars[i].value);
                    }
                    else if (_attributeID == 46)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.SpecialPriceFrom = itemInfo.SpecialPriceFrom;
                        }
                        else
                        {
                            itemInfo.SpecialPriceFrom = formVars[i].value;
                        }                         
                    }
                    else if (_attributeID == 47)
                    {
                        if (formVars[i].value == "" || formVars[i].value == null)
                        {
                            itemInfo.SpecialPriceTo = itemInfo.SpecialPriceTo;
                        }
                        else
                        {
                            itemInfo.SpecialPriceTo = formVars[i].value;
                        }                         
                    }
                    else if (_attributeID == 48 && formVars[i].value != "")
                    {
                        itemInfo.ManufacturerPrice = Convert.ToDecimal(formVars[i].value);
                    }

                }

                if (_attributeID == 37)
                {
                    if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                    {
                        itemAttribute.IntValue = int.Parse(formVars[i].value);
                        listItemAttributes.Add(itemAttribute);
                    }
                    else
                    {
                        isFormValid = false;
                        break;
                    }
                }
                else if (_attributeID == 35)
                {
                    if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                    {
                        itemAttribute.OptionValues = formVars[i].value;
                        listItemAttributes.Add(itemAttribute);
                    }
                    else
                    {
                        isFormValid = false;
                        break;
                    }
                }

                if (_attributeID > 48)
                {
                    if (_inputTypeID == 1)
                    {
                        if (_ValidationTypeID == 3)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                itemAttribute.DecimalValue = decimal.Parse(formVars[i].value);
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else if (_ValidationTypeID == 5)
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                itemAttribute.IntValue = int.Parse(formVars[i].value);
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                        else
                        {
                            if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                            {
                                itemAttribute.NvarcharValue = formVars[i].value;
                            }
                            else
                            {
                                isFormValid = false;
                                break;
                            }
                        }
                    }
                    else if (_inputTypeID == 2)
                    {
                        if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                        {
                            itemAttribute.TextValue = formVars[i].value;
                        }
                        else
                        {
                            isFormValid = false;
                            break;
                        }
                    }
                    else if (_inputTypeID == 3)
                    {
                        if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                        {
                            if (!string.IsNullOrEmpty(formVars[i].value))
                            {
                                itemAttribute.DateValue = DateTime.Parse(formVars[i].value);
                            }
                        }
                        else
                        {
                            isFormValid = false;
                            break;
                        }
                    }
                    else if (_inputTypeID == 4)
                    {
                        if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                        {
                            if (!string.IsNullOrEmpty(attribValue))
                            {
                                itemAttribute.BooleanValue = (formVars[i].value == "1" ||
                                                              formVars[i].value.ToLower() == "true")
                                                                 ? true
                                                                 : false;
                            }
                            else
                            {
                                itemAttribute.BooleanValue = false;
                            }
                        }
                        else
                        {
                            isFormValid = false;
                            break;
                        }
                    }
                    else if (_inputTypeID == 5 || _inputTypeID == 6 || _inputTypeID == 9 || _inputTypeID == 10 ||
                             _inputTypeID == 11 || _inputTypeID == 12)
                    {
                        if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                        {
                            itemAttribute.OptionValues = formVars[i].value;
                        }
                        else
                        {
                            isFormValid = false;
                            break;
                        }
                    }
                    else if (_inputTypeID == 7)
                    {
                        if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID, formVars[i].value))
                        {
                            itemAttribute.DecimalValue = decimal.Parse(formVars[i].value);
                        }
                        else
                        {
                            isFormValid = false;
                            break;
                        }
                    }

                    else if (_inputTypeID == 8)
                    {
                        if (imageCounterFirst % 2 == 0)
                        {
                            toInsertIntoDB = false;
                            if (!string.IsNullOrEmpty(formVars[i].value) &&
                                formVars[i].value.ToLower() != "undefined")
                            {
                                if (formValidation.ValidateValue(formVars[i].name, _ValidationTypeID,
                                                                 formVars[i].value))
                                {
                                    imageVar = attribValue;
                                }
                                else
                                {
                                    isFormValid = false;
                                    break;
                                }
                            }
                            else
                            {
                                imageVar = "";
                            }
                        }
                        else
                        {
                            toInsertIntoDB = true;

                            if (attribValue != imageVar)
                            {
                                //_imageVar = _imageVar.Replace("../", "");
                                imageVar = imageVar.Replace("/", "\\");
                                //attribValue = attribValue.Replace("../", "");
                                attribValue = attribValue.Replace("/", "\\");

                                string tempFolder = @"Upload\temp";
                                FileHelperController fileObj = new FileHelperController();
                                attribValue = fileObj.MoveFileToModuleFolder(tempFolder, attribValue, imageVar,
                                                                             itemLargeThumbNailHeight,
                                                                             itemLargeThumbNailWidth,
                                                                             itemMediumThumbNailHeight,
                                                                             itemMediumThumbNailWidth,
                                                                             itemSmallThumbNailHeight,
                                                                             itemSmallThumbNailWidth,
                                                                             @"Modules\AspxCommerce\AspxItemsManagement\uploads\",
                                                                             itemObj.ItemId, "item_");
                                itemAttribute.FileValue =
                                    attribValue.Replace(@"Modules\AspxCommerce\AspxItemsManagement\uploads\", " ");
                            }
                            else
                            {
                                itemAttribute.FileValue =
                                    attribValue.Replace(@"Modules\AspxCommerce\AspxItemsManagement\uploads\", " ");
                            }
                        }
                        imageCounterFirst++;
                    }
                    if (toInsertIntoDB)
                    {
                        listItemAttributes.Add(itemAttribute);
                    }
                }
            }
            if (isFormValid)
            {
                string groupPrices = GetItemGroupPriceInString(itemObj.GroupPrice, itemObj.ItemId);
                itemObj.ItemId = AspxItemMgntProvider.SaveUpdateItemAttributes(itemObj, aspxCommonObj, true,
                                                                               isModified, itemInfo,
                                                                               listItemAttributes, attributeIDs,
                                                                               hasSystemAttributesOnly, updateFlag, groupPrices);
            }
            else
            {
                throw new Exception("Form is not valid one");
            }

            return itemObj.ItemId;
        }
        public static string GetItemGroupPriceInString(List<ItemPriceGroupInfo> priceGroupObj, int itemID)
        {
            //delete items previus group price
            string groupPrices = string.Empty;
            foreach (var item in priceGroupObj)
            {
                groupPrices += item.GroupID + "#" + item.Price + ",";
            }
            if (groupPrices != "")
            {
                groupPrices = groupPrices.Substring(0, groupPrices.Length - 1);
            }
            return groupPrices;
        }

        public static List<UserInRoleInfo> BindRoles(bool isAll, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<UserInRoleInfo> lstUserInRole = AspxItemMgntProvider.BindRoles(isAll, aspxCommonObj);
                return lstUserInRole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ItemSEOInfo GetSEOSettingsBySKU(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            ItemSEOInfo objItemSEO = new ItemSEOInfo();
            objItemSEO = AspxItemMgntProvider.GetSEOSettingsBySKU(itemSKU, aspxCommonObj);
            return objItemSEO;
        }

        #region Downloadable Item Details
        public static List<DownLoadableItemInfo> GetDownloadableItem(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<DownLoadableItemInfo> lstDownItem = AspxItemMgntProvider.GetDownloadableItem(itemId, aspxCommonObj);
                return lstDownItem;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region "For Item Videos"
        public static List<ItemsInfo.ItemSaveBasicInfo> GetItemVideoContents(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemsInfo.ItemSaveBasicInfo> lstItemVideo = AspxItemMgntProvider.GetItemVideoContents(ItemID, aspxCommonObj);
                return lstItemVideo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region "For Item Settings"
        public static ItemSetting GetItemSetting(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemSetting lstItemSetting = AspxItemMgntProvider.GetItemSetting(ItemID, aspxCommonObj);
                return lstItemSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region "For Item Settings"
        public static List<ItemPriceGroupInfo> GetItemGroupPrices(int ItemID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ItemPriceGroupInfo> lstGroupPrice = AspxItemMgntProvider.GetItemGroupPrices(ItemID, aspxCommonObj);
                return lstGroupPrice;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static ItemTabSettingInfo ItemTabSettingGet(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemTabSettingInfo lstItem = AspxItemMgntProvider.ItemTabSettingGet(aspxCommonObj);
                return lstItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ItemTabSettingSave(string SettingKeys, string SettingValues, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxItemMgntProvider.ItemTabSettingSave(SettingKeys, SettingValues, aspxCommonObj);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
