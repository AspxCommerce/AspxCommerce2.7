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
using System.Web.UI.HtmlControls;
using SageFrame.Web;
using SageFrame.Dashboard;
using SageFrame.Framework;
using System.Data;
#endregion

namespace SageFrame.Modules.DashBoard
{
    public partial class DashBoard : BaseAdministrationUserControl
    {
        public string Extension;
        protected void Page_Load(object sender, EventArgs e)
        {
            Extension = SageFrameSettingKeys.PageExtension;

            SageFrameConfig sfConf = new SageFrameConfig();
            string PortalLogoTemplate = sfConf.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalLogoTemplate);
            if (SageFrameSettingKeys.PortalLogoTemplate.ToString() != string.Empty)
            {
                lblSfInfo.Text = PortalLogoTemplate.ToString();
            }
            if (!Page.IsPostBack)
            {
                DashBoardView();
            }
        }
        protected void imbAdmin_Click(object sender, ImageClickEventArgs e)
        {
        }
        private void DashBoardView()
        {
            try
            {
                string PageSEOName = string.Empty;
                if (Request.QueryString["pgnm"] != null)
                {
                    PageSEOName = Request.QueryString["pgnm"].ToString();
                }
                else
                {
                    PageBase pb = new PageBase();
                    SageUserControl SageUser = new SageUserControl();
                    PageSEOName = pb.GetPageSEOName(SageUser.PagePath);
                }
                DashboardController objController = new DashboardController();
                List<DashboardInfo> lstDashboard = objController.DashBoardView(PageSEOName, GetUsername, GetPortalID);
                lstDashboard.ForEach(
                    delegate(DashboardInfo obj)
                    {
                        if (obj.IconFile != null && obj.IconFile != string.Empty)
                        {
                            string iconFile = string.Empty;
                            iconFile = string.Format("{0}/PageImages/{1}", Request.ApplicationPath == "/" ? "" : Request.ApplicationPath, obj.IconFile);
                            iconFile = "<img align='middle' style='border-width:0px;' src='" + iconFile + "' class='sfImageheight' id='ctl17_rptDashBoard_ctl17_imgDisplayImage'>";
                            obj.IconFile = iconFile;
                        }
                        else
                        {
                            obj.IconFile = "<i class='icon-" + obj.PageName.Replace(" ", "-").ToLower() + "'></i>";

                        }
                        obj.Url = obj.Url + Extension;
                    }
                    );
                rptDashBoard.DataSource = lstDashboard;
                rptDashBoard.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void rptDashBoard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        #region SageFrameRoute Members

        #endregion
    }
}
