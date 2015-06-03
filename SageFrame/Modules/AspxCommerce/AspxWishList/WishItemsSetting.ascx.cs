using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.WishItem;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxWishList_WishItemsSetting : BaseAdministrationUserControl
{
    private string WishItemsModulePath;
    private int StoreID;
    private int CustomerID;
    private int PortalID;
    private string CultureName;
    public string wishItemsSettings = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string UserName;
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, CultureName);
            WishItemsModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            IncludeJs("RecentlyCompareJs", "/Modules/AspxCommerce/AspxWishList/js/WishItemsSetting.js");
            GetWishListItemsSettig(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetWishListItemsSettig(AspxCommonInfo aspxCommonObj)
    {
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        WishItemController wic = new WishItemController();
        WishItemsSettingInfo objWishItemSetting = wic.GetWishItemsSetting(aspxCommonObj);
        if (objWishItemSetting != null)
        {
            object obj = new
            {
                IsEnableImageInWishlist = objWishItemSetting.IsEnableImageInWishlist,
                NoOfRecentAddedWishItems = objWishItemSetting.NoOfRecentAddedWishItems,
                WishListPageName = objWishItemSetting.WishListPageName,
                WishItemsModulePath = WishItemsModulePath
            };
            wishItemsSettings = json_serializer.Serialize(obj);
        }
    }
}