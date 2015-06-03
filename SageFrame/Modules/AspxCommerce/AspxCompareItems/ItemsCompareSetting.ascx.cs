using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using System.Web.Script.Serialization;
using AspxCommerce.CompareItem;

public partial class Modules_AspxCommerce_AspxItemsCompare_ItemsCompareSetting :BaseAdministrationUserControl
{
    public string compareItemsSettings = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetCompareItemsSettig();
            IncludeJs("ItemsCompareSettingJS", "/js/encoder.js", "/js/Templating/tmpl.js", "/js/jquery.cookie.js", "/Modules/AspxCommerce/AspxCompareItems/js/ItemsCompareSetting.js");
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    #region Get Compare Items Setting
    private void GetCompareItemsSettig()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(GetStoreID, GetPortalID, GetCurrentCultureName);
        string CompareItemsModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        CompareItemController cic = new CompareItemController();
        CompareItemsSettingInfo objCompareItemsSetting = cic.GetCompareItemsSetting(aspxCommonObj);
        if (objCompareItemsSetting != null)
        {
                //IsEnableCompareItem = objCompareItemsSetting.IsEnableCompareItem,
            object obj = new
            {
                CompareItemCount = objCompareItemsSetting.CompareItemCount,
                CompareDetailsPage = objCompareItemsSetting.CompareDetailsPage,
                CompareItemsModulePath = CompareItemsModulePath
            };
            compareItemsSettings = new JavaScriptSerializer().Serialize(obj);
        }
    }
    #endregion
}