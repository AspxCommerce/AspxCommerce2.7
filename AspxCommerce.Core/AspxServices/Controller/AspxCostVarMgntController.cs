using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
  public class AspxCostVarMgntController
    {
      public AspxCostVarMgntController()
      {
      }

      //--------------------bind Cost Variants in Grid--------------------------

      public static List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CostVariantInfo> bind = AspxCostVarMgntProvider.GetCostVariants(offset, limit, variantName, aspxCommonObj);
              return bind;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------Delete multiple cost variants --------------------------

      public static void DeleteMultipleCostVariants(string costVariantIDs, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxCostVarMgntProvider.DeleteMultipleCostVariants(costVariantIDs, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //------------------------ single Cost Variants management------------------------

      public static void DeleteSingleCostVariant(string costVariantID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxCostVarMgntProvider.DeleteSingleCostVariant(costVariantID, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      public static List<AttributesInputTypeInfo> GetCostVariantInputTypeList()
      {
          try
          {
              List<AttributesInputTypeInfo> ml = AspxCostVarMgntProvider.GetCostVariantInputTypeList();
              return ml;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //--------------- bind (edit) cost Variant management--------------------

      public static List<CostVariantsGetByCostVariantIDInfo> GetCostVariantInfoByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CostVariantsGetByCostVariantIDInfo> lstCostVar = AspxCostVarMgntProvider.GetCostVariantInfoByCostVariantID(costVariantID, aspxCommonObj);
              return lstCostVar;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //--------------- bind (edit) cost Variant values for cost variant ID --------------------

      public static List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantID(int costVariantID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntProvider.GetCostVariantValuesByCostVariantID(costVariantID, aspxCommonObj);
              return lstCVValue;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
      public static List<CostVariantsvalueInfo> GetCostVariantValuesByCostVariantIDForAdmin(int costVariantID, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              List<CostVariantsvalueInfo> lstCVValue = AspxCostVarMgntProvider.GetCostVariantValuesByCostVariantIDForAdmin(costVariantID, aspxCommonObj);
              return lstCVValue;
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //-----------Save and update Costvariant options-------------------------

      public static void SaveAndUpdateCostVariant(CostVariantsGetByCostVariantIDInfo variantObj, string variantOptions, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              AspxCostVarMgntProvider.SaveAndUpdateCostVariant(variantObj, variantOptions, aspxCommonObj);
          }
          catch (Exception e)
          {
              throw e;
          }
      }

      //---------------- Added for unique name check ---------------------

      public static bool CheckUniqueCostVariantName(string costVariantName, int costVariantId, AspxCommonInfo aspxCommonObj)
      {
          try
          {
              bool isUnique = AspxCostVarMgntProvider.CheckUniqueCostVariantName(costVariantName, costVariantId, aspxCommonObj);
              return isUnique;
          }
          catch (Exception e)
          {
              throw e;
          }
      }
    }
}
