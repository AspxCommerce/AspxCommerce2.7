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

public partial class Modules_SocialShare_SocialShareView :SageFrameStartUpControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss("SocialLinkcss", "Modules/SocialShare/css/SocialLink.css");
    }
}