/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;

using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class ItemsManagementSqlProvider
    {
        public ItemsManagementSqlProvider()
        {
        }

        /// <summary>
        /// To Bind grid with all Items
        /// </summary>
        public List<ItemsInfo> GetAllItems(int offset, int limit, GetItemListInfo getItemObj, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", getItemObj.SKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", getItemObj.ItemName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", getItemObj.ItemTypeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", getItemObj.AttributeSetID));
            parameterCollection.Add(new KeyValuePair<string, object>("@Visibility", getItemObj.Visibility));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", getItemObj.IsActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_ItemsGetAll", parameterCollection);
            return ml;
        }
        /// <summary>
        /// To Bind grid with all Related Items
        /// </summary>

        public List<ItemsInfo> GetRelatedItemsByItemID(int offset, int limit, int selfItemId, bool serviceBit, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID",aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID",aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName",aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName",aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", serviceBit));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetRelatedItemsByItemID", parameterCollection);
            return ml;
        }
        /// <summary>
        /// To Bind grid with all UP Sell Items
        /// </summary>

        public List<ItemsInfo> GetUpSellItemsByItemID(int offset, int limit, int selfItemId, bool serviceBit, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", serviceBit));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetUpSellItemsByItemID", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Bind grid with all Cross Sell Items
        /// </summary>

        public List<ItemsInfo> GetCrossSellItemsByItemID(int offset, int limit, int selfItemId, bool serviceBit, AspxCommonInfo aspxCommonObj)
        {
            List<ItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", selfItemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", serviceBit));
            ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetCrossSellItemsByItemID", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Delete Multiple Item IDs
        /// </summary>
        public void DeleteMultipleItems(string itemIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemsDeleteMultipleSelected", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Delete Single Item ID
        /// </summary>
     
        public void DeleteSingleItem(string itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemByItemID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributes", parameterCollection);
            return formAttributeList;
        }

        public List<AttributeFormInfo> GetItemFormAttributesByItemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeFormInfo> formAttributeList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesByItemSKU", parameterCollection);
            return formAttributeList;
        }

        public List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
        {
            List<TaxRulesInfo> lstTaxManageRule;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();

            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));

            lstTaxManageRule = sqlH.ExecuteAsList<TaxRulesInfo>("dbo.usp_Aspx_TaxRuleGetAll", parameterCollection);
            return lstTaxManageRule;
        }
       
        public int AddItem(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj, bool isActive, bool isModified,
            string sku, string activeFrom, string activeTo, string hidePrice, string isHideInRSS, string isHideToAnonymous,
             bool updateFlag)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemObj.ItemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemObj.ItemTypeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", itemObj.AttributeSetId));
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
                parameterCollection.Add(new KeyValuePair<string, object>("@TaxRuleID", itemObj.TaxRuleId));
                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", activeTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@HidePrice", hidePrice));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideInRSSFeed", isHideInRSS));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideToAnonymous", isHideToAnonymous));
                //For Static tabs
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoriesIDs", itemObj.CategoriesIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@RelatedItemsIDs", itemObj.RelatedItemsIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpSellItemsIDs", itemObj.UpSellItemsIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@CrossSellItemsIDs", itemObj.CrossSellItemsIds));

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@DownloadInfos", itemObj.DownloadItemsValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", updateFlag));

                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_ItemAddUpdate", parameterCollection,"@NewItemID");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveUpdateItemAttributes(int itemID, int attributeSetID, AspxCommonInfo aspxCommonObj , bool isActive, bool isModified, string attribValue, int attributeID, int inputTypeID, int validationTypeID, int attributeSetGroupID, bool isIncludeInPriceRule, bool isIncludeInPromotions, int displayOrder)
        {
            try
            {
                if (string.IsNullOrEmpty(attribValue))
                {
                    attribValue = null;
                }
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                //parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", updateFlag));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attribValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetGroupID", attributeSetGroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", isIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPromotions", isIncludeInPromotions));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                SQLHandler sqlH = new SQLHandler();
                //inputTypeID //validationTypeID
                string valueType = string.Empty;
                if (inputTypeID == 1)
                {
                    if (validationTypeID == 3)
                    {
                        valueType = "DECIMAL";
                    }
                    else if (validationTypeID == 5)
                    {
                        valueType = "INT";
                    }
                    else
                    {
                        valueType = "NVARCHAR";
                    }
                }
                else if (inputTypeID == 2)
                {
                    valueType = "TEXT";
                }
                else if (inputTypeID == 3)
                {
                    valueType = "DATE";
                }
                else if (inputTypeID == 4)
                {
                    valueType = "Boolean";
                }
                else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 ||
                         inputTypeID == 11 || inputTypeID == 12)
                {
                    valueType = "OPTIONS";
                }
                else if (inputTypeID == 7)
                {
                    valueType = "DECIMAL";
                }
                else if (inputTypeID == 8)
                {
                    valueType = "FILE";
                }
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemAttributesValue" + valueType + "AddUpdate", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// make the Item active deactive
        /// </summary>
        public void UpdateItemIsActive(int itemId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_UpdateItemIsActiveByItemID", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeFormInfo> GetItemAttributesValuesByItemID(int itemID, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID",aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID",aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName",aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName",aspxCommonObj.CultureName));
                itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesValuesByItemID", parameterCollection);
                return itemAttributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryInfo> GetCategoryList(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int itemId, bool serviceBit)
        {
            List<CategoryInfo> catList;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
            parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", serviceBit));
            catList = sqlH.ExecuteAsList<CategoryInfo>("[dbo].[usp_Aspx_GetCategoryListForItem]", parameterCollection);
            // catList = sqlH.ExecuteAsList<CategoryInfo>("dbo.usp_Aspx_GetCategoryListForCatalog", parameterCollection);
            return catList;
        }

        public bool CheckUniqueSKUCode(string sku, int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID",aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID",aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName",aspxCommonObj.CultureName));
                return sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_ItemSKUCodeUniquenessCheck", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItemImageByItemID(int itemId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemImageByItemID", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

        public static List<LatestItemsInfo> GetLatestItemsByCount(AspxCommonInfo aspxCommonObj, int count)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@Count", count));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<LatestItemsInfo>("dbo.usp_Aspx_LatestItemsGetByCount", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AttributeFormInfo> GetItemDetailsInfoByItemSKU(string itemSKU, int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<AttributeFormInfo> itemAttributes;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemDetailsByItemSKU", parameterCollection);
                return itemAttributes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemBasicDetailsInfo GetItemBasicInfo(string itemSKU, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ItemBasicDetailsInfo itemBasicDetails;
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                itemBasicDetails = sqlH.ExecuteAsObject<ItemBasicDetailsInfo>("dbo.usp_Aspx_ItemsGetBasicInfos", parameterCollection);
                return itemBasicDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CostVariantInfo> GetAllCostVariantOptions(int itemId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsList<CostVariantInfo>("dbo.usp_Aspx_BindCostVariantsInDropdownList", parameterCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------GetMostViewedItems---------------------------
        public List<MostViewedItemsInfo> GetAllMostViewedItems(int offset, int? limit, string name,AspxCommonInfo aspxCommonObj)
        {
            List<MostViewedItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ml = sqlH.ExecuteAsList<MostViewedItemsInfo>("usp_Aspx_GetMostViewedItems", parameterCollection);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get Low Stock Items----------------------------------------
        public List<LowStockItemsInfo> GetAllLowStockItems(int offset, int? limit, ItemSmallCommonInfo lowStockObj, AspxCommonInfo aspxCommonObj, int lowStock)
        {
            List<LowStockItemsInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", lowStockObj.SKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", lowStockObj.ItemName));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", lowStockObj.IsActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@LowStockQuantity", lowStock));
            ml = sqlH.ExecuteAsList<LowStockItemsInfo>("dbo.usp_Aspx_GetLowStockItems", parameterCollection);
            return ml;
        }
        //-------------------------------------------------------------------------------------------------
        //--------------------------Get Ordered Items List------------------------------------------------
        public List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name,AspxCommonInfo aspxCommonObj)
        {
            List<OrderItemsGroupByItemIDInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ml = sqlH.ExecuteAsList<OrderItemsGroupByItemIDInfo>("dbo.usp_Aspx_GetItemsOrdered", parameterCollection);
            return ml;
        }
        //----------------------------------------------------------------------------------------------
        //-----------------------------------Get DownLoadable Items----------------------------------------
        public List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, System.Nullable<int> limit, GetDownloadableItemInfo downloadableObj, AspxCommonInfo aspxCommonObj)
        {
            List<DownLoadableItemGetInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", downloadableObj.SKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", downloadableObj.ItemName));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CheckUser", downloadableObj.CheckUser));

            ml = sqlH.ExecuteAsList<DownLoadableItemGetInfo>("dbo.usp_Aspx_GetDownloadableItemsForReport", parameterCollection);
            return ml;
        }
        public void InsertBrandMapping(int ItemID, int BrandID, int storeId, int portalId, string userName, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
                parameter.Add(new KeyValuePair<string, object>("@BrandID", BrandID));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("usp_Aspx_InsertBrandMapping", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertAndUpdateItemVideos(int ItemID, string VideoIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
                parameter.Add(new KeyValuePair<string, object>("@VideosIDs", VideoIds));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqLh = new SQLHandler();
                sqLh.ExecuteNonQuery("[usp_Aspx_InsertAndUpdateItemVideos]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
