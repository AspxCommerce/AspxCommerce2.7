using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Shared;
using SageFrame.Modules.Admin.PortalSettings;
using System.Text;

public partial class Modules_Admin_DashboardInfo_DashboardInfo : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SageFrameConfig objConfig = new SageFrameConfig();
        bool isDashboardHelpEnabled = objConfig.GetSettingBoolValueByIndividualKey(SageFrameSettingKeys.EnableDasboardHelp);
        if (isDashboardHelpEnabled)
        {
            divDashboardHelp.Visible = true;
        }
        else
        {
            divDashboardHelp.Visible = false;
        }
        IncludeCss("DashboardHelp", "/Modules/Admin/DashboardInformation/css/module.css");
        IncludeJs("DashboardHelp", "/Modules/Admin/DashboardInformation/js/DashBoardInfoJs.js");
        Specials();
    }
    protected void btnDisableDashboardhelp_Click(object sender, EventArgs e)
    {
        try
        {
            SettingProvider objSettingProvider = new SettingProvider();
            objSettingProvider.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.EnableDasboardHelp, "false", GetUsername, GetPortalID.ToString());
            divDashboardHelp.Visible = false;
            ShowMessage("", GetSageMessage("DashboardHelp", "Cancelled"), "", SageMessageType.Success);
        }
        catch (Exception exec)
        {
            ProcessException(exec);
        }
    }

    public void Specials()
    {
        string parentUrl = GetHostURL();
        StringBuilder html = new StringBuilder();
        html.Append("<a href='");
        html.Append("parentUrl");
        html.Append("/Admin/Roles.aspx'>Dashboard Permission</a>");
        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Admin/Pages.aspx'>Module Loader</a>");
        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Module-Maker.aspx'>Module Maker</a>");
        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Admin/Roles.aspx'>Page Role settings</a>");
        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Admin/SQL.aspx'>Database Backup</a>");
        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Admin/SQL.aspx'>Clean script</a>");

        html.Append("<a href='");
        html.Append(parentUrl);
        html.Append("/Admin/Users.aspx'>Suspended User</a>");

        lblSpecials.Text = html.ToString();
    }
}
