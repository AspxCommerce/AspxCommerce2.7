<%@ WebHandler Language="C#" Class="Handler" %>
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;
using System.IO;
using AspxCommerce.Core;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Random rnd = new Random();
        string path = "~/Upload/temp/";
        String filename = HttpContext.Current.Request.Headers["X-File-Name"];
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        // HttpContext.Current.Request.Headers["aspxCommonInfo"]

        if (HttpContext.Current.Request.QueryString["StoreID"] != null)
        {

            aspxCommonObj.StoreID = int.Parse((HttpContext.Current.Request.QueryString["StoreID"].ToString()));
            aspxCommonObj.PortalID = int.Parse((HttpContext.Current.Request.QueryString["PortalID"].ToString()));
            aspxCommonObj.CultureName = (HttpContext.Current.Request.QueryString["CultureName"].ToString());
            aspxCommonObj.UserName = (HttpContext.Current.Request.QueryString["UserName"].ToString());
        }
        if (string.IsNullOrEmpty(filename) && HttpContext.Current.Request.Files.Count <= 0)
        {
            context.Response.Write("{success:false}");
        }
        else
        {
            string mapPath = HttpContext.Current.Server.MapPath(path);
            if (Directory.Exists(mapPath) == false)
            {
                Directory.CreateDirectory(mapPath);
            }
            if (filename == null)
            {
                //This work for IE
                try
                {
                    HttpPostedFile uploadedfile = context.Request.Files[0];
                    filename = uploadedfile.FileName;
                    string strExtension = Path.GetExtension(filename).ToLower();
                    filename = filename.Substring(0, filename.Length - strExtension.Length);
                    filename = filename + '_' + rnd.Next(111111, 999999).ToString() + strExtension;
                     
                    uploadedfile.SaveAs(mapPath + "\\" + filename);
                    //FileStream fs = new FileStream(mapPath + "\\" + filename, FileMode.Open);

                    //AspxImageManagerController.AddWaterMarksItem(fs, path, filename,
                                //  aspxCommonObj);
                 
                    mapPath = mapPath.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                    mapPath = mapPath.Replace("\\", "/");
                    mapPath = mapPath.Replace("%", "-");
                    mapPath = mapPath.Replace("#", "-");
                    context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + mapPath + filename + "\"}");
                }
                catch (Exception)
                {
                    context.Response.Write("{success:false}");

                }
            }
            else
            {
                //This work for Firefox and Chrome.
                filename = filename.Replace("%20", " ");
                string strExtension = Path.GetExtension(filename).ToLower();
                filename = filename.Substring(0, filename.Length - strExtension.Length);
                filename = filename + '_' + rnd.Next(111111, 999999).ToString() + strExtension;
                FileStream fileStream = new FileStream(mapPath + "\\" + filename, FileMode.OpenOrCreate);

                try
                {
                    Stream inputStream = HttpContext.Current.Request.InputStream;
                    CopyStream(inputStream, fileStream);

              //  AspxImageManagerController.AddWaterMarksItem(fileStream, path, filename,
                                  //aspxCommonObj);
             
                    mapPath = mapPath.Replace(HttpContext.Current.Server.MapPath("~/"), "");
                    mapPath = mapPath.Replace("\\", "/");
                    mapPath = mapPath.Replace("%", "-");
                    mapPath = mapPath.Replace("#", "-");
                    context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + mapPath + filename + "\"}");

                }
                catch (Exception)
                {
                    context.Response.Write("{success:false}");
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }
    }  

    public static long CopyStream(Stream source, Stream target)
    {
        const int bufSize = 0x1000;

        byte[] buf = new byte[bufSize];

        long totalBytes = 0;

        int bytesRead = 0;

        while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
        {
            target.Write(buf, 0, bytesRead);

            totalBytes += bytesRead;
        }
        return totalBytes;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}