using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.Core
{
   public class AspxItemMgntProvider
    {
       public AspxItemMgntProvider()
       {
       }
       public static List<ItemCommonInfo> GetItemsListByCategory(AspxCommonInfo aspxCommonObj, int categoryID)
       {
           List<ItemCommonInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
           ml = sqlH.ExecuteAsList<ItemCommonInfo>("usp_Aspx_GetItemsListByCategory", parameterCollection);
           return ml;
       }

       /// <summary>
       /// To Bind grid with all Items
       /// </summary>
       public static List<ItemsInfo> GetAllItems(int offset, int limit, GetItemListInfo getItemObj, AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SKU", getItemObj.SKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", getItemObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", getItemObj.ItemTypeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", getItemObj.AttributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@Visibility", getItemObj.Visibility));
           parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", getItemObj.IsActive));    
           ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_ItemsGetAll", parameterCollection);
           return ml;
       }
       /// <summary>
       /// To Bind grid with all Related Items
       /// </summary>

       public static List<ItemsInfo> GetRelatedItemsByItemID(int offset, int limit, ItemDetailsCommonInfo IDCommonObj, AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", IDCommonObj.SelfItemID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", IDCommonObj.ItemSKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", IDCommonObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", IDCommonObj.ItemTypeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", IDCommonObj.AttributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", IDCommonObj.ServiceBit));
           ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetRelatedItemsByItemID", parameterCollection);
           return ml;
       }
       /// <summary>
       /// Get Associated Items for group products
       /// </summary>
       /// <param name="offset"></param>
       /// <param name="limit"></param>
       /// <param name="IDCommonObj"></param>
       /// <param name="aspxCommonObj"></param>
       /// <returns></returns>
       public static List<ItemsInfo> GetAssociatedItemsByItemID(int offset, int limit, ItemDetailsCommonInfo IDCommonObj,int categoryID,AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfo> itemsList;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", IDCommonObj.SelfItemID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", IDCommonObj.ItemSKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", IDCommonObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", IDCommonObj.ItemTypeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", IDCommonObj.AttributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", IDCommonObj.ServiceBit));
           parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
           itemsList = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetAssociatedItemsByItemID", parameterCollection);
           return itemsList;
       }
       /// <summary>
       /// To Bind grid with all UP Sell Items
       /// </summary>

       public static List<ItemsInfo> GetUpSellItemsByItemID(int offset, int limit, ItemDetailsCommonInfo UpSellCommonObj, AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", UpSellCommonObj.SelfItemID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", UpSellCommonObj.ItemSKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", UpSellCommonObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", UpSellCommonObj.ItemTypeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", UpSellCommonObj.AttributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", UpSellCommonObj.ServiceBit));
           ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetUpSellItemsByItemID", parameterCollection);
           return ml;
       }

       /// <summary>
       /// To Bind grid with all Cross Sell Items
       /// </summary>

       public static List<ItemsInfo> GetCrossSellItemsByItemID(int offset, int limit, ItemDetailsCommonInfo CrossSellCommonObj, AspxCommonInfo aspxCommonObj)
       {
           List<ItemsInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SelfItemID", CrossSellCommonObj.SelfItemID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", CrossSellCommonObj.ItemSKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", CrossSellCommonObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", CrossSellCommonObj.ItemTypeID));
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", CrossSellCommonObj.AttributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", CrossSellCommonObj.ServiceBit));
           ml = sqlH.ExecuteAsList<ItemsInfo>("dbo.usp_Aspx_GetCrossSellItemsByItemID", parameterCollection);
           return ml;
       }

       public static string GetRelatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           string ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);          
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
           ml = sqlH.ExecuteAsScalar<string>("dbo.usp_Aspx_GetRelatedCheckIDs", parameterCollection);
           return ml;
       }

       public static string GetAssociatedCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           string idsList;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);          
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
           idsList = sqlH.ExecuteAsScalar<string>("dbo.usp_Aspx_GetAssociatedItemsCheckIDs", parameterCollection);
           return idsList;
       }
       
       public static string GetUpSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           string ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
           ml = sqlH.ExecuteAsScalar<string>("dbo.usp_Aspx_GetUpSellCheckIDs", parameterCollection);
           return ml;
       }

       public static string GetCrossSellCheckIDs(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           string ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
           ml = sqlH.ExecuteAsScalar<string>("dbo.usp_Aspx_GetCrossSellCheckIDs", parameterCollection);
           return ml;
       }

       /// <summary>
       /// To Delete Multiple Item IDs
       /// </summary>
       public static void DeleteMultipleItems(string itemIds, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj); 
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemIDs", itemIds));
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

       public static void DeleteSingleItem(string itemId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemByItemID", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<AttributeFormInfo> GetItemFormAttributes(int attributeSetID, int itemTypeID, AspxCommonInfo aspxCommonObj)
       {
           List<AttributeFormInfo> formAttributeList;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj); 
           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
           formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributes", parameterCollection);
           return formAttributeList;
       }

       public static List<AttributeFormInfo> GetItemFormAttributesByItemSKUOnly(string itemSKU, AspxCommonInfo aspxCommonObj)
       {
           List<AttributeFormInfo> formAttributeList;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
           formAttributeList = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesByItemSKU", parameterCollection);
           return formAttributeList;
       }

       public static List<TaxRulesInfo> GetAllTaxRules(int storeID, int portalID, bool isActive)
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

       public static int SaveUpdateItemAttributes(ItemsInfo.ItemSaveBasicInfo itemObj, AspxCommonInfo aspxCommonObj, bool isActive, bool isModified,
           ItemInformationDetailsInfo itemInfoDetails,
                    List<ItemAttributeDetailsInfo> listIA, string attributeIDs, bool hasSystemAttributesOnly,
                        bool updateFlag,string groupPrices)
       {
           int newItemID = 0;
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemObj.ItemId));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemObj.ItemTypeId));
               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", itemObj.AttributeSetId));
               parameterCollection.Add(new KeyValuePair<string, object>("@TaxRuleID", itemObj.TaxRuleId));

               parameterCollection.Add(new KeyValuePair<string, object>("@SKU", itemInfoDetails.SKU));
               parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", itemInfoDetails.ActiveFrom));
               parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", itemInfoDetails.ActiveTo));
               parameterCollection.Add(new KeyValuePair<string, object>("@HidePrice", itemInfoDetails.HidePrice));
               parameterCollection.Add(new KeyValuePair<string, object>("@HideInRSSFeed",
                                                                        itemInfoDetails.HideInRSSFeed));
               parameterCollection.Add(new KeyValuePair<string, object>("@HideToAnonymous",
                                                                        itemInfoDetails.HideToAnonymous));

               parameterCollection.Add(new KeyValuePair<string, object>("@Name", itemInfoDetails.Name));
               parameterCollection.Add(new KeyValuePair<string, object>("@Description", itemInfoDetails.Description));
               parameterCollection.Add(new KeyValuePair<string, object>("@ShortDescription",
                                                                        itemInfoDetails.ShortDescription));
               parameterCollection.Add(new KeyValuePair<string, object>("@Weight", itemInfoDetails.Weight));
               parameterCollection.Add(new KeyValuePair<string, object>("@Quantity", itemInfoDetails.Quantity));
               parameterCollection.Add(new KeyValuePair<string, object>("@Price", itemInfoDetails.Price));
               parameterCollection.Add(new KeyValuePair<string, object>("@ListPrice", itemInfoDetails.ListPrice));
               parameterCollection.Add(new KeyValuePair<string, object>("@NewFromDate", itemInfoDetails.NewFromDate));
               parameterCollection.Add(new KeyValuePair<string, object>("@NewToDate", itemInfoDetails.NewToDate));

               parameterCollection.Add(new KeyValuePair<string, object>("@MetaTitle", itemInfoDetails.MetaTitle));
               parameterCollection.Add(new KeyValuePair<string, object>("@MetaKeyword", itemInfoDetails.MetaKeyword));
               parameterCollection.Add(new KeyValuePair<string, object>("@MetaDescription",
                                                                        itemInfoDetails.MetaDescription));
               parameterCollection.Add(new KeyValuePair<string, object>("@VisibilityOptionValueID",
                                                                        itemInfoDetails.VisibilityOptionValueID));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsFeaturedOptionValueID",
                                                                        itemInfoDetails.IsFeaturedOptionValueID));
               parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedFrom",
                                                                        itemInfoDetails.FeaturedFrom));
               parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedTo", itemInfoDetails.FeaturedTo));

               parameterCollection.Add(new KeyValuePair<string, object>("@IsSpecialOptionValueID",
                                                                        itemInfoDetails.IsSpecialOptionValueID));
               parameterCollection.Add(new KeyValuePair<string, object>("@SpecialFrom", itemInfoDetails.SpecialFrom));
               parameterCollection.Add(new KeyValuePair<string, object>("@SpecialTo", itemInfoDetails.SpecialTo));
               parameterCollection.Add(new KeyValuePair<string, object>("@Length", itemInfoDetails.Length));
               parameterCollection.Add(new KeyValuePair<string, object>("@Height", itemInfoDetails.Height));
               parameterCollection.Add(new KeyValuePair<string, object>("@Width", itemInfoDetails.Width));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsPromo", itemInfoDetails.IsPromo));
               parameterCollection.Add(new KeyValuePair<string, object>("@ServiceDuration",
                                                                        itemInfoDetails.ServiceDuration));

               parameterCollection.Add(new KeyValuePair<string, object>("@HasSystemAttributesOnly",
                                                                        hasSystemAttributesOnly));
               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIDs));

               //For Static tabs
               parameterCollection.Add(new KeyValuePair<string, object>("@CategoriesIDs", itemObj.CategoriesIds));
               parameterCollection.Add(new KeyValuePair<string,object>("@AssociatedItemIDs",itemObj.AssociatedItemIds));
               parameterCollection.Add(new KeyValuePair<string, object>("@RelatedItemsIDs", itemObj.RelatedItemsIds));
               parameterCollection.Add(new KeyValuePair<string, object>("@UpSellItemsIDs", itemObj.UpSellItemsIds));
               parameterCollection.Add(new KeyValuePair<string, object>("@CrossSellItemsIDs", itemObj.CrossSellItemsIds));

               parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
               parameterCollection.Add(new KeyValuePair<string, object>("@DownloadInfos", itemObj.DownloadItemsValue));
               parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", updateFlag));

               parameterCollection.Add(new KeyValuePair<string, object>("@BrandID", itemObj.BrandId));
               parameterCollection.Add(new KeyValuePair<string, object>("@CurrencyCode", itemObj.CurrencyCode));
               parameterCollection.Add(new KeyValuePair<string, object>("@VideosIDs", itemObj.ItemVideoIDs));

               //For settings tabs
               parameterCollection.Add(new KeyValuePair<string, object>("@IsManageInventory", itemInfoDetails.IsManageInventory));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsUsedStoreSetting", itemInfoDetails.IsUsedStoreSetting));
               parameterCollection.Add(new KeyValuePair<string, object>("@MinCartQuantity", itemInfoDetails.MinCartQuantity));
               parameterCollection.Add(new KeyValuePair<string, object>("@MaxCartQuantity", itemInfoDetails.MaxCartQuantity));

               parameterCollection.Add(new KeyValuePair<string, object>("@LowStockQuantity", itemInfoDetails.LowStockQuantity));
               parameterCollection.Add(new KeyValuePair<string, object>("@OutOfStockQuantity", itemInfoDetails.OutOfStockQuantity));

               //for price eg.cost price
               //For settings tabs
               parameterCollection.Add(new KeyValuePair<string, object>("@CostPrice", itemInfoDetails.CostPrice));
               parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPrice", itemInfoDetails.SpecialPrice));
               parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPriceFrom", itemInfoDetails.SpecialPriceFrom));
               parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPriceTo", itemInfoDetails.SpecialPriceTo));
               parameterCollection.Add(new KeyValuePair<string, object>("@ManufacturerPrice", itemInfoDetails.ManufacturerPrice));
               parameterCollection.Add(new KeyValuePair<string, object>("@GroupPrices", groupPrices));

               SQLHandler sqlH = new SQLHandler();
               newItemID = sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_ItemAddUpdate", parameterCollection,
                                                                "@NewItemID");

               int attributeID,inputTypeID, validationTypeID, groupID, displayOrder;
               bool isIncludeInPriceRule;
               string valueType = string.Empty;

               if (hasSystemAttributesOnly && itemObj.AttributeSetId==3)
               {

                   foreach (ItemAttributeDetailsInfo ia in listIA)
                   {
                       parameterCollection.Clear();
                       attributeID = ia.AttributeID;                      
                       inputTypeID = ia.InputTypeID;
                       validationTypeID = ia.ValidationTypeID;
                       groupID = ia.GroupID;
                       isIncludeInPriceRule = ia.IsIncludeInPriceRule;
                       displayOrder = ia.DisplayOrder;
                       if (attributeID == 37)
                       {                         
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.IntValue));                           
                       }
                       else if (attributeID == 35)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                    ia.OptionValues));                          
                       }

                       parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                       parameterCollection.Add(new KeyValuePair<string, object>("@CultureName",
                                                                                aspxCommonObj.CultureName));
                       parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", newItemID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID",
                                                                                itemObj.AttributeSetId));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                       parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));                       
                       parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", groupID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule",
                                                                                isIncludeInPriceRule));
                       parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                       sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemDynamicAttributesAddUpdate",
                                            parameterCollection);
                   }
               }


               if (!hasSystemAttributesOnly)
               {
                   foreach (ItemAttributeDetailsInfo ia in listIA)
                   {
                       parameterCollection.Clear();
                       attributeID = ia.AttributeID;                      
                       inputTypeID = ia.InputTypeID;
                       validationTypeID = ia.ValidationTypeID;
                       groupID = ia.GroupID;
                       isIncludeInPriceRule = ia.IsIncludeInPriceRule;
                       displayOrder = ia.DisplayOrder;
                       if (inputTypeID == 1)
                       {
                           if (validationTypeID == 3)
                           {                              
                               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                        ia.DecimalValue));                              
                           }
                           else if (validationTypeID == 5)
                           {                              
                               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.IntValue));                              
                           }
                           else
                           {                              
                               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                        ia.NvarcharValue));                              
                           }
                       }
                       else if (inputTypeID == 2)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.TextValue));                         
                       }
                       else if (inputTypeID == 3)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.DateValue));                          
                       }
                       else if (inputTypeID == 4)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.BooleanValue));                          
                       }
                       else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 ||
                                inputTypeID == 11 || inputTypeID == 12)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                   ia.OptionValues));                          
                       }
                       else if (inputTypeID == 7)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                    ia.DecimalValue));                           
                       }
                       else if (inputTypeID == 8)
                       {                          
                           parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ia.FileValue));                          
                       }

                       parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                       parameterCollection.Add(new KeyValuePair<string, object>("@CultureName",
                                                                                aspxCommonObj.CultureName));
                       parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", newItemID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID",
                                                                                itemObj.AttributeSetId));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", isModified));
                       parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));                       
                       parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", inputTypeID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", validationTypeID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", groupID));
                       parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule",
                                                                                isIncludeInPriceRule));
                       parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                       sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemDynamicAttributesAddUpdate",
                                            parameterCollection);

                   }
               }
           }
           catch
               (Exception e)
           {
               throw e;
           }
           return newItemID;
       }

       /// <summary>
       /// make the Item active deactive
       /// </summary>
       public static void UpdateItemIsActive(int itemId, AspxCommonInfo aspxCommonObj, bool isActive)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("dbo.usp_Aspx_UpdateItemIsActiveByItemID", parameterCollection);

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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
               itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemFormAttributesValuesByItemID", parameterCollection);
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
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@Prefix", prefix));
           parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
           parameterCollection.Add(new KeyValuePair<string, object>("@ServiceBit", serviceBit));
           catList = sqlH.ExecuteAsList<CategoryInfo>("[dbo].[usp_Aspx_GetCategoryListForItem]", parameterCollection);
           return catList;
       }

       public static bool CheckIsItemInGroupItem(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
               bool isUnique = sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_CheckIsItemInGroupItem", parameterCollection, "@IsItemInGroupItem");
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@SKU", sku));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_ItemSKUCodeUniquenessCheck", parameterCollection, "@IsUnique");
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
               List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteItemImageByItemID", parameterCollection);
           }
           catch (Exception e)
           {
               throw e;
           }
       } 
      
       //--For Gift items List In Front End---------- 
       public static List<LatestItemsInfo> GetAllGiftCards(int offset, int limit,int rowTotal, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);               
               parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
               parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
               parameterCollection.Add(new KeyValuePair<string, object>("@RowTotal", rowTotal));
               return sqlH.ExecuteAsList<LatestItemsInfo>("[dbo].[usp_Aspx_GetAllGiftCards]", parameterCollection);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
               parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetID));
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", itemTypeID));
               itemAttributes = sqlH.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetItemDetailsByItemSKU", parameterCollection);
               return itemAttributes;
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
             
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@GroupSKU", groupSKU));
               List<ItemBasicDetailsInfo> lstGrupdProdct = sqlH.ExecuteAsList<ItemBasicDetailsInfo>("dbo.usp_Aspx_GetGroupedProductsByGroupSKU", parameterCollection);
               return lstGrupdProdct;
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
             
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@GroupItems", groupItems));
               decimal startPrice = sqlH.ExecuteAsScalar<decimal>("dbo.usp_Aspx_GetStartPriceOfGroupAfterDeletion", parameterCollection);
               return startPrice;
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
               itemBasicDetails = sqlH.ExecuteAsObject<ItemBasicDetailsInfo>("dbo.usp_Aspx_ItemsGetBasicInfos", parameterCollection);
               return itemBasicDetails;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static DataSet GetItemDetailsInfo(AspxCommonInfo aspxCommonObj,string itemSKU,string userIP, string countryName)
       {
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@UserIP", userIP));
           parameterCollection.Add(new KeyValuePair<string, object>("@ViewedFromcountry", countryName));
           return sqlH.ExecuteAsDataSet("[dbo].[usp_Aspx_GetItemDetailsInfo]", parameterCollection);

       }

       public static string GetItemVideos(string SKU, AspxCommonInfo aspxCommonObj)
       {
           try
           {               
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@SKU", SKU));
               string itemBasicDetails = sqlH.ExecuteAsScalar<string>("dbo.usp_Aspx_GetItemVideos", parameterCollection);
               return itemBasicDetails;
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamNoCID(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@itemSKU", addUpdateRecentObj.SKU));
               parameter.Add(new KeyValuePair<string, object>("@ViewFromIP", addUpdateRecentObj.IP));
               parameter.Add(new KeyValuePair<string, object>("@ViewedFromCountry", addUpdateRecentObj.CountryName));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_AddRecentlyViewedItems", parameter);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               SQLHandler sqlH = new SQLHandler();
               List<CostVariantInfo> lstCostVar= sqlH.ExecuteAsList<CostVariantInfo>("dbo.usp_Aspx_BindCostVariantsInDropdownList", parameterCollection);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemCostVariantsID", itemCostVariantID));
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleItemCostVariants", parameter);
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
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", name));
           parameterCollection.Add(new KeyValuePair<string, object>("@CurrencySymbol", currencySymbol));         
           ml = sqlH.ExecuteAsList<MostViewedItemsInfo>("usp_Aspx_GetMostViewedItems", parameterCollection);
           return ml;
       }
       //----------------------------------------------------------------------------------------------
       //-----------------------------------Get Low Stock Items----------------------------------------
       public static List<LowStockItemsInfo> GetAllLowStockItems(int offset, int? limit, ItemSmallCommonInfo lowStockObj, AspxCommonInfo aspxCommonObj, int lowStock)
       {
           List<LowStockItemsInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SKU", lowStockObj.SKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", lowStockObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", lowStockObj.IsActive));
           parameterCollection.Add(new KeyValuePair<string, object>("@LowStockQuantity", lowStock));          
           ml = sqlH.ExecuteAsList<LowStockItemsInfo>("dbo.usp_Aspx_GetLowStockItems", parameterCollection);
           return ml;
       }
       //-------------------------------------------------------------------------------------------------
       //--------------------------Get Ordered Items List------------------------------------------------
       public static List<OrderItemsGroupByItemIDInfo> GetOrderedItemsList(int offset, System.Nullable<int> limit, string name, AspxCommonInfo aspxCommonObj)
       {
           List<OrderItemsGroupByItemIDInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@Name", name));          
           ml = sqlH.ExecuteAsList<OrderItemsGroupByItemIDInfo>("dbo.usp_Aspx_GetItemsOrdered", parameterCollection);
           return ml;
       }
       //----------------------------------------------------------------------------------------------
       //-----------------------------------Get DownLoadable Items----------------------------------------
       public static List<DownLoadableItemGetInfo> GetDownLoadableItemsList(int offset, System.Nullable<int> limit, GetDownloadableItemInfo downloadableObj, AspxCommonInfo aspxCommonObj)
       {
           List<DownLoadableItemGetInfo> ml;
           SQLHandler sqlH = new SQLHandler();
           List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
           parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
           parameterCollection.Add(new KeyValuePair<string, object>("@SKU", downloadableObj.SKU));
           parameterCollection.Add(new KeyValuePair<string, object>("@ItemName", downloadableObj.ItemName));
           parameterCollection.Add(new KeyValuePair<string, object>("@CheckUser", downloadableObj.CheckUser));           
           ml = sqlH.ExecuteAsList<DownLoadableItemGetInfo>("dbo.usp_Aspx_GetDownloadableItemsForReport", parameterCollection);
           return ml;
       }

   #region NeW CostVariant Combination
       
       public static List<VariantCombination> GetCostVariantCombinationbyItemSku(string itemSku, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", itemSku));
               SQLHandler sqlH = new SQLHandler();
               List<VariantCombination> lstVarCom= sqlH.ExecuteAsList<VariantCombination>("[usp_Aspx_GetCombinationForCostVariantsByItemSKU]", parameterCollection);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemSKU", itemSku));
               var sqlH = new SQLHandler();
               List<ItemCostVariantsInfo> lstItemCostVar= sqlH.ExecuteAsList<ItemCostVariantsInfo>("[usp_Aspx_GetCostVaraiantsByItemSku]", parameterCollection);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               SQLHandler sqlH = new SQLHandler();
               List<CostVariantInfo> lstCostVar= sqlH.ExecuteAsList<CostVariantInfo>("usp_Aspx_GetCostVariantsList", parameterCollection);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
               SQLHandler sqlH = new SQLHandler();
               List<CostVariantValuesInfo> lstCostVarValue= sqlH.ExecuteAsList<CostVariantValuesInfo>("usp_Aspx_GetCostVariantValuesByCostVariantID", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteCostVariantsDetailsByItemID", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@ItemID", itemId));
               SQLHandler sqlH = new SQLHandler();
               List<VariantCombination> lstVarComb= sqlH.ExecuteAsList<VariantCombination>("usp_Aspx_BindAllCostVariantsDetailsByItemID", parameter);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemCostVariants.ItemId));
               parameterCollection.Add(new KeyValuePair<string, object>("@VariantOptions", cvCombinations));
               SQLHandler sq = new SQLHandler();
               sq.ExecuteNonQuery("usp_Aspx_SaveAndUpdateItemCostVariantsConfig", parameterCollection);
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

               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
               SQLHandler sqlH = new SQLHandler();
               List<TaxItemClassInfo> lstTaxItemClass = sqlH.ExecuteAsList<TaxItemClassInfo>("[dbo].[usp_Aspx_TaxItemClassGetAll]", parameterCollection);
               return lstTaxItemClass;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static List<UserInRoleInfo> BindRoles(bool isAll, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
               parameter.Add(new KeyValuePair<string, object>("@IsAll", isAll));
               parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
               parameter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
               SQLHandler sqlH = new SQLHandler();
               List<UserInRoleInfo> lstUserInRole= sqlH.ExecuteAsList<UserInRoleInfo>("sp_PortalRoleList", parameter);
               return lstUserInRole;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static ItemSEOInfo GetSEOSettingsBySKU(string itemSKU, AspxCommonInfo aspxCommonObj)
       {
           List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
           ParaMeter.Add(new KeyValuePair<string, object>("@itemSKU", itemSKU));
           SQLHandler sqlH = new SQLHandler();
           return sqlH.ExecuteAsObject<ItemSEOInfo>("usp_Aspx_ItemsSEODetailsBySKU", ParaMeter);
       }

       #region Downloadable Item Details
       public static List<DownLoadableItemInfo> GetDownloadableItem(int itemId, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@itemID", itemId));
               SQLHandler sqlh = new SQLHandler();
               List<DownLoadableItemInfo> lstDownItem= sqlh.ExecuteAsList<DownLoadableItemInfo>("usp_Aspx_GetDownloadableItem", parameter);
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
               List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
               SQLHandler sqLH = new SQLHandler();
               List<ItemsInfo.ItemSaveBasicInfo> lstItemVideo= sqLH.ExecuteAsList<ItemsInfo.ItemSaveBasicInfo>("[usp_Aspx_GetVideoIDsByItemID]", ParaMeter);
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
               List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
               SQLHandler sqLH = new SQLHandler();
               ItemSetting lstItemSetting = sqLH.ExecuteAsObject<ItemSetting>("[usp_Aspx_GetItemSettingByItemID]", ParaMeter);
               return lstItemSetting;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion
       #region "For Item Group Prices"
       public static List<ItemPriceGroupInfo> GetItemGroupPrices(int ItemID, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSP(aspxCommonObj);
               ParaMeter.Add(new KeyValuePair<string, object>("@ItemID", ItemID));
               SQLHandler sqLH = new SQLHandler();
               List<ItemPriceGroupInfo> lstGroupPrice = sqLH.ExecuteAsList<ItemPriceGroupInfo>("[usp_Aspx_GetItemGroupPrices]", ParaMeter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);                       
               SQLHandler sqLH = new SQLHandler();
               ItemTabSettingInfo lstItem = sqLH.ExecuteAsObject<ItemTabSettingInfo>("[usp_Aspx_ItemTabSettingGet]", parameter);
               return lstItem;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void ItemTabSettingSave(string SettingKeys, string SettingValues, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);               
               parameter.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
               parameter.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
               SQLHandler sqLH = new SQLHandler();
               sqLH.ExecuteNonQuery("[usp_Aspx_ItemTabSettingSave]", parameter);               
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       #endregion
    }
}
