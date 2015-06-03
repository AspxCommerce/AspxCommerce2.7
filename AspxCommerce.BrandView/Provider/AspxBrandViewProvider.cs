using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.BrandView
{
    public class AspxBrandViewProvider
    {
        public AspxBrandViewProvider()
        { }
        public List<BrandViewInfo> GetAllBrandForSlider(AspxCommonInfo aspxCommonObj, int BrandCount)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("@BrandCount", BrandCount));
                SQLHandler sqLH = new SQLHandler();
                return sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllBrandForSlider", ParaMeter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetBrandSettingAndSlider(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqLH = new SQLHandler();
                return sqLH.ExecuteAsDataSet("usp_Aspx_BrandSettingAndSlider", ParaMeter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandViewInfo> GetAllBrandForItem(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqLH = new SQLHandler();
                List<BrandViewInfo> lstBrand = sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllBrandForItem", ParaMeter);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BrandViewInfo> GetAllFeaturedBrand(AspxCommonInfo aspxCommonObj, int Count)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                ParaMeter.Add(new KeyValuePair<string, object>("@Count", Count));
                SQLHandler sqLH = new SQLHandler();
                List<BrandViewInfo> lstBrand = sqLH.ExecuteAsList<BrandViewInfo>("usp_Aspx_GetAllFeaturedBrands", ParaMeter);
                return lstBrand;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BrandSettingInfo GetBrandSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                BrandSettingInfo view =
                    sqlH.ExecuteAsObject<BrandSettingInfo>("[dbo].[usp_Aspx_BrandSettingGet]", paramCol);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BrandSettingsUpdate(string SettingValues, string SettingKeys, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramCol = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                paramCol.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
                paramCol.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
                SQLHandler sqlH = new SQLHandler();                
                    sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_BrandSettingsUpdate]", paramCol);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BrandRssInfo> GetBrandRssContent(AspxCommonInfo aspxCommonObj, string rssOption, int count)
        {
            try
            {                
                List<KeyValuePair<string, object>> Parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                Parameter.Add(new KeyValuePair<string, object>("@Count", count));
                Parameter.Add(new KeyValuePair<string, object>("@RssOption", rssOption));
                SQLHandler SQLH = new SQLHandler();
                List<BrandRssInfo> rssFeedContent = SQLH.ExecuteAsList<BrandRssInfo>("[dbo].[usp_Aspx_GetRssFeedBrand]", Parameter);
                return rssFeedContent;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
