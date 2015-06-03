using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Data;

namespace AspxCommerce.BrandView
{
    public class AspxBrandViewController
    {
        public AspxBrandViewController()
        {
        }
        public List<BrandViewInfo> GetAllBrandForSlider(AspxCommonInfo aspxCommonObj, int BrandCount)
        {
            try
            {
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                List<BrandViewInfo> objAllBrand = objBrand.GetAllBrandForSlider(aspxCommonObj, BrandCount);
                return objAllBrand;
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
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                DataSet objAllBrand = objBrand.GetBrandSettingAndSlider(aspxCommonObj);
                return objAllBrand;
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
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                List<BrandViewInfo> lstBrand = objBrand.GetAllBrandForItem(aspxCommonObj);
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
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                List<BrandViewInfo> lstBrand = objBrand.GetAllFeaturedBrand(aspxCommonObj, Count);
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
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                BrandSettingInfo lstBrand = objBrand.GetBrandSetting(aspxCommonObj);
                return lstBrand;

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
                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                objBrand.BrandSettingsUpdate(SettingValues, SettingKeys, aspxCommonObj); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public List<BrandRssInfo> GetBrandRssFeedContent(AspxCommonInfo aspxCommonObj,string rssOption, int count)
        {
            try
            {

                AspxBrandViewProvider objBrand = new AspxBrandViewProvider();
                List<BrandRssInfo> brandRssContent = objBrand.GetBrandRssContent(aspxCommonObj, rssOption, count);
                return brandRssContent;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
