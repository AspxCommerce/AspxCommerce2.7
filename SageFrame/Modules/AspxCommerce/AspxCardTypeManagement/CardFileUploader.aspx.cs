/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Modules_AspxCommerce_AspxCardTypeManagement_CardFileUploader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string retMsg = "fail";
        int maxFileSize = 0;
        int retStatus = -1;
        Random rnd = new Random();
        if (Request.Form["MaxFileSize"] != null && Request.Form["MaxFileSize"] != "" && int.Parse(Request.Form["MaxFileSize"].ToString()) > 0)
        {
            maxFileSize = int.Parse(Request.Form["MaxFileSize"].ToString());
        }
        string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
        string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
        strFileName = strFileName.Substring(0, strFileName.Length - strExtension.Length);
        strFileName = strFileName + '_' + rnd.Next(111111, 999999).ToString() + strExtension;

        int strSize = HttpContext.Current.Request.Files[0].ContentLength;
        string strBaseLocation = HttpContext.Current.Server.MapPath("~/Upload/temp/");
        if ((strSize > 0) && (strSize < maxFileSize * 1024))
        {
            if (!Directory.Exists(strBaseLocation))
            {
                Directory.CreateDirectory(strBaseLocation);
            }
            string strSaveLocation = strBaseLocation + strFileName;
            HttpContext.Current.Request.Files[0].SaveAs(strSaveLocation);
            strSaveLocation = strSaveLocation.Replace(HttpContext.Current.Server.MapPath("~/"), "");
            strSaveLocation = strSaveLocation.Replace("\\", "/");
            retMsg = strSaveLocation;
            retStatus = 1;
        }
        else
        {
            retMsg = "Sorry, the image must be less than " + maxFileSize + "KB";
        }
        HttpContext.Current.Response.ContentType = "text/plain";
        HttpContext.Current.Response.Write("({ 'Status': '" + retStatus + "','Message': '" + retMsg + "' })");
        HttpContext.Current.Response.End();
    }
}
