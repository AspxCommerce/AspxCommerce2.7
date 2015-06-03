using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.MegaCategory;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxMegaCategory_MegaCategorySetting : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string CultureName,MegaModulePath;
    public string Settings = string.Empty;   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeJs("MegaCategorySetting", "/Modules/AspxCommerce/AspxMegaCategory/js/MegaCategorySetting.js", "/js/FormValidation/jquery.validate.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CustomerID = GetCustomerID;
                CultureName = GetCurrentCultureName;
                MegaModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                UserModuleID = SageUserModuleID;
            }
            GetMegaCategorySetting();
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void GetMegaCategorySetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        MegaCategoryController objCat = new MegaCategoryController();
        MegaCategorySettingInfo megaCatSetting = objCat.GetMegaCategorySetting(aspxCommonObj);
        if (megaCatSetting != null)
        {
            object obj = new {
            ModeOfView = megaCatSetting.ModeOfView,
            NoOfColumn = megaCatSetting.NoOfColumn,
            ShowCatImage = megaCatSetting.ShowCategoryImage,
            ShowSubCatImage = megaCatSetting.ShowSubCategoryImage,
            Speed = megaCatSetting.Speed,
            Effect = megaCatSetting.Effect,
            EventMega = megaCatSetting.EventMega,
            Direction = megaCatSetting.Direction,
            MegaModulePath = MegaModulePath
        };
            Settings = json_serializer.Serialize(obj); 
        }
    }

    public string UserModuleID { get; set; }
}
