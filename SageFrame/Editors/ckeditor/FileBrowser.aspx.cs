#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Hosting;
using System.IO;
using SageFrame.Web;
using SageFrame.Framework;

#endregion


public partial class FileBrowser : System.Web.UI.Page
{

    public List<ImageFile> ImageFiles { get; set; }

    private string physicalpath = HostingEnvironment.ApplicationVirtualPath.Trim().Equals("//") ? "/" : HostingEnvironment.ApplicationPhysicalPath + @"Upload\images";
    string path = "";//HostingEnvironment.ApplicationVirtualPath+
    string thumbpath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PageBase objPageBase = new PageBase();
        if (objPageBase.IsUserLoggedIn())
        {
            if (CheckAuthentication())
            {
                this.dirLabel.Text = @"Upload\images\";
                ImageFiles = new List<ImageFile>();
                string appPath = Request.ApplicationPath.Equals("/") ? "" : Request.ApplicationPath;
                path = appPath + @"/Upload/images";
                thumbpath = path + "/thumb";
                if (Directory.Exists(physicalpath))
                {
                    foreach (string fileName in Directory.GetFiles(physicalpath))
                    {
                        FileInfo fileInfo = new FileInfo(fileName);

                        ImageFiles.Add(new ImageFile { ThumbImageFileName = thumbpath + @"/" + fileInfo.Name, FileName = path + @"/" + fileInfo.Name, Size = (fileInfo.Length / 1024).ToString(), CreatedDate = fileInfo.LastAccessTime.ToString() });
                    }
                    ImageFiles = ImageFiles.OrderByDescending(x => DateTime.Parse(x.CreatedDate)).ToList();
                }
            }
        }
        else
        {
            //Response.WriteFile(":p");

            this.Page.Form.Visible = false;
            
        }
    }


    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string name = btn.ImageUrl;
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.fuImage.HasFile)
            {
                if (CheckAuthentication())
                {
                    string fileName = this.fuImage.FileName;
                    if (PictureManager.ValidImageExtension(fileName))
                    {
                        string strSaveLocation = Path.Combine(physicalpath, fileName);
                        string strExtension = Path.GetExtension(HttpContext.Current.Request.Files[0].FileName).ToLower();
                        SageFrameConfig config = new SageFrameConfig();
                        string extension = config.GetSettingValueByIndividualKey(SageFrameSettingKeys.FileExtensions); 
                        this.fuImage.SaveAs(strSaveLocation);
                        System.Drawing.Image image = System.Drawing.Image.FromFile(strSaveLocation);
                        System.Drawing.Image thumbImg = image.GetThumbnailImage(125, 100, null, new IntPtr());
                        if (File.Exists(physicalpath + @"\thumb\" + fileName)) File.Delete(physicalpath + @"\thumb\" + fileName);
                        thumbImg.Save(physicalpath + @"\thumb\" + fileName);
                        ImageFiles.Add(new ImageFile { ThumbImageFileName = thumbpath + @"/" + fileName, FileName = path + @"/" + fileName, Size = (this.fuImage.FileBytes.Length / 1024).ToString(), CreatedDate = DateTime.Now.ToString() });
                        ImageFiles = ImageFiles.OrderByDescending(x => DateTime.Parse(x.CreatedDate)).ToList();
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool CheckAuthentication()
    {
        BaseAdministrationUserControl obj = new BaseAdministrationUserControl();
        int userModuleId = int.Parse(Request.QueryString["userModuleId"].ToString());
        SageFrame.Services.AuthenticateService objAuthentication = new SageFrame.Services.AuthenticateService();
        PageBase objPageBase = new PageBase();
        if (objAuthentication.IsPostAuthenticatedView(obj.GetPortalID, userModuleId, obj.GetUsername, objPageBase.SageFrameSecureToken))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
