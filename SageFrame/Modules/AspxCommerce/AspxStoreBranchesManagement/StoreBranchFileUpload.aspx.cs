using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Modules_AspxCommerce_AspxStoreBranchesManagement_StoreBranchFileUpload : System.Web.UI.Page
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
        string strBaseLocation = HttpContext.Current.Server.MapPath("~/Modules/AspxCommerce/AspxStoreBranchesManagement/uploads/");
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
