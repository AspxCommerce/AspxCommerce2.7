using System;
using System.Collections.Generic;
using AspxCommerce.Core;
using AspxCommerce.YouMayAlsoLike;
using SageFrame.Web;
using System.Web.Script.Serialization;

public partial class Modules_AspxCommerce_AspxYouMayAlsoLike_YouMayAlsoLikeSetting : BaseAdministrationUserControl
{
    public int StoreID, PortalID, NoOfYouMayAlsoLikeItems, NoOfYouMayAlsoLikeInARow;
    public string CultureName = string.Empty;
    public bool EnableYouMayAlsoLike;
    public string YouMayAlsoLikeModulePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                YouMayAlsoLikeModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                GetYouMayAlsoLikeSetting();
                IncludeLanguageJS();

            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    public string Settings = "";
    public void GetYouMayAlsoLikeSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        AspxYouMayAlsoLikeController objYouMayLike = new AspxYouMayAlsoLikeController();
        YouMayAlsoLikeSettingInfo lstYouMayAlsoLikeSetting = objYouMayLike.GetYouMayAlsoLikeSetting(aspxCommonObj);
        if (lstYouMayAlsoLikeSetting != null)
        {
            object obj = new
            {
                EnableYouMayAlsoLike = lstYouMayAlsoLikeSetting.IsEnableYouMayAlsoLike,
                NoOfYouMayAlsoLikeItems = lstYouMayAlsoLikeSetting.YouMayAlsoLikeCount,
                NoOfYouMayAlsoLikeInARow = lstYouMayAlsoLikeSetting.YouMayAlsoLikeInARow,
                YouMayAlsoLikeModulePath = YouMayAlsoLikeModulePath
            };
            Settings = json_serializer.Serialize(obj);
        }        
    }
}
