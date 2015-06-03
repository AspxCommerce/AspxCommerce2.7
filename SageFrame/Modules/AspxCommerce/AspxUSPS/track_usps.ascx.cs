using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using AspxCommerce.USPS;

public partial class Modules_AspxCommerce_AspxUSPS_track_usps : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        try
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            var radiobuttonList = (RadioButtonList)Parent.FindControl("rblProviderList");
            var providerId = int.Parse(radiobuttonList.SelectedValue);
            var usps = new USPS();
            string trackingNo = txtTrackingNo.Text.Trim().Replace(" ", "");
            txtTrackingNo.Text = trackingNo;
            AspxCommonInfo commonObj = new AspxCommonInfo();
            commonObj.StoreID = GetStoreID;
            commonObj.PortalID = GetPortalID;

            var response = usps.GetTrackingInfo(trackingNo, providerId, commonObj);
            if (!response.IsFailed)
            {
                lblError.Text = "";
                ShowResult(response);

            }
            else
            {
                dvTrackResponse.InnerHtml = "";
                ShowError(response.Error);
                ShowMessage(SageMessageTitle.Exception.ToString(), response.Error, response.Error, SageMessageType.Error);


            }
                   }
        catch (Exception ex)
        {
            ShowError(ex.Message);
            ProcessException(ex);
        }
    }
    private void ShowError(string error)
    {
        lblError.Text = error;
    }

    private void ShowResult(TrackingInfo info)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<h2>Result </h2><div>");
        builder.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">");
        builder.Append("<tr><td>");
        builder.Append("Tracking Number");
        builder.Append("</td><td>");
        builder.Append(info.TrackingNumber);
        builder.Append("</td></tr>");
        builder.Append("<tr><td>");
        builder.Append("Summary");
        builder.Append("</td><td>");
        builder.Append(info.Summary);
        builder.Append("</td></tr>");
        builder.Append("<tr><td>");
        builder.Append("Details");
        builder.Append("</td><td><div>");
        int i = 0;
        foreach (var detail in info.Details)
        {
            builder.Append("<p>" + detail + "</p>");
            i++;
        }
        if (info.Details.Count == 0)
        {
            builder.Append("<p>N/A</p>");
        }
        builder.Append("</div></td></tr>");
        builder.Append("</table></div>");
        dvTrackResponse.InnerHtml = builder.ToString();
    }
}
