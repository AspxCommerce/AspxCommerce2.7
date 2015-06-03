<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.IO;
using System.Drawing;
using SageFrame.Services;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
         int userModuleID = int.Parse(HttpContext.Current.Request.QueryString["userModuleId"].ToString());
        int portalID = int.Parse(HttpContext.Current.Request.QueryString["portalID"].ToString());
        string userName = HttpContext.Current.Request.QueryString["userName"].ToString();
        string secureToken = HttpContext.Current.Request.QueryString["secureToken"].ToString();
        AuthenticateService objAuthentication = new AuthenticateService();
        if (objAuthentication.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string strFileName = Path.GetFileName(HttpContext.Current.Request.Files[0].FileName);
            if (SageFrame.Web.PictureManager.ValidImageExtension(strFileName))
            {

                HttpRequest Request = HttpContext.Current.Request;
                string Imagepath = Request.ApplicationPath + "/Modules/FileManager/Library/";
                string RealPath = HttpContext.Current.Server.MapPath("~/");

                int mode = Convert.ToInt32(context.Request.QueryString["mode"]);
                string fileName = context.Request.QueryString["fn"];
                string Editpath = Path.Combine(RealPath, "Modules\\FileManager\\Library\\");
                int height = 0, width = 0, theight = 0, twidth = 0;
                int x1 = 0, y1 = 0;
                string path = "Library//";
                Random rnd = new Random();
                //string fileName = rnd.Next(11111, 99999).ToString() + ".jpeg";
                string saveLocation = HttpContext.Current.Server.MapPath(path);
                if (mode == 0)
                {
                    HttpContext.Current.Request.SaveAs(saveLocation + fileName, false);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(Imagepath + fileName);
                }
                else
                {
                    Stream str = context.Request.InputStream;
                    Image img;
                    if (str.Length != 0)
                    {
                        img = Image.FromStream(str);
                    }
                    else
                    {
                        img = Image.FromFile(Editpath + fileName);
                        string FName = fileName;
                        int index = FName.IndexOf('_') + 1;
                        string realfilename = FName.Substring(index, FName.Length - index);
                        fileName = rnd.Next(11111, 99999).ToString() + "_" + realfilename;
                    }
                    int Pwidth = Convert.ToInt32(img.PhysicalDimension.Width);
                    int Pheight = Convert.ToInt32(img.PhysicalDimension.Height);
                    string wid = context.Request.QueryString["wid"];
                    string hei = context.Request.QueryString["he"];
                    if (wid != "" || hei != "")
                    {
                        if (wid != "")
                        {
                            width = Convert.ToInt32(wid);
                            if (Pwidth < width)
                            {
                                width = Pwidth;
                            }
                        }
                        if (hei != "")
                        {
                            height = Convert.ToInt32(hei);
                            if (Pheight < height)
                            {
                                height = Pheight;
                            }
                        }
                    }
                    else
                    {
                        width = Pwidth;
                        height = Pheight;
                    }
                    if (mode == 2)
                    {
                        x1 = Convert.ToInt32(context.Request.QueryString["x1"]);
                        y1 = Convert.ToInt32(context.Request.QueryString["y1"]);
                        twidth = Convert.ToInt32(context.Request.QueryString["twid"]);
                        theight = Convert.ToInt32(context.Request.QueryString["the"]);
                    }
                    Bitmap bmp = new Bitmap(img, width, height);
                    //check height and width 
                    if (mode == 2)
                    {
                        Bitmap rBmp = new Bitmap(twidth, theight);
                        bmp.SetResolution(80, 60);
                        Graphics g = Graphics.FromImage(rBmp);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawImage(bmp, new Rectangle(0, 0, twidth, theight), x1, y1, twidth, theight, GraphicsUnit.Pixel);
                        img.Dispose();
                        bmp.Dispose();
                        g.Dispose();
                        rBmp.Save(saveLocation + "Thumbs/" + fileName);
                    }
                    if (Directory.Exists(saveLocation) == false)
                    {
                        Directory.CreateDirectory(saveLocation);
                        if (mode == 1)
                        {
                            bmp.Save(saveLocation + fileName);
                        }
                    }
                    else
                    {
                        if (mode == 1)
                        {
                            bmp.Save(saveLocation + fileName);
                        }
                    }
                    if (mode == 1)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.Write(Imagepath + fileName);
                    }
                    if (mode == 2)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.Write(Imagepath + "Thumbs/" + fileName);
                    }
                }
            }
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