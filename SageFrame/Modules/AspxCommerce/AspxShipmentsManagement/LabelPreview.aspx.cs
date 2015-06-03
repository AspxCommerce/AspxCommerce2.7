using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Framework;

public partial class Modules_AspxCommerce_AspxShipmentsManagement_LabelPreview : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isFile = bool.Parse(Request.QueryString["isfile"]);
        if (isFile)
        {
            if (Request.QueryString["path"] != null)
            {
                string path = Request.QueryString["path"].ToString();
                try
                {
                    if (!string.IsNullOrEmpty(path))
                        DisplayFile(path);
                    else
                    {
                        throw new Exception("please input valid data!");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }
        else
        {
            try
            {
                int orderId = int.Parse(Request.QueryString["oid"]);
                int storeId = int.Parse(Request.QueryString["sid"]);
                int portalId = int.Parse(Request.QueryString["pid"]);
                GetLabelDetail(orderId, storeId, portalId);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }

    private void GetLabelDetail(int orderId, int storeId, int portalId)
    {
        try
        {
            AspxShipProviderMgntController ctl = new AspxShipProviderMgntController();
            AspxCommonInfo commonObj = new AspxCommonInfo();
            commonObj.StoreID = storeId;
            commonObj.PortalID = portalId;
            ShippingLabelInfo detail = ctl.GetShippingLabelInfo(orderId, commonObj);
            DisplayFile(detail);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void DisplayDetail(ShippingLabelInfo info)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<table>");
        builder.Append("<tr><td>Shipping Label ID:</td><td>");
        builder.Append(info.ShippingLabelID);
        builder.Append("</td><tr/>");
        builder.Append("<tr><td>Barcode No:</td><td>");
        builder.Append(info.BarcodeNo);
        builder.Append("</td><tr/>");
        builder.Append("<tr><td>Tracking No:</td><td>");
        builder.Append(info.TrackingNo);
        builder.Append("<td></tr>");
        builder.Append("</table>");
        ltLabelDetail.Text = builder.ToString();
    }

    private void DisplayFile(ShippingLabelInfo info)
    {
        string strExtenstion = info.Extension.ToLower();
        Response.Clear();
        Response.Buffer = true;
        if (strExtenstion == "doc" || strExtenstion == "docx")
        {
            Response.ContentType = "application/vnd.ms-word";
                       Response.AddHeader("content-disposition", "inline;filename=_labelPreview.doc");
        }
        else if (strExtenstion == "xls" || strExtenstion == "xlsx")
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "inline;filename=_labelPreview.xls");
        }
        else if (strExtenstion == "pdf")
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;filename=_labelPreview.pdf");
        }
        else if (strExtenstion == "tif")
        {
            Response.ContentType = "image/tiff";
            Response.AddHeader("content-disposition", "inline;filename=_labelPreview.tif");
        }
        else if (strExtenstion == "jpeg" || strExtenstion == "jpg")
        {
            Response.ContentType = "image/jpeg";
            Response.AddHeader("content-disposition", "inline;filename=_labelPreview.jpg");
        }
        else if (strExtenstion == "gif")
        {
            Response.ContentType = "image/gif";
            Response.AddHeader("content-disposition", "inline;filename=_labelPreview.gif");
        }
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
       
        Response.BinaryWrite(Convert.FromBase64String(info.ShippingLabelImage));
        Response.End();
    }

    private void DisplayFile(string path)
    {
        string strExtenstion = Path.GetExtension(path).ToLower().Trim();
        string fileName = Path.GetFileNameWithoutExtension(path);
        string file = HttpContext.Current.Server.MapPath("~/" + path);
       
        FileStream fs = File.OpenRead(file);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
        fs.Close();
        fs.Close();

        Response.Clear();
        Response.Buffer = true;
        if (strExtenstion == ".doc" || strExtenstion == ".docx")
        {
            Response.ContentType = "application/vnd.ms-word";
                       Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".doc");
        }
        else if (strExtenstion == ".xls" || strExtenstion == ".xlsx")
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".xls");
        }
        else if (strExtenstion == ".pdf")
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".pdf");
        }
        else if (strExtenstion == ".tif")
        {
            Response.ContentType = "image/tiff";
            Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".tif");
        }
        else if (strExtenstion == ".jpeg" || strExtenstion == ".jpg")
        {
            Response.ContentType = "image/jpeg";
            Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".jpg");
        }
        else if (strExtenstion == ".gif")
        {
            Response.ContentType = "image/gif";
            Response.AddHeader("content-disposition", "inline;filename=" + fileName + ".gif");
        }
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
               Response.BinaryWrite(bytes);//byte
        Response.End();
    }
}
