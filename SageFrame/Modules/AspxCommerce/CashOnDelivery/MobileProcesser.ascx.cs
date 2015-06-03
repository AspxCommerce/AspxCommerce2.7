using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;


public partial class Modules_Mobile_Processer : AspxMobileCheckOutControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StartProccess();
    }
    private void StartProccess()
    {
        try
        {

            var sfConfig = new SageFrameConfig();
           bool _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            string sageRedirectPath;
            if (_isUseFriendlyUrls)
            {
                if (!IsParent)
                {
                    sageRedirectPath = ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + "Cash-On-Delivery-Success.aspx");
                }
                else
                {
                    sageRedirectPath = ResolveUrl("~/" + "Cash-On-Delivery-Success.aspx");
                 }
            }
            else
            {
                sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + "Cash-On-Delivery-Success");
            }

                                 Response.Redirect(sageRedirectPath);



        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
