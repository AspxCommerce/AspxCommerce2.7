using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.PopularTags;

/// <summary>
/// Summary description for PopularTagsWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class PopularTagsWebService : System.Web.Services.WebService {

    public PopularTagsWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<TagDetailsInfo> GetAllPopularTags(AspxCommonInfo aspxCommonObj, int count)
    {
        try
        {
            PopularTagsController ptc = new PopularTagsController();
            List<TagDetailsInfo> lstTagDetail = ptc.GetAllPopularTags(aspxCommonObj, count);
            return lstTagDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<PopularTagsSettingInfo> GetPopularTagsSetting(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            PopularTagsController ptc = new PopularTagsController();
            List<PopularTagsSettingInfo> pTSettingList = ptc.GetPopularTagsSetting(aspxCommonObj);
            return pTSettingList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public PopularTagsSettingKeyPair GetPopularTagsSettingValueByKey(AspxCommonInfo aspxCommonObj, string settingKey)
    {
        try
        {
            PopularTagsController ptc = new PopularTagsController();
            return ptc.GetPopularTagsSettingValueByKey(aspxCommonObj, settingKey);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void SaveUpdatePopularTagsSetting(AspxCommonInfo aspxCommonObj, PopularTagsSettingKeyPair pTSettingList)
    {
        try
        {
            PopularTagsController ptc = new PopularTagsController();
            ptc.SaveUpdatePopularTagsSetting(aspxCommonObj, pTSettingList);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<ItemBasicDetailsInfo> GetUserTaggedItems(int offset, int limit, string tagIDs, int SortBy, int rowTotal, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<ItemBasicDetailsInfo> lstItemBasic = PopularTagsController.GetUserTaggedItems(offset, limit, tagIDs, SortBy, rowTotal, aspxCommonObj);
            return lstItemBasic;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    
}
