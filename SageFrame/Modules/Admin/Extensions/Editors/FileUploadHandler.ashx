<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.IO;
public class FileUploadHandler : IHttpHandler {
    public void ProcessRequest(HttpContext context)
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
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}