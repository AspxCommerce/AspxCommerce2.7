using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Data.SqlClient;
using System.Data;
using SageFrame.Common;
using System.Web;

namespace AspxCommerce.Core
{
    public class AspxImportExportController
    {
        #region Import FROM Excel
        
        public List<string> GetExcelConnection(string fileName)
        {
            try
            {
                var xlsHeader = new List<string>();
                if (fileName != "")
                {
                    string xlPath = HttpContext.Current.Server.MapPath("~/" + fileName); //fileName.Replace("/", @"\"); //@"F:\AKBook.xls";    //location of xlsx file
                    string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
                                    ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                    con.Open();
                    OleDbDataReader dreader = cmd.ExecuteReader();

                    while (dreader.Read())
                    {
                        for (int col = 0; col < dreader.FieldCount; col++)
                        {

                            var columnName = dreader.GetName(col).ToString();
                            var columnFieldType = dreader.GetFieldType(col).ToString(); // Gets the column type
                            var columnDbType = dreader.GetDataTypeName(col).ToString(); // Gets the column database type
                            var columnValue = dreader.GetValue(col).ToString();
                            if (!xlsHeader.Contains(columnName))
                            {
                                xlsHeader.Add(columnName);
                            }
                        }
                    }
                    dreader.Close();
                    return xlsHeader;
                }
                else
                {
                    return xlsHeader;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public List<AttributeInfo> GetAttributesListForImport()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "[dbo].[usp_Aspx_GetAllAttributes]";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlConn.Open();
                SqlDataReader dr = null;
                dr = sqlCmd.ExecuteReader();
                List<AttributeInfo> lst = new List<AttributeInfo>();
                while (dr.Read())
                {
                    var att = new AttributeInfo();
                    att.AttributeID = int.Parse(dr[0].ToString());
                    att.AttributeName = dr[1].ToString();
                    att.InputTypeID = int.Parse(dr[2].ToString());
                    att.ValidationTypeID = int.Parse(dr[3].ToString());
                    lst.Add(att);
                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        public void GetExcelData(List<FinalImportDataInfo> finalInfo, AspxCommonInfo aspxCommonObj, string fileName)
        {
            try
            {
                if (fileName != "")
                {
                    string xlPath = HttpContext.Current.Server.MapPath("~/" + fileName); //@"F:\AKBook.xls"; //location of xlsx file
                    string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
                                    ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                    con.Open();
                    OleDbDataReader dreader = cmd.ExecuteReader();
                    while (dreader.Read())
                    {
                        var currencyCode = "";
                        var itemValues = new List<ItemsValues>();

                        for (int col = 0; col < dreader.FieldCount; col++)
                        {

                            var columnName = dreader.GetName(col).ToString();
                            var columnFieldType = dreader.GetFieldType(col).ToString(); // Gets the column type
                            var columnDbType = dreader.GetDataTypeName(col).ToString(); // Gets the column database type
                            var columnValue = dreader.GetValue(col).ToString();
                            var id = GetAttrubuteId(finalInfo, columnName);
                            var inputId = GetInputTypeId(finalInfo, columnName);
                            var validId = GetValidationTypeId(finalInfo, columnName);
                            if (id > 0)
                            {
                                itemValues.Add(new ItemsValues()
                                {
                                    AttributeId = id,
                                    InputTypeID = inputId,
                                    ValidationTypeID = validId,
                                    AttributeValue = columnValue

                                });
                            }
                            if (columnName == "Category")
                            {
                                itemValues.Add(new ItemsValues()
                                {
                                    AttributeId = 0,
                                    InputTypeID = 0,
                                    ValidationTypeID = 0,
                                    AttributeValue = columnValue
                                });
                            }

                            if (columnName == "CurrencyCode")
                            {
                                currencyCode = columnValue;
                            }

                            if (dreader.FieldCount == col + 1)
                            {
                                int attributeSetId = 2;
                                int itemId = SaveItemFromExcel(itemValues, false, 1, attributeSetId, currencyCode  , aspxCommonObj);
                                SQLHandler sqlH = new SQLHandler();
                                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_TruncateCategoryForExcel]");
                                SaveItemImage(itemId, itemValues, aspxCommonObj);
                                SaveItemAttributesFromExcel(itemId, attributeSetId, itemValues, aspxCommonObj);
                                CacheHelper.Clear("CategoryInfo" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                                CacheHelper.Clear("CategoryForSearch" + aspxCommonObj.StoreID + aspxCommonObj.PortalID + "_" + aspxCommonObj.CultureName);
                            }
                        }
                    }
                    dreader.Close();
                    GetExcelCostVariantData(aspxCommonObj, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetAttrubuteId(List<FinalImportDataInfo> collection, string key)
        {
            return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.AttributeID).FirstOrDefault();
        }

        private int GetInputTypeId(List<FinalImportDataInfo> collection, string key)
        {
            return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.InputTypeID).FirstOrDefault();
        }

        private int GetValidationTypeId(List<FinalImportDataInfo> collection, string key)
        {
            return (from t in collection where t.Header.ToLower().Trim() == key.ToLower().Trim() select t.ValidationTypeID).FirstOrDefault();
        }

        private object GetValuesByAttirbuteId(List<ItemsValues> itemObj, int attributeId)
        {
            return (from t in itemObj where t.AttributeId == attributeId select t.AttributeValue).FirstOrDefault();
        }

        public int SaveItemFromExcel(List<ItemsValues> itemObj, bool isTypeSpecified, int itemTypeId, int attributeSetId, string currencyCode, AspxCommonInfo aspxCommonObj)// bool isActive, bool isModified,
        //string sku, string activeFrom, string activeTo, string hidePrice, string isHideInRSS, string isHideToAnonymous,
        // bool updateFlag)
        {
            System.DateTime today = System.DateTime.Now;
            DateTime activeFrom = DateTime.Now;
            System.TimeSpan day1 = new System.TimeSpan(1, 0, 0, 0);
            activeFrom = today.Subtract(day1);
            DateTime activeTo = DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(365, 0, 0, 0);
            activeTo = today.Add(duration);
            bool hasSystemAttributesOnly = true;
            foreach (ItemsValues item in itemObj)
            {
                if (item.AttributeId > 48)
                {
                    hasSystemAttributesOnly = false;
                    break;
                }
            }
            try
            {
                string attributeIDs = "1,2,3,4,5,6,7,8,9,10,11,13,14,15,19,20,23,24,25,26,27,28,29,30,31,32,33,34,44,45,46,47,48";

                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", 0));
                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypeID", isTypeSpecified ? itemTypeId : 1));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
                parameterCollection.Add(new KeyValuePair<string, object>("@TaxRuleID", 1));
                parameterCollection.Add(new KeyValuePair<string, object>("@SKU", GetValuesByAttirbuteId(itemObj, 4)));

                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveFrom", activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@ActiveTo", activeTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@HidePrice", false));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideInRSSFeed", false));
                parameterCollection.Add(new KeyValuePair<string, object>("@HideToAnonymous", false));
                parameterCollection.Add(new KeyValuePair<string, object>("@Name", GetValuesByAttirbuteId(itemObj, 1)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Description", GetValuesByAttirbuteId(itemObj, 2)));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShortDescription",
                                                                         GetValuesByAttirbuteId(itemObj, 3)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Weight", GetValuesByAttirbuteId(itemObj, 5)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Quantity", GetValuesByAttirbuteId(itemObj, 15)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Price", GetValuesByAttirbuteId(itemObj, 8)));

                if (GetValuesByAttirbuteId(itemObj, 13).ToString() != "")
                {
                    parameterCollection.Add(new KeyValuePair<string, object>("@ListPrice",
                                                                             GetValuesByAttirbuteId(itemObj, 13)));
                }
                else
                {
                    parameterCollection.Add(new KeyValuePair<string, object>("@ListPrice", null));
                }
                parameterCollection.Add(new KeyValuePair<string, object>("@NewFromDate", activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@NewToDate", activeTo));

                parameterCollection.Add(new KeyValuePair<string, object>("@MetaTitle", GetValuesByAttirbuteId(itemObj, 9)));
                parameterCollection.Add(new KeyValuePair<string, object>("@MetaKeyword", GetValuesByAttirbuteId(itemObj, 10)));
                parameterCollection.Add(new KeyValuePair<string, object>("@MetaDescription",
                                                                         GetValuesByAttirbuteId(itemObj, 11)));

                //parameterCollection.Add(new KeyValuePair<string, object>("@VisibilityOptionValueID", true));

                if (GetValuesByAttirbuteId(itemObj, 26) != null)
                {
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsFeaturedOptionValueID",
                                                                             Boolean.Parse(
                                                                                 GetValuesByAttirbuteId(itemObj, 26).
                                                                                     ToString())));
                }
                if (GetValuesByAttirbuteId(itemObj, 29) != null)
                {
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsSpecialOptionValueID",
                                                                             Boolean.Parse(
                                                                                 GetValuesByAttirbuteId(itemObj, 29).
                                                                                     ToString())));
                }
                parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedFrom",
                                                                         activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@FeaturedTo", activeTo));


                parameterCollection.Add(new KeyValuePair<string, object>("@SpecialFrom", activeFrom));
                parameterCollection.Add(new KeyValuePair<string, object>("@SpecialTo", activeTo));
                parameterCollection.Add(new KeyValuePair<string, object>("@Length", GetValuesByAttirbuteId(itemObj, 32)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Height", GetValuesByAttirbuteId(itemObj, 33)));
                parameterCollection.Add(new KeyValuePair<string, object>("@Width", GetValuesByAttirbuteId(itemObj, 34)));
                //parameterCollection.Add(new KeyValuePair<string, object>("@IsPromo", false));
                //parameterCollection.Add(new KeyValuePair<string, object>("@ServiceDuration", null));

                parameterCollection.Add(new KeyValuePair<string, object>("@HasSystemAttributesOnly",
                                                                         hasSystemAttributesOnly));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIDs));

                //For Static tabs
                parameterCollection.Add(new KeyValuePair<string, object>("@CategoriesIDs",
                                                                         GetValuesByAttirbuteId(itemObj, 0)));
                parameterCollection.Add(new KeyValuePair<string, object>("@RelatedItemsIDs", "0"));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpSellItemsIDs", "0"));
                parameterCollection.Add(new KeyValuePair<string, object>("@CrossSellItemsIDs", "0"));

                parameterCollection.Add(new KeyValuePair<string, object>("@DownloadInfos", ""));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", false));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", false));
                //parameterCollection.Add(new KeyValuePair<string, object>("@BrandID", '0'));
                parameterCollection.Add(new KeyValuePair<string, object>("@CurrencyCode", currencyCode));
                //parameterCollection.Add(new KeyValuePair<string, object>("@VideosIDs", '0'));
                parameterCollection.Add(new KeyValuePair<string, object>("@CostPrice", GetValuesByAttirbuteId(itemObj, 44)));
                parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPrice", GetValuesByAttirbuteId(itemObj, 45)));
                parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPriceFrom", GetValuesByAttirbuteId(itemObj, 46)));
                parameterCollection.Add(new KeyValuePair<string, object>("@SpecialPriceTo", GetValuesByAttirbuteId(itemObj, 47)));
                parameterCollection.Add(new KeyValuePair<string, object>("@ManufacturerPrice", GetValuesByAttirbuteId(itemObj, 48)));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_ItemAddUpdateFromExcel", parameterCollection,
                                                            "@NewItemID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveItemImage(int itemId, List<ItemsValues> imageObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sageSql = new SQLHandler();
                var baseImage = GetValuesByAttirbuteId(imageObj, 12);
                if (baseImage != null && baseImage.ToString() != "")
                {
                    var imagePath = baseImage;
                    List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                    parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                    parameterCollection.Add(new KeyValuePair<string, object>("@PathList", imagePath));
                    parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
                    parameterCollection.Add(new KeyValuePair<string, object>("@ImageType", 1));
                    parameterCollection.Add(new KeyValuePair<string, object>("@AlternateText", GetValuesByAttirbuteId(imageObj, 1)));
                    parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", "1"));
                    parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                    sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection);
                }

                //var smallImage = GetValuesByAttirbuteId(imageObj, 21);
                //if (smallImage != null && smallImage != "")
                //{
                //    var imagePath1 = "Modules/AspxCommerce/AspxItemsManagement/uploads + smallImage;
                //    List<KeyValuePair<string, object>> parameterCollection1 = new List<KeyValuePair<string, object>>();
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@PathList", imagePath1));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@IsActive", true));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@ImageType", 2));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@AlternateText", ""));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@DisplayOrder", "3"));
                //    parameterCollection1.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                //    sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection1);
                //}

                var thumbImage = GetValuesByAttirbuteId(imageObj, 22);
                if (thumbImage != null && thumbImage.ToString() != "")
                {
                    var imagePath2 = thumbImage;
                    List<KeyValuePair<string, object>> parameterCollection2 = new List<KeyValuePair<string, object>>();
                    parameterCollection2.Add(new KeyValuePair<string, object>("@ItemID", itemId));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@PathList", imagePath2));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@IsActive", true));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@ImageType", 3));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@AlternateText", GetValuesByAttirbuteId(imageObj, 1)));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@DisplayOrder", "2"));
                    parameterCollection2.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                    sageSql.ExecuteNonQuery("[dbo].[usp_Aspx_InsertUpdateImageFromExcel]", parameterCollection2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveItemAttributesFromExcel(int itemID, int attributeSetId, List<ItemsValues> itemObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                foreach (var itemsValues in itemObj)
                {
                    if (itemsValues.AttributeId > 48)
                    {
                        if (itemsValues.InputTypeID > 0 && itemsValues.InputTypeID != 8)
                        {
                            List<KeyValuePair<string, object>> parameterCollection =
                                new List<KeyValuePair<string, object>>();
                            parameterCollection.Add(new KeyValuePair<string, object>("@ItemID", itemID));
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
                            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
                            parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", false));
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                     itemsValues.AttributeValue));
                            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", itemsValues.AttributeId));
                            parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", itemsValues.InputTypeID));
                            parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID",
                                                                                     itemsValues.ValidationTypeID));

                            int? groupId = GetAttributeSetGroupID(itemsValues.AttributeId, attributeSetId, aspxCommonObj.StoreID, aspxCommonObj.PortalID);

                            parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", groupId));
                            parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", false));
                            parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", 1));
                            //inputTypeID //validationTypeID
                            string valueType = string.Empty;
                            if (itemsValues.InputTypeID == 1)
                            {
                                if (itemsValues.ValidationTypeID == 3)
                                {
                                    valueType = "DECIMAL";
                                }
                                else if (itemsValues.ValidationTypeID == 5)
                                {
                                    valueType = "INT";
                                }
                                else
                                {
                                    valueType = "NVARCHAR";
                                }
                            }
                            else if (itemsValues.InputTypeID == 2)
                            {
                                valueType = "TEXT";
                            }
                            else if (itemsValues.InputTypeID == 3)
                            {
                                valueType = "DATE";
                            }
                            else if (itemsValues.InputTypeID == 4)
                            {
                                valueType = "Boolean";
                            }
                            else if (itemsValues.InputTypeID == 5 || itemsValues.InputTypeID == 6 ||
                                     itemsValues.InputTypeID == 9 ||
                                     itemsValues.InputTypeID == 10 ||
                                     itemsValues.InputTypeID == 11 || itemsValues.InputTypeID == 12)
                            {
                                valueType = "OPTIONS";

                                //TODO: to get attributeValueID from itemsValues.AttributeValue
                                List<KeyValuePair<string, object>> paramCol =
                                new List<KeyValuePair<string, object>>();
                                paramCol.Add(new KeyValuePair<string, object>("@AttributeValue", itemsValues.AttributeValue));
                                paramCol.Add(new KeyValuePair<string, object>("@AttributeID", itemsValues.AttributeId));
                                paramCol.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                                paramCol.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                                paramCol.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                                paramCol.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                                itemsValues.AttributeValue = sqlH.ExecuteNonQueryAsGivenType<int>("dbo.usp_Aspx_GetAttributesValueIDByValue",
                                                 paramCol, "@AttributeValueID");
                                parameterCollection.RemoveAll(x => x.Key.Equals("@AttributeValue"));
                                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue",
                                                                                     itemsValues.AttributeValue));
                            }
                            else if (itemsValues.InputTypeID == 7)
                            {
                                valueType = "DECIMAL";
                            }
                            //else if (itemsValues.InputTypeID == 8)
                            //{
                            //    valueType = "FILE";
                            //    valueType.Replace("T, "");
                            //    valueType.Replace("P, "");
                            //}
                            sqlH.ExecuteNonQuery("dbo.usp_Aspx_ItemAttributesValue" + valueType + "AddUpdate",
                                                 parameterCollection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetAttributeSetGroupID(int attributeId, int attributeSetId, int storeId, int portalId)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                SQLHandler sqlH = new SQLHandler();
                int? id = sqlH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_GetGroupIDByAttributeSetID]",
                                                   parameterCollection);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ImportCostVariants

       
        public void GetExcelCostVariantData(AspxCommonInfo aspxCommonObj, string fileName)
        {
            try
            {
                if (fileName != "")
                {
                    string xlPath = HttpContext.Current.Server.MapPath("~/" + fileName); //@"F:\optionvariant 022113.xls"; //location of xlsx file
                    string constr1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xlPath +
                                     ";Extended Properties=\"Excel 12.0 Xml; HDR=YES; IMEX=1;\"";
                    OleDbConnection con1 = new OleDbConnection(constr1);
                    OleDbCommand cmd1 = new OleDbCommand("select * from [Sheet2$]", con1);
                    con1.Open();
                    OleDbDataReader dreader1 = cmd1.ExecuteReader();
                    while (dreader1.Read())
                    {
                        var costVariantOptions = new List<CostVariantsOptions>();

                        for (int col = 0; col < dreader1.FieldCount; col++)
                        {

                            var columnName = dreader1.GetName(col).ToString();
                            var columnFieldType = dreader1.GetFieldType(col).ToString(); // Gets the column type
                            var columnDbType = dreader1.GetDataTypeName(col).ToString(); // Gets the column database type
                            var columnValue = dreader1.GetValue(col).ToString();
                            costVariantOptions.Add(new CostVariantsOptions()
                            {
                                ColumnName = columnName,
                                ColumnValue = columnValue

                            });
                            if (dreader1.FieldCount == col + 1)
                            {
                                SaveCostVariantOptionsFromExcel(costVariantOptions, aspxCommonObj);
                            }
                        }
                    }
                    dreader1.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveCostVariantOptionsFromExcel(List<CostVariantsOptions> optionObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@Sku", optionObj[0].ColumnValue));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantName", optionObj[4].ColumnValue));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantValueName", optionObj[5].ColumnValue));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantPriceValue", decimal.Parse(optionObj[3].ColumnValue)));
                parameter.Add(new KeyValuePair<string, object>("@CostVariantWeightValue", decimal.Parse(optionObj[1].ColumnValue)));
                parameter.Add(new KeyValuePair<string, object>("@Quantity", int.Parse(optionObj[2].ColumnValue)));
                parameter.Add(new KeyValuePair<string, object>("@Description", ""));
                parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", 1));
                parameter.Add(new KeyValuePair<string, object>("@InputTypeID", 6));
                parameter.Add(new KeyValuePair<string, object>("@DisplayOrder", optionObj[6].ColumnValue));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_SaveCostVariantsFromExcel]", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
