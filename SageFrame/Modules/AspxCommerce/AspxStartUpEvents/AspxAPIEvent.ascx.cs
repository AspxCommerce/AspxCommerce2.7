using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using SageFrame.Web;
using System.Text;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxStartUpEvents_AspxAPIEvent : SageFrameStartUpControl
{
    public string ModuleRedirectPath = string.Empty;
    public string StoreDefaultCurrency;
    protected void Page_Load(object sender, EventArgs e)
    {
        int StoreID, PortalID;
        string CultureName, AllowRealTimeNotifications, UserName = string.Empty;
        ModuleRedirectPath = ResolveUrl(this.aspxRedirectPath);
        GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
        StoreSettingConfig ssc = new StoreSettingConfig();
        ssc.GetStoreSettingParamTwo(StoreSetting.AllowRealTimeNotifications, StoreSetting.MainCurrency, out AllowRealTimeNotifications, out StoreDefaultCurrency, StoreID, PortalID, CultureName);
        if (AllowRealTimeNotifications.ToLower() == "true")
        {
            Page.ClientScript.RegisterClientScriptInclude("SignlaR1", ResolveUrl("~/js/SignalR/jquery.signalR-2.2.0.min.js"));
            Page.ClientScript.RegisterClientScriptInclude("SignlaR2", ResolveUrl("~/signalr/hubs"));
            Page.ClientScript.RegisterClientScriptInclude("SignlaR3", ResolveUrl("~/Modules/AspxCommerce/AspxStartUpEvents/js/RealTimeAspxMgmt.js"));
        }
    }
}