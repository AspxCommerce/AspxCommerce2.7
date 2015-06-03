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
using System.IO;
using System.Web.Hosting;
using SageFrame.Scheduler;
using SageFrame.Web;
using SageFrame.Framework;
using System.Drawing;
#endregion

public partial class Modules_Scheduler_FileUploadHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (CheckAuthentication())
        {
            string s = HttpContext.Current.Request.Form["fileUpload"] as string;
            if (!string.IsNullOrEmpty(s))
            {
                string strSaveLocation = string.Empty;
                try
                {
                    Bitmap img = new Bitmap(HttpContext.Current.Request.Files[0].InputStream, false);
                }
                catch (Exception)
                {
                    strSaveLocation = "LargeImagePixel";
                    return;
                }
                string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                string strBaseLocation = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin\\"); //HttpContext.Current.Server.MapPath("~/bin/");
                if (!Directory.Exists(strBaseLocation))
                {
                    Directory.CreateDirectory(strBaseLocation);
                }
                strSaveLocation = strBaseLocation + strFileName;
                HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);
                //strSaveLocation = strSaveLocation.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                //strSaveLocation = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin\\"); //strSaveLocation.Replace("\\", "/");
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write("({ 'Message': '" + strSaveLocation + "' })");
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
