#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
#endregion

public partial class Modules_Admin_SageFrameInfo_SageFrameInfoView : BaseAdministrationUserControl
{
    public string appPath = "";
    public bool LoadLiveFeed = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        appPath = GetApplicationName;
        SageFrameConfig pagebase = new SageFrameConfig();
        LoadLiveFeed = bool.Parse(pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.EnableLiveFeeds));
        IncludeJs("SageFrameInfo", "/js/jquery.jgfeed-min.js");
        IncludeCss("SageFrameInfo", "/Modules/Admin/SageFrameInfo/module.css");
    }
}
