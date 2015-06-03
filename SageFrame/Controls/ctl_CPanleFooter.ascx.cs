/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;

public partial class Controls_ctl_CPanleFooter : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadHeadContent();
        }
    }

    private void LoadHeadContent()
    {
        try
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string strCPanleHeader = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalCopyright);
            litCPanlePortalCopyright.Text = strCPanleHeader;            
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}