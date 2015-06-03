using System;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxCommerce_AspxGeneralSearch_SimpleSearchSetting : BaseAdministrationUserControl
{
    public string ModuleServicePath, ShowCategoryForSearch, EnableAdvanceSearch, ShowSearchKeyWord;
    protected void Page_Load(object sender, EventArgs e)
    {
        ModuleServicePath = ResolveUrl("~") + "Modules/AspxCommerce/AspxCommerceServices/";
        IncludeJs("SimpleSearchSettingJS", "/Modules/AspxCommerce/AspxGeneralSearch/js/SimpleSearchSetting.js");
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(GetStoreID, GetPortalID, GetCurrentCultureName);
        if (!IsPostBack)
        {
            SearchSettingInfo objSettingInfo = AspxSearchController.GetSearchSetting(aspxCommonObj);
            ShowCategoryForSearch = objSettingInfo.ShowCategoryForSearch;
            EnableAdvanceSearch = objSettingInfo.EnableAdvanceSearch;
            ShowSearchKeyWord = objSettingInfo.ShowSearchKeyWord;
        }
        IncludeLanguageJS();
    }
}