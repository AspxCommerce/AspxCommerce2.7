#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SageFrame.Web;
using System.Web.UI.WebControls;
using SageFrame.SageBannner.Info;
using System.IO;
using SageFrame.SageBannner.Controller;
using System.Data;
using SageFrame.Common;
using System.Collections;
using SageFrame.Web.Utilities;
using AspxCommerce.ImageResizer;
using AspxCommerce.Core;
#endregion

public partial class Modules_Sage_Banner_EditBanner : BaseAdministrationUserControl
{

    public string swfFileName = string.Empty;
    public int UserModuleId;
    public string modulePath = string.Empty;
    public int BannerId;
    public int WebUrl = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss();
        IncludeJS();
        try
        {

            modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var ImagePath='" + ResolveUrl(modulePath) + "';", true);
            if (!IsPostBack)
            {
                LoadBannerListOnGrid(GetPortalID, Int32.Parse(SageUserModuleID), GetCurrentCulture());
                ClearImageForm();
                LoadSagePage();
            }
          //  ImageURL();
            UserModuleId = Int32.Parse(SageUserModuleID);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
   
    private void IncludeJS()
    {
        IncludeJs("SageBanner", "/Modules/Sage_Banner/js/jquery.Jcrop.js");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
    }

    private void IncludeCss()
    {
        IncludeCss("SageBanner", "/Modules/Sage_banner/css/jquery.Jcrop.css", "/Modules/Sage_Banner/css/Module.css");
    }

    private void LoadSagePage()
    {
        SageBannerController obj = new SageBannerController();
        ddlPagesLoad.DataSource = obj.GetAllPagesOfSageFrame(GetPortalID); ;
        ddlPagesLoad.DataTextField = "PageName";
        ddlPagesLoad.DataValueField = "TabPath";
        ddlPagesLoad.DataBind();
    }
    protected void imbSave_Click(object sender, EventArgs e)
    {
        try
        {
            string fName = string.Empty;
            if (Session["ImageName"] != null && Session["ImageName"].ToString() != string.Empty && Session["ImageName"].ToString().Length > 0 && fuFileUpload.FileName.Length < 1)
            {
                fName = Session["ImageName"].ToString();
            }
            else
            {
                fName = fuFileUpload.FileName;
            }
            if (SageFrame.Web.PictureManager.ValidImageExtension(fName))
            {
                BannerId = Convert.ToInt32(ViewState["EditBannerID"]);
                int ImageID = Convert.ToInt32(Session["EditImageID"]);
                if (fuFileUpload.HasFile || ImageID != 0)
                {
                    SaveBannerContent(BannerId, ImageID);
                    LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
                    divbannerImageContainer.Attributes.Add("style", "display:block");
                    divEditBannerImage.Attributes.Add("style", "display:none");
                    ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "BannerSavedsuccesfully"), "", SageMessageType.Success);
                    txtWebUrl.Text = string.Empty;
                    txtBannerDescriptionToBeShown.Text = string.Empty;
                }
                else
                {
                    rdbReadMorePageType.SelectedValue = "0";
                    txtWebUrl.Text = string.Empty;
                    txtBannerDescriptionToBeShown.Text = string.Empty;
                    ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "NoBannerImage"), "", SageMessageType.Error);
                }
            }
            else
            {
                ShowMessage("Invalid File Extension", "Invalid File Extension", "The File you want to upload is invalid", SageMessageType.Error);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void imbCancel_Click(object sender, EventArgs e)
    {
        Session["EditHTMLContentID"] = null;
        Session["NavigationImage"] = null;
        Session["ImageName"] = null;
        Session["EditImageID"] = null;
        Session["ActiveTabIndex"] = null;
        //  Session.Remove("EditImageID");
        txtBannerDescriptionToBeShown.Text = string.Empty;
        rdbReadMorePageType.SelectedValue = "0";
        divbannerImageContainer.Attributes.Add("style", "display:block");
        divEditBannerImage.Attributes.Add("style", "display:none");
    }
    #region CropBanner

    protected void _cropCommand_Click(object sender, EventArgs e)
    {
        try
        {
            var x = int.Parse(_xField.Value);
            var y = int.Parse(_yField.Value);
            var width = int.Parse(_widthField.Value);
            var height = int.Parse(_heightField.Value);
            string imageName = Convert.ToString(ViewState["ImageToBeEdit"]);
            string source = Server.MapPath("~/Modules/Sage_Banner/images/OriginalImage/" + imageName);
            string dest=Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Default/");
            InterceptImageController.ResizeBannerImageAndCrop(source, width,height, dest,imageName);
            string  soruceFolder = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Default/");
            string SourcePath = soruceFolder + imageName;
            string thumbMedium = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Medium/");
            string thumbSmall = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Small/");
            string thumbLarge = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Large/");
            InterceptImageController.ResizeBannerImage(SourcePath, 965, thumbLarge, imageName);
            InterceptImageController.ResizeBannerImage(SourcePath, 768, thumbMedium, imageName);
            InterceptImageController.ResizeBannerImage(SourcePath, 320, thumbSmall, imageName);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
        divImageEditor.Attributes.Add("style", "display:none");
        pnlBannercontainer.Attributes.Add("style", "display:block");
        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "ImageEditedSuccesfully"), "", SageMessageType.Success);
    }

    [Obsolete]
    public void SaveThumbnailImages(string ImageFilePath, int TargetSize, string TargetLocation, string fileName)
    {
        try
        {
            if (!Directory.Exists(TargetLocation))
            {
                Directory.CreateDirectory(TargetLocation);
            }
            string SavePath = string.Empty;
            SavePath = TargetLocation + fileName;
            PictureManager.CreateThmnail(ImageFilePath, TargetSize, SavePath);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region For Image Information
    private void SaveBannerContent(int BannerId, int ImageId)
    {
        try
        {
            string fName = fuFileUpload.FileName;
            if (SageFrame.Web.PictureManager.ValidImageExtension(fName))
            {

                //bool isEdit = false;
                SageBannerInfo obj = new SageBannerInfo();
                if (Session["EditImageID"] != null && Session["EditImageID"].ToString() != string.Empty)
                {
                    obj.ImageID = Int32.Parse(Session["EditImageID"].ToString());
                    if (fuFileUpload.HasFile)
                    {
                        obj.ImagePath = fuFileUpload.PostedFile.FileName.Replace(" ", "_");
                        obj.NavigationImage = fuFileUpload.PostedFile.FileName.Replace(" ", "_");
                    }
                    else
                    {
                       // isEdit = true;
                        obj.ImagePath = Convert.ToString(Session["ImageName"]);
                        obj.NavigationImage = Convert.ToString(Session["ImageName"]);
                    }
                }
                else
                {
                    obj.ImageID = 0;
                    obj.ImagePath = fuFileUpload.FileName.Replace(" ", "_");
                    obj.NavigationImage = fuFileUpload.FileName.Replace(" ", "_");
                }
                obj.Caption = string.Empty;
                if (rdbReadMorePageType.SelectedItem.Text == "Page")
                {
                    obj.ReadMorePage = ddlPagesLoad.SelectedValue.ToString();
                    obj.LinkToImage = string.Empty;
                }
                if (rdbReadMorePageType.SelectedItem.Text == "Web Url")
                {
                    obj.LinkToImage = txtWebUrl.Text;
                    obj.ReadMorePage = string.Empty;
                }
                obj.UserModuleID = Int32.Parse(SageUserModuleID);
                obj.BannerID = BannerId;
                obj.ImageID = ImageId;
                obj.ReadButtonText = txtReadButtonText.Text;
                obj.Description = txtBannerDescriptionToBeShown.Text.Trim();
                obj.PortalID = GetPortalID;
                obj.CultureCode = GetCurrentCulture();
                string swfExt = System.IO.Path.GetExtension(fuFileUpload.PostedFile.FileName);
                if (swfExt == ".swf")
                {
                    if (fuFileUpload.FileContent.Length > 0)
                    {
                        string Path = GetUplaodImagePhysicalPath();
                        string fileName = fuFileUpload.PostedFile.FileName.Replace(" ", "_");
                        DirectoryInfo dirUploadImage = new DirectoryInfo(Path);
                        if (dirUploadImage.Exists == false)
                        {
                            dirUploadImage.Create();
                        }
                        string fileUrl = Path + fileName;
                        int i = 1;
                        while (File.Exists(fileUrl))
                        {

                            fileName = i + fileName;
                            fileUrl = Path + i + fileName;
                            i++;
                        }
                        fuFileUpload.PostedFile.SaveAs(fileUrl);
                        swfFileName = "Modules/Sage_Banner/images/" + fileName;
                        obj.ImagePath = fileName;
                        obj.NavigationImage = fileName;
                    }
                }
                else
                {
                    string target = Server.MapPath("~/Modules/Sage_Banner/images/OriginalImage/");
                    string thumbLarge = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Large/");
                    string thumbMedium = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Medium/");
                    string thumbSmall = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Small/");
                    string defaultImage = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail/Default/");
                    //System.Drawing.Image.GetThumbnailImageAbort thumbnailImageAbortDelegate = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    if (fuFileUpload.HasFile)
                    {
                        string fileName = fuFileUpload.PostedFile.FileName.Replace(" ", "_");
                        int i = 1;
                        while (File.Exists(target + "/" + fileName))
                        {
                            fileName = i + fileName;
                            i++;
                        }
                        fuFileUpload.SaveAs(Path.Combine(target, fileName));
                        fuFileUpload.SaveAs(Path.Combine(defaultImage, fileName));
                        string SourcePath = target + fileName;
                         //Resize Banner Images using Image Resizer
                        InterceptImageController.ResizeBannerImage(SourcePath, 320, thumbSmall, fileName);
                        InterceptImageController.ResizeBannerImage(SourcePath, 768, thumbMedium, fileName);
                        InterceptImageController.ResizeBannerImage(SourcePath, 965, thumbLarge, fileName);
                        obj.ImagePath = fileName;
                        obj.NavigationImage = fileName;
                    }
                }
                SageBannerController objcont = new SageBannerController();
                objcont.SaveBannerContent(obj);
                int userModuleID = Int32.Parse(SageUserModuleID);
                BannerCacheClear();
                ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "BannerSavedsuccesfully"), "", SageMessageType.Success);
            }
            else
            {
                ShowMessage("Invalid File Extension", "Invalid File Extension", "The File you want to upload is invalid", SageMessageType.Error);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
        Session["ImageName"] = null;
        Session["EditImageID"] = null;
    }

    string GetUplaodImagePhysicalPath()
    {
        return System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "Modules\\Sage_Banner\\images\\";
    }

    # endregion

    #region For clear all form
    private void ClearImageForm()
    {
        txtCaption.Text = string.Empty;
        imgEditBannerImageImage.Visible = false;
        txtWebUrl.Text = string.Empty;
        txtReadButtonText.Text = string.Empty;
        txtBannerDescriptionToBeShown.Text = "";
        txtBody.Text = string.Empty;
    }
    #endregion

    #region For banner information

    protected void imbSaveBanner_Click(object sender, EventArgs e)
    {
        List<string> arrBanner = new List<string>();
        SageBannerController obj = new SageBannerController();
        List<SageBannerInfo> bannerInfo = obj.LoadBannerListOnGrid(GetPortalID, UserModuleId, GetCurrentCulture());
        if (bannerInfo != null)
        {
            foreach (SageBannerInfo list in bannerInfo)
            {
                if (!arrBanner.Contains(list.BannerName))
                {
                    arrBanner.Add(list.BannerName.ToLower());
                }
            }
        }
        if (arrBanner.Contains(txtBannerName.Text.Trim().ToLower()))
        {
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "DuplicateName"), "", SageMessageType.Error);
        }
        else
        {
            SaveBannerInformation();
            BannerCacheClear();
            UserControl uc1 = (UserControl)this.FindControl("TabContainerManagePages");
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "BannerAddedSucessfully"), "", SageMessageType.Success);
        }
        pnlBannercontainer.Attributes.Add("style", "display:none");
        pnlBannerList.Attributes.Add("style", "display:block");
        txtBannerName.Text = "";
    }
    private void SaveBannerInformation()
    {
        try
        {
            SageBannerInfo obj = new SageBannerInfo();
            obj.BannerName = txtBannerName.Text.Trim();
            obj.BannerDescription = txtBannerDescription.Text;
            obj.UserModuleID = Int32.Parse(SageUserModuleID);
            obj.PortalID = GetPortalID;
            obj.CultureCode = GetCurrentCulture();
            SageBannerController objBcon = new SageBannerController();
            objBcon.SaveBannerInformation(obj);
            LoadBannerListOnGrid(GetPortalID, Int32.Parse(SageUserModuleID), GetCurrentCulture());
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
    #endregion
    protected void imAddHtmlContent_Click(object sender, EventArgs e)
    {
        txtBody.Text = string.Empty;
        divHtmlBannerContainer.Attributes.Add("style", "display:none");
        divEditWrapper.Attributes.Add("style", "display:block");
        imgEditNavImage.Attributes.Add("style", "display:none");
        BannerCacheClear();
    }

    #region Editor Html Content
    protected void imbSaveEditorContent_Click(object sender, EventArgs e)
    {
        bool isNotEmpty = true;
        string target = Server.MapPath("~/Modules/Sage_Banner/images");
        string thumbTarget = Server.MapPath("~/Modules/Sage_Banner/images/ThumbNail");
        //System.Drawing.Image.GetThumbnailImageAbort thumbnailImageAbortDelegate = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
        if (fluBannerNavigationImage.HasFile)
        {
            isNotEmpty = false;
            fluBannerNavigationImage.SaveAs(System.IO.Path.Combine(target, fluBannerNavigationImage.FileName.Replace(" ", "_")));
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            InterceptImageController.copyOriginalImageToRespectives(new ImageResizer.ResizeSettings(80, 80, ImageResizer.FitMode.Carve, "jpg"), Path.Combine(target, fluBannerNavigationImage.PostedFile.FileName), Path.Combine(thumbTarget, fluBannerNavigationImage.FileName), aspxCommonObj,IsWaterMark.False);
           
        }
        int ImageID;
        int Bannerid = Convert.ToInt32(ViewState["EditBannerID"]);
        string NavImagepath = "";
        if (Session["EditHTMLContentID"] != null && Session["EditHTMLContentID"].ToString() != string.Empty)
        {
            isNotEmpty = false;
            ImageID = Int32.Parse(Session["EditHTMLContentID"].ToString());
            NavImagepath = Convert.ToString(Session["NavigationImage"]);
        }
        else
        {
            ImageID = 0;
            NavImagepath = Convert.ToString(fluBannerNavigationImage.FileName.Replace(" ", "_"));
        }
        try
        {
            ArrayList arrColl = null;
            arrColl = IsContentValid(txtBody.Text.ToString());
            if (arrColl.Count > 0 && arrColl[0].ToString().ToLower().Trim() == "true")
            {
                isNotEmpty = false;
                string HTMLBodyText = arrColl[1].ToString().Trim();
                SageBannerController objController = new SageBannerController();
                objController.SaveHTMLContent(NavImagepath, HTMLBodyText, Bannerid, UserModuleId, ImageID, GetPortalID, GetCurrentCulture());
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
        if (isNotEmpty)
        {
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "NoBannerContent"), "", SageMessageType.Error);
        }
        else
        {

            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "BannerHTMLContentSavedSuccessfully"), "", SageMessageType.Success);
            txtBody.Text = string.Empty;
            Session["EditHTMLContentID"] = null;
            Session["NavigationImage"] = null;
            LoadHTMLContentOnGrid(Bannerid);
            BannerCacheClear();
            divHtmlBannerContainer.Attributes.Add("style", "display:block");
            divEditWrapper.Attributes.Add("style", "display:none");
        }
    }

    private ArrayList IsContentValid(string str)
    {
        bool isValid = true;
        str = RemoveUnwantedHTMLTAG(str);
        if (str == string.Empty)
            isValid = false;
        ArrayList arrColl = new ArrayList();
        arrColl.Add(isValid);
        arrColl.Add(str);
        return arrColl;
    }

    public string RemoveUnwantedHTMLTAG(string str)
    {
        str = System.Text.RegularExpressions.Regex.Replace(str, "<br/>$", "");
        str = System.Text.RegularExpressions.Regex.Replace(str, "<br />$", "");
        str = System.Text.RegularExpressions.Regex.Replace(str, "^&nbsp;", "");
        str = System.Text.RegularExpressions.Regex.Replace(str, "<form[^>]*>", "");
        str = System.Text.RegularExpressions.Regex.Replace(str, "</form>", "");
        return str;
    }

    #endregion

    #region Gridview


    public void LoadBannerImagesOnGrid(int BannerID, int UserModuleID, int PortalID)
    {
        SageBannerController obj = new SageBannerController();
        gdvBannerImages.DataSource = obj.LoadBannerImagesOnGrid(BannerID, UserModuleID, PortalID, GetCurrentCulture());
        gdvBannerImages.DataBind();
        if (gdvBannerImages.Rows.Count > 0)
        {
            if (gdvBannerImages.PageIndex == 0)
            {
                gdvBannerImages.Rows[0].FindControl("imgListUp").Visible = false;

            }
            if (gdvBannerImages.PageIndex == (gdvBannerImages.PageCount - 1))
            {
                gdvBannerImages.Rows[gdvBannerImages.Rows.Count - 1].FindControl("imgListDown").Visible = false;
            }
        }
    }

    protected void gdvBannerImages_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gdvBannerImages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int ImageId = Int32.Parse(e.CommandArgument.ToString());
            BannerId = int.Parse(ViewState["EditBannerID"].ToString());
            switch (e.CommandName.ToString())
            {
                case "Edit":
                    BannerEditByImageID(ImageId);
                    break;
                case "Delete":
                    DeleteBannerContentByID(ImageId);
                    break;
                case "Editimage":
                    divImageEditor.Attributes.Add("style", "display:block");
                    pnlBannercontainer.Attributes.Add("style", "display:none");
                    EditImageByImageID(ImageId);
                    break;
                case "SortUp":
                    SageBannerController obj = new SageBannerController();
                    obj.SortImageList(ImageId, true);
                    LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Banner", "TheBannerContentIsShiftedUpSuccessfully"), "", SageMessageType.Success);
                    break;
                case "SortDown":
                    SageBannerController objc = new SageBannerController();
                    objc.SortImageList(ImageId, false);
                    LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Banner", "TheBannerContentIsShiftedDownSuccessfully"), "", SageMessageType.Success);
                    break;
            }
            BannerCacheClear();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void gdvBannerImages_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    private void EditImageByImageID(int ImageId)
    {
        SageBannerInfo obj = new SageBannerInfo();
        SageBannerController objcnt = new SageBannerController();
        obj = objcnt.GetImageInformationByID(ImageId);
        _imageEditor.ImageUrl = modulePath + "images/OriginalImage/" + obj.ImagePath;
        divImageEditor.Attributes.Add("style", "display:block");
        ViewState["ImageToBeEdit"] = obj.ImagePath;
    }

    private void BannerEditByImageID(int ImageId)
    {
        int BannerId = Convert.ToInt32(ViewState["EditBannerID"]);
        SageBannerInfo objEd = new SageBannerInfo();
        SageBannerController objEc = new SageBannerController();
        objEd = objEc.GetImageInformationByID(ImageId);
        txtCaption.Text = objEd.Caption;
        if (objEd.LinkToImage != null && objEd.LinkToImage != string.Empty)
        {
            rdbReadMorePageType.SelectedValue = "1";
            txtWebUrl.Text = objEd.LinkToImage;
            WebUrl = 1;
        }
        else if (objEd.ReadMorePage != null)
        {
            ddlPagesLoad.SelectedIndex = ddlPagesLoad.Items.IndexOf(ddlPagesLoad.Items.FindByValue(objEd.ReadMorePage));
            WebUrl = 0;
        }
        txtReadButtonText.Text = objEd.ReadButtonText;
        Session["ImageName"] = objEd.ImagePath;
        imgEditBannerImageImage.ImageUrl = modulePath + "images/Thumbnail/Large/" + objEd.ImagePath;
        txtBannerDescriptionToBeShown.Text = objEd.Description;
        imgEditBannerImageImage.Visible = true;
        divbannerImageContainer.Attributes.Add("style", "display:none");
        divEditBannerImage.Attributes.Add("style", "display:block");
        LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
        Session["EditImageID"] = ImageId;
    }

    private void DeleteBannerContentByID(int ImageId)
    {
        string ImageName = GetFileName(ImageId);
        if (ImageName != string.Empty)
        {
            DeleteImageFromFolder(ImageName);
        }
        SageBannerController obDel = new SageBannerController();
        obDel.DeleteBannerContentByID(ImageId);
        int BannerId = Convert.ToInt32(ViewState["EditBannerID"]);
        LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "BannerImageDeletedsuccesfully"), "", SageMessageType.Success);
    }

    private string GetFileName(int ImageID)
    {
        SageBannerController OBJC = new SageBannerController();
        return OBJC.GetFileName(ImageID);
    }

    private void DeleteImageFromFolder(string FileName)
    {
        try
        {
            string BannerImagePath = Server.MapPath(modulePath + "images/OriginalImage/") + FileName;
            string largeThumb = Server.MapPath(modulePath + "images/ThumbNail/Large/") + FileName;
            string mediumThumb = Server.MapPath(modulePath + "images/ThumbNail/Medium/") + FileName;
            string smallThumb = Server.MapPath(modulePath + "images/ThumbNail/Small/") + FileName;
            string defaultThumb = Server.MapPath(modulePath + "images/ThumbNail/Default/") + FileName;
            if (File.Exists(BannerImagePath))
            {
                File.Delete(BannerImagePath);
                if (File.Exists(largeThumb))
                {
                    File.Delete(largeThumb);
                }
                if (File.Exists(mediumThumb))
                {
                    File.Delete(mediumThumb);
                }
                if (File.Exists(smallThumb))
                {
                    File.Delete(smallThumb);
                }
                if (File.Exists(defaultThumb))
                {
                    File.Delete(defaultThumb);
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void LoadHTMLContentOnGrid(int BannerID)
    {
        SageBannerController obj = new SageBannerController();
        List<SageBannerInfo> info = obj.LoadHTMLContentOnGrid(BannerID, Int32.Parse(SageUserModuleID), GetPortalID, GetCurrentCulture());
        gdvHTMLContent.DataSource = info;
        gdvHTMLContent.DataBind();
    }

    #endregion

    #region BannerList Gridview

    public void LoadBannerListOnGrid(int PortalID, int UserModuleID, string CultureCode)
    {
        SageBannerController obj = new SageBannerController();
        List<SageBannerInfo> bannerInfo = obj.LoadBannerListOnGrid(PortalID, UserModuleID, CultureCode);
        gdvBannerList.DataSource = bannerInfo;
        gdvBannerList.DataBind();
    }

    #endregion

    protected void gdvBannerImages_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public bool ThumbnailCallback()
    {
        return false;
    }

    protected void gdvBannerImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #region gdvBannerList

    protected void gdvBannerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //divbannerImageContainer.Attributes.Add("style", "display:block");
        HttpContext.Current.Session["ActiveTabIndex"] = SageBannerTabcontainer.ActiveTabIndex;
        string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { ',' });
        Int32 BannerID = Int32.Parse(commandArgsAccept[0].ToString());
        if (e.CommandName == "BannerEdit")
        {
            divbannerImageContainer.Attributes.Add("style", "display:block");
            pnlBannerList.Attributes.Add("style", "display:none");
            LoadBannerImagesOnGrid(BannerID, Int32.Parse(SageUserModuleID), GetPortalID);
            LoadHTMLContentOnGrid(BannerID);
            pnlBannercontainer.Attributes.Add("style", "display:block");
            divEditWrapper.Attributes.Add("style", "display:none");
            divEditBannerImage.Attributes.Add("style", "display:none");
            divHtmlBannerContainer.Attributes.Add("style", "display:block");
            ViewState["EditBannerID"] = BannerID;
        }
        if (e.CommandName == "BannerDelete")
        {
            string bannerName = txtBannerName.Text.ToLower().Trim();
            DeleteBannerAndItsContentByBannerID(BannerID);
            LoadBannerListOnGrid(GetPortalID, Int32.Parse(SageUserModuleID), GetCurrentCulture());
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "DeletedSucessfully"), "", SageMessageType.Success);
            pnlBannerList.Attributes.Add("style", "display:block");
            pnlBannercontainer.Attributes.Add("style", "display:none");
        }
        BannerCacheClear();
    }
    public void DeleteBannerAndItsContentByBannerID(int BannerID)
    {
        SageBannerController objDelBanner = new SageBannerController();
        objDelBanner.DeleteBannerAndItsContentByBannerID(BannerID);
    }
    #endregion
    protected void gdvHTMLContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int ImageId = Int32.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "DeleteHTML")
        {
            DeleteHTMLContentByID(ImageId);
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/Sage_Banner/ModuleLocalText", "DeletedSucessfully"), "", SageMessageType.Success);
        }
        if (e.CommandName == "EditHTML")
        {
            EditHTMLContentByID(ImageId);
        }
        Session["EditHTMLContentID"] = ImageId;
        BannerCacheClear();
    }

    public void DeleteHTMLContentByID(int ImageId)
    {
        SageBannerController objDel = new SageBannerController();
        objDel.DeleteHTMLContentByID(ImageId);
        LoadHTMLContentOnGrid(Convert.ToInt32(ViewState["EditBannerID"]));
        BannerCacheClear();
    }

    private void EditHTMLContentByID(int ImageId)
    {
        try
        {
            imgEditNavImage.Visible = true;
            SageBannerInfo objEHtmlContent = new SageBannerInfo();
            SageBannerController objHTMl = new SageBannerController();
            objEHtmlContent = objHTMl.GetHTMLContentForEditByID(ImageId);
            txtBody.Text = objEHtmlContent.HTMLBodyText;
            imgEditNavImage.ImageUrl = modulePath + "images/ThumbNail/" + objEHtmlContent.NavigationImage;
            Session["NavigationImage"] = objEHtmlContent.NavigationImage;
            divHtmlBannerContainer.Attributes.Add("style", "display:none");
            divEditWrapper.Attributes.Add("style", "display:block");
            int BannerId = Convert.ToInt32(ViewState["EditBannerID"]);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void gdvBannerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gdvBannerList.PageIndex = e.NewPageIndex;
        LoadBannerListOnGrid(GetPortalID, Int32.Parse(SageUserModuleID), GetCurrentCulture());

    }

    protected void gdvBannerImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvBannerImages.PageIndex = e.NewPageIndex;
        LoadBannerImagesOnGrid(BannerId, Int32.Parse(SageUserModuleID), GetPortalID);
    }

    protected void gdvHTMLContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int bannerId = Convert.ToInt32(ViewState["EditBannerID"]);
        gdvHTMLContent.PageIndex = e.NewPageIndex;
        LoadHTMLContentOnGrid(bannerId);
    }

    protected void imbCancelImageEdit_Click(object sender, EventArgs e)
    {
        txtWebUrl.Text = string.Empty;
        divImageEditor.Attributes.Add("style", "display:none");
        pnlBannercontainer.Attributes.Add("style", "display:block");
    }

    protected void imgCancelHtmlContent_Click(object sender, EventArgs e)
    {
        divHtmlBannerContainer.Attributes.Add("style", "display:block");
        divEditWrapper.Attributes.Add("style", "display:none");
    }
    private void BannerCacheClear()
    {
        HttpRuntime.Cache.Remove("BannerImages_" + GetCurrentCulture() + "_" + UserModuleId.ToString());
        HttpRuntime.Cache.Remove("BannerSetting_" + GetCurrentCulture() + "_" + UserModuleId.ToString());
    }
    protected void imbReturnBack_Click(object sender, EventArgs e)
    {
        txtWebUrl.Text = string.Empty;
        txtBannerDescriptionToBeShown.Text = "";
        rdbReadMorePageType.SelectedValue = "0";
        pnlBannercontainer.Attributes.Add("style", "display:none");
        pnlBannerList.Attributes.Add("style", "display:block");
    }
}
