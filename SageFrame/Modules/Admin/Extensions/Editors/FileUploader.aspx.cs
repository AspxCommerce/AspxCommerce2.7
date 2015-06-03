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

using System.IO;
using SageFrame.Web;
using SageFrame.Framework;
public partial class Modules_Admin_Extensions_Editors_FileUploader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CheckAuthentication())
        {
            int retStatus = 0;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                string s = HttpContext.Current.Request.Form["folderPath"] as string;
                string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                string strBaseLocation = HttpContext.Current.Server.MapPath("~/Resources/");
                string strSaveLocation = strBaseLocation + strFileName;
                object obj = new object();
                lock (obj)
                {

                    if (!Directory.Exists(strBaseLocation))
                    {

                        Directory.CreateDirectory(strBaseLocation);

                    }
                }
                if (!File.Exists(strSaveLocation))
                {
                    HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);
                }
                else
                {
                    retStatus = 1;
                }

                strSaveLocation = strSaveLocation.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                strSaveLocation = strSaveLocation.Replace("\\", "/");
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write("({ 'Status': '" + retStatus + "','Message': '' })");
                HttpContext.Current.Response.End();
            }
        }

    }
    private bool CheckAuthentication()
    {
        BaseAdministrationUserControl obj = new BaseAdministrationUserControl();
        int userModuleId = int.Parse(Request.QueryString["userModuleId"].ToString());
        SageFrame.Services.AuthenticateService objAuthentication = new SageFrame.Services.AuthenticateService();
        PageBase objPageBase = new PageBase();
        if (objAuthentication.IsPostAuthenticatedView(obj.GetPortalID, userModuleId, obj.GetUsername, objPageBase.SageFrameSecureToken))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
