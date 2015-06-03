using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AspxItemAttrMgntController
    {
       public AspxItemAttrMgntController()
       {
       }

       /// <summary>
       /// To Bind grid
       /// </summary>
       /// <returns></returns>
       public static List<AttributesBasicInfo> GetItemAttributes(int offset, int limit, AttributeBindInfo attrbuteBindObj, AspxCommonInfo aspxCommonObj)
       {
           List<AttributesBasicInfo> ml;
           ml = AspxItemAttrMgntProvider.GetItemAttributes(offset,limit,attrbuteBindObj,aspxCommonObj);
           return ml;
       }

       /// <summary>
       /// To Bind Attribute Type dropdown
       /// </summary>
       /// <returns></returns>
       public static List<AttributesInputTypeInfo> GetAttributesInputType()
       {
           List<AttributesInputTypeInfo> ml;
           ml = AspxItemAttrMgntProvider.GetAttributesInputType();
           return ml;
       }

       /// <summary>
       /// To Bind Attribute Item Type dropdown
       /// </summary>
       /// <returns></returns>
       public static List<AttributesItemTypeInfo> GetAttributesItemType(AspxCommonInfo aspxCommonObj)
       {
           List<AttributesItemTypeInfo> ml;
           ml = AspxItemAttrMgntProvider.GetAttributesItemType(aspxCommonObj);
           return ml;
       }

       /// <summary>
       /// To Bind Attribute validation Type dropdown
       /// </summary>
       /// <returns></returns>
       public static List<AttributesValidationTypeInfo> GetAttributesValidationType()
       {
           List<AttributesValidationTypeInfo> ml;
           ml = AspxItemAttrMgntProvider.GetAttributesValidationType();
           return ml;
       }

       /// <summary>
       /// To Bind for Attribute ID
       /// </summary>
       /// <returns></returns>
       public static List<AttributesGetByAttributeIdInfo> GetAttributesInfoByAttributeID(int attributeId, AspxCommonInfo aspxCommonObj)
       {
           List<AttributesGetByAttributeIdInfo> ml;
           ml = AspxItemAttrMgntProvider.GetAttributesInfoByAttributeID(attributeId,aspxCommonObj);
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
               AspxItemAttrMgntProvider.DeleteMultipleAttributes(attributeIds, aspxCommonObj);
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
               AspxItemAttrMgntProvider.DeleteAttribute(attributeId, aspxCommonObj);
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
              AspxItemAttrMgntProvider.UpdateAttributeIsActive(attributeId,aspxCommonObj,isActive);
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
               AspxItemAttrMgntProvider.SaveAttribute(attributeToInsert);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static AttributeFormInfo SaveAttribute(AttributesGetByAttributeIdInfo attributeToInsert, AttributeConfig config)
       {
           try
           {
             return  AspxItemAttrMgntProvider.SaveAttribute(attributeToInsert, config);
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
           ml = AspxItemAttrMgntProvider.GetAttributeSet(aspxCommonObj);
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
               return AspxItemAttrMgntProvider.SaveUpdateAttributeSet(attributeSetInfoToInsert, aspxCommonObj);
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
              return AspxItemAttrMgntProvider.CheckAttributeSetUniqueName(checkUniqueAttrSet, aspxCommonObj);
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
           ml = AspxItemAttrMgntProvider.GetAttributeSetGrid(offset, limit, AttributeSetBindObj, aspxCommonObj);
           return ml;
       }

       /// <summary>
       /// To Bind Attribute set from fill using Attribute set ID
       /// </summary>
       /// <returns></returns>
       public static List<AttributeSetGetByAttributeSetIdInfo> GetAttributeSetInfoByAttributeSetID(int attributeSetId, AspxCommonInfo aspxCommonObj)
       {
           List<AttributeSetGetByAttributeSetIdInfo> ml;
           ml = AspxItemAttrMgntProvider.GetAttributeSetInfoByAttributeSetID(attributeSetId,aspxCommonObj);
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
               AspxItemAttrMgntProvider.DeleteAttributeSet(attributeSetId, aspxCommonObj);

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
               AspxItemAttrMgntProvider.UpdateAttributeSetIsActive(attributeSetId, aspxCommonObj, isActive);

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
               AspxItemAttrMgntProvider.UpdateAttributeGroup(attributeSaveObj, aspxCommonObj);
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
               AspxItemAttrMgntProvider.DeleteAttributeSetGroup(deleteGroupObj, aspxCommonObj);
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
               ml = AspxItemAttrMgntProvider.RenameAttributeSetGroupAlias(attributeSetInfoToUpdate);
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
               AspxItemAttrMgntProvider.DeleteAttribute(deleteGroupObj, aspxCommonObj);
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

               bool isUnique = AspxItemAttrMgntProvider.CheckUniqueName(attrbuteUniqueObj, aspxCommonObj);
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
