using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AspxCommerce_AspxImportManager_DataFileUploadHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string retMsg = "fail";
        int retStatus = -1;
        string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
        int strSize = HttpContext.Current.Request.Files[0].ContentLength;
        string strBaseLocation = HttpContext.Current.Server.MapPath("~/Modules/AspxCommerce/AspxImportManager/Uploads/");
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
                                           HttpContext.Current.Response.ContentType = "text/plain";
        HttpContext.Current.Response.Write("({ 'Status': '" + retStatus + "','Message': '" + retMsg + "' })");
        HttpContext.Current.Response.End();

    }
}
