using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.AdvanceSearch;

/// <summary>
/// Summary description for AdvanceSearchWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AdvanceSearchWebService : System.Web.Services.WebService {

    public AdvanceSearchWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<AdvanceSearchSettingInfo> GetAdvanceSearchSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AdvanceSearchController asc = new AdvanceSearchController();
            List<AdvanceSearchSettingInfo> lstAdvanceSearch = asc.GetAdvanceSearchSetting(aspxCommonObj);
            return lstAdvanceSearch;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public void SaveAndUpdateAdvanceSearchSetting(AspxCommonInfo aspxCommonObj, AdvanceSearchSettingKeyPairInfo advanceObj)
    {
        try
        {
            AdvanceSearchController asc = new AdvanceSearchController();
            asc.SaveAndUpdateAdvanceSearchSetting(aspxCommonObj, advanceObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    //Auto Complete search Box
    [WebMethod]
    public List<SearchTermList> GetSearchedTermList(string search, AspxCommonInfo aspxCommonObj)
    {
        AdvanceSearchController asc = new AdvanceSearchController();
        List<SearchTermList> srInfo = asc.GetSearchedTermList(search, aspxCommonObj);
        return srInfo;
    }

    //[WebMethod]
    //public void AddUpdateSearchTerm(bool? hasData, string searchTerm, AspxCommonInfo aspxCommonObj)
    //{
    //    try
    //    {
    //        AspxSearchTermMgntController.AddUpdateSearchTerm(hasData, searchTerm, aspxCommonObj);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    #region More Advanced Search
    //------------------get dyanamic Attributes for serach-----------------------   
    [WebMethod]
    public List<AttributeShowInAdvanceSearchInfo> GetAttributes(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            AdvanceSearchController asc = new AdvanceSearchController();
            List<AttributeShowInAdvanceSearchInfo> lstAttr = asc.GetAttributes(aspxCommonObj);
            return lstAttr;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //------------------get items by dyanamic Advance serach-----------------------
    [WebMethod]
    public List<AdvanceSearchDetailsInfo> GetItemsByDyanamicAdvanceSearch(int offset, int limit, AspxCommonInfo aspxCommonObj, ItemsByDynamicAdvanceSearchInfo searchObj)
    {
        try
        {
            AdvanceSearchController asc = new AdvanceSearchController();
            List<AdvanceSearchDetailsInfo> lstAdvanceSearch = asc.GetItemsByDyanamicAdvanceSearch(offset, limit, aspxCommonObj, searchObj);
            return lstAdvanceSearch;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<Filter> GetDynamicAttributesForAdvanceSearch(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
    {
        AdvanceSearchController asc = new AdvanceSearchController();
        List<Filter> lstFilter = asc.GetDynamicAttributesForAdvanceSearch(aspxCommonObj, CategoryID, IsGiftCard);
        return lstFilter;
    }

    [WebMethod]
    public List<BrandItemsInfo> GetAllBrandForSearchByCategoryID(AspxCommonInfo aspxCommonObj, int CategoryID, bool IsGiftCard)
    {
        try
        {
            AdvanceSearchController asc = new AdvanceSearchController();
            List<BrandItemsInfo> lstBrandItem = asc.GetAllBrandForSearchByCategoryID(aspxCommonObj, CategoryID, IsGiftCard);
            return lstBrandItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    
}
