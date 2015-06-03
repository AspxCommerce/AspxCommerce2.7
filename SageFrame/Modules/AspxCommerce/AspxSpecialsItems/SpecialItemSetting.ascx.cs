using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SageFrame.Web;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.SpecialItems;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxSpecialsItems_SpecialItemSetting : BaseAdministrationUserControl
{
    public string SpecialItemModulePath;
    public int StoreID, PortalID;
    public string CultureName = string.Empty;
    public string Settings = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeLanguageJS();
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                SpecialItemModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                IncludeJs("SpecialItemSetting", "/js/FormValidation/jquery.validate.js");
                IncludeLanguageJS();
                GetSpecialItemSetting();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    public void GetSpecialItemSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;

        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        SpecialItemsController sic = new SpecialItemsController();
        SpecialItemsSettingInfo lstSpecialSetting = sic.GetSpecialItemsSetting(aspxCommonObj);

        if (lstSpecialSetting != null)
        {
            object obj = new
            {
                IsEnableSpecialItems = lstSpecialSetting.IsEnableSpecialItems,
                NoOfItemShown = lstSpecialSetting.NoOfItemShown,
                NoOfItemInRow = lstSpecialSetting.NoOfItemInRow,
                IsEnableSpecialItemsRss = lstSpecialSetting.IsEnableSpecialItemsRss,
                SpecialItemsRssCount = lstSpecialSetting.SpecialItemsRssCount,
                SpecialItemsRssPageName = lstSpecialSetting.SpecialItemsRssPageName,
                SpecialItemsDetailPageName = lstSpecialSetting.SpecialItemsDetailPageName,
                SpecialItemModulePath = SpecialItemModulePath
            };
            Settings = json_serializer.Serialize(obj);
        }
    }
}