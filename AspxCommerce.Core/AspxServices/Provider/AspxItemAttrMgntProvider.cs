using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class AspxItemAttrMgntProvider
    {

        public AspxItemAttrMgntProvider()
        {

        }
        /// <summary>
        /// To Bind grid
        /// </summary>
        /// <returns></returns>
        public static List<AttributesBasicInfo> GetItemAttributes(int offset, int limit, AttributeBindInfo attrbuteBindObj, AspxCommonInfo aspxCommonObj)
        {
            List<AttributesBasicInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeName", attrbuteBindObj.AttributeName));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsRequired", attrbuteBindObj.IsRequired));
            parameterCollection.Add(new KeyValuePair<string, object>("@Comparable", attrbuteBindObj.ShowInComparison));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsSystem", attrbuteBindObj.IsSystemUsed));
            ml = sqlH.ExecuteAsList<AttributesBasicInfo>("dbo.usp_Aspx_AttributesGetAll", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Bind Attribute Type dropdown
        /// </summary>
        /// <returns></returns>
        public static List<AttributesInputTypeInfo> GetAttributesInputType()
        {
            List<AttributesInputTypeInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            ml = sqlH.ExecuteAsList<AttributesInputTypeInfo>("dbo.usp_Aspx_AttributesInputTypeGetAll");
            return ml;
        }

        /// <summary>
        /// To Bind Attribute Item Type dropdown
        /// </summary>
        /// <returns></returns>
        public static List<AttributesItemTypeInfo> GetAttributesItemType(AspxCommonInfo aspxCommonObj)
        {
            List<AttributesItemTypeInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
            ml = sqlH.ExecuteAsList<AttributesItemTypeInfo>("dbo.usp_Aspx_AttributesItemTypeGetAll", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Bind Attribute validation Type dropdown
        /// </summary>
        /// <returns></returns>
        public static List<AttributesValidationTypeInfo> GetAttributesValidationType()
        {
            List<AttributesValidationTypeInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            ml = sqlH.ExecuteAsList<AttributesValidationTypeInfo>("dbo.usp_Aspx_AttributesValidationTypeGetAll");
            return ml;
        }

        /// <summary>
        /// To Bind for Attribute ID
        /// </summary>
        /// <returns></returns>
        public static List<AttributesGetByAttributeIdInfo> GetAttributesInfoByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
        {
            List<AttributesGetByAttributeIdInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameterCollection.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ml = sqlH.ExecuteAsList<AttributesGetByAttributeIdInfo>("dbo.usp_Aspx_AttributesGetByAttributeID", parameterCollection);
            return ml;
        }
        /// <summary>
        /// To Delete Multiple Attribute IDs
        /// </summary>
        /// <returns></returns>
        public static void DeleteMultipleAttributes(string attributeIds, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeIDs", attributeIds));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_AttributesDeleteMultipleSelected", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Delete by Attribute ID
        /// </summary>
        /// <returns></returns>
        public static void DeleteAttribute(int attributeId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteAttributeByAttributeID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Activate/ Deactivate Attribute 
        /// </summary>
        /// <returns></returns>
        public static void UpdateAttributeIsActive(int attributeId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeId));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_UpdateAttributeIsActiveByAttributeID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Save Attribute 
        /// </summary>
        /// <returns></returns>
        public static AttributeFormInfo SaveAttribute(AttributesGetByAttributeIdInfo attributeToInsert, AttributeConfig config)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeToInsert.AttributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeName", attributeToInsert.AttributeName));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", attributeToInsert.InputTypeID));

                parameterCollection.Add(new KeyValuePair<string, object>("@DefaultValue", attributeToInsert.DefaultValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@Length", attributeToInsert.Length));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasName", attributeToInsert.AliasName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasToolTip", attributeToInsert.AliasToolTip));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasHelp", attributeToInsert.AliasHelp));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", attributeToInsert.DisplayOrder));

                parameterCollection.Add(new KeyValuePair<string, object>("@IsUnique", attributeToInsert.IsUnique));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsRequired", attributeToInsert.IsRequired));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", attributeToInsert.ShowInAdvanceSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", attributeToInsert.ShowInComparison));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", attributeToInsert.IsIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsEnableEditor", attributeToInsert.IsEnableEditor));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInItemDetail", attributeToInsert.IsShowInItemDetail));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInItemListing", attributeToInsert.IsShowInItemListing));


                parameterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", attributeToInsert.IsUseInFilter));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", attributeToInsert.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", attributeToInsert.IsModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", attributeToInsert.ValidationTypeID));

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", attributeToInsert.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", attributeToInsert.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", attributeToInsert.AddedBy));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", attributeToInsert.CultureName));

                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypes", attributeToInsert.ItemTypes));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", attributeToInsert.Flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUsedInConfigItem", attributeToInsert.IsUsedInConfigItem));
                parameterCollection.Add(new KeyValuePair<string, object>("@SaveOptions", attributeToInsert.SaveOptions));
                parameterCollection.Add(new KeyValuePair<string, object>("@attrValId", attributeToInsert.AttributeValueID));

                //config
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", config.GroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", config.AttributeSetID));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteAsObject<AttributeFormInfo>("[dbo].[usp_Aspx_AttributeAddOnTime]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// To Save Attribute 
        /// </summary>
        /// <returns></returns>
        public static void SaveAttribute(AttributesGetByAttributeIdInfo attributeToInsert)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeToInsert.AttributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeName", attributeToInsert.AttributeName));
                parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", attributeToInsert.InputTypeID));

                parameterCollection.Add(new KeyValuePair<string, object>("@DefaultValue", attributeToInsert.DefaultValue));
                parameterCollection.Add(new KeyValuePair<string, object>("@Length", attributeToInsert.Length));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasName", attributeToInsert.AliasName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasToolTip", attributeToInsert.AliasToolTip));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasHelp", attributeToInsert.AliasHelp));
                parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", attributeToInsert.DisplayOrder));

                parameterCollection.Add(new KeyValuePair<string, object>("@IsUnique", attributeToInsert.IsUnique));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsRequired", attributeToInsert.IsRequired));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", attributeToInsert.ShowInAdvanceSearch));
                parameterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", attributeToInsert.ShowInComparison));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", attributeToInsert.IsIncludeInPriceRule));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsEnableEditor", attributeToInsert.IsEnableEditor));

                parameterCollection.Add(new KeyValuePair<string, object>("@IsUseInFilter", attributeToInsert.IsUseInFilter));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInItemListing", attributeToInsert.IsShowInItemListing));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsShowInItemDetail", attributeToInsert.IsShowInItemDetail));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", attributeToInsert.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", attributeToInsert.IsModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@ValidationTypeID", attributeToInsert.ValidationTypeID));

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", attributeToInsert.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", attributeToInsert.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", attributeToInsert.AddedBy));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", attributeToInsert.CultureName));

                parameterCollection.Add(new KeyValuePair<string, object>("@ItemTypes", attributeToInsert.ItemTypes));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", attributeToInsert.Flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsUsedInConfigItem", attributeToInsert.IsUsedInConfigItem));
                parameterCollection.Add(new KeyValuePair<string, object>("@SaveOptions", attributeToInsert.SaveOptions));
                parameterCollection.Add(new KeyValuePair<string, object>("@attrValId", attributeToInsert.AttributeValueID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_AttributeAddUpdate", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Bind Attribute Set Base On dropdown
        /// </summary>
        /// <returns></returns>
        public static List<AttributeSetInfo> GetAttributeSet(AspxCommonInfo aspxCommonObj)
        {
            List<AttributeSetInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            ml = sqlH.ExecuteAsList<AttributeSetInfo>("dbo.usp_Aspx_AttributeSetGetAll", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Save Attribute Set
        /// </summary>
        /// <returns></returns>
        public static int SaveUpdateAttributeSet(AttributeSetInfo attributeSetInfoToInsert, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetInfoToInsert.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetBaseID", attributeSetInfoToInsert.AttributeSetBaseID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetName", attributeSetInfoToInsert.AttributeSetName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", attributeSetInfoToInsert.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", attributeSetInfoToInsert.IsModified));

                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", attributeSetInfoToInsert.AddedBy));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", attributeSetInfoToInsert.Flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@SaveString", attributeSetInfoToInsert.SaveString));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQuery("dbo.usp_Aspx_AttributeSetAddUpdate", parameterCollection, "@AttributeSetIDOut");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Check unique Attribute set name
        /// </summary>
        /// <returns></returns>
        public static int CheckAttributeSetUniqueName(AttributeSaveInfo checkUniqueAttrSet, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", checkUniqueAttrSet.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetName", checkUniqueAttrSet.AttributeSetName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsEdit", checkUniqueAttrSet.Flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                SQLHandler sqlH = new SQLHandler();
                return sqlH.ExecuteNonQuery("dbo.usp_Aspx_AttributeSetUniquenessCheck", parameterCollection, "@AttributeSetCount");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To Bind Attribute set grid
        /// </summary>
        /// <returns></returns>
        public static List<AttributeSetBaseInfo> GetAttributeSetGrid(int offset, int limit, AttributeSetBindInfo AttributeSetBindObj, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeSetBaseInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
            parameterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetName", AttributeSetBindObj.AttributeSetName));
            parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", AttributeSetBindObj.IsActive));
            parameterCollection.Add(new KeyValuePair<string, object>("@UsedInSystem", AttributeSetBindObj.IsSystemUsed));
            ml = sqlH.ExecuteAsList<AttributeSetBaseInfo>("dbo.usp_Aspx_AttributeSetGridGetAll", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To Bind Attribute set from fill using Attribute set ID
        /// </summary>
        /// <returns></returns>
        public static List<AttributeSetGetByAttributeSetIdInfo> GetAttributeSetInfoByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
        {
            List<AttributeSetGetByAttributeSetIdInfo> ml;
            SQLHandler sqlH = new SQLHandler();
            List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
            parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
            ml = sqlH.ExecuteAsList<AttributeSetGetByAttributeSetIdInfo>("dbo.usp_Aspx_AttributeSetGetByAttributeSetID", parameterCollection);
            return ml;
        }

        /// <summary>
        /// To update, delete  Attribute Set only if it is NOT SystemUsed Type
        /// </summary>
        /// <returns></returns>
        public static void DeleteAttributeSet(int attributeSetId, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteAttributeSetByAttributeSetID", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// To update, delete  Attribute Set only if it is NOT SystemUsed Type
        /// </summary>
        /// <returns></returns>
        public static void UpdateAttributeSetIsActive(int attributeSetId, AspxCommonInfo aspxCommonObj, bool isActive)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetId));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_UpdateAttributeSetIsActiveByAttributeSetID", parameterCollection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// To update, add  Attribute Group
        /// </summary>
        /// <returns></returns>
        public static void UpdateAttributeGroup(AttributeSaveInfo attributeSaveObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSaveObj.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupName", attributeSaveObj.GroupName));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", attributeSaveObj.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", attributeSaveObj.IsModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", attributeSaveObj.GroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasName", attributeSaveObj.AliasName));
                parameterCollection.Add(new KeyValuePair<string, object>("@UpdateFlag", attributeSaveObj.Flag));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_AttributeGroupAddUpdate", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteAttributeSetGroup(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", deleteGroupObj.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", deleteGroupObj.GroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteAttributeSetGroupByAttributeSetID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<AttributeSetGroupAliasInfo> RenameAttributeSetGroupAlias(AttributeSetGroupAliasInfo attributeSetInfoToUpdate)
        {
            try
            {
                List<AttributeSetGroupAliasInfo> ml;
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", attributeSetInfoToUpdate.GroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", attributeSetInfoToUpdate.CultureName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AliasName", attributeSetInfoToUpdate.AliasName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", attributeSetInfoToUpdate.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", attributeSetInfoToUpdate.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", attributeSetInfoToUpdate.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", attributeSetInfoToUpdate.IsActive));
                parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", attributeSetInfoToUpdate.IsModified));
                parameterCollection.Add(new KeyValuePair<string, object>("@UserName", attributeSetInfoToUpdate.UpdatedBy));
                SQLHandler sqlH = new SQLHandler();
                ml = sqlH.ExecuteAsList<AttributeSetGroupAliasInfo>("dbo.usp_Aspx_AttributeGroupAliasRename", parameterCollection);
                return ml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteAttribute(AttributeSaveInfo deleteGroupObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeSetID", deleteGroupObj.AttributeSetID));
                parameterCollection.Add(new KeyValuePair<string, object>("@GroupID", deleteGroupObj.GroupID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", deleteGroupObj.AttributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@DeletedBy", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("dbo.usp_Aspx_DeleteAttributeByAttributeSetID", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUniqueName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeName", attrbuteUniqueObj.AttributeName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attrbuteUniqueObj.AttributeID));
                bool isUnique = sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_AttributeUniquenessCheck", parameterCollection, "@IsUnique");
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
