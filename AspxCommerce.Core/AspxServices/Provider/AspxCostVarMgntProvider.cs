using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
   public class AspxCostVarMgntProvider
    {
       public AspxCostVarMgntProvider()
       {
       }
       //--------------------bind Cost Variants in Grid--------------------------
       
       public static List<CostVariantInfo> GetCostVariants(int offset, int limit, string variantName, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@offset", offset));
               parameter.Add(new KeyValuePair<string, object>("@limit", limit));
               parameter.Add(new KeyValuePair<string, object>("@VariantName", variantName));
               SQLHandler sqlH = new SQLHandler();
               List<CostVariantInfo> bind = sqlH.ExecuteAsList<CostVariantInfo>("usp_Aspx_BindCostVariantsInGrid", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CostVariantIds", costVariantIDs));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteMultipleCostVariants", parameter);
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
               List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
               parameter.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_DeleteSingleCostVariants", parameter);
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
               SQLHandler sqlH = new SQLHandler();
               List<AttributesInputTypeInfo> ml = sqlH.ExecuteAsList<AttributesInputTypeInfo>("dbo.usp_Aspx_CostVariantsInputTypeGetAll");
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
               List<CostVariantsGetByCostVariantIDInfo> lstCostVar= sqlH.ExecuteAsList<CostVariantsGetByCostVariantIDInfo>("usp_Aspx_CostVariantsGetByCostVariantID", parameterCollection);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
               List<CostVariantsvalueInfo> lstCVValue= sqlH.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetCostVariantValuesByCostVariantID", parameterCollection);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantID));
               List<CostVariantsvalueInfo> lstCVValue = sqlH.ExecuteAsList<CostVariantsvalueInfo>("usp_Aspx_GetCostVariantValuesByCostVariantIDForAdmin", parameterCollection);
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
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", variantObj.CostVariantID));
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", variantObj.CostVariantName));
               parameterCollection.Add(new KeyValuePair<string, object>("@Description", variantObj.Description));
               parameterCollection.Add(new KeyValuePair<string, object>("@InputTypeID", variantObj.InputTypeID));
               parameterCollection.Add(new KeyValuePair<string, object>("@DisplayOrder", variantObj.DisplayOrder));
               parameterCollection.Add(new KeyValuePair<string, object>("@ShowInAdvanceSearch", variantObj.ShowInAdvanceSearch));
               parameterCollection.Add(new KeyValuePair<string, object>("@ShowInComparison", variantObj.ShowInComparison));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsIncludeInPriceRule", variantObj.IsIncludeInPriceRule));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsActive", variantObj.IsActive));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsModified", variantObj.IsModified));
               parameterCollection.Add(new KeyValuePair<string, object>("@VariantOption", variantOptions));
               parameterCollection.Add(new KeyValuePair<string, object>("@IsNewFlag", variantObj.Flag));
               SQLHandler sqlH = new SQLHandler();
               sqlH.ExecuteNonQuery("usp_Aspx_SaveAndUpdateCostVariants", parameterCollection);
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
               SQLHandler sqlH = new SQLHandler();
               List<KeyValuePair<string, object>> parameterCollection = CommonParmBuilder.GetParamSP(aspxCommonObj);
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantName", costVariantName));
               parameterCollection.Add(new KeyValuePair<string, object>("@CostVariantID", costVariantId));
               bool isUnique= sqlH.ExecuteNonQueryAsBool("usp_Aspx_CostVariantUniquenessCheck", parameterCollection, "@IsUnique");
               return isUnique;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
