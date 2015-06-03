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
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Data;
using SageFrame.Framework;
using System.Web.UI.HtmlControls;
using SageFrame.Packages;
using System.Xml;
using System.Collections;
using System.IO;
using SageFrame.SageFrameClass.Services;
using SageFrame.Common;
#endregion

namespace SageFrame.Modules.Admin.Extensions
{
    /// <remark>
    /// This is modulepackage user control
    /// </remark>
    public partial class ctl_Extensions : BaseAdministrationUserControl
    {
        CommonFunction LToDCon = new CommonFunction();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["extension"] != null)
            {
                string ControlSrc = Request.QueryString["extension"].ToString();
                LoadControl(new Random().Next().ToString(), false, ExtensionPlaceHolder, ControlSrc);
            }
        }

        public void LoadControl(string UpdatePanelIDPrefix, bool IsPartialRendring, PlaceHolder ContainerControl, string ControlSrc)
        {
            SageUserControl ctl;
            if (ControlSrc.ToLower().EndsWith(".ascx"))
            {
                if (IsPartialRendring)
                {
                    UpdatePanel udp = CreateUpdatePanel(UpdatePanelIDPrefix, UpdatePanelUpdateMode.Always, ContainerControl.Controls.Count);
                    ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                    udp.ContentTemplateContainer.Controls.Add(ctl);
                    ContainerControl.Controls.Add(udp);
                }
                else
                {
                    ctl = this.Page.LoadControl("~" + ControlSrc) as SageUserControl;
                    ContainerControl.Controls.Clear();
                    ContainerControl.Controls.Add(ctl);
                }
            }
            else
            {
            }

        }

        public UpdatePanel CreateUpdatePanel(string Prefix, UpdatePanelUpdateMode Upm, int PaneUpdatePanelCount)
        {
            UpdatePanel udp = new UpdatePanel();
            udp.UpdateMode = Upm;
            PaneUpdatePanelCount++;
            udp.ID = "_udp_" + "_" + PaneUpdatePanelCount + Prefix;
            return udp;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Request.QueryString["ExtensionMessage"] != null && HttpContext.Current.Request.QueryString["ExtensionMessage"].ToString() != "")
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), HttpContext.Current.Request.QueryString["ExtensionMessage"].ToString(), "", SageMessageType.Success);
                }
                if (!IsPostBack && Request.QueryString["extension"] == null)
                {
                    
                    BindGrid(string.Empty);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

      

        /// <summary>
        /// This is used to bind the gridview with package info
        /// </summary>
        private void BindGrid(string searchText)
        {
            try
            {
                gdvExtensions.PageSize = int.Parse(ddlRecordsPerPage.Text);
                gdvExtensions.DataSource = PackagesController.GetPackagesByPortalID(GetPortalID, searchText);
                gdvExtensions.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbInstallModule_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/ctl_ModuleInstaller.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }

        protected void imbCreateNewModule_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/Editors/EditModuleDefinition.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }
        protected void imbAvailableModules_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/ctl_AvailableModules.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }
        protected void imbCreateCompositeModule_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/Editors/EditPackageDefinition.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }
        protected void imbCreatePackage_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/Editors/PackageCreator.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }
        protected void imbDownloadModules_Click(object sender, ImageClickEventArgs e)
        {
            string ControlPath = "/Modules/Admin/Extensions/Editors/DownLoadModules.ascx";
            ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
        }
        
        public string ConvertToYesNo(string i)
        {
            string flag = "No";
            if (i == "1")
            {
                flag = "Yes";
            }
            return flag;
        }

        protected void gdvExtensions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            HttpContext.Current.Session[SessionKeys.moduleid] = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    string strURL = string.Empty;
                    string redmain = Request.RawUrl;
                    if (redmain.Contains("ExtensionMessage"))
                        redmain = redmain.Remove(redmain.IndexOf("ExtensionMessage") - 1);
                    //To direct to new page with query string
                    string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleEditor.ascx";

                    if (redmain.Contains("?"))
                    {
                        // Location of the letter ?.
                        int i = redmain.IndexOf('?');
                        // Remainder of string starting at '?'.
                        string d = redmain.Substring(i);
                        strURL = redmain + "?extension=" + ControlPath + "&modulecode=" + HttpContext.Current.Session[SessionKeys.moduleid];

                    }
                    else
                    {
                        strURL = redmain + "?extension=" + ControlPath + "&modulecode=" + HttpContext.Current.Session[SessionKeys.moduleid];
                    }
                    Response.Redirect(strURL);
                    break;
                case "Delete":
                    int moduleID = int.Parse(e.CommandArgument.ToString());
                    try
                    {
                        //Uninstall Module
                        ModuleController objController = new ModuleController();
                        ModuleInfo moduleInfo = objController.GetModuleInformationByModuleID(moduleID);
                        UninstallModule(moduleInfo, true);
                        //Delete module from database
                        objController.DeletePackagesByModuleID(GetPortalID, moduleID);
                        BindGrid(string.Empty);
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Extensions", "ModuleIsDeletedSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                    break;
            }
        }

        protected void gdvExtensions_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gdvExtensions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gdvExtensions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvExtensions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvExtensions.PageIndex = e.NewPageIndex;
            BindGrid(txtSearchText.Text);
        }

        protected void gdvExtensions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("imbDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("Extensions", "AreYouSureToDelete") + "')");

                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");

                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvExtensions.ClientID + "','cssCheckBoxIsActiveItem');");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);

                HiddenField hdnIsAdmin = (HiddenField)e.Row.FindControl("hdnIsAdmin");
                chkIsActiveItem.Disabled = bool.Parse(hdnIsAdmin.Value);
                if (chkIsActiveItem.Disabled == true)
                {
                    chkIsActiveItem.Attributes.Add("class", "disabledClass");
                    LinkButton imbDelete = (LinkButton)e.Row.FindControl("imbDelete");
                    imbDelete.Visible = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                //chkIsActiveHeader.Attributes.Add("onclick", "javascript:SelectAllCheckboxesSpecific(this,'" + gdvExtensions.ClientID + "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void imbBtnSaveChanges_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string seletedModulesID = string.Empty;
                string IsActive = string.Empty;

                for (int i = 0; i < gdvExtensions.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvExtensions.Rows[i].FindControl("chkBoxIsActiveItem");
                    HiddenField hdnModuleID = (HiddenField)gdvExtensions.Rows[i].FindControl("hdnModuleID");
                    seletedModulesID = seletedModulesID + hdnModuleID.Value.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedModulesID.Length > 1 && IsActive.Length > 0)
                {
                    seletedModulesID = seletedModulesID.Substring(0, seletedModulesID.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    PackagesProvider.UpdatePackagesChange(seletedModulesID, IsActive, GetUsername);
                    ShowMessage("", GetSageMessage("Extensions", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }


        protected void imgSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid(txtSearchText.Text);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            gdvExtensions.PageSize = int.Parse(ddlRecordsPerPage.SelectedValue.ToString());
            BindGrid(txtSearchText.Text);
        }

        #region Uninstall Existing Module

        string Exceptions = string.Empty;
        private void UninstallModule(ModuleInfo moduleInfo, bool deleteModuleFolder)
        {
            Installers installerClass = new Installers();
            string path = HttpContext.Current.Server.MapPath("~/");

            //checked if directory exist for current module foldername
            string moduleFolderPath = path + "modules\\" + moduleInfo.FolderName;
            if (!string.IsNullOrEmpty(moduleFolderPath))
            {
                if (Directory.Exists(moduleFolderPath))
                {
                    //check for valid .sfe file exist or not
                    string fileName = installerClass.checkFormanifestFile(moduleFolderPath, moduleInfo);
                    if (fileName != "")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(moduleFolderPath + '\\' + fileName);
                        XmlElement root = doc.DocumentElement;
                        if (installerClass.checkValidManifestFile(root, moduleInfo))
                        {
                            XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                            foreach (XmlNode xn in xnList)
                            {
                                moduleInfo.ModuleName = xn["modulename"].InnerXml.ToString();
                                moduleInfo.FolderName = xn["foldername"].InnerXml.ToString();
                            }
                            try
                            {
                                if (moduleInfo.ModuleID > 0)
                                {
                                    //Run script  
                                    ReadUninstallScriptAndDLLFiles(doc, moduleFolderPath, installerClass);

                                    if (deleteModuleFolder == true)
                                    {
                                        //Delete Module's Folder
                                        //installerClass.DeleteTempDirectory(moduleInfo.InstalledFolderPath);
                                        Directory.Delete(moduleFolderPath, true);

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Exceptions = ex.Message;
                            }

                            if (Exceptions != string.Empty)
                            {
                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ModuleExtensionIsUninstallError"), "", SageMessageType.Alert);
                            }
                            else
                            {
                                string ExtensionMessage = GetSageMessage("Extensions", "ModuleExtensionIsUninstalledSuccessfully");
                                //UninstallProcessCancelRequestBase(Request.RawUrl, true, ExtensionMessage);
                            }
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                            if (Directory.Exists(moduleFolderPath))
                            {
                                Directory.Delete(moduleFolderPath, true);
                            }
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageDoesNotAppearToBeValid"), "", SageMessageType.Alert);
                        if (Directory.Exists(moduleFolderPath))
                        {
                            Directory.Delete(moduleFolderPath, true);
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(moduleFolderPath))
                    {
                        Directory.Delete(moduleFolderPath, true);
                    }
                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions", "ModuleFolderDoesnotExist"), "", SageMessageType.Error);
                }
            }
        }

        private void ReadUninstallScriptAndDLLFiles(XmlDocument doc, string moduleFolderPath, Installers installerClass)
        {
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                ArrayList dllFiles = new ArrayList();
                string _unistallScriptFile = string.Empty;
                XmlNodeList xnFileList = doc.SelectNodes("sageframe/folders/folder/files/file");
                if (xnFileList.Count != 0)
                {
                    foreach (XmlNode xn in xnFileList)
                    {
                        string _fileName = xn["name"].InnerXml;
                        try
                        {
                            #region CheckAlldllFiles
                            if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains(".dll"))
                            {
                                dllFiles.Add(_fileName);
                            }
                            #endregion
                            #region ReadUninstall SQL FileName
                            if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains("Uninstall.SqlDataProvider"))
                            {
                                _unistallScriptFile = _fileName;
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Exceptions = ex.Message;
                        }
                    }
                    if (_unistallScriptFile != "")
                    {
                        RunUninstallScript(_unistallScriptFile, moduleFolderPath, installerClass);
                    }

                    // check IModuleExtraCodeExecute interface is implemented or not for install/unInstall of module
                    //if (installerClass.IsIModuleExtraCodeInterfaceImplemented(doc))
                    //{
                    //    installerClass.ExtraCodeOnUnInstallation(doc);
                    //}                    

                    DeleteAllDllsFromBin(dllFiles, moduleFolderPath);
                }
            }
        }

        private void RunUninstallScript(string _unistallScriptFile, string moduleFolderPath, Installers installerClass)
        {
            Exceptions = installerClass.ReadSQLFile(moduleFolderPath, _unistallScriptFile);
        }

        private void DeleteAllDllsFromBin(ArrayList dllFiles, string moduleFolderPath)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/");

                foreach (string dll in dllFiles)
                {
                    string targetdllPath = path + SageFrame.Common.RegisterModule.Common.DLLTargetPath + '\\' + dll;
                    FileInfo imgInfo = new FileInfo(targetdllPath);
                    if (imgInfo != null)
                    {
                        imgInfo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions = ex.Message;
            }
        }

        #endregion
    }
}