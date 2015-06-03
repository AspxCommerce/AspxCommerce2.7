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
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using AspxCommerce.Core;

public partial class Modules_ASPXCommerce_ASPXItemsManagement_ItemCostVariantsFileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string retMsg = "fail";
        int maxFileSize = 0;
        int retStatus = -1;
        string oldFile = "";
        Random rnd = new Random();
        AspxCommonInfo aspxCommonObj =new AspxCommonInfo();
      
        const string path = "~/Modules/AspxCommerce/AspxItemsManagement/uploads/";

        if (Request.Form["DeleteImage"] != null)
        {
            if (Request.Form["OldFileName"] != null)
            {
                try
                {
               
                oldFile = Request.Form["OldFileName"].ToString();
                oldFile = Path.GetFileName(oldFile);
                string largImage = path + "Large/" + oldFile;
                string midImage = path + "Medium/" + oldFile;
                string smallImage = path + "Small/" + oldFile;
                string baseImage = path + oldFile;
                    Thread.Sleep(2000);
                if (File.Exists(HttpContext.Current.Server.MapPath(largImage)))
                    File.Delete(HttpContext.Current.Server.MapPath(largImage));
                if (File.Exists(HttpContext.Current.Server.MapPath(midImage)))
                    File.Delete(HttpContext.Current.Server.MapPath(midImage));
                if (File.Exists(HttpContext.Current.Server.MapPath(smallImage)))
                    File.Delete(HttpContext.Current.Server.MapPath(smallImage));
                if (File.Exists(HttpContext.Current.Server.MapPath(baseImage)))
                    File.Delete(HttpContext.Current.Server.MapPath(baseImage));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }
        else
        {

            if (Request.Form["MaxFileSize"] != null && Request.Form["MaxFileSize"] != "" &&
                int.Parse(Request.Form["MaxFileSize"].ToString()) > 0)
            {
                maxFileSize = int.Parse(Request.Form["MaxFileSize"].ToString());
            }
            if (Request.Form["StoreID"] != null)
            {
                aspxCommonObj.StoreID = int.Parse(Request.Form["StoreID"].ToString());
                aspxCommonObj.PortalID = int.Parse(Request.Form["PortalID"].ToString());
                aspxCommonObj.CultureName = Request.Form["CultureName"].ToString();
            }
            string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
                                             int strSize = HttpContext.Current.Request.Files[0].ContentLength;
         
            string mapPath = HttpContext.Current.Server.MapPath(path);
            string tempPath = HttpContext.Current.Server.MapPath("~/Upload/temp/");
            if ((strSize > 0) && (strSize < maxFileSize*1024))
            {

                HttpPostedFile uploadedfile = HttpContext.Current.Request.Files[0];
                string filename = uploadedfile.FileName;
                string strExtension = Path.GetExtension(filename).ToLower();
                filename = filename.Substring(0, filename.Length - strExtension.Length);
                filename = filename + '_' + rnd.Next(111111, 999999).ToString() + strExtension;

                uploadedfile.SaveAs(tempPath + "\\" + filename);
                FileStream fs = new FileStream(tempPath + "\\" + filename, FileMode.Open);
                AspxImageManagerController.AddWaterMarksCostVariantItem(fs, path, filename,
                                                                        aspxCommonObj);
                retMsg = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + filename;
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
}
