#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
#endregion

public partial class Modules_SystemEvent_middle : SageFrameStartUpControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("./SageFrame/sf/sflogin" + SageFrameSettingKeys.PageExtension);
    }
}
