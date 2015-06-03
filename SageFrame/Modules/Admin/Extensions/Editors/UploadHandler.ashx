<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using SageFrame.Services;
public class UploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int userModuleID = int.Parse(HttpContext.Current.Request.QueryString["userModuleId"].ToString());
        int portalID = int.Parse(HttpContext.Current.Request.QueryString["portalID"].ToString());
        string userName = HttpContext.Current.Request.QueryString["userName"].ToString();
        string secureToken = HttpContext.Current.Request.QueryString["sageFrameSecureToken"].ToString();
        AuthenticateService objAuthentication = new AuthenticateService();
        if (objAuthentication.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {

            int retStatus = 0;
            string retMsg = string.Empty;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                try
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream, true, true);
                }
                catch (Exception)
                {
                    retMsg = "LargeImagePixel";
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(retMsg);
                    return;
                }
                string s = HttpContext.Current.Request.Form["folderPath"] as string;
                string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                string strBaseLocation = HttpContext.Current.Server.MapPath("~/Resources/temp/NewPackage/" + s + "/");
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
                HttpContext.Current.Response.Write("({ 'Status': '" + retStatus + "','Message': '" + retMsg + "' })");
                HttpContext.Current.Response.End();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}