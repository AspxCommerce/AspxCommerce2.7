<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using SageFrame.Framework;
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
            string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            if (SageFrame.Web.PictureManager.ValidImageExtension(strFileName))
            {
                string funcNum = HttpContext.Current.Request.QueryString["CKEditorFuncNum"] as string;
                string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                string strBaseLocation = HttpContext.Current.Server.MapPath("~/Upload/images/");
                string fileName = GetUniqueFileName(strBaseLocation, strFileName);
                string strSaveLocation = strBaseLocation + fileName;
                object obj = new object();
                lock (obj)
                {
                    if (!Directory.Exists(strBaseLocation))
                    {
                        Directory.CreateDirectory(strBaseLocation);
                    }

                    if (!Directory.Exists(strBaseLocation + "thumb/"))
                    {
                        Directory.CreateDirectory(strBaseLocation + "thumb/");
                    }

                }

                HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);
                Image image = Image.FromFile(strSaveLocation);
                Image thumbImg = image.GetThumbnailImage(125, 100, null, new IntPtr());
                if (File.Exists(strBaseLocation + @"\thumb\" + fileName)) File.Delete(strBaseLocation + @"\thumb\" + fileName);
                thumbImg.Save(strBaseLocation + @"\thumb\" + fileName);
                strSaveLocation = strSaveLocation.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                strSaveLocation = strSaveLocation.Replace("\\", "/");
                string appPath = HttpContext.Current.Request.ApplicationPath.Equals("/") ? "" : HttpContext.Current.Request.ApplicationPath;
                HttpContext.Current.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + funcNum + ", '" + appPath + "'+'/Upload/images/" + fileName + "', 'Uploaded successfully'); self.close();</script>");
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


    public string GetUniqueFileName(string strBaseLocation, string fileName)
    {
        int i = 1;
        string FileName = fileName.Substring(0, fileName.LastIndexOf(@"."));
        string ext = fileName.Substring(fileName.LastIndexOf(@"."));
        string newFileName = FileName + ext;
        bool flag = false;
        while (!flag)
        {
            if (!File.Exists(strBaseLocation + newFileName)) break;
            newFileName = FileName + i + ext;
            i++;
        }

        return newFileName;
    }
}