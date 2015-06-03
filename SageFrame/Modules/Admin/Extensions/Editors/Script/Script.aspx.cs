﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.FileManager;
using System.IO;
using System.Web.Hosting;

public partial class Modules_Admin_Editors_Script_Script : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string dir;
        var fileName = "";
        HttpContext c = HttpContext.Current;
        if (c != null)
        {
            if (c.Request.Form["dir"] == null || c.Request.Form["dir"].Length <= 0)
                dir = "/";
            else
                dir = c.Server.UrlDecode(c.Request.Form["dir"]);

            string rootPath = string.Format("{0}{1}", c.Request.PhysicalApplicationPath.ToString(), dir);
            c.Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");

            if (dir.Equals("/Templates/"))
            {
                if (Directory.Exists(rootPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(rootPath);
                    foreach (DirectoryInfo tempdir in dirInfo.GetDirectories())
                    {
                        fileName = tempdir.FullName.Substring(tempdir.FullName.IndexOf("\\Modules\\") + 8);
                        Response.Write("\t<li class=\"directory collapsed\"><a id=" + fileName + " href=\"#\" rel=\"" + tempdir.FullName + "/\">" + tempdir.Name + "</a></li>\n");
                    }
                    DirectoryInfo difDefaultTemplate = new DirectoryInfo(string.Format("{0}{1}", c.Request.PhysicalApplicationPath.ToString(), "/Core/"));
                    foreach (DirectoryInfo tempdir in difDefaultTemplate.GetDirectories())
                    {
                        if (tempdir.Name.Equals("Template"))
                        {
                            fileName = tempdir.FullName.Substring(tempdir.FullName.IndexOf("\\Modules\\") + 8);
                            Response.Write("\t<li class=\"directory collapsed\"><a id=" + fileName + " href=\"#\" rel=\"" + tempdir.FullName + "/\">Default</a></li>\n");
                        }
                    }
                }
            }
            if (dir.Equals("/Modules/"))
            {
                if (Directory.Exists(rootPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(rootPath);
                    foreach (DirectoryInfo tempdir in dirInfo.GetDirectories())
                    {
                        fileName = tempdir.FullName.Substring(tempdir.FullName.IndexOf("\\Modules\\") + 8);
                        Response.Write("\t<li class=\"directory collapsed\"><a id=" + fileName + " href=\"#\" rel=\"" + tempdir.FullName + "/\">" + tempdir.Name + "</a></li>\n");
                    }
                }
            }
            if (dir.Equals("/"))
            {
                if (Directory.Exists(rootPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(rootPath);

                    foreach (DirectoryInfo tempdir in dirInfo.GetDirectories())
                    {
                        if (tempdir.Name != "Modules" && tempdir.Name != "Templates")
                        {
                            fileName = tempdir.FullName.Substring(tempdir.FullName.IndexOf("\\Modules\\") + 8);
                            Response.Write("\t<li class=\"directory collapsed\"><a  id=" + fileName + "  href=\"#\" rel=\"" + tempdir.FullName + "/\">" + tempdir.Name + "</a></li>\n");
                        }
                    }
                }
            }
            else
            {
                if (Directory.Exists(dir))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if (dirInfo.GetDirectories().Length > 0)
                    {
                        foreach (DirectoryInfo tempdir in dirInfo.GetDirectories())
                        {
                            fileName = tempdir.FullName.Substring(tempdir.FullName.IndexOf("\\Modules\\") + 8);
                            Response.Write("\t<li class=\"directory collapsed\"><a id=" + fileName + "  href=\"#\" rel=\"" + tempdir.FullName + "/\">" + tempdir.Name + "</a></li>\n");
                        }
                    }
                    else if (dirInfo.GetFiles().Length > 0)
                    {
                        foreach (FileInfo tempfile in dirInfo.GetFiles())
                        {
                            fileName = tempfile.FullName.Substring(tempfile.FullName.IndexOf("\\Modules\\") + 8);
                            Response.Write("\t<li class=\"collapsed\"><a id=" + fileName + "  href=\"#\" rel=\"" + tempfile.FullName + "/\">" + tempfile.Name + "</a></li>\n");
                        }
                    }
                   
                }
                
            }
            c.Response.Write("</ul>");
        }
    }
    public static string GetUrlPath(string path)
    {
        string relativePathInitial = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/";
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(relativePathInitial, path)));

    }
}
