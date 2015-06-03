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
using SageFrame.FileManager;
using SageFrame.Framework;
using SageFrame.Web;
#endregion

public partial class Modules_FileManager_FileUploadHandler : System.Web.UI.Page
{
    FileManagerBase fb = new FileManagerBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CheckAuthentication())
        {
            string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            string url = Request.Url.ToString();
            int folderId = int.Parse(Session[SageFrame.Common.SessionKeys.FolderID].ToString());
            string strBaseLocation = string.Empty;
            if (Session[SageFrame.Common.SessionKeys.Path] != null)
                strBaseLocation = Session[SageFrame.Common.SessionKeys.Path].ToString();
            string absolutePath = GetAbsolutePath(strBaseLocation);
            try
            {                
                //try
                //{
                //    System.Drawing.Image img = System.Drawing.Image.FromStream(HttpContext.Current.Request.Files[0].InputStream, true, true);
                //}
                //catch (Exception)
                //{
                //    //retMsg = "LargeImagePixel";
                //    //context.Response.ContentType = "text/plain";
                //    //context.Response.Write(retMsg);
                //    return;
                //}
                string strSaveLocation = absolutePath + strFileName;
                if (!File.Exists(strSaveLocation))
                {
                    HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);
                    File.SetAttributes(strSaveLocation, FileAttributes.Archive);
                }
                CacheHelper.Clear("FileManagerFileList");
            }
            catch (Exception ex)
            {
                fb.ProcessException(ex);
            }
        }
    }
    public static string GetAbsolutePath(string filepath)
    {
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
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
