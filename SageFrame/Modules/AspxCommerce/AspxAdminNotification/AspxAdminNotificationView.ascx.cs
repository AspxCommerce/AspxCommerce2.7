using SageFrame.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AspxCommerce_AspxAdminNotification_AspxAdminNotificationView : BaseAdministrationUserControl
{
    public string AspxAdminNotificationModulePath;
    public bool IsUseFriendlyUrls = true;
      protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            AspxAdminNotificationModulePath = ResolveUrl(modulePath);

            if (!IsPostBack)
            {

                IncludeJs("AspxAdminNotificationView",
                      "/Modules/AspxCommerce/AspxAdminNotification/js/AspxAdminNotificationView.js"
                      );

                IncludeCss("AspxAdminNotificationView",
                     "/Modules/AspxCommerce/AspxAdminNotification/css/AdminNotification.css",
                     "/Modules/AspxCommerce/AspxAdminNotification/css/style.css"
                    );
              
            }

            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    public void BindNitification()
    {
        int usersInfoCount = 0;
        int itemsInfoCount = 5;
        int ordersInfoCount = 0;
        StringBuilder contents = new StringBuilder();
        contents.Append("<div class=\"maindivbg\">");
        contents.Append("<div class=\"maindiv aspxnotifyview\">");
        contents.Append("<ul><li>");
        if (usersInfoCount > 0)
        {
            contents.Append("<span id=\"spanUsersInfo\" class=\"notired\">" + usersInfoCount + "</span>");
        }
        contents.Append("<a class=\"showfrindreq mesgnotfctn spritimg notifriend\" title=\"Click to View User Info\">ws</a></li>");
        contents.Append("<li>");
        if (itemsInfoCount > 0)
        {
            contents.Append("<span id=\"spanItemsInfo\" class=\"notired\">" + itemsInfoCount + "</span>");
        }
        contents.Append("<a class=\"showmesgreq spritimg messagenormal\" title=\"Click to View Items Info\">ws</a></li>");
        contents.Append("<li>");
        if (ordersInfoCount > 0)
        {
            contents.Append("<span id=\"spanOrdersInfo\" class=\"notired\">" + ordersInfoCount + "</span>");
        }

        contents.Append("<a class=\"showmesgreq spritimg nottifinormal\" title=\"Click to View Orders Info\">ws</a></li>");
        contents.Append("</ul><div></div></div></div>");
        divNotification.InnerHtml = contents.ToString();
    }
}