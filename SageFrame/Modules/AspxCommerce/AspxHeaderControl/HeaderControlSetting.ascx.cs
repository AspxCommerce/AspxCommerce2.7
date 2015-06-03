using System;
using System.Collections.Generic;
using System.Web;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Collections;

public partial class Modules_AspxCommerce_AspxHeaderControl_HeaderControlSetting : BaseAdministrationUserControl
{
    public string ModuleServicePath;
    public string HeaderType;
    public AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ModuleServicePath = ResolveUrl("~") + "Modules/AspxCommerce/AspxCommerceServices/";
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            HeaderSettingInfo objHeadeSetting = new HeaderSettingInfo();
            objHeadeSetting = AspxHeaderController.GetHeaderSetting(aspxCommonObj);
            HeaderType = objHeadeSetting.HeaderType;
        }
        IncludeLanguageJS();

    }
}
