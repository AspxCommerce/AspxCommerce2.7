 <%@ WebHandler Language="C#" Class="DownloadHandler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using SageFrame.FileManager;
using AspxCommerce.Core;
using System.Text;
using System.IO;
public class DownloadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request != null)
        {
            string action = context.Request.QueryString["action"] != null ? context.Request.QueryString["action"].ToString() : string.Empty;
            string path = context.Request.QueryString["path"] != null ? context.Request.QueryString["path"].ToString() : string.Empty;
            if (action != string.Empty && path != string.Empty)
            {
                FileInfo file = new FileInfo(GetAbsolutePath(path));
                if (file.Exists)
                {
                    // call appropriate function
                    switch (action)
                    {
                        case "download":
                            try
                            {
                               string fileName = file.Name.Replace(' ', '_');
                               // Clear the content of the response
                                context.Response.ClearContent();

                               // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
                                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                               // Add the file size into the response header
                               // context.Response.AddHeader("Content-Length", file.Length.ToString());

                               // Set the ContentType
                                context.Response.ContentType = FileManagerHelper.ReturnExtension(file.Extension.ToLower());

                               // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                                context.Response.TransmitFile(file.FullName);   
                            }
                            catch (Exception ex)
                            {
                                context.Response.ContentType = "text/plain";
                                context.Response.Write(ex.Message);
                            }
                            finally
                            {
                                context.Response.End();
                            }
                            break;
                        case "info":
                            get_info(context, file);
                            break;
                        default:
                            //alert("Error!");
                            break;
                    }
                }
            }
          
        }
    }
    public void get_info(HttpContext context, FileInfo file_path)
    {
        handleRequest(context, file_path);

        //var data = array(
        //    "filename" => $filename,
        //    "filetype" => $filetype,
        //    "filesize" => $filesize,
        //);        			
        //return itemDwnlDetails;
        //{"filename":"hubble2.jpg","filetype":"image\/jpg","filesize":148}
        //{"FileName":"image1","FileType":"image/jpeg","FileSize":61}
    }

    public void handleRequest(HttpContext context, FileInfo file_path)
    {
        // get file info
        string filename = file_path.Name;
        string filetype = FileManagerHelper.ReturnExtension(Path.GetExtension(file_path.Name));
        decimal filesize = Decimal.Round(file_path.Length / 1024, 2); // file size in KB
        //writeRaw("this is a message");
        writeJson(context, new DownloadItemAttribInfo() { FileName = filename.ToString(), FileType = filetype.ToString(), FileSize = filesize });
    }

    public void writeJson(HttpContext context, object _object)
    {
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        string jsondata = javaScriptSerializer.Serialize(_object);
        writeRaw(context, jsondata);
    }

    public void writeRaw(HttpContext context, string text)
    {
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;
        context.Response.Write(text);
    }
    public static string GetAbsolutePath(string filepath)
    {

        string str = HttpContext.Current.Request.PhysicalPath.ToString();
        string subStr = str.Substring(0, str.LastIndexOf("\\"));

        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(subStr, filepath)));

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }    

}