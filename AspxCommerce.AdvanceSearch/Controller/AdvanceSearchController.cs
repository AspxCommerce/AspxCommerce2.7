using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using System.Data;

namespace AspxCommerce.AdvanceSearch
{
    public class AdvanceSearchController
    {
        public AdvanceSearchController() { }

        public DataSet GetAdvanceSearchDataSet(string prefix, bool isActive, AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                DataSet lstAdvanceSearch = asp.GetAdvanceSearchDataSet(prefix, isActive, aspxCommonObj, CategoryID, IsGiftCard);
                return lstAdvanceSearch;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AdvanceSearchSettingInfo> GetAdvanceSearchSetting(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<AdvanceSearchSettingInfo> lstAdvanceSearch = asp.GetAdvanceSearchSetting(aspxCommonObj);
                return lstAdvanceSearch;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveAndUpdateAdvanceSearchSetting(AspxCommonInfo aspxCommonObj, AdvanceSearchSettingKeyPairInfo advanceObj)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                asp.SaveAndUpdateAdvanceSearchSetting(aspxCommonObj, advanceObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CategoryInfo> GetAllCategoryForSearch(string prefix, bool isActive, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<CategoryInfo> catList = asp.GetAllCategoryForSearch(prefix, isActive, aspxCommonObj);
                return catList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SearchTermList> GetSearchedTermList(string search, AspxCommonInfo aspxCommonObj)
        {
            AdvanceSearchProvider asp = new AdvanceSearchProvider();
            List<SearchTermList> srInfo = asp.GetSearchedTermList(search, aspxCommonObj);
            return srInfo;
        }

        #region Advance Search

        public List<ItemTypeInfo> GetItemTypeList()
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<ItemTypeInfo> lstItemType = asp.GetItemTypeList();
                return lstItemType;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<AttributeFormInfo> GetAttributeByItemType(int itemTypeID, int storeID, int portalID, string cultureName)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<AttributeFormInfo> lstAttrForm = asp.GetAttributeByItemType(itemTypeID, storeID, portalID, cultureName);
                return lstAttrForm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region More Advanced Search
        //------------------get dyanamic Attributes for serach-----------------------   

        public List<AttributeShowInAdvanceSearchInfo> GetAttributes(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<AttributeShowInAdvanceSearchInfo> lstAttr = asp.GetAttributes(aspxCommonObj);
                return lstAttr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //------------------get items by dyanamic Advance serach-----------------------

        public List<AdvanceSearchDetailsInfo> GetItemsByDyanamicAdvanceSearch(int offset, int limit, AspxCommonInfo aspxCommonObj, ItemsByDynamicAdvanceSearchInfo searchObj)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<AdvanceSearchDetailsInfo> lstAdvanceSearch = asp.GetItemsByDyanamicAdvanceSearch(offset, limit, aspxCommonObj, searchObj);
                return lstAdvanceSearch;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Filter> GetDynamicAttributesForAdvanceSearch(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            AdvanceSearchProvider asp = new AdvanceSearchProvider();
            List<Filter> lstFilter = asp.GetDynamicAttributesForAdvanceSearch(aspxCommonObj, CategoryID, IsGiftCard);
            return lstFilter;
        }

        public List<BrandItemsInfo> GetAllBrandForSearchByCategoryID(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
        {
            try
            {
                AdvanceSearchProvider asp = new AdvanceSearchProvider();
                List<BrandItemsInfo> lstBrandItem = asp.GetAllBrandForSearchByCategoryID(aspxCommonObj, CategoryID, IsGiftCard);
                return lstBrandItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
     
        public void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                AspxSearchTermMgntController.AddUpdateSearchTerm(hasData, searchTerm, aspxCommonObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
