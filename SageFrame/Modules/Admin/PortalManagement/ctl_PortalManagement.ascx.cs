#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.PortalSetting;
using System.Web.UI.HtmlControls;
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.Utilities;
using System.IO;
using System.Collections;
using SageFrame.Shared;
using SageFrame.SageFrameClass;
using SageFrame.Message;
using SageFrame.Modules;
using SageFrame.Templating;
using SageFrame.PortalManagement;
using SageFrame.Common;
using System.Text;
using SageFrame.Security.Helpers;
using SageFrame.Modules.Admin.PortalSettings;
using SageFrame.Core;
using System.Text.RegularExpressions;
using SageFrame.SageFrameClass.MessageManagement;
#endregion

namespace SageFrame.Modules.Admin.PortalManagement
{
    public partial class ctl_PortalManagement : BaseAdministrationUserControl
    {
        SageFrameConfig pagebase = new SageFrameConfig();
        string appPath = string.Empty;
        public bool Flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ddlAvailablePortal.Visible = false;
                lblAvailablePortal.Visible = false;
                lblUrl.Visible = false;
                txtUrl.Visible = false;
                appPath = GetApplicationName;
                if (!IsPostBack)
                {
                    BindPortal();
                    //BindSitePortal();
                    PanelVisibility(false, true);
                    imbBtnSaveChanges.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("PortalModules", "AreYouSureToSaveChanges") + "')");
                }
                trEmail.Visible = false;
                //test.Visible = false;
                if (HttpRuntime.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()] != null)
                {
                    trEmail.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindPortal()
        {
            gdvPortal.DataSource = PortalController.GetPortalList();
            gdvPortal.DataBind();
        }
        //private void BindSitePortal()
        //{
        //    try
        //    {
        //        SettingProvider spr = new SettingProvider();
        //        DataTable dt = spr.GetAllPortals();
        //        ddlPortalName.DataSource = dt;
        //        ddlPortalName.DataTextField = "Name";
        //        ddlPortalName.DataValueField = "PortalID";
        //        ddlPortalName.DataBind();
        //        if (ddlPortalName.Items.Count > 0)
        //        {
        //            ddlPortalName.SelectedIndex = ddlPortalName.Items.IndexOf(ddlPortalName.Items.FindByValue(pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.SuperUserPortalId)));
        //        }
        //        if (ddlPortalName.Items.Count <= 1)
        //        {
        //            ddlPortalName.Visible = false;
        //            btnPortalSave.Visible = false;
        //        }
        //        else
        //        {
        //            ddlPortalName.Visible = true;
        //            btnPortalSave.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ProcessException(ex);
        //    }
        //}
        private void PanelVisibility(bool VisiblePortal, bool VisiblePortalList)
        {
            pnlPortal.Visible = VisiblePortal;
            pnlPortalList.Visible = VisiblePortalList;
        }
        private void ClearForm()
        {
            txtEmail.Text = "";
            txtPortalName.Text = "";
            txtUrl.Text = "";
        }
        protected void imgAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GetParentPortalList();
                txtPortalName.Visible = true;
                lblDefaultPortal.Visible = false;
                imgSave.Visible = true;
                //lblSave.Visible = true;
                TabPanelPortalModulesManagement.Visible = false;
                ClearForm();
                txtPortalName.Enabled = true;
                hdnPortalID.Value = "0";
                PanelVisibility(true, false);
                Flag = true;
                ddlAvailablePortal.Visible = true;
                lblAvailablePortal.Visible = true;
                lblUrl.Visible = true;
                txtUrl.Visible = true;
                // BindSitePortal();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imgSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = false;
                foreach (GridViewRow row in gdvPortal.Rows)
                {
                    LinkButton lnkPortal = row.FindControl("lnkUsername") as LinkButton;
                    if (gdvPortal.DataKeys[row.RowIndex]["PortalID"].ToString() != hdnPortalIndex.Value)
                    {
                        if (lnkPortal.Text.ToLower().Equals(txtPortalName.Text.ToLower()))
                        {
                            status = true;
                        }
                    }
                }
                if (!(string.IsNullOrEmpty(txtPortalName.Text)))
                {
                    if (!status)
                    {
                        SaveProtal();
                        BindPortalSetting();
                        HttpRuntime.Cache.Remove(CacheKeys.Portals);
                        SageFrameConfig sf = new SageFrameConfig();
                        sf.ResetSettingKeys(int.Parse(this.hdnPortalID.Value.ToString()));
                        HttpContext.Current.Session.Remove(SessionKeys.SageFrame_PortalSEOName);
                        AppErazer.ClearSysCache();
                        BindPortal();
                        // BindSitePortal();
                        PanelVisibility(false, true);
                        //Redirect(GetPortalID);
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("PortalSettings", "PortalAlreadyExists"), "", SageMessageType.Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string GenerateRandomCode()
        {
            string s = "";
            Random random = new Random();
            string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < 4; i++)
                s = String.Concat(s, CapchaValue[random.Next(36)]);
            return s;
        }

        private string CheckDuplicateTemplate(string portalname)
        {
            bool isDuplicate = false;
            string fname = string.Empty;
            string TempFolder = HttpContext.Current.Server.MapPath(GetApplicationName + "/Templates/");
            DirectoryInfo dInfo = new DirectoryInfo(TempFolder);
            Foldername info = new Foldername();
            foreach (DirectoryInfo obj in dInfo.GetDirectories())
            {
                if (obj.Name.ToLower() == portalname.ToLower())
                {
                    info.Existfolder = obj.Name.ToLower();
                    isDuplicate = true;
                }
            }
            if (isDuplicate)
            {
                string toAppend = GenerateRandomCode();
                portalname = portalname + "_" + toAppend;
                portalname = CheckDuplicateTemplate(portalname);
            }
            return portalname;
        }


        private void Redirect(int portalID)
        {
            string redirectUrl = string.Empty;
            string tempPortalName = string.Empty;
            if (Int32.Parse(hdnPortalID.Value) > 0)
            {
                redirectUrl = !IsParent ? GetApplicationName : GetApplicationName + "/portal/" + txtPortalName.Text.ToLower().Trim();
                SageFrameConfig sfConfig = new SageFrameConfig();
                redirectUrl += "/Admin/Portals" + SageFrameSettingKeys.PageExtension;
            }
            else
            {
                redirectUrl = Request.Url.ToString();
            }
            Response.Redirect(redirectUrl);
        }

        private string SaveTemplate()
        {
            string portalname = CheckDuplicateTemplate(txtPortalName.Text);
            CreateNewTemplateFolder(portalname);
            return portalname;
        }
        public void CreateNewTemplateFolder(string TemplateName)
        {
            try
            {
                string completePath = Server.MapPath(appPath + "/Templates/" + TemplateName);
                string path = HttpContext.Current.Server.MapPath(appPath).Replace(@"\Admin", "");
                if (HttpRuntime.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()] != null)
                {
                    DirectoryInfo SrcDirA = new DirectoryInfo(path + "/Templates/AspxCommerce/");
                    DirectoryInfo DisDirA = new DirectoryInfo(path + "/Templates/" + TemplateName);
                    CopyDirectory(SrcDirA, DisDirA);
                }
                else
                {
                    DirectoryInfo SrcDir = new DirectoryInfo(path + "/Core/Blank/");
                    DirectoryInfo DisDir = new DirectoryInfo(path + "/Templates/" + TemplateName);
                    CopyDirectory(SrcDir, DisDir);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name));
            }
            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        public class Foldername
        {
            public string Existfolder { get; set; }

            public Foldername() { }
        }

        private void BindPortalSetting()
        {
            Hashtable hst = new Hashtable();
            SettingProvider sep = new SettingProvider();
            DataTable dt = sep.GetSettingsByPortal(GetPortalID.ToString(), string.Empty); //GetSettingsByPortal();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                }
            }
            HttpRuntime.Cache.Insert(CacheKeys.SageSetting, hst);
        }

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                PanelVisibility(false, true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvPortal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnPortalID = (HiddenField)e.Row.FindControl("hdnPortalID");
                    HiddenField hdnSEOName = (HiddenField)e.Row.FindControl("hdnSEOName");
                    HiddenField hdnIsParent = (HiddenField)e.Row.FindControl("hdnIsParent");
                    HiddenField hdnParentPortalName = (HiddenField)e.Row.FindControl("hdnParentPortalName");
                    HyperLink hypPortalPreview = (HyperLink)e.Row.FindControl("hypPortalPreview");

                    Label lblDefaultPage = (Label)e.Row.FindControl("lblDefaultPage");
                    //hypPortalPreview.Text = "Preview";

                    if (hdnIsParent.Value.ToLower() != "true")
                    {
                        //hypPortalPreview.NavigateUrl = ResolveUrl("~/portal/" + hdnSEOName.Value.ToLower() + "/" + lblDefaultPage.Text.Replace(" ", "-") + SageFrameSettingKeys.PageExtension);
                        if (hdnParentPortalName.Value.ToString() == "default")
                        {
                            hypPortalPreview.NavigateUrl = ResolveUrl("~/portal/" + hdnSEOName.Value.ToLower() + "/" + lblDefaultPage.Text.Replace(" ", "-") + SageFrameSettingKeys.PageExtension);
                        }
                        else
                        {
                            string ParentPortalName = hdnParentPortalName.Value.ToString().Contains("http://") || hdnParentPortalName.Value.ToString().Contains("https://") ? hdnParentPortalName.Value.ToString() : "http://" + hdnParentPortalName.Value.ToString();
                            hypPortalPreview.NavigateUrl = (ParentPortalName + "/portal/" + hdnSEOName.Value.ToLower() + "/" + lblDefaultPage.Text.Replace(" ", "-") + SageFrameSettingKeys.PageExtension);
                        }
                    }
                    else
                    {
                        if (hdnSEOName.Value.ToString() == "default")
                            hypPortalPreview.NavigateUrl = ResolveUrl("~/" + lblDefaultPage.Text.Replace(" ", "-") + SageFrameSettingKeys.PageExtension);
                        else
                        {
                            string SEOName = hdnSEOName.Value.ToString().Contains("http://") || hdnSEOName.Value.ToString().Contains("https://") ? hdnSEOName.Value.ToString() : "http://" + hdnSEOName.Value.ToString();
                            hypPortalPreview.NavigateUrl = (SEOName + "/" + lblDefaultPage.Text.Replace(" ", "-") + SageFrameSettingKeys.PageExtension);
                        }
                    }
                    LinkButton imgDelete = (LinkButton)e.Row.FindControl("imgDelete");
                    LinkButton imgEdit = (LinkButton)e.Row.FindControl("imgEdit");
                    HtmlInputCheckBox chkBoxIsParentItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsParentItem");
                    if (hdnIsParent != null && chkBoxIsParentItem != null)
                    {
                        chkBoxIsParentItem.Checked = bool.Parse(hdnIsParent.Value);
                    }
                    if (bool.Parse(hdnIsParent.Value) && Int32.Parse(hdnPortalID.Value) == GetPortalID)
                    {
                        imgDelete.Visible = false;
                    }
                    if (hdnSEOName.Value.ToLower() == "default")
                    {
                        imgDelete.Visible = false;
                        imgEdit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvPortal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                int portalID = int.Parse(gdvPortal.DataKeys[rowIndex]["PortalID"].ToString());
                string PortalName = gdvPortal.DataKeys[rowIndex]["Name"].ToString();
                bool IsParent = bool.Parse(gdvPortal.DataKeys[rowIndex]["IsParent"].ToString());

                if (e.CommandName == "EditPortal")
                {
                    Flag = false;
                    if (PortalName.ToLower() != "default")
                    {
                        //if (IsParent)
                        //{

                        //}

                        //else
                        //{
                        ddlAvailablePortal.Visible = false;
                        lblAvailablePortal.Visible = false;
                        lblUrl.Visible = false;
                        txtUrl.Visible = false;
                        //}


                        TabPanelPortalModulesManagement.Visible = true;
                        gdvPortalModulesLists.PageIndex = 0;
                        EditPortal(portalID);
                        PanelVisibility(true, false);
                        hdnPortalIndex.Value = portalID.ToString();
                        txtPortalName.Visible = true;
                        lblDefaultPortal.Visible = false;
                        imgSave.Visible = true;
                        //lblSave.Visible = true;
                    }
                    else if (PortalName.ToLower() == "default")
                    {
                        TabPanelPortalModulesManagement.Visible = true;
                        gdvPortalModulesLists.PageIndex = 0;
                        trEmail.Visible = false;
                        PortalInfo portal = PortalController.GetPortalByPortalID(portalID, GetUsername);
                        txtPortalName.Enabled = portal.Name.Equals("Default") ? false : true;
                        txtPortalName.Visible = false;
                        lblDefaultPortal.Visible = true;
                        lblDefaultPortal.Text = portal.Name;
                        hdnPortalID.Value = portalID.ToString();
                        BindPortalModulesListsGrid(Int32.Parse(hdnPortalID.Value));
                        PanelVisibility(true, false);
                        hdnPortalIndex.Value = portalID.ToString();
                        imgSave.Visible = false;
                        //lblSave.Visible = false;
                    }
                }
                else if (e.CommandName == "DeletePortal")
                {
                    DeletePortal(portalID);
                    HttpRuntime.Cache.Remove(CacheKeys.Portals);
                    BindPortal();
                    //BindSitePortal();
                    PanelVisibility(false, true);
                    string target_dir = Utils.GetTemplatePath(PortalName);
                    if (Directory.Exists(target_dir))
                        Utils.DeleteDirectory(target_dir);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void EditPortal(Int32 portalID)
        {

            trEmail.Visible = false;
            PortalInfo portal = PortalController.GetPortalByPortalID(portalID, GetUsername);
            txtPortalName.Enabled = portal.Name.Equals("Default") ? false : true;
            txtPortalName.Text = portal.Name;
            txtUrl.Text = portal.SEOName;
            GetParentPortalList();
            string ParentPortalName = portal.ParentPortalName.ToString();
            if (portal.IsParent == true)

                ddlAvailablePortal.SelectedIndex = 0;
            else
                if (ParentPortalName != string.Empty)
                    ddlAvailablePortal.Items.FindByText(ParentPortalName).Selected = true;
                else ddlAvailablePortal.SelectedIndex = 0;
            hdnPortalID.Value = portalID.ToString();
            BindPortalModulesListsGrid(Int32.Parse(hdnPortalID.Value));
        }

        private void DeletePortal(Int32 portalID)
        {
            PortalInfo objInfo = PortalController.GetPortalByPortalID(portalID, GetUsername);
            txtPortalName.Text = objInfo.Name;
            PortalController.DeletePortal(portalID, GetUsername);
            ShowMessage("", GetSageMessage("PortalSettings", "PortalDeleteSuccessfully"), "", SageMessageType.Success);
        }

        private void SaveProtal()
        {
            if (Int32.Parse(hdnPortalID.Value) > 0)
            {


                bool IsParent = ddlAvailablePortal.SelectedIndex == 0 ? true : false;
                int ParentID = ddlAvailablePortal.SelectedIndex == 0 ? 0 : int.Parse(ddlAvailablePortal.SelectedValue);
                string PortalURL = ddlAvailablePortal.SelectedIndex == 0 ? txtUrl.Text : txtPortalName.Text;
                PortalController.UpdatePortal(Int32.Parse(hdnPortalID.Value), txtPortalName.Text, IsParent, GetUsername, PortalURL, ParentID);
            }
            else
            {
                if (HttpRuntime.Cache["AspxStoreSetting" + GetPortalID.ToString() + GetStoreID.ToString()] != null)
                {
                    string newpassword = GetRandomPassword(6);
                    int passowrdformat = 2;
                    string password;
                    int? customerID = 0;
                    string passwordsalt, portalUrl;
                    string portalName = txtPortalName.Text.Trim().Replace(" ", "_");
                    portalUrl = Request.ServerVariables["SERVER_NAME"] + "/portal/" + portalName + "/" + "home" + SageFrameSettingKeys.PageExtension;
                    PasswordHelper.EnforcePasswordSecurity(passowrdformat, newpassword, out password,
                                                     out passwordsalt);
                    string email = txtEmail.Text.Trim();
                    SaveTemplate();
                    customerID = PortalMgrController.AddStoreSubscriber(portalName, "", "", email, null, false, false,
                                                           "superuser",
                                                           password, passwordsalt, passowrdformat, false);
                    try
                    {
                        sendEmail(customerID, newpassword, portalName, txtEmail.Text.Trim(), "superuser", "",
                                  portalUrl);
                        ShowMessage(SageMessageTitle.Information.ToString(),
                                    GetSageMessage("PortalSettings", "PortalSavedAndEmailSendSuccessfully"), "",
                                    SageMessageType.Success);
                    }
                    catch
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(),
                                    GetSageMessage("PortalSettings", "PortalAddEmailSendProblem"), "",
                                    SageMessageType.Alert);
                    }
                }
                else
                {
                    string newPortalname = SaveTemplate();
                    int ParentPortal = int.Parse(ddlAvailablePortal.SelectedValue);
                    // string PSEOName = txtUrl.Text == string.Empty ? ddlAvailablePortal.SelectedItem.ToString() : txtUrl.Text;
                    string PSEOName = string.Empty;
                    if (txtUrl.Text != string.Empty)
                        PSEOName = txtUrl.Text.Contains("http://") || txtUrl.Text.Contains("https://") ? txtUrl.Text : "http://" + txtUrl.Text;
                    else
                        PSEOName = txtPortalName.Text;
                    bool IsParent = false;
                    //PortalMgrController.AddPortal(txtPortalName.Text, false, GetUsername, newPortalname);
                    if (ddlAvailablePortal.SelectedIndex == 0)
                    {
                        IsParent = true;
                    }
                    PortalMgrController.AddPortal(txtPortalName.Text, IsParent, GetUsername, newPortalname, ParentPortal, PSEOName);
                    ShowMessage(SageMessageTitle.Information.ToString(),
                                 GetSageMessage("PortalSettings", "PortalSaveSuccessfully"), "",
                                 SageMessageType.Success);
                }
            }
        }

        private void BindPortalModulesListsGrid(int PortalID)
        {
            gdvPortalModulesLists.DataSource = PortalController.GetPortalModulesByPortalID(PortalID, GetUsername);
            gdvPortalModulesLists.DataBind();
        }

        protected void gdvPortalModulesLists_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvPortalModulesLists.PageIndex = e.NewPageIndex;
            BindPortalModulesListsGrid(Int32.Parse(hdnPortalID.Value));
        }

        protected void gdvPortalModulesLists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                HiddenField hdnIsAdmin = (HiddenField)e.Row.FindControl("hdnIsAdmin");

                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvPortalModulesLists.ClientID + "','cssCheckBoxIsActiveItem');");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                if (bool.Parse(hdnIsAdmin.Value))
                {
                    chkIsActiveItem.Disabled = true;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                //chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvPortalModulesLists.ClientID + "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void gdvPortalModulesLists_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gdvPortalModulesLists_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void imbBtnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string seletedModulesID = string.Empty;
                string IsActive = string.Empty;
                int SelectedPortalID = Int32.Parse(hdnPortalID.Value);
                for (int i = 0; i < gdvPortalModulesLists.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvPortalModulesLists.Rows[i].FindControl("chkBoxIsActiveItem");
                    HiddenField hdnModuleID = (HiddenField)gdvPortalModulesLists.Rows[i].FindControl("hdnModuleID");
                    seletedModulesID = seletedModulesID + hdnModuleID.Value.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedModulesID.Length > 1 && IsActive.Length > 0)
                {
                    seletedModulesID = seletedModulesID.Substring(0, seletedModulesID.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    PortalController.UpdatePortalModules(seletedModulesID, IsActive, SelectedPortalID, GetUsername);
                    ShowMessage("", GetSageMessage("PortalModules", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        /// <summary>
        /// Added by bj to change the Portal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnPortalSave_Click(object sender, EventArgs e)
        //{
        //    int portalID = GetPortalID;
        //    if (int.Parse(ddlPortalName.SelectedItem.Value) != 1)
        //    {
        //        SettingProvider sageSP = new SettingProvider();
        //        sageSP.SaveSageSetting(SettingType.SiteAdmin.ToString(), SageFrameSettingKeys.SuperUserPortalId,
        //            ddlPortalName.SelectedItem.Value, GetUsername, portalID.ToString());
        //        sageSP.ChangePortal(int.Parse(ddlPortalName.SelectedItem.Value));
        //        BindPortal();
        //        HttpRuntime.Cache.Remove(CacheKeys.Portals);
        //        HttpRuntime.Cache.Remove(CacheKeys.SageSetting);
        //        Response.Redirect(Request.Url.ToString());
        //    }
        //    else
        //    {
        //        ShowMessage("Current Portal", "", "The portal you want to make Parent is existing parent portal", SageMessageType.Alert);
        //    }
        //}

        public string GetRandomPassword(int length)
        {
            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }

        public void sendEmail(int? custID, string newpassword, string portalName, string userEmail, string firstname, string lastname, string portalUrl)
        {
            StringBuilder emailbody = new StringBuilder();
            portalUrl = "http://" + portalUrl;
            emailbody.Append(
                "<table align=\"center\" width=\"700\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"background-color: #f5f5f5;font: 12px Tahoma, Geneva, sans-serif;color: #797979;text-shadow: 1px 1px 0px #fff;\"> <tbody> <tr> <td> <table width=\"700\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:0px auto; background: #f5f5f5\"> <tbody> <tr> <td valign=\"top\" colspan=\"1\"> <table width=\"622\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin:0px auto; background=#f5f5f5\"> <tbody> <tr> <td width=\"473\"><a href=\"#\"><img width=\"148\" height=\"81\" style=\"border:none;\" alt=\"logo\" src=\"http://www.aspxcommerce.com/Upload/image/logo.png\" /></a></td> <td width=\"139\" style=\"text-align:right\">&nbsp;</td> </tr> <tr> <td colspan=\"2\">&nbsp;</td> </tr> <tr> <td colspan=\"2\"> <p style=\"margin:10px 0 10px 0; line-height:20px\">Dear <b>");

            emailbody.Append(firstname + " " + lastname + ",");
            emailbody.Append(
                "<br /></b></p></td></tr><tr><td valign=\"top\" colspan=\"2\">&nbsp;</td> </tr> <tr> <td valign=\"top\" colspan=\"2\"> <p style=\"margin:0 0 15px 0\"><a target=\"_blank\" href=\"http://www.aspxcommerce.com\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" title=\"AspxCommerce\"><span style=\"mso-bidi-font-family: Arial;color:black\">Congratulation, Your AspxCommerce Store has been created successfully.</span></a></p> </td> </tr> <tr> <td valign=\"top\" colspan=\"2\"> <p style=\"margin:0px\">&nbsp;</p> </td> </tr> <tr> <td height=\"15\" colspan=\"2\">To sign in to your account, please visit <a target='_blank' href='" + portalUrl + "'>" + portalUrl + "</a>");
            emailbody.Append(
                "</td></tr><tr><td height=\"15\" colspan=\"2\"><br /><strong>Your Store Login Information: </strong></td> </tr> <tr> <td height=\"15\" colspan=\"2\"> Your Store Url : ");
            emailbody.Append("<a target='_blank' href='" + portalUrl + "'>" + portalUrl + "</a>");
            emailbody.Append(
                "</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><tr><td height=\"15\" colspan=\"2\"><strong> For Frontend Login: </strong></td></tr><td height=\"15\" colspan=\"2\">UserName: ");
            emailbody.Append("customer_" + custID.ToString());
            emailbody.Append("</td></tr><tr><td height=\"15\" colspan=\"2\"> Password: ");
            emailbody.Append(newpassword);

            emailbody.Append("</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><tr><td height=\"15\" colspan=\"2\"><strong> For Backend Login: </strong></td> </tr><td height=\"15\" colspan=\"2\"> Username: ");

            emailbody.Append("storeadmin_" + custID.ToString());
            emailbody.Append("</td></tr><tr><td height=\"15\" colspan=\"2\"> Password: ");
            emailbody.Append(newpassword);

            emailbody.Append(
                "</td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><td colspan=\"2\"><strong>If you have any questions regarding your account, click 'Reply' in your email client and we'll be only too happy to help. </strong></td></tr><tr><td colspan=\"2\">&nbsp;</td></tr><tr><td colspan=\"2\"><table width=\"661\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"> <tbody> <tr> <td width=\"217\">Best Regards,<br /> <strong style=\"color:#282929\">AspxCommerce Team</strong><br /> <a title=\"AspxCommerce\" style=\"text-decoration: none;color: #226ab7;font-style: italic;\" href=\"http://www.aspxcommerce.com\" target=\"_blank\">http://www.aspxcommerce.com</a></td> <td width=\"401\">&nbsp;</td> </tr> <tr> <td style=\"border-top:0px solid #cacaca; font: 10px Tahoma, Geneva, sans-serif;\" colspan=\"2\">&nbsp;</td> </tr> <tr> <td style=\"border-top:1px solid #cacaca; font: 5px Tahoma, Geneva, sans-serif;\" colspan=\"2\">&nbsp;</td> </tr> </tbody> </table> </td> </tr> <tr> <td colspan=\"1\"><span style=\"font: italic 11px Tahoma, Geneva, sans-serif\">This message is confidential and intended for the recipient only. It is not allowed to copy this message, or to make it accessible for third parties. If you are not the intended recipient, please notify the sender by email.</span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table>");
            SageFrameConfig pagebase = new SageFrameConfig();
            string emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            string emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            string SendEcommerceEmailsFrom = Session["SendEcommerceEmailsFrom"].ToString();
            MailHelper.SendMailNoAttachment(SendEcommerceEmailsFrom, userEmail, "Thank You, for subscribing with AspxCommerce.", emailbody.ToString(), emailSuperAdmin, emailSiteAdmin);
        }



        //---------------------------------------ParentPortal-----------------------------------------------

        public void GetParentPortalList()
        {


            try
            {
                List<PortalInfo> objPortalInfo = new List<PortalInfo>();
                PortalController objController = new PortalController();
                objPortalInfo = objController.GetParentPortalList();

                ddlAvailablePortal.DataSource = objPortalInfo;
                ddlAvailablePortal.DataTextField = "SEOName";
                ddlAvailablePortal.DataValueField = "PortalID";
                ddlAvailablePortal.DataBind();
                ddlAvailablePortal.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        protected void ddlAvailablePortal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAvailablePortal.SelectedIndex == 0)
            {
                lblUrl.Visible = true;
                txtUrl.Visible = true;
            }
            else
            {
                lblUrl.Visible = false;
                txtUrl.Visible = false;
                txtUrl.Text = string.Empty;
            }
        }
    }
}