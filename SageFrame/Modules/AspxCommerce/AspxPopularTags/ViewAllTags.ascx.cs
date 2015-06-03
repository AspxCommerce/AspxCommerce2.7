using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.PopularTags;

public partial class Modules_AspxCommerce_AspxPopularTags_ViewAllTags : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID, PopularTagsCount;
    public string CultureName, PopularTagsModulePath;
    public string ViewTaggedItemPage;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PopularTagsModulePath = ResolveUrl((this.AppRelativeTemplateSourceDirectory));

            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CustomerID = GetCustomerID;
            CultureName = GetCurrentCultureName;            
            GetPopularTagsSettings();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void GetPopularTagsSettings()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        PopularTagsController ptc = new PopularTagsController();
        List<PopularTagsSettingInfo> ptSettingInfo = ptc.GetPopularTagsSetting(aspxCommonObj);
        if (ptSettingInfo != null && ptSettingInfo.Count > 0)
        {
            foreach (var item in ptSettingInfo)
            {
                PopularTagsCount = item.PopularTagCount;
                ViewTaggedItemPage = item.ViewTaggedItemPageName;
            }
        }
    }
}