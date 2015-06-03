using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxCategoryManageProvider
    {
        public AspxCategoryManageProvider()
        {
        }

        public static List<AttributeFormInfo> GetCategoryFormAttributes(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {

            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
            List<AttributeFormInfo> formAttributeList = sqlHandler.ExecuteAsList<AttributeFormInfo>("dbo.usp_Aspx_GetCategoryFormAttributes", parameterCollection);
            return formAttributeList;
        }

        public static List<CategoryInfo> GetCategoryAll(bool isActive, AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
            List<CategoryInfo> catList = sqlHandler.ExecuteAsList<CategoryInfo>("dbo.usp_Aspx_GetCategoryAll", parameterCollection);
            return catList;
        }

        public static List<CategoryAttributeInfo> GetCategoryByCategoryID(Int32 categoryID, AspxCommonInfo aspxCommonObj)
        {

            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
            List<CategoryAttributeInfo> catList = sqlHandler.ExecuteAsList<CategoryAttributeInfo>("dbo.usp_Aspx_GetCategoryByCategoryID", parameterCollection);
            return catList;
        }

        public static bool CheckUniqueName(string catName, int catId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryName", catName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", catId));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_CategoryNameUniquenessCheck", parameterCollection, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int CategoryAddUpdate(CategoryInfo categoryInfo, List<CategoryAttributeInfo> listCA, string userName, string culture, string attributeIDs)
        {
            SQLHandler sqlHandler = new SQLHandler();
            int categoryID = 0;
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryInfo.CategoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("@SelectedItems", categoryInfo.SelectedItems));
            parameterCollection.Add(new KeyValuePair<string, object>("@ParentID", categoryInfo.ParentID));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInSearch", categoryInfo.IsShowInSearch));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInCatalog", categoryInfo.IsShowInCatalog));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInMenu", categoryInfo.IsShowInMenu));
            parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", categoryInfo.ActiveFrom));
            parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", categoryInfo.ActiveTo));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsService", categoryInfo.IsService));
            parameterCollection.Add(new KeyValuePair<string, object>("@BaseImage", categoryInfo.CategoryBaseImage));
            parameterCollection.Add(new KeyValuePair<string, object>("@ThumbNailImage", categoryInfo.CategoryThumbnailImage));
            parameterCollection.Add(new KeyValuePair<string, object>("@SmallImage", categoryInfo.CategorySmallImage));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", categoryInfo.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", categoryInfo.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryName", categoryInfo.CategoryName));
            parameterCollection.Add(new KeyValuePair<string, object>("@MetaTitle", categoryInfo.MetaTitle));
            parameterCollection.Add(new KeyValuePair<string, object>("@Description", categoryInfo.Description));
            parameterCollection.Add(new KeyValuePair<string, object>("@ShortDescription", categoryInfo.ShortDescription));
            parameterCollection.Add(new KeyValuePair<string, object>("@MetaKeyword", categoryInfo.MetaKeyword));
            parameterCollection.Add(new KeyValuePair<string, object>("@MetaDescription", categoryInfo.MetaDescription));
			parameterCollection.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIDs));
			try
            {
                categoryID = sqlHandler.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_CategoryAddUpdate", parameterCollection, "@NewCategoryID");
                if (!categoryInfo.HasSystemAttribute)
                {
                    int inputTypeID, validationTypeID;
                    string valueType = string.Empty;
                    foreach (CategoryAttributeInfo ca in listCA)
                    {
                        parameterCollection.Clear();
                        inputTypeID = ca.InputTypeID;
                        validationTypeID = ca.ValidationTypeID;
                        if (inputTypeID == 1)
                        {
                            if (validationTypeID == 3)
                            {
                                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.DecimalValue));
                                valueType = "DECIMAL";
                            }
                            else if (validationTypeID == 5)
                            {
                                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.IntValue));
                                valueType = "INT";
                            }
                            else
                            {
                                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.NvarcharValue));
                                valueType = "NVARCHAR";
                            }
                        }
                        else if (inputTypeID == 2)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.TextValue));
                            valueType = "TEXT";
                        }
                        else if (inputTypeID == 3)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.DateValue));
                            valueType = "DATE";
                        }
                        else if (inputTypeID == 4)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.BooleanValue));
                            valueType = "Boolean";
                        }
                        else if (inputTypeID == 5 || inputTypeID == 6 || inputTypeID == 9 || inputTypeID == 10 || inputTypeID == 11 || inputTypeID == 12)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.OptionValues));
                            valueType = "OPTIONS";
                        }
                        else if (inputTypeID == 7)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.DecimalValue));
                            valueType = "DECIMAL";
                        }
                        else if (inputTypeID == 8)
                        {
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", ca.FileValue));
                            valueType = "FILE";
                        }
                        parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                        parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", ca.AttributeID));
                        parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", categoryInfo.StoreID));
                        parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", categoryInfo.PortalID));
                        parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                        parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                        sqlHandler.ExecuteNonQuery("dbo.usp_Aspx_CategoryAttributesValue" + valueType + "AddUpdate", parameterCollection);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return categoryID;
        }

        public static void DeleteCategory(Int32 storeID, Int32 portalID, Int32 categoryID, string userName, string culture)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", culture));
                sqlHandler.ExecuteNonQuery("dbo.usp_Aspx_CategoryDelete", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CategoryItemInfo> GetCategoryItems(Int32 offset, int? limit, GetCategoryItemInfo categoryItemsInfo, AspxCommonInfo aspxCommonObj, bool serviceBit)
        {

            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@serviceBit", serviceBit));
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryItemsInfo.CategoryID));
            parameterCollection.Add(new KeyValuePair<string, object>("@SKU", categoryItemsInfo.SKU));
            parameterCollection.Add(new KeyValuePair<string, object>("@Name", categoryItemsInfo.Name));
            parameterCollection.Add(new KeyValuePair<string, object>("@PriceFrom", categoryItemsInfo.PriceFrom));
            parameterCollection.Add(new KeyValuePair<string, object>("@PriceTo", categoryItemsInfo.PriceTo));
            List<CategoryItemInfo> listCategoryItem = sqlHandler.ExecuteAsList<CategoryItemInfo>("[dbo].[usp_Aspx_GetItemsByCategoryIDService]", parameterCollection);
            return listCategoryItem;
        }

        public static string GetCategoryCheckedItems(int CategoryID, AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);       
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", CategoryID));
            string categoryItem = sqlHandler.ExecuteAsScalar<string>("[dbo].[usp_Aspx_GetItemsByCategoryIDAdmin]", parameterCollection);
            return categoryItem;
        }

        public static bool SaveChangesCategoryTree(string categoryIDs, AspxCommonInfo aspxCommonObj)
        {
            SQLHandler sqlHandler = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@CategoryIDs", categoryIDs));
            sqlHandler.ExecuteNonQuery("dbo.usp_Aspx_CategoryTreeUpdate", parameterCollection);
            return true;
        }

        public static void ActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                sqlHandler.ExecuteNonQuery("dbo.usp_Aspx_ActivateCategory", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void DeActivateCategory(int categoryID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoryID", categoryID));
                sqlHandler.ExecuteNonQuery("dbo.usp_Aspx_DeActivateCategory", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
