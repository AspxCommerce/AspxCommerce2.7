/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.SageFrameClass;
using SageFrame.Modules;
using SageFrame.ModuleControls;
using SageFrame.SageFrameClass.Services;
using System.IO;
using RegisterModule;
using System.Xml;
using System.Collections;

namespace SageFrame.Modules.Admin.Extensions.Editors
{
    public partial class ModuleEditor : BaseAdministrationUserControl
    {
        string Exceptions = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Request.QueryString["ExtensionMessage"] != null && HttpContext.Current.Request.QueryString["ExtensionMessage"].ToString() != "")
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), HttpContext.Current.Request.QueryString["ExtensionMessage"].ToString(), "", SageMessageType.Success);
                }
                if (!IsPostBack)
                {
                    imbUninstall.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("Extensions", "ConfirmUninstallExtension") + "')");

                    AddScript();
                    if (HttpContext.Current.Session["moduleid"] != null)
                    {
                        BindControls();
                    }
                    if (HttpContext.Current.Session["ActiveTabID"] != null)
                    {
                        TabContainerManageModules.ActiveTabIndex = Int32.Parse(HttpContext.Current.Session["ActiveTabID"].ToString());
                        HttpContext.Current.Session["ActiveTabID"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }



        private void AddScript()
        {
            txtDefaultCacheTime.Attributes.Add("OnKeydown", "return NumberKey(event)");
        }

        private void BindModuleDefDropDown()
        {
            DataSet dsModuleDef = new DataSet();
            dsModuleDef = GetExtensionSettings(HttpContext.Current.Session["moduleid"].ToString());
            try
            {
                // Get ModuleDefinitions data table
                DataTable dtModuleDefinitions = dsModuleDef.Tables[1];
                if (dtModuleDefinitions != null)
                {
                    udpDefinitions.Visible = true;
                    ddlDefinitions.DataTextField = dtModuleDefinitions.Columns[1].Caption;
                    ddlDefinitions.DataValueField = dtModuleDefinitions.Columns[0].Caption;
                    ddlDefinitions.DataSource = dtModuleDefinitions;
                    ddlDefinitions.DataBind();

                    txtFriendlyName.Text = dtModuleDefinitions.Rows[0]["FriendlyName"].ToString();
                    hdnModuleDefinition.Value = txtFriendlyName.Text;
                    hdfModuleDefID.Value = dtModuleDefinitions.Rows[0]["ModuleDefID"].ToString();
                    txtDefaultCacheTime.Text = dtModuleDefinitions.Rows[0]["DefaultCacheTime"].ToString();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        private void BindControls()
        {
            try
            {
                DataSet dsExtensionSettings = new DataSet();
                dsExtensionSettings = GetExtensionSettings(HttpContext.Current.Session["moduleid"].ToString());
                if (dsExtensionSettings.Tables.Count > 0)
                {
                    try
                    {
                        // Get Modules data table
                        DataTable dtModules = dsExtensionSettings.Tables[0];
                        if (dtModules != null)
                        {
                            lblModuleNameD.Text = dtModules.Rows[0]["FriendlyName"].ToString();
                            hdnModuleName.Value = lblModuleNameD.Text;
                            txtFolderName.Text = dtModules.Rows[0]["FolderName"].ToString();
                            txtBusinessControllerClass.Text = dtModules.Rows[0]["BusinessControllerClass"].ToString();
                            txtDependencies.Text = dtModules.Rows[0]["Dependencies"].ToString();
                            txtPermissions.Text = dtModules.Rows[0]["Permissions"].ToString();
                            chkIsPremium.Checked = (bool)dtModules.Rows[0]["IsPremium"];
                        }
                        auditBar.Visible = true;
                        lblCreatedBy.Visible = true;
                        lblCreatedBy.Text = GetSageMessage("Extensions", "CreatedBy ") + dtModules.Rows[0]["AddedBy"].ToString() + " " + dtModules.Rows[0]["AddedOn"].ToString();
                        if (dtModules.Rows[0]["UpdatedBy"].ToString() != "" && dtModules.Rows[0]["UpdatedOn"].ToString() != "")
                        {
                            lblUpdatedBy.Visible = true;
                            lblUpdatedBy.Text = GetSageMessage("Extensions", "LastUpdatedBy ") + dtModules.Rows[0]["UpdatedBy"].ToString() + " " + dtModules.Rows[0]["UpdatedOn"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                    try
                    {
                        // Get ModuleDefinitions data table
                        DataTable dtModuleDefinitions = dsExtensionSettings.Tables[1];
                        if (dtModuleDefinitions != null)
                        {
                            udpDefinitions.Visible = true;
                            ddlDefinitions.DataTextField = dtModuleDefinitions.Columns[1].Caption;
                            ddlDefinitions.DataValueField = dtModuleDefinitions.Columns[0].Caption;
                            ddlDefinitions.DataSource = dtModuleDefinitions;
                            ddlDefinitions.DataBind();

                            txtFriendlyName.Text = dtModuleDefinitions.Rows[0]["FriendlyName"].ToString();
                            hdnModuleDefinition.Value = txtFriendlyName.Text;
                            hdfModuleDefID.Value = dtModuleDefinitions.Rows[0]["ModuleDefID"].ToString();
                            txtDefaultCacheTime.Text = dtModuleDefinitions.Rows[0]["DefaultCacheTime"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                    try
                    {
                        // Get ModuleControls data table
                        DataTable dtModuleControls = dsExtensionSettings.Tables[2];
                        if (dtModuleControls != null)
                        {
                            gdvControls.Visible = true;
                            gdvControls.DataSource = dtModuleControls;
                            gdvControls.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                    try
                    {
                        // Get Packages data table
                        DataTable dtPackages = dsExtensionSettings.Tables[3];
                        if (dtPackages != null)
                        {
                            PackageDetails1.PackageName = dtPackages.Rows[0]["FriendlyName"].ToString();
                            PackageDetails1.Description = dtPackages.Rows[0]["Description"].ToString();
                            PackageDetails1.License = dtPackages.Rows[0]["License"].ToString();
                            PackageDetails1.ReleaseNotes = dtPackages.Rows[0]["ReleaseNotes"].ToString();
                            PackageDetails1.Owner = dtPackages.Rows[0]["Owner"].ToString();
                            PackageDetails1.Organization = dtPackages.Rows[0]["Organization"].ToString();
                            PackageDetails1.Url = dtPackages.Rows[0]["Url"].ToString();
                            PackageDetails1.Email = dtPackages.Rows[0]["Email"].ToString();

                            //version bind 
                            string _version = dtPackages.Rows[0]["Version"].ToString();
                            string[] version = SeparateByDots(_version);

                            //To bind with dropdown
                            PackageDetails1.FirstVersion = version[0];
                            PackageDetails1.SecondVersion = version[1];
                            PackageDetails1.LastVersion = version[2];
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private string[] SeparateByDots(string _version)
        {
            string[] arrColl = _version.Split('.');
            return arrColl;
        }
        protected void imbAddControl_Click(object sender, EventArgs e)
        {
            int Id = 0;
            if (gdvControls.Rows.Count > 0)
            {
                HiddenField hdnModuleDefID = gdvControls.Rows[0].FindControl("hdnModuleDefID") as HiddenField;
                Id = int.Parse(hdnModuleDefID.Value.ToString());
            }
            else
            {
                Id = int.Parse(hdfModuleDefID.Value.ToString());
            }
            if (Id != 0)
            {
                string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleControlsDetails.ascx&moduledef=" + Id;

                HttpContext.Current.Session["ModuleName"] = hdnModuleName.Value;
                HttpContext.Current.Session["ModuleDefinitionName"] = hdnModuleDefinition.Value;
                ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
            }
        }
        protected void gdvControls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Id = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Edit":
                    //To direct to new page with query string
                    string ControlPath = "/Modules/Admin/Extensions/Editors/ModuleControlsDetails.ascx&modulecontrol=" + Id;
                    HttpContext.Current.Session["ModuleName"] = hdnModuleName.Value;
                    HttpContext.Current.Session["ModuleDefinitionName"] = hdnModuleDefinition.Value;
                    ProcessSourceControlUrl(Request.RawUrl, ControlPath, "extension");
                    break;
                case "Delete":
                    try
                    {
                        ModuleController objController = new ModuleController();
                        objController.ModuleControlsDeleteByModuleControlID(Id, GetUsername);

                        DataSet dsExtensionSettings = new DataSet();
                        if (HttpContext.Current.Session["moduleid"] != null)
                        {
                            dsExtensionSettings = GetExtensionSettings(HttpContext.Current.Session["moduleid"].ToString());
                            DataTable dtModuleControls = dsExtensionSettings.Tables[2];
                            if (dtModuleControls != null)
                            {
                                gdvControls.Visible = true;
                                gdvControls.DataSource = dtModuleControls;
                                gdvControls.DataBind();
                            }
                        }
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Extensions", "ModuleControlIsDeletedSuccessfully"), "", SageMessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                    break;
            }
        }

        protected void ddlDefinitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeDefinitonsInfos();
        }

        private void ChangeDefinitonsInfos()
        {

        }
        protected void imbUpdateDefinition_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController objController = new ModuleController();
                objController.UpdateModuleDefinitions(int.Parse(ddlDefinitions.SelectedValue), txtFriendlyName.Text, int.Parse(txtDefaultCacheTime.Text), true, true, DateTime.Now, GetPortalID, GetUsername);
                BindModuleDefDropDown();
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Extensions", "ModuleDefinitionIsUpdatedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbUpdate_Click(object sender, EventArgs e)
        {
            ModuleInfo objModule = new ModuleInfo();
            objModule.ModuleID = int.Parse(HttpContext.Current.Session["moduleid"].ToString());
            objModule.FolderName = txtFolderName.Text;
            objModule.BusinessControllerClass = txtBusinessControllerClass.Text;
            objModule.dependencies = txtDependencies.Text;
            objModule.permissions = txtPermissions.Text;

            objModule.IsPortable = chkIsPortable.Checked;
            objModule.IsSearchable = chkIsSearchable.Checked;
            objModule.IsUpgradable = chkIsUpgradable.Checked;
            objModule.isPremium = chkIsPremium.Checked;

            objModule.PackageName = PackageDetails1.PackageName;
            objModule.PackageDescription = PackageDetails1.Description;
            objModule.Version = PackageDetails1.FirstVersion + '.' + PackageDetails1.SecondVersion + '.' + PackageDetails1.LastVersion;
            objModule.License = PackageDetails1.License;
            objModule.ReleaseNotes = PackageDetails1.ReleaseNotes;

            objModule.Owner = PackageDetails1.Owner;
            objModule.Organization = PackageDetails1.Organization;
            objModule.URL = PackageDetails1.Url;
            objModule.Email = PackageDetails1.Email;
            objModule.PortalID = GetPortalID;
            objModule.Username = GetUsername;

            ModuleController objController = new ModuleController();
            objController.UpdateExtension(objModule);
            string ExtensionMessage = GetSageMessage("Extensions", "ModuleExtensionIsUpdatedSuccessfully");
            ProcessCancelRequestBase(Request.RawUrl, true, ExtensionMessage);

        }

        protected void gdvControls_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void imbCancel_Click(object sender, EventArgs e)
        {
            ProcessCancelRequest(Request.RawUrl);
        }


        protected void imbUninstall_Click(object sender, EventArgs e)
        {
            UninstallModule();
        }

        private void UninstallModule()
        {
            ModuleInfo module = new ModuleInfo();
            Installers installerClass = new Installers();
            string path = HttpContext.Current.Server.MapPath("~/");

            //checked if directory exist for current module foldername
            if (!string.IsNullOrEmpty(txtFolderName.Text))
            {
                string moduleFolderPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + txtFolderName.Text;
                if (Directory.Exists(moduleFolderPath))
                {
                    //check for valid .sfe file exist or not
                    if (installerClass.checkFormanifestFile(moduleFolderPath, module) != "")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(moduleFolderPath + '\\' + module.ManifestFile);
                        XmlElement root = doc.DocumentElement;
                        if (installerClass.checkValidManifestFile(root, module))
                        {
                            XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                            foreach (XmlNode xn in xnList)
                            {
                                module.ModuleName = xn["modulename"].InnerXml.ToString();
                                module.FolderName = xn["foldername"].InnerXml.ToString();

                                if (!String.IsNullOrEmpty(module.ModuleName) && !String.IsNullOrEmpty(module.FolderName) && hdnModuleName.Value == module.ModuleName && txtFolderName.Text == module.FolderName && installerClass.IsModuleExist(module.ModuleName.ToLower(), module))
                                {
                                    string moduleInstalledPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + module.FolderName;
                                    module.InstalledFolderPath = moduleInstalledPath;
                                }
                                else
                                {
                                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions", "ThisModuleSeemsToBeCorrupted"), "", SageMessageType.Error);
                                }
                            }
                            try
                            {
                                if (module.ModuleID > 0)
                                {
                                    //Run script  
                                    ReadUninstallScriptAndDLLFiles(doc, module.InstalledFolderPath, installerClass);
                                    //Rollback moduleid
                                    installerClass.ModulesRollBack(module.ModuleID, GetPortalID);
                                    //Delete Module's Folder
                                    installerClass.DeleteTempDirectory(module.InstalledFolderPath);
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
                                ProcessCancelRequestBase(Request.RawUrl, true, ExtensionMessage);
                            }
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageDoesNotAppearToHaveManifestFile"), "", SageMessageType.Alert);
                    }
                }
                else
                {
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
        protected void gdvControls_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


    }
}