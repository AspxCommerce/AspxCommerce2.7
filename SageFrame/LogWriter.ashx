<%@ WebHandler Language="C#" Class="Reporter" %>

using System;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Text.RegularExpressions;
public class Reporter : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string path = HostingEnvironment.ApplicationPhysicalPath + "backUpLog.txt";
        string content = "";
        if (File.Exists(path))
        {
            using (StreamReader sw = new StreamReader(path))
            {
                content = sw.ReadToEnd();
                sw.Dispose();                
            }
            content = Regex.Replace(content, "\r\n", "<br>");
        }        
        context.Response.ContentType = "text/plain";
        context.Response.Write(content);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}