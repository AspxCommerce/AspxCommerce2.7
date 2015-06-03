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
            string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            bool isValid = System.Text.RegularExpressions.Regex.IsMatch(strFileName.ToLower(), @"^.*\.(dll)$"); ;
            if(isValid)
            {
                string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                string strBaseLocation = HttpContext.Current.Server.MapPath("~/bin/");
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
                strSaveLocation = strSaveLocation.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                strSaveLocation = strSaveLocation.Replace("\\", "/");
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write("({ 'Message': '" + strSaveLocation + "' })");
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